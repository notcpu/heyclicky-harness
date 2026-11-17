using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x0200014D RID: 333
  [NullableContext(1)]
  [Nullable(0)]
  public static class CardParts
  {
    // Token: 0x060012C2 RID: 4802 RVA: 0x00599A14 File Offset: 0x00599A14
    public static void PlaceInEar(FrameworkElement close)
    {
      close.HorizontalAlignment = HorizontalAlignment.Right;
      close.VerticalAlignment = VerticalAlignment.Top;
      close.Margin = new Thickness(0.0, 2.0, 10.0, 0.0);
    }

    // Token: 0x060012C3 RID: 4803 RVA: 0x00599A54 File Offset: 0x00599A54
    public static Grid Tile(UIElement content, double size = 34.0)
    {
      Grid grid = new Grid();
      grid.Width = size;
      grid.Height = size;
      grid.VerticalAlignment = VerticalAlignment.Center;
      grid.Children.Add(new ShadowLayer(8.0, DS.Black(0.24), 8.0, 4.0, 0.0));
      grid.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(8.0),
        Background = Ui.Brush(DS.White(0.94)),
        BorderBrush = Ui.Brush(DS.Black(0.08)),
        BorderThickness = new Thickness(0.5)
      });
      FrameworkElement frameworkElement = content as FrameworkElement;
      if (frameworkElement != null)
      {
        frameworkElement.HorizontalAlignment = HorizontalAlignment.Center;
        frameworkElement.VerticalAlignment = VerticalAlignment.Center;
      }
      grid.Children.Add(content);
      return grid;
    }

    // Token: 0x060012C4 RID: 4804 RVA: 0x00599B4C File Offset: 0x00599B4C
    public static Grid IconTile(string glyph, double glyphSize, Color color)
    {
      TextBlock textBlock = Icons.Glyph(glyph, glyphSize, color);
      textBlock.FontWeight = FontWeights.SemiBold;
      return CardParts.Tile(textBlock, 34.0);
    }

    // Token: 0x060012C5 RID: 4805 RVA: 0x00599B70 File Offset: 0x00599B70
    public static Pressable PlainClose(Action onClick, string tip = "Dismiss")
    {
      Pressable pressable = new Pressable(true);
      pressable.Width = 20.0;
      pressable.Height = 20.0;
      pressable.Children.Add(new Ellipse
      {
        Width = 20.0,
        Height = 20.0,
        Fill = Ui.Brush(DS.White(0.1)),
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      });
      TextBlock textBlock = Icons.Glyph("\ue711", 8.5, DS.Colors.TextSecondary);
      textBlock.FontWeight = FontWeights.Bold;
      pressable.Children.Add(textBlock);
      pressable.Click += onClick;
      pressable.WithIconHover(20.0);
      return pressable.Tip(tip);
    }

    // Token: 0x060012C6 RID: 4806 RVA: 0x00599C44 File Offset: 0x00599C44
    public static StackPanel TwoLines(string small, string bold, bool smallShrinks = false)
    {
      TextBlock textBlock = Ui.Text(small, 10.5, FontWeights.Medium, DS.Colors.TextSecondary, TextAlignment.Left, 1, false, false);
      TextBlock textBlock2 = Ui.Text(bold, 12.5, FontWeights.SemiBold, DS.Colors.TextPrimary, TextAlignment.Left, 1, false, false);
      return new StackPanel
      {
        Spacing = 3.0,
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          smallShrinks ? Ui.Shrink(textBlock, HorizontalAlignment.Left, 0.85) : textBlock,
          Ui.Shrink(textBlock2, HorizontalAlignment.Left, 0.85)
        }
      };
    }

    // Token: 0x060012C7 RID: 4807 RVA: 0x00599CE4 File Offset: 0x00599CE4
    public static Grid HeadsUpRow(UIElement tile, UIElement lines, [Nullable(2)] UIElement action, [Nullable(2)] UIElement close)
    {
      FrameworkElement frameworkElement = close as FrameworkElement;
      if (frameworkElement != null && NotchGeometry.HasHardwareNotchEar)
      {
        CardParts.PlaceInEar(frameworkElement);
        return CardParts.Stack(new UIElement[]
        {
          CardParts.Row(tile, lines, action, null),
          frameworkElement
        });
      }
      return CardParts.Row(tile, lines, action, close);
    }

    // Token: 0x060012C8 RID: 4808 RVA: 0x00599D30 File Offset: 0x00599D30
    private static Grid Row(UIElement tile, UIElement lines, [Nullable(2)] UIElement action, [Nullable(2)] UIElement close)
    {
      Grid grid = new Grid
      {
        ColumnSpacing = 11.0,
        Margin = new Thickness(10.0, 34.0, 10.0, 0.0),
        VerticalAlignment = VerticalAlignment.Top
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
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      Grid.SetColumn((FrameworkElement)lines, 1);
      grid.Children.Add(tile);
      grid.Children.Add(lines);
      FrameworkElement frameworkElement = action as FrameworkElement;
      if (frameworkElement != null)
      {
        Grid.SetColumn(frameworkElement, 2);
        frameworkElement.Margin = new Thickness(21.0, 0.0, 0.0, 0.0);
        frameworkElement.VerticalAlignment = VerticalAlignment.Center;
        grid.Children.Add(frameworkElement);
      }
      FrameworkElement frameworkElement2 = close as FrameworkElement;
      if (frameworkElement2 != null)
      {
        Grid.SetColumn(frameworkElement2, 3);
        frameworkElement2.VerticalAlignment = VerticalAlignment.Center;
        if (action == null)
        {
          Grid.SetColumn(frameworkElement2, 2);
          frameworkElement2.Margin = new Thickness(21.0, 0.0, 0.0, 0.0);
        }
        grid.Children.Add(frameworkElement2);
      }
      grid.MinHeight = 34.0;
      return grid;
    }

    // Token: 0x060012C9 RID: 4809 RVA: 0x00599EE4 File Offset: 0x00599EE4
    public static StackPanel CenteredTitle(string title, string subtitle, double spacing = 3.0)
    {
      TextBlock textBlock = Ui.Text(title, 14.0, FontWeights.SemiBold, DS.Colors.TextPrimary, TextAlignment.Center, 1, false, false);
      TextBlock textBlock2 = Ui.Text(subtitle, 12.0, FontWeights.Medium, DS.Colors.TextSecondary, TextAlignment.Center, 1, false, false);
      return new StackPanel
      {
        Spacing = spacing,
        HorizontalAlignment = HorizontalAlignment.Stretch,
        Children = 
        {
          Ui.Shrink(textBlock, HorizontalAlignment.Center, 0.85),
          Ui.Shrink(textBlock2, HorizontalAlignment.Center, 0.85)
        }
      };
    }

    // Token: 0x060012CA RID: 4810 RVA: 0x00599F78 File Offset: 0x00599F78
    public static Grid Stack(params UIElement[] layers)
    {
      Grid grid = new Grid();
      foreach (UIElement uielement in layers)
      {
        grid.Children.Add(uielement);
      }
      return grid;
    }

    // Token: 0x040008A5 RID: 2213
    public const double ContentTopClearance = 34.0;
  }
}
