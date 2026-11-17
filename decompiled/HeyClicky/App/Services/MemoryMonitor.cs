using System;
using System.Runtime.CompilerServices;
using System.Threading;
using HeyClicky.Drawing3D;
using HeyClicky.Platform.Input;

namespace HeyClicky.App.Services
{
  // Token: 0x020000E6 RID: 230
  public static class MemoryMonitor
  {
    // Token: 0x06000B49 RID: 2889 RVA: 0x0055B060 File Offset: 0x0055B060
    public static void Start()
    {
      if (MemoryMonitor.timer != null)
      {
        return;
      }
      MemoryMonitor.timer = new Timer(delegate(object _)
      {
        MemoryMonitor.Sample();
      }, null, MemoryMonitor.Interval, MemoryMonitor.Interval);
      MemoryMonitor.Sample();
    }

    // Token: 0x06000B4A RID: 2890 RVA: 0x0055B0B0 File Offset: 0x0055B0B0
    private static void Sample()
    {
      try
      {
        ValueTuple<int, int> liveCounts = ThreeStage.LiveCounts;
        int item = liveCounts.Item1;
        int item2 = liveCounts.Item2;
        int hookReinstallCount = ShortcutMonitor.HookReinstallCount;
        int num = hookReinstallCount - MemoryMonitor.hookReinstallsAtLastSample;
        MemoryMonitor.hookReinstallsAtLastSample = hookReinstallCount;
        string text = "memory";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(56, 4);
        defaultInterpolatedStringHandler.AppendFormatted<MemorySample>(MemorySample.Take());
        defaultInterpolatedStringHandler.AppendLiteral(", 3D stages ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(item);
        defaultInterpolatedStringHandler.AppendLiteral(" (renderers ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(item2);
        defaultInterpolatedStringHandler.AppendLiteral("), keyboard hook reinstalls ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num);
        defaultInterpolatedStringHandler.AppendLiteral("/min");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      }
      catch (Exception ex)
      {
        Log.Warn("memory", "sample failed: " + ex.Message);
      }
    }

    // Token: 0x04000411 RID: 1041
    private static readonly TimeSpan Interval = TimeSpan.FromMinutes(1L);

    // Token: 0x04000412 RID: 1042
    [Nullable(2)]
    private static Timer timer;

    // Token: 0x04000413 RID: 1043
    private static int hookReinstallsAtLastSample;
  }
}
