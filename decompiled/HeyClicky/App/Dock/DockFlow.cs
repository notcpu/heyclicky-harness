using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000334 RID: 820
  internal sealed class DockFlow : Panel
  {
    // Token: 0x17000671 RID: 1649
    // (get) Token: 0x060022F4 RID: 8948 RVA: 0x006298E4 File Offset: 0x006298E4
    // (set) Token: 0x060022F5 RID: 8949 RVA: 0x006298EC File Offset: 0x006298EC
    public double HorizontalSpacing { get; set; } = 6.0;

    // Token: 0x17000672 RID: 1650
    // (get) Token: 0x060022F6 RID: 8950 RVA: 0x006298F8 File Offset: 0x006298F8
    // (set) Token: 0x060022F7 RID: 8951 RVA: 0x00629900 File Offset: 0x00629900
    public double VerticalSpacing { get; set; } = 4.0;

    // Token: 0x060022F8 RID: 8952 RVA: 0x0062990C File Offset: 0x0062990C
    protected override Size MeasureOverride(Size available)
    {
      double num = (double.IsInfinity(available.Width) ? double.MaxValue : available.Width);
      double num2 = 0.0;
      double num3 = 0.0;
      double num4 = 0.0;
      double num5 = 0.0;
      foreach (UIElement uielement in base.Children)
      {
        uielement.Measure(new Size(num, double.PositiveInfinity));
        Size desiredSize = uielement.DesiredSize;
        if (num2 > 0.0 && num2 + desiredSize.Width > num)
        {
          num3 += num4 + this.VerticalSpacing;
          num2 = 0.0;
          num4 = 0.0;
        }
        num2 += desiredSize.Width + this.HorizontalSpacing;
        num4 = Math.Max(num4, desiredSize.Height);
        num5 = Math.Max(num5, num2 - this.HorizontalSpacing);
      }
      return new Size(num5, (base.Children.Count == 0) ? 0.0 : (num3 + num4));
    }

    // Token: 0x060022F9 RID: 8953 RVA: 0x00629A4C File Offset: 0x00629A4C
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
