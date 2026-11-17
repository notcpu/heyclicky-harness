using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200020A RID: 522
  public static class ClickyEdgeStackInkReadings
  {
    // Token: 0x0600190F RID: 6415 RVA: 0x005CEA6C File Offset: 0x005CEA6C
    [NullableContext(1)]
    public static ClickyEdgeStackInkReading Make(ClickyInkLiveNote note, double clock, ClickyInkCardSubject thing, double? seenClock, ClickyInkActionFeedback feedback)
    {
      bool isRunning = thing.IsRunning;
      ClickyInkCardSubject.SubjectPhase phase = thing.Phase;
      bool flag = phase - ClickyInkCardSubject.SubjectPhase.NeedsYou <= 1;
      bool flag2 = flag;
      ClickyEdgeStackInkReading.State state;
      switch (thing.Status)
      {
      case ClickyThingStatus.Watching:
        state = ClickyEdgeStackInkReading.State.Watching;
        goto IL_0059;
      case ClickyThingStatus.Ready:
        state = ClickyEdgeStackInkReading.State.Ready;
        goto IL_0059;
      case ClickyThingStatus.Paused:
        state = ClickyEdgeStackInkReading.State.Paused;
        goto IL_0059;
      }
      state = ClickyEdgeStackInkReading.State.Done;
      IL_0059:
      return new ClickyEdgeStackInkReading(note, clock, isRunning, flag2, state, seenClock, feedback);
    }
  }
}
