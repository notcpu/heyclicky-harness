using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace HeyClicky.Agents.Routines
{
  // Token: 0x02000029 RID: 41
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyRoutine
  {
    // Token: 0x1700010B RID: 267
    // (get) Token: 0x06000419 RID: 1049 RVA: 0x000EA784 File Offset: 0x000EA784
    // (set) Token: 0x0600041A RID: 1050 RVA: 0x000EA78C File Offset: 0x000EA78C
    public string Id { get; set; } = Guid.NewGuid().ToString("D").ToUpperInvariant();

    // Token: 0x1700010C RID: 268
    // (get) Token: 0x0600041B RID: 1051 RVA: 0x000EA798 File Offset: 0x000EA798
    // (set) Token: 0x0600041C RID: 1052 RVA: 0x000EA7A0 File Offset: 0x000EA7A0
    public string ClickySlug { get; set; } = "";

    // Token: 0x1700010D RID: 269
    // (get) Token: 0x0600041D RID: 1053 RVA: 0x000EA7AC File Offset: 0x000EA7AC
    // (set) Token: 0x0600041E RID: 1054 RVA: 0x000EA7B4 File Offset: 0x000EA7B4
    [Nullable(2)]
    public string JobId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700010E RID: 270
    // (get) Token: 0x0600041F RID: 1055 RVA: 0x000EA7C0 File Offset: 0x000EA7C0
    // (set) Token: 0x06000420 RID: 1056 RVA: 0x000EA7C8 File Offset: 0x000EA7C8
    public int? ScheduledMinuteOfDay { get; set; }

    // Token: 0x1700010F RID: 271
    // (get) Token: 0x06000421 RID: 1057 RVA: 0x000EA7D4 File Offset: 0x000EA7D4
    // (set) Token: 0x06000422 RID: 1058 RVA: 0x000EA7DC File Offset: 0x000EA7DC
    public int? ScheduledWeekday { get; set; }

    // Token: 0x17000110 RID: 272
    // (get) Token: 0x06000423 RID: 1059 RVA: 0x000EA7E8 File Offset: 0x000EA7E8
    // (set) Token: 0x06000424 RID: 1060 RVA: 0x000EA7F0 File Offset: 0x000EA7F0
    [Nullable(2)]
    public List<int> ScheduledWeekdays
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000111 RID: 273
    // (get) Token: 0x06000425 RID: 1061 RVA: 0x000EA7FC File Offset: 0x000EA7FC
    // (set) Token: 0x06000426 RID: 1062 RVA: 0x000EA804 File Offset: 0x000EA804
    public string Title { get; set; } = "";

    // Token: 0x17000112 RID: 274
    // (get) Token: 0x06000427 RID: 1063 RVA: 0x000EA810 File Offset: 0x000EA810
    // (set) Token: 0x06000428 RID: 1064 RVA: 0x000EA818 File Offset: 0x000EA818
    public string TaskPrompt { get; set; } = "";

    // Token: 0x17000113 RID: 275
    // (get) Token: 0x06000429 RID: 1065 RVA: 0x000EA824 File Offset: 0x000EA824
    // (set) Token: 0x0600042A RID: 1066 RVA: 0x000EA82C File Offset: 0x000EA82C
    public double IntervalSeconds { get; set; }

    // Token: 0x17000114 RID: 276
    // (get) Token: 0x0600042B RID: 1067 RVA: 0x000EA838 File Offset: 0x000EA838
    // (set) Token: 0x0600042C RID: 1068 RVA: 0x000EA840 File Offset: 0x000EA840
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    // Token: 0x17000115 RID: 277
    // (get) Token: 0x0600042D RID: 1069 RVA: 0x000EA84C File Offset: 0x000EA84C
    // (set) Token: 0x0600042E RID: 1070 RVA: 0x000EA854 File Offset: 0x000EA854
    public bool IsPaused { get; set; }

    // Token: 0x17000116 RID: 278
    // (get) Token: 0x0600042F RID: 1071 RVA: 0x000EA860 File Offset: 0x000EA860
    // (set) Token: 0x06000430 RID: 1072 RVA: 0x000EA868 File Offset: 0x000EA868
    public DateTimeOffset NextRunAt { get; set; } = DateTimeOffset.Now;

    // Token: 0x17000117 RID: 279
    // (get) Token: 0x06000431 RID: 1073 RVA: 0x000EA874 File Offset: 0x000EA874
    // (set) Token: 0x06000432 RID: 1074 RVA: 0x000EA87C File Offset: 0x000EA87C
    public int RunCount { get; set; }

    // Token: 0x17000118 RID: 280
    // (get) Token: 0x06000433 RID: 1075 RVA: 0x000EA888 File Offset: 0x000EA888
    // (set) Token: 0x06000434 RID: 1076 RVA: 0x000EA890 File Offset: 0x000EA890
    public DateTimeOffset? LastRunStartedAt { get; set; }

    // Token: 0x17000119 RID: 281
    // (get) Token: 0x06000435 RID: 1077 RVA: 0x000EA89C File Offset: 0x000EA89C
    // (set) Token: 0x06000436 RID: 1078 RVA: 0x000EA8A4 File Offset: 0x000EA8A4
    public DateTimeOffset? LastRunFinishedAt { get; set; }

    // Token: 0x1700011A RID: 282
    // (get) Token: 0x06000437 RID: 1079 RVA: 0x000EA8B0 File Offset: 0x000EA8B0
    // (set) Token: 0x06000438 RID: 1080 RVA: 0x000EA8B8 File Offset: 0x000EA8B8
    [Nullable(2)]
    public string LastRunSummary
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700011B RID: 283
    // (get) Token: 0x06000439 RID: 1081 RVA: 0x000EA8C4 File Offset: 0x000EA8C4
    // (set) Token: 0x0600043A RID: 1082 RVA: 0x000EA8CC File Offset: 0x000EA8CC
    public bool LastRunFailed { get; set; }

    // Token: 0x1700011C RID: 284
    // (get) Token: 0x0600043B RID: 1083 RVA: 0x000EA8D8 File Offset: 0x000EA8D8
    // (set) Token: 0x0600043C RID: 1084 RVA: 0x000EA8E0 File Offset: 0x000EA8E0
    public DateTimeOffset? PendingRetryAt { get; set; }

    // Token: 0x1700011D RID: 285
    // (get) Token: 0x0600043D RID: 1085 RVA: 0x000EA8EC File Offset: 0x000EA8EC
    // (set) Token: 0x0600043E RID: 1086 RVA: 0x000EA8F4 File Offset: 0x000EA8F4
    [Nullable(2)]
    public string LastRunTaskId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700011E RID: 286
    // (get) Token: 0x0600043F RID: 1087 RVA: 0x000EA900 File Offset: 0x000EA900
    // (set) Token: 0x06000440 RID: 1088 RVA: 0x000EA908 File Offset: 0x000EA908
    public int ConsecutiveFailedRunCount { get; set; }

    // Token: 0x1700011F RID: 287
    // (get) Token: 0x06000441 RID: 1089 RVA: 0x000EA914 File Offset: 0x000EA914
    // (set) Token: 0x06000442 RID: 1090 RVA: 0x000EA91C File Offset: 0x000EA91C
    public DateTimeOffset? WaitingForNetworkSince { get; set; }

    // Token: 0x06000443 RID: 1091 RVA: 0x000EA928 File Offset: 0x000EA928
    public bool IsRunInFlight(DateTimeOffset now, TimeSpan staleAfter)
    {
      DateTimeOffset? lastRunStartedAt = this.LastRunStartedAt;
      if (lastRunStartedAt != null)
      {
        DateTimeOffset valueOrDefault = lastRunStartedAt.GetValueOrDefault();
        if (this.LastRunFinishedAt == null)
        {
          return now - valueOrDefault < staleAfter;
        }
      }
      return false;
    }

    // Token: 0x17000120 RID: 288
    // (get) Token: 0x06000444 RID: 1092 RVA: 0x000EA96C File Offset: 0x000EA96C
    [JsonIgnore]
    public int IntervalMinutes
    {
      get
      {
        return (int)Math.Round(this.IntervalSeconds / 60.0);
      }
    }

    // Token: 0x17000121 RID: 289
    // (get) Token: 0x06000445 RID: 1093 RVA: 0x000EA984 File Offset: 0x000EA984
    [JsonIgnore]
    public bool IsPausedAfterRepeatedFailures
    {
      get
      {
        return this.IsPaused && this.ConsecutiveFailedRunCount >= 3;
      }
    }

    // Token: 0x17000122 RID: 290
    // (get) Token: 0x06000446 RID: 1094 RVA: 0x000EA99C File Offset: 0x000EA99C
    [JsonIgnore]
    public string CadenceLabel
    {
      get
      {
        return ClickyRoutine.CadenceLabelFor(this.IntervalMinutes);
      }
    }

    // Token: 0x17000123 RID: 291
    // (get) Token: 0x06000447 RID: 1095 RVA: 0x000EA9AC File Offset: 0x000EA9AC
    [Nullable(2)]
    [JsonIgnore]
    public string OwningClickySlug
    {
      [NullableContext(2)]
      get
      {
        if (this.JobId != null || this.ClickySlug.Length <= 0)
        {
          return null;
        }
        return this.ClickySlug;
      }
    }

    // Token: 0x17000124 RID: 292
    // (get) Token: 0x06000448 RID: 1096 RVA: 0x000EA9CC File Offset: 0x000EA9CC
    [JsonIgnore]
    public bool IsSavedOnAClicky
    {
      get
      {
        return this.OwningClickySlug != null;
      }
    }

    // Token: 0x17000125 RID: 293
    // (get) Token: 0x06000449 RID: 1097 RVA: 0x000EA9DC File Offset: 0x000EA9DC
    [JsonIgnore]
    public string OwnerLogName
    {
      get
      {
        string jobId = this.JobId;
        if (jobId == null)
        {
          return this.ClickySlug;
        }
        return "job " + jobId;
      }
    }

    // Token: 0x0600044A RID: 1098 RVA: 0x000EAA08 File Offset: 0x000EAA08
    public static string StartCursorLine(string jobTitle, string title)
    {
      if (!string.Equals(jobTitle, title, StringComparison.OrdinalIgnoreCase))
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 2);
        defaultInterpolatedStringHandler.AppendLiteral("Running your routine: ");
        defaultInterpolatedStringHandler.AppendFormatted(title);
        defaultInterpolatedStringHandler.AppendLiteral(" (");
        defaultInterpolatedStringHandler.AppendFormatted(jobTitle);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
      return "Running your routine: " + title;
    }

    // Token: 0x17000126 RID: 294
    // (get) Token: 0x0600044B RID: 1099 RVA: 0x000EAA70 File Offset: 0x000EAA70
    [JsonIgnore]
    public bool HasSetTime
    {
      get
      {
        bool flag = this.ScheduledMinuteOfDay != null;
        if (flag)
        {
          int intervalMinutes = this.IntervalMinutes;
          bool flag2 = intervalMinutes == 1440 || intervalMinutes == 10080;
          flag = flag2;
        }
        return flag;
      }
    }

    // Token: 0x17000127 RID: 295
    // (get) Token: 0x0600044C RID: 1100 RVA: 0x000EAAB8 File Offset: 0x000EAAB8
    [Nullable(2)]
    [JsonIgnore]
    public IReadOnlyList<int> SetDays
    {
      [NullableContext(2)]
      get
      {
        if (this.IntervalMinutes == 1440)
        {
          List<int> scheduledWeekdays = this.ScheduledWeekdays;
          if (scheduledWeekdays != null)
          {
            int count = scheduledWeekdays.Count;
            if (count > 0 && count < 7)
            {
              return scheduledWeekdays;
            }
          }
        }
        return null;
      }
    }

    // Token: 0x0600044D RID: 1101 RVA: 0x000EAAF0 File Offset: 0x000EAAF0
    public DateTimeOffset? NextSetTime(DateTimeOffset date)
    {
      if (this.HasSetTime)
      {
        int? scheduledMinuteOfDay = this.ScheduledMinuteOfDay;
        if (scheduledMinuteOfDay != null)
        {
          int valueOrDefault = scheduledMinuteOfDay.GetValueOrDefault();
          DateTimeOffset dateTimeOffset = date.ToLocalTime();
          DateTimeOffset candidate = new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, valueOrDefault / 60, valueOrDefault % 60, 0, dateTimeOffset.Offset);
          IReadOnlyList<int> setDays = this.SetDays;
          if (setDays != null)
          {
            candidate = setDays.Select<int, DateTimeOffset>((int day) => ClickyRoutine.OnWeekdayAfter(candidate, day, date)).Min<DateTimeOffset>();
          }
          else if (this.IntervalMinutes == 10080)
          {
            candidate = ClickyRoutine.OnWeekdayAfter(candidate, this.ScheduledWeekday ?? ((int)(dateTimeOffset.DayOfWeek + 1)), date);
          }
          else if (candidate <= date)
          {
            candidate = candidate.AddDays(1.0);
          }
          DateTime dateTime = candidate.DateTime;
          return new DateTimeOffset?(new DateTimeOffset(dateTime, TimeZoneInfo.Local.GetUtcOffset(dateTime)));
        }
      }
      return null;
    }

    // Token: 0x0600044E RID: 1102 RVA: 0x000EAC38 File Offset: 0x000EAC38
    private static DateTimeOffset OnWeekdayAfter(DateTimeOffset candidate, int weekday, DateTimeOffset date)
    {
      DateTimeOffset dateTimeOffset = candidate.AddDays((double)((weekday - 1 - (int)candidate.DayOfWeek + 7) % 7));
      if (!(dateTimeOffset <= date))
      {
        return dateTimeOffset;
      }
      return dateTimeOffset.AddDays(7.0);
    }

    // Token: 0x0600044F RID: 1103 RVA: 0x000EAC78 File Offset: 0x000EAC78
    public DateTimeOffset RegularNextRunAt(DateTimeOffset date)
    {
      DateTimeOffset? dateTimeOffset = this.NextSetTime(date);
      if (dateTimeOffset == null)
      {
        return date.AddSeconds(this.IntervalSeconds);
      }
      return dateTimeOffset.GetValueOrDefault();
    }

    // Token: 0x17000128 RID: 296
    // (get) Token: 0x06000450 RID: 1104 RVA: 0x000EACAC File Offset: 0x000EACAC
    [JsonIgnore]
    public string ScheduleLabel
    {
      get
      {
        if (this.HasSetTime)
        {
          int? scheduledMinuteOfDay = this.ScheduledMinuteOfDay;
          if (scheduledMinuteOfDay != null)
          {
            int valueOrDefault = scheduledMinuteOfDay.GetValueOrDefault();
            string text = ClickyRoutine.TimeWords(valueOrDefault);
            IReadOnlyList<int> setDays = this.SetDays;
            if (setDays != null)
            {
              return ClickyRoutine.DaysWords(setDays, null) + " at " + text;
            }
            if (this.IntervalMinutes == 10080)
            {
              int num = this.ScheduledWeekday ?? ((int)(this.NextRunAt.ToLocalTime().DayOfWeek + 1));
              string[] dayNames = CultureInfo.CurrentCulture.DateTimeFormat.DayNames;
              string text2 = ((num >= 1 && num <= 7) ? dayNames[num - 1] : "week");
              return "Every " + text2 + " at " + text;
            }
            return "Every day at " + text;
          }
        }
        return this.CadenceLabel;
      }
    }

    // Token: 0x06000451 RID: 1105 RVA: 0x000EAD94 File Offset: 0x000EAD94
    public static string DaysWords(IEnumerable<int> days, [Nullable(2)] CultureInfo culture = null)
    {
      DateTimeFormatInfo format = (culture ?? CultureInfo.CurrentCulture).DateTimeFormat;
      HashSet<int> hashSet = days.ToHashSet<int>();
      if (hashSet.SetEquals(new <>z__ReadOnlyArray<int>(new int[] { 2, 3, 4, 5, 6 })))
      {
        return "Every weekday";
      }
      if (hashSet.SetEquals(new <>z__ReadOnlyArray<int>(new int[] { 1, 7 })))
      {
        return "Every weekend";
      }
      int firstWeekday = (int)(format.FirstDayOfWeek + 1);
      List<string> list = (from day in (from i in Enumerable.Range(0, 7)
          select (firstWeekday - 1 + i) % 7 + 1).Where<int>(new Func<int, bool>(hashSet.Contains))
        select format.DayNames[day - 1]).ToList<string>();
      string text;
      if (list.Count <= 1)
      {
        text = list.FirstOrDefault<string>() ?? "";
      }
      else
      {
        string text2 = ", ";
        List<string> list2 = list;
        string text3 = string.Join(text2, list2.Slice(0, list2.Count - 1));
        string text4 = " and ";
        List<string> list3 = list;
        text = text3 + text4 + list3[list3.Count - 1];
      }
      string text5 = text;
      return "Every " + text5;
    }

    // Token: 0x06000452 RID: 1106 RVA: 0x000EAEB8 File Offset: 0x000EAEB8
    [NullableContext(2)]
    public static ClickyRoutine.Schedule NormalizedSchedule(int minutes, int? minuteOfDay, IEnumerable<int> weekdays)
    {
      List<int> list = (weekdays ?? Array.Empty<int>()).Where<int>((int day) => day >= 1 && day <= 7).Distinct<int>().Order<int>()
        .ToList<int>();
      if (list.Count == 0 || list.Count >= 7)
      {
        int num = Math.Clamp(minutes, 2, 10080);
        bool flag = num == 1440 || num == 10080;
        bool flag2 = flag;
        int num2 = num;
        int? num3;
        if (flag2 && minuteOfDay != null)
        {
          int valueOrDefault = minuteOfDay.GetValueOrDefault();
          num3 = new int?(Math.Clamp(valueOrDefault, 0, 1439));
        }
        else
        {
          num3 = null;
        }
        return new ClickyRoutine.Schedule(num2, num3, null, null);
      }
      int num4 = Math.Clamp(minuteOfDay.GetValueOrDefault(540), 0, 1439);
      if (list.Count != 1)
      {
        return new ClickyRoutine.Schedule(1440, new int?(num4), null, list);
      }
      return new ClickyRoutine.Schedule(10080, new int?(num4), new int?(list[0]), null);
    }

    // Token: 0x06000453 RID: 1107 RVA: 0x000EAFE0 File Offset: 0x000EAFE0
    public static string TimeWords(int minuteOfDay)
    {
      return DateTime.Today.AddMinutes((double)minuteOfDay).ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern, CultureInfo.CurrentCulture);
    }

    // Token: 0x06000454 RID: 1108 RVA: 0x000EB018 File Offset: 0x000EB018
    public static ClickyRoutine Create(string clickySlug, string title, string taskPrompt, int intervalMinutes, [Nullable(2)] string jobId = null)
    {
      return new ClickyRoutine
      {
        ClickySlug = clickySlug,
        JobId = jobId,
        Title = title,
        TaskPrompt = taskPrompt,
        IntervalSeconds = (double)(Math.Clamp(intervalMinutes, 2, 10080) * 60)
      };
    }

    // Token: 0x06000455 RID: 1109 RVA: 0x000EB054 File Offset: 0x000EB054
    public void ScheduleRetry(DateTimeOffset now)
    {
      this.NextRunAt = now.AddSeconds(300.0);
      this.PendingRetryAt = new DateTimeOffset?(this.NextRunAt);
    }

    // Token: 0x06000456 RID: 1110 RVA: 0x000EB080 File Offset: 0x000EB080
    public void NoteWaitingForNetwork(DateTimeOffset now)
    {
      DateTimeOffset? waitingForNetworkSince = this.WaitingForNetworkSince;
      DateTimeOffset valueOrDefault = waitingForNetworkSince.GetValueOrDefault();
      if (waitingForNetworkSince == null)
      {
        DateTimeOffset? dateTimeOffset = new DateTimeOffset?(now);
        this.WaitingForNetworkSince = dateTimeOffset;
      }
    }

    // Token: 0x06000457 RID: 1111 RVA: 0x000EB0B8 File Offset: 0x000EB0B8
    [NullableContext(2)]
    public void RecordDispatch(DateTimeOffset now, string taskId)
    {
      this.WaitingForNetworkSince = null;
      int runCount = this.RunCount;
      this.RunCount = runCount + 1;
      this.LastRunStartedAt = new DateTimeOffset?(now);
      this.LastRunFinishedAt = null;
      this.LastRunSummary = null;
      this.LastRunFailed = false;
      this.LastRunTaskId = taskId;
      this.PendingRetryAt = null;
      this.NextRunAt = this.RegularNextRunAt(now);
    }

    // Token: 0x06000458 RID: 1112 RVA: 0x000EB130 File Offset: 0x000EB130
    [NullableContext(2)]
    public bool RecordOutcome(DateTimeOffset now, string summary, bool failed, bool shouldRetry, bool countsTowardFailureCap = true)
    {
      this.LastRunFinishedAt = new DateTimeOffset?(now);
      this.LastRunFailed = failed;
      string text = ((summary != null) ? summary.Trim() : null) ?? "";
      this.LastRunSummary = ((text.Length == 0) ? null : ((text.Length > 500) ? text.Substring(0, 500) : text));
      this.PendingRetryAt = null;
      if (!failed)
      {
        this.ConsecutiveFailedRunCount = 0;
        return false;
      }
      if (shouldRetry && !this.IsPaused)
      {
        if (countsTowardFailureCap)
        {
          int consecutiveFailedRunCount = this.ConsecutiveFailedRunCount;
          this.ConsecutiveFailedRunCount = consecutiveFailedRunCount + 1;
          if (this.ConsecutiveFailedRunCount >= 3)
          {
            this.IsPaused = true;
            return true;
          }
        }
        this.ScheduleRetry(now);
      }
      else if (!shouldRetry)
      {
        DateTimeOffset dateTimeOffset = this.RegularNextRunAt(now);
        if (dateTimeOffset > this.NextRunAt)
        {
          this.NextRunAt = dateTimeOffset;
        }
      }
      return false;
    }

    // Token: 0x06000459 RID: 1113 RVA: 0x000EB20C File Offset: 0x000EB20C
    public static DateTimeOffset NextRunAfterAgentMessageLimit(DateTimeOffset now, DateTimeOffset? allowanceResetsAt)
    {
      if (allowanceResetsAt != null)
      {
        DateTimeOffset valueOrDefault = allowanceResetsAt.GetValueOrDefault();
        if (valueOrDefault > now)
        {
          return valueOrDefault.AddSeconds(60.0);
        }
      }
      return now.AddHours(24.0);
    }

    // Token: 0x0600045A RID: 1114 RVA: 0x000EB254 File Offset: 0x000EB254
    [NullableContext(2)]
    public void RecordAgentMessageLimit(DateTimeOffset now, string summary, DateTimeOffset? allowanceResetsAt)
    {
      this.RecordOutcome(now, summary, true, false, false);
      DateTimeOffset dateTimeOffset = ClickyRoutine.NextRunAfterAgentMessageLimit(now, allowanceResetsAt);
      if (dateTimeOffset > this.NextRunAt)
      {
        this.NextRunAt = dateTimeOffset;
      }
    }

    // Token: 0x0600045B RID: 1115 RVA: 0x000EB28C File Offset: 0x000EB28C
    public static string CadenceLabelFor(int minutes)
    {
      if (minutes % 1440 == 0)
      {
        int num = minutes / 1440;
        if (num != 1)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 1);
          defaultInterpolatedStringHandler.AppendLiteral("Every ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(num);
          defaultInterpolatedStringHandler.AppendLiteral(" days");
          return defaultInterpolatedStringHandler.ToStringAndClear();
        }
        return "Every day";
      }
      else
      {
        if (minutes % 60 != 0)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
          defaultInterpolatedStringHandler.AppendLiteral("Every ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(minutes);
          defaultInterpolatedStringHandler.AppendLiteral(" min");
          return defaultInterpolatedStringHandler.ToStringAndClear();
        }
        int num2 = minutes / 60;
        if (num2 != 1)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 1);
          defaultInterpolatedStringHandler.AppendLiteral("Every ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
          defaultInterpolatedStringHandler.AppendLiteral(" hours");
          return defaultInterpolatedStringHandler.ToStringAndClear();
        }
        return "Every hour";
      }
    }

    // Token: 0x0600045C RID: 1116 RVA: 0x000EB360 File Offset: 0x000EB360
    public static string PromptHeaderLine(string title, int runNumber, string cadenceLabel, DateTimeOffset startedAt)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 4);
      defaultInterpolatedStringHandler.AppendLiteral("[Routine \"");
      defaultInterpolatedStringHandler.AppendFormatted(title);
      defaultInterpolatedStringHandler.AppendLiteral("\" run #");
      defaultInterpolatedStringHandler.AppendFormatted<int>(runNumber);
      defaultInterpolatedStringHandler.AppendLiteral(", ");
      defaultInterpolatedStringHandler.AppendFormatted(cadenceLabel.ToLowerInvariant());
      defaultInterpolatedStringHandler.AppendLiteral(", started ");
      defaultInterpolatedStringHandler.AppendFormatted(startedAt.ToString("MMM d, yyyy 'at' h:mm tt", CultureInfo.GetCultureInfo("en-US")));
      defaultInterpolatedStringHandler.AppendLiteral("]");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x0600045D RID: 1117 RVA: 0x000EB3F4 File Offset: 0x000EB3F4
    public static string TaskPromptStrippingLeadingCadence(string taskPrompt)
    {
      string text = taskPrompt.Trim();
      string[] array = new string[] { "^(every|each)\\s+(\\d+\\s+|other\\s+|few\\s+|couple\\s+of\\s+)?(minutes?|mins?|hours?|hrs?|days?|weeks?|months?|mornings?|afternoons?|evenings?|nights?|weekdays?|weekends?|mondays?|tuesdays?|wednesdays?|thursdays?|fridays?|saturdays?|sundays?)(\\s+(at|around|from|between)\\s+((\\d{1,2}([:.]\\d{2})?(\\s*[ap]\\.?\\s?m\\.?)?|noon|midnight)(\\s*(-|–|to|and)\\s*(\\d{1,2}([:.]\\d{2})?(\\s*[ap]\\.?\\s?m\\.?)?|noon|midnight))?(\\s+(pacific|eastern|central|mountain|local|utc|gmt|[pecm][sd]?t)(\\s+time)?)?|[^,.:;]{1,40}(?=\\s*[,:;])))?\\s*[,:;.\\-–—]?\\s+", "^(hourly|daily|weekly|nightly|monthly|(once|twice)\\s+(a|per)\\s+(day|week|month|hour))\\s*[,:;.\\-–—]?\\s+" };
      string text2 = text;
      foreach (string text3 in array)
      {
        Match match = Regex.Match(text2, text3, RegexOptions.IgnoreCase);
        if (match.Success)
        {
          string text4 = text2;
          int length = match.Length;
          text2 = text4.Substring(length, text4.Length - length);
          break;
        }
      }
      text2 = text2.Trim();
      if (text2.Length != 0)
      {
        char c = char.ToUpperInvariant(text2[0]);
        ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c);
        string text4 = text2;
        return readOnlySpan + text4.Substring(1, text4.Length - 1);
      }
      return text;
    }

    // Token: 0x04000186 RID: 390
    public const double FailureRetryDelaySeconds = 300.0;

    // Token: 0x04000187 RID: 391
    public const int MaximumConsecutiveFailedRunsBeforePause = 3;

    // Token: 0x04000188 RID: 392
    public const int MinimumIntervalMinutes = 2;

    // Token: 0x04000189 RID: 393
    public const int MaximumIntervalMinutes = 10080;

    // Token: 0x0400018A RID: 394
    public const int DayMinutes = 1440;

    // Token: 0x0400018B RID: 395
    public const int WeekMinutes = 10080;

    // Token: 0x02000112 RID: 274
    [NullableContext(2)]
    [Nullable(0)]
    public readonly struct Schedule : IEquatable<ClickyRoutine.Schedule>
    {
      // Token: 0x06000BBC RID: 3004 RVA: 0x00110404 File Offset: 0x00110404
      public Schedule(int Minutes, int? MinuteOfDay, int? Weekday, IReadOnlyList<int> Weekdays)
      {
        this.Minutes = Minutes;
        this.MinuteOfDay = MinuteOfDay;
        this.Weekday = Weekday;
        this.Weekdays = Weekdays;
      }

      // Token: 0x1700029D RID: 669
      // (get) Token: 0x06000BBD RID: 3005 RVA: 0x00110424 File Offset: 0x00110424
      // (set) Token: 0x06000BBE RID: 3006 RVA: 0x0011042C File Offset: 0x0011042C
      public int Minutes { get; set; }

      // Token: 0x1700029E RID: 670
      // (get) Token: 0x06000BBF RID: 3007 RVA: 0x00110438 File Offset: 0x00110438
      // (set) Token: 0x06000BC0 RID: 3008 RVA: 0x00110440 File Offset: 0x00110440
      public int? MinuteOfDay { get; set; }

      // Token: 0x1700029F RID: 671
      // (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0011044C File Offset: 0x0011044C
      // (set) Token: 0x06000BC2 RID: 3010 RVA: 0x00110454 File Offset: 0x00110454
      public int? Weekday { get; set; }

      // Token: 0x170002A0 RID: 672
      // (get) Token: 0x06000BC3 RID: 3011 RVA: 0x00110460 File Offset: 0x00110460
      // (set) Token: 0x06000BC4 RID: 3012 RVA: 0x00110468 File Offset: 0x00110468
      public IReadOnlyList<int> Weekdays { get; set; }

      // Token: 0x06000BC5 RID: 3013 RVA: 0x00110474 File Offset: 0x00110474
      [NullableContext(0)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Schedule");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000BC6 RID: 3014 RVA: 0x001104C0 File Offset: 0x001104C0
      [NullableContext(0)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("Minutes = ");
        builder.Append(this.Minutes.ToString());
        builder.Append(", MinuteOfDay = ");
        builder.Append(this.MinuteOfDay.ToString());
        builder.Append(", Weekday = ");
        builder.Append(this.Weekday.ToString());
        builder.Append(", Weekdays = ");
        builder.Append(this.Weekdays);
        return true;
      }

      // Token: 0x06000BC7 RID: 3015 RVA: 0x0011055C File Offset: 0x0011055C
      [CompilerGenerated]
      public static bool operator !=(ClickyRoutine.Schedule left, ClickyRoutine.Schedule right)
      {
        return !(left == right);
      }

      // Token: 0x06000BC8 RID: 3016 RVA: 0x00110568 File Offset: 0x00110568
      [CompilerGenerated]
      public static bool operator ==(ClickyRoutine.Schedule left, ClickyRoutine.Schedule right)
      {
        return left.Equals(right);
      }

      // Token: 0x06000BC9 RID: 3017 RVA: 0x00110574 File Offset: 0x00110574
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<int>.Default.GetHashCode(this.<Minutes>k__BackingField) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<MinuteOfDay>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<Weekday>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<int>>.Default.GetHashCode(this.<Weekdays>k__BackingField);
      }

      // Token: 0x06000BCA RID: 3018 RVA: 0x001105D8 File Offset: 0x001105D8
      [NullableContext(0)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is ClickyRoutine.Schedule && this.Equals((ClickyRoutine.Schedule)obj);
      }

      // Token: 0x06000BCB RID: 3019 RVA: 0x001105F0 File Offset: 0x001105F0
      [CompilerGenerated]
      public bool Equals(ClickyRoutine.Schedule other)
      {
        return EqualityComparer<int>.Default.Equals(this.<Minutes>k__BackingField, other.<Minutes>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<MinuteOfDay>k__BackingField, other.<MinuteOfDay>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<Weekday>k__BackingField, other.<Weekday>k__BackingField) && EqualityComparer<IReadOnlyList<int>>.Default.Equals(this.<Weekdays>k__BackingField, other.<Weekdays>k__BackingField);
      }

      // Token: 0x06000BCC RID: 3020 RVA: 0x00110660 File Offset: 0x00110660
      [CompilerGenerated]
      public void Deconstruct(out int Minutes, out int? MinuteOfDay, out int? Weekday, out IReadOnlyList<int> Weekdays)
      {
        Minutes = this.Minutes;
        MinuteOfDay = this.MinuteOfDay;
        Weekday = this.Weekday;
        Weekdays = this.Weekdays;
      }
    }
  }
}
