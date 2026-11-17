using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.Core.Drive
{
  // Token: 0x02000196 RID: 406
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyDriveAnalytics
  {
    // Token: 0x06001800 RID: 6144 RVA: 0x003C6EE0 File Offset: 0x003C6EE0
    [NullableContext(2)]
    public static void Track([Nullable(1)] string action, string threadId = null, JsonObject properties = null)
    {
      if (properties == null)
      {
        properties = new JsonObject(null);
      }
      properties["action"] = action;
      ClickyAnalytics.TrackProductEvent("clicky_drive_action", null, threadId, properties);
    }

    // Token: 0x06001801 RID: 6145 RVA: 0x003C6F20 File Offset: 0x003C6F20
    public static string Billing(ClickyDriveBilling billing)
    {
      string text;
      if (!(billing is ClickyDriveBilling.Job))
      {
        if (!(billing is ClickyDriveBilling.Setup))
        {
          text = "user";
        }
        else
        {
          text = "setup";
        }
      }
      else
      {
        text = "job";
      }
      return text;
    }

    // Token: 0x06001802 RID: 6146 RVA: 0x003C6F58 File Offset: 0x003C6F58
    public static string Outcome(AgentDriveEnding ending)
    {
      string text;
      switch (ending)
      {
      case AgentDriveEnding.Finished:
        text = "finished";
        break;
      case AgentDriveEnding.UserTookOver:
        text = "user_took_over";
        break;
      case AgentDriveEnding.SafetyStop:
        text = "safety_check";
        break;
      case AgentDriveEnding.CouldNotSee:
        text = "could_not_see";
        break;
      case AgentDriveEnding.OutOfAgentMessages:
        text = "out_of_agent_messages";
        break;
      default:
        text = "failed";
        break;
      }
      return text;
    }

    // Token: 0x06001803 RID: 6147 RVA: 0x003C6FB0 File Offset: 0x003C6FB0
    public static JsonObject StartedProperties(string driveId, string origin, ClickyDriveBilling billing, bool isJobStep, bool hasThen)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["drive_id"] = driveId;
      jsonObject["origin"] = origin;
      jsonObject["billing"] = ClickyDriveAnalytics.Billing(billing);
      jsonObject["is_job_step"] = isJobStep;
      jsonObject["has_then"] = hasThen;
      return jsonObject;
    }

    // Token: 0x06001804 RID: 6148 RVA: 0x003C7028 File Offset: 0x003C7028
    public static JsonObject EndedProperties(string driveId, string origin, ClickyDriveBilling billing, AgentDriveEnding ending, double durationSeconds, int actionCount, int stepCount, bool brainCalledEnd, bool isJobStep, bool earlyRefundRequested, [Nullable(2)] string takenBackBy, int mousePauses = 0)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["drive_id"] = driveId;
      jsonObject["origin"] = origin;
      jsonObject["billing"] = ClickyDriveAnalytics.Billing(billing);
      jsonObject["outcome"] = ClickyDriveAnalytics.Outcome(ending);
      jsonObject["duration_seconds"] = (int)Math.Round(durationSeconds, MidpointRounding.AwayFromZero);
      jsonObject["action_count"] = actionCount;
      jsonObject["step_count"] = stepCount;
      jsonObject["brain_called_end"] = brainCalledEnd;
      jsonObject["is_job_step"] = isJobStep;
      jsonObject["early_refund_requested"] = earlyRefundRequested;
      jsonObject["mouse_pauses"] = mousePauses;
      JsonObject jsonObject2 = jsonObject;
      if (ending == AgentDriveEnding.UserTookOver)
      {
        jsonObject2["taken_back_by"] = takenBackBy ?? "unknown";
      }
      return jsonObject2;
    }

    // Token: 0x06001805 RID: 6149 RVA: 0x003C7138 File Offset: 0x003C7138
    public static void TrackMacStepRequested([Nullable(2)] string threadId, string requestKind, bool driveAvailable)
    {
      string text = "mac_step_requested";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["request_kind"] = requestKind;
      jsonObject["drive_available"] = driveAvailable;
      ClickyDriveAnalytics.Track(text, threadId, jsonObject);
    }

    // Token: 0x06001806 RID: 6150 RVA: 0x003C7180 File Offset: 0x003C7180
    public static void TrackMacStepAnswered([Nullable(2)] string threadId, string decision, string path)
    {
      string text = "mac_step_answered";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["decision"] = decision;
      jsonObject["path"] = path;
      ClickyDriveAnalytics.Track(text, threadId, jsonObject);
    }

    // Token: 0x06001807 RID: 6151 RVA: 0x003C71C8 File Offset: 0x003C71C8
    public static void TrackMacStepResultDelivered([Nullable(2)] string threadId, string via)
    {
      string text = "mac_step_result_delivered";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["delivered_via"] = via;
      ClickyDriveAnalytics.Track(text, threadId, jsonObject);
    }

    // Token: 0x06001808 RID: 6152 RVA: 0x003C7200 File Offset: 0x003C7200
    public static void TrackMacStepCallReturned(string threadId, string status, bool joinedWaitingCall = false)
    {
      string text = "mac_step_call_returned";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["status"] = status;
      jsonObject["joined_waiting_call"] = joinedWaitingCall;
      ClickyDriveAnalytics.Track(text, threadId, jsonObject);
    }

    // Token: 0x06001809 RID: 6153 RVA: 0x003C7248 File Offset: 0x003C7248
    public static void TrackMacStepCallReturned(string threadId, MacStepAnswer answer, bool joinedWaitingCall)
    {
      ClickyDriveAnalytics.TrackMacStepCallReturned(threadId, ClickyDriveAnalytics.CallStatus(answer), joinedWaitingCall);
    }

    // Token: 0x0600180A RID: 6154 RVA: 0x003C7258 File Offset: 0x003C7258
    public static string CallStatus(MacStepAnswer answer)
    {
      string text;
      if (!(answer is MacStepAnswer.Drove))
      {
        if (!(answer is MacStepAnswer.Declined))
        {
          if (!(answer is MacStepAnswer.NoAnswer))
          {
            if (!(answer is MacStepAnswer.NotYet))
            {
              if (!(answer is MacStepAnswer.BeingDriven))
              {
                text = "could_not_start";
              }
              else
              {
                text = "in_progress";
              }
            }
            else
            {
              text = "waiting";
            }
          }
          else
          {
            text = "no_answer";
          }
        }
        else
        {
          text = "declined";
        }
      }
      else
      {
        text = "done";
      }
      return text;
    }

    // Token: 0x0600180B RID: 6155 RVA: 0x003C72C0 File Offset: 0x003C72C0
    public static void TrackOnboardingDriveStarted(bool isRepeat)
    {
      string text = "onboarding_drive_step_action";
      string text2 = null;
      string text3 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "started";
      jsonObject["is_repeat"] = isRepeat;
      ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
    }

    // Token: 0x0600180C RID: 6156 RVA: 0x003C7310 File Offset: 0x003C7310
    public static void TrackOnboardingDriveEnded(string outcome, double durationSeconds)
    {
      string text = "onboarding_drive_step_action";
      string text2 = null;
      string text3 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "ended";
      jsonObject["outcome"] = outcome;
      jsonObject["duration_seconds"] = (int)Math.Round(durationSeconds, MidpointRounding.AwayFromZero);
      ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
    }

    // Token: 0x0200057F RID: 1407
    [Nullable(0)]
    public static class Origin
    {
      // Token: 0x0400209B RID: 8347
      public const string OfferCard = "offer_card";

      // Token: 0x0400209C RID: 8348
      public const string OfferSpoken = "offer_spoken";

      // Token: 0x0400209D RID: 8349
      public const string RecentYes = "recent_yes";

      // Token: 0x0400209E RID: 8350
      public const string JobStepSpoken = "job_step_spoken";

      // Token: 0x0400209F RID: 8351
      public const string JobStepApproved = "job_step_approved";

      // Token: 0x040020A0 RID: 8352
      public const string AppSetup = "app_setup";
    }
  }
}
