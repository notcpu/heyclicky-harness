using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;

namespace HeyClicky.App.Notch
{
  // Token: 0x02000133 RID: 307
  [NullableContext(1)]
  [Nullable(0)]
  public static class NotchAccessibility
  {
    // Token: 0x060010BD RID: 4285 RVA: 0x00587B30 File Offset: 0x00587B30
    public static string Describe(NotchPresentation presentation)
    {
      string text;
      if (!(presentation is NotchPresentation.Idle))
      {
        if (!(presentation is NotchPresentation.TextInput))
        {
          NotchPresentation.TextResponse textResponse = presentation as NotchPresentation.TextResponse;
          if (textResponse != null)
          {
            if (!textResponse.IsThinking)
            {
              text = (string.IsNullOrWhiteSpace(textResponse.Answer) ? "HeyClicky answer" : ("HeyClicky: " + NotchAccessibility.Trim(textResponse.Answer)));
            }
            else
            {
              text = "HeyClicky is thinking";
            }
          }
          else if (!(presentation is NotchPresentation.OnboardingPinned))
          {
            NotchPresentation.LiveEvent liveEvent = presentation as NotchPresentation.LiveEvent;
            if (liveEvent != null)
            {
              NotchLiveEvent @event = liveEvent.Event;
              string text2;
              if (!(@event is NotchLiveEvent.ActivityListening))
              {
                if (!(@event is NotchLiveEvent.ActivityThinking))
                {
                  if (!(@event is NotchLiveEvent.ActivitySpeaking))
                  {
                    NotchLiveEvent.ActivityCustomStatus activityCustomStatus = @event as NotchLiveEvent.ActivityCustomStatus;
                    if (activityCustomStatus == null)
                    {
                      NotchLiveEvent.Dictation dictation = @event as NotchLiveEvent.Dictation;
                      if (dictation == null)
                      {
                        NotchLiveEvent.AgentTask agentTask = @event as NotchLiveEvent.AgentTask;
                        if (agentTask == null)
                        {
                          NotchLiveEvent.AgentMultiActivity agentMultiActivity = @event as NotchLiveEvent.AgentMultiActivity;
                          if (agentMultiActivity == null)
                          {
                            NotchLiveEvent.MicrophoneTrouble microphoneTrouble = @event as NotchLiveEvent.MicrophoneTrouble;
                            if (microphoneTrouble == null)
                            {
                              if (!(@event is NotchLiveEvent.VoiceReplyMutedCopiedToClipboard))
                              {
                                if (!(@event is NotchLiveEvent.DictationCopiedToClipboard))
                                {
                                  NotchLiveEvent.AppUpdated appUpdated = @event as NotchLiveEvent.AppUpdated;
                                  if (appUpdated == null)
                                  {
                                    NotchLiveEvent.DictionaryWordLearned dictionaryWordLearned = @event as NotchLiveEvent.DictionaryWordLearned;
                                    if (dictionaryWordLearned == null)
                                    {
                                      if (!(@event is NotchLiveEvent.RestingMark))
                                      {
                                        NotchLiveEvent.ClickyUpdate clickyUpdate = @event as NotchLiveEvent.ClickyUpdate;
                                        if (clickyUpdate == null)
                                        {
                                          if (!(@event is NotchLiveEvent.AgentPeek))
                                          {
                                            text2 = "HeyClicky";
                                          }
                                          else
                                          {
                                            text2 = "HeyClicky";
                                          }
                                        }
                                        else
                                        {
                                          text2 = ((clickyUpdate.Kind == "done") ? "A job is done" : ((clickyUpdate.Kind == "failed") ? "A job didn't finish" : "A job needs you"));
                                        }
                                      }
                                      else
                                      {
                                        text2 = "HeyClicky, working";
                                      }
                                    }
                                    else
                                    {
                                      text2 = "Added " + dictionaryWordLearned.Word + " to your dictionary";
                                    }
                                  }
                                  else
                                  {
                                    text2 = "HeyClicky updated. You're on version " + appUpdated.Version;
                                  }
                                }
                                else
                                {
                                  text2 = "Copied to your clipboard. Press Ctrl+V to paste";
                                }
                              }
                              else
                              {
                                text2 = "Your sound is off. The reply was copied to your clipboard";
                              }
                            }
                            else
                            {
                              text2 = microphoneTrouble.Message;
                            }
                          }
                          else
                          {
                            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 1);
                            defaultInterpolatedStringHandler.AppendFormatted<int>(agentMultiActivity.Count);
                            defaultInterpolatedStringHandler.AppendLiteral(" Clickys working");
                            text2 = defaultInterpolatedStringHandler.ToStringAndClear();
                          }
                        }
                        else
                        {
                          text2 = agentTask.Title + ": " + agentTask.Status;
                        }
                      }
                      else
                      {
                        text2 = dictation.Label;
                      }
                    }
                    else
                    {
                      text2 = activityCustomStatus.Label;
                    }
                  }
                  else
                  {
                    text2 = "Speaking";
                  }
                }
                else
                {
                  text2 = "Thinking";
                }
              }
              else
              {
                text2 = "Listening";
              }
              text = text2;
            }
            else
            {
              text = "HeyClicky";
            }
          }
          else
          {
            text = "HeyClicky setup";
          }
        }
        else
        {
          text = "Type to HeyClicky";
        }
      }
      else
      {
        text = "HeyClicky";
      }
      return text;
    }

    // Token: 0x060010BE RID: 4286 RVA: 0x00587DEC File Offset: 0x00587DEC
    private static string Trim(string text)
    {
      if (text.Length > 200)
      {
        return text.Substring(0, 200);
      }
      return text;
    }

    // Token: 0x060010BF RID: 4287 RVA: 0x00587E0C File Offset: 0x00587E0C
    public static void Announce(FrameworkElement root, NotchPresentation presentation)
    {
      string text = NotchAccessibility.Describe(presentation);
      if (text == NotchAccessibility.last)
      {
        return;
      }
      NotchAccessibility.last = text;
      AutomationProperties.SetName(root, text);
      AutomationProperties.SetLiveSetting(root, AutomationLiveSetting.Polite);
      try
      {
        if (AutomationPeer.ListenerExists(AutomationEvents.LiveRegionChanged))
        {
          AutomationPeer automationPeer = FrameworkElementAutomationPeer.FromElement(root) ?? FrameworkElementAutomationPeer.CreatePeerForElement(root);
          if (automationPeer != null)
          {
            automationPeer.RaiseAutomationEvent(AutomationEvents.LiveRegionChanged);
          }
        }
      }
      catch
      {
      }
    }

    // Token: 0x04000757 RID: 1879
    [Nullable(2)]
    private static string last;
  }
}
