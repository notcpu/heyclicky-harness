using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeyClicky.App.Interop
{
  // Token: 0x0200019A RID: 410
  internal static class VirtualDesktops
  {
    // Token: 0x0600164B RID: 5707
    [NullableContext(1)]
    [DllImport("user32.dll")]
    private static extern bool EnumWindows(VirtualDesktops.EnumWindowsProc callback, IntPtr lParam);

    // Token: 0x0600164C RID: 5708
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    // Token: 0x0600164D RID: 5709
    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hwnd);

    // Token: 0x0600164E RID: 5710
    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint processId);

    // Token: 0x0600164F RID: 5711 RVA: 0x005BA338 File Offset: 0x005BA338
    [NullableContext(2)]
    private static VirtualDesktops.IVirtualDesktopManager Manager()
    {
      VirtualDesktops.IVirtualDesktopManager virtualDesktopManager2;
      try
      {
        VirtualDesktops.IVirtualDesktopManager virtualDesktopManager;
        if ((virtualDesktopManager = VirtualDesktops.manager) == null)
        {
          virtualDesktopManager = (VirtualDesktops.manager = (VirtualDesktops.IVirtualDesktopManager)new VirtualDesktops.VirtualDesktopManager());
        }
        virtualDesktopManager2 = virtualDesktopManager;
      }
      catch (Exception ex)
      {
        Log.Info("window", "virtual desktops unavailable: " + ex.Message);
        virtualDesktopManager2 = null;
      }
      return virtualDesktopManager2;
    }

    // Token: 0x06001650 RID: 5712 RVA: 0x005BA394 File Offset: 0x005BA394
    public static bool IsOnCurrentDesktop(IntPtr hwnd)
    {
      if (hwnd != 0)
      {
        VirtualDesktops.IVirtualDesktopManager virtualDesktopManager = VirtualDesktops.Manager();
        if (virtualDesktopManager != null)
        {
          bool flag;
          try
          {
            bool flag2;
            flag = virtualDesktopManager.IsWindowOnCurrentVirtualDesktop(hwnd, out flag2) != 0 || flag2;
          }
          catch
          {
            flag = true;
          }
          return flag;
        }
      }
      return true;
    }

    // Token: 0x06001651 RID: 5713 RVA: 0x005BA3D8 File Offset: 0x005BA3D8
    public static bool MoveToCurrentDesktop(IntPtr hwnd)
    {
      if (hwnd != 0)
      {
        VirtualDesktops.IVirtualDesktopManager virtualDesktopManager = VirtualDesktops.Manager();
        if (virtualDesktopManager != null)
        {
          bool flag2;
          try
          {
            bool flag;
            if (virtualDesktopManager.IsWindowOnCurrentVirtualDesktop(hwnd, out flag) != 0 || flag)
            {
              flag2 = false;
            }
            else
            {
              Guid? guid = VirtualDesktops.CurrentDesktopId(virtualDesktopManager, hwnd);
              if (guid != null)
              {
                Guid valueOrDefault = guid.GetValueOrDefault();
                int num = virtualDesktopManager.MoveWindowToDesktop(hwnd, ref valueOrDefault);
                if (num != 0)
                {
                  string text = "window";
                  DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(45, 1);
                  defaultInterpolatedStringHandler.AppendLiteral("couldn't move the window to this desktop (0x");
                  defaultInterpolatedStringHandler.AppendFormatted<int>(num, "X8");
                  defaultInterpolatedStringHandler.AppendLiteral(")");
                  Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
                  flag2 = false;
                }
                else
                {
                  Log.Info("window", "moved the HeyClicky window to the desktop the user is on");
                  flag2 = true;
                }
              }
              else
              {
                flag2 = false;
              }
            }
          }
          catch (Exception ex)
          {
            Log.Info("window", "couldn't move the window to this desktop: " + ex.Message);
            flag2 = false;
          }
          return flag2;
        }
      }
      return false;
    }

    // Token: 0x06001652 RID: 5714 RVA: 0x005BA4CC File Offset: 0x005BA4CC
    [NullableContext(1)]
    private static Guid? CurrentDesktopId(VirtualDesktops.IVirtualDesktopManager desktops, IntPtr ours)
    {
      VirtualDesktops.<>c__DisplayClass11_0 CS$<>8__locals1 = new VirtualDesktops.<>c__DisplayClass11_0();
      CS$<>8__locals1.ours = ours;
      CS$<>8__locals1.desktops = desktops;
      Guid? guid = CS$<>8__locals1.<CurrentDesktopId>g__IdOf|0(VirtualDesktops.GetForegroundWindow());
      if (guid != null)
      {
        Guid valueOrDefault = guid.GetValueOrDefault();
        return new Guid?(valueOrDefault);
      }
      VirtualDesktops.GetWindowThreadProcessId(CS$<>8__locals1.ours, out CS$<>8__locals1.ourProcess);
      CS$<>8__locals1.found = null;
      VirtualDesktops.EnumWindows(delegate(IntPtr candidate, IntPtr _)
      {
        uint num;
        VirtualDesktops.GetWindowThreadProcessId(candidate, out num);
        if (num != CS$<>8__locals1.ourProcess)
        {
          CS$<>8__locals1.found = base.<CurrentDesktopId>g__IdOf|0(candidate);
        }
        return CS$<>8__locals1.found == null;
      }, (IntPtr)0);
      return CS$<>8__locals1.found;
    }

    // Token: 0x04000AF8 RID: 2808
    [Nullable(2)]
    private static VirtualDesktops.IVirtualDesktopManager manager;

    // Token: 0x0200074A RID: 1866
    [Guid("a5cd92ff-29be-454c-8d04-d82879fb3f1b")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    private interface IVirtualDesktopManager
    {
      // Token: 0x060034CC RID: 13516
      [PreserveSig]
      int IsWindowOnCurrentVirtualDesktop(IntPtr topLevelWindow, [MarshalAs(UnmanagedType.Bool)] out bool onCurrentDesktop);

      // Token: 0x060034CD RID: 13517
      [PreserveSig]
      int GetWindowDesktopId(IntPtr topLevelWindow, out Guid desktopId);

      // Token: 0x060034CE RID: 13518
      [PreserveSig]
      int MoveWindowToDesktop(IntPtr topLevelWindow, ref Guid desktopId);
    }

    // Token: 0x0200074B RID: 1867
    [Guid("aa509086-5ca9-4c25-8f95-589d3c07b48a")]
    [ComImport]
    private class VirtualDesktopManager
    {
      // Token: 0x060034CF RID: 13519
      [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
      public extern VirtualDesktopManager();
    }

    // Token: 0x0200074C RID: 1868
    // (Invoke) Token: 0x060034D1 RID: 13521
    private delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);
  }
}
