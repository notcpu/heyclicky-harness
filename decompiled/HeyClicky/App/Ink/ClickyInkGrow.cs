using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020002A2 RID: 674
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkGrow : Panel, IInkBaseline
  {
    // Token: 0x170004CA RID: 1226
    // (get) Token: 0x06001BCE RID: 7118 RVA: 0x005F4FE8 File Offset: 0x005F4FE8
    public double Fraction { get; }

    // Token: 0x06001BCF RID: 7119 RVA: 0x005F4FF0 File Offset: 0x005F4FF0
    public ClickyInkGrow(UIElement content, double fraction)
    {
      this.content = content;
      this.Fraction = fraction;
      base.Children.Add(content);
    }

    // Token: 0x170004CB RID: 1227
    // (get) Token: 0x06001BD0 RID: 7120 RVA: 0x005F5014 File Offset: 0x005F5014
    public double? FirstBaseline
    {
      get
      {
        return InkBaseline.First(this.content);
      }
    }

    // Token: 0x06001BD1 RID: 7121 RVA: 0x005F5024 File Offset: 0x005F5024
    protected override Size MeasureOverride(Size availableSize)
    {
      this.content.Measure(new Size(availableSize.Width, double.PositiveInfinity));
      Size desiredSize = this.content.DesiredSize;
      return new Size(desiredSize.Width, desiredSize.Height * Math.Max(0.0, Math.Min(1.0, this.Fraction)));
    }

    // Token: 0x06001BD2 RID: 7122 RVA: 0x005F5094 File Offset: 0x005F5094
    protected override Size ArrangeOverride(Size finalSize)
    {
      this.content.Arrange(new Rect(0.0, 0.0, finalSize.Width, this.content.DesiredSize.Height));
      return finalSize;
    }

    // Token: 0x06001BD3 RID: 7123 RVA: 0x005F50E0 File Offset: 0x005F50E0
    public static FrameworkElement Around(UIElement view, double clock, double landsAt, double? vanishesAt)
    {
      double num = ClickyInkClock.EaseInOut(ClickyInkClock.Progress(clock, landsAt, landsAt + 0.4));
      double num2;
      if (vanishesAt != null)
      {
        double valueOrDefault = vanishesAt.GetValueOrDefault();
        num2 = ClickyInkClock.Progress(clock, valueOrDefault, valueOrDefault + 0.45);
      }
      else
      {
        num2 = 0.0;
      }
      double num3 = num2;
      double num4 = 1.0 - ClickyInkClock.EaseInOut(num3);
      double num5 = 1.0 - ClickyInkClock.EaseInOut(Math.Min(1.0, num3 * 1.5));
      view.Opacity *= num5;
      return new ClickyInkGrow(view, Math.Min(num, num4));
    }

    // Token: 0x04000D62 RID: 3426
    private readonly UIElement content;
  }
}
