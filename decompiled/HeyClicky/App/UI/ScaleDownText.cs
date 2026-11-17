using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.UI
{
  // Token: 0x020000BB RID: 187
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ScaleDownText : Panel
  {
    // Token: 0x0600079B RID: 1947 RVA: 0x00539740 File Offset: 0x00539740
    public ScaleDownText(TextBlock text, double minimumScale)
    {
      this.text = text;
      this.minimumScale = minimumScale;
      text.TextWrapping = TextWrapping.NoWrap;
      text.TextTrimming = TextTrimming.CharacterEllipsis;
      text.RenderTransform = this.scale;
      base.Children.Add(text);
    }

    // Token: 0x0600079C RID: 1948 RVA: 0x005397A4 File Offset: 0x005397A4
    protected override Size MeasureOverride(Size available)
    {
      this.text.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      Size desiredSize = this.text.DesiredSize;
      double num = 1.0;
      if (!double.IsInfinity(available.Width) && desiredSize.Width > available.Width && desiredSize.Width > 0.0)
      {
        num = Math.Max(this.minimumScale, available.Width / desiredSize.Width);
      }
      this.appliedScale = num;
      if (num >= 1.0)
      {
        return desiredSize;
      }
      double num2 = Math.Min(desiredSize.Width, available.Width / num);
      this.text.Measure(new Size(num2, double.PositiveInfinity));
      return new Size(Math.Min(num2 * num, available.Width), desiredSize.Height * num);
    }

    // Token: 0x0600079D RID: 1949 RVA: 0x00539898 File Offset: 0x00539898
    protected override Size ArrangeOverride(Size final)
    {
      double num = this.appliedScale;
      this.scale.ScaleX = (this.scale.ScaleY = num);
      this.text.Arrange(new Rect(0.0, 0.0, final.Width / num, final.Height / num));
      return final;
    }

    // Token: 0x0400024A RID: 586
    private readonly TextBlock text;

    // Token: 0x0400024B RID: 587
    private readonly double minimumScale;

    // Token: 0x0400024C RID: 588
    private readonly ScaleTransform scale = new ScaleTransform();

    // Token: 0x0400024D RID: 589
    private double appliedScale = 1.0;
  }
}
