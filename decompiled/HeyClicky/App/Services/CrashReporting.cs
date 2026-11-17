using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Services.Updates;
using HeyClicky.Core.Configuration;
using HeyClicky.Core.Identity;
using HeyClicky.Core.Telemetry;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.Win32.SafeHandles;
using Sentry;

namespace HeyClicky.App.Services
{
  // Token: 0x020000E3 RID: 227
  [NullableContext(1)]
  [Nullable(0)]
  public static class CrashReporting
  {
    // Token: 0x170001DE RID: 478
    // (get) Token: 0x06000B09 RID: 2825 RVA: 0x00559AEC File Offset: 0x00559AEC
    public static string LogsDirectory
    {
      get
      {
        return Log.Directory;
      }
    }

    // Token: 0x170001DF RID: 479
    // (get) Token: 0x06000B0A RID: 2826 RVA: 0x00559AF4 File Offset: 0x00559AF4
    // (set) Token: 0x06000B0B RID: 2827 RVA: 0x00559AFC File Offset: 0x00559AFC
    public static bool SentryEnabled { get; private set; }

    // Token: 0x06000B0C RID: 2828 RVA: 0x00559B04 File Offset: 0x00559B04
    public static void Install()
    {
      if (CrashReporting.installed)
      {
        return;
      }
      CrashReporting.installed = true;
      AppDomain currentDomain = AppDomain.CurrentDomain;
      UnhandledExceptionEventHandler unhandledExceptionEventHandler;
      if ((unhandledExceptionEventHandler = CrashReporting.<>O.<0>__OnAppDomainUnhandledException) == null)
      {
        unhandledExceptionEventHandler = (CrashReporting.<>O.<0>__OnAppDomainUnhandledException = new UnhandledExceptionEventHandler(CrashReporting.OnAppDomainUnhandledException));
      }
      currentDomain.UnhandledException += unhandledExceptionEventHandler;
      EventHandler<UnobservedTaskExceptionEventArgs> eventHandler;
      if ((eventHandler = CrashReporting.<>O.<1>__OnUnobservedTaskException) == null)
      {
        eventHandler = (CrashReporting.<>O.<1>__OnUnobservedTaskException = new EventHandler<UnobservedTaskExceptionEventArgs>(CrashReporting.OnUnobservedTaskException));
      }
      TaskScheduler.UnobservedTaskException += eventHandler;
      CrashReporting.InstallNativeFilter();
      CrashReporting.StartSentry();
      CrashReporting.PruneOldDumps();
    }

    // Token: 0x06000B0D RID: 2829 RVA: 0x00559B74 File Offset: 0x00559B74
    private static void StartSentry()
    {
      TelemetryConfiguration config = TelemetryConfiguration.Current;
      ClickyEndpoints clickyEndpoints = AppBundleConfiguration.Load(null);
      string text = null;
      if (text == null && AppBundleConfiguration.IsLocalDevelopmentConfiguration(clickyEndpoints))
      {
        text = "local endpoints";
      }
      if (text == null && !config.HasSentryDsn)
      {
        text = "no DSN configured";
      }
      if (Environment.GetEnvironmentVariable("HEYCLICKY_SENTRY_OFF") == "1")
      {
        text = "HEYCLICKY_SENTRY_OFF=1";
      }
      if (text != null)
      {
        Log.Info("crash", "Sentry off (" + text + "): crash and error reports stay in the logs folder");
        return;
      }
      try
      {
        ClientIdentity identity = ClickyClientIdentity.Current;
        SentrySdk.Init(delegate(SentryOptions options)
        {
          options.Dsn = config.SentryDsn;
          options.Release = "heyclicky-windows@" + identity.AppVersion + "+" + identity.AppBuild;
          options.Environment = "production";
          options.IsGlobalModeEnabled = true;
          options.AutoSessionTracking = true;
          options.SendDefaultPii = true;
          options.AttachStacktrace = true;
          options.CacheDirectoryPath = Path.Combine(AppBundleConfiguration.AppDataDirectory, "sentry");
          options.TracesSampleRate = new double?(0.0);
          options.DisableAppDomainUnhandledExceptionCapture();
          options.DisableUnobservedTaskExceptionCapture();
          options.DisableWinUiUnhandledExceptionIntegration();
        });
        SentrySdk.ConfigureScope(delegate(Scope scope)
        {
          bool flag = UpdateService.IsVelopackInstall();
          scope.SetTag("install_location", flag ? "installed" : "unpackaged");
          scope.SetTag("clicky_platform", identity.Platform);
          scope.SetTag("os_arch", RuntimeInformation.OSArchitecture.ToString());
          scope.SetTag("process_arch", RuntimeInformation.ProcessArchitecture.ToString());
          SentryContexts contexts = scope.Contexts;
          string text2 = "install_location";
          Dictionary<string, object> dictionary = new Dictionary<string, object>();
          dictionary["path"] = AppContext.BaseDirectory;
          dictionary["category"] = (flag ? "installed" : "unpackaged");
          contexts[text2] = dictionary;
        });
        CrashReporter.Install(new CrashReporting.SentryCrashReporter());
        CrashReporting.SentryEnabled = true;
        Log.Info("crash", "Sentry on");
      }
      catch (Exception ex)
      {
        Log.Error("crash", "Sentry failed to start", ex);
      }
    }

