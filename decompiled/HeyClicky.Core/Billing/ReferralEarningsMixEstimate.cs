using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Billing
{
  // Token: 0x0200021F RID: 543
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ReferralEarningsMixEstimate : IEquatable<ReferralEarningsMixEstimate>
  {
    // Token: 0x06001FC5 RID: 8133 RVA: 0x003F30A0 File Offset: 0x003F30A0
    public ReferralEarningsMixEstimate(IReadOnlyList<ReferralEarningsEstimate> PlanEstimates, int MonthlyUsdCents, int FirstYearUsdCents, IReadOnlyList<int> FirstYearMonthlyUsdCents)
    {
      this.PlanEstimates = PlanEstimates;
      this.MonthlyUsdCents = MonthlyUsdCents;
      this.FirstYearUsdCents = FirstYearUsdCents;
      this.FirstYearMonthlyUsdCents = FirstYearMonthlyUsdCents;
      base..ctor();
    }

    // Token: 0x17000724 RID: 1828
    // (get) Token: 0x06001FC6 RID: 8134 RVA: 0x003F30C8 File Offset: 0x003F30C8
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ReferralEarningsMixEstimate);
      }
    }

    // Token: 0x17000725 RID: 1829
    // (get) Token: 0x06001FC7 RID: 8135 RVA: 0x003F30D4 File Offset: 0x003F30D4
    // (set) Token: 0x06001FC8 RID: 8136 RVA: 0x003F30DC File Offset: 0x003F30DC
    public IReadOnlyList<ReferralEarningsEstimate> PlanEstimates { get; set; }

    // Token: 0x17000726 RID: 1830
    // (get) Token: 0x06001FC9 RID: 8137 RVA: 0x003F30E8 File Offset: 0x003F30E8
    // (set) Token: 0x06001FCA RID: 8138 RVA: 0x003F30F0 File Offset: 0x003F30F0
    public int MonthlyUsdCents { get; set; }

    // Token: 0x17000727 RID: 1831
    // (get) Token: 0x06001FCB RID: 8139 RVA: 0x003F30FC File Offset: 0x003F30FC
    // (set) Token: 0x06001FCC RID: 8140 RVA: 0x003F3104 File Offset: 0x003F3104
    public int FirstYearUsdCents { get; set; }

    // Token: 0x17000728 RID: 1832
    // (get) Token: 0x06001FCD RID: 8141 RVA: 0x003F3110 File Offset: 0x003F3110
    // (set) Token: 0x06001FCE RID: 8142 RVA: 0x003F3118 File Offset: 0x003F3118
    public IReadOnlyList<int> FirstYearMonthlyUsdCents { get; set; }

    // Token: 0x06001FCF RID: 8143 RVA: 0x003F3124 File Offset: 0x003F3124
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ReferralEarningsMixEstimate");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001FD0 RID: 8144 RVA: 0x003F3170 File Offset: 0x003F3170
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("PlanEstimates = ");
      builder.Append(this.PlanEstimates);
      builder.Append(", MonthlyUsdCents = ");
      builder.Append(this.MonthlyUsdCents.ToString());
      builder.Append(", FirstYearUsdCents = ");
      builder.Append(this.FirstYearUsdCents.ToString());
      builder.Append(", FirstYearMonthlyUsdCents = ");
      builder.Append(this.FirstYearMonthlyUsdCents);
      return true;
    }

    // Token: 0x06001FD1 RID: 8145 RVA: 0x003F3204 File Offset: 0x003F3204
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ReferralEarningsMixEstimate left, ReferralEarningsMixEstimate right)
    {
      return !(left == right);
    }

    // Token: 0x06001FD2 RID: 8146 RVA: 0x003F3210 File Offset: 0x003F3210
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ReferralEarningsMixEstimate left, ReferralEarningsMixEstimate right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001FD3 RID: 8147 RVA: 0x003F3224 File Offset: 0x003F3224
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<ReferralEarningsEstimate>>.Default.GetHashCode(this.<PlanEstimates>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MonthlyUsdCents>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<FirstYearUsdCents>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<int>>.Default.GetHashCode(this.<FirstYearMonthlyUsdCents>k__BackingField);
    }

    // Token: 0x06001FD4 RID: 8148 RVA: 0x003F32A0 File Offset: 0x003F32A0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ReferralEarningsMixEstimate);
    }

    // Token: 0x06001FD5 RID: 8149 RVA: 0x003F32B0 File Offset: 0x003F32B0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ReferralEarningsMixEstimate other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<ReferralEarningsEstimate>>.Default.Equals(this.<PlanEstimates>k__BackingField, other.<PlanEstimates>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MonthlyUsdCents>k__BackingField, other.<MonthlyUsdCents>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<FirstYearUsdCents>k__BackingField, other.<FirstYearUsdCents>k__BackingField) && EqualityComparer<IReadOnlyList<int>>.Default.Equals(this.<FirstYearMonthlyUsdCents>k__BackingField, other.<FirstYearMonthlyUsdCents>k__BackingField));
    }

    // Token: 0x06001FD7 RID: 8151 RVA: 0x003F3344 File Offset: 0x003F3344
    [CompilerGenerated]
    private ReferralEarningsMixEstimate(ReferralEarningsMixEstimate original)
    {
      this.PlanEstimates = original.<PlanEstimates>k__BackingField;
      this.MonthlyUsdCents = original.<MonthlyUsdCents>k__BackingField;
      this.FirstYearUsdCents = original.<FirstYearUsdCents>k__BackingField;
      this.FirstYearMonthlyUsdCents = original.<FirstYearMonthlyUsdCents>k__BackingField;
    }

    // Token: 0x06001FD8 RID: 8152 RVA: 0x003F337C File Offset: 0x003F337C
    [CompilerGenerated]
    public void Deconstruct(out IReadOnlyList<ReferralEarningsEstimate> PlanEstimates, out int MonthlyUsdCents, out int FirstYearUsdCents, out IReadOnlyList<int> FirstYearMonthlyUsdCents)
    {
      PlanEstimates = this.PlanEstimates;
      MonthlyUsdCents = this.MonthlyUsdCents;
      FirstYearUsdCents = this.FirstYearUsdCents;
      FirstYearMonthlyUsdCents = this.FirstYearMonthlyUsdCents;
    }
  }
}
