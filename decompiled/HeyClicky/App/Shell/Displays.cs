using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using HeyClicky.App.Interop;

namespace HeyClicky.App.Shell
{
  // Token: 0x020000BC RID: 188
  public static class Displays
  {
    // Token: 0x0600079E RID: 1950 RVA: 0x005398FC File Offset: 0x005398FC
    public static Displays.Monitor UnderCursor()
    {
      Native.POINT point;
      Native.GetCursorPos(out point);
      return Displays.From(Native.MonitorFromPoint(point, 1));
    }

    // Token: 0x0600079F RID: 1951 RVA: 0x00539920 File Offset: 0x00539920
    public static Displays.Monitor Primary()
    {
      return Displays.From(Native.MonitorFromPoint(new Native.POINT
      {
        X = 0,
        Y = 0
      }, 1));
    }

    // Token: 0x060007A0 RID: 1952 RVA: 0x00539954 File Offset: 0x00539954
    public static Displays.Monitor ForWindow(IntPtr hwnd)
    {
      return Displays.From(Native.MonitorFromWindow(hwnd, 2));
    }

    // Token: 0x060007A1 RID: 1953 RVA: 0x00539964 File Offset: 0x00539964
    [NullableContext(1)]
    public static Displays.Monitor ForWindow(OverlayWindow window)
    {
      if (window.IsShown)
      {
        return Displays.ForWindow(window.Hwnd);
      }
      ValueTuple<int, int, int, int> rectPx = window.RectPx;
      int item = rectPx.Item1;
      int item2 = rectPx.Item2;
      int item3 = rectPx.Item3;
      int item4 = rectPx.Item4;
      return Displays.At(item + item3 / 2, item2 + item4 / 2);
    }

    // Token: 0x060007A2 RID: 1954 RVA: 0x005399B8 File Offset: 0x005399B8
    public static Displays.Monitor At(int x, int y)
    {
      return Displays.From(Native.MonitorFromPoint(new Native.POINT
      {
        X = x,
        Y = y
      }, 2));
    }

    // Token: 0x060007A3 RID: 1955 RVA: 0x005399EC File Offset: 0x005399EC
    public static double DpiAt(int x, int y)
    {
      return Displays.At(x, y).Scale * 96.0;
    }

    // Token: 0x060007A4 RID: 1956 RVA: 0x00539A14 File Offset: 0x00539A14
    public static Displays.Monitor From(IntPtr mon)
    {
      Native.MONITORINFO monitorinfo = new Native.MONITORINFO
      {
        cbSize = Marshal.SizeOf<Native.MONITORINFO>()
      };
      Native.GetMonitorInfo(mon, ref monitorinfo);
      uint num;
      uint num2;
      Native.GetDpiForMonitor(mon, 0, out num, out num2);
      return new Displays.Monitor(mon, monitorinfo.rcMonitor, monitorinfo.rcWork, (num == 0U) ? 1.0 : (num / 96.0));
    }

    // Token: 0x060007A5 RID: 1957 RVA: 0x00539A7C File Offset: 0x00539A7C
    [return: TupleElementNames(new string[] { "x", "y", "w", "h" })]
    public static ValueTuple<int, int, int, int> CenteredInWork(Displays.Monitor m, double wDip, double hDip)
    {
      int num = (int)Math.Round(wDip * m.Scale);
      int num2 = (int)Math.Round(hDip * m.Scale);
      int num3 = m.Work.Left + (m.Work.Width - num) / 2;
      int num4 = m.Work.Top + (m.Work.Height - num2) / 2;
      return new ValueTuple<int, int, int, int>(num3, num4, num, num2);
    }

