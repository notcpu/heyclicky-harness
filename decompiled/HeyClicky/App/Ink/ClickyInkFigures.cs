using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000264 RID: 612
  public static class ClickyInkFigures
  {
    // Token: 0x06001A93 RID: 6803 RVA: 0x005E38B8 File Offset: 0x005E38B8
    [NullableContext(1)]
    public static FrameworkElement Create(List<ClickyInkLiveNote.Block> blocks, double clock, ClickyInkStyle style)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      Grid grid = new Grid
      {
        Width = contentWidth,
        HorizontalAlignment = HorizontalAlignment.Left,
        ColumnSpacing = 18.0
      };
      Action<InkTextView> <>9__1;
      foreach (ClickyInkLiveNote.Block block in blocks.Take<ClickyInkLiveNote.Block>(3))
      {
        if (clock >= block.LandsAt)
        {
          StackPanel stackPanel = new StackPanel
          {
            Spacing = 2.0,
            HorizontalAlignment = HorizontalAlignment.Stretch
          };
          string text = block.Source["_was"];
          string text2;
          if ((text2 = block.Source["value"]) == null)
          {
            text2 = block.Source["big"] ?? "";
          }
          InkTextView inkTextView = InkTextView.Plain(text2, ClickyPaperFonts.BigNumber(30.0), (text != null) ? style.Fresh : style.Ink).With(delegate(InkTextView t)
          {
            t.Tracking = -1.0;
            t.LineLimit = new int?(1);
            t.MinimumScale = 0.5;
          });
          ClickyInkHStack clickyInkHStack = new ClickyInkHStack(6.0, InkVerticalAlignment.FirstBaseline, new UIElement[] { inkTextView });
          if (text != null)
          {
            UIElementCollection children = clickyInkHStack.Children;
            InkTextView inkTextView2 = InkTextView.Plain(text, ClickyPaperFonts.BigNumber(15.0), style.Quiet);
            Action<InkTextView> action;
            if ((action = <>9__1) == null)
            {
              action = (<>9__1 = delegate(InkTextView t)
              {
                t.Strikethrough = new Color?(style.Fresh);
              });
            }
            children.Add(inkTextView2.With(action));
          }
          stackPanel.Children.Add(clickyInkHStack);
          stackPanel.Children.Add(ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand(15.0), false, new Color?(style.Quiet), null).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(2);
          }));
          ClickyInkFigureRules.Delta delta = ClickyInkFigureRules.Read(block.Source["delta"]);
          if (delta != null)
          {
            stackPanel.Children.Add(delta.IsCaption ? InkTextView.Plain(delta.Text, ClickyPaperFonts.Typed(11.0, new FontWeight?(FontWeights.Medium)), style.Quiet) : InkTextView.Plain(delta.Text, ClickyPaperFonts.Typed(11.0, new FontWeight?(FontWeights.SemiBold)), delta.IsDown ? ClickyInkFigures.DownOrange : ClickyPaperInk.DoneGreen));
          }
          grid.ColumnDefinitions.Add(new ColumnDefinition
          {
            Width = new GridLength(1.0, GridUnitType.Star)
          });
          Grid.SetColumn(stackPanel, grid.Children.Count);
          grid.Children.Add(stackPanel);
        }
      }
      return grid;
    }

    // Token: 0x04000CBF RID: 3263
    private static readonly Color DownOrange = PaperColor.Hex("#C2410C");
  }
}
