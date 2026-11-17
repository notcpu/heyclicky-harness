using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001A3 RID: 419
  [NullableContext(1)]
  public interface IClickyInkNoteHost
  {
    // Token: 0x0600169F RID: 5791
    ClickyInkClickyLive ClickyLiveStatus(string slug);

    // Token: 0x060016A0 RID: 5792
    bool IsClickyUnread(string slug);

    // Token: 0x060016A1 RID: 5793
    bool ClickyExists(string slug);

    // Token: 0x060016A2 RID: 5794
    [return: Nullable(2)]
    ClickyInkJob Job(string jobId);

    // Token: 0x060016A3 RID: 5795
    IReadOnlyList<ClickyInkRoutineControl> RoutineControls(string slug, DateTimeOffset now);

    // Token: 0x060016A4 RID: 5796
    void SetRoutinePaused(string routineId, bool isPaused, string source);

    // Token: 0x060016A5 RID: 5797
    void SetRoutineInterval(string routineId, int minutes, string source);

    // Token: 0x060016A6 RID: 5798
    bool RunRoutineNow(string routineId);

    // Token: 0x060016A7 RID: 5799
    void SendToClicky(string slug, string message, string launchContext);

    // Token: 0x060016A8 RID: 5800
    void SendToThread(string threadId, string message, string launchContext);

    // Token: 0x060016A9 RID: 5801
    void OpenJobArtifact(string target, string jobId);

    // Token: 0x060016AA RID: 5802
    [return: Nullable(2)]
    string BoundThreadId(string slug);

    // Token: 0x060016AB RID: 5803
    [NullableContext(2)]
    bool OpenInViewerIfSupported([Nullable(1)] string target, string slug, string threadId);
  }
}
