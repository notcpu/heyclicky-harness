#!/usr/bin/env node
// clicky: install / run / inspect the HeyClicky <-> opencode gateway.
//
//   node bin/clicky.mjs install               write config + opencode commands
//   node bin/clicky.mjs on|off                start / stop the gateway
//   node bin/clicky.mjs status                what is running right now
//   node bin/clicky.mjs doctor                check keys, providers, paths, conflicts
//   node bin/clicky.mjs providers             list provider presets + current choice
//   node bin/clicky.mjs use <provider> [model] [--no-thinking|--thinking]
//                                             switch provider (and model); toggle thinking
//   node bin/clicky.mjs models [provider]     list models the provider serves
//   node bin/clicky.mjs model [id] [--no-thinking|--thinking]  show or set the model
//   node bin/clicky.mjs key [provider] [key]  show or set an API key
//   node bin/clicky.mjs base <provider> <url> set a custom base URL
//   node bin/clicky.mjs screenshots [on|off]  attach screenshots to brain turns
//   node bin/clicky.mjs test [words]          send a synthetic /brain/turn
//   node bin/clicky.mjs menu                  interactive menu
//   node bin/clicky.mjs logs [-f]             print (or follow) the gateway log

import { spawn } from "node:child_process";
import {
  closeSync,
  copyFileSync,
  existsSync,
  mkdirSync,
  openSync,
  readFileSync,
  readdirSync,
  readSync,
  rmSync,
  statSync,
  unlinkSync,
  writeFileSync,
} from "node:fs";
import os from "node:os";
import path from "node:path";
import readline from "node:readline/promises";
import { stdin as input, stdout as output } from "node:process";
import {
  CONFIG_PATH,
  ENDPOINTS_BACKUP,
  ENDPOINTS_PATH,
  HEYCLICKY_HOME,
  LOG_PATH,
  PID_PATH,
  PRODUCTION_FLAG,
  ROOT,
  authHeaders,
  ensureConfig,
  gatewayUrl,
  isNative,
  loadConfig,
  resolveProvider,
  saveConfig,
} from "../lib/config.mjs";

const SERVER = path.join(ROOT, "gateway", "server.mjs");
const COMMANDS_SRC = path.join(ROOT, "opencode", "commands");
const COMMANDS_DST = path.join(os.homedir(), ".config", "opencode", "commands");
const APPS = [
  path.join(process.env.LOCALAPPDATA || "", "HeyClickyApp", "current", "HeyClicky.exe"),
];

const c = {
  dim: (s) => `\x1b[2m${s}\x1b[0m`,
  bold: (s) => `\x1b[1m${s}\x1b[0m`,
  green: (s) => `\x1b[32m${s}\x1b[0m`,
  yellow: (s) => `\x1b[33m${s}\x1b[0m`,
  red: (s) => `\x1b[31m${s}\x1b[0m`,
  cyan: (s) => `\x1b[36m${s}\x1b[0m`,
};

async function health(cfg) {
  try {
    const res = await fetch(`${gatewayUrl(cfg)}/__clicky/health`, { signal: AbortSignal.timeout(1500) });
    if (!res.ok) return null;
    return await res.json();
  } catch {
    return null;
  }
}

function gatewayPid() {
  try {
    const pid = Number(readFileSync(PID_PATH, "utf8").trim());
    return Number.isFinite(pid) && pid > 0 ? pid : null;
  } catch {
    return null;
  }
}

function pidAlive(pid) {
  if (!pid) return false;
  try {
    process.kill(pid, 0);
    return true;
  } catch {
    return false;
  }
}

function writeEndpoints(cfg) {
  mkdirSync(HEYCLICKY_HOME, { recursive: true });
  const url = gatewayUrl(cfg);
  if (existsSync(ENDPOINTS_PATH)) {
    try {
      const current = JSON.parse(readFileSync(ENDPOINTS_PATH, "utf8"));
      if (current.workerBaseUrl !== url) copyFileSync(ENDPOINTS_PATH, ENDPOINTS_BACKUP);
    } catch {
      copyFileSync(ENDPOINTS_PATH, ENDPOINTS_BACKUP);
    }
  }
  writeFileSync(ENDPOINTS_PATH, JSON.stringify({ workerBaseUrl: url }, null, 2) + "\n");
}

