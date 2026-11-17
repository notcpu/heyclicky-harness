using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;
using HeyClicky.App.Design;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.UI
{
  // Token: 0x020000B4 RID: 180
  [NullableContext(1)]
  [Nullable(0)]
  public static class Markdown
  {
    // Token: 0x14000010 RID: 16
    // (add) Token: 0x06000760 RID: 1888 RVA: 0x005376F0 File Offset: 0x005376F0
    // (remove) Token: 0x06000761 RID: 1889 RVA: 0x00537724 File Offset: 0x00537724
    [Nullable(new byte[] { 2, 2, 1 })]
    [field: Nullable(new byte[] { 2, 2, 1 })]
    public static event Action<XamlRoot, Uri> LinkOpened;

    // Token: 0x06000762 RID: 1890 RVA: 0x00537758 File Offset: 0x00537758
    internal static void RaiseLinkOpened([Nullable(2)] XamlRoot root, Uri uri)
    {
      Action<XamlRoot, Uri> linkOpened = Markdown.LinkOpened;
      if (linkOpened == null)
      {
        return;
      }
      linkOpened(root, uri);
    }

    // Token: 0x06000763 RID: 1891 RVA: 0x0053776C File Offset: 0x0053776C
    public static StackPanel Render(string source, Markdown.Options o, [TupleElementNames(new string[] { "thumb", "link" })] [Nullable(new byte[] { 1, 0, 1, 1 })] out List<ValueTuple<string, string>> images)
    {
      images = new List<ValueTuple<string, string>>();
      StackPanel stackPanel = new StackPanel
      {
        Spacing = o.BlockSpacing
      };
      string[] array = source.Replace("\r\n", "\n").Split('\n', StringSplitOptions.None);
      int i = 0;
      while (i < array.Length)
      {
        string text = array[i];
        string text2 = text.Trim();
        if (text2.Length == 0)
        {
          i++;
        }
        else
        {
          Match match = Markdown.ImageRegex().Match(text2);
          if (match.Success && match.Value.Length == text2.Length)
          {
            images.Add(new ValueTuple<string, string>(match.Groups[2].Value, match.Groups[2].Value));
            i++;
          }
          else if (text2.StartsWith("```"))
          {
            List<string> list = new List<string>();
            i++;
            while (i < array.Length && !array[i].Trim().StartsWith("```"))
            {
              list.Add(array[i++]);
            }
            i++;
            stackPanel.Children.Add(Markdown.CodeBlock(string.Join("\n", list), o));
          }
          else
          {
            Match match2 = Markdown.HeadingRegex().Match(text2);
            if (match2.Success)
            {
              int length = match2.Groups[1].Value.Length;
              double num = o.HeadingSizes[Math.Min(length, o.HeadingSizes.Length) - 1];
              TextBlock textBlock = Markdown.Inline(match2.Groups[2].Value, o, num, FontWeights.SemiBold, null);
              if (length <= 2)
              {
                textBlock.Margin = new Thickness(0.0, 4.0, 0.0, 0.0);
              }
              stackPanel.Children.Add(textBlock);
              i++;
            }
            else if (Markdown.RuleRegex().IsMatch(text2))
            {
              stackPanel.Children.Add(new Border
              {
                Height = 1.0,
                Background = Ui.Brush(o.Secondary.WithAlpha(0.2)),
                Margin = new Thickness(0.0, 2.0, 0.0, 2.0)
              });
              i++;
            }
            else if (text2.StartsWith('>'))
            {
              List<string> list2 = new List<string>();
              while (i < array.Length && array[i].Trim().StartsWith('>'))
              {
                list2.Add(array[i++].Trim().TrimStart('>').Trim());
              }
              Border border = new Border
              {
                Width = 2.0,
                Background = Ui.Brush(o.Secondary.WithAlpha(0.35))
              };
              TextBlock textBlock2 = Markdown.Inline(string.Join(" ", list2), o, o.FontSize, FontWeights.Normal, new Color?(o.Secondary));
              Grid grid = new Grid
              {
                ColumnSpacing = 8.0
              };
              grid.ColumnDefinitions.Add(new ColumnDefinition
              {
                Width = GridLength.Auto
              });
              grid.ColumnDefinitions.Add(new ColumnDefinition
              {
                Width = new GridLength(1.0, GridUnitType.Star)
              });
              Grid.SetColumn(textBlock2, 1);
              grid.Children.Add(border);
              grid.Children.Add(textBlock2);
              stackPanel.Children.Add(grid);
            }
            else if (text2.StartsWith('|') && i + 1 < array.Length && Markdown.TableRuleRegex().IsMatch(array[i + 1].Trim()))
            {
              List<string[]> list3 = new List<string[]> { Markdown.Cells(text2) };
              i += 2;
              while (i < array.Length && array[i].Trim().StartsWith('|'))
              {
                list3.Add(Markdown.Cells(array[i++].Trim()));
              }
              stackPanel.Children.Add(Markdown.Table(list3, o));
            }
            else if (Markdown.BulletRegex().IsMatch(text) || Markdown.NumberRegex().IsMatch(text))
            {
              bool flag = Markdown.NumberRegex().IsMatch(text);
              StackPanel stackPanel2 = new StackPanel
              {
                Spacing = o.ListRowSpacing
              };
              int num2 = 0;
              while (i < array.Length && (flag ? Markdown.NumberRegex().IsMatch(array[i]) : Markdown.BulletRegex().IsMatch(array[i])))
              {
                string text3 = (flag ? Markdown.NumberRegex().Replace(array[i], "", 1) : Markdown.BulletRegex().Replace(array[i], "", 1));
                string text4;
                if (!flag)
                {
                  text4 = "•";
                }
                else
                {
                  DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
                  defaultInterpolatedStringHandler.AppendFormatted<int>(++num2);
                  defaultInterpolatedStringHandler.AppendLiteral(".");
                  text4 = defaultInterpolatedStringHandler.ToStringAndClear();
                }
                TextBlock textBlock3 = Ui.Text(text4, o.FontSize, FontWeights.Normal, o.Secondary, TextAlignment.Left, 0, false, false);
                if (flag)
                {
                  Typography.SetNumeralAlignment(textBlock3, FontNumeralAlignment.Tabular);
                }
                TextBlock textBlock4 = Markdown.Inline(text3, o, o.FontSize, FontWeights.Normal, null);
                double num3 = (flag ? o.NumberIndent : o.BulletIndent);
                Grid grid2 = new Grid
                {
                  ColumnSpacing = (double)((num3 > 0.0) ? 0 : 8)
                };
                grid2.ColumnDefinitions.Add(new ColumnDefinition
                {
                  Width = ((num3 > 0.0) ? new GridLength(num3) : GridLength.Auto)
                });
                grid2.ColumnDefinitions.Add(new ColumnDefinition
                {
                  Width = new GridLength(1.0, GridUnitType.Star)
                });
                Grid.SetColumn(textBlock4, 1);
                Markdown.ApplyLineSpacing(textBlock3, o, o.FontSize);
                grid2.Children.Add(textBlock3);
                grid2.Children.Add(textBlock4);
                stackPanel2.Children.Add(grid2);
                i++;
              }
              stackPanel.Children.Add(stackPanel2);
            }
            else
            {
              List<string> list4 = new List<string>();
              while (i < array.Length)
              {
                string text5 = array[i].Trim();
                if (text5.Length == 0 || text5.StartsWith("```") || Markdown.HeadingRegex().IsMatch(text5) || text5.StartsWith('>') || Markdown.RuleRegex().IsMatch(text5) || Markdown.BulletRegex().IsMatch(array[i]) || Markdown.NumberRegex().IsMatch(array[i]) || (text5.StartsWith('|') && i + 1 < array.Length && Markdown.TableRuleRegex().IsMatch(array[i + 1].Trim())))
                {
                  break;
                }
                list4.Add(array[i].TrimEnd());
                i++;
              }
              stackPanel.Children.Add(Markdown.Inline(string.Join("\n", list4), o, o.FontSize, FontWeights.Normal, null));
            }
          }
        }
      }
      return stackPanel;
    }

    // Token: 0x06000764 RID: 1892 RVA: 0x00537E9C File Offset: 0x00537E9C
    private static FrameworkElement CodeBlock(string code, Markdown.Options o)
    {
      TextBlock textBlock = new TextBlock
      {
        Text = code,
        FontFamily = DS.Fonts.Mono,
        FontSize = 12.0,
        Foreground = Ui.Brush(o.Text),
        IsTextSelectionEnabled = true,
        IsTextScaleFactorEnabled = false
      };
      ScrollViewer scrollViewer = new ScrollViewer
      {
        Content = textBlock,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
        VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
        HorizontalScrollMode = ScrollMode.Enabled,
        VerticalScrollMode = ScrollMode.Disabled
      };
      if (!o.CodeCopyButton)
      {
        textBlock.Margin = o.CodePadding;
      }
      Border border = new Border
      {
        Child = scrollViewer,
        Padding = (o.CodeCopyButton ? o.CodePadding : new Thickness(0.0)),
        CornerRadius = new CornerRadius(8.0),
        Background = Ui.Brush(o.CodeFill),
        BorderBrush = Ui.Brush(o.CodeStroke),
        BorderThickness = new Thickness(o.CodeCopyButton ? 1.0 : 0.5)
      };
      if (!o.CodeCopyButton)
      {
        return border;
      }
      Grid grid = new Grid();
      grid.Children.Add(border);
      Pressable copy = new Pressable(true)
      {
        Width = 22.0,
        Height = 22.0,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Top,
        Margin = new Thickness(0.0, 6.0, 6.0, 0.0),
        Opacity = 0.0
      };
      TextBlock glyph = Icons.Glyph("\ue8c8", 11.0, o.Secondary);
      copy.Children.Add(glyph);
      copy.Click += delegate
      {
        DataPackage dataPackage = new DataPackage();
        dataPackage.SetText(code);
        Clipboard.SetContent(dataPackage);
        glyph.Text = "\ue73e";
      };
      ToolTipService.SetToolTip(copy, "Copy");
      grid.PointerEntered += delegate(object _, PointerRoutedEventArgs _)
      {
        copy.Opacity = 1.0;
      };
      grid.PointerExited += delegate(object _, PointerRoutedEventArgs e)
      {
        if (PointerFrames.IsStale(e))
        {
          return;
        }
        copy.Opacity = 0.0;
        glyph.Text = "\ue8c8";
      };
      grid.Children.Add(copy);
      return grid;
    }

    // Token: 0x06000765 RID: 1893 RVA: 0x005380F0 File Offset: 0x005380F0
    private static string[] Cells(string row)
    {
      return (from c in row.Trim().Trim('|').Split('|', StringSplitOptions.None)
        select c.Trim()).ToArray<string>();
    }

    // Token: 0x06000766 RID: 1894 RVA: 0x00538130 File Offset: 0x00538130
    private static FrameworkElement Table(List<string[]> rows, Markdown.Options o)
    {
      int num = rows.Max<string[]>((string[] r) => r.Length);
      Grid grid = new Grid();
      for (int i = 0; i < num; i++)
      {
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
      }
      for (int j = 0; j < rows.Count; j++)
      {
        grid.RowDefinitions.Add(new RowDefinition
        {
          Height = GridLength.Auto
        });
        for (int k = 0; k < num; k++)
        {
          TextBlock textBlock = Markdown.Inline((k < rows[j].Length) ? rows[j][k] : "", o, o.FontSize - 1.0, (j == 0) ? FontWeights.SemiBold : FontWeights.Normal, null);
          textBlock.TextWrapping = TextWrapping.NoWrap;
          Border border = new Border
          {
            Child = textBlock,
            Padding = new Thickness(10.0, 6.0, 10.0, 6.0),
            BorderBrush = Ui.Brush(DS.White(0.08)),
            BorderThickness = new Thickness(0.0, 0.0, k < num - 1, j < rows.Count - 1),
            Background = ((j == 0) ? Ui.Brush(DS.White(0.04)) : null)
          };
          Grid.SetRow(border, j);
          Grid.SetColumn(border, k);
          grid.Children.Add(border);
        }
      }
      Border border2 = new Border
      {
        Child = grid,
        CornerRadius = new CornerRadius(8.0),
        BorderBrush = Ui.Brush(DS.White(0.1)),
        BorderThickness = new Thickness(1.0),
        HorizontalAlignment = HorizontalAlignment.Left
      };
      return new ScrollViewer
      {
        Content = border2,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
        VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
        HorizontalScrollMode = ScrollMode.Enabled,
        VerticalScrollMode = ScrollMode.Disabled
      };
    }

    // Token: 0x06000767 RID: 1895 RVA: 0x0053837C File Offset: 0x0053837C
    private static void ApplyLineSpacing(TextBlock tb, Markdown.Options o, double size)
    {
      if (o.LineSpacing <= 0.0)
      {
        return;
      }
      tb.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
      tb.LineHeight = Math.Ceiling(size * 1.33) + o.LineSpacing;
    }

    // Token: 0x06000768 RID: 1896 RVA: 0x005383B4 File Offset: 0x005383B4
    public static TextBlock Inline(string text, Markdown.Options o, double size, FontWeight weight, Color? color = null)
    {
      TextBlock textBlock = new TextBlock
      {
        FontSize = size,
        FontWeight = weight,
        FontFamily = DS.Fonts.For(size),
        Foreground = Ui.Brush(color ?? o.Text),
        TextWrapping = TextWrapping.Wrap,
        IsTextSelectionEnabled = true,
        IsTextScaleFactorEnabled = false,
        SelectionHighlightColor = Ui.Brush(DS.Colors.CursorBlue)
      };
      Markdown.ApplyLineSpacing(textBlock, o, size);
      int num = 0;
      foreach (object obj in Markdown.InlineRegex().Matches(text))
      {
        Match match = (Match)obj;
        if (match.Index > num)
        {
          TextBlock textBlock2 = textBlock;
          int num2 = num;
          Markdown.AddRun(textBlock2, text.Substring(num2, match.Index - num2));
        }
        if (match.Groups["bold"].Success)
        {
          textBlock.Inlines.Add(new Run
          {
            Text = match.Groups["bold"].Value,
            FontWeight = o.StrongWeight
          });
        }
        else if (match.Groups["ital"].Success)
        {
          textBlock.Inlines.Add(new Run
          {
            Text = match.Groups["ital"].Value,
            FontStyle = FontStyle.Italic
          });
        }
        else if (match.Groups["code"].Success)
        {
          textBlock.Inlines.Add(new Run
          {
            Text = match.Groups["code"].Value,
            FontFamily = DS.Fonts.Mono
          });
        }
        else if (match.Groups["ltext"].Success)
        {
          Hyperlink hyperlink = new Hyperlink
          {
            Foreground = Ui.Brush(o.Link),
            UnderlineStyle = UnderlineStyle.None
          };
          Uri uri;
          if (Uri.TryCreate(match.Groups["lurl"].Value, UriKind.Absolute, out uri))
          {
            hyperlink.NavigateUri = uri;
            hyperlink.Click += delegate(Hyperlink s, HyperlinkClickEventArgs _)
            {
              Action<XamlRoot, Uri> linkOpened = Markdown.LinkOpened;
              if (linkOpened == null)
              {
                return;
              }
              linkOpened(s.XamlRoot, uri);
            };
          }
          hyperlink.Inlines.Add(new Run
          {
            Text = match.Groups["ltext"].Value
          });
          textBlock.Inlines.Add(hyperlink);
        }
        num = match.Index + match.Length;
      }
      if (num < text.Length)
      {
        TextBlock textBlock3 = textBlock;
        int num2 = num;
        Markdown.AddRun(textBlock3, text.Substring(num2, text.Length - num2));
      }
      return textBlock;
    }

    // Token: 0x06000769 RID: 1897 RVA: 0x005386B0 File Offset: 0x005386B0
    private static void AddRun(TextBlock tb, string s)
    {
      string[] array = s.Split('\n', StringSplitOptions.None);
      for (int i = 0; i < array.Length; i++)
      {
        if (i > 0)
        {
          tb.Inlines.Add(new LineBreak());
        }
        if (array[i].Length > 0)
        {
          tb.Inlines.Add(new Run
          {
            Text = array[i]
          });
        }
      }
    }

    // Token: 0x0600076A RID: 1898 RVA: 0x00538710 File Offset: 0x00538710
    [GeneratedRegex("^(#{1,6})\\s+(.*)$")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex HeadingRegex()
    {
      return <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__HeadingRegex_1.Instance;
    }

    // Token: 0x0600076B RID: 1899 RVA: 0x00538718 File Offset: 0x00538718
    [GeneratedRegex("^\\s*([-*_])(\\s*\\1){2,}\\s*$")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex RuleRegex()
    {
      return <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__RuleRegex_2.Instance;
    }

    // Token: 0x0600076C RID: 1900 RVA: 0x00538720 File Offset: 0x00538720
    [GeneratedRegex("^\\s*[-*]\\s+")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex BulletRegex()
    {
      return <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__BulletRegex_3.Instance;
    }

    // Token: 0x0600076D RID: 1901 RVA: 0x00538728 File Offset: 0x00538728
    [GeneratedRegex("^\\s*\\d+\\.\\s+")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex NumberRegex()
    {
      return <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__NumberRegex_4.Instance;
    }

    // Token: 0x0600076E RID: 1902 RVA: 0x00538730 File Offset: 0x00538730
    [GeneratedRegex("!\\[([^\\]]*)\\]\\(([^)]+)\\)")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex ImageRegex()
    {
      return <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__ImageRegex_5.Instance;
    }

    // Token: 0x0600076F RID: 1903 RVA: 0x00538738 File Offset: 0x00538738
    [GeneratedRegex("^\\|?\\s*:?-{2,}:?\\s*(\\|\\s*:?-{2,}:?\\s*)*\\|?$")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex TableRuleRegex()
    {
      return <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__TableRuleRegex_6.Instance;
    }

    // Token: 0x06000770 RID: 1904 RVA: 0x00538740 File Offset: 0x00538740
    [GeneratedRegex("\\*\\*(?<bold>[^*]+)\\*\\*|__(?<bold>[^_]+)__|\\*(?<ital>[^*\\s][^*]*)\\*|`(?<code>[^`]+)`|\\[(?<ltext>[^\\]]+)\\]\\((?<lurl>[^)\\s]+)\\)")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex InlineRegex()
    {
      return <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__InlineRegex_7.Instance;
    }

    // Token: 0x04000235 RID: 565
    public static readonly Markdown.Options NotchResponse = new Markdown.Options(13.0, DS.Colors.TextPrimary, DS.Colors.Blue300, DS.White(0.55))
    {
      StrongWeight = FontWeights.Bold
    };

    // Token: 0x02000461 RID: 1121
    [Nullable(0)]
    public sealed class Options : IEquatable<Markdown.Options>
    {
      // Token: 0x06002A1D RID: 10781 RVA: 0x0064E4E8 File Offset: 0x0064E4E8
      public Options(double FontSize, Color Text, Color Link, Color Secondary)
      {
        this.FontSize = FontSize;
        this.Text = Text;
        this.Link = Link;
        this.Secondary = Secondary;
        this.BlockSpacing = 10.0;
        this.HeadingSizes = new double[] { 20.0, 17.0, 15.0, 14.0 };
        this.ListRowSpacing = 4.0;
        this.CodeFill = DS.White(0.06);
        this.CodeStroke = DS.White(0.08);
        this.CodePadding = new Thickness(10.0);
        this.StrongWeight = FontWeights.SemiBold;
        base..ctor();
      }

      // Token: 0x170006F6 RID: 1782
      // (get) Token: 0x06002A1E RID: 10782 RVA: 0x0064E594 File Offset: 0x0064E594
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(Markdown.Options);
        }
      }

      // Token: 0x170006F7 RID: 1783
      // (get) Token: 0x06002A1F RID: 10783 RVA: 0x0064E5A0 File Offset: 0x0064E5A0
      // (set) Token: 0x06002A20 RID: 10784 RVA: 0x0064E5A8 File Offset: 0x0064E5A8
      public double FontSize { get; set; }

      // Token: 0x170006F8 RID: 1784
      // (get) Token: 0x06002A21 RID: 10785 RVA: 0x0064E5B4 File Offset: 0x0064E5B4
      // (set) Token: 0x06002A22 RID: 10786 RVA: 0x0064E5BC File Offset: 0x0064E5BC
      public Color Text { get; set; }

      // Token: 0x170006F9 RID: 1785
      // (get) Token: 0x06002A23 RID: 10787 RVA: 0x0064E5C8 File Offset: 0x0064E5C8
      // (set) Token: 0x06002A24 RID: 10788 RVA: 0x0064E5D0 File Offset: 0x0064E5D0
      public Color Link { get; set; }

      // Token: 0x170006FA RID: 1786
      // (get) Token: 0x06002A25 RID: 10789 RVA: 0x0064E5DC File Offset: 0x0064E5DC
      // (set) Token: 0x06002A26 RID: 10790 RVA: 0x0064E5E4 File Offset: 0x0064E5E4
      public Color Secondary { get; set; }

      // Token: 0x170006FB RID: 1787
      // (get) Token: 0x06002A27 RID: 10791 RVA: 0x0064E5F0 File Offset: 0x0064E5F0
      // (set) Token: 0x06002A28 RID: 10792 RVA: 0x0064E5F8 File Offset: 0x0064E5F8
      public double BlockSpacing { get; set; }

      // Token: 0x170006FC RID: 1788
      // (get) Token: 0x06002A29 RID: 10793 RVA: 0x0064E604 File Offset: 0x0064E604
      // (set) Token: 0x06002A2A RID: 10794 RVA: 0x0064E60C File Offset: 0x0064E60C
      public double[] HeadingSizes { get; set; }

      // Token: 0x170006FD RID: 1789
      // (get) Token: 0x06002A2B RID: 10795 RVA: 0x0064E618 File Offset: 0x0064E618
      // (set) Token: 0x06002A2C RID: 10796 RVA: 0x0064E620 File Offset: 0x0064E620
      public double LineSpacing { get; set; }

      // Token: 0x170006FE RID: 1790
      // (get) Token: 0x06002A2D RID: 10797 RVA: 0x0064E62C File Offset: 0x0064E62C
      // (set) Token: 0x06002A2E RID: 10798 RVA: 0x0064E634 File Offset: 0x0064E634
      public double ListRowSpacing { get; set; }

      // Token: 0x170006FF RID: 1791
      // (get) Token: 0x06002A2F RID: 10799 RVA: 0x0064E640 File Offset: 0x0064E640
      // (set) Token: 0x06002A30 RID: 10800 RVA: 0x0064E648 File Offset: 0x0064E648
      public double BulletIndent { get; set; }

      // Token: 0x17000700 RID: 1792
      // (get) Token: 0x06002A31 RID: 10801 RVA: 0x0064E654 File Offset: 0x0064E654
      // (set) Token: 0x06002A32 RID: 10802 RVA: 0x0064E65C File Offset: 0x0064E65C
      public double NumberIndent { get; set; }

      // Token: 0x17000701 RID: 1793
      // (get) Token: 0x06002A33 RID: 10803 RVA: 0x0064E668 File Offset: 0x0064E668
      // (set) Token: 0x06002A34 RID: 10804 RVA: 0x0064E670 File Offset: 0x0064E670
      public Color CodeFill { get; set; }

      // Token: 0x17000702 RID: 1794
      // (get) Token: 0x06002A35 RID: 10805 RVA: 0x0064E67C File Offset: 0x0064E67C
      // (set) Token: 0x06002A36 RID: 10806 RVA: 0x0064E684 File Offset: 0x0064E684
      public Color CodeStroke { get; set; }

      // Token: 0x17000703 RID: 1795
      // (get) Token: 0x06002A37 RID: 10807 RVA: 0x0064E690 File Offset: 0x0064E690
      // (set) Token: 0x06002A38 RID: 10808 RVA: 0x0064E698 File Offset: 0x0064E698
      public Thickness CodePadding { get; set; }

      // Token: 0x17000704 RID: 1796
      // (get) Token: 0x06002A39 RID: 10809 RVA: 0x0064E6A4 File Offset: 0x0064E6A4
      // (set) Token: 0x06002A3A RID: 10810 RVA: 0x0064E6AC File Offset: 0x0064E6AC
      public bool CodeCopyButton { get; set; }

      // Token: 0x17000705 RID: 1797
      // (get) Token: 0x06002A3B RID: 10811 RVA: 0x0064E6B8 File Offset: 0x0064E6B8
      // (set) Token: 0x06002A3C RID: 10812 RVA: 0x0064E6C0 File Offset: 0x0064E6C0
      public FontWeight StrongWeight { get; set; }

      // Token: 0x06002A3D RID: 10813 RVA: 0x0064E6CC File Offset: 0x0064E6CC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Options");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002A3E RID: 10814 RVA: 0x0064E718 File Offset: 0x0064E718
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("FontSize = ");
        builder.Append(this.FontSize.ToString());
        builder.Append(", Text = ");
        builder.Append(this.Text.ToString());
        builder.Append(", Link = ");
        builder.Append(this.Link.ToString());
        builder.Append(", Secondary = ");
        builder.Append(this.Secondary.ToString());
        builder.Append(", BlockSpacing = ");
        builder.Append(this.BlockSpacing.ToString());
        builder.Append(", HeadingSizes = ");
        builder.Append(this.HeadingSizes);
        builder.Append(", LineSpacing = ");
        builder.Append(this.LineSpacing.ToString());
        builder.Append(", ListRowSpacing = ");
        builder.Append(this.ListRowSpacing.ToString());
        builder.Append(", BulletIndent = ");
        builder.Append(this.BulletIndent.ToString());
        builder.Append(", NumberIndent = ");
        builder.Append(this.NumberIndent.ToString());
        builder.Append(", CodeFill = ");
        builder.Append(this.CodeFill.ToString());
        builder.Append(", CodeStroke = ");
        builder.Append(this.CodeStroke.ToString());
        builder.Append(", CodePadding = ");
        builder.Append(this.CodePadding.ToString());
        builder.Append(", CodeCopyButton = ");
        builder.Append(this.CodeCopyButton.ToString());
        builder.Append(", StrongWeight = ");
        builder.Append(this.StrongWeight.ToString());
        return true;
      }

      // Token: 0x06002A3F RID: 10815 RVA: 0x0064E968 File Offset: 0x0064E968
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(Markdown.Options left, Markdown.Options right)
      {
        return !(left == right);
      }

      // Token: 0x06002A40 RID: 10816 RVA: 0x0064E974 File Offset: 0x0064E974
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(Markdown.Options left, Markdown.Options right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002A41 RID: 10817 RVA: 0x0064E988 File Offset: 0x0064E988
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<FontSize>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Link>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Secondary>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<BlockSpacing>k__BackingField)) * -1521134295 + EqualityComparer<double[]>.Default.GetHashCode(this.<HeadingSizes>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<LineSpacing>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<ListRowSpacing>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<BulletIndent>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<NumberIndent>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<CodeFill>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<CodeStroke>k__BackingField)) * -1521134295 + EqualityComparer<Thickness>.Default.GetHashCode(this.<CodePadding>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<CodeCopyButton>k__BackingField)) * -1521134295 + EqualityComparer<FontWeight>.Default.GetHashCode(this.<StrongWeight>k__BackingField);
      }

      // Token: 0x06002A42 RID: 10818 RVA: 0x0064EB00 File Offset: 0x0064EB00
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as Markdown.Options);
      }

      // Token: 0x06002A43 RID: 10819 RVA: 0x0064EB10 File Offset: 0x0064EB10
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(Markdown.Options other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<double>.Default.Equals(this.<FontSize>k__BackingField, other.<FontSize>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Link>k__BackingField, other.<Link>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Secondary>k__BackingField, other.<Secondary>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<BlockSpacing>k__BackingField, other.<BlockSpacing>k__BackingField) && EqualityComparer<double[]>.Default.Equals(this.<HeadingSizes>k__BackingField, other.<HeadingSizes>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<LineSpacing>k__BackingField, other.<LineSpacing>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<ListRowSpacing>k__BackingField, other.<ListRowSpacing>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<BulletIndent>k__BackingField, other.<BulletIndent>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<NumberIndent>k__BackingField, other.<NumberIndent>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<CodeFill>k__BackingField, other.<CodeFill>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<CodeStroke>k__BackingField, other.<CodeStroke>k__BackingField) && EqualityComparer<Thickness>.Default.Equals(this.<CodePadding>k__BackingField, other.<CodePadding>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<CodeCopyButton>k__BackingField, other.<CodeCopyButton>k__BackingField) && EqualityComparer<FontWeight>.Default.Equals(this.<StrongWeight>k__BackingField, other.<StrongWeight>k__BackingField));
      }

      // Token: 0x06002A45 RID: 10821 RVA: 0x0064ECD0 File Offset: 0x0064ECD0
      [CompilerGenerated]
      private Options(Markdown.Options original)
      {
        this.FontSize = original.<FontSize>k__BackingField;
        this.Text = original.<Text>k__BackingField;
        this.Link = original.<Link>k__BackingField;
        this.Secondary = original.<Secondary>k__BackingField;
        this.BlockSpacing = original.<BlockSpacing>k__BackingField;
        this.HeadingSizes = original.<HeadingSizes>k__BackingField;
        this.LineSpacing = original.<LineSpacing>k__BackingField;
        this.ListRowSpacing = original.<ListRowSpacing>k__BackingField;
        this.BulletIndent = original.<BulletIndent>k__BackingField;
        this.NumberIndent = original.<NumberIndent>k__BackingField;
        this.CodeFill = original.<CodeFill>k__BackingField;
        this.CodeStroke = original.<CodeStroke>k__BackingField;
        this.CodePadding = original.<CodePadding>k__BackingField;
        this.CodeCopyButton = original.<CodeCopyButton>k__BackingField;
        this.StrongWeight = original.<StrongWeight>k__BackingField;
      }

      // Token: 0x06002A46 RID: 10822 RVA: 0x0064ED98 File Offset: 0x0064ED98
      [CompilerGenerated]
      public void Deconstruct(out double FontSize, out Color Text, out Color Link, out Color Secondary)
      {
        FontSize = this.FontSize;
        Text = this.Text;
        Link = this.Link;
        Secondary = this.Secondary;
      }
    }
  }
}
