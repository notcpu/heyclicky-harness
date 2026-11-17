using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000299 RID: 665
  public sealed class ClickyInkHeightMorph : Panel
  {
    // Token: 0x06001B92 RID: 7058 RVA: 0x005F22F4 File Offset: 0x005F22F4
    [NullableContext(1)]
    public ClickyInkHeightMorph(double progress, UIElement now, UIElement before)
    {
      this.progress = progress;
      base.Children.Add(now);
      base.Children.Add(before);
    }

    // Token: 0x06001B93 RID: 7059 RVA: 0x005F231C File Offset: 0x005F231C
    protected override Size MeasureOverride(Size availableSize)
    {
      Size size = new Size(availableSize.Width, double.PositiveInfinity);
      base.Children[0].Measure(size);
      Size desiredSize = base.Children[0].DesiredSize;
      if (base.Children.Count < 2)
      {
        return desiredSize;
      }
      base.Children[1].Measure(size);
      double height = base.Children[1].DesiredSize.Height;
      return new Size(desiredSize.Width, height + (desiredSize.Height - height) * ClickyInkClock.EaseInOut(this.progress));
    }

    // Token: 0x06001B94 RID: 7060 RVA: 0x005F23C4 File Offset: 0x005F23C4
    protected override Size ArrangeOverride(Size finalSize)
    {
      foreach (UIElement uielement in base.Children)
      {
        uielement.Arrange(new Rect(0.0, 0.0, finalSize.Width, uielement.DesiredSize.Height));
      }
      return finalSize;
    }

    // Token: 0x04000D47 RID: 3399
    private readonly double progress;
  }
}
