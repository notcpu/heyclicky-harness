using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000164 RID: 356
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchRestingMarkState : IEquatable<NotchRestingMarkState>
  {
    // Token: 0x0600138C RID: 5004 RVA: 0x005A3278 File Offset: 0x005A3278
    public NotchRestingMarkState(int WorkingCount, string AlertPhase)
    {
      this.WorkingCount = WorkingCount;
      this.AlertPhase = AlertPhase;
      base..ctor();
    }

    // Token: 0x1700038F RID: 911
    // (get) Token: 0x0600138D RID: 5005 RVA: 0x005A3290 File Offset: 0x005A3290
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(NotchRestingMarkState);
      }
    }

    // Token: 0x17000390 RID: 912
    // (get) Token: 0x0600138E RID: 5006 RVA: 0x005A329C File Offset: 0x005A329C
    // (set) Token: 0x0600138F RID: 5007 RVA: 0x005A32A4 File Offset: 0x005A32A4
    public int WorkingCount { get; set; }

    // Token: 0x17000391 RID: 913
    // (get) Token: 0x06001390 RID: 5008 RVA: 0x005A32B0 File Offset: 0x005A32B0
    // (set) Token: 0x06001391 RID: 5009 RVA: 0x005A32B8 File Offset: 0x005A32B8
    public string AlertPhase { get; set; }

    // Token: 0x06001392 RID: 5010 RVA: 0x005A32C4 File Offset: 0x005A32C4
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("NotchRestingMarkState");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001393 RID: 5011 RVA: 0x005A3310 File Offset: 0x005A3310
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("WorkingCount = ");
      builder.Append(this.WorkingCount.ToString());
      builder.Append(", AlertPhase = ");
      builder.Append(this.AlertPhase);
      return true;
    }

    // Token: 0x06001394 RID: 5012 RVA: 0x005A3364 File Offset: 0x005A3364
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(NotchRestingMarkState left, NotchRestingMarkState right)
    {
      return !(left == right);
    }

    // Token: 0x06001395 RID: 5013 RVA: 0x005A3370 File Offset: 0x005A3370
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(NotchRestingMarkState left, NotchRestingMarkState right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001396 RID: 5014 RVA: 0x005A3384 File Offset: 0x005A3384
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<WorkingCount>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AlertPhase>k__BackingField);
    }

    // Token: 0x06001397 RID: 5015 RVA: 0x005A33C4 File Offset: 0x005A33C4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as NotchRestingMarkState);
    }

    // Token: 0x06001398 RID: 5016 RVA: 0x005A33D4 File Offset: 0x005A33D4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(NotchRestingMarkState other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<WorkingCount>k__BackingField, other.<WorkingCount>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AlertPhase>k__BackingField, other.<AlertPhase>k__BackingField));
    }

    // Token: 0x0600139A RID: 5018 RVA: 0x005A3438 File Offset: 0x005A3438
    [CompilerGenerated]
    private NotchRestingMarkState(NotchRestingMarkState original)
    {
      this.WorkingCount = original.<WorkingCount>k__BackingField;
      this.AlertPhase = original.<AlertPhase>k__BackingField;
    }

    // Token: 0x0600139B RID: 5019 RVA: 0x005A3458 File Offset: 0x005A3458
    [CompilerGenerated]
    public void Deconstruct(out int WorkingCount, out string AlertPhase)
    {
      WorkingCount = this.WorkingCount;
      AlertPhase = this.AlertPhase;
    }
  }
}
