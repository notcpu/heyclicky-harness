using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200026D RID: 621
  public static class ClickyInkSources
  {
    // Token: 0x06001AA9 RID: 6825 RVA: 0x005E59B8 File Offset: 0x005E59B8
    [NullableContext(1)]
    public static FrameworkElement Create(List<ClickyInkLiveNote.Block> blocks, double clock, ClickyInkStyle style, ClickyInkActions actions)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 5.0
      };
      if (blocks.Count > 0 && blocks[0].Source["_labelled"] == null)
      {
        stackPanel.Children.Add(InkTextView.Plain("sources", ClickyPaperFonts.Hand(15.0), style.Quiet));
      }
      using (List<ClickyInkLiveNote.Block>.Enumerator enumerator = blocks.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ClickyInkLiveNote.Block block = enumerator.Current;
          if (clock >= block.LandsAt)
          {
            bool flag = block.IsStruck(clock);
            ClickyInkHStack clickyInkHStack = new ClickyInkHStack(6.0, InkVerticalAlignment.Center, new UIElement[] { InkTextView.Plain("↗", ClickyPaperFonts.Typed(11.0, new FontWeight?(FontWeights.Bold)), style.Fresh) });
            InkTextView inkTextView = ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Typed(12.5, new FontWeight?(FontWeights.Medium)), false, new Color?(flag ? style.Quiet : style.Ink), null).With(delegate(InkTextView t)
            {
              t.LineLimit = new int?(1);
            });
            if (flag)
            {
              inkTextView.Strikethrough = new Color?(style.Fresh);
            }
            clickyInkHStack.Children.Add(inkTextView);
            string text = block.Source["url"];
            if (text != null)
            {
              clickyInkHStack.Children.Add(InkTextView.Plain(ClickyInkClipping.Domain(text), ClickyPaperFonts.Typed(11.0, null), style.Quiet).With(delegate(InkTextView t)
              {
                t.LineLimit = new int?(1);
              }));
            }
            Pressable pressable = new Pressable(true)
            {
              Children = { clickyInkHStack },
              HorizontalAlignment = HorizontalAlignment.Left
            };
            pressable.Click += delegate
            {
              string text2 = block.Source["url"];
              if (text2 != null)
              {
                actions.Open(text2);
              }
            };
            AutomationProperties.SetName(pressable, block.Words.Current);
            stackPanel.Children.Add(pressable);
          }
        }
      }
      return ClickyInkObjectKit.Leading(stackPanel, contentWidth);
    }
  }
}
