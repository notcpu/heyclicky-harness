using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x02000034 RID: 52
  public readonly struct MemorySample : IEquatable<MemorySample>
  {
    // Token: 0x06000282 RID: 642 RVA: 0x00021068 File Offset: 0x00021068
    public MemorySample(long PrivateBytes, long WorkingSet, long GcHeap, long GcCommitted, long GpuBytes, int Handles, int GdiObjects, int UserObjects, int Threads)
    {
      this.PrivateBytes = PrivateBytes;
      this.WorkingSet = WorkingSet;
      this.GcHeap = GcHeap;
      this.GcCommitted = GcCommitted;
      this.GpuBytes = GpuBytes;
      this.Handles = Handles;
      this.GdiObjects = GdiObjects;
      this.UserObjects = UserObjects;
      this.Threads = Threads;
    }

    // Token: 0x170000A3 RID: 163
    // (get) Token: 0x06000283 RID: 643 RVA: 0x000210BC File Offset: 0x000210BC
    // (set) Token: 0x06000284 RID: 644 RVA: 0x000210C4 File Offset: 0x000210C4
    public long PrivateBytes { get; set; }

    // Token: 0x170000A4 RID: 164
    // (get) Token: 0x06000285 RID: 645 RVA: 0x000210D0 File Offset: 0x000210D0
    // (set) Token: 0x06000286 RID: 646 RVA: 0x000210D8 File Offset: 0x000210D8
    public long WorkingSet { get; set; }

    // Token: 0x170000A5 RID: 165
    // (get) Token: 0x06000287 RID: 647 RVA: 0x000210E4 File Offset: 0x000210E4
    // (set) Token: 0x06000288 RID: 648 RVA: 0x000210EC File Offset: 0x000210EC
    public long GcHeap { get; set; }

    // Token: 0x170000A6 RID: 166
    // (get) Token: 0x06000289 RID: 649 RVA: 0x000210F8 File Offset: 0x000210F8
    // (set) Token: 0x0600028A RID: 650 RVA: 0x00021100 File Offset: 0x00021100
    public long GcCommitted { get; set; }

    // Token: 0x170000A7 RID: 167
    // (get) Token: 0x0600028B RID: 651 RVA: 0x0002110C File Offset: 0x0002110C
    // (set) Token: 0x0600028C RID: 652 RVA: 0x00021114 File Offset: 0x00021114
    public long GpuBytes { get; set; }

    // Token: 0x170000A8 RID: 168
    // (get) Token: 0x0600028D RID: 653 RVA: 0x00021120 File Offset: 0x00021120
    // (set) Token: 0x0600028E RID: 654 RVA: 0x00021128 File Offset: 0x00021128
    public int Handles { get; set; }

    // Token: 0x170000A9 RID: 169
    // (get) Token: 0x0600028F RID: 655 RVA: 0x00021134 File Offset: 0x00021134
    // (set) Token: 0x06000290 RID: 656 RVA: 0x0002113C File Offset: 0x0002113C
    public int GdiObjects { get; set; }

    // Token: 0x170000AA RID: 170
    // (get) Token: 0x06000291 RID: 657 RVA: 0x00021148 File Offset: 0x00021148
    // (set) Token: 0x06000292 RID: 658 RVA: 0x00021150 File Offset: 0x00021150
    public int UserObjects { get; set; }

    // Token: 0x170000AB RID: 171
    // (get) Token: 0x06000293 RID: 659 RVA: 0x0002115C File Offset: 0x0002115C
    // (set) Token: 0x06000294 RID: 660 RVA: 0x00021164 File Offset: 0x00021164
    public int Threads { get; set; }

    // Token: 0x170000AC RID: 172
    // (get) Token: 0x06000295 RID: 661 RVA: 0x00021170 File Offset: 0x00021170
    // (set) Token: 0x06000296 RID: 662 RVA: 0x00021178 File Offset: 0x00021178
    [Nullable(2)]
    public static Func<long> GpuMemory
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000297 RID: 663 RVA: 0x00021180 File Offset: 0x00021180
    public static MemorySample Take()
    {
      MemorySample memorySample;
      using (Process currentProcess = Process.GetCurrentProcess())
      {
        GCMemoryInfo gcmemoryInfo = GC.GetGCMemoryInfo();
        long num = -1L;
        try
        {
          Func<long> gpuMemory = MemorySample.GpuMemory;
          num = ((gpuMemory != null) ? gpuMemory() : (-1L));
        }
        catch
        {
        }
        memorySample = new MemorySample(currentProcess.PrivateMemorySize64, currentProcess.WorkingSet64, GC.GetTotalMemory(false), gcmemoryInfo.TotalCommittedBytes, num, currentProcess.HandleCount, (int)MemorySample.GetGuiResources(currentProcess.Handle, 0U), (int)MemorySample.GetGuiResources(currentProcess.Handle, 1U), currentProcess.Threads.Count);
      }
      return memorySample;
    }

    // Token: 0x06000298 RID: 664 RVA: 0x00021228 File Offset: 0x00021228
    [NullableContext(1)]
    private static string Mb(long bytes)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 1);
      defaultInterpolatedStringHandler.AppendFormatted<double>((double)bytes / 1048576.0, "F0");
      defaultInterpolatedStringHandler.AppendLiteral(" MB");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06000299 RID: 665 RVA: 0x0002126C File Offset: 0x0002126C
    [NullableContext(1)]
    public override string ToString()
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(84, 9);
      defaultInterpolatedStringHandler.AppendLiteral("private ");
      defaultInterpolatedStringHandler.AppendFormatted(MemorySample.Mb(this.PrivateBytes));
      defaultInterpolatedStringHandler.AppendLiteral(", working set ");
      defaultInterpolatedStringHandler.AppendFormatted(MemorySample.Mb(this.WorkingSet));
      defaultInterpolatedStringHandler.AppendLiteral(", GC heap ");
      defaultInterpolatedStringHandler.AppendFormatted(MemorySample.Mb(this.GcHeap));
      defaultInterpolatedStringHandler.AppendLiteral(" (committed ");
      defaultInterpolatedStringHandler.AppendFormatted(MemorySample.Mb(this.GcCommitted));
      defaultInterpolatedStringHandler.AppendLiteral("), ");
      defaultInterpolatedStringHandler.AppendLiteral("GPU ");
      defaultInterpolatedStringHandler.AppendFormatted((this.GpuBytes >= 0L) ? MemorySample.Mb(this.GpuBytes) : "none");
      defaultInterpolatedStringHandler.AppendLiteral(", handles ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(this.Handles);
      defaultInterpolatedStringHandler.AppendLiteral(", GDI ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(this.GdiObjects);
      defaultInterpolatedStringHandler.AppendLiteral(", USER ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(this.UserObjects);
      defaultInterpolatedStringHandler.AppendLiteral(", threads ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(this.Threads);
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x0600029A RID: 666
    [DllImport("user32.dll")]
    private static extern uint GetGuiResources(IntPtr process, uint flags);

    // Token: 0x0600029B RID: 667 RVA: 0x000213A4 File Offset: 0x000213A4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("PrivateBytes = ");
      builder.Append(this.PrivateBytes.ToString());
      builder.Append(", WorkingSet = ");
      builder.Append(this.WorkingSet.ToString());
      builder.Append(", GcHeap = ");
      builder.Append(this.GcHeap.ToString());
      builder.Append(", GcCommitted = ");
      builder.Append(this.GcCommitted.ToString());
      builder.Append(", GpuBytes = ");
      builder.Append(this.GpuBytes.ToString());
      builder.Append(", Handles = ");
      builder.Append(this.Handles.ToString());
      builder.Append(", GdiObjects = ");
      builder.Append(this.GdiObjects.ToString());
      builder.Append(", UserObjects = ");
      builder.Append(this.UserObjects.ToString());
      builder.Append(", Threads = ");
      builder.Append(this.Threads.ToString());
      return true;
    }

    // Token: 0x0600029C RID: 668 RVA: 0x00021514 File Offset: 0x00021514
    [CompilerGenerated]
    public static bool operator !=(MemorySample left, MemorySample right)
    {
      return !(left == right);
    }

    // Token: 0x0600029D RID: 669 RVA: 0x00021520 File Offset: 0x00021520
    [CompilerGenerated]
    public static bool operator ==(MemorySample left, MemorySample right)
    {
      return left.Equals(right);
    }

    // Token: 0x0600029E RID: 670 RVA: 0x0002152C File Offset: 0x0002152C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((EqualityComparer<long>.Default.GetHashCode(this.<PrivateBytes>k__BackingField) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<WorkingSet>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<GcHeap>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<GcCommitted>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<GpuBytes>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Handles>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<GdiObjects>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<UserObjects>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Threads>k__BackingField);
    }

    // Token: 0x0600029F RID: 671 RVA: 0x00021604 File Offset: 0x00021604
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is MemorySample && this.Equals((MemorySample)obj);
    }

    // Token: 0x060002A0 RID: 672 RVA: 0x0002161C File Offset: 0x0002161C
    [CompilerGenerated]
    public bool Equals(MemorySample other)
    {
      return EqualityComparer<long>.Default.Equals(this.<PrivateBytes>k__BackingField, other.<PrivateBytes>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<WorkingSet>k__BackingField, other.<WorkingSet>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<GcHeap>k__BackingField, other.<GcHeap>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<GcCommitted>k__BackingField, other.<GcCommitted>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<GpuBytes>k__BackingField, other.<GpuBytes>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Handles>k__BackingField, other.<Handles>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<GdiObjects>k__BackingField, other.<GdiObjects>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<UserObjects>k__BackingField, other.<UserObjects>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Threads>k__BackingField, other.<Threads>k__BackingField);
    }

    // Token: 0x060002A1 RID: 673 RVA: 0x0002170C File Offset: 0x0002170C
    [CompilerGenerated]
    public void Deconstruct(out long PrivateBytes, out long WorkingSet, out long GcHeap, out long GcCommitted, out long GpuBytes, out int Handles, out int GdiObjects, out int UserObjects, out int Threads)
    {
      PrivateBytes = this.PrivateBytes;
      WorkingSet = this.WorkingSet;
      GcHeap = this.GcHeap;
      GcCommitted = this.GcCommitted;
      GpuBytes = this.GpuBytes;
      Handles = this.Handles;
      GdiObjects = this.GdiObjects;
      UserObjects = this.UserObjects;
      Threads = this.Threads;
    }
  }
}
