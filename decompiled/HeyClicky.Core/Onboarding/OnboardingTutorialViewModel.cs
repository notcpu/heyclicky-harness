using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using HeyClicky.Core.Settings;

namespace HeyClicky.Core.Onboarding
{
  // Token: 0x020000FA RID: 250
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class OnboardingTutorialViewModel : ObservableObject
  {
    // Token: 0x1400001D RID: 29
    // (add) Token: 0x06000FF5 RID: 4085 RVA: 0x00390ADC File Offset: 0x00390ADC
    // (remove) Token: 0x06000FF6 RID: 4086 RVA: 0x00390B14 File Offset: 0x00390B14
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<string> SuccessLine;

    // Token: 0x1400001E RID: 30
    // (add) Token: 0x06000FF7 RID: 4087 RVA: 0x00390B4C File Offset: 0x00390B4C
    // (remove) Token: 0x06000FF8 RID: 4088 RVA: 0x00390B84 File Offset: 0x00390B84
    public event EventHandler StepCompletedChime;

    // Token: 0x1400001F RID: 31
    // (add) Token: 0x06000FF9 RID: 4089 RVA: 0x00390BBC File Offset: 0x00390BBC
    // (remove) Token: 0x06000FFA RID: 4090 RVA: 0x00390BF4 File Offset: 0x00390BF4
    [TupleElementNames(new string[] { "Reason", "CursorLine" })]
    [Nullable(new byte[] { 2, 0, 1, 1 })]
    [field: TupleElementNames(new string[] { "Reason", "CursorLine" })]
    [field: Nullable(new byte[] { 2, 0, 1, 1 })]
    public event EventHandler<ValueTuple<string, string>> Ended;

    // Token: 0x14000020 RID: 32
    // (add) Token: 0x06000FFB RID: 4091 RVA: 0x00390C2C File Offset: 0x00390C2C
    // (remove) Token: 0x06000FFC RID: 4092 RVA: 0x00390C64 File Offset: 0x00390C64
    public event EventHandler StartMusicRequested;

    // Token: 0x14000021 RID: 33
    // (add) Token: 0x06000FFD RID: 4093 RVA: 0x00390C9C File Offset: 0x00390C9C
    // (remove) Token: 0x06000FFE RID: 4094 RVA: 0x00390CD4 File Offset: 0x00390CD4
    public event EventHandler CloseComposerRequested;

    // Token: 0x14000022 RID: 34
    // (add) Token: 0x06000FFF RID: 4095 RVA: 0x00390D0C File Offset: 0x00390D0C
    // (remove) Token: 0x06001000 RID: 4096 RVA: 0x00390D44 File Offset: 0x00390D44
    public event EventHandler InterviewRequested;

    // Token: 0x14000023 RID: 35
    // (add) Token: 0x06001001 RID: 4097 RVA: 0x00390D7C File Offset: 0x00390D7C
    // (remove) Token: 0x06001002 RID: 4098 RVA: 0x00390DB4 File Offset: 0x00390DB4
    public event EventHandler SetVolumeToHalfRequested;

    // Token: 0x06001003 RID: 4099 RVA: 0x00390DEC File Offset: 0x00390DEC
    [NullableContext(1)]
    public OnboardingTutorialViewModel(SettingsStore settings, [Nullable(2)] Func<double> outputVolume = null)
    {
      this.settings = settings;
      Func<double> func = outputVolume;
      if (outputVolume == null && (func = OnboardingTutorialViewModel.<>c.<>9__37_0) == null)
      {
        func = (OnboardingTutorialViewModel.<>c.<>9__37_0 = () => 1.0);
      }
      this.outputVolume = func;
    }

    // Token: 0x170003A9 RID: 937
    // (get) Token: 0x06001004 RID: 4100 RVA: 0x00390E48 File Offset: 0x00390E48
    public bool IsEngaged
    {
      get
      {
        return this.Phase > OnboardingTutorialPhase.Inactive;
      }
    }

    // Token: 0x170003AA RID: 938
    // (get) Token: 0x06001005 RID: 4101 RVA: 0x00390E54 File Offset: 0x00390E54
    public bool IsRestrictingNormalUse
    {
      get
      {
        bool flag = this.IsEngaged;
        if (flag)
        {
          OnboardingTutorialStep? onboardingTutorialStep = this.CurrentStep;
          bool flag2;
          if (onboardingTutorialStep != null)
          {
            OnboardingTutorialStep valueOrDefault = onboardingTutorialStep.GetValueOrDefault();
            if (valueOrDefault == OnboardingTutorialStep.ClickyInterview || valueOrDefault == OnboardingTutorialStep.Finale)
            {
              flag2 = true;
              goto IL_0031;
            }
          }
          flag2 = false;
          IL_0031:
          flag = !flag2;
        }
        return flag;
      }
    }

    // Token: 0x170003AB RID: 939
    // (get) Token: 0x06001006 RID: 4102 RVA: 0x00390E98 File Offset: 0x00390E98
    public bool IsCurrentStepCompleted
    {
      get
      {
        OnboardingTutorialStep? onboardingTutorialStep = this.CurrentStep;
        if (onboardingTutorialStep != null)
        {
          OnboardingTutorialStep valueOrDefault = onboardingTutorialStep.GetValueOrDefault();
          return this.completed.Contains(valueOrDefault);
        }
        return false;
      }
    }

    // Token: 0x170003AC RID: 940
    // (get) Token: 0x06001007 RID: 4103 RVA: 0x00390ECC File Offset: 0x00390ECC
    public bool CanGoBack
    {
      get
      {
        OnboardingTutorialStep? onboardingTutorialStep = this.CurrentStep;
        if (onboardingTutorialStep != null)
        {
          OnboardingTutorialStep valueOrDefault = onboardingTutorialStep.GetValueOrDefault();
          return this.PreviousStep(valueOrDefault) != null;
        }
        return false;
      }
    }

    // Token: 0x170003AD RID: 941
    // (get) Token: 0x06001008 RID: 4104 RVA: 0x00390F08 File Offset: 0x00390F08
    public OnboardingTutorialStepInfo CurrentStepInfo
    {
      get
      {
        OnboardingTutorialStep? onboardingTutorialStep = this.CurrentStep;
        if (onboardingTutorialStep != null)
        {
          OnboardingTutorialStep valueOrDefault = onboardingTutorialStep.GetValueOrDefault();
          return OnboardingTutorialViewModel.Info(valueOrDefault);
        }
        return null;
      }
    }

    // Token: 0x170003AE RID: 942
    // (get) Token: 0x06001009 RID: 4105 RVA: 0x00390F38 File Offset: 0x00390F38
    public bool HasCompletedTutorial
    {
      get
      {
        return this.settings.Read<double?>((AppSettings s) => s.Onboarding.TutorialCompletedAtEpochSeconds) != null;
      }
    }

    // Token: 0x0600100A RID: 4106 RVA: 0x00390F80 File Offset: 0x00390F80
    public bool ShouldOfferAfterVideo(bool isUpgradingUser, bool isSignedIn)
    {
      return !isUpgradingUser && isSignedIn && !this.HasCompletedTutorial;
    }

    // Token: 0x0600100B RID: 4107 RVA: 0x00390F98 File Offset: 0x00390F98
    public static bool InterceptsPushToTalkRelease(OnboardingTutorialStep step)
    {
      return step == OnboardingTutorialStep.ClickyDrive || step == OnboardingTutorialStep.EmailDraft;
    }

    // Token: 0x0600100C RID: 4108 RVA: 0x00390FB4 File Offset: 0x00390FB4
    public static bool SkipsScreenCapture(OnboardingTutorialStep step)
    {
      return step <= OnboardingTutorialStep.VoiceHello || step - OnboardingTutorialStep.ClickyDrive <= 2;
    }

    // Token: 0x0600100D RID: 4109 RVA: 0x00390FD4 File Offset: 0x00390FD4
    public bool IsRetired(OnboardingTutorialStep step)
    {
      bool flag = step == OnboardingTutorialStep.EmailDraft;
      if (!flag)
      {
        bool flag2 = this.SkipsDrawingSteps;
        if (flag2)
        {
          bool flag3 = step - OnboardingTutorialStep.DrawDemo <= 1;
          flag2 = flag3;
        }
        flag = flag2;
      }
      return flag;
    }

    // Token: 0x0600100E RID: 4110 RVA: 0x00391004 File Offset: 0x00391004
    [NullableContext(1)]
    public static OnboardingTutorialStepInfo Info(OnboardingTutorialStep step)
    {
      switch (step)
      {
      case OnboardingTutorialStep.MicCheck:
        return new OnboardingTutorialStepInfo(step, false, false, false, false, 1.6, "loud and clear ٩(◕\u203f◕)۶", "First, can I hear you?", "Say anything out loud. If the bars dance, your mic works. I hear best on built-in or wired mics: Bluetooth ones get flaky.");
      case OnboardingTutorialStep.SpeakerCheck:
        return new OnboardingTutorialStepInfo(step, false, false, false, true, 1.6, "much better. now you'll actually hear me ヽ(^o^)ノ", "Your volume is at zero", "You'd see me talking and hear nothing. One click fixes it before we start.");
      case OnboardingTutorialStep.VoiceHello:
        return new OnboardingTutorialStepInfo(step, true, false, true, false, 1.6, "nice to meet you (◠\u203f◠)", "This is how you talk to me", "Hold the keys down, say hi or tell me your name, then let go. I'm listening the whole time you hold them.");
      case OnboardingTutorialStep.ClickyInterview:
        return new OnboardingTutorialStepInfo(step, true, false, false, false, 1.6, "thanks for the chat (◕\u203f◕)", "I'm curious about you.", "Four quick questions about you, so I can make this whole thing actually useful for you.");
      case OnboardingTutorialStep.DrawDemo:
        return new OnboardingTutorialStepInfo(step, true, false, true, false, 1.6, null, "Here's how I'd help you", "I see what you see. Hold the keys, ask, and watch your screen.");
      case OnboardingTutorialStep.DrawToAsk:
        return new OnboardingTutorialStepInfo(step, true, false, true, true, 1.6, "exactly. marked and understood (¬\u203f¬)", "Circle anything, ask about it", null);
      case OnboardingTutorialStep.ClickyDrive:
        return new OnboardingTutorialStepInfo(step, true, false, false, false, 1.6, "and i can do that in any app. just ask me to do it for you (•\u0300ᴗ•\u0301)و", "I can drive your computer too", "Ask me to do it and I'll take the mouse while you watch. Move it anytime and it's yours again.");
      case OnboardingTutorialStep.TextMode:
        return new OnboardingTutorialStepInfo(step, false, true, false, false, 3.6, "found it. that box is always a double-tap away ( \u02d8▽\u02d8)", "Not in the mood to talk? Type", null);
      case OnboardingTutorialStep.Dictation:
        return new OnboardingTutorialStepInfo(step, false, false, false, true, 3.6, "that works in every text field on your PC ᕕ( ᐛ )ᕗ", "You can also dictate word-for-word", "I type exactly what you say, clean it up, and instantly put it into any box. Works in any program.");
      case OnboardingTutorialStep.Finale:
        return new OnboardingTutorialStepInfo(step, true, true, false, false, 1.6, null, "There's one more thing...", "Voice, drawing, text mode, agents: you've seen it all. But, hey, can I show you one more thing?");
      }
      return new OnboardingTutorialStepInfo(step, false, false, false, false, 1.6, null, "", null);
    }

    // Token: 0x0600100F RID: 4111 RVA: 0x003911DC File Offset: 0x003911DC
    public OnboardingTutorialStep? NextStep(OnboardingTutorialStep step)
    {
      for (int i = (int)(step + 1); i <= 10; i++)
      {
        if (!this.IsRetired((OnboardingTutorialStep)i))
        {
          return new OnboardingTutorialStep?((OnboardingTutorialStep)i);
        }
      }
      return null;
    }

    // Token: 0x06001010 RID: 4112 RVA: 0x00391214 File Offset: 0x00391214
    public OnboardingTutorialStep? PreviousStep(OnboardingTutorialStep step)
    {
      for (int i = step - OnboardingTutorialStep.SpeakerCheck; i >= 0; i--)
      {
        OnboardingTutorialStep onboardingTutorialStep = (OnboardingTutorialStep)i;
        if (!this.IsRetired(onboardingTutorialStep) && (onboardingTutorialStep != OnboardingTutorialStep.SpeakerCheck || this.DidShowSpeakerCheck))
        {
          return new OnboardingTutorialStep?(onboardingTutorialStep);
        }
      }
      return null;
    }

    // Token: 0x06001011 RID: 4113 RVA: 0x00391258 File Offset: 0x00391258
    public bool PresentInvite()
    {
      if (this.Phase != OnboardingTutorialPhase.Inactive)
      {
        return false;
      }
      this.Phase = OnboardingTutorialPhase.InvitePresented;
      return true;
    }

    // Token: 0x06001012 RID: 4114 RVA: 0x0039126C File Offset: 0x0039126C
    public void StartFromInvite()
    {
      this.completed.Clear();
      this.DidShowSpeakerCheck = false;
      this.DictationText = "";
      this.FurthestVisitedStep = 0;
      this.stepTheActiveTurnStartedOn = null;
      this.Phase = OnboardingTutorialPhase.Running;
      this.GoTo(OnboardingTutorialStep.MicCheck);
    }

    // Token: 0x06001013 RID: 4115 RVA: 0x003912AC File Offset: 0x003912AC
    public void BypassInvite()
    {
      if (this.Phase == OnboardingTutorialPhase.InvitePresented)
      {
        this.Phase = OnboardingTutorialPhase.Inactive;
      }
    }

    // Token: 0x06001014 RID: 4116 RVA: 0x003912C0 File Offset: 0x003912C0
    public void Skip()
    {
      this.End("skipped");
    }

    // Token: 0x06001015 RID: 4117 RVA: 0x003912D0 File Offset: 0x003912D0
    public void Finish()
    {
      this.End("finished");
    }

    // Token: 0x06001016 RID: 4118 RVA: 0x003912E0 File Offset: 0x003912E0
    [NullableContext(1)]
    public void End(string reason)
    {
      if (this.Phase == OnboardingTutorialPhase.Inactive)
      {
        return;
      }
      CancellationTokenSource cancellationTokenSource = this.composerCountdown;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.settings.Update(delegate(AppSettings s)
      {
        s.Onboarding.TutorialCompletedAtEpochSeconds = new double?((double)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000.0);
      });
      this.Phase = OnboardingTutorialPhase.Inactive;
      this.CurrentStep = null;
      string text = ((reason == "skipped") ? "skipped? very brave. god speed my friend. here if you need me, just press ctrl + alt" : "that's it! i'm all yours now. hold ctrl + alt and im here. learn something new, get feedback on a design, spawn a research agent, whatever!");
      EventHandler<ValueTuple<string, string>> ended = this.Ended;
      if (ended == null)
      {
        return;
      }
      ended(this, new ValueTuple<string, string>(reason, text));
    }

    // Token: 0x06001017 RID: 4119 RVA: 0x0039137C File Offset: 0x0039137C
    public void Advance()
    {
      OnboardingTutorialStep? onboardingTutorialStep = this.CurrentStep;
      if (onboardingTutorialStep == null)
      {
        return;
      }
      OnboardingTutorialStep valueOrDefault = onboardingTutorialStep.GetValueOrDefault();
      if (valueOrDefault == OnboardingTutorialStep.MicCheck)
      {
        EventHandler startMusicRequested = this.StartMusicRequested;
        if (startMusicRequested != null)
        {
          startMusicRequested(this, EventArgs.Empty);
        }
      }
      if (valueOrDefault == OnboardingTutorialStep.MicCheck)
      {
        bool flag = this.outputVolume() <= 0.02;
        this.DidShowSpeakerCheck = flag;
        this.GoTo(flag ? OnboardingTutorialStep.SpeakerCheck : OnboardingTutorialStep.VoiceHello);
        return;
      }
      onboardingTutorialStep = this.NextStep(valueOrDefault);
      if (onboardingTutorialStep != null)
      {
        OnboardingTutorialStep valueOrDefault2 = onboardingTutorialStep.GetValueOrDefault();
        this.GoTo(valueOrDefault2);
        return;
      }
      this.Finish();
    }

    // Token: 0x06001018 RID: 4120 RVA: 0x00391418 File Offset: 0x00391418
    public void Back()
    {
      OnboardingTutorialStep? onboardingTutorialStep = this.CurrentStep;
      if (onboardingTutorialStep != null)
      {
        OnboardingTutorialStep valueOrDefault = onboardingTutorialStep.GetValueOrDefault();
        onboardingTutorialStep = this.PreviousStep(valueOrDefault);
        if (onboardingTutorialStep != null)
        {
          OnboardingTutorialStep valueOrDefault2 = onboardingTutorialStep.GetValueOrDefault();
          this.GoTo(valueOrDefault2);
        }
      }
    }

    // Token: 0x06001019 RID: 4121 RVA: 0x00391460 File Offset: 0x00391460
    public void BeginAt(OnboardingTutorialStep step)
    {
      if (this.Phase == OnboardingTutorialPhase.Inactive || this.IsRetired(step))
      {
        return;
      }
      this.GoTo(step);
    }

    // Token: 0x0600101A RID: 4122 RVA: 0x0039147C File Offset: 0x0039147C
    public void JumpTo(OnboardingTutorialStep step)
    {
      if (step <= (OnboardingTutorialStep)this.FurthestVisitedStep && !this.IsRetired(step))
      {
        this.GoTo(step);
      }
    }

    // Token: 0x0600101B RID: 4123 RVA: 0x00391498 File Offset: 0x00391498
    private void GoTo(OnboardingTutorialStep step)
    {
      CancellationTokenSource cancellationTokenSource = this.composerCountdown;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.drawDidPresentThisStep = false;
      if (step == OnboardingTutorialStep.Dictation)
      {
        this.DictationText = "";
      }
      this.CurrentStep = new OnboardingTutorialStep?(step);
      this.FurthestVisitedStep = Math.Max(this.FurthestVisitedStep, (int)step);
      if (step == OnboardingTutorialStep.ClickyInterview && !this.completed.Contains(step))
      {
        EventHandler interviewRequested = this.InterviewRequested;
        if (interviewRequested != null)
        {
          interviewRequested(this, EventArgs.Empty);
        }
      }
      base.OnPropertyChanged("IsCurrentStepCompleted");
    }

    // Token: 0x0600101C RID: 4124 RVA: 0x00391520 File Offset: 0x00391520
    public void NoteMicHeardSustainedInput()
    {
      this.MarkCompleted(OnboardingTutorialStep.MicCheck, true);
    }

    // Token: 0x0600101D RID: 4125 RVA: 0x0039152C File Offset: 0x0039152C
    public void NoteOutputVolume(double volume)
    {
      if (volume > 0.05)
      {
        this.MarkCompleted(OnboardingTutorialStep.SpeakerCheck, true);
      }
    }

    // Token: 0x0600101E RID: 4126 RVA: 0x00391544 File Offset: 0x00391544
    public void SetVolumeToHalf()
    {
      EventHandler setVolumeToHalfRequested = this.SetVolumeToHalfRequested;
      if (setVolumeToHalfRequested != null)
      {
        setVolumeToHalfRequested(this, EventArgs.Empty);
      }
      this.MarkCompleted(OnboardingTutorialStep.SpeakerCheck, true);
    }

    // Token: 0x0600101F RID: 4127 RVA: 0x00391568 File Offset: 0x00391568
    public void NoteCompanionTurnStarted()
    {
      this.turnStampGeneration++;
      this.stepTheActiveTurnStartedOn = this.CurrentStep;
    }

    // Token: 0x06001020 RID: 4128 RVA: 0x00391584 File Offset: 0x00391584
    public void NoteDrawingPresented()
    {
      this.drawDidPresentThisStep = true;
    }

    // Token: 0x06001021 RID: 4129 RVA: 0x00391590 File Offset: 0x00391590
    [NullableContext(1)]
    public async Task NoteCompanionTurnCompletedAsync()
    {
      OnboardingTutorialStep? onboardingTutorialStep = this.CurrentStep;
      if (onboardingTutorialStep != null)
      {
        OnboardingTutorialStep step = onboardingTutorialStep.GetValueOrDefault();
        onboardingTutorialStep = this.stepTheActiveTurnStartedOn;
        OnboardingTutorialStep onboardingTutorialStep2 = step;
        if ((onboardingTutorialStep.GetValueOrDefault() == onboardingTutorialStep2) & (onboardingTutorialStep != null))
        {
          switch (step)
          {
          case OnboardingTutorialStep.VoiceHello:
          case OnboardingTutorialStep.DrawToAsk:
            this.MarkCompleted(step, true);
            break;
          case OnboardingTutorialStep.DrawDemo:
            if (this.drawDidPresentThisStep)
            {
              this.stepTheActiveTurnStartedOn = null;
              Func<bool> drawStepStageOwnsCompletion = this.DrawStepStageOwnsCompletion;
              if (drawStepStageOwnsCompletion == null || !drawStepStageOwnsCompletion())
              {
                await Task.Delay(600).ConfigureAwait(true);
                onboardingTutorialStep = this.CurrentStep;
                onboardingTutorialStep2 = step;
                if ((onboardingTutorialStep.GetValueOrDefault() == onboardingTutorialStep2) & (onboardingTutorialStep != null))
                {
                  this.MarkCompleted(step, true);
                  this.Advance();
                }
              }
            }
            break;
          }
        }
      }
    }

    // Token: 0x170003AF RID: 943
    // (get) Token: 0x06001022 RID: 4130 RVA: 0x003915D4 File Offset: 0x003915D4
    // (set) Token: 0x06001023 RID: 4131 RVA: 0x003915DC File Offset: 0x003915DC
    public Func<bool> DrawStepStageOwnsCompletion { get; set; }

    // Token: 0x06001024 RID: 4132 RVA: 0x003915E8 File Offset: 0x003915E8
    public void CompleteDrawStepFromStage()
    {
      if (this.CurrentStep.GetValueOrDefault() != OnboardingTutorialStep.DrawDemo)
      {
        return;
      }
      this.stepTheActiveTurnStartedOn = null;
      this.MarkCompleted(OnboardingTutorialStep.DrawDemo, true);
      this.Advance();
    }

    // Token: 0x170003B0 RID: 944
    // (get) Token: 0x06001025 RID: 4133 RVA: 0x00391624 File Offset: 0x00391624
    public bool DrawDidPresentThisStep
    {
      get
      {
        return this.drawDidPresentThisStep;
      }
    }

    // Token: 0x06001026 RID: 4134 RVA: 0x0039162C File Offset: 0x0039162C
    public void NoteDriveDemoFinished()
    {
      this.MarkCompleted(OnboardingTutorialStep.ClickyDrive, true);
    }

    // Token: 0x06001027 RID: 4135 RVA: 0x00391638 File Offset: 0x00391638
    public void NoteInterviewFinished()
    {
      this.MarkCompleted(OnboardingTutorialStep.ClickyInterview, true);
    }

    // Token: 0x06001028 RID: 4136 RVA: 0x00391644 File Offset: 0x00391644
    public void NoteTextComposerOpened()
    {
      if (this.CurrentStep.GetValueOrDefault() != OnboardingTutorialStep.TextMode)
      {
        return;
      }
      this.MarkCompleted(OnboardingTutorialStep.TextMode, false);
      CancellationTokenSource cancellationTokenSource = this.composerCountdown;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      CancellationTokenSource cancellationTokenSource2 = new CancellationTokenSource();
      this.composerCountdown = cancellationTokenSource2;
      this.RunComposerCountdownAsync(cancellationTokenSource2.Token);
    }

    // Token: 0x06001029 RID: 4137 RVA: 0x00391698 File Offset: 0x00391698
    [NullableContext(1)]
    private async Task RunComposerCountdownAsync(CancellationToken ct)
    {
      try
      {
        await Task.Delay(OnboardingTutorialViewModel.TextModeComposerAutoDismiss, ct).ConfigureAwait(true);
        EventHandler closeComposerRequested = this.CloseComposerRequested;
        if (closeComposerRequested != null)
        {
          closeComposerRequested(this, EventArgs.Empty);
        }
        EventHandler<string> successLine = this.SuccessLine;
        if (successLine != null)
        {
          successLine(this, OnboardingTutorialViewModel.Info(OnboardingTutorialStep.TextMode).SuccessCursorLine);
        }
        await Task.Delay(2000, ct).ConfigureAwait(true);
        if (this.CurrentStep.GetValueOrDefault() == OnboardingTutorialStep.TextMode)
        {
          this.Advance();
        }
      }
      catch (OperationCanceledException)
      {
      }
    }

    // Token: 0x0600102A RID: 4138 RVA: 0x003916E4 File Offset: 0x003916E4
    public void NoteTextComposerTyping()
    {
      CancellationTokenSource cancellationTokenSource = this.composerCountdown;
      if (cancellationTokenSource == null)
      {
        return;
      }
      cancellationTokenSource.Cancel();
    }

    // Token: 0x0600102B RID: 4139 RVA: 0x003916F8 File Offset: 0x003916F8
    public void NoteTextComposerClosedByUser()
    {
      if (this.CurrentStep.GetValueOrDefault() != OnboardingTutorialStep.TextMode)
      {
        return;
      }
      CancellationTokenSource cancellationTokenSource = this.composerCountdown;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.Advance();
    }

    // Token: 0x0600102C RID: 4140 RVA: 0x00391730 File Offset: 0x00391730
    [NullableContext(1)]
    public async Task NoteTypedTurnFinishedAsync()
    {
      if (this.CurrentStep.GetValueOrDefault() == OnboardingTutorialStep.TextMode)
      {
        CancellationTokenSource cancellationTokenSource = this.composerCountdown;
        if (cancellationTokenSource != null)
        {
          cancellationTokenSource.Cancel();
        }
        await Task.Delay(500).ConfigureAwait(true);
        if (this.CurrentStep.GetValueOrDefault() == OnboardingTutorialStep.TextMode)
        {
          EventHandler<string> successLine = this.SuccessLine;
          if (successLine != null)
          {
            successLine(this, OnboardingTutorialViewModel.Info(OnboardingTutorialStep.TextMode).SuccessCursorLine);
          }
          this.Advance();
        }
      }
    }

    // Token: 0x170003B1 RID: 945
    // (get) Token: 0x0600102D RID: 4141 RVA: 0x00391774 File Offset: 0x00391774
    public bool AllowsPushToTalk
    {
      get
      {
        if (!this.IsEngaged)
        {
          return true;
        }
        OnboardingTutorialStep? onboardingTutorialStep = this.CurrentStep;
        if (onboardingTutorialStep != null)
        {
          OnboardingTutorialStep valueOrDefault = onboardingTutorialStep.GetValueOrDefault();
          return OnboardingTutorialViewModel.Info(valueOrDefault).AllowsPushToTalk;
        }
        return false;
      }
    }

    // Token: 0x170003B2 RID: 946
    // (get) Token: 0x0600102E RID: 4142 RVA: 0x003917B0 File Offset: 0x003917B0
    public bool AllowsTextMode
    {
      get
      {
        if (!this.IsEngaged)
        {
          return true;
        }
        OnboardingTutorialStep? onboardingTutorialStep = this.CurrentStep;
        if (onboardingTutorialStep != null)
        {
          OnboardingTutorialStep valueOrDefault = onboardingTutorialStep.GetValueOrDefault();
          return OnboardingTutorialViewModel.Info(valueOrDefault).AllowsTextMode;
        }
        return false;
      }
    }

    // Token: 0x170003B3 RID: 947
    // (get) Token: 0x0600102F RID: 4143 RVA: 0x003917EC File Offset: 0x003917EC
    public bool AllowsDictationPress
    {
      get
      {
        bool flag2;
        if (this.Phase == OnboardingTutorialPhase.Running)
        {
          OnboardingTutorialStep? onboardingTutorialStep = this.CurrentStep;
          bool flag;
          if (onboardingTutorialStep != null)
          {
            OnboardingTutorialStep valueOrDefault = onboardingTutorialStep.GetValueOrDefault();
            if (valueOrDefault - OnboardingTutorialStep.Dictation <= 1)
            {
              flag = true;
              goto IL_002E;
            }
          }
          flag = false;
          IL_002E:
          flag2 = flag;
        }
        else
        {
          flag2 = true;
        }
        return flag2;
      }
    }

    // Token: 0x170003B4 RID: 948
    // (get) Token: 0x06001030 RID: 4144 RVA: 0x00391830 File Offset: 0x00391830
    public bool IsOnDictationStep
    {
      get
      {
        return this.Phase == OnboardingTutorialPhase.Running && this.CurrentStep.GetValueOrDefault() == OnboardingTutorialStep.Dictation;
      }
    }

    // Token: 0x06001031 RID: 4145 RVA: 0x0039185C File Offset: 0x0039185C
    [NullableContext(1)]
    public bool ConsumeDictationTranscript(string transcript)
    {
      if (!this.IsOnDictationStep)
      {
        return false;
      }
      this.DictationText = ((this.DictationText.Length == 0) ? transcript : (this.DictationText + " " + transcript));
      this.MarkCompleted(OnboardingTutorialStep.Dictation, true);
      return true;
    }

    // Token: 0x06001032 RID: 4146 RVA: 0x0039189C File Offset: 0x0039189C
    [NullableContext(1)]
    public void NoteDictationTextInserted(string text)
    {
      if (this.CurrentStep.GetValueOrDefault() != OnboardingTutorialStep.Dictation || string.IsNullOrWhiteSpace(text))
      {
        return;
      }
      this.MarkCompleted(OnboardingTutorialStep.Dictation, true);
    }

    // Token: 0x06001033 RID: 4147 RVA: 0x003918D0 File Offset: 0x003918D0
    private void MarkCompleted(OnboardingTutorialStep step, bool speakSuccessLine = true)
    {
      OnboardingTutorialStep? onboardingTutorialStep = this.CurrentStep;
      if (!((onboardingTutorialStep.GetValueOrDefault() == step) & (onboardingTutorialStep != null)) || !this.completed.Add(step))
      {
        return;
      }
      EventHandler stepCompletedChime = this.StepCompletedChime;
      if (stepCompletedChime != null)
      {
        stepCompletedChime(this, EventArgs.Empty);
      }
      OnboardingTutorialStepInfo onboardingTutorialStepInfo = OnboardingTutorialViewModel.Info(step);
      if (speakSuccessLine)
      {
        string successCursorLine = onboardingTutorialStepInfo.SuccessCursorLine;
        if (successCursorLine != null && step != OnboardingTutorialStep.TextMode)
        {
          EventHandler<string> successLine = this.SuccessLine;
          if (successLine != null)
          {
            successLine(this, successCursorLine);
          }
        }
      }
      base.OnPropertyChanged("IsCurrentStepCompleted");
      if (onboardingTutorialStepInfo.AutoAdvancesAfterCompletion)
      {
        this.AutoAdvanceAsync(step, onboardingTutorialStepInfo.AutoAdvanceDelaySeconds);
      }
    }

    // Token: 0x06001034 RID: 4148 RVA: 0x0039196C File Offset: 0x0039196C
    [NullableContext(1)]
    private async Task AutoAdvanceAsync(OnboardingTutorialStep step, double delaySeconds)
    {
      await Task.Delay(TimeSpan.FromSeconds(delaySeconds)).ConfigureAwait(true);
      OnboardingTutorialStep? onboardingTutorialStep = this.CurrentStep;
      if (((onboardingTutorialStep.GetValueOrDefault() == step) & (onboardingTutorialStep != null)) && this.Phase == OnboardingTutorialPhase.Running)
      {
        this.Advance();
      }
    }

    // Token: 0x170003B5 RID: 949
    // (get) Token: 0x06001035 RID: 4149 RVA: 0x003919C0 File Offset: 0x003919C0
    // (set) Token: 0x06001036 RID: 4150 RVA: 0x003919C8 File Offset: 0x003919C8
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public OnboardingTutorialPhase Phase
    {
      get
      {
        return this.phase;
      }
      set
      {
        if (!EqualityComparer<OnboardingTutorialPhase>.Default.Equals(this.phase, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Phase);
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsEngaged);
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsRestrictingNormalUse);
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.CurrentStepInfo);
          this.phase = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Phase);
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsEngaged);
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsRestrictingNormalUse);
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.CurrentStepInfo);
        }
      }
    }

    // Token: 0x170003B6 RID: 950
    // (get) Token: 0x06001037 RID: 4151 RVA: 0x00391A48 File Offset: 0x00391A48
    // (set) Token: 0x06001038 RID: 4152 RVA: 0x00391A50 File Offset: 0x00391A50
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public OnboardingTutorialStep? CurrentStep
    {
      get
      {
        return this.currentStep;
      }
      set
      {
        if (!EqualityComparer<OnboardingTutorialStep?>.Default.Equals(this.currentStep, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.CurrentStep);
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsRestrictingNormalUse);
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.CurrentStepInfo);
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsCurrentStepCompleted);
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.CanGoBack);
          this.currentStep = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.CurrentStep);
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsRestrictingNormalUse);
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.CurrentStepInfo);
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsCurrentStepCompleted);
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.CanGoBack);
        }
      }
    }

    // Token: 0x170003B7 RID: 951
    // (get) Token: 0x06001039 RID: 4153 RVA: 0x00391AE8 File Offset: 0x00391AE8
    // (set) Token: 0x0600103A RID: 4154 RVA: 0x00391AF0 File Offset: 0x00391AF0
    [Nullable(1)]
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public string DictationText
    {
      [NullableContext(1)]
      get
      {
        return this.dictationText;
      }
      [NullableContext(1)]
      [MemberNotNull("dictationText")]
      set
      {
        if (!EqualityComparer<string>.Default.Equals(this.dictationText, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.DictationText);
          this.dictationText = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.DictationText);
        }
      }
    }

    // Token: 0x170003B8 RID: 952
    // (get) Token: 0x0600103B RID: 4155 RVA: 0x00391B24 File Offset: 0x00391B24
    // (set) Token: 0x0600103C RID: 4156 RVA: 0x00391B2C File Offset: 0x00391B2C
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public int FurthestVisitedStep
    {
      get
      {
        return this.furthestVisitedStep;
      }
      set
      {
        if (!EqualityComparer<int>.Default.Equals(this.furthestVisitedStep, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FurthestVisitedStep);
          this.furthestVisitedStep = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FurthestVisitedStep);
        }
      }
    }

    // Token: 0x170003B9 RID: 953
    // (get) Token: 0x0600103D RID: 4157 RVA: 0x00391B60 File Offset: 0x00391B60
    // (set) Token: 0x0600103E RID: 4158 RVA: 0x00391B68 File Offset: 0x00391B68
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public bool DidShowSpeakerCheck
    {
      get
      {
        return this.didShowSpeakerCheck;
      }
      set
      {
        if (!EqualityComparer<bool>.Default.Equals(this.didShowSpeakerCheck, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.DidShowSpeakerCheck);
          this.didShowSpeakerCheck = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.DidShowSpeakerCheck);
        }
      }
    }

    // Token: 0x170003BA RID: 954
    // (get) Token: 0x0600103F RID: 4159 RVA: 0x00391B9C File Offset: 0x00391B9C
    // (set) Token: 0x06001040 RID: 4160 RVA: 0x00391BA4 File Offset: 0x00391BA4
    [GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
    [ExcludeFromCodeCoverage]
    public bool SkipsDrawingSteps
    {
      get
      {
        return this.skipsDrawingSteps;
      }
      set
      {
        if (!EqualityComparer<bool>.Default.Equals(this.skipsDrawingSteps, value))
        {
          this.OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SkipsDrawingSteps);
          this.skipsDrawingSteps = value;
          this.OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SkipsDrawingSteps);
        }
      }
    }

    // Token: 0x040006DD RID: 1757
    public const double MutedOutputVolumeThreshold = 0.02;

    // Token: 0x040006DE RID: 1758
    public const double SpeakerFixedVolumeThreshold = 0.05;

    // Token: 0x040006DF RID: 1759
    public static readonly TimeSpan TextModeComposerAutoDismiss = TimeSpan.FromSeconds(3L);

    // Token: 0x040006E0 RID: 1760
    [Nullable(1)]
    private readonly SettingsStore settings;

    // Token: 0x040006E1 RID: 1761
    [Nullable(1)]
    private readonly Func<double> outputVolume;

    // Token: 0x040006E2 RID: 1762
    [Nullable(1)]
    private readonly HashSet<OnboardingTutorialStep> completed = new HashSet<OnboardingTutorialStep>();

    // Token: 0x040006E3 RID: 1763
    private int turnStampGeneration;

    // Token: 0x040006E4 RID: 1764
    private OnboardingTutorialStep? stepTheActiveTurnStartedOn;

    // Token: 0x040006E5 RID: 1765
    private bool drawDidPresentThisStep;

    // Token: 0x040006E6 RID: 1766
    private CancellationTokenSource composerCountdown;

    // Token: 0x040006E7 RID: 1767
    [ObservableProperty]
    [NotifyPropertyChangedFor("IsEngaged", new string[] { "IsRestrictingNormalUse", "CurrentStepInfo" })]
    private OnboardingTutorialPhase phase;

    // Token: 0x040006E8 RID: 1768
    [ObservableProperty]
    [NotifyPropertyChangedFor("IsRestrictingNormalUse", new string[] { "CurrentStepInfo", "IsCurrentStepCompleted", "CanGoBack" })]
    private OnboardingTutorialStep? currentStep;

    // Token: 0x040006E9 RID: 1769
    [Nullable(1)]
    [ObservableProperty]
    private string dictationText = "";

    // Token: 0x040006EA RID: 1770
    [ObservableProperty]
    private int furthestVisitedStep;

    // Token: 0x040006EB RID: 1771
    [ObservableProperty]
    private bool didShowSpeakerCheck;

    // Token: 0x040006EC RID: 1772
    [ObservableProperty]
    private bool skipsDrawingSteps;
  }
}
