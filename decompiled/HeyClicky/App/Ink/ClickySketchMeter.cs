using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001FD RID: 509
  public sealed class ClickySketchMeter : Grid
  {
    // Token: 0x06001867 RID: 6247 RVA: 0x005CB624 File Offset: 0x005CB624
    [NullableContext(1)]
    public ClickySketchMeter(IReadOnlyList<double> levels, long seed = 15L)
    {
      base.Height = 36.0;
      base.IsHitTestVisible = false;
      PaperDrawing drawing = new PaperDrawing();
      base.Children.Add(drawing);
      base.SizeChanged += delegate(object _, SizeChangedEventArgs e)
      {
        drawing.Children.Clear();
        double num = e.NewSize.Width / (double)Math.Max(levels.Count, 1);
        for (int i = 0; i < levels.Count; i++)
        {
          double num2 = levels[i];
          double num3 = 6.0 + Math.Min(Math.Max(num2, 0.0), 1.0) * 26.0;
          drawing.Fill(ClickySketchPath.RoundedRect(new Rect((double)i * num + 2.0, 34.0 - num3, num - 4.0, num3), 1.5, seed + (long)i, 0.25), (num2 > 0.15) ? PaperColor.Hex("#8EB2DD") : PaperColor.Hex("#DCE6F2"));
        }
      };
    }
  }
}
