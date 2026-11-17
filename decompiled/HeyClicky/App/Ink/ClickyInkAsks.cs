using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200026E RID: 622
  public static class ClickyInkAsks
  {
    // Token: 0x06001AAA RID: 6826 RVA: 0x005E5C6C File Offset: 0x005E5C6C
    [NullableContext(1)]
    public static FrameworkElement Create(List<ClickyInkLiveNote.Block> blocks, double clock, ClickyInkActions actions)
    {
      ClickyInkAsks.<>c__DisplayClass0_0 CS$<>8__locals1 = new ClickyInkAsks.<>c__DisplayClass0_0();
      CS$<>8__locals1.actions = actions;
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      ClickyNotebookFlow clickyNotebookFlow = new ClickyNotebookFlow
      {
        Spacing = 8.0,
        LineSpacing = 8.0
      };
      for (int i = 0; i < blocks.Count; i++)
      {
        ClickyInkAsks.<>c__DisplayClass0_1 CS$<>8__locals2 = new ClickyInkAsks.<>c__DisplayClass0_1();
        CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
        ClickyInkLiveNote.Block block = blocks[i];
        if (clock >= block.LandsAt)
        {
          ClickyInkAsks.<>c__DisplayClass0_1 CS$<>8__locals3 = CS$<>8__locals2;
          string text;
          if ((text = block.Source["say"]) == null)
          {
            text = block.Source["ask"] ?? block.Words.Current;
          }
          CS$<>8__locals3.say = text;
          clickyNotebookFlow.Children.Add(new ClickyPaperGelButton(block.Words.Current, delegate
          {
            CS$<>8__locals2.CS$<>8__locals1.actions.Send(CS$<>8__locals2.say);
          }, (i == 0) ? ClickyPaperGel.Blue : ClickyPaperGel.White, 30.0, 14.0, 12.5, null));
        }
      }
      return ClickyInkObjectKit.Leading(clickyNotebookFlow, contentWidth);
    }
  }
}
