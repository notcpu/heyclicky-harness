using System;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000252 RID: 594
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class InkMiddleTruncatedText : Panel, IInkBaseline
  {
    // Token: 0x06001A32 RID: 6706 RVA: 0x005DDB08 File Offset: 0x005DDB08
    public InkMiddleTruncatedText(string text, PaperFont font, Color color)
    {
      this.text = text;
      this.block = font.Apply(new TextBlock
      {
        Text = text,
        Foreground = Ui.Brush(color),
        TextWrapping = TextWrapping.NoWrap
      });
      this.block.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
      this.block.LineHeight = font.Size * 1.193;
      base.Children.Add(this.block);
    }

    // Token: 0x1700048C RID: 1164
    // (get) Token: 0x06001A33 RID: 6707 RVA: 0x005DDB88 File Offset: 0x005DDB88
    public double? FirstBaseline
    {
      get
      {
        if (this.block.BaselineOffset <= 0.0)
        {
          return null;
        }
        return new double?(this.block.BaselineOffset);
      }
    }

    // Token: 0x06001A34 RID: 6708 RVA: 0x005DDBC8 File Offset: 0x005DDBC8
    protected override Size MeasureOverride(Size availableSize)
    {
      this.block.Text = this.text;
      this.block.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      if (double.IsInfinity(availableSize.Width) || this.block.DesiredSize.Width <= availableSize.Width)
      {
        return this.block.DesiredSize;
      }
      string[] array = this.text.Characters();
      int i = 0;
      int num = array.Length;
      string text = "…";
      while (i <= num)
      {
        int num2 = (i + num) / 2;
        int num3 = (num2 + 1) / 2;
        int num4 = num2 / 2;
        string text2 = string.Concat(array.Take<string>(num3)) + "…" + string.Concat(array.Skip<string>(array.Length - num4));
        this.block.Text = text2;
        this.block.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        if (this.block.DesiredSize.Width <= availableSize.Width)
        {
          text = text2;
          i = num2 + 1;
        }
        else
        {
          num = num2 - 1;
        }
      }
      this.block.Text = text;
      this.block.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      return new Size(Math.Min(this.block.DesiredSize.Width, availableSize.Width), this.block.DesiredSize.Height);
    }

    // Token: 0x06001A35 RID: 6709 RVA: 0x005DDD64 File Offset: 0x005DDD64
    protected override Size ArrangeOverride(Size finalSize)
    {
      this.block.Arrange(new Rect(0.0, 0.0, finalSize.Width, finalSize.Height));
      return finalSize;
    }

    // Token: 0x04000CA0 RID: 3232
    private readonly string text;

    // Token: 0x04000CA1 RID: 3233
    private readonly TextBlock block;
  }
}
