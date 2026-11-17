using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Core.Tools
{
  // Token: 0x02000098 RID: 152
  [NullableContext(1)]
  public interface IAgentToolHost
  {
    // Token: 0x1700022E RID: 558
    // (get) Token: 0x0600098C RID: 2444
    IReadOnlyList<AgentClickyInfo> Clickys { get; }

    // Token: 0x0600098D RID: 2445
    [return: Nullable(2)]
    AgentClickyInfo FindClicky(string slug);

    // Token: 0x1700022F RID: 559
    // (get) Token: 0x0600098E RID: 2446
    IReadOnlyList<AgentTaskInfo> Tasks { get; }

    // Token: 0x17000230 RID: 560
    // (get) Token: 0x0600098F RID: 2447
    string ProjectsRoot { get; }

    // Token: 0x06000990 RID: 2448
    [return: Nullable(2)]
    string WorkspacePath(string clickySlug);

    // Token: 0x06000991 RID: 2449
    Task<string> LaunchAsync(AgentToolLaunch launch, CancellationToken ct);

    // Token: 0x06000992 RID: 2450
    Task SteerAsync(string taskId, string text, CancellationToken ct);

    // Token: 0x06000993 RID: 2451 RVA: 0x0036E414 File Offset: 0x0036E414
    Task SteerAsync(string taskId, string text, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<byte[]> imagesJpeg, CancellationToken ct)
    {
      return this.SteerAsync(taskId, text, ct);
    }

    // Token: 0x06000994 RID: 2452 RVA: 0x0036E420 File Offset: 0x0036E420
    [NullableContext(2)]
    string BuildsOnContext([Nullable(1)] IReadOnlyList<string> jobIds, string receivingThreadId)
    {
      return null;
    }

    // Token: 0x06000995 RID: 2453
    string OpenThread(string threadId);

    // Token: 0x06000996 RID: 2454
    Task StopAsync(string taskId);

    // Token: 0x06000997 RID: 2455
    Task ApproveExtraUsageAsync(string taskId, bool asManyAsNeeded, CancellationToken ct);

    // Token: 0x06000998 RID: 2456
    AgentClickyInfo CreateClicky(string name, string roleText, string oneLiner);

    // Token: 0x06000999 RID: 2457
    [return: TupleElementNames(new string[] { "Role", "Text" })]
    [return: Nullable(new byte[] { 1, 1, 0, 1, 1 })]
    Task<IReadOnlyList<ValueTuple<string, string>>> RecentMessagesAsync(string threadId, int limit, CancellationToken ct);

    // Token: 0x0600099A RID: 2458
    Task<IReadOnlyList<AgentThreadInfo>> HistoryAsync(int limit, CancellationToken ct);

    // Token: 0x0600099B RID: 2459
    [NullableContext(2)]
    bool OpenHome(string threadId);

    // Token: 0x0600099C RID: 2460
    bool CloseHome();

    // Token: 0x0600099D RID: 2461
    bool OpenPath(string pathOrUrl);

    // Token: 0x0600099E RID: 2462
    bool RevealPath(string path);

    // Token: 0x0600099F RID: 2463
    bool SkipOnboardingInterview();

    // Token: 0x060009A0 RID: 2464 RVA: 0x0036E424 File Offset: 0x0036E424
    [return: Nullable(2)]
    AgentJobInfo FindJob(string threadId)
    {
      return null;
    }

    // Token: 0x060009A1 RID: 2465 RVA: 0x0036E428 File Offset: 0x0036E428
    [return: Nullable(2)]
    string ClickySlugForThread(string threadId)
    {
      AgentClickyInfo agentClickyInfo = this.Clickys.FirstOrDefault<AgentClickyInfo>((AgentClickyInfo c) => c.ThreadId == threadId);
      if (agentClickyInfo == null)
      {
        return null;
      }
      return agentClickyInfo.Slug;
    }

    // Token: 0x060009A2 RID: 2466 RVA: 0x0036E464 File Offset: 0x0036E464
    [return: Nullable(new byte[] { 1, 2 })]
    Task<string> HandleSuggestionActionAsync(string task)
    {
      return Task.FromResult<string>(null);
    }

    // Token: 0x060009A3 RID: 2467 RVA: 0x0036E46C File Offset: 0x0036E46C
    void ApproveComputerUse(string taskId)
    {
    }

    // Token: 0x060009A4 RID: 2468 RVA: 0x0036E470 File Offset: 0x0036E470
    void DeclineComputerUse(string taskId)
    {
    }

    // Token: 0x060009A5 RID: 2469 RVA: 0x0036E474 File Offset: 0x0036E474
    Task<string> FindJobsAsync(string query, int limit)
    {
      return Task.FromResult<string>("{\"status\":\"none\",\"jobs\":[],\"message\":\"No earlier work on this PC yet.\"}");
    }

    // Token: 0x060009A6 RID: 2470 RVA: 0x0036E480 File Offset: 0x0036E480
    [return: TupleElementNames(new string[] { "IsJob", "ThreadId" })]
    [return: Nullable(new byte[] { 0, 2 })]
    ValueTuple<bool, string> ThreadIdForJobId(string jobId)
    {
      return new ValueTuple<bool, string>(false, null);
    }

    // Token: 0x060009A7 RID: 2471 RVA: 0x0036E48C File Offset: 0x0036E48C
    string JobIdForThread(string threadId)
    {
      return threadId;
    }

    // Token: 0x060009A8 RID: 2472 RVA: 0x0036E490 File Offset: 0x0036E490
    void RetitleJob(string threadId, string title)
    {
    }
  }
}
