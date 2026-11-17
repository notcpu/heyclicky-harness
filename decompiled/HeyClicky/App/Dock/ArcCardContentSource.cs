using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Notch.Window;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Tools;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002E8 RID: 744
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ArcCardContentSource
  {
    // Token: 0x06001FF5 RID: 8181 RVA: 0x00611174 File Offset: 0x00611174
    public ArcCardContentSource(AgentService agents, [Nullable(new byte[] { 1, 2 })] Func<OneBrainJobReports> jobReports, ArcDockHost host)
    {
    }

    // Token: 0x06001FF6 RID: 8182 RVA: 0x00611194 File Offset: 0x00611194
    public ArcCardContent ForJob(ClickyJob job)
    {
      ArcPaper arcPaper = new ArcPaper(NotePaperColors.Stock("job:" + job.Id));
      if (job.IsWaitingToStart)
      {
        return new ArcCardContent
        {
          RailKey = job.RailKey,
          Title = HeyClickyWindow.ShortName(job.DisplayName),
          State = ArcCardState.Suggested,
          Paper = arcPaper,
          Summary = ((job.Prompt.Length == 0) ? null : job.Prompt),
          JobId = job.Id,
          WorkspacePath = job.FolderPath
        };
      }
      ClickyJobRailPhase clickyJobRailPhase = this.<agents>P.RailPhase(job);
      string text = this.<agents>P.LiveTaskId(job.Id);
      AgentTaskDetail agentTaskDetail = ((text == null) ? null : this.<agents>P.GetTaskDetail(text));
      string text2 = ((agentTaskDetail != null) ? agentTaskDetail.Snapshot.ThreadId : null) ?? job.ThreadId;
      bool flag;
      if (clickyJobRailPhase == ClickyJobRailPhase.Done)
      {
        OneBrainJobReports oneBrainJobReports = this.<jobReports>P();
        flag = oneBrainJobReports != null && oneBrainJobReports.IsPresenting(text2);
      }
      else
      {
        flag = false;
      }
      bool flag2 = flag;
      ArcCardState arcCardState = (flag2 ? ArcCardState.Working : ArcCardStates.FromJobPhase(clickyJobRailPhase, job.IsSeen));
      string text3 = (ArcLiveNotes.IsEnabled ? ArcLiveNotes.NotePath(job.FolderPath) : null);
      string railKey = job.RailKey;
      bool flag3 = clickyJobRailPhase == ClickyJobRailPhase.Done && !flag2;
      bool flag4 = clickyJobRailPhase <= ClickyJobRailPhase.Working;
      bool flag5 = flag4;
      IReadOnlyList<string> readOnlyList2;
      if (!flag3)
      {
        IReadOnlyList<string> readOnlyList = Array.Empty<string>();
        readOnlyList2 = readOnlyList;
      }
      else
      {
        readOnlyList2 = job.FilesFromLatestReply;
      }
      IReadOnlyList<string> readOnlyList3 = readOnlyList2;
      ClickyAnswer clickyAnswer = (flag3 ? this.ResultCard(agentTaskDetail, text2, job.Id, null) : null);
      object obj;
      if (!flag3)
      {
        obj = null;
      }
      else if ((obj = ((clickyAnswer != null) ? clickyAnswer.Line : null)) == null)
      {
        obj = ((agentTaskDetail != null) ? agentTaskDetail.Summary : null) ?? job.Summary;
      }
      string text4 = obj;
      CardFooterRequest cardFooterRequest = this.Request(text, agentTaskDetail) ?? ArcCardContentSource.JobRequest(job, clickyJobRailPhase, text != null);
      ArcCardContent arcCardContent = new ArcCardContent();
      arcCardContent.RailKey = job.RailKey;
      arcCardContent.Title = HeyClickyWindow.ShortName(job.DisplayName);
      arcCardContent.State = arcCardState;
      arcCardContent.Paper = arcPaper;
      arcCardContent.Plan = (flag2 ? this.Finalizing(railKey, text3) : this.Plan(railKey, text3));
      ArcCardContent arcCardContent2 = arcCardContent;
      object obj2;
      if (!flag2)
      {
        if (!flag5)
        {
          obj2 = null;
        }
        else if ((obj2 = this.<agents>P.LiveSteps.Current(railKey, null)) == null)
        {
          obj2 = JobCardText.BodyText(job, clickyJobRailPhase, agentTaskDetail != null, ArcCardContentSource.LatestCommentary(agentTaskDetail), (agentTaskDetail != null) ? agentTaskDetail.ThinkingHeading : null, (text == null) ? null : this.<agents>P.PendingComputerUseRequest(text), (agentTaskDetail != null) ? agentTaskDetail.FailureMessage : null);
        }
      }
      else
      {
        obj2 = "Finalizing…";
      }
      arcCardContent2.CurrentStepLine = obj2;
      arcCardContent.Summary = ((text4 != null && text4.Trim().Length > 0) ? text4 : null);
      arcCardContent.Files = readOnlyList3;
      arcCardContent.ThreadId = job.ThreadId;
      arcCardContent.JobId = job.Id;
      arcCardContent.TaskId = text;
      arcCardContent.WorkspacePath = job.FolderPath;
      ArcCardContent arcCardContent3 = arcCardContent;
      flag4 = !flag2;
      if (flag4)
      {
        AgentTaskStatus? agentTaskStatus = ((agentTaskDetail != null) ? new AgentTaskStatus?(agentTaskDetail.Snapshot.Status) : null);
        bool flag6;
        if (agentTaskStatus != null)
        {
          AgentTaskStatus valueOrDefault = agentTaskStatus.GetValueOrDefault();
          if (valueOrDefault <= AgentTaskStatus.Running)
          {
            flag6 = true;
            goto IL_0339;
          }
        }
        flag6 = false;
        IL_0339:
        flag4 = flag6;
      }
      arcCardContent3.TaskIsRunning = flag4;
      arcCardContent.Request = cardFooterRequest;
      arcCardContent.ReadyOutput = (flag3 ? ArcCardContentSource.ReadyOutput(readOnlyList3, text4, job.ThreadId, job.FolderPath) : null);
      arcCardContent.LiveLine = (flag5 ? this.<agents>P.LiveSteps.Line(railKey) : null);
      arcCardContent.Answer = clickyAnswer;
      arcCardContent.IsFinalizing = flag2;
      arcCardContent.NotePath = text3;
      return arcCardContent;
    }

    // Token: 0x06001FF7 RID: 8183 RVA: 0x00611550 File Offset: 0x00611550
    public ArcCardContent ForClicky(string slug)
    {
      ClickyInfo clickyInfo = this.<agents>P.FindClicky(slug);
      ArcPaper arcPaper = new ArcPaper(NotePaperColors.Stock("clicky:" + slug));
      string text = this.ClickyTaskId(slug);
      AgentTaskDetail agentTaskDetail = ((text == null) ? null : this.<agents>P.GetTaskDetail(text));
      ArcCardContentSource.LatestTurn latestTurn = this.Latest(slug, agentTaskDetail);
      bool flag = agentTaskDetail == null && latestTurn != null && latestTurn.Kind == ArcLatestKind.Failed && latestTurn.Label == "Interrupted";
      string text2 = ((latestTurn != null) ? latestTurn.ThreadId : null) ?? ((clickyInfo != null) ? clickyInfo.ThreadId : null);
      bool flag2;
      if (latestTurn != null && latestTurn.Kind == ArcLatestKind.Finished)
      {
        OneBrainJobReports oneBrainJobReports = this.<jobReports>P();
        flag2 = oneBrainJobReports != null && oneBrainJobReports.IsPresenting(((agentTaskDetail != null) ? agentTaskDetail.Snapshot.ThreadId : null) ?? text2);
      }
      else
      {
        flag2 = false;
      }
      bool flag3 = flag2;
      bool flag4 = this.<host>P.UnreadClickySlugs().Contains(slug);
      ArcCardState arcCardState = (flag3 ? ArcCardState.Working : ArcCardStates.FromLatest((latestTurn != null) ? new ArcLatestKind?(latestTurn.Kind) : null, flag4));
      if (!flag3 && arcCardState == ArcCardState.Failed && ((agentTaskDetail != null && agentTaskDetail.Snapshot.Status == AgentTaskStatus.Stopped) || flag))
      {
        arcCardState = ArcCardState.Stopped;
      }
      string text3 = (ArcLiveNotes.IsEnabled ? ArcLiveNotes.ClickyNotePath(this.<agents>P, slug) : null);
      bool flag5 = latestTurn != null && latestTurn.Kind == ArcLatestKind.Finished && !flag3;
      IReadOnlyList<string> readOnlyList2;
      if (!flag5)
      {
        IReadOnlyList<string> readOnlyList = Array.Empty<string>();
        readOnlyList2 = readOnlyList;
      }
      else
      {
        readOnlyList2 = latestTurn.Artifacts;
      }
      IReadOnlyList<string> readOnlyList3 = readOnlyList2;
      ClickyAnswer clickyAnswer = (flag5 ? this.ResultCard(agentTaskDetail, text2, null, slug) : null);
      object obj;
      if (!flag5)
      {
        obj = null;
      }
      else if ((obj = ((clickyAnswer != null) ? clickyAnswer.Line : null)) == null)
      {
        obj = ((agentTaskDetail != null) ? agentTaskDetail.Summary : null) ?? ((latestTurn != null) ? latestTurn.Text : null);
      }
      string text4 = obj;
      CardFooterRequest cardFooterRequest;
      if ((cardFooterRequest = this.Request(text, agentTaskDetail)) == null)
      {
        cardFooterRequest = ((agentTaskDetail == null && arcCardState == ArcCardState.Failed) ? new CardFooterRequest(CardFooterRequest.Kinds.Failed, ((latestTurn != null) ? latestTurn.Text : null) ?? "", null) : null) ?? (flag ? new CardFooterRequest(CardFooterRequest.Kinds.Stopped, "HeyClicky restarted before this finished.", null) : null);
      }
      CardFooterRequest cardFooterRequest2 = cardFooterRequest;
      string text5;
      if ((text5 = ((agentTaskDetail != null) ? agentTaskDetail.WorkspacePath : null)) == null)
      {
        text5 = this.<agents>P.WorkspacePath(slug) ?? "";
      }
      string text6 = text5;
      string text7 = HeyClickyWindow.ShortName(((clickyInfo != null) ? clickyInfo.Name : null) ?? slug);
      ArcCardContent arcCardContent = new ArcCardContent();
      arcCardContent.RailKey = slug;
      arcCardContent.Title = text7;
      arcCardContent.State = arcCardState;
      arcCardContent.Paper = arcPaper;
      arcCardContent.Plan = ((text3 == null) ? null : (flag3 ? this.Finalizing(text3, text3) : this.Plan(text3, text3)));
      arcCardContent.CurrentStepLine = (flag3 ? "Finalizing…" : ((arcCardState == ArcCardState.Working) ? (((text3 != null) ? this.<agents>P.LiveSteps.Current(text3, null) : null) ?? ((latestTurn != null) ? latestTurn.Text : null)) : null));
      arcCardContent.Summary = ((text4 != null && text4.Trim().Length > 0) ? text4 : null);
      arcCardContent.Files = readOnlyList3;
      arcCardContent.ThreadId = text2;
      arcCardContent.ClickySlug = slug;
      arcCardContent.TaskId = text;
      arcCardContent.WorkspacePath = text6;
      ArcCardContent arcCardContent2 = arcCardContent;
      bool flag6 = !flag3;
      if (flag6)
      {
        AgentTaskStatus? agentTaskStatus = ((agentTaskDetail != null) ? new AgentTaskStatus?(agentTaskDetail.Snapshot.Status) : null);
        bool flag7;
        if (agentTaskStatus != null)
        {
          AgentTaskStatus valueOrDefault = agentTaskStatus.GetValueOrDefault();
          if (valueOrDefault <= AgentTaskStatus.Running)
          {
            flag7 = true;
            goto IL_038F;
          }
        }
        flag7 = false;
        IL_038F:
        flag6 = flag7;
      }
      arcCardContent2.TaskIsRunning = flag6;
      arcCardContent.Request = cardFooterRequest2;
      ArcCardContent arcCardContent3 = arcCardContent;
      bool flag8 = arcCardState - ArcCardState.New <= 1;
      arcCardContent3.ReadyOutput = (flag8 ? ArcCardContentSource.ReadyOutput(readOnlyList3, text4, text2, text6) : null);
      arcCardContent.LiveLine = ((arcCardState == ArcCardState.Working && !flag3 && text3 != null) ? this.<agents>P.LiveSteps.Line(text3) : null);
      arcCardContent.Answer = clickyAnswer;
      arcCardContent.IsFinalizing = flag3;
      arcCardContent.NotePath = text3;
      return arcCardContent;
    }

    // Token: 0x06001FF8 RID: 8184 RVA: 0x00611978 File Offset: 0x00611978
    [return: Nullable(2)]
    public string ClickyTaskId(string slug)
    {
      ArcCardContentSource.<>c__DisplayClass7_0 CS$<>8__locals1 = new ArcCardContentSource.<>c__DisplayClass7_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.slug = slug;
      ArcCardContentSource.<>c__DisplayClass7_0 CS$<>8__locals2 = CS$<>8__locals1;
      ClickyInfo clickyInfo = this.<agents>P.FindClicky(CS$<>8__locals1.slug);
      CS$<>8__locals2.bound = ((clickyInfo != null) ? clickyInfo.ThreadId : null);
      List<AgentTaskDetail> list = (from d in this.<agents>P.Tasks.Select<AgentTaskSnapshot, AgentTaskDetail>((AgentTaskSnapshot t) => CS$<>8__locals1.<>4__this.<agents>P.GetTaskDetail(t.TaskId)).OfType<AgentTaskDetail>()
        where !d.IsDemo
        select d).ToList<AgentTaskDetail>();
      AgentTaskDetail agentTaskDetail = ((CS$<>8__locals1.bound != null) ? list.FirstOrDefault<AgentTaskDetail>((AgentTaskDetail d) => d.Snapshot.ThreadId == CS$<>8__locals1.bound) : null);
      string text;
      if ((text = ((agentTaskDetail != null) ? agentTaskDetail.Snapshot.TaskId : null)) == null)
      {
        AgentTaskDetail agentTaskDetail2 = list.FirstOrDefault<AgentTaskDetail>((AgentTaskDetail d) => d.ClickySlug == CS$<>8__locals1.slug);
        if (agentTaskDetail2 == null)
        {
          return null;
        }
        text = agentTaskDetail2.Snapshot.TaskId;
      }
      return text;
    }

    // Token: 0x06001FF9 RID: 8185 RVA: 0x00611A5C File Offset: 0x00611A5C
    [NullableContext(2)]
    private ArcCardContentSource.LatestTurn Latest([Nullable(1)] string slug, AgentTaskDetail detail)
    {
      ClickyAppLiveStatus clickyAppLiveStatus = this.<agents>P.LiveStatusForClicky(slug);
      switch (clickyAppLiveStatus.Phase)
      {
      case ClickyAppLiveStatus.Phases.Working:
      {
        ArcLatestKind arcLatestKind = ArcLatestKind.Working;
        string text = "Working";
        DateTimeOffset? dateTimeOffset = clickyAppLiveStatus.StartedAt;
        string text2;
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
          text2 = " · " + ArcCardContentSource.Relative(valueOrDefault);
        }
        else
        {
          text2 = "";
        }
        return new ArcCardContentSource.LatestTurn(arcLatestKind, text + text2, clickyAppLiveStatus.Line, Array.Empty<string>(), (detail != null) ? detail.Snapshot.ThreadId : null);
      }
      case ClickyAppLiveStatus.Phases.NeedsYou:
        return new ArcCardContentSource.LatestTurn(ArcLatestKind.NeedsYou, "Needs you", clickyAppLiveStatus.Line, Array.Empty<string>(), (detail != null) ? detail.Snapshot.ThreadId : null);
      case ClickyAppLiveStatus.Phases.Failed:
        return new ArcCardContentSource.LatestTurn(ArcLatestKind.Failed, "Didn't finish", clickyAppLiveStatus.Line, Array.Empty<string>(), (detail != null) ? detail.Snapshot.ThreadId : null);
      }
      ClickyWorkPiece openQuestion = this.<agents>P.ClickyApps.Shelf(slug).OpenQuestion;
      if (openQuestion != null)
      {
        return new ArcCardContentSource.LatestTurn(ArcLatestKind.NeedsYou, "Waiting for your answer", (openQuestion.Summary.Length == 0) ? openQuestion.Title : openQuestion.Summary, Array.Empty<string>(), openQuestion.ThreadId);
      }
      if (clickyAppLiveStatus.Phase == ClickyAppLiveStatus.Phases.Finished && detail != null)
      {
        ArcLatestKind arcLatestKind2 = ArcLatestKind.Finished;
        string text3 = "Latest";
        DateTimeOffset? dateTimeOffset = clickyAppLiveStatus.FinishedAt;
        string text4;
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault2 = dateTimeOffset.GetValueOrDefault();
          text4 = " · " + ArcCardContentSource.Relative(valueOrDefault2);
        }
        else
        {
          text4 = "";
        }
        return new ArcCardContentSource.LatestTurn(arcLatestKind2, text3 + text4, clickyAppLiveStatus.Line, detail.Artifacts, detail.Snapshot.ThreadId);
      }
      ClickyWorkPiece clickyWorkPiece = (from w in this.<agents>P.ClickyApps.Works(slug)
        orderby w.FinishedAt descending
        select w).FirstOrDefault<ClickyWorkPiece>();
      if (detail == null)
      {
        ClickyRestartInterruptions.Turn turn = this.<agents>P.RestartInterruptions.Interruption(slug);
        if (turn != null && (clickyWorkPiece == null || clickyWorkPiece.FinishedAt < turn.StartedAt))
        {
          return new ArcCardContentSource.LatestTurn(ArcLatestKind.Failed, "Interrupted", "HeyClicky restarted before this finished.", Array.Empty<string>(), turn.ThreadId);
        }
      }
      if (clickyWorkPiece == null)
      {
        return null;
      }
      ArcLatestKind arcLatestKind3 = (clickyWorkPiece.Failed ? ArcLatestKind.Failed : ArcLatestKind.Finished);
      string text5 = (clickyWorkPiece.Failed ? "Didn't finish" : "Latest") + " · " + ArcCardContentSource.Relative(clickyWorkPiece.FinishedAt);
      string text6 = ((clickyWorkPiece.Summary.Length == 0) ? clickyWorkPiece.Title : clickyWorkPiece.Summary);
      IReadOnlyList<string> readOnlyList;
      if (!clickyWorkPiece.Failed)
      {
        readOnlyList = clickyWorkPiece.ArtifactPaths;
      }
      else
      {
        IReadOnlyList<string> readOnlyList2 = Array.Empty<string>();
        readOnlyList = readOnlyList2;
      }
      return new ArcCardContentSource.LatestTurn(arcLatestKind3, text5, text6, readOnlyList, clickyWorkPiece.ThreadId);
    }

    // Token: 0x06001FFA RID: 8186 RVA: 0x00611D00 File Offset: 0x00611D00
    private static string Relative(DateTimeOffset date)
    {
      TimeSpan timeSpan = DateTimeOffset.Now - date;
      if (timeSpan.TotalSeconds < 60.0)
      {
        return "just now";
      }
      if (timeSpan.TotalMinutes < 60.0)
      {
        int num = (int)timeSpan.TotalMinutes;
        if (num != 1)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 1);
          defaultInterpolatedStringHandler.AppendFormatted<int>(num);
          defaultInterpolatedStringHandler.AppendLiteral(" minutes ago");
          return defaultInterpolatedStringHandler.ToStringAndClear();
        }
        return "1 minute ago";
      }
      else if (timeSpan.TotalHours < 24.0)
      {
        int num2 = (int)timeSpan.TotalHours;
        if (num2 != 1)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
          defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
          defaultInterpolatedStringHandler.AppendLiteral(" hours ago");
          return defaultInterpolatedStringHandler.ToStringAndClear();
        }
        return "1 hour ago";
      }
      else
      {
        int num3 = (int)timeSpan.TotalDays;
        if (num3 != 1)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
          defaultInterpolatedStringHandler.AppendFormatted<int>(num3);
          defaultInterpolatedStringHandler.AppendLiteral(" days ago");
          return defaultInterpolatedStringHandler.ToStringAndClear();
        }
        return "1 day ago";
      }
    }

    // Token: 0x06001FFB RID: 8187 RVA: 0x00611E04 File Offset: 0x00611E04
    [NullableContext(2)]
    private static string LatestCommentary(AgentTaskDetail detail)
    {
      if (detail == null)
      {
        return null;
      }
      AgentActivityItem agentActivityItem = detail.Activity.LastOrDefault<AgentActivityItem>((AgentActivityItem a) => a.Kind == AgentActivityKind.Commentary);
      if (agentActivityItem == null)
      {
        return null;
      }
      return agentActivityItem.Text;
    }

    // Token: 0x06001FFC RID: 8188 RVA: 0x00611E40 File Offset: 0x00611E40
    [NullableContext(2)]
    private ClickyArcPlan Plan([Nullable(1)] string key, string notePath)
    {
      ClickyArcPlan clickyArcPlan = this.<agents>P.LiveSteps.Plan(key);
      if (clickyArcPlan != null && !clickyArcPlan.IsEmpty)
      {
        return clickyArcPlan;
      }
      if (notePath == null)
      {
        return null;
      }
      return this.<host>P.NoteChecklistPlan(notePath);
    }

    // Token: 0x06001FFD RID: 8189 RVA: 0x00611E84 File Offset: 0x00611E84
    [NullableContext(2)]
    private ClickyArcPlan Finalizing([Nullable(1)] string key, string notePath)
    {
      ClickyArcPlan clickyArcPlan = this.<agents>P.LiveSteps.LastPlan(key) ?? ((notePath != null) ? this.<host>P.NoteChecklistPlan(notePath) : null);
      if (clickyArcPlan == null || clickyArcPlan.IsEmpty)
      {
        return null;
      }
      return clickyArcPlan.Finalizing;
    }

    // Token: 0x06001FFE RID: 8190 RVA: 0x00611ED4 File Offset: 0x00611ED4
    [NullableContext(2)]
    private ClickyAnswer ResultCard(AgentTaskDetail detail, string threadId, string jobId, string slug)
    {
      if (OneBrainJobReports.IsOn && threadId != null)
      {
        ClickyAnswer clickyAnswer = this.<agents>P.ResultCard(threadId);
        if (clickyAnswer != null)
        {
          return clickyAnswer;
        }
      }
      return ((detail != null) ? detail.Answer : null) ?? this.<host>P.LatestReplyAnswer(jobId, slug);
    }

    // Token: 0x06001FFF RID: 8191 RVA: 0x00611F20 File Offset: 0x00611F20
    [NullableContext(2)]
    private CardFooterRequest Request(string taskId, AgentTaskDetail detail)
    {
      if (taskId == null || detail == null)
      {
        return null;
      }
      string text = this.<agents>P.PendingComputerUseRequest(taskId);
      AgentTaskStatus status = detail.Snapshot.Status;
      string text2 = ((status == AgentTaskStatus.Stopped) ? ((detail.ProgressSummary.Trim() == "Stopped.") ? "Stopped by you." : detail.ProgressSummary) : detail.FailureMessage);
      bool flag = true;
      string text3 = text;
      string text4 = this.<agents>P.PendingComputerUseSummary(taskId);
      string text5 = ((text == null) ? "" : ClickyNotes.SpeakableSentence(text, 110));
      bool requiresExtraUsageApproval = detail.RequiresExtraUsageApproval;
      string threadId = detail.Snapshot.ThreadId;
      bool flag2 = threadId != null && this.<agents>P.NeedsChromeConnection(threadId);
      bool flag3 = status == AgentTaskStatus.Finished;
      ClickyJobUserStep pendingUserStep = detail.PendingUserStep;
      return CardFooterRequest.FromTask(flag, text3, text4, text5, requiresExtraUsageApproval, flag2, flag3, (pendingUserStep != null) ? pendingUserStep.Step : null, this.<agents>P.CanRetryAfterFailure(taskId), this.<agents>P.CanRetryAfterInterruption(taskId), text2);
    }

    // Token: 0x06002000 RID: 8192 RVA: 0x00611FF8 File Offset: 0x00611FF8
    [return: Nullable(2)]
    private static CardFooterRequest JobRequest(ClickyJob job, ClickyJobRailPhase phase, bool hasTask)
    {
      if (job.IsHeldForChrome)
      {
        return new CardFooterRequest(CardFooterRequest.Kinds.ConnectChrome, "", null);
      }
      ClickyJobUserStep waitingStep = job.WaitingStep;
      bool flag = waitingStep != null;
      if (flag)
      {
        bool flag2 = phase <= ClickyJobRailPhase.Working;
        flag = !flag2;
      }
      if (flag)
      {
        return new CardFooterRequest(CardFooterRequest.Kinds.UserStep, waitingStep.Step, null);
      }
      if (hasTask || job.ThreadId == null)
      {
        return null;
      }
      CardFooterRequest cardFooterRequest;
      if (phase != ClickyJobRailPhase.Failed)
      {
        if (phase != ClickyJobRailPhase.Stopped)
        {
          cardFooterRequest = null;
        }
        else
        {
          cardFooterRequest = new CardFooterRequest(CardFooterRequest.Kinds.Stopped, "", null);
        }
      }
      else
      {
        cardFooterRequest = new CardFooterRequest(CardFooterRequest.Kinds.Failed, job.Summary ?? "", null);
      }
      return cardFooterRequest;
    }

    // Token: 0x06002001 RID: 8193 RVA: 0x0061208C File Offset: 0x0061208C
    [NullableContext(2)]
    [return: Nullable(1)]
    public static CardReadyOutput ReadyOutput([Nullable(1)] IReadOnlyList<string> files, string summary, string threadId, string workspace)
    {
      Func<IReadOnlyList<string>, IReadOnlyList<string>> func = (IReadOnlyList<string> entries) => ArcCardContentSource.MainEntries(entries, workspace);
      Func<string, string> func2 = (string e) => DockFiles.Resolve(e, workspace);
      Func<string, ValueTuple<string, string, uint>> func3;
      if ((func3 = ArcCardContentSource.<>O.<0>__FileKind) == null)
      {
        func3 = (ArcCardContentSource.<>O.<0>__FileKind = new Func<string, ValueTuple<string, string, uint>>(FileCardStack.FileKind));
      }
      return CardReadyOutput.Make(files, summary, threadId, func, func2, func3);
    }

    // Token: 0x06002002 RID: 8194 RVA: 0x006120E4 File Offset: 0x006120E4
    public static IReadOnlyList<string> MainEntries(IReadOnlyList<string> entries, [Nullable(2)] string workspace)
    {
      return ClickyMainArtifacts.MainEntries(entries, (string e) => DockFiles.Resolve(e, workspace));
    }

    // Token: 0x06002003 RID: 8195 RVA: 0x00612110 File Offset: 0x00612110
    public static IReadOnlyList<string> CardEntries(ArcCardContent content)
    {
      HashSet<string> seen = new HashSet<string>();
      List<string> list = ArcCardContentSource.MainEntries(content.Files.Where<string>((string f) => f.Length > 0 && seen.Add(f)).ToList<string>(), content.WorkspacePath).ToList<string>();
      CardReadyOutput readyOutput = content.ReadyOutput;
      string text = ((readyOutput != null) ? readyOutput.CardEntry : null);
      if (text != null)
      {
        list.Remove(text);
        list.Insert(0, text);
      }
      return list;
    }

    // Token: 0x04000F63 RID: 3939
    [CompilerGenerated]
    private AgentService <agents>P = agents;

    // Token: 0x04000F64 RID: 3940
    [Nullable(new byte[] { 1, 2 })]
    [CompilerGenerated]
    private Func<OneBrainJobReports> <jobReports>P = jobReports;

    // Token: 0x04000F65 RID: 3941
    [CompilerGenerated]
    private ArcDockHost <host>P = host;

    // Token: 0x04000F66 RID: 3942
    public const string RestartInterruptionReason = "HeyClicky restarted before this finished.";

    // Token: 0x02000923 RID: 2339
    [Nullable(0)]
    public sealed class LatestTurn : IEquatable<ArcCardContentSource.LatestTurn>
    {
      // Token: 0x06003C61 RID: 15457 RVA: 0x00697284 File Offset: 0x00697284
      public LatestTurn(ArcLatestKind Kind, string Label, string Text, IReadOnlyList<string> Artifacts, [Nullable(2)] string ThreadId)
      {
        this.Kind = Kind;
        this.Label = Label;
        this.Text = Text;
        this.Artifacts = Artifacts;
        this.ThreadId = ThreadId;
        base..ctor();
      }

      // Token: 0x17000848 RID: 2120
      // (get) Token: 0x06003C62 RID: 15458 RVA: 0x006972B4 File Offset: 0x006972B4
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ArcCardContentSource.LatestTurn);
        }
      }

      // Token: 0x17000849 RID: 2121
      // (get) Token: 0x06003C63 RID: 15459 RVA: 0x006972C0 File Offset: 0x006972C0
      // (set) Token: 0x06003C64 RID: 15460 RVA: 0x006972C8 File Offset: 0x006972C8
      public ArcLatestKind Kind { get; set; }

      // Token: 0x1700084A RID: 2122
      // (get) Token: 0x06003C65 RID: 15461 RVA: 0x006972D4 File Offset: 0x006972D4
      // (set) Token: 0x06003C66 RID: 15462 RVA: 0x006972DC File Offset: 0x006972DC
      public string Label { get; set; }

      // Token: 0x1700084B RID: 2123
      // (get) Token: 0x06003C67 RID: 15463 RVA: 0x006972E8 File Offset: 0x006972E8
      // (set) Token: 0x06003C68 RID: 15464 RVA: 0x006972F0 File Offset: 0x006972F0
      public string Text { get; set; }

      // Token: 0x1700084C RID: 2124
      // (get) Token: 0x06003C69 RID: 15465 RVA: 0x006972FC File Offset: 0x006972FC
      // (set) Token: 0x06003C6A RID: 15466 RVA: 0x00697304 File Offset: 0x00697304
      public IReadOnlyList<string> Artifacts { get; set; }

      // Token: 0x1700084D RID: 2125
      // (get) Token: 0x06003C6B RID: 15467 RVA: 0x00697310 File Offset: 0x00697310
      // (set) Token: 0x06003C6C RID: 15468 RVA: 0x00697318 File Offset: 0x00697318
      [Nullable(2)]
      public string ThreadId
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x06003C6D RID: 15469 RVA: 0x00697324 File Offset: 0x00697324
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("LatestTurn");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003C6E RID: 15470 RVA: 0x00697370 File Offset: 0x00697370
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Kind = ");
        builder.Append(this.Kind.ToString());
        builder.Append(", Label = ");
        builder.Append(this.Label);
        builder.Append(", Text = ");
        builder.Append(this.Text);
        builder.Append(", Artifacts = ");
        builder.Append(this.Artifacts);
        builder.Append(", ThreadId = ");
        builder.Append(this.ThreadId);
        return true;
      }

      // Token: 0x06003C6F RID: 15471 RVA: 0x00697410 File Offset: 0x00697410
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ArcCardContentSource.LatestTurn left, ArcCardContentSource.LatestTurn right)
      {
        return !(left == right);
      }

      // Token: 0x06003C70 RID: 15472 RVA: 0x0069741C File Offset: 0x0069741C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ArcCardContentSource.LatestTurn left, ArcCardContentSource.LatestTurn right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003C71 RID: 15473 RVA: 0x00697430 File Offset: 0x00697430
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ArcLatestKind>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Label>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Artifacts>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField);
      }

      // Token: 0x06003C72 RID: 15474 RVA: 0x006974C0 File Offset: 0x006974C0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ArcCardContentSource.LatestTurn);
      }

      // Token: 0x06003C73 RID: 15475 RVA: 0x006974D0 File Offset: 0x006974D0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ArcCardContentSource.LatestTurn other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ArcLatestKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Label>k__BackingField, other.<Label>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Artifacts>k__BackingField, other.<Artifacts>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField));
      }

      // Token: 0x06003C75 RID: 15477 RVA: 0x00697580 File Offset: 0x00697580
      [CompilerGenerated]
      private LatestTurn(ArcCardContentSource.LatestTurn original)
      {
        this.Kind = original.<Kind>k__BackingField;
        this.Label = original.<Label>k__BackingField;
        this.Text = original.<Text>k__BackingField;
        this.Artifacts = original.<Artifacts>k__BackingField;
        this.ThreadId = original.<ThreadId>k__BackingField;
      }

      // Token: 0x06003C76 RID: 15478 RVA: 0x006975D0 File Offset: 0x006975D0
      [CompilerGenerated]
      public void Deconstruct(out ArcLatestKind Kind, out string Label, out string Text, out IReadOnlyList<string> Artifacts, [Nullable(2)] out string ThreadId)
      {
        Kind = this.Kind;
        Label = this.Label;
        Text = this.Text;
        Artifacts = this.Artifacts;
        ThreadId = this.ThreadId;
      }
    }

    // Token: 0x02000924 RID: 2340
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x0400268B RID: 9867
      [TupleElementNames(new string[] { "KindLabel", "Symbol", "Tint" })]
      [Nullable(new byte[] { 0, 1, 0, 1, 1 })]
      public static Func<string, ValueTuple<string, string, uint>> <0>__FileKind;
    }
  }
}
