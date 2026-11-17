using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.Core.Drive;

namespace HeyClicky.App.Drive
{
  // Token: 0x020002C1 RID: 705
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentDriveBinding : IEquatable<AgentDriveBinding>
  {
    // Token: 0x06001D9F RID: 7583 RVA: 0x00600D34 File Offset: 0x00600D34
    public AgentDriveBinding(string ClickyName, [Nullable(2)] string JobLeaseId, Action<AgentDriveOutcome> OnEnded, [Nullable(2)] string ThreadId = null)
    {
      this.ClickyName = ClickyName;
      this.JobLeaseId = JobLeaseId;
      this.OnEnded = OnEnded;
      this.ThreadId = ThreadId;
      base..ctor();
    }

    // Token: 0x17000542 RID: 1346
    // (get) Token: 0x06001DA0 RID: 7584 RVA: 0x00600D5C File Offset: 0x00600D5C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentDriveBinding);
      }
    }

    // Token: 0x17000543 RID: 1347
    // (get) Token: 0x06001DA1 RID: 7585 RVA: 0x00600D68 File Offset: 0x00600D68
    // (set) Token: 0x06001DA2 RID: 7586 RVA: 0x00600D70 File Offset: 0x00600D70
    public string ClickyName { get; set; }

    // Token: 0x17000544 RID: 1348
    // (get) Token: 0x06001DA3 RID: 7587 RVA: 0x00600D7C File Offset: 0x00600D7C
    // (set) Token: 0x06001DA4 RID: 7588 RVA: 0x00600D84 File Offset: 0x00600D84
    [Nullable(2)]
    public string JobLeaseId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000545 RID: 1349
    // (get) Token: 0x06001DA5 RID: 7589 RVA: 0x00600D90 File Offset: 0x00600D90
    // (set) Token: 0x06001DA6 RID: 7590 RVA: 0x00600D98 File Offset: 0x00600D98
    public Action<AgentDriveOutcome> OnEnded { get; set; }

    // Token: 0x17000546 RID: 1350
    // (get) Token: 0x06001DA7 RID: 7591 RVA: 0x00600DA4 File Offset: 0x00600DA4
    // (set) Token: 0x06001DA8 RID: 7592 RVA: 0x00600DAC File Offset: 0x00600DAC
    [Nullable(2)]
    public string ThreadId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06001DA9 RID: 7593 RVA: 0x00600DB8 File Offset: 0x00600DB8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentDriveBinding");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001DAA RID: 7594 RVA: 0x00600E04 File Offset: 0x00600E04
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ClickyName = ");
      builder.Append(this.ClickyName);
      builder.Append(", JobLeaseId = ");
      builder.Append(this.JobLeaseId);
      builder.Append(", OnEnded = ");
      builder.Append(this.OnEnded);
      builder.Append(", ThreadId = ");
      builder.Append(this.ThreadId);
      return true;
    }

    // Token: 0x06001DAB RID: 7595 RVA: 0x00600E7C File Offset: 0x00600E7C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentDriveBinding left, AgentDriveBinding right)
    {
      return !(left == right);
    }

    // Token: 0x06001DAC RID: 7596 RVA: 0x00600E88 File Offset: 0x00600E88
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentDriveBinding left, AgentDriveBinding right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001DAD RID: 7597 RVA: 0x00600E9C File Offset: 0x00600E9C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickyName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<JobLeaseId>k__BackingField)) * -1521134295 + EqualityComparer<Action<AgentDriveOutcome>>.Default.GetHashCode(this.<OnEnded>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField);
    }

    // Token: 0x06001DAE RID: 7598 RVA: 0x00600F18 File Offset: 0x00600F18
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentDriveBinding);
    }

    // Token: 0x06001DAF RID: 7599 RVA: 0x00600F28 File Offset: 0x00600F28
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentDriveBinding other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ClickyName>k__BackingField, other.<ClickyName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<JobLeaseId>k__BackingField, other.<JobLeaseId>k__BackingField) && EqualityComparer<Action<AgentDriveOutcome>>.Default.Equals(this.<OnEnded>k__BackingField, other.<OnEnded>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField));
    }

    // Token: 0x06001DB1 RID: 7601 RVA: 0x00600FBC File Offset: 0x00600FBC
    [CompilerGenerated]
    private AgentDriveBinding(AgentDriveBinding original)
    {
      this.ClickyName = original.<ClickyName>k__BackingField;
      this.JobLeaseId = original.<JobLeaseId>k__BackingField;
      this.OnEnded = original.<OnEnded>k__BackingField;
      this.ThreadId = original.<ThreadId>k__BackingField;
    }

    // Token: 0x06001DB2 RID: 7602 RVA: 0x00600FF4 File Offset: 0x00600FF4
    [CompilerGenerated]
    public void Deconstruct(out string ClickyName, [Nullable(2)] out string JobLeaseId, out Action<AgentDriveOutcome> OnEnded, [Nullable(2)] out string ThreadId)
    {
      ClickyName = this.ClickyName;
      JobLeaseId = this.JobLeaseId;
      OnEnded = this.OnEnded;
      ThreadId = this.ThreadId;
    }
  }
}
