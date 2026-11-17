using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Services
{
  // Token: 0x020000DC RID: 220
  [NullableContext(1)]
  [Nullable(0)]
  public static class AppRelaunch
  {
    // Token: 0x06000A67 RID: 2663 RVA: 0x00558430 File Offset: 0x00558430
    public static bool ScheduleRelaunch([Nullable(new byte[] { 2, 1 })] IEnumerable<string> arguments = null)
    {
      string processPath = Environment.ProcessPath;
      if (string.IsNullOrEmpty(processPath))
      {
        return false;
      }
      List<string> list = ((arguments != null) ? arguments.ToList<string>() : null) ?? new List<string>();
      string text3;
      if (list.Count != 0)
      {
        string text = " -ArgumentList ";
        string text2 = ",";
        IEnumerable<string> enumerable = list;
        Func<string, string> func;
        if ((func = AppRelaunch.<>O.<0>__Quote) == null)
        {
          func = (AppRelaunch.<>O.<0>__Quote = new Func<string, string>(AppRelaunch.Quote));
        }
        text3 = text + string.Join(text2, enumerable.Select<string, string>(func));
      }
      else
      {
        text3 = "";
      }
      string text4 = text3;
      return AppRelaunch.RunAfterExit("Start-Process -FilePath " + AppRelaunch.Quote(processPath) + text4, "relaunch");
    }

    // Token: 0x06000A68 RID: 2664 RVA: 0x005584C4 File Offset: 0x005584C4
    public static bool RunAfterExit(string script, string purpose)
    {
      bool flag;
      try
      {
        int processId = Environment.ProcessId;
        string text = "$ProgressPreference='SilentlyContinue'\n";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(147, 1);
        defaultInterpolatedStringHandler.AppendLiteral("$attempt=0; while (Get-Process -Id ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(processId);
        defaultInterpolatedStringHandler.AppendLiteral(" -ErrorAction SilentlyContinue) { $attempt++; if ($attempt -ge 300) { exit 1 }; Start-Sleep -Milliseconds 100 }\n");
        string text2 = text + defaultInterpolatedStringHandler.ToStringAndClear() + script;
        string text3 = Convert.ToBase64String(Encoding.Unicode.GetBytes(text2));
        using (Process process = Process.Start(new ProcessStartInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "WindowsPowerShell", "v1.0", "powershell.exe"))
        {
          ArgumentList = { "-NoLogo", "-NoProfile", "-NonInteractive", "-ExecutionPolicy", "Bypass", "-WindowStyle", "Hidden", "-EncodedCommand", text3 },
          UseShellExecute = false,
          CreateNoWindow = true,
          WorkingDirectory = Path.GetTempPath()
        }))
        {
          string text4 = "relaunch";
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 3);
          defaultInterpolatedStringHandler.AppendFormatted(purpose);
          defaultInterpolatedStringHandler.AppendLiteral(": helper ");
          defaultInterpolatedStringHandler.AppendFormatted<int?>((process != null) ? new int?(process.Id) : null);
          defaultInterpolatedStringHandler.AppendLiteral(" waits for pid ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(processId);
          defaultInterpolatedStringHandler.AppendLiteral(" to exit");
          Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
          flag = process != null;
        }
      }
      catch (Exception ex)
      {
        Log.Warn("relaunch", purpose + ": helper failed to start: " + ex.Message);
        flag = false;
      }
      return flag;
    }

    // Token: 0x06000A69 RID: 2665 RVA: 0x005586D0 File Offset: 0x005586D0
    public static string Quote(string value)
    {
      return "'" + value.Replace("'", "''") + "'";
    }

    // Token: 0x02000547 RID: 1351
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x0400191F RID: 6431
      [Nullable(0)]
      public static Func<string, string> <0>__Quote;
    }
  }
}
