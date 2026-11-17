using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200021D RID: 541
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkFill : Panel
  {
    // Token: 0x0600196E RID: 6510 RVA: 0x005D2A60 File Offset: 0x005D2A60
    public ClickyInkFill(UIElement content, double height)
    {
      this.content = content;
      this.height = height;
      base.Children.Add(content);
    }

    // Token: 0x0600196F RID: 6511 RVA: 0x005D2A84 File Offset: 0x005D2A84
    protected override Size MeasureOverride(Size availableSize)
    {
      double num = (double.IsInfinity(availableSize.Width) ? 0.0 : availableSize.Width);
      this.content.Measure(new Size(num, this.height));
      return new Size(num, this.height);
    }

    // Token: 0x06001970 RID: 6512 RVA: 0x005D2AD8 File Offset: 0x005D2AD8
    protected override Size ArrangeOverride(Size finalSize)
    {
      this.content.Arrange(new Rect(0.0, 0.0, finalSize.Width, this.height));
      return finalSize;
    }

    // Token: 0x04000C5B RID: 3163
    private readonly UIElement content;

    // Token: 0x04000C5C RID: 3164
    private readonly double height;
  }
}
