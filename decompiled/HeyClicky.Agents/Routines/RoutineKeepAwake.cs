using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeyClicky.Agents.Routines
{
  // Token: 0x0200002C RID: 44
  internal static class RoutineKeepAwake
  {
    // Token: 0x060004A0 RID: 1184 RVA: 0x000ED3F0 File Offset: 0x000ED3F0
    public static void Update(bool hasActiveRoutines)
    {
      bool flag = hasActiveRoutines && RoutineKeepAwake.IsOnAcPower();
      object obj = RoutineKeepAwake.gate;
      lock (obj)
      {
        if (flag && RoutineKeepAwake.request == IntPtr.Zero)
        {
          RoutineKeepAwake.ReasonContext reasonContext = new RoutineKeepAwake.ReasonContext
          {
            Version = 0U,
            Flags = 1U,
            SimpleReasonString = "HeyClicky is running a routine on a schedule"
          };
          IntPtr intPtr = RoutineKeepAwake.PowerCreateRequest(ref reasonContext);
          if (intPtr != IntPtr.Zero && intPtr != new IntPtr(-1))
          {
            if (!RoutineKeepAwake.PowerSetRequest(intPtr, 1))
            {
              RoutineKeepAwake.CloseHandle(intPtr);
            }
            else
            {
              RoutineKeepAwake.request = intPtr;
              Log.Info("Routines", "keep-awake request ON (routines armed, on AC power)");
            }
          }
        }
        else if (!flag && RoutineKeepAwake.request != IntPtr.Zero)
        {
          RoutineKeepAwake.PowerClearRequest(RoutineKeepAwake.request, 1);
          RoutineKeepAwake.CloseHandle(RoutineKeepAwake.request);
          RoutineKeepAwake.request = IntPtr.Zero;
          Log.Info("Routines", "keep-awake request OFF");
        }
      }
    }

    // Token: 0x060004A1 RID: 1185 RVA: 0x000ED4F4 File Offset: 0x000ED4F4
    private static bool IsOnAcPower()
    {
      RoutineKeepAwake.SystemPowerStatus systemPowerStatus;
      return RoutineKeepAwake.GetSystemPowerStatus(out systemPowerStatus) && systemPowerStatus.ACLineStatus == 1;
    }

    // Token: 0x060004A2 RID: 1186
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr PowerCreateRequest(ref RoutineKeepAwake.ReasonContext context);

    // Token: 0x060004A3 RID: 1187
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool PowerSetRequest(IntPtr powerRequest, int requestType);

    // Token: 0x060004A4 RID: 1188
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool PowerClearRequest(IntPtr powerRequest, int requestType);

    // Token: 0x060004A5 RID: 1189
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr handle);

    // Token: 0x060004A6 RID: 1190
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GetSystemPowerStatus(out RoutineKeepAwake.SystemPowerStatus status);

    // Token: 0x040001B3 RID: 435
    [Nullable(1)]
    private const string Reason = "HeyClicky is running a routine on a schedule";

    // Token: 0x040001B4 RID: 436
    private const uint PowerRequestContextVersion = 0U;

    // Token: 0x040001B5 RID: 437
    private const uint PowerRequestContextSimpleString = 1U;

    // Token: 0x040001B6 RID: 438
    private const int PowerRequestSystemRequired = 1;

    // Token: 0x040001B7 RID: 439
    [Nullable(1)]
    private static readonly object gate = new object();

    // Token: 0x040001B8 RID: 440
    private static IntPtr request = IntPtr.Zero;

    // Token: 0x0200012E RID: 302
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct ReasonContext
    {
      // Token: 0x04000675 RID: 1653
      public uint Version;

      // Token: 0x04000676 RID: 1654
      public uint Flags;

      // Token: 0x04000677 RID: 1655
      [Nullable(1)]
      [MarshalAs(UnmanagedType.LPWStr)]
      public string SimpleReasonString;
    }

    // Token: 0x0200012F RID: 303
    private struct SystemPowerStatus
    {
      // Token: 0x04000678 RID: 1656
      public byte ACLineStatus;

      // Token: 0x04000679 RID: 1657
      public byte BatteryFlag;

      // Token: 0x0400067A RID: 1658
      public byte BatteryLifePercent;

      // Token: 0x0400067B RID: 1659
      public byte SystemStatusFlag;

      // Token: 0x0400067C RID: 1660
      public int BatteryLifeTime;

      // Token: 0x0400067D RID: 1661
      public int BatteryFullLifeTime;
    }
  }
}
