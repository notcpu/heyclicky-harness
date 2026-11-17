using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Routines;

namespace HeyClicky.App.Home
{
  // Token: 0x020002B0 RID: 688
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DemoHomeSource : IHomeSource
  {
    // Token: 0x1400006F RID: 111
    // (add) Token: 0x06001C45 RID: 7237 RVA: 0x005F8ADC File Offset: 0x005F8ADC
    // (remove) Token: 0x06001C46 RID: 7238 RVA: 0x005F8B14 File Offset: 0x005F8B14
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler TasksChanged;

    // Token: 0x14000070 RID: 112
    // (add) Token: 0x06001C47 RID: 7239 RVA: 0x005F8B4C File Offset: 0x005F8B4C
    // (remove) Token: 0x06001C48 RID: 7240 RVA: 0x005F8B84 File Offset: 0x005F8B84
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<AgentTaskEvent> TaskEvent;

    // Token: 0x14000071 RID: 113
    // (add) Token: 0x06001C49 RID: 7241 RVA: 0x005F8BBC File Offset: 0x005F8BBC
    // (remove) Token: 0x06001C4A RID: 7242 RVA: 0x005F8BF4 File Offset: 0x005F8BF4
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler RoutinesChanged;

    // Token: 0x06001C4B RID: 7243 RVA: 0x005F8C2C File Offset: 0x005F8C2C
    public unsafe DemoHomeSource()
    {
      DateTimeOffset now = DateTimeOffset.Now;
      int i = 4;
      List<ClickyInfo> list = new List<ClickyInfo>(i);
      CollectionsMarshal.SetCount<ClickyInfo>(list, i);
      Span<ClickyInfo> span = CollectionsMarshal.AsSpan<ClickyInfo>(list);
      int num = 0;
      *span[num] = new ClickyInfo("trip-planner", "Trip Planner", "Researcher", "Plans trips and compares flights", "t-trip", now.AddDays(-6.0), false);
      num++;
      *span[num] = new ClickyInfo("launch-writer", "Launch Writer", "Writer", "Drafts launch emails and posts", "t-launch", now.AddDays(-3.0), false);
      num++;
      *span[num] = new ClickyInfo("metrics-buddy", "Metrics Buddy", "Analyst", "Summarizes the weekly numbers", "t-metrics", now.AddDays(-9.0), false);
      num++;
      *span[num] = new ClickyInfo("inbox-zero", "Inbox Zero", "Helper", "Keeps your inbox tidy", null, now.AddDays(-1.0), false);
      this.clickys = list;
      num = 3;
      List<AgentThreadSummary> list2 = new List<AgentThreadSummary>(num);
      CollectionsMarshal.SetCount<AgentThreadSummary>(list2, num);
      Span<AgentThreadSummary> span2 = CollectionsMarshal.AsSpan<AgentThreadSummary>(list2);
      i = 0;
      *span2[i] = new AgentThreadSummary("t-trip", "Trip Planner", "Find me flights to Tokyo in March under $900", new DateTimeOffset?(now.AddDays(-6.0)), new DateTimeOffset?(now.AddMinutes(-2.0)), "active", null, "trip-planner");
      i++;
      *span2[i] = new AgentThreadSummary("t-launch", "Launch Writer", "Draft the launch email for the Windows app", new DateTimeOffset?(now.AddDays(-3.0)), new DateTimeOffset?(now.AddHours(-3.0)), "idle", null, "launch-writer");
      i++;
      *span2[i] = new AgentThreadSummary("t-metrics", "Metrics Buddy", "Summarize Q3 metrics", new DateTimeOffset?(now.AddDays(-9.0)), new DateTimeOffset?(now.AddDays(-1.0).AddHours(-2.0)), "idle", null, "metrics-buddy");
      this.threads = list2;
      Directory.CreateDirectory(Path.Combine(DemoHomeSource.Root, "metrics-buddy", "output"));
      foreach (string text in new string[] { "q3-summary.md", "revenue.csv" })
      {
        string text2 = Path.Combine(DemoHomeSource.Root, "metrics-buddy", "output", text);
        if (!File.Exists(text2))
        {
          File.WriteAllText(text2, "demo");
        }
      }
      DemoFiles.WriteChartPng(Path.Combine(DemoHomeSource.Root, "metrics-buddy", "output", "chart.png"));
      DemoFiles.WritePdf(Path.Combine(DemoHomeSource.Root, "metrics-buddy", "output", "q3-report.pdf"), "Q3 at a glance", "Revenue +12% QoQ. Churn 2.1%. NRR 108%.");
      Dictionary<string, List<AgentTranscriptTurn>> dictionary = this.transcripts;
      string text3 = "t-metrics";
      i = 2;
      List<AgentTranscriptTurn> list3 = new List<AgentTranscriptTurn>(i);
      CollectionsMarshal.SetCount<AgentTranscriptTurn>(list3, i);
      Span<AgentTranscriptTurn> span3 = CollectionsMarshal.AsSpan<AgentTranscriptTurn>(list3);
      num = 0;
      *span3[num] = new AgentTranscriptTurn("0", new <>z__ReadOnlySingleElementList<string>("Pull the Q3 dashboard export"), new <>z__ReadOnlySingleElementList<string>("Opening the dashboard export."), Array.Empty<string>(), Array.Empty<string>(), Array.Empty<string>(), "failed", "unexpected status 502 Bad Gateway: {\"error\":\"upstream timed out\"}", new DateTimeOffset?(now.AddDays(-1.0).AddHours(-3.0)), new DateTimeOffset?(now.AddDays(-1.0).AddHours(-3.0).AddSeconds(41.0)));
      num++;
      *span3[num] = new AgentTranscriptTurn("1", new <>z__ReadOnlySingleElementList<string>("Summarize Q3 metrics from the dashboard export"), new <>z__ReadOnlyArray<string>(new string[] { "Reading the export first.", "$ Get-Content -Path revenue.csv -TotalCount 20", "Search: SaaS net revenue retention benchmark", "$ python summarize.py revenue.csv", "*** Add File: output/q3-summary.md", "Writing the summary now." }), new <>z__ReadOnlySingleElementList<string>("## Q3 at a glance\n\nRevenue grew **12%** quarter over quarter, led by the Pro plan.\n\n| Metric | Q2 | Q3 |\n|---|---|---|\n| Revenue | $1.20M | $1.34M |\n| Active users | 41k | 47k |\n\n- Churn fell to **2.1%**\n- Support tickets dropped by a third\n\n```\nnet_revenue_retention: 108%\n```"), new <>z__ReadOnlyArray<string>(new string[] { "Turn this into slides", "Email it to the team" }), new <>z__ReadOnlyArray<string>(new string[]
      {
        Path.Combine(DemoHomeSource.Root, "metrics-buddy", "output", "q3-summary.md"),
        Path.Combine(DemoHomeSource.Root, "metrics-buddy", "output", "q3-report.pdf"),
        Path.Combine(DemoHomeSource.Root, "metrics-buddy", "output", "revenue.csv"),
        Path.Combine(DemoHomeSource.Root, "metrics-buddy", "output", "chart.png")
      }), "completed", null, new DateTimeOffset?(now.AddDays(-1.0).AddHours(-2.0).AddSeconds(-72.0)), new DateTimeOffset?(now.AddDays(-1.0).AddHours(-2.0)));
      dictionary[text3] = list3;
      Dictionary<string, List<AgentTranscriptTurn>> dictionary2 = this.transcripts;
      string text4 = "t-launch";
      num = 1;
      list3 = new List<AgentTranscriptTurn>(num);
      CollectionsMarshal.SetCount<AgentTranscriptTurn>(list3, num);
      span3 = CollectionsMarshal.AsSpan<AgentTranscriptTurn>(list3);
      i = 0;
      *span3[i] = new AgentTranscriptTurn("1", new <>z__ReadOnlySingleElementList<string>("Draft the launch email for the Windows app"), new <>z__ReadOnlyArray<string>(new string[] { "Reading the release notes", "Drafting" }), new <>z__ReadOnlySingleElementList<string>("Here's a draft:\n\n**Subject:** HeyClicky is on Windows\n\nHi there,\n\nHeyClicky now lives in your Windows notch. Hold *Ctrl + Alt* and ask for anything.\n\n1. Talk to it\n2. Watch it draw on your screen\n3. Hand off work to a Clicky"), Array.Empty<string>(), Array.Empty<string>(), "completed", null, null, null);
      dictionary2[text4] = list3;
      Dictionary<string, List<AgentTranscriptTurn>> dictionary3 = this.transcripts;
      string text5 = "t-trip";
      i = 1;
      List<AgentTranscriptTurn> list4 = new List<AgentTranscriptTurn>(i);
      CollectionsMarshal.SetCount<AgentTranscriptTurn>(list4, i);
      span3 = CollectionsMarshal.AsSpan<AgentTranscriptTurn>(list4);
      num = 0;
      *span3[num] = new AgentTranscriptTurn("1", new <>z__ReadOnlySingleElementList<string>("Find me flights to Tokyo in March under $900"), new <>z__ReadOnlySingleElementList<string>("Searching the web"), new <>z__ReadOnlySingleElementList<string>("I found three options under $900. Want me to keep watching prices?"), Array.Empty<string>(), Array.Empty<string>(), "completed", null, null, null);
      dictionary3[text5] = list4;
      this.details["demo-run"] = new AgentTaskDetail(new AgentTaskSnapshot("demo-run", "t-trip", "Trip Planner", AgentTaskStatus.Running, "Comparing three airlines on the March dates", null, now.AddSeconds(-42.0)), "trip-planner", "Trip Planner", Path.Combine(DemoHomeSource.Root, "trip-planner"), "turn-2", "Comparing three airlines", "Searching the web", "", "", new <>z__ReadOnlyArray<AgentActivityItem>(new AgentActivityItem[]
      {
        new AgentActivityItem("a1", AgentActivityKind.Commentary, "Checking ANA, JAL and United for the week of March 9.", null, now.AddSeconds(-30.0)),
        new AgentActivityItem("a2", AgentActivityKind.ToolCall, "Searching the web", new bool?(true), now.AddSeconds(-20.0)),
        new AgentActivityItem("a3", AgentActivityKind.Commentary, "Comparing three airlines on the March dates.", null, now.AddSeconds(-8.0))
      }), new <>z__ReadOnlyArray<string>(new string[] { "Find me flights to Tokyo in March under $900", "Also check nonstop only" }), null, null, Array.Empty<string>(), Array.Empty<string>(), null, null, false, false, null, 0L, 0L, 0L, false, false, "voice", null, null, null, null, false, false, false, null, null, false);
      num = 2;
      List<ClickyRoutine> list5 = new List<ClickyRoutine>(num);
      CollectionsMarshal.SetCount<ClickyRoutine>(list5, num);
      Span<ClickyRoutine> span4 = CollectionsMarshal.AsSpan<ClickyRoutine>(list5);
      i = 0;
      *span4[i] = new ClickyRoutine
      {
        ClickySlug = "metrics-buddy",
        Title = "Monday metrics digest",
        TaskPrompt = "Every Monday morning, summarize last week's numbers and post them to the team.",
        IntervalSeconds = 604800.0,
        NextRunAt = now.AddDays(2.0).Date.AddHours(9.0),
        RunCount = 6,
        LastRunFinishedAt = new DateTimeOffset?(now.AddDays(-5.0))
      };
      i++;
      *span4[i] = new ClickyRoutine
      {
        ClickySlug = "inbox-zero",
        Title = "Tidy the inbox",
        TaskPrompt = "Archive newsletters older than a week.",
        IntervalSeconds = 14400.0,
        IsPaused = true,
        RunCount = 14
      };
      this.routines = list5;
    }

    // Token: 0x170004D5 RID: 1237
    // (get) Token: 0x06001C4C RID: 7244 RVA: 0x005F94E4 File Offset: 0x005F94E4
    public IReadOnlyList<AgentTaskSnapshot> Tasks
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<AgentTaskSnapshot> readOnlyList;
        lock (obj)
        {
          readOnlyList = this.details.Values.Select<AgentTaskDetail, AgentTaskSnapshot>((AgentTaskDetail d) => d.Snapshot).ToList<AgentTaskSnapshot>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x170004D6 RID: 1238
    // (get) Token: 0x06001C4D RID: 7245 RVA: 0x005F9554 File Offset: 0x005F9554
    public IReadOnlyList<ClickyInfo> Clickys
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<ClickyInfo> readOnlyList;
        lock (obj)
        {
          readOnlyList = this.clickys.ToList<ClickyInfo>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x170004D7 RID: 1239
    // (get) Token: 0x06001C4E RID: 7246 RVA: 0x005F959C File Offset: 0x005F959C
    public IReadOnlyList<ClickyRoutine> Routines
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<ClickyRoutine> readOnlyList;
        lock (obj)
        {
          readOnlyList = this.routines.ToList<ClickyRoutine>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x06001C4F RID: 7247 RVA: 0x005F95E4 File Offset: 0x005F95E4
    [return: Nullable(2)]
    public AgentTaskDetail GetTaskDetail(string taskId)
    {
      object obj = this.gate;
      AgentTaskDetail valueOrDefault;
      lock (obj)
      {
        valueOrDefault = this.details.GetValueOrDefault(taskId);
      }
      return valueOrDefault;
    }

    // Token: 0x06001C50 RID: 7248 RVA: 0x005F962C File Offset: 0x005F962C
    public Task<IReadOnlyList<AgentThreadSummary>> ListThreadsAsync(CancellationToken ct = default(CancellationToken))
    {
      object obj = this.gate;
      Task<IReadOnlyList<AgentThreadSummary>> task;
      lock (obj)
      {
        task = Task.FromResult<IReadOnlyList<AgentThreadSummary>>(this.threads.ToList<AgentThreadSummary>());
      }
      return task;
    }

    // Token: 0x06001C51 RID: 7249 RVA: 0x005F9678 File Offset: 0x005F9678
    public Task<IReadOnlyList<AgentTranscriptTurn>> ReadThreadAsync(string threadId, CancellationToken ct = default(CancellationToken))
    {
      object obj = this.gate;
      Task<IReadOnlyList<AgentTranscriptTurn>> task;
      lock (obj)
      {
        List<AgentTranscriptTurn> valueOrDefault = this.transcripts.GetValueOrDefault(threadId);
        task = Task.FromResult<IReadOnlyList<AgentTranscriptTurn>>(((valueOrDefault != null) ? valueOrDefault.ToList<AgentTranscriptTurn>() : null) ?? new List<AgentTranscriptTurn>());
      }
      return task;
    }

    // Token: 0x06001C52 RID: 7250 RVA: 0x005F96DC File Offset: 0x005F96DC
    public Task ArchiveThreadAsync(string threadId)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.threads.RemoveAll((AgentThreadSummary t) => t.ThreadId == threadId);
        int num = this.clickys.FindIndex((ClickyInfo c) => c.ThreadId == threadId);
        if (num >= 0)
        {
          this.clickys.RemoveAt(num);
        }
      }
      EventHandler tasksChanged = this.TasksChanged;
      if (tasksChanged != null)
      {
        tasksChanged(this, EventArgs.Empty);
      }
      return Task.CompletedTask;
    }

    // Token: 0x06001C53 RID: 7251 RVA: 0x005F9780 File Offset: 0x005F9780
    public void ArchiveClicky(string slug)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.clickys.RemoveAll((ClickyInfo c) => c.Slug == slug);
      }
      EventHandler tasksChanged = this.TasksChanged;
      if (tasksChanged == null)
      {
        return;
      }
      tasksChanged(this, EventArgs.Empty);
    }

