using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000CF RID: 207
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ProactiveSlotDayState : IEquatable<ProactiveSlotDayState>
  {
    // Token: 0x06000D73 RID: 3443 RVA: 0x0038248C File Offset: 0x0038248C
    public ProactiveSlotDayState(DateTimeOffset? MorningFiredAt = null, DateTimeOffset? RetryAfter = null, int? ConsecutiveFailedAttempts = null)
    {
      this.MorningFiredAt = MorningFiredAt;
      this.RetryAfter = RetryAfter;
      this.ConsecutiveFailedAttempts = ConsecutiveFailedAttempts;
      base..ctor();
    }

    // Token: 0x170002FA RID: 762
    // (get) Token: 0x06000D74 RID: 3444 RVA: 0x003824AC File Offset: 0x003824AC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ProactiveSlotDayState);
      }
    }

    // Token: 0x170002FB RID: 763
    // (get) Token: 0x06000D75 RID: 3445 RVA: 0x003824B8 File Offset: 0x003824B8
    // (set) Token: 0x06000D76 RID: 3446 RVA: 0x003824C0 File Offset: 0x003824C0
    public DateTimeOffset? MorningFiredAt { get; set; }

    // Token: 0x170002FC RID: 764
    // (get) Token: 0x06000D77 RID: 3447 RVA: 0x003824CC File Offset: 0x003824CC
    // (set) Token: 0x06000D78 RID: 3448 RVA: 0x003824D4 File Offset: 0x003824D4
    public DateTimeOffset? RetryAfter { get; set; }

    // Token: 0x170002FD RID: 765
    // (get) Token: 0x06000D79 RID: 3449 RVA: 0x003824E0 File Offset: 0x003824E0
    // (set) Token: 0x06000D7A RID: 3450 RVA: 0x003824E8 File Offset: 0x003824E8
    public int? ConsecutiveFailedAttempts { get; set; }

    // Token: 0x06000D7B RID: 3451 RVA: 0x003824F4 File Offset: 0x003824F4
    public static TimeSpan RetryDelay(int consecutiveFailures)
    {
      TimeSpan timeSpan;
      if (consecutiveFailures > 1)
      {
        if (consecutiveFailures != 2)
        {
          if (consecutiveFailures != 3)
          {
            timeSpan = TimeSpan.FromHours(3);
          }
          else
          {
            timeSpan = TimeSpan.FromHours(1);
          }
        }
        else
        {
          timeSpan = TimeSpan.FromMinutes(15L);
        }
      }
      else
      {
        timeSpan = TimeSpan.FromMinutes(5L);
      }
      return timeSpan;
    }

    // Token: 0x06000D7C RID: 3452 RVA: 0x00382538 File Offset: 0x00382538
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ProactiveSlotDayState");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000D7D RID: 3453 RVA: 0x00382584 File Offset: 0x00382584
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("MorningFiredAt = ");
      builder.Append(this.MorningFiredAt.ToString());
      builder.Append(", RetryAfter = ");
      builder.Append(this.RetryAfter.ToString());
      builder.Append(", ConsecutiveFailedAttempts = ");
      builder.Append(this.ConsecutiveFailedAttempts.ToString());
      return true;
    }

    // Token: 0x06000D7E RID: 3454 RVA: 0x0038260C File Offset: 0x0038260C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ProactiveSlotDayState left, ProactiveSlotDayState right)
    {
      return !(left == right);
    }

    // Token: 0x06000D7F RID: 3455 RVA: 0x00382618 File Offset: 0x00382618
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ProactiveSlotDayState left, ProactiveSlotDayState right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000D80 RID: 3456 RVA: 0x0038262C File Offset: 0x0038262C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<MorningFiredAt>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<RetryAfter>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<ConsecutiveFailedAttempts>k__BackingField);
    }

    // Token: 0x06000D81 RID: 3457 RVA: 0x00382690 File Offset: 0x00382690
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ProactiveSlotDayState);
    }

    // Token: 0x06000D82 RID: 3458 RVA: 0x003826A0 File Offset: 0x003826A0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ProactiveSlotDayState other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<MorningFiredAt>k__BackingField, other.<MorningFiredAt>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<RetryAfter>k__BackingField, other.<RetryAfter>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<ConsecutiveFailedAttempts>k__BackingField, other.<ConsecutiveFailedAttempts>k__BackingField));
    }

    // Token: 0x06000D84 RID: 3460 RVA: 0x0038271C File Offset: 0x0038271C
    [CompilerGenerated]
    private ProactiveSlotDayState(ProactiveSlotDayState original)
    {
      this.MorningFiredAt = original.<MorningFiredAt>k__BackingField;
      this.RetryAfter = original.<RetryAfter>k__BackingField;
      this.ConsecutiveFailedAttempts = original.<ConsecutiveFailedAttempts>k__BackingField;
    }

    // Token: 0x06000D85 RID: 3461 RVA: 0x00382748 File Offset: 0x00382748
    [CompilerGenerated]
    public void Deconstruct(out DateTimeOffset? MorningFiredAt, out DateTimeOffset? RetryAfter, out int? ConsecutiveFailedAttempts)
    {
      MorningFiredAt = this.MorningFiredAt;
      RetryAfter = this.RetryAfter;
      ConsecutiveFailedAttempts = this.ConsecutiveFailedAttempts;
    }
  }
}
