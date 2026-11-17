using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Auth;
using HeyClicky.Core.Net;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Voice;

namespace HeyClicky.Core.Dictation
{
  // Token: 0x020001C0 RID: 448
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DictationController : IAsyncDisposable
  {
    // Token: 0x060019F2 RID: 6642 RVA: 0x003D4D04 File Offset: 0x003D4D04
    public DictationController(ClickyHttp http, SupabaseAuthService auth, SettingsStore settings, IMicrophoneCapture microphone, IDictationTextTarget target, NotchPresenter notch, [Nullable(2)] DictationControllerOptions options = null)
    {
      DictationController <>4__this = this;
      this.http = http;
      this.auth = auth;
      this.settings = settings;
      this.microphone = microphone;
      this.target = target;
      this.notch = notch;
      this.options = options ?? new DictationControllerOptions();
      this.actor = new DictationController.ActorContext("HeyClicky dictation");
      this.hotkeys = new DictationHotkeyStateMachine(new DictationController.ActorScheduler(this.actor), () => <>4__this.isSessionActive, new Action(this.Begin), new Action(this.End), new Action(this.OnLockedHandsFree))
      {
        IsOnboardingTutorialActive = this.options.IsOnboardingTutorialRestricting
      };
      microphone.Frame += this.OnMicrophoneFrame;
      auth.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        if (!auth.IsSignedIn)
        {
          <>4__this.actor.Post(new Action(<>4__this.DiscardWarmConnection));
        }
      };
      this.warmupTimer = new Timer(delegate(object _)
      {
        <>4__this.WarmUpNetworkConnections();
      }, null, TimeSpan.FromSeconds(5L), TimeSpan.FromSeconds(55L));
    }

    // Token: 0x060019F3 RID: 6643 RVA: 0x003D4E3C File Offset: 0x003D4E3C
    private void WarmUpNetworkConnections()
    {
      double? num = this.settings.Read<double?>((AppSettings s) => s.DictationLastCompletedAtEpochSeconds);
      if (num != null)
      {
        double valueOrDefault = num.GetValueOrDefault();
        if ((double)DateTimeOffset.UtcNow.ToUnixTimeSeconds() - valueOrDefault <= 604800.0 && this.auth.IsSignedIn)
        {
          this.http.PrewarmWorker();
          this.http.PrewarmGemini();
          return;
        }
      }
    }

    // Token: 0x170005C0 RID: 1472
    // (get) Token: 0x060019F4 RID: 6644 RVA: 0x003D4EC4 File Offset: 0x003D4EC4
    public bool IsSessionActive
    {
      get
      {
        return this.activeFlag;
      }
    }

    // Token: 0x170005C1 RID: 1473
    // (get) Token: 0x060019F5 RID: 6645 RVA: 0x003D4ED0 File Offset: 0x003D4ED0
    public bool IsCapturingAudio
    {
      get
      {
        return this.capturing;
      }
    }

    // Token: 0x170005C2 RID: 1474
    // (get) Token: 0x060019F6 RID: 6646 RVA: 0x003D4EDC File Offset: 0x003D4EDC
    public DictationEngagement Engagement
    {
      get
      {
        return this.hotkeys.Engagement;
      }
    }

    // Token: 0x14000036 RID: 54
    // (add) Token: 0x060019F7 RID: 6647 RVA: 0x003D4EEC File Offset: 0x003D4EEC
    // (remove) Token: 0x060019F8 RID: 6648 RVA: 0x003D4F24 File Offset: 0x003D4F24
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<DictationResult> Completed;

    // Token: 0x060019F9 RID: 6649 RVA: 0x003D4F5C File Offset: 0x003D4F5C
    public void HandleShortcut(ShortcutAction action)
    {
      switch (action)
      {
      case ShortcutAction.DictationDown:
        this.actor.Post(delegate
        {
          if (!this.options.OnboardingTutorialAllowsPress())
          {
            Log.Info("Dictation", "press swallowed by the tour");
            return;
          }
          this.hotkeys.Pressed();
        });
        return;
      case ShortcutAction.DictationUp:
        this.actor.Post(new Action(this.hotkeys.Released));
        return;
      case ShortcutAction.Escape:
        break;
      case ShortcutAction.DictationCancel:
        this.actor.Post(delegate
        {
          if (!this.isSessionActive || this.hotkeys.Engagement != DictationEngagement.Holding)
          {
            return;
          }
          Log.Info("Dictation", "Ctrl+Win+key is a Windows shortcut: dictation cancelled");
          this.Cancel();
        });
        return;
      case ShortcutAction.HandsFreeDictationToggle:
        this.actor.Post(new Action(this.hotkeys.ToggleHandsFree));
        break;
      default:
        return;
      }
    }

    // Token: 0x170005C3 RID: 1475
    // (set) Token: 0x060019FA RID: 6650 RVA: 0x003D4FF0 File Offset: 0x003D4FF0
    public bool HasDedicatedHandsFreeShortcut
    {
      set
      {
        this.actor.Post(delegate
        {
          this.hotkeys.IsDedicatedHandsFreeShortcutConfigured = value;
        });
      }
    }

    // Token: 0x060019FB RID: 6651 RVA: 0x003D5028 File Offset: 0x003D5028
    public bool CancelActiveSession()
    {
      if (!this.activeFlag)
      {
        return false;
      }
      this.actor.Post(delegate
      {
        if (!this.isSessionActive)
        {
          return;
        }
        this.options.PlayCue("textClose", 0.5f);
        this.Cancel();
      });
      return true;
    }

    // Token: 0x060019FC RID: 6652 RVA: 0x003D5050 File Offset: 0x003D5050
    public void RemoveLearnedWord(string word)
    {
      DictationVocabulary.RemoveTerm(this.settings, word);
      this.notch.DismissLiveEvent(typeof(NotchLiveEvent.DictionaryWordLearned));
      Log.Info("Dictation", "learned word removed: " + word);
    }

    // Token: 0x060019FD RID: 6653 RVA: 0x003D5088 File Offset: 0x003D5088
    private void Begin()
    {
      if (this.isSessionActive)
      {
        return;
      }
      if (!this.auth.IsSignedIn)
      {
        this.PresentUserVisibleError("Please sign in to use dictation.");
        return;
      }
      if (this.options.IsMicrophoneDenied())
      {
        this.PresentUserVisibleError("Microphone access is needed for dictation.");
        return;
      }
      if (!this.options.CanStartRecording())
      {
        Log.Info("Dictation", "press ignored: the microphone is busy with push-to-talk");
        return;
      }
      this.StartSession();
    }

