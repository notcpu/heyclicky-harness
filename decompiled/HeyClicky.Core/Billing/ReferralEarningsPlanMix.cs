using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Billing
{
  // Token: 0x0200021D RID: 541
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ReferralEarningsPlanMix : IEquatable<ReferralEarningsPlanMix>
  {
    // Token: 0x06001FA1 RID: 8097 RVA: 0x003F2B8C File Offset: 0x003F2B8C
    public ReferralEarningsPlanMix(int MonthlyPriceUsdCents, int ReferralsPerMonth)
    {
      this.MonthlyPriceUsdCents = MonthlyPriceUsdCents;
      this.ReferralsPerMonth = ReferralsPerMonth;
      base..ctor();
    }

    // Token: 0x1700071C RID: 1820
    // (get) Token: 0x06001FA2 RID: 8098 RVA: 0x003F2BA4 File Offset: 0x003F2BA4
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ReferralEarningsPlanMix);
      }
    }

    // Token: 0x1700071D RID: 1821
    // (get) Token: 0x06001FA3 RID: 8099 RVA: 0x003F2BB0 File Offset: 0x003F2BB0
    // (set) Token: 0x06001FA4 RID: 8100 RVA: 0x003F2BB8 File Offset: 0x003F2BB8
    public int MonthlyPriceUsdCents { get; set; }

    // Token: 0x1700071E RID: 1822
    // (get) Token: 0x06001FA5 RID: 8101 RVA: 0x003F2BC4 File Offset: 0x003F2BC4
    // (set) Token: 0x06001FA6 RID: 8102 RVA: 0x003F2BCC File Offset: 0x003F2BCC
    public int ReferralsPerMonth { get; set; }

    // Token: 0x06001FA7 RID: 8103 RVA: 0x003F2BD8 File Offset: 0x003F2BD8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ReferralEarningsPlanMix");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001FA8 RID: 8104 RVA: 0x003F2C24 File Offset: 0x003F2C24
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("MonthlyPriceUsdCents = ");
      builder.Append(this.MonthlyPriceUsdCents.ToString());
      builder.Append(", ReferralsPerMonth = ");
      builder.Append(this.ReferralsPerMonth.ToString());
      return true;
    }

    // Token: 0x06001FA9 RID: 8105 RVA: 0x003F2C88 File Offset: 0x003F2C88
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ReferralEarningsPlanMix left, ReferralEarningsPlanMix right)
    {
      return !(left == right);
    }

    // Token: 0x06001FAA RID: 8106 RVA: 0x003F2C94 File Offset: 0x003F2C94
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ReferralEarningsPlanMix left, ReferralEarningsPlanMix right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001FAB RID: 8107 RVA: 0x003F2CA8 File Offset: 0x003F2CA8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MonthlyPriceUsdCents>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ReferralsPerMonth>k__BackingField);
    }

    // Token: 0x06001FAC RID: 8108 RVA: 0x003F2CE8 File Offset: 0x003F2CE8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ReferralEarningsPlanMix);
    }

    // Token: 0x06001FAD RID: 8109 RVA: 0x003F2CF8 File Offset: 0x003F2CF8
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ReferralEarningsPlanMix other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<MonthlyPriceUsdCents>k__BackingField, other.<MonthlyPriceUsdCents>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ReferralsPerMonth>k__BackingField, other.<ReferralsPerMonth>k__BackingField));
    }

    // Token: 0x06001FAF RID: 8111 RVA: 0x003F2D5C File Offset: 0x003F2D5C
    [CompilerGenerated]
    private ReferralEarningsPlanMix(ReferralEarningsPlanMix original)
    {
      this.MonthlyPriceUsdCents = original.<MonthlyPriceUsdCents>k__BackingField;
      this.ReferralsPerMonth = original.<ReferralsPerMonth>k__BackingField;
    }

    // Token: 0x06001FB0 RID: 8112 RVA: 0x003F2D7C File Offset: 0x003F2D7C
    [CompilerGenerated]
    public void Deconstruct(out int MonthlyPriceUsdCents, out int ReferralsPerMonth)
    {
      MonthlyPriceUsdCents = this.MonthlyPriceUsdCents;
      ReferralsPerMonth = this.ReferralsPerMonth;
    }
  }
}
