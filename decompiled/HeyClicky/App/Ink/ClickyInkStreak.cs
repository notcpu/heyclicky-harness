using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200022F RID: 559
  public static class ClickyInkStreak
  {
    // Token: 0x060019A9 RID: 6569 RVA: 0x005D5FA0 File Offset: 0x005D5FA0
    [NullableContext(1)]
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, double width)
    {
      ClickyInkDocument.Block source = block.Source;
      ClickyInkStreakReading clickyInkStreakReading = new ClickyInkStreakReading(source);
      double num = ClickyInkClock.Progress(clock, block.LandsAt + 0.1, block.LandsAt + 0.9);
      double num2 = 8.0;
      InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.FirstBaseline;
      UIElement[] array = new UIElement[4];
      array[0] = ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand(19.0), true, null, null).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(1);
      });
      array[1] = new ClickyInkSpacer();
      array[2] = InkDashboardLayout.Fixed(ClickyInkTickingNumber.Create(source, "_streak", clock, style, 22.0, 0.4, clickyInkStreakReading.Shown, clickyInkStreakReading.PreviousShown, false));
      array[3] = InkDashboardLayout.Fixed(InkTextView.Plain(clickyInkStreakReading.Unit, ClickyPaperFonts.Hand(15.0), style.Quiet));
      ClickyInkHStack clickyInkHStack = InkDashboardLayout.HStack(num2, inkVerticalAlignment, array);
      clickyInkHStack.Width = width;
      StackPanel stackPanel = InkDashboardLayout.Width<StackPanel>(new StackPanel
      {
        Spacing = 6.0,
        Children = { clickyInkHStack }
      }, width);
      stackPanel.Children.Add(ClickyInkStreakBoxes.Create(clickyInkStreakReading.Days, ClickyInkChange.IsLatest(source, style.LatestEdit) ? clickyInkStreakReading.PreviousCount : null, ClickyInkChange.Progress(source, clock, 0.2, 0.6), num, 17.0, 14, style, block.Id.CharacterCount()));
      string text = source["note"];
      if (text != null && text.Length > 0)
      {
        InkTextView inkTextView = InkTextView.Plain(text, ClickyPaperFonts.Hand(15.0), style.Quiet).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
        });
        inkTextView.Opacity = num;
        stackPanel.Children.Add(inkTextView);
      }
      return stackPanel;
    }
  }
}
