using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.UI
{
  // Token: 0x020000B3 RID: 179
  public sealed class CenteredWrapPanel : Panel
  {
    // Token: 0x17000131 RID: 305
    // (get) Token: 0x06000758 RID: 1880 RVA: 0x00537334 File Offset: 0x00537334
    // (set) Token: 0x06000759 RID: 1881 RVA: 0x0053733C File Offset: 0x0053733C
    public double HorizontalSpacing { get; set; } = 8.0;

    // Token: 0x17000132 RID: 306
    // (get) Token: 0x0600075A RID: 1882 RVA: 0x00537348 File Offset: 0x00537348
    // (set) Token: 0x0600075B RID: 1883 RVA: 0x00537350 File Offset: 0x00537350
    public double VerticalSpacing { get; set; } = 8.0;

    // Token: 0x0600075C RID: 1884 RVA: 0x0053735C File Offset: 0x0053735C
    [NullableContext(1)]
    private List<List<UIElement>> Rows(double width)
    {
      List<List<UIElement>> list = new List<List<UIElement>>
      {
        new List<UIElement>()
      };
      double num = 0.0;
      foreach (UIElement uielement in base.Children)
      {
        double width2 = uielement.DesiredSize.Width;
        List<List<UIElement>> list2 = list;
        if (list2[list2.Count - 1].Count > 0 && num + this.HorizontalSpacing + width2 > width)
        {
          list.Add(new List<UIElement>());
          num = 0.0;
        }
        double num2 = num;
        List<List<UIElement>> list3 = list;
        num = num2 + (((list3[list3.Count - 1].Count > 0) ? this.HorizontalSpacing : 0.0) + width2);
        List<List<UIElement>> list4 = list;
        list4[list4.Count - 1].Add(uielement);
      }
      return list;
    }

    // Token: 0x0600075D RID: 1885 RVA: 0x00537450 File Offset: 0x00537450
    protected override Size MeasureOverride(Size available)
    {
      foreach (UIElement uielement in base.Children)
      {
        uielement.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      }
      double num = (double.IsInfinity(available.Width) ? 392.0 : available.Width);
      List<List<UIElement>> list = this.Rows(num);
      double num2 = list.Sum<List<UIElement>>(delegate(List<UIElement> r)
      {
        if (r.Count != 0)
        {
          return r.Max<UIElement>((UIElement c) => c.DesiredSize.Height);
        }
        return 0.0;
      }) + this.VerticalSpacing * (double)Math.Max(0, list.Count - 1);
      return new Size(num, num2);
    }

    // Token: 0x0600075E RID: 1886 RVA: 0x00537520 File Offset: 0x00537520
    protected override Size ArrangeOverride(Size final)
    {
      double num = 0.0;
      foreach (List<UIElement> list in this.Rows(final.Width))
      {
        if (list.Count != 0)
        {
          double num2 = list.Sum<UIElement>((UIElement c) => c.DesiredSize.Width) + this.HorizontalSpacing * (double)(list.Count - 1);
          double num3 = list.Max<UIElement>((UIElement c) => c.DesiredSize.Height);
          double num4 = (final.Width - num2) / 2.0;
          foreach (UIElement uielement in list)
          {
            uielement.Arrange(new Rect(num4, num + (num3 - uielement.DesiredSize.Height) / 2.0, uielement.DesiredSize.Width, uielement.DesiredSize.Height));
            num4 += uielement.DesiredSize.Width + this.HorizontalSpacing;
          }
          num += num3 + this.VerticalSpacing;
        }
      }
      return final;
    }
  }
}
