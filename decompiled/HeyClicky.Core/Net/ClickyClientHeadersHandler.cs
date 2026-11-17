using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.Core.Net
{
  // Token: 0x0200011E RID: 286
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyClientHeadersHandler : DelegatingHandler
  {
    // Token: 0x060012BA RID: 4794 RVA: 0x003A3238 File Offset: 0x003A3238
    public ClickyClientHeadersHandler(ClickyEndpoints endpoints, ClientIdentity identity, HttpMessageHandler inner)
      : base(inner)
    {
      this.endpoints = endpoints;
      this.identity = identity;
    }

    // Token: 0x060012BB RID: 4795 RVA: 0x003A3250 File Offset: 0x003A3250
    public static bool IsClickyBackendUrl(Uri url, ClickyEndpoints endpoints)
    {
      return url.IsAbsoluteUri && string.Equals(url.Host, endpoints.WorkerBaseUrl.Host, StringComparison.OrdinalIgnoreCase) && url.Port == endpoints.WorkerBaseUrl.Port;
    }

    // Token: 0x060012BC RID: 4796 RVA: 0x003A3288 File Offset: 0x003A3288
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      Uri requestUri = request.RequestUri;
      if (requestUri != null && ClickyClientHeadersHandler.IsClickyBackendUrl(requestUri, this.endpoints))
      {
        ClickyClientHeadersHandler.Apply(request.Headers, this.identity);
      }
      return base.SendAsync(request, cancellationToken);
    }

    // Token: 0x060012BD RID: 4797 RVA: 0x003A32C8 File Offset: 0x003A32C8
    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      Uri requestUri = request.RequestUri;
      if (requestUri != null && ClickyClientHeadersHandler.IsClickyBackendUrl(requestUri, this.endpoints))
      {
        ClickyClientHeadersHandler.Apply(request.Headers, this.identity);
      }
      return base.Send(request, cancellationToken);
    }

    // Token: 0x060012BE RID: 4798 RVA: 0x003A3308 File Offset: 0x003A3308
    public static void Apply(HttpRequestHeaders headers, ClientIdentity identity)
    {
      headers.TryAddWithoutValidation("X-Clicky-Platform", identity.Platform);
      headers.TryAddWithoutValidation("X-Clicky-Device", identity.DeviceId);
      headers.TryAddWithoutValidation("X-Clicky-App-Version", identity.AppVersion);
      headers.TryAddWithoutValidation("X-Clicky-App-Build", identity.AppBuild);
      headers.TryAddWithoutValidation("X-Clicky-OS-Version", identity.OsVersion);
      try
      {
        ClickyAITracing.Apply(headers);
      }
      catch
      {
      }
    }

    // Token: 0x0400086A RID: 2154
    private readonly ClickyEndpoints endpoints;

    // Token: 0x0400086B RID: 2155
    private readonly ClientIdentity identity;
  }
}
