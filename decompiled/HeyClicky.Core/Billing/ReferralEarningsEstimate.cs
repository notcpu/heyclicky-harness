using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Billing
{
  // Token: 0x0200021E RID: 542
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ReferralEarningsEstimate : IEquatable<ReferralEarningsEstimate>
  {
    // Token: 0x06001FB1 RID: 8113 RVA: 0x003F2D90 File Offset: 0x003F2D90
    public ReferralEarningsEstimate(int PerReferralUsdCents, int MonthlyUsdCents, int FirstYearUsdCents, IReadOnlyList<int> FirstYearMonthlyUsdCents)
    {
      this.PerReferralUsdCents = PerReferralUsdCents;
      this.MonthlyUsdCents = MonthlyUsdCents;
      this.FirstYearUsdCents = FirstYearUsdCents;
      this.FirstYearMonthlyUsdCents = FirstYearMonthlyUsdCents;
      base..ctor();
    }

    // Token: 0x1700071F RID: 1823
    // (get) Token: 0x06001FB2 RID: 8114 RVA: 0x003F2DB8 File Offset: 0x003F2DB8
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ReferralEarningsEstimate);
      }
    }

    // Token: 0x17000720 RID: 1824
    // (get) Token: 0x06001FB3 RID: 8115 RVA: 0x003F2DC4 File Offset: 0x003F2DC4
    // (set) Token: 0x06001FB4 RID: 8116 RVA: 0x003F2DCC File Offset: 0x003F2DCC
    public int PerReferralUsdCents { get; set; }

    // Token: 0x17000721 RID: 1825
    // (get) Token: 0x06001FB5 RID: 8117 RVA: 0x003F2DD8 File Offset: 0x003F2DD8
    // (set) Token: 0x06001FB6 RID: 8118 RVA: 0x003F2DE0 File Offset: 0x003F2DE0
    public int MonthlyUsdCents { get; set; }

    // Token: 0x17000722 RID: 1826
    // (get) Token: 0x06001FB7 RID: 8119 RVA: 0x003F2DEC File Offset: 0x003F2DEC
    // (set) Token: 0x06001FB8 RID: 8120 RVA: 0x003F2DF4 File Offset: 0x003F2DF4
    public int FirstYearUsdCents { get; set; }

    // Token: 0x17000723 RID: 1827
    // (get) Token: 0x06001FB9 RID: 8121 RVA: 0x003F2E00 File Offset: 0x003F2E00
    // (set) Token: 0x06001FBA RID: 8122 RVA: 0x003F2E08 File Offset: 0x003F2E08
    public IReadOnlyList<int> FirstYearMonthlyUsdCents { get; set; }

    // Token: 0x06001FBB RID: 8123 RVA: 0x003F2E14 File Offset: 0x003F2E14
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ReferralEarningsEstimate");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001FBC RID: 8124 RVA: 0x003F2E60 File Offset: 0x003F2E60
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("PerReferralUsdCents = ");
      builder.Append(this.PerReferralUsdCents.ToString());
      builder.Append(", MonthlyUsdCents = ");
      builder.Append(this.MonthlyUsdCents.ToString());
      builder.Append(", FirstYearUsdCents = ");
      builder.Append(this.FirstYearUsdCents.ToString());
      builder.Append(", FirstYearMonthlyUsdCents = ");
      builder.Append(this.FirstYearMonthlyUsdCents);
      return true;
    }

    // Token: 0x06001FBD RID: 8125 RVA: 0x003F2F04 File Offset: 0x003F2F04
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ReferralEarningsEstimate left, ReferralEarningsEstimate right)
    {
      return !(left == right);
    }

    // Token: 0x06001FBE RID: 8126 RVA: 0x003F2F10 File Offset: 0x003F2F10
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ReferralEarningsEstimate left, ReferralEarningsEstimate right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001FBF RID: 8127 RVA: 0x003F2F24 File Offset: 0x003F2F24
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<PerReferralUsdCents>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MonthlyUsdCents>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<FirstYearUsdCents>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<int>>.Default.GetHashCode(this.<FirstYearMonthlyUsdCents>k__BackingField);
    }

    // Token: 0x06001FC0 RID: 8128 RVA: 0x003F2FA0 File Offset: 0x003F2FA0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ReferralEarningsEstimate);
    }

    // Token: 0x06001FC1 RID: 8129 RVA: 0x003F2FB0 File Offset: 0x003F2FB0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ReferralEarningsEstimate other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<PerReferralUsdCents>k__BackingField, other.<PerReferralUsdCents>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MonthlyUsdCents>k__BackingField, other.<MonthlyUsdCents>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<FirstYearUsdCents>k__BackingField, other.<FirstYearUsdCents>k__BackingField) && EqualityComparer<IReadOnlyList<int>>.Default.Equals(this.<FirstYearMonthlyUsdCents>k__BackingField, other.<FirstYearMonthlyUsdCents>k__BackingField));
    }

    // Token: 0x06001FC3 RID: 8131 RVA: 0x003F3044 File Offset: 0x003F3044
    [CompilerGenerated]
    private ReferralEarningsEstimate(ReferralEarningsEstimate original)
    {
      this.PerReferralUsdCents = original.<PerReferralUsdCents>k__BackingField;
      this.MonthlyUsdCents = original.<MonthlyUsdCents>k__BackingField;
      this.FirstYearUsdCents = original.<FirstYearUsdCents>k__BackingField;
      this.FirstYearMonthlyUsdCents = original.<FirstYearMonthlyUsdCents>k__BackingField;
    }

    // Token: 0x06001FC4 RID: 8132 RVA: 0x003F307C File Offset: 0x003F307C
    [CompilerGenerated]
    public void Deconstruct(out int PerReferralUsdCents, out int MonthlyUsdCents, out int FirstYearUsdCents, out IReadOnlyList<int> FirstYearMonthlyUsdCents)
    {
      PerReferralUsdCents = this.PerReferralUsdCents;
      MonthlyUsdCents = this.MonthlyUsdCents;
      FirstYearUsdCents = this.FirstYearUsdCents;
      FirstYearMonthlyUsdCents = this.FirstYearMonthlyUsdCents;
    }
  }
}
