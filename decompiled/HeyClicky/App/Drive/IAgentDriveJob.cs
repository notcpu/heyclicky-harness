using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Drive
{
  // Token: 0x020002BC RID: 700
  [NullableContext(2)]
  public interface IAgentDriveJob
  {
    // Token: 0x17000521 RID: 1313
    // (get) Token: 0x06001D22 RID: 7458
    [Nullable(1)]
    string TaskId
    {
      [NullableContext(1)]
      get;
    }

    // Token: 0x17000522 RID: 1314
    // (get) Token: 0x06001D23 RID: 7459
    string ThreadId { get; }

    // Token: 0x17000523 RID: 1315
    // (get) Token: 0x06001D24 RID: 7460
    [Nullable(1)]
    string Name
    {
      [NullableContext(1)]
      get;
    }

    // Token: 0x17000524 RID: 1316
    // (get) Token: 0x06001D25 RID: 7461
    [Nullable(1)]
    string Title
    {
      [NullableContext(1)]
      get;
    }

    // Token: 0x17000525 RID: 1317
    // (get) Token: 0x06001D26 RID: 7462
    string AgentTurnLeaseId { get; }

    // Token: 0x17000526 RID: 1318
    // (get) Token: 0x06001D27 RID: 7463
    bool IsRunning { get; }

    // Token: 0x17000527 RID: 1319
    // (get) Token: 0x06001D28 RID: 7464
    bool NeedsChromeConnection { get; }

    // Token: 0x17000528 RID: 1320
    // (get) Token: 0x06001D29 RID: 7465
    bool FoundChromeNotConnectedThisTurn { get; }

    // Token: 0x17000529 RID: 1321
    // (get) Token: 0x06001D2A RID: 7466
    // (set) Token: 0x06001D2B RID: 7467
    string PendingComputerUseRequest { get; set; }

    // Token: 0x1700052A RID: 1322
    // (get) Token: 0x06001D2C RID: 7468
    // (set) Token: 0x06001D2D RID: 7469
    string PendingComputerUseSummary { get; set; }

    // Token: 0x1700052B RID: 1323
    // (get) Token: 0x06001D2E RID: 7470
    // (set) Token: 0x06001D2F RID: 7471
    bool DidAnnounceComputerUseRequest { get; set; }
  }
}
