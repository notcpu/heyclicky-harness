// clicky-gateway: a loopback Worker for HeyClicky that routes model inference
// to the provider you choose (opencode Console, OpenRouter, OpenAI, Anthropic,
// LM Studio, Ollama, any OpenAI-compatible server) and proxies everything else
// to the real Worker.
//
//   HeyClicky.exe --(endpoints.json)--> 127.0.0.1:8787 --+--> provider (inference)
//                                                        \--> api.heyclicky.com (rest)
//
// Run:  node gateway/server.mjs        (or: bun gateway/server.mjs)
// Logs: logs/gateway.log

import http from "node:http";
import { appendFileSync, mkdirSync, writeFileSync } from "node:fs";
import path from "node:path";
import { gunzipSync } from "node:zlib";
import { LOG_PATH, apiUrl, authHeaders, isNative, loadConfig, resolveProvider } from "../lib/config.mjs";
import { applyThinking } from "../lib/thinking.mjs";
import { buildBrainRequest } from "./brain.mjs";
import {
  chatChunksToBrain,
  chatChunksToMessages,
  chatChunksToResponses,
  chatJsonToMessages,
  chatJsonToResponses,
  chatToMessages,
  chatToResponses,
  emitMessagesSse,
  messagesJsonToChat,
  messagesStreamToChat,
  messagesToChat,
  responsesJsonToChat,
  responsesStreamToChat,
  responsesToChat,
  sseJson,
} from "./translate.mjs";

const cfg = loadConfig();
const startedAt = Date.now();

// ---------------------------------------------------------------- utilities

function log(level, msg, extra) {
  const line = `${new Date().toISOString()} ${level} ${msg}${extra ? " " + extra : ""}`;
  console.log(line);
  try {
    mkdirSync(path.dirname(LOG_PATH), { recursive: true });
    appendFileSync(LOG_PATH, line + "\n");
  } catch {
    /* logging must never break a turn */
  }
}

const REDACT = /^(authorization|cookie|set-cookie|x-api-key)$/i;

function logHeaders(headers) {
  if (!cfg.log.verbose) return "";
  return (
    " [" +
    Object.entries(headers)
      .map(([k, v]) => `${k}=${REDACT.test(k) ? "***" : Array.isArray(v) ? v.join(",") : v}`)
      .join(" ") +
    "]"
  );
}

async function readBody(req) {
  const chunks = [];
  let total = 0;
  for await (const chunk of req) {
    total += chunk.length;
    if (total > 64 * 1024 * 1024) throw new Error("request body too large");
    chunks.push(chunk);
  }
  let buf = Buffer.concat(chunks);
  const enc = String(req.headers["content-encoding"] || "").toLowerCase();
  if (enc.includes("gzip") && buf.length) buf = gunzipSync(buf);
  return buf;
}

function sendJson(res, status, obj, extraHeaders = {}) {
  const body = Buffer.from(JSON.stringify(obj));
  res.writeHead(status, {
    "content-type": "application/json",
    "content-length": body.length,
    ...extraHeaders,
  });
  res.end(body);
}

function openSse(res) {
  res.writeHead(200, {
    "content-type": "text/event-stream; charset=utf-8",
    "cache-control": "no-cache, no-transform",
    connection: "keep-alive",
    "x-accel-buffering": "no",
    // Never advertise gzip: HeyClicky then sends /brain/turn uncompressed.
    "x-clicky-accepts-gzip": "0",
  });
  if (typeof res.flushHeaders === "function") res.flushHeaders();
  return (event) => {
    if (res.writableEnded) return;
    res.write(`data:${JSON.stringify(event)}\n\n`);
  };
}

/** SSE writer that also emits `event:` lines (Responses / Messages shape). */
function sseEventWriter(res) {
  return (event) => {
    if (res.writableEnded) return;
    res.write(`event: ${event.type || "message"}\ndata: ${JSON.stringify(event)}\n\n`);
  };
}

/** Pick the protocol we can actually reach this provider with. */
function routeMode(provider, desired) {
  if (isNative(provider, desired)) return desired;
  const order = desired === "responses" ? ["chat", "messages"] : ["chat", "responses"];
  for (const kind of order) if (isNative(provider, kind)) return kind;
  return provider.api === "anthropic" ? "messages" : provider.api === "responses" ? "responses" : "chat";
}

function providerHeaders(provider, extra = {}) {
  return authHeaders(provider, { accept: "text/event-stream", ...extra });
}

