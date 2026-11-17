using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Billing
{
  // Token: 0x02000220 RID: 544
  [NullableContext(1)]
  [Nullable(0)]
  public static class ReferralEarningsEstimator
  {
    // Token: 0x06001FD9 RID: 8153 RVA: 0x003F33A0 File Offset: 0x003F33A0
    public static ReferralEarningsEstimate Estimate(int referralsPerMonth, int monthsRetained, int monthlyPriceUsdCents, double commissionPercent, int commissionMonths)
    {
      int num = Math.Max(0, Math.Min(monthsRetained, commissionMonths));
      int num2 = ReferralEarningsEstimator.CommissionUsdCents(monthlyPriceUsdCents, commissionPercent);
      int num3 = num * num2;
      int[] array = new int[12];
      for (int i = 1; i <= 12; i++)
      {
        int num4 = 0;
        for (int j = 1; j <= i; j++)
        {
          if (i - j + 1 <= num)
          {
            num4 += referralsPerMonth * num2;
          }
        }
        array[i - 1] = num4;
      }
      return new ReferralEarningsEstimate(num3, referralsPerMonth * num3, array.Sum(), array);
    }

    // Token: 0x06001FDA RID: 8154 RVA: 0x003F3420 File Offset: 0x003F3420
    public static ReferralEarningsMixEstimate Estimate(IReadOnlyList<ReferralEarningsPlanMix> mixes, int monthsRetained, double commissionPercent, int commissionMonths)
    {
      List<ReferralEarningsEstimate> plans = mixes.Select<ReferralEarningsPlanMix, ReferralEarningsEstimate>((ReferralEarningsPlanMix m) => ReferralEarningsEstimator.Estimate(m.ReferralsPerMonth, monthsRetained, m.MonthlyPriceUsdCents, commissionPercent, commissionMonths)).ToList<ReferralEarningsEstimate>();
      List<int> list = (from i in Enumerable.Range(0, 12)
        select plans.Sum<ReferralEarningsEstimate>((ReferralEarningsEstimate p) => p.FirstYearMonthlyUsdCents[i])).ToList<int>();
      return new ReferralEarningsMixEstimate(plans, plans.Sum<ReferralEarningsEstimate>((ReferralEarningsEstimate p) => p.MonthlyUsdCents), plans.Sum<ReferralEarningsEstimate>((ReferralEarningsEstimate p) => p.FirstYearUsdCents), list);
    }

    // Token: 0x06001FDB RID: 8155 RVA: 0x003F34E4 File Offset: 0x003F34E4
    private static int CommissionUsdCents(int monthlyPriceUsdCents, double commissionPercent)
    {
      return (int)Math.Floor((double)monthlyPriceUsdCents * commissionPercent / 100.0);
    }
  }
}
