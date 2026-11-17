using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using HeyClicky.App.Billing;
using HeyClicky.App.Services;
using HeyClicky.App.Services.Updates;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Telemetry;
using Microsoft.UI.Xaml.Automation;

namespace HeyClicky.App.UI
{
  // Token: 0x020000B0 RID: 176
  [NullableContext(1)]
  [Nullable(0)]
  public static class HeaderPills
  {
    // Token: 0x1400000F RID: 15
    // (add) Token: 0x0600072E RID: 1838 RVA: 0x005363A4 File Offset: 0x005363A4
    // (remove) Token: 0x0600072F RID: 1839 RVA: 0x005363D8 File Offset: 0x005363D8
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public static event Action Changed;

    // Token: 0x06000730 RID: 1840 RVA: 0x0053640C File Offset: 0x0053640C
    public static void RaiseChanged()
    {
      Action changed = HeaderPills.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x17000128 RID: 296
    // (get) Token: 0x06000731 RID: 1841 RVA: 0x00536420 File Offset: 0x00536420
    // (set) Token: 0x06000732 RID: 1842 RVA: 0x00536428 File Offset: 0x00536428
    public static Func<bool> HidesPromotionalButtons { get; set; } = () => false;

    // Token: 0x17000129 RID: 297
    // (get) Token: 0x06000733 RID: 1843 RVA: 0x00536430 File Offset: 0x00536430
    // (set) Token: 0x06000734 RID: 1844 RVA: 0x00536438 File Offset: 0x00536438
    public static bool PreviewUpdateReady
    {
      get
      {
        return HeaderPills.previewUpdateReady;
      }
      set
      {
        if (HeaderPills.previewUpdateReady == value)
        {
          return;
        }
        HeaderPills.previewUpdateReady = value;
        HeaderPills.RaiseChanged();
      }
    }

    // Token: 0x06000735 RID: 1845 RVA: 0x00536450 File Offset: 0x00536450
    public static bool ShowsUpdate(AppServices s)
    {
      return HeaderPills.IsUpdateReady(s) || HeaderPills.IsUpdateDeferred(s);
    }

    // Token: 0x06000736 RID: 1846 RVA: 0x00536464 File Offset: 0x00536464
    private static bool IsUpdateReady(AppServices s)
    {
      return HeaderPills.PreviewUpdateReady || s.Updates.State == UpdateState.ReadyToInstall;
    }

    // Token: 0x06000737 RID: 1847 RVA: 0x00536480 File Offset: 0x00536480
    private static bool IsUpdateDeferred(AppServices s)
    {
      return s.Updates.State == UpdateState.Failed && s.Updates.AvailableVersion != null;
    }

    // Token: 0x06000738 RID: 1848 RVA: 0x005364A4 File Offset: 0x005364A4
    public static bool ShowsUpgrade(AppServices s)
    {
      if (!HeaderPills.ShowsUpdate(s))
      {
        BillingUi billing = s.Billing;
        if (billing != null && billing.State.ShouldOfferManualUpgrade)
        {
          return !HeaderPills.HidesPromotionalButtons();
        }
      }
      return false;
    }

    // Token: 0x06000739 RID: 1849 RVA: 0x005364D8 File Offset: 0x005364D8
    public static GelButton UpdateButton(AppServices s, string surface, bool compact, bool iconOnly, [Nullable(2)] Action before = null)
    {
      bool isReady = HeaderPills.IsUpdateReady(s);
      GelButton gelButton = new GelButton(iconOnly ? "" : "Update app", GelAppearance.Orange, compact ? GelButton.Sizing.Compact : GelButton.Sizing.Normal, 100.0, "\ue896", null, null);
      gelButton.Tip(isReady ? "Install the downloaded update and relaunch HeyClicky" : "Download and install the new version of HeyClicky");
      AutomationProperties.SetName(gelButton, "Update app");
      gelButton.Click += delegate
      {
        Action before2 = before;
        if (before2 != null)
        {
          before2();
        }
        if (surface == "notch_peek")
        {
          HeaderPills.PeekAction("update_app");
        }
        if (!isReady)
        {
          s.Updates.CheckNowAsync(surface);
          return;
        }
        if (HeaderPills.PreviewUpdateReady && s.Updates.State != UpdateState.ReadyToInstall)
        {
          Log.Info("updates", "update pill (" + surface + "): preview only, nothing to install");
          return;
        }
        s.Updates.InstallAndRelaunch(surface);
      };
      return gelButton;
    }

    // Token: 0x0600073A RID: 1850 RVA: 0x00536588 File Offset: 0x00536588
    private static void PeekAction(string action)
    {
      string text = "notch_peek_action";
      string text2 = null;
      string text3 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = action;
      ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
    }

    // Token: 0x0600073B RID: 1851 RVA: 0x005365C0 File Offset: 0x005365C0
    public static GelButton UpgradeButton(AppServices s, PaywallSource source, bool compact, bool withIcon, [Nullable(2)] Action before = null)
    {
      GelButton gelButton = new GelButton("Upgrade", GelAppearance.PrimaryBlue, compact ? GelButton.Sizing.Compact : GelButton.Sizing.Normal, 100.0, withIcon ? "\uf4a5" : null, null, null);
      gelButton.Tip("See HeyClicky Pro and Max");
      gelButton.Click += delegate
      {
        Action before2 = before;
        if (before2 != null)
        {
          before2();
        }
        Log.Info("billing", "upgrade pill: " + source.WireName());
        if (source == PaywallSource.NotchPeekUpgradeButton)
        {
          HeaderPills.PeekAction("upgrade");
        }
        BillingUi billing = s.Billing;
        if (billing == null)
        {
          return;
        }
        billing.ShowPaywall(source);
      };
      return gelButton;
    }

    // Token: 0x04000227 RID: 551
    private static bool previewUpdateReady;
  }
}