function laneInfo(laneName) {
  const lane = cfg[laneName];
  try {
    const p = resolveProvider(cfg, lane);
    return {
      provider: p.name,
      label: p.label,
      api: p.api,
      model: lane.model,
      baseUrl: p.baseUrl,
      native: Object.keys(p.endpoints || {}),
      keyConfigured: Boolean(p.key),
    };
  } catch (err) {
    return { provider: lane.provider, model: lane.model, error: err.message, keyConfigured: false };
  }
}

/** Statuses that mean "upstream is busy", not "your request is wrong". */
const RETRY_STATUS = new Set([408, 409, 429, 500, 502, 503, 504]);

/**
 * When the provider fails outright, stream a tiny valid program so Clicky says
 * something instead of going silent. The real error still lands in the log.
 */
function speakFailure(res, reason, detail) {
  if (res.headersSent || res.writableEnded || cfg.brain.speakErrors === false) return false;
  const emit = openSse(res);
  emit({ type: "meta", model: cfg.brain.model, user_layer: "gateway", fallback: true });
  const reason2 = reason.charAt(0).toUpperCase() + reason.slice(1);
  const line = `Sorry, I couldn't reach the model. ${reason2}. Please try again in a moment.`;
  emit({ type: "text", text: `say(${JSON.stringify(line)});\n` });
  emit({
    type: "done",
    input: 0,
    last_input: 0,
    output: 0,
    cost: 0,
    usage: { fallback: true, error: String(detail ?? "").slice(0, 300) },
  });
  res.end();
  return true;
}

/** Wait, but wake up early (and reject) if the turn is cancelled. */
function sleep(ms, signal) {
  return new Promise((resolve, reject) => {
    if (signal?.aborted) return reject(new Error("aborted"));
    const timer = setTimeout(() => {
      signal?.removeEventListener("abort", onAbort);
      resolve();
    }, ms);
    function onAbort() {
      clearTimeout(timer);
      reject(new Error("aborted"));
    }
    signal?.addEventListener("abort", onAbort, { once: true });
  });
}

/**
 * POST to the provider, retrying transient failures (network errors, 429, 5xx).
 *
 * Google in particular answers 503 "experiencing high demand" for popular
 * models; a couple of short retries usually lands on capacity. The response
 * body is only consumed on the failure path, so a successful streaming reply
 * is returned untouched.
 */
async function postProvider(provider, kind, body, signal, extraHeaders = {}) {
  const url = apiUrl(provider, kind);
  const payload = JSON.stringify(body);
  const attempts = 1 + Math.max(0, Number(cfg.providerRetries ?? 2));
  let lastError = null;

  for (let attempt = 1; attempt <= attempts; attempt++) {
    let res;
    try {
      res = await fetch(url, {
        method: "POST",
        headers: providerHeaders(provider, extraHeaders),
        body: payload,
        signal,
      });
    } catch (err) {
      if (signal?.aborted) throw err;
      lastError = new Error(`provider unreachable: ${err.message}`);
      if (attempt === attempts) throw lastError;
      const wait = 400 * 2 ** (attempt - 1);
      log("WARN", `provider network error (attempt ${attempt}/${attempts}), retrying in ${wait}ms`, err.message);
      await sleep(wait, signal);
      continue;
    }

    if (!RETRY_STATUS.has(res.status) || attempt === attempts) return res;

    const detail = await res.text().catch(() => "");
    lastError = new Error(`provider ${res.status}: ${detail.slice(0, 200)}`);
    const wait = 400 * 2 ** (attempt - 1);
    log(
      "WARN",
      `provider ${res.status} (attempt ${attempt}/${attempts}), retrying in ${wait}ms`,
      detail.slice(0, 160).replace(/\s+/g, " "),
    );
    await sleep(wait, signal);
  }

  throw lastError ?? new Error("provider request failed");
}

// ---------------------------------------------------------------- brain turn

