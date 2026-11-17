using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using HeyClicky.Core.Documents.Spreadsheet;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Windows.UI.Text;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002E3 RID: 739
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SpreadsheetPreview : Grid
  {
    // Token: 0x170005A5 RID: 1445
    // (get) Token: 0x06001F5B RID: 8027 RVA: 0x0060EB44 File Offset: 0x0060EB44
    public SpreadsheetWorkbook Workbook { get; }

    // Token: 0x170005A6 RID: 1446
    // (get) Token: 0x06001F5C RID: 8028 RVA: 0x0060EB4C File Offset: 0x0060EB4C
    public SpreadsheetGridView GridView { get; } = new SpreadsheetGridView();

    // Token: 0x170005A7 RID: 1447
    // (get) Token: 0x06001F5D RID: 8029 RVA: 0x0060EB54 File Offset: 0x0060EB54
    // (set) Token: 0x06001F5E RID: 8030 RVA: 0x0060EB5C File Offset: 0x0060EB5C
    public int SelectedSheetIndex { get; private set; }

    // Token: 0x06001F5F RID: 8031 RVA: 0x0060EB68 File Offset: 0x0060EB68
    public SpreadsheetPreview(SpreadsheetWorkbook workbook)
    {
      this.Workbook = workbook;
      this.SelectedSheetIndex = workbook.InitialSheetIndex;
      base.RequestedTheme = ElementTheme.Light;
      base.Padding = new Thickness(10.0);
      Grid card = new Grid
      {
        Background = Ui.Brush(SpreadsheetChromePalette.Card)
      };
      foreach (GridLength gridLength in new GridLength[]
      {
        new GridLength(32.0),
        new GridLength(1.0),
        new GridLength(1.0, GridUnitType.Star),
        new GridLength(1.0),
        new GridLength(38.0)
      })
      {
        card.RowDefinitions.Add(new RowDefinition
        {
          Height = gridLength
        });
      }
      this.nameBox = Ui.Text("", 12.0, FontWeights.Medium, SpreadsheetChromePalette.PrimaryText, TextAlignment.Left, 1, false, false);
      this.nameBox.Width = 64.0;
      this.nameBox.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
      this.nameBox.VerticalAlignment = VerticalAlignment.Center;
      TextBlock textBlock = new TextBlock
      {
        Text = "fx",
        FontSize = 12.5,
        FontWeight = FontWeights.Medium,
        FontStyle = FontStyle.Italic,
        FontFamily = new FontFamily("Cambria, Georgia, Times New Roman"),
        Foreground = Ui.Brush(SpreadsheetChromePalette.TertiaryText),
        Margin = new Thickness(10.0, 0.0, 10.0, 0.0),
        VerticalAlignment = VerticalAlignment.Center,
        IsTextScaleFactorEnabled = false
      };
      this.formula = Ui.Text("", 12.5, FontWeights.Normal, SpreadsheetChromePalette.PrimaryText, TextAlignment.Left, 1, false, false);
      this.formula.VerticalAlignment = VerticalAlignment.Center;
      this.formula.IsTextSelectionEnabled = true;
      Grid grid = new Grid
      {
        Height = 32.0,
        Background = Ui.Brush(SpreadsheetChromePalette.Card)
      };
      foreach (GridLength gridLength2 in new GridLength[]
      {
        GridLength.Auto,
        GridLength.Auto,
        GridLength.Auto,
        new GridLength(1.0, GridUnitType.Star),
        GridLength.Auto
      })
      {
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = gridLength2
        });
      }
      Border border = new Border
      {
        Width = 1.0,
        Height = 18.0,
        Background = Ui.Brush(SpreadsheetChromePalette.Line),
        VerticalAlignment = VerticalAlignment.Center
      };
      SpreadsheetPreview.Place(grid, this.nameBox, 0);
      SpreadsheetPreview.Place(grid, border, 1);
      SpreadsheetPreview.Place(grid, textBlock, 2);
      SpreadsheetPreview.Place(grid, this.formula, 3);
      this.linkSlot.Margin = new Thickness(0.0, 0.0, 8.0, 0.0);
      this.linkSlot.VerticalAlignment = VerticalAlignment.Center;
      SpreadsheetPreview.Place(grid, this.linkSlot, 4);
      Grid.SetRow(grid, 0);
      card.Children.Add(grid);
      card.Children.Add(SpreadsheetPreview.Rule(1));
      Grid.SetRow(this.GridView, 2);
      card.Children.Add(this.GridView);
      card.Children.Add(SpreadsheetPreview.Rule(3));
      Grid grid2 = new Grid
      {
        Height = 38.0,
        Background = Ui.Brush(SpreadsheetChromePalette.BarFill),
        ColumnSpacing = 8.0
      };
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      ScrollViewer scrollViewer = new ScrollViewer
      {
        Content = this.tabs,
        HorizontalScrollMode = ScrollMode.Enabled,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
        VerticalScrollMode = ScrollMode.Disabled,
        VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
        IsTabStop = false,
        HorizontalAlignment = HorizontalAlignment.Left
      };
      grid2.Children.Add(scrollViewer);
      this.statistics = Ui.Text("", 11.5, FontWeights.Medium, SpreadsheetChromePalette.SecondaryText, TextAlignment.Left, 1, false, false);
      this.statistics.IsTextSelectionEnabled = true;
      this.statistics.VerticalAlignment = VerticalAlignment.Center;
      Typography.SetNumeralAlignment(this.statistics, FontNumeralAlignment.Tabular);
      this.statisticsPill = new Border
      {
        Child = this.statistics,
        Height = 24.0,
        Padding = new Thickness(10.0, 0.0, 10.0, 0.0),
        CornerRadius = new CornerRadius(6.0),
        Background = Ui.Brush(Colors.White),
        BorderBrush = Ui.Brush(SpreadsheetChromePalette.Line),
        BorderThickness = new Thickness(1.0),
        Margin = new Thickness(0.0, 0.0, 8.0, 0.0),
        VerticalAlignment = VerticalAlignment.Center,
        Visibility = Visibility.Collapsed
      };
      Grid.SetColumn(this.statisticsPill, 1);
      grid2.Children.Add(this.statisticsPill);
      Grid.SetRow(grid2, 4);
      card.Children.Add(grid2);
      Border border2 = new Border
      {
        CornerRadius = new CornerRadius(10.0),
        BorderBrush = Ui.Brush(DS.Black(0.08)),
        BorderThickness = new Thickness(1.0),
        IsHitTestVisible = false
      };
      Grid.SetRowSpan(border2, 5);
      card.Children.Add(border2);
      card.Loaded += delegate(object _, RoutedEventArgs _)
      {
        Ui.ClipRounded(card, 10.0);
      };
      base.Children.Add(card);
      this.GridView.SelectionChanged += this.ShowSummary;
      this.GridView.OpenHyperlink += this.OpenLink;
      this.BuildTabs();
      this.GridView.Show(this.CurrentSheet);
    }

    // Token: 0x170005A8 RID: 1448
    // (get) Token: 0x06001F60 RID: 8032 RVA: 0x0060F2E4 File Offset: 0x0060F2E4
    private SpreadsheetSheet CurrentSheet
    {
      get
      {
        return this.Workbook.Sheets[Math.Min(this.SelectedSheetIndex, this.Workbook.Sheets.Count - 1)];
      }
    }

    // Token: 0x06001F61 RID: 8033 RVA: 0x0060F314 File Offset: 0x0060F314
    private static void Place(Grid grid, FrameworkElement element, int column)
    {
      Grid.SetColumn(element, column);
      grid.Children.Add(element);
    }

    // Token: 0x06001F62 RID: 8034 RVA: 0x0060F32C File Offset: 0x0060F32C
    private static Border Rule(int row)
    {
      Border border = new Border();
      border.Height = 1.0;
      border.Background = Ui.Brush(SpreadsheetChromePalette.Line);
      Grid.SetRow(border, row);
      return border;
    }

    // Token: 0x06001F63 RID: 8035 RVA: 0x0060F35C File Offset: 0x0060F35C
    public void SelectSheet(int index)
    {
      if (index < 0 || index >= this.Workbook.Sheets.Count || index == this.SelectedSheetIndex)
      {
        return;
      }
      this.SelectedSheetIndex = index;
      this.BuildTabs();
      this.GridView.Show(this.CurrentSheet);
    }

    // Token: 0x06001F64 RID: 8036 RVA: 0x0060F3A8 File Offset: 0x0060F3A8
    private void BuildTabs()
    {
      this.tabs.Children.Clear();
      for (int j = 0; j < this.Workbook.Sheets.Count; j++)
      {
        int i = j;
        this.tabs.Children.Add(SpreadsheetPreview.SheetTab(this.Workbook.Sheets[j].Name, j == this.SelectedSheetIndex, delegate
        {
          this.SelectSheet(i);
        }));
      }
    }

    // Token: 0x06001F65 RID: 8037 RVA: 0x0060F434 File Offset: 0x0060F434
    private static Pressable SheetTab(string name, bool isSelected, Action action)
    {
      TextBlock textBlock = Ui.Text(name, 12.0, isSelected ? FontWeights.SemiBold : FontWeights.Medium, isSelected ? SpreadsheetChromePalette.SelectedTabText : SpreadsheetChromePalette.SecondaryText, TextAlignment.Left, 1, false, false);
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      textBlock.Margin = new Thickness(12.0, 0.0, 12.0, 0.0);
      Pressable tab = new Pressable(true)
      {
        Height = 28.0,
        CornerRadius = new CornerRadius(7.0),
        VerticalAlignment = VerticalAlignment.Center
      };
      tab.Background = Ui.Brush(isSelected ? SpreadsheetChromePalette.SelectedTabFill : DS.Black(0.0));
      tab.Children.Add(textBlock);
      tab.HoverChanged += delegate(bool hovering)
      {
        tab.Background = Ui.Brush(isSelected ? SpreadsheetChromePalette.SelectedTabFill : DS.Black(hovering ? 0.05 : 0.0));
      };
      tab.Click += action;
      AutomationProperties.SetName(tab, name);
      return tab;
    }

    // Token: 0x06001F66 RID: 8038 RVA: 0x0060F56C File Offset: 0x0060F56C
    private void ShowSummary(SpreadsheetSelectionSummary summary)
    {
      this.nameBox.Text = summary.NameBoxText;
      string text = summary.FormulaBarText.Replace("\n", " ");
      this.formula.Text = text;
      ToolTipService.SetToolTip(this.formula, string.IsNullOrEmpty(summary.FormulaBarText) ? null : summary.FormulaBarText);
      this.linkSlot.Children.Clear();
      string hyperlink = summary.Hyperlink;
      Uri uri;
      if (hyperlink != null && hyperlink.Length > 0 && Uri.TryCreate(hyperlink, UriKind.RelativeOrAbsolute, out uri))
      {
        this.linkSlot.Children.Add(this.LinkButton(uri));
      }
      this.statistics.Text = summary.Statistics ?? "";
      this.statisticsPill.Visibility = ((summary.Statistics == null) ? Visibility.Collapsed : Visibility.Visible);
    }

    // Token: 0x06001F67 RID: 8039 RVA: 0x0060F644 File Offset: 0x0060F644
    private Pressable LinkButton(Uri url)
    {
      SpreadsheetPreview.<>c__DisplayClass25_0 CS$<>8__locals1 = new SpreadsheetPreview.<>c__DisplayClass25_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.url = url;
      TextBlock textBlock = Icons.Glyph("\ue8a7", 11.0, SpreadsheetChromePalette.Link);
      TextBlock textBlock2 = Ui.Text((!CS$<>8__locals1.url.IsAbsoluteUri || string.IsNullOrEmpty(CS$<>8__locals1.url.Host)) ? "Open link" : CS$<>8__locals1.url.Host, 11.5, FontWeights.Medium, SpreadsheetChromePalette.Link, TextAlignment.Left, 1, false, false);
      textBlock2.VerticalAlignment = VerticalAlignment.Center;
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 4.0,
        Margin = new Thickness(8.0, 0.0, 8.0, 0.0),
        VerticalAlignment = VerticalAlignment.Center,
        Children = { textBlock, textBlock2 }
      };
      CS$<>8__locals1.button = new Pressable(true)
      {
        Height = 22.0,
        CornerRadius = new CornerRadius(6.0)
      };
      CS$<>8__locals1.fill = SpreadsheetChromePalette.SelectedTabFill;
      CS$<>8__locals1.button.Background = Ui.Brush(CS$<>8__locals1.<LinkButton>g__Faded|0(0.6));
      CS$<>8__locals1.button.HoverChanged += delegate(bool hovering)
      {
        CS$<>8__locals1.button.Background = Ui.Brush(base.<LinkButton>g__Faded|0(hovering ? 1.0 : 0.6));
      };
      CS$<>8__locals1.button.Children.Add(stackPanel);
      CS$<>8__locals1.button.Click += delegate
      {
        CS$<>8__locals1.<>4__this.OpenLink(CS$<>8__locals1.url.ToString());
      };
      AutomationProperties.SetName(CS$<>8__locals1.button, "Open link");
      return CS$<>8__locals1.button.Tip(CS$<>8__locals1.url.ToString());
    }

    // Token: 0x06001F68 RID: 8040 RVA: 0x0060F7FC File Offset: 0x0060F7FC
    private void OpenLink(string link)
    {
      Uri uri;
      if (!Uri.TryCreate(link, UriKind.Absolute, out uri))
      {
        return;
      }
      base.DispatcherQueue.TryEnqueue(delegate
      {
        ChatBlockViews.OpenPath(uri.ToString());
      });
    }

    // Token: 0x04000F21 RID: 3873
    private readonly TextBlock nameBox;

    // Token: 0x04000F22 RID: 3874
    private readonly TextBlock formula;

    // Token: 0x04000F23 RID: 3875
    private readonly Grid linkSlot = new Grid();

    // Token: 0x04000F24 RID: 3876
    private readonly StackPanel tabs = new StackPanel
    {
      Orientation = Orientation.Horizontal,
      Spacing = 2.0,
      Padding = new Thickness(8.0, 0.0, 8.0, 0.0),
      VerticalAlignment = VerticalAlignment.Center
    };

    // Token: 0x04000F25 RID: 3877
    private readonly Border statisticsPill;

    // Token: 0x04000F26 RID: 3878
    private readonly TextBlock statistics;
  }
}
