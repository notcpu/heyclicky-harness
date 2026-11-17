using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000144 RID: 324
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkRoutineControl : IEquatable<ClickyInkRoutineControl>
  {
    // Token: 0x0600145A RID: 5210 RVA: 0x003ACAF8 File Offset: 0x003ACAF8
    public ClickyInkRoutineControl(string Id, string Title, int IntervalMinutes, bool IsPaused, bool IsPausedAfterFailures, bool IsRunning, DateTimeOffset NextRunAt, int RunCount)
    {
      this.Id = Id;
      this.Title = Title;
      this.IntervalMinutes = IntervalMinutes;
      this.IsPaused = IsPaused;
      this.IsPausedAfterFailures = IsPausedAfterFailures;
      this.IsRunning = IsRunning;
      this.NextRunAt = NextRunAt;
      this.RunCount = RunCount;
      base..ctor();
    }

    // Token: 0x170004CA RID: 1226
    // (get) Token: 0x0600145B RID: 5211 RVA: 0x003ACB48 File Offset: 0x003ACB48
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkRoutineControl);
      }
    }

    // Token: 0x170004CB RID: 1227
    // (get) Token: 0x0600145C RID: 5212 RVA: 0x003ACB54 File Offset: 0x003ACB54
    // (set) Token: 0x0600145D RID: 5213 RVA: 0x003ACB5C File Offset: 0x003ACB5C
    public string Id { get; set; }

    // Token: 0x170004CC RID: 1228
    // (get) Token: 0x0600145E RID: 5214 RVA: 0x003ACB68 File Offset: 0x003ACB68
    // (set) Token: 0x0600145F RID: 5215 RVA: 0x003ACB70 File Offset: 0x003ACB70
    public string Title { get; set; }

    // Token: 0x170004CD RID: 1229
    // (get) Token: 0x06001460 RID: 5216 RVA: 0x003ACB7C File Offset: 0x003ACB7C
    // (set) Token: 0x06001461 RID: 5217 RVA: 0x003ACB84 File Offset: 0x003ACB84
    public int IntervalMinutes { get; set; }

    // Token: 0x170004CE RID: 1230
    // (get) Token: 0x06001462 RID: 5218 RVA: 0x003ACB90 File Offset: 0x003ACB90
    // (set) Token: 0x06001463 RID: 5219 RVA: 0x003ACB98 File Offset: 0x003ACB98
    public bool IsPaused { get; set; }

    // Token: 0x170004CF RID: 1231
    // (get) Token: 0x06001464 RID: 5220 RVA: 0x003ACBA4 File Offset: 0x003ACBA4
    // (set) Token: 0x06001465 RID: 5221 RVA: 0x003ACBAC File Offset: 0x003ACBAC
    public bool IsPausedAfterFailures { get; set; }

    // Token: 0x170004D0 RID: 1232
    // (get) Token: 0x06001466 RID: 5222 RVA: 0x003ACBB8 File Offset: 0x003ACBB8
    // (set) Token: 0x06001467 RID: 5223 RVA: 0x003ACBC0 File Offset: 0x003ACBC0
    public bool IsRunning { get; set; }

    // Token: 0x170004D1 RID: 1233
    // (get) Token: 0x06001468 RID: 5224 RVA: 0x003ACBCC File Offset: 0x003ACBCC
    // (set) Token: 0x06001469 RID: 5225 RVA: 0x003ACBD4 File Offset: 0x003ACBD4
    public DateTimeOffset NextRunAt { get; set; }

    // Token: 0x170004D2 RID: 1234
    // (get) Token: 0x0600146A RID: 5226 RVA: 0x003ACBE0 File Offset: 0x003ACBE0
    // (set) Token: 0x0600146B RID: 5227 RVA: 0x003ACBE8 File Offset: 0x003ACBE8
    public int RunCount { get; set; }

    // Token: 0x0600146C RID: 5228 RVA: 0x003ACBF4 File Offset: 0x003ACBF4
    public static string NextRunWords(DateTimeOffset date, DateTimeOffset? now = null)
    {
      DateTimeOffset dateTimeOffset = date.ToLocalTime();
      DateTime date2 = (now ?? DateTimeOffset.Now).ToLocalTime().Date;
      string text = dateTimeOffset.ToString("t", CultureInfo.CurrentCulture);
      if (dateTimeOffset.Date == date2)
      {
        return "today " + text;
      }
      if (dateTimeOffset.Date == date2.AddDays(1.0))
      {
        return "tomorrow " + text;
      }
      return dateTimeOffset.ToString("ddd", CultureInfo.CurrentCulture) + " " + text;
    }

    // Token: 0x0600146D RID: 5229 RVA: 0x003ACCA8 File Offset: 0x003ACCA8
    public static string CadenceWords(int minutes)
    {
      if (minutes != 10080)
      {
        return ClickyInkRoutineControl.CadenceLabel(minutes);
      }
      return "Every week";
    }

    // Token: 0x0600146E RID: 5230 RVA: 0x003ACCC0 File Offset: 0x003ACCC0
    public static string CadenceLabel(int minutes)
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

    // Token: 0x0600146F RID: 5231 RVA: 0x003ACD94 File Offset: 0x003ACD94
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkRoutineControl");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001470 RID: 5232 RVA: 0x003ACDE0 File Offset: 0x003ACDE0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", IntervalMinutes = ");
      builder.Append(this.IntervalMinutes.ToString());
      builder.Append(", IsPaused = ");
      builder.Append(this.IsPaused.ToString());
      builder.Append(", IsPausedAfterFailures = ");
      builder.Append(this.IsPausedAfterFailures.ToString());
      builder.Append(", IsRunning = ");
      builder.Append(this.IsRunning.ToString());
      builder.Append(", NextRunAt = ");
      builder.Append(this.NextRunAt.ToString());
      builder.Append(", RunCount = ");
      builder.Append(this.RunCount.ToString());
      return true;
    }

    // Token: 0x06001471 RID: 5233 RVA: 0x003ACF10 File Offset: 0x003ACF10
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkRoutineControl left, ClickyInkRoutineControl right)
    {
      return !(left == right);
    }

    // Token: 0x06001472 RID: 5234 RVA: 0x003ACF1C File Offset: 0x003ACF1C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkRoutineControl left, ClickyInkRoutineControl right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001473 RID: 5235 RVA: 0x003ACF30 File Offset: 0x003ACF30
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<IntervalMinutes>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsPaused>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsPausedAfterFailures>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsRunning>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<NextRunAt>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<RunCount>k__BackingField);
    }

    // Token: 0x06001474 RID: 5236 RVA: 0x003AD008 File Offset: 0x003AD008
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkRoutineControl);
    }

    // Token: 0x06001475 RID: 5237 RVA: 0x003AD018 File Offset: 0x003AD018
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyInkRoutineControl other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<IntervalMinutes>k__BackingField, other.<IntervalMinutes>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsPaused>k__BackingField, other.<IsPaused>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsPausedAfterFailures>k__BackingField, other.<IsPausedAfterFailures>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsRunning>k__BackingField, other.<IsRunning>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<NextRunAt>k__BackingField, other.<NextRunAt>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<RunCount>k__BackingField, other.<RunCount>k__BackingField));
    }

    // Token: 0x06001477 RID: 5239 RVA: 0x003AD118 File Offset: 0x003AD118
    [CompilerGenerated]
    private ClickyInkRoutineControl(ClickyInkRoutineControl original)
    {
      this.Id = original.<Id>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.IntervalMinutes = original.<IntervalMinutes>k__BackingField;
      this.IsPaused = original.<IsPaused>k__BackingField;
      this.IsPausedAfterFailures = original.<IsPausedAfterFailures>k__BackingField;
      this.IsRunning = original.<IsRunning>k__BackingField;
      this.NextRunAt = original.<NextRunAt>k__BackingField;
      this.RunCount = original.<RunCount>k__BackingField;
    }

    // Token: 0x06001478 RID: 5240 RVA: 0x003AD18C File Offset: 0x003AD18C
    [CompilerGenerated]
    public void Deconstruct(out string Id, out string Title, out int IntervalMinutes, out bool IsPaused, out bool IsPausedAfterFailures, out bool IsRunning, out DateTimeOffset NextRunAt, out int RunCount)
    {
      Id = this.Id;
      Title = this.Title;
      IntervalMinutes = this.IntervalMinutes;
      IsPaused = this.IsPaused;
      IsPausedAfterFailures = this.IsPausedAfterFailures;
      IsRunning = this.IsRunning;
      NextRunAt = this.NextRunAt;
      RunCount = this.RunCount;
    }
  }
}
