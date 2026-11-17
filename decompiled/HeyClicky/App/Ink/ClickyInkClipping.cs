using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200027C RID: 636
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkClipping
  {
    // Token: 0x06001AE2 RID: 6882 RVA: 0x005E9288 File Offset: 0x005E9288
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      StackPanel stackPanel = new StackPanel();
      stackPanel.Spacing = 8.0;
      stackPanel.Padding = new Thickness(14.0);
      stackPanel.Width = contentWidth - 40.0;
      stackPanel.Children.Add(ClickyInkWordsView.Make(block.Words, clock, style, ClickyInkObjectFonts.SerifFont(14.5, null), false, new Color?(ClickyInkClipping.Newsprint), null).With(delegate(InkTextView t)
      {
        t.Italic = true;
        t.LineSpacing = 2.0;
      }));
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 6.0
      };
      string text = block.Source["by"];
      if (text != null)
      {
        stackPanel2.Children.Add(InkTextView.Plain("— " + text, ClickyInkObjectFonts.SerifFont(11.0, new FontWeight?(FontWeights.SemiBold)), ClickyInkClipping.Newsprint));
      }
      string text2 = block.Source["source"] ?? block.Source["url"];
      if (text2 != null)
      {
        stackPanel2.Children.Add(InkTextView.Plain(ClickyInkClipping.Domain(text2), ClickyInkObjectFonts.SerifFont(10.5, null), ClickyInkClipping.Newsprint.Opacity(0.55)));
      }
      stackPanel.Children.Add(stackPanel2);
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(stackPanel, Array.Empty<UIElement>()).Behind(new ClickyInkShadowedShape(ClickyShape.Rectangle, "rect", PaperColor.Hex("#F1ECE1"), new PaperShadow(PaperColor.Black.Opacity(0.12), 2.5, 0.0, 1.5), null));
      clickyInkOverlay.Transformed(0.0, 0.0, -1.3, 1.0, null);
      clickyInkOverlay.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
      clickyInkOverlay.HorizontalAlignment = HorizontalAlignment.Left;
      return new Grid
      {
        Width = contentWidth,
        HorizontalAlignment = HorizontalAlignment.Left,
        Children = { clickyInkOverlay }
      };
    }

    // Token: 0x06001AE3 RID: 6883 RVA: 0x005E950C File Offset: 0x005E950C
    public static string Domain(string source)
    {
      return ClickyInkMadeLink.Domain(source);
    }

    // Token: 0x04000CE0 RID: 3296
    private static readonly Color Newsprint = PaperColor.Hex("#26231E");
  }
}
