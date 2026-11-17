using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000204 RID: 516
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PaperDrawing : Canvas
  {
    // Token: 0x060018B7 RID: 6327 RVA: 0x005CD674 File Offset: 0x005CD674
    public PaperDrawing()
    {
      base.IsHitTestVisible = false;
    }

    // Token: 0x060018B8 RID: 6328 RVA: 0x005CD684 File Offset: 0x005CD684
    public PaperDrawing Fill(PaperPath p, Color color)
    {
      return this.Fill(p, Ui.Brush(color));
    }

    // Token: 0x060018B9 RID: 6329 RVA: 0x005CD694 File Offset: 0x005CD694
    public PaperDrawing Fill(PaperPath p, Brush brush)
    {
      base.Children.Add(new Path
      {
        Data = p.ToGeometry(),
        Fill = brush,
        IsHitTestVisible = false
      });
      return this;
    }

    // Token: 0x060018BA RID: 6330 RVA: 0x005CD6C4 File Offset: 0x005CD6C4
    public PaperDrawing Stroke(PaperPath p, PaperStroke s)
    {
      Path path = new Path
      {
        Data = p.ToGeometry(),
        Stroke = Ui.Brush(s.Color),
        StrokeThickness = s.Width,
        StrokeLineJoin = s.Join,
        StrokeStartLineCap = s.Cap,
        StrokeEndLineCap = s.Cap,
        StrokeDashCap = s.Cap,
        IsHitTestVisible = false
      };
      double[] dash = s.Dash;
      if (dash != null)
      {
        DoubleCollection doubleCollection = new DoubleCollection();
        foreach (double num in dash)
        {
          doubleCollection.Add(num / Math.Max(s.Width, 0.0001));
        }
        path.StrokeDashArray = doubleCollection;
      }
      base.Children.Add(path);
      return this;
    }

    // Token: 0x060018BB RID: 6331 RVA: 0x005CD794 File Offset: 0x005CD794
    public PaperDrawing Sized(double width, double height)
    {
      base.Width = width;
      base.Height = height;
      return this;
    }
  }
}
