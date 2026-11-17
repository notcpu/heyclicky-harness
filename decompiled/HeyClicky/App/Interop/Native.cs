using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using HeyClicky.App.Shell;

namespace HeyClicky.App.Interop
{
  // Token: 0x02000199 RID: 409
  public static class Native
  {
    // Token: 0x06001608 RID: 5640
    [DllImport("user32.dll")]
    private static extern bool GetLastInputInfo(ref Native.LASTINPUTINFO info);

    // Token: 0x06001609 RID: 5641
    [DllImport("user32.dll")]
    public static extern bool IsIconic(IntPtr hwnd);

    // Token: 0x0600160A RID: 5642 RVA: 0x005B9FAC File Offset: 0x005B9FAC
    public static long MillisecondsSinceLastInput()
    {
      Native.LASTINPUTINFO lastinputinfo = new Native.LASTINPUTINFO
      {
        cbSize = (uint)Marshal.SizeOf<Native.LASTINPUTINFO>()
      };
      if (!Native.GetLastInputInfo(ref lastinputinfo))
      {
        return 0L;
      }
      return (long)((ulong)(Environment.TickCount - (int)lastinputinfo.dwTime));
    }

    // Token: 0x0600160B RID: 5643
    [DllImport("user32.dll", EntryPoint = "SetClassLongPtrW")]
    public static extern IntPtr SetClassLongPtr(IntPtr hwnd, int index, IntPtr value);

    // Token: 0x0600160C RID: 5644
    [DllImport("gdi32.dll")]
    public static extern IntPtr GetStockObject(int index);

    // Token: 0x0600160D RID: 5645
    [DllImport("user32.dll")]
    public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint key, byte alpha, uint flags);

    // Token: 0x0600160E RID: 5646
    [DllImport("user32.dll")]
    public static extern bool GetClientRect(IntPtr hwnd, out Native.RECT rc);

    // Token: 0x0600160F RID: 5647
    [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
    public static extern bool GetWindowRectPublic(IntPtr hwnd, out Native.RECT rc);

    // Token: 0x06001610 RID: 5648
    [DllImport("user32.dll")]
    public static extern int FillRect(IntPtr hdc, ref Native.RECT rc, IntPtr brush);

    // Token: 0x06001611 RID: 5649
    [NullableContext(1)]
    [DllImport("user32.dll")]
    public static extern bool EnumChildWindows(IntPtr parent, Native.EnumWindowsProc proc, IntPtr data);

    // Token: 0x06001612 RID: 5650
    [DllImport("dwmapi.dll")]
    public static extern int DwmEnableBlurBehindWindow(IntPtr hwnd, ref Native.DWM_BLURBEHIND bb);

    // Token: 0x06001613 RID: 5651 RVA: 0x005B9FE8 File Offset: 0x005B9FE8
    public static void EnableAlphaComposition(IntPtr hwnd)
    {
      IntPtr intPtr = Native.CreateRectRgn(0, 0, -1, -1);
      Native.DWM_BLURBEHIND dwm_BLURBEHIND = new Native.DWM_BLURBEHIND
      {
        dwFlags = 3U,
        fEnable = 1,
        hRgnBlur = intPtr
      };
      Native.DwmEnableBlurBehindWindow(hwnd, ref dwm_BLURBEHIND);
      Native.DeleteObject(intPtr);
    }

    // Token: 0x06001614 RID: 5652
    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW")]
    public static extern IntPtr GetWindowLongPtr(IntPtr hwnd, int index);

    // Token: 0x06001615 RID: 5653
    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
    public static extern IntPtr SetWindowLongPtr(IntPtr hwnd, int index, IntPtr value);

    // Token: 0x06001616 RID: 5654
    [DllImport("user32.dll")]
    public static extern bool SetWindowPos(IntPtr hwnd, IntPtr after, int x, int y, int cx, int cy, uint flags);

    // Token: 0x06001617 RID: 5655
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hwnd, int cmd);

    // Token: 0x06001618 RID: 5656
    [DllImport("user32.dll")]
    public static extern bool IsWindowVisible(IntPtr hwnd);

    // Token: 0x06001619 RID: 5657
    [DllImport("user32.dll")]
    public static extern bool IsWindow(IntPtr hwnd);

    // Token: 0x0600161A RID: 5658
    [DllImport("user32.dll")]
    public static extern int SetWindowRgn(IntPtr hwnd, IntPtr hrgn, bool redraw);

