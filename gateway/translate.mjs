// Protocol translation between what HeyClicky speaks and what a provider speaks.
//
// HeyClicky needs two client protocols:
//   - /brain/turn                      custom SSE wrapper (implemented here)
//   - /agent/openai/v1/responses       OpenAI Responses (clicky-harness.exe)
//   - /agent/anthropic/v1/messages     Anthropic Messages (clicky-harness.exe)
//
// Providers speak one or more of: Responses, Chat Completions, Messages.
// When a provider has the native endpoint we pass the request straight through.
// Otherwise we translate through Chat Completions as the hub:
//
//   Responses --responsesToChat--> Chat --chatToMessages--> Messages
//   Messages  --messagesToChat---> Chat --chatToResponses-> Responses
//
//   ...and the matching stream converters bring the answer back.

/** Yield parsed `data:` JSON objects from an SSE stream. */
export async function* sseJson(body) {
  const decoder = new TextDecoder();
  let buffer = "";
  const reader = body.getReader();
  for (;;) {
    const { done, value } = await reader.read();
    if (done) break;
    buffer = (buffer + decoder.decode(value, { stream: true })).replace(/\r\n/g, "\n");
    let idx;
    while ((idx = buffer.indexOf("\n\n")) >= 0) {
      const block = buffer.slice(0, idx);
      buffer = buffer.slice(idx + 2);
      for (const line of block.split("\n")) {
        if (!line.startsWith("data:")) continue;
        const payload = line.slice(5).trim();
        if (!payload || payload === "[DONE]") continue;
        try {
          yield JSON.parse(payload);
        } catch {
          /* ignore malformed frame */
        }
      }
    }
  }
}

const asText = (v) => (typeof v === "string" ? v : v == null ? "" : JSON.stringify(v));
const newId = (prefix) => `${prefix}_${Math.random().toString(36).slice(2, 12)}`;

function safeJson(value, fallback = {}) {
  if (value && typeof value === "object") return value;
  try {
    const parsed = JSON.parse(value || "{}");
    return parsed && typeof parsed === "object" ? parsed : fallback;
  } catch {
    return fallback;
  }
}

/** Responses content blocks -> chat content blocks. */
function contentBlocks(content) {
  if (typeof content === "string") return [{ type: "text", text: content }];
  if (!Array.isArray(content)) return [];
  const out = [];
  for (const block of content) {
    if (!block || typeof block !== "object") continue;
    switch (block.type) {
      case "input_text":
      case "output_text":
      case "text":
        out.push({ type: "text", text: block.text ?? "" });
        break;
      case "input_image":
      case "image_url": {
        const url = typeof block.image_url === "string" ? block.image_url : block.image_url?.url ?? block.url;
        if (url) out.push({ type: "image_url", image_url: { url } });
        break;
      }
      default:
        break;
    }
  }
  return out;
}

/** Chat content -> Anthropic content blocks. */
function anthropicBlocks(content) {
  if (typeof content === "string") return content ? [{ type: "text", text: content }] : [];
  if (!Array.isArray(content)) return [];
  const out = [];
  for (const block of content) {
    if (!block || typeof block !== "object") continue;
    if (block.type === "text") out.push({ type: "text", text: block.text ?? "" });
    else if (block.type === "image_url") {
      const url = typeof block.image_url === "string" ? block.image_url : block.image_url?.url;
      if (!url) continue;
      const dataUrl = /^data:([^;]+);base64,(.*)$/s.exec(url);
      if (dataUrl) out.push({ type: "image", source: { type: "base64", media_type: dataUrl[1], data: dataUrl[2] } });
      else out.push({ type: "image", source: { type: "url", url } });
    }
  }
  return out;
}

