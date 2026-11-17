using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Voice;

namespace HeyClicky.Core.Onboarding
{
  // Token: 0x020000F4 RID: 244
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class PreSignInIntroViewModel : ObservableObject
  {
    // Token: 0x14000017 RID: 23
    // (add) Token: 0x06000F76 RID: 3958 RVA: 0x0038ED18 File Offset: 0x0038ED18
    // (remove) Token: 0x06000F77 RID: 3959 RVA: 0x0038ED50 File Offset: 0x0038ED50
    public event EventHandler HandOffToNotchRequested;

    // Token: 0x14000018 RID: 24
    // (add) Token: 0x06000F78 RID: 3960 RVA: 0x0038ED88 File Offset: 0x0038ED88
    // (remove) Token: 0x06000F79 RID: 3961 RVA: 0x0038EDC0 File Offset: 0x0038EDC0
    public event EventHandler Dismissed;

    // Token: 0x14000019 RID: 25
    // (add) Token: 0x06000F7A RID: 3962 RVA: 0x0038EDF8 File Offset: 0x0038EDF8
    // (remove) Token: 0x06000F7B RID: 3963 RVA: 0x0038EE30 File Offset: 0x0038EE30
    public event EventHandler SignInRequested;

    // Token: 0x06000F7C RID: 3964 RVA: 0x0038EE68 File Offset: 0x0038EE68
    [NullableContext(1)]
    public PreSignInIntroViewModel(SettingsStore settings, [Nullable(new byte[] { 2, 1, 2, 1 })] Func<string, string, Task> syncDiscoveryChannel = null, [Nullable(new byte[] { 2, 2, 1, 1, 1 })] Func<string, Task<IReadOnlyList<string>>> fetchRegionLanguages = null)
    {
      this.settings = settings;
      this.syncDiscoveryChannel = syncDiscoveryChannel;
      this.fetchRegionLanguages = fetchRegionLanguages;
      OnboardingProgress onboardingProgress = settings.Read<OnboardingProgress>(([Nullable(1)] AppSettings s) => s.Onboarding);
      this.selectedDiscoveryChannel = onboardingProgress.DiscoveryChannel;
    }

    // Token: 0x1700037E RID: 894
    // (get) Token: 0x06000F7D RID: 3965 RVA: 0x0038EED0 File Offset: 0x0038EED0
    [Nullable(1)]
    public IReadOnlyList<DiscoveryChannelOption> DiscoveryChannels
    {
      [NullableContext(1)]
      get
      {
        return OnboardingCatalog.DiscoveryChannels;
      }
    }

    // Token: 0x1700037F RID: 895
    // (get) Token: 0x06000F7E RID: 3966 RVA: 0x0038EED8 File Offset: 0x0038EED8
    [Nullable(1)]
    public IReadOnlyList<CursorColorOption> CursorColors
    {
      [NullableContext(1)]
      get
      {
        return OnboardingCatalog.CursorColors;
      }
    }

    // Token: 0x06000F7F RID: 3967 RVA: 0x0038EEE0 File Offset: 0x0038EEE0
    [NullableContext(1)]
    public static bool ShouldPresent(OnboardingProgress progress, bool isSignedIn)
    {
      return !progress.PreSignInIntroDismissed && (!isSignedIn || !progress.HasCompletedOnboarding);
    }

    // Token: 0x17000380 RID: 896
    // (get) Token: 0x06000F80 RID: 3968 RVA: 0x0038EEFC File Offset: 0x0038EEFC
    [Nullable(1)]
    public string AnalyticsName
    {
      [NullableContext(1)]
      get
      {
        string text;
        switch (this.Step)
        {
        case PreSignInIntroStep.WhatIsClicky:
          text = "what_is_clicky";
          break;
        case PreSignInIntroStep.WhereClickyLives:
          text = "where_clicky_lives";
          break;
        case PreSignInIntroStep.ClickyGetsLost:
          text = "clicky_gets_lost";
          break;
        case PreSignInIntroStep.WhereHeardAbout:
          text = "where_heard_about";
          break;
        case PreSignInIntroStep.OpenClicky:
          text = "open_clicky";
          break;
        case PreSignInIntroStep.Hatched:
          text = "hatched";
          break;
        case PreSignInIntroStep.SkillSelection:
          text = "skill_selection";
          break;
        default:
          text = "ready_to_start";
          break;
        }
        return text;
      }
    }

    // Token: 0x17000381 RID: 897
    // (get) Token: 0x06000F81 RID: 3969 RVA: 0x0038EF74 File Offset: 0x0038EF74
    [Nullable(1)]
    public string Title
    {
      [NullableContext(1)]
      get
      {
        string text;
        switch (this.Step)
        {
        case PreSignInIntroStep.WhatIsClicky:
          text = "Welcome to HeyClicky.";
          break;
        case PreSignInIntroStep.WhereClickyLives:
          text = "I live in your notch now.";
          break;
        case PreSignInIntroStep.ClickyGetsLost:
          text = "Sometimes HeyClicky gets lost";
          break;
        case PreSignInIntroStep.WhereHeardAbout:
          text = "How’d you find this?";
          break;
        case PreSignInIntroStep.OpenClicky:
          text = "Pick a color";
          break;
        case PreSignInIntroStep.Hatched:
          text = (string.IsNullOrWhiteSpace(this.UserFirstName) ? "Hi, friend." : ("Hi, " + this.UserFirstName + "."));
          break;
        case PreSignInIntroStep.SkillSelection:
          text = "What should HeyClicky be good at?";
          break;
        default:
          text = "You're all set!";
          break;
        }
        return text;
      }
    }

    // Token: 0x17000382 RID: 898
    // (get) Token: 0x06000F82 RID: 3970 RVA: 0x0038F010 File Offset: 0x0038F010
    public string Body
    {
      get
      {
        switch (this.Step)
        {
        case PreSignInIntroStep.WhatIsClicky:
          return "A tiny AI buddy that lives on your PC.";
        case PreSignInIntroStep.WhereClickyLives:
          return "Try it out! Hover the notch at the top of your screen and you'll see me in my new home.";
        case PreSignInIntroStep.WhereHeardAbout:
          return "We're a tiny team, and knowing where you heard about us helps a ton.";
        case PreSignInIntroStep.OpenClicky:
          return "Click one to bring me to life. Choose wisely.";
        case PreSignInIntroStep.Hatched:
          return "I'm HeyClicky.";
        }
        return null;
      }
    }

    // Token: 0x17000383 RID: 899
    // (get) Token: 0x06000F83 RID: 3971 RVA: 0x0038F070 File Offset: 0x0038F070
    public bool CanGoBack
    {
      get
      {
        return PreSignInIntroViewModel.PreviousStep(this.Step) != null;
      }
    }

    // Token: 0x06000F84 RID: 3972 RVA: 0x0038F098 File Offset: 0x0038F098
    public void ContinueFromWelcome()
    {
      if (this.Step != PreSignInIntroStep.WhatIsClicky)
      {
        return;
      }
      if (this.IsSignedIn)
      {
        this.Step = PreSignInIntroStep.WhereHeardAbout;
        return;
      }
      EventHandler signInRequested = this.SignInRequested;
      if (signInRequested == null)
      {
        return;
      }
      signInRequested(this, EventArgs.Empty);
    }

    // Token: 0x06000F85 RID: 3973 RVA: 0x0038F0CC File Offset: 0x0038F0CC
    [NullableContext(1)]
    public async Task PickDiscoveryChannelAsync(string channelId)
    {
      this.SelectedDiscoveryChannel = channelId;
      if (channelId == "other")
      {
        this.IsOtherDiscoveryFieldVisible = true;
      }
      else
      {
        this.IsOtherDiscoveryFieldVisible = false;
        await this.SaveDiscoveryChannelAsync(channelId, null).ConfigureAwait(true);
        await Task.Delay(140).ConfigureAwait(true);
        if (this.Step == PreSignInIntroStep.WhereHeardAbout)
        {
          this.ApplyDefaultOnboardingLook();
          this.Step = PreSignInIntroStep.OpenClicky;
        }
      }
    }

    // Token: 0x06000F86 RID: 3974 RVA: 0x0038F118 File Offset: 0x0038F118
    [NullableContext(1)]
    public async Task CommitOtherDiscoveryChannelAsync()
    {
      string text = this.OtherDiscoveryText.Trim();
      if (text.Length > 280)
      {
        text = text.Substring(0, 280);
      }
      await this.SaveDiscoveryChannelAsync("other", (text.Length == 0) ? null : text).ConfigureAwait(true);
      if (this.Step == PreSignInIntroStep.WhereHeardAbout)
      {
        this.ApplyDefaultOnboardingLook();
        this.Step = PreSignInIntroStep.OpenClicky;
      }
    }

    // Token: 0x06000F87 RID: 3975 RVA: 0x0038F15C File Offset: 0x0038F15C
    [NullableContext(1)]
    private async Task SaveDiscoveryChannelAsync(string channelId, [Nullable(2)] string other)
    {
      ClickyAnalytics.TrackDiscoveryChannelSelected(channelId, other);
      this.settings.Update(delegate(AppSettings s)
      {
        s.Onboarding.DiscoveryChannel = channelId;
        s.Onboarding.DiscoveryChannelOther = other;
        s.Onboarding.DiscoveryChannelSynced = false;
      });
      if (this.IsSignedIn && this.syncDiscoveryChannel != null)
      {
        try
        {
          await this.syncDiscoveryChannel(channelId, other).ConfigureAwait(true);
          this.settings.Update(delegate(AppSettings s)
          {
            s.Onboarding.DiscoveryChannelSynced = true;
          });
        }
        catch (Exception ex)
        {
          Log.Warn("Onboarding", "discovery channel not synced yet: " + ex.Message);
        }
      }
    }

    // Token: 0x06000F88 RID: 3976 RVA: 0x0038F1B0 File Offset: 0x0038F1B0
    public void ApplyDefaultOnboardingLook()
    {
      SquadPack pack = OnboardingCatalog.DefaultSquadPack;
      this.settings.Update(delegate(AppSettings s)
      {
        s.Onboarding.SquadPackId = pack.Id;
        s.SquadMemberId = pack.Id;
        s.VoiceIdentifier = pack.VoiceIdentifier;
      });
    }

    // Token: 0x17000384 RID: 900
    // (get) Token: 0x06000F89 RID: 3977 RVA: 0x0038F1E8 File Offset: 0x0038F1E8
    public bool IsLanguageStepEnabled
    {
      get
      {
        return this.settings.Read<bool>((AppSettings s) => s.OnboardingLanguageStepEnabled);
      }
    }

    // Token: 0x17000385 RID: 901
    // (get) Token: 0x06000F8A RID: 3978 RVA: 0x0038F214 File Offset: 0x0038F214
    [Nullable(1)]
    public IReadOnlyList<string> VoiceLanguages
    {
      [NullableContext(1)]
      get
      {
        return VoiceCatalog.PickedLanguages(this.settings.Read<string>(([Nullable(1)] AppSettings s) => s.VoiceLanguageCode));
      }
    }

    // Token: 0x06000F8B RID: 3979 RVA: 0x0038F248 File Offset: 0x0038F248
    [NullableContext(1)]
    public async Task<IReadOnlyList<string>> RegionLanguagesAsync([Nullable(2)] string deviceRegion)
    {
      IReadOnlyList<string> readOnlyList;
      if (this.fetchRegionLanguages == null)
      {
        readOnlyList = Array.Empty<string>();
      }
      else
      {
        try
        {
          Task<IReadOnlyList<string>> fetch = this.fetchRegionLanguages(deviceRegion);
          ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter = Task.WhenAny(fetch, Task.Delay(4000)).ConfigureAwait(true).GetAwaiter();
          if (!configuredTaskAwaiter.IsCompleted)
          {
            await configuredTaskAwaiter;
            ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
            configuredTaskAwaiter = configuredTaskAwaiter2;
            configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
          }
          if (configuredTaskAwaiter.GetResult() != fetch)
          {
            readOnlyList = Array.Empty<string>();
          }
          else
          {
            readOnlyList = (await fetch.ConfigureAwait(true)).Where<string>((string c) => VoiceCatalog.Language(c) != null).ToList<string>();
          }
        }
        catch (Exception ex)
        {
          Log.Warn("Onboarding", "language hints: " + ex.Message);
          readOnlyList = Array.Empty<string>();
        }
      }
      return readOnlyList;
    }

    // Token: 0x06000F8C RID: 3980 RVA: 0x0038F294 File Offset: 0x0038F294
    [NullableContext(1)]
    public void PickVoiceLanguages(IReadOnlyList<string> codes)
    {
      string stored = VoiceCatalog.StoredLanguages(codes);
      bool needsNativeAccent = VoiceCatalog.PickedLanguages(stored).Any<string>((string c) => !VoiceCatalog.IsEnglish(c));
      this.settings.Update(delegate(AppSettings s)
      {
        s.VoiceLanguageCode = stored;
        s.VoiceIdentifier = (needsNativeAccent ? "gemini:Puck" : OnboardingCatalog.DefaultSquadPack.VoiceIdentifier);
      });
    }

    // Token: 0x06000F8D RID: 3981 RVA: 0x0038F300 File Offset: 0x0038F300
    [NullableContext(1)]
    public async Task PickCursorColorAsync(string hex)
    {
      this.SelectedCursorColorHex = hex;
      this.settings.Update(delegate(AppSettings s)
      {
        s.CursorColorHex = hex;
      });
      await Task.Delay(120).ConfigureAwait(true);
      if (this.Step == PreSignInIntroStep.OpenClicky)
      {
        this.Step = PreSignInIntroStep.Hatched;
      }
    }

    // Token: 0x06000F8E RID: 3982 RVA: 0x0038F34C File Offset: 0x0038F34C
    public void GiveClickyAHome()
    {
      if (this.Step != PreSignInIntroStep.Hatched)
      {
        return;
      }
      EventHandler handOffToNotchRequested = this.HandOffToNotchRequested;
      if (handOffToNotchRequested == null)
      {
        return;
      }
      handOffToNotchRequested(this, EventArgs.Empty);
    }

    // Token: 0x06000F8F RID: 3983 RVA: 0x0038F370 File Offset: 0x0038F370
    public void Back()
    {
      PreSignInIntroStep? preSignInIntroStep = PreSignInIntroViewModel.PreviousStep(this.Step);
      if (preSignInIntroStep != null)
      {
        PreSignInIntroStep valueOrDefault = preSignInIntroStep.GetValueOrDefault();
        this.Step = valueOrDefault;
      }
    }

    // Token: 0x06000F90 RID: 3984 RVA: 0x0038F3A4 File Offset: 0x0038F3A4
    private static PreSignInIntroStep? PreviousStep(PreSignInIntroStep step)
    {
      switch (step)
      {
      case PreSignInIntroStep.WhereHeardAbout:
        return new PreSignInIntroStep?(PreSignInIntroStep.WhatIsClicky);
      case PreSignInIntroStep.OpenClicky:
        return new PreSignInIntroStep?(PreSignInIntroStep.WhereHeardAbout);
      case PreSignInIntroStep.SkillSelection:
        return new PreSignInIntroStep?(PreSignInIntroStep.OpenClicky);
      case PreSignInIntroStep.ReadyToStart:
        return new PreSignInIntroStep?(PreSignInIntroStep.OpenClicky);
      }
      return null;
    }

    // Token: 0x06000F91 RID: 3985 RVA: 0x0038F400 File Offset: 0x0038F400
    public void Dismiss()
    {
      if (!this.settings.Read<bool>((AppSettings s) => s.Onboarding.PreSignInIntroDismissed))
      {
        ClickyAnalytics.TrackPreSignInIntroDismissed();
      }
      this.settings.Update(delegate(AppSettings s)
      {
        s.Onboarding.PreSignInIntroDismissed = true;
      });
      EventHandler dismissed = this.Dismissed;
      if (dismissed == null)
      {
        return;
      }
      dismissed(this, EventArgs.Empty);
    }

    // Token: 0x06000F92 RID: 3986 RVA: 0x0038F480 File Offset: 0x0038F480
    public void Reset()
    {
      this.Step = PreSignInIntroStep.WhatIsClicky;
      this.IsOtherDiscoveryFieldVisible = false;
      this.OtherDiscoveryText = "";
    }

    // Token: 0x06000F93 RID: 3987 RVA: 0x0038F49C File Offset: 0x0038F49C
    [NullableContext(1)]
    public static async Task FlushPendingDiscoveryChannelAsync(SettingsStore settings, [Nullable(new byte[] { 1, 1, 2, 1 })] Func<string, string, Task> syncDiscoveryChannel)
    {
      ValueTuple<string, string, bool> valueTuple = settings.Read<ValueTuple<string, string, bool>>(([Nullable(1)] AppSettings s) => new ValueTuple<string, string, bool>(s.Onboarding.DiscoveryChannel, s.Onboarding.DiscoveryChannelOther, s.Onboarding.DiscoveryChannelSynced));
      if (valueTuple.Item1 != null && !valueTuple.Item3)
      {
        try
        {
          await syncDiscoveryChannel(valueTuple.Item1, valueTuple.Item2).ConfigureAwait(false);
          settings.Update(delegate(AppSettings s)
          {
            s.Onboarding.DiscoveryChannelSynced = true;
          });
        }
        catch (Exception ex)
        {
          Log.Warn("Onboarding", "discovery channel sync failed: " + ex.Message);
        }
      }
    }

    // Token: 0x17000386 RID: 902
    // (get) Token: 0x06000F94 RID: 3988 RVA: 0x0038F4E8 File Offset: 0x0038F4E8
    // (set) Token: 0x06000F95 RID: 3989 RVA: 0x0038F4F0 File Offset: 0x0038F4F0
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public PreSignInIntroStep Step
    {
      get
      {
        return this.step;
      }
      set
      {
        if (!EqualityComparer<PreSignInIntroStep>.Default.Equals(this.step, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Step);
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Title);
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Body);
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.CanGoBack);
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.AnalyticsName);
          this.step = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Step);
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Title);
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Body);
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.CanGoBack);
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.AnalyticsName);
        }
      }
    }

    // Token: 0x17000387 RID: 903
    // (get) Token: 0x06000F96 RID: 3990 RVA: 0x0038F588 File Offset: 0x0038F588
    // (set) Token: 0x06000F97 RID: 3991 RVA: 0x0038F590 File Offset: 0x0038F590
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public bool IsSignedIn
    {
      get
      {
        return this.isSignedIn;
      }
      set
      {
        if (!EqualityComparer<bool>.Default.Equals(this.isSignedIn, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsSignedIn);
          this.isSignedIn = value;
          this.OnIsSignedInChanged(value);
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsSignedIn);
        }
      }
    }

    // Token: 0x17000388 RID: 904
    // (get) Token: 0x06000F98 RID: 3992 RVA: 0x0038F5CC File Offset: 0x0038F5CC
    // (set) Token: 0x06000F99 RID: 3993 RVA: 0x0038F5D4 File Offset: 0x0038F5D4
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public string UserFirstName
    {
      get
      {
        return this.userFirstName;
      }
      set
      {
        if (!EqualityComparer<string>.Default.Equals(this.userFirstName, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.UserFirstName);
          this.userFirstName = value;
          this.OnUserFirstNameChanged(value);
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.UserFirstName);
        }
      }
    }

    // Token: 0x17000389 RID: 905
    // (get) Token: 0x06000F9A RID: 3994 RVA: 0x0038F610 File Offset: 0x0038F610
    // (set) Token: 0x06000F9B RID: 3995 RVA: 0x0038F618 File Offset: 0x0038F618
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public string SelectedDiscoveryChannel
    {
      get
      {
        return this.selectedDiscoveryChannel;
      }
      set
      {
        if (!EqualityComparer<string>.Default.Equals(this.selectedDiscoveryChannel, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SelectedDiscoveryChannel);
          this.selectedDiscoveryChannel = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SelectedDiscoveryChannel);
        }
      }
    }

    // Token: 0x1700038A RID: 906
    // (get) Token: 0x06000F9C RID: 3996 RVA: 0x0038F64C File Offset: 0x0038F64C
    // (set) Token: 0x06000F9D RID: 3997 RVA: 0x0038F654 File Offset: 0x0038F654
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public bool IsOtherDiscoveryFieldVisible
    {
      get
      {
        return this.isOtherDiscoveryFieldVisible;
      }
      set
      {
        if (!EqualityComparer<bool>.Default.Equals(this.isOtherDiscoveryFieldVisible, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsOtherDiscoveryFieldVisible);
          this.isOtherDiscoveryFieldVisible = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsOtherDiscoveryFieldVisible);
        }
      }
    }

    // Token: 0x1700038B RID: 907
    // (get) Token: 0x06000F9E RID: 3998 RVA: 0x0038F688 File Offset: 0x0038F688
    // (set) Token: 0x06000F9F RID: 3999 RVA: 0x0038F690 File Offset: 0x0038F690
    [Nullable(1)]
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public string OtherDiscoveryText
    {
      [NullableContext(1)]
      get
      {
        return this.otherDiscoveryText;
      }
      [NullableContext(1)]
      [MemberNotNull("otherDiscoveryText")]
      set
      {
        if (!EqualityComparer<string>.Default.Equals(this.otherDiscoveryText, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.OtherDiscoveryText);
          this.otherDiscoveryText = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.OtherDiscoveryText);
        }
      }
    }

    // Token: 0x1700038C RID: 908
    // (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0038F6C4 File Offset: 0x0038F6C4
    // (set) Token: 0x06000FA1 RID: 4001 RVA: 0x0038F6CC File Offset: 0x0038F6CC
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public string SelectedCursorColorHex
    {
      get
      {
        return this.selectedCursorColorHex;
      }
      set
      {
        if (!EqualityComparer<string>.Default.Equals(this.selectedCursorColorHex, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SelectedCursorColorHex);
          this.selectedCursorColorHex = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SelectedCursorColorHex);
        }
      }
    }

    // Token: 0x06000FA2 RID: 4002 RVA: 0x0038F700 File Offset: 0x0038F700
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    private void OnIsSignedInChanged(bool value)
    {
      if (value && this.Step == PreSignInIntroStep.WhatIsClicky)
      {
        this.Step = PreSignInIntroStep.WhereHeardAbout;
      }
    }

    // Token: 0x06000FA3 RID: 4003 RVA: 0x0038F714 File Offset: 0x0038F714
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    private void OnUserFirstNameChanged(string value)
    {
      base.OnPropertyChanged("Title");
    }

    // Token: 0x04000698 RID: 1688
    [Nullable(1)]
    private readonly SettingsStore settings;

    // Token: 0x04000699 RID: 1689
    [Nullable(new byte[] { 2, 1, 2, 1 })]
    private readonly Func<string, string, Task> syncDiscoveryChannel;

    // Token: 0x0400069A RID: 1690
    [ObservableProperty]
    [NotifyPropertyChangedFor("Title", new string[] { "Body", "CanGoBack", "AnalyticsName" })]
    private PreSignInIntroStep step;

    // Token: 0x0400069B RID: 1691
    [ObservableProperty]
    private bool isSignedIn;

    // Token: 0x0400069C RID: 1692
    [ObservableProperty]
    private string userFirstName;

    // Token: 0x0400069D RID: 1693
    [ObservableProperty]
    private string selectedDiscoveryChannel;

    // Token: 0x0400069E RID: 1694
    [ObservableProperty]
    private bool isOtherDiscoveryFieldVisible;

    // Token: 0x0400069F RID: 1695
    [Nullable(1)]
    [ObservableProperty]
    private string otherDiscoveryText = "";

    // Token: 0x040006A0 RID: 1696
    [ObservableProperty]
    private string selectedCursorColorHex;

    // Token: 0x040006A4 RID: 1700
    [Nullable(new byte[] { 2, 2, 1, 1, 1 })]
    private readonly Func<string, Task<IReadOnlyList<string>>> fetchRegionLanguages;
  }
}
