using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000330 RID: 816
  [NullableContext(1)]
  [Nullable(0)]
  internal static class DockTimer
  {
    // Token: 0x060022D9 RID: 8921 RVA: 0x0062887C File Offset: 0x0062887C
    public static DispatcherQueueTimer After(double seconds, Action action)
    {
      DispatcherQueueTimer t = DispatcherQueue.GetForCurrentThread().CreateTimer();
      t.Interval = TimeSpan.FromSeconds(Math.Max(0.0, seconds));
      t.IsRepeating = false;
      t.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        DockTimer.live.Remove(t);
        action();
      };
      DockTimer.live.Add(t);
      t.Start();
      return t;
    }

    // Token: 0x040010FC RID: 4348
    private static readonly HashSet<DispatcherQueueTimer> live = new HashSet<DispatcherQueueTimer>();
  }
}
