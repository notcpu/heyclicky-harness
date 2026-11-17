using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Billing
{
  // Token: 0x02000366 RID: 870
  public static class PaywallFit
  {
    // Token: 0x06002619 RID: 9753 RVA: 0x0063F5D0 File Offset: 0x0063F5D0
    public static double CardWidth(double requested, double workWidthDip, double shadowMargin)
    {
      return Math.Max(360.0, Math.Min(requested, workWidthDip - shadowMargin * 2.0));
    }

    // Token: 0x0600261A RID: 9754 RVA: 0x0063F5F4 File Offset: 0x0063F5F4
    public static double MaxCardHeight(double workHeightDip, double shadowMargin)
    {
      return Math.Max(240.0, workHeightDip - shadowMargin * 2.0);
    }

    // Token: 0x0600261B RID: 9755 RVA: 0x0063F614 File Offset: 0x0063F614
    [return: TupleElementNames(new string[] { "X", "Y" })]
    public static ValueTuple<int, int> Origin(int boundsLeft, int boundsTop, int boundsWidth, int boundsHeight, int workLeft, int workTop, int workRight, int workBottom, int width, int height)
    {
      int num = boundsLeft + (boundsWidth - width) / 2;
      int num2 = boundsTop + (boundsHeight - height) / 2;
      num = Math.Max(workLeft, Math.Min(num, workRight - width));
      num2 = Math.Max(workTop, Math.Min(num2, workBottom - height));
      return new ValueTuple<int, int>(num, num2);
    }

    // Token: 0x04001205 RID: 4613
    public const double MinCardWidth = 360.0;

    // Token: 0x04001206 RID: 4614
    public const double MinCardHeight = 240.0;
  }
}
