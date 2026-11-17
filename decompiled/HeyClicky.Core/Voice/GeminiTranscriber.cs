using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Net;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000079 RID: 121
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class GeminiTranscriber : IAsyncDisposable
  {
    // Token: 0x170001A3 RID: 419
    // (get) Token: 0x06000747 RID: 1863 RVA: 0x00362F2C File Offset: 0x00362F2C
    public string LanguageCode { get; }

    // Token: 0x170001A4 RID: 420
    // (get) Token: 0x06000748 RID: 1864 RVA: 0x00362F34 File Offset: 0x00362F34
    public IReadOnlyList<string> Keyterms { get; }

    // Token: 0x170001A5 RID: 421
    // (get) Token: 0x06000749 RID: 1865 RVA: 0x00362F3C File Offset: 0x00362F3C
    public string UsageContextValue { get; }

    // Token: 0x170001A6 RID: 422
    // (get) Token: 0x0600074A RID: 1866 RVA: 0x00362F44 File Offset: 0x00362F44
    public int SampleRate { get; }

    // Token: 0x170001A7 RID: 423
    // (get) Token: 0x0600074B RID: 1867 RVA: 0x00362F4C File Offset: 0x00362F4C
    // (set) Token: 0x0600074C RID: 1868 RVA: 0x00362F54 File Offset: 0x00362F54
    public bool DidFail { get; private set; }

    // Token: 0x170001A8 RID: 424
    // (get) Token: 0x0600074D RID: 1869 RVA: 0x00362F60 File Offset: 0x00362F60
    // (set) Token: 0x0600074E RID: 1870 RVA: 0x00362F68 File Offset: 0x00362F68
    [Nullable(2)]
    public string FailureMessage
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x170001A9 RID: 425
    // (get) Token: 0x0600074F RID: 1871 RVA: 0x00362F74 File Offset: 0x00362F74
    // (set) Token: 0x06000750 RID: 1872 RVA: 0x00362F7C File Offset: 0x00362F7C
    public bool PaywallReached { get; private set; }

    // Token: 0x170001AA RID: 426
    // (get) Token: 0x06000751 RID: 1873 RVA: 0x00362F88 File Offset: 0x00362F88
    // (set) Token: 0x06000752 RID: 1874 RVA: 0x00362F90 File Offset: 0x00362F90
    [Nullable(2)]
    public string Receipt
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x170001AB RID: 427
    // (get) Token: 0x06000753 RID: 1875 RVA: 0x00362F9C File Offset: 0x00362F9C
    public Task<bool> Opened
    {
      get
      {
        return this.opened.Task;
      }
    }

    // Token: 0x170001AC RID: 428
    // (get) Token: 0x06000754 RID: 1876 RVA: 0x00362FAC File Offset: 0x00362FAC
    public double SentAudioSeconds
    {
      get
      {
        return (double)Interlocked.Read(ref this.sentBytes) / ((double)this.SampleRate * 2.0);
      }
    }

    // Token: 0x170001AD RID: 429
    // (get) Token: 0x06000755 RID: 1877 RVA: 0x00362FCC File Offset: 0x00362FCC
    public bool IsAlive
    {
      get
      {
        if (!this.DidFail && !this.disposed)
        {
          WebSocket webSocket = this.socket;
          return webSocket != null && webSocket.State == WebSocketState.Open;
        }
        return false;
      }
    }

    // Token: 0x06000756 RID: 1878 RVA: 0x00362FF4 File Offset: 0x00362FF4
    public GeminiTranscriber(ClickyHttp http, [Nullable(new byte[] { 1, 1, 2 })] Func<bool, CancellationToken, Task<string>> accessToken, string languageCode, IReadOnlyList<string> keyterms, string usageContext, int sampleRate, string tokenPath = "/brain/ears/token")
      : this(http, accessToken, languageCode, keyterms, usageContext, sampleRate, null, null, tokenPath)
    {
    }

    // Token: 0x06000757 RID: 1879 RVA: 0x00363014 File Offset: 0x00363014
    internal GeminiTranscriber(ClickyHttp http, [Nullable(new byte[] { 1, 1, 2 })] Func<bool, CancellationToken, Task<string>> accessToken, string languageCode, IReadOnlyList<string> keyterms, string usageContext, int sampleRate, [Nullable(new byte[] { 2, 1, 2 })] Func<CancellationToken, Task<GeminiTranscriber.MintedSession>> mint, [Nullable(new byte[] { 2, 1, 1, 1 })] Func<string, CancellationToken, Task<WebSocket>> connect, string tokenPath = "/brain/ears/token")
    {
      bool flag = sampleRate < 8000 || sampleRate > 48000;
      if (flag)
      {
        throw new ArgumentOutOfRangeException("sampleRate");
      }
      this.http = http;
      this.tokenPath = tokenPath;
      this.area = ((tokenPath == "/v2/dictation/gemini-token") ? "Dictation" : "Ears");
      this.accessToken = accessToken;
      this.LanguageCode = languageCode;
      this.Keyterms = keyterms;
      this.UsageContextValue = usageContext;
      this.SampleRate = sampleRate;
      this.mint = mint ?? new Func<CancellationToken, Task<GeminiTranscriber.MintedSession>>(this.MintAsync);
      this.connect = connect ?? new Func<string, CancellationToken, Task<WebSocket>>(this.ConnectAsync);
    }

    // Token: 0x06000758 RID: 1880 RVA: 0x0036315C File Offset: 0x0036315C
    public void Start(bool asWarmSocket = false)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.runTask == null && !this.disposed)
        {
          this.warming = asWarmSocket;
          this.runTask = Task.Run(new Func<Task>(this.RunAsync));
        }
      }
    }

    // Token: 0x06000759 RID: 1881 RVA: 0x003631C8 File Offset: 0x003631C8
    public bool Adopt()
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        if (!this.warming || this.DidFail || this.disposed || this.finishRequested || this.delivered.Task.IsCompleted)
        {
          flag2 = false;
        }
        else
        {
          this.warming = false;
          flag2 = true;
        }
      }
      return flag2;
    }

    // Token: 0x0600075A RID: 1882 RVA: 0x00363244 File Offset: 0x00363244
    [NullableContext(0)]
    public void PushAudio(ReadOnlySpan<byte> pcm16)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (!pcm16.IsEmpty && !this.finishRequested && !this.DidFail && !this.disposed)
        {
          if (pcm16.Length % 2 != 0 || Interlocked.Read(ref this.bufferedBytes) + (long)pcm16.Length > (long)this.SampleRate * 2L * 120L)
          {
            this.Fail("Speech audio exceeded the supported format or duration.");
          }
          else
          {
            this.loudestLevel = Math.Max(this.loudestLevel, GeminiTranscriber.LoudestWindowLevel(pcm16, this.SampleRate));
            byte[] array = ArrayPool<byte>.Shared.Rent(pcm16.Length);
            pcm16.CopyTo(array);
            Interlocked.Add(ref this.bufferedBytes, (long)pcm16.Length);
            if (!this.outgoing.Writer.TryWrite(new GeminiTranscriber.AudioFrame(array, pcm16.Length, false)))
            {
              Interlocked.Add(ref this.bufferedBytes, (long)(-(long)pcm16.Length));
              ArrayPool<byte>.Shared.Return(array, false);
              this.Fail("Speech audio could not be queued.");
            }
          }
        }
      }
    }

    // Token: 0x0600075B RID: 1883 RVA: 0x0036338C File Offset: 0x0036338C
    public string PartialTranscriptSoFar()
    {
      object obj = this.gate;
      string text2;
      lock (obj)
      {
        string text = " ";
        IEnumerable<string> enumerable = this.finals;
        string[] array;
        if (this.interim.Length != 0)
        {
          (array = new string[1])[0] = this.interim;
        }
        else
        {
          array = Array.Empty<string>();
        }
        text2 = string.Join(text, enumerable.Concat<string>(array)).Trim();
      }
      return text2;
    }

    // Token: 0x0600075C RID: 1884 RVA: 0x00363408 File Offset: 0x00363408
    [return: Nullable(new byte[] { 1, 2 })]
    public unsafe async Task<string> FinishAsync(TimeSpan? timeout = null)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.finishRequested = true;
      }
      string text;
      try
      {
        ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.opened.Task.WaitAsync(TimeSpan.FromSeconds(1.5)).ConfigureAwait(false).GetAwaiter();
        if (!configuredTaskAwaiter.IsCompleted)
        {
          await configuredTaskAwaiter;
          ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
          configuredTaskAwaiter = configuredTaskAwaiter2;
          configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
        }
        if (!configuredTaskAwaiter.GetResult())
        {
          text = await this.delivered.Task.ConfigureAwait(false);
        }
        else
        {
          obj = this.gate;
          double num;
          lock (obj)
          {
            if (!this.finishSent && !this.DidFail && !this.disposed)
            {
              this.finishSent = true;
              if (!this.outgoing.Writer.TryWrite(new GeminiTranscriber.AudioFrame(null, 0, false)))
              {
                this.Fail("Speech end could not be queued.");
              }
            }
            num = (double)Interlocked.Read(ref this.bufferedBytes) / ((double)this.SampleRate * 2.0);
          }
          Task uploadBudget = Task.Delay(TimeSpan.FromSeconds(3.0 + num / 4.0));
          <>y__InlineArray3<Task> <>y__InlineArray = default(<>y__InlineArray3<Task>);
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray3<Task>, Task>(ref <>y__InlineArray, 0) = this.activityEndSent.Task;
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray3<Task>, Task>(ref <>y__InlineArray, 1) = this.delivered.Task;
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray3<Task>, Task>(ref <>y__InlineArray, 2) = uploadBudget;
          Task task = await Task.WhenAny(<PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray3<Task>, Task>(in <>y__InlineArray, 3)).ConfigureAwait(false);
          if (task == this.delivered.Task)
          {
            text = await this.delivered.Task.ConfigureAwait(false);
          }
          else if (task == uploadBudget)
          {
            this.Fail("Speech audio upload stalled.");
            text = await this.delivered.Task.ConfigureAwait(false);
          }
          else
          {
            obj = this.gate;
            bool flag2;
            lock (obj)
            {
              flag2 = this.finals.Count == 0 && this.interim.Length == 0 && this.loudestLevel < 0.006;
            }
            if (flag2)
            {
              int num2 = 0;
              try
              {
                return await this.delivered.Task.WaitAsync(GeminiTranscriber.SilentPressWait).ConfigureAwait(false);
              }
              catch (TimeoutException)
              {
                num2 = 1;
              }
              if (num2 == 1)
              {
                obj = this.gate;
                bool flag3;
                lock (obj)
                {
                  flag3 = this.finals.Count > 0 || this.interim.Length > 0;
                  if (!flag3)
                  {
                    this.delivered.TrySetResult("");
                  }
                }
                if (!flag3)
                {
                  return await this.delivered.Task.ConfigureAwait(false);
                }
              }
            }
            text = await this.delivered.Task.WaitAsync(timeout ?? GeminiTranscriber.FinalTranscriptTimeout).ConfigureAwait(false);
          }
        }
      }
      catch (TimeoutException)
      {
        this.Fail("Speech final transcript timed out.");
        text = null;
      }
      return text;
    }

    // Token: 0x0600075D RID: 1885 RVA: 0x00363454 File Offset: 0x00363454
    [NullableContext(2)]
    internal static string CurrentAccountRefusal(DateTimeOffset now)
    {
      object accountRefusalGate = GeminiTranscriber.AccountRefusalGate;
      string text2;
      lock (accountRefusalGate)
      {
        ValueTuple<string, DateTimeOffset>? valueTuple = GeminiTranscriber.accountRefusal;
        string text;
        if (valueTuple != null)
        {
          ValueTuple<string, DateTimeOffset> valueOrDefault = valueTuple.GetValueOrDefault();
          if (valueOrDefault.Item2 > now)
          {
            text = valueOrDefault.Item1;
            goto IL_003E;
          }
        }
        text = null;
        IL_003E:
        text2 = text;
      }
      return text2;
    }

    // Token: 0x0600075E RID: 1886 RVA: 0x003634C0 File Offset: 0x003634C0
    internal static void ClearAccountRefusal()
    {
      object accountRefusalGate = GeminiTranscriber.AccountRefusalGate;
      lock (accountRefusalGate)
      {
        GeminiTranscriber.accountRefusal = null;
      }
    }

    // Token: 0x0600075F RID: 1887 RVA: 0x00363504 File Offset: 0x00363504
    internal static bool IsAccountRefusal(string reason)
    {
      string lowered = reason.ToLowerInvariant();
      return new string[] { "credit", "billing", "quota", "exhausted", "prepayment", "payment" }.Any<string>((string word) => lowered.Contains(word, StringComparison.Ordinal));
    }

    // Token: 0x06000760 RID: 1888 RVA: 0x0036356C File Offset: 0x0036356C
    private Task RunAsync()
    {
      GeminiTranscriber.<RunAsync>d__84 <RunAsync>d__;
      <RunAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
      <RunAsync>d__.<>4__this = this;
      <RunAsync>d__.<>1__state = -1;
      <RunAsync>d__.<>t__builder.Start<GeminiTranscriber.<RunAsync>d__84>(ref <RunAsync>d__);
      return <RunAsync>d__.<>t__builder.Task;
    }

    // Token: 0x06000761 RID: 1889 RVA: 0x003635B0 File Offset: 0x003635B0
    private async Task KeepWarmSocketAliveAsync(CancellationToken ct)
    {
      try
      {
        while (!ct.IsCancellationRequested)
        {
          await Task.Delay(GeminiTranscriber.WarmKeepAliveInterval, ct).ConfigureAwait(false);
          object obj = this.gate;
          lock (obj)
          {
            if (!this.warming || this.finishRequested || this.DidFail || this.disposed)
            {
              break;
            }
            this.outgoing.Writer.TryWrite(new GeminiTranscriber.AudioFrame(null, 0, true));
          }
        }
      }
      catch (OperationCanceledException)
      {
      }
    }

    // Token: 0x06000762 RID: 1890 RVA: 0x003635FC File Offset: 0x003635FC
    [NullableContext(0)]
    internal unsafe static double LoudestWindowLevel(ReadOnlySpan<byte> pcm16, int sampleRate)
    {
      ReadOnlySpan<short> readOnlySpan = MemoryMarshal.Cast<byte, short>(pcm16);
      int num = Math.Max(1, sampleRate / 100);
      double num2 = 0.0;
      for (int i = 0; i < readOnlySpan.Length; i += num)
      {
        int num3 = Math.Min(i + num, readOnlySpan.Length);
        double num4 = 0.0;
        for (int j = i; j < num3; j++)
        {
          double num5 = (double)(*readOnlySpan[j]) / 32768.0;
          num4 += num5 * num5;
        }
        num2 = Math.Max(num2, Math.Sqrt(num4 / (double)(num3 - i)));
      }
      return num2;
    }

    // Token: 0x06000763 RID: 1891 RVA: 0x0036369C File Offset: 0x0036369C
    [return: Nullable(new byte[] { 1, 2 })]
    private async Task<GeminiTranscriber.MintedSession> MintAsync(CancellationToken ct)
    {
      int attempt = 0;
      while (attempt < 2)
      {
        string text = await this.accessToken(attempt > 0, ct).ConfigureAwait(false);
        if (!string.IsNullOrEmpty(text))
        {
          using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, this.http.WorkerUrl(this.tokenPath)))
          {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", text);
            request.Headers.TryAddWithoutValidation("X-Clicky-Usage-Context", this.UsageContextValue);
            using (MemoryStream body = new MemoryStream())
            {
              using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(body, default(JsonWriterOptions)))
              {
                utf8JsonWriter.WriteStartObject();
                utf8JsonWriter.WriteString("language", this.LanguageCode);
                utf8JsonWriter.WriteStartArray("keyterms");
                foreach (string text2 in this.Keyterms)
                {
                  utf8JsonWriter.WriteStringValue(text2);
                }
                utf8JsonWriter.WriteEndArray();
                utf8JsonWriter.WriteEndObject();
              }
              request.Content = new ByteArrayContent(body.ToArray());
              request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
              using (CancellationTokenSource deadline = CancellationTokenSource.CreateLinkedTokenSource(ct))
              {
                deadline.CancelAfter(TimeSpan.FromSeconds(10L));
                using (HttpResponseMessage response = await this.http.Client.SendAsync(request, deadline.Token).ConfigureAwait(false))
                {
                  byte[] array = await response.Content.ReadAsByteArrayAsync(deadline.Token).ConfigureAwait(false);
                  int statusCode = (int)response.StatusCode;
                  if (statusCode != 401 || attempt != 0)
                  {
                    if (statusCode == 402)
                    {
                      object obj = this.gate;
                      bool flag2;
                      lock (obj)
                      {
                        this.PaywallReached = true;
                        flag2 = !this.warming && !this.disposed;
                      }
                      BillingPaywallDetector.HandleIfPaywall(statusCode, array, flag2);
                      this.Fail("Speech quota reached.");
                      return null;
                    }
                    if (!response.IsSuccessStatusCode)
                    {
                      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 1);
                      defaultInterpolatedStringHandler.AppendLiteral("Could not create the speech session (HTTP ");
                      defaultInterpolatedStringHandler.AppendFormatted<int>(statusCode);
                      defaultInterpolatedStringHandler.AppendLiteral(").");
                      this.Fail(defaultInterpolatedStringHandler.ToStringAndClear());
                      return null;
                    }
                    using (JsonDocument jsonDocument = JsonDocument.Parse(array, default(JsonDocumentOptions)))
                    {
                      JsonElement rootElement = jsonDocument.RootElement;
                      JsonElement jsonElement;
                      JsonElement jsonElement2;
                      if (!rootElement.TryGetProperty("access_token", out jsonElement) || jsonElement.ValueKind != JsonValueKind.String || string.IsNullOrEmpty(jsonElement.GetString()) || !rootElement.TryGetProperty("setup", out jsonElement2) || jsonElement2.ValueKind != JsonValueKind.Object)
                      {
                        this.Fail("Invalid speech session response.");
                        return null;
                      }
                      JsonElement jsonElement3;
                      string text3;
                      if (rootElement.TryGetProperty("receipt", out jsonElement3) && jsonElement3.ValueKind == JsonValueKind.String)
                      {
                        string @string = jsonElement3.GetString();
                        if (@string != null && @string.Length > 0)
                        {
                          text3 = @string;
                          goto IL_04CD;
                        }
                      }
                      text3 = null;
                      IL_04CD:
                      string text4 = text3;
                      return new GeminiTranscriber.MintedSession(jsonElement.GetString(), jsonElement2.Clone(), text4);
                    }
                  }
                }
              }
            }
          }
          attempt++;
          continue;
        }
        this.Fail("Please sign in to talk to HeyClicky.");
        return null;
      }
      this.Fail("Speech authentication failed.");
      return null;
    }

    // Token: 0x06000764 RID: 1892 RVA: 0x003636E8 File Offset: 0x003636E8
    private async Task<WebSocket> ConnectAsync(string token, CancellationToken ct)
    {
      ClientWebSocket ws = new ClientWebSocket();
      ws.Options.SetRequestHeader("Authorization", "Token " + token);
      ws.Options.KeepAliveInterval = TimeSpan.FromSeconds(7L);
      ws.Options.KeepAliveTimeout = TimeSpan.FromSeconds(5L);
      WebSocket webSocket;
      try
      {
        using (CancellationTokenSource deadline = CancellationTokenSource.CreateLinkedTokenSource(ct))
        {
          deadline.CancelAfter(TimeSpan.FromSeconds(10L));
          await ws.ConnectAsync(GeminiTranscriber.ListenUrl, this.http.Invoker, deadline.Token).ConfigureAwait(false);
          webSocket = ws;
        }
      }
      catch
      {
        ws.Dispose();
        throw;
      }
      return webSocket;
    }

    // Token: 0x06000765 RID: 1893 RVA: 0x0036373C File Offset: 0x0036373C
    internal static byte[] BuildSetupMessage(JsonElement setup)
    {
      byte[] array;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, default(JsonWriterOptions)))
        {
          utf8JsonWriter.WriteStartObject();
          utf8JsonWriter.WritePropertyName("setup");
          setup.WriteTo(utf8JsonWriter);
          utf8JsonWriter.WriteEndObject();
        }
        array = memoryStream.ToArray();
      }
      return array;
    }

    // Token: 0x06000766 RID: 1894 RVA: 0x003637BC File Offset: 0x003637BC
    [NullableContext(0)]
    [return: Nullable(1)]
    internal static byte[] BuildAudioMessage(ReadOnlySpan<byte> pcm16, int sampleRate)
    {
      byte[] array;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, default(JsonWriterOptions)))
        {
          utf8JsonWriter.WriteStartObject();
          utf8JsonWriter.WriteStartObject("realtimeInput");
          utf8JsonWriter.WriteStartObject("audio");
          utf8JsonWriter.WriteBase64String("data", pcm16);
          Utf8JsonWriter utf8JsonWriter2 = utf8JsonWriter;
          string text = "mimeType";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 1);
          defaultInterpolatedStringHandler.AppendLiteral("audio/pcm;rate=");
          defaultInterpolatedStringHandler.AppendFormatted<int>(sampleRate);
          utf8JsonWriter2.WriteString(text, defaultInterpolatedStringHandler.ToStringAndClear());
          utf8JsonWriter.WriteEndObject();
          utf8JsonWriter.WriteEndObject();
          utf8JsonWriter.WriteEndObject();
        }
        array = memoryStream.ToArray();
      }
      return array;
    }

    // Token: 0x06000767 RID: 1895 RVA: 0x00363888 File Offset: 0x00363888
    private async Task SendLoopAsync(WebSocket ws, CancellationToken ct)
    {
      GeminiTranscriber.<>c__DisplayClass91_0 CS$<>8__locals1 = new GeminiTranscriber.<>c__DisplayClass91_0();
      CS$<>8__locals1.ws = ws;
      CS$<>8__locals1.ct = ct;
      bool activityStarted = false;
      Pcm16MonoResampler resampler = new Pcm16MonoResampler(this.SampleRate, 16000);
      CS$<>8__locals1.chunk = new byte[3200];
      CS$<>8__locals1.chunkLength = 0;
      try
      {
        ConfiguredCancelableAsyncEnumerable<GeminiTranscriber.AudioFrame>.Enumerator enumerator = this.outgoing.Reader.ReadAllAsync(CS$<>8__locals1.ct).ConfigureAwait<GeminiTranscriber.AudioFrame>(false).GetAsyncEnumerator();
        object obj = null;
        try
        {
          for (;;)
          {
            ConfiguredValueTaskAwaitable<bool>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter = enumerator.MoveNextAsync().GetAwaiter();
            if (!configuredValueTaskAwaiter.IsCompleted)
            {
              await configuredValueTaskAwaiter;
              ConfiguredValueTaskAwaitable<bool>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
              configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
              configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<bool>.ConfiguredValueTaskAwaiter);
            }
            if (!configuredValueTaskAwaiter.GetResult())
            {
              break;
            }
            GeminiTranscriber.AudioFrame frame = enumerator.Current;
            try
            {
              if (frame.IsKeepAlive)
              {
                if (!activityStarted)
                {
                  await CS$<>8__locals1.ws.SendAsync(GeminiTranscriber.BuildAudioMessage(new byte[3200], 16000).AsMemory<byte>(), WebSocketMessageType.Text, true, CS$<>8__locals1.ct).ConfigureAwait(false);
                }
                continue;
              }
              if (!activityStarted)
              {
                await CS$<>8__locals1.ws.SendAsync(GeminiTranscriber.ActivityStart.AsMemory<byte>(), WebSocketMessageType.Text, true, CS$<>8__locals1.ct).ConfigureAwait(false);
                activityStarted = true;
              }
              if (frame.Buffer == null)
              {
                await CS$<>8__locals1.<SendLoopAsync>g__SendResampledAsync|0(resampler.Drain()).ConfigureAwait(false);
                if (CS$<>8__locals1.chunkLength > 0)
                {
                  await CS$<>8__locals1.ws.SendAsync(GeminiTranscriber.BuildAudioMessage(CS$<>8__locals1.chunk.AsSpan<byte>(0, CS$<>8__locals1.chunkLength), 16000).AsMemory<byte>(), WebSocketMessageType.Text, true, CS$<>8__locals1.ct).ConfigureAwait(false);
                }
                CS$<>8__locals1.chunkLength = 0;
                await CS$<>8__locals1.ws.SendAsync(GeminiTranscriber.ActivityEnd.AsMemory<byte>(), WebSocketMessageType.Text, true, CS$<>8__locals1.ct).ConfigureAwait(false);
                this.activityEndSent.TrySetResult(true);
                continue;
              }
              await CS$<>8__locals1.<SendLoopAsync>g__SendResampledAsync|0(resampler.Process(frame.Buffer.AsSpan<byte>(0, frame.Length))).ConfigureAwait(false);
              Interlocked.Add(ref this.sentBytes, (long)frame.Length);
            }
            finally
            {
              if (frame.Buffer != null)
              {
                Interlocked.Add(ref this.bufferedBytes, (long)(-(long)frame.Length));
                ArrayPool<byte>.Shared.Return(frame.Buffer, false);
              }
            }
            frame = default(GeminiTranscriber.AudioFrame);
          }
        }
        catch (object obj)
        {
        }
        await enumerator.DisposeAsync();
        object obj2 = obj;
        if (obj2 != null)
        {
          Exception ex = obj2 as Exception;
          if (ex == null)
          {
            throw obj2;
          }
          ExceptionDispatchInfo.Capture(ex).Throw();
        }
        obj = null;
        enumerator = default(ConfiguredCancelableAsyncEnumerable<GeminiTranscriber.AudioFrame>.Enumerator);
      }
      catch (OperationCanceledException obj3) when (CS$<>8__locals1.ct.IsCancellationRequested)
      {
      }
      catch (Exception)
      {
        this.Fail("Could not send speech audio.");
      }
    }

    // Token: 0x06000768 RID: 1896 RVA: 0x003638DC File Offset: 0x003638DC
    private async Task ReceiveLoopAsync(WebSocket ws, CancellationToken ct)
    {
      byte[] buffer = ArrayPool<byte>.Shared.Rent(65536);
      int length = 0;
      try
      {
        while (!this.delivered.Task.IsCompleted && !ct.IsCancellationRequested)
        {
          ValueWebSocketReceiveResult valueWebSocketReceiveResult = await ws.ReceiveAsync(buffer.AsMemory<byte>(length), ct).ConfigureAwait(false);
          if (valueWebSocketReceiveResult.MessageType == WebSocketMessageType.Close)
          {
            this.FailClosedBy(ws, "Speech closed before a final transcript.");
            break;
          }
          length += valueWebSocketReceiveResult.Count;
          if (!valueWebSocketReceiveResult.EndOfMessage)
          {
            if (length == buffer.Length)
            {
              if (buffer.Length >= 1048576)
              {
                this.Fail("Speech response exceeded the supported size.");
                break;
              }
              byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length * 2);
              buffer.AsSpan<byte>(0, length).CopyTo(array);
              ArrayPool<byte>.Shared.Return(buffer, false);
              buffer = array;
            }
          }
          else
          {
            this.HandleMessage(buffer.AsSpan<byte>(0, length));
            length = 0;
          }
        }
      }
      catch (OperationCanceledException obj) when (ct.IsCancellationRequested)
      {
      }
      catch (Exception)
      {
        this.FailClosedBy(ws, "Invalid or interrupted speech response.");
      }
      finally
      {
        ArrayPool<byte>.Shared.Return(buffer, false);
        this.setupReady.TrySetResult(false);
      }
    }

    // Token: 0x06000769 RID: 1897 RVA: 0x00363930 File Offset: 0x00363930
    private void FailClosedBy(WebSocket ws, string withoutReason)
    {
      string closeStatusDescription = ws.CloseStatusDescription;
      string text = ((closeStatusDescription != null) ? closeStatusDescription.Trim() : null);
      if (!string.IsNullOrEmpty(text))
      {
        WebSocketCloseStatus? closeStatus = ws.CloseStatus;
        if (closeStatus != null)
        {
          WebSocketCloseStatus valueOrDefault = closeStatus.GetValueOrDefault();
          if (GeminiTranscriber.IsAccountRefusal(text))
          {
            object accountRefusalGate = GeminiTranscriber.AccountRefusalGate;
            lock (accountRefusalGate)
            {
              GeminiTranscriber.accountRefusal = new ValueTuple<string, DateTimeOffset>?(new ValueTuple<string, DateTimeOffset>(text, DateTimeOffset.UtcNow + GeminiTranscriber.AccountRefusalPause));
            }
            Log.Warn(this.area, "Gemini refused for its account; using the fallback for 10 minutes: " + text);
          }
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 2);
          defaultInterpolatedStringHandler.AppendLiteral("Gemini closed the speech connection (");
          defaultInterpolatedStringHandler.AppendFormatted<int>((int)valueOrDefault);
          defaultInterpolatedStringHandler.AppendLiteral("): ");
          defaultInterpolatedStringHandler.AppendFormatted(text);
          this.Fail(defaultInterpolatedStringHandler.ToStringAndClear());
          return;
        }
      }
      this.Fail(withoutReason);
    }

    // Token: 0x0600076A RID: 1898 RVA: 0x00363A24 File Offset: 0x00363A24
    [NullableContext(0)]
    internal void HandleMessage(ReadOnlySpan<byte> bytes)
    {
      using (JsonDocument jsonDocument = JsonDocument.Parse(bytes.ToArray(), default(JsonDocumentOptions)))
      {
        JsonElement rootElement = jsonDocument.RootElement;
        object obj = this.gate;
        lock (obj)
        {
          if (!this.disposed && !this.delivered.Task.IsCompleted)
          {
            JsonElement jsonElement;
            if (rootElement.TryGetProperty("error", out jsonElement) || rootElement.TryGetProperty("goAway", out jsonElement))
            {
              this.Fail("The speech connection was interrupted.");
            }
            else
            {
              if (rootElement.TryGetProperty("setupComplete", out jsonElement))
              {
                this.setupReady.TrySetResult(true);
              }
              JsonElement jsonElement2;
              if (rootElement.TryGetProperty("serverContent", out jsonElement2))
              {
                JsonElement jsonElement3;
                JsonElement jsonElement4;
                if (jsonElement2.TryGetProperty("interimInputTranscription", out jsonElement3) && jsonElement3.TryGetProperty("text", out jsonElement4))
                {
                  this.interim = jsonElement4.GetString() ?? "";
                }
                JsonElement jsonElement5;
                JsonElement jsonElement6;
                if (jsonElement2.TryGetProperty("inputTranscription", out jsonElement5) && jsonElement5.TryGetProperty("text", out jsonElement6))
                {
                  string @string = jsonElement6.GetString();
                  if (@string != null && @string.Length > 0)
                  {
                    this.finals.Add(@string);
                  }
                  this.interim = "";
                  if (this.finishSent)
                  {
                    this.delivered.TrySetResult(string.Join(" ", this.finals).Trim());
                  }
                }
              }
            }
          }
        }
      }
    }

    // Token: 0x0600076B RID: 1899 RVA: 0x00363BE0 File Offset: 0x00363BE0
    private void Fail(string message)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (!this.delivered.Task.IsCompleted && !this.disposed)
        {
          this.DidFail = true;
          this.FailureMessage = message;
          this.ReportFailureLocked(message);
          this.opened.TrySetResult(false);
          this.setupReady.TrySetResult(false);
          this.delivered.TrySetResult(null);
          this.lifetime.Cancel();
          if (!this.warming && !this.PaywallReached)
          {
            Log.Warn(this.area, message);
          }
        }
      }
    }

    // Token: 0x0600076C RID: 1900 RVA: 0x00363C9C File Offset: 0x00363C9C
    private void ReportFailureLocked(string message)
    {
      if (this.PaywallReached || message.StartsWith("Please sign in", StringComparison.Ordinal) || message == "Speech quota reached.")
      {
        return;
      }
      string text = ((this.tokenPath == "/v2/dictation/gemini-token") ? "dictation" : "ears");
      bool flag = this.setupReady.Task.IsCompletedSuccessfully && this.setupReady.Task.Result;
      string text2 = (this.finishRequested ? "finishing" : (this.warming ? (flag ? "warm_idle" : "warm_connecting") : (flag ? "press" : "press_connecting")));
      WebSocket webSocket = this.socket;
      WebSocketCloseStatus? webSocketCloseStatus = ((webSocket != null) ? webSocket.CloseStatus : null);
      int num;
      if (webSocketCloseStatus != null)
      {
        WebSocketCloseStatus valueOrDefault = webSocketCloseStatus.GetValueOrDefault();
        num = (int)valueOrDefault;
      }
      else
      {
        num = 0;
      }
      int num2 = num;
      WebSocket webSocket2 = this.socket;
      string text3;
      if (webSocket2 == null)
      {
        text3 = null;
      }
      else
      {
        string closeStatusDescription = webSocket2.CloseStatusDescription;
        text3 = ((closeStatusDescription != null) ? closeStatusDescription.Trim() : null);
      }
      string text4 = text3 ?? "";
      if (text4.Length > 200)
      {
        text4 = text4.Substring(0, 200);
      }
      string text5 = "gemini_speech";
      string text6 = "connection_failed";
      Exception ex = null;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(37, 5);
      defaultInterpolatedStringHandler.AppendLiteral("purpose=");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral(" phase=");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral(" close=");
      defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
      defaultInterpolatedStringHandler.AppendLiteral(" reason=");
      defaultInterpolatedStringHandler.AppendFormatted(text4);
      defaultInterpolatedStringHandler.AppendLiteral(" error=");
      defaultInterpolatedStringHandler.AppendFormatted(message);
      string text7 = defaultInterpolatedStringHandler.ToStringAndClear();
      CrashReportLevel crashReportLevel = CrashReportLevel.Warning;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 3);
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral("|close_");
      defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
      ClickyAnalytics.ReportInternalError(text5, text6, ex, text7, crashReportLevel, defaultInterpolatedStringHandler.ToStringAndClear(), null);
    }

    // Token: 0x0600076D RID: 1901 RVA: 0x00363E90 File Offset: 0x00363E90
    public async ValueTask DisposeAsync()
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.disposed)
        {
          return;
        }
        this.disposed = true;
        this.outgoing.Writer.TryComplete(null);
        this.delivered.TrySetResult(null);
        this.opened.TrySetResult(false);
        this.lifetime.Cancel();
        WebSocket webSocket = this.socket;
        if (webSocket != null)
        {
          webSocket.Abort();
        }
      }
      if (this.runTask != null)
      {
        await this.runTask.ConfigureAwait(false);
      }
      WebSocket webSocket2 = this.socket;
      if (webSocket2 != null)
      {
        webSocket2.Dispose();
      }
      GeminiTranscriber.AudioFrame audioFrame;
      while (this.outgoing.Reader.TryRead(out audioFrame))
      {
        if (audioFrame.Buffer != null)
        {
          ArrayPool<byte>.Shared.Return(audioFrame.Buffer, false);
        }
      }
      this.lifetime.Dispose();
    }

    // Token: 0x0400035D RID: 861
    internal static readonly Uri ListenUrl = new Uri("wss://generativelanguage.googleapis.com/ws/google.ai.generativelanguage.v1beta.GenerativeService.BidiGenerateContentConstrained");

    // Token: 0x0400035E RID: 862
    public const string EarsTokenPath = "/brain/ears/token";

    // Token: 0x0400035F RID: 863
    public const string DictationTokenPath = "/v2/dictation/gemini-token";

    // Token: 0x04000360 RID: 864
    public const string SttProviderName = "gemini";

    // Token: 0x04000361 RID: 865
    internal const int StreamSampleRate = 16000;

    // Token: 0x04000362 RID: 866
    internal const int ChunkBytes = 3200;

    // Token: 0x04000363 RID: 867
    private static readonly byte[] ActivityStart = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.90A2AD5ED1088F3DC25A109F34E683DBB921E9D8315B3ABA2C15240DA316FB08), 38).ToArray();

    // Token: 0x04000364 RID: 868
    private static readonly byte[] ActivityEnd = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.10DDC5683E59C7896CF8A2CD26D449E862D81C247EC48A355256C5DCD81C042F), 36).ToArray();

    // Token: 0x04000365 RID: 869
    private readonly ClickyHttp http;

    // Token: 0x04000366 RID: 870
    private readonly string tokenPath;

    // Token: 0x04000367 RID: 871
    private readonly string area;

    // Token: 0x04000368 RID: 872
    [Nullable(new byte[] { 1, 1, 2 })]
    private readonly Func<bool, CancellationToken, Task<string>> accessToken;

    // Token: 0x04000369 RID: 873
    [Nullable(new byte[] { 1, 1, 2 })]
    private readonly Func<CancellationToken, Task<GeminiTranscriber.MintedSession>> mint;

    // Token: 0x0400036A RID: 874
    private readonly Func<string, CancellationToken, Task<WebSocket>> connect;

    // Token: 0x0400036B RID: 875
    private readonly CancellationTokenSource lifetime = new CancellationTokenSource();

    // Token: 0x0400036C RID: 876
    private readonly Channel<GeminiTranscriber.AudioFrame> outgoing = Channel.CreateBounded<GeminiTranscriber.AudioFrame>(new BoundedChannelOptions(2048)
    {
      FullMode = BoundedChannelFullMode.Wait,
      SingleReader = true,
      SingleWriter = false
    });

    // Token: 0x0400036D RID: 877
    private readonly object gate = new object();

    // Token: 0x0400036E RID: 878
    private readonly List<string> finals = new List<string>();

    // Token: 0x0400036F RID: 879
    private readonly TaskCompletionSource<bool> setupReady = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

    // Token: 0x04000370 RID: 880
    private readonly TaskCompletionSource<bool> opened = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

    // Token: 0x04000371 RID: 881
    [Nullable(new byte[] { 1, 2 })]
    private readonly TaskCompletionSource<string> delivered = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);

    // Token: 0x04000372 RID: 882
    private readonly TaskCompletionSource<bool> activityEndSent = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

    // Token: 0x04000373 RID: 883
    [Nullable(2)]
    private WebSocket socket;

    // Token: 0x04000374 RID: 884
    [Nullable(2)]
    private Task runTask;

    // Token: 0x04000375 RID: 885
    private string interim = "";

    // Token: 0x04000376 RID: 886
    private long bufferedBytes;

    // Token: 0x04000377 RID: 887
    private long sentBytes;

    // Token: 0x04000378 RID: 888
    private bool warming;

    // Token: 0x04000379 RID: 889
    private bool finishRequested;

    // Token: 0x0400037A RID: 890
    private bool finishSent;

    // Token: 0x0400037B RID: 891
    private bool disposed;

    // Token: 0x0400037C RID: 892
    private double loudestLevel;

    // Token: 0x0400037D RID: 893
    internal static readonly TimeSpan WarmKeepAliveInterval = TimeSpan.FromSeconds(20L);

    // Token: 0x0400037E RID: 894
    internal const double SilentPressLevel = 0.006;

    // Token: 0x0400037F RID: 895
    internal static readonly TimeSpan SilentPressWait = TimeSpan.FromSeconds(0.6);

    // Token: 0x04000380 RID: 896
    internal static readonly TimeSpan FinalTranscriptTimeout = TimeSpan.FromSeconds(2.8);

    // Token: 0x04000389 RID: 905
    private static readonly object AccountRefusalGate = new object();

    // Token: 0x0400038A RID: 906
    [TupleElementNames(new string[] { "Reason", "Until" })]
    [Nullable(new byte[] { 0, 1 })]
    private static ValueTuple<string, DateTimeOffset>? accountRefusal;

    // Token: 0x0400038B RID: 907
    internal static readonly TimeSpan AccountRefusalPause = TimeSpan.FromMinutes(10L);

    // Token: 0x02000326 RID: 806
    [Nullable(0)]
    internal sealed class MintedSession : IEquatable<GeminiTranscriber.MintedSession>
    {
      // Token: 0x060027D0 RID: 10192 RVA: 0x00414B2C File Offset: 0x00414B2C
      public MintedSession(string Token, JsonElement Setup, [Nullable(2)] string Receipt = null)
      {
        this.Token = Token;
        this.Setup = Setup;
        this.Receipt = Receipt;
        base..ctor();
      }

      // Token: 0x170007DC RID: 2012
      // (get) Token: 0x060027D1 RID: 10193 RVA: 0x00414B4C File Offset: 0x00414B4C
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(GeminiTranscriber.MintedSession);
        }
      }

      // Token: 0x170007DD RID: 2013
      // (get) Token: 0x060027D2 RID: 10194 RVA: 0x00414B58 File Offset: 0x00414B58
      // (set) Token: 0x060027D3 RID: 10195 RVA: 0x00414B60 File Offset: 0x00414B60
      public string Token { get; set; }

      // Token: 0x170007DE RID: 2014
      // (get) Token: 0x060027D4 RID: 10196 RVA: 0x00414B6C File Offset: 0x00414B6C
      // (set) Token: 0x060027D5 RID: 10197 RVA: 0x00414B74 File Offset: 0x00414B74
      public JsonElement Setup { get; set; }

      // Token: 0x170007DF RID: 2015
      // (get) Token: 0x060027D6 RID: 10198 RVA: 0x00414B80 File Offset: 0x00414B80
      // (set) Token: 0x060027D7 RID: 10199 RVA: 0x00414B88 File Offset: 0x00414B88
      [Nullable(2)]
      public string Receipt
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x060027D8 RID: 10200 RVA: 0x00414B94 File Offset: 0x00414B94
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("MintedSession");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060027D9 RID: 10201 RVA: 0x00414BE0 File Offset: 0x00414BE0
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Token = ");
        builder.Append(this.Token);
        builder.Append(", Setup = ");
        builder.Append(this.Setup.ToString());
        builder.Append(", Receipt = ");
        builder.Append(this.Receipt);
        return true;
      }

      // Token: 0x060027DA RID: 10202 RVA: 0x00414C4C File Offset: 0x00414C4C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(GeminiTranscriber.MintedSession left, GeminiTranscriber.MintedSession right)
      {
        return !(left == right);
      }

      // Token: 0x060027DB RID: 10203 RVA: 0x00414C58 File Offset: 0x00414C58
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(GeminiTranscriber.MintedSession left, GeminiTranscriber.MintedSession right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060027DC RID: 10204 RVA: 0x00414C6C File Offset: 0x00414C6C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Token>k__BackingField)) * -1521134295 + EqualityComparer<JsonElement>.Default.GetHashCode(this.<Setup>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Receipt>k__BackingField);
      }

      // Token: 0x060027DD RID: 10205 RVA: 0x00414CD0 File Offset: 0x00414CD0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as GeminiTranscriber.MintedSession);
      }

      // Token: 0x060027DE RID: 10206 RVA: 0x00414CE0 File Offset: 0x00414CE0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(GeminiTranscriber.MintedSession other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Token>k__BackingField, other.<Token>k__BackingField) && EqualityComparer<JsonElement>.Default.Equals(this.<Setup>k__BackingField, other.<Setup>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Receipt>k__BackingField, other.<Receipt>k__BackingField));
      }

      // Token: 0x060027E0 RID: 10208 RVA: 0x00414D5C File Offset: 0x00414D5C
      [CompilerGenerated]
      private MintedSession(GeminiTranscriber.MintedSession original)
      {
        this.Token = original.<Token>k__BackingField;
        this.Setup = original.<Setup>k__BackingField;
        this.Receipt = original.<Receipt>k__BackingField;
      }

      // Token: 0x060027E1 RID: 10209 RVA: 0x00414D88 File Offset: 0x00414D88
      [CompilerGenerated]
      public void Deconstruct(out string Token, out JsonElement Setup, [Nullable(2)] out string Receipt)
      {
        Token = this.Token;
        Setup = this.Setup;
        Receipt = this.Receipt;
      }
    }

    // Token: 0x02000327 RID: 807
    [NullableContext(2)]
    [Nullable(0)]
    private readonly struct AudioFrame : IEquatable<GeminiTranscriber.AudioFrame>
    {
      // Token: 0x060027E2 RID: 10210 RVA: 0x00414DA8 File Offset: 0x00414DA8
      public AudioFrame(byte[] Buffer, int Length, bool IsKeepAlive = false)
      {
        this.Buffer = Buffer;
        this.Length = Length;
        this.IsKeepAlive = IsKeepAlive;
      }

      // Token: 0x170007E0 RID: 2016
      // (get) Token: 0x060027E3 RID: 10211 RVA: 0x00414DC0 File Offset: 0x00414DC0
      // (set) Token: 0x060027E4 RID: 10212 RVA: 0x00414DC8 File Offset: 0x00414DC8
      public byte[] Buffer { get; set; }

      // Token: 0x170007E1 RID: 2017
      // (get) Token: 0x060027E5 RID: 10213 RVA: 0x00414DD4 File Offset: 0x00414DD4
      // (set) Token: 0x060027E6 RID: 10214 RVA: 0x00414DDC File Offset: 0x00414DDC
      public int Length { get; set; }

      // Token: 0x170007E2 RID: 2018
      // (get) Token: 0x060027E7 RID: 10215 RVA: 0x00414DE8 File Offset: 0x00414DE8
      // (set) Token: 0x060027E8 RID: 10216 RVA: 0x00414DF0 File Offset: 0x00414DF0
      public bool IsKeepAlive { get; set; }

      // Token: 0x060027E9 RID: 10217 RVA: 0x00414DFC File Offset: 0x00414DFC
      [NullableContext(0)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("AudioFrame");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060027EA RID: 10218 RVA: 0x00414E48 File Offset: 0x00414E48
      [NullableContext(0)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("Buffer = ");
        builder.Append(this.Buffer);
        builder.Append(", Length = ");
        builder.Append(this.Length.ToString());
        builder.Append(", IsKeepAlive = ");
        builder.Append(this.IsKeepAlive.ToString());
        return true;
      }

      // Token: 0x060027EB RID: 10219 RVA: 0x00414EC0 File Offset: 0x00414EC0
      [CompilerGenerated]
      public static bool operator !=(GeminiTranscriber.AudioFrame left, GeminiTranscriber.AudioFrame right)
      {
        return !(left == right);
      }

      // Token: 0x060027EC RID: 10220 RVA: 0x00414ECC File Offset: 0x00414ECC
      [CompilerGenerated]
      public static bool operator ==(GeminiTranscriber.AudioFrame left, GeminiTranscriber.AudioFrame right)
      {
        return left.Equals(right);
      }

      // Token: 0x060027ED RID: 10221 RVA: 0x00414ED8 File Offset: 0x00414ED8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<byte[]>.Default.GetHashCode(this.<Buffer>k__BackingField) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Length>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsKeepAlive>k__BackingField);
      }

      // Token: 0x060027EE RID: 10222 RVA: 0x00414F18 File Offset: 0x00414F18
      [NullableContext(0)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is GeminiTranscriber.AudioFrame && this.Equals((GeminiTranscriber.AudioFrame)obj);
      }

      // Token: 0x060027EF RID: 10223 RVA: 0x00414F30 File Offset: 0x00414F30
      [CompilerGenerated]
      public bool Equals(GeminiTranscriber.AudioFrame other)
      {
        return EqualityComparer<byte[]>.Default.Equals(this.<Buffer>k__BackingField, other.<Buffer>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Length>k__BackingField, other.<Length>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsKeepAlive>k__BackingField, other.<IsKeepAlive>k__BackingField);
      }

      // Token: 0x060027F0 RID: 10224 RVA: 0x00414F88 File Offset: 0x00414F88
      [CompilerGenerated]
      public void Deconstruct(out byte[] Buffer, out int Length, out bool IsKeepAlive)
      {
        Buffer = this.Buffer;
        Length = this.Length;
        IsKeepAlive = this.IsKeepAlive;
      }
    }
  }
}
