using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000159 RID: 345
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkEventText
  {
    // Token: 0x06001510 RID: 5392 RVA: 0x003B3138 File Offset: 0x003B3138
    public static string When(DateTimeOffset start, DateTimeOffset? end, bool isAllDay, [Nullable(2)] CultureInfo culture = null)
    {
      CultureInfo cultureInfo = culture ?? CultureInfo.CurrentCulture;
      DateTimeOffset dateTimeOffset = start.ToLocalTime();
      if (isAllDay)
      {
        if (end != null)
        {
          DateTimeOffset valueOrDefault = end.GetValueOrDefault();
          if (valueOrDefault > start)
          {
            return ClickyInkEventText.MonthDay(dateTimeOffset, cultureInfo) + " – " + ClickyInkEventText.MonthDay(valueOrDefault.ToLocalTime(), cultureInfo);
          }
        }
        return dateTimeOffset.ToString("dddd", cultureInfo) + ", " + ClickyInkEventText.MonthDay(dateTimeOffset, cultureInfo);
      }
      string text = string.Concat(new string[]
      {
        dateTimeOffset.ToString("ddd", cultureInfo),
        ", ",
        ClickyInkEventText.MonthDay(dateTimeOffset, cultureInfo),
        ", ",
        ClickyInkEventText.Time(dateTimeOffset, cultureInfo)
      });
      if (end != null)
      {
        text = text + " – " + ClickyInkEventText.Time(end.GetValueOrDefault().ToLocalTime(), cultureInfo);
      }
      return text;
    }

    // Token: 0x06001511 RID: 5393 RVA: 0x003B3220 File Offset: 0x003B3220
    public static string MonthDay(DateTimeOffset date, CultureInfo c)
    {
      return date.ToString("MMM d", c);
    }

    // Token: 0x06001512 RID: 5394 RVA: 0x003B3230 File Offset: 0x003B3230
    public static string Time(DateTimeOffset date, CultureInfo c)
    {
      return date.ToString(c.DateTimeFormat.ShortTimePattern.Contains('H') ? "HH:mm" : "h:mm tt", c);
    }

    // Token: 0x06001513 RID: 5395 RVA: 0x003B325C File Offset: 0x003B325C
    [NullableContext(2)]
    [return: TupleElementNames(new string[] { "Month", "Day", "Weekday" })]
    [return: Nullable(new byte[] { 0, 1, 1, 1 })]
    public static ValueTuple<string, string, string> Page(DateTimeOffset start, CultureInfo culture = null)
    {
      CultureInfo cultureInfo = culture ?? CultureInfo.CurrentCulture;
      DateTimeOffset dateTimeOffset = start.ToLocalTime();
      return new ValueTuple<string, string, string>(dateTimeOffset.ToString("MMM", cultureInfo).TrimEnd('.').ToUpper(cultureInfo), dateTimeOffset.Day.ToString(cultureInfo), dateTimeOffset.ToString("ddd", cultureInfo).TrimEnd('.').ToUpper(cultureInfo));
    }
  }
}
