# HeyClicky AI Architecture

A reverse-engineering report on how HeyClicky's AI features work — the cursor brain, the agent
harness, the browser host, voice, and billing — plus the loopback integration that lets the app run
on your own model provider.

Target: `C:\Users\notcpu\AppData\Local\HeyClickyApp\current`
Method: batch IL decompilation with `dnSpy.Console` + live state inspection + protocol replay.
Date: 2026-11-17

---

## 0. Executive summary

HeyClicky is a .NET (WinUI/WebView2) shell around **three separate AI planes**, each with its own
transport:

| Plane | What the user sees | Transport | Who supplies the prompt | Who runs tools |
| --- | --- | --- | --- | --- |
| **OneBrain / cursor** | hold hotkey, talk, Clicky draws on screen | one SSE `POST /brain/turn` per turn | **HeyClicky Worker** (server-side) | mostly the client, some server |
| **Agent harness** | jobs, "Clicky drive", long tasks | local Bun process `clicky-harness.exe`, HTTP `/turn` + `/events` | the harness binary (bundled) | the harness |
| **Browser host** | Clicky clicking around a real browser | MCP over stdio (`ClickyBrowserHost.exe`) | n/a (tool server) | tool server |
| **Voice** | speech in / speech out | worker-minted Gemini/Deepgram tokens + `/agent/realtime/*` | n/a | server |

Two facts unlock a clean integration:

1. **The Worker base URL is already overridable, loopback-only.** `AppBundleConfiguration.Load()`
   reads `%LOCALAPPDATA%\HeyClicky\endpoints.json` (`workerBaseUrl`, `supabaseUrl`,
   `supabaseAnonKey`) and the `HEYCLICKY_WORKER_BASE_URL` env var, and rejects anything that is not
   `localhost` / `127.0.0.1` / `::1`. There is even a built-in `LocalWorkerBaseUrl` constant of
   `http://127.0.0.1:8787`.
2. **Every model call is a plain HTTP call to the Worker.** The cursor brain posts `/brain/turn` and
   parses SSE; the agent harness posts `/agent/openai/v1/responses` and
   `/agent/anthropic/v1/messages`. Neither path carries a provider API key — only a Worker session
   token.

So a loopback Worker can replace exactly the inference half of the product and proxy the rest. That
is what `gateway/` in this repo does. No binary patching, no license bypass, no token forgery.

**What stays on HeyClicky's servers** (by design): sign-in, device registration, memory, app config,
integrations/Composio, web and image search, image-search, speech-to-text, TTS, and the browser
host's cloud pieces. Those are services, not model inference.

---

## 1. Method and confidence legend

Every non-obvious claim below is tagged:

- **[C] Confirmed** — read directly in decompiled IL or in a real artifact on disk (request body,
  log line, JSON file).
- **[S] Supported by call sites** — inferred from how types are used and constructed across several
  files; no runtime capture yet.
- **[N] Suggested by names** — a string, field, or symbol says so, but the behaviour was not traced.
- **[?] Unknown** — not established.

Tools used:

```text
dnSpy.Console.exe --no-resources --no-baml --no-sln -o decompiled <assembly>.dll
```

→ 739 `.cs` files for `HeyClicky.dll`, `HeyClicky.Core.dll`, `HeyClicky.Agents.dll`,
`HeyClicky.Contracts.dll`.

Live evidence came from `%LOCALAPPDATA%\HeyClicky\` (settings, plan cache, harness event logs,
browser host registration) and from replaying `/brain/turn` against the gateway with a mock
upstream.

---

## 2. Install layout and process model

```
HeyClickyApp\current\
├─ HeyClicky.exe                 launcher
├─ HeyClicky.dll                 WinUI shell, settings, notch, windows
├─ HeyClicky.Core.dll            brain client, voice, billing, worker client
├─ HeyClicky.Agents.dll          agent runtime, harness supervisor, MCP plumbing
├─ HeyClicky.Contracts.dll       endpoint value objects, shared DTOs
├─ HarnessRuntime\clicky-harness.exe    85 MB Bun-compiled JS agent  [C]
├─ ClickyBrowserHost\ClickyBrowserHost.exe   MCP stdio server (browser tools)  [C]
├─ Assets\Engine\clicky-draw-engine.js   the drawing engine the model's code runs in  [C]
└─ BundledResources\clicky-model-instructions.md   harness system prompt  [C]
```

Process tree at runtime **[C]** (observed + `AgentHarnessRunnerProcess`):

```
HeyClicky.exe
 ├─ clicky-harness.exe            spawned per job, prints {"port":N} on stdout
 ├─ ClickyBrowserHost.exe         stdio MCP server, 4 tools
 └─ WebView2 (drawing + settings chrome)
