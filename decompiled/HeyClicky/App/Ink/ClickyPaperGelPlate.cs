using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001D2 RID: 466
  public sealed class ClickyPaperGelPlate : Grid
  {
    // Token: 0x060017D8 RID: 6104 RVA: 0x005C714C File Offset: 0x005C714C
    public ClickyPaperGelPlate(ClickyPaperGel gel = ClickyPaperGel.Blue, bool showsShadow = true)
    {
      base.IsHitTestVisible = false;
      PaperSurface paperSurface = new PaperSurface(ClickyShape.Capsule, PaperFill.Vertical(gel.Stops()), null).WithWash(PaperFill.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, PaperColor.White.Opacity(0.88)),
        new ValueTuple<double, Color>(0.46, PaperColor.White.Opacity(0.0))
      }), null, true).WithOutline(new PaperStroke(gel.Rim(), 1.0, PenLineCap.Flat, PenLineJoin.Miter, null), false);
      if (showsShadow)
      {
        paperSurface.WithShadow(PaperColor.Hex("#141E3C").Opacity(0.16), 3.0, 0.0, 3.0);
      }
      base.Children.Add(paperSurface);
    }
  }
}
