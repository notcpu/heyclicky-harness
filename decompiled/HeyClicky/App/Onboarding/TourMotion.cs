using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x02000128 RID: 296
  public static class TourMotion
  {
    // Token: 0x06001028 RID: 4136 RVA: 0x005820E4 File Offset: 0x005820E4
    public static double EaseOut(double t)
    {
      if (t <= 0.0)
      {
        return 0.0;
      }
      if (t >= 1.0)
      {
        return 1.0;
      }
      double num = 0.0;
      double num2 = 1.0;
      double num3 = t;
      for (int i = 0; i < 40; i++)
      {
        num3 = (num + num2) / 2.0;
        if (3.0 * (1.0 - num3) * num3 * num3 * 0.58 + num3 * num3 * num3 < t)
        {
          num = num3;
        }
        else
        {
          num2 = num3;
        }
      }
      return 3.0 * (1.0 - num3) * num3 * num3 + num3 * num3 * num3;
    }

    // Token: 0x06001029 RID: 4137 RVA: 0x005821A0 File Offset: 0x005821A0
    [return: TupleElementNames(new string[] { "X", "Y", "W", "H" })]
    public static ValueTuple<int, int, int, int> CenteredRegionPx(int windowWidthPx, int windowHeightPx, double scale, double panelWidth, double panelHeight, double margin)
    {
      int num = Math.Min(windowWidthPx, (int)Math.Ceiling((panelWidth + margin * 2.0) * scale));
      int num2 = Math.Min(windowHeightPx, (int)Math.Ceiling((panelHeight + margin * 2.0) * scale));
      return new ValueTuple<int, int, int, int>((windowWidthPx - num) / 2, (windowHeightPx - num2) / 2, num, num2);
    }

    // Token: 0x040006F9 RID: 1785
    public const double StepSpringResponse = 0.38;

    // Token: 0x040006FA RID: 1786
    public const double StepSpringDamping = 0.86;

    // Token: 0x040006FB RID: 1787
    public const double ReducedMotionSeconds = 0.12;
  }
}
