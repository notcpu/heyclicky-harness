using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200023E RID: 574
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkDeskView : Grid
  {
    // Token: 0x17000481 RID: 1153
    // (get) Token: 0x060019D0 RID: 6608 RVA: 0x005DA368 File Offset: 0x005DA368
    public static double CardWidth
    {
      get
      {
        return 410.0;
      }
    }

    // Token: 0x060019D1 RID: 6609 RVA: 0x005DA374 File Offset: 0x005DA374
    public ClickyInkDeskView(ClickyInkLiveNote note, double clock, double width = 1440.0, ClickyHomeSpreadPaper? paper = null)
    {
      ClickyInkDeskView.<>c__DisplayClass3_1 CS$<>8__locals2 = new ClickyInkDeskView.<>c__DisplayClass3_1();
      ClickyHomeSpreadPaper clickyHomeSpreadPaper = paper ?? ClickyHomeSpreadPaper.Note;
      ClickyInkStyle clickyInkStyle = new ClickyInkStyle(clickyHomeSpreadPaper.Ink, clickyHomeSpreadPaper.Quiet, new Color?(clickyHomeSpreadPaper.Deep), note.LatestEdit(clock));
      List<ValueTuple<string, ClickyInkGroup>> list = note.Pages.SelectMany<ClickyInkLiveNote.Page, ValueTuple<string, ClickyInkGroup>>((ClickyInkLiveNote.Page page) => from g in ClickyInkGroup.Groups(page, clock)
        select new ValueTuple<string, ClickyInkGroup>(page.Name.Current, g)).ToList<ValueTuple<string, ClickyInkGroup>>();
      CS$<>8__locals2.answer = list.FirstOrDefault<ValueTuple<string, ClickyInkGroup>>(([TupleElementNames(new string[] { "Page", "Group" })] ValueTuple<string, ClickyInkGroup> g) => g.Item2.Kind == ClickyInkDocument.Kind.Answer);
      List<ValueTuple<string, ClickyInkGroup>> list2 = list.Where<ValueTuple<string, ClickyInkGroup>>(delegate([TupleElementNames(new string[] { "Page", "Group" })] ValueTuple<string, ClickyInkGroup> g)
      {
        string id = g.Item2.Id;
        ClickyInkGroup item3 = CS$<>8__locals2.answer.Item2;
        return id != ((item3 != null) ? item3.Id : null);
      }).ToList<ValueTuple<string, ClickyInkGroup>>();
      CS$<>8__locals2.columns = Math.Max(1, (int)((width - 40.0) / (ClickyInkDeskView.CardWidth + 36.0)));
      base.Width = width;
      base.HorizontalAlignment = HorizontalAlignment.Left;
      base.VerticalAlignment = VerticalAlignment.Top;
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 36.0,
        Padding = new Thickness(30.0, 34.0, 30.0, 34.0)
      };
      int column;
      Func<ValueTuple<string, ClickyInkGroup>, int, bool> <>9__4;
      int column2;
      for (column = 0; column < CS$<>8__locals2.columns; column = column2 + 1)
      {
        StackPanel stackPanel2 = new StackPanel
        {
          Spacing = 26.0,
          Width = ClickyInkDeskView.CardWidth,
          VerticalAlignment = VerticalAlignment.Top
        };
        if (column == 0)
        {
          stackPanel2.Children.Add(ClickyInkDeskView.TitleCard(note, CS$<>8__locals2.answer.Item2, clock, clickyInkStyle, clickyHomeSpreadPaper));
        }
        IEnumerable<ValueTuple<string, ClickyInkGroup>> enumerable = list2;
        Func<ValueTuple<string, ClickyInkGroup>, int, bool> func;
        if ((func = <>9__4) == null)
        {
          func = (<>9__4 = ([TupleElementNames(new string[] { "Page", "Group" })] ValueTuple<string, ClickyInkGroup> _, int i) => (i + 1) % CS$<>8__locals2.columns == column);
        }
        foreach (ValueTuple<string, ClickyInkGroup> valueTuple in enumerable.Where<ValueTuple<string, ClickyInkGroup>>(func))
        {
          string item = valueTuple.Item1;
          ClickyInkGroup item2 = valueTuple.Item2;
          stackPanel2.Children.Add(ClickyInkDeskView.Card(item2, item, clock, clickyInkStyle, clickyHomeSpreadPaper));
        }
        stackPanel.Children.Add(stackPanel2);
        column2 = column;
      }
      base.Children.Add(stackPanel);
    }

    // Token: 0x060019D2 RID: 6610 RVA: 0x005DA61C File Offset: 0x005DA61C
    private static FrameworkElement TitleCard(ClickyInkLiveNote note, [Nullable(2)] ClickyInkGroup answer, double clock, ClickyInkStyle style, ClickyHomeSpreadPaper paper)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 10.0,
        Padding = new Thickness(16.0)
      };
      ClickyInkLiveNote.Words title = note.Title;
      if (title != null)
      {
        stackPanel.Children.Add(ClickyInkWordsView.Make(title, clock, style, ClickyPaperFonts.Hand(30.0), true, null, null));
      }
      if (answer != null)
      {
        stackPanel.Children.Add(ClickyInkGroupView.Make(answer, clock, style, paper, new ClickyInkActions()));
      }
      return new Grid
      {
        Width = ClickyInkDeskView.CardWidth,
        HorizontalAlignment = HorizontalAlignment.Left,
        Children = 
        {
          ClickyInkDeskView.PaperCard(PaperColor.Hex("#FFFDF6"), false),
          stackPanel
        }
      }.Transformed(0.0, 0.0, -1.0, 1.0, null);
    }

    // Token: 0x060019D3 RID: 6611 RVA: 0x005DA720 File Offset: 0x005DA720
    private static FrameworkElement Card(ClickyInkGroup group, string label, double clock, ClickyInkStyle style, ClickyHomeSpreadPaper paper)
    {
      ClickyInkLiveNote.Block block = group.Blocks[0];
      double num = ClickyInkClock.Progress(clock, block.LandsAt, block.LandsAt + 0.55);
      double num2 = ClickyInkDeskView.Tilts[(int)(ClickyPaperRandom.StableSeed(block.Id) % 6UL)];
      ClickyInkDocument.Kind kind = group.Kind;
      bool flag = kind - ClickyInkDocument.Kind.Sticky <= 3 || kind - ClickyInkDocument.Kind.Letter <= 1;
      FrameworkElement frameworkElement;
      if (flag)
      {
        frameworkElement = ClickyInkGroupView.Make(group, clock, style, paper, new ClickyInkActions()).Column(ClickyInkDeskView.CardWidth);
      }
      else
      {
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 8.0,
          Padding = new Thickness(16.0)
        };
        stackPanel.Children.Add(InkTextView.Plain(label.ToLowerInvariant(), ClickyPaperFonts.Hand(15.0), PaperColor.Hex("#C2410C").Opacity(0.8)));
        stackPanel.Children.Add(ClickyInkGroupView.Make(group, clock, style, paper, new ClickyInkActions()).Column(378.0));
        frameworkElement = new Grid
        {
          Width = ClickyInkDeskView.CardWidth,
          HorizontalAlignment = HorizontalAlignment.Left,
          Children = 
          {
            ClickyInkDeskView.PaperCard(PaperColor.Hex("#FBF8EF"), true),
            stackPanel
          }
        };
      }
      double num3 = 1.0 - ClickyInkDeskView.EaseOut(num);
      frameworkElement.Transformed(0.0, 0.0, num2, 1.0, null);
      frameworkElement.Transformed(0.0, -40.0 * num3, 0.0, 1.0 + 0.12 * num3, null);
      frameworkElement.Opacity = Math.Min(1.0, num * 2.5);
      return frameworkElement;
    }

    // Token: 0x060019D4 RID: 6612 RVA: 0x005DA924 File Offset: 0x005DA924
    private static FrameworkElement PaperCard(Color tint, bool ruled = false)
    {
      PaperSurface paperSurface = new PaperSurface(ClickyShape.RoundedRectangle(4.0), PaperFill.Of(tint), new double?(0.4)).WithShadow(PaperColor.Black.Opacity(0.35), 7.0, 0.0, 5.0);
      if (ruled)
      {
        paperSurface.WithWash(PaperFill.Of(PaperColor.Hex("#E5484D").Opacity(0.35)), (Size s) => new Rect(0.0, 34.0, s.Width, 1.0), true);
        paperSurface.Washes.Add(new PaperWash(PaperFill.Of(PaperColor.Hex("#3C5AA0").Opacity(0.09)), null, true, delegate(Size size)
        {
          PaperPath paperPath = new PaperPath();
          for (double num = 57.0; num < size.Height; num += 23.0)
          {
            paperPath.AddRect(new Rect(0.0, num, size.Width, 1.0));
          }
          return paperPath;
        }));
      }
      return paperSurface;
    }

    // Token: 0x060019D5 RID: 6613 RVA: 0x005DAA28 File Offset: 0x005DAA28
    private static double EaseOut(double t)
    {
      return 1.0 - Math.Pow(1.0 - t, 3.0);
    }

    // Token: 0x04000C76 RID: 3190
    private static readonly double[] Tilts = new double[] { -2.2, 1.4, -0.8, 2.0, -1.6, 0.9 };
  }
}
