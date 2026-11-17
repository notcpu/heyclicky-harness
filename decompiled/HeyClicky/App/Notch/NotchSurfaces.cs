using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.Notch.Surfaces;
using Windows.UI;

namespace HeyClicky.App.Notch
{
  // Token: 0x0200013A RID: 314
  [NullableContext(1)]
  [Nullable(0)]
  public static class NotchSurfaces
  {
    // Token: 0x17000326 RID: 806
    // (get) Token: 0x0600111A RID: 4378 RVA: 0x0058A9F4 File Offset: 0x0058A9F4
    // (set) Token: 0x0600111B RID: 4379 RVA: 0x0058A9FC File Offset: 0x0058A9FC
    public static Func<string, IReadOnlyList<string>> UseCasesFor { get; set; } = delegate(string toolkit)
    {
      string text = toolkit.ToLowerInvariant();
      string[] array;
      if (!(text == "gmail"))
      {
        if (!(text == "github"))
        {
          if (!(text == "googlecalendar"))
          {
            array = new string[] { "Answer questions with your data", "Automate busywork", "Draft and summarize", "Keep things in sync" };
          }
          else
          {
            array = new string[] { "Plan your week", "Find a free slot", "Prep for your next meeting", "Move a meeting" };
          }
        }
        else
        {
          array = new string[] { "Review open pull requests", "Triage new issues", "Summarize a repo", "Write release notes" };
        }
      }
      else
      {
        array = new string[] { "Draft replies from your inbox", "Summarize unread threads", "Find that one email", "Clean up newsletters" };
      }
      return array;
    };

