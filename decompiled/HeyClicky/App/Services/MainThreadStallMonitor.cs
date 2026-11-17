using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using HeyClicky.Core.Telemetry;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.Services
{
  // Token: 0x020000E5 RID: 229
  [NullableContext(2)]
  [Nullable(0)]
  public static class MainThreadStallMonitor
  {
    // Token: 0x06000B41 RID: 2881
    [DllImport("kernel32.dll")]
    private static extern bool QueryUnbiasedInterruptTime(out long unbiasedTime);

    // Token: 0x06000B42 RID: 2882 RVA: 0x0055AB40 File Offset: 0x0055AB40
    private static long UnbiasedNow()
    {
      long num;
      if (!MainThreadStallMonitor.QueryUnbiasedInterruptTime(out num))
      {
        return Stopwatch.GetTimestamp() * 10000000L / Stopwatch.Frequency;
      }
      return num;
    }

    // Token: 0x06000B43 RID: 2883 RVA: 0x0055AB6C File Offset: 0x0055AB6C
    private static int MillisecondsSince(long unbiasedTicks)
    {
      return (int)Math.Min(2147483647L, (MainThreadStallMonitor.UnbiasedNow() - unbiasedTicks) / 10000L);
    }

    // Token: 0x06000B44 RID: 2884 RVA: 0x0055AB88 File Offset: 0x0055AB88
    private static void ReportHang(int milliseconds, bool ended, MainThreadStallMonitor.StallWork work)
    {
      if (Debugger.IsAttached)
      {
        return;
      }
      string text = ((!ended) ? "unended" : ((milliseconds < 5000) ? "2-5s" : ((milliseconds < 15000) ? "5-15s" : "15s+")));
      string text2 = ((work == null) ? "unknown" : UiWork.Describe(work.Work));
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      string text3;
      if (((work != null) ? work.Work : null) != null)
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 1);
        defaultInterpolatedStringHandler.AppendLiteral(", running ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(work.RunningMs);
        defaultInterpolatedStringHandler.AppendLiteral(" ms at the 1 s mark");
        text3 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text3 = "";
      }
      string text4 = text3;
      string text5;
      if (work != null)
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 1);
        defaultInterpolatedStringHandler.AppendLiteral(", ");
        defaultInterpolatedStringHandler.AppendFormatted<long>(work.CallbacksSincePing);
        defaultInterpolatedStringHandler.AppendLiteral(" UI callbacks started during the stall");
        text5 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text5 = "";
      }
      string text6 = text5;
      string text7 = "main_thread";
      string text8 = "app_hanging";
      Exception ex = null;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(52, 7);
      defaultInterpolatedStringHandler.AppendLiteral("UI thread blocked ");
      defaultInterpolatedStringHandler.AppendFormatted(ended ? "" : "for at least ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(milliseconds);
      defaultInterpolatedStringHandler.AppendLiteral(" ms (");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("), ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(Program.MillisecondsSinceProcessStart() / 1000.0, "0");
      defaultInterpolatedStringHandler.AppendLiteral(" s after launch; UI work: ");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendFormatted(text4);
      defaultInterpolatedStringHandler.AppendFormatted(text6);
      string text9 = defaultInterpolatedStringHandler.ToStringAndClear();
      CrashReportLevel crashReportLevel = CrashReportLevel.Warning;
      string text10 = text2;
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["hang_duration"] = text;
      dictionary["ui_work"] = text2;
      ClickyAnalytics.ReportInternalError(text7, text8, ex, text9, crashReportLevel, text10, dictionary);
    }

    // Token: 0x06000B45 RID: 2885 RVA: 0x0055AD38 File Offset: 0x0055AD38
    [NullableContext(1)]
    public static void Start(DispatcherQueue uiQueue)
    {
      if (MainThreadStallMonitor.thread != null)
      {
        return;
      }
      MainThreadStallMonitor.queue = uiQueue;
      ThreadStart threadStart;
      if ((threadStart = MainThreadStallMonitor.<>O.<0>__Run) == null)
      {
        threadStart = (MainThreadStallMonitor.<>O.<0>__Run = new ThreadStart(MainThreadStallMonitor.Run));
      }
      MainThreadStallMonitor.thread = new Thread(threadStart)
      {
        IsBackground = true,
        Name = "HeyClicky UI stall watchdog",
        Priority = ThreadPriority.BelowNormal
      };
      MainThreadStallMonitor.thread.Start();
      string text = "stall";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(66, 2);
      defaultInterpolatedStringHandler.AppendLiteral("UI-thread stall watchdog armed (threshold ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(100);
      defaultInterpolatedStringHandler.AppendLiteral(" ms, hang report at ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(2000);
      defaultInterpolatedStringHandler.AppendLiteral(" ms)");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x06000B46 RID: 2886 RVA: 0x0055ADF0 File Offset: 0x0055ADF0
    private static void Run()
    {
      for (;;)
      {
        Thread.Sleep(50);
        try
        {
          MainThreadStallMonitor.Tick();
        }
        catch (Exception ex)
        {
          Log.Warn("stall", "watchdog tick: " + ex.Message);
        }
      }
    }

    // Token: 0x06000B47 RID: 2887 RVA: 0x0055AE38 File Offset: 0x0055AE38
    private static void Tick()
    {
      long num = Interlocked.Read(ref MainThreadStallMonitor.outstandingSentTicks);
      if (num != 0L)
      {
        int num2 = MainThreadStallMonitor.MillisecondsSince(num);
        if (num2 >= 1000 && !MainThreadStallMonitor.reportedOutstanding)
        {
          MainThreadStallMonitor.reportedOutstanding = true;
          ValueTuple<object, int> valueTuple = UiWork.Snapshot();
          object item = valueTuple.Item1;
          int item2 = valueTuple.Item2;
          MainThreadStallMonitor.StallWork stallWork = new MainThreadStallMonitor.StallWork(item, item2, UiWork.Started - Interlocked.Read(ref MainThreadStallMonitor.callbacksAtPing));
          Volatile.Write<MainThreadStallMonitor.StallWork>(ref MainThreadStallMonitor.stallWork, stallWork);
          string text = "stall";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(104, 5);
          defaultInterpolatedStringHandler.AppendLiteral("[stall] UI thread unresponsive for ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
          defaultInterpolatedStringHandler.AppendLiteral(" ms and counting (ping sent ");
          defaultInterpolatedStringHandler.AppendFormatted(MainThreadStallMonitor.Stamp(DateTime.Now.AddMilliseconds((double)(-(double)num2))));
          defaultInterpolatedStringHandler.AppendLiteral("; ");
          defaultInterpolatedStringHandler.AppendLiteral("running ");
          defaultInterpolatedStringHandler.AppendFormatted(UiWork.Describe(item));
          string text2;
          if (item != null)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(8, 1);
            defaultInterpolatedStringHandler2.AppendLiteral(" for ");
            defaultInterpolatedStringHandler2.AppendFormatted<int>(item2);
            defaultInterpolatedStringHandler2.AppendLiteral(" ms");
            text2 = defaultInterpolatedStringHandler2.ToStringAndClear();
          }
          else
          {
            text2 = "";
          }
          defaultInterpolatedStringHandler.AppendFormatted(text2);
          defaultInterpolatedStringHandler.AppendLiteral(", ");
          defaultInterpolatedStringHandler.AppendFormatted<long>(stallWork.CallbacksSincePing);
          defaultInterpolatedStringHandler.AppendLiteral(" UI callbacks since the ping)");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        }
        if (num2 >= 15000 && !Volatile.Read(ref MainThreadStallMonitor.reportedHang))
        {
          Volatile.Write(ref MainThreadStallMonitor.reportedHang, true);
          MainThreadStallMonitor.ReportHang(num2, false, Volatile.Read<MainThreadStallMonitor.StallWork>(ref MainThreadStallMonitor.stallWork));
        }
        return;
      }
      long sentAt = MainThreadStallMonitor.UnbiasedNow();
      DateTime sentWall = DateTime.Now;
      Interlocked.Exchange(ref MainThreadStallMonitor.outstandingSentTicks, sentAt);
      MainThreadStallMonitor.reportedOutstanding = false;
      Volatile.Write(ref MainThreadStallMonitor.reportedHang, false);
      Volatile.Write<MainThreadStallMonitor.StallWork>(ref MainThreadStallMonitor.stallWork, null);
      Interlocked.Exchange(ref MainThreadStallMonitor.callbacksAtPing, UiWork.Started);
      DispatcherQueue dispatcherQueue = MainThreadStallMonitor.queue;
      if (dispatcherQueue == null || !dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, delegate
      {
        int num3 = MainThreadStallMonitor.MillisecondsSince(sentAt);
        MainThreadStallMonitor.StallWork stallWork2 = Volatile.Read<MainThreadStallMonitor.StallWork>(ref MainThreadStallMonitor.stallWork);
        Interlocked.Exchange(ref MainThreadStallMonitor.outstandingSentTicks, 0L);
        if (num3 < 100)
        {
          return;
        }
        if (num3 >= 2000 && !Volatile.Read(ref MainThreadStallMonitor.reportedHang))
        {
          Volatile.Write(ref MainThreadStallMonitor.reportedHang, true);
          MainThreadStallMonitor.ReportHang(num3, true, stallWork2);
        }
        Interlocked.Increment(ref MainThreadStallMonitor.StallCount);
        if (num3 > MainThreadStallMonitor.LongestStallMs)
        {
          MainThreadStallMonitor.LongestStallMs = num3;
        }
        if (num3 < 500)
        {
          return;
        }
        string text3 = "stall";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(35, 3);
        defaultInterpolatedStringHandler3.AppendLiteral("[stall] UI thread blocked ");
        defaultInterpolatedStringHandler3.AppendFormatted<int>(num3);
        defaultInterpolatedStringHandler3.AppendLiteral(" ms (");
        defaultInterpolatedStringHandler3.AppendFormatted(MainThreadStallMonitor.Stamp(sentWall));
        defaultInterpolatedStringHandler3.AppendLiteral(" → ");
        defaultInterpolatedStringHandler3.AppendFormatted(MainThreadStallMonitor.Stamp(DateTime.Now));
        defaultInterpolatedStringHandler3.AppendLiteral(")");
        Log.Info(text3, defaultInterpolatedStringHandler3.ToStringAndClear());
      }))
      {
        Interlocked.Exchange(ref MainThreadStallMonitor.outstandingSentTicks, 0L);
      }
    }

    // Token: 0x06000B48 RID: 2888 RVA: 0x0055B050 File Offset: 0x0055B050
    [NullableContext(1)]
    private static string Stamp(DateTime time)
    {
      return time.ToString("HH:mm:ss.fff");
    }

    // Token: 0x04000402 RID: 1026
    private const int PingIntervalMs = 50;

    // Token: 0x04000403 RID: 1027
    private const int StallThresholdMs = 100;

    // Token: 0x04000404 RID: 1028
    private const int InProgressReportMs = 1000;

    // Token: 0x04000405 RID: 1029
    private const int HangReportMs = 2000;

    // Token: 0x04000406 RID: 1030
    private const int UnendedHangReportMs = 15000;

    // Token: 0x04000407 RID: 1031
    private const int AlwaysLoggedMs = 500;

    // Token: 0x04000408 RID: 1032
    private static Thread thread;

    // Token: 0x04000409 RID: 1033
    private static DispatcherQueue queue;

    // Token: 0x0400040A RID: 1034
    private static long outstandingSentTicks;

    // Token: 0x0400040B RID: 1035
    private static bool reportedOutstanding;

    // Token: 0x0400040C RID: 1036
    private static bool reportedHang;

    // Token: 0x0400040D RID: 1037
    private static MainThreadStallMonitor.StallWork stallWork;

    // Token: 0x0400040E RID: 1038
    private static long callbacksAtPing;

    // Token: 0x0400040F RID: 1039
    public static int StallCount;

    // Token: 0x04000410 RID: 1040
    public static int LongestStallMs;

    // Token: 0x02000569 RID: 1385
    [Nullable(0)]
    private sealed class StallWork : IEquatable<MainThreadStallMonitor.StallWork>
    {
      // Token: 0x06002E22 RID: 11810 RVA: 0x0065F378 File Offset: 0x0065F378
      public StallWork(object Work, int RunningMs, long CallbacksSincePing)
      {
        this.Work = Work;
        this.RunningMs = RunningMs;
        this.CallbacksSincePing = CallbacksSincePing;
        base..ctor();
      }

      // Token: 0x17000741 RID: 1857
      // (get) Token: 0x06002E23 RID: 11811 RVA: 0x0065F398 File Offset: 0x0065F398
      [Nullable(1)]
      [CompilerGenerated]
      private Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(MainThreadStallMonitor.StallWork);
        }
      }

      // Token: 0x17000742 RID: 1858
      // (get) Token: 0x06002E24 RID: 11812 RVA: 0x0065F3A4 File Offset: 0x0065F3A4
      // (set) Token: 0x06002E25 RID: 11813 RVA: 0x0065F3AC File Offset: 0x0065F3AC
      public object Work { get; set; }

      // Token: 0x17000743 RID: 1859
      // (get) Token: 0x06002E26 RID: 11814 RVA: 0x0065F3B8 File Offset: 0x0065F3B8
      // (set) Token: 0x06002E27 RID: 11815 RVA: 0x0065F3C0 File Offset: 0x0065F3C0
      public int RunningMs { get; set; }

      // Token: 0x17000744 RID: 1860
      // (get) Token: 0x06002E28 RID: 11816 RVA: 0x0065F3CC File Offset: 0x0065F3CC
      // (set) Token: 0x06002E29 RID: 11817 RVA: 0x0065F3D4 File Offset: 0x0065F3D4
      public long CallbacksSincePing { get; set; }

      // Token: 0x06002E2A RID: 11818 RVA: 0x0065F3E0 File Offset: 0x0065F3E0
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("StallWork");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002E2B RID: 11819 RVA: 0x0065F42C File Offset: 0x0065F42C
      [NullableContext(1)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Work = ");
        builder.Append(this.Work);
        builder.Append(", RunningMs = ");
        builder.Append(this.RunningMs.ToString());
        builder.Append(", CallbacksSincePing = ");
        builder.Append(this.CallbacksSincePing.ToString());
        return true;
      }

      // Token: 0x06002E2C RID: 11820 RVA: 0x0065F4A8 File Offset: 0x0065F4A8
      [CompilerGenerated]
      public static bool operator !=(MainThreadStallMonitor.StallWork left, MainThreadStallMonitor.StallWork right)
      {
        return !(left == right);
      }

      // Token: 0x06002E2D RID: 11821 RVA: 0x0065F4B4 File Offset: 0x0065F4B4
      [CompilerGenerated]
      public static bool operator ==(MainThreadStallMonitor.StallWork left, MainThreadStallMonitor.StallWork right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002E2E RID: 11822 RVA: 0x0065F4C8 File Offset: 0x0065F4C8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<object>.Default.GetHashCode(this.<Work>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<RunningMs>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<CallbacksSincePing>k__BackingField);
      }

      // Token: 0x06002E2F RID: 11823 RVA: 0x0065F52C File Offset: 0x0065F52C
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MainThreadStallMonitor.StallWork);
      }

      // Token: 0x06002E30 RID: 11824 RVA: 0x0065F53C File Offset: 0x0065F53C
      [CompilerGenerated]
      public bool Equals(MainThreadStallMonitor.StallWork other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<object>.Default.Equals(this.<Work>k__BackingField, other.<Work>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<RunningMs>k__BackingField, other.<RunningMs>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<CallbacksSincePing>k__BackingField, other.<CallbacksSincePing>k__BackingField));
      }

      // Token: 0x06002E32 RID: 11826 RVA: 0x0065F5B8 File Offset: 0x0065F5B8
      [CompilerGenerated]
      private StallWork([Nullable(1)] MainThreadStallMonitor.StallWork original)
      {
        this.Work = original.<Work>k__BackingField;
        this.RunningMs = original.<RunningMs>k__BackingField;
        this.CallbacksSincePing = original.<CallbacksSincePing>k__BackingField;
      }

      // Token: 0x06002E33 RID: 11827 RVA: 0x0065F5E4 File Offset: 0x0065F5E4
      [CompilerGenerated]
      public void Deconstruct(out object Work, out int RunningMs, out long CallbacksSincePing)
      {
        Work = this.Work;
        RunningMs = this.RunningMs;
        CallbacksSincePing = this.CallbacksSincePing;
      }
    }

    // Token: 0x0200056A RID: 1386
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001988 RID: 6536
      [Nullable(0)]
      public static ThreadStart <0>__Run;
    }
  }
}
