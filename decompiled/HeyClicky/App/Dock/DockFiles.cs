using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000332 RID: 818
  [NullableContext(1)]
  [Nullable(0)]
  internal static class DockFiles
  {
    // Token: 0x060022EB RID: 8939 RVA: 0x00629454 File Offset: 0x00629454
    public static bool IsWebLink(string entry)
    {
      return entry.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || entry.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
    }

    // Token: 0x060022EC RID: 8940 RVA: 0x00629474 File Offset: 0x00629474
    public static string Resolve(string entry, [Nullable(2)] string workspace)
    {
      if (DockFiles.IsWebLink(entry))
      {
        return entry;
      }
      string text = entry.Trim();
      Uri uri;
      if (text.StartsWith("file://", StringComparison.OrdinalIgnoreCase) && Uri.TryCreate(text, UriKind.Absolute, out uri))
      {
        text = uri.LocalPath;
      }
      text = Environment.ExpandEnvironmentVariables(text.Replace('/', '\\'));
      if (text.StartsWith("~\\"))
      {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string text2 = text;
        text = Path.Combine(folderPath, text2.Substring(2, text2.Length - 2));
      }
      if (Path.IsPathRooted(text))
      {
        return text;
      }
      if (workspace != null)
      {
        return Path.GetFullPath(Path.Combine(workspace, text));
      }
      return text;
    }

    // Token: 0x060022ED RID: 8941 RVA: 0x00629508 File Offset: 0x00629508
    public static void Open(string entry, [Nullable(2)] string workspace)
    {
      try
      {
        string text = DockFiles.Resolve(entry, workspace);
        if (!DockFiles.IsWebLink(entry) && !File.Exists(text) && !Directory.Exists(text))
        {
          string text2 = Path.GetDirectoryName(text);
          while (text2 != null && !Directory.Exists(text2))
          {
            text2 = Path.GetDirectoryName(text2);
          }
          if (text2 == null)
          {
            return;
          }
          text = text2;
        }
        ShellOpen.Start(text);
      }
      catch (Exception ex)
      {
        Log.Warn("dock", "open " + entry + ": " + ex.Message);
      }
    }

    // Token: 0x060022EE RID: 8942 RVA: 0x00629594 File Offset: 0x00629594
    public static void Reveal(string entry, [Nullable(2)] string workspace)
    {
      if (DockFiles.IsWebLink(entry))
      {
        return;
      }
      string text = DockFiles.Resolve(entry, workspace);
      try
      {
        if (File.Exists(text))
        {
          Process.Start("explorer.exe", "/select,\"" + text + "\"");
        }
        else
        {
          DockFiles.Open(entry, workspace);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("dock", "reveal " + entry + ": " + ex.Message);
      }
    }

    // Token: 0x060022EF RID: 8943 RVA: 0x00629614 File Offset: 0x00629614
    public static string FormatBytes(long b)
    {
      string text;
      if (b < 1000000L)
      {
        if (b >= 1000L)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 1);
          defaultInterpolatedStringHandler.AppendFormatted<double>((double)b / 1000.0, "0");
          defaultInterpolatedStringHandler.AppendLiteral(" KB");
          text = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
          defaultInterpolatedStringHandler.AppendFormatted<long>(b);
          defaultInterpolatedStringHandler.AppendLiteral(" bytes");
          text = defaultInterpolatedStringHandler.ToStringAndClear();
        }
      }
      else if (b >= 1000000000L)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 1);
        defaultInterpolatedStringHandler.AppendFormatted<double>((double)b / 1000000000.0, "0.#");
        defaultInterpolatedStringHandler.AppendLiteral(" GB");
        text = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 1);
        defaultInterpolatedStringHandler.AppendFormatted<double>((double)b / 1000000.0, "0.#");
        defaultInterpolatedStringHandler.AppendLiteral(" MB");
        text = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      return text;
    }
  }
}
