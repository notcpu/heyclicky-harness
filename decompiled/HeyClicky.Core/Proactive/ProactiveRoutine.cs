using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000CA RID: 202
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ProactiveRoutine : IEquatable<ProactiveRoutine>
  {
    // Token: 0x06000D1B RID: 3355 RVA: 0x00381318 File Offset: 0x00381318
    public ProactiveRoutine(int EveryMinutes, string Title)
    {
      this.EveryMinutes = EveryMinutes;
      this.Title = Title;
      base..ctor();
    }

    // Token: 0x170002DE RID: 734
    // (get) Token: 0x06000D1C RID: 3356 RVA: 0x00381330 File Offset: 0x00381330
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ProactiveRoutine);
      }
    }

    // Token: 0x170002DF RID: 735
    // (get) Token: 0x06000D1D RID: 3357 RVA: 0x0038133C File Offset: 0x0038133C
    // (set) Token: 0x06000D1E RID: 3358 RVA: 0x00381344 File Offset: 0x00381344
    public int EveryMinutes { get; set; }

    // Token: 0x170002E0 RID: 736
    // (get) Token: 0x06000D1F RID: 3359 RVA: 0x00381350 File Offset: 0x00381350
    // (set) Token: 0x06000D20 RID: 3360 RVA: 0x00381358 File Offset: 0x00381358
    public string Title { get; set; }

    // Token: 0x170002E1 RID: 737
    // (get) Token: 0x06000D21 RID: 3361 RVA: 0x00381364 File Offset: 0x00381364
    public int ClampedIntervalMinutes
    {
      get
      {
        return Math.Clamp(this.EveryMinutes, 2, 10080);
      }
    }

    // Token: 0x170002E2 RID: 738
    // (get) Token: 0x06000D22 RID: 3362 RVA: 0x00381378 File Offset: 0x00381378
    public string CadenceLabel
    {
      get
      {
        return ProactiveRoutine.CadenceLabelFor(this.ClampedIntervalMinutes);
      }
    }

    // Token: 0x170002E3 RID: 739
    // (get) Token: 0x06000D23 RID: 3363 RVA: 0x00381388 File Offset: 0x00381388
    public string CadenceSpokenLabel
    {
      get
      {
        int clampedIntervalMinutes = this.ClampedIntervalMinutes;
        if (clampedIntervalMinutes % 10080 == 0)
        {
          int num = clampedIntervalMinutes / 10080;
          if (num != 1)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 1);
            defaultInterpolatedStringHandler.AppendLiteral("every ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(num);
            defaultInterpolatedStringHandler.AppendLiteral(" weeks");
            return defaultInterpolatedStringHandler.ToStringAndClear();
          }
          return "every week";
        }
        else if (clampedIntervalMinutes % 1440 == 0)
        {
          int num2 = clampedIntervalMinutes / 1440;
          if (num2 != 1)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 1);
            defaultInterpolatedStringHandler.AppendLiteral("every ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
            defaultInterpolatedStringHandler.AppendLiteral(" days");
            return defaultInterpolatedStringHandler.ToStringAndClear();
          }
          return "every day";
        }
        else
        {
          if (clampedIntervalMinutes % 60 != 0)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(14, 1);
            defaultInterpolatedStringHandler.AppendLiteral("every ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(clampedIntervalMinutes);
            defaultInterpolatedStringHandler.AppendLiteral(" minutes");
            return defaultInterpolatedStringHandler.ToStringAndClear();
          }
          int num3 = clampedIntervalMinutes / 60;
          if (num3 != 1)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 1);
            defaultInterpolatedStringHandler.AppendLiteral("every ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(num3);
            defaultInterpolatedStringHandler.AppendLiteral(" hours");
            return defaultInterpolatedStringHandler.ToStringAndClear();
          }
          return "every hour";
        }
      }
    }

    // Token: 0x06000D24 RID: 3364 RVA: 0x003814B4 File Offset: 0x003814B4
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

    // Token: 0x06000D25 RID: 3365 RVA: 0x00381588 File Offset: 0x00381588
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ProactiveRoutine");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000D26 RID: 3366 RVA: 0x003815D4 File Offset: 0x003815D4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("EveryMinutes = ");
      builder.Append(this.EveryMinutes.ToString());
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", ClampedIntervalMinutes = ");
      builder.Append(this.ClampedIntervalMinutes.ToString());
      builder.Append(", CadenceLabel = ");
      builder.Append(this.CadenceLabel);
      builder.Append(", CadenceSpokenLabel = ");
      builder.Append(this.CadenceSpokenLabel);
      return true;
    }

    // Token: 0x06000D27 RID: 3367 RVA: 0x00381680 File Offset: 0x00381680
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ProactiveRoutine left, ProactiveRoutine right)
    {
      return !(left == right);
    }

    // Token: 0x06000D28 RID: 3368 RVA: 0x0038168C File Offset: 0x0038168C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ProactiveRoutine left, ProactiveRoutine right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000D29 RID: 3369 RVA: 0x003816A0 File Offset: 0x003816A0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<EveryMinutes>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField);
    }

    // Token: 0x06000D2A RID: 3370 RVA: 0x003816E0 File Offset: 0x003816E0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ProactiveRoutine);
    }

    // Token: 0x06000D2B RID: 3371 RVA: 0x003816F0 File Offset: 0x003816F0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ProactiveRoutine other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<EveryMinutes>k__BackingField, other.<EveryMinutes>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField));
    }

    // Token: 0x06000D2D RID: 3373 RVA: 0x00381754 File Offset: 0x00381754
    [CompilerGenerated]
    private ProactiveRoutine(ProactiveRoutine original)
    {
      this.EveryMinutes = original.<EveryMinutes>k__BackingField;
      this.Title = original.<Title>k__BackingField;
    }

    // Token: 0x06000D2E RID: 3374 RVA: 0x00381774 File Offset: 0x00381774
    [CompilerGenerated]
    public void Deconstruct(out int EveryMinutes, out string Title)
    {
      EveryMinutes = this.EveryMinutes;
      Title = this.Title;
    }

    // Token: 0x04000591 RID: 1425
    public const int MinimumIntervalMinutes = 2;

    // Token: 0x04000592 RID: 1426
    public const int MaximumIntervalMinutes = 10080;
  }
}