/** Chat content -> Responses content blocks. */
function responsesBlocks(content, textType) {
  if (typeof content === "string") return content ? [{ type: textType, text: content }] : [];
  if (!Array.isArray(content)) return [];
  const out = [];
  for (const block of content) {
    if (!block || typeof block !== "object") continue;
    if (block.type === "text") out.push({ type: textType, text: block.text ?? "" });
    else if (block.type === "image_url") {
      const url = typeof block.image_url === "string" ? block.image_url : block.image_url?.url;
      if (url) out.push({ type: "input_image", image_url: url });
    }
  }
  return out;
}

function chatContent(blocks) {
  if (blocks.length === 0) return "";
  if (blocks.every((b) => b.type === "text")) return blocks.map((b) => b.text).join("");
  return blocks;
}

// ------------------------------------------------------- requests into chat

/** Responses request body -> Chat Completions request body. */
export function responsesToChat(body) {
  const messages = [];
  if (body.instructions) messages.push({ role: "system", content: body.instructions });

  let pendingCalls = [];
  const flushCalls = () => {
    if (!pendingCalls.length) return;
    messages.push({ role: "assistant", content: null, tool_calls: pendingCalls });
    pendingCalls = [];
  };

  for (const item of body.input ?? []) {
    if (typeof item === "string") {
      flushCalls();
      messages.push({ role: "user", content: item });
      continue;
    }
    if (!item || typeof item !== "object") continue;

    if (!item.type || item.type === "message") {
      const role = item.role === "developer" ? "system" : item.role || "user";
      const blocks = contentBlocks(item.content ?? item.text);
      flushCalls();
      messages.push({ role, content: chatContent(blocks) });
      continue;
    }

    if (item.type === "function_call") {
      pendingCalls.push({
        id: item.call_id || item.id || newId("call"),
        type: "function",
        function: { name: item.name, arguments: item.arguments || "{}" },
      });
      continue;
    }

    if (item.type === "function_call_output") {
      flushCalls();
      messages.push({ role: "tool", tool_call_id: item.call_id || "", content: asText(item.output) });
      continue;
    }

    // reasoning, computer_call, computer_call_output and anything else the
    // chat API has no slot for are dropped.
  }
  flushCalls();

  const req = { model: body.model, messages, stream: Boolean(body.stream) };
  if (req.stream) req.stream_options = { include_usage: true };
  if (body.max_output_tokens) req.max_tokens = body.max_output_tokens;
  if (body.temperature != null) req.temperature = body.temperature;
  if (body.parallel_tool_calls != null) req.parallel_tool_calls = body.parallel_tool_calls;
  if (Array.isArray(body.tools) && body.tools.length) {
    req.tools = body.tools
      .filter((t) => t && (t.type === "function" || t.name))
      .map((t) => ({
        type: "function",
        function: {
          name: t.name ?? t.function?.name,
          description: t.description ?? t.function?.description,
          parameters: t.parameters ?? t.function?.parameters ?? { type: "object", properties: {} },
        },
      }));
    if (body.tool_choice && body.tool_choice !== "auto") req.tool_choice = body.tool_choice;
  }
  return req;
}

