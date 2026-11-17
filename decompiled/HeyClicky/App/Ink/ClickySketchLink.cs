using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001F5 RID: 501
  public sealed class ClickySketchLink : Pressable
  {
    // Token: 0x0600184F RID: 6223 RVA: 0x005CA554 File Offset: 0x005CA554
    [NullableContext(1)]
    public ClickySketchLink(string title, Action action, Color? color = null, double fontSize = 17.0)
      : base(true)
    {
      Color color2 = color ?? PaperColor.Hex("#6E7FA6");
      PaperFont paperFont = ClickyPaperFonts.Hand(fontSize);
      TextBlock textBlock = paperFont.Apply(new TextBlock
      {
        Text = title,
        Foreground = Ui.Brush(color2),
        TextWrapping = TextWrapping.NoWrap,
        TextDecorations = TextDecorations.Underline
      });
      double halfLeading = ClickyHandTextMetrics.For(paperFont, fontSize, 1.0).HalfLeading;
      textBlock.Margin = new Thickness(0.0, halfLeading, 0.0, halfLeading);
      base.Children.Add(textBlock);
      base.HorizontalAlignment = HorizontalAlignment.Left;
      base.VerticalAlignment = VerticalAlignment.Top;
      base.Click += action;
      ClickySketchPressStyle.Apply(this);
      AutomationProperties.SetName(this, title);
    }
  }
}
