using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Drive
{
  // Token: 0x02000190 RID: 400
  [NullableContext(1)]
  [Nullable(0)]
  public static class AgentDriveReports
  {
    // Token: 0x060017B0 RID: 6064 RVA: 0x003C504C File Offset: 0x003C504C
    public static string Report(AgentDriveOutcome outcome, string request)
    {
      string text;
      switch (outcome.Ending)
      {
      case AgentDriveEnding.Finished:
        text = ((outcome.Actions.Count > 0) ? "It finished." : (AgentDriveReports.HasSummary(outcome) ? "It looked at the screen and didn't need to click or type anything. Trust what it found below; don't ask for this same step again unless that says something is still left to do on the PC." : "It ended without acting on the screen, so this step may not be done."));
        goto IL_007B;
      case AgentDriveEnding.UserTookOver:
        text = "The user took the screen back partway through, so this step may not be finished.";
        goto IL_007B;
      case AgentDriveEnding.SafetyStop:
        text = "It stopped for a safety check, so the user has to decide that part themselves.";
        goto IL_007B;
      case AgentDriveEnding.CouldNotSee:
        text = "It couldn't see the screen, so nothing was done.";
        goto IL_007B;
      case AgentDriveEnding.Failed:
        text = "It lost its connection partway, so this step may not be finished.";
        goto IL_007B;
      }
      text = "It stopped partway: the user is out of agent messages for now, so this step may not be finished.";
      IL_007B:
      string text2 = text;
      if (AgentDriveReports.HasSummary(outcome))
      {
        text2 = text2 + " Its summary: " + outcome.Summary.Trim();
      }
      string text3 = string.Join("; ", outcome.Actions.TakeLast<string>(40));
      string text4 = ((text3.Length == 0) ? "" : (" What it did on screen: " + text3 + "."));
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(65, 4);
      defaultInterpolatedStringHandler.AppendFormatted("[Clicky drove your screen for this step]");
      defaultInterpolatedStringHandler.AppendLiteral(" HeyClicky drove the user's screen for the step you asked for: ");
      defaultInterpolatedStringHandler.AppendFormatted(request);
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendFormatted(text4);
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      return defaultInterpolatedStringHandler.ToStringAndClear() + "Carry on with the rest of the task from here. Check the result yourself where you can (open the file, read the page) instead of assuming it worked, don't redo a step that worked, and if something is still left on the PC, ask again the same way.";
    }

    // Token: 0x060017B1 RID: 6065 RVA: 0x003C5190 File Offset: 0x003C5190
    private static bool HasSummary(AgentDriveOutcome outcome)
    {
      string summary = outcome.Summary;
      string text = ((summary != null) ? summary.Trim() : null);
      return text != null && text.Length > 0 && text != "done";
    }

    // Token: 0x060017B2 RID: 6066 RVA: 0x003C51CC File Offset: 0x003C51CC
    public static string Unavailable(string request)
    {
      return "[Clicky drove your screen for this step] HeyClicky can't drive the screen right now, so nothing was done on the PC for: " + request + " Finish what you can without it, and tell me exactly what to click or type for that step so I can do it myself.";
    }

    // Token: 0x060017B3 RID: 6067 RVA: 0x003C51E0 File Offset: 0x003C51E0
    public static string Busy(string request)
    {
      return "[Clicky drove your screen for this step] HeyClicky couldn't drive the screen just now (it was already busy driving). Nothing was done on the PC for: " + request + " Finish what you can without it and tell me which step is left for me.";
    }

    // Token: 0x060017B4 RID: 6068 RVA: 0x003C51F4 File Offset: 0x003C51F4
    public static JsonObject MacStepDeclinedBefore()
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["status"] = "declined";
      jsonObject["message"] = "The user already said not now to this step. Don't ask for it again unless they tell you to go ahead with it: finish what you can without it, and in your reply say exactly what's left for them to do there (the clicks, in a short numbered list).";
      return jsonObject;
    }

    // Token: 0x060017B5 RID: 6069 RVA: 0x003C523C File Offset: 0x003C523C
    [return: Nullable(2)]
    public static string MacStepCardLine(string ask, string why)
    {
      string text = ask.Trim(new char[] { ' ', '.' });
      if (text.Length == 0)
      {
        return null;
      }
      string text2 = why.Trim(new char[] { ' ', '.' });
      string text3 = ((text2.Length == 0) ? text : (text + ", " + text2));
      char c = char.ToUpperInvariant(text3[0]);
      ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c);
      string text4 = text3;
      return readOnlySpan + text4.Substring(1, text4.Length - 1) + ".";
    }

    // Token: 0x060017B6 RID: 6070 RVA: 0x003C52D8 File Offset: 0x003C52D8
    public static string ShownRequest(string step, string why)
    {
      string text = why.Trim(new char[] { ' ', '.' });
      if (text.Length != 0)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
        defaultInterpolatedStringHandler.AppendFormatted<char>(char.ToUpperInvariant(text[0]));
        string text2 = text;
        defaultInterpolatedStringHandler.AppendFormatted(text2.Substring(1, text2.Length - 1));
        defaultInterpolatedStringHandler.AppendLiteral(": ");
        defaultInterpolatedStringHandler.AppendFormatted(step);
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
      return step;
    }

    // Token: 0x060017B7 RID: 6071 RVA: 0x003C5358 File Offset: 0x003C5358
    public static JsonObject MacStepBusy()
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["status"] = "busy";
      jsonObject["message"] = "A step on the PC is already waiting for the user's answer. Wait for it before asking for another.";
      return jsonObject;
    }

    // Token: 0x060017B8 RID: 6072 RVA: 0x003C53A0 File Offset: 0x003C53A0
    public static JsonObject MacStepUnavailable()
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["status"] = "unavailable";
      jsonObject["message"] = "HeyClicky can't do steps on the screen right now. Finish what you can, and in your reply tell the user exactly what to click or type for this step, as a short numbered list.";
      return jsonObject;
    }

    // Token: 0x060017B9 RID: 6073 RVA: 0x003C53E8 File Offset: 0x003C53E8
    public static JsonObject MacStepChoosingChrome()
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["status"] = "unavailable";
      jsonObject["message"] = "Not now: they're choosing whether to connect Chrome for this job. Don't do the website part another way. End your turn; you'll be told to carry on once they've chosen.";
      return jsonObject;
    }

    // Token: 0x060017BA RID: 6074 RVA: 0x003C5430 File Offset: 0x003C5430
    public static JsonObject MacStepChromeAskedAtTurnEnd()
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["status"] = "unavailable";
      jsonObject["message"] = "Not yet: Chrome isn't connected, and HeyClicky asks them about it once your turn ends. Don't do a website step another way. Finish everything else, then end your turn with a line saying the rest needs Chrome connected (name this step, even if it isn't on a website); you'll be told to carry on once they've chosen.";
      return jsonObject;
    }

    // Token: 0x060017BB RID: 6075 RVA: 0x003C5478 File Offset: 0x003C5478
    public static JsonObject MacStepToolResult(MacStepAnswer answer, string step)
    {
      MacStepAnswer.Drove drove = answer as MacStepAnswer.Drove;
      JsonObject jsonObject2;
      if (drove == null)
      {
        if (!(answer is MacStepAnswer.Declined))
        {
          if (!(answer is MacStepAnswer.NoAnswer))
          {
            if (!(answer is MacStepAnswer.NotYet))
            {
              if (!(answer is MacStepAnswer.BeingDriven))
              {
                JsonObject jsonObject = new JsonObject(null);
                jsonObject["status"] = "busy";
                jsonObject["message"] = "HeyClicky was already driving the screen, so nothing was done for this step. Finish what you can and say what's left.";
                jsonObject2 = jsonObject;
              }
              else
              {
                JsonObject jsonObject3 = new JsonObject(null);
                jsonObject3["status"] = "in_progress";
                jsonObject3["message"] = "HeyClicky is doing this step on their screen right now. End your turn now with one line saying you're waiting for it (no request block). When it's done, HeyClicky continues this conversation with what happened as your next message.";
                jsonObject2 = jsonObject3;
              }
            }
            else
            {
              JsonObject jsonObject4 = new JsonObject(null);
              jsonObject4["status"] = "waiting";
              jsonObject4["message"] = "The user hasn't answered yet, and the request stays on your card. End your turn now: say in one line that you're waiting for this step on their PC, and end your final message with <COMPUTER_USE_REQUEST>" + step + "</COMPUTER_USE_REQUEST>. When it's done, HeyClicky continues this conversation with what happened as your next message. Don't ask for it again.";
              jsonObject2 = jsonObject4;
            }
          }
          else
          {
            JsonObject jsonObject5 = new JsonObject(null);
            jsonObject5["status"] = "no_answer";
            jsonObject5["message"] = "The user didn't answer. Finish what you can without this step, and say in your reply exactly what's left for them to do there.";
            jsonObject2 = jsonObject5;
          }
        }
        else
        {
          JsonObject jsonObject6 = new JsonObject(null);
          jsonObject6["status"] = "declined";
          jsonObject6["message"] = "The user said not now. Finish what you can without this step, and say in your reply exactly what's left for them to do there (the clicks, in a short numbered list). Don't ask for this step again unless they tell you to go ahead with it.";
          jsonObject2 = jsonObject6;
        }
      }
      else
      {
        JsonObject jsonObject7 = new JsonObject(null);
        jsonObject7["status"] = "done";
        jsonObject7["result"] = drove.Report;
        jsonObject2 = jsonObject7;
      }
      return jsonObject2;
    }

    // Token: 0x060017BC RID: 6076 RVA: 0x003C5634 File Offset: 0x003C5634
    public static bool DriveGoalNamesJob(string goal, string threadId, string title)
    {
      string text = title.Trim();
      return goal.Contains(threadId, StringComparison.Ordinal) || (text.Length >= 4 && CultureInfo.InvariantCulture.CompareInfo.IndexOf(goal, text, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0);
    }

    // Token: 0x060017BD RID: 6077 RVA: 0x003C5678 File Offset: 0x003C5678
    public static bool DriveGoalSharesWordsWithStep(string goal, string step)
    {
      return AgentDriveReports.<DriveGoalSharesWordsWithStep>g__Words|17_0(goal).Intersect<string>(AgentDriveReports.<DriveGoalSharesWordsWithStep>g__Words|17_0(step)).Count<string>() >= 2;
    }

    // Token: 0x060017BF RID: 6079 RVA: 0x003C57BC File Offset: 0x003C57BC
    [CompilerGenerated]
    internal static HashSet<string> <DriveGoalSharesWordsWithStep>g__Words|17_0(string text)
    {
      HashSet<string> hashSet = new HashSet<string>();
      StringBuilder stringBuilder = new StringBuilder();
      foreach (char c in text.ToLowerInvariant() + " ")
      {
        if (char.IsLetter(c))
        {
          stringBuilder.Append(c);
        }
        else
        {
          if (stringBuilder.Length >= 4 && !AgentDriveReports.CommonWords.Contains(stringBuilder.ToString()))
          {
            hashSet.Add(stringBuilder.ToString());
          }
          stringBuilder.Clear();
        }
      }
      return hashSet;
    }

    // Token: 0x04000A38 RID: 2616
    public const string Declined = "Not now. Finish without controlling my PC, and tell me what you did get done. Don't ask to do this step on my PC again unless I tell you to go ahead with it.";

    // Token: 0x04000A39 RID: 2617
    public const string DeclinedCursorLine = "Okay, I'll leave that step to you. The job finishes without it.";

    // Token: 0x04000A3A RID: 2618
    public static readonly TimeSpan MacStepOpenCall = TimeSpan.FromSeconds(240L);

    // Token: 0x04000A3B RID: 2619
    private static readonly HashSet<string> CommonWords = new HashSet<string>
    {
      "this", "that", "with", "from", "into", "your", "their", "then", "when", "save",
      "file", "open", "the", "and", "for", "step", "mac", "screen", "folder", "user",
      "users"
    };
  }
}
