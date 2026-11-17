using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Agents.Harness
{
  // Token: 0x02000064 RID: 100
  [NullableContext(1)]
  public interface IAgentHarnessRuntime
  {
    // Token: 0x0600081D RID: 2077
    Task RunTurnAsync(AgentHarnessTurn turn, Action<AgentHarnessEvent> emit, CancellationToken ct);

    // Token: 0x0600081E RID: 2078
    Task<bool> SteerAsync(string harnessThreadId, string text, bool informational, CancellationToken ct, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> imagePaths = null);

    // Token: 0x0600081F RID: 2079
    Task InterruptAsync(string harnessThreadId);
  }
}
