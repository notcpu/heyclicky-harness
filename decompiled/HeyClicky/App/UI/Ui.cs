using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.UI
{
  // Token: 0x020000BA RID: 186
  [NullableContext(1)]
  [Nullable(0)]
  public static class Ui
  {
    // Token: 0x06000781 RID: 1921 RVA: 0x00538F10 File Offset: 0x00538F10
    public static SolidColorBrush Brush(Color c)
    {
      uint num = (uint)(((int)c.A << 24) | ((int)c.R << 16) | ((int)c.G << 8) | (int)c.B);
      SolidColorBrush solidColorBrush;
      if (!Ui.brushCache.TryGetValue(num, out solidColorBrush))
      {
        solidColorBrush = (Ui.brushCache[num] = new SolidColorBrush(c));
      }
      return solidColorBrush;
    }

    // Token: 0x06000782 RID: 1922 RVA: 0x00538F68 File Offset: 0x00538F68
    public static TextBlock Text(string text, double size, FontWeight weight, Color color, TextAlignment align = TextAlignment.Left, int maxLines = 0, bool wrap = false, bool mono = false)
    {
      TextBlock textBlock = new TextBlock
      {
        Text = text,
        FontSize = size,
        FontWeight = weight,
        FontFamily = (mono ? DS.Fonts.Mono : DS.Fonts.For(size)),
        Foreground = Ui.Brush(color),
        TextAlignment = align,
        IsTextScaleFactorEnabled = false,
        TextWrapping = ((wrap || maxLines > 1) ? TextWrapping.WrapWholeWords : TextWrapping.NoWrap),
        TextTrimming = TextTrimming.CharacterEllipsis,
        OpticalMarginAlignment = OpticalMarginAlignment.TrimSideBearings
      };
      if (maxLines > 0)
      {
        textBlock.MaxLines = maxLines;
      }
      if (align == TextAlignment.Center)
      {
        textBlock.HorizontalAlignment = HorizontalAlignment.Center;
      }
      return textBlock;
    }

    // Token: 0x06000783 RID: 1923 RVA: 0x00538FFC File Offset: 0x00538FFC
    public static FrameworkElement Shrink(TextBlock t, HorizontalAlignment align = HorizontalAlignment.Left, double minimumScale = 0.85)
    {
      return new ScaleDownText(t, minimumScale)
      {
        HorizontalAlignment = align
      };
    }

    // Token: 0x06000784 RID: 1924 RVA: 0x0053900C File Offset: 0x0053900C
    public static TextBlock Semibold(string text, double size, Color color)
    {
      return Ui.Text(text, size, FontWeights.SemiBold, color, TextAlignment.Left, 0, false, false);
    }

    // Token: 0x06000785 RID: 1925 RVA: 0x00539020 File Offset: 0x00539020
    public static TextBlock Medium(string text, double size, Color color)
    {
      return Ui.Text(text, size, FontWeights.Medium, color, TextAlignment.Left, 0, false, false);
    }

    // Token: 0x06000786 RID: 1926 RVA: 0x00539034 File Offset: 0x00539034
    public static TextBlock Regular(string text, double size, Color color)
    {
      return Ui.Text(text, size, FontWeights.Normal, color, TextAlignment.Left, 0, false, false);
    }

    // Token: 0x06000787 RID: 1927 RVA: 0x00539048 File Offset: 0x00539048
    public static Grid Embossed(TextBlock ink, double highlightAlpha = 0.8, double dy = 1.5, double radius = 1.0)
    {
      Canvas host = new Canvas
      {
        IsHitTestVisible = false
      };
      Grid grid = new Grid();
      grid.Children.Add(host);
      grid.Children.Add(ink);
      DropShadow drop = Ui.Compositor.CreateDropShadow();
      drop.Color = DS.White(highlightAlpha);
      drop.BlurRadius = (float)(radius * 2.0);
      drop.Offset = new Vector3(0f, (float)dy, 0f);
      SpriteVisual sprite = Ui.Compositor.CreateSpriteVisual();
      sprite.Shadow = drop;
      ElementCompositionPreview.SetElementChildVisual(host, sprite);
      Ui.embosses.AddOrUpdate(grid, drop);
      ink.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        base.<Embossed>g__Place|0();
      };
      grid.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        base.<Embossed>g__Place|0();
      };
      return grid;
    }

    // Token: 0x06000788 RID: 1928 RVA: 0x00539160 File Offset: 0x00539160
    public static void SetEmboss(Grid embossed, double highlightAlpha)
    {
      DropShadow dropShadow;
      if (Ui.embosses.TryGetValue(embossed, out dropShadow))
      {
        dropShadow.Color = DS.White(highlightAlpha);
      }
    }

    // Token: 0x06000789 RID: 1929 RVA: 0x00539188 File Offset: 0x00539188
    public static StackPanel H(double spacing, [Nullable(new byte[] { 1, 2 })] params UIElement[] children)
    {
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = spacing
      };
      foreach (UIElement uielement in children)
      {
        if (uielement != null)
        {
          stackPanel.Children.Add(uielement);
        }
      }
      return stackPanel;
    }

    // Token: 0x0600078A RID: 1930 RVA: 0x005391D0 File Offset: 0x005391D0
    public static StackPanel V(double spacing, [Nullable(new byte[] { 1, 2 })] params UIElement[] children)
    {
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Vertical,
        Spacing = spacing
      };
      foreach (UIElement uielement in children)
      {
        if (uielement != null)
        {
          stackPanel.Children.Add(uielement);
        }
      }
      return stackPanel;
    }

    // Token: 0x0600078B RID: 1931 RVA: 0x00539218 File Offset: 0x00539218
    public static Grid Row(double spacing, UIElement[] leading, UIElement[] trailing, double minSpacer = 0.0)
    {
      Grid grid = new Grid
      {
        ColumnSpacing = spacing
      };
      int num = 0;
      foreach (UIElement uielement in leading)
      {
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        Grid.SetColumn((FrameworkElement)uielement, num++);
        grid.Children.Add(uielement);
      }
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star),
        MinWidth = minSpacer
      });
      num++;
      foreach (UIElement uielement2 in trailing)
      {
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        Grid.SetColumn((FrameworkElement)uielement2, num++);
        grid.Children.Add(uielement2);
      }
      return grid;
    }

    // Token: 0x0600078C RID: 1932 RVA: 0x00539304 File Offset: 0x00539304
    public static Border Box(double w, double h, Color fill, double radius = 0.0)
    {
      return new Border
      {
        Width = w,
        Height = h,
        Background = Ui.Brush(fill),
        CornerRadius = new CornerRadius(radius)
      };
    }

    // Token: 0x0600078D RID: 1933 RVA: 0x00539334 File Offset: 0x00539334
    public static Border Capsule(this Border b)
    {
      b.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        base.<Capsule>g__Fit|0();
      };
      return b;
    }

    // Token: 0x0600078E RID: 1934 RVA: 0x0053936C File Offset: 0x0053936C
    public static T Pad<[Nullable(0)] T>(this T e, double l, double t, double r, double b) where T : FrameworkElement
    {
      e.Margin = new Thickness(l, t, r, b);
      return e;
    }

    // Token: 0x0600078F RID: 1935 RVA: 0x00539384 File Offset: 0x00539384
    public static T Pad<[Nullable(0)] T>(this T e, double all) where T : FrameworkElement
    {
      e.Margin = new Thickness(all);
      return e;
    }

    // Token: 0x06000790 RID: 1936 RVA: 0x00539398 File Offset: 0x00539398
    public static T Size<[Nullable(0)] T>(this T e, double w, double h) where T : FrameworkElement
    {
      if (w > 0.0)
      {
        e.Width = w;
      }
      if (h > 0.0)
      {
        e.Height = h;
      }
      return e;
    }

    // Token: 0x06000791 RID: 1937 RVA: 0x005393CC File Offset: 0x005393CC
    public static T Align<[Nullable(0)] T>(this T e, HorizontalAlignment h, VerticalAlignment v) where T : FrameworkElement
    {
      e.HorizontalAlignment = h;
      e.VerticalAlignment = v;
      return e;
    }

    // Token: 0x06000792 RID: 1938 RVA: 0x005393E8 File Offset: 0x005393E8
    public static T Center<[Nullable(0)] T>(this T e) where T : FrameworkElement
    {
      e.HorizontalAlignment = HorizontalAlignment.Center;
      e.VerticalAlignment = VerticalAlignment.Center;
      return e;
    }

    // Token: 0x06000793 RID: 1939 RVA: 0x00539404 File Offset: 0x00539404
    public static T Tip<[Nullable(0)] T>(this T e, string tip) where T : FrameworkElement
    {
      if (!string.IsNullOrEmpty(tip))
      {
        ToolTipService.SetToolTip(e, tip);
      }
      return e;
    }

    // Token: 0x06000794 RID: 1940 RVA: 0x0053941C File Offset: 0x0053941C
    public static LinearGradientBrush Vertical([TupleElementNames(new string[] { "offset", "color" })] [Nullable(new byte[] { 1, 0 })] params ValueTuple<double, Color>[] stops)
    {
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush
      {
        StartPoint = new Point(0f, 0f),
        EndPoint = new Point(0f, 1f)
      };
      foreach (ValueTuple<double, Color> valueTuple in stops)
      {
        double item = valueTuple.Item1;
        Color item2 = valueTuple.Item2;
        linearGradientBrush.GradientStops.Add(new GradientStop
        {
          Offset = item,
          Color = item2
        });
      }
      return linearGradientBrush;
    }

    // Token: 0x06000795 RID: 1941 RVA: 0x005394A0 File Offset: 0x005394A0
    public static LinearGradientBrush Horizontal([TupleElementNames(new string[] { "offset", "color" })] [Nullable(new byte[] { 1, 0 })] params ValueTuple<double, Color>[] stops)
    {
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush
      {
        StartPoint = new Point(0f, 0f),
        EndPoint = new Point(1f, 0f)
      };
      foreach (ValueTuple<double, Color> valueTuple in stops)
      {
        double item = valueTuple.Item1;
        Color item2 = valueTuple.Item2;
        linearGradientBrush.GradientStops.Add(new GradientStop
        {
          Offset = item,
          Color = item2
        });
      }
      return linearGradientBrush;
    }

    // Token: 0x17000134 RID: 308
    // (get) Token: 0x06000796 RID: 1942 RVA: 0x00539524 File Offset: 0x00539524
    public static Compositor Compositor
    {
      get
      {
        return CompositionTarget.GetCompositorForCurrentThread();
      }
    }

    // Token: 0x06000797 RID: 1943 RVA: 0x0053952C File Offset: 0x0053952C
    public static Grid GlowSpot(Color color, double alpha, double size, double solidUntil = 0.15)
    {
      Grid grid = new Grid();
      grid.Width = size;
      grid.Height = size;
      grid.Margin = new Thickness(-size / 2.0);
      grid.IsHitTestVisible = false;
      grid.HorizontalAlignment = HorizontalAlignment.Center;
      grid.VerticalAlignment = VerticalAlignment.Center;
      Compositor compositor = Ui.Compositor;
      CompositionRadialGradientBrush compositionRadialGradientBrush = compositor.CreateRadialGradientBrush();
      compositionRadialGradientBrush.ColorStops.Add(compositor.CreateColorGradientStop(0f, color.WithAlphaFactor(alpha)));
      compositionRadialGradientBrush.ColorStops.Add(compositor.CreateColorGradientStop((float)solidUntil, color.WithAlphaFactor(alpha)));
      compositionRadialGradientBrush.ColorStops.Add(compositor.CreateColorGradientStop((float)(solidUntil + (1.0 - solidUntil) * 0.45), color.WithAlphaFactor(alpha * 0.4)));
      compositionRadialGradientBrush.ColorStops.Add(compositor.CreateColorGradientStop(1f, color.WithAlphaFactor(0.0)));
      SpriteVisual spriteVisual = compositor.CreateSpriteVisual();
      spriteVisual.Brush = compositionRadialGradientBrush;
      spriteVisual.Size = new Vector2((float)size, (float)size);
      ElementCompositionPreview.SetElementChildVisual(grid, spriteVisual);
      return grid;
    }

    // Token: 0x06000798 RID: 1944 RVA: 0x00539640 File Offset: 0x00539640
    private static Color WithAlphaFactor(this Color c, double a)
    {
      return Color.FromArgb((byte)Math.Round(Math.Clamp(a, 0.0, 1.0) * (double)c.A), c.R, c.G, c.B);
    }

    // Token: 0x06000799 RID: 1945 RVA: 0x00539690 File Offset: 0x00539690
    public static void ClipRounded(FrameworkElement e, double radius)
    {
      Ui.<>c__DisplayClass28_0 CS$<>8__locals1 = new Ui.<>c__DisplayClass28_0();
      CS$<>8__locals1.e = e;
      CS$<>8__locals1.radius = radius;
      Visual elementVisual = ElementCompositionPreview.GetElementVisual(CS$<>8__locals1.e);
      Compositor compositor = elementVisual.Compositor;
      CS$<>8__locals1.geo = compositor.CreateRoundedRectangleGeometry();
      CS$<>8__locals1.geo.CornerRadius = new Vector2((float)CS$<>8__locals1.radius);
      CompositionGeometricClip compositionGeometricClip = compositor.CreateGeometricClip(CS$<>8__locals1.geo);
      elementVisual.Clip = compositionGeometricClip;
      CS$<>8__locals1.e.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        base.<ClipRounded>g__Update|0();
      };
      CS$<>8__locals1.<ClipRounded>g__Update|0();
    }

    // Token: 0x04000247 RID: 583
    private static readonly Dictionary<uint, SolidColorBrush> brushCache = new Dictionary<uint, SolidColorBrush>();

    // Token: 0x04000248 RID: 584
    public static readonly SolidColorBrush Transparent = new SolidColorBrush(Colors.Transparent);

    // Token: 0x04000249 RID: 585
    private static readonly ConditionalWeakTable<Grid, DropShadow> embosses = new ConditionalWeakTable<Grid, DropShadow>();
  }
}
