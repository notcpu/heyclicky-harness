using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky
{
  // Token: 0x02000030 RID: 48
  [NullableContext(1)]
  public interface IAgentService : IAsyncDisposable
  {
    // Token: 0x14000012 RID: 18
    // (add) Token: 0x06000259 RID: 601
    // (remove) Token: 0x0600025A RID: 602
    [Nullable(2)]
    event EventHandler TasksChanged;

    // Token: 0x1700009B RID: 155
    // (get) Token: 0x0600025B RID: 603
    IReadOnlyList<AgentTaskSnapshot> Tasks { get; }

    // Token: 0x1700009C RID: 156
    // (get) Token: 0x0600025C RID: 604
    bool IsRuntimeReady { get; }

    // Token: 0x0600025D RID: 605
    Task EnsureRuntimeReadyAsync(CancellationToken ct = default(CancellationToken));

    // Token: 0x0600025E RID: 606
    Task<string> LaunchAsync(string prompt, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<byte[]> imagesJpeg = null, CancellationToken ct = default(CancellationToken));

    // Token: 0x0600025F RID: 607
    Task SteerAsync(string taskId, string text, CancellationToken ct = default(CancellationToken));

    // Token: 0x06000260 RID: 608
    Task StopAsync(string taskId);
  }
}
