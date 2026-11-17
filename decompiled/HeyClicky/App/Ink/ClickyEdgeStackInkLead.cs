using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000212 RID: 530
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyEdgeStackInkLead
  {
    // Token: 0x06001945 RID: 6469 RVA: 0x005CFF44 File Offset: 0x005CFF44
    [return: Nullable(2)]
    public static FrameworkElement Make(ClickyEdgeStackInkReading reading, double clock, ClickyInkStyle style, bool isLarge, bool showsSupport = true)
    {
      ClickyInkLiveNote.Block lead = reading.Lead;
      if (lead == null)
      {
        return null;
      }
      int num = ((!reading.ChangeCounts.IsEmpty + (reading.CurrentState == ClickyEdgeStackInkReading.State.Writing) + ((reading.WaitingLine == null && reading.PendingLine == null) ? false : true)) ? 1 : 0);
      int num2 = (isLarge ? 9 : Math.Max(2, 5 - num));
      StackPanel stackPanel = new StackPanel
      {
        Spacing = (double)(isLarge ? 8 : 6)
      };
      stackPanel.Children.Add(ClickyEdgeStackInkLead.LeadContent(reading, lead, clock, style, isLarge, num2));
      if (showsSupport)
      {
        ClickyInkLiveNote.Block support = reading.Support;
        if (support != null && lead.Source.Kind == ClickyInkDocument.Kind.Answer)
        {
          int num3 = ((lead.Source["big"] == null) ? (isLarge ? 46 : 42) : (isLarge ? 38 : 34));
          int num4 = Math.Max(1, Math.Min(3, (lead.Words.Current.CharacterCount() + num3 - 1) / num3));
          int num5 = num2 - num4;
          if (num5 > 0)
          {
            stackPanel.Children.Add(ClickyEdgeStackInkLead.SupportContent(reading, support, num5, clock, style, isLarge));
          }
        }
      }
      return stackPanel;
    }

    // Token: 0x06001946 RID: 6470 RVA: 0x005D005C File Offset: 0x005D005C
    private static FrameworkElement LeadContent(ClickyEdgeStackInkReading reading, ClickyInkLiveNote.Block lead, double clock, ClickyInkStyle style, bool isLarge, int lineBudget)
    {
      PaperFont paperFont = ClickyPaperFonts.Hand((double)(isLarge ? 19 : 16));
      if (lead.Source.Kind == ClickyInkDocument.Kind.Answer)
      {
        string text = lead.Source["big"];
        if (text != null && text.Length > 0)
        {
          InkTextView inkTextView = ClickyInkWordsView.Make(lead.Words, clock, style, paperFont, true, null, null).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(3);
          });
          return new ClickyInkHStack((double)(isLarge ? 14 : 11), InkVerticalAlignment.Center, new UIElement[]
          {
            ClickyEdgeStackInkLead.BigNumber(text, lead, clock, style, isLarge),
            ClickyInkHStack.Flexible<FrameworkElement>(ClickyEdgeStackInkLead.Left(ClickyEdgeStackHighlight.Wrap(inkTextView, reading.LeadChanged, 5)))
          })
          {
            Margin = new Thickness(3.0, 0.0, 0.0, 0.0)
          };
        }
      }
      if (lead.Items.Count > 0)
      {
        return ClickyEdgeStackInkLead.Items(reading, lead, Math.Min(lineBudget, isLarge ? 6 : 4), clock, style, isLarge);
      }
      int limit = ((lead.Source.Kind == ClickyInkDocument.Kind.Answer) ? 3 : Math.Min(lineBudget, 5));
      return ClickyEdgeStackInkLead.Left(ClickyEdgeStackHighlight.Wrap(ClickyInkWordsView.Make(lead.Words, clock, style, paperFont, true, null, null).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(limit);
      }), reading.LeadChanged, 5));
    }

    // Token: 0x06001947 RID: 6471 RVA: 0x005D01F8 File Offset: 0x005D01F8
    private static FrameworkElement SupportContent(ClickyEdgeStackInkReading reading, ClickyInkLiveNote.Block support, int lines, double clock, ClickyInkStyle style, bool isLarge)
    {
      if (support.Items.Count > 0)
      {
        return ClickyEdgeStackInkLead.Items(reading, support, Math.Min(lines, isLarge ? 5 : 3), clock, style, isLarge);
      }
      return ClickyInkWordsView.Make(support.Words, clock, style, ClickyPaperFonts.Hand(isLarge ? 17.0 : 14.5), true, new Color?(style.Quiet), null).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(Math.Min(lines, 3));
      });
    }

    // Token: 0x06001948 RID: 6472 RVA: 0x005D0294 File Offset: 0x005D0294
    private static FrameworkElement Items(ClickyEdgeStackInkReading reading, ClickyInkLiveNote.Block block, int limit, double clock, ClickyInkStyle style, bool isLarge)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = (double)(isLarge ? 5 : 3)
      };
      IEnumerable<ClickyInkLiveNote.Item> items = block.Items;
      Func<ClickyInkLiveNote.Item, bool> <>9__0;
      Func<ClickyInkLiveNote.Item, bool> func;
      if ((func = <>9__0) == null)
      {
        func = (<>9__0 = (ClickyInkLiveNote.Item i) => !i.IsStruck(clock));
      }
      foreach (ClickyInkLiveNote.Item item in items.Where<ClickyInkLiveNote.Item>(func).Take<ClickyInkLiveNote.Item>(limit))
      {
        FrameworkElement frameworkElement = ClickyEdgeStackInkLead.Left(ClickyEdgeStackHighlight.Wrap(ClickyInkWordsView.Make(item.Words, clock, style, ClickyPaperFonts.Hand(isLarge ? 17.5 : 15.0), true, null, null).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
        }), reading.ChangedItemIDs.Contains(item.Id), item.Id.Length));
        stackPanel.Children.Add(new ClickyInkHStack(7.0, InkVerticalAlignment.FirstBaseline, new UIElement[]
        {
          ClickyEdgeStackInkLead.Marker(item, block, style, isLarge),
          ClickyInkHStack.Flexible<FrameworkElement>(frameworkElement)
        }));
      }
      return stackPanel;
    }

    // Token: 0x06001949 RID: 6473 RVA: 0x005D03FC File Offset: 0x005D03FC
    private static FrameworkElement BigNumber(string big, ClickyInkLiveNote.Block lead, double clock, ClickyInkStyle style, bool isLarge)
    {
      double num = ClickyInkClock.Progress(clock, lead.Words.FinishedAt, lead.Words.FinishedAt + 0.5);
      string text = lead.Source["_was"];
      double num2 = ((text != null) ? lead.ShownSince() : (lead.Words.StartsAt ?? lead.LandsAt));
      double num3 = ClickyInkClock.EaseOut(ClickyInkClock.Progress(clock, num2, num2 + 0.45));
      int num4 = big.CharacterCount();
      double num5 = (double)(isLarge ? ((num4 > 6) ? 28 : 34) : ((num4 > 6) ? 21 : 26));
      InkTextView inkTextView = InkTextView.Plain(big, ClickyPaperFonts.BigNumber(num5), style.Ink).With(delegate(InkTextView t)
      {
        t.Tracking = -0.8;
        t.MonospacedDigit = true;
        t.LineLimit = new int?(1);
        t.MinimumScale = 0.55;
      });
      inkTextView.Margin = new Thickness(7.0, 2.0, 7.0, 2.0);
      ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyHandRing(num4, 0.16).Trim(0.0, num), new PaperStroke(style.Fresh.Opacity(0.78), 1.9, PenLineCap.Round, PenLineJoin.Miter, null));
      clickyShapeView.Margin = new Thickness(-3.0);
      List<UIElement> list = new List<UIElement> { clickyShapeView };
      if (text != null)
      {
        InkTextView inkTextView2 = InkTextView.Plain(text, ClickyPaperFonts.BigNumber(isLarge ? 14.0 : 11.5), style.Quiet).With(delegate(InkTextView t)
        {
          t.Strikethrough = new Color?(style.Fresh);
        });
        inkTextView2.HorizontalAlignment = HorizontalAlignment.Left;
        inkTextView2.VerticalAlignment = VerticalAlignment.Top;
        inkTextView2.Transformed(4.0, -14.0, -6.0, 1.0, null);
        list.Add(inkTextView2);
      }
      return new ClickyInkOverlay(inkTextView, list.ToArray())
      {
        Opacity = ((ClickyInkWords.IsStarted(lead.Words, clock) || text != null) ? num3 : 0.0),
        MaxWidth = (double)(isLarge ? 150 : 112),
        HorizontalAlignment = HorizontalAlignment.Left
      };
    }

    // Token: 0x0600194A RID: 6474 RVA: 0x005D0680 File Offset: 0x005D0680
    private static FrameworkElement Marker(ClickyInkLiveNote.Item item, ClickyInkLiveNote.Block lead, ClickyInkStyle style, bool isLarge)
    {
      if (lead.Source.Kind == ClickyInkDocument.Kind.Checklist)
      {
        bool valueOrDefault = item.Source.IsChecked.GetValueOrDefault();
        return (ClickyInkIcon.Glyph(valueOrDefault ? "checkmark.square" : "square", isLarge ? 12.0 : 10.5, valueOrDefault ? ClickyPaperInk.DoneGreen : style.Quiet, false) ?? new Grid()).BaselineFromBottom(isLarge ? 2.4 : 2.1);
      }
      return InkTextView.Plain("•", ClickyPaperFonts.Hand((double)(isLarge ? 17 : 15)), style.Quiet);
    }

    // Token: 0x0600194B RID: 6475 RVA: 0x005D0730 File Offset: 0x005D0730
    private static FrameworkElement Left(FrameworkElement view)
    {
      view.HorizontalAlignment = HorizontalAlignment.Left;
      return view;
    }
  }
}
