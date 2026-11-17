using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Services;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.Wiring
{
  // Token: 0x02000094 RID: 148
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CompanionNotchBridge : IDisposable
  {
    // Token: 0x060005DC RID: 1500 RVA: 0x0052E4C4 File Offset: 0x0052E4C4
    public CompanionNotchBridge(AppServices services, DispatcherQueue dispatcher)
    {
      this.s = services;
      this.dq = dispatcher;
      this.s.Shortcuts.ShortcutTriggered += this.OnShortcut;
      this.s.Companion.StateChanged += this.OnState;
      this.s.Companion.ResponseTextUpdated += this.OnResponse;
    }

    // Token: 0x060005DD RID: 1501 RVA: 0x0052E53C File Offset: 0x0052E53C
    [NullableContext(2)]
    private void OnShortcut(object sender, ShortcutAction a)
    {
      this.dq.TryEnqueue(delegate
      {
        if (this.s.Settings.IsPaused && a != ShortcutAction.Escape)
        {
          return;
        }
        switch (a)
        {
        case ShortcutAction.PushToTalkDown:
          this.voiceTurn = true;
          this.s.Microphone.Start();
          this.s.Presenter.ShowLiveEvent(new NotchLiveEvent.ActivityListening(), null);
          this.s.Companion.BeginPushToTalkAsync();
          return;
        case ShortcutAction.PushToTalkUp:
          this.s.Microphone.Stop();
          this.s.Companion.EndPushToTalkAsync();
          return;
        case ShortcutAction.TextComposerToggle:
          if (this.s.Presenter.Presentation is NotchPresentation.TextInput)
          {
            this.s.Presenter.ReturnToIdle();
            return;
          }
          this.voiceTurn = false;
          this.s.Presenter.Present(new NotchPresentation.TextInput("", null));
          return;
        case ShortcutAction.DictationDown:
        case ShortcutAction.DictationUp:
          break;
        case ShortcutAction.Escape:
        {
          NotchPresentation presentation = this.s.Presenter.Presentation;
          bool flag = presentation is NotchPresentation.TextInput || presentation is NotchPresentation.TextResponse;
          if (flag || this.s.Companion.State != VoiceState.Idle)
          {
            this.s.Companion.Cancel();
            this.s.Presenter.ReturnToIdle();
          }
          break;
        }
        default:
          return;
        }
      });
    }

    // Token: 0x060005DE RID: 1502 RVA: 0x0052E578 File Offset: 0x0052E578
    private void OnState([Nullable(2)] object sender, EventArgs e)
    {
      this.dq.TryEnqueue(delegate
      {
        NotchPresentation presentation = this.s.Presenter.Presentation;
        bool flag = presentation is NotchPresentation.TextResponse || presentation is NotchPresentation.TextInput;
        if (flag)
        {
          return;
        }
        if (!this.voiceTurn)
        {
          return;
        }
        switch (this.s.Companion.State)
        {
        case VoiceState.Idle:
        {
          this.voiceTurn = false;
          NotchPresentation.LiveEvent liveEvent = this.s.Presenter.Presentation as NotchPresentation.LiveEvent;
          if (liveEvent != null)
          {
            NotchLiveEvent @event = liveEvent.Event;
            if (@event is NotchLiveEvent.ActivityListening || @event is NotchLiveEvent.ActivityThinking || @event is NotchLiveEvent.ActivitySpeaking)
            {
              flag = true;
              goto IL_0105;
            }
          }
          flag = false;
          IL_0105:
          if (flag)
          {
            this.s.Presenter.ReturnToIdle();
          }
          return;
        }
        case VoiceState.Listening:
          this.s.Presenter.ShowLiveEvent(new NotchLiveEvent.ActivityListening(), null);
          return;
        case VoiceState.Processing:
          this.s.Presenter.ShowLiveEvent(new NotchLiveEvent.ActivityThinking(), null);
          return;
        case VoiceState.Responding:
          this.s.Presenter.ShowLiveEvent(new NotchLiveEvent.ActivitySpeaking(), null);
          return;
        default:
          return;
        }
      });
    }

    // Token: 0x060005DF RID: 1503 RVA: 0x0052E594 File Offset: 0x0052E594
    private void OnResponse([Nullable(2)] object sender, string text)
    {
      this.dq.TryEnqueue(delegate
      {
        NotchPresentation.TextResponse textResponse = this.s.Presenter.Presentation as NotchPresentation.TextResponse;
        if (textResponse != null)
        {
          this.s.Presenter.Present(new NotchPresentation.TextResponse(textResponse.Question, text, false));
        }
      });
    }

    // Token: 0x060005E0 RID: 1504 RVA: 0x0052E5D0 File Offset: 0x0052E5D0
    public void Dispose()
    {
      this.s.Shortcuts.ShortcutTriggered -= this.OnShortcut;
      this.s.Companion.StateChanged -= this.OnState;
      this.s.Companion.ResponseTextUpdated -= this.OnResponse;
    }

    // Token: 0x0400013D RID: 317
    private readonly AppServices s;

    // Token: 0x0400013E RID: 318
    private readonly DispatcherQueue dq;

    // Token: 0x0400013F RID: 319
    private bool voiceTurn;
  }
}
