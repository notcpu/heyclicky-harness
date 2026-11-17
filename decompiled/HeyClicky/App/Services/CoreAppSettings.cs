using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Settings;
using HeyClicky.Platform.Win32;

namespace HeyClicky.App.Services
{
  // Token: 0x020000E2 RID: 226
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CoreAppSettings : IAppSettings
  {
    // Token: 0x14000028 RID: 40
    // (add) Token: 0x06000AE2 RID: 2786 RVA: 0x005593AC File Offset: 0x005593AC
    // (remove) Token: 0x06000AE3 RID: 2787 RVA: 0x005593E4 File Offset: 0x005593E4
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler Changed;

    // Token: 0x06000AE4 RID: 2788 RVA: 0x0055941C File Offset: 0x0055941C
    public CoreAppSettings(SettingsStore store)
    {
      this.store = store;
      store.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        EventHandler changed = this.Changed;
        if (changed == null)
        {
          return;
        }
        changed(this, EventArgs.Empty);
      };
    }

    // Token: 0x170001CA RID: 458
    // (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00559440 File Offset: 0x00559440
    public SettingsStore Store
    {
      get
      {
        return this.store;
      }
    }

    // Token: 0x170001CB RID: 459
    // (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00559448 File Offset: 0x00559448
    // (set) Token: 0x06000AE7 RID: 2791 RVA: 0x00559474 File Offset: 0x00559474
    public string CursorColorHex
    {
      get
      {
        return this.store.Read<string>(([Nullable(1)] AppSettings s) => s.CursorColorHex);
      }
      set
      {
        this.store.Update(delegate(AppSettings s)
        {
          s.CursorColorHex = value;
        });
      }
    }

    // Token: 0x170001CC RID: 460
    // (get) Token: 0x06000AE8 RID: 2792 RVA: 0x005594A8 File Offset: 0x005594A8
    // (set) Token: 0x06000AE9 RID: 2793 RVA: 0x005594D4 File Offset: 0x005594D4
    public bool ScreenHighlightEnabled
    {
      get
      {
        return this.store.Read<bool>((AppSettings s) => s.ScreenHighlightEnabled);
      }
      set
      {
        this.store.Update(delegate(AppSettings s)
        {
          s.ScreenHighlightEnabled = value;
        });
      }
    }

    // Token: 0x170001CD RID: 461
    // (get) Token: 0x06000AEA RID: 2794 RVA: 0x00559508 File Offset: 0x00559508
    // (set) Token: 0x06000AEB RID: 2795 RVA: 0x00559534 File Offset: 0x00559534
    public bool LibraryModeEnabled
    {
      get
      {
        return this.store.Read<bool>((AppSettings s) => s.TypedTurnsRespondWithVoice);
      }
      set
      {
        this.store.Update(delegate(AppSettings s)
        {
          s.TypedTurnsRespondWithVoice = value;
        });
      }
    }

    // Token: 0x170001CE RID: 462
    // (get) Token: 0x06000AEC RID: 2796 RVA: 0x00559568 File Offset: 0x00559568
    // (set) Token: 0x06000AED RID: 2797 RVA: 0x00559594 File Offset: 0x00559594
    public bool HasCompletedIntro
    {
      get
      {
        return this.store.Read<bool>((AppSettings s) => s.Onboarding.PreSignInIntroDismissed);
      }
      set
      {
        this.store.Update(delegate(AppSettings s)
        {
          s.Onboarding.PreSignInIntroDismissed = value;
        });
      }
    }

    // Token: 0x170001CF RID: 463
    // (get) Token: 0x06000AEE RID: 2798 RVA: 0x005595C8 File Offset: 0x005595C8
    // (set) Token: 0x06000AEF RID: 2799 RVA: 0x005595F4 File Offset: 0x005595F4
    public bool HasCompletedOnboarding
    {
      get
      {
        return this.store.Read<bool>((AppSettings s) => s.Onboarding.HasCompletedOnboarding);
      }
      set
      {
        this.store.Update(delegate(AppSettings s)
        {
          s.Onboarding.HasCompletedOnboarding = value;
        });
      }
    }

