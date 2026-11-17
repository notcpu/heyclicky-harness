using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Drive;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.App.Drive
{
  // Token: 0x020002BE RID: 702
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentDriveConsent
  {
    // Token: 0x06001D3B RID: 7483 RVA: 0x005FDA18 File Offset: 0x005FDA18
    public AgentDriveConsent(ClickyTakeOverController drive, IAgentDriveJobs jobs, [Nullable(2)] MacStepWaiters waiters = null)
    {
      this.drive = drive;
      this.jobs = jobs;
      this.waiters = waiters ?? MacStepWaiters.Shared;
      drive.MacStepBindingForBrainDrive = new Func<string, AgentDriveBinding>(this.MacStepBindingForBrainDrive);
    }

    // Token: 0x1700052E RID: 1326
    // (get) Token: 0x06001D3C RID: 7484 RVA: 0x005FDA50 File Offset: 0x005FDA50
    // (set) Token: 0x06001D3D RID: 7485 RVA: 0x005FDA58 File Offset: 0x005FDA58
    [Nullable(new byte[] { 2, 1 })]
    public Action<string> ShowCursorLine
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x1700052F RID: 1327
    // (get) Token: 0x06001D3E RID: 7486 RVA: 0x005FDA64 File Offset: 0x005FDA64
    public bool AgentClickyDriveIsAvailable
    {
      get
      {
        return this.drive.IsAvailable();
      }
    }

    // Token: 0x06001D3F RID: 7487 RVA: 0x005FDA78 File Offset: 0x005FDA78
    public void AnnounceAgentAsksToUsePcIfNeeded(IAgentDriveJob job, string requestKind = "end_of_turn")
    {
      string pendingComputerUseRequest = job.PendingComputerUseRequest;
      if (pendingComputerUseRequest == null || job.DidAnnounceComputerUseRequest)
      {
        return;
      }
      job.DidAnnounceComputerUseRequest = true;
      ClickyDriveAnalytics.TrackMacStepRequested(job.ThreadId, requestKind, this.AgentClickyDriveIsAvailable);
      this.jobs.NoteEventForBrain(job, "is waiting for a yes to let HeyClicky drive their PC for one step", pendingComputerUseRequest);
      this.drive.PlayChime("agent-needs-you", 0.8);
      this.jobs.AnnounceVerbatim(ClickyDriveLines.AsksToUsePc((job.Name == "Your job") ? null : job.Name, job.PendingComputerUseSummary ?? pendingComputerUseRequest));
    }

    // Token: 0x06001D40 RID: 7488 RVA: 0x005FDB18 File Offset: 0x005FDB18
    public void ApproveComputerUse(string taskId)
    {
      this.LetClickyDriveForAgent(taskId);
    }

    // Token: 0x06001D41 RID: 7489 RVA: 0x005FDB24 File Offset: 0x005FDB24
    public void LetClickyDriveForAgent(string taskId)
    {
      AgentDriveConsent.<>c__DisplayClass14_0 CS$<>8__locals1 = new AgentDriveConsent.<>c__DisplayClass14_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.taskId = taskId;
      IAgentDriveJob agentDriveJob = this.jobs.Find(CS$<>8__locals1.taskId);
      if (agentDriveJob != null && this.DriveWaitingMacStep(agentDriveJob))
      {
        return;
      }
      IAgentDriveJob agentDriveJob2 = this.jobs.Find(CS$<>8__locals1.taskId);
      if (agentDriveJob2 != null)
      {
        CS$<>8__locals1.threadId = agentDriveJob2.ThreadId;
        if (CS$<>8__locals1.threadId != null)
        {
          CS$<>8__locals1.request = agentDriveJob2.PendingComputerUseRequest;
          if (CS$<>8__locals1.request != null)
          {
            CS$<>8__locals1.askingLease = agentDriveJob2.AgentTurnLeaseId;
            string name = agentDriveJob2.Name;
            agentDriveJob2.PendingComputerUseRequest = null;
            ClickyAnalytics.TrackComputerUseConsent(true, false);
            ClickyDriveAnalytics.TrackMacStepAnswered(CS$<>8__locals1.threadId, "approve", "end_of_turn");
            this.waiters.ForgetUnanswered(CS$<>8__locals1.taskId);
            if (!this.AgentClickyDriveIsAvailable)
            {
              Log.Info("drive", "clicky drive unavailable for thread=" + CS$<>8__locals1.threadId);
              string text = "start_refused";
              string threadId = CS$<>8__locals1.threadId;
              JsonObject jsonObject = new JsonObject(null);
              jsonObject["reason"] = "drive_unavailable";
              jsonObject["origin"] = "job_step_approved";
              ClickyDriveAnalytics.Track(text, threadId, jsonObject);
              CS$<>8__locals1.<LetClickyDriveForAgent>g__Report|0(AgentDriveReports.Unavailable(CS$<>8__locals1.request));
              return;
            }
            bool flag = this.drive.DriveForAgent(CS$<>8__locals1.request, name, CS$<>8__locals1.askingLease, delegate(AgentDriveOutcome outcome)
            {
              string text3 = "drive";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(39, 2);
              defaultInterpolatedStringHandler2.AppendLiteral("clicky drive for thread=");
              defaultInterpolatedStringHandler2.AppendFormatted(CS$<>8__locals1.threadId);
              defaultInterpolatedStringHandler2.AppendLiteral(" ended actions=");
              defaultInterpolatedStringHandler2.AppendFormatted<int>(outcome.Actions.Count);
              Log.Info(text3, defaultInterpolatedStringHandler2.ToStringAndClear());
              base.<LetClickyDriveForAgent>g__Report|0(AgentDriveReports.Report(outcome, CS$<>8__locals1.request));
            }, CS$<>8__locals1.threadId);
            string text2 = "drive";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 2);
            defaultInterpolatedStringHandler.AppendLiteral("clicky drive for thread=");
            defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.threadId);
            defaultInterpolatedStringHandler.AppendLiteral(" started=");
            defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
            Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
            if (!flag)
            {
              CS$<>8__locals1.<LetClickyDriveForAgent>g__Report|0(AgentDriveReports.Busy(CS$<>8__locals1.request));
            }
            return;
          }
        }
      }
    }

    // Token: 0x06001D42 RID: 7490 RVA: 0x005FDCF0 File Offset: 0x005FDCF0
    public void DeliverMacStepOutcome(string taskId, string threadId, [Nullable(2)] string askingTurnLeaseId, string report)
    {
      this.waiters.ForgetUnanswered(taskId);
      if (this.waiters.IsWaiting(taskId))
      {
        ClickyDriveAnalytics.TrackMacStepResultDelivered(threadId, "open_call");
        this.waiters.Resolve(taskId, new MacStepAnswer.Drove(report));
        return;
      }
      this.waiters.EndDrive(taskId);
      IAgentDriveJob agentDriveJob = this.jobs.Find(taskId);
      if (agentDriveJob != null)
      {
        agentDriveJob.PendingComputerUseRequest = null;
      }
      Log.Info("drive", "step result for thread=" + threadId + " delivered as a message");
      if (this.jobs.IsThreadActive(threadId))
      {
        ClickyDriveAnalytics.TrackMacStepResultDelivered(threadId, "steer");
        this.jobs.SubmitFollowUp(threadId, report, null, "HeyClicky did the step on their PC you asked for and is passing on what happened (the user didn't type this message).");
        return;
      }
      ClickyDriveAnalytics.TrackMacStepResultDelivered(threadId, "revive");
      this.ContinueAskingTurnAsync(threadId, askingTurnLeaseId, report);
    }

    // Token: 0x06001D43 RID: 7491 RVA: 0x005FDDB8 File Offset: 0x005FDDB8
    [return: Nullable(2)]
    public AgentDriveBinding MacStepBindingForBrainDrive(string goal)
    {
      List<ValueTuple<IAgentDriveJob, string, string, string>> list = (from j in this.jobs.Jobs
        where j.ThreadId != null && (this.waiters.Step(j.TaskId) ?? j.PendingComputerUseRequest) != null
        select new ValueTuple<IAgentDriveJob, string, string, string>(j, j.ThreadId, this.waiters.Step(j.TaskId) ?? j.PendingComputerUseRequest, j.Title)).ToList<ValueTuple<IAgentDriveJob, string, string, string>>();
      ValueTuple<IAgentDriveJob, string, string, string> valueTuple = list.FirstOrDefault<ValueTuple<IAgentDriveJob, string, string, string>>(([TupleElementNames(new string[] { "Job", "ThreadId", "Request", "Title" })] ValueTuple<IAgentDriveJob, string, string, string> c) => AgentDriveReports.DriveGoalNamesJob(goal, c.Item2, c.Item4));
      if (valueTuple.Item1 == null && list.Count == 1 && AgentDriveReports.DriveGoalSharesWordsWithStep(goal, list[0].Item3))
      {
        valueTuple = list[0];
      }
      if (valueTuple.Item1 == null)
      {
        return null;
      }
      string taskId = valueTuple.Item1.TaskId;
      string askingLease = valueTuple.Item1.AgentTurnLeaseId;
      valueTuple.Item1.PendingComputerUseRequest = null;
      this.waiters.ForgetUnanswered(taskId);
      if (this.waiters.IsWaiting(taskId))
      {
        this.waiters.MarkDriving(taskId);
      }
      ClickyAnalytics.TrackComputerUseConsent(true, false);
      ClickyDriveAnalytics.TrackMacStepAnswered(valueTuple.Item2, "approve", "brain_drive");
      Log.Info("drive", "the brain's drive is the step of thread=" + valueTuple.Item2);
      string request = valueTuple.Item3;
      string threadId = valueTuple.Item2;
      return new AgentDriveBinding(valueTuple.Item4, askingLease, delegate(AgentDriveOutcome outcome)
      {
        this.DeliverMacStepOutcome(taskId, threadId, askingLease, AgentDriveReports.Report(outcome, request));
      }, threadId);
    }

    // Token: 0x17000530 RID: 1328
    // (get) Token: 0x06001D44 RID: 7492 RVA: 0x005FDF30 File Offset: 0x005FDF30
    public bool IsPcStepMomentInProgress
    {
      get
      {
        return this.drive.IsDriving || DateTimeOffset.Now - this.drive.LastEndedAt < TimeSpan.FromSeconds(20L) || this.waiters.HasStepAsked(TimeSpan.FromSeconds(60L));
      }
    }

    // Token: 0x06001D45 RID: 7493 RVA: 0x005FDF84 File Offset: 0x005FDF84
    private async Task ContinueAskingTurnAsync(string threadId, [Nullable(2)] string askingTurnLeaseId, string answer)
    {
      string reopened = null;
      if (askingTurnLeaseId != null)
      {
        try
        {
          string text = await this.jobs.ContinueLeaseAfterConsentAsync(askingTurnLeaseId).ConfigureAwait(false);
          reopened = text;
        }
        catch (Exception ex)
        {
          Log.Warn("drive", "lease not reopened: " + ex.Message);
        }
        if (reopened == null)
        {
          this.jobs.CompleteLease(askingTurnLeaseId, threadId);
        }
      }
      string text2 = "drive";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 2);
      defaultInterpolatedStringHandler.AppendLiteral("answer thread=");
      defaultInterpolatedStringHandler.AppendFormatted(threadId);
      defaultInterpolatedStringHandler.AppendLiteral(" continuesAskingTurn=");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(reopened != null);
      Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
      this.jobs.SubmitFollowUp(threadId, answer, reopened, "HeyClicky is passing on the user's answer to your request for a step on their PC (they chose it on HeyClicky's card; they didn't type this message).");
    }

    // Token: 0x06001D46 RID: 7494 RVA: 0x005FDFE0 File Offset: 0x005FDFE0
    public void DeclineComputerUse(string taskId)
    {
      this.waiters.ForgetUnanswered(taskId);
      string text;
      if ((text = this.waiters.Step(taskId)) == null)
      {
        IAgentDriveJob agentDriveJob = this.jobs.Find(taskId);
        text = ((agentDriveJob != null) ? agentDriveJob.PendingComputerUseRequest : null);
      }
      string text2 = text;
      if (text2 != null)
      {
        this.waiters.RememberDeclined(taskId, text2);
      }
      try
      {
        Action<string> showCursorLine = this.ShowCursorLine;
        if (showCursorLine != null)
        {
          showCursorLine("Okay, I'll leave that step to you. The job finishes without it.");
        }
      }
      catch (Exception ex)
      {
        Log.Warn("drive", "decline line: " + ex.Message);
      }
      if (this.waiters.IsWaiting(taskId))
      {
        IAgentDriveJob agentDriveJob2 = this.jobs.Find(taskId);
        if (agentDriveJob2 != null)
        {
          agentDriveJob2.PendingComputerUseRequest = null;
        }
        ClickyAnalytics.TrackComputerUseConsent(false, false);
        ClickyDriveAnalytics.TrackMacStepAnswered((agentDriveJob2 != null) ? agentDriveJob2.ThreadId : null, "decline", "waiting_call");
        this.waiters.Resolve(taskId, MacStepAnswer.DeclinedAnswer);
        return;
      }
      IAgentDriveJob agentDriveJob3 = this.jobs.Find(taskId);
      if (agentDriveJob3 != null)
      {
        string threadId = agentDriveJob3.ThreadId;
        if (threadId != null)
        {
          string agentTurnLeaseId = agentDriveJob3.AgentTurnLeaseId;
          agentDriveJob3.PendingComputerUseRequest = null;
          ClickyAnalytics.TrackComputerUseConsent(false, false);
          ClickyDriveAnalytics.TrackMacStepAnswered(threadId, "decline", "end_of_turn");
          Log.Info("drive", "decline thread=" + threadId);
          this.ContinueAskingTurnAsync(threadId, agentTurnLeaseId, "Not now. Finish without controlling my PC, and tell me what you did get done. Don't ask to do this step on my PC again unless I tell you to go ahead with it.");
          return;
        }
      }
    }

    // Token: 0x06001D47 RID: 7495 RVA: 0x005FE13C File Offset: 0x005FE13C
    public async Task<JsonObject> AnswerMacStepToolCallAsync(string threadId, string step, string why, string ask = "")
    {
      AgentDriveConsent.<>c__DisplayClass21_0 CS$<>8__locals1 = new AgentDriveConsent.<>c__DisplayClass21_0();
      CS$<>8__locals1.threadId = threadId;
      CS$<>8__locals1.<>4__this = this;
      IAgentDriveJob job = this.jobs.Jobs.FirstOrDefault<IAgentDriveJob>((IAgentDriveJob j) => j.ThreadId == CS$<>8__locals1.threadId);
      JsonObject jsonObject2;
      if (job == null)
      {
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["error"] = "No running job for this call.";
        jsonObject2 = jsonObject;
      }
      else if (!this.AgentClickyDriveIsAvailable)
      {
        ClickyDriveAnalytics.TrackMacStepCallReturned(CS$<>8__locals1.threadId, "unavailable", false);
        jsonObject2 = AgentDriveReports.MacStepUnavailable();
      }
      else if ((job.NeedsChromeConnection || job.FoundChromeNotConnectedThisTurn) && !this.jobs.ChromeIsConnected && !this.jobs.SkipsChrome(CS$<>8__locals1.threadId))
      {
        ClickyDriveAnalytics.TrackMacStepCallReturned(CS$<>8__locals1.threadId, "waiting_on_chrome_choice", false);
        jsonObject2 = (job.NeedsChromeConnection ? AgentDriveReports.MacStepChoosingChrome() : AgentDriveReports.MacStepChromeAskedAtTurnEnd());
      }
      else
      {
        CS$<>8__locals1.taskId = job.TaskId;
        string text = this.waiters.Step(CS$<>8__locals1.taskId);
        if (text != null)
        {
          if (text != step)
          {
            ClickyDriveAnalytics.TrackMacStepCallReturned(CS$<>8__locals1.threadId, "busy", false);
            jsonObject2 = AgentDriveReports.MacStepBusy();
          }
          else
          {
            MacStepAnswer macStepAnswer = await this.waiters.WaitAsync(CS$<>8__locals1.taskId, step, AgentDriveReports.MacStepOpenCall, new Func<bool>(CS$<>8__locals1.<AnswerMacStepToolCallAsync>g__StillRunning|1), default(CancellationToken)).ConfigureAwait(false);
            ClickyDriveAnalytics.TrackMacStepCallReturned(CS$<>8__locals1.threadId, macStepAnswer, true);
            jsonObject2 = AgentDriveReports.MacStepToolResult(macStepAnswer, step);
          }
        }
        else if (job.PendingComputerUseRequest != null)
        {
          ClickyDriveAnalytics.TrackMacStepCallReturned(CS$<>8__locals1.threadId, "busy", false);
          jsonObject2 = AgentDriveReports.MacStepBusy();
        }
        else
        {
          string shown = AgentDriveReports.ShownRequest(step, why);
          if (this.waiters.WasRecentlyDeclined(CS$<>8__locals1.taskId, step) || this.waiters.WasRecentlyDeclined(CS$<>8__locals1.taskId, shown))
          {
            ClickyDriveAnalytics.TrackMacStepCallReturned(CS$<>8__locals1.threadId, "declined_before", false);
            jsonObject2 = AgentDriveReports.MacStepDeclinedBefore();
          }
          else
          {
            job.PendingComputerUseRequest = shown;
            job.PendingComputerUseSummary = AgentDriveReports.MacStepCardLine(ask, why);
            job.DidAnnounceComputerUseRequest = false;
            this.AnnounceAgentAsksToUsePcIfNeeded(job, "mac_step_call");
            this.jobs.PresentNeedsYou(job);
            Log.Info("drive", "mac_step asked thread=" + CS$<>8__locals1.threadId);
            MacStepAnswer macStepAnswer2 = await this.waiters.WaitAsync(CS$<>8__locals1.taskId, step, AgentDriveReports.MacStepOpenCall, new Func<bool>(CS$<>8__locals1.<AnswerMacStepToolCallAsync>g__StillRunning|1), default(CancellationToken)).ConfigureAwait(false);
            if (macStepAnswer2 is MacStepAnswer.NotYet)
            {
              this.waiters.RememberUnanswered(CS$<>8__locals1.taskId, shown);
            }
            else if (job.PendingComputerUseRequest == shown)
            {
              job.PendingComputerUseRequest = null;
            }
            ClickyDriveAnalytics.TrackMacStepCallReturned(CS$<>8__locals1.threadId, macStepAnswer2, false);
            jsonObject2 = AgentDriveReports.MacStepToolResult(macStepAnswer2, step);
          }
        }
      }
      return jsonObject2;
    }

    // Token: 0x06001D48 RID: 7496 RVA: 0x005FE1A0 File Offset: 0x005FE1A0
    public bool DriveWaitingMacStep(IAgentDriveJob job)
    {
      string taskId = job.TaskId;
      string step = this.waiters.Step(taskId);
      if (step == null)
      {
        return false;
      }
      job.PendingComputerUseRequest = null;
      ClickyAnalytics.TrackComputerUseConsent(true, false);
      ClickyDriveAnalytics.TrackMacStepAnswered(job.ThreadId, "approve", "waiting_call");
      this.waiters.MarkDriving(taskId);
      string threadId = job.ThreadId;
      string askingLease = job.AgentTurnLeaseId;
      if (!this.drive.DriveForAgent(step, job.Name, askingLease, delegate(AgentDriveOutcome outcome)
      {
        string text = AgentDriveReports.Report(outcome, step);
        if (threadId == null)
        {
          ClickyDriveAnalytics.TrackMacStepResultDelivered(threadId, "open_call");
          this.waiters.Resolve(taskId, new MacStepAnswer.Drove(text));
          return;
        }
        this.DeliverMacStepOutcome(taskId, threadId, askingLease, text);
      }, threadId))
      {
        this.waiters.Resolve(taskId, MacStepAnswer.CouldNotStartAnswer);
      }
      return true;
    }

    // Token: 0x04000DFA RID: 3578
    private readonly ClickyTakeOverController drive;

    // Token: 0x04000DFB RID: 3579
    private readonly IAgentDriveJobs jobs;

    // Token: 0x04000DFC RID: 3580
    private readonly MacStepWaiters waiters;

    // Token: 0x04000DFD RID: 3581
    public const string MacStepResultLaunchContext = "HeyClicky did the step on their PC you asked for and is passing on what happened (the user didn't type this message).";

    // Token: 0x04000DFE RID: 3582
    public const string MacStepAnswerLaunchContext = "HeyClicky is passing on the user's answer to your request for a step on their PC (they chose it on HeyClicky's card; they didn't type this message).";
  }
}
