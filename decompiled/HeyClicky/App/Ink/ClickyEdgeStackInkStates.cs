using System;
using HeyClicky.Core.Ink;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001AA RID: 426
  public static class ClickyEdgeStackInkStates
  {
    // Token: 0x060016F8 RID: 5880 RVA: 0x005BE4A4 File Offset: 0x005BE4A4
    public static ClickyThingStatus Status(this ClickyEdgeStackInkReading.State state)
    {
      ClickyThingStatus clickyThingStatus;
      switch (state)
      {
      case ClickyEdgeStackInkReading.State.Writing:
        clickyThingStatus = ClickyThingStatus.Working;
        break;
      case ClickyEdgeStackInkReading.State.NeedsYou:
        clickyThingStatus = ClickyThingStatus.NeedsYou;
        break;
      case ClickyEdgeStackInkReading.State.Ready:
        clickyThingStatus = ClickyThingStatus.Ready;
        break;
      case ClickyEdgeStackInkReading.State.Done:
        clickyThingStatus = ClickyThingStatus.Done;
        break;
      case ClickyEdgeStackInkReading.State.Watching:
        clickyThingStatus = ClickyThingStatus.Watching;
        break;
      default:
        clickyThingStatus = ClickyThingStatus.Paused;
        break;
      }
      return clickyThingStatus;
    }

    // Token: 0x060016F9 RID: 5881 RVA: 0x005BE4E4 File Offset: 0x005BE4E4
    public static bool WantsAttention(this ClickyEdgeStackInkReading.State state)
    {
      return state <= ClickyEdgeStackInkReading.State.Ready;
    }
  }
}
