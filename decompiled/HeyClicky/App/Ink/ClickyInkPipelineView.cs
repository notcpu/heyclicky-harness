using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000231 RID: 561
  public static class ClickyInkPipelineView
  {
    // Token: 0x060019AC RID: 6572 RVA: 0x005D6580 File Offset: 0x005D6580
    [NullableContext(1)]
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, double width)
    {
      ClickyInkDocument.Block source = block.Source;
      string text;
      if ((text = source["steps"]) == null)
      {
        text = source["stages"] ?? source["values"];
      }
      List<ClickyInkPipeline.Stage> list = ClickyInkPipeline.Stages(text);
      string text2 = ClickyInkChange.Previous(source, "steps");
      List<ClickyInkPipeline.Stage> list2 = ((text2 != null) ? ClickyInkPipeline.Stages(text2) : new List<ClickyInkPipeline.Stage>());
      List<double> list3 = list.Select<ClickyInkPipeline.Stage, double>(delegate(ClickyInkPipeline.Stage s)
      {
        if (ClickyInkNumber.Parse(s.Value) == null)
        {
          return 0.0;
        }
        ClickyInkNumber? clickyInkNumber;
        return clickyInkNumber.GetValueOrDefault().Value;
      }).ToList<double>();
      double num = Math.Max((list3.Count == 0) ? 1.0 : list3.Max(), 1.0);
      double num2 = ((list.Count == 0) ? width : ((width - 26.0 * (double)(list.Count - 1)) / (double)list.Count));
      StackPanel stackPanel = InkDashboardLayout.Width<StackPanel>(new StackPanel
      {
        Spacing = 8.0
      }, width);
      if (block.Words.Current.Length > 0)
      {
        stackPanel.Children.Add(ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand(19.0), true, null, null));
      }
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 0.0,
        HorizontalAlignment = HorizontalAlignment.Left
      };
      for (int i = 0; i < list.Count; i++)
      {
        ClickyInkPipeline.Stage stage = list[i];
        double num3 = ClickyInkClock.Progress(clock, block.LandsAt + 0.15 + (double)i * 0.18, block.LandsAt + 0.5 + (double)i * 0.18);
        string label = stage.Label.ToLowerInvariant();
        string text3 = (from p in list2
          where p.Label.ToLowerInvariant() == label
          select p.Value).FirstOrDefault<string>();
        FrameworkElement frameworkElement = ClickyInkTickingNumber.Create(source, "steps", clock, style, (double)((num2 < 80.0) ? 22 : 26), 0.2 + (double)i * 0.15, stage.Value, text3, false);
        InkTextView inkTextView = InkTextView.Plain(stage.Label, ClickyPaperFonts.Typed(9.5, new FontWeight?(FontWeights.SemiBold)), style.Quiet).With(delegate(InkTextView t)
        {
          t.Uppercase = true;
          t.Tracking = 0.5;
          t.LineLimit = new int?(1);
          t.MinimumScale = 0.75;
        });
        double num4 = ClickyInkDashboardViewMath.PipelineBarWidth(num2, list3[i], num, num3);
        Grid grid = new Grid
        {
          Width = num4,
          Height = 8.0,
          HorizontalAlignment = HorizontalAlignment.Left,
          Margin = new Thickness(0.0, 4.0, 0.0, 0.0)
        };
        grid.Children.Add(ClickyShapeView.Fill(new ClickyInkPipelineView.SketchBar(i + 5), (i == list.Count - 1) ? style.Fresh.Opacity(0.55) : ClickyInkDashboardPalette.Pencil.Opacity(0.2)));
        grid.Children.Add(ClickyShapeView.Stroke(new ClickyInkPipelineView.SketchBar(i + 5), ClickyInkDashboardPalette.Pencil.Opacity(0.55), 0.9));
        StackPanel stackPanel3 = new StackPanel
        {
          Spacing = 2.0,
          Width = Math.Max(0.0, num2),
          Children = { frameworkElement, inkTextView, grid }
        };
        stackPanel3.Opacity = num3 > 0.0;
        stackPanel2.Children.Add(stackPanel3);
        if (i < list.Count - 1)
        {
          ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyInkArrowStroke(i).Trim(0.0, num3), new PaperStroke(ClickyInkDashboardPalette.Pencil.Opacity(0.6), 1.3, PenLineCap.Round, PenLineJoin.Round, null));
          clickyShapeView.Width = 20.0;
          clickyShapeView.Height = 10.0;
          clickyShapeView.HorizontalAlignment = HorizontalAlignment.Center;
          StackPanel stackPanel4 = new StackPanel
          {
            Spacing = 1.0,
            Width = 26.0,
            Margin = new Thickness(0.0, 10.0, 0.0, 0.0),
            Children = { clickyShapeView }
          };
          string text4 = ClickyInkDashboardViewMath.PipelinePercent(list3[i], list3[i + 1]);
          if (text4 != null)
          {
            TextBlock textBlock = ClickyPaperFonts.Rounded(9.0, FontWeights.Medium).Apply(new TextBlock
            {
              Text = text4,
              Foreground = Ui.Brush(style.Quiet),
              IsTextScaleFactorEnabled = false,
              HorizontalAlignment = HorizontalAlignment.Center
            });
            stackPanel4.Children.Add(textBlock);
          }
          stackPanel2.Children.Add(stackPanel4);
        }
      }
      stackPanel.Children.Add(stackPanel2);
      return stackPanel;
    }

    // Token: 0x020007D9 RID: 2009
    private sealed class SketchBar : IClickyShape
    {
      // Token: 0x0600373D RID: 14141 RVA: 0x00681080 File Offset: 0x00681080
      public SketchBar(int seed)
      {
      }

      // Token: 0x0600373E RID: 14142 RVA: 0x00681090 File Offset: 0x00681090
      [NullableContext(1)]
      public PaperPath PathIn(Rect rect)
      {
        ClickyInkPipelineView.SketchBar.<>c__DisplayClass2_0 CS$<>8__locals1;
        CS$<>8__locals1.generator = new ClickyPaperRandom((ulong)((long)(this.<seed>P + 17)));
        PaperPath paperPath = new PaperPath();
        double num = rect.X + ClickyInkPipelineView.SketchBar.<PathIn>g__J|2_0(ref CS$<>8__locals1);
        paperPath.MoveTo(num, rect.Y + ClickyInkPipelineView.SketchBar.<PathIn>g__J|2_0(ref CS$<>8__locals1));
        num = rect.X + rect.Width + ClickyInkPipelineView.SketchBar.<PathIn>g__J|2_0(ref CS$<>8__locals1);
        paperPath.LineTo(num, rect.Y + ClickyInkPipelineView.SketchBar.<PathIn>g__J|2_0(ref CS$<>8__locals1));
        num = rect.X + rect.Width + ClickyInkPipelineView.SketchBar.<PathIn>g__J|2_0(ref CS$<>8__locals1);
        paperPath.LineTo(num, rect.Y + rect.Height + ClickyInkPipelineView.SketchBar.<PathIn>g__J|2_0(ref CS$<>8__locals1));
        num = rect.X + ClickyInkPipelineView.SketchBar.<PathIn>g__J|2_0(ref CS$<>8__locals1);
        paperPath.LineTo(num, rect.Y + rect.Height + ClickyInkPipelineView.SketchBar.<PathIn>g__J|2_0(ref CS$<>8__locals1));
        return paperPath.Close();
      }

      // Token: 0x0600373F RID: 14143 RVA: 0x0068117C File Offset: 0x0068117C
      [CompilerGenerated]
      internal static double <PathIn>g__J|2_0(ref ClickyInkPipelineView.SketchBar.<>c__DisplayClass2_0 A_0)
      {
        return (A_0.generator.Next() % 100UL / 100.0 - 0.5) * 1.6;
      }

      // Token: 0x040021BE RID: 8638
      [CompilerGenerated]
      private int <seed>P = seed;
    }
  }
}
