using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Billing
{
  // Token: 0x0200021C RID: 540
  [NullableContext(1)]
  [Nullable(0)]
  public static class PaywallPayloads
  {
    // Token: 0x06001F9D RID: 8093 RVA: 0x003F28C8 File Offset: 0x003F28C8
    private static PaywallPlanOption Pro(int cap, bool recommended, bool yearly)
    {
      string text = "pro";
      string text2 = "HeyClicky Pro";
      string text3 = "$20/mo";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 1);
      defaultInterpolatedStringHandler.AppendFormatted<int>(cap);
      defaultInterpolatedStringHandler.AppendLiteral(" agent messages per month");
      return new PaywallPlanOption(text, text2, text3, cap, defaultInterpolatedStringHandler.ToStringAndClear(), "Get HeyClicky Pro", recommended, yearly ? "$16/mo" : null, yearly ? "billed annually ($192)" : null);
    }

    // Token: 0x06001F9E RID: 8094 RVA: 0x003F292C File Offset: 0x003F292C
    private static PaywallPlanOption Max(int cap, bool recommended, bool yearly)
    {
      string text = "max";
      string text2 = "HeyClicky Max";
      string text3 = "$100/mo";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 1);
      defaultInterpolatedStringHandler.AppendFormatted<int>(cap);
      defaultInterpolatedStringHandler.AppendLiteral(" agent messages per month");
      return new PaywallPlanOption(text, text2, text3, cap, defaultInterpolatedStringHandler.ToStringAndClear(), "Get HeyClicky Max", recommended, yearly ? "$80/mo" : null, yearly ? "billed annually ($960)" : null);
    }

    // Token: 0x06001F9F RID: 8095 RVA: 0x003F2990 File Offset: 0x003F2990
    [return: Nullable(2)]
    public static PaywallPayload ManualUpgrade(string currentTier, int? proAgentsCap, int? maxAgentsCap, bool proYearlyAvailable, bool maxYearlyAvailable)
    {
      int valueOrDefault = proAgentsCap.GetValueOrDefault(150);
      int valueOrDefault2 = maxAgentsCap.GetValueOrDefault(1000);
      PaywallPlanOption paywallPlanOption = PaywallPayloads.Pro(valueOrDefault, currentTier == "free", proYearlyAvailable);
      PaywallPlanOption paywallPlanOption2 = PaywallPayloads.Max(valueOrDefault2, currentTier == "pro", maxYearlyAvailable);
      IReadOnlyList<PaywallPlanOption> readOnlyList;
      if (!(currentTier == "free"))
      {
        if (!(currentTier == "pro"))
        {
          return null;
        }
        readOnlyList = new <>z__ReadOnlySingleElementList<PaywallPlanOption>(paywallPlanOption2);
      }
      else
      {
        readOnlyList = new <>z__ReadOnlyArray<PaywallPlanOption>(new PaywallPlanOption[] { paywallPlanOption, paywallPlanOption2 });
      }
      bool flag = readOnlyList.Any<PaywallPlanOption>((PaywallPlanOption p) => p.SupportsYearly);
      int num = ((currentTier == "free") ? valueOrDefault : valueOrDefault2);
      string text = ((currentTier == "free") ? "Upgrade HeyClicky to keep going." : "Upgrade to Max");
      string text2 = "quota_exceeded";
      string text3 = "agents";
      string text4 = text;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 1);
      defaultInterpolatedStringHandler.AppendFormatted<int>(num);
      defaultInterpolatedStringHandler.AppendLiteral(" HeyClicky Agent messages per month");
      return new PaywallPayload(text2, text3, text4, defaultInterpolatedStringHandler.ToStringAndClear(), "", "", 0, 0, null, readOnlyList, flag ? "Save 20%" : null, null);
    }

    // Token: 0x06001FA0 RID: 8096 RVA: 0x003F2AC8 File Offset: 0x003F2AC8
    public static PaywallPayload LocalMessagesLimit(int currentUsage, int limit, [Nullable(2)] string resetAt, int? proAgentsCap, int? maxAgentsCap, bool proYearlyAvailable, bool maxYearlyAvailable)
    {
      int valueOrDefault = proAgentsCap.GetValueOrDefault(150);
      int valueOrDefault2 = maxAgentsCap.GetValueOrDefault(1000);
      IReadOnlyList<PaywallPlanOption> readOnlyList = new <>z__ReadOnlyArray<PaywallPlanOption>(new PaywallPlanOption[]
      {
        PaywallPayloads.Pro(valueOrDefault, true, proYearlyAvailable),
        PaywallPayloads.Max(valueOrDefault2, false, maxYearlyAvailable)
      });
      string text = "quota_exceeded";
      string text2 = "messages";
      string text3 = "You're out of messages.";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 1);
      defaultInterpolatedStringHandler.AppendFormatted<int>(valueOrDefault);
      defaultInterpolatedStringHandler.AppendLiteral(" HeyClicky Agent messages per month");
      return new PaywallPayload(text, text2, text3, defaultInterpolatedStringHandler.ToStringAndClear(), "Get HeyClicky Pro for $20/mo", "$20/mo", currentUsage, limit, resetAt, readOnlyList, readOnlyList.Any<PaywallPlanOption>((PaywallPlanOption p) => p.SupportsYearly) ? "Save 20%" : null, null);
    }

    // Token: 0x04000D80 RID: 3456
    public const string QuotaExceeded = "quota_exceeded";

    // Token: 0x04000D81 RID: 3457
    public const string PaymentProblem = "payment_problem";
  }
}
