using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents
{
  // Token: 0x02000012 RID: 18
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentPlanStep : IEquatable<AgentPlanStep>
  {
    // Token: 0x0600012A RID: 298 RVA: 0x000D9738 File Offset: 0x000D9738
    public AgentPlanStep(string Text, string Status)
    {
      this.Text = Text;
      this.Status = Status;
      base..ctor();
    }

    // Token: 0x17000064 RID: 100
    // (get) Token: 0x0600012B RID: 299 RVA: 0x000D9750 File Offset: 0x000D9750
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentPlanStep);
      }
    }

    // Token: 0x17000065 RID: 101
    // (get) Token: 0x0600012C RID: 300 RVA: 0x000D975C File Offset: 0x000D975C
    // (set) Token: 0x0600012D RID: 301 RVA: 0x000D9764 File Offset: 0x000D9764
    public string Text { get; set; }

    // Token: 0x17000066 RID: 102
    // (get) Token: 0x0600012E RID: 302 RVA: 0x000D9770 File Offset: 0x000D9770
    // (set) Token: 0x0600012F RID: 303 RVA: 0x000D9778 File Offset: 0x000D9778
    public string Status { get; set; }

    // Token: 0x17000067 RID: 103
    // (get) Token: 0x06000130 RID: 304 RVA: 0x000D9784 File Offset: 0x000D9784
    public bool IsDone
    {
      get
      {
        return this.Status == "completed";
      }
    }

    // Token: 0x17000068 RID: 104
    // (get) Token: 0x06000131 RID: 305 RVA: 0x000D9798 File Offset: 0x000D9798
    public bool IsNow
    {
      get
      {
        string status = this.Status;
        return status == "inProgress" || status == "in_progress";
      }
    }

    // Token: 0x06000132 RID: 306 RVA: 0x000D97D0 File Offset: 0x000D97D0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentPlanStep");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000133 RID: 307 RVA: 0x000D981C File Offset: 0x000D981C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Text = ");
      builder.Append(this.Text);
      builder.Append(", Status = ");
      builder.Append(this.Status);
      builder.Append(", IsDone = ");
      builder.Append(this.IsDone.ToString());
      builder.Append(", IsNow = ");
      builder.Append(this.IsNow.ToString());
      return true;
    }

    // Token: 0x06000134 RID: 308 RVA: 0x000D98B0 File Offset: 0x000D98B0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentPlanStep left, AgentPlanStep right)
    {
      return !(left == right);
    }

    // Token: 0x06000135 RID: 309 RVA: 0x000D98BC File Offset: 0x000D98BC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentPlanStep left, AgentPlanStep right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000136 RID: 310 RVA: 0x000D98D0 File Offset: 0x000D98D0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Status>k__BackingField);
    }

    // Token: 0x06000137 RID: 311 RVA: 0x000D9910 File Offset: 0x000D9910
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentPlanStep);
    }

    // Token: 0x06000138 RID: 312 RVA: 0x000D9920 File Offset: 0x000D9920
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentPlanStep other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField));
    }

    // Token: 0x0600013A RID: 314 RVA: 0x000D9984 File Offset: 0x000D9984
    [CompilerGenerated]
    private AgentPlanStep(AgentPlanStep original)
    {
      this.Text = original.<Text>k__BackingField;
      this.Status = original.<Status>k__BackingField;
    }

    // Token: 0x0600013B RID: 315 RVA: 0x000D99A4 File Offset: 0x000D99A4
    [CompilerGenerated]
    public void Deconstruct(out string Text, out string Status)
    {
      Text = this.Text;
      Status = this.Status;
    }
  }
}
