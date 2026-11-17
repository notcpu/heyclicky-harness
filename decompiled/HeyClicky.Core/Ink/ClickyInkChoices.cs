using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000152 RID: 338
  public static class ClickyInkChoices
  {
    // Token: 0x060014FC RID: 5372 RVA: 0x003B2B8C File Offset: 0x003B2B8C
    [NullableContext(1)]
    public static List<string> Of([Nullable(2)] string text)
    {
      return (from p in (text ?? "").Split('|', StringSplitOptions.None)
        select p.TrimWs() into p
        where p.Length > 0
        select p).ToList<string>();
    }
  }
}