/** Anthropic Messages request body -> Chat Completions request body. */
export function messagesToChat(body) {
  const messages = [];
  const system =
    typeof body.system === "string" ? body.system : (body.system ?? []).map((b) => b.text).join("\n");
  if (system) messages.push({ role: "system", content: system });

  for (const msg of body.messages ?? []) {
    const blocks = Array.isArray(msg.content) ? msg.content : [{ type: "text", text: msg.content ?? "" }];
    if (msg.role === "assistant") {
      const text = blocks.filter((b) => b.type === "text").map((b) => b.text).join("");
      const toolCalls = blocks
        .filter((b) => b.type === "tool_use")
        .map((b) => ({
          id: b.id,
          type: "function",
          function: { name: b.name, arguments: JSON.stringify(b.input ?? {}) },
        }));
      messages.push({ role: "assistant", content: text || null, ...(toolCalls.length ? { tool_calls: toolCalls } : {}) });
      continue;
    }
    const parts = [];
    for (const b of blocks) {
      if (b.type === "text") parts.push({ type: "text", text: b.text });
      else if (b.type === "image") {
        const src = b.source ?? {};
        const url = src.type === "base64" ? `data:${src.media_type};base64,${src.data}` : src.url;
        if (url) parts.push({ type: "image_url", image_url: { url } });
      } else if (b.type === "tool_result") {
        messages.push({
          role: "tool",
          tool_call_id: b.tool_use_id,
          content: typeof b.content === "string" ? b.content : JSON.stringify(b.content ?? ""),
        });
      }
    }
    if (parts.length) messages.push({ role: "user", content: chatContent(parts) });
  }

  const req = { model: body.model, messages, stream: Boolean(body.stream) };
  if (req.stream) req.stream_options = { include_usage: true };
  if (body.max_tokens) req.max_tokens = body.max_tokens;
  if (body.temperature != null) req.temperature = body.temperature;
  if (Array.isArray(body.tools) && body.tools.length) {
    req.tools = body.tools.map((t) => ({
      type: "function",
      function: { name: t.name, description: t.description, parameters: t.input_schema ?? { type: "object", properties: {} } },
    }));
  }
  return req;
}

// ------------------------------------------------------ requests out of chat

/** Chat Completions request body -> Responses request body. */
export function chatToResponses(chat) {
  const instructions = [];
  const input = [];

  for (const msg of chat.messages ?? []) {
    const role = msg.role || "user";
    if (role === "system" || role === "developer") {
      const text = typeof msg.content === "string" ? msg.content : asText(msg.content);
      if (text) instructions.push(text);
      continue;
    }
    if (role === "tool") {
      input.push({
        type: "function_call_output",
        call_id: msg.tool_call_id || "",
        output: asText(msg.content),
      });
      continue;
    }
    if (role === "assistant") {
      const blocks = responsesBlocks(msg.content, "output_text");
      if (blocks.length) input.push({ type: "message", role: "assistant", content: blocks });
      for (const tc of msg.tool_calls ?? []) {
        input.push({
          type: "function_call",
          call_id: tc.id || newId("call"),
          name: tc.function?.name,
          arguments: tc.function?.arguments || "{}",
        });
      }
      continue;
    }
    input.push({ type: "message", role: "user", content: responsesBlocks(msg.content, "input_text") });
  }

  const req = { model: chat.model, input, stream: Boolean(chat.stream) };
  if (instructions.length) req.instructions = instructions.join("\n\n");
  if (chat.max_tokens ?? chat.max_completion_tokens) req.max_output_tokens = chat.max_tokens ?? chat.max_completion_tokens;
  if (chat.temperature != null) req.temperature = chat.temperature;
  if (chat.parallel_tool_calls != null) req.parallel_tool_calls = chat.parallel_tool_calls;
  if (Array.isArray(chat.tools) && chat.tools.length) {
    req.tools = chat.tools
      .filter((t) => t?.function?.name || t?.name)
      .map((t) => ({
        type: "function",
        name: t.function?.name ?? t.name,
        description: t.function?.description ?? t.description ?? "",
        parameters: t.function?.parameters ?? t.parameters ?? { type: "object", properties: {} },
        strict: false,
      }));
    if (chat.tool_choice) req.tool_choice = chat.tool_choice;
  }
  return req;
}

