using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.Core.Brain
{
  // Token: 0x02000201 RID: 513
  public static class BrainTurnTiming
  {
    // Token: 0x06001DEB RID: 7659 RVA: 0x003EA4D8 File Offset: 0x003EA4D8
    public static void NoteReleased()
    {
      object obj = BrainTurnTiming.gate;
      lock (obj)
      {
        BrainTurnTiming.current = new BrainTurnTiming.Marks
        {
          ReleasedAt = new DateTimeOffset?(BrainTurnTiming.Now())
        };
      }
    }

    // Token: 0x06001DEC RID: 7660 RVA: 0x003EA530 File Offset: 0x003EA530
    public static void NoteTranscriptReady()
    {
      object obj = BrainTurnTiming.gate;
      lock (obj)
      {
        if (BrainTurnTiming.current != null)
        {
          DateTimeOffset? transcriptAt = BrainTurnTiming.current.TranscriptAt;
          if (transcriptAt == null)
          {
            BrainTurnTiming.current.TranscriptAt = new DateTimeOffset?(BrainTurnTiming.Now());
          }
        }
      }
    }

    // Token: 0x06001DED RID: 7661 RVA: 0x003EA5A0 File Offset: 0x003EA5A0
    public static void NoteRequestStarted(bool isTyped)
    {
      object obj = BrainTurnTiming.gate;
      lock (obj)
      {
        if (BrainTurnTiming.current == null || BrainTurnTiming.IsStaleLocked())
        {
          BrainTurnTiming.current = new BrainTurnTiming.Marks
          {
            IsTyped = isTyped
          };
        }
        DateTimeOffset? requestStartedAt = BrainTurnTiming.current.RequestStartedAt;
        if (requestStartedAt == null)
        {
          BrainTurnTiming.current.RequestStartedAt = new DateTimeOffset?(BrainTurnTiming.Now());
          BrainTurnTiming.current.IsTyped = isTyped;
        }
      }
    }

    // Token: 0x06001DEE RID: 7662 RVA: 0x003EA634 File Offset: 0x003EA634
    public static void NoteRequestBody(int bytes, bool gzip)
    {
      object obj = BrainTurnTiming.gate;
      lock (obj)
      {
        if (BrainTurnTiming.current != null && BrainTurnTiming.current.RequestBodyBytes == 0)
        {
          BrainTurnTiming.current.RequestBodyBytes = bytes;
          BrainTurnTiming.current.RequestBodyWasGzip = gzip;
        }
      }
    }

    // Token: 0x06001DEF RID: 7663 RVA: 0x003EA698 File Offset: 0x003EA698
    public static void NoteResponseHeaders()
    {
      object obj = BrainTurnTiming.gate;
      lock (obj)
      {
        if (BrainTurnTiming.current != null)
        {
          DateTimeOffset? dateTimeOffset = BrainTurnTiming.current.HeadersAt;
          if (dateTimeOffset == null)
          {
            dateTimeOffset = BrainTurnTiming.current.RequestStartedAt;
            if (dateTimeOffset != null)
            {
              BrainTurnTiming.current.HeadersAt = new DateTimeOffset?(BrainTurnTiming.Now());
            }
          }
        }
      }
    }

    // Token: 0x06001DF0 RID: 7664 RVA: 0x003EA71C File Offset: 0x003EA71C
    public static void NoteFirstToken()
    {
      object obj = BrainTurnTiming.gate;
      lock (obj)
      {
        if (BrainTurnTiming.current != null)
        {
          DateTimeOffset? dateTimeOffset = BrainTurnTiming.current.FirstTokenAt;
          if (dateTimeOffset == null)
          {
            dateTimeOffset = BrainTurnTiming.current.RequestStartedAt;
            if (dateTimeOffset != null)
            {
              BrainTurnTiming.current.FirstTokenAt = new DateTimeOffset?(BrainTurnTiming.Now());
            }
          }
        }
      }
    }

    // Token: 0x06001DF1 RID: 7665 RVA: 0x003EA7A0 File Offset: 0x003EA7A0
    public static void DiscardCurrent()
    {
      object obj = BrainTurnTiming.gate;
      lock (obj)
      {
        BrainTurnTiming.current = null;
      }
    }

    // Token: 0x06001DF2 RID: 7666 RVA: 0x003EA7E0 File Offset: 0x003EA7E0
    [NullableContext(2)]
    public static void NoteFirstLineHeard(string model)
    {
      object obj = BrainTurnTiming.gate;
      BrainTurnTiming.Marks marks2;
      lock (obj)
      {
        BrainTurnTiming.Marks marks = BrainTurnTiming.current;
        if (marks != null)
        {
          DateTimeOffset? requestStartedAt = marks.RequestStartedAt;
          if (requestStartedAt != null && !BrainTurnTiming.IsStaleLocked())
          {
            marks2 = BrainTurnTiming.current;
            BrainTurnTiming.current = null;
            goto IL_005C;
          }
        }
        BrainTurnTiming.current = null;
        return;
      }
      IL_005C:
      DateTimeOffset dateTimeOffset = BrainTurnTiming.Now();
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["typed"] = marks2.IsTyped;
      jsonObject["body_kb"] = marks2.RequestBodyBytes / 1024;
      jsonObject["body_gzip"] = marks2.RequestBodyWasGzip;
      JsonObject jsonObject2 = jsonObject;
      if (model != null)
      {
        jsonObject2["model"] = model;
      }
      ValueTuple<string, int?>[] array = new ValueTuple<string, int?>[]
      {
        new ValueTuple<string, int?>("release_to_transcript_ms", BrainTurnTiming.<NoteFirstLineHeard>g__Milliseconds|13_0(marks2.ReleasedAt, marks2.TranscriptAt)),
        new ValueTuple<string, int?>("transcript_to_request_ms", BrainTurnTiming.<NoteFirstLineHeard>g__Milliseconds|13_0(marks2.TranscriptAt, marks2.RequestStartedAt)),
        new ValueTuple<string, int?>("request_to_headers_ms", BrainTurnTiming.<NoteFirstLineHeard>g__Milliseconds|13_0(marks2.RequestStartedAt, marks2.HeadersAt)),
        new ValueTuple<string, int?>("headers_to_first_token_ms", BrainTurnTiming.<NoteFirstLineHeard>g__Milliseconds|13_0(marks2.HeadersAt, marks2.FirstTokenAt)),
        new ValueTuple<string, int?>("first_token_to_first_line_ms", BrainTurnTiming.<NoteFirstLineHeard>g__Milliseconds|13_0(marks2.FirstTokenAt, new DateTimeOffset?(dateTimeOffset))),
        new ValueTuple<string, int?>("request_to_first_line_ms", BrainTurnTiming.<NoteFirstLineHeard>g__Milliseconds|13_0(marks2.RequestStartedAt, new DateTimeOffset?(dateTimeOffset))),
        new ValueTuple<string, int?>("release_to_first_line_ms", BrainTurnTiming.<NoteFirstLineHeard>g__Milliseconds|13_0(marks2.ReleasedAt, new DateTimeOffset?(dateTimeOffset)))
      };
      foreach (ValueTuple<string, int?> valueTuple in array)
      {
        string item = valueTuple.Item1;
        int? item2 = valueTuple.Item2;
        if (item2 != null)
        {
          int valueOrDefault = item2.GetValueOrDefault();
          jsonObject2[item] = valueOrDefault;
        }
      }
      string text = "⏱ [one brain] ";
      string text2 = string.Join(" ", array.Where<ValueTuple<string, int?>>(delegate([TupleElementNames(new string[] { "Key", "Value" })] ValueTuple<string, int?> l)
      {
        int? item3 = l.Item2;
        return item3 != null;
      }).Select<ValueTuple<string, int?>, string>(delegate([TupleElementNames(new string[] { "Key", "Value" })] ValueTuple<string, int?> l)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(1, 2);
        defaultInterpolatedStringHandler2.AppendFormatted(l.Item1.Replace("_ms", ""));
        defaultInterpolatedStringHandler2.AppendLiteral("=");
        defaultInterpolatedStringHandler2.AppendFormatted<int?>(l.Item2);
        return defaultInterpolatedStringHandler2.ToStringAndClear();
      }));
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 2);
      defaultInterpolatedStringHandler.AppendLiteral(" body=");
      defaultInterpolatedStringHandler.AppendFormatted<int>(marks2.RequestBodyBytes / 1024);
      defaultInterpolatedStringHandler.AppendLiteral("KB");
      defaultInterpolatedStringHandler.AppendFormatted(marks2.RequestBodyWasGzip ? " gzip" : "");
      string text3 = text + text2 + defaultInterpolatedStringHandler.ToStringAndClear();
      Log.Info("OneBrain", text3);
      Action<string, JsonObject> reportedForTest = BrainTurnTiming.ReportedForTest;
      if (reportedForTest != null)
      {
        reportedForTest(text3, jsonObject2);
        return;
      }
      if (Random.Shared.NextDouble() < 0.1)
      {
        jsonObject2["sample_rate"] = 0.1;
        ClickyAnalytics.Capture("one_brain_turn_timing", jsonObject2, null, null);
      }
    }

    // Token: 0x06001DF3 RID: 7667 RVA: 0x003EAB28 File Offset: 0x003EAB28
    private static bool IsStaleLocked()
    {
      BrainTurnTiming.Marks marks = BrainTurnTiming.current;
      DateTimeOffset? dateTimeOffset = ((marks != null) ? marks.ReleasedAt : null);
      DateTimeOffset? dateTimeOffset2;
      if (dateTimeOffset == null)
      {
        BrainTurnTiming.Marks marks2 = BrainTurnTiming.current;
        dateTimeOffset2 = ((marks2 != null) ? marks2.RequestStartedAt : null);
      }
      else
      {
        dateTimeOffset2 = dateTimeOffset;
      }
      DateTimeOffset? dateTimeOffset3 = dateTimeOffset2;
      if (dateTimeOffset3 != null)
      {
        DateTimeOffset valueOrDefault = dateTimeOffset3.GetValueOrDefault();
        return BrainTurnTiming.Now() - valueOrDefault > BrainTurnTiming.StaleAfter;
      }
      return false;
    }

    // Token: 0x06001DF4 RID: 7668 RVA: 0x003EABA4 File Offset: 0x003EABA4
    internal static void ResetForTest()
    {
      object obj = BrainTurnTiming.gate;
      lock (obj)
      {
        BrainTurnTiming.current = null;
      }
    }

    // Token: 0x06001DF6 RID: 7670 RVA: 0x003EAC14 File Offset: 0x003EAC14
    [CompilerGenerated]
    internal static int? <NoteFirstLineHeard>g__Milliseconds|13_0(DateTimeOffset? from, DateTimeOffset? to)
    {
      if (from != null)
      {
        DateTimeOffset valueOrDefault = from.GetValueOrDefault();
        if (to != null)
        {
          DateTimeOffset valueOrDefault2 = to.GetValueOrDefault();
          return new int?(Math.Max(0, (int)(valueOrDefault2 - valueOrDefault).TotalMilliseconds));
        }
      }
      return null;
    }

    // Token: 0x04000CDA RID: 3290
    [Nullable(1)]
    private static readonly object gate = new object();

    // Token: 0x04000CDB RID: 3291
    [Nullable(2)]
    private static BrainTurnTiming.Marks current;

    // Token: 0x04000CDC RID: 3292
    private static readonly TimeSpan StaleAfter = TimeSpan.FromSeconds(120L);

    // Token: 0x04000CDD RID: 3293
    [Nullable(1)]
    internal static Func<DateTimeOffset> Now = () => DateTimeOffset.UtcNow;

    // Token: 0x04000CDE RID: 3294
    [Nullable(new byte[] { 2, 1, 1 })]
    internal static Action<string, JsonObject> ReportedForTest;

    // Token: 0x04000CDF RID: 3295
    private const double PostHogSampleRate = 0.1;

    // Token: 0x02000659 RID: 1625
    private sealed class Marks
    {
      // Token: 0x040024FA RID: 9466
      public DateTimeOffset? ReleasedAt;

      // Token: 0x040024FB RID: 9467
      public DateTimeOffset? TranscriptAt;

      // Token: 0x040024FC RID: 9468
      public DateTimeOffset? RequestStartedAt;

      // Token: 0x040024FD RID: 9469
      public DateTimeOffset? HeadersAt;

      // Token: 0x040024FE RID: 9470
      public DateTimeOffset? FirstTokenAt;

      // Token: 0x040024FF RID: 9471
      public int RequestBodyBytes;

      // Token: 0x04002500 RID: 9472
      public bool RequestBodyWasGzip;

      // Token: 0x04002501 RID: 9473
      public bool IsTyped;
    }
  }
}
