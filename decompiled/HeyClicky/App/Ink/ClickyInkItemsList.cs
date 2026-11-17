using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000258 RID: 600
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkItemsList
  {
    // Token: 0x06001A4C RID: 6732 RVA: 0x005DFC28 File Offset: 0x005DFC28
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      List<string> list = ClickyInkItemsRules.Buttons(block.Source);
      InkTextView inkTextView = ((block.Words.Current.Length == 0) ? InkViews.Plain(" ", ClickyPaperFonts.Hand(19.0), style.Ink, null) : InkViews.Words(block.Words, clock, style, ClickyPaperFonts.Hand(19.0), null, true, null));
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(new ClickyInkCentered(InkViews.Plain(block.Items.Count.ToString(CultureInfo.InvariantCulture), InkObjectFonts.Rounded(12.0, FontWeights.ExtraBold), ClickyInkItemsList.Red, null), 26.0, 20.0), new UIElement[] { ClickyShapeView.Stroke(new ClickyHandRing(block.Items.Count + 3, 0.16), new PaperStroke(ClickyInkItemsList.Red.Opacity(0.65), 1.3, PenLineCap.Flat, PenLineJoin.Miter, null)) });
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(8.0, InkVerticalAlignment.FirstBaseline, new UIElement[]
      {
        ClickyInkHStack.Flexible<InkTextView>(inkTextView),
        new ClickyInkSpacer(),
        clickyInkOverlay
      })
      {
        Margin = new Thickness(0.0, 0.0, 0.0, 7.0)
      };
      StackPanel stackPanel = InkViews.VStack(0.0, new UIElement[]
      {
        clickyInkHStack,
        new Border
        {
          Height = 1.2,
          Background = Ui.Brush(PaperColor.Hex("#E5484D").Opacity(0.4))
        }
      });
      for (int i = 0; i < block.Items.Count; i++)
      {
        stackPanel.Children.Add(ClickyInkItemsList.Row(block, block.Items[i], list, clock, style, actions));
        if (i < block.Items.Count - 1)
        {
          stackPanel.Children.Add(new Border
          {
            Height = 1.0,
            Background = Ui.Brush(PaperColor.Hex("#6E9BD6").Opacity(0.25))
          });
        }
      }
      stackPanel.Margin = new Thickness(16.0, 15.0, 16.0, 9.0);
      InkPaperSheet inkPaperSheet = new InkPaperSheet("items-card", ClickyShape.RoundedRectangle(3.0), PaperFill.Of(PaperColor.Hex("#FFFEF9")), new double?(0.22), new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.07), 2.0, 0.0, 1.0)
      }, null, null);
      FrameworkElement frameworkElement = ClickyInkMaskingTape.Make(60.0, -2.5, null);
      frameworkElement.HorizontalAlignment = HorizontalAlignment.Center;
      frameworkElement.VerticalAlignment = VerticalAlignment.Top;
      frameworkElement.Transformed(0.0, -7.0, 0.0, 1.0, null);
      ClickyInkOverlay clickyInkOverlay2 = new ClickyInkOverlay(stackPanel, new UIElement[] { frameworkElement }).Behind(inkPaperSheet);
      clickyInkOverlay2.Width = contentWidth - 8.0;
      clickyInkOverlay2.HorizontalAlignment = HorizontalAlignment.Left;
      clickyInkOverlay2.Transformed(0.0, 0.0, 0.15, 1.0, null);
      return new Grid
      {
        Width = contentWidth,
        HorizontalAlignment = HorizontalAlignment.Left,
        Padding = new Thickness(0.0, 6.0, 0.0, 0.0),
        Children = { clickyInkOverlay2 }
      };
    }

    // Token: 0x06001A4D RID: 6733 RVA: 0x005E0054 File Offset: 0x005E0054
    private static FrameworkElement Row(ClickyInkLiveNote.Block block, ClickyInkLiveNote.Item item, List<string> buttons, double clock, ClickyInkStyle style, ClickyInkActions actions)
    {
      ClickyInkDocument.Item source = item.Source;
      bool flag = source.Mark.GetValueOrDefault() == ClickyInkDocument.Mark.Done;
      bool isOut = source.Mark.GetValueOrDefault() == ClickyInkDocument.Mark.Gone;
      string text = ClickyInkNoteActions.ItemKey(block.Source, source.Text);
      ClickyInkSentAction valueOrDefault = actions.Feedback.Answered.GetValueOrDefault(text);
      ClickyInkSentAction pending = actions.Feedback.Pending;
      bool flag2 = ((pending != null) ? pending.ObjectKey : null) == text;
      InkTextView inkTextView = InkViews.Words(item.Words, clock, style, ClickyPaperFonts.Typed(13.0, new FontWeight?((source.Mark.GetValueOrDefault() == ClickyInkDocument.Mark.Pick) ? FontWeights.SemiBold : FontWeights.Normal)), isOut ? new Color?(style.Quiet) : null, true, delegate(InkTextView t)
      {
        t.LineLimit = new int?(2);
        if (isOut)
        {
          t.Strikethrough = new Color?(style.Quiet);
        }
      });
      StackPanel stackPanel = InkViews.VStack(2.0, new UIElement[] { inkTextView });
      ClickyInkLiveNote.Words detail = item.Detail;
      if (detail != null && ClickyInkWords.IsStarted(detail, clock))
      {
        stackPanel.Children.Add(InkViews.Words(detail, clock, style, ClickyPaperFonts.Typed(11.0, null), new Color?(style.Quiet), false, delegate(InkTextView t)
        {
          t.LineLimit = new int?(2);
        }));
      }
      FrameworkElement frameworkElement = stackPanel;
      string url = source.Url;
      if (url != null)
      {
        Pressable pressable = InkViews.PlainButton(stackPanel, delegate
        {
          actions.Open(url);
        }, null);
        pressable.HorizontalAlignment = HorizontalAlignment.Stretch;
        frameworkElement = pressable;
      }
      else
      {
        stackPanel.Background = Ui.Transparent;
      }
      FrameworkElement frameworkElement2 = null;
      if (flag)
      {
        frameworkElement2 = ClickyInkTag.Make("done", ClickyPaperInk.DoneGreen);
      }
      else if (isOut)
      {
        frameworkElement2 = ClickyInkTag.Make("skipped", style.Quiet);
      }
      else if (valueOrDefault != null)
      {
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(4.0, InkVerticalAlignment.Center, Array.Empty<UIElement>());
        if (flag2)
        {
          clickyInkHStack.Children.Add(InkViews.Spinner(12.0));
        }
        clickyInkHStack.Children.Add(InkViews.Plain(valueOrDefault.MarginWords, ClickyPaperFonts.Hand(14.0), style.Fresh, delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
        }));
        frameworkElement2 = clickyInkHStack;
      }
      else if (clock >= item.Words.FinishedAt)
      {
        StackPanel stackPanel2 = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 6.0,
          VerticalAlignment = VerticalAlignment.Center
        };
        for (int i = 0; i < buttons.Count; i++)
        {
          string title = buttons[i];
          stackPanel2.Children.Add(new ClickyPaperGelButton(title, delegate
          {
            ClickyInkNoteAction clickyInkNoteAction = ClickyInkNoteActions.ItemAction(block.Source, source.Text, title);
            if (clickyInkNoteAction != null)
            {
              actions.Perform(clickyInkNoteAction);
            }
          }, (i == 0) ? ClickyPaperGel.Blue : ClickyPaperGel.White, 24.0, 10.0, 11.5, null));
        }
        frameworkElement2 = stackPanel2;
      }
      ClickyInkHStack clickyInkHStack2 = new ClickyInkHStack(10.0, InkVerticalAlignment.Center, new UIElement[]
      {
        ClickyInkHStack.Flexible<FrameworkElement>(frameworkElement),
        new ClickyInkSpacer()
      })
      {
        Margin = new Thickness(0.0, 7.0, 0.0, 7.0)
      };
      if (frameworkElement2 != null)
      {
        clickyInkHStack2.Children.Add(frameworkElement2);
      }
      return clickyInkHStack2;
    }

    // Token: 0x04000CA5 RID: 3237
    private static readonly Color Red = PaperColor.Hex("#C0392B");
  }
}
