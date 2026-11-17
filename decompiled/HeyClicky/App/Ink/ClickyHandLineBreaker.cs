using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001B2 RID: 434
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyHandLineBreaker
  {
    // Token: 0x06001711 RID: 5905 RVA: 0x005BEE7C File Offset: 0x005BEE7C
    private static CanvasTextLayout Layout(string text, PaperFont font, double width, bool wraps)
    {
      CanvasTextLayout canvasTextLayout;
      using (CanvasTextFormat canvasTextFormat = font.TextFormat())
      {
        canvasTextFormat.WordWrapping = (wraps ? CanvasWordWrapping.Wrap : CanvasWordWrapping.NoWrap);
        canvasTextLayout = new CanvasTextLayout(CanvasDevice.GetSharedDevice(), text, canvasTextFormat, (float)Math.Min(width, 1000000.0), 1000000f);
      }
      return canvasTextLayout;
    }

    // Token: 0x06001712 RID: 5906 RVA: 0x005BEEDC File Offset: 0x005BEEDC
    public static double NaturalLineHeight(PaperFont font)
    {
      return ClickyHandLineBreaker.Metrics(font).Item1;
    }

    // Token: 0x06001713 RID: 5907 RVA: 0x005BEEEC File Offset: 0x005BEEEC
    public static double Ascent(PaperFont font)
    {
      return ClickyHandLineBreaker.Metrics(font).Item2;
    }

    // Token: 0x06001714 RID: 5908 RVA: 0x005BEEFC File Offset: 0x005BEEFC
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "Natural", "Ascent" })]
    private static ValueTuple<double, double> Metrics([Nullable(1)] PaperFont font)
    {
      Dictionary<PaperFont, ValueTuple<double, double>> dictionary = ClickyHandLineBreaker.metricsCache;
      ValueTuple<double, double> valueTuple2;
      lock (dictionary)
      {
        ValueTuple<double, double> valueTuple;
        if (ClickyHandLineBreaker.metricsCache.TryGetValue(font, out valueTuple))
        {
          valueTuple2 = valueTuple;
        }
        else
        {
          using (CanvasTextLayout canvasTextLayout = ClickyHandLineBreaker.Layout("Hg", font, 1000000.0, false))
          {
            CanvasLineMetrics canvasLineMetrics = canvasTextLayout.LineMetrics[0];
            ValueTuple<float, float> valueTuple3 = new ValueTuple<float, float>(canvasLineMetrics.Height, canvasLineMetrics.Baseline);
            Dictionary<PaperFont, ValueTuple<double, double>> dictionary2 = ClickyHandLineBreaker.metricsCache;
            ValueTuple<float, float> valueTuple4 = valueTuple3;
            dictionary2[font] = new ValueTuple<double, double>((double)valueTuple4.Item1, (double)valueTuple4.Item2);
            valueTuple4 = valueTuple3;
            valueTuple2 = new ValueTuple<double, double>((double)valueTuple4.Item1, (double)valueTuple4.Item2);
          }
        }
      }
      return valueTuple2;
    }

    // Token: 0x06001715 RID: 5909 RVA: 0x005BEFDC File Offset: 0x005BEFDC
    public unsafe static IReadOnlyList<string> Lines(string text, PaperFont font, double width, int? lineLimit)
    {
      if (string.IsNullOrEmpty(text))
      {
        return new <>z__ReadOnlySingleElementList<string>("");
      }
      double num = Math.Max(width, 1.0);
      List<string> list = new List<string>();
      IReadOnlyList<string> readOnlyList2;
      using (CanvasTextLayout canvasTextLayout = ClickyHandLineBreaker.Layout(text, font, num, true))
      {
        int num2 = 0;
        foreach (CanvasLineMetrics canvasLineMetrics in canvasTextLayout.LineMetrics)
        {
          if (num2 >= text.Length)
          {
            break;
          }
          if (lineLimit != null)
          {
            int valueOrDefault = lineLimit.GetValueOrDefault();
            if (list.Count == valueOrDefault - 1)
            {
              int num3 = num2;
              string text2 = text.Substring(num3, text.Length - num3).Trim();
              list.Add(ClickyHandLineBreaker.Truncated(text2, font, num));
              break;
            }
          }
          int num4 = Math.Min(canvasLineMetrics.CharacterCount, text.Length - num2);
          if (num4 <= 0)
          {
            break;
          }
          list.Add(text.Substring(num2, num4).Trim());
          num2 += num4;
        }
        IReadOnlyList<string> readOnlyList;
        if (list.Count != 0)
        {
          readOnlyList = list;
        }
        else
        {
          int i = 1;
          List<string> list2 = new List<string>(i);
          CollectionsMarshal.SetCount<string>(list2, i);
          readOnlyList = list2;
          Span<string> span = CollectionsMarshal.AsSpan<string>(list2);
          int num3 = 0;
          *span[num3] = "";
        }
        readOnlyList2 = readOnlyList;
      }
      return readOnlyList2;
    }

    // Token: 0x06001716 RID: 5910 RVA: 0x005BF130 File Offset: 0x005BF130
    public static double Width(string line, PaperFont font)
    {
      if (string.IsNullOrEmpty(line))
      {
        return 0.0;
      }
      double width;
      using (CanvasTextLayout canvasTextLayout = ClickyHandLineBreaker.Layout(line, font, 1000000.0, false))
      {
        width = canvasTextLayout.LayoutBoundsIncludingTrailingWhitespace.Width;
      }
      return width;
    }

    // Token: 0x06001717 RID: 5911 RVA: 0x005BF190 File Offset: 0x005BF190
    private static string Truncated(string line, PaperFont font, double width)
    {
      if (ClickyHandLineBreaker.Width(line, font) <= width)
      {
        return line;
      }
      string text = line;
      while (text.Length > 0)
      {
        TextElementEnumerator textElementEnumerator = StringInfo.GetTextElementEnumerator(text);
        int num = 0;
        while (textElementEnumerator.MoveNext())
        {
          num = textElementEnumerator.ElementIndex;
        }
        text = text.Substring(0, num);
        string text2 = text.TrimEnd(new char[] { ' ', '\t' }).TrimStart(new char[] { ' ', '\t' }) + "…";
        if (ClickyHandLineBreaker.Width(text2, font) <= width)
        {
          return text2;
        }
      }
      return "…";
    }

    // Token: 0x04000B47 RID: 2887
    [TupleElementNames(new string[] { "Natural", "Ascent" })]
    [Nullable(new byte[] { 1, 1, 0 })]
    private static readonly Dictionary<PaperFont, ValueTuple<double, double>> metricsCache = new Dictionary<PaperFont, ValueTuple<double, double>>();
  }
}
