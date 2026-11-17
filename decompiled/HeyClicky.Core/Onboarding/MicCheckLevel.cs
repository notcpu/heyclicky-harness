using System;

namespace HeyClicky.Core.Onboarding
{
  // Token: 0x020000ED RID: 237
  public static class MicCheckLevel
  {
    // Token: 0x06000F2D RID: 3885 RVA: 0x0038E240 File Offset: 0x0038E240
    public static double FromLinearLevel(float level)
    {
      if (level <= 0f)
      {
        return 0.0;
      }
      double num = (double)Math.Min(level, 1f) / 10.2;
      return Math.Clamp((20.0 * Math.Log10(num) + 50.0) / 50.0, 0.0, 1.0);
    }

    // Token: 0x06000F2E RID: 3886 RVA: 0x0038E2B4 File Offset: 0x0038E2B4
    public static bool PassesByFallback(double secondsOnStep, bool heardAnyInput)
    {
      return heardAnyInput && secondsOnStep >= 10.0;
    }

    // Token: 0x04000679 RID: 1657
    public const double PassLevel = 0.25;

    // Token: 0x0400067A RID: 1658
    public const double PassSeconds = 0.25;

    // Token: 0x0400067B RID: 1659
    public const double FallbackSeconds = 10.0;
  }
}
