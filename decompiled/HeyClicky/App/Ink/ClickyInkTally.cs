using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200022D RID: 557
  public static class ClickyInkTally
  {
    // Token: 0x060019A6 RID: 6566 RVA: 0x005D58BC File Offset: 0x005D58BC
    [NullableContext(1)]
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, double width)
    {
      ClickyInkDocument.Block source = block.Source;
      ClickyInkTallyReading clickyInkTallyReading = new ClickyInkTallyReading(source, style.LatestEdit);
      double num = ClickyInkClock.Progress(clock, block.LandsAt + 0.1, block.LandsAt + 1.0);
      double num2 = 5.0;
      InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.FirstBaseline;
      UIElement[] array = new UIElement[2];
      array[0] = ClickyInkTickingNumber.Create(source, clickyInkTallyReading.Key, clock, style, 28.0, 0.3, null, null, true);
      int num3 = 1;
      string text = source["goal"] ?? source["of"];
      array[num3] = ((text != null) ? InkDashboardLayout.Fixed(InkTextView.Plain("of " + text, ClickyPaperFonts.Hand(15.0), style.Quiet)) : null);
      ClickyInkHStack clickyInkHStack = InkDashboardLayout.HStack(num2, inkVerticalAlignment, array);
      StackPanel stackPanel = new StackPanel();
      stackPanel.Children.Add(ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand(18.0), true, null, null).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(2);
      }));
      stackPanel.Children.Add(clickyInkHStack);
      StackPanel stackPanel2 = InkDashboardLayout.Width<StackPanel>(stackPanel, width * 0.32);
      int num4 = (int)Math.Min(2147483647L, clickyInkTallyReading.Fresh);
      FrameworkElement frameworkElement = ClickyInkTallyMarks.Create((int)Math.Min(2147483647L, clickyInkTallyReading.Count), num4, ClickyInkChange.Progress(source, clock, 0.3, Math.Max(0.6, (double)num4 * 0.16)), num, 20.0, style, block.Id.CharacterCount(), new double?(Math.Max(0.0, width - width * 0.32 - 16.0)), new double?((double)48));
      frameworkElement.Margin = new Thickness(0.0, 4.0, 0.0, 0.0);
      ClickyInkHStack clickyInkHStack2 = InkDashboardLayout.Width<ClickyInkHStack>(InkDashboardLayout.HStack(16.0, InkVerticalAlignment.Top, new UIElement[] { stackPanel2, frameworkElement }), width);
      string text2 = source["note"];
      if (text2 == null)
      {
        return clickyInkHStack2;
      }
      FrameworkElement frameworkElement2 = clickyInkHStack2;
      if (text2.Length > 0)
      {
        InkTextView inkTextView = InkTextView.Plain(text2, ClickyPaperFonts.Hand(14.0), style.Fresh).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
        });
        inkTextView.VerticalAlignment = VerticalAlignment.Bottom;
        inkTextView.HorizontalAlignment = HorizontalAlignment.Left;
        inkTextView.Transformed(0.0, 16.0, 0.0, 1.0, null);
        frameworkElement2 = new ClickyInkOverlay(clickyInkHStack2, new UIElement[] { inkTextView });
      }
      frameworkElement2.Margin = new Thickness(0.0, 0.0, 0.0, 16.0);
      return frameworkElement2;
    }
  }
}
