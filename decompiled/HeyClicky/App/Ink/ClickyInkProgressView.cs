using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200022B RID: 555
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkProgressView
  {
    // Token: 0x0600199E RID: 6558 RVA: 0x005D5208 File Offset: 0x005D5208
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, double width)
    {
      ClickyInkDocument.Block source = block.Source;
      ClickyInkProgress clickyInkProgress = new ClickyInkProgress(source.Attributes);
      double? num = (ClickyInkChange.IsLatest(source, style.LatestEdit) ? ClickyInkProgressView.PreviousFraction(source) : null);
      double num2 = ClickyInkClock.Progress(clock, block.LandsAt + 0.1, block.LandsAt + 0.8);
      double num3 = 8.0;
      InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.FirstBaseline;
      UIElement[] array = new UIElement[3];
      array[0] = ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand(19.0), true, null, null).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(1);
      });
      array[1] = new ClickyInkSpacer();
      array[2] = InkDashboardLayout.Fixed(ClickyInkTickingNumber.Create(source, ClickyInkProgressView.ValueKey(source), clock, style, 18.0, 0.2, clickyInkProgress.Shown, (num != null) ? ClickyInkProgressView.PreviousShown(source) : null, false));
      ClickyInkHStack clickyInkHStack = InkDashboardLayout.HStack(num3, inkVerticalAlignment, array);
      clickyInkHStack.Width = width;
      StackPanel stackPanel = InkDashboardLayout.Width<StackPanel>(new StackPanel
      {
        Spacing = 6.0,
        Children = { clickyInkHStack }
      }, width);
      stackPanel.Children.Add(ClickyInkProgressBar.Create(clickyInkProgress.Fill, num, ClickyInkChange.Progress(source, clock, 0.2, 0.9), num2, 15.0, style, block.Id.CharacterCount(), new double?(width)));
      string text = source["note"];
      if (text != null && text.Length > 0)
      {
        InkTextView inkTextView = InkTextView.Plain(text, ClickyPaperFonts.Hand(15.0), style.Quiet).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
        });
        inkTextView.Margin = new Thickness(0.0, 3.0, 0.0, 0.0);
        inkTextView.Opacity = num2;
        stackPanel.Children.Add(inkTextView);
      }
      return stackPanel;
    }

    // Token: 0x0600199F RID: 6559 RVA: 0x005D5454 File Offset: 0x005D5454
    public static string ValueKey(ClickyInkDocument.Block block)
    {
      return ClickyInkDashboardViewMath.ProgressValueKey(block);
    }

    // Token: 0x060019A0 RID: 6560 RVA: 0x005D545C File Offset: 0x005D545C
    public static double? PreviousFraction(ClickyInkDocument.Block block)
    {
      return ClickyInkDashboardViewMath.ProgressPreviousFraction(block);
    }

    // Token: 0x060019A1 RID: 6561 RVA: 0x005D5464 File Offset: 0x005D5464
    [return: Nullable(2)]
    public static string PreviousShown(ClickyInkDocument.Block block)
    {
      return ClickyInkDashboardViewMath.ProgressPreviousShown(block);
    }
  }
}