function removeEndpoints() {
  if (!existsSync(ENDPOINTS_PATH)) return "already absent";
  try {
    const current = JSON.parse(readFileSync(ENDPOINTS_PATH, "utf8"));
    if (current.workerBaseUrl && !/^http:\/\/(127\.0\.0\.1|localhost|\[::1\]):/.test(current.workerBaseUrl)) {
      // Not ours: leave the user's override alone.
      return "left a non-gateway endpoints.json alone";
    }
  } catch {
    /* fall through */
  }
  unlinkSync(ENDPOINTS_PATH);
  if (existsSync(ENDPOINTS_BACKUP)) {
    copyFileSync(ENDPOINTS_BACKUP, ENDPOINTS_PATH);
    unlinkSync(ENDPOINTS_BACKUP);
    return "removed; previous endpoints.json restored";
  }
  return "removed";
}

/**
 * First useful line the child wrote to gateway.stdout.log since `fromByte`.
 * A gateway that dies at startup never reaches gateway.log — its crash trace
 * (SyntaxError, EADDRINUSE, bad flag, ...) lands in gateway.stdout.log.
 */
function childCrashReason(fromByte) {
  const file = path.join(ROOT, "logs", "gateway.stdout.log");
  try {
    const size = statSync(file).size;
    const fd = openSync(file, "r");
    const buf = Buffer.alloc(Math.max(0, size - fromByte));
    readSync(fd, buf, 0, buf.length, fromByte);
    closeSync(fd);
    const lines = buf.toString("utf8").split("\n").map((s) => s.trim()).filter(Boolean);
    const hit = lines.find((l) => /error|cannot find|eaddrinuse|unexpected|throw|refused/i.test(l)) || lines[0];
    return hit ? hit.slice(0, 240) : "";
  } catch {
    return "";
  }
}

async function startGateway(cfg) {
  const up = await health(cfg);
  if (up) return { started: false, health: up };

  mkdirSync(path.dirname(LOG_PATH), { recursive: true });
  // The gateway writes gateway.log itself; keep its stdout separate so crash
  // traces do not duplicate every line in the log.
  const outPath = path.join(ROOT, "logs", "gateway.stdout.log");
  const outMark = existsSync(outPath) ? statSync(outPath).size : 0;
  const out = openSync(outPath, "a");
  const ready = path.join(ROOT, "logs", ".ready");
  try {
    rmSync(ready, { force: true });
  } catch {
    /* ignore */
  }
  const child = spawn(process.execPath, [SERVER], {
    detached: true,
    stdio: ["ignore", out, out],
    windowsHide: true,
    env: { ...process.env, CLICKY_GATEWAY_READY_FILE: ready },
  });
  child.unref();
  writeFileSync(PID_PATH, String(child.pid));

  for (let i = 0; i < 50; i++) {
    await new Promise((r) => setTimeout(r, 200));
    const h = await health(cfg);
    if (h) return { started: true, health: h, pid: child.pid };
  }
  const why = childCrashReason(outMark);
  const alive = pidAlive(child.pid);
  throw new Error(
    `gateway did not answer on ${gatewayUrl(cfg)} — see ${LOG_PATH}` +
      (why ? `\n  child said: ${why}\n  full output: ${outPath}` : `\n  (no output captured — check ${outPath})`) +
      (alive ? `\n  child pid ${child.pid} is still running but not answering` : `\n  child pid ${child.pid} exited before answering (startup crash)`),
  );
}

async function stopGateway() {
  const up = await health(loadConfig());
  const pid = gatewayPid();
  const alive = pidAlive(pid);
  if (!up && !alive) return false;

  if (alive) {
    try {
      process.kill(pid);
    } catch {
      /* Windows fallback below */
    }
    for (let i = 0; i < 25 && pidAlive(pid); i++) await new Promise((r) => setTimeout(r, 200));
    if (pidAlive(pid) && process.platform === "win32") {
      spawn("taskkill", ["/PID", String(pid), "/F"], { windowsHide: true });
      await new Promise((r) => setTimeout(r, 600));
    }
  }
  try {
    rmSync(PID_PATH, { force: true });
  } catch {
    /* ignore */
  }
  return true;
}

// ------------------------------------------------------------------ helpers

function lane(cfg, name) {
  try {
    return { ok: true, provider: resolveProvider(cfg, cfg[name]), model: cfg[name].model };
  } catch (err) {
    return { ok: false, provider: { name: cfg[name].provider, api: "?", key: "" }, model: cfg[name].model, error: err.message };
  }
}

// --no-thinking / --thinking flip cfg[lane].thinking (undefined = provider default).
// Returns "off" | "on" | null (flag not given).
function thinkingFlag(args) {
  if (args.includes("--no-thinking")) return "off";
  if (args.includes("--thinking")) return "on";
  return null;
}

function applyThinking(cfg, lanes, flag) {
  if (!flag) return;
  for (const l of lanes) cfg[l].thinking = flag === "on";
}

