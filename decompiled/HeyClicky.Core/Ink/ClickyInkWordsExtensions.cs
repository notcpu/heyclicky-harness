using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200013E RID: 318
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkWordsExtensions
  {
    // Token: 0x0600140D RID: 5133 RVA: 0x003AB1FC File Offset: 0x003AB1FC
    public static double FinishOfWriting(this ClickyInkLiveNote.Words words, double time)
    {
      double? num = null;
      foreach (ClickyInkLiveNote.Segment segment in words.Segments)
      {
        double? num2 = null;
        InkRange? inkRange = segment.Written;
        if (inkRange == null)
        {
          goto IL_0068;
        }
        InkRange valueOrDefault = inkRange.GetValueOrDefault();
        if (valueOrDefault.LowerBound < time - 0.001)
        {
          goto IL_0068;
        }
        num2 = new double?(valueOrDefault.UpperBound);
        IL_00A5:
        if (num2 != null)
        {
          double valueOrDefault2 = num2.GetValueOrDefault();
          if (num != null)
          {
            double num3 = valueOrDefault2;
            double? num4 = num;
            if (!((num3 > num4.GetValueOrDefault()) & (num4 != null)))
            {
              continue;
            }
          }
          num = new double?(valueOrDefault2);
          continue;
        }
        continue;
        IL_0068:
        inkRange = segment.Struck;
        if (inkRange == null)
        {
          goto IL_00A5;
        }
        InkRange valueOrDefault3 = inkRange.GetValueOrDefault();
        if (valueOrDefault3.LowerBound >= time - 0.001)
        {
          num2 = new double?(valueOrDefault3.UpperBound);
          goto IL_00A5;
        }
        goto IL_00A5;
      }
      if (num != null)
      {
        return num.GetValueOrDefault();
      }
      double? num5 = null;
      foreach (ClickyInkLiveNote.Segment segment2 in words.Segments)
      {
        InkRange? inkRange = segment2.Written;
        if (inkRange != null)
        {
          InkRange valueOrDefault4 = inkRange.GetValueOrDefault();
          if (num5 != null)
          {
            double upperBound = valueOrDefault4.UpperBound;
            double? num4 = num5;
            if (!((upperBound > num4.GetValueOrDefault()) & (num4 != null)))
            {
              continue;
            }
          }
          num5 = new double?(valueOrDefault4.UpperBound);
        }
      }
      return num5.GetValueOrDefault();
    }

    // Token: 0x0600140E RID: 5134 RVA: 0x003AB3B8 File Offset: 0x003AB3B8
    [return: TupleElementNames(new string[] { "Head", "Tail" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    public static ValueTuple<ClickyInkLiveNote.Words, ClickyInkLiveNote.Words>? Split(this ClickyInkLiveNote.Words words, string separator)
    {
      string text = string.Concat(words.Segments.Select<ClickyInkLiveNote.Segment, string>((ClickyInkLiveNote.Segment s) => s.Text));
      int num = text.Find(separator);
      if (num < 0)
      {
        return null;
      }
      int num2 = text.Substring(0, num).CharacterCount();
      int num3 = text.Substring(0, num + separator.Length).CharacterCount();
      List<ClickyInkLiveNote.Segment> list = new List<ClickyInkLiveNote.Segment>();
      List<ClickyInkLiveNote.Segment> list2 = new List<ClickyInkLiveNote.Segment>();
      int num4 = 0;
      foreach (ClickyInkLiveNote.Segment segment in words.Segments)
      {
        ClickyInkWordsExtensions.<>c__DisplayClass1_0 CS$<>8__locals1;
        CS$<>8__locals1.segment = segment;
        ClickyInkWordsExtensions.<>c__DisplayClass1_1 CS$<>8__locals2;
        CS$<>8__locals2.characters = CS$<>8__locals1.segment.Text.Characters();
        CS$<>8__locals2.length = CS$<>8__locals2.characters.Length;
        CS$<>8__locals2.start = num4;
        CS$<>8__locals2.end = num4 + CS$<>8__locals2.length;
        num4 = CS$<>8__locals2.end;
        ClickyInkLiveNote.Segment? segment2 = ClickyInkWordsExtensions.<Split>g__Piece|1_1(0, num2, ref CS$<>8__locals1, ref CS$<>8__locals2);
        if (segment2 != null)
        {
          ClickyInkLiveNote.Segment valueOrDefault = segment2.GetValueOrDefault();
          list.Add(valueOrDefault);
        }
        segment2 = ClickyInkWordsExtensions.<Split>g__Piece|1_1(num3, int.MaxValue, ref CS$<>8__locals1, ref CS$<>8__locals2);
        if (segment2 != null)
        {
          ClickyInkLiveNote.Segment valueOrDefault2 = segment2.GetValueOrDefault();
          list2.Add(valueOrDefault2);
        }
      }
      return new ValueTuple<ClickyInkLiveNote.Words, ClickyInkLiveNote.Words>?(new ValueTuple<ClickyInkLiveNote.Words, ClickyInkLiveNote.Words>(new ClickyInkLiveNote.Words(list, null, null), new ClickyInkLiveNote.Words(list2, null, null)));
    }

    // Token: 0x0600140F RID: 5135 RVA: 0x003AB56C File Offset: 0x003AB56C
    [CompilerGenerated]
    internal static ClickyInkLiveNote.Segment? <Split>g__Piece|1_1(int from, int to, ref ClickyInkWordsExtensions.<>c__DisplayClass1_0 A_2, ref ClickyInkWordsExtensions.<>c__DisplayClass1_1 A_3)
    {
      int num = Math.Max(from, A_3.start);
      int num2 = Math.Min(to, A_3.end);
      if (num >= num2)
      {
        return null;
      }
      ClickyInkLiveNote.Segment segment = A_2.segment;
      segment.Text = string.Concat(RuntimeHelpers.GetSubArray<string>(A_3.characters, new Range(num - A_3.start, num2 - A_3.start)));
      ClickyInkLiveNote.Segment segment2 = segment;
      InkRange? written = A_2.segment.Written;
      if (written != null)
      {
        InkRange valueOrDefault = written.GetValueOrDefault();
        if (A_3.length > 0)
        {
          double num3 = valueOrDefault.UpperBound - valueOrDefault.LowerBound;
          double num4 = valueOrDefault.LowerBound + num3 * (double)(num - A_3.start) / (double)A_3.length;
          double num5 = valueOrDefault.LowerBound + num3 * (double)(num2 - A_3.start) / (double)A_3.length;
          segment = segment2;
          segment.Written = new InkRange?(new InkRange(num4, Math.Max(num4, num5)));
          segment2 = segment;
        }
      }
      return new ClickyInkLiveNote.Segment?(segment2);
    }
  }
}
