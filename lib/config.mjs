// Shared configuration for the Clicky gateway + CLI.
//
// Everything lives next to the checkout (gateway/config.json) so the whole
// integration is self-contained and portable. Environment variables override
// the file, which is handy for one-off experiments.

import { existsSync, mkdirSync, readFileSync, writeFileSync } from "node:fs";
import path from "node:path";
import { fileURLToPath } from "node:url";

export const ROOT = path.resolve(path.dirname(fileURLToPath(import.meta.url)), "..");
export const CONFIG_PATH = process.env.CLICKY_GATEWAY_CONFIG || path.join(ROOT, "gateway", "config.json");
export const LOG_PATH = path.join(ROOT, "logs", "gateway.log");
export const PID_PATH = path.join(ROOT, "logs", "gateway.pid");

const LOCALAPPDATA = process.env.LOCALAPPDATA || path.join(process.env.USERPROFILE || ".", "AppData", "Local");

/** HeyClicky writes all of its state here (AppPaths.Root). */
export const HEYCLICKY_HOME = path.join(LOCALAPPDATA, "HeyClicky");
/** The sanctioned developer override file read by AppBundleConfiguration.Load(). */
export const ENDPOINTS_PATH = path.join(HEYCLICKY_HOME, "endpoints.json");
export const ENDPOINTS_BACKUP = path.join(HEYCLICKY_HOME, "endpoints.json.clicky-backup");
/** When this file exists the app throws away the loopback worker URL. */
export const PRODUCTION_FLAG = path.join(HEYCLICKY_HOME, "use-production-backend.flag");

/**
 * Provider presets.
 *
 * `api` is the protocol used when the provider is asked for something it has
 * no native endpoint for. `endpoints` lists the native paths it does speak;
 * the gateway uses them directly (highest fidelity) and only translates when
 * the path is missing. Paths are appended to `baseUrl`.
 *
 *   responses — OpenAI Responses API   (HeyClicky's native agent protocol)
 *   chat      — OpenAI Chat Completions (OpenRouter, LM Studio, Ollama, ...)
 *   messages  — Anthropic Messages
 */
export const PROVIDERS = {
  opencode: {
    label: "opencode Console",
    api: "responses",
    baseUrl: "https://opencode.ai/inference",
    plan: "payg", // "payg" | "go"
    endpoints: {
      responses: "/openai/v1/responses",
      messages: "/anthropic/v1/messages",
    },
    key: "",
  },
  openrouter: {
    label: "OpenRouter",
    api: "chat",
    baseUrl: "https://openrouter.ai/api/v1",
    endpoints: { chat: "/chat/completions" },
    headers: { "X-Title": "HeyClicky" },
    key: "",
  },
  openai: {
    label: "OpenAI",
    api: "responses",
    baseUrl: "https://api.openai.com/v1",
    endpoints: { responses: "/responses", chat: "/chat/completions" },
    key: "",
  },
  anthropic: {
    label: "Anthropic",
    api: "anthropic",
    baseUrl: "https://api.anthropic.com",
    endpoints: { messages: "/v1/messages" },
    key: "",
  },
  google: {
    label: "Google Gemini (AI Studio)",
    api: "chat",
    baseUrl: "https://generativelanguage.googleapis.com/v1beta/openai",
    endpoints: { chat: "/chat/completions" },
    // The compat layer has no /models, so list them from the native API.
    modelsUrl: "https://generativelanguage.googleapis.com/v1beta/models",
    modelsKeyParam: true,
    key: "",
  },
  lmstudio: {
    label: "LM Studio (local)",
    api: "chat",
    baseUrl: "http://127.0.0.1:1234/v1",
    endpoints: { chat: "/chat/completions" },
    key: "lm-studio",
  },
  ollama: {
    label: "Ollama (local)",
    api: "chat",
    baseUrl: "http://127.0.0.1:11434/v1",
    endpoints: { chat: "/chat/completions" },
    key: "ollama",
  },
  custom: {
    label: "Custom OpenAI-compatible",
    api: "chat",
    baseUrl: "",
    endpoints: { chat: "/chat/completions" },
    key: "",
  },
};