/** Chat Completions request body -> Anthropic Messages request body. */
export function chatToMessages(chat) {
  const system = [];
  const messages = [];
  let toolResults = [];

  const flushResults = () => {
    if (!toolResults.length) return;
    messages.push({ role: "user", content: toolResults });
    toolResults = [];
  };

  for (const msg of chat.messages ?? []) {
    const role = msg.role || "user";
    if (role === "system" || role === "developer") {
      const text = typeof msg.content === "string" ? msg.content : asText(msg.content);
      if (text) system.push(text);
      continue;
    }
    if (role === "tool") {
      toolResults.push({
        type: "tool_result",
        tool_use_id: msg.tool_call_id || "",
        content: asText(msg.content),
      });
      continue;
    }
    flushResults();

    if (role === "assistant") {
      const blocks = [];
      if (msg.content) blocks.push(...anthropicBlocks(msg.content));
      for (const tc of msg.tool_calls ?? []) {
        blocks.push({
          type: "tool_use",
          id: tc.id || newId("toolu"),
          name: tc.function?.name,
          input: safeJson(tc.function?.arguments),
        });
      }
      messages.push({ role: "assistant", content: blocks.length ? blocks : [{ type: "text", text: "" }] });
      continue;
    }

    const blocks = anthropicBlocks(msg.content);
    messages.push({ role: "user", content: blocks.length ? blocks : [{ type: "text", text: "" }] });
  }
  flushResults();

  const req = {
    model: chat.model,
    messages,
    max_tokens: chat.max_tokens ?? chat.max_completion_tokens ?? 8192,
    stream: Boolean(chat.stream),
  };
  if (system.length) req.system = system.join("\n\n");
  if (chat.temperature != null) req.temperature = chat.temperature;
  if (Array.isArray(chat.tools) && chat.tools.length) {
    req.tools = chat.tools
      .filter((t) => t?.function?.name || t?.name)
      .map((t) => ({
        name: t.function?.name ?? t.name,
        description: t.function?.description ?? t.description ?? "",
        input_schema: t.function?.parameters ?? t.parameters ?? { type: "object", properties: {} },
      }));
    if (chat.tool_choice === "auto") req.tool_choice = { type: "auto" };
    else if (chat.tool_choice) req.tool_choice = { type: "any" };
  }
  return req;
}

// ------------------------------------------------------------ stream helpers

/** Normalise the streamed tool-call deltas Chat Completions sends. */
function accumulateToolCalls(map, deltas) {
  for (const tc of deltas ?? []) {
    const index = tc.index ?? 0;
    const cur = map.get(index) ?? { call_id: "", name: "", arguments: "" };
    if (tc.id) cur.call_id = tc.id;
    if (tc.function?.name) cur.name = (cur.name || "") + tc.function.name;
    if (tc.function?.arguments) cur.arguments += tc.function.arguments;
    map.set(index, cur);
  }
}

function orderedCalls(map) {
  return [...map.entries()]
    .sort((a, b) => a[0] - b[0])
    .map(([, c]) => ({
      call_id: c.call_id || newId("call"),
      name: c.name,
      arguments: c.arguments || "{}",
    }))
    .filter((c) => c.name);
}

function usageOf(chatUsage) {
  return {
    input_tokens: chatUsage?.prompt_tokens ?? 0,
    output_tokens: chatUsage?.completion_tokens ?? 0,
    reasoning_tokens: chatUsage?.completion_tokens_details?.reasoning_tokens ?? 0,
  };
}

/** Responses SSE -> Chat Completions-style chunk objects. */
export async function* responsesStreamToChat(body) {
  const calls = new Map();

  for await (const evt of sseJson(body)) {
    switch (evt.type) {
      case "response.output_text.delta":
        if (evt.delta) yield { choices: [{ index: 0, delta: { content: evt.delta } }] };
        break;

      case "response.output_item.added":
        if (evt.item?.type === "function_call") {
          const index = evt.output_index ?? calls.size;
          const id = evt.item.call_id || evt.item.id || newId("call");
          const name = evt.item.name || "";
          calls.set(index, { id, name, arguments: "" });
          yield {
            choices: [
              { index: 0, delta: { tool_calls: [{ index, id, type: "function", function: { name, arguments: "" } }] } },
            ],
          };
        }
        break;

      case "response.function_call_arguments.delta": {
        const index = evt.output_index ?? 0;
        const call = calls.get(index);
        if (call && evt.delta) {
          call.arguments += evt.delta;
          yield {
            choices: [
              { index: 0, delta: { tool_calls: [{ index, function: { arguments: evt.delta } }] } },
            ],
          };
        }
        break;
      }

      case "response.completed": {
        const u = evt.response?.usage;
        yield {
          choices: [{ index: 0, delta: {}, finish_reason: "stop" }],
          ...(u
            ? { usage: { prompt_tokens: u.input_tokens ?? 0, completion_tokens: u.output_tokens ?? 0 } }
            : {}),
        };
        break;
      }

      case "response.failed":
      case "response.incomplete":
        throw new Error(evt.response?.error?.message || "the model stopped early");

      case "error":
        throw new Error(evt.message || evt.error?.message || "upstream error");

      default:
        break;
    }
  }
}

