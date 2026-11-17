using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001B3 RID: 435
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyHandText : Panel
  {
    // Token: 0x06001719 RID: 5913 RVA: 0x005BF22C File Offset: 0x005BF22C
    public ClickyHandText(string text, double size, double lineHeight = 1.2, int? lineLimit = null, double minimumScale = 1.0, TextAlignment alignment = TextAlignment.Left)
      : this(text, ClickyPaperFonts.Hand(size), ClickyHandTextMetrics.For(ClickyPaperFonts.Hand(size), size, lineHeight), lineLimit, minimumScale, alignment, false)
    {
    }

    // Token: 0x0600171A RID: 5914 RVA: 0x005BF250 File Offset: 0x005BF250
    private ClickyHandText(string text, PaperFont font, ClickyHandTextMetrics metrics, int? lineLimit, double minimumScale, TextAlignment alignment, bool paragraphOnly)
    {
      this.cssLineLimitOne = paragraphOnly && lineLimit.GetValueOrDefault() == 1;
      this.text = text;
      this.font = font;
      this.metrics = metrics;
      this.lineLimit = lineLimit;
      this.minimumScale = minimumScale;
      this.alignment = alignment;
      AutomationProperties.SetName(this, text);
      if (lineLimit.GetValueOrDefault() == 1 && !paragraphOnly)
      {
        this.single = font.Apply(new TextBlock
        {
          Text = text,
          TextWrapping = TextWrapping.NoWrap,
          TextTrimming = TextTrimming.CharacterEllipsis,
          TextAlignment = alignment,
          Margin = new Thickness(0.0, metrics.HalfLeading, 0.0, metrics.HalfLeading),
          Foreground = Ui.Brush(this.foreground)
        });
        UIElementCollection children = base.Children;
        FrameworkElement frameworkElement;
        if (minimumScale < 1.0)
        {
          TextBlock textBlock = this.single;
          HorizontalAlignment horizontalAlignment;
          if (alignment != TextAlignment.Center)
          {
            if (alignment != TextAlignment.Right)
            {
              horizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
              horizontalAlignment = HorizontalAlignment.Right;
            }
          }
          else
          {
            horizontalAlignment = HorizontalAlignment.Center;
          }
          frameworkElement = Ui.Shrink(textBlock, horizontalAlignment, minimumScale);
        }
        else
        {
          frameworkElement = this.single;
        }
        children.Add(frameworkElement);
      }
    }

    // Token: 0x0600171B RID: 5915 RVA: 0x005BF38C File Offset: 0x005BF38C
    public static ClickyHandText CssText(string text, PaperFont font, double lineHeight, TextAlignment alignment = TextAlignment.Left, int? lineLimit = null)
    {
      return new ClickyHandText(text, font, ClickyHandTextMetrics.For(font, font.Size, lineHeight / font.Size), lineLimit, 1.0, alignment, true);
    }

    // Token: 0x17000414 RID: 1044
    // (get) Token: 0x0600171C RID: 5916 RVA: 0x005BF3B8 File Offset: 0x005BF3B8
    public string Text
    {
      get
      {
        return this.text;
      }
    }

    // Token: 0x17000415 RID: 1045
    // (get) Token: 0x0600171D RID: 5917 RVA: 0x005BF3C0 File Offset: 0x005BF3C0
    public PaperFont Font
    {
      get
      {
        return this.font;
      }
    }

    // Token: 0x17000416 RID: 1046
    // (get) Token: 0x0600171E RID: 5918 RVA: 0x005BF3C8 File Offset: 0x005BF3C8
    public ClickyHandTextMetrics Metrics
    {
      get
      {
        return this.metrics;
      }
    }

    // Token: 0x17000417 RID: 1047
    // (get) Token: 0x0600171F RID: 5919 RVA: 0x005BF3D0 File Offset: 0x005BF3D0
    public double FirstBaseline
    {
      get
      {
        return this.metrics.HalfLeading + ClickyHandLineBreaker.Ascent(this.font);
      }
    }

    // Token: 0x17000418 RID: 1048
    // (get) Token: 0x06001720 RID: 5920 RVA: 0x005BF3EC File Offset: 0x005BF3EC
    public double LastBaseline
    {
      get
      {
        return (double)Math.Max(1, (this.single == null) ? this.lines.Count : 1) * this.metrics.Pitch - this.metrics.Pitch + this.FirstBaseline;
      }
    }

    // Token: 0x17000419 RID: 1049
    // (get) Token: 0x06001721 RID: 5921 RVA: 0x005BF42C File Offset: 0x005BF42C
    // (set) Token: 0x06001722 RID: 5922 RVA: 0x005BF434 File Offset: 0x005BF434
    public Color Foreground
    {
      get
      {
        return this.foreground;
      }
      set
      {
        this.foreground = value;
        SolidColorBrush solidColorBrush = Ui.Brush(value);
        if (this.single != null)
        {
          this.single.Foreground = solidColorBrush;
        }
        foreach (UIElement uielement in base.Children)
        {
          TextBlock textBlock = uielement as TextBlock;
          if (textBlock != null)
          {
            textBlock.Foreground = solidColorBrush;
          }
        }
      }
    }

    // Token: 0x06001723 RID: 5923 RVA: 0x005BF4AC File Offset: 0x005BF4AC
    public ClickyHandText WithForeground(Color color)
    {
      this.Foreground = color;
      return this;
    }

    // Token: 0x06001724 RID: 5924 RVA: 0x005BF4B8 File Offset: 0x005BF4B8
    protected override Size MeasureOverride(Size availableSize)
    {
      if (this.single != null)
      {
        FrameworkElement frameworkElement = (FrameworkElement)base.Children[0];
        frameworkElement.Measure(availableSize);
        return frameworkElement.DesiredSize;
      }
      double width = availableSize.Width;
      IReadOnlyList<string> readOnlyList = ClickyHandLineBreaker.Lines(this.text, this.font, width, this.cssLineLimitOne ? new int?(1) : this.lineLimit);
      this.SetLines(readOnlyList);
      double num = Math.Ceiling(readOnlyList.Select<string, double>((string l) => ClickyHandLineBreaker.Width(l, this.font)).DefaultIfEmpty(0.0).Max());
      double num2 = (double.IsFinite(width) ? Math.Min(num, width) : num);
      foreach (UIElement uielement in base.Children)
      {
        uielement.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      }
      return new Size(num2, (double)readOnlyList.Count * this.metrics.Pitch);
    }

    // Token: 0x06001725 RID: 5925 RVA: 0x005BF5D4 File Offset: 0x005BF5D4
    protected override Size ArrangeOverride(Size finalSize)
    {
      if (this.single != null)
      {
        base.Children[0].Arrange(new Rect(0.0, 0.0, finalSize.Width, finalSize.Height));
        return finalSize;
      }
      for (int i = 0; i < base.Children.Count; i++)
      {
        TextBlock textBlock = (TextBlock)base.Children[i];
        textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        double width = textBlock.DesiredSize.Width;
        TextAlignment textAlignment = this.alignment;
        double num;
        if (textAlignment != TextAlignment.Center)
        {
          if (textAlignment != TextAlignment.Right)
          {
            num = 0.0;
          }
          else
          {
            num = finalSize.Width - width;
          }
        }
        else
        {
          num = (finalSize.Width - width) / 2.0;
        }
        double num2 = num;
        double num3 = (double)i * this.metrics.Pitch + this.metrics.HalfLeading;
        textBlock.Arrange(new Rect(num2, num3, width, textBlock.DesiredSize.Height));
      }
      return finalSize;
    }

    // Token: 0x06001726 RID: 5926 RVA: 0x005BF6FC File Offset: 0x005BF6FC
    private void SetLines(IReadOnlyList<string> broken)
    {
      if (this.lines.SequenceEqual<string>(broken))
      {
        return;
      }
      this.lines = broken;
      SolidColorBrush solidColorBrush = Ui.Brush(this.foreground);
      while (base.Children.Count > broken.Count)
      {
        base.Children.RemoveAt(base.Children.Count - 1);
      }
      for (int i = 0; i < broken.Count; i++)
      {
        if (i < base.Children.Count)
        {
          ((TextBlock)base.Children[i]).Text = broken[i];
        }
        else
        {
          TextBlock textBlock = this.font.Apply(new TextBlock
          {
            Text = broken[i],
            TextWrapping = TextWrapping.NoWrap,
            Foreground = solidColorBrush
          });
          AutomationProperties.SetAccessibilityView(textBlock, AccessibilityView.Raw);
          base.Children.Add(textBlock);
        }
      }
    }

    // Token: 0x04000B48 RID: 2888
    private readonly string text;

    // Token: 0x04000B49 RID: 2889
    private readonly PaperFont font;

    // Token: 0x04000B4A RID: 2890
    private readonly ClickyHandTextMetrics metrics;

    // Token: 0x04000B4B RID: 2891
    private readonly int? lineLimit;

    // Token: 0x04000B4C RID: 2892
    private readonly double minimumScale;

    // Token: 0x04000B4D RID: 2893
    private readonly TextAlignment alignment;

    // Token: 0x04000B4E RID: 2894
    [Nullable(2)]
    private readonly TextBlock single;

    // Token: 0x04000B4F RID: 2895
    private IReadOnlyList<string> lines = Array.Empty<string>();

    // Token: 0x04000B50 RID: 2896
    private Color foreground = PaperColor.Black;

    // Token: 0x04000B51 RID: 2897
    private readonly bool cssLineLimitOne;
  }
}
