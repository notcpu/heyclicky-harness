using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001AC RID: 428
  public sealed class ClickyEdgeStackInkPaper : Grid
  {
    // Token: 0x060016FD RID: 5885 RVA: 0x005BE60C File Offset: 0x005BE60C
    public ClickyEdgeStackInkPaper(ClickyHomeSpreadPaper paper, bool isJob, bool isLifted = false, double cornerRadius = 9.0)
    {
      base.IsHitTestVisible = false;
      IClickyShape clickyShape;
      if (!isJob)
      {
        clickyShape = ClickyShape.RoundedRectangle(cornerRadius);
      }
      else
      {
        IClickyShape clickyShape2 = new ClickyNoteShape(true, 10.0, 2.7, 0.8, 3.0);
        clickyShape = clickyShape2;
      }
      PaperSurface paperSurface = new PaperSurface(clickyShape, paper.Fill, new double?(0.45)).WithOutline(new PaperStroke(PaperColor.Black.Opacity(0.05), 0.5, PenLineCap.Flat, PenLineJoin.Miter, null), false).WithShadow(PaperColor.Hex("#0A101E").Opacity(0.16), 0.5, 0.0, 1.0).WithShadow(PaperColor.Hex("#0A1428").Opacity(isLifted ? 0.32 : 0.15), (double)(isLifted ? 22 : 6), 0.0, (double)(isLifted ? 16 : 7));
      if (isJob)
      {
        paperSurface.WithWash(PaperFill.Evenly(new Point(0.5, 0.0), new Point(0.5, 1.0), new Color[]
        {
          PaperColor.Hex("#78643C").Opacity(0.08),
          PaperColor.Clear
        }), (Size s) => new Rect(0.0, 0.0, s.Width, 7.0), true);
      }
      base.Children.Add(paperSurface);
    }
  }
}
