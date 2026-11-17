using System;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000245 RID: 581
  public static class InkSpring
  {
    // Token: 0x060019F2 RID: 6642 RVA: 0x005DBD9C File Offset: 0x005DBD9C
    public static double Value(double response, double dampingFraction, double elapsed)
    {
      if (elapsed <= 0.0)
      {
        return 0.0;
      }
      double num = 6.283185307179586 / response;
      if (dampingFraction >= 1.0)
      {
        return 1.0 - Math.Exp(-num * elapsed) * (1.0 + num * elapsed);
      }
      double num2 = dampingFraction * num;
      double num3 = num * Math.Sqrt(1.0 - dampingFraction * dampingFraction);
      return 1.0 - Math.Exp(-num2 * elapsed) * (Math.Cos(num3 * elapsed) + num2 / num3 * Math.Sin(num3 * elapsed));
    }

    // Token: 0x060019F3 RID: 6643 RVA: 0x005DBE40 File Offset: 0x005DBE40
    public static double SettleSeconds(double response, double dampingFraction)
    {
      return Math.Min(2.5, 6.9 / (Math.Max(0.05, Math.Min(1.0, dampingFraction)) * 2.0 * 3.141592653589793 / response));
    }

    // Token: 0x060019F4 RID: 6644 RVA: 0x005DBE98 File Offset: 0x005DBE98
    public static double Between(double from, double to, double response, double dampingFraction, double startedAt)
    {
      return from + (to - from) * InkSpring.Value(response, dampingFraction, InkObjectState.Now - startedAt);
    }
  }
}