    // Token: 0x0600161B RID: 5659
    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateRectRgn(int l, int t, int r, int b);

    // Token: 0x0600161C RID: 5660
    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateRoundRectRgn(int l, int t, int r, int b, int w, int h);

    // Token: 0x0600161D RID: 5661
    [DllImport("gdi32.dll")]
    public static extern int CombineRgn(IntPtr dst, IntPtr a, IntPtr b, int mode);

    // Token: 0x0600161E RID: 5662
    [DllImport("gdi32.dll")]
    public static extern bool DeleteObject(IntPtr obj);

    // Token: 0x0600161F RID: 5663
    [DllImport("gdi32.dll")]
    public static extern int OffsetRgn(IntPtr rgn, int x, int y);

    // Token: 0x06001620 RID: 5664
    [DllImport("gdi32.dll")]
    public static extern bool EqualRgn(IntPtr a, IntPtr b);

    // Token: 0x06001621 RID: 5665
    [DllImport("user32.dll")]
    public static extern int GetWindowRgn(IntPtr hwnd, IntPtr rgn);

    // Token: 0x06001622 RID: 5666
    [DllImport("user32.dll")]
    private static extern IntPtr GetWindow(IntPtr hwnd, uint cmd);

    // Token: 0x06001623 RID: 5667 RVA: 0x005BA034 File Offset: 0x005BA034
    [NullableContext(1)]
    public static List<IntPtr> DirectChildren(IntPtr hwnd)
    {
      List<IntPtr> list = new List<IntPtr>();
      for (IntPtr intPtr = Native.GetWindow(hwnd, 5U); intPtr != 0; intPtr = Native.GetWindow(intPtr, 2U))
      {
        list.Add(intPtr);
      }
      return list;
    }

    // Token: 0x06001624 RID: 5668
    [DllImport("user32.dll")]
    public static extern bool SetWindowDisplayAffinity(IntPtr hwnd, uint affinity);

