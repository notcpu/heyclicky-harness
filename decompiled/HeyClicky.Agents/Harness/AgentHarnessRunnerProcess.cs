using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents.Runtime;

namespace HeyClicky.Agents.Harness
{
  // Token: 0x0200005D RID: 93
  [NullableContext(1)]
  [Nullable(0)]
  public static class AgentHarnessRunnerProcess
  {
    // Token: 0x170001E0 RID: 480
    // (get) Token: 0x0600078A RID: 1930 RVA: 0x000FD480 File Offset: 0x000FD480
    // (set) Token: 0x0600078B RID: 1931 RVA: 0x000FD488 File Offset: 0x000FD488
    public static string StateDirectory { get; set; } = AppPaths.Combine(new string[] { "agents", "AgentHarness" });

    // Token: 0x170001E1 RID: 481
    // (get) Token: 0x0600078C RID: 1932 RVA: 0x000FD490 File Offset: 0x000FD490
    // (set) Token: 0x0600078D RID: 1933 RVA: 0x000FD498 File Offset: 0x000FD498
    public static string BundledRunnerDirectory { get; set; } = Path.Combine(AppContext.BaseDirectory, "HarnessRuntime");

    // Token: 0x14000018 RID: 24
    // (add) Token: 0x0600078E RID: 1934 RVA: 0x000FD4A0 File Offset: 0x000FD4A0
    // (remove) Token: 0x0600078F RID: 1935 RVA: 0x000FD4D4 File Offset: 0x000FD4D4
    [Nullable(new byte[] { 2, 1, 1, 1, 1 })]
    [field: Nullable(new byte[] { 2, 1, 1, 1, 1 })]
    public static event Action<string, string, string, string> InternalError;

