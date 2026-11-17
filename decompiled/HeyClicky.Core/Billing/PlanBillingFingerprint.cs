using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core.Billing
{
  // Token: 0x02000211 RID: 529
  [NullableContext(2)]
  [Nullable(0)]
  internal sealed class PlanBillingFingerprint : IEquatable<PlanBillingFingerprint>
  {
    // Token: 0x06001EEF RID: 7919 RVA: 0x003F06A8 File Offset: 0x003F06A8
    public PlanBillingFingerprint([Nullable(1)] string Plan, [Nullable(1)] string Tier, string Status, string ProductId, bool? CancelsAtPeriodEnd, string BillingInterval)
    {
      this.Plan = Plan;
      this.Tier = Tier;
      this.Status = Status;
      this.ProductId = ProductId;
      this.CancelsAtPeriodEnd = CancelsAtPeriodEnd;
      this.BillingInterval = BillingInterval;
      base..ctor();
    }

    // Token: 0x170006E7 RID: 1767
    // (get) Token: 0x06001EF0 RID: 7920 RVA: 0x003F06E0 File Offset: 0x003F06E0
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(PlanBillingFingerprint);
      }
    }

    // Token: 0x170006E8 RID: 1768
    // (get) Token: 0x06001EF1 RID: 7921 RVA: 0x003F06EC File Offset: 0x003F06EC
    // (set) Token: 0x06001EF2 RID: 7922 RVA: 0x003F06F4 File Offset: 0x003F06F4
    [Nullable(1)]
    public string Plan
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170006E9 RID: 1769
    // (get) Token: 0x06001EF3 RID: 7923 RVA: 0x003F0700 File Offset: 0x003F0700
    // (set) Token: 0x06001EF4 RID: 7924 RVA: 0x003F0708 File Offset: 0x003F0708
    [Nullable(1)]
    public string Tier
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170006EA RID: 1770
    // (get) Token: 0x06001EF5 RID: 7925 RVA: 0x003F0714 File Offset: 0x003F0714
    // (set) Token: 0x06001EF6 RID: 7926 RVA: 0x003F071C File Offset: 0x003F071C
    public string Status { get; set; }

    // Token: 0x170006EB RID: 1771
    // (get) Token: 0x06001EF7 RID: 7927 RVA: 0x003F0728 File Offset: 0x003F0728
    // (set) Token: 0x06001EF8 RID: 7928 RVA: 0x003F0730 File Offset: 0x003F0730
    public string ProductId { get; set; }

    // Token: 0x170006EC RID: 1772
    // (get) Token: 0x06001EF9 RID: 7929 RVA: 0x003F073C File Offset: 0x003F073C
    // (set) Token: 0x06001EFA RID: 7930 RVA: 0x003F0744 File Offset: 0x003F0744
    public bool? CancelsAtPeriodEnd { get; set; }

    // Token: 0x170006ED RID: 1773
    // (get) Token: 0x06001EFB RID: 7931 RVA: 0x003F0750 File Offset: 0x003F0750
    // (set) Token: 0x06001EFC RID: 7932 RVA: 0x003F0758 File Offset: 0x003F0758
    public string BillingInterval { get; set; }

    // Token: 0x06001EFD RID: 7933 RVA: 0x003F0764 File Offset: 0x003F0764
    public static PlanBillingFingerprint From(PlanSnapshot s)
    {
      if (s != null && !s.IsFailOpenFallback.GetValueOrDefault())
      {
        string plan = s.Plan;
        string effectiveTier = s.EffectiveTier;
        PlanSubscription subscription = s.Subscription;
        string text = ((subscription != null) ? subscription.Status : null);
        PlanSubscription subscription2 = s.Subscription;
        string text2 = ((subscription2 != null) ? subscription2.ProductId : null);
        PlanSubscription subscription3 = s.Subscription;
        bool? flag = ((subscription3 != null) ? new bool?(subscription3.CancelAtPeriodEnd) : null);
        PlanSubscription subscription4 = s.Subscription;
        return new PlanBillingFingerprint(plan, effectiveTier, text, text2, flag, (subscription4 != null) ? subscription4.BillingInterval : null);
      }
      return null;
    }

    // Token: 0x06001EFE RID: 7934 RVA: 0x003F07EC File Offset: 0x003F07EC
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PlanBillingFingerprint");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001EFF RID: 7935 RVA: 0x003F0838 File Offset: 0x003F0838
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Plan = ");
      builder.Append(this.Plan);
      builder.Append(", Tier = ");
      builder.Append(this.Tier);
      builder.Append(", Status = ");
      builder.Append(this.Status);
      builder.Append(", ProductId = ");
      builder.Append(this.ProductId);
      builder.Append(", CancelsAtPeriodEnd = ");
      builder.Append(this.CancelsAtPeriodEnd.ToString());
      builder.Append(", BillingInterval = ");
      builder.Append(this.BillingInterval);
      return true;
    }

    // Token: 0x06001F00 RID: 7936 RVA: 0x003F08F0 File Offset: 0x003F08F0
    [CompilerGenerated]
    public static bool operator !=(PlanBillingFingerprint left, PlanBillingFingerprint right)
    {
      return !(left == right);
    }

    // Token: 0x06001F01 RID: 7937 RVA: 0x003F08FC File Offset: 0x003F08FC
    [CompilerGenerated]
    public static bool operator ==(PlanBillingFingerprint left, PlanBillingFingerprint right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001F02 RID: 7938 RVA: 0x003F0910 File Offset: 0x003F0910
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Plan>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Tier>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ProductId>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<CancelsAtPeriodEnd>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BillingInterval>k__BackingField);
    }

    // Token: 0x06001F03 RID: 7939 RVA: 0x003F09B8 File Offset: 0x003F09B8
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PlanBillingFingerprint);
    }

    // Token: 0x06001F04 RID: 7940 RVA: 0x003F09C8 File Offset: 0x003F09C8
    [CompilerGenerated]
    public bool Equals(PlanBillingFingerprint other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Plan>k__BackingField, other.<Plan>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Tier>k__BackingField, other.<Tier>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ProductId>k__BackingField, other.<ProductId>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<CancelsAtPeriodEnd>k__BackingField, other.<CancelsAtPeriodEnd>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BillingInterval>k__BackingField, other.<BillingInterval>k__BackingField));
    }

    // Token: 0x06001F06 RID: 7942 RVA: 0x003F0A94 File Offset: 0x003F0A94
    [CompilerGenerated]
    private PlanBillingFingerprint([Nullable(1)] PlanBillingFingerprint original)
    {
      this.Plan = original.<Plan>k__BackingField;
      this.Tier = original.<Tier>k__BackingField;
      this.Status = original.<Status>k__BackingField;
      this.ProductId = original.<ProductId>k__BackingField;
      this.CancelsAtPeriodEnd = original.<CancelsAtPeriodEnd>k__BackingField;
      this.BillingInterval = original.<BillingInterval>k__BackingField;
    }

    // Token: 0x06001F07 RID: 7943 RVA: 0x003F0AF0 File Offset: 0x003F0AF0
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string Plan, [Nullable(1)] out string Tier, out string Status, out string ProductId, out bool? CancelsAtPeriodEnd, out string BillingInterval)
    {
      Plan = this.Plan;
      Tier = this.Tier;
      Status = this.Status;
      ProductId = this.ProductId;
      CancelsAtPeriodEnd = this.CancelsAtPeriodEnd;
      BillingInterval = this.BillingInterval;
    }
  }
}
