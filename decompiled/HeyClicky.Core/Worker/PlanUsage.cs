using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200003E RID: 62
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PlanUsage : IEquatable<PlanUsage>
  {
    // Token: 0x06000225 RID: 549 RVA: 0x00350818 File Offset: 0x00350818
    public PlanUsage(PlanUsageBucket Messages, PlanUsageBucket Agents, [Nullable(2)] PlanUsageBucket Dictation, [Nullable(2)] string WindowResetsAt)
    {
      this.Messages = Messages;
      this.Agents = Agents;
      this.Dictation = Dictation;
      this.WindowResetsAt = WindowResetsAt;
      base..ctor();
    }

    // Token: 0x17000066 RID: 102
    // (get) Token: 0x06000226 RID: 550 RVA: 0x00350840 File Offset: 0x00350840
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(PlanUsage);
      }
    }

    // Token: 0x17000067 RID: 103
    // (get) Token: 0x06000227 RID: 551 RVA: 0x0035084C File Offset: 0x0035084C
    // (set) Token: 0x06000228 RID: 552 RVA: 0x00350854 File Offset: 0x00350854
    public PlanUsageBucket Messages { get; set; }

    // Token: 0x17000068 RID: 104
    // (get) Token: 0x06000229 RID: 553 RVA: 0x00350860 File Offset: 0x00350860
    // (set) Token: 0x0600022A RID: 554 RVA: 0x00350868 File Offset: 0x00350868
    public PlanUsageBucket Agents { get; set; }

    // Token: 0x17000069 RID: 105
    // (get) Token: 0x0600022B RID: 555 RVA: 0x00350874 File Offset: 0x00350874
    // (set) Token: 0x0600022C RID: 556 RVA: 0x0035087C File Offset: 0x0035087C
    [Nullable(2)]
    public PlanUsageBucket Dictation
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700006A RID: 106
    // (get) Token: 0x0600022D RID: 557 RVA: 0x00350888 File Offset: 0x00350888
    // (set) Token: 0x0600022E RID: 558 RVA: 0x00350890 File Offset: 0x00350890
    [Nullable(2)]
    public string WindowResetsAt
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x0600022F RID: 559 RVA: 0x0035089C File Offset: 0x0035089C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PlanUsage");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000230 RID: 560 RVA: 0x003508E8 File Offset: 0x003508E8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Messages = ");
      builder.Append(this.Messages);
      builder.Append(", Agents = ");
      builder.Append(this.Agents);
      builder.Append(", Dictation = ");
      builder.Append(this.Dictation);
      builder.Append(", WindowResetsAt = ");
      builder.Append(this.WindowResetsAt);
      return true;
    }

    // Token: 0x06000231 RID: 561 RVA: 0x00350960 File Offset: 0x00350960
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(PlanUsage left, PlanUsage right)
    {
      return !(left == right);
    }

    // Token: 0x06000232 RID: 562 RVA: 0x0035096C File Offset: 0x0035096C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(PlanUsage left, PlanUsage right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000233 RID: 563 RVA: 0x00350980 File Offset: 0x00350980
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<PlanUsageBucket>.Default.GetHashCode(this.<Messages>k__BackingField)) * -1521134295 + EqualityComparer<PlanUsageBucket>.Default.GetHashCode(this.<Agents>k__BackingField)) * -1521134295 + EqualityComparer<PlanUsageBucket>.Default.GetHashCode(this.<Dictation>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WindowResetsAt>k__BackingField);
    }

    // Token: 0x06000234 RID: 564 RVA: 0x003509FC File Offset: 0x003509FC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PlanUsage);
    }

    // Token: 0x06000235 RID: 565 RVA: 0x00350A0C File Offset: 0x00350A0C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(PlanUsage other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<PlanUsageBucket>.Default.Equals(this.<Messages>k__BackingField, other.<Messages>k__BackingField) && EqualityComparer<PlanUsageBucket>.Default.Equals(this.<Agents>k__BackingField, other.<Agents>k__BackingField) && EqualityComparer<PlanUsageBucket>.Default.Equals(this.<Dictation>k__BackingField, other.<Dictation>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WindowResetsAt>k__BackingField, other.<WindowResetsAt>k__BackingField));
    }

    // Token: 0x06000237 RID: 567 RVA: 0x00350AA0 File Offset: 0x00350AA0
    [CompilerGenerated]
    private PlanUsage(PlanUsage original)
    {
      this.Messages = original.<Messages>k__BackingField;
      this.Agents = original.<Agents>k__BackingField;
      this.Dictation = original.<Dictation>k__BackingField;
      this.WindowResetsAt = original.<WindowResetsAt>k__BackingField;
    }

    // Token: 0x06000238 RID: 568 RVA: 0x00350AD8 File Offset: 0x00350AD8
    [CompilerGenerated]
    public void Deconstruct(out PlanUsageBucket Messages, out PlanUsageBucket Agents, [Nullable(2)] out PlanUsageBucket Dictation, [Nullable(2)] out string WindowResetsAt)
    {
      Messages = this.Messages;
      Agents = this.Agents;
      Dictation = this.Dictation;
      WindowResetsAt = this.WindowResetsAt;
    }
  }
}
