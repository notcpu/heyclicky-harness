using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Design;
using HeyClicky.App.Dock;
using HeyClicky.App.Home;
using HeyClicky.App.Ink;
using HeyClicky.App.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.UI;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200016A RID: 362
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyDrawingLaptopScreen : Grid
  {
    // Token: 0x060013F6 RID: 5110 RVA: 0x005A6950 File Offset: 0x005A6950
    public ClickyDrawingLaptopScreen(StoredLesson stored, Action<bool> play, [TupleElementNames(new string[] { "Help", "Label", "Perform" })] [Nullable(new byte[] { 0, 1, 1, 1 })] ValueTuple<string, string, Action>? remove = null, bool showsCaptionButtons = true, double? outerCornerRadius = null)
    {
      ClickyDrawingLaptopScreen <>4__this = this;
      ValueTuple<double, double> valueTuple = StickyPreviewLayout.CornerRadii(outerCornerRadius);
      base.Width = 494.0;
      base.Height = 314.0;
      base.Background = Ui.Transparent;
      base.ProtectedCursor = Pressable.ClickableCursor;
      base.Children.Add(new ShadowLayer(valueTuple.Item2, DS.Black(0.35), 20.0, 10.0, 0.0));
      this.bezel = new Border
      {
        CornerRadius = new CornerRadius(valueTuple.Item2),
        Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Rgb(0.16, 0.16, 0.16, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Rgb(0.08, 0.08, 0.08, 1.0))
        }),
        BorderBrush = Ui.Brush(DS.White(0.16)),
        BorderThickness = new Thickness(1.0)
      };
      base.Children.Add(this.bezel);
      Grid grid = new Grid
      {
        Margin = new Thickness(7.0),
        Width = 480.0,
        Height = 300.0
      };
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(34.0)
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      ClickyEdgeStackInkScreenPaper clickyEdgeStackInkScreenPaper = new ClickyEdgeStackInkScreenPaper(ClickyDrawingLaptopScreen.Paper, true);
      Grid.SetRowSpan(clickyEdgeStackInkScreenPaper, 2);
      grid.Children.Add(clickyEdgeStackInkScreenPaper);
      Grid grid2 = new Grid
      {
        Background = Ui.Transparent
      };
      grid2.Children.Add(new Border
      {
        Background = Ui.Horizontal(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Hex(14079447U, 1.0)),
          new ValueTuple<double, Color>(0.5, DS.Hex(15790320U, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Hex(14079447U, 1.0))
        })
      });
      this.wash.Background = Ui.Brush(ClickyDrawingLaptopScreen.Paper.TitleWash.WithAlpha(0.62));
      grid2.Children.Add(this.wash);
      grid2.Children.Add(new Border
      {
        Height = 0.5,
        VerticalAlignment = VerticalAlignment.Bottom,
        Background = Ui.Brush(DS.Black(0.08))
      });
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Top
      };
      if (showsCaptionButtons)
      {
        CaptionButton captionButton = new CaptionButton(CaptionButton.Kind.Maximize, 34.0, ClickyDrawingLaptopScreen.Paper.Ink.WithAlpha(0.8), "Play it in a window");
        captionButton.Click += delegate
        {
          play(false);
        };
        stackPanel.Children.Add(captionButton);
        if (remove != null)
        {
          ValueTuple<string, string, Action> valueOrDefault = remove.GetValueOrDefault();
          CaptionButton captionButton2 = new CaptionButton(CaptionButton.Kind.Close, 34.0, ClickyDrawingLaptopScreen.Paper.Ink.WithAlpha(0.8), valueOrDefault.Item1);
          captionButton2.Click += valueOrDefault.Item3;
          stackPanel.Children.Add(captionButton2);
        }
      }
      grid2.Children.Add(stackPanel);
      this.captionButtons.AddRange(stackPanel.Children.OfType<CaptionButton>());
      double num = (double)stackPanel.Children.Count * 46.0;
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 4.0,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Center,
        Margin = new Thickness(0.0, 0.0, num + (double)((num > 0.0) ? 10 : 15), 0.0)
      };
      if (stored.Data.HasNarration)
      {
        TextBlock textBlock = Icons.Glyph("\ue767", 9.5, ClickyDrawingLaptopScreen.Paper.Ink.WithAlpha(0.55));
        textBlock.FontWeight = FontWeights.SemiBold;
        stackPanel2.Children.Add(textBlock);
      }
      TextBlock textBlock2 = Ui.Text(ClickyDrawingLaptopScreen.LengthText(stored.Data.Duration), 11.0, FontWeights.SemiBold, ClickyDrawingLaptopScreen.Paper.Ink.WithAlpha(0.55), TextAlignment.Left, 0, false, false);
      Typography.SetNumeralAlignment(textBlock2, FontNumeralAlignment.Tabular);
      stackPanel2.Children.Add(textBlock2);
      grid2.Children.Add(stackPanel2);
      TextBlock textBlock3 = Ui.Text(stored.Data.Title, 11.5, FontWeights.SemiBold, ClickyDrawingLaptopScreen.Paper.Ink.WithAlpha(0.85), TextAlignment.Center, 1, false, false);
      double num2 = Math.Max(90.0, num + 60.0);
      textBlock3.Margin = new Thickness(num2, 0.0, num2, 0.0);
      textBlock3.VerticalAlignment = VerticalAlignment.Center;
      textBlock3.IsHitTestVisible = false;
      grid2.Children.Add(textBlock3);
      grid.Children.Add(grid2);
      Pressable pressable = new Pressable(true)
      {
        Background = Ui.Transparent
      };
      if (File.Exists(stored.PosterPath))
      {
        pressable.Children.Add(new Image
        {
          Source = new BitmapImage(new Uri(stored.PosterPath))
          {
            DecodePixelWidth = 960
          },
          Stretch = Stretch.UniformToFill
        });
      }
      else
      {
        TextBlock textBlock4 = Icons.Glyph("\ue70f", 34.0, ClickyDrawingLaptopScreen.Paper.Deep.WithAlpha(0.5));
        textBlock4.FontWeight = FontWeights.Medium;
        pressable.Children.Add(textBlock4);
      }
      this.hoverShade = new Border
      {
        Background = Ui.Brush(DS.Black(0.18)),
        Opacity = 0.0,
        IsHitTestVisible = false
      };
      pressable.Children.Add(this.hoverShade);
      this.playButton = new Grid
      {
        Width = 62.0,
        Height = 62.0,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        Opacity = 0.88,
        IsHitTestVisible = false
      };
      this.playButton.Children.Add(new ShadowLayer(31.0, DS.Black(0.35), 8.0, 3.0, 0.0));
      this.playGel = new GelSurface(GelAppearance.PrimaryBlue, 31.0);
      this.playButton.Children.Add(this.playGel);
      TextBlock textBlock5 = Icons.Glyph("\uf5b0", 22.0, DS.Black(0.82));
      textBlock5.FontWeight = FontWeights.Bold;
      textBlock5.Margin = new Thickness(4.0, 0.0, 0.0, 0.0);
      this.playButton.Children.Add(textBlock5);
      this.playButton.CenterPoint = new Vector3(31f, 31f, 0f);
      pressable.Children.Add(this.playButton);
      Ui.ClipRounded(pressable, 0.0);
      pressable.Click += delegate
      {
        play(false);
      };
      Grid.SetRow(pressable, 1);
      grid.Children.Add(pressable);
      Ui.ClipRounded(grid, valueTuple.Item1);
      base.Children.Add(grid);
      base.PointerEntered += delegate(object _, PointerRoutedEventArgs _)
      {
        <>4__this.SetHovered(true);
      };
      base.PointerExited += delegate(object _, PointerRoutedEventArgs e)
      {
        if (PointerFrames.IsStale(e))
        {
          return;
        }
        <>4__this.SetHovered(false);
      };
      ToolTipService.SetToolTip(this, "Play “" + stored.Data.Title + "” again in a window");
      AutomationProperties.SetName(this, "Drawing: " + stored.Data.Title);
      base.ContextFlyout = ClickyDrawingLaptopScreen.Menu(stored, play, remove);
      this.LoadTintAsync(stored.PosterPath);
    }

    // Token: 0x060013F7 RID: 5111 RVA: 0x005A72B4 File Offset: 0x005A72B4
    private void SetHovered(bool hovered)
    {
      if (this.isHovered == hovered)
      {
        return;
      }
      this.isHovered = hovered;
      CubicBezierEasingFunction cubicBezierEasingFunction = Easing.EaseOut(Ui.Compositor);
      this.bezel.BorderBrush = Ui.Brush(DS.White(hovered ? 0.26 : 0.16));
      Anim.Opacity(this.hoverShade, hovered > false, 0.16, cubicBezierEasingFunction, 0.0);
      Anim.Opacity(this.playButton, hovered ? 1f : 0.88f, 0.16, cubicBezierEasingFunction, 0.0);
      Anim.Scale(this.playButton, (hovered && !Anim.ReduceMotion) ? 1.08f : 1f, 0.16, cubicBezierEasingFunction, new Vector3?(new Vector3(31f, 31f, 0f)));
      this.playGel.Hovered = hovered;
    }

    // Token: 0x060013F8 RID: 5112 RVA: 0x005A73AC File Offset: 0x005A73AC
    private static MenuFlyout Menu(StoredLesson stored, Action<bool> play, [TupleElementNames(new string[] { "Help", "Label", "Perform" })] [Nullable(new byte[] { 0, 1, 1, 1 })] ValueTuple<string, string, Action>? remove)
    {
      ClickyDrawingLaptopScreen.<>c__DisplayClass11_0 CS$<>8__locals1 = new ClickyDrawingLaptopScreen.<>c__DisplayClass11_0();
      CS$<>8__locals1.play = play;
      CS$<>8__locals1.stored = stored;
      CS$<>8__locals1.menu = new MenuFlyout();
      CS$<>8__locals1.<Menu>g__Item|0("Play again", delegate
      {
        CS$<>8__locals1.play(false);
      });
      CS$<>8__locals1.<Menu>g__Item|0("Play on a white board", delegate
      {
        CS$<>8__locals1.play(true);
      });
      CS$<>8__locals1.<Menu>g__Item|0("Show in Explorer", delegate
      {
        ChatBlockViews.RevealPath(CS$<>8__locals1.stored.Folder);
      });
      if (remove != null)
      {
        ValueTuple<string, string, Action> valueOrDefault = remove.GetValueOrDefault();
        CS$<>8__locals1.menu.Items.Add(new MenuFlyoutSeparator());
        CS$<>8__locals1.<Menu>g__Item|0(valueOrDefault.Item2, valueOrDefault.Item3);
      }
      return CS$<>8__locals1.menu;
    }

    // Token: 0x060013F9 RID: 5113 RVA: 0x005A745C File Offset: 0x005A745C
    public static string LengthText(double duration)
    {
      int num = (int)Math.Round(duration, MidpointRounding.AwayFromZero);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
      defaultInterpolatedStringHandler.AppendFormatted<int>(num / 60);
      defaultInterpolatedStringHandler.AppendLiteral(":");
      defaultInterpolatedStringHandler.AppendFormatted<int>(num % 60, "00");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x060013FA RID: 5114 RVA: 0x005A74AC File Offset: 0x005A74AC
    private async Task LoadTintAsync(string posterPath)
    {
      Color? color = await ClickyDrawingLaptopScreen.Tint(posterPath);
      if (color != null)
      {
        Color valueOrDefault = color.GetValueOrDefault();
        this.wash.Background = Ui.Brush(valueOrDefault.WithAlpha(0.62));
      }
    }

    // Token: 0x060013FB RID: 5115 RVA: 0x005A74F8 File Offset: 0x005A74F8
    public static async Task<Color?> Tint(string posterPath)
    {
      Color? color;
      Color? color2;
      if (ClickyDrawingLaptopScreen.TintCache.TryGetValue(posterPath, out color))
      {
        color2 = color;
      }
      else
      {
        Color? tint = null;
        try
        {
          if (File.Exists(posterPath))
          {
            ClickyDrawingLaptopScreen.<>c__DisplayClass15_0 CS$<>8__locals1 = new ClickyDrawingLaptopScreen.<>c__DisplayClass15_0();
            byte[] array = await File.ReadAllBytesAsync(posterPath, default(CancellationToken));
            CS$<>8__locals1.bytes = array;
            tint = await Task.Run<Color?>(delegate
            {
              ClickyDrawingLaptopScreen.<>c__DisplayClass15_0.<<Tint>b__0>d <<Tint>b__0>d;
              <<Tint>b__0>d.<>t__builder = AsyncTaskMethodBuilder<Color?>.Create();
              <<Tint>b__0>d.<>4__this = CS$<>8__locals1;
              <<Tint>b__0>d.<>1__state = -1;
              <<Tint>b__0>d.<>t__builder.Start<ClickyDrawingLaptopScreen.<>c__DisplayClass15_0.<<Tint>b__0>d>(ref <<Tint>b__0>d);
              return <<Tint>b__0>d.<>t__builder.Task;
            });
            CS$<>8__locals1 = null;
          }
        }
        catch (Exception ex)
        {
          Log.Warn("notch-chat", "drawing tint: " + ex.Message);
        }
        ClickyDrawingLaptopScreen.TintCache[posterPath] = tint;
        color2 = tint;
      }
      return color2;
    }

    // Token: 0x0400095B RID: 2395
    public static readonly ClickyHomeSpreadPaper Paper = ClickyHomeSpreadPaper.Of(ClickyPaperStock.Blush);

    // Token: 0x0400095C RID: 2396
    private const double TitleBarHeight = 34.0;

    // Token: 0x0400095D RID: 2397
    private readonly Border bezel;

    // Token: 0x0400095E RID: 2398
    private readonly Border wash = new Border();

    // Token: 0x0400095F RID: 2399
    private readonly Border hoverShade;

    // Token: 0x04000960 RID: 2400
    private readonly Grid playButton;

    // Token: 0x04000961 RID: 2401
    private readonly GelSurface playGel;

    // Token: 0x04000962 RID: 2402
    private readonly List<CaptionButton> captionButtons = new List<CaptionButton>();

    // Token: 0x04000963 RID: 2403
    private bool isHovered;

    // Token: 0x04000964 RID: 2404
    private static readonly Dictionary<string, Color?> TintCache = new Dictionary<string, Color?>(StringComparer.OrdinalIgnoreCase);
  }
}
