using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents
{
  // Token: 0x0200000F RID: 15
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentRuntimeTimings : IEquatable<AgentRuntimeTimings>
  {
    // Token: 0x06000102 RID: 258 RVA: 0x000D9174 File Offset: 0x000D9174
    public AgentRuntimeTimings(long RuntimeReadyMs, long LastLaunchToTurnStartedMs, long LastLaunchToFirstTokenMs, long LastLaunchToFinishedMs)
    {
      this.RuntimeReadyMs = RuntimeReadyMs;
      this.LastLaunchToTurnStartedMs = LastLaunchToTurnStartedMs;
      this.LastLaunchToFirstTokenMs = LastLaunchToFirstTokenMs;
      this.LastLaunchToFinishedMs = LastLaunchToFinishedMs;
      base..ctor();
    }

    // Token: 0x1700005A RID: 90
    // (get) Token: 0x06000103 RID: 259 RVA: 0x000D919C File Offset: 0x000D919C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentRuntimeTimings);
      }
    }

    // Token: 0x1700005B RID: 91
    // (get) Token: 0x06000104 RID: 260 RVA: 0x000D91A8 File Offset: 0x000D91A8
    // (set) Token: 0x06000105 RID: 261 RVA: 0x000D91B0 File Offset: 0x000D91B0
    public long RuntimeReadyMs { get; set; }

    // Token: 0x1700005C RID: 92
    // (get) Token: 0x06000106 RID: 262 RVA: 0x000D91BC File Offset: 0x000D91BC
    // (set) Token: 0x06000107 RID: 263 RVA: 0x000D91C4 File Offset: 0x000D91C4
    public long LastLaunchToTurnStartedMs { get; set; }

    // Token: 0x1700005D RID: 93
    // (get) Token: 0x06000108 RID: 264 RVA: 0x000D91D0 File Offset: 0x000D91D0
    // (set) Token: 0x06000109 RID: 265 RVA: 0x000D91D8 File Offset: 0x000D91D8
    public long LastLaunchToFirstTokenMs { get; set; }

    // Token: 0x1700005E RID: 94
    // (get) Token: 0x0600010A RID: 266 RVA: 0x000D91E4 File Offset: 0x000D91E4
    // (set) Token: 0x0600010B RID: 267 RVA: 0x000D91EC File Offset: 0x000D91EC
    public long LastLaunchToFinishedMs { get; set; }

    // Token: 0x0600010C RID: 268 RVA: 0x000D91F8 File Offset: 0x000D91F8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentRuntimeTimings");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600010D RID: 269 RVA: 0x000D9244 File Offset: 0x000D9244
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("RuntimeReadyMs = ");
      builder.Append(this.RuntimeReadyMs.ToString());
      builder.Append(", LastLaunchToTurnStartedMs = ");
      builder.Append(this.LastLaunchToTurnStartedMs.ToString());
      builder.Append(", LastLaunchToFirstTokenMs = ");
      builder.Append(this.LastLaunchToFirstTokenMs.ToString());
      builder.Append(", LastLaunchToFinishedMs = ");
      builder.Append(this.LastLaunchToFinishedMs.ToString());
      return true;
    }

    // Token: 0x0600010E RID: 270 RVA: 0x000D92F4 File Offset: 0x000D92F4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentRuntimeTimings left, AgentRuntimeTimings right)
    {
      return !(left == right);
    }

    // Token: 0x0600010F RID: 271 RVA: 0x000D9300 File Offset: 0x000D9300
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentRuntimeTimings left, AgentRuntimeTimings right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000110 RID: 272 RVA: 0x000D9314 File Offset: 0x000D9314
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<RuntimeReadyMs>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<LastLaunchToTurnStartedMs>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<LastLaunchToFirstTokenMs>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<LastLaunchToFinishedMs>k__BackingField);
    }

    // Token: 0x06000111 RID: 273 RVA: 0x000D9390 File Offset: 0x000D9390
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentRuntimeTimings);
    }

    // Token: 0x06000112 RID: 274 RVA: 0x000D93A0 File Offset: 0x000D93A0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentRuntimeTimings other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<long>.Default.Equals(this.<RuntimeReadyMs>k__BackingField, other.<RuntimeReadyMs>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<LastLaunchToTurnStartedMs>k__BackingField, other.<LastLaunchToTurnStartedMs>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<LastLaunchToFirstTokenMs>k__BackingField, other.<LastLaunchToFirstTokenMs>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<LastLaunchToFinishedMs>k__BackingField, other.<LastLaunchToFinishedMs>k__BackingField));
    }

    // Token: 0x06000114 RID: 276 RVA: 0x000D9434 File Offset: 0x000D9434
    [CompilerGenerated]
    private AgentRuntimeTimings(AgentRuntimeTimings original)
    {
      this.RuntimeReadyMs = original.<RuntimeReadyMs>k__BackingField;
      this.LastLaunchToTurnStartedMs = original.<LastLaunchToTurnStartedMs>k__BackingField;
      this.LastLaunchToFirstTokenMs = original.<LastLaunchToFirstTokenMs>k__BackingField;
      this.LastLaunchToFinishedMs = original.<LastLaunchToFinishedMs>k__BackingField;
    }

    // Token: 0x06000115 RID: 277 RVA: 0x000D946C File Offset: 0x000D946C
    [CompilerGenerated]
    public void Deconstruct(out long RuntimeReadyMs, out long LastLaunchToTurnStartedMs, out long LastLaunchToFirstTokenMs, out long LastLaunchToFinishedMs)
    {
      RuntimeReadyMs = this.RuntimeReadyMs;
      LastLaunchToTurnStartedMs = this.LastLaunchToTurnStartedMs;
      LastLaunchToFirstTokenMs = this.LastLaunchToFirstTokenMs;
      LastLaunchToFinishedMs = this.LastLaunchToFinishedMs;
    }
  }
}
