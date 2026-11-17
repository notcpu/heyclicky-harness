using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace HeyClicky
{
  // Token: 0x02000033 RID: 51
  [NullableContext(1)]
  [Nullable(0)]
  public static class FullscreenGuard
  {
    // Token: 0x170000A2 RID: 162
    // (get) Token: 0x0600026F RID: 623 RVA: 0x00020DE8 File Offset: 0x00020DE8
    // (set) Token: 0x06000270 RID: 624 RVA: 0x00020DF0 File Offset: 0x00020DF0
    public static IntPtr TestGameWindow { get; set; }

    // Token: 0x06000271 RID: 625 RVA: 0x00020DF8 File Offset: 0x00020DF8
    public static IntPtr GameWindow()
    {
      IntPtr intPtr = FullscreenGuard.Find();
      if (Interlocked.Exchange(ref FullscreenGuard.lastGame, intPtr) != intPtr)
      {
        Log.Info("fullscreen", (intPtr != 0) ? (FullscreenGuard.Describe(intPtr) + " owns its screen (" + FullscreenGuard.NotificationState() + "): HeyClicky's windows stay under it so it keeps fullscreen; the voice still works") : "no fullscreen game in front: HeyClicky's windows go back on top");
      }
      return intPtr;
    }

    // Token: 0x06000272 RID: 626 RVA: 0x00020E48 File Offset: 0x00020E48
    public static IntPtr TopmostInsertAfter()
    {
      IntPtr intPtr = FullscreenGuard.GameWindow();
      if (intPtr == 0)
      {
        return FullscreenGuard.Topmost;
      }
      return intPtr;
    }

    // Token: 0x06000273 RID: 627 RVA: 0x00020E68 File Offset: 0x00020E68
    private static IntPtr Find()
    {
      IntPtr testGameWindow = FullscreenGuard.TestGameWindow;
      if (testGameWindow != 0)
      {
        if (!FullscreenGuard.IsWindowVisible(testGameWindow))
        {
          return (IntPtr)0;
        }
        return testGameWindow;
      }
      else
      {
        IntPtr foregroundWindow = FullscreenGuard.GetForegroundWindow();
        if (foregroundWindow == 0 || !FullscreenGuard.IsWindowVisible(foregroundWindow) || FullscreenGuard.IsIconic(foregroundWindow))
        {
          return (IntPtr)0;
        }
        uint num;
        FullscreenGuard.GetWindowThreadProcessId(foregroundWindow, out num);
        if ((ulong)num == (ulong)((long)Environment.ProcessId))
        {
          return (IntPtr)0;
        }
        if ((FullscreenGuard.GetWindowLongPtr(foregroundWindow, -20) & 8L) == 0L)
        {
          return (IntPtr)0;
        }
        FullscreenGuard.RECT rect;
        if (!FullscreenGuard.GetWindowRect(foregroundWindow, out rect))
        {
          return (IntPtr)0;
        }
        FullscreenGuard.MONITORINFO monitorinfo = new FullscreenGuard.MONITORINFO
        {
          cbSize = Marshal.SizeOf<FullscreenGuard.MONITORINFO>()
        };
        if (!FullscreenGuard.GetMonitorInfo(FullscreenGuard.MonitorFromWindow(foregroundWindow, 2U), ref monitorinfo))
        {
          return (IntPtr)0;
        }
        FullscreenGuard.RECT rcMonitor = monitorinfo.rcMonitor;
        if (rect.Left > rcMonitor.Left || rect.Top > rcMonitor.Top || rect.Right < rcMonitor.Right || rect.Bottom < rcMonitor.Bottom)
        {
          return (IntPtr)0;
        }
        return foregroundWindow;
      }
    }

    // Token: 0x06000274 RID: 628 RVA: 0x00020F50 File Offset: 0x00020F50
    public static string Describe(IntPtr hwnd)
    {
      char[] array = new char[96];
      int className = FullscreenGuard.GetClassName(hwnd, array, array.Length);
      return FullscreenGuard.ProcessName(hwnd) + " [" + new string(array, 0, Math.Max(0, className)) + "]";
    }

    // Token: 0x06000275 RID: 629 RVA: 0x00020F94 File Offset: 0x00020F94
    public static string ProcessName(IntPtr hwnd)
    {
      uint num;
      FullscreenGuard.GetWindowThreadProcessId(hwnd, out num);
      string text;
      try
      {
        text = Process.GetProcessById((int)num).ProcessName;
      }
      catch
      {
        text = num.ToString();
      }
      return text;
    }

    // Token: 0x06000276 RID: 630 RVA: 0x00020FD4 File Offset: 0x00020FD4
    public static string NotificationState()
    {
      int num = 0;
      try
      {
        if (FullscreenGuard.SHQueryUserNotificationState(out num) != 0)
        {
          num = 0;
        }
      }
      catch
      {
      }
      string text;
      switch (num)
      {
      case 2:
        text = "shell: busy";
        break;
      case 3:
        text = "shell: D3D fullscreen";
        break;
      case 4:
        text = "shell: presentation";
        break;
      case 5:
        text = "shell: accepts notifications";
        break;
      case 6:
        text = "shell: quiet time";
        break;
      case 7:
        text = "shell: immersive app";
        break;
      default:
        text = "shell: unknown";
        break;
      }
      return text;
    }

    // Token: 0x06000277 RID: 631
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    // Token: 0x06000278 RID: 632
    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hwnd);

    // Token: 0x06000279 RID: 633
    [DllImport("user32.dll")]
    private static extern bool IsIconic(IntPtr hwnd);

    // Token: 0x0600027A RID: 634
    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint pid);

    // Token: 0x0600027B RID: 635
    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW")]
    private static extern long GetWindowLongPtr(IntPtr hwnd, int index);

    // Token: 0x0600027C RID: 636
    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hwnd, out FullscreenGuard.RECT rect);

    // Token: 0x0600027D RID: 637
    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint flags);

    // Token: 0x0600027E RID: 638
    [DllImport("user32.dll", EntryPoint = "GetMonitorInfoW")]
    private static extern bool GetMonitorInfo(IntPtr monitor, ref FullscreenGuard.MONITORINFO info);

    // Token: 0x0600027F RID: 639
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetClassName(IntPtr hwnd, char[] name, int max);

    // Token: 0x06000280 RID: 640
    [DllImport("shell32.dll")]
    private static extern int SHQueryUserNotificationState(out int state);

    // Token: 0x040000AA RID: 170
    private static readonly IntPtr Topmost = (IntPtr)(-1);

    // Token: 0x040000AB RID: 171
    private static IntPtr lastGame;

    // Token: 0x040000AD RID: 173
    private const int GWL_EXSTYLE = -20;

    // Token: 0x040000AE RID: 174
    private const long WS_EX_TOPMOST = 8L;

    // Token: 0x0200005A RID: 90
    [NullableContext(0)]
    private struct RECT
    {
      // Token: 0x040000FF RID: 255
      public int Left;

      // Token: 0x04000100 RID: 256
      public int Top;

      // Token: 0x04000101 RID: 257
      public int Right;

      // Token: 0x04000102 RID: 258
      public int Bottom;
    }

    // Token: 0x0200005B RID: 91
    [NullableContext(0)]
    private struct MONITORINFO
    {
      // Token: 0x04000103 RID: 259
      public int cbSize;

      // Token: 0x04000104 RID: 260
      public FullscreenGuard.RECT rcMonitor;

      // Token: 0x04000105 RID: 261
      public FullscreenGuard.RECT rcWork;

      // Token: 0x04000106 RID: 262
      public uint dwFlags;
    }
  }
}
