using System;
using System.Runtime.CompilerServices;
using HeyClicky.Agents.Jobs;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000311 RID: 785
  public static class JobCardText
  {
    // Token: 0x06002229 RID: 8745 RVA: 0x006203B0 File Offset: 0x006203B0
    [NullableContext(2)]
    [return: Nullable(1)]
    public static string BodyText([Nullable(1)] ClickyJob job, ClickyJobRailPhase phase, bool hasTask, string latestCommentary, string thinkingHeading, string pendingComputerUse, string taskFailureSummary)
    {
      switch (phase)
      {
      case ClickyJobRailPhase.Queued:
        return "Getting started…";
      case ClickyJobRailPhase.Working:
      {
        if (!hasTask)
        {
          return "Working on it";
        }
        string text = ((latestCommentary == null) ? null : ClickyWorkReplyParts.FirstSentence(latestCommentary, 160));
        string text2 = ((thinkingHeading != null) ? thinkingHeading.Trim() : null);
        if (text != null && text.Length > 0)
        {
          return text;
        }
        if (text2 == null || text2.Length <= 0)
        {
          return "Working on it";
        }
        return text2;
      }
      case ClickyJobRailPhase.NeedsYou:
      {
        string text3 = ((pendingComputerUse != null) ? pendingComputerUse.Trim() : null);
        if (text3 != null && text3.Length > 0)
        {
          return "One step on your PC: " + text3;
        }
        return "Needs more agent messages to keep going.";
      }
      case ClickyJobRailPhase.Done:
      {
        string summary = job.Summary;
        string text4 = ((summary != null) ? summary.Trim() : null);
        string text5 = ((text4 != null && text4.Length > 0) ? text4 : null);
        string replyText = job.ReplyText;
        string text7;
        if (replyText != null)
        {
          string text6 = ClickyWorkReplyParts.FirstSentence(replyText, 420);
          if (text6 != null && text6.Length > 0)
          {
            text7 = text6;
            goto IL_00F6;
          }
        }
        text7 = null;
        IL_00F6:
        string text8 = text7;
        string text9;
        if ((text9 = text5) == null)
        {
          text9 = text8 ?? "Done.";
        }
        return text9;
      }
      case ClickyJobRailPhase.Failed:
      {
        string summary2 = job.Summary;
        if (summary2 != null && summary2.Length > 0)
        {
          return summary2;
        }
        if (taskFailureSummary != null && taskFailureSummary.Length > 0)
        {
          return taskFailureSummary;
        }
        return "Something went wrong.";
      }
      default:
      {
        string summary3 = job.Summary;
        if (summary3 == null || summary3.Length <= 0)
        {
          return "Stopped before it finished.";
        }
        return summary3;
      }
      }
    }
  }
}
