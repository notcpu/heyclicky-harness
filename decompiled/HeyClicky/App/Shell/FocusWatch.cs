using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace HeyClicky.App.Shell
{
  // Token: 0x020000BD RID: 189
  [NullableContext(1)]
  [Nullable(0)]
  internal static class FocusWatch
  {
    // Token: 0x14000011 RID: 17
    // (add) Token: 0x060007A6 RID: 1958 RVA: 0x00539AF4 File Offset: 0x00539AF4
    // (remove) Token: 0x060007A7 RID: 1959 RVA: 0x00539B28 File Offset: 0x00539B28
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public static event Action<bool, bool> ForegroundChanged;

    // Token: 0x060007A8 RID: 1960 RVA: 0x00539B5C File Offset: 0x00539B5C
    public static void Start()
    {
      if (FocusWatch.callback != null)
      {
        return;
      }
      FocusWatch.WinEventProc winEventProc;
      if ((winEventProc = FocusWatch.<>O.<0>__OnEvent) == null)
      {
        winEventProc = (FocusWatch.<>O.<0>__OnEvent = new FocusWatch.WinEventProc(FocusWatch.OnEvent));
      }
      FocusWatch.callback = winEventProc;
      FocusWatch.foregroundHook = FocusWatch.SetWinEventHook(3U, 3U, (IntPtr)0, FocusWatch.callback, 0U, 0U, 0U);
      FocusWatch.minimizeHook = FocusWatch.SetWinEventHook(22U, 22U, (IntPtr)0, FocusWatch.callback, 0U, 0U, 0U);
      FocusWatch.lastForeground = FocusWatch.GetForegroundWindow();
      FocusWatch.lastGame = FullscreenGuard.GameWindow();
      if (FocusWatch.foregroundHook == 0 || FocusWatch.minimizeHook == 0)
      {
        Log.Warn("focus", "the foreground watch didn't start");
      }
    }

    // Token: 0x060007A9 RID: 1961 RVA: 0x00539BF0 File Offset: 0x00539BF0
    private static void OnEvent(IntPtr hook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint thread, uint time)
    {
      if (idObject != 0 || hwnd == 0)
      {
        return;
      }
      try
      {
        if (eventType == 22U)
        {
          if (hwnd == FocusWatch.lastGame)
          {
            string text = "focus";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 2);
            defaultInterpolatedStringHandler.AppendLiteral("the fullscreen game ");
            defaultInterpolatedStringHandler.AppendFormatted(FullscreenGuard.Describe(hwnd));
            defaultInterpolatedStringHandler.AppendLiteral(" is being minimized (");
            defaultInterpolatedStringHandler.AppendFormatted(FullscreenGuard.NotificationState());
            defaultInterpolatedStringHandler.AppendLiteral(")");
            Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
          }
        }
        else
        {
          IntPtr intPtr = FocusWatch.lastForeground;
          FocusWatch.lastForeground = hwnd;
          uint num;
          FocusWatch.GetWindowThreadProcessId(hwnd, out num);
          if (intPtr != hwnd)
          {
            uint num2;
            bool flag = intPtr != 0 && FocusWatch.GetWindowThreadProcessId(intPtr, out num2) != 0U && (ulong)num2 == (ulong)((long)Environment.ProcessId);
            Action<bool, bool> foregroundChanged = FocusWatch.ForegroundChanged;
            if (foregroundChanged != null)
            {
              foregroundChanged(flag, (ulong)num == (ulong)((long)Environment.ProcessId));
            }
          }
          bool flag2 = intPtr != 0 && intPtr == FocusWatch.lastGame;
          if ((ulong)num == (ulong)((long)Environment.ProcessId))
          {
            Log.Warn("focus", string.Concat(new string[]
            {
              "HeyClicky's \"",
              FocusWatch.Title(hwnd),
              "\" took the foreground from ",
              (intPtr == 0) ? "nothing" : FullscreenGuard.Describe(intPtr),
              flag2 ? " (a fullscreen game: it may leave fullscreen)" : ""
            }));
          }
          else if (flag2)
          {
            Log.Info("focus", "the fullscreen game " + FullscreenGuard.Describe(intPtr) + " lost the foreground to " + FullscreenGuard.Describe(hwnd));
          }
          IntPtr intPtr2 = FullscreenGuard.GameWindow();
          if (intPtr2 != 0)
          {
            FocusWatch.lastGame = intPtr2;
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("focus", "foreground watch: " + ex.Message);
      }
    }

    // Token: 0x060007AA RID: 1962 RVA: 0x00539DA4 File Offset: 0x00539DA4
    public static void NoteActivated(string window)
    {
      IEnumerable<string> enumerable = from m in (from f in new StackTrace(2, false).GetFrames()
          select f.GetMethod()).Where<MethodBase>(delegate(MethodBase m)
        {
          if (m == null)
          {
            return false;
          }
          Type declaringType = m.DeclaringType;
          bool? flag;
          if (declaringType == null)
          {
            flag = null;
          }
          else
          {
            string @namespace = declaringType.Namespace;
            flag = ((@namespace != null) ? new bool?(@namespace.StartsWith("HeyClicky", StringComparison.Ordinal)) : null);
          }
          bool? flag2 = flag;
          return flag2.GetValueOrDefault();
        }).Take<MethodBase>(6)
        select m.DeclaringType.Name + "." + m.Name;
      Log.Info("focus", "\"" + window + "\" activated, from " + string.Join(" ← ", enumerable.DefaultIfEmpty("outside HeyClicky's code (a click or the system)")));
    }

    // Token: 0x060007AB RID: 1963 RVA: 0x00539E60 File Offset: 0x00539E60
    private static string Title(IntPtr hwnd)
    {
      StringBuilder stringBuilder = new StringBuilder(96);
      FocusWatch.GetWindowText(hwnd, stringBuilder, stringBuilder.Capacity);
      return stringBuilder.ToString();
    }

    // Token: 0x060007AC RID: 1964
    [DllImport("user32.dll")]
    private static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr module, FocusWatch.WinEventProc callback, uint process, uint thread, uint flags);

    // Token: 0x060007AD RID: 1965
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    // Token: 0x060007AE RID: 1966
    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint pid);

    // Token: 0x060007AF RID: 1967
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetWindowText(IntPtr hwnd, StringBuilder text, int max);

    // Token: 0x0400024E RID: 590
    private const uint EVENT_SYSTEM_FOREGROUND = 3U;

    // Token: 0x0400024F RID: 591
    private const uint EVENT_SYSTEM_MINIMIZESTART = 22U;

    // Token: 0x04000250 RID: 592
    private const uint WINEVENT_OUTOFCONTEXT = 0U;

    // Token: 0x04000251 RID: 593
    private const int OBJID_WINDOW = 0;

    // Token: 0x04000252 RID: 594
    [Nullable(2)]
    private static FocusWatch.WinEventProc callback;

    // Token: 0x04000253 RID: 595
    private static IntPtr foregroundHook;

    // Token: 0x04000254 RID: 596
    private static IntPtr minimizeHook;

    // Token: 0x04000255 RID: 597
    private static IntPtr lastForeground;

    // Token: 0x04000256 RID: 598
    private static IntPtr lastGame;

    // Token: 0x0200046C RID: 1132
    // (Invoke) Token: 0x06002A74 RID: 10868
    [NullableContext(0)]
    private delegate void WinEventProc(IntPtr hook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint thread, uint time);

    // Token: 0x0200046D RID: 1133
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x0400160E RID: 5646
      [Nullable(0)]
      public static FocusWatch.WinEventProc <0>__OnEvent;
    }
  }
}
