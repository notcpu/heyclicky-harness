using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200023C RID: 572
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkCenteredOverflow : Panel
  {
    // Token: 0x060019C9 RID: 6601 RVA: 0x005D9978 File Offset: 0x005D9978
    public ClickyInkCenteredOverflow(UIElement content, double width)
    {
      this.content = content;
      this.width = width;
      base.Children.Add(content);
    }

    // Token: 0x060019CA RID: 6602 RVA: 0x005D999C File Offset: 0x005D999C
    protected override Size MeasureOverride(Size availableSize)
    {
      this.content.Measure(new Size(double.PositiveInfinity, availableSize.Height));
      return new Size(this.width, this.content.DesiredSize.Height);
    }

    // Token: 0x060019CB RID: 6603 RVA: 0x005D99E8 File Offset: 0x005D99E8
    protected override Size ArrangeOverride(Size finalSize)
    {
      Size desiredSize = this.content.DesiredSize;
      this.content.Arrange(new Rect((finalSize.Width - desiredSize.Width) / 2.0, (finalSize.Height - desiredSize.Height) / 2.0, desiredSize.Width, desiredSize.Height));
      return finalSize;
    }

    // Token: 0x04000C72 RID: 3186
    private readonly UIElement content;

    // Token: 0x04000C73 RID: 3187
    private readonly double width;
  }
}
