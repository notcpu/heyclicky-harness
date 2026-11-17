using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Design;
using HeyClicky.App.Interop;
using HeyClicky.App.Notch.Surfaces;
using HeyClicky.App.Services;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using HeyClicky.Core.Onboarding;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Voice;
using HeyClicky.Platform.Audio;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x02000131 RID: 305
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class TutorialWindow
  {
    // Token: 0x170002F4 RID: 756
    // (get) Token: 0x06001041 RID: 4161 RVA: 0x0058388C File Offset: 0x0058388C
    public TutorialWindow.Step CurrentStep
    {
      get
      {
        return this.step;
      }
    }

    // Token: 0x170002F5 RID: 757
    // (get) Token: 0x06001042 RID: 4162 RVA: 0x00583894 File Offset: 0x00583894
    public PanelWindow Window
    {
      get
      {
        return this.window;
      }
    }

    // Token: 0x1400003F RID: 63
    // (add) Token: 0x06001043 RID: 4163 RVA: 0x0058389C File Offset: 0x0058389C
    // (remove) Token: 0x06001044 RID: 4164 RVA: 0x005838D4 File Offset: 0x005838D4
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Ended;

    // Token: 0x170002F6 RID: 758
    // (get) Token: 0x06001045 RID: 4165 RVA: 0x0058390C File Offset: 0x0058390C
    // (set) Token: 0x06001046 RID: 4166 RVA: 0x00583914 File Offset: 0x00583914
    [Nullable(2)]
    public OnboardingTutorialViewModel Vm
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06001047 RID: 4167 RVA: 0x00583920 File Offset: 0x00583920
    private static OnboardingTutorialStep Map(TutorialWindow.Step st)
    {
      return Enum.Parse<OnboardingTutorialStep>(st.ToString());
    }

    // Token: 0x170002F7 RID: 759
    // (get) Token: 0x06001048 RID: 4168 RVA: 0x00583934 File Offset: 0x00583934
    private Color Theme
    {
      get
      {
        return DS.CursorColor(this.s.Settings.CursorColorHex);
      }
    }

    // Token: 0x06001049 RID: 4169 RVA: 0x0058394C File Offset: 0x0058394C
    public TutorialWindow(AppServices services, IAppActions actions, bool excludeFromCapture)
    {
      this.s = services;
      this.actions = actions;
      this.dq = DispatcherQueue.GetForCurrentThread();
      this.window = new PanelWindow("HeyClicky tour", excludeFromCapture, true, 14.0, null, 0.1);
      Grid grid = new Grid
      {
        Margin = new Thickness(24.0)
      };
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      this.dotsSlot.Margin = new Thickness(0.0, 8.0, 0.0, 20.0);
      Grid.SetRow(this.stage, 1);
      Grid.SetRow(this.footer, 2);
      this.footer.Margin = new Thickness(0.0, 22.0, 0.0, 0.0);
      this.footer.Height = 42.0;
      grid.Children.Add(this.dotsSlot);
      grid.Children.Add(this.stage);
      grid.Children.Add(this.footer);
      this.card.Children.Add(grid);
      this.back = TutorialWindow.ChromeButton(Ui.H(3.0, new UIElement[]
      {
        TutorialWindow.Bold(Icons.Glyph("\ue76b", 10.0, DS.Colors.TextSecondary)),
        Ui.Medium("Back", 12.0, DS.Colors.TextSecondary)
      }), new Thickness(4.0, 6.0, 4.0, 6.0));
      this.back.Click += this.Back;
      this.music = TutorialWindow.ChromeButton(Ui.Medium("♫", 13.0, DS.Colors.TextSecondary).Size(18.0, 0.0), new Thickness(4.0));
      this.music.Click += this.ToggleMusic;
      this.skip = TutorialWindow.ChromeButton(Ui.Medium("Skip demo", 12.0, DS.Colors.TextSecondary), new Thickness(6.0, 4.0, 6.0, 4.0));
      this.skip.Click += delegate
      {
        this.End("skipped");
      };
      StackPanel stackPanel = Ui.H(0.0, new UIElement[] { this.music, this.skip });
      stackPanel.HorizontalAlignment = HorizontalAlignment.Right;
      this.back.HorizontalAlignment = HorizontalAlignment.Left;
      Grid grid2 = new Grid
      {
        Margin = new Thickness(16.0, 24.0, 16.0, 0.0),
        VerticalAlignment = VerticalAlignment.Top,
        Children = { this.back, stackPanel }
      };
      this.card.Children.Add(grid2);
      this.window.Panel.Children.Add(this.card);
      this.window.UseFixedEnvelope(760.0, 596.0);
      this.window.ShownSizeChanged += delegate(double _, double h)
      {
        this.stage.Height = Math.Max(0.0, h - 156.0);
      };
      this.poll = this.dq.CreateTimer();
      this.poll.Interval = TimeSpan.FromMilliseconds(33L, 0L);
      this.poll.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.Tick();
      };
      this.s.Companion.StateChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.dq.TryEnqueue(new DispatcherQueueHandler(this.OnVoiceState));
      };
      this.s.Presenter.PresentationChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.dq.TryEnqueue(new DispatcherQueueHandler(this.OnPresentation));
      };
    }

    // Token: 0x0600104A RID: 4170 RVA: 0x00583E08 File Offset: 0x00583E08
    private static TextBlock Bold(TextBlock t)
    {
      t.FontWeight = FontWeights.SemiBold;
      return t;
    }

    // Token: 0x0600104B RID: 4171 RVA: 0x00583E18 File Offset: 0x00583E18
    public static string AnalyticsName(TutorialWindow.Step st)
    {
      string text;
      switch (st)
      {
      case TutorialWindow.Step.MicCheck:
        text = "mic_check";
        break;
      case TutorialWindow.Step.SpeakerCheck:
        text = "speaker_check";
        break;
      case TutorialWindow.Step.VoiceHello:
        text = "voice_hello";
        break;
      case TutorialWindow.Step.ClickyInterview:
        text = "clicky_interview";
        break;
      case TutorialWindow.Step.DrawDemo:
        text = "draw_demo";
        break;
      case TutorialWindow.Step.DrawToAsk:
        text = "draw_to_ask";
        break;
      case TutorialWindow.Step.ClickyDrive:
        text = "clicky_drive";
        break;
      case TutorialWindow.Step.TextMode:
        text = "text_mode";
        break;
      case TutorialWindow.Step.Dictation:
        text = "dictation";
        break;
      default:
        text = "finale";
        break;
      }
      return text;
    }

    // Token: 0x170002F8 RID: 760
    // (get) Token: 0x0600104C RID: 4172 RVA: 0x00583EA0 File Offset: 0x00583EA0
    private int? DurationSeconds
    {
      get
      {
        long? num = this.startedAt;
        if (num != null)
        {
          long valueOrDefault = num.GetValueOrDefault();
          return new int?((int)((Environment.TickCount64 - valueOrDefault) / 1000L));
        }
        return null;
      }
    }

    // Token: 0x0600104D RID: 4173 RVA: 0x00583EE8 File Offset: 0x00583EE8
    private void TrackResolved(string outcome, TutorialWindow.Step lastStep)
    {
      ClickyAnalytics.TrackOnboardingTutorialResolved(outcome, TutorialWindow.AnalyticsName(lastStep), this.DurationSeconds);
      this.startedAt = null;
    }

    // Token: 0x0600104E RID: 4174 RVA: 0x00583F08 File Offset: 0x00583F08
    internal void Back()
    {
      TutorialWindow.Step? previous = this.Previous;
      if (previous != null)
      {
        TutorialWindow.Step valueOrDefault = previous.GetValueOrDefault();
        this.Go(valueOrDefault, "back");
      }
    }

    // Token: 0x0600104F RID: 4175 RVA: 0x00583F3C File Offset: 0x00583F3C
    internal void ToggleMusic()
    {
      this.musicMuted = !this.musicMuted;
      Action<bool> action = this.Music;
      if (action != null)
      {
        action(!this.musicMuted);
      }
      ClickyAnalytics.TrackOnboardingTutorialMusicToggled(this.musicMuted);
      this.RenderChrome();
    }

    // Token: 0x06001050 RID: 4176 RVA: 0x00583F78 File Offset: 0x00583F78
    private static Pressable ChromeButton(FrameworkElement content, Thickness padding)
    {
      Pressable pressable = new Pressable(true);
      pressable.Padding = padding;
      pressable.Children.Add(content);
      pressable.WithPressScale(0.97f, 0.14, false);
      return pressable;
    }

    // Token: 0x170002F9 RID: 761
    // (get) Token: 0x06001051 RID: 4177 RVA: 0x00583FAC File Offset: 0x00583FAC
    private IEnumerable<TutorialWindow.Step> VisibleSteps
    {
      get
      {
        return Enum.GetValues<TutorialWindow.Step>().Where<TutorialWindow.Step>(delegate(TutorialWindow.Step x)
        {
          bool flag = x != TutorialWindow.Step.SpeakerCheck || this.showSpeakerCheck;
          if (flag)
          {
            OnboardingTutorialViewModel vm = this.Vm;
            bool flag2 = vm != null && vm.SkipsDrawingSteps;
            if (flag2)
            {
              bool flag3 = x - TutorialWindow.Step.DrawDemo <= 1;
              flag2 = flag3;
            }
            flag = !flag2;
          }
          return flag;
        });
      }
    }

    // Token: 0x170002FA RID: 762
    // (get) Token: 0x06001052 RID: 4178 RVA: 0x00583FC4 File Offset: 0x00583FC4
    private TutorialWindow.Step? Previous
    {
      get
      {
        List<TutorialWindow.Step> list = this.VisibleSteps.ToList<TutorialWindow.Step>();
        int num = list.IndexOf(this.step);
        if (num <= 0)
        {
          return null;
        }
        return new TutorialWindow.Step?(list[num - 1]);
      }
    }

    // Token: 0x170002FB RID: 763
    // (get) Token: 0x06001053 RID: 4179 RVA: 0x00584008 File Offset: 0x00584008
    private TutorialWindow.Step? Next
    {
      get
      {
        List<TutorialWindow.Step> list = this.VisibleSteps.ToList<TutorialWindow.Step>();
        int num = list.IndexOf(this.step);
        if (num < 0 || num + 1 >= list.Count)
        {
          return null;
        }
        return new TutorialWindow.Step?(list[num + 1]);
      }
    }

    // Token: 0x06001054 RID: 4180 RVA: 0x00584058 File Offset: 0x00584058
    public void Show(TutorialWindow.Step at = TutorialWindow.Step.MicCheck, bool includeSpeakerCheck = false)
    {
      OnboardingTutorialViewModel vm = this.Vm;
      if (vm == null || vm.IsEngaged)
      {
        OnboardingTutorialViewModel vm2 = this.Vm;
        if (vm2 == null || vm2.Phase != OnboardingTutorialPhase.InvitePresented)
        {
          goto IL_0048;
        }
      }
      this.Vm.StartFromInvite();
      if (at != TutorialWindow.Step.MicCheck)
      {
        this.Vm.BeginAt(TutorialWindow.Map(at));
      }
      IL_0048:
      this.HookVm();
      if (!this.bringingForward)
      {
        this.CancelDrawStepStage(false);
      }
      this.showSpeakerCheck = includeSpeakerCheck || at == TutorialWindow.Step.SpeakerCheck;
      if (!this.interviewHooked)
      {
        InterviewController interview = this.Interview;
        if (interview != null)
        {
          this.interviewHooked = true;
          interview.Completed += delegate
          {
            this.dq.TryEnqueue(new DispatcherQueueHandler(this.OnInterviewCompleted));
          };
          interview.IsOnInterviewStep = () => this.shown && !this.revealRunning && this.step == TutorialWindow.Step.ClickyInterview;
        }
      }
      TutorialWindow.Step step = (this.shown ? this.step : ((TutorialWindow.Step)(-1)));
      this.shown = true;
      this.step = at;
      if (!this.bringingForward)
      {
        this.startedAt = new long?(Environment.TickCount64);
        if (at == TutorialWindow.Step.MicCheck)
        {
          ClickyAnalytics.TrackOnboardingTutorialStarted();
        }
        ClickyAnalytics.TrackOnboardingTutorialStepViewed(TutorialWindow.AnalyticsName(at), (at == TutorialWindow.Step.MicCheck) ? "start" : "debug_jump");
      }
      this.InterviewStepChanged(step, at);
      Action<TutorialWindow.Step?> stepChanged = this.StepChanged;
      if (stepChanged != null)
      {
        stepChanged(new TutorialWindow.Step?(at));
      }
      this.furthest = Math.Max(this.furthest, this.VisibleSteps.ToList<TutorialWindow.Step>().IndexOf(at));
      this.Render(false);
      this.window.ShowCentered();
      Sounds.Play("clicky-text-open", 0.35);
      this.card.Opacity = 0.0;
      this.card.Translation = new Vector3(0f, 10f, 0f);
      this.card.Scale = new Vector3(0.985f, 0.985f, 1f);
      this.card.CenterPoint = new Vector3((float)this.window.PanelWidth / 2f, (float)this.window.PanelHeight / 2f, 0f);
      SwiftSpring swiftSpring = new SwiftSpring(0.4, 0.86);
      Anim.OpacitySpring(this.card, 1f, swiftSpring);
      Anim.TranslationSpring(this.card, Vector3.Zero, swiftSpring);
      Anim.ScaleSpring(this.card, 1f, swiftSpring, null);
      this.poll.Start();
    }

    // Token: 0x170002FC RID: 764
    // (get) Token: 0x06001055 RID: 4181 RVA: 0x005842BC File Offset: 0x005842BC
    public bool IsRunning
    {
      get
      {
        return this.shown && !this.revealRunning;
      }
    }

    // Token: 0x06001056 RID: 4182 RVA: 0x005842D4 File Offset: 0x005842D4
    public void Hide()
    {
      this.poll.Stop();
      this.shown = false;
      if (this.step == TutorialWindow.Step.MicCheck)
      {
        this.StopMicLevelMonitor();
      }
      this.window.HideWindow();
    }

    // Token: 0x170002FD RID: 765
    // (get) Token: 0x06001057 RID: 4183 RVA: 0x00584304 File Offset: 0x00584304
    public bool IsShown
    {
      get
      {
        return this.shown;
      }
    }

    // Token: 0x170002FE RID: 766
    // (get) Token: 0x06001058 RID: 4184 RVA: 0x0058430C File Offset: 0x0058430C
    public bool IsBuried
    {
      get
      {
        return this.shown && Native.GetForegroundWindow() != this.window.Hwnd;
      }
    }

    // Token: 0x06001059 RID: 4185 RVA: 0x00584330 File Offset: 0x00584330
    private void StopMicLevelMonitor()
    {
      if (this.s.Companion.State != VoiceState.Listening)
      {
        this.s.Microphone.Stop();
      }
    }

    // Token: 0x0600105A RID: 4186 RVA: 0x00584358 File Offset: 0x00584358
    public void BringForward()
    {
      if (!this.shown)
      {
        this.bringingForward = true;
        try
        {
          this.Show(this.step, false);
        }
        finally
        {
          this.bringingForward = false;
        }
        return;
      }
      this.window.ShowActivated();
    }

    // Token: 0x0600105B RID: 4187 RVA: 0x005843A8 File Offset: 0x005843A8
    public void Go(TutorialWindow.Step next, string navigation = "jump")
    {
      OnboardingTutorialViewModel vm = this.Vm;
      if (vm != null)
      {
        OnboardingTutorialStep onboardingTutorialStep = TutorialWindow.Map(next);
        OnboardingTutorialStep? currentStep = vm.CurrentStep;
        OnboardingTutorialStep onboardingTutorialStep2 = onboardingTutorialStep;
        if (!((currentStep.GetValueOrDefault() == onboardingTutorialStep2) & (currentStep != null)))
        {
          vm.JumpTo(onboardingTutorialStep);
        }
      }
      TutorialWindow.Step step = this.step;
      this.step = next;
      ClickyAnalytics.TrackOnboardingTutorialStepViewed(TutorialWindow.AnalyticsName(next), navigation);
      if (step == TutorialWindow.Step.MicCheck && next != TutorialWindow.Step.MicCheck)
      {
        this.StopMicLevelMonitor();
      }
      this.DrawingStepChanged(step, next);
      this.InterviewStepChanged(step, next);
      Action<TutorialWindow.Step?> stepChanged = this.StepChanged;
      if (stepChanged != null)
      {
        stepChanged(new TutorialWindow.Step?(next));
      }
      this.furthest = Math.Max(this.furthest, this.VisibleSteps.ToList<TutorialWindow.Step>().IndexOf(next));
      this.Render(true);
    }

    // Token: 0x0600105C RID: 4188 RVA: 0x00584464 File Offset: 0x00584464
    internal void Advance()
    {
      if (this.step == TutorialWindow.Step.MicCheck)
      {
        double num = this.systemVolume ?? SystemVolume.EffectiveLevel();
        this.showSpeakerCheck = this.showSpeakerCheck || num <= 0.02;
      }
      if (this.step == TutorialWindow.Step.MicCheck && !this.musicMuted)
      {
        Action<bool> action = this.Music;
        if (action != null)
        {
          action(true);
        }
      }
      OnboardingTutorialViewModel vm = this.Vm;
      if (vm != null)
      {
        vm.Advance();
      }
      TutorialWindow.Step? next = this.Next;
      if (next != null)
      {
        TutorialWindow.Step valueOrDefault = next.GetValueOrDefault();
        this.Go(valueOrDefault, "advance");
        return;
      }
      this.End("finished");
    }

    // Token: 0x0600105D RID: 4189 RVA: 0x0058451C File Offset: 0x0058451C
    internal void End(string reason)
    {
      if (this.step == TutorialWindow.Step.ClickyInterview)
      {
        InterviewController interview = this.Interview;
        if (interview != null)
        {
          interview.Stop();
        }
      }
      InterviewController interview2 = this.Interview;
      if (interview2 != null)
      {
        interview2.CancelPreparedOpening();
      }
      this.CancelDrawStepStage(false);
      this.CancelDriveDemo();
      if (reason != "finished" || this.s.Companion.State != VoiceState.Idle)
      {
        string text = "tutorial";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 2);
        defaultInterpolatedStringHandler.AppendLiteral("ended (");
        defaultInterpolatedStringHandler.AppendFormatted(reason);
        defaultInterpolatedStringHandler.AppendLiteral(") on ");
        defaultInterpolatedStringHandler.AppendFormatted<TutorialWindow.Step>(this.step);
        defaultInterpolatedStringHandler.AppendLiteral(": voice stopped, marks cleared");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        Action stopVoiceAndMarks = this.StopVoiceAndMarks;
        if (stopVoiceAndMarks != null)
        {
          stopVoiceAndMarks();
        }
      }
      this.composerClosingForCountdown = false;
      this.Hide();
      OnboardingTutorialViewModel vm = this.Vm;
      if (vm != null)
      {
        vm.End(reason);
      }
      else
      {
        this.s.Settings.HasCompletedTutorial = true;
      }
      this.TrackResolved(reason, this.step);
      Action<TutorialWindow.Step?> stepChanged = this.StepChanged;
      if (stepChanged != null)
      {
        stepChanged(null);
      }
      Action ended = this.Ended;
      if (ended == null)
      {
        return;
      }
      ended();
    }

    // Token: 0x170002FF RID: 767
    // (get) Token: 0x0600105E RID: 4190 RVA: 0x00584650 File Offset: 0x00584650
    // (set) Token: 0x0600105F RID: 4191 RVA: 0x00584658 File Offset: 0x00584658
    [Nullable(2)]
    public Action StopVoiceAndMarks
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000300 RID: 768
    // (get) Token: 0x06001060 RID: 4192 RVA: 0x00584664 File Offset: 0x00584664
    // (set) Token: 0x06001061 RID: 4193 RVA: 0x0058466C File Offset: 0x0058466C
    [Nullable(2)]
    public Action<TutorialWindow.Step?> StepChanged
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000301 RID: 769
    // (get) Token: 0x06001062 RID: 4194 RVA: 0x00584678 File Offset: 0x00584678
    // (set) Token: 0x06001063 RID: 4195 RVA: 0x00584680 File Offset: 0x00584680
    [Nullable(2)]
    public Func<bool> DrawingShown
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06001064 RID: 4196 RVA: 0x0058468C File Offset: 0x0058468C
    private void HookVm()
    {
      if (!this.vmHooked)
      {
        OnboardingTutorialViewModel vm = this.Vm;
        if (vm != null)
        {
          this.vmHooked = true;
          TextInputSurface.Typing += delegate
          {
            if (this.step == TutorialWindow.Step.TextMode && this.shown)
            {
              vm.NoteTextComposerTyping();
            }
          };
          DispatcherQueueHandler <>9__3;
          DispatcherQueueHandler <>9__4;
          vm.PropertyChanged += delegate([Nullable(2)] object _, PropertyChangedEventArgs e)
          {
            if (e.PropertyName == "CurrentStep")
            {
              this.dq.TryEnqueue(new DispatcherQueueHandler(this.FollowVmStep));
              return;
            }
            if (e.PropertyName == "DictationText")
            {
              DispatcherQueue dispatcherQueue = this.dq;
              DispatcherQueueHandler dispatcherQueueHandler;
              if ((dispatcherQueueHandler = <>9__3) == null)
              {
                dispatcherQueueHandler = (<>9__3 = delegate
                {
                  TextBox textBox = this.dictationBox;
                  if (textBox == null || textBox.Text == vm.DictationText)
                  {
                    return;
                  }
                  textBox.Text = vm.DictationText;
                  textBox.SelectionStart = textBox.Text.Length;
                });
              }
              dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
              return;
            }
            if (e.PropertyName == "IsCurrentStepCompleted")
            {
              DispatcherQueue dispatcherQueue2 = this.dq;
              DispatcherQueueHandler dispatcherQueueHandler2;
              if ((dispatcherQueueHandler2 = <>9__4) == null)
              {
                dispatcherQueueHandler2 = (<>9__4 = delegate
                {
                  if (vm.IsCurrentStepCompleted)
                  {
                    OnboardingTutorialStep? currentStep = vm.CurrentStep;
                    if (currentStep != null)
                    {
                      OnboardingTutorialStep valueOrDefault = currentStep.GetValueOrDefault();
                      if (TutorialWindow.Map(this.step) == valueOrDefault)
                      {
                        this.Complete(this.step, false);
                      }
                    }
                  }
                });
              }
              dispatcherQueue2.TryEnqueue(dispatcherQueueHandler2);
            }
          };
          DispatcherQueueHandler <>9__5;
          vm.CloseComposerRequested += delegate([Nullable(2)] object _, EventArgs _)
          {
            DispatcherQueue dispatcherQueue3 = this.dq;
            DispatcherQueueHandler dispatcherQueueHandler3;
            if ((dispatcherQueueHandler3 = <>9__5) == null)
            {
              dispatcherQueueHandler3 = (<>9__5 = delegate
              {
                if (!(this.s.Presenter.Presentation is NotchPresentation.TextInput))
                {
                  return;
                }
                this.composerClosingForCountdown = true;
                this.s.Presenter.Present(new NotchPresentation.Idle());
              });
            }
            dispatcherQueue3.TryEnqueue(dispatcherQueueHandler3);
          };
          return;
        }
      }
    }

    // Token: 0x06001065 RID: 4197 RVA: 0x0058470C File Offset: 0x0058470C
    private void FollowVmStep()
    {
      OnboardingTutorialViewModel vm = this.Vm;
      OnboardingTutorialStep? onboardingTutorialStep = ((vm != null) ? vm.CurrentStep : null);
      if (onboardingTutorialStep != null)
      {
        OnboardingTutorialStep valueOrDefault = onboardingTutorialStep.GetValueOrDefault();
        if (this.shown && !this.revealRunning && TutorialWindow.Map(this.step) != valueOrDefault)
        {
          TutorialWindow.Step step;
          if (!Enum.TryParse<TutorialWindow.Step>(valueOrDefault.ToString(), out step))
          {
            return;
          }
          if (step == TutorialWindow.Step.SpeakerCheck)
          {
            this.showSpeakerCheck = true;
          }
          if (!this.VisibleSteps.Contains(step))
          {
            return;
          }
          if (this.step == TutorialWindow.Step.MicCheck && step != TutorialWindow.Step.MicCheck && !this.musicMuted)
          {
            Action<bool> action = this.Music;
            if (action != null)
            {
              action(true);
            }
          }
          this.Go(step, "jump");
          return;
        }
      }
    }

    // Token: 0x06001066 RID: 4198 RVA: 0x005847C8 File Offset: 0x005847C8
    private void Complete(TutorialWindow.Step which, bool autoAdvance)
    {
      if (!this.completed.Add(which))
      {
        return;
      }
      if (which == TutorialWindow.Step.DrawDemo)
      {
        this.popContinueOnRender = true;
      }
      ClickyAnalytics.TrackOnboardingTutorialStepCompleted(TutorialWindow.AnalyticsName(which));
      if (which == TutorialWindow.Step.MicCheck)
      {
        OnboardingTutorialViewModel vm = this.Vm;
        if (vm != null)
        {
          vm.NoteMicHeardSustainedInput();
        }
      }
      if (which == this.step)
      {
        this.RenderFooter();
      }
      if (autoAdvance)
      {
        this.After(1.6, delegate
        {
          if (this.step == which)
          {
            this.Advance();
          }
        });
      }
    }

    // Token: 0x06001067 RID: 4199 RVA: 0x00584868 File Offset: 0x00584868
    private void Render(bool animated)
    {
      TutorialWindow.<>c__DisplayClass96_0 CS$<>8__locals1 = new TutorialWindow.<>c__DisplayClass96_0();
      CS$<>8__locals1.<>4__this = this;
      bool flag = this.step == TutorialWindow.Step.ClickyInterview;
      double num = (flag ? 760.0 : 500.0);
      double num2 = 156.0 + (flag ? 440.0 : 416.0);
      if (animated)
      {
        this.window.AnimatePanelSize(num, num2, TutorialWindow.PageSpring);
      }
      else
      {
        this.window.SetPanelSize(num, num2);
      }
      CS$<>8__locals1.list = this.VisibleSteps.ToList<TutorialWindow.Step>();
      this.dotsSlot.Content = new ProgressDots(CS$<>8__locals1.list.Count, CS$<>8__locals1.list.IndexOf(this.step), delegate(int i)
      {
        CS$<>8__locals1.<>4__this.Go(CS$<>8__locals1.list[i], "jump");
      }, this.furthest);
      this.RenderChrome();
      TutorialWindow.<>c__DisplayClass96_0 CS$<>8__locals2 = CS$<>8__locals1;
      FrameworkElement frameworkElement;
      if (this.stage.Children.Count <= 0)
      {
        frameworkElement = null;
      }
      else
      {
        UIElementCollection children = this.stage.Children;
        frameworkElement = (FrameworkElement)children[children.Count - 1];
      }
      CS$<>8__locals2.old = frameworkElement;
      this.liveCaps.Clear();
      this.meter = null;
      this.statusLine = null;
      this.dictationBox = null;
      FrameworkElement frameworkElement2 = this.Page();
      frameworkElement2.Margin = new Thickness(0.0, 24.0, 0.0, 0.0);
      frameworkElement2.VerticalAlignment = VerticalAlignment.Stretch;
      this.stage.Children.Add(frameworkElement2);
      if (CS$<>8__locals1.old != null)
      {
        if (animated)
        {
          Anim.OpacitySpring(CS$<>8__locals1.old, 0f, TutorialWindow.PageSpring);
          Anim.TranslationSpring(CS$<>8__locals1.old, new Vector3(0f, -6f, 0f), TutorialWindow.PageSpring);
          this.After(0.4, delegate
          {
            CS$<>8__locals1.<>4__this.stage.Children.Remove(CS$<>8__locals1.old);
          });
          frameworkElement2.Opacity = 0.0;
          frameworkElement2.Translation = new Vector3(0f, 10f, 0f);
          frameworkElement2.Scale = new Vector3(0.985f, 0.985f, 1f);
          frameworkElement2.CenterPoint = new Vector3((float)num / 2f - 24f, 200f, 0f);
          Anim.OpacitySpring(frameworkElement2, 1f, TutorialWindow.PageSpring);
          Anim.TranslationSpring(frameworkElement2, Vector3.Zero, TutorialWindow.PageSpring);
          Anim.ScaleSpring(frameworkElement2, 1f, TutorialWindow.PageSpring, null);
        }
        else
        {
          this.stage.Children.Remove(CS$<>8__locals1.old);
        }
      }
      this.RenderFooter();
      if (this.step == TutorialWindow.Step.Finale)
      {
        this.skip.Visibility = Visibility.Collapsed;
        return;
      }
      this.skip.Visibility = Visibility.Visible;
    }

    // Token: 0x06001068 RID: 4200 RVA: 0x00584B38 File Offset: 0x00584B38
    private void RenderChrome()
    {
      this.back.Visibility = ((this.Previous == null) ? Visibility.Collapsed : Visibility.Visible);
      this.music.Visibility = ((this.furthest > 0) ? Visibility.Visible : Visibility.Collapsed);
      this.music.Children.Clear();
      TextBlock textBlock = Ui.Medium("♫", 13.0, DS.Colors.TextSecondary).Size(18.0, 0.0);
      Grid grid = new Grid
      {
        Children = { textBlock }
      };
      if (this.musicMuted)
      {
        grid.Children.Add(new Rectangle
        {
          Width = 1.5,
          Height = 15.0,
          Fill = Ui.Brush(DS.Colors.TextSecondary),
          RenderTransform = new RotateTransform
          {
            Angle = -45.0,
            CenterX = 0.75,
            CenterY = 7.5
          },
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Center
        });
      }
      this.music.Children.Add(grid);
      AutomationProperties.SetName(this.music, this.musicMuted ? "Turn tour music on" : "Turn tour music off");
    }

    // Token: 0x06001069 RID: 4201 RVA: 0x00584C90 File Offset: 0x00584C90
    private void RenderFooter()
    {
      this.footer.Children.Clear();
      if (this.step == TutorialWindow.Step.Finale)
      {
        return;
      }
      this.continueButton = new GelButton("Continue", GelAppearance.PrimaryBlue, GelButton.Sizing.Large, 100.0, null, null, null)
      {
        HorizontalAlignment = HorizontalAlignment.Center
      };
      bool flag = this.completed.Contains(this.step);
      this.continueButton.IsEnabledGel = flag;
      if (flag && this.step == TutorialWindow.Step.DrawDemo && this.popContinueOnRender)
      {
        this.popContinueOnRender = false;
        this.PopContinue(this.continueButton);
      }
      if (this.step == TutorialWindow.Step.ClickyInterview && !flag)
      {
        this.continueButton.Opacity = 0.0;
      }
      this.continueButton.Click += delegate
      {
        string text = "tutorial";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 2);
        defaultInterpolatedStringHandler.AppendLiteral("Continue clicked on ");
        defaultInterpolatedStringHandler.AppendFormatted<TutorialWindow.Step>(this.step);
        defaultInterpolatedStringHandler.AppendLiteral(" (completed: ");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(this.completed.Contains(this.step));
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        if (this.completed.Contains(this.step))
        {
          this.Advance();
        }
      };
      this.footer.Children.Add(this.continueButton);
    }

    // Token: 0x0600106A RID: 4202 RVA: 0x00584D80 File Offset: 0x00584D80
    private FrameworkElement Layout(string title, string body, FrameworkElement interactive)
    {
      TextBlock textBlock = Ui.Text(title, 20.0, FontWeights.SemiBold, DS.Colors.TextPrimary, TextAlignment.Center, 0, true, false);
      TextBlock textBlock2 = Ui.Text(body, 13.0, FontWeights.Medium, DS.Colors.TextSecondary, TextAlignment.Center, 0, true, false);
      textBlock2.LineHeight = 19.29;
      textBlock2.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 8.0,
        MaxWidth = 360.0,
        HorizontalAlignment = HorizontalAlignment.Center,
        Children = { textBlock, textBlock2 }
      };
      interactive.MaxWidth = Math.Max((interactive.MaxWidth == double.PositiveInfinity) ? 360.0 : interactive.MaxWidth, 360.0);
      interactive.HorizontalAlignment = HorizontalAlignment.Center;
      interactive.VerticalAlignment = VerticalAlignment.Center;
      Grid grid = new Grid();
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      StepInteractiveSlot stepInteractiveSlot = new StepInteractiveSlot(interactive)
      {
        Margin = new Thickness(0.0, 18.0, 0.0, 18.0)
      };
      Grid.SetRow(stepInteractiveSlot, 1);
      grid.Children.Add(stackPanel);
      grid.Children.Add(stepInteractiveSlot);
      return grid;
    }

    // Token: 0x0600106B RID: 4203 RVA: 0x00584F00 File Offset: 0x00584F00
    private FrameworkElement Page()
    {
      FrameworkElement frameworkElement;
      switch (this.step)
      {
      case TutorialWindow.Step.MicCheck:
        frameworkElement = this.MicCheckPage();
        break;
      case TutorialWindow.Step.SpeakerCheck:
        frameworkElement = this.SpeakerPage();
        break;
      case TutorialWindow.Step.VoiceHello:
        frameworkElement = this.VoicePage("This is how you talk to me", "Hold the keys down, say hi or tell me your name, then let go. I'm listening the whole time you hold them.", "My name is " + this.FirstName(), false);
        break;
      case TutorialWindow.Step.ClickyInterview:
        frameworkElement = this.InterviewPage();
        break;
      case TutorialWindow.Step.DrawDemo:
        frameworkElement = this.VoicePage("Here's how I'd help you", "I see what you see. Hold the keys, ask, and watch your screen.", "Show me how you'd help me", true);
        break;
      case TutorialWindow.Step.DrawToAsk:
        frameworkElement = this.DrawToAskPage();
        break;
      case TutorialWindow.Step.ClickyDrive:
        frameworkElement = this.ClickyDrivePage();
        break;
      case TutorialWindow.Step.TextMode:
        frameworkElement = this.TextModePage();
        break;
      case TutorialWindow.Step.Dictation:
        frameworkElement = this.DictationPage();
        break;
      default:
        frameworkElement = this.FinalePage();
        break;
      }
      return frameworkElement;
    }

    // Token: 0x0600106C RID: 4204 RVA: 0x00584FC4 File Offset: 0x00584FC4
    private string FirstName()
    {
      UserProfile user = this.s.Auth.User;
      string text = ((user != null) ? user.DisplayName : null);
      if (!string.IsNullOrWhiteSpace(text))
      {
        return text.Trim().Split(' ', StringSplitOptions.None)[0];
      }
      string userName = Environment.UserName;
      if (!string.IsNullOrWhiteSpace(userName))
      {
        char c = char.ToUpperInvariant(userName[0]);
        ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c);
        string text2 = userName;
        return readOnlySpan + text2.Substring(1, text2.Length - 1);
      }
      return "[your name]";
    }

    // Token: 0x0600106D RID: 4205 RVA: 0x0058504C File Offset: 0x0058504C
    private FrameworkElement MicCheckPage()
    {
      this.meter = new MicMeter(this.Theme);
      this.micCheckSeconds = 0.0;
      this.micCheckHeardAnyInput = false;
      FrameworkElement frameworkElement = this.MicPicker();
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 16.0,
        Children = 
        {
          this.meter,
          new SpokenBubble("Testing, testing, 1, 2, 3"),
          frameworkElement
        }
      };
      frameworkElement.HorizontalAlignment = HorizontalAlignment.Center;
      this.s.Microphone.Start();
      return this.Layout("First, can I hear you?", "Say anything out loud. If the bars dance, your mic works. I hear best on built-in or wired mics: Bluetooth ones get flaky.", stackPanel);
    }

    // Token: 0x0600106E RID: 4206 RVA: 0x005850F8 File Offset: 0x005850F8
    private FrameworkElement MicPicker()
    {
      TutorialWindow.<>c__DisplayClass103_0 CS$<>8__locals1 = new TutorialWindow.<>c__DisplayClass103_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.p = new Pressable(true)
      {
        Height = 38.0,
        Padding = new Thickness(16.0, 0.0, 16.0, 0.0),
        CornerRadius = new CornerRadius(10.0),
        Background = Ui.Brush(DS.White(0.06)),
        BorderBrush = Ui.Brush(DS.White(0.22)),
        BorderThickness = new Thickness(1.0)
      };
      TextBlock textBlock = TutorialWindow.Bold(Icons.Glyph("\ue720", 12.0, DS.Colors.TextSecondary));
      TutorialWindow.<>c__DisplayClass103_0 CS$<>8__locals2 = CS$<>8__locals1;
      Func<string> microphoneName = this.MicrophoneName;
      CS$<>8__locals2.name = Ui.Semibold(((microphoneName != null) ? microphoneName() : null) ?? "System Default", 13.0, DS.Colors.TextPrimary);
      TextBlock textBlock2 = TutorialWindow.Bold(Icons.Glyph("\ue8cb", 10.0, DS.Colors.TextSecondary));
      textBlock2.Margin = new Thickness(2.0, 0.0, 0.0, 0.0);
      CS$<>8__locals1.p.Children.Add(Ui.H(8.0, new UIElement[] { textBlock, CS$<>8__locals1.name, textBlock2 }).Center<StackPanel>());
      CS$<>8__locals1.menu = new MenuFlyout();
      CS$<>8__locals1.<MicPicker>g__Add|0("System Default", null, CS$<>8__locals1.name.Text == "System Default");
      CS$<>8__locals1.p.Click += delegate
      {
        TutorialWindow.<>c__DisplayClass103_0.<<MicPicker>b__1>d <<MicPicker>b__1>d;
        <<MicPicker>b__1>d.<>t__builder = AsyncVoidMethodBuilder.Create();
        <<MicPicker>b__1>d.<>4__this = CS$<>8__locals1;
        <<MicPicker>b__1>d.<>1__state = -1;
        <<MicPicker>b__1>d.<>t__builder.Start<TutorialWindow.<>c__DisplayClass103_0.<<MicPicker>b__1>d>(ref <<MicPicker>b__1>d);
      };
      return CS$<>8__locals1.p;
    }

    // Token: 0x0600106F RID: 4207 RVA: 0x005852D8 File Offset: 0x005852D8
    [return: Nullable(2)]
    private static string EndpointId(string interfaceId)
    {
      Match match = Regex.Match(interfaceId, "MMDEVAPI#(\\{[^#]+)#", RegexOptions.IgnoreCase);
      if (!match.Success)
      {
        return null;
      }
      return match.Groups[1].Value;
    }

    // Token: 0x17000302 RID: 770
    // (get) Token: 0x06001070 RID: 4208 RVA: 0x00585310 File Offset: 0x00585310
    // (set) Token: 0x06001071 RID: 4209 RVA: 0x00585318 File Offset: 0x00585318
    [Nullable(2)]
    public Func<string> MicrophoneName
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000303 RID: 771
    // (get) Token: 0x06001072 RID: 4210 RVA: 0x00585324 File Offset: 0x00585324
    // (set) Token: 0x06001073 RID: 4211 RVA: 0x0058532C File Offset: 0x0058532C
    [Nullable(new byte[] { 2, 2, 1 })]
    public Action<string, string> MicrophonePicked
    {
      [return: Nullable(new byte[] { 2, 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 2, 1 })]
      set;
    }

    // Token: 0x06001074 RID: 4212 RVA: 0x00585338 File Offset: 0x00585338
    private FrameworkElement SpeakerPage()
    {
      bool flag = this.completed.Contains(TutorialWindow.Step.SpeakerCheck);
      TextBlock textBlock = Icons.Glyph(flag ? "\ue767" : "\ue74f", 30.0, flag ? this.Theme : DS.Colors.TextSecondary);
      textBlock.Height = 40.0;
      Pressable pressable = new Pressable(true)
      {
        CornerRadius = new CornerRadius(100.0),
        Background = Ui.Brush(this.Theme),
        Padding = new Thickness(24.0, 10.0, 24.0, 10.0),
        HorizontalAlignment = HorizontalAlignment.Center
      };
      pressable.Children.Add(Ui.Semibold("Set volume to 50%", 13.0, Colors.White));
      pressable.WithPressScale(0.97f, 0.14, false);
      pressable.Click += delegate
      {
        SystemVolume.SetLevel(0.5);
        ClickyAnalytics.TrackOnboardingTutorialSpeakerVolumeFixed("button");
        this.Complete(TutorialWindow.Step.SpeakerCheck, true);
        this.Render(false);
      };
      return this.Layout("Your volume is at zero", "You'd see me talking and hear nothing. One click fixes it before we start.", new StackPanel
      {
        Spacing = 18.0,
        Children = { textBlock, pressable }
      });
    }

    // Token: 0x06001075 RID: 4213 RVA: 0x00585478 File Offset: 0x00585478
    private FrameworkElement KeyRow(string lead, string trail, KeyCap.Size size, double labelWidth, double spacing, double fontSize)
    {
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = spacing,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      TextBlock textBlock = Ui.Medium(lead, fontSize, DS.Colors.TextSecondary);
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      if (labelWidth > 0.0)
      {
        textBlock.Width = labelWidth;
        textBlock.TextAlignment = TextAlignment.Right;
      }
      stackPanel.Children.Add(textBlock);
      KeyCap keyCap = new KeyCap("Ctrl", "control", size, this.Theme);
      KeyCap keyCap2 = new KeyCap("Alt", "alt", size, this.Theme);
      this.liveCaps.Add(keyCap);
      this.liveCaps.Add(keyCap2);
      stackPanel.Children.Add(Ui.H(spacing, new UIElement[] { keyCap, keyCap2 }));
      if (!string.IsNullOrEmpty(trail))
      {
        TextBlock textBlock2 = Ui.Medium(trail, fontSize, DS.Colors.TextSecondary);
        textBlock2.VerticalAlignment = VerticalAlignment.Center;
        if (labelWidth > 0.0)
        {
          textBlock2.Width = labelWidth;
        }
        stackPanel.Children.Add(textBlock2);
      }
      return stackPanel;
    }

    // Token: 0x06001076 RID: 4214 RVA: 0x00585588 File Offset: 0x00585588
    private FrameworkElement VoicePage(string title, string body, string line, bool stagger)
    {
      FrameworkElement frameworkElement = this.KeyRow("Hold", "and say", KeyCap.Size.Regular, 56.0, 10.0, 12.5);
      SpokenBubble spokenBubble = new SpokenBubble(line);
      if (stagger)
      {
        this.Reveal(frameworkElement, 0.5);
        this.Reveal(spokenBubble, 0.85);
      }
      Grid grid = new Grid
      {
        Height = 16.0
      };
      this.statusLine = grid;
      this.UpdateStatus();
      return this.Layout(title, body, new StackPanel
      {
        Spacing = 14.0,
        Children = 
        {
          new StackPanel
          {
            Spacing = 10.0,
            Children = { frameworkElement, spokenBubble }
          },
          grid
        }
      });
    }

    // Token: 0x06001077 RID: 4215 RVA: 0x00585670 File Offset: 0x00585670
    private void Reveal(FrameworkElement e, double delay)
    {
      if (Anim.Instant)
      {
        return;
      }
      e.Opacity = 0.0;
      e.Translation = new Vector3(0f, 8f, 0f);
      e.Scale = new Vector3(0.97f, 0.97f, 1f);
      this.After(delay, delegate
      {
        SwiftSpring swiftSpring = new SwiftSpring(0.45, 0.85);
        Anim.OpacitySpring(e, 1f, swiftSpring);
        Anim.TranslationSpring(e, Vector3.Zero, swiftSpring);
        Anim.ScaleSpring(e, 1f, swiftSpring, null);
      });
    }

    // Token: 0x06001078 RID: 4216 RVA: 0x005856F8 File Offset: 0x005856F8
    private void UpdateStatus()
    {
      Grid grid = this.statusLine as Grid;
      if (grid == null)
      {
        return;
      }
      grid.Children.Clear();
      if (this.completed.Contains(this.step))
      {
        return;
      }
      string text;
      switch (this.s.Companion.State)
      {
      case VoiceState.Listening:
        text = "listening…";
        break;
      case VoiceState.Processing:
        text = "thinking…";
        break;
      case VoiceState.Responding:
        text = "speaking…";
        break;
      default:
        text = ((this.step == TutorialWindow.Step.DrawToAsk) ? null : "waiting for you…");
        break;
      }
      string text2 = text;
      if (text2 != null)
      {
        grid.Children.Add(Ui.Text(text2, 11.5, FontWeights.Medium, DS.Colors.TextSecondary, TextAlignment.Center, 0, false, false));
      }
    }

    // Token: 0x06001079 RID: 4217 RVA: 0x005857B4 File Offset: 0x005857B4
    private FrameworkElement DrawToAskPage()
    {
      Grid grid = new Grid
      {
        Width = 320.0,
        Height = 195.0,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      string text = Path.Combine(AppContext.BaseDirectory, "Assets", "Video", "clicky-spatial-demo.mp4");
      if (File.Exists(text))
      {
        RoundedVideoView view = new RoundedVideoView(320.0, 195.0, 12.0);
        MediaPlayer mp = new MediaPlayer
        {
          Source = MediaSource.CreateFromUri(new Uri(text)),
          IsLoopingEnabled = true,
          IsMuted = true,
          AutoPlay = false
        };
        view.Attach(mp);
        view.Loaded += delegate(object _, RoutedEventArgs _)
        {
          mp.Play();
        };
        view.Unloaded += delegate(object _, RoutedEventArgs _)
        {
          view.Attach(null);
          try
          {
            mp.Pause();
            mp.Dispose();
          }
          catch
          {
          }
        };
        grid.Children.Add(view);
      }
      else
      {
        Border border = new Border
        {
          Background = Ui.Brush(DS.White(0.06))
        };
        grid.Children.Add(border);
        Ui.ClipRounded(grid, 12.0);
      }
      grid.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(12.0),
        BorderBrush = Ui.Brush(DS.White(0.14)),
        BorderThickness = new Thickness(0.5),
        IsHitTestVisible = false
      });
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 7.0,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      TextBlock textBlock = Ui.Medium("Hold", 11.5, DS.Colors.TextSecondary);
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      KeyCap keyCap = new KeyCap("Ctrl", "control", KeyCap.Size.Mini, this.Theme);
      KeyCap keyCap2 = new KeyCap("Alt", "alt", KeyCap.Size.Mini, this.Theme);
      this.liveCaps.Add(keyCap);
      this.liveCaps.Add(keyCap2);
      TextBlock textBlock2 = Ui.Medium("and", 11.5, DS.Colors.TextSecondary);
      textBlock2.VerticalAlignment = VerticalAlignment.Center;
      Grid grid2 = new Grid
      {
        Width = 44.0,
        Height = 44.0
      };
      grid2.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(8.0),
        Background = Ui.Brush(DS.White(0.06)),
        BorderBrush = Ui.Brush(DS.White(0.14)),
        BorderThickness = new Thickness(0.5)
      });
      grid2.Children.Add(Icons.Glyph("\ue70f", 18.0, this.Theme));
      TextBlock textBlock3 = Ui.Medium("highlight anything, then ask", 11.5, DS.Colors.TextSecondary);
      textBlock3.VerticalAlignment = VerticalAlignment.Center;
      stackPanel.Children.Add(textBlock);
      stackPanel.Children.Add(Ui.H(7.0, new UIElement[] { keyCap, keyCap2 }));
      stackPanel.Children.Add(textBlock2);
      stackPanel.Children.Add(grid2);
      stackPanel.Children.Add(Ui.Shrink(textBlock3, HorizontalAlignment.Left, 0.85));
      Grid grid3 = new Grid
      {
        Height = 14.0
      };
      this.statusLine = grid3;
      this.UpdateStatus();
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 10.0,
        Children = { grid, stackPanel, grid3 }
      };
      stackPanel2.MaxWidth = 440.0;
      return this.Layout("Circle anything, ask about it", "Hold the keys, circle something on your screen with the cursor, and ask about it. Like this:", stackPanel2);
    }

    // Token: 0x0600107A RID: 4218 RVA: 0x00585BD0 File Offset: 0x00585BD0
    private FrameworkElement TextModePage()
    {
      KeyCap keyCap = new KeyCap("Ctrl", "control", KeyCap.Size.Regular, this.Theme);
      this.liveCaps.Add(keyCap);
      TextBlock textBlock = Ui.Text("tap it twice, quickly", 11.0, FontWeights.Medium, DS.Colors.TextSecondary, TextAlignment.Center, 0, false, false);
      Grid grid = new Grid
      {
        Height = 16.0
      };
      if (this.s.Presenter.Presentation is NotchPresentation.TextResponse)
      {
        grid.Children.Add(Ui.Text("answering in the notch…", 11.5, FontWeights.Medium, DS.Colors.TextSecondary, TextAlignment.Center, 0, false, false));
      }
      this.GhostDoubleTap(keyCap);
      return this.Layout("Not in the mood to talk? Type", "Double-tap Ctrl and you can just talk to me ChatGPT style. But, just a note I use Claude in the background!", new StackPanel
      {
        Spacing = 14.0,
        Children = 
        {
          Ui.H(12.0, new UIElement[] { keyCap }).Center<StackPanel>(),
          textBlock,
          grid
        }
      });
    }

    // Token: 0x0600107B RID: 4219 RVA: 0x00585CE8 File Offset: 0x00585CE8
    private void GhostDoubleTap(KeyCap cap)
    {
      if (Anim.Instant)
      {
        return;
      }
      DispatcherQueueTimer t = this.dq.CreateTimer().Rooted();
      t.Interval = TimeSpan.FromSeconds(2.6);
      Action <>9__1;
      Action <>9__2;
      Action <>9__3;
      t.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        if (this.step != TutorialWindow.Step.TextMode || !this.window.Root.IsLoaded)
        {
          t.Stop();
          return;
        }
        cap.Pressed = true;
        TutorialWindow <>4__this = this;
        double num = 0.14;
        Action action;
        if ((action = <>9__1) == null)
        {
          action = (<>9__1 = delegate
          {
            cap.Pressed = false;
          });
        }
        <>4__this.After(num, action);
        TutorialWindow <>4__this2 = this;
        double num2 = 0.3;
        Action action2;
        if ((action2 = <>9__2) == null)
        {
          action2 = (<>9__2 = delegate
          {
            cap.Pressed = true;
          });
        }
        <>4__this2.After(num2, action2);
        TutorialWindow <>4__this3 = this;
        double num3 = 0.44;
        Action action3;
        if ((action3 = <>9__3) == null)
        {
          action3 = (<>9__3 = delegate
          {
            cap.Pressed = false;
          });
        }
        <>4__this3.After(num3, action3);
      };
      t.Start();
    }

    // Token: 0x0600107C RID: 4220 RVA: 0x00585D64 File Offset: 0x00585D64
    private FrameworkElement InterviewPage()
    {
      TextBlock textBlock = Ui.Text("I'm curious about you.", 20.0, FontWeights.SemiBold, DS.Colors.TextPrimary, TextAlignment.Center, 0, true, false);
      TextBlock textBlock2 = Ui.Text("Four quick questions about you, so I can make this whole thing actually useful for you.", 13.0, FontWeights.Medium, DS.Colors.TextSecondary, TextAlignment.Center, 0, true, false);
      textBlock2.LineHeight = 19.29;
      textBlock2.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 8.0,
        MaxWidth = 480.0,
        HorizontalAlignment = HorizontalAlignment.Center,
        Children = { textBlock, textBlock2 }
      };
      Grid grid = new Grid();
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      grid.Children.Add(stackPanel);
      InterviewController interview = this.Interview;
      if (interview != null)
      {
        InterviewStage interviewStage = new InterviewStage(this.s, interview)
        {
          VerticalAlignment = VerticalAlignment.Bottom
        };
        Grid.SetRow(interviewStage, 1);
        grid.Children.Add(interviewStage);
      }
      return grid;
    }

    // Token: 0x0600107D RID: 4221 RVA: 0x00585E98 File Offset: 0x00585E98
    private void InterviewStepChanged(TutorialWindow.Step from, TutorialWindow.Step to)
    {
      InterviewController interview = this.Interview;
      if (interview == null)
      {
        return;
      }
      if (to < TutorialWindow.Step.ClickyInterview)
      {
        interview.WantsPreparedOpening = true;
        interview.PrepareOpening();
      }
      else if (to > TutorialWindow.Step.ClickyInterview)
      {
        interview.CancelPreparedOpening();
      }
      else
      {
        interview.WantsPreparedOpening = false;
      }
      if (to == TutorialWindow.Step.ClickyInterview && from != TutorialWindow.Step.ClickyInterview)
      {
        interview.Start();
        return;
      }
      if (from == TutorialWindow.Step.ClickyInterview && to != TutorialWindow.Step.ClickyInterview)
      {
        interview.Stop();
      }
    }

    // Token: 0x0600107E RID: 4222 RVA: 0x00585EF4 File Offset: 0x00585EF4
    private void OnInterviewCompleted()
    {
      OnboardingTutorialViewModel vm = this.Vm;
      if (vm != null)
      {
        vm.NoteInterviewFinished();
      }
      this.Complete(TutorialWindow.Step.ClickyInterview, false);
    }

    // Token: 0x0600107F RID: 4223 RVA: 0x00585F10 File Offset: 0x00585F10
    private FrameworkElement FinalePage()
    {
      Grid grid = new Grid
      {
        Width = 88.0,
        Height = 88.0,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      grid.Children.Add(Ui.GlowSpot(this.Theme, 0.35, 168.0, 0.3).Pad(-40.0));
      grid.Children.Add(Ui.GlowSpot(this.Theme, 0.55, 132.0, 0.35).Pad(-22.0));
      grid.Children.Add(IntroMark.Create(88.0, this.Theme, 0.0));
      GelButton show = new GelButton("Show me", GelAppearance.PrimaryBlue, GelButton.Sizing.Large, 100.0, null, null, null)
      {
        HorizontalAlignment = HorizontalAlignment.Center
      };
      this.finaleShowMe = show;
      Action <>9__1;
      Action <>9__2;
      show.Click += delegate
      {
        if (show.IsLoading)
        {
          return;
        }
        Func<bool> canDrawFinale = this.CanDrawFinale;
        if (canDrawFinale != null && canDrawFinale())
        {
          Func<Task> runDrawnFinale = this.RunDrawnFinale;
          if (runDrawnFinale != null)
          {
            this.revealRunning = true;
            this.poll.Stop();
            Sounds.Play("clicky-text-open", 0.4);
            Anim.Opacity(this.window.Root, 0f, 1.25, Easing.EaseInOut(Ui.Compositor), 0.0);
            TutorialWindow <>4__this = this;
            double num = 1.25;
            Action action;
            if ((action = <>9__1) == null)
            {
              action = (<>9__1 = delegate
              {
                this.window.HideWindow();
                this.window.Root.Opacity = 1.0;
              });
            }
            <>4__this.After(num, action);
            runDrawnFinale();
            return;
          }
        }
        Func<bool> canReveal = this.CanReveal;
        if (canReveal != null && canReveal())
        {
          Func<Task> reveal = this.RunReveal;
          if (reveal != null)
          {
            InterviewController interview = this.Interview;
            if (interview != null && interview.IsPlanInFlight)
            {
              show.Title = "One moment...";
              show.IsLoading = true;
              AutomationProperties.SetName(show, "One moment, setting up your Clickys");
              this.WaitForPlan(interview, 120.0, delegate
              {
                this.DissolveInto(reveal, 0.7);
              });
              return;
            }
            this.DissolveInto(reveal, 0.0);
            return;
          }
        }
        Sounds.Play("reveal-boot", 0.6);
        Anim.Opacity(this.window.Root, 0f, 1.25, Easing.EaseInOut(Ui.Compositor), 0.0);
        TutorialWindow <>4__this2 = this;
        double num2 = 1.3;
        Action action2;
        if ((action2 = <>9__2) == null)
        {
          action2 = (<>9__2 = delegate
          {
            this.window.Root.Opacity = 1.0;
            this.End("finished");
          });
        }
        <>4__this2.After(num2, action2);
      };
      return this.Layout("There's one more thing...", "Voice, drawing, text mode, agents: you've seen it all. But, hey, can I show you one more thing?", new StackPanel
      {
        Spacing = 44.0,
        Children = { grid, show }
      });
    }

    // Token: 0x06001080 RID: 4224 RVA: 0x00586094 File Offset: 0x00586094
    private void DissolveInto(Func<Task> reveal, double extraPause)
    {
      this.revealRunning = true;
      this.poll.Stop();
      Sounds.Play("clicky-text-open", 0.4);
      Anim.Opacity(this.window.Root, 0f, 1.25, Easing.EaseInOut(Ui.Compositor), 0.0);
      this.After(1.25 + extraPause, delegate
      {
        this.window.HideWindow();
        this.window.Root.Opacity = 1.0;
        reveal();
      });
    }

    // Token: 0x06001081 RID: 4225 RVA: 0x00586128 File Offset: 0x00586128
    private void WaitForPlan(InterviewController interview, double maxSeconds, Action then)
    {
      long started = Environment.TickCount64;
      DispatcherQueueTimer t = this.dq.CreateTimer().Rooted();
      t.Interval = TimeSpan.FromMilliseconds(250L, 0L);
      t.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        if (interview.IsPlanInFlight && (double)(Environment.TickCount64 - started) < maxSeconds * 1000.0)
        {
          return;
        }
        t.Stop();
        this.pendingTimers.Remove(t);
        then();
      };
      this.pendingTimers.Add(t);
      t.Start();
    }

    // Token: 0x06001082 RID: 4226 RVA: 0x005861C4 File Offset: 0x005861C4
    public void SkipForTests()
    {
      this.End("skipped");
    }

    // Token: 0x17000304 RID: 772
    // (get) Token: 0x06001083 RID: 4227 RVA: 0x005861D4 File Offset: 0x005861D4
    public IntPtr HwndForTests
    {
      get
      {
        return this.window.Hwnd;
      }
    }

    // Token: 0x17000305 RID: 773
    // (get) Token: 0x06001084 RID: 4228 RVA: 0x005861E4 File Offset: 0x005861E4
    public IReadOnlyList<TutorialWindow.Step> VisibleStepsForTests
    {
      get
      {
        return this.VisibleSteps.ToList<TutorialWindow.Step>();
      }
    }

    // Token: 0x06001085 RID: 4229 RVA: 0x005861F4 File Offset: 0x005861F4
    public void FinishAfterReveal()
    {
      if (!this.revealRunning)
      {
        return;
      }
      this.revealRunning = false;
      this.shown = false;
      OnboardingTutorialViewModel vm = this.Vm;
      if (vm != null)
      {
        vm.End("finished");
      }
      else
      {
        this.s.Settings.HasCompletedTutorial = true;
      }
      this.TrackResolved("finished", TutorialWindow.Step.Finale);
      Action<TutorialWindow.Step?> stepChanged = this.StepChanged;
      if (stepChanged != null)
      {
        stepChanged(null);
      }
      Action ended = this.Ended;
      if (ended == null)
      {
        return;
      }
      ended();
    }

    // Token: 0x17000306 RID: 774
    // (get) Token: 0x06001086 RID: 4230 RVA: 0x00586278 File Offset: 0x00586278
    // (set) Token: 0x06001087 RID: 4231 RVA: 0x00586280 File Offset: 0x00586280
    [Nullable(2)]
    public Func<bool> CanReveal
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000307 RID: 775
    // (get) Token: 0x06001088 RID: 4232 RVA: 0x0058628C File Offset: 0x0058628C
    // (set) Token: 0x06001089 RID: 4233 RVA: 0x00586294 File Offset: 0x00586294
    [Nullable(new byte[] { 2, 1 })]
    public Func<Task> RunReveal
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000308 RID: 776
    // (get) Token: 0x0600108A RID: 4234 RVA: 0x005862A0 File Offset: 0x005862A0
    // (set) Token: 0x0600108B RID: 4235 RVA: 0x005862A8 File Offset: 0x005862A8
    [Nullable(2)]
    public Func<bool> CanDrawFinale
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000309 RID: 777
    // (get) Token: 0x0600108C RID: 4236 RVA: 0x005862B4 File Offset: 0x005862B4
    // (set) Token: 0x0600108D RID: 4237 RVA: 0x005862BC File Offset: 0x005862BC
    [Nullable(new byte[] { 2, 1 })]
    public Func<Task> RunDrawnFinale
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x1700030A RID: 778
    // (get) Token: 0x0600108E RID: 4238 RVA: 0x005862C8 File Offset: 0x005862C8
    // (set) Token: 0x0600108F RID: 4239 RVA: 0x005862D0 File Offset: 0x005862D0
    [Nullable(2)]
    public IScreenDrawingCanvas Canvas
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700030B RID: 779
    // (get) Token: 0x06001090 RID: 4240 RVA: 0x005862DC File Offset: 0x005862DC
    // (set) Token: 0x06001091 RID: 4241 RVA: 0x005862E4 File Offset: 0x005862E4
    [Nullable(2)]
    public Action<bool> Music
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700030C RID: 780
    // (get) Token: 0x06001092 RID: 4242 RVA: 0x005862F0 File Offset: 0x005862F0
    // (set) Token: 0x06001093 RID: 4243 RVA: 0x005862F8 File Offset: 0x005862F8
    [Nullable(2)]
    public InterviewController Interview
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06001094 RID: 4244 RVA: 0x00586304 File Offset: 0x00586304
    private void ReadSystemVolume()
    {
      if (this.volumeReadInFlight)
      {
        return;
      }
      this.volumeReadInFlight = true;
      Task.Run(delegate
      {
        double? level;
        try
        {
          level = new double?(SystemVolume.EffectiveLevel());
        }
        catch (Exception)
        {
          level = null;
        }
        this.dq.TryEnqueue(delegate
        {
          this.volumeReadInFlight = false;
          if (level != null)
          {
            double valueOrDefault = level.GetValueOrDefault();
            this.systemVolume = new double?(valueOrDefault);
          }
        });
      });
    }

    // Token: 0x06001095 RID: 4245 RVA: 0x00586328 File Offset: 0x00586328
    private void Tick()
    {
      if (!this.window.Root.IsLoaded)
      {
        return;
      }
      TutorialWindow.Step step = this.step;
      bool flag = step <= TutorialWindow.Step.SpeakerCheck;
      if (flag)
      {
        int num = this.volumeTicks + 1;
        this.volumeTicks = num;
        if (num % 15 == 0)
        {
          this.ReadSystemVolume();
        }
      }
      if (this.step == TutorialWindow.Step.SpeakerCheck && !this.completed.Contains(this.step))
      {
        double? num2 = this.systemVolume;
        double num3 = 0.05;
        if ((num2.GetValueOrDefault() > num3) & (num2 != null))
        {
          ClickyAnalytics.TrackOnboardingTutorialSpeakerVolumeFixed("self");
          this.Complete(TutorialWindow.Step.SpeakerCheck, true);
          this.Render(false);
        }
      }
      if (this.step == TutorialWindow.Step.DrawDemo)
      {
        OnboardingTutorialViewModel vm = this.Vm;
        if (vm != null)
        {
          int num = this.volumeTicks;
          this.volumeTicks = num + 1;
          if (num % 10 == 0)
          {
            Func<bool> drawingShown = this.DrawingShown;
            if (drawingShown != null && drawingShown())
            {
              vm.NoteDrawingPresented();
            }
          }
        }
      }
      if (this.meter != null)
      {
        float level = this.s.Microphone.Level;
        double num4 = MicCheckLevel.FromLinearLevel(level);
        this.meter.SetLevel(num4);
        if (num4 > 0.25)
        {
          this.loudSeconds += 0.033;
        }
        else
        {
          this.loudSeconds = Math.Max(0.0, this.loudSeconds - 0.0165);
        }
        if (this.step == TutorialWindow.Step.MicCheck && !this.completed.Contains(TutorialWindow.Step.MicCheck))
        {
          this.micCheckSeconds += 0.033;
          if (level > 0f)
          {
            this.micCheckHeardAnyInput = true;
          }
          if (this.loudSeconds >= 0.25)
          {
            this.Complete(TutorialWindow.Step.MicCheck, false);
          }
          else if (MicCheckLevel.PassesByFallback(this.micCheckSeconds, this.micCheckHeardAnyInput))
          {
            string text = "tutorial";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(70, 1);
            defaultInterpolatedStringHandler.AppendLiteral("mic check passed by fallback after ");
            defaultInterpolatedStringHandler.AppendFormatted<double>(this.micCheckSeconds, "0.0");
            defaultInterpolatedStringHandler.AppendLiteral(" s (input heard, never loud enough)");
            Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
            ClickyAnalytics.TrackOnboardingTutorialMicCheckPassedByFallback(this.micCheckSeconds);
            this.Complete(TutorialWindow.Step.MicCheck, false);
          }
        }
      }
      if (this.liveCaps.Count > 0 && this.step != TutorialWindow.Step.TextMode)
      {
        bool flag2 = ((int)Native.GetAsyncKeyState(17) & 32768) != 0;
        bool flag3 = ((int)Native.GetAsyncKeyState(18) & 32768) != 0;
        for (int i = 0; i < this.liveCaps.Count; i++)
        {
          KeyCap keyCap = this.liveCaps[i];
          int? virtualKey = this.liveCaps[i].VirtualKey;
          bool flag4;
          if (virtualKey != null)
          {
            int valueOrDefault = virtualKey.GetValueOrDefault();
            flag4 = ((int)Native.GetAsyncKeyState(valueOrDefault) & 32768) != 0;
          }
          else
          {
            flag4 = ((i % 2 == 0) ? flag2 : flag3);
          }
          keyCap.Pressed = flag4;
        }
        return;
      }
      if (this.liveCaps.Count == 1)
      {
        this.liveCaps[0].Pressed = this.liveCaps[0].Pressed;
      }
    }

    // Token: 0x06001096 RID: 4246 RVA: 0x00586640 File Offset: 0x00586640
    public void NoteCompanionTurnFinished(CompanionTurnFinish finish)
    {
      OnboardingTutorialViewModel vm = this.Vm;
      if (vm == null || !vm.IsEngaged)
      {
        return;
      }
      if (finish.WordedTurn && finish.SpokenText.Length != 0)
      {
        InterviewController interview = this.Interview;
        if (interview == null || !interview.IsActive)
        {
          InterviewController interview2 = this.Interview;
          if (interview2 == null || !interview2.IsWrappingUp)
          {
            string text = "tutorial";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(50, 3);
            defaultInterpolatedStringHandler.AppendLiteral("companion turn finished on ");
            defaultInterpolatedStringHandler.AppendFormatted<TutorialWindow.Step>(this.step);
            defaultInterpolatedStringHandler.AppendLiteral(": ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(finish.HeardLines.Count);
            defaultInterpolatedStringHandler.AppendLiteral(" line(s) heard, drew=");
            defaultInterpolatedStringHandler.AppendFormatted<bool>(finish.DrewMarks);
            Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
            if (this.step == TutorialWindow.Step.DrawDemo && finish.DrewMarks)
            {
              vm.NoteDrawingPresented();
            }
            vm.NoteCompanionTurnCompletedAsync();
            return;
          }
        }
      }
    }

    // Token: 0x06001097 RID: 4247 RVA: 0x00586728 File Offset: 0x00586728
    private void OnVoiceState()
    {
      VoiceState state = this.s.Companion.State;
      this.UpdateStatus();
      OnboardingTutorialViewModel vm = this.Vm;
      if (vm != null)
      {
        if (this.lastVoice == VoiceState.Idle && state != VoiceState.Idle)
        {
          vm.NoteCompanionTurnStarted();
          if (this.step == TutorialWindow.Step.DrawDemo && state == VoiceState.Listening)
          {
            this.BeginDrawStepStageIfNeeded();
          }
        }
        this.lastVoice = state;
        return;
      }
      if (this.lastVoice == VoiceState.Responding && state == VoiceState.Idle)
      {
        switch (this.step)
        {
        case TutorialWindow.Step.VoiceHello:
          this.Complete(this.step, false);
          break;
        case TutorialWindow.Step.DrawDemo:
          this.After(0.6, delegate
          {
            this.Complete(TutorialWindow.Step.DrawDemo, true);
          });
          break;
        case TutorialWindow.Step.DrawToAsk:
          this.Complete(this.step, true);
          break;
        }
      }
      this.lastVoice = state;
    }

    // Token: 0x06001098 RID: 4248 RVA: 0x005867F0 File Offset: 0x005867F0
    private void OnPresentation()
    {
      NotchPresentation presentation = this.s.Presenter.Presentation;
      NotchPresentation notchPresentation = this.lastPresentation;
      this.lastPresentation = presentation;
      if (this.step != TutorialWindow.Step.TextMode)
      {
        return;
      }
      OnboardingTutorialViewModel vm = this.Vm;
      if (vm != null)
      {
        NotchPresentation.TextInput textInput = presentation as NotchPresentation.TextInput;
        if (textInput != null)
        {
          if (!(notchPresentation is NotchPresentation.TextInput))
          {
            this.composerSeen = true;
            this.composerClosingForCountdown = false;
            vm.NoteTextComposerOpened();
            return;
          }
          if (textInput.Draft.Length > 0)
          {
            vm.NoteTextComposerTyping();
            return;
          }
        }
        else
        {
          if (presentation is NotchPresentation.TextResponse)
          {
            this.typedTurnInFlight = true;
            return;
          }
          if (presentation is NotchPresentation.Idle && this.composerSeen)
          {
            this.composerSeen = false;
            if (this.typedTurnInFlight)
            {
              this.typedTurnInFlight = false;
              vm.NoteTypedTurnFinishedAsync();
            }
            else if (!this.composerClosingForCountdown)
            {
              vm.NoteTextComposerClosedByUser();
            }
            this.composerClosingForCountdown = false;
          }
        }
        return;
      }
      if (presentation is NotchPresentation.TextInput)
      {
        this.composerSeen = true;
        this.Complete(TutorialWindow.Step.TextMode, false);
        return;
      }
      if (this.composerSeen && presentation is NotchPresentation.Idle)
      {
        this.composerSeen = false;
        this.After(2.0, delegate
        {
          if (this.step == TutorialWindow.Step.TextMode)
          {
            this.Advance();
          }
        });
      }
    }

    // Token: 0x06001099 RID: 4249 RVA: 0x00586910 File Offset: 0x00586910
    public void MarkCompleted(TutorialWindow.Step st)
    {
      this.Complete(st, false);
    }

    // Token: 0x0600109A RID: 4250 RVA: 0x0058691C File Offset: 0x0058691C
    private void After(double seconds, Action a)
    {
      DispatcherQueueTimer t = this.dq.CreateTimer().Rooted();
      t.Interval = TimeSpan.FromSeconds(seconds);
      t.IsRepeating = false;
      this.pendingTimers.Add(t);
      t.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.pendingTimers.Remove(t);
        a();
      };
      t.Start();
    }

    // Token: 0x0600109B RID: 4251 RVA: 0x005869A4 File Offset: 0x005869A4
    private FrameworkElement DictationPage()
    {
      List<KeyCap> list = KeyCap.ForBinding(this.s.Settings.DictationBinding, KeyCap.Size.Compact, this.Theme);
      this.liveCaps.AddRange(list);
      TextBlock textBlock = Ui.Medium("Hold", 12.0, DS.Colors.TextSecondary);
      textBlock.Width = 52.0;
      textBlock.TextAlignment = TextAlignment.Right;
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      TextBlock textBlock2 = Ui.Medium("and say", 12.0, DS.Colors.TextSecondary);
      textBlock2.Width = 52.0;
      textBlock2.VerticalAlignment = VerticalAlignment.Center;
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 8.0,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      stackPanel.Children.Add(textBlock);
      foreach (KeyCap keyCap in list)
      {
        stackPanel.Children.Add(keyCap);
      }
      stackPanel.Children.Add(textBlock2);
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 8.0,
        Children = 
        {
          stackPanel,
          new SpokenBubble("Hey Claude, make me a billion-dollar app, make no mistakes")
        }
      };
      FrameworkElement frameworkElement = this.TerminalWindowCard();
      frameworkElement.Margin = new Thickness(0.0, 0.0, 0.0, 8.0);
      StackPanel stackPanel3 = new StackPanel
      {
        Spacing = 10.0,
        Children = { frameworkElement, stackPanel2 }
      };
      TextBox box = this.dictationBox;
      this.After(0.5, delegate
      {
        if (this.step == TutorialWindow.Step.Dictation && this.dictationBox == box && box != null)
        {
          box.Focus(FocusState.Programmatic);
        }
      });
      OnboardingTutorialStepInfo onboardingTutorialStepInfo = OnboardingTutorialViewModel.Info(OnboardingTutorialStep.Dictation);
      return this.Layout(onboardingTutorialStepInfo.Title, onboardingTutorialStepInfo.Body, stackPanel3);
    }

    // Token: 0x0600109C RID: 4252 RVA: 0x00586BBC File Offset: 0x00586BBC
    private FrameworkElement TerminalWindowCard()
    {
      TutorialWindow.<>c__DisplayClass177_0 CS$<>8__locals1 = new TutorialWindow.<>c__DisplayClass177_0();
      CS$<>8__locals1.<>4__this = this;
      TextBlock textBlock = Ui.Text("~/billion-dollar-app — claude", 10.0, FontWeights.Medium, DS.White(0.55), TextAlignment.Center, 0, false, true);
      textBlock.HorizontalAlignment = HorizontalAlignment.Center;
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 14.0,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Center
      };
      foreach (string text in new string[] { "\ue921", "\ue922", "\ue8bb" })
      {
        stackPanel.Children.Add(Icons.Glyph(text, 8.0, DS.White(0.55)));
      }
      Grid grid = new Grid
      {
        Padding = new Thickness(10.0, 7.0, 10.0, 7.0),
        Background = Ui.Brush(DS.White(0.07)),
        BorderBrush = Ui.Brush(DS.White(0.08)),
        BorderThickness = new Thickness(0.0, 0.0, 0.0, 1.0),
        IsHitTestVisible = false,
        Children = { textBlock, stackPanel }
      };
      TextBlock textBlock2 = Ui.Text("✻", 12.0, FontWeights.SemiBold, DS.Hex(14251863U, 1.0), TextAlignment.Left, 0, false, false);
      TextBlock textBlock3 = Ui.Text("Welcome to Claude Code!", 11.0, FontWeights.SemiBold, DS.White(0.85), TextAlignment.Left, 0, false, true);
      textBlock3.VerticalAlignment = VerticalAlignment.Center;
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 6.0,
        Children = { textBlock2, textBlock3 }
      };
      TextBlock textBlock4 = Ui.Text(">", 11.0, FontWeights.SemiBold, DS.White(0.55), TextAlignment.Left, 0, false, true);
      textBlock4.Margin = new Thickness(0.0, 1.0, 0.0, 0.0);
      AutomationProperties.SetAccessibilityView(textBlock4, AccessibilityView.Raw);
      TutorialWindow.<>c__DisplayClass177_0 CS$<>8__locals2 = CS$<>8__locals1;
      TextBox textBox = new TextBox();
      OnboardingTutorialViewModel vm = this.Vm;
      textBox.Text = ((vm != null) ? vm.DictationText : null) ?? "";
      textBox.PlaceholderText = "your words land here…";
      textBox.FontFamily = DS.Fonts.Mono;
      textBox.FontSize = 11.0;
      textBox.Height = 64.0;
      textBox.AcceptsReturn = true;
      textBox.TextWrapping = TextWrapping.Wrap;
      textBox.IsTextScaleFactorEnabled = false;
      textBox.Padding = new Thickness(5.0, 1.0, 0.0, 0.0);
      textBox.BorderThickness = new Thickness(0.0);
      textBox.Background = Ui.Transparent;
      CS$<>8__locals2.box = textBox;
      ComposerBox.StripChrome(CS$<>8__locals1.box);
      foreach (string text2 in new string[] { "TextControlForeground", "TextControlForegroundPointerOver", "TextControlForegroundFocused" })
      {
        CS$<>8__locals1.box.Resources[text2] = Ui.Brush(DS.White(0.9));
      }
      CS$<>8__locals1.box.Resources["TextControlPlaceholderForeground"] = (CS$<>8__locals1.box.Resources["TextControlPlaceholderForegroundPointerOver"] = (CS$<>8__locals1.box.Resources["TextControlPlaceholderForegroundFocused"] = Ui.Brush(DS.White(0.3))));
      CS$<>8__locals1.box.Foreground = Ui.Brush(DS.White(0.9));
      CS$<>8__locals1.box.TextChanged += delegate(object _, TextChangedEventArgs _)
      {
        OnboardingTutorialViewModel vm2 = CS$<>8__locals1.<>4__this.Vm;
        if (vm2 != null)
        {
          if (vm2.DictationText != CS$<>8__locals1.box.Text)
          {
            vm2.DictationText = CS$<>8__locals1.box.Text;
          }
          vm2.NoteDictationTextInserted(CS$<>8__locals1.box.Text);
          return;
        }
        if (!string.IsNullOrWhiteSpace(CS$<>8__locals1.box.Text))
        {
          CS$<>8__locals1.<>4__this.Complete(TutorialWindow.Step.Dictation, true);
        }
      };
      this.dictationBox = CS$<>8__locals1.box;
      Grid grid2 = new Grid
      {
        ColumnSpacing = 6.0
      };
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      Grid.SetColumn(CS$<>8__locals1.box, 1);
      grid2.Children.Add(textBlock4);
      grid2.Children.Add(CS$<>8__locals1.box);
      Border border = new Border
      {
        Padding = new Thickness(8.0, 7.0, 8.0, 7.0),
        CornerRadius = new CornerRadius(6.0),
        BorderBrush = Ui.Brush(DS.White(0.25)),
        BorderThickness = new Thickness(1.0),
        Child = grid2
      };
      StackPanel stackPanel3 = new StackPanel
      {
        Spacing = 8.0,
        Padding = new Thickness(12.0, 10.0, 12.0, 10.0),
        Children = { stackPanel2, border }
      };
      Border border2 = new Border
      {
        CornerRadius = new CornerRadius(10.0),
        Background = Ui.Brush(DS.Hex(1973790U, 1.0)),
        BorderBrush = Ui.Brush(DS.White(0.14)),
        BorderThickness = new Thickness(1.0),
        Child = new StackPanel
        {
          Children = { grid, stackPanel3 }
        }
      };
      Ui.ClipRounded(border2, 10.0);
      return new Grid
      {
        Width = 360.0,
        HorizontalAlignment = HorizontalAlignment.Center,
        Children = 
        {
          new ShadowLayer(10.0, DS.Black(0.35), 6.0, 2.0, 0.0),
          border2
        }
      };
    }

    // Token: 0x1700030D RID: 781
    // (get) Token: 0x0600109D RID: 4253 RVA: 0x00587284 File Offset: 0x00587284
    public bool IsDrawStepStageRunning
    {
      get
      {
        return this.drawStepStage != null;
      }
    }

    // Token: 0x0600109E RID: 4254 RVA: 0x00587294 File Offset: 0x00587294
    private void BeginDrawStepStageIfNeeded()
    {
      if (this.step != TutorialWindow.Step.DrawDemo || this.completed.Contains(TutorialWindow.Step.DrawDemo) || this.drawStepStage != null)
      {
        return;
      }
      OnboardingTutorialViewModel vm = this.Vm;
      if (vm != null)
      {
        vm.DrawStepStageOwnsCompletion = () => this.drawStepStage != null;
      }
      string text = "tutorial";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(98, 3);
      defaultInterpolatedStringHandler.AppendLiteral("draw step: press, the panel stays until the drawing starts (canvas open ");
      IScreenDrawingCanvas canvas = this.Canvas;
      defaultInterpolatedStringHandler.AppendFormatted<bool>(canvas != null && canvas.IsShowing);
      defaultInterpolatedStringHandler.AppendLiteral(", ");
      defaultInterpolatedStringHandler.AppendLiteral("drawing up ");
      IScreenDrawingCanvas canvas2 = this.Canvas;
      defaultInterpolatedStringHandler.AppendFormatted<bool>(canvas2 != null && canvas2.IsShowingDrawing);
      defaultInterpolatedStringHandler.AppendLiteral(", narrating ");
      IScreenDrawingCanvas canvas3 = this.Canvas;
      defaultInterpolatedStringHandler.AppendFormatted<bool>(canvas3 != null && canvas3.IsStreamingNarration);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      this.window.SetTemporarilyHiddenFromOwnCapture(true);
      CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
      this.drawStepStage = cancellationTokenSource;
      this.RunDrawStepStageAsync(cancellationTokenSource);
    }

    // Token: 0x0600109F RID: 4255 RVA: 0x005873A0 File Offset: 0x005873A0
    private Task RunDrawStepStageAsync(CancellationTokenSource stage)
    {
      TutorialWindow.<RunDrawStepStageAsync>d__185 <RunDrawStepStageAsync>d__;
      <RunDrawStepStageAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
      <RunDrawStepStageAsync>d__.<>4__this = this;
      <RunDrawStepStageAsync>d__.stage = stage;
      <RunDrawStepStageAsync>d__.<>1__state = -1;
      <RunDrawStepStageAsync>d__.<>t__builder.Start<TutorialWindow.<RunDrawStepStageAsync>d__185>(ref <RunDrawStepStageAsync>d__);
      return <RunDrawStepStageAsync>d__.<>t__builder.Task;
    }

    // Token: 0x060010A0 RID: 4256 RVA: 0x005873EC File Offset: 0x005873EC
    private static DateTime Max(DateTime a, DateTime b)
    {
      if (!(a > b))
      {
        return b;
      }
      return a;
    }

    // Token: 0x060010A1 RID: 4257 RVA: 0x005873FC File Offset: 0x005873FC
    private void CancelDrawStepStage(bool bringPanelBack)
    {
      CancellationTokenSource cancellationTokenSource = this.drawStepStage;
      if (cancellationTokenSource == null)
      {
        return;
      }
      cancellationTokenSource.Cancel();
      this.drawStepStage = null;
      this.window.SetTemporarilyHiddenFromOwnCapture(false);
      if (bringPanelBack && this.shown)
      {
        this.SetPanelHidden(false, 0.0);
      }
    }

    // Token: 0x060010A2 RID: 4258 RVA: 0x00587448 File Offset: 0x00587448
    private void SetPanelHidden(bool hidden, double fadeSeconds = 0.0)
    {
      if (!hidden)
      {
        this.window.Root.Opacity = 1.0;
        Anim.Opacity(this.window.Root, 1f, 0.01, null, 0.0);
        this.window.ShowNoActivate();
        return;
      }
      if (fadeSeconds > 0.0 && !Anim.Instant)
      {
        Anim.Opacity(this.window.Root, 0f, fadeSeconds, Easing.EaseInOut(Ui.Compositor), 0.0);
        this.After(fadeSeconds, new Action(this.window.HideWindow));
        return;
      }
      this.window.HideWindow();
    }

    // Token: 0x060010A3 RID: 4259 RVA: 0x00587508 File Offset: 0x00587508
    private void DrawingStepChanged(TutorialWindow.Step from, TutorialWindow.Step to)
    {
      if (from == to)
      {
        return;
      }
      if (from == TutorialWindow.Step.ClickyDrive)
      {
        this.CancelDriveDemo();
      }
      bool flag = from - TutorialWindow.Step.DrawDemo <= 1;
      if (flag)
      {
        IScreenDrawingCanvas canvas = this.Canvas;
        if (canvas != null && (canvas.IsShowing || canvas.IsStreamingNarration))
        {
          canvas.Clear(true);
        }
      }
    }

    // Token: 0x060010A4 RID: 4260 RVA: 0x00587554 File Offset: 0x00587554
    private void PopContinue(GelButton button)
    {
      if (Anim.Instant || Anim.ReduceMotion)
      {
        return;
      }
      button.Loaded += delegate(object _, RoutedEventArgs _)
      {
        Vector3 center = new Vector3((float)button.ActualWidth / 2f, (float)button.ActualHeight / 2f, 0f);
        Anim.ScaleSpring(button, 1.14f, new SwiftSpring(0.22, 0.5), new Vector3?(center));
        this.After(0.2, delegate
        {
          Anim.ScaleSpring(button, 1f, new SwiftSpring(0.42, 0.55), new Vector3?(center));
        });
      };
    }

    // Token: 0x060010A5 RID: 4261 RVA: 0x0058759C File Offset: 0x0058759C
    private FrameworkElement ClickyDrivePage()
    {
      TourDrive.Completed = delegate
      {
        this.dq.TryEnqueue(delegate
        {
          OnboardingTutorialViewModel vm = this.Vm;
          if (vm != null)
          {
            vm.NoteDriveDemoFinished();
            return;
          }
          this.Complete(TutorialWindow.Step.ClickyDrive, false);
        });
      };
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 7.0,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      TextBlock textBlock = Ui.Medium("Hold", 11.5, DS.Colors.TextSecondary);
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      List<KeyCap> list = KeyCap.ForBinding(this.s.Settings.PushToTalkBinding, KeyCap.Size.Mini, this.Theme);
      this.liveCaps.AddRange(list);
      TextBlock textBlock2 = Ui.Medium("and say", 11.5, DS.Colors.TextSecondary);
      textBlock2.VerticalAlignment = VerticalAlignment.Center;
      stackPanel.Children.Add(textBlock);
      UIElementCollection children = stackPanel.Children;
      List<KeyCap> list2 = list;
      int num = 0;
      UIElement[] array = new UIElement[list2.Count];
      foreach (KeyCap keyCap in list2)
      {
        array[num] = keyCap;
        num++;
      }
      children.Add(Ui.H(7.0, array));
      stackPanel.Children.Add(textBlock2);
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 8.0,
        Children = 
        {
          stackPanel,
          new SpokenBubble("Please drive my computer and make a list")
        }
      };
      Func<Color, FrameworkElement, Func<bool>, FrameworkElement> stepContent = TourDrive.StepContent;
      FrameworkElement frameworkElement;
      if (stepContent != null)
      {
        frameworkElement = stepContent(this.Theme, stackPanel2, () => this.completed.Contains(TutorialWindow.Step.ClickyDrive));
      }
      else
      {
        Grid grid = new Grid
        {
          Height = 14.0
        };
        this.statusLine = grid;
        this.UpdateStatus();
        frameworkElement = new StackPanel
        {
          Spacing = 12.0,
          Children = { stackPanel2, grid }
        };
      }
      return this.Layout("I can drive your computer too", "Ask me to do it and I'll take the mouse while you watch. Move it anytime and it's yours again.", frameworkElement);
    }

    // Token: 0x060010A6 RID: 4262 RVA: 0x005877A0 File Offset: 0x005877A0
    public bool InterceptPushToTalkRelease()
    {
      if (!this.shown || this.revealRunning || this.step != TutorialWindow.Step.ClickyDrive)
      {
        return false;
      }
      Action run = TourDrive.Run;
      if (run != null)
      {
        run();
      }
      else
      {
        Log.Info("tutorial", "drive step: no drive in this build; the ask completes the step");
        OnboardingTutorialViewModel vm = this.Vm;
        if (vm != null)
        {
          vm.NoteDriveDemoFinished();
        }
        else
        {
          this.Complete(TutorialWindow.Step.ClickyDrive, false);
        }
      }
      return true;
    }

    // Token: 0x1700030E RID: 782
    // (get) Token: 0x060010A7 RID: 4263 RVA: 0x00587804 File Offset: 0x00587804
    public bool IsOnDriveStep
    {
      get
      {
        return this.shown && !this.revealRunning && this.step == TutorialWindow.Step.ClickyDrive;
      }
    }

    // Token: 0x1700030F RID: 783
    // (get) Token: 0x060010A8 RID: 4264 RVA: 0x00587824 File Offset: 0x00587824
    public bool IsDriveDemoDriving
    {
      get
      {
        if (this.shown && this.step == TutorialWindow.Step.ClickyDrive)
        {
          Func<bool> isDriving = TourDrive.IsDriving;
          return isDriving != null && isDriving();
        }
        return false;
      }
    }

    // Token: 0x060010A9 RID: 4265 RVA: 0x0058784C File Offset: 0x0058784C
    private void CancelDriveDemo()
    {
      Action cancel = TourDrive.Cancel;
      if (cancel == null)
      {
        return;
      }
      cancel();
    }

    // Token: 0x060010AA RID: 4266 RVA: 0x00587860 File Offset: 0x00587860
    internal void PressShowMeForTests()
    {
      GelButton gelButton = this.finaleShowMe;
      if (gelButton == null)
      {
        return;
      }
      gelButton.RaiseClick();
    }

    // Token: 0x04000711 RID: 1809
    private readonly AppServices s;

    // Token: 0x04000712 RID: 1810
    private readonly IAppActions actions;

    // Token: 0x04000713 RID: 1811
    private readonly PanelWindow window;

    // Token: 0x04000714 RID: 1812
    private readonly DispatcherQueue dq;

    // Token: 0x04000715 RID: 1813
    private readonly Grid card = new Grid();

    // Token: 0x04000716 RID: 1814
    private readonly Grid stage = new Grid();

    // Token: 0x04000717 RID: 1815
    private readonly ContentControl dotsSlot = new ContentControl
    {
      HorizontalContentAlignment = HorizontalAlignment.Center,
      IsTabStop = false
    };

    // Token: 0x04000718 RID: 1816
    private readonly Grid footer = new Grid();

    // Token: 0x04000719 RID: 1817
    private readonly Pressable back;

    // Token: 0x0400071A RID: 1818
    private readonly Pressable music;

    // Token: 0x0400071B RID: 1819
    private readonly Pressable skip;

    // Token: 0x0400071C RID: 1820
    private readonly HashSet<TutorialWindow.Step> completed = new HashSet<TutorialWindow.Step>();

    // Token: 0x0400071D RID: 1821
    private readonly DispatcherQueueTimer poll;

    // Token: 0x0400071E RID: 1822
    private TutorialWindow.Step step;

    // Token: 0x0400071F RID: 1823
    private int furthest;

    // Token: 0x04000720 RID: 1824
    private bool showSpeakerCheck;

    // Token: 0x04000721 RID: 1825
    private bool musicMuted;

    // Token: 0x04000722 RID: 1826
    [Nullable(2)]
    private GelButton continueButton;

    // Token: 0x04000723 RID: 1827
    [Nullable(2)]
    private MicMeter meter;

    // Token: 0x04000724 RID: 1828
    private readonly List<KeyCap> liveCaps = new List<KeyCap>();

    // Token: 0x04000725 RID: 1829
    private double loudSeconds;

    // Token: 0x04000726 RID: 1830
    private double micCheckSeconds;

    // Token: 0x04000727 RID: 1831
    private bool micCheckHeardAnyInput;

    // Token: 0x04000728 RID: 1832
    private VoiceState lastVoice;

    // Token: 0x04000729 RID: 1833
    [Nullable(2)]
    private FrameworkElement statusLine;

    // Token: 0x0400072A RID: 1834
    private static readonly SwiftSpring PageSpring = new SwiftSpring(0.38, 0.86);

    // Token: 0x0400072B RID: 1835
    private const double PanelWidth = 500.0;

    // Token: 0x0400072C RID: 1836
    private const double InterviewPanelWidth = 760.0;

    // Token: 0x0400072D RID: 1837
    private const double StageHeight = 416.0;

    // Token: 0x0400072E RID: 1838
    private const double InterviewStageHeight = 440.0;

    // Token: 0x0400072F RID: 1839
    private const double PanelChromeHeight = 156.0;

    // Token: 0x04000732 RID: 1842
    private long? startedAt;

    // Token: 0x04000733 RID: 1843
    private bool bringingForward;

    // Token: 0x04000734 RID: 1844
    private bool interviewHooked;

    // Token: 0x04000735 RID: 1845
    private bool shown;

    // Token: 0x04000739 RID: 1849
    private bool vmHooked;

    // Token: 0x0400073A RID: 1850
    private bool composerClosingForCountdown;

    // Token: 0x0400073B RID: 1851
    private bool typedTurnInFlight;

    // Token: 0x0400073E RID: 1854
    private bool revealRunning;

    // Token: 0x04000746 RID: 1862
    private int volumeTicks;

    // Token: 0x04000747 RID: 1863
    private double? systemVolume;

    // Token: 0x04000748 RID: 1864
    private bool volumeReadInFlight;

    // Token: 0x04000749 RID: 1865
    private bool composerSeen;

    // Token: 0x0400074A RID: 1866
    [Nullable(2)]
    private NotchPresentation lastPresentation;

    // Token: 0x0400074B RID: 1867
    private readonly HashSet<DispatcherQueueTimer> pendingTimers = new HashSet<DispatcherQueueTimer>();

    // Token: 0x0400074C RID: 1868
    [Nullable(2)]
    private TextBox dictationBox;

    // Token: 0x0400074D RID: 1869
    public const double DrawStepClearLeadSeconds = 0.7;

    // Token: 0x0400074E RID: 1870
    public const double DrawStepLastLineMinimumSeconds = 2.5;

    // Token: 0x0400074F RID: 1871
    [Nullable(2)]
    private CancellationTokenSource drawStepStage;

    // Token: 0x04000750 RID: 1872
    private bool popContinueOnRender;

    // Token: 0x04000751 RID: 1873
    [Nullable(2)]
    private GelButton finaleShowMe;

    // Token: 0x02000667 RID: 1639
    [NullableContext(0)]
    public enum Step
    {
      // Token: 0x04001D50 RID: 7504
      MicCheck,
      // Token: 0x04001D51 RID: 7505
      SpeakerCheck,
      // Token: 0x04001D52 RID: 7506
      VoiceHello,
      // Token: 0x04001D53 RID: 7507
      ClickyInterview,
      // Token: 0x04001D54 RID: 7508
      DrawDemo,
      // Token: 0x04001D55 RID: 7509
      DrawToAsk,
      // Token: 0x04001D56 RID: 7510
      ClickyDrive,
      // Token: 0x04001D57 RID: 7511
      TextMode,
      // Token: 0x04001D58 RID: 7512
      Dictation,
      // Token: 0x04001D59 RID: 7513
      Finale
    }
  }
}
