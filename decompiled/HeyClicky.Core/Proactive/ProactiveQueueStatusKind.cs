using System;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000D3 RID: 211
  public enum ProactiveQueueStatusKind
  {
    // Token: 0x040005BE RID: 1470
    Ready,
    // Token: 0x040005BF RID: 1471
    Starting,
    // Token: 0x040005C0 RID: 1472
    Paused,
    // Token: 0x040005C1 RID: 1473
    WaitingForClicky,
    // Token: 0x040005C2 RID: 1474
    WaitingForEarlierTask
  }
}