    // Token: 0x06000790 RID: 1936 RVA: 0x000FD508 File Offset: 0x000FD508
    private static void ReportInternalError(string stage, string detail, string key)
    {
      try
      {
        Action<string, string, string, string> internalError = AgentHarnessRunnerProcess.InternalError;
        if (internalError != null)
        {
          internalError("agent_harness", stage, (detail.Length > 500) ? detail.Substring(0, 500) : detail, key);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Harness", "InternalError handler threw: " + ex.Message);
      }
    }

    // Token: 0x170001E2 RID: 482
    // (get) Token: 0x06000791 RID: 1937 RVA: 0x000FD578 File Offset: 0x000FD578
    public static bool IsRunning
    {
      get
      {
        if (AgentHarnessRunnerProcess.endpoint != null)
        {
          Process process = AgentHarnessRunnerProcess.process;
          return process != null && !process.HasExited;
        }
        return false;
      }
    }

    // Token: 0x06000792 RID: 1938 RVA: 0x000FD5A4 File Offset: 0x000FD5A4
    public static async Task<AgentHarnessRunnerProcess.Endpoint> EnsureRunningAsync(CancellationToken ct = default(CancellationToken))
    {
      AgentHarnessRunnerProcess.Endpoint endpoint = AgentHarnessRunnerProcess.endpoint;
      if (endpoint != null)
      {
        Process process = AgentHarnessRunnerProcess.process;
        if (process != null && !process.HasExited)
        {
          return endpoint;
        }
      }
      await AgentHarnessRunnerProcess.StartGate.WaitAsync(ct).ConfigureAwait(false);
      AgentHarnessRunnerProcess.Endpoint endpoint3;
      try
      {
        AgentHarnessRunnerProcess.Endpoint endpoint2 = AgentHarnessRunnerProcess.endpoint;
        if (endpoint2 != null)
        {
          Process process = AgentHarnessRunnerProcess.process;
          if (process != null && !process.HasExited)
          {
            return endpoint2;
          }
        }
        AgentHarnessRunnerProcess.endpoint = null;
        try
        {
          endpoint3 = (AgentHarnessRunnerProcess.endpoint = await AgentHarnessRunnerProcess.StartAsync(ct).ConfigureAwait(false));
        }
        catch (Exception ex) when (!(ex is OperationCanceledException) || !ct.IsCancellationRequested)
        {
          Log.Warn("Harness", "[harness runner] couldn't start: " + ex.Message);
          AgentHarnessRunnerProcess.ReportInternalError("runner_start_failed", ex.Message, "runner_start");
          throw new AgentHarnessException("HeyClicky's agent runner couldn't start, so this job didn't run. " + ex.Message, true, true);
        }
      }
      finally
      {
        AgentHarnessRunnerProcess.StartGate.Release();
      }
      return endpoint3;
    }

    // Token: 0x06000793 RID: 1939 RVA: 0x000FD5E8 File Offset: 0x000FD5E8
    public static void Stop()
    {
      Process process = AgentHarnessRunnerProcess.process;
      AgentHarnessRunnerProcess.process = null;
      AgentHarnessRunnerProcess.endpoint = null;
      if (process == null)
      {
        return;
      }
      try
      {
        process.StandardInput.Close();
        if (!process.WaitForExit(1500))
        {
          process.Kill(true);
        }
      }
      catch (Exception ex) when (ex is InvalidOperationException || ex is Win32Exception || ex is IOException)
      {
      }
      finally
      {
        process.Dispose();
      }
    }

    // Token: 0x170001E3 RID: 483
    // (get) Token: 0x06000794 RID: 1940 RVA: 0x000FD684 File Offset: 0x000FD684
    public static string RunnerScriptPath
    {
      get
      {
        string text = AgentDefaults.String("ClickyAgentHarnessRunnerPath");
        if (text != null && text.Length > 0)
        {
          return text;
        }
        string environmentVariable = Environment.GetEnvironmentVariable("HEYCLICKY_AGENT_HARNESS_RUNNER");
        if (environmentVariable != null && environmentVariable.Length > 0)
        {
          return environmentVariable;
        }
        return Path.Combine(AgentHarnessRunnerProcess.RepoRoot("C:\\Users\\niles\\code\\clicky-closed\\windows\\src\\HeyClicky.Agents\\Harness\\AgentHarnessRunnerRuntime.cs"), "agent-harness", "runner.mjs");
      }
    }

    // Token: 0x06000795 RID: 1941 RVA: 0x000FD6DC File Offset: 0x000FD6DC
    private unsafe static string RepoRoot([CallerFilePath] string sourceFile = "")
    {
      <>y__InlineArray5<string> <>y__InlineArray = default(<>y__InlineArray5<string>);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray5<string>, string>(ref <>y__InlineArray, 0) = Path.GetDirectoryName(sourceFile) ?? ".";
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray5<string>, string>(ref <>y__InlineArray, 1) = "..";
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray5<string>, string>(ref <>y__InlineArray, 2) = "..";
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray5<string>, string>(ref <>y__InlineArray, 3) = "..";
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray5<string>, string>(ref <>y__InlineArray, 4) = "..";
      return Path.GetFullPath(Path.Combine(<PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray5<string>, string>(in <>y__InlineArray, 5)));
    }

    // Token: 0x06000796 RID: 1942 RVA: 0x000FD754 File Offset: 0x000FD754
    [NullableContext(2)]
    public static string NodeExecutablePath()
    {
      string text = AgentDefaults.String("ClickyAgentHarnessNodePath");
      if (text != null && text.Length > 0 && File.Exists(text))
      {
        return text;
      }
      List<string> list = new List<string>();
      foreach (string text2 in (Environment.GetEnvironmentVariable("PATH") ?? "").Split(';', StringSplitOptions.RemoveEmptyEntries))
      {
        list.Add(Path.Combine(text2.Trim(), "node.exe"));
      }
      string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
      string folderPath2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      string folderPath3 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      list.Add(Path.Combine(folderPath, "nodejs", "node.exe"));
      list.AddRange(AgentHarnessRunnerProcess.NewestVersionedNodes(Path.Combine(folderPath2, "nvm"), "node.exe"));
      list.AddRange(AgentHarnessRunnerProcess.NewestVersionedNodes(Path.Combine(folderPath3, "fnm", "node-versions"), Path.Combine("installation", "node.exe")));
      list.Add(Path.Combine(folderPath3, "Volta", "bin", "node.exe"));
      foreach (string text3 in list.Distinct<string>(StringComparer.OrdinalIgnoreCase))
      {
        if (File.Exists(text3) && AgentHarnessRunnerProcess.NodeMajorVersion(text3) >= 22)
        {
          return text3;
        }
      }
      return null;
    }

    // Token: 0x06000797 RID: 1943 RVA: 0x000FD8CC File Offset: 0x000FD8CC
    private static IEnumerable<string> NewestVersionedNodes(string directory, string binary = "node.exe")
    {
      if (!Directory.Exists(directory))
      {
        return Array.Empty<string>();
      }
      return from c in Directory.GetDirectories(directory).Select<string, ValueTuple<string, Version>>(delegate(string d)
        {
          Version version;
          return new ValueTuple<string, Version>(Path.Combine(d, binary), Version.TryParse(Path.GetFileName(d).TrimStart('v'), out version) ? version : null);
        }).Where<ValueTuple<string, Version>>(delegate([TupleElementNames(new string[] { "Path", "Version" })] ValueTuple<string, Version> c)
        {
          Version item = c.Item2;
          return item != null && item.Major >= 22;
        })
        orderby c.Item2 descending
        select c.Item1;
    }

    // Token: 0x06000798 RID: 1944 RVA: 0x000FD978 File Offset: 0x000FD978
    private static int NodeMajorVersion(string node)
    {
      int num;
      try
      {
        using (Process process = Process.Start(new ProcessStartInfo(node, "--version")
        {
          UseShellExecute = false,
          RedirectStandardOutput = true,
          CreateNoWindow = true
        }))
        {
          if (process == null)
          {
            num = 0;
          }
          else
          {
            string text = process.StandardOutput.ReadToEnd().Trim();
            if (!process.WaitForExit(5000))
            {
              try
              {
                process.Kill();
              }
              catch
              {
              }
              num = 0;
            }
            else
            {
              int num2;
              num = (int.TryParse(text.TrimStart('v').Split('.', StringSplitOptions.None)[0], out num2) ? num2 : 0);
            }
          }
        }
      }
      catch (Exception)
      {
        num = 0;
      }
      return num;
    }

    // Token: 0x170001E4 RID: 484
    // (get) Token: 0x06000799 RID: 1945 RVA: 0x000FDA3C File Offset: 0x000FDA3C
    public static string BundledRunnerPath
    {
      get
      {
        return Path.Combine(AgentHarnessRunnerProcess.BundledRunnerDirectory, "clicky-harness.exe");
      }
    }

    // Token: 0x0600079A RID: 1946 RVA: 0x000FDA50 File Offset: 0x000FDA50
    private static AgentHarnessRunnerProcess.Launch ResolveLaunch()
    {
      string bundledRunnerPath = AgentHarnessRunnerProcess.BundledRunnerPath;
      if (!File.Exists(bundledRunnerPath))
      {
        throw new AgentHarnessException("This build has no agent harness runner (HarnessRuntime\\clicky-harness.exe).", true, false);
      }
      return new AgentHarnessRunnerProcess.Launch.Bundled(bundledRunnerPath);
    }

    // Token: 0x0600079B RID: 1947 RVA: 0x000FDA74 File Offset: 0x000FDA74
    private static async Task<AgentHarnessRunnerProcess.Endpoint> StartAsync(CancellationToken ct)
    {
      AgentHarnessRunnerProcess.<>c__DisplayClass33_0 CS$<>8__locals1 = new AgentHarnessRunnerProcess.<>c__DisplayClass33_0();
      AgentHarnessRunnerProcess.Launch launch = AgentHarnessRunnerProcess.ResolveLaunch();
      Directory.CreateDirectory(AgentHarnessRunnerProcess.StateDirectory);
      string token = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
      AgentHarnessRunnerProcess.Launch.Bundled bundled = launch as AgentHarnessRunnerProcess.Launch.Bundled;
      ProcessStartInfo processStartInfo;
      if (bundled == null)
      {
        AgentHarnessRunnerProcess.Launch.Node node = launch as AgentHarnessRunnerProcess.Launch.Node;
        if (node == null)
        {
          throw new InvalidOperationException();
        }
        processStartInfo = new ProcessStartInfo(node.NodePath)
        {
          ArgumentList = { node.Script },
          WorkingDirectory = Path.GetDirectoryName(node.Script)
        };
      }
      else
      {
        processStartInfo = new ProcessStartInfo(bundled.Binary)
        {
          WorkingDirectory = AgentHarnessRunnerProcess.StateDirectory
        };
      }
      ProcessStartInfo processStartInfo2 = processStartInfo;
      processStartInfo2.UseShellExecute = false;
      processStartInfo2.RedirectStandardInput = true;
      processStartInfo2.RedirectStandardOutput = true;
      processStartInfo2.RedirectStandardError = true;
      processStartInfo2.CreateNoWindow = true;
      processStartInfo2.StandardOutputEncoding = new UTF8Encoding(false);
      processStartInfo2.StandardErrorEncoding = new UTF8Encoding(false);
      AgentHarnessRunnerProcess.Launch.Node node2 = launch as AgentHarnessRunnerProcess.Launch.Node;
      if (node2 != null)
      {
        string text2;
        string text = (processStartInfo2.Environment.TryGetValue("PATH", out text2) ? (text2 ?? "") : "");
        processStartInfo2.Environment["PATH"] = string.Join<string>(';', new string[]
        {
          Path.GetDirectoryName(node2.NodePath),
          text
        }.Where<string>((string s) => s.Length > 0));
      }
      processStartInfo2.Environment["CLICKY_HARNESS_TOKEN"] = token;
      processStartInfo2.Environment["CLICKY_HARNESS_STATE"] = AgentHarnessRunnerProcess.StateDirectory;
      processStartInfo2.Environment["CLICKY_H1_STATE_DIR"] = Path.Combine(AgentHarnessRunnerProcess.StateDirectory, "h1-local");
      CS$<>8__locals1.started = new Process
      {
        StartInfo = processStartInfo2,
        EnableRaisingEvents = true
      };
      if (!CS$<>8__locals1.started.Start())
      {
        throw new AgentHarnessException("The agent harness runner didn't start.", false, false);
      }
      try
      {
        KillOnCloseJob shared = KillOnCloseJob.Shared;
        if (shared != null)
        {
          shared.Assign(CS$<>8__locals1.started);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Harness", "could not assign the runner to the job object: " + ex.Message);
      }
      CS$<>8__locals1.started.ErrorDataReceived += delegate(object _, DataReceivedEventArgs e)
      {
        if (!string.IsNullOrWhiteSpace(e.Data))
        {
          Log.Info("Harness", "[harness runner] " + ((e.Data.Length > 600) ? e.Data.Substring(0, 600) : e.Data));
        }
      };
      CS$<>8__locals1.started.BeginErrorReadLine();
      CS$<>8__locals1.started.Exited += delegate([Nullable(2)] object _, EventArgs _)
      {
        int num2;
        try
        {
          num2 = CS$<>8__locals1.started.ExitCode;
        }
        catch
        {
          num2 = -1;
        }
        string text5 = "Harness";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(26, 1);
        defaultInterpolatedStringHandler2.AppendLiteral("[harness runner] exited (");
        defaultInterpolatedStringHandler2.AppendFormatted<int>(num2);
        defaultInterpolatedStringHandler2.AppendLiteral(")");
        Log.Info(text5, defaultInterpolatedStringHandler2.ToStringAndClear());
        if (AgentHarnessRunnerProcess.process == CS$<>8__locals1.started)
        {
          AgentHarnessRunnerProcess.process = null;
          AgentHarnessRunnerProcess.endpoint = null;
        }
      };
      AgentHarnessRunnerProcess.Endpoint endpoint;
      using (CancellationTokenSource timeout = CancellationTokenSource.CreateLinkedTokenSource(ct))
      {
        timeout.CancelAfter(TimeSpan.FromSeconds(20L));
        string text3;
        try
        {
          text3 = await CS$<>8__locals1.started.StandardOutput.ReadLineAsync(timeout.Token).ConfigureAwait(false);
        }
        catch (OperationCanceledException obj) when (!ct.IsCancellationRequested)
        {
          try
          {
            CS$<>8__locals1.started.Kill(true);
          }
          catch
          {
          }
          throw new AgentHarnessException("The agent harness runner didn't start within 20 s.", false, false);
        }
        int num = 0;
        try
        {
          if (text3 != null)
          {
            JsonObject jsonObject = JsonNode.Parse(text3, null, default(JsonDocumentOptions)) as JsonObject;
            if (jsonObject != null)
            {
              JsonValue jsonValue = jsonObject["port"] as JsonValue;
              if (jsonValue != null)
              {
                jsonValue.TryGetValue<int>(out num);
              }
            }
          }
        }
        catch (JsonException)
        {
        }
        if (num <= 0)
        {
          try
          {
            CS$<>8__locals1.started.Kill(true);
          }
          catch
          {
          }
          throw new AgentHarnessException("The agent harness runner said: " + (text3 ?? "(nothing)").Substring(0, Math.Min(300, (text3 ?? "(nothing)").Length)), false, false);
        }
        Task.Run(delegate
        {
          AgentHarnessRunnerProcess.<>c__DisplayClass33_0.<<StartAsync>b__2>d <<StartAsync>b__2>d;
          <<StartAsync>b__2>d.<>t__builder = AsyncTaskMethodBuilder.Create();
          <<StartAsync>b__2>d.<>4__this = CS$<>8__locals1;
          <<StartAsync>b__2>d.<>1__state = -1;
          <<StartAsync>b__2>d.<>t__builder.Start<AgentHarnessRunnerProcess.<>c__DisplayClass33_0.<<StartAsync>b__2>d>(ref <<StartAsync>b__2>d);
          return <<StartAsync>b__2>d.<>t__builder.Task;
        });
        AgentHarnessRunnerProcess.process = CS$<>8__locals1.started;
        AgentHarnessRunnerProcess.Launch.Bundled bundled2 = launch as AgentHarnessRunnerProcess.Launch.Bundled;
        string text4;
        if (bundled2 == null)
        {
          AgentHarnessRunnerProcess.Launch.Node node3 = launch as AgentHarnessRunnerProcess.Launch.Node;
          if (node3 == null)
          {
            text4 = "";
          }
          else
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(37, 3);
            defaultInterpolatedStringHandler.AppendLiteral("[harness runner] up on 127.0.0.1:");
            defaultInterpolatedStringHandler.AppendFormatted<int>(num);
            defaultInterpolatedStringHandler.AppendLiteral(" (");
            defaultInterpolatedStringHandler.AppendFormatted(node3.NodePath);
            defaultInterpolatedStringHandler.AppendLiteral(" ");
            defaultInterpolatedStringHandler.AppendFormatted(Path.GetFileName(node3.Script));
            defaultInterpolatedStringHandler.AppendLiteral(")");
            text4 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 2);
          defaultInterpolatedStringHandler.AppendLiteral("[harness runner] up on 127.0.0.1:");
          defaultInterpolatedStringHandler.AppendFormatted<int>(num);
          defaultInterpolatedStringHandler.AppendLiteral(" (");
          defaultInterpolatedStringHandler.AppendFormatted(bundled2.Binary);
          defaultInterpolatedStringHandler.AppendLiteral(")");
          text4 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        Log.Info("Harness", text4);
        endpoint = new AgentHarnessRunnerProcess.Endpoint(num, token);
      }
      return endpoint;
    }

    // Token: 0x040002CD RID: 717
    private static readonly SemaphoreSlim StartGate = new SemaphoreSlim(1, 1);

    // Token: 0x040002CE RID: 718
    [Nullable(2)]
    private static Process process;

    // Token: 0x040002CF RID: 719
    [Nullable(2)]
    private static AgentHarnessRunnerProcess.Endpoint endpoint;

    // Token: 0x040002D3 RID: 723
    private const string RunnerPathKey = "ClickyAgentHarnessRunnerPath";

    // Token: 0x040002D4 RID: 724
    private const string NodePathKey = "ClickyAgentHarnessNodePath";

    // Token: 0x040002D5 RID: 725
    public const string UsesBundledRunnerKey = "ClickyAgentHarnessUsesBundledRunner";

    // Token: 0x0200019D RID: 413
    [Nullable(0)]
    public sealed class Endpoint : IEquatable<AgentHarnessRunnerProcess.Endpoint>
    {
      // Token: 0x06000E20 RID: 3616 RVA: 0x00116B4C File Offset: 0x00116B4C
      public Endpoint(int Port, string Token)
      {
        this.Port = Port;
        this.Token = Token;
        base..ctor();
      }

      // Token: 0x170002DB RID: 731
      // (get) Token: 0x06000E21 RID: 3617 RVA: 0x00116B64 File Offset: 0x00116B64
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentHarnessRunnerProcess.Endpoint);
        }
      }

