using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using HeyClicky.App.Interop;

namespace HeyClicky.App.Shell
{
  // Token: 0x020000C3 RID: 195
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class TrayIcon : IDisposable
  {
    // Token: 0x14000016 RID: 22
    // (add) Token: 0x06000814 RID: 2068 RVA: 0x0053C9F4 File Offset: 0x0053C9F4
    // (remove) Token: 0x06000815 RID: 2069 RVA: 0x0053CA2C File Offset: 0x0053CA2C
    public event Action OpenSettings;

    // Token: 0x14000017 RID: 23
    // (add) Token: 0x06000816 RID: 2070 RVA: 0x0053CA64 File Offset: 0x0053CA64
    // (remove) Token: 0x06000817 RID: 2071 RVA: 0x0053CA9C File Offset: 0x0053CA9C
    public event Action Quit;

    // Token: 0x14000018 RID: 24
    // (add) Token: 0x06000818 RID: 2072 RVA: 0x0053CAD4 File Offset: 0x0053CAD4
    // (remove) Token: 0x06000819 RID: 2073 RVA: 0x0053CB0C File Offset: 0x0053CB0C
    public event Action ReportBug;

    // Token: 0x14000019 RID: 25
    // (add) Token: 0x0600081A RID: 2074 RVA: 0x0053CB44 File Offset: 0x0053CB44
    // (remove) Token: 0x0600081B RID: 2075 RVA: 0x0053CB7C File Offset: 0x0053CB7C
    public event Action RequestFeature;

    // Token: 0x1400001A RID: 26
    // (add) Token: 0x0600081C RID: 2076 RVA: 0x0053CBB4 File Offset: 0x0053CBB4
    // (remove) Token: 0x0600081D RID: 2077 RVA: 0x0053CBEC File Offset: 0x0053CBEC
    public event Action WhatsNew;

    // Token: 0x1400001B RID: 27
    // (add) Token: 0x0600081E RID: 2078 RVA: 0x0053CC24 File Offset: 0x0053CC24
    // (remove) Token: 0x0600081F RID: 2079 RVA: 0x0053CC5C File Offset: 0x0053CC5C
    public event Action CheckForUpdates;

    // Token: 0x1400001C RID: 28
    // (add) Token: 0x06000820 RID: 2080 RVA: 0x0053CC94 File Offset: 0x0053CC94
    // (remove) Token: 0x06000821 RID: 2081 RVA: 0x0053CCCC File Offset: 0x0053CCCC
    public event Action Clicked;

    // Token: 0x1400001D RID: 29
    // (add) Token: 0x06000822 RID: 2082 RVA: 0x0053CD04 File Offset: 0x0053CD04
    // (remove) Token: 0x06000823 RID: 2083 RVA: 0x0053CD3C File Offset: 0x0053CD3C
    public event Action<bool> PauseToggled;

    // Token: 0x17000148 RID: 328
    // (get) Token: 0x06000824 RID: 2084 RVA: 0x0053CD74 File Offset: 0x0053CD74
    // (set) Token: 0x06000825 RID: 2085 RVA: 0x0053CD7C File Offset: 0x0053CD7C
    public Func<bool> IsPaused { get; set; }

    // Token: 0x06000826 RID: 2086
    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    private static extern bool Shell_NotifyIconW(uint msg, ref TrayIcon.NOTIFYICONDATAW data);

    // Token: 0x06000827 RID: 2087
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern ushort RegisterClassW(ref TrayIcon.WNDCLASSW wc);

    // Token: 0x06000828 RID: 2088
    [NullableContext(1)]
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr CreateWindowExW(uint ex, string cls, string name, uint style, int x, int y, int w, int h, IntPtr parent, IntPtr menu, IntPtr inst, IntPtr param);

    // Token: 0x06000829 RID: 2089
    [DllImport("user32.dll")]
    private static extern IntPtr DefWindowProcW(IntPtr hwnd, uint msg, IntPtr w, IntPtr l);

    // Token: 0x0600082A RID: 2090
    [DllImport("user32.dll")]
    private static extern bool DestroyWindow(IntPtr hwnd);

    // Token: 0x0600082B RID: 2091
    [DllImport("user32.dll")]
    private static extern IntPtr CreatePopupMenu();

    // Token: 0x0600082C RID: 2092
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern bool AppendMenuW(IntPtr menu, uint flags, IntPtr id, string text);

    // Token: 0x0600082D RID: 2093
    [DllImport("user32.dll")]
    private static extern int TrackPopupMenuEx(IntPtr menu, uint flags, int x, int y, IntPtr hwnd, IntPtr tpm);

    // Token: 0x0600082E RID: 2094
    [DllImport("user32.dll")]
    private static extern bool DestroyMenu(IntPtr menu);

    // Token: 0x0600082F RID: 2095
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hwnd);

