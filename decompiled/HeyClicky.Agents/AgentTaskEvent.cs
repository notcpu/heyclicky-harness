using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents
{
  // Token: 0x0200000C RID: 12
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentTaskEvent : IEquatable<AgentTaskEvent>
  {
    // Token: 0x060000B4 RID: 180 RVA: 0x000D83D4 File Offset: 0x000D83D4
    public AgentTaskEvent(string TaskId, AgentTaskEventKind Kind, [Nullable(2)] string Text = null)
    {
      this.TaskId = TaskId;
      this.Kind = Kind;
      this.Text = Text;
      base..ctor();
    }

    // Token: 0x17000042 RID: 66
    // (get) Token: 0x060000B5 RID: 181 RVA: 0x000D83F4 File Offset: 0x000D83F4
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentTaskEvent);
      }
    }

    // Token: 0x17000043 RID: 67
    // (get) Token: 0x060000B6 RID: 182 RVA: 0x000D8400 File Offset: 0x000D8400
    // (set) Token: 0x060000B7 RID: 183 RVA: 0x000D8408 File Offset: 0x000D8408
    public string TaskId { get; set; }

    // Token: 0x17000044 RID: 68
    // (get) Token: 0x060000B8 RID: 184 RVA: 0x000D8414 File Offset: 0x000D8414
    // (set) Token: 0x060000B9 RID: 185 RVA: 0x000D841C File Offset: 0x000D841C
    public AgentTaskEventKind Kind { get; set; }

    // Token: 0x17000045 RID: 69
    // (get) Token: 0x060000BA RID: 186 RVA: 0x000D8428 File Offset: 0x000D8428
    // (set) Token: 0x060000BB RID: 187 RVA: 0x000D8430 File Offset: 0x000D8430
    [Nullable(2)]
    public string Text
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x060000BC RID: 188 RVA: 0x000D843C File Offset: 0x000D843C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentTaskEvent");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060000BD RID: 189 RVA: 0x000D8488 File Offset: 0x000D8488
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("TaskId = ");
      builder.Append(this.TaskId);
      builder.Append(", Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", Text = ");
      builder.Append(this.Text);
      return true;
    }

    // Token: 0x060000BE RID: 190 RVA: 0x000D84F4 File Offset: 0x000D84F4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentTaskEvent left, AgentTaskEvent right)
    {
      return !(left == right);
    }

    // Token: 0x060000BF RID: 191 RVA: 0x000D8500 File Offset: 0x000D8500
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentTaskEvent left, AgentTaskEvent right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060000C0 RID: 192 RVA: 0x000D8514 File Offset: 0x000D8514
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TaskId>k__BackingField)) * -1521134295 + EqualityComparer<AgentTaskEventKind>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField);
    }

    // Token: 0x060000C1 RID: 193 RVA: 0x000D8578 File Offset: 0x000D8578
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentTaskEvent);
    }

    // Token: 0x060000C2 RID: 194 RVA: 0x000D8588 File Offset: 0x000D8588
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentTaskEvent other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<TaskId>k__BackingField, other.<TaskId>k__BackingField) && EqualityComparer<AgentTaskEventKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField));
    }

    // Token: 0x060000C4 RID: 196 RVA: 0x000D8604 File Offset: 0x000D8604
    [CompilerGenerated]
    private AgentTaskEvent(AgentTaskEvent original)
    {
      this.TaskId = original.<TaskId>k__BackingField;
      this.Kind = original.<Kind>k__BackingField;
      this.Text = original.<Text>k__BackingField;
    }

    // Token: 0x060000C5 RID: 197 RVA: 0x000D8630 File Offset: 0x000D8630
    [CompilerGenerated]
    public void Deconstruct(out string TaskId, out AgentTaskEventKind Kind, [Nullable(2)] out string Text)
    {
      TaskId = this.TaskId;
      Kind = this.Kind;
      Text = this.Text;
    }
  }
}
