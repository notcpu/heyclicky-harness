using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200027D RID: 637
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkChart
  {
    // Token: 0x06001AE5 RID: 6885 RVA: 0x005E9528 File Offset: 0x005E9528
    public static string ValueWithUnit(string value, string unit)
    {
      return ClickyInkChartRules.ValueWithUnit(value, unit);
    }

    // Token: 0x06001AE6 RID: 6886 RVA: 0x005E9534 File Offset: 0x005E9534
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      List<double> list = ClickyInkChartRules.Values(block.Source);
      double num;
      if (list.Count != 0)
      {
        IEnumerable<double> enumerable = list;
        Func<double, double> func;
        if ((func = ClickyInkChart.<>O.<0>__Abs) == null)
        {
          func = (ClickyInkChart.<>O.<0>__Abs = new Func<double, double>(Math.Abs));
        }
        num = enumerable.Max<double>(func);
      }
      else
      {
        num = 1.0;
      }
      double num2 = Math.Max(num, 0.0001);
      string text = block.Source["unit"] ?? "";
      double num3 = ClickyInkChartRules.LabelWidth(block.Source);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 7.0
      };
      if (block.Words.Current.Length > 0)
      {
        stackPanel.Children.Add(ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand(18.0), true, null, null));
      }
      int i = 0;
      while (i < block.Items.Count)
      {
        ClickyInkLiveNote.Item item = block.Items[i];
        double num4 = ClickyInkClock.Progress(clock, item.LandsAt, item.LandsAt + 0.45);
        ClickyInkDocument.Mark? mark = item.Source.Mark;
        if (mark == null)
        {
          goto IL_0149;
        }
        ClickyInkDocument.Mark valueOrDefault = mark.GetValueOrDefault();
        if (valueOrDefault - ClickyInkDocument.Mark.Pick > 1)
        {
          goto IL_0149;
        }
        bool flag = true;
        IL_014C:
        bool flag2 = flag;
        InkTextView inkTextView = InkTextView.Plain((item.Source.Cells.Count > 0) ? item.Source.Cells[0] : item.Source.Text, ClickyPaperFonts.Hand(15.0), style.Ink).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(2);
          t.MinimumScale = 0.85;
          t.TextAlignment = TextAlignment.Right;
        });
        inkTextView.HorizontalAlignment = HorizontalAlignment.Right;
        Grid grid = new Grid
        {
          Width = num3,
          Children = { inkTextView }
        };
        double num5 = Math.Max(2.0, (contentWidth - num3 - 70.0) * (Math.Abs((i < list.Count) ? list[i] : 0.0) / num2) * num4);
        Grid grid2 = new Grid
        {
          Width = num5,
          Height = 15.0
        };
        grid2.Children.Add(ClickyShapeView.Fill(new ClickyInkSketchBar(i), flag2 ? style.Fresh.Opacity(0.75) : style.Ink.Opacity(0.22)));
        grid2.Children.Add(ClickyShapeView.Stroke(new ClickyInkSketchBar(i), flag2 ? style.Fresh : style.Ink.Opacity(0.5), 1.1));
        InkTextView inkTextView2 = InkTextView.Plain(ClickyInkChart.ValueWithUnit((item.Source.Cells.Count > 1) ? item.Source.Cells[1] : "", text), ClickyInkObjectFonts.Rounded(11.5, flag2 ? FontWeights.Bold : FontWeights.Medium), flag2 ? style.Fresh : style.Ink.Opacity(0.7));
        inkTextView2.Opacity = num4;
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(8.0, InkVerticalAlignment.Center, new UIElement[] { grid, grid2, inkTextView2 });
        clickyInkHStack.Opacity = clock >= item.LandsAt;
        stackPanel.Children.Add(clickyInkHStack);
        i++;
        continue;
        IL_0149:
        flag = false;
        goto IL_014C;
      }
      return ClickyInkObjectKit.Leading(stackPanel, contentWidth);
    }

    // Token: 0x02000849 RID: 2121
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x0400232D RID: 9005
      [Nullable(0)]
      public static Func<double, double> <0>__Abs;
    }
  }
}
