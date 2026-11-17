using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200022A RID: 554
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkProgressBar
  {
    // Token: 0x0600199B RID: 6555 RVA: 0x005D4EC8 File Offset: 0x005D4EC8
    public static FrameworkElement Create(double fraction, double? previous = null, double progress = 1.0, double appear = 1.0, double height = 14.0, [Nullable(2)] ClickyInkStyle style = null, int seed = 1, double? width = null)
    {
      ClickyInkStyle inkStyle = style ?? new ClickyInkStyle(ClickyInkDashboardPalette.Pencil, ClickyInkDashboardPalette.Pencil.Opacity(0.58), null, null);
      return new ClickyInkDashboardCanvas(delegate(PaperDrawing drawing, Size size)
      {
        ClickyInkProgressBar.Draw(drawing, size.Width, height, fraction, previous, progress, appear, inkStyle, seed);
      }, width, new double?(height), false);
    }

    // Token: 0x0600199C RID: 6556 RVA: 0x005D4F64 File Offset: 0x005D4F64
    private static void Draw(PaperDrawing drawing, double width, double height, double fraction, double? previous, double progress, double appear, ClickyInkStyle style, int seed)
    {
      double num = Math.Min(fraction, previous.GetValueOrDefault(fraction));
      double num2;
      if (previous != null)
      {
        double valueOrDefault = previous.GetValueOrDefault();
        num2 = valueOrDefault + (fraction - valueOrDefault) * progress;
      }
      else
      {
        num2 = fraction;
      }
      double num3 = num2;
      double num4 = Math.Max(0.0, width * ((previous == null) ? num3 : num));
      ClickyInkProgressBar.Place(drawing, ClickyInkHatch.Create(ClickyInkDashboardPalette.Pencil.Opacity(0.85), appear, 2.4, 1.2, seed, 0.0, new double?(num4), new double?(height), null), 0.0, 0.0);
      if (previous != null)
      {
        double valueOrDefault2 = previous.GetValueOrDefault();
        if (fraction > valueOrDefault2)
        {
          ClickyInkProgressBar.Place(drawing, ClickyInkHatch.Create(style.Fresh, 1.0, 2.4, 1.3, seed + 9, 0.0, new double?(Math.Max(0.0, width * (num3 - valueOrDefault2))), new double?(height), null), width * valueOrDefault2, 0.0);
        }
      }
      for (int i = 1; i < 4; i++)
      {
        drawing.Fill(new PaperPath().AddRect(new Rect(width * (double)i / 4.0, height + 2.0, 1.0, 4.0)), ClickyInkDashboardPalette.Pencil.Opacity(0.35));
      }
      if (previous != null)
      {
        double valueOrDefault3 = previous.GetValueOrDefault();
        if (valueOrDefault3 != fraction && progress > 0.0)
        {
          drawing.Stroke(new PaperPath().MoveTo(width * valueOrDefault3, -4.0).LineTo(width * valueOrDefault3 + 1.0, height + 3.0), PaperStroke.Round(style.Quiet, 1.2));
        }
      }
      drawing.Stroke(new ClickyInkRuledBox(seed, 3.0).PathIn(new Rect(0.0, 0.0, width, height)).Trimmed(0.0, appear), PaperStroke.Round(ClickyInkDashboardPalette.Pencil.Opacity(0.7), 1.1));
    }

    // Token: 0x0600199D RID: 6557 RVA: 0x005D51EC File Offset: 0x005D51EC
    private static void Place(PaperDrawing drawing, FrameworkElement element, double x, double y)
    {
      Canvas.SetLeft(element, x);
      Canvas.SetTop(element, y);
      drawing.Children.Add(element);
    }
  }
}
