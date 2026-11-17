using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Worker;
using HeyClicky.Platform.Win32;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.Billing
{
  // Token: 0x02000362 RID: 866
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BillingUi : IPaywallCardHost
  {
    // Token: 0x060025BD RID: 9661 RVA: 0x0063C65C File Offset: 0x0063C65C
    public BillingUi(BillingStateService state, WorkerClient worker, DispatcherQueue ui, Action<string> openUrl, Action tuckAway, bool excludeFromCapture)
    {
      BillingUi <>4__this = this;
      this.State = state;
      this.worker = worker;
      this.ui = ui;
      this.openUrl = openUrl;
      this.tuckAway = tuckAway;
      this.excludeFromCapture = excludeFromCapture;
      state.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        ui.TryEnqueue(new DispatcherQueueHandler(<>4__this.OnStateChanged));
      };
      state.PaidPlanStarted += delegate([Nullable(2)] object _, string tier)
      {
        ui.TryEnqueue(delegate
        {
          <>4__this.OnPaidPlanStarted(tier);
        });
      };
      BillingPaywallDetector.PaywallDetected += delegate(PaywallPayload p, JsonElement raw)
      {
        ui.TryEnqueue(delegate
        {
          <>4__this.ShowPaywall(p, PaywallSource.LimitHit);
          <>4__this.State.RefreshAsync();
        });
      };
    }

    // Token: 0x170006B6 RID: 1718
    // (get) Token: 0x060025BE RID: 9662 RVA: 0x0063C734 File Offset: 0x0063C734
    public BillingStateService State { get; }

    // Token: 0x170006B7 RID: 1719
    // (get) Token: 0x060025BF RID: 9663 RVA: 0x0063C73C File Offset: 0x0063C73C
    public BillingPlanCard PlanCard
    {
      get
      {
        return BillingPlanCard.From(this.State);
      }
    }

    // Token: 0x1400008B RID: 139
    // (add) Token: 0x060025C0 RID: 9664 RVA: 0x0063C74C File Offset: 0x0063C74C
    // (remove) Token: 0x060025C1 RID: 9665 RVA: 0x0063C784 File Offset: 0x0063C784
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x060025C2 RID: 9666 RVA: 0x0063C7BC File Offset: 0x0063C7BC
    public Task RefreshAsync()
    {
      return this.State.RefreshAsync();
    }

    // Token: 0x060025C3 RID: 9667 RVA: 0x0063C7CC File Offset: 0x0063C7CC
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<string> RunPlanActionAsync(BillingPlanActionKind kind)
    {
      string text;
      switch (kind)
      {
      case BillingPlanActionKind.TryAgain:
        await this.State.RefreshAsync();
        text = null;
        break;
      case BillingPlanActionKind.FixPayment:
      case BillingPlanActionKind.ManageSubscription:
        text = await this.ManageBillingAsync((kind == BillingPlanActionKind.FixPayment) ? "settings_fix_payment" : "settings_manage_subscription");
        break;
      case BillingPlanActionKind.ViewTeam:
        this.openUrl("https://team.heyclicky.com");
        this.tuckAway();
        text = null;
        break;
      case BillingPlanActionKind.Upgrade:
        text = (this.ShowPaywall(PaywallSource.SettingsUpgradeButton) ? null : BillingPlanCard.ActionErrorText(false));
        break;
      case BillingPlanActionKind.SwitchToYearly:
        text = await this.SwitchToYearlyAsync();
        break;
      default:
        text = null;
        break;
      }
      return text;
    }

    // Token: 0x060025C4 RID: 9668 RVA: 0x0063C818 File Offset: 0x0063C818
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<string> ManageBillingAsync(string analyticsSource = "settings_manage_subscription")
    {
      string text;
      try
      {
        await this.OpenPortalOrThrowAsync(analyticsSource);
        text = null;
      }
      catch (Exception ex) when (!(ex is OutOfMemoryException))
      {
        Log.Warn("Billing", "portal launch failed: " + ex.Message);
        text = BillingPlanCard.ActionErrorText(BillingUi.IsSignedOut(ex));
      }
      return text;
    }

    // Token: 0x060025C5 RID: 9669 RVA: 0x0063C864 File Offset: 0x0063C864
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<string> SwitchToYearlyAsync()
    {
      string currentTier = this.State.CurrentTier;
      string text;
      if (!(currentTier == "pro") && !(currentTier == "max"))
      {
        text = null;
      }
      else
      {
        ClickyAnalytics.TrackAnnualBillingSwitchClicked(currentTier);
        try
        {
          await this.OpenCheckoutOrThrowAsync(currentTier, "year", "settings_plan_section", null, null);
          text = null;
        }
        catch (Exception ex) when (!(ex is OutOfMemoryException))
        {
          Log.Warn("Billing", "yearly checkout failed: " + ex.Message);
          text = BillingPlanCard.ActionErrorText(BillingUi.IsSignedOut(ex));
        }
      }
      return text;
    }

    // Token: 0x170006B8 RID: 1720
    // (get) Token: 0x060025C6 RID: 9670 RVA: 0x0063C8A8 File Offset: 0x0063C8A8
    public bool IsPaywallVisible
    {
      get
      {
        PaywallWindow paywallWindow = this.window;
        return paywallWindow != null && paywallWindow.IsShowing;
      }
    }

    // Token: 0x1400008C RID: 140
    // (add) Token: 0x060025C7 RID: 9671 RVA: 0x0063C8BC File Offset: 0x0063C8BC
    // (remove) Token: 0x060025C8 RID: 9672 RVA: 0x0063C8F4 File Offset: 0x0063C8F4
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<PaywallSource, PaywallPayload> PaywallShown;

    // Token: 0x1400008D RID: 141
    // (add) Token: 0x060025C9 RID: 9673 RVA: 0x0063C92C File Offset: 0x0063C92C
    // (remove) Token: 0x060025CA RID: 9674 RVA: 0x0063C964 File Offset: 0x0063C964
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<PaywallSource, PaywallCloseKind> PaywallClosed;

    // Token: 0x060025CB RID: 9675 RVA: 0x0063C99C File Offset: 0x0063C99C
    public bool ShowPaywall(PaywallSource source)
    {
      PaywallPayload manualUpgradePayload = this.State.ManualUpgradePayload;
      if (manualUpgradePayload == null)
      {
        Log.Info("Billing", "no upgrade to offer (" + this.State.CurrentTier + "); paywall not shown");
        return false;
      }
      return this.ShowPaywall(manualUpgradePayload, source);
    }

    // Token: 0x060025CC RID: 9676 RVA: 0x0063C9E8 File Offset: 0x0063C9E8
    public async Task<bool> ShowOnboardingPaywallAsync()
    {
      string userId = this.State.AuthenticatedUserId;
      bool flag;
      if (userId == null)
      {
        flag = BillingUi.<ShowOnboardingPaywallAsync>g__NotShown|36_0("signed out");
      }
      else if (this.IsPaywallVisible)
      {
        flag = BillingUi.<ShowOnboardingPaywallAsync>g__NotShown|36_0("a " + this.source.WireName() + " card is up");
      }
      else
      {
        int generation = this.presentation;
        await this.State.RefreshAsync();
        if (this.State.AuthenticatedUserId != userId)
        {
          flag = BillingUi.<ShowOnboardingPaywallAsync>g__NotShown|36_0("the account changed");
        }
        else if (this.State.Plan == null)
        {
          flag = BillingUi.<ShowOnboardingPaywallAsync>g__NotShown|36_0("the plan is unknown");
        }
        else if (this.State.CurrentTier != "free" || this.State.IsTeamManagedPlan)
        {
          flag = BillingUi.<ShowOnboardingPaywallAsync>g__NotShown|36_0("a " + (this.State.IsTeamManagedPlan ? "team" : this.State.CurrentTier) + " plan");
        }
        else if (generation != this.presentation)
        {
          flag = BillingUi.<ShowOnboardingPaywallAsync>g__NotShown|36_0("another card came up");
        }
        else
        {
          PaywallPayload manualUpgradePayload = this.State.ManualUpgradePayload;
          if (manualUpgradePayload == null)
          {
            flag = BillingUi.<ShowOnboardingPaywallAsync>g__NotShown|36_0("no upgrade to offer");
          }
          else
          {
            flag = this.ShowPaywall(manualUpgradePayload, PaywallSource.OnboardingCompleted);
          }
        }
      }
      return flag;
    }

    // Token: 0x060025CD RID: 9677 RVA: 0x0063CA2C File Offset: 0x0063CA2C
    public bool ShowPaywall(PaywallPayload payload, PaywallSource source)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (BillingUi.WaitsOutQuiet(source))
      {
        string text = BillingUi.QuietReasonNow();
        if (text != null)
        {
          this.heldBack = new ValueTuple<PaywallPayload, PaywallSource, DateTimeOffset>?(new ValueTuple<PaywallPayload, PaywallSource, DateTimeOffset>(payload, source, DateTimeOffset.Now));
          this.StartQuietWatch();
          string text2 = "Billing";
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 2);
          defaultInterpolatedStringHandler.AppendLiteral("paywall held back: ");
          defaultInterpolatedStringHandler.AppendFormatted(text);
          defaultInterpolatedStringHandler.AppendLiteral(" (source=");
          defaultInterpolatedStringHandler.AppendFormatted(source.WireName());
          defaultInterpolatedStringHandler.AppendLiteral(")");
          Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
          return false;
        }
      }
      this.heldBack = null;
      try
      {
        this.tuckAway();
      }
      catch (Exception ex)
      {
        Log.Warn("Billing", "tuck away: " + ex.Message);
      }
      this.presentation++;
      this.previewReferral = null;
      this.source = source;
      this.payload = payload;
      PaywallPalette paywallPalette = PaywallPalette.Current();
      this.card = new PaywallCard(payload, source, paywallPalette, this.State.CurrentTier, this.State.ReferralDiscount, this);
      if (this.window == null)
      {
        this.window = new PaywallWindow(this.excludeFromCapture);
      }
      double num = (double)(source.OffersFreeOption() ? 800 : ((payload.UpgradeOptions.Count >= 2) ? 560 : 400));
      this.window.Present(this.card, num, paywallPalette);
      string text3 = "Billing";
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 4);
      defaultInterpolatedStringHandler.AppendLiteral("paywall shown: source=");
      defaultInterpolatedStringHandler.AppendFormatted(source.WireName());
      defaultInterpolatedStringHandler.AppendLiteral(" surface=");
      defaultInterpolatedStringHandler.AppendFormatted(payload.Surface);
      defaultInterpolatedStringHandler.AppendLiteral(" reason=");
      defaultInterpolatedStringHandler.AppendFormatted(payload.Reason ?? "-");
      defaultInterpolatedStringHandler.AppendLiteral(" plans=");
      defaultInterpolatedStringHandler.AppendFormatted<int>(payload.UpgradeOptions.Count);
      Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
      ClickyAnalytics.TrackPaywallShown(payload.Surface, payload.Reason, payload.CurrentUsage, payload.Limit, payload.PriceDisplay, source.WireName(), source.OffersFreeOption(), payload.UpgradeOptions.Select<PaywallPlanOption, string>((PaywallPlanOption o) => o.Key), payload.SupportsYearly, payload.DefaultInterval);
      if (BillingUi.WaitsOutQuiet(source))
      {
        this.StartQuietWatch();
      }
      if (this.State.AuthenticatedUserId != null)
      {
        this.State.RefreshReferralAsync();
      }
      Action<PaywallSource, PaywallPayload> paywallShown = this.PaywallShown;
      if (paywallShown != null)
      {
        paywallShown(source, payload);
      }
      return true;
    }

    // Token: 0x060025CE RID: 9678 RVA: 0x0063CCE0 File Offset: 0x0063CCE0
    public void DismissPaywall()
    {
      this.Close(PaywallCloseKind.Dismissed);
    }

    // Token: 0x060025CF RID: 9679 RVA: 0x0063CCEC File Offset: 0x0063CCEC
    private static bool WaitsOutQuiet(PaywallSource source)
    {
      return source == PaywallSource.LimitHit;
    }

    // Token: 0x170006B9 RID: 1721
    // (get) Token: 0x060025D0 RID: 9680 RVA: 0x0063CCF4 File Offset: 0x0063CCF4
    // (set) Token: 0x060025D1 RID: 9681 RVA: 0x0063CCFC File Offset: 0x0063CCFC
    [Nullable(new byte[] { 1, 2 })]
    public static Func<string> QuietReasonNow
    {
      [return: Nullable(new byte[] { 1, 2 })]
      get;
      [param: Nullable(new byte[] { 1, 2 })]
      set;
    } = delegate
    {
      if (QuietContext.IgnoredForTests)
      {
        return null;
      }
      if (QuietContext.QueryNotificationState() == QuietContext.NotificationState.PresentationMode)
      {
        return "presenting";
      }
      if (!QuietContext.IsAnotherAppUsingMicrophone())
      {
        return null;
      }
      return "on a call";
    };

    // Token: 0x060025D2 RID: 9682 RVA: 0x0063CD04 File Offset: 0x0063CD04
    private void StartQuietWatch()
    {
      if (this.quietWatch == null)
      {
        this.quietWatch = this.ui.CreateTimer();
        this.quietWatch.Interval = TimeSpan.FromSeconds(2L);
        this.quietWatch.Tick += delegate(DispatcherQueueTimer _, object _)
        {
          this.CheckQuiet();
        };
      }
      if (!this.quietWatch.IsRunning)
      {
        this.quietWatch.Start();
      }
    }

    // Token: 0x060025D3 RID: 9683 RVA: 0x0063CD6C File Offset: 0x0063CD6C
    private void CheckQuiet()
    {
      string text = BillingUi.QuietReasonNow();
      if (this.IsPaywallVisible && BillingUi.WaitsOutQuiet(this.source))
      {
        if (text == null)
        {
          return;
        }
        string text2 = "Billing";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 2);
        defaultInterpolatedStringHandler.AppendLiteral("paywall dismissed: ");
        defaultInterpolatedStringHandler.AppendFormatted(text);
        defaultInterpolatedStringHandler.AppendLiteral(" (source=");
        defaultInterpolatedStringHandler.AppendFormatted(this.source.WireName());
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
        this.Close(PaywallCloseKind.Dismissed);
      }
      ValueTuple<PaywallPayload, PaywallSource, DateTimeOffset>? valueTuple = this.heldBack;
      if (valueTuple != null)
      {
        ValueTuple<PaywallPayload, PaywallSource, DateTimeOffset> valueOrDefault = valueTuple.GetValueOrDefault();
        if (DateTimeOffset.Now - valueOrDefault.Item3 > BillingUi.HeldBackLifetime)
        {
          this.heldBack = null;
          Log.Info("Billing", "held-back paywall dropped (too old)");
        }
        else if (text == null && !this.IsPaywallVisible)
        {
          this.heldBack = null;
          Log.Info("Billing", "the call is over: showing the held-back paywall");
          this.ShowPaywall(valueOrDefault.Item1, valueOrDefault.Item2);
          return;
        }
      }
      valueTuple = this.heldBack;
      if (valueTuple == null && (!this.IsPaywallVisible || !BillingUi.WaitsOutQuiet(this.source)))
      {
        DispatcherQueueTimer dispatcherQueueTimer = this.quietWatch;
        if (dispatcherQueueTimer == null)
        {
          return;
        }
        dispatcherQueueTimer.Stop();
      }
    }

    // Token: 0x170006BA RID: 1722
    // (get) Token: 0x060025D4 RID: 9684 RVA: 0x0063CEC0 File Offset: 0x0063CEC0
    [Nullable(2)]
    public PaywallCard Card
    {
      [NullableContext(2)]
      get
      {
        PaywallWindow paywallWindow = this.window;
        if (paywallWindow == null || !paywallWindow.IsShowing)
        {
          return null;
        }
        return this.card;
      }
    }

    // Token: 0x170006BB RID: 1723
    // (get) Token: 0x060025D5 RID: 9685 RVA: 0x0063CEE0 File Offset: 0x0063CEE0
    [Nullable(2)]
    public PaywallWindow Window
    {
      [NullableContext(2)]
      get
      {
        return this.window;
      }
    }

    // Token: 0x060025D6 RID: 9686 RVA: 0x0063CEE8 File Offset: 0x0063CEE8
    void IPaywallCardHost.Close(PaywallCloseKind kind)
    {
      this.Close(kind);
    }

    // Token: 0x060025D7 RID: 9687 RVA: 0x0063CEF4 File Offset: 0x0063CEF4
    private void Close(PaywallCloseKind kind)
    {
      if (this.window == null || !this.window.IsShowing)
      {
        return;
      }
      this.window.Dismiss();
      this.card = null;
      string text = "Billing";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 2);
      defaultInterpolatedStringHandler.AppendLiteral("paywall closed: ");
      defaultInterpolatedStringHandler.AppendFormatted<PaywallCloseKind>(kind);
      defaultInterpolatedStringHandler.AppendLiteral(" (source=");
      defaultInterpolatedStringHandler.AppendFormatted(this.source.WireName());
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      Action<PaywallSource, PaywallCloseKind> paywallClosed = this.PaywallClosed;
      if (paywallClosed == null)
      {
        return;
      }
      paywallClosed(this.source, kind);
    }

    // Token: 0x060025D8 RID: 9688 RVA: 0x0063CF9C File Offset: 0x0063CF9C
    void IPaywallCardHost.ContentResized()
    {
      PaywallWindow paywallWindow = this.window;
      if (paywallWindow != null && paywallWindow.IsShowing)
      {
        this.ui.TryEnqueue(DispatcherQueuePriority.Low, delegate
        {
          PaywallWindow paywallWindow2 = this.window;
          if (paywallWindow2 == null)
          {
            return;
          }
          paywallWindow2.Fit();
        });
      }
    }

    // Token: 0x060025D9 RID: 9689 RVA: 0x0063CFCC File Offset: 0x0063CFCC
    [return: Nullable(new byte[] { 1, 2 })]
    async Task<string> IPaywallCardHost.StartCheckoutAsync(string plan, string interval, string surface, PaywallSource source)
    {
      string text2;
      try
      {
        string text = ((source == PaywallSource.DeveloperOnboardingPreview) ? "developer_paywall_preview" : (source.OffersFreeOption() ? "onboarding_paywall" : "paywall"));
        await this.OpenCheckoutOrThrowAsync(plan, interval, text, surface, source.WireName());
        text2 = null;
      }
      catch (Exception ex) when (!(ex is OutOfMemoryException))
      {
        string text3 = "Billing";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 3);
        defaultInterpolatedStringHandler.AppendLiteral("checkout launch failed (plan=");
        defaultInterpolatedStringHandler.AppendFormatted(plan);
        defaultInterpolatedStringHandler.AppendLiteral(" interval=");
        defaultInterpolatedStringHandler.AppendFormatted(interval);
        defaultInterpolatedStringHandler.AppendLiteral("): ");
        defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
        Log.Warn(text3, defaultInterpolatedStringHandler.ToStringAndClear());
        text2 = BillingUi.CheckoutFailureMessage(ex);
      }
      return text2;
    }

    // Token: 0x060025DA RID: 9690 RVA: 0x0063D030 File Offset: 0x0063D030
    [return: Nullable(new byte[] { 1, 2 })]
    async Task<string> IPaywallCardHost.OpenPortalAsync()
    {
      string text;
      try
      {
        await this.OpenPortalOrThrowAsync("paywall_payment_problem");
        text = null;
      }
      catch (Exception ex) when (!(ex is OutOfMemoryException))
      {
        Log.Warn("Billing", "portal launch failed: " + ex.Message);
        text = BillingUi.CheckoutFailureMessage(ex);
      }
      return text;
    }

    // Token: 0x060025DB RID: 9691 RVA: 0x0063D074 File Offset: 0x0063D074
    private void OnStateChanged()
    {
      PaywallCard paywallCard = this.card;
      if (paywallCard != null)
      {
        paywallCard.SetReferral(this.previewReferral ?? this.State.ReferralDiscount);
      }
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x060025DC RID: 9692 RVA: 0x0063D0AC File Offset: 0x0063D0AC
    private async Task OpenCheckoutOrThrowAsync(string plan, string interval, string analyticsSource, [Nullable(2)] string surface, [Nullable(2)] string presentationSource)
    {
      CheckoutResponse checkoutResponse = await this.worker.CreateCheckoutAsync(plan, interval, null, default(CancellationToken));
      if (checkoutResponse == null)
      {
        throw new BillingUi.BillingSignedOutException();
      }
      Uri uri;
      bool flag = !Uri.TryCreate(checkoutResponse.Url, UriKind.Absolute, out uri);
      if (!flag)
      {
        string scheme = uri.Scheme;
        flag = !(scheme == "https") && !(scheme == "http");
      }
      if (flag)
      {
        throw new JsonException("checkout URL missing");
      }
      this.tuckAway();
      this.openUrl(uri.AbsoluteUri);
      this.State.BeginWatchingForBrowserPlanChange();
      string text = "Billing";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(61, 4);
      defaultInterpolatedStringHandler.AppendLiteral("checkout opened in the browser: plan=");
      defaultInterpolatedStringHandler.AppendFormatted(plan);
      defaultInterpolatedStringHandler.AppendLiteral(" interval=");
      defaultInterpolatedStringHandler.AppendFormatted(interval);
      defaultInterpolatedStringHandler.AppendLiteral(" source=");
      defaultInterpolatedStringHandler.AppendFormatted(analyticsSource);
      defaultInterpolatedStringHandler.AppendLiteral(" host=");
      defaultInterpolatedStringHandler.AppendFormatted(uri.Host);
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      ClickyAnalytics.TrackCheckoutStarted(analyticsSource, surface, plan, interval, presentationSource);
      this.SayCheckoutOpened(plan);
    }

    // Token: 0x060025DD RID: 9693 RVA: 0x0063D11C File Offset: 0x0063D11C
    private async Task OpenPortalOrThrowAsync(string analyticsSource)
    {
      PortalResponse portalResponse = await this.worker.CreatePortalSessionAsync(default(CancellationToken));
      if (portalResponse == null)
      {
        throw new BillingUi.BillingSignedOutException();
      }
      Uri uri;
      bool flag = !Uri.TryCreate(portalResponse.Url, UriKind.Absolute, out uri);
      if (!flag)
      {
        string scheme = uri.Scheme;
        flag = !(scheme == "https") && !(scheme == "http");
      }
      if (flag)
      {
        throw new JsonException("portal URL missing");
      }
      this.tuckAway();
      this.openUrl(uri.AbsoluteUri);
      this.State.BeginWatchingForBrowserPlanChange();
      Log.Info("Billing", "customer portal opened in the browser: source=" + analyticsSource + " host=" + uri.Host);
      ClickyAnalytics.TrackCustomerPortalOpened(analyticsSource);
    }

    // Token: 0x060025DE RID: 9694 RVA: 0x0063D168 File Offset: 0x0063D168
    private static bool IsSignedOut(Exception ex)
    {
      if (!(ex is BillingUi.BillingSignedOutException))
      {
        WorkerHttpException ex2 = ex as WorkerHttpException;
        return ex2 != null && BillingStateService.IsAuthFailure(ex2.Status, ex2.Body);
      }
      return true;
    }

    // Token: 0x060025DF RID: 9695 RVA: 0x0063D19C File Offset: 0x0063D19C
    public static string CheckoutFailureMessage(Exception ex)
    {
      WorkerHttpException ex2 = ex as WorkerHttpException;
      if (ex2 != null)
      {
        string body = ex2.Body;
        if (BillingUi.ServerField(body, "code") == "invalid_discount_code")
        {
          string text = BillingUi.ServerField(body, "error");
          if (text != null)
          {
            return text;
          }
        }
      }
      string text2;
      if (BillingUi.IsSignedOut(ex))
      {
        text2 = "you appear to be signed out, try signing in again";
      }
      else
      {
        WorkerHttpException ex3 = ex as WorkerHttpException;
        if (ex3 == null)
        {
          if (!(ex is JsonException))
          {
            if (!(ex is HttpRequestException) && !(ex is TaskCanceledException) && !(ex is IOException))
            {
              text2 = ex.Message;
            }
            else
            {
              text2 = "a network problem reaching our servers";
            }
          }
          else
          {
            text2 = "we couldn't read the server's response";
          }
        }
        else
        {
          string text3 = BillingUi.ServerField(ex3.Body, "error");
          string text4;
          if (text3 == null)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 1);
            defaultInterpolatedStringHandler.AppendLiteral("the server returned HTTP ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(ex3.Status);
            text4 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          else
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 2);
            defaultInterpolatedStringHandler.AppendFormatted(text3);
            defaultInterpolatedStringHandler.AppendLiteral(" (HTTP ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(ex3.Status);
            defaultInterpolatedStringHandler.AppendLiteral(")");
            text4 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          text2 = text4;
        }
      }
      string text5 = text2;
      return "Couldn't start checkout — " + text5 + ". Please email hi@heyclicky.com and we'll sort it out fast.";
    }

    // Token: 0x060025E0 RID: 9696 RVA: 0x0063D2F4 File Offset: 0x0063D2F4
    [return: Nullable(2)]
    private static string ServerField(string body, string name)
    {
      string text3;
      try
      {
        using (JsonDocument jsonDocument = JsonDocument.Parse(body, default(JsonDocumentOptions)))
        {
          JsonElement jsonElement;
          string text2;
          if (jsonDocument.RootElement.ValueKind == JsonValueKind.Object && jsonDocument.RootElement.TryGetProperty(name, out jsonElement) && jsonElement.ValueKind == JsonValueKind.String)
          {
            string @string = jsonElement.GetString();
            string text = ((@string != null) ? @string.Trim() : null);
            if (text != null && text.Length > 0)
            {
              text2 = text;
              goto IL_0064;
            }
          }
          text2 = null;
          IL_0064:
          text3 = text2;
        }
      }
      catch (JsonException)
      {
        text3 = null;
      }
      return text3;
    }

    // Token: 0x060025E1 RID: 9697 RVA: 0x0063D398 File Offset: 0x0063D398
    public void HandleCheckoutComplete(CheckoutCompleteLink link)
    {
      this.State.HandleCheckoutCompleteAsync(link);
      if (link.IsCanceled)
      {
        this.SayLine("( \u02d8▽\u02d8) no worries, nothing was charged. i'm here whenever you're ready.");
        return;
      }
      if (link.IsPortal)
      {
        this.SayLine("(^-^)ノ welcome back!");
        return;
      }
      DispatcherQueueTimer dispatcherQueueTimer = this.paidReturnTimer;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      this.paidReturnTimer = this.ui.CreateTimer();
      this.paidReturnTimer.Interval = TimeSpan.FromSeconds(6L);
      this.paidReturnTimer.IsRepeating = false;
      this.paidReturnTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.paidReturnTimer = null;
        DateTimeOffset? dateTimeOffset = this.lastPaidPlanCelebratedAt;
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
          if (DateTimeOffset.Now - valueOrDefault < TimeSpan.FromMinutes(10L))
          {
            this.SayLine("(・ω・)ノ hi, i'm here!");
            return;
          }
        }
        if (this.State.CurrentTier != "free")
        {
          this.SayLine("(^_^)v you're all set! thanks for making me more powerful. " + this.HoldToTalkHint);
          return;
        }
        this.SayLine("(^_^)v payment's through! your plan will switch on in a moment.");
      };
      this.paidReturnTimer.Start();
    }

    // Token: 0x1400008E RID: 142
    // (add) Token: 0x060025E2 RID: 9698 RVA: 0x0063D43C File Offset: 0x0063D43C
    // (remove) Token: 0x060025E3 RID: 9699 RVA: 0x0063D474 File Offset: 0x0063D474
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<string> CursorLine;

    // Token: 0x170006BC RID: 1724
    // (get) Token: 0x060025E4 RID: 9700 RVA: 0x0063D4AC File Offset: 0x0063D4AC
    // (set) Token: 0x060025E5 RID: 9701 RVA: 0x0063D4B4 File Offset: 0x0063D4B4
    public Func<bool> CanSayLine { get; set; } = () => true;

    // Token: 0x170006BD RID: 1725
    // (get) Token: 0x060025E6 RID: 9702 RVA: 0x0063D4C0 File Offset: 0x0063D4C0
    // (set) Token: 0x060025E7 RID: 9703 RVA: 0x0063D4C8 File Offset: 0x0063D4C8
    public Func<string> HoldShortcutDescription { get; set; } = () => "ctrl + alt";

    // Token: 0x170006BE RID: 1726
    // (get) Token: 0x060025E8 RID: 9704 RVA: 0x0063D4D4 File Offset: 0x0063D4D4
    private string HoldToTalkHint
    {
      get
      {
        return "hold " + this.HoldShortcutDescription() + " anytime to talk to me.";
      }
    }

    // Token: 0x060025E9 RID: 9705 RVA: 0x0063D4F0 File Offset: 0x0063D4F0
    private void SayCheckoutOpened(string plan)
    {
      this.SayLine((plan == "max") ? "＼(^o^)／ let's go max! i'll be right here when you're done." : "(\uff3e▽\uff3e) let's go pro! i'll be right here when you're done.");
    }

    // Token: 0x060025EA RID: 9706 RVA: 0x0063D514 File Offset: 0x0063D514
    private void OnPaidPlanStarted(string tier)
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.paidReturnTimer;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      this.paidReturnTimer = null;
      this.lastPaidPlanCelebratedAt = new DateTimeOffset?(DateTimeOffset.Now);
      string text = ((tier == "max") ? "＼(^o^)／ welcome to max! thank you for making me extra powerful." : "(\uff3e▽\uff3e) welcome to pro! thank you for making me more powerful.");
      this.SayLine(text + " " + this.HoldToTalkHint);
    }

    // Token: 0x060025EB RID: 9707 RVA: 0x0063D57C File Offset: 0x0063D57C
    private void SayLine(string line)
    {
      if (!this.CanSayLine())
      {
        Log.Info("Billing", "cursor line skipped (busy or onboarding): " + line);
        return;
      }
      Log.Info("Billing", "cursor line: " + line);
      Action<string> cursorLine = this.CursorLine;
      if (cursorLine == null)
      {
        return;
      }
      cursorLine(line);
    }

    // Token: 0x060025ED RID: 9709 RVA: 0x0063D5F8 File Offset: 0x0063D5F8
    [CompilerGenerated]
    internal static bool <ShowOnboardingPaywallAsync>g__NotShown|36_0(string why)
    {
      Log.Info("Billing", "onboarding paywall not shown: " + why);
      return false;
    }

    // Token: 0x040011DC RID: 4572
    private const string Area = "Billing";

    // Token: 0x040011DD RID: 4573
    private readonly WorkerClient worker;

    // Token: 0x040011DE RID: 4574
    private readonly DispatcherQueue ui;

    // Token: 0x040011DF RID: 4575
    private readonly Action<string> openUrl;

    // Token: 0x040011E0 RID: 4576
    private readonly Action tuckAway;

    // Token: 0x040011E1 RID: 4577
    private readonly bool excludeFromCapture;

    // Token: 0x040011E2 RID: 4578
    [Nullable(2)]
    private PaywallWindow window;

    // Token: 0x040011E3 RID: 4579
    [Nullable(2)]
    private PaywallCard card;

    // Token: 0x040011E4 RID: 4580
    private PaywallSource source;

    // Token: 0x040011E5 RID: 4581
    [Nullable(2)]
    private PaywallPayload payload;

    // Token: 0x040011E6 RID: 4582
    private int presentation;

    // Token: 0x040011E7 RID: 4583
    [Nullable(2)]
    private ReferralPaywallDiscount previewReferral;

    // Token: 0x040011E8 RID: 4584
    [Nullable(2)]
    private DispatcherQueueTimer paidReturnTimer;

    // Token: 0x040011E9 RID: 4585
    private DateTimeOffset? lastPaidPlanCelebratedAt;

    // Token: 0x040011EE RID: 4590
    [TupleElementNames(new string[] { "Payload", "Source", "At" })]
    [Nullable(new byte[] { 0, 1 })]
    private ValueTuple<PaywallPayload, PaywallSource, DateTimeOffset>? heldBack;

    // Token: 0x040011EF RID: 4591
    [Nullable(2)]
    private DispatcherQueueTimer quietWatch;

    // Token: 0x040011F0 RID: 4592
    private static readonly TimeSpan HeldBackLifetime = TimeSpan.FromMinutes(15L);

    // Token: 0x02000A7F RID: 2687
    [NullableContext(0)]
    private sealed class BillingSignedOutException : Exception
    {
      // Token: 0x0600414F RID: 16719 RVA: 0x006C6938 File Offset: 0x006C6938
      public BillingSignedOutException()
        : base("not signed in")
      {
      }
    }
  }
}