```

The harness is launched with:

| Env var | Purpose |
| --- | --- |
| `CLICKY_HARNESS_TOKEN` | bearer token the app uses for `/turn`, `/events`, `/steer`, `/cancel` |
| `CLICKY_HARNESS_STATE` | state root |
| `CLICKY_H1_STATE_DIR` | per-job state dir (`agents\AgentHarness\h1-local\...`) |

and the app talks to it over loopback HTTP: `POST /turn`, `GET /events?job=…&after=…&wait=20000`,
`POST /steer`, `POST /cancel`.

State on disk:

```
%LOCALAPPDATA%\HeyClicky\
├─ settings.json                 60+ settings incl. OneBrainModel, DeveloperChatModels
├─ app-config.json               server-pushed config
├─ device-registration.json
├─ auth.bin                      Supabase session (encrypted)
├─ endpoints.json                <-- developer Worker override (absent by default)
├─ billing\last-plan-<uuid>.json cached plan snapshot
├─ agents\AgentHarness\h1-local\<harness-id>\{job.json,turns.json,events.jsonl,steps.jsonl,blobs}
├─ BrowserBridge\connection.json {"port":50540,"token":"…"}      local bridge
├─ BrowserHost\com.heyclicky.browser.json                        native messaging host
├─ codex-home\                   Codex CLI heritage (connectors, sign-ins)
└─ OneBrain\                     thread cache
```

---

## 3. The cursor brain (OneBrain)

### 3.1 Turn lifecycle

```
hotkey down
  → audio frames → Gemini live transcription (worker-minted token)      [C]
  → screenshot of the screen under the cursor (JPEG)                    [C]
  → POST /brain/turn   (SSE, 120 s timeout, optional gzip)              [C]
  → SSE text deltas accumulate into a *drawing program*                 [C]
  → ProgramText.ProgramCode() extracts fenced blocks                    [C]
  → WebView2 runs them in clicky-draw-engine.js                         [C]
  → say() lines are spoken (worker TTS) while strokes are paced         [S]
  → client-run tools fire (open_url, copy, insert_text, …)              [C]
  → if tool calls need a result, the client posts another /brain/turn
    with turn_items containing function_call_output                     [C]
  → on completion the client commits the turn into its own thread       [C]
