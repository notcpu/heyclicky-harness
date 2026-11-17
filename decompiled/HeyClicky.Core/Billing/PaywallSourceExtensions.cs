using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Billing
{
  // Token: 0x0200021B RID: 539
  public static class PaywallSourceExtensions
  {
    // Token: 0x06001F9B RID: 8091 RVA: 0x003F2838 File Offset: 0x003F2838
    public static bool OffersFreeOption(this PaywallSource source)
    {
      return source - PaywallSource.OnboardingCompleted <= 1;
    }

    // Token: 0x06001F9C RID: 8092 RVA: 0x003F2854 File Offset: 0x003F2854
    [NullableContext(1)]
    public static string WireName(this PaywallSource source)
    {
      string text;
      switch (source)
      {
      case PaywallSource.OnboardingCompleted:
        text = "onboarding_completed";
        break;
      case PaywallSource.DeveloperOnboardingPreview:
        text = "developer_onboarding_preview";
        break;
      case PaywallSource.NotchUpgradeButton:
        text = "notch_upgrade_button";
        break;
      case PaywallSource.SettingsUpgradeButton:
        text = "settings_upgrade_button";
        break;
      case PaywallSource.NotchPeekUpgradeButton:
        text = "notch_peek_upgrade_button";
        break;
      case PaywallSource.HomeUpgradeButton:
        text = "home_upgrade_button";
        break;
      case PaywallSource.ReferralPage:
        text = "referral_page";
        break;
      default:
        text = "limit_hit";
        break;
      }
      return text;
    }
  }
}
