using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents
{
  // Token: 0x02000009 RID: 9
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentActivityItem : IEquatable<AgentActivityItem>
  {
    // Token: 0x0600004A RID: 74 RVA: 0x000D6D18 File Offset: 0x000D6D18
    public AgentActivityItem(string Id, AgentActivityKind Kind, string Text, bool? IsRunning, DateTimeOffset At)
    {
      this.Id = Id;
      this.Kind = Kind;
      this.Text = Text;
      this.IsRunning = IsRunning;
      this.At = At;
      base..ctor();
    }

    // Token: 0x17000017 RID: 23
    // (get) Token: 0x0600004B RID: 75 RVA: 0x000D6D48 File Offset: 0x000D6D48
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentActivityItem);
      }
    }

    // Token: 0x17000018 RID: 24
    // (get) Token: 0x0600004C RID: 76 RVA: 0x000D6D54 File Offset: 0x000D6D54
    // (set) Token: 0x0600004D RID: 77 RVA: 0x000D6D5C File Offset: 0x000D6D5C
    public string Id { get; set; }

    // Token: 0x17000019 RID: 25
    // (get) Token: 0x0600004E RID: 78 RVA: 0x000D6D68 File Offset: 0x000D6D68
    // (set) Token: 0x0600004F RID: 79 RVA: 0x000D6D70 File Offset: 0x000D6D70
    public AgentActivityKind Kind { get; set; }

    // Token: 0x1700001A RID: 26
    // (get) Token: 0x06000050 RID: 80 RVA: 0x000D6D7C File Offset: 0x000D6D7C
    // (set) Token: 0x06000051 RID: 81 RVA: 0x000D6D84 File Offset: 0x000D6D84
    public string Text { get; set; }

    // Token: 0x1700001B RID: 27
    // (get) Token: 0x06000052 RID: 82 RVA: 0x000D6D90 File Offset: 0x000D6D90
    // (set) Token: 0x06000053 RID: 83 RVA: 0x000D6D98 File Offset: 0x000D6D98
    public bool? IsRunning { get; set; }

    // Token: 0x1700001C RID: 28
    // (get) Token: 0x06000054 RID: 84 RVA: 0x000D6DA4 File Offset: 0x000D6DA4
    // (set) Token: 0x06000055 RID: 85 RVA: 0x000D6DAC File Offset: 0x000D6DAC
    public DateTimeOffset At { get; set; }

    // Token: 0x06000056 RID: 86 RVA: 0x000D6DB8 File Offset: 0x000D6DB8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentActivityItem");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000057 RID: 87 RVA: 0x000D6E04 File Offset: 0x000D6E04
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

    // Token: 0x06000058 RID: 88 RVA: 0x000D6EC0 File Offset: 0x000D6EC0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentActivityItem left, AgentActivityItem right)
    {
      return !(left == right);
    }

    // Token: 0x06000059 RID: 89 RVA: 0x000D6ECC File Offset: 0x000D6ECC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentActivityItem left, AgentActivityItem right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600005A RID: 90 RVA: 0x000D6EE0 File Offset: 0x000D6EE0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<AgentActivityKind>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<IsRunning>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<At>k__BackingField);
    }

    // Token: 0x0600005B RID: 91 RVA: 0x000D6F70 File Offset: 0x000D6F70
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentActivityItem);
    }

    // Token: 0x0600005C RID: 92 RVA: 0x000D6F80 File Offset: 0x000D6F80
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentActivityItem other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<AgentActivityKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<IsRunning>k__BackingField, other.<IsRunning>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField));
    }

    // Token: 0x0600005E RID: 94 RVA: 0x000D7030 File Offset: 0x000D7030
    [CompilerGenerated]
    private AgentActivityItem(AgentActivityItem original)
    {
      this.Id = original.<Id>k__BackingField;
      this.Kind = original.<Kind>k__BackingField;
      this.Text = original.<Text>k__BackingField;
      this.IsRunning = original.<IsRunning>k__BackingField;
      this.At = original.<At>k__BackingField;
    }

    // Token: 0x0600005F RID: 95 RVA: 0x000D7080 File Offset: 0x000D7080
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