export const DEFAULTS = {
  port: 8787,
  host: "127.0.0.1",

  providers: structuredClone(PROVIDERS),

  // The cursor brain: the SSE endpoint HeyClicky calls for every spoken turn.
  brain: {
    provider: "opencode",
    model: "gpt-6.1-sol",
    effort: "low",
    maxOutputTokens: 4096,
    // Attach the screenshot HeyClicky sends on every turn. Turn this off for
    // text-only models (the gateway also retries without images automatically
    // when a provider rejects them).
    screenshots: true,
    // Client-side tools we expose to the model. All three run locally on
    // Windows inside HeyClicky (see CompanionSession.RunToolCallAsync).
    tools: ["open_url", "copy_to_clipboard", "insert_text"],
    // Save every /brain/turn body here (null = off).
    dumpDir: null,
    // If the provider still fails after retries, speak a short apology instead
    // of leaving Clicky silent. Turn off to debug raw errors.
    speakErrors: true,
    // false = ask the provider for the least thinking it allows (Gemini today).
    // Toggle with: clicky use <provider> [model] --no-thinking | --thinking
    thinking: true,
    // Optional: force the level used when thinking is off on level-based Gemini
    // models (minimal | low | medium | high). Handy for brand-new models.
    thinkingOffLevel: null,
  },

  // The agent harness (jobs, Clicky drive): model calls made by
  // clicky-harness.exe, rewritten to the chosen provider/model.
  agent: {
    provider: "opencode",
    model: "gpt-6.1-sol",
    effort: "medium",
    maxTokens: 8192,
    // Fields HeyClicky's harness sends that some upstreams do not support.
    strip: ["store", "previous_response_id", "prompt_cache_key"],
    preserveRequestedModel: false,
    headers: {},
    thinking: true,
    thinkingOffLevel: null,
  },

  // Everything that is not model inference still goes to the real Worker.
  proxy: {
    enabled: true,
    baseUrl: "https://api.heyclicky.com",
    // "off" rewrites /me/plan to report billing_enforcement:"off", which is
    // the client's own switch for its local free-tier message gate.
    planEnforcement: "off",
  },

  log: { verbose: false },

  // Extra attempts for transient provider failures (429 / 5xx / network), with
  // short exponential backoff. 2 = up to 3 tries, roughly 1.2s of waiting.
  providerRetries: 2,
};

function deepMerge(base, patch) {
  if (patch === null || patch === undefined) return base;
  if (Array.isArray(patch)) return patch.slice();
  if (typeof patch !== "object") return patch;
  const out = Array.isArray(base) ? base.slice() : { ...base };
  for (const [k, v] of Object.entries(patch)) {
    out[k] = v && typeof v === "object" && !Array.isArray(v) ? deepMerge(out[k] ?? {}, v) : v;
  }
  return out;
}

export function loadConfig() {
  let cfg = structuredClone(DEFAULTS);
  if (existsSync(CONFIG_PATH)) {
    try {
      cfg = deepMerge(cfg, JSON.parse(readFileSync(CONFIG_PATH, "utf8")));
    } catch (err) {
      console.error(`[config] ${CONFIG_PATH} is not valid JSON: ${err.message}`);
    }
  }

  // Migration: the v1 shape had one `upstream` block and no providers.
  const u = cfg.upstream;
  if (u && cfg.providers?.opencode) {
    if (u.baseUrl) cfg.providers.opencode.baseUrl = u.baseUrl;
    if (u.apiKey) cfg.providers.opencode.key = u.apiKey;
    if (u.plan) cfg.providers.opencode.plan = u.plan;
    if (u.headers) cfg.providers.opencode.headers = { ...(cfg.providers.opencode.headers || {}), ...u.headers };
  }
  // Migration: v2 lanes named a provider but no model default existed.
  if (typeof cfg.brain.provider !== "string") cfg.brain.provider = "opencode";
  if (typeof cfg.agent.provider !== "string") cfg.agent.provider = "opencode";

  // Environment overrides win over the file.
  if (process.env.CLICKY_PROVIDER) {
    cfg.brain.provider = process.env.CLICKY_PROVIDER;
    cfg.agent.provider = process.env.CLICKY_PROVIDER;
  }
  if (process.env.CLICKY_UPSTREAM_KEY) {
    const names = new Set([cfg.brain.provider, cfg.agent.provider, "opencode"]);
    for (const name of names) if (cfg.providers[name]) cfg.providers[name].key = process.env.CLICKY_UPSTREAM_KEY;
  }
  if (process.env.CLICKY_UPSTREAM_BASE) {
    const name = cfg.brain.provider || "opencode";
    if (cfg.providers[name]) cfg.providers[name].baseUrl = process.env.CLICKY_UPSTREAM_BASE;
  }
  if (process.env.CLICKY_BRAIN_MODEL) cfg.brain.model = process.env.CLICKY_BRAIN_MODEL;
  if (process.env.CLICKY_AGENT_MODEL) cfg.agent.model = process.env.CLICKY_AGENT_MODEL;
  if (process.env.CLICKY_PORT) cfg.port = Number(process.env.CLICKY_PORT);

  cfg.port = Number(cfg.port) || 8787;
  cfg.brain.maxOutputTokens = Number(cfg.brain.maxOutputTokens) || 4096;
  return cfg;
}

