using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000155 RID: 341
  public static class ClickyInkCriteriaRules
  {
    // Token: 0x06001501 RID: 5377 RVA: 0x003B2D64 File Offset: 0x003B2D64
    [NullableContext(1)]
    public unsafe static List<string> Values([Nullable(2)] string text)
    {
      string text2 = ClickyInkNoteActions.Plain(text ?? "");
      List<string> list = (from v in text2.Contains('|') ? text2.Split('|', StringSplitOptions.None) : text2.Split(',', StringSplitOptions.None)
        select v.TrimWs() into v
        where v.Length > 0
        select v).ToList<string>();
      if (list.Count != 0)
      {
        return list;
      }
      if (text2.Length != 0)
      {
        int num = 1;
        List<string> list2 = new List<string>(num);
        CollectionsMarshal.SetCount<string>(list2, num);
        Span<string> span = CollectionsMarshal.AsSpan<string>(list2);
        int num2 = 0;
        *span[num2] = text2;
        return list2;
      }
      return new List<string>();
    }
  }
}
