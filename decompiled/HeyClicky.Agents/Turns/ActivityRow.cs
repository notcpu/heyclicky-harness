using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Turns
{
  // Token: 0x02000017 RID: 23
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class ActivityRow : IEquatable<ActivityRow>
  {
    // Token: 0x06000326 RID: 806 RVA: 0x000E45FC File Offset: 0x000E45FC
    public ActivityRow(string Id, AgentActivityKind Kind, string Text, bool? IsRunning, DateTimeOffset At)
    {
      this.Id = Id;
      this.Kind = Kind;
      this.Text = Text;
      this.IsRunning = IsRunning;
      this.At = At;
      base..ctor();
    }

    // Token: 0x170000E0 RID: 224
    // (get) Token: 0x06000327 RID: 807 RVA: 0x000E462C File Offset: 0x000E462C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ActivityRow);
      }
    }

    // Token: 0x170000E1 RID: 225
    // (get) Token: 0x06000328 RID: 808 RVA: 0x000E4638 File Offset: 0x000E4638
    // (set) Token: 0x06000329 RID: 809 RVA: 0x000E4640 File Offset: 0x000E4640
    public string Id { get; set; }

    // Token: 0x170000E2 RID: 226
    // (get) Token: 0x0600032A RID: 810 RVA: 0x000E464C File Offset: 0x000E464C
    // (set) Token: 0x0600032B RID: 811 RVA: 0x000E4654 File Offset: 0x000E4654
    public AgentActivityKind Kind { get; set; }

    // Token: 0x170000E3 RID: 227
    // (get) Token: 0x0600032C RID: 812 RVA: 0x000E4660 File Offset: 0x000E4660
    // (set) Token: 0x0600032D RID: 813 RVA: 0x000E4668 File Offset: 0x000E4668
    public string Text { get; set; }

    // Token: 0x170000E4 RID: 228
    // (get) Token: 0x0600032E RID: 814 RVA: 0x000E4674 File Offset: 0x000E4674
    // (set) Token: 0x0600032F RID: 815 RVA: 0x000E467C File Offset: 0x000E467C
    public bool? IsRunning { get; set; }

    // Token: 0x170000E5 RID: 229
    // (get) Token: 0x06000330 RID: 816 RVA: 0x000E4688 File Offset: 0x000E4688
    // (set) Token: 0x06000331 RID: 817 RVA: 0x000E4690 File Offset: 0x000E4690
    public DateTimeOffset At { get; set; }

    // Token: 0x06000332 RID: 818 RVA: 0x000E469C File Offset: 0x000E469C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ActivityRow");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000333 RID: 819 RVA: 0x000E46E8 File Offset: 0x000E46E8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", Text = ");
      builder.Append(this.Text);
      builder.Append(", IsRunning = ");
      builder.Append(this.IsRunning.ToString());
      builder.Append(", At = ");
      builder.Append(this.At.ToString());
      return true;
    }

    // Token: 0x06000334 RID: 820 RVA: 0x000E47A4 File Offset: 0x000E47A4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ActivityRow left, ActivityRow right)
    {
      return !(left == right);
    }

    // Token: 0x06000335 RID: 821 RVA: 0x000E47B0 File Offset: 0x000E47B0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ActivityRow left, ActivityRow right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000336 RID: 822 RVA: 0x000E47C4 File Offset: 0x000E47C4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<AgentActivityKind>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<IsRunning>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<At>k__BackingField);
    }

    // Token: 0x06000337 RID: 823 RVA: 0x000E4854 File Offset: 0x000E4854
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ActivityRow);
    }

    // Token: 0x06000338 RID: 824 RVA: 0x000E4864 File Offset: 0x000E4864
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ActivityRow other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<AgentActivityKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<IsRunning>k__BackingField, other.<IsRunning>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField));
    }

    // Token: 0x0600033A RID: 826 RVA: 0x000E4914 File Offset: 0x000E4914
    [CompilerGenerated]
    private ActivityRow(ActivityRow original)
    {
      this.Id = original.<Id>k__BackingField;
      this.Kind = original.<Kind>k__BackingField;
      this.Text = original.<Text>k__BackingField;
      this.IsRunning = original.<IsRunning>k__BackingField;
      this.At = original.<At>k__BackingField;
    }

    // Token: 0x0600033B RID: 827 RVA: 0x000E4964 File Offset: 0x000E4964
    [CompilerGenerated]
    public void Deconstruct(out string Id, out AgentActivityKind Kind, out string Text, out bool? IsRunning, out DateTimeOffset At)
    {
      Id = this.Id;
      Kind = this.Kind;
      Text = this.Text;
      IsRunning = this.IsRunning;
      At = this.At;
    }
  }
}
