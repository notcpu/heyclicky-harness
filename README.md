# HeyClicky × opencode

Use **HeyClicky as the harness** — its cursor, drawings, notch, agents and drive UI — while
**inference runs on a provider you choose** instead of HeyClicky's hosted models.

```
HeyClicky.exe ──endpoints.json──▶ 127.0.0.1:8787 (clicky-gateway) ──▶ your provider
                                            └──────────────▶ api.heyclicky.com (everything else)
```

Nothing in `HeyClicky.dll` is patched. The app already supports a loopback Worker override
(`endpoints.json` / `HEYCLICKY_WORKER_BASE_URL`); the gateway is the server that override points at.

## Quick start

```powershell
cd C:\Users\notcpu\Desktop\heyclicky-re

node bin/clicky.mjs install                      # config + opencode slash commands
node bin/clicky.mjs key openrouter sk-or-...     # key for that provider
node bin/clicky.mjs use openrouter anthropic/claude-sonnet-4.5
node bin/clicky.mjs on                           # start gateway, write endpoints.json
# quit HeyClicky, open it again
```

Then hold the Clicky hotkey and ask something. The cursor still draws; the answer comes from your
model.

> **Pick a vision model, or turn screenshots off.** HeyClicky attaches a screenshot to every turn.
> Text-only models make OpenRouter/OpenAI reject the whole request
> (`No endpoints found that support image input`). Either choose a model marked `[vision]` in
> `clicky models`, or run `node bin/clicky.mjs screenshots off`. The gateway also detects that error,
> drops the screenshot and retries once, so a text-only model still answers (minus screen awareness).

Undo:

```powershell
node bin/clicky.mjs off                # stop gateway, remove endpoints.json
```

## Providers and models

Seven presets ship in `lib/config.mjs`; all of them work in both lanes.

| Preset | Speaks | Best for |
| --- | --- | --- |
| `opencode` | Responses + Messages | one key for GPT / Claude / Gemini / GLM / Kimi (Console translates per model) |
| `openrouter` | Chat Completions | hundreds of models, one key |
| `openai` | Responses + Chat | direct OpenAI billing |
| `anthropic` | Messages | direct Claude billing |
| `google` | Chat Completions (Gemini compat) | Google AI Studio key, Gemini models, images supported |
| `lmstudio` | Chat Completions | local models, no key needed |
| `ollama` | Chat Completions | local models, no key needed |
| `custom` | Chat Completions | any OpenAI-compatible server |

```powershell
node bin/clicky.mjs providers                    # what is available + what is active
node bin/clicky.mjs models openrouter            # ids the provider actually serves
node bin/clicky.mjs use openrouter               # switch, keep models
node bin/clicky.mjs use openrouter <model>       # switch provider + model
node bin/clicky.mjs use <provider> <model> --brain   # cursor lane only
node bin/clicky.mjs use <provider> <model> --agent   # agent lane only
node bin/clicky.mjs base custom http://127.0.0.1:8080/v1
node bin/clicky.mjs key <provider> <key>
node bin/clicky.mjs screenshots off             # text-only models
```

Example with Google:

```powershell
node bin/clicky.mjs key google AIza...
node bin/clicky.mjs models google
node bin/clicky.mjs use google gemini-3-flash-preview
node bin/clicky.mjs off; node bin/clicky.mjs on
```

Google notes (learned the hard way):

- Model ids come and go on AI Studio. A retired one answers `404 ... no longer available to new
  users`, a popular one answers `503 ... experiencing high demand`. Neither is a gateway bug —
  pick another id from `clicky models google`.
- The gateway retries `429`/`5xx` and network errors twice with short backoff (`providerRetries`),
  which usually rides out a 503 spike.
- If it still fails, the gateway streams a tiny `say("...")` program so Clicky explains the problem
  out loud instead of staying silent. Set `brain.speakErrors: false` to get the raw 502 instead.
