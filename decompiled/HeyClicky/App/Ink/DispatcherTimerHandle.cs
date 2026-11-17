using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200020D RID: 525
  internal sealed class DispatcherTimerHandle
  {
    // Token: 0x06001936 RID: 6454 RVA: 0x005CF438 File Offset: 0x005CF438
    [NullableContext(1)]
    public static DispatcherTimerHandle After(DispatcherQueue queue, double seconds, Action action)
    {
      DispatcherTimerHandle handle = new DispatcherTimerHandle();
      DispatcherQueueTimer dispatcherQueueTimer = (handle.timer = queue.CreateTimer());
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(seconds);
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        handle.timer = null;
        action();
      };
      dispatcherQueueTimer.Start();
      return handle;
    }

    // Token: 0x06001937 RID: 6455 RVA: 0x005CF4A4 File Offset: 0x005CF4A4
    public void Cancel()
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.timer;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      this.timer = null;
    }

    // Token: 0x04000C4A RID: 3146
    [Nullable(2)]
    private DispatcherQueueTimer timer;
  }
}
