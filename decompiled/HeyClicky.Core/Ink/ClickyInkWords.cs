using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.UI;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000185 RID: 389
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkWords
  {
    // Token: 0x06001753 RID: 5971 RVA: 0x003C2DCC File Offset: 0x003C2DCC
    public static ClickyInkTextContent Text(ClickyInkLiveNote.Words words, double clock, ClickyInkStyle style, bool pencil = true, Color? color = null, InkRange? struckOver = null)
    {
      Color color2 = color ?? style.Ink;
      ClickyInkWords.Builder builder = new ClickyInkWords.Builder(style);
      IReadOnlyList<ClickyInkLiveNote.Segment> segments = words.Segments;
      int num = -1;
      int i = segments.Count - 1;
      while (i >= 0)
      {
        ClickyInkLiveNote.Segment segment = segments[i];
        double? num2 = segment.VanishesAt;
        if (num2 == null)
        {
          goto IL_0063;
        }
        double valueOrDefault = num2.GetValueOrDefault();
        if (clock < valueOrDefault)
        {
          goto IL_0063;
        }
        IL_008D:
        i--;
        continue;
        IL_0063:
        InkRange? inkRange = segment.Written;
        if (inkRange == null || clock >= inkRange.GetValueOrDefault().LowerBound)
        {
          num = i;
          break;
        }
        goto IL_008D;
      }
      int? num3 = null;
      if (pencil)
      {
        ClickyInkPenSchedule penSchedule = style.PenSchedule;
        if (penSchedule != null && !penSchedule.IsWriting(clock))
        {
          for (int j = 0; j < segments.Count; j++)
          {
            ClickyInkLiveNote.Segment segment2 = segments[j];
            InkRange? inkRange = segment2.Written;
            if (inkRange != null)
            {
              InkRange valueOrDefault2 = inkRange.GetValueOrDefault();
              double? num2 = segment2.VanishesAt;
              if (num2 != null)
              {
                double valueOrDefault3 = num2.GetValueOrDefault();
                if (clock >= valueOrDefault3)
                {
                  goto IL_0135;
                }
              }
              if (valueOrDefault2.LowerBound > clock && valueOrDefault2.LowerBound <= clock + 0.35)
              {
                num3 = new int?(j);
                break;
              }
            }
            IL_0135:;
          }
        }
      }
      int k = 0;
      while (k < segments.Count)
      {
        ClickyInkLiveNote.Segment segment3 = segments[k];
        double? num2 = segment3.VanishesAt;
        if (num2 == null)
        {
          goto IL_017A;
        }
        double valueOrDefault4 = num2.GetValueOrDefault();
        if (clock < valueOrDefault4)
        {
          goto IL_017A;
        }
        IL_04A3:
        k++;
        continue;
        IL_017A:
        bool flag;
        if (segment3.Edit != null)
        {
          int? edit = segment3.Edit;
          int? num4 = style.LatestEdit;
          flag = (edit.GetValueOrDefault() == num4.GetValueOrDefault()) & (edit != null == (num4 != null));
        }
        else
        {
          flag = false;
        }
        bool flag2 = flag;
        InkRange? inkRange = segment3.Written;
        if (inkRange != null)
        {
          InkRange valueOrDefault5 = inkRange.GetValueOrDefault();
          if (clock < valueOrDefault5.LowerBound)
          {
            int num5 = k;
            int? num4 = num3;
            if ((num5 == num4.GetValueOrDefault()) & (num4 != null))
            {
              builder.AddPoised(segment3.Text, color2);
              goto IL_04A3;
            }
            num2 = words.ReservedUntil;
            if (num2 == null)
            {
              goto IL_04A3;
            }
            double valueOrDefault6 = num2.GetValueOrDefault();
            if (valueOrDefault5.LowerBound < valueOrDefault6 && k > num)
            {
              builder.AddClear(segment3.Text);
              goto IL_04A3;
            }
            goto IL_04A3;
          }
          else
          {
            double num6 = ClickyInkClock.Progress(clock, valueOrDefault5.LowerBound, valueOrDefault5.UpperBound);
            if (num6 < 1.0)
            {
              builder.AddBeingWritten(segment3.Text, num6, flag2 ? style.Fresh : color2, pencil, color2);
              goto IL_04A3;
            }
          }
        }
        int num7 = 0;
        while (num7 < segment3.Text.Length && segment3.Text[num7] == ' ')
        {
          num7++;
        }
        string text = segment3.Text.Substring(0, num7);
        if (text.Length > 0 && segment3.Text.Length > text.Length)
        {
          builder.Add(new ClickyInkRun(text, null, false, null, null, false, null, null));
        }
        string text3;
        if (text.Length != 0 && segment3.Text.Length != text.Length)
        {
          string text2 = segment3.Text;
          int length = text.Length;
          text3 = text2.Substring(length, text2.Length - length);
        }
        else
        {
          text3 = segment3.Text;
        }
        string text4 = text3;
        inkRange = segment3.Struck;
        if (inkRange != null)
        {
          InkRange valueOrDefault7 = inkRange.GetValueOrDefault();
          if (clock >= valueOrDefault7.LowerBound)
          {
            builder.AddStruck(text4, ClickyInkClock.Progress(clock, valueOrDefault7.LowerBound, valueOrDefault7.UpperBound), color2);
            goto IL_04A3;
          }
        }
        ClickyInkRun clickyInkRun = new ClickyInkRun(text4, new Color?(flag2 ? style.Fresh : color2), false, null, null, false, null, null);
        if (flag2)
        {
          inkRange = segment3.Written;
          double num8 = ((inkRange != null) ? inkRange.GetValueOrDefault().UpperBound : double.NegativeInfinity);
          double num9 = ClickyInkClock.EaseOut(ClickyInkClock.Progress(clock, num8 + 0.12, num8 + 0.12 + 0.45));
          if (num9 > 0.0)
          {
            ClickyInkRun clickyInkRun2 = clickyInkRun;
            clickyInkRun2.Background = new Color?(ClickyInkWords.Multiply(style.Highlighter, num9));
            clickyInkRun = clickyInkRun2;
          }
        }
        builder.Add(clickyInkRun);
        goto IL_04A3;
      }
      if (struckOver != null)
      {
        InkRange valueOrDefault8 = struckOver.GetValueOrDefault();
        if (clock >= valueOrDefault8.LowerBound)
        {
          builder.StrikeEverything(ClickyInkClock.Progress(clock, valueOrDefault8.LowerBound, valueOrDefault8.UpperBound));
        }
      }
      ClickyInkTextContent clickyInkTextContent = builder.Result();
      clickyInkTextContent.PenClock = clock;
      return clickyInkTextContent;
    }

    // Token: 0x06001754 RID: 5972 RVA: 0x003C32D4 File Offset: 0x003C32D4
    public static bool IsStarted(ClickyInkLiveNote.Words words, double clock)
    {
      return words.Segments.Any<ClickyInkLiveNote.Segment>(delegate(ClickyInkLiveNote.Segment s)
      {
        InkRange? written = s.Written;
        if (written != null)
        {
          InkRange valueOrDefault = written.GetValueOrDefault();
          if (clock < valueOrDefault.LowerBound)
          {
            return false;
          }
        }
        double? vanishesAt = s.VanishesAt;
        if (vanishesAt != null)
        {
          double valueOrDefault2 = vanishesAt.GetValueOrDefault();
          return clock < valueOrDefault2;
        }
        return true;
      });
    }

    // Token: 0x06001755 RID: 5973 RVA: 0x003C3308 File Offset: 0x003C3308
    public static bool MayHavePencil(this ClickyInkLiveNote.Words words, double clock)
    {
      return words.Segments.Any<ClickyInkLiveNote.Segment>(delegate(ClickyInkLiveNote.Segment s)
      {
        InkRange? written = s.Written;
        if (written != null)
        {
          InkRange valueOrDefault = written.GetValueOrDefault();
          if (clock >= valueOrDefault.LowerBound - 0.35)
          {
            return clock < valueOrDefault.UpperBound;
          }
        }
        return false;
      });
    }

    // Token: 0x06001756 RID: 5974 RVA: 0x003C333C File Offset: 0x003C333C
    public static Color Multiply(Color color, double opacity)
    {
      return Color.FromArgb((byte)Math.Round(Math.Clamp(opacity, 0.0, 1.0) * (double)color.A), color.R, color.G, color.B);
    }

    // Token: 0x02000561 RID: 1377
    [Nullable(0)]
    private sealed class Builder
    {
      // Token: 0x0600349D RID: 13469 RVA: 0x0043FA04 File Offset: 0x0043FA04
      public Builder(ClickyInkStyle style)
      {
      }

      // Token: 0x17000957 RID: 2391
      // (get) Token: 0x0600349E RID: 13470 RVA: 0x0043FA34 File Offset: 0x0043FA34
      private bool HasPen
      {
        get
        {
          ClickyInkPenAttribute? clickyInkPenAttribute = this.pen;
          return clickyInkPenAttribute != null;
        }
      }

      // Token: 0x0600349F RID: 13471 RVA: 0x0043FA58 File Offset: 0x0043FA58
      public void Add(ClickyInkRun run)
      {
        ClickyInkWords.Builder.<>c__DisplayClass13_0 CS$<>8__locals1;
        CS$<>8__locals1.run = run;
        CS$<>8__locals1.<>4__this = this;
        string text = CS$<>8__locals1.run.Text;
        bool flag = false;
        string text2 = text;
        for (int i = 0; i < text2.Length; i++)
        {
          if (ClickyInkParser.Emphasis.IsMarker(text2[i]))
          {
            flag = true;
            break;
          }
        }
        if (!flag && !this.isBold && !this.isHighlighted && !this.isLink)
        {
          this.Append(CS$<>8__locals1.run);
          return;
        }
        CS$<>8__locals1.piece = new StringBuilder();
        foreach (char c in text)
        {
          switch (c)
          {
          case '\ue001':
            this.<Add>g__Flush|13_0(ref CS$<>8__locals1);
            this.isBold = true;
            break;
          case '\ue002':
            this.<Add>g__Flush|13_0(ref CS$<>8__locals1);
            this.isBold = false;
            break;
          case '\ue003':
            this.<Add>g__Flush|13_0(ref CS$<>8__locals1);
            this.isHighlighted = true;
            break;
          case '\ue004':
            this.<Add>g__Flush|13_0(ref CS$<>8__locals1);
            this.isHighlighted = false;
            break;
          case '\ue005':
            this.<Add>g__Flush|13_0(ref CS$<>8__locals1);
            this.isLink = true;
            break;
          case '\ue006':
            this.<Add>g__Flush|13_0(ref CS$<>8__locals1);
            this.isLink = false;
            break;
          default:
            CS$<>8__locals1.piece.Append(c);
            break;
          }
        }
        this.<Add>g__Flush|13_0(ref CS$<>8__locals1);
      }

      // Token: 0x060034A0 RID: 13472 RVA: 0x0043FBAC File Offset: 0x0043FBAC
      private ClickyInkRun StyleAsLink(ClickyInkRun run)
      {
        Color? foreground = run.Foreground;
        if (foreground != null)
        {
          Color valueOrDefault = foreground.GetValueOrDefault();
          if (!(valueOrDefault != this.<style>P.Ink) || !(valueOrDefault != this.<style>P.Fresh))
          {
            ClickyInkRun clickyInkRun = run;
            clickyInkRun.Foreground = new Color?(this.<style>P.Fresh);
            clickyInkRun.IsLink = true;
            clickyInkRun.LinkTint = new Color?(this.<style>P.Fresh);
            return clickyInkRun;
          }
        }
        return run;
      }

      // Token: 0x060034A1 RID: 13473 RVA: 0x0043FC34 File Offset: 0x0043FC34
      private void Append(ClickyInkRun run)
      {
        ClickyInkWords.Builder.Target target = this.target;
        if (target == ClickyInkWords.Builder.Target.Before)
        {
          this.before.Add(run);
          return;
        }
        if (target != ClickyInkWords.Builder.Target.PenGlyph)
        {
          this.after.Add(run);
          return;
        }
        this.penGlyph.Add(run);
      }

      // Token: 0x060034A2 RID: 13474 RVA: 0x0043FC78 File Offset: 0x0043FC78
      public void AddBeingWritten(string text, double progress, Color color, bool pencil, Color pencilTint)
      {
        ValueTuple<int, double> valueTuple = ClickyInkPen.Revealed(text, progress);
        int item = valueTuple.Item1;
        double item2 = valueTuple.Item2;
        string[] array = text.Characters();
        string text2 = string.Concat(array.Take<string>(item));
        int num = 0;
        while (num < text2.Length && text2[num] == ' ')
        {
          num++;
        }
        if (num > 0)
        {
          this.Add(new ClickyInkRun(text2.Substring(0, num), null, false, null, null, false, null, null));
        }
        string text3 = text2;
        int num2 = num;
        this.Add(new ClickyInkRun(text3.Substring(num2, text3.Length - num2), new Color?(color), false, null, null, false, null, null));
        if (item >= array.Length)
        {
          return;
        }
        string text4 = array[item];
        Color color2 = ((this.isLink && (color == this.<style>P.Ink || color == this.<style>P.Fresh)) ? this.<style>P.Fresh : color);
        ClickyInkRun clickyInkRun = new ClickyInkRun(text4, new Color?((item2 > 0.0) ? ClickyInkWords.Multiply(color2, 0.2 + 0.8 * item2) : ClickyInkRun.Clear), false, null, null, false, null, null);
        ClickyInkRun clickyInkRun2 = new ClickyInkRun(string.Concat(array.Skip<string>(item + 1)), new Color?(ClickyInkRun.Clear), false, null, null, false, null, null);
        if (pencil && !this.HasPen)
        {
          this.target = ClickyInkWords.Builder.Target.PenGlyph;
          this.Add(clickyInkRun);
          this.pen = new ClickyInkPenAttribute?(new ClickyInkPenAttribute(item2, pencilTint));
          this.target = ClickyInkWords.Builder.Target.After;
          this.Add(clickyInkRun2);
          return;
        }
        this.Add(clickyInkRun);
        this.Add(clickyInkRun2);
      }

      // Token: 0x060034A3 RID: 13475 RVA: 0x0043FE94 File Offset: 0x0043FE94
      public void AddPoised(string text, Color pencilTint)
      {
        if (this.HasPen)
        {
          this.AddClear(text);
          return;
        }
        this.AddBeingWritten(text, 0.0, ClickyInkRun.Clear, true, pencilTint);
      }

      // Token: 0x060034A4 RID: 13476 RVA: 0x0043FEC0 File Offset: 0x0043FEC0
      public void AddClear(string text)
      {
        this.Add(new ClickyInkRun(text, new Color?(ClickyInkRun.Clear), false, null, null, false, null, null));
      }

      // Token: 0x060034A5 RID: 13477 RVA: 0x0043FF04 File Offset: 0x0043FF04
      public void AddStruck(string text, double progress, Color color)
      {
        int num = 0;
        while (num < text.Length && text[text.Length - 1 - num] == ' ')
        {
          num++;
        }
        string[] array = text.Substring(0, text.Length - num).Characters();
        int num2 = (int)Math.Ceiling((double)array.Length * ClickyInkClock.EaseInOut(progress));
        string text2 = string.Concat(array.Take<string>(num2));
        Color? color2 = new Color?(ClickyInkWords.Multiply(this.<style>P.Quiet, 0.75));
        bool flag = false;
        Color? color3 = new Color?(this.<style>P.Fresh);
        this.Add(new ClickyInkRun(text2, color2, flag, null, color3, false, null, null));
        if (num2 < array.Length)
        {
          string text3 = string.Concat(array.Skip<string>(num2));
          Color? color4 = new Color?(color);
          bool flag2 = false;
          color3 = null;
          Color? color5 = color3;
          color3 = null;
          Color? color6 = color3;
          bool flag3 = false;
          color3 = null;
          this.Add(new ClickyInkRun(text3, color4, flag2, color5, color6, flag3, color3, null));
        }
        if (num > 0)
        {
          string text4 = new string(' ', num);
          color3 = null;
          Color? color7 = color3;
          bool flag4 = false;
          color3 = null;
          Color? color8 = color3;
          color3 = null;
          Color? color9 = color3;
          bool flag5 = false;
          color3 = null;
          this.Add(new ClickyInkRun(text4, color7, flag4, color8, color9, flag5, color3, null));
        }
      }

      // Token: 0x060034A6 RID: 13478 RVA: 0x0044003C File Offset: 0x0044003C
      public void StrikeEverything(double progress)
      {
        int num = this.before.Sum<ClickyInkRun>((ClickyInkRun r) => r.Text.CharacterCount());
        int num2 = (int)Math.Ceiling((double)(num + this.penGlyph.Sum<ClickyInkRun>((ClickyInkRun r) => r.Text.CharacterCount())) * ClickyInkClock.EaseInOut(progress));
        this.before = this.Strike(this.before, Math.Min(num2, num));
        this.penGlyph = this.Strike(this.penGlyph, Math.Max(0, num2 - num));
      }

      // Token: 0x060034A7 RID: 13479 RVA: 0x004400E4 File Offset: 0x004400E4
      private List<ClickyInkRun> Strike(List<ClickyInkRun> runs, int count)
      {
        if (count <= 0)
        {
          return runs;
        }
        List<ClickyInkRun> list = new List<ClickyInkRun>();
        int num = 0;
        foreach (ClickyInkRun clickyInkRun in runs)
        {
          string[] array = clickyInkRun.Text.Characters();
          int num2 = Math.Max(0, num);
          int num3 = Math.Min(count, num + array.Length);
          if (num2 < num3 && !clickyInkRun.IsClear)
          {
            int num4 = num2 - num;
            int num5 = num3 - num;
            ClickyInkRun clickyInkRun2;
            if (num4 > 0)
            {
              List<ClickyInkRun> list2 = list;
              clickyInkRun2 = clickyInkRun;
              clickyInkRun2.Text = string.Concat(array.Take<string>(num4));
              list2.Add(clickyInkRun2);
            }
            List<ClickyInkRun> list3 = list;
            clickyInkRun2 = clickyInkRun;
            clickyInkRun2.Text = string.Concat(array.Skip<string>(num4).Take<string>(num5 - num4));
            clickyInkRun2.Foreground = new Color?(ClickyInkWords.Multiply(this.<style>P.Quiet, 0.7));
            clickyInkRun2.Strike = new Color?(this.<style>P.Fresh);
            clickyInkRun2.IsLink = false;
            clickyInkRun2.LinkTint = null;
            list3.Add(clickyInkRun2);
            if (num5 < array.Length)
            {
              List<ClickyInkRun> list4 = list;
              clickyInkRun2 = clickyInkRun;
              clickyInkRun2.Text = string.Concat(array.Skip<string>(num5));
              list4.Add(clickyInkRun2);
            }
          }
          else
          {
            list.Add(clickyInkRun);
          }
          num += array.Length;
        }
        return list;
      }

      // Token: 0x060034A8 RID: 13480 RVA: 0x00440270 File Offset: 0x00440270
      public ClickyInkTextContent Result()
      {
        ClickyInkTextContent clickyInkTextContent = new ClickyInkTextContent(this.before);
        ClickyInkPenAttribute? clickyInkPenAttribute = this.pen;
        if (clickyInkPenAttribute != null)
        {
          ClickyInkPenAttribute valueOrDefault = clickyInkPenAttribute.GetValueOrDefault();
          if (this.penGlyph.Sum<ClickyInkRun>((ClickyInkRun r) => r.Text.Length) > 0)
          {
            clickyInkTextContent.PenRun = clickyInkTextContent.Runs.Count;
            clickyInkTextContent.Pen = new ClickyInkPenAttribute?(valueOrDefault);
            clickyInkTextContent.Runs.Add(new ClickyInkRun(string.Concat(this.penGlyph.Select<ClickyInkRun, string>((ClickyInkRun r) => r.Text)), this.penGlyph[0].Foreground, this.penGlyph[0].IsBold, this.penGlyph[0].Background, this.penGlyph[0].Strike, false, null, null));
            goto IL_0146;
          }
        }
        clickyInkTextContent.Runs.AddRange(this.penGlyph.Select<ClickyInkRun, ClickyInkRun>(delegate(ClickyInkRun r)
        {
          ClickyInkRun clickyInkRun2 = r;
          clickyInkRun2.IsLink = false;
          clickyInkRun2.LinkTint = null;
          return clickyInkRun2;
        }));
        IL_0146:
        clickyInkTextContent.Runs.AddRange(this.after.Select<ClickyInkRun, ClickyInkRun>(delegate(ClickyInkRun r)
        {
          ClickyInkRun clickyInkRun3 = r;
          clickyInkRun3.IsLink = false;
          clickyInkRun3.LinkTint = null;
          return clickyInkRun3;
        }));
        clickyInkTextContent.Runs.RemoveAll((ClickyInkRun r) => r.Text.Length == 0 && r.Symbol == null);
        if (clickyInkTextContent.PenRun >= 0)
        {
          int num = 0;
          foreach (ClickyInkRun clickyInkRun in this.before)
          {
            if (clickyInkRun.Text.Length > 0)
            {
              num++;
            }
          }
          int num2 = num;
          clickyInkTextContent.PenRun = num2;
        }
        return clickyInkTextContent;
      }

      // Token: 0x060034A9 RID: 13481 RVA: 0x00440498 File Offset: 0x00440498
      [CompilerGenerated]
      private void <Add>g__Flush|13_0(ref ClickyInkWords.Builder.<>c__DisplayClass13_0 A_1)
      {
        if (A_1.piece.Length == 0)
        {
          return;
        }
        ClickyInkRun clickyInkRun = A_1.run;
        clickyInkRun.Text = A_1.piece.ToString();
        ClickyInkRun clickyInkRun2 = clickyInkRun;
        if (this.isBold)
        {
          clickyInkRun = clickyInkRun2;
          clickyInkRun.IsBold = true;
          clickyInkRun2 = clickyInkRun;
        }
        if (this.isHighlighted && !clickyInkRun2.IsClear)
        {
          clickyInkRun = clickyInkRun2;
          clickyInkRun.Background = new Color?(this.<style>P.Highlighter);
          clickyInkRun2 = clickyInkRun;
        }
        if (this.isLink)
        {
          clickyInkRun2 = this.StyleAsLink(clickyInkRun2);
        }
        this.Append(clickyInkRun2);
        A_1.piece.Clear();
      }

      // Token: 0x0400205A RID: 8282
      [CompilerGenerated]
      private ClickyInkStyle <style>P = style;

      // Token: 0x0400205B RID: 8283
      private List<ClickyInkRun> before = new List<ClickyInkRun>();

      // Token: 0x0400205C RID: 8284
      private List<ClickyInkRun> penGlyph = new List<ClickyInkRun>();

      // Token: 0x0400205D RID: 8285
      private readonly List<ClickyInkRun> after = new List<ClickyInkRun>();

      // Token: 0x0400205E RID: 8286
      private ClickyInkPenAttribute? pen;

      // Token: 0x0400205F RID: 8287
      private bool isBold;

      // Token: 0x04002060 RID: 8288
      private bool isHighlighted;

      // Token: 0x04002061 RID: 8289
      private bool isLink;

      // Token: 0x04002062 RID: 8290
      private ClickyInkWords.Builder.Target target;

      // Token: 0x0200070C RID: 1804
      [NullableContext(0)]
      private enum Target
      {
        // Token: 0x0400268A RID: 9866
        Before,
        // Token: 0x0400268B RID: 9867
        PenGlyph,
        // Token: 0x0400268C RID: 9868
        After
      }
    }
  }
}
