using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000154 RID: 340
  public static class ClickyInkItemsRules
  {
    // Token: 0x06001500 RID: 5376 RVA: 0x003B2CE8 File Offset: 0x003B2CE8
    [NullableContext(1)]
    public unsafe static List<string> Buttons(ClickyInkDocument.Block block)
    {
      List<string> list = ClickyInkChoices.Of(block["actions"] ?? block["buttons"]);
      IEnumerable<string> enumerable;
      if (list.Count != 0)
      {
        enumerable = list;
      }
      else
      {
        int num = 2;
        List<string> list2 = new List<string>(num);
        CollectionsMarshal.SetCount<string>(list2, num);
        enumerable = list2;
        Span<string> span = CollectionsMarshal.AsSpan<string>(list2);
        int num2 = 0;
        *span[num2] = "Go ahead";
        num2++;
        *span[num2] = "Skip";
      }
      return enumerable.Take<string>(3).ToList<string>();
    }
  }
}
