using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.UI
{
  // Token: 0x020000B6 RID: 182
  [NullableContext(1)]
  [Nullable(0)]
  public static class Once
  {
    // Token: 0x06000775 RID: 1909 RVA: 0x005388B4 File Offset: 0x005388B4
    public static DispatcherQueueTimer After(double seconds, Action action, [Nullable(2)] DispatcherQueue queue = null)
    {
      DispatcherQueueTimer t = (queue ?? DispatcherQueue.GetForCurrentThread()).CreateTimer();
      t.Interval = TimeSpan.FromSeconds(Math.Max(0.0, seconds));
      t.IsRepeating = false;
      t.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        Once.live.Remove(t);
        action();
      };
      Once.live.Add(t);
      t.Start();
      return t;
    }

    // Token: 0x0400023A RID: 570
    private static readonly HashSet<DispatcherQueueTimer> live = new HashSet<DispatcherQueueTimer>();
  }
}