    // Token: 0x170001D0 RID: 464
    // (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00559628 File Offset: 0x00559628
    // (set) Token: 0x06000AF1 RID: 2801 RVA: 0x00559670 File Offset: 0x00559670
    public bool HasCompletedTutorial
    {
      get
      {
        return this.store.Read<double?>((AppSettings s) => s.Onboarding.TutorialCompletedAtEpochSeconds) != null;
      }
      set
      {
        this.store.Update(delegate(AppSettings s)
        {
          s.Onboarding.TutorialCompletedAtEpochSeconds = (value ? new double?((double)DateTimeOffset.UtcNow.ToUnixTimeSeconds()) : null);
        });
      }
    }

    // Token: 0x170001D1 RID: 465
    // (get) Token: 0x06000AF2 RID: 2802 RVA: 0x005596A4 File Offset: 0x005596A4
    // (set) Token: 0x06000AF3 RID: 2803 RVA: 0x005596D0 File Offset: 0x005596D0
    [Nullable(2)]
    public string SquadPack
    {
      [NullableContext(2)]
      get
      {
        return this.store.Read<string>(([Nullable(1)] AppSettings s) => s.Onboarding.SquadPackId);
      }
      [NullableContext(2)]
      set
      {
        this.store.Update(delegate(AppSettings s)
        {
          s.Onboarding.SquadPackId = value;
        });
      }
    }

    // Token: 0x170001D2 RID: 466
    // (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00559704 File Offset: 0x00559704
    // (set) Token: 0x06000AF5 RID: 2805 RVA: 0x00559730 File Offset: 0x00559730
    [Nullable(2)]
    public string DiscoveryChannel
    {
      [NullableContext(2)]
      get
      {
        return this.store.Read<string>(([Nullable(1)] AppSettings s) => s.Onboarding.DiscoveryChannel);
      }
      [NullableContext(2)]
      set
      {
        this.store.Update(delegate(AppSettings s)
        {
          s.Onboarding.DiscoveryChannel = value;
        });
      }
    }

    // Token: 0x170001D3 RID: 467
    // (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00559764 File Offset: 0x00559764
    // (set) Token: 0x06000AF7 RID: 2807 RVA: 0x0055976C File Offset: 0x0055976C
    public bool IsPaused
    {
      get
      {
        return this.paused;
      }
      set
      {
        this.paused = value;
        EventHandler changed = this.Changed;
        if (changed == null)
        {
          return;
        }
        changed(this, EventArgs.Empty);
      }
    }

    // Token: 0x170001D4 RID: 468
    // (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0055978C File Offset: 0x0055978C
    // (set) Token: 0x06000AF9 RID: 2809 RVA: 0x005597B8 File Offset: 0x005597B8
    public bool CursorDocked
    {
      get
      {
        return this.store.Read<bool>((AppSettings s) => s.CursorDocked);
      }
      set
      {
        this.store.Update(delegate(AppSettings s)
        {
          s.CursorDocked = value;
        });
      }
    }

    // Token: 0x170001D5 RID: 469
    // (get) Token: 0x06000AFA RID: 2810 RVA: 0x005597EC File Offset: 0x005597EC
    // (set) Token: 0x06000AFB RID: 2811 RVA: 0x00559818 File Offset: 0x00559818
    public bool ShakeToDetachEnabled
    {
      get
      {
        return this.store.Read<bool>((AppSettings s) => s.ShakeToDetachEnabled);
      }
      set
      {
        this.store.Update(delegate(AppSettings s)
        {
          s.ShakeToDetachEnabled = value;
        });
      }
    }

    // Token: 0x170001D6 RID: 470
    // (get) Token: 0x06000AFC RID: 2812 RVA: 0x0055984C File Offset: 0x0055984C
    public string PushToTalkDisplay
    {
      get
      {
        return this.store.Read<string>(([Nullable(1)] AppSettings s) => s.EffectiveHotkeys.PushToTalk.DisplayText);
      }
    }

