using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Brain;
using HeyClicky.Core.Net;
using HeyClicky.Core.Voice;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core.Dictation
{
  // Token: 0x020001CA RID: 458
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DictationTranscriptionClient
  {
    // Token: 0x170005DE RID: 1502
    // (get) Token: 0x06001A7A RID: 6778 RVA: 0x003D776C File Offset: 0x003D776C
    // (set) Token: 0x06001A7B RID: 6779 RVA: 0x003D7774 File Offset: 0x003D7774
    public DictationCleanupContext Context { get; set; } = new DictationCleanupContext("", null, "", "", "", false);

    // Token: 0x170005DF RID: 1503
    // (get) Token: 0x06001A7C RID: 6780 RVA: 0x003D7780 File Offset: 0x003D7780
    // (set) Token: 0x06001A7D RID: 6781 RVA: 0x003D7788 File Offset: 0x003D7788
    public IReadOnlyList<string> KnownTerms { get; set; } = Array.Empty<string>();

    // Token: 0x06001A7E RID: 6782 RVA: 0x003D7794 File Offset: 0x003D7794
    public DictationTranscriptionClient(ClickyHttp http, [Nullable(new byte[] { 1, 1, 2 })] Func<bool, CancellationToken, Task<string>> accessToken, string usageContext, string languageHint, int sampleRate)
    {
      this.http = http;
      this.accessToken = accessToken;
      this.usageContext = usageContext;
      this.languageHint = languageHint;
      this.sampleRate = sampleRate;
    }

    // Token: 0x06001A7F RID: 6783 RVA: 0x003D7814 File Offset: 0x003D7814
    public byte[] AccumulatedAudio()
    {
      object obj = this.gate;
      byte[] array;
      lock (obj)
      {
        array = this.pcm.AsSpan<byte>(0, this.pcmLength).ToArray();
      }
      return array;
    }

    // Token: 0x06001A80 RID: 6784 RVA: 0x003D786C File Offset: 0x003D786C
    [NullableContext(0)]
    public void AppendAudio(ReadOnlySpan<byte> pcm16)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (!this.cancelled && this.pcmLength + pcm16.Length <= 360 * this.sampleRate * 2)
        {
          if (this.pcmLength + pcm16.Length > this.pcm.Length)
          {
            Array.Resize<byte>(ref this.pcm, Math.Max(this.pcm.Length * 2, this.pcmLength + pcm16.Length));
          }
          pcm16.CopyTo(this.pcm.AsSpan<byte>(this.pcmLength));
          this.pcmLength += pcm16.Length;
        }
      }
    }

    // Token: 0x170005E0 RID: 1504
    // (get) Token: 0x06001A81 RID: 6785 RVA: 0x003D793C File Offset: 0x003D793C
    public double AccumulatedSeconds
    {
      get
      {
        object obj = this.gate;
        double num;
        lock (obj)
        {
          num = (double)this.pcmLength / 2.0 / (double)this.sampleRate;
        }
        return num;
      }
    }

    // Token: 0x06001A82 RID: 6786 RVA: 0x003D7994 File Offset: 0x003D7994
    public void Cancel()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.cancelled = true;
        this.pcm = Array.Empty<byte>();
        this.pcmLength = 0;
      }
    }

    // Token: 0x06001A83 RID: 6787 RVA: 0x003D79E8 File Offset: 0x003D79E8
    [return: TupleElementNames(new string[] { "Text", "AudioSeconds" })]
    [return: Nullable(new byte[] { 1, 0, 1 })]
    public async Task<ValueTuple<string, double>> TranscribeAsync(CancellationToken ct = default(CancellationToken))
    {
      object obj = this.gate;
      double seconds;
      byte[] array;
      lock (obj)
      {
        seconds = (double)this.pcmLength / 2.0 / (double)this.sampleRate;
        if (seconds < 0.25)
        {
          return new ValueTuple<string, double>("", 0.0);
        }
        array = Pcm16.WavFromPcm16Mono(this.pcm.AsSpan<byte>(0, this.pcmLength), this.sampleRate);
      }
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["audio_wav_base64"] = Convert.ToBase64String(array);
      jsonObject["audio_seconds"] = seconds;
      jsonObject["context"] = this.ContextPayload();
      JsonObject jsonObject2 = jsonObject;
      string text = this.languageHint.Trim();
      if (text.Length > 0 && text != "multi")
      {
        jsonObject2["language"] = text;
      }
      ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.PostExpectingTextAsync("/v2/dictation/transcribe", jsonObject2, null, TimeSpan.FromSeconds(60L), ct).ConfigureAwait(false).GetAwaiter();
      if (!configuredTaskAwaiter.IsCompleted)
      {
        await configuredTaskAwaiter;
        ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
        configuredTaskAwaiter = configuredTaskAwaiter2;
        configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
      }
      return new ValueTuple<string, double>(configuredTaskAwaiter.GetResult(), seconds);
    }

    // Token: 0x06001A84 RID: 6788 RVA: 0x003D7A34 File Offset: 0x003D7A34
    public Task<string> CleanUpAsync(string transcript, double audioSeconds, [Nullable(2)] string receipt, bool useFastCleanupLane, int? sttMilliseconds, [Nullable(2)] string sttProvider = null, CancellationToken ct = default(CancellationToken))
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["transcript"] = transcript;
      jsonObject["audio_seconds"] = audioSeconds;
      jsonObject["context"] = this.ContextPayload();
      JsonObject jsonObject2 = jsonObject;
      if (useFastCleanupLane)
      {
        jsonObject2["fast_cleanup"] = true;
      }
      if (sttMilliseconds != null && sttMilliseconds.GetValueOrDefault() >= 0)
      {
        jsonObject2["stt_ms"] = sttMilliseconds.Value;
      }
      if (!string.IsNullOrEmpty(sttProvider))
      {
        jsonObject2["stt_provider"] = sttProvider;
      }
      return this.PostExpectingTextAsync("/v2/dictation/cleanup", jsonObject2, receipt, TimeSpan.FromSeconds(20L), ct);
    }

    // Token: 0x06001A85 RID: 6789 RVA: 0x003D7AF4 File Offset: 0x003D7AF4
    private JsonObject ContextPayload()
    {
      JsonObject jsonObject = new JsonObject(null);
      if (this.Context.AppName.Length > 0)
      {
        jsonObject["app_name"] = this.Context.AppName;
      }
      if (this.Context.BeforeText.Length > 0)
      {
        jsonObject["before_text"] = this.Context.BeforeText;
      }
      if (this.Context.SelectedText.Length > 0)
      {
        jsonObject["selected_text"] = this.Context.SelectedText;
      }
      if (this.Context.AfterText.Length > 0)
      {
        jsonObject["after_text"] = this.Context.AfterText;
      }
      jsonObject["single_line"] = this.Context.SingleLine;
      if (!string.IsNullOrEmpty(this.Context.SiteHost))
      {
        jsonObject["site_host"] = this.Context.SiteHost;
      }
      if (this.KnownTerms.Count > 0)
      {
        JsonArray jsonArray = new JsonArray(null);
        foreach (string text in this.KnownTerms.Take<string>(50))
        {
          jsonArray.Add<string>(text);
        }
        jsonObject["known_terms"] = jsonArray;
      }
      return jsonObject;
    }

    // Token: 0x06001A86 RID: 6790 RVA: 0x003D7C84 File Offset: 0x003D7C84
    private async Task<string> PostExpectingTextAsync(string path, JsonObject body, [Nullable(2)] string receipt, TimeSpan timeout, CancellationToken ct)
    {
      byte[] bytes = JsonSerializer.SerializeToUtf8Bytes<JsonObject>(body, JsonNodeContext.Default.JsonObject);
      object obj = await this.SendAsync(path, bytes, receipt, false, timeout, ct).ConfigureAwait(false);
      int num = obj.Item1;
      byte[] array = obj.Item2;
      if (num == 401)
      {
        Log.Info("Dictation", (receipt == null) ? (path + ": 401, retrying with a refreshed token") : (path + ": receipt rejected (expired?), retrying with the bearer"));
        object obj2 = await this.SendAsync(path, bytes, null, receipt == null, timeout, ct).ConfigureAwait(false);
        num = obj2.Item1;
        array = obj2.Item2;
      }
      if (num == 402)
      {
        BillingPaywallDetector.HandleIfPaywall(402, array, true);
        throw new DictationPaywallReachedException();
      }
      if (num < 200 || num > 299)
      {
        throw new WorkerHttpException(num, Encoding.UTF8.GetString(array), path);
      }
      JsonObject jsonObject = JsonNode.Parse(array, null, default(JsonDocumentOptions)) as JsonObject;
      JsonValue jsonValue = ((jsonObject != null) ? jsonObject["text"] : null) as JsonValue;
      string text;
      if (jsonValue == null || !jsonValue.TryGetValue<string>(out text))
      {
        throw new InvalidOperationException("Invalid dictation response.");
      }
      return text.Trim();
    }

    // Token: 0x06001A87 RID: 6791 RVA: 0x003D7CF4 File Offset: 0x003D7CF4
    [return: TupleElementNames(new string[] { "Status", "Body" })]
    [return: Nullable(new byte[] { 1, 0, 1 })]
    private async Task<ValueTuple<int, byte[]>> SendAsync(string path, byte[] body, [Nullable(2)] string receipt, bool forceRefresh, TimeSpan timeout, CancellationToken ct)
    {
      ValueTuple<int, byte[]> valueTuple;
      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, this.http.WorkerUrl(path)))
      {
        if (!string.IsNullOrEmpty(receipt))
        {
          request.Headers.TryAddWithoutValidation("X-Clicky-Dictation-Receipt", receipt);
        }
        else
        {
          string text = await this.accessToken(forceRefresh, ct).ConfigureAwait(false);
          if (string.IsNullOrEmpty(text))
          {
            throw new InvalidOperationException("Please sign in to use dictation.");
          }
          request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", text);
        }
        request.Headers.TryAddWithoutValidation("X-Clicky-Usage-Context", this.usageContext);
        request.Content = new ByteArrayContent(body);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        using (CancellationTokenSource timeoutSource = CancellationTokenSource.CreateLinkedTokenSource(ct))
        {
          timeoutSource.CancelAfter(timeout);
          using (HttpResponseMessage response = await this.http.Client.SendAsync(request, timeoutSource.Token).ConfigureAwait(false))
          {
            byte[] array = await response.Content.ReadAsByteArrayAsync(timeoutSource.Token).ConfigureAwait(false);
            valueTuple = new ValueTuple<int, byte[]>((int)response.StatusCode, array);
          }
        }
      }
      return valueTuple;
    }

    // Token: 0x04000B5C RID: 2908
    private const string Area = "Dictation";

    // Token: 0x04000B5D RID: 2909
    private const int MaxAccumulatedSeconds = 360;

    // Token: 0x04000B5E RID: 2910
    private readonly ClickyHttp http;

    // Token: 0x04000B5F RID: 2911
    [Nullable(new byte[] { 1, 1, 2 })]
    private readonly Func<bool, CancellationToken, Task<string>> accessToken;

    // Token: 0x04000B60 RID: 2912
    private readonly string usageContext;

    // Token: 0x04000B61 RID: 2913
    private readonly string languageHint;

    // Token: 0x04000B62 RID: 2914
    private readonly int sampleRate;

    // Token: 0x04000B63 RID: 2915
    private readonly object gate = new object();

    // Token: 0x04000B64 RID: 2916
    private byte[] pcm = new byte[65536];

    // Token: 0x04000B65 RID: 2917
    private int pcmLength;

    // Token: 0x04000B66 RID: 2918
    private bool cancelled;
  }
}
