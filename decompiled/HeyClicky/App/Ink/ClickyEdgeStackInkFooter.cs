using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000213 RID: 531
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyEdgeStackInkFooter
  {
    // Token: 0x0600194C RID: 6476 RVA: 0x005D073C File Offset: 0x005D073C
    [return: Nullable(2)]
    public static FrameworkElement Make(ClickyEdgeStackInkReading reading, [Nullable(2)] string liveStep, ClickyInkStyle style, int waitingLineLimit = 2, bool omitsWaiting = false)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 5.0
      };
      if (!reading.ChangeCounts.IsEmpty)
      {
        stackPanel.Children.Add(ClickyEdgeStackInkFooter.Changes(reading, style));
      }
      if (reading.CurrentState == ClickyEdgeStackInkReading.State.Writing)
      {
        stackPanel.Children.Add(ClickyEdgeStackInkFooter.Live(reading, liveStep, style));
      }
      string waitingLine = reading.WaitingLine;
      if (waitingLine != null && !omitsWaiting)
      {
        stackPanel.Children.Add(ClickyEdgeStackInkFooter.Waiting(waitingLine, (reading.CurrentState == ClickyEdgeStackInkReading.State.Writing) ? 1 : waitingLineLimit));
      }
      else
      {
        string pendingLine = reading.PendingLine;
        if (pendingLine != null && !omitsWaiting)
        {
          stackPanel.Children.Add(InkTextView.Plain("↳ " + pendingLine, ClickyPaperFonts.Hand(15.0), style.Fresh).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(1);
          }));
        }
      }
      if (stackPanel.Children.Count != 0)
      {
        return stackPanel;
      }
      return null;
    }

    // Token: 0x0600194D RID: 6477 RVA: 0x005D0838 File Offset: 0x005D0838
    private static FrameworkElement Changes(ClickyEdgeStackInkReading reading, ClickyInkStyle style)
    {
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(5.0, InkVerticalAlignment.Center, Array.Empty<UIElement>());
      ClickyEdgeStackInkReading.Changes changeCounts = reading.ChangeCounts;
      if (changeCounts.New > 0)
      {
        UIElementCollection children = clickyInkHStack.Children;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(changeCounts.New);
        defaultInterpolatedStringHandler.AppendLiteral(" new");
        children.Add(ClickyEdgeStackInkFooter.Chip(defaultInterpolatedStringHandler.ToStringAndClear(), ClickyPaperInk.DoneGreen));
      }
      if (changeCounts.Updated > 0)
      {
        UIElementCollection children2 = clickyInkHStack.Children;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(changeCounts.Updated);
        defaultInterpolatedStringHandler.AppendLiteral(" changed");
        children2.Add(ClickyEdgeStackInkFooter.Chip(defaultInterpolatedStringHandler.ToStringAndClear(), style.Fresh));
      }
      if (changeCounts.Gone > 0)
      {
        UIElementCollection children3 = clickyInkHStack.Children;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(changeCounts.Gone);
        defaultInterpolatedStringHandler.AppendLiteral(" gone");
        children3.Add(ClickyEdgeStackInkFooter.Chip(defaultInterpolatedStringHandler.ToStringAndClear(), PaperColor.Hex("#C0473A")));
      }
      clickyInkHStack.Children.Add(InkTextView.Plain("since you looked", ClickyPaperFonts.Hand(13.0), style.Quiet).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(1);
      }));
      clickyInkHStack.HorizontalAlignment = HorizontalAlignment.Left;
      return clickyInkHStack;
    }

    // Token: 0x0600194E RID: 6478 RVA: 0x005D0994 File Offset: 0x005D0994
    private static FrameworkElement Live(ClickyEdgeStackInkReading reading, [Nullable(2)] string liveStep, ClickyInkStyle style)
    {
      FrameworkElement frameworkElement = ClickyInkIcon.Glyph("pencil", 12.5, style.Fresh.Opacity(0.85), true) ?? new Grid();
      frameworkElement.Transformed(0.0, 0.0, -14.0, 1.0, null);
      InkTextView inkTextView = InkTextView.Plain(liveStep ?? (reading.IsPenBusy ? "writing it down" : "working on it"), ClickyPaperFonts.Hand(15.0), style.Fresh).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(1);
      });
      return new ClickyInkHStack(6.0, InkVerticalAlignment.Center, new UIElement[]
      {
        frameworkElement,
        ClickyInkHStack.Flexible<InkTextView>(inkTextView)
      })
      {
        HorizontalAlignment = HorizontalAlignment.Left
      };
    }

    // Token: 0x0600194F RID: 6479 RVA: 0x005D0A88 File Offset: 0x005D0A88
    private static FrameworkElement Waiting(string line, int lineLimit)
    {
      Grid grid = new Grid
      {
        Width = 15.0,
        Height = 15.0
      };
      grid.Children.Add(new Ellipse
      {
        Fill = Ui.Brush(ClickyEdgeStackInkLook.TapeColor(ClickyEdgeStackInkReading.State.NeedsYou))
      });
      TextBlock textBlock = PaperText.Line("?", ClickyPaperFonts.Rounded(10.0, FontWeights.ExtraBold), PaperColor.White, TextAlignment.Left);
      textBlock.HorizontalAlignment = HorizontalAlignment.Center;
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      grid.Children.Add(textBlock);
      grid.BaselineFromBottom(3.5);
      InkTextView inkTextView = InkTextView.Plain(line, ClickyPaperFonts.Hand(15.0), ClickyEdgeStackInkLook.WaitingAmber).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(lineLimit);
      });
      return new ClickyInkHStack(6.0, InkVerticalAlignment.FirstBaseline, new UIElement[]
      {
        grid,
        ClickyInkHStack.Flexible<InkTextView>(inkTextView)
      });
    }

    // Token: 0x06001950 RID: 6480 RVA: 0x005D0B84 File Offset: 0x005D0B84
    private static FrameworkElement Chip(string words, Color color)
    {
      InkTextView inkTextView = InkTextView.Plain(words, ClickyPaperFonts.Hand(12.5), color).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(1);
      });
      inkTextView.Margin = new Thickness(5.0, 0.0, 5.0, 0.0);
      ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(ClickyShape.Capsule, color.Opacity(0.45), 0.8);
      return new ClickyInkOverlay(inkTextView, new UIElement[] { clickyShapeView })
      {
        HorizontalAlignment = HorizontalAlignment.Left
      };
    }
  }
}
