using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using HeyClicky.Core.Documents.Markdown;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002D8 RID: 728
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MarkdownViewerTableView : Grid
  {
    // Token: 0x17000583 RID: 1411
    // (get) Token: 0x06001EA9 RID: 7849 RVA: 0x00607E0C File Offset: 0x00607E0C
    private Color Plate
    {
      get
      {
        return this.look.ComposerFieldFill;
      }
    }

    // Token: 0x17000584 RID: 1412
    // (get) Token: 0x06001EAA RID: 7850 RVA: 0x00607E1C File Offset: 0x00607E1C
    private Color PlateStroke
    {
      get
      {
        return this.look.ComposerFieldStroke;
      }
    }

    // Token: 0x17000585 RID: 1413
    // (get) Token: 0x06001EAB RID: 7851 RVA: 0x00607E2C File Offset: 0x00607E2C
    private Color HeaderFill
    {
      get
      {
        return this.look.RowDivider.WithAlpha(0.55);
      }
    }

    // Token: 0x17000586 RID: 1414
    // (get) Token: 0x06001EAC RID: 7852 RVA: 0x00607E48 File Offset: 0x00607E48
    private Color InlineCodeFill
    {
      get
      {
        return this.look.RowDivider.WithAlpha(0.75);
      }
    }

    // Token: 0x06001EAD RID: 7853 RVA: 0x00607E64 File Offset: 0x00607E64
    public MarkdownViewerTableView(MarkdownViewerTableContent content, MessagesPalette look)
    {
      this.content = content;
      this.look = look;
      base.Background = Ui.Brush(this.Plate);
      base.CornerRadius = new CornerRadius(8.0);
      this.scroller = new ScrollViewer
      {
        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
        VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
        HorizontalScrollMode = ScrollMode.Enabled,
        VerticalScrollMode = ScrollMode.Disabled,
        IsVerticalScrollChainingEnabled = true,
        ZoomMode = ZoomMode.Disabled
      };
      base.Children.Add(this.scroller);
      base.Children.Add(new Border
      {
        BorderBrush = Ui.Brush(this.PlateStroke),
        BorderThickness = new Thickness(1.0),
        CornerRadius = new CornerRadius(8.0),
        IsHitTestVisible = false
      });
      Ui.ClipRounded(this, 8.0);
      AutomationProperties.SetName(this, "Markdown table");
    }

    // Token: 0x06001EAE RID: 7854 RVA: 0x00607F6C File Offset: 0x00607F6C
    public Size LayoutTable(double availableWidth)
    {
      double num = Math.Max(1.0, Math.Floor(availableWidth));
      if (num == this.lastWidth)
      {
        return this.measuredSize;
      }
      this.lastWidth = num;
      IReadOnlyList<string> headers = this.content.Headers;
      IReadOnlyList<IReadOnlyList<string>> rows = this.content.Rows;
      double num2 = Math.Max(1.0, num - 2.0);
      Func<string, double> func;
      if ((func = MarkdownViewerTableView.<>O.<0>__MeasureCell) == null)
      {
        func = (MarkdownViewerTableView.<>O.<0>__MeasureCell = new Func<string, double>(MarkdownViewerTableView.MeasureCell));
      }
      MarkdownViewerTableLayout markdownViewerTableLayout = MarkdownViewerTableLayout.Compute(headers, rows, num2, func);
      Grid grid = this.BuildGrid(this.content.Headers, this.content.Rows, markdownViewerTableLayout.ColumnWidths);
      double num3 = Math.Ceiling(markdownViewerTableLayout.ColumnWidths.Sum());
      grid.Measure(new Size(num3, double.PositiveInfinity));
      double num4 = Math.Ceiling(grid.DesiredSize.Height);
      grid.Width = num3;
      grid.Height = num4;
      double num5 = (markdownViewerTableLayout.ScrollsHorizontally ? 15.0 : 0.0);
      grid.Margin = new Thickness(0.0, 0.0, 0.0, num5);
      this.scroller.HorizontalScrollBarVisibility = (markdownViewerTableLayout.ScrollsHorizontally ? ScrollBarVisibility.Visible : ScrollBarVisibility.Disabled);
      this.scroller.Content = grid;
      this.measuredSize = new Size(num, num4 + num5 + 2.0);
      base.Width = this.measuredSize.Width;
      base.Height = this.measuredSize.Height;
      return this.measuredSize;
    }

    // Token: 0x06001EAF RID: 7855 RVA: 0x00608110 File Offset: 0x00608110
    private static double MeasureCell(string plainText)
    {
      double num;
      if (MarkdownViewerTableView.CellWidthCache.TryGetValue(plainText, out num))
      {
        return num;
      }
      if (MarkdownViewerTableView.measuringBlock == null)
      {
        MarkdownViewerTableView.measuringBlock = new TextBlock
        {
          FontSize = 13.0,
          FontFamily = MarkdownViewerFonts.System(13.0),
          IsTextScaleFactorEnabled = false
        };
      }
      MarkdownViewerTableView.measuringBlock.Text = plainText;
      MarkdownViewerTableView.measuringBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      double width = MarkdownViewerTableView.measuringBlock.DesiredSize.Width;
      if (MarkdownViewerTableView.CellWidthCache.Count >= 2000)
      {
        MarkdownViewerTableView.CellWidthCache.Clear();
      }
      MarkdownViewerTableView.CellWidthCache[plainText] = width;
      return width;
    }

    // Token: 0x06001EB0 RID: 7856 RVA: 0x006081D0 File Offset: 0x006081D0
    private Grid BuildGrid(IReadOnlyList<string> headers, IReadOnlyList<IReadOnlyList<string>> rows, IReadOnlyList<double> widths)
    {
      MarkdownViewerTableView.<>c__DisplayClass20_0 CS$<>8__locals1;
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.headers = headers;
      CS$<>8__locals1.grid = new Grid
      {
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top
      };
      foreach (double num in widths)
      {
        CS$<>8__locals1.grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = new GridLength(num)
        });
      }
      CS$<>8__locals1.gridRow = 0;
      this.<BuildGrid>g__AddRow|20_0(CS$<>8__locals1.headers, true, ref CS$<>8__locals1);
      this.<BuildGrid>g__AddHairline|20_1(ref CS$<>8__locals1);
      for (int i = 0; i < rows.Count; i++)
      {
        this.<BuildGrid>g__AddRow|20_0(rows[i], false, ref CS$<>8__locals1);
        if (i < rows.Count - 1)
        {
          this.<BuildGrid>g__AddHairline|20_1(ref CS$<>8__locals1);
        }
      }
      return CS$<>8__locals1.grid;
    }

    // Token: 0x06001EB1 RID: 7857 RVA: 0x006082B8 File Offset: 0x006082B8
    private Border Cell(string text, bool isHeader)
    {
      TextBlock textBlock = new TextBlock
      {
        FontSize = 13.0,
        FontFamily = MarkdownViewerFonts.System(13.0),
        FontWeight = (isHeader ? FontWeights.SemiBold : FontWeights.Normal),
        Foreground = Ui.Brush(isHeader ? this.look.SecondaryText : this.look.PrimaryText),
        TextWrapping = TextWrapping.Wrap,
        IsTextSelectionEnabled = true,
        IsTextScaleFactorEnabled = false,
        LineStackingStrategy = LineStackingStrategy.BlockLineHeight,
        LineHeight = Math.Round(18.509, 1)
      };
      List<ValueTuple<int, int>> list = new List<ValueTuple<int, int>>();
      int num = 0;
      foreach (MarkdownInlineRun markdownInlineRun in MarkdownInlineParser.Parse(text))
      {
        Run run = new Run
        {
          Text = markdownInlineRun.Text
        };
        if (markdownInlineRun.IsCode)
        {
          run.FontFamily = MarkdownViewerFonts.Mono;
          run.FontSize = 12.0;
          list.Add(new ValueTuple<int, int>(num, markdownInlineRun.Text.Length));
        }
        if (markdownInlineRun.IsStrong)
        {
          run.FontWeight = FontWeights.SemiBold;
        }
        if (markdownInlineRun.IsEmphasized)
        {
          run.FontStyle = FontStyle.Italic;
        }
        if (markdownInlineRun.IsStrikethrough)
        {
          run.TextDecorations = TextDecorations.Strikethrough;
        }
        string link = markdownInlineRun.Link;
        Uri uri;
        Inline inline;
        if (link != null && Uri.TryCreate(link, UriKind.Absolute, out uri))
        {
          inline = new Hyperlink
          {
            NavigateUri = uri,
            Foreground = Ui.Brush(MarkdownViewerPalette.For(this.look).LinkText),
            Inlines = { run }
          };
        }
        else
        {
          inline = run;
        }
        textBlock.Inlines.Add(inline);
        num += markdownInlineRun.Text.Length;
      }
      if (list.Count > 0)
      {
        TextHighlighter textHighlighter = new TextHighlighter
        {
          Background = Ui.Brush(this.InlineCodeFill),
          Foreground = Ui.Brush(this.look.PrimaryText)
        };
        foreach (ValueTuple<int, int> valueTuple in list)
        {
          int item = valueTuple.Item1;
          int item2 = valueTuple.Item2;
          textHighlighter.Ranges.Add(new TextRange
          {
            StartIndex = item,
            Length = item2
          });
        }
        textBlock.TextHighlighters.Add(textHighlighter);
      }
      return new Border
      {
        Padding = new Thickness(12.0, 8.0, 12.0, 8.0),
        Child = textBlock
      };
    }

    // Token: 0x06001EB3 RID: 7859 RVA: 0x006085E0 File Offset: 0x006085E0
    [CompilerGenerated]
    private void <BuildGrid>g__AddRow|20_0(IReadOnlyList<string> cells, bool isHeader, ref MarkdownViewerTableView.<>c__DisplayClass20_0 A_3)
    {
      A_3.grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      Color? color = (isHeader ? new Color?(this.HeaderFill) : (MarkdownViewerTableLayout.IsBestRow(cells) ? new Color?(MarkdownViewerTableView.BestRowFill) : null));
      if (color != null)
      {
        Color valueOrDefault = color.GetValueOrDefault();
        Border border = new Border
        {
          Background = Ui.Brush(valueOrDefault)
        };
        Grid.SetRow(border, A_3.gridRow);
        Grid.SetColumnSpan(border, Math.Max(1, A_3.headers.Count));
        A_3.grid.Children.Add(border);
      }
      for (int i = 0; i < A_3.headers.Count; i++)
      {
        Border border2 = this.Cell((i < cells.Count) ? cells[i] : "", isHeader);
        if (i < A_3.headers.Count - 1)
        {
          border2.BorderBrush = Ui.Brush(this.PlateStroke);
        }
        border2.BorderThickness = new Thickness(0.0, 0.0, i < A_3.headers.Count - 1, 0.0);
        Grid.SetRow(border2, A_3.gridRow);
        Grid.SetColumn(border2, i);
        A_3.grid.Children.Add(border2);
      }
      int gridRow = A_3.gridRow;
      A_3.gridRow = gridRow + 1;
    }

    // Token: 0x06001EB4 RID: 7860 RVA: 0x0060876C File Offset: 0x0060876C
    [CompilerGenerated]
    private void <BuildGrid>g__AddHairline|20_1(ref MarkdownViewerTableView.<>c__DisplayClass20_0 A_1)
    {
      A_1.grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0)
      });
      Border border = new Border
      {
        Background = Ui.Brush(this.PlateStroke),
        Height = 1.0
      };
      Grid.SetRow(border, A_1.gridRow);
      Grid.SetColumnSpan(border, Math.Max(1, A_1.headers.Count));
      A_1.grid.Children.Add(border);
      int gridRow = A_1.gridRow;
      A_1.gridRow = gridRow + 1;
    }

    // Token: 0x04000EC8 RID: 3784
    private static readonly Color BestRowFill = DS.Rgb(0.3, 0.78, 0.48, 0.14);

    // Token: 0x04000EC9 RID: 3785
    private const double ScrollerHeight = 15.0;

    // Token: 0x04000ECA RID: 3786
    private readonly MarkdownViewerTableContent content;

    // Token: 0x04000ECB RID: 3787
    private readonly MessagesPalette look;

    // Token: 0x04000ECC RID: 3788
    private readonly ScrollViewer scroller;

    // Token: 0x04000ECD RID: 3789
    private double lastWidth = -1.0;

    // Token: 0x04000ECE RID: 3790
    private Size measuredSize;

    // Token: 0x04000ECF RID: 3791
    private static readonly Dictionary<string, double> CellWidthCache = new Dictionary<string, double>();

    // Token: 0x04000ED0 RID: 3792
    [Nullable(2)]
    private static TextBlock measuringBlock;

    // Token: 0x02000900 RID: 2304
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x0400260E RID: 9742
      [Nullable(new byte[] { 0, 1 })]
      public static Func<string, double> <0>__MeasureCell;
    }
  }
}