/** Anthropic Messages SSE -> Chat Completions-style chunk objects. */
export async function* messagesStreamToChat(body) {
  const toolIndex = new Map();

  for await (const evt of sseJson(body)) {
    switch (evt.type) {
      case "message_start": {
        const u = evt.message?.usage;
        if (u) yield { usage: { prompt_tokens: u.input_tokens ?? 0, completion_tokens: 0 } };
        break;
      }

      case "content_block_start": {
        const block = evt.content_block ?? {};
        if (block.type === "tool_use") {
          const index = toolIndex.size;
          toolIndex.set(evt.index, index);
          yield {
            choices: [
              {
                index: 0,
                delta: {
                  tool_calls: [{ index, id: block.id, type: "function", function: { name: block.name, arguments: "" } }],
                },
              },
            ],
          };
        }
        break;
      }

      case "content_block_delta": {
        const d = evt.delta ?? {};
        if (d.type === "text_delta" && d.text) {
          yield { choices: [{ index: 0, delta: { content: d.text } }] };
        } else if (d.type === "input_json_delta" && d.partial_json) {
          const index = toolIndex.get(evt.index);
          if (index != null) {
            yield {
              choices: [{ index: 0, delta: { tool_calls: [{ index, function: { arguments: d.partial_json } }] } }],
            };
          }
        }
        break;
      }

      case "message_delta": {
        const u = evt.usage;
        yield {
          choices: [
            {
              index: 0,
              delta: {},
              finish_reason: evt.delta?.stop_reason === "tool_use" ? "tool_calls" : "stop",
            },
          ],
          ...(u ? { usage: { completion_tokens: u.output_tokens ?? 0 } } : {}),
        };
        break;
      }

      case "error":
        throw new Error(evt.error?.message || "upstream error");

      default:
        break;
    }
  }
}

/** Non-streaming Responses JSON -> Chat Completions JSON. */
export function responsesJsonToChat(json) {
  const text = [];
  const toolCalls = [];
  for (const item of json.output ?? []) {
    if (item.type === "message") {
      for (const block of item.content ?? []) {
        if (block.type === "output_text" || block.type === "text") text.push(block.text ?? "");
      }
    } else if (item.type === "function_call") {
      toolCalls.push({
        id: item.call_id || item.id || newId("call"),
        type: "function",
        function: { name: item.name, arguments: item.arguments || "{}" },
      });
    }
  }
  const u = json.usage ?? {};
  return {
    id: json.id || newId("chatcmpl"),
    object: "chat.completion",
    model: json.model,
    choices: [
      {
        index: 0,
        finish_reason: toolCalls.length ? "tool_calls" : "stop",
        message: { role: "assistant", content: text.join("") || null, ...(toolCalls.length ? { tool_calls: toolCalls } : {}) },
      },
    ],
    usage: {
      prompt_tokens: u.input_tokens ?? 0,
      completion_tokens: u.output_tokens ?? 0,
    },
  };
}

