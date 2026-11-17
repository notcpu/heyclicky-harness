using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001C0 RID: 448
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyNoteView : Canvas
  {
    // Token: 0x1700043A RID: 1082
    // (get) Token: 0x0600179D RID: 6045 RVA: 0x005C2D28 File Offset: 0x005C2D28
    public static Size BoxSize
    {
      get
      {
        return new Size(ClickyNoteView.SheetSize.Width + ClickyNoteView.ShadowInsets.Left + ClickyNoteView.ShadowInsets.Right, ClickyNoteView.SheetSize.Height + ClickyNoteView.ShadowInsets.Top + ClickyNoteView.ShadowInsets.Bottom);
      }
    }

    // Token: 0x0600179E RID: 6046 RVA: 0x005C2D8C File Offset: 0x005C2D8C
    public ClickyNoteView(ClickyNoteFace face, [Nullable(new byte[] { 2, 1 })] Action<string> onAction = null)
    {
      this.face = face;
      Action<string> action = onAction;
      if (onAction == null && (action = ClickyNoteView.<>c.<>9__9_0) == null)
      {
        action = (ClickyNoteView.<>c.<>9__9_0 = delegate(string _)
        {
        });
      }
      this.onAction = action;
      base.Width = ClickyNoteView.BoxSize.Width;
      base.Height = ClickyNoteView.BoxSize.Height;
      Canvas.SetLeft(this.sheet, ClickyNoteView.ShadowInsets.Left);
      Canvas.SetTop(this.sheet, ClickyNoteView.ShadowInsets.Top);
      base.Children.Add(this.sheet);
      this.Paper();
      this.Content();
    }

    // Token: 0x0600179F RID: 6047 RVA: 0x005C2E74 File Offset: 0x005C2E74
    private void At(FrameworkElement e, double x, double y)
    {
      Canvas.SetLeft(e, x);
      Canvas.SetTop(e, y);
      this.sheet.Children.Add(e);
    }

    // Token: 0x060017A0 RID: 6048 RVA: 0x005C2E98 File Offset: 0x005C2E98
    private void Paper()
    {
      ClickyNoteShape clickyNoteShape = new ClickyNoteShape(!this.face.IsIdea, 10.0, 2.7, 0.8, 3.0);
      PaperFill paperFill = (this.face.IsIdea ? PaperFill.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, PaperColor.Hex("#FFFDF6")),
        new ValueTuple<double, Color>(1.0, PaperColor.Hex("#FCF8EE"))
      }) : PaperFill.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, PaperColor.Hex("#FFFCF3")),
        new ValueTuple<double, Color>(0.4, PaperColor.Hex("#FBF7EC")),
        new ValueTuple<double, Color>(1.0, PaperColor.Hex("#F7F2E4"))
      }));
      PaperSurface paperSurface = new PaperSurface(clickyNoteShape, paperFill, new double?(this.face.IsIdea ? 0.35 : 0.42))
      {
        Width = ClickyNoteView.SheetSize.Width,
        Height = ClickyNoteView.SheetSize.Height
      }.WithShadow(PaperColor.Hex("#0A101E").Opacity(this.face.IsIdea ? 0.12 : 0.16), 0.5, 0.0, 1.0).WithShadow(PaperColor.Hex("#0A1428").Opacity(this.face.IsIdea ? 0.12 : 0.15), 6.0, 0.0, 7.0);
      if (!this.face.IsIdea)
      {
        paperSurface.WithWash(PaperFill.Evenly(new Point(0.5, 0.0), new Point(0.5, 1.0), new Color[]
        {
          PaperColor.Hex("#78643C").Opacity(0.08),
          PaperColor.Clear
        }), (Size s) => new Rect(0.0, 0.0, s.Width, 7.0), false);
      }
      this.At(paperSurface, 0.0, 0.0);
      foreach (double num in ClickyNoteView.Rules)
      {
        this.At(new Border
        {
          Width = ClickyNoteView.SheetSize.Width,
          Height = 1.0,
          Background = Ui.Brush(ClickyPaperInk.NoteRule)
        }, 0.0, num);
      }
      if (this.face.IsIdea)
      {
        ClickyShapeView clickyShapeView = ClickyShapeView.StrokeBorder(ClickyShape.RoundedRectangle(3.0), new PaperStroke(PaperColor.Hex("#E0A030").Opacity(0.85), 1.6, PenLineCap.Flat, PenLineJoin.Miter, new double[] { 5.0, 3.5 }));
        clickyShapeView.Width = ClickyNoteView.SheetSize.Width - 8.0;
        clickyShapeView.Height = ClickyNoteView.SheetSize.Height - 8.0;
        this.At(clickyShapeView, 4.0, 4.0);
      }
    }

    // Token: 0x060017A1 RID: 6049 RVA: 0x005C3270 File Offset: 0x005C3270
    private void Content()
    {
      this.At(this.TitleRow(), 16.0, 15.0);
      string meta = this.face.Meta;
      if (meta != null)
      {
        PaperFont paperFont = ClickyPaperFonts.Typed(10.5, new FontWeight?(FontWeights.Medium));
        double num = ClickyHandLineBreaker.Ascent(paperFont);
        StackPanel stackPanel = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 5.0,
          MaxWidth = ClickyNoteView.SheetSize.Width - 32.0
        };
        ClickyPaperGlyph? metaLogo = this.face.MetaLogo;
        if (metaLogo != null)
        {
          ClickyPaperGlyph valueOrDefault = metaLogo.GetValueOrDefault();
          stackPanel.Children.Add(new ClickyPaperGlyphView(valueOrDefault, PaperColor.Hex("#E5483B"), 11.0, 2.0)
          {
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(0.0, num - 10.0, 0.0, 0.0)
          });
        }
        stackPanel.Children.Add(PaperText.Line(meta, paperFont, this.face.MetaIsClicky ? ClickyPaperInk.ClickyBlue : ClickyNoteView.NoteInk.Opacity(0.55), TextAlignment.Left));
        this.sheet.Children.Add(new ClickyBaselinePlaced(stackPanel, 16.0, ClickyNoteView.Rules[0] - 5.0, num));
      }
      ClickyNoteFace.NoteBody body = this.face.Body;
      ClickyNoteFace.NoteBody.Lines lines = body as ClickyNoteFace.NoteBody.Lines;
      if (lines == null)
      {
        ClickyNoteFace.NoteBody.Chart chart = body as ClickyNoteFace.NoteBody.Chart;
        if (chart != null)
        {
          this.ChartView(chart.Series, chart.ShowsTrend);
        }
      }
      else
      {
        this.LinesView(lines.Items);
      }
      FrameworkElement frameworkElement = this.FooterView();
      if (frameworkElement != null)
      {
        Grid grid = new Grid
        {
          Width = this.FooterWidth,
          Height = 26.0
        };
        grid.Children.Add(frameworkElement);
        grid.Clip = new RectangleGeometry
        {
          Rect = new Rect(0.0, 0.0, this.FooterWidth, 26.0)
        };
        this.At(grid, 14.0, ClickyNoteView.SheetSize.Height - 11.0 - 26.0);
      }
      ClickyThingStatus? stamp = this.face.Stamp;
      if (stamp != null)
      {
        ClickyThingStatus valueOrDefault2 = stamp.GetValueOrDefault();
        if (!this.IsStampMakingRoomForActions)
        {
          ClickyPaperStamp clickyPaperStamp = new ClickyPaperStamp(valueOrDefault2, null, 9.5, -6.0);
          this.At(clickyPaperStamp, ClickyNoteView.SheetSize.Width - 12.0 - clickyPaperStamp.StampSize.Width, ClickyNoteView.SheetSize.Height - 13.0 - clickyPaperStamp.StampSize.Height);
        }
      }
    }

    // Token: 0x060017A2 RID: 6050 RVA: 0x005C3590 File Offset: 0x005C3590
    [return: TupleElementNames(new string[] { "Label", "Kind" })]
    [return: Nullable(new byte[] { 1, 0, 1 })]
    public static List<ValueTuple<string, ClickyPaperFileKind>> DistinctFileKinds(IEnumerable<string> names)
    {
      HashSet<string> hashSet = new HashSet<string>();
      List<ValueTuple<string, ClickyPaperFileKind>> list = new List<ValueTuple<string, ClickyPaperFileKind>>();
      foreach (string text in names)
      {
        string text2 = ClickyPaperFileKinds.ShortLabel(text);
        if (hashSet.Add(text2))
        {
          list.Add(new ValueTuple<string, ClickyPaperFileKind>(text2, ClickyPaperFileKinds.ForFileName(text)));
        }
      }
      return list;
    }

    // Token: 0x060017A3 RID: 6051 RVA: 0x005C3604 File Offset: 0x005C3604
    public static int FileChipsThatFit(IReadOnlyList<string> labels, double width)
    {
      PaperFont paperFont = ClickyPaperFonts.Typed(11.0, new FontWeight?(FontWeights.SemiBold));
      double num = 0.0;
      int num2 = 0;
      foreach (ValueTuple<string, int> valueTuple in labels.Take<string>(3).Select<string, ValueTuple<string, int>>((string l, int i) => new ValueTuple<string, int>(l, i)))
      {
        string item = valueTuple.Item1;
        int item2 = valueTuple.Item2;
        double num3 = 32.0 + Math.Ceiling(ClickyHandLineBreaker.Width(item, paperFont));
        double num4 = (double)((num2 > 0) ? 6 : 0);
        double num5 = (double)((item2 < labels.Count - 1) ? 30 : 0);
        if (num + num4 + num3 + num5 > width)
        {
          break;
        }
        num += num4 + num3;
        num2++;
      }
      return Math.Max(Math.Min(1, labels.Count), num2);
    }

    // Token: 0x1700043B RID: 1083
    // (get) Token: 0x060017A4 RID: 6052 RVA: 0x005C3704 File Offset: 0x005C3704
    private double FooterWidth
    {
      get
      {
        if (this.face.Stamp != null && !this.IsStampMakingRoomForActions)
        {
          return this.StampedFooterWidth;
        }
        return ClickyNoteView.SheetSize.Width - 28.0;
      }
    }

    // Token: 0x1700043C RID: 1084
    // (get) Token: 0x060017A5 RID: 6053 RVA: 0x005C374C File Offset: 0x005C374C
    private double StampedFooterWidth
    {
      get
      {
        ClickyThingStatus? stamp = this.face.Stamp;
        if (stamp != null)
        {
          ClickyThingStatus valueOrDefault = stamp.GetValueOrDefault();
          double width = ClickyPaperStamp.SizeOf(valueOrDefault, null, 9.5).Width;
          return ClickyNoteView.SheetSize.Width - 14.0 - 12.0 - width - 10.0;
        }
        return ClickyNoteView.SheetSize.Width - 28.0;
      }
    }

    // Token: 0x1700043D RID: 1085
    // (get) Token: 0x060017A6 RID: 6054 RVA: 0x005C37D8 File Offset: 0x005C37D8
    private bool IsStampMakingRoomForActions
    {
      get
      {
        if (this.face.Stamp != null)
        {
          ClickyNoteFace.NoteFooter.Actions actions = this.face.Footer as ClickyNoteFace.NoteFooter.Actions;
          if (actions != null)
          {
            ClickyNoteFace.NoteAction[] items = actions.Items;
            if (items != null && items.Length > 0)
            {
              PaperFont font = ClickyPaperFonts.Typed(12.0, new FontWeight?(FontWeights.SemiBold));
              return actions.Items.Sum<ClickyNoteFace.NoteAction>((ClickyNoteFace.NoteAction a) => Math.Ceiling(ClickyHandLineBreaker.Width(a.Title, font)) + 24.0) + (double)((actions.Items.Length - 1) * 6) > this.StampedFooterWidth;
            }
          }
        }
        return false;
      }
    }

    // Token: 0x060017A7 RID: 6055 RVA: 0x005C3874 File Offset: 0x005C3874
    private FrameworkElement TitleRow()
    {
      StackPanel stackPanel = new StackPanel();
      stackPanel.Orientation = Orientation.Horizontal;
      stackPanel.Spacing = 7.0;
      stackPanel.Height = 21.0;
      stackPanel.MaxWidth = ClickyNoteView.SheetSize.Width - 32.0;
      stackPanel.Children.Add(new ClickyPaperGlyphView(this.face.Glyph, ClickyNoteView.NoteInk.Opacity(0.82), 17.0, 1.8)
      {
        VerticalAlignment = VerticalAlignment.Center
      });
      TextBlock textBlock = PaperText.Line(this.face.Title, ClickyPaperFonts.Hand(21.0), ClickyNoteView.NoteInk, TextAlignment.Left);
      textBlock.Margin = new Thickness(0.0, 1.0, 0.0, 0.0);
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      textBlock.MaxWidth = ClickyNoteView.SheetSize.Width - 32.0 - 24.0;
      stackPanel.Children.Add(textBlock);
      return stackPanel;
    }

    // Token: 0x060017A8 RID: 6056 RVA: 0x005C39A0 File Offset: 0x005C39A0
    private void LinesView(ClickyNoteFace.NoteLine[] lines)
    {
      bool flag = lines.Any<ClickyNoteFace.NoteLine>((ClickyNoteFace.NoteLine l) => !(l.Mark is ClickyNoteFace.LineMark.None));
      double num = (double)(flag ? 18 : 19);
      double num2 = (double)(flag ? 36 : 16);
      PaperFont paperFont = ClickyPaperFonts.Hand(num);
      double num3 = ClickyHandLineBreaker.Ascent(paperFont);
      for (int i = 0; i < Math.Min(3, lines.Length); i++)
      {
        ClickyNoteFace.NoteLine noteLine = lines[i];
        double num4 = ClickyNoteView.Rules[i + 1];
        if (noteLine.IsCircled)
        {
          ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyHandRing(i + 3, 0.16), new PaperStroke(ClickyPaperInk.DoneGreen, 1.8, PenLineCap.Round, PenLineJoin.Miter, null));
          clickyShapeView.Width = 100.0;
          clickyShapeView.Height = 28.0;
          this.At(clickyShapeView, num2 - 9.0, num4 - 24.0);
        }
        this.MarkView(noteLine.Mark, num4);
        ValueTuple<List<ClickyNoteFace.LineRun>, List<ClickyNoteFace.LineRun>> valueTuple = ClickyNoteView.CaretSplit(noteLine);
        List<ClickyNoteFace.LineRun> item = valueTuple.Item1;
        List<ClickyNoteFace.LineRun> item2 = valueTuple.Item2;
        double num5 = num - 1.0;
        double num6 = (noteLine.HasCaret ? Math.Max(num3, num5 * 0.82) : num3);
        double num7 = ClickyNoteView.SheetSize.Width - num2 - (double)((noteLine.Value == null) ? 14 : 60);
        StackPanel stackPanel = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          MaxWidth = num7
        };
        TextBlock textBlock = this.LineText(item, noteLine, paperFont);
        textBlock.Margin = new Thickness(0.0, num6 - num3, 0.0, 0.0);
        FrameworkElement frameworkElement = Ui.Shrink(textBlock, HorizontalAlignment.Left, 0.86);
        frameworkElement.MaxWidth = num7;
        stackPanel.Children.Add(frameworkElement);
        if (noteLine.HasCaret)
        {
          stackPanel.Children.Add(new ClickyWritingCaret(num5)
          {
            Margin = new Thickness(2.0, num6 - num5 * 0.82, 2.0, 0.0),
            VerticalAlignment = VerticalAlignment.Top
          });
        }
        if (item2.Count > 0)
        {
          TextBlock textBlock2 = this.LineText(item2, noteLine, paperFont);
          textBlock2.Margin = new Thickness(0.0, num6 - num3, 0.0, 0.0);
          stackPanel.Children.Add(textBlock2);
        }
        this.sheet.Children.Add(new ClickyBaselinePlaced(stackPanel, num2, num4 - 2.0, num6));
        string value = noteLine.Value;
        if (value != null)
        {
          PaperFont paperFont2 = ClickyPaperFonts.Rounded(13.0, FontWeights.SemiBold);
          TextBlock textBlock3 = PaperText.Line(value, paperFont2, ClickyNoteView.NoteInk, TextAlignment.Right);
          Typography.SetNumeralAlignment(textBlock3, FontNumeralAlignment.Tabular);
          textBlock3.Width = ClickyNoteView.SheetSize.Width - 18.0;
          this.sheet.Children.Add(new ClickyBaselinePlaced(textBlock3, 0.0, num4 - 3.0, ClickyHandLineBreaker.Ascent(paperFont2)));
        }
      }
    }

    // Token: 0x060017A9 RID: 6057 RVA: 0x005C3CE4 File Offset: 0x005C3CE4
    [return: TupleElementNames(new string[] { "Before", "After" })]
    [return: Nullable(new byte[] { 0, 1, 1, 1, 1 })]
    public static ValueTuple<List<ClickyNoteFace.LineRun>, List<ClickyNoteFace.LineRun>> CaretSplit(ClickyNoteFace.NoteLine line)
    {
      ClickyNoteFace.LineRun[] runs = line.Runs;
      if (runs == null || runs.Length <= 0)
      {
        return new ValueTuple<List<ClickyNoteFace.LineRun>, List<ClickyNoteFace.LineRun>>(new List<ClickyNoteFace.LineRun>(), new List<ClickyNoteFace.LineRun>());
      }
      int num = Array.FindLastIndex<ClickyNoteFace.LineRun>(runs, (ClickyNoteFace.LineRun r) => r.IsClicky);
      if (!line.HasCaret || num < 0)
      {
        return new ValueTuple<List<ClickyNoteFace.LineRun>, List<ClickyNoteFace.LineRun>>(runs.ToList<ClickyNoteFace.LineRun>(), new List<ClickyNoteFace.LineRun>());
      }
      return new ValueTuple<List<ClickyNoteFace.LineRun>, List<ClickyNoteFace.LineRun>>(RuntimeHelpers.GetSubArray<ClickyNoteFace.LineRun>(runs, Range.EndAt(num + 1)).ToList<ClickyNoteFace.LineRun>(), RuntimeHelpers.GetSubArray<ClickyNoteFace.LineRun>(runs, Range.StartAt(num + 1)).ToList<ClickyNoteFace.LineRun>());
    }

    // Token: 0x060017AA RID: 6058 RVA: 0x005C3D8C File Offset: 0x005C3D8C
    private TextBlock LineText(List<ClickyNoteFace.LineRun> runs, ClickyNoteFace.NoteLine line, PaperFont font)
    {
      TextBlock textBlock = font.Apply(new TextBlock
      {
        TextWrapping = TextWrapping.NoWrap,
        TextTrimming = TextTrimming.CharacterEllipsis
      });
      if (runs.Count == 0)
      {
        textBlock.Text = line.Text;
        textBlock.Foreground = Ui.Brush(ClickyNoteView.LineColor(line));
        return textBlock;
      }
      foreach (ClickyNoteFace.LineRun lineRun in runs)
      {
        textBlock.Inlines.Add(new Run
        {
          Text = lineRun.Text,
          Foreground = Ui.Brush(lineRun.IsClicky ? ClickyPaperInk.ClickyBlue : ClickyNoteView.NoteInk)
        });
      }
      return textBlock;
    }

    // Token: 0x060017AB RID: 6059 RVA: 0x005C3E50 File Offset: 0x005C3E50
    private static Color LineColor(ClickyNoteFace.NoteLine line)
    {
      ClickyNoteFace.LineMark mark = line.Mark;
      ClickyNoteFace.LineTone tone = line.Tone;
      Color color;
      if (!(mark is ClickyNoteFace.LineMark.Now) && tone != ClickyNoteFace.LineTone.Clicky)
      {
        if (!(mark is ClickyNoteFace.LineMark.Todo) && tone != ClickyNoteFace.LineTone.Quiet)
        {
          color = ClickyNoteView.NoteInk;
        }
        else
        {
          color = PaperColor.Hex("#A3A7B3");
        }
      }
      else
      {
        color = ClickyPaperInk.ClickyBlue;
      }
      return color;
    }

    // Token: 0x060017AC RID: 6060 RVA: 0x005C3EA4 File Offset: 0x005C3EA4
    private void MarkView(ClickyNoteFace.LineMark mark, double ruleY)
    {
      if (mark is ClickyNoteFace.LineMark.Done)
      {
        this.sheet.Children.Add(new PaperDrawing().Stroke(new PaperPath().MoveTo(18.0, ruleY - 8.0).LineTo(22.0, ruleY - 4.0).LineTo(29.0, ruleY - 13.0), new PaperStroke(ClickyPaperInk.DoneGreen, 2.0, PenLineCap.Round, PenLineJoin.Round, null)));
        return;
      }
      if (mark is ClickyNoteFace.LineMark.Now)
      {
        this.sheet.Children.Add(new PaperDrawing().Stroke(new PaperPath().AddArc(new Point(23.5, ruleY - 8.5), 5.0, -1.5707963267948966, 3.490658503988659, false), new PaperStroke(ClickyPaperInk.ClickyBlue, 2.0, PenLineCap.Round, PenLineJoin.Miter, null)));
        return;
      }
      if (mark is ClickyNoteFace.LineMark.Todo)
      {
        ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyWobblyRect((int)ruleY, 1.1), PaperColor.Hex("#A3A7B3"), 1.6);
        clickyShapeView.Width = (clickyShapeView.Height = 12.0);
        this.At(clickyShapeView, 18.0, ruleY - 15.0);
        return;
      }
      ClickyNoteFace.LineMark.Number number = mark as ClickyNoteFace.LineMark.Number;
      if (number == null)
      {
        return;
      }
      PaperFont paperFont = ClickyPaperFonts.Hand(18.0);
      UIElementCollection children = this.sheet.Children;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
      defaultInterpolatedStringHandler.AppendFormatted<int>(number.Value);
      children.Add(new ClickyBaselinePlaced(PaperText.Line(defaultInterpolatedStringHandler.ToStringAndClear(), paperFont, ClickyNoteView.NoteInk, TextAlignment.Left), 16.0, ruleY - 2.0, ClickyHandLineBreaker.Ascent(paperFont)));
    }

    // Token: 0x060017AD RID: 6061 RVA: 0x005C4094 File Offset: 0x005C4094
    private void ChartView(double[] series, bool showsTrend)
    {
      Rect frame = new Rect(16f, 52f, 208f, 80f);
      PaperDrawing paperDrawing = new PaperDrawing();
      paperDrawing.Stroke(new PaperPath().MoveTo(frame.X + 4.0, frame.Y + 6.0).LineTo(frame.X + 4.0, frame.Y + 70.0).LineTo(frame.Right - 4.0, frame.Y + 70.0), new PaperStroke(ClickyNoteView.NoteInk.Opacity(0.35), 1.4, PenLineCap.Round, PenLineJoin.Miter, null));
      if (series.Length > 1)
      {
        double low = series.Min();
        double num = series.Max();
        double span = Math.Max(num - low, 0.0001);
        List<Point> list = series.Select<double, Point>((double value, int index) => new Point(frame.X + 10.0 + (double)index / (double)(series.Length - 1) * (frame.Width - 22.0), frame.Y + 62.0 - (value - low) / span * 46.0)).ToList<Point>();
        paperDrawing.Stroke(new PaperPath().AddLines(list), new PaperStroke(PaperColor.Hex("#2E9E62"), 2.2, PenLineCap.Round, PenLineJoin.Round, null));
        if (showsTrend)
        {
          PaperDrawing paperDrawing2 = paperDrawing;
          PaperPath paperPath = new PaperPath().MoveTo(list[0].X, list[0].Y + 4.0);
          List<Point> list2 = list;
          double num2 = list2[list2.Count - 1].X + 2.0;
          List<Point> list3 = list;
          paperDrawing2.Stroke(paperPath.LineTo(num2, list3[list3.Count - 1].Y - 6.0), new PaperStroke(ClickyPaperInk.ClickyBlue, 2.0, PenLineCap.Round, PenLineJoin.Miter, new double[] { 6.0, 5.0 }));
        }
      }
      this.sheet.Children.Add(paperDrawing);
    }

    // Token: 0x060017AE RID: 6062 RVA: 0x005C42F0 File Offset: 0x005C42F0
    [NullableContext(2)]
    private FrameworkElement FooterView()
    {
      ClickyNoteFace.NoteFooter footer = this.face.Footer;
      ClickyNoteFace.NoteFooter.Actions actions = footer as ClickyNoteFace.NoteFooter.Actions;
      if (actions != null)
      {
        StackPanel stackPanel = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 6.0,
          Height = 26.0
        };
        ClickyNoteFace.NoteAction[] items = actions.Items;
        for (int i = 0; i < items.Length; i++)
        {
          ClickyNoteFace.NoteAction action = items[i];
          stackPanel.Children.Add(new ClickyPaperGelButton(action.Title, delegate
          {
            this.onAction(action.Id);
          }, action.Gel, 26.0, 12.0, 12.0, null));
        }
        return stackPanel;
      }
      ClickyNoteFace.NoteFooter.Files files = footer as ClickyNoteFace.NoteFooter.Files;
      if (files != null)
      {
        List<ValueTuple<string, ClickyPaperFileKind>> list = ClickyNoteView.DistinctFileKinds(files.Names);
        int num = ClickyNoteView.FileChipsThatFit(list.Select<ValueTuple<string, ClickyPaperFileKind>, string>(([TupleElementNames(new string[] { "Label", "Kind" })] ValueTuple<string, ClickyPaperFileKind> k) => k.Item1).ToList<string>(), this.FooterWidth);
        StackPanel stackPanel2 = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 6.0,
          Height = 26.0
        };
        foreach (ValueTuple<string, ClickyPaperFileKind> valueTuple in list.Take<ValueTuple<string, ClickyPaperFileKind>>(num))
        {
          stackPanel2.Children.Add(new ClickyPaperFileChip(valueTuple.Item1, new ClickyPaperFileKind?(valueTuple.Item2), 24.0, 11.0)
          {
            VerticalAlignment = VerticalAlignment.Center
          });
        }
        if (list.Count > num)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
          defaultInterpolatedStringHandler.AppendLiteral("+");
          defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count - num);
          TextBlock textBlock = PaperText.Line(defaultInterpolatedStringHandler.ToStringAndClear(), ClickyPaperFonts.Typed(10.5, new FontWeight?(FontWeights.SemiBold)), ClickyNoteView.NoteInk.Opacity(0.55), TextAlignment.Left);
          textBlock.VerticalAlignment = VerticalAlignment.Center;
          stackPanel2.Children.Add(textBlock);
        }
        return stackPanel2;
      }
      ClickyNoteFace.NoteFooter.Progress progress = footer as ClickyNoteFace.NoteFooter.Progress;
      if (progress != null)
      {
        StackPanel stackPanel3 = new StackPanel();
        stackPanel3.Orientation = Orientation.Horizontal;
        stackPanel3.Spacing = 7.0;
        stackPanel3.Height = 26.0;
        stackPanel3.Children.Add(new ClickyPaperProgressBar(progress.Fraction, 74.0, 4.0, null, null)
        {
          VerticalAlignment = VerticalAlignment.Center
        });
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
        defaultInterpolatedStringHandler.AppendFormatted<int>(progress.Step);
        defaultInterpolatedStringHandler.AppendLiteral(" of ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(progress.Total);
        TextBlock textBlock2 = PaperText.Line(defaultInterpolatedStringHandler.ToStringAndClear(), ClickyPaperFonts.Typed(10.5, new FontWeight?(FontWeights.Medium)), ClickyNoteView.NoteInk.Opacity(0.6), TextAlignment.Left);
        Typography.SetNumeralAlignment(textBlock2, FontNumeralAlignment.Tabular);
        textBlock2.VerticalAlignment = VerticalAlignment.Center;
        stackPanel3.Children.Add(textBlock2);
        return stackPanel3;
      }
      ClickyNoteFace.NoteFooter.Text text = footer as ClickyNoteFace.NoteFooter.Text;
      if (text == null)
      {
        return null;
      }
      TextBlock textBlock3 = PaperText.Line(text.Words, ClickyPaperFonts.Typed(10.5, new FontWeight?(FontWeights.Medium)), ClickyNoteView.NoteInk.Opacity(0.55), TextAlignment.Left);
      textBlock3.VerticalAlignment = VerticalAlignment.Center;
      return new Grid
      {
        Height = 26.0,
        Children = { textBlock3 }
      };
    }

    // Token: 0x04000B7A RID: 2938
    public static readonly Size SheetSize = new Size(240f, 176f);

    // Token: 0x04000B7B RID: 2939
    public static readonly Thickness ShadowInsets = new Thickness(6.0, 5.0, 6.0, 9.0);

    // Token: 0x04000B7C RID: 2940
    public static readonly double[] Rules = new double[] { 52.0, 78.0, 104.0, 130.0 };

    // Token: 0x04000B7D RID: 2941
    private static readonly Color NoteInk = ClickyPaperStock.Cream.Ink();

    // Token: 0x04000B7E RID: 2942
    private readonly ClickyNoteFace face;

    // Token: 0x04000B7F RID: 2943
    private readonly Action<string> onAction;

    // Token: 0x04000B80 RID: 2944
    private readonly Canvas sheet = new Canvas
    {
      Width = ClickyNoteView.SheetSize.Width,
      Height = ClickyNoteView.SheetSize.Height
    };
  }
}
