using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace HeyClicky.App.UI
{
  // Token: 0x020000A9 RID: 169
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class GelActionButton : Pressable
  {
    // Token: 0x06000718 RID: 1816 RVA: 0x00535410 File Offset: 0x00535410
    public GelActionButton(string title, [Nullable(2)] string iconGlyph, GelAppearance appearance, double height, double horizontalPadding, double minWidth = 0.0, double fontSize = 12.0, double iconSize = 10.0, double spacing = 5.0, bool iconTrailing = false)
      : base(true)
    {
      this.surface = new GelSurface(appearance, height / 2.0);
      base.Height = height;
      base.MinWidth = minWidth;
      Color color = DS.Black(0.85);
      TextBlock textBlock = Ui.Text(title, fontSize, FontWeights.SemiBold, color, TextAlignment.Left, 0, false, false);
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = spacing,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        Padding = new Thickness(horizontalPadding, 0.0, horizontalPadding, 0.0)
      };
      UIElement uielement = (string.IsNullOrEmpty(iconGlyph) ? null : Ui.Embossed(Icons.Glyph(iconGlyph, iconSize, color), 0.65, 1.0, 0.8));
      if (uielement != null && !iconTrailing)
      {
        stackPanel.Children.Add(uielement);
      }
      stackPanel.Children.Add(Ui.Embossed(textBlock, 0.65, 1.0, 0.8));
      if (uielement != null && iconTrailing)
      {
        stackPanel.Children.Add(uielement);
      }
      base.Children.Add(this.surface);
      base.Children.Add(stackPanel);
      base.HoverChanged += delegate(bool h)
      {
        this.surface.Hovered = h;
      };
      base.WithPressScale(0.97f, 0.12, true);
    }

    // Token: 0x06000719 RID: 1817 RVA: 0x00535580 File Offset: 0x00535580
    public static GelActionButton WithIcon(string title, Func<Color, FrameworkElement> icon, GelAppearance appearance, double height, double horizontalPadding, double minWidth = 0.0, double fontSize = 12.0, double spacing = 5.0, bool iconTrailing = false)
    {
      GelActionButton gelActionButton = new GelActionButton(title, null, appearance, height, horizontalPadding, minWidth, fontSize, 10.0, spacing, iconTrailing);
      StackPanel stackPanel = (StackPanel)gelActionButton.Children[1];
      FrameworkElement frameworkElement = icon(DS.White(0.65));
      frameworkElement.RenderTransform = new TranslateTransform
      {
        Y = 1.0
      };
      Grid grid = new Grid
      {
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          frameworkElement,
          icon(DS.Black(0.85))
        }
      };
      if (iconTrailing)
      {
        stackPanel.Children.Add(grid);
        return gelActionButton;
      }
      stackPanel.Children.Insert(0, grid);
      return gelActionButton;
    }

    // Token: 0x040001E8 RID: 488
    private readonly GelSurface surface;
  }
}