/** Non-streaming Anthropic Messages JSON -> Chat Completions JSON. */
export function messagesJsonToChat(json) {
  const text = [];
  const toolCalls = [];
  for (const block of json.content ?? []) {
    if (block.type === "text") text.push(block.text ?? "");
    else if (block.type === "tool_use") {
      toolCalls.push({
        id: block.id,
        type: "function",
        function: { name: block.name, arguments: JSON.stringify(block.input ?? {}) },
      });
    }
  }
  const u = json.usage ?? {};
  return {
    id: json.id || newId("chatcmpl"),
    object: "chat.completion",
    model: json.model,
    choices: [
      {
        index: 0,
        finish_reason: json.stop_reason === "tool_use" ? "tool_calls" : "stop",
        message: { role: "assistant", content: text.join("") || null, ...(toolCalls.length ? { tool_calls: toolCalls } : {}) },
      },
    ],
    usage: {
      prompt_tokens: u.input_tokens ?? 0,
      completion_tokens: u.output_tokens ?? 0,
    },
  };
}

// --------------------------------------------------------- out to HeyClicky

/**
 * Chat chunks -> HeyClicky /brain/turn events (text chunks only).
 * Returns the tool calls and usage so the caller can emit tool_calls + done.
 */
export async function chatChunksToBrain(chunks, emit) {
  const calls = new Map();
  let usage = null;
  let textSeen = 0;

  for await (const evt of chunks) {
    if (evt.error) throw new Error(evt.error.message || "upstream error");
    if (evt.usage) usage = { ...(usage || {}), ...evt.usage };
    const choice = evt.choices?.[0];
    if (!choice) continue;
    const delta = choice.delta ?? {};
    if (typeof delta.content === "string" && delta.content.length) {
      textSeen += delta.content.length;
      emit({ type: "text", text: delta.content });
    } else if (Array.isArray(delta.content)) {
      for (const part of delta.content) {
        if (part?.type === "text" && part.text) {
          textSeen += part.text.length;
          emit({ type: "text", text: part.text });
        }
      }
    }
    accumulateToolCalls(calls, delta.tool_calls);
  }

  return { calls: orderedCalls(calls), usage: usageOf(usage), textSeen };
}

/** Chat chunks -> OpenAI Responses SSE, for clicky-harness.exe. */
export async function chatChunksToResponses(chunks, emit, model) {
  const id = newId("resp");
  const createdAt = Math.floor(Date.now() / 1000);
  const messageId = newId("msg");
  const calls = new Map();
  let text = "";
  let messageOpen = false;
  let messageIndex = 0;
  let usage = null;
  let seq = 0;
  const output = [];

  const send = (event) => {
    event.sequence_number = seq++;
    emit(event);
  };

  send({
    type: "response.created",
    response: { id, object: "response", created_at: createdAt, status: "in_progress", model },
  });

  const openMessage = () => {
    if (messageOpen) return;
    messageOpen = true;
    messageIndex = output.length;
    send({
      type: "response.output_item.added",
      output_index: messageIndex,
      item: { type: "message", id: messageId, status: "in_progress", role: "assistant", content: [] },
    });
  };

  const closeMessage = () => {
    if (!messageOpen) return;
    messageOpen = false;
    send({
      type: "response.output_item.done",
      output_index: messageIndex,
      item: {
        type: "message",
        id: messageId,
        status: "completed",
        role: "assistant",
        content: [{ type: "output_text", text, annotations: [] }],
      },
    });
    output.push({ type: "message", role: "assistant", content: [{ type: "output_text", text }] });
  };

  const started = new Set();
  for await (const evt of chunks) {
    if (evt.error) throw new Error(evt.error.message || "upstream error");
    if (evt.usage) usage = { ...(usage || {}), ...evt.usage };
    const choice = evt.choices?.[0];
    if (!choice) continue;
    const delta = choice.delta ?? {};

    if (typeof delta.content === "string" && delta.content.length) {
      openMessage();
      text += delta.content;
      send({
        type: "response.output_text.delta",
        item_id: messageId,
        output_index: messageIndex,
        content_index: 0,
        delta: delta.content,
      });
    }

    for (const tc of delta.tool_calls ?? []) {
      const index = tc.index ?? 0;
      const call = calls.get(index) ?? { call_id: "", name: "", arguments: "" };
      const first = !started.has(index);
      if (first) {
        started.add(index);
        closeMessage();
        call.outputIndex = output.length;
        call.itemId = newId("fc");
      }
      if (tc.id) call.call_id = tc.id;
      if (tc.function?.name) call.name = (call.name || "") + tc.function.name;
      if (first) {
        send({
          type: "response.output_item.added",
          output_index: call.outputIndex,
          item: { type: "function_call", id: call.itemId, call_id: call.call_id, name: call.name, arguments: "" },
        });
      }
      if (tc.function?.arguments) {
        call.arguments += tc.function.arguments;
        send({
          type: "response.function_call_arguments.delta",
          item_id: call.itemId,
          output_index: call.outputIndex,
          delta: tc.function.arguments,
        });
      }
      calls.set(index, call);
    }
  }

  closeMessage();

  for (const [, call] of [...calls.entries()].sort((a, b) => a[0] - b[0])) {
    if (!call.name) continue;
    const callId = call.call_id || newId("call");
    send({
      type: "response.output_item.done",
      output_index: call.outputIndex,
      item: {
        type: "function_call",
        id: call.itemId,
        call_id: callId,
        name: call.name,
        arguments: call.arguments || "{}",
        status: "completed",
      },
    });
    output.push({ type: "function_call", call_id: callId, name: call.name, arguments: call.arguments || "{}" });
  }

  const u = usageOf(usage);
  send({
    type: "response.completed",
    response: {
      id,
      object: "response",
      created_at: createdAt,
      status: "completed",
      model,
      output,
      usage: { ...u, total_tokens: u.input_tokens + u.output_tokens },
    },
  });
}