/** Parse a Responses SSE stream and forward it as HeyClicky SSE events. */
async function relayBrainStream(upstreamRes, emit, meta) {
  const decoder = new TextDecoder();
  let buffer = "";
  let textSeen = 0;
  let usage = null;
  let failure = null;
  const pending = new Map(); // item id -> { call_id, name, arguments }
  const calls = [];

  const handle = (payload) => {
    let evt;
    try {
      evt = JSON.parse(payload);
    } catch {
      return;
    }
    const type = evt.type;

    if (type === "response.output_text.delta" && typeof evt.delta === "string") {
      if (evt.delta.length) {
        textSeen += evt.delta.length;
        emit({ type: "text", text: evt.delta });
      }
      return;
    }

    if (type === "response.output_item.added" && evt.item?.type === "function_call") {
      pending.set(evt.item.id || evt.item.call_id, {
        call_id: evt.item.call_id || evt.item.id,
        name: evt.item.name,
        arguments: evt.item.arguments || "",
      });
      return;
    }

    if (type === "response.function_call_arguments.delta" && typeof evt.delta === "string") {
      const item = pending.get(evt.item_id);
      if (item) item.arguments += evt.delta;
      return;
    }

    if (type === "response.output_item.done" && evt.item?.type === "function_call") {
      const key = evt.item.id || evt.item.call_id;
      const item = pending.get(key) || {
        call_id: evt.item.call_id || evt.item.id,
        name: evt.item.name,
        arguments: "",
      };
      if (!item.arguments && evt.item.arguments) item.arguments = evt.item.arguments;
      if (item.name) calls.push({ ...item });
      pending.delete(key);
      return;
    }

    if (type === "response.completed") {
      usage = evt.response?.usage ?? null;
      return;
    }

    if (type === "response.failed" || type === "response.incomplete") {
      failure = evt.response?.error?.message || evt.response?.incomplete_details?.reason || "the model stopped early";
      return;
    }

    if (type === "error") {
      failure = evt.message || evt.error?.message || "upstream error";
    }
  };

  const feed = (chunk) => {
    buffer += chunk.replace(/\r\n/g, "\n");
    let idx;
    // SSE blocks are separated by a blank line.
    while ((idx = buffer.indexOf("\n\n")) >= 0) {
      const block = buffer.slice(0, idx);
      buffer = buffer.slice(idx + 2);
      for (const line of block.split("\n")) {
        if (!line.startsWith("data:")) continue;
        const payload = line.slice(5).trim();
        if (payload && payload !== "[DONE]") handle(payload);
      }
    }
  };

  const reader = upstreamRes.body.getReader();
  for (;;) {
    const { done, value } = await reader.read();
    if (done) break;
    feed(decoder.decode(value, { stream: true }));
  }

  if (failure) emit({ type: "error", message: failure });

  if (calls.length) {
    emit({
      type: "tool_calls",
      calls: calls.map((c) => ({
        call_id: c.call_id || `call_${Math.random().toString(36).slice(2, 10)}`,
        name: c.name,
        arguments: c.arguments || "{}",
        // open_url / copy_to_clipboard / insert_text all run locally and
        // finish before the turn does, so the client does not need another
        // round just to hand the result back.
        async: true,
      })),
    });
  }

  emit({
    type: "done",
    input: usage?.input_tokens ?? 0,
    last_input: usage?.input_tokens ?? 0,
    output: usage?.output_tokens ?? 0,
    cost: 0,
    keep_reasoning: false,
    message_recorded: false,
    message_refunded: false,
    usage: {
      input_tokens: usage?.input_tokens ?? 0,
      output_tokens: usage?.output_tokens ?? 0,
      reasoning_tokens: usage?.output_tokens_details?.reasoning_tokens ?? 0,
      model: meta.model,
      calls: calls.map((c) => c.name),
      text_chars: textSeen,
    },
  });
}

function finishBrain(emit, result, model) {
  if (result.calls.length) {
    emit({
      type: "tool_calls",
      calls: result.calls.map((c) => ({
        call_id: c.call_id,
        name: c.name,
        arguments: c.arguments || "{}",
        async: true,
      })),
    });
  }
  emit({
    type: "done",
    input: result.usage.input_tokens,
    last_input: result.usage.input_tokens,
    output: result.usage.output_tokens,
    cost: 0,
    keep_reasoning: false,
    message_recorded: false,
    message_refunded: false,
    usage: {
      input_tokens: result.usage.input_tokens,
      output_tokens: result.usage.output_tokens,
      reasoning_tokens: result.usage.reasoning_tokens,
      model,
      calls: result.calls.map((c) => c.name),
      text_chars: result.textSeen,
    },
  });
}

/** Provider+model pairs that already rejected image input this run. */
const imageRejections = new Map();

