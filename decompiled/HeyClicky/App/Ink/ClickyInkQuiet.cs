using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000234 RID: 564
  public static class ClickyInkQuiet
  {
    // Token: 0x060019B1 RID: 6577 RVA: 0x005D6D30 File Offset: 0x005D6D30
    [NullableContext(1)]
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, double width, bool isHeadline = false)
    {
      ClickyInkQuietReading clickyInkQuietReading = new ClickyInkQuietReading(block.Source);
      double num = ClickyInkClock.Progress(clock, block.LandsAt + 0.05, block.LandsAt + 0.35);
      Grid grid = new Grid
      {
        Children = 
        {
          new ClickyPaperStamp(ClickyThingStatus.Ready, "ALL QUIET", isHeadline ? 13.0 : 10.5, -6.0)
        }
      };
      grid.Transformed(0.0, 0.0, 0.0, 1.0 + 0.35 * (1.0 - num), null);
      grid.Opacity = ((num > 0.0) ? (0.3 + 0.7 * num) : 0.0);
      StackPanel stackPanel = new StackPanel();
      stackPanel.Spacing = 2.0;
      stackPanel.Children.Add(ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand((double)(isHeadline ? 26 : 19)), true, null, null).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(2);
      }));
      StackPanel stackPanel2 = stackPanel;
      string checkedLine = clickyInkQuietReading.CheckedLine;
      if (checkedLine != null)
      {
        InkTextView inkTextView = InkTextView.Plain(checkedLine, ClickyPaperFonts.Typed(isHeadline ? 12.5 : 11.5, new FontWeight?(FontWeights.Medium)), style.Quiet).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
        });
        inkTextView.Opacity = ClickyInkClock.Progress(clock, block.Words.FinishedAt, block.Words.FinishedAt + 0.3);
        stackPanel2.Children.Add(inkTextView);
      }
      if (clickyInkQuietReading.Runs > 1L)
      {
        int num2 = (int)Math.Min(clickyInkQuietReading.Runs, 12L);
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(4.0, InkVerticalAlignment.Center, Array.Empty<UIElement>());
        for (int i = 0; i < num2; i++)
        {
          clickyInkHStack.Children.Add(new Ellipse
          {
            Width = 5.0,
            Height = 5.0,
            Fill = Ui.Brush(ClickyPaperInk.DoneGreen.Opacity((i == num2 - 1) ? 0.95 : 0.45))
          });
        }
        UIElementCollection children = clickyInkHStack.Children;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 1);
        defaultInterpolatedStringHandler.AppendFormatted<long>(clickyInkQuietReading.Runs);
        defaultInterpolatedStringHandler.AppendLiteral(" quiet runs in a row");
        children.Add(InkTextView.Plain(defaultInterpolatedStringHandler.ToStringAndClear(), ClickyPaperFonts.Hand(14.0), style.Quiet));
        clickyInkHStack.HorizontalAlignment = HorizontalAlignment.Left;
        clickyInkHStack.Opacity = ClickyInkClock.Progress(clock, block.Words.FinishedAt + 0.1, block.Words.FinishedAt + 0.4);
        stackPanel2.Children.Add(clickyInkHStack);
      }
      return InkDashboardLayout.Width<ClickyInkHStack>(InkDashboardLayout.HStack(14.0, InkVerticalAlignment.Center, new UIElement[]
      {
        grid,
        ClickyInkHStack.Flexible<StackPanel>(stackPanel2),
        new ClickyInkSpacer()
      }), width);
    }
  }
}
