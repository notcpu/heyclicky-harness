using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200028B RID: 651
  public static class ClickyInkMarginNote
  {
    // Token: 0x06001B47 RID: 6983 RVA: 0x005ED008 File Offset: 0x005ED008
    [NullableContext(1)]
    public static FrameworkElement Make(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      InkTextView inkTextView = InkTextView.Plain("↳", ClickyPaperFonts.Hand(20.0), style.Fresh);
      InkTextView inkTextView2 = ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand(18.0), true, new Color?(style.Fresh), null);
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(6.0, InkVerticalAlignment.FirstBaseline, new UIElement[] { inkTextView, inkTextView2 });
      clickyInkHStack.Transformed(0.0, 0.0, -1.2, 1.0, null);
      clickyInkHStack.HorizontalAlignment = HorizontalAlignment.Left;
      clickyInkHStack.Margin = new Thickness(18.0, 0.0, 0.0, 0.0);
      return new Grid
      {
        Width = contentWidth,
        HorizontalAlignment = HorizontalAlignment.Left,
        Children = { clickyInkHStack },
        Opacity = (ClickyInkWords.IsStarted(block.Words, clock) > false)
      };
    }
  }
}
