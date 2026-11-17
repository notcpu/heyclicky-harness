using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Routines;
using HeyClicky.Core.Drive;

namespace HeyClicky.App.Home
{
  // Token: 0x020002AF RID: 687
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentHomeSource : IHomeSource
  {
    // Token: 0x06001C29 RID: 7209 RVA: 0x005F8810 File Offset: 0x005F8810
    public AgentHomeSource(AgentService agents)
    {
      this.a = agents;
    }

    // Token: 0x06001C2A RID: 7210 RVA: 0x005F8820 File Offset: 0x005F8820
    [return: Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> TakeLegacyClickyMigration()
    {
      return this.a.TakeLegacyClickyMigration();
    }

    // Token: 0x1400006C RID: 108
    // (add) Token: 0x06001C2B RID: 7211 RVA: 0x005F8830 File Offset: 0x005F8830
    // (remove) Token: 0x06001C2C RID: 7212 RVA: 0x005F8840 File Offset: 0x005F8840
    [Nullable(2)]
    public event EventHandler TasksChanged
    {
      [NullableContext(2)]
      add
      {
        this.a.TasksChanged += value;
      }
      [NullableContext(2)]
      remove
      {
        this.a.TasksChanged -= value;
      }
    }

    // Token: 0x1400006D RID: 109
    // (add) Token: 0x06001C2D RID: 7213 RVA: 0x005F8850 File Offset: 0x005F8850
    // (remove) Token: 0x06001C2E RID: 7214 RVA: 0x005F8860 File Offset: 0x005F8860
    [Nullable(new byte[] { 2, 1 })]
    public event EventHandler<AgentTaskEvent> TaskEvent
    {
      add
      {
        this.a.TaskEvent += value;
      }
      remove
      {
        this.a.TaskEvent -= value;
      }
    }

    // Token: 0x1400006E RID: 110
    // (add) Token: 0x06001C2F RID: 7215 RVA: 0x005F8870 File Offset: 0x005F8870
    // (remove) Token: 0x06001C30 RID: 7216 RVA: 0x005F8880 File Offset: 0x005F8880
    [Nullable(2)]
    public event EventHandler RoutinesChanged
    {
      [NullableContext(2)]
      add
      {
        this.a.RoutinesChanged += value;
      }
      [NullableContext(2)]
      remove
      {
        this.a.RoutinesChanged -= value;
      }
    }

    // Token: 0x170004D2 RID: 1234
    // (get) Token: 0x06001C31 RID: 7217 RVA: 0x005F8890 File Offset: 0x005F8890
    public IReadOnlyList<AgentTaskSnapshot> Tasks
    {
      get
      {
        return this.a.Tasks;
      }
    }

    // Token: 0x170004D3 RID: 1235
    // (get) Token: 0x06001C32 RID: 7218 RVA: 0x005F88A0 File Offset: 0x005F88A0
    public IReadOnlyList<ClickyInfo> Clickys
    {
      get
      {
        return this.a.Clickys;
      }
    }

    // Token: 0x170004D4 RID: 1236
    // (get) Token: 0x06001C33 RID: 7219 RVA: 0x005F88B0 File Offset: 0x005F88B0
    public IReadOnlyList<ClickyRoutine> Routines
    {
      get
      {
        return this.a.Routines.Routines;
      }
    }

    // Token: 0x06001C34 RID: 7220 RVA: 0x005F88C4 File Offset: 0x005F88C4
    [return: Nullable(2)]
    public AgentTaskDetail GetTaskDetail(string taskId)
    {
      return this.a.GetTaskDetail(taskId);
    }

    // Token: 0x06001C35 RID: 7221 RVA: 0x005F88D4 File Offset: 0x005F88D4
    public async Task<IReadOnlyList<AgentThreadSummary>> ListThreadsAsync(CancellationToken ct = default(CancellationToken))
    {
      List<AgentThreadSummary> all = new List<AgentThreadSummary>();
      string text = null;
      for (int page = 0; page < 2; page++)
      {
        object obj = await this.a.ListThreadsAsync(100, text, null, ct).ConfigureAwait(false);
        IReadOnlyList<AgentThreadSummary> item = obj.Item1;
        string item2 = obj.Item2;
        all.AddRange(item);
        if (item2 == null || item.Count == 0)
        {
          break;
        }
        text = item2;
      }
      return all;
    }

    // Token: 0x06001C36 RID: 7222 RVA: 0x005F8920 File Offset: 0x005F8920
    public Task<IReadOnlyList<AgentTranscriptTurn>> ReadThreadAsync(string threadId, CancellationToken ct = default(CancellationToken))
    {
      return this.a.ReadThreadAsync(threadId, ct);
    }

    // Token: 0x06001C37 RID: 7223 RVA: 0x005F8930 File Offset: 0x005F8930
    public Task ArchiveThreadAsync(string threadId)
    {
      return this.a.ArchiveThreadAsync(threadId, default(CancellationToken));
    }

    // Token: 0x06001C38 RID: 7224 RVA: 0x005F8954 File Offset: 0x005F8954
    public void ArchiveClicky(string slug)
    {
      this.a.ArchiveClicky(slug);
    }

    // Token: 0x06001C39 RID: 7225 RVA: 0x005F8964 File Offset: 0x005F8964
    public ClickyInfo CreateClicky(string name, string role, string oneLiner)
    {
      return this.a.CreateClicky(name, role, oneLiner);
    }

    // Token: 0x06001C3A RID: 7226 RVA: 0x005F8974 File Offset: 0x005F8974
    public bool KnowsClicky(string slug)
    {
      return this.a.KnowsClicky(slug);
    }

    // Token: 0x06001C3B RID: 7227 RVA: 0x005F8984 File Offset: 0x005F8984
    public string OpenThread(string threadId)
    {
      return this.a.OpenThread(threadId);
    }

    // Token: 0x06001C3C RID: 7228 RVA: 0x005F8994 File Offset: 0x005F8994
    public Task<string> LaunchAsync(AgentLaunchRequest request)
    {
      return this.a.LaunchAsync(request, default(CancellationToken));
    }

    // Token: 0x06001C3D RID: 7229 RVA: 0x005F89B8 File Offset: 0x005F89B8
    public Task SteerAsync(string taskId, string text)
    {
      MacStepWaiters.Shared.ForgetDeclined(taskId);
      return this.a.SteerAsync(taskId, text, default(CancellationToken));
    }

    // Token: 0x06001C3E RID: 7230 RVA: 0x005F89E8 File Offset: 0x005F89E8
    public Task StopAsync(string taskId)
    {
      return this.a.StopAsync(taskId);
    }

    // Token: 0x06001C3F RID: 7231 RVA: 0x005F89F8 File Offset: 0x005F89F8
    public Task ApproveExtraUsageAsync(string taskId, bool always)
    {
      return this.a.ApproveExtraUsageAsync(taskId, always, default(CancellationToken), true);
    }

    // Token: 0x06001C40 RID: 7232 RVA: 0x005F8A1C File Offset: 0x005F8A1C
    [return: Nullable(2)]
    public string WorkspacePath(string clickySlug)
    {
      return this.a.WorkspacePath(clickySlug);
    }

    // Token: 0x06001C41 RID: 7233 RVA: 0x005F8A2C File Offset: 0x005F8A2C
    public void SetRoutinePaused(string routineId, bool paused)
    {
      this.a.Routines.SetPaused(routineId, paused, "profile");
    }

    // Token: 0x06001C42 RID: 7234 RVA: 0x005F8A48 File Offset: 0x005F8A48
    public Task RunRoutineNowAsync(string routineId)
    {
      return this.a.Routines.RunNowAsync(routineId);
    }

    // Token: 0x06001C43 RID: 7235 RVA: 0x005F8A5C File Offset: 0x005F8A5C
    public void DeleteRoutine(string routineId)
    {
      this.a.Routines.Delete(routineId, "profile");
    }

    // Token: 0x06001C44 RID: 7236 RVA: 0x005F8A74 File Offset: 0x005F8A74
    public void CreateRoutine(string clickySlug, string title, string taskPrompt, int intervalMinutes, bool runImmediately)
    {
      if (this.a.Routines.Routines.Any<ClickyRoutine>((ClickyRoutine r) => r.ClickySlug == clickySlug && string.Equals(r.Title, title, StringComparison.OrdinalIgnoreCase)))
      {
        return;
      }
      this.a.Routines.CreateRoutine(clickySlug, title, taskPrompt, intervalMinutes, runImmediately, null);
    }

    // Token: 0x04000D77 RID: 3447
    private readonly AgentService a;
  }
}
