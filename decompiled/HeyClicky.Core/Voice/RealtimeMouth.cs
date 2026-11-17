using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Net;
using HeyClicky.Core.Onboarding;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000080 RID: 128
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class RealtimeMouth : IAsyncDisposable
  {
    // Token: 0x14000010 RID: 16
    // (add) Token: 0x060007A6 RID: 1958 RVA: 0x00365B18 File Offset: 0x00365B18
    // (remove) Token: 0x060007A7 RID: 1959 RVA: 0x00365B50 File Offset: 0x00365B50
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<int, double> LineAudioQueued;

    // Token: 0x14000011 RID: 17
    // (add) Token: 0x060007A8 RID: 1960 RVA: 0x00365B88 File Offset: 0x00365B88
    // (remove) Token: 0x060007A9 RID: 1961 RVA: 0x00365BC0 File Offset: 0x00365BC0
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event LineAudioChunkHandler LineAudioChunk;

    // Token: 0x14000012 RID: 18
    // (add) Token: 0x060007AA RID: 1962 RVA: 0x00365BF8 File Offset: 0x00365BF8
    // (remove) Token: 0x060007AB RID: 1963 RVA: 0x00365C30 File Offset: 0x00365C30
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<int, double> LineGenerated;

    // Token: 0x14000013 RID: 19
    // (add) Token: 0x060007AC RID: 1964 RVA: 0x00365C68 File Offset: 0x00365C68
    // (remove) Token: 0x060007AD RID: 1965 RVA: 0x00365CA0 File Offset: 0x00365CA0
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<string> Closed;

    // Token: 0x170001B4 RID: 436
    // (get) Token: 0x060007AE RID: 1966 RVA: 0x00365CD8 File Offset: 0x00365CD8
    // (set) Token: 0x060007AF RID: 1967 RVA: 0x00365CE0 File Offset: 0x00365CE0
    [Nullable(2)]
    public string Model
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x170001B5 RID: 437
    // (get) Token: 0x060007B0 RID: 1968 RVA: 0x00365CEC File Offset: 0x00365CEC
    public bool IsOpen
    {
      get
      {
        object obj = this.gate;
        bool flag3;
        lock (obj)
        {
          bool flag2;
          if (!this.geminiReady)
          {
            ClientWebSocket clientWebSocket = this.socket;
            flag2 = clientWebSocket != null && clientWebSocket.State == WebSocketState.Open;
          }
          else
          {
            flag2 = true;
          }
          flag3 = flag2;
        }
        return flag3;
      }
    }

    // Token: 0x170001B6 RID: 438
    // (get) Token: 0x060007B1 RID: 1969 RVA: 0x00365D48 File Offset: 0x00365D48
    public bool IsGeminiSession
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.gemini && this.geminiReady;
        }
        return flag2;
      }
    }

    // Token: 0x170001B7 RID: 439
    // (get) Token: 0x060007B2 RID: 1970 RVA: 0x00365D98 File Offset: 0x00365D98
    public long LastServerMessageTicks
    {
      get
      {
        return Interlocked.Read(ref this.lastServerMessageTicks);
      }
    }

    // Token: 0x170001B8 RID: 440
    // (get) Token: 0x060007B3 RID: 1971 RVA: 0x00365DA8 File Offset: 0x00365DA8
    public int CompletedResponses
    {
      get
      {
        object obj = this.gate;
        int num;
        lock (obj)
        {
          num = this.completedResponses;
        }
        return num;
      }
    }

    // Token: 0x170001B9 RID: 441
    // (get) Token: 0x060007B4 RID: 1972 RVA: 0x00365DEC File Offset: 0x00365DEC
    public double EnqueuedSecondsSinceFlush
    {
      get
      {
        object obj = this.gate;
        double num;
        lock (obj)
        {
          num = this.enqueuedSecondsSinceFlush;
        }
        return num;
      }
    }

    // Token: 0x170001BA RID: 442
    // (get) Token: 0x060007B5 RID: 1973 RVA: 0x00365E30 File Offset: 0x00365E30
    public bool IsResponseInFlight
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.responseInFlight || this.queuedLines.Count > 0;
        }
        return flag2;
      }
    }

    // Token: 0x170001BB RID: 443
    // (get) Token: 0x060007B6 RID: 1974 RVA: 0x00365E88 File Offset: 0x00365E88
    // (set) Token: 0x060007B7 RID: 1975 RVA: 0x00365E90 File Offset: 0x00365E90
    [TupleElementNames(new string[] { "MintMs", "ConnectMs" })]
    [Nullable(0)]
    public ValueTuple<int, int> LastOpenTimings
    {
      [NullableContext(0)]
      [return: TupleElementNames(new string[] { "MintMs", "ConnectMs" })]
      get;
      [NullableContext(0)]
      [param: TupleElementNames(new string[] { "MintMs", "ConnectMs" })]
      private set;
    }

    // Token: 0x170001BC RID: 444
    // (get) Token: 0x060007B8 RID: 1976 RVA: 0x00365E9C File Offset: 0x00365E9C
    // (set) Token: 0x060007B9 RID: 1977 RVA: 0x00365EA4 File Offset: 0x00365EA4
    [Nullable(2)]
    public Exception LastOpenFailure
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x060007BA RID: 1978 RVA: 0x00365EB0 File Offset: 0x00365EB0
    public RealtimeMouth(ClickyHttp http, [Nullable(new byte[] { 1, 1, 2 })] Func<bool, CancellationToken, Task<string>> accessToken, IAudioOutput audio, Func<MouthConfig> config)
    {
      this.http = http;
      this.accessToken = accessToken;
      this.audio = audio;
      this.config = config;
      this.seamSilence = new byte[(int)(0.35 * (double)Math.Max(8000, audio.SampleRate)) * 2];
    }

    // Token: 0x170001BD RID: 445
    // (get) Token: 0x060007BB RID: 1979 RVA: 0x00365FD0 File Offset: 0x00365FD0
    // (set) Token: 0x060007BC RID: 1980 RVA: 0x00365FD8 File Offset: 0x00365FD8
    public Func<bool> IsAtRest { get; set; } = () => true;

    // Token: 0x170001BE RID: 446
    // (get) Token: 0x060007BD RID: 1981 RVA: 0x00365FE4 File Offset: 0x00365FE4
    // (set) Token: 0x060007BE RID: 1982 RVA: 0x00365FEC File Offset: 0x00365FEC
    public Func<bool> KeepWarm { get; set; } = () => false;

    // Token: 0x170001BF RID: 447
    // (get) Token: 0x060007BF RID: 1983 RVA: 0x00365FF8 File Offset: 0x00365FF8
    // (set) Token: 0x060007C0 RID: 1984 RVA: 0x00366000 File Offset: 0x00366000
    public TimeSpan IdleTeardownWindow { get; set; } = RealtimeMouth.IdleTeardown;

    // Token: 0x060007C1 RID: 1985 RVA: 0x0036600C File Offset: 0x0036600C
    private void ArmIdleTeardown()
    {
      RealtimeMouth.<>c__DisplayClass86_0 CS$<>8__locals1 = new RealtimeMouth.<>c__DisplayClass86_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.window = this.IdleTeardownWindow;
      CS$<>8__locals1.cts = new CancellationTokenSource();
      CancellationTokenSource cancellationTokenSource = Interlocked.Exchange<CancellationTokenSource>(ref this.idleTeardown, CS$<>8__locals1.cts);
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      Task.Run(delegate
      {
        RealtimeMouth.<>c__DisplayClass86_0.<<ArmIdleTeardown>b__0>d <<ArmIdleTeardown>b__0>d;
        <<ArmIdleTeardown>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<ArmIdleTeardown>b__0>d.<>4__this = CS$<>8__locals1;
        <<ArmIdleTeardown>b__0>d.<>1__state = -1;
        <<ArmIdleTeardown>b__0>d.<>t__builder.Start<RealtimeMouth.<>c__DisplayClass86_0.<<ArmIdleTeardown>b__0>d>(ref <<ArmIdleTeardown>b__0>d);
        return <<ArmIdleTeardown>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x060007C2 RID: 1986 RVA: 0x0036606C File Offset: 0x0036606C
    private void CancelIdleTeardown()
    {
      CancellationTokenSource cancellationTokenSource = Interlocked.Exchange<CancellationTokenSource>(ref this.idleTeardown, null);
      if (cancellationTokenSource == null)
      {
        return;
      }
      cancellationTokenSource.Cancel();
    }

    // Token: 0x060007C3 RID: 1987 RVA: 0x00366084 File Offset: 0x00366084
    public Task<bool> EnsureOpenAsync(CancellationToken ct = default(CancellationToken))
    {
      object obj = this.gate;
      Task<bool> task2;
      lock (obj)
      {
        if (!this.geminiReady)
        {
          ClientWebSocket clientWebSocket = this.socket;
          if (clientWebSocket == null || clientWebSocket.State != WebSocketState.Open)
          {
            Task<bool> task = this.openingTask;
            if (task != null && !task.IsCompleted)
            {
              return this.openingTask;
            }
            this.openingTask = this.OpenAsync(ct);
            return this.openingTask;
          }
        }
        task2 = Task.FromResult<bool>(true);
      }
      return task2;
    }

    // Token: 0x060007C4 RID: 1988 RVA: 0x00366118 File Offset: 0x00366118
    public async Task RefreshAfterConfigChangeAsync(CancellationToken ct = default(CancellationToken))
    {
      int waited = 0;
      while (this.IsResponseInFlight && waited < 120)
      {
        await Task.Delay(500, ct).ConfigureAwait(false);
        waited++;
      }
      object obj = this.gate;
      bool flag3;
      lock (obj)
      {
        bool flag2;
        if (!this.geminiReady)
        {
          ClientWebSocket clientWebSocket = this.socket;
          if (clientWebSocket == null || clientWebSocket.State != WebSocketState.Open)
          {
            Task<bool> task = this.openingTask;
            flag2 = task != null && !task.IsCompleted;
            goto IL_010C;
          }
        }
        flag2 = true;
        IL_010C:
        flag3 = flag2;
      }
      if (flag3)
      {
        MouthConfig mouthConfig = this.config();
        string text = "Mouth";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 2);
        defaultInterpolatedStringHandler.AppendLiteral("voice settings changed (");
        defaultInterpolatedStringHandler.AppendFormatted(mouthConfig.VoiceIdentifier);
        defaultInterpolatedStringHandler.AppendLiteral(", ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(mouthConfig.Speed, "0.##");
        defaultInterpolatedStringHandler.AppendLiteral("x): reopening the mouth");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        await this.ReplaceAsync(ct).ConfigureAwait(false);
      }
    }

    // Token: 0x060007C5 RID: 1989 RVA: 0x00366164 File Offset: 0x00366164
    public async Task<bool> ReplaceAsync(CancellationToken ct = default(CancellationToken))
    {
      await this.CloseSocketAsync("replaced").ConfigureAwait(false);
      return await this.EnsureOpenAsync(ct).ConfigureAwait(false);
    }

    // Token: 0x060007C6 RID: 1990 RVA: 0x003661B0 File Offset: 0x003661B0
    private unsafe async Task<bool> OpenAsync(CancellationToken ct)
    {
      await Task.Yield();
      long started = Environment.TickCount64;
      bool flag;
      try
      {
        MouthConfig mouthConfig = this.config();
        if (VoiceCatalog.IsGemini(mouthConfig.VoiceIdentifier))
        {
          if (Environment.TickCount64 >= Interlocked.Read(ref RealtimeMouth.geminiUnavailableUntilTicks))
          {
            this.OpenGeminiSpeech(mouthConfig);
            return true;
          }
          Log.Info("Mouth", "expressive voice unavailable lately: this session speaks in the closest OpenAI voice");
          MouthConfig mouthConfig2 = mouthConfig.<Clone>$();
          mouthConfig2.VoiceIdentifier = VoiceCatalog.OpenAIVoice(mouthConfig.VoiceIdentifier);
          mouthConfig = mouthConfig2;
        }
        byte[] array = await this.MintAsync(mouthConfig, ct).ConfigureAwait(false);
        if (array == null)
        {
          flag = false;
        }
        else
        {
          ValueTuple<string, string>? minted = RealtimeMouth.ParseMint(array);
          if (minted == null)
          {
            flag = false;
          }
          else
          {
            int mintMs = (int)(Environment.TickCount64 - started);
            ClientWebSocket ws = new ClientWebSocket();
            ws.Options.SetRequestHeader("Authorization", "Bearer " + minted.Value.Item1);
            ws.Options.KeepAliveInterval = TimeSpan.FromSeconds(20L);
            long connectStarted = Environment.TickCount64;
            using (CancellationTokenSource timeout = CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
              timeout.CancelAfter(TimeSpan.FromSeconds(10L));
              try
              {
                await ws.ConnectAsync(new Uri("wss://api.openai.com/v1/realtime?model=" + Uri.EscapeDataString(minted.Value.Item2)), this.http.Invoker, timeout.Token).ConfigureAwait(false);
              }
              catch (Exception ex) when (!ct.IsCancellationRequested)
              {
                ws.Dispose();
                throw new RealtimeSocketOpenException(ex);
              }
            }
            CancellationTokenSource timeout = null;
            this.LastOpenTimings = new ValueTuple<int, int>(mintMs, (int)(Environment.TickCount64 - connectStarted));
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            object obj2 = this.gate;
            lock (obj2)
            {
              this.socket = ws;
              this.socketLifetime = cancellationTokenSource;
              this.Model = minted.Value.Item2;
              this.responseInFlight = false;
              this.queuedLines.Clear();
              this.gemini = false;
              this.stretcher = null;
            }
            Interlocked.Exchange(ref this.lastServerMessageTicks, Environment.TickCount64);
            this.ReceiveLoopAsync(ws, cancellationTokenSource.Token);
            await this.SendAsync(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.D387E4C9F485410F1F80D2A92B41CC39D9134F6FA916F9DBE042184B17064755), 97).ToArray(), ct).ConfigureAwait(false);
            string text = "Mouth";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 3);
            defaultInterpolatedStringHandler.AppendLiteral("mouth open on ");
            defaultInterpolatedStringHandler.AppendFormatted(minted.Value.Item2);
            defaultInterpolatedStringHandler.AppendLiteral(" (mint ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(mintMs);
            defaultInterpolatedStringHandler.AppendLiteral(" ms, connect ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(this.LastOpenTimings.Item2);
            defaultInterpolatedStringHandler.AppendLiteral(" ms)");
            Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
            ClickyAnalytics.TrackRealtimeSessionOpened(mintMs, this.LastOpenTimings.Item2, (int)(Environment.TickCount64 - started), Interlocked.Exchange(ref RealtimeMouth.hasOpenedASessionThisProcess, 1) == 0);
            this.LastOpenFailure = null;
            this.ArmIdleTeardown();
            flag = true;
          }
        }
      }
      catch (Exception ex2) when (!(ex2 is PaywallReachedException))
      {
        Log.Warn("Mouth", "mouth did not open: " + ex2.Message);
        if (!ct.IsCancellationRequested)
        {
          this.LastOpenFailure = ex2;
        }
        flag = false;
      }
      catch (PaywallReachedException)
      {
        flag = false;
      }
      return flag;
    }

    // Token: 0x060007C7 RID: 1991 RVA: 0x003661FC File Offset: 0x003661FC
    private void OpenGeminiSpeech(MouthConfig pick)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.gemini = true;
        this.geminiReady = true;
        this.geminiVoice = VoiceCatalog.GeminiName(pick.VoiceIdentifier);
        this.Model = "gemini-3.8-flash-lite-tts";
        this.responseInFlight = false;
        this.queuedLines.Clear();
        this.closeAfterResponse = false;
        this.playbackRate = pick.Speed;
        this.stretcher = null;
        this.geminiTurns.Reset();
      }
      Interlocked.Exchange(ref this.lastServerMessageTicks, Environment.TickCount64);
      Task.Run(async delegate
      {
        try
        {
          using (HttpRequestMessage warm = new HttpRequestMessage(HttpMethod.Head, this.http.WorkerUrl("/agent/realtime/warmup")))
          {
            using (CancellationTokenSource timeout = new CancellationTokenSource(TimeSpan.FromSeconds(5L)))
            {
              using (await this.http.Client.SendAsync(warm, timeout.Token).ConfigureAwait(false))
              {
              }
            }
          }
          HttpRequestMessage warm = null;
          CancellationTokenSource timeout = null;
        }
        catch
        {
        }
      });
      Log.Info("Mouth", "mouth ready on the expressive voice " + this.geminiVoice + " (a request per line)");
      this.LastOpenFailure = null;
      this.ArmIdleTeardown();
    }

    // Token: 0x060007C8 RID: 1992 RVA: 0x003662E8 File Offset: 0x003662E8
    private async Task SpeakGeminiLineAsync(string line, int lineIndex, RealtimeMouth.GeminiLine request)
    {
      long started = Environment.TickCount64;
      bool receivedAudio = false;
      string failure = null;
      try
      {
        MouthConfig pick = this.config();
        object obj = this.gate;
        string text;
        lock (obj)
        {
          text = this.geminiVoice;
        }
        byte[] body = GeminiMouthTurns.SpeakRequestBody(text, line, VoiceCatalog.PickedLanguages(pick.Language));
        int attempt = 0;
        while (attempt < 2)
        {
          string text2 = await this.accessToken(attempt > 0, request.Cancel.Token).ConfigureAwait(false);
          if (text2 == null)
          {
            failure = "signed out";
            break;
          }
          Stream stream;
          using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, this.http.WorkerUrl("/agent/realtime/speak")))
          {
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", text2);
            message.Headers.TryAddWithoutValidation("X-Clicky-Usage-Context", pick.UsageContext);
            message.Content = new ByteArrayContent(body);
            message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            using (CancellationTokenSource timeout = CancellationTokenSource.CreateLinkedTokenSource(request.Cancel.Token))
            {
              timeout.CancelAfter(TimeSpan.FromSeconds(30L));
              using (HttpResponseMessage response = await this.http.Client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead, timeout.Token).ConfigureAwait(false))
              {
                int statusCode = (int)response.StatusCode;
                if (statusCode == 401 && attempt == 0)
                {
                  goto IL_06BC;
                }
                if (statusCode != 200)
                {
                  DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
                  defaultInterpolatedStringHandler.AppendLiteral("HTTP ");
                  defaultInterpolatedStringHandler.AppendFormatted<int>(statusCode);
                  failure = defaultInterpolatedStringHandler.ToStringAndClear();
                  break;
                }
                Interlocked.Exchange(ref this.lastServerMessageTicks, Environment.TickCount64);
                stream = await response.Content.ReadAsStreamAsync(timeout.Token).ConfigureAwait(false);
                object obj2 = null;
                int num = 0;
                try
                {
                  using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                  {
                    for (;;)
                    {
                      string text3 = await reader.ReadLineAsync(timeout.Token).ConfigureAwait(false);
                      if (text3 == null)
                      {
                        goto IL_0562;
                      }
                      if (Volatile.Read(ref request.Ended) != 0)
                      {
                        break;
                      }
                      Interlocked.Exchange(ref this.lastServerMessageTicks, Environment.TickCount64);
                      if (text3.Length != 0)
                      {
                        byte[] bytes = Encoding.UTF8.GetBytes(text3);
                        string text4 = GeminiMouthTurns.ErrorMessage(bytes);
                        if (text4 != null)
                        {
                          failure = text4;
                        }
                        else
                        {
                          if (GeminiMouthTurns.IsLineEnd(bytes))
                          {
                            goto IL_0562;
                          }
                          if (!receivedAudio && GeminiMouthTurns.CarriesAudio(bytes))
                          {
                            receivedAudio = true;
                            string text5 = "Mouth";
                            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(59, 2);
                            defaultInterpolatedStringHandler.AppendLiteral("line ");
                            defaultInterpolatedStringHandler.AppendFormatted<int>(lineIndex);
                            defaultInterpolatedStringHandler.AppendLiteral(": expressive voice's first audio ");
                            defaultInterpolatedStringHandler.AppendFormatted<long>(Environment.TickCount64 - started);
                            defaultInterpolatedStringHandler.AppendLiteral(" ms after the request");
                            Log.Info(text5, defaultInterpolatedStringHandler.ToStringAndClear());
                          }
                          this.HandleGeminiMessage(bytes);
                        }
                      }
                    }
                    goto IL_0585;
                    IL_0562:;
                  }
                  num = 1;
                  goto IL_0598;
                  IL_0585:
                  num = 2;
                }
                catch (object obj)
                {
                  obj2 = obj;
                }
                IL_0598:
                if (stream != null)
                {
                  await stream.DisposeAsync();
                }
                obj = obj2;
                if (obj != null)
                {
                  Exception ex = obj as Exception;
                  if (ex == null)
                  {
                    throw obj;
                  }
                  ExceptionDispatchInfo.Capture(ex).Throw();
                }
                int num2 = num;
                if (num2 == 1)
                {
                  break;
                }
                if (num2 == 2)
                {
                  return;
                }
                obj2 = null;
              }
            }
          }
          goto IL_0699;
          IL_06BC:
          attempt++;
          continue;
          IL_0699:
          HttpRequestMessage message = null;
          CancellationTokenSource timeout = null;
          HttpResponseMessage response = null;
          stream = null;
          StreamReader reader = null;
          goto IL_06BC;
        }
        pick = null;
        body = null;
      }
      catch (OperationCanceledException obj3) when (request.Cancel.IsCancellationRequested)
      {
        return;
      }
      catch (Exception ex2)
      {
        failure = ex2.Message;
      }
      if (receivedAudio && failure != null && Volatile.Read(ref request.Ended) == 0)
      {
        string text6 = "Mouth";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(64, 2);
        defaultInterpolatedStringHandler.AppendLiteral("line ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(lineIndex);
        defaultInterpolatedStringHandler.AppendLiteral(": the expressive voice was cut off after its first audio (");
        defaultInterpolatedStringHandler.AppendFormatted(failure);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Warn(text6, defaultInterpolatedStringHandler.ToStringAndClear());
        ClickyAnalytics.ReportInternalError("one_brain", "gemini_mouth_line_cut_off", null, failure, CrashReportLevel.Warning, null, null);
      }
      if (!receivedAudio && failure != null && Volatile.Read(ref request.Ended) == 0)
      {
        string text7 = "Mouth";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(81, 2);
        defaultInterpolatedStringHandler.AppendLiteral("line ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(lineIndex);
        defaultInterpolatedStringHandler.AppendLiteral(": the expressive voice couldn't speak it (");
        defaultInterpolatedStringHandler.AppendFormatted(failure);
        defaultInterpolatedStringHandler.AppendLiteral("): the OpenAI voice for 10 minutes");
        Log.Warn(text7, defaultInterpolatedStringHandler.ToStringAndClear());
        ClickyAnalytics.ReportInternalError("one_brain", "gemini_mouth_line_failed", null, failure, CrashReportLevel.Warning, failure.StartsWith("HTTP ", StringComparison.Ordinal) ? failure.Substring(0, Math.Min(8, failure.Length)) : "stream", null);
        Interlocked.Exchange(ref RealtimeMouth.geminiUnavailableUntilTicks, Environment.TickCount64 + (long)TimeSpan.FromMinutes(10L).TotalMilliseconds);
        object obj = this.gate;
        lock (obj)
        {
          this.closeAfterResponse = true;
        }
        this.QueueLineAudio(new byte[(int)(0.3 * (double)this.audio.SampleRate) * 2], false);
      }
      this.EndGeminiLine(request);
    }

    // Token: 0x060007C9 RID: 1993 RVA: 0x00366344 File Offset: 0x00366344
    private void EndGeminiLine(RealtimeMouth.GeminiLine request)
    {
      if (Interlocked.Exchange(ref request.Ended, 1) != 0)
      {
        return;
      }
      request.Cancel.Cancel();
      object obj = this.gate;
      lock (obj)
      {
        if (this.currentGeminiLine == request)
        {
          this.currentGeminiLine = null;
        }
      }
      this.HandleGeminiMessage(GeminiMouthTurns.LineEndMessage);
    }

    // Token: 0x060007CA RID: 1994 RVA: 0x003663B8 File Offset: 0x003663B8
    public void SetPlaybackRate(double rate)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.playbackRate = rate;
        if (this.stretcher != null)
        {
          this.stretcher.Rate = rate;
        }
      }
      string text = "Mouth";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 1);
      defaultInterpolatedStringHandler.AppendLiteral("expressive voice speed ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(rate, "0.##");
      defaultInterpolatedStringHandler.AppendLiteral("x");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x060007CB RID: 1995 RVA: 0x00366450 File Offset: 0x00366450
    [return: Nullable(new byte[] { 1, 2 })]
    private async Task<byte[]> MintAsync(MouthConfig mouth, CancellationToken ct)
    {
      string text2;
      string text = (TimeZoneInfo.TryConvertWindowsIdToIanaId(TimeZoneInfo.Local.Id, out text2) ? text2 : TimeZoneInfo.Local.Id);
      RealtimeSessionRequest realtimeSessionRequest = new RealtimeSessionRequest(mouth.VoiceIdentifier, mouth.Speed, new RealtimeSessionPromptContext(Array.Empty<string>(), Array.Empty<string>(), 0, text, DateTimeOffset.Now.ToString("yyyy-MM-dd'T'HH:mm:sszzz"), OnboardingCatalog.VoiceGender(mouth.VoiceIdentifier), Array.Empty<JsonElement>(), null), RealtimeMouth.ClientCapabilities, null, null, null);
      byte[] body = JsonSerializer.SerializeToUtf8Bytes<RealtimeSessionRequest>(realtimeSessionRequest, CoreJsonContext.Default.RealtimeSessionRequest);
      int attempt = 0;
      while (attempt < 2)
      {
        string text3 = await this.accessToken(attempt > 0, ct).ConfigureAwait(false);
        if (text3 != null)
        {
          using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, this.http.WorkerUrl("/agent/realtime/session")))
          {
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", text3);
            message.Headers.TryAddWithoutValidation("X-Clicky-Usage-Context", mouth.UsageContext);
            message.Content = new ByteArrayContent(body);
            message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            using (CancellationTokenSource timeout = CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
              timeout.CancelAfter(TimeSpan.FromSeconds(15L));
              using (HttpResponseMessage response = await this.http.Client.SendAsync(message, timeout.Token).ConfigureAwait(false))
              {
                byte[] array = await response.Content.ReadAsByteArrayAsync(timeout.Token).ConfigureAwait(false);
                int statusCode = (int)response.StatusCode;
                if (statusCode != 401 || attempt != 0)
                {
                  PaywallPayload paywallPayload;
                  JsonElement jsonElement;
                  if (BillingPaywallDetector.HandleIfPaywall(statusCode, array, false, out paywallPayload, out jsonElement))
                  {
                    throw new PaywallReachedException(paywallPayload, jsonElement);
                  }
                  if (statusCode < 200 || statusCode >= 300)
                  {
                    throw new WorkerHttpException(statusCode, Encoding.UTF8.GetString(array), "/agent/realtime/session");
                  }
                  return array;
                }
              }
            }
          }
          attempt++;
          continue;
        }
        return null;
      }
      return null;
    }

    // Token: 0x060007CC RID: 1996 RVA: 0x003664A4 File Offset: 0x003664A4
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "Secret", "Model" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    internal static ValueTuple<string, string>? ParseMint(ReadOnlySpan<byte> body)
    {
      ValueTuple<string, string>? valueTuple;
      using (JsonDocument jsonDocument = JsonDocument.Parse(body.ToArray(), default(JsonDocumentOptions)))
      {
        JsonElement rootElement = jsonDocument.RootElement;
        string text = null;
        JsonElement jsonElement;
        JsonElement jsonElement2;
        if (rootElement.TryGetProperty("client_secret", out jsonElement) && jsonElement.ValueKind == JsonValueKind.Object && jsonElement.TryGetProperty("value", out jsonElement2) && jsonElement2.ValueKind == JsonValueKind.String)
        {
          text = jsonElement2.GetString();
        }
        JsonElement jsonElement3;
        if (text == null && rootElement.TryGetProperty("value", out jsonElement3) && jsonElement3.ValueKind == JsonValueKind.String)
        {
          text = jsonElement3.GetString();
        }
        if (string.IsNullOrEmpty(text))
        {
          throw new InvalidDataException("the realtime mint returned no client secret");
        }
        string text2 = null;
        JsonElement jsonElement4;
        JsonElement jsonElement5;
        if (rootElement.TryGetProperty("session", out jsonElement4) && jsonElement4.ValueKind == JsonValueKind.Object && jsonElement4.TryGetProperty("model", out jsonElement5) && jsonElement5.ValueKind == JsonValueKind.String)
        {
          text2 = jsonElement5.GetString();
        }
        JsonElement jsonElement6;
        if (string.IsNullOrEmpty(text2) && rootElement.TryGetProperty("model", out jsonElement6) && jsonElement6.ValueKind == JsonValueKind.String)
        {
          text2 = jsonElement6.GetString();
        }
        valueTuple = new ValueTuple<string, string>?(new ValueTuple<string, string>(text, string.IsNullOrEmpty(text2) ? "gpt-realtime-2.1-mini" : text2));
      }
      return valueTuple;
    }

    // Token: 0x060007CD RID: 1997 RVA: 0x00366600 File Offset: 0x00366600
    public void SpeakLine(string line, int lineIndex)
    {
      string text = line.Trim();
      if (text.Length == 0)
      {
        return;
      }
      this.CancelIdleTeardown();
      object obj = this.gate;
      lock (obj)
      {
        if (this.responseInFlight)
        {
          this.queuedLines.Enqueue(new ValueTuple<string, int>(text, lineIndex));
          return;
        }
        this.responseInFlight = true;
        this.currentLineIndex = lineIndex;
      }
      this.SendLineAsync(text, lineIndex);
    }

    // Token: 0x060007CE RID: 1998 RVA: 0x00366684 File Offset: 0x00366684
    private async Task SendLineAsync(string line, int lineIndex)
    {
      try
      {
        Interlocked.Exchange(ref this.lineSentTicks, Environment.TickCount64);
        RealtimeMouth.GeminiLine geminiLine = null;
        object obj = this.gate;
        lock (obj)
        {
          if (this.gemini)
          {
            this.suppressAudioUntilNextResponse = false;
            this.currentResponseHadAudio = false;
            this.currentResponseAudioBytes = 0L;
            this.currentLineIndex = lineIndex;
            this.stretcher = ((Math.Abs(this.playbackRate - 1.0) < 0.01) ? null : new TimeStretcher(this.audio.SampleRate, this.playbackRate));
            this.geminiTurns.LineSent();
            geminiLine = new RealtimeMouth.GeminiLine();
            this.currentGeminiLine = geminiLine;
          }
        }
        if (geminiLine != null)
        {
          await this.SpeakGeminiLineAsync(line, lineIndex, geminiLine).ConfigureAwait(false);
        }
        else
        {
          await this.SendAsync(RealtimeMouth.BuildVerbatimResponseCreate(line, lineIndex), CancellationToken.None).ConfigureAwait(false);
        }
      }
      catch (Exception ex)
      {
        string text = "Mouth";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 2);
        defaultInterpolatedStringHandler.AppendLiteral("line ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(lineIndex);
        defaultInterpolatedStringHandler.AppendLiteral(" not sent: ");
        defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
        Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
        object obj = this.gate;
        bool flag = false;
        RealtimeMouth.Rendering rendering;
        try
        {
          Monitor.Enter(obj, ref flag);
          this.responseInFlight = false;
          this.renderings.Remove(lineIndex, out rendering);
        }
        finally
        {
          int num;
          if (num < 0 && flag)
          {
            Monitor.Exit(obj);
          }
        }
        if (rendering != null)
        {
          rendering.Done.TrySetResult(null);
        }
        Action<int, double> lineGenerated = this.LineGenerated;
        if (lineGenerated != null)
        {
          lineGenerated(lineIndex, 0.0);
        }
      }
    }

    // Token: 0x060007CF RID: 1999 RVA: 0x003666D8 File Offset: 0x003666D8
    public static byte[] BuildVerbatimResponseCreate(string line, int lineIndex)
    {
      ArrayBufferWriter<byte> arrayBufferWriter = new ArrayBufferWriter<byte>(line.Length + 64);
      using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(arrayBufferWriter, default(JsonWriterOptions)))
      {
        utf8JsonWriter.WriteStartObject();
        utf8JsonWriter.WriteString("response_text", line);
        utf8JsonWriter.WriteBoolean("require_repeat_verbatim", true);
        utf8JsonWriter.WriteEndObject();
      }
      ArrayBufferWriter<byte> arrayBufferWriter2 = new ArrayBufferWriter<byte>(1024 + line.Length * 2);
      using (Utf8JsonWriter utf8JsonWriter2 = new Utf8JsonWriter(arrayBufferWriter2, default(JsonWriterOptions)))
      {
        utf8JsonWriter2.WriteStartObject();
        utf8JsonWriter2.WriteString("type", "response.create");
        utf8JsonWriter2.WriteStartObject("response");
        utf8JsonWriter2.WriteString("conversation", "none");
        utf8JsonWriter2.WriteStartArray("output_modalities");
        utf8JsonWriter2.WriteStringValue("audio");
        utf8JsonWriter2.WriteEndArray();
        utf8JsonWriter2.WriteString("instructions", "# Role and Objective\nYou are the voice of Clicky, a friendly companion who draws on the user's screen while explaining. Your only job is to read the given narration line aloud.\n\n# Rules\n- Say exactly the text of the line, word for word, and nothing else.\n- No preamble, no greeting, no filler, no follow-up question.\n- Speak warmly and naturally, at a steady, unhurried pace.\n- Speak each line in the language it is written in, with that language's natural native accent and pronunciation.\n\nThe user message is a JSON tool result. Output exactly `response_text` and nothing else.");
        utf8JsonWriter2.WriteStartArray("input");
        utf8JsonWriter2.WriteStartObject();
        utf8JsonWriter2.WriteString("type", "message");
        utf8JsonWriter2.WriteString("role", "user");
        utf8JsonWriter2.WriteStartArray("content");
        utf8JsonWriter2.WriteStartObject();
        utf8JsonWriter2.WriteString("type", "input_text");
        utf8JsonWriter2.WriteString("text", Encoding.UTF8.GetString(arrayBufferWriter.WrittenSpan));
        utf8JsonWriter2.WriteEndObject();
        utf8JsonWriter2.WriteEndArray();
        utf8JsonWriter2.WriteEndObject();
        utf8JsonWriter2.WriteEndArray();
        utf8JsonWriter2.WriteStartObject("metadata");
        utf8JsonWriter2.WriteString("clicky_line", lineIndex.ToString());
        utf8JsonWriter2.WriteEndObject();
        utf8JsonWriter2.WriteEndObject();
        utf8JsonWriter2.WriteEndObject();
      }
      return arrayBufferWriter2.WrittenSpan.ToArray();
    }

    // Token: 0x060007D0 RID: 2000 RVA: 0x003668D4 File Offset: 0x003668D4
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<byte[]> RenderLineAsync(string line, int lineIndex, CancellationToken ct)
    {
      Task<byte[]> task = this.BeginRendering(lineIndex);
      this.SpeakLine(line, lineIndex);
      byte[] array;
      using (ct.Register(delegate
      {
        this.EndRendering(lineIndex);
      }))
      {
        array = await task.ConfigureAwait(false);
      }
      return array;
    }

    // Token: 0x060007D1 RID: 2001 RVA: 0x00366930 File Offset: 0x00366930
    [return: Nullable(new byte[] { 1, 2 })]
    internal Task<byte[]> BeginRendering(int lineIndex)
    {
      RealtimeMouth.Rendering rendering = new RealtimeMouth.Rendering();
      object obj = this.gate;
      lock (obj)
      {
        this.renderings[lineIndex] = rendering;
      }
      return rendering.Done.Task;
    }

    // Token: 0x060007D2 RID: 2002 RVA: 0x00366988 File Offset: 0x00366988
    private void EndRendering(int lineIndex)
    {
      object obj = this.gate;
      RealtimeMouth.Rendering rendering;
      lock (obj)
      {
        this.renderings.Remove(lineIndex, out rendering);
      }
      if (rendering != null)
      {
        rendering.Done.TrySetResult(null);
      }
    }

    // Token: 0x060007D3 RID: 2003 RVA: 0x003669E4 File Offset: 0x003669E4
    public bool PlayRenderedLine(byte[] pcm, int lineIndex)
    {
      if (pcm.Length == 0)
      {
        return false;
      }
      double num = (double)pcm.Length / 2.0 / (double)this.audio.SampleRate;
      object obj = this.gate;
      double num2;
      lock (obj)
      {
        if (this.responseInFlight || this.queuedLines.Count > 0)
        {
          return false;
        }
        if (this.audio.Buffered > TimeSpan.Zero && this.enqueuedSecondsSinceFlush > 0.0)
        {
          this.audio.Enqueue(this.seamSilence);
          this.enqueuedSecondsSinceFlush += (double)this.seamSilence.Length / 2.0 / (double)this.audio.SampleRate;
        }
        num2 = this.enqueuedSecondsSinceFlush;
        this.audio.Enqueue(pcm);
        this.enqueuedSecondsSinceFlush += num;
      }
      string text = "Mouth";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(55, 2);
      defaultInterpolatedStringHandler.AppendLiteral("line ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(lineIndex);
      defaultInterpolatedStringHandler.AppendLiteral(": played from a rendering (");
      defaultInterpolatedStringHandler.AppendFormatted<double>(num, "0.0");
      defaultInterpolatedStringHandler.AppendLiteral(" s, no generation wait)");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      Action<int, double> lineAudioQueued = this.LineAudioQueued;
      if (lineAudioQueued != null)
      {
        lineAudioQueued(lineIndex, num2);
      }
      Action<int, double> lineGenerated = this.LineGenerated;
      if (lineGenerated != null)
      {
        lineGenerated(lineIndex, num);
      }
      return true;
    }

    // Token: 0x060007D4 RID: 2004 RVA: 0x00366B70 File Offset: 0x00366B70
    private void AbandonRenderingsLocked(List<RealtimeMouth.Rendering> abandoned)
    {
      abandoned.AddRange(this.renderings.Values);
      this.renderings.Clear();
    }

    // Token: 0x060007D5 RID: 2005 RVA: 0x00366B90 File Offset: 0x00366B90
    public void Interrupt()
    {
      RealtimeMouth.<>c__DisplayClass110_0 CS$<>8__locals1 = new RealtimeMouth.<>c__DisplayClass110_0();
      CS$<>8__locals1.<>4__this = this;
      List<RealtimeMouth.Rendering> list = new List<RealtimeMouth.Rendering>();
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        this.AbandonRenderingsLocked(list);
        CS$<>8__locals1.cancel = this.responseInFlight;
        this.queuedLines.Clear();
        this.suppressAudioUntilNextResponse = CS$<>8__locals1.cancel;
        this.responseInFlight = false;
        this.enqueuedSecondsSinceFlush = 0.0;
        flag2 = this.gemini;
        if (flag2)
        {
          this.stretcher = null;
        }
      }
      foreach (RealtimeMouth.Rendering rendering in list)
      {
        rendering.Done.TrySetResult(null);
      }
      this.audio.Flush();
      if (flag2)
      {
        obj = this.gate;
        RealtimeMouth.GeminiLine geminiLine;
        lock (obj)
        {
          geminiLine = this.currentGeminiLine;
        }
        if (this.geminiTurns.CancelLineInProgress())
        {
          this.FinishResponse(false);
        }
        if (geminiLine != null)
        {
          this.EndGeminiLine(geminiLine);
        }
        return;
      }
      if (!this.IsOpen)
      {
        return;
      }
      Task.Run(delegate
      {
        RealtimeMouth.<>c__DisplayClass110_0.<<Interrupt>b__0>d <<Interrupt>b__0>d;
        <<Interrupt>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<Interrupt>b__0>d.<>4__this = CS$<>8__locals1;
        <<Interrupt>b__0>d.<>1__state = -1;
        <<Interrupt>b__0>d.<>t__builder.Start<RealtimeMouth.<>c__DisplayClass110_0.<<Interrupt>b__0>d>(ref <<Interrupt>b__0>d);
        return <<Interrupt>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x060007D6 RID: 2006 RVA: 0x00366CF0 File Offset: 0x00366CF0
    private async Task SendAsync(byte[] payload, CancellationToken ct)
    {
      ClientWebSocket ws = this.socket;
      if (ws == null || ws.State != WebSocketState.Open)
      {
        throw new InvalidOperationException("the mouth is not open");
      }
      await this.sendLock.WaitAsync(ct).ConfigureAwait(false);
      try
      {
        await ws.SendAsync(payload, WebSocketMessageType.Text, true, ct).ConfigureAwait(false);
      }
      finally
      {
        this.sendLock.Release();
      }
    }

    // Token: 0x060007D7 RID: 2007 RVA: 0x00366D44 File Offset: 0x00366D44
    private async Task ReceiveLoopAsync(ClientWebSocket ws, CancellationToken ct)
    {
      byte[] buffer = ArrayPool<byte>.Shared.Rent(262144);
      int length = 0;
      string reason = "closed";
      try
      {
        while (ws.State == WebSocketState.Open && !ct.IsCancellationRequested)
        {
          if (length == buffer.Length)
          {
            byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length * 2);
            buffer.AsSpan<byte>(0, length).CopyTo(array);
            ArrayPool<byte>.Shared.Return(buffer, false);
            buffer = array;
          }
          ValueWebSocketReceiveResult valueWebSocketReceiveResult = await ws.ReceiveAsync(buffer.AsMemory<byte>(length), ct).ConfigureAwait(false);
          if (valueWebSocketReceiveResult.MessageType == WebSocketMessageType.Close)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 2);
            defaultInterpolatedStringHandler.AppendLiteral("server closed (");
            defaultInterpolatedStringHandler.AppendFormatted<WebSocketCloseStatus?>(ws.CloseStatus);
            defaultInterpolatedStringHandler.AppendLiteral(" ");
            defaultInterpolatedStringHandler.AppendFormatted(ws.CloseStatusDescription);
            defaultInterpolatedStringHandler.AppendLiteral(")");
            reason = defaultInterpolatedStringHandler.ToStringAndClear();
            break;
          }
          length += valueWebSocketReceiveResult.Count;
          if (valueWebSocketReceiveResult.EndOfMessage)
          {
            Interlocked.Exchange(ref this.lastServerMessageTicks, Environment.TickCount64);
            this.HandleServerEvent(buffer.AsSpan<byte>(0, length));
            length = 0;
          }
        }
      }
      catch (Exception ex) when (ex is OperationCanceledException || ex is WebSocketException || ex is ObjectDisposedException)
      {
        reason = ex.Message;
      }
      finally
      {
        ArrayPool<byte>.Shared.Return(buffer, false);
        int? num = null;
        List<RealtimeMouth.Rendering> list = new List<RealtimeMouth.Rendering>();
        object obj2 = this.gate;
        bool flag = false;
        int num2;
        try
        {
          Monitor.Enter(obj2, ref flag);
          if (this.socket == ws)
          {
            this.socket = null;
            if (this.responseInFlight)
            {
              num = new int?(this.currentLineIndex);
            }
            this.responseInFlight = false;
            this.queuedLines.Clear();
            this.AbandonRenderingsLocked(list);
          }
        }
        finally
        {
          if (num2 < 0 && flag)
          {
            Monitor.Exit(obj2);
          }
        }
        List<RealtimeMouth.Rendering>.Enumerator enumerator = list.GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            RealtimeMouth.Rendering rendering = enumerator.Current;
            rendering.Done.TrySetResult(null);
          }
        }
        finally
        {
          if (num2 < 0)
          {
            ((IDisposable)enumerator).Dispose();
          }
        }
        obj2 = this.gate;
        flag = false;
        try
        {
          Monitor.Enter(obj2, ref flag);
          if (this.socket == null && !this.geminiReady)
          {
            this.stretcher = null;
          }
        }
        finally
        {
          if (num2 < 0 && flag)
          {
            Monitor.Exit(obj2);
          }
        }
        if (num != null)
        {
          int valueOrDefault = num.GetValueOrDefault();
          Action<int, double> lineGenerated = this.LineGenerated;
          if (lineGenerated != null)
          {
            lineGenerated(valueOrDefault, 0.0);
          }
        }
        if (!ct.IsCancellationRequested)
        {
          Log.Info("Mouth", "mouth session ended: " + reason);
        }
        Action<string> closed = this.Closed;
        if (closed != null)
        {
          closed(reason);
        }
      }
    }

    // Token: 0x060007D8 RID: 2008 RVA: 0x00366D98 File Offset: 0x00366D98
    [NullableContext(0)]
    internal unsafe void HandleServerEvent(ReadOnlySpan<byte> json)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = this.gemini;
      }
      if (flag2)
      {
        this.HandleGeminiMessage(json);
        return;
      }
      Utf8JsonReader utf8JsonReader = new Utf8JsonReader(json, default(JsonReaderOptions));
      if (!utf8JsonReader.Read() || utf8JsonReader.TokenType != JsonTokenType.StartObject)
      {
        return;
      }
      bool flag3 = false;
      bool flag4 = false;
      while (utf8JsonReader.Read() && utf8JsonReader.TokenType == JsonTokenType.PropertyName)
      {
        if (utf8JsonReader.ValueTextEquals(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.04F69A0EDB2F94586D5C7530ADF4E471A14739D72ADDD4F09B2BA21019596682), 4)))
        {
          utf8JsonReader.Read();
          flag4 = true;
          flag3 = utf8JsonReader.ValueTextEquals(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.51C78A15D294DC594B935E15B57FF8E8397C266AD2F0CA117A26309497ECBF16), 27)) || utf8JsonReader.ValueTextEquals(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.B9F9E1162EDC9F7376A2F09D1BFBD7EE2040CB35157F37F79D01B75D415F9889), 20));
          if (!flag3)
          {
            break;
          }
        }
        else
        {
          if (utf8JsonReader.ValueTextEquals(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.6411970BA342BC3AA2AF86630D4DE747D3356AC6C6F29DAB799E640EC52FB8B4), 5)) && flag3)
          {
            utf8JsonReader.Read();
            this.HandleAudioDelta(ref utf8JsonReader);
            return;
          }
          utf8JsonReader.Read();
          utf8JsonReader.Skip();
        }
      }
      if (!flag4 || flag3)
      {
        return;
      }
      this.HandleControlEvent(json);
    }

    // Token: 0x060007D9 RID: 2009 RVA: 0x00366EC4 File Offset: 0x00366EC4
    private void HandleAudioDelta(ref Utf8JsonReader reader)
    {
      if (reader.TokenType != JsonTokenType.String)
      {
        return;
      }
      ReadOnlySpan<byte> readOnlySpan;
      if (!reader.HasValueSequence)
      {
        readOnlySpan = reader.ValueSpan;
      }
      else
      {
        ReadOnlySequence<byte> valueSequence = reader.ValueSequence;
        readOnlySpan = (in valueSequence).ToArray<byte>();
      }
      ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan;
      if (reader.ValueIsEscaped)
      {
        readOnlySpan2 = Encoding.UTF8.GetBytes(reader.GetString() ?? "");
      }
      int maxDecodedFromUtf8Length = Base64.GetMaxDecodedFromUtf8Length(readOnlySpan2.Length);
      if (maxDecodedFromUtf8Length > this.decodeBuffer.Length)
      {
        ArrayPool<byte>.Shared.Return(this.decodeBuffer, false);
        this.decodeBuffer = ArrayPool<byte>.Shared.Rent(maxDecodedFromUtf8Length);
      }
      int num;
      int num2;
      if (Base64.DecodeFromUtf8(readOnlySpan2, this.decodeBuffer, out num, out num2, true) != OperationStatus.Done || num2 == 0)
      {
        return;
      }
      this.QueueLineAudio(this.decodeBuffer.AsSpan<byte>(0, num2), false);
    }

    // Token: 0x060007DA RID: 2010 RVA: 0x00366F98 File Offset: 0x00366F98
    [NullableContext(0)]
    private void QueueLineAudio(ReadOnlySpan<byte> raw, bool lineTail = false)
    {
      double num = 0.0;
      ReadOnlySpan<byte> readOnlySpan = raw;
      object obj = this.gate;
      bool flag2;
      int num2;
      lock (obj)
      {
        if (this.suppressAudioUntilNextResponse)
        {
          return;
        }
        if (this.stretcher != null && !lineTail)
        {
          readOnlySpan = this.stretcher.Process(raw);
        }
        int length = readOnlySpan.Length;
        if (length == 0)
        {
          return;
        }
        RealtimeMouth.Rendering rendering;
        if (this.renderings.TryGetValue(this.currentLineIndex, out rendering))
        {
          rendering.Pcm.Write(readOnlySpan);
          this.currentResponseAudioBytes += (long)length;
          return;
        }
        flag2 = !this.currentResponseHadAudio;
        this.currentResponseHadAudio = true;
        this.currentResponseAudioBytes += (long)length;
        num2 = this.currentLineIndex;
        if (flag2)
        {
          if (this.audio.Buffered > TimeSpan.Zero && this.enqueuedSecondsSinceFlush > 0.0)
          {
            this.audio.Enqueue(this.seamSilence);
            this.enqueuedSecondsSinceFlush += (double)this.seamSilence.Length / 2.0 / (double)this.audio.SampleRate;
          }
          num = this.enqueuedSecondsSinceFlush;
        }
        this.audio.Enqueue(readOnlySpan);
        this.enqueuedSecondsSinceFlush += (double)length / 2.0 / (double)this.audio.SampleRate;
      }
      LineAudioChunkHandler lineAudioChunk = this.LineAudioChunk;
      if (lineAudioChunk != null)
      {
        lineAudioChunk(num2, readOnlySpan);
      }
      if (flag2)
      {
        string text = "Mouth";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 2);
        defaultInterpolatedStringHandler.AppendLiteral("line ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
        defaultInterpolatedStringHandler.AppendLiteral(": first audio ");
        defaultInterpolatedStringHandler.AppendFormatted<long>(Environment.TickCount64 - Interlocked.Read(ref this.lineSentTicks));
        defaultInterpolatedStringHandler.AppendLiteral(" ms after response.create");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        Action<int, double> lineAudioQueued = this.LineAudioQueued;
        if (lineAudioQueued == null)
        {
          return;
        }
        lineAudioQueued(num2, num);
      }
    }

    // Token: 0x060007DB RID: 2011 RVA: 0x003671BC File Offset: 0x003671BC
    [NullableContext(0)]
    private void HandleControlEvent(ReadOnlySpan<byte> json)
    {
      using (JsonDocument jsonDocument = JsonDocument.Parse(json.ToArray(), default(JsonDocumentOptions)))
      {
        JsonElement rootElement = jsonDocument.RootElement;
        string @string = rootElement.GetProperty("type").GetString();
        if (!(@string == "response.created"))
        {
          if (!(@string == "response.done") && !(@string == "response.cancelled"))
          {
            if (!(@string == "error"))
            {
              return;
            }
            object obj = this.gate;
            lock (obj)
            {
              this.responseInFlight = false;
            }
            JsonElement jsonElement;
            JsonElement jsonElement2;
            string text = ((rootElement.TryGetProperty("error", out jsonElement) && jsonElement.TryGetProperty("message", out jsonElement2)) ? jsonElement2.GetString() : rootElement.GetRawText());
            Log.Warn("Mouth", "realtime error: " + text);
            return;
          }
        }
        else
        {
          object obj = this.gate;
          lock (obj)
          {
            this.suppressAudioUntilNextResponse = false;
            this.currentResponseHadAudio = false;
            this.currentResponseAudioBytes = 0L;
            JsonElement jsonElement3;
            JsonElement jsonElement4;
            JsonElement jsonElement5;
            int num;
            if (rootElement.TryGetProperty("response", out jsonElement3) && jsonElement3.TryGetProperty("metadata", out jsonElement4) && jsonElement4.ValueKind == JsonValueKind.Object && jsonElement4.TryGetProperty("clicky_line", out jsonElement5) && int.TryParse(jsonElement5.GetString(), out num))
            {
              this.currentLineIndex = num;
            }
            return;
          }
        }
        this.FinishResponse(@string == "response.done");
      }
    }

    // Token: 0x060007DC RID: 2012 RVA: 0x003673AC File Offset: 0x003673AC
    private void FinishResponse(bool completed)
    {
      object obj = this.gate;
      byte[] array;
      lock (obj)
      {
        array = ((completed && this.stretcher != null) ? this.stretcher.Flush() : null);
        this.stretcher = null;
      }
      if (array != null && array.Length > 0)
      {
        this.QueueLineAudio(array, true);
      }
      ValueTuple<string, int>? valueTuple = null;
      obj = this.gate;
      int num;
      double num2;
      RealtimeMouth.Rendering rendering;
      bool flag2;
      lock (obj)
      {
        num = this.currentLineIndex;
        num2 = (double)this.currentResponseAudioBytes / 2.0 / (double)this.audio.SampleRate;
        this.renderings.Remove(num, out rendering);
        this.completedResponses++;
        this.responseInFlight = false;
        if (this.queuedLines.Count > 0)
        {
          valueTuple = new ValueTuple<string, int>?(this.queuedLines.Dequeue());
          this.responseInFlight = true;
          this.currentLineIndex = valueTuple.Value.Item2;
        }
        flag2 = valueTuple == null && this.closeAfterResponse;
      }
      if (rendering != null)
      {
        rendering.Done.TrySetResult((completed && rendering.Pcm.Length > 0L) ? rendering.Pcm.ToArray() : null);
      }
      Action<int, double> lineGenerated = this.LineGenerated;
      if (lineGenerated != null)
      {
        lineGenerated(num, num2);
      }
      if (valueTuple != null)
      {
        ValueTuple<string, int> valueOrDefault = valueTuple.GetValueOrDefault();
        this.SendLineAsync(valueOrDefault.Item1, valueOrDefault.Item2);
        return;
      }
      if (flag2)
      {
        this.CloseSocketAsync("expressive voice failed");
        return;
      }
      this.ArmIdleTeardown();
    }

    // Token: 0x060007DD RID: 2013 RVA: 0x00367574 File Offset: 0x00367574
    [NullableContext(0)]
    private void HandleGeminiMessage(ReadOnlySpan<byte> json)
    {
      List<GeminiMouthEvent> list;
      try
      {
        list = this.geminiTurns.Events(json);
      }
      catch (JsonException)
      {
        return;
      }
      foreach (GeminiMouthEvent geminiMouthEvent in list)
      {
        switch (geminiMouthEvent.Kind)
        {
        case GeminiMouthEventKind.Audio:
        {
          int num = geminiMouthEvent.Base64Audio.Length / 4 * 3 + 3;
          if (num > this.decodeBuffer.Length)
          {
            ArrayPool<byte>.Shared.Return(this.decodeBuffer, false);
            this.decodeBuffer = ArrayPool<byte>.Shared.Rent(num);
          }
          int num2;
          if (Convert.TryFromBase64String(geminiMouthEvent.Base64Audio, this.decodeBuffer, out num2) && num2 > 0)
          {
            this.QueueLineAudio(this.decodeBuffer.AsSpan<byte>(0, num2), false);
          }
          break;
        }
        case GeminiMouthEventKind.LineDone:
          this.FinishResponse(true);
          break;
        }
      }
    }

    // Token: 0x060007DE RID: 2014 RVA: 0x00367690 File Offset: 0x00367690
    public void NoteAudioFlushed()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.enqueuedSecondsSinceFlush = 0.0;
      }
    }

    // Token: 0x060007DF RID: 2015 RVA: 0x003676DC File Offset: 0x003676DC
    private async Task CloseSocketAsync(string reason)
    {
      List<RealtimeMouth.Rendering> list = new List<RealtimeMouth.Rendering>();
      object obj = this.gate;
      ClientWebSocket ws;
      CancellationTokenSource cancellationTokenSource;
      bool flag2;
      RealtimeMouth.GeminiLine geminiLine;
      lock (obj)
      {
        ws = this.socket;
        cancellationTokenSource = this.socketLifetime;
        this.socket = null;
        this.socketLifetime = null;
        this.responseInFlight = false;
        this.queuedLines.Clear();
        this.AbandonRenderingsLocked(list);
        flag2 = this.geminiReady;
        this.geminiReady = false;
        this.gemini = false;
        this.stretcher = null;
        geminiLine = this.currentGeminiLine;
        this.currentGeminiLine = null;
      }
      foreach (RealtimeMouth.Rendering rendering in list)
      {
        rendering.Done.TrySetResult(null);
      }
      if (geminiLine != null)
      {
        this.EndGeminiLine(geminiLine);
      }
      this.geminiTurns.Reset();
      if (flag2 && !(reason == "disposed"))
      {
        Action<string> closed = this.Closed;
        if (closed != null)
        {
          closed(reason);
        }
      }
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      if (ws != null)
      {
        try
        {
          if (ws.State == WebSocketState.Open)
          {
            using (CancellationTokenSource cts = new CancellationTokenSource(500))
            {
              await ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, reason, cts.Token).ConfigureAwait(false);
            }
            CancellationTokenSource cts = null;
          }
        }
        catch
        {
        }
        ws.Dispose();
      }
    }

    // Token: 0x060007E0 RID: 2016 RVA: 0x00367728 File Offset: 0x00367728
    public async ValueTask DisposeAsync()
    {
      this.CancelIdleTeardown();
      await this.CloseSocketAsync("disposed").ConfigureAwait(false);
      ArrayPool<byte>.Shared.Return(this.decodeBuffer, false);
      this.decodeBuffer = Array.Empty<byte>();
    }

    // Token: 0x040003AA RID: 938
    private const string Area = "Mouth";

    // Token: 0x040003AB RID: 939
    public const string DefaultMouthModel = "gpt-realtime-2.1-mini";

    // Token: 0x040003AC RID: 940
    private const double SeamSilenceSeconds = 0.35;

    // Token: 0x040003AD RID: 941
    private static readonly string[] ClientCapabilities = new string[]
    {
      "spotify_search", "clipboard_copy", "realtime_model_from_mint", "save_memory", "insert_text", "capture_screen", "agent_dashboard", "clicky_memory", "clicky_artifacts", "agent_routines",
      "agent_requests", "onboarding_interview_skip", "one_brain_mouth"
    };

    // Token: 0x040003AE RID: 942
    private const string VerbatimNarrationInstructions = "# Role and Objective\nYou are the voice of Clicky, a friendly companion who draws on the user's screen while explaining. Your only job is to read the given narration line aloud.\n\n# Rules\n- Say exactly the text of the line, word for word, and nothing else.\n- No preamble, no greeting, no filler, no follow-up question.\n- Speak warmly and naturally, at a steady, unhurried pace.\n- Speak each line in the language it is written in, with that language's natural native accent and pronunciation.\n\nThe user message is a JSON tool result. Output exactly `response_text` and nothing else.";

    // Token: 0x040003AF RID: 943
    private readonly ClickyHttp http;

    // Token: 0x040003B0 RID: 944
    [Nullable(new byte[] { 1, 1, 2 })]
    private readonly Func<bool, CancellationToken, Task<string>> accessToken;

    // Token: 0x040003B1 RID: 945
    private readonly IAudioOutput audio;

    // Token: 0x040003B2 RID: 946
    private readonly Func<MouthConfig> config;

    // Token: 0x040003B3 RID: 947
    private readonly SemaphoreSlim sendLock = new SemaphoreSlim(1, 1);

    // Token: 0x040003B4 RID: 948
    private readonly object gate = new object();

    // Token: 0x040003B5 RID: 949
    [TupleElementNames(new string[] { "Text", "LineIndex" })]
    [Nullable(new byte[] { 1, 0, 1 })]
    private readonly Queue<ValueTuple<string, int>> queuedLines = new Queue<ValueTuple<string, int>>();

    // Token: 0x040003B6 RID: 950
    private readonly byte[] seamSilence;

    // Token: 0x040003B7 RID: 951
    [Nullable(2)]
    private ClientWebSocket socket;

    // Token: 0x040003B8 RID: 952
    [Nullable(2)]
    private CancellationTokenSource socketLifetime;

    // Token: 0x040003B9 RID: 953
    [Nullable(2)]
    private Task<bool> openingTask;

    // Token: 0x040003BA RID: 954
    private bool responseInFlight;

    // Token: 0x040003BB RID: 955
    private int currentLineIndex = -1;

    // Token: 0x040003BC RID: 956
    private bool currentResponseHadAudio;

    // Token: 0x040003BD RID: 957
    private long currentResponseAudioBytes;

    // Token: 0x040003BE RID: 958
    private bool suppressAudioUntilNextResponse;

    // Token: 0x040003BF RID: 959
    private double enqueuedSecondsSinceFlush;

    // Token: 0x040003C0 RID: 960
    private int completedResponses;

    // Token: 0x040003C1 RID: 961
    private long lastServerMessageTicks;

    // Token: 0x040003C2 RID: 962
    private static int hasOpenedASessionThisProcess;

    // Token: 0x040003C3 RID: 963
    private byte[] decodeBuffer = ArrayPool<byte>.Shared.Rent(65536);

    // Token: 0x040003C4 RID: 964
    private bool gemini;

    // Token: 0x040003C5 RID: 965
    private bool geminiReady;

    // Token: 0x040003C6 RID: 966
    private string geminiVoice = "";

    // Token: 0x040003C7 RID: 967
    private readonly GeminiMouthTurns geminiTurns = new GeminiMouthTurns();

    // Token: 0x040003C8 RID: 968
    [Nullable(2)]
    private RealtimeMouth.GeminiLine currentGeminiLine;

    // Token: 0x040003C9 RID: 969
    private bool closeAfterResponse;

    // Token: 0x040003CA RID: 970
    private static long geminiUnavailableUntilTicks;

    // Token: 0x040003CB RID: 971
    private double playbackRate = 1.0;

    // Token: 0x040003CC RID: 972
    [Nullable(2)]
    private TimeStretcher stretcher;

    // Token: 0x040003D4 RID: 980
    public static readonly TimeSpan IdleTeardown = TimeSpan.FromMinutes(20L);

    // Token: 0x040003D8 RID: 984
    [Nullable(2)]
    private CancellationTokenSource idleTeardown;

    // Token: 0x040003D9 RID: 985
    public const string GeminiSpeechModel = "gemini-3.8-flash-lite-tts";

    // Token: 0x040003DA RID: 986
    private readonly Dictionary<int, RealtimeMouth.Rendering> renderings = new Dictionary<int, RealtimeMouth.Rendering>();

    // Token: 0x040003DB RID: 987
    private long lineSentTicks;

    // Token: 0x02000342 RID: 834
    [NullableContext(0)]
    private sealed class GeminiLine
    {
      // Token: 0x040016F5 RID: 5877
      [Nullable(1)]
      public readonly CancellationTokenSource Cancel = new CancellationTokenSource();

      // Token: 0x040016F6 RID: 5878
      public int Ended;
    }

    // Token: 0x02000343 RID: 835
    [NullableContext(0)]
    private sealed class Rendering
    {
      // Token: 0x040016F7 RID: 5879
      [Nullable(1)]
      public readonly MemoryStream Pcm = new MemoryStream();

      // Token: 0x040016F8 RID: 5880
      [Nullable(new byte[] { 1, 2 })]
      public readonly TaskCompletionSource<byte[]> Done = new TaskCompletionSource<byte[]>(TaskCreationOptions.RunContinuationsAsynchronously);
    }
  }
}
