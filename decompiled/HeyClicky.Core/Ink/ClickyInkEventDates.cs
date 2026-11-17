using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000149 RID: 329
  [NullableContext(2)]
  [Nullable(0)]
  public static class ClickyInkEventDates
  {
    // Token: 0x060014B7 RID: 5303 RVA: 0x003AFC38 File Offset: 0x003AFC38
    public static bool IsDateOnly(string text)
    {
      string text2 = ((text != null) ? text.TrimWs() : null);
      return text2 != null && text2.CharacterCount() == 10 && ClickyInkEventDates.Date(text2) != null;
    }

    // Token: 0x060014B8 RID: 5304 RVA: 0x003AFC78 File Offset: 0x003AFC78
    public static DateTimeOffset? Date(string text)
    {
      string text2 = ((text != null) ? text.TrimWs() : null) ?? "";
      if (text2.Length == 0)
      {
        return null;
      }
      DateTimeOffset dateTimeOffset;
      if (DateTimeOffset.TryParseExact(text2, new string[] { "yyyy-MM-dd'T'HH:mm:ssZ", "yyyy-MM-dd'T'HH:mm:sszzz" }, CultureInfo.InvariantCulture, text2.HasSuffix("Z") ? (DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal) : DateTimeStyles.None, out dateTimeOffset))
      {
        return new DateTimeOffset?(dateTimeOffset);
      }
      foreach (string text3 in ClickyInkEventDates.Formats)
      {
        DateTime dateTime;
        if (DateTime.TryParseExact(text2, text3, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dateTime))
        {
          return new DateTimeOffset?(new DateTimeOffset(DateTime.SpecifyKind(dateTime, DateTimeKind.Local)));
        }
      }
      return null;
    }

    // Token: 0x060014B9 RID: 5305 RVA: 0x003AFD38 File Offset: 0x003AFD38
    public static DateTimeOffset? End(string text, DateTimeOffset start)
    {
      string text2 = ((text != null) ? text.TrimWs() : null) ?? "";
      if (text2.Length == 0)
      {
        return null;
      }
      DateTimeOffset? dateTimeOffset = ClickyInkEventDates.Date(text2);
      if (dateTimeOffset != null)
      {
        DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
        return new DateTimeOffset?(valueOrDefault);
      }
      foreach (string text3 in new string[] { "HH:mm", "h:mm tt", "htt", "h tt" })
      {
        DateTime dateTime;
        if (DateTime.TryParseExact(text2, text3, CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out dateTime))
        {
          return new DateTimeOffset?(new DateTimeOffset(DateTime.SpecifyKind(start.ToLocalTime().Date.AddHours((double)dateTime.Hour).AddMinutes((double)dateTime.Minute), DateTimeKind.Local)));
        }
      }
      return null;
    }

    // Token: 0x060014BA RID: 5306 RVA: 0x003AFE2C File Offset: 0x003AFE2C
    [NullableContext(1)]
    public static string Ics(string title, DateTimeOffset start, DateTimeOffset? end, bool isAllDay, [Nullable(2)] string location, [Nullable(2)] string notes)
    {
      List<string> list = new List<string>
      {
        "BEGIN:VCALENDAR",
        "VERSION:2.0",
        "PRODID:-//HeyClicky//Note//EN",
        "BEGIN:VEVENT",
        "UID:" + Guid.NewGuid().ToString().ToUpperInvariant() + "@heyclicky",
        "DTSTAMP:" + ClickyInkEventDates.<Ics>g__Utc|4_1(DateTimeOffset.Now),
        "SUMMARY:" + ClickyInkEventDates.<Ics>g__Escaped|4_0(title)
      };
      if (isAllDay)
      {
        list.Add("DTSTART;VALUE=DATE:" + ClickyInkEventDates.<Ics>g__Day|4_2(start));
        DateTimeOffset dateTimeOffset;
        if (end != null)
        {
          DateTimeOffset valueOrDefault = end.GetValueOrDefault();
          if (valueOrDefault > start)
          {
            dateTimeOffset = valueOrDefault;
            goto IL_00C6;
          }
        }
        dateTimeOffset = start;
        IL_00C6:
        DateTimeOffset dateTimeOffset2 = dateTimeOffset;
        list.Add("DTEND;VALUE=DATE:" + ClickyInkEventDates.<Ics>g__Day|4_2(dateTimeOffset2.ToLocalTime().AddDays(1.0)));
      }
      else
      {
        list.Add("DTSTART:" + ClickyInkEventDates.<Ics>g__Utc|4_1(start));
        list.Add("DTEND:" + ClickyInkEventDates.<Ics>g__Utc|4_1(end ?? start.AddSeconds(3600.0)));
      }
      if (!string.IsNullOrEmpty(location))
      {
        list.Add("LOCATION:" + ClickyInkEventDates.<Ics>g__Escaped|4_0(ClickyInkNoteActions.Plain(location)));
      }
      if (!string.IsNullOrEmpty(notes))
      {
        list.Add("DESCRIPTION:" + ClickyInkEventDates.<Ics>g__Escaped|4_0(ClickyInkNoteActions.Plain(notes)));
      }
      list.AddRange(new <>z__ReadOnlyArray<string>(new string[] { "END:VEVENT", "END:VCALENDAR" }));
      return string.Join("\r\n", list);
    }

    // Token: 0x060014BC RID: 5308 RVA: 0x003B0048 File Offset: 0x003B0048
    [NullableContext(1)]
    [CompilerGenerated]
    internal static string <Ics>g__Escaped|4_0(string text)
    {
      return text.Replace("\\", "\\\\").Replace(";", "\\;").Replace(",", "\\,")
        .Replace("\n", "\\n");
    }

    // Token: 0x060014BD RID: 5309 RVA: 0x003B0088 File Offset: 0x003B0088
    [NullableContext(1)]
    [CompilerGenerated]
    internal static string <Ics>g__Utc|4_1(DateTimeOffset date)
    {
      return date.UtcDateTime.ToString("yyyyMMdd'T'HHmmss'Z'", CultureInfo.InvariantCulture);
    }

    // Token: 0x060014BE RID: 5310 RVA: 0x003B00B0 File Offset: 0x003B00B0
    [NullableContext(1)]
    [CompilerGenerated]
    internal static string <Ics>g__Day|4_2(DateTimeOffset date)
    {
      return date.ToLocalTime().ToString("yyyyMMdd", CultureInfo.InvariantCulture);
    }

    // Token: 0x0400090D RID: 2317
    [Nullable(1)]
    private static readonly string[] Formats = new string[] { "yyyy-MM-dd HH:mm", "yyyy-MM-dd'T'HH:mm", "yyyy-MM-dd'T'HH:mm:ss", "yyyy-MM-dd h:mm tt", "yyyy-MM-dd htt", "yyyy-MM-dd h tt", "yyyy-MM-dd" };
  }
}
