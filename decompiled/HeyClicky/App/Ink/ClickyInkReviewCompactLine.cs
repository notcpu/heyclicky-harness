using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000296 RID: 662
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkReviewCompactLine : Panel, IInkBaseline
  {
    // Token: 0x06001B68 RID: 7016 RVA: 0x005EF7C8 File Offset: 0x005EF7C8
    public ClickyInkReviewCompactLine(FrameworkElement head, [Nullable(2)] FrameworkElement reason)
    {
      this.head = head;
      this.reason = reason;
      base.Children.Add(head);
      if (reason != null)
      {
        base.Children.Add(reason);
      }
    }

    // Token: 0x170004B5 RID: 1205
    // (get) Token: 0x06001B69 RID: 7017 RVA: 0x005EF7FC File Offset: 0x005EF7FC
    public double? FirstBaseline
    {
      get
      {
        return new double?(this.headBaseline);
      }
    }

    // Token: 0x06001B6A RID: 7018 RVA: 0x005EF80C File Offset: 0x005EF80C
    private static double Baseline(UIElement e)
    {
      double? num = InkBaseline.First(e);
      if (num == null)
      {
        return e.DesiredSize.Height;
      }
      return num.GetValueOrDefault();
    }

    // Token: 0x06001B6B RID: 7019 RVA: 0x005EF840 File Offset: 0x005EF840
    protected override Size MeasureOverride(Size availableSize)
    {
      this.head.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      Size desiredSize = this.head.DesiredSize;
      Size? size = null;
      if (this.reason != null)
      {
        this.reason.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        size = new Size?(this.reason.DesiredSize);
      }
      double num = (double.IsInfinity(availableSize.Width) ? (desiredSize.Width + ((size == null) ? 0.0 : (8.0 + size.GetValueOrDefault().Width))) : availableSize.Width);
      double num2 = Math.Min(desiredSize.Width, num);
      this.head.Measure(new Size(num2, double.PositiveInfinity));
      Size size2 = new Size(num2, this.head.DesiredSize.Height);
      double num3 = ClickyInkReviewCompactLine.Baseline(this.head);
      if (this.reason == null || size == null)
      {
        this.headFrame = new Rect(0.0, 0.0, size2.Width, size2.Height);
        this.headBaseline = num3;
        return new Size(num, size2.Height);
      }
      Size valueOrDefault = size.GetValueOrDefault();
      double num4 = num - num2 - 8.0;
      if (num4 >= Math.Min(valueOrDefault.Width, 110.0))
      {
        this.reason.Measure(new Size(Math.Min(valueOrDefault.Width, num4), double.PositiveInfinity));
        Size size3 = new Size(Math.Min(this.reason.DesiredSize.Width, num4), this.reason.DesiredSize.Height);
        double num5 = num3 - ClickyInkReviewCompactLine.Baseline(this.reason);
        double num6 = Math.Min(0.0, num5);
        this.reasonFrame = new Rect(num2 + 8.0, num5 - num6, size3.Width, size3.Height);
        double num7 = Math.Max(size2.Height - num6, this.reasonFrame.Bottom);
        this.headFrame = new Rect(0.0, -num6, size2.Width, size2.Height);
        this.headBaseline = num3 - num6;
        return new Size(num, num7);
      }
      this.reason.Measure(new Size(num, double.PositiveInfinity));
      this.reasonFrame = new Rect(0.0, size2.Height + 1.0, Math.Min(this.reason.DesiredSize.Width, num), this.reason.DesiredSize.Height);
      this.headFrame = new Rect(0.0, 0.0, size2.Width, size2.Height);
      this.headBaseline = num3;
      return new Size(num, this.reasonFrame.Bottom);
    }

    // Token: 0x06001B6C RID: 7020 RVA: 0x005EFB9C File Offset: 0x005EFB9C
    protected override Size ArrangeOverride(Size finalSize)
    {
      this.head.Arrange(this.headFrame);
      FrameworkElement frameworkElement = this.reason;
      if (frameworkElement != null)
      {
        frameworkElement.Arrange(this.reasonFrame);
      }
      return finalSize;
    }

    // Token: 0x04000D22 RID: 3362
    private const double Spacing = 8.0;

    // Token: 0x04000D23 RID: 3363
    private const double MinimumReasonWidth = 110.0;

    // Token: 0x04000D24 RID: 3364
    private readonly FrameworkElement head;

    // Token: 0x04000D25 RID: 3365
    [Nullable(2)]
    private readonly FrameworkElement reason;

    // Token: 0x04000D26 RID: 3366
    private Rect headFrame;

    // Token: 0x04000D27 RID: 3367
    private Rect reasonFrame;

    // Token: 0x04000D28 RID: 3368
    private double headBaseline;
  }
}
