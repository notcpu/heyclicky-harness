using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000261 RID: 609
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkOverlay : Panel, IInkBaseline
  {
    // Token: 0x06001A82 RID: 6786 RVA: 0x005E2BE8 File Offset: 0x005E2BE8
    public ClickyInkOverlay(UIElement content, params UIElement[] overlays)
    {
      this.content = content;
      base.Children.Add(content);
      foreach (UIElement uielement in overlays)
      {
        base.Children.Add(uielement);
      }
    }

    // Token: 0x06001A83 RID: 6787 RVA: 0x005E2C30 File Offset: 0x005E2C30
    public ClickyInkOverlay Behind(UIElement background)
    {
      base.Children.Insert(0, background);
      return this;
    }

    // Token: 0x17000496 RID: 1174
    // (get) Token: 0x06001A84 RID: 6788 RVA: 0x005E2C40 File Offset: 0x005E2C40
    public double? FirstBaseline
    {
      get
      {
        return InkBaseline.First(this.content);
      }
    }

    // Token: 0x06001A85 RID: 6789 RVA: 0x005E2C50 File Offset: 0x005E2C50
    protected override Size MeasureOverride(Size availableSize)
    {
      this.content.Measure(availableSize);
      Size desiredSize = this.content.DesiredSize;
      foreach (UIElement uielement in base.Children)
      {
        if (uielement != this.content)
        {
          uielement.Measure(desiredSize);
        }
      }
      return desiredSize;
    }

    // Token: 0x06001A86 RID: 6790 RVA: 0x005E2CC4 File Offset: 0x005E2CC4
    protected override Size ArrangeOverride(Size finalSize)
    {
      foreach (UIElement uielement in base.Children)
      {
        if (!(uielement == this.content))
        {
          FrameworkElement frameworkElement = uielement as FrameworkElement;
          double num = ((frameworkElement != null && !double.IsNaN(frameworkElement.Width)) ? (frameworkElement.Width + frameworkElement.Margin.Left + frameworkElement.Margin.Right) : finalSize.Width);
          double num2 = ((frameworkElement != null && !double.IsNaN(frameworkElement.Height)) ? (frameworkElement.Height + frameworkElement.Margin.Top + frameworkElement.Margin.Bottom) : finalSize.Height);
          if (frameworkElement != null && double.IsNaN(frameworkElement.Width) && frameworkElement.HorizontalAlignment != HorizontalAlignment.Stretch)
          {
            num = Math.Min(uielement.DesiredSize.Width, finalSize.Width);
          }
          if (frameworkElement != null && double.IsNaN(frameworkElement.Height) && frameworkElement.VerticalAlignment != VerticalAlignment.Stretch)
          {
            num2 = Math.Min(uielement.DesiredSize.Height, finalSize.Height);
          }
          HorizontalAlignment? horizontalAlignment = ((frameworkElement != null) ? new HorizontalAlignment?(frameworkElement.HorizontalAlignment) : null);
          if (horizontalAlignment == null)
          {
            goto IL_01A5;
          }
          HorizontalAlignment valueOrDefault = horizontalAlignment.GetValueOrDefault();
          double num3;
          if (valueOrDefault != HorizontalAlignment.Center)
          {
            if (valueOrDefault != HorizontalAlignment.Right)
            {
              goto IL_01A5;
            }
            num3 = finalSize.Width - num;
          }
          else
          {
            num3 = (finalSize.Width - num) / 2.0;
          }
          IL_01B0:
          double num4 = num3;
          VerticalAlignment? verticalAlignment = ((frameworkElement != null) ? new VerticalAlignment?(frameworkElement.VerticalAlignment) : null);
          if (verticalAlignment == null)
          {
            goto IL_0212;
          }
          VerticalAlignment valueOrDefault2 = verticalAlignment.GetValueOrDefault();
          if (valueOrDefault2 != VerticalAlignment.Center)
          {
            if (valueOrDefault2 != VerticalAlignment.Bottom)
            {
              goto IL_0212;
            }
            num3 = finalSize.Height - num2;
          }
          else
          {
            num3 = (finalSize.Height - num2) / 2.0;
          }
          IL_021D:
          double num5 = num3;
          uielement.Arrange(new Rect(num4, num5, num, num2));
          continue;
          IL_0212:
          num3 = 0.0;
          goto IL_021D;
          IL_01A5:
          num3 = 0.0;
          goto IL_01B0;
        }
        uielement.Arrange(new Rect(0.0, 0.0, finalSize.Width, finalSize.Height));
      }
      return finalSize;
    }

    // Token: 0x04000CBD RID: 3261
    private readonly UIElement content;
  }
}
