using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HeyClicky
{
  // Token: 0x02000036 RID: 54
  [NullableContext(1)]
  [Nullable(0)]
  public static class ShellOpen
  {
    // Token: 0x170000AD RID: 173
    // (get) Token: 0x060002A4 RID: 676 RVA: 0x000217E0 File Offset: 0x000217E0
    // (set) Token: 0x060002A5 RID: 677 RVA: 0x000217E8 File Offset: 0x000217E8
    [Nullable(2)]
    public static Func<bool> UserIsTypingInHeyClicky
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x060002A6 RID: 678 RVA: 0x000217F0 File Offset: 0x000217F0
    public static void Start(string target)
    {
      bool flag = ShellOpen.IsUserTyping();
      string handler = ShellOpen.HandlerExecutable(target);
      IReadOnlyList<ShellOpenRules.TopWindow> before = (flag ? new List<ShellOpenRules.TopWindow>() : ShellOpen.TopLevelWindows());
      if (!flag)
      {
        ShellOpen.AllowSetForegroundWindow(uint.MaxValue);
      }
      else if (ShellOpen.LockSetForegroundWindow(1U))
      {
        Task.Delay(ShellOpen.ForegroundLockAfterOpen).ContinueWith<bool>((Task _) => ShellOpen.LockSetForegroundWindow(2U), TaskScheduler.Default);
      }
      using (ShellOpen.StartProcess(target, handler))
      {
      }
      if (flag)
      {
        Log.Info("Open", "opened " + ShellOpen.Describe(target) + "; left behind: the user is typing in the composer");
        return;
      }
      Task.Run(() => ShellOpen.BringOpenedWindowForwardAsync(target, (handler == null) ? null : Path.GetFileName(handler), before));
    }

    // Token: 0x060002A7 RID: 679 RVA: 0x000218E8 File Offset: 0x000218E8
    public static bool TryStart(string target, string area)
    {
      bool flag;
      try
      {
        ShellOpen.Start(target);
        flag = true;
      }
      catch (Exception ex)
      {
        Log.Warn(area, "open " + ShellOpen.Describe(target) + " failed: " + ex.Message);
        flag = false;
      }
      return flag;
    }

    // Token: 0x060002A8 RID: 680 RVA: 0x00021938 File Offset: 0x00021938
    private static bool IsUserTyping()
    {
      bool flag;
      try
      {
        Func<bool> userIsTypingInHeyClicky = ShellOpen.UserIsTypingInHeyClicky;
        flag = userIsTypingInHeyClicky != null && userIsTypingInHeyClicky();
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    // Token: 0x060002A9 RID: 681 RVA: 0x00021970 File Offset: 0x00021970
    [NullableContext(2)]
    private static Process StartProcess([Nullable(1)] string target, string handler)
    {
      return Process.Start(new ProcessStartInfo(target)
      {
        UseShellExecute = true
      });
    }

    // Token: 0x060002AA RID: 682 RVA: 0x00021984 File Offset: 0x00021984
    private static async Task BringOpenedWindowForwardAsync(string target, [Nullable(2)] string handlerExeName, IReadOnlyList<ShellOpenRules.TopWindow> before)
    {
      try
      {
        int self = Environment.ProcessId;
        Stopwatch watch = Stopwatch.StartNew();
        ShellOpen.<>c__DisplayClass11_0 CS$<>8__locals1;
        ShellOpenRules.TopWindow? topWindow;
        for (;;)
        {
          CS$<>8__locals1 = new ShellOpen.<>c__DisplayClass11_0();
          await Task.Delay(ShellOpen.PollInterval).ConfigureAwait(false);
          if (ShellOpen.IsUserTyping())
          {
            break;
          }
          bool flag = watch.Elapsed >= ShellOpen.FindDeadline;
          List<ShellOpenRules.TopWindow> list = ShellOpen.TopLevelWindows();
          topWindow = ShellOpenRules.PickOpenedWindow(before, list, handlerExeName, self, flag);
          if (topWindow != null)
          {
            goto Block_3;
          }
          if (flag)
          {
            goto Block_7;
          }
          CS$<>8__locals1 = null;
        }
        Log.Info("Open", ShellOpen.Describe(target) + ": left behind, the user started typing in the composer");
        return;
        Block_3:
        CS$<>8__locals1.window = topWindow.GetValueOrDefault();
        ShellOpenRules.TopWindow topWindow2 = before.FirstOrDefault<ShellOpenRules.TopWindow>((ShellOpenRules.TopWindow w) => w.Handle == CS$<>8__locals1.window.Handle);
        string text = ((topWindow2.Handle == 0) ? "a new window" : ((topWindow2.Title != CS$<>8__locals1.window.Title) ? "its title changed" : "its frontmost window"));
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
        if (ShellOpen.GetForegroundWindow() == CS$<>8__locals1.window.Handle)
        {
          string text2 = "Open";
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 3);
          defaultInterpolatedStringHandler.AppendFormatted(ShellOpen.Describe(target));
          defaultInterpolatedStringHandler.AppendLiteral(": ");
          defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.window.ExeName);
          defaultInterpolatedStringHandler.AppendLiteral(" (");
          defaultInterpolatedStringHandler.AppendFormatted(text);
          defaultInterpolatedStringHandler.AppendLiteral(") is already in front");
          Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
          return;
        }
        string text3 = ShellOpen.BringToFront(CS$<>8__locals1.window.Handle);
        string text4 = "Open";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 5);
        defaultInterpolatedStringHandler.AppendFormatted(ShellOpen.Describe(target));
        defaultInterpolatedStringHandler.AppendLiteral(": ");
        defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.window.ExeName);
        defaultInterpolatedStringHandler.AppendLiteral(" (");
        defaultInterpolatedStringHandler.AppendFormatted(text);
        defaultInterpolatedStringHandler.AppendLiteral(", after ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(watch.Elapsed.TotalSeconds, "0.0");
        defaultInterpolatedStringHandler.AppendLiteral(" s) brought forward: ");
        defaultInterpolatedStringHandler.AppendFormatted(text3);
        Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
        return;
        Block_7:
        Log.Info("Open", ShellOpen.Describe(target) + ": no window of " + (handlerExeName ?? "its app") + " showed up to bring forward");
      }
      catch (Exception ex)
      {
        Log.Warn("Open", "bringing " + ShellOpen.Describe(target) + " forward failed: " + ex.Message);
      }
    }

    // Token: 0x060002AB RID: 683 RVA: 0x000219D8 File Offset: 0x000219D8
    private static string BringToFront(IntPtr window)
    {
      ShellOpen.MSG msg;
      ShellOpen.PeekMessageW(out msg, (IntPtr)0, 0U, 0U, 0U);
      if (ShellOpen.IsIconic(window))
      {
        ShellOpen.ShowWindow(window, 9);
      }
      if (ShellOpen.SetForegroundWindow(window) && ShellOpen.GetForegroundWindow() == window)
      {
        return "yes";
      }
      IntPtr foregroundWindow = ShellOpen.GetForegroundWindow();
      uint num2;
      uint num = ((foregroundWindow == 0) ? 0U : ShellOpen.GetWindowThreadProcessId(foregroundWindow, out num2));
      uint currentThreadId = ShellOpen.GetCurrentThreadId();
      if (num != 0U && num != currentThreadId && ShellOpen.AttachThreadInput(currentThreadId, num, true))
      {
        try
        {
          ShellOpen.BringWindowToTop(window);
          ShellOpen.SetForegroundWindow(window);
        }
        finally
        {
          ShellOpen.AttachThreadInput(currentThreadId, num, false);
        }
        if (ShellOpen.GetForegroundWindow() == window)
        {
          return "yes (attached to the foreground input)";
        }
      }
      ShellOpen.TapAlt();
      ShellOpen.TapAlt();
      ShellOpen.SetForegroundWindow(window);
      if (ShellOpen.GetForegroundWindow() != window)
      {
        return "no, Windows refused";
      }
      return "yes (Alt taps)";
    }

    // Token: 0x060002AC RID: 684 RVA: 0x00021AA4 File Offset: 0x00021AA4
    private static void TapAlt()
    {
      ShellOpen.INPUT[] array = new ShellOpen.INPUT[]
      {
        new ShellOpen.INPUT
        {
          type = 1U,
          ki = new ShellOpen.KEYBDINPUT
          {
            wVk = 18,
            dwExtraInfo = (IntPtr)1212369995
          }
        },
        new ShellOpen.INPUT
        {
          type = 1U,
          ki = new ShellOpen.KEYBDINPUT
          {
            wVk = 18,
            dwFlags = 2U,
            dwExtraInfo = (IntPtr)1212369995
          }
        }
      };
      ShellOpen.SendInput((uint)array.Length, array, Marshal.SizeOf<ShellOpen.INPUT>());
    }

    // Token: 0x060002AD RID: 685 RVA: 0x00021B4C File Offset: 0x00021B4C
    private static string Describe(string target)
    {
      if (!ShellOpenRules.LooksLikeUri(target))
      {
        string fileName = Path.GetFileName(target.TrimEnd(new char[] { '\\', '/' }));
        if (fileName == null || fileName.Length <= 0)
        {
          return "a folder";
        }
        return fileName;
      }
      else
      {
        Uri uri;
        if (!Uri.TryCreate(target, UriKind.Absolute, out uri))
        {
          return "a link";
        }
        return uri.Scheme + ":" + (string.IsNullOrEmpty(uri.Host) ? "" : ("//" + uri.Host));
      }
    }

    // Token: 0x060002AE RID: 686 RVA: 0x00021BD8 File Offset: 0x00021BD8
    [return: Nullable(2)]
    public static string HandlerExecutable(string target)
    {
      string text;
      try
      {
        bool flag = !ShellOpenRules.LooksLikeUri(target) && Directory.Exists(target);
        ValueTuple<string, bool> valueTuple = ShellOpenRules.AssociationKey(target, flag);
        string item = valueTuple.Item1;
        bool item2 = valueTuple.Item2;
        if (item == null)
        {
          text = null;
        }
        else if (item == "Folder")
        {
          text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "explorer.exe");
        }
        else
        {
          uint num = 1056U | (item2 ? 4096U : 0U);
          string[] array = new string[2];
          array[0] = "open";
          foreach (string text2 in array)
          {
            char[] array3 = new char[1024];
            uint num2 = (uint)array3.Length;
            if (ShellOpen.AssocQueryStringW(num, 2U, item, text2, array3, ref num2) == 0)
            {
              string text3 = new string(array3, 0, (int)Math.Min(num2, (uint)array3.Length)).TrimEnd('\0');
              if (text3.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) && File.Exists(text3))
              {
                return text3;
              }
            }
          }
          text = null;
        }
      }
      catch
      {
        text = null;
      }
      return text;
    }

    // Token: 0x060002AF RID: 687 RVA: 0x00021CF0 File Offset: 0x00021CF0
    private static List<ShellOpenRules.TopWindow> TopLevelWindows()
    {
      List<ShellOpenRules.TopWindow> windows = new List<ShellOpenRules.TopWindow>();
      Dictionary<uint, string> exeByProcess = new Dictionary<uint, string>();
      char[] title = new char[256];
      ShellOpen.EnumWindows(delegate(IntPtr hwnd, IntPtr _)
      {
        if (!ShellOpen.IsWindowVisible(hwnd) || ShellOpen.GetWindow(hwnd, 4U) != 0)
        {
          return true;
        }
        if (((long)ShellOpen.GetWindowLongPtrW(hwnd, -20) & 128L) != 0L)
        {
          return true;
        }
        int num;
        if (ShellOpen.DwmGetWindowAttribute(hwnd, 14, out num, 4) == 0 && num != 0)
        {
          return true;
        }
        uint num2;
        ShellOpen.GetWindowThreadProcessId(hwnd, out num2);
        string text;
        if (!exeByProcess.TryGetValue(num2, out text))
        {
          text = (exeByProcess[num2] = ShellOpen.ProcessExeName(num2));
        }
        int windowTextW = ShellOpen.GetWindowTextW(hwnd, title, title.Length);
        windows.Add(new ShellOpenRules.TopWindow(hwnd, (int)num2, text, new string(title, 0, Math.Max(0, windowTextW)), ShellOpen.IsIconic(hwnd)));
        return true;
      }, (IntPtr)0);
      return windows;
    }

    // Token: 0x060002B0 RID: 688 RVA: 0x00021D44 File Offset: 0x00021D44
    private static string ProcessExeName(uint pid)
    {
      IntPtr intPtr = ShellOpen.OpenProcess(4096U, false, pid);
      if (intPtr == 0)
      {
        return "";
      }
      string text;
      try
      {
        char[] array = new char[1024];
        int num = array.Length;
        text = (ShellOpen.QueryFullProcessImageNameW(intPtr, 0U, array, ref num) ? Path.GetFileName(new string(array, 0, num)) : "");
      }
      finally
      {
        ShellOpen.CloseHandle(intPtr);
      }
      return text;
    }

    // Token: 0x060002B1 RID: 689
    [DllImport("user32.dll")]
    private static extern bool AllowSetForegroundWindow(uint processId);

    // Token: 0x060002B2 RID: 690
    [DllImport("user32.dll")]
    private static extern bool LockSetForegroundWindow(uint code);

    // Token: 0x060002B3 RID: 691
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hwnd);

    // Token: 0x060002B4 RID: 692
    [DllImport("user32.dll")]
    private static extern bool PeekMessageW(out ShellOpen.MSG msg, IntPtr hwnd, uint min, uint max, uint remove);

    // Token: 0x060002B5 RID: 693
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    // Token: 0x060002B6 RID: 694
    [DllImport("user32.dll")]
    private static extern bool BringWindowToTop(IntPtr hwnd);

    // Token: 0x060002B7 RID: 695
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int command);

    // Token: 0x060002B8 RID: 696
    [DllImport("user32.dll")]
    private static extern bool IsIconic(IntPtr hwnd);

    // Token: 0x060002B9 RID: 697
    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hwnd);

    // Token: 0x060002BA RID: 698
    [DllImport("user32.dll")]
    private static extern IntPtr GetWindow(IntPtr hwnd, uint command);

    // Token: 0x060002BB RID: 699
    [DllImport("user32.dll")]
    private static extern IntPtr GetWindowLongPtrW(IntPtr hwnd, int index);

    // Token: 0x060002BC RID: 700
    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint processId);

    // Token: 0x060002BD RID: 701
    [DllImport("user32.dll")]
    private static extern bool AttachThreadInput(uint attach, uint attachTo, bool doAttach);

    // Token: 0x060002BE RID: 702
    [DllImport("user32.dll")]
    private static extern bool EnumWindows(ShellOpen.EnumWindowsProc callback, IntPtr lParam);

    // Token: 0x060002BF RID: 703
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextW(IntPtr hwnd, [Out] char[] text, int max);

    // Token: 0x060002C0 RID: 704
    [DllImport("user32.dll")]
    private static extern uint SendInput(uint count, ShellOpen.INPUT[] inputs, int size);

    // Token: 0x060002C1 RID: 705
    [DllImport("kernel32.dll")]
    private static extern uint GetCurrentThreadId();

    // Token: 0x060002C2 RID: 706
    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(uint access, bool inherit, uint processId);

    // Token: 0x060002C3 RID: 707
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    private static extern bool QueryFullProcessImageNameW(IntPtr process, uint flags, [Out] char[] name, ref int size);

    // Token: 0x060002C4 RID: 708
    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr handle);

    // Token: 0x060002C5 RID: 709
    [DllImport("dwmapi.dll")]
    private static extern int DwmGetWindowAttribute(IntPtr hwnd, int attribute, out int value, int size);

    // Token: 0x060002C6 RID: 710
    [NullableContext(2)]
    [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
    private static extern int AssocQueryStringW(uint flags, uint str, [Nullable(1)] string assoc, string extra, [Out] char[] output, ref uint length);

    // Token: 0x040000BA RID: 186
    private static readonly TimeSpan FindDeadline = TimeSpan.FromSeconds(6L);

    // Token: 0x040000BB RID: 187
    private static readonly TimeSpan PollInterval = TimeSpan.FromMilliseconds(100L, 0L);

    // Token: 0x040000BC RID: 188
    private static readonly TimeSpan ForegroundLockAfterOpen = TimeSpan.FromSeconds(4L);

    // Token: 0x040000BD RID: 189
    private const uint ASFW_ANY = 4294967295U;

    // Token: 0x040000BE RID: 190
    private const int SW_RESTORE = 9;

    // Token: 0x040000BF RID: 191
    private const uint GW_OWNER = 4U;

    // Token: 0x040000C0 RID: 192
    private const int GWL_EXSTYLE = -20;

    // Token: 0x040000C1 RID: 193
    private const long WS_EX_TOOLWINDOW = 128L;

    // Token: 0x040000C2 RID: 194
    private const int DWMWA_CLOAKED = 14;

    // Token: 0x040000C3 RID: 195
    private const uint PROCESS_QUERY_LIMITED_INFORMATION = 4096U;

    // Token: 0x040000C4 RID: 196
    private const uint ASSOCF_NOTRUNCATE = 32U;

    // Token: 0x040000C5 RID: 197
    private const uint ASSOCF_INIT_IGNOREUNKNOWN = 1024U;

    // Token: 0x040000C6 RID: 198
    private const uint ASSOCF_IS_PROTOCOL = 4096U;

    // Token: 0x040000C7 RID: 199
    private const uint ASSOCSTR_EXECUTABLE = 2U;

    // Token: 0x040000C8 RID: 200
    private const uint INPUT_KEYBOARD = 1U;

    // Token: 0x040000C9 RID: 201
    private const ushort VK_MENU = 18;

    // Token: 0x040000CA RID: 202
    private const uint KEYEVENTF_KEYUP = 2U;

    // Token: 0x040000CB RID: 203
    private const IntPtr HeyClickyInputSignature = 1212369995;

    // Token: 0x040000CC RID: 204
    private const uint PM_NOREMOVE = 0U;

    // Token: 0x040000CD RID: 205
    private const uint LSFW_LOCK = 1U;

    // Token: 0x040000CE RID: 206
    private const uint LSFW_UNLOCK = 2U;

    // Token: 0x0200005C RID: 92
    [NullableContext(0)]
    private struct KEYBDINPUT
    {
      // Token: 0x04000107 RID: 263
      public ushort wVk;

      // Token: 0x04000108 RID: 264
      public ushort wScan;

      // Token: 0x04000109 RID: 265
      public uint dwFlags;

      // Token: 0x0400010A RID: 266
      public uint time;

      // Token: 0x0400010B RID: 267
      public IntPtr dwExtraInfo;
    }

    // Token: 0x0200005D RID: 93
    [NullableContext(0)]
    private struct MOUSEINPUT
    {
      // Token: 0x0400010C RID: 268
      public int dx;

      // Token: 0x0400010D RID: 269
      public int dy;

      // Token: 0x0400010E RID: 270
      public uint mouseData;

      // Token: 0x0400010F RID: 271
      public uint dwFlags;

      // Token: 0x04000110 RID: 272
      public uint time;

      // Token: 0x04000111 RID: 273
      public IntPtr dwExtraInfo;
    }

    // Token: 0x0200005E RID: 94
    [NullableContext(0)]
    [StructLayout(LayoutKind.Explicit)]
    private struct INPUT
    {
      // Token: 0x04000112 RID: 274
      [FieldOffset(0)]
      public uint type;

      // Token: 0x04000113 RID: 275
      [FieldOffset(8)]
      public ShellOpen.KEYBDINPUT ki;

      // Token: 0x04000114 RID: 276
      [FieldOffset(8)]
      public ShellOpen.MOUSEINPUT mi;
    }

    // Token: 0x0200005F RID: 95
    [NullableContext(0)]
    private struct MSG
    {
      // Token: 0x04000115 RID: 277
      public IntPtr hwnd;

      // Token: 0x04000116 RID: 278
      public uint message;

      // Token: 0x04000117 RID: 279
      public IntPtr wParam;

      // Token: 0x04000118 RID: 280
      public IntPtr lParam;

      // Token: 0x04000119 RID: 281
      public uint time;

      // Token: 0x0400011A RID: 282
      public int x;

      // Token: 0x0400011B RID: 283
      public int y;
    }

    // Token: 0x02000060 RID: 96
    // (Invoke) Token: 0x060004DC RID: 1244
    [NullableContext(0)]
    private delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);
  }
}
