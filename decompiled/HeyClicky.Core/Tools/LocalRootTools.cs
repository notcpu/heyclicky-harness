using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Documents;

namespace HeyClicky.Core.Tools
{
  // Token: 0x020000A0 RID: 160
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class LocalRootTools
  {
    // Token: 0x06000A0A RID: 2570 RVA: 0x00372C6C File Offset: 0x00372C6C
    public LocalRootTools(Func<string> workspaceRoot, [Nullable(2)] string shell = null)
    {
      this.workspaceRoot = workspaceRoot;
      this.shell = shell ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "WindowsPowerShell", "v1.0", "powershell.exe");
    }

    // Token: 0x06000A0B RID: 2571 RVA: 0x00372CA4 File Offset: 0x00372CA4
    public Task<string> RunAsync(string toolName, ToolArgs args, CancellationToken ct = default(CancellationToken))
    {
      string toolName2 = toolName;
      Task<string> task;
      if (!(toolName2 == "shell_exec"))
      {
        if (!(toolName2 == "file_read") && !(toolName2 == "file_write") && !(toolName2 == "file_list"))
        {
          task = Task.FromResult<string>(ToolResults.Summary("error", toolName, "not_implemented", null));
        }
        else
        {
          task = Task.Run<string>(() => this.FileTool(toolName, args), ct);
        }
      }
      else
      {
        task = this.ShellExecAsync(args, ct);
      }
      return task;
    }

