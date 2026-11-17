using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HeyClicky.Agents.Routines
{
  // Token: 0x0200002A RID: 42
  [NullableContext(1)]
  internal interface IRoutineHost
  {
    // Token: 0x17000129 RID: 297
    // (get) Token: 0x0600045F RID: 1119
    bool IsSignedIn { get; }

    // Token: 0x06000460 RID: 1120
    bool ClickyExists(string slug);

    // Token: 0x06000461 RID: 1121
    bool IsClickyBusy(string slug);

    // Token: 0x06000462 RID: 1122
    bool IsClickyAwaitingExtraUsage(string slug);

    // Token: 0x06000463 RID: 1123
    [return: Nullable(new byte[] { 1, 2 })]
    Task<string> LaunchRoutineRunAsync(ClickyRoutine routine, int runNumber, string prompt, string launchContext);

    // Token: 0x06000464 RID: 1124
    bool JobExists(string jobId);

    // Token: 0x06000465 RID: 1125
    bool IsJobBusy(string jobId);

    // Token: 0x06000466 RID: 1126
    bool IsJobAwaitingExtraUsage(string jobId);

    // Token: 0x06000467 RID: 1127
    [return: Nullable(new byte[] { 1, 2 })]
    Task<string> LaunchJobRoutineRunAsync(ClickyRoutine routine, int runNumber, string prompt, bool scheduled);

    // Token: 0x06000468 RID: 1128
    [return: Nullable(2)]
    string CreateJobForMovedRoutine(ClickyRoutine routine);

    // Token: 0x06000469 RID: 1129
    void RetireClickysNoteForMovedRoutineJob(string jobId, bool onlyWhenLeft);

    // Token: 0x0600046A RID: 1130 RVA: 0x000EB518 File Offset: 0x000EB518
    void BackfillWorkReportsIfNeeded()
    {
    }
  }
}
