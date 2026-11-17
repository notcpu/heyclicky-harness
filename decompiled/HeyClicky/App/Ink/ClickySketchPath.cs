using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001EA RID: 490
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickySketchPath
  {
    // Token: 0x06001835 RID: 6197 RVA: 0x005C96C0 File Offset: 0x005C96C0
    public static List<Point> RoundedRectPoints(Rect rect, double radius, double step = 7.0)
    {
      ClickySketchPath.<>c__DisplayClass0_0 CS$<>8__locals1;
      CS$<>8__locals1.step = step;
      CS$<>8__locals1.r = Math.Min(radius, Math.Min(rect.Width / 2.0, rect.Height / 2.0));
      CS$<>8__locals1.points = new List<Point>();
      double x = rect.X;
      double y = rect.Y;
      double width = rect.Width;
      double height = rect.Height;
      ClickySketchPath.<RoundedRectPoints>g__Edge|0_0(new Point(x + CS$<>8__locals1.r, y), new Point(x + width - CS$<>8__locals1.r, y), ref CS$<>8__locals1);
      ClickySketchPath.<RoundedRectPoints>g__Arc|0_1(new Point(x + width - CS$<>8__locals1.r, y + CS$<>8__locals1.r), -1.5707963267948966, 0.0, ref CS$<>8__locals1);
      ClickySketchPath.<RoundedRectPoints>g__Edge|0_0(new Point(x + width, y + CS$<>8__locals1.r), new Point(x + width, y + height - CS$<>8__locals1.r), ref CS$<>8__locals1);
      ClickySketchPath.<RoundedRectPoints>g__Arc|0_1(new Point(x + width - CS$<>8__locals1.r, y + height - CS$<>8__locals1.r), 0.0, 1.5707963267948966, ref CS$<>8__locals1);
      ClickySketchPath.<RoundedRectPoints>g__Edge|0_0(new Point(x + width - CS$<>8__locals1.r, y + height), new Point(x + CS$<>8__locals1.r, y + height), ref CS$<>8__locals1);
      ClickySketchPath.<RoundedRectPoints>g__Arc|0_1(new Point(x + CS$<>8__locals1.r, y + height - CS$<>8__locals1.r), 1.5707963267948966, 3.141592653589793, ref CS$<>8__locals1);
      ClickySketchPath.<RoundedRectPoints>g__Edge|0_0(new Point(x, y + height - CS$<>8__locals1.r), new Point(x, y + CS$<>8__locals1.r), ref CS$<>8__locals1);
      ClickySketchPath.<RoundedRectPoints>g__Arc|0_1(new Point(x + CS$<>8__locals1.r, y + CS$<>8__locals1.r), 3.141592653589793, 4.71238898038469, ref CS$<>8__locals1);
      return CS$<>8__locals1.points;
    }

    // Token: 0x06001836 RID: 6198 RVA: 0x005C98A0 File Offset: 0x005C98A0
    public static PaperPath Smooth(IReadOnlyList<Point> input, bool closed)
    {
      List<Point> list = input.ToList<Point>();
      if (list.Count < 2)
      {
        return new PaperPath();
      }
      if (closed)
      {
        list.AddRange(list.Take<Point>(2).ToList<Point>());
      }
      PaperPath paperPath = new PaperPath();
      paperPath.MoveTo(new Point((list[0].X + list[1].X) / 2.0, (list[0].Y + list[1].Y) / 2.0));
      for (int i = 1; i < list.Count - 1; i++)
      {
        Point point = new Point((list[i].X + list[i + 1].X) / 2.0, (list[i].Y + list[i + 1].Y) / 2.0);
        paperPath.QuadTo(point, list[i]);
      }
      if (closed)
      {
        paperPath.Close();
      }
      return paperPath;
    }

    // Token: 0x06001837 RID: 6199 RVA: 0x005C99C8 File Offset: 0x005C99C8
    public static PaperPath RoundedRect(Rect rect, double radius, long seed, double jitter = 0.7)
    {
      ClickySketchRandom random = new ClickySketchRandom(seed);
      double drift = 0.0;
      return ClickySketchPath.Smooth(ClickySketchPath.RoundedRectPoints(rect, radius, 7.0).Select<Point, Point>(delegate(Point point)
      {
        drift = drift * 0.6 + random.Uniform(-jitter, jitter) * 0.4;
        double num = point.X + drift + random.Uniform(-0.25, 0.25);
        double num2 = point.Y + drift * 0.8 + random.Uniform(-0.25, 0.25);
        return new Point(num, num2);
      }).ToList<Point>(), true);
    }

    // Token: 0x06001838 RID: 6200 RVA: 0x005C9A2C File Offset: 0x005C9A2C
    public static PaperPath Line(Point start, Point end, long seed, double jitter = 0.6, double step = 10.0)
    {
      ClickySketchRandom clickySketchRandom = new ClickySketchRandom(seed);
      int num = Math.Max(2, (int)(Math.Sqrt((end.X - start.X) * (end.X - start.X) + (end.Y - start.Y) * (end.Y - start.Y)) / step));
      List<Point> list = new List<Point>();
      for (int i = 0; i <= num; i++)
      {
        double num2 = (double)i / (double)num;
        double num3 = start.X + (end.X - start.X) * num2 + clickySketchRandom.Uniform(-0.3, 0.3);
        double num4 = start.Y + (end.Y - start.Y) * num2 + clickySketchRandom.Uniform(-jitter, jitter);
        list.Add(new Point(num3, num4));
      }
      return ClickySketchPath.Smooth(list, false).LineTo(end);
    }

    // Token: 0x06001839 RID: 6201 RVA: 0x005C9B24 File Offset: 0x005C9B24
    [CompilerGenerated]
    internal static void <RoundedRectPoints>g__Edge|0_0(Point from, Point to, ref ClickySketchPath.<>c__DisplayClass0_0 A_2)
    {
      int num = Math.Max(2, (int)(Math.Sqrt((to.X - from.X) * (to.X - from.X) + (to.Y - from.Y) * (to.Y - from.Y)) / A_2.step));
      for (int i = 0; i < num; i++)
      {
        double num2 = (double)i / (double)num;
        A_2.points.Add(new Point(from.X + (to.X - from.X) * num2, from.Y + (to.Y - from.Y) * num2));
      }
    }

    // Token: 0x0600183A RID: 6202 RVA: 0x005C9BD8 File Offset: 0x005C9BD8
    [CompilerGenerated]
    internal static void <RoundedRectPoints>g__Arc|0_1(Point center, double start, double end, ref ClickySketchPath.<>c__DisplayClass0_0 A_3)
    {
      int num = Math.Max(2, (int)(Math.Abs(end - start) * A_3.r / A_3.step) + 1);
      for (int i = 0; i < num; i++)
      {
        double num2 = start + (end - start) * (double)i / (double)num;
        A_3.points.Add(new Point(center.X + A_3.r * Math.Cos(num2), center.Y + A_3.r * Math.Sin(num2)));
      }
    }
  }
}
