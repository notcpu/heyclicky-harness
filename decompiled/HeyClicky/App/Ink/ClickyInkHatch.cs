using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000223 RID: 547
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkHatch
  {
    // Token: 0x0600198D RID: 6541 RVA: 0x005D356C File Offset: 0x005D356C
    public static FrameworkElement Create(Color color, double progress = 1.0, double spacing = 2.6, double lineWidth = 1.5, int seed = 1, double from = 0.0, double? width = null, double? height = null, double? circleInset = null)
    {
      return new ClickyInkDashboardCanvas(delegate(PaperDrawing drawing, Size size)
      {
        ClickyInkHatch.Draw(drawing, size, color, progress, spacing, lineWidth, seed, from, circleInset, 0.0, 0.0);
      }, width, height, true);
    }

    // Token: 0x0600198E RID: 6542 RVA: 0x005D35C8 File Offset: 0x005D35C8
    public static void Draw(PaperDrawing drawing, Size size, Color color, double progress = 1.0, double spacing = 2.6, double lineWidth = 1.5, int seed = 1, double from = 0.0, double? circleInset = null, double offsetX = 0.0, double offsetY = 0.0)
    {
      ClickyPaperRandom clickyPaperRandom = new ClickyPaperRandom((ulong)((long)(seed + 41)));
      double num = size.Height * 0.55;
      double num2 = size.Width + num;
      int num3 = Math.Max(1, (int)(num2 / spacing));
      double num4 = (double)num3 * progress;
      Point point = new Point(size.Width / 2.0, size.Height / 2.0);
      double num5 = Math.Min(size.Width, size.Height) / 2.0 - circleInset.GetValueOrDefault();
      for (int i = 0; i < num3; i++)
      {
        double num6 = (double)i / (double)num3;
        double num7 = clickyPaperRandom.Next(-0.7, 0.7);
        double num8 = clickyPaperRandom.Next(-0.7, 0.7);
        double num9 = clickyPaperRandom.Next(0.62, 0.95);
        if (num6 >= from && (double)i < num4)
        {
          double num10 = (double)i * spacing - num;
          Point point2 = new Point(num10 + num7, size.Height + 1.0);
          Point point3 = new Point(num10 + num + num8, -1.0);
          double num11 = Math.Min(1.0, num4 - (double)i);
          if (num11 < 1.0)
          {
            point3 = new Point(point2.X + (point3.X - point2.X) * num11, point2.Y + (point3.Y - point2.Y) * num11);
          }
          bool flag = false;
          bool flag2 = false;
          if (circleInset == null || ClickyInkHatch.ClipToCircle(ref point2, ref point3, point, num5, out flag, out flag2))
          {
            PaperPath paperPath = new PaperPath().MoveTo(point2.X + offsetX, point2.Y + offsetY).LineTo(point3.X + offsetX, point3.Y + offsetY);
            drawing.Children.Add(new Path
            {
              Data = paperPath.ToGeometry(),
              Stroke = Ui.Brush(color.Opacity(num9)),
              StrokeThickness = lineWidth,
              StrokeStartLineCap = (flag ? PenLineCap.Flat : PenLineCap.Round),
              StrokeEndLineCap = (flag2 ? PenLineCap.Flat : PenLineCap.Round),
              IsHitTestVisible = false
            });
          }
        }
      }
    }

    // Token: 0x0600198F RID: 6543 RVA: 0x005D3834 File Offset: 0x005D3834
    private static bool ClipToCircle(ref Point a, ref Point b, Point c, double r, out bool startClipped, out bool endClipped)
    {
      startClipped = (endClipped = false);
      if (r <= 0.0)
      {
        return false;
      }
      double num = b.X - a.X;
      double num2 = b.Y - a.Y;
      double num3 = a.X - c.X;
      double num4 = a.Y - c.Y;
      double num5 = num * num + num2 * num2;
      double num6 = 2.0 * (num3 * num + num4 * num2);
      double num7 = num3 * num3 + num4 * num4 - r * r;
      if (num5 <= 0.0)
      {
        return num7 <= 0.0;
      }
      double num8 = num6 * num6 - 4.0 * num5 * num7;
      if (num8 <= 0.0)
      {
        return false;
      }
      double num9 = Math.Sqrt(num8);
      double num10 = (-num6 - num9) / (2.0 * num5);
      double num11 = (-num6 + num9) / (2.0 * num5);
      double num12 = Math.Max(0.0, num10);
      double num13 = Math.Min(1.0, num11);
      if (num13 <= num12)
      {
        return false;
      }
      startClipped = num10 > 0.0;
      endClipped = num11 < 1.0;
      Point point = a;
      a = new Point(point.X + num * num12, point.Y + num2 * num12);
      b = new Point(point.X + num * num13, point.Y + num2 * num13);
      return true;
    }
  }
}