    // Token: 0x06000B0E RID: 2830 RVA: 0x00559C70 File Offset: 0x00559C70
    public static void ReportUiException(Exception exception)
    {
      Log.Error("crash", "unhandled UI exception", exception);
      CrashReporting.Capture(exception, "Microsoft.UI.Xaml.UnhandledException", false);
    }

    // Token: 0x06000B0F RID: 2831 RVA: 0x00559C90 File Offset: 0x00559C90
    private static void OnUnobservedTaskException([Nullable(2)] object sender, UnobservedTaskExceptionEventArgs e)
    {
      Log.Error("crash", "unobserved task exception", e.Exception);
      CrashReporting.Capture(e.Exception, "UnobservedTaskException", false);
      e.SetObserved();
    }

    // Token: 0x06000B10 RID: 2832 RVA: 0x00559CC0 File Offset: 0x00559CC0
    private static void OnAppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      Exception ex = e.ExceptionObject as Exception;
      string text = "crash";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 2);
      defaultInterpolatedStringHandler.AppendLiteral("unhandled exception on thread ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(Environment.CurrentManagedThreadId);
      defaultInterpolatedStringHandler.AppendLiteral(" (terminating: ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(e.IsTerminating);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Error(text, defaultInterpolatedStringHandler.ToStringAndClear(), ex);
      if (e.IsTerminating)
      {
        string text2 = CrashReporting.WriteMinidump(Marshal.GetExceptionPointers(), (uint)((ex == null) ? 0 : ex.HResult), (ex != null) ? ex.GetType().FullName : null);
        if (text2 != null)
        {
          Log.Error("crash", "minidump written: " + text2, null);
        }
      }
      if (ex != null)
      {
        CrashReporting.Capture(ex, "AppDomain.UnhandledException", false);
      }
      try
      {
        if (CrashReporting.SentryEnabled)
        {
          SentrySdk.Flush(TimeSpan.FromSeconds(2L));
        }
      }
      catch
      {
      }
    }

    // Token: 0x06000B11 RID: 2833 RVA: 0x00559DB4 File Offset: 0x00559DB4
    private static void Capture(Exception exception, string mechanism, bool handled)
    {
      try
      {
        if (CrashReporter.Current.IsEnabled)
        {
          CrashReporter.Current.CaptureException(exception, mechanism, handled);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("crash", "report failed: " + ex.Message);
      }
    }

    // Token: 0x06000B12 RID: 2834
    [DllImport("kernel32.dll")]
    private static extern IntPtr SetUnhandledExceptionFilter(IntPtr filter);

    // Token: 0x06000B13 RID: 2835
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetCurrentProcess();

    // Token: 0x06000B14 RID: 2836
    [DllImport("kernel32.dll")]
    private static extern uint GetCurrentThreadId();

    // Token: 0x06000B15 RID: 2837
    [DllImport("dbghelp.dll", SetLastError = true)]
    private static extern bool MiniDumpWriteDump(IntPtr process, uint processId, SafeFileHandle file, uint dumpType, ref CrashReporting.MinidumpExceptionInformation exceptionParam, IntPtr userStreamParam, IntPtr callbackParam);

    // Token: 0x06000B16 RID: 2838
    [DllImport("dbghelp.dll", EntryPoint = "MiniDumpWriteDump", SetLastError = true)]
    private static extern bool MiniDumpWriteDumpNoException(IntPtr process, uint processId, SafeFileHandle file, uint dumpType, IntPtr exceptionParam, IntPtr userStreamParam, IntPtr callbackParam);

    // Token: 0x06000B17 RID: 2839 RVA: 0x00559E0C File Offset: 0x00559E0C
    private static void InstallNativeFilter()
    {
      try
      {
        CrashReporting.TopLevelExceptionFilter topLevelExceptionFilter;
        if ((topLevelExceptionFilter = CrashReporting.<>O.<2>__OnNativeUnhandledException) == null)
        {
          topLevelExceptionFilter = (CrashReporting.<>O.<2>__OnNativeUnhandledException = new CrashReporting.TopLevelExceptionFilter(CrashReporting.OnNativeUnhandledException));
        }
        CrashReporting.nativeFilter = topLevelExceptionFilter;
        CrashReporting.previousNativeFilter = CrashReporting.SetUnhandledExceptionFilter(Marshal.GetFunctionPointerForDelegate<CrashReporting.TopLevelExceptionFilter>(CrashReporting.nativeFilter));
      }
      catch (Exception ex)
      {
        Log.Warn("crash", "native crash filter not installed: " + ex.Message);
      }
    }

    // Token: 0x06000B18 RID: 2840 RVA: 0x00559E7C File Offset: 0x00559E7C
    private static int OnNativeUnhandledException(IntPtr exceptionPointers)
    {
      try
      {
        uint num = 0U;
        if (exceptionPointers != IntPtr.Zero)
        {
          IntPtr intPtr = Marshal.ReadIntPtr(exceptionPointers);
          if (intPtr != IntPtr.Zero)
          {
            num = (uint)Marshal.ReadInt32(intPtr);
          }
        }
        string text = "crash";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 2);
        defaultInterpolatedStringHandler.AppendLiteral("native crash: unhandled exception 0x");
        defaultInterpolatedStringHandler.AppendFormatted<uint>(num, "X8");
        defaultInterpolatedStringHandler.AppendLiteral(" on thread ");
        defaultInterpolatedStringHandler.AppendFormatted<uint>(CrashReporting.GetCurrentThreadId());
        Log.Error(text, defaultInterpolatedStringHandler.ToStringAndClear(), null);
        string text2 = CrashReporting.WriteMinidump(exceptionPointers, num, "native");
        if (text2 != null)
        {
          Log.Error("crash", "minidump written: " + text2, null);
        }
      }
      catch
      {
      }
      if (CrashReporting.previousNativeFilter != IntPtr.Zero)
      {
        try
        {
          return Marshal.GetDelegateForFunctionPointer<CrashReporting.TopLevelExceptionFilter>(CrashReporting.previousNativeFilter)(exceptionPointers);
        }
        catch
        {
        }
        return 0;
      }
      return 0;
    }

    // Token: 0x06000B19 RID: 2841 RVA: 0x00559F64 File Offset: 0x00559F64
    [NullableContext(2)]
    public static string WriteMinidump(IntPtr exceptionPointers, uint exceptionCode, string kind)
    {
      if (Interlocked.Exchange(ref CrashReporting.dumpWritten, 1) == 1)
      {
        return null;
      }
      string text4;
      try
      {
        Directory.CreateDirectory(CrashReporting.LogsDirectory);
        string logsDirectory = CrashReporting.LogsDirectory;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 2);
        defaultInterpolatedStringHandler.AppendLiteral("crash-");
        defaultInterpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now, "yyyyMMdd-HHmmss");
        defaultInterpolatedStringHandler.AppendLiteral("-");
        defaultInterpolatedStringHandler.AppendFormatted<int>(Environment.ProcessId);
        string text = Path.Combine(logsDirectory, defaultInterpolatedStringHandler.ToStringAndClear());
        string text2 = text + ".dmp";
        bool flag;
        using (FileStream fileStream = File.Create(text2))
        {
          SafeFileHandle safeFileHandle = fileStream.SafeFileHandle;
          if (exceptionPointers != IntPtr.Zero)
          {
            CrashReporting.MinidumpExceptionInformation minidumpExceptionInformation = new CrashReporting.MinidumpExceptionInformation
            {
              ThreadId = CrashReporting.GetCurrentThreadId(),
              ExceptionPointers = exceptionPointers,
              ClientPointers = false
            };
            flag = CrashReporting.MiniDumpWriteDump(CrashReporting.GetCurrentProcess(), (uint)Environment.ProcessId, safeFileHandle, 4452U, ref minidumpExceptionInformation, IntPtr.Zero, IntPtr.Zero);
          }
          else
          {
            flag = CrashReporting.MiniDumpWriteDumpNoException(CrashReporting.GetCurrentProcess(), (uint)Environment.ProcessId, safeFileHandle, 4452U, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
          }
        }
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["time"] = DateTimeOffset.Now.ToString("O");
        jsonObject["pid"] = Environment.ProcessId;
        jsonObject["kind"] = kind;
        string text3 = "exception_code";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 1);
        defaultInterpolatedStringHandler.AppendLiteral("0x");
        defaultInterpolatedStringHandler.AppendFormatted<uint>(exceptionCode, "X8");
        jsonObject[text3] = defaultInterpolatedStringHandler.ToStringAndClear();
        jsonObject["dump"] = (flag ? Path.GetFileName(text2) : null);
        jsonObject["app_version"] = ClickyClientIdentity.Current.AppVersion;
        jsonObject["process"] = Path.GetFileName(Environment.ProcessPath);
        JsonObject jsonObject2 = jsonObject;
        File.WriteAllText(text + ".json", jsonObject2.ToJsonString(null));
        if (!flag)
        {
          try
          {
            File.Delete(text2);
          }
          catch
          {
          }
          text4 = null;
        }
        else
        {
          text4 = text2;
        }
      }
      catch
      {
        text4 = null;
      }
      return text4;
    }

    // Token: 0x06000B1A RID: 2842 RVA: 0x0055A1E8 File Offset: 0x0055A1E8
    public static void ReportPreviousCrashes()
    {
      try
      {
        if (Directory.Exists(CrashReporting.LogsDirectory))
        {
          foreach (string text in Directory.GetFiles(CrashReporting.LogsDirectory, "crash-*.json"))
          {
            CrashReporting.<>c__DisplayClass29_0 CS$<>8__locals1 = new CrashReporting.<>c__DisplayClass29_0();
            if (!text.Contains(".reported", StringComparison.OrdinalIgnoreCase))
            {
              JsonObject jsonObject = null;
              try
              {
                jsonObject = JsonNode.Parse(File.ReadAllText(text), null, default(JsonDocumentOptions)) as JsonObject;
              }
              catch
              {
              }
              int? num;
              if (jsonObject == null)
              {
                num = null;
              }
              else
              {
                JsonNode jsonNode = jsonObject["pid"];
                num = ((jsonNode != null) ? new int?(jsonNode.GetValue<int>()) : null);
              }
              int? num2 = num;
              int processId = Environment.ProcessId;
              if (!((num2.GetValueOrDefault() == processId) & (num2 != null)))
              {
                string text2;
                if (jsonObject == null)
                {
                  text2 = null;
                }
                else
                {
                  JsonNode jsonNode2 = jsonObject["dump"];
                  text2 = ((jsonNode2 != null) ? jsonNode2.GetValue<string>() : null);
                }
                string text3 = text2;
                CS$<>8__locals1.dumpPath = ((text3 == null) ? null : Path.Combine(CrashReporting.LogsDirectory, text3));
                CrashReporting.<>c__DisplayClass29_0 CS$<>8__locals2 = CS$<>8__locals1;
                string text4;
                if (jsonObject == null)
                {
                  text4 = null;
                }
                else
                {
                  JsonNode jsonNode3 = jsonObject["exception_code"];
                  text4 = ((jsonNode3 != null) ? jsonNode3.GetValue<string>() : null);
                }
                CS$<>8__locals2.code = text4 ?? "unknown";
                CrashReporting.<>c__DisplayClass29_0 CS$<>8__locals3 = CS$<>8__locals1;
                string text5;
                if (jsonObject == null)
                {
                  text5 = null;
                }
                else
                {
                  JsonNode jsonNode4 = jsonObject["kind"];
                  text5 = ((jsonNode4 != null) ? jsonNode4.GetValue<string>() : null);
                }
                CS$<>8__locals3.kind = text5 ?? "unknown";
                string text6 = "crash";
                DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(45, 4);
                defaultInterpolatedStringHandler.AppendLiteral("the previous run crashed (");
                defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.kind);
                defaultInterpolatedStringHandler.AppendLiteral(", ");
                defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.code);
                defaultInterpolatedStringHandler.AppendLiteral(") at ");
                defaultInterpolatedStringHandler.AppendFormatted<JsonNode>((jsonObject != null) ? jsonObject["time"] : null);
                defaultInterpolatedStringHandler.AppendLiteral("; minidump: ");
                defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.dumpPath ?? "none");
                Log.Warn(text6, defaultInterpolatedStringHandler.ToStringAndClear());
                if (CrashReporting.SentryEnabled)
                {
                  SentrySdk.CaptureMessage("Crash in a previous run: " + CS$<>8__locals1.kind + " " + CS$<>8__locals1.code, delegate(Scope scope)
                  {
                    scope.Level = new SentryLevel?(SentryLevel.Fatal);
                    scope.SetTag("crash_kind", CS$<>8__locals1.kind);
                    scope.SetTag("exception_code", CS$<>8__locals1.code);
                    scope.SetFingerprint(new string[] { "previous_run_crash", CS$<>8__locals1.kind, CS$<>8__locals1.code });
                    if (CS$<>8__locals1.dumpPath != null && File.Exists(CS$<>8__locals1.dumpPath) && new FileInfo(CS$<>8__locals1.dumpPath).Length < 20000000L)
                    {
                      scope.AddAttachment(CS$<>8__locals1.dumpPath, AttachmentType.Minidump, null);
                    }
                  }, SentryLevel.Fatal);
                }
                File.Move(text, Path.ChangeExtension(text, ".reported"), true);
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("crash", "previous crash reports: " + ex.Message);
      }
    }

    // Token: 0x06000B1B RID: 2843 RVA: 0x0055A494 File Offset: 0x0055A494
    private static void PruneOldDumps()
    {
      try
      {
        if (Directory.Exists(CrashReporting.LogsDirectory))
        {
          foreach (FileInfo fileInfo in (from f in new DirectoryInfo(CrashReporting.LogsDirectory).GetFiles("crash-*.dmp")
            orderby f.LastWriteTimeUtc descending
            select f).Skip<FileInfo>(5))
          {
            try
            {
              fileInfo.Delete();
            }
            catch
            {
            }
          }
        }
      }
      catch
      {
      }
    }

    // Token: 0x06000B1C RID: 2844 RVA: 0x0055A548 File Offset: 0x0055A548
    [Conditional("DEBUG")]
    public static void RunCrashTestIfRequested(DispatcherQueue ui)
    {
      string environmentVariable = Environment.GetEnvironmentVariable("HEYCLICKY_CRASH_TEST");
      if (string.IsNullOrEmpty(environmentVariable))
      {
        return;
      }
      Log.Warn("crash", "crash test: " + environmentVariable);
      if (environmentVariable == "ui")
      {
        DispatcherTimer timer = new DispatcherTimer
        {
          Interval = TimeSpan.FromMilliseconds(200L, 0L)
        };
        timer.Tick += delegate([Nullable(2)] object _, object _)
        {
          timer.Stop();
          throw new InvalidOperationException("HEYCLICKY_CRASH_TEST=ui: a forced UI-thread exception");
        };
        timer.Start();
        return;
      }
      if (environmentVariable == "async")
      {
        ui.TryEnqueue(delegate
        {
          CrashReporting.<>c.<<RunCrashTestIfRequested>b__31_1>d <<RunCrashTestIfRequested>b__31_1>d;
          <<RunCrashTestIfRequested>b__31_1>d.<>t__builder = AsyncVoidMethodBuilder.Create();
          <<RunCrashTestIfRequested>b__31_1>d.<>1__state = -1;
          <<RunCrashTestIfRequested>b__31_1>d.<>t__builder.Start<CrashReporting.<>c.<<RunCrashTestIfRequested>b__31_1>d>(ref <<RunCrashTestIfRequested>b__31_1>d);
        });
        return;
      }
      if (environmentVariable == "task")
      {
        Task.Run(delegate
        {
          throw new InvalidOperationException("HEYCLICKY_CRASH_TEST=task: a forced unobserved task exception");
        });
        Task.Run(async delegate
        {
          await Task.Delay(1000);
          GC.Collect();
          GC.WaitForPendingFinalizers();
          GC.Collect();
        });
        return;
      }
      if (environmentVariable == "thread")
      {
        new Thread(delegate
        {
          throw new InvalidOperationException("HEYCLICKY_CRASH_TEST=thread: a forced unhandled exception on a background thread");
        })
        {
          IsBackground = true
        }.Start();
        return;
      }
      if (environmentVariable == "native")
      {
        CrashReporting.NativeCrash.CrashOnNativeThread();
        return;
      }
      if (!(environmentVariable == "report-error"))
      {
        return;
      }
      ClickyAnalytics.TrackAppError("crash_test", new InvalidOperationException("HEYCLICKY_CRASH_TEST=report-error: a forced user-visible error"), "error, plz restart me. if still broken, send email to hi@heyclicky.com very srry!", "generic");
    }

    // Token: 0x040003F2 RID: 1010
    private const string Area = "crash";

    // Token: 0x040003F3 RID: 1011
    private static int dumpWritten;

    // Token: 0x040003F4 RID: 1012
    [Nullable(2)]
    private static CrashReporting.TopLevelExceptionFilter nativeFilter;

    // Token: 0x040003F5 RID: 1013
    private static IntPtr previousNativeFilter;

    // Token: 0x040003F6 RID: 1014
    private static bool installed;

    // Token: 0x040003F8 RID: 1016
    private const uint DumpType = 4452U;

    // Token: 0x0200055D RID: 1373
    [Nullable(0)]
    private sealed class SentryCrashReporter : ICrashReporter
    {
      // Token: 0x17000731 RID: 1841
      // (get) Token: 0x06002DD7 RID: 11735 RVA: 0x0065DEAC File Offset: 0x0065DEAC
      public bool IsEnabled
      {
        get
        {
          return SentrySdk.IsEnabled;
        }
      }

      // Token: 0x06002DD8 RID: 11736 RVA: 0x0065DEB4 File Offset: 0x0065DEB4
      private static SentryLevel Level(CrashReportLevel level)
      {
        switch (level)
        {
        case CrashReportLevel.Debug:
          return SentryLevel.Debug;
        case CrashReportLevel.Info:
          return SentryLevel.Info;
        case CrashReportLevel.Warning:
          return SentryLevel.Warning;
        case CrashReportLevel.Fatal:
          return SentryLevel.Fatal;
        }
        return SentryLevel.Error;
      }

      // Token: 0x06002DD9 RID: 11737 RVA: 0x0065DEF0 File Offset: 0x0065DEF0
      private static BreadcrumbLevel Crumb(CrashReportLevel level)
      {
        switch (level)
        {
        case CrashReportLevel.Debug:
          return BreadcrumbLevel.Debug;
        case CrashReportLevel.Info:
          return BreadcrumbLevel.Info;
        case CrashReportLevel.Warning:
          return BreadcrumbLevel.Warning;
        case CrashReportLevel.Fatal:
          return BreadcrumbLevel.Fatal;
        }
        return BreadcrumbLevel.Error;
      }

      // Token: 0x06002DDA RID: 11738 RVA: 0x0065DF2C File Offset: 0x0065DF2C
      [NullableContext(2)]
      public void AddBreadcrumb([Nullable(1)] string category, string message, CrashReportLevel level, JsonObject data)
      {
        Dictionary<string, string> dictionary;
        if (data != null)
        {
          dictionary = data.ToDictionary<KeyValuePair<string, JsonNode>, string, string>((KeyValuePair<string, JsonNode> p) => p.Key, delegate(KeyValuePair<string, JsonNode> p)
          {
            JsonNode value = p.Value;
            return ((value != null) ? value.ToString() : null) ?? "";
          });
        }
        else
        {
          dictionary = null;
        }
        Dictionary<string, string> dictionary2 = dictionary;
        SentrySdk.AddBreadcrumb(message ?? "", category, null, dictionary2, CrashReporting.SentryCrashReporter.Crumb(level));
      }

      // Token: 0x06002DDB RID: 11739 RVA: 0x0065DFA0 File Offset: 0x0065DFA0
      public void CaptureMessage(string message, CrashReportLevel level, IReadOnlyList<string> fingerprint, IReadOnlyDictionary<string, string> tags, string contextName, JsonObject context, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> attachmentPaths = null)
      {
        SentrySdk.CaptureMessage(message, delegate(Scope scope)
        {
          scope.Level = new SentryLevel?(CrashReporting.SentryCrashReporter.Level(level));
          foreach (KeyValuePair<string, string> keyValuePair in tags)
          {
            string text;
            string text2;
            keyValuePair.Deconstruct(out text, out text2);
            string text3 = text;
            string text4 = text2;
            scope.SetTag(text3, text4);
          }
          scope.SetFingerprint(fingerprint);
          scope.Contexts[contextName] = context.ToDictionary<KeyValuePair<string, JsonNode>, string, object>((KeyValuePair<string, JsonNode> p) => p.Key, delegate(KeyValuePair<string, JsonNode> p)
          {
            JsonNode value = p.Value;
            if (value == null)
            {
              return null;
            }
            return value.ToString();
          });
          foreach (string text5 in (attachmentPaths ?? Array.Empty<string>()))
          {
            if (File.Exists(text5))
            {
              scope.AddAttachment(text5, AttachmentType.Default, null);
            }
          }
        }, CrashReporting.SentryCrashReporter.Level(level));
      }

      // Token: 0x06002DDC RID: 11740 RVA: 0x0065E000 File Offset: 0x0065E000
      public void CaptureException(Exception exception, string mechanism, bool handled)
      {
        exception.SetSentryMechanism(mechanism, null, new bool?(handled), null);
        SentrySdk.CaptureException(exception);
      }

      // Token: 0x06002DDD RID: 11741 RVA: 0x0065E02C File Offset: 0x0065E02C
      [NullableContext(2)]
      public void SetUser(string id, string email, string name)
      {
        SentrySdk.ConfigureScope(delegate(Scope scope)
        {
          SentryUser sentryUser2;
          if (id != null)
          {
            SentryUser sentryUser = new SentryUser();
            sentryUser.Id = id;
            sentryUser.Email = email;
            sentryUser2 = sentryUser;
            sentryUser.Username = name;
          }
          else
          {
            sentryUser2 = new SentryUser();
          }
          scope.User = sentryUser2;
        });
      }

      // Token: 0x06002DDE RID: 11742 RVA: 0x0065E058 File Offset: 0x0065E058
      public void ClearBreadcrumbs()
      {
        SentrySdk.ConfigureScope(delegate(Scope scope)
        {
          scope.ClearBreadcrumbs();
        });
      }

      // Token: 0x06002DDF RID: 11743 RVA: 0x0065E080 File Offset: 0x0065E080
      public Task FlushAsync(TimeSpan timeout)
      {
        return SentrySdk.FlushAsync(timeout);
      }
    }

    // Token: 0x0200055E RID: 1374
    // (Invoke) Token: 0x06002DE2 RID: 11746
    [NullableContext(0)]
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    private delegate int TopLevelExceptionFilter(IntPtr exceptionPointers);

    // Token: 0x0200055F RID: 1375
    [NullableContext(0)]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private struct MinidumpExceptionInformation
    {
      // Token: 0x04001956 RID: 6486
      public uint ThreadId;

      // Token: 0x04001957 RID: 6487
      public IntPtr ExceptionPointers;

      // Token: 0x04001958 RID: 6488
      [MarshalAs(UnmanagedType.Bool)]
      public bool ClientPointers;
    }

    // Token: 0x02000560 RID: 1376
    [Nullable(0)]
    public sealed class ReportingSynchronizationContext : SynchronizationContext
    {
      // Token: 0x06002DE5 RID: 11749 RVA: 0x0065E090 File Offset: 0x0065E090
      public ReportingSynchronizationContext(DispatcherQueue queue)
      {
        this.inner = new DispatcherQueueSynchronizationContext(this.<queue>P);
        base..ctor();
      }

      // Token: 0x06002DE6 RID: 11750 RVA: 0x0065E0B0 File Offset: 0x0065E0B0
      public override void Post(SendOrPostCallback d, [Nullable(2)] object state)
      {
        this.inner.Post(delegate(object s)
        {
          ValueTuple<SendOrPostCallback, object> valueTuple = (ValueTuple<SendOrPostCallback, object>)s;
          SendOrPostCallback item = valueTuple.Item1;
          object item2 = valueTuple.Item2;
          ValueTuple<object, long> valueTuple2 = UiWork.Enter(UiWork.Pick(item, item2));
          try
          {
            item(item2);
          }
          catch (Exception ex)
          {
            CrashReporting.ReportUiException(ex);
          }
          finally
          {
            UiWork.Exit(valueTuple2);
          }
        }, new ValueTuple<SendOrPostCallback, object>(d, state));
      }

      // Token: 0x06002DE7 RID: 11751 RVA: 0x0065E0E8 File Offset: 0x0065E0E8
      public override void Send(SendOrPostCallback d, [Nullable(2)] object state)
      {
        this.inner.Send(d, state);
      }

      // Token: 0x06002DE8 RID: 11752 RVA: 0x0065E0F8 File Offset: 0x0065E0F8
      public override SynchronizationContext CreateCopy()
      {
        return new CrashReporting.ReportingSynchronizationContext(this.<queue>P);
      }

      // Token: 0x04001959 RID: 6489
      [CompilerGenerated]
      private DispatcherQueue <queue>P = queue;

      // Token: 0x0400195A RID: 6490
      private readonly DispatcherQueueSynchronizationContext inner;
    }

    // Token: 0x02000561 RID: 1377
    [Nullable(0)]
    private static class NativeCrash
    {
      // Token: 0x06002DE9 RID: 11753
      [DllImport("kernel32.dll")]
      private static extern IntPtr CreateThread(IntPtr attributes, UIntPtr stackSize, IntPtr startAddress, IntPtr parameter, uint flags, out uint threadId);

      // Token: 0x06002DEA RID: 11754
      [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
      private static extern IntPtr LoadLibraryW(string name);

      // Token: 0x06002DEB RID: 11755
      [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
      private static extern IntPtr GetProcAddress(IntPtr module, string name);

      // Token: 0x06002DEC RID: 11756 RVA: 0x0065E108 File Offset: 0x0065E108
      public static void CrashOnNativeThread()
      {
        IntPtr procAddress = CrashReporting.NativeCrash.GetProcAddress(CrashReporting.NativeCrash.LoadLibraryW("ucrtbase.dll"), "strlen");
        uint num;
        CrashReporting.NativeCrash.CreateThread(IntPtr.Zero, UIntPtr.Zero, procAddress, new IntPtr(16), 0U, out num);
      }
    }

    // Token: 0x02000562 RID: 1378
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x0400195B RID: 6491
      [Nullable(0)]
      public static UnhandledExceptionEventHandler <0>__OnAppDomainUnhandledException;

      // Token: 0x0400195C RID: 6492
      [Nullable(new byte[] { 0, 1 })]
      public static EventHandler<UnobservedTaskExceptionEventArgs> <1>__OnUnobservedTaskException;

      // Token: 0x0400195D RID: 6493
      [Nullable(0)]
      public static CrashReporting.TopLevelExceptionFilter <2>__OnNativeUnhandledException;
    }
  }
}