- Verified working with this gateway (Nov 2026): `gemini-3-flash-preview` (~3s a turn),
  `gemini-3.1-flash-lite` (~2s), `gemini-3.5-flash` (~15s, strongest of the three).
  `gemini-2.5-flash` is retired; `gemini-flash-latest` was returning 503.

`clicky models` tags entries `[vision]` / `[text]` when the provider publishes that metadata. The
gateway retries without the screenshot once if the provider rejects image input, and remembers the
choice per provider+model until restart.

Mixing lanes is fine: e.g. a cheap fast model for the cursor brain and a strong model for agents.
The gateway is provider-agnostic — when the provider has the native endpoint it passes through, and
when it does not it translates through Chat Completions (see `gateway/translate.mjs`).

## Menu

Inside opencode:

| command | what it does |
| --- | --- |
| `/clicky` | status |
| `/clicky-on` | start the gateway and point HeyClicky at it |
| `/clicky-off` | stop and restore the stock Worker |
| `/clicky-doctor` | check keys, providers, paths |
| `/clicky-model <provider> [model]` | switch provider/model |
| `/clicky-test <words>` | send a synthetic turn |

Or a terminal menu:

```powershell
node bin/clicky.mjs menu
```

## What runs where

| Path | Handled by | Billed to |
| --- | --- | --- |
| `/brain/turn` (cursor, voice turns) | gateway → provider | your provider |
| `/agent/openai/v1/responses` (agent harness) | gateway → provider | your provider |
| `/agent/anthropic/v1/messages` | gateway → provider | your provider |
| `/brain/compact`, `/brain/prewarm`, `/brain/session-end` | gateway (no-op) | — |
| `/me/plan` | real plan, `billing_enforcement` rewritten to `off` | — |
| `/runtime/model-policy` | gateway | — |
| everything else (auth, memory, app-config, integrations, voice, dictation, search, billing portal) | proxied to `api.heyclicky.com` | HeyClicky |

Voice transcription (`/brain/ears/*`), speech (`/agent/realtime/*`), image search and the browser
host still use HeyClicky's servers, because they are server-mediated services, not model calls.

## Config

`gateway/config.json` (created on first run):

```jsonc
{
  "providers": {
    "opencode":   { "api": "responses", "baseUrl": "https://opencode.ai/inference", "plan": "payg", "key": "oc_sk_..." },
    "openrouter": { "api": "chat", "baseUrl": "https://openrouter.ai/api/v1", "key": "sk-or-..." },
    "custom":     { "api": "chat", "baseUrl": "", "key": "" }
    // ...all presets are filled in automatically
  },
  "brain":  { "provider": "opencode", "model": "gpt-6.1-sol", "effort": "low",
              "screenshots": true,
              "tools": ["open_url", "copy_to_clipboard", "insert_text"] },
  "agent":  { "provider": "opencode", "model": "gpt-6.1-sol", "effort": "medium" },
  "proxy":  { "baseUrl": "https://api.heyclicky.com", "planEnforcement": "off" }
}
```

- `brain.provider` / `agent.provider` pick the preset; `*.model` is passed through verbatim.
- `opencode.plan: "go"` switches the base URL to `https://opencode.ai/inference/go`.
- `proxy.planEnforcement: "on"` keeps the app's free-tier gate; `"off"` disables only the *local*
  block for free plans (inference is not reaching HeyClicky anyway).

## Files

```
gateway/server.mjs     the loopback Worker: routing, plan, passthrough
gateway/brain.mjs      /brain/turn -> Responses request + system prompt
gateway/translate.mjs  Responses <-> Chat <-> Messages, streaming and JSON
lib/config.mjs         paths, provider presets, config load/save
bin/clicky.mjs         install / on / off / status / doctor / providers / use / models / key / test / menu / logs
opencode/commands/*.md the slash commands installed into ~/.config/opencode/commands
report/                the full reverse-engineering report
```

The in-depth write-up is `report/HEYCLICKY-AI-ARCHITECTURE.md`.