      // Token: 0x170002DC RID: 732
      // (get) Token: 0x06000E22 RID: 3618 RVA: 0x00116B70 File Offset: 0x00116B70
      // (set) Token: 0x06000E23 RID: 3619 RVA: 0x00116B78 File Offset: 0x00116B78
      public int Port { get; set; }

      // Token: 0x170002DD RID: 733
      // (get) Token: 0x06000E24 RID: 3620 RVA: 0x00116B84 File Offset: 0x00116B84
      // (set) Token: 0x06000E25 RID: 3621 RVA: 0x00116B8C File Offset: 0x00116B8C
      public string Token { get; set; }

      // Token: 0x06000E26 RID: 3622 RVA: 0x00116B98 File Offset: 0x00116B98
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Endpoint");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000E27 RID: 3623 RVA: 0x00116BE4 File Offset: 0x00116BE4
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Port = ");
        builder.Append(this.Port.ToString());
        builder.Append(", Token = ");
        builder.Append(this.Token);
        return true;
      }

      // Token: 0x06000E28 RID: 3624 RVA: 0x00116C38 File Offset: 0x00116C38
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentHarnessRunnerProcess.Endpoint left, AgentHarnessRunnerProcess.Endpoint right)
      {
        return !(left == right);
      }

      // Token: 0x06000E29 RID: 3625 RVA: 0x00116C44 File Offset: 0x00116C44
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentHarnessRunnerProcess.Endpoint left, AgentHarnessRunnerProcess.Endpoint right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000E2A RID: 3626 RVA: 0x00116C58 File Offset: 0x00116C58
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Port>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Token>k__BackingField);
      }

      // Token: 0x06000E2B RID: 3627 RVA: 0x00116C98 File Offset: 0x00116C98
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentHarnessRunnerProcess.Endpoint);
      }

      // Token: 0x06000E2C RID: 3628 RVA: 0x00116CA8 File Offset: 0x00116CA8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentHarnessRunnerProcess.Endpoint other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<Port>k__BackingField, other.<Port>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Token>k__BackingField, other.<Token>k__BackingField));
      }

      // Token: 0x06000E2E RID: 3630 RVA: 0x00116D0C File Offset: 0x00116D0C
      [CompilerGenerated]
      private Endpoint(AgentHarnessRunnerProcess.Endpoint original)
      {
        this.Port = original.<Port>k__BackingField;
        this.Token = original.<Token>k__BackingField;
      }

      // Token: 0x06000E2F RID: 3631 RVA: 0x00116D2C File Offset: 0x00116D2C
      [CompilerGenerated]
      public void Deconstruct(out int Port, out string Token)
      {
        Port = this.Port;
        Token = this.Token;
      }
    }

    // Token: 0x0200019E RID: 414
    [Nullable(0)]
    private abstract class Launch : IEquatable<AgentHarnessRunnerProcess.Launch>
    {
      // Token: 0x170002DE RID: 734
      // (get) Token: 0x06000E30 RID: 3632 RVA: 0x00116D40 File Offset: 0x00116D40
      [CompilerGenerated]
      protected virtual Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentHarnessRunnerProcess.Launch);
        }
      }

      // Token: 0x06000E31 RID: 3633 RVA: 0x00116D4C File Offset: 0x00116D4C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Launch");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000E32 RID: 3634 RVA: 0x00116D98 File Offset: 0x00116D98
      [CompilerGenerated]
      protected virtual bool PrintMembers(StringBuilder builder)
      {
        return false;
      }

      // Token: 0x06000E33 RID: 3635 RVA: 0x00116D9C File Offset: 0x00116D9C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentHarnessRunnerProcess.Launch left, AgentHarnessRunnerProcess.Launch right)
      {
        return !(left == right);
      }

      // Token: 0x06000E34 RID: 3636 RVA: 0x00116DA8 File Offset: 0x00116DA8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentHarnessRunnerProcess.Launch left, AgentHarnessRunnerProcess.Launch right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000E35 RID: 3637 RVA: 0x00116DBC File Offset: 0x00116DBC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
      }

      // Token: 0x06000E36 RID: 3638 RVA: 0x00116DD0 File Offset: 0x00116DD0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentHarnessRunnerProcess.Launch);
      }

      // Token: 0x06000E37 RID: 3639 RVA: 0x00116DE0 File Offset: 0x00116DE0
      [NullableContext(2)]
      [CompilerGenerated]
      public virtual bool Equals(AgentHarnessRunnerProcess.Launch other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract);
      }

      // Token: 0x06000E39 RID: 3641 RVA: 0x00116E00 File Offset: 0x00116E00
      [CompilerGenerated]
      protected Launch(AgentHarnessRunnerProcess.Launch original)
      {
      }

      // Token: 0x06000E3A RID: 3642 RVA: 0x00116E08 File Offset: 0x00116E08
      protected Launch()
      {
      }

      // Token: 0x0200020A RID: 522
      [Nullable(0)]
      public sealed class Bundled : AgentHarnessRunnerProcess.Launch, IEquatable<AgentHarnessRunnerProcess.Launch.Bundled>
      {
        // Token: 0x06000FF5 RID: 4085 RVA: 0x001205A0 File Offset: 0x001205A0
        public Bundled(string Binary)
        {
          this.Binary = Binary;
          base..ctor();
        }

        // Token: 0x1700030B RID: 779
        // (get) Token: 0x06000FF6 RID: 4086 RVA: 0x001205B0 File Offset: 0x001205B0
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(AgentHarnessRunnerProcess.Launch.Bundled);
          }
        }

        // Token: 0x1700030C RID: 780
        // (get) Token: 0x06000FF7 RID: 4087 RVA: 0x001205BC File Offset: 0x001205BC
        // (set) Token: 0x06000FF8 RID: 4088 RVA: 0x001205C4 File Offset: 0x001205C4
        public string Binary { get; set; }

        // Token: 0x06000FF9 RID: 4089 RVA: 0x001205D0 File Offset: 0x001205D0
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Bundled");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06000FFA RID: 4090 RVA: 0x0012061C File Offset: 0x0012061C
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Binary = ");
          builder.Append(this.Binary);
          return true;
        }

        // Token: 0x06000FFB RID: 4091 RVA: 0x00120650 File Offset: 0x00120650
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(AgentHarnessRunnerProcess.Launch.Bundled left, AgentHarnessRunnerProcess.Launch.Bundled right)
        {
          return !(left == right);
        }

        // Token: 0x06000FFC RID: 4092 RVA: 0x0012065C File Offset: 0x0012065C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(AgentHarnessRunnerProcess.Launch.Bundled left, AgentHarnessRunnerProcess.Launch.Bundled right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06000FFD RID: 4093 RVA: 0x00120670 File Offset: 0x00120670
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Binary>k__BackingField);
        }

        // Token: 0x06000FFE RID: 4094 RVA: 0x00120690 File Offset: 0x00120690
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as AgentHarnessRunnerProcess.Launch.Bundled);
        }

        // Token: 0x06000FFF RID: 4095 RVA: 0x001206A0 File Offset: 0x001206A0
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(AgentHarnessRunnerProcess.Launch other)
        {
          return this.Equals(other);
        }

        // Token: 0x06001000 RID: 4096 RVA: 0x001206AC File Offset: 0x001206AC
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(AgentHarnessRunnerProcess.Launch.Bundled other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Binary>k__BackingField, other.<Binary>k__BackingField));
        }

        // Token: 0x06001002 RID: 4098 RVA: 0x001206E0 File Offset: 0x001206E0
        [CompilerGenerated]
        private Bundled(AgentHarnessRunnerProcess.Launch.Bundled original)
          : base(original)
        {
          this.Binary = original.<Binary>k__BackingField;
        }

        // Token: 0x06001003 RID: 4099 RVA: 0x001206F8 File Offset: 0x001206F8
        [CompilerGenerated]
        public void Deconstruct(out string Binary)
        {
          Binary = this.Binary;
        }
      }

      // Token: 0x0200020B RID: 523
      [Nullable(0)]
      public sealed class Node : AgentHarnessRunnerProcess.Launch, IEquatable<AgentHarnessRunnerProcess.Launch.Node>
      {
        // Token: 0x06001004 RID: 4100 RVA: 0x00120704 File Offset: 0x00120704
        public Node(string NodePath, string Script)
        {
          this.NodePath = NodePath;
          this.Script = Script;
          base..ctor();
        }

        // Token: 0x1700030D RID: 781
        // (get) Token: 0x06001005 RID: 4101 RVA: 0x0012071C File Offset: 0x0012071C
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(AgentHarnessRunnerProcess.Launch.Node);
          }
        }

        // Token: 0x1700030E RID: 782
        // (get) Token: 0x06001006 RID: 4102 RVA: 0x00120728 File Offset: 0x00120728
        // (set) Token: 0x06001007 RID: 4103 RVA: 0x00120730 File Offset: 0x00120730
        public string NodePath { get; set; }

        // Token: 0x1700030F RID: 783
        // (get) Token: 0x06001008 RID: 4104 RVA: 0x0012073C File Offset: 0x0012073C
        // (set) Token: 0x06001009 RID: 4105 RVA: 0x00120744 File Offset: 0x00120744
        public string Script { get; set; }

        // Token: 0x0600100A RID: 4106 RVA: 0x00120750 File Offset: 0x00120750
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Node");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x0600100B RID: 4107 RVA: 0x0012079C File Offset: 0x0012079C
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("NodePath = ");
          builder.Append(this.NodePath);
          builder.Append(", Script = ");
          builder.Append(this.Script);
          return true;
        }

        // Token: 0x0600100C RID: 4108 RVA: 0x001207F4 File Offset: 0x001207F4
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(AgentHarnessRunnerProcess.Launch.Node left, AgentHarnessRunnerProcess.Launch.Node right)
        {
          return !(left == right);
        }

        // Token: 0x0600100D RID: 4109 RVA: 0x00120800 File Offset: 0x00120800
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(AgentHarnessRunnerProcess.Launch.Node left, AgentHarnessRunnerProcess.Launch.Node right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x0600100E RID: 4110 RVA: 0x00120814 File Offset: 0x00120814
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NodePath>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Script>k__BackingField);
        }

        // Token: 0x0600100F RID: 4111 RVA: 0x0012084C File Offset: 0x0012084C
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as AgentHarnessRunnerProcess.Launch.Node);
        }

        // Token: 0x06001010 RID: 4112 RVA: 0x0012085C File Offset: 0x0012085C
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(AgentHarnessRunnerProcess.Launch other)
        {
          return this.Equals(other);
        }

        // Token: 0x06001011 RID: 4113 RVA: 0x00120868 File Offset: 0x00120868
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(AgentHarnessRunnerProcess.Launch.Node other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<NodePath>k__BackingField, other.<NodePath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Script>k__BackingField, other.<Script>k__BackingField));
        }

        // Token: 0x06001013 RID: 4115 RVA: 0x001208BC File Offset: 0x001208BC
        [CompilerGenerated]
        private Node(AgentHarnessRunnerProcess.Launch.Node original)
          : base(original)
        {
          this.NodePath = original.<NodePath>k__BackingField;
          this.Script = original.<Script>k__BackingField;
        }

        // Token: 0x06001014 RID: 4116 RVA: 0x001208E0 File Offset: 0x001208E0
        [CompilerGenerated]
        public void Deconstruct(out string NodePath, out string Script)
        {
          NodePath = this.NodePath;
          Script = this.Script;
        }
      }
    }
  }
}
