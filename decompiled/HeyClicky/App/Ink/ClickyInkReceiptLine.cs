using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200026A RID: 618
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkReceiptLine : Panel, IInkBaseline
  {
    // Token: 0x06001AA1 RID: 6817 RVA: 0x005E542C File Offset: 0x005E542C
    public ClickyInkReceiptLine(InkTextView label, [Nullable(2)] InkTextView amount, double? amountMaximumWidth)
    {
      this.label = label;
      this.amount = amount;
      this.amountMaximumWidth = amountMaximumWidth;
      this.leader = ClickyShapeView.Stroke(new ClickyInkLeader(), new PaperStroke(PaperColor.Hex("#2A2A2A").Opacity(0.4), 1.4, PenLineCap.Round, PenLineJoin.Round, new double[] { 0.1, 4.5 }));
      base.Children.Add(label);
      base.Children.Add(this.leader);
      if (amount != null)
      {
        base.Children.Add(amount);
      }
    }

    // Token: 0x17000497 RID: 1175
    // (get) Token: 0x06001AA2 RID: 6818 RVA: 0x005E54D8 File Offset: 0x005E54D8
    public double? FirstBaseline
    {
      get
      {
        return new double?(this.baseline);
      }
    }

    // Token: 0x06001AA3 RID: 6819 RVA: 0x005E54E8 File Offset: 0x005E54E8
    protected override Size MeasureOverride(Size availableSize)
    {
      double num = (double.IsInfinity(availableSize.Width) ? 1000.0 : availableSize.Width);
      double num2 = 0.0;
      double num3 = 4.0;
      if (this.amount != null)
      {
        this.amount.Measure(new Size(this.amountMaximumWidth.GetValueOrDefault(double.PositiveInfinity), double.PositiveInfinity));
        num2 = this.amount.DesiredSize.Width;
        num3 += 4.0;
      }
      this.label.Measure(new Size(Math.Max(0.0, num - num2 - num3 - 6.0), double.PositiveInfinity));
      this.leader.Measure(new Size(0f, 2f));
      double num4 = InkBaseline.First(this.label) ?? this.label.DesiredSize.Height;
      double num5 = ((this.amount == null) ? 0.0 : (InkBaseline.First(this.amount) ?? this.amount.DesiredSize.Height));
      this.baseline = Math.Max(Math.Max(num4, num5), 2.0);
      double num6 = Math.Max(this.label.DesiredSize.Height - num4, (this.amount == null) ? 0.0 : (this.amount.DesiredSize.Height - num5));
      return new Size(num, this.baseline + num6);
    }

    // Token: 0x06001AA4 RID: 6820 RVA: 0x005E56C0 File Offset: 0x005E56C0
    protected override Size ArrangeOverride(Size finalSize)
    {
      double num = InkBaseline.First(this.label) ?? this.label.DesiredSize.Height;
      this.label.Arrange(new Rect(0.0, this.baseline - num, this.label.DesiredSize.Width, this.label.DesiredSize.Height));
      double num2 = finalSize.Width;
      if (this.amount != null)
      {
        double num3 = InkBaseline.First(this.amount) ?? this.amount.DesiredSize.Height;
        num2 -= this.amount.DesiredSize.Width;
        this.amount.Arrange(new Rect(num2, this.baseline - num3, this.amount.DesiredSize.Width, this.amount.DesiredSize.Height));
        num2 -= 4.0;
      }
      double num4 = this.label.DesiredSize.Width + 4.0;
      this.leader.Arrange(new Rect(num4, this.baseline - 2.0, Math.Max(6.0, num2 - num4), 2.0));
      return finalSize;
    }

    // Token: 0x04000CC4 RID: 3268
    private const double Spacing = 4.0;

    // Token: 0x04000CC5 RID: 3269
    private const double MinimumLeader = 6.0;

    // Token: 0x04000CC6 RID: 3270
    private readonly InkTextView label;

    // Token: 0x04000CC7 RID: 3271
    [Nullable(2)]
    private readonly InkTextView amount;

    // Token: 0x04000CC8 RID: 3272
    private readonly double? amountMaximumWidth;

    // Token: 0x04000CC9 RID: 3273
    private readonly ClickyShapeView leader;

    // Token: 0x04000CCA RID: 3274
    private double baseline;
  }
}
