using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000263 RID: 611
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkTable
  {
    // Token: 0x06001A8C RID: 6796 RVA: 0x005E3160 File Offset: 0x005E3160
    private static PaperFont FontFor(ClickyInkTableFont font, double scale)
    {
      PaperFont paperFont;
      if (font != ClickyInkTableFont.Name)
      {
        if (font != ClickyInkTableFont.Header)
        {
          paperFont = ClickyPaperFonts.Typed(13.0 * scale, new FontWeight?(FontWeights.SemiBold));
        }
        else
        {
          paperFont = ClickyPaperFonts.Typed(Math.Max(8.0, 9.5 * scale), new FontWeight?(FontWeights.SemiBold));
        }
      }
      else
      {
        paperFont = ClickyPaperFonts.Hand(17.5 * scale);
      }
      return paperFont;
    }

    // Token: 0x06001A8D RID: 6797 RVA: 0x005E31D4 File Offset: 0x005E31D4
    public static double Measure(string text, ClickyInkTableFont font, double scale)
    {
      if (text.Length == 0)
      {
        return 0.0;
      }
      double width;
      using (CanvasTextFormat canvasTextFormat = ClickyInkTable.FontFor(font, scale).TextFormat())
      {
        using (CanvasTextLayout canvasTextLayout = new CanvasTextLayout(CanvasDevice.GetSharedDevice(), text, canvasTextFormat, 100000f, 100000f))
        {
          width = canvasTextLayout.LayoutBoundsIncludingTrailingWhitespace.Width;
        }
      }
      return width;
    }

    // Token: 0x06001A8E RID: 6798 RVA: 0x005E325C File Offset: 0x005E325C
    public static ClickyInkTableLayout Layout(ClickyInkDocument.Block source, double width)
    {
      IReadOnlyList<string> columns = source.Columns;
      IReadOnlyList<IReadOnlyList<string>> readOnlyList = source.Items.Select<ClickyInkDocument.Item, IReadOnlyList<string>>((ClickyInkDocument.Item i) => i.Cells).ToList<IReadOnlyList<string>>();
      ClickyInkTableLayout.Measurer measurer;
      if ((measurer = ClickyInkTable.<>O.<0>__Measure) == null)
      {
        measurer = (ClickyInkTable.<>O.<0>__Measure = new ClickyInkTableLayout.Measurer(ClickyInkTable.Measure));
      }
      return ClickyInkTableLayout.Make(columns, readOnlyList, width, measurer);
    }

    // Token: 0x06001A8F RID: 6799 RVA: 0x005E32C0 File Offset: 0x005E32C0
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, double width)
    {
      ClickyInkTableLayout clickyInkTableLayout = ClickyInkTable.Layout(block.Source, width);
      double fontScale = clickyInkTableLayout.FontScale;
      StackPanel stackPanel = new StackPanel
      {
        Width = width,
        HorizontalAlignment = HorizontalAlignment.Left
      };
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(0.0, InkVerticalAlignment.Center, Array.Empty<UIElement>())
      {
        Margin = new Thickness(0.0, 0.0, 0.0, 6.0)
      };
      for (int i = 0; i < clickyInkTableLayout.Widths.Count; i++)
      {
        InkTextView inkTextView = InkTextView.Plain((i < block.Source.Columns.Count) ? block.Source.Columns[i].ToUpperInvariant() : "", ClickyPaperFonts.Typed(Math.Max(8.0, 9.5 * fontScale), new FontWeight?(FontWeights.SemiBold)), style.Quiet).With(delegate(InkTextView t)
        {
          t.Tracking = 0.4;
          t.LineLimit = new int?(2);
          t.MinimumScale = 0.8;
        });
        inkTextView.Margin = new Thickness(0.0, 0.0, 8.0, 0.0);
        clickyInkHStack.Children.Add(ClickyInkTable.Cell(inkTextView, clickyInkTableLayout.Widths[i]));
      }
      stackPanel.Children.Add(clickyInkHStack);
      stackPanel.Children.Add(new Border
      {
        Height = 1.2,
        Background = Ui.Brush(style.Ink.Opacity(0.3))
      });
      foreach (ClickyInkLiveNote.Item item in block.Items)
      {
        stackPanel.Children.Add(ClickyInkGrow.Around(ClickyInkTable.Row(item, clickyInkTableLayout, clock, style, width), clock, item.LandsAt, item.VanishesAt));
      }
      return stackPanel;
    }

    // Token: 0x06001A90 RID: 6800 RVA: 0x005E34EC File Offset: 0x005E34EC
    private static FrameworkElement Cell(FrameworkElement content, double width)
    {
      content.HorizontalAlignment = HorizontalAlignment.Left;
      return new Grid
      {
        Width = Math.Max(0.0, width),
        Children = { content }
      };
    }

    // Token: 0x06001A91 RID: 6801 RVA: 0x005E351C File Offset: 0x005E351C
    private static FrameworkElement Row(ClickyInkLiveNote.Item item, ClickyInkTableLayout layout, double clock, ClickyInkStyle style, double width)
    {
      bool flag = item.Source.Mark.GetValueOrDefault() == ClickyInkDocument.Mark.Pick;
      double? goneAt = item.GoneAt;
      bool flag2;
      if (goneAt != null)
      {
        double valueOrDefault = goneAt.GetValueOrDefault();
        flag2 = clock >= valueOrDefault;
      }
      else
      {
        flag2 = item.Source.Mark.GetValueOrDefault() == ClickyInkDocument.Mark.Gone;
      }
      bool flag3 = flag2;
      double fontScale = layout.FontScale;
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(0.0, InkVerticalAlignment.FirstBaseline, Array.Empty<UIElement>())
      {
        Margin = new Thickness(0.0, 7.0, 0.0, 7.0)
      };
      for (int i = 0; i < layout.Widths.Count; i++)
      {
        FrameworkElement frameworkElement;
        if (i < item.Cells.Count)
        {
          ClickyInkLiveNote.Words words = item.Cells[i];
          bool flag4 = ClickyInkTableRules.IsCrossingOut(words, clock);
          PaperFont paperFont = ((i == 0) ? ClickyPaperFonts.Hand(17.5 * fontScale) : ClickyPaperFonts.Typed(13.0 * fontScale, new FontWeight?(flag ? FontWeights.SemiBold : FontWeights.Normal)));
          InkTextView inkTextView = ClickyInkWordsView.Make(words, clock, style, paperFont, false, flag3 ? new Color?(style.Quiet) : null, null);
          if (flag3)
          {
            inkTextView.Strikethrough = new Color?(style.Fresh);
          }
          inkTextView.LineLimit = new int?(layout.Rigid[i] ? (flag4 ? 2 : 1) : (flag4 ? 6 : 3));
          inkTextView.MinimumScale = (layout.Rigid[i] ? 0.7 : 0.85);
          frameworkElement = inkTextView;
        }
        else
        {
          frameworkElement = InkTextView.Plain("", ClickyPaperFonts.Typed(13.0 * fontScale, null), style.Ink);
        }
        frameworkElement.Margin = new Thickness(0.0, 0.0, 8.0, 0.0);
        clickyInkHStack.Children.Add(ClickyInkTable.Cell(frameworkElement, layout.Widths[i]));
      }
      Grid grid = new Grid();
      if (flag)
      {
        double num = ClickyInkClock.Progress(clock, item.LandsAt + 0.3, item.LandsAt + 0.8);
        ClickyShapeView clickyShapeView = ClickyShapeView.Fill(ClickyShape.RoundedRectangle(3.0), ClickyInkTable.PickHighlight);
        clickyShapeView.Width = Math.Max(0.0, width * num);
        clickyShapeView.Height = 20.0;
        clickyShapeView.HorizontalAlignment = HorizontalAlignment.Left;
        clickyShapeView.VerticalAlignment = VerticalAlignment.Center;
        clickyShapeView.Transformed(0.0, 0.0, -0.6, 1.0, null);
        grid.Children.Add(clickyShapeView);
      }
      grid.Children.Add(clickyInkHStack);
      return new StackPanel
      {
        Children = 
        {
          grid,
          new Border
          {
            Height = 1.0,
            Background = Ui.Brush(style.Ink.Opacity(0.09))
          }
        }
      };
    }

    // Token: 0x04000CBE RID: 3262
    private static readonly Color PickHighlight = PaperColor.Hex("#FFE27A").Opacity(0.55);

    // Token: 0x02000825 RID: 2085
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040022C2 RID: 8898
      [Nullable(0)]
      public static ClickyInkTableLayout.Measurer <0>__Measure;
    }
  }
}
