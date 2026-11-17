using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Billing
{
  // Token: 0x02000208 RID: 520
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class AgentTurnUsageRecord : IEquatable<AgentTurnUsageRecord>
  {
    // Token: 0x06001E27 RID: 7719 RVA: 0x003ECBB0 File Offset: 0x003ECBB0
    public AgentTurnUsageRecord([Nullable(1)] string TaskId, string ThreadId, string TurnId, string ClickySlug, string ClickyName, DateTimeOffset FinishedAt, [Nullable(1)] string Model, [Nullable(1)] string ReasoningEffort, long InputTokens, long CachedInputTokens, long OutputTokens, string LeaseId = null, double? BilledCostUsd = null)
    {
      this.TaskId = TaskId;
      this.ThreadId = ThreadId;
      this.TurnId = TurnId;
      this.ClickySlug = ClickySlug;
      this.ClickyName = ClickyName;
      this.FinishedAt = FinishedAt;
      this.Model = Model;
      this.ReasoningEffort = ReasoningEffort;
      this.InputTokens = InputTokens;
      this.CachedInputTokens = CachedInputTokens;
      this.OutputTokens = OutputTokens;
      this.LeaseId = LeaseId;
      this.BilledCostUsd = BilledCostUsd;
      base..ctor();
    }

    // Token: 0x170006AA RID: 1706
    // (get) Token: 0x06001E28 RID: 7720 RVA: 0x003ECC28 File Offset: 0x003ECC28
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(AgentTurnUsageRecord);
      }
    }

    // Token: 0x170006AB RID: 1707
    // (get) Token: 0x06001E29 RID: 7721 RVA: 0x003ECC34 File Offset: 0x003ECC34
    // (set) Token: 0x06001E2A RID: 7722 RVA: 0x003ECC3C File Offset: 0x003ECC3C
    [Nullable(1)]
    public string TaskId
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170006AC RID: 1708
    // (get) Token: 0x06001E2B RID: 7723 RVA: 0x003ECC48 File Offset: 0x003ECC48
    // (set) Token: 0x06001E2C RID: 7724 RVA: 0x003ECC50 File Offset: 0x003ECC50
    public string ThreadId { get; set; }

    // Token: 0x170006AD RID: 1709
    // (get) Token: 0x06001E2D RID: 7725 RVA: 0x003ECC5C File Offset: 0x003ECC5C
    // (set) Token: 0x06001E2E RID: 7726 RVA: 0x003ECC64 File Offset: 0x003ECC64
    public string TurnId { get; set; }

    // Token: 0x170006AE RID: 1710
    // (get) Token: 0x06001E2F RID: 7727 RVA: 0x003ECC70 File Offset: 0x003ECC70
    // (set) Token: 0x06001E30 RID: 7728 RVA: 0x003ECC78 File Offset: 0x003ECC78
    public string ClickySlug { get; set; }

    // Token: 0x170006AF RID: 1711
    // (get) Token: 0x06001E31 RID: 7729 RVA: 0x003ECC84 File Offset: 0x003ECC84
    // (set) Token: 0x06001E32 RID: 7730 RVA: 0x003ECC8C File Offset: 0x003ECC8C
    public string ClickyName { get; set; }

    // Token: 0x170006B0 RID: 1712
    // (get) Token: 0x06001E33 RID: 7731 RVA: 0x003ECC98 File Offset: 0x003ECC98
    // (set) Token: 0x06001E34 RID: 7732 RVA: 0x003ECCA0 File Offset: 0x003ECCA0
    public DateTimeOffset FinishedAt { get; set; }

    // Token: 0x170006B1 RID: 1713
    // (get) Token: 0x06001E35 RID: 7733 RVA: 0x003ECCAC File Offset: 0x003ECCAC
    // (set) Token: 0x06001E36 RID: 7734 RVA: 0x003ECCB4 File Offset: 0x003ECCB4
    [Nullable(1)]
    public string Model
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170006B2 RID: 1714
    // (get) Token: 0x06001E37 RID: 7735 RVA: 0x003ECCC0 File Offset: 0x003ECCC0
    // (set) Token: 0x06001E38 RID: 7736 RVA: 0x003ECCC8 File Offset: 0x003ECCC8
    [Nullable(1)]
    public string ReasoningEffort
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170006B3 RID: 1715
    // (get) Token: 0x06001E39 RID: 7737 RVA: 0x003ECCD4 File Offset: 0x003ECCD4
    // (set) Token: 0x06001E3A RID: 7738 RVA: 0x003ECCDC File Offset: 0x003ECCDC
    public long InputTokens { get; set; }

    // Token: 0x170006B4 RID: 1716
    // (get) Token: 0x06001E3B RID: 7739 RVA: 0x003ECCE8 File Offset: 0x003ECCE8
    // (set) Token: 0x06001E3C RID: 7740 RVA: 0x003ECCF0 File Offset: 0x003ECCF0
    public long CachedInputTokens { get; set; }

    // Token: 0x170006B5 RID: 1717
    // (get) Token: 0x06001E3D RID: 7741 RVA: 0x003ECCFC File Offset: 0x003ECCFC
    // (set) Token: 0x06001E3E RID: 7742 RVA: 0x003ECD04 File Offset: 0x003ECD04
    public long OutputTokens { get; set; }

    // Token: 0x170006B6 RID: 1718
    // (get) Token: 0x06001E3F RID: 7743 RVA: 0x003ECD10 File Offset: 0x003ECD10
    // (set) Token: 0x06001E40 RID: 7744 RVA: 0x003ECD18 File Offset: 0x003ECD18
    public string LeaseId { get; set; }

    // Token: 0x170006B7 RID: 1719
    // (get) Token: 0x06001E41 RID: 7745 RVA: 0x003ECD24 File Offset: 0x003ECD24
    // (set) Token: 0x06001E42 RID: 7746 RVA: 0x003ECD2C File Offset: 0x003ECD2C
    public double? BilledCostUsd { get; set; }

    // Token: 0x06001E43 RID: 7747 RVA: 0x003ECD38 File Offset: 0x003ECD38
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentTurnUsageRecord");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001E44 RID: 7748 RVA: 0x003ECD84 File Offset: 0x003ECD84
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("TaskId = ");
      builder.Append(this.TaskId);
      builder.Append(", ThreadId = ");
      builder.Append(this.ThreadId);
      builder.Append(", TurnId = ");
      builder.Append(this.TurnId);
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      builder.Append(", ClickyName = ");
      builder.Append(this.ClickyName);
      builder.Append(", FinishedAt = ");
      builder.Append(this.FinishedAt.ToString());
      builder.Append(", Model = ");
      builder.Append(this.Model);
      builder.Append(", ReasoningEffort = ");
      builder.Append(this.ReasoningEffort);
      builder.Append(", InputTokens = ");
      builder.Append(this.InputTokens.ToString());
      builder.Append(", CachedInputTokens = ");
      builder.Append(this.CachedInputTokens.ToString());
      builder.Append(", OutputTokens = ");
      builder.Append(this.OutputTokens.ToString());
      builder.Append(", LeaseId = ");
      builder.Append(this.LeaseId);
      builder.Append(", BilledCostUsd = ");
      builder.Append(this.BilledCostUsd.ToString());
      return true;
    }

    // Token: 0x06001E45 RID: 7749 RVA: 0x003ECF24 File Offset: 0x003ECF24
    [CompilerGenerated]
    public static bool operator !=(AgentTurnUsageRecord left, AgentTurnUsageRecord right)
    {
      return !(left == right);
    }

    // Token: 0x06001E46 RID: 7750 RVA: 0x003ECF30 File Offset: 0x003ECF30
    [CompilerGenerated]
    public static bool operator ==(AgentTurnUsageRecord left, AgentTurnUsageRecord right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001E47 RID: 7751 RVA: 0x003ECF44 File Offset: 0x003ECF44
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TaskId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TurnId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickyName>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<FinishedAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Model>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ReasoningEffort>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<InputTokens>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<CachedInputTokens>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<OutputTokens>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LeaseId>k__BackingField)) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<BilledCostUsd>k__BackingField);
    }

    // Token: 0x06001E48 RID: 7752 RVA: 0x003ED08C File Offset: 0x003ED08C
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentTurnUsageRecord);
    }

    // Token: 0x06001E49 RID: 7753 RVA: 0x003ED09C File Offset: 0x003ED09C
    [CompilerGenerated]
    public bool Equals(AgentTurnUsageRecord other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<TaskId>k__BackingField, other.<TaskId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TurnId>k__BackingField, other.<TurnId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickyName>k__BackingField, other.<ClickyName>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<FinishedAt>k__BackingField, other.<FinishedAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Model>k__BackingField, other.<Model>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ReasoningEffort>k__BackingField, other.<ReasoningEffort>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<InputTokens>k__BackingField, other.<InputTokens>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<CachedInputTokens>k__BackingField, other.<CachedInputTokens>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<OutputTokens>k__BackingField, other.<OutputTokens>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LeaseId>k__BackingField, other.<LeaseId>k__BackingField) && EqualityComparer<double?>.Default.Equals(this.<BilledCostUsd>k__BackingField, other.<BilledCostUsd>k__BackingField));
    }

    // Token: 0x06001E4B RID: 7755 RVA: 0x003ED224 File Offset: 0x003ED224
    [CompilerGenerated]
    private AgentTurnUsageRecord([Nullable(1)] AgentTurnUsageRecord original)
    {
      this.TaskId = original.<TaskId>k__BackingField;
      this.ThreadId = original.<ThreadId>k__BackingField;
      this.TurnId = original.<TurnId>k__BackingField;
      this.ClickySlug = original.<ClickySlug>k__BackingField;
      this.ClickyName = original.<ClickyName>k__BackingField;
      this.FinishedAt = original.<FinishedAt>k__BackingField;
      this.Model = original.<Model>k__BackingField;
      this.ReasoningEffort = original.<ReasoningEffort>k__BackingField;
      this.InputTokens = original.<InputTokens>k__BackingField;
      this.CachedInputTokens = original.<CachedInputTokens>k__BackingField;
      this.OutputTokens = original.<OutputTokens>k__BackingField;
      this.LeaseId = original.<LeaseId>k__BackingField;
      this.BilledCostUsd = original.<BilledCostUsd>k__BackingField;
    }

    // Token: 0x06001E4C RID: 7756 RVA: 0x003ED2D4 File Offset: 0x003ED2D4
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string TaskId, out string ThreadId, out string TurnId, out string ClickySlug, out string ClickyName, out DateTimeOffset FinishedAt, [Nullable(1)] out string Model, [Nullable(1)] out string ReasoningEffort, out long InputTokens, out long CachedInputTokens, out long OutputTokens, out string LeaseId, out double? BilledCostUsd)
    {
      TaskId = this.TaskId;
      ThreadId = this.ThreadId;
      TurnId = this.TurnId;
      ClickySlug = this.ClickySlug;
      ClickyName = this.ClickyName;
      FinishedAt = this.FinishedAt;
      Model = this.Model;
      ReasoningEffort = this.ReasoningEffort;
      InputTokens = this.InputTokens;
      CachedInputTokens = this.CachedInputTokens;
      OutputTokens = this.OutputTokens;
      LeaseId = this.LeaseId;
      BilledCostUsd = this.BilledCostUsd;
    }
  }
}
