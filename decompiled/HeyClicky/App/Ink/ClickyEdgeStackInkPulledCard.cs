using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200020F RID: 527
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyEdgeStackInkPulledCard : StackPanel
  {
    // Token: 0x0600193C RID: 6460 RVA: 0x005CF830 File Offset: 0x005CF830
    [NullableContext(2)]
    public ClickyEdgeStackInkPulledCard([Nullable(1)] ClickyInkLiveNote note, [Nullable(1)] ClickyEdgeStackInkReading reading, double clock, ClickyHomeSpreadPaper paper, bool isJob, string liveStep, [Nullable(1)] ClickyInkActions actions, Action onOpen = null, Action onPutBack = null, Action onUnpin = null, double waitingMaximumHeight = 420.0)
    {
      base.Spacing = 12.0;
      base.Padding = new Thickness(0.0, 6.0, 0.0, 0.0);
      ClickyInkStyle clickyInkStyle = ClickyEdgeStackInkLook.Style(note, paper, clock, reading);
      ClickyInkGroup clickyInkGroup = ClickyEdgeStackInkPulledCard.WaitingGroup(note, reading, clock);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 10.0
      };
      stackPanel.Children.Add(new ClickyEdgeStackInkHeader(reading, clock, clickyInkStyle, 27.0, 9.5));
      FrameworkElement frameworkElement = ClickyEdgeStackInkLead.Make(reading, clock, clickyInkStyle, true, clickyInkGroup == null);
      if (frameworkElement != null)
      {
        stackPanel.Children.Add(frameworkElement);
      }
      FrameworkElement frameworkElement2 = ClickyEdgeStackInkFooter.Make(reading, liveStep, clickyInkStyle, 2, clickyInkGroup != null);
      if (frameworkElement2 != null)
      {
        stackPanel.Children.Add(frameworkElement2);
      }
      if (clickyInkGroup != null)
      {
        FrameworkElement frameworkElement3 = ClickyEdgeStackInkPulledCard.Waiting(clickyInkGroup, clock, clickyInkStyle, paper, actions, waitingMaximumHeight);
        frameworkElement3.Margin = new Thickness(-6.0, -2.0, -6.0, -6.0);
        stackPanel.Children.Add(frameworkElement3);
      }
      List<string> list = ClickyEdgeStackInkPulledCard.OtherPageNames(note, clock);
      if (list.Count > 0)
      {
        TextBlock textBlock = PaperText.Line("also in the note: " + string.Join(" · ", list), ClickyPaperFonts.Hand(14.5), clickyInkStyle.Quiet, TextAlignment.Left);
        textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
        textBlock.MaxLines = 1;
        textBlock.Margin = new Thickness(0.0, 2.0, 0.0, 0.0);
        stackPanel.Children.Add(textBlock);
      }
      Grid grid = new Grid
      {
        Width = 420.0,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      grid.Children.Add(new ClickyEdgeStackInkPaper(paper, isJob, true, 9.0));
      stackPanel.Padding = new Thickness(21.0, (double)(isJob ? 22 : 20), 21.0, 18.0);
      grid.Children.Add(stackPanel);
      base.Children.Add(grid);
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 8.0,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      stackPanel2.Children.Add(new ClickyPaperGelButton("Open", onOpen, ClickyPaperGel.Blue, 32.0, 13.0, 13.0, null));
      stackPanel2.Children.Add(new ClickyPaperGelButton("Put back", onPutBack, ClickyPaperGel.White, 32.0, 13.0, 13.0, null));
      stackPanel2.Children.Add(new ClickyPaperGelButton("Unpin", onUnpin, ClickyPaperGel.White, 32.0, 13.0, 13.0, null));
      base.Children.Add(stackPanel2);
    }

    // Token: 0x0600193D RID: 6461 RVA: 0x005CFB48 File Offset: 0x005CFB48
    private static List<string> OtherPageNames(ClickyInkLiveNote note, double clock)
    {
      Func<ClickyInkLiveNote.Block, bool> <>9__3;
      return (from page in note.Pages.Skip<ClickyInkLiveNote.Page>(1).Where<ClickyInkLiveNote.Page>(delegate(ClickyInkLiveNote.Page page)
        {
          if (page.LandsAt <= clock)
          {
            IEnumerable<ClickyInkLiveNote.Block> blocks = page.Blocks;
            Func<ClickyInkLiveNote.Block, bool> func;
            if ((func = <>9__3) == null)
            {
              func = (<>9__3 = (ClickyInkLiveNote.Block b) => b.IsPresent(clock));
            }
            return blocks.Any<ClickyInkLiveNote.Block>(func);
          }
          return false;
        })
        select ClickyInkParser.Emphasis.Strip(page.Name.TextAt(clock)).TrimWs() into name
        where name.Length > 0
        select name).ToList<string>();
    }

    // Token: 0x0600193E RID: 6462 RVA: 0x005CFBBC File Offset: 0x005CFBBC
    [return: Nullable(2)]
    private static ClickyInkGroup WaitingGroup(ClickyInkLiveNote note, ClickyEdgeStackInkReading reading, double clock)
    {
      string id = reading.WaitingBlockID ?? reading.PendingBlockID;
      if (id == null)
      {
        return null;
      }
      Func<ClickyInkLiveNote.Block, bool> <>9__1;
      Func<ClickyInkGroup, bool> <>9__0;
      foreach (ClickyInkLiveNote.Page page in note.Pages)
      {
        IEnumerable<ClickyInkGroup> enumerable = ClickyInkGroup.Groups(page, clock);
        Func<ClickyInkGroup, bool> func;
        if ((func = <>9__0) == null)
        {
          func = (<>9__0 = delegate(ClickyInkGroup g)
          {
            IEnumerable<ClickyInkLiveNote.Block> blocks = g.Blocks;
            Func<ClickyInkLiveNote.Block, bool> func2;
            if ((func2 = <>9__1) == null)
            {
              func2 = (<>9__1 = (ClickyInkLiveNote.Block b) => b.Id == id);
            }
            return blocks.Any<ClickyInkLiveNote.Block>(func2);
          });
        }
        ClickyInkGroup clickyInkGroup = enumerable.FirstOrDefault<ClickyInkGroup>(func);
        if (clickyInkGroup != null)
        {
          return clickyInkGroup;
        }
      }
      return null;
    }

    // Token: 0x0600193F RID: 6463 RVA: 0x005CFC64 File Offset: 0x005CFC64
    private static FrameworkElement Waiting(ClickyInkGroup group, double clock, ClickyInkStyle style, ClickyHomeSpreadPaper paper, ClickyInkActions actions, double maximumHeight)
    {
      ClickyInkEnvironment clickyInkEnvironment = ClickyInkEnvironment.Default.<Clone>$();
      clickyInkEnvironment.ContentWidth = 378.0;
      FrameworkElement frameworkElement;
      using (ClickyInkEnvironment.Use(clickyInkEnvironment))
      {
        frameworkElement = ClickyInkGroupView.Make(group, clock, style, paper, actions);
      }
      FrameworkElement frameworkElement2 = frameworkElement.Column(378.0);
      frameworkElement2.Margin = new Thickness(6.0);
      return new ClickyEdgeStackFitOrScroll(maximumHeight, frameworkElement2)
      {
        Margin = new Thickness(-6.0)
      };
    }

    // Token: 0x04000C4D RID: 3149
    public const double CardWidth = 420.0;

    // Token: 0x04000C4E RID: 3150
    public const double RoomAboveAndBelowWaiting = 236.0;
  }
}
