using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000DB RID: 219
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MorningDismissalHistory : IEquatable<MorningDismissalHistory>
  {
    // Token: 0x06000E44 RID: 3652 RVA: 0x0038AE54 File Offset: 0x0038AE54
    public MorningDismissalHistory(List<DateTime> DismissedDays, DateTimeOffset? RestingUntil = null)
    {
      this.DismissedDays = DismissedDays;
      this.RestingUntil = RestingUntil;
      base..ctor();
    }

    // Token: 0x1700032F RID: 815
    // (get) Token: 0x06000E45 RID: 3653 RVA: 0x0038AE6C File Offset: 0x0038AE6C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(MorningDismissalHistory);
      }
    }

    // Token: 0x17000330 RID: 816
    // (get) Token: 0x06000E46 RID: 3654 RVA: 0x0038AE78 File Offset: 0x0038AE78
    // (set) Token: 0x06000E47 RID: 3655 RVA: 0x0038AE80 File Offset: 0x0038AE80
    public List<DateTime> DismissedDays { get; set; }

    // Token: 0x17000331 RID: 817
    // (get) Token: 0x06000E48 RID: 3656 RVA: 0x0038AE8C File Offset: 0x0038AE8C
    // (set) Token: 0x06000E49 RID: 3657 RVA: 0x0038AE94 File Offset: 0x0038AE94
    public DateTimeOffset? RestingUntil { get; set; }

    // Token: 0x17000332 RID: 818
    // (get) Token: 0x06000E4A RID: 3658 RVA: 0x0038AEA0 File Offset: 0x0038AEA0
    public static MorningDismissalHistory Empty
    {
      get
      {
        return new MorningDismissalHistory(new List<DateTime>(), null);
      }
    }

    // Token: 0x06000E4B RID: 3659 RVA: 0x0038AEC0 File Offset: 0x0038AEC0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("MorningDismissalHistory");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000E4C RID: 3660 RVA: 0x0038AF0C File Offset: 0x0038AF0C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("DismissedDays = ");
      builder.Append(this.DismissedDays);
      builder.Append(", RestingUntil = ");
      builder.Append(this.RestingUntil.ToString());
      return true;
    }

    // Token: 0x06000E4D RID: 3661 RVA: 0x0038AF60 File Offset: 0x0038AF60
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(MorningDismissalHistory left, MorningDismissalHistory right)
    {
      return !(left == right);
    }

    // Token: 0x06000E4E RID: 3662 RVA: 0x0038AF6C File Offset: 0x0038AF6C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(MorningDismissalHistory left, MorningDismissalHistory right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000E4F RID: 3663 RVA: 0x0038AF80 File Offset: 0x0038AF80
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<List<DateTime>>.Default.GetHashCode(this.<DismissedDays>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<RestingUntil>k__BackingField);
    }

    // Token: 0x06000E50 RID: 3664 RVA: 0x0038AFC0 File Offset: 0x0038AFC0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as MorningDismissalHistory);
    }

    // Token: 0x06000E51 RID: 3665 RVA: 0x0038AFD0 File Offset: 0x0038AFD0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(MorningDismissalHistory other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<List<DateTime>>.Default.Equals(this.<DismissedDays>k__BackingField, other.<DismissedDays>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<RestingUntil>k__BackingField, other.<RestingUntil>k__BackingField));
    }

    // Token: 0x06000E53 RID: 3667 RVA: 0x0038B034 File Offset: 0x0038B034
    [CompilerGenerated]
    private MorningDismissalHistory(MorningDismissalHistory original)
    {
      this.DismissedDays = original.<DismissedDays>k__BackingField;
      this.RestingUntil = original.<RestingUntil>k__BackingField;
    }

    // Token: 0x06000E54 RID: 3668 RVA: 0x0038B054 File Offset: 0x0038B054
    [CompilerGenerated]
    public void Deconstruct(out List<DateTime> DismissedDays, out DateTimeOffset? RestingUntil)
    {
      DismissedDays = this.DismissedDays;
      RestingUntil = this.RestingUntil;
    }
  }
}