/** Drop OpenAI-only fields some providers reject (e.g. Gemini). */
function finalizeProviderBody(body, provider, lane) {
  if (!body || typeof body !== "object") return body;
  if (provider.streamOptions === false) delete body.stream_options;
  for (const field of provider.stripFields ?? []) delete body[field];
  return applyThinking(body, provider, lane);
}

function encodeBrainRequest(built, mode, provider) {
  if (mode === "responses") return built.request;
  const chat = responsesToChat(built.request);
  return finalizeProviderBody(mode === "chat" ? chat : chatToMessages(chat), provider, cfg.brain);
}

async function handleBrainTurn(req, res) {
  const controller = new AbortController();
  const timer = setTimeout(() => controller.abort(), 180_000);
  res.on("close", () => controller.abort());

  let provider;
  try {
    provider = resolveProvider(cfg, cfg.brain);
  } catch (err) {
    log("ERROR", "brain: bad provider config", err.message);
    sendJson(res, 500, { error: err.message });
    return;
  }

  let body;
  try {
    const raw = await readBody(req);
    body = JSON.parse(raw.toString("utf8") || "{}");
    if (cfg.brain.dumpDir) {
      try {
        mkdirSync(cfg.brain.dumpDir, { recursive: true });
        writeFileSync(path.join(cfg.brain.dumpDir, `turn-${Date.now()}.json`), raw);
      } catch (err) {
        log("WARN", "brain dump failed", err.message);
      }
    }
  } catch (err) {
    log("ERROR", "brain: unreadable body", err.message);
    sendJson(res, 400, { error: "unreadable body" });
    return;
  }

  const mode = routeMode(provider, "responses");
  const laneKey = `${provider.name}:${cfg.brain.model}`;
  let images = cfg.brain.screenshots !== false && !imageRejections.has(laneKey);

  let built;
  try {
    built = buildBrainRequest(body, cfg, { images });
  } catch (err) {
    log("ERROR", "brain: could not build request", err.message);
    sendJson(res, 500, { error: err.message });
    return;
  }

  const words = String(body.turn?.words ?? "");
  log(
    "INFO",
    "brain turn",
    `provider=${provider.name} api=${mode} model=${cfg.brain.model} images=${images ? "on" : "off"} words="${words.slice(0, 60)}"`,
  );

  const sessionHeader =
    provider.name === "opencode" ? { "x-opencode-session": `clicky-${body.session_id || "session"}` } : {};
  const send = () => postProvider(provider, mode, encodeBrainRequest(built, mode, provider), controller.signal, sessionHeader);

  let upstream;
  try {
    upstream = await send();
  } catch (err) {
    clearTimeout(timer);
    log("ERROR", "brain: provider unreachable", err.message);
    if (!speakFailure(res, "the model endpoint did not respond", err.message)) {
      sendJson(res, 502, { error: `provider unreachable: ${err.message}` });
    }
    return;
  }

  if (!upstream.ok) {
    let detail = await upstream.text().catch(() => "");
    if (images && upstream.status >= 400 && upstream.status < 500 && /image|vision|modalit|attachment/i.test(detail)) {
      imageRejections.set(laneKey, detail.slice(0, 300).replace(/\s+/g, " "));
      images = false;
      log("WARN", "brain: provider rejected the screenshot; retrying without images", detail.slice(0, 200));
      try {
        built = buildBrainRequest(body, cfg, { images: false });
        upstream = await send();
      } catch (err) {
        clearTimeout(timer);
        log("ERROR", "brain: retry without images failed", err.message);
        if (!speakFailure(res, "the model endpoint did not respond", err.message)) {
          sendJson(res, 502, { error: `provider unreachable: ${err.message}` });
        }
        return;
      }
      if (!upstream.ok) detail = await upstream.text().catch(() => "");
    }
    if (!upstream.ok) {
      clearTimeout(timer);
      log("ERROR", `brain: provider ${upstream.status}`, detail.slice(0, 400));
      const busy = upstream.status === 429 || upstream.status >= 500;
      const reason = busy
        ? `the model provider is busy (HTTP ${upstream.status})`
        : `the model provider rejected the request (HTTP ${upstream.status})`;
      if (!speakFailure(res, reason, detail)) {
        sendJson(res, 502, { error: `provider ${upstream.status}: ${detail.slice(0, 300)}` });
      }
      return;
    }
  }

  const emit = openSse(res);
  emit({ type: "meta", model: cfg.brain.model, user_layer: "gateway", provider: provider.name, images });

  try {
    if (mode === "responses") {
      await relayBrainStream(upstream, emit, { model: cfg.brain.model });
    } else {
      const chunks = mode === "chat" ? sseJson(upstream.body) : messagesStreamToChat(upstream.body);
      const result = await chatChunksToBrain(chunks, emit);
      finishBrain(emit, result, cfg.brain.model);
    }
  } catch (err) {
    if (err.name !== "AbortError") log("ERROR", "brain: stream failed", err.message);
    emit({ type: "error", message: err.message });
    if (mode !== "responses") {
      emit({ type: "done", input: 0, last_input: 0, output: 0, cost: 0, usage: {} });
    }
  } finally {
    clearTimeout(timer);
    if (!res.writableEnded) res.end();
  }
}

