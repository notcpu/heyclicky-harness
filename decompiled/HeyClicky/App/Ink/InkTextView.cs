using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
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
  // Token: 0x0200029E RID: 670
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class InkTextView : Panel, IInkBaseline
  {
    // Token: 0x06001BA5 RID: 7077 RVA: 0x005F3B10 File Offset: 0x005F3B10
    public InkTextView(ClickyInkTextContent content, PaperFont font, Color? foreground = null)
    {
      this.content = content;
      this.font = font;
      this.Foreground = foreground;
      base.Children.Add(this.behind);
      base.Children.Add(this.block);
      base.Children.Add(this.overlay);
      AutomationProperties.SetName(this, content.PlainText);
    }

    // Token: 0x06001BA6 RID: 7078 RVA: 0x005F3BFC File Offset: 0x005F3BFC
    public static InkTextView Plain(string text, PaperFont font, Color color)
    {
      return new InkTextView(ClickyInkTextContent.Plain(text, null), font, new Color?(color));
    }

    // Token: 0x170004BC RID: 1212
    // (get) Token: 0x06001BA7 RID: 7079 RVA: 0x005F3C24 File Offset: 0x005F3C24
    // (set) Token: 0x06001BA8 RID: 7080 RVA: 0x005F3C2C File Offset: 0x005F3C2C
    public ClickyInkTextContent Content
    {
      get
      {
        return this.content;
      }
      set
      {
        this.content = value;
        this.dirty = true;
        base.InvalidateMeasure();
      }
    }

    // Token: 0x170004BD RID: 1213
    // (get) Token: 0x06001BA9 RID: 7081 RVA: 0x005F3C44 File Offset: 0x005F3C44
    // (set) Token: 0x06001BAA RID: 7082 RVA: 0x005F3C4C File Offset: 0x005F3C4C
    public PaperFont Font
    {
      get
      {
        return this.font;
      }
      set
      {
        this.font = value;
        this.dirty = true;
        base.InvalidateMeasure();
      }
    }

    // Token: 0x170004BE RID: 1214
    // (get) Token: 0x06001BAB RID: 7083 RVA: 0x005F3C64 File Offset: 0x005F3C64
    // (set) Token: 0x06001BAC RID: 7084 RVA: 0x005F3C6C File Offset: 0x005F3C6C
    public Color? Foreground { get; set; }

    // Token: 0x170004BF RID: 1215
    // (get) Token: 0x06001BAD RID: 7085 RVA: 0x005F3C78 File Offset: 0x005F3C78
    // (set) Token: 0x06001BAE RID: 7086 RVA: 0x005F3C80 File Offset: 0x005F3C80
    public int? LineLimit { get; set; }

    // Token: 0x170004C0 RID: 1216
    // (get) Token: 0x06001BAF RID: 7087 RVA: 0x005F3C8C File Offset: 0x005F3C8C
    // (set) Token: 0x06001BB0 RID: 7088 RVA: 0x005F3C94 File Offset: 0x005F3C94
    public double LineSpacing { get; set; }

    // Token: 0x170004C1 RID: 1217
    // (get) Token: 0x06001BB1 RID: 7089 RVA: 0x005F3CA0 File Offset: 0x005F3CA0
    // (set) Token: 0x06001BB2 RID: 7090 RVA: 0x005F3CA8 File Offset: 0x005F3CA8
    public Color? Strikethrough { get; set; }

    // Token: 0x170004C2 RID: 1218
    // (get) Token: 0x06001BB3 RID: 7091 RVA: 0x005F3CB4 File Offset: 0x005F3CB4
    // (set) Token: 0x06001BB4 RID: 7092 RVA: 0x005F3CBC File Offset: 0x005F3CBC
    public double Tracking { get; set; }

    // Token: 0x170004C3 RID: 1219
    // (get) Token: 0x06001BB5 RID: 7093 RVA: 0x005F3CC8 File Offset: 0x005F3CC8
    // (set) Token: 0x06001BB6 RID: 7094 RVA: 0x005F3CD0 File Offset: 0x005F3CD0
    public bool Uppercase { get; set; }

    // Token: 0x170004C4 RID: 1220
    // (get) Token: 0x06001BB7 RID: 7095 RVA: 0x005F3CDC File Offset: 0x005F3CDC
    // (set) Token: 0x06001BB8 RID: 7096 RVA: 0x005F3CE4 File Offset: 0x005F3CE4
    public bool Italic { get; set; }

    // Token: 0x170004C5 RID: 1221
    // (get) Token: 0x06001BB9 RID: 7097 RVA: 0x005F3CF0 File Offset: 0x005F3CF0
    // (set) Token: 0x06001BBA RID: 7098 RVA: 0x005F3CF8 File Offset: 0x005F3CF8
    public TextAlignment TextAlignment { get; set; } = TextAlignment.Left;

    // Token: 0x170004C6 RID: 1222
    // (get) Token: 0x06001BBB RID: 7099 RVA: 0x005F3D04 File Offset: 0x005F3D04
    // (set) Token: 0x06001BBC RID: 7100 RVA: 0x005F3D0C File Offset: 0x005F3D0C
    public double MinimumScale { get; set; } = 1.0;

    // Token: 0x170004C7 RID: 1223
    // (get) Token: 0x06001BBD RID: 7101 RVA: 0x005F3D18 File Offset: 0x005F3D18
    // (set) Token: 0x06001BBE RID: 7102 RVA: 0x005F3D20 File Offset: 0x005F3D20
    public bool MonospacedDigit { get; set; }

    // Token: 0x170004C8 RID: 1224
    // (get) Token: 0x06001BBF RID: 7103 RVA: 0x005F3D2C File Offset: 0x005F3D2C
    // (set) Token: 0x06001BC0 RID: 7104 RVA: 0x005F3D34 File Offset: 0x005F3D34
    public bool FillsWidth { get; set; }

    // Token: 0x170004C9 RID: 1225
    // (get) Token: 0x06001BC1 RID: 7105 RVA: 0x005F3D40 File Offset: 0x005F3D40
    public double? FirstBaseline
    {
      get
      {
        if (this.block.BaselineOffset <= 0.0)
        {
          return null;
        }
        return new double?(this.block.BaselineOffset);
      }
    }

    // Token: 0x06001BC2 RID: 7106 RVA: 0x005F3D80 File Offset: 0x005F3D80
    public InkTextView With(Action<InkTextView> configure)
    {
      configure(this);
      this.dirty = true;
      return this;
    }

    // Token: 0x06001BC3 RID: 7107 RVA: 0x005F3D94 File Offset: 0x005F3D94
    private void Build(double size)
    {
      this.appliedSize = size;
      this.block.Inlines.Clear();
      this.runs.Clear();
      this.block.FontFamily = this.font.Family;
      this.block.FontWeight = this.font.Weight;
      this.block.FontStyle = (this.Italic ? FontStyle.Italic : FontStyle.Normal);
      this.block.FontSize = size;
      this.block.TextAlignment = this.TextAlignment;
      this.block.LineHeight = size * 1.193 + this.LineSpacing;
      this.block.CharacterSpacing = ((this.Tracking == 0.0) ? 0 : ((int)Math.Round(this.Tracking * 1000.0 / size)));
      Typography.SetNumeralAlignment(this.block, this.MonospacedDigit ? FontNumeralAlignment.Tabular : FontNumeralAlignment.Normal);
      this.block.Foreground = Ui.Brush(this.Foreground ?? PaperColor.Black);
      int? lineLimit = this.LineLimit;
      if (lineLimit != null)
      {
        int valueOrDefault = lineLimit.GetValueOrDefault();
        this.block.MaxLines = valueOrDefault;
        this.block.TextTrimming = TextTrimming.CharacterEllipsis;
        if (valueOrDefault == 1)
        {
          this.block.TextWrapping = TextWrapping.NoWrap;
        }
      }
      else
      {
        this.block.MaxLines = 0;
        this.block.TextTrimming = TextTrimming.None;
        this.block.TextWrapping = TextWrapping.Wrap;
      }
      foreach (ClickyInkRun clickyInkRun in this.content.Runs)
      {
        Run run = new Run
        {
          Text = (this.Uppercase ? clickyInkRun.Text.ToUpperInvariant() : clickyInkRun.Text)
        };
        string symbol = clickyInkRun.Symbol;
        if (symbol != null)
        {
          char? c;
          run.Text = ((ClickyInkIcon.Resolve(symbol) != null) ? c.GetValueOrDefault().ToString() : null) ?? "";
          run.FontFamily = DS.Fonts.Symbols;
        }
        Color? foreground = clickyInkRun.Foreground;
        if (foreground != null)
        {
          Color valueOrDefault2 = foreground.GetValueOrDefault();
          run.Foreground = Ui.Brush(valueOrDefault2);
        }
        if (clickyInkRun.IsBold)
        {
          run.FontWeight = ((this.font.Weight.Weight >= FontWeights.Bold.Weight) ? FontWeights.Black : FontWeights.Bold);
        }
        this.runs.Add(run);
        this.block.Inlines.Add(run);
      }
      this.overlayKey = "";
      this.dirty = false;
    }

    // Token: 0x06001BC4 RID: 7108 RVA: 0x005F4084 File Offset: 0x005F4084
    protected override Size MeasureOverride(Size availableSize)
    {
      double size = this.font.Size;
      if (this.dirty || this.appliedSize <= 0.0)
      {
        this.Build(size);
      }
      this.behind.Measure(new Size(0f, 0f));
      this.overlay.Measure(new Size(0f, 0f));
      if (this.MinimumScale < 1.0 && this.LineLimit.GetValueOrDefault() == 1 && !double.IsInfinity(availableSize.Width))
      {
        if (this.appliedSize != size)
        {
          this.Build(size);
        }
        this.block.Measure(new Size(double.PositiveInfinity, availableSize.Height));
        double width = this.block.DesiredSize.Width;
        if (width > availableSize.Width && width > 0.0)
        {
          double num = size * Math.Max(this.MinimumScale, availableSize.Width / width);
          this.Build(Math.Floor(num * 10.0) / 10.0);
        }
      }
      this.block.Measure(availableSize);
      Size desiredSize = this.block.DesiredSize;
      return new Size((this.FillsWidth && !double.IsInfinity(availableSize.Width)) ? availableSize.Width : desiredSize.Width, desiredSize.Height);
    }

    // Token: 0x06001BC5 RID: 7109 RVA: 0x005F4208 File Offset: 0x005F4208
    protected override Size ArrangeOverride(Size finalSize)
    {
      this.behind.Arrange(new Rect(0.0, 0.0, finalSize.Width, finalSize.Height));
      this.block.Arrange(new Rect(0.0, 0.0, finalSize.Width, finalSize.Height));
      this.overlay.Arrange(new Rect(0.0, 0.0, finalSize.Width, finalSize.Height));
      this.DrawMarks();
      return finalSize;
    }

    // Token: 0x06001BC6 RID: 7110 RVA: 0x005F42AC File Offset: 0x005F42AC
    [NullableContext(2)]
    private List<InkTextView.GlyphBox> Glyphs(int index, double baselineInLine)
    {
      List<InkTextView.GlyphBox> list;
      try
      {
        Run run = this.runs[index];
        int length = run.Text.Length;
        if (length == 0)
        {
          list = new List<InkTextView.GlyphBox>();
        }
        else
        {
          List<InkTextView.GlyphBox> list2 = new List<InkTextView.GlyphBox>(length);
          for (int i = 0; i < length; i++)
          {
            TextPointer positionAtOffset = run.ContentStart.GetPositionAtOffset(i, LogicalDirection.Forward);
            if (positionAtOffset == null)
            {
              return null;
            }
            Rect characterRect = positionAtOffset.GetCharacterRect(LogicalDirection.Forward);
            double num = ((characterRect.Width > 0.0) ? (characterRect.X + characterRect.Width) : characterRect.X);
            if (characterRect.Width <= 0.0 && i + 1 <= length)
            {
              TextPointer textPointer = ((i + 1 < length) ? run.ContentStart.GetPositionAtOffset(i + 1, LogicalDirection.Forward) : run.ContentEnd);
              Rect? rect = ((textPointer != null) ? new Rect?(textPointer.GetCharacterRect((i + 1 < length) ? LogicalDirection.Forward : LogicalDirection.Backward)) : null);
              if (rect != null)
              {
                Rect valueOrDefault = rect.GetValueOrDefault();
                if (Math.Abs(valueOrDefault.Y - characterRect.Y) < 0.5)
                {
                  num = valueOrDefault.X;
                }
              }
            }
            list2.Add(new InkTextView.GlyphBox(characterRect.X, num, characterRect.Y, characterRect.Y + characterRect.Height, characterRect.Y + baselineInLine));
          }
          list = list2;
        }
      }
      catch
      {
        list = null;
      }
      return list;
    }

    // Token: 0x06001BC7 RID: 7111 RVA: 0x005F4448 File Offset: 0x005F4448
    private static List<InkTextView.GlyphBox> Lines(List<InkTextView.GlyphBox> glyphs, [Nullable(2)] Func<int, bool> include = null)
    {
      List<InkTextView.GlyphBox> list = new List<InkTextView.GlyphBox>();
      for (int i = 0; i < glyphs.Count; i++)
      {
        if (include == null || include(i))
        {
          InkTextView.GlyphBox glyphBox = glyphs[i];
          if (list.Count > 0)
          {
            List<InkTextView.GlyphBox> list2 = list;
            if (Math.Abs(list2[list2.Count - 1].Baseline - glyphBox.Baseline) < 0.5)
            {
              double left = glyphBox.Left;
              List<InkTextView.GlyphBox> list3 = list;
              if (left >= list3[list3.Count - 1].Left - 0.5)
              {
                List<InkTextView.GlyphBox> list4 = list;
                int num = list4.Count - 1;
                List<InkTextView.GlyphBox> list5 = list;
                InkTextView.GlyphBox glyphBox2 = list5[list5.Count - 1];
                List<InkTextView.GlyphBox> list6 = list;
                glyphBox2.Right = Math.Max(list6[list6.Count - 1].Right, glyphBox.Right);
                list4[num] = glyphBox2;
                goto IL_00D8;
              }
            }
          }
          list.Add(glyphBox);
        }
        IL_00D8:;
      }
      return list;
    }

    // Token: 0x06001BC8 RID: 7112 RVA: 0x005F4540 File Offset: 0x005F4540
    private void DrawMarks()
    {
      bool flag;
      if (this.Strikethrough == null && this.content.PenRun < 0)
      {
        flag = this.content.Runs.Any<ClickyInkRun>((ClickyInkRun r) => r.Strike != null || r.IsLink || r.Background != null);
      }
      else
      {
        flag = true;
      }
      if (!flag)
      {
        if (this.overlayKey.Length > 0)
        {
          this.behind.Children.Clear();
          this.overlay.Children.Clear();
          this.overlayKey = "";
        }
        return;
      }
      double num = this.appliedSize;
      double num2 = 0.0;
      try
      {
        if (this.runs.Count > 0)
        {
          num2 = this.runs[0].ContentStart.GetCharacterRect(LogicalDirection.Forward).Y;
        }
      }
      catch
      {
      }
      double num3 = this.block.BaselineOffset - num2;
      List<ValueTuple<PaperPath, Color>> list = new List<ValueTuple<PaperPath, Color>>();
      List<ValueTuple<PaperPath, PaperStroke>> list2 = new List<ValueTuple<PaperPath, PaperStroke>>();
      StringBuilder stringBuilder = new StringBuilder();
      ValueTuple<double, double, double>? valueTuple = null;
      int num4 = 0;
      while (num4 < this.runs.Count && num4 < this.content.Runs.Count)
      {
        ClickyInkRun r = this.content.Runs[num4];
        Color? color = r.Strike;
        Color? color2 = ((color != null) ? color : this.Strikethrough);
        bool flag2 = num4 == this.content.PenRun && this.content.Pen != null;
        if (color2 != null || r.IsLink || r.Background != null || flag2)
        {
          List<InkTextView.GlyphBox> list3 = this.Glyphs(num4, num3);
          if (list3 != null && list3.Count > 0)
          {
            foreach (InkTextView.GlyphBox glyphBox in list3)
            {
              StringBuilder stringBuilder2 = stringBuilder;
              StringBuilder stringBuilder3 = stringBuilder2;
              StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(2, 2, stringBuilder2);
              appendInterpolatedStringHandler.AppendFormatted<double>(glyphBox.Left, "0.#");
              appendInterpolatedStringHandler.AppendLiteral(",");
              appendInterpolatedStringHandler.AppendFormatted<double>(glyphBox.Baseline, "0.#");
              appendInterpolatedStringHandler.AppendLiteral(";");
              stringBuilder3.Append(ref appendInterpolatedStringHandler);
            }
            color = r.Background;
            if (color != null)
            {
              Color valueOrDefault = color.GetValueOrDefault();
              foreach (InkTextView.GlyphBox glyphBox2 in InkTextView.Lines(list3, null))
              {
                list.Add(new ValueTuple<PaperPath, Color>(new PaperPath().AddRect(new Rect(glyphBox2.Left, glyphBox2.Top, Math.Max(0.0, glyphBox2.Right - glyphBox2.Left), glyphBox2.Bottom - glyphBox2.Top)), valueOrDefault));
              }
            }
            if (color2 != null)
            {
              Color valueOrDefault2 = color2.GetValueOrDefault();
              foreach (InkTextView.GlyphBox glyphBox3 in InkTextView.Lines(list3, (int i) => i < r.Text.TrimEnd(' ').Length))
              {
                double num5 = glyphBox3.Baseline - num * 0.26;
                list2.Add(new ValueTuple<PaperPath, PaperStroke>(new PaperPath().MoveTo(glyphBox3.Left, num5).LineTo(glyphBox3.Right, num5), new PaperStroke(valueOrDefault2, Math.Max(1.0, num * 0.05), PenLineCap.Flat, PenLineJoin.Miter, null)));
              }
            }
            if (r.IsLink)
            {
              color = r.LinkTint;
              if (color != null)
              {
                Color valueOrDefault3 = color.GetValueOrDefault();
                foreach (InkTextView.GlyphBox glyphBox4 in InkTextView.Lines(list3, null))
                {
                  PaperPath paperPath = InkTextView.LinkUnderline(glyphBox4, num);
                  if (paperPath != null)
                  {
                    list2.Add(new ValueTuple<PaperPath, PaperStroke>(paperPath, new PaperStroke(valueOrDefault3.Opacity(0.55), 1.25, PenLineCap.Round, PenLineJoin.Round, null)));
                  }
                }
              }
            }
            if (flag2)
            {
              InkTextView.GlyphBox glyphBox5 = list3[0];
              double num6 = list3.Max<InkTextView.GlyphBox>((InkTextView.GlyphBox x) => x.Right);
              valueTuple = new ValueTuple<double, double, double>?(new ValueTuple<double, double, double>((this.content.Pen.Value.Wet > 0.0) ? num6 : glyphBox5.Left, glyphBox5.Baseline, num * 0.9521484375));
            }
          }
        }
        num4++;
      }
      string text = stringBuilder.ToString() + "|" + this.content.Pen.ToString() + this.content.PenClock.ToString();
      if (text == this.overlayKey)
      {
        return;
      }
      this.overlayKey = text;
      this.behind.Children.Clear();
      this.overlay.Children.Clear();
      foreach (ValueTuple<PaperPath, Color> valueTuple2 in list)
      {
        PaperPath item = valueTuple2.Item1;
        Color item2 = valueTuple2.Item2;
        this.behind.Children.Add(new Path
        {
          Data = item.ToGeometry(),
          Fill = Ui.Brush(item2)
        });
      }
      foreach (ValueTuple<PaperPath, PaperStroke> valueTuple3 in list2)
      {
        PaperPath item3 = valueTuple3.Item1;
        PaperStroke item4 = valueTuple3.Item2;
        this.overlay.Children.Add(new Path
        {
          Data = item3.ToGeometry(),
          Stroke = Ui.Brush(item4.Color),
          StrokeThickness = item4.Width,
          StrokeStartLineCap = item4.Cap,
          StrokeEndLineCap = item4.Cap,
          StrokeLineJoin = item4.Join
        });
      }
      if (valueTuple != null)
      {
        ValueTuple<double, double, double> valueOrDefault4 = valueTuple.GetValueOrDefault();
        ClickyInkPenAttribute? pen = this.content.Pen;
        if (pen != null)
        {
          ClickyInkPenAttribute valueOrDefault5 = pen.GetValueOrDefault();
          this.overlay.Children.Add(InkTextView.Pencil(valueOrDefault5, valueOrDefault4.Item1, valueOrDefault4.Item2, valueOrDefault4.Item3, this.content.PenClock));
        }
      }
    }

    // Token: 0x06001BC9 RID: 7113 RVA: 0x005F4C98 File Offset: 0x005F4C98
    [NullableContext(2)]
    private static PaperPath LinkUnderline(InkTextView.GlyphBox line, double size)
    {
      double num = line.Right - line.Left;
      if (num <= 2.0)
      {
        return null;
      }
      double num2 = line.Baseline + Math.Max(1.6, size * 0.2412109375 * 0.42);
      double num3 = line.Baseline - size * 0.9521484375;
      double num4 = (double)((int)(line.Left * 7.0 + num3 * 3.0) % 97);
      double num5 = line.Left + 0.5;
      double num6 = line.Right - 0.5;
      PaperPath paperPath = new PaperPath().MoveTo(num5, num2 + 0.3);
      int num7 = Math.Max(2, (int)(num / 18.0));
      for (int i = 1; i <= num7; i++)
      {
        double num8 = (double)i / (double)num7;
        double num9 = num5 + (num6 - num5) * num8;
        double num10 = Math.Sin(num4 + num8 * 7.3) * 0.55 + (num8 - 0.5) * 0.5;
        paperPath.LineTo(num9, num2 + num10);
      }
      return paperPath;
    }

    // Token: 0x06001BCA RID: 7114 RVA: 0x005F4DE8 File Offset: 0x005F4DE8
    private static FrameworkElement Pencil(ClickyInkPenAttribute pen, double edge, double baseline, double ascent, double clock)
    {
      double num = Math.Max(9.0, ascent * 0.85);
      double num2 = edge + 0.5;
      double num3 = Math.Sin(clock * 2.0 * 3.141592653589793 * 4.5) * 0.7;
      FrameworkElement frameworkElement = ClickyInkPencilGlyph.Make(num, pen.Tint, false);
      frameworkElement.Opacity = 0.62;
      Canvas.SetLeft(frameworkElement, num2 - num * 0.06);
      Canvas.SetTop(frameworkElement, baseline - ascent * 0.18 + num3 - num * 0.94);
      frameworkElement.RenderTransform = new RotateTransform
      {
        Angle = Math.Sin(clock * 2.0 * 3.141592653589793 * 2.3) * 2.5,
        CenterX = num * 0.06,
        CenterY = num * 0.94
      };
      return frameworkElement;
    }

    // Token: 0x04000D4E RID: 3406
    private readonly TextBlock block = new TextBlock
    {
      TextWrapping = TextWrapping.Wrap,
      IsTextScaleFactorEnabled = false,
      LineStackingStrategy = LineStackingStrategy.BlockLineHeight,
      OpticalMarginAlignment = OpticalMarginAlignment.None,
      TextLineBounds = TextLineBounds.Full
    };

    // Token: 0x04000D4F RID: 3407
    private readonly Canvas behind = new Canvas
    {
      IsHitTestVisible = false
    };

    // Token: 0x04000D50 RID: 3408
    private readonly Canvas overlay = new Canvas
    {
      IsHitTestVisible = false
    };

    // Token: 0x04000D51 RID: 3409
    private readonly List<Run> runs = new List<Run>();

    // Token: 0x04000D52 RID: 3410
    private ClickyInkTextContent content;

    // Token: 0x04000D53 RID: 3411
    private PaperFont font;

    // Token: 0x04000D54 RID: 3412
    private bool dirty = true;

    // Token: 0x04000D55 RID: 3413
    private double appliedSize;

    // Token: 0x04000D56 RID: 3414
    private string overlayKey = "";

    // Token: 0x02000877 RID: 2167
    [NullableContext(0)]
    private readonly struct GlyphBox : IEquatable<InkTextView.GlyphBox>
    {
      // Token: 0x0600395D RID: 14685 RVA: 0x006891D0 File Offset: 0x006891D0
      public GlyphBox(double Left, double Right, double Top, double Bottom, double Baseline)
      {
        this.Left = Left;
        this.Right = Right;
        this.Top = Top;
        this.Bottom = Bottom;
        this.Baseline = Baseline;
      }

      // Token: 0x170007FD RID: 2045
      // (get) Token: 0x0600395E RID: 14686 RVA: 0x006891F8 File Offset: 0x006891F8
      // (set) Token: 0x0600395F RID: 14687 RVA: 0x00689200 File Offset: 0x00689200
      public double Left { get; set; }

      // Token: 0x170007FE RID: 2046
      // (get) Token: 0x06003960 RID: 14688 RVA: 0x0068920C File Offset: 0x0068920C
      // (set) Token: 0x06003961 RID: 14689 RVA: 0x00689214 File Offset: 0x00689214
      public double Right { get; set; }

      // Token: 0x170007FF RID: 2047
      // (get) Token: 0x06003962 RID: 14690 RVA: 0x00689220 File Offset: 0x00689220
      // (set) Token: 0x06003963 RID: 14691 RVA: 0x00689228 File Offset: 0x00689228
      public double Top { get; set; }

      // Token: 0x17000800 RID: 2048
      // (get) Token: 0x06003964 RID: 14692 RVA: 0x00689234 File Offset: 0x00689234
      // (set) Token: 0x06003965 RID: 14693 RVA: 0x0068923C File Offset: 0x0068923C
      public double Bottom { get; set; }

      // Token: 0x17000801 RID: 2049
      // (get) Token: 0x06003966 RID: 14694 RVA: 0x00689248 File Offset: 0x00689248
      // (set) Token: 0x06003967 RID: 14695 RVA: 0x00689250 File Offset: 0x00689250
      public double Baseline { get; set; }

      // Token: 0x06003968 RID: 14696 RVA: 0x0068925C File Offset: 0x0068925C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("GlyphBox");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003969 RID: 14697 RVA: 0x006892A8 File Offset: 0x006892A8
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("Left = ");
        builder.Append(this.Left.ToString());
        builder.Append(", Right = ");
        builder.Append(this.Right.ToString());
        builder.Append(", Top = ");
        builder.Append(this.Top.ToString());
        builder.Append(", Bottom = ");
        builder.Append(this.Bottom.ToString());
        builder.Append(", Baseline = ");
        builder.Append(this.Baseline.ToString());
        return true;
      }

      // Token: 0x0600396A RID: 14698 RVA: 0x0068937C File Offset: 0x0068937C
      [CompilerGenerated]
      public static bool operator !=(InkTextView.GlyphBox left, InkTextView.GlyphBox right)
      {
        return !(left == right);
      }

      // Token: 0x0600396B RID: 14699 RVA: 0x00689388 File Offset: 0x00689388
      [CompilerGenerated]
      public static bool operator ==(InkTextView.GlyphBox left, InkTextView.GlyphBox right)
      {
        return left.Equals(right);
      }

      // Token: 0x0600396C RID: 14700 RVA: 0x00689394 File Offset: 0x00689394
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((EqualityComparer<double>.Default.GetHashCode(this.<Left>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Right>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Top>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Bottom>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Baseline>k__BackingField);
      }

      // Token: 0x0600396D RID: 14701 RVA: 0x00689410 File Offset: 0x00689410
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is InkTextView.GlyphBox && this.Equals((InkTextView.GlyphBox)obj);
      }

      // Token: 0x0600396E RID: 14702 RVA: 0x00689428 File Offset: 0x00689428
      [CompilerGenerated]
      public bool Equals(InkTextView.GlyphBox other)
      {
        return EqualityComparer<double>.Default.Equals(this.<Left>k__BackingField, other.<Left>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Right>k__BackingField, other.<Right>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Top>k__BackingField, other.<Top>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Bottom>k__BackingField, other.<Bottom>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Baseline>k__BackingField, other.<Baseline>k__BackingField);
      }

      // Token: 0x0600396F RID: 14703 RVA: 0x006894B0 File Offset: 0x006894B0
      [CompilerGenerated]
      public void Deconstruct(out double Left, out double Right, out double Top, out double Bottom, out double Baseline)
      {
        Left = this.Left;
        Right = this.Right;
        Top = this.Top;
        Bottom = this.Bottom;
        Baseline = this.Baseline;
      }
    }
  }
}
