using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200030C RID: 780
  [NullableContext(1)]
  [Nullable(0)]
  public static class ArcDockAnalytics
  {
    // Token: 0x060021DB RID: 8667 RVA: 0x0061E85C File Offset: 0x0061E85C
    public static string StateName(ArcCardState state)
    {
      string text;
      switch (state)
      {
      case ArcCardState.Queued:
        text = "queued";
        break;
      case ArcCardState.Working:
        text = "working";
        break;
      case ArcCardState.NeedsYou:
        text = "needs_you";
        break;
      case ArcCardState.Failed:
        text = "failed";
        break;
      case ArcCardState.Stopped:
        text = "stopped";
        break;
      case ArcCardState.New:
        text = "new";
        break;
      case ArcCardState.Done:
        text = "done";
        break;
      case ArcCardState.Idle:
        text = "idle";
        break;
      case ArcCardState.Pinning:
        text = "pinning";
        break;
      default:
        text = "suggested";
        break;
      }
      return text;
    }

    // Token: 0x060021DC RID: 8668 RVA: 0x0061E8E4 File Offset: 0x0061E8E4
    public static JsonObject WindowProperties(ArcCardState state, bool isFinalizing, bool isJob)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["window_kind"] = ((state == ArcCardState.Suggested) ? "suggested" : (isJob ? "job" : "clicky"));
      jsonObject["state"] = (isFinalizing ? "finalizing" : ArcDockAnalytics.StateName(state));
      return jsonObject;
    }

    // Token: 0x060021DD RID: 8669 RVA: 0x0061E94C File Offset: 0x0061E94C
    public static string RequestKind(CardFooterRequest.Kinds kind)
    {
      string text;
      switch (kind)
      {
      case CardFooterRequest.Kinds.ComputerUse:
        text = "computer_use";
        break;
      case CardFooterRequest.Kinds.ExtraUsage:
        text = "extra_usage";
        break;
      case CardFooterRequest.Kinds.UserStep:
        text = "user_step";
        break;
      case CardFooterRequest.Kinds.ConnectChrome:
        text = "connect_chrome";
        break;
      case CardFooterRequest.Kinds.Failed:
        text = "failed";
        break;
      case CardFooterRequest.Kinds.Stopped:
        text = "stopped";
        break;
      default:
        text = "ready";
        break;
      }
      return text;
    }

    // Token: 0x060021DE RID: 8670 RVA: 0x0061E9B0 File Offset: 0x0061E9B0
    public static string OutputKind(CardReadyOutput.Kinds kind)
    {
      string text;
      switch (kind)
      {
      case CardReadyOutput.Kinds.File:
        text = "file";
        break;
      case CardReadyOutput.Kinds.Link:
        text = "link";
        break;
      case CardReadyOutput.Kinds.Several:
        text = "several";
        break;
      default:
        text = "nothing";
        break;
      }
      return text;
    }

    // Token: 0x060021DF RID: 8671 RVA: 0x0061E9F0 File Offset: 0x0061E9F0
    public static string Reason(string reason)
    {
      if (reason.StartsWith("notch tab → ", StringComparison.Ordinal))
      {
        string text = "notch_tab_";
        int num = "notch tab → ".Length;
        return text + reason.Substring(num, reason.Length - num);
      }
      if (reason != null)
      {
        int num = reason.Length;
        switch (num)
        {
        case 13:
          if (!(reason == "job dismissed"))
          {
            goto IL_03C1;
          }
          return "job_dismissed";
        case 14:
        case 15:
        case 17:
        case 20:
        case 23:
        case 24:
        case 27:
        case 29:
        case 30:
        case 32:
        case 33:
          goto IL_03C1;
        case 16:
          if (!(reason == "the notch's gear"))
          {
            goto IL_03C1;
          }
          return "notch_settings";
        case 18:
        {
          char c = reason[0];
          if (c != 'j')
          {
            if (c != 'n')
            {
              goto IL_03C1;
            }
            if (!(reason == "notch open on Home"))
            {
              goto IL_03C1;
            }
            return "notch_home";
          }
          else
          {
            if (!(reason == "job opened in Home"))
            {
              goto IL_03C1;
            }
            return "job_opened";
          }
          break;
        }
        case 19:
          if (!(reason == "pointer on a window"))
          {
            goto IL_03C1;
          }
          break;
        case 21:
        {
          char c = reason[5];
          if (c != 'd')
          {
            if (c != 'e')
            {
              if (c != 'o')
              {
                goto IL_03C1;
              }
              if (!(reason == "the notch folded away"))
              {
                goto IL_03C1;
              }
              return "notch_folded";
            }
            else if (!(reason == "the deck turned to it"))
            {
              goto IL_03C1;
            }
          }
          else
          {
            if (!(reason == "a window's red button"))
            {
              goto IL_03C1;
            }
            goto IL_0371;
          }
          break;
        }
        case 22:
          if (!(reason == "a window's file opened"))
          {
            goto IL_03C1;
          }
          return "file_opened";
        case 25:
          if (!(reason == "Show me on a finished job"))
          {
            goto IL_03C1;
          }
          return "show_me";
        case 26:
          if (!(reason == "panel hidden: dock stopped"))
          {
            goto IL_03C1;
          }
          return "panel_hidden_dock_stopped";
        case 28:
          if (!(reason == "a pinned window's red button"))
          {
            goto IL_03C1;
          }
          goto IL_0371;
        case 31:
        {
          char c = reason[0];
          if (c != 'a')
          {
            if (c != 'p')
            {
              if (c != 't')
              {
                goto IL_03C1;
              }
              if (!(reason == "the window beside a removed one"))
              {
                goto IL_03C1;
              }
            }
            else
            {
              if (!(reason == "panel hidden: dock switched off"))
              {
                goto IL_03C1;
              }
              return "panel_hidden_dock_off";
            }
          }
          else
          {
            if (!(reason == "a window came forward by itself"))
            {
              goto IL_03C1;
            }
            return "auto_presented";
          }
          break;
        }
        case 34:
        {
          char c = reason[4];
          if (c != 'd')
          {
            if (c != 't')
            {
              if (c != 'w')
              {
                goto IL_03C1;
              }
              if (!(reason == "the welcome tour showing the notch"))
              {
                goto IL_03C1;
              }
              return "tour_showcase";
            }
            else
            {
              if (!(reason == "pointer left the arc and the notch"))
              {
                goto IL_03C1;
              }
              return "pointer_left";
            }
          }
          else
          {
            if (!(reason == "the dock's last window was removed"))
            {
              goto IL_03C1;
            }
            return "last_window_removed";
          }
          break;
        }
        case 35:
          if (!(reason == "the notch opened on another display"))
          {
            goto IL_03C1;
          }
          return "moved_to_other_display";
        case 36:
          if (!(reason == "pointer at the top edge while tucked"))
          {
            goto IL_03C1;
          }
          return "top_edge_while_tucked";
        default:
          if (num != 44)
          {
            if (num != 59)
            {
              goto IL_03C1;
            }
            if (!(reason == "panel hidden: dock not shown (setup not done or signed out)"))
            {
              goto IL_03C1;
            }
            return "panel_hidden_dock_not_shown";
          }
          else
          {
            if (!(reason == "panel hidden: HeyClicky's visuals suppressed"))
            {
              goto IL_03C1;
            }
            return "panel_hidden_visuals_suppressed";
          }
          break;
        }
        return "window";
        IL_0371:
        return "red_button";
      }
      IL_03C1:
      return reason.StartsWith("panel hidden: ", StringComparison.Ordinal) ? "panel_hidden" : "other";
    }

    // Token: 0x060021E0 RID: 8672 RVA: 0x0061EDDC File Offset: 0x0061EDDC
    [return: Nullable(2)]
    public static JsonObject Closed(string reason, string openedReason, int windowCount, double openSeconds)
    {
      if (openSeconds < 3.0)
      {
        return null;
      }
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "closed";
      jsonObject["reason"] = ArcDockAnalytics.Reason(reason);
      jsonObject["opened_reason"] = ArcDockAnalytics.Reason(openedReason);
      jsonObject["window_count"] = windowCount;
      jsonObject["open_seconds"] = Math.Round(openSeconds * 10.0, MidpointRounding.AwayFromZero) / 10.0;
      return jsonObject;
    }

    // Token: 0x060021E1 RID: 8673 RVA: 0x0061EE84 File Offset: 0x0061EE84
    public static JsonObject DeckTurned(string via, int previous, int next, int windowCount, string windowKind)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "deck_turned";
      jsonObject["via"] = via;
      jsonObject["direction"] = ((next < previous) ? "left" : "right");
      jsonObject["steps"] = Math.Abs(next - previous);
      jsonObject["window_count"] = windowCount;
      jsonObject["window_kind"] = windowKind;
      return jsonObject;
    }

    // Token: 0x060021E2 RID: 8674 RVA: 0x0061EF20 File Offset: 0x0061EF20
    public static JsonObject RequestAnswered(string answer, string surface, bool isJob, bool hasLiveTask)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["answer"] = answer;
      jsonObject["surface"] = surface;
      jsonObject["is_job"] = isJob;
      JsonObject jsonObject2 = jsonObject;
      if (!hasLiveTask)
      {
        jsonObject2["has_live_task"] = false;
      }
      return jsonObject2;
    }

    // Token: 0x04001056 RID: 4182
    public const double MinimumTrackedOpenSeconds = 3.0;
  }
}
