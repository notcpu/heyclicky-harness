using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.Core.Notch
{
  // Token: 0x02000114 RID: 276
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class NotchPresenter : INotchPresenter, IDisposable
  {
    // Token: 0x14000026 RID: 38
    // (add) Token: 0x060011BF RID: 4543 RVA: 0x0039DEE4 File Offset: 0x0039DEE4
    // (remove) Token: 0x060011C0 RID: 4544 RVA: 0x0039DF1C File Offset: 0x0039DF1C
    public event EventHandler PresentationChanged;

    // Token: 0x14000027 RID: 39
    // (add) Token: 0x060011C1 RID: 4545 RVA: 0x0039DF54 File Offset: 0x0039DF54
    // (remove) Token: 0x060011C2 RID: 4546 RVA: 0x0039DF8C File Offset: 0x0039DF8C
    public event Action HoverDwellCompleted;

    // Token: 0x060011C3 RID: 4547 RVA: 0x0039DFC4 File Offset: 0x0039DFC4
    public NotchPresenter(Func<DateTimeOffset> clock = null, bool autoTick = true)
    {
      Func<DateTimeOffset> func = clock;
      if (clock == null && (func = NotchPresenter.<>c.<>9__45_0) == null)
      {
        func = (NotchPresenter.<>c.<>9__45_0 = () => DateTimeOffset.UtcNow);
      }
      this.clock = func;
      this.lastTick = this.clock();
      if (autoTick)
      {
        this.ticker = new Timer(delegate(object _)
        {
          this.Tick();
        }, null, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
      }
      this.Tabs = new NotchTabs(this, this.gate);
      this.Updates = new NotchUpdates(this.clock, autoTick);
      this.Updates.CurrentChanged += delegate
      {
        this.Update(delegate
        {
        }, false);
      };
    }

    // Token: 0x17000421 RID: 1057
    // (get) Token: 0x060011C4 RID: 4548 RVA: 0x0039E0D0 File Offset: 0x0039E0D0
    [Nullable(1)]
    public NotchTabs Tabs
    {
      [NullableContext(1)]
      get;
    }

    // Token: 0x17000422 RID: 1058
    // (get) Token: 0x060011C5 RID: 4549 RVA: 0x0039E0D8 File Offset: 0x0039E0D8
    [Nullable(1)]
    public NotchUpdates Updates
    {
      [NullableContext(1)]
      get;
    }

    // Token: 0x17000423 RID: 1059
    // (get) Token: 0x060011C6 RID: 4550 RVA: 0x0039E0E0 File Offset: 0x0039E0E0
    internal DateTimeOffset Now
    {
      get
      {
        return this.clock();
      }
    }

    // Token: 0x17000424 RID: 1060
    // (get) Token: 0x060011C7 RID: 4551 RVA: 0x0039E0F0 File Offset: 0x0039E0F0
    [Nullable(1)]
    public NotchPresentation Presentation
    {
      [NullableContext(1)]
      get
      {
        object obj = this.gate;
        NotchPresentation notchPresentation;
        lock (obj)
        {
          notchPresentation = this.presentation;
        }
        return notchPresentation;
      }
    }

    // Token: 0x17000425 RID: 1061
    // (get) Token: 0x060011C8 RID: 4552 RVA: 0x0039E134 File Offset: 0x0039E134
    public bool IsHovering
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.hovering;
        }
        return flag2;
      }
    }

    // Token: 0x17000426 RID: 1062
    // (get) Token: 0x060011C9 RID: 4553 RVA: 0x0039E178 File Offset: 0x0039E178
    public VoiceState VoiceState
    {
      get
      {
        object obj = this.gate;
        VoiceState voiceState;
        lock (obj)
        {
          voiceState = this.voiceState;
        }
        return voiceState;
      }
    }

    // Token: 0x060011CA RID: 4554 RVA: 0x0039E1BC File Offset: 0x0039E1BC
    [NullableContext(1)]
    public void Present(NotchPresentation next)
    {
      NotchPresentation.TextInput <input>5__2 = next as NotchPresentation.TextInput;
      if (<input>5__2 != null)
      {
        this.Update(delegate
        {
          this.textInputAgentFollowUp = (this.textInputActive ? (<input>5__2.AgentFollowUp ?? this.textInputAgentFollowUp) : <input>5__2.AgentFollowUp);
          this.textInputActive = true;
          this.textInputDraft = <input>5__2.Draft;
          this.textResponse = null;
          this.textResponseRemaining = null;
        }, false);
        return;
      }
      NotchPresentation.TextResponse <response>5__3 = next as NotchPresentation.TextResponse;
      if (<response>5__3 != null)
      {
        this.Update(delegate
        {
          this.EndTextInput();
          if (this.textResponse == null || this.textResponse.Question != <response>5__3.Question || <response>5__3.IsThinking)
          {
            this.textResponseUserPaused = false;
          }
          this.textResponse = <response>5__3;
          this.textResponseRemaining = (<response>5__3.IsThinking ? null : new TimeSpan?(NotchPresenter.AutoDismissForTextResponse(<response>5__3.Answer, this.isRestrictingForTutorial)));
          TimeSpan? timeSpan = this.textResponseRemaining;
          if (timeSpan != null)
          {
            TimeSpan valueOrDefault = timeSpan.GetValueOrDefault();
            this.textResponseTotal = valueOrDefault;
          }
        }, false);
        return;
      }
      NotchPresentation.OnboardingPinned <pinned>5__4 = next as NotchPresentation.OnboardingPinned;
      if (<pinned>5__4 != null)
      {
        this.Update(delegate
        {
          this.onboardingPinStep = new int?(<pinned>5__4.Step);
          this.onboardingPinManuallyDismissed = false;
        }, false);
        return;
      }
      NotchPresentation.LiveEvent liveEvent = next as NotchPresentation.LiveEvent;
      if (liveEvent != null)
      {
        this.ShowLiveEvent(liveEvent.Event, null);
        return;
      }
      if (!(next is NotchPresentation.Idle))
      {
        return;
      }
      this.Update(delegate
      {
        this.EndTextInput();
        this.DismissTextResponse();
      }, false);
    }

    // Token: 0x17000427 RID: 1063
    // (get) Token: 0x060011CB RID: 4555 RVA: 0x0039E28C File Offset: 0x0039E28C
    public NotchPresentation.TextResponse CurrentTextResponse
    {
      get
      {
        object obj = this.gate;
        NotchPresentation.TextResponse textResponse;
        lock (obj)
        {
          textResponse = this.textResponse;
        }
        return textResponse;
      }
    }

    // Token: 0x17000428 RID: 1064
    // (get) Token: 0x060011CC RID: 4556 RVA: 0x0039E2D0 File Offset: 0x0039E2D0
    public bool IsTextResponseInProgress
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.textResponse != null;
        }
        return flag2;
      }
    }

    // Token: 0x060011CD RID: 4557 RVA: 0x0039E318 File Offset: 0x0039E318
    private void EndTextInput()
    {
      if (!this.textInputActive)
      {
        return;
      }
      if (this.pointerInside)
      {
        this.suppressHoverExpandUntilNotchReentry = true;
      }
      this.textInputActive = false;
      this.textInputAgentFollowUp = null;
    }

    // Token: 0x060011CE RID: 4558 RVA: 0x0039E340 File Offset: 0x0039E340
    private void DismissTextResponse()
    {
      if (this.textResponse != null && this.pointerInside)
      {
        this.suppressHoverExpandUntilNotchReentry = true;
      }
      this.textResponse = null;
      this.textResponseRemaining = null;
    }

    // Token: 0x060011CF RID: 4559 RVA: 0x0039E36C File Offset: 0x0039E36C
    [NullableContext(1)]
    public void ShowLiveEvent(NotchLiveEvent liveEvent, TimeSpan? autoDismissAfter = null)
    {
      if (liveEvent is NotchLiveEvent.ActivityListening)
      {
        this.SetVoiceState(VoiceState.Listening, null);
        return;
      }
      if (liveEvent is NotchLiveEvent.ActivityThinking)
      {
        this.SetVoiceState(VoiceState.Processing, null);
        return;
      }
      if (liveEvent is NotchLiveEvent.ActivitySpeaking)
      {
        this.SetVoiceState(VoiceState.Responding, null);
        return;
      }
      NotchLiveEvent.ActivityCustomStatus activityCustomStatus = liveEvent as NotchLiveEvent.ActivityCustomStatus;
      if (activityCustomStatus != null)
      {
        this.SetVoiceState(VoiceState.Processing, activityCustomStatus.Label);
        return;
      }
      if (!(liveEvent is NotchLiveEvent.AgentPeek))
      {
        this.Update(delegate
        {
          NotchPresenter.LiveEventSlot liveEventSlot;
          if (liveEvent is NotchLiveEvent.MicrophoneTrouble && autoDismissAfter == null && this.slots.TryGetValue(liveEvent.GetType(), out liveEventSlot) && liveEventSlot.Event.Equals(liveEvent))
          {
            return;
          }
          ValueTuple<TimeSpan?, bool> valueTuple = NotchPresenter.DefaultAutoDismiss(liveEvent);
          TimeSpan? item = valueTuple.Item1;
          bool item2 = valueTuple.Item2;
          Dictionary<Type, NotchPresenter.LiveEventSlot> dictionary = this.slots;
          Type type = liveEvent.GetType();
          NotchLiveEvent liveEvent2 = liveEvent;
          TimeSpan? autoDismissAfter2 = autoDismissAfter;
          dictionary[type] = new NotchPresenter.LiveEventSlot(liveEvent2, (autoDismissAfter2 != null) ? autoDismissAfter2 : item, item2);
        }, false);
        return;
      }
      this.OpenNotchAgentPeekOnRequest();
    }

    // Token: 0x060011D0 RID: 4560 RVA: 0x0039E41C File Offset: 0x0039E41C
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "Remaining", "Total" })]
    public ValueTuple<TimeSpan, TimeSpan>? LiveEventCountdown([Nullable(1)] Type liveEventType)
    {
      object obj = this.gate;
      ValueTuple<TimeSpan, TimeSpan>? valueTuple;
      lock (obj)
      {
        NotchPresenter.LiveEventSlot liveEventSlot;
        if (this.slots.TryGetValue(liveEventType, out liveEventSlot))
        {
          TimeSpan? remaining = liveEventSlot.Remaining;
          if (remaining != null)
          {
            TimeSpan valueOrDefault = remaining.GetValueOrDefault();
            TimeSpan valueOrDefault2 = NotchPresenter.DefaultAutoDismiss(liveEventSlot.Event).Item1.GetValueOrDefault(valueOrDefault);
            return new ValueTuple<TimeSpan, TimeSpan>?(new ValueTuple<TimeSpan, TimeSpan>(valueOrDefault, (valueOrDefault2 < valueOrDefault) ? valueOrDefault : valueOrDefault2));
          }
        }
        valueTuple = null;
        valueTuple = valueTuple;
      }
      return valueTuple;
    }

    // Token: 0x060011D1 RID: 4561 RVA: 0x0039E4CC File Offset: 0x0039E4CC
    [NullableContext(1)]
    public bool HasLiveEvent(Type liveEventType)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = this.slots.ContainsKey(liveEventType);
      }
      return flag2;
    }

    // Token: 0x060011D2 RID: 4562 RVA: 0x0039E514 File Offset: 0x0039E514
    [NullableContext(1)]
    public void DismissLiveEvent(Type liveEventType)
    {
      if (liveEventType == typeof(NotchLiveEvent.ActivityListening) || liveEventType == typeof(NotchLiveEvent.ActivityThinking) || liveEventType == typeof(NotchLiveEvent.ActivitySpeaking) || liveEventType == typeof(NotchLiveEvent.ActivityCustomStatus))
      {
        this.SetVoiceState(VoiceState.Idle, null);
        return;
      }
      if (liveEventType == typeof(NotchLiveEvent.AgentPeek))
      {
        this.DismissNotchAgentPeek();
        return;
      }
      this.Update(delegate
      {
        this.slots.Remove(liveEventType);
      }, false);
    }

    // Token: 0x060011D3 RID: 4563 RVA: 0x0039E5CC File Offset: 0x0039E5CC
    public void ReturnToIdle()
    {
      this.Update(delegate
      {
        this.EndTextInput();
        this.DismissTextResponse();
        this.slots.Clear();
        this.heldVoiceEvent = null;
        this.customStatusLabel = null;
        this.voiceState = VoiceState.Idle;
      }, false);
    }

    // Token: 0x060011D4 RID: 4564 RVA: 0x0039E5E4 File Offset: 0x0039E5E4
    public void SetVoiceState(VoiceState next, string customStatus = null)
    {
      this.Update(delegate
      {
        VoiceState voiceState = this.voiceState;
        this.voiceState = next;
        if (next == VoiceState.Listening)
        {
          this.voiceTurnBeganWithListening = true;
        }
        this.customStatusLabel = ((next == VoiceState.Processing) ? customStatus : null);
        if (voiceState == next)
        {
          return;
        }
        if (next != VoiceState.Idle)
        {
          this.heldVoiceEvent = null;
          return;
        }
        if (this.voiceTurnTerminationSuppressesNextHold)
        {
          this.voiceTurnTerminationSuppressesNextHold = false;
          this.heldVoiceEvent = null;
          return;
        }
        NotchPresentation.LiveEvent liveEvent = this.presentation as NotchPresentation.LiveEvent;
        if (liveEvent != null && liveEvent.Event is NotchLiveEvent.MorningSuggestions && voiceState != VoiceState.Listening)
        {
          this.heldVoiceEvent = null;
          return;
        }
        ValueTuple<NotchLiveEvent, TimeSpan> valueTuple;
        if (voiceState != VoiceState.Listening)
        {
          if (voiceState != VoiceState.Processing)
          {
            valueTuple = new ValueTuple<NotchLiveEvent, TimeSpan>(new NotchLiveEvent.ActivitySpeaking(), NotchPresenter.HoldAfterResponding);
          }
          else
          {
            valueTuple = new ValueTuple<NotchLiveEvent, TimeSpan>(new NotchLiveEvent.ActivityThinking(), NotchPresenter.HoldAfterProcessing);
          }
        }
        else
        {
          valueTuple = new ValueTuple<NotchLiveEvent, TimeSpan>(new NotchLiveEvent.ActivityThinking(), NotchPresenter.HoldAfterListening);
        }
        ValueTuple<NotchLiveEvent, TimeSpan> valueTuple2 = valueTuple;
        this.heldVoiceEvent = valueTuple2.Item1;
        TimeSpan item = valueTuple2.Item2;
        this.heldVoiceEventUntil = this.clock() + item;
      }, false);
    }

    // Token: 0x17000429 RID: 1065
    // (get) Token: 0x060011D5 RID: 4565 RVA: 0x0039E620 File Offset: 0x0039E620
    public string CustomStatusLabel
    {
      get
      {
        object obj = this.gate;
        string text;
        lock (obj)
        {
          text = this.customStatusLabel;
        }
        return text;
      }
    }

    // Token: 0x060011D6 RID: 4566 RVA: 0x0039E664 File Offset: 0x0039E664
    public void SetCustomStatusLabel(string label)
    {
      this.Update(delegate
      {
        this.customStatusLabel = ((this.voiceState == VoiceState.Processing) ? label : null);
      }, false);
    }

    // Token: 0x060011D7 RID: 4567 RVA: 0x0039E698 File Offset: 0x0039E698
    public void NotifyVoiceTurnTerminating()
    {
      this.Update(delegate
      {
        if (this.voiceState != VoiceState.Idle)
        {
          this.voiceTurnTerminationSuppressesNextHold = true;
        }
        this.heldVoiceEvent = null;
      }, false);
    }

    // Token: 0x060011D8 RID: 4568 RVA: 0x0039E6B0 File Offset: 0x0039E6B0
    public void SetPointerInside(bool inside)
    {
      this.Update(delegate
      {
        if (inside == this.pointerInside)
        {
          return;
        }
        this.pointerInside = inside;
        if (!inside)
        {
          this.shutUntilPointerLeaves = false;
        }
        if (inside)
        {
          this.pointerEnteredAt = this.clock();
          NotchPresentation.LiveEvent liveEvent = this.presentation as NotchPresentation.LiveEvent;
          if (liveEvent != null && liveEvent.Event is NotchLiveEvent.AgentPeek && !this.introOwnsStage)
          {
            this.hovering = true;
            this.NoteHoverForOpenRequest();
            return;
          }
        }
        else
        {
          this.hovering = false;
          if (this.openRequested && this.openRequestSawHover)
          {
            this.openRequested = false;
            this.openRequestSawHover = false;
          }
          if (this.clock() >= this.hoverExpandRearmNotBefore)
          {
            this.suppressHoverExpandUntilNotchReentry = false;
          }
          if (this.introWelcomeAutoShownPin && this.introWelcomeHadHoverIn)
          {
            this.introWelcomeAutoShownPin = false;
            this.introWelcomeHadHoverIn = false;
          }
        }
      }, false);
    }

    // Token: 0x1700042A RID: 1066
    // (get) Token: 0x060011D9 RID: 4569 RVA: 0x0039E6E4 File Offset: 0x0039E6E4
    public bool RequiresPointerExitBeforeHoverExpansion
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.suppressHoverExpandUntilNotchReentry;
        }
        return flag2;
      }
    }

    // Token: 0x060011DA RID: 4570 RVA: 0x0039E728 File Offset: 0x0039E728
    public void SuppressHoverUntilPointerReentry()
    {
      this.Update(delegate
      {
        this.suppressHoverExpandUntilNotchReentry = true;
        this.hoverExpandRearmNotBefore = this.clock() + NotchPresenter.HoverExpandHoldAfterCardLeft;
        this.hovering = false;
      }, false);
    }

    // Token: 0x1700042B RID: 1067
    // (get) Token: 0x060011DB RID: 4571 RVA: 0x0039E740 File Offset: 0x0039E740
    [TupleElementNames(new string[] { "Phase", "Label" })]
    [Nullable(new byte[] { 0, 2 })]
    public ValueTuple<NotchActivityPhase, string>? VoicePill
    {
      [return: TupleElementNames(new string[] { "Phase", "Label" })]
      [return: Nullable(new byte[] { 0, 2 })]
      get
      {
        object obj = this.gate;
        ValueTuple<NotchActivityPhase, string>? valueTuple;
        lock (obj)
        {
          valueTuple = this.voicePill;
        }
        return valueTuple;
      }
    }

    // Token: 0x14000028 RID: 40
    // (add) Token: 0x060011DC RID: 4572 RVA: 0x0039E784 File Offset: 0x0039E784
    // (remove) Token: 0x060011DD RID: 4573 RVA: 0x0039E7BC File Offset: 0x0039E7BC
    public event Action VoicePillChanged;

    // Token: 0x14000029 RID: 41
    // (add) Token: 0x060011DE RID: 4574 RVA: 0x0039E7F4 File Offset: 0x0039E7F4
    // (remove) Token: 0x060011DF RID: 4575 RVA: 0x0039E82C File Offset: 0x0039E82C
    public event Action HoverOpenedNotch;

    // Token: 0x1400002A RID: 42
    // (add) Token: 0x060011E0 RID: 4576 RVA: 0x0039E864 File Offset: 0x0039E864
    // (remove) Token: 0x060011E1 RID: 4577 RVA: 0x0039E89C File Offset: 0x0039E89C
    public event Action NotchSessionChanged;

    // Token: 0x1400002B RID: 43
    // (add) Token: 0x060011E2 RID: 4578 RVA: 0x0039E8D4 File Offset: 0x0039E8D4
    // (remove) Token: 0x060011E3 RID: 4579 RVA: 0x0039E90C File Offset: 0x0039E90C
    public event Action DroppedAttachmentsPending;

    // Token: 0x1700042C RID: 1068
    // (get) Token: 0x060011E4 RID: 4580 RVA: 0x0039E944 File Offset: 0x0039E944
    public bool IsShowingNotchAgentPeek
    {
      get
      {
        object obj = this.gate;
        bool isShowingNotchAgentPeekUnlocked;
        lock (obj)
        {
          isShowingNotchAgentPeekUnlocked = this.IsShowingNotchAgentPeekUnlocked;
        }
        return isShowingNotchAgentPeekUnlocked;
      }
    }

    // Token: 0x1700042D RID: 1069
    // (get) Token: 0x060011E5 RID: 4581 RVA: 0x0039E988 File Offset: 0x0039E988
    internal bool IsShowingNotchAgentPeekUnlocked
    {
      get
      {
        return NotchPresenter.IsPeekPresentation(this.presentation);
      }
    }

    // Token: 0x060011E6 RID: 4582 RVA: 0x0039E998 File Offset: 0x0039E998
    [NullableContext(1)]
    private static bool IsPeekPresentation(NotchPresentation p)
    {
      NotchPresentation.LiveEvent liveEvent = p as NotchPresentation.LiveEvent;
      return liveEvent != null && liveEvent.Event is NotchLiveEvent.AgentPeek;
    }

    // Token: 0x1700042E RID: 1070
    // (get) Token: 0x060011E7 RID: 4583 RVA: 0x0039E9C0 File Offset: 0x0039E9C0
    public bool IsNotchDocumentOpen
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.documentOpen;
        }
        return flag2;
      }
    }

    // Token: 0x1700042F RID: 1071
    // (get) Token: 0x060011E8 RID: 4584 RVA: 0x0039EA04 File Offset: 0x0039EA04
    public bool IsNotchChatSessionOpen
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.chatSessionOpen;
        }
        return flag2;
      }
    }

    // Token: 0x17000430 RID: 1072
    // (get) Token: 0x060011E9 RID: 4585 RVA: 0x0039EA48 File Offset: 0x0039EA48
    public bool IsNotchHeldOpenByArc
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.heldOpenByArc;
        }
        return flag2;
      }
    }

    // Token: 0x17000431 RID: 1073
    // (get) Token: 0x060011EA RID: 4586 RVA: 0x0039EA8C File Offset: 0x0039EA8C
    public bool RendersTextResponsesInNotchChat
    {
      get
      {
        object obj = this.gate;
        bool rendersInChatUnlocked;
        lock (obj)
        {
          rendersInChatUnlocked = this.RendersInChatUnlocked;
        }
        return rendersInChatUnlocked;
      }
    }

    // Token: 0x17000432 RID: 1074
    // (get) Token: 0x060011EB RID: 4587 RVA: 0x0039EAD0 File Offset: 0x0039EAD0
    private bool RendersInChatUnlocked
    {
      get
      {
        return this.chatSessionOpen || this.windowShowsMainThread || (this.routesTextModeToNotchChat && this.isSignedIn);
      }
    }

    // Token: 0x060011EC RID: 4588 RVA: 0x0039EAF4 File Offset: 0x0039EAF4
    public void SetWindowShowsMainThread(bool shows)
    {
      this.Update(delegate
      {
        this.windowShowsMainThread = shows;
      }, false);
    }

    // Token: 0x17000433 RID: 1075
    // (get) Token: 0x060011ED RID: 4589 RVA: 0x0039EB28 File Offset: 0x0039EB28
    private bool ChatSessionHoldingNotch
    {
      get
      {
        return this.chatSessionOpen && this.Tabs.SelectedCore == NotchTab.Chat;
      }
    }

    // Token: 0x17000434 RID: 1076
    // (get) Token: 0x060011EE RID: 4590 RVA: 0x0039EB44 File Offset: 0x0039EB44
    public bool HoldsNotchOpenWithoutArc
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.hovering || this.peekHeldOpen || this.chatBeingResized || this.heldOpenForPresentation || this.openRequested || this.documentOpen || this.ChatSessionHoldingNotch;
        }
        return flag2;
      }
    }

    // Token: 0x060011EF RID: 4591 RVA: 0x0039EBBC File Offset: 0x0039EBBC
    public void SetHoverRoutesToNotch(bool routes)
    {
      this.Update(delegate
      {
        this.hoverRoutesToNotch = routes;
      }, false);
    }

    // Token: 0x060011F0 RID: 4592 RVA: 0x0039EBF0 File Offset: 0x0039EBF0
    public void SetNotchHeldOpenForPresentation(bool held)
    {
      this.Update(delegate
      {
        this.heldOpenForPresentation = held;
      }, false);
    }

    // Token: 0x060011F1 RID: 4593 RVA: 0x0039EC24 File Offset: 0x0039EC24
    public void SetNotchHeldOpenByArc(bool held)
    {
      this.Update(delegate
      {
        this.heldOpenByArc = held;
      }, false);
    }

    // Token: 0x060011F2 RID: 4594 RVA: 0x0039EC58 File Offset: 0x0039EC58
    public void SetNotchAgentPeekHeldOpen(bool held)
    {
      this.Update(delegate
      {
        this.peekHeldOpen = held;
      }, false);
    }

    // Token: 0x060011F3 RID: 4595 RVA: 0x0039EC8C File Offset: 0x0039EC8C
    public void SetClickyDriveShowing(bool showing)
    {
      this.Update(delegate
      {
        this.clickyDriveShowing = showing;
      }, false);
    }

    // Token: 0x060011F4 RID: 4596 RVA: 0x0039ECC0 File Offset: 0x0039ECC0
    public void SetNotchChatBeingResized(bool resizing)
    {
      this.Update(delegate
      {
        this.chatBeingResized = resizing;
      }, false);
    }

    // Token: 0x060011F5 RID: 4597 RVA: 0x0039ECF4 File Offset: 0x0039ECF4
    public void SetRestingMarkState(bool showsHandle, bool isWorking)
    {
      this.Update(delegate
      {
        this.restingMarkShowsHandle = showsHandle;
        this.restingMarkIsWorking = isWorking;
      }, false);
    }

    // Token: 0x060011F6 RID: 4598 RVA: 0x0039ED30 File Offset: 0x0039ED30
    public void NoteTabPickedInsideOpenNotch()
    {
      this.Update(delegate
      {
        if (!this.IsShowingNotchAgentPeekUnlocked)
        {
          return;
        }
        this.suppressHoverExpandUntilNotchReentry = false;
        this.hovering = true;
        this.openRequested = true;
        this.openRequestSawHover = true;
      }, false);
    }

    // Token: 0x060011F7 RID: 4599 RVA: 0x0039ED48 File Offset: 0x0039ED48
    public void OpenNotchAgentPeekOnRequest()
    {
      this.Update(delegate
      {
        this.shutUntilPointerLeaves = false;
        this.openRequested = true;
        this.openRequestSawHover = this.hovering;
        this.suppressHoverExpandUntilNotchReentry = false;
        this.openRequestDeadline = this.clock() + NotchPresenter.OpenRequestTimeout;
      }, false);
    }

    // Token: 0x060011F8 RID: 4600 RVA: 0x0039ED60 File Offset: 0x0039ED60
    public void OpenNotchDocument()
    {
      this.Update(delegate
      {
        this.shutUntilPointerLeaves = false;
        this.documentOpen = true;
        this.suppressHoverExpandUntilNotchReentry = false;
      }, true);
    }

    // Token: 0x060011F9 RID: 4601 RVA: 0x0039ED78 File Offset: 0x0039ED78
    public void CloseNotchDocument()
    {
      this.Update(delegate
      {
        this.documentOpen = false;
      }, true);
    }

    // Token: 0x060011FA RID: 4602 RVA: 0x0039ED90 File Offset: 0x0039ED90
    public void FoldIntoWindow()
    {
      if (this.IsShowingNotchAgentPeek)
      {
        this.Tabs.ForgetReplyAndDocumentOnNextClose();
      }
      this.DismissNotchAgentPeek();
      this.Update(delegate
      {
        this.shutUntilPointerLeaves = this.pointerInside;
      }, false);
    }

    // Token: 0x060011FB RID: 4603 RVA: 0x0039EDC0 File Offset: 0x0039EDC0
    public void DismissNotchAgentPeek()
    {
      this.Update(delegate
      {
        if (!this.IsShowingNotchAgentPeekUnlocked)
        {
          return;
        }
        this.keepsOpenThroughVoiceTurn = false;
        this.peekHeldOpen = false;
        this.heldOpenForPresentation = false;
        this.heldOpenByArc = false;
        this.documentOpen = false;
        if (this.chatSessionOpen)
        {
          this.TrackChatSessionEnded(NotchChatSessionCloseReason.NotchFolded);
        }
        this.chatSessionOpen = false;
        this.chatComposerHasDraft = false;
        this.chatComposerHasKeyboard = false;
        this.Tabs.DropDocumentIfShowingCore();
        this.openRequested = false;
        this.suppressHoverExpandUntilNotchReentry = true;
        this.hoverExpandRearmNotBefore = this.clock() + NotchPresenter.HoverExpandHoldAfterCardLeft;
        this.hovering = false;
      }, true);
    }

    // Token: 0x060011FC RID: 4604 RVA: 0x0039EDD8 File Offset: 0x0039EDD8
    public void SetRoutesTextModeToNotchChat(bool routes)
    {
      this.Update(delegate
      {
        this.routesTextModeToNotchChat = routes;
      }, false);
    }

    // Token: 0x17000435 RID: 1077
    // (get) Token: 0x060011FD RID: 4605 RVA: 0x0039EE0C File Offset: 0x0039EE0C
    public bool RoutesTextModeToNotchChat
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.routesTextModeToNotchChat && this.isSignedIn;
        }
        return flag2;
      }
    }

    // Token: 0x060011FE RID: 4606 RVA: 0x0039EE5C File Offset: 0x0039EE5C
    public void SetNotchChatComposerHasDraft(bool hasDraft)
    {
      this.Update(delegate
      {
        this.chatComposerHasDraft = hasDraft;
      }, false);
    }

    // Token: 0x060011FF RID: 4607 RVA: 0x0039EE90 File Offset: 0x0039EE90
    public void SetNotchChatComposerHasKeyboard(bool hasKeyboard)
    {
      this.Update(delegate
      {
        this.chatComposerHasKeyboard = hasKeyboard;
      }, false);
    }

    // Token: 0x17000436 RID: 1078
    // (get) Token: 0x06001200 RID: 4608 RVA: 0x0039EEC4 File Offset: 0x0039EEC4
    public bool NotchChatComposerHasKeyboard
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.chatComposerHasKeyboard;
        }
        return flag2;
      }
    }

    // Token: 0x17000437 RID: 1079
    // (get) Token: 0x06001201 RID: 4609 RVA: 0x0039EF08 File Offset: 0x0039EF08
    public bool IsNotchChatAwaitingAnswer
    {
      get
      {
        object obj = this.gate;
        bool chatAwaitingAnswer;
        lock (obj)
        {
          chatAwaitingAnswer = this.ChatAwaitingAnswer;
        }
        return chatAwaitingAnswer;
      }
    }

    // Token: 0x17000438 RID: 1080
    // (get) Token: 0x06001202 RID: 4610 RVA: 0x0039EF4C File Offset: 0x0039EF4C
    private bool ChatAwaitingAnswer
    {
      get
      {
        bool flag = this.textResponse != null;
        if (!flag)
        {
          VoiceState voiceState = this.voiceState;
          bool flag2 = voiceState - VoiceState.Processing <= 1;
          flag = flag2 && !this.voiceTurnBeganWithListening;
        }
        return flag;
      }
    }

    // Token: 0x06001203 RID: 4611 RVA: 0x0039EF90 File Offset: 0x0039EF90
    public void NoteTypedChatMessageSent(int attachmentCount = 0)
    {
      this.Update(delegate
      {
        this.voiceTurnBeganWithListening = false;
        if (this.chatSessionOpen)
        {
          this.chatSessionSentMessageCount++;
          this.chatSessionSentAttachmentCount += attachmentCount;
        }
      }, false);
    }

    // Token: 0x06001204 RID: 4612 RVA: 0x0039EFC4 File Offset: 0x0039EFC4
    private void TrackChatSessionOpened(NotchChatSessionOpenVia via, bool notchWasOpen, NotchTab tabBefore)
    {
      this.chatSessionStartedAt = new DateTimeOffset?(this.clock());
      this.chatSessionOpenedVia = via;
      this.chatSessionSentMessageCount = 0;
      this.chatSessionSentAttachmentCount = 0;
      this.pendingChatAnalytics.Add(NotchChatAnalytics.SessionOpened(via, notchWasOpen, tabBefore));
    }

    // Token: 0x06001205 RID: 4613 RVA: 0x0039F004 File Offset: 0x0039F004
    private void TrackChatSessionEnded(NotchChatSessionCloseReason reason)
    {
      DateTimeOffset? dateTimeOffset = this.chatSessionStartedAt;
      if (dateTimeOffset != null)
      {
        DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
        this.chatSessionStartedAt = null;
        this.pendingChatAnalytics.Add(NotchChatAnalytics.SessionClosed(reason, this.chatSessionOpenedVia, this.clock() - valueOrDefault, this.chatSessionSentMessageCount, this.chatSessionSentAttachmentCount, this.chatComposerHasDraft, this.ChatAwaitingAnswer));
        return;
      }
    }

    // Token: 0x06001206 RID: 4614 RVA: 0x0039F078 File Offset: 0x0039F078
    private void FlushChatAnalytics()
    {
      object obj = this.gate;
      JsonObject[] array;
      lock (obj)
      {
        if (this.pendingChatAnalytics.Count == 0)
        {
          return;
        }
        array = this.pendingChatAnalytics.ToArray();
        this.pendingChatAnalytics.Clear();
      }
      foreach (JsonObject jsonObject in array)
      {
        ClickyAnalytics.TrackProductEvent("notch_chat_action", null, null, jsonObject);
      }
    }

    // Token: 0x06001207 RID: 4615 RVA: 0x0039F100 File Offset: 0x0039F100
    public void OpenNotchChatSession(NotchChatSessionOpenVia via = NotchChatSessionOpenVia.DoubleControl)
    {
      object obj = this.gate;
      bool isShowingNotchAgentPeekUnlocked;
      lock (obj)
      {
        isShowingNotchAgentPeekUnlocked = this.IsShowingNotchAgentPeekUnlocked;
        if (!this.chatSessionOpen)
        {
          this.TrackChatSessionOpened(via, isShowingNotchAgentPeekUnlocked, this.Tabs.SelectedCore);
        }
      }
      this.FlushChatAnalytics();
      this.Tabs.ShowChatForTyping(isShowingNotchAgentPeekUnlocked);
      this.Update(delegate
      {
        this.EndTextInput();
        this.documentOpen = false;
        NotchPresentation.TextResponse textResponse = this.textResponse;
        if (textResponse != null && !textResponse.IsThinking && this.voiceState == VoiceState.Idle)
        {
          this.textResponse = null;
          this.textResponseRemaining = null;
        }
        this.chatSessionOpen = true;
        this.shutUntilPointerLeaves = false;
        this.suppressHoverExpandUntilNotchReentry = false;
      }, true);
      this.Tabs.RequestComposerFocus();
    }

    // Token: 0x06001208 RID: 4616 RVA: 0x0039F18C File Offset: 0x0039F18C
    public void CloseNotchChatSession(NotchChatSessionCloseReason reason = NotchChatSessionCloseReason.Unspecified)
    {
      this.Update(delegate
      {
        if (!this.chatSessionOpen)
        {
          return;
        }
        this.TrackChatSessionEnded(reason);
        this.dragOpenedChatCloseAt = null;
        this.chatOpenedByFileDrag = false;
        this.chatSessionOpen = false;
        this.chatComposerHasDraft = false;
        this.chatComposerHasKeyboard = false;
        this.peekHeldOpen = false;
        this.Tabs.ConsumeComposerFocusRequestCore();
        this.pendingDroppedAttachments.Clear();
        this.suppressHoverExpandUntilNotchReentry = this.hovering;
        this.hovering = false;
      }, true);
    }

    // Token: 0x06001209 RID: 4617 RVA: 0x0039F1C0 File Offset: 0x0039F1C0
    public void EndNotchChatSessionIfLeftBehind(bool awaitingAnswerToSentMessage)
    {
      this.Update(delegate
      {
        if ((!this.chatSessionOpen || this.ChatAwaitingAnswer) | awaitingAnswerToSentMessage)
        {
          return;
        }
        NotchTab selectedCore = this.Tabs.SelectedCore;
        bool flag = selectedCore == NotchTab.Chat || selectedCore == NotchTab.Document;
        if (flag)
        {
          return;
        }
        this.dragOpenedChatCloseAt = null;
        this.chatOpenedByFileDrag = false;
        this.TrackChatSessionEnded(NotchChatSessionCloseReason.LeftChatTab);
        this.chatSessionOpen = false;
        this.chatComposerHasDraft = false;
        this.chatComposerHasKeyboard = false;
        this.Tabs.ConsumeComposerFocusRequestCore();
        this.pendingDroppedAttachments.Clear();
      }, true);
    }

    // Token: 0x0600120A RID: 4618 RVA: 0x0039F1F4 File Offset: 0x0039F1F4
    public bool NotchChatHostsFileDrops(bool textResponseFollowUpHostsDrops)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = ((this.routesTextModeToNotchChat && this.isSignedIn) || this.chatSessionOpen) && !this.textInputActive && !textResponseFollowUpHostsDrops;
      }
      return flag2;
    }

    // Token: 0x0600120B RID: 4619 RVA: 0x0039F258 File Offset: 0x0039F258
    public bool NoteFileDragOverNotchChat()
    {
      bool flag = false;
      bool flag2 = false;
      object obj = this.gate;
      lock (obj)
      {
        this.dragOpenedChatCloseAt = null;
        if (!this.chatSessionOpen && !this.chatOpenedByFileDrag)
        {
          this.chatOpenedByFileDrag = true;
          flag = true;
        }
        else if (this.chatSessionOpen && this.Tabs.SelectedCore != NotchTab.Chat)
        {
          flag2 = true;
        }
      }
      if (flag)
      {
        this.OpenNotchChatSession(NotchChatSessionOpenVia.FileDrag);
      }
      else if (flag2)
      {
        this.Tabs.Select(NotchTab.Chat);
      }
      return flag;
    }

    // Token: 0x0600120C RID: 4620 RVA: 0x0039F2F4 File Offset: 0x0039F2F4
    public bool NoteFileDragLeftNotchChat()
    {
      bool scheduled = false;
      this.Update(delegate
      {
        if (this.chatOpenedByFileDrag)
        {
          DateTimeOffset? dateTimeOffset = this.dragOpenedChatCloseAt;
          if (dateTimeOffset == null)
          {
            this.dragOpenedChatCloseAt = new DateTimeOffset?(this.clock() + NotchPresenter.DragOpenedChatCloseGrace);
            scheduled = true;
            return;
          }
        }
      }, false);
      return scheduled;
    }

    // Token: 0x1400002C RID: 44
    // (add) Token: 0x0600120D RID: 4621 RVA: 0x0039F330 File Offset: 0x0039F330
    // (remove) Token: 0x0600120E RID: 4622 RVA: 0x0039F368 File Offset: 0x0039F368
    public event Action DragOpenedChatAbandoned;

    // Token: 0x0600120F RID: 4623 RVA: 0x0039F3A0 File Offset: 0x0039F3A0
    [NullableContext(1)]
    public string DropFilesOnNotchChat(IReadOnlyList<string> paths)
    {
      bool flag = false;
      object obj = this.gate;
      string text;
      bool flag3;
      lock (obj)
      {
        text = (this.chatOpenedByFileDrag ? "drag_opened" : (this.chatSessionOpen ? "user_opened" : "drop_opened"));
        this.dragOpenedChatCloseAt = null;
        this.chatOpenedByFileDrag = false;
        this.pendingDroppedAttachments.AddRange(paths);
        flag3 = !this.chatSessionOpen;
        if (!flag3 && this.Tabs.SelectedCore != NotchTab.Chat)
        {
          flag = true;
        }
      }
      if (flag3)
      {
        this.OpenNotchChatSession(NotchChatSessionOpenVia.FileDrop);
      }
      else if (flag)
      {
        this.Tabs.Select(NotchTab.Chat);
      }
      Action droppedAttachmentsPending = this.DroppedAttachmentsPending;
      if (droppedAttachmentsPending != null)
      {
        droppedAttachmentsPending();
      }
      return text;
    }

    // Token: 0x06001210 RID: 4624 RVA: 0x0039F46C File Offset: 0x0039F46C
    [NullableContext(1)]
    public IReadOnlyList<string> TakePendingDroppedAttachments()
    {
      object obj = this.gate;
      IReadOnlyList<string> readOnlyList2;
      lock (obj)
      {
        IReadOnlyList<string> readOnlyList = this.pendingDroppedAttachments.ToList<string>();
        this.pendingDroppedAttachments.Clear();
        readOnlyList2 = readOnlyList;
      }
      return readOnlyList2;
    }

    // Token: 0x06001211 RID: 4625 RVA: 0x0039F4C0 File Offset: 0x0039F4C0
    private void NoteHoverForOpenRequest()
    {
      if (this.openRequested)
      {
        this.openRequestSawHover = true;
      }
    }

    // Token: 0x06001212 RID: 4626 RVA: 0x0039F4D4 File Offset: 0x0039F4D4
    [return: Nullable(new byte[] { 0, 2 })]
    private ValueTuple<NotchActivityPhase, string>? VoicePillPhaseForCurrentVoiceState()
    {
      if (this.textResponse != null)
      {
        return null;
      }
      if (this.chatSessionOpen && !this.voiceTurnBeganWithListening && this.voiceState != VoiceState.Listening)
      {
        return null;
      }
      switch (this.voiceState)
      {
      case VoiceState.Listening:
        return new ValueTuple<NotchActivityPhase, string>?(new ValueTuple<NotchActivityPhase, string>(NotchActivityPhase.Listening, null));
      case VoiceState.Processing:
        return new ValueTuple<NotchActivityPhase, string>?(string.IsNullOrEmpty(this.customStatusLabel) ? new ValueTuple<NotchActivityPhase, string>(NotchActivityPhase.Thinking, null) : new ValueTuple<NotchActivityPhase, string>(NotchActivityPhase.Custom, this.customStatusLabel));
      case VoiceState.Responding:
        return new ValueTuple<NotchActivityPhase, string>?(new ValueTuple<NotchActivityPhase, string>(NotchActivityPhase.Speaking, null));
      default:
      {
        NotchLiveEvent notchLiveEvent = this.heldVoiceEvent;
        ValueTuple<NotchActivityPhase, string>? valueTuple;
        if (!(notchLiveEvent is NotchLiveEvent.ActivityListening))
        {
          if (!(notchLiveEvent is NotchLiveEvent.ActivityThinking))
          {
            if (!(notchLiveEvent is NotchLiveEvent.ActivitySpeaking))
            {
              NotchLiveEvent.ActivityCustomStatus activityCustomStatus = notchLiveEvent as NotchLiveEvent.ActivityCustomStatus;
              if (activityCustomStatus == null)
              {
                valueTuple = null;
              }
              else
              {
                valueTuple = new ValueTuple<NotchActivityPhase, string>?(new ValueTuple<NotchActivityPhase, string>(NotchActivityPhase.Custom, activityCustomStatus.Label));
              }
            }
            else
            {
              valueTuple = new ValueTuple<NotchActivityPhase, string>?(new ValueTuple<NotchActivityPhase, string>(NotchActivityPhase.Speaking, null));
            }
          }
          else
          {
            valueTuple = new ValueTuple<NotchActivityPhase, string>?(new ValueTuple<NotchActivityPhase, string>(NotchActivityPhase.Thinking, null));
          }
        }
        else
        {
          valueTuple = new ValueTuple<NotchActivityPhase, string>?(new ValueTuple<NotchActivityPhase, string>(NotchActivityPhase.Listening, null));
        }
        return valueTuple;
      }
      }
    }

    // Token: 0x17000439 RID: 1081
    // (get) Token: 0x06001213 RID: 4627 RVA: 0x0039F5F0 File Offset: 0x0039F5F0
    private bool VoiceTurnActiveForPresentation
    {
      get
      {
        return this.voiceState != VoiceState.Idle || (this.heldVoiceEvent != null && this.clock() < this.heldVoiceEventUntil);
      }
    }

    // Token: 0x06001214 RID: 4628 RVA: 0x0039F61C File Offset: 0x0039F61C
    public void SetTourShowcaseShowing(bool showing)
    {
      this.Update(delegate
      {
        this.tourShowcaseShowing = showing;
      }, false);
    }

    // Token: 0x1700043A RID: 1082
    // (get) Token: 0x06001215 RID: 4629 RVA: 0x0039F650 File Offset: 0x0039F650
    public bool IsTourShowcaseShowing
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.tourShowcaseShowing;
        }
        return flag2;
      }
    }

    // Token: 0x06001216 RID: 4630 RVA: 0x0039F694 File Offset: 0x0039F694
    public void SetIntroHomeWelcomeActive(bool active)
    {
      this.Update(delegate
      {
        if (this.introWelcomeActive == active)
        {
          return;
        }
        this.introWelcomeActive = active;
        if (!active)
        {
          this.introWelcomeAutoShownPin = false;
          this.introWelcomeHadHoverIn = false;
        }
      }, false);
    }

    // Token: 0x06001217 RID: 4631 RVA: 0x0039F6C8 File Offset: 0x0039F6C8
    public void SetIntroHomeWelcomeAutoShownPin(bool pinned)
    {
      this.Update(delegate
      {
        this.introWelcomeAutoShownPin = pinned;
        if (!pinned)
        {
          this.introWelcomeHadHoverIn = false;
        }
      }, false);
    }

    // Token: 0x1700043B RID: 1083
    // (get) Token: 0x06001218 RID: 4632 RVA: 0x0039F6FC File Offset: 0x0039F6FC
    public bool IsIntroHomeWelcomeAutoShownPin
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.introWelcomeAutoShownPin;
        }
        return flag2;
      }
    }

    // Token: 0x06001219 RID: 4633 RVA: 0x0039F740 File Offset: 0x0039F740
    public bool CollapseHoverAfterOutsideClick()
    {
      bool collapsed = false;
      bool closeChat = false;
      this.Update(delegate
      {
        if (this.textInputActive)
        {
          return;
        }
        if (this.keepsOpenThroughVoiceTurn)
        {
          this.keepsOpenThroughVoiceTurn = false;
          collapsed = true;
        }
        bool flag = this.openRequested;
        if (flag)
        {
          this.openRequested = false;
          this.openRequestSawHover = false;
        }
        if (this.chatSessionOpen)
        {
          if (this.ChatAwaitingAnswer)
          {
            collapsed = collapsed || flag;
            return;
          }
          closeChat = true;
          return;
        }
        else
        {
          bool flag2 = this.peekHeldOpen;
          this.peekHeldOpen = false;
          if (this.documentOpen)
          {
            this.documentOpen = false;
            this.Tabs.DropDocumentIfShowingCore();
            this.hovering = false;
            collapsed = true;
            return;
          }
          if (this.textResponse != null)
          {
            return;
          }
          if (!this.hovering && !flag && !flag2)
          {
            return;
          }
          this.hovering = false;
          this.pointerInside = false;
          collapsed = true;
          return;
        }
      }, true);
      if (closeChat)
      {
        this.CloseNotchChatSession(NotchChatSessionCloseReason.ClickElsewhere);
        return true;
      }
      return collapsed;
    }

    // Token: 0x0600121A RID: 4634 RVA: 0x0039F794 File Offset: 0x0039F794
    public void SetSignedIn(bool signedIn)
    {
      this.Update(delegate
      {
        this.isSignedIn = signedIn;
      }, false);
    }

    // Token: 0x0600121B RID: 4635 RVA: 0x0039F7C8 File Offset: 0x0039F7C8
    public void SetOnboardingPin(int? step)
    {
      this.Update(delegate
      {
        int? num = this.onboardingPinStep;
        int? step2 = step;
        if (!((num.GetValueOrDefault() == step2.GetValueOrDefault()) & (num != null == (step2 != null))))
        {
          this.onboardingPinManuallyDismissed = false;
        }
        this.onboardingPinStep = step;
      }, false);
    }

    // Token: 0x0600121C RID: 4636 RVA: 0x0039F7FC File Offset: 0x0039F7FC
    public void DismissOnboardingPin()
    {
      this.Update(delegate
      {
        this.onboardingPinManuallyDismissed = true;
        this.hovering = false;
      }, false);
    }

    // Token: 0x0600121D RID: 4637 RVA: 0x0039F814 File Offset: 0x0039F814
    public void SetTutorialRestrictingNormalUse(bool restricting)
    {
      this.Update(delegate
      {
        this.isRestrictingForTutorial = restricting;
      }, false);
    }

    // Token: 0x0600121E RID: 4638 RVA: 0x0039F848 File Offset: 0x0039F848
    public void SetOnboardingIntroOwnsStage(bool owns)
    {
      this.Update(delegate
      {
        if (this.introOwnsStage == owns)
        {
          return;
        }
        this.introOwnsStage = owns;
        if (owns)
        {
          this.hovering = false;
        }
      }, false);
    }

    // Token: 0x1700043C RID: 1084
    // (get) Token: 0x0600121F RID: 4639 RVA: 0x0039F87C File Offset: 0x0039F87C
    public bool IsOnboardingIntroOwningStage
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.introOwnsStage;
        }
        return flag2;
      }
    }

    // Token: 0x06001220 RID: 4640 RVA: 0x0039F8C0 File Offset: 0x0039F8C0
    public void SetAmbientSuppressed(bool suppressed)
    {
      this.Update(delegate
      {
        this.ambientSuppressed = suppressed;
      }, false);
    }

    // Token: 0x1700043D RID: 1085
    // (get) Token: 0x06001221 RID: 4641 RVA: 0x0039F8F4 File Offset: 0x0039F8F4
    public bool IsAmbientSuppressed
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.ambientSuppressed;
        }
        return flag2;
      }
    }

    // Token: 0x1700043E RID: 1086
    // (get) Token: 0x06001222 RID: 4642 RVA: 0x0039F938 File Offset: 0x0039F938
    [TupleElementNames(new string[] { "Remaining", "Total", "UserPaused" })]
    [Nullable(0)]
    public ValueTuple<TimeSpan, TimeSpan, bool>? TextResponseCountdown
    {
      [NullableContext(0)]
      [return: TupleElementNames(new string[] { "Remaining", "Total", "UserPaused" })]
      get
      {
        object obj = this.gate;
        ValueTuple<TimeSpan, TimeSpan, bool>? valueTuple;
        lock (obj)
        {
          if (this.presentation is NotchPresentation.TextResponse)
          {
            TimeSpan? timeSpan = this.textResponseRemaining;
            if (timeSpan != null)
            {
              TimeSpan valueOrDefault = timeSpan.GetValueOrDefault();
              return new ValueTuple<TimeSpan, TimeSpan, bool>?(new ValueTuple<TimeSpan, TimeSpan, bool>(valueOrDefault, this.textResponseTotal, this.textResponseUserPaused));
            }
          }
          valueTuple = null;
          valueTuple = valueTuple;
        }
        return valueTuple;
      }
    }

    // Token: 0x06001223 RID: 4643 RVA: 0x0039F9C4 File Offset: 0x0039F9C4
    public void PauseTextResponseAutoDismiss()
    {
      this.Update(delegate
      {
        this.textResponseUserPaused = true;
      }, false);
    }

    // Token: 0x06001224 RID: 4644 RVA: 0x0039F9DC File Offset: 0x0039F9DC
    public void SetTextResponseFollowUpActive(bool active)
    {
      this.Update(delegate
      {
        this.textResponseFollowUpActive = active;
      }, false);
    }

    // Token: 0x06001225 RID: 4645 RVA: 0x0039FA10 File Offset: 0x0039FA10
    [NullableContext(1)]
    public void UpdateTextInputDraft(string draft)
    {
      this.Update(delegate
      {
        this.textInputDraft = draft;
      }, false);
    }

    // Token: 0x06001226 RID: 4646 RVA: 0x0039FA44 File Offset: 0x0039FA44
    [NullableContext(1)]
    private void Update(Action change, bool sessionChanged = false)
    {
      object obj = this.gate;
      bool flag3;
      NotchTab? notchTab;
      bool flag7;
      bool flag8;
      lock (obj)
      {
        ValueTuple<NotchActivityPhase, string>? valueTuple = this.voicePill;
        bool flag2 = this.heldOpenByArc;
        change();
        flag3 = this.Recompute();
        this.UpdateTicker();
        notchTab = this.tabsBeforeLadder;
        this.tabsBeforeLadder = null;
        ValueTuple<NotchActivityPhase, string>? valueTuple2 = this.voicePill;
        ValueTuple<NotchActivityPhase, string>? valueTuple3 = valueTuple;
        bool flag4 = valueTuple2 != null;
        bool flag5;
        if (flag4 != (valueTuple3 != null))
        {
          flag5 = true;
        }
        else if (!flag4)
        {
          flag5 = false;
        }
        else
        {
          ValueTuple<NotchActivityPhase, string> valueOrDefault = valueTuple2.GetValueOrDefault();
          ValueTuple<NotchActivityPhase, string> valueOrDefault2 = valueTuple3.GetValueOrDefault();
          flag5 = valueOrDefault.Item1 != valueOrDefault2.Item1 || valueOrDefault.Item2 != valueOrDefault2.Item2;
        }
        bool flag6;
        if (!flag5)
        {
          valueTuple3 = this.voicePill;
          flag6 = valueTuple3 != null && this.heldOpenByArc != flag2;
        }
        else
        {
          flag6 = true;
        }
        flag7 = flag6;
        flag8 = this.hoverOpenedNotchThisPass;
        this.hoverOpenedNotchThisPass = false;
      }
      this.FlushChatAnalytics();
      this.RaiseAfterLadder(flag3, notchTab, flag7, flag8, sessionChanged);
    }

    // Token: 0x06001227 RID: 4647 RVA: 0x0039FB60 File Offset: 0x0039FB60
    private void RaiseAfterLadder(bool changed, NotchTab? tabsBefore, bool pillChanged, bool hoverOpened, bool sessionChanged)
    {
      if (tabsBefore != null)
      {
        NotchTab valueOrDefault = tabsBefore.GetValueOrDefault();
        this.Tabs.RaiseChangedAfterLadder(valueOrDefault);
      }
      if (changed)
      {
        this.RaiseChanged();
      }
      if (pillChanged)
      {
        Action voicePillChanged = this.VoicePillChanged;
        if (voicePillChanged != null)
        {
          voicePillChanged();
        }
      }
      if (hoverOpened)
      {
        Action hoverOpenedNotch = this.HoverOpenedNotch;
        if (hoverOpenedNotch != null)
        {
          hoverOpenedNotch();
        }
      }
      if (sessionChanged)
      {
        Action notchSessionChanged = this.NotchSessionChanged;
        if (notchSessionChanged == null)
        {
          return;
        }
        notchSessionChanged();
      }
    }

    // Token: 0x06001228 RID: 4648 RVA: 0x0039FBD0 File Offset: 0x0039FBD0
    public void Tick()
    {
      bool flag = false;
      bool flag2 = false;
      bool flag3 = false;
      object obj = this.gate;
      bool flag5;
      NotchTab? notchTab;
      bool flag8;
      bool flag9;
      lock (obj)
      {
        ValueTuple<NotchActivityPhase, string>? valueTuple = this.voicePill;
        DateTimeOffset dateTimeOffset = this.clock();
        TimeSpan timeSpan = dateTimeOffset - this.lastTick;
        this.lastTick = dateTimeOffset;
        if (timeSpan < TimeSpan.Zero)
        {
          timeSpan = TimeSpan.Zero;
        }
        if (this.openRequested && !this.openRequestSawHover && dateTimeOffset >= this.openRequestDeadline)
        {
          this.openRequested = false;
          this.openRequestSawHover = false;
        }
        DateTimeOffset? dateTimeOffset2 = this.dragOpenedChatCloseAt;
        if (dateTimeOffset2 != null)
        {
          DateTimeOffset valueOrDefault = dateTimeOffset2.GetValueOrDefault();
          if (dateTimeOffset >= valueOrDefault)
          {
            this.dragOpenedChatCloseAt = null;
            if (this.chatOpenedByFileDrag && this.chatSessionOpen)
            {
              this.chatOpenedByFileDrag = false;
              if (!this.chatComposerHasDraft)
              {
                this.TrackChatSessionEnded(NotchChatSessionCloseReason.DragAbandoned);
                this.chatSessionOpen = false;
                this.chatComposerHasKeyboard = false;
                this.peekHeldOpen = false;
                this.Tabs.ConsumeComposerFocusRequestCore();
                this.pendingDroppedAttachments.Clear();
                this.suppressHoverExpandUntilNotchReentry = this.hovering;
                this.hovering = false;
                flag2 = true;
                flag3 = true;
              }
            }
          }
        }
        if (this.pointerInside && !this.hovering && !this.introOwnsStage && dateTimeOffset - this.pointerEnteredAt >= NotchPresenter.HoverDwell)
        {
          this.hovering = true;
          this.NoteHoverForOpenRequest();
          this.onboardingPinManuallyDismissed = false;
          flag = !this.suppressHoverExpandUntilNotchReentry;
          if (this.introWelcomeAutoShownPin && !this.slots.ContainsKey(typeof(NotchLiveEvent.IntroHatchPeek)))
          {
            this.introWelcomeHadHoverIn = true;
          }
        }
        if (this.suppressHoverExpandUntilNotchReentry && !this.pointerInside && dateTimeOffset >= this.hoverExpandRearmNotBefore)
        {
          this.suppressHoverExpandUntilNotchReentry = false;
        }
        if (this.heldVoiceEvent != null && dateTimeOffset >= this.heldVoiceEventUntil)
        {
          this.heldVoiceEvent = null;
        }
        NotchPresentation.LiveEvent liveEvent = this.presentation as NotchPresentation.LiveEvent;
        NotchPresenter.LiveEventSlot liveEventSlot;
        if (liveEvent != null && this.slots.TryGetValue(liveEvent.Event.GetType(), out liveEventSlot))
        {
          TimeSpan? remaining = liveEventSlot.Remaining;
          if (remaining != null)
          {
            TimeSpan timeSpan2 = remaining.GetValueOrDefault();
            if (!liveEventSlot.PausesOnHover || !this.hovering)
            {
              timeSpan2 -= timeSpan;
              if (timeSpan2 <= TimeSpan.Zero)
              {
                this.slots.Remove(liveEvent.Event.GetType());
              }
              else
              {
                Dictionary<Type, NotchPresenter.LiveEventSlot> dictionary = this.slots;
                Type type = liveEvent.Event.GetType();
                NotchPresenter.LiveEventSlot liveEventSlot2 = liveEventSlot.<Clone>$();
                liveEventSlot2.Remaining = new TimeSpan?(timeSpan2);
                dictionary[type] = liveEventSlot2;
              }
            }
          }
        }
        if (this.presentation is NotchPresentation.TextResponse)
        {
          TimeSpan? remaining = this.textResponseRemaining;
          if (remaining != null)
          {
            TimeSpan timeSpan3 = remaining.GetValueOrDefault();
            if (!this.hovering && !this.textResponseUserPaused && !this.textResponseFollowUpActive)
            {
              timeSpan3 -= timeSpan;
              if (timeSpan3 <= TimeSpan.Zero)
              {
                this.textResponse = null;
                this.textResponseRemaining = null;
              }
              else
              {
                this.textResponseRemaining = new TimeSpan?(timeSpan3);
              }
            }
          }
        }
        flag5 = this.Recompute();
        this.UpdateTicker();
        notchTab = this.tabsBeforeLadder;
        this.tabsBeforeLadder = null;
        ValueTuple<NotchActivityPhase, string>? valueTuple2 = this.voicePill;
        ValueTuple<NotchActivityPhase, string>? valueTuple3 = valueTuple;
        bool flag6 = valueTuple2 != null;
        bool flag7;
        if (flag6 != (valueTuple3 != null))
        {
          flag7 = true;
        }
        else if (!flag6)
        {
          flag7 = false;
        }
        else
        {
          ValueTuple<NotchActivityPhase, string> valueOrDefault2 = valueTuple2.GetValueOrDefault();
          ValueTuple<NotchActivityPhase, string> valueOrDefault3 = valueTuple3.GetValueOrDefault();
          flag7 = valueOrDefault2.Item1 != valueOrDefault3.Item1 || valueOrDefault2.Item2 != valueOrDefault3.Item2;
        }
        flag8 = flag7;
        flag9 = this.hoverOpenedNotchThisPass;
        this.hoverOpenedNotchThisPass = false;
      }
      this.FlushChatAnalytics();
      this.RaiseAfterLadder(flag5, notchTab, flag8, flag9, flag3);
      if (flag2)
      {
        Action dragOpenedChatAbandoned = this.DragOpenedChatAbandoned;
        if (dragOpenedChatAbandoned != null)
        {
          dragOpenedChatAbandoned();
        }
      }
      if (flag)
      {
        try
        {
          Action hoverDwellCompleted = this.HoverDwellCompleted;
          if (hoverDwellCompleted != null)
          {
            hoverDwellCompleted();
          }
        }
        catch (Exception ex)
        {
          Log.Error("Notch", "HoverDwellCompleted handler threw", ex);
        }
      }
    }

    // Token: 0x06001229 RID: 4649 RVA: 0x003A0018 File Offset: 0x003A0018
    private void UpdateTicker()
    {
      if (this.ticker == null)
      {
        return;
      }
      bool flag;
      if ((!this.pointerInside || this.hovering) && this.heldVoiceEvent == null && (!this.suppressHoverExpandUntilNotchReentry || this.pointerInside) && (!this.openRequested || this.openRequestSawHover))
      {
        DateTimeOffset? dateTimeOffset = this.dragOpenedChatCloseAt;
        if (dateTimeOffset == null)
        {
          NotchPresentation.LiveEvent liveEvent = this.presentation as NotchPresentation.LiveEvent;
          NotchPresenter.LiveEventSlot liveEventSlot;
          if (liveEvent == null || !this.slots.TryGetValue(liveEvent.Event.GetType(), out liveEventSlot) || liveEventSlot.Remaining == null)
          {
            if (this.presentation is NotchPresentation.TextResponse)
            {
              TimeSpan? timeSpan = this.textResponseRemaining;
              if (timeSpan != null)
              {
                flag = !this.textResponseUserPaused;
                goto IL_00C1;
              }
            }
            flag = false;
            goto IL_00C1;
          }
        }
      }
      flag = true;
      IL_00C1:
      bool flag2 = flag;
      if (flag2 == this.ticking)
      {
        return;
      }
      this.ticking = flag2;
      if (flag2)
      {
        this.lastTick = this.clock();
        this.ticker.Change(TimeSpan.FromMilliseconds(100L, 0L), TimeSpan.FromMilliseconds(100L, 0L));
        return;
      }
      this.ticker.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
    }

    // Token: 0x0600122A RID: 4650 RVA: 0x003A0144 File Offset: 0x003A0144
    private bool Recompute()
    {
      this.HoldHoverClosedIfShownCardIsGoingAway();
      NotchPresentation notchPresentation = this.Ladder();
      if (notchPresentation == this.presentation)
      {
        this.hasAppliedInitialPresentation = true;
        return false;
      }
      this.TrackPinnedTransition(this.presentation, notchPresentation);
      bool flag = NotchPresenter.IsPeekPresentation(this.presentation) && !NotchPresenter.IsPeekPresentation(notchPresentation);
      this.presentation = notchPresentation;
      if (flag)
      {
        this.Tabs.NoteNotchClosedCore();
      }
      return true;
    }

    // Token: 0x0600122B RID: 4651 RVA: 0x003A01B0 File Offset: 0x003A01B0
    private void HoldHoverClosedIfShownCardIsGoingAway()
    {
      NotchPresentation.LiveEvent liveEvent = this.presentation as NotchPresentation.LiveEvent;
      if (liveEvent == null)
      {
        return;
      }
      NotchLiveEvent @event = liveEvent.Event;
      Type type = @event.GetType();
      if (!((@event is NotchLiveEvent.ClickyUpdate) ? (this.Updates.Current == null) : (NotchPresenter.CardsHoldingHoverAfterLeaving.Contains(type) && !this.slots.ContainsKey(type))))
      {
        return;
      }
      this.suppressHoverExpandUntilNotchReentry = true;
      this.hoverExpandRearmNotBefore = this.clock() + NotchPresenter.HoverExpandHoldAfterCardLeft;
    }

    // Token: 0x1700043F RID: 1087
    // (get) Token: 0x0600122C RID: 4652 RVA: 0x003A023C File Offset: 0x003A023C
    public bool IntroWelcomeAutoPinned
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.introWelcomeActive && this.introWelcomeAutoShownPin;
        }
        return flag2;
      }
    }

    // Token: 0x0600122D RID: 4653 RVA: 0x003A028C File Offset: 0x003A028C
    [NullableContext(1)]
    private void TrackPinnedTransition(NotchPresentation previous, NotchPresentation next)
    {
      bool flag = !this.hasAppliedInitialPresentation;
      this.hasAppliedInitialPresentation = true;
      bool flag2 = previous is NotchPresentation.OnboardingPinned;
      bool flag3 = next is NotchPresentation.OnboardingPinned;
      if (flag)
      {
        if (flag3)
        {
          this.expandedSurfaceStartedAt = new DateTimeOffset?(this.clock());
          return;
        }
      }
      else
      {
        if (!flag2 && flag3)
        {
          this.expandedSurfaceStartedAt = new DateTimeOffset?(this.clock());
          ClickyAnalytics.TrackNotchExpanded(this.IntroWelcomeAutoPinned ? "intro_welcome_auto_pin" : (this.hovering ? "hover" : "onboarding_pin"));
          return;
        }
        if (flag2 && !flag3)
        {
          DateTimeOffset? dateTimeOffset = this.expandedSurfaceStartedAt;
          int num;
          if (dateTimeOffset != null)
          {
            DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
            num = (int)(this.clock() - valueOrDefault).TotalMilliseconds;
          }
          else
          {
            num = 0;
          }
          this.expandedSurfaceStartedAt = null;
          ClickyAnalytics.TrackNotchCollapsed(num);
        }
      }
    }

    // Token: 0x0600122E RID: 4654 RVA: 0x003A0374 File Offset: 0x003A0374
    [NullableContext(1)]
    private NotchPresentation Ladder()
    {
      bool isShowingNotchAgentPeekUnlocked = this.IsShowingNotchAgentPeekUnlocked;
      DateTimeOffset dateTimeOffset = this.clock();
      int? num = this.onboardingPinStep;
      bool flag = num != null && !this.introOwnsStage;
      NotchUpdate notchUpdate = this.Updates.Current;
      bool flag2 = this.slots.Keys.Any<Type>((Type t) => NotchPresenter.ActionableCards.Contains(t)) || notchUpdate != null;
      bool flag3 = this.hovering && !this.suppressHoverExpandUntilNotchReentry && !this.shutUntilPointerLeaves && !flag && !flag2 && !this.isRestrictingForTutorial && !this.introOwnsStage;
      if (this.textInputActive)
      {
        return new NotchPresentation.TextInput(this.textInputDraft, this.textInputAgentFollowUp);
      }
      if (this.clickyDriveShowing)
      {
        return new NotchPresentation.Idle();
      }
      NotchLiveEvent.Dictation dictation;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.Dictation>(out dictation))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(dictation);
      }
      if (!this.RendersInChatUnlocked)
      {
        NotchPresentation.TextResponse textResponse = this.textResponse;
        if (textResponse != null)
        {
          if (textResponse.Answer.Length == 0 && textResponse.IsThinking)
          {
            return NotchPresenter.<Ladder>g__Live|230_0(string.IsNullOrEmpty(this.customStatusLabel) ? new NotchLiveEvent.ActivityThinking() : new NotchLiveEvent.ActivityCustomStatus(this.customStatusLabel));
          }
          return textResponse;
        }
      }
      NotchLiveEvent.IntegrationConnected integrationConnected;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.IntegrationConnected>(out integrationConnected))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(integrationConnected);
      }
      if (this.VoiceTurnActiveForPresentation && isShowingNotchAgentPeekUnlocked)
      {
        this.keepsOpenThroughVoiceTurn = true;
      }
      if (this.keepsOpenThroughVoiceTurn)
      {
        bool flag4 = this.hovering || this.peekHeldOpen || this.chatBeingResized || this.heldOpenForPresentation || this.heldOpenByArc || this.documentOpen || this.ChatSessionHoldingNotch || this.openRequested;
        if (this.VoiceTurnActiveForPresentation && flag4)
        {
          this.voicePill = (this.tourShowcaseShowing ? null : this.VoicePillPhaseForCurrentVoiceState());
          return NotchPresenter.<Ladder>g__Peek|230_2();
        }
        this.keepsOpenThroughVoiceTurn = false;
      }
      this.voicePill = null;
      NotchLiveEvent.VoiceReplyMutedCopiedToClipboard voiceReplyMutedCopiedToClipboard;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.VoiceReplyMutedCopiedToClipboard>(out voiceReplyMutedCopiedToClipboard))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(voiceReplyMutedCopiedToClipboard);
      }
      if (this.voiceState == VoiceState.Listening)
      {
        return NotchPresenter.<Ladder>g__Live|230_0(new NotchLiveEvent.ActivityListening());
      }
      bool chatSessionHoldingNotch = this.ChatSessionHoldingNotch;
      if ((this.peekHeldOpen || this.chatBeingResized || this.heldOpenForPresentation || this.heldOpenByArc || this.openRequested || this.documentOpen || chatSessionHoldingNotch) && (!this.shutUntilPointerLeaves || isShowingNotchAgentPeekUnlocked))
      {
        if (!isShowingNotchAgentPeekUnlocked)
        {
          this.<Ladder>g__NoteFreshOpen|230_3(false);
          if (chatSessionHoldingNotch && this.Tabs.SelectedCore != NotchTab.Chat)
          {
            this.Tabs.SelectCoreInternal(NotchTab.Chat);
          }
        }
        return NotchPresenter.<Ladder>g__Peek|230_2();
      }
      if (flag3)
      {
        if (this.hoverRoutesToNotch && this.isSignedIn)
        {
          if (!isShowingNotchAgentPeekUnlocked)
          {
            this.hoverOpenedNotchThisPass = true;
            this.<Ladder>g__NoteFreshOpen|230_3(true);
          }
          return NotchPresenter.<Ladder>g__Peek|230_2();
        }
        if (!this.isSignedIn)
        {
          return new NotchPresentation.OnboardingPinned(-1);
        }
      }
      if (notchUpdate != null)
      {
        return NotchPresenter.<Ladder>g__Live|230_0(new NotchLiveEvent.ClickyUpdate(notchUpdate.RailKey, notchUpdate.Kind.RawValue()));
      }
      if (this.voiceState == VoiceState.Processing)
      {
        return NotchPresenter.<Ladder>g__Live|230_0(string.IsNullOrEmpty(this.customStatusLabel) ? new NotchLiveEvent.ActivityThinking() : new NotchLiveEvent.ActivityCustomStatus(this.customStatusLabel));
      }
      if (this.voiceState == VoiceState.Responding)
      {
        return NotchPresenter.<Ladder>g__Live|230_0(new NotchLiveEvent.ActivitySpeaking());
      }
      if (this.heldVoiceEvent != null && dateTimeOffset < this.heldVoiceEventUntil)
      {
        return NotchPresenter.<Ladder>g__Live|230_0(this.heldVoiceEvent);
      }
      NotchLiveEvent.HandoffSent handoffSent;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.HandoffSent>(out handoffSent))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(handoffSent);
      }
      NotchLiveEvent.HandoffQueued handoffQueued;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.HandoffQueued>(out handoffQueued))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(handoffQueued);
      }
      NotchLiveEvent.OnboardingTutorialInvite onboardingTutorialInvite;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.OnboardingTutorialInvite>(out onboardingTutorialInvite))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(onboardingTutorialInvite);
      }
      NotchLiveEvent.OnboardingTutorialResume onboardingTutorialResume;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.OnboardingTutorialResume>(out onboardingTutorialResume) && this.hovering)
      {
        return NotchPresenter.<Ladder>g__Live|230_0(onboardingTutorialResume);
      }
      NotchLiveEvent.MicrophoneTrouble microphoneTrouble;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.MicrophoneTrouble>(out microphoneTrouble))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(microphoneTrouble);
      }
      NotchLiveEvent.DictationCopiedToClipboard dictationCopiedToClipboard;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.DictationCopiedToClipboard>(out dictationCopiedToClipboard))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(dictationCopiedToClipboard);
      }
      NotchLiveEvent.MeetingCountdown meetingCountdown;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.MeetingCountdown>(out meetingCountdown))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(meetingCountdown);
      }
      NotchLiveEvent.DocumentFolderAccessDenied documentFolderAccessDenied;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.DocumentFolderAccessDenied>(out documentFolderAccessDenied))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(documentFolderAccessDenied);
      }
      NotchLiveEvent.MorningSuggestions morningSuggestions;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.MorningSuggestions>(out morningSuggestions) && this.voiceState == VoiceState.Idle)
      {
        return NotchPresenter.<Ladder>g__Live|230_0(morningSuggestions);
      }
      NotchLiveEvent.SpotifyVoiceTip spotifyVoiceTip;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.SpotifyVoiceTip>(out spotifyVoiceTip))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(spotifyVoiceTip);
      }
      NotchLiveEvent.SidecarLearningSuggestion sidecarLearningSuggestion;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.SidecarLearningSuggestion>(out sidecarLearningSuggestion))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(sidecarLearningSuggestion);
      }
      NotchLiveEvent.IntegrationSuggestion integrationSuggestion;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.IntegrationSuggestion>(out integrationSuggestion))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(integrationSuggestion);
      }
      NotchLiveEvent.DictionaryWordLearned dictionaryWordLearned;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.DictionaryWordLearned>(out dictionaryWordLearned))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(dictionaryWordLearned);
      }
      NotchLiveEvent.AppUpdated appUpdated;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.AppUpdated>(out appUpdated))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(appUpdated);
      }
      bool flag5 = !this.ambientSuppressed;
      NotchLiveEvent.AgentMultiActivity agentMultiActivity;
      if (NotchPresenter.ShowsRunningAgentsStrip && flag5 && this.<Ladder>g__Has|230_1<NotchLiveEvent.AgentMultiActivity>(out agentMultiActivity))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(agentMultiActivity);
      }
      NotchLiveEvent.AgentTask agentTask;
      if (NotchPresenter.ShowsRunningAgentsStrip && flag5 && this.<Ladder>g__Has|230_1<NotchLiveEvent.AgentTask>(out agentTask))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(agentTask);
      }
      NotchLiveEvent.IntroHatchPeek introHatchPeek;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.IntroHatchPeek>(out introHatchPeek))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(introHatchPeek);
      }
      NotchLiveEvent.IntroSkillPing introSkillPing;
      if (this.<Ladder>g__Has|230_1<NotchLiveEvent.IntroSkillPing>(out introSkillPing))
      {
        return NotchPresenter.<Ladder>g__Live|230_0(introSkillPing);
      }
      bool flag6 = !this.introWelcomeActive || this.hovering || this.introWelcomeAutoShownPin;
      if (flag && !this.onboardingPinManuallyDismissed && flag6)
      {
        return new NotchPresentation.OnboardingPinned(this.onboardingPinStep.Value);
      }
      NotchLiveEvent.SkillPinned skillPinned;
      if (flag5 && this.<Ladder>g__Has|230_1<NotchLiveEvent.SkillPinned>(out skillPinned) && skillPinned.Count > 0)
      {
        return NotchPresenter.<Ladder>g__Live|230_0(skillPinned);
      }
      if (flag5 && this.restingMarkShowsHandle && this.restingMarkIsWorking)
      {
        return NotchPresenter.<Ladder>g__Live|230_0(new NotchLiveEvent.RestingMark());
      }
      return new NotchPresentation.Idle();
    }

    // Token: 0x17000440 RID: 1088
    // (get) Token: 0x0600122F RID: 4655 RVA: 0x003A08C0 File Offset: 0x003A08C0
    // (set) Token: 0x06001230 RID: 4656 RVA: 0x003A08C8 File Offset: 0x003A08C8
    public static bool ShowsRunningAgentsStrip { get; set; } = false;

    // Token: 0x17000441 RID: 1089
    // (get) Token: 0x06001231 RID: 4657 RVA: 0x003A08D0 File Offset: 0x003A08D0
    // (set) Token: 0x06001232 RID: 4658 RVA: 0x003A08D8 File Offset: 0x003A08D8
    public static bool RestingMarkIsGlowingBar { get; set; } = true;

    // Token: 0x06001233 RID: 4659 RVA: 0x003A08E0 File Offset: 0x003A08E0
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "Duration", "PausesOnHover" })]
    private static ValueTuple<TimeSpan?, bool> DefaultAutoDismiss([Nullable(1)] NotchLiveEvent liveEvent)
    {
      ValueTuple<TimeSpan?, bool> valueTuple;
      if (!(liveEvent is NotchLiveEvent.MicrophoneTrouble))
      {
        if (!(liveEvent is NotchLiveEvent.DictationCopiedToClipboard))
        {
          if (!(liveEvent is NotchLiveEvent.VoiceReplyMutedCopiedToClipboard))
          {
            if (!(liveEvent is NotchLiveEvent.AppUpdated))
            {
              if (!(liveEvent is NotchLiveEvent.HandoffQueued))
              {
                if (!(liveEvent is NotchLiveEvent.HandoffSent))
                {
                  if (!(liveEvent is NotchLiveEvent.DictionaryWordLearned))
                  {
                    valueTuple = new ValueTuple<TimeSpan?, bool>(null, false);
                  }
                  else
                  {
                    valueTuple = new ValueTuple<TimeSpan?, bool>(new TimeSpan?(TimeSpan.FromSeconds(6L)), false);
                  }
                }
                else
                {
                  valueTuple = new ValueTuple<TimeSpan?, bool>(new TimeSpan?(TimeSpan.FromSeconds(1.5)), false);
                }
              }
              else
              {
                valueTuple = new ValueTuple<TimeSpan?, bool>(new TimeSpan?(TimeSpan.FromSeconds(1.4)), false);
              }
            }
            else
            {
              valueTuple = new ValueTuple<TimeSpan?, bool>(new TimeSpan?(NotchPresenter.AppUpdatedAutoHide), true);
            }
          }
          else
          {
            valueTuple = new ValueTuple<TimeSpan?, bool>(new TimeSpan?(NotchPresenter.MutedSpeakerAutoHide), true);
          }
        }
        else
        {
          valueTuple = new ValueTuple<TimeSpan?, bool>(new TimeSpan?(NotchPresenter.DictationClipboardAutoHide), false);
        }
      }
      else
      {
        valueTuple = new ValueTuple<TimeSpan?, bool>(new TimeSpan?(NotchPresenter.MicrophoneTroubleAutoHide), false);
      }
      return valueTuple;
    }

    // Token: 0x06001234 RID: 4660 RVA: 0x003A09E0 File Offset: 0x003A09E0
    [NullableContext(1)]
    public static TimeSpan AutoDismissForTextResponse(string answer, bool duringTutorial = false)
    {
      string text = answer.Trim();
      double num;
      if (text.Length == 0)
      {
        num = 10.0;
      }
      else
      {
        int num2 = text.Split(null, StringSplitOptions.RemoveEmptyEntries).Length;
        int num3 = text.Split('\n', StringSplitOptions.None).Sum<string>((string line) => Math.Max(1, (int)Math.Ceiling((double)line.Length / 74.0)));
        num = (double)((num2 <= 32 && num3 <= 3) ? 10 : ((num2 <= 90 && num3 <= 8) ? 20 : 30));
      }
      if (duringTutorial)
      {
        num = Math.Min(num, 6.0);
      }
      return TimeSpan.FromSeconds(num);
    }

    // Token: 0x06001235 RID: 4661 RVA: 0x003A0A78 File Offset: 0x003A0A78
    private void RaiseChanged()
    {
      try
      {
        EventHandler presentationChanged = this.PresentationChanged;
        if (presentationChanged != null)
        {
          presentationChanged(this, EventArgs.Empty);
        }
      }
      catch (Exception ex)
      {
        Log.Error("Notch", "PresentationChanged handler threw", ex);
      }
    }

    // Token: 0x06001236 RID: 4662 RVA: 0x003A0AC4 File Offset: 0x003A0AC4
    public void Dispose()
    {
      Timer timer = this.ticker;
      if (timer == null)
      {
        return;
      }
      timer.Dispose();
    }

    // Token: 0x06001237 RID: 4663 RVA: 0x003A0AD8 File Offset: 0x003A0AD8
    // Note: this type is marked as 'beforefieldinit'.
    static NotchPresenter()
    {
      HashSet<Type> hashSet = new HashSet<Type>();
      foreach (Type type in NotchPresenter.ActionableCards)
      {
        hashSet.Add(type);
      }
      hashSet.Add(typeof(NotchLiveEvent.IntegrationConnected));
      NotchPresenter.CardsHoldingHoverAfterLeaving = hashSet;
    }

    // Token: 0x06001247 RID: 4679 RVA: 0x003A0F30 File Offset: 0x003A0F30
    [NullableContext(1)]
    [CompilerGenerated]
    internal static NotchPresentation <Ladder>g__Live|230_0(NotchLiveEvent e)
    {
      return new NotchPresentation.LiveEvent(e);
    }

    // Token: 0x06001248 RID: 4680 RVA: 0x003A0F38 File Offset: 0x003A0F38
    [NullableContext(1)]
    [CompilerGenerated]
    private bool <Ladder>g__Has|230_1<T>(out T value) where T : NotchLiveEvent
    {
      NotchPresenter.LiveEventSlot liveEventSlot;
      if (this.slots.TryGetValue(typeof(T), out liveEventSlot))
      {
        value = (T)((object)liveEventSlot.Event);
        return true;
      }
      value = default(T);
      return false;
    }

    // Token: 0x06001249 RID: 4681 RVA: 0x003A0F7C File Offset: 0x003A0F7C
    [NullableContext(1)]
    [CompilerGenerated]
    internal static NotchPresentation <Ladder>g__Peek|230_2()
    {
      return NotchPresenter.<Ladder>g__Live|230_0(new NotchLiveEvent.AgentPeek(0.0));
    }

    // Token: 0x0600124A RID: 4682 RVA: 0x003A0F94 File Offset: 0x003A0F94
    [CompilerGenerated]
    private void <Ladder>g__NoteFreshOpen|230_3(bool isHoverOpen = false)
    {
      NotchTab notchTab = this.tabsBeforeLadder.GetValueOrDefault();
      if (this.tabsBeforeLadder == null)
      {
        notchTab = this.Tabs.SelectedCore;
        this.tabsBeforeLadder = new NotchTab?(notchTab);
      }
      this.Tabs.NoteFreshOpenCore(isHoverOpen);
    }

    // Token: 0x040007C6 RID: 1990
    public static readonly TimeSpan HoverDwell = TimeSpan.FromSeconds(0.45);

    // Token: 0x040007C7 RID: 1991
    public static readonly TimeSpan MutedSpeakerAutoHide = TimeSpan.FromSeconds(10L);

    // Token: 0x040007C8 RID: 1992
    public static readonly TimeSpan MicrophoneTroubleAutoHide = TimeSpan.FromSeconds(8L);

    // Token: 0x040007C9 RID: 1993
    public static readonly TimeSpan DictationClipboardAutoHide = TimeSpan.FromSeconds(6L);

    // Token: 0x040007CA RID: 1994
    public static readonly TimeSpan AppUpdatedAutoHide = TimeSpan.FromSeconds(12L);

    // Token: 0x040007CB RID: 1995
    public static readonly TimeSpan HoverExpandHoldAfterCardLeft = TimeSpan.FromSeconds(1L);

    // Token: 0x040007CC RID: 1996
    private static readonly TimeSpan HoldAfterListening = TimeSpan.FromSeconds(2.5);

    // Token: 0x040007CD RID: 1997
    private static readonly TimeSpan HoldAfterProcessing = TimeSpan.FromSeconds(30L);

    // Token: 0x040007CE RID: 1998
    private static readonly TimeSpan HoldAfterResponding = TimeSpan.FromSeconds(0.3);

    // Token: 0x040007CF RID: 1999
    [Nullable(1)]
    private readonly object gate = new object();

    // Token: 0x040007D0 RID: 2000
    [Nullable(1)]
    private readonly Func<DateTimeOffset> clock;

    // Token: 0x040007D1 RID: 2001
    private readonly Timer ticker;

    // Token: 0x040007D2 RID: 2002
    [Nullable(1)]
    private readonly Dictionary<Type, NotchPresenter.LiveEventSlot> slots = new Dictionary<Type, NotchPresenter.LiveEventSlot>();

    // Token: 0x040007D3 RID: 2003
    [Nullable(1)]
    private NotchPresentation presentation = new NotchPresentation.Idle();

    // Token: 0x040007D4 RID: 2004
    private bool textInputActive;

    // Token: 0x040007D5 RID: 2005
    [Nullable(1)]
    private string textInputDraft = "";

    // Token: 0x040007D6 RID: 2006
    private NotchAgentFollowUp textInputAgentFollowUp;

    // Token: 0x040007D7 RID: 2007
    private NotchPresentation.TextResponse textResponse;

    // Token: 0x040007D8 RID: 2008
    private TimeSpan? textResponseRemaining;

    // Token: 0x040007D9 RID: 2009
    private TimeSpan textResponseTotal;

    // Token: 0x040007DA RID: 2010
    private bool textResponseUserPaused;

    // Token: 0x040007DB RID: 2011
    private bool textResponseFollowUpActive;

    // Token: 0x040007DC RID: 2012
    private bool ambientSuppressed;

    // Token: 0x040007DD RID: 2013
    private int? onboardingPinStep;

    // Token: 0x040007DE RID: 2014
    private bool onboardingPinManuallyDismissed;

    // Token: 0x040007DF RID: 2015
    private bool isSignedIn = true;

    // Token: 0x040007E0 RID: 2016
    private bool isRestrictingForTutorial;

    // Token: 0x040007E1 RID: 2017
    private VoiceState voiceState;

    // Token: 0x040007E2 RID: 2018
    private string customStatusLabel;

    // Token: 0x040007E3 RID: 2019
    private NotchLiveEvent heldVoiceEvent;

    // Token: 0x040007E4 RID: 2020
    private DateTimeOffset heldVoiceEventUntil;

    // Token: 0x040007E5 RID: 2021
    private bool voiceTurnTerminationSuppressesNextHold;

    // Token: 0x040007E6 RID: 2022
    private bool pointerInside;

    // Token: 0x040007E7 RID: 2023
    private DateTimeOffset pointerEnteredAt;

    // Token: 0x040007E8 RID: 2024
    private bool hovering;

    // Token: 0x040007E9 RID: 2025
    private bool suppressHoverExpandUntilNotchReentry;

    // Token: 0x040007EA RID: 2026
    private bool shutUntilPointerLeaves;

    // Token: 0x040007EB RID: 2027
    private DateTimeOffset hoverExpandRearmNotBefore = DateTimeOffset.MinValue;

    // Token: 0x040007EC RID: 2028
    private DateTimeOffset lastTick;

    // Token: 0x040007F1 RID: 2033
    public static readonly TimeSpan OpenRequestTimeout = TimeSpan.FromSeconds(6L);

    // Token: 0x040007F2 RID: 2034
    public static readonly TimeSpan DragOpenedChatCloseGrace = TimeSpan.FromSeconds(0.35);

    // Token: 0x040007F3 RID: 2035
    private bool hoverRoutesToNotch;

    // Token: 0x040007F4 RID: 2036
    private bool peekHeldOpen;

    // Token: 0x040007F5 RID: 2037
    private bool chatBeingResized;

    // Token: 0x040007F6 RID: 2038
    private bool clickyDriveShowing;

    // Token: 0x040007F7 RID: 2039
    private bool heldOpenForPresentation;

    // Token: 0x040007F8 RID: 2040
    private bool heldOpenByArc;

    // Token: 0x040007F9 RID: 2041
    private bool openRequested;

    // Token: 0x040007FA RID: 2042
    private bool openRequestSawHover;

    // Token: 0x040007FB RID: 2043
    private DateTimeOffset openRequestDeadline;

    // Token: 0x040007FC RID: 2044
    private bool documentOpen;

    // Token: 0x040007FD RID: 2045
    private bool chatSessionOpen;

    // Token: 0x040007FE RID: 2046
    private bool chatComposerHasDraft;

    // Token: 0x040007FF RID: 2047
    private bool chatComposerHasKeyboard;

    // Token: 0x04000800 RID: 2048
    private bool chatOpenedByFileDrag;

    // Token: 0x04000801 RID: 2049
    private DateTimeOffset? dragOpenedChatCloseAt;

    // Token: 0x04000802 RID: 2050
    private bool keepsOpenThroughVoiceTurn;

    // Token: 0x04000803 RID: 2051
    private bool voiceTurnBeganWithListening = true;

    // Token: 0x04000804 RID: 2052
    private bool restingMarkShowsHandle;

    // Token: 0x04000805 RID: 2053
    private bool restingMarkIsWorking;

    // Token: 0x04000806 RID: 2054
    [Nullable(1)]
    private readonly List<string> pendingDroppedAttachments = new List<string>();

    // Token: 0x04000807 RID: 2055
    [TupleElementNames(new string[] { "Phase", "Label" })]
    [Nullable(new byte[] { 0, 2 })]
    private ValueTuple<NotchActivityPhase, string>? voicePill;

    // Token: 0x04000808 RID: 2056
    private NotchTab? tabsBeforeLadder;

    // Token: 0x04000809 RID: 2057
    private bool hoverOpenedNotchThisPass;

    // Token: 0x0400080E RID: 2062
    private bool windowShowsMainThread;

    // Token: 0x0400080F RID: 2063
    private bool routesTextModeToNotchChat;

    // Token: 0x04000810 RID: 2064
    private DateTimeOffset? chatSessionStartedAt;

    // Token: 0x04000811 RID: 2065
    private NotchChatSessionOpenVia chatSessionOpenedVia;

    // Token: 0x04000812 RID: 2066
    private int chatSessionSentMessageCount;

    // Token: 0x04000813 RID: 2067
    private int chatSessionSentAttachmentCount;

    // Token: 0x04000814 RID: 2068
    [Nullable(1)]
    private readonly List<JsonObject> pendingChatAnalytics = new List<JsonObject>();

    // Token: 0x04000816 RID: 2070
    private bool tourShowcaseShowing;

    // Token: 0x04000817 RID: 2071
    private bool introWelcomeActive;

    // Token: 0x04000818 RID: 2072
    private bool introWelcomeAutoShownPin;

    // Token: 0x04000819 RID: 2073
    private bool introWelcomeHadHoverIn;

    // Token: 0x0400081A RID: 2074
    private bool introOwnsStage;

    // Token: 0x0400081B RID: 2075
    private bool ticking;

    // Token: 0x0400081C RID: 2076
    private bool hasAppliedInitialPresentation;

    // Token: 0x0400081D RID: 2077
    private DateTimeOffset? expandedSurfaceStartedAt;

    // Token: 0x04000820 RID: 2080
    public const int SignedOutCardStep = -1;

    // Token: 0x04000821 RID: 2081
    [Nullable(1)]
    private static readonly HashSet<Type> ActionableCards = new HashSet<Type>
    {
      typeof(NotchLiveEvent.IntegrationSuggestion),
      typeof(NotchLiveEvent.MeetingCountdown),
      typeof(NotchLiveEvent.DictationCopiedToClipboard),
      typeof(NotchLiveEvent.VoiceReplyMutedCopiedToClipboard),
      typeof(NotchLiveEvent.MicrophoneTrouble),
      typeof(NotchLiveEvent.AppUpdated),
      typeof(NotchLiveEvent.MorningSuggestions),
      typeof(NotchLiveEvent.OnboardingTutorialInvite),
      typeof(NotchLiveEvent.SidecarLearningSuggestion),
      typeof(NotchLiveEvent.SpotifyVoiceTip),
      typeof(NotchLiveEvent.DictionaryWordLearned),
      typeof(NotchLiveEvent.DocumentFolderAccessDenied)
    };

    // Token: 0x04000822 RID: 2082
    [Nullable(1)]
    private static readonly HashSet<Type> CardsHoldingHoverAfterLeaving;

    // Token: 0x02000458 RID: 1112
    [NullableContext(1)]
    [Nullable(0)]
    private sealed class LiveEventSlot : IEquatable<NotchPresenter.LiveEventSlot>
    {
      // Token: 0x06002E3E RID: 11838 RVA: 0x0042F1E4 File Offset: 0x0042F1E4
      public LiveEventSlot(NotchLiveEvent Event, TimeSpan? Remaining, bool PausesOnHover)
      {
        this.Event = Event;
        this.Remaining = Remaining;
        this.PausesOnHover = PausesOnHover;
        base..ctor();
      }

      // Token: 0x17000867 RID: 2151
      // (get) Token: 0x06002E3F RID: 11839 RVA: 0x0042F204 File Offset: 0x0042F204
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchPresenter.LiveEventSlot);
        }
      }

      // Token: 0x17000868 RID: 2152
      // (get) Token: 0x06002E40 RID: 11840 RVA: 0x0042F210 File Offset: 0x0042F210
      // (set) Token: 0x06002E41 RID: 11841 RVA: 0x0042F218 File Offset: 0x0042F218
      public NotchLiveEvent Event { get; set; }

      // Token: 0x17000869 RID: 2153
      // (get) Token: 0x06002E42 RID: 11842 RVA: 0x0042F224 File Offset: 0x0042F224
      // (set) Token: 0x06002E43 RID: 11843 RVA: 0x0042F22C File Offset: 0x0042F22C
      public TimeSpan? Remaining { get; set; }

      // Token: 0x1700086A RID: 2154
      // (get) Token: 0x06002E44 RID: 11844 RVA: 0x0042F238 File Offset: 0x0042F238
      // (set) Token: 0x06002E45 RID: 11845 RVA: 0x0042F240 File Offset: 0x0042F240
      public bool PausesOnHover { get; set; }

      // Token: 0x06002E46 RID: 11846 RVA: 0x0042F24C File Offset: 0x0042F24C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("LiveEventSlot");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002E47 RID: 11847 RVA: 0x0042F298 File Offset: 0x0042F298
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Event = ");
        builder.Append(this.Event);
        builder.Append(", Remaining = ");
        builder.Append(this.Remaining.ToString());
        builder.Append(", PausesOnHover = ");
        builder.Append(this.PausesOnHover.ToString());
        return true;
      }

      // Token: 0x06002E48 RID: 11848 RVA: 0x0042F314 File Offset: 0x0042F314
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchPresenter.LiveEventSlot left, NotchPresenter.LiveEventSlot right)
      {
        return !(left == right);
      }

      // Token: 0x06002E49 RID: 11849 RVA: 0x0042F320 File Offset: 0x0042F320
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchPresenter.LiveEventSlot left, NotchPresenter.LiveEventSlot right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002E4A RID: 11850 RVA: 0x0042F334 File Offset: 0x0042F334
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<NotchLiveEvent>.Default.GetHashCode(this.<Event>k__BackingField)) * -1521134295 + EqualityComparer<TimeSpan?>.Default.GetHashCode(this.<Remaining>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<PausesOnHover>k__BackingField);
      }

      // Token: 0x06002E4B RID: 11851 RVA: 0x0042F398 File Offset: 0x0042F398
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchPresenter.LiveEventSlot);
      }

      // Token: 0x06002E4C RID: 11852 RVA: 0x0042F3A8 File Offset: 0x0042F3A8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchPresenter.LiveEventSlot other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<NotchLiveEvent>.Default.Equals(this.<Event>k__BackingField, other.<Event>k__BackingField) && EqualityComparer<TimeSpan?>.Default.Equals(this.<Remaining>k__BackingField, other.<Remaining>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<PausesOnHover>k__BackingField, other.<PausesOnHover>k__BackingField));
      }

      // Token: 0x06002E4E RID: 11854 RVA: 0x0042F424 File Offset: 0x0042F424
      [CompilerGenerated]
      private LiveEventSlot(NotchPresenter.LiveEventSlot original)
      {
        this.Event = original.<Event>k__BackingField;
        this.Remaining = original.<Remaining>k__BackingField;
        this.PausesOnHover = original.<PausesOnHover>k__BackingField;
      }

      // Token: 0x06002E4F RID: 11855 RVA: 0x0042F450 File Offset: 0x0042F450
      [CompilerGenerated]
      public void Deconstruct(out NotchLiveEvent Event, out TimeSpan? Remaining, out bool PausesOnHover)
      {
        Event = this.Event;
        Remaining = this.Remaining;
        PausesOnHover = this.PausesOnHover;
      }
    }
  }
}
