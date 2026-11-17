using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000145 RID: 325
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkDashboardViewMath
  {
    // Token: 0x06001479 RID: 5241 RVA: 0x003AD1E4 File Offset: 0x003AD1E4
    [NullableContext(2)]
    public static string DeltaWordsWithoutAChange(string given)
    {
      string text = ((given != null) ? given.TrimWs() : null);
      if (!string.IsNullOrEmpty(text))
      {
        IEnumerable<char> enumerable = text;
        Func<char, bool> func;
        if ((func = ClickyInkDashboardViewMath.<>O.<0>__IsDigit) == null)
        {
          func = (ClickyInkDashboardViewMath.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
        }
        if (!enumerable.Any<char>(func))
        {
          string lower = text.Lowered();
          if (!new string[] { "up", "down", "fell", "rose", "flat", "same", "no change" }.Any<string>((string w) => lower.Contains(w, StringComparison.Ordinal)))
          {
            return text;
          }
          return null;
        }
      }
      return null;
    }

    // Token: 0x0600147A RID: 5242 RVA: 0x003AD294 File Offset: 0x003AD294
    [NullableContext(2)]
    [return: TupleElementNames(new string[] { "Text", "Value" })]
    [return: Nullable(new byte[] { 0, 1 })]
    public static ValueTuple<string, double>? ResolvedDelta(string given, [TupleElementNames(new string[] { "Text", "Value" })] [Nullable(new byte[] { 0, 1 })] ValueTuple<string, double>? computed)
    {
      string text = ((given != null) ? given.TrimWs() : null);
      if (!string.IsNullOrEmpty(text))
      {
        string text2 = text.Lowered();
        bool flag = text.HasPrefix("-") || text.HasPrefix("−") || text2.Contains("down", StringComparison.Ordinal) || text2.Contains("fell", StringComparison.Ordinal);
        bool flag2 = text2 == "0" || text2 == "flat" || text2 == "same" || text2 == "no change";
        bool flag3 = flag2;
        return new ValueTuple<string, double>?(new ValueTuple<string, double>(text, (double)(flag3 ? 0 : (flag ? (-1) : 1))));
      }
      if (computed != null)
      {
        ValueTuple<string, double> valueOrDefault = computed.GetValueOrDefault();
        if (valueOrDefault.Item2 != 0.0)
        {
          return new ValueTuple<string, double>?(valueOrDefault);
        }
      }
      return null;
    }

    // Token: 0x0600147B RID: 5243 RVA: 0x003AD388 File Offset: 0x003AD388
    public static string DeltaArrow(double value)
    {
      if (value > 0.0)
      {
        return "↑";
      }
      if (value >= 0.0)
      {
        return "=";
      }
      return "↓";
    }

    // Token: 0x0600147C RID: 5244 RVA: 0x003AD3B4 File Offset: 0x003AD3B4
    public static string DeltaWords(string text)
    {
      return text.TrimSet("+-−");
    }

    // Token: 0x0600147D RID: 5245 RVA: 0x003AD3C4 File Offset: 0x003AD3C4
    public static bool? DeltaIsGoodNews(double delta, ClickyInkDocument.Block block)
    {
      if (delta == 0.0)
      {
        return null;
      }
      string text = block["good"];
      string text2 = ((text != null) ? text.Lowered() : null) ?? "";
      bool flag = text2 == "down" || text2 == "lower" || text2 == "less";
      bool flag2 = flag;
      return new bool?(delta > 0.0 != flag2);
    }

    // Token: 0x0600147E RID: 5246 RVA: 0x003AD450 File Offset: 0x003AD450
    public static string ProgressValueKey(ClickyInkDocument.Block block)
    {
      return new string[] { "value", "progress", "percent", "done" }.FirstOrDefault<string>((string k) => block[k] != null) ?? "value";
    }

    // Token: 0x0600147F RID: 5247 RVA: 0x003AD4AC File Offset: 0x003AD4AC
    public static double? ProgressPreviousFraction(ClickyInkDocument.Block block)
    {
      if (ClickyInkChange.ChangedAt(block) == null)
      {
        return null;
      }
      Dictionary<string, string> dictionary = new Dictionary<string, string>(block.Attributes);
      bool flag = false;
      foreach (string text in ClickyInkDashboardViewMath.ProgressKeys)
      {
        string text2 = ClickyInkChange.Previous(block, text);
        if (text2 != null)
        {
          dictionary[text] = text2;
          flag = true;
        }
      }
      if (!flag)
      {
        return null;
      }
      double? fraction = new ClickyInkProgress(dictionary).Fraction;
      if (fraction != null)
      {
        double valueOrDefault = fraction.GetValueOrDefault();
        return new double?(Math.Min(1.0, Math.Max(0.0, valueOrDefault)));
      }
      return null;
    }

    // Token: 0x06001480 RID: 5248 RVA: 0x003AD578 File Offset: 0x003AD578
    [return: Nullable(2)]
    public static string ProgressPreviousShown(ClickyInkDocument.Block block)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>(block.Attributes);
      foreach (string text in ClickyInkDashboardViewMath.ProgressKeys)
      {
        string text2 = ClickyInkChange.Previous(block, text);
        if (text2 != null)
        {
          dictionary[text] = text2;
        }
      }
      return new ClickyInkProgress(dictionary).Shown;
    }

    // Token: 0x06001481 RID: 5249 RVA: 0x003AD5D0 File Offset: 0x003AD5D0
    public static int TallyCapacity(double width, int rows)
    {
      return Math.Max(5, (int)((width + 9.0) / 35.0) * 5 * rows);
    }

    // Token: 0x06001482 RID: 5250 RVA: 0x003AD5F4 File Offset: 0x003AD5F4
    public static int TallyGroupsPerRow(double width)
    {
      return Math.Max(1, (int)((width + 9.0) / 35.0));
    }

    // Token: 0x06001483 RID: 5251 RVA: 0x003AD614 File Offset: 0x003AD614
    public static int TileColumns(int count, double width, bool isWindowPreview)
    {
      if (!isWindowPreview)
      {
        int num = Math.Max(1, Math.Min((int)(width / 176.0), 4));
        int num2 = (count + num - 1) / num;
        return Math.Max(1, (count + Math.Max(num2, 1) - 1) / Math.Max(num2, 1));
      }
      if (count <= 4)
      {
        return Math.Max(count, 1);
      }
      return (count + 1) / 2;
    }

    // Token: 0x06001484 RID: 5252 RVA: 0x003AD670 File Offset: 0x003AD670
    public static double TileHeight(ClickyInkDocument.Block block, bool stacksChange, bool compact = false)
    {
      ClickyInkDocument.Kind kind = block.Kind;
      int num;
      if (kind != ClickyInkDocument.Kind.Paragraph)
      {
        if (kind != ClickyInkDocument.Kind.Figure)
        {
          switch (kind)
          {
          case ClickyInkDocument.Kind.Spark:
            num = 34;
            goto IL_006C;
          case ClickyInkDocument.Kind.Progress:
            num = 20;
            goto IL_006C;
          case ClickyInkDocument.Kind.Tally:
            num = 42;
            goto IL_006C;
          case ClickyInkDocument.Kind.Streak:
            num = 18;
            goto IL_006C;
          case ClickyInkDocument.Kind.Countdown:
            num = 16;
            goto IL_006C;
          }
          num = 0;
        }
        else
        {
          num = ((block["values"] != null) ? 30 : 0);
        }
      }
      else
      {
        num = 20;
      }
      IL_006C:
      double num2 = (double)num;
      string text = block["note"];
      double num3 = (double)((text != null && text.Length > 0) ? (compact ? 17 : 20) : 0);
      double num4 = (stacksChange ? 17.0 : 0.0);
      return 58.0 + num2 + num3 + num4 + (double)(compact ? 6 : 16);
    }

    // Token: 0x06001485 RID: 5253 RVA: 0x003AD748 File Offset: 0x003AD748
    [return: TupleElementNames(new string[] { "Number", "NumberSize", "Change" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    public static ValueTuple<string, double, string>? TileChangeMeasure(ClickyInkDocument.Block block)
    {
      string text = block["delta"];
      string text2 = ((text != null) ? text.TrimWs() : null);
      if (text2 == null || text2.Length <= 0)
      {
        return null;
      }
      ClickyInkDocument.Kind kind = block.Kind;
      string text4;
      double num;
      if (kind != ClickyInkDocument.Kind.Figure)
      {
        if (kind != ClickyInkDocument.Kind.Spark)
        {
          return null;
        }
        string last = new ClickyInkSparkReading(block).Last;
        string text3 = block["unit"];
        text4 = last + ((text3 != null) ? (" " + text3) : "");
        num = 26.0;
      }
      else
      {
        string text5;
        if ((text5 = block["value"]) == null)
        {
          text5 = block["big"] ?? "";
        }
        text4 = text5;
        num = 28.0;
      }
      return new ValueTuple<string, double, string>?(new ValueTuple<string, double, string>(text4, num, "↑ " + ClickyInkDashboardViewMath.DeltaWords(text2)));
    }

    // Token: 0x06001486 RID: 5254 RVA: 0x003AD830 File Offset: 0x003AD830
    public static bool TileStacksChange(double numberWidth, int numberCharacters, double changeWidth, double width)
    {
      return numberWidth - 0.8 * (double)numberCharacters + 6.0 + changeWidth > width - 24.0;
    }

    // Token: 0x06001487 RID: 5255 RVA: 0x003AD85C File Offset: 0x003AD85C
    public static string StateWord(string word)
    {
      if (word.CharacterCount() > 3)
      {
        return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(word.ToLowerInvariant());
      }
      return word.ToUpperInvariant();
    }

    // Token: 0x06001488 RID: 5256 RVA: 0x003AD884 File Offset: 0x003AD884
    [return: Nullable(2)]
    public static ClickyInkRoutineControl RunStampRoutine(ClickyInkDocument.Block block, IReadOnlyList<ClickyInkRoutineControl> routines)
    {
      string words = (block.Text + " " + block["extra"]).Lowered();
      return routines.FirstOrDefault<ClickyInkRoutineControl>((ClickyInkRoutineControl r) => r.Title.Length > 0 && words.Contains(r.Title.Lowered(), StringComparison.Ordinal)) ?? routines.FirstOrDefault<ClickyInkRoutineControl>();
    }

    // Token: 0x06001489 RID: 5257 RVA: 0x003AD8DC File Offset: 0x003AD8DC
    public static List<int> RoutineIntervalOptions(int interval)
    {
      int[] array = new int[] { 60, 240, 1440, 10080 };
      if (!array.Contains(interval))
      {
        return (from m in array.Append(interval)
          orderby m
          select m).ToList<int>();
      }
      return array.ToList<int>();
    }

    // Token: 0x0600148A RID: 5258 RVA: 0x003AD93C File Offset: 0x003AD93C
    public static string RunCountWords(int runCount)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 2);
      defaultInterpolatedStringHandler.AppendLiteral("ran ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(runCount);
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted((runCount == 1) ? "time" : "times");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x0600148B RID: 5259 RVA: 0x003AD990 File Offset: 0x003AD990
    public static string RoutineStatusWords(ClickyInkRoutineControl routine, DateTimeOffset? now = null)
    {
      if (routine.IsPaused)
      {
        if (!routine.IsPausedAfterFailures)
        {
          return "paused";
        }
        return "paused after 3 failed runs";
      }
      else
      {
        if (routine.IsRunning)
        {
          return "running now";
        }
        return "next: " + ClickyInkRoutineControl.NextRunWords(routine.NextRunAt, now) + " · " + ClickyInkRoutineControl.CadenceWords(routine.IntervalMinutes).ToLowerInvariant();
      }
    }

    // Token: 0x0600148C RID: 5260 RVA: 0x003AD9F4 File Offset: 0x003AD9F4
    [NullableContext(2)]
    public static string PipelinePercent(double from, double to)
    {
      if (from <= 0.0)
      {
        return null;
      }
      return InkText.InkClamping(Math.Round(to / from * 100.0, MidpointRounding.AwayFromZero)).ToString(CultureInfo.InvariantCulture) + "%";
    }

    // Token: 0x0600148D RID: 5261 RVA: 0x003ADA40 File Offset: 0x003ADA40
    public static double PipelineBarWidth(double stageWidth, double value, double top, double appear)
    {
      return Math.Max(4.0, (stageWidth - 10.0) * (value / top) * appear);
    }

    // Token: 0x04000901 RID: 2305
    private static readonly string[] ProgressKeys = new string[] { "value", "progress", "percent", "done", "of", "total", "goal", "max", "target" };

    // Token: 0x04000902 RID: 2306
    public const double TallyGroupWidth = 26.0;

    // Token: 0x04000903 RID: 2307
    public const double TallyGroupGap = 9.0;

    // Token: 0x04000904 RID: 2308
    public const double TileHorizontalPadding = 12.0;

    // Token: 0x04000905 RID: 2309
    public const double TileStackedChangeHeight = 17.0;

    // Token: 0x020004C9 RID: 1225
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001DE9 RID: 7657
      [Nullable(0)]
      public static Func<char, bool> <0>__IsDigit;
    }
  }
}
