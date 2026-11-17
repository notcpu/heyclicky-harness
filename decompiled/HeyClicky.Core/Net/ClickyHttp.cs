using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Core.Net
{
  // Token: 0x0200011F RID: 287
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyHttp : IDisposable
  {
    // Token: 0x17000459 RID: 1113
    // (get) Token: 0x060012BF RID: 4799 RVA: 0x003A338C File Offset: 0x003A338C
    public HttpClient Client { get; }

    // Token: 0x1700045A RID: 1114
    // (get) Token: 0x060012C0 RID: 4800 RVA: 0x003A3394 File Offset: 0x003A3394
    public HttpMessageInvoker Invoker { get; }

    // Token: 0x1700045B RID: 1115
    // (get) Token: 0x060012C1 RID: 4801 RVA: 0x003A339C File Offset: 0x003A339C
    public ClickyEndpoints Endpoints { get; }

    // Token: 0x1700045C RID: 1116
    // (get) Token: 0x060012C2 RID: 4802 RVA: 0x003A33A4 File Offset: 0x003A33A4
    public ClientIdentity Identity { get; }

    // Token: 0x060012C3 RID: 4803 RVA: 0x003A33AC File Offset: 0x003A33AC
    public unsafe ClickyHttp(ClickyEndpoints endpoints, ClientIdentity identity)
    {
      this.Endpoints = endpoints;
      this.Identity = identity;
      SocketsHttpHandler socketsHttpHandler = new SocketsHttpHandler();
      socketsHttpHandler.EnableMultipleHttp2Connections = true;
      socketsHttpHandler.PooledConnectionLifetime = TimeSpan.FromMinutes(10L);
      socketsHttpHandler.PooledConnectionIdleTimeout = TimeSpan.FromMinutes(3L);
      socketsHttpHandler.ConnectTimeout = TimeSpan.FromSeconds(6L);
      socketsHttpHandler.KeepAlivePingDelay = TimeSpan.FromSeconds(30L);
      socketsHttpHandler.KeepAlivePingTimeout = TimeSpan.FromSeconds(10L);
      socketsHttpHandler.KeepAlivePingPolicy = HttpKeepAlivePingPolicy.Always;
      socketsHttpHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli;
      socketsHttpHandler.MaxConnectionsPerServer = 16;
      socketsHttpHandler.UseCookies = false;
      SslClientAuthenticationOptions sslClientAuthenticationOptions = new SslClientAuthenticationOptions();
      int num = 2;
      List<SslApplicationProtocol> list = new List<SslApplicationProtocol>(num);
      CollectionsMarshal.SetCount<SslApplicationProtocol>(list, num);
      Span<SslApplicationProtocol> span = CollectionsMarshal.AsSpan<SslApplicationProtocol>(list);
      int num2 = 0;
      *span[num2] = SslApplicationProtocol.Http2;
      num2++;
      *span[num2] = SslApplicationProtocol.Http11;
      sslClientAuthenticationOptions.ApplicationProtocols = list;
      socketsHttpHandler.SslOptions = sslClientAuthenticationOptions;
      this.socketsHandler = socketsHttpHandler;
      this.Invoker = new HttpMessageInvoker(this.socketsHandler, false);
      this.Client = new HttpClient(new ClickyClientHeadersHandler(endpoints, identity, this.socketsHandler), false)
      {
        Timeout = Timeout.InfiniteTimeSpan,
        DefaultRequestVersion = HttpVersion.Version20,
        DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower
      };
    }

    // Token: 0x060012C4 RID: 4804 RVA: 0x003A34DC File Offset: 0x003A34DC
    public Uri WorkerUrl(string pathAndQuery)
    {
      return new Uri(this.Endpoints.WorkerBaseUrl, pathAndQuery);
    }

    // Token: 0x060012C5 RID: 4805 RVA: 0x003A34F0 File Offset: 0x003A34F0
    public Uri SupabaseUrl(string pathAndQuery)
    {
      return new Uri(this.Endpoints.SupabaseUrl, pathAndQuery);
    }

    // Token: 0x060012C6 RID: 4806 RVA: 0x003A3504 File Offset: 0x003A3504
    public void PrewarmWorker()
    {
      this.PrewarmHost(this.Endpoints.WorkerBaseUrl, ref this.lastWorkerPrewarmTicks, TimeSpan.FromSeconds(5L));
    }

    // Token: 0x060012C7 RID: 4807 RVA: 0x003A3524 File Offset: 0x003A3524
    public void PrewarmOpenAI()
    {
      this.PrewarmHost(new Uri("https://api.openai.com"), ref this.lastOpenAIPrewarmTicks, TimeSpan.FromSeconds(60L));
    }

    // Token: 0x060012C8 RID: 4808 RVA: 0x003A3544 File Offset: 0x003A3544
    public void PrewarmDeepgram()
    {
      this.PrewarmHost(new Uri("https://api.deepgram.com"), ref this.lastDeepgramPrewarmTicks, TimeSpan.FromSeconds(60L));
    }

    // Token: 0x060012C9 RID: 4809 RVA: 0x003A3564 File Offset: 0x003A3564
    public void PrewarmGemini()
    {
      this.PrewarmHost(new Uri("https://generativelanguage.googleapis.com"), ref this.lastGeminiPrewarmTicks, TimeSpan.FromSeconds(60L));
    }

    // Token: 0x060012CA RID: 4810 RVA: 0x003A3584 File Offset: 0x003A3584
    public void PrewarmAll()
    {
      this.PrewarmWorker();
      this.PrewarmOpenAI();
      this.PrewarmDeepgram();
      this.PrewarmGemini();
    }

    // Token: 0x060012CB RID: 4811 RVA: 0x003A35A0 File Offset: 0x003A35A0
    private void PrewarmHost(Uri baseUrl, ref long lastTicks, TimeSpan minimumGap)
    {
      ClickyHttp.<>c__DisplayClass25_0 CS$<>8__locals1 = new ClickyHttp.<>c__DisplayClass25_0();
      CS$<>8__locals1.baseUrl = baseUrl;
      CS$<>8__locals1.<>4__this = this;
      long tickCount = Environment.TickCount64;
      long num = Interlocked.Read(ref lastTicks);
      if (num != 0L && (double)(tickCount - num) < minimumGap.TotalMilliseconds)
      {
        return;
      }
      Interlocked.Exchange(ref lastTicks, tickCount);
      Task.Run(delegate
      {
        ClickyHttp.<>c__DisplayClass25_0.<<PrewarmHost>b__0>d <<PrewarmHost>b__0>d;
        <<PrewarmHost>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<PrewarmHost>b__0>d.<>4__this = CS$<>8__locals1;
        <<PrewarmHost>b__0>d.<>1__state = -1;
        <<PrewarmHost>b__0>d.<>t__builder.Start<ClickyHttp.<>c__DisplayClass25_0.<<PrewarmHost>b__0>d>(ref <<PrewarmHost>b__0>d);
        return <<PrewarmHost>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x060012CC RID: 4812 RVA: 0x003A35FC File Offset: 0x003A35FC
    public void Dispose()
    {
      this.Client.Dispose();
      this.Invoker.Dispose();
      this.socketsHandler.Dispose();
    }

    // Token: 0x04000870 RID: 2160
    private readonly SocketsHttpHandler socketsHandler;

    // Token: 0x04000871 RID: 2161
    private long lastWorkerPrewarmTicks;

    // Token: 0x04000872 RID: 2162
    private long lastOpenAIPrewarmTicks;

    // Token: 0x04000873 RID: 2163
    private long lastDeepgramPrewarmTicks;

    // Token: 0x04000874 RID: 2164
    private long lastGeminiPrewarmTicks;
  }
}
