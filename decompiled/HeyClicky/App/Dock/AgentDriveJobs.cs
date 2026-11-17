using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.App.Browser;
using HeyClicky.App.Drive;
using HeyClicky.Core.Browser;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002F8 RID: 760
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class AgentDriveJobs : IAgentDriveJobs
  {
    // Token: 0x0600211E RID: 8478 RVA: 0x0061B774 File Offset: 0x0061B774
    public AgentDriveJobs(AgentService agents, Action<string, string> presentNeedsYou, Action<string, string> noteEventForBrain, Func<string, Task> announce)
    {
    }

    // Token: 0x17000611 RID: 1553
    // (get) Token: 0x0600211F RID: 8479 RVA: 0x0061B79C File Offset: 0x0061B79C
    public IEnumerable<IAgentDriveJob> Jobs
    {
      get
      {
        return (from d in this.<agents>P.Tasks.Select<AgentTaskSnapshot, AgentTaskDetail>((AgentTaskSnapshot t) => this.<agents>P.GetTaskDetail(t.TaskId)).OfType<AgentTaskDetail>()
          where !d.IsDemo
          select new DriveJob(this.<agents>P, d.Snapshot.TaskId)).ToList<IAgentDriveJob>();
      }
    }

    // Token: 0x06002120 RID: 8480 RVA: 0x0061B804 File Offset: 0x0061B804
    [return: Nullable(2)]
    public IAgentDriveJob Find(string taskId)
    {
      if (this.<agents>P.GetTaskDetail(taskId) != null)
      {
        return new DriveJob(this.<agents>P, taskId);
      }
      return null;
    }

    // Token: 0x06002121 RID: 8481 RVA: 0x0061B824 File Offset: 0x0061B824
    public bool IsThreadActive(string threadId)
    {
      return this.<agents>P.IsThreadActive(threadId);
    }

    // Token: 0x06002122 RID: 8482 RVA: 0x0061B834 File Offset: 0x0061B834
    public void SubmitFollowUp(string threadId, string text, [Nullable(2)] string preauthorizedLeaseId, string launchContext)
    {
      AgentDriveJobs.Run(delegate
      {
        AgentService agentService = this.<agents>P;
        string threadId2 = threadId;
        string text2 = text;
        string preauthorizedLeaseId2 = preauthorizedLeaseId;
        string launchContext2 = launchContext;
        return agentService.SubmitThreadFollowUpAsync(threadId2, text2, preauthorizedLeaseId2, default(CancellationToken), launchContext2);
      }, "follow-up");
    }

    // Token: 0x06002123 RID: 8483 RVA: 0x0061B880 File Offset: 0x0061B880
    [return: Nullable(new byte[] { 1, 2 })]
    public Task<string> ContinueLeaseAfterConsentAsync(string leaseId)
    {
      return this.<agents>P.ContinueLeaseAfterComputerUseConsentAsync(leaseId, default(CancellationToken));
    }

    // Token: 0x06002124 RID: 8484 RVA: 0x0061B8A4 File Offset: 0x0061B8A4
    public void CompleteLease(string leaseId, string threadId)
    {
      this.<agents>P.CompleteLease(leaseId, threadId, "completed");
    }

    // Token: 0x06002125 RID: 8485 RVA: 0x0061B8B8 File Offset: 0x0061B8B8
    public void PresentNeedsYou(IAgentDriveJob job)
    {
      this.<presentNeedsYou>P(job.TaskId, "needs_you");
    }

    // Token: 0x06002126 RID: 8486 RVA: 0x0061B8D0 File Offset: 0x0061B8D0
    public void NoteEventForBrain(IAgentDriveJob job, string eventText, string summary)
    {
      string threadId = job.ThreadId;
      if (threadId != null)
      {
        this.<noteEventForBrain>P(threadId, eventText);
      }
    }

    // Token: 0x06002127 RID: 8487 RVA: 0x0061B8F4 File Offset: 0x0061B8F4
    public void AnnounceVerbatim(string line)
    {
      this.<announce>P(line);
    }

    // Token: 0x06002128 RID: 8488 RVA: 0x0061B904 File Offset: 0x0061B904
    public bool SkipsChrome(string threadId)
    {
      return ChromeSetupFlow.Shared.SkipsChrome(threadId);
    }

    // Token: 0x17000612 RID: 1554
    // (get) Token: 0x06002129 RID: 8489 RVA: 0x0061B914 File Offset: 0x0061B914
    public bool ChromeIsConnected
    {
      get
      {
        return ClickyBrowserBridge.Shared.IsConnected;
      }
    }

    // Token: 0x0600212A RID: 8490 RVA: 0x0061B920 File Offset: 0x0061B920
    private static async Task Run(Func<Task> work, string what)
    {
      try
      {
        await work().ConfigureAwait(false);
      }
      catch (Exception ex)
      {
        Log.Warn("dock", "drive " + what + ": " + ex.Message);
      }
    }

    // Token: 0x04000FFC RID: 4092
    [CompilerGenerated]
    private AgentService <agents>P = agents;

    // Token: 0x04000FFD RID: 4093
    [CompilerGenerated]
    private Action<string, string> <presentNeedsYou>P = presentNeedsYou;

    // Token: 0x04000FFE RID: 4094
    [CompilerGenerated]
    private Action<string, string> <noteEventForBrain>P = noteEventForBrain;

    // Token: 0x04000FFF RID: 4095
    [CompilerGenerated]
    private Func<string, Task> <announce>P = announce;
  }
}
