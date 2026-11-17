using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x0200004F RID: 79
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyPendingAgentRequestStore
  {
    // Token: 0x06000707 RID: 1799 RVA: 0x000F9B74 File Offset: 0x000F9B74
    public static IReadOnlyList<ClickyPendingAgentRequest> All()
    {
      DateTimeOffset cutoff = DateTimeOffset.Now - ClickyPendingAgentRequestStore.MaximumAge;
      object gate = ClickyPendingAgentRequestStore.Gate;
      IReadOnlyList<ClickyPendingAgentRequest> readOnlyList;
      lock (gate)
      {
        readOnlyList = (AgentDefaults.Get<List<ClickyPendingAgentRequest>>("clicky.agent.pendingRequests.v1") ?? new List<ClickyPendingAgentRequest>()).Where<ClickyPendingAgentRequest>((ClickyPendingAgentRequest r) => r.AskedAt > cutoff).ToList<ClickyPendingAgentRequest>();
      }
      return readOnlyList;
    }

    // Token: 0x06000708 RID: 1800 RVA: 0x000F9BF4 File Offset: 0x000F9BF4
    public static void Remove(string threadId)
    {
      object gate = ClickyPendingAgentRequestStore.Gate;
      lock (gate)
      {
        IReadOnlyList<ClickyPendingAgentRequest> readOnlyList = ClickyPendingAgentRequestStore.All();
        if (readOnlyList.Any<ClickyPendingAgentRequest>((ClickyPendingAgentRequest r) => r.ThreadId == threadId))
        {
          ClickyPendingAgentRequestStore.Write(readOnlyList.Where<ClickyPendingAgentRequest>((ClickyPendingAgentRequest r) => r.ThreadId != threadId).ToList<ClickyPendingAgentRequest>());
        }
      }
    }

    // Token: 0x06000709 RID: 1801 RVA: 0x000F9C74 File Offset: 0x000F9C74
    public static void RemoveAllForSignOut()
    {
      AgentDefaults.Remove("clicky.agent.pendingRequests.v1");
    }

    // Token: 0x0600070A RID: 1802 RVA: 0x000F9C80 File Offset: 0x000F9C80
    public static void Sync([Nullable(2)] ClickyPendingAgentRequest request, string threadId)
    {
      object gate = ClickyPendingAgentRequestStore.Gate;
      lock (gate)
      {
        if (request == null)
        {
          ClickyPendingAgentRequestStore.Remove(threadId);
        }
        else
        {
          List<ClickyPendingAgentRequest> list = (from r in ClickyPendingAgentRequestStore.All()
            where r.ThreadId != threadId
            select r).ToList<ClickyPendingAgentRequest>();
          list.Add(request);
          ClickyPendingAgentRequestStore.Write(list);
        }
      }
    }

    // Token: 0x0600070B RID: 1803 RVA: 0x000F9D00 File Offset: 0x000F9D00
    private static void Write(IReadOnlyList<ClickyPendingAgentRequest> records)
    {
      if (records.Count == 0)
      {
        AgentDefaults.Remove("clicky.agent.pendingRequests.v1");
        return;
      }
      AgentDefaults.Set<IReadOnlyList<ClickyPendingAgentRequest>>("clicky.agent.pendingRequests.v1", records);
    }

    // Token: 0x0400028D RID: 653
    private const string DefaultsKey = "clicky.agent.pendingRequests.v1";

    // Token: 0x0400028E RID: 654
    private static readonly TimeSpan MaximumAge = TimeSpan.FromDays(7);

    // Token: 0x0400028F RID: 655
    private static readonly object Gate = new object();
  }
}