    // Token: 0x0600111C RID: 4380 RVA: 0x0058AA04 File Offset: 0x0058AA04
    [return: Nullable(2)]
    public static NotchSurface Create(NotchPresentation p, INotchHost host)
    {
      NotchSurfaces.<>c__DisplayClass4_0 CS$<>8__locals1 = new NotchSurfaces.<>c__DisplayClass4_0();
      CS$<>8__locals1.host = host;
      p = NotchGeometry.Shown(p);
      NotchPresentation.TextInput textInput = p as NotchPresentation.TextInput;
      if (textInput != null)
      {
        return new TextInputSurface(textInput, CS$<>8__locals1.host.Services.Auth.IsSignedIn, () => CS$<>8__locals1.host.WindowHandle);
      }
      NotchPresentation.TextResponse textResponse = p as NotchPresentation.TextResponse;
      if (textResponse != null)
      {
        return new TextResponseSurface(textResponse);
      }
      NotchPresentation.OnboardingPinned onboardingPinned = p as NotchPresentation.OnboardingPinned;
      if (onboardingPinned == null)
      {
        NotchPresentation.LiveEvent liveEvent = p as NotchPresentation.LiveEvent;
        if (liveEvent != null)
        {
          NotchLiveEvent @event = liveEvent.Event;
          ValueTuple<ActivityPhase, string, bool>? valueTuple = ActivitySurface.Classify(@event);
          if (valueTuple != null)
          {
            ValueTuple<ActivityPhase, string, bool> valueOrDefault = valueTuple.GetValueOrDefault();
            ActivityPhase item = valueOrDefault.Item1;
            string item2 = valueOrDefault.Item2;
            bool item3 = valueOrDefault.Item3;
            return new ActivitySurface(item, item2, item3, new Func<Color>(CS$<>8__locals1.<Create>g__Cursor|0), null);
          }
          NotchLiveEvent.AgentTask agentTask = @event as NotchLiveEvent.AgentTask;
          if (agentTask != null)
          {
            return new AgentTaskSurface(agentTask, AgentColors.For(agentTask.TaskId, 0));
          }
          if (!(@event is NotchLiveEvent.RestingMark))
          {
            if (@event is NotchLiveEvent.ClickyUpdate)
            {
              return new ClickyUpdateSurface();
            }
            NotchLiveEvent.AppUpdated appUpdated = @event as NotchLiveEvent.AppUpdated;
            if (appUpdated != null)
            {
              return new AppUpdatedSurface(appUpdated);
            }
            if (@event is NotchLiveEvent.DictationCopiedToClipboard)
            {
              return new DictationClipboardSurface();
            }
            NotchLiveEvent.VoiceReplyMutedCopiedToClipboard voiceReplyMutedCopiedToClipboard = @event as NotchLiveEvent.VoiceReplyMutedCopiedToClipboard;
            if (voiceReplyMutedCopiedToClipboard != null)
            {
              return new MutedSpeakerSurface(voiceReplyMutedCopiedToClipboard);
            }
            NotchLiveEvent.MicrophoneTrouble microphoneTrouble = @event as NotchLiveEvent.MicrophoneTrouble;
            if (microphoneTrouble != null)
            {
              return new MicrophoneTroubleSurface(microphoneTrouble);
            }
            NotchLiveEvent.MeetingCountdown meetingCountdown = @event as NotchLiveEvent.MeetingCountdown;
            if (meetingCountdown != null)
            {
              return new MeetingCountdownSurface(meetingCountdown);
            }
            if (@event is NotchLiveEvent.OnboardingTutorialInvite)
            {
              return new TutorialInviteSurface(TutorialInviteSurface.Mode.Invite, true);
            }
            if (@event is NotchLiveEvent.OnboardingTutorialResume)
            {
              return new TutorialInviteSurface(TutorialInviteSurface.Mode.Resume, CS$<>8__locals1.host.SizeContext.TutorialResumeShowsContinue);
            }
            NotchLiveEvent.MorningSuggestions morningSuggestions = @event as NotchLiveEvent.MorningSuggestions;
            if (morningSuggestions != null)
            {
              return new MorningSuggestionsSurface(morningSuggestions);
            }
            NotchLiveEvent.IntegrationSuggestion integrationSuggestion = @event as NotchLiveEvent.IntegrationSuggestion;
            if (integrationSuggestion != null)
            {
              IReadOnlyList<string> readOnlyList = NotchSurfaces.UseCasesFor(integrationSuggestion.Toolkit);
              CS$<>8__locals1.host.SizeContext.IntegrationHasUseCases = readOnlyList.Count > 0;
              return new IntegrationSuggestionSurface(integrationSuggestion, readOnlyList);
            }
            NotchLiveEvent.IntegrationConnected integrationConnected = @event as NotchLiveEvent.IntegrationConnected;
            if (integrationConnected != null)
            {
              IReadOnlyList<string> readOnlyList2 = NotchSurfaces.UseCasesFor(integrationConnected.Toolkit);
              CS$<>8__locals1.host.SizeContext.IntegrationHasUseCases = readOnlyList2.Count > 0;
              return new IntegrationConnectedSurface(integrationConnected, readOnlyList2);
            }
            if (@event is NotchLiveEvent.AgentPeek)
            {
              return new NotchPeekSurface();
            }
            if (@event is NotchLiveEvent.HandoffQueued || @event is NotchLiveEvent.HandoffSent)
            {
              return new HandoffSurface(@event);
            }
            NotchLiveEvent.SkillPinned skillPinned = @event as NotchLiveEvent.SkillPinned;
            if (skillPinned != null)
            {
              return new SkillPinnedSurface(skillPinned);
            }
            NotchLiveEvent.IntroSkillPing introSkillPing = @event as NotchLiveEvent.IntroSkillPing;
            if (introSkillPing != null)
            {
              return new IntroSkillPingSurface(introSkillPing, CS$<>8__locals1.<Create>g__Cursor|0());
            }
            if (@event is NotchLiveEvent.SidecarLearningSuggestion || @event is NotchLiveEvent.SpotifyVoiceTip)
            {
              return new AppTipSurface(@event);
            }
            NotchLiveEvent.DictionaryWordLearned dictionaryWordLearned = @event as NotchLiveEvent.DictionaryWordLearned;
            if (dictionaryWordLearned != null)
            {
              return new DictionaryLearnedSurface(dictionaryWordLearned);
            }
            NotchLiveEvent.DocumentFolderAccessDenied documentFolderAccessDenied = @event as NotchLiveEvent.DocumentFolderAccessDenied;
            if (documentFolderAccessDenied != null)
            {
              return new DocumentAccessSurface(documentFolderAccessDenied);
            }
          }
          else
          {
            if (!NotchGeometry.IsWorkingBar(p))
            {
              return new RestingMarkSurface();
            }
            return null;
          }
        }
        return null;
      }
      return new OnboardingCardSurface(onboardingPinned.Step);
    }
  }
}
