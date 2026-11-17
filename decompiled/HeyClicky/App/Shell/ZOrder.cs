using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using HeyClicky.App.Interop;

namespace HeyClicky.App.Shell
{
  // Token: 0x020000C4 RID: 196
  [NullableContext(1)]
  [Nullable(0)]
  public static class ZOrder
  {
    // Token: 0x06000841 RID: 2113
    [DllImport("user32.dll")]
    private static extern IntPtr GetWindow(IntPtr hwnd, uint cmd);

    // Token: 0x06000842 RID: 2114
    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hwnd);

    // Token: 0x06000843 RID: 2115
    [DllImport("user32.dll")]
    private static extern bool IsIconic(IntPtr hwnd);

    // Token: 0x06000844 RID: 2116
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetClassName(IntPtr hwnd, StringBuilder text, int max);

    // Token: 0x06000845 RID: 2117
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetWindowText(IntPtr hwnd, StringBuilder text, int max);

    // Token: 0x06000846 RID: 2118
    [DllImport("dwmapi.dll")]
    private static extern int DwmGetWindowAttribute(IntPtr hwnd, int attribute, out int value, int size);

    // Token: 0x06000847 RID: 2119 RVA: 0x0053D5F0 File Offset: 0x0053D5F0
    [NullableContext(0)]
    public static IntPtr ForeignWindowAbove(IntPtr hwnd, [TupleElementNames(new string[] { "X", "Y", "W", "H" })] ValueTuple<int, int, int, int> rectPx)
    {
      if (rectPx.Item3 <= 0 || rectPx.Item4 <= 0)
      {
        return (IntPtr)0;
      }
      uint processId = (uint)Environment.ProcessId;
      int num = 0;
      IntPtr intPtr = ZOrder.GetWindow(hwnd, 3U);
      while (intPtr != 0 && num++ < 2000)
      {
        if (ZOrder.IsWindowVisible(intPtr) && !ZOrder.IsIconic(intPtr))
        {
          uint num2;
          Native.GetWindowThreadProcessId(intPtr, out num2);
          if (num2 != processId || intPtr == FullscreenGuard.TestGameWindow)
          {
            uint num3 = (uint)Native.GetWindowLongPtr(intPtr, -20);
            int num4;
            if (((num3 & 32U) == 0U || (num3 & 524288U) == 0U) && (ZOrder.DwmGetWindowAttribute(intPtr, 14, out num4, 4) != 0 || num4 == 0))
            {
              string text = ZOrder.ClassName(intPtr);
              bool flag = text == "Shell_TrayWnd" || text == "Shell_SecondaryTrayWnd";
              Native.RECT rect;
              if (!flag && Native.GetWindowRectPublic(intPtr, out rect) && rect.Width > 0 && rect.Height > 0 && (rect.Left < rectPx.Item1 + rectPx.Item3 && rectPx.Item1 < rect.Right && rect.Top < rectPx.Item2 + rectPx.Item4 && rectPx.Item2 < rect.Bottom))
              {
                return intPtr;
              }
            }
          }
        }
        intPtr = ZOrder.GetWindow(intPtr, 3U);
      }
      return (IntPtr)0;
    }

    // Token: 0x06000848 RID: 2120 RVA: 0x0053D744 File Offset: 0x0053D744
    public static string Describe(IntPtr hwnd)
    {
      StringBuilder stringBuilder = new StringBuilder(96);
      ZOrder.GetWindowText(hwnd, stringBuilder, 96);
      uint num;
      Native.GetWindowThreadProcessId(hwnd, out num);
      string text;
      try
      {
        text = Process.GetProcessById((int)num).ProcessName;
      }
      catch
      {
        text = num.ToString();
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 3);
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral(" [");
      defaultInterpolatedStringHandler.AppendFormatted(ZOrder.ClassName(hwnd));
      defaultInterpolatedStringHandler.AppendLiteral("]");
      defaultInterpolatedStringHandler.AppendFormatted((stringBuilder.Length > 0) ? " (titled)" : "");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06000849 RID: 2121 RVA: 0x0053D7F0 File Offset: 0x0053D7F0
    private static string ClassName(IntPtr hwnd)
    {
      StringBuilder stringBuilder = new StringBuilder(96);
      ZOrder.GetClassName(hwnd, stringBuilder, 96);
      return stringBuilder.ToString();
    }

    // Token: 0x0600084A RID: 2122 RVA: 0x0053D818 File Offset: 0x0053D818
    public static void RaiseToTop(IntPtr hwnd)
    {
      Native.SetWindowPos(hwnd, FullscreenGuard.TopmostInsertAfter(), 0, 0, 0, 0, 531U);
    }

    // Token: 0x040002D6 RID: 726
    private const uint GW_HWNDPREV = 3U;

    // Token: 0x040002D7 RID: 727
    private const int DWMWA_CLOAKED = 14;
  }
}
