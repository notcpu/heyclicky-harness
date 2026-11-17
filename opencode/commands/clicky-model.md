---
description: Choose the provider/model HeyClicky uses through the gateway
---

Arguments: $ARGUMENTS

If the arguments are empty, first run this and ask which provider and model to use:

!`node C:/Users/notcpu/Desktop/heyclicky-re/bin/clicky.mjs providers`
!`node C:/Users/notcpu/Desktop/heyclicky-re/bin/clicky.mjs models`

Otherwise treat the first argument as the provider and the rest as the model, then run:

```text
node C:/Users/notcpu/Desktop/heyclicky-re/bin/clicky.mjs use $ARGUMENTS
node C:/Users/notcpu/Desktop/heyclicky-re/bin/clicky.mjs off
node C:/Users/notcpu/Desktop/heyclicky-re/bin/clicky.mjs on
```

If only a model id is given (no provider), use the current provider:

```text
node C:/Users/notcpu/Desktop/heyclicky-re/bin/clicky.mjs model $ARGUMENTS
node C:/Users/notcpu/Desktop/heyclicky-re/bin/clicky.mjs off
node C:/Users/notcpu/Desktop/heyclicky-re/bin/clicky.mjs on
```

Report the provider + model now in use in one line.
