using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.Core.Notch
{
  // Token: 0x02000113 RID: 275
  [NullableContext(1)]
  [Nullable(0)]
  public static class NotchChatAnalytics
  {
    // Token: 0x060011B0 RID: 4528 RVA: 0x0039D7D0 File Offset: 0x0039D7D0
    public static string RawValue(this NotchChatSessionOpenVia via)
    {
      string text;
      switch (via)
      {
      case NotchChatSessionOpenVia.DoubleControl:
        text = "double_control";
        break;
      case NotchChatSessionOpenVia.ComposerClick:
        text = "composer_click";
        break;
      case NotchChatSessionOpenVia.FileDrag:
        text = "file_drag";
        break;
      case NotchChatSessionOpenVia.FileDrop:
        text = "file_drop";
        break;
      default:
        text = "send";
        break;
      }
      return text;
    }

    // Token: 0x060011B1 RID: 4529 RVA: 0x0039D81C File Offset: 0x0039D81C
    public static string RawValue(this NotchChatSessionCloseReason reason)
    {
      string text;
      switch (reason)
      {
      case NotchChatSessionCloseReason.Escape:
        text = "escape";
        break;
      case NotchChatSessionCloseReason.DoubleControl:
        text = "double_control";
        break;
      case NotchChatSessionCloseReason.ClickElsewhere:
        text = "click_elsewhere";
        break;
      case NotchChatSessionCloseReason.DragAbandoned:
        text = "drag_abandoned";
        break;
      case NotchChatSessionCloseReason.LeftChatTab:
        text = "left_chat_tab";
        break;
      case NotchChatSessionCloseReason.NotchFolded:
        text = "notch_folded";
        break;
      case NotchChatSessionCloseReason.DrawingCaption:
        text = "drawing_caption";
        break;
      case NotchChatSessionCloseReason.ClickElsewhereDuringAnswer:
        text = "click_elsewhere_during_answer";
        break;
      default:
        text = "unspecified";
        break;
      }
      return text;
    }

    // Token: 0x060011B2 RID: 4530 RVA: 0x0039D898 File Offset: 0x0039D898
    public static string RawValue(this NotchChatFullReplyOpenSource source)
    {
      string text;
      if (source != NotchChatFullReplyOpenSource.ShowMore)
      {
        if (source != NotchChatFullReplyOpenSource.ArcCard)
        {
          text = "notch_card";
        }
        else
        {
          text = "arc_card";
        }
      }
      else
      {
        text = "show_more";
      }
      return text;
    }

    // Token: 0x060011B3 RID: 4531 RVA: 0x0039D8C8 File Offset: 0x0039D8C8
    public static string RawValue(this NotchChatFullReplyCloseVia via)
    {
      switch (via)
      {
      case NotchChatFullReplyCloseVia.Escape:
        return "escape";
      case NotchChatFullReplyCloseVia.CloseButton:
        return "close_button";
      case NotchChatFullReplyCloseVia.ExpandedToWindow:
        return "expanded_to_window";
      }
      return "follow_up_sent";
    }

    // Token: 0x060011B4 RID: 4532 RVA: 0x0039D90C File Offset: 0x0039D90C
    public static string RawValue(this ChatHandOffOutcome outcome)
    {
      string text;
      switch (outcome)
      {
      case ChatHandOffOutcome.Finished:
        text = "finished";
        break;
      case ChatHandOffOutcome.Failed:
        text = "failed";
        break;
      case ChatHandOffOutcome.Stopped:
        text = "stopped";
        break;
      default:
        text = "needsYou";
        break;
      }
      return text;
    }

    // Token: 0x060011B5 RID: 4533 RVA: 0x0039D94C File Offset: 0x0039D94C
    public static double Tenths(double seconds)
    {
      return Math.Round(seconds * 10.0, MidpointRounding.AwayFromZero) / 10.0;
    }

    // Token: 0x060011B6 RID: 4534 RVA: 0x0039D96C File Offset: 0x0039D96C
    public static JsonObject SessionOpened(NotchChatSessionOpenVia via, bool notchWasOpen, NotchTab tabBefore)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "session_opened";
      jsonObject["via"] = via.RawValue();
      jsonObject["notch_was_open"] = notchWasOpen;
      jsonObject["tab_before"] = tabBefore.RawValue();
      return jsonObject;
    }

    // Token: 0x060011B7 RID: 4535 RVA: 0x0039D9DC File Offset: 0x0039D9DC
    public static JsonObject SessionClosed(NotchChatSessionCloseReason reason, NotchChatSessionOpenVia openedVia, TimeSpan duration, int sentMessageCount, int sentAttachmentCount, bool hadUnsentDraft, bool answerInProgress)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "session_closed";
      jsonObject["reason"] = reason.RawValue();
      jsonObject["opened_via"] = openedVia.RawValue();
      jsonObject["duration_seconds"] = NotchChatAnalytics.Tenths(duration.TotalSeconds);
      jsonObject["sent_message_count"] = sentMessageCount;
      jsonObject["sent_attachment_count"] = sentAttachmentCount;
      jsonObject["had_unsent_draft"] = hadUnsentDraft;
      jsonObject["answer_in_progress"] = answerInProgress;
      return jsonObject;
    }

    // Token: 0x060011B8 RID: 4536 RVA: 0x0039DA9C File Offset: 0x0039DA9C
    public static JsonObject FullReplyOpened([Nullable(2)] ChatHandOff entry, NotchChatFullReplyOpenSource from)
    {
      string text = ChatReplyPreview.FullReplyMarkdown(entry);
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "full_reply_opened";
      jsonObject["from"] = from.RawValue();
      jsonObject["layout"] = (ChatReplyPreview.UsesWideLayout(text) ? "wide" : "narrow");
      ChatHandOffOutcome? chatHandOffOutcome;
      jsonObject["outcome"] = ((entry != null) ? ((entry.Outcome != null) ? chatHandOffOutcome.GetValueOrDefault().RawValue() : null) : null) ?? "none";
      jsonObject["is_job"] = ((entry != null) ? entry.JobId : null) != null;
      jsonObject["is_routine_run"] = entry != null && entry.IsRoutineRun.GetValueOrDefault();
      string text2 = "file_count";
      int? num;
      if (entry == null)
      {
        num = null;
      }
      else
      {
        IReadOnlyList<string> artifactEntries = entry.ArtifactEntries;
        num = ((artifactEntries != null) ? new int?(artifactEntries.Count) : null);
      }
      int? num2 = num;
      jsonObject[text2] = num2.GetValueOrDefault();
      string text3 = "follow_up_count";
      int? num3;
      if (entry == null)
      {
        num3 = null;
      }
      else
      {
        IReadOnlyList<string> followUps = entry.FollowUps;
        num3 = ((followUps != null) ? new int?(followUps.Count) : null);
      }
      num2 = num3;
      jsonObject[text3] = num2.GetValueOrDefault();
      jsonObject["reply_character_count"] = ((text == null) ? 0 : new StringInfo(text).LengthInTextElements);
      return jsonObject;
    }

    // Token: 0x060011B9 RID: 4537 RVA: 0x0039DC44 File Offset: 0x0039DC44
    public static JsonObject FullReplyClosed(NotchChatFullReplyCloseVia via, TimeSpan? openFor)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "full_reply_closed";
      jsonObject["via"] = via.RawValue();
      JsonObject jsonObject2 = jsonObject;
      if (openFor != null)
      {
        jsonObject2["duration_seconds"] = NotchChatAnalytics.Tenths(openFor.GetValueOrDefault().TotalSeconds);
      }
      return jsonObject2;
    }

    // Token: 0x060011BA RID: 4538 RVA: 0x0039DCBC File Offset: 0x0039DCBC
    public static JsonObject EarlierMessagesLoaded(string trigger, string source, int pageCount)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "earlier_messages_loaded";
      jsonObject["trigger"] = trigger;
      jsonObject["source"] = source;
      jsonObject["page_count"] = pageCount;
      return jsonObject;
    }

    // Token: 0x060011BB RID: 4539 RVA: 0x0039DD20 File Offset: 0x0039DD20
    public static JsonObject ReadAloudToggled(bool isOn, bool hasDraft)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "read_aloud_toggled";
      jsonObject["is_on"] = isOn;
      jsonObject["has_draft"] = hasDraft;
      return jsonObject;
    }

    // Token: 0x060011BC RID: 4540 RVA: 0x0039DD74 File Offset: 0x0039DD74
    public static JsonObject ChatSend(int attachmentCount, bool hasText, bool readAloudOn)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "chat_send";
      jsonObject["attachment_count"] = attachmentCount;
      jsonObject["has_attachments"] = attachmentCount > 0;
      jsonObject["has_text"] = hasText;
      jsonObject["read_aloud_on"] = readAloudOn;
      return jsonObject;
    }

    // Token: 0x060011BD RID: 4541 RVA: 0x0039DDEC File Offset: 0x0039DDEC
    public static JsonObject ChatArtifactOpened(string location, string artifact)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "chat_artifact_opened";
      jsonObject["location"] = location;
      jsonObject["artifact_kind"] = ClickyAnalytics.ProductArtifactKind(artifact);
      return jsonObject;
    }

    // Token: 0x060011BE RID: 4542 RVA: 0x0039DE44 File Offset: 0x0039DE44
    public static JsonObject ChatFollowUpSuggestion(ChatHandOff entry, string followUp, bool fromFullReply)
    {
      int num = -1;
      IReadOnlyList<string> followUps = entry.FollowUps;
      if (followUps != null)
      {
        for (int i = 0; i < followUps.Count; i++)
        {
          if (followUps[i] == followUp)
          {
            num = i;
            break;
          }
        }
      }
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "chat_follow_up_suggestion";
      jsonObject["is_job"] = entry.JobId != null;
      jsonObject["follow_up_index"] = num;
      jsonObject["from_full_reply"] = fromFullReply;
      return jsonObject;
    }
  }
}
