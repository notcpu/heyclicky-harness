using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Core.Telemetry
{
  // Token: 0x020000AD RID: 173
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PostHogAnalyticsSink : IAnalyticsSink, IDisposable
  {
    // Token: 0x17000247 RID: 583
    // (get) Token: 0x06000A7C RID: 2684 RVA: 0x00374BF0 File Offset: 0x00374BF0
    public string Name
    {
      get
      {
        return "posthog";
      }
    }

    // Token: 0x06000A7D RID: 2685 RVA: 0x00374BF8 File Offset: 0x00374BF8
    public PostHogAnalyticsSink(HttpClient http, string host, string apiKey)
    {
      this.http = http;
      this.apiKey = apiKey;
      this.batchUrl = new Uri(host.TrimEnd('/') + "/batch/");
      this.timer = new Timer(delegate(object _)
      {
        _ = this.FlushAsync();
      }, null, TimeSpan.FromSeconds(30L), TimeSpan.FromSeconds(30L));
    }

    // Token: 0x06000A7E RID: 2686 RVA: 0x00374C84 File Offset: 0x00374C84
    public void Enqueue(AnalyticsEvent analyticsEvent)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        this.queue.Add(analyticsEvent);
        if (this.queue.Count > 1000)
        {
          this.queue.RemoveRange(0, this.queue.Count - 1000);
        }
        flag2 = this.queue.Count >= 20;
      }
      if (flag2)
      {
        this.FlushAsync();
      }
    }

    // Token: 0x06000A7F RID: 2687 RVA: 0x00374D18 File Offset: 0x00374D18
    public async Task FlushAsync()
    {
      ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.sending.WaitAsync(0).ConfigureAwait(false).GetAwaiter();
      if (!configuredTaskAwaiter.IsCompleted)
      {
        await configuredTaskAwaiter;
        ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
        configuredTaskAwaiter = configuredTaskAwaiter2;
        configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
      }
      if (configuredTaskAwaiter.GetResult())
      {
        try
        {
          object obj = this.gate;
          AnalyticsEvent[] batch;
          lock (obj)
          {
            if (this.queue.Count == 0)
            {
              return;
            }
            batch = this.queue.ToArray();
            this.queue.Clear();
          }
          JsonArray jsonArray = new JsonArray(null);
          AnalyticsEvent[] array = batch;
          for (int i = 0; i < array.Length; i++)
          {
            jsonArray.Add<JsonObject>(array[i].ToPostHogJson());
          }
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["api_key"] = this.apiKey;
          jsonObject["batch"] = jsonArray;
          try
          {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, this.batchUrl)
            {
              Content = new StringContent(jsonObject.ToJsonString(null), Encoding.UTF8, new MediaTypeHeaderValue("application/json"))
            })
            {
              using (CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(15L)))
              {
                using (HttpResponseMessage httpResponseMessage = await this.http.SendAsync(request, cts.Token).ConfigureAwait(false))
                {
                  if (!httpResponseMessage.IsSuccessStatusCode)
                  {
                    string text = "analytics";
                    DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 2);
                    defaultInterpolatedStringHandler.AppendLiteral("PostHog batch of ");
                    defaultInterpolatedStringHandler.AppendFormatted<int>(batch.Length);
                    defaultInterpolatedStringHandler.AppendLiteral(" answered HTTP ");
                    defaultInterpolatedStringHandler.AppendFormatted<int>((int)httpResponseMessage.StatusCode);
                    Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
                    if (httpResponseMessage.StatusCode >= HttpStatusCode.InternalServerError)
                    {
                      this.Requeue(batch);
                    }
                  }
                }
              }
            }
            HttpRequestMessage request = null;
            CancellationTokenSource cts = null;
          }
          catch (Exception ex)
          {
            string text2 = "analytics";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 2);
            defaultInterpolatedStringHandler.AppendLiteral("PostHog batch of ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(batch.Length);
            defaultInterpolatedStringHandler.AppendLiteral(" not sent: ");
            defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
            Log.Warn(text2, defaultInterpolatedStringHandler.ToStringAndClear());
            this.Requeue(batch);
          }
          batch = null;
        }
        finally
        {
          this.sending.Release();
        }
      }
    }

    // Token: 0x06000A80 RID: 2688 RVA: 0x00374D5C File Offset: 0x00374D5C
    private void Requeue(AnalyticsEvent[] batch)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.queue.InsertRange(0, batch);
        if (this.queue.Count > 1000)
        {
          this.queue.RemoveRange(1000, this.queue.Count - 1000);
        }
      }
    }

    // Token: 0x06000A81 RID: 2689 RVA: 0x00374DD8 File Offset: 0x00374DD8
    public void Dispose()
    {
      this.timer.Dispose();
    }

    // Token: 0x040004B2 RID: 1202
    private const int FlushAt = 20;

    // Token: 0x040004B3 RID: 1203
    private const int MaxQueued = 1000;

    // Token: 0x040004B4 RID: 1204
    private readonly HttpClient http;

    // Token: 0x040004B5 RID: 1205
    private readonly Uri batchUrl;

    // Token: 0x040004B6 RID: 1206
    private readonly string apiKey;

    // Token: 0x040004B7 RID: 1207
    private readonly object gate = new object();

    // Token: 0x040004B8 RID: 1208
    private readonly List<AnalyticsEvent> queue = new List<AnalyticsEvent>();

    // Token: 0x040004B9 RID: 1209
    private readonly Timer timer;

    // Token: 0x040004BA RID: 1210
    private readonly SemaphoreSlim sending = new SemaphoreSlim(1, 1);
  }
}
