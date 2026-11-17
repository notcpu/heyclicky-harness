using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x02000130 RID: 304
  public sealed class StepInteractiveSlot : Panel
  {
    // Token: 0x0600103E RID: 4158 RVA: 0x00583704 File Offset: 0x00583704
    [NullableContext(1)]
    public StepInteractiveSlot(FrameworkElement content)
    {
      base.Children.Add(content);
    }

    // Token: 0x0600103F RID: 4159 RVA: 0x00583718 File Offset: 0x00583718
    protected override Size MeasureOverride(Size available)
    {
      double num = 0.0;
      double num2 = 0.0;
      foreach (UIElement uielement in base.Children)
      {
        uielement.Measure(new Size(available.Width, double.PositiveInfinity));
        num = Math.Max(num, uielement.DesiredSize.Width);
        num2 = Math.Max(num2, uielement.DesiredSize.Height);
      }
      return new Size(Math.Min(num, available.Width), double.IsInfinity(available.Height) ? num2 : Math.Min(num2, available.Height));
    }

    // Token: 0x06001040 RID: 4160 RVA: 0x005837EC File Offset: 0x005837EC
    protected override Size ArrangeOverride(Size final)
    {
      foreach (UIElement uielement in base.Children)
      {
        Size desiredSize = uielement.DesiredSize;
        double num = Math.Max(desiredSize.Width, final.Width);
        uielement.Arrange(new Rect((final.Width - num) / 2.0, (final.Height - desiredSize.Height) / 2.0, num, desiredSize.Height));
      }
      return final;
    }
  }
}
