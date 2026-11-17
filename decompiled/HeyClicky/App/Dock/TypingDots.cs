using System;
using System.Numerics;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200031B RID: 795
  public sealed class TypingDots : StackPanel
  {
    // Token: 0x06002269 RID: 8809 RVA: 0x006233E0 File Offset: 0x006233E0
    public TypingDots(Color color, double scale = 1.0)
    {
      base.Orientation = Orientation.Horizontal;
      base.Spacing = 5.0 * scale;
      base.VerticalAlignment = VerticalAlignment.Center;
      base.IsHitTestVisible = false;
      double num = 8.0 * scale;
      for (int i = 0; i < 3; i++)
      {
        Ellipse ellipse = new Ellipse
        {
          Width = num,
          Height = num,
          Fill = Ui.Brush(color),
          CenterPoint = new Vector3((float)num / 2f, (float)num / 2f, 0f)
        };
        base.Children.Add(ellipse);
        int num2 = i;
        double lag = 1.1 * (double)num2 / 6.0;
        if (Anim.ReduceMotion)
        {
          ellipse.Opacity = ((num2 == 1) ? 0.75 : 0.3799999952316284);
        }
        else
        {
          ellipse.Opacity = 0.3799999952316284;
          Anim.LoopScalar(ellipse, "Opacity", 1.1, (double t) => (float)TypingDots.Lerp3(0.3799999952316284, 1.0, 0.3799999952316284, TypingDots.Phase(t - lag)), 48, 0.0);
          Anim.LoopVector3(ellipse, "Scale", 1.1, delegate(double t)
          {
            float num3 = (float)TypingDots.Lerp3(0.86, 1.08, 0.86, TypingDots.Phase(t - lag));
            return new Vector3(num3, num3, 1f);
          }, 48, 0.0);
        }
      }
    }

    // Token: 0x0600226A RID: 8810 RVA: 0x00623538 File Offset: 0x00623538
    private static double Phase(double t)
    {
      double num = (t % 1.1 + 1.1) % 1.1 / 1.1;
      return num * num * (3.0 - 2.0 * num);
    }

    // Token: 0x0600226B RID: 8811 RVA: 0x00623588 File Offset: 0x00623588
    private static double Lerp3(double a, double b, double c, double u)
    {
      if (u >= 0.5)
      {
        return b + (c - b) * ((u - 0.5) / 0.5);
      }
      return a + (b - a) * (u / 0.5);
    }

    // Token: 0x04001093 RID: 4243
    public const double DotDiameter = 8.0;

    // Token: 0x04001094 RID: 4244
    public const double DotSpacing = 5.0;

    // Token: 0x04001095 RID: 4245
    private const double Cycle = 1.1;

    // Token: 0x04001096 RID: 4246
    private const float Resting = 0.38f;
  }
}