// ---------------------------------------------------------------- agent lane

/**
 * clicky-harness.exe calls ${workerBase}/agent/openai/v1/responses and
 * ${workerBase}/agent/anthropic/v1/messages with its own bodies. When the
 * provider speaks that protocol natively we only rewrite the model; otherwise
 * we translate through Chat Completions.
 */
async function handleAgent(req, res, kind) {
  let provider;
  try {
    provider = resolveProvider(cfg, cfg.agent);
  } catch (err) {
    sendJson(res, 500, { error: err.message });
    return;
  }

  const raw = await readBody(req);
  let body = {};
  try {
    body = raw.length ? JSON.parse(raw.toString("utf8")) : {};
  } catch {
    sendJson(res, 400, { error: "unreadable body" });
    return;
  }

  const mode = routeMode(provider, kind);
  const stream = body.stream === true;

  let providerBody;
  if (mode === kind) {
    providerBody = body;
    if (!cfg.agent.preserveRequestedModel) providerBody.model = cfg.agent.model;
    if (kind === "responses") for (const field of cfg.agent.strip ?? []) delete providerBody[field];
    else if (providerBody.max_tokens == null) providerBody.max_tokens = cfg.agent.maxTokens;
  } else {
    const chat = kind === "responses" ? responsesToChat(body) : messagesToChat(body);
    if (!cfg.agent.preserveRequestedModel) chat.model = cfg.agent.model;
    if (chat.max_tokens == null) chat.max_tokens = cfg.agent.maxTokens;
    chat.stream = stream;
    providerBody = mode === "chat" ? chat : mode === "responses" ? chatToResponses(chat) : chatToMessages(chat);
  }

  providerBody = finalizeProviderBody(providerBody, provider, cfg.agent);

  log("INFO", `agent ${kind}`, `provider=${provider.name} api=${mode} model=${providerBody.model} bytes=${raw.length}`);

  let upstream;
  try {
    upstream = await postProvider(
      provider,
      mode,
      providerBody,
      undefined,
      provider.name === "opencode"
        ? { "x-opencode-session": `clicky-${body.session_id || body.job?.id || "agent"}` }
        : {},
    );
  } catch (err) {
    log("ERROR", `agent ${kind}: provider unreachable`, err.message);
    sendJson(res, 502, { error: `provider unreachable: ${err.message}` });
    return;
  }

  if (!upstream.ok) {
    const detail = await upstream.text().catch(() => "");
    log("ERROR", `agent ${kind}: provider ${upstream.status}`, detail.slice(0, 400));
    sendJson(res, upstream.status, { error: `provider ${upstream.status}: ${detail.slice(0, 500)}` });
    return;
  }

  // ---- non-streaming: translate JSON, keep the client's wire shape
  if (!stream) {
    let json;
    try {
      json = await upstream.json();
    } catch (err) {
      sendJson(res, 502, { error: `provider returned non-JSON: ${err.message}` });
      return;
    }
    const chatJson =
      mode === "chat" ? json : mode === "responses" ? responsesJsonToChat(json) : messagesJsonToChat(json);
    if (kind === "responses") sendJson(res, 200, chatJsonToResponses(chatJson, cfg.agent.model));
    else sendJson(res, 200, chatJsonToMessages(chatJson, cfg.agent.model));
    return;
  }

  // ---- streaming: native passthrough, or translate chunks
  if (mode === kind) {
    const drop = new Set(["content-encoding", "content-length", "transfer-encoding", "connection", "set-cookie"]);
    const outHeaders = {};
    upstream.headers.forEach((value, key) => {
      if (!drop.has(key.toLowerCase())) outHeaders[key] = value;
    });
    res.writeHead(upstream.status, outHeaders);
    if (!upstream.body) {
      res.end();
      return;
    }
    const reader = upstream.body.getReader();
    res.on("close", () => {
      try {
        reader.cancel();
      } catch {
        /* already gone */
      }
    });
    try {
      for (;;) {
        const { done, value } = await reader.read();
        if (done) break;
        if (!res.write(Buffer.from(value))) await new Promise((resolve) => res.once("drain", resolve));
      }
    } catch (err) {
      log("WARN", `agent ${kind}: relay stopped`, err.message);
    } finally {
      res.end();
    }
    return;
  }

  res.writeHead(200, {
    "content-type": "text/event-stream; charset=utf-8",
    "cache-control": "no-cache, no-transform",
    connection: "keep-alive",
    "x-accel-buffering": "no",
  });
  if (typeof res.flushHeaders === "function") res.flushHeaders();
  const emit = sseEventWriter(res);

  try {
    const chunks =
      mode === "chat" ? sseJson(upstream.body) : mode === "responses" ? responsesStreamToChat(upstream.body) : messagesStreamToChat(upstream.body);
    if (kind === "responses") await chatChunksToResponses(chunks, emit, cfg.agent.model);
    else await chatChunksToMessages(chunks, emit, cfg.agent.model);
  } catch (err) {
    log("ERROR", `agent ${kind}: translation failed`, err.message);
    if (!res.writableEnded) emit({ type: "error", error: { type: "api_error", message: err.message } });
  } finally {
    if (!res.writableEnded) res.end();
  }
}

