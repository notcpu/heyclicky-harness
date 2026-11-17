using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200028A RID: 650
  public static class ClickyInkProse
  {
    // Token: 0x06001B46 RID: 6982 RVA: 0x005ECE5C File Offset: 0x005ECE5C
    [NullableContext(1)]
    public static FrameworkElement Make(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      bool flag = block.Words.Current.CharacterCount() > 150;
      bool flag2 = block.Source.Kind == ClickyInkDocument.Kind.Quote;
      InkTextView inkTextView = ClickyInkWordsView.Make(block.Words, clock, style, flag ? ClickyPaperFonts.Typed(13.5, null) : ClickyPaperFonts.Hand(19.0), true, null, null);
      inkTextView.LineSpacing = (double)(flag ? 3 : 1);
      double? goneAt = block.GoneAt;
      if (goneAt != null)
      {
        double valueOrDefault = goneAt.GetValueOrDefault();
        if (clock >= valueOrDefault)
        {
          inkTextView.Strikethrough = new Color?(style.Fresh);
        }
      }
      Grid grid = new Grid
      {
        Width = contentWidth,
        HorizontalAlignment = HorizontalAlignment.Left,
        ColumnSpacing = 10.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      if (flag2)
      {
        grid.Children.Add(new Border
        {
          Width = 3.0,
          CornerRadius = new CornerRadius(1.5),
          Background = Ui.Brush(style.Fresh.Opacity(0.35))
        });
      }
      else
      {
        grid.ColumnSpacing = 0.0;
      }
      Grid.SetColumn(inkTextView, 1);
      grid.Children.Add(inkTextView);
      return grid;
    }
  }
}
