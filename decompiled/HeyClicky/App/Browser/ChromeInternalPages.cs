using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HeyClicky.App.Browser
{
  // Token: 0x0200035C RID: 860
  [NullableContext(1)]
  [Nullable(0)]
  public static class ChromeInternalPages
  {
    // Token: 0x06002566 RID: 9574 RVA: 0x0063B1E8 File Offset: 0x0063B1E8
    private static bool IsInternalPage(string url)
    {
      return url.StartsWith("chrome://", StringComparison.OrdinalIgnoreCase) || url.StartsWith("edge://", StringComparison.OrdinalIgnoreCase) || url.StartsWith("brave://", StringComparison.OrdinalIgnoreCase);
    }

    // Token: 0x06002567 RID: 9575 RVA: 0x0063B214 File Offset: 0x0063B214
    public static Task<bool> Open(string browserExecutable, string url)
    {
      ChromeInternalPages.<>c__DisplayClass1_0 CS$<>8__locals1 = new ChromeInternalPages.<>c__DisplayClass1_0();
      CS$<>8__locals1.browserExecutable = browserExecutable;
      CS$<>8__locals1.url = url;
      if (!ChromeInternalPages.IsInternalPage(CS$<>8__locals1.url))
      {
        return Task.FromResult<bool>(ChromeInternalPages.Launch(CS$<>8__locals1.browserExecutable, CS$<>8__locals1.url, false));
      }
      return Task.Run<bool>(delegate
      {
        ChromeInternalPages.<>c__DisplayClass1_0.<<Open>b__0>d <<Open>b__0>d;
        <<Open>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
        <<Open>b__0>d.<>4__this = CS$<>8__locals1;
        <<Open>b__0>d.<>1__state = -1;
        <<Open>b__0>d.<>t__builder.Start<ChromeInternalPages.<>c__DisplayClass1_0.<<Open>b__0>d>(ref <<Open>b__0>d);
        return <<Open>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x06002568 RID: 9576 RVA: 0x0063B26C File Offset: 0x0063B26C
    private static bool Launch(string exe, string url, bool newWindow = false)
    {
      ProcessStartInfo processStartInfo = new ProcessStartInfo(exe)
      {
        UseShellExecute = false
      };
      if (newWindow)
      {
        processStartInfo.ArgumentList.Add("--new-window");
      }
      processStartInfo.ArgumentList.Add(url);
      bool flag;
      try
      {
        Process process = Process.Start(processStartInfo);
        if (process != null)
        {
          process.Dispose();
        }
        flag = true;
      }
      catch (Exception ex)
      {
        Log.Warn("Browser", "open " + url + " failed: " + ex.Message);
        flag = false;
      }
      return flag;
    }

    // Token: 0x06002569 RID: 9577 RVA: 0x0063B2F4 File Offset: 0x0063B2F4
    private static HashSet<IntPtr> BrowserWindows(string exe)
    {
      string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(exe);
      HashSet<uint> pids = Process.GetProcessesByName(fileNameWithoutExtension).Select<Process, uint>(delegate(Process p)
      {
        uint id;
        try
        {
          id = (uint)p.Id;
        }
        finally
        {
          if (p != null)
          {
            ((IDisposable)p).Dispose();
          }
        }
        return id;
      }).ToHashSet<uint>();
      HashSet<IntPtr> windows = new HashSet<IntPtr>();
      ChromeInternalPages.EnumWindows(delegate(IntPtr hwnd, IntPtr _)
      {
        uint num;
        if (ChromeInternalPages.IsWindowVisible(hwnd) && ChromeInternalPages.GetWindowThreadProcessId(hwnd, out num) != 0U && pids.Contains(num))
        {
          windows.Add(hwnd);
        }
        return true;
      }, (IntPtr)0);
      return windows;
    }

    // Token: 0x0600256A RID: 9578 RVA: 0x0063B368 File Offset: 0x0063B368
    private static void TypeText(string text)
    {
      List<ChromeInternalPages.INPUT> list = new List<ChromeInternalPages.INPUT>();
      foreach (char c in text)
      {
        list.Add(ChromeInternalPages.Key(0, c, 4U));
        list.Add(ChromeInternalPages.Key(0, c, 6U));
      }
      ChromeInternalPages.SendInput((uint)list.Count, list.ToArray(), Marshal.SizeOf<ChromeInternalPages.INPUT>());
    }

    // Token: 0x0600256B RID: 9579 RVA: 0x0063B3CC File Offset: 0x0063B3CC
    private static void PressEnter()
    {
      ChromeInternalPages.INPUT[] array = new ChromeInternalPages.INPUT[]
      {
        ChromeInternalPages.Key(13, '\0', 0U),
        ChromeInternalPages.Key(13, '\0', 2U)
      };
      ChromeInternalPages.SendInput((uint)array.Length, array, Marshal.SizeOf<ChromeInternalPages.INPUT>());
    }

    // Token: 0x0600256C RID: 9580 RVA: 0x0063B410 File Offset: 0x0063B410
    private static ChromeInternalPages.INPUT Key(ushort vk, char scan, uint flags)
    {
      return new ChromeInternalPages.INPUT
      {
        type = 1U,
        ki = new ChromeInternalPages.KEYBDINPUT
        {
          wVk = vk,
          wScan = (ushort)scan,
          dwFlags = flags
        }
      };
    }

    // Token: 0x0600256D RID: 9581
    [DllImport("user32.dll")]
    private static extern bool EnumWindows(ChromeInternalPages.EnumWindowsProc proc, IntPtr lParam);

    // Token: 0x0600256E RID: 9582
    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hwnd);

    // Token: 0x0600256F RID: 9583
    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint pid);

    // Token: 0x06002570 RID: 9584
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    // Token: 0x06002571 RID: 9585
    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint SendInput(uint count, ChromeInternalPages.INPUT[] inputs, int size);

    // Token: 0x040011C1 RID: 4545
    private const uint KEYEVENTF_KEYUP = 2U;

    // Token: 0x040011C2 RID: 4546
    private const uint KEYEVENTF_UNICODE = 4U;

    // Token: 0x02000A6C RID: 2668
    [NullableContext(0)]
    private struct KEYBDINPUT
    {
      // Token: 0x04002D33 RID: 11571
      public ushort wVk;

      // Token: 0x04002D34 RID: 11572
      public ushort wScan;

      // Token: 0x04002D35 RID: 11573
      public uint dwFlags;

      // Token: 0x04002D36 RID: 11574
      public uint time;

      // Token: 0x04002D37 RID: 11575
      public IntPtr dwExtraInfo;
    }

    // Token: 0x02000A6D RID: 2669
    [NullableContext(0)]
    [StructLayout(LayoutKind.Explicit, Size = 40)]
    private struct INPUT
    {
      // Token: 0x04002D38 RID: 11576
      [FieldOffset(0)]
      public uint type;

      // Token: 0x04002D39 RID: 11577
      [FieldOffset(8)]
      public ChromeInternalPages.KEYBDINPUT ki;
    }

    // Token: 0x02000A6E RID: 2670
    // (Invoke) Token: 0x0600411B RID: 16667
    [NullableContext(0)]
    private delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);
  }
}
