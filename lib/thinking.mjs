// Turn Gemini "thinking" down/off for requests going to Google's
// OpenAI-compatible endpoint (what the `google` provider preset uses).
//
// Wire format (chat/completions on generativelanguage.googleapis.com):
//   { ..., extra_body: { google: { thinking_config: { thinking_budget | thinking_level } } } }
//
// Per Google's thinking docs, "off" means different things per family:
//   2.5 Flash / Flash-Lite   thinking_budget: 0       (true off)
//   2.5 Pro                  thinking_budget: 128     (floor, cannot disable)
//   3 Flash, 3.6 Flash,      thinking_level: minimal  (closest to off)
//     3.x Flash-Lite, Robotics-ER
//   3.8 Flash, 3 Pro, ...    thinking_level: low      (minimal is rejected)
//   4.x (Argon, ...)         thinking_level: low      (cannot disable, minimal errors)
//   Gemma 4                  thinking_level: minimal  (works, zero thought tokens)
// Never send budget AND level together: that is a 400.

const GOOGLE_HOST = /generativelanguage\.googleapis\.com/i;
const LEVELS = new Set(["minimal", "low", "medium", "high"]);

export function isGoogle(provider) {
  return GOOGLE_HOST.test(provider?.baseUrl || "") || provider?.name === "google";
}

/**
 * thinking_config that gives the least thinking this model allows.
 * `override` (cfg.<lane>.thinkingOffLevel) forces a level on level-based models,
 * useful for brand-new models this table has not heard of yet.
 */
export function googleThinkingOff(model, override) {
  const id = String(model || "").toLowerCase().replace(/^models\//, "");
  const forced = LEVELS.has(String(override)) ? String(override) : null;

  if (/gemma-4/.test(id)) return { thinking_level: forced || "minimal" };

  if (/gemini-2\.5/.test(id)) {
    return { thinking_budget: /pro/.test(id) ? 128 : 0 };
  }

  const m = id.match(/gemini-(\d+)/);
  if (!m) return null; // not a Gemini model we know: leave the request alone
  if (forced) return { thinking_level: forced };

  const major = Number(m[1]);
  if (major >= 4) return { thinking_level: "low" };

  // Gemini 3.x: only some models accept "minimal".
  const minimalOk = /flash-lite|gemini-3-flash|gemini-3\.6-flash|robotics/.test(id);
  return { thinking_level: minimalOk ? "minimal" : "low" };
}

/**
 * Mutates + returns a chat/completions body. No-op unless the lane has
 * `thinking: false` and the provider is Google.
 */
export function applyThinking(body, provider, lane) {
  if (!body || typeof body !== "object") return body;
  if (lane?.thinking !== false || !isGoogle(provider)) return body;

  const tc = googleThinkingOff(body.model || lane.model, lane.thinkingOffLevel);
  if (!tc) return body;

  // Compat layer treats these as competing controls; ours wins.
  delete body.reasoning_effort;
  delete body.reasoning;

  const prev = body.extra_body && typeof body.extra_body === "object" ? body.extra_body : {};
  const prevGoogle = prev.google && typeof prev.google === "object" ? prev.google : {};
  body.extra_body = {
    ...prev,
    google: { ...prevGoogle, thinking_config: { ...(prevGoogle.thinking_config || {}), ...tc } },
  };
  // Make sure a stale budget/level from the caller can't ride along with ours.
  if ("thinking_level" in tc) delete body.extra_body.google.thinking_config.thinking_budget;
  if ("thinking_budget" in tc) delete body.extra_body.google.thinking_config.thinking_level;
  return body;
}