/**
 * Chat chunks -> Anthropic Messages SSE.
 * Text streams live; tool calls are buffered until their arguments are whole.
 */
export async function chatChunksToMessages(chunks, emit, model) {
  const messageId = newId("msg");
  const calls = new Map();
  let textOpen = false;
  let textIndex = 0;
  let nextIndex = 0;
  let usage = null;
  let stopReason = "end_turn";

  const send = (event) => emit(event);

  send({
    type: "message_start",
    message: {
      id: messageId,
      type: "message",
      role: "assistant",
      content: [],
      model,
      stop_reason: null,
      usage: { input_tokens: 0, output_tokens: 0 },
    },
  });

  const openText = () => {
    if (textOpen) return;
    textOpen = true;
    textIndex = nextIndex++;
    send({ type: "content_block_start", index: textIndex, content_block: { type: "text", text: "" } });
  };

  for await (const evt of chunks) {
    if (evt.error) throw new Error(evt.error.message || "upstream error");
    if (evt.usage) usage = { ...(usage || {}), ...evt.usage };
    const choice = evt.choices?.[0];
    if (!choice) continue;
    const delta = choice.delta ?? {};

    if (typeof delta.content === "string" && delta.content.length) {
      openText();
      send({ type: "content_block_delta", index: textIndex, delta: { type: "text_delta", text: delta.content } });
    }
    if (choice.finish_reason === "tool_calls") stopReason = "tool_use";
    for (const tc of delta.tool_calls ?? []) {
      const index = tc.index ?? 0;
      const call = calls.get(index) ?? { id: "", name: "", arguments: "" };
      if (tc.id) call.id = tc.id;
      if (tc.function?.name) call.name = (call.name || "") + tc.function.name;
      if (tc.function?.arguments) call.arguments += tc.function.arguments;
      calls.set(index, call);
      if (calls.size) stopReason = "tool_use";
    }
  }

  if (textOpen) send({ type: "content_block_stop", index: textIndex });

  for (const [, call] of [...calls.entries()].sort((a, b) => a[0] - b[0])) {
    if (!call.name) continue;
    const index = nextIndex++;
    send({
      type: "content_block_start",
      index,
      content_block: { type: "tool_use", id: call.id || newId("toolu"), name: call.name, input: {} },
    });
    send({
      type: "content_block_delta",
      index,
      delta: { type: "input_json_delta", partial_json: call.arguments || "{}" },
    });
    send({ type: "content_block_stop", index });
  }

  const u = usageOf(usage);
  send({
    type: "message_delta",
    delta: { stop_reason: stopReason },
    usage: { input_tokens: u.input_tokens, output_tokens: u.output_tokens },
  });
  send({ type: "message_stop" });
}

