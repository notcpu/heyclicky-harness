using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000253 RID: 595
  public sealed class InkViewThatFits : Panel
  {
    // Token: 0x06001A36 RID: 6710 RVA: 0x005DDD98 File Offset: 0x005DDD98
    [NullableContext(1)]
    public InkViewThatFits(params UIElement[] options)
    {
      foreach (UIElement uielement in options)
      {
        base.Children.Add(uielement);
      }
    }

    // Token: 0x06001A37 RID: 6711 RVA: 0x005DDDCC File Offset: 0x005DDDCC
    protected override Size MeasureOverride(Size availableSize)
    {
      this.shown = null;
      foreach (UIElement uielement in base.Children)
      {
        uielement.Measure(new Size(double.PositiveInfinity, availableSize.Height));
        if (uielement.DesiredSize.Width <= availableSize.Width + 0.5)
        {
          this.shown = uielement;
          break;
        }
      }
      if (this.shown == null)
      {
        UIElement uielement2;
        if (base.Children.Count <= 0)
        {
          uielement2 = null;
        }
        else
        {
          UIElementCollection children = base.Children;
          uielement2 = children[children.Count - 1];
        }
        this.shown = uielement2;
      }
      foreach (UIElement uielement3 in base.Children)
      {
        uielement3.Opacity = (uielement3 == this.shown) > false;
        uielement3.IsHitTestVisible = uielement3 == this.shown;
      }
      if (this.shown == null)
      {
        return new Size(0f, 0f);
      }
      this.shown.Measure(availableSize);
      return this.shown.DesiredSize;
    }

    // Token: 0x06001A38 RID: 6712 RVA: 0x005DDF1C File Offset: 0x005DDF1C
    protected override Size ArrangeOverride(Size finalSize)
    {
      foreach (UIElement uielement in base.Children)
      {
        uielement.Arrange((uielement == this.shown) ? new Rect(0.0, 0.0, finalSize.Width, finalSize.Height) : new Rect(0f, 0f, 0f, 0f));
      }
      return finalSize;
    }

    // Token: 0x04000CA2 RID: 3234
    [Nullable(2)]
    private UIElement shown;
  }
}
