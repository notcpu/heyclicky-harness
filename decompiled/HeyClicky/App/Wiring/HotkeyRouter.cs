using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Browser;
using HeyClicky.App.Proactive;
using HeyClicky.App.Services;
using HeyClicky.Core.Voice;
using HeyClicky.Platform.Input;
using HeyClicky.Platform.Win32;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.Wiring
{
  // Token: 0x0200009B RID: 155
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class HotkeyRouter : IDisposable
  {
    // Token: 0x170000EC RID: 236
    // (get) Token: 0x06000620 RID: 1568 RVA: 0x0052F20C File Offset: 0x0052F20C
    // (set) Token: 0x06000621 RID: 1569 RVA: 0x0052F214 File Offset: 0x0052F214
    [Nullable(1)]
    public Func<bool> AllowsPushToTalk
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = () => true;

    // Token: 0x170000ED RID: 237
    // (get) Token: 0x06000622 RID: 1570 RVA: 0x0052F220 File Offset: 0x0052F220
    // (set) Token: 0x06000623 RID: 1571 RVA: 0x0052F228 File Offset: 0x0052F228
    [Nullable(1)]
    public Func<bool> AllowsTextMode
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = () => true;

    // Token: 0x170000EE RID: 238
    // (get) Token: 0x06000624 RID: 1572 RVA: 0x0052F234 File Offset: 0x0052F234
    // (set) Token: 0x06000625 RID: 1573 RVA: 0x0052F23C File Offset: 0x0052F23C
    public Func<bool> NotchChatToggle { get; set; }

    // Token: 0x170000EF RID: 239
    // (get) Token: 0x06000626 RID: 1574 RVA: 0x0052F248 File Offset: 0x0052F248
    // (set) Token: 0x06000627 RID: 1575 RVA: 0x0052F250 File Offset: 0x0052F250
    public Func<bool> NotchEscape { get; set; }

    // Token: 0x170000F0 RID: 240
    // (get) Token: 0x06000628 RID: 1576 RVA: 0x0052F25C File Offset: 0x0052F25C
    // (set) Token: 0x06000629 RID: 1577 RVA: 0x0052F264 File Offset: 0x0052F264
    public Func<bool> NotchEscapeWouldClaim { get; set; }

    // Token: 0x170000F1 RID: 241
    // (get) Token: 0x0600062A RID: 1578 RVA: 0x0052F270 File Offset: 0x0052F270
    // (set) Token: 0x0600062B RID: 1579 RVA: 0x0052F278 File Offset: 0x0052F278
    public Func<bool> NotchViewerEscapeWouldClaim { get; set; }

    // Token: 0x170000F2 RID: 242
    // (get) Token: 0x0600062C RID: 1580 RVA: 0x0052F284 File Offset: 0x0052F284
    // (set) Token: 0x0600062D RID: 1581 RVA: 0x0052F28C File Offset: 0x0052F28C
    public Func<bool> InterceptReleaseAfterPress { get; set; }

    // Token: 0x0600062E RID: 1582 RVA: 0x0052F298 File Offset: 0x0052F298
    [NullableContext(1)]
    public HotkeyRouter(AppServices services, RealStack real, DispatcherQueue dq)
    {
      HotkeyRouter <>4__this = this;
      this.s = services;
      this.real = real;
      this.dq = dq;
      this.s.Shortcuts.ShortcutTriggered += this.OnShortcut;
      ShortcutMonitor shortcuts = real.Shortcuts;
      if (shortcuts != null)
      {
        shortcuts.ConsumesEscape = new Func<bool>(this.WouldHandleEscape);
      }
      Func<bool> <>9__1;
      this.s.Presenter.PresentationChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        bool flag = <>4__this.s.Presenter.Presentation is NotchPresentation.TextInput;
        if (flag == <>4__this.textInputOpen)
        {
          return;
        }
        <>4__this.textInputOpen = flag;
        if (flag)
        {
          CompanionSession companion = real.Companion;
          Func<bool> func;
          if ((func = <>9__1) == null)
          {
            func = (<>9__1 = () => <>4__this.s.Presenter.Presentation is NotchPresentation.TextInput);
          }
          companion.BeginPageReadForTextInput(func);
          return;
        }
        real.Companion.TextInputClosed();
      };
    }

    // Token: 0x0600062F RID: 1583 RVA: 0x0052F37C File Offset: 0x0052F37C
    public bool WouldHandleEscape()
    {
      Func<bool> driveClaimsEscape = this.DriveClaimsEscape;
      if (driveClaimsEscape != null && driveClaimsEscape())
      {
        return true;
      }
      Func<bool> lessonReplaying = this.LessonReplaying;
      if (lessonReplaying != null && lessonReplaying())
      {
        return true;
      }
      if (this.s.Settings.IsPaused)
      {
        return false;
      }
      if (ChromeSetupFlow.Shared.IsShowingHow)
      {
        return true;
      }
      if (this.real.Dictation.IsSessionActive)
      {
        return true;
      }
      Func<bool> notchViewerEscapeWouldClaim = this.NotchViewerEscapeWouldClaim;
      if (notchViewerEscapeWouldClaim != null && notchViewerEscapeWouldClaim())
      {
        return true;
      }
      if (this.pressStarted != 0L)
      {
        return true;
      }
      if (this.real.Companion.IsOnboardingAnswerInFlight)
      {
        return false;
      }
      Func<bool> notchEscapeWouldClaim = this.NotchEscapeWouldClaim;
      if (notchEscapeWouldClaim != null && notchEscapeWouldClaim())
      {
        return true;
      }
      if (this.s.Companion.State != VoiceState.Idle)
      {
        return true;
      }
      if (this.real.Canvas.IsShowing)
      {
        return true;
      }
      NotchPresentation presentation = this.s.Presenter.Presentation;
      return presentation is NotchPresentation.TextInput || presentation is NotchPresentation.TextResponse;
    }

    // Token: 0x170000F3 RID: 243
    // (get) Token: 0x06000630 RID: 1584 RVA: 0x0052F488 File Offset: 0x0052F488
    // (set) Token: 0x06000631 RID: 1585 RVA: 0x0052F490 File Offset: 0x0052F490
    public Func<bool> DriveClaimsEscape { get; set; }

    // Token: 0x170000F4 RID: 244
    // (get) Token: 0x06000632 RID: 1586 RVA: 0x0052F49C File Offset: 0x0052F49C
    // (set) Token: 0x06000633 RID: 1587 RVA: 0x0052F4A4 File Offset: 0x0052F4A4
    public Func<bool> DriveEscape { get; set; }

    // Token: 0x170000F5 RID: 245
    // (get) Token: 0x06000634 RID: 1588 RVA: 0x0052F4B0 File Offset: 0x0052F4B0
    // (set) Token: 0x06000635 RID: 1589 RVA: 0x0052F4B8 File Offset: 0x0052F4B8
    public Func<bool> LessonReplaying { get; set; }

    // Token: 0x170000F6 RID: 246
    // (get) Token: 0x06000636 RID: 1590 RVA: 0x0052F4C4 File Offset: 0x0052F4C4
    // (set) Token: 0x06000637 RID: 1591 RVA: 0x0052F4CC File Offset: 0x0052F4CC
    public Action StopLessonReplay { get; set; }

    // Token: 0x06000638 RID: 1592 RVA: 0x0052F4D8 File Offset: 0x0052F4D8
    private void OnShortcut(object sender, ShortcutAction action)
    {
      if (action == ShortcutAction.SystemScreenshotShortcut)
      {
        ScreenCaptureYield.NoteCaptureUiMayBeOpening("screenshot shortcut pressed");
        return;
      }
      bool flag = this.s.Settings.IsPaused;
      if (flag)
      {
        ShortcutAction action2 = action;
        bool flag2 = action2 - ShortcutAction.DictationUp <= 2;
        flag = !flag2;
      }
      if (flag)
      {
        return;
      }
      switch (action)
      {
      case ShortcutAction.PushToTalkDown:
        if (!this.AllowsPushToTalk())
        {
          return;
        }
        if (this.real.Dictation.IsCapturingAudio)
        {
          Log.Info("hotkeys", "push-to-talk ignored: dictation is recording");
          this.pressStarted = 0L;
          return;
        }
        this.pressStarted = Environment.TickCount64;
        this.real.Capture.Prewarm();
        this.real.MicrophoneStartCue.Play("textOpen", null);
        this.s.Companion.BeginPushToTalkAsync();
        return;
      case ShortcutAction.PushToTalkUp:
      {
        if (this.pressStarted == 0L)
        {
          return;
        }
        long num = Environment.TickCount64 - this.pressStarted;
        this.pressStarted = 0L;
        Func<bool> interceptReleaseAfterPress = this.InterceptReleaseAfterPress;
        if (interceptReleaseAfterPress != null && interceptReleaseAfterPress())
        {
          this.s.Companion.Cancel();
          this.real.Highlight.Clear();
          return;
        }
        this.s.Companion.EndPushToTalkAsync();
        if (num < 500L)
        {
          this.real.Highlight.Clear();
          return;
        }
        break;
      }
      case ShortcutAction.TextComposerToggle:
        if (!this.AllowsTextMode())
        {
          return;
        }
        this.dq.TryEnqueue(delegate
        {
          Func<bool> notchChatToggle = this.NotchChatToggle;
          if (notchChatToggle != null && notchChatToggle())
          {
            return;
          }
          if (this.s.Presenter.Presentation is NotchPresentation.TextInput)
          {
            this.s.Presenter.Present(new NotchPresentation.Idle());
            return;
          }
          this.real.Audio.PlayEarcon("textOpen");
          this.s.Presenter.Present(new NotchPresentation.TextInput("", null));
        });
        return;
      case ShortcutAction.DictationDown:
      case ShortcutAction.DictationUp:
      case ShortcutAction.DictationCancel:
        this.real.Dictation.HandleShortcut(action);
        return;
      case ShortcutAction.Escape:
      {
        Func<bool> driveEscape = this.DriveEscape;
        if (driveEscape != null && driveEscape())
        {
          return;
        }
        if (ChromeSetupFlow.Shared.IsShowingHow)
        {
          this.dq.TryEnqueue(delegate
          {
            ChromeSetupFlow.Shared.StopShowingHowForEscape();
          });
          return;
        }
        Func<bool> lessonReplaying = this.LessonReplaying;
        if (lessonReplaying != null && lessonReplaying())
        {
          Action stopReplay = this.StopLessonReplay;
          if (stopReplay != null)
          {
            this.dq.TryEnqueue(delegate
            {
              stopReplay();
            });
            return;
          }
        }
        ProactiveCoordinator proactive = ProactiveCoordinator.Current;
        if (proactive != null && (proactive.AdjustingCardId != null || proactive.IsRemoteTaskVoiceActive))
        {
          this.dq.TryEnqueue(delegate
          {
            proactive.CancelRemoteTaskVoice();
            proactive.CancelAdjustingFromEscape();
          });
          return;
        }
        if (this.real.Dictation.CancelActiveSession())
        {
          return;
        }
        Func<bool> notchViewerEscapeWouldClaim = this.NotchViewerEscapeWouldClaim;
        if (notchViewerEscapeWouldClaim != null && notchViewerEscapeWouldClaim())
        {
          Func<bool> closeViewer = this.NotchEscape;
          if (closeViewer != null)
          {
            this.dq.TryEnqueue(delegate
            {
              closeViewer();
            });
            return;
          }
        }
        if (this.pressStarted == 0L && this.real.Companion.IsOnboardingAnswerInFlight)
        {
          return;
        }
        Func<bool> notchEscapeWouldClaim = this.NotchEscapeWouldClaim;
        if (notchEscapeWouldClaim != null && notchEscapeWouldClaim())
        {
          Func<bool> notchEscape = this.NotchEscape;
          if (notchEscape != null)
          {
            this.dq.TryEnqueue(delegate
            {
              notchEscape();
            });
            return;
          }
        }
        bool flag3 = this.s.Companion.State > VoiceState.Idle;
        NotchPresentation presentation = this.s.Presenter.Presentation;
        flag = presentation is NotchPresentation.TextInput || presentation is NotchPresentation.TextResponse;
        bool flag4 = flag;
        if (!flag3 && !flag4 && !this.real.Canvas.IsShowing)
        {
          return;
        }
        this.real.Canvas.PlayClearSoundIfShowingDrawing();
        this.s.Companion.Cancel();
        this.real.Highlight.Clear();
        if (flag4)
        {
          this.dq.TryEnqueue(delegate
          {
            this.s.Presenter.Present(new NotchPresentation.Idle());
          });
          return;
        }
        break;
      }
      case ShortcutAction.HandsFreeDictationToggle:
        this.real.Dictation.HandleShortcut(action);
        break;
      case ShortcutAction.ProactiveDebugSweep:
      case ShortcutAction.MorningPreviewChord:
        this.dq.TryEnqueue(delegate
        {
          ProactiveCoordinator proactiveCoordinator = ProactiveCoordinator.Current;
          if (proactiveCoordinator == null)
          {
            return;
          }
          proactiveCoordinator.OnDebugChord(action);
        });
        return;
      default:
        return;
      }
    }

    // Token: 0x06000639 RID: 1593 RVA: 0x0052F930 File Offset: 0x0052F930
    public void Dispose()
    {
      this.s.Shortcuts.ShortcutTriggered -= this.OnShortcut;
    }

    // Token: 0x04000163 RID: 355
    [Nullable(1)]
    private readonly AppServices s;

    // Token: 0x04000164 RID: 356
    [Nullable(1)]
    private readonly RealStack real;

    // Token: 0x04000165 RID: 357
    [Nullable(1)]
    private readonly DispatcherQueue dq;

    // Token: 0x04000166 RID: 358
    private long pressStarted;

    // Token: 0x0400016E RID: 366
    private volatile bool textInputOpen;
  }
}
