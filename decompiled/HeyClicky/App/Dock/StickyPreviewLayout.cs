using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002FE RID: 766
  public static class StickyPreviewLayout
  {
    // Token: 0x0600218C RID: 8588 RVA: 0x0061CA94 File Offset: 0x0061CA94
    [return: TupleElementNames(new string[] { "Screen", "Outer" })]
    public static ValueTuple<double, double> CornerRadii(double? outer = null)
    {
      if (outer != null)
      {
        double valueOrDefault = outer.GetValueOrDefault();
        return new ValueTuple<double, double>(Math.Max(0.0, valueOrDefault - 7.0), valueOrDefault);
      }
      return new ValueTuple<double, double>(10.0, 17.0);
    }

    // Token: 0x04001014 RID: 4116
    public const double ScreenWidth = 480.0;

    // Token: 0x04001015 RID: 4117
    public const double ScreenHeight = 300.0;

    // Token: 0x04001016 RID: 4118
    public const double Bezel = 7.0;

    // Token: 0x04001017 RID: 4119
    public const double ScreenCornerRadius = 10.0;

    // Token: 0x04001018 RID: 4120
    public const double Width = 494.0;

    // Token: 0x04001019 RID: 4121
    public const double Height = 314.0;
  }
}