    // Token: 0x06000830 RID: 2096
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out Native.POINT p);

    // Token: 0x06000831 RID: 2097
    [DllImport("user32.dll")]
    private static extern IntPtr CreateIconIndirect(ref TrayIcon.ICONINFO info);

    // Token: 0x06000832 RID: 2098
    [DllImport("user32.dll")]
    private static extern bool DestroyIcon(IntPtr icon);

    // Token: 0x06000833 RID: 2099
    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateBitmap(int w, int h, uint planes, uint bpp, byte[] bits);

    // Token: 0x06000834 RID: 2100
    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateDIBSection(IntPtr hdc, ref TrayIcon.BITMAPINFOHEADER bi, uint usage, out IntPtr bits, IntPtr section, uint offset);

    // Token: 0x06000835 RID: 2101
    [DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr o);

    // Token: 0x06000836 RID: 2102
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandleW(string name);

    // Token: 0x06000837 RID: 2103
    [NullableContext(1)]
    [DllImport("user32.dll")]
    private static extern uint RegisterWindowMessageW([MarshalAs(UnmanagedType.LPWStr)] string name);

    // Token: 0x06000838 RID: 2104 RVA: 0x0053CD88 File Offset: 0x0053CD88
    [NullableContext(1)]
    public TrayIcon(string tooltip, uint colorArgb)
    {
      this.proc = new TrayIcon.WndProcDelegate(this.WndProc);
      TrayIcon.WNDCLASSW wndclassw = new TrayIcon.WNDCLASSW
      {
        lpfnWndProc = Marshal.GetFunctionPointerForDelegate<TrayIcon.WndProcDelegate>(this.proc),
        hInstance = TrayIcon.GetModuleHandleW(null),
        lpszClassName = "HeyClickyTray"
      };
      TrayIcon.RegisterClassW(ref wndclassw);
      this.hwnd = TrayIcon.CreateWindowExW(0U, "HeyClickyTray", "HeyClickyTray", 0U, 0, 0, 0, 0, (IntPtr)(-3), (IntPtr)0, wndclassw.hInstance, (IntPtr)0);
      this.taskbarCreated = TrayIcon.RegisterWindowMessageW("TaskbarCreated");
      this.icon = TrayIcon.MakeIcon(colorArgb);
      this.data = new TrayIcon.NOTIFYICONDATAW
      {
        cbSize = Marshal.SizeOf<TrayIcon.NOTIFYICONDATAW>(),
        hWnd = this.hwnd,
        uID = 1U,
        uFlags = 135U,
        uCallbackMessage = 32769U,
        hIcon = this.icon,
        szTip = tooltip,
        szInfo = "",
        szInfoTitle = "",
        uVersion = 4U
      };
      this.Add();
    }

    // Token: 0x06000839 RID: 2105 RVA: 0x0053CEB4 File Offset: 0x0053CEB4
    private void Add()
    {
      TrayIcon.Shell_NotifyIconW(0U, ref this.data);
      TrayIcon.Shell_NotifyIconW(4U, ref this.data);
    }

    // Token: 0x0600083A RID: 2106 RVA: 0x0053CED0 File Offset: 0x0053CED0
    private IntPtr WndProc(IntPtr h, uint msg, IntPtr w, IntPtr l)
    {
      if (msg == 32769U)
      {
        uint num = (uint)(l & (IntPtr)65535);
        if (num == 514U || num == 1024U)
        {
          Action clicked = this.Clicked;
          if (clicked != null)
          {
            clicked();
          }
          else
          {
            Action openSettings = this.OpenSettings;
            if (openSettings != null)
            {
              openSettings();
            }
          }
          return (IntPtr)0;
        }
        if (num == 123U || num == 517U)
        {
          this.ShowMenu();
          return (IntPtr)0;
        }
      }
      else if (msg == this.taskbarCreated)
      {
        this.Add();
      }
      return TrayIcon.DefWindowProcW(h, msg, w, l);
    }

    // Token: 0x0600083B RID: 2107 RVA: 0x0053CF58 File Offset: 0x0053CF58
    private void ShowMenu()
    {
      IntPtr intPtr = TrayIcon.CreatePopupMenu();
      TrayIcon.AppendMenuW(intPtr, 0U, (IntPtr)1, "Open Settings");
      Func<bool> isPaused = this.IsPaused;
      bool flag = isPaused != null && isPaused();
      TrayIcon.AppendMenuW(intPtr, 0U, (IntPtr)2, flag ? "Resume HeyClicky" : "Pause HeyClicky");
      TrayIcon.AppendMenuW(intPtr, 2048U, (IntPtr)0, null);
      TrayIcon.AppendMenuW(intPtr, 0U, (IntPtr)10, "Report a Bug…");
      TrayIcon.AppendMenuW(intPtr, 0U, (IntPtr)11, "Request a Feature…");
      TrayIcon.AppendMenuW(intPtr, 0U, (IntPtr)12, "What's New");
      TrayIcon.AppendMenuW(intPtr, 0U, (IntPtr)13, "Check for Updates…");
      TrayIcon.AppendMenuW(intPtr, 2048U, (IntPtr)0, null);
      TrayIcon.AppendMenuW(intPtr, 0U, (IntPtr)3, "Quit HeyClicky");
      Native.POINT point;
      TrayIcon.GetCursorPos(out point);
      TrayIcon.SetForegroundWindow(this.hwnd);
      int num = TrayIcon.TrackPopupMenuEx(intPtr, 288U, point.X, point.Y, this.hwnd, (IntPtr)0);
      TrayIcon.DestroyMenu(intPtr);
      switch (num)
      {
      case 1:
      {
        Action openSettings = this.OpenSettings;
        if (openSettings == null)
        {
          return;
        }
        openSettings();
        return;
      }
      case 2:
      {
        Action<bool> pauseToggled = this.PauseToggled;
        if (pauseToggled == null)
        {
          return;
        }
        pauseToggled(!flag);
        return;
      }
      case 3:
      {
        Action quit = this.Quit;
        if (quit == null)
        {
          return;
        }
        quit();
        return;
      }
      case 4:
      case 5:
      case 6:
      case 7:
      case 8:
      case 9:
        break;
      case 10:
      {
        Action reportBug = this.ReportBug;
        if (reportBug == null)
        {
          return;
        }
        reportBug();
        return;
      }
      case 11:
      {
        Action requestFeature = this.RequestFeature;
        if (requestFeature == null)
        {
          return;
        }
        requestFeature();
        return;
      }
      case 12:
      {
        Action whatsNew = this.WhatsNew;
        if (whatsNew == null)
        {
          return;
        }
        whatsNew();
        return;
      }
      case 13:
      {
        Action checkForUpdates = this.CheckForUpdates;
        if (checkForUpdates == null)
        {
          return;
        }
        checkForUpdates();
        break;
      }
      default:
        return;
      }
    }

    // Token: 0x0600083C RID: 2108 RVA: 0x0053D104 File Offset: 0x0053D104
    private static IntPtr MakeIcon(uint argb)
    {
      TrayIcon.BITMAPINFOHEADER bitmapinfoheader = new TrayIcon.BITMAPINFOHEADER
      {
        biSize = Marshal.SizeOf<TrayIcon.BITMAPINFOHEADER>(),
        biWidth = 32,
        biHeight = -32,
        biPlanes = 1,
        biBitCount = 32
      };
      IntPtr intPtr2;
      IntPtr intPtr = TrayIcon.CreateDIBSection((IntPtr)0, ref bitmapinfoheader, 0U, out intPtr2, (IntPtr)0, 0U);
      int[] array = new int[1024];
      double num = 24.0;
      double num2 = num * Math.Sqrt(3.0) / 2.0;
      double num3 = 16.0;
      ValueTuple<double, double>[] array2 = new ValueTuple<double, double>[]
      {
        new ValueTuple<double, double>(0.0, -num2 / 1.5),
        new ValueTuple<double, double>(num / 2.0, num2 / 3.0),
        new ValueTuple<double, double>(-num / 2.0, num2 / 3.0)
      };
      double num4 = -0.6108652381980153;
      for (int i = 0; i < 3; i++)
      {
        ValueTuple<double, double> valueTuple = array2[i];
        double item = valueTuple.Item1;
        double item2 = valueTuple.Item2;
        array2[i] = new ValueTuple<double, double>(num3 + item * Math.Cos(num4) - item2 * Math.Sin(num4), num3 + item * Math.Sin(num4) + item2 * Math.Cos(num4));
      }
      byte b = (byte)(argb >> 16);
      byte b2 = (byte)(argb >> 8);
      byte b3 = (byte)argb;
      for (int j = 0; j < 32; j++)
      {
        for (int k = 0; k < 32; k++)
        {
          int num5 = 0;
          for (int l = 0; l < 4; l++)
          {
            for (int m = 0; m < 4; m++)
            {
              if (TrayIcon.<MakeIcon>g__Inside|72_0((double)k + ((double)m + 0.5) / 4.0, (double)j + ((double)l + 0.5) / 4.0, array2))
              {
                num5++;
              }
            }
          }
          int num6 = num5 * 255 / 16;
          array[j * 32 + k] = (num6 << 24) | ((int)b * num6 / 255 << 16) | ((int)b2 * num6 / 255 << 8) | ((int)b3 * num6 / 255);
        }
      }
      Marshal.Copy(array, 0, intPtr2, array.Length);
      IntPtr intPtr3 = TrayIcon.CreateBitmap(32, 32, 1U, 1U, new byte[128]);
      TrayIcon.ICONINFO iconinfo = new TrayIcon.ICONINFO
      {
        fIcon = true,
        hbmColor = intPtr,
        hbmMask = intPtr3
      };
      IntPtr intPtr4 = TrayIcon.CreateIconIndirect(ref iconinfo);
      TrayIcon.DeleteObject(intPtr);
      TrayIcon.DeleteObject(intPtr3);
      return intPtr4;
    }

    // Token: 0x0600083D RID: 2109 RVA: 0x0053D3C8 File Offset: 0x0053D3C8
    public void SetColor(uint colorArgb)
    {
      IntPtr intPtr = this.icon;
      this.icon = TrayIcon.MakeIcon(colorArgb);
      this.data.hIcon = this.icon;
      TrayIcon.Shell_NotifyIconW(1U, ref this.data);
      TrayIcon.DestroyIcon(intPtr);
    }

    // Token: 0x0600083E RID: 2110 RVA: 0x0053D400 File Offset: 0x0053D400
    [NullableContext(1)]
    public void SetTooltip(string tooltip)
    {
      if (tooltip.Length > 127)
      {
        tooltip = tooltip.Substring(0, 126) + "…";
      }
      if (tooltip == this.data.szTip)
      {
        return;
      }
      this.data.szTip = tooltip;
      TrayIcon.Shell_NotifyIconW(1U, ref this.data);
      Log.Info("tray", "tooltip: " + tooltip);
    }

    // Token: 0x0600083F RID: 2111 RVA: 0x0053D470 File Offset: 0x0053D470
    public void Dispose()
    {
      TrayIcon.Shell_NotifyIconW(2U, ref this.data);
      TrayIcon.DestroyIcon(this.icon);
      TrayIcon.DestroyWindow(this.hwnd);
    }

    // Token: 0x06000840 RID: 2112 RVA: 0x0053D498 File Offset: 0x0053D498
    [CompilerGenerated]
    internal static bool <MakeIcon>g__Inside|72_0(double x, double y, [TupleElementNames(new string[] { "x", "y" })] [Nullable(new byte[] { 1, 0 })] ValueTuple<double, double>[] t)
    {
      double num = (x - t[1].Item1) * (t[0].Item2 - t[1].Item2) - (t[0].Item1 - t[1].Item1) * (y - t[1].Item2);
      double num2 = (x - t[2].Item1) * (t[1].Item2 - t[2].Item2) - (t[1].Item1 - t[2].Item1) * (y - t[2].Item2);
      double num3 = (x - t[0].Item1) * (t[2].Item2 - t[0].Item2) - (t[2].Item1 - t[0].Item1) * (y - t[0].Item2);
      bool flag = num < 0.0 || num2 < 0.0 || num3 < 0.0;
      bool flag2 = num > 0.0 || num2 > 0.0 || num3 > 0.0;
      return !flag || !flag2;
    }

    // Token: 0x040002C1 RID: 705
    private const uint WM_TRAY = 32769U;

    // Token: 0x040002C2 RID: 706
    private const uint NIM_ADD = 0U;

    // Token: 0x040002C3 RID: 707
    private const uint NIM_MODIFY = 1U;

    // Token: 0x040002C4 RID: 708
    private const uint NIM_DELETE = 2U;

    // Token: 0x040002C5 RID: 709
    private const uint NIM_SETVERSION = 4U;

    // Token: 0x040002C6 RID: 710
    private const uint NIF_MESSAGE = 1U;

    // Token: 0x040002C7 RID: 711
    private const uint NIF_ICON = 2U;

    // Token: 0x040002C8 RID: 712
    private const uint NIF_TIP = 4U;

    // Token: 0x040002C9 RID: 713
    private const uint NIF_SHOWTIP = 128U;

    // Token: 0x040002CA RID: 714
    private const int ID_SETTINGS = 1;

    // Token: 0x040002CB RID: 715
    private const int ID_PAUSE = 2;

    // Token: 0x040002CC RID: 716
    private const int ID_QUIT = 3;

    // Token: 0x040002CD RID: 717
    [Nullable(1)]
    private readonly TrayIcon.WndProcDelegate proc;

    // Token: 0x040002CE RID: 718
    private readonly IntPtr hwnd;

    // Token: 0x040002CF RID: 719
    private IntPtr icon;

    // Token: 0x040002D0 RID: 720
    private readonly uint taskbarCreated;

    // Token: 0x040002D1 RID: 721
    private TrayIcon.NOTIFYICONDATAW data;

    // Token: 0x040002D2 RID: 722
    private const int ID_REPORT_BUG = 10;

    // Token: 0x040002D3 RID: 723
    private const int ID_REQUEST_FEATURE = 11;

    // Token: 0x040002D4 RID: 724
    private const int ID_WHATS_NEW = 12;

    // Token: 0x040002D5 RID: 725
    private const int ID_CHECK_UPDATES = 13;

    // Token: 0x0200047E RID: 1150
    [NullableContext(1)]
    [Nullable(0)]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct NOTIFYICONDATAW
    {
      // Token: 0x04001644 RID: 5700
      public int cbSize;

      // Token: 0x04001645 RID: 5701
      public IntPtr hWnd;

      // Token: 0x04001646 RID: 5702
      public uint uID;

      // Token: 0x04001647 RID: 5703
      public uint uFlags;

      // Token: 0x04001648 RID: 5704
      public uint uCallbackMessage;

      // Token: 0x04001649 RID: 5705
      public IntPtr hIcon;

      // Token: 0x0400164A RID: 5706
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
      public string szTip;

      // Token: 0x0400164B RID: 5707
      public uint dwState;

      // Token: 0x0400164C RID: 5708
      public uint dwStateMask;

      // Token: 0x0400164D RID: 5709
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
      public string szInfo;

      // Token: 0x0400164E RID: 5710
      public uint uVersion;

      // Token: 0x0400164F RID: 5711
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
      public string szInfoTitle;

      // Token: 0x04001650 RID: 5712
      public uint dwInfoFlags;

      // Token: 0x04001651 RID: 5713
      public Guid guidItem;

      // Token: 0x04001652 RID: 5714
      public IntPtr hBalloonIcon;
    }

    // Token: 0x0200047F RID: 1151
    // (Invoke) Token: 0x06002AA7 RID: 10919
    [NullableContext(0)]
    private delegate IntPtr WndProcDelegate(IntPtr hwnd, uint msg, IntPtr w, IntPtr l);

    // Token: 0x02000480 RID: 1152
    [NullableContext(0)]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct WNDCLASSW
    {
      // Token: 0x04001653 RID: 5715
      public uint style;

      // Token: 0x04001654 RID: 5716
      public IntPtr lpfnWndProc;

      // Token: 0x04001655 RID: 5717
      public int cbClsExtra;

      // Token: 0x04001656 RID: 5718
      public int cbWndExtra;

      // Token: 0x04001657 RID: 5719
      public IntPtr hInstance;

      // Token: 0x04001658 RID: 5720
      public IntPtr hIcon;

      // Token: 0x04001659 RID: 5721
      public IntPtr hCursor;

      // Token: 0x0400165A RID: 5722
      public IntPtr hbrBackground;

      // Token: 0x0400165B RID: 5723
      [Nullable(2)]
      public string lpszMenuName;

      // Token: 0x0400165C RID: 5724
      [Nullable(1)]
      public string lpszClassName;
    }

    // Token: 0x02000481 RID: 1153
    [NullableContext(0)]
    private struct ICONINFO
    {
      // Token: 0x0400165D RID: 5725
      public bool fIcon;

      // Token: 0x0400165E RID: 5726
      public int xHotspot;

      // Token: 0x0400165F RID: 5727
      public int yHotspot;

      // Token: 0x04001660 RID: 5728
      public IntPtr hbmMask;

      // Token: 0x04001661 RID: 5729
      public IntPtr hbmColor;
    }

    // Token: 0x02000482 RID: 1154
    [NullableContext(0)]
    private struct BITMAPINFOHEADER
    {
      // Token: 0x04001662 RID: 5730
      public int biSize;

      // Token: 0x04001663 RID: 5731
      public int biWidth;

      // Token: 0x04001664 RID: 5732
      public int biHeight;

      // Token: 0x04001665 RID: 5733
      public short biPlanes;

      // Token: 0x04001666 RID: 5734
      public short biBitCount;

      // Token: 0x04001667 RID: 5735
      public int biCompression;

      // Token: 0x04001668 RID: 5736
      public int biSizeImage;

      // Token: 0x04001669 RID: 5737
      public int biXPelsPerMeter;

      // Token: 0x0400166A RID: 5738
      public int biYPelsPerMeter;

      // Token: 0x0400166B RID: 5739
      public int biClrUsed;

      // Token: 0x0400166C RID: 5740
      public int biClrImportant;
    }
  }
}
