using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000295 RID: 661
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkReviewList
  {
    // Token: 0x06001B60 RID: 7008 RVA: 0x005EE6DC File Offset: 0x005EE6DC
    public static bool IsPathLike(string text)
    {
      return ClickyInkReviewListRules.IsPathLike(text);
    }

    // Token: 0x06001B61 RID: 7009 RVA: 0x005EE6E4 File Offset: 0x005EE6E4
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions, double? blockGoneAt = null)
    {
      ClickyInkDocument.Block source = block.Source;
      string text = ClickyInkObjectKey.Of(source);
      ClickyInkActionFeedback feedback = actions.Feedback;
      ClickyInkSentAction pending = feedback.Pending;
      bool flag = ((pending != null) ? pending.ObjectKey : null) == text;
      ClickyInkSentAction valueOrDefault = feedback.Answered.GetValueOrDefault(text);
      IReadOnlyList<string> readOnlyList = ((valueOrDefault != null) ? valueOrDefault.ReviewedItems : null) ?? Array.Empty<string>();
      bool flag2;
      if (valueOrDefault != null)
      {
        IReadOnlyList<string> reviewedItems = valueOrDefault.ReviewedItems;
        flag2 = reviewedItems != null && reviewedItems.Count == 0;
      }
      else
      {
        flag2 = false;
      }
      bool flag3 = flag2;
      List<string> list = ClickyInkReviewRules.ItemsToSend(source, readOnlyList);
      IEnumerable<ClickyInkDocument.Item> items = source.Items;
      Func<ClickyInkDocument.Item, bool> func;
      if ((func = ClickyInkReviewList.<>O.<0>__IsOpen) == null)
      {
        func = (ClickyInkReviewList.<>O.<0>__IsOpen = new Func<ClickyInkDocument.Item, bool>(ClickyInkReviewRules.IsOpen));
      }
      int num = items.Count<ClickyInkDocument.Item>(func);
      bool flag4 = feedback.IsLocked || flag3;
      double num2;
      if (block.Items.Count != 0)
      {
        num2 = block.Items.Max<ClickyInkLiveNote.Item>(delegate(ClickyInkLiveNote.Item i)
        {
          double num4 = Math.Max(i.LandsAt, i.Words.FinishedAt);
          ClickyInkLiveNote.Words detail = i.Detail;
          return Math.Max(num4, (detail != null) ? detail.FinishedAt : 0.0);
        });
      }
      else
      {
        num2 = 0.0;
      }
      double num3 = num2;
      bool flag5 = clock >= Math.Max(num3, block.Words.FinishedAt);
      bool flag6 = ClickyInkReviewListRules.IsCompact(block.Items.Count);
      StackPanel stackPanel = InkViews.VStack(0.0, new UIElement[] { ClickyInkReviewList.Header(block, clock, style, flag, num, flag5, flag3) });
      stackPanel.Children[0].SetValue(FrameworkElement.MarginProperty, new Thickness(0.0, 0.0, 0.0, 4.0));
      for (int j = 0; j < block.Items.Count; j++)
      {
        ClickyInkLiveNote.Item item = block.Items[j];
        FrameworkElement frameworkElement = ClickyInkReviewList.Row(block, item, j, clock, style, actions, blockGoneAt, flag6, flag4, readOnlyList);
        stackPanel.Children.Add(ClickyInkGrow.Around(frameworkElement, clock, item.LandsAt, item.VanishesAt));
      }
      FrameworkElement frameworkElement2 = ClickyInkReviewList.Footer(source, clock, style, actions, feedback, flag, flag3, valueOrDefault, num, list.Count, flag4, list);
      frameworkElement2.Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
      frameworkElement2.Opacity = flag5 > false;
      if (!flag5)
      {
        frameworkElement2.IsHitTestVisible = false;
      }
      stackPanel.Children.Add(frameworkElement2);
      stackPanel.Margin = new Thickness(34.0, 12.0, 14.0, 12.0);
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(stackPanel, Array.Empty<UIElement>()).Behind(ClickyInkReviewList.Pad());
      clickyInkOverlay.Width = 372.0;
      clickyInkOverlay.HorizontalAlignment = HorizontalAlignment.Left;
      clickyInkOverlay.Transformed(0.0, 0.0, -0.35, 1.0, null);
      return new Grid
      {
        Width = 378.0,
        HorizontalAlignment = HorizontalAlignment.Left,
        Padding = new Thickness(3.0, 0.0, 0.0, 0.0),
        Children = { clickyInkOverlay }
      };
    }

    // Token: 0x06001B62 RID: 7010 RVA: 0x005EEA48 File Offset: 0x005EEA48
    private static FrameworkElement Header(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, bool isPending, int openCount, bool isWritten, bool wasSkipped)
    {
      FrameworkElement frameworkElement;
      if (block.Words.Current.Length != 0)
      {
        frameworkElement = InkViews.Words(block.Words, clock, style, ClickyPaperFonts.Hand(21.0), null, true, delegate(InkTextView t)
        {
          t.LineLimit = new int?(2);
        });
      }
      else
      {
        frameworkElement = InkViews.Plain("Your OK", ClickyPaperFonts.Hand(21.0), style.Ink, null);
      }
      FrameworkElement frameworkElement2 = frameworkElement;
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(8.0, InkVerticalAlignment.FirstBaseline, new UIElement[]
      {
        ClickyInkHStack.Flexible<FrameworkElement>(frameworkElement2),
        new ClickyInkSpacer()
      });
      if (openCount > 0 && isWritten && !wasSkipped)
      {
        clickyInkHStack.Children.Add(ClickyInkTag.Make(isPending ? "on its way" : "waiting on you", isPending ? style.Fresh : ClickyInkReviewList.WaitingAmber));
      }
      return clickyInkHStack;
    }

    // Token: 0x06001B63 RID: 7011 RVA: 0x005EEB34 File Offset: 0x005EEB34
    private static FrameworkElement Row(ClickyInkLiveNote.Block block, ClickyInkLiveNote.Item item, int offset, double clock, ClickyInkStyle style, ClickyInkActions actions, double? blockGoneAt, bool isCompact, bool isLocked, IReadOnlyList<string> alreadySent)
    {
      ClickyInkDocument.Item source = item.Source;
      bool flag = source.Mark.GetValueOrDefault() == ClickyInkDocument.Mark.Done;
      bool flag2 = source.Mark.GetValueOrDefault() == ClickyInkDocument.Mark.Gone;
      bool flag3 = ClickyInkReviewRules.WasSent(source, alreadySent);
      bool flag4 = source.IsChecked.GetValueOrDefault() || flag;
      double? goneAt = item.GoneAt;
      double? num2;
      if (goneAt != null)
      {
        double valueOrDefault = goneAt.GetValueOrDefault();
        double num;
        if (blockGoneAt != null)
        {
          double valueOrDefault2 = blockGoneAt.GetValueOrDefault();
          num = Math.Min(valueOrDefault, valueOrDefault2);
        }
        else
        {
          num = valueOrDefault;
        }
        num2 = new double?(num);
      }
      else
      {
        num2 = blockGoneAt;
      }
      double? num3 = num2;
      bool flag5;
      if (num3 != null)
      {
        double valueOrDefault3 = num3.GetValueOrDefault();
        flag5 = clock >= valueOrDefault3;
      }
      else
      {
        flag5 = false;
      }
      bool flag6 = flag5;
      bool canTick = !isLocked && !flag && !flag2 && !flag6 && (!flag3 || !flag4);
      ValueTuple<ClickyInkLiveNote.Words, ClickyInkLiveNote.Words>? valueTuple = item.Words.Split(" — ");
      ValueTuple<ClickyInkLiveNote.Words, ClickyInkLiveNote.Words>? valueTuple2 = ((valueTuple != null) ? valueTuple : item.Words.Split(" – "));
      ClickyInkLiveNote.Words words = (flag2 ? (item.Detail ?? ((valueTuple2 != null) ? valueTuple2.GetValueOrDefault().Item2 : null)) : (((valueTuple2 != null) ? valueTuple2.GetValueOrDefault().Item2 : null) ?? item.Detail));
      ClickyInkLiveNote.Words words2 = ((valueTuple2 != null) ? valueTuple2.GetValueOrDefault().Item1 : null) ?? item.Words;
      double? num4 = (flag2 ? new double?(item.Words.FinishedAt) : num3);
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack((double)(isCompact ? 8 : 10), InkVerticalAlignment.FirstBaseline, new UIElement[] { ClickyInkReviewList.Box(flag4, flag, offset, item, clock) });
      if (isCompact)
      {
        FrameworkElement frameworkElement;
        if (ClickyInkReviewList.IsPathLike(words2.Current) && clock >= words2.FinishedAt)
        {
          if (num4 != null)
          {
            double valueOrDefault4 = num4.GetValueOrDefault();
            if (clock >= valueOrDefault4)
            {
              goto IL_0264;
            }
          }
          frameworkElement = new InkMiddleTruncatedText(ClickyInkParser.Emphasis.Strip(words2.Current), ClickyPaperFonts.Hand(17.0), flag2 ? style.Quiet : style.Ink);
          goto IL_02A2;
        }
        IL_0264:
        frameworkElement = ClickyInkLine.Make(words2, clock, style, ClickyPaperFonts.Hand(17.0), flag2 ? new Color?(style.Quiet) : null, num4, new int?(1), false);
        IL_02A2:
        FrameworkElement frameworkElement2 = null;
        if (words != null)
        {
          if (ClickyInkWords.IsStarted(words, clock))
          {
            frameworkElement2 = InkViews.Words(words, clock, style, ClickyPaperFonts.Typed(11.0, null), new Color?(style.Quiet), false, delegate(InkTextView t)
            {
              t.LineLimit = new int?(1);
            });
          }
          else
          {
            frameworkElement2 = InkViews.Plain(ClickyInkParser.Emphasis.Strip(words.Current), ClickyPaperFonts.Typed(11.0, null), style.Quiet, delegate(InkTextView t)
            {
              t.LineLimit = new int?(1);
            });
            frameworkElement2.Opacity = 0.0;
          }
        }
        clickyInkHStack.Children.Add(ClickyInkHStack.Flexible<ClickyInkReviewCompactLine>(new ClickyInkReviewCompactLine(frameworkElement, frameworkElement2)));
      }
      else
      {
        clickyInkHStack.Children.Add(ClickyInkHStack.Flexible<InkTextView>(ClickyInkLine.Make(words2, clock, style, ClickyPaperFonts.Hand(19.0), flag2 ? new Color?(style.Quiet) : null, num4, null, false)));
      }
      if (clock >= item.Words.FinishedAt)
      {
        FrameworkElement frameworkElement3 = null;
        if (flag)
        {
          frameworkElement3 = ClickyInkTag.Make("done", ClickyPaperInk.DoneGreen);
        }
        else if (flag2)
        {
          frameworkElement3 = ClickyInkTag.Make("couldn't", PaperColor.Hex("#C2410C"));
        }
        else if (flag3 && flag4)
        {
          frameworkElement3 = ClickyInkTag.Make("sent", style.Fresh);
        }
        else
        {
          ClickyInkDocument.Mark? mark = source.Mark;
          ClickyInkDocument.Mark mark2 = ClickyInkDocument.Mark.New;
          if ((mark.GetValueOrDefault() == mark2) & (mark != null))
          {
            frameworkElement3 = ClickyInkTag.Make("new", ClickyPaperInk.DoneGreen);
          }
        }
        if (frameworkElement3 != null)
        {
          clickyInkHStack.Children.Add(frameworkElement3);
        }
      }
      if (!isCompact)
      {
        clickyInkHStack.Children.Add(new ClickyInkSpacer());
      }
      StackPanel stackPanel = InkViews.VStack(1.0, new UIElement[] { clickyInkHStack });
      if (!isCompact && words != null && ClickyInkWords.IsStarted(words, clock))
      {
        InkTextView inkTextView = InkViews.Words(words, clock, style, ClickyPaperFonts.Typed(11.5, null), new Color?(style.Quiet), false, delegate(InkTextView t)
        {
          t.LineLimit = new int?(2);
        });
        inkTextView.Margin = new Thickness(28.0, 0.0, 0.0, 0.0);
        stackPanel.Children.Add(inkTextView);
      }
      stackPanel.Margin = new Thickness(0.0, isCompact ? 2.5 : 4.0, 0.0, isCompact ? 2.5 : 4.0);
      Border border = new Border
      {
        Height = 1.0,
        Background = Ui.Brush(ClickyInkReviewList.Rule),
        VerticalAlignment = VerticalAlignment.Bottom,
        IsHitTestVisible = false,
        Margin = new Thickness(-34.0, 0.0, -14.0, 0.0)
      };
      Pressable pressable = InkViews.PlainButton(new ClickyInkOverlay(stackPanel, new UIElement[] { border }), delegate
      {
        if (!canTick)
        {
          return;
        }
        actions.Toggle(block.Id, item.Id);
      }, ClickyInkNoteActions.Plain(source.Text));
      pressable.HorizontalAlignment = HorizontalAlignment.Stretch;
      if (!canTick)
      {
        pressable.IsHitTestVisible = false;
      }
      AutomationProperties.SetItemStatus(pressable, flag4 ? "ticked" : "not ticked");
      return pressable;
    }

    // Token: 0x06001B64 RID: 7012 RVA: 0x005EF1AC File Offset: 0x005EF1AC
    private static FrameworkElement Box(bool isChecked, bool isDone, int offset, ClickyInkLiveNote.Item item, double clock)
    {
      ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyInkBox(offset + 7), new PaperStroke(ClickyInkReviewList.Pencil.Opacity(0.6), 1.4, PenLineCap.Flat, PenLineJoin.Miter, null));
      clickyShapeView.Width = 16.0;
      clickyShapeView.Height = 16.0;
      clickyShapeView.HorizontalAlignment = HorizontalAlignment.Center;
      clickyShapeView.VerticalAlignment = VerticalAlignment.Center;
      Grid grid = new Grid
      {
        Width = 18.0,
        Height = 18.0,
        Children = { clickyShapeView }
      };
      if (isChecked)
      {
        double num = 1.0;
        double? num2 = item.Since((ClickyInkDocument.Item i) => i.IsChecked.GetValueOrDefault() || i.Mark.GetValueOrDefault() == ClickyInkDocument.Mark.Done);
        if (num2 != null)
        {
          double valueOrDefault = num2.GetValueOrDefault();
          if (valueOrDefault > item.LandsAt)
          {
            num = ClickyInkClock.EaseOut(ClickyInkClock.Progress(clock, valueOrDefault, valueOrDefault + 0.22));
          }
        }
        if (num > 0.0)
        {
          ClickyShapeView clickyShapeView2 = ClickyShapeView.Stroke(new ClickyInkTick().Trim(0.0, num), new PaperStroke(isDone ? ClickyPaperInk.DoneGreen : ClickyInkReviewList.Pencil, 2.4, PenLineCap.Round, PenLineJoin.Round, null));
          clickyShapeView2.Width = 17.0;
          clickyShapeView2.Height = 16.0;
          clickyShapeView2.HorizontalAlignment = HorizontalAlignment.Center;
          clickyShapeView2.VerticalAlignment = VerticalAlignment.Center;
          clickyShapeView2.Transformed(2.0, -3.0, 0.0, 1.0, null);
          grid.Children.Add(clickyShapeView2);
        }
      }
      return grid.BaselineFromBottom(3.0);
    }

    // Token: 0x06001B65 RID: 7013 RVA: 0x005EF380 File Offset: 0x005EF380
    private static FrameworkElement Footer(ClickyInkDocument.Block source, double clock, ClickyInkStyle style, ClickyInkActions actions, ClickyInkActionFeedback feedback, bool isPending, bool wasSkipped, [Nullable(2)] ClickyInkSentAction answered, int openCount, int count, bool isLocked, List<string> toSend)
    {
      ClickyInkReviewList.<>c__DisplayClass12_0 CS$<>8__locals1 = new ClickyInkReviewList.<>c__DisplayClass12_0();
      CS$<>8__locals1.count = count;
      CS$<>8__locals1.openCount = openCount;
      CS$<>8__locals1.style = style;
      CS$<>8__locals1.source = source;
      CS$<>8__locals1.isLocked = isLocked;
      CS$<>8__locals1.actions = actions;
      CS$<>8__locals1.toSend = toSend;
      if (isPending)
      {
        ClickyInkSentAction pending = feedback.Pending;
        if (pending != null)
        {
          return ClickyInkYouSaidSlip.Make(pending.MarginWords, true, clock, CS$<>8__locals1.style).Padding(-18.0, 0.0, 0.0, 0.0);
        }
      }
      if (wasSkipped && answered != null)
      {
        return ClickyInkYouSaidSlip.Make(answered.MarginWords, false, clock, CS$<>8__locals1.style).Padding(-18.0, 0.0, 0.0, 0.0);
      }
      if (CS$<>8__locals1.openCount == 0)
      {
        ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyInkTick(), new PaperStroke(ClickyPaperInk.DoneGreen, 2.2, PenLineCap.Round, PenLineJoin.Round, null));
        clickyShapeView.Width = 14.0;
        clickyShapeView.Height = 12.0;
        return new ClickyInkHStack(6.0, InkVerticalAlignment.Center, new UIElement[]
        {
          clickyShapeView,
          InkViews.Plain("all done", ClickyPaperFonts.Hand(17.0), ClickyPaperInk.DoneGreen, null)
        });
      }
      return new InkViewThatFits(new UIElement[]
      {
        new ClickyInkHStack(8.0, InkVerticalAlignment.Center, new UIElement[]
        {
          CS$<>8__locals1.<Footer>g__Count|0(),
          new ClickyInkSpacer
          {
            MinWidth = 4.0
          },
          CS$<>8__locals1.<Footer>g__Buttons|1()
        }),
        new ClickyInkHStack(8.0, InkVerticalAlignment.Center, new UIElement[]
        {
          new ClickyInkSpacer(),
          CS$<>8__locals1.<Footer>g__Buttons|1()
        }),
        InkViews.VStack(8.0, new UIElement[]
        {
          CS$<>8__locals1.<Footer>g__Count|0(),
          CS$<>8__locals1.<Footer>g__Buttons|1()
        })
      });
    }

    // Token: 0x06001B66 RID: 7014 RVA: 0x005EF57C File Offset: 0x005EF57C
    private static FrameworkElement Pad()
    {
      InkPaperSheet inkPaperSheet = new InkPaperSheet("review-pad", ClickyShape.Rectangle, PaperFill.Of(ClickyInkReviewList.PadYellow), new double?(0.35), new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.1), 0.6, 0.0, 0.5),
        new PaperShadow(PaperColor.Hex("#141E3C").Opacity(0.14), 6.0, 0.0, 4.0)
      }, null, null);
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 3.0,
        Margin = new Thickness(23.0, 0.0, 0.0, 0.0),
        HorizontalAlignment = HorizontalAlignment.Left,
        IsHitTestVisible = false,
        Children = 
        {
          new Border
          {
            Width = 1.0,
            Background = Ui.Brush(ClickyInkReviewList.MarginRed.Opacity(0.7))
          },
          new Border
          {
            Width = 1.0,
            Background = Ui.Brush(ClickyInkReviewList.MarginRed.Opacity(0.45))
          }
        }
      };
      ClickyShapeView clickyShapeView = ClickyShapeView.StrokeBorder(ClickyShape.Rectangle, new PaperStroke(ClickyInkReviewList.PadEdge, 0.8, PenLineCap.Flat, PenLineJoin.Miter, null));
      return new Grid
      {
        IsHitTestVisible = false,
        Children = { inkPaperSheet, stackPanel, clickyShapeView }
      };
    }

    // Token: 0x04000D1B RID: 3355
    private static readonly Color PadYellow = PaperColor.Hex("#FFF6CF");

    // Token: 0x04000D1C RID: 3356
    private static readonly Color PadEdge = PaperColor.Hex("#E7D58E");

    // Token: 0x04000D1D RID: 3357
    private static readonly Color MarginRed = PaperColor.Hex("#E58A8A");

    // Token: 0x04000D1E RID: 3358
    private static readonly Color Rule = PaperColor.Hex("#7A93C8").Opacity(0.26);

    // Token: 0x04000D1F RID: 3359
    private static readonly Color WaitingAmber = PaperColor.Hex("#8A5300");

    // Token: 0x04000D20 RID: 3360
    private static readonly Color Pencil = PaperColor.Hex("#3A3833");

    // Token: 0x04000D21 RID: 3361
    private const double MarginInset = 34.0;

    // Token: 0x02000869 RID: 2153
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04002387 RID: 9095
      [Nullable(0)]
      public static Func<ClickyInkDocument.Item, bool> <0>__IsOpen;
    }
  }
}