// ---------------------------------------------------------------- plan + misc

async function handlePlan(req, res) {
  const upstream = await proxyFetch(req, "/me/plan", "GET").catch((err) => {
    log("WARN", "plan: upstream failed", err.message);
    return null;
  });

  let plan = null;
  if (upstream && upstream.ok) {
    plan = await upstream.json().catch(() => null);
  }
  if (!plan || typeof plan !== "object") {
    // Offline / signed-out: say enough for the client to keep working.
    plan = {
      plan: "free",
      usage: {
        messages: { used: 0, cap: 12 },
        agents: { used: 0, cap: 12 },
        dictation: { used: 0, cap: 4000 },
      },
      billing_enforcement: "on",
    };
  }

  if (cfg.proxy.planEnforcement === "off") {
    if (plan.plan === "free" && plan.billing_enforcement === "on") {
      // BillingStateService.HasHitLocalMessagesLimit() only gates when the
      // plan is free AND enforcement is "on". Inference never reaches
      // HeyClicky's servers on this path, so the local cap does not apply.
      plan.billing_enforcement = "off";
    }
  }

  sendJson(res, 200, plan, { "x-clicky-gateway": "plan" });
}

function modelPolicy() {
  return {
    codexAgentLane: {
      model: cfg.agent.model,
      reasoningEffort: cfg.agent.effort || "medium",
    },
  };
}

// ---------------------------------------------------------------- passthrough

function proxyFetch(req, pathname, method = req.method, body) {
  const base = cfg.proxy.baseUrl.replace(/\/+$/, "");
  const target = new URL(base + pathname);
  const headers = {};
  for (const [k, v] of Object.entries(req.headers)) {
    if (["host", "content-length", "connection", "accept-encoding", "transfer-encoding"].includes(k)) continue;
    if (v !== undefined) headers[k] = v;
  }
  headers["accept-encoding"] = "identity";
  return fetch(target, { method, headers, body, redirect: "manual" });
}

async function handlePassthrough(req, res, url) {
  if (!cfg.proxy.enabled) {
    sendJson(res, 404, { error: "not proxied by clicky-gateway", path: url.pathname });
    return;
  }
  const body = ["GET", "HEAD", "OPTIONS"].includes(req.method) ? undefined : await readBody(req);
  let upstream;
  try {
    upstream = await proxyFetch(req, url.pathname + url.search, req.method, body);
  } catch (err) {
    log("WARN", `proxy ${url.pathname} failed`, err.message);
    sendJson(res, 502, { error: `proxy failed: ${err.message}` });
    return;
  }

  const drop = new Set(["content-encoding", "content-length", "transfer-encoding", "connection", "set-cookie"]);
  const headers = {};
  upstream.headers.forEach((value, key) => {
    if (!drop.has(key.toLowerCase())) headers[key] = value;
  });
  headers["x-clicky-gateway"] = "proxy";
  res.writeHead(upstream.status, headers);

  if (!upstream.body) {
    res.end();
    return;
  }
  const reader = upstream.body.getReader();
  try {
    for (;;) {
      const { done, value } = await reader.read();
      if (done) break;
      if (!res.write(Buffer.from(value))) await new Promise((r) => res.once("drain", r));
    }
  } catch (err) {
    if (cfg.log.verbose) log("WARN", `proxy ${url.pathname} relay stopped`, err.message);
  } finally {
    res.end();
  }
}

