using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001A9 RID: 425
  public sealed class ClickyCardSparkline : Grid
  {
    // Token: 0x060016F7 RID: 5879 RVA: 0x005BE438 File Offset: 0x005BE438
    [NullableContext(1)]
    public ClickyCardSparkline(IReadOnlyList<double> values, Color ink, Color ringColor, bool circledEnd = false)
    {
      base.IsHitTestVisible = false;
      PaperDrawing drawing = new PaperDrawing();
      base.Children.Add(drawing);
      base.SizeChanged += delegate(object _, SizeChangedEventArgs e)
      {
        drawing.Children.Clear();
        Size size = e.NewSize;
        if (values.Count <= 1)
        {
          return;
        }
        double low = values.Min();
        double num = values.Max();
        double span = Math.Max(num - low, 0.0001);
        List<Point> list = values.Select<double, Point>((double value, int index) => new Point(4.0 + (double)index / (double)(values.Count - 1) * (size.Width - 8.0 - 8.0), 4.0 + (1.0 - (value - low) / span) * (size.Height - 8.0 - 6.0))).ToList<Point>();
        PaperPath paperPath = new PaperPath().MoveTo(list[0]);
        for (int i = 1; i < list.Count; i++)
        {
          Point point = list[i - 1];
          Point point2 = list[i];
          paperPath.QuadTo(new Point((point.X + point2.X) / 2.0, (point.Y + point2.Y) / 2.0), point);
          if (i == list.Count - 1)
          {
            paperPath.LineTo(point2);
          }
        }
        drawing.Stroke(paperPath, new PaperStroke(ink, 2.0, PenLineCap.Round, PenLineJoin.Round, null));
        List<Point> list2 = list;
        Point point3 = list2[list2.Count - 1];
        drawing.Fill(new PaperPath().AddEllipse(new Rect(point3.X - 3.2, point3.Y - 3.2, 6.4, 6.4)), ink);
        if (circledEnd)
        {
          drawing.Stroke(new ClickyHandRing(5, 0.18).PathIn(new Rect(point3.X - 12.0, point3.Y - 11.0, 24.0, 22.0)), new PaperStroke(ringColor, 2.0, PenLineCap.Round, PenLineJoin.Miter, null));
        }
      };
    }
  }
}
