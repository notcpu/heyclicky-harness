using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Core.Platform
{
  // Token: 0x020000E6 RID: 230
  public sealed class WindowsDesktopActions : IDesktopActions
  {
    // Token: 0x06000ED4 RID: 3796 RVA: 0x0038C564 File Offset: 0x0038C564
    [NullableContext(1)]
    public bool OpenUrl(string url)
    {
      Uri uri;
      if (!Uri.TryCreate(url, UriKind.Absolute, out uri) || (uri.Scheme != Uri.UriSchemeHttps && uri.Scheme != Uri.UriSchemeHttp) || string.IsNullOrEmpty(uri.Host))
      {
        return false;
      }
      bool flag;
      try
      {
        ShellOpen.Start(uri.AbsoluteUri);
        flag = true;
      }
      catch (Exception ex)
      {
        Log.Warn("Desktop", "open url failed: " + ex.Message);
        flag = false;
      }
      return flag;
    }

    // Token: 0x06000ED5 RID: 3797 RVA: 0x0038C5F0 File Offset: 0x0038C5F0
    [NullableContext(1)]
    public bool CopyToClipboard(string text)
    {
      return WindowsDesktopActions.RunOnStaThread<bool>(() => WindowsDesktopActions.SetClipboardText(text));
    }

    // Token: 0x06000ED6 RID: 3798 RVA: 0x0038C610 File Offset: 0x0038C610
    [NullableContext(1)]
    public async Task<string> TypeIntoFocusedFieldAsync(string text)
    {
      WindowsDesktopActions.<>c__DisplayClass2_0 CS$<>8__locals1 = new WindowsDesktopActions.<>c__DisplayClass2_0();
      string text2;
      if (WindowsDesktopActions.GetForegroundWindow() == IntPtr.Zero)
      {
        text2 = (this.CopyToClipboard(text) ? "copied_to_clipboard" : "refused");
      }
      else
      {
        WindowsDesktopActions.<>c__DisplayClass2_0 CS$<>8__locals2 = CS$<>8__locals1;
        Func<string> func;
        if ((func = WindowsDesktopActions.<>O.<0>__GetClipboardText) == null)
        {
          func = (WindowsDesktopActions.<>O.<0>__GetClipboardText = new Func<string>(WindowsDesktopActions.GetClipboardText));
        }
        CS$<>8__locals2.previous = WindowsDesktopActions.RunOnStaThread<string>(func);
        if (!this.CopyToClipboard(text))
        {
          text2 = "refused";
        }
        else
        {
          WindowsDesktopActions.SendKeys(18, true);
          WindowsDesktopActions.SendKeys(17, false);
          WindowsDesktopActions.SendKeys(86, false);
          WindowsDesktopActions.SendKeys(86, true);
          WindowsDesktopActions.SendKeys(17, true);
          await Task.Delay(400).ConfigureAwait(false);
          if (CS$<>8__locals1.previous != null)
          {
            WindowsDesktopActions.RunOnStaThread<bool>(() => WindowsDesktopActions.SetClipboardText(CS$<>8__locals1.previous));
          }
          text2 = "typed";
        }
      }
      return text2;
    }

    // Token: 0x06000ED7 RID: 3799 RVA: 0x0038C65C File Offset: 0x0038C65C
    [return: TupleElementNames(new string[] { "DisplayName", "ProcessName" })]
    [return: Nullable(new byte[] { 0, 2, 2 })]
    public ValueTuple<string, string> FrontApplication()
    {
      ValueTuple<string, string> valueTuple;
      try
      {
        IntPtr foregroundWindow = WindowsDesktopActions.GetForegroundWindow();
        if (foregroundWindow == IntPtr.Zero)
        {
          valueTuple = new ValueTuple<string, string>(null, null);
        }
        else
        {
          uint num;
          WindowsDesktopActions.GetWindowThreadProcessId(foregroundWindow, out num);
          using (Process processById = Process.GetProcessById((int)num))
          {
            string text = null;
            try
            {
              ProcessModule mainModule = processById.MainModule;
              text = ((mainModule != null) ? mainModule.FileVersionInfo.FileDescription : null);
            }
            catch
            {
            }
            string processName = processById.ProcessName;
            valueTuple = new ValueTuple<string, string>(string.IsNullOrWhiteSpace(text) ? processName : text.Trim(), processName);
          }
        }
      }
      catch
      {
        valueTuple = new ValueTuple<string, string>(null, null);
      }
      return valueTuple;
    }

    // Token: 0x06000ED8 RID: 3800 RVA: 0x0038C718 File Offset: 0x0038C718
    [NullableContext(1)]
    private static T RunOnStaThread<[Nullable(2)] T>(Func<T> work)
    {
      if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
      {
        return work();
      }
      T result = default(T);
      Thread thread = new Thread(delegate
      {
        result = work();
      });
      thread.IsBackground = true;
      thread.SetApartmentState(ApartmentState.STA);
      thread.Start();
      thread.Join();
      return result;
    }

    // Token: 0x06000ED9 RID: 3801 RVA: 0x0038C788 File Offset: 0x0038C788
    [NullableContext(1)]
    private unsafe static bool SetClipboardText(string text)
    {
      int num = 0;
      while (num < 10 && !WindowsDesktopActions.OpenClipboard(IntPtr.Zero))
      {
        if (num == 9)
        {
          return false;
        }
        Thread.Sleep(20);
        num++;
      }
      bool flag;
      try
      {
        WindowsDesktopActions.EmptyClipboard();
        int num2 = (text.Length + 1) * 2;
        IntPtr intPtr = WindowsDesktopActions.GlobalAlloc(2U, (UIntPtr)((IntPtr)num2));
        if (intPtr == IntPtr.Zero)
        {
          flag = false;
        }
        else
        {
          IntPtr intPtr2 = WindowsDesktopActions.GlobalLock(intPtr);
          if (intPtr2 == IntPtr.Zero)
          {
            WindowsDesktopActions.GlobalFree(intPtr);
            flag = false;
          }
          else
          {
            Span<char> span = new Span<char>(intPtr2, text.Length + 1);
            text.AsSpan().CopyTo(span);
            *span[span.Length - 1] = '\0';
            WindowsDesktopActions.GlobalUnlock(intPtr);
            if (WindowsDesktopActions.SetClipboardData(13U, intPtr) == IntPtr.Zero)
            {
              WindowsDesktopActions.GlobalFree(intPtr);
              flag = false;
            }
            else
            {
              flag = true;
            }
          }
        }
      }
      finally
      {
        WindowsDesktopActions.CloseClipboard();
      }
      return flag;
    }

    // Token: 0x06000EDA RID: 3802 RVA: 0x0038C874 File Offset: 0x0038C874
    [NullableContext(2)]
    private static string GetClipboardText()
    {
      if (!WindowsDesktopActions.IsClipboardFormatAvailable(13U) || !WindowsDesktopActions.OpenClipboard(IntPtr.Zero))
      {
        return null;
      }
      string text;
      try
      {
        IntPtr clipboardData = WindowsDesktopActions.GetClipboardData(13U);
        if (clipboardData == IntPtr.Zero)
        {
          text = null;
        }
        else
        {
          IntPtr intPtr = WindowsDesktopActions.GlobalLock(clipboardData);
          if (intPtr == IntPtr.Zero)
          {
            text = null;
          }
          else
          {
            try
            {
              text = Marshal.PtrToStringUni(intPtr);
            }
            finally
            {
              WindowsDesktopActions.GlobalUnlock(clipboardData);
            }
          }
        }
      }
      finally
      {
        WindowsDesktopActions.CloseClipboard();
      }
      return text;
    }

    // Token: 0x06000EDB RID: 3803 RVA: 0x0038C8F8 File Offset: 0x0038C8F8
    private static void SendKeys(ushort virtualKey, bool keyUp)
    {
      WindowsDesktopActions.Input input = new WindowsDesktopActions.Input
      {
        Type = 1U,
        Keyboard = new WindowsDesktopActions.KeyboardInput
        {
          VirtualKey = virtualKey,
          Flags = (keyUp ? 2U : 0U)
        }
      };
      WindowsDesktopActions.SendInput(1U, ref input, Marshal.SizeOf<WindowsDesktopActions.Input>());
    }

    // Token: 0x06000EDC RID: 3804
    [LibraryImport("user32.dll")]
    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern IntPtr GetForegroundWindow();

    // Token: 0x06000EDD RID: 3805 RVA: 0x0038C94C File Offset: 0x0038C94C
    [LibraryImport("user32.dll")]
    [GeneratedCode("Microsoft.Interop.LibraryImportGenerator", "9.0.15.2218")]
    [SkipLocalsInit]
    private unsafe static uint GetWindowThreadProcessId(IntPtr window, out uint processId)
    {
      processId = 0U;
      uint num;
      fixed (uint* ptr = &processId)
      {
        uint* ptr2 = ptr;
        num = WindowsDesktopActions.<GetWindowThreadProcessId>g____PInvoke|19_0(window, ptr2);
      }
      return num;
    }

    // Token: 0x06000EDE RID: 3806 RVA: 0x0038C96C File Offset: 0x0038C96C
    [LibraryImport("user32.dll", SetLastError = true)]
    [GeneratedCode("Microsoft.Interop.LibraryImportGenerator", "9.0.15.2218")]
    [SkipLocalsInit]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static bool OpenClipboard(IntPtr owner)
    {
      Marshal.SetLastSystemError(0);
      int num = WindowsDesktopActions.<OpenClipboard>g____PInvoke|20_0(owner);
      int lastSystemError = Marshal.GetLastSystemError();
      bool flag = num != 0;
      Marshal.SetLastPInvokeError(lastSystemError);
      return flag;
    }

    // Token: 0x06000EDF RID: 3807 RVA: 0x0038C994 File Offset: 0x0038C994
    [LibraryImport("user32.dll")]
    [GeneratedCode("Microsoft.Interop.LibraryImportGenerator", "9.0.15.2218")]
    [SkipLocalsInit]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static bool CloseClipboard()
    {
      return WindowsDesktopActions.<CloseClipboard>g____PInvoke|21_0() != 0;
    }

    // Token: 0x06000EE0 RID: 3808 RVA: 0x0038C9A0 File Offset: 0x0038C9A0
    [LibraryImport("user32.dll")]
    [GeneratedCode("Microsoft.Interop.LibraryImportGenerator", "9.0.15.2218")]
    [SkipLocalsInit]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static bool EmptyClipboard()
    {
      return WindowsDesktopActions.<EmptyClipboard>g____PInvoke|22_0() != 0;
    }

    // Token: 0x06000EE1 RID: 3809
    [LibraryImport("user32.dll")]
    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern IntPtr SetClipboardData(uint format, IntPtr memory);

    // Token: 0x06000EE2 RID: 3810
    [LibraryImport("user32.dll")]
    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern IntPtr GetClipboardData(uint format);

    // Token: 0x06000EE3 RID: 3811 RVA: 0x0038C9AC File Offset: 0x0038C9AC
    [LibraryImport("user32.dll")]
    [GeneratedCode("Microsoft.Interop.LibraryImportGenerator", "9.0.15.2218")]
    [SkipLocalsInit]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static bool IsClipboardFormatAvailable(uint format)
    {
      return WindowsDesktopActions.<IsClipboardFormatAvailable>g____PInvoke|25_0(format) != 0;
    }

    // Token: 0x06000EE4 RID: 3812 RVA: 0x0038C9B8 File Offset: 0x0038C9B8
    [LibraryImport("user32.dll")]
    [GeneratedCode("Microsoft.Interop.LibraryImportGenerator", "9.0.15.2218")]
    [SkipLocalsInit]
    private unsafe static uint SendInput(uint count, ref WindowsDesktopActions.Input input, int size)
    {
      uint num;
      fixed (WindowsDesktopActions.Input* ptr = &input)
      {
        WindowsDesktopActions.Input* ptr2 = ptr;
        num = WindowsDesktopActions.<SendInput>g____PInvoke|26_0(count, ptr2, size);
      }
      return num;
    }

    // Token: 0x06000EE5 RID: 3813
    [LibraryImport("kernel32.dll")]
    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GlobalAlloc(uint flags, UIntPtr bytes);

    // Token: 0x06000EE6 RID: 3814
    [LibraryImport("kernel32.dll")]
    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GlobalLock(IntPtr memory);

    // Token: 0x06000EE7 RID: 3815 RVA: 0x0038C9D8 File Offset: 0x0038C9D8
    [LibraryImport("kernel32.dll")]
    [GeneratedCode("Microsoft.Interop.LibraryImportGenerator", "9.0.15.2218")]
    [SkipLocalsInit]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static bool GlobalUnlock(IntPtr memory)
    {
      return WindowsDesktopActions.<GlobalUnlock>g____PInvoke|29_0(memory) != 0;
    }

    // Token: 0x06000EE8 RID: 3816
    [LibraryImport("kernel32.dll")]
    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GlobalFree(IntPtr memory);

    // Token: 0x06000EEA RID: 3818
    [CompilerGenerated]
    [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", ExactSpelling = true)]
    internal unsafe static extern uint <GetWindowThreadProcessId>g____PInvoke|19_0(IntPtr __window_native, uint* __processId_native);

    // Token: 0x06000EEB RID: 3819
    [CompilerGenerated]
    [DllImport("user32.dll", EntryPoint = "OpenClipboard", ExactSpelling = true)]
    internal static extern int <OpenClipboard>g____PInvoke|20_0(IntPtr __owner_native);

    // Token: 0x06000EEC RID: 3820
    [CompilerGenerated]
    [DllImport("user32.dll", EntryPoint = "CloseClipboard", ExactSpelling = true)]
    internal static extern int <CloseClipboard>g____PInvoke|21_0();

    // Token: 0x06000EED RID: 3821
    [CompilerGenerated]
    [DllImport("user32.dll", EntryPoint = "EmptyClipboard", ExactSpelling = true)]
    internal static extern int <EmptyClipboard>g____PInvoke|22_0();

    // Token: 0x06000EEE RID: 3822
    [CompilerGenerated]
    [DllImport("user32.dll", EntryPoint = "IsClipboardFormatAvailable", ExactSpelling = true)]
    internal static extern int <IsClipboardFormatAvailable>g____PInvoke|25_0(uint __format_native);

    // Token: 0x06000EEF RID: 3823
    [CompilerGenerated]
    [DllImport("user32.dll", EntryPoint = "SendInput", ExactSpelling = true)]
    internal unsafe static extern uint <SendInput>g____PInvoke|26_0(uint __count_native, WindowsDesktopActions.Input* __input_native, int __size_native);

    // Token: 0x06000EF0 RID: 3824
    [CompilerGenerated]
    [DllImport("kernel32.dll", EntryPoint = "GlobalUnlock", ExactSpelling = true)]
    internal static extern int <GlobalUnlock>g____PInvoke|29_0(IntPtr __memory_native);

    // Token: 0x0400065A RID: 1626
    private const uint CfUnicodeText = 13U;

    // Token: 0x0400065B RID: 1627
    private const uint GmemMoveable = 2U;

    // Token: 0x0400065C RID: 1628
    private const uint InputKeyboard = 1U;

    // Token: 0x0400065D RID: 1629
    private const uint KeyEventKeyUp = 2U;

    // Token: 0x0400065E RID: 1630
    private const ushort VirtualKeyControl = 17;

    // Token: 0x0400065F RID: 1631
    private const ushort VirtualKeyMenu = 18;

    // Token: 0x04000660 RID: 1632
    private const ushort VirtualKeyV = 86;

    // Token: 0x020003FC RID: 1020
    private struct KeyboardInput
    {
      // Token: 0x04001B39 RID: 6969
      public ushort VirtualKey;

      // Token: 0x04001B3A RID: 6970
      public ushort ScanCode;

      // Token: 0x04001B3B RID: 6971
      public uint Flags;

      // Token: 0x04001B3C RID: 6972
      public uint Time;

      // Token: 0x04001B3D RID: 6973
      public IntPtr ExtraInfo;
    }

    // Token: 0x020003FD RID: 1021
    [StructLayout(LayoutKind.Explicit)]
    private struct Input
    {
      // Token: 0x04001B3E RID: 6974
      [FieldOffset(0)]
      public uint Type;

      // Token: 0x04001B3F RID: 6975
      [FieldOffset(8)]
      public WindowsDesktopActions.KeyboardInput Keyboard;

      // Token: 0x04001B40 RID: 6976
      [FieldOffset(8)]
      public WindowsDesktopActions.MousePadding Padding;
    }

    // Token: 0x020003FE RID: 1022
    private struct MousePadding
    {
      // Token: 0x04001B41 RID: 6977
      public int Dx;

      // Token: 0x04001B42 RID: 6978
      public int Dy;

      // Token: 0x04001B43 RID: 6979
      public uint MouseData;

      // Token: 0x04001B44 RID: 6980
      public uint Flags;

      // Token: 0x04001B45 RID: 6981
      public uint Time;

      // Token: 0x04001B46 RID: 6982
      public IntPtr ExtraInfo;
    }

    // Token: 0x020003FF RID: 1023
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001B47 RID: 6983
      public static Func<string> <0>__GetClipboardText;
    }
  }
}
