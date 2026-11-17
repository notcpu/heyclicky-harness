using System;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.Ink;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200017C RID: 380
  public sealed class ChatDrawingRow : StackPanel
  {
    // Token: 0x060014B5 RID: 5301 RVA: 0x005B00B0 File Offset: 0x005B00B0
    [NullableContext(1)]
    public ChatDrawingRow(INotchHost host, ChatHandOff entry)
    {
      ChatDrawingRow.<>c__DisplayClass0_0 CS$<>8__locals1 = new ChatDrawingRow.<>c__DisplayClass0_0();
      CS$<>8__locals1.entry = entry;
      CS$<>8__locals1.host = host;
      base..ctor();
      ChatDrawingRow.<>c__DisplayClass0_1 CS$<>8__locals2 = new ChatDrawingRow.<>c__DisplayClass0_1();
      CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
      MessagesPalette messagesPalette = MessagesPalette.Current;
      base.Spacing = 6.0;
      ChatDrawingRow.<>c__DisplayClass0_1 CS$<>8__locals3 = CS$<>8__locals2;
      LessonService lessons = NotchChat.Lessons;
      CS$<>8__locals3.stored = ((lessons != null) ? lessons.Store.List().FirstOrDefault<StoredLesson>((StoredLesson l) => l.Data.Id == CS$<>8__locals2.CS$<>8__locals1.entry.LessonId) : null);
      if (CS$<>8__locals2.stored == null)
      {
        return;
      }
      ClickyHomeSpreadPaper paper = ClickyDrawingLaptopScreen.Paper;
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
      Border border = new Border
      {
        Width = 22.0,
        Height = 22.0,
        CornerRadius = new CornerRadius(Math.Round(5.720000000000001)),
        Background = Ui.Brush(paper.TitleWash)
      };
      TextBlock textBlock = Icons.Glyph("\ue70f", 10.5, paper.Deep);
      textBlock.FontWeight = FontWeights.SemiBold;
      border.Child = textBlock;
      grid.Children.Add(border);
      TextBlock textBlock2 = new TextBlock
      {
        FontSize = 11.5,
        FontWeight = FontWeights.Medium,
        FontFamily = DS.Fonts.Small,
        IsTextScaleFactorEnabled = false,
        TextTrimming = TextTrimming.CharacterEllipsis,
        VerticalAlignment = VerticalAlignment.Center,
        MaxLines = 1
      };
      textBlock2.Inlines.Add(new Run
      {
        Text = "Drew ",
        Foreground = Ui.Brush(messagesPalette.SecondaryText)
      });
      textBlock2.Inlines.Add(new Run
      {
        Text = CS$<>8__locals2.stored.Data.Title,
        Foreground = Ui.Brush(messagesPalette.PrimaryText),
        FontWeight = FontWeights.Bold
      });
      textBlock2.Inlines.Add(new Run
      {
        Text = "  ·  " + ChatBubbles.ShortTime(CS$<>8__locals2.CS$<>8__locals1.entry.CreatedAt),
        Foreground = Ui.Brush(messagesPalette.TertiaryText)
      });
      Grid.SetColumn(textBlock2, 1);
      grid.Children.Add(textBlock2);
      base.Children.Add(grid);
      double num = 0.19433198380566802;
      ClickyDrawingLaptopScreen clickyDrawingLaptopScreen = new ClickyDrawingLaptopScreen(CS$<>8__locals2.stored, new Action<bool>(CS$<>8__locals2.<.ctor>g__Play|1), new ValueTuple<string, string, Action>?(new ValueTuple<string, string, Action>("Remove from the chat (it stays in Drawings)", "Remove from chat", new Action(CS$<>8__locals2.CS$<>8__locals1.<.ctor>g__RemoveFromChat|2))), false, new double?(6.0 / num));
      base.Children.Add(new Viewbox
      {
        Width = 96.0,
        Height = ChatRowLayout.CardHeight,
        Stretch = Stretch.Uniform,
        Child = clickyDrawingLaptopScreen,
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top,
        Margin = new Thickness(30.0, 0.0, 0.0, 0.0)
      });
    }
  }
}
