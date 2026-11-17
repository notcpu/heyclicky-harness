using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Harness;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.DevFakes;
using HeyClicky.App.Home;
using HeyClicky.App.Interop;
using HeyClicky.App.Onboarding;
using HeyClicky.App.Portraits;
using HeyClicky.App.Shell;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Voice;
using HeyClicky.Core.Worker;
using HeyClicky.Platform.Audio;
using HeyClicky.Platform.Overlay;
using HeyClicky.Platform.Win32;

namespace HeyClicky.App.Demo
{
  // Token: 0x02000353 RID: 851
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class EndToEnd
  {
    // Token: 0x060024C3 RID: 9411 RVA: 0x00637CD0 File Offset: 0x00637CD0
    private async Task BrainAsync()
    {
      HashSet<string> steps = (Environment.GetEnvironmentVariable("HEYCLICKY_E2E_STEPS") ?? "context,page,document,attach,tools,agent").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToHashSet<string>();
      this.host.Real.BrainContext.Refresh();
      await Task.Delay(2500);
      if (steps.Contains("context"))
      {
        await this.AskAsync("context", "In one sentence: which Clickys do I have, and how many agent messages do I have left this period?", null, 1500);
      }
      if (steps.Contains("page"))
      {
        await this.PageAsync();
      }
      if (steps.Contains("document"))
      {
        await this.DocumentAsync();
      }
      if (steps.Contains("attach"))
      {
        string environmentVariable = Environment.GetEnvironmentVariable("HEYCLICKY_E2E_IMAGE");
        if (environmentVariable != null && environmentVariable.Length > 0)
        {
          await this.AskAsync("attach", "What number is written in this picture? Just the number.", new <>z__ReadOnlySingleElementList<string>(environmentVariable), 1500);
        }
      }
      if (steps.Contains("tools"))
      {
        await this.ToolsAsync();
      }
      if (steps.Contains("agent"))
      {
        await this.BrainAgentAsync();
      }
      if (steps.Contains("muted"))
      {
        await this.MutedAsync();
      }
      if (steps.Contains("escape"))
      {
        await this.EscapeAsync();
      }
      if (steps.Contains("cue"))
      {
        await this.StartCueAsync();
      }
    }

    // Token: 0x060024C4 RID: 9412 RVA: 0x00637D14 File Offset: 0x00637D14
    private Task EscapeAsync()
    {
      EndToEnd.<EscapeAsync>d__1 <EscapeAsync>d__;
      <EscapeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
      <EscapeAsync>d__.<>4__this = this;
      <EscapeAsync>d__.<>1__state = -1;
      <EscapeAsync>d__.<>t__builder.Start<EndToEnd.<EscapeAsync>d__1>(ref <EscapeAsync>d__);
      return <EscapeAsync>d__.<>t__builder.Task;
    }

    // Token: 0x060024C5 RID: 9413
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr FindWindowW([Nullable(2)] string className, string title);

    // Token: 0x060024C6 RID: 9414
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr window);

    // Token: 0x060024C7 RID: 9415
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    // Token: 0x060024C8 RID: 9416
    [DllImport("user32.dll")]
    private static extern void keybd_event(byte key, byte scan, uint flags, UIntPtr extra);

    // Token: 0x060024C9 RID: 9417 RVA: 0x00637D58 File Offset: 0x00637D58
    private async Task StartCueAsync()
    {
      EndToEnd.<>c__DisplayClass6_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass6_0();
      RealStack real = this.host.Real;
      CS$<>8__locals1.said = new List<string>();
      real.Companion.ResponseTextUpdated += CS$<>8__locals1.<StartCueAsync>g__OnText|0;
      try
      {
        Task cue = real.AgentVoice.SpeakStartCueAsync("Find me three cheap flights from Delhi to Tokyo next month.");
        await Task.Delay(2500);
        CursorOverlay inner = real.Cursor.Inner;
        if (inner != null)
        {
          string text = Path.Combine(this.outDir, "e2e-cursor-cue.png");
          inner.SnapshotClusterPng(text, "#F5F5F0");
          EndToEnd.Say("cue: cursor PNG → " + text);
        }
        await cue;
        List<string> said = CS$<>8__locals1.said;
        lock (said)
        {
          EndToEnd.Say("cue: spoken = " + (CS$<>8__locals1.said.LastOrDefault<string>() ?? "(none)"));
        }
        cue = null;
      }
      finally
      {
        real.Companion.ResponseTextUpdated -= CS$<>8__locals1.<StartCueAsync>g__OnText|0;
      }
    }