    // Token: 0x06001C54 RID: 7252 RVA: 0x005F97F8 File Offset: 0x005F97F8
    public bool KnowsClicky(string slug)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = this.clickys.Any<ClickyInfo>((ClickyInfo c) => c.Slug == slug);
      }
      return flag2;
    }

    // Token: 0x06001C55 RID: 7253 RVA: 0x005F9858 File Offset: 0x005F9858
    public ClickyInfo CreateClicky(string name, string role, string oneLiner)
    {
      ClickyInfo clickyInfo = new ClickyInfo(name.Trim().ToLowerInvariant().Replace(' ', '-'), name, role, oneLiner, null, DateTimeOffset.Now, false);
      object obj = this.gate;
      lock (obj)
      {
        this.clickys.Add(clickyInfo);
      }
      EventHandler tasksChanged = this.TasksChanged;
      if (tasksChanged != null)
      {
        tasksChanged(this, EventArgs.Empty);
      }
      return clickyInfo;
    }

    // Token: 0x06001C56 RID: 7254 RVA: 0x005F98DC File Offset: 0x005F98DC
    public string OpenThread(string threadId)
    {
      AgentTaskDetail agentTaskDetail = this.details.Values.FirstOrDefault<AgentTaskDetail>((AgentTaskDetail d) => d.Snapshot.ThreadId == threadId);
      return ((agentTaskDetail != null) ? agentTaskDetail.Snapshot.TaskId : null) ?? ("open-" + threadId);
    }

    // Token: 0x06001C57 RID: 7255 RVA: 0x005F9938 File Offset: 0x005F9938
    public Task<string> LaunchAsync(AgentLaunchRequest request)
    {
      return Task.FromResult<string>("demo-run");
    }

    // Token: 0x06001C58 RID: 7256 RVA: 0x005F9944 File Offset: 0x005F9944
    public Task SteerAsync(string taskId, string text)
    {
      EventHandler<AgentTaskEvent> taskEvent = this.TaskEvent;
      if (taskEvent != null)
      {
        taskEvent(this, new AgentTaskEvent(taskId, AgentTaskEventKind.ProgressChanged, "Steering the agent…"));
      }
      EventHandler<AgentTaskEvent> taskEvent2 = this.TaskEvent;
      if (taskEvent2 != null)
      {
        taskEvent2(this, new AgentTaskEvent(taskId, AgentTaskEventKind.SteerAccepted, text));
      }
      return Task.CompletedTask;
    }

    // Token: 0x06001C59 RID: 7257 RVA: 0x005F9984 File Offset: 0x005F9984
    public Task StopAsync(string taskId)
    {
      return Task.CompletedTask;
    }

    // Token: 0x06001C5A RID: 7258 RVA: 0x005F998C File Offset: 0x005F998C
    public Task ApproveExtraUsageAsync(string taskId, bool always)
    {
      return Task.CompletedTask;
    }

    // Token: 0x06001C5B RID: 7259 RVA: 0x005F9994 File Offset: 0x005F9994
    [return: Nullable(2)]
    public string WorkspacePath(string clickySlug)
    {
      return Path.Combine(DemoHomeSource.Root, clickySlug);
    }

    // Token: 0x06001C5C RID: 7260 RVA: 0x005F99A4 File Offset: 0x005F99A4
    public void SetRoutinePaused(string routineId, bool paused)
    {
      object obj = this.gate;
      lock (obj)
      {
        ClickyRoutine clickyRoutine = this.routines.FirstOrDefault<ClickyRoutine>((ClickyRoutine x) => x.Id == routineId);
        if (clickyRoutine != null)
        {
          clickyRoutine.IsPaused = paused;
        }
      }
      EventHandler routinesChanged = this.RoutinesChanged;
      if (routinesChanged == null)
      {
        return;
      }
      routinesChanged(this, EventArgs.Empty);
    }

    // Token: 0x06001C5D RID: 7261 RVA: 0x005F9A24 File Offset: 0x005F9A24
    public Task RunRoutineNowAsync(string routineId)
    {
      return Task.CompletedTask;
    }

    // Token: 0x06001C5E RID: 7262 RVA: 0x005F9A2C File Offset: 0x005F9A2C
    public void CreateRoutine(string clickySlug, string title, string taskPrompt, int intervalMinutes, bool runImmediately)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.routines.Add(ClickyRoutine.Create(clickySlug, title, taskPrompt, intervalMinutes, null));
      }
      EventHandler routinesChanged = this.RoutinesChanged;
      if (routinesChanged == null)
      {
        return;
      }
      routinesChanged(this, EventArgs.Empty);
    }

    // Token: 0x06001C5F RID: 7263 RVA: 0x005F9A94 File Offset: 0x005F9A94
    public void DeleteRoutine(string routineId)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.routines.RemoveAll((ClickyRoutine r) => r.Id == routineId);
      }
      EventHandler routinesChanged = this.RoutinesChanged;
      if (routinesChanged == null)
      {
        return;
      }
      routinesChanged(this, EventArgs.Empty);
    }

    // Token: 0x04000D7B RID: 3451
    private readonly object gate = new object();

    // Token: 0x04000D7C RID: 3452
    private readonly List<ClickyInfo> clickys;

    // Token: 0x04000D7D RID: 3453
    private readonly List<AgentThreadSummary> threads;

    // Token: 0x04000D7E RID: 3454
    private readonly List<ClickyRoutine> routines;

    // Token: 0x04000D7F RID: 3455
    private readonly Dictionary<string, List<AgentTranscriptTurn>> transcripts = new Dictionary<string, List<AgentTranscriptTurn>>();

    // Token: 0x04000D80 RID: 3456
    private readonly Dictionary<string, AgentTaskDetail> details = new Dictionary<string, AgentTaskDetail>();

    // Token: 0x04000D81 RID: 3457
    private static readonly string Root = Path.Combine(Path.GetTempPath(), "heyclicky-home-demo");
  }
}
