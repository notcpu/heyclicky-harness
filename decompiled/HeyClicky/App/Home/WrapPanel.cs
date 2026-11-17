using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Home
{
  // Token: 0x020002AD RID: 685
  public sealed class WrapPanel : Panel
  {
    // Token: 0x170004CD RID: 1229
    // (get) Token: 0x06001C08 RID: 7176 RVA: 0x005F85AC File Offset: 0x005F85AC
    // (set) Token: 0x06001C09 RID: 7177 RVA: 0x005F85B4 File Offset: 0x005F85B4
    public double HorizontalSpacing { get; set; } = 8.0;

    // Token: 0x170004CE RID: 1230
    // (get) Token: 0x06001C0A RID: 7178 RVA: 0x005F85C0 File Offset: 0x005F85C0
    // (set) Token: 0x06001C0B RID: 7179 RVA: 0x005F85C8 File Offset: 0x005F85C8
    public double VerticalSpacing { get; set; } = 8.0;

    // Token: 0x06001C0C RID: 7180 RVA: 0x005F85D4 File Offset: 0x005F85D4
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
          num2 += num3 + this.VerticalSpacing;
          num = 0.0;
          num3 = 0.0;
        }
        num += desiredSize.Width + this.HorizontalSpacing;
        num3 = Math.Max(num3, desiredSize.Height);
        num4 = Math.Max(num4, num - this.HorizontalSpacing);
      }
      return new Size(double.IsInfinity(available.Width) ? num4 : Math.Min(num4, available.Width), num2 + num3);
    }

    // Token: 0x06001C0D RID: 7181 RVA: 0x005F8700 File Offset: 0x005F8700
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
          num2 += num3 + this.VerticalSpacing;
          num = 0.0;
          num3 = 0.0;
        }
        uielement.Arrange(new Rect(num, num2, desiredSize.Width, desiredSize.Height));
        num += desiredSize.Width + this.HorizontalSpacing;
        num3 = Math.Max(num3, desiredSize.Height);
      }
      return final;
    }
  }
}