    // Token: 0x060019FE RID: 6654 RVA: 0x003D5100 File Offset: 0x003D5100
    private void StartSession()
    {
      CancellationTokenSource cancellationTokenSource = this.correctionWatch;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.correctionWatch = null;
      bool flag = (this.activeFlag = true);
      this.isSessionActive = flag;
      int num = this.sessionToken + 1;
      this.sessionToken = num;
      int num2 = num;
      this.streamingPartialBeforeFailure = null;
      this.usedWarmConnection = false;
      this.startedOnTutorialDictationStep = this.options.IsOnboardingTutorialDictationStepActive();
      this.capturedFocus = null;
      this.keyDownWorkAppWithoutFocus = null;
      this.framesThisSession = 0;
      this.sessionStartedTicks = Environment.TickCount64;
      this.releaseTicks = 0L;
      (this.options.PlayMicrophoneStartCue ?? this.options.PlayCue)("textOpen", 0.35f);
      this.SetLabel("Dictating", true);
      this.http.PrewarmWorker();
      AppSettings appSettings = this.settings.Current;
      string text = (string.IsNullOrWhiteSpace(appSettings.DictationLanguageCode) ? "multi" : appSettings.DictationLanguageCode.Trim());
      IReadOnlyList<string> readOnlyList = DictationVocabulary.Terms(appSettings.DictationCustomWords);
      string text2 = this.options.UsageContext();
      DictationTranscriptionClient dictationTranscriptionClient = new DictationTranscriptionClient(this.http, (bool force, CancellationToken ct) => this.auth.GetValidAccessTokenAsync(force, ct), text2, text, this.microphone.SampleRate)
      {
        KnownTerms = readOnlyList
      };
      GeminiTranscriber geminiTranscriber = this.TakeWarmConnection(text, readOnlyList, text2);
      object obj = this.frameGate;
      lock (obj)
      {
        this.client = dictationTranscriptionClient;
        this.streamer = geminiTranscriber;
      }
      this.usedWarmConnection = geminiTranscriber != null;
      try
      {
        this.capturing = true;
        this.microphone.Start();
      }
      catch (Exception ex)
      {
        Log.Error("Dictation", "microphone did not start", ex);
        this.HandleSessionError("Couldn't start the microphone.");
        return;
      }
      string text3 = "Dictation";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 3);
      defaultInterpolatedStringHandler.AppendLiteral("session started (");
      defaultInterpolatedStringHandler.AppendFormatted((geminiTranscriber == null) ? "cold" : "warm socket");
      defaultInterpolatedStringHandler.AppendLiteral(", language ");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral(", ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(readOnlyList.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" custom words)");
      Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
      this.setupTask = this.SetUpSessionAsync(num2, dictationTranscriptionClient, geminiTranscriber, text, readOnlyList, text2);
    }

    // Token: 0x060019FF RID: 6655 RVA: 0x003D537C File Offset: 0x003D537C
    private unsafe async Task SetUpSessionAsync(int token, DictationTranscriptionClient newClient, [Nullable(2)] GeminiTranscriber adopted, string language, IReadOnlyList<string> customWords, string usageContext)
    {
      Task<DictationFocus> focusTask = this.target.CaptureFocusAsync(default(CancellationToken));
      TaskAwaiter<Task> taskAwaiter = Task.WhenAny(focusTask, Task.Delay(DictationController.FocusCaptureBudget)).GetAwaiter();
      if (!taskAwaiter.IsCompleted)
      {
        await taskAwaiter;
        TaskAwaiter<Task> taskAwaiter2;
        taskAwaiter = taskAwaiter2;
        taskAwaiter2 = default(TaskAwaiter<Task>);
      }
      DictationFocus dictationFocus;
      if (taskAwaiter.GetResult() == focusTask)
      {
        dictationFocus = await focusTask;
      }
      else
      {
        dictationFocus = null;
      }
      DictationFocus dictationFocus2 = dictationFocus;
      if (token == this.sessionToken)
      {
        this.capturedFocus = dictationFocus2;
        this.keyDownWorkAppWithoutFocus = ((dictationFocus2 == null) ? this.target.ForegroundWorkApp() : null);
        DictationCleanupContext dictationCleanupContext;
        if (dictationFocus2 == null)
        {
          DictationWorkApp dictationWorkApp = this.keyDownWorkAppWithoutFocus;
          string text = ((dictationWorkApp != null) ? dictationWorkApp.AppName : null) ?? "";
          DictationWorkApp dictationWorkApp2 = this.keyDownWorkAppWithoutFocus;
          dictationCleanupContext = new DictationCleanupContext(text, (dictationWorkApp2 != null) ? dictationWorkApp2.SiteHost : null, "", "", "", false);
        }
        else
        {
          dictationCleanupContext = new DictationCleanupContext(dictationFocus2.AppName, dictationFocus2.SiteHost, dictationFocus2.BeforeText, dictationFocus2.SelectedText, dictationFocus2.AfterText, dictationFocus2.IsSingleLine);
        }
        newClient.Context = dictationCleanupContext;
        IReadOnlyList<string> readOnlyList;
        if (dictationFocus2 != null)
        {
          string text2 = " ";
          <>y__InlineArray3<string> <>y__InlineArray = default(<>y__InlineArray3<string>);
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 0) = dictationFocus2.BeforeText;
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 1) = dictationFocus2.SelectedText;
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 2) = dictationFocus2.AfterText;
          readOnlyList = DictationVocabulary.ContextKeyterms(string.Join(text2, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray3<string>, string>(in <>y__InlineArray, 3)));
        }
        else
        {
          readOnlyList = Array.Empty<string>();
        }
        IReadOnlyList<string> readOnlyList2 = readOnlyList;
        List<string> list = customWords.Concat<string>(readOnlyList2).Distinct<string>(StringComparer.OrdinalIgnoreCase).Take<string>(50)
          .ToList<string>();
        string text3 = "Dictation";
        string text4;
        if (dictationFocus2 != null)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(45, 6);
          defaultInterpolatedStringHandler.AppendLiteral("focus: ");
          defaultInterpolatedStringHandler.AppendFormatted(dictationFocus2.Role);
          defaultInterpolatedStringHandler.AppendLiteral(" in ");
          defaultInterpolatedStringHandler.AppendFormatted(dictationFocus2.AppName);
          defaultInterpolatedStringHandler.AppendFormatted(dictationFocus2.IsSecure ? " (password field)" : "");
          defaultInterpolatedStringHandler.AppendFormatted(dictationFocus2.IsSingleLine ? ", single line" : "");
          defaultInterpolatedStringHandler.AppendLiteral(", ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(dictationFocus2.BeforeText.Length);
          defaultInterpolatedStringHandler.AppendLiteral(" chars before, ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(readOnlyList2.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" context keyterms");
          text4 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        else
        {
          string text5 = "focus: no editable field (app ";
          DictationWorkApp dictationWorkApp3 = this.keyDownWorkAppWithoutFocus;
          string text6 = ((dictationWorkApp3 != null) ? dictationWorkApp3.AppName : null) ?? "?";
          DictationWorkApp dictationWorkApp4 = this.keyDownWorkAppWithoutFocus;
          text4 = text5 + text6 + ((dictationWorkApp4 != null && dictationWorkApp4.AllowsBlindPaste) ? ", blind paste allowed" : "") + ")";
        }
        Log.Info(text3, text4);
        GeminiTranscriber transcriber;
        if (adopted != null)
        {
          newClient.KnownTerms = list;
          transcriber = adopted;
        }
        else
        {
          transcriber = this.NewStreamingTranscriber(language, list, usageContext);
          transcriber.Start(false);
          object obj = this.frameGate;
          lock (obj)
          {
            transcriber.PushAudio(newClient.AccumulatedAudio());
            this.streamer = transcriber;
          }
        }
        Action <>9__1;
        transcriber.Opened.ContinueWith(delegate(Task<bool> _)
        {
          if (transcriber.PaywallReached)
          {
            DictationController.ActorContext actorContext = this.actor;
            Action action;
            if ((action = <>9__1) == null)
            {
              action = (<>9__1 = delegate
              {
                if (token == this.sessionToken)
                {
                  this.HandlePaywallReached();
                }
              });
            }
            actorContext.Post(action);
          }
        }, TaskScheduler.Default);
      }
    }

    // Token: 0x06001A00 RID: 6656 RVA: 0x003D53F4 File Offset: 0x003D53F4
    private void OnLockedHandsFree()
    {
      this.SetLabel("Dictating (hands-free)", true);
      this.options.PlayCue("textOpen", 0.22f);
      Log.Info("Dictation", "hands-free (double-tap)");
    }

    // Token: 0x06001A01 RID: 6657 RVA: 0x003D542C File Offset: 0x003D542C
    private void End()
    {
      if (!this.isSessionActive)
      {
        return;
      }
      this.options.PlayCue("textReceive", 0.6f);
      this.SetLabel("Transcribing", false);
      this.releaseTicks = Environment.TickCount64;
      int token = this.sessionToken;
      IDisposable disposable = this.tailTimer;
      if (disposable != null)
      {
        disposable.Dispose();
      }
      this.tailTimer = new DictationController.ActorScheduler(this.actor).Schedule(DictationController.MicrophoneTail, delegate
      {
        if (token != this.sessionToken)
        {
          return;
        }
        this.StopMicrophone();
        if (this.releaseTicks - this.sessionStartedTicks > 1500L && Volatile.Read(ref this.framesThisSession) == 0)
        {
          this.notch.ShowLiveEvent(new NotchLiveEvent.MicrophoneTrouble("I can't hear your microphone. Is it muted or unplugged?", null, true), null);
        }
        this.FinishAsync(token);
      });
    }

    // Token: 0x06001A02 RID: 6658 RVA: 0x003D54C4 File Offset: 0x003D54C4
    private async Task FinishAsync(int token)
    {
      try
      {
        await this.setupTask;
        if (token == this.sessionToken)
        {
          GeminiTranscriber transcriber = this.streamer;
          DictationTranscriptionClient batchClient = this.client;
          if (batchClient != null)
          {
            if (transcriber != null && transcriber.PaywallReached)
            {
              this.HandlePaywallReached();
            }
            else if (transcriber == null || transcriber.DidFail)
            {
              GeminiTranscriber geminiTranscriber = transcriber;
              this.streamingPartialBeforeFailure = ((geminiTranscriber != null) ? geminiTranscriber.PartialTranscriptSoFar() : null);
              DictationTranscriptionClient dictationTranscriptionClient = batchClient;
              GeminiTranscriber geminiTranscriber2 = transcriber;
              await this.RunBatchAsync(token, dictationTranscriptionClient, (geminiTranscriber2 != null) ? geminiTranscriber2.FailureMessage : null);
            }
            else
            {
              string text = await transcriber.FinishAsync(null);
              if (token == this.sessionToken)
              {
                if (transcriber.PaywallReached)
                {
                  this.HandlePaywallReached();
                }
                else if (transcriber.DidFail)
                {
                  this.streamingPartialBeforeFailure = transcriber.PartialTranscriptSoFar();
                  await this.RunBatchAsync(token, batchClient, transcriber.FailureMessage);
                }
                else
                {
                  int transcriptWaitMs = (int)(Environment.TickCount64 - this.releaseTicks);
                  string text2 = (text ?? "").Trim();
                  double sentAudioSeconds = transcriber.SentAudioSeconds;
                  batchClient.Cancel();
                  if (text2.Length == 0)
                  {
                    await this.DeliverAsync(token, "", false, transcriptWaitMs, null);
                  }
                  else
                  {
                    string finalText = text2;
                    int? cleanupMs = null;
                    long cleanupStarted = Environment.TickCount64;
                    try
                    {
                      finalText = await batchClient.CleanUpAsync(text2, sentAudioSeconds, transcriber.Receipt, true, new int?(transcriptWaitMs), "gemini", default(CancellationToken));
                      cleanupMs = new int?((int)(Environment.TickCount64 - cleanupStarted));
                    }
                    catch (DictationPaywallReachedException)
                    {
                      if (token == this.sessionToken)
                      {
                        this.HandlePaywallReached();
                      }
                      return;
                    }
                    catch (Exception ex)
                    {
                      Log.Warn("Dictation", "cleanup failed, inserting the raw transcript: " + ex.Message);
                    }
                    await this.DeliverAsync(token, finalText, false, transcriptWaitMs, cleanupMs);
                    transcriber = null;
                    batchClient = null;
                    finalText = null;
                  }
                }
              }
            }
          }
        }
      }
      catch (Exception ex2)
      {
        Log.Error("Dictation", "finish failed", ex2);
        if (token == this.sessionToken)
        {
          this.HandleSessionError("Dictation failed. Try again");
        }
      }
    }

    // Token: 0x06001A03 RID: 6659 RVA: 0x003D5510 File Offset: 0x003D5510
    private async Task RunBatchAsync(int token, DictationTranscriptionClient batchClient, [Nullable(2)] string why)
    {
      Log.Info("Dictation", "streaming unavailable (" + (why ?? "no socket") + "): batch transcription");
      try
      {
        TaskAwaiter<ValueTuple<string, double>> taskAwaiter = batchClient.TranscribeAsync(default(CancellationToken)).GetAwaiter();
        if (!taskAwaiter.IsCompleted)
        {
          await taskAwaiter;
          TaskAwaiter<ValueTuple<string, double>> taskAwaiter2;
          taskAwaiter = taskAwaiter2;
          taskAwaiter2 = default(TaskAwaiter<ValueTuple<string, double>>);
        }
        string item = taskAwaiter.GetResult().Item1;
        if (token == this.sessionToken)
        {
          await this.DeliverAsync(token, item, true, (int)(Environment.TickCount64 - this.releaseTicks), null);
        }
      }
      catch (DictationPaywallReachedException)
      {
        if (token == this.sessionToken)
        {
          this.HandlePaywallReached();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Dictation", "batch transcription failed: " + ex.Message);
        if (token == this.sessionToken)
        {
          this.HandleSessionError((ex is HttpRequestException || ex is TaskCanceledException) ? "Couldn't reach HeyClicky's servers." : ex.Message);
        }
      }
    }

    // Token: 0x06001A04 RID: 6660 RVA: 0x003D556C File Offset: 0x003D556C
    private async Task DeliverAsync(int token, string text, bool usedBatch, int transcriptWaitMs, int? cleanupMs)
    {
      if (token == this.sessionToken)
      {
        DictationFocus focus = this.capturedFocus;
        DictationWorkApp workApp = this.keyDownWorkAppWithoutFocus;
        bool handsFree = this.hotkeys.SessionUsedHandsFree;
        long releasedAt = this.releaseTicks;
        bool flag = this.startedOnTutorialDictationStep;
        this.startedOnTutorialDictationStep = false;
        this.FinishSessionState();
        string transcript = text.Trim();
        if (transcript.Length == 0)
        {
          Log.Info("Dictation", "nothing dictated");
          this.SetLabel(null, false);
          Action<DictationResult> completed = this.Completed;
          if (completed != null)
          {
            completed(new DictationResult("", DictationDeliveryOutcome.Nothing, null, usedBatch, handsFree, this.usedWarmConnection, 0));
          }
        }
        else
        {
          string text2 = transcript;
          DictationFocus dictationFocus = focus;
          string text3;
          if ((text3 = ((dictationFocus != null) ? dictationFocus.AppName : null)) == null)
          {
            DictationWorkApp dictationWorkApp = workApp;
            text3 = ((dictationWorkApp != null) ? dictationWorkApp.AppName : null);
          }
          DictationTranscriptArchive.AppendFinishedTranscript(text2, text3);
          this.settings.Update(delegate(AppSettings s)
          {
            s.DictationLastCompletedAtEpochSeconds = new double?((double)DateTimeOffset.UtcNow.ToUnixTimeSeconds());
          });
          string clipboardReason = null;
          bool lateRecapture = false;
          if (this.options.ConsumeOnboardingTutorialTranscript(transcript))
          {
            string text4 = "Dictation";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 1);
            defaultInterpolatedStringHandler.AppendLiteral("transcript delivered to the tour's box (");
            defaultInterpolatedStringHandler.AppendFormatted<int>(transcript.Length);
            defaultInterpolatedStringHandler.AppendLiteral(" chars)");
            Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
            this.options.PlayCue("textSend", 0.5f);
            this.SetLabel(null, false);
            Action<DictationResult> completed2 = this.Completed;
            if (completed2 != null)
            {
              completed2(new DictationResult(transcript, DictationDeliveryOutcome.Inserted, null, usedBatch, handsFree, this.usedWarmConnection, (releasedAt > 0L) ? ((int)(Environment.TickCount64 - releasedAt)) : 0));
            }
            if (!usedBatch)
            {
              this.PrepareWarmConnection();
            }
          }
          else if (flag)
          {
            Log.Info("Dictation", "dropping a tour-step transcript delivered after the step was left");
            this.SetLabel(null, false);
          }
          else
          {
            if (focus == null)
            {
              DictationFocus dictationFocus2 = await this.target.CaptureFocusAsync(default(CancellationToken));
              if (dictationFocus2 != null && workApp != null && dictationFocus2.ProcessId != workApp.ProcessId)
              {
                Log.Info("Dictation", "late re-capture found a field in a different app; keeping the clipboard fallback");
              }
              else
              {
                focus = dictationFocus2;
              }
              lateRecapture = focus != null;
            }
            DictationDeliveryOutcome dictationDeliveryOutcome;
            if (focus != null && focus.IsEditableInsertionTarget)
            {
              string verificationAppName = focus.AppName;
              string verificationProcessName = DictationController.ProcessNameOf(focus.ProcessId);
              DictationInsertOutcome dictationInsertOutcome = await this.target.InsertAsync(transcript, focus, delegate(string verification, int waitMs)
              {
                ClickyAnalytics.TrackDictationPasteVerification(verification, waitMs, verificationAppName, verificationProcessName);
              });
              if (dictationInsertOutcome == DictationInsertOutcome.Inserted)
              {
                this.options.PlayCue("textSend", 0.5f);
                this.SetLabel(null, false);
                dictationDeliveryOutcome = DictationDeliveryOutcome.Inserted;
                this.StartCorrectionLearning(focus, transcript);
              }
              else
              {
                string text5;
                switch (dictationInsertOutcome)
                {
                case DictationInsertOutcome.RefusedUnsafeTarget:
                  text5 = "secure_field";
                  break;
                case DictationInsertOutcome.FocusLost:
                  text5 = "focus_lost";
                  break;
                case DictationInsertOutcome.ModifiersHeld:
                  text5 = "modifiers_held";
                  break;
                case DictationInsertOutcome.ClipboardWriteFailed:
                  text5 = "pasteboard_write_failed";
                  break;
                default:
                  text5 = "empty_transcript";
                  break;
                }
                clipboardReason = text5;
                await this.CopyTranscriptToClipboardAsync(transcript);
                dictationDeliveryOutcome = DictationDeliveryOutcome.Clipboard;
              }
            }
            else
            {
              bool flag2 = focus == null && workApp != null && workApp.AllowsBlindPaste;
              if (flag2)
              {
                flag2 = await this.target.BlindPasteAsync(transcript, workApp);
              }
              if (flag2)
              {
                this.options.PlayCue("textSend", 0.5f);
                this.SetLabel(null, false);
                dictationDeliveryOutcome = DictationDeliveryOutcome.PastedBlind;
              }
              else
              {
                clipboardReason = ((focus == null) ? "no_focus_target" : "secure_field");
                await this.CopyTranscriptToClipboardAsync(transcript);
                dictationDeliveryOutcome = DictationDeliveryOutcome.Clipboard;
              }
            }
            int num = ((releasedAt > 0L) ? ((int)(Environment.TickCount64 - releasedAt)) : 0);
            string text6 = "Dictation";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(121, 10);
            defaultInterpolatedStringHandler.AppendLiteral("dictation completed: ");
            defaultInterpolatedStringHandler.AppendFormatted<DictationDeliveryOutcome>(dictationDeliveryOutcome);
            defaultInterpolatedStringHandler.AppendFormatted((clipboardReason == null) ? "" : (" (" + clipboardReason + ")"));
            defaultInterpolatedStringHandler.AppendLiteral(", ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(transcript.Split(null, StringSplitOptions.RemoveEmptyEntries).Length);
            defaultInterpolatedStringHandler.AppendLiteral(" words, ");
            defaultInterpolatedStringHandler.AppendLiteral("engine ");
            defaultInterpolatedStringHandler.AppendFormatted(usedBatch ? "batch" : "gemini");
            defaultInterpolatedStringHandler.AppendFormatted(this.usedWarmConnection ? " (warm)" : "");
            defaultInterpolatedStringHandler.AppendLiteral(", hands-free ");
            defaultInterpolatedStringHandler.AppendFormatted<bool>(handsFree);
            defaultInterpolatedStringHandler.AppendLiteral(", late re-capture ");
            defaultInterpolatedStringHandler.AppendFormatted<bool>(lateRecapture);
            defaultInterpolatedStringHandler.AppendLiteral(", ");
            defaultInterpolatedStringHandler.AppendLiteral("transcript ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(transcriptWaitMs);
            defaultInterpolatedStringHandler.AppendLiteral(" ms, cleanup ");
            defaultInterpolatedStringHandler.AppendFormatted(((cleanupMs != null) ? cleanupMs.GetValueOrDefault().ToString() : null) ?? "-");
            defaultInterpolatedStringHandler.AppendLiteral(" ms, release to insert ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(num);
            defaultInterpolatedStringHandler.AppendLiteral(" ms");
            Log.Info(text6, defaultInterpolatedStringHandler.ToStringAndClear());
            Action<DictationResult> completed3 = this.Completed;
            if (completed3 != null)
            {
              completed3(new DictationResult(transcript, dictationDeliveryOutcome, clipboardReason, usedBatch, handsFree, this.usedWarmConnection, num));
            }
            if (!usedBatch)
            {
              this.PrepareWarmConnection();
            }
          }
        }
      }
    }

    // Token: 0x06001A05 RID: 6661 RVA: 0x003D55DC File Offset: 0x003D55DC
    [NullableContext(2)]
    private static string ProcessNameOf(int processId)
    {
      string text;
      try
      {
        using (Process processById = Process.GetProcessById(processId))
        {
          text = processById.ProcessName;
        }
      }
      catch (Exception)
      {
        text = null;
      }
      return text;
    }

    // Token: 0x06001A06 RID: 6662 RVA: 0x003D5628 File Offset: 0x003D5628
    private async Task CopyTranscriptToClipboardAsync(string text)
    {
      TaskAwaiter<bool> taskAwaiter = this.target.CopyToClipboardAsync(text).GetAwaiter();
      if (!taskAwaiter.IsCompleted)
      {
        await taskAwaiter;
        TaskAwaiter<bool> taskAwaiter2;
        taskAwaiter = taskAwaiter2;
        taskAwaiter2 = default(TaskAwaiter<bool>);
      }
      if (!taskAwaiter.GetResult())
      {
        Log.Warn("Dictation", "clipboard write failed");
      }
      this.notch.ShowLiveEvent(new NotchLiveEvent.DictationCopiedToClipboard(), null);
      this.SetLabel(null, false);
    }

    // Token: 0x06001A07 RID: 6663 RVA: 0x003D5674 File Offset: 0x003D5674
    private void HandlePaywallReached()
    {
      if (!this.isSessionActive)
      {
        return;
      }
      Log.Info("Dictation", "session ended quietly: dictation word limit (402)");
      this.options.PlayCue("textClose", 0.5f);
      this.FinishSessionState();
      this.SetLabel(null, false);
    }

    // Token: 0x06001A08 RID: 6664 RVA: 0x003D56C4 File Offset: 0x003D56C4
    private void HandleSessionError(string message)
    {
      GeminiTranscriber geminiTranscriber = this.streamer;
      string text;
      if ((text = ((geminiTranscriber != null) ? geminiTranscriber.PartialTranscriptSoFar() : null)) == null)
      {
        text = this.streamingPartialBeforeFailure ?? "";
      }
      string text2 = text.Trim();
      DictationFocus dictationFocus = this.capturedFocus;
      string text3;
      if ((text3 = ((dictationFocus != null) ? dictationFocus.AppName : null)) == null)
      {
        DictationWorkApp dictationWorkApp = this.keyDownWorkAppWithoutFocus;
        text3 = ((dictationWorkApp != null) ? dictationWorkApp.AppName : null);
      }
      string text4 = text3;
      this.FinishSessionState();
      if (text2.Length == 0)
      {
        this.PresentUserVisibleError(message);
        return;
      }
      string text5 = "Dictation";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(51, 2);
      defaultInterpolatedStringHandler.AppendLiteral("session failed (");
      defaultInterpolatedStringHandler.AppendFormatted(message);
      defaultInterpolatedStringHandler.AppendLiteral("); salvaged ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(text2.Length);
      defaultInterpolatedStringHandler.AppendLiteral(" chars to the clipboard");
      Log.Info(text5, defaultInterpolatedStringHandler.ToStringAndClear());
      DictationTranscriptArchive.AppendFinishedTranscript(text2, text4);
      this.CopyTranscriptToClipboardAsync(text2);
    }

    // Token: 0x06001A09 RID: 6665 RVA: 0x003D57A0 File Offset: 0x003D57A0
    private void PresentUserVisibleError(string message)
    {
      Log.Warn("Dictation", "error: " + message);
      ClickyAnalytics.TrackAppError("dictation", "generic", message, null, 0, message, false);
      string label = ((message.Length <= 64) ? message : "Dictation failed. Try again");
      this.SetLabel(label, false);
      new DictationController.ActorScheduler(this.actor).Schedule(TimeSpan.FromSeconds(2.5), delegate
      {
        if (this.currentLabel == label)
        {
          this.SetLabel(null, false);
        }
      });
    }

    // Token: 0x06001A0A RID: 6666 RVA: 0x003D5834 File Offset: 0x003D5834
    private void Cancel()
    {
      this.sessionToken++;
      IDisposable disposable = this.tailTimer;
      if (disposable != null)
      {
        disposable.Dispose();
      }
      this.tailTimer = null;
      this.StopMicrophone();
      this.DisposeSessionTranscribers();
      this.capturedFocus = null;
      this.keyDownWorkAppWithoutFocus = null;
      this.isSessionActive = (this.activeFlag = false);
      this.hotkeys.Reset();
      this.SetLabel(null, false);
      Log.Info("Dictation", "session cancelled");
    }

    // Token: 0x06001A0B RID: 6667 RVA: 0x003D58B8 File Offset: 0x003D58B8
    private void FinishSessionState()
    {
      this.sessionToken++;
      IDisposable disposable = this.tailTimer;
      if (disposable != null)
      {
        disposable.Dispose();
      }
      this.tailTimer = null;
      this.StopMicrophone();
      this.DisposeSessionTranscribers();
      this.streamingPartialBeforeFailure = null;
      this.capturedFocus = null;
      this.keyDownWorkAppWithoutFocus = null;
      this.isSessionActive = (this.activeFlag = false);
      this.hotkeys.Reset();
    }

    // Token: 0x06001A0C RID: 6668 RVA: 0x003D592C File Offset: 0x003D592C
    private void DisposeSessionTranscribers()
    {
      object obj = this.frameGate;
      GeminiTranscriber geminiTranscriber;
      DictationTranscriptionClient dictationTranscriptionClient;
      lock (obj)
      {
        geminiTranscriber = this.streamer;
        dictationTranscriptionClient = this.client;
        this.streamer = null;
        this.client = null;
      }
      if (dictationTranscriptionClient != null)
      {
        dictationTranscriptionClient.Cancel();
      }
      if (geminiTranscriber != null)
      {
        geminiTranscriber.DisposeAsync().AsTask();
      }
    }

    // Token: 0x06001A0D RID: 6669 RVA: 0x003D59A0 File Offset: 0x003D59A0
    private void StopMicrophone()
    {
      if (!this.capturing)
      {
        return;
      }
      this.capturing = false;
      try
      {
        this.microphone.Stop();
      }
      catch (Exception ex)
      {
        Log.Warn("Dictation", "microphone stop: " + ex.Message);
      }
    }

    // Token: 0x06001A0E RID: 6670 RVA: 0x003D59FC File Offset: 0x003D59FC
    [NullableContext(2)]
    private void SetLabel(string label, bool recording = false)
    {
      this.currentLabel = label;
      if (label == null)
      {
        this.notch.DismissLiveEvent(typeof(NotchLiveEvent.Dictation));
        return;
      }
      if (recording)
      {
        this.notch.DismissLiveEvent(typeof(NotchLiveEvent.DictationCopiedToClipboard));
      }
      this.notch.ShowLiveEvent(new NotchLiveEvent.Dictation(label, recording), null);
    }

    // Token: 0x06001A0F RID: 6671 RVA: 0x003D5A5C File Offset: 0x003D5A5C
    [NullableContext(0)]
    private void OnMicrophoneFrame(ReadOnlyMemory<byte> frame)
    {
      if (!this.capturing)
      {
        return;
      }
      Interlocked.Increment(ref this.framesThisSession);
      object obj = this.frameGate;
      lock (obj)
      {
        DictationTranscriptionClient dictationTranscriptionClient = this.client;
        if (dictationTranscriptionClient != null)
        {
          dictationTranscriptionClient.AppendAudio(frame.Span);
        }
        GeminiTranscriber geminiTranscriber = this.streamer;
        if (geminiTranscriber != null)
        {
          geminiTranscriber.PushAudio(frame.Span);
        }
      }
    }

    // Token: 0x06001A10 RID: 6672 RVA: 0x003D5AE0 File Offset: 0x003D5AE0
    private static string WordsKey(IReadOnlyList<string> words)
    {
      return string.Join("\n", words);
    }

    // Token: 0x06001A11 RID: 6673 RVA: 0x003D5AF0 File Offset: 0x003D5AF0
    private GeminiTranscriber NewStreamingTranscriber(string language, IReadOnlyList<string> keyterms, string usageContext)
    {
      return new GeminiTranscriber(this.http, (bool force, CancellationToken ct) => this.auth.GetValidAccessTokenAsync(force, ct), language, keyterms, usageContext, this.microphone.SampleRate, "/v2/dictation/gemini-token");
    }

    // Token: 0x06001A12 RID: 6674 RVA: 0x003D5B1C File Offset: 0x003D5B1C
    [return: Nullable(2)]
    private GeminiTranscriber TakeWarmConnection(string language, IReadOnlyList<string> customWords, string usageContext)
    {
      DictationController.WarmConnection warmConnection = this.warm;
      if (warmConnection == null)
      {
        return null;
      }
      this.warm = null;
      warmConnection.Expiry.Dispose();
      if (warmConnection.Language == language && warmConnection.Words == DictationController.WordsKey(customWords) && warmConnection.UsageContext == usageContext && warmConnection.Transcriber.SampleRate == this.microphone.SampleRate && warmConnection.Transcriber.Adopt())
      {
        return warmConnection.Transcriber;
      }
      warmConnection.Transcriber.DisposeAsync().AsTask();
      return null;
    }

    // Token: 0x06001A13 RID: 6675 RVA: 0x003D5BB8 File Offset: 0x003D5BB8
    private void PrepareWarmConnection()
    {
      this.DiscardWarmConnection();
      if (!this.auth.IsSignedIn)
      {
        return;
      }
      AppSettings appSettings = this.settings.Current;
      string text = (string.IsNullOrWhiteSpace(appSettings.DictationLanguageCode) ? "multi" : appSettings.DictationLanguageCode.Trim());
      IReadOnlyList<string> readOnlyList = DictationVocabulary.Terms(appSettings.DictationCustomWords);
      string text2 = this.options.UsageContext();
      GeminiTranscriber geminiTranscriber = this.NewStreamingTranscriber(text, readOnlyList, text2);
      DictationController.WarmConnection connection = null;
      IDisposable disposable = new DictationController.ActorScheduler(this.actor).Schedule(DictationController.WarmConnectionWindow, delegate
      {
        if (this.warm == connection)
        {
          this.DiscardWarmConnection();
        }
      });
      connection = new DictationController.WarmConnection(geminiTranscriber, text, DictationController.WordsKey(readOnlyList), text2, disposable);
      this.warm = connection;
      geminiTranscriber.Start(true);
    }

    // Token: 0x06001A14 RID: 6676 RVA: 0x003D5C94 File Offset: 0x003D5C94
    private void DiscardWarmConnection()
    {
      DictationController.WarmConnection warmConnection = this.warm;
      if (warmConnection == null)
      {
        return;
      }
      this.warm = null;
      warmConnection.Expiry.Dispose();
      warmConnection.Transcriber.DisposeAsync().AsTask();
    }

    // Token: 0x06001A15 RID: 6677 RVA: 0x003D5CD4 File Offset: 0x003D5CD4
    private void StartCorrectionLearning(DictationFocus focus, string insertedText)
    {
      CancellationTokenSource cancellationTokenSource = this.correctionWatch;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      CancellationTokenSource cancellationTokenSource2 = (this.correctionWatch = new CancellationTokenSource());
      this.WatchForCorrectionAsync(focus, insertedText, cancellationTokenSource2.Token);
    }

    // Token: 0x06001A16 RID: 6678 RVA: 0x003D5D10 File Offset: 0x003D5D10
    private async Task WatchForCorrectionAsync(DictationFocus focus, string insertedText, CancellationToken ct)
    {
      try
      {
        await Task.Delay(DictationCorrectionDetector.BaselineDelay, ct);
        string baseline = await this.target.ReadFieldValueAsync(focus);
        if (baseline != null && baseline.Length <= 20000)
        {
          long deadline = Environment.TickCount64 + (long)DictationCorrectionDetector.ObservationWindow.TotalMilliseconds;
          string lastSeen = baseline;
          while (Environment.TickCount64 < deadline)
          {
            await Task.Delay(DictationCorrectionDetector.PollInterval, ct);
            string text = await this.target.ReadFieldValueAsync(focus);
            if (ct.IsCancellationRequested || text == null)
            {
              return;
            }
            if (!(text == lastSeen))
            {
              lastSeen = text;
              DictationLearnedCorrection dictationLearnedCorrection = DictationCorrectionDetector.DetectSingleWordCorrection(baseline, text, insertedText);
              if (dictationLearnedCorrection != null)
              {
                if (!DictationVocabulary.AddTerm(this.settings, dictationLearnedCorrection.Corrected))
                {
                  return;
                }
                string text2 = "Dictation";
                DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(19, 2);
                defaultInterpolatedStringHandler.AppendLiteral("learned \"");
                defaultInterpolatedStringHandler.AppendFormatted(dictationLearnedCorrection.Corrected);
                defaultInterpolatedStringHandler.AppendLiteral("\" (was \"");
                defaultInterpolatedStringHandler.AppendFormatted(dictationLearnedCorrection.Original);
                defaultInterpolatedStringHandler.AppendLiteral("\")");
                Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
                if (this.warm != null)
                {
                  this.PrepareWarmConnection();
                }
                this.options.PlayCue("textReceive", 0.35f);
                this.notch.ShowLiveEvent(new NotchLiveEvent.DictionaryWordLearned(dictationLearnedCorrection.Corrected), null);
                return;
              }
            }
          }
          baseline = null;
          lastSeen = null;
        }
      }
      catch (OperationCanceledException)
      {
      }
      catch (Exception ex)
      {
        Log.Warn("Dictation", "correction watch: " + ex.Message);
      }
    }

    // Token: 0x06001A17 RID: 6679 RVA: 0x003D5D6C File Offset: 0x003D5D6C
    public async ValueTask DisposeAsync()
    {
      this.microphone.Frame -= this.OnMicrophoneFrame;
      await this.warmupTimer.DisposeAsync().ConfigureAwait(false);
      TaskCompletionSource done = new TaskCompletionSource();
      this.actor.Post(delegate
      {
        if (this.isSessionActive)
        {
          this.Cancel();
        }
        this.DiscardWarmConnection();
        CancellationTokenSource cancellationTokenSource = this.correctionWatch;
        if (cancellationTokenSource != null)
        {
          cancellationTokenSource.Cancel();
        }
        done.TrySetResult();
      });
      await Task.WhenAny(done.Task, Task.Delay(1000)).ConfigureAwait(false);
      this.actor.Dispose();
    }

    // Token: 0x04000B0B RID: 2827
    private const string Area = "Dictation";

    // Token: 0x04000B0C RID: 2828
    private static readonly TimeSpan MicrophoneTail = TimeSpan.FromSeconds(0.15);

    // Token: 0x04000B0D RID: 2829
    private static readonly TimeSpan WarmConnectionWindow = TimeSpan.FromSeconds(120L);

    // Token: 0x04000B0E RID: 2830
    private static readonly TimeSpan FocusCaptureBudget = TimeSpan.FromMilliseconds(600L, 0L);

    // Token: 0x04000B0F RID: 2831
    private const string HandsFreeLabel = "Dictating (hands-free)";

    // Token: 0x04000B10 RID: 2832
    private readonly ClickyHttp http;

    // Token: 0x04000B11 RID: 2833
    private readonly SupabaseAuthService auth;

    // Token: 0x04000B12 RID: 2834
    private readonly SettingsStore settings;

    // Token: 0x04000B13 RID: 2835
    private readonly IMicrophoneCapture microphone;

    // Token: 0x04000B14 RID: 2836
    private readonly IDictationTextTarget target;

    // Token: 0x04000B15 RID: 2837
    private readonly NotchPresenter notch;

    // Token: 0x04000B16 RID: 2838
    private readonly DictationControllerOptions options;

    // Token: 0x04000B17 RID: 2839
    private readonly DictationController.ActorContext actor;

    // Token: 0x04000B18 RID: 2840
    private readonly DictationHotkeyStateMachine hotkeys;

    // Token: 0x04000B19 RID: 2841
    private readonly object frameGate = new object();

    // Token: 0x04000B1A RID: 2842
    private bool isSessionActive;

    // Token: 0x04000B1B RID: 2843
    private int sessionToken;

    // Token: 0x04000B1C RID: 2844
    [Nullable(2)]
    private DictationTranscriptionClient client;

    // Token: 0x04000B1D RID: 2845
    [Nullable(2)]
    private GeminiTranscriber streamer;

    // Token: 0x04000B1E RID: 2846
    private Task setupTask = Task.CompletedTask;

    // Token: 0x04000B1F RID: 2847
    [Nullable(2)]
    private DictationFocus capturedFocus;

    // Token: 0x04000B20 RID: 2848
    [Nullable(2)]
    private DictationWorkApp keyDownWorkAppWithoutFocus;

    // Token: 0x04000B21 RID: 2849
    [Nullable(2)]
    private string streamingPartialBeforeFailure;

    // Token: 0x04000B22 RID: 2850
    private bool usedWarmConnection;

    // Token: 0x04000B23 RID: 2851
    private bool startedOnTutorialDictationStep;

    // Token: 0x04000B24 RID: 2852
    private long sessionStartedTicks;

    // Token: 0x04000B25 RID: 2853
    private long releaseTicks;

    // Token: 0x04000B26 RID: 2854
    [Nullable(2)]
    private IDisposable tailTimer;

    // Token: 0x04000B27 RID: 2855
    [Nullable(2)]
    private string currentLabel;

    // Token: 0x04000B28 RID: 2856
    [Nullable(2)]
    private DictationController.WarmConnection warm;

    // Token: 0x04000B29 RID: 2857
    [Nullable(2)]
    private CancellationTokenSource correctionWatch;

    // Token: 0x04000B2A RID: 2858
    private volatile bool capturing;

    // Token: 0x04000B2B RID: 2859
    private volatile bool activeFlag;

    // Token: 0x04000B2C RID: 2860
    private int framesThisSession;

    // Token: 0x04000B2D RID: 2861
    private readonly Timer warmupTimer;

    // Token: 0x020005BE RID: 1470
    [Nullable(0)]
    private sealed class WarmConnection : IEquatable<DictationController.WarmConnection>
    {
      // Token: 0x060036C3 RID: 14019 RVA: 0x00444554 File Offset: 0x00444554
      public WarmConnection(GeminiTranscriber Transcriber, string Language, string Words, string UsageContext, IDisposable Expiry)
      {
        this.Transcriber = Transcriber;
        this.Language = Language;
        this.Words = Words;
        this.UsageContext = UsageContext;
        this.Expiry = Expiry;
        base..ctor();
      }

      // Token: 0x1700099D RID: 2461
      // (get) Token: 0x060036C4 RID: 14020 RVA: 0x00444584 File Offset: 0x00444584
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(DictationController.WarmConnection);
        }
      }

      // Token: 0x1700099E RID: 2462
      // (get) Token: 0x060036C5 RID: 14021 RVA: 0x00444590 File Offset: 0x00444590
      // (set) Token: 0x060036C6 RID: 14022 RVA: 0x00444598 File Offset: 0x00444598
      public GeminiTranscriber Transcriber { get; set; }

      // Token: 0x1700099F RID: 2463
      // (get) Token: 0x060036C7 RID: 14023 RVA: 0x004445A4 File Offset: 0x004445A4
      // (set) Token: 0x060036C8 RID: 14024 RVA: 0x004445AC File Offset: 0x004445AC
      public string Language { get; set; }

      // Token: 0x170009A0 RID: 2464
      // (get) Token: 0x060036C9 RID: 14025 RVA: 0x004445B8 File Offset: 0x004445B8
      // (set) Token: 0x060036CA RID: 14026 RVA: 0x004445C0 File Offset: 0x004445C0
      public string Words { get; set; }

      // Token: 0x170009A1 RID: 2465
      // (get) Token: 0x060036CB RID: 14027 RVA: 0x004445CC File Offset: 0x004445CC
      // (set) Token: 0x060036CC RID: 14028 RVA: 0x004445D4 File Offset: 0x004445D4
      public string UsageContext { get; set; }

      // Token: 0x170009A2 RID: 2466
      // (get) Token: 0x060036CD RID: 14029 RVA: 0x004445E0 File Offset: 0x004445E0
      // (set) Token: 0x060036CE RID: 14030 RVA: 0x004445E8 File Offset: 0x004445E8
      public IDisposable Expiry { get; set; }

      // Token: 0x060036CF RID: 14031 RVA: 0x004445F4 File Offset: 0x004445F4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("WarmConnection");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060036D0 RID: 14032 RVA: 0x00444640 File Offset: 0x00444640
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Transcriber = ");
        builder.Append(this.Transcriber);
        builder.Append(", Language = ");
        builder.Append(this.Language);
        builder.Append(", Words = ");
        builder.Append(this.Words);
        builder.Append(", UsageContext = ");
        builder.Append(this.UsageContext);
        builder.Append(", Expiry = ");
        builder.Append(this.Expiry);
        return true;
      }

      // Token: 0x060036D1 RID: 14033 RVA: 0x004446D0 File Offset: 0x004446D0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(DictationController.WarmConnection left, DictationController.WarmConnection right)
      {
        return !(left == right);
      }

      // Token: 0x060036D2 RID: 14034 RVA: 0x004446DC File Offset: 0x004446DC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(DictationController.WarmConnection left, DictationController.WarmConnection right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060036D3 RID: 14035 RVA: 0x004446F0 File Offset: 0x004446F0
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<GeminiTranscriber>.Default.GetHashCode(this.<Transcriber>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Language>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Words>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UsageContext>k__BackingField)) * -1521134295 + EqualityComparer<IDisposable>.Default.GetHashCode(this.<Expiry>k__BackingField);
      }

      // Token: 0x060036D4 RID: 14036 RVA: 0x00444780 File Offset: 0x00444780
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as DictationController.WarmConnection);
      }

      // Token: 0x060036D5 RID: 14037 RVA: 0x00444790 File Offset: 0x00444790
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(DictationController.WarmConnection other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<GeminiTranscriber>.Default.Equals(this.<Transcriber>k__BackingField, other.<Transcriber>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Language>k__BackingField, other.<Language>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Words>k__BackingField, other.<Words>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UsageContext>k__BackingField, other.<UsageContext>k__BackingField) && EqualityComparer<IDisposable>.Default.Equals(this.<Expiry>k__BackingField, other.<Expiry>k__BackingField));
      }

      // Token: 0x060036D7 RID: 14039 RVA: 0x00444840 File Offset: 0x00444840
      [CompilerGenerated]
      private WarmConnection(DictationController.WarmConnection original)
      {
        this.Transcriber = original.<Transcriber>k__BackingField;
        this.Language = original.<Language>k__BackingField;
        this.Words = original.<Words>k__BackingField;
        this.UsageContext = original.<UsageContext>k__BackingField;
        this.Expiry = original.<Expiry>k__BackingField;
      }

      // Token: 0x060036D8 RID: 14040 RVA: 0x00444890 File Offset: 0x00444890
      [CompilerGenerated]
      public void Deconstruct(out GeminiTranscriber Transcriber, out string Language, out string Words, out string UsageContext, out IDisposable Expiry)
      {
        Transcriber = this.Transcriber;
        Language = this.Language;
        Words = this.Words;
        UsageContext = this.UsageContext;
        Expiry = this.Expiry;
      }
    }

    // Token: 0x020005BF RID: 1471
    [Nullable(0)]
    private sealed class ActorContext : SynchronizationContext, IDisposable
    {
      // Token: 0x060036D9 RID: 14041 RVA: 0x004448BC File Offset: 0x004448BC
      public ActorContext(string name)
      {
        this.thread = new Thread(new ThreadStart(this.Run))
        {
          IsBackground = true,
          Name = name
        };
        this.thread.Start();
      }

      // Token: 0x060036DA RID: 14042 RVA: 0x0044490C File Offset: 0x0044490C
      private void Run()
      {
        SynchronizationContext.SetSynchronizationContext(this);
        foreach (ValueTuple<SendOrPostCallback, object> valueTuple in this.queue.GetConsumingEnumerable())
        {
          SendOrPostCallback item = valueTuple.Item1;
          object item2 = valueTuple.Item2;
          try
          {
            item(item2);
          }
          catch (Exception ex)
          {
            Log.Error("Dictation", "dictation work item threw", ex);
          }
        }
      }

      // Token: 0x060036DB RID: 14043 RVA: 0x00444994 File Offset: 0x00444994
      public override void Post(SendOrPostCallback d, [Nullable(2)] object state)
      {
        try
        {
          this.queue.Add(new ValueTuple<SendOrPostCallback, object>(d, state));
        }
        catch (InvalidOperationException)
        {
        }
      }

      // Token: 0x060036DC RID: 14044 RVA: 0x004449C8 File Offset: 0x004449C8
      public void Post(Action action)
      {
        this.Post(delegate(object _)
        {
          action();
        }, null);
      }

      // Token: 0x060036DD RID: 14045 RVA: 0x004449F8 File Offset: 0x004449F8
      public override void Send(SendOrPostCallback d, [Nullable(2)] object state)
      {
        throw new NotSupportedException();
      }

      // Token: 0x060036DE RID: 14046 RVA: 0x00444A00 File Offset: 0x00444A00
      public void Dispose()
      {
        this.queue.CompleteAdding();
      }

      // Token: 0x0400217C RID: 8572
      [TupleElementNames(new string[] { "Callback", "State" })]
      [Nullable(new byte[] { 1, 0, 1, 2 })]
      private readonly BlockingCollection<ValueTuple<SendOrPostCallback, object>> queue = new BlockingCollection<ValueTuple<SendOrPostCallback, object>>();

      // Token: 0x0400217D RID: 8573
      private readonly Thread thread;
    }

    // Token: 0x020005C0 RID: 1472
    [NullableContext(0)]
    private sealed class ActorScheduler : IDictationScheduler
    {
      // Token: 0x060036DF RID: 14047 RVA: 0x00444A10 File Offset: 0x00444A10
      [NullableContext(1)]
      public ActorScheduler(DictationController.ActorContext actor)
      {
      }

      // Token: 0x170009A3 RID: 2467
      // (get) Token: 0x060036E0 RID: 14048 RVA: 0x00444A20 File Offset: 0x00444A20
      public DateTimeOffset Now
      {
        get
        {
          return DateTimeOffset.UtcNow;
        }
      }

      // Token: 0x060036E1 RID: 14049 RVA: 0x00444A28 File Offset: 0x00444A28
      [NullableContext(1)]
      public IDisposable Schedule(TimeSpan delay, Action action)
      {
        DictationController.ActorScheduler.Handle handle = new DictationController.ActorScheduler.Handle();
        Action <>9__1;
        handle.Timer = new Timer(delegate(object _)
        {
          if (handle.Cancelled)
          {
            return;
          }
          DictationController.ActorContext actorContext = this.<actor>P;
          Action action2;
          if ((action2 = <>9__1) == null)
          {
            action2 = (<>9__1 = delegate
            {
              if (!handle.Cancelled)
              {
                action();
              }
            });
          }
          actorContext.Post(action2);
        }, null, delay, Timeout.InfiniteTimeSpan);
        return handle;
      }

      // Token: 0x0400217E RID: 8574
      [Nullable(1)]
      [CompilerGenerated]
      private DictationController.ActorContext <actor>P = actor;

      // Token: 0x02000711 RID: 1809
      private sealed class Handle : IDisposable
      {
        // Token: 0x06003CC9 RID: 15561 RVA: 0x00467B0C File Offset: 0x00467B0C
        public void Dispose()
        {
          this.Cancelled = true;
          Timer timer = this.Timer;
          if (timer == null)
          {
            return;
          }
          timer.Dispose();
        }

        // Token: 0x0400269A RID: 9882
        [Nullable(2)]
        public Timer Timer;

        // Token: 0x0400269B RID: 9883
        public volatile bool Cancelled;
      }
    }
  }
}