/** Non-streaming Chat Completions JSON -> Responses JSON. */
export function chatJsonToResponses(chat, model) {
  const choice = chat.choices?.[0];
  const text = choice?.message?.content ?? "";
  const output = [];
  if (text) output.push({ type: "message", role: "assistant", content: [{ type: "output_text", text }] });
  for (const tc of choice?.message?.tool_calls ?? []) {
    output.push({
      type: "function_call",
      call_id: tc.id || newId("call"),
      name: tc.function?.name,
      arguments: tc.function?.arguments || "{}",
    });
  }
  const u = usageOf(chat.usage);
  return {
    id: newId("resp"),
    object: "response",
    created_at: Math.floor(Date.now() / 1000),
    status: "completed",
    model: model || chat.model,
    output,
    usage: { ...u, total_tokens: u.input_tokens + u.output_tokens },
  };
}

/** Non-streaming Chat Completions JSON -> Anthropic Messages JSON. */
export function chatJsonToMessages(chat, model) {
  const choice = chat.choices?.[0];
  const content = [];
  const text = choice?.message?.content;
  if (typeof text === "string" && text) content.push({ type: "text", text });
  for (const tc of choice?.message?.tool_calls ?? []) {
    content.push({
      type: "tool_use",
      id: tc.id || newId("toolu"),
      name: tc.function?.name,
      input: safeJson(tc.function?.arguments),
    });
  }
  const u = usageOf(chat.usage);
  return {
    id: newId("msg"),
    type: "message",
    role: "assistant",
    model: model || chat.model,
    content,
    stop_reason: content.some((b) => b.type === "tool_use") ? "tool_use" : "end_turn",
    stop_sequence: null,
    usage: { input_tokens: u.input_tokens, output_tokens: u.output_tokens },
  };
}

/** Non-streaming Chat Completions JSON -> Anthropic Messages SSE (buffered). */
export function emitMessagesSse(chat, model, emit) {
  const choice = chat.choices?.[0];
  const text = choice?.message?.content ?? "";
  const toolCalls = choice?.message?.tool_calls ?? [];
  const u = usageOf(chat.usage);

  emit({
    type: "message_start",
    message: {
      id: newId("msg"),
      type: "message",
      role: "assistant",
      content: [],
      model,
      stop_reason: null,
      usage: { input_tokens: u.input_tokens, output_tokens: 0 },
    },
  });

  let index = 0;
  if (text) {
    emit({ type: "content_block_start", index, content_block: { type: "text", text: "" } });
    emit({ type: "content_block_delta", index, delta: { type: "text_delta", text } });
    emit({ type: "content_block_stop", index });
    index++;
  }
  for (const tc of toolCalls) {
    emit({
      type: "content_block_start",
      index,
      content_block: { type: "tool_use", id: tc.id || newId("toolu"), name: tc.function?.name, input: {} },
    });
    emit({
      type: "content_block_delta",
      index,
      delta: { type: "input_json_delta", partial_json: tc.function?.arguments || "{}" },
    });
    emit({ type: "content_block_stop", index });
    index++;
  }

  emit({
    type: "message_delta",
    delta: { stop_reason: toolCalls.length ? "tool_use" : "end_turn" },
    usage: { input_tokens: u.input_tokens, output_tokens: u.output_tokens },
  });
  emit({ type: "message_stop" });
}
