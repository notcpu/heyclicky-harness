using System;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001AE RID: 430
  public sealed class ClickyPaperBoardDots : Grid
  {
    // Token: 0x060016FF RID: 5887 RVA: 0x005BE824 File Offset: 0x005BE824
    public ClickyPaperBoardDots(Color? color = null, bool followsResizing = false)
    {
      ClickyPaperBoardDots.<>c__DisplayClass3_0 CS$<>8__locals1 = new ClickyPaperBoardDots.<>c__DisplayClass3_0();
      base..ctor();
      CS$<>8__locals1.<>4__this = this;
      base.IsHitTestVisible = false;
      CS$<>8__locals1.c = color ?? PaperColor.Black.Opacity(0.11);
      CS$<>8__locals1.inset = 6.4;
      if (followsResizing)
      {
        Path rows = new Path
        {
          Stroke = Ui.Brush(CS$<>8__locals1.c),
          StrokeThickness = 2.2,
          StrokeDashArray = new DoubleCollection { 0.0, 7.2727272727272725 },
          StrokeDashCap = PenLineCap.Round,
          StrokeStartLineCap = PenLineCap.Round,
          StrokeEndLineCap = PenLineCap.Round,
          IsHitTestVisible = false
        };
        base.Children.Add(new Canvas
        {
          IsHitTestVisible = false,
          Children = { rows }
        });
        Size drawn = default(Size);
        base.SizeChanged += delegate(object _, SizeChangedEventArgs e)
        {
          Size newSize = e.NewSize;
          if (newSize.Width > drawn.Width || newSize.Height > drawn.Height)
          {
            drawn = new Size(newSize.Width + 480.0, newSize.Height + 480.0);
            GeometryGroup geometryGroup = new GeometryGroup();
            for (double num = 16.0; num <= CS$<>8__locals1.<.ctor>g__Last|1(drawn.Height); num += 16.0)
            {
              geometryGroup.Children.Add(new LineGeometry
              {
                StartPoint = new Point(16.0, num),
                EndPoint = new Point(CS$<>8__locals1.<.ctor>g__Last|1(drawn.Width), num)
              });
            }
            rows.Data = geometryGroup;
          }
          CS$<>8__locals1.<>4__this.Clip = new RectangleGeometry
          {
            Rect = new Rect(0.0, 0.0, Math.Max(0.0, CS$<>8__locals1.<.ctor>g__Last|1(newSize.Width) + 1.1 + 0.5), Math.Max(0.0, CS$<>8__locals1.<.ctor>g__Last|1(newSize.Height) + 1.1 + 0.5))
          };
        };
        return;
      }
      CS$<>8__locals1.drawing = new PaperDrawing();
      base.Children.Add(CS$<>8__locals1.drawing);
      base.SizeChanged += delegate(object _, SizeChangedEventArgs e)
      {
        CS$<>8__locals1.drawing.Children.Clear();
        PaperPath paperPath = new PaperPath();
        for (double num2 = 16.0; num2 <= e.NewSize.Height - CS$<>8__locals1.inset; num2 += 16.0)
        {
          for (double num3 = 16.0; num3 <= e.NewSize.Width - CS$<>8__locals1.inset; num3 += 16.0)
          {
            paperPath.AddEllipse(new Rect(num3 - 1.1, num2 - 1.1, 2.2, 2.2));
          }
        }
        CS$<>8__locals1.drawing.Fill(paperPath, CS$<>8__locals1.c);
      };
    }

    // Token: 0x04000B42 RID: 2882
    public const double Spacing = 16.0;

    // Token: 0x04000B43 RID: 2883
    public const double Radius = 1.1;

    // Token: 0x04000B44 RID: 2884
    public const double DotOpacity = 0.11;
  }
}