    // Token: 0x060024CA RID: 9418 RVA: 0x00637D9C File Offset: 0x00637D9C
    private async Task<string> AskAsync(string step, string text, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> files = null, int typingMs = 1500)
    {
      CompanionSession companion = this.host.Real.Companion;
      this.host.DispatcherQueue.TryEnqueue(delegate
      {
        this.host.Services.Presenter.Present(new NotchPresentation.TextInput("", null));
      });
      await Task.Delay(typingMs);
      Stopwatch sw = Stopwatch.StartNew();
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 3);
      defaultInterpolatedStringHandler.AppendFormatted(step);
      defaultInterpolatedStringHandler.AppendLiteral(": ask \"");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("\"");
      string text2;
      if (files == null || files.Count <= 0)
      {
        text2 = "";
      }
      else
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(14, 1);
        defaultInterpolatedStringHandler2.AppendLiteral(" with ");
        defaultInterpolatedStringHandler2.AppendFormatted<int>(files.Count);
        defaultInterpolatedStringHandler2.AppendLiteral(" file(s)");
        text2 = defaultInterpolatedStringHandler2.ToStringAndClear();
      }
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      if (files == null || files.Count <= 0)
      {
        companion.SubmitTextAsync(text);
      }
      else
      {
        companion.SubmitTextAsync(text, files);
      }
      await EndToEnd.WaitIdleAsync(companion, 120000);
      string answer = companion.ResponseText;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 3);
      defaultInterpolatedStringHandler.AppendFormatted(step);
      defaultInterpolatedStringHandler.AppendLiteral(": answer after ");
      defaultInterpolatedStringHandler.AppendFormatted<long>(sw.ElapsedMilliseconds);
      defaultInterpolatedStringHandler.AppendLiteral(" ms = ");
      defaultInterpolatedStringHandler.AppendFormatted(EndToEnd.Trim(answer, 500));
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      this.host.DispatcherQueue.TryEnqueue(delegate
      {
        this.host.Services.Presenter.Present(new NotchPresentation.Idle());
      });
      await Task.Delay(400);
      return answer;
    }

    // Token: 0x060024CB RID: 9419 RVA: 0x00637E00 File Offset: 0x00637E00
    [return: Nullable(2)]
    private static Process OpenBrowser(string url, string profile)
    {
      IEnumerable<string> enumerable = new string[] { "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe", "C:\\Program Files\\Microsoft\\Edge\\Application\\msedge.exe" };
      Func<string, bool> func;
      if ((func = EndToEnd.<>O.<0>__Exists) == null)
      {
        func = (EndToEnd.<>O.<0>__Exists = new Func<string, bool>(File.Exists));
      }
      string text = enumerable.FirstOrDefault<string>(func);
      if (text == null)
      {
        return null;
      }
      return Process.Start(new ProcessStartInfo(text)
      {
        ArgumentList = 
        {
          "--user-data-dir=" + profile,
          "--guest",
          "--no-first-run",
          "--no-default-browser-check",
          "--new-window",
          "--force-renderer-accessibility",
          url
        },
        UseShellExecute = false
      });
    }

    // Token: 0x060024CC RID: 9420 RVA: 0x00637ED0 File Offset: 0x00637ED0
    private static void CloseTree([Nullable(2)] Process process, string what)
    {
      if (process == null)
      {
        return;
      }
      try
      {
        process.Kill(true);
        EndToEnd.Say("closed " + what);
      }
      catch (Exception ex)
      {
        EndToEnd.Say("closing " + what + ": " + ex.Message);
      }
    }

    // Token: 0x060024CD RID: 9421 RVA: 0x00637F2C File Offset: 0x00637F2C
    private async Task PageAsync()
    {
      string text = Environment.GetEnvironmentVariable("HEYCLICKY_E2E_PAGE") ?? "https://en.wikipedia.org/wiki/Darjeeling_tea";
      string text2 = Path.Combine(Path.GetTempPath(), "heyclicky-e2e-edge-" + Environment.ProcessId.ToString());
      Process browser = EndToEnd.OpenBrowser(text, text2);
      if (browser == null)
      {
        EndToEnd.Say("page: no Edge on this PC");
      }
      else
      {
        try
        {
          await Task.Delay(6000);
          PageReference pageReference = await this.host.Real.Companion.PageReaderForTest.CaptureReferenceAsync(default(CancellationToken));
          EndToEnd.Say("page: reference = " + ((pageReference != null) ? pageReference.Kind : null) + " " + ((pageReference != null) ? pageReference.Reference : null));
          if (pageReference != null)
          {
            Stopwatch sw = Stopwatch.StartNew();
            PageContext pageContext = await this.host.Real.Companion.PageReaderForTest.ExtractAsync(pageReference, default(CancellationToken));
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 3);
            defaultInterpolatedStringHandler.AppendLiteral("page: read ");
            string text3 = pageContext.Text;
            defaultInterpolatedStringHandler.AppendFormatted<int>((text3 != null) ? text3.Length : 0);
            defaultInterpolatedStringHandler.AppendLiteral(" chars in ");
            defaultInterpolatedStringHandler.AppendFormatted<long>(sw.ElapsedMilliseconds);
            defaultInterpolatedStringHandler.AppendLiteral(" ms: ");
            defaultInterpolatedStringHandler.AppendFormatted(EndToEnd.Trim(pageContext.Text ?? "", 160));
            EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
            sw = null;
          }
          await this.AskAsync("page", "According to the article I have open, at what elevations is this tea grown? Answer from the article.", null, 1500);
        }
        finally
        {
          EndToEnd.CloseTree(browser, "the test browser");
        }
      }
    }

    // Token: 0x060024CE RID: 9422 RVA: 0x00637F70 File Offset: 0x00637F70
    private async Task DocumentAsync()
    {
      string text = Path.Combine(Path.GetTempPath(), "heyclicky-e2e-docs");
      Directory.CreateDirectory(text);
      string text2 = Path.Combine(text, "clicky-notes.txt");
      File.WriteAllText(text2, "Project notes\nThe code word for the launch is MARMALADE-7.\nShip date: November 3.");
      Process notepad = Process.Start(new ProcessStartInfo(text2)
      {
        UseShellExecute = true
      });
      try
      {
        await Task.Delay(3000);
        PageReference pageReference = await this.host.Real.Companion.PageReaderForTest.CaptureReferenceAsync(default(CancellationToken));
        EndToEnd.Say("document: reference = " + ((pageReference != null) ? pageReference.Kind : null) + " " + ((pageReference != null) ? pageReference.Reference : null));
        await this.AskAsync("document", "What's the code word in the document I have open?", null, 1500);
      }
      finally
      {
        IEnumerator<Process> enumerator = (from p in Process.GetProcessesByName("Notepad")
          where p.MainWindowTitle.Contains("clicky-notes", StringComparison.OrdinalIgnoreCase)
          select p).GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            Process process = enumerator.Current;
            EndToEnd.CloseTree(process, "Notepad");
          }
        }
        finally
        {
          int num;
          if (num < 0 && enumerator != null)
          {
            enumerator.Dispose();
          }
        }
        EndToEnd.CloseTree(notepad, "Notepad (launcher)");
      }
      string environmentVariable = Environment.GetEnvironmentVariable("HEYCLICKY_E2E_PDF");
      if (environmentVariable != null && environmentVariable.Length > 0 && File.Exists(environmentVariable))
      {
        string text3 = Path.Combine(Path.GetTempPath(), "heyclicky-e2e-edge-pdf-" + Environment.ProcessId.ToString());
        Process browser = EndToEnd.OpenBrowser(new Uri(environmentVariable).AbsoluteUri, text3);
        try
        {
          await Task.Delay(6000);
          PageReference pageReference2 = await this.host.Real.Companion.PageReaderForTest.CaptureReferenceAsync(default(CancellationToken));
          EndToEnd.Say("pdf: reference = " + ((pageReference2 != null) ? pageReference2.Kind : null) + " " + ((pageReference2 != null) ? pageReference2.Reference : null));
          await this.AskAsync("pdf", "According to the PDF I have open, what's the total on the invoice?", null, 1500);
        }
        finally
        {
          EndToEnd.CloseTree(browser, "the PDF browser");
        }
        browser = null;
      }
    }

    // Token: 0x060024CF RID: 9423 RVA: 0x00637FB4 File Offset: 0x00637FB4
    private async Task ToolsAsync()
    {
      await this.AskAsync("tools-shell", "Use a shell command to count the files in C:\\Windows\\System32\\drivers\\etc and tell me the number.", null, 1500);
      await this.AskAsync("tools-files", "List the folder C:\\Windows\\System32\\drivers\\etc with file_list and name two of the files.", null, 1500);
      await this.AskAsync("tools-calendar", "What's on my Google Calendar today?", null, 1500);
      await this.AskAsync("tools-spotify", "Play Never Gonna Give You Up by Rick Astley on Spotify.", null, 1500);
      await this.AskAsync("tools-copy", "Copy the word pineapple to my clipboard.", null, 1500);
      string text = await TextInsertion.ReadTextAsync();
      EndToEnd.Say("tools-copy: clipboard = \"" + text + "\"");
      await this.AskAsync("tools-home", "Open my agents dashboard.", null, 1500);
      await Task.Delay(1500);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 1);
      defaultInterpolatedStringHandler.AppendLiteral("tools-home: notch open = ");
      NotchPresenter notchPresenter = this.host.Services.Presenter as NotchPresenter;
      defaultInterpolatedStringHandler.AppendFormatted<bool?>((notchPresenter != null) ? new bool?(notchPresenter.IsShowingNotchAgentPeek) : null);
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      await this.AskAsync("tools-home-close", "Close the agents dashboard.", null, 1500);
      await Task.Delay(800);
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(31, 1);
      defaultInterpolatedStringHandler.AppendLiteral("tools-home-close: notch open = ");
      NotchPresenter notchPresenter2 = this.host.Services.Presenter as NotchPresenter;
      defaultInterpolatedStringHandler.AppendFormatted<bool?>((notchPresenter2 != null) ? new bool?(notchPresenter2.IsShowingNotchAgentPeek) : null);
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x060024D0 RID: 9424 RVA: 0x00637FF8 File Offset: 0x00637FF8
    private async Task BrainAgentAsync()
    {
      EndToEnd.<>c__DisplayClass13_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass13_0();
      RealStack real = this.host.Real;
      CS$<>8__locals1.spoken = new List<string>();
      real.Companion.ResponseTextUpdated += CS$<>8__locals1.<BrainAgentAsync>g__OnText|0;
      CS$<>8__locals1.tasksBefore = real.Agents.Tasks.Select<AgentTaskSnapshot, string>((AgentTaskSnapshot t) => t.TaskId).ToHashSet<string>();
      try
      {
        EndToEnd.<>c__DisplayClass13_1 CS$<>8__locals2 = new EndToEnd.<>c__DisplayClass13_1();
        await this.AskAsync("agent", "Start a Clicky to write a two-line poem about tea into a file named tea-poem.txt in its output folder.", null, 1500);
        CS$<>8__locals2.task = real.Agents.Tasks.FirstOrDefault<AgentTaskSnapshot>((AgentTaskSnapshot t) => !CS$<>8__locals1.tasksBefore.Contains(t.TaskId));
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 2);
        defaultInterpolatedStringHandler.AppendLiteral("agent: launched = ");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals2.task != null);
        defaultInterpolatedStringHandler.AppendLiteral(" (");
        AgentTaskSnapshot task = CS$<>8__locals2.task;
        defaultInterpolatedStringHandler.AppendFormatted((task != null) ? task.Title : null);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        if (CS$<>8__locals2.task != null)
        {
          Stopwatch sw = Stopwatch.StartNew();
          for (;;)
          {
            bool flag = sw.Elapsed < TimeSpan.FromMinutes(5L);
            if (flag)
            {
              IEnumerable<AgentTaskSnapshot> tasks = real.Agents.Tasks;
              Func<AgentTaskSnapshot, bool> func;
              if ((func = CS$<>8__locals2.<>9__3) == null)
              {
                EndToEnd.<>c__DisplayClass13_1 CS$<>8__locals3 = CS$<>8__locals2;
                Func<AgentTaskSnapshot, bool> func2 = (AgentTaskSnapshot t) => t.TaskId == CS$<>8__locals2.task.TaskId;
                CS$<>8__locals3.<>9__3 = func2;
                func = func2;
              }
              AgentTaskSnapshot agentTaskSnapshot = tasks.FirstOrDefault<AgentTaskSnapshot>(func);
              AgentTaskStatus? agentTaskStatus = ((agentTaskSnapshot != null) ? new AgentTaskStatus?(agentTaskSnapshot.Status) : null);
              flag = agentTaskStatus != null && agentTaskStatus.GetValueOrDefault() <= AgentTaskStatus.Running;
            }
            if (!flag)
            {
              break;
            }
            await Task.Delay(500);
          }
          AgentTaskDetail detail = real.Agents.GetTaskDetail(CS$<>8__locals2.task.TaskId);
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 3);
          defaultInterpolatedStringHandler.AppendLiteral("agent: ");
          AgentTaskDetail agentTaskDetail = detail;
          defaultInterpolatedStringHandler.AppendFormatted<AgentTaskStatus?>((agentTaskDetail != null) ? new AgentTaskStatus?(agentTaskDetail.Snapshot.Status) : null);
          defaultInterpolatedStringHandler.AppendLiteral(" after ");
          defaultInterpolatedStringHandler.AppendFormatted<double>(sw.Elapsed.TotalSeconds, "0");
          defaultInterpolatedStringHandler.AppendLiteral(" s: ");
          AgentTaskDetail agentTaskDetail2 = detail;
          string text;
          if ((text = ((agentTaskDetail2 != null) ? agentTaskDetail2.Summary : null)) == null)
          {
            AgentTaskDetail agentTaskDetail3 = detail;
            if ((text = ((agentTaskDetail3 != null) ? agentTaskDetail3.Snapshot.FinalResponseMarkdown : null)) == null)
            {
              AgentTaskDetail agentTaskDetail4 = detail;
              text = ((agentTaskDetail4 != null) ? agentTaskDetail4.FailureMessage : null) ?? "";
            }
          }
          defaultInterpolatedStringHandler.AppendFormatted(EndToEnd.Trim(text, 300));
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          Stopwatch spokenWait = Stopwatch.StartNew();
          List<string> list;
          while (spokenWait.Elapsed < TimeSpan.FromSeconds(60L))
          {
            list = CS$<>8__locals1.spoken;
            lock (list)
            {
              if (CS$<>8__locals1.spoken.Any<string>((string t) => t.Contains("is done", StringComparison.OrdinalIgnoreCase)))
              {
                break;
              }
            }
            await Task.Delay(250);
          }
          list = CS$<>8__locals1.spoken;
          lock (list)
          {
            EndToEnd.Say("agent: spoken finish = " + (CS$<>8__locals1.spoken.LastOrDefault<string>((string t) => t.Contains("is done", StringComparison.OrdinalIgnoreCase)) ?? "(none)"));
          }
          await EndToEnd.WaitIdleAsync(real.Companion, 60000);
          string text2 = "agent-memory";
          string text3 = "What did ";
          AgentTaskDetail agentTaskDetail5 = detail;
          await this.AskAsync(text2, text3 + (((agentTaskDetail5 != null) ? agentTaskDetail5.ClickyName : null) ?? "that Clicky") + " just make? Read me the file it wrote.", null, 1500);
          await this.AskAsync("agent-open", "Show me that file in File Explorer.", null, 1500);
          await Task.Delay(1500);
          foreach (Process process in from p in Process.GetProcessesByName("explorer")
            where p.MainWindowTitle.Contains("output", StringComparison.OrdinalIgnoreCase)
            select p)
          {
            EndToEnd.Say("agent-open: an Explorer window is open on \"" + process.MainWindowTitle + "\"");
          }
          CS$<>8__locals2 = null;
          sw = null;
          detail = null;
          spokenWait = null;
        }
      }
      finally
      {
        real.Companion.ResponseTextUpdated -= CS$<>8__locals1.<BrainAgentAsync>g__OnText|0;
      }
    }

    // Token: 0x060024D1 RID: 9425 RVA: 0x0063803C File Offset: 0x0063803C
    private async Task MutedAsync()
    {
      RealStack real = this.host.Real;
      string before = SystemAudioOutputVolume.CurrentSilenceKind();
      EndToEnd.Say("muted: speaker before = " + (before ?? "audible"));
      SystemAudioOutputVolume.SetMuted(true);
      try
      {
        EndToEnd.Say("muted: speaker now = " + (SystemAudioOutputVolume.CurrentSilenceKind() ?? "audible"));
        await this.VoiceAsync();
        string text = await TextInsertion.ReadTextAsync();
        EndToEnd.Say("muted: clipboard = " + EndToEnd.Trim(text ?? "", 200));
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 1);
        defaultInterpolatedStringHandler.AppendLiteral("muted: notch = ");
        defaultInterpolatedStringHandler.AppendFormatted<NotchPresentation>(this.host.Services.Presenter.Presentation);
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        await this.SnapNotchAsync("e2e-muted-card");
        bool flag = SystemAudioOutputVolume.RestoreAudibleOutput(0.5f);
        this.host.DispatcherQueue.TryEnqueue(delegate
        {
          this.host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.VoiceReplyMutedCopiedToClipboard));
        });
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 2);
        defaultInterpolatedStringHandler.AppendLiteral("muted: after the card's fix changed=");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
        defaultInterpolatedStringHandler.AppendLiteral(", speaker = ");
        defaultInterpolatedStringHandler.AppendFormatted(SystemAudioOutputVolume.CurrentSilenceKind() ?? "audible");
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      }
      finally
      {
        if (before != "muted")
        {
          SystemAudioOutputVolume.SetMuted(false);
        }
      }
    }

    // Token: 0x060024D2 RID: 9426 RVA: 0x00638080 File Offset: 0x00638080
    private async Task SnapNotchAsync(string name)
    {
      EndToEnd.<>c__DisplayClass15_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass15_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.name = name;
      CS$<>8__locals1.tcs = new TaskCompletionSource();
      this.host.DispatcherQueue.TryEnqueue(delegate
      {
        EndToEnd.<>c__DisplayClass15_0.<<SnapNotchAsync>b__0>d <<SnapNotchAsync>b__0>d;
        <<SnapNotchAsync>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
        <<SnapNotchAsync>b__0>d.<>4__this = CS$<>8__locals1;
        <<SnapNotchAsync>b__0>d.<>1__state = -1;
        <<SnapNotchAsync>b__0>d.<>t__builder.Start<EndToEnd.<>c__DisplayClass15_0.<<SnapNotchAsync>b__0>d>(ref <<SnapNotchAsync>b__0>d);
      });
      await CS$<>8__locals1.tcs.Task;
    }

    // Token: 0x060024D3 RID: 9427 RVA: 0x006380CC File Offset: 0x006380CC
    private async Task BundleAsync()
    {
      TaskAwaiter<bool> taskAwaiter = this.SignInAsync().GetAwaiter();
      if (!taskAwaiter.IsCompleted)
      {
        await taskAwaiter;
        TaskAwaiter<bool> taskAwaiter2;
        taskAwaiter = taskAwaiter2;
        taskAwaiter2 = default(TaskAwaiter<bool>);
      }
      if (taskAwaiter.GetResult())
      {
        HashSet<string> steps = (Environment.GetEnvironmentVariable("HEYCLICKY_E2E_STEPS") ?? "highlight,save,escape,yield,exit,idle,muted,mouth").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToHashSet<string>();
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(31, 2);
        defaultInterpolatedStringHandler.AppendLiteral("bundle: steps ");
        defaultInterpolatedStringHandler.AppendFormatted(string.Join(",", steps));
        defaultInterpolatedStringHandler.AppendLiteral("; audio topology ");
        defaultInterpolatedStringHandler.AppendFormatted<AudioTopologySnapshot>(AudioTopologyMonitor.Shared.Snapshot);
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        foreach (string step in new string[] { "highlight", "save", "escape", "yield", "exit", "idle", "muted", "mouth" })
        {
          if (steps.Contains(step))
          {
            try
            {
              string text = step;
              if (text != null)
              {
                switch (text.Length)
                {
                case 4:
                {
                  char c = text[0];
                  if (c != 'e')
                  {
                    if (c != 'i')
                    {
                      if (c == 's')
                      {
                        if (text == "save")
                        {
                          await this.SaveThatAsync();
                        }
                      }
                    }
                    else if (text == "idle")
                    {
                      await this.IdleFoundingAsync();
                    }
                  }
                  else if (text == "exit")
                  {
                    await this.WalkthroughPassiveExitAsync();
                  }
                  break;
                }
                case 5:
                {
                  char c = text[1];
                  if (c != 'i')
                  {
                    if (c != 'o')
                    {
                      if (c == 'u')
                      {
                        if (text == "muted")
                        {
                          await this.MutedExemptionAsync();
                        }
                      }
                    }
                    else if (text == "mouth")
                    {
                      await this.MouthIdleAsync();
                    }
                  }
                  else if (text == "yield")
                  {
                    await this.CaptureYieldAsync();
                  }
                  break;
                }
                case 6:
                  if (text == "escape")
                  {
                    await this.EscapeClearSoundAsync();
                  }
                  break;
                case 9:
                  if (text == "highlight")
                  {
                    await this.HighlightTextAsync();
                  }
                  break;
                }
              }
            }
            catch (Exception ex)
            {
              EndToEnd.Say("bundle: " + step + " FAILED: " + ex.Message);
              Log.Error("e2e", step, ex);
            }
            await Task.Delay(800);
            step = null;
          }
        }
        string[] array = null;
      }
    }

    // Token: 0x1700069D RID: 1693
    // (get) Token: 0x060024D4 RID: 9428 RVA: 0x00638110 File Offset: 0x00638110
    private string LogFile
    {
      get
      {
        string directory = Log.Directory;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 3);
        defaultInterpolatedStringHandler.AppendFormatted(Path.GetFileNameWithoutExtension(Environment.ProcessPath ?? "HeyClicky"));
        defaultInterpolatedStringHandler.AppendLiteral("-");
        defaultInterpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now, "yyyyMMdd");
        defaultInterpolatedStringHandler.AppendLiteral("-");
        defaultInterpolatedStringHandler.AppendFormatted<int>(Environment.ProcessId);
        defaultInterpolatedStringHandler.AppendLiteral(".log");
        return Path.Combine(directory, defaultInterpolatedStringHandler.ToStringAndClear());
      }
    }

    // Token: 0x060024D5 RID: 9429 RVA: 0x00638194 File Offset: 0x00638194
    private List<string> LogSince(int marker)
    {
      List<string> list;
      try
      {
        using (FileStream fileStream = new FileStream(this.LogFile, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Write | FileShare.Delete))
        {
          using (StreamReader streamReader = new StreamReader(fileStream))
          {
            list = (from l in streamReader.ReadToEnd().Split('\n', StringSplitOptions.None).Skip<string>(marker)
              select l.TrimEnd('\r')).ToList<string>();
          }
        }
      }
      catch
      {
        list = new List<string>();
      }
      return list;
    }

    // Token: 0x060024D6 RID: 9430 RVA: 0x00638240 File Offset: 0x00638240
    private int LogMark()
    {
      return this.LogSince(0).Count;
    }

    // Token: 0x060024D7 RID: 9431 RVA: 0x00638250 File Offset: 0x00638250
    private bool UseWav(string name)
    {
      string environmentVariable = Environment.GetEnvironmentVariable("HEYCLICKY_E2E_WAVS");
      string environmentVariable2 = Environment.GetEnvironmentVariable("HEYCLICKY_TEST_MIC_WAV");
      if (environmentVariable == null || environmentVariable2 == null || !File.Exists(Path.Combine(environmentVariable, name)))
      {
        EndToEnd.Say("bundle: no " + name + " (HEYCLICKY_E2E_WAVS / HEYCLICKY_TEST_MIC_WAV)");
        return false;
      }
      File.Copy(Path.Combine(environmentVariable, name), environmentVariable2, true);
      File.SetLastWriteTimeUtc(environmentVariable2, DateTime.UtcNow);
      return true;
    }

    // Token: 0x060024D8 RID: 9432 RVA: 0x006382B8 File Offset: 0x006382B8
    private double WavSeconds()
    {
      WavFileMicrophone wavFileMicrophone = this.host.Real.Microphone as WavFileMicrophone;
      if (wavFileMicrophone == null)
      {
        return 3.0;
      }
      return wavFileMicrophone.DurationSeconds;
    }

    // Token: 0x060024D9 RID: 9433 RVA: 0x006382F0 File Offset: 0x006382F0
    [return: TupleElementNames(new string[] { "Process", "Hwnd" })]
    [return: Nullable(new byte[] { 1, 0, 1 })]
    private async Task<ValueTuple<Process, IntPtr>?> LaunchNotepadAsync(string fileName, string text, [TupleElementNames(new string[] { "X", "Y", "W", "H" })] [Nullable(0)] ValueTuple<int, int, int, int> rect)
    {
      ValueTuple<Process, IntPtr>? valueTuple;
      if (Process.GetProcessesByName("Notepad").Any<Process>((Process p) => !this.launchedNotepads.Contains(p.Id)))
      {
        EndToEnd.Say("bundle: Notepad is already running (not ours): skipped so nothing opens in the user's window");
        valueTuple = null;
      }
      else
      {
        string text2 = Path.Combine(Path.GetTempPath(), "heyclicky-e2e-bundle");
        Directory.CreateDirectory(text2);
        string text3 = Path.Combine(text2, fileName);
        File.WriteAllText(text3, text);
        Process launcher = Process.Start(new ProcessStartInfo("notepad.exe", "\"" + text3 + "\"")
        {
          UseShellExecute = true
        });
        string stem = Path.GetFileNameWithoutExtension(fileName);
        for (int i = 0; i < 60; i++)
        {
          await Task.Delay(250);
          foreach (Process p2 in Process.GetProcessesByName("Notepad"))
          {
            p2.Refresh();
            if (p2.MainWindowHandle != 0 && p2.MainWindowTitle.Contains(stem, StringComparison.OrdinalIgnoreCase))
            {
              this.launchedNotepads.Add(p2.Id);
              EndToEnd.MoveWindow(p2.MainWindowHandle, rect.Item1, rect.Item2, rect.Item3, rect.Item4, true);
              EndToEnd.SetForegroundWindow(p2.MainWindowHandle);
              await Task.Delay(700);
              return new ValueTuple<Process, IntPtr>?(new ValueTuple<Process, IntPtr>(p2, p2.MainWindowHandle));
            }
            p2 = null;
          }
        }
        EndToEnd.CloseTree(launcher, "Notepad launcher");
        EndToEnd.Say("bundle: Notepad didn't open");
        valueTuple = null;
      }
      return valueTuple;
    }

    // Token: 0x060024DA RID: 9434 RVA: 0x0063834C File Offset: 0x0063834C
    private void CloseNotepads()
    {
      foreach (int num in this.launchedNotepads.ToList<int>())
      {
        try
        {
          using (Process processById = Process.GetProcessById(num))
          {
            processById.Kill(true);
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 1);
            defaultInterpolatedStringHandler.AppendLiteral("bundle: closed Notepad ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(num);
            EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          }
        }
        catch
        {
        }
        this.launchedNotepads.Remove(num);
      }
    }

    // Token: 0x060024DB RID: 9435 RVA: 0x0063840C File Offset: 0x0063840C
    private static async Task HoldPushToTalkAsync(double seconds, [Nullable(new byte[] { 2, 1 })] Func<Task> whileHeld = null)
    {
      EndToEnd.keybd_event(162, 0, 0U, (UIntPtr)((IntPtr)0));
      EndToEnd.keybd_event(164, 0, 0U, (UIntPtr)((IntPtr)0));
      Stopwatch held = Stopwatch.StartNew();
      try
      {
        if (whileHeld != null)
        {
          await whileHeld();
        }
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds) - held.Elapsed;
        if (timeSpan > TimeSpan.Zero)
        {
          await Task.Delay(timeSpan);
        }
      }
      finally
      {
        EndToEnd.keybd_event(164, 0, 2U, (UIntPtr)((IntPtr)0));
        EndToEnd.keybd_event(162, 0, 2U, (UIntPtr)((IntPtr)0));
      }
    }

    // Token: 0x060024DC RID: 9436 RVA: 0x00638458 File Offset: 0x00638458
    private static void PressKeys(params byte[] keys)
    {
      for (int i = 0; i < keys.Length; i++)
      {
        EndToEnd.keybd_event(keys[i], 0, 0U, (UIntPtr)((IntPtr)0));
      }
      Thread.Sleep(40);
      foreach (byte b in keys.Reverse<byte>())
      {
        EndToEnd.keybd_event(b, 0, 2U, (UIntPtr)((IntPtr)0));
      }
    }

    // Token: 0x060024DD RID: 9437 RVA: 0x006384CC File Offset: 0x006384CC
    private async Task<bool> TypedTurnAsync(string text, int idleTimeoutMs = 120000)
    {
      CompanionSession companion = this.host.Real.Companion;
      companion.SubmitTextAsync(text, new bool?(true));
      await Task.Delay(1200);
      await EndToEnd.WaitIdleAsync(companion, idleTimeoutMs);
      EndToEnd.Say("bundle: answer = " + EndToEnd.Trim(companion.ResponseText, 200));
      return true;
    }

    // Token: 0x060024DE RID: 9438
    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);

    // Token: 0x060024DF RID: 9439
    [DllImport("user32.dll")]
    private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int w, int h, bool repaint);

    // Token: 0x060024E0 RID: 9440
    [DllImport("dwmapi.dll")]
    private static extern int DwmGetWindowAttribute(IntPtr hwnd, int attribute, out int value, int size);

    // Token: 0x060024E1 RID: 9441 RVA: 0x00638520 File Offset: 0x00638520
    private static bool IsCloaked(IntPtr hwnd)
    {
      int num;
      return EndToEnd.DwmGetWindowAttribute(hwnd, 14, out num, 4) == 0 && num != 0;
    }

    // Token: 0x060024E2 RID: 9442 RVA: 0x00638540 File Offset: 0x00638540
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "X", "Y", "W", "H" })]
    private ValueTuple<int, int, int, int> LeftHalf()
    {
      Displays.Monitor monitor = Displays.ForWindow(this.host.NotchUi.Window.Hwnd);
      return new ValueTuple<int, int, int, int>(monitor.Work.Left + 40, monitor.Work.Top + 120, Math.Min(1100, monitor.Work.Width / 2), Math.Min(720, monitor.Work.Height - 200));
    }

    // Token: 0x060024E3 RID: 9443 RVA: 0x006385C8 File Offset: 0x006385C8
    private async Task HighlightTextAsync()
    {
      RealStack real = this.host.Real;
      if (this.UseWav("highlight.wav"))
      {
        ValueTuple<int, int, int, int> rect = this.LeftHalf();
        string text = string.Join("\n", Enumerable.Range(1, 14).Select<int, string>(delegate(int i)
        {
          if (i != 4)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(26, 1);
            defaultInterpolatedStringHandler2.AppendLiteral("Line ");
            defaultInterpolatedStringHandler2.AppendFormatted<int>(i);
            defaultInterpolatedStringHandler2.AppendLiteral(": nothing to see here");
            return defaultInterpolatedStringHandler2.ToStringAndClear();
          }
          return "Invoice 4719 is due on Friday the 24th";
        }));
        ValueTuple<Process, IntPtr>? valueTuple = await this.LaunchNotepadAsync("clicky-highlight.txt", text, rect);
        if (valueTuple != null)
        {
          ValueTuple<Process, IntPtr> valueOrDefault = valueTuple.GetValueOrDefault();
          try
          {
            EndToEnd.<>c__DisplayClass41_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass41_0();
            int mark = this.LogMark();
            CS$<>8__locals1.scale = Displays.ForWindow(valueOrDefault.Item2).Scale;
            CS$<>8__locals1.center = new PointD((double)rect.Item1 + 230.0 * CS$<>8__locals1.scale, (double)rect.Item2 + 174.6 * CS$<>8__locals1.scale);
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(72, 3);
            defaultInterpolatedStringHandler.AppendLiteral("bundle: highlight: holding Ctrl+Alt and circling ");
            defaultInterpolatedStringHandler.AppendFormatted<double>(CS$<>8__locals1.center.X, "0");
            defaultInterpolatedStringHandler.AppendLiteral(",");
            defaultInterpolatedStringHandler.AppendFormatted<double>(CS$<>8__locals1.center.Y, "0");
            defaultInterpolatedStringHandler.AppendLiteral(" over Notepad (");
            defaultInterpolatedStringHandler.AppendFormatted<double>(this.WavSeconds(), "0.0");
            defaultInterpolatedStringHandler.AppendLiteral(" s WAV)");
            EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
            await EndToEnd.HoldPushToTalkAsync(this.WavSeconds() + 0.8, delegate
            {
              EndToEnd.<>c__DisplayClass41_0.<<HighlightTextAsync>b__1>d <<HighlightTextAsync>b__1>d;
              <<HighlightTextAsync>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
              <<HighlightTextAsync>b__1>d.<>4__this = CS$<>8__locals1;
              <<HighlightTextAsync>b__1>d.<>1__state = -1;
              <<HighlightTextAsync>b__1>d.<>t__builder.Start<EndToEnd.<>c__DisplayClass41_0.<<HighlightTextAsync>b__1>d>(ref <<HighlightTextAsync>b__1>d);
              return <<HighlightTextAsync>b__1>d.<>t__builder.Task;
            });
            await Task.Delay(1500);
            await EndToEnd.WaitIdleAsync(real.Companion, 120000);
            string lastHighlightText = real.Companion.LastHighlightText;
            EndToEnd.Say("bundle: highlight: text under the ink = " + ((lastHighlightText == null) ? "(no highlight)" : ("\"" + EndToEnd.Trim(lastHighlightText, 400) + "\"")));
            EndToEnd.Say("bundle: highlight: " + (((lastHighlightText != null && lastHighlightText.Contains("4719")) || (lastHighlightText != null && lastHighlightText.Contains("Line "))) ? "PASS" : "FAIL") + " the harvest read Notepad's text");
            EndToEnd.Say("bundle: highlight: answer = " + EndToEnd.Trim(real.Companion.ResponseText, 200));
            foreach (string text2 in from l in this.LogSince(mark)
              where l.Contains("highlight text") || l.Contains("[Companion] turn context")
              select l)
            {
              EndToEnd.Say("  log: " + EndToEnd.Trim(text2, 300));
            }
          }
          finally
          {
            this.CloseNotepads();
          }
        }
      }
    }

    // Token: 0x060024E4 RID: 9444 RVA: 0x0063860C File Offset: 0x0063860C
    private async Task SaveThatAsync()
    {
      EndToEnd.<>c__DisplayClass42_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass42_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.real = this.host.Real;
      LessonService lessons = this.host.Lessons;
      if (lessons == null)
      {
        EndToEnd.Say("bundle: save: no lesson service");
      }
      else
      {
        CS$<>8__locals1.real.Core.Settings.Update(delegate(AppSettings s)
        {
          s.SaveDrawings = true;
        });
        int savedBefore = lessons.Store.List().Count;
        int recentBefore = lessons.RecentDrawings.Count;
        int mark = this.LogMark();
        CS$<>8__locals1.caption = "";
        CS$<>8__locals1.sampling = new CancellationTokenSource();
        try
        {
          Task sampler = Task.Run(delegate
          {
            EndToEnd.<>c__DisplayClass42_0.<<SaveThatAsync>b__1>d <<SaveThatAsync>b__1>d;
            <<SaveThatAsync>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
            <<SaveThatAsync>b__1>d.<>4__this = CS$<>8__locals1;
            <<SaveThatAsync>b__1>d.<>1__state = -1;
            <<SaveThatAsync>b__1>d.<>t__builder.Start<EndToEnd.<>c__DisplayClass42_0.<<SaveThatAsync>b__1>d>(ref <<SaveThatAsync>b__1>d);
            return <<SaveThatAsync>b__1>d.<>t__builder.Task;
          });
          await this.TypedTurnAsync("Explain how DNS turns a name into an address in three short spoken steps, drawn as a little diagram, and give it a lessonTitle.", 120000);
          CS$<>8__locals1.sampling.Cancel();
          await sampler;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(86, 2);
          defaultInterpolatedStringHandler.AppendLiteral("bundle: caption: ");
          defaultInterpolatedStringHandler.AppendFormatted((CS$<>8__locals1.caption.Length > 0) ? "PASS" : "FAIL");
          defaultInterpolatedStringHandler.AppendLiteral(" the debug subtitle showed under the drawing (\"");
          defaultInterpolatedStringHandler.AppendFormatted(EndToEnd.Trim(CS$<>8__locals1.caption, 120));
          defaultInterpolatedStringHandler.AppendLiteral("\"; bundle-caption.png)");
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          int i = 0;
          while (i < 20 && lessons.RecentDrawings.Count == recentBefore)
          {
            await Task.Delay(250);
            i++;
          }
          LessonService.Recent recent = lessons.RecentDrawings.LastOrDefault<LessonService.Recent>();
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 3);
          defaultInterpolatedStringHandler.AppendLiteral("bundle: save: drawing told = ");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(lessons.RecentDrawings.Count > recentBefore);
          defaultInterpolatedStringHandler.AppendLiteral(" (\"");
          defaultInterpolatedStringHandler.AppendFormatted((recent != null) ? recent.Record.Title : null);
          defaultInterpolatedStringHandler.AppendLiteral("\", id ");
          defaultInterpolatedStringHandler.AppendFormatted((recent != null) ? recent.Handle : null);
          defaultInterpolatedStringHandler.AppendLiteral(")");
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          if (this.UseWav("save.wav"))
          {
            await EndToEnd.HoldPushToTalkAsync(this.WavSeconds() + 0.8, null);
            await Task.Delay(1500);
            await EndToEnd.WaitIdleAsync(CS$<>8__locals1.real.Companion, 90000);
            int count = lessons.Store.List().Count;
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(75, 4);
            defaultInterpolatedStringHandler.AppendLiteral("bundle: save: ");
            defaultInterpolatedStringHandler.AppendFormatted((count > savedBefore) ? "PASS" : "FAIL");
            defaultInterpolatedStringHandler.AppendLiteral(" \"save that\" by voice kept it (");
            defaultInterpolatedStringHandler.AppendFormatted<int>(savedBefore);
            defaultInterpolatedStringHandler.AppendLiteral(" → ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(count);
            defaultInterpolatedStringHandler.AppendLiteral(" saved drawings); answer = ");
            defaultInterpolatedStringHandler.AppendFormatted(EndToEnd.Trim(CS$<>8__locals1.real.Companion.ResponseText, 200));
            EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
            foreach (string text in from l in this.LogSince(mark)
              where l.Contains("save_drawing") || l.Contains("[clicky drawing]") || l.Contains("[lessons]")
              select l)
            {
              EndToEnd.Say("  log: " + EndToEnd.Trim(text, 300));
            }
          }
        }
        finally
        {
          if (CS$<>8__locals1.sampling != null)
          {
            ((IDisposable)CS$<>8__locals1.sampling).Dispose();
          }
        }
      }
    }

    // Token: 0x060024E5 RID: 9445 RVA: 0x00638650 File Offset: 0x00638650
    private async Task EscapeClearSoundAsync()
    {
      RealStack real = this.host.Real;
      int mark = this.LogMark();
      await this.TypedTurnAsync("Circle the middle of my screen and say one short line about it.", 120000);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 1);
      defaultInterpolatedStringHandler.AppendLiteral("bundle: escape: drawing showing = ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(real.Canvas.IsShowingDrawing);
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      EndToEnd.PressKeys(new byte[] { 27 });
      await Task.Delay(800);
      bool flag = this.LogSince(mark).Any<string>((string l) => l.Contains("page-clear"));
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(93, 3);
      defaultInterpolatedStringHandler.AppendLiteral("bundle: escape: ");
      defaultInterpolatedStringHandler.AppendFormatted((flag && !real.Canvas.IsShowing) ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" a real Escape cleared it with the paper sweep (page-clear logged ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
      defaultInterpolatedStringHandler.AppendLiteral(", showing ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(real.Canvas.IsShowing);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x060024E6 RID: 9446 RVA: 0x00638694 File Offset: 0x00638694
    private async Task CaptureYieldAsync()
    {
      IntPtr notch = this.host.NotchUi.Window.Hwnd;
      int mark = this.LogMark();
      EndToEnd.Say("bundle: yield: pressing Win+Shift+S");
      EndToEnd.PressKeys(new byte[] { 91, 160, 83 });
      bool cloakedSeen = false;
      bool yieldingSeen = false;
      int i;
      for (i = 0; i < 30; i++)
      {
        await Task.Delay(100);
        cloakedSeen |= EndToEnd.IsCloaked(notch);
        yieldingSeen |= ScreenCaptureYield.IsYielding;
        if (i == 15)
        {
          Native.RECT bounds = Displays.ForWindow(notch).Bounds;
          await ScreenGrab.SaveAsync(Path.Combine(this.outDir, "bundle-yield-snip.png"), bounds.Left, bounds.Top, bounds.Width, bounds.Height / 3);
        }
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(65, 2);
      defaultInterpolatedStringHandler.AppendLiteral("bundle: yield: while the snip UI is up: yielding ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(yieldingSeen);
      defaultInterpolatedStringHandler.AppendLiteral(", notch cloaked ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(cloakedSeen);
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      EndToEnd.PressKeys(new byte[] { 27 });
      bool flag = false;
      i = 0;
      while (i < 40 && !flag)
      {
        await Task.Delay(100);
        flag = !ScreenCaptureYield.IsYielding && !EndToEnd.IsCloaked(notch);
        i++;
      }
      ValueTuple<bool, int> lastWatch = ScreenCaptureYield.LastWatch;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(83, 3);
      defaultInterpolatedStringHandler.AppendLiteral("bundle: yield: ");
      defaultInterpolatedStringHandler.AppendFormatted((yieldingSeen && cloakedSeen && flag) ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" the overlays stepped aside and came back (snip UI seen ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(lastWatch.Item1);
      defaultInterpolatedStringHandler.AppendLiteral(", aside ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(lastWatch.Item2);
      defaultInterpolatedStringHandler.AppendLiteral(" ms)");
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      foreach (string text in from l in this.LogSince(mark)
        where l.Contains("[CaptureYield]")
        select l)
      {
        EndToEnd.Say("  log: " + EndToEnd.Trim(text, 300));
      }
    }

    // Token: 0x060024E7 RID: 9447 RVA: 0x006386D8 File Offset: 0x006386D8
    private async Task WalkthroughPassiveExitAsync()
    {
      RealStack real = this.host.Real;
      CompanionSession companion = real.Companion;
      ValueTuple<int, int, int, int> valueTuple = this.LeftHalf();
      ValueTuple<Process, IntPtr>? valueTuple2 = await this.LaunchNotepadAsync("clicky-walkthrough.txt", "A short note.\nTurn on word wrap for me.", valueTuple);
      if (valueTuple2 != null)
      {
        ValueTuple<Process, IntPtr> valueOrDefault = valueTuple2.GetValueOrDefault();
        EndToEnd.SetForegroundWindow(valueOrDefault.Item2);
        await Task.Delay(800);
        int mark = this.LogMark();
        Process other = null;
        try
        {
          await this.TypedTurnAsync("In this Notepad window, walk me through turning on word wrap, one step at a time. Give me just the first step, with a click target on the View menu.", 120000);
          ClickTargetRing armedClickTarget = companion.ArmedClickTarget;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 2);
          defaultInterpolatedStringHandler.AppendLiteral("bundle: exit: ring armed = ");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(armedClickTarget != null);
          defaultInterpolatedStringHandler.AppendFormatted((armedClickTarget == null) ? "" : (" on \"" + armedClickTarget.Label + "\""));
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          if (armedClickTarget != null)
          {
            EndToEnd.SyntheticInput.Click(armedClickTarget.CenterPx);
            await Task.Delay(1700);
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(51, 1);
            defaultInterpolatedStringHandler.AppendLiteral("bundle: exit: state ");
            defaultInterpolatedStringHandler.AppendFormatted<VoiceState>(companion.State);
            defaultInterpolatedStringHandler.AppendLiteral(" when another app comes forward");
            EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
            other = Process.Start(new ProcessStartInfo("conhost.exe", "cmd.exe /k title HeyClicky-e2e-other")
            {
              UseShellExecute = true
            });
            await EndToEnd.WaitIdleAsync(companion, 60000);
            bool flag = this.LogSince(mark).Any<string>((string l) => l.Contains("passive exit"));
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(93, 5);
            defaultInterpolatedStringHandler.AppendLiteral("bundle: exit: ");
            defaultInterpolatedStringHandler.AppendFormatted((flag && companion.ArmedClickTarget == null && companion.State == VoiceState.Idle) ? "PASS" : "FAIL");
            defaultInterpolatedStringHandler.AppendLiteral(" the walkthrough ended quietly (passive exit logged ");
            defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
            defaultInterpolatedStringHandler.AppendLiteral(", ring ");
            defaultInterpolatedStringHandler.AppendFormatted<bool>(companion.ArmedClickTarget != null);
            defaultInterpolatedStringHandler.AppendLiteral(", state ");
            defaultInterpolatedStringHandler.AppendFormatted<VoiceState>(companion.State);
            defaultInterpolatedStringHandler.AppendLiteral(", answer \"");
            defaultInterpolatedStringHandler.AppendFormatted(EndToEnd.Trim(companion.ResponseText, 120));
            defaultInterpolatedStringHandler.AppendLiteral("\")");
            EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
            foreach (string text in from l in this.LogSince(mark)
              where l.Contains("guided") || l.Contains("passive")
              select l)
            {
              EndToEnd.Say("  log: " + EndToEnd.Trim(text, 300));
            }
          }
        }
        finally
        {
          EndToEnd.CloseTree(other, "the test console");
          this.CloseNotepads();
        }
      }
    }

    // Token: 0x060024E8 RID: 9448 RVA: 0x0063871C File Offset: 0x0063871C
    private async Task IdleFoundingAsync()
    {
      EndToEnd.<>c__DisplayClass46_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass46_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.before = ClickyRoster.Shared.Clickys.Select<RosterClicky, string>((RosterClicky c) => c.Slug).ToHashSet<string>();
      await this.TypedTurnAsync("Make me a Clicky that keeps my reading list tidy. Just make it, don't start any work.", 120000);
      CS$<>8__locals1.made = null;
      int i = 0;
      while (i < 60 && CS$<>8__locals1.made == null)
      {
        await Task.Delay(250);
        EndToEnd.<>c__DisplayClass46_0 CS$<>8__locals2 = CS$<>8__locals1;
        IEnumerable<RosterClicky> clickys = ClickyRoster.Shared.Clickys;
        Func<RosterClicky, bool> func;
        if ((func = CS$<>8__locals1.<>9__2) == null)
        {
          EndToEnd.<>c__DisplayClass46_0 CS$<>8__locals3 = CS$<>8__locals1;
          Func<RosterClicky, bool> func2 = (RosterClicky c) => !CS$<>8__locals1.before.Contains(c.Slug);
          CS$<>8__locals3.<>9__2 = func2;
          func = func2;
        }
        CS$<>8__locals2.made = clickys.FirstOrDefault<RosterClicky>(func);
        i++;
      }
      if (CS$<>8__locals1.made == null)
      {
        EndToEnd.Say("bundle: idle: FAIL no Clicky was founded");
      }
      else
      {
        IReadOnlyList<string> asks = Array.Empty<string>();
        for (i = 0; i < 80; i++)
        {
          IEnumerable<RosterClicky> clickys2 = ClickyRoster.Shared.Clickys;
          Func<RosterClicky, bool> func3;
          if ((func3 = CS$<>8__locals1.<>9__3) == null)
          {
            EndToEnd.<>c__DisplayClass46_0 CS$<>8__locals4 = CS$<>8__locals1;
            Func<RosterClicky, bool> func4 = (RosterClicky c) => c.Slug == CS$<>8__locals1.made.Slug;
            CS$<>8__locals4.<>9__3 = func4;
            func3 = func4;
          }
          RosterClicky rosterClicky = clickys2.FirstOrDefault<RosterClicky>(func3);
          asks = ((rosterClicky != null) ? rosterClicky.SuggestedAsks : null) ?? Array.Empty<string>();
          if (asks.Count >= 2)
          {
            break;
          }
          await Task.Delay(250);
        }
        bool flag = this.host.Real.Agents.Tasks.Any<AgentTaskSnapshot>(delegate(AgentTaskSnapshot t)
        {
          AgentTaskStatus status = t.Status;
          bool flag2 = status <= AgentTaskStatus.Running;
          if (flag2)
          {
            AgentTaskDetail taskDetail = CS$<>8__locals1.<>4__this.host.Real.Agents.GetTaskDetail(t.TaskId);
            return ((taskDetail != null) ? taskDetail.ClickySlug : null) == CS$<>8__locals1.made.Slug;
          }
          return false;
        });
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(60, 5);
        defaultInterpolatedStringHandler.AppendLiteral("bundle: idle: ");
        defaultInterpolatedStringHandler.AppendFormatted((asks.Count > 0 && !flag) ? "PASS" : "FAIL");
        defaultInterpolatedStringHandler.AppendLiteral(" \"");
        defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.made.Name);
        defaultInterpolatedStringHandler.AppendLiteral("\" founded idle (running ");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
        defaultInterpolatedStringHandler.AppendLiteral(") with ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(asks.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" first asks: ");
        defaultInterpolatedStringHandler.AppendFormatted(string.Join(" | ", asks.Select<string, string>((string a) => EndToEnd.Trim(a, 80))));
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      }
    }

    // Token: 0x060024E9 RID: 9449 RVA: 0x00638760 File Offset: 0x00638760
    private async Task MutedExemptionAsync()
    {
      EndToEnd.<>c__DisplayClass47_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass47_0();
      CS$<>8__locals1.<>4__this = this;
      RealStack real = this.host.Real;
      string environmentVariable = Environment.GetEnvironmentVariable("HEYCLICKY_TEST_SPEAKER_SILENT");
      if (environmentVariable == null || environmentVariable.Length <= 0)
      {
        EndToEnd.Say("bundle: muted: set HEYCLICKY_TEST_SPEAKER_SILENT=muted to run this");
      }
      else if (this.UseWav("hello.wav"))
      {
        Func<bool> previous = FlowHooks.MutedSpeakerReplyExempt;
        int mark = this.LogMark();
        CS$<>8__locals1.cardShown = false;
        this.host.Services.Presenter.PresentationChanged += CS$<>8__locals1.<MutedExemptionAsync>g__OnPresented|0;
        try
        {
          FlowHooks.MutedSpeakerReplyExempt = () => true;
          await EndToEnd.HoldPushToTalkAsync(this.WavSeconds() + 0.8, null);
          await Task.Delay(1500);
          await EndToEnd.WaitIdleAsync(real.Companion, 90000);
          bool flag = this.LogSince(mark).Any<string>((string l) => l.Contains("no clipboard card"));
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(114, 3);
          defaultInterpolatedStringHandler.AppendLiteral("bundle: muted: ");
          defaultInterpolatedStringHandler.AppendFormatted((flag && !CS$<>8__locals1.cardShown) ? "PASS" : "FAIL");
          defaultInterpolatedStringHandler.AppendLiteral(" a silent speaker during the introduction raised no clipboard card (exemption logged ");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
          defaultInterpolatedStringHandler.AppendLiteral(", card shown ");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.cardShown);
          defaultInterpolatedStringHandler.AppendLiteral(")");
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        }
        finally
        {
          FlowHooks.MutedSpeakerReplyExempt = previous;
          this.host.Services.Presenter.PresentationChanged -= CS$<>8__locals1.<MutedExemptionAsync>g__OnPresented|0;
        }
      }
    }

    // Token: 0x060024EA RID: 9450 RVA: 0x006387A4 File Offset: 0x006387A4
    private async Task MouthIdleAsync()
    {
      RealtimeMouth mouth = this.host.Real.Companion.Mouth;
      TimeSpan window = mouth.IdleTeardownWindow;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 2);
      defaultInterpolatedStringHandler.AppendLiteral("bundle: mouth: open ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(mouth.IsOpen);
      defaultInterpolatedStringHandler.AppendLiteral(", idle window ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(window.TotalSeconds, "0");
      defaultInterpolatedStringHandler.AppendLiteral(" s");
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      if (window > TimeSpan.FromSeconds(60L))
      {
        EndToEnd.Say("bundle: mouth: set HEYCLICKY_TEST_MOUTH_IDLE_SECONDS to run this");
      }
      else
      {
        if (!mouth.IsOpen)
        {
          await mouth.EnsureOpenAsync(default(CancellationToken));
        }
        Stopwatch sw = Stopwatch.StartNew();
        while (mouth.IsOpen && sw.Elapsed < window + TimeSpan.FromSeconds(15L))
        {
          await Task.Delay(250);
        }
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 2);
        defaultInterpolatedStringHandler.AppendLiteral("bundle: mouth: ");
        defaultInterpolatedStringHandler.AppendFormatted((!mouth.IsOpen) ? "PASS" : "FAIL");
        defaultInterpolatedStringHandler.AppendLiteral(" the mouth closed after ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(sw.Elapsed.TotalSeconds, "0.0");
        defaultInterpolatedStringHandler.AppendLiteral(" s idle");
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      }
    }

    // Token: 0x060024EB RID: 9451 RVA: 0x006387E8 File Offset: 0x006387E8
    private async Task MicAsync()
    {
      EndToEnd.<>c__DisplayClass49_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass49_0();
      CS$<>8__locals1.<>4__this = this;
      TaskAwaiter<bool> taskAwaiter = this.SignInAsync().GetAwaiter();
      if (!taskAwaiter.IsCompleted)
      {
        await taskAwaiter;
        TaskAwaiter<bool> taskAwaiter2;
        taskAwaiter = taskAwaiter2;
        taskAwaiter2 = default(TaskAwaiter<bool>);
      }
      if (taskAwaiter.GetResult())
      {
        WasapiMicrophoneCapture mic = this.host.Real.Microphone as WasapiMicrophoneCapture;
        if (mic == null)
        {
          EndToEnd.Say("mic: needs the real microphone (unset HEYCLICKY_TEST_MIC_WAV)");
        }
        else
        {
          int mark = this.LogMark();
          CS$<>8__locals1.card = null;
          this.host.Services.Presenter.PresentationChanged += CS$<>8__locals1.<MicAsync>g__OnPresented|0;
          try
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 4);
            defaultInterpolatedStringHandler.AppendLiteral("mic: device '");
            defaultInterpolatedStringHandler.AppendFormatted(mic.DeviceName);
            defaultInterpolatedStringHandler.AppendLiteral("' (");
            AudioEndpointInfo currentDevice = mic.CurrentDevice;
            defaultInterpolatedStringHandler.AppendFormatted((currentDevice != null) ? currentDevice.TransportCategory : null);
            defaultInterpolatedStringHandler.AppendLiteral("), bluetooth ");
            defaultInterpolatedStringHandler.AppendFormatted<bool>(mic.IsBluetoothDevice);
            defaultInterpolatedStringHandler.AppendLiteral("; topology ");
            defaultInterpolatedStringHandler.AppendFormatted<AudioTopologySnapshot>(AudioTopologyMonitor.Shared.Snapshot);
            EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
            await EndToEnd.HoldPushToTalkAsync(6.0, null);
            await Task.Delay(2500);
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(57, 3);
            defaultInterpolatedStringHandler.AppendLiteral("mic: after the press: failover pin ");
            defaultInterpolatedStringHandler.AppendFormatted((mic.FailoverDeviceId == null) ? "none" : "set");
            defaultInterpolatedStringHandler.AppendLiteral(", holding open ");
            defaultInterpolatedStringHandler.AppendFormatted<bool>(mic.IsHoldingDeviceOpen);
            defaultInterpolatedStringHandler.AppendLiteral(", card ");
            string text;
            if (CS$<>8__locals1.card != null)
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(23, 3);
              defaultInterpolatedStringHandler2.AppendLiteral("\"");
              defaultInterpolatedStringHandler2.AppendFormatted(CS$<>8__locals1.card.Message);
              defaultInterpolatedStringHandler2.AppendLiteral("\" / \"");
              defaultInterpolatedStringHandler2.AppendFormatted(CS$<>8__locals1.card.Subtitle);
              defaultInterpolatedStringHandler2.AppendLiteral("\" (Mic settings ");
              defaultInterpolatedStringHandler2.AppendFormatted<bool>(CS$<>8__locals1.card.ShowsMicrophoneSettingsAction);
              defaultInterpolatedStringHandler2.AppendLiteral(")");
              text = defaultInterpolatedStringHandler2.ToStringAndClear();
            }
            else
            {
              text = "none";
            }
            defaultInterpolatedStringHandler.AppendFormatted(text);
            EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
            if (mic.IsHoldingDeviceOpen)
            {
              Stopwatch sw = Stopwatch.StartNew();
              while (mic.IsHoldingDeviceOpen && sw.Elapsed < TimeSpan.FromSeconds(40L))
              {
                await Task.Delay(250);
              }
              defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(52, 2);
              defaultInterpolatedStringHandler.AppendLiteral("mic: ");
              defaultInterpolatedStringHandler.AppendFormatted((!mic.IsHoldingDeviceOpen) ? "PASS" : "FAIL");
              defaultInterpolatedStringHandler.AppendLiteral(" the held Bluetooth mic closed ");
              defaultInterpolatedStringHandler.AppendFormatted<double>(sw.Elapsed.TotalSeconds + 2.5, "0.0");
              defaultInterpolatedStringHandler.AppendLiteral(" s after release");
              EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
              sw = null;
            }
            foreach (string text2 in from l in this.LogSince(mark)
              where l.Contains("[Mic]") || l.Contains("mic ") || l.Contains("Bluetooth") || l.Contains("no audio")
              select l)
            {
              EndToEnd.Say("  log: " + EndToEnd.Trim(text2, 300));
            }
          }
          finally
          {
            this.host.Services.Presenter.PresentationChanged -= CS$<>8__locals1.<MicAsync>g__OnPresented|0;
          }
        }
      }
    }

    // Token: 0x060024EC RID: 9452 RVA: 0x0063882C File Offset: 0x0063882C
    public EndToEnd(AppHost host)
    {
      this.host = host;
      string environmentVariable = Environment.GetEnvironmentVariable("HEYCLICKY_E2E_OUT");
      this.outDir = ((environmentVariable != null && environmentVariable.Length > 0) ? environmentVariable : Path.Combine(Path.GetTempPath(), "heyclicky-e2e"));
      Directory.CreateDirectory(this.outDir);
      EndToEnd.sayFile = Path.Combine(this.outDir, "e2e.log");
    }

    // Token: 0x060024ED RID: 9453 RVA: 0x006388A4 File Offset: 0x006388A4
    private static void Say(string message)
    {
      Log.Info("e2e", message);
      try
      {
        if (EndToEnd.sayFile != null)
        {
          string text = EndToEnd.sayFile;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 3);
          defaultInterpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now, "HH:mm:ss.fff");
          defaultInterpolatedStringHandler.AppendLiteral(" ");
          defaultInterpolatedStringHandler.AppendFormatted(message);
          defaultInterpolatedStringHandler.AppendFormatted(Environment.NewLine);
          File.AppendAllText(text, defaultInterpolatedStringHandler.ToStringAndClear());
        }
      }
      catch
      {
      }
    }

    // Token: 0x060024EE RID: 9454 RVA: 0x00638924 File Offset: 0x00638924
    public async Task RunAsync(string scenario)
    {
      EndToEnd.Say("e2e runs only in Debug builds");
    }

    // Token: 0x060024EF RID: 9455 RVA: 0x00638960 File Offset: 0x00638960
    private async Task<bool> SignInAsync()
    {
      RealStack real = this.host.Real;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (real.Core.Auth.IsSignedIn)
      {
        string text = "signin: already signed in as ";
        UserProfile user = real.Core.Auth.User;
        EndToEnd.Say(text + ((user != null) ? user.Email : null));
      }
      else
      {
        Stopwatch sw = Stopwatch.StartNew();
        bool flag = await this.host.DevSignInAsync();
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 2);
        defaultInterpolatedStringHandler.AppendLiteral("signin: ");
        defaultInterpolatedStringHandler.AppendFormatted(flag ? "ok" : "FAILED");
        defaultInterpolatedStringHandler.AppendLiteral(" in ");
        defaultInterpolatedStringHandler.AppendFormatted<long>(sw.ElapsedMilliseconds);
        defaultInterpolatedStringHandler.AppendLiteral(" ms");
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        if (!flag)
        {
          return false;
        }
        sw = null;
      }
      PlanSnapshot planSnapshot = await real.Core.Worker.GetPlanAsync(default(CancellationToken));
      EndToEnd.Say("signin: /me/plan " + ((planSnapshot == null) ? "null" : "ok"));
      await real.Core.Worker.RegisterThisDeviceIfNeededAsync(default(CancellationToken));
      IReadOnlyList<RegisteredDevice> readOnlyList = await real.Core.Worker.GetDevicesAsync(default(CancellationToken));
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 2);
      defaultInterpolatedStringHandler.AppendLiteral("signin: devices registered = ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(readOnlyList.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" (");
      defaultInterpolatedStringHandler.AppendFormatted(string.Join(", ", readOnlyList.Select<RegisteredDevice, string>((RegisteredDevice d) => d.ToString())));
      defaultInterpolatedStringHandler.AppendLiteral(")");
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      return true;
    }

    // Token: 0x060024F0 RID: 9456 RVA: 0x006389A4 File Offset: 0x006389A4
    private async Task TypedAsync(string text)
    {
      EndToEnd.<>c__DisplayClass57_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass57_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.text = text;
      CS$<>8__locals1.real = this.host.Real;
      bool flag = !CS$<>8__locals1.real.Core.Auth.IsSignedIn;
      if (flag)
      {
        TaskAwaiter<bool> taskAwaiter = this.SignInAsync().GetAwaiter();
        if (!taskAwaiter.IsCompleted)
        {
          await taskAwaiter;
          TaskAwaiter<bool> taskAwaiter2;
          taskAwaiter = taskAwaiter2;
          taskAwaiter2 = default(TaskAwaiter<bool>);
        }
        flag = !taskAwaiter.GetResult();
      }
      if (!flag)
      {
        CompanionSession companion = CS$<>8__locals1.real.Companion;
        CS$<>8__locals1.firstText = new TaskCompletionSource<long>();
        CS$<>8__locals1.sw = Stopwatch.StartNew();
        companion.ResponseTextUpdated += CS$<>8__locals1.<TypedAsync>g__OnText|0;
        this.host.DispatcherQueue.TryEnqueue(delegate
        {
          CS$<>8__locals1.<>4__this.host.Services.Presenter.Present(new NotchPresentation.TextInput(CS$<>8__locals1.text, null));
        });
        await Task.Delay(700);
        CS$<>8__locals1.sw.Restart();
        EndToEnd.Say("typed: submit \"" + CS$<>8__locals1.text + "\"");
        companion.SubmitTextAsync(CS$<>8__locals1.text);
        CS$<>8__locals1.cursorWatch = new CancellationTokenSource();
        try
        {
          CS$<>8__locals1.cursorModes = new List<string>();
          CS$<>8__locals1.cursorPositions = new List<PointD>();
          Task watcher = Task.Run(delegate
          {
            EndToEnd.<>c__DisplayClass57_0.<<TypedAsync>b__2>d <<TypedAsync>b__2>d;
            <<TypedAsync>b__2>d.<>t__builder = AsyncTaskMethodBuilder.Create();
            <<TypedAsync>b__2>d.<>4__this = CS$<>8__locals1;
            <<TypedAsync>b__2>d.<>1__state = -1;
            <<TypedAsync>b__2>d.<>t__builder.Start<EndToEnd.<>c__DisplayClass57_0.<<TypedAsync>b__2>d>(ref <<TypedAsync>b__2>d);
            return <<TypedAsync>b__2>d.<>t__builder.Task;
          });
          TaskAwaiter<Task> taskAwaiter3 = Task.WhenAny(CS$<>8__locals1.firstText.Task, Task.Delay(60000)).GetAwaiter();
          if (!taskAwaiter3.IsCompleted)
          {
            await taskAwaiter3;
            TaskAwaiter<Task> taskAwaiter4;
            taskAwaiter3 = taskAwaiter4;
            taskAwaiter4 = default(TaskAwaiter<Task>);
          }
          long num = ((taskAwaiter3.GetResult() == CS$<>8__locals1.firstText.Task) ? CS$<>8__locals1.firstText.Task.Result : (-1L));
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
          defaultInterpolatedStringHandler.AppendLiteral("typed: first text after ");
          defaultInterpolatedStringHandler.AppendFormatted<long>(num);
          defaultInterpolatedStringHandler.AppendLiteral(" ms");
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          await EndToEnd.WaitIdleAsync(companion, 90000);
          await Task.Delay(1500);
          CS$<>8__locals1.cursorWatch.Cancel();
          await watcher;
          double num2 = CS$<>8__locals1.cursorPositions.Zip<PointD, PointD, double>(CS$<>8__locals1.cursorPositions.Skip<PointD>(1), (PointD a, PointD b) => Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y))).Sum();
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(52, 3);
          defaultInterpolatedStringHandler.AppendLiteral("typed: cursor modes ");
          defaultInterpolatedStringHandler.AppendFormatted(string.Join(" â†’ ", CS$<>8__locals1.cursorModes));
          defaultInterpolatedStringHandler.AppendLiteral("; riding samples ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.cursorPositions.Count);
          defaultInterpolatedStringHandler.AppendLiteral(", travelled ");
          defaultInterpolatedStringHandler.AppendFormatted<double>(num2, "0");
          defaultInterpolatedStringHandler.AppendLiteral(" px");
          string text2 = defaultInterpolatedStringHandler.ToStringAndClear();
          string text3;
          if (CS$<>8__locals1.cursorPositions.Count <= 0)
          {
            text3 = "";
          }
          else
          {
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 2);
            defaultInterpolatedStringHandler.AppendLiteral(", from ");
            defaultInterpolatedStringHandler.AppendFormatted<PointD>(CS$<>8__locals1.cursorPositions[0]);
            defaultInterpolatedStringHandler.AppendLiteral(" to ");
            List<PointD> cursorPositions = CS$<>8__locals1.cursorPositions;
            defaultInterpolatedStringHandler.AppendFormatted<PointD>(cursorPositions[cursorPositions.Count - 1]);
            text3 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          EndToEnd.Say(text2 + text3);
          companion.ResponseTextUpdated -= CS$<>8__locals1.<TypedAsync>g__OnText|0;
          EndToEnd.Say("typed: answer = " + EndToEnd.Trim(companion.ResponseText, 200));
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(31, 2);
          defaultInterpolatedStringHandler.AppendLiteral("typed: canvas showing=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.real.Canvas.IsShowing);
          defaultInterpolatedStringHandler.AppendLiteral(" drawing=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.real.Canvas.IsShowingDrawing);
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          await this.SaveArtifactsAsync("typed");
        }
        finally
        {
          if (CS$<>8__locals1.cursorWatch != null)
          {
            ((IDisposable)CS$<>8__locals1.cursorWatch).Dispose();
          }
        }
      }
    }

    // Token: 0x060024F1 RID: 9457 RVA: 0x006389F0 File Offset: 0x006389F0
    private async Task ContinueDrawingAsync()
    {
      RealStack real = this.host.Real;
      await this.TypedAsync("Draw a box around the top-left quarter of my screen and label it A.");
      string text = real.Canvas.DrawingStateForModel() ?? "";
      EndToEnd.Say("continue: before = " + EndToEnd.Trim(text, 300));
      await this.TypedAsync("Now add a second box around the bottom-right quarter labelled B, keep the first one.");
      string text2 = real.Canvas.DrawingStateForModel() ?? "";
      EndToEnd.Say("continue: after = " + EndToEnd.Trim(text2, 400));
      int count = Regex.Matches(text2, "\\bTurn \\d+").Count;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(60, 2);
      defaultInterpolatedStringHandler.AppendLiteral("continue: built on the first drawing = ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(count >= 2);
      defaultInterpolatedStringHandler.AppendLiteral(" (");
      defaultInterpolatedStringHandler.AppendFormatted<int>(count);
      defaultInterpolatedStringHandler.AppendLiteral(" turns on the card)");
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x060024F2 RID: 9458 RVA: 0x00638A34 File Offset: 0x00638A34
    private async Task StaleDrawingAsync()
    {
      RealStack real = this.host.Real;
      await this.TypedAsync("Circle the start button on my taskbar.");
      await Task.Delay(2000);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 1);
      defaultInterpolatedStringHandler.AppendLiteral("stale: after 2 s of nothing, drawing showing = ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(real.Canvas.IsShowingDrawing);
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      using (Process other = Process.Start(new ProcessStartInfo("conhost.exe", "cmd.exe /k title HeyClicky-e2e-other")
      {
        UseShellExecute = true
      }))
      {
        Stopwatch deadline = Stopwatch.StartNew();
        while (real.Canvas.IsShowing && deadline.ElapsedMilliseconds < 4000L)
        {
          await Task.Delay(100);
        }
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(57, 2);
        defaultInterpolatedStringHandler.AppendLiteral("stale: another app in front, drawing showing = ");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(real.Canvas.IsShowing);
        defaultInterpolatedStringHandler.AppendLiteral(" after ");
        defaultInterpolatedStringHandler.AppendFormatted<long>(deadline.ElapsedMilliseconds);
        defaultInterpolatedStringHandler.AppendLiteral(" ms");
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        try
        {
          Process process = other;
          if (process != null)
          {
            process.Kill(true);
          }
        }
        catch (Exception ex)
        {
          EndToEnd.Say("stale: closing the console failed: " + ex.Message);
        }
      }
    }

    // Token: 0x060024F3 RID: 9459 RVA: 0x00638A78 File Offset: 0x00638A78
    private async Task WalkthroughAsync()
    {
      EndToEnd.<>c__DisplayClass60_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass60_0();
      RealStack real = this.host.Real;
      CompanionSession companion = real.Companion;
      CS$<>8__locals1.turns = new List<ValueTuple<string, int?>>();
      companion.TurnStarted += CS$<>8__locals1.<WalkthroughAsync>g__OnTurn|0;
      try
      {
        await this.TypedAsync(Environment.GetEnvironmentVariable("HEYCLICKY_E2E_TEXT") ?? "Walk me through opening Windows Settings, one step at a time. Give me just the first step, with a click target on the Start button on my taskbar.");
        ClickTargetRing ring = companion.ArmedClickTarget;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 1);
        defaultInterpolatedStringHandler.AppendLiteral("walkthrough: ring armed = ");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(ring != null);
        string text = defaultInterpolatedStringHandler.ToStringAndClear();
        string text3;
        if (ring != null)
        {
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 5);
          defaultInterpolatedStringHandler.AppendLiteral(" at ");
          defaultInterpolatedStringHandler.AppendFormatted<double>(ring.CenterPx.X, "0");
          defaultInterpolatedStringHandler.AppendLiteral(",");
          defaultInterpolatedStringHandler.AppendFormatted<double>(ring.CenterPx.Y, "0");
          defaultInterpolatedStringHandler.AppendLiteral(" radius ");
          defaultInterpolatedStringHandler.AppendFormatted<double>(ring.RadiusPx, "0");
          defaultInterpolatedStringHandler.AppendLiteral(" px, control ");
          RectD? controlFramePx = ring.ControlFramePx;
          string text2;
          if (controlFramePx != null)
          {
            RectD valueOrDefault = controlFramePx.GetValueOrDefault();
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(3, 4);
            defaultInterpolatedStringHandler2.AppendFormatted<double>(valueOrDefault.X, "0");
            defaultInterpolatedStringHandler2.AppendLiteral(",");
            defaultInterpolatedStringHandler2.AppendFormatted<double>(valueOrDefault.Y, "0");
            defaultInterpolatedStringHandler2.AppendLiteral(" ");
            defaultInterpolatedStringHandler2.AppendFormatted<double>(valueOrDefault.Width, "0");
            defaultInterpolatedStringHandler2.AppendLiteral("x");
            defaultInterpolatedStringHandler2.AppendFormatted<double>(valueOrDefault.Height, "0");
            text2 = defaultInterpolatedStringHandler2.ToStringAndClear();
          }
          else
          {
            text2 = "none";
          }
          defaultInterpolatedStringHandler.AppendFormatted(text2);
          defaultInterpolatedStringHandler.AppendLiteral(", label \"");
          defaultInterpolatedStringHandler.AppendFormatted(ring.Label);
          defaultInterpolatedStringHandler.AppendLiteral("\"");
          text3 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        else
        {
          text3 = "";
        }
        EndToEnd.Say(text + text3);
        if (ring != null)
        {
          CursorOverlay inner = real.Cursor.Inner;
          if (inner != null)
          {
            ValueTuple<ClickTargetRing, double, bool> clickTargetDiagnostics = inner.GetClickTargetDiagnostics();
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(55, 3);
            defaultInterpolatedStringHandler.AppendLiteral("walkthrough: overlay ring shown = ");
            defaultInterpolatedStringHandler.AppendFormatted<bool>(clickTargetDiagnostics.Item1 != null);
            defaultInterpolatedStringHandler.AppendLiteral(", reveal ");
            defaultInterpolatedStringHandler.AppendFormatted<double>(clickTargetDiagnostics.Item2, "0.00");
            defaultInterpolatedStringHandler.AppendLiteral(", pulsing = ");
            defaultInterpolatedStringHandler.AppendFormatted<bool>(clickTargetDiagnostics.Item3);
            EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
            string text4 = Path.Combine(this.outDir, "e2e-walkthrough-ring.png");
            inner.SnapshotClickTargetPng(text4, "#F5F5F0");
            EndToEnd.Say("walkthrough: ring PNG â†’ " + text4);
          }
          Stopwatch clicked = Stopwatch.StartNew();
          bool realClick = EndToEnd.SyntheticInput.Click(ring.CenterPx);
          while (realClick && !CS$<>8__locals1.<WalkthroughAsync>g__HasFollowUp|1() && clicked.ElapsedMilliseconds < 1500L)
          {
            await Task.Delay(50);
          }
          string how = "a real click through the hook";
          if (!CS$<>8__locals1.<WalkthroughAsync>g__HasFollowUp|1())
          {
            how = "a press at the hook's entry point (synthetic input didn't arrive)";
            companion.SimulatePressForTest(ring.CenterPx);
          }
          while (!CS$<>8__locals1.<WalkthroughAsync>g__HasFollowUp|1() && clicked.ElapsedMilliseconds < 15000L)
          {
            await Task.Delay(50);
          }
          List<ValueTuple<string, int?>> turns = CS$<>8__locals1.turns;
          ValueTuple<string, int?> valueTuple;
          lock (turns)
          {
            valueTuple = CS$<>8__locals1.turns.LastOrDefault<ValueTuple<string, int?>>(([TupleElementNames(new string[] { "Trigger", "Step" })] ValueTuple<string, int?> t) => t.Item1 == "clicked_target");
          }
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(81, 5);
          defaultInterpolatedStringHandler.AppendLiteral("walkthrough: clicked by ");
          defaultInterpolatedStringHandler.AppendFormatted(how);
          defaultInterpolatedStringHandler.AppendLiteral("; follow-up turn = ");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.<WalkthroughAsync>g__HasFollowUp|1());
          defaultInterpolatedStringHandler.AppendLiteral(" (trigger ");
          defaultInterpolatedStringHandler.AppendFormatted(valueTuple.Item1);
          defaultInterpolatedStringHandler.AppendLiteral(", step ");
          defaultInterpolatedStringHandler.AppendFormatted<int?>(valueTuple.Item2);
          defaultInterpolatedStringHandler.AppendLiteral(") ");
          defaultInterpolatedStringHandler.AppendFormatted<long>(clicked.ElapsedMilliseconds);
          defaultInterpolatedStringHandler.AppendLiteral(" ms after the click");
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          await EndToEnd.WaitIdleAsync(companion, 90000);
          EndToEnd.Say("walkthrough: step 1 answer = " + EndToEnd.Trim(companion.ResponseText, 200));
          ClickTargetRing next = companion.ArmedClickTarget;
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 1);
          defaultInterpolatedStringHandler.AppendLiteral("walkthrough: step 1 ring armed = ");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(next != null);
          string text5 = defaultInterpolatedStringHandler.ToStringAndClear();
          string text6;
          if (next != null)
          {
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 3);
            defaultInterpolatedStringHandler.AppendLiteral(" at ");
            defaultInterpolatedStringHandler.AppendFormatted<double>(next.CenterPx.X, "0");
            defaultInterpolatedStringHandler.AppendLiteral(",");
            defaultInterpolatedStringHandler.AppendFormatted<double>(next.CenterPx.Y, "0");
            defaultInterpolatedStringHandler.AppendLiteral(", label \"");
            defaultInterpolatedStringHandler.AppendFormatted(next.Label);
            defaultInterpolatedStringHandler.AppendLiteral("\"");
            text6 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          else
          {
            text6 = "";
          }
          EndToEnd.Say(text5 + text6);
          await this.SaveArtifactsAsync("walkthrough");
          if (next != null)
          {
            companion.SimulatePressForTest(new PointD(next.CenterPx.X + next.RadiusPx + 400.0, next.CenterPx.Y + next.RadiusPx + 300.0));
            await Task.Delay(300);
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(51, 1);
            defaultInterpolatedStringHandler.AppendLiteral("walkthrough: after a click elsewhere, ring armed = ");
            defaultInterpolatedStringHandler.AppendFormatted<bool>(companion.ArmedClickTarget != null);
            EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          }
          if (realClick)
          {
            EndToEnd.SyntheticInput.PressEscape();
          }
          ring = null;
          clicked = null;
          how = null;
          next = null;
        }
      }
      finally
      {
        companion.TurnStarted -= CS$<>8__locals1.<WalkthroughAsync>g__OnTurn|0;
      }
    }

    // Token: 0x060024F4 RID: 9460 RVA: 0x00638ABC File Offset: 0x00638ABC
    private async Task VoiceAsync()
    {
      RealStack real = this.host.Real;
      bool flag = !real.Core.Auth.IsSignedIn;
      if (flag)
      {
        TaskAwaiter<bool> taskAwaiter = this.SignInAsync().GetAwaiter();
        if (!taskAwaiter.IsCompleted)
        {
          await taskAwaiter;
          TaskAwaiter<bool> taskAwaiter2;
          taskAwaiter = taskAwaiter2;
          taskAwaiter2 = default(TaskAwaiter<bool>);
        }
        flag = !taskAwaiter.GetResult();
      }
      if (!flag)
      {
        if (!(real.Microphone is WavFileMicrophone))
        {
          EndToEnd.Say("voice: set HEYCLICKY_TEST_MIC_WAV to a WAV to run this");
        }
        else
        {
          int num;
          int repeat = (int.TryParse(Environment.GetEnvironmentVariable("HEYCLICKY_E2E_REPEAT"), out num) ? Math.Clamp(num, 1, 20) : 1);
          for (int press = 1; press <= repeat; press++)
          {
            if (repeat > 1)
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 2);
              defaultInterpolatedStringHandler.AppendLiteral("voice: press ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(press);
              defaultInterpolatedStringHandler.AppendLiteral(" of ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(repeat);
              EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
            }
            await this.VoicePressAsync(real);
            if (press < repeat)
            {
              await Task.Delay(2000);
            }
          }
          await this.SaveArtifactsAsync("voice");
        }
      }
    }

    // Token: 0x060024F5 RID: 9461 RVA: 0x00638B00 File Offset: 0x00638B00
    private async Task VoicePressAsync(RealStack real)
    {
      EndToEnd.<>c__DisplayClass63_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass63_0();
      WavFileMicrophone wav = (WavFileMicrophone)real.Microphone;
      CompanionSession companion = real.Companion;
      CS$<>8__locals1.transcript = new TaskCompletionSource<long>();
      CS$<>8__locals1.firstAudio = new TaskCompletionSource<long>();
      CS$<>8__locals1.lastTranscript = "";
      CS$<>8__locals1.sw = Stopwatch.StartNew();
      CS$<>8__locals1.audioSeconds = 0.0;
      CS$<>8__locals1.linesQueued = 0;
      companion.UserTranscriptUpdated += CS$<>8__locals1.<VoicePressAsync>g__OnTranscript|0;
      companion.Mouth.LineAudioQueued += CS$<>8__locals1.<VoicePressAsync>g__OnAudio|1;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 1);
      defaultInterpolatedStringHandler.AppendLiteral("voice: press (WAV ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(wav.DurationSeconds, "0.00");
      defaultInterpolatedStringHandler.AppendLiteral(" s)");
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      await companion.BeginPushToTalkAsync();
      await Task.Delay(TimeSpan.FromSeconds(wav.DurationSeconds + 0.6));
      CS$<>8__locals1.sw.Restart();
      await companion.EndPushToTalkAsync();
      long t = ((await Task.WhenAny(CS$<>8__locals1.transcript.Task, Task.Delay(30000)) == CS$<>8__locals1.transcript.Task) ? CS$<>8__locals1.transcript.Task.Result : (-1L));
      long t2 = ((await Task.WhenAny(CS$<>8__locals1.firstAudio.Task, Task.Delay(60000)) == CS$<>8__locals1.firstAudio.Task) ? CS$<>8__locals1.firstAudio.Task.Result : (-1L));
      await EndToEnd.WaitIdleAsync(companion, 90000);
      companion.UserTranscriptUpdated -= CS$<>8__locals1.<VoicePressAsync>g__OnTranscript|0;
      companion.Mouth.LineAudioQueued -= CS$<>8__locals1.<VoicePressAsync>g__OnAudio|1;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(92, 2);
      defaultInterpolatedStringHandler.AppendLiteral("voice: transcript = \"");
      defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.lastTranscript);
      defaultInterpolatedStringHandler.AppendLiteral("\" (");
      defaultInterpolatedStringHandler.AppendFormatted<long>(t);
      defaultInterpolatedStringHandler.AppendLiteral(" ms after key-up; final transcript events may arrive before release)");
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(101, 5);
      defaultInterpolatedStringHandler.AppendLiteral("voice: first audio queued ");
      defaultInterpolatedStringHandler.AppendFormatted<long>(t2);
      defaultInterpolatedStringHandler.AppendLiteral(" ms after key-up; lines with audio ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.linesQueued);
      defaultInterpolatedStringHandler.AppendLiteral(", audio ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(CS$<>8__locals1.audioSeconds, "0.00");
      defaultInterpolatedStringHandler.AppendLiteral(" s, mouth enqueued ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(companion.Mouth.EnqueuedSecondsSinceFlush, "0.00");
      defaultInterpolatedStringHandler.AppendLiteral(" s, played ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(real.Audio.PlayedSeconds, "0.00");
      defaultInterpolatedStringHandler.AppendLiteral(" s");
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      EndToEnd.Say("voice: answer = " + EndToEnd.Trim(companion.ResponseText, 200));
    }

    // Token: 0x060024F6 RID: 9462 RVA: 0x00638B44 File Offset: 0x00638B44
    private async Task AgentAsync()
    {
      EndToEnd.<>c__DisplayClass64_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass64_0();
      CS$<>8__locals1.<>4__this = this;
      RealStack real = this.host.Real;
      bool flag = !real.Core.Auth.IsSignedIn;
      if (flag)
      {
        TaskAwaiter<bool> taskAwaiter = this.SignInAsync().GetAwaiter();
        if (!taskAwaiter.IsCompleted)
        {
          await taskAwaiter;
          TaskAwaiter<bool> taskAwaiter2;
          taskAwaiter = taskAwaiter2;
          taskAwaiter2 = default(TaskAwaiter<bool>);
        }
        flag = !taskAwaiter.GetResult();
      }
      if (!flag)
      {
        AgentService agents = real.Agents;
        CS$<>8__locals1.sw = Stopwatch.StartNew();
        this.host.DispatcherQueue.TryEnqueue(delegate
        {
          CS$<>8__locals1.<>4__this.host.ShowInChat(null, null);
        });
        string text = Environment.GetEnvironmentVariable("HEYCLICKY_E2E_AGENT") ?? "Create a text file named hello-windows.txt in your output folder that says 'Hello from HeyClicky on Windows', then reply done.";
        CS$<>8__locals1.id = await agents.LaunchAsync(text, null, default(CancellationToken));
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 2);
        defaultInterpolatedStringHandler.AppendLiteral("agent: launched ");
        defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.id);
        defaultInterpolatedStringHandler.AppendLiteral(" in ");
        defaultInterpolatedStringHandler.AppendFormatted<long>(CS$<>8__locals1.sw.ElapsedMilliseconds);
        defaultInterpolatedStringHandler.AppendLiteral(" ms");
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        AgentTaskSnapshot snap = null;
        while (CS$<>8__locals1.sw.Elapsed < TimeSpan.FromMinutes(4L))
        {
          snap = agents.Tasks.FirstOrDefault<AgentTaskSnapshot>((AgentTaskSnapshot t) => t.TaskId == CS$<>8__locals1.id);
          AgentTaskSnapshot agentTaskSnapshot = snap;
          AgentTaskStatus? agentTaskStatus = ((agentTaskSnapshot != null) ? new AgentTaskStatus?(agentTaskSnapshot.Status) : null);
          if (agentTaskStatus != null && agentTaskStatus.GetValueOrDefault() - AgentTaskStatus.Finished <= 2)
          {
            break;
          }
          await Task.Delay(500);
        }
        AgentTaskDetail taskDetail = agents.GetTaskDetail(CS$<>8__locals1.id);
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 4);
        defaultInterpolatedStringHandler.AppendLiteral("agent: ");
        AgentTaskSnapshot agentTaskSnapshot2 = snap;
        defaultInterpolatedStringHandler.AppendFormatted<AgentTaskStatus?>((agentTaskSnapshot2 != null) ? new AgentTaskStatus?(agentTaskSnapshot2.Status) : null);
        defaultInterpolatedStringHandler.AppendLiteral(" after ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(CS$<>8__locals1.sw.Elapsed.TotalSeconds, "0.0");
        defaultInterpolatedStringHandler.AppendLiteral(" s; title \"");
        AgentTaskSnapshot agentTaskSnapshot3 = snap;
        defaultInterpolatedStringHandler.AppendFormatted((agentTaskSnapshot3 != null) ? agentTaskSnapshot3.Title : null);
        defaultInterpolatedStringHandler.AppendLiteral("\"; reply ");
        AgentTaskSnapshot agentTaskSnapshot4 = snap;
        string text2;
        if ((text2 = ((agentTaskSnapshot4 != null) ? agentTaskSnapshot4.FinalResponseMarkdown : null)) == null)
        {
          text2 = ((taskDetail != null) ? taskDetail.FailureMessage : null) ?? "";
        }
        defaultInterpolatedStringHandler.AppendFormatted(EndToEnd.Trim(text2, 200));
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        string text3 = ((taskDetail != null) ? taskDetail.WorkspacePath : null);
        if (text3 != null && Directory.Exists(text3))
        {
          List<string> list = (from f in Directory.EnumerateFiles(text3, "*", SearchOption.AllDirectories)
            where File.GetLastWriteTimeUtc(f) > DateTime.UtcNow - CS$<>8__locals1.sw.Elapsed - TimeSpan.FromSeconds(5L)
            select f).ToList<string>();
          string text4 = "agent: files written in ";
          string text5 = text3;
          string text6 = ": ";
          string text7 = ", ";
          Func<string, string> func;
          if ((func = EndToEnd.<>O.<1>__GetFileName) == null)
          {
            func = (EndToEnd.<>O.<1>__GetFileName = new Func<string, string>(Path.GetFileName));
          }
          EndToEnd.Say(text4 + text5 + text6 + string.Join(text7, list.Select<string, string>(func)));
        }
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 1);
        defaultInterpolatedStringHandler.AppendLiteral("agent: timings ");
        defaultInterpolatedStringHandler.AppendFormatted<AgentRuntimeTimings>(agents.Timings);
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        this.host.DispatcherQueue.TryEnqueue(delegate
        {
          EndToEnd.<>c__DisplayClass64_0.<<AgentAsync>b__2>d <<AgentAsync>b__2>d;
          <<AgentAsync>b__2>d.<>t__builder = AsyncVoidMethodBuilder.Create();
          <<AgentAsync>b__2>d.<>4__this = CS$<>8__locals1;
          <<AgentAsync>b__2>d.<>1__state = -1;
          <<AgentAsync>b__2>d.<>t__builder.Start<EndToEnd.<>c__DisplayClass64_0.<<AgentAsync>b__2>d>(ref <<AgentAsync>b__2>d);
        });
      }
    }

    // Token: 0x060024F7 RID: 9463 RVA: 0x00638B88 File Offset: 0x00638B88
    private unsafe async Task InterviewAsync()
    {
      EndToEnd.<>c__DisplayClass65_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass65_0();
      CS$<>8__locals1.<>4__this = this;
      RealStack real = this.host.Real;
      bool flag = !real.Core.Auth.IsSignedIn;
      if (flag)
      {
        TaskAwaiter<bool> taskAwaiter = this.SignInAsync().GetAwaiter();
        if (!taskAwaiter.IsCompleted)
        {
          await taskAwaiter;
          TaskAwaiter<bool> taskAwaiter2;
          taskAwaiter = taskAwaiter2;
          taskAwaiter2 = default(TaskAwaiter<bool>);
        }
        flag = !taskAwaiter.GetResult();
      }
      if (!flag)
      {
        EndToEnd.<>c__DisplayClass65_0 CS$<>8__locals2 = CS$<>8__locals1;
        string text;
        if ((text = Environment.GetEnvironmentVariable("HEYCLICKY_E2E_ANSWERS")) == null)
        {
          string text2 = "|";
          <>y__InlineArray4<string> <>y__InlineArray = default(<>y__InlineArray4<string>);
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<string>, string>(ref <>y__InlineArray, 0) = "I make YouTube videos about indie game development, and I'm building a small cozy farming game on the side.";
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<string>, string>(ref <>y__InlineArray, 1) = "Getting the channel to 100k subscribers this year and shipping the game's demo on Steam by December.";
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<string>, string>(ref <>y__InlineArray, 2) = "Scripting. I spend hours on the hook and researching what other devlog channels are doing, and I keep putting off the Steam page.";
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<string>, string>(ref <>y__InlineArray, 3) = "Mostly ChatGPT. I tried Claude Code once to fix a Godot script.";
          text = string.Join(text2, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray4<string>, string>(in <>y__InlineArray, 4));
        }
        CS$<>8__locals2.answers = text.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        CS$<>8__locals1.interview = null;
        await this.OnUi(delegate
        {
          CS$<>8__locals1.<>4__this.host.DemoTutorial(TutorialWindow.Step.ClickyInterview);
          CS$<>8__locals1.interview = CS$<>8__locals1.<>4__this.host.Interview;
        });
        CS$<>8__locals1.iv = CS$<>8__locals1.interview;
        Stopwatch sw = Stopwatch.StartNew();
        while (sw.Elapsed < TimeSpan.FromSeconds(30L) && (!CS$<>8__locals1.iv.HasHeardFirstQuestion || CS$<>8__locals1.iv.IsOpenerSpeaking))
        {
          await Task.Delay(250);
        }
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 3);
        defaultInterpolatedStringHandler.AppendLiteral("interview: opener heard=");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.iv.HasHeardFirstQuestion);
        defaultInterpolatedStringHandler.AppendLiteral(" after ");
        defaultInterpolatedStringHandler.AppendFormatted<long>(sw.ElapsedMilliseconds);
        defaultInterpolatedStringHandler.AppendLiteral(" ms: ");
        defaultInterpolatedStringHandler.AppendFormatted(EndToEnd.Trim(CS$<>8__locals1.iv.SpokenText, 300));
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        await this.SnapAsync("e2e-interview-opener");
        EndToEnd.<>c__DisplayClass65_1 CS$<>8__locals3 = new EndToEnd.<>c__DisplayClass65_1();
        CS$<>8__locals3.CS$<>8__locals1 = CS$<>8__locals1;
        CS$<>8__locals3.i = 0;
        while (CS$<>8__locals3.i < CS$<>8__locals3.CS$<>8__locals1.answers.Length)
        {
          Stopwatch turn = Stopwatch.StartNew();
          await this.OnUi(delegate
          {
            CS$<>8__locals3.CS$<>8__locals1.iv.SubmitTyped(CS$<>8__locals3.CS$<>8__locals1.answers[CS$<>8__locals3.i]);
          });
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 2);
          defaultInterpolatedStringHandler.AppendLiteral("interview: answer ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals3.i + 1);
          defaultInterpolatedStringHandler.AppendLiteral(": ");
          defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals3.CS$<>8__locals1.answers[CS$<>8__locals3.i]);
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          await Task.Delay(800);
          await EndToEnd.WaitIdleAsync(real.Companion, 90000);
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 3);
          defaultInterpolatedStringHandler.AppendLiteral("interview: reply ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals3.i + 1);
          defaultInterpolatedStringHandler.AppendLiteral(" after ");
          defaultInterpolatedStringHandler.AppendFormatted<long>(turn.ElapsedMilliseconds);
          defaultInterpolatedStringHandler.AppendLiteral(" ms: ");
          defaultInterpolatedStringHandler.AppendFormatted(EndToEnd.Trim(real.Companion.ResponseText, 400));
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          if (CS$<>8__locals3.i == 1)
          {
            await this.SnapAsync("e2e-interview-answer2");
          }
          turn = null;
          CS$<>8__locals3.i++;
        }
        CS$<>8__locals3 = null;
        await this.SnapAsync("e2e-interview-wrapup");
        Stopwatch planWait = Stopwatch.StartNew();
        while (planWait.Elapsed < TimeSpan.FromMinutes(4L) && !CS$<>8__locals1.iv.HasBuiltPlan && (CS$<>8__locals1.iv.IsPlanInFlight || CS$<>8__locals1.iv.IsActive))
        {
          await Task.Delay(500);
        }
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 2);
        defaultInterpolatedStringHandler.AppendLiteral("interview: transcript ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.iv.Transcript.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" lines (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.iv.Transcript.Count<InterviewLine>((InterviewLine l) => l.Role == "user"));
        defaultInterpolatedStringHandler.AppendLiteral(" answers)");
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        foreach (InterviewLine interviewLine in CS$<>8__locals1.iv.Transcript)
        {
          EndToEnd.Say("interview:   " + interviewLine.Role + ": " + EndToEnd.Trim(interviewLine.Text, 300));
        }
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(60, 3);
        defaultInterpolatedStringHandler.AppendLiteral("interview: plan built=");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.iv.HasBuiltPlan);
        defaultInterpolatedStringHandler.AppendLiteral(" in ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(planWait.Elapsed.TotalSeconds, "0.0");
        defaultInterpolatedStringHandler.AppendLiteral(" s after the wrap-up watch; error=");
        defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.iv.PlanError ?? "none");
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        StarterPlanResponse lastPlan = CS$<>8__locals1.iv.LastPlan;
        if (lastPlan != null)
        {
          EndToEnd.Say("interview: goal_line=\"" + lastPlan.GoalLine + "\" agent_familiarity=" + lastPlan.AgentFamiliarity);
          foreach (StarterPlanClicky starterPlanClicky in (lastPlan.Clickys ?? Array.Empty<StarterPlanClicky>()))
          {
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(62, 7);
            defaultInterpolatedStringHandler.AppendLiteral("interview: clicky ");
            defaultInterpolatedStringHandler.AppendFormatted(starterPlanClicky.Key);
            defaultInterpolatedStringHandler.AppendLiteral(": ");
            defaultInterpolatedStringHandler.AppendFormatted(starterPlanClicky.Name);
            defaultInterpolatedStringHandler.AppendLiteral(" (");
            defaultInterpolatedStringHandler.AppendFormatted(starterPlanClicky.Role);
            defaultInterpolatedStringHandler.AppendLiteral(") — ");
            defaultInterpolatedStringHandler.AppendFormatted(starterPlanClicky.Description);
            defaultInterpolatedStringHandler.AppendLiteral(" | reveal: ");
            defaultInterpolatedStringHandler.AppendFormatted(starterPlanClicky.RevealLine);
            defaultInterpolatedStringHandler.AppendLiteral(" | intro: ");
            defaultInterpolatedStringHandler.AppendFormatted(string.Join(" / ", starterPlanClicky.IntroMessages ?? Array.Empty<string>()));
            defaultInterpolatedStringHandler.AppendLiteral(" | first asks: ");
            defaultInterpolatedStringHandler.AppendFormatted(string.Join(" / ", starterPlanClicky.FirstAsks ?? Array.Empty<string>()));
            EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          }
          foreach (StarterPlanSuggestion starterPlanSuggestion in (lastPlan.SuggestedAgents ?? Array.Empty<StarterPlanSuggestion>()))
          {
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 5);
            defaultInterpolatedStringHandler.AppendLiteral("interview: task \"");
            defaultInterpolatedStringHandler.AppendFormatted(starterPlanSuggestion.Title);
            defaultInterpolatedStringHandler.AppendLiteral("\" for ");
            defaultInterpolatedStringHandler.AppendFormatted(starterPlanSuggestion.ClickyKey ?? starterPlanSuggestion.ClickySlug);
            defaultInterpolatedStringHandler.AppendLiteral(" [");
            defaultInterpolatedStringHandler.AppendFormatted(starterPlanSuggestion.Deliverable);
            StarterPlanRoutine routine = starterPlanSuggestion.Routine;
            string text3;
            if (routine == null)
            {
              text3 = "";
            }
            else
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(20, 1);
              defaultInterpolatedStringHandler2.AppendLiteral(", routine every ");
              defaultInterpolatedStringHandler2.AppendFormatted<int>(routine.EveryMinutes);
              defaultInterpolatedStringHandler2.AppendLiteral(" min");
              text3 = defaultInterpolatedStringHandler2.ToStringAndClear();
            }
            defaultInterpolatedStringHandler.AppendFormatted(text3);
            defaultInterpolatedStringHandler.AppendLiteral("] — ");
            defaultInterpolatedStringHandler.AppendFormatted(starterPlanSuggestion.Description);
            EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          }
        }
        await this.OnUi(delegate
        {
          CS$<>8__locals1.<>4__this.host.DemoTutorial(TutorialWindow.Step.Finale);
        });
        await Task.Delay(600);
        CS$<>8__locals1.tourDone = new TaskCompletionSource();
        await this.OnUi(delegate
        {
          TutorialWindow tutorialWindow = CS$<>8__locals1.<>4__this.host.DemoTutorialWindow();
          if (tutorialWindow != null)
          {
            tutorialWindow.Hide();
          }
          Task task = CS$<>8__locals1.<>4__this.host.RunFinaleRevealAsync();
          Func<Task, bool> func;
          if ((func = CS$<>8__locals1.<>9__5) == null)
          {
            func = (CS$<>8__locals1.<>9__5 = (Task _) => CS$<>8__locals1.tourDone.TrySetResult());
          }
          task.ContinueWith<bool>(func);
        });
        Stopwatch tourWait = Stopwatch.StartNew();
        await Task.WhenAny(CS$<>8__locals1.tourDone.Task, Task.Delay(TimeSpan.FromMinutes(4L)));
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 2);
        defaultInterpolatedStringHandler.AppendLiteral("interview: home tour finished=");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.tourDone.Task.IsCompleted);
        defaultInterpolatedStringHandler.AppendLiteral(" in ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(tourWait.Elapsed.TotalSeconds, "0.0");
        defaultInterpolatedStringHandler.AppendLiteral(" s");
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        ClickyRoster shared = ClickyRoster.Shared;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 2);
        defaultInterpolatedStringHandler.AppendLiteral("interview: roster now [");
        defaultInterpolatedStringHandler.AppendFormatted(string.Join(", ", shared.Clickys.Select<RosterClicky, string>((RosterClicky c) => c.Slug)));
        defaultInterpolatedStringHandler.AppendLiteral("]; agents' store [");
        defaultInterpolatedStringHandler.AppendFormatted(string.Join(", ", real.Agents.Clickys.Select<ClickyInfo, string>((ClickyInfo c) => c.Slug)));
        defaultInterpolatedStringHandler.AppendLiteral("]");
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      }
    }

    // Token: 0x060024F8 RID: 9464 RVA: 0x00638BCC File Offset: 0x00638BCC
    private Task OnUi(Action a)
    {
      TaskCompletionSource tcs = new TaskCompletionSource();
      this.host.DispatcherQueue.TryEnqueue(delegate
      {
        try
        {
          a();
          tcs.TrySetResult();
        }
        catch (Exception ex)
        {
          tcs.TrySetException(ex);
        }
      });
      return tcs.Task;
    }

    // Token: 0x060024F9 RID: 9465 RVA: 0x00638C1C File Offset: 0x00638C1C
    private async Task SnapAsync(string name)
    {
      EndToEnd.<>c__DisplayClass67_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass67_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.name = name;
      CS$<>8__locals1.tcs = new TaskCompletionSource();
      this.host.DispatcherQueue.TryEnqueue(delegate
      {
        EndToEnd.<>c__DisplayClass67_0.<<SnapAsync>b__0>d <<SnapAsync>b__0>d;
        <<SnapAsync>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
        <<SnapAsync>b__0>d.<>4__this = CS$<>8__locals1;
        <<SnapAsync>b__0>d.<>1__state = -1;
        <<SnapAsync>b__0>d.<>t__builder.Start<EndToEnd.<>c__DisplayClass67_0.<<SnapAsync>b__0>d>(ref <<SnapAsync>b__0>d);
      });
      await CS$<>8__locals1.tcs.Task;
    }

    // Token: 0x060024FA RID: 9466 RVA: 0x00638C68 File Offset: 0x00638C68
    private static async Task WaitIdleAsync(ICompanionSession companion, int timeoutMs)
    {
      Stopwatch sw = Stopwatch.StartNew();
      await Task.Delay(500);
      while (companion.State != VoiceState.Idle && sw.ElapsedMilliseconds < (long)timeoutMs)
      {
        await Task.Delay(200);
      }
      await Task.Delay(800);
    }

    // Token: 0x060024FB RID: 9467 RVA: 0x00638CB4 File Offset: 0x00638CB4
    private async Task SaveArtifactsAsync(string name)
    {
      EndToEnd.<>c__DisplayClass69_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass69_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.name = name;
      RealStack real = this.host.Real;
      try
      {
        byte[] png = real.Canvas.RenderDrawingPng(null, 1.0, true, true);
        if (png != null)
        {
          string path = Path.Combine(this.outDir, "e2e-" + CS$<>8__locals1.name + "-drawing.png");
          await File.WriteAllBytesAsync(path, png, default(CancellationToken));
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 3);
          defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.name);
          defaultInterpolatedStringHandler.AppendLiteral(": drawing PNG ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(png.Length);
          defaultInterpolatedStringHandler.AppendLiteral(" bytes â†’ ");
          defaultInterpolatedStringHandler.AppendFormatted(path);
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          path = null;
        }
        else
        {
          EndToEnd.Say(CS$<>8__locals1.name + ": no drawing to render");
        }
        EndToEnd.Say(CS$<>8__locals1.name + ": drawing state = " + EndToEnd.Trim(real.Canvas.DrawingStateForModel() ?? "", 300));
        png = null;
      }
      catch (Exception ex)
      {
        EndToEnd.Say(CS$<>8__locals1.name + ": drawing render failed: " + ex.Message);
      }
      CS$<>8__locals1.tcs = new TaskCompletionSource();
      this.host.DispatcherQueue.TryEnqueue(delegate
      {
        EndToEnd.<>c__DisplayClass69_0.<<SaveArtifactsAsync>b__0>d <<SaveArtifactsAsync>b__0>d;
        <<SaveArtifactsAsync>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
        <<SaveArtifactsAsync>b__0>d.<>4__this = CS$<>8__locals1;
        <<SaveArtifactsAsync>b__0>d.<>1__state = -1;
        <<SaveArtifactsAsync>b__0>d.<>t__builder.Start<EndToEnd.<>c__DisplayClass69_0.<<SaveArtifactsAsync>b__0>d>(ref <<SaveArtifactsAsync>b__0>d);
      });
      await CS$<>8__locals1.tcs.Task;
    }

    // Token: 0x060024FC RID: 9468 RVA: 0x00638D00 File Offset: 0x00638D00
    private static string Trim(string s, int max = 200)
    {
      s = s.Replace('\n', ' ').Trim();
      if (s.Length <= max)
      {
        return s;
      }
      return s.Substring(0, max) + "…";
    }

    // Token: 0x060024FD RID: 9469 RVA: 0x00638D30 File Offset: 0x00638D30
    private async Task HarnessAsync()
    {
      EndToEnd.<>c__DisplayClass71_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass71_0();
      RealStack real = this.host.Real;
      CS$<>8__locals1.agents = real.Agents;
      ValueTuple<AgentHarness, AgentHarnessPlace> valueTuple = AgentHarnessThreads.Choice(null);
      EndToEnd.Say("h1: new conversations run on " + valueTuple.Item1.DisplayName() + " " + valueTuple.Item2.DisplayName());
      bool flag;
      try
      {
        await AgentHarnessRunnerProcess.EnsureRunningAsync(default(CancellationToken));
        flag = true;
      }
      catch (AgentHarnessException ex)
      {
        flag = false;
        EndToEnd.Say("h1: " + ex.Message);
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 3);
      defaultInterpolatedStringHandler.AppendLiteral("h1: runner available=");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
      defaultInterpolatedStringHandler.AppendLiteral(" (bundled runner ");
      defaultInterpolatedStringHandler.AppendFormatted(File.Exists(AgentHarnessRunnerProcess.BundledRunnerPath) ? "present" : "missing");
      defaultInterpolatedStringHandler.AppendLiteral(" at ");
      defaultInterpolatedStringHandler.AppendFormatted(AgentHarnessRunnerProcess.BundledRunnerPath);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      CS$<>8__locals1.jobTaskId = null;
      new List<string>();
      CS$<>8__locals1.agents.TaskEvent += CS$<>8__locals1.<HarnessAsync>g__OnEvent|0;
      CS$<>8__locals1.agents.LiveSteps.Changed += CS$<>8__locals1.<HarnessAsync>g__OnLive|1;
      try
      {
        Stopwatch sw = Stopwatch.StartNew();
        string text = Environment.GetEnvironmentVariable("HEYCLICKY_E2E_AGENT") ?? "Find today's date and the current time, write them into a file called today.txt in your output folder, and tell me what you wrote.";
        CS$<>8__locals1.jobTaskId = await CS$<>8__locals1.agents.LaunchAsync(new AgentLaunchRequest
        {
          Prompt = text,
          NewClickyName = "Writing today's date",
          LaunchSource = "text",
          LaunchContext = "The voice decided this needs background work and called sessions_spawn, so HeyClicky is starting a helper on it."
        }, default(CancellationToken));
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 2);
        defaultInterpolatedStringHandler.AppendLiteral("h1: launched task ");
        defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.jobTaskId);
        defaultInterpolatedStringHandler.AppendLiteral(" in ");
        defaultInterpolatedStringHandler.AppendFormatted<long>(sw.ElapsedMilliseconds);
        defaultInterpolatedStringHandler.AppendLiteral(" ms");
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        AgentTaskDetail detail = await EndToEnd.WaitForTaskEndAsync(CS$<>8__locals1.agents, CS$<>8__locals1.jobTaskId, TimeSpan.FromMinutes(6L));
        this.ReportJobTurn(CS$<>8__locals1.agents, CS$<>8__locals1.jobTaskId, detail, sw);
        AgentTaskDetail agentTaskDetail = detail;
        await this.WaitForHandBackAsync((agentTaskDetail != null) ? agentTaskDetail.Snapshot.ThreadId : null, TimeSpan.FromSeconds(90L));
        AgentTaskDetail agentTaskDetail2 = detail;
        string jobId = ((agentTaskDetail2 != null) ? agentTaskDetail2.JobId : null);
        if (jobId != null && Environment.GetEnvironmentVariable("HEYCLICKY_E2E_SKIP_FOLLOWUP") != "1")
        {
          Stopwatch follow = Stopwatch.StartNew();
          string followId = await CS$<>8__locals1.agents.LaunchAsync(new AgentLaunchRequest
          {
            Prompt = "Add one more line to today.txt with the day of the week, then tell me the file's contents.",
            JobId = jobId,
            LaunchSource = "text"
          }, default(CancellationToken));
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 3);
          defaultInterpolatedStringHandler.AppendLiteral("h1: follow-up sent to job ");
          defaultInterpolatedStringHandler.AppendFormatted(jobId);
          defaultInterpolatedStringHandler.AppendLiteral(" (task ");
          defaultInterpolatedStringHandler.AppendFormatted(followId);
          defaultInterpolatedStringHandler.AppendLiteral("; same task: ");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(followId == CS$<>8__locals1.jobTaskId);
          defaultInterpolatedStringHandler.AppendLiteral(")");
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          CS$<>8__locals1.jobTaskId = followId;
          await Task.Delay(1500);
          AgentTaskDetail agentTaskDetail3 = await EndToEnd.WaitForTaskEndAsync(CS$<>8__locals1.agents, followId, TimeSpan.FromMinutes(6L));
          this.ReportJobTurn(CS$<>8__locals1.agents, followId, agentTaskDetail3, follow);
          await this.WaitForHandBackAsync((agentTaskDetail3 != null) ? agentTaskDetail3.Snapshot.ThreadId : null, TimeSpan.FromSeconds(90L));
          follow = null;
          followId = null;
        }
        sw = null;
        detail = null;
        jobId = null;
      }
      finally
      {
        CS$<>8__locals1.agents.TaskEvent -= CS$<>8__locals1.<HarnessAsync>g__OnEvent|0;
        CS$<>8__locals1.agents.LiveSteps.Changed -= CS$<>8__locals1.<HarnessAsync>g__OnLive|1;
      }
    }

    // Token: 0x060024FE RID: 9470 RVA: 0x00638D74 File Offset: 0x00638D74
    [return: Nullable(new byte[] { 1, 2 })]
    private static async Task<AgentTaskDetail> WaitForTaskEndAsync(AgentService agents, string taskId, TimeSpan limit)
    {
      Stopwatch sw = Stopwatch.StartNew();
      for (;;)
      {
        bool flag = sw.Elapsed < TimeSpan.FromSeconds(20L);
        if (flag)
        {
          AgentTaskDetail taskDetail = agents.GetTaskDetail(taskId);
          AgentTaskStatus? agentTaskStatus = ((taskDetail != null) ? new AgentTaskStatus?(taskDetail.Snapshot.Status) : null);
          flag = agentTaskStatus == null || agentTaskStatus.GetValueOrDefault() > AgentTaskStatus.Running;
        }
        if (!flag)
        {
          break;
        }
        await Task.Delay(250);
      }
      while (sw.Elapsed < limit)
      {
        AgentTaskDetail taskDetail2 = agents.GetTaskDetail(taskId);
        AgentTaskStatus? agentTaskStatus = ((taskDetail2 != null) ? new AgentTaskStatus?(taskDetail2.Snapshot.Status) : null);
        if (agentTaskStatus != null && agentTaskStatus.GetValueOrDefault() - AgentTaskStatus.WaitingForUser <= 3)
        {
          return taskDetail2;
        }
        await Task.Delay(500);
      }
      return agents.GetTaskDetail(taskId);
    }

    // Token: 0x060024FF RID: 9471 RVA: 0x00638DC8 File Offset: 0x00638DC8
    private void ReportJobTurn(AgentService agents, string taskId, [Nullable(2)] AgentTaskDetail detail, Stopwatch sw)
    {
      if (detail == null)
      {
        EndToEnd.Say("h1: FAILED no task detail");
        return;
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 5);
      defaultInterpolatedStringHandler.AppendLiteral("h1: ");
      defaultInterpolatedStringHandler.AppendFormatted<AgentTaskStatus>(detail.Snapshot.Status);
      defaultInterpolatedStringHandler.AppendLiteral(" after ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(sw.Elapsed.TotalSeconds, "0.0");
      defaultInterpolatedStringHandler.AppendLiteral(" s; harness turn=");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(detail.IsHarnessTurn);
      defaultInterpolatedStringHandler.AppendLiteral("; thread ");
      defaultInterpolatedStringHandler.AppendFormatted(detail.Snapshot.ThreadId);
      defaultInterpolatedStringHandler.AppendLiteral("; turn ");
      defaultInterpolatedStringHandler.AppendFormatted(detail.TurnId);
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      string text = "h1: title \"";
      string title = detail.Snapshot.Title;
      string text2 = "\"; summary: ";
      string text3;
      if ((text3 = detail.Summary) == null)
      {
        text3 = detail.FailureMessage ?? "";
      }
      EndToEnd.Say(text + title + text2 + EndToEnd.Trim(text3, 300));
      EndToEnd.Say("h1: final plan [" + string.Join(" | ", (detail.Plan ?? Array.Empty<AgentPlanStep>()).Select<AgentPlanStep, string>((AgentPlanStep s) => s.Text + " (" + s.Status + ")")) + "]");
      ClickyAnswer answer = detail.Answer;
      if (answer != null)
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 3);
        defaultInterpolatedStringHandler.AppendLiteral("h1: ANSWER outcome \"");
        defaultInterpolatedStringHandler.AppendFormatted(answer.Line);
        defaultInterpolatedStringHandler.AppendLiteral("\"; blocks [");
        defaultInterpolatedStringHandler.AppendFormatted(string.Join<ClickyAnswer.Block>(",", answer.Blocks));
        defaultInterpolatedStringHandler.AppendLiteral("]; report JSON ");
        defaultInterpolatedStringHandler.AppendFormatted(answer.ReportJson);
        EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      }
      else
      {
        EndToEnd.Say("h1: no <ANSWER> block");
      }
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 2);
      defaultInterpolatedStringHandler.AppendLiteral("h1: artifacts [");
      defaultInterpolatedStringHandler.AppendFormatted(string.Join(", ", detail.Artifacts));
      defaultInterpolatedStringHandler.AppendLiteral("]; next actions [");
      defaultInterpolatedStringHandler.AppendFormatted(string.Join(" | ", detail.NextActions));
      defaultInterpolatedStringHandler.AppendLiteral("]");
      EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      EndToEnd.Say("h1: full report: " + EndToEnd.Trim(detail.Snapshot.FinalResponseMarkdown ?? "", 600));
      string jobId = detail.JobId;
      if (jobId != null)
      {
        ClickyJob clickyJob = agents.Jobs.Job(jobId);
        if (clickyJob != null)
        {
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 5);
          defaultInterpolatedStringHandler.AppendLiteral("h1: job ");
          defaultInterpolatedStringHandler.AppendFormatted(clickyJob.Id);
          defaultInterpolatedStringHandler.AppendLiteral(" \"");
          defaultInterpolatedStringHandler.AppendFormatted(clickyJob.Title);
          defaultInterpolatedStringHandler.AppendLiteral("\" outcome=");
          defaultInterpolatedStringHandler.AppendFormatted<ClickyJobOutcome>(clickyJob.Outcome);
          defaultInterpolatedStringHandler.AppendLiteral(" phase=");
          defaultInterpolatedStringHandler.AppendFormatted<ClickyJobRailPhase>(agents.RailPhase(clickyJob));
          defaultInterpolatedStringHandler.AppendLiteral(" folder=");
          defaultInterpolatedStringHandler.AppendFormatted(clickyJob.FolderPath);
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          EndToEnd.Say("h1: job runs: " + string.Join(" / ", (clickyJob.Runs ?? Array.Empty<ClickyJobRun>()).Select<ClickyJobRun, string>(delegate(ClickyJobRun r)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(11, 3);
            defaultInterpolatedStringHandler2.AppendLiteral("[");
            defaultInterpolatedStringHandler2.AppendFormatted<ClickyJobOutcome>(r.Outcome);
            defaultInterpolatedStringHandler2.AppendLiteral("] ");
            defaultInterpolatedStringHandler2.AppendFormatted(EndToEnd.Trim(r.Ask, 60));
            defaultInterpolatedStringHandler2.AppendLiteral(" thread=");
            defaultInterpolatedStringHandler2.AppendFormatted(r.ThreadId);
            return defaultInterpolatedStringHandler2.ToStringAndClear();
          })));
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 2);
          defaultInterpolatedStringHandler.AppendLiteral("h1: job summary: ");
          defaultInterpolatedStringHandler.AppendFormatted(EndToEnd.Trim(clickyJob.Summary ?? "", 200));
          defaultInterpolatedStringHandler.AppendLiteral("; files from latest reply: [");
          defaultInterpolatedStringHandler.AppendFormatted(string.Join(", ", clickyJob.FilesFromLatestReply));
          defaultInterpolatedStringHandler.AppendLiteral("]");
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          if (Directory.Exists(Path.Combine(clickyJob.FolderPath, "output")))
          {
            foreach (string text4 in Directory.EnumerateFiles(Path.Combine(clickyJob.FolderPath, "output")))
            {
              EndToEnd.Say("h1: output file " + Path.GetFileName(text4) + ": " + EndToEnd.Trim(File.ReadAllText(text4), 200));
            }
          }
          string text5 = Path.Combine(Path.GetDirectoryName(clickyJob.FolderPath), "WORK.md");
          if (File.Exists(text5))
          {
            EndToEnd.Say("h1: WORK.md: " + EndToEnd.Trim(File.ReadAllText(text5), 300));
            return;
          }
          return;
        }
      }
      EndToEnd.Say("h1: (not a job)");
    }

    // Token: 0x06002500 RID: 9472 RVA: 0x00639288 File Offset: 0x00639288
    private async Task WaitForHandBackAsync([Nullable(2)] string threadId, TimeSpan limit)
    {
      if (threadId != null)
      {
        OneBrainJobReports reports = this.host.JobReports;
        if (reports != null)
        {
          Stopwatch sw = Stopwatch.StartNew();
          while (sw.Elapsed < limit && !reports.HasPresented(threadId))
          {
            await Task.Delay(500);
          }
          IReadOnlyList<string> readOnlyList = reports.Lines(threadId);
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 4);
          defaultInterpolatedStringHandler.AppendLiteral("h1: hand-back after ");
          defaultInterpolatedStringHandler.AppendFormatted<double>(sw.Elapsed.TotalSeconds, "0.0");
          defaultInterpolatedStringHandler.AppendLiteral(" s: presented=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(reports.HasPresented(threadId));
          defaultInterpolatedStringHandler.AppendLiteral(" lines=");
          defaultInterpolatedStringHandler.AppendFormatted<int>(readOnlyList.Count);
          defaultInterpolatedStringHandler.AppendLiteral(": ");
          defaultInterpolatedStringHandler.AppendFormatted(EndToEnd.Trim(string.Join(" ", readOnlyList), 400));
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          ClickyAnswer clickyAnswer = this.host.Real.Agents.ResultCard(threadId);
          if (clickyAnswer != null)
          {
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 2);
            defaultInterpolatedStringHandler.AppendLiteral("h1: hand-back card: outcome \"");
            defaultInterpolatedStringHandler.AppendFormatted(clickyAnswer.Line);
            defaultInterpolatedStringHandler.AppendLiteral("\" blocks [");
            defaultInterpolatedStringHandler.AppendFormatted(string.Join<ClickyAnswer.Block>(",", clickyAnswer.Blocks));
            defaultInterpolatedStringHandler.AppendLiteral("]");
            EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          }
          else
          {
            EndToEnd.Say("h1: hand-back card: none");
          }
          return;
        }
      }
      EndToEnd.Say("h1: hand-back: not wired");
    }

    // Token: 0x06002501 RID: 9473 RVA: 0x006392DC File Offset: 0x006392DC
    private async Task IntroNotchAsync()
    {
      EndToEnd.<>c__DisplayClass75_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass75_0();
      CS$<>8__locals1.<>4__this = this;
      TaskAwaiter<bool> taskAwaiter = this.SignInAsync().GetAwaiter();
      if (!taskAwaiter.IsCompleted)
      {
        await taskAwaiter;
        TaskAwaiter<bool> taskAwaiter2;
        taskAwaiter = taskAwaiter2;
        taskAwaiter2 = default(TaskAwaiter<bool>);
      }
      if (taskAwaiter.GetResult())
      {
        CS$<>8__locals1.p = this.host.CorePresenter;
        if (CS$<>8__locals1.p == null)
        {
          EndToEnd.Say("intro-notch: no core presenter");
        }
        else
        {
          await this.OnUi(delegate
          {
            CS$<>8__locals1.<>4__this.host.DemoIntroSequence(true);
          });
          await Task.Delay(500);
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 1);
          defaultInterpolatedStringHandler.AppendLiteral("intro-notch: started, intro owns the stage=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.p.IsOnboardingIntroOwningStage);
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          await CS$<>8__locals1.<IntroNotchAsync>g__HoverAndClickAsync|2("the welcome");
          int i = 0;
          while (i < 300 && this.host.CursorCompanion.CurrentPlayer == null)
          {
            await Task.Delay(100);
            i++;
          }
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
          defaultInterpolatedStringHandler.AppendLiteral("intro-notch: video playing=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(this.host.CursorCompanion.CurrentPlayer != null);
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          await CS$<>8__locals1.<IntroNotchAsync>g__HoverAndClickAsync|2("the video");
          await this.OnUi(delegate
          {
            CS$<>8__locals1.p.SetPointerInside(true);
          });
          await this.OnUi(delegate
          {
            CS$<>8__locals1.<>4__this.host.CursorCompanion.SkipVideo();
          });
          i = 0;
          while (i < 150 && this.host.IsIntroSequenceActive)
          {
            await Task.Delay(100);
            i++;
          }
          await Task.Delay(1000);
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(75, 4);
          defaultInterpolatedStringHandler.AppendLiteral("intro-notch: after the reel: intro active=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(this.host.IsIntroSequenceActive);
          defaultInterpolatedStringHandler.AppendLiteral(" owns the stage=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.p.IsOnboardingIntroOwningStage);
          defaultInterpolatedStringHandler.AppendLiteral(" hovering=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.p.IsHovering);
          defaultInterpolatedStringHandler.AppendLiteral(" shown=");
          defaultInterpolatedStringHandler.AppendFormatted(EndToEnd.<IntroNotchAsync>g__Shown|75_0(CS$<>8__locals1.p));
          EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
          await this.OnUi(delegate
          {
            CS$<>8__locals1.p.SetPointerInside(false);
          });
          EndToEnd.Say("intro-notch: done");
        }
      }
    }

    // Token: 0x06002502 RID: 9474 RVA: 0x00639320 File Offset: 0x00639320
    private async Task LessonReplayAsync()
    {
      EndToEnd.<>c__DisplayClass76_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass76_0();
      TaskAwaiter<bool> taskAwaiter = this.SignInAsync().GetAwaiter();
      if (!taskAwaiter.IsCompleted)
      {
        await taskAwaiter;
        TaskAwaiter<bool> taskAwaiter2;
        taskAwaiter = taskAwaiter2;
        taskAwaiter2 = default(TaskAwaiter<bool>);
      }
      if (taskAwaiter.GetResult())
      {
        CS$<>8__locals1.lessons = this.host.Lessons;
        if (CS$<>8__locals1.lessons == null)
        {
          EndToEnd.Say("lesson-replay: no lesson service");
        }
        else
        {
          CS$<>8__locals1.lesson = CS$<>8__locals1.lessons.Store.List().FirstOrDefault<StoredLesson>();
          if (CS$<>8__locals1.lesson == null)
          {
            EndToEnd.Say("lesson-replay: no lesson in " + LessonStore.Root);
          }
          else
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 3);
            defaultInterpolatedStringHandler.AppendLiteral("lesson-replay: \"");
            defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.lesson.Data.Title);
            defaultInterpolatedStringHandler.AppendLiteral("\", ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.lesson.Data.Lines.Count);
            defaultInterpolatedStringHandler.AppendLiteral(" lines over ");
            defaultInterpolatedStringHandler.AppendFormatted<double>(CS$<>8__locals1.lesson.Data.Duration, "0.0");
            defaultInterpolatedStringHandler.AppendLiteral(" s");
            EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
            CS$<>8__locals1.thrown = new ConcurrentDictionary<string, int>();
            AppDomain.CurrentDomain.FirstChanceException += CS$<>8__locals1.<LessonReplayAsync>g__OnThrown|0;
            int mark = this.LogMark();
            await this.OnUi(delegate
            {
              CS$<>8__locals1.lessons.PlayAsync(CS$<>8__locals1.lesson, true, false);
            });
            DateTime until = DateTime.UtcNow + TimeSpan.FromSeconds(CS$<>8__locals1.lesson.Data.Duration + 20.0);
            List<string> list = new List<string>();
            while (DateTime.UtcNow < until)
            {
              await Task.Delay(500);
              list = this.LogSince(mark);
              if (list.Any<string>((string l) => l.Contains("[Drawing] timeline")))
              {
                break;
              }
            }
            await Task.Delay(500);
            AppDomain.CurrentDomain.FirstChanceException -= CS$<>8__locals1.<LessonReplayAsync>g__OnThrown|0;
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 1);
            defaultInterpolatedStringHandler.AppendLiteral("lesson-replay: ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.thrown.Values.Sum());
            defaultInterpolatedStringHandler.AppendLiteral(" exceptions thrown while it played");
            EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
            foreach (KeyValuePair<string, int> keyValuePair in CS$<>8__locals1.thrown.OrderByDescending<KeyValuePair<string, int>, int>((KeyValuePair<string, int> p) => p.Value).Take<KeyValuePair<string, int>>(8))
            {
              string text;
              int num;
              keyValuePair.Deconstruct(out text, out num);
              string text2 = text;
              int num2 = num;
              defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 2);
              defaultInterpolatedStringHandler.AppendLiteral("  ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
              defaultInterpolatedStringHandler.AppendLiteral(" x ");
              defaultInterpolatedStringHandler.AppendFormatted(text2);
              EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
            }
            list = this.LogSince(mark);
            int num3 = list.FindIndex((string l) => l.Contains("[Drawing] timeline"));
            if (num3 < 0)
            {
              EndToEnd.Say("lesson-replay: no timeline logged");
            }
            else
            {
              foreach (string text3 in list.Skip<string>(num3 + 1).TakeWhile<string>((string l) => l.Length > 0 && !char.IsDigit(l[0])))
              {
                EndToEnd.Say("  " + text3.Trim());
              }
              int num4 = list.Skip<string>(num3 + 1).TakeWhile<string>((string l) => l.Length > 0 && !char.IsDigit(l[0])).Count<string>((string l) => l.Contains("<<"));
              int num5 = list.Skip<string>(num3 + 1).TakeWhile<string>((string l) => l.Length > 0 && !char.IsDigit(l[0])).Count<string>((string l) => l.Contains("not drawn"));
              defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 2);
              defaultInterpolatedStringHandler.AppendLiteral("lesson-replay: ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(num4);
              defaultInterpolatedStringHandler.AppendLiteral(" calls started late, ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(num5);
              defaultInterpolatedStringHandler.AppendLiteral(" not drawn");
              EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
            }
          }
        }
      }
    }

    // Token: 0x06002503 RID: 9475 RVA: 0x00639364 File Offset: 0x00639364
    private async Task LessonsAsync()
    {
      EndToEnd.<>c__DisplayClass78_0 CS$<>8__locals1 = new EndToEnd.<>c__DisplayClass78_0();
      TaskAwaiter<bool> taskAwaiter = this.SignInAsync().GetAwaiter();
      if (!taskAwaiter.IsCompleted)
      {
        await taskAwaiter;
        TaskAwaiter<bool> taskAwaiter2;
        taskAwaiter = taskAwaiter2;
        taskAwaiter2 = default(TaskAwaiter<bool>);
      }
      if (taskAwaiter.GetResult())
      {
        CS$<>8__locals1.real = this.host.Real;
        CS$<>8__locals1.lessons = this.host.Lessons;
        if (CS$<>8__locals1.lessons == null)
        {
          EndToEnd.Say("lessons: no lesson service");
        }
        else
        {
          CS$<>8__locals1.real.Core.Settings.Update(delegate(AppSettings s)
          {
            s.SaveDrawings = false;
            s.KeepDrawingsInChat = new bool?(false);
          });
          int recentBefore = CS$<>8__locals1.lessons.RecentDrawings.Count;
          int mark = this.LogMark();
          await this.TypedTurnAsync("Circle the middle of my screen and explain in three short spoken steps what a screen's middle is good for. Call it lessonTitle(\"The middle\").", 120000);
          await Task.Delay(1500);
          List<string> list = this.LogSince(mark);
          string text = "with Save drawings off nothing is kept";
          bool flag;
          if (CS$<>8__locals1.lessons.RecentDrawings.Count == recentBefore)
          {
            flag = !list.Any<string>((string l) => l.Contains("drawing told:"));
          }
          else
          {
            flag = false;
          }
          EndToEnd.<LessonsAsync>g__Check|78_0(text, flag);
          EndToEnd.<LessonsAsync>g__Check|78_0("with Save drawings off the drawing left up gets Clear only", list.Any<string>((string l) => l.Contains("clear offer shown (Clear only)")) || !CS$<>8__locals1.real.Canvas.IsShowingDrawing);
          await this.OnUi(delegate
          {
            CS$<>8__locals1.real.Canvas.Clear(false);
          });
          await Task.Delay(800);
          CS$<>8__locals1.real.Core.Settings.Update(delegate(AppSettings s)
          {
            s.SaveDrawings = true;
            s.KeepDrawingsInChat = new bool?(false);
          });
          recentBefore = CS$<>8__locals1.lessons.RecentDrawings.Count;
          mark = this.LogMark();
          await this.TypedTurnAsync("Explain how a cache speeds up a website. Draw it on my screen in three or four short steps and call the drawing lessonTitle(\"How a cache works\").", 120000);
          int i = 0;
          while (i < 40 && CS$<>8__locals1.lessons.RecentDrawings.Count == recentBefore)
          {
            await Task.Delay(250);
            i++;
          }
          if (CS$<>8__locals1.lessons.RecentDrawings.Count == recentBefore)
          {
            EndToEnd.<LessonsAsync>g__Check|78_0("an explanation was told", false);
          }
          else
          {
            EndToEnd.<>c__DisplayClass78_0 CS$<>8__locals2 = CS$<>8__locals1;
            IReadOnlyList<LessonService.Recent> recentDrawings = CS$<>8__locals1.lessons.RecentDrawings;
            CS$<>8__locals2.recent = recentDrawings[recentDrawings.Count - 1];
            DrawingRecord record = CS$<>8__locals1.recent.Record;
            byte[] narration = record.Narration;
            double num = ((narration != null) ? ((double)narration.Length / 2.0 / (double)record.NarrationSampleRate) : 0.0);
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(53, 6);
            defaultInterpolatedStringHandler.AppendLiteral("lessons: told \"");
            defaultInterpolatedStringHandler.AppendFormatted(record.Title);
            defaultInterpolatedStringHandler.AppendLiteral("\": ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(record.Lines.Count);
            defaultInterpolatedStringHandler.AppendLiteral(" lines at [");
            defaultInterpolatedStringHandler.AppendFormatted(string.Join(", ", record.Lines.Select<DrawingRecord.Line, string>(delegate(DrawingRecord.Line l)
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(1, 2);
              defaultInterpolatedStringHandler2.AppendFormatted<double>(l.Start, "0.00");
              defaultInterpolatedStringHandler2.AppendLiteral("+");
              defaultInterpolatedStringHandler2.AppendFormatted<double?>(l.Length, "0.00");
              return defaultInterpolatedStringHandler2.ToStringAndClear();
            })));
            defaultInterpolatedStringHandler.AppendLiteral("], ");
            defaultInterpolatedStringHandler.AppendFormatted<double>(record.Duration, "0.0");
            defaultInterpolatedStringHandler.AppendLiteral(" s, voice ");
            defaultInterpolatedStringHandler.AppendFormatted<double>(num, "0.0");
            defaultInterpolatedStringHandler.AppendLiteral(" s, ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.recent.Photos.Count);
            defaultInterpolatedStringHandler.AppendLiteral(" photos");
            EndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
            EndToEnd.<LessonsAsync>g__Check|78_0("each line's voice was recorded into one track", record.Narration != null && Math.Abs(num - (double)((int)(record.Duration * (double)record.NarrationSampleRate)) / (double)record.NarrationSampleRate) < 0.01);
            EndToEnd.<LessonsAsync>g__Check|78_0("every line lasts as long as its audio", record.Lines.All<DrawingRecord.Line>((DrawingRecord.Line l) => l.Length > 0.2));
            EndToEnd.<LessonsAsync>g__Check|78_0("the save offer shows Save", this.LogSince(mark).Any<string>((string l) => l.Contains("[lessons] save offer shown")));
            CS$<>8__locals1.saveTask = new TaskCompletionSource<StoredLesson>();
            this.host.DispatcherQueue.TryEnqueue(delegate
            {
              EndToEnd.<>c__DisplayClass78_0.<<LessonsAsync>b__8>d <<LessonsAsync>b__8>d;
              <<LessonsAsync>b__8>d.<>t__builder = AsyncVoidMethodBuilder.Create();
              <<LessonsAsync>b__8>d.<>4__this = CS$<>8__locals1;
              <<LessonsAsync>b__8>d.<>1__state = -1;
              <<LessonsAsync>b__8>d.<>t__builder.Start<EndToEnd.<>c__DisplayClass78_0.<<LessonsAsync>b__8>d>(ref <<LessonsAsync>b__8>d);
            });
            CS$<>8__locals1.saved = await CS$<>8__locals1.saveTask.Task;
            if (CS$<>8__locals1.saved == null)
            {
              EndToEnd.<LessonsAsync>g__Check|78_0("the lesson was saved", false);
            }
            else
            {
              ValueTuple<byte[], int>? valueTuple = (File.Exists(CS$<>8__locals1.saved.NarrationPath) ? LessonNarration.FromWav(File.ReadAllBytes(CS$<>8__locals1.saved.NarrationPath)) : null);
              defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 1);
              defaultInterpolatedStringHandler.AppendLiteral("narration.wav saved with the lesson (");
              double num2;
              if (valueTuple != null)
              {
                ValueTuple<byte[], int> valueOrDefault = valueTuple.GetValueOrDefault();
                num2 = (double)valueOrDefault.Item1.Length / 2.0 / (double)valueOrDefault.Item2;
              }
              else
              {
                num2 = 0.0;
              }
              defaultInterpolatedStringHandler.AppendFormatted<double>(num2, "0.0");
              defaultInterpolatedStringHandler.AppendLiteral(" s)");
              EndToEnd.<LessonsAsync>g__Check|78_0(defaultInterpolatedStringHandler.ToStringAndClear(), CS$<>8__locals1.saved.Data.HasNarration && valueTuple != null);
              int num3 = (Directory.Exists(CS$<>8__locals1.saved.PhotosFolder) ? Directory.GetFiles(CS$<>8__locals1.saved.PhotosFolder).Length : 0);
              defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 2);
              defaultInterpolatedStringHandler.AppendLiteral("its photos were saved with it (");
              defaultInterpolatedStringHandler.AppendFormatted<int>(num3);
              defaultInterpolatedStringHandler.AppendLiteral(" of ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.recent.Photos.Count);
              defaultInterpolatedStringHandler.AppendLiteral(")");
              EndToEnd.<LessonsAsync>g__Check|78_0(defaultInterpolatedStringHandler.ToStringAndClear(), num3 == CS$<>8__locals1.recent.Photos.Count);
              await Task.Delay(2500);
              await this.OnUi(delegate
              {
                CS$<>8__locals1.real.Canvas.Clear(false);
              });
              await Task.Delay(800);
              mark = this.LogMark();
              this.OnUi(delegate
              {
                CS$<>8__locals1.lessons.PlayAsync(CS$<>8__locals1.saved, true, false);
              });
              CS$<>8__locals1.untilSecondLine = ((CS$<>8__locals1.saved.Data.Lines.Count > 1) ? (CS$<>8__locals1.saved.Data.Lines[1].Start - CS$<>8__locals1.saved.ReplayShift) : 2.0);
              CS$<>8__locals1.ridingSamples = 0;
              await Task.Run(delegate
              {
                EndToEnd.<>c__DisplayClass78_0.<<LessonsAsync>b__11>d <<LessonsAsync>b__11>d;
                <<LessonsAsync>b__11>d.<>t__builder = AsyncTaskMethodBuilder.Create();
                <<LessonsAsync>b__11>d.<>4__this = CS$<>8__locals1;
                <<LessonsAsync>b__11>d.<>1__state = -1;
                <<LessonsAsync>b__11>d.<>t__builder.Start<EndToEnd.<>c__DisplayClass78_0.<<LessonsAsync>b__11>d>(ref <<LessonsAsync>b__11>d);
                return <<LessonsAsync>b__11>d.<>t__builder.Task;
              });
              if (CS$<>8__locals1.real.Cursor.Inner != null)
              {
                defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(53, 1);
                defaultInterpolatedStringHandler.AppendLiteral("the cursor rides the pen during the replay (");
                defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.ridingSamples);
                defaultInterpolatedStringHandler.AppendLiteral(" samples)");
                EndToEnd.<LessonsAsync>g__Check|78_0(defaultInterpolatedStringHandler.ToStringAndClear(), CS$<>8__locals1.ridingSamples > 0);
              }
              List<string> list2 = this.LogSince(mark);
              foreach (string text2 in list2.Where<string>((string l) => l.Contains("[lessons] replay")))
              {
                EndToEnd.Say("  log: " + EndToEnd.Trim(text2, 220));
              }
              EndToEnd.<LessonsAsync>g__Check|78_0("the full-screen replay plays its recorded voice", list2.Any<string>((string l) => l.Contains("with its recorded voice")));
              EndToEnd.<LessonsAsync>g__Check|78_0("lines are released in step with the voice", list2.Count<string>((string l) => l.Contains("[lessons] replay line")) >= 2 && CS$<>8__locals1.lessons.IsReplaying);
              byte[] array = CS$<>8__locals1.real.Canvas.RenderDrawingPng(null, 0.5, true, false);
              if (array != null)
              {
                await File.WriteAllBytesAsync(Path.Combine(this.outDir, "lessons-replay.png"), array, default(CancellationToken));
              }
              EndToEnd.PressKeys(new byte[] { 27 });
              await Task.Delay(1000);
              List<string> escapeLog = this.LogSince(mark);
              string text3 = "a real Escape stops the replay (StopReplay)";
              bool flag2;
              if (!CS$<>8__locals1.lessons.IsReplaying)
              {
                flag2 = escapeLog.Any<string>((string l) => l.Contains("replay stopped (Escape)"));
              }
              else
              {
                flag2 = false;
              }
              EndToEnd.<LessonsAsync>g__Check|78_0(text3, flag2);
              await Task.Delay(1500);
              EndToEnd.<LessonsAsync>g__Check|78_0("nothing more is released after Escape", this.LogSince(mark).Count<string>((string l) => l.Contains("[lessons] replay line")) == escapeLog.Count<string>((string l) => l.Contains("[lessons] replay line")));
            }
          }
        }
      }
    }

    // Token: 0x06002504 RID: 9476 RVA: 0x006393A8 File Offset: 0x006393A8
    [CompilerGenerated]
    internal static int <EscapeAsync>g__Count|1_0(ref EndToEnd.<>c__DisplayClass1_0 A_0)
    {
      if (!File.Exists(A_0.probeLog))
      {
        return 0;
      }
      return File.ReadAllLines(A_0.probeLog).Length;
    }

    // Token: 0x06002509 RID: 9481 RVA: 0x00639444 File Offset: 0x00639444
    [CompilerGenerated]
    internal static string <IntroNotchAsync>g__Shown|75_0(NotchPresenter n)
    {
      NotchPresentation.LiveEvent liveEvent = n.Presentation as NotchPresentation.LiveEvent;
      if (liveEvent == null)
      {
        return n.Presentation.GetType().Name;
      }
      return "LiveEvent." + liveEvent.Event.GetType().Name;
    }

    // Token: 0x0600250A RID: 9482 RVA: 0x0063948C File Offset: 0x0063948C
    [CompilerGenerated]
    internal static void <LessonsAsync>g__Check|78_0(string what, bool passed)
    {
      EndToEnd.Say("lessons: " + (passed ? "PASS" : "FAIL") + " " + what);
    }

    // Token: 0x0400119F RID: 4511
    private readonly HashSet<int> launchedNotepads = new HashSet<int>();

    // Token: 0x040011A0 RID: 4512
    private const byte VkLControl = 162;

    // Token: 0x040011A1 RID: 4513
    private const byte VkLMenu = 164;

    // Token: 0x040011A2 RID: 4514
    private const byte VkLWin = 91;

    // Token: 0x040011A3 RID: 4515
    private const byte VkLShift = 160;

    // Token: 0x040011A4 RID: 4516
    private const byte VkS = 83;

    // Token: 0x040011A5 RID: 4517
    private const byte VkEscape = 27;

    // Token: 0x040011A6 RID: 4518
    private const uint KeyUp = 2U;

    // Token: 0x040011A7 RID: 4519
    private readonly AppHost host;

    // Token: 0x040011A8 RID: 4520
    private readonly string outDir;

    // Token: 0x040011A9 RID: 4521
    [Nullable(2)]
    private static string sayFile;

    // Token: 0x040011AA RID: 4522
    private const string LessonQuestion = "Explain how a cache speeds up a website. Draw it on my screen in three or four short steps and call the drawing lessonTitle(\"How a cache works\").";

    // Token: 0x020009F6 RID: 2550
    [NullableContext(0)]
    private static class SyntheticInput
    {
      // Token: 0x06003F85 RID: 16261 RVA: 0x006A8990 File Offset: 0x006A8990
      public static bool Click(PointD screenPx)
      {
        if (!EndToEnd.SyntheticInput.SetCursorPos((int)Math.Round(screenPx.X), (int)Math.Round(screenPx.Y)))
        {
          return false;
        }
        Thread.Sleep(60);
        EndToEnd.SyntheticInput.mouse_event(2U, 0U, 0U, 0U, (UIntPtr)((IntPtr)0));
        Thread.Sleep(30);
        EndToEnd.SyntheticInput.mouse_event(4U, 0U, 0U, 0U, (UIntPtr)((IntPtr)0));
        return true;
      }

      // Token: 0x06003F86 RID: 16262 RVA: 0x006A89E8 File Offset: 0x006A89E8
      public static void PressEscape()
      {
        EndToEnd.SyntheticInput.keybd_event(27, 0, 0U, (UIntPtr)((IntPtr)0));
        EndToEnd.SyntheticInput.keybd_event(27, 0, 2U, (UIntPtr)((IntPtr)0));
      }

      // Token: 0x06003F87 RID: 16263
      [DllImport("user32.dll")]
      private static extern bool SetCursorPos(int x, int y);

      // Token: 0x06003F88 RID: 16264
      [DllImport("user32.dll")]
      private static extern void mouse_event(uint flags, uint dx, uint dy, uint data, UIntPtr extra);

      // Token: 0x06003F89 RID: 16265
      [DllImport("user32.dll")]
      private static extern void keybd_event(byte key, byte scan, uint flags, UIntPtr extra);

      // Token: 0x040029F2 RID: 10738
      private const uint MouseLeftDown = 2U;

      // Token: 0x040029F3 RID: 10739
      private const uint MouseLeftUp = 4U;

      // Token: 0x040029F4 RID: 10740
      private const uint KeyUp = 2U;

      // Token: 0x040029F5 RID: 10741
      private const byte VkEscape = 27;
    }

    // Token: 0x020009F7 RID: 2551
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040029F6 RID: 10742
      [Nullable(0)]
      public static Func<string, bool> <0>__Exists;

      // Token: 0x040029F7 RID: 10743
      [Nullable(0)]
      public static Func<string, string> <1>__GetFileName;
    }
  }
}
