using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.Onboarding;
using HeyClicky.App.UI;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000D6 RID: 214
  [NullableContext(1)]
  [Nullable(0)]
  public static class SK
  {
    // Token: 0x17000191 RID: 401
    // (get) Token: 0x06000A26 RID: 2598 RVA: 0x0055600C File Offset: 0x0055600C
    public static Color Ink
    {
      get
      {
        return MessagesPalette.Current.Ink;
      }
    }

    // Token: 0x17000192 RID: 402
    // (get) Token: 0x06000A27 RID: 2599 RVA: 0x00556018 File Offset: 0x00556018
    public static Color TextPrimary
    {
      get
      {
        return MessagesPalette.Current.ThemeTextPrimary;
      }
    }

    // Token: 0x17000193 RID: 403
    // (get) Token: 0x06000A28 RID: 2600 RVA: 0x00556024 File Offset: 0x00556024
    public static Color TextSecondary
    {
      get
      {
        return MessagesPalette.Current.ThemeTextSecondary;
      }
    }

    // Token: 0x17000194 RID: 404
    // (get) Token: 0x06000A29 RID: 2601 RVA: 0x00556030 File Offset: 0x00556030
    public static Color TextTertiary
    {
      get
      {
        return MessagesPalette.Current.ThemeTextTertiary;
      }
    }

    // Token: 0x17000195 RID: 405
    // (get) Token: 0x06000A2A RID: 2602 RVA: 0x0055603C File Offset: 0x0055603C
    public static Color WindowBackground
    {
      get
      {
        return MessagesPalette.Current.WindowBackground;
      }
    }

    // Token: 0x06000A2B RID: 2603 RVA: 0x00556048 File Offset: 0x00556048
    public static Grid Scaffold(string title, string description, [Nullable(new byte[] { 1, 2 })] IEnumerable<FrameworkElement> sections, double sectionSpacing = 20.0)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 5.0,
        Children = 
        {
          Ui.Text(title, 22.0, FontWeights.SemiBold, SK.TextPrimary, TextAlignment.Left, 0, false, false),
          Ui.Text(description, 12.0, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 0, true, false)
        }
      };
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = sectionSpacing,
        Margin = new Thickness(32.0, 10.0, 32.0, 32.0),
        Children = { stackPanel }
      };
      foreach (FrameworkElement frameworkElement in sections)
      {
        if (frameworkElement != null)
        {
          stackPanel2.Children.Add(frameworkElement);
        }
      }
      return new Grid
      {
        HorizontalAlignment = HorizontalAlignment.Stretch,
        ColumnDefinitions = 
        {
          new ColumnDefinition
          {
            Width = new GridLength(1.0, GridUnitType.Star),
            MaxWidth = 684.0
          },
          new ColumnDefinition
          {
            Width = new GridLength(0.0, GridUnitType.Pixel)
          }
        },
        Children = { stackPanel2 }
      };
    }

    // Token: 0x06000A2C RID: 2604 RVA: 0x005561B8 File Offset: 0x005561B8
    [NullableContext(2)]
    [return: Nullable(1)]
    public static StackPanel Group(string label, string footnote, [Nullable(new byte[] { 1, 2 })] IEnumerable<FrameworkElement> rows, double dividerInset = 14.0, string caption = null)
    {
      StackPanel stackPanel = new StackPanel();
      bool flag = true;
      foreach (FrameworkElement frameworkElement in rows)
      {
        if (frameworkElement != null)
        {
          if (!flag)
          {
            stackPanel.Children.Add(SK.Divider(dividerInset));
          }
          stackPanel.Children.Add(frameworkElement);
          flag = false;
        }
      }
      Border border = new Border
      {
        CornerRadius = new CornerRadius(14.0),
        Background = Ui.Brush(SK.Ink.WithAlpha(0.05)),
        BorderBrush = Ui.Brush(SK.Ink.WithAlpha(0.06)),
        BorderThickness = new Thickness(1.0),
        Child = stackPanel,
        HorizontalAlignment = HorizontalAlignment.Stretch
      };
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 7.0
      };
      if (label != null)
      {
        if (caption == null)
        {
          stackPanel2.Children.Add(SK.MicroLabel(label, 10.0, FontWeights.Bold, 100));
        }
        else
        {
          StackPanel stackPanel3 = Ui.H(8.0, new UIElement[]
          {
            SK.MicroLabel(label, 10.0, FontWeights.Bold, 100),
            Ui.Text(caption, 11.0, FontWeights.Normal, SK.TextSecondary, TextAlignment.Left, 0, false, false)
          });
          stackPanel3.Margin = new Thickness(4.0, 0.0, 4.0, 0.0);
          stackPanel2.Children.Add(stackPanel3);
        }
      }
      stackPanel2.Children.Add(border);
      if (footnote != null)
      {
        TextBlock textBlock = Ui.Text(footnote, 10.5, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 0, true, false);
        textBlock.Margin = new Thickness(4.0, 0.0, 4.0, 0.0);
        stackPanel2.Children.Add(textBlock);
      }
      return stackPanel2;
    }

    // Token: 0x06000A2D RID: 2605 RVA: 0x005563E4 File Offset: 0x005563E4
    public static StackPanel Group([Nullable(2)] string label, [Nullable(new byte[] { 1, 2 })] params FrameworkElement[] rows)
    {
      return SK.Group(label, null, rows, 14.0, null);
    }

    // Token: 0x06000A2E RID: 2606 RVA: 0x005563F8 File Offset: 0x005563F8
    public static TextBlock MicroLabel(string text, double size, FontWeight weight, int spacing)
    {
      TextBlock textBlock = Ui.Text(text.ToUpperInvariant(), size, weight, SK.TextTertiary, TextAlignment.Left, 0, false, false);
      textBlock.CharacterSpacing = spacing;
      textBlock.Margin = new Thickness(4.0, 0.0, 4.0, 0.0);
      return textBlock;
    }

    // Token: 0x06000A2F RID: 2607 RVA: 0x00556454 File Offset: 0x00556454
    public static Border Divider(double leadingInset = 14.0)
    {
      return new Border
      {
        Height = 1.0,
        Background = Ui.Brush(SK.Ink.WithAlpha(0.06)),
        Margin = new Thickness(leadingInset, 0.0, 0.0, 0.0)
      };
    }

    // Token: 0x06000A30 RID: 2608 RVA: 0x005564BC File Offset: 0x005564BC
    public static Grid Row(string title, [Nullable(2)] string subtitle, [Nullable(2)] FrameworkElement trailing, Color? titleColor = null)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 2.5,
        VerticalAlignment = VerticalAlignment.Center,
        Children = { Ui.Text(title, 13.0, FontWeights.Medium, titleColor ?? SK.TextPrimary, TextAlignment.Left, 0, true, false) }
      };
      if (!string.IsNullOrEmpty(subtitle))
      {
        stackPanel.Children.Add(Ui.Text(subtitle, 11.0, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 0, true, false));
      }
      Grid grid = new Grid
      {
        Padding = new Thickness(14.0, 11.0, 14.0, 11.0),
        ColumnSpacing = 12.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.Children.Add(stackPanel);
      if (trailing != null)
      {
        Grid.SetColumn(trailing, 1);
        trailing.VerticalAlignment = VerticalAlignment.Center;
        grid.Children.Add(trailing);
      }
      if (trailing is Pressable && string.IsNullOrEmpty(AutomationProperties.GetName(trailing)))
      {
        AutomationProperties.SetName(trailing, title);
      }
      return grid;
    }

    // Token: 0x06000A31 RID: 2609 RVA: 0x0055661C File Offset: 0x0055661C
    public static Grid ToggleRow(string title, [Nullable(2)] string subtitle, bool isOn, Action<bool> changed)
    {
      return SK.Row(title, subtitle, SK.Switch(isOn, changed), null);
    }

    // Token: 0x06000A32 RID: 2610 RVA: 0x00556640 File Offset: 0x00556640
    public static FrameworkElement TrailingSymbol(string glyph)
    {
      if (glyph == "arrow.up.right")
      {
        return Icons.Vector(8.5, SK.TextTertiary, null, "M3.5,20.5 L20,4 M7,4 L20,4 L20,17", 3.6);
      }
      if (glyph == "play.fill")
      {
        return Icons.Vector(9.0, SK.TextTertiary, "M5,3.5 C5,2.4 6.2,1.7 7.2,2.3 L20.6,10.7 C21.5,11.3 21.5,12.7 20.6,13.3 L7.2,21.7 C6.2,22.3 5,21.6 5,20.5 Z", null, 2.0);
      }
      TextBlock textBlock = Icons.Glyph((glyph == "chevron") ? "\ue76c" : glyph, 10.0, SK.TextTertiary);
      textBlock.FontWeight = FontWeights.SemiBold;
      return textBlock;
    }

    // Token: 0x06000A33 RID: 2611 RVA: 0x005566E0 File Offset: 0x005566E0
    public static Pressable ActionRow(string title, [Nullable(2)] string subtitle, Action onClick, [Nullable(2)] string trailingText = null, [Nullable(2)] string trailingGlyph = "chevron", bool destructive = false)
    {
      Pressable p = new Pressable(true);
      StackPanel stackPanel = Ui.H(7.0, Array.Empty<UIElement>());
      if (trailingText != null)
      {
        TextBlock textBlock = Ui.Text(trailingText, 11.5, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 1, false, false);
        textBlock.MaxWidth = 220.0;
        textBlock.TextAlignment = TextAlignment.Right;
        stackPanel.Children.Add(textBlock);
      }
      if (trailingGlyph != null && trailingGlyph.Length > 0)
      {
        stackPanel.Children.Add(SK.TrailingSymbol(trailingGlyph));
      }
      p.Children.Add(SK.Row(title, subtitle, (stackPanel.Children.Count > 0) ? stackPanel : null, destructive ? new Color?(SK.Destructive) : null));
      Color ink = SK.Ink;
      p.HoverChanged += delegate(bool h)
      {
        if (p.Enabled)
        {
          p.Background = Ui.Brush(ink.WithAlpha(h ? 0.05 : 0.0));
        }
      };
      p.Click += onClick;
      return p;
    }

    // Token: 0x06000A34 RID: 2612 RVA: 0x005567EC File Offset: 0x005567EC
    public static Pressable Switch(bool on, Action<bool> changed)
    {
      SK.<>c__DisplayClass26_0 CS$<>8__locals1 = new SK.<>c__DisplayClass26_0();
      CS$<>8__locals1.on = on;
      CS$<>8__locals1.changed = changed;
      CS$<>8__locals1.p = new Pressable(true)
      {
        Width = 46.0,
        Height = 24.0,
        AccessibleToggleState = new bool?(CS$<>8__locals1.on)
      };
      CS$<>8__locals1.ink = SK.Ink;
      CS$<>8__locals1.windowBackground = SK.WindowBackground;
      CS$<>8__locals1.glow = new ShadowLayer(12.0, CS$<>8__locals1.ink.WithAlpha(0.25), 5.0, 0.0, 0.0)
      {
        Opacity = 0.0
      };
      CS$<>8__locals1.track = new Border
      {
        Background = Ui.Brush(CS$<>8__locals1.on ? CS$<>8__locals1.ink : CS$<>8__locals1.ink.WithAlpha(0.16))
      }.Capsule();
      CS$<>8__locals1.gel = new GelSurface(GelAppearance.PrimaryBlue, 12.0)
      {
        Opacity = (CS$<>8__locals1.on ? 1.0 : 0.16),
        ShowsHoverSheen = false
      };
      CS$<>8__locals1.rim = new Border
      {
        BorderThickness = new Thickness(1.0),
        BorderBrush = Ui.Brush(CS$<>8__locals1.ink.WithAlpha(CS$<>8__locals1.on ? 0.0 : 0.1)),
        IsHitTestVisible = false
      }.Capsule();
      ShadowLayer shadowLayer = new ShadowLayer(9.0, DS.Black(CS$<>8__locals1.on ? 0.18 : 0.08), 1.5, 0.5, 0.0);
      CS$<>8__locals1.knob = new Border
      {
        Background = Ui.Brush(CS$<>8__locals1.on ? CS$<>8__locals1.windowBackground : CS$<>8__locals1.ink.WithAlpha(0.65))
      }.Capsule();
      CS$<>8__locals1.knobHost = new Grid
      {
        Width = 18.0,
        Height = 18.0,
        HorizontalAlignment = HorizontalAlignment.Left,
        Margin = new Thickness(3.0, 0.0, 0.0, 0.0),
        Children = { shadowLayer, CS$<>8__locals1.knob }
      };
      CS$<>8__locals1.knobHost.Translation = new Vector3(CS$<>8__locals1.on ? ((float)CS$<>8__locals1.<Switch>g__Travel|0()) : 0f, 0f, 0f);
      CS$<>8__locals1.p.Children.Add(CS$<>8__locals1.glow);
      CS$<>8__locals1.p.Children.Add(CS$<>8__locals1.track);
      CS$<>8__locals1.p.Children.Add(CS$<>8__locals1.gel);
      CS$<>8__locals1.p.Children.Add(CS$<>8__locals1.rim);
      CS$<>8__locals1.p.Children.Add(CS$<>8__locals1.knobHost);
      CS$<>8__locals1.p.HoverChanged += delegate(bool h)
      {
        CS$<>8__locals1.gel.Hovered = h;
        CS$<>8__locals1.glow.Opacity = (double)((CS$<>8__locals1.on && h) ? 1 : 0);
      };
      CS$<>8__locals1.p.PressedChanged += delegate(bool pressed)
      {
        CS$<>8__locals1.knobHost.Width = (pressed ? 23.0 : 18.0);
        base.<Switch>g__Slide|1(0.18);
      };
      CS$<>8__locals1.p.Click += delegate
      {
        CS$<>8__locals1.on = !CS$<>8__locals1.on;
        CS$<>8__locals1.p.AccessibleToggleState = new bool?(CS$<>8__locals1.on);
        CS$<>8__locals1.track.Background = Ui.Brush(CS$<>8__locals1.on ? CS$<>8__locals1.ink : CS$<>8__locals1.ink.WithAlpha(0.16));
        CS$<>8__locals1.gel.Opacity = (CS$<>8__locals1.on ? 1.0 : 0.16);
        CS$<>8__locals1.rim.BorderBrush = Ui.Brush(CS$<>8__locals1.ink.WithAlpha(CS$<>8__locals1.on ? 0.0 : 0.1));
        CS$<>8__locals1.knob.Background = Ui.Brush(CS$<>8__locals1.on ? CS$<>8__locals1.windowBackground : CS$<>8__locals1.ink.WithAlpha(0.65));
        CS$<>8__locals1.glow.Opacity = (double)((CS$<>8__locals1.on && CS$<>8__locals1.p.IsHovered) ? 1 : 0);
        base.<Switch>g__Slide|1(0.28);
        CS$<>8__locals1.changed(CS$<>8__locals1.on);
      };
      return CS$<>8__locals1.p;
    }

    // Token: 0x06000A35 RID: 2613 RVA: 0x00556B7C File Offset: 0x00556B7C
    public static Pressable Chip(string label, bool selected, bool gelSelection, Action onClick)
    {
      Pressable p = new Pressable(true)
      {
        CornerRadius = new CornerRadius(100.0)
      };
      Color ink = SK.Ink;
      GelSurface gel = null;
      if (selected && gelSelection)
      {
        gel = new GelSurface(GelAppearance.PrimaryBlue, 100.0);
        p.Children.Add(gel);
      }
      else
      {
        p.Background = Ui.Brush(selected ? SK.Ink : SK.Ink.WithAlpha(0.07));
      }
      TextBlock textBlock = Ui.Text(label, 11.5, FontWeights.SemiBold, selected ? (gelSelection ? Colors.Black : SK.WindowBackground) : SK.TextSecondary, TextAlignment.Left, 0, false, false);
      textBlock.Margin = new Thickness(13.0, 6.0, 13.0, 6.0);
      p.Children.Add(textBlock);
      p.HoverChanged += delegate(bool h)
      {
        if (gel != null)
        {
          gel.Hovered = h;
          return;
        }
        if (!selected)
        {
          p.Background = Ui.Brush(ink.WithAlpha(h ? 0.13 : 0.07));
        }
      };
      p.Click += onClick;
      return p;
    }

    // Token: 0x06000A36 RID: 2614 RVA: 0x00556CD4 File Offset: 0x00556CD4
    public static Pressable PillButton(string label, Action onClick, bool prominent = false, bool destructive = false, bool warning = false)
    {
      SK.<>c__DisplayClass28_0 CS$<>8__locals1 = new SK.<>c__DisplayClass28_0();
      CS$<>8__locals1.prominent = prominent;
      CS$<>8__locals1.warning = warning;
      CS$<>8__locals1.p = new Pressable(true)
      {
        CornerRadius = new CornerRadius(100.0)
      };
      Color color = (CS$<>8__locals1.prominent ? SK.WindowBackground : (destructive ? SK.Destructive : (CS$<>8__locals1.warning ? SK.Warning : SK.TextPrimary)));
      CS$<>8__locals1.ink = SK.Ink;
      CS$<>8__locals1.p.Background = Ui.Brush(CS$<>8__locals1.<PillButton>g__Bg|0(false));
      TextBlock textBlock = Ui.Text(label, 11.0, FontWeights.SemiBold, color, TextAlignment.Left, 0, false, false);
      textBlock.Margin = new Thickness(12.0, 5.5, 12.0, 5.5);
      CS$<>8__locals1.p.Children.Add(textBlock);
      CS$<>8__locals1.p.HoverChanged += delegate(bool h)
      {
        CS$<>8__locals1.p.Background = Ui.Brush(base.<PillButton>g__Bg|0(h));
      };
      CS$<>8__locals1.p.Click += onClick;
      return CS$<>8__locals1.p;
    }

    // Token: 0x06000A37 RID: 2615 RVA: 0x00556DEC File Offset: 0x00556DEC
    public static Pressable MenuPicker(IReadOnlyList<string> options, int selected, Action<int> picked, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> labels = null)
    {
      Pressable p = new Pressable(true)
      {
        Padding = new Thickness(2.0, 3.0, 2.0, 3.0)
      };
      if (labels == null)
      {
        labels = options;
      }
      TextBlock label = Ui.Text(labels[Math.Clamp(selected, 0, options.Count - 1)], 11.5, FontWeights.Medium, SK.TextSecondary, TextAlignment.Left, 1, false, false);
      TextBlock chevron = Icons.Glyph("\ue70d", 8.0, SK.TextSecondary);
      chevron.FontWeight = FontWeights.Bold;
      p.Children.Add(Ui.H(5.0, new UIElement[] { label, chevron }));
      MenuFlyout menu = new MenuFlyout
      {
        Placement = FlyoutPlacementMode.BottomEdgeAlignedRight
      };
      for (int i = 0; i < options.Count; i++)
      {
        int index = i;
        ToggleMenuFlyoutItem item = new ToggleMenuFlyoutItem
        {
          Text = options[i],
          IsChecked = (i == selected)
        };
        item.Click += delegate(object _, RoutedEventArgs _)
        {
          label.Text = labels[index];
          foreach (ToggleMenuFlyoutItem toggleMenuFlyoutItem in menu.Items.OfType<ToggleMenuFlyoutItem>())
          {
            toggleMenuFlyoutItem.IsChecked = toggleMenuFlyoutItem == item;
          }
          picked(index);
        };
        menu.Items.Add(item);
      }
      Color primary = SK.TextPrimary;
      Color secondary = SK.TextSecondary;
      p.HoverChanged += delegate(bool h)
      {
        label.Foreground = Ui.Brush(h ? primary : secondary);
        chevron.Foreground = label.Foreground;
      };
      p.Click += delegate
      {
        menu.ShowAt(p);
      };
      return p;
    }

    // Token: 0x06000A38 RID: 2616 RVA: 0x00556FCC File Offset: 0x00556FCC
    [return: TupleElementNames(new string[] { "Box", "Input" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    public static ValueTuple<Grid, TextBox> SearchField(string placeholder, string text, Action<string> changed, bool magnifier = true, double fontSize = 12.0, Color? placeholderColor = null)
    {
      TextBox input = new TextBox
      {
        PlaceholderText = placeholder,
        Text = text,
        FontSize = fontSize,
        FontFamily = DS.Fonts.Text,
        Padding = new Thickness((double)(magnifier ? 26 : 11), 6.0, 9.0, 6.0),
        MinHeight = 0.0,
        IsSpellCheckEnabled = false,
        IsTextScaleFactorEnabled = false,
        BorderThickness = new Thickness(0.0),
        CornerRadius = new CornerRadius(9.0),
        Foreground = Ui.Brush(SK.TextPrimary),
        VerticalAlignment = VerticalAlignment.Center
      };
      ResourceDictionary resources = input.Resources;
      foreach (string text2 in new string[] { "TextControlBackground", "TextControlBackgroundPointerOver", "TextControlBackgroundFocused" })
      {
        resources[text2] = Ui.Brush(SK.Ink.WithAlpha(0.07));
      }
      foreach (string text3 in new string[] { "TextControlBorderBrush", "TextControlBorderBrushPointerOver", "TextControlBorderBrushFocused" })
      {
        resources[text3] = Ui.Transparent;
      }
      foreach (string text4 in new string[] { "TextControlForeground", "TextControlForegroundPointerOver", "TextControlForegroundFocused" })
      {
        resources[text4] = Ui.Brush(SK.TextPrimary);
      }
      resources["TextControlPlaceholderForeground"] = (resources["TextControlPlaceholderForegroundPointerOver"] = (resources["TextControlPlaceholderForegroundFocused"] = Ui.Brush(placeholderColor ?? SK.TextTertiary)));
      resources["TextControlBorderThemeThicknessFocused"] = new Thickness(0.0);
      input.TextChanged += delegate(object _, TextChangedEventArgs _)
      {
        changed(input.Text);
      };
      Grid grid = new Grid();
      grid.Children.Add(input);
      if (magnifier)
      {
        TextBlock textBlock = Icons.Glyph("\ue721", 11.0, SK.TextTertiary);
        textBlock.HorizontalAlignment = HorizontalAlignment.Left;
        textBlock.Margin = new Thickness(10.0, 0.0, 0.0, 0.0);
        textBlock.IsHitTestVisible = false;
        grid.Children.Add(textBlock);
      }
      return new ValueTuple<Grid, TextBox>(grid, input);
    }

    // Token: 0x06000A39 RID: 2617 RVA: 0x0055729C File Offset: 0x0055729C
    public static StackPanel KeyCaps(HotkeyBinding binding, bool rendersDoubleTapAsPair = true)
    {
      StackPanel stackPanel = Ui.H(4.0, Array.Empty<UIElement>());
      IReadOnlyList<string> keyNames = binding.KeyNames;
      foreach (string text in ((binding.Trigger == HotkeyTrigger.DoubleTap && rendersDoubleTapAsPair) ? keyNames.Concat<string>(keyNames).ToList<string>() : keyNames.ToList<string>()))
      {
        stackPanel.Children.Add(new Border
        {
          CornerRadius = new CornerRadius(5.0),
          Padding = new Thickness(6.0, 3.0, 6.0, 3.0),
          Background = Ui.Brush(DS.Hex(4210752U, 1.0)),
          Child = Ui.Text(text, 11.0, FontWeights.SemiBold, DS.Colors.TextPrimary, TextAlignment.Left, 0, false, false)
        });
      }
      ToolTipService.SetToolTip(stackPanel, binding.DisplayText);
      return stackPanel;
    }

    // Token: 0x06000A3A RID: 2618 RVA: 0x005573C8 File Offset: 0x005573C8
    public static Pressable Tile(FrameworkElement content, bool selected, double radius, Action onClick, double checkSize = 20.0, bool topRightCheck = true)
    {
      Color ink = SK.Ink;
      Pressable tile = new Pressable(true)
      {
        CornerRadius = new CornerRadius(radius),
        Background = Ui.Brush(ink.WithAlpha(selected ? 0.09 : 0.04))
      };
      if (!selected)
      {
        tile.Children.Add(new Border
        {
          CornerRadius = new CornerRadius(radius),
          BorderBrush = Ui.Brush(ink.WithAlpha(0.12)),
          BorderThickness = new Thickness(1.0),
          IsHitTestVisible = false
        });
      }
      tile.Children.Add(content);
      if (selected)
      {
        tile.Children.Add(new GelSelectionBorder(radius)
        {
          Margin = new Thickness(0.0)
        });
        if (topRightCheck)
        {
          FrameworkElement frameworkElement = IntroWindow.GelCheckmark(checkSize);
          frameworkElement.HorizontalAlignment = HorizontalAlignment.Right;
          frameworkElement.VerticalAlignment = VerticalAlignment.Top;
          frameworkElement.Margin = new Thickness(0.0, 8.0, 8.0, 0.0);
          tile.Children.Add(frameworkElement);
        }
      }
      tile.HoverChanged += delegate(bool h)
      {
        if (!selected)
        {
          tile.Background = Ui.Brush(ink.WithAlpha(h ? 0.07 : 0.04));
        }
      };
      tile.Click += onClick;
      return tile;
    }

    // Token: 0x06000A3B RID: 2619 RVA: 0x00557564 File Offset: 0x00557564
    public static FrameworkElement Alert(string text, Color color)
    {
      TextBlock textBlock = Icons.Glyph("\ue7ba", 10.0, color);
      TextBlock textBlock2 = Ui.Text(text, 11.0, FontWeights.SemiBold, color, TextAlignment.Left, 0, true, false);
      Grid grid = new Grid();
      grid.ColumnSpacing = 6.0;
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      textBlock.VerticalAlignment = VerticalAlignment.Top;
      textBlock.Margin = new Thickness(0.0, 2.0, 0.0, 0.0);
      Grid.SetColumn(textBlock2, 1);
      grid.Children.Add(textBlock);
      grid.Children.Add(textBlock2);
      return grid;
    }

    // Token: 0x06000A3C RID: 2620 RVA: 0x00557634 File Offset: 0x00557634
    public static WrapPanel Wrap(double spacing)
    {
      return new WrapPanel
      {
        Spacing = spacing
      };
    }

    // Token: 0x040003B5 RID: 949
    public static readonly Color Destructive = DS.Rgb(1.0, 0.47, 0.44, 1.0);

    // Token: 0x040003B6 RID: 950
    public static readonly Color Positive = DS.Rgb(0.3, 0.83, 0.55, 1.0);

    // Token: 0x040003B7 RID: 951
    public static readonly Color Warning = DS.Rgb(1.0, 0.75, 0.35, 1.0);

    // Token: 0x040003B8 RID: 952
    public const string Chevron = "chevron";

    // Token: 0x040003B9 RID: 953
    public const string NoGlyph = "";

    // Token: 0x040003BA RID: 954
    public const string ArrowUpRight = "arrow.up.right";

    // Token: 0x040003BB RID: 955
    public const string PlayFill = "play.fill";
  }
}