export function saveConfig(cfg) {
  // Never persist the migration-only legacy block.
  if (cfg.upstream) delete cfg.upstream;
  mkdirSync(path.dirname(CONFIG_PATH), { recursive: true });
  writeFileSync(CONFIG_PATH, JSON.stringify(cfg, null, 2) + "\n");
  return CONFIG_PATH;
}

export function ensureConfig() {
  if (!existsSync(CONFIG_PATH)) saveConfig(structuredClone(DEFAULTS));
  return loadConfig();
}

/** Resolve the provider a lane (cfg.brain | cfg.agent) uses. */
export function resolveProvider(cfg, lane) {
  const name = lane.provider || "opencode";
  const preset = cfg.providers?.[name];
  if (!preset) throw new Error(`unknown provider "${name}" (try: node bin/clicky.mjs providers)`);
  const p = { name, ...preset };
  p.endpoints = { ...(preset.endpoints || {}) };
  p.api = p.api || "chat";
  p.key = p.key || "";
  p.baseUrl = (p.baseUrl || "").replace(/\/+$/, "");
  if (!p.baseUrl) throw new Error(`provider "${name}" has no baseUrl (edit gateway/config.json or run: clicky base ${name} <url>)`);
  if (name === "opencode" && p.plan === "go" && !/\/go$/.test(p.baseUrl)) p.baseUrl += "/go";
  return p;
}

/** Absolute URL for a native protocol endpoint of a provider. */
export function apiUrl(provider, kind) {
  const native = provider.endpoints?.[kind];
  if (native) return provider.baseUrl + native;
  const fallback =
    kind === "chat" ? "/chat/completions" : kind === "responses" ? "/responses" : "/v1/messages";
  return provider.baseUrl + fallback;
}

/** Does the provider serve this protocol itself (no translation needed)? */
export function isNative(provider, kind) {
  return Boolean(provider.endpoints?.[kind]);
}

/** Auth headers for a provider. */
export function authHeaders(provider, extra = {}) {
  // Local servers usually ignore auth, so a key is optional for loopback.
  const local = /^https?:\/\/(127\.0\.0\.1|localhost|\[::1\])(:|\/|$)/i.test(provider.baseUrl || "");
  if (!provider.key && !local) {
    throw new Error(
      `no API key for provider "${provider.name}": run  node bin/clicky.mjs key ${provider.name} <key>`,
    );
  }
  let base = {};
  if (provider.key) {
    base =
      provider.api === "anthropic"
        ? { "x-api-key": provider.key, "anthropic-version": "2023-06-01" }
        : { authorization: `Bearer ${provider.key}` };
  }
  return { ...base, "content-type": "application/json", ...(provider.headers || {}), ...extra };
}

export function gatewayUrl(cfg) {
  return `http://${cfg.host === "0.0.0.0" ? "127.0.0.1" : cfg.host}:${cfg.port}`;
}