    // Token: 0x06000A0C RID: 2572 RVA: 0x00372D4C File Offset: 0x00372D4C
    private async Task<string> ShellExecAsync(ToolArgs args, CancellationToken ct)
    {
      string command = args.Exact("command");
      string text;
      if (string.IsNullOrWhiteSpace(command))
      {
        text = ToolResults.Summary("blocked", "shell_exec", "shell_exec needs a command.", null);
      }
      else
      {
        int timeoutMs = Math.Max(100, args.Int("timeoutMs").GetValueOrDefault(10000));
        string text2 = this.workspaceRoot();
        string text3 = args.Str(new string[] { "cwd" });
        string cwd = ((text3 != null) ? LocalRootTools.ResolvePath(text3, text2) : text2);
        string text4 = "Tools";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 3);
        defaultInterpolatedStringHandler.AppendLiteral("shell_exec cwd=");
        defaultInterpolatedStringHandler.AppendFormatted(cwd);
        defaultInterpolatedStringHandler.AppendLiteral(" timeoutMs=");
        defaultInterpolatedStringHandler.AppendFormatted<int>(timeoutMs);
        defaultInterpolatedStringHandler.AppendLiteral(" command=");
        defaultInterpolatedStringHandler.AppendFormatted(LocalRootTools.Truncate1(command, 300));
        Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
        LocalRootTools.ShellOutcome shellOutcome = await this.RunPowerShellAsync(command, cwd, timeoutMs, ct).ConfigureAwait(false);
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["command"] = command;
        jsonObject["cwd"] = cwd;
        jsonObject["timeoutMs"] = timeoutMs;
        jsonObject["shell"] = "powershell";
        JsonObject jsonObject2 = jsonObject;
        if (shellOutcome.TimedOut)
        {
          jsonObject2["timedOut"] = true;
          Log.Info("Tools", "shell_exec timed out");
          text = ToolResults.Summary("failed", "shell_exec", "Command timed out." + ((shellOutcome.Output.Length > 0) ? (" Output before the deadline: " + ToolResults.Truncate(shellOutcome.Output, 4000)) : ""), jsonObject2);
        }
        else
        {
          jsonObject2["exitCode"] = shellOutcome.ExitCode;
          string text5 = "Tools";
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 2);
          defaultInterpolatedStringHandler.AppendLiteral("shell_exec exit ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(shellOutcome.ExitCode);
          defaultInterpolatedStringHandler.AppendLiteral(": ");
          defaultInterpolatedStringHandler.AppendFormatted(LocalRootTools.Truncate1(shellOutcome.Output, 400));
          Log.Info(text5, defaultInterpolatedStringHandler.ToStringAndClear());
          text = ToolResults.Summary((shellOutcome.ExitCode == 0) ? "completed" : "failed", "shell_exec", ToolResults.Truncate((shellOutcome.Output.Length == 0) ? "Command completed." : shellOutcome.Output, 4000), jsonObject2);
        }
      }
      return text;
    }

    // Token: 0x06000A0D RID: 2573 RVA: 0x00372DA0 File Offset: 0x00372DA0
    public async Task<LocalRootTools.ShellOutcome> RunPowerShellAsync(string command, string cwd, int timeoutMs, CancellationToken ct = default(CancellationToken))
    {
      LocalRootTools.ShellOutcome shellOutcome;
      if (!Directory.Exists(cwd))
      {
        shellOutcome = new LocalRootTools.ShellOutcome(-1, "The folder " + cwd + " doesn't exist.", false);
      }
      else
      {
        string text = "$ProgressPreference='SilentlyContinue'; [Console]::OutputEncoding=[System.Text.Encoding]::UTF8; $OutputEncoding=[System.Text.Encoding]::UTF8\n" + command;
        string text2 = Convert.ToBase64String(Encoding.Unicode.GetBytes(text));
        ProcessStartInfo processStartInfo = new ProcessStartInfo(this.shell)
        {
          ArgumentList = { "-NoLogo", "-NoProfile", "-NonInteractive", "-ExecutionPolicy", "Bypass", "-EncodedCommand", text2 },
          WorkingDirectory = cwd,
          UseShellExecute = false,
          CreateNoWindow = true,
          RedirectStandardInput = true,
          RedirectStandardOutput = true,
          RedirectStandardError = true,
          StandardOutputEncoding = Encoding.UTF8,
          StandardErrorEncoding = Encoding.UTF8
        };
        StringBuilder stdout = new StringBuilder();
        StringBuilder stderr = new StringBuilder();
        using (Process process = new Process
        {
          StartInfo = processStartInfo,
          EnableRaisingEvents = true
        })
        {
          process.OutputDataReceived += delegate(object _, DataReceivedEventArgs e)
          {
            if (e.Data != null)
            {
              StringBuilder stdout2 = stdout;
              lock (stdout2)
              {
                if (stdout.Length < 16000)
                {
                  stdout.Append(e.Data).Append('\n');
                }
              }
            }
          };
          process.ErrorDataReceived += delegate(object _, DataReceivedEventArgs e)
          {
            if (e.Data != null)
            {
              StringBuilder stderr2 = stderr;
              lock (stderr2)
              {
                if (stderr.Length < 16000)
                {
                  stderr.Append(e.Data).Append('\n');
                }
              }
            }
          };
          try
          {
            process.Start();
          }
          catch (Exception ex)
          {
            return new LocalRootTools.ShellOutcome(-1, ex.Message, false);
          }
          process.StandardInput.Close();
          process.BeginOutputReadLine();
          process.BeginErrorReadLine();
          using (CancellationTokenSource deadline = CancellationTokenSource.CreateLinkedTokenSource(ct))
          {
            deadline.CancelAfter(timeoutMs);
            bool timedOut = false;
            try
            {
              await process.WaitForExitAsync(deadline.Token).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
              timedOut = true;
              try
              {
                process.Kill(true);
              }
              catch
              {
              }
            }
            try
            {
              await process.WaitForExitAsync(CancellationToken.None).WaitAsync(TimeSpan.FromSeconds(2L)).ConfigureAwait(false);
            }
            catch
            {
            }
            StringBuilder stdout3 = stdout;
            string text3;
            lock (stdout3)
            {
              StringBuilder stderr3 = stderr;
              lock (stderr3)
              {
                text3 = string.Join("\n", new string[]
                {
                  stdout.ToString().Trim(),
                  stderr.ToString().Trim()
                }.Where<string>((string s) => s.Length > 0));
              }
            }
            shellOutcome = new LocalRootTools.ShellOutcome(timedOut ? (-1) : LocalRootTools.SafeExitCode(process), text3, timedOut);
          }
        }
      }
      return shellOutcome;
    }

    // Token: 0x06000A0E RID: 2574 RVA: 0x00372E04 File Offset: 0x00372E04
    private static int SafeExitCode(Process process)
    {
      int num;
      try
      {
        num = process.ExitCode;
      }
      catch
      {
        num = -1;
      }
      return num;
    }

    // Token: 0x06000A0F RID: 2575 RVA: 0x00372E30 File Offset: 0x00372E30
    private string FileTool(string toolName, ToolArgs args)
    {
      string text = args.Str(new string[] { "path" });
      if (text == null)
      {
        return ToolResults.Summary("blocked", toolName, toolName + " needs a path.", null);
      }
      string text2 = ((toolName == "file_write") ? args.Exact("content") : null);
      if (toolName == "file_write" && text2 == null)
      {
        return ToolResults.Summary("blocked", toolName, "file_write needs content.", null);
      }
      string text3 = LocalRootTools.ResolvePath(text, this.workspaceRoot());
      string text5;
      try
      {
        if (!(toolName == "file_read"))
        {
          if (!(toolName == "file_list"))
          {
            Directory.CreateDirectory(Path.GetDirectoryName(text3));
            string text4 = text3 + ".clicky-write";
            File.WriteAllText(text4, text2, new UTF8Encoding(false));
            File.Move(text4, text3, true);
            text5 = ToolResults.Summary("completed", toolName, "Wrote " + text3 + ".", null);
          }
          else
          {
            IEnumerable<string> enumerable = new DirectoryInfo(text3).EnumerateFileSystemInfos().Select<FileSystemInfo, string>(delegate(FileSystemInfo entry)
            {
              if (!(entry is DirectoryInfo))
              {
                return entry.Name;
              }
              return entry.Name + "\\";
            }).Order<string>(StringComparer.OrdinalIgnoreCase)
              .Take<string>(100);
            text5 = ToolResults.Summary("completed", toolName, "Listed " + text3 + ": " + string.Join(", ", enumerable), null);
          }
        }
        else
        {
          string text6 = LocalRootTools.ReadText(text3);
          text5 = ToolResults.Summary("completed", toolName, "Read " + text3 + ": " + ToolResults.Truncate(text6, 4000), null);
        }
      }
      catch (Exception ex)
      {
        text5 = ToolResults.Summary("failed", toolName, ex.Message, null);
      }
      return text5;
    }

    // Token: 0x06000A10 RID: 2576 RVA: 0x00373004 File Offset: 0x00373004
    private static string ReadText(string path)
    {
      string text2;
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Write | FileShare.Delete))
      {
        byte[] array = new byte[(int)Math.Min(4194304L, fileStream.Length)];
        fileStream.ReadExactly(array);
        string text = DocumentText.Decode(array);
        if (text == null)
        {
          throw new InvalidDataException("This file isn't text.");
        }
        text2 = text;
      }
      return text2;
    }

    // Token: 0x06000A11 RID: 2577 RVA: 0x00373074 File Offset: 0x00373074
    public static string ResolvePath(string rawPath, string workspaceRoot)
    {
      string text = Environment.ExpandEnvironmentVariables(rawPath.Trim().Trim('"'));
      if (text == "~" || text.StartsWith("~/", StringComparison.Ordinal) || text.StartsWith("~\\", StringComparison.Ordinal))
      {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string text2 = text;
        text = folderPath + text2.Substring(1, text2.Length - 1);
      }
      text = text.Replace('/', '\\');
      return Path.GetFullPath(Path.IsPathFullyQualified(text) ? text : Path.Combine(workspaceRoot, text));
    }

    // Token: 0x06000A12 RID: 2578 RVA: 0x00373100 File Offset: 0x00373100
    private static string Truncate1(string text, int limit)
    {
      return ToolResults.Truncate(text.Replace("\r", "\\r").Replace("\n", "\\n"), limit);
    }

    // Token: 0x0400048D RID: 1165
    public static readonly IReadOnlySet<string> Names = new HashSet<string> { "shell_exec", "file_read", "file_write", "file_list" };

    // Token: 0x0400048E RID: 1166
    public const int DefaultTimeoutMs = 10000;

    // Token: 0x0400048F RID: 1167
    public const int OutputLimit = 4000;

    // Token: 0x04000490 RID: 1168
    private const int ListLimit = 100;

    // Token: 0x04000491 RID: 1169
    private const long ReadLimitBytes = 4194304L;

    // Token: 0x04000492 RID: 1170
    private readonly Func<string> workspaceRoot;

    // Token: 0x04000493 RID: 1171
    private readonly string shell;

    // Token: 0x020003A3 RID: 931
    [Nullable(0)]
    public sealed class ShellOutcome : IEquatable<LocalRootTools.ShellOutcome>
    {
      // Token: 0x0600299B RID: 10651 RVA: 0x00422F10 File Offset: 0x00422F10
      public ShellOutcome(int ExitCode, string Output, bool TimedOut)
      {
        this.ExitCode = ExitCode;
        this.Output = Output;
        this.TimedOut = TimedOut;
        base..ctor();
      }

      // Token: 0x17000802 RID: 2050
      // (get) Token: 0x0600299C RID: 10652 RVA: 0x00422F30 File Offset: 0x00422F30
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(LocalRootTools.ShellOutcome);
        }
      }

      // Token: 0x17000803 RID: 2051
      // (get) Token: 0x0600299D RID: 10653 RVA: 0x00422F3C File Offset: 0x00422F3C
      // (set) Token: 0x0600299E RID: 10654 RVA: 0x00422F44 File Offset: 0x00422F44
      public int ExitCode { get; set; }

      // Token: 0x17000804 RID: 2052
      // (get) Token: 0x0600299F RID: 10655 RVA: 0x00422F50 File Offset: 0x00422F50
      // (set) Token: 0x060029A0 RID: 10656 RVA: 0x00422F58 File Offset: 0x00422F58
      public string Output { get; set; }

      // Token: 0x17000805 RID: 2053
      // (get) Token: 0x060029A1 RID: 10657 RVA: 0x00422F64 File Offset: 0x00422F64
      // (set) Token: 0x060029A2 RID: 10658 RVA: 0x00422F6C File Offset: 0x00422F6C
      public bool TimedOut { get; set; }

      // Token: 0x060029A3 RID: 10659 RVA: 0x00422F78 File Offset: 0x00422F78
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ShellOutcome");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060029A4 RID: 10660 RVA: 0x00422FC4 File Offset: 0x00422FC4
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("ExitCode = ");
        builder.Append(this.ExitCode.ToString());
        builder.Append(", Output = ");
        builder.Append(this.Output);
        builder.Append(", TimedOut = ");
        builder.Append(this.TimedOut.ToString());
        return true;
      }

      // Token: 0x060029A5 RID: 10661 RVA: 0x00423040 File Offset: 0x00423040
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(LocalRootTools.ShellOutcome left, LocalRootTools.ShellOutcome right)
      {
        return !(left == right);
      }

      // Token: 0x060029A6 RID: 10662 RVA: 0x0042304C File Offset: 0x0042304C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(LocalRootTools.ShellOutcome left, LocalRootTools.ShellOutcome right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060029A7 RID: 10663 RVA: 0x00423060 File Offset: 0x00423060
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ExitCode>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Output>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<TimedOut>k__BackingField);
      }

      // Token: 0x060029A8 RID: 10664 RVA: 0x004230C4 File Offset: 0x004230C4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as LocalRootTools.ShellOutcome);
      }

      // Token: 0x060029A9 RID: 10665 RVA: 0x004230D4 File Offset: 0x004230D4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(LocalRootTools.ShellOutcome other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<ExitCode>k__BackingField, other.<ExitCode>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Output>k__BackingField, other.<Output>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<TimedOut>k__BackingField, other.<TimedOut>k__BackingField));
      }

      // Token: 0x060029AB RID: 10667 RVA: 0x00423150 File Offset: 0x00423150
      [CompilerGenerated]
      private ShellOutcome(LocalRootTools.ShellOutcome original)
      {
        this.ExitCode = original.<ExitCode>k__BackingField;
        this.Output = original.<Output>k__BackingField;
        this.TimedOut = original.<TimedOut>k__BackingField;
      }

      // Token: 0x060029AC RID: 10668 RVA: 0x0042317C File Offset: 0x0042317C
      [CompilerGenerated]
      public void Deconstruct(out int ExitCode, out string Output, out bool TimedOut)
      {
        ExitCode = this.ExitCode;
        Output = this.Output;
        TimedOut = this.TimedOut;
      }
    }
  }
}
