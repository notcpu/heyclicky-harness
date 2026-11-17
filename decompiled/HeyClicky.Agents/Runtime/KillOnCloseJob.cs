using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace HeyClicky.Agents.Runtime
{
  // Token: 0x02000027 RID: 39
  internal sealed class KillOnCloseJob : IDisposable
  {
    // Token: 0x1700010A RID: 266
    // (get) Token: 0x0600040B RID: 1035 RVA: 0x000EA508 File Offset: 0x000EA508
    [Nullable(2)]
    public static KillOnCloseJob Shared
    {
      [NullableContext(2)]
      get
      {
        return KillOnCloseJob.SharedJob.Value;
      }
    }

    // Token: 0x0600040C RID: 1036 RVA: 0x000EA514 File Offset: 0x000EA514
    [NullableContext(2)]
    public KillOnCloseJob(string name = null)
    {
      this.handle = KillOnCloseJob.CreateJobObjectW(IntPtr.Zero, name);
      if (this.handle.IsInvalid)
      {
        throw new Win32Exception(Marshal.GetLastWin32Error(), "CreateJobObject failed");
      }
      KillOnCloseJob.JOBOBJECT_EXTENDED_LIMIT_INFORMATION jobobject_EXTENDED_LIMIT_INFORMATION = new KillOnCloseJob.JOBOBJECT_EXTENDED_LIMIT_INFORMATION
      {
        BasicLimitInformation = new KillOnCloseJob.JOBOBJECT_BASIC_LIMIT_INFORMATION
        {
          LimitFlags = 8192U
        }
      };
      int num = Marshal.SizeOf<KillOnCloseJob.JOBOBJECT_EXTENDED_LIMIT_INFORMATION>();
      IntPtr intPtr = Marshal.AllocHGlobal(num);
      try
      {
        Marshal.StructureToPtr<KillOnCloseJob.JOBOBJECT_EXTENDED_LIMIT_INFORMATION>(jobobject_EXTENDED_LIMIT_INFORMATION, intPtr, false);
        if (!KillOnCloseJob.SetInformationJobObject(this.handle, 9, intPtr, (uint)num))
        {
          throw new Win32Exception(Marshal.GetLastWin32Error(), "SetInformationJobObject failed");
        }
      }
      finally
      {
        Marshal.FreeHGlobal(intPtr);
      }
    }

    // Token: 0x0600040D RID: 1037 RVA: 0x000EA5CC File Offset: 0x000EA5CC
    [NullableContext(1)]
    public void Assign(Process process)
    {
      if (!KillOnCloseJob.AssignProcessToJobObject(this.handle, process.Handle))
      {
        throw new Win32Exception(Marshal.GetLastWin32Error(), "AssignProcessToJobObject failed");
      }
    }

    // Token: 0x0600040E RID: 1038 RVA: 0x000EA5F4 File Offset: 0x000EA5F4
    public void TerminateAll(uint exitCode = 1U)
    {
      KillOnCloseJob.TerminateJobObject(this.handle, exitCode);
    }

    // Token: 0x0600040F RID: 1039 RVA: 0x000EA604 File Offset: 0x000EA604
    public void Dispose()
    {
      this.handle.Dispose();
    }

    // Token: 0x06000410 RID: 1040
    [NullableContext(1)]
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern KillOnCloseJob.SafeJobHandle CreateJobObjectW(IntPtr lpJobAttributes, [Nullable(2)] string lpName);

    // Token: 0x06000411 RID: 1041
    [NullableContext(1)]
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetInformationJobObject(KillOnCloseJob.SafeJobHandle hJob, int infoClass, IntPtr lpInfo, uint cbInfoLength);

    // Token: 0x06000412 RID: 1042
    [NullableContext(1)]
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool AssignProcessToJobObject(KillOnCloseJob.SafeJobHandle hJob, IntPtr hProcess);

    // Token: 0x06000413 RID: 1043
    [NullableContext(1)]
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool TerminateJobObject(KillOnCloseJob.SafeJobHandle hJob, uint uExitCode);

    // Token: 0x06000414 RID: 1044
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr hObject);

    // Token: 0x04000180 RID: 384
    [Nullable(1)]
    private readonly KillOnCloseJob.SafeJobHandle handle;

    // Token: 0x04000181 RID: 385
    [Nullable(new byte[] { 1, 2 })]
    private static readonly Lazy<KillOnCloseJob> SharedJob = new Lazy<KillOnCloseJob>(delegate
    {
      KillOnCloseJob killOnCloseJob;
      try
      {
        killOnCloseJob = new KillOnCloseJob(null);
      }
      catch (Exception ex)
      {
        Log.Error("Agents", "could not create kill-on-close job object", ex);
        killOnCloseJob = null;
      }
      return killOnCloseJob;
    });

    // Token: 0x04000182 RID: 386
    private const int JobObjectExtendedLimitInformation = 9;

    // Token: 0x04000183 RID: 387
    private const uint JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE = 8192U;

    // Token: 0x0200010D RID: 269
    private struct JOBOBJECT_BASIC_LIMIT_INFORMATION
    {
      // Token: 0x040005F7 RID: 1527
      public long PerProcessUserTimeLimit;

      // Token: 0x040005F8 RID: 1528
      public long PerJobUserTimeLimit;

      // Token: 0x040005F9 RID: 1529
      public uint LimitFlags;

      // Token: 0x040005FA RID: 1530
      public UIntPtr MinimumWorkingSetSize;

      // Token: 0x040005FB RID: 1531
      public UIntPtr MaximumWorkingSetSize;

      // Token: 0x040005FC RID: 1532
      public uint ActiveProcessLimit;

      // Token: 0x040005FD RID: 1533
      public UIntPtr Affinity;

      // Token: 0x040005FE RID: 1534
      public uint PriorityClass;

      // Token: 0x040005FF RID: 1535
      public uint SchedulingClass;
    }

    // Token: 0x0200010E RID: 270
    private struct IO_COUNTERS
    {
      // Token: 0x04000600 RID: 1536
      public ulong ReadOperationCount;

      // Token: 0x04000601 RID: 1537
      public ulong WriteOperationCount;

      // Token: 0x04000602 RID: 1538
      public ulong OtherOperationCount;

      // Token: 0x04000603 RID: 1539
      public ulong ReadTransferCount;

      // Token: 0x04000604 RID: 1540
      public ulong WriteTransferCount;

      // Token: 0x04000605 RID: 1541
      public ulong OtherTransferCount;
    }

    // Token: 0x0200010F RID: 271
    private struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
    {
      // Token: 0x04000606 RID: 1542
      public KillOnCloseJob.JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;

      // Token: 0x04000607 RID: 1543
      public KillOnCloseJob.IO_COUNTERS IoInfo;

      // Token: 0x04000608 RID: 1544
      public UIntPtr ProcessMemoryLimit;

      // Token: 0x04000609 RID: 1545
      public UIntPtr JobMemoryLimit;

      // Token: 0x0400060A RID: 1546
      public UIntPtr PeakProcessMemoryUsed;

      // Token: 0x0400060B RID: 1547
      public UIntPtr PeakJobMemoryUsed;
    }

    // Token: 0x02000110 RID: 272
    private sealed class SafeJobHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
      // Token: 0x06000BB7 RID: 2999 RVA: 0x00110398 File Offset: 0x00110398
      public SafeJobHandle()
        : base(true)
      {
      }

      // Token: 0x06000BB8 RID: 3000 RVA: 0x001103A4 File Offset: 0x001103A4
      protected override bool ReleaseHandle()
      {
        return KillOnCloseJob.CloseHandle(this.handle);
      }
    }
  }
}