    // Token: 0x0200046B RID: 1131
    public readonly struct Monitor : IEquatable<Displays.Monitor>
    {
      // Token: 0x06002A62 RID: 10850 RVA: 0x0064F110 File Offset: 0x0064F110
      public Monitor(IntPtr Handle, Native.RECT Bounds, Native.RECT Work, double Scale)
      {
        this.Handle = Handle;
        this.Bounds = Bounds;
        this.Work = Work;
        this.Scale = Scale;
      }

      // Token: 0x17000706 RID: 1798
      // (get) Token: 0x06002A63 RID: 10851 RVA: 0x0064F130 File Offset: 0x0064F130
      // (set) Token: 0x06002A64 RID: 10852 RVA: 0x0064F138 File Offset: 0x0064F138
      public IntPtr Handle { get; set; }

      // Token: 0x17000707 RID: 1799
      // (get) Token: 0x06002A65 RID: 10853 RVA: 0x0064F144 File Offset: 0x0064F144
      // (set) Token: 0x06002A66 RID: 10854 RVA: 0x0064F14C File Offset: 0x0064F14C
      public Native.RECT Bounds { get; set; }

      // Token: 0x17000708 RID: 1800
      // (get) Token: 0x06002A67 RID: 10855 RVA: 0x0064F158 File Offset: 0x0064F158
      // (set) Token: 0x06002A68 RID: 10856 RVA: 0x0064F160 File Offset: 0x0064F160
      public Native.RECT Work { get; set; }

      // Token: 0x17000709 RID: 1801
      // (get) Token: 0x06002A69 RID: 10857 RVA: 0x0064F16C File Offset: 0x0064F16C
      // (set) Token: 0x06002A6A RID: 10858 RVA: 0x0064F174 File Offset: 0x0064F174
      public double Scale { get; set; }

      // Token: 0x06002A6B RID: 10859 RVA: 0x0064F180 File Offset: 0x0064F180
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Monitor");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002A6C RID: 10860 RVA: 0x0064F1CC File Offset: 0x0064F1CC
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("Handle = ");
        builder.Append(this.Handle.ToString());
        builder.Append(", Bounds = ");
        builder.Append(this.Bounds.ToString());
        builder.Append(", Work = ");
        builder.Append(this.Work.ToString());
        builder.Append(", Scale = ");
        builder.Append(this.Scale.ToString());
        return true;
      }

      // Token: 0x06002A6D RID: 10861 RVA: 0x0064F278 File Offset: 0x0064F278
      [CompilerGenerated]
      public static bool operator !=(Displays.Monitor left, Displays.Monitor right)
      {
        return !(left == right);
      }

      // Token: 0x06002A6E RID: 10862 RVA: 0x0064F284 File Offset: 0x0064F284
      [CompilerGenerated]
      public static bool operator ==(Displays.Monitor left, Displays.Monitor right)
      {
        return left.Equals(right);
      }

      // Token: 0x06002A6F RID: 10863 RVA: 0x0064F290 File Offset: 0x0064F290
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<IntPtr>.Default.GetHashCode(this.<Handle>k__BackingField) * -1521134295 + EqualityComparer<Native.RECT>.Default.GetHashCode(this.<Bounds>k__BackingField)) * -1521134295 + EqualityComparer<Native.RECT>.Default.GetHashCode(this.<Work>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Scale>k__BackingField);
      }

      // Token: 0x06002A70 RID: 10864 RVA: 0x0064F2F4 File Offset: 0x0064F2F4
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is Displays.Monitor && this.Equals((Displays.Monitor)obj);
      }

      // Token: 0x06002A71 RID: 10865 RVA: 0x0064F30C File Offset: 0x0064F30C
      [CompilerGenerated]
      public bool Equals(Displays.Monitor other)
      {
        return EqualityComparer<IntPtr>.Default.Equals(this.<Handle>k__BackingField, other.<Handle>k__BackingField) && EqualityComparer<Native.RECT>.Default.Equals(this.<Bounds>k__BackingField, other.<Bounds>k__BackingField) && EqualityComparer<Native.RECT>.Default.Equals(this.<Work>k__BackingField, other.<Work>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Scale>k__BackingField, other.<Scale>k__BackingField);
      }

      // Token: 0x06002A72 RID: 10866 RVA: 0x0064F37C File Offset: 0x0064F37C
      [CompilerGenerated]
      public void Deconstruct(out IntPtr Handle, out Native.RECT Bounds, out Native.RECT Work, out double Scale)
      {
        Handle = this.Handle;
        Bounds = this.Bounds;
        Work = this.Work;
        Scale = this.Scale;
      }
    }
  }
}
