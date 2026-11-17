using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Auth;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Brain;
using HeyClicky.Core.Drive;
using HeyClicky.Core.Identity;
using HeyClicky.Core.Net;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Platform;
using HeyClicky.Core.Proactive;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Tools;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200006B RID: 107
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CompanionSession : ICompanionSession, IAsyncDisposable
  {
    // Token: 0x0600057F RID: 1407 RVA: 0x00358D78 File Offset: 0x00358D78
    public Task AskWithAppFactAsync(string fact, string trigger)
    {
      CompanionSession.<AskWithAppFactAsync>d__0 <AskWithAppFactAsync>d__;
      <AskWithAppFactAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
      <AskWithAppFactAsync>d__.<>4__this = this;
      <AskWithAppFactAsync>d__.fact = fact;
      <AskWithAppFactAsync>d__.trigger = trigger;
      <AskWithAppFactAsync>d__.<>1__state = -1;
      <AskWithAppFactAsync>d__.<>t__builder.Start<CompanionSession.<AskWithAppFactAsync>d__0>(ref <AskWithAppFactAsync>d__);
      return <AskWithAppFactAsync>d__.<>t__builder.Task;
    }

    // Token: 0x14000002 RID: 2
    // (add) Token: 0x06000580 RID: 1408 RVA: 0x00358DCC File Offset: 0x00358DCC
    // (remove) Token: 0x06000581 RID: 1409 RVA: 0x00358E04 File Offset: 0x00358E04
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler StateChanged;

    // Token: 0x14000003 RID: 3
    // (add) Token: 0x06000582 RID: 1410 RVA: 0x00358E3C File Offset: 0x00358E3C
    // (remove) Token: 0x06000583 RID: 1411 RVA: 0x00358E74 File Offset: 0x00358E74
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<string> UserTranscriptUpdated;

    // Token: 0x14000004 RID: 4
    // (add) Token: 0x06000584 RID: 1412 RVA: 0x00358EAC File Offset: 0x00358EAC
    // (remove) Token: 0x06000585 RID: 1413 RVA: 0x00358EE4 File Offset: 0x00358EE4
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<string> ResponseTextUpdated;

    // Token: 0x14000005 RID: 5
    // (add) Token: 0x06000586 RID: 1414 RVA: 0x00358F1C File Offset: 0x00358F1C
    // (remove) Token: 0x06000587 RID: 1415 RVA: 0x00358F54 File Offset: 0x00358F54
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<CompanionError> Failed;

    // Token: 0x14000006 RID: 6
    // (add) Token: 0x06000588 RID: 1416 RVA: 0x00358F8C File Offset: 0x00358F8C
    // (remove) Token: 0x06000589 RID: 1417 RVA: 0x00358FC4 File Offset: 0x00358FC4
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<DrawingAction> UnhandledDrawingAction;

    // Token: 0x14000007 RID: 7
    // (add) Token: 0x0600058A RID: 1418 RVA: 0x00358FFC File Offset: 0x00358FFC
    // (remove) Token: 0x0600058B RID: 1419 RVA: 0x00359034 File Offset: 0x00359034
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<CompanionTurnMetrics> TurnCompleted;

    // Token: 0x14000008 RID: 8
    // (add) Token: 0x0600058C RID: 1420 RVA: 0x0035906C File Offset: 0x0035906C
    // (remove) Token: 0x0600058D RID: 1421 RVA: 0x003590A4 File Offset: 0x003590A4
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<CompanionTurnFinish> TurnFinished;

    // Token: 0x14000009 RID: 9
    // (add) Token: 0x0600058E RID: 1422 RVA: 0x003590DC File Offset: 0x003590DC
    // (remove) Token: 0x0600058F RID: 1423 RVA: 0x00359114 File Offset: 0x00359114
    [TupleElementNames(new string[] { "CharacterCount", "Typed", "Trigger" })]
    [Nullable(new byte[] { 2, 0, 1 })]
    [field: TupleElementNames(new string[] { "CharacterCount", "Typed", "Trigger" })]
    [field: Nullable(new byte[] { 2, 0, 1 })]
    public event EventHandler<ValueTuple<int, bool, string>> MessageSent;

    // Token: 0x1400000A RID: 10
    // (add) Token: 0x06000590 RID: 1424 RVA: 0x0035914C File Offset: 0x0035914C
    // (remove) Token: 0x06000591 RID: 1425 RVA: 0x00359184 File Offset: 0x00359184
    [Nullable(new byte[] { 2, 1, 2 })]
    [field: Nullable(new byte[] { 2, 1, 2 })]
    public event Action<string, string, bool> TextDelivered;

    // Token: 0x06000592 RID: 1426 RVA: 0x003591BC File Offset: 0x003591BC
    private void RaiseTextDelivered(string text, [Nullable(2)] string app, bool wasCopied)
    {
      try
      {
        Action<string, string, bool> textDelivered = this.TextDelivered;
        if (textDelivered != null)
        {
          textDelivered(text, app, wasCopied);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Companion", "TextDelivered handler threw: " + ex.Message);
      }
    }

    // Token: 0x1700013D RID: 317
    // (get) Token: 0x06000593 RID: 1427 RVA: 0x0035920C File Offset: 0x0035920C
    public bool LastTurnWasTyped
    {
      get
      {
        return this.lastTurnWasTyped;
      }
    }

    // Token: 0x06000594 RID: 1428 RVA: 0x00359218 File Offset: 0x00359218
    public CompanionSession(ClickyHttp http, SupabaseAuthService auth, BrainClient brainClient, BrainSession brain, SettingsStore settings, IMicrophoneCapture microphone, IAudioOutput audio, IScreenCaptureService screens, IScreenDrawingCanvas canvas, ICursorOverlay cursor, [Nullable(2)] IHighlightOverlay highlight, INotchPresenter notch, [Nullable(2)] CompanionSessionOptions options = null)
    {
      CompanionSession.<>c__DisplayClass74_0 CS$<>8__locals1 = new CompanionSession.<>c__DisplayClass74_0();
      CS$<>8__locals1.auth = auth;
      CS$<>8__locals1.settings = settings;
      base..ctor();
      CS$<>8__locals1.<>4__this = this;
      this.http = http;
      this.auth = CS$<>8__locals1.auth;
      this.brainClient = brainClient;
      this.brain = brain;
      this.settings = CS$<>8__locals1.settings;
      this.microphone = microphone;
      this.audio = audio;
      this.screens = screens;
      this.canvas = canvas;
      this.cursor = cursor;
      this.highlight = highlight;
      this.notch = notch;
      this.options = options ?? new CompanionSessionOptions();
      this.ui = new UiInvoker(this.options.UiContext);
      IPageReader pageReader = this.options.PageReader;
      this.pageReads = ((pageReader != null) ? new PageReads(pageReader, null) : null);
      this.mouth = new RealtimeMouth(http, (bool force, CancellationToken ct) => CS$<>8__locals1.auth.GetValidAccessTokenAsync(force, ct), audio, delegate
      {
        AppSettings appSettings = CS$<>8__locals1.settings.Current;
        return new MouthConfig(appSettings.VoiceIdentifier, appSettings.SpeechSpeed, CS$<>8__locals1.<>4__this.options.UsageContext(), appSettings.VoiceLanguageCode);
      });
      ValueTuple<string, double, string> mouthPick = CS$<>8__locals1.settings.Read<ValueTuple<string, double, string>>(([Nullable(1)] AppSettings s) => new ValueTuple<string, double, string>(s.VoiceIdentifier, s.SpeechSpeed, s.VoiceLanguageCode));
      ClickyAnalytics.TrackVoiceSettings(mouthPick.Item1, mouthPick.Item3, mouthPick.Item2);
      CS$<>8__locals1.settings.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        ValueTuple<string, double, string> valueTuple = CS$<>8__locals1.settings.Read<ValueTuple<string, double, string>>(([Nullable(1)] AppSettings s) => new ValueTuple<string, double, string>(s.VoiceIdentifier, s.SpeechSpeed, s.VoiceLanguageCode));
        ValueTuple<string, double, string> valueTuple2 = valueTuple;
        ValueTuple<string, double, string> mouthPick3 = mouthPick;
        if (valueTuple2.Item1 == mouthPick3.Item1 && valueTuple2.Item2 == mouthPick3.Item2 && valueTuple2.Item3 == mouthPick3.Item3)
        {
          return;
        }
        ValueTuple<string, double, string> mouthPick2 = mouthPick;
        mouthPick = valueTuple;
        ClickyAnalytics.TrackVoiceSettings(valueTuple.Item1, valueTuple.Item3, valueTuple.Item2);
        bool flag = valueTuple.Item1 != mouthPick2.Item1;
        bool flag2 = VoiceCatalog.IsGemini(valueTuple.Item1);
        if (!flag && flag2 && valueTuple.Item2 != mouthPick2.Item2 && CS$<>8__locals1.<>4__this.mouth.IsGeminiSession)
        {
          CS$<>8__locals1.<>4__this.mouth.SetPlaybackRate(valueTuple.Item2);
        }
        bool flag3;
        if (flag)
        {
          flag3 = true;
        }
        else if (flag2)
        {
          flag3 = valueTuple.Item3 != mouthPick2.Item3 || (valueTuple.Item2 != mouthPick2.Item2 && !CS$<>8__locals1.<>4__this.mouth.IsGeminiSession);
        }
        else
        {
          flag3 = valueTuple.Item2 != mouthPick2.Item2;
        }
        if (flag3 && CS$<>8__locals1.auth.IsSignedIn)
        {
          CS$<>8__locals1.<>4__this.mouth.RefreshAfterConfigChangeAsync(default(CancellationToken));
        }
      };
      this.mouth.LineAudioQueued += this.OnLineAudioQueued;
      this.mouth.LineGenerated += this.OnLineGenerated;
      this.mouth.IsAtRest = () => CS$<>8__locals1.<>4__this.State == VoiceState.Idle && Volatile.Read(ref CS$<>8__locals1.<>4__this.activeTurnCount) == 0 && !CS$<>8__locals1.<>4__this.IsListeningNow;
      this.mouth.KeepWarm = delegate
      {
        Func<bool> keepMouthWarm = CS$<>8__locals1.<>4__this.options.KeepMouthWarm;
        return keepMouthWarm != null && keepMouthWarm();
      };
      microphone.Frame += this.OnMicrophoneFrame;
      microphone.DeviceProvedDead += this.OnMicrophoneDeviceProvedDead;
      canvas.ActionRequested += this.OnCanvasActionRequested;
      canvas.UserActedOnScreen += this.OnCanvasUserActedOnScreen;
      CS$<>8__locals1.auth.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        if (!CS$<>8__locals1.auth.IsSignedIn)
        {
          CS$<>8__locals1.<>4__this.DiscardWarmEarsAsync();
        }
      };
    }

    // Token: 0x1700013E RID: 318
    // (get) Token: 0x06000595 RID: 1429 RVA: 0x003594EC File Offset: 0x003594EC
    public VoiceState State
    {
      get
      {
        object obj = this.gate;
        VoiceState voiceState;
        lock (obj)
        {
          voiceState = this.state;
        }
        return voiceState;
      }
    }

    // Token: 0x1700013F RID: 319
    // (get) Token: 0x06000596 RID: 1430 RVA: 0x00359530 File Offset: 0x00359530
    private bool IsListeningNow
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.isListening;
        }
        return flag2;
      }
    }

    // Token: 0x17000140 RID: 320
    // (get) Token: 0x06000597 RID: 1431 RVA: 0x00359574 File Offset: 0x00359574
    public string ResponseText
    {
      get
      {
        object obj = this.gate;
        string text;
        lock (obj)
        {
          text = this.responseText;
        }
        return text;
      }
    }

    // Token: 0x17000141 RID: 321
    // (get) Token: 0x06000598 RID: 1432 RVA: 0x003595B8 File Offset: 0x003595B8
    public RealtimeMouth Mouth
    {
      get
      {
        return this.mouth;
      }
    }

    // Token: 0x06000599 RID: 1433 RVA: 0x003595C0 File Offset: 0x003595C0
    public RealtimeMouth CreateSilentMouth()
    {
      return new RealtimeMouth(this.http, (bool force, CancellationToken ct) => this.auth.GetValidAccessTokenAsync(force, ct), new CompanionSession.SilentAudioOutput(this.audio.SampleRate), delegate
      {
        AppSettings appSettings = this.settings.Current;
        return new MouthConfig(appSettings.VoiceIdentifier, appSettings.SpeechSpeed, this.options.UsageContext(), appSettings.VoiceLanguageCode);
      });
    }

    // Token: 0x17000142 RID: 322
    // (get) Token: 0x0600059A RID: 1434 RVA: 0x003595F8 File Offset: 0x003595F8
    [Nullable(2)]
    public IPageReader PageReaderForTest
    {
      [NullableContext(2)]
      get
      {
        return this.options.PageReader;
      }
    }

    // Token: 0x0600059B RID: 1435 RVA: 0x00359608 File Offset: 0x00359608
    public void Prewarm()
    {
      this.http.PrewarmAll();
      if (this.auth.IsSignedIn)
      {
        this.mouth.EnsureOpenAsync(default(CancellationToken));
      }
    }

    // Token: 0x17000143 RID: 323
    // (get) Token: 0x0600059C RID: 1436 RVA: 0x00359644 File Offset: 0x00359644
    private bool UsesOpenAIBrain
    {
      get
      {
        string text = this.settings.Read<string>(([Nullable(1)] AppSettings s) => s.OneBrainModel);
        return text == null || text.Length <= 0 || text.StartsWith("gpt-", StringComparison.Ordinal);
      }
    }

    // Token: 0x17000144 RID: 324
    // (get) Token: 0x0600059D RID: 1437 RVA: 0x00359698 File Offset: 0x00359698
    private bool DriveOfferedForRequest
    {
      get
      {
        IClickyDriveTurns drive = this.options.Drive;
        return drive != null && drive.OffersDrive && this.UsesOpenAIBrain;
      }
    }

    // Token: 0x0600059E RID: 1438 RVA: 0x003596BC File Offset: 0x003596BC
    private BrainRequestFlags RequestFlags(bool offersDrive)
    {
      BrainRequestFlags brainRequestFlags = new BrainRequestFlags(CompanionSession.SendsLibraryItems, this.settings.Read<bool>((AppSettings s) => s.SaveDrawingsEffective), offersDrive);
      brainRequestFlags.FiguresFloat = this.settings.Read<bool>((AppSettings s) => s.StreamMode);
      return brainRequestFlags;
    }

    // Token: 0x17000145 RID: 325
    // (get) Token: 0x0600059F RID: 1439 RVA: 0x00359730 File Offset: 0x00359730
    public int ActiveTurnCount
    {
      get
      {
        return Volatile.Read(ref this.activeTurnCount);
      }
    }

    // Token: 0x17000146 RID: 326
    // (get) Token: 0x060005A0 RID: 1440 RVA: 0x00359740 File Offset: 0x00359740
    public bool IsListening
    {
      get
      {
        return this.IsListeningNow;
      }
    }

    // Token: 0x17000147 RID: 327
    // (get) Token: 0x060005A1 RID: 1441 RVA: 0x00359748 File Offset: 0x00359748
    public bool InFlightTurnIsOnboarding
    {
      get
      {
        return Volatile.Read(ref this.inFlightTurnIsOnboarding);
      }
    }

    // Token: 0x17000148 RID: 328
    // (get) Token: 0x060005A2 RID: 1442 RVA: 0x00359758 File Offset: 0x00359758
    public bool IsOnboardingAnswerInFlight
    {
      get
      {
        bool flag = this.InFlightTurnIsOnboarding;
        if (flag)
        {
          VoiceState voiceState = this.State;
          bool flag2 = voiceState - VoiceState.Processing <= 1;
          flag = flag2;
        }
        return flag;
      }
    }

    // Token: 0x17000149 RID: 329
    // (get) Token: 0x060005A3 RID: 1443 RVA: 0x00359788 File Offset: 0x00359788
    public string CurrentTurnProgramSoFar
    {
      get
      {
        return Volatile.Read<string>(ref this.currentTurnProgramSoFar);
      }
    }

    // Token: 0x060005A4 RID: 1444 RVA: 0x00359798 File Offset: 0x00359798
    public void StartFreshSession()
    {
      this.brain.StartFreshSession(this.auth.UserId);
      this.DropActionsWaitingForTheDrawing();
    }

    // Token: 0x060005A5 RID: 1445 RVA: 0x003597B8 File Offset: 0x003597B8
    public Task BeginPushToTalkAsync()
    {
      IClickyDriveTurns drive = this.options.Drive;
      if (drive != null)
      {
        drive.StopBecauseUserSpoke();
      }
      this.InterruptForNewInput(false);
      this.http.PrewarmWorker();
      this.http.PrewarmGemini();
      if (!this.auth.IsSignedIn)
      {
        this.RaiseFailed(new CompanionError("not_signed_in", "Sign in to talk to HeyClicky.", null));
        return Task.CompletedTask;
      }
      AppSettings current = this.settings.Current;
      string text = this.options.UsageContext();
      object obj;
      if (this.RefusedOverMessageCap(text))
      {
        obj = this.gate;
        lock (obj)
        {
          this.pressWasRefused = true;
        }
        this.NotifyTerminating();
        this.SetState(VoiceState.Idle);
        return Task.CompletedTask;
      }
      string text2 = EarsLanguage.Code();
      List<string> list = CompanionSession.NormalizedKeyterms(current.DictationCustomWords);
      GeminiTranscriber geminiTranscriber = this.TakeWarmEars(text2, list, text);
      GeminiTranscriber geminiTranscriber2;
      if (geminiTranscriber != null && geminiTranscriber.SampleRate == this.microphone.SampleRate && geminiTranscriber.Adopt())
      {
        geminiTranscriber2 = geminiTranscriber;
        Log.Info("Companion", "ears: on the warm socket");
      }
      else
      {
        if (geminiTranscriber != null)
        {
          geminiTranscriber.DisposeAsync().AsTask();
        }
        geminiTranscriber2 = new GeminiTranscriber(this.http, (bool force, CancellationToken ct) => this.auth.GetValidAccessTokenAsync(force, ct), text2, list, text, this.microphone.SampleRate, "/brain/ears/token");
        geminiTranscriber2.Start(false);
      }
      obj = this.gate;
      lock (obj)
      {
        this.ears = geminiTranscriber2;
        this.pressStartedTicks = Environment.TickCount64;
        this.pressAudioLength = 0;
        this.pressFrameCount = 0;
        this.pressWasRefused = false;
        this.isListening = true;
      }
      this.EndBluetoothHold(false);
      try
      {
        this.microphone.Start();
      }
      catch (Exception ex)
      {
        Log.Error("Companion", "microphone did not start", ex);
        this.ShowLiveEvent(CompanionSession.CannotHearMicrophone(this.microphone.DeviceName));
        this.CancelListening();
        return Task.CompletedTask;
      }
      IHighlightOverlay highlightOverlay = this.highlight;
      if (highlightOverlay != null && highlightOverlay.IsEnabled && current.ScreenHighlightEnabled)
      {
        this.ui.Post(delegate
        {
          this.highlight.BeginWatch(current.CursorColorHex);
        });
      }
      PageReads pageReads = this.pageReads;
      if (pageReads != null)
      {
        pageReads.BeginRead(true);
      }
      this.SetState(VoiceState.Listening);
      this.StartLiveTranscriptLoop(geminiTranscriber2);
      this.WatchForEarsPaywall(geminiTranscriber2);
      Task.Run(delegate
      {
        this.brain.AdoptAccount(this.auth.UserId);
        this.brain.NoteKeyDown(this.UsesOpenAIBrain, this.CursorScreenCanvas(), this.RequestFlags(this.DriveOfferedForRequest));
        this.http.PrewarmOpenAI();
        this.mouth.EnsureOpenAsync(default(CancellationToken));
      });
      return Task.CompletedTask;
    }

    // Token: 0x060005A6 RID: 1446 RVA: 0x00359A80 File Offset: 0x00359A80
    private void WatchForEarsPaywall(GeminiTranscriber transcriber)
    {
      transcriber.Opened.ContinueWith(delegate(Task<bool> _)
      {
        if (!transcriber.PaywallReached)
        {
          return;
        }
        object obj = this.gate;
        lock (obj)
        {
          if (this.ears != transcriber)
          {
            return;
          }
          this.pressWasRefused = true;
        }
        this.CancelListening();
        this.NotifyTerminating();
        this.SetState(VoiceState.Idle);
        this.RaiseFailed(new CompanionError("paywall", "You've reached your message limit.", null));
      }, TaskScheduler.Default);
    }

    // Token: 0x060005A7 RID: 1447 RVA: 0x00359AC4 File Offset: 0x00359AC4
    public Task EndPushToTalkAsync()
    {
      CompanionSession.<EndPushToTalkAsync>d__108 <EndPushToTalkAsync>d__;
      <EndPushToTalkAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
      <EndPushToTalkAsync>d__.<>4__this = this;
      <EndPushToTalkAsync>d__.<>1__state = -1;
      <EndPushToTalkAsync>d__.<>t__builder.Start<CompanionSession.<EndPushToTalkAsync>d__108>(ref <EndPushToTalkAsync>d__);
      return <EndPushToTalkAsync>d__.<>t__builder.Task;
    }

    // Token: 0x060005A8 RID: 1448 RVA: 0x00359B08 File Offset: 0x00359B08
    [NullableContext(2)]
    private string WithoutStrayScript(string words)
    {
      if (string.IsNullOrEmpty(words))
      {
        return words;
      }
      IReadOnlyList<IReadOnlyList<string>> readOnlyList = EarsLanguage.StoredHeardScripts();
      if (readOnlyList.Count < 10)
      {
        return words;
      }
      IReadOnlyList<string> readOnlyList2 = Array.Empty<string>();
      try
      {
        Func<IReadOnlyList<string>> systemLanguageTags = this.options.SystemLanguageTags;
        readOnlyList2 = ((systemLanguageTags != null) ? systemLanguageTags() : null) ?? Array.Empty<string>();
      }
      catch (Exception ex)
      {
        Log.Warn("Companion", "system languages: " + ex.Message);
      }
      ValueTuple<string, string> valueTuple = this.settings.Read<ValueTuple<string, string>>(([Nullable(1)] AppSettings s) => new ValueTuple<string, string>(s.VoiceLanguageCode, s.DictationLanguageCode));
      string item = valueTuple.Item1;
      string item2 = valueTuple.Item2;
      IReadOnlySet<EarsLanguage.Script> readOnlySet = EarsLanguage.AcceptedScripts(VoiceCatalog.PickedLanguages(item), item2, readOnlyList2, readOnlyList);
      string text = EarsLanguage.GuardingNoise(words, readOnlySet, null) ?? "";
      if (text == words)
      {
        return words;
      }
      Log.Info("Companion", "ears: dropped words in a script the user doesn't use (" + ((text.Length == 0) ? "the whole press" : "the edges") + ")");
      string text2 = "one_brain_stray_script_dropped";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["whole_press"] = text.Length == 0;
      jsonObject["word_count"] = words.Split(null, StringSplitOptions.RemoveEmptyEntries).Length;
      ClickyAnalytics.Capture(text2, jsonObject, null, null);
      return text;
    }

    // Token: 0x060005A9 RID: 1449 RVA: 0x00359C78 File Offset: 0x00359C78
    private static async Task<bool> TakesWordsDirectlyAsync(Func<string, Task<bool>> takeDirectly, string words)
    {
      bool flag;
      try
      {
        flag = await takeDirectly(words).ConfigureAwait(false);
      }
      catch (Exception ex)
      {
        Log.Warn("Companion", "passing the words straight on failed: " + ex.Message);
        flag = false;
      }
      return flag;
    }

    // Token: 0x1700014A RID: 330
    // (get) Token: 0x060005AA RID: 1450 RVA: 0x00359CC4 File Offset: 0x00359CC4
    // (set) Token: 0x060005AB RID: 1451 RVA: 0x00359CCC File Offset: 0x00359CCC
    [Nullable(2)]
    public string LastHighlightText
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x060005AC RID: 1452 RVA: 0x00359CD8 File Offset: 0x00359CD8
    private async Task<CompanionSession.TurnCapture> CaptureForTurnAsync(bool endHighlight)
    {
      long started = Environment.TickCount64;
      IReadOnlyList<HighlightStroke> strokes = Array.Empty<HighlightStroke>();
      if (endHighlight && this.highlight != null)
      {
        try
        {
          IReadOnlyList<HighlightStroke> readOnlyList = await this.ui.InvokeAsync<IReadOnlyList<HighlightStroke>>(() => this.highlight.EndWatch()).ConfigureAwait(false);
          strokes = readOnlyList;
        }
        catch (Exception ex)
        {
          Log.Warn("Companion", "highlight: " + ex.Message);
        }
      }
      Task<string> highlightTextTask = ((strokes.Count > 0) ? this.HarvestHighlightTextAsync(strokes) : Task.FromResult<string>(null));
      IReadOnlyList<ScreenCaptureFrame> frames;
      IReadOnlyList<DisplayInfo> displays;
      try
      {
        frames = await this.screens.CaptureAllAsync(1280, 80, default(CancellationToken)).ConfigureAwait(false);
        displays = this.screens.GetDisplays();
      }
      catch (Exception ex2)
      {
        Log.Error("Companion", "screen capture failed", ex2);
        return new CompanionSession.TurnCapture(Array.Empty<ScreenCaptureFrame>(), Array.Empty<DisplayInfo>(), null, (int)(Environment.TickCount64 - started), null);
      }
      string gesture = null;
      IReadOnlyList<ScreenCaptureFrame> framesWithoutInk = null;
      if (strokes.Count > 0)
      {
        gesture = HighlightCompositor.GestureSummary(strokes, displays);
        string color = this.settings.Read<string>(([Nullable(1)] AppSettings s) => s.CursorColorHex);
        List<ScreenCaptureFrame> painted = new List<ScreenCaptureFrame>(frames.Count);
        foreach (ScreenCaptureFrame screenCaptureFrame in frames)
        {
          List<ScreenCaptureFrame> list = painted;
          list.Add(await HighlightCompositor.CompositeAsync(screenCaptureFrame, strokes, color).ConfigureAwait(false));
          list = null;
        }
        IEnumerator<ScreenCaptureFrame> enumerator = null;
        framesWithoutInk = frames;
        frames = painted;
        color = null;
        painted = null;
      }
      string text = ((gesture != null) ? (await highlightTextTask.ConfigureAwait(false)) : null);
      if (gesture != null)
      {
        this.LastHighlightText = text ?? "";
      }
      return new CompanionSession.TurnCapture(frames, displays, gesture, (int)(Environment.TickCount64 - started), text)
      {
        FramesWithoutInk = framesWithoutInk
      };
    }

    // Token: 0x060005AD RID: 1453 RVA: 0x00359D24 File Offset: 0x00359D24
    [return: Nullable(new byte[] { 1, 2 })]
    private async Task<string> HarvestHighlightTextAsync(IReadOnlyList<HighlightStroke> strokes)
    {
      Func<IReadOnlyList<HighlightStroke>, CancellationToken, Task<string>> highlightText = this.options.HighlightText;
      string text;
      if (highlightText == null)
      {
        text = null;
      }
      else
      {
        long started = Environment.TickCount64;
        using (CancellationTokenSource budget = new CancellationTokenSource(CompanionSession.HighlightTextBudget))
        {
          try
          {
            string text2 = await highlightText(strokes, budget.Token).WaitAsync(CompanionSession.HighlightTextBudget, budget.Token).ConfigureAwait(false);
            string text3 = "Companion";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(22, 2);
            defaultInterpolatedStringHandler.AppendLiteral("highlight text: ");
            string text4;
            if (text2 != null)
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(6, 1);
              defaultInterpolatedStringHandler2.AppendFormatted<int>(text2.Length);
              defaultInterpolatedStringHandler2.AppendLiteral(" chars");
              text4 = defaultInterpolatedStringHandler2.ToStringAndClear();
            }
            else
            {
              text4 = "nothing readable";
            }
            defaultInterpolatedStringHandler.AppendFormatted(text4);
            defaultInterpolatedStringHandler.AppendLiteral(" (");
            defaultInterpolatedStringHandler.AppendFormatted<long>(Environment.TickCount64 - started);
            defaultInterpolatedStringHandler.AppendLiteral(" ms)");
            Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
            text = (string.IsNullOrWhiteSpace(text2) ? null : text2.Trim());
          }
          catch (Exception ex) when (ex is OperationCanceledException || ex is TimeoutException)
          {
            string text5 = "Companion";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 1);
            defaultInterpolatedStringHandler.AppendLiteral("highlight text: no answer within ");
            defaultInterpolatedStringHandler.AppendFormatted<double>(CompanionSession.HighlightTextBudget.TotalMilliseconds, "0");
            defaultInterpolatedStringHandler.AppendLiteral(" ms");
            Log.Info(text5, defaultInterpolatedStringHandler.ToStringAndClear());
            text = null;
          }
          catch (Exception ex2)
          {
            Log.Warn("Companion", "highlight text: " + ex2.Message);
            text = null;
          }
        }
      }
      return text;
    }

    // Token: 0x060005AE RID: 1454 RVA: 0x00359D70 File Offset: 0x00359D70
    [return: Nullable(new byte[] { 1, 2 })]
    private async Task<string> TranscribeFallbackAsync(byte[] pcm16)
    {
      string text = await this.auth.GetValidAccessTokenAsync(default(CancellationToken)).ConfigureAwait(false);
      string text2;
      if (text == null)
      {
        text2 = null;
      }
      else
      {
        byte[] wav = Pcm16.WavFromPcm16Mono(pcm16, this.microphone.SampleRate);
        string language = this.settings.Read<string>(([Nullable(1)] AppSettings s) => s.DictationLanguageCode);
        ValueTuple<int, JsonObject>? valueTuple = await this.brainClient.PostJsonAsync("/brain/ears/transcribe", delegate(Utf8JsonWriter writer)
        {
          writer.WriteBase64String("audio_wav_base64", wav);
          writer.WriteString("language", language);
        }, text, new TimeSpan?(TimeSpan.FromSeconds(20L)), this.options.UsageContext(), default(CancellationToken)).ConfigureAwait(false);
        if (valueTuple != null)
        {
          ValueTuple<int, JsonObject> valueOrDefault = valueTuple.GetValueOrDefault();
          if (valueOrDefault.Item1 == 402)
          {
            BillingPaywallDetector.HandleIfPaywall(402, JsonSerializer.SerializeToUtf8Bytes<JsonObject>(valueOrDefault.Item2, JsonNodeContext.Default.JsonObject), true);
            text2 = null;
          }
          else
          {
            string text3 = (string)valueOrDefault.Item2["text"];
            string text4 = ((text3 != null) ? text3.Trim() : null);
            text2 = ((valueOrDefault.Item1 == 200 && !string.IsNullOrEmpty(text4)) ? text4 : null);
          }
        }
        else
        {
          text2 = null;
        }
      }
      return text2;
    }

    // Token: 0x060005AF RID: 1455 RVA: 0x00359DBC File Offset: 0x00359DBC
    [NullableContext(0)]
    private void OnMicrophoneFrame(ReadOnlyMemory<byte> frame)
    {
      object obj = this.gate;
      GeminiTranscriber geminiTranscriber;
      lock (obj)
      {
        if (!this.isListening)
        {
          return;
        }
        geminiTranscriber = this.ears;
        this.pressFrameCount++;
        int num = this.microphone.SampleRate * 2 * 120;
        if (this.pressAudioLength + frame.Length <= num)
        {
          if (this.pressAudioLength + frame.Length > this.pressAudio.Length)
          {
            byte[] array = ArrayPool<byte>.Shared.Rent(Math.Min(num, Math.Max(this.pressAudio.Length * 2, this.pressAudioLength + frame.Length + 65536)));
            this.pressAudio.AsSpan<byte>(0, this.pressAudioLength).CopyTo(array);
            if (this.pressAudio.Length != 0)
            {
              ArrayPool<byte>.Shared.Return(this.pressAudio, false);
            }
            this.pressAudio = array;
          }
          frame.Span.CopyTo(this.pressAudio.AsSpan<byte>(this.pressAudioLength));
          this.pressAudioLength += frame.Length;
        }
      }
      if (geminiTranscriber != null)
      {
        geminiTranscriber.PushAudio(frame.Span);
      }
    }

    // Token: 0x060005B0 RID: 1456 RVA: 0x00359F20 File Offset: 0x00359F20
    private void StartLiveTranscriptLoop(GeminiTranscriber transcriber)
    {
      CompanionSession.<>c__DisplayClass121_0 CS$<>8__locals1 = new CompanionSession.<>c__DisplayClass121_0();
      CS$<>8__locals1.transcriber = transcriber;
      CS$<>8__locals1.<>4__this = this;
      CancellationTokenSource cancellationTokenSource = this.liveTranscriptLoop;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      CS$<>8__locals1.loop = new CancellationTokenSource();
      this.liveTranscriptLoop = CS$<>8__locals1.loop;
      Task.Run(delegate
      {
        CompanionSession.<>c__DisplayClass121_0.<<StartLiveTranscriptLoop>b__0>d <<StartLiveTranscriptLoop>b__0>d;
        <<StartLiveTranscriptLoop>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<StartLiveTranscriptLoop>b__0>d.<>4__this = CS$<>8__locals1;
        <<StartLiveTranscriptLoop>b__0>d.<>1__state = -1;
        <<StartLiveTranscriptLoop>b__0>d.<>t__builder.Start<CompanionSession.<>c__DisplayClass121_0.<<StartLiveTranscriptLoop>b__0>d>(ref <<StartLiveTranscriptLoop>b__0>d);
        return <<StartLiveTranscriptLoop>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x060005B1 RID: 1457 RVA: 0x00359F7C File Offset: 0x00359F7C
    private void CancelListening()
    {
      object obj = this.gate;
      GeminiTranscriber geminiTranscriber;
      lock (obj)
      {
        geminiTranscriber = this.ears;
        this.ears = null;
        this.isListening = false;
        this.pressAudioLength = 0;
      }
      CancellationTokenSource cancellationTokenSource = this.liveTranscriptLoop;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.EndBluetoothHold(false);
      try
      {
        this.microphone.Stop();
      }
      catch
      {
      }
      if (geminiTranscriber != null)
      {
        geminiTranscriber.DisposeAsync().AsTask();
      }
    }

    // Token: 0x060005B2 RID: 1458 RVA: 0x0035A018 File Offset: 0x0035A018
    private void PrepareWarmEars()
    {
      AppSettings appSettings = this.settings.Current;
      GeminiTranscriber transcriber = new GeminiTranscriber(this.http, (bool force, CancellationToken ct) => this.auth.GetValidAccessTokenAsync(force, ct), EarsLanguage.Code(), CompanionSession.NormalizedKeyterms(appSettings.DictationCustomWords), this.options.UsageContext(), this.microphone.SampleRate, "/brain/ears/token");
      object obj = this.gate;
      GeminiTranscriber geminiTranscriber;
      lock (obj)
      {
        geminiTranscriber = this.warmEars;
        this.warmEars = transcriber;
        this.warmEarsExpiresTicks = Environment.TickCount64 + 120000L;
      }
      if (geminiTranscriber != null)
      {
        geminiTranscriber.DisposeAsync().AsTask();
      }
      transcriber.Start(true);
      Task.Delay(TimeSpan.FromSeconds(120L)).ContinueWith(delegate(Task _)
      {
        object obj2 = this.gate;
        lock (obj2)
        {
          if (this.warmEars != transcriber)
          {
            return;
          }
          this.warmEars = null;
        }
        _ = transcriber.DisposeAsync().AsTask();
      }, TaskScheduler.Default);
    }

    // Token: 0x060005B3 RID: 1459 RVA: 0x0035A124 File Offset: 0x0035A124
    [return: Nullable(2)]
    private GeminiTranscriber TakeWarmEars(string language, IReadOnlyList<string> keyterms, string usageContext)
    {
      object obj = this.gate;
      GeminiTranscriber geminiTranscriber;
      bool flag2;
      lock (obj)
      {
        geminiTranscriber = this.warmEars;
        this.warmEars = null;
        if (geminiTranscriber == null)
        {
          return null;
        }
        flag2 = Environment.TickCount64 > this.warmEarsExpiresTicks;
      }
      if (flag2 || geminiTranscriber.LanguageCode != language || geminiTranscriber.UsageContextValue != usageContext || !geminiTranscriber.Keyterms.SequenceEqual<string>(keyterms))
      {
        geminiTranscriber.DisposeAsync().AsTask();
        return null;
      }
      return geminiTranscriber;
    }

    // Token: 0x060005B4 RID: 1460 RVA: 0x0035A1C8 File Offset: 0x0035A1C8
    private async Task DiscardWarmEarsAsync()
    {
      object obj = this.gate;
      GeminiTranscriber geminiTranscriber;
      lock (obj)
      {
        geminiTranscriber = this.warmEars;
        this.warmEars = null;
      }
      if (geminiTranscriber != null)
      {
        await geminiTranscriber.DisposeAsync().ConfigureAwait(false);
      }
    }

    // Token: 0x060005B5 RID: 1461 RVA: 0x0035A20C File Offset: 0x0035A20C
    internal static List<string> NormalizedKeyterms(IEnumerable<string> words)
    {
      return (from w in words.SelectMany<string, string>((string w) => w.Split(new char[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries))
        select w.Trim()).Where<string>(delegate(string w)
      {
        int length = w.Length;
        return length > 0 && length <= 60;
      }).DistinctBy<string, string>((string w) => w.ToLowerInvariant()).Take<string>(50)
        .ToList<string>();
    }

    // Token: 0x060005B6 RID: 1462 RVA: 0x0035A2B8 File Offset: 0x0035A2B8
    public Task SubmitTextAsync(string text)
    {
      return this.SubmitTextAsync(text, null);
    }

    // Token: 0x060005B7 RID: 1463 RVA: 0x0035A2D8 File Offset: 0x0035A2D8
    public Task SubmitTextAsync(string text, bool? respondWithVoice)
    {
      return this.SubmitTextAsync(text, respondWithVoice, "text", null);
    }

    // Token: 0x060005B8 RID: 1464 RVA: 0x0035A2E8 File Offset: 0x0035A2E8
    public Task SubmitTextAsync(string text, IReadOnlyList<string> attachedFilePaths)
    {
      return this.SubmitTextAsync(text, null, "text", attachedFilePaths);
    }

    // Token: 0x060005B9 RID: 1465 RVA: 0x0035A30C File Offset: 0x0035A30C
    private Task SubmitTextAsync(string text, bool? respondWithVoice, string trigger, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> attachedFilePaths = null)
    {
      CompanionSession.<SubmitTextAsync>d__130 <SubmitTextAsync>d__;
      <SubmitTextAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
      <SubmitTextAsync>d__.<>4__this = this;
      <SubmitTextAsync>d__.text = text;
      <SubmitTextAsync>d__.respondWithVoice = respondWithVoice;
      <SubmitTextAsync>d__.trigger = trigger;
      <SubmitTextAsync>d__.attachedFilePaths = attachedFilePaths;
      <SubmitTextAsync>d__.<>1__state = -1;
      <SubmitTextAsync>d__.<>t__builder.Start<CompanionSession.<SubmitTextAsync>d__130>(ref <SubmitTextAsync>d__);
      return <SubmitTextAsync>d__.<>t__builder.Task;
    }

    // Token: 0x060005BA RID: 1466 RVA: 0x0035A370 File Offset: 0x0035A370
    public void BeginPageReadForTextInput(Func<bool> isStillOpen)
    {
      PageReads pageReads = this.pageReads;
      if (pageReads == null)
      {
        return;
      }
      pageReads.BeginReadForTextInput(isStillOpen);
    }

    // Token: 0x060005BB RID: 1467 RVA: 0x0035A384 File Offset: 0x0035A384
    public void TextInputClosed()
    {
      PageReads pageReads = this.pageReads;
      if (pageReads == null)
      {
        return;
      }
      pageReads.TextInputClosed();
    }

    // Token: 0x060005BC RID: 1468 RVA: 0x0035A398 File Offset: 0x0035A398
    public Task AskFromDrawingAsync(string text, string trigger)
    {
      CompanionSession.<AskFromDrawingAsync>d__135 <AskFromDrawingAsync>d__;
      <AskFromDrawingAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
      <AskFromDrawingAsync>d__.<>4__this = this;
      <AskFromDrawingAsync>d__.text = text;
      <AskFromDrawingAsync>d__.trigger = trigger;
      <AskFromDrawingAsync>d__.<>1__state = -1;
      <AskFromDrawingAsync>d__.<>t__builder.Start<CompanionSession.<AskFromDrawingAsync>d__135>(ref <AskFromDrawingAsync>d__);
      return <AskFromDrawingAsync>d__.<>t__builder.Task;
    }

    // Token: 0x060005BD RID: 1469 RVA: 0x0035A3EC File Offset: 0x0035A3EC
    public Task SubmitFactAsync(string fact, bool pointsOnly = false)
    {
      CompanionSession.<SubmitFactAsync>d__136 <SubmitFactAsync>d__;
      <SubmitFactAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
      <SubmitFactAsync>d__.<>4__this = this;
      <SubmitFactAsync>d__.fact = fact;
      <SubmitFactAsync>d__.pointsOnly = pointsOnly;
      <SubmitFactAsync>d__.<>1__state = -1;
      <SubmitFactAsync>d__.<>t__builder.Start<CompanionSession.<SubmitFactAsync>d__136>(ref <SubmitFactAsync>d__);
      return <SubmitFactAsync>d__.<>t__builder.Task;
    }

    // Token: 0x060005BE RID: 1470 RVA: 0x0035A440 File Offset: 0x0035A440
    [return: Nullable(new byte[] { 1, 2, 1 })]
    public async Task<IReadOnlyList<string>> WriteReportTurnAsync(string fact, CancellationToken ct = default(CancellationToken))
    {
      IReadOnlyList<string> readOnlyList;
      try
      {
        string text = await this.auth.GetValidAccessTokenAsync(ct).ConfigureAwait(false);
        if (text == null)
        {
          Log.Warn("Companion", "report turn not written: not signed in");
          readOnlyList = null;
        }
        else
        {
          this.brain.AdoptAccount(this.auth.UserId);
          this.brain.BeginTurnStartingNewSessionIfIdle();
          CompanionSession.NarrationState narration = new CompanionSession.NarrationState
          {
            WriteOnly = true
          };
          CompanionSession.TurnRequest turnRequest = new CompanionSession.TurnRequest(null, fact, false, false, null, this.options.DesktopActions.FrontApplication(), null, "app", Environment.TickCount64, 0, null)
          {
            Screenless = true
          };
          ScreenCaptureFrame screenCaptureFrame = new ScreenCaptureFrame(this.screens.GetDisplayUnderCursor(), Array.Empty<byte>(), 0, 0, true, default(PointD));
          CompanionSession.TurnCapture turnCapture = new CompanionSession.TurnCapture(new <>z__ReadOnlySingleElementList<ScreenCaptureFrame>(screenCaptureFrame), Array.Empty<DisplayInfo>(), null, 0, null);
          await this.StreamBrainAsync(turnRequest, turnCapture, screenCaptureFrame, new List<ScreenCaptureFrame>(), text, narration, null, () => !ct.IsCancellationRequested, new CompanionSession.TurnTimer(turnRequest.StartedTicks), ct, null).ConfigureAwait(false);
          object obj = narration.Gate;
          lock (obj)
          {
            string failure = narration.Failure;
            if (failure != null)
            {
              Log.Warn("Companion", "report turn not written: " + failure);
              readOnlyList = null;
            }
            else
            {
              readOnlyList = (from l in SayLines.InOrder(narration.ProgramSoFar)
                select l.Trim() into l
                where l.Length > 0
                select l).ToList<string>();
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Companion", "report turn not written: " + ex.Message);
        readOnlyList = null;
      }
      return readOnlyList;
    }

    // Token: 0x060005BF RID: 1471 RVA: 0x0035A494 File Offset: 0x0035A494
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<string> WriteSpokenProgramAsync(string fact, CancellationToken ct = default(CancellationToken))
    {
      string text2;
      try
      {
        CompanionSession.TurnCapture turnCapture = await this.CaptureForTurnAsync(false).ConfigureAwait(false);
        CompanionSession.TurnCapture capture = turnCapture;
        string text = await this.auth.GetValidAccessTokenAsync(ct).ConfigureAwait(false);
        if (capture.Frames.Count == 0 || text == null)
        {
          Log.Warn("Companion", "lines not written: no screenshot or not signed in");
          text2 = null;
        }
        else
        {
          this.brain.AdoptAccount(this.auth.UserId);
          this.brain.BeginTurnStartingNewSessionIfIdle();
          ScreenCaptureFrame cursorFrame = capture.Frames.FirstOrDefault<ScreenCaptureFrame>((ScreenCaptureFrame f) => f.ContainsCursor) ?? capture.Frames[0];
          List<ScreenCaptureFrame> list = capture.Frames.Where<ScreenCaptureFrame>((ScreenCaptureFrame f) => f != cursorFrame).ToList<ScreenCaptureFrame>();
          CompanionSession.NarrationState narration = new CompanionSession.NarrationState
          {
            WriteOnly = true
          };
          CompanionSession.TurnRequest turnRequest = new CompanionSession.TurnRequest(null, fact, false, false, capture, this.options.DesktopActions.FrontApplication(), null, "app", Environment.TickCount64, 0, null);
          await this.StreamBrainAsync(turnRequest, capture, cursorFrame, list, text, narration, null, () => !ct.IsCancellationRequested, new CompanionSession.TurnTimer(turnRequest.StartedTicks), ct, null).ConfigureAwait(false);
          object obj = narration.Gate;
          lock (obj)
          {
            string failure = narration.Failure;
            if (failure != null)
            {
              Log.Warn("Companion", "lines not written: " + failure);
              text2 = null;
            }
            else
            {
              text2 = narration.ProgramSoFar;
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Companion", "lines not written: " + ex.Message);
        text2 = null;
      }
      return text2;
    }

    // Token: 0x060005C0 RID: 1472 RVA: 0x0035A4E8 File Offset: 0x0035A4E8
    public void Cancel()
    {
      this.InterruptForNewInput(true);
      this.ui.Post(delegate
      {
        this.canvas.IsStreamingNarration = false;
      });
      this.CancelListening();
      this.ui.Post(delegate
      {
        IHighlightOverlay highlightOverlay = this.highlight;
        if (highlightOverlay == null)
        {
          return;
        }
        highlightOverlay.EndWatch();
      });
      this.NotifyTerminating();
      this.SetState(VoiceState.Idle);
    }

    // Token: 0x060005C1 RID: 1473 RVA: 0x0035A540 File Offset: 0x0035A540
    private void InterruptForNewInput(bool clearDrawing)
    {
      object obj = this.gate;
      CancellationTokenSource cancellationTokenSource;
      lock (obj)
      {
        this.generation += 1L;
        cancellationTokenSource = this.turnCancellation;
        this.turnCancellation = null;
        this.lineAudioOffsets.Clear();
        this.lineGeneratedSeconds.Clear();
      }
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.mouth.Interrupt();
      this.DisarmGuidedTarget(null);
      this.DisarmAdvance();
      if (clearDrawing)
      {
        this.brain.ForgetDrawingOnScreen();
        this.ui.Post(delegate
        {
          this.canvas.Clear(true);
        });
      }
    }

    // Token: 0x060005C2 RID: 1474 RVA: 0x0035A5F4 File Offset: 0x0035A5F4
    private bool RefusedOverMessageCap(string usageContext)
    {
      if (usageContext == "companion")
      {
        Func<string, bool> refuseTurnOverMessageCap = this.options.RefuseTurnOverMessageCap;
        return refuseTurnOverMessageCap != null && refuseTurnOverMessageCap(usageContext);
      }
      return false;
    }

    // Token: 0x060005C3 RID: 1475 RVA: 0x0035A61C File Offset: 0x0035A61C
    private async Task RunTurnAsync(CompanionSession.TurnRequest request, long turnGeneration, [Nullable(2)] CompanionSession.PrefetchedTurn prefetched = null)
    {
      CompanionSession.<>c__DisplayClass144_0 CS$<>8__locals1 = new CompanionSession.<>c__DisplayClass144_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.turnGeneration = turnGeneration;
      CS$<>8__locals1.prefetched = prefetched;
      CS$<>8__locals1.request = request;
      string text = CS$<>8__locals1.request.Trigger;
      bool flag = text == "clicked_target" || text == "did_step";
      if (flag && this.RefusedOverMessageCap(this.options.UsageContext()))
      {
        this.FinishTurnQuietly(Interlocked.Read(ref this.generation) == CS$<>8__locals1.turnGeneration);
      }
      else
      {
        long startedTicks = ((CS$<>8__locals1.prefetched == null) ? CS$<>8__locals1.request.StartedTicks : Environment.TickCount64);
        object obj = this.gate;
        CancellationTokenSource cancellation;
        lock (obj)
        {
          if (this.generation != CS$<>8__locals1.turnGeneration)
          {
            return;
          }
          cancellation = new CancellationTokenSource();
          this.turnCancellation = cancellation;
          this.turnStartedTicks = startedTicks;
          this.firstAudioMs = null;
          this.waitStatusLabel = null;
          this.lineAudioOffsets.Clear();
          this.lineGeneratedSeconds.Clear();
          this.lastTurnWasTyped = CS$<>8__locals1.request.Typed;
          this.lastTurnAnsweredInText = CS$<>8__locals1.request.AnswersInText;
        }
        Interlocked.Increment(ref this.activeTurnCount);
        if (CS$<>8__locals1.prefetched == null)
        {
          this.DropPrefetchedTurn("another one-brain turn started");
        }
        CS$<>8__locals1.ct = cancellation.Token;
        int? num = CS$<>8__locals1.request.WalkthroughStep;
        if (num != null || CS$<>8__locals1.request.Flow != null)
        {
          string text2 = "Companion";
          string text3 = "walkthrough turn: trigger=";
          string trigger = CS$<>8__locals1.request.Trigger;
          string text4 = " step=";
          num = CS$<>8__locals1.request.WalkthroughStep;
          Log.Info(text2, text3 + trigger + text4 + (((num != null) ? num.GetValueOrDefault().ToString() : null) ?? "-"));
        }
        EventHandler<ValueTuple<string, int?>> turnStarted = this.TurnStarted;
        if (turnStarted != null)
        {
          turnStarted(this, new ValueTuple<string, int?>(CS$<>8__locals1.request.Trigger, CS$<>8__locals1.request.WalkthroughStep));
        }
        try
        {
          EventHandler<ValueTuple<int, bool, string>> messageSent = this.MessageSent;
          if (messageSent != null)
          {
            string text5;
            if ((text5 = CS$<>8__locals1.request.Words) == null)
            {
              text5 = CS$<>8__locals1.request.Fact ?? "";
            }
            messageSent(this, new ValueTuple<int, bool, string>(text5.Length, CS$<>8__locals1.request.Typed, CS$<>8__locals1.request.Trigger));
          }
        }
        catch (Exception ex)
        {
          Log.Warn("Companion", "MessageSent handler threw: " + ex.Message);
        }
        CompanionSession.TurnTimer metrics = new CompanionSession.TurnTimer(startedTicks);
        long drawingStartedTicks = Environment.TickCount64;
        CompanionSession.NarrationState toldNarration = null;
        bool continuedDrawing = false;
        bool turnThrew = false;
        int displayCount = 0;
        try
        {
          CompanionSession.<>c__DisplayClass144_1 CS$<>8__locals2 = new CompanionSession.<>c__DisplayClass144_1();
          CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
          this.brain.AdoptAccount(this.auth.UserId);
          if (CS$<>8__locals2.CS$<>8__locals1.prefetched == null)
          {
            this.brain.BeginTurnStartingNewSessionIfIdle();
          }
          OneBrainStats.Shared.NoteTurnSent(CS$<>8__locals2.CS$<>8__locals1.request.Words != null && !CS$<>8__locals2.CS$<>8__locals1.request.Typed, new DateTimeOffset?(this.brain.PreviousTurnAt));
          Task<bool> mouthOpening = (CS$<>8__locals2.CS$<>8__locals1.request.AnswersInText ? Task.FromResult<bool>(false) : this.mouth.EnsureOpenAsync(CS$<>8__locals2.CS$<>8__locals1.ct));
          long setupStarted = Environment.TickCount64;
          CompanionSession.TurnCapture turnCapture = CS$<>8__locals2.CS$<>8__locals1.request.Capture;
          if (turnCapture == null)
          {
            turnCapture = await this.CaptureForTurnAsync(false).ConfigureAwait(false);
          }
          CS$<>8__locals2.capture = turnCapture;
          long captureDone = Environment.TickCount64;
          text = await this.auth.GetValidAccessTokenAsync(CS$<>8__locals2.CS$<>8__locals1.ct).ConfigureAwait(false);
          string accessToken = text;
          long tokenDone = Environment.TickCount64;
          if (CS$<>8__locals2.capture.Frames.Count == 0 || accessToken == null)
          {
            Log.Warn("Companion", "no screenshot or not signed in; turn dropped");
            this.FinishTurnQuietly(CS$<>8__locals2.CS$<>8__locals1.<RunTurnAsync>g__StillCurrent|0());
            if (accessToken == null)
            {
              this.RaiseFailed(new CompanionError("not_signed_in", "Sign in to talk to HeyClicky.", null));
            }
            else
            {
              this.RaiseFailed(new CompanionError("no_screenshot", "I couldn't see your screen just now, so I couldn't answer. Try again in a moment.", null));
            }
          }
          else
          {
            CS$<>8__locals2.cursorFrame = CS$<>8__locals2.capture.Frames.FirstOrDefault<ScreenCaptureFrame>((ScreenCaptureFrame f) => f.ContainsCursor) ?? CS$<>8__locals2.capture.Frames[0];
            List<ScreenCaptureFrame> otherFrames = CS$<>8__locals2.capture.Frames.Where<ScreenCaptureFrame>((ScreenCaptureFrame f) => f != CS$<>8__locals2.cursorFrame).ToList<ScreenCaptureFrame>();
            CS$<>8__locals2.colorHex = this.settings.Read<string>(([Nullable(1)] AppSettings s) => s.CursorColorHex);
            CompanionSession.<>c__DisplayClass144_1 CS$<>8__locals3 = CS$<>8__locals2;
            CompanionSession.PrefetchedTurn prefetched2 = CS$<>8__locals2.CS$<>8__locals1.prefetched;
            CS$<>8__locals3.narration = ((prefetched2 != null) ? prefetched2.Narration : null) ?? new CompanionSession.NarrationState();
            CompanionSession.<>c__DisplayClass144_1 CS$<>8__locals4 = CS$<>8__locals2;
            bool flag2;
            if (CS$<>8__locals2.CS$<>8__locals1.request.AnswersInText)
            {
              Func<bool> rendersTextInNotchChat = this.options.RendersTextInNotchChat;
              flag2 = rendersTextInNotchChat != null && rendersTextInNotchChat();
            }
            else
            {
              flag2 = false;
            }
            CS$<>8__locals4.hidesLoneSources = flag2;
            ValueTuple<DrawingContinuation, string> drawingSetup = ((!CS$<>8__locals2.CS$<>8__locals1.request.IsDrive) ? (await this.ui.InvokeAsync<ValueTuple<DrawingContinuation, string>>(delegate
            {
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.IsStreamingNarration = true;
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.HidesLoneSources = CS$<>8__locals2.hidesLoneSources;
              if (CS$<>8__locals2.CS$<>8__locals1.prefetched == null && CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.CanContinueDrawing(CS$<>8__locals2.cursorFrame))
              {
                DrawingContinuation drawingContinuation = CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.ContinueStream();
                if (drawingContinuation != null)
                {
                  return new ValueTuple<DrawingContinuation, string>(drawingContinuation, CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.DrawingStateForModel());
                }
              }
              if (CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.IsShowing)
              {
                CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.Clear(false);
              }
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.BeginProgram(CS$<>8__locals2.cursorFrame, CS$<>8__locals2.colorHex);
              return new ValueTuple<DrawingContinuation, string>(null, null);
            }).ConfigureAwait(false)) : new ValueTuple<DrawingContinuation, string>(null, null));
            long tickCount = Environment.TickCount64;
            if (tickCount - startedTicks > 900L)
            {
              string text6 = "Companion";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(105, 5);
              defaultInterpolatedStringHandler.AppendLiteral("turn setup slow: ");
              defaultInterpolatedStringHandler.AppendFormatted<long>(tickCount - startedTicks);
              defaultInterpolatedStringHandler.AppendLiteral(" ms after key-up (before setup ");
              defaultInterpolatedStringHandler.AppendFormatted<long>(setupStarted - startedTicks);
              defaultInterpolatedStringHandler.AppendLiteral(" ms, ");
              defaultInterpolatedStringHandler.AppendLiteral("capture ");
              defaultInterpolatedStringHandler.AppendFormatted<long>(captureDone - setupStarted);
              defaultInterpolatedStringHandler.AppendLiteral(" ms, token ");
              defaultInterpolatedStringHandler.AppendFormatted<long>(tokenDone - captureDone);
              defaultInterpolatedStringHandler.AppendLiteral(" ms, canvas on the UI thread ");
              defaultInterpolatedStringHandler.AppendFormatted<long>(tickCount - tokenDone);
              defaultInterpolatedStringHandler.AppendLiteral(" ms)");
              Log.Info(text6, defaultInterpolatedStringHandler.ToStringAndClear());
            }
            ScreenCaptureFrame requestFrame = CS$<>8__locals2.cursorFrame;
            CS$<>8__locals2.drawingFrame = CS$<>8__locals2.cursorFrame;
            CompanionSession.<>c__DisplayClass144_1 CS$<>8__locals5 = CS$<>8__locals2;
            bool flag3;
            if (!CS$<>8__locals2.CS$<>8__locals1.request.AnswersInText && !CS$<>8__locals2.CS$<>8__locals1.request.IsDrive)
            {
              string text7;
              if ((text7 = CS$<>8__locals2.CS$<>8__locals1.request.Flow) == null)
              {
                Func<string> turnFlow = this.options.TurnFlow;
                text7 = ((turnFlow != null) ? turnFlow() : null);
              }
              flag3 = string.IsNullOrEmpty(text7);
            }
            else
            {
              flag3 = false;
            }
            CS$<>8__locals5.recordsLesson = flag3;
            if ((drawingSetup.Item1 == null) & CS$<>8__locals2.recordsLesson)
            {
              this.BeginLessonRecording(CS$<>8__locals2.CS$<>8__locals1.request.Words, CS$<>8__locals2.capture.WithoutUserInk(CS$<>8__locals2.cursorFrame), CS$<>8__locals2.colorHex);
            }
            else
            {
              obj = this.gate;
              lock (obj)
              {
                this.lessonRecording = null;
              }
            }
            DrawingContinuation continuation = drawingSetup.Item1;
            if (continuation != null)
            {
              CS$<>8__locals2.narration.FirstLine = continuation.FirstLine;
              CS$<>8__locals2.narration.IsContinuationDecided = false;
              long swingDeadline = Environment.TickCount64 + 1150L;
              for (;;)
              {
                flag = Environment.TickCount64 < swingDeadline;
                if (flag)
                {
                  UiInvoker uiInvoker = this.ui;
                  Func<bool> func;
                  if ((func = CS$<>8__locals2.CS$<>8__locals1.<>9__7) == null)
                  {
                    CompanionSession.<>c__DisplayClass144_0 CS$<>8__locals6 = CS$<>8__locals2.CS$<>8__locals1;
                    Func<bool> func2 = () => CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.IsSwingingFiguresToFront;
                    CS$<>8__locals6.<>9__7 = func2;
                    func = func2;
                  }
                  flag = await uiInvoker.InvokeAsync<bool>(func).ConfigureAwait(false);
                }
                if (!flag)
                {
                  break;
                }
                await Task.Delay(40, CS$<>8__locals2.CS$<>8__locals1.ct).ConfigureAwait(false);
              }
              ScreenCaptureFrame screenCaptureFrame = await this.ui.InvokeAsync<ScreenCaptureFrame>(() => CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.CaptureDrawingForModel(CS$<>8__locals2.cursorFrame)).ConfigureAwait(false);
              if (screenCaptureFrame != null)
              {
                requestFrame = screenCaptureFrame;
              }
              string text8 = "Companion";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(58, 2);
              defaultInterpolatedStringHandler.AppendLiteral("building on the drawing on screen: first new line ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(continuation.FirstLine);
              defaultInterpolatedStringHandler.AppendLiteral(", ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(continuation.Marks);
              defaultInterpolatedStringHandler.AppendLiteral(" marks");
              Log.Info(text8, defaultInterpolatedStringHandler.ToStringAndClear());
            }
            CS$<>8__locals2.narration.StartOver = delegate
            {
              CompanionSession.<>c__DisplayClass144_1.<<RunTurnAsync>b__4>d <<RunTurnAsync>b__4>d;
              <<RunTurnAsync>b__4>d.<>t__builder = AsyncTaskMethodBuilder.Create();
              <<RunTurnAsync>b__4>d.<>4__this = CS$<>8__locals2;
              <<RunTurnAsync>b__4>d.<>1__state = -1;
              <<RunTurnAsync>b__4>d.<>t__builder.Start<CompanionSession.<>c__DisplayClass144_1.<<RunTurnAsync>b__4>d>(ref <<RunTurnAsync>b__4>d);
              return <<RunTurnAsync>b__4>d.<>t__builder.Task;
            };
            CS$<>8__locals2.displays = new List<ScreenCaptureFrame> { CS$<>8__locals2.cursorFrame };
            CS$<>8__locals2.displays.AddRange(otherFrames);
            if (!CS$<>8__locals2.CS$<>8__locals1.request.IsDrive && CS$<>8__locals2.displays.Count > 1)
            {
              CompanionSession.<>c__DisplayClass144_2 CS$<>8__locals7 = new CompanionSession.<>c__DisplayClass144_2();
              CS$<>8__locals7.CS$<>8__locals2 = CS$<>8__locals2;
              CS$<>8__locals7.CS$<>8__locals2.narration.DisplayCount = CS$<>8__locals7.CS$<>8__locals2.displays.Count;
              CS$<>8__locals7.CS$<>8__locals2.narration.IsDisplayDecided = false;
              CS$<>8__locals7.buildsOnDrawing = drawingSetup.Item1 != null;
              CS$<>8__locals7.CS$<>8__locals2.narration.MoveToDisplay = delegate(int index)
              {
                CompanionSession.<>c__DisplayClass144_2.<<RunTurnAsync>b__11>d <<RunTurnAsync>b__11>d;
                <<RunTurnAsync>b__11>d.<>t__builder = AsyncTaskMethodBuilder.Create();
                <<RunTurnAsync>b__11>d.<>4__this = CS$<>8__locals7;
                <<RunTurnAsync>b__11>d.index = index;
                <<RunTurnAsync>b__11>d.<>1__state = -1;
                <<RunTurnAsync>b__11>d.<>t__builder.Start<CompanionSession.<>c__DisplayClass144_2.<<RunTurnAsync>b__11>d>(ref <<RunTurnAsync>b__11>d);
                return <<RunTurnAsync>b__11>d.<>t__builder.Task;
              };
            }
            if (CS$<>8__locals2.CS$<>8__locals1.<RunTurnAsync>g__StillCurrent|0())
            {
              if (!CS$<>8__locals2.CS$<>8__locals1.request.IsDrive)
              {
                toldNarration = CS$<>8__locals2.narration;
              }
              continuedDrawing = drawingSetup.Item1 != null;
              displayCount = ((CS$<>8__locals2.capture.Displays.Count > 0) ? CS$<>8__locals2.capture.Displays.Count : CS$<>8__locals2.capture.Frames.Count);
              Task narrationTask = this.NarrateAsync(CS$<>8__locals2.narration, CS$<>8__locals2.CS$<>8__locals1.request, mouthOpening, new Func<bool>(CS$<>8__locals2.CS$<>8__locals1.<RunTurnAsync>g__StillCurrent|0), metrics, CS$<>8__locals2.CS$<>8__locals1.ct);
              if (CS$<>8__locals2.CS$<>8__locals1.prefetched != null)
              {
                await CS$<>8__locals2.CS$<>8__locals1.prefetched.PlayAsync(this, new CompanionSession.CanvasFeeder(this, CS$<>8__locals2.narration, new Func<bool>(CS$<>8__locals2.CS$<>8__locals1.<RunTurnAsync>g__StillCurrent|0)), new Func<bool>(CS$<>8__locals2.CS$<>8__locals1.<RunTurnAsync>g__StillCurrent|0), CS$<>8__locals2.CS$<>8__locals1.ct).ConfigureAwait(false);
                obj = CS$<>8__locals2.narration.Gate;
                lock (obj)
                {
                  CompanionSession.TurnTimer turnTimer = metrics;
                  if (turnTimer.Model == null)
                  {
                    turnTimer.Model = CS$<>8__locals2.narration.Model;
                  }
                  goto IL_10BD;
                }
              }
              await this.StreamBrainAsync(CS$<>8__locals2.CS$<>8__locals1.request, CS$<>8__locals2.capture, requestFrame, otherFrames, accessToken, CS$<>8__locals2.narration, drawingSetup.Item2, new Func<bool>(CS$<>8__locals2.CS$<>8__locals1.<RunTurnAsync>g__StillCurrent|0), metrics, CS$<>8__locals2.CS$<>8__locals1.ct, null).ConfigureAwait(false);
              IL_10BD:
              await narrationTask.ConfigureAwait(false);
              if (CS$<>8__locals2.CS$<>8__locals1.<RunTurnAsync>g__StillCurrent|0())
              {
                obj = CS$<>8__locals2.narration.Gate;
                List<string> list;
                string programSoFar;
                lock (obj)
                {
                  list = ((CS$<>8__locals2.narration.FirstLine == 0 && CS$<>8__locals2.narration.Failure == null) ? CS$<>8__locals2.narration.Lines.ToList<string>() : new List<string>());
                  programSoFar = CS$<>8__locals2.narration.ProgramSoFar;
                }
                this.FinishLessonRecording(list, programSoFar);
              }
              if (CS$<>8__locals2.CS$<>8__locals1.<RunTurnAsync>g__StillCurrent|0() && !CS$<>8__locals2.CS$<>8__locals1.request.IsDrive)
              {
                this.ui.Post(delegate
                {
                  CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.Finish();
                  CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.IsStreamingNarration = false;
                });
              }
              await this.RunActionsWaitingForTheDrawingAsync(CS$<>8__locals2.CS$<>8__locals1.<RunTurnAsync>g__StillCurrent|0()).ConfigureAwait(false);
              if (CS$<>8__locals2.CS$<>8__locals1.<RunTurnAsync>g__StillCurrent|0() && !CS$<>8__locals2.CS$<>8__locals1.request.IsDrive)
              {
                this.RaiseTurnFinished(CS$<>8__locals2.CS$<>8__locals1.request, CS$<>8__locals2.narration);
              }
              if (CS$<>8__locals2.CS$<>8__locals1.<RunTurnAsync>g__StillCurrent|0() && !CS$<>8__locals2.CS$<>8__locals1.request.IsDrive)
              {
                this.NoteTurnEndedForWalkthrough(CS$<>8__locals2.narration, CS$<>8__locals2.CS$<>8__locals1.request, CS$<>8__locals2.drawingFrame);
              }
              CS$<>8__locals2 = null;
              mouthOpening = null;
              accessToken = null;
              otherFrames = null;
              drawingSetup = default(ValueTuple<DrawingContinuation, string>);
              requestFrame = null;
              continuation = null;
              narrationTask = null;
            }
          }
        }
        catch (OperationCanceledException obj2) when (!CS$<>8__locals1.<RunTurnAsync>g__StillCurrent|0())
        {
        }
        catch (Exception ex2)
        {
          turnThrew = true;
          Log.Error("Companion", "turn failed", ex2);
          if (CS$<>8__locals1.<RunTurnAsync>g__StillCurrent|0())
          {
            this.RaiseFailed(new CompanionError("turn_failed", ex2.Message, null));
          }
        }
        finally
        {
          if (toldNarration != null)
          {
            CompanionSession.TrackScreenDrawingCompleted(toldNarration, CS$<>8__locals1.request, metrics, CS$<>8__locals1.<RunTurnAsync>g__StillCurrent|0(), turnThrew, continuedDrawing, displayCount, drawingStartedTicks);
          }
          if (!CS$<>8__locals1.request.Typed && !CS$<>8__locals1.request.AnswersInText)
          {
            Func<ConversationHistory> chatHistory = this.options.ChatHistory;
            if (chatHistory != null)
            {
              ConversationHistory conversationHistory = chatHistory();
              if (conversationHistory != null)
              {
                conversationHistory.NoteSpokenAnswerFinished();
              }
            }
          }
          Interlocked.Decrement(ref this.activeTurnCount);
          if (!CS$<>8__locals1.<RunTurnAsync>g__StillCurrent|0())
          {
            this.DropActionsHeldBy(CS$<>8__locals1.request);
          }
          CompanionSession.PrefetchedTurn prefetched3 = CS$<>8__locals1.prefetched;
          if (prefetched3 != null)
          {
            prefetched3.Cancel();
          }
          if (Interlocked.Read(ref this.guidedStepLoadingGeneration) == CS$<>8__locals1.turnGeneration)
          {
            this.NoteGuidedStepShowed();
          }
          if (CS$<>8__locals1.<RunTurnAsync>g__StillCurrent|0())
          {
            obj = this.gate;
            flag = false;
            try
            {
              Monitor.Enter(obj, ref flag);
              if (this.turnCancellation == cancellation)
              {
                this.turnCancellation = null;
              }
            }
            finally
            {
              int num2;
              if (num2 < 0 && flag)
              {
                Monitor.Exit(obj);
              }
            }
            flag = Volatile.Read(ref this.activeTurnCount) == 0;
            if (flag)
            {
              flag = this.State - VoiceState.Processing <= 1;
            }
            if (flag)
            {
              if (this.State == VoiceState.Processing)
              {
                this.NotifyTerminating();
              }
              this.SetState(VoiceState.Idle);
            }
            EventHandler<CompanionTurnMetrics> turnCompleted = this.TurnCompleted;
            if (turnCompleted != null)
            {
              CompanionSession.TurnTimer turnTimer2 = metrics;
              bool typed = CS$<>8__locals1.request.Typed;
              int transcriptMs = CS$<>8__locals1.request.TranscriptMs;
              CompanionSession.TurnCapture capture = CS$<>8__locals1.request.Capture;
              turnCompleted(this, turnTimer2.ToMetrics(typed, transcriptMs, (capture != null) ? capture.CaptureMs : 0, this.firstAudioMs));
            }
            Log.Info("Companion", "turn timings: " + metrics.Describe(this.firstAudioMs));
          }
          cancellation.Dispose();
          if (CS$<>8__locals1.prefetched == null)
          {
            this.RaiseOneBrainTurnEnded();
          }
        }
      }
    }

    // Token: 0x060005C4 RID: 1476 RVA: 0x0035A678 File Offset: 0x0035A678
    private void RaiseTurnFinished(CompanionSession.TurnRequest request, CompanionSession.NarrationState narration)
    {
      object obj = narration.Gate;
      CompanionTurnFinish companionTurnFinish;
      lock (obj)
      {
        companionTurnFinish = new CompanionTurnFinish(request.Words != null, narration.HeardLines.ToList<string>(), narration.Marks > 0, request.Words);
      }
      try
      {
        EventHandler<CompanionTurnFinish> turnFinished = this.TurnFinished;
        if (turnFinished != null)
        {
          turnFinished(this, companionTurnFinish);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Companion", "TurnFinished handler threw: " + ex.Message);
      }
    }

    // Token: 0x060005C5 RID: 1477 RVA: 0x0035A720 File Offset: 0x0035A720
    private static void TrackScreenDrawingCompleted(CompanionSession.NarrationState narration, CompanionSession.TurnRequest request, CompanionSession.TurnTimer metrics, bool stillCurrent, bool threw, bool continuedDrawing, int displayCount, long startedTicks)
    {
      object obj = narration.Gate;
      int count;
      int marks;
      int textDeliveries;
      int count2;
      int firstLine;
      bool targetArmed;
      bool drewOnOtherDisplay;
      string failure;
      long? firstLineHeardTicks;
      lock (obj)
      {
        count = narration.HeardLines.Count;
        marks = narration.Marks;
        textDeliveries = narration.TextDeliveries;
        count2 = narration.Lines.Count;
        firstLine = narration.FirstLine;
        targetArmed = narration.TargetArmed;
        drewOnOtherDisplay = narration.DrewOnOtherDisplay;
        failure = narration.Failure;
        firstLineHeardTicks = narration.FirstLineHeardTicks;
      }
      string text = ((!stillCurrent) ? "superseded" : (threw ? "failed" : (targetArmed ? "target_armed" : ((count > 0) ? "completed" : ((failure == null && count2 <= firstLine) ? "completed" : "failed")))));
      string text2 = metrics.Model ?? "unknown";
      string text3 = text;
      int num = count;
      int num2 = marks;
      bool flag2 = text == "target_armed";
      int valueOrDefault = request.WalkthroughStep.GetValueOrDefault();
      bool flag3 = drewOnOtherDisplay;
      bool typed = request.Typed;
      int num3 = textDeliveries;
      int? num4;
      if (firstLineHeardTicks != null)
      {
        long valueOrDefault2 = firstLineHeardTicks.GetValueOrDefault();
        num4 = new int?((int)(valueOrDefault2 - startedTicks));
      }
      else
      {
        num4 = null;
      }
      ClickyAnalytics.TrackScreenDrawingCompleted(text2, text3, num, num2, flag2, valueOrDefault, displayCount, flag3, continuedDrawing, typed, num3, num4, (int)(Environment.TickCount64 - startedTicks));
    }

    // Token: 0x060005C6 RID: 1478 RVA: 0x0035A864 File Offset: 0x0035A864
    private void FinishTurnQuietly(bool stillCurrent)
    {
      if (!stillCurrent)
      {
        return;
      }
      this.NotifyTerminating();
      this.SetState(VoiceState.Idle);
    }

    // Token: 0x060005C7 RID: 1479 RVA: 0x0035A878 File Offset: 0x0035A878
    private async Task StreamBrainAsync(CompanionSession.TurnRequest request, CompanionSession.TurnCapture capture, ScreenCaptureFrame cursorFrame, List<ScreenCaptureFrame> otherFrames, string accessToken, CompanionSession.NarrationState narration, [Nullable(2)] string drawingOnScreen, Func<bool> stillCurrent, CompanionSession.TurnTimer metrics, CancellationToken ct, [Nullable(2)] CompanionSession.PrefetchedTurn prefetched = null)
    {
      CompanionSession.<>c__DisplayClass148_0 CS$<>8__locals1 = new CompanionSession.<>c__DisplayClass148_0();
      CS$<>8__locals1.ct = ct;
      CS$<>8__locals1.request = request;
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.narration = narration;
      CS$<>8__locals1.metrics = metrics;
      CS$<>8__locals1.stillCurrent = stillCurrent;
      CS$<>8__locals1.cursorFrame = cursorFrame;
      CS$<>8__locals1.otherFrames = otherFrames;
      CS$<>8__locals1.drawingOnScreen = drawingOnScreen;
      CS$<>8__locals1.capture = capture;
      CS$<>8__locals1.accessToken = accessToken;
      CS$<>8__locals1.timeLabel = DateTime.Now.ToString("HH:mm, dddd MMM d, yyyy", CultureInfo.InvariantCulture);
      CS$<>8__locals1.frontApp = CS$<>8__locals1.request.FrontApp.Item1;
      IReadOnlyList<byte[]> readOnlyList;
      if (CS$<>8__locals1.request.AttachedFilePaths.Count > 0)
      {
        readOnlyList = await Attachments.AttachedImagesAsync(CS$<>8__locals1.request.AttachedFilePaths).ConfigureAwait(false);
      }
      else
      {
        readOnlyList = Array.Empty<byte[]>();
      }
      CS$<>8__locals1.attachedImages = readOnlyList;
      CompanionSession.<>c__DisplayClass148_0 CS$<>8__locals2 = CS$<>8__locals1;
      string words = CS$<>8__locals1.request.Words;
      CS$<>8__locals2.words = ((words != null && CS$<>8__locals1.request.AttachedFilePaths.Count > 0) ? Attachments.WordsWithSummary(words, CS$<>8__locals1.request.AttachedFilePaths, CS$<>8__locals1.attachedImages.Count > 0) : CS$<>8__locals1.request.Words);
      PageContext pageContext;
      if (CS$<>8__locals1.words != null && this.pageReads != null)
      {
        PageReads pageReads = this.pageReads;
        CancellationToken ct2 = CS$<>8__locals1.ct;
        pageContext = await pageReads.TakeForTurnAsync(null, ct2).ConfigureAwait(false);
      }
      else
      {
        pageContext = null;
      }
      CS$<>8__locals1.page = pageContext;
      CS$<>8__locals1.pageAccessNoted = true;
      if (CS$<>8__locals1.page != null && CS$<>8__locals1.page.AccessDenied && !(CS$<>8__locals1.page.Kind == "web_page"))
      {
        CompanionSession.<>c__DisplayClass148_0 CS$<>8__locals3 = CS$<>8__locals1;
        Func<string, bool> documentAccessDenied = this.options.DocumentAccessDenied;
        CS$<>8__locals3.pageAccessNoted = documentAccessDenied == null || documentAccessDenied(CS$<>8__locals1.page.Reference);
      }
      CS$<>8__locals1.turnNote = "[This turn] " + CS$<>8__locals1.timeLabel + "." + ((CS$<>8__locals1.frontApp == null) ? "" : ("\nFront app: " + CS$<>8__locals1.frontApp + "."));
      if (CS$<>8__locals1.page != null)
      {
        CS$<>8__locals1.turnNote = CS$<>8__locals1.turnNote + "\n" + ((CS$<>8__locals1.page.Kind == "web_page") ? "Page open: " : "Document open: ") + CS$<>8__locals1.page.ThreadSafeReference;
      }
      CS$<>8__locals1.highlightGesture = (CS$<>8__locals1.request.Screenless ? null : CS$<>8__locals1.capture.HighlightGesture);
      if (CS$<>8__locals1.highlightGesture != null)
      {
        string highlightGesture = CS$<>8__locals1.highlightGesture;
        CS$<>8__locals1.turnNote = CS$<>8__locals1.turnNote + "\nThey highlighted part of the screen while asking: " + highlightGesture;
      }
      CS$<>8__locals1.drive = this.options.Drive;
      CS$<>8__locals1.isDrive = CS$<>8__locals1.request.IsDrive;
      CS$<>8__locals1.offersDrive = this.DriveOfferedForRequest;
      CS$<>8__locals1.requestFlags = this.RequestFlags(CS$<>8__locals1.offersDrive);
      CS$<>8__locals1.keepsScreens = this.settings.Read<bool>((AppSettings s) => s.OneBrainKeepsScreensInHistory) && !CS$<>8__locals1.isDrive && !CS$<>8__locals1.request.Screenless;
      string storedSaid = (CS$<>8__locals1.keepsScreens ? BrainSession.StoredUserText(CS$<>8__locals1.words ?? "", CS$<>8__locals1.request.Fact ?? "") : (CS$<>8__locals1.words ?? (CS$<>8__locals1.request.Screenless ? ("(" + CS$<>8__locals1.request.Fact + ")") : ("(" + CS$<>8__locals1.request.Fact + " Here is the screen now.)"))));
      string text;
      if (!CS$<>8__locals1.request.Screenless)
      {
        Func<string> liveStateNote = this.options.LiveStateNote;
        text = ((liveStateNote != null) ? liveStateNote() : null);
      }
      else
      {
        text = null;
      }
      string text2 = text;
      string text3;
      if (CS$<>8__locals1.words == null)
      {
        text3 = null;
      }
      else
      {
        Func<bool, string> edgeDockFocusNote = this.options.EdgeDockFocusNote;
        text3 = ((edgeDockFocusNote != null) ? edgeDockFocusNote(CS$<>8__locals1.request.Typed) : null);
      }
      string text4 = text3;
      string text5;
      if (!(CS$<>8__locals1.request.Trigger == "button"))
      {
        text5 = null;
      }
      else
      {
        Func<string> takePillPlanNote = this.options.TakePillPlanNote;
        text5 = ((takePillPlanNote != null) ? takePillPlanNote() : null);
      }
      string text6 = text5;
      if (!CS$<>8__locals1.request.Screenless)
      {
        Func<bool> isOnboardingTurn = this.options.IsOnboardingTurn;
        this.inFlightTurnIsOnboarding = isOnboardingTurn != null && isOnboardingTurn();
      }
      CS$<>8__locals1.flowBrief = (CS$<>8__locals1.request.Screenless ? null : (CS$<>8__locals1.request.FlowBrief ?? CompanionSession.SafeTake(this.options.TakeFlowBrief)));
      JsonObject flowBrief = CS$<>8__locals1.flowBrief;
      bool flag = (string)((flowBrief != null) ? flowBrief["kind"] : null) == "tour_finale";
      CompanionSession.<>c__DisplayClass148_0 CS$<>8__locals4 = CS$<>8__locals1;
      string text7 = "\n";
      IEnumerable<string> enumerable;
      if (!flag)
      {
        string[] array = new string[8];
        array[0] = text6;
        int num = 1;
        Func<string> rosterNote = this.options.RosterNote;
        array[num] = ((rosterNote != null) ? rosterNote() : null);
        array[2] = text4;
        int num2 = 3;
        Func<string> planNote = this.options.PlanNote;
        array[num2] = ((planNote != null) ? planNote() : null);
        array[4] = text2;
        int num3 = 5;
        Func<bool, string> browserStateNote = this.options.BrowserStateNote;
        array[num3] = ((browserStateNote != null) ? browserStateNote(CS$<>8__locals1.requestFlags.Drive) : null);
        array[6] = (CS$<>8__locals1.request.Screenless ? null : FullscreenGameNote.Text());
        enumerable = array;
        array[7] = VoiceCatalog.SpokenLanguageNote(this.settings.Read<string>(([Nullable(1)] AppSettings s) => s.VoiceLanguageCode));
      }
      else
      {
        enumerable = Array.Empty<string>();
      }
      CS$<>8__locals4.liveState = string.Join(text7, from note in enumerable.Append(Drawing3DAvailability.Note)
        where !string.IsNullOrWhiteSpace(note)
        select note);
      CS$<>8__locals1.userContext = this.SafeUserContext();
      CompanionSession.<>c__DisplayClass148_0 CS$<>8__locals5 = CS$<>8__locals1;
      Func<string> turnFlow = this.options.TurnFlow;
      CS$<>8__locals5.flow = ((turnFlow != null) ? turnFlow() : null);
      CS$<>8__locals1.usageContext = this.options.UsageContext();
      string text8;
      if (CS$<>8__locals1.highlightGesture != null)
      {
        string highlightGesture2 = CS$<>8__locals1.highlightGesture;
        string highlightText = CS$<>8__locals1.capture.HighlightText;
        if (highlightText != null)
        {
          text8 = highlightGesture2 + " " + highlightText;
          goto IL_08F6;
        }
      }
      text8 = CS$<>8__locals1.highlightGesture;
      IL_08F6:
      string text9 = text8;
      CS$<>8__locals1.toolContext = new CompanionToolContext(CS$<>8__locals1.request.Words, CS$<>8__locals1.request.Typed, CS$<>8__locals1.request.AttachedFilePaths, CS$<>8__locals1.cursorFrame.Jpeg, text9, (CS$<>8__locals1.page == null) ? null : new PageReference((CS$<>8__locals1.page.Kind == "web_page") ? "web_page" : "file", CS$<>8__locals1.page.Reference, 0, (IntPtr)0));
      object obj = CS$<>8__locals1.narration.Gate;
      lock (obj)
      {
        CS$<>8__locals1.narration.ToolContext = CS$<>8__locals1.toolContext;
      }
      if (!CS$<>8__locals1.request.Screenless)
      {
        this.canvas.ForgetWhetherLatestAnswerPointsAtScreen();
      }
      CS$<>8__locals1.context = this.brain.BeginRequest(new ValueTuple<int, int>(CS$<>8__locals1.cursorFrame.PixelWidth, CS$<>8__locals1.cursorFrame.PixelHeight), CS$<>8__locals1.drawingOnScreen != null, CS$<>8__locals1.request.Screenless);
      CS$<>8__locals1.screenshotBase64 = (CS$<>8__locals1.request.Screenless ? "" : Convert.ToBase64String(CS$<>8__locals1.cursorFrame.Jpeg));
      string storedScreenshotDataUrl = (CS$<>8__locals1.keepsScreens ? ("data:image/jpeg;base64," + CS$<>8__locals1.screenshotBase64) : null);
      List<JsonNode> turnItems = new List<JsonNode>();
      List<string> searchRecords = new List<string>();
      string turnToken = null;
      string earlierCode = "";
      CS$<>8__locals1.streamedCode = "";
      bool keepsReasoning = false;
      int lastInputTokens = 0;
      string turnScreenshotFile = null;
      int audioPlaybackBeganBefore = this.AudioPlaybackBeganCount;
      obj = this.gate;
      lock (obj)
      {
        this.currentTurnProgramSoFar = "";
      }
      BrainToolRunner runner = null;
      CS$<>8__locals1.feeder = ((prefetched != null) ? prefetched.Feeder : null) ?? new CompanionSession.CanvasFeeder(this, CS$<>8__locals1.narration, CS$<>8__locals1.stillCurrent);
      int maximumRounds = (CS$<>8__locals1.isDrive ? 2147483646 : 5);
      try
      {
        CompanionSession.<>c__DisplayClass148_1 CS$<>8__locals6 = new CompanionSession.<>c__DisplayClass148_1();
        CS$<>8__locals6.CS$<>8__locals1 = CS$<>8__locals1;
        CS$<>8__locals6.round = 0;
        while (CS$<>8__locals6.round <= maximumRounds)
        {
          CompanionSession.<>c__DisplayClass148_2 CS$<>8__locals7 = new CompanionSession.<>c__DisplayClass148_2();
          CS$<>8__locals7.CS$<>8__locals2 = CS$<>8__locals6;
          CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
          CS$<>8__locals7.codePrefix = earlierCode;
          CompanionSession.<>c__DisplayClass148_2 CS$<>8__locals8 = CS$<>8__locals7;
          Func<BrainToolCall, Task<string>> func;
          if ((func = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.<>9__3) == null)
          {
            CompanionSession.<>c__DisplayClass148_0 CS$<>8__locals9 = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1;
            Func<BrainToolCall, Task<string>> func2 = delegate(BrainToolCall call)
            {
              CompanionSession.<>c__DisplayClass148_0.<<StreamBrainAsync>b__3>d <<StreamBrainAsync>b__3>d;
              <<StreamBrainAsync>b__3>d.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
              <<StreamBrainAsync>b__3>d.<>4__this = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1;
              <<StreamBrainAsync>b__3>d.call = call;
              <<StreamBrainAsync>b__3>d.<>1__state = -1;
              <<StreamBrainAsync>b__3>d.<>t__builder.Start<CompanionSession.<>c__DisplayClass148_0.<<StreamBrainAsync>b__3>d>(ref <<StreamBrainAsync>b__3>d);
              return <<StreamBrainAsync>b__3>d.<>t__builder.Task;
            };
            CS$<>8__locals9.<>9__3 = func2;
            func = func2;
          }
          CS$<>8__locals8.roundRunner = new BrainToolRunner(func);
          if (CS$<>8__locals7.CS$<>8__locals2.round == 0)
          {
            string text10 = "Companion";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(129, 8);
            defaultInterpolatedStringHandler.AppendLiteral("turn context: page=");
            string text12;
            if (CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.page != null)
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(10, 4);
              defaultInterpolatedStringHandler2.AppendFormatted(CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.page.Kind);
              defaultInterpolatedStringHandler2.AppendLiteral(" ");
              defaultInterpolatedStringHandler2.AppendFormatted(CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.page.ThreadSafeReference);
              defaultInterpolatedStringHandler2.AppendLiteral(" (");
              string text11 = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.page.Text;
              defaultInterpolatedStringHandler2.AppendFormatted<int>((text11 != null) ? text11.Length : 0);
              defaultInterpolatedStringHandler2.AppendLiteral(" chars");
              defaultInterpolatedStringHandler2.AppendFormatted(CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.page.AccessDenied ? ", access denied" : "");
              defaultInterpolatedStringHandler2.AppendLiteral(")");
              text12 = defaultInterpolatedStringHandler2.ToStringAndClear();
            }
            else
            {
              text12 = "none";
            }
            defaultInterpolatedStringHandler.AppendFormatted(text12);
            defaultInterpolatedStringHandler.AppendLiteral(", ");
            defaultInterpolatedStringHandler.AppendLiteral("attached images=");
            defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.attachedImages.Count);
            defaultInterpolatedStringHandler.AppendLiteral(", files=");
            defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.request.AttachedFilePaths.Count);
            defaultInterpolatedStringHandler.AppendLiteral(", live state=");
            defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.liveState.Length);
            defaultInterpolatedStringHandler.AppendLiteral(" chars, ");
            defaultInterpolatedStringHandler.AppendLiteral("user context: apps=[");
            defaultInterpolatedStringHandler.AppendFormatted(string.Join(",", CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.userContext.ConnectedApps));
            defaultInterpolatedStringHandler.AppendLiteral("] reconnect=[");
            defaultInterpolatedStringHandler.AppendFormatted(string.Join(",", CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.userContext.NeedsReconnect));
            defaultInterpolatedStringHandler.AppendLiteral("] roster=");
            defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.userContext.RosterNote.Length);
            defaultInterpolatedStringHandler.AppendLiteral(" chars, front app id=");
            defaultInterpolatedStringHandler.AppendFormatted(AppTeachingIds.FrontAppIdentifier(CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.request.FrontApp.Item2));
            Log.Info(text10, defaultInterpolatedStringHandler.ToStringAndClear());
          }
          runner = CS$<>8__locals7.roundRunner;
          CS$<>8__locals7.userLayer = this.brain.UserLayer;
          CS$<>8__locals7.userLayerSignature = this.brain.UserLayerSignature;
          CS$<>8__locals7.sendEnvironment = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.context.SendEnvironment && CS$<>8__locals7.CS$<>8__locals2.round == 0;
          CS$<>8__locals7.roundTurnToken = turnToken;
          CS$<>8__locals7.itemsSoFar = turnItems.ToList<JsonNode>();
          CS$<>8__locals7.isTurnsFirstRequest = CS$<>8__locals7.roundTurnToken == null;
          CS$<>8__locals7.driveBilling = (CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.isDrive ? CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.drive.TakeDriveBillingRequestFields() : null);
          int num4 = 0;
          BrainReply reply;
          try
          {
            reply = await CS$<>8__locals7.<StreamBrainAsync>g__StreamOnce|4(this.brain.ThreadSnapshotForWorker()).ConfigureAwait(false);
          }
          catch (ScreenshotFilesUnavailableException)
          {
            num4 = 1;
          }
          if (num4 == 1)
          {
            this.brain.TurnOffScreenshotFilesForSession();
            reply = await CS$<>8__locals7.<StreamBrainAsync>g__StreamOnce|4(this.brain.ThreadSnapshotForWorker()).ConfigureAwait(false);
          }
          if (this.brain.SessionId != CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.context.SessionId)
          {
            throw new OperationCanceledException("a fresh session started during this turn");
          }
          if (turnScreenshotFile == null)
          {
            turnScreenshotFile = reply.ScreenshotFile;
          }
          if (CS$<>8__locals7.CS$<>8__locals2.round == 0)
          {
            int? num5 = reply.FirstTokenMilliseconds;
            if (num5 != null)
            {
              int valueOrDefault = num5.GetValueOrDefault();
              num5 = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.metrics.RequestSentMs;
              if (num5 != null)
              {
                int valueOrDefault2 = num5.GetValueOrDefault();
                CompanionSession.TurnTimer metrics2 = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.metrics;
                metrics2.FirstTextMs.GetValueOrDefault();
                if (metrics2.FirstTextMs == null)
                {
                  metrics2.FirstTextMs = new int?(valueOrDefault2 + valueOrDefault);
                }
              }
            }
          }
          string userLayer = reply.UserLayer;
          if (userLayer != null)
          {
            this.brain.AdoptUserLayer(userLayer, reply.UserLayerSignature);
          }
          CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.metrics.Model = reply.ModelIdentifier ?? CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.metrics.Model;
          obj = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.narration.Gate;
          lock (obj)
          {
            CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.narration.Model = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.metrics.Model;
          }
          OneBrainStats shared = OneBrainStats.Shared;
          int round = CS$<>8__locals7.CS$<>8__locals2.round;
          JsonObject usage = reply.Usage;
          double? costUsd = reply.CostUsd;
          string text13;
          if ((text13 = CompanionSession.ModelDisplayName(this.settings.Read<string>(([Nullable(1)] AppSettings s) => s.OneBrainModel))) == null)
          {
            text13 = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.metrics.Model ?? "";
          }
          shared.NoteRound(round, usage, costUsd, text13, reply.FirstTokenMilliseconds);
          if (CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.context.IsSessionsFirstTurn && CS$<>8__locals7.CS$<>8__locals2.round == 0 && reply.LastInputTokens > 0)
          {
            string text14 = this.settings.Read<string>(([Nullable(1)] AppSettings s) => s.OneBrainModel);
            if (text14 == null || !text14.StartsWith("claude-", StringComparison.Ordinal))
            {
              this.brain.NoteBaselineInputTokens(reply.LastInputTokens);
            }
          }
          keepsReasoning = reply.KeepsReasoning;
          if (reply.MessageRecorded)
          {
            Action<bool> messageCounted = this.options.MessageCounted;
            if (messageCounted != null)
            {
              messageCounted(true);
            }
          }
          if (reply.MessageRefunded)
          {
            Action<bool> messageCounted2 = this.options.MessageCounted;
            if (messageCounted2 != null)
            {
              messageCounted2(false);
            }
          }
          for (int k = 0; k < reply.DriveMessagesRefunded; k++)
          {
            Action<bool> messageCounted3 = this.options.MessageCounted;
            if (messageCounted3 != null)
            {
              messageCounted3(false);
            }
          }
          if (reply.LastInputTokens > 0)
          {
            lastInputTokens = reply.LastInputTokens;
          }
          else if (reply.InputTokens > 0)
          {
            lastInputTokens = reply.InputTokens;
          }
          turnItems.AddRange(reply.Items);
          searchRecords.AddRange(reply.SearchRecords);
          if (reply.Code.Length > 0)
          {
            earlierCode = ((CS$<>8__locals7.codePrefix.Length == 0) ? reply.Code : (CS$<>8__locals7.codePrefix + "\n" + reply.Code));
          }
          string turnToken2 = reply.TurnToken;
          if (turnToken2 != null)
          {
            turnToken = turnToken2;
          }
          BrainSession brainSession = this.brain;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["kind"] = "round";
          jsonObject["round"] = CS$<>8__locals7.CS$<>8__locals2.round;
          jsonObject["words"] = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.words ?? "";
          jsonObject["fact"] = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.request.Fact ?? "";
          jsonObject["code"] = reply.Code;
          jsonObject["tool_calls"] = new JsonArray(reply.ToolCalls.Select<BrainToolCall, JsonNode>((BrainToolCall c) => JsonValue.Create(c.Name, null)).ToArray<JsonNode>());
          jsonObject["async_calls"] = new JsonArray((from c in reply.ToolCalls
            where c.IsAsync
            select JsonValue.Create(c.Name, null)).ToArray<JsonNode>());
          jsonObject["computer_actions"] = new JsonArray((from a in reply.ComputerCalls.SelectMany<BrainComputerCall, JsonObject>((BrainComputerCall c) => c.Actions)
            select JsonValue.Create(((string)a["type"]) ?? "", null)).ToArray<JsonNode>());
          string text15 = "usage";
          JsonObject usage2 = reply.Usage;
          jsonObject[text15] = ((usage2 != null) ? usage2.DeepClone() : null);
          brainSession.LogRecord(jsonObject);
          if (CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.request.IsDrive)
          {
            string spokenSentence = reply.SpokenSentence;
            if (spokenSentence != null)
            {
              IClickyDriveTurns drive = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.drive;
              if (drive != null)
              {
                drive.NoteSpokenStep(spokenSentence);
              }
              obj = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.narration.Gate;
              lock (obj)
              {
                CompanionSession.NarrationState narration2 = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.narration;
                List<string> lines = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.narration.Lines;
                List<string> list = new List<string>(1 + lines.Count);
                list.AddRange(lines);
                list.Add(spokenSentence);
                narration2.Lines = list;
                CompanionSession.NarrationState narration3 = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.narration;
                narration3.FirstSayLineMs.GetValueOrDefault();
                if (narration3.FirstSayLineMs == null)
                {
                  narration3.FirstSayLineMs = new int?((int)(Environment.TickCount64 - this.turnStartedTicks));
                }
              }
              this.SignalNarration();
            }
          }
          if (CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.request.IsDrive && CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.drive != null && reply.ComputerCalls.Count > 0)
          {
            foreach (JsonObject jsonObject2 in from i in reply.Items.OfType<JsonObject>()
              where (string)i["type"] == "computer_call"
              select i)
            {
              string text16 = "Companion";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(37, 2);
              defaultInterpolatedStringHandler.AppendLiteral("drive round ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals7.CS$<>8__locals2.round);
              defaultInterpolatedStringHandler.AppendLiteral(": computer_call fields [");
              defaultInterpolatedStringHandler.AppendFormatted(string.Join(",", jsonObject2.Select<KeyValuePair<string, JsonNode>, string>((KeyValuePair<string, JsonNode> p) => p.Key + ((p.Value == null) ? "=null" : ""))));
              defaultInterpolatedStringHandler.AppendLiteral("]");
              Log.Info(text16, defaultInterpolatedStringHandler.ToStringAndClear());
            }
            runner = null;
            bool handedBack = false;
            foreach (BrainComputerCall call2 in reply.ComputerCalls)
            {
              if (call2.PendingSafetyChecks.Count > 0)
              {
                CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.drive.StopForSafetyCheck(call2.PendingSafetyChecks);
                handedBack = true;
                break;
              }
              ScreenCaptureFrame screenCaptureFrame = await CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.drive.PerformComputerActionsAsync(call2.Actions, audioPlaybackBeganBefore).ConfigureAwait(false);
              if (screenCaptureFrame == null)
              {
                handedBack = true;
                break;
              }
              CompanionSession.ShrinkOlderScreenshots(turnItems);
              List<JsonNode> list2 = turnItems;
              JsonObject jsonObject3 = new JsonObject(null);
              jsonObject3["type"] = "computer_call_output";
              jsonObject3["call_id"] = call2.CallId;
              string text17 = "output";
              JsonObject jsonObject4 = new JsonObject(null);
              jsonObject4["type"] = "computer_screenshot";
              jsonObject4["image_url"] = "data:image/jpeg;base64," + Convert.ToBase64String(screenCaptureFrame.Jpeg);
              jsonObject4["detail"] = "original";
              jsonObject3[text17] = jsonObject4;
              list2.Add(jsonObject3);
              call2 = null;
            }
            List<BrainComputerCall>.Enumerator enumerator2 = default(List<BrainComputerCall>.Enumerator);
            if (handedBack)
            {
              break;
            }
            if (!CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.stillCurrent())
            {
              break;
            }
          }
          else
          {
            if (reply.ToolCalls.Count == 0)
            {
              break;
            }
            IClickyDriveTurns drive2 = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.drive;
            if (drive2 != null)
            {
              drive2.NoteTurnBeforeTools(SayLines.InOrder(earlierCode).Count > 0, turnToken);
            }
            CS$<>8__locals7.pointsAtScreen = this.canvas.LatestAnswerPointsAtScreen;
            CS$<>8__locals7.held = reply.ToolCalls.Where<BrainToolCall>((BrainToolCall c) => CS$<>8__locals7.pointsAtScreen && BrainToolCallPolicy.BringsAnotherAppForward(c.Name, c.IsAsync) && !CS$<>8__locals7.roundRunner.HasStarted(c.CallId)).ToList<BrainToolCall>();
            List<BrainToolCall> runNow = reply.ToolCalls.Where<BrainToolCall>((BrainToolCall c) => !CS$<>8__locals7.held.Contains(c)).ToList<BrainToolCall>();
            List<string> list3 = await CS$<>8__locals7.roundRunner.OutputsAsync(runNow).ConfigureAwait(false);
            for (int j = 0; j < runNow.Count; j++)
            {
              List<JsonNode> list4 = turnItems;
              JsonObject jsonObject5 = new JsonObject(null);
              jsonObject5["type"] = "function_call_output";
              jsonObject5["call_id"] = runNow[j].CallId;
              jsonObject5["output"] = list3[j];
              list4.Add(jsonObject5);
            }
            if (CS$<>8__locals7.held.Count > 0)
            {
              this.HoldActionsForTheDrawing(CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.context.SessionId, CS$<>8__locals7.held, CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.request, CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.toolContext);
            }
            runner = null;
            if (CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.drive != null)
            {
              IEnumerable<BrainToolCall> toolCalls = reply.ToolCalls;
              Func<BrainToolCall, bool> func3;
              if ((func3 = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.<>9__9) == null)
              {
                CompanionSession.<>c__DisplayClass148_0 CS$<>8__locals10 = CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1;
                Func<BrainToolCall, bool> func4 = (BrainToolCall c) => CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.drive.IsDriveToolName(c.Name);
                CS$<>8__locals10.<>9__9 = func4;
                func3 = func4;
              }
              if (toolCalls.Any<BrainToolCall>(func3))
              {
                break;
              }
            }
            if ((reply.Code.Length != 0 && !BrainToolCallPolicy.NeedsAnotherRound(runNow, list3)) || CS$<>8__locals7.CS$<>8__locals2.round >= maximumRounds || !CS$<>8__locals7.CS$<>8__locals2.CS$<>8__locals1.stillCurrent())
            {
              break;
            }
            CS$<>8__locals7 = null;
            reply = null;
            runNow = null;
          }
          CS$<>8__locals6.round++;
        }
        CS$<>8__locals6 = null;
        if (!CS$<>8__locals1.isDrive)
        {
          CS$<>8__locals1.feeder.Push(earlierCode, true);
          await CS$<>8__locals1.feeder.DrainAsync().ConfigureAwait(false);
        }
        obj = CS$<>8__locals1.narration.Gate;
        lock (obj)
        {
          CS$<>8__locals1.narration.Finished = true;
        }
        this.SignalNarration();
        bool flag2 = prefetched != null;
        if (flag2)
        {
          ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = prefetched.CommitGate.ConfigureAwait(false).GetAwaiter();
          if (!configuredTaskAwaiter.IsCompleted)
          {
            await configuredTaskAwaiter;
            ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
            configuredTaskAwaiter = configuredTaskAwaiter2;
            configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
            int num6 = -1;
          }
          flag2 = !configuredTaskAwaiter.GetResult();
        }
        if (flag2)
        {
          BrainSession brainSession2 = this.brain;
          JsonObject jsonObject6 = new JsonObject(null);
          jsonObject6["kind"] = "dropped_before_use";
          jsonObject6["fact"] = CS$<>8__locals1.request.Fact ?? "";
          brainSession2.LogRecord(jsonObject6);
          throw new OperationCanceledException("the prefetched turn was dropped before use");
        }
        if (this.brain.CommitTurn(CS$<>8__locals1.context.SessionId, CS$<>8__locals1.turnNote, storedSaid, storedScreenshotDataUrl, turnItems, searchRecords, false, keepsReasoning && !CS$<>8__locals1.isDrive, (CS$<>8__locals1.isDrive || CS$<>8__locals1.request.Screenless) ? null : earlierCode, turnScreenshotFile, false))
        {
          if (CS$<>8__locals1.isDrive)
          {
            this.NoteDriveActions(CS$<>8__locals1.context.SessionId);
          }
          if (CS$<>8__locals1.drive != null && SayLines.InOrder(earlierCode).Count == 0)
          {
            JsonNode jsonNode = turnItems.LastOrDefault<JsonNode>((JsonNode i) => (string)i["type"] == "function_call" && CS$<>8__locals1.drive.IsDriveToolName(((string)i["name"]) ?? ""));
            if (jsonNode != null)
            {
              CS$<>8__locals1.drive.VoiceSilentToolTurn(((string)jsonNode["name"]) ?? "", ((string)jsonNode["arguments"]) ?? "{}");
            }
          }
          this.brain.CompactThreadIfLarge(lastInputTokens);
          this.brain.SaveToDisk();
        }
      }
      catch (Exception ex)
      {
        List<JsonNode> list5 = turnItems.ToList<JsonNode>();
        BrainToolRunner brainToolRunner = runner;
        List<ValueTuple<BrainToolCall, string>>.Enumerator enumerator3 = (((brainToolRunner != null) ? brainToolRunner.FinishedCallsWithOutputs() : null) ?? new List<ValueTuple<BrainToolCall, string>>()).GetEnumerator();
        int num6;
        try
        {
          while (enumerator3.MoveNext())
          {
            ValueTuple<BrainToolCall, string> valueTuple = enumerator3.Current;
            BrainToolCall item = valueTuple.Item1;
            string item2 = valueTuple.Item2;
            JsonObject jsonObject7 = new JsonObject(null);
            jsonObject7["type"] = "function_call";
            jsonObject7["call_id"] = item.CallId;
            jsonObject7["name"] = item.Name;
            jsonObject7["arguments"] = item.Arguments;
            list5.Add(jsonObject7);
            JsonObject jsonObject8 = new JsonObject(null);
            jsonObject8["type"] = "function_call_output";
            jsonObject8["call_id"] = item.CallId;
            jsonObject8["output"] = item2;
            list5.Add(jsonObject8);
          }
        }
        finally
        {
          if (num6 < 0)
          {
            ((IDisposable)enumerator3).Dispose();
          }
        }
        string text18 = ((CS$<>8__locals1.streamedCode.Length > earlierCode.Length) ? CS$<>8__locals1.streamedCode : earlierCode);
        if (text18.Length > 0)
        {
          JsonObject jsonObject9 = new JsonObject(null);
          jsonObject9["type"] = "message";
          jsonObject9["role"] = "assistant";
          string text19 = "content";
          JsonObject jsonObject10 = new JsonObject(null);
          jsonObject10["type"] = "output_text";
          jsonObject10["text"] = "```js\n" + text18 + "\n```";
          JsonNode jsonNode2 = jsonObject10;
          jsonObject9[text19] = new JsonArray(new ReadOnlySpan<JsonNode>(ref jsonNode2));
          list5.Add(jsonObject9);
        }
        bool flag3 = ex is PaywallReachedException;
        if (prefetched != null)
        {
          BrainSession brainSession3 = this.brain;
          JsonObject jsonObject11 = new JsonObject(null);
          jsonObject11["kind"] = "dropped_before_use";
          jsonObject11["fact"] = CS$<>8__locals1.request.Fact ?? "";
          jsonObject11["error"] = ex.Message;
          brainSession3.LogRecord(jsonObject11);
        }
        else if (!flag3)
        {
          if (this.brain.CommitTurn(CS$<>8__locals1.context.SessionId, CS$<>8__locals1.turnNote, storedSaid, storedScreenshotDataUrl, list5, searchRecords, true, keepsReasoning && !CS$<>8__locals1.isDrive, null, turnScreenshotFile, BrainSession.IsSafetyRefusal(ex.Message)) & CS$<>8__locals1.isDrive)
          {
            this.NoteDriveActions(CS$<>8__locals1.context.SessionId);
          }
          this.brain.SaveToDisk();
        }
        if (CS$<>8__locals1.request.IsDrive && flag3)
        {
          IClickyDriveTurns drive3 = CS$<>8__locals1.drive;
          if (drive3 != null)
          {
            drive3.StopForOutOfAgentMessages();
          }
        }
        else if (CS$<>8__locals1.request.IsDrive && !(ex is OperationCanceledException))
        {
          IClickyDriveTurns drive4 = CS$<>8__locals1.drive;
          if (drive4 != null)
          {
            drive4.NoteDriveRequestFailed();
          }
          ClickyAnalytics.ReportInternalError("clicky_drive", "drive_failed", ex, null, CrashReportLevel.Error, null, null);
        }
        obj = CS$<>8__locals1.narration.Gate;
        bool flag2 = false;
        try
        {
          Monitor.Enter(obj, ref flag2);
          CS$<>8__locals1.narration.Finished = true;
          CS$<>8__locals1.narration.Failure = ((ex is OperationCanceledException) ? "cancelled" : ex.Message);
        }
        finally
        {
          if (num6 < 0 && flag2)
          {
            Monitor.Exit(obj);
          }
        }
        this.SignalNarration();
        PaywallReachedException ex2 = ex as PaywallReachedException;
        if (ex2 != null)
        {
          if (CS$<>8__locals1.stillCurrent())
          {
            this.NotifyTerminating();
            this.SetState(VoiceState.Idle);
            this.RaiseFailed(new CompanionError("paywall", ex2.Payload.Title, new JsonElement?(ex2.Raw)));
          }
        }
        else
        {
          if (ex is OperationCanceledException && !CS$<>8__locals1.stillCurrent())
          {
            throw;
          }
          Log.Warn("Companion", "brain turn failed: " + ex.Message);
          WorkerHttpException ex3 = ex as WorkerHttpException;
          if (ex3 != null && ex3.Status == 429)
          {
            obj = CS$<>8__locals1.narration.Gate;
            flag2 = false;
            try
            {
              Monitor.Enter(obj, ref flag2);
              CS$<>8__locals1.narration.Failure = "HTTP 429";
            }
            finally
            {
              if (num6 < 0 && flag2)
              {
                Monitor.Exit(obj);
              }
            }
          }
        }
      }
    }

    // Token: 0x060005C8 RID: 1480 RVA: 0x0035A91C File Offset: 0x0035A91C
    [NullableContext(2)]
    private static JsonObject SafeTake(Func<JsonObject> take)
    {
      if (take == null)
      {
        return null;
      }
      JsonObject jsonObject;
      try
      {
        jsonObject = take();
      }
      catch (Exception ex)
      {
        Log.Warn("Companion", "flow brief: " + ex.Message);
        jsonObject = null;
      }
      return jsonObject;
    }

    // Token: 0x060005C9 RID: 1481 RVA: 0x0035A968 File Offset: 0x0035A968
    [NullableContext(2)]
    private static string ModelDisplayName(string pick)
    {
      if (pick != null)
      {
        if (pick != null)
        {
          int length = pick.Length;
          if (length == 0)
          {
            goto IL_018D;
          }
          switch (length)
          {
          case 11:
            if (pick == "gpt-6.1-sol")
            {
              return "GPT-6.1 Sol";
            }
            break;
          case 15:
            if (pick == "claude-opus-5-5")
            {
              return "Claude Opus 5.5 (experimental)";
            }
            break;
          case 16:
          {
            char c = pick[0];
            if (c != 'c')
            {
              if (c == 'g')
              {
                if (pick == "gpt-6.1-sol-fast")
                {
                  return "GPT-6.1 Sol fast";
                }
              }
            }
            else if (pick == "claude-haiku-5-5")
            {
              return "Claude Haiku 5.5 (experimental)";
            }
            break;
          }
          case 17:
            if (pick == "claude-sonnet-5-5")
            {
              return "Claude Sonnet 5.5 (experimental)";
            }
            break;
          case 18:
            if (pick == "gpt-6.1-sol-medium")
            {
              return "GPT-6.1 Sol (medium)";
            }
            break;
          case 21:
            if (pick == "gpt-6.1-sol-ultrafast")
            {
              return "GPT-6.1 Sol Ultrafast (low)";
            }
            break;
          case 23:
            if (pick == "gpt-6.1-sol-fast-medium")
            {
              return "GPT-6.1 Sol fast (medium)";
            }
            break;
          case 24:
            if (pick == "deepseek-v4.1-flash-high")
            {
              return "DeepSeek V4.1 Flash 1017 (high, experimental)";
            }
            break;
          case 26:
          {
            char c = pick[0];
            if (c != 'd')
            {
              if (c == 'g')
              {
                if (pick == "gpt-6.1-sol-ultrafast-high")
                {
                  return "GPT-6.1 Sol Ultrafast (high)";
                }
              }
            }
            else if (pick == "deepseek-v4.1-flash-medium")
            {
              return "DeepSeek V4.1 Flash 1017 (medium, experimental)";
            }
            break;
          }
          case 28:
            if (pick == "gpt-6.1-sol-ultrafast-medium")
            {
              return "GPT-6.1 Sol Ultrafast (medium)";
            }
            break;
          }
        }
        return pick;
      }
      IL_018D:
      return null;
    }

    // Token: 0x060005CA RID: 1482 RVA: 0x0035AB6C File Offset: 0x0035AB6C
    [NullableContext(2)]
    private static void NoteFirstLineHeard(string model)
    {
      OneBrainStats.Shared.NoteFirstLineHeard();
      BrainTurnTiming.NoteFirstLineHeard(model);
    }

    // Token: 0x060005CB RID: 1483 RVA: 0x0035AB80 File Offset: 0x0035AB80
    internal static void WriteTurnBody(Utf8JsonWriter writer, CompanionSession.TurnBodyParts p)
    {
      writer.WriteStartObject();
      BrainClient.WriteThread(writer, "thread", p.Thread);
      writer.WriteStartObject("turn");
      writer.WriteString("words", p.Words ?? "");
      writer.WriteString("fact", p.Fact ?? "");
      writer.WriteString("time_label", p.TimeLabel);
      writer.WriteString("front_app", p.FrontApp ?? "");
      writer.WriteString("front_app_bundle_id", p.FrontAppProcess ?? "");
      writer.WriteString("drawing_on_screen", p.DrawingOnScreen ?? "");
      if (p.HighlightGesture == null)
      {
        writer.WriteNull("highlight");
      }
      else
      {
        writer.WriteStartObject("highlight");
        writer.WriteString("gesture", p.HighlightGesture);
        writer.WriteString("text", p.HighlightText ?? "");
        writer.WriteEndObject();
      }
      PageContext page = p.Page;
      if (page != null)
      {
        page.Write(writer, p.PageAccessNoted);
      }
      else
      {
        writer.WriteNull("page");
      }
      writer.WriteString("input", p.Typed ? "text" : "voice");
      writer.WriteString("live_state", p.LiveState);
      if (!p.Screenless)
      {
        writer.WriteStartObject("screenshot");
        writer.WriteString("base64", p.ScreenshotBase64);
        writer.WriteString("mimeType", "image/jpeg");
        writer.WriteNumber("widthInPixels", p.Screenshot.PixelWidth);
        writer.WriteNumber("heightInPixels", p.Screenshot.PixelHeight);
        writer.WriteEndObject();
      }
      if (p.StableNote != null)
      {
        writer.WriteString("stable_note", p.StableNote);
      }
      if (p.FlowBrief != null)
      {
        writer.WritePropertyName("flow_brief");
        p.FlowBrief.WriteTo(writer, null);
      }
      if (p.DrawingCode != null)
      {
        writer.WriteString("drawing_code", p.DrawingCode);
      }
      double? audioSeconds = p.AudioSeconds;
      if (audioSeconds != null)
      {
        double valueOrDefault = audioSeconds.GetValueOrDefault();
        writer.WriteNumber("audio_seconds", valueOrDefault);
        writer.WriteString("stt_provider", "gemini");
      }
      if (p.AttachedImages.Count > 0)
      {
        writer.WriteStartArray("attached_images");
        foreach (byte[] array in p.AttachedImages)
        {
          writer.WriteStartObject();
          writer.WriteBase64String("base64", array);
          writer.WriteString("mimeType", "image/jpeg");
          writer.WriteEndObject();
        }
        writer.WriteEndArray();
      }
      if (p.OtherScreens.Count > 0)
      {
        writer.WriteStartArray("other_screens");
        foreach (ScreenCaptureFrame screenCaptureFrame in p.OtherScreens)
        {
          writer.WriteStartObject();
          writer.WriteBase64String("base64", screenCaptureFrame.Jpeg);
          writer.WriteString("mimeType", "image/jpeg");
          writer.WriteNumber("widthInPixels", screenCaptureFrame.PixelWidth);
          writer.WriteNumber("heightInPixels", screenCaptureFrame.PixelHeight);
          writer.WriteEndObject();
        }
        writer.WriteEndArray();
      }
      int? walkthroughStep = p.WalkthroughStep;
      if (walkthroughStep != null)
      {
        int valueOrDefault2 = walkthroughStep.GetValueOrDefault();
        writer.WriteNumber("walkthrough_step", valueOrDefault2);
      }
      writer.WriteString("trigger", p.Trigger);
      string flow = p.Flow;
      if (flow != null && flow.Length > 0)
      {
        writer.WriteString("flow", flow);
      }
      writer.WriteEndObject();
      BrainClient.WriteThread(writer, "turn_items", p.TurnItems);
      writer.WriteString("session_id", p.SessionId);
      writer.WriteNumber("max_rounds", 5);
      writer.WriteBoolean("async_tools", true);
      if (p.TurnToken != null)
      {
        writer.WriteString("turn_token", p.TurnToken);
      }
      if (p.Screenless)
      {
        writer.WriteBoolean("screenless", true);
      }
      Drawing3DAvailability.WriteFlags(writer);
      BrainRequestFlags brainRequestFlags = p.Flags.<Clone>$();
      brainRequestFlags.Drive = (p.Flags.Drive || p.OffersDrive) && !p.IsDrive;
      brainRequestFlags.Write(writer);
      if (p.IsDrive)
      {
        writer.WriteBoolean("takeover", true);
      }
      if (p.IsDrive)
      {
        JsonObject driveBillingFields = p.DriveBillingFields;
        if (driveBillingFields != null)
        {
          foreach (KeyValuePair<string, JsonNode> keyValuePair in driveBillingFields)
          {
            string text;
            JsonNode jsonNode;
            keyValuePair.Deconstruct(out text, out jsonNode);
            string text2 = text;
            JsonNode jsonNode2 = jsonNode;
            writer.WritePropertyName(text2);
            if (jsonNode2 == null)
            {
              writer.WriteNullValue();
            }
            else
            {
              jsonNode2.WriteTo(writer, null);
            }
          }
        }
      }
      if (p.UserLayer != null && p.UserLayerSignature != null)
      {
        writer.WriteString("user_layer", p.UserLayer);
        writer.WriteString("user_layer_signature", p.UserLayerSignature);
      }
      else
      {
        p.UserContext.Write(writer, CompanionSession.IanaTimeZone());
      }
      if (p.SendEnvironment)
      {
        ClickySystemEnvironment.Write(writer, CompanionSession.IanaTimeZone());
      }
      if (!string.IsNullOrEmpty(p.BrainModel))
      {
        writer.WriteString("brain_model", p.BrainModel);
      }
      if (p.AppReads)
      {
        writer.WriteBoolean("app_reads", true);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060005CC RID: 1484 RVA: 0x0035B138 File Offset: 0x0035B138
    private static string IanaTimeZone()
    {
      string text;
      if (!TimeZoneInfo.TryConvertWindowsIdToIanaId(TimeZoneInfo.Local.Id, out text))
      {
        return TimeZoneInfo.Local.Id;
      }
      return text;
    }

    // Token: 0x060005CD RID: 1485 RVA: 0x0035B164 File Offset: 0x0035B164
    private void OnBrainStatus(CompanionSession.NarrationState narration, string status)
    {
      object obj = narration.Gate;
      string text;
      bool heldForAdoption;
      lock (obj)
      {
        if (status.StartsWith("tool:", StringComparison.Ordinal))
        {
          int num = "tool:".Length;
          narration.RunningToolName = status.Substring(num, status.Length - num);
        }
        else if (status.StartsWith("tool_done:", StringComparison.Ordinal))
        {
          string runningToolName = narration.RunningToolName;
          int num = "tool_done:".Length;
          if (runningToolName == status.Substring(num, status.Length - num))
          {
            narration.RunningToolName = null;
          }
        }
        else
        {
          narration.IsSearchingTheWeb = status == "searching";
          narration.IsReadingConnectedApps = status == "reading_apps";
        }
        if (status == "reading_apps" && !narration.ReadConnectedApps)
        {
          narration.ReadConnectedApps = true;
          Log.Info("Companion", "this turn reads the user's connected apps: the drawing's typeText / copyText / openPage / handOff won't run");
        }
        text = CompanionSession.WaitStatusLabel(narration.IsReadingConnectedApps, narration.IsSearchingTheWeb, narration.RunningToolName);
        narration.StatusLabel = text;
        heldForAdoption = narration.HeldForAdoption;
      }
      if (heldForAdoption)
      {
        return;
      }
      this.ShowWaitStatusLabel(text);
    }

    // Token: 0x060005CE RID: 1486 RVA: 0x0035B29C File Offset: 0x0035B29C
    [NullableContext(2)]
    private void ShowWaitStatusLabel(string label)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.waitStatusLabel = label;
      }
      if (this.State == VoiceState.Processing)
      {
        NotchPresenter notchPresenter = this.notch as NotchPresenter;
        if (notchPresenter != null)
        {
          notchPresenter.SetCustomStatusLabel(label);
        }
      }
      this.SignalNarration();
    }

    // Token: 0x060005CF RID: 1487 RVA: 0x0035B304 File Offset: 0x0035B304
    [NullableContext(2)]
    internal static string WaitStatusLabel(bool isReadingConnectedApps, bool isSearchingTheWeb, string runningToolName)
    {
      if (isReadingConnectedApps)
      {
        return CompanionSession.StatusLabelForTool("read_connected_apps", null);
      }
      if (isSearchingTheWeb)
      {
        return CompanionSession.StatusLabelForTool("web_search", null);
      }
      if (runningToolName != null)
      {
        return CompanionSession.StatusLabelForTool(runningToolName, null);
      }
      return null;
    }

    // Token: 0x060005D0 RID: 1488 RVA: 0x0035B330 File Offset: 0x0035B330
    internal static string StatusLabelForTool(string name, [Nullable(2)] string argumentsJson = null)
    {
      if (name == "sessions_spawn" && argumentsJson != null)
      {
        string text = new ToolArgs(argumentsJson).Str(new string[] { "task" });
        if (text != null && SuggestionVoiceAction.Parse(text).Kind == SuggestionVoiceAction.Kind.Skip)
        {
          return "Next suggestion…";
        }
      }
      if (name != null)
      {
        switch (name.Length)
        {
        case 8:
        {
          char c = name[0];
          if (c != 'o')
          {
            if (c == 'r')
            {
              if (name == "routines")
              {
                return "Updating your routines…";
              }
            }
          }
          else if (name == "open_url")
          {
            return "Opening link…";
          }
          break;
        }
        case 9:
        {
          char c = name[5];
          if (c != 'j')
          {
            if (c != 'l')
            {
              if (c == 'r')
              {
                if (name == "file_read")
                {
                  return "Reading file…";
                }
              }
            }
            else if (name == "file_list")
            {
              return "Listing files…";
            }
          }
          else if (name == "find_jobs")
          {
            return "Looking through earlier work…";
          }
          break;
        }
        case 10:
        {
          char c = name[0];
          if (c != 'f')
          {
            if (c != 's')
            {
              if (c == 'w')
              {
                if (name == "web_search")
                {
                  return "Searching the web…";
                }
              }
            }
            else if (name == "shell_exec")
            {
              return "Running command…";
            }
          }
          else if (name == "file_write")
          {
            return "Writing file…";
          }
          break;
        }
        case 11:
        {
          char c = name[0];
          if (c != 'i')
          {
            if (c == 's')
            {
              if (name == "save_memory")
              {
                return "Saving to memory…";
              }
            }
          }
          else if (name == "insert_text")
          {
            return "Typing it in…";
          }
          break;
        }
        case 12:
          if (name == "save_drawing")
          {
            return "Saving the drawing…";
          }
          break;
        case 13:
        {
          char c = name[0];
          if (c != 'c')
          {
            if (c != 'o')
            {
              if (c == 's')
              {
                if (name == "sessions_send")
                {
                  return "Sending to agent…";
                }
              }
            }
            else if (name == "open_artifact")
            {
              return "Opening artifact…";
            }
          }
          else if (name == "clicky_memory")
          {
            return "Checking memory…";
          }
          break;
        }
        case 14:
        {
          char c = name[9];
          if (c <= 's')
          {
            if (c != 'c')
            {
              if (c == 's')
              {
                if (name == "sessions_spawn")
                {
                  return "Starting agent…";
                }
              }
            }
            else if (name == "capture_screen")
            {
              return "Looking at your screen…";
            }
          }
          else if (c != 't')
          {
            if (c == 'y')
            {
              if (name == "sessions_yield")
              {
                return "Checking agent…";
              }
            }
          }
          else
          {
            if (name == "runtime_status")
            {
              return "Checking status…";
            }
            if (name == "session_status")
            {
              return "Checking agents…";
            }
          }
          break;
        }
        case 15:
          if (name == "play_on_spotify")
          {
            return "Playing on Spotify…";
          }
          break;
        case 16:
          if (name == "clicky_artifacts")
          {
            return "Checking its files…";
          }
          break;
        case 17:
          if (name == "copy_to_clipboard")
          {
            return "Copying to clipboard…";
          }
          break;
        case 18:
          if (name == "sessions_interrupt")
          {
            return "Stopping agent…";
          }
          break;
        case 19:
          if (name == "read_connected_apps")
          {
            return "Reading your apps…";
          }
          break;
        case 20:
        {
          char c = name[0];
          if (c != 'o')
          {
            if (c == 's')
            {
              if (name == "send_to_higher_model")
              {
                return "Thinking deeper";
              }
            }
          }
          else if (name == "open_agent_dashboard")
          {
            return "Opening Home…";
          }
          break;
        }
        case 23:
        {
          char c = name[0];
          if (c != 's')
          {
            if (c == 't')
            {
              if (name == "tutorial_draw_on_screen")
              {
                return "Drawing on your screen…";
              }
            }
          }
          else if (name == "sessions_answer_request")
          {
            return "Answering request…";
          }
          break;
        }
        case 25:
          if (name == "skip_onboarding_interview")
          {
            return "Skipping the interview…";
          }
          break;
        }
      }
      return "Working…";
    }

    // Token: 0x060005D1 RID: 1489 RVA: 0x0035B890 File Offset: 0x0035B890
    private void SignalNarration()
    {
      if (this.narrationSignal.CurrentCount < 4)
      {
        this.narrationSignal.Release();
      }
    }

    // Token: 0x060005D2 RID: 1490 RVA: 0x0035B8AC File Offset: 0x0035B8AC
    private async Task WaitForChangeAsync(int milliseconds, CancellationToken ct)
    {
      try
      {
        await this.narrationSignal.WaitAsync(milliseconds, ct).ConfigureAwait(false);
      }
      catch (OperationCanceledException)
      {
      }
    }

    // Token: 0x060005D3 RID: 1491 RVA: 0x0035B900 File Offset: 0x0035B900
    private void OnLineAudioQueued(int lineIndex, double offsetSeconds)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (!this.lineAudioOffsets.ContainsKey(lineIndex))
        {
          this.audioPlaybackBeganCount++;
        }
        this.lineAudioOffsets[lineIndex] = offsetSeconds;
        int? num = this.firstAudioMs;
        if (num == null && lineIndex >= 0)
        {
          this.firstAudioMs = new int?((int)(Environment.TickCount64 - this.turnStartedTicks));
        }
      }
      this.SignalNarration();
    }

    // Token: 0x060005D4 RID: 1492 RVA: 0x0035B998 File Offset: 0x0035B998
    private void OnLineGenerated(int lineIndex, double seconds)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.lineGeneratedSeconds[lineIndex] = seconds;
      }
      if (lineIndex >= 0 && seconds > 0.0)
      {
        this.ui.Post(delegate
        {
          this.canvas.SetLineLength(lineIndex, seconds);
        });
      }
      if (lineIndex >= 0)
      {
        this.NoteLessonLineLength(lineIndex, seconds);
      }
      this.SignalNarration();
    }

    // Token: 0x060005D5 RID: 1493 RVA: 0x0035BA58 File Offset: 0x0035BA58
    private async Task NarrateAsync(CompanionSession.NarrationState narration, CompanionSession.TurnRequest request, Task<bool> mouthOpening, Func<bool> stillCurrent, CompanionSession.TurnTimer metrics, CancellationToken ct)
    {
      await Task.Yield();
      this.canvas.NarrationAudioEndsAt = null;
      HashSet<int> deliveredActions = new HashSet<int>();
      List<string> corrections = new List<string>();
      string words = request.Words;
      long firstLineDeadline = Environment.TickCount64 + 90000L;
      while (stillCurrent())
      {
        object obj = narration.Gate;
        lock (obj)
        {
          if (narration.Lines.Count > narration.FirstLine || narration.Finished || (!request.AnswersInText && narration.EarlyFirstLine != null))
          {
            break;
          }
        }
        if (Environment.TickCount64 > firstLineDeadline)
        {
          break;
        }
        await this.WaitForChangeAsync(250, ct).ConfigureAwait(false);
      }
      if (stillCurrent())
      {
        object obj = narration.Gate;
        int num;
        string failure;
        lock (obj)
        {
          num = Math.Max(0, narration.Lines.Count - narration.FirstLine);
          if (num == 0 && !request.AnswersInText && narration.EarlyFirstLine != null)
          {
            num = 1;
          }
          failure = narration.Failure;
        }
        if (num == 0)
        {
          if (failure == null || failure == "cancelled")
          {
            await this.DeliverActionsAsync(narration, int.MaxValue, deliveredActions, corrections, request, stillCurrent).ConfigureAwait(false);
          }
          else
          {
            await this.TellAsync(failure.StartsWith("HTTP 429", StringComparison.Ordinal) ? "You're asking me a lot right now. Give me a minute, then try again." : (BrainSession.IsSafetyRefusal(failure) ? "My AI provider's safety filter blocked this because it looks like hands-on security work. I can still explain the concepts." : "Sorry, I couldn't get an answer just now. Try asking me again."), request, mouthOpening, stillCurrent).ConfigureAwait(false);
            this.RaiseFailed(new CompanionError("turn_failed", failure, null));
          }
        }
        else
        {
          metrics.FirstSayLineMs = narration.FirstSayLineMs;
          if (request.AnswersInText)
          {
            await this.NarrateInTextAsync(narration, request, deliveredActions, corrections, stillCurrent, ct).ConfigureAwait(false);
          }
          else
          {
            await this.NarrateAloudAsync(narration, request, mouthOpening, deliveredActions, corrections, stillCurrent, metrics, ct).ConfigureAwait(false);
          }
          while (stillCurrent())
          {
            obj = narration.Gate;
            lock (obj)
            {
              if (narration.Finished)
              {
                break;
              }
            }
            await this.WaitForChangeAsync(250, ct).ConfigureAwait(false);
          }
          if (stillCurrent())
          {
            await this.DeliverActionsAsync(narration, int.MaxValue, deliveredActions, corrections, request, stillCurrent).ConfigureAwait(false);
            obj = narration.Gate;
            string failure2;
            lock (obj)
            {
              failure2 = narration.Failure;
            }
            if (BrainSession.IsSafetyRefusal(failure2))
            {
              corrections.Add("My AI provider's safety filter blocked this because it looks like hands-on security work. I can still explain the concepts.");
            }
            foreach (string text in corrections)
            {
              await this.TellAsync(text, request, mouthOpening, stillCurrent).ConfigureAwait(false);
            }
            List<string>.Enumerator enumerator = default(List<string>.Enumerator);
          }
        }
      }
    }

    // Token: 0x060005D6 RID: 1494 RVA: 0x0035BAD0 File Offset: 0x0035BAD0
    private async Task NarrateAloudAsync(CompanionSession.NarrationState narration, CompanionSession.TurnRequest request, Task<bool> mouthOpening, HashSet<int> deliveredActions, List<string> corrections, Func<bool> stillCurrent, CompanionSession.TurnTimer metrics, CancellationToken ct)
    {
      CompanionSession.<>c__DisplayClass166_0 CS$<>8__locals1 = new CompanionSession.<>c__DisplayClass166_0();
      CS$<>8__locals1.narration = narration;
      CS$<>8__locals1.metrics = metrics;
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.request = request;
      ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter = Task.WhenAny(mouthOpening, Task.Delay(8000, ct)).ConfigureAwait(false).GetAwaiter();
      if (!configuredTaskAwaiter.IsCompleted)
      {
        await configuredTaskAwaiter;
        ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
        configuredTaskAwaiter = configuredTaskAwaiter2;
        configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
      }
      bool flag = configuredTaskAwaiter.GetResult() == mouthOpening && mouthOpening.Result;
      if (!flag)
      {
        flag = await this.mouth.EnsureOpenAsync(ct).ConfigureAwait(false);
      }
      if (stillCurrent())
      {
        if (!flag)
        {
          Log.Warn("Companion", "the mouth could not open; answering in text");
          Task<RealtimeProviderUnavailableReason> diagnosis = RealtimeProviderAvailabilityDiagnosis.DiagnoseAsync(this.mouth.LastOpenFailure, null, null);
          CompanionSession.NarrationState narration2 = CS$<>8__locals1.narration;
          CompanionSession.TurnRequest turnRequest = CS$<>8__locals1.request.<Clone>$();
          turnRequest.AnswersInText = true;
          await this.NarrateInTextAsync(narration2, turnRequest, deliveredActions, corrections, stillCurrent, ct).ConfigureAwait(false);
          await this.ExplainMouthOpenFailureAsync(diagnosis, stillCurrent).ConfigureAwait(false);
        }
        else
        {
          object obj = CS$<>8__locals1.narration.Gate;
          lock (obj)
          {
            CS$<>8__locals1.firstLine = CS$<>8__locals1.narration.FirstLine;
          }
          CS$<>8__locals1.requested = CS$<>8__locals1.firstLine;
          List<string> spokenLines = new List<string>();
          string silentSpeaker = (CS$<>8__locals1.request.Typed ? null : this.OutputSilenceKindNow());
          CS$<>8__locals1.earlyFirstLineSpoken = null;
          CS$<>8__locals1.<NarrateAloudAsync>g__SpeakNext|0();
          long handedOverTicks = Environment.TickCount64;
          while (stillCurrent() && Environment.TickCount64 - handedOverTicks < 6000L && this.mouth.LastServerMessageTicks <= handedOverTicks && !this.HasAudioOffset(CS$<>8__locals1.firstLine))
          {
            await this.WaitForChangeAsync(50, ct).ConfigureAwait(false);
          }
          if (stillCurrent() && this.mouth.LastServerMessageTicks <= handedOverTicks && !this.HasAudioOffset(CS$<>8__locals1.firstLine))
          {
            Log.Warn("Companion", "the mouth never answered line 1: replacing its connection");
            ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.mouth.ReplaceAsync(ct).ConfigureAwait(false).GetAwaiter();
            if (!configuredTaskAwaiter3.IsCompleted)
            {
              await configuredTaskAwaiter3;
              ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
              configuredTaskAwaiter3 = configuredTaskAwaiter4;
              configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
            }
            if (configuredTaskAwaiter3.GetResult() && stillCurrent())
            {
              CS$<>8__locals1.requested = CS$<>8__locals1.firstLine;
              CS$<>8__locals1.<NarrateAloudAsync>g__SpeakNext|0();
            }
          }
          CS$<>8__locals1.chatMirrorEnded = false;
          CS$<>8__locals1.heardSoFar = -1;
          CS$<>8__locals1.nextStepAsideCheck = 0L;
          int lineIndex = CS$<>8__locals1.firstLine;
          while (stillCurrent())
          {
            CompanionSession.<>c__DisplayClass166_1 CS$<>8__locals2 = new CompanionSession.<>c__DisplayClass166_1();
            CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
            long lineDeadline = Environment.TickCount64 + ((lineIndex == CS$<>8__locals2.CS$<>8__locals1.firstLine) ? 20000L : 30000L);
            double? offset = null;
            while (stillCurrent() && Environment.TickCount64 < lineDeadline)
            {
              obj = this.gate;
              lock (obj)
              {
                double num;
                double num2;
                if (this.lineAudioOffsets.TryGetValue(lineIndex, out num))
                {
                  offset = new double?(num);
                }
                else if (this.lineGeneratedSeconds.TryGetValue(lineIndex, out num2) && num2 <= 0.0)
                {
                  offset = new double?((double)(-1));
                }
              }
              if (offset != null)
              {
                break;
              }
              CS$<>8__locals2.CS$<>8__locals1.<NarrateAloudAsync>g__StepAsideOnceDrawingStarts|1();
              await this.WaitForChangeAsync(20, ct).ConfigureAwait(false);
            }
            if (offset == null)
            {
              break;
            }
            if (!stillCurrent())
            {
              break;
            }
            while (stillCurrent())
            {
              double? num3 = offset;
              double num4 = 0.0;
              if (!((num3.GetValueOrDefault() >= num4) & (num3 != null)))
              {
                break;
              }
              double num5 = this.audio.PlayedSeconds + 0.02;
              num3 = offset;
              if (!((num5 < num3.GetValueOrDefault()) & (num3 != null)))
              {
                break;
              }
              this.PumpNextLine(CS$<>8__locals2.CS$<>8__locals1.narration, lineIndex, ref CS$<>8__locals2.CS$<>8__locals1.requested, new Action(CS$<>8__locals2.CS$<>8__locals1.<NarrateAloudAsync>g__SpeakNext|0), false);
              CS$<>8__locals2.CS$<>8__locals1.<NarrateAloudAsync>g__StepAsideOnceDrawingStarts|1();
              await this.WaitForChangeAsync(10, ct).ConfigureAwait(false);
            }
            if (!stillCurrent())
            {
              break;
            }
            CS$<>8__locals2.heard = lineIndex;
            CS$<>8__locals2.captionLine = (this.settings.Read<bool>((AppSettings s) => s.DrawingVoiceCaptions) ? CS$<>8__locals2.CS$<>8__locals1.narration.LineText(lineIndex) : null);
            CompanionSession.<>c__DisplayClass166_1 CS$<>8__locals3 = CS$<>8__locals2;
            double num6;
            if (CS$<>8__locals2.captionLine != null)
            {
              num6 = DrawingCaptionTiming.SpokenSeconds(CS$<>8__locals2.captionLine, this.settings.Read<double>((AppSettings s) => s.SpeechSpeed));
            }
            else
            {
              num6 = 0.0;
            }
            CS$<>8__locals3.spokenSeconds = num6;
            this.ui.Post(delegate
            {
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.ReleaseLine(CS$<>8__locals2.heard);
              if (CS$<>8__locals2.captionLine != null)
              {
                CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.ShowLiveCaption(CS$<>8__locals2.captionLine, CS$<>8__locals2.spokenSeconds, CS$<>8__locals2.spokenSeconds + 1.0);
              }
            });
            this.NoteLessonLineHeard(CS$<>8__locals2.heard);
            if (lineIndex == CS$<>8__locals2.CS$<>8__locals1.firstLine)
            {
              CS$<>8__locals2.CS$<>8__locals1.metrics.MarkFirstSpoken();
              this.NoteGuidedStepShowed();
              this.SetState(VoiceState.Responding);
            }
            else if (this.State == VoiceState.Processing)
            {
              this.SetState(VoiceState.Responding);
            }
            string text = CS$<>8__locals2.CS$<>8__locals1.narration.LineText(lineIndex) ?? "";
            if (lineIndex == CS$<>8__locals2.CS$<>8__locals1.firstLine && CS$<>8__locals2.CS$<>8__locals1.earlyFirstLineSpoken != null && text != CS$<>8__locals2.CS$<>8__locals1.earlyFirstLineSpoken)
            {
              Log.Warn("Companion", "line 1 read early from the code differs from the engine's line");
            }
            spokenLines.Add(text);
            obj = CS$<>8__locals2.CS$<>8__locals1.narration.Gate;
            bool flag3;
            lock (obj)
            {
              CS$<>8__locals2.CS$<>8__locals1.narration.HeardLines.Add(text);
              long? firstLineHeardTicks = CS$<>8__locals2.CS$<>8__locals1.narration.FirstLineHeardTicks;
              flag3 = firstLineHeardTicks == null;
              CompanionSession.NarrationState narration3 = CS$<>8__locals2.CS$<>8__locals1.narration;
              narration3.FirstLineHeardTicks.GetValueOrDefault();
              if (narration3.FirstLineHeardTicks == null)
              {
                narration3.FirstLineHeardTicks = new long?(Environment.TickCount64);
              }
            }
            if (flag3)
            {
              CompanionSession.NoteFirstLineHeard(CS$<>8__locals2.CS$<>8__locals1.narration.Model);
            }
            this.SetResponseText(string.Join(" ", spokenLines));
            CS$<>8__locals2.CS$<>8__locals1.heardSoFar = lineIndex;
            if (!CS$<>8__locals2.CS$<>8__locals1.request.Typed)
            {
              Func<ConversationHistory> chatHistory = this.options.ChatHistory;
              if (chatHistory != null)
              {
                ConversationHistory conversationHistory = chatHistory();
                if (conversationHistory != null)
                {
                  conversationHistory.NoteSpokenLineHeard(text);
                }
              }
            }
            if (!CS$<>8__locals2.CS$<>8__locals1.chatMirrorEnded && CS$<>8__locals2.CS$<>8__locals1.request.Typed)
            {
              Func<bool> rendersTextInNotchChat = this.options.RendersTextInNotchChat;
              if (rendersTextInNotchChat != null && rendersTextInNotchChat() && text.Trim().Length > 0)
              {
                string chatText = CompanionSession.NotchChatText(spokenLines);
                string words = CS$<>8__locals2.CS$<>8__locals1.request.Words ?? "";
                this.ui.Post(delegate
                {
                  CS$<>8__locals2.CS$<>8__locals1.<>4__this.notch.Present(new NotchPresentation.TextResponse(words, chatText, false));
                });
              }
            }
            CS$<>8__locals2.CS$<>8__locals1.<NarrateAloudAsync>g__StepAsideOnceDrawingStarts|1();
            await this.DeliverActionsAsync(CS$<>8__locals2.CS$<>8__locals1.narration, lineIndex, deliveredActions, corrections, CS$<>8__locals2.CS$<>8__locals1.request, stillCurrent).ConfigureAwait(false);
            lineIndex++;
            long waitStarted = Environment.TickCount64;
            long lastProgress = waitStarted;
            int programLength = CS$<>8__locals2.CS$<>8__locals1.narration.ProgramSoFar.Length;
            while (stillCurrent())
            {
              obj = CS$<>8__locals2.CS$<>8__locals1.narration.Gate;
              bool flag4;
              bool finished;
              bool flag5;
              lock (obj)
              {
                flag4 = CS$<>8__locals2.CS$<>8__locals1.narration.Lines.Count > lineIndex;
                finished = CS$<>8__locals2.CS$<>8__locals1.narration.Finished;
                flag5 = CS$<>8__locals2.CS$<>8__locals1.narration.ProgramSoFar.Length != programLength || CS$<>8__locals2.CS$<>8__locals1.narration.StatusLabel != null;
                programLength = CS$<>8__locals2.CS$<>8__locals1.narration.ProgramSoFar.Length;
              }
              if (flag4 || finished)
              {
                break;
              }
              if (flag5)
              {
                lastProgress = Environment.TickCount64;
              }
              if (this.audio.Buffered <= TimeSpan.Zero && this.State == VoiceState.Responding)
              {
                this.SetState(VoiceState.Processing);
              }
              if (Environment.TickCount64 - lastProgress > 60000L || Environment.TickCount64 - waitStarted > 300000L)
              {
                break;
              }
              this.PumpNextLine(CS$<>8__locals2.CS$<>8__locals1.narration, lineIndex - 1, ref CS$<>8__locals2.CS$<>8__locals1.requested, new Action(CS$<>8__locals2.CS$<>8__locals1.<NarrateAloudAsync>g__SpeakNext|0), false);
              CS$<>8__locals2.CS$<>8__locals1.<NarrateAloudAsync>g__StepAsideOnceDrawingStarts|1();
              await this.WaitForChangeAsync(50, ct).ConfigureAwait(false);
            }
            obj = CS$<>8__locals2.CS$<>8__locals1.narration.Gate;
            lock (obj)
            {
              if (CS$<>8__locals2.CS$<>8__locals1.narration.Lines.Count <= lineIndex)
              {
                break;
              }
            }
            if (CS$<>8__locals2.CS$<>8__locals1.requested <= lineIndex)
            {
              this.PumpNextLine(CS$<>8__locals2.CS$<>8__locals1.narration, lineIndex - 1, ref CS$<>8__locals2.CS$<>8__locals1.requested, new Action(CS$<>8__locals2.CS$<>8__locals1.<NarrateAloudAsync>g__SpeakNext|0), true);
            }
            CS$<>8__locals2 = null;
          }
          long drainDeadline = Environment.TickCount64 + 60000L;
          while (stillCurrent() && this.audio.Buffered > TimeSpan.Zero && Environment.TickCount64 < drainDeadline)
          {
            if (this.canvas.NarrationAudioEndsAt == null && !this.Mouth.IsResponseInFlight)
            {
              this.canvas.NarrationAudioEndsAt = new DateTime?(DateTime.UtcNow + this.audio.Buffered);
            }
            CS$<>8__locals1.<NarrateAloudAsync>g__StepAsideOnceDrawingStarts|1();
            await this.WaitForChangeAsync(50, ct).ConfigureAwait(false);
          }
          if (stillCurrent() && this.canvas.NarrationAudioEndsAt == null)
          {
            this.canvas.NarrationAudioEndsAt = new DateTime?(DateTime.UtcNow);
          }
          if (!CS$<>8__locals1.request.Typed && silentSpeaker != null && stillCurrent())
          {
            if (this.MutedSpeakerReplyExemptNow())
            {
              Log.Info("Companion", "the speaker was silent, but this was the New Clicky interview or its introduction: no clipboard card");
            }
            else
            {
              this.PresentMutedSpeakerReply(string.Join(" ", spokenLines));
            }
          }
        }
      }
    }

    // Token: 0x060005D7 RID: 1495 RVA: 0x0035BB58 File Offset: 0x0035BB58
    private async Task ExplainMouthOpenFailureAsync([Nullable(new byte[] { 1, 2 })] Task<RealtimeProviderUnavailableReason> diagnosis, Func<bool> stillCurrent)
    {
      RealtimeProviderUnavailableReason reason = null;
      try
      {
        RealtimeProviderUnavailableReason realtimeProviderUnavailableReason = await diagnosis.ConfigureAwait(false);
        reason = realtimeProviderUnavailableReason;
      }
      catch (Exception ex)
      {
        Log.Warn("Companion", "provider diagnosis: " + ex.Message);
      }
      if (reason == null)
      {
        this.RaiseFailed(new CompanionError("voice_unavailable", "My voice connection didn't open, so I wrote the answer instead.", null));
      }
      else
      {
        Log.Warn("Companion", "the mouth can't open where they are: " + reason.AnalyticsKey);
        ClickyAnalytics.ReportInternalError("realtime", "provider_unavailable", null, reason.AnalyticsKey + " surface=push_to_talk", CrashReportLevel.Warning, reason.AnalyticsKey, null);
        this.RaiseFailed(new CompanionError("voice_provider_unavailable", reason.UserFacingMessage, null));
        if (stillCurrent())
        {
          string message = reason.UserFacingMessage;
          this.ui.Post(delegate
          {
            this.cursor.ShowMessageAsync(message, true, 8.0, default(CancellationToken));
          });
        }
      }
    }

    // Token: 0x060005D8 RID: 1496 RVA: 0x0035BBAC File Offset: 0x0035BBAC
    public async Task<bool> AnnounceAsync(string line, bool showAtCursor = true, CancellationToken ct = default(CancellationToken))
    {
      CompanionSession.<>c__DisplayClass168_0 CS$<>8__locals1 = new CompanionSession.<>c__DisplayClass168_0();
      CS$<>8__locals1.ct = ct;
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.line = line;
      CS$<>8__locals1.line = CS$<>8__locals1.line.Trim();
      bool flag;
      if (CS$<>8__locals1.line.Length == 0 || !this.auth.IsSignedIn)
      {
        flag = false;
      }
      else
      {
        long waitDeadline = Environment.TickCount64 + 45000L;
        while (this.State != VoiceState.Idle || Volatile.Read(ref this.activeTurnCount) > 0)
        {
          if (Environment.TickCount64 > waitDeadline)
          {
            Log.Info("Companion", "announcement dropped: HeyClicky stayed busy");
            return false;
          }
          await Task.Delay(150, CS$<>8__locals1.ct).ConfigureAwait(false);
        }
        object obj = this.gate;
        lock (obj)
        {
          CS$<>8__locals1.announcementGeneration = this.generation;
        }
        if (showAtCursor)
        {
          this.ui.Post(delegate
          {
            CS$<>8__locals1.<>4__this.cursor.ShowMessage(CS$<>8__locals1.line);
          });
        }
        ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.mouth.EnsureOpenAsync(CS$<>8__locals1.ct).ConfigureAwait(false).GetAwaiter();
        if (!configuredTaskAwaiter.IsCompleted)
        {
          await configuredTaskAwaiter;
          ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
          configuredTaskAwaiter = configuredTaskAwaiter2;
          configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
        }
        if (!configuredTaskAwaiter.GetResult() || !CS$<>8__locals1.<AnnounceAsync>g__StillCurrent|0())
        {
          Log.Warn("Companion", "announcement not spoken: the mouth didn't open");
          flag = false;
        }
        else
        {
          obj = this.gate;
          lock (obj)
          {
            this.lineAudioOffsets.Remove(-1);
          }
          this.audio.PrepareForUpcomingAudio();
          Log.Info("Companion", "announcing: " + CS$<>8__locals1.line);
          this.mouth.SpeakLine(CS$<>8__locals1.line, -1);
          long startDeadline = Environment.TickCount64 + 10000L;
          while (CS$<>8__locals1.<AnnounceAsync>g__StillCurrent|0() && !this.HasAudioOffset(-1) && Environment.TickCount64 < startDeadline)
          {
            await Task.Delay(30, CS$<>8__locals1.ct).ConfigureAwait(false);
          }
          if (!CS$<>8__locals1.<AnnounceAsync>g__StillCurrent|0() || !this.HasAudioOffset(-1))
          {
            flag = false;
          }
          else
          {
            this.SetState(VoiceState.Responding);
            this.SetResponseText(CS$<>8__locals1.line);
            long playDeadline = Environment.TickCount64 + 60000L;
            await Task.Delay(200, CS$<>8__locals1.ct).ConfigureAwait(false);
            while (CS$<>8__locals1.<AnnounceAsync>g__StillCurrent|0() && this.audio.Buffered > TimeSpan.Zero && Environment.TickCount64 < playDeadline)
            {
              await Task.Delay(60, CS$<>8__locals1.ct).ConfigureAwait(false);
            }
            if (Interlocked.Read(ref this.generation) == CS$<>8__locals1.announcementGeneration && this.State == VoiceState.Responding)
            {
              this.SetState(VoiceState.Idle);
            }
            flag = true;
          }
        }
      }
      return flag;
    }

    // Token: 0x060005D9 RID: 1497 RVA: 0x0035BC08 File Offset: 0x0035BC08
    private void PresentMutedSpeakerReply(string answer)
    {
      string text = answer.Trim();
      if (text.Length == 0)
      {
        return;
      }
      if (!this.options.DesktopActions.CopyToClipboard(text))
      {
        return;
      }
      string text2 = this.OutputSilenceKindNow() ?? "sound_came_back";
      string text3 = "Companion";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(66, 2);
      defaultInterpolatedStringHandler.AppendLiteral("the speaker was silent (");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral("): the answer (");
      defaultInterpolatedStringHandler.AppendFormatted<int>(text.Length);
      defaultInterpolatedStringHandler.AppendLiteral(" chars) is on the clipboard");
      Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
      this.NotifyTerminating();
      this.ShowLiveEvent(new NotchLiveEvent.VoiceReplyMutedCopiedToClipboard(text2));
    }

    // Token: 0x060005DA RID: 1498 RVA: 0x0035BCB0 File Offset: 0x0035BCB0
    private bool MutedSpeakerReplyExemptNow()
    {
      bool flag;
      try
      {
        Func<bool> mutedSpeakerReplyExempt = this.options.MutedSpeakerReplyExempt;
        flag = mutedSpeakerReplyExempt != null && mutedSpeakerReplyExempt();
      }
      catch (Exception ex)
      {
        Log.Warn("Companion", "muted-speaker exemption: " + ex.Message);
        flag = false;
      }
      return flag;
    }

    // Token: 0x060005DB RID: 1499 RVA: 0x0035BD08 File Offset: 0x0035BD08
    [NullableContext(2)]
    private string OutputSilenceKindNow()
    {
      string text;
      try
      {
        Func<string> outputSilenceKind = this.options.OutputSilenceKind;
        text = ((outputSilenceKind != null) ? outputSilenceKind() : null);
      }
      catch (Exception ex)
      {
        Log.Warn("Companion", "output volume: " + ex.Message);
        text = null;
      }
      return text;
    }

    // Token: 0x060005DC RID: 1500 RVA: 0x0035BD60 File Offset: 0x0035BD60
    private void PumpNextLine(CompanionSession.NarrationState narration, int currentLine, ref int requested, Action speakNext, bool force = false)
    {
      object obj = narration.Gate;
      bool flag2;
      lock (obj)
      {
        flag2 = narration.Lines.Count > requested;
      }
      if (!flag2 || requested != currentLine + 1)
      {
        return;
      }
      obj = this.gate;
      bool flag3;
      lock (obj)
      {
        flag3 = this.lineGeneratedSeconds.ContainsKey(currentLine);
      }
      if (flag3 || force)
      {
        speakNext();
      }
    }

    // Token: 0x060005DD RID: 1501 RVA: 0x0035BDF8 File Offset: 0x0035BDF8
    private bool HasAudioOffset(int line)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = this.lineAudioOffsets.ContainsKey(line);
      }
      return flag2;
    }

    // Token: 0x1400000B RID: 11
    // (add) Token: 0x060005DE RID: 1502 RVA: 0x0035BE40 File Offset: 0x0035BE40
    // (remove) Token: 0x060005DF RID: 1503 RVA: 0x0035BE78 File Offset: 0x0035BE78
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler TextAnswerHandedToCaptions;

    // Token: 0x060005E0 RID: 1504 RVA: 0x0035BEB0 File Offset: 0x0035BEB0
    public void MoveTypedAnswerToCaptions()
    {
      Log.Info("Companion", "the chat was put away mid-answer: the rest of the typed answer shows as captions");
      this.typedAnswerCaptionRequested = true;
    }

    // Token: 0x060005E1 RID: 1505 RVA: 0x0035BECC File Offset: 0x0035BECC
    private bool ConsumeTypedAnswerCaptionRequest()
    {
      if (!this.typedAnswerCaptionRequested)
      {
        return false;
      }
      this.typedAnswerCaptionRequested = false;
      return true;
    }

    // Token: 0x060005E2 RID: 1506 RVA: 0x0035BEE4 File Offset: 0x0035BEE4
    private void HandTextAnswerToDrawingCaption()
    {
      Log.Info("Companion", "the typed answer draws on the screen: its lines show as captions");
      this.notch.ReturnToIdle();
      EventHandler textAnswerHandedToCaptions = this.TextAnswerHandedToCaptions;
      if (textAnswerHandedToCaptions == null)
      {
        return;
      }
      textAnswerHandedToCaptions(this, EventArgs.Empty);
    }

    // Token: 0x060005E3 RID: 1507 RVA: 0x0035BF18 File Offset: 0x0035BF18
    internal async Task NarrateInTextAsync(CompanionSession.NarrationState narration, CompanionSession.TurnRequest request, HashSet<int> deliveredActions, List<string> corrections, Func<bool> stillCurrent, CancellationToken ct)
    {
      CompanionSession.<>c__DisplayClass182_0 CS$<>8__locals1 = new CompanionSession.<>c__DisplayClass182_0();
      CS$<>8__locals1.<>4__this = this;
      this.captionsTextAnswer = false;
      List<string> shown = new List<string>();
      CS$<>8__locals1.lastShownLine = null;
      CS$<>8__locals1.question = request.Words ?? "";
      object obj = narration.Gate;
      int lineIndex;
      lock (obj)
      {
        lineIndex = narration.FirstLine;
      }
      int firstLine = lineIndex;
      while (stillCurrent())
      {
        CompanionSession.<>c__DisplayClass182_1 CS$<>8__locals2 = new CompanionSession.<>c__DisplayClass182_1();
        CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
        CS$<>8__locals2.line = null;
        obj = narration.Gate;
        bool finished;
        lock (obj)
        {
          if (narration.Lines.Count > lineIndex)
          {
            CS$<>8__locals2.line = narration.Lines[lineIndex];
          }
          finished = narration.Finished;
        }
        if (CS$<>8__locals2.line == null)
        {
          if (finished)
          {
            break;
          }
          CS$<>8__locals2.CS$<>8__locals1.<NarrateInTextAsync>g__StepAsideOnceDrawingStarts|0();
          await this.WaitForChangeAsync(50, ct).ConfigureAwait(false);
        }
        else
        {
          CS$<>8__locals2.CS$<>8__locals1.<NarrateInTextAsync>g__AdoptCaptionRequest|1();
          shown.Add(CS$<>8__locals2.line);
          obj = narration.Gate;
          lock (obj)
          {
            narration.HeardLines.Add(CS$<>8__locals2.line);
            long? firstLineHeardTicks = narration.FirstLineHeardTicks;
            if (firstLineHeardTicks == null)
            {
              CompanionSession.NoteFirstLineHeard(narration.Model);
            }
            narration.FirstLineHeardTicks.GetValueOrDefault();
            if (narration.FirstLineHeardTicks == null)
            {
              narration.FirstLineHeardTicks = new long?(Environment.TickCount64);
            }
          }
          CompanionSession.<>c__DisplayClass182_1 CS$<>8__locals3 = CS$<>8__locals2;
          Func<bool> rendersTextInNotchChat = this.options.RendersTextInNotchChat;
          CS$<>8__locals3.answer = ((rendersTextInNotchChat != null && rendersTextInNotchChat()) ? CompanionSession.NotchChatText(shown) : string.Join(" ", shown));
          CS$<>8__locals2.index = lineIndex;
          CS$<>8__locals2.reading = CompanionSession.ReadingSeconds(CS$<>8__locals2.line);
          CS$<>8__locals2.enteringCaptions = !this.captionsTextAnswer && this.canvas.InkedMarkCount > 0;
          if (CS$<>8__locals2.enteringCaptions)
          {
            this.captionsTextAnswer = true;
          }
          CS$<>8__locals2.captioned = this.captionsTextAnswer;
          this.ui.Post(delegate
          {
            CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.SetLineLength(CS$<>8__locals2.index, CS$<>8__locals2.reading);
            CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.ReleaseLine(CS$<>8__locals2.index);
            if (CS$<>8__locals2.enteringCaptions)
            {
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.HandTextAnswerToDrawingCaption();
            }
            if (CS$<>8__locals2.captioned)
            {
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.canvas.ShowLiveCaption(CS$<>8__locals2.line, DrawingCaptionTiming.RevealSeconds(CS$<>8__locals2.line, CS$<>8__locals2.reading), CS$<>8__locals2.reading + 1.2);
              return;
            }
            CS$<>8__locals2.CS$<>8__locals1.<>4__this.notch.Present(new NotchPresentation.TextResponse(CS$<>8__locals2.CS$<>8__locals1.question, CS$<>8__locals2.answer, false));
          });
          if (lineIndex == firstLine)
          {
            this.NoteGuidedStepShowed();
            this.SetState(VoiceState.Responding);
          }
          this.SetResponseText(CS$<>8__locals2.answer);
          CS$<>8__locals2.CS$<>8__locals1.lastShownLine = CS$<>8__locals2.line;
          await this.DeliverActionsAsync(narration, lineIndex, deliveredActions, corrections, request, stillCurrent).ConfigureAwait(false);
          lineIndex++;
          long readUntil = Environment.TickCount64 + (long)(CS$<>8__locals2.reading * 1000.0);
          while (Environment.TickCount64 < readUntil)
          {
            CS$<>8__locals2.CS$<>8__locals1.<NarrateInTextAsync>g__StepAsideOnceDrawingStarts|0();
            try
            {
              await Task.Delay(TimeSpan.FromMilliseconds(Math.Min(40L, Math.Max(1L, readUntil - Environment.TickCount64)), 0L), ct).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
              return;
            }
          }
          CS$<>8__locals2 = null;
        }
      }
    }

    // Token: 0x060005E4 RID: 1508 RVA: 0x0035BF90 File Offset: 0x0035BF90
    public static string NotchChatText(IEnumerable<string> lines)
    {
      return string.Concat(lines.Select<string, string>((string l) => l.Trim() + "\n\n"));
    }

    // Token: 0x060005E5 RID: 1509 RVA: 0x0035BFBC File Offset: 0x0035BFBC
    public static double ReadingSeconds(string line)
    {
      int num = line.Split(null, StringSplitOptions.RemoveEmptyEntries).Length;
      return Math.Min(6.0, Math.Max(1.6, 0.8 + (double)num * 0.28));
    }

    // Token: 0x060005E6 RID: 1510 RVA: 0x0035C008 File Offset: 0x0035C008
    private async Task TellAsync(string line, CompanionSession.TurnRequest request, Task<bool> mouthOpening, Func<bool> stillCurrent)
    {
      if (stillCurrent())
      {
        this.SetResponseText(line);
        if (request.AnswersInText)
        {
          if (this.ConsumeTypedAnswerCaptionRequest())
          {
            this.captionsTextAnswer = true;
          }
          if (this.captionsTextAnswer)
          {
            double reading = CompanionSession.ReadingSeconds(line);
            this.ui.Post(delegate
            {
              this.canvas.ShowLiveCaption(line, DrawingCaptionTiming.RevealSeconds(line, reading), reading + 1.2);
            });
          }
          else
          {
            this.ui.Post(delegate
            {
              this.notch.Present(new NotchPresentation.TextResponse(request.Words ?? "", line, false));
            });
          }
        }
        else
        {
          bool flag = mouthOpening.IsCompletedSuccessfully && mouthOpening.Result;
          if (!flag)
          {
            flag = await this.mouth.EnsureOpenAsync(default(CancellationToken)).ConfigureAwait(false);
          }
          if (flag)
          {
            this.mouth.SpeakLine(line, -1);
            if (!request.Typed)
            {
              Func<ConversationHistory> chatHistory = this.options.ChatHistory;
              if (chatHistory != null)
              {
                ConversationHistory conversationHistory = chatHistory();
                if (conversationHistory != null)
                {
                  conversationHistory.NoteSpokenLineHeard(line);
                }
              }
            }
          }
          else
          {
            this.ui.Post(delegate
            {
              this.notch.Present(new NotchPresentation.TextResponse(request.Words ?? "", line, false));
            });
          }
        }
      }
    }

    // Token: 0x060005E7 RID: 1511 RVA: 0x0035C06C File Offset: 0x0035C06C
    private async Task DeliverActionsAsync(CompanionSession.NarrationState narration, int throughLine, HashSet<int> delivered, List<string> corrections, CompanionSession.TurnRequest request, Func<bool> stillCurrent)
    {
      object obj = narration.Gate;
      List<DrawingAction> actions;
      lock (obj)
      {
        actions = narration.Actions;
      }
      for (int i = 0; i < actions.Count; i++)
      {
        DrawingAction drawingAction = actions[i];
        if (!delivered.Contains(i) && CompanionSession.ActionLine(drawingAction) <= throughLine)
        {
          delivered.Add(i);
          await this.PerformDrawingActionAsync(drawingAction, corrections, request, narration, stillCurrent).ConfigureAwait(false);
        }
      }
    }

    // Token: 0x060005E8 RID: 1512 RVA: 0x0035C0E4 File Offset: 0x0035C0E4
    private static int ActionLine(DrawingAction action)
    {
      JsonElement jsonElement;
      int num;
      if (action.Payload.ValueKind != JsonValueKind.Object || !action.Payload.TryGetProperty("line", out jsonElement) || !jsonElement.TryGetInt32(out num))
      {
        return 0;
      }
      return num;
    }

    // Token: 0x060005E9 RID: 1513 RVA: 0x0035C128 File Offset: 0x0035C128
    [return: Nullable(2)]
    private static string PayloadString(JsonElement payload, params string[] names)
    {
      if (payload.ValueKind == JsonValueKind.String)
      {
        return payload.GetString();
      }
      if (payload.ValueKind != JsonValueKind.Object)
      {
        return null;
      }
      foreach (string text in names)
      {
        JsonElement jsonElement;
        if (payload.TryGetProperty(text, out jsonElement) && jsonElement.ValueKind == JsonValueKind.String)
        {
          return jsonElement.GetString();
        }
      }
      return null;
    }

    // Token: 0x060005EA RID: 1514 RVA: 0x0035C188 File Offset: 0x0035C188
    private async Task PerformDrawingActionAsync(DrawingAction action, List<string> corrections, CompanionSession.TurnRequest request, CompanionSession.NarrationState narration, Func<bool> stillCurrent)
    {
      IDesktopActions desktopActions = this.options.DesktopActions;
      object obj = narration.Gate;
      bool readConnectedApps;
      lock (obj)
      {
        readConnectedApps = narration.ReadConnectedApps;
      }
      bool flag = readConnectedApps;
      if (flag)
      {
        string text = action.Kind;
        bool flag2 = text == "type" || text == "copy" || text == "open" || text == "handoff";
        flag = flag2;
      }
      if (flag)
      {
        string text2 = "Companion";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(54, 2);
        defaultInterpolatedStringHandler.AppendFormatted(action.Kind);
        defaultInterpolatedStringHandler.AppendLiteral(" in beat ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(CompanionSession.ActionLine(action) + 1);
        defaultInterpolatedStringHandler.AppendLiteral(" not run: this turn read their connected apps");
        Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
        if (!corrections.Contains("I didn't do that part, since I'd just read your apps. Ask me again and I'll do it."))
        {
          corrections.Add("I didn't do that part, since I'd just read your apps. Ask me again and I'll do it.");
        }
      }
      else
      {
        string text = action.Kind;
        if (text != null)
        {
          switch (text.Length)
          {
          case 4:
          {
            char c = text[0];
            if (c != 'c')
            {
              if (c != 'o')
              {
                if (c != 't')
                {
                  goto IL_02B7;
                }
                if (!(text == "type"))
                {
                  goto IL_02B7;
                }
              }
              else if (!(text == "open"))
              {
                goto IL_02B7;
              }
            }
            else if (!(text == "copy"))
            {
              goto IL_02B7;
            }
            break;
          }
          case 5:
            goto IL_02B7;
          case 6:
            if (!(text == "button"))
            {
              goto IL_02B7;
            }
            break;
          case 7:
          {
            char c = text[0];
            if (c != 'h')
            {
              if (c != 't')
              {
                goto IL_02B7;
              }
              if (!(text == "tourjob"))
              {
                goto IL_02B7;
              }
            }
            else if (!(text == "handoff"))
            {
              goto IL_02B7;
            }
            break;
          }
          case 8:
          {
            char c = text[4];
            if (c != 'a')
            {
              if (c != 'f')
              {
                if (c != 't')
                {
                  goto IL_02B7;
                }
                if (!(text == "tourtuck"))
                {
                  goto IL_02B7;
                }
              }
              else if (!(text == "tourfade"))
              {
                goto IL_02B7;
              }
            }
            else if (!(text == "tourasks"))
            {
              goto IL_02B7;
            }
            break;
          }
          case 9:
            if (!(text == "tournotch"))
            {
              goto IL_02B7;
            }
            break;
          default:
            goto IL_02B7;
          }
          flag = true;
          goto IL_02BA;
        }
        IL_02B7:
        flag = false;
        IL_02BA:
        if (flag)
        {
          obj = narration.Gate;
          lock (obj)
          {
            narration.TextDeliveries++;
          }
        }
        text = action.Kind;
        string typeText;
        if (text != null)
        {
          switch (text.Length)
          {
          case 4:
          {
            char c = text[0];
            if (c <= 'l')
            {
              if (c != 'c')
              {
                if (c != 'l')
                {
                  goto IL_0A51;
                }
                if (!(text == "link"))
                {
                  goto IL_0A51;
                }
                goto IL_0A69;
              }
              else
              {
                if (!(text == "copy"))
                {
                  goto IL_0A51;
                }
                string text3 = CompanionSession.PayloadString(action.Payload, new string[] { "text" });
                bool flag3 = !string.IsNullOrEmpty(text3) && desktopActions.CopyToClipboard(text3);
                if (!string.IsNullOrEmpty(text3))
                {
                  ClickyAnalytics.TrackClipboardCopyCompleted("screen_drawing", text3.Length, ClickyAnalytics.WordCount(text3), flag3);
                }
                if (!flag3)
                {
                  corrections.Add("The clipboard copy didn't go through, so ask me again if you need that text.");
                  goto IL_0A69;
                }
                this.RaiseTextDelivered(text3, null, true);
                goto IL_0A69;
              }
            }
            else if (c != 'o')
            {
              if (c != 't')
              {
                goto IL_0A51;
              }
              if (!(text == "type"))
              {
                goto IL_0A51;
              }
              typeText = CompanionSession.PayloadString(action.Payload, new string[] { "text" });
              if (string.IsNullOrEmpty(typeText))
              {
                goto IL_0A69;
              }
              TextInsertOutcome textInsertOutcome = await desktopActions.InsertIntoFocusedFieldAsync(CompanionSession.TextForInsertion(typeText)).ConfigureAwait(false);
              Log.Info("Companion", "typeText: " + textInsertOutcome.Status + ((textInsertOutcome.Reason == null) ? "" : (" (" + textInsertOutcome.Reason + ")")));
              ClickyAnalytics.TrackTextInjectionCompleted(ClickyAnalytics.TextInjectionOutcomeName(textInsertOutcome.Status, textInsertOutcome.Reason), typeText.Length, ClickyAnalytics.WordCount(typeText), "screen_drawing", (textInsertOutcome.Status == "inserted") ? textInsertOutcome.App : null, (textInsertOutcome.Status == "failed") ? textInsertOutcome.Reason : null);
              if (textInsertOutcome.Status == "inserted")
              {
                this.RaiseTextDelivered(typeText, textInsertOutcome.App, false);
              }
              else if (textInsertOutcome.Status == "copied_to_clipboard")
              {
                this.RaiseTextDelivered(typeText, null, true);
              }
              if (textInsertOutcome.Status == "copied_to_clipboard")
              {
                corrections.Add("I couldn't drop that straight into the field, so it's on your clipboard. Press Control V to paste it.");
                goto IL_0A69;
              }
              if (textInsertOutcome.Status == "unconfirmed")
              {
                corrections.Add("I pasted it, but I couldn't see it show up in the field, so check it's there.");
                goto IL_0A69;
              }
              if (textInsertOutcome.Status == "refused" && textInsertOutcome.Reason == "secure_field")
              {
                corrections.Add("I didn't fill that in, it's a password field, so type that one yourself.");
                goto IL_0A69;
              }
              if (textInsertOutcome.Status != "inserted")
              {
                corrections.Add("I didn't fill that in, that field didn't look safe to type into.");
                goto IL_0A69;
              }
              goto IL_0A69;
            }
            else
            {
              if (!(text == "open"))
              {
                goto IL_0A51;
              }
              string text4 = CompanionSession.PayloadString(action.Payload, new string[] { "text", "url" });
              if (text4 == null || !desktopActions.OpenUrl(text4))
              {
                corrections.Add("I couldn't open that page, so ask me again and I'll try another link.");
                goto IL_0A69;
              }
              goto IL_0A69;
            }
            break;
          }
          case 5:
          {
            char c = text[2];
            if (c != 'i')
            {
              if (c != 'o')
              {
                if (c != 'v')
                {
                  goto IL_0A51;
                }
                if (!(text == "hover"))
                {
                  goto IL_0A51;
                }
              }
              else
              {
                if (!(text == "photo"))
                {
                  goto IL_0A51;
                }
                goto IL_0A69;
              }
            }
            else if (!(text == "point"))
            {
              goto IL_0A51;
            }
            break;
          }
          case 6:
          {
            char c = text[0];
            if (c != 'b')
            {
              if (c != 't')
              {
                goto IL_0A51;
              }
              if (!(text == "target"))
              {
                goto IL_0A51;
              }
            }
            else
            {
              if (!(text == "button"))
              {
                goto IL_0A51;
              }
              goto IL_0A69;
            }
            break;
          }
          case 7:
            if (!(text == "handoff"))
            {
              goto IL_0A51;
            }
            await this.StartDrawingHandOffAsync(action, corrections, narration).ConfigureAwait(false);
            goto IL_0A69;
          default:
            goto IL_0A51;
          }
          PointD point;
          if (!CompanionSession.TryScreenPoint(action.Payload, request.Capture, out point))
          {
            goto IL_0A69;
          }
          string label = CompanionSession.PayloadString(action.Payload, new string[] { "label" });
          RectD? rectD = null;
          bool armsClickTarget = action.Kind != "point" && !request.PointsOnly;
          RectD rectD2;
          DisplayInfo displayInfo;
          if (armsClickTarget && CompanionSession.TryScreenBox(action.Payload, request.Capture, out rectD2, out displayInfo))
          {
            ValueTuple<PointD, RectD> valueTuple = await this.SnappedTargetAsync(rectD2, displayInfo, label, point).ConfigureAwait(false);
            RectD? rectD3 = new RectD?(valueTuple.Item2);
            point = valueTuple.Item1;
            rectD = rectD3;
          }
          this.ui.Post(delegate
          {
            this.cursor.PointAtAsync(point, label, default(CancellationToken));
          });
          if (armsClickTarget)
          {
            await this.ArmGuidedTargetIfLastBeatAsync(narration, action, point, rectD, request, stillCurrent).ConfigureAwait(false);
            goto IL_0A69;
          }
          goto IL_0A69;
        }
        IL_0A51:
        EventHandler<DrawingAction> unhandledDrawingAction = this.UnhandledDrawingAction;
        if (unhandledDrawingAction != null)
        {
          unhandledDrawingAction(this, action);
        }
        IL_0A69:
        typeText = null;
      }
    }

    // Token: 0x060005EB RID: 1515 RVA: 0x0035C1F8 File Offset: 0x0035C1F8
    private async Task StartDrawingHandOffAsync(DrawingAction action, List<string> corrections, CompanionSession.NarrationState narration)
    {
      object obj = narration.Gate;
      CompanionToolContext toolContext;
      lock (obj)
      {
        toolContext = narration.ToolContext;
      }
      string text = CompanionSession.PayloadString(action.Payload, new string[] { "text" });
      Func<BrainToolCall, CompanionToolContext, CancellationToken, Task<string>> toolCallHandler = this.options.ToolCallHandler;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (toolCallHandler != null && text != null)
      {
        string text2 = AgentTools.HandOffArguments(text);
        if (text2 != null)
        {
          ToolArgs toolArgs = new ToolArgs(text2);
          string task = toolArgs.Str(new string[] { "task" }) ?? "";
          string text3 = "Companion";
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 2);
          defaultInterpolatedStringHandler.AppendLiteral("handOff in beat ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(CompanionSession.ActionLine(action) + 1);
          defaultInterpolatedStringHandler.AppendLiteral(": ");
          defaultInterpolatedStringHandler.AppendFormatted(toolArgs.Str(new string[] { "label" }) ?? "");
          Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
          string text4;
          try
          {
            text4 = await toolCallHandler(new BrainToolCall("drawing-handoff-" + Guid.NewGuid().ToString("N").Substring(0, 12), "sessions_spawn", text2, true), toolContext ?? CompanionToolContext.None, CancellationToken.None).ConfigureAwait(false);
          }
          catch (Exception ex)
          {
            text4 = ToolOutput.Error(ex.Message);
          }
          ToolArgs toolArgs2 = new ToolArgs(text4);
          string text5 = toolArgs2.Str(new string[] { "status" });
          bool flag = text5 == "dispatched" || text5 == "founded";
          if (flag)
          {
            string text6 = toolArgs2.Str(new string[] { "clicky_name" }) ?? "a Clicky";
            string text7 = toolArgs2.Str(new string[] { "clicky_slug" });
            string text8 = ((text7 != null) ? (" (clicky_slug: " + text7 + ")") : "");
            BrainSession brainSession = this.brain;
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(154, 3);
            defaultInterpolatedStringHandler.AppendLiteral("[clicky drawing] The drawing handed this work to ");
            defaultInterpolatedStringHandler.AppendFormatted(text6);
            defaultInterpolatedStringHandler.AppendFormatted(text8);
            defaultInterpolatedStringHandler.AppendLiteral(", which started it: ");
            defaultInterpolatedStringHandler.AppendFormatted((task.Length > 300) ? task.Substring(0, 300) : task);
            defaultInterpolatedStringHandler.AppendLiteral(". It is already running: never start it again; follow-ups about it go to that Clicky.");
            brainSession.NoteForNextTurn(defaultInterpolatedStringHandler.ToStringAndClear(), null);
          }
          else
          {
            Log.Info("Companion", "handOff not started: " + text4);
            corrections.Add("I couldn't start a job for that, so nothing was started yet. Ask me again in a moment.");
          }
          return;
        }
      }
      string text9 = "Companion";
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 2);
      defaultInterpolatedStringHandler.AppendLiteral("handOff in beat ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(CompanionSession.ActionLine(action) + 1);
      defaultInterpolatedStringHandler.AppendLiteral(" not started: ");
      defaultInterpolatedStringHandler.AppendFormatted((this.options.ToolCallHandler == null) ? "no dispatcher" : "unreadable payload");
      Log.Info(text9, defaultInterpolatedStringHandler.ToStringAndClear());
      corrections.Add("I couldn't start a job for that, so nothing was started. Ask me again and I'll start one.");
    }

    // Token: 0x060005EC RID: 1516 RVA: 0x0035C254 File Offset: 0x0035C254
    [NullableContext(2)]
    private static bool TryScreenPoint(JsonElement payload, CompanionSession.TurnCapture capture, out PointD point)
    {
      point = default(PointD);
      if (payload.ValueKind != JsonValueKind.Object || capture == null || capture.Frames.Count == 0)
      {
        return false;
      }
      JsonElement jsonElement;
      JsonElement jsonElement2;
      double num;
      double num2;
      if (!payload.TryGetProperty("x", out jsonElement) || !payload.TryGetProperty("y", out jsonElement2) || !jsonElement.TryGetDouble(out num) || !jsonElement2.TryGetDouble(out num2))
      {
        return false;
      }
      ScreenCaptureFrame screenCaptureFrame = capture.Frames.FirstOrDefault<ScreenCaptureFrame>((ScreenCaptureFrame f) => f.ContainsCursor) ?? capture.Frames[0];
      RectD boundsPx = screenCaptureFrame.Display.BoundsPx;
      point = new PointD(boundsPx.X + num * boundsPx.Width / (double)Math.Max(1, screenCaptureFrame.PixelWidth), boundsPx.Y + num2 * boundsPx.Height / (double)Math.Max(1, screenCaptureFrame.PixelHeight));
      return true;
    }

    // Token: 0x060005ED RID: 1517 RVA: 0x0035C354 File Offset: 0x0035C354
    private static bool TryScreenBox(JsonElement payload, [Nullable(2)] CompanionSession.TurnCapture capture, out RectD box, out DisplayInfo display)
    {
      box = default(RectD);
      display = null;
      if (payload.ValueKind != JsonValueKind.Object || capture == null || capture.Frames.Count == 0)
      {
        return false;
      }
      JsonElement jsonElement;
      if (!payload.TryGetProperty("box", out jsonElement) || jsonElement.ValueKind != JsonValueKind.Array || jsonElement.GetArrayLength() != 4)
      {
        return false;
      }
      double[] array = new double[4];
      for (int i = 0; i < 4; i++)
      {
        if (!jsonElement[i].TryGetDouble(out array[i]))
        {
          return false;
        }
      }
      ScreenCaptureFrame screenCaptureFrame = capture.Frames.FirstOrDefault<ScreenCaptureFrame>((ScreenCaptureFrame f) => f.ContainsCursor) ?? capture.Frames[0];
      RectD boundsPx = screenCaptureFrame.Display.BoundsPx;
      double num = boundsPx.Width / (double)Math.Max(1, screenCaptureFrame.PixelWidth);
      double num2 = boundsPx.Height / (double)Math.Max(1, screenCaptureFrame.PixelHeight);
      box = new RectD(boundsPx.X + array[0] * num, boundsPx.Y + array[1] * num2, array[2] * num, array[3] * num2);
      display = screenCaptureFrame.Display;
      return box.Width > 0.0 && box.Height > 0.0;
    }

    // Token: 0x060005EE RID: 1518 RVA: 0x0035C4B8 File Offset: 0x0035C4B8
    [return: TupleElementNames(new string[] { "Point", "Frame" })]
    [return: Nullable(new byte[] { 1, 0 })]
    private async Task<ValueTuple<PointD, RectD>> SnappedTargetAsync(RectD modelBox, DisplayInfo display, [Nullable(2)] string label, PointD fallback)
    {
      Func<RectD, DisplayInfo, CancellationToken, Task<RectD?>> snapTargetToControl = this.options.SnapTargetToControl;
      ValueTuple<PointD, RectD> valueTuple;
      if (snapTargetToControl == null)
      {
        valueTuple = new ValueTuple<PointD, RectD>(fallback, modelBox);
      }
      else
      {
        long started = Environment.TickCount64;
        RectD? control = null;
        try
        {
          RectD? rectD = await snapTargetToControl(modelBox, display, CancellationToken.None).ConfigureAwait(false);
          control = rectD;
        }
        catch (Exception ex)
        {
          Log.Warn("Companion", "target snap failed: " + ex.Message);
        }
        long num = Environment.TickCount64 - started;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
        defaultInterpolatedStringHandler.AppendFormatted<double>(modelBox.Width, "0");
        defaultInterpolatedStringHandler.AppendLiteral("x");
        defaultInterpolatedStringHandler.AppendFormatted<double>(modelBox.Height, "0");
        string text = defaultInterpolatedStringHandler.ToStringAndClear();
        if (control != null)
        {
          RectD valueOrDefault = control.GetValueOrDefault();
          string text2 = "Companion";
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(59, 5);
          defaultInterpolatedStringHandler.AppendLiteral("target \"");
          defaultInterpolatedStringHandler.AppendFormatted(label);
          defaultInterpolatedStringHandler.AppendLiteral("\": snapped to the control ");
          defaultInterpolatedStringHandler.AppendFormatted<double>(valueOrDefault.Width, "0");
          defaultInterpolatedStringHandler.AppendLiteral("x");
          defaultInterpolatedStringHandler.AppendFormatted<double>(valueOrDefault.Height, "0");
          defaultInterpolatedStringHandler.AppendLiteral(" from the model's ");
          defaultInterpolatedStringHandler.AppendFormatted(text);
          defaultInterpolatedStringHandler.AppendLiteral(" (");
          defaultInterpolatedStringHandler.AppendFormatted<long>(num);
          defaultInterpolatedStringHandler.AppendLiteral(" ms)");
          Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
          valueTuple = new ValueTuple<PointD, RectD>(new PointD(valueOrDefault.X + valueOrDefault.Width / 2.0, valueOrDefault.Y + valueOrDefault.Height / 2.0), valueOrDefault);
        }
        else
        {
          string text3 = "Companion";
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(56, 3);
          defaultInterpolatedStringHandler.AppendLiteral("target \"");
          defaultInterpolatedStringHandler.AppendFormatted(label);
          defaultInterpolatedStringHandler.AppendLiteral("\": kept the model's box ");
          defaultInterpolatedStringHandler.AppendFormatted(text);
          defaultInterpolatedStringHandler.AppendLiteral(", no control found (");
          defaultInterpolatedStringHandler.AppendFormatted<long>(num);
          defaultInterpolatedStringHandler.AppendLiteral(" ms)");
          Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
          valueTuple = new ValueTuple<PointD, RectD>(fallback, modelBox);
        }
      }
      return valueTuple;
    }

    // Token: 0x060005EF RID: 1519 RVA: 0x0035C51C File Offset: 0x0035C51C
    private void OnCanvasActionRequested([Nullable(2)] object sender, DrawingAction action)
    {
      string kind = action.Kind;
      if (!(kind == "open") && !(kind == "link"))
      {
        if (!(kind == "copy"))
        {
          if (!(kind == "ask"))
          {
            EventHandler<DrawingAction> unhandledDrawingAction = this.UnhandledDrawingAction;
            if (unhandledDrawingAction == null)
            {
              return;
            }
            unhandledDrawingAction(this, action);
          }
          else
          {
            string text = CompanionSession.PayloadString(action.Payload, new string[] { "words", "text" });
            if (text != null && text.Length > 0)
            {
              this.audio.PlayEarcon("textSend");
              this.AskFromDrawingAsync(text, "button");
              return;
            }
          }
        }
        else
        {
          string text2 = CompanionSession.PayloadString(action.Payload, new string[] { "text" });
          if (text2 != null)
          {
            bool flag = this.options.DesktopActions.CopyToClipboard(text2);
            ClickyAnalytics.TrackClipboardCopyCompleted("screen_drawing", text2.Length, ClickyAnalytics.WordCount(text2), flag);
            return;
          }
        }
      }
      else
      {
        string text3 = CompanionSession.PayloadString(action.Payload, new string[] { "url", "text" });
        if (text3 != null && this.options.DesktopActions.OpenUrl(text3))
        {
          this.brain.NoteForNextTurn("[clicky drawing] The user pressed the drawing's button, which opened " + text3 + " in their browser.", null);
          return;
        }
      }
    }

    // Token: 0x060005F0 RID: 1520 RVA: 0x0035C674 File Offset: 0x0035C674
    private async Task<string> RunToolCallAsync(BrainToolCall call, CompanionSession.TurnRequest request, CompanionToolContext context, CancellationToken ct)
    {
      string text;
      if (ct.IsCancellationRequested)
      {
        text = ToolOutput.Status("cancelled", "The user moved on before this ran, so it didn't run.");
      }
      else
      {
        Log.Info("Companion", "tool " + call.Name);
        if (this.options.IsOnboardingTutorialRestrictingNormalUse() && BrainToolCallPolicy.ToolsHeldDuringTheTour.Contains(call.Name))
        {
          text = ToolOutput.Status("not_during_the_tour", "The user is in HeyClicky's welcome tour. Nothing was started. Say in one short line that you'll do it right after the tour, and carry on with this step.");
        }
        else
        {
          IClickyDriveTurns drive = this.options.Drive;
          if (drive != null && drive.IsDriveToolName(call.Name))
          {
            text = await drive.HandleToolAsync(call.Name, call.Arguments).ConfigureAwait(false);
          }
          else
          {
            ToolArgs toolArgs = new ToolArgs(call.Arguments);
            string name = call.Name;
            if (!(name == "open_url"))
            {
              if (!(name == "copy_to_clipboard"))
              {
                if (!(name == "insert_text"))
                {
                  Func<BrainToolCall, CompanionToolContext, CancellationToken, Task<string>> toolCallHandler = this.options.ToolCallHandler;
                  if (toolCallHandler != null)
                  {
                    try
                    {
                      return await toolCallHandler(call, context, ct).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException obj) when (ct.IsCancellationRequested)
                    {
                      return ToolOutput.Status("cancelled", "The user moved on before this finished.");
                    }
                    catch (Exception ex)
                    {
                      return ToolOutput.Error(ex.Message);
                    }
                  }
                  text = ToolOutput.Error(call.Name + " isn't available in HeyClicky for Windows yet. Tell them in one short line.");
                }
                else
                {
                  text = await this.InsertTextToolAsync(toolArgs.Exact("text") ?? "").ConfigureAwait(false);
                }
              }
              else
              {
                string text2 = toolArgs.Exact("text");
                if (string.IsNullOrEmpty(text2))
                {
                  text = "{\"status\":\"error\",\"message\":\"missing or empty text\"}";
                }
                else
                {
                  bool flag = this.options.DesktopActions.CopyToClipboard(text2);
                  ClickyAnalytics.TrackClipboardCopyCompleted("copy_to_clipboard_tool", text2.Length, ClickyAnalytics.WordCount(text2), flag);
                  if (!flag)
                  {
                    text = "{\"status\":\"error\",\"message\":\"clipboard write failed\"}";
                  }
                  else
                  {
                    this.RaiseTextDelivered(text2, null, true);
                    JsonObject jsonObject = new JsonObject(null);
                    jsonObject["status"] = "ok";
                    jsonObject["copied_to_clipboard"] = true;
                    jsonObject["chars"] = text2.Length;
                    text = ToolResults.Json(jsonObject);
                  }
                }
              }
            }
            else
            {
              string text3 = toolArgs.Str(new string[] { "url" });
              Uri uri;
              if (text3 == null || !Uri.TryCreate(text3, UriKind.Absolute, out uri))
              {
                text = "{\"status\":\"error\",\"message\":\"missing or malformed url\"}";
              }
              else
              {
                string text4;
                if (!this.options.DesktopActions.OpenUrl(text3))
                {
                  text4 = ToolOutput.Error("That URL couldn't be opened.");
                }
                else
                {
                  JsonObject jsonObject2 = new JsonObject(null);
                  jsonObject2["status"] = "opened";
                  jsonObject2["url"] = text3;
                  text4 = ToolResults.Json(jsonObject2);
                }
                text = text4;
              }
            }
          }
        }
      }
      return text;
    }

    // Token: 0x060005F1 RID: 1521 RVA: 0x0035C6D0 File Offset: 0x0035C6D0
    internal async Task<string> InsertTextToolAsync(string raw)
    {
      string text = CompanionSession.TextForInsertion(raw);
      string text2;
      if (text.Length == 0)
      {
        text2 = "{\"status\":\"error\",\"message\":\"missing text\"}";
      }
      else
      {
        TextInsertOutcome textInsertOutcome = await this.options.DesktopActions.InsertIntoFocusedFieldAsync(text).ConfigureAwait(false);
        if (textInsertOutcome.Status == "inserted")
        {
          this.RaiseTextDelivered(text, textInsertOutcome.App, false);
        }
        else if (textInsertOutcome.Status == "copied_to_clipboard")
        {
          this.RaiseTextDelivered(text, null, true);
        }
        string status = textInsertOutcome.Status;
        string text3;
        if (!(status == "inserted"))
        {
          if (!(status == "refused"))
          {
            if (!(status == "copied_to_clipboard"))
            {
              if (!(status == "unconfirmed"))
              {
                JsonObject jsonObject = new JsonObject(null);
                jsonObject["status"] = "failed";
                jsonObject["reason"] = textInsertOutcome.Reason ?? "paste_failed";
                jsonObject["message"] = "Couldn't insert into the focused field. Type it into the field you see with typeText(x, y, text) in your drawing instead.";
                text3 = ToolResults.Json(jsonObject);
              }
              else
              {
                JsonObject jsonObject2 = new JsonObject(null);
                jsonObject2["status"] = "unconfirmed";
                jsonObject2["message"] = "The paste was sent, but the text never showed up in the field (an app that reformats text can hide it, so it may still be there). Don't say it's in: tell the user to check, and if it's missing offer to copy it to their clipboard (copy_to_clipboard).";
                text3 = ToolResults.Json(jsonObject2);
              }
            }
            else
            {
              JsonObject jsonObject3 = new JsonObject(null);
              jsonObject3["status"] = "copied_to_clipboard";
              jsonObject3["message"] = "It couldn't go into the field, so it's on their clipboard: tell them to press Control V.";
              text3 = ToolResults.Json(jsonObject3);
            }
          }
          else
          {
            JsonObject jsonObject4 = new JsonObject(null);
            jsonObject4["status"] = "refused";
            jsonObject4["reason"] = textInsertOutcome.Reason ?? "unsafe_target";
            string reason = textInsertOutcome.Reason;
            jsonObject4["message"] = ((reason == "no_focused_text_field") ? "No editable text field has focus right now. If the user means a visible field, type it into the field you see with typeText(x, y, text) in your drawing." : ((reason == "own_window") ? "Focus is in HeyClicky's own UI, not an app text field. Insert it once the user is in their field." : ((reason == "secure_field") ? "The focused field is a password/secure field — text is never inserted there. Tell the user to type it themselves." : ((reason == "address_bar") ? "The focused field is the browser's address bar — not a valid insert target for composed text." : "That field didn't look safe to type into."))));
            text3 = ToolResults.Json(jsonObject4);
          }
        }
        else
        {
          string app = textInsertOutcome.App;
          JsonObject jsonObject6;
          if (app == null)
          {
            JsonObject jsonObject5 = new JsonObject(null);
            jsonObject5["status"] = "inserted";
            jsonObject6 = jsonObject5;
            jsonObject5["characters"] = text.Length;
          }
          else
          {
            JsonObject jsonObject7 = new JsonObject(null);
            jsonObject7["status"] = "inserted";
            jsonObject7["characters"] = text.Length;
            jsonObject6 = jsonObject7;
            jsonObject7["app"] = app;
          }
          text3 = ToolResults.Json(jsonObject6);
        }
        text2 = text3;
      }
      return text2;
    }

    // Token: 0x060005F2 RID: 1522 RVA: 0x0035C71C File Offset: 0x0035C71C
    internal static string TextForInsertion(string raw)
    {
      string text = raw.Replace("\r\n", "\n").Replace('\r', '\n');
      if (text.Contains('—'))
      {
        text = Regex.Replace(text, "[ \\t]*—[ \\t]*", ", ");
        text = Regex.Replace(text, "(?m)^, ", "");
      }
      return text.Trim();
    }

    // Token: 0x060005F3 RID: 1523 RVA: 0x0035C778 File Offset: 0x0035C778
    private BrainUserContext SafeUserContext()
    {
      BrainUserContext brainUserContext;
      try
      {
        Func<BrainUserContext> userContext = this.options.UserContext;
        brainUserContext = ((userContext != null) ? userContext() : null) ?? BrainUserContext.Empty;
      }
      catch (Exception ex)
      {
        Log.Warn("Companion", "user context: " + ex.Message);
        brainUserContext = BrainUserContext.Empty;
      }
      return brainUserContext;
    }

    // Token: 0x060005F4 RID: 1524 RVA: 0x0035C7DC File Offset: 0x0035C7DC
    private void SetState(VoiceState next)
    {
      object obj = this.gate;
      string text;
      lock (obj)
      {
        if (this.state == next)
        {
          return;
        }
        this.state = next;
        text = ((next == VoiceState.Processing && Volatile.Read(ref this.activeTurnCount) > 0) ? this.waitStatusLabel : null);
      }
      NotchPresenter notchPresenter = this.notch as NotchPresenter;
      if (notchPresenter != null)
      {
        notchPresenter.SetVoiceState(next, text);
      }
      else
      {
        this.ui.Post(delegate
        {
          switch (next)
          {
          case VoiceState.Listening:
            this.notch.ShowLiveEvent(new NotchLiveEvent.ActivityListening(), null);
            return;
          case VoiceState.Processing:
            this.notch.ShowLiveEvent(new NotchLiveEvent.ActivityThinking(), null);
            return;
          case VoiceState.Responding:
            this.notch.ShowLiveEvent(new NotchLiveEvent.ActivitySpeaking(), null);
            return;
          default:
            this.notch.DismissLiveEvent(typeof(NotchLiveEvent.ActivitySpeaking));
            return;
          }
        });
      }
      this.ui.Post(delegate
      {
        this.cursor.SetVoiceState(next, 0f);
      });
      EventHandler stateChanged = this.StateChanged;
      if (stateChanged == null)
      {
        return;
      }
      stateChanged(this, EventArgs.Empty);
    }

    // Token: 0x060005F5 RID: 1525 RVA: 0x0035C8D0 File Offset: 0x0035C8D0
    private void NotifyTerminating()
    {
      NotchPresenter notchPresenter = this.notch as NotchPresenter;
      if (notchPresenter != null)
      {
        notchPresenter.NotifyVoiceTurnTerminating();
      }
    }

    // Token: 0x060005F6 RID: 1526 RVA: 0x0035C8F4 File Offset: 0x0035C8F4
    private void ShowLiveEvent(NotchLiveEvent liveEvent)
    {
      NotchPresenter notchPresenter = this.notch as NotchPresenter;
      if (notchPresenter != null)
      {
        notchPresenter.ShowLiveEvent(liveEvent, null);
        return;
      }
      this.ui.Post(delegate
      {
        this.notch.ShowLiveEvent(liveEvent, null);
      });
    }

    // Token: 0x060005F7 RID: 1527 RVA: 0x0035C954 File Offset: 0x0035C954
    private void SetResponseText(string text)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.responseText = text;
      }
      EventHandler<string> responseTextUpdated = this.ResponseTextUpdated;
      if (responseTextUpdated == null)
      {
        return;
      }
      responseTextUpdated(this, text);
    }

    // Token: 0x060005F8 RID: 1528 RVA: 0x0035C9A8 File Offset: 0x0035C9A8
    private void RaiseFailed(CompanionError error)
    {
      Log.Warn("Companion", "failed: " + error.Code + " " + error.Message);
      try
      {
        EventHandler<CompanionError> failed = this.Failed;
        if (failed != null)
        {
          failed(this, error);
        }
      }
      catch (Exception ex)
      {
        Log.Error("Companion", "Failed handler threw", ex);
      }
    }

    // Token: 0x060005F9 RID: 1529 RVA: 0x0035CA14 File Offset: 0x0035CA14
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "Width", "Height" })]
    private ValueTuple<int, int> CursorScreenCanvas()
    {
      ValueTuple<int, int> valueTuple;
      try
      {
        DisplayInfo displayUnderCursor = this.screens.GetDisplayUnderCursor();
        double num = displayUnderCursor.BoundsPx.Width / Math.Max(1.0, displayUnderCursor.BoundsPx.Height);
        valueTuple = ((num >= 1.0) ? new ValueTuple<int, int>(1280, (int)(1280.0 / num)) : new ValueTuple<int, int>((int)(1280.0 * num), 1280));
      }
      catch
      {
        valueTuple = new ValueTuple<int, int>(1280, 800);
      }
      return valueTuple;
    }

    // Token: 0x060005FA RID: 1530 RVA: 0x0035CABC File Offset: 0x0035CABC
    public async ValueTask DisposeAsync()
    {
      this.microphone.Frame -= this.OnMicrophoneFrame;
      this.microphone.DeviceProvedDead -= this.OnMicrophoneDeviceProvedDead;
      this.EndBluetoothHold(true);
      this.canvas.ActionRequested -= this.OnCanvasActionRequested;
      this.canvas.UserActedOnScreen -= this.OnCanvasUserActedOnScreen;
      this.DisarmGuidedTarget(null);
      this.CancelListening();
      await this.DiscardWarmEarsAsync().ConfigureAwait(false);
      await this.mouth.DisposeAsync().ConfigureAwait(false);
      if (this.pressAudio.Length != 0)
      {
        ArrayPool<byte>.Shared.Return(this.pressAudio, false);
      }
    }

    // Token: 0x060005FB RID: 1531 RVA: 0x0035CB00 File Offset: 0x0035CB00
    public void AttachDrive(IClickyDriveTurns drive)
    {
      this.options.Drive = drive;
    }

    // Token: 0x1700014B RID: 331
    // (get) Token: 0x060005FC RID: 1532 RVA: 0x0035CB10 File Offset: 0x0035CB10
    public int AudioPlaybackBeganCount
    {
      get
      {
        object obj = this.gate;
        int num;
        lock (obj)
        {
          num = this.audioPlaybackBeganCount;
        }
        return num;
      }
    }

    // Token: 0x060005FD RID: 1533 RVA: 0x0035CB54 File Offset: 0x0035CB54
    public async Task RunDriveTurnAsync(string fact, ScreenCaptureFrame frame, IReadOnlyList<DisplayInfo> displays)
    {
      this.InterruptForNewInput(true);
      object obj = this.gate;
      long num;
      lock (obj)
      {
        num = this.generation;
      }
      this.http.PrewarmWorker();
      this.SetState(VoiceState.Processing);
      ScreenCaptureFrame screenCaptureFrame = frame.<Clone>$();
      screenCaptureFrame.ContainsCursor = true;
      CompanionSession.TurnCapture turnCapture = new CompanionSession.TurnCapture(new <>z__ReadOnlySingleElementList<ScreenCaptureFrame>(screenCaptureFrame), displays, null, 0, null);
      CompanionSession.TurnRequest turnRequest = new CompanionSession.TurnRequest(null, fact, false, false, turnCapture, this.options.DesktopActions.FrontApplication(), null, "app", Environment.TickCount64, 0, null)
      {
        IsDrive = true
      };
      await this.RunTurnAsync(turnRequest, num, null).ConfigureAwait(false);
    }

    // Token: 0x060005FE RID: 1534 RVA: 0x0035CBB0 File Offset: 0x0035CBB0
    public void ClearDrawingForDrive()
    {
      this.brain.ForgetDrawingOnScreen();
      this.ui.Post(delegate
      {
        this.canvas.Clear(true);
      });
    }

    // Token: 0x060005FF RID: 1535 RVA: 0x0035CBD4 File Offset: 0x0035CBD4
    public void InterruptForDrive()
    {
      this.InterruptForNewInput(false);
      if (this.State == VoiceState.Listening)
      {
        return;
      }
      this.NotifyTerminating();
      this.SetState(VoiceState.Idle);
    }

    // Token: 0x06000600 RID: 1536 RVA: 0x0035CBF4 File Offset: 0x0035CBF4
    public void SettleAfterDrive()
    {
      if (this.State != VoiceState.Processing)
      {
        return;
      }
      this.NotifyTerminating();
      this.SetState(VoiceState.Idle);
    }

    // Token: 0x06000601 RID: 1537 RVA: 0x0035CC10 File Offset: 0x0035CC10
    public async Task SpeakVerbatimLineAsync(string line, CancellationToken ct = default(CancellationToken))
    {
      line = line.Trim();
      if (line.Length != 0 && this.auth.IsSignedIn)
      {
        try
        {
          ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.mouth.EnsureOpenAsync(ct).ConfigureAwait(false).GetAwaiter();
          if (!configuredTaskAwaiter.IsCompleted)
          {
            await configuredTaskAwaiter;
            ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
            configuredTaskAwaiter = configuredTaskAwaiter2;
            configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
          }
          if (!configuredTaskAwaiter.GetResult())
          {
            Log.Warn("Companion", "drive line not spoken: the mouth didn't open");
          }
          else
          {
            object obj = this.gate;
            lock (obj)
            {
              this.lineAudioOffsets.Remove(-1);
            }
            this.audio.PrepareForUpcomingAudio();
            Log.Info("Companion", "drive line: " + line);
            this.mouth.SpeakLine(line, -1);
          }
        }
        catch (Exception ex) when (!(ex is OperationCanceledException))
        {
          Log.Warn("Companion", "drive line not spoken: " + ex.Message);
        }
      }
    }

    // Token: 0x06000602 RID: 1538 RVA: 0x0035CC64 File Offset: 0x0035CC64
    private void NoteDriveActions(string sessionId)
    {
      IClickyDriveTurns drive = this.options.Drive;
      IReadOnlyList<string> readOnlyList = ((drive != null) ? drive.ActionLog : null);
      if (readOnlyList == null || readOnlyList.Count <= 0)
      {
        return;
      }
      this.brain.AppendDeveloperNote(sessionId, ClickyDriveLines.ActionsNote(readOnlyList));
    }

    // Token: 0x06000603 RID: 1539 RVA: 0x0035CCA8 File Offset: 0x0035CCA8
    internal static void ShrinkOlderScreenshots(List<JsonNode> items)
    {
      List<JsonNode> list = items.Where<JsonNode>((JsonNode i) => (string)i["type"] == "computer_call_output").ToList<JsonNode>();
      foreach (JsonNode jsonNode in list.Take<JsonNode>(Math.Max(0, list.Count - 1)))
      {
        JsonObject jsonObject = jsonNode["output"] as JsonObject;
        if (jsonObject != null && !((string)jsonObject["image_url"] == "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAMCAgMCAgMDAwMEAwMEBQgFBQQEBQoHBwYIDAoMDAsKCwsNDhIQDQ4RDgsLEBYQERMUFRUVDA8XGBYUGBIUFRT/2wBDAQMEBAUEBQkFBQkUDQsNFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBT/wAARCAAIAAgDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwAooooA/9k="))
        {
          jsonObject["image_url"] = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAMCAgMCAgMDAwMEAwMEBQgFBQQEBQoHBwYIDAoMDAsKCwsNDhIQDQ4RDgsLEBYQERMUFRUVDA8XGBYUGBIUFRT/2wBDAQMEBAUEBQkFBQkUDQsNFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBT/wAARCAAIAAgDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwAooooA/9k=";
        }
      }
    }

    // Token: 0x06000604 RID: 1540 RVA: 0x0035CD6C File Offset: 0x0035CD6C
    private void HoldActionsForTheDrawing(string sessionId, List<BrainToolCall> calls, CompanionSession.TurnRequest request, CompanionToolContext context)
    {
      object obj = this.gate;
      lock (obj)
      {
        CompanionSession.HeldActions heldActions = this.actionsWaitingForTheDrawing;
        List<BrainToolCall> list = ((heldActions != null && heldActions.SessionId == sessionId) ? heldActions.Calls : new List<BrainToolCall>());
        List<BrainToolCall> list2 = list;
        int num = list2.Count + calls.Count;
        List<BrainToolCall> list3 = new List<BrainToolCall>(num);
        CollectionsMarshal.SetCount<BrainToolCall>(list3, num);
        Span<BrainToolCall> span = CollectionsMarshal.AsSpan<BrainToolCall>(list3);
        int num2 = 0;
        Span<BrainToolCall> span2 = CollectionsMarshal.AsSpan<BrainToolCall>(list2);
        span2.CopyTo(span.Slice(num2, span2.Length));
        num2 += span2.Length;
        Span<BrainToolCall> span3 = CollectionsMarshal.AsSpan<BrainToolCall>(calls);
        span3.CopyTo(span.Slice(num2, span3.Length));
        num2 += span3.Length;
        this.actionsWaitingForTheDrawing = new CompanionSession.HeldActions(sessionId, list3, request, context);
      }
      string text = "Companion";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(81, 2);
      defaultInterpolatedStringHandler.AppendFormatted<int>(calls.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" action(s) wait until the drawing is told (");
      defaultInterpolatedStringHandler.AppendFormatted(string.Join(", ", calls.Select<BrainToolCall, string>((BrainToolCall c) => c.Name)));
      defaultInterpolatedStringHandler.AppendLiteral("): the screen stays as it is meanwhile");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x06000605 RID: 1541 RVA: 0x0035CEDC File Offset: 0x0035CEDC
    private void DropActionsHeldBy(CompanionSession.TurnRequest request)
    {
      CompanionSession.HeldActions heldActions = null;
      object obj = this.gate;
      lock (obj)
      {
        CompanionSession.HeldActions heldActions2 = this.actionsWaitingForTheDrawing;
        if (heldActions2 != null && heldActions2.Request == request)
        {
          heldActions = heldActions2;
          this.actionsWaitingForTheDrawing = null;
        }
      }
      if (heldActions != null)
      {
        Log.Info("Companion", "a new turn began before the drawing was told: " + string.Join(", ", heldActions.Calls.Select<BrainToolCall, string>((BrainToolCall c) => c.Name)) + " dropped");
      }
    }

    // Token: 0x06000606 RID: 1542 RVA: 0x0035CF84 File Offset: 0x0035CF84
    private void DropActionsWaitingForTheDrawing()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.actionsWaitingForTheDrawing = null;
      }
    }

    // Token: 0x06000607 RID: 1543 RVA: 0x0035CFC8 File Offset: 0x0035CFC8
    internal async Task<List<string>> PerformDrawingActionsForTestAsync(IReadOnlyList<DrawingAction> actions, bool readConnectedApps)
    {
      CompanionSession.NarrationState narration = new CompanionSession.NarrationState
      {
        ReadConnectedApps = readConnectedApps
      };
      List<string> corrections = new List<string>();
      CompanionSession.TurnRequest request = new CompanionSession.TurnRequest("question", null, false, false, null, new ValueTuple<string, string>(null, null), null, "voice", Environment.TickCount64, 0, null);
      foreach (DrawingAction drawingAction in actions)
      {
        await this.PerformDrawingActionAsync(drawingAction, corrections, request, narration, () => true).ConfigureAwait(false);
      }
      IEnumerator<DrawingAction> enumerator = null;
      return corrections;
    }

    // Token: 0x1700014C RID: 332
    // (get) Token: 0x06000608 RID: 1544 RVA: 0x0035D01C File Offset: 0x0035D01C
    internal int HeldActionCountForTest
    {
      get
      {
        object obj = this.gate;
        int num;
        lock (obj)
        {
          CompanionSession.HeldActions heldActions = this.actionsWaitingForTheDrawing;
          num = ((heldActions != null) ? heldActions.Calls.Count : 0);
        }
        return num;
      }
    }

    // Token: 0x06000609 RID: 1545 RVA: 0x0035D070 File Offset: 0x0035D070
    private async Task RunActionsWaitingForTheDrawingAsync(bool stillCurrent)
    {
      CompanionSession.<>c__DisplayClass227_0 CS$<>8__locals1 = new CompanionSession.<>c__DisplayClass227_0();
      CS$<>8__locals1.<>4__this = this;
      object obj = this.gate;
      lock (obj)
      {
        CS$<>8__locals1.waiting = this.actionsWaitingForTheDrawing;
        this.actionsWaitingForTheDrawing = null;
      }
      if (CS$<>8__locals1.waiting != null)
      {
        if (!stillCurrent || CS$<>8__locals1.waiting.SessionId != this.brain.SessionId)
        {
          Log.Info("Companion", "a new turn began before the drawing was told: " + string.Join(", ", CS$<>8__locals1.waiting.Calls.Select<BrainToolCall, string>((BrainToolCall c) => c.Name)) + " dropped");
        }
        else
        {
          List<string> failures = new List<string>();
          foreach (BrainToolCall call in CS$<>8__locals1.waiting.Calls)
          {
            string text = await this.RunToolCallAsync(call, CS$<>8__locals1.waiting.Request, CS$<>8__locals1.waiting.Context, CancellationToken.None).ConfigureAwait(false);
            Log.Info("Companion", "after the drawing: " + call.Name + " → " + ((text.Length > 400) ? (text.Substring(0, 400) + "…") : text));
            this.brain.AppendToolResult(call, text);
            if (!BrainToolCallPolicy.IsCleanAsyncSuccess(call.Name, text))
            {
              failures.Add(call.Name + ": " + ((text.Length > 400) ? text.Substring(0, 400) : text));
            }
            call = null;
          }
          List<BrainToolCall>.Enumerator enumerator = default(List<BrainToolCall>.Enumerator);
          this.brain.SaveToDisk();
          BrainSession brainSession = this.brain;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["kind"] = "after_drawing";
          jsonObject["calls"] = new JsonArray(CS$<>8__locals1.waiting.Calls.Select<BrainToolCall, JsonNode>((BrainToolCall c) => JsonValue.Create(c.Name, null)).ToArray<JsonNode>());
          jsonObject["failures"] = new JsonArray(failures.Select<string, JsonNode>((string f) => JsonValue.Create(f, null)).ToArray<JsonNode>());
          brainSession.LogRecord(jsonObject);
          if (failures.Count != 0 && !this.IsListeningNow)
          {
            obj = this.gate;
            lock (obj)
            {
              CS$<>8__locals1.generationNow = this.generation;
            }
            CS$<>8__locals1.fact = "[App] After your answer was told, this didn't work: " + string.Join("; ", failures) + ". Tell them in a few words.";
            Task.Run(delegate
            {
              CompanionSession.<>c__DisplayClass227_0.<<RunActionsWaitingForTheDrawingAsync>b__1>d <<RunActionsWaitingForTheDrawingAsync>b__1>d;
              <<RunActionsWaitingForTheDrawingAsync>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
              <<RunActionsWaitingForTheDrawingAsync>b__1>d.<>4__this = CS$<>8__locals1;
              <<RunActionsWaitingForTheDrawingAsync>b__1>d.<>1__state = -1;
              <<RunActionsWaitingForTheDrawingAsync>b__1>d.<>t__builder.Start<CompanionSession.<>c__DisplayClass227_0.<<RunActionsWaitingForTheDrawingAsync>b__1>d>(ref <<RunActionsWaitingForTheDrawingAsync>b__1>d);
              return <<RunActionsWaitingForTheDrawingAsync>b__1>d.<>t__builder.Task;
            });
          }
        }
      }
    }

    // Token: 0x0600060A RID: 1546 RVA: 0x0035D0BC File Offset: 0x0035D0BC
    private Task RunAppFactTurnAsync(string fact, bool typed, bool answersInText)
    {
      CompanionSession.<RunAppFactTurnAsync>d__228 <RunAppFactTurnAsync>d__;
      <RunAppFactTurnAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
      <RunAppFactTurnAsync>d__.<>4__this = this;
      <RunAppFactTurnAsync>d__.fact = fact;
      <RunAppFactTurnAsync>d__.typed = typed;
      <RunAppFactTurnAsync>d__.answersInText = answersInText;
      <RunAppFactTurnAsync>d__.<>1__state = -1;
      <RunAppFactTurnAsync>d__.<>t__builder.Start<CompanionSession.<RunAppFactTurnAsync>d__228>(ref <RunAppFactTurnAsync>d__);
      return <RunAppFactTurnAsync>d__.<>t__builder.Task;
    }

    // Token: 0x0600060B RID: 1547 RVA: 0x0035D118 File Offset: 0x0035D118
    [return: Nullable(new byte[] { 1, 2, 1 })]
    public async Task<IReadOnlyList<string>> RunReportTurnAsync(string fact, CancellationToken ct = default(CancellationToken))
    {
      IReadOnlyList<string> readOnlyList;
      try
      {
        string text = await this.auth.GetValidAccessTokenAsync(ct).ConfigureAwait(false);
        if (text == null)
        {
          Log.Warn("Companion", "report turn: not signed in");
          readOnlyList = null;
        }
        else
        {
          this.brain.AdoptAccount(this.auth.UserId);
          this.brain.BeginTurnStartingNewSessionIfIdle();
          ScreenCaptureFrame screenCaptureFrame = new ScreenCaptureFrame(new DisplayInfo(0, "", new RectD(0.0, 0.0, 0.0, 0.0), new RectD(0.0, 0.0, 0.0, 0.0), 1.0, true), Array.Empty<byte>(), 0, 0, false, new PointD(0.0, 0.0));
          CompanionSession.TurnCapture turnCapture = new CompanionSession.TurnCapture(new <>z__ReadOnlySingleElementList<ScreenCaptureFrame>(screenCaptureFrame), Array.Empty<DisplayInfo>(), null, 0, null);
          CompanionSession.NarrationState narration = new CompanionSession.NarrationState
          {
            WriteOnly = true
          };
          CompanionSession.TurnRequest turnRequest = new CompanionSession.TurnRequest(null, fact, false, false, turnCapture, new ValueTuple<string, string>(null, null), null, "app", Environment.TickCount64, 0, null)
          {
            Screenless = true
          };
          await this.StreamBrainAsync(turnRequest, turnCapture, screenCaptureFrame, new List<ScreenCaptureFrame>(), text, narration, null, () => !ct.IsCancellationRequested, new CompanionSession.TurnTimer(turnRequest.StartedTicks), ct, null).ConfigureAwait(false);
          object obj = narration.Gate;
          lock (obj)
          {
            string failure = narration.Failure;
            if (failure != null)
            {
              Log.Warn("Companion", "report turn failed: " + failure);
              readOnlyList = null;
            }
            else if (string.IsNullOrWhiteSpace(narration.ProgramSoFar))
            {
              Log.Warn("Companion", "report turn failed: it wrote nothing");
              readOnlyList = null;
            }
            else
            {
              readOnlyList = (from l in SayLines.InOrder(narration.ProgramSoFar)
                select l.Trim() into l
                where l.Length > 0
                select l).ToList<string>();
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Companion", "report turn failed: " + ex.Message);
        readOnlyList = null;
      }
      return readOnlyList;
    }

    // Token: 0x1400000C RID: 12
    // (add) Token: 0x0600060C RID: 1548 RVA: 0x0035D16C File Offset: 0x0035D16C
    // (remove) Token: 0x0600060D RID: 1549 RVA: 0x0035D1A4 File Offset: 0x0035D1A4
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<DrawingRecord> DrawingTold;

    // Token: 0x1400000D RID: 13
    // (add) Token: 0x0600060E RID: 1550 RVA: 0x0035D1DC File Offset: 0x0035D1DC
    // (remove) Token: 0x0600060F RID: 1551 RVA: 0x0035D214 File Offset: 0x0035D214
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<ScreenCaptureFrame> DrawingLeftUp;

    // Token: 0x06000610 RID: 1552 RVA: 0x0035D24C File Offset: 0x0035D24C
    internal void BeginLessonRecording([Nullable(2)] string question, ScreenCaptureFrame screen, string color)
    {
      bool flag = this.settings.Read<bool>((AppSettings s) => s.SaveDrawingsEffective);
      bool flag2 = this.settings.Read<bool>((AppSettings s) => s.KeepDrawingsInChatEffective);
      object obj = this.gate;
      lock (obj)
      {
        this.lessonRecording = new CompanionSession.LessonRecording(question ?? "", screen, color, flag, flag2);
        flag = flag || flag2;
        if (flag && !this.listensForLessonAudio)
        {
          this.listensForLessonAudio = true;
          this.mouth.LineAudioChunk += this.OnLessonLineAudio;
        }
      }
    }

    // Token: 0x06000611 RID: 1553 RVA: 0x0035D324 File Offset: 0x0035D324
    [NullableContext(0)]
    private void OnLessonLineAudio(int index, ReadOnlySpan<byte> pcm16)
    {
      if (index < 0)
      {
        return;
      }
      object obj = this.gate;
      lock (obj)
      {
        CompanionSession.LessonRecording lessonRecording = this.lessonRecording;
        if (lessonRecording != null && lessonRecording.SavesDrawing)
        {
          MemoryStream memoryStream;
          if (!lessonRecording.Audio.TryGetValue(index, out memoryStream))
          {
            memoryStream = (lessonRecording.Audio[index] = new MemoryStream());
          }
          memoryStream.Write(pcm16);
        }
      }
    }

    // Token: 0x06000612 RID: 1554 RVA: 0x0035D3A4 File Offset: 0x0035D3A4
    internal void NoteLessonLineHeard(int index)
    {
      object obj = this.gate;
      lock (obj)
      {
        CompanionSession.LessonRecording lessonRecording = this.lessonRecording;
        if (lessonRecording != null && !lessonRecording.Starts.ContainsKey(index))
        {
          lessonRecording.Starts[index] = (double)(Environment.TickCount64 - lessonRecording.StartedTicks) / 1000.0;
        }
      }
    }

    // Token: 0x06000613 RID: 1555 RVA: 0x0035D41C File Offset: 0x0035D41C
    private void NoteLessonLineLength(int index, double seconds)
    {
      object obj = this.gate;
      lock (obj)
      {
        CompanionSession.LessonRecording lessonRecording = this.lessonRecording;
        if (lessonRecording != null && seconds > 0.0)
        {
          lessonRecording.Lengths[index] = seconds;
        }
      }
    }

    // Token: 0x06000614 RID: 1556 RVA: 0x0035D47C File Offset: 0x0035D47C
    internal void FinishLessonRecording(IReadOnlyList<string> lines, string program)
    {
      object obj = this.gate;
      CompanionSession.LessonRecording lessonRecording;
      lock (obj)
      {
        lessonRecording = this.lessonRecording;
        this.lessonRecording = null;
      }
      if (lessonRecording == null)
      {
        return;
      }
      DrawingRecord drawingRecord = (lessonRecording.SavesDrawing ? CompanionSession.LessonRecord(lessonRecording, lines, program, this.audio.SampleRate) : null);
      if (drawingRecord == null)
      {
        try
        {
          EventHandler<ScreenCaptureFrame> drawingLeftUp = this.DrawingLeftUp;
          if (drawingLeftUp != null)
          {
            drawingLeftUp(this, lessonRecording.Screen);
          }
        }
        catch (Exception ex)
        {
          Log.Warn("Companion", "DrawingLeftUp handler: " + ex.Message);
        }
        return;
      }
      string text = "Companion";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 3);
      defaultInterpolatedStringHandler.AppendLiteral("drawing told: \"");
      defaultInterpolatedStringHandler.AppendFormatted(drawingRecord.Title);
      defaultInterpolatedStringHandler.AppendLiteral("\", ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(drawingRecord.Lines.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" lines, ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(drawingRecord.Duration, "0.0");
      defaultInterpolatedStringHandler.AppendLiteral(" s, ");
      string text2 = defaultInterpolatedStringHandler.ToStringAndClear();
      byte[] narration = drawingRecord.Narration;
      string text3;
      if (narration == null)
      {
        text3 = "no voice (a line's audio is missing)";
      }
      else
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
        defaultInterpolatedStringHandler.AppendLiteral("voice ");
        defaultInterpolatedStringHandler.AppendFormatted<double>((double)narration.Length / 2.0 / (double)drawingRecord.NarrationSampleRate, "0.0");
        defaultInterpolatedStringHandler.AppendLiteral(" s");
        text3 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      Log.Info(text, text2 + text3);
      try
      {
        EventHandler<DrawingRecord> drawingTold = this.DrawingTold;
        if (drawingTold != null)
        {
          drawingTold(this, drawingRecord);
        }
      }
      catch (Exception ex2)
      {
        Log.Warn("Companion", "DrawingTold handler: " + ex2.Message);
      }
    }

    // Token: 0x06000615 RID: 1557 RVA: 0x0035D650 File Offset: 0x0035D650
    [return: Nullable(2)]
    private static DrawingRecord LessonRecord(CompanionSession.LessonRecording r, IReadOnlyList<string> lines, string program, int sampleRate)
    {
      if (lines.Count < (r.KeepsInChat ? 1 : 3) || program.Trim().Length == 0)
      {
        return null;
      }
      if (!Enumerable.Range(0, lines.Count).All<int>(new Func<int, bool>(r.Starts.ContainsKey)))
      {
        return null;
      }
      bool hasEveryLineAudio = sampleRate > 0 && Enumerable.Range(0, lines.Count).All<int>(delegate(int i)
      {
        MemoryStream memoryStream;
        return r.Audio.TryGetValue(i, out memoryStream) && memoryStream.Length > 0L;
      });
      List<DrawingRecord.Line> list = lines.Select<string, DrawingRecord.Line>(delegate(string text, int i)
      {
        double num2;
        return new DrawingRecord.Line(text, r.Starts[i], hasEveryLineAudio ? new double?((double)r.Audio[i].Length / 2.0 / (double)sampleRate) : (r.Lengths.TryGetValue(i, out num2) ? new double?(num2) : null));
      }).ToList<DrawingRecord.Line>();
      double num = list.Max<DrawingRecord.Line>((DrawingRecord.Line l) => l.Start + (l.Length ?? CompanionSession.ReadingSeconds(l.Text))) + 0.8;
      byte[] array;
      if (!hasEveryLineAudio)
      {
        array = null;
      }
      else
      {
        array = LessonNarration.BuildTrack(list.Select<DrawingRecord.Line, double>((DrawingRecord.Line l) => l.Start).ToList<double>(), (from i in Enumerable.Range(0, lines.Count)
          select r.Audio[i].ToArray()).ToList<byte[]>(), sampleRate, num);
      }
      byte[] array2 = array;
      return new DrawingRecord(r.Question, CompanionSession.LessonTitle(program, lines[0]), program, list, num, r.Screen, r.Color, DateTimeOffset.Now, array2, hasEveryLineAudio ? sampleRate : 0)
      {
        KeepsInChat = r.KeepsInChat
      };
    }

    // Token: 0x06000616 RID: 1558 RVA: 0x0035D800 File Offset: 0x0035D800
    public static string LessonTitle(string program, string firstLine)
    {
      Match match = Regex.Match(program, "lessonTitle\\(\\s*(?:\"([^\"\\n]{1,80})\"|'([^'\\n]{1,80})')\\s*\\)");
      if (match.Success)
      {
        string text = (match.Groups[1].Success ? match.Groups[1].Value : match.Groups[2].Value).Trim();
        if (text.Length > 0)
        {
          return text;
        }
      }
      string[] array = firstLine.Trim().Trim(new char[] { '.', '!', '?', ',', ';', ':' }).Split(' ', StringSplitOptions.RemoveEmptyEntries);
      string text2 = string.Join<string>(' ', array.Take<string>(7));
      if (array.Length <= 7)
      {
        return text2;
      }
      return text2 + "…";
    }

    // Token: 0x06000617 RID: 1559 RVA: 0x0035D8B0 File Offset: 0x0035D8B0
    private static NotchLiveEvent.MicrophoneTrouble CannotHearMicrophone([Nullable(2)] string deviceName)
    {
      return new NotchLiveEvent.MicrophoneTrouble((string.IsNullOrWhiteSpace(deviceName) ? "Your microphone" : deviceName) + " isn't sending audio", "HeyClicky can't hear your microphone", true);
    }

    // Token: 0x06000618 RID: 1560 RVA: 0x0035D8D8 File Offset: 0x0035D8D8
    private static NotchLiveEvent.MicrophoneTrouble FailedOverToBuiltInMicrophone(string deadDeviceName)
    {
      return new NotchLiveEvent.MicrophoneTrouble(deadDeviceName + " stopped sending audio", "Switched to your PC's microphone", false);
    }

    // Token: 0x06000619 RID: 1561 RVA: 0x0035D8F0 File Offset: 0x0035D8F0
    private void OnMicrophoneDeviceProvedDead(string deadDeviceName)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = this.isListening;
      }
      Log.Warn("Companion", "the mic's capture gave up on '" + deadDeviceName + "'" + (flag2 ? " mid-press" : ""));
      this.MicrophoneProvedDead(deadDeviceName);
    }

    // Token: 0x0600061A RID: 1562 RVA: 0x0035D964 File Offset: 0x0035D964
    private void MicrophoneProvedDead(string deadDeviceName)
    {
      string text;
      try
      {
        text = this.microphone.FailOverToBuiltInMicrophone(deadDeviceName);
      }
      catch (Exception ex)
      {
        Log.Warn("Companion", "mic failover: " + ex.Message);
        text = null;
      }
      if (text != null)
      {
        Interlocked.Exchange(ref this.consecutiveDeadAirPresses, 0);
        string text2 = "Companion";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 2);
        defaultInterpolatedStringHandler.AppendLiteral("mic failover: '");
        defaultInterpolatedStringHandler.AppendFormatted(deadDeviceName);
        defaultInterpolatedStringHandler.AppendLiteral("' proved dead, using '");
        defaultInterpolatedStringHandler.AppendFormatted(text);
        defaultInterpolatedStringHandler.AppendLiteral("'");
        Log.Warn(text2, defaultInterpolatedStringHandler.ToStringAndClear());
        this.ShowLiveEvent(CompanionSession.FailedOverToBuiltInMicrophone(deadDeviceName));
        return;
      }
      if (this.hasReportedIrrecoverableMicrophone)
      {
        return;
      }
      this.hasReportedIrrecoverableMicrophone = true;
      Log.Warn("Companion", "mic irrecoverable: '" + deadDeviceName + "' dead with no failover left");
      this.ShowLiveEvent(CompanionSession.CannotHearMicrophone(deadDeviceName));
    }

    // Token: 0x0600061B RID: 1563 RVA: 0x0035DA54 File Offset: 0x0035DA54
    private void HoldBluetoothMicrophoneOpenUntilTheReplyPlaysOut()
    {
      CompanionSession.<>c__DisplayClass253_0 CS$<>8__locals1 = new CompanionSession.<>c__DisplayClass253_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.hold = new CancellationTokenSource();
      CancellationTokenSource cancellationTokenSource = Interlocked.Exchange<CancellationTokenSource>(ref this.bluetoothHold, CS$<>8__locals1.hold);
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      Task.Run(delegate
      {
        CompanionSession.<>c__DisplayClass253_0.<<HoldBluetoothMicrophoneOpenUntilTheReplyPlaysOut>b__0>d <<HoldBluetoothMicrophoneOpenUntilTheReplyPlaysOut>b__0>d;
        <<HoldBluetoothMicrophoneOpenUntilTheReplyPlaysOut>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<HoldBluetoothMicrophoneOpenUntilTheReplyPlaysOut>b__0>d.<>4__this = CS$<>8__locals1;
        <<HoldBluetoothMicrophoneOpenUntilTheReplyPlaysOut>b__0>d.<>1__state = -1;
        <<HoldBluetoothMicrophoneOpenUntilTheReplyPlaysOut>b__0>d.<>t__builder.Start<CompanionSession.<>c__DisplayClass253_0.<<HoldBluetoothMicrophoneOpenUntilTheReplyPlaysOut>b__0>d>(ref <<HoldBluetoothMicrophoneOpenUntilTheReplyPlaysOut>b__0>d);
        return <<HoldBluetoothMicrophoneOpenUntilTheReplyPlaysOut>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x0600061C RID: 1564 RVA: 0x0035DAA8 File Offset: 0x0035DAA8
    private void EndBluetoothHold(bool release)
    {
      CancellationTokenSource cancellationTokenSource = Interlocked.Exchange<CancellationTokenSource>(ref this.bluetoothHold, null);
      if (cancellationTokenSource == null)
      {
        return;
      }
      cancellationTokenSource.Cancel();
      if (release)
      {
        this.microphone.ReleaseHeldDevice();
      }
    }

    // Token: 0x1400000E RID: 14
    // (add) Token: 0x0600061D RID: 1565 RVA: 0x0035DADC File Offset: 0x0035DADC
    // (remove) Token: 0x0600061E RID: 1566 RVA: 0x0035DB14 File Offset: 0x0035DB14
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action OneBrainTurnEnded;

    // Token: 0x0600061F RID: 1567 RVA: 0x0035DB4C File Offset: 0x0035DB4C
    private void RaiseOneBrainTurnEnded()
    {
      try
      {
        Action oneBrainTurnEnded = this.OneBrainTurnEnded;
        if (oneBrainTurnEnded != null)
        {
          oneBrainTurnEnded();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Companion", "OneBrainTurnEnded handler threw: " + ex.Message);
      }
    }

    // Token: 0x06000620 RID: 1568 RVA: 0x0035DB9C File Offset: 0x0035DB9C
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<CompanionSession.PrefetchedTurn> PreparePrefetchedFactTurnAsync(string fact)
    {
      CompanionSession.TurnCapture turnCapture = await this.CaptureForTurnAsync(false).ConfigureAwait(false);
      CompanionSession.TurnCapture capture = turnCapture;
      string text = await this.auth.GetValidAccessTokenAsync(default(CancellationToken)).ConfigureAwait(false);
      CompanionSession.PrefetchedTurn prefetchedTurn;
      if (capture.Frames.Count == 0 || text == null)
      {
        prefetchedTurn = null;
      }
      else
      {
        prefetchedTurn = new CompanionSession.PrefetchedTurn(fact, capture, text, this.options.DesktopActions.FrontApplication(), this.auth.UserId);
      }
      return prefetchedTurn;
    }

    // Token: 0x06000621 RID: 1569 RVA: 0x0035DBE8 File Offset: 0x0035DBE8
    public bool StartPrefetchedTurn(CompanionSession.PrefetchedTurn turn, [Nullable(2)] JsonObject flowBrief)
    {
      object obj = this.prefetchGate;
      lock (obj)
      {
        if (turn.Request != null || Volatile.Read(ref this.activeTurnCount) > 0 || this.pendingPrefetchedTurn != null)
        {
          return false;
        }
        this.pendingPrefetchedTurn = turn;
        turn.Request = new CompanionSession.TurnRequest(null, turn.Fact, false, false, turn.Capture, turn.FrontApp, null, "app", Environment.TickCount64, 0, null)
        {
          FlowBrief = flowBrief
        };
        turn.StartedAt = DateTimeOffset.Now;
      }
      CompanionSession.TurnRequest request = turn.Request;
      this.brain.AdoptAccount(this.auth.UserId);
      this.brain.BeginTurnStartingNewSessionIfIdle();
      this.mouth.EnsureOpenAsync(default(CancellationToken));
      CompanionSession.TurnCapture capture = turn.Capture;
      ScreenCaptureFrame cursorFrame = capture.Frames.FirstOrDefault<ScreenCaptureFrame>((ScreenCaptureFrame f) => f.ContainsCursor) ?? capture.Frames[0];
      List<ScreenCaptureFrame> otherFrames = capture.Frames.Where<ScreenCaptureFrame>((ScreenCaptureFrame f) => f != cursorFrame).ToList<ScreenCaptureFrame>();
      turn.Streaming = Task.Run(() => this.StreamBrainAsync(request, capture, cursorFrame, otherFrames, turn.AccessToken, turn.Narration, null, new Func<bool>(turn.StillCurrent), new CompanionSession.TurnTimer(request.StartedTicks), turn.Token, turn));
      turn.Streaming.ContinueWith(delegate(Task t)
      {
        AggregateException exception = t.Exception;
      }, TaskContinuationOptions.OnlyOnFaulted);
      return true;
    }

    // Token: 0x06000622 RID: 1570 RVA: 0x0035DDF4 File Offset: 0x0035DDF4
    public bool AdoptPrefetchedTurn(CompanionSession.PrefetchedTurn turn)
    {
      object obj = this.prefetchGate;
      lock (obj)
      {
        if (turn.IsDropped || turn.IsAdopted || turn.Request == null)
        {
          return false;
        }
        if (this.pendingPrefetchedTurn == turn)
        {
          this.pendingPrefetchedTurn = null;
        }
        turn.MarkAdopted();
      }
      BrainTurnTiming.DiscardCurrent();
      return true;
    }

    // Token: 0x06000623 RID: 1571 RVA: 0x0035DE68 File Offset: 0x0035DE68
    public void DropPrefetchedTurn(string reason)
    {
      object obj = this.prefetchGate;
      lock (obj)
      {
        CompanionSession.PrefetchedTurn prefetchedTurn = this.pendingPrefetchedTurn;
        if (prefetchedTurn == null)
        {
          return;
        }
        this.pendingPrefetchedTurn = null;
        prefetchedTurn.Drop();
      }
      Log.Info("Companion", "prefetched turn dropped: " + reason);
    }

    // Token: 0x06000624 RID: 1572 RVA: 0x0035DED0 File Offset: 0x0035DED0
    public async Task PlayPrefetchedTurnAsync(CompanionSession.PrefetchedTurn turn)
    {
      CompanionSession.TurnRequest request = turn.Request;
      if (request != null && turn.IsAdopted)
      {
        this.InterruptForNewInput(false);
        object obj = this.gate;
        long num;
        lock (obj)
        {
          num = this.generation;
        }
        this.http.PrewarmWorker();
        this.SetState(VoiceState.Processing);
        await this.RunTurnAsync(request, num, turn).ConfigureAwait(false);
      }
    }

    // Token: 0x1700014D RID: 333
    // (get) Token: 0x06000625 RID: 1573 RVA: 0x0035DF1C File Offset: 0x0035DF1C
    public IReadOnlyList<string> CurrentTurnSpokenLines
    {
      get
      {
        return SayLines.InOrder(this.CurrentTurnProgramSoFar);
      }
    }

    // Token: 0x1400000F RID: 15
    // (add) Token: 0x06000626 RID: 1574 RVA: 0x0035DF2C File Offset: 0x0035DF2C
    // (remove) Token: 0x06000627 RID: 1575 RVA: 0x0035DF64 File Offset: 0x0035DF64
    [TupleElementNames(new string[] { "Trigger", "WalkthroughStep" })]
    [Nullable(new byte[] { 2, 0, 1 })]
    [field: TupleElementNames(new string[] { "Trigger", "WalkthroughStep" })]
    [field: Nullable(new byte[] { 2, 0, 1 })]
    public event EventHandler<ValueTuple<string, int?>> TurnStarted;

    // Token: 0x1700014E RID: 334
    // (get) Token: 0x06000628 RID: 1576 RVA: 0x0035DF9C File Offset: 0x0035DF9C
    [Nullable(2)]
    public ClickTargetRing ArmedClickTarget
    {
      [NullableContext(2)]
      get
      {
        object obj = this.gate;
        ClickTargetRing clickTargetRing;
        lock (obj)
        {
          CompanionSession.GuidedTarget guidedTarget = this.guidedTarget;
          clickTargetRing = ((guidedTarget != null) ? new ClickTargetRing(guidedTarget.CenterPx, guidedTarget.RadiusPx, guidedTarget.ControlFramePx, guidedTarget.Label) : null);
        }
        return clickTargetRing;
      }
    }

    // Token: 0x06000629 RID: 1577 RVA: 0x0035E004 File Offset: 0x0035E004
    public void SimulatePressForTest(PointD point)
    {
      object obj = this.gate;
      long num;
      lock (obj)
      {
        CompanionSession.GuidedTarget guidedTarget = this.guidedTarget;
        num = ((guidedTarget != null) ? guidedTarget.Id : (-1L));
      }
      if (num >= 0L)
      {
        this.OnGuidedPress(num, point);
      }
    }

    // Token: 0x0600062A RID: 1578 RVA: 0x0035E060 File Offset: 0x0035E060
    private async Task ArmGuidedTargetIfLastBeatAsync(CompanionSession.NarrationState narration, DrawingAction action, PointD point, RectD? frame, CompanionSession.TurnRequest request, Func<bool> stillCurrent)
    {
      int line = CompanionSession.ActionLine(action);
      long deadline = Environment.TickCount64 + 8000L;
      while (stillCurrent() && Environment.TickCount64 < deadline)
      {
        object obj = narration.Gate;
        lock (obj)
        {
          if (narration.Finished || narration.Lines.Count > line + 1)
          {
            break;
          }
        }
        await this.WaitForChangeAsync(100, CancellationToken.None).ConfigureAwait(false);
      }
      if (stillCurrent())
      {
        object obj = narration.Gate;
        string text2;
        lock (obj)
        {
          if (narration.Lines.Count > line + 1)
          {
            string text = "Companion";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 1);
            defaultInterpolatedStringHandler.AppendLiteral("click target in beat ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(line + 1);
            defaultInterpolatedStringHandler.AppendLiteral(" skipped: more beats follow");
            Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
            return;
          }
          text2 = ((line < narration.Lines.Count) ? narration.Lines[line] : "");
        }
        string text3 = CompanionSession.PayloadString(action.Payload, new string[] { "label" });
        int valueOrDefault = request.WalkthroughStep.GetValueOrDefault();
        JsonElement jsonElement;
        double num2;
        double num = ((action.Payload.TryGetProperty("r", out jsonElement) && jsonElement.TryGetDouble(out num2)) ? num2 : 0.0);
        ClickyAnalytics.TrackAnnotationMarkShown("screen_drawing", text3, action.Kind == "hover", (int)Math.Round(Math.Max(num, 14.0)), 1);
        if (valueOrDefault >= 15)
        {
          string text4 = "Companion";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(68, 2);
          defaultInterpolatedStringHandler.AppendLiteral("guided target suppressed: step ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(valueOrDefault);
          defaultInterpolatedStringHandler.AppendLiteral(" is the walkthrough's last (label \"");
          defaultInterpolatedStringHandler.AppendFormatted(text3);
          defaultInterpolatedStringHandler.AppendLiteral("\")");
          Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
        }
        else
        {
          CompanionSession.TurnCapture capture = request.Capture;
          ScreenCaptureFrame screenCaptureFrame;
          if (capture != null)
          {
            IReadOnlyList<ScreenCaptureFrame> frames = capture.Frames;
            if (frames != null && frames.Count > 0)
            {
              screenCaptureFrame = capture.Frames.FirstOrDefault<ScreenCaptureFrame>((ScreenCaptureFrame f) => f.ContainsCursor) ?? capture.Frames[0];
              goto IL_0358;
            }
          }
          screenCaptureFrame = null;
          IL_0358:
          ScreenCaptureFrame screenCaptureFrame2 = screenCaptureFrame;
          if (screenCaptureFrame2 != null)
          {
            DisplayInfo display = screenCaptureFrame2.Display;
            double num3 = Math.Max(0.5, display.Scale);
            double num4 = Math.Max(display.BoundsPx.Width / (double)Math.Max(1, screenCaptureFrame2.PixelWidth), display.BoundsPx.Height / (double)Math.Max(1, screenCaptureFrame2.PixelHeight));
            double num5 = Math.Clamp(Math.Max(num, 14.0) * num4, 28.0 * num3, 180.0 * num3);
            PointD pointD = point;
            if (frame != null)
            {
              RectD valueOrDefault2 = frame.GetValueOrDefault();
              pointD = new PointD(valueOrDefault2.X + valueOrDefault2.Width / 2.0, valueOrDefault2.Y + valueOrDefault2.Height / 2.0);
              num5 = Math.Max(28.0 * num3, Math.Sqrt(valueOrDefault2.Width * valueOrDefault2.Width + valueOrDefault2.Height * valueOrDefault2.Height) / 2.0);
            }
            string text5 = CompanionSession.<ArmGuidedTargetIfLastBeatAsync>g__IfNotOurs|299_1(this.options.DesktopActions.FrontApplication().Item2) ?? CompanionSession.<ArmGuidedTargetIfLastBeatAsync>g__IfNotOurs|299_1(request.FrontApp.Item2);
            CompanionSession.GuidedTarget guidedTarget = new CompanionSession.GuidedTarget(Interlocked.Increment(ref this.guidedTargetIds), pointD, num5, frame, display.BoundsPx, num3, text3, text2, valueOrDefault, action.Kind == "hover", Environment.TickCount64, request.Typed, request.AnswersInText, text5);
            this.ArmGuidedTarget(guidedTarget);
            obj = narration.Gate;
            lock (obj)
            {
              narration.TargetArmed = true;
            }
          }
        }
      }
    }

    // Token: 0x0600062B RID: 1579 RVA: 0x0035E0D8 File Offset: 0x0035E0D8
    private void ArmGuidedTarget(CompanionSession.GuidedTarget target)
    {
      this.DisarmAdvance();
      this.DisarmGuidedTarget(null);
      CancellationTokenSource loop = new CancellationTokenSource();
      object obj = this.gate;
      lock (obj)
      {
        this.guidedTarget = target;
        this.guidedWatchLoop = loop;
      }
      IPointerInput pointerInput = this.options.PointerInput;
      if (pointerInput != null)
      {
        this.guidedPressWatch = pointerInput.WatchPresses(delegate(PointD press)
        {
          this.OnGuidedPress(target.Id, press);
        });
      }
      else
      {
        Log.Warn("Companion", "no pointer input: the click target can't see clicks");
      }
      string text = "Companion";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(62, 7);
      defaultInterpolatedStringHandler.AppendLiteral("guided target arm label=\"");
      defaultInterpolatedStringHandler.AppendFormatted(target.Label ?? "target");
      defaultInterpolatedStringHandler.AppendLiteral("\" x=");
      defaultInterpolatedStringHandler.AppendFormatted<double>(target.CenterPx.X, "0");
      defaultInterpolatedStringHandler.AppendLiteral(" y=");
      defaultInterpolatedStringHandler.AppendFormatted<double>(target.CenterPx.Y, "0");
      defaultInterpolatedStringHandler.AppendLiteral(" radius=");
      defaultInterpolatedStringHandler.AppendFormatted<double>(target.RadiusPx, "0");
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendLiteral("control=");
      RectD? controlFramePx = target.ControlFramePx;
      string text2;
      if (controlFramePx != null)
      {
        RectD valueOrDefault = controlFramePx.GetValueOrDefault();
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(1, 2);
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
      defaultInterpolatedStringHandler.AppendLiteral(" step=");
      defaultInterpolatedStringHandler.AppendFormatted<int>(target.Step);
      defaultInterpolatedStringHandler.AppendLiteral(" hover=");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(target.IsHover);
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      ClickyAnalytics.TrackAnnotationTargetArmed(target.Label, target.IsHover, target.Step);
      Task.Run(() => this.WatchGuidedTargetAsync(target, loop.Token));
      ClickTargetRing ring = new ClickTargetRing(target.CenterPx, target.RadiusPx, target.ControlFramePx, target.Label);
      Action <>9__3;
      Task.Delay(450).ContinueWith(delegate(Task _)
      {
        if (this.IsArmed(target.Id))
        {
          UiInvoker uiInvoker = this.ui;
          Action action;
          if ((action = <>9__3) == null)
          {
            action = (<>9__3 = delegate
            {
              this.cursor.ShowClickTarget(ring);
            });
          }
          uiInvoker.Post(action);
        }
      }, TaskScheduler.Default);
    }

    // Token: 0x0600062C RID: 1580 RVA: 0x0035E3A4 File Offset: 0x0035E3A4
    private bool IsArmed(long id)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        CompanionSession.GuidedTarget guidedTarget = this.guidedTarget;
        flag2 = guidedTarget != null && guidedTarget.Id == id;
      }
      return flag2;
    }

    // Token: 0x0600062D RID: 1581 RVA: 0x0035E3F8 File Offset: 0x0035E3F8
    [NullableContext(2)]
    private void DisarmGuidedTarget(string reason)
    {
      object obj = this.gate;
      CompanionSession.GuidedTarget guidedTarget;
      IDisposable disposable;
      CancellationTokenSource cancellationTokenSource;
      lock (obj)
      {
        guidedTarget = this.guidedTarget;
        disposable = this.guidedPressWatch;
        cancellationTokenSource = this.guidedWatchLoop;
        this.guidedTarget = null;
        this.guidedPressWatch = null;
        this.guidedWatchLoop = null;
      }
      if (guidedTarget == null)
      {
        return;
      }
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      if (disposable != null)
      {
        disposable.Dispose();
      }
      this.ui.Post(delegate
      {
        this.cursor.HideClickTarget();
      });
      if (reason != null)
      {
        Log.Info("Companion", "guided target cleared label=\"" + (guidedTarget.Label ?? "target") + "\": " + reason);
      }
    }

    // Token: 0x0600062E RID: 1582 RVA: 0x0035E4B8 File Offset: 0x0035E4B8
    private async Task WatchGuidedTargetAsync(CompanionSession.GuidedTarget target, CancellationToken ct)
    {
      double restSeconds = 0.0;
      int ticks = 0;
      try
      {
        while (!ct.IsCancellationRequested)
        {
          await Task.Delay(120, ct).ConfigureAwait(false);
          if (!this.IsArmed(target.Id))
          {
            break;
          }
          if ((double)(Environment.TickCount64 - target.ArmedTicks) / 1000.0 > 600.0)
          {
            this.DisarmGuidedTarget("expired");
            break;
          }
          if (++ticks % 4 == 0)
          {
            string frontProcess = target.FrontProcess;
            if (frontProcess != null && frontProcess.Length > 0)
            {
              string item = this.options.DesktopActions.FrontApplication().Item2;
              if (item != null && item.Length > 0 && !item.Equals(frontProcess, StringComparison.OrdinalIgnoreCase) && !item.Equals(CompanionSession.OwnProcessName, StringComparison.OrdinalIgnoreCase))
              {
                this.DisarmGuidedTarget("another app came to the front (" + item + ")");
                break;
              }
            }
          }
          if (target.IsHover)
          {
            IPointerInput pointerInput = this.options.PointerInput;
            if (pointerInput != null)
            {
              PointD cursorPosition = pointerInput.CursorPosition;
              restSeconds = (target.Contains(cursorPosition) ? (restSeconds + 0.12) : 0.0);
              if (restSeconds >= 2.0)
              {
                Log.Info("Companion", "guided hover-rest complete label=\"" + (target.Label ?? "target") + "\"");
                this.CompleteGuidedTarget(target, cursorPosition, true);
                break;
              }
            }
          }
        }
      }
      catch (OperationCanceledException)
      {
      }
    }

    // Token: 0x0600062F RID: 1583 RVA: 0x0035E50C File Offset: 0x0035E50C
    private void OnGuidedPress(long targetId, PointD press)
    {
      object obj = this.gate;
      CompanionSession.GuidedTarget guidedTarget;
      lock (obj)
      {
        guidedTarget = this.guidedTarget;
      }
      if (guidedTarget == null || guidedTarget.Id != targetId)
      {
        return;
      }
      if ((double)(Environment.TickCount64 - guidedTarget.ArmedTicks) / 1000.0 > 600.0 || guidedTarget.Step >= 15)
      {
        this.DisarmGuidedTarget("expired");
        return;
      }
      if (!guidedTarget.Contains(press))
      {
        string text = guidedTarget.Label ?? "the target";
        string text2 = "Companion";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(64, 3);
        defaultInterpolatedStringHandler.AppendLiteral("guided target off-target click at ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(press.X, "0");
        defaultInterpolatedStringHandler.AppendLiteral(",");
        defaultInterpolatedStringHandler.AppendFormatted<double>(press.Y, "0");
        defaultInterpolatedStringHandler.AppendLiteral(": ending the guide (label \"");
        defaultInterpolatedStringHandler.AppendFormatted(text);
        defaultInterpolatedStringHandler.AppendLiteral("\")");
        Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
        this.brain.NoteForNextTurn("During a guided step I pointed at \"" + text + "\" and asked them to click it, but they clicked elsewhere — the guide ended and that step was likely not done.", null);
        RectD displayBoundsPx = guidedTarget.DisplayBoundsPx;
        bool flag2 = new RectD(displayBoundsPx.X - guidedTarget.RadiusPx, displayBoundsPx.Y - guidedTarget.RadiusPx, displayBoundsPx.Width + 2.0 * guidedTarget.RadiusPx, displayBoundsPx.Height + 2.0 * guidedTarget.RadiusPx).Contains(press);
        ClickyAnalytics.TrackAnnotationTargetMissed(guidedTarget.Label, 1, flag2);
        this.DisarmGuidedTarget("clicked elsewhere");
        return;
      }
      this.CompleteGuidedTarget(guidedTarget, press, false);
    }

    // Token: 0x06000630 RID: 1584 RVA: 0x0035E6CC File Offset: 0x0035E6CC
    private void CompleteGuidedTarget(CompanionSession.GuidedTarget target, PointD press, bool completedByHoverRest)
    {
      CompanionSession.<>c__DisplayClass305_0 CS$<>8__locals1 = new CompanionSession.<>c__DisplayClass305_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.target = target;
      if (!this.IsArmed(CS$<>8__locals1.target.Id))
      {
        return;
      }
      this.DisarmGuidedTarget(null);
      CS$<>8__locals1.step = CS$<>8__locals1.target.Step + 1;
      string text = CS$<>8__locals1.target.Label ?? "target";
      string text2 = "Companion";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(64, 4);
      defaultInterpolatedStringHandler.AppendLiteral("guided target hit label=\"");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("\" distance=");
      defaultInterpolatedStringHandler.AppendFormatted<double>(CompanionSession.Distance(press, CS$<>8__locals1.target.CenterPx), "0");
      defaultInterpolatedStringHandler.AppendLiteral("px hoverRest=");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(completedByHoverRest);
      defaultInterpolatedStringHandler.AppendLiteral(": step ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.step);
      defaultInterpolatedStringHandler.AppendLiteral(" follows");
      Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
      ClickyAnalytics.TrackAnnotationTargetCompleted(CS$<>8__locals1.target.Label, CS$<>8__locals1.target.IsHover, completedByHoverRest, CS$<>8__locals1.target.Step);
      ClickyAnalytics.TrackAnnotationFollowUpRequested(true, CS$<>8__locals1.step);
      this.InterruptForNewInput(true);
      object obj = this.gate;
      lock (obj)
      {
        CS$<>8__locals1.turnGeneration = this.generation;
      }
      this.SetState(VoiceState.Processing);
      CompanionSession.<>c__DisplayClass305_0 CS$<>8__locals2 = CS$<>8__locals1;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(76, 2);
      defaultInterpolatedStringHandler.AppendLiteral("[App] The user ");
      defaultInterpolatedStringHandler.AppendFormatted(completedByHoverRest ? "rested the cursor on" : "clicked");
      defaultInterpolatedStringHandler.AppendLiteral(" \"");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("\", the step your drawing asked for, and the screen changed.");
      CS$<>8__locals2.fact = defaultInterpolatedStringHandler.ToStringAndClear();
      this.WatchForPassiveExitWhileTheNextStepLoads(CS$<>8__locals1.turnGeneration, CS$<>8__locals1.target.FrontProcess);
      Task.Run(delegate
      {
        CompanionSession.<>c__DisplayClass305_0.<<CompleteGuidedTarget>b__0>d <<CompleteGuidedTarget>b__0>d;
        <<CompleteGuidedTarget>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<CompleteGuidedTarget>b__0>d.<>4__this = CS$<>8__locals1;
        <<CompleteGuidedTarget>b__0>d.<>1__state = -1;
        <<CompleteGuidedTarget>b__0>d.<>t__builder.Start<CompanionSession.<>c__DisplayClass305_0.<<CompleteGuidedTarget>b__0>d>(ref <<CompleteGuidedTarget>b__0>d);
        return <<CompleteGuidedTarget>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x06000631 RID: 1585 RVA: 0x0035E8C0 File Offset: 0x0035E8C0
    [NullableContext(2)]
    private void WatchForPassiveExitWhileTheNextStepLoads(long turnGeneration, string sourceProcess)
    {
      CompanionSession.<>c__DisplayClass308_0 CS$<>8__locals1 = new CompanionSession.<>c__DisplayClass308_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.sourceProcess = sourceProcess;
      CS$<>8__locals1.turnGeneration = turnGeneration;
      Interlocked.Exchange(ref this.guidedStepLoadingGeneration, CS$<>8__locals1.turnGeneration);
      if (CS$<>8__locals1.sourceProcess == null || CS$<>8__locals1.sourceProcess.Length <= 0)
      {
        return;
      }
      CS$<>8__locals1.graceEnds = Environment.TickCount64 + (long)CompanionSession.PassiveExitGrace.TotalMilliseconds;
      Task.Run(delegate
      {
        CompanionSession.<>c__DisplayClass308_0.<<WatchForPassiveExitWhileTheNextStepLoads>b__0>d <<WatchForPassiveExitWhileTheNextStepLoads>b__0>d;
        <<WatchForPassiveExitWhileTheNextStepLoads>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<WatchForPassiveExitWhileTheNextStepLoads>b__0>d.<>4__this = CS$<>8__locals1;
        <<WatchForPassiveExitWhileTheNextStepLoads>b__0>d.<>1__state = -1;
        <<WatchForPassiveExitWhileTheNextStepLoads>b__0>d.<>t__builder.Start<CompanionSession.<>c__DisplayClass308_0.<<WatchForPassiveExitWhileTheNextStepLoads>b__0>d>(ref <<WatchForPassiveExitWhileTheNextStepLoads>b__0>d);
        return <<WatchForPassiveExitWhileTheNextStepLoads>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x06000632 RID: 1586 RVA: 0x0035E93C File Offset: 0x0035E93C
    private void NoteGuidedStepShowed()
    {
      Interlocked.Exchange(ref this.guidedStepLoadingGeneration, -1L);
    }

    // Token: 0x06000633 RID: 1587 RVA: 0x0035E94C File Offset: 0x0035E94C
    private void PassivelyExitGuidedFollowUp()
    {
      Interlocked.Exchange(ref this.guidedStepLoadingGeneration, -1L);
      this.InterruptForNewInput(false);
      this.brain.ForgetDrawingOnScreen();
      this.ui.Post(delegate
      {
        this.canvas.IsStreamingNarration = false;
        this.canvas.Clear(false);
      });
      this.NotifyTerminating();
      this.SetState(VoiceState.Idle);
    }

    // Token: 0x06000634 RID: 1588 RVA: 0x0035E9A0 File Offset: 0x0035E9A0
    [return: Nullable(new byte[] { 1, 2 })]
    private async Task<byte[]> CaptureDisplayJpegAsync(RectD displayBounds)
    {
      IReadOnlyList<ScreenCaptureFrame> readOnlyList = await this.screens.CaptureAllAsync(256, 60, default(CancellationToken)).ConfigureAwait(false);
      ScreenCaptureFrame screenCaptureFrame = readOnlyList.FirstOrDefault<ScreenCaptureFrame>((ScreenCaptureFrame f) => f.Display.BoundsPx == displayBounds) ?? readOnlyList.FirstOrDefault<ScreenCaptureFrame>();
      return (screenCaptureFrame != null) ? screenCaptureFrame.Jpeg : null;
    }

    // Token: 0x06000635 RID: 1589 RVA: 0x0035E9EC File Offset: 0x0035E9EC
    private void NoteTurnEndedForWalkthrough(CompanionSession.NarrationState narration, CompanionSession.TurnRequest request, ScreenCaptureFrame screen)
    {
      if (this.ArmedClickTarget != null)
      {
        this.DisarmAdvance();
        return;
      }
      object obj = narration.Gate;
      List<string> list;
      int marks;
      lock (obj)
      {
        list = narration.Lines.Skip<string>(narration.FirstLine).ToList<string>();
        marks = narration.Marks;
      }
      if (!CompanionSession.ShouldArmAdvance(list, marks > 0))
      {
        this.DisarmAdvance();
        return;
      }
      obj = this.gate;
      lock (obj)
      {
        this.advanceArmedUntilTicks = Environment.TickCount64 + 120000L;
        this.advanceTurn = request;
        this.advanceBaseline = screen;
      }
      Log.Info("Companion", "a step with no click target: the next click or scroll that changes the screen moves on");
    }

    // Token: 0x06000636 RID: 1590 RVA: 0x0035EAC0 File Offset: 0x0035EAC0
    internal static bool ShouldArmAdvance(IReadOnlyList<string> lines, bool drewMarks)
    {
      if (!drewMarks || lines.Count == 0)
      {
        return false;
      }
      string text = lines[lines.Count - 1].Trim();
      return text.Length > 0 && !text.EndsWith('?') && CompanionSession.StepInstructionPattern.IsMatch(text);
    }

    // Token: 0x06000637 RID: 1591 RVA: 0x0035EB10 File Offset: 0x0035EB10
    private void DisarmAdvance()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.advanceArmedUntilTicks = 0L;
        this.advanceTurn = null;
        this.advanceBaseline = null;
      }
    }

    // Token: 0x06000638 RID: 1592 RVA: 0x0035EB60 File Offset: 0x0035EB60
    private void OnCanvasUserActedOnScreen([Nullable(2)] object sender, string reason)
    {
      CompanionSession.<>c__DisplayClass315_0 CS$<>8__locals1 = new CompanionSession.<>c__DisplayClass315_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.reason = reason;
      object obj = this.gate;
      lock (obj)
      {
        if (this.advanceTurn == null || Environment.TickCount64 > this.advanceArmedUntilTicks || this.isListening)
        {
          return;
        }
        CS$<>8__locals1.turn = this.advanceTurn;
        CS$<>8__locals1.baseline = this.advanceBaseline;
      }
      if (CS$<>8__locals1.baseline == null)
      {
        return;
      }
      Task.Run(delegate
      {
        CompanionSession.<>c__DisplayClass315_0.<<OnCanvasUserActedOnScreen>b__0>d <<OnCanvasUserActedOnScreen>b__0>d;
        <<OnCanvasUserActedOnScreen>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<OnCanvasUserActedOnScreen>b__0>d.<>4__this = CS$<>8__locals1;
        <<OnCanvasUserActedOnScreen>b__0>d.<>1__state = -1;
        <<OnCanvasUserActedOnScreen>b__0>d.<>t__builder.Start<CompanionSession.<>c__DisplayClass315_0.<<OnCanvasUserActedOnScreen>b__0>d>(ref <<OnCanvasUserActedOnScreen>b__0>d);
        return <<OnCanvasUserActedOnScreen>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x06000639 RID: 1593 RVA: 0x0035EC00 File Offset: 0x0035EC00
    private static double Distance(PointD a, PointD b)
    {
      return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
    }

    // Token: 0x06000644 RID: 1604 RVA: 0x0035EDA4 File Offset: 0x0035EDA4
    [NullableContext(2)]
    [CompilerGenerated]
    internal static string <ArmGuidedTargetIfLastBeatAsync>g__IfNotOurs|299_1(string process)
    {
      if (process == null || process.Length <= 0 || process.Equals(CompanionSession.OwnProcessName, StringComparison.OrdinalIgnoreCase))
      {
        return null;
      }
      return process;
    }

    // Token: 0x04000289 RID: 649
    private const string Area = "Companion";

    // Token: 0x0400028A RID: 650
    private const int MaximumToolRounds = 5;

    // Token: 0x0400028B RID: 651
    private static readonly TimeSpan ReleaseTail = TimeSpan.FromMilliseconds(150L, 0L);

    // Token: 0x0400028C RID: 652
    private const int MaximumPressAudioSeconds = 120;

    // Token: 0x0400028D RID: 653
    private readonly ClickyHttp http;

    // Token: 0x0400028E RID: 654
    private readonly SupabaseAuthService auth;

    // Token: 0x0400028F RID: 655
    private readonly BrainClient brainClient;

    // Token: 0x04000290 RID: 656
    private readonly BrainSession brain;

    // Token: 0x04000291 RID: 657
    private readonly SettingsStore settings;

    // Token: 0x04000292 RID: 658
    private readonly IMicrophoneCapture microphone;

    // Token: 0x04000293 RID: 659
    private readonly IAudioOutput audio;

    // Token: 0x04000294 RID: 660
    private readonly IScreenCaptureService screens;

    // Token: 0x04000295 RID: 661
    private readonly IScreenDrawingCanvas canvas;

    // Token: 0x04000296 RID: 662
    private readonly ICursorOverlay cursor;

    // Token: 0x04000297 RID: 663
    [Nullable(2)]
    private readonly IHighlightOverlay highlight;

    // Token: 0x04000298 RID: 664
    private readonly INotchPresenter notch;

    // Token: 0x04000299 RID: 665
    private readonly CompanionSessionOptions options;

    // Token: 0x0400029A RID: 666
    private readonly UiInvoker ui;

    // Token: 0x0400029B RID: 667
    private readonly RealtimeMouth mouth;

    // Token: 0x0400029C RID: 668
    [Nullable(2)]
    private readonly PageReads pageReads;

    // Token: 0x0400029D RID: 669
    private readonly object gate = new object();

    // Token: 0x0400029E RID: 670
    private VoiceState state;

    // Token: 0x0400029F RID: 671
    private string responseText = "";

    // Token: 0x040002A0 RID: 672
    private long generation;

    // Token: 0x040002A1 RID: 673
    [Nullable(2)]
    private CancellationTokenSource turnCancellation;

    // Token: 0x040002A2 RID: 674
    private int activeTurnCount;

    // Token: 0x040002A3 RID: 675
    private bool isListening;

    // Token: 0x040002A4 RID: 676
    private bool pressWasRefused;

    // Token: 0x040002A5 RID: 677
    private long pressStartedTicks;

    // Token: 0x040002A6 RID: 678
    [Nullable(2)]
    private GeminiTranscriber ears;

    // Token: 0x040002A7 RID: 679
    private byte[] pressAudio = Array.Empty<byte>();

    // Token: 0x040002A8 RID: 680
    private int pressAudioLength;

    // Token: 0x040002A9 RID: 681
    private int pressFrameCount;

    // Token: 0x040002AA RID: 682
    private int consecutiveDeadAirPresses;

    // Token: 0x040002AB RID: 683
    [Nullable(2)]
    private CancellationTokenSource liveTranscriptLoop;

    // Token: 0x040002AC RID: 684
    [Nullable(2)]
    private GeminiTranscriber warmEars;

    // Token: 0x040002AD RID: 685
    private long warmEarsExpiresTicks;

    // Token: 0x040002AE RID: 686
    private readonly Dictionary<int, double> lineAudioOffsets = new Dictionary<int, double>();

    // Token: 0x040002AF RID: 687
    private readonly Dictionary<int, double> lineGeneratedSeconds = new Dictionary<int, double>();

    // Token: 0x040002B0 RID: 688
    private readonly SemaphoreSlim narrationSignal = new SemaphoreSlim(0, int.MaxValue);

    // Token: 0x040002B1 RID: 689
    private long turnStartedTicks;

    // Token: 0x040002B2 RID: 690
    private int? firstAudioMs;

    // Token: 0x040002B3 RID: 691
    [Nullable(2)]
    private string waitStatusLabel;

    // Token: 0x040002BD RID: 701
    private volatile bool inFlightTurnIsOnboarding;

    // Token: 0x040002BE RID: 702
    private string currentTurnProgramSoFar = "";

    // Token: 0x040002C0 RID: 704
    private static readonly TimeSpan HighlightTextBudget = TimeSpan.FromMilliseconds(900L, 0L);

    // Token: 0x040002C1 RID: 705
    private volatile bool lastTurnWasTyped;

    // Token: 0x040002C2 RID: 706
    private volatile bool lastTurnAnsweredInText;

    // Token: 0x040002C3 RID: 707
    public static volatile bool SendsLibraryItems = true;

    // Token: 0x040002C4 RID: 708
    private volatile bool captionsTextAnswer;

    // Token: 0x040002C6 RID: 710
    private volatile bool typedAnswerCaptionRequested;

    // Token: 0x040002C7 RID: 711
    private int audioPlaybackBeganCount;

    // Token: 0x040002C8 RID: 712
    internal const string PlaceholderScreenshot = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAMCAgMCAgMDAwMEAwMEBQgFBQQEBQoHBwYIDAoMDAsKCwsNDhIQDQ4RDgsLEBYQERMUFRUVDA8XGBYUGBIUFRT/2wBDAQMEBAUEBQkFBQkUDQsNFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBT/wAARCAAIAAgDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwAooooA/9k=";

    // Token: 0x040002C9 RID: 713
    [Nullable(2)]
    private CompanionSession.HeldActions actionsWaitingForTheDrawing;

    // Token: 0x040002CA RID: 714
    public const int LessonMinimumLineCount = 3;

    // Token: 0x040002CD RID: 717
    [Nullable(2)]
    private CompanionSession.LessonRecording lessonRecording;

    // Token: 0x040002CE RID: 718
    private bool listensForLessonAudio;

    // Token: 0x040002CF RID: 719
    private bool hasReportedIrrecoverableMicrophone;

    // Token: 0x040002D0 RID: 720
    [Nullable(2)]
    private CancellationTokenSource bluetoothHold;

    // Token: 0x040002D1 RID: 721
    private readonly object prefetchGate = new object();

    // Token: 0x040002D2 RID: 722
    [Nullable(2)]
    private CompanionSession.PrefetchedTurn pendingPrefetchedTurn;

    // Token: 0x040002D4 RID: 724
    internal const int WalkthroughMaximumSteps = 15;

    // Token: 0x040002D5 RID: 725
    private const double GuidedTargetMaximumAgeSeconds = 600.0;

    // Token: 0x040002D6 RID: 726
    private const double GuidedTargetMinimumRadiusPoints = 28.0;

    // Token: 0x040002D7 RID: 727
    private const double GuidedTargetMaximumRadiusPoints = 180.0;

    // Token: 0x040002D8 RID: 728
    private const double GuidedTargetSmallestModelRadius = 14.0;

    // Token: 0x040002D9 RID: 729
    private const double ElementFrameHitSlackPoints = 8.0;

    // Token: 0x040002DA RID: 730
    private const double HoverRestSeconds = 2.0;

    // Token: 0x040002DB RID: 731
    private const int GuidedWatchTickMilliseconds = 120;

    // Token: 0x040002DC RID: 732
    private const int RingRevealDelayMilliseconds = 450;

    // Token: 0x040002DD RID: 733
    private static readonly TimeSpan PostClickCaptureFallback = TimeSpan.FromMilliseconds(900L, 0L);

    // Token: 0x040002DE RID: 734
    private const int AdvanceArmedMilliseconds = 120000;

    // Token: 0x040002DF RID: 735
    private const double StepScreenChangeTolerance = 0.03;

    // Token: 0x040002E0 RID: 736
    private static readonly Regex StepInstructionPattern = new Regex("(^|[.!,;:]\\s*|\\b(now|then|next|and)\\s+)(drag|double-click|click|select|choose|scroll|move|extend|stretch|lengthen|resize|pull|slide|grab|press|hold|release|type|enter|pick|set|turn|tap|put|add|draw|open)\\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

    // Token: 0x040002E1 RID: 737
    private static readonly string OwnProcessName = Path.GetFileNameWithoutExtension(Environment.ProcessPath ?? "HeyClicky");

    // Token: 0x040002E2 RID: 738
    [Nullable(2)]
    private CompanionSession.GuidedTarget guidedTarget;

    // Token: 0x040002E3 RID: 739
    [Nullable(2)]
    private IDisposable guidedPressWatch;

    // Token: 0x040002E4 RID: 740
    [Nullable(2)]
    private CancellationTokenSource guidedWatchLoop;

    // Token: 0x040002E5 RID: 741
    private long guidedTargetIds;

    // Token: 0x040002E6 RID: 742
    private long advanceArmedUntilTicks;

    // Token: 0x040002E7 RID: 743
    [Nullable(2)]
    private CompanionSession.TurnRequest advanceTurn;

    // Token: 0x040002E8 RID: 744
    [Nullable(2)]
    private ScreenCaptureFrame advanceBaseline;

    // Token: 0x040002EA RID: 746
    private static readonly TimeSpan PassiveExitGrace = TimeSpan.FromSeconds(1.4);

    // Token: 0x040002EB RID: 747
    private long guidedStepLoadingGeneration = -1L;

    // Token: 0x020002B2 RID: 690
    [NullableContext(0)]
    private sealed class SilentAudioOutput : IAudioOutput, IDisposable
    {
      // Token: 0x06002588 RID: 9608 RVA: 0x00400140 File Offset: 0x00400140
      public SilentAudioOutput(int sampleRate)
      {
      }

      // Token: 0x17000771 RID: 1905
      // (get) Token: 0x06002589 RID: 9609 RVA: 0x00400150 File Offset: 0x00400150
      public int SampleRate
      {
        get
        {
          return this.<sampleRate>P;
        }
      } = sampleRate;

      // Token: 0x0600258A RID: 9610 RVA: 0x00400158 File Offset: 0x00400158
      public void Enqueue(ReadOnlySpan<byte> pcm16Mono)
      {
      }

      // Token: 0x0600258B RID: 9611 RVA: 0x0040015C File Offset: 0x0040015C
      public void Flush()
      {
      }

      // Token: 0x17000772 RID: 1906
      // (get) Token: 0x0600258C RID: 9612 RVA: 0x00400160 File Offset: 0x00400160
      public TimeSpan Buffered
      {
        get
        {
          return TimeSpan.Zero;
        }
      }

      // Token: 0x17000773 RID: 1907
      // (get) Token: 0x0600258D RID: 9613 RVA: 0x00400168 File Offset: 0x00400168
      public double PlayedSeconds
      {
        get
        {
          return 0.0;
        }
      }

      // Token: 0x17000774 RID: 1908
      // (get) Token: 0x0600258E RID: 9614 RVA: 0x00400174 File Offset: 0x00400174
      public float Level
      {
        get
        {
          return 0f;
        }
      }

      // Token: 0x0600258F RID: 9615 RVA: 0x0040017C File Offset: 0x0040017C
      [NullableContext(1)]
      public void PlayEarcon(string name)
      {
      }

      // Token: 0x06002590 RID: 9616 RVA: 0x00400180 File Offset: 0x00400180
      public void Dispose()
      {
      }

      // Token: 0x04001292 RID: 4754
      [CompilerGenerated]
      private int <sampleRate>P;
    }

    // Token: 0x020002B3 RID: 691
    [Nullable(0)]
    internal sealed class TurnCapture : IEquatable<CompanionSession.TurnCapture>
    {
      // Token: 0x06002591 RID: 9617 RVA: 0x00400184 File Offset: 0x00400184
      public TurnCapture(IReadOnlyList<ScreenCaptureFrame> Frames, IReadOnlyList<DisplayInfo> Displays, [Nullable(2)] string HighlightGesture, int CaptureMs, [Nullable(2)] string HighlightText = null)
      {
        this.Frames = Frames;
        this.Displays = Displays;
        this.HighlightGesture = HighlightGesture;
        this.CaptureMs = CaptureMs;
        this.HighlightText = HighlightText;
        base..ctor();
      }

      // Token: 0x17000775 RID: 1909
      // (get) Token: 0x06002592 RID: 9618 RVA: 0x004001B4 File Offset: 0x004001B4
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(CompanionSession.TurnCapture);
        }
      }

      // Token: 0x17000776 RID: 1910
      // (get) Token: 0x06002593 RID: 9619 RVA: 0x004001C0 File Offset: 0x004001C0
      // (set) Token: 0x06002594 RID: 9620 RVA: 0x004001C8 File Offset: 0x004001C8
      public IReadOnlyList<ScreenCaptureFrame> Frames { get; set; }

      // Token: 0x17000777 RID: 1911
      // (get) Token: 0x06002595 RID: 9621 RVA: 0x004001D4 File Offset: 0x004001D4
      // (set) Token: 0x06002596 RID: 9622 RVA: 0x004001DC File Offset: 0x004001DC
      public IReadOnlyList<DisplayInfo> Displays { get; set; }

      // Token: 0x17000778 RID: 1912
      // (get) Token: 0x06002597 RID: 9623 RVA: 0x004001E8 File Offset: 0x004001E8
      // (set) Token: 0x06002598 RID: 9624 RVA: 0x004001F0 File Offset: 0x004001F0
      [Nullable(2)]
      public string HighlightGesture
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x17000779 RID: 1913
      // (get) Token: 0x06002599 RID: 9625 RVA: 0x004001FC File Offset: 0x004001FC
      // (set) Token: 0x0600259A RID: 9626 RVA: 0x00400204 File Offset: 0x00400204
      public int CaptureMs { get; set; }

      // Token: 0x1700077A RID: 1914
      // (get) Token: 0x0600259B RID: 9627 RVA: 0x00400210 File Offset: 0x00400210
      // (set) Token: 0x0600259C RID: 9628 RVA: 0x00400218 File Offset: 0x00400218
      [Nullable(2)]
      public string HighlightText
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x1700077B RID: 1915
      // (get) Token: 0x0600259D RID: 9629 RVA: 0x00400224 File Offset: 0x00400224
      // (set) Token: 0x0600259E RID: 9630 RVA: 0x0040022C File Offset: 0x0040022C
      [Nullable(new byte[] { 2, 1 })]
      public IReadOnlyList<ScreenCaptureFrame> FramesWithoutInk
      {
        [return: Nullable(new byte[] { 2, 1 })]
        get;
        [param: Nullable(new byte[] { 2, 1 })]
        set;
      }

      // Token: 0x0600259F RID: 9631 RVA: 0x00400238 File Offset: 0x00400238
      public ScreenCaptureFrame WithoutUserInk(ScreenCaptureFrame frame)
      {
        IReadOnlyList<ScreenCaptureFrame> framesWithoutInk = this.FramesWithoutInk;
        if (framesWithoutInk == null)
        {
          return frame;
        }
        int num = 0;
        while (num < this.Frames.Count && num < framesWithoutInk.Count)
        {
          if (this.Frames[num] == frame)
          {
            return framesWithoutInk[num];
          }
          num++;
        }
        return frame;
      }

      // Token: 0x060025A0 RID: 9632 RVA: 0x00400288 File Offset: 0x00400288
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("TurnCapture");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060025A1 RID: 9633 RVA: 0x004002D4 File Offset: 0x004002D4
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Frames = ");
        builder.Append(this.Frames);
        builder.Append(", Displays = ");
        builder.Append(this.Displays);
        builder.Append(", HighlightGesture = ");
        builder.Append(this.HighlightGesture);
        builder.Append(", CaptureMs = ");
        builder.Append(this.CaptureMs.ToString());
        builder.Append(", HighlightText = ");
        builder.Append(this.HighlightText);
        builder.Append(", FramesWithoutInk = ");
        builder.Append(this.FramesWithoutInk);
        return true;
      }

      // Token: 0x060025A2 RID: 9634 RVA: 0x0040038C File Offset: 0x0040038C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(CompanionSession.TurnCapture left, CompanionSession.TurnCapture right)
      {
        return !(left == right);
      }

      // Token: 0x060025A3 RID: 9635 RVA: 0x00400398 File Offset: 0x00400398
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(CompanionSession.TurnCapture left, CompanionSession.TurnCapture right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060025A4 RID: 9636 RVA: 0x004003AC File Offset: 0x004003AC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<ScreenCaptureFrame>>.Default.GetHashCode(this.<Frames>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<DisplayInfo>>.Default.GetHashCode(this.<Displays>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<HighlightGesture>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<CaptureMs>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<HighlightText>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ScreenCaptureFrame>>.Default.GetHashCode(this.<FramesWithoutInk>k__BackingField);
      }

      // Token: 0x060025A5 RID: 9637 RVA: 0x00400454 File Offset: 0x00400454
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as CompanionSession.TurnCapture);
      }

      // Token: 0x060025A6 RID: 9638 RVA: 0x00400464 File Offset: 0x00400464
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(CompanionSession.TurnCapture other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<ScreenCaptureFrame>>.Default.Equals(this.<Frames>k__BackingField, other.<Frames>k__BackingField) && EqualityComparer<IReadOnlyList<DisplayInfo>>.Default.Equals(this.<Displays>k__BackingField, other.<Displays>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<HighlightGesture>k__BackingField, other.<HighlightGesture>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<CaptureMs>k__BackingField, other.<CaptureMs>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<HighlightText>k__BackingField, other.<HighlightText>k__BackingField) && EqualityComparer<IReadOnlyList<ScreenCaptureFrame>>.Default.Equals(this.<FramesWithoutInk>k__BackingField, other.<FramesWithoutInk>k__BackingField));
      }

      // Token: 0x060025A8 RID: 9640 RVA: 0x00400530 File Offset: 0x00400530
      [CompilerGenerated]
      private TurnCapture(CompanionSession.TurnCapture original)
      {
        this.Frames = original.<Frames>k__BackingField;
        this.Displays = original.<Displays>k__BackingField;
        this.HighlightGesture = original.<HighlightGesture>k__BackingField;
        this.CaptureMs = original.<CaptureMs>k__BackingField;
        this.HighlightText = original.<HighlightText>k__BackingField;
        this.FramesWithoutInk = original.<FramesWithoutInk>k__BackingField;
      }

      // Token: 0x060025A9 RID: 9641 RVA: 0x0040058C File Offset: 0x0040058C
      [CompilerGenerated]
      public void Deconstruct(out IReadOnlyList<ScreenCaptureFrame> Frames, out IReadOnlyList<DisplayInfo> Displays, [Nullable(2)] out string HighlightGesture, out int CaptureMs, [Nullable(2)] out string HighlightText)
      {
        Frames = this.Frames;
        Displays = this.Displays;
        HighlightGesture = this.HighlightGesture;
        CaptureMs = this.CaptureMs;
        HighlightText = this.HighlightText;
      }
    }

    // Token: 0x020002B4 RID: 692
    [NullableContext(2)]
    [Nullable(0)]
    internal sealed class TurnRequest : IEquatable<CompanionSession.TurnRequest>
    {
      // Token: 0x060025AA RID: 9642 RVA: 0x004005B8 File Offset: 0x004005B8
      public TurnRequest(string Words, string Fact, bool Typed, bool AnswersInText, CompanionSession.TurnCapture Capture, [TupleElementNames(new string[] { "DisplayName", "ProcessName" })] [Nullable(new byte[] { 0, 2, 2 })] ValueTuple<string, string> FrontApp, double? AudioSeconds, [Nullable(1)] string Trigger, long StartedTicks, int TranscriptMs, int? WalkthroughStep = null)
      {
        this.Words = Words;
        this.Fact = Fact;
        this.Typed = Typed;
        this.AnswersInText = AnswersInText;
        this.Capture = Capture;
        this.FrontApp = FrontApp;
        this.AudioSeconds = AudioSeconds;
        this.Trigger = Trigger;
        this.StartedTicks = StartedTicks;
        this.TranscriptMs = TranscriptMs;
        this.WalkthroughStep = WalkthroughStep;
        this.AttachedFilePaths = Array.Empty<string>();
        base..ctor();
      }

      // Token: 0x1700077C RID: 1916
      // (get) Token: 0x060025AB RID: 9643 RVA: 0x0040062C File Offset: 0x0040062C
      [Nullable(1)]
      [CompilerGenerated]
      private Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(CompanionSession.TurnRequest);
        }
      }

      // Token: 0x1700077D RID: 1917
      // (get) Token: 0x060025AC RID: 9644 RVA: 0x00400638 File Offset: 0x00400638
      // (set) Token: 0x060025AD RID: 9645 RVA: 0x00400640 File Offset: 0x00400640
      public string Words { get; set; }

      // Token: 0x1700077E RID: 1918
      // (get) Token: 0x060025AE RID: 9646 RVA: 0x0040064C File Offset: 0x0040064C
      // (set) Token: 0x060025AF RID: 9647 RVA: 0x00400654 File Offset: 0x00400654
      public string Fact { get; set; }

      // Token: 0x1700077F RID: 1919
      // (get) Token: 0x060025B0 RID: 9648 RVA: 0x00400660 File Offset: 0x00400660
      // (set) Token: 0x060025B1 RID: 9649 RVA: 0x00400668 File Offset: 0x00400668
      public bool Typed { get; set; }

      // Token: 0x17000780 RID: 1920
      // (get) Token: 0x060025B2 RID: 9650 RVA: 0x00400674 File Offset: 0x00400674
      // (set) Token: 0x060025B3 RID: 9651 RVA: 0x0040067C File Offset: 0x0040067C
      public bool AnswersInText { get; set; }

      // Token: 0x17000781 RID: 1921
      // (get) Token: 0x060025B4 RID: 9652 RVA: 0x00400688 File Offset: 0x00400688
      // (set) Token: 0x060025B5 RID: 9653 RVA: 0x00400690 File Offset: 0x00400690
      public CompanionSession.TurnCapture Capture { get; set; }

      // Token: 0x17000782 RID: 1922
      // (get) Token: 0x060025B6 RID: 9654 RVA: 0x0040069C File Offset: 0x0040069C
      // (set) Token: 0x060025B7 RID: 9655 RVA: 0x004006A4 File Offset: 0x004006A4
      [TupleElementNames(new string[] { "DisplayName", "ProcessName" })]
      [Nullable(new byte[] { 0, 2, 2 })]
      public ValueTuple<string, string> FrontApp
      {
        [return: TupleElementNames(new string[] { "DisplayName", "ProcessName" })]
        [return: Nullable(new byte[] { 0, 2, 2 })]
        get;
        [param: TupleElementNames(new string[] { "DisplayName", "ProcessName" })]
        [param: Nullable(new byte[] { 0, 2, 2 })]
        set;
      }

      // Token: 0x17000783 RID: 1923
      // (get) Token: 0x060025B8 RID: 9656 RVA: 0x004006B0 File Offset: 0x004006B0
      // (set) Token: 0x060025B9 RID: 9657 RVA: 0x004006B8 File Offset: 0x004006B8
      public double? AudioSeconds { get; set; }

      // Token: 0x17000784 RID: 1924
      // (get) Token: 0x060025BA RID: 9658 RVA: 0x004006C4 File Offset: 0x004006C4
      // (set) Token: 0x060025BB RID: 9659 RVA: 0x004006CC File Offset: 0x004006CC
      [Nullable(1)]
      public string Trigger
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x17000785 RID: 1925
      // (get) Token: 0x060025BC RID: 9660 RVA: 0x004006D8 File Offset: 0x004006D8
      // (set) Token: 0x060025BD RID: 9661 RVA: 0x004006E0 File Offset: 0x004006E0
      public long StartedTicks { get; set; }

      // Token: 0x17000786 RID: 1926
      // (get) Token: 0x060025BE RID: 9662 RVA: 0x004006EC File Offset: 0x004006EC
      // (set) Token: 0x060025BF RID: 9663 RVA: 0x004006F4 File Offset: 0x004006F4
      public int TranscriptMs { get; set; }

      // Token: 0x17000787 RID: 1927
      // (get) Token: 0x060025C0 RID: 9664 RVA: 0x00400700 File Offset: 0x00400700
      // (set) Token: 0x060025C1 RID: 9665 RVA: 0x00400708 File Offset: 0x00400708
      public int? WalkthroughStep { get; set; }

      // Token: 0x17000788 RID: 1928
      // (get) Token: 0x060025C2 RID: 9666 RVA: 0x00400714 File Offset: 0x00400714
      // (set) Token: 0x060025C3 RID: 9667 RVA: 0x0040071C File Offset: 0x0040071C
      [Nullable(1)]
      public IReadOnlyList<string> AttachedFilePaths
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x17000789 RID: 1929
      // (get) Token: 0x060025C4 RID: 9668 RVA: 0x00400728 File Offset: 0x00400728
      // (set) Token: 0x060025C5 RID: 9669 RVA: 0x00400730 File Offset: 0x00400730
      public bool IsDrive { get; set; }

      // Token: 0x1700078A RID: 1930
      // (get) Token: 0x060025C6 RID: 9670 RVA: 0x0040073C File Offset: 0x0040073C
      // (set) Token: 0x060025C7 RID: 9671 RVA: 0x00400744 File Offset: 0x00400744
      public bool Screenless { get; set; }

      // Token: 0x1700078B RID: 1931
      // (get) Token: 0x060025C8 RID: 9672 RVA: 0x00400750 File Offset: 0x00400750
      // (set) Token: 0x060025C9 RID: 9673 RVA: 0x00400758 File Offset: 0x00400758
      public bool PointsOnly { get; set; }

      // Token: 0x1700078C RID: 1932
      // (get) Token: 0x060025CA RID: 9674 RVA: 0x00400764 File Offset: 0x00400764
      // (set) Token: 0x060025CB RID: 9675 RVA: 0x0040076C File Offset: 0x0040076C
      public JsonObject FlowBrief { get; set; }

      // Token: 0x1700078D RID: 1933
      // (get) Token: 0x060025CC RID: 9676 RVA: 0x00400778 File Offset: 0x00400778
      public string Flow
      {
        get
        {
          bool flag = this.WalkthroughStep != null;
          if (!flag)
          {
            string trigger = this.Trigger;
            bool flag2 = trigger == "clicked_target" || trigger == "did_step";
            flag = flag2;
          }
          if (!flag)
          {
            return null;
          }
          return "walkthrough";
        }
      }

      // Token: 0x060025CD RID: 9677 RVA: 0x004007D0 File Offset: 0x004007D0
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("TurnRequest");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060025CE RID: 9678 RVA: 0x0040081C File Offset: 0x0040081C
      [NullableContext(1)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Words = ");
        builder.Append(this.Words);
        builder.Append(", Fact = ");
        builder.Append(this.Fact);
        builder.Append(", Typed = ");
        builder.Append(this.Typed.ToString());
        builder.Append(", AnswersInText = ");
        builder.Append(this.AnswersInText.ToString());
        builder.Append(", Capture = ");
        builder.Append(this.Capture);
        builder.Append(", FrontApp = ");
        builder.Append(this.FrontApp.ToString());
        builder.Append(", AudioSeconds = ");
        builder.Append(this.AudioSeconds.ToString());
        builder.Append(", Trigger = ");
        builder.Append(this.Trigger);
        builder.Append(", StartedTicks = ");
        builder.Append(this.StartedTicks.ToString());
        builder.Append(", TranscriptMs = ");
        builder.Append(this.TranscriptMs.ToString());
        builder.Append(", WalkthroughStep = ");
        builder.Append(this.WalkthroughStep.ToString());
        builder.Append(", AttachedFilePaths = ");
        builder.Append(this.AttachedFilePaths);
        builder.Append(", IsDrive = ");
        builder.Append(this.IsDrive.ToString());
        builder.Append(", Screenless = ");
        builder.Append(this.Screenless.ToString());
        builder.Append(", PointsOnly = ");
        builder.Append(this.PointsOnly.ToString());
        builder.Append(", FlowBrief = ");
        builder.Append(this.FlowBrief);
        builder.Append(", Flow = ");
        builder.Append(this.Flow);
        return true;
      }

      // Token: 0x060025CF RID: 9679 RVA: 0x00400A68 File Offset: 0x00400A68
      [CompilerGenerated]
      public static bool operator !=(CompanionSession.TurnRequest left, CompanionSession.TurnRequest right)
      {
        return !(left == right);
      }

      // Token: 0x060025D0 RID: 9680 RVA: 0x00400A74 File Offset: 0x00400A74
      [CompilerGenerated]
      public static bool operator ==(CompanionSession.TurnRequest left, CompanionSession.TurnRequest right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060025D1 RID: 9681 RVA: 0x00400A88 File Offset: 0x00400A88
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Words>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Fact>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Typed>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<AnswersInText>k__BackingField)) * -1521134295 + EqualityComparer<CompanionSession.TurnCapture>.Default.GetHashCode(this.<Capture>k__BackingField)) * -1521134295 + EqualityComparer<ValueTuple<string, string>>.Default.GetHashCode(this.<FrontApp>k__BackingField)) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<AudioSeconds>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Trigger>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<StartedTicks>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<TranscriptMs>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<WalkthroughStep>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<AttachedFilePaths>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsDrive>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Screenless>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<PointsOnly>k__BackingField)) * -1521134295 + EqualityComparer<JsonObject>.Default.GetHashCode(this.<FlowBrief>k__BackingField);
      }

      // Token: 0x060025D2 RID: 9682 RVA: 0x00400C18 File Offset: 0x00400C18
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as CompanionSession.TurnRequest);
      }

      // Token: 0x060025D3 RID: 9683 RVA: 0x00400C28 File Offset: 0x00400C28
      [CompilerGenerated]
      public bool Equals(CompanionSession.TurnRequest other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Words>k__BackingField, other.<Words>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Fact>k__BackingField, other.<Fact>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Typed>k__BackingField, other.<Typed>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<AnswersInText>k__BackingField, other.<AnswersInText>k__BackingField) && EqualityComparer<CompanionSession.TurnCapture>.Default.Equals(this.<Capture>k__BackingField, other.<Capture>k__BackingField) && EqualityComparer<ValueTuple<string, string>>.Default.Equals(this.<FrontApp>k__BackingField, other.<FrontApp>k__BackingField) && EqualityComparer<double?>.Default.Equals(this.<AudioSeconds>k__BackingField, other.<AudioSeconds>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Trigger>k__BackingField, other.<Trigger>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<StartedTicks>k__BackingField, other.<StartedTicks>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<TranscriptMs>k__BackingField, other.<TranscriptMs>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<WalkthroughStep>k__BackingField, other.<WalkthroughStep>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<AttachedFilePaths>k__BackingField, other.<AttachedFilePaths>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsDrive>k__BackingField, other.<IsDrive>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Screenless>k__BackingField, other.<Screenless>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<PointsOnly>k__BackingField, other.<PointsOnly>k__BackingField) && EqualityComparer<JsonObject>.Default.Equals(this.<FlowBrief>k__BackingField, other.<FlowBrief>k__BackingField));
      }

      // Token: 0x060025D5 RID: 9685 RVA: 0x00400E00 File Offset: 0x00400E00
      [CompilerGenerated]
      private TurnRequest([Nullable(1)] CompanionSession.TurnRequest original)
      {
        this.Words = original.<Words>k__BackingField;
        this.Fact = original.<Fact>k__BackingField;
        this.Typed = original.<Typed>k__BackingField;
        this.AnswersInText = original.<AnswersInText>k__BackingField;
        this.Capture = original.<Capture>k__BackingField;
        this.FrontApp = original.<FrontApp>k__BackingField;
        this.AudioSeconds = original.<AudioSeconds>k__BackingField;
        this.Trigger = original.<Trigger>k__BackingField;
        this.StartedTicks = original.<StartedTicks>k__BackingField;
        this.TranscriptMs = original.<TranscriptMs>k__BackingField;
        this.WalkthroughStep = original.<WalkthroughStep>k__BackingField;
        this.AttachedFilePaths = original.<AttachedFilePaths>k__BackingField;
        this.IsDrive = original.<IsDrive>k__BackingField;
        this.Screenless = original.<Screenless>k__BackingField;
        this.PointsOnly = original.<PointsOnly>k__BackingField;
        this.FlowBrief = original.<FlowBrief>k__BackingField;
      }

      // Token: 0x060025D6 RID: 9686 RVA: 0x00400ED4 File Offset: 0x00400ED4
      [CompilerGenerated]
      public void Deconstruct(out string Words, out string Fact, out bool Typed, out bool AnswersInText, out CompanionSession.TurnCapture Capture, [TupleElementNames(new string[] { "DisplayName", "ProcessName" })] [Nullable(new byte[] { 0, 2, 2 })] out ValueTuple<string, string> FrontApp, out double? AudioSeconds, [Nullable(1)] out string Trigger, out long StartedTicks, out int TranscriptMs, out int? WalkthroughStep)
      {
        Words = this.Words;
        Fact = this.Fact;
        Typed = this.Typed;
        AnswersInText = this.AnswersInText;
        Capture = this.Capture;
        FrontApp = this.FrontApp;
        AudioSeconds = this.AudioSeconds;
        Trigger = this.Trigger;
        StartedTicks = this.StartedTicks;
        TranscriptMs = this.TranscriptMs;
        WalkthroughStep = this.WalkthroughStep;
      }
    }

    // Token: 0x020002B5 RID: 693
    [NullableContext(2)]
    [Nullable(0)]
    internal sealed class NarrationState
    {
      // Token: 0x060025D7 RID: 9687 RVA: 0x00400F50 File Offset: 0x00400F50
      public string LineText(int index)
      {
        object gate = this.Gate;
        string text;
        lock (gate)
        {
          if (this.Lines.Count > index)
          {
            text = this.Lines[index];
          }
          else
          {
            text = ((index == this.FirstLine) ? this.EarlyFirstLine : null);
          }
        }
        return text;
      }

      // Token: 0x040012A9 RID: 4777
      [Nullable(1)]
      public readonly object Gate = new object();

      // Token: 0x040012AA RID: 4778
      [Nullable(1)]
      public List<string> Lines = new List<string>();

      // Token: 0x040012AB RID: 4779
      [Nullable(1)]
      public List<DrawingAction> Actions = new List<DrawingAction>();

      // Token: 0x040012AC RID: 4780
      [Nullable(1)]
      public string ProgramSoFar = "";

      // Token: 0x040012AD RID: 4781
      public bool Finished;

      // Token: 0x040012AE RID: 4782
      public string Failure;

      // Token: 0x040012AF RID: 4783
      public string StatusLabel;

      // Token: 0x040012B0 RID: 4784
      public string RunningToolName;

      // Token: 0x040012B1 RID: 4785
      public bool IsSearchingTheWeb;

      // Token: 0x040012B2 RID: 4786
      public bool IsReadingConnectedApps;

      // Token: 0x040012B3 RID: 4787
      [Nullable(1)]
      public List<string> HeardLines = new List<string>();

      // Token: 0x040012B4 RID: 4788
      public int? FirstSayLineMs;

      // Token: 0x040012B5 RID: 4789
      public int FirstLine;

      // Token: 0x040012B6 RID: 4790
      public bool IsContinuationDecided = true;

      // Token: 0x040012B7 RID: 4791
      [Nullable(new byte[] { 2, 1 })]
      public Func<Task> StartOver;

      // Token: 0x040012B8 RID: 4792
      public int DisplayCount = 1;

      // Token: 0x040012B9 RID: 4793
      public bool IsDisplayDecided = true;

      // Token: 0x040012BA RID: 4794
      [Nullable(new byte[] { 2, 1 })]
      public Func<int, Task> MoveToDisplay;

      // Token: 0x040012BB RID: 4795
      public bool DrewOnOtherDisplay;

      // Token: 0x040012BC RID: 4796
      public string EarlyFirstLine;

      // Token: 0x040012BD RID: 4797
      public int Marks;

      // Token: 0x040012BE RID: 4798
      public bool WriteOnly;

      // Token: 0x040012BF RID: 4799
      public bool ReadConnectedApps;

      // Token: 0x040012C0 RID: 4800
      public CompanionToolContext ToolContext;

      // Token: 0x040012C1 RID: 4801
      public bool TargetArmed;

      // Token: 0x040012C2 RID: 4802
      public int TextDeliveries;

      // Token: 0x040012C3 RID: 4803
      public long? FirstLineHeardTicks;

      // Token: 0x040012C4 RID: 4804
      public string Model;

      // Token: 0x040012C5 RID: 4805
      public bool HeldForAdoption;
    }

    // Token: 0x020002B6 RID: 694
    [NullableContext(2)]
    [Nullable(0)]
    internal sealed class TurnBodyParts : IEquatable<CompanionSession.TurnBodyParts>
    {
      // Token: 0x060025D9 RID: 9689 RVA: 0x0040101C File Offset: 0x0040101C
      public TurnBodyParts([Nullable(1)] List<JsonNode> Thread, [Nullable(1)] List<JsonNode> TurnItems, [Nullable(1)] string SessionId, string TurnToken, string UserLayer, string UserLayerSignature, bool SendEnvironment, string Words, string Fact, [Nullable(1)] string TimeLabel, string FrontApp, string FrontAppProcess, string HighlightGesture, bool Typed, [Nullable(1)] string LiveState, [Nullable(1)] string ScreenshotBase64, [Nullable(1)] ScreenCaptureFrame Screenshot, [Nullable(1)] List<ScreenCaptureFrame> OtherScreens, string StableNote, double? AudioSeconds, [Nullable(1)] string Trigger, string DrawingCode, string DrawingOnScreen = null, int? WalkthroughStep = null, string Flow = null, string BrainModel = null, bool AppReads = false)
      {
        this.Thread = Thread;
        this.TurnItems = TurnItems;
        this.SessionId = SessionId;
        this.TurnToken = TurnToken;
        this.UserLayer = UserLayer;
        this.UserLayerSignature = UserLayerSignature;
        this.SendEnvironment = SendEnvironment;
        this.Words = Words;
        this.Fact = Fact;
        this.TimeLabel = TimeLabel;
        this.FrontApp = FrontApp;
        this.FrontAppProcess = FrontAppProcess;
        this.HighlightGesture = HighlightGesture;
        this.Typed = Typed;
        this.LiveState = LiveState;
        this.ScreenshotBase64 = ScreenshotBase64;
        this.Screenshot = Screenshot;
        this.OtherScreens = OtherScreens;
        this.StableNote = StableNote;
        this.AudioSeconds = AudioSeconds;
        this.Trigger = Trigger;
        this.DrawingCode = DrawingCode;
        this.DrawingOnScreen = DrawingOnScreen;
        this.WalkthroughStep = WalkthroughStep;
        this.Flow = Flow;
        this.BrainModel = BrainModel;
        this.AppReads = AppReads;
        this.PageAccessNoted = true;
        this.AttachedImages = Array.Empty<byte[]>();
        this.UserContext = BrainUserContext.Empty;
        this.Flags = BrainRequestFlags.None;
        base..ctor();
      }

      // Token: 0x1700078E RID: 1934
      // (get) Token: 0x060025DA RID: 9690 RVA: 0x0040112C File Offset: 0x0040112C
      [Nullable(1)]
      [CompilerGenerated]
      private Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(CompanionSession.TurnBodyParts);
        }
      }

      // Token: 0x1700078F RID: 1935
      // (get) Token: 0x060025DB RID: 9691 RVA: 0x00401138 File Offset: 0x00401138
      // (set) Token: 0x060025DC RID: 9692 RVA: 0x00401140 File Offset: 0x00401140
      [Nullable(1)]
      public List<JsonNode> Thread
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x17000790 RID: 1936
      // (get) Token: 0x060025DD RID: 9693 RVA: 0x0040114C File Offset: 0x0040114C
      // (set) Token: 0x060025DE RID: 9694 RVA: 0x00401154 File Offset: 0x00401154
      [Nullable(1)]
      public List<JsonNode> TurnItems
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x17000791 RID: 1937
      // (get) Token: 0x060025DF RID: 9695 RVA: 0x00401160 File Offset: 0x00401160
      // (set) Token: 0x060025E0 RID: 9696 RVA: 0x00401168 File Offset: 0x00401168
      [Nullable(1)]
      public string SessionId
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x17000792 RID: 1938
      // (get) Token: 0x060025E1 RID: 9697 RVA: 0x00401174 File Offset: 0x00401174
      // (set) Token: 0x060025E2 RID: 9698 RVA: 0x0040117C File Offset: 0x0040117C
      public string TurnToken { get; set; }

      // Token: 0x17000793 RID: 1939
      // (get) Token: 0x060025E3 RID: 9699 RVA: 0x00401188 File Offset: 0x00401188
      // (set) Token: 0x060025E4 RID: 9700 RVA: 0x00401190 File Offset: 0x00401190
      public string UserLayer { get; set; }

      // Token: 0x17000794 RID: 1940
      // (get) Token: 0x060025E5 RID: 9701 RVA: 0x0040119C File Offset: 0x0040119C
      // (set) Token: 0x060025E6 RID: 9702 RVA: 0x004011A4 File Offset: 0x004011A4
      public string UserLayerSignature { get; set; }

      // Token: 0x17000795 RID: 1941
      // (get) Token: 0x060025E7 RID: 9703 RVA: 0x004011B0 File Offset: 0x004011B0
      // (set) Token: 0x060025E8 RID: 9704 RVA: 0x004011B8 File Offset: 0x004011B8
      public bool SendEnvironment { get; set; }

      // Token: 0x17000796 RID: 1942
      // (get) Token: 0x060025E9 RID: 9705 RVA: 0x004011C4 File Offset: 0x004011C4
      // (set) Token: 0x060025EA RID: 9706 RVA: 0x004011CC File Offset: 0x004011CC
      public string Words { get; set; }

      // Token: 0x17000797 RID: 1943
      // (get) Token: 0x060025EB RID: 9707 RVA: 0x004011D8 File Offset: 0x004011D8
      // (set) Token: 0x060025EC RID: 9708 RVA: 0x004011E0 File Offset: 0x004011E0
      public string Fact { get; set; }

      // Token: 0x17000798 RID: 1944
      // (get) Token: 0x060025ED RID: 9709 RVA: 0x004011EC File Offset: 0x004011EC
      // (set) Token: 0x060025EE RID: 9710 RVA: 0x004011F4 File Offset: 0x004011F4
      [Nullable(1)]
      public string TimeLabel
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x17000799 RID: 1945
      // (get) Token: 0x060025EF RID: 9711 RVA: 0x00401200 File Offset: 0x00401200
      // (set) Token: 0x060025F0 RID: 9712 RVA: 0x00401208 File Offset: 0x00401208
      public string FrontApp { get; set; }

      // Token: 0x1700079A RID: 1946
      // (get) Token: 0x060025F1 RID: 9713 RVA: 0x00401214 File Offset: 0x00401214
      // (set) Token: 0x060025F2 RID: 9714 RVA: 0x0040121C File Offset: 0x0040121C
      public string FrontAppProcess { get; set; }

      // Token: 0x1700079B RID: 1947
      // (get) Token: 0x060025F3 RID: 9715 RVA: 0x00401228 File Offset: 0x00401228
      // (set) Token: 0x060025F4 RID: 9716 RVA: 0x00401230 File Offset: 0x00401230
      public string HighlightGesture { get; set; }

      // Token: 0x1700079C RID: 1948
      // (get) Token: 0x060025F5 RID: 9717 RVA: 0x0040123C File Offset: 0x0040123C
      // (set) Token: 0x060025F6 RID: 9718 RVA: 0x00401244 File Offset: 0x00401244
      public bool Typed { get; set; }

      // Token: 0x1700079D RID: 1949
      // (get) Token: 0x060025F7 RID: 9719 RVA: 0x00401250 File Offset: 0x00401250
      // (set) Token: 0x060025F8 RID: 9720 RVA: 0x00401258 File Offset: 0x00401258
      [Nullable(1)]
      public string LiveState
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x1700079E RID: 1950
      // (get) Token: 0x060025F9 RID: 9721 RVA: 0x00401264 File Offset: 0x00401264
      // (set) Token: 0x060025FA RID: 9722 RVA: 0x0040126C File Offset: 0x0040126C
      [Nullable(1)]
      public string ScreenshotBase64
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x1700079F RID: 1951
      // (get) Token: 0x060025FB RID: 9723 RVA: 0x00401278 File Offset: 0x00401278
      // (set) Token: 0x060025FC RID: 9724 RVA: 0x00401280 File Offset: 0x00401280
      [Nullable(1)]
      public ScreenCaptureFrame Screenshot
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x170007A0 RID: 1952
      // (get) Token: 0x060025FD RID: 9725 RVA: 0x0040128C File Offset: 0x0040128C
      // (set) Token: 0x060025FE RID: 9726 RVA: 0x00401294 File Offset: 0x00401294
      [Nullable(1)]
      public List<ScreenCaptureFrame> OtherScreens
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x170007A1 RID: 1953
      // (get) Token: 0x060025FF RID: 9727 RVA: 0x004012A0 File Offset: 0x004012A0
      // (set) Token: 0x06002600 RID: 9728 RVA: 0x004012A8 File Offset: 0x004012A8
      public string StableNote { get; set; }

      // Token: 0x170007A2 RID: 1954
      // (get) Token: 0x06002601 RID: 9729 RVA: 0x004012B4 File Offset: 0x004012B4
      // (set) Token: 0x06002602 RID: 9730 RVA: 0x004012BC File Offset: 0x004012BC
      public double? AudioSeconds { get; set; }

      // Token: 0x170007A3 RID: 1955
      // (get) Token: 0x06002603 RID: 9731 RVA: 0x004012C8 File Offset: 0x004012C8
      // (set) Token: 0x06002604 RID: 9732 RVA: 0x004012D0 File Offset: 0x004012D0
      [Nullable(1)]
      public string Trigger
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x170007A4 RID: 1956
      // (get) Token: 0x06002605 RID: 9733 RVA: 0x004012DC File Offset: 0x004012DC
      // (set) Token: 0x06002606 RID: 9734 RVA: 0x004012E4 File Offset: 0x004012E4
      public string DrawingCode { get; set; }

      // Token: 0x170007A5 RID: 1957
      // (get) Token: 0x06002607 RID: 9735 RVA: 0x004012F0 File Offset: 0x004012F0
      // (set) Token: 0x06002608 RID: 9736 RVA: 0x004012F8 File Offset: 0x004012F8
      public string DrawingOnScreen { get; set; }

      // Token: 0x170007A6 RID: 1958
      // (get) Token: 0x06002609 RID: 9737 RVA: 0x00401304 File Offset: 0x00401304
      // (set) Token: 0x0600260A RID: 9738 RVA: 0x0040130C File Offset: 0x0040130C
      public int? WalkthroughStep { get; set; }

      // Token: 0x170007A7 RID: 1959
      // (get) Token: 0x0600260B RID: 9739 RVA: 0x00401318 File Offset: 0x00401318
      // (set) Token: 0x0600260C RID: 9740 RVA: 0x00401320 File Offset: 0x00401320
      public string Flow { get; set; }

      // Token: 0x170007A8 RID: 1960
      // (get) Token: 0x0600260D RID: 9741 RVA: 0x0040132C File Offset: 0x0040132C
      // (set) Token: 0x0600260E RID: 9742 RVA: 0x00401334 File Offset: 0x00401334
      public string BrainModel { get; set; }

      // Token: 0x170007A9 RID: 1961
      // (get) Token: 0x0600260F RID: 9743 RVA: 0x00401340 File Offset: 0x00401340
      // (set) Token: 0x06002610 RID: 9744 RVA: 0x00401348 File Offset: 0x00401348
      public bool AppReads { get; set; }

      // Token: 0x170007AA RID: 1962
      // (get) Token: 0x06002611 RID: 9745 RVA: 0x00401354 File Offset: 0x00401354
      // (set) Token: 0x06002612 RID: 9746 RVA: 0x0040135C File Offset: 0x0040135C
      public bool OffersDrive { get; set; }

      // Token: 0x170007AB RID: 1963
      // (get) Token: 0x06002613 RID: 9747 RVA: 0x00401368 File Offset: 0x00401368
      // (set) Token: 0x06002614 RID: 9748 RVA: 0x00401370 File Offset: 0x00401370
      public bool IsDrive { get; set; }

      // Token: 0x170007AC RID: 1964
      // (get) Token: 0x06002615 RID: 9749 RVA: 0x0040137C File Offset: 0x0040137C
      // (set) Token: 0x06002616 RID: 9750 RVA: 0x00401384 File Offset: 0x00401384
      public JsonObject DriveBillingFields { get; set; }

      // Token: 0x170007AD RID: 1965
      // (get) Token: 0x06002617 RID: 9751 RVA: 0x00401390 File Offset: 0x00401390
      // (set) Token: 0x06002618 RID: 9752 RVA: 0x00401398 File Offset: 0x00401398
      public PageContext Page { get; set; }

      // Token: 0x170007AE RID: 1966
      // (get) Token: 0x06002619 RID: 9753 RVA: 0x004013A4 File Offset: 0x004013A4
      // (set) Token: 0x0600261A RID: 9754 RVA: 0x004013AC File Offset: 0x004013AC
      public bool PageAccessNoted { get; set; }

      // Token: 0x170007AF RID: 1967
      // (get) Token: 0x0600261B RID: 9755 RVA: 0x004013B8 File Offset: 0x004013B8
      // (set) Token: 0x0600261C RID: 9756 RVA: 0x004013C0 File Offset: 0x004013C0
      [Nullable(1)]
      public IReadOnlyList<byte[]> AttachedImages
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x170007B0 RID: 1968
      // (get) Token: 0x0600261D RID: 9757 RVA: 0x004013CC File Offset: 0x004013CC
      // (set) Token: 0x0600261E RID: 9758 RVA: 0x004013D4 File Offset: 0x004013D4
      [Nullable(1)]
      public BrainUserContext UserContext
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x170007B1 RID: 1969
      // (get) Token: 0x0600261F RID: 9759 RVA: 0x004013E0 File Offset: 0x004013E0
      // (set) Token: 0x06002620 RID: 9760 RVA: 0x004013E8 File Offset: 0x004013E8
      public string HighlightText { get; set; }

      // Token: 0x170007B2 RID: 1970
      // (get) Token: 0x06002621 RID: 9761 RVA: 0x004013F4 File Offset: 0x004013F4
      // (set) Token: 0x06002622 RID: 9762 RVA: 0x004013FC File Offset: 0x004013FC
      public JsonObject FlowBrief { get; set; }

      // Token: 0x170007B3 RID: 1971
      // (get) Token: 0x06002623 RID: 9763 RVA: 0x00401408 File Offset: 0x00401408
      // (set) Token: 0x06002624 RID: 9764 RVA: 0x00401410 File Offset: 0x00401410
      public bool Screenless { get; set; }

      // Token: 0x170007B4 RID: 1972
      // (get) Token: 0x06002625 RID: 9765 RVA: 0x0040141C File Offset: 0x0040141C
      // (set) Token: 0x06002626 RID: 9766 RVA: 0x00401424 File Offset: 0x00401424
      [Nullable(1)]
      public BrainRequestFlags Flags
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x06002627 RID: 9767 RVA: 0x00401430 File Offset: 0x00401430
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("TurnBodyParts");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002628 RID: 9768 RVA: 0x0040147C File Offset: 0x0040147C
      [NullableContext(1)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Thread = ");
        builder.Append(this.Thread);
        builder.Append(", TurnItems = ");
        builder.Append(this.TurnItems);
        builder.Append(", SessionId = ");
        builder.Append(this.SessionId);
        builder.Append(", TurnToken = ");
        builder.Append(this.TurnToken);
        builder.Append(", UserLayer = ");
        builder.Append(this.UserLayer);
        builder.Append(", UserLayerSignature = ");
        builder.Append(this.UserLayerSignature);
        builder.Append(", SendEnvironment = ");
        builder.Append(this.SendEnvironment.ToString());
        builder.Append(", Words = ");
        builder.Append(this.Words);
        builder.Append(", Fact = ");
        builder.Append(this.Fact);
        builder.Append(", TimeLabel = ");
        builder.Append(this.TimeLabel);
        builder.Append(", FrontApp = ");
        builder.Append(this.FrontApp);
        builder.Append(", FrontAppProcess = ");
        builder.Append(this.FrontAppProcess);
        builder.Append(", HighlightGesture = ");
        builder.Append(this.HighlightGesture);
        builder.Append(", Typed = ");
        builder.Append(this.Typed.ToString());
        builder.Append(", LiveState = ");
        builder.Append(this.LiveState);
        builder.Append(", ScreenshotBase64 = ");
        builder.Append(this.ScreenshotBase64);
        builder.Append(", Screenshot = ");
        builder.Append(this.Screenshot);
        builder.Append(", OtherScreens = ");
        builder.Append(this.OtherScreens);
        builder.Append(", StableNote = ");
        builder.Append(this.StableNote);
        builder.Append(", AudioSeconds = ");
        builder.Append(this.AudioSeconds.ToString());
        builder.Append(", Trigger = ");
        builder.Append(this.Trigger);
        builder.Append(", DrawingCode = ");
        builder.Append(this.DrawingCode);
        builder.Append(", DrawingOnScreen = ");
        builder.Append(this.DrawingOnScreen);
        builder.Append(", WalkthroughStep = ");
        builder.Append(this.WalkthroughStep.ToString());
        builder.Append(", Flow = ");
        builder.Append(this.Flow);
        builder.Append(", BrainModel = ");
        builder.Append(this.BrainModel);
        builder.Append(", AppReads = ");
        builder.Append(this.AppReads.ToString());
        builder.Append(", OffersDrive = ");
        builder.Append(this.OffersDrive.ToString());
        builder.Append(", IsDrive = ");
        builder.Append(this.IsDrive.ToString());
        builder.Append(", DriveBillingFields = ");
        builder.Append(this.DriveBillingFields);
        builder.Append(", Page = ");
        builder.Append(this.Page);
        builder.Append(", PageAccessNoted = ");
        builder.Append(this.PageAccessNoted.ToString());
        builder.Append(", AttachedImages = ");
        builder.Append(this.AttachedImages);
        builder.Append(", UserContext = ");
        builder.Append(this.UserContext);
        builder.Append(", HighlightText = ");
        builder.Append(this.HighlightText);
        builder.Append(", FlowBrief = ");
        builder.Append(this.FlowBrief);
        builder.Append(", Screenless = ");
        builder.Append(this.Screenless.ToString());
        builder.Append(", Flags = ");
        builder.Append(this.Flags);
        return true;
      }

      // Token: 0x06002629 RID: 9769 RVA: 0x004018C4 File Offset: 0x004018C4
      [CompilerGenerated]
      public static bool operator !=(CompanionSession.TurnBodyParts left, CompanionSession.TurnBodyParts right)
      {
        return !(left == right);
      }

      // Token: 0x0600262A RID: 9770 RVA: 0x004018D0 File Offset: 0x004018D0
      [CompilerGenerated]
      public static bool operator ==(CompanionSession.TurnBodyParts left, CompanionSession.TurnBodyParts right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600262B RID: 9771 RVA: 0x004018E4 File Offset: 0x004018E4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((((((((((((((((((((((((((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<List<JsonNode>>.Default.GetHashCode(this.<Thread>k__BackingField)) * -1521134295 + EqualityComparer<List<JsonNode>>.Default.GetHashCode(this.<TurnItems>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SessionId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TurnToken>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UserLayer>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UserLayerSignature>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<SendEnvironment>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Words>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Fact>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TimeLabel>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FrontApp>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FrontAppProcess>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<HighlightGesture>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Typed>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LiveState>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ScreenshotBase64>k__BackingField)) * -1521134295 + EqualityComparer<ScreenCaptureFrame>.Default.GetHashCode(this.<Screenshot>k__BackingField)) * -1521134295 + EqualityComparer<List<ScreenCaptureFrame>>.Default.GetHashCode(this.<OtherScreens>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<StableNote>k__BackingField)) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<AudioSeconds>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Trigger>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DrawingCode>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DrawingOnScreen>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<WalkthroughStep>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Flow>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BrainModel>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<AppReads>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<OffersDrive>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsDrive>k__BackingField)) * -1521134295 + EqualityComparer<JsonObject>.Default.GetHashCode(this.<DriveBillingFields>k__BackingField)) * -1521134295 + EqualityComparer<PageContext>.Default.GetHashCode(this.<Page>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<PageAccessNoted>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<byte[]>>.Default.GetHashCode(this.<AttachedImages>k__BackingField)) * -1521134295 + EqualityComparer<BrainUserContext>.Default.GetHashCode(this.<UserContext>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<HighlightText>k__BackingField)) * -1521134295 + EqualityComparer<JsonObject>.Default.GetHashCode(this.<FlowBrief>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Screenless>k__BackingField)) * -1521134295 + EqualityComparer<BrainRequestFlags>.Default.GetHashCode(this.<Flags>k__BackingField);
      }

      // Token: 0x0600262C RID: 9772 RVA: 0x00401C6C File Offset: 0x00401C6C
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as CompanionSession.TurnBodyParts);
      }

      // Token: 0x0600262D RID: 9773 RVA: 0x00401C7C File Offset: 0x00401C7C
      [CompilerGenerated]
      public bool Equals(CompanionSession.TurnBodyParts other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<List<JsonNode>>.Default.Equals(this.<Thread>k__BackingField, other.<Thread>k__BackingField) && EqualityComparer<List<JsonNode>>.Default.Equals(this.<TurnItems>k__BackingField, other.<TurnItems>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SessionId>k__BackingField, other.<SessionId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TurnToken>k__BackingField, other.<TurnToken>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UserLayer>k__BackingField, other.<UserLayer>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UserLayerSignature>k__BackingField, other.<UserLayerSignature>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<SendEnvironment>k__BackingField, other.<SendEnvironment>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Words>k__BackingField, other.<Words>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Fact>k__BackingField, other.<Fact>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TimeLabel>k__BackingField, other.<TimeLabel>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FrontApp>k__BackingField, other.<FrontApp>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FrontAppProcess>k__BackingField, other.<FrontAppProcess>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<HighlightGesture>k__BackingField, other.<HighlightGesture>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Typed>k__BackingField, other.<Typed>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LiveState>k__BackingField, other.<LiveState>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ScreenshotBase64>k__BackingField, other.<ScreenshotBase64>k__BackingField) && EqualityComparer<ScreenCaptureFrame>.Default.Equals(this.<Screenshot>k__BackingField, other.<Screenshot>k__BackingField) && EqualityComparer<List<ScreenCaptureFrame>>.Default.Equals(this.<OtherScreens>k__BackingField, other.<OtherScreens>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<StableNote>k__BackingField, other.<StableNote>k__BackingField) && EqualityComparer<double?>.Default.Equals(this.<AudioSeconds>k__BackingField, other.<AudioSeconds>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Trigger>k__BackingField, other.<Trigger>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DrawingCode>k__BackingField, other.<DrawingCode>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DrawingOnScreen>k__BackingField, other.<DrawingOnScreen>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<WalkthroughStep>k__BackingField, other.<WalkthroughStep>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Flow>k__BackingField, other.<Flow>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BrainModel>k__BackingField, other.<BrainModel>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<AppReads>k__BackingField, other.<AppReads>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<OffersDrive>k__BackingField, other.<OffersDrive>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsDrive>k__BackingField, other.<IsDrive>k__BackingField) && EqualityComparer<JsonObject>.Default.Equals(this.<DriveBillingFields>k__BackingField, other.<DriveBillingFields>k__BackingField) && EqualityComparer<PageContext>.Default.Equals(this.<Page>k__BackingField, other.<Page>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<PageAccessNoted>k__BackingField, other.<PageAccessNoted>k__BackingField) && EqualityComparer<IReadOnlyList<byte[]>>.Default.Equals(this.<AttachedImages>k__BackingField, other.<AttachedImages>k__BackingField) && EqualityComparer<BrainUserContext>.Default.Equals(this.<UserContext>k__BackingField, other.<UserContext>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<HighlightText>k__BackingField, other.<HighlightText>k__BackingField) && EqualityComparer<JsonObject>.Default.Equals(this.<FlowBrief>k__BackingField, other.<FlowBrief>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Screenless>k__BackingField, other.<Screenless>k__BackingField) && EqualityComparer<BrainRequestFlags>.Default.Equals(this.<Flags>k__BackingField, other.<Flags>k__BackingField));
      }

      // Token: 0x0600262F RID: 9775 RVA: 0x004020A8 File Offset: 0x004020A8
      [CompilerGenerated]
      private TurnBodyParts([Nullable(1)] CompanionSession.TurnBodyParts original)
      {
        this.Thread = original.<Thread>k__BackingField;
        this.TurnItems = original.<TurnItems>k__BackingField;
        this.SessionId = original.<SessionId>k__BackingField;
        this.TurnToken = original.<TurnToken>k__BackingField;
        this.UserLayer = original.<UserLayer>k__BackingField;
        this.UserLayerSignature = original.<UserLayerSignature>k__BackingField;
        this.SendEnvironment = original.<SendEnvironment>k__BackingField;
        this.Words = original.<Words>k__BackingField;
        this.Fact = original.<Fact>k__BackingField;
        this.TimeLabel = original.<TimeLabel>k__BackingField;
        this.FrontApp = original.<FrontApp>k__BackingField;
        this.FrontAppProcess = original.<FrontAppProcess>k__BackingField;
        this.HighlightGesture = original.<HighlightGesture>k__BackingField;
        this.Typed = original.<Typed>k__BackingField;
        this.LiveState = original.<LiveState>k__BackingField;
        this.ScreenshotBase64 = original.<ScreenshotBase64>k__BackingField;
        this.Screenshot = original.<Screenshot>k__BackingField;
        this.OtherScreens = original.<OtherScreens>k__BackingField;
        this.StableNote = original.<StableNote>k__BackingField;
        this.AudioSeconds = original.<AudioSeconds>k__BackingField;
        this.Trigger = original.<Trigger>k__BackingField;
        this.DrawingCode = original.<DrawingCode>k__BackingField;
        this.DrawingOnScreen = original.<DrawingOnScreen>k__BackingField;
        this.WalkthroughStep = original.<WalkthroughStep>k__BackingField;
        this.Flow = original.<Flow>k__BackingField;
        this.BrainModel = original.<BrainModel>k__BackingField;
        this.AppReads = original.<AppReads>k__BackingField;
        this.OffersDrive = original.<OffersDrive>k__BackingField;
        this.IsDrive = original.<IsDrive>k__BackingField;
        this.DriveBillingFields = original.<DriveBillingFields>k__BackingField;
        this.Page = original.<Page>k__BackingField;
        this.PageAccessNoted = original.<PageAccessNoted>k__BackingField;
        this.AttachedImages = original.<AttachedImages>k__BackingField;
        this.UserContext = original.<UserContext>k__BackingField;
        this.HighlightText = original.<HighlightText>k__BackingField;
        this.FlowBrief = original.<FlowBrief>k__BackingField;
        this.Screenless = original.<Screenless>k__BackingField;
        this.Flags = original.<Flags>k__BackingField;
      }

      // Token: 0x06002630 RID: 9776 RVA: 0x00402284 File Offset: 0x00402284
      [CompilerGenerated]
      public void Deconstruct([Nullable(1)] out List<JsonNode> Thread, [Nullable(1)] out List<JsonNode> TurnItems, [Nullable(1)] out string SessionId, out string TurnToken, out string UserLayer, out string UserLayerSignature, out bool SendEnvironment, out string Words, out string Fact, [Nullable(1)] out string TimeLabel, out string FrontApp, out string FrontAppProcess, out string HighlightGesture, out bool Typed, [Nullable(1)] out string LiveState, [Nullable(1)] out string ScreenshotBase64, [Nullable(1)] out ScreenCaptureFrame Screenshot, [Nullable(1)] out List<ScreenCaptureFrame> OtherScreens, out string StableNote, out double? AudioSeconds, [Nullable(1)] out string Trigger, out string DrawingCode, out string DrawingOnScreen, out int? WalkthroughStep, out string Flow, out string BrainModel, out bool AppReads)
      {
        Thread = this.Thread;
        TurnItems = this.TurnItems;
        SessionId = this.SessionId;
        TurnToken = this.TurnToken;
        UserLayer = this.UserLayer;
        UserLayerSignature = this.UserLayerSignature;
        SendEnvironment = this.SendEnvironment;
        Words = this.Words;
        Fact = this.Fact;
        TimeLabel = this.TimeLabel;
        FrontApp = this.FrontApp;
        FrontAppProcess = this.FrontAppProcess;
        HighlightGesture = this.HighlightGesture;
        Typed = this.Typed;
        LiveState = this.LiveState;
        ScreenshotBase64 = this.ScreenshotBase64;
        Screenshot = this.Screenshot;
        OtherScreens = this.OtherScreens;
        StableNote = this.StableNote;
        AudioSeconds = this.AudioSeconds;
        Trigger = this.Trigger;
        DrawingCode = this.DrawingCode;
        DrawingOnScreen = this.DrawingOnScreen;
        WalkthroughStep = this.WalkthroughStep;
        Flow = this.Flow;
        BrainModel = this.BrainModel;
        AppReads = this.AppReads;
      }
    }

    // Token: 0x020002B7 RID: 695
    [Nullable(0)]
    internal sealed class CanvasFeeder : CompanionSession.IProgramFeeder
    {
      // Token: 0x06002631 RID: 9777 RVA: 0x0040238C File Offset: 0x0040238C
      public CanvasFeeder(CompanionSession owner, CompanionSession.NarrationState narration, Func<bool> stillCurrent)
      {
      }

      // Token: 0x06002632 RID: 9778 RVA: 0x004023C0 File Offset: 0x004023C0
      public void Push(string code, bool done)
      {
        object obj = this.feedGate;
        lock (obj)
        {
          this.pendingCode = code;
          this.pendingDone = this.pendingDone || done;
          if (this.running.IsCompleted)
          {
            this.running = Task.Run(new Func<Task>(this.LoopAsync));
          }
        }
      }

      // Token: 0x06002633 RID: 9779 RVA: 0x00402438 File Offset: 0x00402438
      public async Task DrainAsync()
      {
        object obj = this.feedGate;
        Task task;
        lock (obj)
        {
          task = this.running;
        }
        await task.ConfigureAwait(false);
        obj = this.feedGate;
        lock (obj)
        {
          task = this.running;
        }
        await task.ConfigureAwait(false);
      }

      // Token: 0x06002634 RID: 9780 RVA: 0x0040247C File Offset: 0x0040247C
      private async Task LoopAsync()
      {
        for (;;)
        {
          CompanionSession.CanvasFeeder.<>c__DisplayClass10_0 CS$<>8__locals1 = new CompanionSession.CanvasFeeder.<>c__DisplayClass10_0();
          CS$<>8__locals1.<>4__this = this;
          object obj = this.feedGate;
          lock (obj)
          {
            if (this.pendingCode == null)
            {
              break;
            }
            CS$<>8__locals1.code = this.pendingCode;
            CS$<>8__locals1.done = this.pendingDone;
            this.pendingCode = null;
          }
          if (this.<stillCurrent>P())
          {
            if (!this.<narration>P.IsDisplayDecided)
            {
              int num = DrawingDisplayChoices.Decide(CS$<>8__locals1.code, CS$<>8__locals1.done, this.<narration>P.DisplayCount);
              if (num < 0)
              {
                continue;
              }
              this.<narration>P.IsDisplayDecided = true;
              if (num > 0)
              {
                Func<int, Task> moveToDisplay = this.<narration>P.MoveToDisplay;
                if (moveToDisplay != null)
                {
                  try
                  {
                    await moveToDisplay(num).ConfigureAwait(false);
                  }
                  catch (OperationCanceledException)
                  {
                    break;
                  }
                }
              }
            }
            if (!this.<narration>P.IsContinuationDecided)
            {
              DrawingContinuationDecision drawingContinuationDecision = DrawingContinuationDecisions.Decide(CS$<>8__locals1.code, CS$<>8__locals1.done);
              if (drawingContinuationDecision == DrawingContinuationDecision.Undecided)
              {
                continue;
              }
              this.<narration>P.IsContinuationDecided = true;
              if (drawingContinuationDecision == DrawingContinuationDecision.StartOver)
              {
                Func<Task> startOver = this.<narration>P.StartOver;
                if (startOver != null)
                {
                  try
                  {
                    await startOver().ConfigureAwait(false);
                  }
                  catch (OperationCanceledException)
                  {
                    break;
                  }
                }
              }
            }
            obj = this.<narration>P.Gate;
            lock (obj)
            {
              this.<narration>P.ProgramSoFar = CS$<>8__locals1.code;
            }
            if (!this.<narration>P.WriteOnly)
            {
              int? num2 = this.<narration>P.FirstSayLineMs;
              if (num2 == null)
              {
                string text = ProgramText.FirstSayLiteral(CS$<>8__locals1.code);
                if (text != null)
                {
                  obj = this.<narration>P.Gate;
                  lock (obj)
                  {
                    if (this.<narration>P.Lines.Count <= this.<narration>P.FirstLine)
                    {
                      this.<narration>P.EarlyFirstLine = text;
                      this.<narration>P.FirstSayLineMs = new int?((int)(Environment.TickCount64 - this.<owner>P.turnStartedTicks));
                    }
                  }
                  this.<owner>P.SignalNarration();
                }
              }
              try
              {
                DrawingFeedResult drawingFeedResult = await this.<owner>P.ui.InvokeAsync<DrawingFeedResult>(() => CS$<>8__locals1.<>4__this.<owner>P.canvas.Feed(CS$<>8__locals1.code, CS$<>8__locals1.done)).ConfigureAwait(false);
                obj = this.<narration>P.Gate;
                lock (obj)
                {
                  if (drawingFeedResult.Lines.Count >= this.<narration>P.Lines.Count)
                  {
                    this.<narration>P.Lines = drawingFeedResult.Lines.ToList<string>();
                  }
                  if (drawingFeedResult.Actions.Count >= this.<narration>P.Actions.Count)
                  {
                    this.<narration>P.Actions = drawingFeedResult.Actions.ToList<DrawingAction>();
                  }
                  this.<narration>P.Marks = Math.Max(this.<narration>P.Marks, drawingFeedResult.MarkCount);
                  num2 = this.<narration>P.FirstSayLineMs;
                  if (num2 == null && this.<narration>P.Lines.Count > this.<narration>P.FirstLine)
                  {
                    this.<narration>P.FirstSayLineMs = new int?((int)(Environment.TickCount64 - this.<owner>P.turnStartedTicks));
                  }
                }
              }
              catch (Exception ex)
              {
                Log.Error("Companion", "canvas feed failed", ex);
              }
              this.<owner>P.SignalNarration();
              CS$<>8__locals1 = null;
            }
          }
        }
      }

      // Token: 0x040012EC RID: 4844
      [CompilerGenerated]
      private CompanionSession <owner>P = owner;

      // Token: 0x040012ED RID: 4845
      [CompilerGenerated]
      private CompanionSession.NarrationState <narration>P = narration;

      // Token: 0x040012EE RID: 4846
      [CompilerGenerated]
      private Func<bool> <stillCurrent>P = stillCurrent;

      // Token: 0x040012EF RID: 4847
      private readonly object feedGate = new object();

      // Token: 0x040012F0 RID: 4848
      [Nullable(2)]
      private string pendingCode;

      // Token: 0x040012F1 RID: 4849
      private bool pendingDone;

      // Token: 0x040012F2 RID: 4850
      private Task running = Task.CompletedTask;
    }

    // Token: 0x020002B8 RID: 696
    [Nullable(0)]
    private sealed class TurnTimer
    {
      // Token: 0x06002635 RID: 9781 RVA: 0x004024C0 File Offset: 0x004024C0
      public TurnTimer(long startedTicks)
      {
      }

      // Token: 0x170007B5 RID: 1973
      // (get) Token: 0x06002636 RID: 9782 RVA: 0x004024D0 File Offset: 0x004024D0
      private int Elapsed
      {
        get
        {
          return (int)(Environment.TickCount64 - this.<startedTicks>P);
        }
      }

      // Token: 0x06002637 RID: 9783 RVA: 0x004024E0 File Offset: 0x004024E0
      public void MarkBrainFirstByte()
      {
        int num = this.BrainFirstByteMs.GetValueOrDefault();
        if (this.BrainFirstByteMs == null)
        {
          num = this.Elapsed;
          this.BrainFirstByteMs = new int?(num);
        }
      }

      // Token: 0x06002638 RID: 9784 RVA: 0x0040251C File Offset: 0x0040251C
      public void MarkFirstSpoken()
      {
        int num = this.FirstSpokenMs.GetValueOrDefault();
        if (this.FirstSpokenMs == null)
        {
          num = this.Elapsed;
          this.FirstSpokenMs = new int?(num);
        }
      }

      // Token: 0x06002639 RID: 9785 RVA: 0x00402558 File Offset: 0x00402558
      public void MarkRequestSent()
      {
        int num = this.RequestSentMs.GetValueOrDefault();
        if (this.RequestSentMs == null)
        {
          num = this.Elapsed;
          this.RequestSentMs = new int?(num);
        }
      }

      // Token: 0x0600263A RID: 9786 RVA: 0x00402594 File Offset: 0x00402594
      public void MarkFirstText()
      {
        int num = this.FirstTextMs.GetValueOrDefault();
        if (this.FirstTextMs == null)
        {
          num = this.Elapsed;
          this.FirstTextMs = new int?(num);
        }
      }

      // Token: 0x0600263B RID: 9787 RVA: 0x004025D0 File Offset: 0x004025D0
      public void MarkFirstCode()
      {
        int num = this.FirstCodeMs.GetValueOrDefault();
        if (this.FirstCodeMs == null)
        {
          num = this.Elapsed;
          this.FirstCodeMs = new int?(num);
        }
      }

      // Token: 0x0600263C RID: 9788 RVA: 0x0040260C File Offset: 0x0040260C
      public void MarkFirstLineToMouth()
      {
        int num = this.FirstLineToMouthMs.GetValueOrDefault();
        if (this.FirstLineToMouthMs == null)
        {
          num = this.Elapsed;
          this.FirstLineToMouthMs = new int?(num);
        }
      }

      // Token: 0x0600263D RID: 9789 RVA: 0x00402648 File Offset: 0x00402648
      public CompanionTurnMetrics ToMetrics(bool typed, int transcriptMs, int capture, int? firstAudio)
      {
        return new CompanionTurnMetrics(typed, transcriptMs, capture, this.BrainFirstByteMs, this.FirstSayLineMs, firstAudio, this.FirstSpokenMs, (int)(Environment.TickCount64 - this.<startedTicks>P), this.Model);
      }

      // Token: 0x0600263E RID: 9790 RVA: 0x00402684 File Offset: 0x00402684
      public string Describe(int? firstAudio)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(157, 10);
        defaultInterpolatedStringHandler.AppendLiteral("request sent ");
        defaultInterpolatedStringHandler.AppendFormatted(((this.RequestSentMs != null) ? this.RequestSentMs.GetValueOrDefault().ToString() : null) ?? "-");
        defaultInterpolatedStringHandler.AppendLiteral(" ms, brain first byte ");
        defaultInterpolatedStringHandler.AppendFormatted(((this.BrainFirstByteMs != null) ? this.BrainFirstByteMs.GetValueOrDefault().ToString() : null) ?? "-");
        defaultInterpolatedStringHandler.AppendLiteral(" ms, first text ");
        defaultInterpolatedStringHandler.AppendFormatted(((this.FirstTextMs != null) ? this.FirstTextMs.GetValueOrDefault().ToString() : null) ?? "-");
        defaultInterpolatedStringHandler.AppendLiteral(" ms, first code ");
        defaultInterpolatedStringHandler.AppendFormatted(((this.FirstCodeMs != null) ? this.FirstCodeMs.GetValueOrDefault().ToString() : null) ?? "-");
        defaultInterpolatedStringHandler.AppendLiteral(" ms, first say() ");
        defaultInterpolatedStringHandler.AppendFormatted(((this.FirstSayLineMs != null) ? this.FirstSayLineMs.GetValueOrDefault().ToString() : null) ?? "-");
        defaultInterpolatedStringHandler.AppendLiteral(" ms, line 1 to mouth ");
        defaultInterpolatedStringHandler.AppendFormatted(((this.FirstLineToMouthMs != null) ? this.FirstLineToMouthMs.GetValueOrDefault().ToString() : null) ?? "-");
        defaultInterpolatedStringHandler.AppendLiteral(" ms, first audio ");
        defaultInterpolatedStringHandler.AppendFormatted(((firstAudio != null) ? firstAudio.GetValueOrDefault().ToString() : null) ?? "-");
        defaultInterpolatedStringHandler.AppendLiteral(" ms, first spoken ");
        defaultInterpolatedStringHandler.AppendFormatted(((this.FirstSpokenMs != null) ? this.FirstSpokenMs.GetValueOrDefault().ToString() : null) ?? "-");
        defaultInterpolatedStringHandler.AppendLiteral(" ms, total ");
        defaultInterpolatedStringHandler.AppendFormatted<int>((int)(Environment.TickCount64 - this.<startedTicks>P));
        defaultInterpolatedStringHandler.AppendLiteral(" ms (");
        defaultInterpolatedStringHandler.AppendFormatted(this.Model ?? "?");
        defaultInterpolatedStringHandler.AppendLiteral(")");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }

      // Token: 0x040012F3 RID: 4851
      [CompilerGenerated]
      private long <startedTicks>P = startedTicks;

      // Token: 0x040012F4 RID: 4852
      public int? BrainFirstByteMs;

      // Token: 0x040012F5 RID: 4853
      public int? FirstSayLineMs;

      // Token: 0x040012F6 RID: 4854
      public int? FirstSpokenMs;

      // Token: 0x040012F7 RID: 4855
      [Nullable(2)]
      public string Model;

      // Token: 0x040012F8 RID: 4856
      public int? RequestSentMs;

      // Token: 0x040012F9 RID: 4857
      public int? FirstTextMs;

      // Token: 0x040012FA RID: 4858
      public int? FirstCodeMs;

      // Token: 0x040012FB RID: 4859
      public int? FirstLineToMouthMs;
    }

    // Token: 0x020002B9 RID: 697
    [Nullable(0)]
    private sealed class HeldActions : IEquatable<CompanionSession.HeldActions>
    {
      // Token: 0x0600263F RID: 9791 RVA: 0x004028C8 File Offset: 0x004028C8
      public HeldActions(string SessionId, List<BrainToolCall> Calls, CompanionSession.TurnRequest Request, CompanionToolContext Context)
      {
        this.SessionId = SessionId;
        this.Calls = Calls;
        this.Request = Request;
        this.Context = Context;
        base..ctor();
      }

      // Token: 0x170007B6 RID: 1974
      // (get) Token: 0x06002640 RID: 9792 RVA: 0x004028F0 File Offset: 0x004028F0
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(CompanionSession.HeldActions);
        }
      }

      // Token: 0x170007B7 RID: 1975
      // (get) Token: 0x06002641 RID: 9793 RVA: 0x004028FC File Offset: 0x004028FC
      // (set) Token: 0x06002642 RID: 9794 RVA: 0x00402904 File Offset: 0x00402904
      public string SessionId { get; set; }

      // Token: 0x170007B8 RID: 1976
      // (get) Token: 0x06002643 RID: 9795 RVA: 0x00402910 File Offset: 0x00402910
      // (set) Token: 0x06002644 RID: 9796 RVA: 0x00402918 File Offset: 0x00402918
      public List<BrainToolCall> Calls { get; set; }

      // Token: 0x170007B9 RID: 1977
      // (get) Token: 0x06002645 RID: 9797 RVA: 0x00402924 File Offset: 0x00402924
      // (set) Token: 0x06002646 RID: 9798 RVA: 0x0040292C File Offset: 0x0040292C
      public CompanionSession.TurnRequest Request { get; set; }

      // Token: 0x170007BA RID: 1978
      // (get) Token: 0x06002647 RID: 9799 RVA: 0x00402938 File Offset: 0x00402938
      // (set) Token: 0x06002648 RID: 9800 RVA: 0x00402940 File Offset: 0x00402940
      public CompanionToolContext Context { get; set; }

      // Token: 0x06002649 RID: 9801 RVA: 0x0040294C File Offset: 0x0040294C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("HeldActions");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600264A RID: 9802 RVA: 0x00402998 File Offset: 0x00402998
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("SessionId = ");
        builder.Append(this.SessionId);
        builder.Append(", Calls = ");
        builder.Append(this.Calls);
        builder.Append(", Request = ");
        builder.Append(this.Request);
        builder.Append(", Context = ");
        builder.Append(this.Context);
        return true;
      }

      // Token: 0x0600264B RID: 9803 RVA: 0x00402A10 File Offset: 0x00402A10
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(CompanionSession.HeldActions left, CompanionSession.HeldActions right)
      {
        return !(left == right);
      }

      // Token: 0x0600264C RID: 9804 RVA: 0x00402A1C File Offset: 0x00402A1C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(CompanionSession.HeldActions left, CompanionSession.HeldActions right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600264D RID: 9805 RVA: 0x00402A30 File Offset: 0x00402A30
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SessionId>k__BackingField)) * -1521134295 + EqualityComparer<List<BrainToolCall>>.Default.GetHashCode(this.<Calls>k__BackingField)) * -1521134295 + EqualityComparer<CompanionSession.TurnRequest>.Default.GetHashCode(this.<Request>k__BackingField)) * -1521134295 + EqualityComparer<CompanionToolContext>.Default.GetHashCode(this.<Context>k__BackingField);
      }

      // Token: 0x0600264E RID: 9806 RVA: 0x00402AAC File Offset: 0x00402AAC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as CompanionSession.HeldActions);
      }

      // Token: 0x0600264F RID: 9807 RVA: 0x00402ABC File Offset: 0x00402ABC
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(CompanionSession.HeldActions other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<SessionId>k__BackingField, other.<SessionId>k__BackingField) && EqualityComparer<List<BrainToolCall>>.Default.Equals(this.<Calls>k__BackingField, other.<Calls>k__BackingField) && EqualityComparer<CompanionSession.TurnRequest>.Default.Equals(this.<Request>k__BackingField, other.<Request>k__BackingField) && EqualityComparer<CompanionToolContext>.Default.Equals(this.<Context>k__BackingField, other.<Context>k__BackingField));
      }

      // Token: 0x06002651 RID: 9809 RVA: 0x00402B50 File Offset: 0x00402B50
      [CompilerGenerated]
      private HeldActions(CompanionSession.HeldActions original)
      {
        this.SessionId = original.<SessionId>k__BackingField;
        this.Calls = original.<Calls>k__BackingField;
        this.Request = original.<Request>k__BackingField;
        this.Context = original.<Context>k__BackingField;
      }

      // Token: 0x06002652 RID: 9810 RVA: 0x00402B88 File Offset: 0x00402B88
      [CompilerGenerated]
      public void Deconstruct(out string SessionId, out List<BrainToolCall> Calls, out CompanionSession.TurnRequest Request, out CompanionToolContext Context)
      {
        SessionId = this.SessionId;
        Calls = this.Calls;
        Request = this.Request;
        Context = this.Context;
      }
    }

    // Token: 0x020002BA RID: 698
    [Nullable(0)]
    private sealed class LessonRecording
    {
      // Token: 0x06002653 RID: 9811 RVA: 0x00402BAC File Offset: 0x00402BAC
      public LessonRecording(string question, ScreenCaptureFrame screen, string color, bool savesDrawing, bool keepsInChat)
      {
      }

      // Token: 0x04001300 RID: 4864
      public readonly string Question = question;

      // Token: 0x04001301 RID: 4865
      public readonly ScreenCaptureFrame Screen = screen;

      // Token: 0x04001302 RID: 4866
      public readonly string Color = color;

      // Token: 0x04001303 RID: 4867
      public readonly bool SavesDrawing = savesDrawing || keepsInChat;

      // Token: 0x04001304 RID: 4868
      public readonly bool KeepsInChat = keepsInChat;

      // Token: 0x04001305 RID: 4869
      public readonly long StartedTicks = Environment.TickCount64;

      // Token: 0x04001306 RID: 4870
      public readonly Dictionary<int, double> Starts = new Dictionary<int, double>();

      // Token: 0x04001307 RID: 4871
      public readonly Dictionary<int, double> Lengths = new Dictionary<int, double>();

      // Token: 0x04001308 RID: 4872
      public readonly Dictionary<int, MemoryStream> Audio = new Dictionary<int, MemoryStream>();
    }

    // Token: 0x020002BB RID: 699
    [Nullable(0)]
    public sealed class PrefetchedTurn
    {
      // Token: 0x06002654 RID: 9812 RVA: 0x00402C14 File Offset: 0x00402C14
      internal PrefetchedTurn(string fact, CompanionSession.TurnCapture capture, string accessToken, [TupleElementNames(new string[] { "DisplayName", "ProcessName" })] [Nullable(new byte[] { 0, 2, 2 })] ValueTuple<string, string> frontApp, [Nullable(2)] string accountId)
      {
        this.Fact = fact;
        this.Capture = capture;
        this.AccessToken = accessToken;
        this.FrontApp = frontApp;
        this.AccountId = accountId;
        this.Feeder = new CompanionSession.HeldFeeder(this.cancellation.Token);
      }

      // Token: 0x170007BB RID: 1979
      // (get) Token: 0x06002655 RID: 9813 RVA: 0x00402C98 File Offset: 0x00402C98
      [Nullable(2)]
      public string AccountId
      {
        [NullableContext(2)]
        get;
      }

      // Token: 0x170007BC RID: 1980
      // (get) Token: 0x06002656 RID: 9814 RVA: 0x00402CA0 File Offset: 0x00402CA0
      // (set) Token: 0x06002657 RID: 9815 RVA: 0x00402CA8 File Offset: 0x00402CA8
      public DateTimeOffset StartedAt { get; internal set; }

      // Token: 0x170007BD RID: 1981
      // (get) Token: 0x06002658 RID: 9816 RVA: 0x00402CB4 File Offset: 0x00402CB4
      public int CodeCharactersReady
      {
        get
        {
          return this.Feeder.LatestLength;
        }
      }

      // Token: 0x170007BE RID: 1982
      // (get) Token: 0x06002659 RID: 9817 RVA: 0x00402CC4 File Offset: 0x00402CC4
      public bool IsAdopted
      {
        get
        {
          return this.isAdopted;
        }
      }

      // Token: 0x170007BF RID: 1983
      // (get) Token: 0x0600265A RID: 9818 RVA: 0x00402CD0 File Offset: 0x00402CD0
      public bool IsDropped
      {
        get
        {
          return this.isDropped;
        }
      }

      // Token: 0x170007C0 RID: 1984
      // (get) Token: 0x0600265B RID: 9819 RVA: 0x00402CDC File Offset: 0x00402CDC
      public bool DidFail
      {
        get
        {
          object gate = this.Narration.Gate;
          bool flag2;
          lock (gate)
          {
            flag2 = this.Narration.Failure != null;
          }
          return flag2;
        }
      }

      // Token: 0x170007C1 RID: 1985
      // (get) Token: 0x0600265C RID: 9820 RVA: 0x00402D30 File Offset: 0x00402D30
      [Nullable(2)]
      public string Failure
      {
        [NullableContext(2)]
        get
        {
          object gate = this.Narration.Gate;
          string failure;
          lock (gate)
          {
            failure = this.Narration.Failure;
          }
          return failure;
        }
      }

      // Token: 0x170007C2 RID: 1986
      // (get) Token: 0x0600265D RID: 9821 RVA: 0x00402D7C File Offset: 0x00402D7C
      internal CancellationToken Token
      {
        get
        {
          return this.cancellation.Token;
        }
      }

      // Token: 0x170007C3 RID: 1987
      // (get) Token: 0x0600265E RID: 9822 RVA: 0x00402D8C File Offset: 0x00402D8C
      internal Task<bool> CommitGate
      {
        get
        {
          return this.commitGate.Task;
        }
      }

      // Token: 0x0600265F RID: 9823 RVA: 0x00402D9C File Offset: 0x00402D9C
      internal bool StillCurrent()
      {
        if (!this.cancellation.IsCancellationRequested)
        {
          Func<bool> func = this.playingTurnIsCurrent;
          return func == null || func();
        }
        return false;
      }

      // Token: 0x06002660 RID: 9824 RVA: 0x00402DC0 File Offset: 0x00402DC0
      internal void MarkAdopted()
      {
        this.isAdopted = true;
        this.commitGate.TrySetResult(true);
      }

      // Token: 0x06002661 RID: 9825 RVA: 0x00402DD8 File Offset: 0x00402DD8
      internal void Drop()
      {
        this.isDropped = true;
        this.commitGate.TrySetResult(false);
        this.cancellation.Cancel();
      }

      // Token: 0x06002662 RID: 9826 RVA: 0x00402DFC File Offset: 0x00402DFC
      internal void Cancel()
      {
        this.cancellation.Cancel();
      }

      // Token: 0x06002663 RID: 9827 RVA: 0x00402E0C File Offset: 0x00402E0C
      internal Task PlayAsync(CompanionSession session, CompanionSession.CanvasFeeder feeder, Func<bool> stillCurrent, CancellationToken ct)
      {
        CompanionSession.PrefetchedTurn.<PlayAsync>d__39 <PlayAsync>d__;
        <PlayAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
        <PlayAsync>d__.<>4__this = this;
        <PlayAsync>d__.session = session;
        <PlayAsync>d__.feeder = feeder;
        <PlayAsync>d__.stillCurrent = stillCurrent;
        <PlayAsync>d__.ct = ct;
        <PlayAsync>d__.<>1__state = -1;
        <PlayAsync>d__.<>t__builder.Start<CompanionSession.PrefetchedTurn.<PlayAsync>d__39>(ref <PlayAsync>d__);
        return <PlayAsync>d__.<>t__builder.Task;
      }

      // Token: 0x04001309 RID: 4873
      internal readonly string Fact;

      // Token: 0x0400130A RID: 4874
      internal readonly CompanionSession.TurnCapture Capture;

      // Token: 0x0400130B RID: 4875
      internal readonly string AccessToken;

      // Token: 0x0400130C RID: 4876
      [TupleElementNames(new string[] { "DisplayName", "ProcessName" })]
      [Nullable(new byte[] { 0, 2, 2 })]
      internal readonly ValueTuple<string, string> FrontApp;

      // Token: 0x0400130D RID: 4877
      [Nullable(2)]
      internal CompanionSession.TurnRequest Request;

      // Token: 0x0400130E RID: 4878
      internal readonly CompanionSession.NarrationState Narration = new CompanionSession.NarrationState
      {
        HeldForAdoption = true
      };

      // Token: 0x0400130F RID: 4879
      internal readonly CompanionSession.HeldFeeder Feeder;

      // Token: 0x04001310 RID: 4880
      internal Task Streaming = Task.CompletedTask;

      // Token: 0x04001311 RID: 4881
      private readonly CancellationTokenSource cancellation = new CancellationTokenSource();

      // Token: 0x04001312 RID: 4882
      private readonly TaskCompletionSource<bool> commitGate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

      // Token: 0x04001313 RID: 4883
      [Nullable(2)]
      private volatile Func<bool> playingTurnIsCurrent;

      // Token: 0x04001314 RID: 4884
      private volatile bool isAdopted;

      // Token: 0x04001315 RID: 4885
      private volatile bool isDropped;
    }

    // Token: 0x020002BC RID: 700
    internal interface IProgramFeeder
    {
      // Token: 0x06002664 RID: 9828
      void Push(string code, bool done);

      // Token: 0x06002665 RID: 9829
      Task DrainAsync();
    }

    // Token: 0x020002BD RID: 701
    [Nullable(0)]
    internal sealed class HeldFeeder : CompanionSession.IProgramFeeder
    {
      // Token: 0x06002666 RID: 9830 RVA: 0x00402E70 File Offset: 0x00402E70
      public HeldFeeder(CancellationToken dropped)
      {
      }

      // Token: 0x170007C4 RID: 1988
      // (get) Token: 0x06002667 RID: 9831 RVA: 0x00402E98 File Offset: 0x00402E98
      public int LatestLength
      {
        get
        {
          object obj = this.feedGate;
          int num;
          lock (obj)
          {
            string text = this.latestCode;
            num = ((text != null) ? text.Length : 0);
          }
          return num;
        }
      }

      // Token: 0x06002668 RID: 9832 RVA: 0x00402EE8 File Offset: 0x00402EE8
      public void Push(string code, bool done)
      {
        object obj = this.feedGate;
        lock (obj)
        {
          this.latestCode = code;
          this.latestDone = this.latestDone || done;
          CompanionSession.CanvasFeeder canvasFeeder = this.target;
          if (canvasFeeder != null)
          {
            canvasFeeder.Push(code, done);
          }
        }
      }

      // Token: 0x06002669 RID: 9833 RVA: 0x00402F4C File Offset: 0x00402F4C
      public void Attach(CompanionSession.CanvasFeeder feeder)
      {
        object obj = this.feedGate;
        lock (obj)
        {
          this.target = feeder;
          if (this.latestCode != null)
          {
            feeder.Push(this.latestCode, this.latestDone);
          }
        }
        this.attached.TrySetResult(feeder);
      }

      // Token: 0x0600266A RID: 9834 RVA: 0x00402FB4 File Offset: 0x00402FB4
      public async Task DrainAsync()
      {
        await (await this.attached.Task.WaitAsync(this.<dropped>P).ConfigureAwait(false)).DrainAsync().ConfigureAwait(false);
      }

      // Token: 0x04001318 RID: 4888
      [CompilerGenerated]
      private CancellationToken <dropped>P = dropped;

      // Token: 0x04001319 RID: 4889
      private readonly object feedGate = new object();

      // Token: 0x0400131A RID: 4890
      [Nullable(2)]
      private string latestCode;

      // Token: 0x0400131B RID: 4891
      private bool latestDone;

      // Token: 0x0400131C RID: 4892
      [Nullable(2)]
      private CompanionSession.CanvasFeeder target;

      // Token: 0x0400131D RID: 4893
      private readonly TaskCompletionSource<CompanionSession.CanvasFeeder> attached = new TaskCompletionSource<CompanionSession.CanvasFeeder>(TaskCreationOptions.RunContinuationsAsynchronously);
    }

    // Token: 0x020002BE RID: 702
    [NullableContext(2)]
    [Nullable(0)]
    private sealed class GuidedTarget : IEquatable<CompanionSession.GuidedTarget>
    {
      // Token: 0x0600266B RID: 9835 RVA: 0x00402FF8 File Offset: 0x00402FF8
      public GuidedTarget(long Id, PointD CenterPx, double RadiusPx, RectD? ControlFramePx, RectD DisplayBoundsPx, double Scale, string Label, [Nullable(1)] string SpokenInstruction, int Step, bool IsHover, long ArmedTicks, bool Typed, bool AnswersInText, string FrontProcess)
      {
        this.Id = Id;
        this.CenterPx = CenterPx;
        this.RadiusPx = RadiusPx;
        this.ControlFramePx = ControlFramePx;
        this.DisplayBoundsPx = DisplayBoundsPx;
        this.Scale = Scale;
        this.Label = Label;
        this.SpokenInstruction = SpokenInstruction;
        this.Step = Step;
        this.IsHover = IsHover;
        this.ArmedTicks = ArmedTicks;
        this.Typed = Typed;
        this.AnswersInText = AnswersInText;
        this.FrontProcess = FrontProcess;
        base..ctor();
      }

      // Token: 0x170007C5 RID: 1989
      // (get) Token: 0x0600266C RID: 9836 RVA: 0x00403078 File Offset: 0x00403078
      [Nullable(1)]
      [CompilerGenerated]
      private Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(CompanionSession.GuidedTarget);
        }
      }

      // Token: 0x170007C6 RID: 1990
      // (get) Token: 0x0600266D RID: 9837 RVA: 0x00403084 File Offset: 0x00403084
      // (set) Token: 0x0600266E RID: 9838 RVA: 0x0040308C File Offset: 0x0040308C
      public long Id { get; set; }

      // Token: 0x170007C7 RID: 1991
      // (get) Token: 0x0600266F RID: 9839 RVA: 0x00403098 File Offset: 0x00403098
      // (set) Token: 0x06002670 RID: 9840 RVA: 0x004030A0 File Offset: 0x004030A0
      public PointD CenterPx { get; set; }

      // Token: 0x170007C8 RID: 1992
      // (get) Token: 0x06002671 RID: 9841 RVA: 0x004030AC File Offset: 0x004030AC
      // (set) Token: 0x06002672 RID: 9842 RVA: 0x004030B4 File Offset: 0x004030B4
      public double RadiusPx { get; set; }

      // Token: 0x170007C9 RID: 1993
      // (get) Token: 0x06002673 RID: 9843 RVA: 0x004030C0 File Offset: 0x004030C0
      // (set) Token: 0x06002674 RID: 9844 RVA: 0x004030C8 File Offset: 0x004030C8
      public RectD? ControlFramePx { get; set; }

      // Token: 0x170007CA RID: 1994
      // (get) Token: 0x06002675 RID: 9845 RVA: 0x004030D4 File Offset: 0x004030D4
      // (set) Token: 0x06002676 RID: 9846 RVA: 0x004030DC File Offset: 0x004030DC
      public RectD DisplayBoundsPx { get; set; }

      // Token: 0x170007CB RID: 1995
      // (get) Token: 0x06002677 RID: 9847 RVA: 0x004030E8 File Offset: 0x004030E8
      // (set) Token: 0x06002678 RID: 9848 RVA: 0x004030F0 File Offset: 0x004030F0
      public double Scale { get; set; }

      // Token: 0x170007CC RID: 1996
      // (get) Token: 0x06002679 RID: 9849 RVA: 0x004030FC File Offset: 0x004030FC
      // (set) Token: 0x0600267A RID: 9850 RVA: 0x00403104 File Offset: 0x00403104
      public string Label { get; set; }

      // Token: 0x170007CD RID: 1997
      // (get) Token: 0x0600267B RID: 9851 RVA: 0x00403110 File Offset: 0x00403110
      // (set) Token: 0x0600267C RID: 9852 RVA: 0x00403118 File Offset: 0x00403118
      [Nullable(1)]
      public string SpokenInstruction
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x170007CE RID: 1998
      // (get) Token: 0x0600267D RID: 9853 RVA: 0x00403124 File Offset: 0x00403124
      // (set) Token: 0x0600267E RID: 9854 RVA: 0x0040312C File Offset: 0x0040312C
      public int Step { get; set; }

      // Token: 0x170007CF RID: 1999
      // (get) Token: 0x0600267F RID: 9855 RVA: 0x00403138 File Offset: 0x00403138
      // (set) Token: 0x06002680 RID: 9856 RVA: 0x00403140 File Offset: 0x00403140
      public bool IsHover { get; set; }

      // Token: 0x170007D0 RID: 2000
      // (get) Token: 0x06002681 RID: 9857 RVA: 0x0040314C File Offset: 0x0040314C
      // (set) Token: 0x06002682 RID: 9858 RVA: 0x00403154 File Offset: 0x00403154
      public long ArmedTicks { get; set; }

      // Token: 0x170007D1 RID: 2001
      // (get) Token: 0x06002683 RID: 9859 RVA: 0x00403160 File Offset: 0x00403160
      // (set) Token: 0x06002684 RID: 9860 RVA: 0x00403168 File Offset: 0x00403168
      public bool Typed { get; set; }

      // Token: 0x170007D2 RID: 2002
      // (get) Token: 0x06002685 RID: 9861 RVA: 0x00403174 File Offset: 0x00403174
      // (set) Token: 0x06002686 RID: 9862 RVA: 0x0040317C File Offset: 0x0040317C
      public bool AnswersInText { get; set; }

      // Token: 0x170007D3 RID: 2003
      // (get) Token: 0x06002687 RID: 9863 RVA: 0x00403188 File Offset: 0x00403188
      // (set) Token: 0x06002688 RID: 9864 RVA: 0x00403190 File Offset: 0x00403190
      public string FrontProcess { get; set; }

      // Token: 0x06002689 RID: 9865 RVA: 0x0040319C File Offset: 0x0040319C
      public bool Contains(PointD point)
      {
        RectD? controlFramePx = this.ControlFramePx;
        if (controlFramePx != null)
        {
          RectD valueOrDefault = controlFramePx.GetValueOrDefault();
          double num = 8.0 * this.Scale;
          return point.X >= valueOrDefault.X - num && point.X <= valueOrDefault.Right + num && point.Y >= valueOrDefault.Y - num && point.Y <= valueOrDefault.Bottom + num;
        }
        return CompanionSession.Distance(point, this.CenterPx) <= this.RadiusPx;
      }

      // Token: 0x0600268A RID: 9866 RVA: 0x00403238 File Offset: 0x00403238
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("GuidedTarget");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600268B RID: 9867 RVA: 0x00403284 File Offset: 0x00403284
      [NullableContext(1)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Id = ");
        builder.Append(this.Id.ToString());
        builder.Append(", CenterPx = ");
        builder.Append(this.CenterPx.ToString());
        builder.Append(", RadiusPx = ");
        builder.Append(this.RadiusPx.ToString());
        builder.Append(", ControlFramePx = ");
        builder.Append(this.ControlFramePx.ToString());
        builder.Append(", DisplayBoundsPx = ");
        builder.Append(this.DisplayBoundsPx.ToString());
        builder.Append(", Scale = ");
        builder.Append(this.Scale.ToString());
        builder.Append(", Label = ");
        builder.Append(this.Label);
        builder.Append(", SpokenInstruction = ");
        builder.Append(this.SpokenInstruction);
        builder.Append(", Step = ");
        builder.Append(this.Step.ToString());
        builder.Append(", IsHover = ");
        builder.Append(this.IsHover.ToString());
        builder.Append(", ArmedTicks = ");
        builder.Append(this.ArmedTicks.ToString());
        builder.Append(", Typed = ");
        builder.Append(this.Typed.ToString());
        builder.Append(", AnswersInText = ");
        builder.Append(this.AnswersInText.ToString());
        builder.Append(", FrontProcess = ");
        builder.Append(this.FrontProcess);
        return true;
      }

      // Token: 0x0600268C RID: 9868 RVA: 0x00403494 File Offset: 0x00403494
      [CompilerGenerated]
      public static bool operator !=(CompanionSession.GuidedTarget left, CompanionSession.GuidedTarget right)
      {
        return !(left == right);
      }

      // Token: 0x0600268D RID: 9869 RVA: 0x004034A0 File Offset: 0x004034A0
      [CompilerGenerated]
      public static bool operator ==(CompanionSession.GuidedTarget left, CompanionSession.GuidedTarget right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600268E RID: 9870 RVA: 0x004034B4 File Offset: 0x004034B4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<PointD>.Default.GetHashCode(this.<CenterPx>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<RadiusPx>k__BackingField)) * -1521134295 + EqualityComparer<RectD?>.Default.GetHashCode(this.<ControlFramePx>k__BackingField)) * -1521134295 + EqualityComparer<RectD>.Default.GetHashCode(this.<DisplayBoundsPx>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Scale>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Label>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SpokenInstruction>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Step>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsHover>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<ArmedTicks>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Typed>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<AnswersInText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FrontProcess>k__BackingField);
      }

      // Token: 0x0600268F RID: 9871 RVA: 0x00403614 File Offset: 0x00403614
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as CompanionSession.GuidedTarget);
      }

      // Token: 0x06002690 RID: 9872 RVA: 0x00403624 File Offset: 0x00403624
      [CompilerGenerated]
      public bool Equals(CompanionSession.GuidedTarget other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<long>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<PointD>.Default.Equals(this.<CenterPx>k__BackingField, other.<CenterPx>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<RadiusPx>k__BackingField, other.<RadiusPx>k__BackingField) && EqualityComparer<RectD?>.Default.Equals(this.<ControlFramePx>k__BackingField, other.<ControlFramePx>k__BackingField) && EqualityComparer<RectD>.Default.Equals(this.<DisplayBoundsPx>k__BackingField, other.<DisplayBoundsPx>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Scale>k__BackingField, other.<Scale>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Label>k__BackingField, other.<Label>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SpokenInstruction>k__BackingField, other.<SpokenInstruction>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Step>k__BackingField, other.<Step>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsHover>k__BackingField, other.<IsHover>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<ArmedTicks>k__BackingField, other.<ArmedTicks>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Typed>k__BackingField, other.<Typed>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<AnswersInText>k__BackingField, other.<AnswersInText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FrontProcess>k__BackingField, other.<FrontProcess>k__BackingField));
      }

      // Token: 0x06002692 RID: 9874 RVA: 0x004037C8 File Offset: 0x004037C8
      [CompilerGenerated]
      private GuidedTarget([Nullable(1)] CompanionSession.GuidedTarget original)
      {
        this.Id = original.<Id>k__BackingField;
        this.CenterPx = original.<CenterPx>k__BackingField;
        this.RadiusPx = original.<RadiusPx>k__BackingField;
        this.ControlFramePx = original.<ControlFramePx>k__BackingField;
        this.DisplayBoundsPx = original.<DisplayBoundsPx>k__BackingField;
        this.Scale = original.<Scale>k__BackingField;
        this.Label = original.<Label>k__BackingField;
        this.SpokenInstruction = original.<SpokenInstruction>k__BackingField;
        this.Step = original.<Step>k__BackingField;
        this.IsHover = original.<IsHover>k__BackingField;
        this.ArmedTicks = original.<ArmedTicks>k__BackingField;
        this.Typed = original.<Typed>k__BackingField;
        this.AnswersInText = original.<AnswersInText>k__BackingField;
        this.FrontProcess = original.<FrontProcess>k__BackingField;
      }

      // Token: 0x06002693 RID: 9875 RVA: 0x00403884 File Offset: 0x00403884
      [CompilerGenerated]
      public void Deconstruct(out long Id, out PointD CenterPx, out double RadiusPx, out RectD? ControlFramePx, out RectD DisplayBoundsPx, out double Scale, out string Label, [Nullable(1)] out string SpokenInstruction, out int Step, out bool IsHover, out long ArmedTicks, out bool Typed, out bool AnswersInText, out string FrontProcess)
      {
        Id = this.Id;
        CenterPx = this.CenterPx;
        RadiusPx = this.RadiusPx;
        ControlFramePx = this.ControlFramePx;
        DisplayBoundsPx = this.DisplayBoundsPx;
        Scale = this.Scale;
        Label = this.Label;
        SpokenInstruction = this.SpokenInstruction;
        Step = this.Step;
        IsHover = this.IsHover;
        ArmedTicks = this.ArmedTicks;
        Typed = this.Typed;
        AnswersInText = this.AnswersInText;
        FrontProcess = this.FrontProcess;
      }
    }

    // Token: 0x020002BF RID: 703
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x0400132C RID: 4908
      [Nullable(0)]
      public static Action <0>__NoteFirstToken;

      // Token: 0x0400132D RID: 4909
      [Nullable(0)]
      public static Action<int, bool> <1>__NoteRequestBody;
    }
  }
}
