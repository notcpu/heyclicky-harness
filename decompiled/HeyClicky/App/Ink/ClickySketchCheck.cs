using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001F9 RID: 505
  public sealed class ClickySketchCheck : Grid
  {
    // Token: 0x0600185C RID: 6236 RVA: 0x005CAC2C File Offset: 0x005CAC2C
    public ClickySketchCheck(Color? color = null, double scale = 1.0)
    {
      base.IsHitTestVisible = false;
      base.Width = 22.0 * scale;
      base.Height = 18.0 * scale;
      PaperPath paperPath = new PaperPath().MoveTo(2.0, 9.5).CubicTo(new Point(8.0, 15.5), new Point(4f, 11f), new Point(6.0, 13.5)).CubicTo(new Point(20f, 2f), new Point(11f, 10f), new Point(15f, 5f))
        .Transformed(scale, scale, 0.0, 0.0);
      base.Children.Add(new PaperDrawing().Stroke(paperPath, new PaperStroke(color ?? ClickySketchInk.TealLine, 2.6 * scale, PenLineCap.Round, PenLineJoin.Round, null)));
    }
  }
}
