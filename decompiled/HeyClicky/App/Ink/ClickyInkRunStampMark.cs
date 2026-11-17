using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000238 RID: 568
  public static class ClickyInkRunStampMark
  {
    // Token: 0x060019BA RID: 6586 RVA: 0x005D7904 File Offset: 0x005D7904
    [NullableContext(1)]
    public static FrameworkElement Create([Nullable(2)] string number, ClickyInkDocument.Block block, double clock, double landsAt, double fontSize = 11.0)
    {
      double? num = ClickyInkChange.ChangedAt(block);
      double num2;
      if (num != null)
      {
        double valueOrDefault = num.GetValueOrDefault();
        if (ClickyInkChange.Previous(block, "n") != null || ClickyInkChange.Previous(block, "run") != null)
        {
          num2 = valueOrDefault;
          goto IL_0036;
        }
      }
      num2 = landsAt;
      IL_0036:
      double num3 = num2;
      double num4 = ClickyInkClock.Progress(clock, num3 + 0.1, num3 + 0.4);
      Grid grid = new Grid();
      grid.Children.Add(new ClickyPaperStamp(ClickyThingStatus.Watching, (number == null) ? "KEPT" : ("RUN " + number), fontSize, -5.0));
      grid.Transformed(0.0, 0.0, 0.0, 1.0 + 0.35 * (1.0 - num4), null);
      grid.Opacity = ((num4 > 0.0) ? (0.3 + 0.7 * num4) : 0.0);
      return grid;
    }
  }
}
