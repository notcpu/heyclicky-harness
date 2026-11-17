using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HeyClicky.App.Billing;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Worker;
using HeyClicky.Platform.Audio;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000C5 RID: 197
  [NullableContext(1)]
  [Nullable(0)]
  public static class BugReport
  {
    // Token: 0x0600084B RID: 2123
    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int index);

    // Token: 0x0600084C RID: 2124 RVA: 0x0053D830 File Offset: 0x0053D830
    public static string Run(SettingsHost host)
    {
      string text = "clicky-" + Guid.NewGuid().ToString().ToLowerInvariant();
      BillingUi billing = host.Billing;
      string text2 = ((billing != null) ? billing.State.ReportPlanLabel : null) ?? "unknown";
      string text3 = null;
      try
      {
        text3 = BugReport.WriteDiagnostics(host, text, text2);
      }
      catch (Exception ex)
      {
        Log.Warn("bugreport", "diagnostics zip failed: " + ex.Message);
      }
      Version version = typeof(BugReport).Assembly.GetName().Version;
      bool isEnabled = CrashReporter.Current.IsEnabled;
      StringBuilder stringBuilder = new StringBuilder().Append("Hey team,\n\n<tell us about your bug or request>\n\n--\n\n");
      StringBuilder stringBuilder2 = stringBuilder;
      StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(18, 3, stringBuilder);
      appendInterpolatedStringHandler.AppendLiteral("App: HeyClicky ");
      appendInterpolatedStringHandler.AppendFormatted<int?>((version != null) ? new int?(version.Major) : null);
      appendInterpolatedStringHandler.AppendLiteral(".");
      appendInterpolatedStringHandler.AppendFormatted<int?>((version != null) ? new int?(version.Minor) : null);
      appendInterpolatedStringHandler.AppendLiteral(".");
      appendInterpolatedStringHandler.AppendFormatted<int?>((version != null) ? new int?(version.Build) : null);
      appendInterpolatedStringHandler.AppendLiteral("\n");
      StringBuilder stringBuilder3 = stringBuilder2.Append(ref appendInterpolatedStringHandler);
      StringBuilder stringBuilder4 = stringBuilder3;
      StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler2 = new StringBuilder.AppendInterpolatedStringHandler(13, 2, stringBuilder3);
      appendInterpolatedStringHandler2.AppendLiteral("Windows: ");
      appendInterpolatedStringHandler2.AppendFormatted<Version>(Environment.OSVersion.Version);
      appendInterpolatedStringHandler2.AppendLiteral(" (");
      appendInterpolatedStringHandler2.AppendFormatted<Architecture>(RuntimeInformation.OSArchitecture);
      appendInterpolatedStringHandler2.AppendLiteral(")\n");
      StringBuilder stringBuilder5 = stringBuilder4.Append(ref appendInterpolatedStringHandler2);
      StringBuilder stringBuilder6 = stringBuilder5;
      StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler3 = new StringBuilder.AppendInterpolatedStringHandler(5, 1, stringBuilder5);
      appendInterpolatedStringHandler3.AppendLiteral("PC: ");
      appendInterpolatedStringHandler3.AppendFormatted(Environment.MachineName);
      appendInterpolatedStringHandler3.AppendLiteral("\n");
      StringBuilder stringBuilder7 = stringBuilder6.Append(ref appendInterpolatedStringHandler3);
      StringBuilder stringBuilder8 = stringBuilder7;
      StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler4 = new StringBuilder.AppendInterpolatedStringHandler(20, 1, stringBuilder7);
      appendInterpolatedStringHandler4.AppendLiteral("Number of screens: ");
      appendInterpolatedStringHandler4.AppendFormatted<int>(BugReport.GetSystemMetrics(80));
      appendInterpolatedStringHandler4.AppendLiteral("\n");
      StringBuilder stringBuilder9 = stringBuilder8.Append(ref appendInterpolatedStringHandler4);
      BillingUi billing2 = host.Billing;
      string text4 = ((billing2 != null) ? billing2.State.PaidPlanName : null);
      StringBuilder stringBuilder10 = stringBuilder9.Append((text4 != null) ? ("Plan: " + text4 + "\n") : "");
      StringBuilder stringBuilder11 = stringBuilder10;
      StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler5 = new StringBuilder.AppendInterpolatedStringHandler(11, 1, stringBuilder10);
      appendInterpolatedStringHandler5.AppendLiteral("Report ID: ");
      appendInterpolatedStringHandler5.AppendFormatted(text);
      string text5 = stringBuilder11.Append(ref appendInterpolatedStringHandler5).Append((text3 == null || isEnabled) ? "" : ("\nDiagnostics: please attach " + Path.GetFileName(text3))).ToString();
      string text6 = "https://mail.google.com/mail/?view=cm&fs=1&to=hi%40heyclicky.com&su=" + Uri.EscapeDataString("hey clicky") + "&body=" + Uri.EscapeDataString(text5);
      host.OpenUrl(text6);
      AppHost app = host.App;
      if (app != null)
      {
        app.FoldNotchForOpenedLink();
      }
      ClickyAnalytics.TrackBugReportSubmitted(text);
      if (text3 != null && !isEnabled && Environment.GetEnvironmentVariable("HEYCLICKY_E2E_NO_BROWSER") != "1")
      {
        try
        {
          Process.Start("explorer.exe", "/select,\"" + text3 + "\"");
        }
        catch (Exception ex2)
        {
          Log.Warn("bugreport", "reveal failed: " + ex2.Message);
        }
      }
      WorkerClient worker = host.Worker;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (worker != null)
      {
        string text7 = "bug_report";
        string text8 = "user_report";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 2);
        defaultInterpolatedStringHandler.AppendLiteral("Bug report ");
        defaultInterpolatedStringHandler.AppendFormatted(text);
        defaultInterpolatedStringHandler.AppendLiteral(" (diagnostics: ");
        defaultInterpolatedStringHandler.AppendFormatted((text3 == null) ? "none" : "local zip");
        defaultInterpolatedStringHandler.AppendLiteral(")");
        string text9 = defaultInterpolatedStringHandler.ToStringAndClear();
        string text10 = "";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
        defaultInterpolatedStringHandler.AppendFormatted<int?>((version != null) ? new int?(version.Major) : null);
        defaultInterpolatedStringHandler.AppendLiteral(".");
        defaultInterpolatedStringHandler.AppendFormatted<int?>((version != null) ? new int?(version.Minor) : null);
        defaultInterpolatedStringHandler.AppendLiteral(".");
        defaultInterpolatedStringHandler.AppendFormatted<int?>((version != null) ? new int?(version.Build) : null);
        string text11 = defaultInterpolatedStringHandler.ToStringAndClear();
        int num = 0;
        string text12 = "bug_report";
        UserProfile user = host.Services.Auth.User;
        string text13 = ((user != null) ? user.UserId : null);
        UserProfile user2 = host.Services.Auth.User;
        worker.ReportError(new ErrorReport(text7, text8, text9, text10, text11, num, text12, text13, (user2 != null) ? user2.Email : null));
      }
      if (isEnabled)
      {
        string text14 = text;
        string text15 = text2;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
        defaultInterpolatedStringHandler.AppendFormatted<int?>((version != null) ? new int?(version.Major) : null);
        defaultInterpolatedStringHandler.AppendLiteral(".");
        defaultInterpolatedStringHandler.AppendFormatted<int?>((version != null) ? new int?(version.Minor) : null);
        defaultInterpolatedStringHandler.AppendLiteral(".");
        defaultInterpolatedStringHandler.AppendFormatted<int?>((version != null) ? new int?(version.Build) : null);
        BugReport.SendToSentry(host, text14, text15, defaultInterpolatedStringHandler.ToStringAndClear());
      }
      string text16 = "bugreport";
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 3);
      defaultInterpolatedStringHandler.AppendLiteral("report ");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral(" (");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral("), diagnostics ");
      defaultInterpolatedStringHandler.AppendFormatted(text3 ?? "none");
      Log.Info(text16, defaultInterpolatedStringHandler.ToStringAndClear());
      return text;
    }

    // Token: 0x0600084D RID: 2125 RVA: 0x0053DDE4 File Offset: 0x0053DDE4
    private static void SendToSentry(SettingsHost host, string id, string plan, string appVersion)
    {
      BugReport.<>c__DisplayClass3_0 CS$<>8__locals1 = new BugReport.<>c__DisplayClass3_0();
      CS$<>8__locals1.id = id;
      CS$<>8__locals1.plan = plan;
      try
      {
        CS$<>8__locals1.dir = AppPaths.Combine(new string[] { "bug-reports" });
        Directory.CreateDirectory(CS$<>8__locals1.dir);
        BugReport.<>c__DisplayClass3_0 CS$<>8__locals2 = CS$<>8__locals1;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["schemaVersion"] = 4;
        jsonObject["report_id"] = CS$<>8__locals1.id;
        jsonObject["app_version"] = appVersion;
        jsonObject["windows_version"] = Environment.OSVersion.Version.ToString();
        jsonObject["os_arch"] = RuntimeInformation.OSArchitecture.ToString();
        jsonObject["process_arch"] = RuntimeInformation.ProcessArchitecture.ToString();
        jsonObject["install_location"] = AppContext.BaseDirectory;
        jsonObject["plan"] = CS$<>8__locals1.plan;
        jsonObject["signed_in"] = host.IsSignedIn;
        string text = "voice_state";
        ICompanionSession companion = host.Services.Companion;
        jsonObject[text] = ((companion != null) ? companion.State.ToString() : null);
        string text2 = "agent_task_count";
        IAgentService agents = host.Services.Agents;
        jsonObject[text2] = ((agents != null) ? new int?(agents.Tasks.Count) : null);
        jsonObject["selected_microphone"] = host.Read<string>(([Nullable(1)] AppSettings x) => x.MicrophoneDeviceId) ?? "System Default";
        jsonObject["selected_output_device"] = host.Read<string>(([Nullable(1)] AppSettings x) => x.OutputDeviceId) ?? "System Default";
        jsonObject["display_count"] = BugReport.GetSystemMetrics(80);
        jsonObject["recentErrors"] = new JsonArray(ClickyAnalytics.RecentAppErrorsSnapshot().Select<ClickyAnalytics.RecentAppError, JsonNode>(delegate(ClickyAnalytics.RecentAppError e)
        {
          JsonObject jsonObject3 = new JsonObject(null);
          jsonObject3["timestamp"] = e.TimestampIso8601;
          jsonObject3["stage"] = e.Stage;
          jsonObject3["kind"] = e.Kind;
          jsonObject3["error_message"] = e.ErrorMessage;
          jsonObject3["error_domain"] = e.ErrorDomain;
          jsonObject3["error_code"] = e.ErrorCode;
          jsonObject3["user_facing_message"] = e.UserFacingMessage;
          return jsonObject3;
        }).ToArray<JsonNode>());
        CS$<>8__locals2.state = jsonObject;
        BugReport.<>c__DisplayClass3_0 CS$<>8__locals3 = CS$<>8__locals1;
        JsonObject jsonObject2 = new JsonObject(null);
        jsonObject2["report_id"] = CS$<>8__locals1.id;
        jsonObject2["app_version"] = appVersion;
        string text3 = "voice_state";
        ICompanionSession companion2 = host.Services.Companion;
        jsonObject2[text3] = ((companion2 != null) ? companion2.State.ToString() : null);
        jsonObject2["signed_in"] = host.IsSignedIn;
        string text4 = "running_agent_task_count";
        IAgentService agents2 = host.Services.Agents;
        jsonObject2[text4] = ((agents2 != null) ? new int?(agents2.Tasks.Count) : null);
        CS$<>8__locals3.context = jsonObject2;
        Task.Run(delegate
        {
          try
          {
            string text5 = Path.Combine(CS$<>8__locals1.dir, "clicky-logs-" + CS$<>8__locals1.id + ".log");
            File.WriteAllText(text5, BugReport.RedactSensitiveValues(BugReport.RecentLogText()), new UTF8Encoding(false));
            string text6 = Path.Combine(CS$<>8__locals1.dir, "state-" + CS$<>8__locals1.id + ".json");
            File.WriteAllText(text6, BugReport.RedactSensitiveValues(CS$<>8__locals1.state.ToJsonString(new JsonSerializerOptions
            {
              WriteIndented = true
            })), new UTF8Encoding(false));
            ICrashReporter crashReporter = CrashReporter.Current;
            string text7 = "User Bug Report";
            CrashReportLevel crashReportLevel = CrashReportLevel.Info;
            IReadOnlyList<string> readOnlyList = new <>z__ReadOnlySingleElementList<string>("user-bug-report");
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["report_id"] = CS$<>8__locals1.id;
            dictionary["plan"] = CS$<>8__locals1.plan;
            crashReporter.CaptureMessage(text7, crashReportLevel, readOnlyList, dictionary, "clicky_state", CS$<>8__locals1.context, new <>z__ReadOnlyArray<string>(new string[] { text5, text6 }));
            string text8 = "bugreport";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(62, 2);
            defaultInterpolatedStringHandler.AppendLiteral("report ");
            defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.id);
            defaultInterpolatedStringHandler.AppendLiteral(" sent to Sentry with ");
            defaultInterpolatedStringHandler.AppendFormatted<long>(new FileInfo(text5).Length / 1024L);
            defaultInterpolatedStringHandler.AppendLiteral(" KB of logs and the state snapshot");
            Log.Info(text8, defaultInterpolatedStringHandler.ToStringAndClear());
          }
          catch (Exception ex2)
          {
            Log.Warn("bugreport", "Sentry report failed: " + ex2.Message);
          }
        });
      }
      catch (Exception ex)
      {
        Log.Warn("bugreport", "Sentry report failed: " + ex.Message);
      }
    }

    // Token: 0x0600084E RID: 2126 RVA: 0x0053E16C File Offset: 0x0053E16C
    private static string RecentLogText()
    {
      List<FileInfo> list = (from f in Directory.Exists(Log.Directory) ? Directory.GetFiles(Log.Directory, "*.log") : Array.Empty<string>()
        select new FileInfo(f) into f
        where DateTime.Now - f.LastWriteTime <= TimeSpan.FromDays(2)
        orderby f.LastWriteTime descending
        select f).ToList<FileInfo>();
      List<string> list2 = new List<string>();
      long num = 3145728L;
      bool flag = false;
      foreach (FileInfo fileInfo in list)
      {
        if (num <= 0L)
        {
          flag = true;
          break;
        }
        using (FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Write | FileShare.Delete))
        {
          long num2 = Math.Min(num, fileStream.Length);
          if (num2 < fileStream.Length)
          {
            fileStream.Seek(-num2, SeekOrigin.End);
            flag = true;
          }
          byte[] array = new byte[num2];
          fileStream.ReadExactly(array);
          string text = Encoding.UTF8.GetString(array);
          if (num2 < fileStream.Length)
          {
            int num3 = text.IndexOf('\n');
            if (num3 >= 0)
            {
              string text2 = text;
              int num4 = num3 + 1;
              text = text2.Substring(num4, text2.Length - num4);
            }
          }
          list2.Insert(0, "===== " + fileInfo.Name + " =====\n" + text);
          num -= num2;
        }
      }
      string text3 = string.Join("\n", list2);
      if (!flag)
      {
        return text3;
      }
      return "{\"_note\":\"older log lines truncated to keep the most recent 3MB\"}\n" + text3;
    }

    // Token: 0x0600084F RID: 2127 RVA: 0x0053E35C File Offset: 0x0053E35C
    public static string RedactSensitiveValues(string text)
    {
      text = Regex.Replace(text, "(?i)(access_token|refresh_token|id_token|provider_token)=[A-Za-z0-9._~+/\\-]+", "$1=[redacted]");
      text = Regex.Replace(text, "eyJ[A-Za-z0-9_-]{10,}(?:\\.[A-Za-z0-9_-]+){0,2}", "[redacted]");
      text = Regex.Replace(text, "(?i)bearer\\s+[A-Za-z0-9._\\-]+", "[redacted]");
      text = Regex.Replace(text, "sk-[A-Za-z0-9_-]{16,}", "[redacted]");
      return text;
    }

    // Token: 0x06000850 RID: 2128 RVA: 0x0053E3B4 File Offset: 0x0053E3B4
    private static string WriteDiagnostics(SettingsHost host, string id, string plan)
    {
      string text = AppPaths.Combine(new string[] { "bug-reports" });
      Directory.CreateDirectory(text);
      string text2 = Path.Combine(text, id + ".zip");
      string text4;
      using (ZipArchive zipArchive = ZipFile.Open(text2, ZipArchiveMode.Create))
      {
        foreach (string text3 in Directory.Exists(Log.Directory) ? Directory.GetFiles(Log.Directory, "*.log") : Array.Empty<string>())
        {
          FileInfo fileInfo = new FileInfo(text3);
          if (!(DateTime.Now - fileInfo.LastWriteTime > TimeSpan.FromDays(2)) && fileInfo.Length <= 41943040L)
          {
            using (FileStream fileStream = new FileStream(text3, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Write | FileShare.Delete))
            {
              using (Stream stream = zipArchive.CreateEntry("logs/" + fileInfo.Name, CompressionLevel.Optimal).Open())
              {
                fileStream.CopyTo(stream);
              }
            }
          }
        }
        string filePath = host.Store.FilePath;
        if (File.Exists(filePath))
        {
          host.Store.Flush();
          using (FileStream fileStream2 = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
          {
            using (Stream stream2 = zipArchive.CreateEntry("settings.json").Open())
            {
              fileStream2.CopyTo(stream2);
            }
          }
        }
        StringBuilder stringBuilder = new StringBuilder();
        StringBuilder stringBuilder2 = stringBuilder;
        StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(8, 1, stringBuilder);
        appendInterpolatedStringHandler.AppendLiteral("report: ");
        appendInterpolatedStringHandler.AppendFormatted(id);
        StringBuilder stringBuilder3 = stringBuilder2.AppendLine(ref appendInterpolatedStringHandler);
        StringBuilder stringBuilder4 = stringBuilder3;
        StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler2 = new StringBuilder.AppendInterpolatedStringHandler(6, 1, stringBuilder3);
        appendInterpolatedStringHandler2.AppendLiteral("time: ");
        appendInterpolatedStringHandler2.AppendFormatted<DateTimeOffset>(DateTimeOffset.Now, "O");
        StringBuilder stringBuilder5 = stringBuilder4.AppendLine(ref appendInterpolatedStringHandler2);
        StringBuilder stringBuilder6 = stringBuilder5;
        StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler3 = new StringBuilder.AppendInterpolatedStringHandler(6, 1, stringBuilder5);
        appendInterpolatedStringHandler3.AppendLiteral("plan: ");
        appendInterpolatedStringHandler3.AppendFormatted(plan);
        StringBuilder stringBuilder7 = stringBuilder6.AppendLine(ref appendInterpolatedStringHandler3);
        StringBuilder stringBuilder8 = stringBuilder7;
        StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler4 = new StringBuilder.AppendInterpolatedStringHandler(11, 1, stringBuilder7);
        appendInterpolatedStringHandler4.AppendLiteral("signed in: ");
        appendInterpolatedStringHandler4.AppendFormatted<bool>(host.IsSignedIn);
        StringBuilder stringBuilder9 = stringBuilder8.AppendLine(ref appendInterpolatedStringHandler4);
        StringBuilder stringBuilder10 = stringBuilder9;
        StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler5 = new StringBuilder.AppendInterpolatedStringHandler(13, 1, stringBuilder9);
        appendInterpolatedStringHandler5.AppendLiteral("voice state: ");
        ICompanionSession companion = host.Services.Companion;
        appendInterpolatedStringHandler5.AppendFormatted<VoiceState?>((companion != null) ? new VoiceState?(companion.State) : null);
        StringBuilder stringBuilder11 = stringBuilder10.AppendLine(ref appendInterpolatedStringHandler5);
        StringBuilder stringBuilder12 = stringBuilder11;
        StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler6 = new StringBuilder.AppendInterpolatedStringHandler(13, 1, stringBuilder11);
        appendInterpolatedStringHandler6.AppendLiteral("agent tasks: ");
        IAgentService agents = host.Services.Agents;
        appendInterpolatedStringHandler6.AppendFormatted<int?>((agents != null) ? new int?(agents.Tasks.Count) : null);
        StringBuilder stringBuilder13 = stringBuilder12.AppendLine(ref appendInterpolatedStringHandler6);
        StringBuilder stringBuilder14 = stringBuilder13;
        StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler7 = new StringBuilder.AppendInterpolatedStringHandler(5, 2, stringBuilder13);
        appendInterpolatedStringHandler7.AppendLiteral("os: ");
        appendInterpolatedStringHandler7.AppendFormatted<OperatingSystem>(Environment.OSVersion);
        appendInterpolatedStringHandler7.AppendLiteral(" ");
        appendInterpolatedStringHandler7.AppendFormatted<Architecture>(RuntimeInformation.OSArchitecture);
        StringBuilder stringBuilder15 = stringBuilder14.AppendLine(ref appendInterpolatedStringHandler7);
        StringBuilder stringBuilder16 = stringBuilder15;
        StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler8 = new StringBuilder.AppendInterpolatedStringHandler(9, 1, stringBuilder15);
        appendInterpolatedStringHandler8.AppendLiteral("process: ");
        appendInterpolatedStringHandler8.AppendFormatted<Architecture>(RuntimeInformation.ProcessArchitecture);
        StringBuilder stringBuilder17 = stringBuilder16.AppendLine(ref appendInterpolatedStringHandler8);
        StringBuilder stringBuilder18 = stringBuilder17;
        StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler9 = new StringBuilder.AppendInterpolatedStringHandler(17, 1, stringBuilder17);
        appendInterpolatedStringHandler9.AppendLiteral("microphone pick: ");
        appendInterpolatedStringHandler9.AppendFormatted(host.Read<string>(([Nullable(1)] AppSettings x) => x.MicrophoneDeviceId) ?? "System Default");
        StringBuilder stringBuilder19 = stringBuilder18.AppendLine(ref appendInterpolatedStringHandler9);
        StringBuilder stringBuilder20 = stringBuilder19;
        StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler10 = new StringBuilder.AppendInterpolatedStringHandler(13, 1, stringBuilder19);
        appendInterpolatedStringHandler10.AppendLiteral("output pick: ");
        appendInterpolatedStringHandler10.AppendFormatted(host.Read<string>(([Nullable(1)] AppSettings x) => x.OutputDeviceId) ?? "System Default");
        StringBuilder stringBuilder21 = stringBuilder20.AppendLine(ref appendInterpolatedStringHandler10);
        StringBuilder stringBuilder22 = stringBuilder21;
        StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler11 = new StringBuilder.AppendInterpolatedStringHandler(16, 1, stringBuilder21);
        appendInterpolatedStringHandler11.AppendLiteral("audio topology: ");
        appendInterpolatedStringHandler11.AppendFormatted<AudioTopologySnapshot>(AudioTopologyMonitor.Shared.Snapshot);
        StringBuilder stringBuilder23 = stringBuilder22.AppendLine(ref appendInterpolatedStringHandler11);
        StringBuilder stringBuilder24 = stringBuilder23;
        StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler12 = new StringBuilder.AppendInterpolatedStringHandler(9, 1, stringBuilder23);
        appendInterpolatedStringHandler12.AppendLiteral("hotkeys: ");
        appendInterpolatedStringHandler12.AppendFormatted<HotkeyBindings>(host.Read<HotkeyBindings>(([Nullable(1)] AppSettings x) => x.EffectiveHotkeys));
        StringBuilder stringBuilder25 = stringBuilder24.AppendLine(ref appendInterpolatedStringHandler12);
        using (StreamWriter streamWriter = new StreamWriter(zipArchive.CreateEntry("app-state.txt").Open()))
        {
          streamWriter.Write(stringBuilder25.ToString());
        }
        text4 = text2;
      }
      return text4;
    }

    // Token: 0x040002D8 RID: 728
    private const int MaxLogBytes = 3145728;
  }
}
