using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeyClicky.App.Shell
{
  // Token: 0x020000C1 RID: 193
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class TaskbarButton : IDisposable
  {
    // Token: 0x14000014 RID: 20
    // (add) Token: 0x060007F6 RID: 2038 RVA: 0x0053C194 File Offset: 0x0053C194
    // (remove) Token: 0x060007F7 RID: 2039 RVA: 0x0053C1CC File Offset: 0x0053C1CC
    public event Action Clicked;

    // Token: 0x14000015 RID: 21
    // (add) Token: 0x060007F8 RID: 2040 RVA: 0x0053C204 File Offset: 0x0053C204
    // (remove) Token: 0x060007F9 RID: 2041 RVA: 0x0053C23C File Offset: 0x0053C23C
    public event Action CloseRequested;

    // Token: 0x060007FA RID: 2042 RVA: 0x0053C274 File Offset: 0x0053C274
    [NullableContext(1)]
    public TaskbarButton(string title, string iconPath)
    {
      IntPtr moduleHandle = TaskbarButton.GetModuleHandle(null);
      if (!TaskbarButton.classRegistered)
      {
        TaskbarButton.WndProc wndProc;
        if ((wndProc = TaskbarButton.<>O.<0>__Proc) == null)
        {
          wndProc = (TaskbarButton.<>O.<0>__Proc = new TaskbarButton.WndProc(TaskbarButton.Proc));
        }
        TaskbarButton.wndProc = wndProc;
        TaskbarButton.WNDCLASSEX wndclassex = new TaskbarButton.WNDCLASSEX
        {
          cbSize = Marshal.SizeOf<TaskbarButton.WNDCLASSEX>(),
          lpfnWndProc = Marshal.GetFunctionPointerForDelegate<TaskbarButton.WndProc>(TaskbarButton.wndProc),
          hInstance = moduleHandle,
          lpszClassName = "HeyClickyTaskbarButton"
        };
        if (TaskbarButton.RegisterClassEx(ref wndclassex) == 0)
        {
          string text = "taskbar";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 1);
          defaultInterpolatedStringHandler.AppendLiteral("register class failed (");
          defaultInterpolatedStringHandler.AppendFormatted<int>(Marshal.GetLastWin32Error());
          defaultInterpolatedStringHandler.AppendLiteral(")");
          Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
          return;
        }
        TaskbarButton.classRegistered = true;
      }
      this.taskbarList = TaskbarButton.CreateTaskbarList();
      this.hwnd = TaskbarButton.CreateWindowEx((this.taskbarList != null) ? 128 : 262144, "HeyClickyTaskbarButton", title, 13565952, -32000, -32000, 1, 1, (IntPtr)0, (IntPtr)0, moduleHandle, (IntPtr)0);
      if (this.hwnd == 0)
      {
        string text2 = "taskbar";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 1);
        defaultInterpolatedStringHandler.AppendLiteral("create window failed (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(Marshal.GetLastWin32Error());
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Warn(text2, defaultInterpolatedStringHandler.ToStringAndClear());
        return;
      }
      TaskbarButton.instances[this.hwnd] = this;
      foreach (ValueTuple<int, uint> valueTuple in new ValueTuple<int, uint>[]
      {
        new ValueTuple<int, uint>(0, 1U),
        new ValueTuple<int, uint>(16, 0U)
      })
      {
        int item = valueTuple.Item1;
        uint item2 = valueTuple.Item2;
        IntPtr intPtr = TaskbarButton.LoadImage((IntPtr)0, iconPath, 1U, item, item, 16U | ((item == 0) ? 64U : 0U));
        if (intPtr != 0)
        {
          TaskbarButton.SendMessage(this.hwnd, 128U, (IntPtr)((UIntPtr)item2), intPtr);
        }
      }
    }

    // Token: 0x060007FB RID: 2043 RVA: 0x0053C468 File Offset: 0x0053C468
    public void SetShown(bool show)
    {
      if (this.hwnd == 0 || show == this.shown)
      {
        return;
      }
      this.shown = show;
      if (!show)
      {
        this.DeleteTab();
      }
      if (show)
      {
        this.ShowMinimizedOffDesktop();
      }
      else
      {
        TaskbarButton.ShowWindow(this.hwnd, 0);
      }
      if (show)
      {
        this.AddTab();
      }
      Log.Info("taskbar", show ? "button shown" : "button hidden");
    }

    // Token: 0x060007FC RID: 2044 RVA: 0x0053C4D4 File Offset: 0x0053C4D4
    private void ShowMinimizedOffDesktop()
    {
      TaskbarButton.WINDOWPLACEMENT windowplacement = new TaskbarButton.WINDOWPLACEMENT
      {
        length = (uint)Marshal.SizeOf<TaskbarButton.WINDOWPLACEMENT>(),
        flags = 1U,
        showCmd = 7U,
        ptMinPosition = new TaskbarButton.POINT
        {
          X = -32000,
          Y = -32000
        },
        ptMaxPosition = new TaskbarButton.POINT
        {
          X = -1,
          Y = -1
        },
        rcNormalPosition = new TaskbarButton.RECT
        {
          Left = -32000,
          Top = -32000,
          Right = -31999,
          Bottom = -31999
        }
      };
      if (!TaskbarButton.SetWindowPlacement(this.hwnd, ref windowplacement))
      {
        string text = "taskbar";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 1);
        defaultInterpolatedStringHandler.AppendLiteral("SetWindowPlacement failed (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(Marshal.GetLastWin32Error());
        defaultInterpolatedStringHandler.AppendLiteral("): minimizing in place");
        Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
        TaskbarButton.ShowWindow(this.hwnd, 7);
      }
    }

    // Token: 0x060007FD RID: 2045 RVA: 0x0053C5EC File Offset: 0x0053C5EC
    private static TaskbarButton.ITaskbarList CreateTaskbarList()
    {
      try
      {
        TaskbarButton.ITaskbarList taskbarList = (TaskbarButton.ITaskbarList)new TaskbarButton.TaskbarListClass();
        if (taskbarList.HrInit() >= 0)
        {
          return taskbarList;
        }
        Log.Warn("taskbar", "the taskbar list didn't start: the button is an app window (in Alt+Tab)");
      }
      catch (Exception ex)
      {
        Log.Warn("taskbar", "no taskbar list (" + ex.Message + "): the button is an app window (in Alt+Tab)");
      }
      return null;
    }

    // Token: 0x060007FE RID: 2046 RVA: 0x0053C658 File Offset: 0x0053C658
    private void AddTab()
    {
      if (this.taskbarList == null || this.hwnd == 0)
      {
        return;
      }
      try
      {
        int num = this.taskbarList.AddTab(this.hwnd);
        if (num < 0)
        {
          string text = "taskbar";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 1);
          defaultInterpolatedStringHandler.AppendLiteral("AddTab failed (0x");
          defaultInterpolatedStringHandler.AppendFormatted<int>(num, "X8");
          defaultInterpolatedStringHandler.AppendLiteral(")");
          Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
        }
      }
      catch (Exception ex)
      {
        Log.Warn("taskbar", "AddTab failed: " + ex.Message);
      }
    }

    // Token: 0x060007FF RID: 2047 RVA: 0x0053C6FC File Offset: 0x0053C6FC
    private void DeleteTab()
    {
      if (this.taskbarList == null || this.hwnd == 0)
      {
        return;
      }
      try
      {
        this.taskbarList.DeleteTab(this.hwnd);
      }
      catch (Exception ex)
      {
        Log.Warn("taskbar", "DeleteTab failed: " + ex.Message);
      }
    }

    // Token: 0x06000800 RID: 2048 RVA: 0x0053C75C File Offset: 0x0053C75C
    private void RaiseClicked()
    {
      long tickCount = Environment.TickCount64;
      if (tickCount - this.lastClick < 400L)
      {
        return;
      }
      this.lastClick = tickCount;
      Action clicked = this.Clicked;
      if (clicked == null)
      {
        return;
      }
      clicked();
    }

    // Token: 0x06000801 RID: 2049 RVA: 0x0053C798 File Offset: 0x0053C798
    private static IntPtr Proc(IntPtr h, uint msg, IntPtr w, IntPtr l)
    {
      TaskbarButton taskbarButton;
      if (TaskbarButton.instances.TryGetValue(h, out taskbarButton))
      {
        if (msg == 70U && l != 0 && TaskbarButton.IsIconic(h))
        {
          TaskbarButton.WINDOWPOS windowpos = Marshal.PtrToStructure<TaskbarButton.WINDOWPOS>(l);
          if ((windowpos.flags & 2U) == 0U && (windowpos.x != -32000 || windowpos.y != -32000))
          {
            windowpos.x = -32000;
            windowpos.y = -32000;
            Marshal.StructureToPtr<TaskbarButton.WINDOWPOS>(windowpos, l, false);
          }
        }
        if (msg == TaskbarButton.TaskbarCreatedMessage && TaskbarButton.TaskbarCreatedMessage != 0U)
        {
          if (taskbarButton.shown)
          {
            taskbarButton.AddTab();
          }
          return (IntPtr)0;
        }
        if (msg <= 16U)
        {
          if (msg != 6U)
          {
            if (msg != 16U)
            {
              goto IL_0113;
            }
          }
          else
          {
            if ((w & (IntPtr)65535) != 0)
            {
              return (IntPtr)0;
            }
            goto IL_0113;
          }
        }
        else
        {
          if (msg == 19U)
          {
            taskbarButton.RaiseClicked();
            return (IntPtr)0;
          }
          if (msg != 274U)
          {
            goto IL_0113;
          }
          int num = (int)w & 65520;
          bool flag = num == 61488 || num == 61728;
          if (flag)
          {
            taskbarButton.RaiseClicked();
            return (IntPtr)0;
          }
          if (((int)w & 65520) != 61536)
          {
            goto IL_0113;
          }
        }
        Action closeRequested = taskbarButton.CloseRequested;
        if (closeRequested != null)
        {
          closeRequested();
        }
        return (IntPtr)0;
      }
      IL_0113:
      return TaskbarButton.DefWindowProc(h, msg, w, l);
    }

    // Token: 0x06000802 RID: 2050 RVA: 0x0053C8C4 File Offset: 0x0053C8C4
    public void Dispose()
    {
      if (this.hwnd == 0)
      {
        return;
      }
      this.DeleteTab();
      TaskbarButton.instances.Remove(this.hwnd);
      TaskbarButton.DestroyWindow(this.hwnd);
      this.hwnd = (IntPtr)0;
    }

    // Token: 0x06000803 RID: 2051
    [NullableContext(1)]
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern uint RegisterWindowMessage(string name);

    // Token: 0x06000804 RID: 2052
    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern ushort RegisterClassEx(ref TaskbarButton.WNDCLASSEX wc);

    // Token: 0x06000805 RID: 2053
    [NullableContext(1)]
    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr CreateWindowEx(int exStyle, string className, string title, int style, int x, int y, int w, int h, IntPtr parent, IntPtr menu, IntPtr instance, IntPtr param);

    // Token: 0x06000806 RID: 2054
    [DllImport("user32.dll", EntryPoint = "DefWindowProcW")]
    private static extern IntPtr DefWindowProc(IntPtr hwnd, uint msg, IntPtr w, IntPtr l);

    // Token: 0x06000807 RID: 2055
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int cmd);

    // Token: 0x06000808 RID: 2056
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPlacement(IntPtr hwnd, ref TaskbarButton.WINDOWPLACEMENT placement);

    // Token: 0x06000809 RID: 2057
    [DllImport("user32.dll")]
    private static extern bool IsIconic(IntPtr hwnd);

    // Token: 0x0600080A RID: 2058
    [DllImport("user32.dll")]
    private static extern bool DestroyWindow(IntPtr hwnd);

    // Token: 0x0600080B RID: 2059
    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hwnd, uint msg, IntPtr w, IntPtr l);

    // Token: 0x0600080C RID: 2060
    [NullableContext(1)]
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr LoadImage(IntPtr instance, string name, uint type, int cx, int cy, uint load);

    // Token: 0x0600080D RID: 2061
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr GetModuleHandle(string name);

    // Token: 0x04000296 RID: 662
    [Nullable(1)]
    private const string ClassName = "HeyClickyTaskbarButton";

    // Token: 0x04000297 RID: 663
    private const int WS_OVERLAPPEDWINDOW = 13565952;

    // Token: 0x04000298 RID: 664
    private const int WS_EX_APPWINDOW = 262144;

    // Token: 0x04000299 RID: 665
    private const int WS_EX_TOOLWINDOW = 128;

    // Token: 0x0400029A RID: 666
    private const int SW_HIDE = 0;

    // Token: 0x0400029B RID: 667
    private const int SW_SHOWMINNOACTIVE = 7;

    // Token: 0x0400029C RID: 668
    private const uint WM_SYSCOMMAND = 274U;

    // Token: 0x0400029D RID: 669
    private const uint WM_CLOSE = 16U;

    // Token: 0x0400029E RID: 670
    private const uint WM_SETICON = 128U;

    // Token: 0x0400029F RID: 671
    private const uint WM_QUERYOPEN = 19U;

    // Token: 0x040002A0 RID: 672
    private const uint WM_ACTIVATE = 6U;

    // Token: 0x040002A1 RID: 673
    private const int SC_RESTORE = 61728;

    // Token: 0x040002A2 RID: 674
    private const int SC_MAXIMIZE = 61488;

    // Token: 0x040002A3 RID: 675
    private const int SC_CLOSE = 61536;

    // Token: 0x040002A4 RID: 676
    private const uint IMAGE_ICON = 1U;

    // Token: 0x040002A5 RID: 677
    private const uint LR_LOADFROMFILE = 16U;

    // Token: 0x040002A6 RID: 678
    private const uint LR_DEFAULTSIZE = 64U;

    // Token: 0x040002A7 RID: 679
    private const uint WM_WINDOWPOSCHANGING = 70U;

    // Token: 0x040002A8 RID: 680
    private const uint SWP_NOMOVE = 2U;

    // Token: 0x040002A9 RID: 681
    private const uint WPF_SETMINPOSITION = 1U;

    // Token: 0x040002AA RID: 682
    private const int ParkedX = -32000;

    // Token: 0x040002AB RID: 683
    private const int ParkedY = -32000;

    // Token: 0x040002AC RID: 684
    private static TaskbarButton.WndProc wndProc;

    // Token: 0x040002AD RID: 685
    private static bool classRegistered;

    // Token: 0x040002AE RID: 686
    private static readonly uint TaskbarCreatedMessage = TaskbarButton.RegisterWindowMessage("TaskbarCreated");

    // Token: 0x040002AF RID: 687
    private IntPtr hwnd;

    // Token: 0x040002B0 RID: 688
    private bool shown;

    // Token: 0x040002B1 RID: 689
    private readonly TaskbarButton.ITaskbarList taskbarList;

    // Token: 0x040002B4 RID: 692
    [Nullable(1)]
    private static readonly Dictionary<IntPtr, TaskbarButton> instances = new Dictionary<IntPtr, TaskbarButton>();

    // Token: 0x040002B5 RID: 693
    private long lastClick;

    // Token: 0x02000475 RID: 1141
    // (Invoke) Token: 0x06002A9D RID: 10909
    [NullableContext(0)]
    private delegate IntPtr WndProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

    // Token: 0x02000476 RID: 1142
    [Guid("56FDF342-FD6D-11d0-958A-006097C9A090")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    private interface ITaskbarList
    {
      // Token: 0x06002AA0 RID: 10912
      [PreserveSig]
      int HrInit();

      // Token: 0x06002AA1 RID: 10913
      [PreserveSig]
      int AddTab(IntPtr hwnd);

      // Token: 0x06002AA2 RID: 10914
      [PreserveSig]
      int DeleteTab(IntPtr hwnd);

      // Token: 0x06002AA3 RID: 10915
      [PreserveSig]
      int ActivateTab(IntPtr hwnd);

      // Token: 0x06002AA4 RID: 10916
      [PreserveSig]
      int SetActiveAlt(IntPtr hwnd);
    }

    // Token: 0x02000477 RID: 1143
    [NullableContext(0)]
    [Guid("56FDF344-FD6D-11d0-958A-006097C9A090")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComImport]
    private class TaskbarListClass
    {
      // Token: 0x06002AA5 RID: 10917
      [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
      public extern TaskbarListClass();
    }

    // Token: 0x02000478 RID: 1144
    [NullableContext(0)]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct WNDCLASSEX
    {
      // Token: 0x04001624 RID: 5668
      public int cbSize;

      // Token: 0x04001625 RID: 5669
      public uint style;

      // Token: 0x04001626 RID: 5670
      public IntPtr lpfnWndProc;

      // Token: 0x04001627 RID: 5671
      public int cbClsExtra;

      // Token: 0x04001628 RID: 5672
      public int cbWndExtra;

      // Token: 0x04001629 RID: 5673
      public IntPtr hInstance;

      // Token: 0x0400162A RID: 5674
      public IntPtr hIcon;

      // Token: 0x0400162B RID: 5675
      public IntPtr hCursor;

      // Token: 0x0400162C RID: 5676
      public IntPtr hbrBackground;

      // Token: 0x0400162D RID: 5677
      [Nullable(2)]
      public string lpszMenuName;

      // Token: 0x0400162E RID: 5678
      [Nullable(1)]
      public string lpszClassName;

      // Token: 0x0400162F RID: 5679
      public IntPtr hIconSm;
    }

    // Token: 0x02000479 RID: 1145
    [NullableContext(0)]
    private struct POINT
    {
      // Token: 0x04001630 RID: 5680
      public int X;

      // Token: 0x04001631 RID: 5681
      public int Y;
    }

    // Token: 0x0200047A RID: 1146
    [NullableContext(0)]
    private struct RECT
    {
      // Token: 0x04001632 RID: 5682
      public int Left;

      // Token: 0x04001633 RID: 5683
      public int Top;

      // Token: 0x04001634 RID: 5684
      public int Right;

      // Token: 0x04001635 RID: 5685
      public int Bottom;
    }

    // Token: 0x0200047B RID: 1147
    [NullableContext(0)]
    private struct WINDOWPLACEMENT
    {
      // Token: 0x04001636 RID: 5686
      public uint length;

      // Token: 0x04001637 RID: 5687
      public uint flags;

      // Token: 0x04001638 RID: 5688
      public uint showCmd;

      // Token: 0x04001639 RID: 5689
      public TaskbarButton.POINT ptMinPosition;

      // Token: 0x0400163A RID: 5690
      public TaskbarButton.POINT ptMaxPosition;

      // Token: 0x0400163B RID: 5691
      public TaskbarButton.RECT rcNormalPosition;
    }

    // Token: 0x0200047C RID: 1148
    [NullableContext(0)]
    private struct WINDOWPOS
    {
      // Token: 0x0400163C RID: 5692
      public IntPtr hwnd;

      // Token: 0x0400163D RID: 5693
      public IntPtr hwndInsertAfter;

      // Token: 0x0400163E RID: 5694
      public int x;

      // Token: 0x0400163F RID: 5695
      public int y;

      // Token: 0x04001640 RID: 5696
      public int cx;

      // Token: 0x04001641 RID: 5697
      public int cy;

      // Token: 0x04001642 RID: 5698
      public uint flags;
    }

    // Token: 0x0200047D RID: 1149
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001643 RID: 5699
      [Nullable(0)]
      public static TaskbarButton.WndProc <0>__Proc;
    }
  }
}
