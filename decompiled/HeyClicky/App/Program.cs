using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using HeyClicky.App.Services;
using HeyClicky.App.Services.Updates;
using HeyClicky.App.Shell;
using HeyClicky.Platform.Win32;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using WinRT;

namespace HeyClicky.App
{
  // Token: 0x02000081 RID: 129
  [NullableContext(1)]
  [Nullable(0)]
  public static class Program
  {
    // Token: 0x170000A6 RID: 166
    // (get) Token: 0x060004F0 RID: 1264 RVA: 0x0052BCF0 File Offset: 0x0052BCF0
    // (set) Token: 0x060004F1 RID: 1265 RVA: 0x0052BCF8 File Offset: 0x0052BCF8
    public static string[] Args { get; private set; } = Array.Empty<string>();

    // Token: 0x14000004 RID: 4
    // (add) Token: 0x060004F2 RID: 1266 RVA: 0x0052BD00 File Offset: 0x0052BD00
    // (remove) Token: 0x060004F3 RID: 1267 RVA: 0x0052BD34 File Offset: 0x0052BD34
    [Nullable(new byte[] { 2, 1, 1 })]
    [field: Nullable(new byte[] { 2, 1, 1 })]
    public static event Action<string[]> ArgumentsForwarded;

    // Token: 0x060004F4 RID: 1268 RVA: 0x0052BD68 File Offset: 0x0052BD68
    [STAThread]
    private static int Main(string[] args)
    {
      try
      {
        UpdateService.ConfigureVelopack(args).Run();
      }
      catch (Exception ex)
      {
        Log.Warn("updates", "Velopack startup: " + ex.Message);
      }
      Program.Args = args;
      string environmentVariable = Environment.GetEnvironmentVariable("HEYCLICKY_TEST_CULTURE");
      if (environmentVariable != null && environmentVariable.Length > 0)
      {
        CultureInfo.CurrentCulture = (CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo(environmentVariable));
      }
      bool flag = Environment.GetEnvironmentVariable("HEYCLICKY_OFFSCREEN") == "1";
      if (!args.Any<string>((string a) => a == "--demo-states" || a == "--demo-only" || a == "--fakes") && (!flag || (!args.Contains("--e2e") && !AppPaths.IsOverridden)) && (!args.Contains("--e2e") || !OverlayWindow.OnscreenBesideReal))
      {
        if (!SingleInstance.TryClaim(args, "HeyClicky", null, null))
        {
          return 0;
        }
        Program.ownsInstance = true;
        SingleInstance.ArgumentsReceived += delegate(string[] forwarded)
        {
          Action<string[]> argumentsForwarded = Program.ArgumentsForwarded;
          if (argumentsForwarded == null)
          {
            return;
          }
          argumentsForwarded(forwarded);
        };
      }
      CrashReporting.Install();
      GpuScheduling.RaiseForProcess();
      ComWrappersSupport.InitializeComWrappers(null);
      Application.Start(delegate(ApplicationInitializationCallbackParams startParams)
      {
        SynchronizationContext.SetSynchronizationContext(new CrashReporting.ReportingSynchronizationContext(DispatcherQueue.GetForCurrentThread()));
        new App();
      });
      if (Program.ownsInstance)
      {
        SingleInstance.Release();
      }
      Log.Info("app", "exited");
      Program.Exit();
      return 0;
    }

    // Token: 0x060004F5 RID: 1269 RVA: 0x0052BEDC File Offset: 0x0052BEDC
    internal static void Exit()
    {
      new Thread(delegate
      {
        Thread.Sleep(TimeSpan.FromSeconds(5L));
        Log.Warn("shutdown", "still exiting 5 s later: terminating");
        Program.Terminate();
      })
      {
        IsBackground = true,
        Name = "ExitWatchdog"
      }.Start();
      Environment.Exit(0);
    }

    // Token: 0x060004F6 RID: 1270 RVA: 0x0052BF2C File Offset: 0x0052BF2C
    internal static void Terminate()
    {
      Program.TerminateProcess(Program.GetCurrentProcess(), 0U);
    }

    // Token: 0x060004F7 RID: 1271
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetCurrentProcess();

    // Token: 0x060004F8 RID: 1272
    [DllImport("kernel32.dll")]
    private static extern bool TerminateProcess(IntPtr process, uint exitCode);

    // Token: 0x060004F9 RID: 1273 RVA: 0x0052BF3C File Offset: 0x0052BF3C
    public static double MillisecondsSinceProcessStart()
    {
      double num;
      try
      {
        num = (DateTime.Now - Process.GetCurrentProcess().StartTime).TotalMilliseconds;
      }
      catch
      {
        num = Program.SinceStart.Elapsed.TotalMilliseconds;
      }
      return num;
    }

    // Token: 0x040000FA RID: 250
    public static readonly Stopwatch SinceStart = Stopwatch.StartNew();

    // Token: 0x040000FD RID: 253
    private static bool ownsInstance;
  }
}
