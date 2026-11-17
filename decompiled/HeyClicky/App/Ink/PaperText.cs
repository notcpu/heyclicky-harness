using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001B4 RID: 436
  [NullableContext(1)]
  [Nullable(0)]
  public static class PaperText
  {
    // Token: 0x06001728 RID: 5928 RVA: 0x005BF7E8 File Offset: 0x005BF7E8
    public static T CssLineBox<[Nullable(0)] T>(this T element, PaperFont font, double lineHeight) where T : FrameworkElement
    {
      double num = (lineHeight - ClickyHandLineBreaker.NaturalLineHeight(font)) / 2.0;
      Thickness margin = element.Margin;
      element.Margin = new Thickness(margin.Left, margin.Top + num, margin.Right, margin.Bottom + num);
      return element;
    }

    // Token: 0x06001729 RID: 5929 RVA: 0x005BF848 File Offset: 0x005BF848
    public static TextBlock Line(string text, PaperFont font, Color color, TextAlignment alignment = TextAlignment.Left)
    {
      return font.Apply(new TextBlock
      {
        Text = text,
        Foreground = Ui.Brush(color),
        TextWrapping = TextWrapping.NoWrap,
        TextTrimming = TextTrimming.CharacterEllipsis,
        TextAlignment = alignment
      });
    }

    // Token: 0x0600172A RID: 5930 RVA: 0x005BF880 File Offset: 0x005BF880
    public static int CharacterCount(string text)
    {
      return new StringInfo(text).LengthInTextElements;
    }
  }
}
