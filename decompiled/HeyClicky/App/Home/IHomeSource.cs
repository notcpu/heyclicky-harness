using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Routines;

namespace HeyClicky.App.Home
{
  // Token: 0x020002AE RID: 686
  [NullableContext(1)]
  public interface IHomeSource
  {
    // Token: 0x14000069 RID: 105
    // (add) Token: 0x06001C0F RID: 7183
    // (remove) Token: 0x06001C10 RID: 7184
    [Nullable(2)]
    event EventHandler TasksChanged;

    // Token: 0x1400006A RID: 106
    // (add) Token: 0x06001C11 RID: 7185
    // (remove) Token: 0x06001C12 RID: 7186
    [Nullable(new byte[] { 2, 1 })]
    event EventHandler<AgentTaskEvent> TaskEvent;

    // Token: 0x1400006B RID: 107
    // (add) Token: 0x06001C13 RID: 7187
    // (remove) Token: 0x06001C14 RID: 7188
    [Nullable(2)]
    event EventHandler RoutinesChanged;

    // Token: 0x170004CF RID: 1231
    // (get) Token: 0x06001C15 RID: 7189
    IReadOnlyList<AgentTaskSnapshot> Tasks { get; }

    // Token: 0x170004D0 RID: 1232
    // (get) Token: 0x06001C16 RID: 7190
    IReadOnlyList<ClickyInfo> Clickys { get; }

    // Token: 0x170004D1 RID: 1233
    // (get) Token: 0x06001C17 RID: 7191
    IReadOnlyList<ClickyRoutine> Routines { get; }

    // Token: 0x06001C18 RID: 7192
    [return: Nullable(2)]
    AgentTaskDetail GetTaskDetail(string taskId);

    // Token: 0x06001C19 RID: 7193
    Task<IReadOnlyList<AgentThreadSummary>> ListThreadsAsync(CancellationToken ct = default(CancellationToken));

    // Token: 0x06001C1A RID: 7194
    Task<IReadOnlyList<AgentTranscriptTurn>> ReadThreadAsync(string threadId, CancellationToken ct = default(CancellationToken));

    // Token: 0x06001C1B RID: 7195
    Task ArchiveThreadAsync(string threadId);

    // Token: 0x06001C1C RID: 7196
    void ArchiveClicky(string slug);

    // Token: 0x06001C1D RID: 7197
    ClickyInfo CreateClicky(string name, string role, string oneLiner);

    // Token: 0x06001C1E RID: 7198
    bool KnowsClicky(string slug);

    // Token: 0x06001C1F RID: 7199
    string OpenThread(string threadId);

    // Token: 0x06001C20 RID: 7200
    Task<string> LaunchAsync(AgentLaunchRequest request);

    // Token: 0x06001C21 RID: 7201
    Task SteerAsync(string taskId, string text);

    // Token: 0x06001C22 RID: 7202
    Task StopAsync(string taskId);

    // Token: 0x06001C23 RID: 7203
    Task ApproveExtraUsageAsync(string taskId, bool always);

    // Token: 0x06001C24 RID: 7204
    [return: Nullable(2)]
    string WorkspacePath(string clickySlug);

    // Token: 0x06001C25 RID: 7205
    void SetRoutinePaused(string routineId, bool paused);

    // Token: 0x06001C26 RID: 7206
    Task RunRoutineNowAsync(string routineId);

    // Token: 0x06001C27 RID: 7207
    void DeleteRoutine(string routineId);

    // Token: 0x06001C28 RID: 7208
    void CreateRoutine(string clickySlug, string title, string taskPrompt, int intervalMinutes, bool runImmediately);
  }
}
