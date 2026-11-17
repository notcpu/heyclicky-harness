using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Portraits;
using HeyClicky.App.UI;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Notch;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x0200011C RID: 284
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class HomeTour
  {
    // Token: 0x170002C9 RID: 713
    // (get) Token: 0x06000F3F RID: 3903 RVA: 0x00579B34 File Offset: 0x00579B34
    // (set) Token: 0x06000F40 RID: 3904 RVA: 0x00579B3C File Offset: 0x00579B3C
    public bool IsRunning { get; private set; }

    // Token: 0x170002CA RID: 714
    // (get) Token: 0x06000F41 RID: 3905 RVA: 0x00579B48 File Offset: 0x00579B48
    // (set) Token: 0x06000F42 RID: 3906 RVA: 0x00579B50 File Offset: 0x00579B50
    public bool WasAborted { get; private set; }

    // Token: 0x170002CB RID: 715
    // (get) Token: 0x06000F43 RID: 3907 RVA: 0x00579B5C File Offset: 0x00579B5C
    // (set) Token: 0x06000F44 RID: 3908 RVA: 0x00579B64 File Offset: 0x00579B64
    public bool IsDebugPreview { get; set; }

    // Token: 0x06000F45 RID: 3909 RVA: 0x00579B70 File Offset: 0x00579B70
    public HomeTour(AppHost host, [Nullable(2)] InterviewController interview)
    {
      this.host = host;
      this.interview = interview;
      this.dq = DispatcherQueue.GetForCurrentThread();
    }

    // Token: 0x170002CC RID: 716
    // (get) Token: 0x06000F46 RID: 3910 RVA: 0x00579BC0 File Offset: 0x00579BC0
    [Nullable(2)]
    private NotchPresenter Presenter
    {
      [NullableContext(2)]
      get
      {
        return this.host.Services.Presenter as NotchPresenter;
      }
    }

    // Token: 0x06000F47 RID: 3911 RVA: 0x00579BD8 File Offset: 0x00579BD8
    public void AbortForEscape()
    {
      if (!this.IsRunning || this.WasAborted)
      {
        return;
      }
      this.WasAborted = true;
      Log.Info("tour", "reveal aborted (double Escape)");
      this.abort.Cancel();
      RealStack real = this.host.Real;
      if (real != null)
      {
        real.Companion.Mouth.Interrupt();
      }
      this.FoldNotch();
      NotchPresenter presenter = this.Presenter;
      if (presenter == null)
      {
        return;
      }
      presenter.DismissNotchAgentPeek();
    }

    // Token: 0x06000F48 RID: 3912 RVA: 0x00579C50 File Offset: 0x00579C50
    private void OpenNotch()
    {
      Sounds.Play("home-reveal", 0.7);
      NotchPresenter presenter = this.Presenter;
      if (presenter == null)
      {
        return;
      }
      presenter.Tabs.Select(NotchTab.Home);
      presenter.OpenNotchAgentPeekOnRequest();
      presenter.SetNotchHeldOpenForPresentation(true);
      this.holdingNotch = true;
      Log.Info("tour", "reveal: notch opened on Home; introducing");
    }

    // Token: 0x06000F49 RID: 3913 RVA: 0x00579CAC File Offset: 0x00579CAC
    private void FoldNotch()
    {
      if (!this.holdingNotch)
      {
        return;
      }
      this.holdingNotch = false;
      NotchPresenter presenter = this.Presenter;
      if (presenter != null)
      {
        presenter.SetNotchHeldOpenForPresentation(false);
      }
      NotchPresenter presenter2 = this.Presenter;
      if (presenter2 == null)
      {
        return;
      }
      presenter2.DismissNotchAgentPeek();
    }

    // Token: 0x06000F4A RID: 3914 RVA: 0x00579CE0 File Offset: 0x00579CE0
    public Task<bool> RunAsync()
    {
      HomeTour.<RunAsync>d__32 <RunAsync>d__;
      <RunAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
      <RunAsync>d__.<>4__this = this;
      <RunAsync>d__.<>1__state = -1;
      <RunAsync>d__.<>t__builder.Start<HomeTour.<RunAsync>d__32>(ref <RunAsync>d__);
      return <RunAsync>d__.<>t__builder.Task;
    }

    // Token: 0x06000F4B RID: 3915 RVA: 0x00579D24 File Offset: 0x00579D24
    private static bool Fits(HomeTour.Beat beat, string line, int written, int expected)
    {
      if (beat.Key.StartsWith("clicky:", StringComparison.Ordinal))
      {
        string text = beat.Fallback.Split('.', StringSplitOptions.None)[0];
        return text != null && text.Length > 0 && line.Contains(text, StringComparison.OrdinalIgnoreCase);
      }
      return written == expected;
    }

    // Token: 0x06000F4C RID: 3916 RVA: 0x00579D70 File Offset: 0x00579D70
    private int Speak(string line, bool voiceReady)
    {
      object obj = this.gate;
      int num3;
      lock (obj)
      {
        int num = 4000;
        int num2 = this.nextLine;
        this.nextLine = num2 + 1;
        num3 = num + num2;
      }
      obj = this.gate;
      lock (obj)
      {
        this.audioQueued[num3] = new TaskCompletionSource<double>(TaskCreationOptions.RunContinuationsAsynchronously);
      }
      string text = "tour";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 2);
      defaultInterpolatedStringHandler.AppendLiteral("line ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(num3 - 4000);
      defaultInterpolatedStringHandler.AppendLiteral(": ");
      defaultInterpolatedStringHandler.AppendFormatted(line);
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      if (voiceReady)
      {
        this.host.Real.Companion.Mouth.SpeakLine(line, num3);
      }
      else
      {
        obj = this.gate;
        lock (obj)
        {
          this.audioQueued[num3].TrySetResult(0.0);
        }
        this.noVoiceUntil = Environment.TickCount64 + (long)(Math.Max(2.5, (double)line.Length / 15.0) * 1000.0);
      }
      return num3;
    }

    // Token: 0x06000F4D RID: 3917 RVA: 0x00579EE4 File Offset: 0x00579EE4
    private void OnLineAudioQueued(int index, double offset)
    {
      object obj = this.gate;
      TaskCompletionSource<double> taskCompletionSource;
      lock (obj)
      {
        this.audioQueued.TryGetValue(index, out taskCompletionSource);
      }
      RealStack real = this.host.Real;
      double num = ((real != null) ? real.Audio.Buffered.TotalSeconds : 0.0);
      if (taskCompletionSource != null)
      {
        taskCompletionSource.TrySetResult(Math.Max(0.0, num - 0.1));
      }
    }

    // Token: 0x06000F4E RID: 3918 RVA: 0x00579F80 File Offset: 0x00579F80
    private async Task<double?> PlaybackStartAsync(int index, double timeoutSeconds, CancellationToken ct)
    {
      object obj = this.gate;
      TaskCompletionSource<double> tcs;
      lock (obj)
      {
        tcs = this.audioQueued[index];
      }
      Task<double> task = await Task.WhenAny(tcs.Task, Task.Delay(TimeSpan.FromSeconds(timeoutSeconds), ct));
      ct.ThrowIfCancellationRequested();
      double? num;
      if (task != tcs.Task)
      {
        num = null;
      }
      else
      {
        double wait = tcs.Task.Result;
        if (wait > 0.0)
        {
          await Task.Delay(TimeSpan.FromSeconds(wait), ct);
        }
        num = new double?(wait);
      }
      return num;
    }

    // Token: 0x06000F4F RID: 3919 RVA: 0x00579FDC File Offset: 0x00579FDC
    private async Task WaitForLineToPlayOutAsync(int index, CancellationToken ct, bool last)
    {
      if (!last)
      {
        RealStack real = this.host.Real;
        if (real != null)
        {
          if (this.noVoiceUntil <= 0L)
          {
            long started = Environment.TickCount64;
            while (Environment.TickCount64 - started < 60000L)
            {
              ct.ThrowIfCancellationRequested();
              if (!real.Companion.Mouth.IsResponseInFlight && real.Audio.Buffered < TimeSpan.FromMilliseconds(250L, 0L))
              {
                break;
              }
              await Task.Delay(40, ct);
            }
            return;
          }
        }
        while (Environment.TickCount64 < this.noVoiceUntil)
        {
          await Task.Delay(50, ct);
        }
      }
    }

    // Token: 0x06000F50 RID: 3920 RVA: 0x0057A030 File Offset: 0x0057A030
    private async Task WaitForQuietAsync(double maxSeconds, CancellationToken ct)
    {
      RealStack real = this.host.Real;
      if (real == null)
      {
        while (Environment.TickCount64 < this.noVoiceUntil)
        {
          await Task.Delay(50, ct);
        }
      }
      else
      {
        long started = Environment.TickCount64;
        long? quietSince = null;
        while ((double)(Environment.TickCount64 - started) / 1000.0 < maxSeconds)
        {
          if (real.Companion.Mouth.IsResponseInFlight || real.Audio.Buffered > TimeSpan.FromMilliseconds(60L, 0L))
          {
            quietSince = null;
          }
          else
          {
            long tickCount = Environment.TickCount64;
            long num = quietSince.GetValueOrDefault();
            long num2;
            if (quietSince == null)
            {
              num = Environment.TickCount64;
              quietSince = new long?(num);
              num2 = num;
            }
            else
            {
              num2 = num;
            }
            if (tickCount - num2 >= 200L)
            {
              break;
            }
          }
          await Task.Delay(40, ct);
        }
      }
    }

    // Token: 0x06000F51 RID: 3921 RVA: 0x0057A084 File Offset: 0x0057A084
    [return: Nullable(new byte[] { 1, 2, 1 })]
    private async Task<Dictionary<int, string>> WriteLinesAsync(RealStack real, List<HomeTour.Beat> beats, CancellationToken ct)
    {
      string text = "[App] The welcome tour's last part is starting: their Home is opening in the notch, and you narrate it one beat at a time. You already said the opener: \"Okay, check this out. So, I live in your PC's notch. It looks like this.\" Write every beat's line now, in one go.\n[Home tour] You are showing this person their new Clickys, one beat at a time, while their Home is open in the PC's notch. Each \"[Home tour beat]\" gives only the facts for that beat. Use your own warm, plain words and their interview context, but add no extra explanation, recap, promises, or transition sentence. Follow each beat's word limit; otherwise use at most 25 words in one or two short sentences, then stop. Say each Clicky's name exactly. A Clicky is an AI agent that does work for them; explain this only in the introduction. After that, call them Clickys. The personalized Clickys are the focus: keep the defaults brief. Do not read instructions aloud or mention beats or tour mechanics. No lists, dashes, or questions. Finish with the short goodbye you are given.\n" + string.Join("\n", beats.Select<HomeTour.Beat, string>(delegate(HomeTour.Beat b, int i)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(22, 3);
        defaultInterpolatedStringHandler2.AppendLiteral("[Home tour beat ");
        defaultInterpolatedStringHandler2.AppendFormatted<int>(i + 1);
        defaultInterpolatedStringHandler2.AppendLiteral(" of ");
        defaultInterpolatedStringHandler2.AppendFormatted<int>(beats.Count);
        defaultInterpolatedStringHandler2.AppendLiteral("] ");
        defaultInterpolatedStringHandler2.AppendFormatted(b.Brief);
        return defaultInterpolatedStringHandler2.ToStringAndClear();
      })) + "\nAnswer with ONE ```js block holding, for each beat n in order, step(n) then exactly one say(\"…\") with that beat's whole line, and nothing else: nothing drawn (no marks, cards, photos or buttons), no tool calls.";
      Stopwatch sw = Stopwatch.StartNew();
      string text2 = await real.Companion.WriteSpokenProgramAsync(text, ct);
      Dictionary<int, string> dictionary = ((text2 == null) ? null : SayLines.ByStep(text2));
      if (text2 != null)
      {
        Log.Info("tour", "written program: " + text2.Replace('\n', ' ').Substring(0, Math.Min(text2.Length, 1500)));
      }
      string text3 = "tour";
      string text4;
      if (dictionary != null)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 2);
        defaultInterpolatedStringHandler.AppendLiteral("one brain wrote ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(dictionary.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" line(s) in ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(sw.Elapsed.TotalSeconds, "0.0");
        defaultInterpolatedStringHandler.AppendLiteral(" s");
        text4 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text4 = "lines not written (fallbacks)";
      }
      Log.Info(text3, text4);
      return dictionary;
    }

    // Token: 0x06000F52 RID: 3922 RVA: 0x0057A0D8 File Offset: 0x0057A0D8
    private static string IntroBrief(IReadOnlyList<RosterClicky> cast, int foundedCount)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      string text2;
      if (cast.Count != 0)
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(190, 2);
        defaultInterpolatedStringHandler.AppendLiteral("Say that Farza made ");
        string text;
        if (cast.Count != 1)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(8, 1);
          defaultInterpolatedStringHandler2.AppendFormatted<int>(cast.Count);
          defaultInterpolatedStringHandler2.AppendLiteral(" Clickys");
          text = defaultInterpolatedStringHandler2.ToStringAndClear();
        }
        else
        {
          text = "one Clicky";
        }
        defaultInterpolatedStringHandler.AppendFormatted(text);
        defaultInterpolatedStringHandler.AppendLiteral(" everyone starts with: ");
        defaultInterpolatedStringHandler.AppendFormatted(string.Join(" and ", cast.Select<RosterClicky, string>((RosterClicky c) => c.Name + " (" + c.OneLiner + ")")));
        defaultInterpolatedStringHandler.AppendLiteral(". Name each exactly, with a few words on what it does. Then say each Clicky is an AI agent with its own memory that does work for them on their PC.");
        text2 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text2 = "Say that Clickys are AI agents that do work for them on their PC, each with its own memory.";
      }
      string text3 = text2;
      string text4;
      if (foundedCount <= 0)
      {
        text4 = "";
      }
      else
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(95, 1);
        defaultInterpolatedStringHandler.AppendLiteral("End by saying you made ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(foundedCount);
        defaultInterpolatedStringHandler.AppendLiteral(" more just for them from what they told you. No names or goal recap yet.");
        text4 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      string text5 = text4;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(58, 2);
      defaultInterpolatedStringHandler.AppendLiteral("In at most 50 words total. ");
      defaultInterpolatedStringHandler.AppendFormatted(text3);
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(text5);
      defaultInterpolatedStringHandler.AppendLiteral(" At most four short sentences.");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06000F53 RID: 3923 RVA: 0x0057A228 File Offset: 0x0057A228
    private unsafe static string ClickyBrief(RosterClicky c, string revealLine)
    {
      string text = " ";
      <>y__InlineArray3<string> <>y__InlineArray = default(<>y__InlineArray3<string>);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 0) = "Land, in order: its name, " + c.Name + ", said first and exactly;";
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 1) = "and what this Clicky is for, tied to what they told you (the fact to convey: \"" + revealLine + "\").";
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 2) = "At most 25 words in one or two short sentences. No general explanation of Clickys.";
      return string.Join(text, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray3<string>, string>(in <>y__InlineArray, 3));
    }

    // Token: 0x06000F54 RID: 3924 RVA: 0x0057A294 File Offset: 0x0057A294
    private static string EndingBrief(IReadOnlyList<string> names)
    {
      string goalLine = ClickyRoster.Shared.GoalLine;
      string text = ((goalLine == null) ? "" : (" (what they told you they are going for, in their words: \"" + goalLine + "\"; use THEIR words, not a paraphrase)"));
      string text2 = ((names.Count == 0) ? "" : (" The Clickys you just introduced: " + string.Join(", ", names) + "."));
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(820, 2);
      defaultInterpolatedStringHandler.AppendLiteral("the goodbye. SHORT, two or three sentences, but never at the cost of warmth: if two sentences would sound cold, say three. This is you saying goodbye to THIS person at the end of THIS conversation, so it must carry something only they would recognize: their name if they gave it, the thing they are going for");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral(", a detail they mentioned or a Clicky by name.");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral(" Woven in, in whatever order reads naturally, never as instructions: all of this lives up here in their PC's notch, hover it any time; and wherever they are on their PC they can hold Control and Alt (say the shortcut exactly: \"hold Control and Alt\") and just talk to you. End on the good luck, aimed at their thing, said the way a friend who was rooting for them would say it. No recap, no list, no example asks, nothing that could be said to anyone else. Then stop.");
      string text3 = defaultInterpolatedStringHandler.ToStringAndClear();
      return "[Home tour beat] Last beat. On screen, as you speak: the notch folds back up; the user's own desktop is what's left. Land: " + text3 + " Say it now, in your own words, per the Home tour rules, then stop for good.";
    }

    // Token: 0x06000F55 RID: 3925 RVA: 0x0057A34C File Offset: 0x0057A34C
    private static async Task Delay(double seconds, CancellationToken ct)
    {
      if (seconds > 0.0)
      {
        await Task.Delay(TimeSpan.FromSeconds(seconds), ct);
      }
    }

    // Token: 0x06000F56 RID: 3926 RVA: 0x0057A398 File Offset: 0x0057A398
    private static async Task<T> WithTimeout<[Nullable(2)] T>(Task<T> task, double seconds, T fallback)
    {
      TaskAwaiter<Task> taskAwaiter = Task.WhenAny(task, Task.Delay(TimeSpan.FromSeconds(Math.Max(0.0, seconds)))).GetAwaiter();
      if (!taskAwaiter.IsCompleted)
      {
        await taskAwaiter;
        TaskAwaiter<Task> taskAwaiter2;
        taskAwaiter = taskAwaiter2;
        taskAwaiter2 = default(TaskAwaiter<Task>);
      }
      return (taskAwaiter.GetResult() == task && task.IsCompletedSuccessfully) ? task.Result : fallback;
    }

    // Token: 0x04000664 RID: 1636
    public const string OpenerWords = "Okay, check this out. So, I live in your PC's notch. It looks like this.";

    // Token: 0x04000665 RID: 1637
    private const string NarrationRules = "[Home tour] You are showing this person their new Clickys, one beat at a time, while their Home is open in the PC's notch. Each \"[Home tour beat]\" gives only the facts for that beat. Use your own warm, plain words and their interview context, but add no extra explanation, recap, promises, or transition sentence. Follow each beat's word limit; otherwise use at most 25 words in one or two short sentences, then stop. Say each Clicky's name exactly. A Clicky is an AI agent that does work for them; explain this only in the introduction. After that, call them Clickys. The personalized Clickys are the focus: keep the defaults brief. Do not read instructions aloud or mention beats or tour mechanics. No lists, dashes, or questions. Finish with the short goodbye you are given.";

    // Token: 0x04000666 RID: 1638
    private const int MaximumClickyCount = 3;

    // Token: 0x04000667 RID: 1639
    private const double LineWritingDeadlineSeconds = 25.0;

    // Token: 0x04000668 RID: 1640
    private const int LineIndexBase = 4000;

    // Token: 0x04000669 RID: 1641
    private readonly AppHost host;

    // Token: 0x0400066A RID: 1642
    [Nullable(2)]
    private readonly InterviewController interview;

    // Token: 0x0400066B RID: 1643
    private readonly DispatcherQueue dq;

    // Token: 0x0400066C RID: 1644
    private readonly CancellationTokenSource abort = new CancellationTokenSource();

    // Token: 0x0400066D RID: 1645
    private readonly Dictionary<int, TaskCompletionSource<double>> audioQueued = new Dictionary<int, TaskCompletionSource<double>>();

    // Token: 0x0400066E RID: 1646
    private readonly object gate = new object();

    // Token: 0x0400066F RID: 1647
    private int nextLine;

    // Token: 0x04000670 RID: 1648
    private bool holdingNotch;

    // Token: 0x04000674 RID: 1652
    private long noVoiceUntil;

    // Token: 0x02000622 RID: 1570
    [Nullable(0)]
    private sealed class Beat : IEquatable<HomeTour.Beat>
    {
      // Token: 0x060030FC RID: 12540 RVA: 0x0066A710 File Offset: 0x0066A710
      public Beat(string Key, string Brief, string Fallback, [Nullable(2)] Action OnAudioBegins)
      {
        this.Key = Key;
        this.Brief = Brief;
        this.Fallback = Fallback;
        this.OnAudioBegins = OnAudioBegins;
        base..ctor();
      }

      // Token: 0x1700077B RID: 1915
      // (get) Token: 0x060030FD RID: 12541 RVA: 0x0066A738 File Offset: 0x0066A738
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(HomeTour.Beat);
        }
      }

      // Token: 0x1700077C RID: 1916
      // (get) Token: 0x060030FE RID: 12542 RVA: 0x0066A744 File Offset: 0x0066A744
      // (set) Token: 0x060030FF RID: 12543 RVA: 0x0066A74C File Offset: 0x0066A74C
      public string Key { get; set; }

      // Token: 0x1700077D RID: 1917
      // (get) Token: 0x06003100 RID: 12544 RVA: 0x0066A758 File Offset: 0x0066A758
      // (set) Token: 0x06003101 RID: 12545 RVA: 0x0066A760 File Offset: 0x0066A760
      public string Brief { get; set; }

      // Token: 0x1700077E RID: 1918
      // (get) Token: 0x06003102 RID: 12546 RVA: 0x0066A76C File Offset: 0x0066A76C
      // (set) Token: 0x06003103 RID: 12547 RVA: 0x0066A774 File Offset: 0x0066A774
      public string Fallback { get; set; }

      // Token: 0x1700077F RID: 1919
      // (get) Token: 0x06003104 RID: 12548 RVA: 0x0066A780 File Offset: 0x0066A780
      // (set) Token: 0x06003105 RID: 12549 RVA: 0x0066A788 File Offset: 0x0066A788
      [Nullable(2)]
      public Action OnAudioBegins
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x06003106 RID: 12550 RVA: 0x0066A794 File Offset: 0x0066A794
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Beat");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003107 RID: 12551 RVA: 0x0066A7E0 File Offset: 0x0066A7E0
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Key = ");
        builder.Append(this.Key);
        builder.Append(", Brief = ");
        builder.Append(this.Brief);
        builder.Append(", Fallback = ");
        builder.Append(this.Fallback);
        builder.Append(", OnAudioBegins = ");
        builder.Append(this.OnAudioBegins);
        return true;
      }

      // Token: 0x06003108 RID: 12552 RVA: 0x0066A858 File Offset: 0x0066A858
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(HomeTour.Beat left, HomeTour.Beat right)
      {
        return !(left == right);
      }

      // Token: 0x06003109 RID: 12553 RVA: 0x0066A864 File Offset: 0x0066A864
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(HomeTour.Beat left, HomeTour.Beat right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600310A RID: 12554 RVA: 0x0066A878 File Offset: 0x0066A878
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Key>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Brief>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Fallback>k__BackingField)) * -1521134295 + EqualityComparer<Action>.Default.GetHashCode(this.<OnAudioBegins>k__BackingField);
      }

      // Token: 0x0600310B RID: 12555 RVA: 0x0066A8F4 File Offset: 0x0066A8F4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as HomeTour.Beat);
      }

      // Token: 0x0600310C RID: 12556 RVA: 0x0066A904 File Offset: 0x0066A904
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(HomeTour.Beat other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Key>k__BackingField, other.<Key>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Brief>k__BackingField, other.<Brief>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Fallback>k__BackingField, other.<Fallback>k__BackingField) && EqualityComparer<Action>.Default.Equals(this.<OnAudioBegins>k__BackingField, other.<OnAudioBegins>k__BackingField));
      }

      // Token: 0x0600310E RID: 12558 RVA: 0x0066A998 File Offset: 0x0066A998
      [CompilerGenerated]
      private Beat(HomeTour.Beat original)
      {
        this.Key = original.<Key>k__BackingField;
        this.Brief = original.<Brief>k__BackingField;
        this.Fallback = original.<Fallback>k__BackingField;
        this.OnAudioBegins = original.<OnAudioBegins>k__BackingField;
      }

      // Token: 0x0600310F RID: 12559 RVA: 0x0066A9D0 File Offset: 0x0066A9D0
      [CompilerGenerated]
      public void Deconstruct(out string Key, out string Brief, out string Fallback, [Nullable(2)] out Action OnAudioBegins)
      {
        Key = this.Key;
        Brief = this.Brief;
        Fallback = this.Fallback;
        OnAudioBegins = this.OnAudioBegins;
      }
    }
  }
}
