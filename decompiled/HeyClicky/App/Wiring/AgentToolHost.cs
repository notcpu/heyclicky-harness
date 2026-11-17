using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Portraits;
using HeyClicky.App.Proactive;
using HeyClicky.Core.Proactive;
using HeyClicky.Core.Tools;

namespace HeyClicky.App.Wiring
{
  // Token: 0x02000091 RID: 145
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentToolHost : IAgentToolHost
  {
    // Token: 0x06000596 RID: 1430 RVA: 0x0052D1E8 File Offset: 0x0052D1E8
    public AgentToolHost(AgentService agents, [Nullable(new byte[] { 1, 2 })] Func<string, bool> openHome, Func<bool> closeHome)
    {
      this.agents = agents;
      this.openHome = openHome;
      this.closeHome = closeHome;
    }

    // Token: 0x06000597 RID: 1431 RVA: 0x0052D208 File Offset: 0x0052D208
    private static AgentClickyInfo Info(ClickyInfo c)
    {
      return new AgentClickyInfo(c.Slug, c.Name, c.RoleText, c.OneLiner, c.ThreadId, c.IsArchived);
    }

    // Token: 0x170000D3 RID: 211
    // (get) Token: 0x06000598 RID: 1432 RVA: 0x0052D234 File Offset: 0x0052D234
    public IReadOnlyList<AgentClickyInfo> Clickys
    {
      get
      {
        IEnumerable<ClickyInfo> clickys = this.agents.Clickys;
        Func<ClickyInfo, AgentClickyInfo> func;
        if ((func = AgentToolHost.<>O.<0>__Info) == null)
        {
          func = (AgentToolHost.<>O.<0>__Info = new Func<ClickyInfo, AgentClickyInfo>(AgentToolHost.Info));
        }
        return clickys.Select<ClickyInfo, AgentClickyInfo>(func).ToList<AgentClickyInfo>();
      }
    }

    // Token: 0x06000599 RID: 1433 RVA: 0x0052D268 File Offset: 0x0052D268
    [return: Nullable(2)]
    public AgentClickyInfo FindClicky(string slug)
    {
      ClickyInfo clickyInfo = this.agents.FindClicky(slug);
      if (clickyInfo == null)
      {
        return null;
      }
      return AgentToolHost.Info(clickyInfo);
    }

    // Token: 0x170000D4 RID: 212
    // (get) Token: 0x0600059A RID: 1434 RVA: 0x0052D290 File Offset: 0x0052D290
    public IReadOnlyList<AgentTaskInfo> Tasks
    {
      get
      {
        return (from t in this.agents.Tasks
          select this.agents.GetTaskDetail(t.TaskId) into d
          where d != null
          select d).Select<AgentTaskDetail, AgentTaskInfo>(delegate(AgentTaskDetail d)
        {
          AgentTaskInfo agentTaskInfo = AgentToolHost.TaskInfo(d).<Clone>$();
          agentTaskInfo.PendingComputerUseRequest = this.agents.PendingComputerUseRequest(d.Snapshot.TaskId);
          agentTaskInfo.IsDemo = d.IsDemo;
          return agentTaskInfo;
        }).ToList<AgentTaskInfo>();
      }
    }

    // Token: 0x0600059B RID: 1435 RVA: 0x0052D2F4 File Offset: 0x0052D2F4
    private static AgentTaskInfo TaskInfo(AgentTaskDetail d)
    {
      return new AgentTaskInfo(d.Snapshot.TaskId, d.Snapshot.ThreadId, d.Snapshot.Title, d.Snapshot.Status, d.ClickySlug, d.ClickyName, d.ProgressSummary, d.Summary, d.Artifacts, d.RequiresExtraUsageApproval, d.UserPrompts, (from a in d.Activity
        where a.Kind == AgentActivityKind.Commentary
        select a.Text).ToList<string>(), d.StreamingResponse, d.WorkspacePath, d.Snapshot.StartedAt);
    }

    // Token: 0x0600059C RID: 1436 RVA: 0x0052D3C8 File Offset: 0x0052D3C8
    [return: Nullable(2)]
    public AgentJobInfo FindJob(string threadId)
    {
      ClickyJob clickyJob = this.agents.Jobs.JobForThread(threadId);
      if (clickyJob == null)
      {
        return null;
      }
      string text;
      switch (this.agents.RailPhase(clickyJob))
      {
      case ClickyJobRailPhase.Queued:
        text = "starting";
        break;
      case ClickyJobRailPhase.Working:
        text = "working";
        break;
      case ClickyJobRailPhase.NeedsYou:
        text = "waiting on the user";
        break;
      case ClickyJobRailPhase.Done:
        text = "done";
        break;
      case ClickyJobRailPhase.Failed:
        text = "failed";
        break;
      default:
        text = "stopped";
        break;
      }
      string text2 = text;
      string text3 = this.agents.LiveTaskId(clickyJob.Id);
      AgentTaskDetail agentTaskDetail = ((text3 == null) ? null : this.agents.GetTaskDetail(text3));
      List<AgentJobRunInfo> list = (clickyJob.Runs ?? Array.Empty<ClickyJobRun>()).Select<ClickyJobRun, AgentJobRunInfo>((ClickyJobRun r) => new AgentJobRunInfo(r.Ask, r.StartedAt, AgentToolHost.<FindJob>g__Outcome|11_0(r.Outcome), r.Summary)).ToList<AgentJobRunInfo>();
      string text4 = ((text3 == null) ? null : this.agents.FullReplyText(text3));
      string text5 = ((text4 != null && text4.Length > 0) ? text4 : (clickyJob.ReplyText ?? ""));
      return new AgentJobInfo(clickyJob.ThreadId, clickyJob.Title, text2, clickyJob.FolderPath, list, text5, clickyJob.Summary, clickyJob.ArtifactEntries, (agentTaskDetail == null) ? null : AgentToolHost.TaskInfo(agentTaskDetail));
    }

    // Token: 0x0600059D RID: 1437 RVA: 0x0052D518 File Offset: 0x0052D518
    [return: Nullable(2)]
    public string ClickySlugForThread(string threadId)
    {
      ClickyInfo clickyInfo = this.agents.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo c) => c.ThreadId == threadId);
      string text;
      if ((text = ((clickyInfo != null) ? clickyInfo.Slug : null)) == null)
      {
        AgentTaskInfo agentTaskInfo = this.Tasks.FirstOrDefault<AgentTaskInfo>((AgentTaskInfo t) => t.ThreadId == threadId && t.ClickySlug != null);
        if (agentTaskInfo == null)
        {
          return null;
        }
        text = agentTaskInfo.ClickySlug;
      }
      return text;
    }

    // Token: 0x170000D5 RID: 213
    // (get) Token: 0x0600059E RID: 1438 RVA: 0x0052D580 File Offset: 0x0052D580
    public string ProjectsRoot
    {
      get
      {
        return this.agents.ProjectsRoot;
      }
    }

    // Token: 0x0600059F RID: 1439 RVA: 0x0052D590 File Offset: 0x0052D590
    [return: Nullable(2)]
    public string WorkspacePath(string clickySlug)
    {
      return this.agents.WorkspacePath(clickySlug);
    }

    // Token: 0x170000D6 RID: 214
    // (get) Token: 0x060005A0 RID: 1440 RVA: 0x0052D5A0 File Offset: 0x0052D5A0
    // (set) Token: 0x060005A1 RID: 1441 RVA: 0x0052D5A8 File Offset: 0x0052D5A8
    [Nullable(new byte[] { 2, 1, 1 })]
    public static Action<AgentToolLaunch, string> Launched
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x170000D7 RID: 215
    // (get) Token: 0x060005A2 RID: 1442 RVA: 0x0052D5B0 File Offset: 0x0052D5B0
    // (set) Token: 0x060005A3 RID: 1443 RVA: 0x0052D5B8 File Offset: 0x0052D5B8
    [Nullable(new byte[] { 2, 1, 1 })]
    public static Action<string, string> Steered
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x060005A4 RID: 1444 RVA: 0x0052D5C0 File Offset: 0x0052D5C0
    public async Task<string> LaunchAsync(AgentToolLaunch launch, CancellationToken ct)
    {
      bool withoutChrome = launch.ChromeChoice == "without";
      string text = await this.agents.LaunchAsync(new AgentLaunchRequest
      {
        Prompt = launch.Prompt,
        ClickySlug = launch.ClickySlug,
        NewClickyName = launch.NewClickyName,
        NewClickyRole = launch.NewClickyRole,
        NewClickyOneLiner = launch.NewClickyOneLiner,
        LaunchContext = (withoutChrome ? (launch.LaunchContext + " They chose not to connect Chrome for this job: don't ask about Chrome again. For a step on a website they're signed into, call mac_step and HeyClicky does it on their screen while they watch.") : launch.LaunchContext),
        LaunchSource = launch.LaunchSource,
        ImagesJpeg = launch.ImagesJpeg,
        AttachedFilePaths = launch.AttachedFilePaths,
        UsageContext = launch.UsageContext,
        IsGeneratedTaskBrief = launch.IsGeneratedTaskBrief,
        BuildsOnJobIds = launch.BuildsOnJobIds
      }, ct).ConfigureAwait(false);
      if (withoutChrome)
      {
        AgentTaskDetail taskDetail = this.agents.GetTaskDetail(text);
        string text2 = ((taskDetail != null) ? taskDetail.JobId : null);
        if (text2 != null)
        {
          this.agents.Jobs.SetSkipsChrome(text2);
        }
      }
      try
      {
        Action<AgentToolLaunch, string> launched = AgentToolHost.Launched;
        if (launched != null)
        {
          launched(launch, text);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Tools", "launch note: " + ex.Message);
      }
      return text;
    }

    // Token: 0x060005A5 RID: 1445 RVA: 0x0052D614 File Offset: 0x0052D614
    public Task SteerAsync(string taskId, string text, CancellationToken ct)
    {
      return this.SteerAsync(taskId, text, null, ct);
    }

    // Token: 0x060005A6 RID: 1446 RVA: 0x0052D620 File Offset: 0x0052D620
    public async Task SteerAsync(string taskId, string text, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<byte[]> imagesJpeg, CancellationToken ct)
    {
      await this.agents.SteerAsync(taskId, text, imagesJpeg, ct, true).ConfigureAwait(false);
      try
      {
        Action<string, string> steered = AgentToolHost.Steered;
        if (steered != null)
        {
          steered(taskId, text);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Tools", "steer note: " + ex.Message);
      }
    }

    // Token: 0x060005A7 RID: 1447 RVA: 0x0052D684 File Offset: 0x0052D684
    public string OpenThread(string threadId)
    {
      return this.agents.OpenThread(threadId);
    }

    // Token: 0x060005A8 RID: 1448 RVA: 0x0052D694 File Offset: 0x0052D694
    public Task StopAsync(string taskId)
    {
      return this.agents.StopAsync(taskId);
    }

    // Token: 0x060005A9 RID: 1449 RVA: 0x0052D6A4 File Offset: 0x0052D6A4
    public Task ApproveExtraUsageAsync(string taskId, bool asManyAsNeeded, CancellationToken ct)
    {
      return this.agents.ApproveExtraUsageAsync(taskId, asManyAsNeeded, ct, true);
    }

    // Token: 0x060005AA RID: 1450 RVA: 0x0052D6B8 File Offset: 0x0052D6B8
    public AgentClickyInfo CreateClicky(string name, string roleText, string oneLiner)
    {
      ClickyInfo clickyInfo = this.agents.CreateClicky(name, roleText, oneLiner);
      ClickyRoster.Shared.TrackCreated(clickyInfo.Slug, "agent", 0);
      return AgentToolHost.Info(clickyInfo);
    }

    // Token: 0x060005AB RID: 1451 RVA: 0x0052D6F0 File Offset: 0x0052D6F0
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<string> HandleSuggestionActionAsync(string task)
    {
      SuggestionVoiceAction.Request action = SuggestionVoiceAction.Parse(task);
      string text;
      if (action.Kind == SuggestionVoiceAction.Kind.NotSuggestionAction)
      {
        text = null;
      }
      else
      {
        text = (await ProactiveHooks.RunOnUiAsync<string>((ProactiveCoordinator c) => c.HandleVoiceAction(action)).ConfigureAwait(false)) ?? "{\"status\":\"error\",\"message\":\"Suggestions aren't available right now.\"}";
      }
      return text;
    }

    // Token: 0x060005AC RID: 1452 RVA: 0x0052D734 File Offset: 0x0052D734
    [return: TupleElementNames(new string[] { "Role", "Text" })]
    [return: Nullable(new byte[] { 1, 1, 0, 1, 1 })]
    public async Task<IReadOnlyList<ValueTuple<string, string>>> RecentMessagesAsync(string threadId, int limit, CancellationToken ct)
    {
      IEnumerable<AgentTranscriptTurn> enumerable = await this.agents.ReadThreadAsync(threadId, ct).ConfigureAwait(false);
      List<ValueTuple<string, string>> list = new List<ValueTuple<string, string>>();
      foreach (AgentTranscriptTurn agentTranscriptTurn in enumerable)
      {
        foreach (string text in agentTranscriptTurn.UserMessages)
        {
          if (!string.IsNullOrWhiteSpace(text))
          {
            list.Add(new ValueTuple<string, string>("user", text));
          }
        }
        foreach (string text2 in agentTranscriptTurn.AssistantMessages)
        {
          if (!string.IsNullOrWhiteSpace(text2))
          {
            list.Add(new ValueTuple<string, string>("clicky", text2));
          }
        }
      }
      return list.TakeLast<ValueTuple<string, string>>(limit).ToList<ValueTuple<string, string>>();
    }

    // Token: 0x060005AD RID: 1453 RVA: 0x0052D790 File Offset: 0x0052D790
    public async Task<IReadOnlyList<AgentThreadInfo>> HistoryAsync(int limit, CancellationToken ct)
    {
      ConfiguredTaskAwaitable<ValueTuple<IReadOnlyList<AgentThreadSummary>, string>>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.agents.ListThreadsAsync(limit, null, null, ct).ConfigureAwait(false).GetAwaiter();
      if (!configuredTaskAwaiter.IsCompleted)
      {
        await configuredTaskAwaiter;
        ConfiguredTaskAwaitable<ValueTuple<IReadOnlyList<AgentThreadSummary>, string>>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
        configuredTaskAwaiter = configuredTaskAwaiter2;
        configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<ValueTuple<IReadOnlyList<AgentThreadSummary>, string>>.ConfiguredTaskAwaiter);
      }
      return configuredTaskAwaiter.GetResult().Item1.Select<AgentThreadSummary, AgentThreadInfo>((AgentThreadSummary t) => new AgentThreadInfo(t.ThreadId, t.Name, t.Preview, t.UpdatedAt, t.Status, t.ClickySlug)).ToList<AgentThreadInfo>();
    }

    // Token: 0x060005AE RID: 1454 RVA: 0x0052D7E4 File Offset: 0x0052D7E4
    [NullableContext(2)]
    public bool OpenHome(string threadId)
    {
      string text;
      if (threadId != null)
      {
        AgentTaskSnapshot agentTaskSnapshot = this.agents.Tasks.FirstOrDefault<AgentTaskSnapshot>((AgentTaskSnapshot t) => t.ThreadId == threadId);
        text = ((agentTaskSnapshot != null) ? agentTaskSnapshot.TaskId : null);
      }
      else
      {
        text = null;
      }
      string text2 = text;
      return this.openHome(text2);
    }

    // Token: 0x060005AF RID: 1455 RVA: 0x0052D840 File Offset: 0x0052D840
    public bool CloseHome()
    {
      return this.closeHome();
    }

    // Token: 0x060005B0 RID: 1456 RVA: 0x0052D850 File Offset: 0x0052D850
    public bool OpenPath(string pathOrUrl)
    {
      bool flag;
      try
      {
        ShellOpen.Start(pathOrUrl);
        flag = true;
      }
      catch (Exception ex)
      {
        Log.Warn("Tools", "open " + pathOrUrl + ": " + ex.Message);
        flag = false;
      }
      return flag;
    }

    // Token: 0x060005B1 RID: 1457 RVA: 0x0052D8A0 File Offset: 0x0052D8A0
    public bool RevealPath(string path)
    {
      bool flag;
      try
      {
        Process.Start(new ProcessStartInfo("explorer.exe", "/select,\"" + path + "\"")
        {
          UseShellExecute = false
        });
        flag = true;
      }
      catch (Exception ex)
      {
        Log.Warn("Tools", "reveal " + path + ": " + ex.Message);
        flag = false;
      }
      return flag;
    }

    // Token: 0x060005B2 RID: 1458 RVA: 0x0052D910 File Offset: 0x0052D910
    public bool SkipOnboardingInterview()
    {
      Func<bool> skipInterview = FlowHooks.SkipInterview;
      return skipInterview != null && skipInterview();
    }

    // Token: 0x170000D8 RID: 216
    // (get) Token: 0x060005B3 RID: 1459 RVA: 0x0052D924 File Offset: 0x0052D924
    // (set) Token: 0x060005B4 RID: 1460 RVA: 0x0052D92C File Offset: 0x0052D92C
    [Nullable(new byte[] { 2, 1 })]
    public Action<string> ApproveComputerUseHook
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170000D9 RID: 217
    // (get) Token: 0x060005B5 RID: 1461 RVA: 0x0052D938 File Offset: 0x0052D938
    // (set) Token: 0x060005B6 RID: 1462 RVA: 0x0052D940 File Offset: 0x0052D940
    [Nullable(new byte[] { 2, 1 })]
    public Action<string> DeclineComputerUseHook
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x060005B7 RID: 1463 RVA: 0x0052D94C File Offset: 0x0052D94C
    public void ApproveComputerUse(string taskId)
    {
      Action<string> approveComputerUseHook = this.ApproveComputerUseHook;
      if (approveComputerUseHook == null)
      {
        return;
      }
      approveComputerUseHook(taskId);
    }

    // Token: 0x060005B8 RID: 1464 RVA: 0x0052D960 File Offset: 0x0052D960
    public void DeclineComputerUse(string taskId)
    {
      Action<string> declineComputerUseHook = this.DeclineComputerUseHook;
      if (declineComputerUseHook == null)
      {
        return;
      }
      declineComputerUseHook(taskId);
    }

    // Token: 0x060005B9 RID: 1465 RVA: 0x0052D974 File Offset: 0x0052D974
    public async Task<string> FindJobsAsync(string query, int limit)
    {
      return ToolResults.Json(await this.agents.FindJobsAsync(query, limit, false, null).ConfigureAwait(false));
    }

    // Token: 0x060005BA RID: 1466 RVA: 0x0052D9C8 File Offset: 0x0052D9C8
    [return: TupleElementNames(new string[] { "IsJob", "ThreadId" })]
    [return: Nullable(new byte[] { 0, 2 })]
    public ValueTuple<bool, string> ThreadIdForJobId(string jobId)
    {
      return this.agents.ThreadIdForOneBrainJobId(jobId);
    }

    // Token: 0x060005BB RID: 1467 RVA: 0x0052D9D8 File Offset: 0x0052D9D8
    public string JobIdForThread(string threadId)
    {
      return this.agents.OneBrainJobId(threadId);
    }

    // Token: 0x060005BC RID: 1468 RVA: 0x0052D9E8 File Offset: 0x0052D9E8
    [NullableContext(2)]
    public string BuildsOnContext([Nullable(1)] IReadOnlyList<string> jobIds, string receivingThreadId)
    {
      ClickyJobStore jobs = this.agents.Jobs;
      string text;
      if (receivingThreadId != null)
      {
        ClickyJob clickyJob = this.agents.Jobs.JobForThread(receivingThreadId);
        text = ((clickyJob != null) ? clickyJob.Id : null);
      }
      else
      {
        text = null;
      }
      return jobs.BuildsOnContext(jobIds, text, new Func<string, ValueTuple<string, string, string>?>(this.agents.EarlierWorkElsewhere));
    }

    // Token: 0x060005BD RID: 1469 RVA: 0x0052DA3C File Offset: 0x0052DA3C
    public void RetitleJob(string threadId, string title)
    {
      ClickyJob clickyJob = this.agents.Jobs.JobForThread(threadId);
      if (clickyJob != null)
      {
        this.agents.Jobs.Retitle(clickyJob.Id, title);
      }
    }

    // Token: 0x060005C0 RID: 1472 RVA: 0x0052DAC4 File Offset: 0x0052DAC4
    [CompilerGenerated]
    internal static string <FindJob>g__Outcome|11_0(ClickyJobOutcome outcome)
    {
      return outcome.ToString().ToLowerInvariant();
    }

    // Token: 0x04000124 RID: 292
    private readonly AgentService agents;

    // Token: 0x04000125 RID: 293
    [Nullable(new byte[] { 1, 2 })]
    private readonly Func<string, bool> openHome;

    // Token: 0x04000126 RID: 294
    private readonly Func<bool> closeHome;

    // Token: 0x0200041B RID: 1051
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040014A6 RID: 5286
      [Nullable(0)]
      public static Func<ClickyInfo, AgentClickyInfo> <0>__Info;
    }
  }
}
