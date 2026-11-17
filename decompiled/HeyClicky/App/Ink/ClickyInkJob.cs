using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001A2 RID: 418
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkJob : IEquatable<ClickyInkJob>
  {
    // Token: 0x06001689 RID: 5769 RVA: 0x005BB04C File Offset: 0x005BB04C
    public ClickyInkJob(string Id, ClickyInkJobPhase Phase, bool IsSeen, DateTimeOffset? FinishedAt, [Nullable(2)] string ThreadId)
    {
      this.Id = Id;
      this.Phase = Phase;
      this.IsSeen = IsSeen;
      this.FinishedAt = FinishedAt;
      this.ThreadId = ThreadId;
      base..ctor();
    }

    // Token: 0x170003FF RID: 1023
    // (get) Token: 0x0600168A RID: 5770 RVA: 0x005BB07C File Offset: 0x005BB07C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkJob);
      }
    }

    // Token: 0x17000400 RID: 1024
    // (get) Token: 0x0600168B RID: 5771 RVA: 0x005BB088 File Offset: 0x005BB088
    // (set) Token: 0x0600168C RID: 5772 RVA: 0x005BB090 File Offset: 0x005BB090
    public string Id { get; set; }

    // Token: 0x17000401 RID: 1025
    // (get) Token: 0x0600168D RID: 5773 RVA: 0x005BB09C File Offset: 0x005BB09C
    // (set) Token: 0x0600168E RID: 5774 RVA: 0x005BB0A4 File Offset: 0x005BB0A4
    public ClickyInkJobPhase Phase { get; set; }

    // Token: 0x17000402 RID: 1026
    // (get) Token: 0x0600168F RID: 5775 RVA: 0x005BB0B0 File Offset: 0x005BB0B0
    // (set) Token: 0x06001690 RID: 5776 RVA: 0x005BB0B8 File Offset: 0x005BB0B8
    public bool IsSeen { get; set; }

    // Token: 0x17000403 RID: 1027
    // (get) Token: 0x06001691 RID: 5777 RVA: 0x005BB0C4 File Offset: 0x005BB0C4
    // (set) Token: 0x06001692 RID: 5778 RVA: 0x005BB0CC File Offset: 0x005BB0CC
    public DateTimeOffset? FinishedAt { get; set; }

    // Token: 0x17000404 RID: 1028
    // (get) Token: 0x06001693 RID: 5779 RVA: 0x005BB0D8 File Offset: 0x005BB0D8
    // (set) Token: 0x06001694 RID: 5780 RVA: 0x005BB0E0 File Offset: 0x005BB0E0
    [Nullable(2)]
    public string ThreadId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06001695 RID: 5781 RVA: 0x005BB0EC File Offset: 0x005BB0EC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkJob");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001696 RID: 5782 RVA: 0x005BB138 File Offset: 0x005BB138
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", Phase = ");
      builder.Append(this.Phase.ToString());
      builder.Append(", IsSeen = ");
      builder.Append(this.IsSeen.ToString());
      builder.Append(", FinishedAt = ");
      builder.Append(this.FinishedAt.ToString());
      builder.Append(", ThreadId = ");
      builder.Append(this.ThreadId);
      return true;
    }

    // Token: 0x06001697 RID: 5783 RVA: 0x005BB1F4 File Offset: 0x005BB1F4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkJob left, ClickyInkJob right)
    {
      return !(left == right);
    }

    // Token: 0x06001698 RID: 5784 RVA: 0x005BB200 File Offset: 0x005BB200
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkJob left, ClickyInkJob right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001699 RID: 5785 RVA: 0x005BB214 File Offset: 0x005BB214
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<ClickyInkJobPhase>.Default.GetHashCode(this.<Phase>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsSeen>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<FinishedAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField);
    }

    // Token: 0x0600169A RID: 5786 RVA: 0x005BB2A4 File Offset: 0x005BB2A4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkJob);
    }

    // Token: 0x0600169B RID: 5787 RVA: 0x005BB2B4 File Offset: 0x005BB2B4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyInkJob other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<ClickyInkJobPhase>.Default.Equals(this.<Phase>k__BackingField, other.<Phase>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsSeen>k__BackingField, other.<IsSeen>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<FinishedAt>k__BackingField, other.<FinishedAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField));
    }

    // Token: 0x0600169D RID: 5789 RVA: 0x005BB364 File Offset: 0x005BB364
    [CompilerGenerated]
    private ClickyInkJob(ClickyInkJob original)
    {
      this.Id = original.<Id>k__BackingField;
      this.Phase = original.<Phase>k__BackingField;
      this.IsSeen = original.<IsSeen>k__BackingField;
      this.FinishedAt = original.<FinishedAt>k__BackingField;
      this.ThreadId = original.<ThreadId>k__BackingField;
    }

    // Token: 0x0600169E RID: 5790 RVA: 0x005BB3B4 File Offset: 0x005BB3B4
    [CompilerGenerated]
    public void Deconstruct(out string Id, out ClickyInkJobPhase Phase, out bool IsSeen, out DateTimeOffset? FinishedAt, [Nullable(2)] out string ThreadId)
    {
      Id = this.Id;
      Phase = this.Phase;
      IsSeen = this.IsSeen;
      FinishedAt = this.FinishedAt;
      ThreadId = this.ThreadId;
    }
  }
}