```

`BrainClient.StreamTurnAsync` (`HeyClicky.Core\Brain\BrainClient.cs:33`) builds the body with a
`Utf8JsonWriter`, optionally gzips it (`BrainRequestCompression`, path-gated, only after the Worker
advertises `x-clicky-accepts-gzip: 1`), POSTs with `Authorization: Bearer <supabase access token>`
and optional `X-Clicky-Usage-Context`, then streams and parses SSE.

A non-200 goes through `BillingPaywallDetector.HandleIfPaywall` → `PaywallReachedException`, or
`WorkerHttpException`. So **the paywall is an HTTP status from the Worker**, not a client decision.

There is a debug hook: `HEYCLICKY_BRAIN_DUMP_DIR` writes every request body to disk before sending
(`BrainClient.cs:69`). The gateway has the equivalent (`brain.dumpDir`) but sees decoded JSON.

### 3.2 Request body

Written by `CompanionSession.WriteTurnBody` (`HeyClicky.Core\Voice\CompanionSession.cs:2590`).
**[C]**

```jsonc
{
  "thread": [ /* prior turns, Responses-style items */ ],
  "turn": {
    "words": "…",                       // what the user said / typed
    "fact": "…",                        // short situational fact
    "time_label": "…",
    "front_app": "…", "front_app_bundle_id": "…",
    "drawing_on_screen": "…",           // code of a drawing already shown
    "highlight": { "gesture": "…", "text": "…" } | null,
    "page": { … } | null,               // browser page context
    "input": "voice" | "text",
    "live_state": "…",
    "screenshot": {                     // omitted when screenless
      "base64": "…", "mimeType": "image/jpeg",
      "widthInPixels": 1512, "heightInPixels": 982
    },
    "stable_note": "…",                 // optional
    "flow_brief": { … },                // optional
    "drawing_code": "…",                // optional
    "audio_seconds": 3.42, "stt_provider": "gemini",
    "attached_images": [ { "base64": "…", "mimeType": "image/jpeg" } ],
    "other_screens":   [ { "base64": "…", "mimeType": "image/jpeg", "widthInPixels": …, "heightInPixels": … } ],
    "walkthrough_step": 2,
    "trigger": "…",
    "flow": "…"
  },
  "turn_items": [ /* tool outputs produced during this turn */ ],
  "session_id": "…",
  "max_rounds": 5,
  "async_tools": true,
  "turn_token": "…",                    // optional, billing/continuation
  "screenless": true,                   // optional
  "figures_float": true,                // optional flags
  "library_items": true, "routines_tool": true, "find_jobs_tool": true, "job_handoffs": true,
  "saves_drawings": true,
  "drive": true, "takeover": true,      // drive mode
  "user_layer": "…", "user_layer_signature": "…",   // OR user_context + environment
  "brain_model": "gpt-6.1-sol"          // optional override (Settings → Developer)
}
```

Two design details matter:

- **`user_layer` / `user_layer_signature`** are echoed back from a previous `meta` SSE event. They
  are the server-issued system prompt (and its cache signature). The client never ships a brain
  system prompt itself. **[C]** — this is why a local brain needs its own prompt.
- **`max_rounds: 5`** and **`async_tools: true`** are constants, not policy from the server. **[C]**

### 3.3 SSE wire format

`BrainSseParser.DispatchLine` (`BrainSseParser.cs:65`) is strict and small:

- A line must start with the five bytes `data:`; anything else is ignored (comments, `event:`,
  `id:`, `retry:`).
- Leading spaces after the colon are trimmed; empty payloads are dropped.
- If the payload starts with the 14 bytes `{"type":"text"`, it is treated as a **text chunk** and
  the `text` property is appended to the answer. **Key order matters** — `type` first.
- Otherwise it is parsed as JSON and dispatched by its `type` field.

So the minimal viable stream is:

```
data:{"type":"text","text":"```js\n"}
data:{"type":"text","text":"say(\"hi\");\n```"}
data:{"type":"done","input":1234,"last_input":1200}
```

`BrainSseParser` → `BrainReplyAccumulator.OnEvent` (`BrainReplyAccumulator.cs:103`) handles:

| `type` | Effect |
| --- | --- |
| `meta` | `model`, `user_layer`, `user_layer_signature`, `guided_max_rounds`, `drive_lease_id` |
| `text` (implicit) | appended to raw text → program code |
| `items` | architecturally-significant items (`function_call`, `reasoning`, `message`…) |
| `status` | spoken/waiting status (`searching`, `reading_apps`, `tool:<name>`, `tool_done:<name>`) |
| `tool_call` | one `{call_id,name,arguments,async}` call |
| `tool_calls` | array of calls for this round |
| `computer_calls` | drive: `{call_id, actions[], pending_safety_checks[]}` |
| `search_record` | a search citation line kept in history |
| `error` | `{message, code}` → `BrainTurnException` |
| `done` | ends the turn; carries usage + `turn_token` + `screenshot_file` |

**`done` is mandatory.** If the stream ends without it the client throws
`IOException("the connection ended before the answer was complete")`. **[C]**

### 3.4 The tool loop

The Worker is stateless per request; the **client** orchestrates rounds
(`CompanionSession.cs:2208-2260`):

1. Reply contains `tool_calls`.
2. `BrainToolRunner.OutputsAsync` runs them, in parallel for a whitelist
   (`file_read`, `file_list`, `clicky_memory`, `session_status`, `clicky_artifacts`,
   `google_calendar_find_events`, `find_jobs`) and sequentially otherwise.
3. Each result becomes `{"type":"function_call_output","call_id":…,"output":…}` in `turn_items`.
4. If `BrainToolCallPolicy.NeedsAnotherRound` says so, the client posts `/brain/turn` again with the
   same `thread` and the accumulated `turn_items`.
5. Loop stops at `max_rounds`, when code was produced and no tool needs a round, or on drive tool
   names.

Tool names seen in the client **[C]**: `open_url`, `copy_to_clipboard`, `insert_text`,
`file_read`, `file_list`, `file_write`, `web_search`, `shell_exec`, `save_memory`, `clicky_memory`,
`capture_screen`, `sessions_spawn`, `sessions_send`, `sessions_answer_request`, `sessions_yield`,
`sessions_interrupt`, `routines`, `play_on_spotify`, `clicky_artifacts`, `open_artifact`,
`reveal_artifact`, `open_agent_dashboard`, `read_connected_apps`, `send_to_higher_model`,
`find_jobs`, `show_result`, `runtime_status`, `session_status`, `tutorial_draw_on_screen`,
`skip_onboarding_interview`.

Only three are implemented in the client itself (`RunToolCallAsync`, `CompanionSession.cs:4665`):
`open_url`, `copy_to_clipboard`, `insert_text`. Everything else goes through
`options.ToolCallHandler` in `HeyClicky.dll`, which is where Worker-backed and app-backed tools live.

### 3.5 The drawing program

The reply is not prose. `ProgramText.ProgramCode` (`ProgramText.cs:51`) extracts fenced blocks and
bare `say(...)` segments, joins them, and hands them to WebView2. Anything outside a fence is
discarded unless it contains `say(`.

The engine (`Assets\Engine\clicky-draw-engine.js`, "lab engine 1.6") exposes a small drawing API:
`say`, `step`, `line`, `curve`, `circle`, `ellipse`, `rect`, `poly`, `path`, `text`, `arrow`,
`highlight`, `dot`, `image`, `molecule`, plus geometry helpers and named colours. It also supports
incremental "live" execution, per-line speech pacing, live edits (`L.edit`), 3D stages, and
`seen()` / `noted()` silent annotations. **[C]**

This is the single most important detail for a BYOK brain: *the model has to emit this DSL*, not
markdown.

---

## 4. Agent harness and drive

### 4.1 Harness contract

`AgentHarnessRunnerRuntime` posts a turn payload to the harness:

```jsonc
{
  "job": "…", "harness": "h1|codex|pi", "place": "…", "model": "…",
  "workspace": "…", "instructions": "…",
  "mcp": { … }, "skills": [ … ], "userSkills": [ … ],
  "leaseMetadata": { … }, "deviceHelper": { … },
  "worker": { "base": "https://api.heyclicky.com", "sessionToken": "…" },
  "text": "…", "images": [ … ],
  "planTool": { … }, "note": "…"
}
```

Events stream back from `/events`. The harness executes the agent loop locally with
`update_plan`, `bash` (PowerShell on Windows), `read`, `edit`, `view_image`, `run_script`, plus MCP
servers.

### 4.2 Model gateway

Inside `clicky-harness.exe` the model URLs are built from the **same** `worker.base`:

```
${workerBase}/agent/openai/v1/responses
${workerBase}/agent/anthropic/v1/messages
${workerBase}/agent/web-search
```

Observed in a real job trace (`events.jsonl`): `provider:"openai"`, `path:"/v1/responses"`,
`model:"gpt-6-luna"`, `effort:"medium"`, `stream:true`, request bodies 45 KB → 210 KB, usage
`{input, cached, output, reasoning}`. **[C]**

So the harness is a Codex-descended agent that assumes an OpenAI Responses endpoint. It never sees a
provider key; it passes its Worker session token as the bearer.

---

## 5. Voice, dictation, realtime

Voice is **server-mediated**, which is why it cannot be moved to a BYOK model without reimplementing
audio:

| Endpoint | Purpose | Confidence |
| --- | --- | --- |
| `/brain/ears/token` | mint a live STT session (Gemini) | [C] |
| `/brain/ears/transcribe` | batch transcription | [C] |
| `/agent/realtime/session` | realtime speech session | [C] |
| `/agent/realtime/speak` | TTS | [C] |
| `/agent/realtime/voice-preview` | voice picker preview | [C] |
| `/agent/realtime/warmup` | pre-open a session | [C] |
| `/v2/dictation/gemini-token` | dictation STT token | [C] |
| `/v2/dictation/deepgram-token` | dictation STT token (fallback) | [C] |

The turn body carries `stt_provider: "gemini"` when audio was transcribed, which confirms the
input side. **[C]**

---

## 6. Billing and entitlement model

There are **two enforcement layers**.

**Server-side (authoritative).** `/brain/turn`, `/agent/*`, search, and voice return `402`/`409` with
a paywall payload when the account is out of quota. `BillingPaywallDetector` turns that into
`PaywallReachedException`. **[C]**

**Client-side (the "free tier" courtesy gate).**
`BillingStateService.ShouldBlockFreeUserMessageTurnLocally()` (`BillingStateService.cs:581`):

```csharp
if (plan != "free") return false;
if (billing_enforcement != "on") return false;
if (localMessagesUsed >= usage.messages.cap) { /* paywall */ }
```

`localMessagesUsed` is seeded from `/me/plan` and incremented locally per turn. A cached snapshot
lives at `billing\last-plan-<uuid>.json`; the user's machine currently reads:

```json
{"plan":"free","usage":{"messages":{"used":12,"cap":12},"agents":{"used":12,"cap":12},
 "dictation":{"used":0,"cap":4000},"window_resets_at":"2026-11-16T16:00:00.000Z"},
 "billing_enforcement":"on","tier":"free"}
```

So the app bricks its own turn button before it ever sends a request. That single field —
`billing_enforcement` — is the switch the gateway flips for plans that no longer consume HeyClicky
inference.

`PlanFixtures` (Settings → Developer → Force plan state) lets the team preview every payment state
without Stripe. **[C]**

---

## 7. Local state and privacy

What leaves the machine on a normal turn:

- a JPEG of the screen under the cursor (unless `screenless`),
- the spoken words (already transcribed in the cloud),
- front-app name and optional page context,
- the conversation thread,
- for agents: the workspace contents the harness reads.

What stays local: drawings, the browser bridge token, `auth.bin`, the thread cache in `OneBrain\`.

With the gateway, the same payload goes to **your** provider instead of `api.heyclicky.com`, while
auth/memory/integrations still go to HeyClicky. That is a meaningful privacy difference in one
direction (the model vendor changes) and no difference in another (HeyClicky still sees metadata and
service calls).

---

## 8. The integration

### 8.1 Why this seam

Three alternatives were considered:

| Approach | Verdict |
| --- | --- |
| Patch `HeyClicky.dll` / IL-edit the model call | brittle, breaks on update, no need |
| Fake the plan locally (edit `last-plan-*.json`) | only bypasses the courtesy gate; server still 402s |
| **Point the loopback Worker at a gateway** | uses the app's own developer path; no patching; clean split between inference and services |

### 8.2 Architecture

```
┌────────────────┐    endpoints.json      ┌──────────────────────┐
│  HeyClicky.exe │ ─────────────────────▶ │  clicky-gateway      │
│  (harness/UI)  │  127.0.0.1:8787        │  node, no deps       │
└────────────────┘                        └───────┬──────────────┘
        │                                         │
        │ /brain/turn  (SSE)                      ├─▶ provider endpoint
        │ /agent/openai/v1/responses              │     responses | chat | messages
        │ /agent/anthropic/v1/messages            │
        │ /me/plan, /runtime/model-policy         └─▶ api.heyclicky.com (passthrough)
        │ everything else ─────────────────────────────────────────▶
```

The gateway is provider-agnostic. A preset declares which protocols it serves **natively**
(`endpoints`); the gateway calls those paths directly and only translates when the protocol is
missing. Every lane therefore works with every preset. **[C]**

| Preset | `api` | Native | Notes |
| --- | --- | --- | --- |
| `opencode` | responses | Responses, Messages | one key for GPT/Claude/Gemini/GLM/Kimi (Console translates per model) |
| `openrouter` | chat | Chat Completions | hundreds of models behind one key |
| `openai` | responses | Responses, Chat | Responses is the highest-fidelity agent lane |
| `anthropic` | messages | Messages | uses `x-api-key` + `anthropic-version` |
| `google` | chat | Chat Completions (Gemini compat) | AI Studio key; model list from native `/v1beta/models`; `stream_options` stripped |
| `lmstudio` | chat | Chat Completions | local, key optional |
| `ollama` | chat | Chat Completions | local, key optional |
| `custom` | chat | Chat Completions | point `baseUrl` at any OpenAI-compatible server |

Presets live in `lib/config.mjs:PROVIDERS`; every field can be overridden in `gateway/config.json`
or with `node bin/clicky.mjs base <provider> <url>`.

### 8.3 Request translation

| HeyClicky | Gateway | Provider |
| --- | --- | --- |
| `POST /brain/turn` (thread + turn + turn_items) | rebuild as a Responses request: `instructions` = reconstructed prompt, `input` = thread + turn context + screenshot + turn_items, `tools` = the three local tools | native `responses` endpoint |
| same, chat-only provider | `responsesToChat` (system message, function calls, tool outputs, images) | `chat/completions` |
| same, Messages-only provider | `responsesToChat` → `chatToMessages` | `v1/messages` |
| SSE `text` deltas | re-emitted as `{"type":"text"}` chunks | `response.output_text.delta` / `choices[].delta.content` / `text_delta` |
| `tool_calls` | `{"type":"tool_calls","calls":[…],"async":true}` | streamed `function_call` / `tool_calls` / `tool_use` |
| `done` | synthesized with usage + `cost:0` | completed / final usage chunk / `message_delta` |
| `POST /agent/openai/v1/responses` | model rewrite + field strip, or translate to the provider protocol | native `responses`, else chat/messages |
| `POST /agent/anthropic/v1/messages` | model rewrite + `max_tokens` default, or translate | native `messages`, else chat/responses |

Translations use **Chat Completions as the hub** (`gateway/translate.mjs`):

```
Responses --responsesToChat--> Chat --chatToMessages--> Messages
Messages  --messagesToChat---> Chat --chatToResponses-> Responses
```

with matching stream converters in each direction (`chatChunksToBrain`, `chatChunksToResponses`,
`chatChunksToMessages`, `messagesStreamToChat`, `responsesStreamToChat`, plus the non-streaming JSON
equivalents). Tool arguments are accumulated across deltas, consecutive `function_call` items are
merged into one assistant `tool_calls` message, and `stream_options.include_usage` is added for
OpenAI-style streams.

The thread and `turn_items` are **already** OpenAI Responses items (`message` with
`input_text`/`output_text`/`input_image`, `function_call`, `function_call_output`,
`computer_call_output`, `reasoning`). On a native Responses provider the brain path needs no schema
conversion — only prompt and glue. On translated paths `reasoning` and `computer_call` items have no
Chat/Messages equivalent and are dropped.

### 8.4 The reconstructed prompt (honest caveat)

HeyClicky's real brain prompt is server-side: the request carries `user_layer` +
`user_layer_signature`, and the client has no copy. The gateway therefore ships
`gateway/brain.mjs:PROGRAM_INSTRUCTIONS`, written against the public surface of
`clicky-draw-engine.js`: output format, coordinate space, the full drawing API, style rules, the
three tools, and behavioural rules.

This is the one place where the integration is **not** byte-compatible with production. Expect:

- good structural fidelity (fences, `say()`, coordinates, tools),
- weaker taste than the tuned production prompt,
- occasional prose-instead-of-program answers, which the client will silently drop.

Practical mitigations: keep `brain.effort` low, use a strong model for the brain lane, and put
few-shot examples in the prompt if you care about a house style.

### 8.5 Billing semantics

- Inference is billed to **the provider you selected** — opencode Console (PAYG balance or Go
  allowance), OpenRouter credits, OpenAI/Anthropic billing, or a local server (free).
- `/me/plan` is fetched from the real Worker, then `billing_enforcement` is rewritten to `"off"`
  **only when the plan is free**. The app then shows the real plan/usage but stops blocking turns
  locally. Paid plans are untouched.
- Nothing else about the account changes. Voice minutes, dictation, image search, integrations and
  the browser host keep their real server-side limits.

This is deliberately *not* a "make me Pro" switch: it does not unlock paid server features, it only
stops a free account from being rate-limited on a turn that never reaches HeyClicky.

### 8.6 Limitations

| Feature | Status with the gateway |
| --- | --- |
| Cursor drawing + speech | works (text + vision model) |
| Voice input | still HeyClicky (worker STT); unaffected |
| TTS | still HeyClicky; unaffected |
| `open_url` / `copy_to_clipboard` / `insert_text` | works (client-side tools) |
| `web_search`, `image_search` | proxied to HeyClicky; subject to their limits |
| Memory, connected apps, Composio | proxied to HeyClicky; subject to their limits |
| Jobs / agents | works via `/agent/*` (model), tools run in the harness |
| **Clicky drive (computer use)** | **not implemented** — drive needs `computer_calls` + screenshot round-trips; the gateway's prompt tells the model to decline politely |
| Thread compaction | disabled (`/brain/compact` returns "disabled"); client-side history keeps growing |
| Prompt caching | only what the upstream does; no `user_layer_signature` reuse |
| Vision | screenshots travel as images; pick a vision-capable model (`clicky models` tags them). A provider that rejects image input triggers one automatic retry without the screenshot, and that provider+model is remembered as text-only until restart. `clicky screenshots off` skips images from the start |
| Reasoning effort | forwarded on native Responses; omitted when a chat/messages provider is translated |
| Vendor extras | Anthropic prompt caching, OpenAI `store`, Gemini safety blocks etc. are only available when the native endpoint is used |

### 8.7 Validation performed

Replayed against mock providers that emit real SSE for each protocol:

- `/brain/turn` via a **Chat Completions** provider → meta, text deltas, `tool_calls(open_url)`,
  `done` with merged usage. **[C]**
- `/brain/turn` via a **Messages** provider → text deltas, merged input/output usage, valid `done`. **[C]**
- `/brain/turn` via a **Responses** provider → native relay, text + tool call + usage. **[C]**
- `/agent/openai/v1/responses` with a chat provider → `response.created` … `output_text.delta` …
  `function_call` (single, un-duplicated name) … `response.completed`. **[C]**
- `/agent/anthropic/v1/messages` with a chat provider → `message_start` … `text_delta` … `tool_use`
  … `message_stop`. **[C]**
- Both cross-protocol agent lanes (messages↔responses) → correct wire format each time. **[C]**
- Non-streaming agent calls → Responses JSON / Messages JSON with text and tool calls. **[C]**
- Text-only model path: a provider that answers 404 `No endpoints found that support image input`
  causes one retry with the screenshot (and any images in the thread) stripped; the turn then streams
  normally and the model is remembered as text-only. **[C]**
- `clicky models` annotates `[vision]` / `[text]` from OpenRouter-style `architecture.input_modalities`. **[C]**
- `google` preset: OpenAI-compatible Gemini endpoint, native model listing (prefix stripped, embeddings
  filtered), screenshot sent as `image_url`, tools and `stream_options.include_usage` accepted (so
  token counts reach `done`). **[C]**
- Transient provider failures (`408/409/429/500/502/503/504`, network errors) retry twice with 0.4s/0.8s
  backoff (`providerRetries`), then a valid `say("…")` program is streamed so the failure is spoken
  rather than silent (`brain.speakErrors`). Verified with a mock that answers 503,503,200 → one turn,
  three attempts, real answer; 503 forever → HTTP 200 SSE with `meta.fallback` and a `done`. **[C]**
- Live Google check (Nov 2026): `gemini-3-flash-preview` streams text (~3s), a 1×1 PNG screenshot is
  accepted (`images=true`), `open_url` tool call surfaces as `tool_calls`, and usage reports
  `input_tokens`/`output_tokens`. `gemini-2.5-flash` → 404 retired, `gemini-flash-latest` → 503,
  `gemini-3.5-flash` works but ~15s per turn. **[C]**
- `/me/plan` → synthesized free plan with `billing_enforcement:"off"`. **[C]**
- `/runtime/model-policy` → `{"codexAgentLane":{…}}` (the shape `AgentTurnCostLedger` reads). **[C]**
- `clicky providers|use|models|key|base|status|doctor|off` clean lifecycle; endpoints.json removed on
  `off`. **[C]**

Not yet validated: a live end-to-end turn inside HeyClicky against each real provider (needs real
keys and an app restart).

### 8.8 Install / undo

```powershell
node bin/clicky.mjs install                      # config + ~/.config/opencode/commands
node bin/clicky.mjs key openrouter sk-or-…       # or: key oc_sk_… (current provider)
node bin/clicky.mjs use openrouter anthropic/claude-sonnet-4.5
node bin/clicky.mjs on                           # writes endpoints.json, starts the gateway
# restart HeyClicky

node bin/clicky.mjs off                          # stops gateway, deletes endpoints.json (restores backup)
```

Useful extras: `clicky providers` (preset matrix), `clicky models <provider>` (ids the provider
actually serves), `clicky use <provider> <model> --brain|--agent` (per-lane), `clicky base <provider>
<url>` (custom server), `clicky status` / `doctor`.

The gateway never writes to the HeyClicky install directory, never modifies `HeyClicky.dll`, and
never touches `auth.bin`. The only app-visible change is `endpoints.json`.

---

## 9. Findings ledger

| # | Finding | Confidence |
| --- | --- | --- |
| 1 | Worker base URL overridable via `endpoints.json` + `HEYCLICKY_WORKER_BASE_URL`, loopback-only | [C] |
| 2 | `LocalWorkerBaseUrl = http://127.0.0.1:8787` is a built-in constant | [C] |
| 3 | `/brain/turn` carries thread + turn + turn_items and streams SSE | [C] |
| 4 | SSE lines must start `data:`; text events must start `{"type":"text"` | [C] |
| 5 | `done` event is required or the client throws | [C] |
| 6 | Tool loop is client-orchestrated, max 5 rounds, `async_tools:true` | [C] |
| 7 | Brain prompt is server-side (`user_layer` echoed by client, never shipped) | [C] |
| 8 | Reply text is a drawing program for `clicky-draw-engine.js` | [C] |
| 9 | Harness model calls go to `${workerBase}/agent/{openai,anthropic}/…` | [C] |
| 10 | Harness runs `gpt-6-luna` via `/v1/responses`, stream, effort medium | [C] |
| 11 | Free-tier gate is `plan=="free" && billing_enforcement=="on" && used>=cap` | [C] |
| 12 | Server-side paywall is `402` + payload, detected by `BillingPaywallDetector` | [C] |
| 13 | `clicky_browser` MCP server has 4 tools, stdio, registered by native messaging manifest | [C] |
| 14 | Voice STT/TTS is worker-minted (Gemini live, Deepgram fallback) | [C] |
| 15 | Harness is Codex-descended (codex-home, connectors.codex-sign-ins-imported, Responses API) | [S] |
| 16 | Composio provides third-party app integrations | [S] |
| 17 | `clickys.json` / `Clickys\jobs` is the local job store | [S] |
| 18 | Drive uses `computer_calls` with screenshot outputs per round | [C] |
| 19 | `HEYCLICKY_BRAIN_DUMP_DIR` dumps turn bodies pre-send | [C] |
| 20 | Plan/preview fixtures exist for every billing state | [C] |

---

## 10. Ethics and terms

This integration keeps HeyClicky's own service boundary intact: it substitutes *your* inference
provider for *their* inference, and it stops a local courtesy gate from blocking turns that never
reach their servers. It does not forge auth, unlock paid server features, or bypass a server-side
paywall — those still return `402` and still apply.

Two things to keep in mind:

- HeyClicky's terms may restrict redirecting the client to a non-official backend. The override is a
  first-class developer feature, but "developer feature" is not the same as "supported for end
  users".
- The gateway sends screen images to the provider you configure. Make sure that provider's retention
  policy is the one you want.

---

## Appendix A — Endpoints observed

| Endpoint | Method | Owner |
| --- | --- | --- |
| `/brain/turn` | POST SSE | Worker |
| `/brain/compact` | POST | Worker |
| `/brain/session-end` | POST | Worker |
| `/brain/prewarm` | POST | Worker |
| `/brain/ears/token` | POST | Worker |
| `/brain/ears/transcribe` | POST | Worker |
| `/agent/openai/v1/responses` | POST | Worker → provider |
| `/agent/anthropic/v1/messages` | POST | Worker → provider |
| `/agent/web-search` | POST | Worker |
| `/agent/realtime/{session,speak,voice-preview,warmup}` | POST | Worker |
| `/me/plan` | GET | Worker |
| `/me/memory`, `/me/memory/save` | GET/POST | Worker |
| `/me/devices`, `/me/discovery-channel` | GET/POST | Worker |
| `/me/account/delete` | POST | Worker |
| `/me/referral`, `/me/referral/code`, `/me/referral/claim` | GET/POST | Worker |
| `/runtime/model-policy` | GET | Worker |
| `/app-config` | GET | Worker |
| `/billing/checkout`, `/billing/portal` | POST | Worker |
| `/image-search`, `/web-search` | POST | Worker |
| `/onboarding/language-hints`, `/onboarding/starter-plan` | POST | Worker |
| `/v2/dictation/{gemini,deepgram}-token` | POST | Worker |

Supabase is used directly for auth (`https://mrpvynsdsnimuisyhkow.supabase.co`).

## Appendix B — Environment variables

| Variable | Effect |
| --- | --- |
| `HEYCLICKY_WORKER_BASE_URL` | loopback Worker override |
| `HEYCLICKY_BRAIN_DUMP_DIR` | dump `/brain/turn` bodies |
| `CLICKY_HARNESS_TOKEN` / `CLICKY_HARNESS_STATE` / `CLICKY_H1_STATE_DIR` | harness launch |
| `CLICKY_UPSTREAM_KEY` / `CLICKY_UPSTREAM_BASE` | gateway config override |
| `CLICKY_PROVIDER` | force both lanes onto one provider preset |
| `CLICKY_BRAIN_MODEL` / `CLICKY_AGENT_MODEL` / `CLICKY_PORT` | gateway config override |
| `CLICKY_GATEWAY_CONFIG` | alternate config path |

## Appendix C — Source references

| Topic | File |
| --- | --- |
| endpoints override | `HeyClicky.Core\Configuration\AppBundleConfiguration.cs` |
| brain transport | `HeyClicky.Core\Brain\BrainClient.cs` |
| SSE parsing | `HeyClicky.Core\Brain\BrainSseParser.cs` |
| event handling | `HeyClicky.Core\Brain\BrainReplyAccumulator.cs` |
| request body | `HeyClicky.Core\Voice\CompanionSession.cs:2590` |
| tool loop | `HeyClicky.Core\Voice\CompanionSession.cs:2208` |
| tool policy | `HeyClicky.Core\Brain\BrainToolCallPolicy.cs` |
| program extraction | `HeyClicky.Core\Brain\ProgramText.cs` |
| billing gate | `HeyClicky.Core\Billing\BillingStateService.cs:581` |
| plan model | `HeyClicky.Core\Worker\PlanSnapshot.cs` |
| worker client | `HeyClicky.Core\Worker\WorkerClient.cs` |
| harness launch | `HeyClicky.Agents\Harness\AgentHarnessRunnerProcess.cs` |
| harness runtime | `HeyClicky.Agents\Harness\AgentHarnessRunnerRuntime.cs` |
| developer settings | `HeyClicky\App\Settings\SettingsContent.cs:2076` |
| developer flags | `HeyClicky\App\Settings\DeveloperFlags.cs` |
| drawing engine | `Assets\Engine\clicky-draw-engine.js` |
| harness prompt | `BundledResources\clicky-model-instructions.md` |

## Appendix D — Drawing DSL quick reference

```js
say("spoken line")              // one per beat; strokes after it are paced to speech
step(n)
line([[x,y],…], o)  curve([[x,y],…], o)  poly([[x,y],…], o)  arrow([[x,y],…], o)
rect(x,y,w,h,o)  circle(x,y,r,o)  ellipse(x,y,rx,ry,o)  dot(x,y,r,o)
text(x,y,"label",{size,anchor,color,weight})  highlight(x,y,w,h,o)
path("M0 0 L10 10", o)  image(x,y,w,h,{query})  molecule(smiles,x,y,w,h)
seen("…")  noted("…")           // silent annotations
```

Coordinates are canvas pixels (`W`, `H`). Colours: `ink, white, grey, butter, sun, orange, peach,
coral, red, pink, lilac, violet, sky, blue, mint, leaf, brown, tan, cream`, or hex. Helpers:
`range, lerp, polar, add, sub, mul, mid, along, dist, angle, perp, rotate`, plus the usual `Math`
functions.
