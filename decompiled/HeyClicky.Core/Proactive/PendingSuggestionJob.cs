using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000DA RID: 218
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PendingSuggestionJob : IEquatable<PendingSuggestionJob>
  {
    // Token: 0x06000E32 RID: 3634 RVA: 0x0038ABC8 File Offset: 0x0038ABC8
    public PendingSuggestionJob(string JobId, DateTimeOffset StartedAt, ProactiveCheckReason CheckReason)
    {
      this.JobId = JobId;
      this.StartedAt = StartedAt;
      this.CheckReason = CheckReason;
      base..ctor();
    }

    // Token: 0x1700032B RID: 811
    // (get) Token: 0x06000E33 RID: 3635 RVA: 0x0038ABE8 File Offset: 0x0038ABE8
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(PendingSuggestionJob);
      }
    }

    // Token: 0x1700032C RID: 812
    // (get) Token: 0x06000E34 RID: 3636 RVA: 0x0038ABF4 File Offset: 0x0038ABF4
    // (set) Token: 0x06000E35 RID: 3637 RVA: 0x0038ABFC File Offset: 0x0038ABFC
    public string JobId { get; set; }

    // Token: 0x1700032D RID: 813
    // (get) Token: 0x06000E36 RID: 3638 RVA: 0x0038AC08 File Offset: 0x0038AC08
    // (set) Token: 0x06000E37 RID: 3639 RVA: 0x0038AC10 File Offset: 0x0038AC10
    public DateTimeOffset StartedAt { get; set; }

    // Token: 0x1700032E RID: 814
    // (get) Token: 0x06000E38 RID: 3640 RVA: 0x0038AC1C File Offset: 0x0038AC1C
    // (set) Token: 0x06000E39 RID: 3641 RVA: 0x0038AC24 File Offset: 0x0038AC24
    public ProactiveCheckReason CheckReason { get; set; }

    // Token: 0x06000E3A RID: 3642 RVA: 0x0038AC30 File Offset: 0x0038AC30
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PendingSuggestionJob");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000E3B RID: 3643 RVA: 0x0038AC7C File Offset: 0x0038AC7C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("JobId = ");
      builder.Append(this.JobId);
      builder.Append(", StartedAt = ");
      builder.Append(this.StartedAt.ToString());
      builder.Append(", CheckReason = ");
      builder.Append(this.CheckReason.ToString());
      return true;
    }

    // Token: 0x06000E3C RID: 3644 RVA: 0x0038ACF8 File Offset: 0x0038ACF8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(PendingSuggestionJob left, PendingSuggestionJob right)
    {
      return !(left == right);
    }

    // Token: 0x06000E3D RID: 3645 RVA: 0x0038AD04 File Offset: 0x0038AD04
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(PendingSuggestionJob left, PendingSuggestionJob right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000E3E RID: 3646 RVA: 0x0038AD18 File Offset: 0x0038AD18
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<JobId>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<StartedAt>k__BackingField)) * -1521134295 + EqualityComparer<ProactiveCheckReason>.Default.GetHashCode(this.<CheckReason>k__BackingField);
    }

    // Token: 0x06000E3F RID: 3647 RVA: 0x0038AD7C File Offset: 0x0038AD7C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PendingSuggestionJob);
    }

    // Token: 0x06000E40 RID: 3648 RVA: 0x0038AD8C File Offset: 0x0038AD8C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(PendingSuggestionJob other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<JobId>k__BackingField, other.<JobId>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<StartedAt>k__BackingField, other.<StartedAt>k__BackingField) && EqualityComparer<ProactiveCheckReason>.Default.Equals(this.<CheckReason>k__BackingField, other.<CheckReason>k__BackingField));
    }

    // Token: 0x06000E42 RID: 3650 RVA: 0x0038AE08 File Offset: 0x0038AE08
    [CompilerGenerated]
    private PendingSuggestionJob(PendingSuggestionJob original)
    {
      this.JobId = original.<JobId>k__BackingField;
      this.StartedAt = original.<StartedAt>k__BackingField;
      this.CheckReason = original.<CheckReason>k__BackingField;
    }

    // Token: 0x06000E43 RID: 3651 RVA: 0x0038AE34 File Offset: 0x0038AE34
    [CompilerGenerated]
    public void Deconstruct(out string JobId, out DateTimeOffset StartedAt, out ProactiveCheckReason CheckReason)
    {
      JobId = this.JobId;
      StartedAt = this.StartedAt;
      CheckReason = this.CheckReason;
    }
  }
}
