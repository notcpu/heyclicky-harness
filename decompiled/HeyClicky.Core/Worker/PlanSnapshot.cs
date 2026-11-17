using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000040 RID: 64
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class PlanSnapshot : IEquatable<PlanSnapshot>
  {
    // Token: 0x06000253 RID: 595 RVA: 0x00350F70 File Offset: 0x00350F70
    public PlanSnapshot([Nullable(1)] string Plan, [Nullable(1)] PlanUsage Usage, [Nullable(1)] string BillingEnforcement, string Tier = null, string TierSource = null, string TeamName = null, PlanSubscription Subscription = null, int? UnlimitedAgentsCap = null, int? ProAgentsCap = null, int? MaxAgentsCap = null, bool? ProYearlyAvailable = null, bool? MaxYearlyAvailable = null, bool? IsFailOpenFallback = null)
    {
      this.Plan = Plan;
      this.Usage = Usage;
      this.BillingEnforcement = BillingEnforcement;
      this.Tier = Tier;
      this.TierSource = TierSource;
      this.TeamName = TeamName;
      this.Subscription = Subscription;
      this.UnlimitedAgentsCap = UnlimitedAgentsCap;
      this.ProAgentsCap = ProAgentsCap;
      this.MaxAgentsCap = MaxAgentsCap;
      this.ProYearlyAvailable = ProYearlyAvailable;
      this.MaxYearlyAvailable = MaxYearlyAvailable;
      this.IsFailOpenFallback = IsFailOpenFallback;
      base..ctor();
    }

    // Token: 0x17000073 RID: 115
    // (get) Token: 0x06000254 RID: 596 RVA: 0x00350FE8 File Offset: 0x00350FE8
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(PlanSnapshot);
      }
    }

    // Token: 0x17000074 RID: 116
    // (get) Token: 0x06000255 RID: 597 RVA: 0x00350FF4 File Offset: 0x00350FF4
    // (set) Token: 0x06000256 RID: 598 RVA: 0x00350FFC File Offset: 0x00350FFC
    [Nullable(1)]
    public string Plan
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000075 RID: 117
    // (get) Token: 0x06000257 RID: 599 RVA: 0x00351008 File Offset: 0x00351008
    // (set) Token: 0x06000258 RID: 600 RVA: 0x00351010 File Offset: 0x00351010
    [Nullable(1)]
    public PlanUsage Usage
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000076 RID: 118
    // (get) Token: 0x06000259 RID: 601 RVA: 0x0035101C File Offset: 0x0035101C
    // (set) Token: 0x0600025A RID: 602 RVA: 0x00351024 File Offset: 0x00351024
    [Nullable(1)]
    public string BillingEnforcement
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000077 RID: 119
    // (get) Token: 0x0600025B RID: 603 RVA: 0x00351030 File Offset: 0x00351030
    // (set) Token: 0x0600025C RID: 604 RVA: 0x00351038 File Offset: 0x00351038
    public string Tier { get; set; }

    // Token: 0x17000078 RID: 120
    // (get) Token: 0x0600025D RID: 605 RVA: 0x00351044 File Offset: 0x00351044
    // (set) Token: 0x0600025E RID: 606 RVA: 0x0035104C File Offset: 0x0035104C
    public string TierSource { get; set; }

    // Token: 0x17000079 RID: 121
    // (get) Token: 0x0600025F RID: 607 RVA: 0x00351058 File Offset: 0x00351058
    // (set) Token: 0x06000260 RID: 608 RVA: 0x00351060 File Offset: 0x00351060
    public string TeamName { get; set; }

    // Token: 0x1700007A RID: 122
    // (get) Token: 0x06000261 RID: 609 RVA: 0x0035106C File Offset: 0x0035106C
    // (set) Token: 0x06000262 RID: 610 RVA: 0x00351074 File Offset: 0x00351074
    public PlanSubscription Subscription { get; set; }

    // Token: 0x1700007B RID: 123
    // (get) Token: 0x06000263 RID: 611 RVA: 0x00351080 File Offset: 0x00351080
    // (set) Token: 0x06000264 RID: 612 RVA: 0x00351088 File Offset: 0x00351088
    public int? UnlimitedAgentsCap { get; set; }

    // Token: 0x1700007C RID: 124
    // (get) Token: 0x06000265 RID: 613 RVA: 0x00351094 File Offset: 0x00351094
    // (set) Token: 0x06000266 RID: 614 RVA: 0x0035109C File Offset: 0x0035109C
    public int? ProAgentsCap { get; set; }

    // Token: 0x1700007D RID: 125
    // (get) Token: 0x06000267 RID: 615 RVA: 0x003510A8 File Offset: 0x003510A8
    // (set) Token: 0x06000268 RID: 616 RVA: 0x003510B0 File Offset: 0x003510B0
    public int? MaxAgentsCap { get; set; }

    // Token: 0x1700007E RID: 126
    // (get) Token: 0x06000269 RID: 617 RVA: 0x003510BC File Offset: 0x003510BC
    // (set) Token: 0x0600026A RID: 618 RVA: 0x003510C4 File Offset: 0x003510C4
    public bool? ProYearlyAvailable { get; set; }

    // Token: 0x1700007F RID: 127
    // (get) Token: 0x0600026B RID: 619 RVA: 0x003510D0 File Offset: 0x003510D0
    // (set) Token: 0x0600026C RID: 620 RVA: 0x003510D8 File Offset: 0x003510D8
    public bool? MaxYearlyAvailable { get; set; }

    // Token: 0x17000080 RID: 128
    // (get) Token: 0x0600026D RID: 621 RVA: 0x003510E4 File Offset: 0x003510E4
    // (set) Token: 0x0600026E RID: 622 RVA: 0x003510EC File Offset: 0x003510EC
    public bool? IsFailOpenFallback { get; set; }

    // Token: 0x17000081 RID: 129
    // (get) Token: 0x0600026F RID: 623 RVA: 0x003510F8 File Offset: 0x003510F8
    [Nullable(1)]
    [JsonIgnore]
    public string EffectiveTier
    {
      [NullableContext(1)]
      get
      {
        string text;
        if ((text = this.Tier) == null)
        {
          if (!(this.Plan == "unlimited"))
          {
            return "free";
          }
          text = "pro";
        }
        return text;
      }
    }

    // Token: 0x17000082 RID: 130
    // (get) Token: 0x06000270 RID: 624 RVA: 0x00351124 File Offset: 0x00351124
    [JsonIgnore]
    public bool IsFreeUserOverMessageCap
    {
      get
      {
        return this.EffectiveTier == "free" && this.BillingEnforcement == "on" && this.Usage.Messages.Cap > 0 && this.Usage.Messages.Used >= this.Usage.Messages.Cap;
      }
    }

    // Token: 0x06000271 RID: 625 RVA: 0x00351190 File Offset: 0x00351190
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PlanSnapshot");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000272 RID: 626 RVA: 0x003511DC File Offset: 0x003511DC
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Plan = ");
      builder.Append(this.Plan);
      builder.Append(", Usage = ");
      builder.Append(this.Usage);
      builder.Append(", BillingEnforcement = ");
      builder.Append(this.BillingEnforcement);
      builder.Append(", Tier = ");
      builder.Append(this.Tier);
      builder.Append(", TierSource = ");
      builder.Append(this.TierSource);
      builder.Append(", TeamName = ");
      builder.Append(this.TeamName);
      builder.Append(", Subscription = ");
      builder.Append(this.Subscription);
      builder.Append(", UnlimitedAgentsCap = ");
      builder.Append(this.UnlimitedAgentsCap.ToString());
      builder.Append(", ProAgentsCap = ");
      builder.Append(this.ProAgentsCap.ToString());
      builder.Append(", MaxAgentsCap = ");
      builder.Append(this.MaxAgentsCap.ToString());
      builder.Append(", ProYearlyAvailable = ");
      builder.Append(this.ProYearlyAvailable.ToString());
      builder.Append(", MaxYearlyAvailable = ");
      builder.Append(this.MaxYearlyAvailable.ToString());
      builder.Append(", IsFailOpenFallback = ");
      builder.Append(this.IsFailOpenFallback.ToString());
      builder.Append(", EffectiveTier = ");
      builder.Append(this.EffectiveTier);
      builder.Append(", IsFreeUserOverMessageCap = ");
      builder.Append(this.IsFreeUserOverMessageCap.ToString());
      return true;
    }

    // Token: 0x06000273 RID: 627 RVA: 0x003513C8 File Offset: 0x003513C8
    [CompilerGenerated]
    public static bool operator !=(PlanSnapshot left, PlanSnapshot right)
    {
      return !(left == right);
    }

    // Token: 0x06000274 RID: 628 RVA: 0x003513D4 File Offset: 0x003513D4
    [CompilerGenerated]
    public static bool operator ==(PlanSnapshot left, PlanSnapshot right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000275 RID: 629 RVA: 0x003513E8 File Offset: 0x003513E8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Plan>k__BackingField)) * -1521134295 + EqualityComparer<PlanUsage>.Default.GetHashCode(this.<Usage>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BillingEnforcement>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Tier>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TierSource>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TeamName>k__BackingField)) * -1521134295 + EqualityComparer<PlanSubscription>.Default.GetHashCode(this.<Subscription>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<UnlimitedAgentsCap>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<ProAgentsCap>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<MaxAgentsCap>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<ProYearlyAvailable>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<MaxYearlyAvailable>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<IsFailOpenFallback>k__BackingField);
    }

    // Token: 0x06000276 RID: 630 RVA: 0x00351530 File Offset: 0x00351530
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PlanSnapshot);
    }

    // Token: 0x06000277 RID: 631 RVA: 0x00351540 File Offset: 0x00351540
    [CompilerGenerated]
    public bool Equals(PlanSnapshot other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Plan>k__BackingField, other.<Plan>k__BackingField) && EqualityComparer<PlanUsage>.Default.Equals(this.<Usage>k__BackingField, other.<Usage>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BillingEnforcement>k__BackingField, other.<BillingEnforcement>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Tier>k__BackingField, other.<Tier>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TierSource>k__BackingField, other.<TierSource>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TeamName>k__BackingField, other.<TeamName>k__BackingField) && EqualityComparer<PlanSubscription>.Default.Equals(this.<Subscription>k__BackingField, other.<Subscription>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<UnlimitedAgentsCap>k__BackingField, other.<UnlimitedAgentsCap>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<ProAgentsCap>k__BackingField, other.<ProAgentsCap>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<MaxAgentsCap>k__BackingField, other.<MaxAgentsCap>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<ProYearlyAvailable>k__BackingField, other.<ProYearlyAvailable>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<MaxYearlyAvailable>k__BackingField, other.<MaxYearlyAvailable>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<IsFailOpenFallback>k__BackingField, other.<IsFailOpenFallback>k__BackingField));
    }

    // Token: 0x06000279 RID: 633 RVA: 0x003516C8 File Offset: 0x003516C8
    [CompilerGenerated]
    private PlanSnapshot([Nullable(1)] PlanSnapshot original)
    {
      this.Plan = original.<Plan>k__BackingField;
      this.Usage = original.<Usage>k__BackingField;
      this.BillingEnforcement = original.<BillingEnforcement>k__BackingField;
      this.Tier = original.<Tier>k__BackingField;
      this.TierSource = original.<TierSource>k__BackingField;
      this.TeamName = original.<TeamName>k__BackingField;
      this.Subscription = original.<Subscription>k__BackingField;
      this.UnlimitedAgentsCap = original.<UnlimitedAgentsCap>k__BackingField;
      this.ProAgentsCap = original.<ProAgentsCap>k__BackingField;
      this.MaxAgentsCap = original.<MaxAgentsCap>k__BackingField;
      this.ProYearlyAvailable = original.<ProYearlyAvailable>k__BackingField;
      this.MaxYearlyAvailable = original.<MaxYearlyAvailable>k__BackingField;
      this.IsFailOpenFallback = original.<IsFailOpenFallback>k__BackingField;
    }

    // Token: 0x0600027A RID: 634 RVA: 0x00351778 File Offset: 0x00351778
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string Plan, [Nullable(1)] out PlanUsage Usage, [Nullable(1)] out string BillingEnforcement, out string Tier, out string TierSource, out string TeamName, out PlanSubscription Subscription, out int? UnlimitedAgentsCap, out int? ProAgentsCap, out int? MaxAgentsCap, out bool? ProYearlyAvailable, out bool? MaxYearlyAvailable, out bool? IsFailOpenFallback)
    {
      Plan = this.Plan;
      Usage = this.Usage;
      BillingEnforcement = this.BillingEnforcement;
      Tier = this.Tier;
      TierSource = this.TierSource;
      TeamName = this.TeamName;
      Subscription = this.Subscription;
      UnlimitedAgentsCap = this.UnlimitedAgentsCap;
      ProAgentsCap = this.ProAgentsCap;
      MaxAgentsCap = this.MaxAgentsCap;
      ProYearlyAvailable = this.ProYearlyAvailable;
      MaxYearlyAvailable = this.MaxYearlyAvailable;
      IsFailOpenFallback = this.IsFailOpenFallback;
    }
  }
}
