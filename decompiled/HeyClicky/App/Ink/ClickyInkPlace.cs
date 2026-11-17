using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000277 RID: 631
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkPlace
  {
    // Token: 0x06001AD0 RID: 6864 RVA: 0x005E7B40 File Offset: 0x005E7B40
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      double num = ClickyInkClock.Progress(clock, block.LandsAt + 0.3, block.LandsAt + 0.75);
      Grid grid = new Grid
      {
        Width = 96.0,
        Height = 74.0,
        CornerRadius = new CornerRadius(8.0),
        Background = Ui.Brush(PaperColor.Hex("#EEF1E6"))
      };
      grid.Children.Add(ClickyInkPlace.Map(block.Source.Text, 96.0, 74.0));
      FrameworkElement frameworkElement = ClickyInkPlace.Pin();
      frameworkElement.Transformed(0.0, -18.0 * (1.0 - num) - 4.0, 0.0, 1.0, null);
      frameworkElement.Opacity = num > 0.0;
      grid.Children.Add(frameworkElement);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 3.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      stackPanel.Children.Add(ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand(20.0), true, null, null).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(2);
      }));
      string text = block.Source["note"];
      if (text != null)
      {
        stackPanel.Children.Add(InkTextView.Plain(text, ClickyPaperFonts.Typed(12.0, null), style.Quiet).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(2);
        }));
      }
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 8.0
      };
      string text2 = block.Source["rating"];
      if (text2 != null)
      {
        stackPanel2.Children.Add(InkTextView.Plain("★ " + text2, ClickyPaperFonts.Typed(11.0, new FontWeight?(FontWeights.SemiBold)), PaperColor.Hex("#B7791F")));
      }
      string text3 = block.Source["when"] ?? block.Source["hours"];
      if (text3 != null)
      {
        stackPanel2.Children.Add(InkTextView.Plain(text3, ClickyPaperFonts.Typed(11.0, null), style.Quiet));
      }
      stackPanel.Children.Add(stackPanel2);
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(12.0, InkVerticalAlignment.Center, new UIElement[]
      {
        grid,
        ClickyInkHStack.Flexible<StackPanel>(stackPanel)
      })
      {
        Margin = new Thickness(8.0),
        HorizontalAlignment = HorizontalAlignment.Left
      };
      return new Grid
      {
        Width = contentWidth,
        HorizontalAlignment = HorizontalAlignment.Left,
        Children = 
        {
          ClickyShapeView.Fill(ClickyShape.RoundedRectangle(10.0), PaperColor.White.Opacity(0.55)),
          clickyInkHStack,
          ClickyShapeView.Stroke(ClickyShape.RoundedRectangle(10.0), style.Ink.Opacity(0.08), 1.0)
        }
      };
    }

    // Token: 0x06001AD1 RID: 6865 RVA: 0x005E7EF8 File Offset: 0x005E7EF8
    private static FrameworkElement Map(string text, double width, double height)
    {
      ClickyInkPlace.<>c__DisplayClass1_0 CS$<>8__locals1;
      CS$<>8__locals1.generator = new ClickyPaperRandom(ClickyPaperRandom.StableSeed(text));
      PaperDrawing paperDrawing = new PaperDrawing().Sized(width, height);
      double num = width * ClickyInkPlace.<Map>g__R|1_0(ref CS$<>8__locals1) * 0.5;
      double num2 = height * ClickyInkPlace.<Map>g__R|1_0(ref CS$<>8__locals1) * 0.5;
      paperDrawing.Fill(new PaperPath().AddEllipse(new Rect(num, num2, 34.0, 26.0)), PaperColor.Hex("#CFE7C4"));
      for (int i = 0; i < 3; i++)
      {
        double num3 = height * ClickyInkPlace.<Map>g__R|1_0(ref CS$<>8__locals1);
        double num4 = height * ClickyInkPlace.<Map>g__R|1_0(ref CS$<>8__locals1);
        double num5 = width * ClickyInkPlace.<Map>g__R|1_0(ref CS$<>8__locals1);
        double num6 = height * ClickyInkPlace.<Map>g__R|1_0(ref CS$<>8__locals1);
        paperDrawing.Stroke(new PaperPath().MoveTo(0.0, num3).QuadTo(new Point(width, num4), new Point(num5, num6)), new PaperStroke(PaperColor.White, 4.0, PenLineCap.Flat, PenLineJoin.Miter, null));
      }
      double num7 = width * ClickyInkPlace.<Map>g__R|1_0(ref CS$<>8__locals1);
      double num8 = width * ClickyInkPlace.<Map>g__R|1_0(ref CS$<>8__locals1);
      double num9 = width * ClickyInkPlace.<Map>g__R|1_0(ref CS$<>8__locals1);
      paperDrawing.Stroke(new PaperPath().MoveTo(num7, 0.0).QuadTo(new Point(num8, height), new Point(num9, height / 2.0)), new PaperStroke(PaperColor.Hex("#BFD9F2"), 5.0, PenLineCap.Flat, PenLineJoin.Miter, null));
      return paperDrawing;
    }

    // Token: 0x06001AD2 RID: 6866 RVA: 0x005E807C File Offset: 0x005E807C
    private static FrameworkElement Pin()
    {
      Ellipse ellipse = new Ellipse
      {
        Width = 18.919999999999998,
        Height = 18.919999999999998,
        Fill = Ui.Brush(PaperColor.Hex("#E5484D"))
      };
      Grid grid = new Grid
      {
        Width = 18.919999999999998,
        Height = 18.919999999999998,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        IsHitTestVisible = false
      };
      grid.Children.Add(ellipse);
      TextBlock textBlock = ClickyInkIcon.Glyph("mappin", 11.0, PaperColor.White, true);
      if (textBlock != null)
      {
        grid.Children.Add(textBlock);
      }
      return grid;
    }

    // Token: 0x06001AD3 RID: 6867 RVA: 0x005E8134 File Offset: 0x005E8134
    [CompilerGenerated]
    internal static double <Map>g__R|1_0(ref ClickyInkPlace.<>c__DisplayClass1_0 A_0)
    {
      return A_0.generator.Next() % 1000UL / 1000.0;
    }
  }
}
