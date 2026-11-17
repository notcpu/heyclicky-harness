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
  // Token: 0x02000240 RID: 576
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkFrontPagePlans
  {
    // Token: 0x060019DA RID: 6618 RVA: 0x005DAAD8 File Offset: 0x005DAAD8
    public static double MissionTileHeight(IReadOnlyList<ClickyInkDocument.Block> blocks)
    {
      double num;
      if (blocks.Count != 0)
      {
        Func<ClickyInkDocument.Block, double> func;
        if ((func = ClickyInkFrontPagePlans.<>O.<0>__TileHeight) == null)
        {
          func = (ClickyInkFrontPagePlans.<>O.<0>__TileHeight = new Func<ClickyInkDocument.Block, double>(ClickyInkDashboardFormNeeds.TileHeight));
        }
        num = blocks.Max<ClickyInkDocument.Block>(func);
      }
      else
      {
        num = 90.0;
      }
      return num + 4.0;
    }

    // Token: 0x060019DB RID: 6619 RVA: 0x005DAB18 File Offset: 0x005DAB18
    public static ClickyInkFrontPagePlanner.Plan For(ClickyInkLiveNote note, ClickyInkFrontPageVariant variant, ClickyHomeSpreadPaper paper)
    {
      string text = ClickyInkFrontPagePlanner.Key(note, variant);
      ClickyInkFrontPagePlanner.Plan plan;
      if (ClickyInkFrontPagePlans.Cache.TryGetValue(text, out plan))
      {
        return plan;
      }
      double num = note.FinishedClock + 30.0;
      ClickyInkStyle clickyInkStyle = new ClickyInkStyle(paper.Ink, paper.Quiet, new Color?(paper.Deep), note.LatestEdit(num));
      ClickyInkFrontPagePlanner.Plan plan2 = ClickyInkFrontPagePlanner.Make(note, variant, new ClickyInkFrontPagePlans.Measurer(num, clickyInkStyle, paper));
      if (ClickyInkFrontPagePlans.Cache.Count > 60)
      {
        ClickyInkFrontPagePlans.Cache.Clear();
      }
      ClickyInkFrontPagePlans.Cache[text] = plan2;
      return plan2;
    }

    // Token: 0x04000C77 RID: 3191
    private static readonly Dictionary<string, ClickyInkFrontPagePlanner.Plan> Cache = new Dictionary<string, ClickyInkFrontPagePlanner.Plan>();

    // Token: 0x020007FA RID: 2042
    [Nullable(0)]
    private sealed class Measurer : IClickyInkFrontPageMeasurer
    {
      // Token: 0x060037B8 RID: 14264 RVA: 0x00684FDC File Offset: 0x00684FDC
      public Measurer(double clock, ClickyInkStyle style, ClickyHomeSpreadPaper paper)
      {
      }

      // Token: 0x170007EC RID: 2028
      // (get) Token: 0x060037B9 RID: 14265 RVA: 0x00684FFC File Offset: 0x00684FFC
      public double ContentWidth
      {
        get
        {
          return 378.0;
        }
      }

      // Token: 0x060037BA RID: 14266 RVA: 0x00685008 File Offset: 0x00685008
      public double Group(ClickyInkGroup group)
      {
        return ClickyInkFormKit.MeasureHeight(ClickyInkGroupView.Make(group, this.<clock>P, this.<style>P, this.<paper>P, new ClickyInkActions()), 378.0);
      }

      // Token: 0x060037BB RID: 14267 RVA: 0x00685038 File Offset: 0x00685038
      public double Table(ClickyInkGroup group, double width)
      {
        return ClickyInkFormKit.MeasureHeight(ClickyInkTable.Create(group.Blocks[0], this.<clock>P, this.<style>P, width), width);
      }

      // Token: 0x060037BC RID: 14268 RVA: 0x00685060 File Offset: 0x00685060
      public double Tiles(IReadOnlyList<ClickyInkLiveNote.Block> blocks, double width)
      {
        return ClickyInkFormKit.MeasureHeight(ClickyInkDashboardFormNeeds.Tiles(blocks.ToList<ClickyInkLiveNote.Block>(), this.<clock>P, this.<style>P, width, ClickyInkTiles.Look.Ruled), width);
      }

      // Token: 0x060037BD RID: 14269 RVA: 0x00685084 File Offset: 0x00685084
      public double MissionTileHeight(IReadOnlyList<ClickyInkDocument.Block> blocks)
      {
        return ClickyInkFrontPagePlans.MissionTileHeight(blocks);
      }

      // Token: 0x060037BE RID: 14270 RVA: 0x0068508C File Offset: 0x0068508C
      public double Headline(ClickyInkDashboardBoard board, double width, bool isMission)
      {
        double num = width - (double)(isMission ? 36 : 0);
        ClickyInkGroup quiet = board.Quiet;
        FrameworkElement frameworkElement;
        if (quiet != null)
        {
          StackPanel stackPanel = new StackPanel
          {
            Spacing = 4.0
          };
          stackPanel.Children.Add(ClickyInkWordsView.Make(quiet.Blocks[0].Words, this.<clock>P, this.<style>P, ClickyPaperFonts.Hand((double)(isMission ? 34 : 42)), true, null, null).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(2);
          }));
          stackPanel.Children.Add(ClickyInkFormKit.Text("checked", ClickyPaperFonts.Typed(14.0, new FontWeight?(FontWeights.Medium)), this.<style>P.Ink, 0.0, 1.0, new int?(1)));
          ClickyInkGroup answer = board.Answer;
          ClickyInkLiveNote.Block block = ((answer != null) ? answer.Blocks.FirstOrDefault<ClickyInkLiveNote.Block>() : null);
          if (block != null)
          {
            stackPanel.Children.Add(ClickyInkFormKit.Text("last news " + block.Words.Current, ClickyPaperFonts.Hand(21.0), this.<style>P.Ink, 0.0, 1.0, new int?(2)).Padding(0.0, 4.0, 0.0, 0.0));
          }
          frameworkElement = new ClickyInkHStack(22.0, InkVerticalAlignment.Center, new UIElement[]
          {
            new ClickyPaperStamp(ClickyThingStatus.Ready, "ALL QUIET", 22.0, -6.0),
            ClickyInkHStack.Flexible<StackPanel>(stackPanel),
            new ClickyInkSpacer()
          })
          {
            Width = num
          };
        }
        else
        {
          ClickyInkGroup answer2 = board.Answer;
          ClickyInkLiveNote.Block block2 = ((answer2 != null) ? answer2.Blocks.FirstOrDefault<ClickyInkLiveNote.Block>() : null);
          if (block2 != null)
          {
            int num2 = block2.Words.Current.CharacterCount();
            double num3 = (double)(isMission ? ((num2 > 70) ? 30 : 36) : ((num2 > 80) ? 36 : ((num2 > 50) ? 42 : 48)));
            ClickyInkHStack clickyInkHStack = new ClickyInkHStack(22.0, InkVerticalAlignment.Center, Array.Empty<UIElement>())
            {
              Width = num
            };
            if (isMission)
            {
              clickyInkHStack.Children.Add(new Grid
              {
                Width = 28.0,
                Height = 28.0
              });
            }
            clickyInkHStack.Children.Add(ClickyInkWordsView.Make(block2.Words, this.<clock>P, this.<style>P, ClickyPaperFonts.Hand(num3), true, null, null).With(delegate(InkTextView t)
            {
              t.LineSpacing = -2.0;
            }));
            clickyInkHStack.Children.Add(new ClickyInkSpacer());
            string text = block2.Source["big"];
            if (text != null && text.Length > 0)
            {
              StackPanel stackPanel2 = new StackPanel
              {
                Spacing = 2.0
              };
              stackPanel2.Children.Add(ClickyInkFormKit.Text(text, ClickyPaperFonts.BigNumber((double)((text.CharacterCount() > 6) ? 46 : 60)), this.<style>P.Ink, 0.0, 1.0, new int?(1)).Padding(12.0, 4.0, 12.0, 4.0));
              string text2 = block2.Source["note"];
              if (text2 != null && text2.Length > 0)
              {
                stackPanel2.Children.Add(ClickyInkFormKit.Text(text2, ClickyPaperFonts.Hand(19.0), this.<style>P.Ink, 0.0, 1.0, new int?(2)).With(delegate(InkTextView t)
                {
                  t.TextAlignment = TextAlignment.Center;
                }).MaxWidthOf(220.0));
              }
              clickyInkHStack.Children.Add(stackPanel2);
            }
            frameworkElement = clickyInkHStack;
          }
          else
          {
            frameworkElement = new Grid
            {
              Height = 10.0
            };
          }
        }
        frameworkElement.Margin = new Thickness((double)(isMission ? 18 : 0));
        return ClickyInkFormKit.MeasureHeight(frameworkElement, width);
      }

      // Token: 0x04002223 RID: 8739
      [CompilerGenerated]
      private double <clock>P = clock;

      // Token: 0x04002224 RID: 8740
      [CompilerGenerated]
      private ClickyInkStyle <style>P = style;

      // Token: 0x04002225 RID: 8741
      [CompilerGenerated]
      private ClickyHomeSpreadPaper <paper>P = paper;
    }

    // Token: 0x020007FB RID: 2043
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04002226 RID: 8742
      [Nullable(0)]
      public static Func<ClickyInkDocument.Block, double> <0>__TileHeight;
    }
  }
}
