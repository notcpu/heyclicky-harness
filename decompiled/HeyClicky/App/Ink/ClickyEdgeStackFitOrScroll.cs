using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000210 RID: 528
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyEdgeStackFitOrScroll : Grid
  {
    // Token: 0x1700047C RID: 1148
    // (get) Token: 0x06001940 RID: 6464 RVA: 0x005CFCF8 File Offset: 0x005CFCF8
    public double MaximumHeight { get; }

    // Token: 0x06001941 RID: 6465 RVA: 0x005CFD00 File Offset: 0x005CFD00
    public ClickyEdgeStackFitOrScroll(double maximumHeight, FrameworkElement content)
    {
      this.MaximumHeight = maximumHeight;
      this.scroller = new ScrollViewer
      {
        Content = content,
        VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
        VerticalScrollMode = ScrollMode.Auto
      };
      base.Children.Add(this.scroller);
    }

    // Token: 0x06001942 RID: 6466 RVA: 0x005CFD54 File Offset: 0x005CFD54
    protected override Size MeasureOverride(Size availableSize)
    {
      FrameworkElement frameworkElement = (FrameworkElement)this.scroller.Content;
      frameworkElement.Measure(new Size(availableSize.Width, double.PositiveInfinity));
      double height = frameworkElement.DesiredSize.Height;
      this.scroller.Measure(new Size(availableSize.Width, Math.Min(height, this.MaximumHeight)));
      return new Size(frameworkElement.DesiredSize.Width, Math.Min(height, this.MaximumHeight));
    }

    // Token: 0x06001943 RID: 6467 RVA: 0x005CFDDC File Offset: 0x005CFDDC
    protected override Size ArrangeOverride(Size finalSize)
    {
      this.scroller.Arrange(new Rect(0.0, 0.0, finalSize.Width, finalSize.Height));
      return finalSize;
    }

    // Token: 0x04000C50 RID: 3152
    private readonly ScrollViewer scroller;
  }
}
