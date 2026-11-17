using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001EC RID: 492
  public sealed class ClickySketchShapeView : Grid
  {
    // Token: 0x0600183D RID: 6205 RVA: 0x005C9C94 File Offset: 0x005C9C94
    public ClickySketchShapeView(Color fill, Color pencil, double radius = 3.0, long seed = 1L, double lineWidth = 1.3, bool isDoubled = true, bool hasShelf = false)
    {
      this.fill = fill;
      this.pencil = pencil;
      this.radius = radius;
      this.seed = seed;
      this.lineWidth = lineWidth;
      this.isDoubled = isDoubled;
      this.hasShelf = hasShelf;
      base.IsHitTestVisible = false;
      base.Children.Add(this.drawing);
      base.SizeChanged += delegate(object _, SizeChangedEventArgs e)
      {
        this.Draw(e.NewSize);
      };
    }

    // Token: 0x0600183E RID: 6206 RVA: 0x005C9D28 File Offset: 0x005C9D28
    private void Draw(Size size)
    {
      this.drawing.Children.Clear();
      PaperPath paperPath = ClickySketchPath.RoundedRect(new Rect(0.0, 0.0, size.Width, size.Height), this.radius, this.seed, 0.7);
      this.drawing.Fill(paperPath, this.fill);
      this.drawing.Stroke(paperPath, new PaperStroke(this.pencil, this.lineWidth, PenLineCap.Flat, PenLineJoin.Round, null));
      if (this.isDoubled)
      {
        this.drawing.Stroke(ClickySketchPath.RoundedRect(new Rect(0.5, 0.3, size.Width - 0.6, size.Height - 0.4), this.radius, this.seed + 17L, 0.55), new PaperStroke(this.pencil.Opacity(0.45), this.lineWidth * 0.7, PenLineCap.Flat, PenLineJoin.Round, null));
      }
      if (this.hasShelf)
      {
        this.drawing.Stroke(ClickySketchPath.Line(new Point(-2.0, size.Height + 2.2), new Point(size.Width + 2.0, size.Height + 2.4), this.seed + 3L, 0.35, 10.0), new PaperStroke(ClickySketchInk.Shelf, 2.8, PenLineCap.Round, PenLineJoin.Miter, null));
        this.drawing.Stroke(ClickySketchPath.Line(new Point(-1.0, size.Height + 5.6), new Point(size.Width + 1.0, size.Height + 5.4), this.seed + 5L, 0.3, 10.0), new PaperStroke(PaperColor.White.Opacity(0.85), 1.6, PenLineCap.Round, PenLineJoin.Miter, null));
      }
    }

    // Token: 0x04000BF1 RID: 3057
    private readonly Color fill;

    // Token: 0x04000BF2 RID: 3058
    private readonly Color pencil;

    // Token: 0x04000BF3 RID: 3059
    private readonly double radius;

    // Token: 0x04000BF4 RID: 3060
    private readonly double lineWidth;

    // Token: 0x04000BF5 RID: 3061
    private readonly long seed;

    // Token: 0x04000BF6 RID: 3062
    private readonly bool isDoubled;

    // Token: 0x04000BF7 RID: 3063
    private readonly bool hasShelf;

    // Token: 0x04000BF8 RID: 3064
    [Nullable(1)]
    private readonly PaperDrawing drawing = new PaperDrawing();
  }
}
