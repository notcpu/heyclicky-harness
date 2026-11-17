using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000279 RID: 633
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkUnder : Panel
  {
    // Token: 0x06001AD8 RID: 6872 RVA: 0x005E8830 File Offset: 0x005E8830
    public ClickyInkUnder(UIElement top, UIElement under, double drop)
    {
      this.top = top;
      this.under = under;
      this.drop = drop;
      base.Children.Add(top);
      base.Children.Add(under);
    }

    // Token: 0x06001AD9 RID: 6873 RVA: 0x005E8868 File Offset: 0x005E8868
    protected override Size MeasureOverride(Size availableSize)
    {
      this.top.Measure(availableSize);
      this.under.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      return new Size(this.top.DesiredSize.Width, this.top.DesiredSize.Height + this.drop);
    }

    // Token: 0x06001ADA RID: 6874 RVA: 0x005E88D8 File Offset: 0x005E88D8
    protected override Size ArrangeOverride(Size finalSize)
    {
      Size desiredSize = this.top.DesiredSize;
      this.top.Arrange(new Rect(0.0, 0.0, desiredSize.Width, desiredSize.Height));
      Size desiredSize2 = this.under.DesiredSize;
      this.under.Arrange(new Rect((desiredSize.Width - desiredSize2.Width) / 2.0, desiredSize.Height - desiredSize2.Height + this.drop, desiredSize2.Width, desiredSize2.Height));
      return finalSize;
    }

    // Token: 0x04000CDA RID: 3290
    private readonly UIElement top;

    // Token: 0x04000CDB RID: 3291
    private readonly UIElement under;

    // Token: 0x04000CDC RID: 3292
    private readonly double drop;
  }
}
