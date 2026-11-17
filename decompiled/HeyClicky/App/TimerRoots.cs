using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App
{
  // Token: 0x02000082 RID: 130
  [NullableContext(1)]
  [Nullable(0)]
  public static class TimerRoots
  {
    // Token: 0x060004FB RID: 1275 RVA: 0x0052BFA8 File Offset: 0x0052BFA8
    public static DispatcherQueueTimer Rooted(this DispatcherQueueTimer timer)
    {
      HashSet<DispatcherQueueTimer> hashSet = TimerRoots.live;
      lock (hashSet)
      {
        TimerRoots.live.Add(timer);
      }
      timer.Tick += delegate(DispatcherQueueTimer t, object _)
      {
        if (!t.IsRepeating)
        {
          HashSet<DispatcherQueueTimer> hashSet2 = TimerRoots.live;
          lock (hashSet2)
          {
            TimerRoots.live.Remove(t);
          }
        }
      };
      return timer;
    }

    // Token: 0x060004FC RID: 1276 RVA: 0x0052C014 File Offset: 0x0052C014
    public static void Release(DispatcherQueueTimer timer)
    {
      timer.Stop();
      HashSet<DispatcherQueueTimer> hashSet = TimerRoots.live;
      lock (hashSet)
      {
        TimerRoots.live.Remove(timer);
      }
    }

    // Token: 0x040000FE RID: 254
    private static readonly HashSet<DispatcherQueueTimer> live = new HashSet<DispatcherQueueTimer>();
  }
}
