using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Harness
{
  // Token: 0x02000062 RID: 98
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentHarnessLocalMcpCommand : IEquatable<AgentHarnessLocalMcpCommand>
  {
    // Token: 0x06000800 RID: 2048 RVA: 0x000FF304 File Offset: 0x000FF304
    public AgentHarnessLocalMcpCommand(string Command, IReadOnlyList<string> Arguments, IReadOnlyDictionary<string, string> Environment)
    {
      this.Command = Command;
      this.Arguments = Arguments;
      this.Environment = Environment;
      base..ctor();
    }

    // Token: 0x17000205 RID: 517
    // (get) Token: 0x06000801 RID: 2049 RVA: 0x000FF324 File Offset: 0x000FF324
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentHarnessLocalMcpCommand);
      }
    }

    // Token: 0x17000206 RID: 518
    // (get) Token: 0x06000802 RID: 2050 RVA: 0x000FF330 File Offset: 0x000FF330
    // (set) Token: 0x06000803 RID: 2051 RVA: 0x000FF338 File Offset: 0x000FF338
    public string Command { get; set; }

    // Token: 0x17000207 RID: 519
    // (get) Token: 0x06000804 RID: 2052 RVA: 0x000FF344 File Offset: 0x000FF344
    // (set) Token: 0x06000805 RID: 2053 RVA: 0x000FF34C File Offset: 0x000FF34C
    public IReadOnlyList<string> Arguments { get; set; }

    // Token: 0x17000208 RID: 520
    // (get) Token: 0x06000806 RID: 2054 RVA: 0x000FF358 File Offset: 0x000FF358
    // (set) Token: 0x06000807 RID: 2055 RVA: 0x000FF360 File Offset: 0x000FF360
    public IReadOnlyDictionary<string, string> Environment { get; set; }

    // Token: 0x06000808 RID: 2056 RVA: 0x000FF36C File Offset: 0x000FF36C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentHarnessLocalMcpCommand");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000809 RID: 2057 RVA: 0x000FF3B8 File Offset: 0x000FF3B8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Command = ");
      builder.Append(this.Command);
      builder.Append(", Arguments = ");
      builder.Append(this.Arguments);
      builder.Append(", Environment = ");
      builder.Append(this.Environment);
      return true;
    }

    // Token: 0x0600080A RID: 2058 RVA: 0x000FF418 File Offset: 0x000FF418
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentHarnessLocalMcpCommand left, AgentHarnessLocalMcpCommand right)
    {
      return !(left == right);
    }

    // Token: 0x0600080B RID: 2059 RVA: 0x000FF424 File Offset: 0x000FF424
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentHarnessLocalMcpCommand left, AgentHarnessLocalMcpCommand right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600080C RID: 2060 RVA: 0x000FF438 File Offset: 0x000FF438
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Command>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Arguments>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyDictionary<string, string>>.Default.GetHashCode(this.<Environment>k__BackingField);
    }

    // Token: 0x0600080D RID: 2061 RVA: 0x000FF49C File Offset: 0x000FF49C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentHarnessLocalMcpCommand);
    }

    // Token: 0x0600080E RID: 2062 RVA: 0x000FF4AC File Offset: 0x000FF4AC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentHarnessLocalMcpCommand other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Command>k__BackingField, other.<Command>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Arguments>k__BackingField, other.<Arguments>k__BackingField) && EqualityComparer<IReadOnlyDictionary<string, string>>.Default.Equals(this.<Environment>k__BackingField, other.<Environment>k__BackingField));
    }

    // Token: 0x06000810 RID: 2064 RVA: 0x000FF528 File Offset: 0x000FF528
    [CompilerGenerated]
    private AgentHarnessLocalMcpCommand(AgentHarnessLocalMcpCommand original)
    {
      this.Command = original.<Command>k__BackingField;
      this.Arguments = original.<Arguments>k__BackingField;
      this.Environment = original.<Environment>k__BackingField;
    }

    // Token: 0x06000811 RID: 2065 RVA: 0x000FF554 File Offset: 0x000FF554
    [CompilerGenerated]
    public void Deconstruct(out string Command, out IReadOnlyList<string> Arguments, out IReadOnlyDictionary<string, string> Environment)
    {
      Command = this.Command;
      Arguments = this.Arguments;
      Environment = this.Environment;
    }
  }
}
