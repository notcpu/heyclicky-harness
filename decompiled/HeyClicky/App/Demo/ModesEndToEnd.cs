using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.App.Interop;
using HeyClicky.App.Notch;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.Shell;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Notch;
using HeyClicky.Platform.Overlay;

namespace HeyClicky.App.Demo
{
  // Token: 0x02000354 RID: 852
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ModesEndToEnd
  {
    // Token: 0x0600250B RID: 9483 RVA: 0x006394B4 File Offset: 0x006394B4
    public ModesEndToEnd(AppHost host, string outDir)
    {
    }

    // Token: 0x0600250C RID: 9484 RVA: 0x006394CC File Offset: 0x006394CC
    private static void Say(string message)
    {
      Log.Info("e2e", "modes: " + message);
      try
      {
        string text = Path.Combine(Environment.GetEnvironmentVariable("HEYCLICKY_E2E_OUT") ?? Path.GetTempPath(), "e2e.log");
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 3);
        defaultInterpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now, "HH:mm:ss.fff");
        defaultInterpolatedStringHandler.AppendLiteral(" modes: ");
        defaultInterpolatedStringHandler.AppendFormatted(message);
        defaultInterpolatedStringHandler.AppendFormatted(Environment.NewLine);
        File.AppendAllText(text, defaultInterpolatedStringHandler.ToStringAndClear());
      }
      catch
      {
      }
    }

    // Token: 0x0600250D RID: 9485
    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    private static extern bool SetCursorPosRaw(int x, int y);

    // Token: 0x0600250E RID: 9486
    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int index);

    // Token: 0x0600250F RID: 9487 RVA: 0x00639568 File Offset: 0x00639568
    private static bool SetCursorPos(int x, int y)
    {
      int systemMetrics = ModesEndToEnd.GetSystemMetrics(76);
      int systemMetrics2 = ModesEndToEnd.GetSystemMetrics(77);
      int num = Math.Max(1, ModesEndToEnd.GetSystemMetrics(78) - 1);
      int num2 = Math.Max(1, ModesEndToEnd.GetSystemMetrics(79) - 1);
      int num3 = (int)Math.Round((double)(x - systemMetrics) * 65535.0 / (double)num);
      int num4 = (int)Math.Round((double)(y - systemMetrics2) * 65535.0 / (double)num2);
      ModesEndToEnd.mouse_event(49153U, num3, num4, 0U, (IntPtr)0);
      return true;
    }

    // Token: 0x06002510 RID: 9488
    [DllImport("user32.dll")]
    private static extern void mouse_event(uint flags, int dx, int dy, uint data, IntPtr extra);

    // Token: 0x06002511 RID: 9489 RVA: 0x006395EC File Offset: 0x006395EC
    private Task OnUi(Action a)
    {
      TaskCompletionSource tcs = new TaskCompletionSource();
      this.<host>P.DispatcherQueue.TryEnqueue(delegate
      {
        try
        {
          a();
          tcs.SetResult();
        }
        catch (Exception ex)
        {
          tcs.SetException(ex);
        }
      });
      return tcs.Task;
    }

    // Token: 0x06002512 RID: 9490 RVA: 0x0063963C File Offset: 0x0063963C
    private Task<T> OnUi<[Nullable(2)] T>(Func<T> f)
    {
      TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
      this.<host>P.DispatcherQueue.TryEnqueue(delegate
      {
        try
        {
          tcs.SetResult(f());
        }
        catch (Exception ex)
        {
          tcs.SetException(ex);
        }
      });
      return tcs.Task;
    }

    // Token: 0x06002513 RID: 9491 RVA: 0x0063968C File Offset: 0x0063968C
    private async Task Snap(string name)
    {
      string text = Path.Combine(this.<outDir>P, "snap-" + name + ".req");
      string done = Path.Combine(this.<outDir>P, "snap-" + name + ".done");
      try
      {
        File.Delete(done);
      }
      catch
      {
      }
      File.WriteAllText(text, name);
      Stopwatch sw = Stopwatch.StartNew();
      while (!File.Exists(done) && sw.ElapsedMilliseconds < 6000L)
      {
        await Task.Delay(50);
      }
      ModesEndToEnd.Say(File.Exists(done) ? ("screenshot " + name + ".png") : ("screenshot " + name + ": no watcher"));
    }

    // Token: 0x06002514 RID: 9492 RVA: 0x006396D8 File Offset: 0x006396D8
    private static async Task Move(int x, int y, int steps = 12, int ms = 10)
    {
      Native.POINT p;
      Native.GetCursorPos(out p);
      for (int i = 1; i <= steps; i++)
      {
        ModesEndToEnd.SetCursorPos(p.X + (x - p.X) * i / steps, p.Y + (y - p.Y) * i / steps);
        await Task.Delay(ms);
      }
    }

    // Token: 0x06002515 RID: 9493 RVA: 0x00639734 File Offset: 0x00639734
    private static async Task Click(int x, int y)
    {
      ModesEndToEnd.SetCursorPos(x, y);
      await Task.Delay(60);
      ModesEndToEnd.mouse_event(2U, 0, 0, 0U, (IntPtr)0);
      await Task.Delay(60);
      ModesEndToEnd.mouse_event(4U, 0, 0, 0U, (IntPtr)0);
    }

    // Token: 0x1700069E RID: 1694
    // (get) Token: 0x06002516 RID: 9494 RVA: 0x00639780 File Offset: 0x00639780
    private NotchPresentation Presentation
    {
      get
      {
        return this.<host>P.Services.Presenter.Presentation;
      }
    }

    // Token: 0x06002517 RID: 9495 RVA: 0x00639798 File Offset: 0x00639798
    private static string Describe(NotchPresentation p)
    {
      string text = NotchView.Describe(p);
      NotchPresentation.OnboardingPinned onboardingPinned = p as NotchPresentation.OnboardingPinned;
      string text2;
      if (onboardingPinned == null)
      {
        text2 = "";
      }
      else
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 1);
        defaultInterpolatedStringHandler.AppendLiteral("(");
        defaultInterpolatedStringHandler.AppendFormatted<int>(onboardingPinned.Step);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        text2 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      return text + text2;
    }

    // Token: 0x06002518 RID: 9496 RVA: 0x006397F8 File Offset: 0x006397F8
    private async Task<bool> WaitFor(Func<bool> condition, int timeoutMs, int pollMs = 50)
    {
      Stopwatch sw = Stopwatch.StartNew();
      while (sw.ElapsedMilliseconds < (long)timeoutMs)
      {
        if (condition())
        {
          return true;
        }
        await Task.Delay(pollMs);
      }
      return condition();
    }

    // Token: 0x06002519 RID: 9497 RVA: 0x0063984C File Offset: 0x0063984C
    public async Task RunAsync(Func<Task<bool>> signIn)
    {
      RealStack real = this.<host>P.Real;
      CursorOverlay cursor = real.Cursor.Inner;
      if (cursor == null)
      {
        ModesEndToEnd.Say("FAIL no cursor overlay (offscreen run?)");
      }
      else
      {
        Displays.Monitor i = Displays.Primary();
        int cx = i.Bounds.Left + i.Bounds.Width / 2;
        int cy = i.Bounds.Top + i.Bounds.Height / 2;
        double sc = i.Scale;
        Process backdrop = Process.Start(new ProcessStartInfo("powershell.exe", "-NoProfile -Command \"Add-Type -AssemblyName System.Windows.Forms; $f = New-Object System.Windows.Forms.Form; $f.WindowState = 'Maximized'; $f.BackColor = 'Gainsboro'; $f.Text = 'HeyClicky e2e backdrop'; $f.ShowDialog()\"")
        {
          UseShellExecute = false,
          CreateNoWindow = true
        });
        await Task.Delay(2500);
        try
        {
          await this.RunStepsAsync(signIn, real, cursor, i, cx, cy, sc);
        }
        finally
        {
          try
          {
            Process process = backdrop;
            if (process != null)
            {
              process.Kill(true);
            }
          }
          catch
          {
          }
        }
      }
    }

    // Token: 0x0600251A RID: 9498 RVA: 0x00639898 File Offset: 0x00639898
    private async Task RunStepsAsync(Func<Task<bool>> signIn, RealStack real, CursorOverlay cursor, Displays.Monitor m, int cx, int cy, double sc)
    {
      ModesEndToEnd.<>c__DisplayClass20_0 CS$<>8__locals1 = new ModesEndToEnd.<>c__DisplayClass20_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.cursor = cursor;
      CS$<>8__locals1.real = real;
      await ModesEndToEnd.Move(cx - 200, cy, 12, 10);
      Task said = this.<host>P.SayAtCursorAsync("Your report is ready. I saved it to your Documents folder, and the chart is on page two.", true, 3.0, default(CancellationToken));
      await Task.Delay(2600);
      ValueTuple<string, double, bool, bool, bool, double> valueTuple = CS$<>8__locals1.cursor.GetStateDiagnostics();
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 3);
      defaultInterpolatedStringHandler.AppendFormatted((valueTuple.Item1.Length > 20 && valueTuple.Item2 > 0.9) ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" message bubble streaming: \"");
      defaultInterpolatedStringHandler.AppendFormatted(valueTuple.Item1);
      defaultInterpolatedStringHandler.AppendLiteral("\" opacity ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(valueTuple.Item2, "0.00");
      ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      CS$<>8__locals1.cursor.SnapshotMessagePng(Path.Combine(this.<outDir>P, "cursor-message-offscreen.png"), "#F5F5F0");
      await this.Snap("01-message-bubble");
      await Task.WhenAny(said, Task.Delay(10000));
      valueTuple = CS$<>8__locals1.cursor.GetStateDiagnostics();
      ModesEndToEnd.Say(((said.IsCompleted && valueTuple.Item2 == 0.0) ? "PASS" : "FAIL") + " message bubble faded after its hold");
      this.<host>P.SayAtCursorAsync("Building that snake game.", false, 2.0, default(CancellationToken));
      await Task.Delay(500);
      ModesEndToEnd.Say(((CS$<>8__locals1.cursor.GetStateDiagnostics().Item1 == "Building that snake game.") ? "PASS" : "FAIL") + " start cue pops in whole");
      await this.Snap("02-start-cue");
      await Task.Delay(2600);
      Task welcome = this.<host>P.PlayCursorWelcomeAsync();
      await Task.Delay(800);
      valueTuple = CS$<>8__locals1.cursor.GetStateDiagnostics();
      ModesEndToEnd.Say(((valueTuple.Item1 == "hi, im heyclicky") ? "PASS" : "FAIL") + " welcome typed: \"" + valueTuple.Item1 + "\"");
      await this.Snap("03-welcome");
      await Task.WhenAny(welcome, Task.Delay(5000));
      ModesEndToEnd.Say((welcome.IsCompleted ? "PASS" : "FAIL") + " welcome finished");
      await this.OnUi<bool>(() => CS$<>8__locals1.<>4__this.<host>P.Services.Settings.ShakeToDetachEnabled = true);
      await Task.Delay(300);
      await ModesEndToEnd.Move(cx, cy, 12, 10);
      await Task.Delay(400);
      await ModesEndToEnd.Shake(cx, cy);
      bool detached = await this.WaitFor(() => CS$<>8__locals1.cursor.IsDetached, 1000, 50);
      PointD frozen = CS$<>8__locals1.cursor.GetDiagnostics().Item3;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 2);
      defaultInterpolatedStringHandler.AppendFormatted(detached ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" shake detached the cursor at ");
      defaultInterpolatedStringHandler.AppendFormatted<PointD>(frozen);
      ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      await ModesEndToEnd.Move(cx + 160, cy + 120, 12, 10);
      await Task.Delay(600);
      ModesEndToEnd.Say(((CS$<>8__locals1.cursor.GetDiagnostics().Item3 == frozen) ? "PASS" : "FAIL") + " detached cursor stays frozen while the mouse moves");
      await this.Snap("04-detached");
      await ModesEndToEnd.Move((int)frozen.X, (int)frozen.Y, 8, 10);
      bool flag = await this.WaitFor(() => CS$<>8__locals1.cursor.GetStateDiagnostics().Item4, 1500, 50);
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(71, 3);
      defaultInterpolatedStringHandler.AppendFormatted(flag ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" hovering the frozen cursor (click catcher; mouse at ");
      defaultInterpolatedStringHandler.AppendFormatted<PointD>(frozen);
      defaultInterpolatedStringHandler.AppendLiteral(", still detached ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.cursor.IsDetached);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      await this.Snap("05-detached-hover");
      await ModesEndToEnd.Move(cx + 300, cy + 200, 12, 10);
      TaskAwaiter<bool> taskAwaiter = this.WaitFor(() => !CS$<>8__locals1.cursor.IsDetached, 4500, 50).GetAwaiter();
      TaskAwaiter<bool> taskAwaiter2;
      if (!taskAwaiter.IsCompleted)
      {
        await taskAwaiter;
        taskAwaiter = taskAwaiter2;
        taskAwaiter2 = default(TaskAwaiter<bool>);
      }
      ModesEndToEnd.Say((taskAwaiter.GetResult() ? "PASS" : "FAIL") + " auto-reattached after 3 s without a click");
      await ModesEndToEnd.Move(cx, cy, 12, 10);
      await Task.Delay(300);
      await ModesEndToEnd.Shake(cx, cy);
      detached = await this.WaitFor(() => CS$<>8__locals1.cursor.IsDetached, 1000, 50);
      frozen = CS$<>8__locals1.cursor.GetDiagnostics().Item3;
      await ModesEndToEnd.Click((int)frozen.X, (int)frozen.Y);
      bool flag2 = await this.WaitFor(() => CS$<>8__locals1.real.Companion.State == VoiceState.Listening, 4000, 50);
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(55, 2);
      defaultInterpolatedStringHandler.AppendFormatted((detached && flag2) ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" click on the detached cursor starts listening (state ");
      defaultInterpolatedStringHandler.AppendFormatted<VoiceState>(CS$<>8__locals1.real.Companion.State);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      await Task.Delay(700);
      await this.Snap("06-detached-listening-pill");
      await ModesEndToEnd.Click((int)frozen.X, (int)frozen.Y);
      bool flag3 = await this.WaitFor(() => CS$<>8__locals1.real.Companion.State != VoiceState.Listening && !CS$<>8__locals1.cursor.IsDetached, 4000, 50);
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(53, 2);
      defaultInterpolatedStringHandler.AppendFormatted(flag3 ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" second click stops recording and reattaches (state ");
      defaultInterpolatedStringHandler.AppendFormatted<VoiceState>(CS$<>8__locals1.real.Companion.State);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      CS$<>8__locals1.real.Companion.Cancel();
      await Task.Delay(800);
      await ModesEndToEnd.Move(cx, cy, 12, 10);
      await Task.Delay(300);
      await ModesEndToEnd.Shake(cx, cy);
      detached = await this.WaitFor(() => CS$<>8__locals1.cursor.IsDetached, 1000, 50);
      frozen = CS$<>8__locals1.cursor.GetDiagnostics().Item3;
      await ModesEndToEnd.Click((int)frozen.X, (int)frozen.Y);
      await Task.Delay(150);
      CS$<>8__locals1.real.Companion.Cancel();
      await ModesEndToEnd.Move(cx + 300, cy + 200, 12, 10);
      bool flag4 = await this.WaitFor(() => !CS$<>8__locals1.cursor.IsDetached, 7000, 50);
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(90, 4);
      defaultInterpolatedStringHandler.AppendFormatted((detached && flag4) ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" a click whose recording ended at once still reattaches (detached ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(detached);
      defaultInterpolatedStringHandler.AppendLiteral(", now detached ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.cursor.IsDetached);
      defaultInterpolatedStringHandler.AppendLiteral(", state ");
      defaultInterpolatedStringHandler.AppendFormatted<VoiceState>(CS$<>8__locals1.real.Companion.State);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      await this.OnUi<bool>(() => CS$<>8__locals1.<>4__this.<host>P.Services.Settings.ShakeToDetachEnabled = false);
      await Task.Delay(1500);
      await ModesEndToEnd.Move(cx - 300, cy + 100, 12, 10);
      await Task.Delay(800);
      PointD beforeHide = CS$<>8__locals1.cursor.GetDiagnostics().Item3;
      await this.OnUi(delegate
      {
        CS$<>8__locals1.<>4__this.<host>P.SetCursorDocked(true);
      });
      bool goodbyeTyped = await this.WaitFor(() => CS$<>8__locals1.cursor.GetStateDiagnostics().Item1 == "okay see you <3", 2000, 50);
      await Task.Delay(300);
      ValueTuple<string, double, bool, bool, bool, double> stateDiagnostics = CS$<>8__locals1.cursor.GetStateDiagnostics();
      ValueTuple<long, long, PointD, string> valueTuple2 = CS$<>8__locals1.cursor.GetDiagnostics();
      bool flag5;
      if (Math.Abs(valueTuple2.Item3.X - beforeHide.X) < 4.0)
      {
        valueTuple2 = CS$<>8__locals1.cursor.GetDiagnostics();
        flag5 = Math.Abs(valueTuple2.Item3.Y - beforeHide.Y) < 4.0;
      }
      else
      {
        flag5 = false;
      }
      bool flag6 = flag5;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(147, 10);
      defaultInterpolatedStringHandler.AppendFormatted((goodbyeTyped && flag6 && !stateDiagnostics.Item5 && CS$<>8__locals1.cursor.IsSayingGoodbye && !this.<host>P.Services.Settings.CursorDocked && stateDiagnostics.Item6 > 0.9) ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" goodbye: \"");
      defaultInterpolatedStringHandler.AppendFormatted(stateDiagnostics.Item1);
      defaultInterpolatedStringHandler.AppendLiteral("\" beside the cursor where it was (no flight), not docked yet, opacity ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(stateDiagnostics.Item6, "0.00");
      defaultInterpolatedStringHandler.AppendLiteral(" (typed ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(goodbyeTyped);
      defaultInterpolatedStringHandler.AppendLiteral(", stayed ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(flag6);
      defaultInterpolatedStringHandler.AppendLiteral(": ");
      defaultInterpolatedStringHandler.AppendFormatted<PointD>(beforeHide);
      defaultInterpolatedStringHandler.AppendLiteral(" → ");
      defaultInterpolatedStringHandler.AppendFormatted<PointD>(CS$<>8__locals1.cursor.GetDiagnostics().Item3);
      defaultInterpolatedStringHandler.AppendLiteral(", docked ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(stateDiagnostics.Item5);
      defaultInterpolatedStringHandler.AppendLiteral(", saying goodbye ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.cursor.IsSayingGoodbye);
      defaultInterpolatedStringHandler.AppendLiteral(", setting docked ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(this.<host>P.Services.Settings.CursorDocked);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      await this.Snap("07-goodbye-bubble");
      await this.OnUi(delegate
      {
        CS$<>8__locals1.<>4__this.<host>P.SetCursorDocked(true);
      });
      ModesEndToEnd.Say(((CS$<>8__locals1.cursor.IsSayingGoodbye && !CS$<>8__locals1.cursor.IsDocked) ? "PASS" : "FAIL") + " a second hide during the goodbye is ignored");
      bool flag7 = await this.WaitFor(delegate
      {
        ValueTuple<string, double, bool, bool, bool, double> stateDiagnostics3 = CS$<>8__locals1.cursor.GetStateDiagnostics();
        double num4 = stateDiagnostics3.Item6;
        if (num4 > 0.15 && num4 < 0.75)
        {
          num4 = stateDiagnostics3.Item2;
          return num4 > 0.15 && num4 < 0.75;
        }
        return false;
      }, 3000, 15);
      ValueTuple<string, double, bool, bool, bool, double> stateDiagnostics2 = CS$<>8__locals1.cursor.GetStateDiagnostics();
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(51, 3);
      defaultInterpolatedStringHandler.AppendFormatted(flag7 ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" cursor and bubble fade together (cursor ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(stateDiagnostics2.Item6, "0.00");
      defaultInterpolatedStringHandler.AppendLiteral(", bubble ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(stateDiagnostics2.Item2, "0.00");
      defaultInterpolatedStringHandler.AppendLiteral(")");
      ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      await this.Snap("07b-goodbye-fade");
      bool docked = await this.WaitFor(() => CS$<>8__locals1.cursor.IsDocked && CS$<>8__locals1.<>4__this.<host>P.Services.Settings.CursorDocked, 3000, 50);
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 2);
      defaultInterpolatedStringHandler.AppendFormatted((docked && CS$<>8__locals1.cursor.GetStateDiagnostics().Item6 == 0.0) ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" docked only after the fade (opacity ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(CS$<>8__locals1.cursor.GetStateDiagnostics().Item6, "0.00");
      defaultInterpolatedStringHandler.AppendLiteral(")");
      ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      await Task.Delay(600);
      NotchPresentation presentation = this.Presentation;
      bool flag8;
      if (!(presentation is NotchPresentation.Idle))
      {
        NotchPresentation.LiveEvent liveEvent = presentation as NotchPresentation.LiveEvent;
        if (liveEvent == null || !(liveEvent.Event is NotchLiveEvent.RestingMark))
        {
          flag8 = false;
          goto IL_22FC;
        }
      }
      flag8 = true;
      IL_22FC:
      bool flag9 = flag8;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 4);
      defaultInterpolatedStringHandler.AppendFormatted((docked && flag9) ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" hidden: cursor gone, notch at rest ");
      defaultInterpolatedStringHandler.AppendFormatted(ModesEndToEnd.Describe(this.Presentation));
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      NotchPresentation.LiveEvent liveEvent2 = this.Presentation as NotchPresentation.LiveEvent;
      defaultInterpolatedStringHandler.AppendFormatted<NotchLiveEvent>((liveEvent2 != null) ? liveEvent2.Event : null);
      defaultInterpolatedStringHandler.AppendLiteral(", opacity ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(CS$<>8__locals1.cursor.GetStateDiagnostics().Item6, "0.00");
      ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      await ModesEndToEnd.Move(cx + 200, cy + 100, 12, 10);
      await Task.Delay(500);
      ModesEndToEnd.Say(((CS$<>8__locals1.cursor.GetStateDiagnostics().Item6 == 0.0) ? "PASS" : "FAIL") + " docked cursor hidden while the mouse moves");
      await this.Snap("08-docked");
      this.<host>P.SayAtCursorAsync("Docked, but I still talk beside your cursor.", false, 1.5, default(CancellationToken));
      await Task.Delay(600);
      ModesEndToEnd.Say(((CS$<>8__locals1.cursor.GetStateDiagnostics().Item6 > 0.5) ? "PASS" : "FAIL") + " docked cursor comes out to anchor a message");
      await this.Snap("09-docked-message");
      await Task.Delay(2500);
      await this.OnUi(delegate
      {
        CS$<>8__locals1.<>4__this.<host>P.SetCursorDocked(false);
      });
      bool undocked = await this.WaitFor(() => !CS$<>8__locals1.cursor.IsDocked && CS$<>8__locals1.cursor.GetDiagnostics().Item4 == "FollowingCursor", 3000, 50);
      await Task.Delay(500);
      await this.Snap("10-shown-again");
      PointD item = CS$<>8__locals1.cursor.GetDiagnostics().Item3;
      double num = (double)(cx + 200) + 35.0 * sc;
      double num2 = (double)(cy + 100) + 25.0 * sc;
      bool flag10 = Math.Abs(item.X - num) < 4.0 * sc && Math.Abs(item.Y - num2) < 4.0 * sc;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(68, 6);
      defaultInterpolatedStringHandler.AppendFormatted((undocked && flag10 && CS$<>8__locals1.cursor.GetStateDiagnostics().Item6 > 0.9) ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" shown again: back beside the mouse (");
      defaultInterpolatedStringHandler.AppendFormatted<PointD>(item);
      defaultInterpolatedStringHandler.AppendLiteral(", expected ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(num, "0");
      defaultInterpolatedStringHandler.AppendLiteral(",");
      defaultInterpolatedStringHandler.AppendFormatted<double>(num2, "0");
      defaultInterpolatedStringHandler.AppendLiteral("), opacity ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(CS$<>8__locals1.cursor.GetStateDiagnostics().Item6, "0.00");
      defaultInterpolatedStringHandler.AppendLiteral(", notch ");
      defaultInterpolatedStringHandler.AppendFormatted(ModesEndToEnd.Describe(this.Presentation));
      ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
      await CS$<>8__locals1.real.Core.Auth.SignOutAsync();
      await Task.Delay(800);
      await ModesEndToEnd.Move(cx, m.Bounds.Top + 3, 12, 10);
      taskAwaiter = this.WaitFor(delegate
      {
        NotchPresentation.OnboardingPinned onboardingPinned = CS$<>8__locals1.<>4__this.Presentation as NotchPresentation.OnboardingPinned;
        return onboardingPinned != null && onboardingPinned.Step == -1;
      }, 2500, 50).GetAwaiter();
      if (!taskAwaiter.IsCompleted)
      {
        await taskAwaiter;
        taskAwaiter = taskAwaiter2;
        taskAwaiter2 = default(TaskAwaiter<bool>);
      }
      ModesEndToEnd.Say((taskAwaiter.GetResult() ? "PASS" : "FAIL") + " signed-out hover shows the sign-in card (" + ModesEndToEnd.Describe(this.Presentation) + ")");
      await this.Snap("11-signed-out-hover");
      await ModesEndToEnd.Move(cx, cy, 12, 10);
      await Task.Delay(500);
      taskAwaiter = signIn().GetAwaiter();
      if (!taskAwaiter.IsCompleted)
      {
        await taskAwaiter;
        taskAwaiter = taskAwaiter2;
        taskAwaiter2 = default(TaskAwaiter<bool>);
      }
      if (!taskAwaiter.GetResult())
      {
        ModesEndToEnd.Say("FAIL could not sign back in");
      }
      else
      {
        await Task.Delay(1500);
        await this.OnUi(delegate
        {
          CS$<>8__locals1.<>4__this.<host>P.Services.Presenter.Present(new NotchPresentation.TextInput("", null));
        });
        await Task.Delay(500);
        CS$<>8__locals1.real.Companion.SubmitTextAsync("What's 2+2? One short sentence, no drawing.");
        bool answered = await this.WaitFor(delegate
        {
          NotchPresenter notchPresenter4 = CS$<>8__locals1.<>4__this.<host>P.Services.Presenter as NotchPresenter;
          ValueTuple<TimeSpan, TimeSpan, bool>? valueTuple5 = ((notchPresenter4 != null) ? notchPresenter4.TextResponseCountdown : null);
          return valueTuple5 != null && CS$<>8__locals1.real.Companion.State == VoiceState.Idle;
        }, 60000, 200);
        await Task.Delay(800);
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 2);
        defaultInterpolatedStringHandler.AppendFormatted(answered ? "PASS" : "FAIL");
        defaultInterpolatedStringHandler.AppendLiteral(" answer countdown armed: ");
        NotchPresenter notchPresenter = this.<host>P.Services.Presenter as NotchPresenter;
        ValueTuple<TimeSpan, TimeSpan, bool>? valueTuple4;
        if (notchPresenter == null)
        {
          ValueTuple<TimeSpan, TimeSpan, bool>? valueTuple3 = (valueTuple4 = null);
        }
        else
        {
          valueTuple4 = notchPresenter.TextResponseCountdown;
        }
        defaultInterpolatedStringHandler.AppendFormatted<ValueTuple<TimeSpan, TimeSpan, bool>?>(valueTuple4);
        ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        await this.Snap("12-answer-clock");
        await ModesEndToEnd.Move(cx, m.Bounds.Top + 40, 12, 10);
        await Task.Delay(900);
        await this.Snap("13-answer-hover-pills");
        NotchPresenter notchPresenter2 = this.<host>P.Services.Presenter as NotchPresenter;
        TimeSpan? timeSpan;
        if (notchPresenter2 == null)
        {
          timeSpan = null;
        }
        else
        {
          ValueTuple<TimeSpan, TimeSpan, bool>? valueTuple3 = notchPresenter2.TextResponseCountdown;
          timeSpan = ((valueTuple3 != null) ? new TimeSpan?(valueTuple3.GetValueOrDefault().Item1) : null);
        }
        TimeSpan? before = timeSpan;
        await Task.Delay(1000);
        NotchPresenter notchPresenter3 = this.<host>P.Services.Presenter as NotchPresenter;
        TimeSpan? timeSpan2;
        if (notchPresenter3 == null)
        {
          timeSpan2 = null;
        }
        else
        {
          ValueTuple<TimeSpan, TimeSpan, bool>? valueTuple3 = notchPresenter3.TextResponseCountdown;
          timeSpan2 = ((valueTuple3 != null) ? new TimeSpan?(valueTuple3.GetValueOrDefault().Item1) : null);
        }
        TimeSpan? timeSpan3 = timeSpan2;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 3);
        defaultInterpolatedStringHandler.AppendFormatted((before == timeSpan3) ? "PASS" : "FAIL");
        defaultInterpolatedStringHandler.AppendLiteral(" countdown holds while hovered (");
        defaultInterpolatedStringHandler.AppendFormatted<TimeSpan?>(before);
        defaultInterpolatedStringHandler.AppendLiteral(" → ");
        defaultInterpolatedStringHandler.AppendFormatted<TimeSpan?>(timeSpan3);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        await this.OnUi(delegate
        {
          NotchPresenter notchPresenter5 = CS$<>8__locals1.<>4__this.<host>P.Services.Presenter as NotchPresenter;
          if (notchPresenter5 == null)
          {
            return;
          }
          notchPresenter5.PauseTextResponseAutoDismiss();
        });
        await ModesEndToEnd.Move(cx, cy, 12, 10);
        await Task.Delay(800);
        await this.Snap("14-answer-paused");
        await this.OnUi(delegate
        {
          CS$<>8__locals1.<>4__this.<host>P.Services.Presenter.Present(new NotchPresentation.Idle());
        });
        await Task.Delay(800);
        CS$<>8__locals1.agents = CS$<>8__locals1.real.Agents;
        CS$<>8__locals1.presenter = this.<host>P.Services.Presenter as NotchPresenter;
        CS$<>8__locals1.a = await CS$<>8__locals1.agents.LaunchAsync(new AgentLaunchRequest
        {
          Prompt = "Create a file named notch-a.txt in the output folder containing the word alpha. Keep it quick.",
          NewClickyName = "Strip Tester",
          LaunchSource = "text"
        }, default(CancellationToken));
        CS$<>8__locals1.b = await CS$<>8__locals1.agents.LaunchAsync(new AgentLaunchRequest
        {
          Prompt = "Create a file named notch-b.txt in the output folder containing the word beta. Keep it quick.",
          NewClickyName = "Corner Tester",
          LaunchSource = "text"
        }, default(CancellationToken));
        taskAwaiter = this.WaitFor(delegate
        {
          NotchPresentation.LiveEvent liveEvent3 = CS$<>8__locals1.<>4__this.Presentation as NotchPresentation.LiveEvent;
          return liveEvent3 != null && liveEvent3.Event is NotchLiveEvent.RestingMark;
        }, 5000, 50).GetAwaiter();
        if (!taskAwaiter.IsCompleted)
        {
          await taskAwaiter;
          taskAwaiter = taskAwaiter2;
          taskAwaiter2 = default(TaskAwaiter<bool>);
        }
        ModesEndToEnd.Say((taskAwaiter.GetResult() ? "PASS" : "FAIL") + " working Clickys show the resting mark (" + ModesEndToEnd.Describe(this.Presentation) + ")");
        await Task.Delay(1500);
        await this.Snap("15-resting-mark");
        CS$<>8__locals1.steers = 0;
        CS$<>8__locals1.agents.TaskEvent += CS$<>8__locals1.<RunStepsAsync>g__OnSteer|8;
        try
        {
          await CS$<>8__locals1.agents.SteerAsync(CS$<>8__locals1.a, "Also add a second line that says done", default(CancellationToken));
        }
        catch (Exception ex)
        {
          ModesEndToEnd.Say("steer: " + ex.Message);
        }
        await Task.Delay(500);
        CS$<>8__locals1.agents.TaskEvent -= CS$<>8__locals1.<RunStepsAsync>g__OnSteer|8;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(68, 2);
        defaultInterpolatedStringHandler.AppendFormatted((CS$<>8__locals1.steers > 0) ? "PASS" : "INFO");
        defaultInterpolatedStringHandler.AppendLiteral(" steer raised SteerAccepted x");
        defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.steers);
        defaultInterpolatedStringHandler.AppendLiteral(" (only when the turn was still running)");
        ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        taskAwaiter = this.WaitFor(delegate
        {
          IEnumerable<AgentTaskSnapshot> tasks = CS$<>8__locals1.agents.Tasks;
          Func<AgentTaskSnapshot, bool> func;
          if ((func = CS$<>8__locals1.<>9__28) == null)
          {
            func = (CS$<>8__locals1.<>9__28 = (AgentTaskSnapshot t) => t.TaskId == CS$<>8__locals1.a || t.TaskId == CS$<>8__locals1.b);
          }
          return tasks.Where<AgentTaskSnapshot>(func).All<AgentTaskSnapshot>(delegate(AgentTaskSnapshot t)
          {
            AgentTaskStatus status = t.Status;
            return status - AgentTaskStatus.Finished <= 2;
          });
        }, 150000, 500).GetAwaiter();
        if (!taskAwaiter.IsCompleted)
        {
          await taskAwaiter;
          taskAwaiter = taskAwaiter2;
          taskAwaiter2 = default(TaskAwaiter<bool>);
        }
        ModesEndToEnd.Say((taskAwaiter.GetResult() ? "PASS" : "FAIL") + " agents finished; notch " + ModesEndToEnd.Describe(this.Presentation));
        taskAwaiter = this.WaitFor(delegate
        {
          NotchPresentation.LiveEvent liveEvent4 = CS$<>8__locals1.<>4__this.Presentation as NotchPresentation.LiveEvent;
          return liveEvent4 != null && liveEvent4.Event is NotchLiveEvent.ClickyUpdate;
        }, 60000, 250).GetAwaiter();
        if (!taskAwaiter.IsCompleted)
        {
          await taskAwaiter;
          taskAwaiter = taskAwaiter2;
          taskAwaiter2 = default(TaskAwaiter<bool>);
        }
        ModesEndToEnd.Say((taskAwaiter.GetResult() ? "PASS" : "FAIL") + " the notch unfolds the update card (" + ModesEndToEnd.Describe(this.Presentation) + ")");
        await this.Snap("16-update-card");
        ChatHandOffs handOffs = NotchChat.HandOffs;
        int num3 = ((handOffs != null) ? handOffs.Entries.Count : 0);
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 2);
        defaultInterpolatedStringHandler.AppendFormatted((num3 > 0) ? "PASS" : "FAIL");
        defaultInterpolatedStringHandler.AppendLiteral(" the chat holds ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num3);
        defaultInterpolatedStringHandler.AppendLiteral(" hand-off event(s)");
        ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        await ModesEndToEnd.Move(cx - 100, cy - 50, 12, 10);
        bool flag11 = await this.WaitFor(() => CS$<>8__locals1.cursor.GetStateDiagnostics().Item2 > 0.5 && CS$<>8__locals1.cursor.GetStateDiagnostics().Item1.Length > 0, 12000, 50);
        ModesEndToEnd.Say((flag11 ? "PASS" : "INFO") + " agent finish line beside the cursor: \"" + CS$<>8__locals1.cursor.GetStateDiagnostics().Item1 + "\"");
        if (flag11)
        {
          await this.Snap("17-finish-line");
        }
        await this.OnUi(delegate
        {
          NotchPresenter presenter2 = CS$<>8__locals1.presenter;
          if (presenter2 == null)
          {
            return;
          }
          presenter2.Updates.Reset();
        });
        await Task.Delay(1500);
        await ModesEndToEnd.Move(cx, m.Bounds.Top + 3, 12, 10);
        bool flag12 = await this.WaitFor(delegate
        {
          NotchPresentation.LiveEvent liveEvent5 = CS$<>8__locals1.<>4__this.Presentation as NotchPresentation.LiveEvent;
          return liveEvent5 != null && liveEvent5.Event is NotchLiveEvent.AgentPeek;
        }, 2500, 50);
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(31, 3);
        defaultInterpolatedStringHandler.AppendFormatted(flag12 ? "PASS" : "FAIL");
        defaultInterpolatedStringHandler.AppendLiteral(" hover opens the notch (");
        defaultInterpolatedStringHandler.AppendFormatted(ModesEndToEnd.Describe(this.Presentation));
        defaultInterpolatedStringHandler.AppendLiteral(", tab ");
        NotchPresenter presenter = CS$<>8__locals1.presenter;
        defaultInterpolatedStringHandler.AppendFormatted<NotchTab?>((presenter != null) ? new NotchTab?(presenter.Tabs.Selected) : null);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        await ModesEndToEnd.Move(cx, m.Bounds.Top + 60, 6, 10);
        await Task.Delay(900);
        await this.Snap("18-notch-home");
        await this.OnUi(delegate
        {
          NotchPresenter presenter3 = CS$<>8__locals1.presenter;
          if (presenter3 == null)
          {
            return;
          }
          presenter3.Tabs.Select(NotchTab.Chat);
        });
        await Task.Delay(900);
        await this.Snap("19-notch-chat");
        await ModesEndToEnd.Move(cx, cy, 12, 10);
        taskAwaiter = this.WaitFor(delegate
        {
          NotchPresenter presenter4 = CS$<>8__locals1.presenter;
          return presenter4 != null && !presenter4.IsShowingNotchAgentPeek;
        }, 3000, 50).GetAwaiter();
        if (!taskAwaiter.IsCompleted)
        {
          await taskAwaiter;
          taskAwaiter = taskAwaiter2;
          taskAwaiter2 = default(TaskAwaiter<bool>);
        }
        ModesEndToEnd.Say((taskAwaiter.GetResult() ? "PASS" : "FAIL") + " the notch folds when the pointer leaves");
        await Task.Delay(800);
        Process fullscreen = Process.Start(new ProcessStartInfo("powershell.exe", "-NoProfile -Command \"Add-Type -AssemblyName System.Windows.Forms; $f = New-Object System.Windows.Forms.Form; $f.FormBorderStyle = 'None'; $f.WindowState = 'Maximized'; $f.BackColor = 'DarkSlateGray'; $f.Text = 'Fullscreen test'; $f.ShowDialog()\"")
        {
          UseShellExecute = false,
          CreateNoWindow = true
        });
        bool flag13 = await this.WaitFor(delegate
        {
          NotchPresenter presenter5 = CS$<>8__locals1.presenter;
          return presenter5 != null && presenter5.IsAmbientSuppressed;
        }, 8000, 200);
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(63, 3);
        defaultInterpolatedStringHandler.AppendFormatted(flag13 ? "PASS" : "FAIL");
        defaultInterpolatedStringHandler.AppendLiteral(" fullscreen app: ambient states hidden (notch ");
        defaultInterpolatedStringHandler.AppendFormatted(ModesEndToEnd.Describe(this.Presentation));
        defaultInterpolatedStringHandler.AppendLiteral(", sliver hidden ");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(this.<host>P.NotchUi.IsAmbientHidden);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        ModesEndToEnd.Say(defaultInterpolatedStringHandler.ToStringAndClear());
        await this.Snap("20-fullscreen");
        try
        {
          Process process = fullscreen;
          if (process != null)
          {
            process.Kill(true);
          }
        }
        catch
        {
        }
        taskAwaiter = this.WaitFor(delegate
        {
          NotchPresenter presenter6 = CS$<>8__locals1.presenter;
          return presenter6 != null && !presenter6.IsAmbientSuppressed;
        }, 4000, 200).GetAwaiter();
        if (!taskAwaiter.IsCompleted)
        {
          await taskAwaiter;
          taskAwaiter = taskAwaiter2;
          taskAwaiter2 = default(TaskAwaiter<bool>);
        }
        ModesEndToEnd.Say((taskAwaiter.GetResult() ? "PASS" : "FAIL") + " fullscreen ended: ambient states back (" + ModesEndToEnd.Describe(this.Presentation) + ")");
        await this.Snap("21-after-fullscreen");
        await this.OnUi(delegate
        {
          CS$<>8__locals1.agents.DismissTask(CS$<>8__locals1.a);
          CS$<>8__locals1.agents.DismissTask(CS$<>8__locals1.b);
        });
      }
    }

    // Token: 0x0600251B RID: 9499 RVA: 0x00639918 File Offset: 0x00639918
    private static async Task Shake(int x, int y)
    {
      for (int i = 0; i < 16; i++)
      {
        ModesEndToEnd.SetCursorPos(x + ((i % 2 == 0) ? 70 : (-70)), y + ((i % 3 == 0) ? 6 : (-6)));
        await Task.Delay(18);
      }
      ModesEndToEnd.SetCursorPos(x, y);
    }

    // Token: 0x040011AB RID: 4523
    [CompilerGenerated]
    private AppHost <host>P = host;

    // Token: 0x040011AC RID: 4524
    [CompilerGenerated]
    private string <outDir>P = outDir;

    // Token: 0x040011AD RID: 4525
    private const uint LeftDown = 2U;

    // Token: 0x040011AE RID: 4526
    private const uint LeftUp = 4U;
  }
}
