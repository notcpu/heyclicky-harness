using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000227 RID: 551
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkStatusBoard
  {
    // Token: 0x06001996 RID: 6550 RVA: 0x005D3F24 File Offset: 0x005D3F24
    public static FrameworkElement Create(List<ClickyInkLiveNote.Block> blocks, double clock, ClickyInkStyle style, double width)
    {
      StackPanel stackPanel = InkDashboardLayout.Width<StackPanel>(new StackPanel
      {
        Spacing = 8.0
      }, width);
      for (int i = 0; i < blocks.Count; i++)
      {
        ClickyInkLiveNote.Block block = blocks[i];
        if (clock >= block.LandsAt)
        {
          stackPanel.Children.Add(ClickyInkGrow.Around(ClickyInkStatusBoard.Row(block, i, clock, style), clock, block.LandsAt, block.VanishesAt));
        }
      }
      return stackPanel;
    }

    // Token: 0x06001997 RID: 6551 RVA: 0x005D3F98 File Offset: 0x005D3F98
    private static FrameworkElement Row(ClickyInkLiveNote.Block block, int index, double clock, ClickyInkStyle style)
    {
      ClickyInkDocument.Block source = block.Source;
      string text;
      if ((text = source["state"]) == null)
      {
        text = source["status"] ?? source["value"];
      }
      ClickyInkStatusState clickyInkStatusState = ClickyInkStatus.State(text);
      string text2 = ClickyInkChange.Previous(source, "state");
      ClickyInkStatusState? clickyInkStatusState2 = ((text2 != null) ? new ClickyInkStatusState?(ClickyInkStatus.State(text2)) : null);
      double num = ClickyInkChange.Progress(source, clock, 0.1, 0.8);
      double num2 = ClickyInkClock.Progress(clock, block.LandsAt, block.LandsAt + 0.5);
      bool flag = block.IsStruck(clock);
      bool flag2;
      if (clickyInkStatusState2 != null)
      {
        ClickyInkStatusState valueOrDefault = clickyInkStatusState2.GetValueOrDefault();
        if (valueOrDefault != clickyInkStatusState)
        {
          int? num3 = ClickyInkChange.ChangedEdit(source);
          int? latestEdit = style.LatestEdit;
          if ((num3.GetValueOrDefault() == latestEdit.GetValueOrDefault()) & (num3 != null == (latestEdit != null)))
          {
            flag2 = style.LatestEdit != null;
            goto IL_0101;
          }
        }
      }
      flag2 = false;
      IL_0101:
      bool flag3 = flag2;
      FrameworkElement frameworkElement = ClickyInkLamp.Create(clickyInkStatusState, clickyInkStatusState2, num, 19.0, index + 1, num2);
      ClickyInkLiveNote.Words words = block.Words;
      PaperFont paperFont = ClickyPaperFonts.Hand(19.0);
      double? goneAt = block.GoneAt;
      InkTextView inkTextView = ClickyInkLine.Make(words, clock, style, paperFont, null, goneAt, null, false);
      Color color = ClickyInkDashboardPalette.Word(clickyInkStatusState);
      InkTextView inkTextView2 = InkTextView.Plain(ClickyInkStatus.Word(source["state"] ?? source["status"], clickyInkStatusState), ClickyPaperFonts.Typed(10.0, new FontWeight?(FontWeights.ExtraBold)), color).With(delegate(InkTextView t)
      {
        t.Tracking = 0.9;
      });
      inkTextView2.Margin = new Thickness(5.0, 1.5, 5.0, 1.5);
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(inkTextView2, new UIElement[] { ClickyShapeView.Stroke(ClickyShape.RoundedRectangle(3.0), color.Opacity(0.55), 1.0) });
      clickyInkOverlay.Transformed(0.0, 0.0, -2.0, 1.0, null);
      clickyInkOverlay.Opacity = ClickyInkClock.Progress(clock, block.Words.FinishedAt, block.Words.FinishedAt + 0.25);
      ClickyInkOverlay clickyInkOverlay2 = new ClickyInkOverlay(clickyInkOverlay, Array.Empty<UIElement>());
      if (flag3)
      {
        ClickyShapeView clickyShapeView = ClickyShapeView.Fill(ClickyShape.RoundedRectangle(3.0), style.Highlighter);
        clickyShapeView.Margin = new Thickness(-2.0);
        clickyInkOverlay2.Behind(clickyShapeView);
      }
      ClickyInkHStack clickyInkHStack = InkDashboardLayout.HStack(8.0, InkVerticalAlignment.FirstBaseline, new UIElement[]
      {
        inkTextView,
        InkDashboardLayout.Fixed(clickyInkOverlay2),
        new ClickyInkSpacer()
      });
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 1.0,
        Children = { clickyInkHStack }
      };
      string text3 = source["note"];
      if (text3 != null && text3.Length > 0)
      {
        InkTextView inkTextView3 = InkTextView.Plain(text3, ClickyPaperFonts.Typed(11.5, null), style.Quiet).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(2);
        });
        inkTextView3.Opacity = ClickyInkClock.Progress(clock, block.Words.FinishedAt, block.Words.FinishedAt + 0.3);
        stackPanel.Children.Add(inkTextView3);
      }
      ClickyInkHStack clickyInkHStack2 = InkDashboardLayout.HStack(10.0, InkVerticalAlignment.Center, new UIElement[]
      {
        frameworkElement,
        ClickyInkHStack.Flexible<StackPanel>(stackPanel)
      });
      clickyInkHStack2.Opacity = (flag ? 0.55 : 1.0);
      return clickyInkHStack2;
    }
  }
}
