using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000139 RID: 313
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkDates
  {
    // Token: 0x170004AA RID: 1194
    // (get) Token: 0x060013EA RID: 5098 RVA: 0x003AA07C File Offset: 0x003AA07C
    // (set) Token: 0x060013EB RID: 5099 RVA: 0x003AA084 File Offset: 0x003AA084
    public static Func<DateTimeOffset> ReferenceNow { get; set; } = () => DateTimeOffset.Now;

    // Token: 0x060013EC RID: 5100 RVA: 0x003AA08C File Offset: 0x003AA08C
    [GeneratedRegex("(\\d)\\s*([ap])\\.?m\\.?\\b", RegexOptions.IgnoreCase)]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex MeridiemRegex()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MeridiemRegex_37.Instance;
    }

    // Token: 0x060013ED RID: 5101 RVA: 0x003AA094 File Offset: 0x003AA094
    [NullableContext(2)]
    public static DateTimeOffset? Parse(string input, DateTimeOffset? now = null)
    {
      DateTimeOffset dateTimeOffset = now ?? ClickyInkDates.ReferenceNow();
      string text = ((input != null) ? input.TrimWs() : null) ?? "";
      if (text.Length == 0 || text.CharacterCount() > 40)
      {
        return null;
      }
      text = text.Replace(" at ", " ").Replace("·", " ").Replace("  ", " ");
      string text2 = text.SplitOmittingEmpty(' ').FirstOrDefault<string>();
      int i;
      if (text2 != null && ClickyInkDates.Weekdays.Contains(text2.Lowered().TrimSet(",.")))
      {
        string text3 = text;
        i = text.IndexOf(text2, StringComparison.Ordinal) + text2.Length;
        text = text3.Substring(i, text3.Length - i).TrimSet(" ,");
      }
      text = ClickyInkDates.MeridiemRegex().Replace(text, "$1 $2M").Replace(" aM", " AM")
        .Replace(" pM", " PM");
      string[] formats = ClickyInkDates.Formats;
      i = 0;
      while (i < formats.Length)
      {
        string text4 = formats[i];
        bool flag = text4.Contains("yyyy", StringComparison.Ordinal);
        string text5 = (flag ? text : (text + " 2000"));
        string text6 = (flag ? text4 : (text4 + " yyyy"));
        DateTime dateTime;
        if (DateTime.TryParseExact(text5, text6, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dateTime))
        {
          if (flag)
          {
            return new DateTimeOffset?(new DateTimeOffset(DateTime.SpecifyKind(dateTime, DateTimeKind.Local)));
          }
          int year = dateTimeOffset.ToLocalTime().Year;
          DateTimeOffset dateTimeOffset2 = ClickyInkDates.LocalDate(year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute);
          if (dateTimeOffset2 < dateTimeOffset.AddSeconds(-5184000.0))
          {
            dateTimeOffset2 = ClickyInkDates.LocalDate(year + 1, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute);
          }
          return new DateTimeOffset?(dateTimeOffset2);
        }
        else
        {
          i++;
        }
      }
      return null;
    }

    // Token: 0x060013EE RID: 5102 RVA: 0x003AA2C8 File Offset: 0x003AA2C8
    private static DateTimeOffset LocalDate(int year, int month, int day, int hour, int minute)
    {
      return new DateTimeOffset(new DateTime(year, month, 1, hour, minute, 0, DateTimeKind.Local).AddDays((double)(day - 1)));
    }

    // Token: 0x060013EF RID: 5103 RVA: 0x003AA2F4 File Offset: 0x003AA2F4
    public static ClickyInkDates.Remaining RemainingUntil(DateTimeOffset deadline, DateTimeOffset? now = null)
    {
      DateTimeOffset dateTimeOffset = now ?? ClickyInkDates.ReferenceNow();
      double totalSeconds = (deadline - dateTimeOffset).TotalSeconds;
      if (totalSeconds < 0.0)
      {
        double num = -totalSeconds;
        if (num < 3600.0)
        {
          return new ClickyInkDates.Remaining(Math.Max(1L, (long)(num / 60.0)).ToString(CultureInfo.InvariantCulture), "min ago", true, false);
        }
        if (num < 86400.0)
        {
          return new ClickyInkDates.Remaining(ClickyInkDates.<RemainingUntil>g__Whole|10_0(num / 3600.0), ((long)(num / 3600.0) == 1L) ? "hour ago" : "hours ago", true, false);
        }
        long num2 = (long)(num / 86400.0);
        return new ClickyInkDates.Remaining(num2.ToString(CultureInfo.InvariantCulture), (num2 == 1L) ? "day ago" : "days ago", true, false);
      }
      else
      {
        if (totalSeconds < 3600.0)
        {
          return new ClickyInkDates.Remaining(Math.Max(1L, (long)(totalSeconds / 60.0)).ToString(CultureInfo.InvariantCulture), "min", false, true);
        }
        if (totalSeconds < 86400.0)
        {
          long num3 = (long)(totalSeconds / 3600.0);
          return new ClickyInkDates.Remaining(num3.ToString(CultureInfo.InvariantCulture), (num3 == 1L) ? "hour" : "hours", false, true);
        }
        int num4 = Math.Max(1, (int)Math.Round((deadline.ToLocalTime().Date - dateTimeOffset.ToLocalTime().Date).TotalDays));
        return new ClickyInkDates.Remaining(num4.ToString(CultureInfo.InvariantCulture), (num4 == 1) ? "day" : "days", false, false);
      }
    }

    // Token: 0x060013F0 RID: 5104 RVA: 0x003AA4D0 File Offset: 0x003AA4D0
    public static string ShortLine(DateTimeOffset date)
    {
      DateTimeOffset dateTimeOffset = date.ToLocalTime();
      return dateTimeOffset.ToString((dateTimeOffset.Hour != 0 || dateTimeOffset.Minute != 0) ? "ddd MMM d · h:mm tt" : "ddd MMM d", CultureInfo.InvariantCulture);
    }

    // Token: 0x060013F2 RID: 5106 RVA: 0x003AA6FC File Offset: 0x003AA6FC
    [CompilerGenerated]
    internal static string <RemainingUntil>g__Whole|10_0(double v)
    {
      return ((long)v).ToString(CultureInfo.InvariantCulture);
    }

    // Token: 0x040008DE RID: 2270
    private static readonly string[] Formats = new string[]
    {
      "yyyy-MM-dd HH:mm", "yyyy-MM-dd'T'HH:mm", "yyyy-MM-dd'T'HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd h:mm tt", "yyyy-MM-dd", "MMM d, yyyy h:mm tt", "MMM d, yyyy HH:mm", "MMM d yyyy h:mm tt", "MMM d, yyyy",
      "MMM d yyyy", "MMMM d, yyyy h:mm tt", "MMMM d, yyyy", "d MMM yyyy", "d MMMM yyyy", "yyyy-MM-dd h tt", "MMM d, yyyy h tt", "MMM d, h:mm tt", "MMM d h:mm tt", "MMM d, HH:mm",
      "MMM d HH:mm", "MMMM d, h:mm tt", "MMMM d h:mm tt", "MMMM d HH:mm", "MMM d, h tt", "MMM d h tt", "MMMM d, h tt", "MMMM d h tt", "MMM d", "MMMM d",
      "d MMM", "d MMMM"
    };

    // Token: 0x040008DF RID: 2271
    private static readonly string[] Weekdays = new string[]
    {
      "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday", "mon", "tue", "tues",
      "wed", "thu", "thur", "thurs", "fri", "sat", "sun"
    };

    // Token: 0x020004BA RID: 1210
    [Nullable(0)]
    public readonly struct Remaining : IEquatable<ClickyInkDates.Remaining>
    {
      // Token: 0x06002FE4 RID: 12260 RVA: 0x00433574 File Offset: 0x00433574
      public Remaining(string Big, string Unit, bool IsPast, bool IsClose)
      {
        this.Big = Big;
        this.Unit = Unit;
        this.IsPast = IsPast;
        this.IsClose = IsClose;
      }

      // Token: 0x17000892 RID: 2194
      // (get) Token: 0x06002FE5 RID: 12261 RVA: 0x00433594 File Offset: 0x00433594
      // (set) Token: 0x06002FE6 RID: 12262 RVA: 0x0043359C File Offset: 0x0043359C
      public string Big { get; set; }

      // Token: 0x17000893 RID: 2195
      // (get) Token: 0x06002FE7 RID: 12263 RVA: 0x004335A8 File Offset: 0x004335A8
      // (set) Token: 0x06002FE8 RID: 12264 RVA: 0x004335B0 File Offset: 0x004335B0
      public string Unit { get; set; }

      // Token: 0x17000894 RID: 2196
      // (get) Token: 0x06002FE9 RID: 12265 RVA: 0x004335BC File Offset: 0x004335BC
      // (set) Token: 0x06002FEA RID: 12266 RVA: 0x004335C4 File Offset: 0x004335C4
      public bool IsPast { get; set; }

      // Token: 0x17000895 RID: 2197
      // (get) Token: 0x06002FEB RID: 12267 RVA: 0x004335D0 File Offset: 0x004335D0
      // (set) Token: 0x06002FEC RID: 12268 RVA: 0x004335D8 File Offset: 0x004335D8
      public bool IsClose { get; set; }

      // Token: 0x06002FED RID: 12269 RVA: 0x004335E4 File Offset: 0x004335E4
      [NullableContext(0)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Remaining");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002FEE RID: 12270 RVA: 0x00433630 File Offset: 0x00433630
      [NullableContext(0)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("Big = ");
        builder.Append(this.Big);
        builder.Append(", Unit = ");
        builder.Append(this.Unit);
        builder.Append(", IsPast = ");
        builder.Append(this.IsPast.ToString());
        builder.Append(", IsClose = ");
        builder.Append(this.IsClose.ToString());
        return true;
      }

      // Token: 0x06002FEF RID: 12271 RVA: 0x004336C0 File Offset: 0x004336C0
      [CompilerGenerated]
      public static bool operator !=(ClickyInkDates.Remaining left, ClickyInkDates.Remaining right)
      {
        return !(left == right);
      }

      // Token: 0x06002FF0 RID: 12272 RVA: 0x004336CC File Offset: 0x004336CC
      [CompilerGenerated]
      public static bool operator ==(ClickyInkDates.Remaining left, ClickyInkDates.Remaining right)
      {
        return left.Equals(right);
      }

      // Token: 0x06002FF1 RID: 12273 RVA: 0x004336D8 File Offset: 0x004336D8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<string>.Default.GetHashCode(this.<Big>k__BackingField) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Unit>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsPast>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsClose>k__BackingField);
      }

      // Token: 0x06002FF2 RID: 12274 RVA: 0x0043373C File Offset: 0x0043373C
      [NullableContext(0)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is ClickyInkDates.Remaining && this.Equals((ClickyInkDates.Remaining)obj);
      }

      // Token: 0x06002FF3 RID: 12275 RVA: 0x00433754 File Offset: 0x00433754
      [CompilerGenerated]
      public bool Equals(ClickyInkDates.Remaining other)
      {
        return EqualityComparer<string>.Default.Equals(this.<Big>k__BackingField, other.<Big>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Unit>k__BackingField, other.<Unit>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsPast>k__BackingField, other.<IsPast>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsClose>k__BackingField, other.<IsClose>k__BackingField);
      }

      // Token: 0x06002FF4 RID: 12276 RVA: 0x004337C4 File Offset: 0x004337C4
      [CompilerGenerated]
      public void Deconstruct(out string Big, out string Unit, out bool IsPast, out bool IsClose)
      {
        Big = this.Big;
        Unit = this.Unit;
        IsPast = this.IsPast;
        IsClose = this.IsClose;
      }
    }
  }
}
