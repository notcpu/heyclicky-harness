using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001B8 RID: 440
  public sealed class ClickyNotebookFlow : Panel
  {
    // Token: 0x1700042A RID: 1066
    // (get) Token: 0x06001759 RID: 5977 RVA: 0x005C0104 File Offset: 0x005C0104
    // (set) Token: 0x0600175A RID: 5978 RVA: 0x005C010C File Offset: 0x005C010C
    public double Spacing { get; set; } = 8.0;

    // Token: 0x1700042B RID: 1067
    // (get) Token: 0x0600175B RID: 5979 RVA: 0x005C0118 File Offset: 0x005C0118
    // (set) Token: 0x0600175C RID: 5980 RVA: 0x005C0120 File Offset: 0x005C0120
    public double LineSpacing { get; set; } = 4.0;

    // Token: 0x0600175D RID: 5981 RVA: 0x005C012C File Offset: 0x005C012C
    protected override Size MeasureOverride(Size availableSize)
    {
      double width = availableSize.Width;
      double num = 0.0;
      double num2 = 0.0;
      double num3 = 0.0;
      double num4 = 0.0;
      foreach (UIElement uielement in base.Children)
      {
        uielement.Measure(new Size(width, double.PositiveInfinity));
        Size desiredSize = uielement.DesiredSize;
        if (num > 0.0 && num + desiredSize.Width > width)
        {
          num2 += num3 + this.LineSpacing;
          num = 0.0;
          num3 = 0.0;
        }
        num += desiredSize.Width + this.Spacing;
        num4 = Math.Max(num4, num - this.Spacing);
        num3 = Math.Max(num3, desiredSize.Height);
      }
      return new Size(Math.Min(num4, width), num2 + num3);
    }

    // Token: 0x0600175E RID: 5982 RVA: 0x005C0240 File Offset: 0x005C0240
    protected override Size ArrangeOverride(Size finalSize)
    {
      double num = 0.0;
      double num2 = 0.0;
      double num3 = 0.0;
      foreach (UIElement uielement in base.Children)
      {
        Size desiredSize = uielement.DesiredSize;
        if (num > 0.0 && num + desiredSize.Width > finalSize.Width)
        {
          num2 += num3 + this.LineSpacing;
          num = 0.0;
          num3 = 0.0;
        }
        uielement.Arrange(new Rect(num, num2, desiredSize.Width, desiredSize.Height));
        num += desiredSize.Width + this.Spacing;
        num3 = Math.Max(num3, desiredSize.Height);
      }
      return finalSize;
    }
  }
}
