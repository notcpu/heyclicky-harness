using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020002A3 RID: 675
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkRestingPencil
  {
    // Token: 0x06001BD4 RID: 7124 RVA: 0x005F5190 File Offset: 0x005F5190
    public static FrameworkElement Make(double clock, double since, Color tint, double size = 15.0)
    {
      if (ClickyHomeRendering.IsOffscreen || Anim.ReduceMotion)
      {
        return ClickyInkRestingPencil.AtClock(clock, since, tint, size);
      }
      return new ClickyInkPencilLayers(new ClickyInkPencilLayers.Motion.Resting(Math.Max(0.0, clock - since)), tint, size)
      {
        Width = size + 22.0,
        Height = size + 6.0
      };
    }

    // Token: 0x06001BD5 RID: 7125 RVA: 0x005F51F4 File Offset: 0x005F51F4
    private static FrameworkElement AtClock(double clock, double since, Color tint, double size)
    {
      Canvas canvas = new Canvas
      {
        Width = size + 22.0,
        Height = size + 6.0,
        IsHitTestVisible = false
      };
      double num = ClickyInkClock.EaseInOut(ClickyInkClock.Progress(clock, since, since + 0.5));
      if (num <= 0.0)
      {
        return canvas;
      }
      double num2 = clock - since;
      double num3 = num2 % 2.4;
      double num4 = ((num2 > 0.8 && num3 < 0.5) ? Math.Abs(Math.Sin(num3 / 0.25 * 3.141592653589793)) : 0.0);
      double num5 = canvas.Height * 0.78;
      FrameworkElement frameworkElement = ClickyInkPencilGlyph.Make(size, tint, false);
      frameworkElement.Opacity = 0.6 * num;
      Canvas.SetLeft(frameworkElement, 0.0);
      Canvas.SetTop(frameworkElement, num5 - size - 1.0 - (1.0 - num) * 4.0 + num4 * 2.2);
      canvas.Children.Add(frameworkElement);
      for (int i = 0; i < 3; i++)
      {
        double num6 = (num2 * 0.9 - (double)i * 0.22) % 1.8;
        double num7 = ((num6 < 0.0) ? 0.0 : (Math.Max(0.0, Math.Sin(Math.Min(1.0, num6 / 1.2) * 3.141592653589793)) * 0.55 * num));
        Ellipse ellipse = new Ellipse
        {
          Width = 2.8,
          Height = 2.8,
          Fill = Ui.Brush(tint),
          Opacity = num7
        };
        Canvas.SetLeft(ellipse, size + 4.0 + (double)i * 5.5);
        Canvas.SetTop(ellipse, num5 - 2.8);
        canvas.Children.Add(ellipse);
      }
      return canvas;
    }
  }
}
