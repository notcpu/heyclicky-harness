// The cursor brain: HeyClicky's /brain/turn contract, reconstructed.
//
// HeyClicky posts one JSON body per spoken turn and reads an SSE stream back.
// The Worker normally supplies the system prompt and the model. The gateway
// supplies both: a prompt written against the client's drawing engine
// (Assets/Engine/clicky-draw-engine.js) and the model configured in
// gateway/config.json.
//
// Wire shapes below match HeyClicky.Core:
//   CompanionSession.WriteTurnBody     -> request body
//   BrainSseParser                     -> "data:" lines, {"type":"text"} chunks
//   BrainReplyAccumulator.OnEvent      -> meta / items / status / tool_calls /
//                                         computer_calls / error / done
//   BrainToolCall.FromEvent            -> {call_id, name, arguments, async}

export const PROGRAM_INSTRUCTIONS = `You are Clicky: a small, warm, slightly cheeky character who lives on the user's screen. You talk out loud while you draw on top of whatever they are looking at.

# Output format (strict)
Reply with exactly one fenced code block and nothing else:

\`\`\`js
say("short spoken line");
say("optional second line");
step(1);
// drawing calls
\`\`\`

Anything outside a fenced block is thrown away, so never put words there. Keep the program small: 1-3 say() lines and usually 8-40 drawing calls. Every say() line is spoken aloud, so write the way people talk: no markdown, no lists, no URLs, no emoji.

# Coordinate space
The canvas is W pixels wide and H pixels tall (they are supplied in each turn). Origin is top-left. Place drawings on the calm part of the screen (not on top of the thing the user is reading), and leave ~24px margins.

# Drawing API (all calls are top-level, no imports)
say(text)                       // speak this line; the strokes that follow are paced to it
step(n)                         // start a new beat (optional)
line(points, opts)              // polyline: [[x,y],...]
curve(points, opts)             // smooth polyline
rect(x, y, w, h, opts)
circle(x, y, r, opts)
ellipse(x, y, rx, ry, opts)
dot(x, y, r, opts)
poly(points, opts)              // closed shape
path(svgPathString, opts)       // raw SVG path
arrow(points, opts)             // line with an arrow head
highlight(x, y, w, h, opts)     // marker swash behind text
text(x, y, string, opts)        // handwritten text; opts.size, opts.anchor:'middle'|'end', opts.color, opts.weight
image(x, y, w, h, {query})      // a real photo the app searches for
molecule(smiles, x, y, w, h)    // chemical structure
seen("what the screen shows them doing")   // silent note, draws nothing
noted("a durable fact about the user")     // silent note, draws nothing

Helpers: W, H, PI, sin, cos, abs, min, max, sqrt, round, floor, ceil, pow, atan2, hypot, range(n), lerp(a,b,t), polar(cx,cy,r,a), add/sub/mul(a,b), mid(a,b), along(a,b,t), dist(a,b), angle(a,b), perp(a,b), rotate(p,center,angle).
Colours: ink, white, grey, butter, sun, orange, peach, coral, red, pink, lilac, violet, sky, blue, mint, leaf, brown, tan, cream, or a hex string.

# Style
Hand-drawn, confident, minimal. Prefer a few well-placed strokes and one clear label over a dense diagram. Write short labels (1-4 words). Draw arrows that start from the thing you name. Keep line lengths deliberate, not scribbly. Never draw a card or a big panel unless the user asked for one.

# Tools
You also have real tools. Call them when the user asks for the action; never pretend in text.
- open_url {url}: open a link in the default browser.
- copy_to_clipboard {text}: put text on the clipboard.
- insert_text {text}: type text into the field in front of the user. Only when they clearly want typing.
If you call a tool, also say one short line about it in the program. Do not invent tool names. Do not claim an action happened unless you called the tool.

# Behaviour
Be brief and useful. Answer first, decorate second. If the request is emotional, drop the drawing and just say something kind in one say() line. If you need the screen, look at the attached screenshot; never describe something the screenshot does not show. If the request is ambiguous, ask one short question in say() and draw nothing.`;

const TOOL_SCHEMAS = {
  open_url: {
    type: "function",
    name: "open_url",
    description: "Open a URL in the user's default browser.",
    parameters: {
      type: "object",
      properties: { url: { type: "string", description: "Absolute http(s) URL." } },
      required: ["url"],
      additionalProperties: false,
    },
    strict: false,
  },
  copy_to_clipboard: {
    type: "function",
    name: "copy_to_clipboard",
    description: "Copy text to the user's clipboard.",
    parameters: {
      type: "object",
      properties: { text: { type: "string" } },
      required: ["text"],
      additionalProperties: false,
    },
    strict: false,
  },
  insert_text: {
    type: "function",
    name: "insert_text",
    description: "Type text into the focused field in the frontmost app.",
    parameters: {
      type: "object",
      properties: { text: { type: "string" } },
      required: ["text"],
      additionalProperties: false,
    },
    strict: false,
  },
};

export function toolsFor(names = []) {
  return names.map((n) => TOOL_SCHEMAS[n]).filter(Boolean);
}

