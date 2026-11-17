using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;

namespace HeyClicky.App.DevFakes
{
  // Token: 0x0200033F RID: 831
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class FakeAgentService : IAgentService, IAsyncDisposable
  {
    // Token: 0x14000083 RID: 131
    // (add) Token: 0x06002368 RID: 9064 RVA: 0x0062E58C File Offset: 0x0062E58C
    // (remove) Token: 0x06002369 RID: 9065 RVA: 0x0062E5C4 File Offset: 0x0062E5C4
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler TasksChanged;

    // Token: 0x1700067D RID: 1661
    // (get) Token: 0x0600236A RID: 9066 RVA: 0x0062E5FC File Offset: 0x0062E5FC
    public IReadOnlyList<AgentTaskSnapshot> Tasks
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<AgentTaskSnapshot> readOnlyList;
        lock (obj)
        {
          readOnlyList = this.tasks.ToList<AgentTaskSnapshot>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x1700067E RID: 1662
    // (get) Token: 0x0600236B RID: 9067 RVA: 0x0062E644 File Offset: 0x0062E644
    public bool IsRuntimeReady
    {
      get
      {
        return true;
      }
    }

    // Token: 0x0600236C RID: 9068 RVA: 0x0062E648 File Offset: 0x0062E648
    public Task EnsureRuntimeReadyAsync(CancellationToken ct = default(CancellationToken))
    {
      return Task.CompletedTask;
    }

    // Token: 0x0600236D RID: 9069 RVA: 0x0062E650 File Offset: 0x0062E650
    private void Upsert(AgentTaskSnapshot s)
    {
      object obj = this.gate;
      lock (obj)
      {
        int num = this.tasks.FindIndex((AgentTaskSnapshot t) => t.TaskId == s.TaskId);
        if (num >= 0)
        {
          this.tasks[num] = s;
        }
        else
        {
          this.tasks.Add(s);
        }
      }
      EventHandler tasksChanged = this.TasksChanged;
      if (tasksChanged == null)
      {
        return;
      }
      tasksChanged(this, EventArgs.Empty);
    }

    // Token: 0x0600236E RID: 9070 RVA: 0x0062E6F0 File Offset: 0x0062E6F0
    public async Task<string> LaunchAsync(string prompt, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<byte[]> imagesJpeg = null, CancellationToken ct = default(CancellationToken))
    {
      FakeAgentService.<>c__DisplayClass11_0 CS$<>8__locals1 = new FakeAgentService.<>c__DisplayClass11_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.id = Guid.NewGuid().ToString("N").Substring(0, 8);
      string text = ((prompt.Length > 28) ? (prompt.Substring(0, 28).TrimEnd() + "…") : prompt);
      CS$<>8__locals1.snap = new AgentTaskSnapshot(CS$<>8__locals1.id, null, text, AgentTaskStatus.Starting, "", null, DateTimeOffset.Now);
      this.DetailFor(CS$<>8__locals1.id).UserPrompts.Add(prompt);
      this.Upsert(CS$<>8__locals1.snap);
      Task.Run(delegate
      {
        FakeAgentService.<>c__DisplayClass11_0.<<LaunchAsync>b__0>d <<LaunchAsync>b__0>d;
        <<LaunchAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<LaunchAsync>b__0>d.<>4__this = CS$<>8__locals1;
        <<LaunchAsync>b__0>d.<>1__state = -1;
        <<LaunchAsync>b__0>d.<>t__builder.Start<FakeAgentService.<>c__DisplayClass11_0.<<LaunchAsync>b__0>d>(ref <<LaunchAsync>b__0>d);
        return <<LaunchAsync>b__0>d.<>t__builder.Task;
      });
      await Task.Yield();
      return CS$<>8__locals1.id;
    }

    // Token: 0x0600236F RID: 9071 RVA: 0x0062E73C File Offset: 0x0062E73C
    public Task SteerAsync(string taskId, string text, CancellationToken ct = default(CancellationToken))
    {
      object obj = this.gate;
      lock (obj)
      {
        int num = this.tasks.FindIndex((AgentTaskSnapshot t) => t.TaskId == taskId);
        if (num >= 0)
        {
          List<AgentTaskSnapshot> list = this.tasks;
          int num2 = num;
          AgentTaskSnapshot agentTaskSnapshot = this.tasks[num].<Clone>$();
          agentTaskSnapshot.LatestCommentary = "Got it: " + text;
          agentTaskSnapshot.Status = AgentTaskStatus.Running;
          list[num2] = agentTaskSnapshot;
        }
      }
      this.DetailFor(taskId).UserPrompts.Add(text);
      EventHandler tasksChanged = this.TasksChanged;
      if (tasksChanged != null)
      {
        tasksChanged(this, EventArgs.Empty);
      }
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

    // Token: 0x06002370 RID: 9072 RVA: 0x0062E84C File Offset: 0x0062E84C
    public Task StopAsync(string taskId)
    {
      object obj = this.gate;
      lock (obj)
      {
        int num = this.tasks.FindIndex((AgentTaskSnapshot t) => t.TaskId == taskId);
        if (num >= 0)
        {
          List<AgentTaskSnapshot> list = this.tasks;
          int num2 = num;
          AgentTaskSnapshot agentTaskSnapshot = this.tasks[num].<Clone>$();
          agentTaskSnapshot.Status = AgentTaskStatus.Stopped;
          agentTaskSnapshot.LatestCommentary = "Stopped.";
          list[num2] = agentTaskSnapshot;
        }
      }
      EventHandler tasksChanged = this.TasksChanged;
      if (tasksChanged != null)
      {
        tasksChanged(this, EventArgs.Empty);
      }
      return Task.CompletedTask;
    }

    // Token: 0x06002371 RID: 9073 RVA: 0x0062E8FC File Offset: 0x0062E8FC
    public void Seed(params AgentTaskSnapshot[] snapshots)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.tasks.Clear();
        this.tasks.AddRange(snapshots);
        this.details.Clear();
      }
      EventHandler tasksChanged = this.TasksChanged;
      if (tasksChanged == null)
      {
        return;
      }
      tasksChanged(this, EventArgs.Empty);
    }

    // Token: 0x14000084 RID: 132
    // (add) Token: 0x06002372 RID: 9074 RVA: 0x0062E970 File Offset: 0x0062E970
    // (remove) Token: 0x06002373 RID: 9075 RVA: 0x0062E9A8 File Offset: 0x0062E9A8
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<AgentTaskEvent> TaskEvent;

    // Token: 0x06002374 RID: 9076 RVA: 0x0062E9E0 File Offset: 0x0062E9E0
    public FakeAgentService.FakeDetail DetailFor(string taskId)
    {
      object obj = this.gate;
      FakeAgentService.FakeDetail fakeDetail2;
      lock (obj)
      {
        FakeAgentService.FakeDetail fakeDetail;
        if (!this.details.TryGetValue(taskId, out fakeDetail))
        {
          fakeDetail = (this.details[taskId] = new FakeAgentService.FakeDetail());
        }
        fakeDetail2 = fakeDetail;
      }
      return fakeDetail2;
    }

    // Token: 0x06002375 RID: 9077 RVA: 0x0062EA40 File Offset: 0x0062EA40
    [return: Nullable(2)]
    public AgentTaskDetail GetTaskDetail(string taskId)
    {
      object obj = this.gate;
      AgentTaskSnapshot agentTaskSnapshot;
      FakeAgentService.FakeDetail fakeDetail;
      lock (obj)
      {
        agentTaskSnapshot = this.tasks.Find((AgentTaskSnapshot t) => t.TaskId == taskId);
        FakeAgentService.FakeDetail fakeDetail2;
        fakeDetail = (this.details.TryGetValue(taskId, out fakeDetail2) ? fakeDetail2 : new FakeAgentService.FakeDetail());
      }
      if (agentTaskSnapshot == null)
      {
        return null;
      }
      return new AgentTaskDetail(agentTaskSnapshot, null, agentTaskSnapshot.Title, fakeDetail.WorkspacePath, null, agentTaskSnapshot.LatestCommentary, null, "", "", fakeDetail.Activity.ToList<AgentActivityItem>(), fakeDetail.UserPrompts.ToList<string>(), fakeDetail.Summary, null, fakeDetail.NextActions.ToList<string>(), fakeDetail.Artifacts.ToList<string>(), fakeDetail.FailureMessage, null, fakeDetail.RequiresExtraUsageApproval, fakeDetail.DidRefundEarlyStop, null, 0L, 0L, 0L, false, false, "voice", null, null, null, null, false, false, false, null, null, false);
    }

    // Token: 0x06002376 RID: 9078 RVA: 0x0062EB48 File Offset: 0x0062EB48
    public Task ApproveExtraUsageAsync(string taskId, bool asManyAsNeeded, CancellationToken ct = default(CancellationToken))
    {
      this.DetailFor(taskId).RequiresExtraUsageApproval = false;
      object obj = this.gate;
      lock (obj)
      {
        int num = this.tasks.FindIndex((AgentTaskSnapshot t) => t.TaskId == taskId);
        if (num >= 0)
        {
          List<AgentTaskSnapshot> list = this.tasks;
          int num2 = num;
          AgentTaskSnapshot agentTaskSnapshot = this.tasks[num].<Clone>$();
          agentTaskSnapshot.Status = AgentTaskStatus.Running;
          agentTaskSnapshot.LatestCommentary = "Picking up where I left off";
          list[num2] = agentTaskSnapshot;
        }
      }
      EventHandler tasksChanged = this.TasksChanged;
      if (tasksChanged != null)
      {
        tasksChanged(this, EventArgs.Empty);
      }
      return Task.CompletedTask;
    }

    // Token: 0x06002377 RID: 9079 RVA: 0x0062EC08 File Offset: 0x0062EC08
    public void DismissTask(string taskId)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.tasks.RemoveAll((AgentTaskSnapshot t) => t.TaskId == taskId);
      }
      EventHandler tasksChanged = this.TasksChanged;
      if (tasksChanged == null)
      {
        return;
      }
      tasksChanged(this, EventArgs.Empty);
    }

    // Token: 0x06002378 RID: 9080 RVA: 0x0062EC80 File Offset: 0x0062EC80
    public void RaiseTaskEvent(AgentTaskEvent e)
    {
      EventHandler<AgentTaskEvent> taskEvent = this.TaskEvent;
      if (taskEvent == null)
      {
        return;
      }
      taskEvent(this, e);
    }

    // Token: 0x06002379 RID: 9081 RVA: 0x0062EC94 File Offset: 0x0062EC94
    public ValueTask DisposeAsync()
    {
      return ValueTask.CompletedTask;
    }

    // Token: 0x0400115E RID: 4446
    private readonly List<AgentTaskSnapshot> tasks = new List<AgentTaskSnapshot>();

    // Token: 0x0400115F RID: 4447
    private readonly object gate = new object();

    // Token: 0x04001161 RID: 4449
    private readonly Dictionary<string, FakeAgentService.FakeDetail> details = new Dictionary<string, FakeAgentService.FakeDetail>();

    // Token: 0x02000993 RID: 2451
    [Nullable(0)]
    public sealed class FakeDetail
    {
      // Token: 0x1700085D RID: 2141
      // (get) Token: 0x06003DAC RID: 15788 RVA: 0x0069B0D8 File Offset: 0x0069B0D8
      public List<AgentActivityItem> Activity { get; } = new List<AgentActivityItem>();

      // Token: 0x1700085E RID: 2142
      // (get) Token: 0x06003DAD RID: 15789 RVA: 0x0069B0E0 File Offset: 0x0069B0E0
      public List<string> UserPrompts { get; } = new List<string>();

      // Token: 0x1700085F RID: 2143
      // (get) Token: 0x06003DAE RID: 15790 RVA: 0x0069B0E8 File Offset: 0x0069B0E8
      public List<string> Artifacts { get; } = new List<string>();

      // Token: 0x17000860 RID: 2144
      // (get) Token: 0x06003DAF RID: 15791 RVA: 0x0069B0F0 File Offset: 0x0069B0F0
      public List<string> NextActions { get; } = new List<string>();

      // Token: 0x17000861 RID: 2145
      // (get) Token: 0x06003DB0 RID: 15792 RVA: 0x0069B0F8 File Offset: 0x0069B0F8
      // (set) Token: 0x06003DB1 RID: 15793 RVA: 0x0069B100 File Offset: 0x0069B100
      [Nullable(2)]
      public string Summary
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x17000862 RID: 2146
      // (get) Token: 0x06003DB2 RID: 15794 RVA: 0x0069B10C File Offset: 0x0069B10C
      // (set) Token: 0x06003DB3 RID: 15795 RVA: 0x0069B114 File Offset: 0x0069B114
      [Nullable(2)]
      public string WorkspacePath
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x17000863 RID: 2147
      // (get) Token: 0x06003DB4 RID: 15796 RVA: 0x0069B120 File Offset: 0x0069B120
      // (set) Token: 0x06003DB5 RID: 15797 RVA: 0x0069B128 File Offset: 0x0069B128
      [Nullable(2)]
      public string FailureMessage
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x17000864 RID: 2148
      // (get) Token: 0x06003DB6 RID: 15798 RVA: 0x0069B134 File Offset: 0x0069B134
      // (set) Token: 0x06003DB7 RID: 15799 RVA: 0x0069B13C File Offset: 0x0069B13C
      public bool RequiresExtraUsageApproval { get; set; }

      // Token: 0x17000865 RID: 2149
      // (get) Token: 0x06003DB8 RID: 15800 RVA: 0x0069B148 File Offset: 0x0069B148
      // (set) Token: 0x06003DB9 RID: 15801 RVA: 0x0069B150 File Offset: 0x0069B150
      public bool DidRefundEarlyStop { get; set; }
    }
  }
}
