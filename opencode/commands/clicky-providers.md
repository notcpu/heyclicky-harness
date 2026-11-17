---
description: List HeyClicky gateway providers and their models
---

!`node C:/Users/notcpu/Desktop/heyclicky-re/bin/clicky.mjs providers`

Then list the models the current provider actually serves:

!`node C:/Users/notcpu/Desktop/heyclicky-re/bin/clicky.mjs models`

Tell the user, in at most five lines, which provider is active for the brain and agent lanes, and the exact command to switch (for example `node bin/clicky.mjs use openrouter <model>`). Add `--brain` or `--agent` to switch one lane only. Do not run other tools.