// ---------------------------------------------------------------- router

const server = http.createServer(async (req, res) => {
  const url = new URL(req.url, `http://${req.headers.host || "127.0.0.1"}`);
  const p = url.pathname;
  if (cfg.log.verbose) log("DEBUG", `${req.method} ${p}`, logHeaders(req.headers));

  try {
    if (p === "/__clicky/health") {
      const brain = laneInfo("brain");
      const agent = laneInfo("agent");
      sendJson(res, 200, {
        ok: true,
        gateway: "clicky-gateway",
        uptimeSeconds: Math.round((Date.now() - startedAt) / 1000),
        brainModel: cfg.brain.model,
        agentModel: cfg.agent.model,
        brain,
        agent,
        screenshots: {
          enabled: cfg.brain.screenshots !== false,
          rejected: [...imageRejections.entries()].map(([lane, reason]) => ({ lane, reason })),
        },
        planEnforcement: cfg.proxy.planEnforcement,
      });
      return;
    }

    if (p === "/__clicky/status") {
      sendJson(res, 200, {
        ok: true,
        config: {
          port: cfg.port,
          brain: cfg.brain,
          agent: { ...cfg.agent, headers: undefined },
          lane: { brain: laneInfo("brain"), agent: laneInfo("agent") },
          screenshots: cfg.brain.screenshots !== false,
          proxy: cfg.proxy,
        },
      });
      return;
    }

    if (p === "/brain/turn" && req.method === "POST") {
      await handleBrainTurn(req, res);
      return;
    }

    if (p === "/brain/compact") {
      // No server-side thread compaction on this path; the client just keeps
      // its own history (BrainSession trims screenshots on its own).
      sendJson(res, 200, { reason: "gateway: compaction disabled" });
      return;
    }

    if (p === "/brain/session-end" || p === "/brain/prewarm") {
      sendJson(res, 200, {});
      return;
    }

    if (p === "/me/plan" && req.method === "GET") {
      await handlePlan(req, res);
      return;
    }

    if (p === "/runtime/model-policy" && req.method === "GET") {
      sendJson(res, 200, modelPolicy());
      return;
    }

    if (p === "/agent/openai/v1/responses" && req.method === "POST") {
      await handleAgent(req, res, "responses");
      return;
    }

    if (p === "/agent/anthropic/v1/messages" && req.method === "POST") {
      await handleAgent(req, res, "messages");
      return;
    }

    await handlePassthrough(req, res, url);
  } catch (err) {
    log("ERROR", `${req.method} ${p} failed`, err.stack || err.message);
    if (!res.headersSent) sendJson(res, 500, { error: err.message });
    else if (!res.writableEnded) res.end();
  }
});

server.listen(cfg.port, cfg.host, () => {
  const brain = laneInfo("brain");
  const agent = laneInfo("agent");
  log("INFO", `clicky-gateway listening on http://${cfg.host}:${cfg.port}`);
  log("INFO", `brain -> ${brain.provider} (${brain.api})`, `model=${cfg.brain.model}`);
  log("INFO", `agent -> ${agent.provider} (${agent.api})`, `model=${cfg.agent.model}`);
  log("INFO", "everything else -> " + cfg.proxy.baseUrl);
  for (const lane of [brain, agent]) {
    if (lane.error) log("WARN", `lane ${lane.provider}: ${lane.error}`);
    else if (!lane.keyConfigured) log("WARN", `no API key for ${lane.provider}: model calls will fail until one is set`);
  }
  if (process.env.CLICKY_GATEWAY_READY_FILE) {
    try {
      writeFileSync(process.env.CLICKY_GATEWAY_READY_FILE, String(process.pid));
    } catch {
      /* best effort */
    }
  }
});

for (const signal of ["SIGINT", "SIGTERM"]) {
  process.on(signal, () => {
    log("INFO", `received ${signal}, shutting down`);
    server.close(() => process.exit(0));
    setTimeout(() => process.exit(0), 1500);
  });
}
