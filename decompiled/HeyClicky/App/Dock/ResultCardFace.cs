using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.Ink;
using HeyClicky.App.UI;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000339 RID: 825
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ResultCardFace : Grid
  {
    // Token: 0x0600231E RID: 8990 RVA: 0x0062AF80 File Offset: 0x0062AF80
    public ResultCardFace(string title, ClickyAnswer answer, Color ink, Color wash, [Nullable(2)] string whenLine, bool isDraftCopied, [Nullable(2)] string sentChoice, Action<ClickyAnswer.Choice> onChoose, Action<string> onOpen)
    {
      this.title = title;
      this.answer = answer;
      this.ink = ink;
      this.wash = wash;
      this.whenLine = whenLine;
      this.isDraftCopied = isDraftCopied;
      this.sentChoice = sentChoice;
      this.onChoose = onChoose;
      this.onOpen = onOpen;
      base.Padding = new Thickness(18.0, 12.0, 18.0, 8.0);
      base.Children.Add(this.pages);
      this.Build();
    }

    // Token: 0x0600231F RID: 8991 RVA: 0x0062B02C File Offset: 0x0062B02C
    [NullableContext(2)]
    public void Update(bool draftCopied, string sent)
    {
      if (draftCopied == this.isDraftCopied && sent == this.sentChoice)
      {
        return;
      }
      this.isDraftCopied = draftCopied;
      this.sentChoice = sent;
      this.Build();
    }

    // Token: 0x06002320 RID: 8992 RVA: 0x0062B05C File Offset: 0x0062B05C
    private void Build()
    {
      this.pages.Children.Clear();
      foreach (ResultCardLayouts.Arrangement arrangement in ResultCardLayouts.Layouts(this.answer))
      {
        this.pages.Children.Add(this.Page(arrangement));
      }
      if (this.pages.Children.Count == 0)
      {
        this.pages.Children.Add(this.Page(new ResultCardLayouts.Arrangement(Array.Empty<ClickyAnswer.Block>(), false, false)));
      }
    }

    // Token: 0x06002321 RID: 8993 RVA: 0x0062B104 File Offset: 0x0062B104
    private FrameworkElement Page(ResultCardLayouts.Arrangement layout)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 8.0,
        HorizontalAlignment = HorizontalAlignment.Stretch,
        VerticalAlignment = VerticalAlignment.Top
      };
      TextBlock textBlock = Ui.Text(this.title, 22.0, FontWeights.Bold, this.ink, TextAlignment.Left, 2, false, false);
      textBlock.FontFamily = DS.Fonts.Display;
      textBlock.CharacterSpacing = -14;
      stackPanel.Children.Add(Ui.Shrink(textBlock, HorizontalAlignment.Left, 0.8));
      stackPanel.Children.Add(this.Outcome());
      foreach (ClickyAnswer.Block block in layout.Blocks)
      {
        FrameworkElement frameworkElement = this.BlockView(block, layout.IsCompact);
        if (frameworkElement != null)
        {
          stackPanel.Children.Add(frameworkElement);
        }
      }
      if (this.answer.IsQuiet || this.answer.NumbersLook.GetValueOrDefault() == ClickyAnswer.NumbersLookKind.Changes)
      {
        string text = this.whenLine;
        if (text != null)
        {
          TextBlock textBlock2 = Ui.Text(text, 12.0, FontWeights.Normal, this.ink.WithAlpha(0.5), TextAlignment.Left, 0, true, false);
          textBlock2.Margin = new Thickness(4.0, 0.0, 4.0, 0.0);
          stackPanel.Children.Add(textBlock2);
        }
      }
      if (layout.ShowsNote)
      {
        string note = this.answer.Note;
        if (note != null)
        {
          stackPanel.Children.Add(ResultCardFace.Sticky(note));
        }
      }
      return stackPanel;
    }

    // Token: 0x06002322 RID: 8994 RVA: 0x0062B2BC File Offset: 0x0062B2BC
    private FrameworkElement Outcome()
    {
      TextBlock textBlock = Ui.Text(this.answer.Line, 14.5, this.answer.IsQuiet ? FontWeights.Medium : FontWeights.SemiBold, this.ink.WithAlpha(this.answer.IsQuiet ? 0.75 : 1.0), TextAlignment.Left, 0, true, false);
      textBlock.LineHeight = 21.285;
      return new Border
      {
        CornerRadius = new CornerRadius(11.0),
        Padding = new Thickness(12.0, 9.0, 12.0, 9.0),
        Background = Ui.Brush(this.answer.IsQuiet ? this.ink.WithAlpha(0.06) : this.wash.WithAlpha(0.72)),
        Child = textBlock
      };
    }

    // Token: 0x06002323 RID: 8995 RVA: 0x0062B3CC File Offset: 0x0062B3CC
    [NullableContext(2)]
    private FrameworkElement BlockView(ClickyAnswer.Block block, bool isCompact)
    {
      switch (block)
      {
      case ClickyAnswer.Block.Numbers:
      {
        IReadOnlyList<ClickyAnswer.Number> numbers = this.answer.Numbers;
        if (numbers != null)
        {
          ClickyAnswer.NumbersLookKind? numbersLook = this.answer.NumbersLook;
          if (numbersLook != null)
          {
            ClickyAnswer.NumbersLookKind valueOrDefault = numbersLook.GetValueOrDefault();
            return this.Numbers(numbers, valueOrDefault, isCompact);
          }
        }
        break;
      }
      case ClickyAnswer.Block.List:
      {
        IReadOnlyList<ClickyAnswer.Row> list = this.answer.List;
        if (list != null)
        {
          return this.List(list, this.answer.Ranked.GetValueOrDefault());
        }
        break;
      }
      case ClickyAnswer.Block.Done:
      {
        IReadOnlyList<ClickyAnswer.DoneRow> done = this.answer.Done;
        if (done != null)
        {
          return this.Receipt(done);
        }
        break;
      }
      case ClickyAnswer.Block.Draft:
      {
        ClickyAnswer.Draft draftText = this.answer.DraftText;
        if (draftText != null)
        {
          return this.DraftSlip(draftText);
        }
        break;
      }
      case ClickyAnswer.Block.Ask:
      {
        IReadOnlyList<ClickyAnswer.Choice> ask = this.answer.Ask;
        if (ask != null)
        {
          return this.Choices(ask, isCompact);
        }
        break;
      }
      }
      return null;
    }

    // Token: 0x06002324 RID: 8996 RVA: 0x0062B4C8 File Offset: 0x0062B4C8
    private FrameworkElement Numbers(IReadOnlyList<ClickyAnswer.Number> numbers, ClickyAnswer.NumbersLookKind look, bool isCompact)
    {
      Color color = this.ink.WithAlpha(0.55);
      switch (look)
      {
      case ClickyAnswer.NumbersLookKind.Hero:
      {
        ClickyAnswer.Number number = numbers[0];
        TextBlock textBlock = ResultCardFace.Rounded(number.Value, (double)(isCompact ? 32 : 46), FontWeights.ExtraBold, this.ink);
        textBlock.CharacterSpacing = -20;
        StackPanel stackPanel = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 10.0,
          Margin = new Thickness(4.0, 0.0, 4.0, 0.0)
        };
        stackPanel.Children.Add(Ui.Shrink(textBlock, HorizontalAlignment.Left, 0.6));
        string label = number.Label;
        if (label != null)
        {
          TextBlock textBlock2 = Ui.Text(label, 14.0, FontWeights.SemiBold, this.ink.WithAlpha(0.6), TextAlignment.Left, 0, true, false);
          textBlock2.VerticalAlignment = VerticalAlignment.Bottom;
          textBlock2.Margin = new Thickness(0.0, 0.0, 0.0, (double)(isCompact ? 5 : 9));
          stackPanel.Children.Add(textBlock2);
        }
        return stackPanel;
      }
      case ClickyAnswer.NumbersLookKind.Tiles:
      {
        Grid grid = new Grid
        {
          Margin = new Thickness(4.0, (double)(isCompact ? 6 : 8), 4.0, (double)(isCompact ? 6 : 8))
        };
        Grid grid2 = new Grid();
        for (int i = 0; i < numbers.Count; i++)
        {
          if (i > 0)
          {
            grid2.ColumnDefinitions.Add(new ColumnDefinition
            {
              Width = new GridLength(1.5)
            });
          }
          grid2.ColumnDefinitions.Add(new ColumnDefinition
          {
            Width = new GridLength(1.0, GridUnitType.Star)
          });
        }
        int num = 0;
        for (int j = 0; j < numbers.Count; j++)
        {
          if (j > 0)
          {
            Rectangle rectangle = new Rectangle
            {
              Fill = Ui.Brush(color)
            };
            Grid.SetColumn(rectangle, num++);
            grid2.Children.Add(rectangle);
          }
          TextBlock textBlock3 = Ui.Text((numbers[j].Label ?? "").ToUpperInvariant(), 9.5, FontWeights.Bold, this.ink.WithAlpha(0.55), TextAlignment.Left, 0, false, false);
          textBlock3.CharacterSpacing = 89;
          TextBlock textBlock4 = ResultCardFace.Rounded(numbers[j].Value, 24.0, FontWeights.ExtraBold, this.ink);
          textBlock4.CharacterSpacing = -10;
          StackPanel stackPanel2 = new StackPanel
          {
            Spacing = 1.0,
            Padding = new Thickness(12.0, (double)(isCompact ? 4 : 7), 12.0, (double)(isCompact ? 5 : 8))
          };
          stackPanel2.Children.Add(Ui.Shrink(textBlock3, HorizontalAlignment.Left, 0.8));
          stackPanel2.Children.Add(Ui.Shrink(textBlock4, HorizontalAlignment.Left, 0.6));
          Grid.SetColumn(stackPanel2, num++);
          grid2.Children.Add(stackPanel2);
        }
        grid.Children.Add(grid2);
        grid.Children.Add(new Rectangle
        {
          Height = 1.5,
          Fill = Ui.Brush(color),
          VerticalAlignment = VerticalAlignment.Top
        });
        grid.Children.Add(new Rectangle
        {
          Height = 1.5,
          Fill = Ui.Brush(color),
          VerticalAlignment = VerticalAlignment.Bottom
        });
        grid.Children.Add(new Rectangle
        {
          Width = 1.5,
          Fill = Ui.Brush(color),
          HorizontalAlignment = HorizontalAlignment.Left,
          Margin = new Thickness(0.0, -6.0, 0.0, -6.0)
        });
        grid.Children.Add(new Rectangle
        {
          Width = 1.5,
          Fill = Ui.Brush(color),
          HorizontalAlignment = HorizontalAlignment.Right,
          Margin = new Thickness(0.0, -6.0, 0.0, -6.0)
        });
        return grid;
      }
      case ClickyAnswer.NumbersLookKind.Changes:
      {
        Grid grid3 = new Grid
        {
          ColumnSpacing = 8.0,
          RowSpacing = 6.0,
          Margin = new Thickness(4.0, 0.0, 4.0, 0.0)
        };
        for (int k = 0; k < 5; k++)
        {
          grid3.ColumnDefinitions.Add(new ColumnDefinition
          {
            Width = GridLength.Auto
          });
        }
        for (int l = 0; l < numbers.Count; l++)
        {
          grid3.RowDefinitions.Add(new RowDefinition
          {
            Height = GridLength.Auto
          });
          ClickyAnswer.Number number2 = numbers[l];
          ResultCardLayouts.MoveKind moveKind = ResultCardLayouts.Move(number2);
          Color color2 = DS.Rgb(0.17, 0.15, 0.13, 1.0);
          ValueTuple<string, Color, Color, Color> valueTuple;
          switch (moveKind)
          {
          case ResultCardLayouts.MoveKind.Same:
            valueTuple = new ValueTuple<string, Color, Color, Color>("same", color2.WithAlpha(0.4), color2.WithAlpha(0.07), color2.WithAlpha(0.55));
            break;
          case ResultCardLayouts.MoveKind.Better:
            valueTuple = new ValueTuple<string, Color, Color, Color>("better", ResultCardFace.Green, DS.Rgb(0.84, 0.94, 0.86, 1.0), DS.Rgb(0.08, 0.48, 0.22, 1.0));
            break;
          case ResultCardLayouts.MoveKind.Worse:
            valueTuple = new ValueTuple<string, Color, Color, Color>("worse", DS.Rgb(0.76, 0.25, 0.05, 1.0), DS.Rgb(0.99, 0.89, 0.83, 1.0), DS.Rgb(0.71, 0.25, 0.05, 1.0));
            break;
          case ResultCardLayouts.MoveKind.Plain:
            valueTuple = new ValueTuple<string, Color, Color, Color>(null, color2.WithAlpha(0.55), Colors.Transparent, Colors.Transparent);
            break;
          default:
            valueTuple = new ValueTuple<string, Color, Color, Color>(null, Colors.Transparent, Colors.Transparent, Colors.Transparent);
            break;
          }
          ValueTuple<string, Color, Color, Color> valueTuple2 = valueTuple;
          TextBlock textBlock5 = Ui.Text(number2.Label ?? "", 13.0, FontWeights.Normal, this.ink.WithAlpha(0.6), TextAlignment.Left, 0, false, false);
          textBlock5.MaxWidth = 130.0;
          ResultCardFace.Place(grid3, textBlock5, l, 0);
          string was = number2.Was;
          if (was != null)
          {
            TextBlock textBlock6 = Ui.Text(was, 16.0, FontWeights.SemiBold, this.ink.WithAlpha(0.45), TextAlignment.Left, 0, false, false);
            textBlock6.TextDecorations = TextDecorations.Strikethrough;
            ResultCardFace.Place(grid3, textBlock6, l, 1);
            ResultCardFace.Place(grid3, Ui.Text("→", 13.0, FontWeights.Bold, valueTuple2.Item2, TextAlignment.Left, 0, false, false), l, 2);
          }
          ResultCardFace.Place(grid3, ResultCardFace.Rounded(number2.Value, 22.0, FontWeights.ExtraBold, this.ink), l, 3);
          string item = valueTuple2.Item1;
          if (item != null)
          {
            Border border = new Border
            {
              Background = Ui.Brush(valueTuple2.Item3),
              Padding = new Thickness(7.0, 2.0, 7.0, 2.0),
              VerticalAlignment = VerticalAlignment.Center,
              HorizontalAlignment = HorizontalAlignment.Left,
              Child = Ui.Text(item, 11.5, FontWeights.Bold, valueTuple2.Item4, TextAlignment.Left, 0, false, false)
            }.Capsule();
            ResultCardFace.Place(grid3, border, l, 4);
          }
        }
        return grid3;
      }
      default:
      {
        StackPanel stackPanel3 = new StackPanel
        {
          Spacing = 10.0,
          Margin = new Thickness(4.0, 2.0, 4.0, 0.0)
        };
        foreach (ClickyAnswer.Number number3 in numbers)
        {
          StackPanel stackPanel4 = new StackPanel
          {
            Spacing = 6.0
          };
          StackPanel stackPanel5 = new StackPanel
          {
            Orientation = Orientation.Horizontal,
            Spacing = 8.0
          };
          stackPanel5.Children.Add(ResultCardFace.Rounded(number3.Value, 24.0, FontWeights.ExtraBold, this.ink));
          string text = " ";
          string[] array = new string[2];
          int num2 = 0;
          string of = number3.Of;
          array[num2] = ((of != null) ? ("of " + of) : null);
          array[1] = number3.Label;
          TextBlock textBlock7 = Ui.Text(string.Join(text, array.OfType<string>()), 13.0, FontWeights.Normal, this.ink.WithAlpha(0.6), TextAlignment.Left, 0, false, false);
          textBlock7.VerticalAlignment = VerticalAlignment.Bottom;
          textBlock7.Margin = new Thickness(0.0, 0.0, 0.0, 4.0);
          stackPanel5.Children.Add(textBlock7);
          stackPanel4.Children.Add(stackPanel5);
          double? num3 = ResultCardLayouts.Fraction(number3);
          if (num3 != null)
          {
            double fraction = num3.GetValueOrDefault();
            Grid grid4 = new Grid
            {
              Height = 8.0
            };
            grid4.Children.Add(new Border
            {
              Background = Ui.Brush(this.ink.WithAlpha(0.12))
            }.Capsule());
            Border fill = new Border
            {
              Background = Ui.Brush(ResultCardFace.Green),
              HorizontalAlignment = HorizontalAlignment.Left
            }.Capsule();
            grid4.SizeChanged += delegate(object _, SizeChangedEventArgs e)
            {
              fill.Width = Math.Max(8.0, e.NewSize.Width * fraction);
            };
            grid4.Children.Add(fill);
            stackPanel4.Children.Add(grid4);
          }
          stackPanel3.Children.Add(stackPanel4);
        }
        return stackPanel3;
      }
      }
    }

    // Token: 0x06002325 RID: 8997 RVA: 0x0062C014 File Offset: 0x0062C014
    private static void Place(Grid grid, FrameworkElement e, int row, int column)
    {
      e.VerticalAlignment = VerticalAlignment.Center;
      Grid.SetRow(e, row);
      Grid.SetColumn(e, column);
      grid.Children.Add(e);
    }

    // Token: 0x06002326 RID: 8998 RVA: 0x0062C038 File Offset: 0x0062C038
    private static TextBlock Rounded(string text, double size, FontWeight weight, Color color)
    {
      TextBlock textBlock = Ui.Text(text, size, weight, color, TextAlignment.Left, 0, false, false);
      textBlock.FontFamily = DS.Fonts.Display;
      return textBlock;
    }

    // Token: 0x06002327 RID: 8999 RVA: 0x0062C054 File Offset: 0x0062C054
    private FrameworkElement List(IReadOnlyList<ClickyAnswer.Row> rows, bool isRanked)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 5.0,
        Margin = new Thickness(4.0, 0.0, 4.0, 0.0)
      };
      for (int i = 0; i < rows.Count; i++)
      {
        Grid grid = new Grid
        {
          ColumnSpacing = 9.0
        };
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = new GridLength(1.0, GridUnitType.Star)
        });
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        FrameworkElement frameworkElement;
        if (!isRanked)
        {
          frameworkElement = Ui.Text("–", 13.5, FontWeights.Normal, this.ink.WithAlpha(0.45), TextAlignment.Left, 0, false, false);
        }
        else
        {
          Border border = new Border();
          border.Width = 16.0;
          border.Height = 16.0;
          border.CornerRadius = new CornerRadius(8.0);
          border.Background = Ui.Brush(this.ink.WithAlpha(0.1));
          border.VerticalAlignment = VerticalAlignment.Top;
          border.Margin = new Thickness(0.0, 1.0, 0.0, 0.0);
          frameworkElement = border;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
          defaultInterpolatedStringHandler.AppendFormatted<int>(i + 1);
          border.Child = Ui.Text(defaultInterpolatedStringHandler.ToStringAndClear(), 11.0, FontWeights.Bold, this.ink.WithAlpha(0.85), TextAlignment.Center, 0, false, false);
        }
        FrameworkElement frameworkElement2 = frameworkElement;
        grid.Children.Add(frameworkElement2);
        TextBlock textBlock = Ui.Text(rows[i].Text, 13.5, FontWeights.Normal, this.ink, TextAlignment.Left, 0, true, false);
        Grid.SetColumn(textBlock, 1);
        grid.Children.Add(textBlock);
        string detail = rows[i].Detail;
        if (detail != null)
        {
          TextBlock textBlock2 = Ui.Text(detail, 12.5, FontWeights.Normal, this.ink.WithAlpha(0.55), TextAlignment.Left, 0, false, false);
          textBlock2.Margin = new Thickness(8.0, 1.0, 0.0, 0.0);
          Grid.SetColumn(textBlock2, 2);
          grid.Children.Add(textBlock2);
        }
        stackPanel.Children.Add(grid);
      }
      return stackPanel;
    }

    // Token: 0x06002328 RID: 9000 RVA: 0x0062C31C File Offset: 0x0062C31C
    private static FrameworkElement FileIcon(string path)
    {
      ResultCardFace.<>c__DisplayClass23_0 CS$<>8__locals1 = new ResultCardFace.<>c__DisplayClass23_0();
      CS$<>8__locals1.path = path;
      DockFileStyle dockFileStyle = DockFileStyle.For(CS$<>8__locals1.path);
      CS$<>8__locals1.host = new Border
      {
        Width = 16.0,
        Height = 16.0,
        Child = Icons.Glyph(dockFileStyle.Glyph, 14.0, dockFileStyle.Tint)
      };
      CS$<>8__locals1.<FileIcon>g__LoadAsync|0();
      return CS$<>8__locals1.host;
    }

    // Token: 0x06002329 RID: 9001 RVA: 0x0062C398 File Offset: 0x0062C398
    private FrameworkElement Receipt(IReadOnlyList<ClickyAnswer.DoneRow> rows)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 6.0,
        Margin = new Thickness(4.0, 0.0, 4.0, 0.0)
      };
      foreach (ClickyAnswer.DoneRow doneRow in rows)
      {
        Grid grid = new Grid
        {
          ColumnSpacing = 9.0
        };
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = new GridLength(1.0, GridUnitType.Star)
        });
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        TextBlock textBlock = Icons.Glyph("\ue73e", 8.5, Colors.White);
        textBlock.FontWeight = FontWeights.ExtraBold;
        grid.Children.Add(new Border
        {
          Width = 16.0,
          Height = 16.0,
          CornerRadius = new CornerRadius(4.0),
          Background = Ui.Brush(ResultCardFace.Green),
          Child = textBlock,
          VerticalAlignment = VerticalAlignment.Center
        });
        TextBlock textBlock2 = new TextBlock
        {
          FontSize = 13.5,
          FontFamily = DS.Fonts.For(13.5),
          TextWrapping = TextWrapping.WrapWholeWords,
          IsTextScaleFactorEnabled = false,
          VerticalAlignment = VerticalAlignment.Center
        };
        textBlock2.Inlines.Add(new Run
        {
          Text = doneRow.Did,
          Foreground = Ui.Brush(this.ink)
        });
        string what = doneRow.What;
        if (what != null)
        {
          textBlock2.Inlines.Add(new Run
          {
            Text = " " + what,
            Foreground = Ui.Brush(ResultCardFace.Blue),
            FontWeight = FontWeights.SemiBold
          });
        }
        Grid.SetColumn(textBlock2, 1);
        grid.Children.Add(textBlock2);
        string link = doneRow.Link;
        if (link != null)
        {
          FrameworkElement frameworkElement2;
          if (link.StartsWith("http", StringComparison.OrdinalIgnoreCase))
          {
            FrameworkElement frameworkElement = ArcSymbols.Make("arrow.up.right.square", 13.0, this.ink.WithAlpha(0.4), false);
            frameworkElement2 = ClickyInkObjectKit.Favicon(link, 16.0, new Grid
            {
              Width = 16.0,
              Height = 16.0,
              Children = { frameworkElement }
            });
            frameworkElement2.Width = (frameworkElement2.Height = 16.0);
            Ui.ClipRounded(frameworkElement2, 4.0);
          }
          else
          {
            frameworkElement2 = ResultCardFace.FileIcon(link);
          }
          frameworkElement2.VerticalAlignment = VerticalAlignment.Center;
          frameworkElement2.Margin = new Thickness(6.0, 0.0, 0.0, 0.0);
          Grid.SetColumn(frameworkElement2, 2);
          grid.Children.Add(frameworkElement2);
          Pressable pressable = new Pressable(true)
          {
            Children = { grid }
          };
          pressable.Click += delegate
          {
            this.onOpen(link);
          };
          ToolTipService.SetToolTip(pressable, link);
          stackPanel.Children.Add(pressable);
        }
        else
        {
          stackPanel.Children.Add(grid);
        }
      }
      return stackPanel;
    }

    // Token: 0x0600232A RID: 9002 RVA: 0x0062C780 File Offset: 0x0062C780
    private FrameworkElement DraftSlip(ClickyAnswer.Draft draft)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 4.0
      };
      string to = draft.To;
      if (to != null)
      {
        stackPanel.Children.Add(Ui.Text("To " + to, 10.5, FontWeights.SemiBold, DS.Black(0.45), TextAlignment.Left, 0, false, false));
      }
      TextBlock textBlock = Ui.Text(draft.Text, 12.0, FontWeights.Medium, DS.Rgb(0.17, 0.15, 0.13, 1.0), TextAlignment.Left, 3, false, true);
      textBlock.LineHeight = 18.6;
      stackPanel.Children.Add(textBlock);
      Grid slip = new Grid
      {
        Margin = new Thickness(4.0, 0.0, 4.0, 0.0),
        RenderTransform = new RotateTransform
        {
          Angle = -0.6
        }
      };
      slip.SizeChanged += delegate(object _, SizeChangedEventArgs e)
      {
        ((RotateTransform)slip.RenderTransform).CenterX = e.NewSize.Width / 2.0;
      };
      slip.Children.Add(new ShadowLayer(6.0, DS.Black(0.12), 6.0, 4.0, 0.0));
      slip.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(6.0),
        Background = Ui.Brush(Colors.White),
        BorderBrush = Ui.Brush(DS.Black(0.05)),
        BorderThickness = new Thickness(1.0),
        Padding = new Thickness(12.0, 10.0, 12.0, 10.0),
        Child = stackPanel
      });
      if (this.isDraftCopied)
      {
        Border border = new Border
        {
          CornerRadius = new CornerRadius(6.0),
          Padding = new Thickness(7.0, 3.0, 7.0, 3.0),
          Background = Ui.Brush(DS.Rgb(0.92, 0.94, 0.99, 1.0)),
          BorderBrush = Ui.Brush(ResultCardFace.Blue.WithAlpha(0.25)),
          BorderThickness = new Thickness(1.0),
          HorizontalAlignment = HorizontalAlignment.Right,
          VerticalAlignment = VerticalAlignment.Top,
          Margin = new Thickness(0.0, -10.0, 10.0, 0.0),
          Child = Ui.Text("Copied", 10.5, FontWeights.Bold, ResultCardFace.Blue, TextAlignment.Left, 0, false, false)
        };
        slip.Children.Add(border);
        if (!Anim.ReduceMotion)
        {
          border.Opacity = 0.0;
          border.Scale = new Vector3(0.9f, 0.9f, 1f);
          Anim.Opacity(border, 1f, 0.2, null, 0.0);
          Anim.Scale(border, 1f, 0.2, null, null);
        }
      }
      return slip;
    }

    // Token: 0x0600232B RID: 9003 RVA: 0x0062CB48 File Offset: 0x0062CB48
    private FrameworkElement Choices(IReadOnlyList<ClickyAnswer.Choice> choices, bool isCompact)
    {
      Panel panel;
      if (choices.All<ClickyAnswer.Choice>((ClickyAnswer.Choice c) => c.File != null))
      {
        Grid grid = new Grid
        {
          ColumnSpacing = 10.0
        };
        for (int i = 0; i < choices.Count; i++)
        {
          grid.ColumnDefinitions.Add(new ColumnDefinition
          {
            Width = new GridLength(1.0, GridUnitType.Star)
          });
          FrameworkElement frameworkElement = this.<Choices>g__Button|26_2(choices[i], ResultCardFace.FileChoice(choices[i], this.<Choices>g__IsSent|26_0(choices[i]), isCompact));
          Grid.SetColumn(frameworkElement, i);
          grid.Children.Add(frameworkElement);
        }
        panel = grid;
      }
      else if (isCompact)
      {
        DockFlow dockFlow = new DockFlow
        {
          HorizontalSpacing = 6.0,
          VerticalSpacing = 6.0
        };
        foreach (ClickyAnswer.Choice choice in choices)
        {
          StackPanel stackPanel = new StackPanel
          {
            Orientation = Orientation.Horizontal,
            Spacing = 5.0,
            VerticalAlignment = VerticalAlignment.Center
          };
          if (this.<Choices>g__IsSent|26_0(choice))
          {
            TextBlock textBlock = Icons.Glyph("\ue73e", 9.0, ResultCardFace.Blue);
            textBlock.FontWeight = FontWeights.ExtraBold;
            stackPanel.Children.Add(textBlock);
          }
          stackPanel.Children.Add(Ui.Text(choice.Answer ?? "", 13.0, FontWeights.SemiBold, this.ink, TextAlignment.Left, 0, false, false));
          Border border = new Border
          {
            Padding = new Thickness(11.0, 6.0, 11.0, 6.0),
            Background = Ui.Brush(DS.White(0.8)),
            BorderBrush = Ui.Brush(this.<Choices>g__IsSent|26_0(choice) ? ResultCardFace.Blue.WithAlpha(0.7) : this.ink.WithAlpha(0.16)),
            BorderThickness = new Thickness(this.<Choices>g__IsSent|26_0(choice) ? 1.5 : 1.0),
            Child = stackPanel
          }.Capsule();
          dockFlow.Children.Add(this.<Choices>g__Button|26_2(choice, border));
        }
        panel = dockFlow;
      }
      else
      {
        StackPanel stackPanel2 = new StackPanel
        {
          Spacing = 5.0
        };
        for (int j = 0; j < choices.Count; j++)
        {
          ClickyAnswer.Choice choice2 = choices[j];
          Grid grid2 = new Grid
          {
            ColumnSpacing = 10.0,
            Padding = new Thickness(11.0, 7.0, 11.0, 7.0)
          };
          grid2.ColumnDefinitions.Add(new ColumnDefinition
          {
            Width = GridLength.Auto
          });
          grid2.ColumnDefinitions.Add(new ColumnDefinition
          {
            Width = GridLength.Auto
          });
          grid2.ColumnDefinitions.Add(new ColumnDefinition
          {
            Width = new GridLength(1.0, GridUnitType.Star)
          });
          grid2.ColumnDefinitions.Add(new ColumnDefinition
          {
            Width = GridLength.Auto
          });
          FrameworkElement frameworkElement2;
          if (!this.<Choices>g__IsSent|26_0(choice2))
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
            defaultInterpolatedStringHandler.AppendFormatted<int>(j + 1);
            frameworkElement2 = Ui.Text(defaultInterpolatedStringHandler.ToStringAndClear(), 10.0, FontWeights.Bold, this.ink.WithAlpha(0.6), TextAlignment.Center, 0, false, true);
          }
          else
          {
            frameworkElement2 = Icons.Glyph("\ue73e", 9.0, Colors.White);
          }
          FrameworkElement frameworkElement3 = frameworkElement2;
          grid2.Children.Add(new Border
          {
            Width = 18.0,
            Height = 18.0,
            CornerRadius = new CornerRadius(5.0),
            VerticalAlignment = VerticalAlignment.Center,
            Background = Ui.Brush(this.<Choices>g__IsSent|26_0(choice2) ? ResultCardFace.Blue : this.ink.WithAlpha(0.08)),
            Child = frameworkElement3
          });
          TextBlock textBlock2 = Ui.Text(choice2.Answer ?? "", 13.5, FontWeights.SemiBold, this.ink, TextAlignment.Left, 0, false, false);
          textBlock2.VerticalAlignment = VerticalAlignment.Center;
          Grid.SetColumn(textBlock2, 1);
          grid2.Children.Add(textBlock2);
          string detail = choice2.Detail;
          if (detail != null)
          {
            TextBlock textBlock3 = Ui.Text(detail, 12.0, FontWeights.Normal, this.ink.WithAlpha(0.55), TextAlignment.Left, 0, false, false);
            textBlock3.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textBlock3, 3);
            grid2.Children.Add(textBlock3);
          }
          Grid grid3 = new Grid();
          grid3.Children.Add(new Border
          {
            CornerRadius = new CornerRadius(10.0),
            Background = Ui.Brush(DS.White(0.75)),
            BorderBrush = Ui.Brush(this.<Choices>g__IsSent|26_0(choice2) ? ResultCardFace.Blue.WithAlpha(0.7) : this.ink.WithAlpha(0.14)),
            BorderThickness = new Thickness(this.<Choices>g__IsSent|26_0(choice2) ? 1.5 : 1.0)
          });
          grid3.Children.Add(grid2);
          stackPanel2.Children.Add(this.<Choices>g__Button|26_2(choice2, grid3));
        }
        panel = stackPanel2;
      }
      panel.Margin = new Thickness(4.0, 0.0, 4.0, 0.0);
      return panel;
    }

    // Token: 0x0600232C RID: 9004 RVA: 0x0062D194 File Offset: 0x0062D194
    private static FrameworkElement FileChoice(ClickyAnswer.Choice choice, bool isSent, bool isCompact)
    {
      string text = choice.File ?? "";
      Grid grid = new Grid
      {
        Height = (double)(isCompact ? 34 : 56)
      };
      grid.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(4.0),
        Background = Ui.Brush(DS.Rgb(0.95, 0.95, 0.96, 1.0))
      });
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 4.0,
        Padding = new Thickness(7.0)
      };
      stackPanel.Children.Add(new Border
      {
        Width = 34.0,
        Height = 4.0,
        HorizontalAlignment = HorizontalAlignment.Left,
        Background = Ui.Brush(DS.Rgb(0.11, 0.12, 0.14, 1.0))
      }.Capsule());
      for (int i = 0; i < 3; i++)
      {
        stackPanel.Children.Add(new Border
        {
          Height = 3.0,
          MaxWidth = ((i == 1) ? 50.0 : double.PositiveInfinity),
          HorizontalAlignment = HorizontalAlignment.Stretch,
          Background = Ui.Brush(DS.Rgb(0.85, 0.86, 0.88, 1.0))
        }.Capsule());
      }
      grid.Children.Add(stackPanel);
      Ui.ClipRounded(grid, 4.0);
      ResultCardFace.LoadThumbnailAsync(grid, stackPanel, text);
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 4.0,
        Padding = new Thickness(7.0)
      };
      stackPanel2.Children.Add(grid);
      TextBlock textBlock = Ui.Text(choice.Answer ?? Path.GetFileName(text), 9.5, FontWeights.Bold, ResultCardFace.Ink, TextAlignment.Left, 0, false, false);
      textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
      stackPanel2.Children.Add(textBlock);
      stackPanel2.Children.Add(Ui.Text(choice.Detail ?? ResultCardFace.FileDetail(text), 9.0, FontWeights.Normal, DS.Rgb(0.54, 0.56, 0.62, 1.0), TextAlignment.Left, 0, false, false));
      return new Grid
      {
        Children = 
        {
          new ShadowLayer(7.0, DS.Black(0.14), 4.5, 3.0, 0.0),
          new Border
          {
            CornerRadius = new CornerRadius(7.0),
            Background = Ui.Brush(Colors.White),
            BorderBrush = Ui.Brush(isSent ? ResultCardFace.Blue.WithAlpha(0.8) : DS.Black(0.05)),
            BorderThickness = new Thickness((double)(isSent ? 2 : 1))
          },
          stackPanel2
        }
      };
    }

    // Token: 0x0600232D RID: 9005 RVA: 0x0062D50C File Offset: 0x0062D50C
    private static async Task LoadThumbnailAsync(Grid picture, FrameworkElement placeholder, string path)
    {
      if (File.Exists(path))
      {
        try
        {
          ImageSource imageSource = await ArtifactThumbnails.GetAsync(path, 360);
          if (imageSource != null)
          {
            picture.Children.Remove(placeholder);
            picture.Children.Add(new Image
            {
              Source = imageSource,
              Stretch = Stretch.UniformToFill,
              VerticalAlignment = VerticalAlignment.Top
            });
          }
        }
        catch (Exception ex)
        {
          Log.Warn("dock", "answer file picture: " + ex.Message);
        }
      }
    }

    // Token: 0x0600232E RID: 9006 RVA: 0x0062D560 File Offset: 0x0062D560
    public static string FileDetail(string path)
    {
      string text;
      try
      {
        FileInfo fileInfo = new FileInfo(path);
        if (!fileInfo.Exists)
        {
          text = "";
        }
        else
        {
          text = fileInfo.LastWriteTime.ToString("MMM d", CultureInfo.CurrentCulture) + " · " + DockFiles.FormatBytes(fileInfo.Length);
        }
      }
      catch
      {
        text = "";
      }
      return text;
    }

    // Token: 0x0600232F RID: 9007 RVA: 0x0062D5D0 File Offset: 0x0062D5D0
    private static FrameworkElement Sticky(string text)
    {
      TextBlock textBlock = Ui.Text(text, 12.5, FontWeights.Normal, DS.Rgb(0.23, 0.2, 0.0, 1.0), TextAlignment.Left, 0, true, false);
      textBlock.LineHeight = 17.75;
      Grid grid = new Grid();
      grid.MaxWidth = 300.0;
      grid.HorizontalAlignment = HorizontalAlignment.Left;
      grid.Margin = new Thickness(4.0, 2.0, 4.0, 0.0);
      grid.Children.Add(new ShadowLayer(0.0, DS.Black(0.12), 4.0, 3.0, 0.0));
      grid.Children.Add(new Border
      {
        Background = Ui.Brush(DS.Rgb(1.0, 0.957, 0.69, 1.0)),
        Padding = new Thickness(11.0, 7.0, 11.0, 7.0),
        Child = textBlock
      });
      RotateTransform rotate = new RotateTransform
      {
        Angle = -1.0
      };
      grid.RenderTransform = rotate;
      grid.SizeChanged += delegate(object _, SizeChangedEventArgs e)
      {
        rotate.CenterX = e.NewSize.Width / 2.0;
        rotate.CenterY = e.NewSize.Height / 2.0;
      };
      return grid;
    }

    // Token: 0x06002331 RID: 9009 RVA: 0x0062D80C File Offset: 0x0062D80C
    [CompilerGenerated]
    private bool <Choices>g__IsSent|26_0(ClickyAnswer.Choice c)
    {
      return this.sentChoice == c.Reply;
    }

    // Token: 0x06002332 RID: 9010 RVA: 0x0062D820 File Offset: 0x0062D820
    [CompilerGenerated]
    private bool <Choices>g__IsDimmed|26_1(ClickyAnswer.Choice c)
    {
      return this.sentChoice != null && !this.<Choices>g__IsSent|26_0(c);
    }

    // Token: 0x06002333 RID: 9011 RVA: 0x0062D838 File Offset: 0x0062D838
    [CompilerGenerated]
    private FrameworkElement <Choices>g__Button|26_2(ClickyAnswer.Choice choice, FrameworkElement label)
    {
      Pressable pressable = new Pressable(true)
      {
        Children = { label }
      };
      pressable.Enabled = this.sentChoice == null;
      pressable.WithPressScale(0.97f, 0.12, true);
      pressable.Click += delegate
      {
        if (this.sentChoice == null)
        {
          this.onChoose(choice);
        }
      };
      string file = choice.File;
      if (file != null)
      {
        ToolTipService.SetToolTip(pressable, file);
      }
      pressable.Opacity = (this.<Choices>g__IsDimmed|26_1(choice) ? 0.45 : 1.0);
      return pressable;
    }

    // Token: 0x04001124 RID: 4388
    public static readonly Color Ink = DS.Rgb(0.11, 0.12, 0.14, 1.0);

    // Token: 0x04001125 RID: 4389
    public static readonly Color Green = DS.Rgb(0.12, 0.62, 0.29, 1.0);

    // Token: 0x04001126 RID: 4390
    public static readonly Color Blue = DS.Rgb(0.18, 0.36, 0.89, 1.0);

    // Token: 0x04001127 RID: 4391
    private readonly string title;

    // Token: 0x04001128 RID: 4392
    private readonly ClickyAnswer answer;

    // Token: 0x04001129 RID: 4393
    private readonly Color ink;

    // Token: 0x0400112A RID: 4394
    private readonly Color wash;

    // Token: 0x0400112B RID: 4395
    [Nullable(2)]
    private readonly string whenLine;

    // Token: 0x0400112C RID: 4396
    private readonly Action<ClickyAnswer.Choice> onChoose;

    // Token: 0x0400112D RID: 4397
    private readonly Action<string> onOpen;

    // Token: 0x0400112E RID: 4398
    private bool isDraftCopied;

    // Token: 0x0400112F RID: 4399
    [Nullable(2)]
    private string sentChoice;

    // Token: 0x04001130 RID: 4400
    private readonly FirstThatFits pages = new FirstThatFits();
  }
}