    // Token: 0x06001625 RID: 5669
    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    // Token: 0x06001626 RID: 5670
    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hwnd);

    // Token: 0x06001627 RID: 5671
    [DllImport("user32.dll")]
    public static extern IntPtr SetFocus(IntPtr hwnd);

    // Token: 0x06001628 RID: 5672
    [DllImport("user32.dll")]
    public static extern IntPtr SetActiveWindow(IntPtr hwnd);

    // Token: 0x06001629 RID: 5673
    [DllImport("user32.dll")]
    public static extern bool BringWindowToTop(IntPtr hwnd);

    // Token: 0x0600162A RID: 5674
    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint pid);

    // Token: 0x0600162B RID: 5675
    [DllImport("user32.dll")]
    public static extern IntPtr WindowFromPoint(Native.POINT point);

    // Token: 0x0600162C RID: 5676
    [DllImport("user32.dll")]
    public static extern IntPtr GetAncestor(IntPtr hwnd, uint flags);

    // Token: 0x0600162D RID: 5677 RVA: 0x005BA064 File Offset: 0x005BA064
    public static bool IsOwnWindowAt(int x, int y)
    {
      return Native.IsOwnWindow(Native.WindowFromPoint(new Native.POINT
      {
        X = x,
        Y = y
      }));
    }

    // Token: 0x0600162E RID: 5678 RVA: 0x005BA094 File Offset: 0x005BA094
    public static bool IsOwnWindow(IntPtr hwnd)
    {
      if (hwnd == 0)
      {
        return false;
      }
      uint num;
      Native.GetWindowThreadProcessId(hwnd, out num);
      return num == (uint)Environment.ProcessId;
    }

    // Token: 0x0600162F RID: 5679 RVA: 0x005BA0B8 File Offset: 0x005BA0B8
    [NullableContext(1)]
    public static string DescribeForeground(IntPtr hwnd)
    {
      if (hwnd == 0)
      {
        return "nothing";
      }
      if (!Native.IsOwnWindow(hwnd))
      {
        return FullscreenGuard.Describe(hwnd);
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 1);
      defaultInterpolatedStringHandler.AppendLiteral("HeyClicky's own window ");
      defaultInterpolatedStringHandler.AppendFormatted<IntPtr>(hwnd);
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06001630 RID: 5680
    [DllImport("kernel32.dll")]
    public static extern uint GetCurrentThreadId();

    // Token: 0x06001631 RID: 5681
    [DllImport("user32.dll")]
    public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool attach);

    // Token: 0x06001632 RID: 5682
    [DllImport("user32.dll")]
    public static extern bool AllowSetForegroundWindow(int pid);

    // Token: 0x06001633 RID: 5683
    [DllImport("user32.dll")]
    public static extern void keybd_event(byte vk, byte scan, uint flags, UIntPtr extra);

    // Token: 0x06001634 RID: 5684
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out Native.POINT p);

    // Token: 0x06001635 RID: 5685
    [DllImport("user32.dll")]
    public static extern IntPtr MonitorFromPoint(Native.POINT p, int flags);

    // Token: 0x06001636 RID: 5686
    [DllImport("user32.dll")]
    public static extern IntPtr MonitorFromWindow(IntPtr hwnd, int flags);

    // Token: 0x06001637 RID: 5687
    [DllImport("user32.dll", EntryPoint = "GetMonitorInfoW")]
    public static extern bool GetMonitorInfo(IntPtr mon, ref Native.MONITORINFO info);

    // Token: 0x06001638 RID: 5688
    [DllImport("shcore.dll")]
    public static extern int GetDpiForMonitor(IntPtr mon, int type, out uint dpiX, out uint dpiY);

    // Token: 0x06001639 RID: 5689
    [DllImport("user32.dll")]
    public static extern uint GetDpiForWindow(IntPtr hwnd);

    // Token: 0x0600163A RID: 5690
    [DllImport("user32.dll", EntryPoint = "CallWindowProcW")]
    public static extern IntPtr CallWindowProc(IntPtr prev, IntPtr hwnd, uint msg, IntPtr w, IntPtr l);

    // Token: 0x0600163B RID: 5691
    [DllImport("user32.dll", EntryPoint = "DefWindowProcW")]
    public static extern IntPtr DefWindowProc(IntPtr hwnd, uint msg, IntPtr w, IntPtr l);

    // Token: 0x0600163C RID: 5692
    [DllImport("user32.dll", EntryPoint = "SendMessageW")]
    public static extern IntPtr SendMessage(IntPtr hwnd, uint msg, IntPtr w, IntPtr l);

    // Token: 0x0600163D RID: 5693
    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();

    // Token: 0x0600163E RID: 5694
    [DllImport("user32.dll", EntryPoint = "PostMessageW")]
    public static extern bool PostMessage(IntPtr hwnd, uint msg, IntPtr w, IntPtr l);

    // Token: 0x0600163F RID: 5695
    [DllImport("user32.dll")]
    public static extern bool DestroyWindow(IntPtr hwnd);

    // Token: 0x06001640 RID: 5696
    [DllImport("user32.dll")]
    public static extern short GetAsyncKeyState(int vk);

    // Token: 0x06001641 RID: 5697
    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int index);

    // Token: 0x06001642 RID: 5698 RVA: 0x005BA104 File Offset: 0x005BA104
    public static bool IsPrimaryButtonDown()
    {
      return ((int)Native.GetAsyncKeyState((Native.GetSystemMetrics(23) != 0) ? 2 : 1) & 32768) != 0;
    }

    // Token: 0x06001643 RID: 5699
    [NullableContext(1)]
    [DllImport("user32.dll")]
    public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr clip, Native.MonitorEnumProc proc, IntPtr data);

    // Token: 0x06001644 RID: 5700
    [DllImport("dwmapi.dll")]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int value, int size);

    // Token: 0x06001645 RID: 5701
    [DllImport("dwmapi.dll")]
    public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref Native.MARGINS m);

    // Token: 0x06001646 RID: 5702
    [DllImport("CoreMessaging.dll")]
    public static extern int CreateDispatcherQueueController(Native.DispatcherQueueOptions options, out IntPtr controller);

    // Token: 0x06001647 RID: 5703 RVA: 0x005BA124 File Offset: 0x005BA124
    public static void AddExStyle(IntPtr hwnd, uint add, uint remove = 0U)
    {
      uint num = (uint)Native.GetWindowLongPtr(hwnd, -20);
      num = (num | add) & ~remove;
      Native.SetWindowLongPtr(hwnd, -20, (IntPtr)((UIntPtr)num));
    }

    // Token: 0x06001648 RID: 5704 RVA: 0x005BA150 File Offset: 0x005BA150
    public static void SetStyle(IntPtr hwnd, uint add, uint remove)
    {
      uint num = (uint)Native.GetWindowLongPtr(hwnd, -16);
      num = (num | add) & ~remove;
      Native.SetWindowLongPtr(hwnd, -16, (IntPtr)((UIntPtr)num));
    }

    // Token: 0x06001649 RID: 5705 RVA: 0x005BA17C File Offset: 0x005BA17C
    public static void ForceForeground(IntPtr hwnd)
    {
      if (OverlayWindow.Offscreen)
      {
        return;
      }
      IntPtr foregroundWindow = Native.GetForegroundWindow();
      if (foregroundWindow == hwnd)
      {
        return;
      }
      uint num2;
      uint num = ((foregroundWindow != 0) ? Native.GetWindowThreadProcessId(foregroundWindow, out num2) : 0U);
      uint currentThreadId = Native.GetCurrentThreadId();
      bool flag = num != 0U && num != currentThreadId && Native.AttachThreadInput(currentThreadId, num, true);
      try
      {
        Native.BringWindowToTop(hwnd);
        if (!Native.SetForegroundWindow(hwnd))
        {
          Native.keybd_event(18, 0, 0U, (UIntPtr)((IntPtr)0));
          Native.keybd_event(byte.MaxValue, 0, 0U, (UIntPtr)((IntPtr)0));
          Native.keybd_event(byte.MaxValue, 0, 2U, (UIntPtr)((IntPtr)0));
          Native.keybd_event(18, 0, 2U, (UIntPtr)((IntPtr)0));
          if (!Native.SetForegroundWindow(hwnd))
          {
            string text = "window";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(71, 3);
            defaultInterpolatedStringHandler.AppendLiteral("couldn't bring window ");
            defaultInterpolatedStringHandler.AppendFormatted<IntPtr>(hwnd);
            defaultInterpolatedStringHandler.AppendLiteral(" to the foreground (in front: ");
            defaultInterpolatedStringHandler.AppendFormatted(Native.DescribeForeground(Native.GetForegroundWindow()));
            defaultInterpolatedStringHandler.AppendLiteral(", input attached: ");
            defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
            defaultInterpolatedStringHandler.AppendLiteral(")");
            Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
          }
        }
      }
      finally
      {
        if (flag)
        {
          Native.AttachThreadInput(currentThreadId, num, false);
        }
      }
      IntPtr foregroundWindow2 = Native.GetForegroundWindow();
      if (foregroundWindow2 != hwnd)
      {
        string text2 = "window";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(88, 4);
        defaultInterpolatedStringHandler.AppendLiteral("window ");
        defaultInterpolatedStringHandler.AppendFormatted<IntPtr>(hwnd);
        defaultInterpolatedStringHandler.AppendLiteral(" still isn't in front after ForceForeground (in front: ");
        defaultInterpolatedStringHandler.AppendFormatted(Native.DescribeForeground(foregroundWindow2));
        defaultInterpolatedStringHandler.AppendLiteral(", was: ");
        defaultInterpolatedStringHandler.AppendFormatted(Native.DescribeForeground(foregroundWindow));
        defaultInterpolatedStringHandler.AppendLiteral(", input attached: ");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Warn(text2, defaultInterpolatedStringHandler.ToStringAndClear());
      }
    }

    // Token: 0x04000AB7 RID: 2743
    public const int GWL_STYLE = -16;

    // Token: 0x04000AB8 RID: 2744
    public const int GWL_EXSTYLE = -20;

    // Token: 0x04000AB9 RID: 2745
    public const int GWLP_WNDPROC = -4;

    // Token: 0x04000ABA RID: 2746
    public const int GCLP_HBRBACKGROUND = -10;

    // Token: 0x04000ABB RID: 2747
    public const int BLACK_BRUSH = 4;

    // Token: 0x04000ABC RID: 2748
    public const uint WS_POPUP = 2147483648U;

    // Token: 0x04000ABD RID: 2749
    public const uint WS_VISIBLE = 268435456U;

    // Token: 0x04000ABE RID: 2750
    public const uint WS_CLIPCHILDREN = 33554432U;

    // Token: 0x04000ABF RID: 2751
    public const uint WS_CAPTION = 12582912U;

    // Token: 0x04000AC0 RID: 2752
    public const uint WS_THICKFRAME = 262144U;

    // Token: 0x04000AC1 RID: 2753
    public const uint WS_SYSMENU = 524288U;

    // Token: 0x04000AC2 RID: 2754
    public const uint WS_MINIMIZEBOX = 131072U;

    // Token: 0x04000AC3 RID: 2755
    public const uint WS_MAXIMIZEBOX = 65536U;

    // Token: 0x04000AC4 RID: 2756
    public const uint WS_EX_TOPMOST = 8U;

    // Token: 0x04000AC5 RID: 2757
    public const uint WS_EX_TRANSPARENT = 32U;

    // Token: 0x04000AC6 RID: 2758
    public const uint WS_EX_TOOLWINDOW = 128U;

    // Token: 0x04000AC7 RID: 2759
    public const uint WS_EX_APPWINDOW = 262144U;

    // Token: 0x04000AC8 RID: 2760
    public const uint WS_EX_LAYERED = 524288U;

    // Token: 0x04000AC9 RID: 2761
    public const uint WS_EX_NOACTIVATE = 134217728U;

    // Token: 0x04000ACA RID: 2762
    public const uint SWP_NOSIZE = 1U;

    // Token: 0x04000ACB RID: 2763
    public const uint SWP_NOMOVE = 2U;

    // Token: 0x04000ACC RID: 2764
    public const uint SWP_NOZORDER = 4U;

    // Token: 0x04000ACD RID: 2765
    public const uint SWP_NOACTIVATE = 16U;

    // Token: 0x04000ACE RID: 2766
    public const uint SWP_FRAMECHANGED = 32U;

    // Token: 0x04000ACF RID: 2767
    public const uint SWP_SHOWWINDOW = 64U;

    // Token: 0x04000AD0 RID: 2768
    public const uint SWP_HIDEWINDOW = 128U;

    // Token: 0x04000AD1 RID: 2769
    public static readonly IntPtr HWND_TOPMOST = (IntPtr)(-1);

    // Token: 0x04000AD2 RID: 2770
    public const int SW_HIDE = 0;

    // Token: 0x04000AD3 RID: 2771
    public const int SW_SHOWNOACTIVATE = 4;

    // Token: 0x04000AD4 RID: 2772
    public const int SW_SHOWNA = 8;

    // Token: 0x04000AD5 RID: 2773
    public const int SW_SHOW = 5;

    // Token: 0x04000AD6 RID: 2774
    public const uint WDA_NONE = 0U;

    // Token: 0x04000AD7 RID: 2775
    public const uint WDA_EXCLUDEFROMCAPTURE = 17U;

    // Token: 0x04000AD8 RID: 2776
    public const uint WM_MOUSEACTIVATE = 33U;

    // Token: 0x04000AD9 RID: 2777
    public const int MA_NOACTIVATE = 3;

    // Token: 0x04000ADA RID: 2778
    public const uint WM_DPICHANGED = 736U;

    // Token: 0x04000ADB RID: 2779
    public const uint WM_DISPLAYCHANGE = 126U;

    // Token: 0x04000ADC RID: 2780
    public const uint WM_SETTINGCHANGE = 26U;

    // Token: 0x04000ADD RID: 2781
    public const uint WM_ACTIVATE = 6U;

    // Token: 0x04000ADE RID: 2782
    public const uint WM_NCACTIVATE = 134U;

    // Token: 0x04000ADF RID: 2783
    public const uint WM_CLOSE = 16U;

    // Token: 0x04000AE0 RID: 2784
    public const uint WM_DESTROY = 2U;

    // Token: 0x04000AE1 RID: 2785
    public const uint WM_USER = 1024U;

    // Token: 0x04000AE2 RID: 2786
    public const uint WM_APP = 32768U;

    // Token: 0x04000AE3 RID: 2787
    public const uint WM_COMMAND = 273U;

    // Token: 0x04000AE4 RID: 2788
    public const uint WM_LBUTTONUP = 514U;

    // Token: 0x04000AE5 RID: 2789
    public const uint WM_RBUTTONUP = 517U;

    // Token: 0x04000AE6 RID: 2790
    public const uint WM_LBUTTONDBLCLK = 515U;

    // Token: 0x04000AE7 RID: 2791
    public const uint WM_CONTEXTMENU = 123U;

    // Token: 0x04000AE8 RID: 2792
    public const uint WM_COPYDATA = 74U;

    // Token: 0x04000AE9 RID: 2793
    public const int MONITOR_DEFAULTTONEAREST = 2;

    // Token: 0x04000AEA RID: 2794
    public const int MONITOR_DEFAULTTOPRIMARY = 1;

    // Token: 0x04000AEB RID: 2795
    public const int RGN_COPY = 5;

    // Token: 0x04000AEC RID: 2796
    public const uint GA_ROOT = 2U;

    // Token: 0x04000AED RID: 2797
    private const byte VK_MENU = 18;

    // Token: 0x04000AEE RID: 2798
    private const byte VK_MASK_KEY = 255;

    // Token: 0x04000AEF RID: 2799
    private const uint KEYEVENTF_KEYUP = 2U;

    // Token: 0x04000AF0 RID: 2800
    public const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;

    // Token: 0x04000AF1 RID: 2801
    public const int DWMWA_BORDER_COLOR = 34;

    // Token: 0x04000AF2 RID: 2802
    public const int DWMWA_TRANSITIONS_FORCEDISABLED = 3;

    // Token: 0x04000AF3 RID: 2803
    public const int DWMWCP_DONOTROUND = 1;

    // Token: 0x04000AF4 RID: 2804
    public const int DWMWCP_ROUND = 2;

    // Token: 0x04000AF5 RID: 2805
    public const int DWMWA_COLOR_NONE = -2;

    // Token: 0x04000AF6 RID: 2806
    public const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

    // Token: 0x04000AF7 RID: 2807
    public const int DWMWA_SYSTEMBACKDROP_TYPE = 38;

    // Token: 0x02000741 RID: 1857
    private struct LASTINPUTINFO
    {
      // Token: 0x0400202A RID: 8234
      public uint cbSize;

      // Token: 0x0400202B RID: 8235
      public uint dwTime;
    }

    // Token: 0x02000742 RID: 1858
    // (Invoke) Token: 0x060034C3 RID: 13507
    public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr data);

    // Token: 0x02000743 RID: 1859
    public struct DWM_BLURBEHIND
    {
      // Token: 0x0400202C RID: 8236
      public uint dwFlags;

      // Token: 0x0400202D RID: 8237
      public int fEnable;

      // Token: 0x0400202E RID: 8238
      public IntPtr hRgnBlur;

      // Token: 0x0400202F RID: 8239
      public int fTransitionOnMaximized;
    }

    // Token: 0x02000744 RID: 1860
    public struct POINT
    {
      // Token: 0x04002030 RID: 8240
      public int X;

      // Token: 0x04002031 RID: 8241
      public int Y;
    }

    // Token: 0x02000745 RID: 1861
    public struct RECT
    {
      // Token: 0x170007AF RID: 1967
      // (get) Token: 0x060034C6 RID: 13510 RVA: 0x0067AFD4 File Offset: 0x0067AFD4
      public int Width
      {
        get
        {
          return this.Right - this.Left;
        }
      }

      // Token: 0x170007B0 RID: 1968
      // (get) Token: 0x060034C7 RID: 13511 RVA: 0x0067AFE4 File Offset: 0x0067AFE4
      public int Height
      {
        get
        {
          return this.Bottom - this.Top;
        }
      }

      // Token: 0x04002032 RID: 8242
      public int Left;

      // Token: 0x04002033 RID: 8243
      public int Top;

      // Token: 0x04002034 RID: 8244
      public int Right;

      // Token: 0x04002035 RID: 8245
      public int Bottom;
    }

    // Token: 0x02000746 RID: 1862
    public struct MONITORINFO
    {
      // Token: 0x04002036 RID: 8246
      public int cbSize;

      // Token: 0x04002037 RID: 8247
      public Native.RECT rcMonitor;

      // Token: 0x04002038 RID: 8248
      public Native.RECT rcWork;

      // Token: 0x04002039 RID: 8249
      public uint dwFlags;
    }

    // Token: 0x02000747 RID: 1863
    // (Invoke) Token: 0x060034C9 RID: 13513
    public delegate bool MonitorEnumProc(IntPtr mon, IntPtr hdc, ref Native.RECT rc, IntPtr data);

    // Token: 0x02000748 RID: 1864
    public struct MARGINS
    {
      // Token: 0x0400203A RID: 8250
      public int L;

      // Token: 0x0400203B RID: 8251
      public int R;

      // Token: 0x0400203C RID: 8252
      public int T;

      // Token: 0x0400203D RID: 8253
      public int B;
    }

    // Token: 0x02000749 RID: 1865
    public struct DispatcherQueueOptions
    {
      // Token: 0x0400203E RID: 8254
      public int dwSize;

      // Token: 0x0400203F RID: 8255
      public int threadType;

      // Token: 0x04002040 RID: 8256
      public int apartmentType;
    }
  }
}
