using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000228 RID: 552
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkSparkLine
  {
    // Token: 0x06001998 RID: 6552 RVA: 0x005D43B8 File Offset: 0x005D43B8
    public static FrameworkElement Create(IReadOnlyList<double> values, [Nullable(2)] IReadOnlyList<double> previous = null, double progress = 1.0, double appear = 1.0, double? goal = null, [Nullable(2)] ClickyInkStyle style = null, Color? color = null, int seed = 1, bool marksNew = true, double? width = null, double? height = null)
    {
      ClickyInkStyle inkStyle = style ?? new ClickyInkStyle(ClickyInkDashboardPalette.Pencil, ClickyInkDashboardPalette.Pencil.Opacity(0.58), null, null);
      return new ClickyInkDashboardCanvas(delegate(PaperDrawing drawing, Size size)
      {
        ClickyInkSparkLine.Draw(drawing, size, values, previous, progress, appear, goal, inkStyle, color, seed, marksNew);
      }, width, height, true);
    }

    // Token: 0x06001999 RID: 6553 RVA: 0x005D4458 File Offset: 0x005D4458
    private static void Draw(PaperDrawing drawing, Size size, IReadOnlyList<double> values, [Nullable(2)] IReadOnlyList<double> previous, double progress, double appear, double? goal, ClickyInkStyle style, Color? color, int seed, bool marksNew)
    {
      if (values.Count == 0)
      {
        drawing.Stroke(new PaperPath().MoveTo(0.0, size.Height * 0.7).LineTo(size.Width, size.Height * 0.7), new PaperStroke(style.Quiet.Opacity(0.5), 1.0, PenLineCap.Flat, PenLineJoin.Miter, new double[] { 2.0, 4.0 }));
        return;
      }
      ClickyPaperRandom clickyPaperRandom = new ClickyPaperRandom((ulong)((long)(seed + 13)));
      object obj;
      if (goal != null)
      {
        double valueOrDefault = goal.GetValueOrDefault();
        obj = values.Append(valueOrDefault).ToList<double>();
      }
      else
      {
        obj = values.ToList<double>();
      }
      object obj2 = obj;
      double num = obj2.Min();
      double num2 = obj2.Max() - num;
      double num3 = size.Width - 3.0 - 7.0;
      double num4 = size.Height - 5.0 - 5.0;
      List<Point> list = new List<Point>(values.Count);
      for (int i = 0; i < values.Count; i++)
      {
        double num5 = ((values.Count == 1) ? (3.0 + num3) : (3.0 + num3 * (double)i / (double)(values.Count - 1)));
        double num6 = ((num2 == 0.0) ? (5.0 + num4 / 2.0) : (5.0 + num4 * (1.0 - (values[i] - num) / num2)));
        double num7 = clickyPaperRandom.Next(-0.4, 0.4);
        double num8 = clickyPaperRandom.Next(-0.4, 0.4);
        list.Add(new Point(num5 + num7, num6 + num8));
      }
      ClickyInkSeries.Change? change = ((previous == null) ? null : ClickyInkSeries.ChangeBetween(previous, values));
      int num9 = ((progress < 1.0) ? ((change != null) ? change.GetValueOrDefault().Added : 0) : 0);
      double num10 = Math.Min((double)Math.Max(0, list.Count - 1 - num9) + (double)num9 * progress, (double)(list.Count - 1) * appear);
      if (goal != null)
      {
        double valueOrDefault2 = goal.GetValueOrDefault();
        if (num2 > 0.0)
        {
          double num11 = 5.0 + num4 * (1.0 - (valueOrDefault2 - num) / num2);
          drawing.Stroke(new PaperPath().MoveTo(0.0, num11).LineTo(size.Width, num11), new PaperStroke(style.Quiet.Opacity(0.55), 1.0, PenLineCap.Flat, PenLineJoin.Miter, new double[] { 3.0, 3.0 }));
        }
      }
      Color color2 = color ?? ClickyInkDashboardPalette.Pencil;
      if (list.Count == 1)
      {
        drawing.Fill(new PaperPath().AddEllipse(new Rect(list[0].X - 3.0, list[0].Y - 3.0, 6.0, 6.0)), color2);
        return;
      }
      for (int j = 0; j < list.Count - 1; j++)
      {
        double num12 = Math.Min(1.0, num10 - (double)j);
        if (num12 <= 0.0)
        {
          break;
        }
        Point point = list[j];
        Point point2 = list[j + 1];
        Point point3 = new Point(point.X + (point2.X - point.X) * num12, point.Y + (point2.Y - point.Y) * num12);
        bool flag = marksNew && change != null && j >= list.Count - 1 - change.Value.Added;
        drawing.Stroke(new PaperPath().MoveTo(point).LineTo(point3), PaperStroke.Round(flag ? style.Fresh : color2.Opacity(0.85), 1.6));
      }
      bool flag2 = change != null && marksNew;
      for (int k = 0; k < list.Count; k++)
      {
        if ((double)k <= num10 + 0.001)
        {
          Point point4 = list[k];
          bool flag3 = k == list.Count - 1;
          double num13 = (flag3 ? 2.8 : 1.4);
          drawing.Fill(new PaperPath().AddEllipse(new Rect(point4.X - num13, point4.Y - num13, num13 * 2.0, num13 * 2.0)), flag3 ? (flag2 ? style.Fresh : color2) : color2.Opacity(0.55));
          if (flag3)
          {
            Rect rect = new Rect(point4.X - 6.0, point4.Y - 6.0, 12.0, 12.0);
            drawing.Stroke(new ClickyHandRing(seed, 0.16).PathIn(rect), new PaperStroke((flag2 ? style.Fresh : color2).Opacity(0.7), 1.1, PenLineCap.Round, PenLineJoin.Miter, new double[] { 2.2, 1.8 }));
          }
        }
      }
    }
  }
}
