using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Auth;
using HeyClicky.Core.Configuration;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core.Billing
{
  // Token: 0x02000210 RID: 528
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class BillingStateService : IDisposable
  {
    // Token: 0x1400003E RID: 62
    // (add) Token: 0x06001EB8 RID: 7864 RVA: 0x003EF298 File Offset: 0x003EF298
    // (remove) Token: 0x06001EB9 RID: 7865 RVA: 0x003EF2D0 File Offset: 0x003EF2D0
    public event EventHandler Changed;

    // Token: 0x1400003F RID: 63
    // (add) Token: 0x06001EBA RID: 7866 RVA: 0x003EF308 File Offset: 0x003EF308
    // (remove) Token: 0x06001EBB RID: 7867 RVA: 0x003EF340 File Offset: 0x003EF340
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<string> PaidPlanStarted;

    // Token: 0x06001EBC RID: 7868 RVA: 0x003EF378 File Offset: 0x003EF378
    [NullableContext(1)]
    public BillingStateService(WorkerClient worker, SupabaseAuthService auth, [Nullable(2)] string cacheDirectory = null)
    {
      this.worker = worker;
      this.auth = auth;
      this.cacheDirectory = cacheDirectory ?? Path.Combine(AppBundleConfiguration.AppDataDirectory, "billing");
      this.currentUserId = auth.UserId;
      this.plan = this.RestoreLastKnown(this.currentUserId);
      auth.Changed += this.OnAuthChanged;
    }

    // Token: 0x06001EBD RID: 7869 RVA: 0x003EF3FC File Offset: 0x003EF3FC
    public void Dispose()
    {
      this.auth.Changed -= this.OnAuthChanged;
      object obj = this.gate;
      lock (obj)
      {
        this.EndBrowserWatchLocked();
      }
    }

    // Token: 0x170006CF RID: 1743
    // (get) Token: 0x06001EBE RID: 7870 RVA: 0x003EF454 File Offset: 0x003EF454
    public PlanSnapshot Plan
    {
      get
      {
        object obj = this.gate;
        PlanSnapshot planSnapshot;
        lock (obj)
        {
          planSnapshot = this.plan;
        }
        return planSnapshot;
      }
    }

    // Token: 0x170006D0 RID: 1744
    // (get) Token: 0x06001EBF RID: 7871 RVA: 0x003EF498 File Offset: 0x003EF498
    public bool IsFetching
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.isFetching;
        }
        return flag2;
      }
    }

    // Token: 0x170006D1 RID: 1745
    // (get) Token: 0x06001EC0 RID: 7872 RVA: 0x003EF4DC File Offset: 0x003EF4DC
    public BillingFetchError LastFetchError
    {
      get
      {
        object obj = this.gate;
        BillingFetchError billingFetchError;
        lock (obj)
        {
          billingFetchError = this.lastFetchError;
        }
        return billingFetchError;
      }
    }

    // Token: 0x170006D2 RID: 1746
    // (get) Token: 0x06001EC1 RID: 7873 RVA: 0x003EF520 File Offset: 0x003EF520
    public DateTimeOffset? LastPlanAnswerAt
    {
      get
      {
        object obj = this.gate;
        DateTimeOffset? dateTimeOffset;
        lock (obj)
        {
          dateTimeOffset = this.lastPlanAnswerAt;
        }
        return dateTimeOffset;
      }
    }

    // Token: 0x170006D3 RID: 1747
    // (get) Token: 0x06001EC2 RID: 7874 RVA: 0x003EF564 File Offset: 0x003EF564
    public string AuthenticatedUserId
    {
      get
      {
        if (!this.auth.IsSignedIn)
        {
          return null;
        }
        return this.auth.UserId;
      }
    }

    // Token: 0x170006D4 RID: 1748
    // (get) Token: 0x06001EC3 RID: 7875 RVA: 0x003EF580 File Offset: 0x003EF580
    public ReferralPaywallDiscount ReferralDiscount
    {
      get
      {
        object obj = this.gate;
        ReferralPaywallDiscount referralPaywallDiscount;
        lock (obj)
        {
          referralPaywallDiscount = this.referralDiscount;
        }
        return referralPaywallDiscount;
      }
    }

    // Token: 0x170006D5 RID: 1749
    // (get) Token: 0x06001EC4 RID: 7876 RVA: 0x003EF5C4 File Offset: 0x003EF5C4
    public bool IsUnlimited
    {
      get
      {
        PlanSnapshot planSnapshot = this.Plan;
        return ((planSnapshot != null) ? planSnapshot.Plan : null) == "unlimited";
      }
    }

    // Token: 0x170006D6 RID: 1750
    // (get) Token: 0x06001EC5 RID: 7877 RVA: 0x003EF5E4 File Offset: 0x003EF5E4
    [Nullable(1)]
    public string CurrentTier
    {
      [NullableContext(1)]
      get
      {
        PlanSnapshot planSnapshot = this.Plan;
        return ((planSnapshot != null) ? planSnapshot.EffectiveTier : null) ?? "free";
      }
    }

    // Token: 0x170006D7 RID: 1751
    // (get) Token: 0x06001EC6 RID: 7878 RVA: 0x003EF604 File Offset: 0x003EF604
    public bool IsMaxTier
    {
      get
      {
        return this.CurrentTier == "max";
      }
    }

    // Token: 0x170006D8 RID: 1752
    // (get) Token: 0x06001EC7 RID: 7879 RVA: 0x003EF618 File Offset: 0x003EF618
    public string PaidPlanName
    {
      get
      {
        if (!this.IsUnlimited)
        {
          return null;
        }
        if (!this.IsMaxTier)
        {
          return "HeyClicky Pro";
        }
        return "HeyClicky Max";
      }
    }

    // Token: 0x170006D9 RID: 1753
    // (get) Token: 0x06001EC8 RID: 7880 RVA: 0x003EF638 File Offset: 0x003EF638
    [Nullable(1)]
    public string ReportPlanLabel
    {
      [NullableContext(1)]
      get
      {
        if (this.Plan == null)
        {
          return "unknown";
        }
        if (!this.IsUnlimited)
        {
          return "free";
        }
        if (!this.IsMaxTier)
        {
          return "pro";
        }
        return "max";
      }
    }

    // Token: 0x170006DA RID: 1754
    // (get) Token: 0x06001EC9 RID: 7881 RVA: 0x003EF66C File Offset: 0x003EF66C
    public bool IsTeamManagedPlan
    {
      get
      {
        if (this.IsUnlimited)
        {
          PlanSnapshot planSnapshot = this.Plan;
          return ((planSnapshot != null) ? planSnapshot.TierSource : null) == "team";
        }
        return false;
      }
    }

    // Token: 0x170006DB RID: 1755
    // (get) Token: 0x06001ECA RID: 7882 RVA: 0x003EF694 File Offset: 0x003EF694
    public string TeamPlanTeamName
    {
      get
      {
        if (!this.IsTeamManagedPlan)
        {
          return null;
        }
        PlanSnapshot planSnapshot = this.Plan;
        if (planSnapshot == null)
        {
          return null;
        }
        return planSnapshot.TeamName;
      }
    }

    // Token: 0x170006DC RID: 1756
    // (get) Token: 0x06001ECB RID: 7883 RVA: 0x003EF6B4 File Offset: 0x003EF6B4
    public string CurrentBillingInterval
    {
      get
      {
        PlanSnapshot planSnapshot = this.Plan;
        if (planSnapshot == null)
        {
          return null;
        }
        PlanSubscription subscription = planSnapshot.Subscription;
        if (subscription == null)
        {
          return null;
        }
        return subscription.BillingInterval;
      }
    }

    // Token: 0x170006DD RID: 1757
    // (get) Token: 0x06001ECC RID: 7884 RVA: 0x003EF6D4 File Offset: 0x003EF6D4
    public bool RequiresPaymentResolution
    {
      get
      {
        PlanSnapshot planSnapshot = this.Plan;
        PlanSubscription planSubscription = ((planSnapshot != null) ? planSnapshot.Subscription : null);
        return planSubscription != null && BillingStateService.RequiresPayment(planSubscription);
      }
    }

    // Token: 0x170006DE RID: 1758
    // (get) Token: 0x06001ECD RID: 7885 RVA: 0x003EF700 File Offset: 0x003EF700
    public bool IsBillingEnforcementOn
    {
      get
      {
        PlanSnapshot planSnapshot = this.Plan;
        return ((planSnapshot != null) ? planSnapshot.BillingEnforcement : null) == "on";
      }
    }

    // Token: 0x06001ECE RID: 7886 RVA: 0x003EF720 File Offset: 0x003EF720
    [NullableContext(1)]
    public static bool RequiresPayment(PlanSubscription subscription)
    {
      string text = subscription.Status.Trim().ToLowerInvariant();
      return text == "past_due" || text == "unpaid" || text == "payment_due";
    }

    // Token: 0x170006DF RID: 1759
    // (get) Token: 0x06001ECF RID: 7887 RVA: 0x003EF76C File Offset: 0x003EF76C
    public PaywallPayload ManualUpgradePayload
    {
      get
      {
        PlanSnapshot planSnapshot = this.Plan;
        return PaywallPayloads.ManualUpgrade(this.CurrentTier, (planSnapshot != null) ? planSnapshot.ProAgentsCap : null, (planSnapshot != null) ? planSnapshot.MaxAgentsCap : null, ((planSnapshot != null) ? planSnapshot.ProYearlyAvailable : null).GetValueOrDefault(), ((planSnapshot != null) ? planSnapshot.MaxYearlyAvailable : null).GetValueOrDefault());
      }
    }

    // Token: 0x170006E0 RID: 1760
    // (get) Token: 0x06001ED0 RID: 7888 RVA: 0x003EF7EC File Offset: 0x003EF7EC
    public string AnnualBillingSavingsCtaTitle
    {
      get
      {
        PlanSnapshot planSnapshot = this.Plan;
        if (!this.IsUnlimited || this.CurrentBillingInterval != "month")
        {
          return null;
        }
        string currentTier = this.CurrentTier;
        if (!(currentTier == "pro"))
        {
          if (currentTier == "max")
          {
            if (planSnapshot != null && planSnapshot.MaxYearlyAvailable.GetValueOrDefault())
            {
              return "Save $240 on annual billing of HeyClicky Max plan";
            }
          }
        }
        else if (planSnapshot != null && planSnapshot.ProYearlyAvailable.GetValueOrDefault())
        {
          return "Save $48 on annual billing of HeyClicky Pro plan";
        }
        return null;
      }
    }

    // Token: 0x170006E1 RID: 1761
    // (get) Token: 0x06001ED1 RID: 7889 RVA: 0x003EF878 File Offset: 0x003EF878
    public bool ShouldOfferManualUpgrade
    {
      get
      {
        bool isBillingEnforcementOn = this.IsBillingEnforcementOn;
        return this.Plan != null && this.CurrentTier == "free" && !this.IsTeamManagedPlan && isBillingEnforcementOn && this.ManualUpgradePayload != null;
      }
    }

    // Token: 0x170006E2 RID: 1762
    // (get) Token: 0x06001ED2 RID: 7890 RVA: 0x003EF8C4 File Offset: 0x003EF8C4
    public DateTimeOffset? UsageResetsDate
    {
      get
      {
        PlanSnapshot planSnapshot = this.Plan;
        if (planSnapshot == null)
        {
          return null;
        }
        DateTimeOffset? dateTimeOffset = BillingStateService.ParseDate(planSnapshot.Usage.WindowResetsAt);
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
          return new DateTimeOffset?(valueOrDefault);
        }
        if (!(planSnapshot.Plan == "unlimited"))
        {
          return null;
        }
        PlanSubscription subscription = planSnapshot.Subscription;
        return BillingStateService.ParseDate((subscription != null) ? subscription.CurrentPeriodEnd : null);
      }
    }

    // Token: 0x170006E3 RID: 1763
    // (get) Token: 0x06001ED3 RID: 7891 RVA: 0x003EF944 File Offset: 0x003EF944
    public string UsageResetsPhrase
    {
      get
      {
        DateTimeOffset? usageResetsDate = this.UsageResetsDate;
        if (usageResetsDate != null)
        {
          DateTimeOffset valueOrDefault = usageResetsDate.GetValueOrDefault();
          return BillingStateService.RelativePhrase(valueOrDefault, null);
        }
        return null;
      }
    }

    // Token: 0x170006E4 RID: 1764
    // (get) Token: 0x06001ED4 RID: 7892 RVA: 0x003EF97C File Offset: 0x003EF97C
    public string UsageResetsLabel
    {
      get
      {
        string usageResetsPhrase = this.UsageResetsPhrase;
        if (usageResetsPhrase == null)
        {
          return null;
        }
        return "Your current usage resets " + usageResetsPhrase + ".";
      }
    }

    // Token: 0x170006E5 RID: 1765
    // (get) Token: 0x06001ED5 RID: 7893 RVA: 0x003EF9A8 File Offset: 0x003EF9A8
    public string SubscriptionCancellationLabel
    {
      get
      {
        PlanSnapshot planSnapshot = this.Plan;
        if (!(((planSnapshot != null) ? planSnapshot.Plan : null) != "unlimited"))
        {
          PlanSubscription subscription = planSnapshot.Subscription;
          if (subscription != null)
          {
            if (!subscription.CancelAtPeriodEnd && subscription.CancelAt == null)
            {
              return null;
            }
            DateTimeOffset? dateTimeOffset;
            if (!subscription.CancelAtPeriodEnd)
            {
              dateTimeOffset = BillingStateService.ParseDate(subscription.CancelAt);
            }
            else
            {
              DateTimeOffset? dateTimeOffset2 = BillingStateService.ParseDate(subscription.CurrentPeriodEnd);
              dateTimeOffset = ((dateTimeOffset2 != null) ? dateTimeOffset2 : BillingStateService.ParseDate(subscription.CancelAt));
            }
            DateTimeOffset? dateTimeOffset3 = dateTimeOffset;
            if (dateTimeOffset3 != null)
            {
              DateTimeOffset valueOrDefault = dateTimeOffset3.GetValueOrDefault();
              return "Cancels " + BillingStateService.RelativePhrase(valueOrDefault, null) + ".";
            }
            return null;
          }
        }
        return null;
      }
    }

    // Token: 0x06001ED6 RID: 7894 RVA: 0x003EFA64 File Offset: 0x003EFA64
    [NullableContext(1)]
    public static string RelativePhrase(DateTimeOffset target, DateTimeOffset? now = null)
    {
      int num = (int)(target - (now ?? DateTimeOffset.Now)).TotalSeconds;
      if (num <= 0)
      {
        return "soon";
      }
      int num2 = num / 3600;
      int num3 = num2 / 24;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (num3 > 0)
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 2);
        defaultInterpolatedStringHandler.AppendLiteral("in ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num3);
        defaultInterpolatedStringHandler.AppendLiteral("d ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num2 % 24);
        defaultInterpolatedStringHandler.AppendLiteral("h");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
      if (num2 > 0)
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
        defaultInterpolatedStringHandler.AppendLiteral("in ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
        defaultInterpolatedStringHandler.AppendLiteral("h");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
      defaultInterpolatedStringHandler.AppendLiteral("in ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(Math.Max(1, num / 60));
      defaultInterpolatedStringHandler.AppendLiteral("m");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06001ED7 RID: 7895 RVA: 0x003EFB6C File Offset: 0x003EFB6C
    private static DateTimeOffset? ParseDate(string raw)
    {
      DateTimeOffset dateTimeOffset;
      if (raw == null || !DateTimeOffset.TryParse(raw, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTimeOffset))
      {
        return null;
      }
      return new DateTimeOffset?(dateTimeOffset);
    }

    // Token: 0x06001ED8 RID: 7896 RVA: 0x003EFBA0 File Offset: 0x003EFBA0
    public void RegisterLocalMessageTurnConsumed()
    {
      object obj = this.gate;
      lock (obj)
      {
        int? num = this.localMessagesUsed;
        if (num == null)
        {
          return;
        }
        int valueOrDefault = num.GetValueOrDefault();
        this.localMessagesUsed = new int?(valueOrDefault + 1);
      }
      this.RaiseChanged();
    }

    // Token: 0x06001ED9 RID: 7897 RVA: 0x003EFC0C File Offset: 0x003EFC0C
    public void RollbackLocalMessageTurnConsumed()
    {
      object obj = this.gate;
      lock (obj)
      {
        int? num = this.localMessagesUsed;
        if (num == null)
        {
          return;
        }
        int valueOrDefault = num.GetValueOrDefault();
        int num2 = Math.Max(0, valueOrDefault - 1);
        PlanSnapshot planSnapshot = this.plan;
        this.localMessagesUsed = new int?((planSnapshot != null && planSnapshot.Plan == "free") ? Math.Max(num2, planSnapshot.Usage.Messages.Used) : num2);
      }
      this.RaiseChanged();
    }

    // Token: 0x170006E6 RID: 1766
    // (get) Token: 0x06001EDA RID: 7898 RVA: 0x003EFCB8 File Offset: 0x003EFCB8
    public PlanUsageBucket DisplayedMessagesUsage
    {
      get
      {
        object obj = this.gate;
        PlanUsageBucket planUsageBucket;
        lock (obj)
        {
          if (this.plan == null)
          {
            planUsageBucket = null;
          }
          else
          {
            PlanUsageBucket planUsageBucket2;
            if (this.plan.Plan == "free")
            {
              int? num = this.localMessagesUsed;
              if (num != null)
              {
                int valueOrDefault = num.GetValueOrDefault();
                (planUsageBucket2 = this.plan.Usage.Messages.<Clone>$()).Used = valueOrDefault;
                goto IL_007D;
              }
            }
            planUsageBucket2 = this.plan.Usage.Messages;
            IL_007D:
            planUsageBucket = planUsageBucket2;
          }
        }
        return planUsageBucket;
      }
    }

    // Token: 0x06001EDB RID: 7899 RVA: 0x003EFD60 File Offset: 0x003EFD60
    public bool ShouldBlockFreeUserMessageTurnLocally()
    {
      object obj = this.gate;
      PlanSnapshot planSnapshot;
      int? num;
      bool flag2;
      lock (obj)
      {
        planSnapshot = this.plan;
        num = this.localMessagesUsed;
        flag2 = this.gateSuspendedAfterFailOpen;
      }
      if (planSnapshot == null || planSnapshot.IsFailOpenFallback.GetValueOrDefault() || flag2)
      {
        return false;
      }
      if (!(planSnapshot.Plan != "free") && !(planSnapshot.BillingEnforcement != "on") && num != null)
      {
        int valueOrDefault = num.GetValueOrDefault();
        DateTimeOffset? dateTimeOffset = BillingStateService.ParseDate(planSnapshot.Usage.WindowResetsAt);
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault2 = dateTimeOffset.GetValueOrDefault();
          if (valueOrDefault2 <= DateTimeOffset.Now)
          {
            this.RefreshAsync();
            return false;
          }
        }
        return valueOrDefault >= planSnapshot.Usage.Messages.Cap;
      }
      return false;
    }

    // Token: 0x06001EDC RID: 7900 RVA: 0x003EFE5C File Offset: 0x003EFE5C
    public PaywallPayload MakeLocalMessagesLimitPaywallPayload()
    {
      object obj = this.gate;
      PlanSnapshot planSnapshot;
      int? num;
      lock (obj)
      {
        planSnapshot = this.plan;
        num = this.localMessagesUsed;
      }
      if (planSnapshot == null || !(planSnapshot.Plan == "free"))
      {
        return null;
      }
      return PaywallPayloads.LocalMessagesLimit(num ?? planSnapshot.Usage.Messages.Used, planSnapshot.Usage.Messages.Cap, planSnapshot.Usage.WindowResetsAt, planSnapshot.ProAgentsCap, planSnapshot.MaxAgentsCap, planSnapshot.ProYearlyAvailable.GetValueOrDefault(), planSnapshot.MaxYearlyAvailable.GetValueOrDefault());
    }

    // Token: 0x06001EDD RID: 7901 RVA: 0x003EFF2C File Offset: 0x003EFF2C
    [NullableContext(1)]
    public Task RefreshAsync()
    {
      object obj = this.gate;
      Task task2;
      lock (obj)
      {
        this.requestedRefreshGeneration++;
        Task task = this.activeRefresh;
        if (task != null)
        {
          task2 = task;
        }
        else if (!this.auth.IsSignedIn)
        {
          task2 = Task.CompletedTask;
        }
        else
        {
          this.isFetching = true;
          this.activeRefresh = Task.Run(new Func<Task>(this.RefreshLoopAsync));
          task2 = this.activeRefresh;
        }
      }
      return task2;
    }

    // Token: 0x06001EDE RID: 7902 RVA: 0x003EFFC0 File Offset: 0x003EFFC0
    [NullableContext(1)]
    private async Task RefreshLoopAsync()
    {
      this.RaiseChanged();
      try
      {
        for (;;)
        {
          object obj = this.gate;
          int served;
          lock (obj)
          {
            served = this.requestedRefreshGeneration;
          }
          await this.PerformSingleRefreshAsync().ConfigureAwait(false);
          obj = this.gate;
          lock (obj)
          {
            if (this.requestedRefreshGeneration != served)
            {
              continue;
            }
            this.activeRefresh = null;
            this.isFetching = false;
          }
          break;
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Billing", "plan refresh loop: " + ex.Message);
        object obj = this.gate;
        bool flag = false;
        try
        {
          Monitor.Enter(obj, ref flag);
          this.activeRefresh = null;
          this.isFetching = false;
        }
        finally
        {
          int num;
          if (num < 0 && flag)
          {
            Monitor.Exit(obj);
          }
        }
      }
      this.RaiseChanged();
    }

    // Token: 0x06001EDF RID: 7903 RVA: 0x003F0004 File Offset: 0x003F0004
    [NullableContext(1)]
    private async Task PerformSingleRefreshAsync()
    {
      string requestUserId = this.auth.UserId;
      PlanSnapshot planSnapshot;
      bool flag2;
      Exception ex2;
      object obj2;
      bool flag3;
      try
      {
        planSnapshot = await this.worker.GetPlanAsync(default(CancellationToken)).ConfigureAwait(false);
      }
      catch (WorkerHttpException ex)
      {
        bool flag = BillingStateService.IsAuthFailure(ex.Status, ex.Body);
        object obj = this.gate;
        flag2 = false;
        try
        {
          Monitor.Enter(obj, ref flag2);
          this.lastFetchError = new BillingFetchError(flag ? BillingFetchErrorKind.SignedOut : BillingFetchErrorKind.Server, ex.Status, ex.Message);
        }
        finally
        {
          int num;
          if (num < 0 && flag2)
          {
            Monitor.Exit(obj);
          }
        }
        Log.Warn("Billing", "plan refresh failed: " + ex.Message);
        return;
      }
      catch when (delegate
      {
        // Failed to create a 'catch-when' expression
        ex2 = obj2 as Exception;
        if (ex2 == null)
        {
          flag3 = false;
        }
        else
        {
          flag2 = ex2 is HttpRequestException || ex2 is TaskCanceledException || ex2 is IOException || ex2 is JsonException;
          flag3 = flag2 > false;
        }
        endfilter(flag3);
      })
      {
        object obj = this.gate;
        bool flag4 = false;
        try
        {
          Monitor.Enter(obj, ref flag4);
          this.lastFetchError = new BillingFetchError((ex2 is JsonException) ? BillingFetchErrorKind.Decode : BillingFetchErrorKind.Transport, 0, ex2.Message);
        }
        finally
        {
          int num;
          if (num < 0 && flag4)
          {
            Monitor.Exit(obj);
          }
        }
        Log.Warn("Billing", "plan refresh failed: " + ex2.Message);
        return;
      }
      if (planSnapshot == null)
      {
        if (this.auth.IsSignedIn)
        {
          object obj = this.gate;
          lock (obj)
          {
            this.lastFetchError = new BillingFetchError(BillingFetchErrorKind.SignedOut, 401, "no session");
          }
        }
      }
      else if (requestUserId == null || requestUserId != this.auth.UserId)
      {
        Log.Warn("Billing", "discarding a plan fetched for a different account than the one signed in now");
      }
      else
      {
        string text = null;
        ValueTuple<string, string>? valueTuple = null;
        object obj = this.gate;
        lock (obj)
        {
          this.lastPlanAnswerAt = new DateTimeOffset?(DateTimeOffset.UtcNow);
          if (planSnapshot.IsFailOpenFallback.GetValueOrDefault())
          {
            this.lastFetchError = null;
            this.gateSuspendedAfterFailOpen = true;
            PlanSnapshot planSnapshot2 = this.plan;
            if (planSnapshot2 != null && !(planSnapshot2.IsFailOpenFallback ?? false))
            {
              Log.Warn("Billing", "plan refresh returned the fail-open placeholder; keeping the known " + this.plan.Plan + " plan");
            }
            else
            {
              this.plan = null;
              Log.Warn("Billing", "plan refresh returned the fail-open placeholder with no known plan; leaving it unknown");
            }
          }
          else
          {
            PlanSnapshot planSnapshot3 = this.plan;
            string text2 = ((planSnapshot3 != null) ? planSnapshot3.Plan : null);
            PlanSnapshot planSnapshot4 = this.plan;
            string text3 = ((planSnapshot4 != null) ? planSnapshot4.EffectiveTier : null);
            bool flag5 = this.pendingActivationSessionId != null;
            this.plan = planSnapshot;
            this.lastFetchError = null;
            this.gateSuspendedAfterFailOpen = false;
            this.localMessagesUsed = ((planSnapshot.Plan == "free") ? new int?(planSnapshot.Usage.Messages.Used) : null);
            if (planSnapshot.Plan == "unlimited" && (text2 == "free" || (text2 == null && flag5)))
            {
              string text4 = this.pendingActivationSessionId;
              if (text4 != null)
              {
                this.handledActivationSessionIds.Add(text4);
              }
              this.pendingActivationSessionId = null;
              string effectiveTier = planSnapshot.EffectiveTier;
              valueTuple = new ValueTuple<string, string>?(new ValueTuple<string, string>(text4, (effectiveTier == "max") ? "HeyClicky Max" : ((effectiveTier == "pro") ? "HeyClicky Pro" : "Free")));
            }
            string effectiveTier2 = planSnapshot.EffectiveTier;
            bool flag6 = text2 == "free" || (text2 == null && flag5);
            bool flag7 = text3 == "pro" && effectiveTier2 == "max" && flag5;
            if (planSnapshot.Plan == "unlimited" && effectiveTier2 != "free" && planSnapshot.TierSource != "team" && (flag6 || flag7))
            {
              text = effectiveTier2;
            }
          }
        }
        if (!planSnapshot.IsFailOpenFallback.GetValueOrDefault())
        {
          this.PersistLastKnown(planSnapshot, requestUserId);
          string text5 = "Billing";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(53, 7);
          defaultInterpolatedStringHandler.AppendLiteral("plan refreshed: ");
          defaultInterpolatedStringHandler.AppendFormatted(planSnapshot.Plan);
          defaultInterpolatedStringHandler.AppendLiteral("/");
          defaultInterpolatedStringHandler.AppendFormatted(planSnapshot.EffectiveTier);
          defaultInterpolatedStringHandler.AppendLiteral(", messages=");
          defaultInterpolatedStringHandler.AppendFormatted<int>(planSnapshot.Usage.Messages.Used);
          defaultInterpolatedStringHandler.AppendLiteral("/");
          defaultInterpolatedStringHandler.AppendFormatted<int>(planSnapshot.Usage.Messages.Cap);
          defaultInterpolatedStringHandler.AppendLiteral(", ");
          defaultInterpolatedStringHandler.AppendLiteral("agents=");
          defaultInterpolatedStringHandler.AppendFormatted<int>(planSnapshot.Usage.Agents.Used);
          defaultInterpolatedStringHandler.AppendLiteral("/");
          defaultInterpolatedStringHandler.AppendFormatted<int>(planSnapshot.Usage.Agents.Cap);
          defaultInterpolatedStringHandler.AppendLiteral(", enforcement=");
          defaultInterpolatedStringHandler.AppendFormatted(planSnapshot.BillingEnforcement);
          Log.Info(text5, defaultInterpolatedStringHandler.ToStringAndClear());
        }
        if (valueTuple != null)
        {
          ValueTuple<string, string> valueOrDefault = valueTuple.GetValueOrDefault();
          ClickyAnalytics.TrackSubscriptionActivated(valueOrDefault.Item1, valueOrDefault.Item2);
        }
        if (text != null)
        {
          Log.Info("Billing", "paid plan started: " + text);
          try
          {
            EventHandler<string> paidPlanStarted = this.PaidPlanStarted;
            if (paidPlanStarted != null)
            {
              paidPlanStarted(this, text);
            }
          }
          catch (Exception ex3)
          {
            Log.Error("Billing", "PaidPlanStarted handler threw", ex3);
          }
        }
      }
    }

    // Token: 0x06001EE0 RID: 7904 RVA: 0x003F0048 File Offset: 0x003F0048
    [NullableContext(1)]
    public async Task HandleCheckoutCompleteAsync(CheckoutCompleteLink link)
    {
      string text = "Billing";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(51, 2);
      defaultInterpolatedStringHandler.AppendLiteral("checkout-complete URL received (canceled=");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(link.IsCanceled);
      defaultInterpolatedStringHandler.AppendLiteral(", portal=");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(link.IsPortal);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      PlanBillingFingerprint beforeReturn = PlanBillingFingerprint.From(this.Plan);
      bool flag = true;
      if (!link.IsCanceled && !link.IsPortal)
      {
        string checkoutSessionId = link.CheckoutSessionId;
        if (checkoutSessionId != null && checkoutSessionId.Length > 0)
        {
          object obj = this.gate;
          lock (obj)
          {
            if (this.handledActivationSessionIds.Contains(checkoutSessionId))
            {
              Log.Info("Billing", "ignoring a duplicate checkout-complete callback for a handled session");
              flag = false;
            }
            else
            {
              this.pendingActivationSessionId = checkoutSessionId;
            }
          }
        }
      }
      if (link.IsCanceled)
      {
        object obj = this.gate;
        lock (obj)
        {
          this.EndBrowserWatchLocked();
        }
      }
      if (flag)
      {
        await this.RefreshAsync().ConfigureAwait(false);
      }
      if (!link.IsCanceled)
      {
        object obj = this.gate;
        PlanBillingFingerprint planBillingFingerprint;
        lock (obj)
        {
          planBillingFingerprint = this.browserWatchStart;
        }
        this.WatchForBrowserPlanChange(planBillingFingerprint ?? beforeReturn, BillingStateService.BrowserReturnFollowUps);
      }
    }

    // Token: 0x06001EE1 RID: 7905 RVA: 0x003F0094 File Offset: 0x003F0094
    [NullableContext(1)]
    public Task RefreshIfStaleAsync(TimeSpan? maximumAge = null)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.plan != null)
        {
          DateTimeOffset? dateTimeOffset = this.lastPlanAnswerAt;
          if (dateTimeOffset != null)
          {
            DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
            if (DateTimeOffset.UtcNow - valueOrDefault < (maximumAge ?? TimeSpan.FromMinutes(1L)))
            {
              return Task.CompletedTask;
            }
          }
        }
      }
      return this.RefreshAsync();
    }

    // Token: 0x06001EE2 RID: 7906 RVA: 0x003F0130 File Offset: 0x003F0130
    public void BeginWatchingForBrowserPlanChange()
    {
      this.WatchForBrowserPlanChange(PlanBillingFingerprint.From(this.Plan), Array.Empty<TimeSpan>());
    }

    // Token: 0x06001EE3 RID: 7907 RVA: 0x003F0148 File Offset: 0x003F0148
    [NullableContext(1)]
    private void WatchForBrowserPlanChange([Nullable(2)] PlanBillingFingerprint start, TimeSpan[] followUps)
    {
      BillingStateService.<>c__DisplayClass91_0 CS$<>8__locals1 = new BillingStateService.<>c__DisplayClass91_0();
      CS$<>8__locals1.followUps = followUps;
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.start = start;
      object obj = this.gate;
      lock (obj)
      {
        this.EndBrowserWatchLocked();
        if (BillingStateService.HasPlanChanged(CS$<>8__locals1.start, PlanBillingFingerprint.From(this.plan)))
        {
          return;
        }
        this.browserWatchStart = CS$<>8__locals1.start;
        CS$<>8__locals1.watch = (this.browserWatch = new CancellationTokenSource());
      }
      CS$<>8__locals1.token = CS$<>8__locals1.watch.Token;
      CS$<>8__locals1.deadline = DateTimeOffset.UtcNow + BillingStateService.BrowserWatchDuration;
      Task.Run(delegate
      {
        BillingStateService.<>c__DisplayClass91_0.<<WatchForBrowserPlanChange>b__0>d <<WatchForBrowserPlanChange>b__0>d;
        <<WatchForBrowserPlanChange>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<WatchForBrowserPlanChange>b__0>d.<>4__this = CS$<>8__locals1;
        <<WatchForBrowserPlanChange>b__0>d.<>1__state = -1;
        <<WatchForBrowserPlanChange>b__0>d.<>t__builder.Start<BillingStateService.<>c__DisplayClass91_0.<<WatchForBrowserPlanChange>b__0>d>(ref <<WatchForBrowserPlanChange>b__0>d);
        return <<WatchForBrowserPlanChange>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x06001EE4 RID: 7908 RVA: 0x003F0218 File Offset: 0x003F0218
    internal static bool HasPlanChanged(PlanBillingFingerprint start, PlanBillingFingerprint now)
    {
      if (now == null)
      {
        return false;
      }
      if (start != null)
      {
        return now != start;
      }
      return now.Plan == "unlimited";
    }

    // Token: 0x06001EE5 RID: 7909 RVA: 0x003F023C File Offset: 0x003F023C
    private void EndBrowserWatchLocked()
    {
      CancellationTokenSource cancellationTokenSource = this.browserWatch;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.browserWatch = null;
      this.browserWatchStart = null;
    }

    // Token: 0x06001EE6 RID: 7910 RVA: 0x003F0260 File Offset: 0x003F0260
    [NullableContext(1)]
    public async Task RefreshReferralAsync()
    {
      if (this.auth.IsSignedIn)
      {
        try
        {
          ReferralPaywallDiscount referralPaywallDiscount = ReferralPaywallDiscount.FromSnapshot(await this.worker.GetReferralAsync(default(CancellationToken)).ConfigureAwait(false));
          object obj = this.gate;
          bool flag2;
          lock (obj)
          {
            flag2 = referralPaywallDiscount != this.referralDiscount;
            this.referralDiscount = referralPaywallDiscount;
          }
          if (flag2)
          {
            this.RaiseChanged();
          }
        }
        catch (Exception ex) when (!(ex is OutOfMemoryException))
        {
          Log.Warn("Billing", "/me/referral failed: " + ex.Message);
        }
      }
    }

    // Token: 0x06001EE7 RID: 7911 RVA: 0x003F02A4 File Offset: 0x003F02A4
    [NullableContext(1)]
    private void OnAuthChanged([Nullable(2)] object sender, EventArgs e)
    {
      string text = (this.auth.IsSignedIn ? this.auth.UserId : null);
      object obj = this.gate;
      lock (obj)
      {
        if (text == this.currentUserId)
        {
          return;
        }
        this.currentUserId = text;
        this.plan = this.RestoreLastKnown(text);
        this.localMessagesUsed = null;
        this.gateSuspendedAfterFailOpen = false;
        this.lastFetchError = null;
        this.referralDiscount = null;
        this.lastPlanAnswerAt = null;
        this.EndBrowserWatchLocked();
      }
      this.RaiseChanged();
      if (text != null)
      {
        this.RefreshAsync();
      }
    }

    // Token: 0x06001EE8 RID: 7912 RVA: 0x003F0364 File Offset: 0x003F0364
    private string CachePath(string userId)
    {
      if (!string.IsNullOrEmpty(userId) && userId.IndexOfAny(Path.GetInvalidFileNameChars()) < 0)
      {
        return Path.Combine(this.cacheDirectory, "last-plan-" + userId + ".json");
      }
      return null;
    }

    // Token: 0x06001EE9 RID: 7913 RVA: 0x003F039C File Offset: 0x003F039C
    private PlanSnapshot RestoreLastKnown(string userId)
    {
      string text = this.CachePath(userId);
      if (text == null || !File.Exists(text))
      {
        return null;
      }
      PlanSnapshot planSnapshot2;
      try
      {
        PlanSnapshot planSnapshot = JsonSerializer.Deserialize<PlanSnapshot>(File.ReadAllBytes(text), CoreJsonContext.Default.PlanSnapshot);
        if (planSnapshot != null)
        {
          Log.Info("Billing", "restored last-known plan: " + planSnapshot.Plan);
        }
        planSnapshot2 = planSnapshot;
      }
      catch (Exception ex) when (ex is JsonException || ex is IOException || ex is UnauthorizedAccessException)
      {
        Log.Warn("Billing", "could not read the last-known plan; ignoring it");
        planSnapshot2 = null;
      }
      return planSnapshot2;
    }

    // Token: 0x06001EEA RID: 7914 RVA: 0x003F0454 File Offset: 0x003F0454
    [NullableContext(1)]
    private void PersistLastKnown(PlanSnapshot snapshot, [Nullable(2)] string userId)
    {
      string text = this.CachePath(userId);
      if (text == null)
      {
        return;
      }
      try
      {
        Directory.CreateDirectory(this.cacheDirectory);
        string text2 = text + ".tmp";
        File.WriteAllBytes(text2, JsonSerializer.SerializeToUtf8Bytes<PlanSnapshot>(snapshot, CoreJsonContext.Default.PlanSnapshot));
        File.Move(text2, text, true);
      }
      catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
      {
        Log.Warn("Billing", "could not save the last-known plan: " + ex.Message);
      }
    }

    // Token: 0x06001EEB RID: 7915 RVA: 0x003F04FC File Offset: 0x003F04FC
    private void RaiseChanged()
    {
      try
      {
        EventHandler changed = this.Changed;
        if (changed != null)
        {
          changed(this, EventArgs.Empty);
        }
      }
      catch (Exception ex)
      {
        Log.Error("Billing", "Changed handler threw", ex);
      }
    }

    // Token: 0x06001EEC RID: 7916 RVA: 0x003F0548 File Offset: 0x003F0548
    [NullableContext(1)]
    public static bool IsAuthFailure(int status, string body)
    {
      if (status != 401 && status != 403)
      {
        return false;
      }
      string text = body.ToLowerInvariant();
      return text.Contains("authentication required") || text.Contains("bad_jwt") || text.Contains("invalid jwt") || text.Contains("unable to parse or verify signature") || text.Contains("unrecognized jwt kid");
    }

    // Token: 0x06001EED RID: 7917 RVA: 0x003F05B8 File Offset: 0x003F05B8
    internal void SetPlanForTesting(PlanSnapshot snapshot)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.plan = snapshot;
        this.localMessagesUsed = ((snapshot != null && snapshot.Plan == "free") ? new int?(snapshot.Usage.Messages.Used) : null);
      }
      this.RaiseChanged();
    }

    // Token: 0x04000D2B RID: 3371
    [Nullable(1)]
    private const string Area = "Billing";

    // Token: 0x04000D2C RID: 3372
    [Nullable(1)]
    private readonly WorkerClient worker;

    // Token: 0x04000D2D RID: 3373
    [Nullable(1)]
    private readonly SupabaseAuthService auth;

    // Token: 0x04000D2E RID: 3374
    [Nullable(1)]
    private readonly string cacheDirectory;

    // Token: 0x04000D2F RID: 3375
    [Nullable(1)]
    private readonly object gate = new object();

    // Token: 0x04000D30 RID: 3376
    private PlanSnapshot plan;

    // Token: 0x04000D31 RID: 3377
    private bool isFetching;

    // Token: 0x04000D32 RID: 3378
    private BillingFetchError lastFetchError;

    // Token: 0x04000D33 RID: 3379
    private int? localMessagesUsed;

    // Token: 0x04000D34 RID: 3380
    private bool gateSuspendedAfterFailOpen;

    // Token: 0x04000D35 RID: 3381
    private Task activeRefresh;

    // Token: 0x04000D36 RID: 3382
    private int requestedRefreshGeneration;

    // Token: 0x04000D37 RID: 3383
    private string pendingActivationSessionId;

    // Token: 0x04000D38 RID: 3384
    [Nullable(1)]
    private readonly HashSet<string> handledActivationSessionIds = new HashSet<string>();

    // Token: 0x04000D39 RID: 3385
    private string currentUserId;

    // Token: 0x04000D3A RID: 3386
    private ReferralPaywallDiscount referralDiscount;

    // Token: 0x04000D3B RID: 3387
    private DateTimeOffset? lastPlanAnswerAt;

    // Token: 0x04000D3C RID: 3388
    private CancellationTokenSource browserWatch;

    // Token: 0x04000D3D RID: 3389
    private PlanBillingFingerprint browserWatchStart;

    // Token: 0x04000D40 RID: 3392
    private static readonly TimeSpan BrowserWatchDuration = TimeSpan.FromMinutes(15L);

    // Token: 0x04000D41 RID: 3393
    private static readonly TimeSpan BrowserWatchPollInterval = TimeSpan.FromSeconds(20L);

    // Token: 0x04000D42 RID: 3394
    [Nullable(1)]
    private static readonly TimeSpan[] BrowserReturnFollowUps = new TimeSpan[]
    {
      TimeSpan.FromSeconds(2L),
      TimeSpan.FromSeconds(4L),
      TimeSpan.FromSeconds(8L),
      TimeSpan.FromSeconds(15L)
    };

    // Token: 0x04000D43 RID: 3395
    [Nullable(1)]
    public const string SignedOutMessage = "it seems like you got logged out. can you log in again for me and try again?";
  }
}
