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
  // Token: 0x02000288 RID: 648
  public static class ClickyInkAnswer
  {
    // Token: 0x06001B44 RID: 6980 RVA: 0x005EC5F0 File Offset: 0x005EC5F0
    [NullableContext(1)]
    public static FrameworkElement Make(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, bool isStruck = false)
    {
      ClickyInkAnswer.<>c__DisplayClass0_0 CS$<>8__locals1 = new ClickyInkAnswer.<>c__DisplayClass0_0();
      CS$<>8__locals1.block = block;
      CS$<>8__locals1.clock = clock;
      CS$<>8__locals1.style = style;
      CS$<>8__locals1.isStruck = isStruck;
      ClickyInkEnvironment clickyInkEnvironment = ClickyInkEnvironment.Current;
      bool isWindowPreview = clickyInkEnvironment.IsWindowPreview;
      double? num = null;
      if (CS$<>8__locals1.block.ShownSince() > CS$<>8__locals1.block.LandsAt + 0.001)
      {
        List<double> list = (from s in CS$<>8__locals1.block.Words.Segments.Where<ClickyInkLiveNote.Segment>(delegate(ClickyInkLiveNote.Segment s)
          {
            InkRange? written = s.Written;
            return written != null && written.GetValueOrDefault().LowerBound < CS$<>8__locals1.block.ShownSince();
          })
          select s.Written.Value.UpperBound).ToList<double>();
        num = ((list.Count == 0) ? null : new double?(list.Max()));
      }
      int num2 = CS$<>8__locals1.block.Words.Current.CharacterCount();
      bool flag = !isWindowPreview && num2 > 46;
      List<FrameworkElement> list2 = new List<FrameworkElement>();
      if (flag)
      {
        list2.Add(CS$<>8__locals1.<Make>g__WordsView|0((double)((num2 > 90) ? 20 : 23)));
      }
      string text = CS$<>8__locals1.block.Source["big"];
      if (text != null && text.Length > 0)
      {
        string text2 = CS$<>8__locals1.block.Source["_was"];
        double? num3 = ((text2 != null) ? new double?(CS$<>8__locals1.block.NumberShownSince ?? CS$<>8__locals1.block.ShownSince()) : null);
        double num5;
        if (num3 != null)
        {
          double num4 = CS$<>8__locals1.block.NumberLineWrittenAt ?? CS$<>8__locals1.block.Words.FinishedAt;
          num5 = ClickyInkClock.Progress(CS$<>8__locals1.clock, num4, num4 + 0.5);
        }
        else
        {
          double num6;
          if (num != null)
          {
            double valueOrDefault = num.GetValueOrDefault();
            num6 = ClickyInkClock.Progress(CS$<>8__locals1.clock, valueOrDefault, valueOrDefault + 0.5);
          }
          else
          {
            num6 = 0.0;
          }
          double num7 = num6;
          num5 = Math.Max(ClickyInkClock.Progress(CS$<>8__locals1.clock, CS$<>8__locals1.block.Words.FinishedAt, CS$<>8__locals1.block.Words.FinishedAt + 0.5), num7);
        }
        double num8 = num3 ?? (CS$<>8__locals1.block.LandsAt + 0.13999999999999999);
        double num9 = ClickyInkClock.EaseOut(ClickyInkClock.Progress(CS$<>8__locals1.clock, num8, num8 + 0.45));
        int num10 = text.CharacterCount();
        double num11 = (double)(isWindowPreview ? ((num10 > 6) ? 28 : 38) : ((num10 > 6) ? 34 : 44));
        InkTextView inkTextView = InkTextView.Plain(text, ClickyPaperFonts.BigNumber(num11), CS$<>8__locals1.style.Ink).With(delegate(InkTextView t)
        {
          t.Tracking = -1.4;
          t.MonospacedDigit = true;
          t.LineLimit = new int?(1);
          t.MinimumScale = 0.5;
          if (CS$<>8__locals1.isStruck)
          {
            t.Strikethrough = new Color?(CS$<>8__locals1.style.Fresh);
          }
        });
        inkTextView.Margin = new Thickness(10.0, 4.0, 10.0, 4.0);
        ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyHandRing(num10, 0.16).Trim(0.0, num5), new PaperStroke(CS$<>8__locals1.style.Fresh.Opacity(0.8), 2.4, PenLineCap.Round, PenLineJoin.Miter, null));
        clickyShapeView.Margin = new Thickness(-4.0);
        FrameworkElement frameworkElement = new ClickyInkOverlay(inkTextView, new UIElement[] { clickyShapeView });
        frameworkElement.Margin = new Thickness(6.0, 0.0, 0.0, 0.0);
        frameworkElement.Opacity = ((ClickyInkWords.IsStarted(CS$<>8__locals1.block.Words, CS$<>8__locals1.clock) || text2 != null) ? num9 : 0.0);
        frameworkElement.Transformed(0.0, 0.0, 0.0, 1.12 - 0.12 * num9, null);
        frameworkElement.HorizontalAlignment = HorizontalAlignment.Left;
        if (text2 != null)
        {
          FrameworkElement frameworkElement2 = ClickyInkWasValue.Make(text2, CS$<>8__locals1.style);
          frameworkElement2.Opacity = num9;
          if (flag)
          {
            list2.Add(new ClickyInkHStack(14.0, InkVerticalAlignment.Center, new UIElement[] { frameworkElement, frameworkElement2 }));
          }
          else
          {
            frameworkElement2.Margin = new Thickness(8.0, frameworkElement2.Margin.Top, 0.0, frameworkElement2.Margin.Bottom);
            list2.Add(new StackPanel
            {
              Spacing = 0.0,
              Children = { frameworkElement2, frameworkElement }
            });
          }
        }
        else
        {
          list2.Add(frameworkElement);
        }
      }
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 4.0
      };
      if (!flag)
      {
        stackPanel.Children.Add(CS$<>8__locals1.<Make>g__WordsView|0((double)(isWindowPreview ? 22 : 25)));
      }
      string text3 = CS$<>8__locals1.block.Source["note"];
      if (text3 != null && text3.Length > 0)
      {
        string text4 = ClickyInkIcon.SymbolName(CS$<>8__locals1.block.Source["icon"], CS$<>8__locals1.block.Source.Text, false);
        InkTextView inkTextView2 = new InkTextView((text4 != null) ? ClickyInkTextContent.Symbol(text4, null).Append(ClickyInkTextContent.Plain(" " + text3, null)) : ClickyInkTextContent.Plain(text3, null), ClickyPaperFonts.Hand(16.0), new Color?(CS$<>8__locals1.style.Fresh));
        UIElement uielement = inkTextView2;
        double num12;
        if (CS$<>8__locals1.clock < CS$<>8__locals1.block.Words.FinishedAt)
        {
          if (num != null)
          {
            double valueOrDefault2 = num.GetValueOrDefault();
            if (CS$<>8__locals1.clock >= valueOrDefault2)
            {
              goto IL_0650;
            }
          }
          num12 = (double)0;
          goto IL_0651;
        }
        IL_0650:
        num12 = (double)1;
        IL_0651:
        uielement.Opacity = num12;
        stackPanel.Children.Add(inkTextView2);
      }
      if (stackPanel.Children.Count > 0)
      {
        list2.Add(stackPanel);
      }
      FrameworkElement frameworkElement4;
      if (flag)
      {
        StackPanel stackPanel2 = new StackPanel
        {
          Spacing = 10.0
        };
        foreach (FrameworkElement frameworkElement3 in list2)
        {
          frameworkElement3.HorizontalAlignment = HorizontalAlignment.Left;
          stackPanel2.Children.Add(frameworkElement3);
        }
        frameworkElement4 = stackPanel2;
      }
      else
      {
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(16.0, InkVerticalAlignment.Center, Array.Empty<UIElement>());
        foreach (FrameworkElement frameworkElement5 in list2)
        {
          if (frameworkElement5 is StackPanel)
          {
            ClickyInkHStack.Flexible<FrameworkElement>(frameworkElement5);
          }
          clickyInkHStack.Children.Add(frameworkElement5);
        }
        frameworkElement4 = clickyInkHStack;
      }
      frameworkElement4.Width = clickyInkEnvironment.ContentWidth;
      frameworkElement4.HorizontalAlignment = HorizontalAlignment.Left;
      frameworkElement4.Margin = new Thickness(0.0, 0.0, 0.0, 2.0);
      return frameworkElement4;
    }
  }
}
