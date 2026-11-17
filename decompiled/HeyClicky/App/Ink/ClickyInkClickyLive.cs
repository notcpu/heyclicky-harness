using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001A0 RID: 416
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkClickyLive : IEquatable<ClickyInkClickyLive>
  {
    // Token: 0x06001679 RID: 5753 RVA: 0x005BAE44 File Offset: 0x005BAE44
    public ClickyInkClickyLive(ClickyInkClickyPhase Phase, DateTimeOffset? FinishedAt = null)
    {
      this.Phase = Phase;
      this.FinishedAt = FinishedAt;
      base..ctor();
    }

    // Token: 0x170003FC RID: 1020
    // (get) Token: 0x0600167A RID: 5754 RVA: 0x005BAE5C File Offset: 0x005BAE5C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkClickyLive);
      }
    }

    // Token: 0x170003FD RID: 1021
    // (get) Token: 0x0600167B RID: 5755 RVA: 0x005BAE68 File Offset: 0x005BAE68
    // (set) Token: 0x0600167C RID: 5756 RVA: 0x005BAE70 File Offset: 0x005BAE70
    public ClickyInkClickyPhase Phase { get; set; }

    // Token: 0x170003FE RID: 1022
    // (get) Token: 0x0600167D RID: 5757 RVA: 0x005BAE7C File Offset: 0x005BAE7C
    // (set) Token: 0x0600167E RID: 5758 RVA: 0x005BAE84 File Offset: 0x005BAE84
    public DateTimeOffset? FinishedAt { get; set; }

    // Token: 0x0600167F RID: 5759 RVA: 0x005BAE90 File Offset: 0x005BAE90
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkClickyLive");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001680 RID: 5760 RVA: 0x005BAEDC File Offset: 0x005BAEDC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Phase = ");
      builder.Append(this.Phase.ToString());
      builder.Append(", FinishedAt = ");
      builder.Append(this.FinishedAt.ToString());
      return true;
    }

    // Token: 0x06001681 RID: 5761 RVA: 0x005BAF40 File Offset: 0x005BAF40
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkClickyLive left, ClickyInkClickyLive right)
    {
      return !(left == right);
    }

    // Token: 0x06001682 RID: 5762 RVA: 0x005BAF4C File Offset: 0x005BAF4C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkClickyLive left, ClickyInkClickyLive right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001683 RID: 5763 RVA: 0x005BAF60 File Offset: 0x005BAF60
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ClickyInkClickyPhase>.Default.GetHashCode(this.<Phase>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<FinishedAt>k__BackingField);
    }

    // Token: 0x06001684 RID: 5764 RVA: 0x005BAFA0 File Offset: 0x005BAFA0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkClickyLive);
    }

    // Token: 0x06001685 RID: 5765 RVA: 0x005BAFB0 File Offset: 0x005BAFB0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyInkClickyLive other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ClickyInkClickyPhase>.Default.Equals(this.<Phase>k__BackingField, other.<Phase>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<FinishedAt>k__BackingField, other.<FinishedAt>k__BackingField));
    }

    // Token: 0x06001687 RID: 5767 RVA: 0x005BB014 File Offset: 0x005BB014
    [CompilerGenerated]
    private ClickyInkClickyLive(ClickyInkClickyLive original)
    {
      this.Phase = original.<Phase>k__BackingField;
      this.FinishedAt = original.<FinishedAt>k__BackingField;
    }

    // Token: 0x06001688 RID: 5768 RVA: 0x005BB034 File Offset: 0x005BB034
    [CompilerGenerated]
    public void Deconstruct(out ClickyInkClickyPhase Phase, out DateTimeOffset? FinishedAt)
    {
      Phase = this.Phase;
      FinishedAt = this.FinishedAt;
    }
  }
}
