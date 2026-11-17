using System;
using System.Buffers;
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
using System.Threading.Channels;
using System.Threading.Tasks;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Net;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000070 RID: 112
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DeepgramTranscriber : IAsyncDisposable
  {
    // Token: 0x1700018C RID: 396
    // (get) Token: 0x060006DF RID: 1759 RVA: 0x003600B0 File Offset: 0x003600B0
    public string LanguageCode { get; }

    // Token: 0x1700018D RID: 397
    // (get) Token: 0x060006E0 RID: 1760 RVA: 0x003600B8 File Offset: 0x003600B8
    public IReadOnlyList<string> Keyterms { get; }

    // Token: 0x1700018E RID: 398
    // (get) Token: 0x060006E1 RID: 1761 RVA: 0x003600C0 File Offset: 0x003600C0
    public string UsageContextValue { get; }

    // Token: 0x1700018F RID: 399
    // (get) Token: 0x060006E2 RID: 1762 RVA: 0x003600C8 File Offset: 0x003600C8
    public int SampleRate { get; }

    // Token: 0x17000190 RID: 400
    // (get) Token: 0x060006E3 RID: 1763 RVA: 0x003600D0 File Offset: 0x003600D0
    [Nullable(2)]
    public string Purpose
    {
      [NullableContext(2)]
      get;
    }

    // Token: 0x17000191 RID: 401
    // (get) Token: 0x060006E4 RID: 1764 RVA: 0x003600D8 File Offset: 0x003600D8
    // (set) Token: 0x060006E5 RID: 1765 RVA: 0x003600E0 File Offset: 0x003600E0
    [Nullable(2)]
    public string Receipt
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x17000192 RID: 402
    // (get) Token: 0x060006E6 RID: 1766 RVA: 0x003600EC File Offset: 0x003600EC
    public bool DidFail
    {
      get
      {
        return this.failed;
      }
    }

    // Token: 0x17000193 RID: 403
    // (get) Token: 0x060006E7 RID: 1767 RVA: 0x003600F8 File Offset: 0x003600F8
    [Nullable(2)]
    public Exception Failure
    {
      [NullableContext(2)]
      get
      {
        return this.failure;
      }
    }

    // Token: 0x17000194 RID: 404
    // (get) Token: 0x060006E8 RID: 1768 RVA: 0x00360100 File Offset: 0x00360100
    // (set) Token: 0x060006E9 RID: 1769 RVA: 0x00360108 File Offset: 0x00360108
    public bool PaywallReached { get; private set; }

    // Token: 0x17000195 RID: 405
    // (get) Token: 0x060006EA RID: 1770 RVA: 0x00360114 File Offset: 0x00360114
    public double SentAudioSeconds
    {
      get
      {
        return (double)Interlocked.Read(ref this.sentPcmBytes) / 2.0 / (double)this.SampleRate;
      }
    }

    // Token: 0x17000196 RID: 406
    // (get) Token: 0x060006EB RID: 1771 RVA: 0x00360134 File Offset: 0x00360134
    public Task<bool> Opened
    {
      get
      {
        return this.opened.Task;
      }
    }

    // Token: 0x17000197 RID: 407
    // (get) Token: 0x060006EC RID: 1772 RVA: 0x00360144 File Offset: 0x00360144
    public bool IsAlive
    {
      get
      {
        ClientWebSocket clientWebSocket = this.socket;
        return clientWebSocket != null && clientWebSocket.State == WebSocketState.Open && !this.failed;
      }
    }

    // Token: 0x17000198 RID: 408
    // (get) Token: 0x060006ED RID: 1773 RVA: 0x0036016C File Offset: 0x0036016C
    // (set) Token: 0x060006EE RID: 1774 RVA: 0x00360174 File Offset: 0x00360174
    public long OpenedAtTicks { get; private set; }

    // Token: 0x060006EF RID: 1775 RVA: 0x00360180 File Offset: 0x00360180
    public DeepgramTranscriber(ClickyHttp http, [Nullable(new byte[] { 1, 1, 2 })] Func<bool, CancellationToken, Task<string>> accessToken, string languageCode, IReadOnlyList<string> keyterms, string usageContext, int sampleRate, [Nullable(2)] string purpose = "one_brain")
    {
      this.Purpose = purpose;
      this.http = http;
      this.accessToken = accessToken;
      this.LanguageCode = languageCode;
      this.Keyterms = keyterms;
      this.UsageContextValue = usageContext;
      this.SampleRate = sampleRate;
    }

    // Token: 0x060006F0 RID: 1776 RVA: 0x00360238 File Offset: 0x00360238
    public void Start(bool asWarmSocket = false)
    {
      this.isWarmUnadopted = asWarmSocket;
      CancellationToken ct;
      try
      {
        ct = this.lifetime.Token;
      }
      catch (ObjectDisposedException)
      {
        return;
      }
      Task.Run(() => this.RunAsync(ct));
    }

    // Token: 0x060006F1 RID: 1777 RVA: 0x00360294 File Offset: 0x00360294
    public bool Adopt()
    {
      if (!this.failed && !this.PaywallReached)
      {
        if (this.opened.Task.IsCompleted)
        {
          ClientWebSocket clientWebSocket = this.socket;
          if (clientWebSocket == null || clientWebSocket.State != WebSocketState.Open)
          {
            return false;
          }
        }
        this.isWarmUnadopted = false;
        return true;
      }
      return false;
    }

    // Token: 0x060006F2 RID: 1778 RVA: 0x003602EC File Offset: 0x003602EC
    [NullableContext(0)]
    public void PushAudio(ReadOnlySpan<byte> pcm16)
    {
      if (pcm16.IsEmpty || this.closeStreamSent || this.failed)
      {
        return;
      }
      byte[] array = ArrayPool<byte>.Shared.Rent(pcm16.Length);
      pcm16.CopyTo(array);
      if (!this.outgoing.Writer.TryWrite(new DeepgramTranscriber.OutgoingFrame(array, pcm16.Length, false, true)))
      {
        ArrayPool<byte>.Shared.Return(array, false);
      }
    }

    // Token: 0x060006F3 RID: 1779 RVA: 0x00360364 File Offset: 0x00360364
    public string PartialTranscriptSoFar()
    {
      object obj = this.gate;
      string text2;
      lock (obj)
      {
        string text = string.Join(" ", this.finalSegments);
        text2 = (string.IsNullOrEmpty(this.latestInterim) ? text : ((text.Length == 0) ? this.latestInterim : (text + " " + this.latestInterim)));
      }
      return text2;
    }

    // Token: 0x060006F4 RID: 1780 RVA: 0x003603E4 File Offset: 0x003603E4
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<string> FinishAsync(TimeSpan? timeout = null)
    {
      ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter;
      ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
      if (!this.opened.Task.IsCompleted)
      {
        configuredTaskAwaiter = Task.WhenAny(this.opened.Task, Task.Delay(1500)).ConfigureAwait(false).GetAwaiter();
        if (!configuredTaskAwaiter.IsCompleted)
        {
          await configuredTaskAwaiter;
          configuredTaskAwaiter = configuredTaskAwaiter2;
          configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
        }
        if (configuredTaskAwaiter.GetResult() != this.opened.Task)
        {
          this.MarkFailed(new TimeoutException("the Deepgram socket did not open in time"), false);
        }
      }
      if (!this.failed && !this.closeStreamSent)
      {
        this.closeStreamSent = true;
        this.outgoing.Writer.TryWrite(new DeepgramTranscriber.OutgoingFrame(DeepgramTranscriber.CloseStreamMessage, DeepgramTranscriber.CloseStreamMessage.Length, true, false));
      }
      configuredTaskAwaiter = Task.WhenAny(this.delivered.Task, Task.Delay(timeout ?? TimeSpan.FromSeconds(3.5))).ConfigureAwait(false).GetAwaiter();
      if (!configuredTaskAwaiter.IsCompleted)
      {
        await configuredTaskAwaiter;
        configuredTaskAwaiter = configuredTaskAwaiter2;
        configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
      }
      if (configuredTaskAwaiter.GetResult() != this.delivered.Task)
      {
        string text = this.PartialTranscriptSoFar().Trim();
        this.delivered.TrySetResult((text.Length > 0) ? text : null);
      }
      return await this.delivered.Task.ConfigureAwait(false);
    }

    // Token: 0x060006F5 RID: 1781 RVA: 0x00360430 File Offset: 0x00360430
    private async Task RunAsync(CancellationToken ct)
    {
      if (!ct.IsCancellationRequested)
      {
        try
        {
          ValueTuple<string, string>? valueTuple = await this.MintAsync(ct).ConfigureAwait(false);
          if (valueTuple != null)
          {
            Uri uri = DeepgramTranscriber.BuildListenUrl(valueTuple.Value.Item2, this.SampleRate, this.LanguageCode, this.Keyterms);
            ClientWebSocket ws = new ClientWebSocket();
            ws.Options.SetRequestHeader("Authorization", "Bearer " + valueTuple.Value.Item1);
            ws.Options.KeepAliveInterval = TimeSpan.FromSeconds(20L);
            this.socket = ws;
            using (CancellationTokenSource connectTimeout = CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
              connectTimeout.CancelAfter(TimeSpan.FromSeconds(10L));
              await ws.ConnectAsync(uri, this.http.Invoker, connectTimeout.Token).ConfigureAwait(false);
            }
            CancellationTokenSource connectTimeout = null;
            this.OpenedAtTicks = Environment.TickCount64;
            this.opened.TrySetResult(true);
            Task sender = this.SendLoopAsync(ws, ct);
            Task keepAlive = this.KeepAliveLoopAsync(ct);
            await this.ReceiveLoopAsync(ws, ct).ConfigureAwait(false);
            await Task.WhenAny(sender, Task.Delay(100)).ConfigureAwait(false);
            ws = null;
            sender = null;
          }
        }
        catch (OperationCanceledException obj) when (ct.IsCancellationRequested)
        {
          this.delivered.TrySetResult(null);
        }
        catch (Exception ex)
        {
          this.MarkFailed(ex, false);
        }
        finally
        {
          this.opened.TrySetResult(false);
        }
      }
    }

    // Token: 0x060006F6 RID: 1782 RVA: 0x0036047C File Offset: 0x0036047C
    [return: TupleElementNames(new string[] { "Token", "Model" })]
    [return: Nullable(new byte[] { 1, 0, 1, 1 })]
    private async Task<ValueTuple<string, string>?> MintAsync(CancellationToken ct)
    {
      int attempt = 0;
      while (attempt < 2)
      {
        string text = await this.accessToken(attempt > 0, ct).ConfigureAwait(false);
        if (text != null)
        {
          using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, this.http.WorkerUrl((this.Purpose == null) ? "/v2/dictation/deepgram-token" : ("/v2/dictation/deepgram-token?purpose=" + Uri.EscapeDataString(this.Purpose)))))
          {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", text);
            request.Headers.TryAddWithoutValidation("X-Clicky-Usage-Context", this.UsageContextValue);
            request.Content = new ByteArrayContent(Array.Empty<byte>());
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            using (CancellationTokenSource timeout = CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
              timeout.CancelAfter(TimeSpan.FromSeconds(10L));
              using (HttpResponseMessage response = await this.http.Client.SendAsync(request, timeout.Token).ConfigureAwait(false))
              {
                byte[] array = await response.Content.ReadAsByteArrayAsync(timeout.Token).ConfigureAwait(false);
                int statusCode = (int)response.StatusCode;
                if (statusCode != 401 || attempt != 0)
                {
                  if (statusCode == 402)
                  {
                    this.PaywallReached = true;
                    BillingPaywallDetector.HandleIfPaywall(statusCode, array, !this.isWarmUnadopted);
                    this.MarkFailed(new InvalidOperationException("paywall"), true);
                    return null;
                  }
                  if (statusCode != 200)
                  {
                    this.MarkFailed(new WorkerHttpException(statusCode, Encoding.UTF8.GetString(array), "/v2/dictation/deepgram-token"), false);
                    return null;
                  }
                  DeepgramTokenResponse deepgramTokenResponse = JsonSerializer.Deserialize<DeepgramTokenResponse>(array, CoreJsonContext.Default.DeepgramTokenResponse);
                  if (string.IsNullOrEmpty((deepgramTokenResponse != null) ? deepgramTokenResponse.AccessToken : null))
                  {
                    this.MarkFailed(new InvalidOperationException("the Deepgram token mint returned no access_token"), false);
                    return null;
                  }
                  this.Receipt = (string.IsNullOrEmpty(deepgramTokenResponse.Receipt) ? null : deepgramTokenResponse.Receipt);
                  return new ValueTuple<string, string>?(new ValueTuple<string, string>(deepgramTokenResponse.AccessToken, string.IsNullOrEmpty(deepgramTokenResponse.Model) ? "nova-3" : deepgramTokenResponse.Model));
                }
              }
            }
          }
          attempt++;
          continue;
        }
        this.MarkFailed(new InvalidOperationException("not signed in"), false);
        return null;
      }
      return null;
    }

    // Token: 0x060006F7 RID: 1783 RVA: 0x003604C8 File Offset: 0x003604C8
    public static Uri BuildListenUrl(string model, int sampleRate, string languageCode, IReadOnlyList<string> keyterms)
    {
      StringBuilder stringBuilder = new StringBuilder("wss://api.deepgram.com/v1/listen?");
      stringBuilder.Append("model=").Append(Uri.EscapeDataString(model));
      stringBuilder.Append("&encoding=linear16");
      stringBuilder.Append("&sample_rate=").Append(sampleRate);
      stringBuilder.Append("&channels=1");
      stringBuilder.Append("&smart_format=true");
      stringBuilder.Append("&interim_results=true");
      if (!string.IsNullOrEmpty(languageCode))
      {
        stringBuilder.Append("&language=").Append(Uri.EscapeDataString(languageCode));
      }
      if (string.IsNullOrEmpty(languageCode) || languageCode.StartsWith("en", StringComparison.OrdinalIgnoreCase))
      {
        stringBuilder.Append("&filler_words=true");
      }
      foreach (string text in keyterms)
      {
        stringBuilder.Append("&keyterm=").Append(Uri.EscapeDataString(text));
      }
      return new Uri(stringBuilder.ToString());
    }

    // Token: 0x060006F8 RID: 1784 RVA: 0x003605D0 File Offset: 0x003605D0
    private async Task SendLoopAsync(ClientWebSocket ws, CancellationToken ct)
    {
      try
      {
        ConfiguredCancelableAsyncEnumerable<DeepgramTranscriber.OutgoingFrame>.Enumerator enumerator = this.outgoing.Reader.ReadAllAsync(ct).ConfigureAwait<DeepgramTranscriber.OutgoingFrame>(false).GetAsyncEnumerator();
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
            DeepgramTranscriber.OutgoingFrame frame = enumerator.Current;
            try
            {
              if (ws.State != WebSocketState.Open)
              {
                continue;
              }
              await ws.SendAsync(frame.Buffer.AsMemory<byte>(0, frame.Length), frame.IsText ? WebSocketMessageType.Text : WebSocketMessageType.Binary, true, ct).ConfigureAwait(false);
              if (!frame.IsText)
              {
                Interlocked.Add(ref this.sentPcmBytes, (long)frame.Length);
              }
            }
            finally
            {
              if (frame.Pooled)
              {
                ArrayPool<byte>.Shared.Return(frame.Buffer, false);
              }
            }
            frame = default(DeepgramTranscriber.OutgoingFrame);
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
        enumerator = default(ConfiguredCancelableAsyncEnumerable<DeepgramTranscriber.OutgoingFrame>.Enumerator);
      }
      catch (Exception ex2) when (ex2 is OperationCanceledException || ex2 is WebSocketException || ex2 is ObjectDisposedException)
      {
        if (!ct.IsCancellationRequested && !this.delivered.Task.IsCompleted)
        {
          this.MarkFailed(ex2, false);
        }
      }
    }

    // Token: 0x060006F9 RID: 1785 RVA: 0x00360624 File Offset: 0x00360624
    private async Task KeepAliveLoopAsync(CancellationToken ct)
    {
      try
      {
        while (!ct.IsCancellationRequested)
        {
          await Task.Delay(TimeSpan.FromSeconds(7L), ct).ConfigureAwait(false);
          if (!this.isWarmUnadopted)
          {
            break;
          }
          this.outgoing.Writer.TryWrite(new DeepgramTranscriber.OutgoingFrame(DeepgramTranscriber.KeepAliveMessage, DeepgramTranscriber.KeepAliveMessage.Length, true, false));
        }
      }
      catch (OperationCanceledException)
      {
      }
    }

    // Token: 0x060006FA RID: 1786 RVA: 0x00360670 File Offset: 0x00360670
    private async Task ReceiveLoopAsync(ClientWebSocket ws, CancellationToken ct)
    {
      byte[] buffer = ArrayPool<byte>.Shared.Rent(65536);
      int length = 0;
      try
      {
        while (ws.State == WebSocketState.Open && !ct.IsCancellationRequested)
        {
          ValueWebSocketReceiveResult valueWebSocketReceiveResult = await ws.ReceiveAsync(buffer.AsMemory<byte>(length), ct).ConfigureAwait(false);
          if (valueWebSocketReceiveResult.MessageType == WebSocketMessageType.Close)
          {
            this.delivered.TrySetResult(this.FinalTranscript());
            break;
          }
          length += valueWebSocketReceiveResult.Count;
          if (!valueWebSocketReceiveResult.EndOfMessage)
          {
            if (length == buffer.Length)
            {
              byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length * 2);
              buffer.AsSpan<byte>(0, length).CopyTo(array);
              ArrayPool<byte>.Shared.Return(buffer, false);
              buffer = array;
            }
          }
          else
          {
            if (valueWebSocketReceiveResult.MessageType == WebSocketMessageType.Text)
            {
              this.HandleMessage(buffer.AsSpan<byte>(0, length));
            }
            length = 0;
          }
        }
      }
      catch (Exception ex) when (ex is OperationCanceledException || ex is WebSocketException || ex is ObjectDisposedException)
      {
        if (!ct.IsCancellationRequested && !this.delivered.Task.IsCompleted)
        {
          this.MarkFailed(ex, false);
        }
      }
      finally
      {
        ArrayPool<byte>.Shared.Return(buffer, false);
      }
    }

    // Token: 0x060006FB RID: 1787 RVA: 0x003606C4 File Offset: 0x003606C4
    [NullableContext(0)]
    internal void HandleMessage(ReadOnlySpan<byte> json)
    {
      using (JsonDocument jsonDocument = JsonDocument.Parse(json.ToArray(), default(JsonDocumentOptions)))
      {
        JsonElement rootElement = jsonDocument.RootElement;
        JsonElement jsonElement;
        string text = ((rootElement.TryGetProperty("type", out jsonElement) && jsonElement.ValueKind == JsonValueKind.String) ? jsonElement.GetString() : null);
        if (!(text == "Results"))
        {
          if (!(text == "Metadata"))
          {
            if (!(text == "Error"))
            {
              return;
            }
            JsonElement jsonElement2;
            string text2;
            if ((text2 = (rootElement.TryGetProperty("description", out jsonElement2) ? jsonElement2.GetString() : null)) == null)
            {
              JsonElement jsonElement3;
              text2 = (rootElement.TryGetProperty("message", out jsonElement3) ? jsonElement3.GetString() : null) ?? "Deepgram error";
            }
            string text3 = text2;
            this.MarkFailed(new IOException(text3), false);
            goto IL_01ED;
          }
        }
        else
        {
          string text4 = "";
          JsonElement jsonElement4;
          JsonElement jsonElement5;
          JsonElement jsonElement6;
          if (rootElement.TryGetProperty("channel", out jsonElement4) && jsonElement4.TryGetProperty("alternatives", out jsonElement5) && jsonElement5.ValueKind == JsonValueKind.Array && jsonElement5.GetArrayLength() > 0 && jsonElement5[0].TryGetProperty("transcript", out jsonElement6) && jsonElement6.ValueKind == JsonValueKind.String)
          {
            text4 = jsonElement6.GetString() ?? "";
          }
          JsonElement jsonElement7;
          bool flag = rootElement.TryGetProperty("is_final", out jsonElement7) && jsonElement7.ValueKind == JsonValueKind.True;
          object obj = this.gate;
          lock (obj)
          {
            if (flag)
            {
              if (text4.Trim().Length > 0)
              {
                this.finalSegments.Add(text4.Trim());
              }
              this.latestInterim = "";
              return;
            }
            if (text4.Trim().Length > 0)
            {
              this.latestInterim = text4.Trim();
            }
            return;
          }
        }
        if (this.closeStreamSent)
        {
          this.delivered.TrySetResult(this.FinalTranscript());
        }
        IL_01ED:;
      }
    }

    // Token: 0x060006FC RID: 1788 RVA: 0x00360900 File Offset: 0x00360900
    [NullableContext(2)]
    private string FinalTranscript()
    {
      object obj = this.gate;
      string text2;
      lock (obj)
      {
        string text = string.Join(" ", this.finalSegments).Trim();
        text2 = ((text.Length == 0) ? null : text);
      }
      return text2;
    }

    // Token: 0x060006FD RID: 1789 RVA: 0x00360960 File Offset: 0x00360960
    private void MarkFailed(Exception ex, bool isPaywall = false)
    {
      if (this.delivered.Task.IsCompleted)
      {
        return;
      }
      if (!isPaywall)
      {
        Log.Warn("Ears", "Deepgram failed: " + ex.Message);
      }
      this.failure = ex;
      this.failed = true;
      this.opened.TrySetResult(false);
      this.delivered.TrySetResult(null);
    }

    // Token: 0x060006FE RID: 1790 RVA: 0x003609C8 File Offset: 0x003609C8
    public async ValueTask DisposeAsync()
    {
      this.lifetime.Cancel();
      this.outgoing.Writer.TryComplete(null);
      this.delivered.TrySetResult(null);
      ClientWebSocket ws = this.socket;
      if (ws != null)
      {
        try
        {
          if (ws.State == WebSocketState.Open)
          {
            using (CancellationTokenSource cts = new CancellationTokenSource(500))
            {
              await ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, cts.Token).ConfigureAwait(false);
            }
            CancellationTokenSource cts = null;
          }
        }
        catch
        {
        }
        ws.Dispose();
      }
      this.lifetime.Dispose();
    }

    // Token: 0x04000325 RID: 805
    private const string Area = "Ears";

    // Token: 0x04000326 RID: 806
    private const int MaximumBufferedChunks = 2048;

    // Token: 0x04000327 RID: 807
    private static readonly byte[] KeepAliveMessage = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.78E211A62B53D9C0B74D1F4791778360D74E94DD8087DBC5835EE2987399C982), 20).ToArray();

    // Token: 0x04000328 RID: 808
    private static readonly byte[] CloseStreamMessage = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.5640DD0587C8BF07FDFA18F06914FA3CEB02ABDC51CBF7527B9B38CACB429B0C), 22).ToArray();

    // Token: 0x04000329 RID: 809
    private readonly ClickyHttp http;

    // Token: 0x0400032A RID: 810
    [Nullable(new byte[] { 1, 1, 2 })]
    private readonly Func<bool, CancellationToken, Task<string>> accessToken;

    // Token: 0x0400032B RID: 811
    private readonly CancellationTokenSource lifetime = new CancellationTokenSource();

    // Token: 0x0400032C RID: 812
    private readonly Channel<DeepgramTranscriber.OutgoingFrame> outgoing = Channel.CreateBounded<DeepgramTranscriber.OutgoingFrame>(new BoundedChannelOptions(2048)
    {
      FullMode = BoundedChannelFullMode.DropOldest,
      SingleReader = true,
      SingleWriter = false
    });

    // Token: 0x0400032D RID: 813
    private readonly List<string> finalSegments = new List<string>();

    // Token: 0x0400032E RID: 814
    private readonly object gate = new object();

    // Token: 0x0400032F RID: 815
    [Nullable(new byte[] { 1, 2 })]
    private readonly TaskCompletionSource<string> delivered = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);

    // Token: 0x04000330 RID: 816
    private readonly TaskCompletionSource<bool> opened = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

    // Token: 0x04000331 RID: 817
    [Nullable(2)]
    private ClientWebSocket socket;

    // Token: 0x04000332 RID: 818
    private string latestInterim = "";

    // Token: 0x04000333 RID: 819
    private long sentPcmBytes;

    // Token: 0x04000334 RID: 820
    private bool isWarmUnadopted;

    // Token: 0x04000335 RID: 821
    private bool closeStreamSent;

    // Token: 0x04000336 RID: 822
    private volatile bool failed;

    // Token: 0x04000337 RID: 823
    [Nullable(2)]
    private Exception failure;

    // Token: 0x02000316 RID: 790
    [Nullable(0)]
    private readonly struct OutgoingFrame
    {
      // Token: 0x060027AB RID: 10155 RVA: 0x00413444 File Offset: 0x00413444
      public OutgoingFrame(byte[] buffer, int length, bool isText, bool pooled = true)
      {
        this.Buffer = buffer;
        this.Length = length;
        this.IsText = isText;
        this.Pooled = pooled;
      }

      // Token: 0x170007D8 RID: 2008
      // (get) Token: 0x060027AC RID: 10156 RVA: 0x00413464 File Offset: 0x00413464
      public byte[] Buffer { get; }

      // Token: 0x170007D9 RID: 2009
      // (get) Token: 0x060027AD RID: 10157 RVA: 0x0041346C File Offset: 0x0041346C
      public int Length { get; }

      // Token: 0x170007DA RID: 2010
      // (get) Token: 0x060027AE RID: 10158 RVA: 0x00413474 File Offset: 0x00413474
      public bool IsText { get; }

      // Token: 0x170007DB RID: 2011
      // (get) Token: 0x060027AF RID: 10159 RVA: 0x0041347C File Offset: 0x0041347C
      public bool Pooled { get; }
    }
  }
}
