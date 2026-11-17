using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020002A6 RID: 678
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkTile
  {
    // Token: 0x06001BDA RID: 7130 RVA: 0x005F5A68 File Offset: 0x005F5A68
    public static string StateWord(string word)
    {
      return ClickyInkDashboardViewMath.StateWord(word);
    }

    // Token: 0x06001BDB RID: 7131 RVA: 0x005F5A70 File Offset: 0x005F5A70
    public static bool StacksChange(ClickyInkDocument.Block block, double? width)
    {
      if (width != null)
      {
        double valueOrDefault = width.GetValueOrDefault();
        ValueTuple<string, double, string>? valueTuple = ClickyInkDashboardViewMath.TileChangeMeasure(block);
        if (valueTuple != null)
        {
          ValueTuple<string, double, string> valueOrDefault2 = valueTuple.GetValueOrDefault();
          double num = ClickyInkTile.MeasuredWidth(valueOrDefault2.Item1, ClickyPaperFonts.Rounded(valueOrDefault2.Item2, FontWeights.Bold));
          double num2 = ClickyInkTile.MeasuredWidth(valueOrDefault2.Item3, ClickyPaperFonts.Rounded(11.5, FontWeights.SemiBold));
          return ClickyInkDashboardViewMath.TileStacksChange(num, valueOrDefault2.Item1.CharacterCount(), num2, valueOrDefault);
        }
      }
      return false;
    }

    // Token: 0x06001BDC RID: 7132 RVA: 0x005F5AF8 File Offset: 0x005F5AF8
    private static double MeasuredWidth(string text, PaperFont font)
    {
      TextBlock textBlock = font.Apply(new TextBlock
      {
        Text = text,
        TextWrapping = TextWrapping.NoWrap
      });
      textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      return Math.Ceiling(textBlock.DesiredSize.Width);
    }

    // Token: 0x06001BDD RID: 7133 RVA: 0x005F5B50 File Offset: 0x005F5B50
    public static double Height(ClickyInkDocument.Block block, double? width = null, bool compact = false)
    {
      return ClickyInkDashboardViewMath.TileHeight(block, ClickyInkTile.StacksChange(block, width), compact);
    }

    // Token: 0x06001BDE RID: 7134 RVA: 0x005F5B60 File Offset: 0x005F5B60
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, int index = 0, ClickyInkTiles.Look look = ClickyInkTiles.Look.Ruled, double? width = null)
    {
      bool isWindowPreview = ClickyInkEnvironment.Current.IsWindowPreview;
      ClickyInkDocument.Block source = block.Source;
      double num = ClickyInkClock.Progress(clock, block.LandsAt, block.LandsAt + 0.55);
      double? goneAt = block.GoneAt;
      double num2;
      if (goneAt != null)
      {
        double valueOrDefault = goneAt.GetValueOrDefault();
        num2 = ClickyInkClock.Progress(clock, valueOrDefault, valueOrDefault + 0.48);
      }
      else
      {
        num2 = 0.0;
      }
      double num3 = num2;
      double num4 = 0.25 + (double)index * 0.22;
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 3.0
      };
      if (source.Kind != ClickyInkDocument.Kind.Paragraph)
      {
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(6.0, InkVerticalAlignment.Center, Array.Empty<UIElement>());
        string text = ClickyInkIcon.SymbolName(source["icon"], source.Text, true);
        if (text != null)
        {
          TextBlock textBlock = ClickyInkIcon.Glyph(text, 10.0, style.Quiet, true);
          if (textBlock != null)
          {
            clickyInkHStack.Children.Add(textBlock);
          }
        }
        clickyInkHStack.Children.Add(ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Typed(9.5, new FontWeight?(FontWeights.Bold)), false, new Color?(style.Quiet), null).With(delegate(InkTextView t)
        {
          t.Tracking = 0.7;
          t.Uppercase = true;
          t.LineLimit = new int?(1);
          t.MinimumScale = 0.75;
        }));
        clickyInkHStack.Children.Add(new ClickyInkSpacer());
        if (source.Kind == ClickyInkDocument.Kind.Status)
        {
          clickyInkHStack.Children.Add(ClickyInkTile.Lamp(block, clock, num4, 16.0));
        }
        stackPanel.Children.Add(clickyInkHStack);
      }
      foreach (UIElement uielement in ClickyInkTile.Content(block, clock, style, index, look, width, num4))
      {
        stackPanel.Children.Add(uielement);
      }
      Grid grid = new Grid
      {
        Padding = new Thickness(12.0, (double)(isWindowPreview ? 5 : 10), 12.0, (double)(isWindowPreview ? 2 : 9))
      };
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid.Children.Add(stackPanel);
      string text2 = source["note"];
      if (text2 != null && text2.Length > 0 && source.Kind != ClickyInkDocument.Kind.Paragraph)
      {
        int? num5 = ClickyInkChange.ChangedEdit(source);
        bool flag;
        if (num5 != null)
        {
          int valueOrDefault2 = num5.GetValueOrDefault();
          int num6 = valueOrDefault2;
          num5 = style.LatestEdit;
          flag = (num6 == num5.GetValueOrDefault()) & (num5 != null);
        }
        else
        {
          flag = false;
        }
        bool flag2 = flag;
        InkTextView inkTextView = InkTextView.Plain(text2, ClickyPaperFonts.Hand(14.0), flag2 ? style.Fresh : style.Quiet).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
          t.MinimumScale = 0.8;
        });
        inkTextView.Opacity = ClickyInkClock.Progress(clock, block.Words.FinishedAt + 0.1, block.Words.FinishedAt + 0.4);
        inkTextView.Margin = new Thickness(0.0, 6.0, 0.0, 0.0);
        Grid.SetRow(inkTextView, 2);
        grid.Children.Add(inkTextView);
      }
      grid.Opacity = ((clock >= block.LandsAt) ? (Math.Min(1.0, num * 2.0) * (1.0 - 0.45 * num3)) : 0.0);
      grid.Transformed(0.0, -5.0 * (1.0 - num), 0.0, 1.0, null);
      if (num3 <= 0.0)
      {
        return grid;
      }
      ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyInkCrossOut(block.Id.CharacterCount()).Trim(0.0, num3), new PaperStroke(style.Fresh.Opacity(0.85), 2.0, PenLineCap.Round, PenLineJoin.Miter, null));
      clickyShapeView.Margin = new Thickness(14.0);
      return new ClickyInkOverlay(grid, new UIElement[] { clickyShapeView });
    }

    // Token: 0x06001BDF RID: 7135 RVA: 0x005F605C File Offset: 0x005F605C
    private static FrameworkElement Lamp(ClickyInkLiveNote.Block block, double clock, double delay, double diameter)
    {
      ClickyInkDocument.Block source = block.Source;
      string text;
      if ((text = source["state"]) == null)
      {
        text = source["status"] ?? source["value"];
      }
      ClickyInkStatusState clickyInkStatusState = ClickyInkStatus.State(text);
      string text2 = ClickyInkChange.Previous(source, "state");
      ClickyInkStatusState? clickyInkStatusState2 = ((text2 != null) ? new ClickyInkStatusState?(ClickyInkStatus.State(text2)) : null);
      return ClickyInkLamp.Create(clickyInkStatusState, clickyInkStatusState2, ClickyInkChange.Progress(source, clock, delay, 0.8), diameter, block.Id.CharacterCount(), ClickyInkClock.Progress(clock, block.LandsAt + 0.1, block.LandsAt + 0.6));
    }

    // Token: 0x06001BE0 RID: 7136 RVA: 0x005F6110 File Offset: 0x005F6110
    private static FrameworkElement NumberAndChange(bool stacksChange, double spacing, [Nullable(new byte[] { 1, 2 })] UIElement[] number, [Nullable(2)] UIElement change)
    {
      if (stacksChange)
      {
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 1.0,
          HorizontalAlignment = HorizontalAlignment.Left,
          Children = { InkDashboardLayout.HStack(spacing, InkVerticalAlignment.FirstBaseline, number) }
        };
        if (change != null)
        {
          stackPanel.Children.Add(change);
        }
        return stackPanel;
      }
      InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.FirstBaseline;
      int num = 0;
      UIElement[] array = new UIElement[1 + number.Length];
      ReadOnlySpan<UIElement> readOnlySpan = new ReadOnlySpan<UIElement>(number);
      readOnlySpan.CopyTo(new Span<UIElement>(array).Slice(num, readOnlySpan.Length));
      num += readOnlySpan.Length;
      array[num] = change;
      ClickyInkHStack clickyInkHStack = InkDashboardLayout.HStack(spacing, inkVerticalAlignment, array);
      clickyInkHStack.HorizontalAlignment = HorizontalAlignment.Left;
      return clickyInkHStack;
    }

    // Token: 0x06001BE1 RID: 7137 RVA: 0x005F61B4 File Offset: 0x005F61B4
    private static List<UIElement> Content(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, int index, ClickyInkTiles.Look look, double? width, double delay)
    {
      ClickyInkDocument.Block source = block.Source;
      bool flag = ClickyInkChange.IsLatest(source, style.LatestEdit);
      bool flag2 = ClickyInkChange.Animates(source, style.LatestEdit);
      bool flag3 = ClickyInkTile.StacksChange(source, width);
      double num = ClickyInkClock.Progress(clock, block.LandsAt + 0.15, block.LandsAt + 0.9);
      int num2 = block.Id.CharacterCount();
      List<UIElement> list = new List<UIElement>();
      ClickyInkDocument.Kind kind = source.Kind;
      if (kind != ClickyInkDocument.Kind.Figure)
      {
        switch (kind)
        {
        case ClickyInkDocument.Kind.Spark:
        {
          ClickyInkSparkReading clickyInkSparkReading = new ClickyInkSparkReading(source);
          List<UIElement> list2 = list;
          bool flag4 = flag3;
          double num3 = 5.0;
          UIElement[] array = new UIElement[2];
          array[0] = ClickyInkTickingNumber.Create(source, "values", clock, style, 26.0, delay, clickyInkSparkReading.Last, clickyInkSparkReading.PreviousLast, true);
          int num4 = 1;
          string text = source["unit"];
          UIElement uielement;
          if (text == null || text.Length <= 0)
          {
            uielement = null;
          }
          else
          {
            uielement = InkDashboardLayout.Fixed(InkTextView.Plain(text, ClickyPaperFonts.Hand(14.0), style.Quiet).With(delegate(InkTextView t)
            {
              t.LineLimit = new int?(1);
            }));
          }
          array[num4] = uielement;
          list2.Add(ClickyInkTile.NumberAndChange(flag4, num3, array, ClickyInkDeltaTag.Create(source, clock, source["delta"], clickyInkSparkReading.Step, 11.5, "", flag3)));
          List<UIElement> list3 = list;
          IReadOnlyList<double> values = clickyInkSparkReading.Values;
          IReadOnlyList<double> readOnlyList = (flag2 ? clickyInkSparkReading.PreviousValues : null);
          double num5 = ClickyInkChange.Progress(source, clock, delay, 0.9);
          double num6 = num;
          string text2 = source["goal"];
          ClickyInkNumber? clickyInkNumber;
          double? num7 = ((text2 != null) ? ((ClickyInkNumber.Parse(text2) != null) ? new double?(clickyInkNumber.GetValueOrDefault().Value) : null) : null);
          ClickyInkStyle style2 = style;
          int num8 = num2;
          bool flag5 = flag;
          double? num9 = new double?((double)30);
          list3.Add(ClickyInkSparkLine.Create(values, readOnlyList, num5, num6, num7, style2, null, num8, flag5, null, num9));
          break;
        }
        case ClickyInkDocument.Kind.Status:
        {
          string text3;
          if ((text3 = source["state"]) == null)
          {
            text3 = source["status"] ?? source["value"];
          }
          ClickyInkStatusState clickyInkStatusState = ClickyInkStatus.State(text3);
          string text4 = ClickyInkChange.Previous(source, "state");
          ClickyInkStatusState? clickyInkStatusState2 = ((text4 != null) ? new ClickyInkStatusState?(ClickyInkStatus.State(text4)) : null);
          bool flag6;
          if (clickyInkStatusState2 != null)
          {
            ClickyInkStatusState valueOrDefault = clickyInkStatusState2.GetValueOrDefault();
            if (valueOrDefault != clickyInkStatusState)
            {
              int? num10 = ClickyInkChange.ChangedEdit(source);
              int? num11 = style.LatestEdit;
              if ((num10.GetValueOrDefault() == num11.GetValueOrDefault()) & (num10 != null == (num11 != null)))
              {
                flag6 = style.LatestEdit != null;
                goto IL_058E;
              }
            }
          }
          flag6 = false;
          IL_058E:
          bool flag7 = flag6;
          InkTextView inkTextView = InkTextView.Plain(ClickyInkTile.StateWord(ClickyInkStatus.Word(source["state"] ?? source["status"], clickyInkStatusState)), ClickyPaperFonts.BigNumber(24.0), ClickyInkDashboardPalette.Word(clickyInkStatusState)).With(delegate(InkTextView t)
          {
            t.Tracking = -0.5;
            t.LineLimit = new int?(1);
            t.MinimumScale = 0.6;
          });
          FrameworkElement frameworkElement = inkTextView;
          if (flag7)
          {
            ClickyShapeView clickyShapeView = ClickyShapeView.Fill(ClickyShape.RoundedRectangle(3.0), style.Highlighter);
            clickyShapeView.Margin = new Thickness(-3.0, 2.0, -3.0, 2.0);
            frameworkElement = new ClickyInkOverlay(inkTextView, Array.Empty<UIElement>()).Behind(clickyShapeView);
          }
          ClickyInkHStack clickyInkHStack = InkDashboardLayout.HStack(6.0, InkVerticalAlignment.FirstBaseline, new UIElement[] { frameworkElement });
          if (flag7 && clickyInkStatusState2 != null)
          {
            ClickyInkStatusState valueOrDefault2 = clickyInkStatusState2.GetValueOrDefault();
            clickyInkHStack.Children.Add(InkDashboardLayout.Fixed(InkTextView.Plain(ClickyInkTile.StateWord(ClickyInkStatus.Word(text4, valueOrDefault2)), ClickyPaperFonts.Rounded(12.0, FontWeights.SemiBold), style.Quiet).With(delegate(InkTextView t)
            {
              t.Strikethrough = new Color?(style.Fresh);
            })));
          }
          clickyInkHStack.HorizontalAlignment = HorizontalAlignment.Left;
          list.Add(clickyInkHStack);
          break;
        }
        case ClickyInkDocument.Kind.Progress:
        {
          ClickyInkProgress clickyInkProgress = new ClickyInkProgress(source.Attributes);
          double? num9;
          double? num12;
          if (!flag)
          {
            num9 = null;
            num12 = num9;
          }
          else
          {
            num12 = ClickyInkProgressView.PreviousFraction(source);
          }
          double? num13 = num12;
          list.Add(ClickyInkTickingNumber.Create(source, ClickyInkProgressView.ValueKey(source), clock, style, 24.0, delay, clickyInkProgress.Shown, (num13 == null) ? null : ClickyInkProgressView.PreviousShown(source), false));
          double fill = clickyInkProgress.Fill;
          double? num14 = num13;
          double num15 = ClickyInkChange.Progress(source, clock, delay, 0.9);
          double num16 = num;
          double num17 = 11.0;
          ClickyInkStyle style3 = style;
          int num18 = num2;
          num9 = null;
          FrameworkElement frameworkElement2 = ClickyInkProgressBar.Create(fill, num14, num15, num16, num17, style3, num18, num9);
          frameworkElement2.Margin = new Thickness(0.0, 4.0, 0.0, 0.0);
          list.Add(frameworkElement2);
          break;
        }
        case ClickyInkDocument.Kind.Tally:
        {
          ClickyInkTallyReading clickyInkTallyReading = new ClickyInkTallyReading(source, style.LatestEdit);
          double num19 = 4.0;
          InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.FirstBaseline;
          UIElement[] array2 = new UIElement[2];
          array2[0] = ClickyInkTickingNumber.Create(source, clickyInkTallyReading.Key, clock, style, 26.0, delay, null, null, true);
          int num20 = 1;
          string text5 = source["goal"] ?? source["of"];
          array2[num20] = ((text5 != null) ? InkDashboardLayout.Fixed(InkTextView.Plain("of " + text5, ClickyPaperFonts.Hand(14.0), style.Quiet)) : null);
          ClickyInkHStack clickyInkHStack2 = InkDashboardLayout.HStack(num19, inkVerticalAlignment, array2);
          clickyInkHStack2.HorizontalAlignment = HorizontalAlignment.Left;
          list.Add(clickyInkHStack2);
          if (clickyInkTallyReading.Count >= 5L)
          {
            int num21 = (int)Math.Min(2147483647L, clickyInkTallyReading.Fresh);
            List<UIElement> list4 = list;
            int num22 = (int)Math.Min(2147483647L, clickyInkTallyReading.Count);
            int num23 = num21;
            double num24 = ClickyInkChange.Progress(source, clock, delay, Math.Max(0.6, (double)num21 * 0.15));
            double num25 = num;
            double num26 = 15.0;
            ClickyInkStyle style4 = style;
            int num27 = num2;
            double? num9 = new double?((double)38);
            list4.Add(ClickyInkTallyMarks.Create(num22, num23, num24, num25, num26, style4, num27, null, num9));
          }
          break;
        }
        case ClickyInkDocument.Kind.Streak:
        {
          ClickyInkStreakReading clickyInkStreakReading = new ClickyInkStreakReading(source);
          double num28 = 5.0;
          InkVerticalAlignment inkVerticalAlignment2 = InkVerticalAlignment.FirstBaseline;
          UIElement[] array3 = new UIElement[2];
          array3[0] = ClickyInkTickingNumber.Create(source, "_streak", clock, style, 26.0, delay, clickyInkStreakReading.Shown, clickyInkStreakReading.PreviousShown, false);
          array3[1] = InkTextView.Plain(clickyInkStreakReading.Unit, ClickyPaperFonts.Hand(14.0), style.Quiet).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(1);
          });
          ClickyInkHStack clickyInkHStack3 = InkDashboardLayout.HStack(num28, inkVerticalAlignment2, array3);
          clickyInkHStack3.HorizontalAlignment = HorizontalAlignment.Left;
          list.Add(clickyInkHStack3);
          IReadOnlyList<ClickyInkStreakDay> readOnlyList2 = clickyInkStreakReading.Days.Skip<ClickyInkStreakDay>(Math.Max(0, clickyInkStreakReading.Days.Count - 14)).ToList<ClickyInkStreakDay>();
          int? num29;
          if (flag)
          {
            int? num11 = clickyInkStreakReading.PreviousCount;
            if (num11 != null)
            {
              int valueOrDefault3 = num11.GetValueOrDefault();
              num29 = new int?(Math.Max(0, valueOrDefault3 - Math.Max(0, clickyInkStreakReading.Days.Count - 14)));
              goto IL_0A49;
            }
          }
          num29 = null;
          IL_0A49:
          int? num30 = num29;
          FrameworkElement frameworkElement3 = ClickyInkStreakBoxes.Create(readOnlyList2, num30, ClickyInkChange.Progress(source, clock, delay, 0.6), num, 10.0, 14, style, num2);
          frameworkElement3.Margin = new Thickness(0.0, 3.0, 0.0, 0.0);
          list.Add(frameworkElement3);
          break;
        }
        case ClickyInkDocument.Kind.Countdown:
        {
          ClickyInkCountdownReading clickyInkCountdownReading = new ClickyInkCountdownReading(source);
          double num31 = 5.0;
          InkVerticalAlignment inkVerticalAlignment3 = InkVerticalAlignment.FirstBaseline;
          UIElement[] array4 = new UIElement[3];
          array4[0] = InkDashboardLayout.Fixed(InkTextView.Plain((clickyInkCountdownReading.Big.Length == 0) ? "–" : clickyInkCountdownReading.Big, ClickyPaperFonts.BigNumber(28.0), ClickyInkCountdown.Color(clickyInkCountdownReading, style)).With(delegate(InkTextView t)
          {
            t.MonospacedDigit = true;
          }));
          array4[1] = InkTextView.Plain(clickyInkCountdownReading.Unit, ClickyPaperFonts.Hand(15.0), style.Quiet).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(1);
          });
          array4[2] = (clickyInkCountdownReading.IsPast ? InkDashboardLayout.Fixed(ClickyInkTag.Make("past", ClickyInkDashboardPalette.Word(ClickyInkStatusState.Bad))) : null);
          ClickyInkHStack clickyInkHStack4 = InkDashboardLayout.HStack(num31, inkVerticalAlignment3, array4);
          clickyInkHStack4.HorizontalAlignment = HorizontalAlignment.Left;
          list.Add(clickyInkHStack4);
          string dateLine = clickyInkCountdownReading.DateLine;
          if (dateLine != null)
          {
            list.Add(InkTextView.Plain(dateLine, ClickyPaperFonts.Typed(10.5, new FontWeight?(FontWeights.Medium)), style.Quiet).With(delegate(InkTextView t)
            {
              t.LineLimit = new int?(1);
              t.MinimumScale = 0.8;
            }));
          }
          break;
        }
        default:
          list.Add(ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand(16.0), true, null, null).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(4);
          }));
          break;
        }
      }
      else
      {
        string text6 = ((source["value"] != null) ? "value" : "big");
        ValueTuple<string, double>? valueTuple = (flag ? ClickyInkNumber.Difference(ClickyInkChange.Previous(source, text6), source[text6]) : null);
        FrameworkElement frameworkElement4 = ClickyInkTile.NumberAndChange(flag3, 6.0, new UIElement[] { ClickyInkTickingNumber.Create(source, text6, clock, style, 28.0, delay, null, null, true) }, ClickyInkDeltaTag.Create(source, clock, source["delta"], valueTuple, 11.5, "", flag3));
        if (look == ClickyInkTiles.Look.Tape)
        {
          frameworkElement4.Margin = new Thickness(12.0, 3.0, 12.0, 3.0);
          ClickyShapeView clickyShapeView2 = ClickyShapeView.Fill(new ClickyInkTornTape(3.5), ClickyInkTile.TapeColors[index % ClickyInkTile.TapeColors.Length].Opacity(0.82));
          ClickyShapeView clickyShapeView3 = ClickyShapeView.Fill(new ClickyInkTornTape(3.5), PaperColor.Black.Opacity(0.08));
          clickyShapeView3.Transformed(0.0, 1.0, 0.0, 1.0, null);
          ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(frameworkElement4, Array.Empty<UIElement>()).Behind(clickyShapeView2).Behind(clickyShapeView3);
          clickyShapeView2.Opacity = num;
          clickyShapeView3.Opacity = num;
          clickyInkOverlay.HorizontalAlignment = HorizontalAlignment.Left;
          clickyInkOverlay.Transformed(0.0, 0.0, ClickyInkTile.TapeTilt[index % ClickyInkTile.TapeTilt.Length], 1.0, null);
          frameworkElement4 = clickyInkOverlay;
        }
        list.Add(frameworkElement4);
        if (source["values"] != null)
        {
          ClickyInkSparkReading clickyInkSparkReading2 = new ClickyInkSparkReading(source);
          List<UIElement> list5 = list;
          IReadOnlyList<double> values2 = clickyInkSparkReading2.Values;
          IReadOnlyList<double> readOnlyList3 = (flag2 ? clickyInkSparkReading2.PreviousValues : null);
          double num32 = ClickyInkChange.Progress(source, clock, delay, 0.9);
          double num33 = num;
          ClickyInkStyle style5 = style;
          int num8 = num2;
          bool flag5 = flag;
          double? num9 = new double?((double)26);
          list5.Add(ClickyInkSparkLine.Create(values2, readOnlyList3, num32, num33, null, style5, null, num8, flag5, null, num9));
        }
      }
      return list;
    }

    // Token: 0x04000D64 RID: 3428
    private static readonly Color[] TapeColors = new Color[]
    {
      PaperColor.Hex("#F6C6CF"),
      PaperColor.Hex("#FFE07D"),
      PaperColor.Hex("#C3D0FF")
    };

    // Token: 0x04000D65 RID: 3429
    private static readonly double[] TapeTilt = new double[] { -1.6, 1.1, -0.8 };
  }
}
