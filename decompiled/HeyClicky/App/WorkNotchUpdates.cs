using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Browser;
using HeyClicky.App.Dock;
using HeyClicky.App.Home;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.Notch.Surfaces;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Tools;

namespace HeyClicky.App
{
  // Token: 0x0200007E RID: 126
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class WorkNotchUpdates : INotchUpdateContentSource
  {
    // Token: 0x14000003 RID: 3
    // (add) Token: 0x060004B2 RID: 1202 RVA: 0x0052A270 File Offset: 0x0052A270
    // (remove) Token: 0x060004B3 RID: 1203 RVA: 0x0052A2A8 File Offset: 0x0052A2A8
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x060004B4 RID: 1204 RVA: 0x0052A2E0 File Offset: 0x0052A2E0
    public WorkNotchUpdates(AgentService agents, [Nullable(new byte[] { 1, 2 })] Func<OneBrainJobReports> reports, AppHost host)
    {
      this.agents = agents;
      this.reports = reports;
      this.host = host;
      agents.TasksChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        Action changed = this.Changed;
        if (changed == null)
        {
          return;
        }
        changed();
      };
      agents.Jobs.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        Action changed2 = this.Changed;
        if (changed2 == null)
        {
          return;
        }
        changed2();
      };
      agents.ResultCardShown += delegate(string _)
      {
        Action changed3 = this.Changed;
        if (changed3 == null)
        {
          return;
        }
        changed3();
      };
      OneBrainJobReports oneBrainJobReports = reports();
      if (oneBrainJobReports != null)
      {
        oneBrainJobReports.PresentingChanged += delegate
        {
          Action changed4 = this.Changed;
          if (changed4 == null)
          {
            return;
          }
          changed4();
        };
      }
    }

    // Token: 0x060004B5 RID: 1205 RVA: 0x0052A360 File Offset: 0x0052A360
    [return: Nullable(2)]
    public NotchUpdateContent Content(string railKey)
    {
      string text = ClickyJob.JobIdFromRailKey(railKey);
      if (text == null)
      {
        return this.ClickyContent(railKey);
      }
      return this.JobContent(text, railKey);
    }

    // Token: 0x060004B6 RID: 1206 RVA: 0x0052A388 File Offset: 0x0052A388
    [return: TupleElementNames(new string[] { "Subject", "Properties" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    public ValueTuple<string, JsonObject>? ShownAnalytics(string railKey, NotchUpdateKind kind)
    {
      bool flag = kind == NotchUpdateKind.Done;
      ArcDock dock = this.host.Dock;
      ArcCardContent arcCardContent = ((dock != null) ? dock.ContentForKey(railKey) : null);
      if (arcCardContent != null)
      {
        return new ValueTuple<string, JsonObject>?(new ValueTuple<string, JsonObject>((flag && this.IsRoutineReturn(arcCardContent)) ? "routine" : ((arcCardContent.JobId == null) ? "clicky" : "job"), NotchUpdateCardAnalytics.ShownProperties(arcCardContent, flag)));
      }
      NotchUpdateContent notchUpdateContent = this.Content(railKey);
      if (notchUpdateContent == null)
      {
        return null;
      }
      string text = (flag ? "done" : ((kind == NotchUpdateKind.NeedsYou) ? "needs_you" : (notchUpdateContent.IsStopped ? "stopped" : "failed")));
      string text2 = ((notchUpdateContent.JobId == null) ? "clicky" : "job");
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["state"] = text;
      return new ValueTuple<string, JsonObject>?(new ValueTuple<string, JsonObject>(text2, jsonObject));
    }

    // Token: 0x060004B7 RID: 1207 RVA: 0x0052A46C File Offset: 0x0052A46C
    private bool IsRoutineReturn(ArcCardContent content)
    {
      string clickySlug = content.ClickySlug;
      if (clickySlug == null)
      {
        return false;
      }
      string taskId = content.TaskId;
      bool flag;
      if (taskId != null)
      {
        AgentTaskDetail taskDetail = this.agents.GetTaskDetail(taskId);
        if (taskDetail != null)
        {
          flag = taskDetail.IsRoutineRun;
          goto IL_0065;
        }
      }
      ChatHandOffs handOffs = NotchChat.HandOffs;
      if (handOffs != null)
      {
        string text = handOffs.LatestReturnEntryId(null, clickySlug);
        if (text != null)
        {
          ChatHandOff chatHandOff = handOffs.Entry(text);
          flag = chatHandOff != null && chatHandOff.IsRoutineRun.GetValueOrDefault();
          goto IL_0065;
        }
      }
      flag = false;
      IL_0065:
      return flag && this.agents.Routines.ForClicky(clickySlug).Count > 0;
    }

    // Token: 0x060004B8 RID: 1208 RVA: 0x0052A4FC File Offset: 0x0052A4FC
    [return: Nullable(2)]
    private NotchUpdateContent JobContent(string jobId, string railKey)
    {
      ClickyJob clickyJob = this.agents.Jobs.Job(jobId);
      if (clickyJob == null)
      {
        return null;
      }
      ClickyJobRailPhase clickyJobRailPhase = this.agents.RailPhase(clickyJob);
      string text = this.agents.LiveTaskId(clickyJob.Id);
      AgentTaskDetail agentTaskDetail = ((text != null) ? this.agents.GetTaskDetail(text) : null);
      string text2 = ((agentTaskDetail != null) ? agentTaskDetail.Snapshot.ThreadId : null) ?? clickyJob.ThreadId;
      bool flag;
      if (clickyJobRailPhase == ClickyJobRailPhase.Done)
      {
        OneBrainJobReports oneBrainJobReports = this.reports();
        flag = oneBrainJobReports != null && oneBrainJobReports.IsPresenting(text2);
      }
      else
      {
        flag = false;
      }
      bool flag2 = flag;
      bool flag3 = clickyJobRailPhase == ClickyJobRailPhase.Done && !flag2;
      IReadOnlyList<string> readOnlyList2;
      if (!flag3)
      {
        IReadOnlyList<string> readOnlyList = Array.Empty<string>();
        readOnlyList2 = readOnlyList;
      }
      else
      {
        readOnlyList2 = clickyJob.FilesFromLatestReply;
      }
      IReadOnlyList<string> readOnlyList3 = readOnlyList2;
      ClickyAnswer clickyAnswer = (flag3 ? (((text2 == null) ? null : this.agents.ResultCard(text2)) ?? ((agentTaskDetail != null) ? agentTaskDetail.Answer : null)) : null);
      object obj;
      if (!flag3)
      {
        obj = null;
      }
      else if ((obj = ((clickyAnswer != null) ? clickyAnswer.Line : null)) == null)
      {
        obj = ((agentTaskDetail != null) ? agentTaskDetail.Summary : null) ?? clickyJob.Summary;
      }
      string text3 = obj;
      NotchUpdateRequest notchUpdateRequest = this.TaskRequest(agentTaskDetail, clickyJob.Title, clickyJob.Id) ?? this.JobRequest(clickyJob, clickyJobRailPhase, agentTaskDetail);
      return new NotchUpdateContent
      {
        RailKey = railKey,
        JobId = clickyJob.Id,
        ThreadId = text2,
        Title = clickyJob.Title,
        Summary = (string.IsNullOrWhiteSpace(text3) ? null : text3),
        Request = notchUpdateRequest,
        ReadyOutput = (flag3 ? this.ReadyOutput(readOnlyList3, text2, null) : null),
        Files = readOnlyList3,
        IsStopped = (clickyJobRailPhase == ClickyJobRailPhase.Stopped)
      };
    }

    // Token: 0x060004B9 RID: 1209 RVA: 0x0052A6A8 File Offset: 0x0052A6A8
    [return: Nullable(2)]
    private NotchUpdateContent ClickyContent(string slug)
    {
      AgentTaskDetail agentTaskDetail = (from t in this.agents.Tasks
        select this.agents.GetTaskDetail(t.TaskId) into d
        where ((d != null) ? d.ClickySlug : null) == slug
        orderby d.Snapshot.StartedAt descending
        select d).FirstOrDefault<AgentTaskDetail>();
      if (agentTaskDetail == null)
      {
        return null;
      }
      string text = (agentTaskDetail.DoneTitle ?? agentTaskDetail.Snapshot.Title).Trim();
      bool flag;
      if (agentTaskDetail.Snapshot.Status == AgentTaskStatus.Finished)
      {
        OneBrainJobReports oneBrainJobReports = this.reports();
        flag = oneBrainJobReports == null || !oneBrainJobReports.IsPresenting(agentTaskDetail.Snapshot.ThreadId);
      }
      else
      {
        flag = false;
      }
      bool flag2 = flag;
      IReadOnlyList<string> readOnlyList2;
      if (!flag2)
      {
        IReadOnlyList<string> readOnlyList = Array.Empty<string>();
        readOnlyList2 = readOnlyList;
      }
      else
      {
        readOnlyList2 = agentTaskDetail.Artifacts;
      }
      IReadOnlyList<string> readOnlyList3 = readOnlyList2;
      ClickyAnswer clickyAnswer;
      if (!flag2)
      {
        clickyAnswer = null;
      }
      else
      {
        string threadId = agentTaskDetail.Snapshot.ThreadId;
        clickyAnswer = ((threadId != null) ? this.agents.ResultCard(threadId) : null) ?? agentTaskDetail.Answer;
      }
      ClickyAnswer clickyAnswer2 = clickyAnswer;
      return new NotchUpdateContent
      {
        RailKey = slug,
        ClickySlug = slug,
        ThreadId = agentTaskDetail.Snapshot.ThreadId,
        Title = text,
        Summary = (flag2 ? (((clickyAnswer2 != null) ? clickyAnswer2.Line : null) ?? agentTaskDetail.Summary) : null),
        Request = this.TaskRequest(agentTaskDetail, text, null),
        ReadyOutput = (flag2 ? this.ReadyOutput(readOnlyList3, agentTaskDetail.Snapshot.ThreadId, slug) : null),
        Files = readOnlyList3,
        IsStopped = (agentTaskDetail.Snapshot.Status == AgentTaskStatus.Stopped)
      };
    }

    // Token: 0x060004BA RID: 1210 RVA: 0x0052A86C File Offset: 0x0052A86C
    [NullableContext(2)]
    private NotchUpdateRequest TaskRequest(AgentTaskDetail task, [Nullable(1)] string taskTitle, string jobId)
    {
      if (task == null)
      {
        return null;
      }
      string text = taskTitle.Trim();
      string threadId = task.Snapshot.ThreadId;
      string taskId = task.Snapshot.TaskId;
      string text2 = ((jobId != null) ? ("job:" + jobId) : (task.ClickySlug ?? ""));
      string text3 = this.agents.PendingComputerUseRequest(taskId);
      if (text3 != null)
      {
        CardFooterRequest cardFooterRequest = new CardFooterRequest(CardFooterRequest.Kinds.ComputerUse, this.agents.PendingComputerUseSummary(taskId) ?? ClickyNotes.SpeakableSentence(text3, 110), null);
        return new NotchUpdateRequest(cardFooterRequest.Title, cardFooterRequest.Detail, new <>z__ReadOnlyArray<NotchUpdateButton>(new NotchUpdateButton[]
        {
          new NotchUpdateButton("Let HeyClicky drive", true, this.Answered("drive_approved", text2, taskId, delegate
          {
            ArcDock dock = this.host.Dock;
            if (dock == null)
            {
              return;
            }
            dock.LetClickyDrive(taskId);
          })),
          new NotchUpdateButton("Not now", false, this.Answered("drive_declined", text2, taskId, delegate
          {
            ArcDock dock2 = this.host.Dock;
            if (dock2 != null)
            {
              dock2.DeclineComputerUse(taskId);
              return;
            }
            this.agents.SetPendingComputerUseRequest(taskId, null);
          }))
        }), false);
      }
      if (task.RequiresExtraUsageApproval)
      {
        return new NotchUpdateRequest("Needs more agent messages", (text.Length > 0) ? (text + " is a big one. Let it keep going?") : "This one's big. Let it keep going?", new <>z__ReadOnlyArray<NotchUpdateButton>(new NotchUpdateButton[]
        {
          new NotchUpdateButton("Always approve", true, this.Answered("extra_usage_always_approve", text2, taskId, delegate
          {
            WorkNotchUpdates.ApproveExtraUsageAsync(this.agents, task.Snapshot.TaskId, true);
          })),
          new NotchUpdateButton("Approve once", false, this.Answered("extra_usage_approve_once", text2, taskId, delegate
          {
            WorkNotchUpdates.ApproveExtraUsageAsync(this.agents, task.Snapshot.TaskId, false);
          }))
        }), false);
      }
      if (threadId != null && this.agents.NeedsChromeConnection(threadId))
      {
        return this.ConnectChrome(text, jobId, threadId, text2, taskId);
      }
      if (task.Snapshot.Status == AgentTaskStatus.Finished)
      {
        ClickyJobUserStep pendingUserStep = task.PendingUserStep;
        if (pendingUserStep != null)
        {
          return this.UserStep(pendingUserStep.Step, jobId, taskId);
        }
      }
      if (task.Snapshot.Status == AgentTaskStatus.Failed)
      {
        return new NotchUpdateRequest((text.Length > 0) ? ("Didn't finish " + text) : "Didn't finish", WorkNotchUpdates.FirstLine(task.FailureMessage) ?? "Something went wrong partway through.", new <>z__ReadOnlySingleElementList<NotchUpdateButton>(new NotchUpdateButton("Retry", true, this.Answered("retry", text2, taskId, delegate
        {
          this.Retry(task, jobId);
        }))), false);
      }
      if (task.Snapshot.Status == AgentTaskStatus.Stopped)
      {
        return new NotchUpdateRequest("Interrupted", WorkNotchUpdates.StoppedDetail(task.FailureMessage), new <>z__ReadOnlySingleElementList<NotchUpdateButton>(new NotchUpdateButton("Retry", true, this.Answered("retry", text2, taskId, delegate
        {
          this.Retry(task, jobId);
        }))), false);
      }
      if (task.Snapshot.Status == AgentTaskStatus.WaitingForUser && jobId == null)
      {
        return new NotchUpdateRequest("Your turn", task.Summary ?? task.ProgressSummary, new <>z__ReadOnlySingleElementList<NotchUpdateButton>(new NotchUpdateButton("Show in chat", true, delegate
        {
          this.host.ShowInChat(null, task.ClickySlug);
        })), false);
      }
      return null;
    }

    // Token: 0x060004BB RID: 1211 RVA: 0x0052ABEC File Offset: 0x0052ABEC
    [NullableContext(2)]
    private NotchUpdateRequest JobRequest([Nullable(1)] ClickyJob job, ClickyJobRailPhase phase, AgentTaskDetail task)
    {
      string text = ((task != null) ? task.Snapshot.TaskId : null);
      if (job.IsHeldForChrome)
      {
        return this.ConnectChrome(job.Title.Trim(), job.Id, job.ThreadId, "job:" + job.Id, text);
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
        return this.UserStep(waitingStep.Step, job.Id, text);
      }
      if (task != null || job.ThreadId == null)
      {
        return null;
      }
      string text2 = "job:" + job.Id;
      NotchUpdateRequest notchUpdateRequest;
      if (phase != ClickyJobRailPhase.Failed)
      {
        if (phase != ClickyJobRailPhase.Stopped)
        {
          notchUpdateRequest = null;
        }
        else
        {
          notchUpdateRequest = new NotchUpdateRequest("Interrupted", WorkNotchUpdates.StoppedDetail(null), new <>z__ReadOnlySingleElementList<NotchUpdateButton>(new NotchUpdateButton("Retry", true, this.Answered("retry", text2, null, delegate
          {
            this.RetryJob(job);
          }))), false);
        }
      }
      else
      {
        notchUpdateRequest = new NotchUpdateRequest((job.Title.Trim().Length > 0) ? ("Didn't finish " + job.Title.Trim()) : "Didn't finish", WorkNotchUpdates.FirstLine(job.Summary) ?? "Something went wrong partway through.", new <>z__ReadOnlySingleElementList<NotchUpdateButton>(new NotchUpdateButton("Retry", true, this.Answered("retry", text2, null, delegate
        {
          this.RetryJob(job);
        }))), false);
      }
      return notchUpdateRequest;
    }

    // Token: 0x060004BC RID: 1212 RVA: 0x0052ADB8 File Offset: 0x0052ADB8
    private Action Answered(string answer, string railKey, [Nullable(2)] string taskId, Action action)
    {
      return delegate
      {
        ArcDock.TrackRequestAnswered(this.agents, answer, "notch_card", railKey, taskId);
        action();
      };
    }

    // Token: 0x060004BD RID: 1213 RVA: 0x0052ADF0 File Offset: 0x0052ADF0
    private NotchUpdateRequest ConnectChrome(string title, [Nullable(2)] string jobId, [Nullable(2)] string threadId, string railKey, [Nullable(2)] string taskId)
    {
      ChromeSetupFlow flow = ChromeSetupFlow.Shared;
      ChromeSetupFlow.Target target = new ChromeSetupFlow.Target(jobId, threadId);
      List<NotchUpdateButton> list = new List<NotchUpdateButton>();
      string browserName = flow.BrowserName;
      if (flow.CanDoItForThem)
      {
        list.Add(new NotchUpdateButton("Get new extension", true, this.Answered("chrome_do_it_for_me", railKey, taskId, delegate
        {
          flow.Choose(ChromeSetupFlow.Choice.DoItForMe, target);
        })));
      }
      list.Add(new NotchUpdateButton(flow.CanDoItForThem ? "I'll do it" : (flow.AddsAgain ? "Show me how" : ("Add to " + browserName)), !flow.CanDoItForThem, this.Answered("chrome_show_me_how", railKey, taskId, delegate
      {
        flow.Choose(ChromeSetupFlow.Choice.ShowMeHow, target);
      })));
      list.Add(new NotchUpdateButton("Not now", false, this.Answered("chrome_not_now", railKey, taskId, delegate
      {
        flow.Choose(ChromeSetupFlow.Choice.NotNow, target);
      })));
      string text;
      if (!flow.AddsAgain)
      {
        if (title.Length <= 0)
        {
          text = CardFooterRequest.ConnectChromeDetail(browserName, false);
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(75, 2);
          defaultInterpolatedStringHandler.AppendLiteral("To finish “");
          defaultInterpolatedStringHandler.AppendFormatted(title);
          defaultInterpolatedStringHandler.AppendLiteral("”, HeyClicky's extension goes in your ");
          defaultInterpolatedStringHandler.AppendFormatted(browserName);
          defaultInterpolatedStringHandler.AppendLiteral(" once, a couple of clicks.");
          text = defaultInterpolatedStringHandler.ToStringAndClear();
        }
      }
      else
      {
        text = ((title.Length > 0) ? ("To finish “" + title + "”, swap in the new extension. It updates itself, so this is the last time you'll do this.") : CardFooterRequest.ConnectChromeDetail(browserName, true));
      }
      string text2 = text;
      return new NotchUpdateRequest(CardFooterRequest.ConnectChromeTitle(browserName, flow.AddsAgain), text2, list, true);
    }

    // Token: 0x060004BE RID: 1214 RVA: 0x0052AF9C File Offset: 0x0052AF9C
    private NotchUpdateRequest UserStep(string step, [Nullable(2)] string jobId, [Nullable(2)] string taskId)
    {
      string text = "Your turn";
      IReadOnlyList<NotchUpdateButton> readOnlyList2;
      if (jobId != null)
      {
        IReadOnlyList<NotchUpdateButton> readOnlyList = new <>z__ReadOnlySingleElementList<NotchUpdateButton>(new NotchUpdateButton("I'm done", true, this.Answered("user_step_done", "job:" + jobId, taskId, delegate
        {
          this.agents.ContinueJobAfterUserStep(jobId, null);
        })));
        readOnlyList2 = readOnlyList;
      }
      else
      {
        IReadOnlyList<NotchUpdateButton> readOnlyList = Array.Empty<NotchUpdateButton>();
        readOnlyList2 = readOnlyList;
      }
      return new NotchUpdateRequest(text, step, readOnlyList2, false);
    }

    // Token: 0x060004BF RID: 1215 RVA: 0x0052B014 File Offset: 0x0052B014
    private static async Task ApproveExtraUsageAsync(AgentService agents, string taskId, bool asManyAsNeeded)
    {
      try
      {
        await agents.ApproveExtraUsageAsync(taskId, asManyAsNeeded, default(CancellationToken), true);
      }
      catch (Exception ex)
      {
        Log.Warn("work", "extra usage approval: " + ex.Message);
      }
    }

    // Token: 0x060004C0 RID: 1216 RVA: 0x0052B068 File Offset: 0x0052B068
    private static string StoppedDetail([Nullable(2)] string reason)
    {
      if (((reason != null) ? reason.Trim() : null) == "Stopped by you.")
      {
        return "You stopped it before it finished.";
      }
      return WorkNotchUpdates.FirstLine(reason) ?? "It was cut off before it finished.";
    }

    // Token: 0x060004C1 RID: 1217 RVA: 0x0052B098 File Offset: 0x0052B098
    [NullableContext(2)]
    private NotchUpdateReadyOutput ReadyOutput([Nullable(1)] IReadOnlyList<string> files, string threadId, string slug)
    {
      if (files.Count == 0)
      {
        return null;
      }
      string first = files[0];
      bool flag = ArtifactPaths.IsWebLink(first);
      NotchDocumentStyle notchDocumentStyle = NotchDocumentStyle.For(first, flag);
      return new NotchUpdateReadyOutput(flag ? "Open" : ("Open " + notchDocumentStyle.KindLabel), notchDocumentStyle.Glyph, files.Count > 1, flag, null, delegate
      {
        this.host.OpenChatArtifact(first, threadId, slug);
      });
    }

    // Token: 0x060004C2 RID: 1218 RVA: 0x0052B12C File Offset: 0x0052B12C
    private void Retry(AgentTaskDetail task, [Nullable(2)] string jobId)
    {
      string text = task.UserPrompts.LastOrDefault<string>();
      if (text == null || text.Length <= 0)
      {
        return;
      }
      if (jobId != null)
      {
        this.agents.LaunchAsync(new AgentLaunchRequest
        {
          Prompt = text,
          JobId = jobId,
          TargetThreadId = task.Snapshot.ThreadId,
          LaunchSource = "retry",
          IsGeneratedTaskBrief = true
        }, default(CancellationToken));
        return;
      }
      string clickySlug = task.ClickySlug;
      if (clickySlug != null)
      {
        this.agents.LaunchAsync(new AgentLaunchRequest
        {
          Prompt = text,
          ClickySlug = clickySlug,
          LaunchSource = "retry",
          IsGeneratedTaskBrief = true
        }, default(CancellationToken));
      }
    }

    // Token: 0x060004C3 RID: 1219 RVA: 0x0052B1E8 File Offset: 0x0052B1E8
    private void RetryJob(ClickyJob job)
    {
      IReadOnlyList<ClickyJobRun> runs = job.Runs;
      string text;
      if (runs == null)
      {
        text = null;
      }
      else
      {
        ClickyJobRun clickyJobRun = runs.LastOrDefault<ClickyJobRun>();
        text = ((clickyJobRun != null) ? clickyJobRun.Ask : null);
      }
      string text2 = text;
      string text3 = ((text2 != null && text2.Length > 0) ? text2 : job.Prompt);
      this.agents.LaunchAsync(new AgentLaunchRequest
      {
        Prompt = text3,
        JobId = job.Id,
        TargetThreadId = job.ThreadId,
        LaunchSource = "retry",
        IsGeneratedTaskBrief = true
      }, default(CancellationToken));
    }

    // Token: 0x060004C4 RID: 1220 RVA: 0x0052B278 File Offset: 0x0052B278
    [NullableContext(2)]
    private static string FirstLine(string text)
    {
      if (text == null)
      {
        return null;
      }
      return (from l in text.Split('\n', StringSplitOptions.RemoveEmptyEntries)
        select l.Trim()).FirstOrDefault<string>((string l) => l.Length > 0);
    }

    // Token: 0x040000EC RID: 236
    private readonly AgentService agents;

    // Token: 0x040000ED RID: 237
    [Nullable(new byte[] { 1, 2 })]
    private readonly Func<OneBrainJobReports> reports;

    // Token: 0x040000EE RID: 238
    private readonly AppHost host;
  }
}
