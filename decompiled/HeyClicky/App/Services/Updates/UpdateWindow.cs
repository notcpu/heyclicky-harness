using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace HeyClicky.App.Services.Updates
{
  // Token: 0x020000F1 RID: 241
  [NullableContext(1)]
  [Nullable(0)]
  internal static class UpdateWindow
  {
    // Token: 0x06000BCE RID: 3022 RVA: 0x0055CA94 File Offset: 0x0055CA94
    [NullableContext(2)]
    public static bool TryShow(string packageFileName)
    {
      bool flag;
      try
      {
        string text = AppContext.BaseDirectory.TrimEnd(new char[]
        {
          Path.DirectorySeparatorChar,
          Path.AltDirectorySeparatorChar
        });
        string text2 = Path.GetDirectoryName(text) ?? "";
        string text3 = Path.Combine(text, "HeyClicky-Update.exe");
        if (!File.Exists(text3) || !File.Exists(Path.Combine(text2, "Update.exe")))
        {
          Log.Info("updates", "no update window in this build");
          flag = false;
        }
        else
        {
          string text4 = AppPaths.Combine(new string[] { "updater" });
          Directory.CreateDirectory(text4);
          string text5 = Path.Combine(text4, "HeyClicky-Update.exe");
          try
          {
            File.Copy(text3, text5, true);
          }
          catch (IOException)
          {
            string text6 = text4;
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 1);
            defaultInterpolatedStringHandler.AppendLiteral("HeyClicky-Update-");
            defaultInterpolatedStringHandler.AppendFormatted<int>(Environment.ProcessId);
            defaultInterpolatedStringHandler.AppendLiteral(".exe");
            text5 = Path.Combine(text6, defaultInterpolatedStringHandler.ToStringAndClear());
            File.Copy(text3, text5, true);
          }
          ProcessStartInfo processStartInfo = new ProcessStartInfo(text5)
          {
            UseShellExecute = false,
            WorkingDirectory = text4
          };
          processStartInfo.ArgumentList.Add("--update-window");
          processStartInfo.ArgumentList.Add("--pid");
          processStartInfo.ArgumentList.Add(Environment.ProcessId.ToString(CultureInfo.InvariantCulture));
          processStartInfo.ArgumentList.Add("--root");
          processStartInfo.ArgumentList.Add(text2);
          if (!string.IsNullOrEmpty(packageFileName))
          {
            processStartInfo.ArgumentList.Add("--package");
            processStartInfo.ArgumentList.Add(Path.Combine(text2, "packages", packageFileName));
          }
          processStartInfo.ArgumentList.Add("--log");
          processStartInfo.ArgumentList.Add(Path.Combine(Log.Directory, "update-window.log"));
          Process process = Process.Start(processStartInfo);
          if (process != null)
          {
            process.Dispose();
          }
          Log.Info("updates", "update window shown");
          flag = true;
        }
      }
      catch (Exception ex)
      {
        Log.Warn("updates", "update window didn't start: " + ex.Message);
        flag = false;
      }
      return flag;
    }

    // Token: 0x06000BCF RID: 3023 RVA: 0x0055CCD8 File Offset: 0x0055CCD8
    public static void SignalAppReady()
    {
      try
      {
        EventWaitHandle eventWaitHandle;
        if (EventWaitHandle.TryOpenExisting("Local\\HeyClicky.AppReady", out eventWaitHandle))
        {
          using (eventWaitHandle)
          {
            eventWaitHandle.Set();
          }
          Log.Info("updates", "told the installer / update window HeyClicky is up");
        }
      }
      catch (Exception ex)
      {
        Log.Warn("updates", "app-ready signal: " + ex.Message);
      }
    }

    // Token: 0x04000449 RID: 1097
    private const string Area = "updates";

    // Token: 0x0400044A RID: 1098
    private const string ExeName = "HeyClicky-Update.exe";

    // Token: 0x0400044B RID: 1099
    private const string AppReadyEventName = "Local\\HeyClicky.AppReady";
  }
}
