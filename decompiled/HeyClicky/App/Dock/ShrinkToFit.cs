using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002EC RID: 748
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class ShrinkToFit : Panel
  {
    // Token: 0x06002039 RID: 8249 RVA: 0x006154E0 File Offset: 0x006154E0
    public ShrinkToFit(FrameworkElement child, double minimumScale, int maxLines = 0)
    {
      this.child = child;
      this.minimumScale = minimumScale;
      this.maxLines = maxLines;
      child.RenderTransform = this.scale;
      base.Children.Add(child);
      ShrinkToFit.<>c__DisplayClass7_0 CS$<>8__locals1;
      CS$<>8__locals1.text = child as TextBlock;
      if (CS$<>8__locals1.text != null && maxLines > 0)
      {
        this.unlimitedProbe = ShrinkToFit.<.ctor>g__Probe|7_0(0, ref CS$<>8__locals1);
        this.oneLineProbe = ShrinkToFit.<.ctor>g__Probe|7_0(1, ref CS$<>8__locals1);
        base.Children.Add(this.unlimitedProbe);
        base.Children.Add(this.oneLineProbe);
      }
    }

    // Token: 0x0600203A RID: 8250 RVA: 0x00615594 File Offset: 0x00615594
    protected override Size MeasureOverride(Size available)
    {
      double num = double.PositiveInfinity;
      if (this.oneLineProbe != null)
      {
        this.oneLineProbe.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        num = this.oneLineProbe.DesiredSize.Height * (double)this.maxLines + 0.5;
      }
      double num2 = 1.0;
      for (;;)
      {
        Size size = new Size(double.IsInfinity(available.Width) ? double.PositiveInfinity : (available.Width / num2), double.PositiveInfinity);
        this.child.Measure(size);
        double num3 = this.child.DesiredSize.Height;
        if (this.unlimitedProbe != null)
        {
          this.unlimitedProbe.Measure(size);
          if (this.unlimitedProbe.DesiredSize.Height > num)
          {
            num3 = double.PositiveInfinity;
          }
        }
        if (num3 * num2 <= available.Height + 0.5 || num2 <= this.minimumScale)
        {
          break;
        }
        num2 = Math.Max(this.minimumScale, num2 - 0.025);
      }
      this.appliedScale = num2;
      Size desiredSize = this.child.DesiredSize;
      return new Size(Math.Min(desiredSize.Width * num2, available.Width), Math.Min(desiredSize.Height * num2, available.Height));
    }

    // Token: 0x0600203B RID: 8251 RVA: 0x00615714 File Offset: 0x00615714
    protected override Size ArrangeOverride(Size final)
    {
      double num = this.appliedScale;
      this.scale.ScaleX = (this.scale.ScaleY = num);
      this.child.Arrange(new Rect(0.0, 0.0, final.Width / num, this.child.DesiredSize.Height));
      TextBlock textBlock = this.unlimitedProbe;
      if (textBlock != null)
      {
        textBlock.Arrange(new Rect(0f, 0f, 0f, 0f));
      }
      TextBlock textBlock2 = this.oneLineProbe;
      if (textBlock2 != null)
      {
        textBlock2.Arrange(new Rect(0f, 0f, 0f, 0f));
      }
      return final;
    }

    // Token: 0x0600203C RID: 8252 RVA: 0x006157D8 File Offset: 0x006157D8
    [CompilerGenerated]
    internal static TextBlock <.ctor>g__Probe|7_0(int lines, ref ShrinkToFit.<>c__DisplayClass7_0 A_1)
    {
      return new TextBlock
      {
        Text = A_1.text.Text,
        FontSize = A_1.text.FontSize,
        FontWeight = A_1.text.FontWeight,
        FontFamily = A_1.text.FontFamily,
        TextWrapping = TextWrapping.Wrap,
        MaxLines = lines,
        IsTextScaleFactorEnabled = A_1.text.IsTextScaleFactorEnabled,
        Opacity = 0.0,
        IsHitTestVisible = false
      };
    }

    // Token: 0x04000F95 RID: 3989
    private readonly FrameworkElement child;

    // Token: 0x04000F96 RID: 3990
    private readonly double minimumScale;

    // Token: 0x04000F97 RID: 3991
    [Nullable(2)]
    private readonly TextBlock unlimitedProbe;

    // Token: 0x04000F98 RID: 3992
    [Nullable(2)]
    private readonly TextBlock oneLineProbe;

    // Token: 0x04000F99 RID: 3993
    private readonly int maxLines;

    // Token: 0x04000F9A RID: 3994
    private readonly ScaleTransform scale = new ScaleTransform();

    // Token: 0x04000F9B RID: 3995
    private double appliedScale = 1.0;
  }
}
