using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000150 RID: 336
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkRowMotionGroups
  {
    // Token: 0x060014F4 RID: 5364 RVA: 0x003B23E8 File Offset: 0x003B23E8
    public static List<ClickyInkGroup> Groups(ClickyInkLiveNote.Page page, double settled, ClickyInkRowMotion.Table table)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
      defaultInterpolatedStringHandler.AppendFormatted(table.Key);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(page.Id);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(InkText.DoubleDescription(settled));
      string text = defaultInterpolatedStringHandler.ToStringAndClear();
      Dictionary<string, List<ClickyInkGroup>> restingGroups = ClickyInkRowMotionGroups.RestingGroups;
      List<ClickyInkGroup> list2;
      lock (restingGroups)
      {
        List<ClickyInkGroup> list;
        if (ClickyInkRowMotionGroups.RestingGroups.TryGetValue(text, out list))
        {
          list2 = list;
        }
        else
        {
          List<ClickyInkGroup> list3 = ClickyInkGroup.Groups(page, settled);
          if (ClickyInkRowMotionGroups.RestingGroups.Count > 200)
          {
            ClickyInkRowMotionGroups.RestingGroups.Clear();
          }
          ClickyInkRowMotionGroups.RestingGroups[text] = list3;
          list2 = list3;
        }
      }
      return list2;
    }

    // Token: 0x04000921 RID: 2337
    private static readonly Dictionary<string, List<ClickyInkGroup>> RestingGroups = new Dictionary<string, List<ClickyInkGroup>>();
  }
}