    // Token: 0x170001D7 RID: 471
    // (get) Token: 0x06000AFD RID: 2813 RVA: 0x00559878 File Offset: 0x00559878
    public HotkeyBinding PushToTalkBinding
    {
      get
      {
        return this.store.Read<HotkeyBinding>(([Nullable(1)] AppSettings s) => s.EffectiveHotkeys.PushToTalk);
      }
    }

    // Token: 0x170001D8 RID: 472
    // (get) Token: 0x06000AFE RID: 2814 RVA: 0x005598A4 File Offset: 0x005598A4
    public HotkeyBinding DictationBinding
    {
      get
      {
        return this.store.Read<HotkeyBinding>(([Nullable(1)] AppSettings s) => s.EffectiveHotkeys.Dictation);
      }
    }

    // Token: 0x170001D9 RID: 473
    // (get) Token: 0x06000AFF RID: 2815 RVA: 0x005598D0 File Offset: 0x005598D0
    // (set) Token: 0x06000B00 RID: 2816 RVA: 0x00559908 File Offset: 0x00559908
    public bool DictationAutoDetect
    {
      get
      {
        return this.store.Read<string>(([Nullable(1)] AppSettings s) => s.DictationLanguageCode) == "multi";
      }
      set
      {
        this.store.Update(delegate(AppSettings s)
        {
          s.DictationLanguageCode = (value ? "multi" : s.DictationSpecificLanguageCode);
        });
      }
    }

    // Token: 0x170001DA RID: 474
    // (get) Token: 0x06000B01 RID: 2817 RVA: 0x0055993C File Offset: 0x0055993C
    // (set) Token: 0x06000B02 RID: 2818 RVA: 0x00559968 File Offset: 0x00559968
    public string DictationSpecificLanguageCode
    {
      get
      {
        return this.store.Read<string>(([Nullable(1)] AppSettings s) => s.DictationSpecificLanguageCode);
      }
      set
      {
        this.store.Update(delegate(AppSettings s)
        {
          s.DictationSpecificLanguageCode = value;
          if (s.DictationLanguageCode != "multi")
          {
            s.DictationLanguageCode = value;
          }
        });
      }
    }

    // Token: 0x170001DB RID: 475
    // (get) Token: 0x06000B03 RID: 2819 RVA: 0x0055999C File Offset: 0x0055999C
    // (set) Token: 0x06000B04 RID: 2820 RVA: 0x005599D4 File Offset: 0x005599D4
    public string DictationCustomWordsText
    {
      get
      {
        return string.Join("\n", this.store.Read<List<string>>(([Nullable(1)] AppSettings s) => s.DictationCustomWords.ToList<string>()));
      }
      set
      {
        this.store.Update(delegate(AppSettings s)
        {
          s.DictationCustomWords = (from line in value.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n', StringSplitOptions.None)
            select line.Trim() into line
            where line.Length > 0
            select line).ToList<string>();
        });
      }
    }

    // Token: 0x170001DC RID: 476
    // (get) Token: 0x06000B05 RID: 2821 RVA: 0x00559A08 File Offset: 0x00559A08
    public string TextModeDisplay
    {
      get
      {
        return this.store.Read<string>(([Nullable(1)] AppSettings s) => string.Join(" + ", s.EffectiveHotkeys.TextMode.KeyNames));
      }
    }

    // Token: 0x170001DD RID: 477
    // (get) Token: 0x06000B06 RID: 2822 RVA: 0x00559A34 File Offset: 0x00559A34
    // (set) Token: 0x06000B07 RID: 2823 RVA: 0x00559A60 File Offset: 0x00559A60
    public bool LaunchAtLogin
    {
      get
      {
        return this.store.Read<bool>((AppSettings s) => s.LaunchAtLogin);
      }
      set
      {
        this.store.Update(delegate(AppSettings s)
        {
          s.LaunchAtLogin = value;
        });
        try
        {
          HeyClicky.Platform.Win32.LaunchAtLogin.SetEnabled(value, "HeyClicky", null);
        }
        catch (Exception ex)
        {
          Log.Warn("settings", "launch at login: " + ex.Message);
        }
      }
    }

    // Token: 0x040003EF RID: 1007
    private readonly SettingsStore store;

    // Token: 0x040003F0 RID: 1008
    private bool paused;
  }
}
