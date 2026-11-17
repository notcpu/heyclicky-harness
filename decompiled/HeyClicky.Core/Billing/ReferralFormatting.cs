using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Billing
{
  // Token: 0x02000221 RID: 545
  [NullableContext(1)]
  [Nullable(0)]
  public static class ReferralFormatting
  {
    // Token: 0x06001FDC RID: 8156 RVA: 0x003F34FC File Offset: 0x003F34FC
    public static string Dollars(int usdCents)
    {
      return ((double)usdCents / 100.0).ToString("C2", ReferralFormatting.En);
    }

    // Token: 0x06001FDD RID: 8157 RVA: 0x003F3528 File Offset: 0x003F3528
    public static string WholeDollars(int usdCents)
    {
      return Math.Round((double)usdCents / 100.0, MidpointRounding.AwayFromZero).ToString("C0", ReferralFormatting.En);
    }

    // Token: 0x06001FDE RID: 8158 RVA: 0x003F355C File Offset: 0x003F355C
    public static string CompactDollars(int usdCents)
    {
      if (usdCents % 100 != 0)
      {
        return ReferralFormatting.Dollars(usdCents);
      }
      return ReferralFormatting.WholeDollars(usdCents);
    }

    // Token: 0x06001FDF RID: 8159 RVA: 0x003F3574 File Offset: 0x003F3574
    public static string Percent(double value)
    {
      if (value != Math.Round(value))
      {
        return value.ToString("0.0", ReferralFormatting.En) + "%";
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
      defaultInterpolatedStringHandler.AppendFormatted<int>((int)value);
      defaultInterpolatedStringHandler.AppendLiteral("%");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06001FE0 RID: 8160 RVA: 0x003F35CC File Offset: 0x003F35CC
    public static string Months(int count)
    {
      if (count != 1)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(count);
        defaultInterpolatedStringHandler.AppendLiteral(" months");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
      return "1 month";
    }

    // Token: 0x06001FE1 RID: 8161 RVA: 0x003F3608 File Offset: 0x003F3608
    public static string Referrals(int count, string planName)
    {
      if (count != 1)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 2);
        defaultInterpolatedStringHandler.AppendFormatted<int>(count);
        defaultInterpolatedStringHandler.AppendLiteral(" ");
        defaultInterpolatedStringHandler.AppendFormatted(planName);
        defaultInterpolatedStringHandler.AppendLiteral(" referrals");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
      return "1 " + planName + " referral";
    }

    // Token: 0x06001FE2 RID: 8162 RVA: 0x003F3664 File Offset: 0x003F3664
    public static string People(int count)
    {
      if (count != 1)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(count);
        defaultInterpolatedStringHandler.AppendLiteral(" people");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
      return "1 person";
    }

    // Token: 0x06001FE3 RID: 8163 RVA: 0x003F36A0 File Offset: 0x003F36A0
    public static string ShortMonthLabel(string month)
    {
      if (month.Length >= 2)
      {
        int length = month.Length;
        int num = length - 2;
        int num2;
        if (int.TryParse(month.Substring(num, length - num), out num2) && num2 >= 1 && num2 <= 12)
        {
          return ReferralFormatting.En.DateTimeFormat.AbbreviatedMonthNames[num2 - 1];
        }
      }
      return month;
    }

    // Token: 0x06001FE4 RID: 8164 RVA: 0x003F36F4 File Offset: 0x003F36F4
    public static string FirstYearExplanation(ReferralEarningsMixEstimate mix, int monthsRetained, int commissionMonths)
    {
      string text = ReferralFormatting.WholeDollars(mix.FirstYearUsdCents);
      if (Math.Min(monthsRetained, commissionMonths) <= 1)
      {
        return "That's about " + text + " in your first year.";
      }
      return "That's about " + text + " in your first year, and the people who join late keep paying into the next.";
    }

    // Token: 0x06001FE5 RID: 8165 RVA: 0x003F3738 File Offset: 0x003F3738
    [NullableContext(2)]
    public static string NegativeBalanceExplanation(int availableUsdCents, int reversedPaymentCount, int reversedUsdCents)
    {
      if (availableUsdCents >= 0)
      {
        return null;
      }
      string text = ReferralFormatting.Dollars(-availableUsdCents);
      string text2 = "You don't owe anything. Your next earnings just cover the " + text + " first, then payouts carry on like normal (・ω・)";
      if (reversedPaymentCount <= 0)
      {
        return "You got paid a bit more than has cleared so far. " + text2;
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      string text3;
      if (reversedPaymentCount != 1)
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(reversedPaymentCount);
        defaultInterpolatedStringHandler.AppendLiteral(" of your friends' payments got");
        text3 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text3 = "One of your friends' payments got";
      }
      string text4 = text3;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(123, 3);
      defaultInterpolatedStringHandler.AppendFormatted(text4);
      defaultInterpolatedStringHandler.AppendLiteral(" refunded or pulled back by their bank, so ");
      defaultInterpolatedStringHandler.AppendFormatted(ReferralFormatting.Dollars(reversedUsdCents));
      defaultInterpolatedStringHandler.AppendLiteral(" of what you earned came back out, and some of it had already been paid to you. ");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x04000D8C RID: 3468
    private static readonly CultureInfo En = CultureInfo.GetCultureInfo("en-US");
  }
}
