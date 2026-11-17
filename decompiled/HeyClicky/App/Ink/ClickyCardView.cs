using System;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001A8 RID: 424
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyCardView : Canvas
  {
    // Token: 0x060016ED RID: 5869 RVA: 0x005BCF64 File Offset: 0x005BCF64
    public ClickyCardView(ClickyCardFace face, bool showsSticker = true)
    {
      this.face = face;
      base.Width = ClickyCardView.BoxSize.Width;
      base.Height = ClickyCardView.BoxSize.Height;
      PaperSurface paperSurface = new PaperSurface(ClickyCardView.CardShape, PaperFill.Of(face.Stock.Paper().Brightness(-0.07).Saturation(0.95)), null).WithShadow(PaperColor.Hex("#0A101E").Opacity(0.16), 1.0, 0.0, 1.0);
      this.Place(paperSurface, ClickyCardView.SheetOrigin.X + 2.5, ClickyCardView.SheetOrigin.Y + 2.0, ClickyCardView.SheetSize.Width, ClickyCardView.SheetSize.Height);
      this.Place(this.Front(), ClickyCardView.SheetOrigin.X, ClickyCardView.SheetOrigin.Y, ClickyCardView.SheetSize.Width, ClickyCardView.SheetSize.Height);
      if (showsSticker)
      {
        ClickyPaperStickerKind? sticker = face.Sticker;
        if (sticker != null)
        {
          ClickyPaperStickerKind valueOrDefault = sticker.GetValueOrDefault();
          this.Place(new ClickyPaperSticker(valueOrDefault, 42.0), ClickyCardView.SheetOrigin.X + ClickyCardView.SheetSize.Width - 20.0, ClickyCardView.SheetOrigin.Y - 15.0, double.NaN, double.NaN);
        }
      }
    }

    // Token: 0x060016EE RID: 5870 RVA: 0x005BD130 File Offset: 0x005BD130
    private void Place(FrameworkElement e, double x, double y, double w = double.NaN, double h = double.NaN)
    {
      if (!double.IsNaN(w))
      {
        e.Width = w;
      }
      if (!double.IsNaN(h))
      {
        e.Height = h;
      }
      Canvas.SetLeft(e, x);
      Canvas.SetTop(e, y);
      base.Children.Add(e);
    }

    // Token: 0x060016EF RID: 5871 RVA: 0x005BD170 File Offset: 0x005BD170
    private FrameworkElement Front()
    {
      Color color = this.face.Stock.Ink();
      Grid grid = new Grid();
      PaperSurface paperSurface = new PaperSurface(ClickyCardView.CardShape, PaperFill.Of(this.face.Stock.Paper()), new double?(0.5)).WithWash(PaperFill.Horizontal(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, PaperColor.Black.Opacity(0.14)),
        new ValueTuple<double, Color>(0.38, PaperColor.Black.Opacity(0.04)),
        new ValueTuple<double, Color>(0.68, PaperColor.White.Opacity(0.3)),
        new ValueTuple<double, Color>(1.0, PaperColor.White.Opacity(0.0))
      }), (Size s) => new Rect(0.0, 0.0, 15.0, s.Height), false).WithWash(new PaperFill.Linear(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, PaperColor.White.Opacity(0.22)),
        new ValueTuple<double, Color>(0.45, PaperColor.White.Opacity(0.0)),
        new ValueTuple<double, Color>(0.72, PaperColor.Black.Opacity(0.0)),
        new ValueTuple<double, Color>(1.0, PaperColor.Black.Opacity(0.05))
      }, new Point(0.2, 0.0), new Point(0.8, 1.0)), null, true).WithOutline(new PaperStroke(PaperColor.Black.Opacity(0.07), 0.5, PenLineCap.Flat, PenLineJoin.Miter, null), false)
        .WithShadow(PaperColor.Hex("#0A101E").Opacity(0.18), 1.0, 0.0, 1.0)
        .WithShadow(PaperColor.Hex("#0A1428").Opacity(0.18), 11.0, 0.0, 10.0);
      grid.Children.Add(paperSurface);
      grid.Children.Add(this.Content(color));
      ClickyBrandMark clickyBrandMark = new ClickyBrandMark(21.0, new Color?(color), 2.4)
      {
        Opacity = 0.5,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Bottom,
        Margin = new Thickness(0.0, 0.0, 12.0, 12.0)
      };
      grid.Children.Add(clickyBrandMark);
      return grid;
    }

    // Token: 0x060016F0 RID: 5872 RVA: 0x005BD4A4 File Offset: 0x005BD4A4
    private Canvas Content(Color ink)
    {
      ClickyCardView.<>c__DisplayClass8_0 CS$<>8__locals1;
      CS$<>8__locals1.c = new Canvas
      {
        Width = ClickyCardView.SheetSize.Width,
        Height = ClickyCardView.SheetSize.Height
      };
      Grid grid = new Grid();
      grid.Width = ClickyCardView.SheetSize.Width - 34.0;
      grid.Height = 21.0;
      grid.ColumnSpacing = 7.0;
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid.Children.Add(new ClickyPaperGlyphView(this.face.Glyph, ink, 17.0, 1.8)
      {
        VerticalAlignment = VerticalAlignment.Center
      });
      TextBlock textBlock = PaperText.Line(this.face.Title, ClickyPaperFonts.Hand(21.0), ink, TextAlignment.Left);
      textBlock.Margin = new Thickness(0.0, 1.0, 0.0, 0.0);
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      Grid.SetColumn(textBlock, 1);
      grid.Children.Add(textBlock);
      ClickyCardView.<Content>g__At|8_0(grid, 22.0, 18.0, ref CS$<>8__locals1);
      bool flag = this.face.Headline is ClickyCardFace.CardHeadline.Message;
      string caps = this.face.Caps;
      if (caps != null)
      {
        TextBlock textBlock2 = PaperText.Line(caps.ToUpperInvariant(), ClickyPaperFonts.Typed(9.5, new FontWeight?(FontWeights.SemiBold)), ink.Opacity(0.58), TextAlignment.Left);
        textBlock2.CharacterSpacing = (int)Math.Round(80.0);
        textBlock2.Width = ClickyCardView.SheetSize.Width - 34.0;
        ClickyCardView.<Content>g__At|8_0(textBlock2, 22.0, (double)(flag ? 52 : 50), ref CS$<>8__locals1);
      }
      ValueTuple<FrameworkElement, double, double>? valueTuple = this.Headline(ink);
      if (valueTuple != null)
      {
        ValueTuple<FrameworkElement, double, double> valueOrDefault = valueTuple.GetValueOrDefault();
        ClickyCardView.<Content>g__At|8_0(valueOrDefault.Item1, valueOrDefault.Item2, valueOrDefault.Item3, ref CS$<>8__locals1);
      }
      valueTuple = this.Detail(ink);
      if (valueTuple != null)
      {
        ValueTuple<FrameworkElement, double, double> valueOrDefault2 = valueTuple.GetValueOrDefault();
        ClickyCardView.<Content>g__At|8_0(valueOrDefault2.Item1, valueOrDefault2.Item2, valueOrDefault2.Item3, ref CS$<>8__locals1);
      }
      string foot = this.face.Foot;
      if (foot != null)
      {
        PaperFont paperFont = ClickyPaperFonts.Hand(16.0);
        TextBlock textBlock3 = PaperText.Line(foot, paperFont, this.FootColor, TextAlignment.Left);
        textBlock3.MaxWidth = ClickyCardView.SheetSize.Width - 58.0;
        textBlock3.RenderTransformOrigin = new Point(0f, 1f);
        textBlock3.RenderTransform = new RotateTransform
        {
          Angle = -1.5
        };
        CS$<>8__locals1.c.Children.Add(new ClickyBaselinePlaced(textBlock3, 22.0, ClickyCardView.SheetSize.Height - 17.0, ClickyBaselinePlaced.TextBaseline(paperFont)));
      }
      return CS$<>8__locals1.c;
    }

    // Token: 0x17000411 RID: 1041
    // (get) Token: 0x060016F1 RID: 5873 RVA: 0x005BD800 File Offset: 0x005BD800
    private Color FootColor
    {
      get
      {
        ClickyCardFace.CardFootTone footTone = this.face.FootTone;
        Color color;
        if (footTone != ClickyCardFace.CardFootTone.Deep)
        {
          if (footTone != ClickyCardFace.CardFootTone.Good)
          {
            color = this.face.Stock.Ink().Opacity(0.62);
          }
          else
          {
            color = PaperColor.Hex("#1F7A3E");
          }
        }
        else
        {
          color = this.face.Stock.Deep();
        }
        return color;
      }
    }

    // Token: 0x060016F2 RID: 5874 RVA: 0x005BD864 File Offset: 0x005BD864
    private static TextBlock Tabular(TextBlock t)
    {
      Typography.SetNumeralAlignment(t, FontNumeralAlignment.Tabular);
      return t;
    }

    // Token: 0x060016F3 RID: 5875 RVA: 0x005BD870 File Offset: 0x005BD870
    [return: TupleElementNames(new string[] { "View", "X", "Y" })]
    [return: Nullable(new byte[] { 0, 1 })]
    private ValueTuple<FrameworkElement, double, double>? Headline(Color ink)
    {
      ClickyCardFace.CardHeadline headline = this.face.Headline;
      ClickyCardFace.CardHeadline.Big big = headline as ClickyCardFace.CardHeadline.Big;
      if (big != null)
      {
        TextBlock textBlock = ClickyCardView.Tabular(PaperText.Line(big.Text, ClickyPaperFonts.BigNumber(42.0), ink, TextAlignment.Left));
        textBlock.CharacterSpacing = (int)Math.Round(-33.333333333333336);
        FrameworkElement frameworkElement = Ui.Shrink(textBlock, HorizontalAlignment.Left, 0.6);
        frameworkElement.Width = ClickyCardView.SheetSize.Width - 40.0;
        return new ValueTuple<FrameworkElement, double, double>?(new ValueTuple<FrameworkElement, double, double>(frameworkElement, 20.0, 62.0));
      }
      ClickyCardFace.CardHeadline.Count count = headline as ClickyCardFace.CardHeadline.Count;
      if (count != null)
      {
        PaperFont paperFont = ClickyPaperFonts.BigNumber(42.0);
        PaperFont paperFont2 = ClickyPaperFonts.Hand(19.0);
        TextBlock textBlock2 = ClickyCardView.Tabular(PaperText.Line(count.Number, paperFont, ink, TextAlignment.Left));
        TextBlock textBlock3 = PaperText.Line(count.Word, paperFont2, ink, TextAlignment.Left);
        double num = ClickyHandLineBreaker.Ascent(paperFont);
        double num2 = ClickyHandLineBreaker.Ascent(paperFont2);
        Grid grid = new Grid();
        grid.Width = ClickyCardView.SheetSize.Width - 40.0;
        grid.ColumnSpacing = 8.0;
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = new GridLength(1.0, GridUnitType.Star)
        });
        textBlock2.VerticalAlignment = VerticalAlignment.Top;
        grid.Children.Add(textBlock2);
        FrameworkElement frameworkElement2 = Ui.Shrink(textBlock3, HorizontalAlignment.Left, 0.7);
        frameworkElement2.VerticalAlignment = VerticalAlignment.Top;
        frameworkElement2.Margin = new Thickness(0.0, num - num2, 0.0, 0.0);
        Grid.SetColumn(frameworkElement2, 1);
        grid.Children.Add(frameworkElement2);
        return new ValueTuple<FrameworkElement, double, double>?(new ValueTuple<FrameworkElement, double, double>(grid, 20.0, 62.0));
      }
      ClickyCardFace.CardHeadline.Sentence sentence = headline as ClickyCardFace.CardHeadline.Sentence;
      if (sentence != null)
      {
        PaperFont paperFont3 = ClickyPaperFonts.Typed(15.0, new FontWeight?(FontWeights.SemiBold));
        return new ValueTuple<FrameworkElement, double, double>?(new ValueTuple<FrameworkElement, double, double>(paperFont3.Apply(new TextBlock
        {
          Text = sentence.Text,
          Foreground = Ui.Brush(ink),
          TextWrapping = TextWrapping.WrapWholeWords,
          MaxLines = 2,
          TextTrimming = TextTrimming.CharacterEllipsis,
          Width = 140.0,
          LineStackingStrategy = LineStackingStrategy.BlockLineHeight,
          LineHeight = ClickyHandLineBreaker.NaturalLineHeight(paperFont3) + 1.0
        }), 22.0, 68.0));
      }
      ClickyCardFace.CardHeadline.Message message = headline as ClickyCardFace.CardHeadline.Message;
      if (message == null)
      {
        return null;
      }
      StackPanel stackPanel = new StackPanel();
      stackPanel.Spacing = 0.0;
      PaperFont paperFont4 = ClickyPaperFonts.Typed(12.0, null);
      stackPanel.Children.Add(paperFont4.Apply(new TextBlock
      {
        Text = message.Text,
        Foreground = Ui.Brush(ink.Opacity(0.92)),
        TextWrapping = TextWrapping.WrapWholeWords,
        MaxLines = 2,
        TextTrimming = TextTrimming.CharacterEllipsis,
        Width = 138.0,
        LineStackingStrategy = LineStackingStrategy.BlockLineHeight,
        LineHeight = ClickyHandLineBreaker.NaturalLineHeight(paperFont4) + 2.0
      }));
      TextBlock textBlock4 = PaperText.Line("my draft", ClickyPaperFonts.Hand(14.0), ink.Opacity(0.6), TextAlignment.Left);
      textBlock4.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
      stackPanel.Children.Add(textBlock4);
      PaperFont paperFont5 = ClickyPaperFonts.Hand(19.0);
      stackPanel.Children.Add(paperFont5.Apply(new TextBlock
      {
        Text = message.Draft,
        Foreground = Ui.Brush(ink),
        TextWrapping = TextWrapping.WrapWholeWords,
        MaxLines = 3,
        TextTrimming = TextTrimming.CharacterEllipsis,
        Width = 142.0,
        Margin = new Thickness(0.0, 2.0, 0.0, 0.0),
        LineStackingStrategy = LineStackingStrategy.BlockLineHeight,
        LineHeight = ClickyHandLineBreaker.NaturalLineHeight(paperFont5) - 1.0
      }));
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 3);
      defaultInterpolatedStringHandler.AppendFormatted(message.From);
      defaultInterpolatedStringHandler.AppendLiteral(" wrote: ");
      defaultInterpolatedStringHandler.AppendFormatted(message.Text);
      defaultInterpolatedStringHandler.AppendLiteral(". Draft: ");
      defaultInterpolatedStringHandler.AppendFormatted(message.Draft);
      AutomationProperties.SetName(stackPanel, defaultInterpolatedStringHandler.ToStringAndClear());
      return new ValueTuple<FrameworkElement, double, double>?(new ValueTuple<FrameworkElement, double, double>(stackPanel, 22.0, 70.0));
    }

    // Token: 0x060016F4 RID: 5876 RVA: 0x005BDD7C File Offset: 0x005BDD7C
    [return: TupleElementNames(new string[] { "View", "X", "Y" })]
    [return: Nullable(new byte[] { 0, 1 })]
    private ValueTuple<FrameworkElement, double, double>? Detail(Color ink)
    {
      ClickyCardFace.CardDetail detail = this.face.Detail;
      ClickyCardFace.CardDetail.Sparkline sparkline = detail as ClickyCardFace.CardDetail.Sparkline;
      if (sparkline != null)
      {
        return new ValueTuple<FrameworkElement, double, double>?(new ValueTuple<FrameworkElement, double, double>(new ClickyCardSparkline(sparkline.Values, ink, this.face.Stock.Deep(), sparkline.CircledEnd)
        {
          Width = 136.0,
          Height = 56.0
        }, 22.0, 122.0));
      }
      ClickyCardFace.CardDetail.Rows rows = detail as ClickyCardFace.CardDetail.Rows;
      if (rows != null)
      {
        StackPanel stackPanel = new StackPanel
        {
          Width = 140.0
        };
        PaperFont paperFont = ClickyPaperFonts.Hand(18.0);
        PaperFont paperFont2 = ClickyPaperFonts.Rounded(12.0, FontWeights.SemiBold);
        double num = ClickyHandLineBreaker.Ascent(paperFont);
        double num2 = ClickyHandLineBreaker.Ascent(paperFont2);
        double num3 = Math.Max(num, num2);
        foreach (ValueTuple<ClickyCardFace.CardRow, int> valueTuple in rows.Items.Take<ClickyCardFace.CardRow>(4).Select<ClickyCardFace.CardRow, ValueTuple<ClickyCardFace.CardRow, int>>((ClickyCardFace.CardRow r, int i) => new ValueTuple<ClickyCardFace.CardRow, int>(r, i)))
        {
          ClickyCardFace.CardRow item = valueTuple.Item1;
          int item2 = valueTuple.Item2;
          Grid grid = new Grid
          {
            Height = 27.0
          };
          Grid grid2 = new Grid
          {
            VerticalAlignment = VerticalAlignment.Center
          };
          grid2.ColumnDefinitions.Add(new ColumnDefinition
          {
            Width = new GridLength(16.0)
          });
          grid2.ColumnDefinitions.Add(new ColumnDefinition
          {
            Width = new GridLength(1.0, GridUnitType.Star)
          });
          grid2.ColumnDefinitions.Add(new ColumnDefinition
          {
            Width = GridLength.Auto,
            MinWidth = 6.0
          });
          grid2.ColumnDefinitions.Add(new ColumnDefinition
          {
            Width = GridLength.Auto
          });
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
          defaultInterpolatedStringHandler.AppendFormatted<int>(item2 + 1);
          TextBlock textBlock = PaperText.Line(defaultInterpolatedStringHandler.ToStringAndClear(), paperFont, ink, TextAlignment.Left);
          textBlock.Margin = new Thickness(0.0, num3 - num, 0.0, 0.0);
          TextBlock textBlock2 = PaperText.Line(item.Label, paperFont, ink, TextAlignment.Left);
          textBlock2.Margin = new Thickness(0.0, num3 - num, 0.0, 0.0);
          Grid.SetColumn(textBlock2, 1);
          TextBlock textBlock3 = ClickyCardView.Tabular(PaperText.Line(item.Value, paperFont2, ink, TextAlignment.Left));
          textBlock3.Margin = new Thickness(6.0, num3 - num2, 0.0, 0.0);
          Grid.SetColumn(textBlock3, 3);
          grid2.Children.Add(textBlock);
          grid2.Children.Add(textBlock2);
          grid2.Children.Add(textBlock3);
          grid.Children.Add(grid2);
          stackPanel.Children.Add(grid);
        }
        return new ValueTuple<FrameworkElement, double, double>?(new ValueTuple<FrameworkElement, double, double>(stackPanel, 20.0, 66.0));
      }
      ClickyCardFace.CardDetail.Bullets bullets = detail as ClickyCardFace.CardDetail.Bullets;
      if (bullets != null)
      {
        StackPanel stackPanel2 = new StackPanel
        {
          Width = 140.0
        };
        PaperFont paperFont3 = ClickyPaperFonts.Hand(17.0);
        foreach (string text in bullets.Items.Take<string>(3))
        {
          StackPanel stackPanel3 = new StackPanel
          {
            Orientation = Orientation.Horizontal,
            Spacing = 7.0,
            Height = 24.0
          };
          TextBlock textBlock4 = PaperText.Line("·", paperFont3, this.face.Stock.Deep(), TextAlignment.Left);
          textBlock4.VerticalAlignment = VerticalAlignment.Center;
          stackPanel3.Children.Add(textBlock4);
          TextBlock textBlock5 = PaperText.Line(text, paperFont3, ink, TextAlignment.Left);
          textBlock5.VerticalAlignment = VerticalAlignment.Center;
          stackPanel3.Children.Add(textBlock5);
          stackPanel2.Children.Add(stackPanel3);
        }
        return new ValueTuple<FrameworkElement, double, double>?(new ValueTuple<FrameworkElement, double, double>(stackPanel2, 22.0, 112.0));
      }
      ClickyCardFace.CardDetail.Waiting waiting = detail as ClickyCardFace.CardDetail.Waiting;
      if (waiting == null)
      {
        return null;
      }
      StackPanel stackPanel4 = new StackPanel
      {
        Width = 140.0
      };
      foreach (string text2 in waiting.Names.Take<string>(3))
      {
        StackPanel stackPanel5 = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 7.0,
          Height = 19.0
        };
        stackPanel5.Children.Add(new Ellipse
        {
          Width = 5.0,
          Height = 5.0,
          Fill = Ui.Brush(PaperColor.Hex("#E08A00")),
          VerticalAlignment = VerticalAlignment.Center
        });
        TextBlock textBlock6 = PaperText.Line(text2, ClickyPaperFonts.Typed(11.5, new FontWeight?(FontWeights.Medium)), ink, TextAlignment.Left);
        textBlock6.VerticalAlignment = VerticalAlignment.Center;
        stackPanel5.Children.Add(textBlock6);
        stackPanel4.Children.Add(stackPanel5);
      }
      return new ValueTuple<FrameworkElement, double, double>?(new ValueTuple<FrameworkElement, double, double>(stackPanel4, 22.0, 122.0));
    }

    // Token: 0x060016F6 RID: 5878 RVA: 0x005BE414 File Offset: 0x005BE414
    [CompilerGenerated]
    internal static void <Content>g__At|8_0(FrameworkElement e, double x, double y, ref ClickyCardView.<>c__DisplayClass8_0 A_3)
    {
      Canvas.SetLeft(e, x);
      Canvas.SetTop(e, y);
      A_3.c.Children.Add(e);
    }

    // Token: 0x04000B3B RID: 2875
    public static readonly Size SheetSize = new Size(176f, 232f);

    // Token: 0x04000B3C RID: 2876
    public static readonly Point SheetOrigin = new Point(6f, 16f);

    // Token: 0x04000B3D RID: 2877
    public static readonly Size BoxSize = new Size(ClickyCardView.SheetSize.Width + 30.0, ClickyCardView.SheetSize.Height + 24.0);

    // Token: 0x04000B3E RID: 2878
    private static readonly IClickyShape CardShape = ClickyShape.UnevenRoundedRectangle(2.0, 2.0, 6.0, 6.0);

    // Token: 0x04000B3F RID: 2879
    private readonly ClickyCardFace face;
  }
}
