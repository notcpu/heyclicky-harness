using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200028D RID: 653
  public sealed class ClickyInkCentered : Panel, IInkBaseline
  {
    // Token: 0x06001B4C RID: 6988 RVA: 0x005ED928 File Offset: 0x005ED928
    [NullableContext(1)]
    public ClickyInkCentered(UIElement content, double width, double height)
    {
      this.content = content;
      base.Width = width;
      base.Height = height;
      base.Children.Add(content);
    }

    // Token: 0x170004B3 RID: 1203
    // (get) Token: 0x06001B4D RID: 6989 RVA: 0x005ED954 File Offset: 0x005ED954
    private double Top
    {
      get
      {
        return (base.Height - this.content.DesiredSize.Height) / 2.0;
      }
    }

    // Token: 0x170004B4 RID: 1204
    // (get) Token: 0x06001B4E RID: 6990 RVA: 0x005ED988 File Offset: 0x005ED988
    public double? FirstBaseline
    {
      get
      {
        double? num = InkBaseline.First(this.content);
        if (num != null)
        {
          double valueOrDefault = num.GetValueOrDefault();
          return new double?(this.Top + valueOrDefault);
        }
        return null;
      }
    }

    // Token: 0x06001B4F RID: 6991 RVA: 0x005ED9CC File Offset: 0x005ED9CC
    protected override Size MeasureOverride(Size availableSize)
    {
      this.content.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      return new Size(base.Width, base.Height);
    }

    // Token: 0x06001B50 RID: 6992 RVA: 0x005EDA04 File Offset: 0x005EDA04
    protected override Size ArrangeOverride(Size finalSize)
    {
      Size desiredSize = this.content.DesiredSize;
      this.content.Arrange(new Rect((finalSize.Width - desiredSize.Width) / 2.0, (finalSize.Height - desiredSize.Height) / 2.0, desiredSize.Width, desiredSize.Height));
      return finalSize;
    }

    // Token: 0x04000D15 RID: 3349
    [Nullable(1)]
    private readonly UIElement content;
  }
}
