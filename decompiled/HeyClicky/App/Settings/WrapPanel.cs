using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000D7 RID: 215
  public sealed class WrapPanel : Panel
  {
    // Token: 0x17000196 RID: 406
    // (get) Token: 0x06000A3E RID: 2622 RVA: 0x005576DC File Offset: 0x005576DC
    // (set) Token: 0x06000A3F RID: 2623 RVA: 0x005576E4 File Offset: 0x005576E4
    public double Spacing { get; set; } = 6.0;

    // Token: 0x06000A40 RID: 2624 RVA: 0x005576F0 File Offset: 0x005576F0
    protected override Size MeasureOverride(Size available)
    {
      double num = 0.0;
      double num2 = 0.0;
      double num3 = 0.0;
      double num4 = 0.0;
      foreach (UIElement uielement in base.Children)
      {
        uielement.Measure(new Size(available.Width, double.PositiveInfinity));
        Size desiredSize = uielement.DesiredSize;
        if (num > 0.0 && num + desiredSize.Width > available.Width)
        {
          num2 += num3 + this.Spacing;
          num = 0.0;
          num3 = 0.0;
        }
        num += desiredSize.Width + this.Spacing;
        num3 = Math.Max(num3, desiredSize.Height);
        num4 = Math.Max(num4, num - this.Spacing);
      }
      return new Size(double.IsInfinity(available.Width) ? num4 : Math.Min(num4, available.Width), num2 + num3);
    }

    // Token: 0x06000A41 RID: 2625 RVA: 0x0055781C File Offset: 0x0055781C
    protected override Size ArrangeOverride(Size final)
    {
      double num = 0.0;
      double num2 = 0.0;
      double num3 = 0.0;
      foreach (UIElement uielement in base.Children)
      {
        Size desiredSize = uielement.DesiredSize;
        if (num > 0.0 && num + desiredSize.Width > final.Width)
        {
          num2 += num3 + this.Spacing;
          num = 0.0;
          num3 = 0.0;
        }
        uielement.Arrange(new Rect(num, num2, desiredSize.Width, desiredSize.Height));
        num += desiredSize.Width + this.Spacing;
        num3 = Math.Max(num3, desiredSize.Height);
      }
      return final;
    }
  }
}
