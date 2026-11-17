using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.Core.Onboarding
{
  // Token: 0x020000F6 RID: 246
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class NotchOnboardingCardViewModel : ObservableObject
  {
    // Token: 0x1400001A RID: 26
    // (add) Token: 0x06000FA4 RID: 4004 RVA: 0x0038F724 File Offset: 0x0038F724
    // (remove) Token: 0x06000FA5 RID: 4005 RVA: 0x0038F75C File Offset: 0x0038F75C
    public event EventHandler OnboardingTriggered;

    // Token: 0x1400001B RID: 27
    // (add) Token: 0x06000FA6 RID: 4006 RVA: 0x0038F794 File Offset: 0x0038F794
    // (remove) Token: 0x06000FA7 RID: 4007 RVA: 0x0038F7CC File Offset: 0x0038F7CC
    public event EventHandler SignInRequested;

    // Token: 0x1400001C RID: 28
    // (add) Token: 0x06000FA8 RID: 4008 RVA: 0x0038F804 File Offset: 0x0038F804
    // (remove) Token: 0x06000FA9 RID: 4009 RVA: 0x0038F83C File Offset: 0x0038F83C
    public event EventHandler IntroWelcomeFinished;

    // Token: 0x06000FAA RID: 4010 RVA: 0x0038F874 File Offset: 0x0038F874
    [NullableContext(1)]
    public NotchOnboardingCardViewModel(SettingsStore settings, IPermissionService permissions)
    {
      this.settings = settings;
      this.permissions = permissions;
      foreach (PermissionKind permissionKind in NotchOnboardingCardViewModel.PermissionOrder)
      {
        this.lastPermissionStatus[permissionKind] = permissions.GetStatus(permissionKind);
      }
      permissions.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.TrackPermissionGrants();
        this.Refresh();
      };
      settings.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.Refresh();
      };
      this.Refresh();
    }

    // Token: 0x06000FAB RID: 4011 RVA: 0x0038F928 File Offset: 0x0038F928
    private void TrackPermissionGrants()
    {
      foreach (PermissionKind permissionKind in NotchOnboardingCardViewModel.PermissionOrder)
      {
        PermissionStatus status = this.permissions.GetStatus(permissionKind);
        object obj = this.funnelGate;
        bool flag2;
        lock (obj)
        {
          int valueOrDefault = (int)this.lastPermissionStatus.GetValueOrDefault(permissionKind);
          this.lastPermissionStatus[permissionKind] = status;
          flag2 = valueOrDefault != 5 && status == PermissionStatus.Granted;
        }
        if (flag2)
        {
          ClickyAnalytics.TrackPermissionGranted(permissionKind, null);
          if (this.Kind == NotchOnboardingCardKind.Permission)
          {
            PermissionKind? permissionKind2 = this.ActivePermission;
            PermissionKind permissionKind3 = permissionKind;
            if ((permissionKind2.GetValueOrDefault() == permissionKind3) & (permissionKind2 != null))
            {
              this.BeginGrantedCelebration(permissionKind);
            }
          }
        }
      }
    }

    // Token: 0x1700038D RID: 909
    // (get) Token: 0x06000FAC RID: 4012 RVA: 0x0038F9F4 File Offset: 0x0038F9F4
    // (set) Token: 0x06000FAD RID: 4013 RVA: 0x0038F9FC File Offset: 0x0038F9FC
    public TimeSpan GrantedSuccessHold { get; set; } = TimeSpan.FromSeconds(1.2);

    // Token: 0x1700038E RID: 910
    // (get) Token: 0x06000FAE RID: 4014 RVA: 0x0038FA08 File Offset: 0x0038FA08
    public PermissionKind? CelebratingPermission
    {
      get
      {
        object obj = this.funnelGate;
        PermissionKind? permissionKind;
        lock (obj)
        {
          permissionKind = this.celebratingPermission;
        }
        return permissionKind;
      }
    }

    // Token: 0x06000FAF RID: 4015 RVA: 0x0038FA4C File Offset: 0x0038FA4C
    private void BeginGrantedCelebration(PermissionKind permission)
    {
      if (this.IsSignedIn && !(this.GrantedSuccessHold <= TimeSpan.Zero))
      {
        if (!this.settings.Read<bool>((AppSettings s) => s.Onboarding.HasCompletedOnboarding))
        {
          CancellationTokenSource cancellation = new CancellationTokenSource();
          object obj = this.funnelGate;
          lock (obj)
          {
            CancellationTokenSource cancellationTokenSource = this.celebrationCancellation;
            if (cancellationTokenSource != null)
            {
              cancellationTokenSource.Cancel();
            }
            this.celebrationCancellation = cancellation;
            this.celebratingPermission = new PermissionKind?(permission);
          }
          string text = "Onboarding";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 2);
          defaultInterpolatedStringHandler.AppendFormatted<PermissionKind>(permission);
          defaultInterpolatedStringHandler.AppendLiteral(" granted: holding its success state for ");
          defaultInterpolatedStringHandler.AppendFormatted<double>(this.GrantedSuccessHold.TotalSeconds, "0.0");
          defaultInterpolatedStringHandler.AppendLiteral(" s");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
          Task.Delay(this.GrantedSuccessHold, cancellation.Token).ContinueWith(delegate(Task t)
          {
            if (t.IsCanceled)
            {
              return;
            }
            object obj2 = this.funnelGate;
            lock (obj2)
            {
              if (this.celebrationCancellation != cancellation)
              {
                return;
              }
              this.celebrationCancellation = null;
              this.celebratingPermission = null;
            }
            this.Refresh();
          }, TaskScheduler.Default);
          return;
        }
      }
    }

    // Token: 0x1700038F RID: 911
    // (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0038FB94 File Offset: 0x0038FB94
    public int TotalPermissionSteps
    {
      get
      {
        return NotchOnboardingCardViewModel.PermissionOrder.Length;
      }
    }

    // Token: 0x17000390 RID: 912
    // (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0038FBA0 File Offset: 0x0038FBA0
    public bool NeedsPin
    {
      get
      {
        return this.Kind != NotchOnboardingCardKind.Ready;
      }
    }

    // Token: 0x17000391 RID: 913
    // (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0038FBB0 File Offset: 0x0038FBB0
    public bool CanAdvancePastPermissionsStep
    {
      get
      {
        return NotchOnboardingCardViewModel.PermissionOrder.All<PermissionKind>((PermissionKind p) => this.permissions.GetStatus(p) == PermissionStatus.Granted);
      }
    }

    // Token: 0x17000392 RID: 914
    // (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0038FBC8 File Offset: 0x0038FBC8
    public bool WelcomeMeetsClicky
    {
      get
      {
        return !this.IsUpgradingUser && this.CanAdvancePastPermissionsStep;
      }
    }

    // Token: 0x17000393 RID: 915
    // (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0038FBDC File Offset: 0x0038FBDC
    public bool WelcomeOpensMicrophoneSettings
    {
      get
      {
        bool flag = !this.CanAdvancePastPermissionsStep;
        if (flag)
        {
          PermissionStatus status = this.permissions.GetStatus(PermissionKind.Microphone);
          bool flag2 = status == PermissionStatus.WaitingInSettings || status == PermissionStatus.Denied;
          flag = flag2;
        }
        return flag && this.permissions.GetStatus(PermissionKind.ScreenCapture) == PermissionStatus.Granted;
      }
    }

    // Token: 0x06000FB5 RID: 4021 RVA: 0x0038FC28 File Offset: 0x0038FC28
    [return: TupleElementNames(new string[] { "Title", "Subtitle", "Action" })]
    [return: Nullable(new byte[] { 0, 1, 1, 1 })]
    public static ValueTuple<string, string, string> MicrophoneOffCopy(MicrophoneBlock block)
    {
      ValueTuple<string, string, string> valueTuple;
      switch (block)
      {
      case MicrophoneBlock.Policy:
        valueTuple = new ValueTuple<string, string, string>("Your microphone is turned off.", "Your organization turned it off for apps. Ask your IT team to allow it, then come back.", "Open Settings");
        break;
      case MicrophoneBlock.Device:
        valueTuple = new ValueTuple<string, string, string>("Your microphone is turned off.", "Turn on “Microphone access” in Settings, then come back here.", "Open Settings");
        break;
      case MicrophoneBlock.AllApps:
        valueTuple = new ValueTuple<string, string, string>("Your microphone is turned off.", "Turn on “Let apps access your microphone” in Settings, then come back here.", "Open Settings");
        break;
      case MicrophoneBlock.DesktopApps:
        valueTuple = new ValueTuple<string, string, string>("Your microphone is turned off.", "Turn on “Let desktop apps access your microphone” in Settings, then come back here.", "Open Settings");
        break;
      default:
        valueTuple = new ValueTuple<string, string, string>("I can't hear you yet.", "Turn on microphone access for HeyClicky in Settings, then come back here.", "Open Settings");
        break;
      }
      return valueTuple;
    }

    // Token: 0x06000FB6 RID: 4022 RVA: 0x0038FCC4 File Offset: 0x0038FCC4
    public void Refresh()
    {
      ValueTuple<bool, bool> valueTuple = this.settings.Read<ValueTuple<bool, bool>>(([Nullable(1)] AppSettings s) => new ValueTuple<bool, bool>(s.Onboarding.HasCompletedOnboarding, s.Onboarding.HasAcknowledgedPermissionIntro));
      string text = (valueTuple.Item1 ? "recovery" : "firstRun");
      bool canAdvancePastPermissionsStep = this.CanAdvancePastPermissionsStep;
      object obj = this.funnelGate;
      bool flag4;
      lock (obj)
      {
        bool? flag2 = this.hadAllPermissions;
        bool flag3 = false;
        flag4 = ((flag2.GetValueOrDefault() == flag3) & (flag2 != null)) && canAdvancePastPermissionsStep;
        this.hadAllPermissions = new bool?(canAdvancePastPermissionsStep);
        if (!this.IsSignedIn || canAdvancePastPermissionsStep)
        {
          this.permissionFlowShownTracked = false;
          this.lastTrackedPermissionStep = null;
        }
      }
      if (flag4)
      {
        ClickyAnalytics.TrackCorePermissionsGranted();
        ClickyAnalytics.TrackAllPermissionsGranted();
        ClickyAnalytics.TrackPermissionFlowCompleted(text);
      }
      this.ActivePermission = null;
      if (!this.IsSignedIn)
      {
        this.Set(NotchOnboardingCardKind.SignedOut, "Sign in to get started", "Sees your screen, talks back, runs your apps.", "Sign in with Google");
      }
      else if (!valueTuple.Item1 && !valueTuple.Item2)
      {
        if (this.IsUpgradingUser && this.CanAdvancePastPermissionsStep)
        {
          this.Set(NotchOnboardingCardKind.PermissionIntro, "I live in your notch now.", null, "Got it");
        }
        else if (this.WelcomeMeetsClicky)
        {
          this.Set(NotchOnboardingCardKind.PermissionIntro, "I live in your notch now.", "Turn your sound on and tap the button below.", "Meet HeyClicky");
        }
        else if (this.WelcomeOpensMicrophoneSettings)
        {
          ValueTuple<string, string, string> valueTuple2 = NotchOnboardingCardViewModel.MicrophoneOffCopy(this.permissions.MicrophoneBlockedBy);
          string item = valueTuple2.Item1;
          string item2 = valueTuple2.Item2;
          string item3 = valueTuple2.Item3;
          this.Set(NotchOnboardingCardKind.PermissionIntro, item, item2, item3);
          obj = this.funnelGate;
          bool flag5;
          lock (obj)
          {
            flag5 = !this.permissionFlowShownTracked;
            this.permissionFlowShownTracked = true;
            this.lastTrackedPermissionStep = new PermissionKind?(PermissionKind.Microphone);
          }
          if (flag5)
          {
            ClickyAnalytics.TrackPermissionFlowShown(text);
            ClickyAnalytics.TrackPermissionStepShown(PermissionKind.Microphone);
          }
        }
        else
        {
          this.Set(NotchOnboardingCardKind.PermissionIntro, "I live in your notch now.", "Need permissions to get started.", "Let's start");
        }
      }
      else
      {
        PermissionKind? permissionKind = this.CelebratingPermission;
        if (permissionKind != null)
        {
          PermissionKind valueOrDefault = permissionKind.GetValueOrDefault();
          if (!valueTuple.Item1)
          {
            this.ActivePermission = new PermissionKind?(valueOrDefault);
            this.ActivePermissionStatus = PermissionStatus.Granted;
            this.ProgressIndex = Array.IndexOf<PermissionKind>(NotchOnboardingCardViewModel.PermissionOrder, valueOrDefault) + 1;
            this.Set(NotchOnboardingCardKind.Permission, "Got it!", ((valueOrDefault == PermissionKind.Microphone) ? "Microphone" : "Screen capture") + " is ready.", "Granted");
            this.IsPrimaryActionEnabled = false;
            base.OnPropertyChanged("NeedsPin");
            return;
          }
        }
        if (!this.CanAdvancePastPermissionsStep)
        {
          int num = Array.FindIndex<PermissionKind>(NotchOnboardingCardViewModel.PermissionOrder, (PermissionKind p) => this.permissions.GetStatus(p) != PermissionStatus.Granted);
          PermissionKind permissionKind2 = NotchOnboardingCardViewModel.PermissionOrder[num];
          PermissionStatus status = this.permissions.GetStatus(permissionKind2);
          this.ActivePermission = new PermissionKind?(permissionKind2);
          this.ActivePermissionStatus = status;
          this.ProgressIndex = num + 1;
          obj = this.funnelGate;
          bool flag6;
          bool flag7;
          lock (obj)
          {
            flag6 = !this.permissionFlowShownTracked;
            this.permissionFlowShownTracked = true;
            permissionKind = this.lastTrackedPermissionStep;
            PermissionKind permissionKind3 = permissionKind2;
            flag7 = !((permissionKind.GetValueOrDefault() == permissionKind3) & (permissionKind != null));
            this.lastTrackedPermissionStep = new PermissionKind?(permissionKind2);
          }
          if (flag6)
          {
            ClickyAnalytics.TrackPermissionFlowShown(text);
          }
          if (flag7)
          {
            ClickyAnalytics.TrackPermissionStepShown(permissionKind2);
          }
          string text2;
          string text3;
          if (permissionKind2 != PermissionKind.Microphone)
          {
            text2 = "Let me see your screen";
            text3 = "I look at your screen when you ask, so I can point at things and draw on it.";
          }
          else
          {
            text2 = "Let me hear you";
            text3 = "Hold Ctrl+Alt and talk: I need your microphone for that.";
          }
          string text4 = text2;
          string text5 = text3;
          string text6;
          switch (status)
          {
          case PermissionStatus.WaitingInSettings:
          case PermissionStatus.Denied:
            text6 = "Open Settings";
            goto IL_03CD;
          case PermissionStatus.Granted:
            text6 = "Granted";
            goto IL_03CD;
          }
          text6 = "Allow";
          IL_03CD:
          this.Set(NotchOnboardingCardKind.Permission, text4, text5, text6);
          bool flag;
          switch (status)
          {
          case PermissionStatus.Prompting:
          case PermissionStatus.WaitingForPicker:
          case PermissionStatus.Granted:
            flag = true;
            goto IL_0401;
          }
          flag = false;
          IL_0401:
          this.IsPrimaryActionEnabled = !flag;
          base.OnPropertyChanged("NeedsPin");
          return;
        }
        if (!valueTuple.Item1)
        {
          this.Set(NotchOnboardingCardKind.MeetClicky, "All set.", "Turn your sound on and tap the button below.", "Meet HeyClicky");
        }
        else
        {
          this.Set(NotchOnboardingCardKind.Ready, "", null, null);
        }
      }
      this.IsPrimaryActionEnabled = true;
      base.OnPropertyChanged("NeedsPin");
    }

    // Token: 0x06000FB7 RID: 4023 RVA: 0x00390154 File Offset: 0x00390154
    private void Set(NotchOnboardingCardKind cardKind, [Nullable(1)] string cardTitle, string cardSubtitle, string action)
    {
      this.Kind = cardKind;
      this.Title = cardTitle;
      this.Subtitle = cardSubtitle;
      this.PrimaryActionLabel = action;
    }

    // Token: 0x06000FB8 RID: 4024 RVA: 0x00390174 File Offset: 0x00390174
    [NullableContext(1)]
    public async Task PrimaryActionAsync()
    {
      switch (this.Kind)
      {
      case NotchOnboardingCardKind.SignedOut:
      {
        EventHandler signInRequested = this.SignInRequested;
        if (signInRequested != null)
        {
          signInRequested(this, EventArgs.Empty);
        }
        break;
      }
      case NotchOnboardingCardKind.PermissionIntro:
      {
        bool welcomeOpensMicrophoneSettings = this.WelcomeOpensMicrophoneSettings;
        if (!welcomeOpensMicrophoneSettings)
        {
          this.settings.Update(delegate(AppSettings s)
          {
            s.Onboarding.HasAcknowledgedPermissionIntro = true;
          });
        }
        if (this.IsIntroHomeWelcomeActive)
        {
          ClickyAnalytics.TrackPreSignInIntroNotchFinalSetupTapped();
          if (!this.settings.Read<bool>((AppSettings s) => s.Onboarding.PreSignInIntroDismissed))
          {
            ClickyAnalytics.TrackPreSignInIntroDismissed();
          }
          this.settings.Update(delegate(AppSettings s)
          {
            s.Onboarding.PreSignInIntroDismissed = true;
          });
          this.IsIntroHomeWelcomeActive = false;
          EventHandler introWelcomeFinished = this.IntroWelcomeFinished;
          if (introWelcomeFinished != null)
          {
            introWelcomeFinished(this, EventArgs.Empty);
          }
        }
        if (welcomeOpensMicrophoneSettings)
        {
          this.permissions.OpenSystemSettings(PermissionKind.Microphone);
          ClickyAnalytics.TrackPermissionSettingsOpened(PermissionKind.Microphone);
          this.Refresh();
        }
        else if (this.CanAdvancePastPermissionsStep)
        {
          this.TriggerOnboarding();
        }
        else
        {
          this.Refresh();
        }
        break;
      }
      case NotchOnboardingCardKind.Permission:
      {
        PermissionKind? permissionKind = this.ActivePermission;
        if (permissionKind != null)
        {
          PermissionKind permission = permissionKind.GetValueOrDefault();
          if (this.CelebratingPermission == null)
          {
            PermissionStatus permissionStatus = this.ActivePermissionStatus;
            bool flag = permissionStatus == PermissionStatus.WaitingInSettings || permissionStatus == PermissionStatus.Denied;
            if (flag)
            {
              this.permissions.OpenSystemSettings(permission);
              ClickyAnalytics.TrackPermissionSettingsOpened(permission);
            }
            else
            {
              PermissionStatus permissionStatus2 = await this.permissions.RequestAsync(permission).ConfigureAwait(true);
              if (permissionStatus2 == PermissionStatus.WaitingInSettings)
              {
                ClickyAnalytics.TrackPermissionSettingsOpened(permission);
              }
              else if (permissionStatus2 == PermissionStatus.Prompting)
              {
                ClickyAnalytics.TrackPermissionNativePromptTriggered(permission);
              }
              else if (permissionStatus2 == PermissionStatus.Denied && permission == PermissionKind.Microphone)
              {
                ClickyAnalytics.TrackPermissionDenied(permission, null);
              }
            }
            this.Refresh();
          }
        }
        break;
      }
      case NotchOnboardingCardKind.MeetClicky:
        this.TriggerOnboarding();
        break;
      }
    }

    // Token: 0x06000FB9 RID: 4025 RVA: 0x003901B8 File Offset: 0x003901B8
    public void TriggerOnboarding()
    {
      ClickyAnalytics.TrackOnboardingStarted();
      this.settings.Update(delegate(AppSettings s)
      {
        s.Onboarding.HasCompletedOnboarding = true;
      });
      this.Refresh();
      EventHandler onboardingTriggered = this.OnboardingTriggered;
      if (onboardingTriggered == null)
      {
        return;
      }
      onboardingTriggered(this, EventArgs.Empty);
    }

    // Token: 0x17000394 RID: 916
    // (get) Token: 0x06000FBA RID: 4026 RVA: 0x00390210 File Offset: 0x00390210
    // (set) Token: 0x06000FBB RID: 4027 RVA: 0x00390218 File Offset: 0x00390218
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

    // Token: 0x17000395 RID: 917
    // (get) Token: 0x06000FBC RID: 4028 RVA: 0x00390254 File Offset: 0x00390254
    // (set) Token: 0x06000FBD RID: 4029 RVA: 0x0039025C File Offset: 0x0039025C
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public bool IsUpgradingUser
    {
      get
      {
        return this.isUpgradingUser;
      }
      set
      {
        if (!EqualityComparer<bool>.Default.Equals(this.isUpgradingUser, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsUpgradingUser);
          this.isUpgradingUser = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsUpgradingUser);
        }
      }
    }

    // Token: 0x17000396 RID: 918
    // (get) Token: 0x06000FBE RID: 4030 RVA: 0x00390290 File Offset: 0x00390290
    // (set) Token: 0x06000FBF RID: 4031 RVA: 0x00390298 File Offset: 0x00390298
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public NotchOnboardingCardKind Kind
    {
      get
      {
        return this.kind;
      }
      set
      {
        if (!EqualityComparer<NotchOnboardingCardKind>.Default.Equals(this.kind, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Kind);
          this.kind = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Kind);
        }
      }
    }

    // Token: 0x17000397 RID: 919
    // (get) Token: 0x06000FC0 RID: 4032 RVA: 0x003902CC File Offset: 0x003902CC
    // (set) Token: 0x06000FC1 RID: 4033 RVA: 0x003902D4 File Offset: 0x003902D4
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public PermissionKind? ActivePermission
    {
      get
      {
        return this.activePermission;
      }
      set
      {
        if (!EqualityComparer<PermissionKind?>.Default.Equals(this.activePermission, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ActivePermission);
          this.activePermission = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ActivePermission);
        }
      }
    }

    // Token: 0x17000398 RID: 920
    // (get) Token: 0x06000FC2 RID: 4034 RVA: 0x00390308 File Offset: 0x00390308
    // (set) Token: 0x06000FC3 RID: 4035 RVA: 0x00390310 File Offset: 0x00390310
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public PermissionStatus ActivePermissionStatus
    {
      get
      {
        return this.activePermissionStatus;
      }
      set
      {
        if (!EqualityComparer<PermissionStatus>.Default.Equals(this.activePermissionStatus, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ActivePermissionStatus);
          this.activePermissionStatus = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ActivePermissionStatus);
        }
      }
    }

    // Token: 0x17000399 RID: 921
    // (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00390344 File Offset: 0x00390344
    // (set) Token: 0x06000FC5 RID: 4037 RVA: 0x0039034C File Offset: 0x0039034C
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public int ProgressIndex
    {
      get
      {
        return this.progressIndex;
      }
      set
      {
        if (!EqualityComparer<int>.Default.Equals(this.progressIndex, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ProgressIndex);
          this.progressIndex = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ProgressIndex);
        }
      }
    }

    // Token: 0x1700039A RID: 922
    // (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00390380 File Offset: 0x00390380
    // (set) Token: 0x06000FC7 RID: 4039 RVA: 0x00390388 File Offset: 0x00390388
    [Nullable(1)]
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public string Title
    {
      [NullableContext(1)]
      get
      {
        return this.title;
      }
      [NullableContext(1)]
      [MemberNotNull("title")]
      set
      {
        if (!EqualityComparer<string>.Default.Equals(this.title, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Title);
          this.title = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Title);
        }
      }
    }

    // Token: 0x1700039B RID: 923
    // (get) Token: 0x06000FC8 RID: 4040 RVA: 0x003903BC File Offset: 0x003903BC
    // (set) Token: 0x06000FC9 RID: 4041 RVA: 0x003903C4 File Offset: 0x003903C4
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public string Subtitle
    {
      get
      {
        return this.subtitle;
      }
      set
      {
        if (!EqualityComparer<string>.Default.Equals(this.subtitle, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Subtitle);
          this.subtitle = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Subtitle);
        }
      }
    }

    // Token: 0x1700039C RID: 924
    // (get) Token: 0x06000FCA RID: 4042 RVA: 0x003903F8 File Offset: 0x003903F8
    // (set) Token: 0x06000FCB RID: 4043 RVA: 0x00390400 File Offset: 0x00390400
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public string PrimaryActionLabel
    {
      get
      {
        return this.primaryActionLabel;
      }
      set
      {
        if (!EqualityComparer<string>.Default.Equals(this.primaryActionLabel, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.PrimaryActionLabel);
          this.primaryActionLabel = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.PrimaryActionLabel);
        }
      }
    }

    // Token: 0x1700039D RID: 925
    // (get) Token: 0x06000FCC RID: 4044 RVA: 0x00390434 File Offset: 0x00390434
    // (set) Token: 0x06000FCD RID: 4045 RVA: 0x0039043C File Offset: 0x0039043C
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public bool IsPrimaryActionEnabled
    {
      get
      {
        return this.isPrimaryActionEnabled;
      }
      set
      {
        if (!EqualityComparer<bool>.Default.Equals(this.isPrimaryActionEnabled, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsPrimaryActionEnabled);
          this.isPrimaryActionEnabled = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsPrimaryActionEnabled);
        }
      }
    }

    // Token: 0x1700039E RID: 926
    // (get) Token: 0x06000FCE RID: 4046 RVA: 0x00390470 File Offset: 0x00390470
    // (set) Token: 0x06000FCF RID: 4047 RVA: 0x00390478 File Offset: 0x00390478
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public bool IsIntroHomeWelcomeActive
    {
      get
      {
        return this.isIntroHomeWelcomeActive;
      }
      set
      {
        if (!EqualityComparer<bool>.Default.Equals(this.isIntroHomeWelcomeActive, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsIntroHomeWelcomeActive);
          this.isIntroHomeWelcomeActive = value;
          this.OnIsIntroHomeWelcomeActiveChanged(value);
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsIntroHomeWelcomeActive);
        }
      }
    }

    // Token: 0x06000FD0 RID: 4048 RVA: 0x003904B4 File Offset: 0x003904B4
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    private void OnIsSignedInChanged(bool value)
    {
      this.Refresh();
    }

    // Token: 0x06000FD1 RID: 4049 RVA: 0x003904BC File Offset: 0x003904BC
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    private void OnIsIntroHomeWelcomeActiveChanged(bool value)
    {
      if (value)
      {
        ClickyAnalytics.TrackPreSignInIntroNotchWelcomeShown();
      }
    }

    // Token: 0x040006AB RID: 1707
    [Nullable(1)]
    private static readonly PermissionKind[] PermissionOrder = new PermissionKind[]
    {
      PermissionKind.Microphone,
      PermissionKind.ScreenCapture
    };

    // Token: 0x040006AC RID: 1708
    [Nullable(1)]
    private readonly SettingsStore settings;

    // Token: 0x040006AD RID: 1709
    [Nullable(1)]
    private readonly IPermissionService permissions;

    // Token: 0x040006AE RID: 1710
    [ObservableProperty]
    private bool isSignedIn;

    // Token: 0x040006AF RID: 1711
    [ObservableProperty]
    private bool isUpgradingUser;

    // Token: 0x040006B0 RID: 1712
    [ObservableProperty]
    private NotchOnboardingCardKind kind;

    // Token: 0x040006B1 RID: 1713
    [ObservableProperty]
    private PermissionKind? activePermission;

    // Token: 0x040006B2 RID: 1714
    [ObservableProperty]
    private PermissionStatus activePermissionStatus;

    // Token: 0x040006B3 RID: 1715
    [ObservableProperty]
    private int progressIndex;

    // Token: 0x040006B4 RID: 1716
    [Nullable(1)]
    [ObservableProperty]
    private string title = "";

    // Token: 0x040006B5 RID: 1717
    [ObservableProperty]
    private string subtitle;

    // Token: 0x040006B6 RID: 1718
    [ObservableProperty]
    private string primaryActionLabel;

    // Token: 0x040006B7 RID: 1719
    [ObservableProperty]
    private bool isPrimaryActionEnabled = true;

    // Token: 0x040006B8 RID: 1720
    [ObservableProperty]
    private bool isIntroHomeWelcomeActive;

    // Token: 0x040006BC RID: 1724
    [Nullable(1)]
    private readonly object funnelGate = new object();

    // Token: 0x040006BD RID: 1725
    [Nullable(1)]
    private readonly Dictionary<PermissionKind, PermissionStatus> lastPermissionStatus = new Dictionary<PermissionKind, PermissionStatus>();

    // Token: 0x040006BE RID: 1726
    private bool permissionFlowShownTracked;

    // Token: 0x040006BF RID: 1727
    private PermissionKind? lastTrackedPermissionStep;

    // Token: 0x040006C0 RID: 1728
    private bool? hadAllPermissions;

    // Token: 0x040006C2 RID: 1730
    private PermissionKind? celebratingPermission;

    // Token: 0x040006C3 RID: 1731
    private CancellationTokenSource celebrationCancellation;
  }
}