/** Trim a long string without cutting a UTF-16 surrogate in half. */
function clip(value, max) {
  const s = typeof value === "string" ? value : String(value ?? "");
  if (s.length <= max) return s;
  return s.slice(0, max - 1).replace(/[\uD800-\uDBFF]$/, "") + "…";
}

/** Remove image blocks from a Responses item (text-only models). */
function withoutImages(item) {
  if (!item || typeof item !== "object") return item;
  if (Array.isArray(item.content)) {
    return {
      ...item,
      content: item.content.filter(
        (block) => block?.type !== "input_image" && block?.type !== "image_url" && block?.type !== "image",
      ),
    };
  }
  return item;
}

/**
 * Turn HeyClicky's /brain/turn body into an OpenAI Responses request body.
 * The thread and turn_items are already Responses-shaped, so most of this is
 * passing them through with the turn context folded into one user message.
 */
export function buildBrainRequest(body, cfg, options = {}) {
  const images = options.images !== false;
  const turn = body.turn ?? {};
  const shot = turn.screenshot;
  const width = shot?.widthInPixels || 1512;
  const height = shot?.heightInPixels || 982;

  const input = [];

  // 1. Conversation so far (messages, function calls, screenshots).
  for (const item of body.thread ?? []) input.push(images ? item : withoutImages(item));

  // 2. This turn: context block + screenshot + the actual words.
  const context = [];
  if (images) {
    context.push(`SCREEN: ${width}x${height}`);
  } else {
    context.push(
      "SCREEN: no screenshot attached (the selected model is text-only). Do not claim to see the screen or its contents. You can still speak and draw, and you may ask the user to describe what they see.",
    );
  }
  if (turn.live_state) context.push(`STATE: ${turn.live_state}`);
  if (turn.front_app) context.push(`FRONT APP: ${turn.front_app}${turn.front_app_bundle_id ? ` (${turn.front_app_bundle_id})` : ""}`);
  if (turn.time_label) context.push(`TIME: ${turn.time_label}`);
  if (turn.fact) context.push(`FACT: ${clip(turn.fact, 600)}`);
  if (turn.stable_note) context.push(`NOTE: ${clip(turn.stable_note, 600)}`);
  if (turn.drawing_on_screen) context.push(`ALREADY ON SCREEN (drawing code):\n${clip(turn.drawing_on_screen, 1800)}`);
  if (turn.highlight?.text) context.push(`HIGHLIGHTED (${turn.highlight.gesture}): ${clip(turn.highlight.text, 800)}`);
  if (turn.page) {
    const p = turn.page;
    const bits = [];
    if (p.url) bits.push(`url: ${p.url}`);
    if (p.title) bits.push(`title: ${p.title}`);
    if (p.text) bits.push(`text: ${clip(p.text, 2500)}`);
    if (bits.length) context.push(`PAGE:\n${bits.join("\n")}`);
  }
  if (turn.flow_brief) context.push(`FLOW: ${clip(JSON.stringify(turn.flow_brief), 1200)}`);
  if (turn.trigger) context.push(`TRIGGER: ${turn.trigger}`);
  if (body.app_reads) context.push("APP READS: on (you may refer to connected apps if asked)");
  if (body.drive || body.takeover) {
    context.push("DRIVE: the user asked you to take over. This gateway does not support computer control; say so briefly and offer to draw or explain instead.");
  }
  if (turn.walkthrough_step != null) context.push(`WALKTHROUGH STEP: ${turn.walkthrough_step}`);

  const content = [{ type: "input_text", text: `<turn>\n${context.join("\n")}\n</turn>` }];
  if (images && shot?.base64) {
    content.push({
      type: "input_image",
      image_url: shot.base64.startsWith("data:") ? shot.base64 : `data:${shot.mimeType || "image/jpeg"};base64,${shot.base64}`,
      detail: "original",
    });
  }
  for (const extra of images ? turn.other_screens ?? [] : []) {
    if (!extra?.base64) continue;
    content.push({
      type: "input_image",
      image_url: extra.base64.startsWith("data:") ? extra.base64 : `data:${extra.mimeType || "image/jpeg"};base64,${extra.base64}`,
      detail: "high",
    });
  }
  for (const img of images ? turn.attached_images ?? [] : []) {
    if (!img?.base64) continue;
    content.push({
      type: "input_image",
      image_url: img.base64.startsWith("data:") ? img.base64 : `data:${img.mimeType || "image/jpeg"};base64,${img.base64}`,
      detail: "high",
    });
  }
  content.push({ type: "input_text", text: turn.words || "(no words — the user is silent)" });
  input.push({ type: "message", role: "user", content });

  // 3. Tool outputs already produced in this turn (function_call_output,
  //    computer_call_output, assistant messages from earlier rounds).
  for (const item of body.turn_items ?? []) input.push(images ? item : withoutImages(item));

  const req = {
    model: cfg.brain.model,
    instructions: PROGRAM_INSTRUCTIONS,
    input,
    stream: true,
    max_output_tokens: cfg.brain.maxOutputTokens,
    parallel_tool_calls: false,
  };
  const tools = toolsFor(cfg.brain.tools);
  if (tools.length) {
    req.tools = tools;
    req.tool_choice = "auto";
  }
  if (cfg.brain.effort) req.reasoning = { effort: cfg.brain.effort };
  return { request: req, width, height };
}
