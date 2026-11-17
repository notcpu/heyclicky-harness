using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents
{
  // Token: 0x02000010 RID: 16
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentPaywallEventArgs : IEquatable<AgentPaywallEventArgs>
  {
    // Token: 0x06000116 RID: 278 RVA: 0x000D9490 File Offset: 0x000D9490
    public AgentPaywallEventArgs(int StatusCode, string Body, bool Presents)
    {
      this.StatusCode = StatusCode;
      this.Body = Body;
      this.Presents = Presents;
      base..ctor();
    }

    // Token: 0x1700005F RID: 95
    // (get) Token: 0x06000117 RID: 279 RVA: 0x000D94B0 File Offset: 0x000D94B0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentPaywallEventArgs);
      }
    }

    // Token: 0x17000060 RID: 96
    // (get) Token: 0x06000118 RID: 280 RVA: 0x000D94BC File Offset: 0x000D94BC
    // (set) Token: 0x06000119 RID: 281 RVA: 0x000D94C4 File Offset: 0x000D94C4
    public int StatusCode { get; set; }

    // Token: 0x17000061 RID: 97
    // (get) Token: 0x0600011A RID: 282 RVA: 0x000D94D0 File Offset: 0x000D94D0
    // (set) Token: 0x0600011B RID: 283 RVA: 0x000D94D8 File Offset: 0x000D94D8
    public string Body { get; set; }

    // Token: 0x17000062 RID: 98
    // (get) Token: 0x0600011C RID: 284 RVA: 0x000D94E4 File Offset: 0x000D94E4
    // (set) Token: 0x0600011D RID: 285 RVA: 0x000D94EC File Offset: 0x000D94EC
    public bool Presents { get; set; }

    // Token: 0x0600011E RID: 286 RVA: 0x000D94F8 File Offset: 0x000D94F8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentPaywallEventArgs");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600011F RID: 287 RVA: 0x000D9544 File Offset: 0x000D9544
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("StatusCode = ");
      builder.Append(this.StatusCode.ToString());
      builder.Append(", Body = ");
      builder.Append(this.Body);
      builder.Append(", Presents = ");
      builder.Append(this.Presents.ToString());
      return true;
    }

    // Token: 0x06000120 RID: 288 RVA: 0x000D95C0 File Offset: 0x000D95C0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentPaywallEventArgs left, AgentPaywallEventArgs right)
    {
      return !(left == right);
    }

    // Token: 0x06000121 RID: 289 RVA: 0x000D95CC File Offset: 0x000D95CC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentPaywallEventArgs left, AgentPaywallEventArgs right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000122 RID: 290 RVA: 0x000D95E0 File Offset: 0x000D95E0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<StatusCode>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Body>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Presents>k__BackingField);
    }

    // Token: 0x06000123 RID: 291 RVA: 0x000D9644 File Offset: 0x000D9644
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentPaywallEventArgs);
    }

    // Token: 0x06000124 RID: 292 RVA: 0x000D9654 File Offset: 0x000D9654
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentPaywallEventArgs other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<StatusCode>k__BackingField, other.<StatusCode>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Body>k__BackingField, other.<Body>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Presents>k__BackingField, other.<Presents>k__BackingField));
    }

    // Token: 0x06000126 RID: 294 RVA: 0x000D96D0 File Offset: 0x000D96D0
    [CompilerGenerated]
    private AgentPaywallEventArgs(AgentPaywallEventArgs original)
    {
      this.StatusCode = original.<StatusCode>k__BackingField;
      this.Body = original.<Body>k__BackingField;
      this.Presents = original.<Presents>k__BackingField;
    }

    // Token: 0x06000127 RID: 295 RVA: 0x000D96FC File Offset: 0x000D96FC
    [CompilerGenerated]
    public void Deconstruct(out int StatusCode, out string Body, out bool Presents)
    {
      StatusCode = this.StatusCode;
      Body = this.Body;
      Presents = this.Presents;
    }
  }
}
