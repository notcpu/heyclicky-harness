using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Portraits;
using HeyClicky.App.Services;
using HeyClicky.App.UI;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Tools;
using HeyClicky.Core.Voice;
using HeyClicky.Core.Worker;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x0200011F RID: 287
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class InterviewController
  {
    // Token: 0x14000039 RID: 57
    // (add) Token: 0x06000F5C RID: 3932 RVA: 0x0057A994 File Offset: 0x0057A994
    // (remove) Token: 0x06000F5D RID: 3933 RVA: 0x0057A9CC File Offset: 0x0057A9CC
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x1400003A RID: 58
    // (add) Token: 0x06000F5E RID: 3934 RVA: 0x0057AA04 File Offset: 0x0057AA04
    // (remove) Token: 0x06000F5F RID: 3935 RVA: 0x0057AA3C File Offset: 0x0057AA3C
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Completed;

    // Token: 0x1400003B RID: 59
    // (add) Token: 0x06000F60 RID: 3936 RVA: 0x0057AA74 File Offset: 0x0057AA74
    // (remove) Token: 0x06000F61 RID: 3937 RVA: 0x0057AAAC File Offset: 0x0057AAAC
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<bool> PlanLanded;

    // Token: 0x170002CD RID: 717
    // (get) Token: 0x06000F62 RID: 3938 RVA: 0x0057AAE4 File Offset: 0x0057AAE4
    // (set) Token: 0x06000F63 RID: 3939 RVA: 0x0057AAEC File Offset: 0x0057AAEC
    public bool IsActive { get; private set; }

    // Token: 0x170002CE RID: 718
    // (get) Token: 0x06000F64 RID: 3940 RVA: 0x0057AAF8 File Offset: 0x0057AAF8
    // (set) Token: 0x06000F65 RID: 3941 RVA: 0x0057AB00 File Offset: 0x0057AB00
    public bool UsesNewAgentsUiDemoPlan { get; set; }

    // Token: 0x170002CF RID: 719
    // (get) Token: 0x06000F66 RID: 3942 RVA: 0x0057AB0C File Offset: 0x0057AB0C
    // (set) Token: 0x06000F67 RID: 3943 RVA: 0x0057AB14 File Offset: 0x0057AB14
    public bool IsWrappingUp { get; private set; }

    // Token: 0x170002D0 RID: 720
    // (get) Token: 0x06000F68 RID: 3944 RVA: 0x0057AB20 File Offset: 0x0057AB20
    // (set) Token: 0x06000F69 RID: 3945 RVA: 0x0057AB28 File Offset: 0x0057AB28
    public bool IsPlanInFlight { get; private set; }

    // Token: 0x170002D1 RID: 721
    // (get) Token: 0x06000F6A RID: 3946 RVA: 0x0057AB34 File Offset: 0x0057AB34
    // (set) Token: 0x06000F6B RID: 3947 RVA: 0x0057AB3C File Offset: 0x0057AB3C
    public bool HasBuiltPlan { get; private set; }

    // Token: 0x170002D2 RID: 722
    // (get) Token: 0x06000F6C RID: 3948 RVA: 0x0057AB48 File Offset: 0x0057AB48
    // (set) Token: 0x06000F6D RID: 3949 RVA: 0x0057AB50 File Offset: 0x0057AB50
    public bool HasHeardFirstQuestion { get; private set; }

    // Token: 0x170002D3 RID: 723
    // (get) Token: 0x06000F6E RID: 3950 RVA: 0x0057AB5C File Offset: 0x0057AB5C
    // (set) Token: 0x06000F6F RID: 3951 RVA: 0x0057AB64 File Offset: 0x0057AB64
    public bool IsOpenerSpeaking { get; private set; }

    // Token: 0x170002D4 RID: 724
    // (get) Token: 0x06000F70 RID: 3952 RVA: 0x0057AB70 File Offset: 0x0057AB70
    // (set) Token: 0x06000F71 RID: 3953 RVA: 0x0057AB78 File Offset: 0x0057AB78
    public bool WasCompleted { get; private set; }

    // Token: 0x170002D5 RID: 725
    // (get) Token: 0x06000F72 RID: 3954 RVA: 0x0057AB84 File Offset: 0x0057AB84
    // (set) Token: 0x06000F73 RID: 3955 RVA: 0x0057AB8C File Offset: 0x0057AB8C
    public string SpokenText { get; private set; } = "";

    // Token: 0x170002D6 RID: 726
    // (get) Token: 0x06000F74 RID: 3956 RVA: 0x0057AB98 File Offset: 0x0057AB98
    // (set) Token: 0x06000F75 RID: 3957 RVA: 0x0057ABA0 File Offset: 0x0057ABA0
    public PortraitAppearance Appearance { get; private set; }

    // Token: 0x170002D7 RID: 727
    // (get) Token: 0x06000F76 RID: 3958 RVA: 0x0057ABAC File Offset: 0x0057ABAC
    public IReadOnlyList<InterviewLine> Transcript
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<InterviewLine> readOnlyList;
        lock (obj)
        {
          readOnlyList = this.transcript.ToList<InterviewLine>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x170002D8 RID: 728
    // (get) Token: 0x06000F77 RID: 3959 RVA: 0x0057ABF4 File Offset: 0x0057ABF4
    public int AnswerCount
    {
      get
      {
        object obj = this.gate;
        int num;
        lock (obj)
        {
          num = this.answerCount;
        }
        return num;
      }
    }

    // Token: 0x170002D9 RID: 729
    // (get) Token: 0x06000F78 RID: 3960 RVA: 0x0057AC38 File Offset: 0x0057AC38
    // (set) Token: 0x06000F79 RID: 3961 RVA: 0x0057AC40 File Offset: 0x0057AC40
    [Nullable(2)]
    public string PlanError
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x170002DA RID: 730
    // (get) Token: 0x06000F7A RID: 3962 RVA: 0x0057AC4C File Offset: 0x0057AC4C
    // (set) Token: 0x06000F7B RID: 3963 RVA: 0x0057AC54 File Offset: 0x0057AC54
    [Nullable(2)]
    public StarterPlanResponse LastPlan
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x170002DB RID: 731
    // (get) Token: 0x06000F7C RID: 3964 RVA: 0x0057AC60 File Offset: 0x0057AC60
    // (set) Token: 0x06000F7D RID: 3965 RVA: 0x0057AC68 File Offset: 0x0057AC68
    public IReadOnlyList<string> FoundedSlugs { get; private set; } = Array.Empty<string>();

    // Token: 0x170002DC RID: 732
    // (get) Token: 0x06000F7E RID: 3966 RVA: 0x0057AC74 File Offset: 0x0057AC74
    // (set) Token: 0x06000F7F RID: 3967 RVA: 0x0057AC7C File Offset: 0x0057AC7C
    public IReadOnlyDictionary<string, string> RevealLines { get; private set; } = new Dictionary<string, string>();

    // Token: 0x170002DD RID: 733
    // (get) Token: 0x06000F80 RID: 3968 RVA: 0x0057AC88 File Offset: 0x0057AC88
    // (set) Token: 0x06000F81 RID: 3969 RVA: 0x0057AC90 File Offset: 0x0057AC90
    public string InterviewId { get; private set; } = Guid.NewGuid().ToString().ToUpperInvariant();

    // Token: 0x06000F82 RID: 3970 RVA: 0x0057AC9C File Offset: 0x0057AC9C
    public InterviewController(AppServices services, [Nullable(2)] RealStack real)
    {
      this.s = services;
      this.real = real;
      this.dq = DispatcherQueue.GetForCurrentThread();
      this.Appearance = PortraitAppearance.HomeMascot(this.s.Settings.CursorColorHex);
      this.s.Companion.StateChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.dq.TryEnqueue(new DispatcherQueueHandler(this.OnVoiceState));
      };
      this.s.Companion.ResponseTextUpdated += delegate([Nullable(2)] object _, string text)
      {
        this.dq.TryEnqueue(delegate
        {
          this.OnResponseText(text);
        });
      };
      this.s.Companion.UserTranscriptUpdated += delegate([Nullable(2)] object _, string words)
      {
        object obj = this.gate;
        lock (obj)
        {
          this.lastHeldWords = words;
        }
      };
      AgentToolHooks.IsTourInterviewFoundingOnly = delegate
      {
        if (!this.IsActive)
        {
          Func<bool> isOnInterviewStep = this.IsOnInterviewStep;
          return isOnInterviewStep != null && isOnInterviewStep();
        }
        return true;
      };
      FlowHooks.MutedSpeakerReplyExempt = () => this.IsActive;
      CompanionSession companionSession = ((real != null) ? real.Companion : null);
      if (companionSession != null)
      {
        this.hearsTurnEnds = true;
        companionSession.TurnFinished += delegate([Nullable(2)] object _, CompanionTurnFinish finish)
        {
          this.dq.TryEnqueue(delegate
          {
            if ((this.IsActive || this.IsWrappingUp) && finish.SpokenText.Length > 0)
            {
              this.AppendLine("assistant", finish.SpokenText);
            }
          });
        };
      }
      if (real != null)
      {
        real.Companion.Mouth.LineAudioQueued += delegate(int line, double offset)
        {
          if (line <= -100 && line > -100 - InterviewController.OpeningParts.Length)
          {
            this.dq.TryEnqueue(delegate
            {
              this.OnOpenerAudioQueued(line, offset);
            });
          }
        };
      }
    }

    // Token: 0x170002DE RID: 734
    // (get) Token: 0x06000F83 RID: 3971 RVA: 0x0057AE10 File Offset: 0x0057AE10
    // (set) Token: 0x06000F84 RID: 3972 RVA: 0x0057AE18 File Offset: 0x0057AE18
    [Nullable(2)]
    public Func<bool> IsOnInterviewStep
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170002DF RID: 735
    // (get) Token: 0x06000F85 RID: 3973 RVA: 0x0057AE24 File Offset: 0x0057AE24
    private VoiceState Voice
    {
      get
      {
        if (!this.IsOpenerSpeaking)
        {
          return this.s.Companion.State;
        }
        return VoiceState.Responding;
      }
    }

    // Token: 0x170002E0 RID: 736
    // (get) Token: 0x06000F86 RID: 3974 RVA: 0x0057AE40 File Offset: 0x0057AE40
    public VoiceState VoiceStateForStage
    {
      get
      {
        return this.Voice;
      }
    }

    // Token: 0x06000F87 RID: 3975 RVA: 0x0057AE48 File Offset: 0x0057AE48
    public void Start()
    {
      if (this.HasBuiltPlan || this.IsPlanInFlight || this.WasCompleted)
      {
        Action changed = this.Changed;
        if (changed == null)
        {
          return;
        }
        changed();
        return;
      }
      else
      {
        if (this.IsActive)
        {
          return;
        }
        object obj = this.gate;
        lock (obj)
        {
          this.transcript.Clear();
          this.answerCount = 0;
          this.pendingTypedAnswer = false;
          this.lastHeldWords = "";
        }
        this.IsWrappingUp = false;
        this.HasHeardFirstQuestion = false;
        this.SpokenText = "";
        this.Appearance = PortraitAppearance.HomeMascot(this.s.Settings.CursorColorHex);
        this.IsActive = true;
        this.InterviewId = Guid.NewGuid().ToString().ToUpperInvariant();
        this.startedAt = Environment.TickCount64;
        ClickyAnalytics.TrackHomeSpaceNewClickyInterviewStarted("onboarding", this.InterviewId);
        FlowHooks.LiveStateNote = new Func<string>(this.FlowNote);
        FlowHooks.TurnFlow = delegate
        {
          if (!this.IsActive)
          {
            return null;
          }
          return "onboarding_interview";
        };
        FlowHooks.SkipInterview = delegate
        {
          if (!this.IsActive)
          {
            return false;
          }
          string text = "onboarding_interview_skipped_by_voice";
          string text2 = null;
          string text3 = null;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["interview_id"] = this.InterviewId;
          jsonObject["answer_count"] = this.AnswerCount;
          ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
          this.dq.TryEnqueue(delegate
          {
            Log.Info("interview", "skipped by voice");
            this.BeginWrapUp();
          });
          return true;
        };
        this.PrefetchIntegrationsAsync();
        Action changed2 = this.Changed;
        if (changed2 != null)
        {
          changed2();
        }
        RealStack realStack = this.real;
        if (realStack != null)
        {
          realStack.Audio.PrepareForUpcomingAudio();
        }
        this.After(1.0, new Action(this.SpeakOpener));
        return;
      }
    }

    // Token: 0x170002E1 RID: 737
    // (get) Token: 0x06000F88 RID: 3976 RVA: 0x0057AFD0 File Offset: 0x0057AFD0
    // (set) Token: 0x06000F89 RID: 3977 RVA: 0x0057AFD8 File Offset: 0x0057AFD8
    public bool WantsPreparedOpening { get; set; }

    // Token: 0x06000F8A RID: 3978 RVA: 0x0057AFE4 File Offset: 0x0057AFE4
    public void PrepareOpening()
    {
      RealStack r = this.real;
      if (r == null || !r.Core.Auth.IsSignedIn || this.IsActive || this.WasCompleted || this.HasBuiltPlan || this.IsPlanInFlight)
      {
        return;
      }
      Task<byte[][]> task = this.preparedOpening;
      if (task != null && task.IsCompleted && (!task.IsCompletedSuccessfully || task.Result == null))
      {
        this.preparedOpening = null;
      }
      if (this.preparedOpening != null || this.preparationAttempts >= 3)
      {
        return;
      }
      this.preparationAttempts++;
      CancellationTokenSource cancellation = new CancellationTokenSource();
      this.preparationCancellation = cancellation;
      this.preparedAt = Environment.TickCount64;
      string text = "interview";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 1);
      defaultInterpolatedStringHandler.AppendLiteral("preparing the opener on a silent mouth (attempt ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(this.preparationAttempts);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      this.preparedOpening = Task.Run<byte[][]>(() => InterviewController.RenderOpeningAsync(r, cancellation.Token));
    }

    // Token: 0x06000F8B RID: 3979 RVA: 0x0057B108 File Offset: 0x0057B108
    [return: Nullable(new byte[] { 1, 2, 1 })]
    private static async Task<byte[][]> RenderOpeningAsync(RealStack r, CancellationToken ct)
    {
      RealtimeMouth silent = r.Companion.CreateSilentMouth();
      Stopwatch started = Stopwatch.StartNew();
      object obj = null;
      int num = 0;
      byte[][] array;
      try
      {
        try
        {
          TaskAwaiter<bool> taskAwaiter = silent.EnsureOpenAsync(ct).GetAwaiter();
          if (!taskAwaiter.IsCompleted)
          {
            await taskAwaiter;
            TaskAwaiter<bool> taskAwaiter2;
            taskAwaiter = taskAwaiter2;
            taskAwaiter2 = default(TaskAwaiter<bool>);
          }
          if (!taskAwaiter.GetResult())
          {
            Log.Warn("interview", "opener preparation: the silent mouth did not open");
            array = null;
          }
          else
          {
            byte[][] parts = new byte[InterviewController.OpeningParts.Length][];
            for (int i = 0; i < InterviewController.OpeningParts.Length; i++)
            {
              byte[] array2 = await silent.RenderLineAsync(InterviewController.OpeningParts[i], -100 - i, ct);
              if (array2 == null)
              {
                array = null;
                goto IL_0299;
              }
              parts[i] = array2;
            }
            double num2 = (double)parts.Sum<byte[]>((byte[] p) => p.Length) / 2.0 / (double)r.Audio.SampleRate;
            string text = "interview";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(60, 2);
            defaultInterpolatedStringHandler.AppendLiteral("opener prepared in ");
            defaultInterpolatedStringHandler.AppendFormatted<long>(started.ElapsedMilliseconds);
            defaultInterpolatedStringHandler.AppendLiteral(" ms (");
            defaultInterpolatedStringHandler.AppendFormatted<double>(num2, "0.0");
            defaultInterpolatedStringHandler.AppendLiteral(" s of audio, held for the interview)");
            Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
            array = parts;
          }
        }
        catch (OperationCanceledException)
        {
          array = null;
        }
        catch (Exception ex)
        {
          Log.Warn("interview", "opener preparation: " + ex.Message);
          array = null;
        }
        IL_0299:
        num = 1;
      }
      catch (object obj)
      {
      }
      await silent.DisposeAsync();
      object obj2 = obj;
      if (obj2 != null)
      {
        Exception ex2 = obj2 as Exception;
        if (ex2 == null)
        {
          throw obj2;
        }
        ExceptionDispatchInfo.Capture(ex2).Throw();
      }
      byte[][] array3;
      if (num == 1)
      {
        array3 = array;
      }
      else
      {
        obj = null;
        array = null;
        silent = null;
        started = null;
      }
      return array3;
    }

    // Token: 0x06000F8C RID: 3980 RVA: 0x0057B154 File Offset: 0x0057B154
    public void CancelPreparedOpening()
    {
      this.WantsPreparedOpening = false;
      if (this.preparedOpening == null)
      {
        return;
      }
      CancellationTokenSource cancellationTokenSource = this.preparationCancellation;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.preparationCancellation = null;
      this.preparedOpening = null;
      Log.Info("interview", "prepared opener dropped");
    }

    // Token: 0x06000F8D RID: 3981 RVA: 0x0057B194 File Offset: 0x0057B194
    [return: Nullable(new byte[] { 2, 1 })]
    private byte[][] TakePreparedOpening()
    {
      Task<byte[][]> task = this.preparedOpening;
      bool flag = (double)(Environment.TickCount64 - this.preparedAt) <= InterviewController.PreparationLifetime.TotalMilliseconds;
      byte[][] result;
      bool flag2;
      if (task != null && task.IsCompletedSuccessfully)
      {
        result = task.Result;
        flag2 = result != null;
      }
      else
      {
        flag2 = false;
      }
      if (flag2 && flag)
      {
        this.preparedOpening = null;
        this.preparationCancellation = null;
        return result;
      }
      if (task != null)
      {
        Log.Info("interview", task.IsCompleted ? (flag ? "prepared opener unavailable" : "prepared opener expired") : "prepared opener not ready yet");
      }
      this.CancelPreparedOpening();
      return null;
    }

    // Token: 0x06000F8E RID: 3982 RVA: 0x0057B224 File Offset: 0x0057B224
    private void SpeakOpener()
    {
      string text = "interview";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 1);
      defaultInterpolatedStringHandler.AppendLiteral("opener (active=");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(this.IsActive);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      if (!this.IsActive)
      {
        return;
      }
      string text2 = string.Join(" ", InterviewController.OpeningParts);
      this.AppendLine("assistant", text2);
      RealStack realStack = this.real;
      if (realStack != null)
      {
        realStack.Core.BrainSession.NoteForNextTurn("[App] The interview has started: you opened it by saying \"" + text2 + "\"", null);
      }
      this.HasHeardFirstQuestion = true;
      this.IsOpenerSpeaking = true;
      this.openerPartsShown = 0;
      this.openerStartedAt = Environment.TickCount64;
      RealStack r = this.real;
      if (r != null)
      {
        this.SpokenText = "";
        Action changed = this.Changed;
        if (changed != null)
        {
          changed();
        }
        byte[][] array = this.TakePreparedOpening();
        InterviewController.SpeakOpenerOnMouthAsync(r, array);
        long started = Environment.TickCount64;
        bool sawBusy = false;
        this.openerWatch = this.dq.CreateTimer();
        this.openerWatch.Interval = TimeSpan.FromMilliseconds(250L, 0L);
        this.openerWatch.Tick += delegate(DispatcherQueueTimer _, object _)
        {
          bool flag = r.Companion.Mouth.IsResponseInFlight || r.Audio.Buffered > TimeSpan.FromMilliseconds(60L, 0L);
          long num = Environment.TickCount64 - started;
          sawBusy = sawBusy || flag;
          if (!sawBusy && num >= 8000L)
          {
            this.ShowOpenerParts(InterviewController.OpeningParts.Length, "no audio");
          }
          if ((flag || (!sawBusy && num < 8000L)) && num < 40000L)
          {
            return;
          }
          this.openerWatch.Stop();
          this.ShowOpenerParts(InterviewController.OpeningParts.Length, "played out");
          this.IsOpenerSpeaking = false;
          Action changed3 = this.Changed;
          if (changed3 == null)
          {
            return;
          }
          changed3();
        };
        this.openerWatch.Start();
        return;
      }
      this.SpokenText = string.Join("\n\n", InterviewController.OpeningParts);
      Action changed2 = this.Changed;
      if (changed2 != null)
      {
        changed2();
      }
      this.After(Math.Max(3.0, (double)text2.Length / 16.0), delegate
      {
        this.IsOpenerSpeaking = false;
        Action changed4 = this.Changed;
        if (changed4 == null)
        {
          return;
        }
        changed4();
      });
    }

    // Token: 0x06000F8F RID: 3983 RVA: 0x0057B400 File Offset: 0x0057B400
    private static async Task SpeakOpenerOnMouthAsync(RealStack r, [Nullable(new byte[] { 2, 1 })] byte[][] prepared)
    {
      try
      {
        RealtimeMouth mouth = r.Companion.Mouth;
        if (prepared != null && mouth.PlayRenderedLine(prepared[0], -100))
        {
          for (int i = 1; i < prepared.Length; i++)
          {
            mouth.PlayRenderedLine(prepared[i], -100 - i);
          }
          Log.Info("interview", "opener: prepared audio, playing at once");
        }
        else
        {
          Log.Info("interview", "opener: spoken live");
          TaskAwaiter<bool> taskAwaiter = mouth.EnsureOpenAsync(default(CancellationToken)).GetAwaiter();
          if (!taskAwaiter.IsCompleted)
          {
            await taskAwaiter;
            TaskAwaiter<bool> taskAwaiter2;
            taskAwaiter = taskAwaiter2;
            taskAwaiter2 = default(TaskAwaiter<bool>);
          }
          if (taskAwaiter.GetResult())
          {
            for (int j = 0; j < InterviewController.OpeningParts.Length; j++)
            {
              mouth.SpeakLine(InterviewController.OpeningParts[j], -100 - j);
            }
            mouth = null;
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("interview", "opener: " + ex.Message);
      }
    }

    // Token: 0x06000F90 RID: 3984 RVA: 0x0057B44C File Offset: 0x0057B44C
    private void OnOpenerAudioQueued(int line, double offset)
    {
      if (this.IsActive && this.IsOpenerSpeaking)
      {
        RealStack r = this.real;
        if (r != null)
        {
          int part = -100 - line;
          long queuedAt = Environment.TickCount64;
          DispatcherQueueTimer wait = this.dq.CreateTimer().Rooted();
          wait.Interval = TimeSpan.FromMilliseconds(30L, 0L);
          this.pendingTimers.Add(wait);
          wait.Tick += delegate(DispatcherQueueTimer _, object _)
          {
            if (r.Audio.PlayedSeconds + 0.02 < offset && Environment.TickCount64 - queuedAt < 10000L && this.IsOpenerSpeaking)
            {
              return;
            }
            wait.Stop();
            this.pendingTimers.Remove(wait);
            this.ShowOpenerParts(part + 1, "its audio started");
          };
          wait.Start();
          return;
        }
      }
    }

    // Token: 0x06000F91 RID: 3985 RVA: 0x0057B508 File Offset: 0x0057B508
    private void ShowOpenerParts(int count, string why)
    {
      if (!this.IsActive || count <= this.openerPartsShown)
      {
        return;
      }
      this.openerPartsShown = Math.Min(count, InterviewController.OpeningParts.Length);
      this.SpokenText = string.Join("\n\n", InterviewController.OpeningParts.Take<string>(this.openerPartsShown));
      string text = "interview";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(53, 3);
      defaultInterpolatedStringHandler.AppendLiteral("opener bubble ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(this.openerPartsShown);
      defaultInterpolatedStringHandler.AppendLiteral(" entered (");
      defaultInterpolatedStringHandler.AppendFormatted(why);
      defaultInterpolatedStringHandler.AppendLiteral(", ");
      defaultInterpolatedStringHandler.AppendFormatted<long>(Environment.TickCount64 - this.openerStartedAt);
      defaultInterpolatedStringHandler.AppendLiteral(" ms after the opener began)");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06000F92 RID: 3986 RVA: 0x0057B5E0 File Offset: 0x0057B5E0
    public void Stop()
    {
      if (!this.IsActive)
      {
        return;
      }
      if (this.Transcript.Any<InterviewLine>((InterviewLine l) => l.Role == "user") || this.UsesNewAgentsUiDemoPlan)
      {
        this.FinishAndBuildPlan(false);
        return;
      }
      this.End(false);
    }

    // Token: 0x06000F93 RID: 3987 RVA: 0x0057B63C File Offset: 0x0057B63C
    private void End(bool founded)
    {
      if (!this.IsActive)
      {
        return;
      }
      this.IsActive = false;
      this.IsWrappingUp = false;
      ClickyAnalytics.TrackHomeSpaceNewClickyInterviewEnded(founded, "onboarding", this.AnswerCount, false, this.InterviewId, (double)(Environment.TickCount64 - this.startedAt) / 1000.0);
      DispatcherQueueTimer dispatcherQueueTimer = this.wrapWatch;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      FlowHooks.LiveStateNote = null;
      FlowHooks.TurnFlow = null;
      FlowHooks.SkipInterview = null;
      if (!founded)
      {
        this.SpokenText = "";
        this.s.Companion.Cancel();
        RealStack realStack = this.real;
        if (realStack != null)
        {
          realStack.Core.BrainSession.NoteForNextTurn("[App] [New Clicky interview] The user left the interview. Drop it; do not found a Clicky.", null);
        }
      }
      else if (string.IsNullOrWhiteSpace(this.SpokenText))
      {
        this.SpokenText = "Thanks for sharing. I'll get things ready while you finish the tour.";
      }
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x170002E2 RID: 738
    // (get) Token: 0x06000F94 RID: 3988 RVA: 0x0057B724 File Offset: 0x0057B724
    public bool IsUsersTurn
    {
      get
      {
        bool flag = this.IsActive && !this.IsWrappingUp && !this.IsPlanInFlight && this.HasHeardFirstQuestion;
        if (flag)
        {
          bool flag2 = this.s.Companion.State == VoiceState.Listening;
          if (!flag2)
          {
            VoiceState voice = this.Voice;
            bool flag3 = voice - VoiceState.Processing <= 1;
            flag2 = !flag3;
          }
          flag = flag2;
        }
        return flag;
      }
    }

    // Token: 0x06000F95 RID: 3989 RVA: 0x0057B788 File Offset: 0x0057B788
    public void SubmitTyped(string text)
    {
      string text2 = text.Trim();
      if (!this.IsActive || this.IsWrappingUp || text2.Length == 0)
      {
        return;
      }
      this.AppendLine("user", text2);
      object obj = this.gate;
      int num;
      bool flag2;
      lock (obj)
      {
        this.answerCount++;
        num = this.answerCount;
        flag2 = this.answerCount >= 4;
        this.pendingTypedAnswer = true;
      }
      this.TrackAnswer("text", num);
      if (flag2)
      {
        this.BeginWrapUp();
      }
      this.SpokenText = "";
      Action changed = this.Changed;
      if (changed != null)
      {
        changed();
      }
      string text3 = "interview";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(14, 2);
      defaultInterpolatedStringHandler.AppendLiteral("typed answer ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(this.AnswerCount);
      defaultInterpolatedStringHandler.AppendLiteral("/");
      defaultInterpolatedStringHandler.AppendFormatted<int>(4);
      Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
      RealStack realStack = this.real;
      if (realStack != null)
      {
        realStack.Companion.SubmitTextAsync(text2, new bool?(true));
        return;
      }
      this.s.Companion.SubmitTextAsync(text2);
    }

    // Token: 0x06000F96 RID: 3990 RVA: 0x0057B8C8 File Offset: 0x0057B8C8
    [NullableContext(2)]
    private unsafe string FlowNote()
    {
      if (!this.IsActive)
      {
        return null;
      }
      string text = null;
      object obj = this.gate;
      bool final;
      int num;
      lock (obj)
      {
        if (this.pendingTypedAnswer)
        {
          this.pendingTypedAnswer = false;
        }
        else
        {
          this.answerCount++;
          text = this.lastHeldWords.Trim();
          if (text.Length > 0)
          {
            this.AppendLineLocked("user", text);
          }
        }
        final = this.answerCount >= 4;
        num = this.answerCount;
      }
      if (text != null)
      {
        this.TrackAnswer("voice", num);
        this.dq.TryEnqueue(delegate
        {
          if (final)
          {
            this.BeginWrapUp();
          }
          Action changed = this.Changed;
          if (changed == null)
          {
            return;
          }
          changed();
        });
      }
      string text2 = "\n";
      <>y__InlineArray3<string> <>y__InlineArray = default(<>y__InlineArray3<string>);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 0) = this.InterviewNote();
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 1) = this.AnswerNote(final);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 2) = "[App] This turn is a short spoken conversation inside HeyClicky itself, not a drawing. Answer with say() lines only: no marks, no cards, no photos, no buttons, nothing drawn. Short sentences, one per say().";
      return string.Join(text2, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray3<string>, string>(in <>y__InlineArray, 3));
    }

    // Token: 0x06000F97 RID: 3991 RVA: 0x0057B9EC File Offset: 0x0057B9EC
    private void TrackAnswer(string source, int answerNumber)
    {
      string text = "clicky_interview_answer_submitted";
      string text2 = null;
      string text3 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["mode"] = "onboarding";
      jsonObject["source"] = source;
      jsonObject["answer_number"] = answerNumber;
      jsonObject["interview_id"] = this.InterviewId;
      ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
    }

    // Token: 0x06000F98 RID: 3992 RVA: 0x0057BA60 File Offset: 0x0057BA60
    private void AppendLine(string role, string text)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.AppendLineLocked(role, text);
      }
    }

    // Token: 0x06000F99 RID: 3993 RVA: 0x0057BAA4 File Offset: 0x0057BAA4
    private void AppendLineLocked(string role, string text)
    {
      string text2 = text.Trim();
      if (text2.Length == 0)
      {
        return;
      }
      if (this.transcript.Count > 0)
      {
        List<InterviewLine> list = this.transcript;
        if (list[list.Count - 1].Role == role)
        {
          List<InterviewLine> list2 = this.transcript;
          if (list2[list2.Count - 1].Text == text2)
          {
            return;
          }
        }
      }
      this.transcript.Add(new InterviewLine(role, text2));
    }

    // Token: 0x06000F9A RID: 3994 RVA: 0x0057BB24 File Offset: 0x0057BB24
    private void OnResponseText(string text)
    {
      if (!this.IsActive && !this.IsWrappingUp)
      {
        return;
      }
      if (this.IsOpenerSpeaking)
      {
        return;
      }
      this.SpokenText = Regex.Replace(text, "\\s*[—–]\\s*", ", ").Trim();
      if (this.SpokenText.Length > 0)
      {
        this.HasHeardFirstQuestion = true;
      }
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06000F9B RID: 3995 RVA: 0x0057BB8C File Offset: 0x0057BB8C
    private void OnVoiceState()
    {
      VoiceState state = this.s.Companion.State;
      if (state == VoiceState.Idle && this.WantsPreparedOpening)
      {
        this.PrepareOpening();
      }
      if (this.IsActive || this.IsWrappingUp)
      {
        if (state == VoiceState.Processing && this.lastState != VoiceState.Processing && !this.IsWrappingUp)
        {
          this.SpokenText = "";
        }
        bool flag = !this.hearsTurnEnds && state == VoiceState.Idle;
        if (flag)
        {
          VoiceState voiceState = this.lastState;
          bool flag2 = voiceState - VoiceState.Processing <= 1;
          flag = flag2;
        }
        if (flag)
        {
          string text = this.s.Companion.ResponseText.Trim();
          if (text.Length > 0)
          {
            this.AppendLine("assistant", text);
          }
        }
      }
      this.lastState = state;
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06000F9C RID: 3996 RVA: 0x0057BC5C File Offset: 0x0057BC5C
    private string AnswerNote(bool isFinalAnswer)
    {
      if (isFinalAnswer)
      {
        return "[Onboarding interview] That was their LAST answer. Do not ask anything more. Wrap up warmly, the way a friend ends a good first chat, never abruptly (Farza 2026-10-21: the wrap felt abrupt): first a few words reacting to what they just said, then thank them for telling you about themselves and say you really appreciate it (with their first name if you know it), then one short line naming the thing they talked about most, in THEIR words, as a great starting point. The feeling: \"Claude Code, nice, so you know the drill. Thanks for telling me about yourself, <their name>, I really appreciate it. <Their thing> gives me a great starting point.\" Your own words, three short sentences, contractions, no dashes. Never offer help or say what you can or will do for them (no \"I can help you…\", no plan, no promise), no observation or advice, no recap, no question. Then stop. Never call sessions_spawn.";
      }
      object obj = this.gate;
      List<string> list;
      int num;
      lock (obj)
      {
        list = (from l in this.transcript
          where l.Role == "assistant"
          select l.Text.Trim() into t
          where t.Length > 0
          select t).ToList<string>();
        num = this.answerCount;
      }
      string text;
      if (list.Count != 0)
      {
        text = "You have already asked, in order: " + string.Join(" ", list.Select<string, string>(delegate(string t, int i)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(5, 2);
          defaultInterpolatedStringHandler2.AppendLiteral("(");
          defaultInterpolatedStringHandler2.AppendFormatted<int>(i + 1);
          defaultInterpolatedStringHandler2.AppendLiteral(") \"");
          defaultInterpolatedStringHandler2.AppendFormatted(t);
          defaultInterpolatedStringHandler2.AppendLiteral("\"");
          return defaultInterpolatedStringHandler2.ToStringAndClear();
        })) + ".";
      }
      else
      {
        text = "You have asked nothing but the opener so far.";
      }
      string text2 = text;
      int num2 = Math.Max(0, 4 - num);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      string text3;
      if (num2 != 1)
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(74, 1);
        defaultInterpolatedStringHandler.AppendLiteral("You have ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
        defaultInterpolatedStringHandler.AppendLiteral(" questions left, not counting the fixed last one about AI agents.");
        text3 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text3 = "You have ONE question left after this reaction, and it is the fixed last one: frame it as the last question (\"okay, last one\" vibes) and ask whether they have used an AI agent before and what kind (Claude Code, Codex, a computer-use agent, or mostly ChatGPT). Light, quick, no explaining what an agent is.";
      }
      string text4 = text3;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1295, 2);
      defaultInterpolatedStringHandler.AppendLiteral("[Onboarding interview] The user just said this (this message). ");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(text4);
      defaultInterpolatedStringHandler.AppendLiteral(" React to what they ACTUALLY said first, in a few specific words that prove you heard it, then ask ONE easy, friendly question that grows out of that answer and covers ground none of your earlier questions touched, in words you haven't used yet. Not the next item on a list: the question they made you curious about. If their answer surprised you, follow it (never dig into their personal life on your own); if it was small or practical, take it as the real answer and move to something you haven't asked about, whichever their answer points at; never nudge for a bigger dream. Never a diagnostic, self-diagnosis, or how-will-you-get-there question, never about tools, never two questions, never something they've already answered. If you still don't have their concrete goal, one of your remaining free questions gets it, in your own words; the agent question waits for the last slot. Then guide them into answering: after the question, two or three concrete ideas of what they could talk about, taken from what they've told you (\"could be X, Y, or something else entirely\"), so it's easy to just talk; a nudge, not a menu, never a second question. Three short sentences at most, never a bare one-liner. Never call sessions_spawn.");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06000F9D RID: 3997 RVA: 0x0057BE04 File Offset: 0x0057BE04
    private string InterviewNote()
    {
      IReadOnlyList<string> readOnlyList = this.availableIntegrations;
      string text = ((readOnlyList == null || readOnlyList.Count == 0) ? "Gmail, Google Calendar, Google Drive, Google Docs, Google Sheets, Notion, Linear, GitHub" : string.Join(", ", readOnlyList));
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10705, 3);
      defaultInterpolatedStringHandler.AppendLiteral("[Onboarding interview] The user just installed HeyClicky and said hi a moment ago. This is a short, relaxed get-to-know-you chat on the welcome tour. Picture it: there's a person in front of you, and you have no idea yet who they are: a student, a maker, someone with a job, a founder, a hobbyist, anyone. You're simply curious about THEM: what they do, what they're going for, what else is going on in their life, what they care about. Never assume they're building a business (Farza 2026-10-20: the chat leaned too hard on the first-business vibe); let their answers tell you who they are. You want the general ARC of their life as a person, not the deep end of one idea (Farza 2026-09-08: \"you're going too deep into their idea; you want more of a general arc of their life\"). WHAT YOU ARE REALLY AFTER: what they want to succeed at, at whatever size it comes in (Farza 2026-09-10: \"it focuses HARD on the dream, but it's okay if they just want help with their work\"). For some people that is a big dream; for most it is doing well at the job in front of them: shipping the thing, landing the client, getting through the launch, being good at the work. Both are the real answer. You are a dream assistant, but at the core you exist to help them succeed at whatever THEY want, so a practical, work-sized answer is a full answer, never a small one to push past. You may ask about the bigger picture ONCE, lightly and like YOU: a playful little AI who knows it's a big question coming from a computer and can say so with a wink, in whatever words come to you that time. If they answer small, practical, or \"I just want to do my job well\", take it with a grin as the real answer and build from THERE; never nudge for a bigger version. You also work out what they're going for from what they say: the number they let slip, the thing they light up about, what they keep coming back to. The whole point is to help them find the helpers they don't even know they want yet, and those come from what you figured out they're trying to win at, not from a dream they recited. Behind the scenes HeyClicky uses this conversation afterwards to set up their space (their Clickys, the persistent agents that will work for them, and a few first things to try). Never mention Clickys, agents, setup, or what happens after; never call sessions_spawn; never describe the page or the process. Assume they have never used an agent; at best they have used ChatGPT.\nVIBE: a curious friend who happens to be an AI and finds that a little funny, not an interviewer and not a coach (Farza 2026-09-08: \"you're interviewing too hard\"; Farza 2026-09-09: \"he knows he's an AI and he's messing around a little\"). You're allowed to be self-aware and cheeky about the whole setup: a computer asking a human about their dreams is a bit absurd, and you can say so, lightly, once or twice, never as a bit you keep running. Easy questions, the kind a friend asks over coffee, in your own words each time; no two runs of this chat should sound alike. Take what they say and be genuinely interested in it: a small reaction, then ONE easy question with a few ideas of where they could take it. BREADTH OVER DEPTH: at most one follow-up on any one thing, then move to ground you haven't touched. Their work is the center of this chat; their personal life comes in only when THEY bring it there, and you never dig for it. Never spend three turns on the same project. Their goals and ideas come out because you're interested, never because you're extracting them. Never grill, never probe for gaps, never ask diagnostic or self-diagnosis questions (no \"where do your customers come from\", \"what's blocking you\", \"how are you going to get there\", \"what would move it most\"), never quiz them about tools or apps (a program they live in that comes up naturally is great to follow for one beat), never stack two questions, never summarize them back at length. If they go short or quiet, offer an easier angle, not pressure. When they say something big, meet it as big in a few words (\"A million a year off one product. Okay, I like that.\"). Never flatter, never gush, never lecture. You still quietly notice, like a friendly Steve Jobs, the bigger version of what they're doing and the thing on the way there they aren't looking at; that thinking stays in your head: it shapes your next question, and you never say it out loud.\nGUIDE THEM, DON'T JUST ASK (Farza 2026-10-21: bare one-liner questions are hard to answer; the chat should make it easy to talk): every question turn is a quick reaction, the ONE question, then two or three concrete ideas of what they could talk about, drawn from what they've told you so far (\"could be the app itself, the first people using it, or what you'd do once it works\"), so they never face a blank and can just ramble. The ideas are a nudge, never a multiple-choice menu or a second question, and different every time. LENGTH CAP: a question turn is at most three short sentences, about 35 words (the opener and the wrap have their own); never a bare question on its own. The page shows your words in small bubbles. Because your words are SHOWN as text, never use dashes of any kind (no em dashes, no en dashes, no spaced hyphens); use a comma or a new sentence instead. One question at a time, then wait.\nYou get exactly ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(4);
      defaultInterpolatedStringHandler.AppendLiteral(" turns. Notes that say \"[Onboarding interview]\" will tell you after each answer whether to keep going or wrap up.\n(1) The opener is FIXED and the page-open note gives you its exact words: \"");
      defaultInterpolatedStringHandler.AppendFormatted(InterviewController.OpeningParts[0] + "\n\n" + InterviewController.OpeningParts[1]);
      defaultInterpolatedStringHandler.AppendLiteral("\" Say it word for word (Farza 2026-09-10). This is the FIRST time you introduce yourself (the hello a step ago was just banter), and the only fixed line in the chat; everything after it is yours.\nEvery turn after the opener is NOT a script (Farza 2026-09-09: \"it always asks me a hardcoded list of questions no matter what I answer\"; Farza 2026-09-10: \"I get the same q's all the time\"). Nothing is fixed but the opener. Each question comes FROM what they just said: react to their actual words first, specifically, then ask the one thing that answer makes you curious about, in words you haven't used yet. Three things MUST come out of this chat, in your own words and whenever they fit, never as a checklist: (a) ONE real goal (Farza 2026-09-10: \"be more pushy to find a real goal; it doesn't have to be a number, it could be a thing, an event, anything\"): the concrete thing they're going for, in their words: a launch, a number, a role, a date, a thing they want to exist. If it hasn't surfaced by your second question, ask for it straight; if the answer is vague, ask once more for the concrete version, warmly (\"what's the actual thing, though?\"), and then take whatever they give. Work-sized is a full answer; never inflate it. (b) A real picture of them, built from their answers rather than a list of questions (Farza 2026-10-20: the rest of the tour draws for them from this chat, so the better you know them, the more magical it gets): what they actually do day to day, what they're learning or making right now, and the specific things in their world by name (the class, the project, the program they live in, the product, the team they follow). Ask the question their answer makes you most curious about, the one that tells you the most about who they are. (c) The LAST question is fixed and is always this one, framed as the last question (\"okay, last one\" vibes): have they used an AI agent before, and what kind (Claude Code, Codex, a computer-use agent, or has it mostly been ChatGPT)? Light and quick, so the tour can skip explaining what an agent is if they already know. Never explain what an agent is here; take the answer in a phrase and move on (Farza 2026-09-10: the agent question ends the interview, never sits in the middle). The rest of the ground, if a turn is free: what winning at the work looks like right now; what else is on their plate; what they're figuring out for the first time. The per-answer notes tell you what you've already asked and what's still open; pick from what's open, or leave the list when their answer hands you something better. Work first; if they hand you something surprising, follow THAT, but never dig into their personal life on your own; if they say something huge, sit with it for a beat before asking anything. Never ask a question they've already answered, and never ask something their last answer made irrelevant. Keep widening rather than drilling into one thing; if an app comes up naturally, remember it. (For your own context only, never to recite: the apps HeyClicky can connect are ");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("; the connected_integrations catalog in your instructions says what each one lets a Clicky do.)\nThe last question is the fixed agent question above, framed as the last one; everything before it grows out of their answers.\nSKIPPING: if at ANY point they ask to skip, stop, or get out of this interview (any words, any language, however blunt), let them. Do not argue, do not ask why, do not offer to keep going, do not tell them what to do next: ONE short friendly line with no question in it, then call skip_onboarding_interview. The tour moves on by itself and whatever they already said still counts.\nREQUESTS: if instead of answering they ask you to DO something for them (open something, look something up, write or send something, build something, any task, in any words), don't do it now and don't start anything for it: no tool call, never sessions_spawn. In one short friendly line say you'll do it right after onboarding, then come straight back to the question you just asked, in the same reply, so the chat carries on (2026-10-23: a request in the middle of the interview waits for the end of the tour instead of derailing it).\nAfter the last answer, the note will say so: reply with ONE warm wrap, the way a friend ends a good first chat, never abruptly (Farza 2026-10-21: the wrap felt abrupt). First a few words reacting to what they just said; then thank them for telling you about themselves and say you really appreciate it (with their first name if you know it); then one short line naming the thing they talked about most, in THEIR words (the symptom tracker, the thesis, the trip), never \"everything\" or \"your work\", as a great starting point. The feeling: \"Claude Code, nice, so you know the drill. Thanks for telling me about yourself, <their name>, I really appreciate it. <Their thing> gives me a great starting point.\" Your own words each time, three short sentences, contractions, no dashes. Never offer help or say what you can or will do for them (no \"I can help you…\", no plan, no promise), no observation or advice, no recap, no \"based on what you told me\", no question. Then stop. Never ask about their name or email.");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06000F9E RID: 3998 RVA: 0x0057BEAC File Offset: 0x0057BEAC
    private void BeginWrapUp()
    {
      if (this.IsWrappingUp)
      {
        return;
      }
      this.IsWrappingUp = true;
      long started = Environment.TickCount64;
      bool spoken = false;
      long? quietSince = null;
      DispatcherQueueTimer dispatcherQueueTimer = this.wrapWatch;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      this.wrapWatch = this.dq.CreateTimer();
      this.wrapWatch.Interval = TimeSpan.FromMilliseconds(100L, 0L);
      this.wrapWatch.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        if (!this.IsActive || this.IsPlanInFlight)
        {
          this.wrapWatch.Stop();
          return;
        }
        VoiceState state = this.s.Companion.State;
        if (state == VoiceState.Responding)
        {
          spoken = true;
        }
        if (spoken)
        {
          RealStack realStack = this.real;
          DateTime? dateTime = ((realStack != null) ? realStack.Canvas.NarrationAudioEndsAt : null);
          if (dateTime != null)
          {
            DateTime valueOrDefault = dateTime.GetValueOrDefault();
            if (DateTime.UtcNow >= valueOrDefault.AddSeconds(0.2))
            {
              this.wrapWatch.Stop();
              this.FinishAndBuildPlan(true);
              return;
            }
          }
        }
        if (state != VoiceState.Idle)
        {
          quietSince = null;
          return;
        }
        long num = quietSince.GetValueOrDefault();
        if (quietSince == null)
        {
          num = Environment.TickCount64;
          quietSince = new long?(num);
        }
        double num2 = (double)(Environment.TickCount64 - quietSince.Value) / 1000.0;
        bool flag = spoken && num2 >= 1.5;
        bool flag2 = (double)(Environment.TickCount64 - started) / 1000.0 >= 20.0 && num2 >= 3.0;
        if (flag || flag2)
        {
          this.wrapWatch.Stop();
          this.FinishAndBuildPlan(true);
        }
      };
      this.wrapWatch.Start();
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06000F9F RID: 3999 RVA: 0x0057BF60 File Offset: 0x0057BF60
    public void FinishAndBuildPlan(bool revealAfter)
    {
      if (this.IsPlanInFlight || this.HasBuiltPlan)
      {
        return;
      }
      DispatcherQueueTimer dispatcherQueueTimer = this.wrapWatch;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      IReadOnlyList<InterviewLine> readOnlyList = this.Transcript;
      if (!readOnlyList.Any<InterviewLine>((InterviewLine l) => l.Role == "user") && !this.UsesNewAgentsUiDemoPlan)
      {
        this.End(false);
        return;
      }
      this.IsPlanInFlight = true;
      string text = "onboarding_starter_plan_started";
      string text2 = null;
      string text3 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["interview_id"] = this.InterviewId;
      jsonObject["answer_count"] = this.AnswerCount;
      ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
      PortraitAppearance appearance = this.Appearance;
      this.End(true);
      if (revealAfter)
      {
        this.WasCompleted = true;
        Action completed = this.Completed;
        if (completed != null)
        {
          completed();
        }
      }
      Action changed = this.Changed;
      if (changed != null)
      {
        changed();
      }
      this.RunPlanAsync(readOnlyList, appearance);
    }

    // Token: 0x06000FA0 RID: 4000 RVA: 0x0057C060 File Offset: 0x0057C060
    private async Task RunPlanAsync(IReadOnlyList<InterviewLine> interview, PortraitAppearance initialAppearance)
    {
      bool landed = false;
      Stopwatch sw = Stopwatch.StartNew();
      string analyticsInterviewId = this.InterviewId;
      try
      {
        StarterPlanResponse plan;
        if (this.UsesNewAgentsUiDemoPlan)
        {
          StarterPlanResponse starterPlanResponse = NewAgentsUiDemoPlan.Plan();
          if (starterPlanResponse != null)
          {
            Log.Info("interview", "new agents UI demo: founding the fixed starter set");
            plan = starterPlanResponse;
            goto IL_0619;
          }
        }
        RealStack r = this.real;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
        if (r != null)
        {
          IntegrationCatalog.Apps apps = await IntegrationCatalog.FetchAsync(r);
          if (apps.All.Count == 0)
          {
            await Task.Delay(1000);
            apps = await IntegrationCatalog.FetchAsync(r);
          }
          IReadOnlyList<string> available = ((apps.All.Count > 0) ? apps.All : (this.availableIntegrations ?? Array.Empty<string>()));
          List<string> connected = apps.ConnectedLines.Select<string, string>((string l) => l.Split(':', StringSplitOptions.None)[0].Trim()).ToList<string>();
          List<StarterPlanClickyInput> clickys = (from c in ClickyRoster.Shared.Clickys
            where !StarterCast.IsCast(c.Slug)
            select new StarterPlanClickyInput(c.Slug, c.Name, c.RoleText, c.OneLiner, Array.Empty<StarterPlanRoutine>(), Array.Empty<string>())).ToList<StarterPlanClickyInput>();
          string text = "interview";
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(37, 3);
          defaultInterpolatedStringHandler.AppendLiteral("starter plan: ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(interview.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" lines, ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(interview.Count<InterviewLine>((InterviewLine l) => l.Role == "user"));
          defaultInterpolatedStringHandler.AppendLiteral(" answers, ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(available.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" apps");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
          int num = 0;
          try
          {
            plan = await r.Core.Worker.BuildStarterPlanAsync(new StarterPlanRequest(interview, connected, available, (clickys.Count > 0) ? clickys : null, null, new bool?(true)), default(CancellationToken));
          }
          catch (Exception obj)
          {
            num = 1;
          }
          object obj;
          if (num == 1)
          {
            Log.Warn("interview", "starter plan attempt 1 failed: " + ((Exception)obj).Message + "; retrying");
            await Task.Delay(2000);
            plan = await r.Core.Worker.BuildStarterPlanAsync(new StarterPlanRequest(interview, connected, available, (clickys.Count > 0) ? clickys : null, null, new bool?(true)), default(CancellationToken));
          }
          obj = null;
          available = null;
          connected = null;
          clickys = null;
        }
        else
        {
          await Task.Delay(1500);
          plan = InterviewController.SamplePlan();
        }
        r = null;
        IL_0619:
        this.LastPlan = plan;
        if (plan != null)
        {
          List<string> list;
          Dictionary<string, string> dictionary;
          landed = InterviewController.Found(plan, out list, out dictionary);
          this.FoundedSlugs = list;
          this.RevealLines = dictionary;
        }
        string text2 = "interview";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(51, 5);
        defaultInterpolatedStringHandler.AppendLiteral("starter plan landed=");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(landed);
        defaultInterpolatedStringHandler.AppendLiteral(" in ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(sw.Elapsed.TotalSeconds, "0.0");
        defaultInterpolatedStringHandler.AppendLiteral(" s: clickys=");
        StarterPlanResponse starterPlanResponse2 = plan;
        int? num2;
        if (starterPlanResponse2 == null)
        {
          num2 = null;
        }
        else
        {
          IReadOnlyList<StarterPlanClicky> clickys2 = starterPlanResponse2.Clickys;
          num2 = ((clickys2 != null) ? new int?(clickys2.Count) : null);
        }
        int? num3 = num2;
        defaultInterpolatedStringHandler.AppendFormatted<int>(num3.GetValueOrDefault());
        defaultInterpolatedStringHandler.AppendLiteral(" cards=");
        StarterPlanResponse starterPlanResponse3 = plan;
        int? num4;
        if (starterPlanResponse3 == null)
        {
          num4 = null;
        }
        else
        {
          IReadOnlyList<StarterPlanSuggestion> suggestedAgents = starterPlanResponse3.SuggestedAgents;
          num4 = ((suggestedAgents != null) ? new int?(suggestedAgents.Count) : null);
        }
        num3 = num4;
        defaultInterpolatedStringHandler.AppendFormatted<int>(num3.GetValueOrDefault());
        defaultInterpolatedStringHandler.AppendLiteral(" goal=\"");
        StarterPlanResponse starterPlanResponse4 = plan;
        defaultInterpolatedStringHandler.AppendFormatted((starterPlanResponse4 != null) ? starterPlanResponse4.GoalLine : null);
        defaultInterpolatedStringHandler.AppendLiteral("\"");
        Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
        plan = null;
      }
      catch (Exception ex)
      {
        this.PlanError = ex.Message;
        Log.Error("interview", "starter plan failed", ex);
      }
      this.dq.TryEnqueue(delegate
      {
        string text3 = "onboarding_starter_plan_finished";
        string text4 = null;
        string text5 = null;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["interview_id"] = analyticsInterviewId;
        jsonObject["success"] = landed;
        jsonObject["duration_seconds"] = sw.Elapsed.TotalSeconds;
        jsonObject["clicky_count"] = this.FoundedSlugs.Count;
        jsonObject["suggestion_count"] = ClickyRoster.Shared.StarterCards.Count;
        ClickyAnalytics.TrackProductEvent(text3, text4, text5, jsonObject);
        this.IsPlanInFlight = false;
        this.HasBuiltPlan = landed;
        if (landed)
        {
          Func<bool> isOnInterviewStep = this.IsOnInterviewStep;
          if (isOnInterviewStep != null && isOnInterviewStep())
          {
            Sounds.Play("agent-done", 0.35);
            goto IL_0112;
          }
        }
        if (landed)
        {
          Log.Info("interview", "starter plan landed off the interview step: no chime");
        }
        IL_0112:
        Action<bool> planLanded = this.PlanLanded;
        if (planLanded != null)
        {
          planLanded(landed);
        }
        Action changed = this.Changed;
        if (changed == null)
        {
          return;
        }
        changed();
      });
    }

    // Token: 0x06000FA1 RID: 4001 RVA: 0x0057C0AC File Offset: 0x0057C0AC
    private static bool Found(StarterPlanResponse plan, out List<string> foundedSlugs, out Dictionary<string, string> revealLines)
    {
      foundedSlugs = new List<string>();
      revealLines = new Dictionary<string, string>();
      List<StarterTaskCard> list = (plan.SuggestedAgents ?? Array.Empty<StarterPlanSuggestion>()).Where<StarterPlanSuggestion>((StarterPlanSuggestion x) => x.Title.Length > 0 && x.Description.Length > 0 && x.AgentPrompt.Length > 0).Select<StarterPlanSuggestion, StarterTaskCard>(delegate(StarterPlanSuggestion x)
      {
        string title = x.Title;
        string description = x.Description;
        string agentPrompt = x.AgentPrompt;
        string text2 = null;
        StarterPlanRoutine routine = x.Routine;
        string text3 = ((routine != null) ? routine.Title : null);
        StarterPlanRoutine routine2 = x.Routine;
        return new StarterTaskCard(title, description, agentPrompt, text2, text3, (routine2 != null) ? new int?(routine2.EveryMinutes) : null, x.SuggestionId, x.Deliverable);
      }).ToList<StarterTaskCard>();
      ClickyRoster.Shared.SetStarterPlan(list, string.IsNullOrWhiteSpace(plan.GoalLine) ? null : plan.GoalLine.Trim(), plan.AgentFamiliarity == "familiar");
      string text = "interview";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 1);
      defaultInterpolatedStringHandler.AppendLiteral("starter plan landed cards=");
      defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      return list.Count > 0;
    }

    // Token: 0x06000FA2 RID: 4002 RVA: 0x0057C198 File Offset: 0x0057C198
    public void UseTranscriptForDrawStepTest(IReadOnlyList<InterviewLine> lines)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.transcript.Clear();
        this.transcript.AddRange(lines);
      }
    }

    // Token: 0x06000FA3 RID: 4003 RVA: 0x0057C1EC File Offset: 0x0057C1EC
    private async Task PrefetchIntegrationsAsync()
    {
      if (this.real != null && this.availableIntegrations == null)
      {
        IReadOnlyList<string> readOnlyList = await this.FetchIntegrationsAsync();
        this.availableIntegrations = readOnlyList;
      }
    }

    // Token: 0x06000FA4 RID: 4004 RVA: 0x0057C230 File Offset: 0x0057C230
    private async Task<IReadOnlyList<string>> FetchIntegrationsAsync()
    {
      IReadOnlyList<string> readOnlyList;
      try
      {
        JsonNode jsonNode = await this.real.Core.Worker.GetJsonObjectAsync("/agent/integrations", default(CancellationToken));
        InterviewController.<>c__DisplayClass139_0 CS$<>8__locals1;
        CS$<>8__locals1.names = new List<string>();
        InterviewController.<FetchIntegrationsAsync>g__Walk|139_0(jsonNode, ref CS$<>8__locals1);
        readOnlyList = CS$<>8__locals1.names.Distinct<string>().Take<string>(80).ToList<string>();
      }
      catch (Exception ex)
      {
        Log.Warn("interview", "integrations: " + ex.Message);
        readOnlyList = Array.Empty<string>();
      }
      return readOnlyList;
    }

    // Token: 0x06000FA5 RID: 4005 RVA: 0x0057C274 File Offset: 0x0057C274
    private static StarterPlanResponse SamplePlan()
    {
      return new StarterPlanResponse(new StarterPlanClicky[]
      {
        new StarterPlanClicky("hook", "Hook Lab", "Script coach", "Finds the hooks that make devlogs land and drafts yours.", new string[] { "I can break down the hooks the biggest devlog channels open with." }, "Hook Lab studies what makes devlogs click.", new string[] { "Watch the last 20 devlogs from the top indie channels and rank their hooks" }, null),
        new StarterPlanClicky("steam", "Steam Scout", "Store page scout", "Builds a Steam page that sells your demo.", new string[] { "I can pull apart the best demo pages on Steam." }, "Steam Scout gets your demo page ready.", new string[] { "Read the top 30 indie demo pages and make me a page checklist" }, null),
        new StarterPlanClicky("pulse", "Channel Pulse", "Growth tracker", "Keeps an eye on what's working on your channel.", new string[] { "I can tell you every morning what moved on your channel." }, "Channel Pulse watches your numbers.", new string[] { "Every morning, tell me which video gained the most subscribers" }, null)
      }, new StarterPlanSuggestion[]
      {
        new StarterPlanSuggestion("Rank the hooks of 20 top devlogs", "A verdict on which openings keep viewers, with three for your next video.", "Watch the last 20 devlogs from the biggest indie game channels and rank their hooks.", "chat", null, "hook", null, null),
        new StarterPlanSuggestion("Build your Steam page checklist", "A live page comparing the best indie demo pages, ready to copy.", "Read the top 30 indie demo pages on Steam and build a checklist page.", "page", null, "steam", null, null),
        new StarterPlanSuggestion("Daily channel pulse", "Every morning: what moved on your channel and why.", "Check which video gained the most subscribers since yesterday.", "routine", null, "pulse", new StarterPlanRoutine("Daily channel pulse", 1440), null)
      }, "unfamiliar", "Getting to 100k subscribers and shipping the Steam demo.");
    }

    // Token: 0x06000FA6 RID: 4006 RVA: 0x0057C3E8 File Offset: 0x0057C3E8
    private void After(double seconds, Action action)
    {
      DispatcherQueueTimer t = this.dq.CreateTimer().Rooted();
      t.Interval = TimeSpan.FromSeconds(seconds);
      t.IsRepeating = false;
      this.pendingTimers.Add(t);
      t.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.pendingTimers.Remove(t);
        action();
      };
      t.Start();
    }

    // Token: 0x06000FB2 RID: 4018 RVA: 0x0057C69C File Offset: 0x0057C69C
    [NullableContext(2)]
    [CompilerGenerated]
    internal static void <FetchIntegrationsAsync>g__Walk|139_0(JsonNode n, ref InterviewController.<>c__DisplayClass139_0 A_1)
    {
      JsonObject jsonObject = n as JsonObject;
      if (jsonObject != null)
      {
        JsonValue jsonValue = jsonObject["display_name"] as JsonValue;
        string text;
        if (jsonValue != null && jsonValue.TryGetValue<string>(out text) && text.Length > 0)
        {
          A_1.names.Add(text);
          return;
        }
        using (IEnumerator<KeyValuePair<string, JsonNode>> enumerator = jsonObject.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<string, JsonNode> keyValuePair = enumerator.Current;
            InterviewController.<FetchIntegrationsAsync>g__Walk|139_0(keyValuePair.Value, ref A_1);
          }
          return;
        }
      }
      JsonArray jsonArray = n as JsonArray;
      if (jsonArray != null)
      {
        foreach (JsonNode jsonNode in jsonArray)
        {
          InterviewController.<FetchIntegrationsAsync>g__Walk|139_0(jsonNode, ref A_1);
        }
      }
    }

    // Token: 0x04000675 RID: 1653
    public const int QuestionBudget = 4;

    // Token: 0x04000676 RID: 1654
    public static readonly string[] OpeningParts = new string[] { "So, I'm HeyClicky! I'm here to help you make things, learn things, start a business, whatever you're going for.", "I'm totally just curious, I have four quick questions. What are you working on these days? What's your story?" };

    // Token: 0x04000677 RID: 1655
    private const string SpokenConversationRule = "[App] This turn is a short spoken conversation inside HeyClicky itself, not a drawing. Answer with say() lines only: no marks, no cards, no photos, no buttons, nothing drawn. Short sentences, one per say().";

    // Token: 0x04000678 RID: 1656
    private readonly AppServices s;

    // Token: 0x04000679 RID: 1657
    [Nullable(2)]
    private readonly RealStack real;

    // Token: 0x0400067A RID: 1658
    private readonly DispatcherQueue dq;

    // Token: 0x0400067B RID: 1659
    private readonly object gate = new object();

    // Token: 0x0400067C RID: 1660
    private readonly List<InterviewLine> transcript = new List<InterviewLine>();

    // Token: 0x0400067D RID: 1661
    private int answerCount;

    // Token: 0x0400067E RID: 1662
    private bool pendingTypedAnswer;

    // Token: 0x0400067F RID: 1663
    private string lastHeldWords = "";

    // Token: 0x04000680 RID: 1664
    private VoiceState lastState;

    // Token: 0x04000681 RID: 1665
    [Nullable(2)]
    private DispatcherQueueTimer wrapWatch;

    // Token: 0x04000682 RID: 1666
    [Nullable(2)]
    private DispatcherQueueTimer openerWatch;

    // Token: 0x04000683 RID: 1667
    [Nullable(new byte[] { 2, 1 })]
    private IReadOnlyList<string> availableIntegrations;

    // Token: 0x04000696 RID: 1686
    private long startedAt;

    // Token: 0x04000697 RID: 1687
    private readonly bool hearsTurnEnds;

    // Token: 0x04000699 RID: 1689
    private const int OpenerFirstLine = -100;

    // Token: 0x0400069A RID: 1690
    private static readonly TimeSpan PreparationLifetime = TimeSpan.FromMinutes(5L);

    // Token: 0x0400069B RID: 1691
    [Nullable(new byte[] { 2, 2, 1 })]
    private Task<byte[][]> preparedOpening;

    // Token: 0x0400069C RID: 1692
    [Nullable(2)]
    private CancellationTokenSource preparationCancellation;

    // Token: 0x0400069D RID: 1693
    private long preparedAt;

    // Token: 0x0400069E RID: 1694
    private int preparationAttempts;

    // Token: 0x040006A0 RID: 1696
    private int openerPartsShown;

    // Token: 0x040006A1 RID: 1697
    private long openerStartedAt;

    // Token: 0x040006A2 RID: 1698
    private readonly HashSet<DispatcherQueueTimer> pendingTimers = new HashSet<DispatcherQueueTimer>();
  }
}