function thinkingTag(cfg, lane) {
  return cfg[lane]?.thinking === false ? c.yellow(" thinking=off") : "";
}

function laneLine(l) {
  if (!l.ok) return `${c.red(l.provider.name)} ${c.dim("(" + l.error + ")")} model=${l.model}`;
  const p = l.provider;
  const key = p.key ? c.green("key") : /^https?:\/\/(127\.0\.0\.1|localhost|\[::1\])/i.test(p.baseUrl) ? c.dim("no key needed") : c.red("no key");
  return `${c.cyan(p.name)} ${c.dim("[" + p.api + "]")} model=${c.bold(l.model)} ${key}`;
}

function modelsUrl(provider) {
  if (provider.modelsUrl) {
    const url = provider.modelsUrl;
    if (provider.modelsKeyParam && provider.key) {
      return `${url}${url.includes("?") ? "&" : "?"}key=${encodeURIComponent(provider.key)}`;
    }
    return url;
  }
  return /\/v1\/?$/.test(provider.baseUrl) ? `${provider.baseUrl}/models` : `${provider.baseUrl}/v1/models`;
}

async function fetchModels(provider) {
  const res = await fetch(modelsUrl(provider), {
    headers: provider.modelsKeyParam ? { accept: "application/json" } : authHeaders(provider, { accept: "application/json" }),
    signal: AbortSignal.timeout(10_000),
  });
  const text = await res.text();
  if (!res.ok) throw new Error(`HTTP ${res.status}: ${text.slice(0, 160).replace(/\s+/g, " ")}`);
  let json;
  try {
    json = JSON.parse(text);
  } catch {
    throw new Error(`non-JSON reply: ${text.slice(0, 120)}`);
  }
  const list = Array.isArray(json?.data) ? json.data : Array.isArray(json?.models) ? json.models : [];
  return list
    .map((m) => {
      const id = String(m.id || m.name || m.model || "").replace(/^models\//, "");
      if (!id) return null;
      // Native Gemini lists embed models too; keep only generation models.
      if (provider.name === "google" && Array.isArray(m.supportedGenerationMethods)) {
        if (!m.supportedGenerationMethods.includes("generateContent")) return null;
      }
      const mods = m.architecture?.input_modalities ?? m.input_modalities ?? m.capabilities?.vision;
      let vision = Array.isArray(mods) ? mods.includes("image") : typeof mods === "boolean" ? mods : null;
      if (vision == null && provider.name === "google") {
        vision = /^gemini-(1\.5|2|3|4)/i.test(id) ? true : null;
      }
      return { id, vision };
    })
    .filter(Boolean);
}

// ------------------------------------------------------------------ commands

function cmdInstall(cfg) {
  // Deliberately does NOT write endpoints.json: pointing HeyClicky at a
  // gateway that is not running would break the app. `on` does that.
  let copied = 0;
  if (existsSync(COMMANDS_SRC)) {
    mkdirSync(COMMANDS_DST, { recursive: true });
    for (const name of readdirSync(COMMANDS_SRC).filter((f) => f.endsWith(".md"))) {
      copyFileSync(path.join(COMMANDS_SRC, name), path.join(COMMANDS_DST, name));
      copied++;
    }
  }
  console.log(c.bold("Installed"));
  console.log(`  config       ${CONFIG_PATH}`);
  console.log(`  commands     ${copied} file(s) -> ${COMMANDS_DST}`);
  console.log(c.dim(`  endpoints    written by \`on\`: ${ENDPOINTS_PATH} -> ${gatewayUrl(cfg)}`));
  if (existsSync(PRODUCTION_FLAG)) {
    console.log(c.yellow(`  warning      ${PRODUCTION_FLAG} exists; it forces the production Worker and overrides endpoints.json.`));
    console.log(c.yellow(`               delete that file (Developer -> Use production backend) before using the gateway.`));
  }
  const current = lane(cfg, "brain");
  console.log(`\n  provider     ${current.ok ? current.provider.name : current.provider.name + " (needs setup)"}`);
  console.log(c.dim("\nNext:"));
  console.log(c.dim("  node bin/clicky.mjs key <provider> <key>"));
  console.log(c.dim("  node bin/clicky.mjs use <provider> <model>"));
  console.log(c.dim("  node bin/clicky.mjs on"));
}

async function cmdOn(cfg) {
  if (existsSync(PRODUCTION_FLAG)) {
    console.log(c.yellow("use-production-backend.flag exists; HeyClicky would ignore the loopback Worker."));
    console.log(c.yellow("Turn it off first (Developer -> Use production backend), then run this again."));
    process.exitCode = 1;
    return;
  }
  writeEndpoints(cfg);
  const { started, health: h } = await startGateway(cfg);
  console.log(`${started ? c.green("Gateway started") : c.green("Gateway already running")}  ${c.dim(gatewayUrl(cfg))}`);
  const b = h.brain || { provider: "?", api: "?", model: h.brainModel, keyConfigured: h.keyConfigured };
  const a = h.agent || { provider: "?", api: "?", model: h.agentModel, keyConfigured: h.keyConfigured };
  console.log(`  brain         ${b.provider} ${c.dim("[" + b.api + "]")} ${b.model}`);
  console.log(`  agent         ${a.provider} ${c.dim("[" + a.api + "]")} ${a.model}`);
  for (const l of [b, a]) {
    if (l.error) console.log(c.red(`  ${l.provider}: ${l.error}`));
    else if (!l.keyConfigured) console.log(c.yellow(`  ${l.provider}: no API key (node bin/clicky.mjs key ${l.provider} <key>)`));
  }
  if (!started && (h.brainModel !== cfg.brain.model || h.agentModel !== cfg.agent.model)) {
    console.log(c.yellow("\nThe running gateway uses different models. Restart it: clicky off && clicky on"));
  }
  console.log(c.dim("\nQuit and reopen HeyClicky so it picks up endpoints.json."));
}

async function cmdOff() {
  const stopped = await stopGateway();
  const note = removeEndpoints();
  console.log(`${stopped ? c.green("Gateway stopped") : c.dim("Gateway was not running")}`);
  console.log(`  endpoints.json ${note}`);
  console.log(c.dim("\nReopen HeyClicky to go back to the stock Worker."));
}

async function cmdStatus(cfg) {
  const h = await health(cfg);
  const pid = gatewayPid();
  console.log(c.bold("clicky-gateway"));
  console.log(`  process       ${h ? c.green("running") : c.red("stopped")}${pid ? c.dim(` (pid ${pid})`) : ""}`);
  if (h) {
    const b = h.brain || { provider: "?", api: "?", model: h.brainModel, keyConfigured: h.keyConfigured };
    const a = h.agent || { provider: "?", api: "?", model: h.agentModel, keyConfigured: h.keyConfigured };
    console.log(`  listen        ${gatewayUrl(cfg)}  ${c.dim(h.uptimeSeconds + "s")}`);
    console.log(`  brain         ${b.provider} ${c.dim("[" + b.api + "]")} ${b.model}  ${b.keyConfigured ? c.green("key") : c.red("no key")}`);
    console.log(`  agent         ${a.provider} ${c.dim("[" + a.api + "]")} ${a.model}  ${a.keyConfigured ? c.green("key") : c.red("no key")}`);
    console.log(`  plan gate     ${h.planEnforcement === "off" ? c.green("off (local free-tier gate disabled)") : "kept"}`);
    if (h.screenshots) {
      const rejected = (h.screenshots.rejected || []).find((r) => r.lane === `${b.provider}:${b.model}`);
      const state = !h.screenshots.enabled
        ? c.dim("off (text-only mode)")
        : rejected
          ? c.yellow("dropped automatically (the model rejected images)")
          : c.green("on");
      console.log(`  screenshots   ${state}`);
    }
  }
  console.log(c.bold("\nconfig"));
  console.log(`  brain         ${laneLine(lane(cfg, "brain"))}`);
  console.log(`  agent         ${laneLine(lane(cfg, "agent"))}`);
  const endpoints = existsSync(ENDPOINTS_PATH) ? readFileSync(ENDPOINTS_PATH, "utf8").trim() : null;
  console.log(`  endpoints     ${endpoints ? c.green(endpoints) : c.dim("absent (HeyClicky uses the stock Worker)")}`);
  console.log(`  production?   ${existsSync(PRODUCTION_FLAG) ? c.red("flag present (forces prod)") : c.dim("no")}`);
}

async function cmdDoctor(cfg) {
  let bad = 0;
  const line = (label, good, detail) => {
    if (!good) bad++;
    console.log(`  ${good ? c.green("ok  ") : c.red("fail")} ${label}${detail ? c.dim("  " + detail) : ""}`);
  };

  console.log(c.bold("Paths"));
  line("config.json", existsSync(CONFIG_PATH), CONFIG_PATH);
  line("gateway server", existsSync(SERVER), SERVER);
  line("HeyClicky install", APPS.some((p) => existsSync(p)), APPS.find((p) => existsSync(p)) || APPS[0]);
  line("no production flag", !existsSync(PRODUCTION_FLAG), PRODUCTION_FLAG);

  console.log(c.bold("\nProviders"));
  for (const name of ["brain", "agent"]) {
    const l = lane(cfg, name);
    line(name, l.ok, l.ok ? `${l.provider.name} [${l.provider.api}] model=${l.model}` : l.error);
    if (!l.ok) continue;
    const local = /^https?:\/\/(127\.0\.0\.1|localhost|\[::1\])/i.test(l.provider.baseUrl);
    line(`${name} key`, Boolean(l.provider.key) || local, l.provider.key ? l.provider.key.slice(0, 8) + "…" : local ? "local, optional" : "set it with: clicky key " + l.provider.name);
  }

  const seen = new Set();
  for (const name of ["brain", "agent"]) {
    const l = lane(cfg, name);
    if (!l.ok || seen.has(l.provider.name)) continue;
    seen.add(l.provider.name);
    process.stdout.write(c.dim(`  … asking ${l.provider.name} for its model list\n`));
    try {
      const models = await fetchModels(l.provider);
      line(
        `${l.provider.name} reachable`,
        true,
        `${models.length} model(s)${models[0] ? ", e.g. " + models.slice(0, 3).map((m) => m.id).join(", ") : ""}`,
      );
    } catch (err) {
      line(`${l.provider.name} reachable`, false, err.message);
    }
  }

  console.log(c.bold("\nLocal"));
  const h = await health(cfg);
  line("gateway health", Boolean(h), h ? gatewayUrl(cfg) : "run: node bin/clicky.mjs on");
  line("endpoints.json", existsSync(ENDPOINTS_PATH), existsSync(ENDPOINTS_PATH) ? "points at the gateway" : "run: node bin/clicky.mjs on");
  if (h) {
    line(
      "gateway config in sync",
      h.brainModel === cfg.brain.model && h.agentModel === cfg.agent.model,
      h.brainModel === cfg.brain.model && h.agentModel === cfg.agent.model ? "" : "restart: clicky off && clicky on",
    );
  }

  console.log(bad === 0 ? c.green("\nAll checks passed.") : c.yellow(`\n${bad} check(s) need attention.`));
  if (bad) process.exitCode = 1;
}

function cmdProviders(cfg) {
  console.log(c.bold("Providers\n"));
  const width = Math.max(...Object.keys(cfg.providers).map((n) => n.length), 8);
  console.log(c.dim(`  ${"NAME".padEnd(width)}  API        NATIVE               KEY         LABEL`));
  for (const [name, p] of Object.entries(cfg.providers)) {
    const native = Object.keys(p.endpoints || {}).join(",") || "-";
    const keyRaw = p.key ? "set" : "-";
    const marker = name === cfg.brain.provider ? c.cyan("  <- brain") : name === cfg.agent.provider ? c.cyan("  <- agent") : "";
    console.log(`  ${name.padEnd(width)}  ${(p.api || "chat").padEnd(9)}  ${native.padEnd(20)}  ${keyRaw.padEnd(10)}  ${p.label || ""}${marker}`);
  }
  console.log(c.dim("\n  switch:  node bin/clicky.mjs use <provider> [model]"));
  console.log(c.dim("  models:  node bin/clicky.mjs models [provider]"));
}

function cmdUse(cfg, args) {
  const name = args[0];
  if (!name) {
    console.log(`brain  ${laneLine(lane(cfg, "brain"))}${thinkingTag(cfg, "brain")}`);
    console.log(`agent  ${laneLine(lane(cfg, "agent"))}${thinkingTag(cfg, "agent")}`);
    console.log(c.dim("\nnode bin/clicky.mjs use <provider> [model]            both lanes"));
    console.log(c.dim("node bin/clicky.mjs use <provider> [model] --brain    brain only"));
    console.log(c.dim("node bin/clicky.mjs use <provider> [model] --agent    agent only"));
    console.log(c.dim("node bin/clicky.mjs use <provider> [model] --no-thinking   turn thinking off"));
    console.log(c.dim("node bin/clicky.mjs use <provider> [model] --thinking      turn thinking back on"));
    return;
  }
  if (!cfg.providers[name]) {
    console.log(c.red(`unknown provider "${name}".`));
    console.log(c.dim("try: " + Object.keys(cfg.providers).join(" | ")));
    process.exitCode = 1;
    return;
  }
  const model = args[1] && !args[1].startsWith("--") ? args[1] : null;
  const lanes = args.includes("--brain") ? ["brain"] : args.includes("--agent") ? ["agent"] : ["brain", "agent"];
  for (const l of lanes) {
    cfg[l].provider = name;
    if (model) cfg[l].model = model;
  }
  applyThinking(cfg, lanes, thinkingFlag(args));
  saveConfig(cfg);
  for (const l of lanes) console.log(`${l}  ${name}${model ? "  model=" + model : "  model=" + cfg[l].model}${thinkingTag(cfg, l)}`);
  console.log(c.dim("\nrestart the gateway for it to take effect: node bin/clicky.mjs off && node bin/clicky.mjs on"));
}

async function cmdModels(cfg, args) {
  const name = args[0] || cfg.brain.provider;
  let provider;
  try {
    provider = resolveProvider(cfg, { provider: name, model: cfg.brain.model });
  } catch (err) {
    console.log(c.red(err.message));
    process.exitCode = 1;
    return;
  }
  console.log(c.dim(`${name}  ${modelsUrl(provider)}`));
  let models;
  try {
    models = await fetchModels(provider);
  } catch (err) {
    console.log(c.red("could not list models: " + err.message));
    if (!provider.key) console.log(c.dim(`set a key first: node bin/clicky.mjs key ${name} <key>`));
    process.exitCode = 1;
    return;
  }
  if (!models.length) {
    console.log(c.yellow("provider returned an empty list"));
    return;
  }
  const query = args[1]?.toLowerCase();
  const shown = query ? models.filter((m) => m.id.toLowerCase().includes(query)) : models;
  for (const m of shown) {
    const tag = m.vision === true ? c.green("  [vision]") : m.vision === false ? c.dim("  [text]") : "";
    console.log("  " + m.id + tag);
  }
  console.log(c.dim(`\n${shown.length} of ${models.length} model(s). set one: node bin/clicky.mjs use ${name} <model>`));
  if (shown.some((m) => m.vision === true)) {
    console.log(c.dim("HeyClicky sends a screenshot each turn: prefer a [vision] model, or run `clicky screenshots off`."));
  }
}

function cmdModel(cfg, args) {
  const id = args[0] && !args[0].startsWith("--") ? args[0] : null;
  if (!id) {
    console.log(`brain model  ${cfg.brain.model}  ${c.dim("(" + cfg.brain.provider + ")")}`);
    console.log(`agent model  ${cfg.agent.model}  ${c.dim("(" + cfg.agent.provider + ")")}`);
    console.log(c.dim("\nnode bin/clicky.mjs model <id>            set both"));
    console.log(c.dim("node bin/clicky.mjs model <id> --brain    brain only"));
    console.log(c.dim("node bin/clicky.mjs model <id> --agent    agent only"));
    console.log(c.dim("node bin/clicky.mjs model <id> --no-thinking | --thinking   toggle thinking"));
    console.log(c.dim("node bin/clicky.mjs models <provider>     list what the provider serves"));
    return;
  }
  const lanes = args.includes("--brain") ? ["brain"] : args.includes("--agent") ? ["agent"] : ["brain", "agent"];
  for (const l of lanes) cfg[l].model = id;
  applyThinking(cfg, lanes, thinkingFlag(args));
  saveConfig(cfg);
  console.log(`updated ${CONFIG_PATH}: brain=${cfg.brain.model}${thinkingTag(cfg, "brain")} agent=${cfg.agent.model}${thinkingTag(cfg, "agent")}`);
  console.log(c.dim("restart the gateway: node bin/clicky.mjs off && node bin/clicky.mjs on"));
}

function cmdKey(cfg, args) {
  if (args.length === 1 && cfg.providers[args[0]]) {
    const p = cfg.providers[args[0]];
    console.log(p.key ? `${args[0]} key: ${p.key.slice(0, 10)}…` : `${args[0]}: no key set`);
    return;
  }
  if (!args.length) {
    for (const [name, p] of Object.entries(cfg.providers)) {
      console.log(`  ${name.padEnd(12)} ${p.key ? c.green("set") : c.dim("-")}`);
    }
    console.log(c.dim("\nnode bin/clicky.mjs key <provider> <key>"));
    return;
  }
  let name;
  let key;
  if (cfg.providers[args[0]]) {
    name = args[0];
    key = args[1];
  } else {
    name = cfg.brain.provider;
    key = args[0];
  }
  if (!cfg.providers[name]) {
    console.log(c.red(`unknown provider "${name}"`));
    process.exitCode = 1;
    return;
  }
  if (!key) {
    console.log(c.red("no key given"));
    process.exitCode = 1;
    return;
  }
  cfg.providers[name].key = key.trim();
  saveConfig(cfg);
  console.log(`saved ${name} key to ${CONFIG_PATH}`);
  console.log(c.dim("restart the gateway for it to take effect: node bin/clicky.mjs off && node bin/clicky.mjs on"));
}

function cmdScreenshots(cfg, args) {
  const value = args[0];
  if (!value) {
    console.log(`screenshots  ${cfg.brain.screenshots !== false ? "on" : "off"}`);
    console.log(c.dim("\nnode bin/clicky.mjs screenshots off    for a text-only model"));
    console.log(c.dim("node bin/clicky.mjs screenshots on     back to normal"));
    return;
  }
  cfg.brain.screenshots = !/^(off|false|no|0)$/i.test(value);
  saveConfig(cfg);
  console.log(`screenshots ${cfg.brain.screenshots ? "on" : "off"}`);
  console.log(c.dim("restart the gateway: node bin/clicky.mjs off && node bin/clicky.mjs on"));
}

function cmdBase(cfg, args) {  const [name, url] = args;
  if (!name || !cfg.providers[name]) {
    console.log(c.red("usage: node bin/clicky.mjs base <provider> <baseUrl>"));
    console.log(c.dim("providers: " + Object.keys(cfg.providers).join(" | ")));
    process.exitCode = 1;
    return;
  }
  if (!url) {
    console.log(`${name}  ${cfg.providers[name].baseUrl || "(unset)"}`);
    return;
  }
  cfg.providers[name].baseUrl = url.replace(/\/+$/, "");
  saveConfig(cfg);
  console.log(`${name} baseUrl = ${cfg.providers[name].baseUrl}`);
  console.log(c.dim("restart the gateway for it to take effect."));
}

/** Synthetic turn: exercises buildBrainRequest + SSE relay without HeyClicky. */
async function cmdTest(cfg, args) {
  const up = await health(cfg);
  if (!up) {
    console.log(c.red("gateway is not running: node bin/clicky.mjs on"));
    process.exitCode = 1;
    return;
  }
  const words = args.join(" ") || "draw a small sun and say good morning";
  const body = {
    session_id: "gateway-selftest",
    thread: [],
    turn: { words, input: "text", live_state: "listening", trigger: "test", time_label: new Date().toISOString() },
    turn_items: [],
    max_rounds: 5,
    async_tools: true,
  };
  console.log(c.dim(`POST /brain/turn  words="${words}"`));
  const res = await fetch(`${gatewayUrl(cfg)}/brain/turn`, {
    method: "POST",
    headers: { "content-type": "application/json", authorization: "Bearer test" },
    body: JSON.stringify(body),
  });
  if (!res.ok) {
    console.log(c.red(`HTTP ${res.status}: ${await res.text()}`));
    process.exitCode = 1;
    return;
  }
  const text = await res.text();
  let program = "";
  let fallback = false;
  for (const line of text.split("\n")) {
    if (!line.startsWith("data:")) continue;
    let evt;
    try {
      evt = JSON.parse(line.slice(5).trim());
    } catch {
      continue;
    }
    if (evt.type === "meta" && evt.fallback) {
      fallback = true;
      console.log(c.yellow("[fallback] the provider failed; the gateway spoke an apology instead"));
    }
    if (evt.type === "text") {
      program += evt.text;
      process.stdout.write(evt.text);
    } else if (evt.type === "tool_calls") {
      console.log(c.cyan(`\n[tool_calls] ${evt.calls.map((x) => x.name).join(", ")}`));
    } else if (evt.type === "error") {
      console.log(c.red(`\n[error] ${evt.message}`));
    } else if (evt.type === "done") {
      console.log(c.dim(`\n[done] ${JSON.stringify(evt.usage)}`));
    }
  }
  if (!program.trim()) console.log(c.yellow("\n(no text streamed)"));
  if (fallback) process.exitCode = 1;
}

function cmdLogs(args) {
  if (!existsSync(LOG_PATH)) {
    console.log("no log yet:", LOG_PATH);
    return;
  }
  const follow = args.includes("-f") || args.includes("--follow");
  if (!follow) {
    process.stdout.write(readFileSync(LOG_PATH, "utf8"));
    return;
  }
  let offset = statSync(LOG_PATH).size;
  console.log(c.dim(`following ${LOG_PATH} (ctrl+c to stop)`));
  setInterval(() => {
    let size;
    try {
      size = statSync(LOG_PATH).size;
    } catch {
      return;
    }
    if (size <= offset) return;
    const fd = openSync(LOG_PATH, "r");
    const buf = Buffer.alloc(size - offset);
    readSync(fd, buf, 0, buf.length, offset);
    closeSync(fd);
    process.stdout.write(buf);
    offset = size;
  }, 500);
}

async function cmdMenu(cfg) {
  const rl = readline.createInterface({ input, output });
  for (;;) {
    const h = await health(cfg);
    const b = lane(cfg, "brain");
    const a = lane(cfg, "agent");
    console.log("");
    console.log(c.bold("  HeyClicky x opencode"));
    console.log(c.dim(`  gateway ${h ? "● running" : "○ stopped"}`));
    console.log(`  brain   ${b.ok ? b.provider.name : "?"}  ${b.model}`);
    console.log(`  agent   ${a.ok ? a.provider.name : "?"}  ${a.model}`);
    console.log("");
    console.log("  1  start gateway (point HeyClicky at it)");
    console.log("  2  stop gateway (restore stock Worker)");
    console.log("  3  status");
    console.log("  4  doctor");
    console.log("  5  choose provider + model");
    console.log("  6  set an API key");
    console.log("  7  list models");
    console.log("  8  send a test turn");
    console.log("  9  show report path");
    console.log("  0  quit");
    const answer = (await rl.question("\n  choose: ")).trim();
    try {
      if (answer === "1") await cmdOn(cfg);
      else if (answer === "2") await cmdOff();
      else if (answer === "3") await cmdStatus(cfg);
      else if (answer === "4") await cmdDoctor(cfg);
      else if (answer === "5") {
        const names = Object.keys(cfg.providers);
        names.forEach((n, i) => console.log(`   ${i + 1}) ${n}  ${c.dim(cfg.providers[n].label || "")}`));
        const pick = (await rl.question("  provider: ")).trim();
        const name = names[Number(pick) - 1] || pick;
        if (!cfg.providers[name]) {
          console.log(c.red("  unknown provider"));
          continue;
        }
        let model = (await rl.question("  model (blank = keep): ")).trim();
        if (!model) {
          try {
            const models = await fetchModels(resolveProvider(cfg, { provider: name, model: "" }));
            models.slice(0, 20).forEach((m, i) => console.log(`   ${i + 1}) ${m.id}${m.vision === true ? c.green("  [vision]") : ""}`));
            const pick = (await rl.question("  pick number or type an id (blank = keep): ")).trim();
            model = models[Number(pick) - 1]?.id || pick;
          } catch (err) {
            console.log(c.dim("  could not list models: " + err.message));
          }
        }
        cmdUse(cfg, model ? [name, model] : [name]);
      } else if (answer === "6") {
        const name = (await rl.question("  provider: ")).trim() || cfg.brain.provider;
        const key = (await rl.question("  key: ")).trim();
        if (key) cmdKey(cfg, [name, key]);
      } else if (answer === "7") {
        const name = (await rl.question("  provider (blank = current): ")).trim();
        await cmdModels(cfg, name ? [name] : []);
      } else if (answer === "8") {
        const words = (await rl.question("  words: ")).trim();
        await cmdTest(cfg, words ? words.split(/\s+/) : []);
      } else if (answer === "9") {
        console.log("  " + path.join(ROOT, "report", "HEYCLICKY-AI-ARCHITECTURE.md"));
      } else if (answer === "0") break;
    } catch (err) {
      console.log(c.red("  " + err.message));
    }
  }
  rl.close();
}

// --------------------------------------------------------------------- main

const [command = "status", ...args] = process.argv.slice(2);
const cfg = ensureConfig();

switch (command) {
  case "install":
    cmdInstall(cfg);
    break;
  case "on":
  case "start":
    await cmdOn(cfg);
    break;
  case "off":
  case "stop":
    await cmdOff();
    break;
  case "status":
    await cmdStatus(cfg);
    break;
  case "doctor":
    await cmdDoctor(cfg);
    break;
  case "providers":
    cmdProviders(cfg);
    break;
  case "use":
    cmdUse(cfg, args);
    break;
  case "models":
    await cmdModels(cfg, args);
    break;
  case "model":
    cmdModel(cfg, args);
    break;
  case "key":
    cmdKey(cfg, args);
    break;
  case "base":
    cmdBase(cfg, args);
    break;
  case "screenshots":
  case "images":
    cmdScreenshots(cfg, args);
    break;
  case "test":
    await cmdTest(cfg, args);
    break;
  case "logs":
    cmdLogs(args);
    break;
  case "menu":
    await cmdMenu(cfg);
    break;
  case "help":
  case "--help":
  case "-h":
    console.log(
      readFileSync(new URL(import.meta.url), "utf8")
        .split("\n")
        .slice(1, 18)
        .map((l) => l.replace(/^\/\/ ?/, ""))
        .join("\n"),
    );
    break;
  default:
    console.log(`unknown command: ${command}`);
    console.log("try: install | on | off | status | doctor | providers | use | models | model | key | base | test | logs | menu");
    process.exitCode = 1;
}
