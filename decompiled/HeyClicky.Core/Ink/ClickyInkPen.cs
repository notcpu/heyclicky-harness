using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200015E RID: 350
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkPen
  {
    // Token: 0x06001558 RID: 5464 RVA: 0x003B5CF0 File Offset: 0x003B5CF0
    public static double AfterDoneHaste(double left)
    {
      if (left <= 1.5)
      {
        return 1.0;
      }
      double num = 1.5 + 1.0 * Math.Log(1.0 + (left - 1.5) / 1.0);
      return Math.Min(2.5, Math.Max(1.0, left / num));
    }

    // Token: 0x06001559 RID: 5465 RVA: 0x003B5D6C File Offset: 0x003B5D6C
    public static double Haste(double backlog, double work)
    {
      double num = 1.0 + Math.Max(0.0, backlog - 4.0) / 6.0;
      double num2 = ((work <= 12.0) ? work : (12.0 + 8.0 * Math.Log(1.0 + (work - 12.0) / 8.0)));
      double num3 = ((num2 > 0.0) ? (work / num2) : 1.0);
      return Math.Min(2.5, Math.Max(Math.Max(1.0, num), num3));
    }

    // Token: 0x0600155A RID: 5466 RVA: 0x003B5E30 File Offset: 0x003B5E30
    public static double Beats(string text)
    {
      string[] array = text.Characters();
      double num = 0.0;
      for (int i = 0; i < array.Length; i++)
      {
        num += ClickyInkPen.Beat(array[i]) + ClickyInkPen.Rest(array[i], (i + 1 < array.Length) ? array[i + 1] : null, (i > 0) ? array[i - 1] : null);
      }
      return num;
    }

    // Token: 0x0600155B RID: 5467 RVA: 0x003B5E90 File Offset: 0x003B5E90
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "Characters", "Wet" })]
    public static ValueTuple<int, double> Revealed([Nullable(1)] string text, double progress)
    {
      if (progress >= 1.0)
      {
        return new ValueTuple<int, double>(text.CharacterCount(), 0.0);
      }
      if (progress <= 0.0)
      {
        return new ValueTuple<int, double>(0, 0.0);
      }
      string[] array = text.Characters();
      double num = progress * ClickyInkPen.Beats(text);
      double num2 = 0.0;
      for (int i = 0; i < array.Length; i++)
      {
        double num3 = ClickyInkPen.Beat(array[i]);
        if (num < num2 + num3)
        {
          return new ValueTuple<int, double>(i, Math.Max(0.0, Math.Min(1.0, (num - num2) / num3)));
        }
        num2 += num3 + ClickyInkPen.Rest(array[i], (i + 1 < array.Length) ? array[i + 1] : null, (i > 0) ? array[i - 1] : null);
        if (num < num2)
        {
          return new ValueTuple<int, double>(i + 1, 0.0);
        }
      }
      return new ValueTuple<int, double>(array.Length, 0.0);
    }

    // Token: 0x0600155C RID: 5468 RVA: 0x003B5F90 File Offset: 0x003B5F90
    public static List<double> CumulativeBeats(string text)
    {
      string[] array = text.Characters();
      List<double> list = new List<double>(array.Length + 1) { 0.0 };
      for (int i = 0; i < array.Length; i++)
      {
        list.Add(list[i] + ClickyInkPen.Beat(array[i]) + ClickyInkPen.Rest(array[i], (i + 1 < array.Length) ? array[i + 1] : null, (i > 0) ? array[i - 1] : null));
      }
      return list;
    }

    // Token: 0x0600155D RID: 5469 RVA: 0x003B6008 File Offset: 0x003B6008
    private static double Beat(string character)
    {
      if (ClickyInkParser.Emphasis.IsMarker(character))
      {
        return 0.0;
      }
      if (!(character == " "))
      {
        return 1.0;
      }
      return 0.8;
    }

    // Token: 0x0600155E RID: 5470 RVA: 0x003B603C File Offset: 0x003B603C
    [NullableContext(2)]
    private static double Rest([Nullable(1)] string character, string next, string previous)
    {
      bool flag = next == null || next == " " || next == "\n";
      if (next == null)
      {
        return 0.0;
      }
      if (character != null)
      {
        int length = character.Length;
        if (length == 1)
        {
          char c = character[0];
          if (c <= ':')
          {
            if (c <= '!')
            {
              if (c == '\n')
              {
                return 4.0;
              }
              if (c != '!')
              {
                goto IL_00EA;
              }
              goto IL_00BA;
            }
            else if (c != ',')
            {
              if (c == '.')
              {
                goto IL_00BA;
              }
              if (c != ':')
              {
                goto IL_00EA;
              }
            }
          }
          else if (c <= '?')
          {
            if (c != ';')
            {
              if (c != '?')
              {
                goto IL_00EA;
              }
              goto IL_00BA;
            }
          }
          else
          {
            if (c == '–' || c == '—')
            {
              return 2.5;
            }
            if (c != '…')
            {
              goto IL_00EA;
            }
            goto IL_00BA;
          }
          return flag ? 3.5 : 0.0;
          IL_00BA:
          return (double)((flag && previous != ".") ? 6 : 0);
        }
      }
      IL_00EA:
      return 0.0;
    }

    // Token: 0x04000939 RID: 2361
    public const double TitleCharactersPerSecond = 24.0;

    // Token: 0x0400093A RID: 2362
    public const double BodyCharactersPerSecond = 48.0;

    // Token: 0x0400093B RID: 2363
    public const double CellCharactersPerSecond = 80.0;

    // Token: 0x0400093C RID: 2364
    public const double ObjectLandSeconds = 0.55;

    // Token: 0x0400093D RID: 2365
    public const double RowSeconds = 0.1;

    // Token: 0x0400093E RID: 2366
    public const double StrikeSeconds = 0.3;

    // Token: 0x0400093F RID: 2367
    public const double FoldSeconds = 0.45;

    // Token: 0x04000940 RID: 2368
    public const double GrowSeconds = 0.4;

    // Token: 0x04000941 RID: 2369
    public const double PauseBetweenBlocks = 0.1;

    // Token: 0x04000942 RID: 2370
    public const double TravelSeconds = 0.35;

    // Token: 0x04000943 RID: 2371
    public const double TitleLeadSeconds = 0.3;

    // Token: 0x04000944 RID: 2372
    public const double HeavyRewriteSimilarity = 0.3;

    // Token: 0x04000945 RID: 2373
    public const int HeavyRewriteStrikeRuns = 3;

    // Token: 0x04000946 RID: 2374
    public const int HeavyRewriteMinimumWords = 5;

    // Token: 0x04000947 RID: 2375
    public const double HeavyRewriteStrikeCharactersPerSecond = 90.0;

    // Token: 0x04000948 RID: 2376
    public const double HeavyRewriteStrikeMaximumSeconds = 0.8;

    // Token: 0x04000949 RID: 2377
    public const double JumpTravelSeconds = 0.8;

    // Token: 0x0400094A RID: 2378
    public const int JumpPageDistance = 2;

    // Token: 0x0400094B RID: 2379
    public const int JumpBlockDistance = 4;

    // Token: 0x0400094C RID: 2380
    public const double AfterDoneHasteMaximum = 2.5;

    // Token: 0x0400094D RID: 2381
    public const double AfterDoneUnhurriedSeconds = 1.5;

    // Token: 0x0400094E RID: 2382
    public const double AfterDoneSoftness = 1.0;

    // Token: 0x0400094F RID: 2383
    public const double TickSeconds = 0.3;

    // Token: 0x04000950 RID: 2384
    public const double NumberChangeSeconds = 0.45;

    // Token: 0x04000951 RID: 2385
    public const double MaximumBacklogSeconds = 6.0;

    // Token: 0x04000952 RID: 2386
    public const double HurryAfterBacklogSeconds = 4.0;

    // Token: 0x04000953 RID: 2387
    public const double BacklogSecondsPerHaste = 6.0;

    // Token: 0x04000954 RID: 2388
    public const double MaximumHaste = 2.5;

    // Token: 0x04000955 RID: 2389
    public const double MaximumMotionHaste = 1.6;

    // Token: 0x04000956 RID: 2390
    public const double UnhurriedVersionSeconds = 12.0;

    // Token: 0x04000957 RID: 2391
    public const double LongVersionSoftness = 8.0;

    // Token: 0x04000958 RID: 2392
    public const double SlowestStreamHaste = 0.6;
  }
}
