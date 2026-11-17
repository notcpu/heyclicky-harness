using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeyClicky
{
  // Token: 0x02000035 RID: 53
  public static class NonRudeWindow
  {
    // Token: 0x060002A2 RID: 674 RVA: 0x00021768 File Offset: 0x00021768
    public static void Mark(IntPtr hwnd)
    {
      if (hwnd == 0)
      {
        return;
      }
      if (!NonRudeWindow.SetPropW(hwnd, "NonRudeHWND", (IntPtr)1))
      {
        string text = "NonRudeWindow";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 2);
        defaultInterpolatedStringHandler.AppendLiteral("SetProp NonRudeHWND failed for 0x");
        defaultInterpolatedStringHandler.AppendFormatted<IntPtr>(hwnd, "X");
        defaultInterpolatedStringHandler.AppendLiteral(" (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(Marshal.GetLastWin32Error());
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
      }
    }

    // Token: 0x060002A3 RID: 675
    [NullableContext(1)]
    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool SetPropW(IntPtr hwnd, string name, IntPtr data);
  }
}
