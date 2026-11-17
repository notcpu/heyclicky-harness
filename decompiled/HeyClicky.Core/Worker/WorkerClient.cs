using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Auth;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Configuration;
using HeyClicky.Core.Net;
using Microsoft.Win32;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200003A RID: 58
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class WorkerClient
  {
    // Token: 0x060001E0 RID: 480 RVA: 0x0034F994 File Offset: 0x0034F994
    public WorkerClient(ClickyHttp http, SupabaseAuthService auth)
    {
      this.http = http;
      this.auth = auth;
    }

    // Token: 0x1700005F RID: 95
    // (get) Token: 0x060001E1 RID: 481 RVA: 0x0034F9AC File Offset: 0x0034F9AC
    public ClickyHttp Http
    {
      get
      {
        return this.http;
      }
    }

    // Token: 0x060001E2 RID: 482 RVA: 0x0034F9B4 File Offset: 0x0034F9B4
    [return: Nullable(new byte[] { 1, 2 })]
    public Task<PlanSnapshot> GetPlanAsync(CancellationToken ct = default(CancellationToken))
    {
      return this.GetAsync<PlanSnapshot>("/me/plan", CoreJsonContext.Default.PlanSnapshot, ct);
    }

    // Token: 0x060001E3 RID: 483 RVA: 0x0034F9CC File Offset: 0x0034F9CC
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<AppConfig> GetAppConfigAsync(CancellationToken ct = default(CancellationToken))
    {
      object obj = await this.SendAsync<AppConfigResponse>(HttpMethod.Get, "/app-config", null, CoreJsonContext.Default.AppConfigResponse, false, ct, null, null).ConfigureAwait(false);
      return (obj != null) ? obj.AppConfig : null;
    }

    // Token: 0x060001E4 RID: 484 RVA: 0x0034FA18 File Offset: 0x0034FA18
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<JsonObject> GetRuntimeModelPolicyAsync(CancellationToken ct = default(CancellationToken))
    {
      byte[] array = await this.SendRawAsync(HttpMethod.Get, "/runtime/model-policy", null, true, ct, new TimeSpan?(TimeSpan.FromSeconds(10L)), null).ConfigureAwait(false);
      return (array == null) ? null : (JsonNode.Parse(array, null, default(JsonDocumentOptions)) as JsonObject);
    }

    // Token: 0x060001E5 RID: 485 RVA: 0x0034FA64 File Offset: 0x0034FA64
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<RegisteredDevice> RegisterDeviceAsync(CancellationToken ct = default(CancellationToken))
    {
      DeviceRegistrationRequest deviceRegistrationRequest = new DeviceRegistrationRequest(this.http.Identity.Capabilities, WorkerClient.DeviceDisplayName(), WorkerClient.DeviceModel());
      object obj = await this.SendAsync<DeviceResponse>(HttpMethod.Post, "/me/devices", WorkerClient.Serialize<DeviceRegistrationRequest>(deviceRegistrationRequest, CoreJsonContext.Default.DeviceRegistrationRequest), CoreJsonContext.Default.DeviceResponse, true, ct, new TimeSpan?(TimeSpan.FromSeconds(20L)), null).ConfigureAwait(false);
      return (obj != null) ? obj.Device : null;
    }

    // Token: 0x060001E6 RID: 486 RVA: 0x0034FAB0 File Offset: 0x0034FAB0
    public async Task<IReadOnlyList<RegisteredDevice>> GetDevicesAsync(CancellationToken ct = default(CancellationToken))
    {
      object obj = await this.GetAsync<DevicesResponse>("/me/devices", CoreJsonContext.Default.DevicesResponse, ct).ConfigureAwait(false);
      return ((obj != null) ? obj.Devices : null) ?? Array.Empty<RegisteredDevice>();
    }

    // Token: 0x060001E7 RID: 487 RVA: 0x0034FAFC File Offset: 0x0034FAFC
    public async Task RegisterThisDeviceIfNeededAsync(CancellationToken ct = default(CancellationToken))
    {
      string userId = this.auth.UserId;
      if (userId != null)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 4);
        defaultInterpolatedStringHandler.AppendFormatted(userId);
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted<Uri>(this.http.Endpoints.WorkerBaseUrl);
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted(this.http.Identity.AppBuild);
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted(this.http.Identity.OsVersion);
        string fingerprint = defaultInterpolatedStringHandler.ToStringAndClear();
        string stampPath = Path.Combine(AppBundleConfiguration.AppDataDirectory, "device-registration.json");
        try
        {
          if (File.Exists(stampPath))
          {
            DeviceRegistrationStamp deviceRegistrationStamp = JsonSerializer.Deserialize<DeviceRegistrationStamp>(File.ReadAllBytes(stampPath), CoreJsonContext.Default.DeviceRegistrationStamp);
            if (deviceRegistrationStamp != null && deviceRegistrationStamp.Fingerprint == fingerprint && DateTimeOffset.UtcNow - DateTimeOffset.FromUnixTimeSeconds(deviceRegistrationStamp.RegisteredAtEpochSeconds) < TimeSpan.FromHours(24))
            {
              return;
            }
          }
          await this.RegisterDeviceAsync(ct).ConfigureAwait(false);
          AtomicFile.WriteAllBytes(stampPath, JsonSerializer.SerializeToUtf8Bytes<DeviceRegistrationStamp>(new DeviceRegistrationStamp(fingerprint, DateTimeOffset.UtcNow.ToUnixTimeSeconds()), CoreJsonContext.Default.DeviceRegistrationStamp));
          Log.Info("Worker", "device registered");
        }
        catch (Exception ex)
        {
          Log.Warn("Worker", "device registration failed: " + ex.Message);
        }
      }
    }

    // Token: 0x060001E8 RID: 488 RVA: 0x0034FB48 File Offset: 0x0034FB48
    [NullableContext(2)]
    private static string DeviceDisplayName()
    {
      string machineName = Environment.MachineName;
      string text = ((machineName != null) ? machineName.Trim() : null);
      if (string.IsNullOrEmpty(text))
      {
        return null;
      }
      if (text.Length <= 80)
      {
        return text;
      }
      return text.Substring(0, 80);
    }

    // Token: 0x060001E9 RID: 489 RVA: 0x0034FB88 File Offset: 0x0034FB88
    [NullableContext(2)]
    private static string DeviceModel()
    {
      string text3;
      try
      {
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\BIOS"))
        {
          string text = ((registryKey != null) ? registryKey.GetValue("SystemProductName") : null) as string;
          string text2 = ((text != null) ? text.Trim() : null);
          text3 = (string.IsNullOrEmpty(text2) ? null : ((text2.Length > 60) ? text2.Substring(0, 60) : text2));
        }
      }
      catch
      {
        text3 = null;
      }
      return text3;
    }

    // Token: 0x060001EA RID: 490 RVA: 0x0034FC18 File Offset: 0x0034FC18
    [return: Nullable(new byte[] { 1, 2 })]
    public Task<DiscoveryChannelResponse> SetDiscoveryChannelAsync(string channel, [Nullable(2)] string other, CancellationToken ct = default(CancellationToken))
    {
      return this.SendAsync<DiscoveryChannelResponse>(HttpMethod.Post, "/me/discovery-channel", WorkerClient.Serialize<DiscoveryChannelRequest>(new DiscoveryChannelRequest(channel, string.IsNullOrWhiteSpace(other) ? null : other.Trim()), CoreJsonContext.Default.DiscoveryChannelRequest), CoreJsonContext.Default.DiscoveryChannelResponse, true, ct, null, null);
    }

    // Token: 0x060001EB RID: 491 RVA: 0x0034FC74 File Offset: 0x0034FC74
    [return: Nullable(new byte[] { 1, 2 })]
    public Task<DiscoveryChannelResponse> GetDiscoveryChannelAsync(CancellationToken ct = default(CancellationToken))
    {
      return this.GetAsync<DiscoveryChannelResponse>("/me/discovery-channel", CoreJsonContext.Default.DiscoveryChannelResponse, ct);
    }

    // Token: 0x060001EC RID: 492 RVA: 0x0034FC8C File Offset: 0x0034FC8C
    [return: Nullable(new byte[] { 1, 2 })]
    public Task<UserMemoryResponse> GetMemoryAsync(CancellationToken ct = default(CancellationToken))
    {
      return this.SendAsync<UserMemoryResponse>(HttpMethod.Get, "/me/memory", null, CoreJsonContext.Default.UserMemoryResponse, true, ct, new TimeSpan?(TimeSpan.FromSeconds(4L)), null);
    }

    // Token: 0x060001ED RID: 493 RVA: 0x0034FCC4 File Offset: 0x0034FCC4
    [return: Nullable(new byte[] { 1, 2 })]
    public Task<SaveMemoryResponse> SaveMemoryAsync(string memory, CancellationToken ct = default(CancellationToken))
    {
      return this.SendAsync<SaveMemoryResponse>(HttpMethod.Post, "/me/memory/save", WorkerClient.Serialize<SaveMemoryRequest>(new SaveMemoryRequest(memory), CoreJsonContext.Default.SaveMemoryRequest), CoreJsonContext.Default.SaveMemoryResponse, true, ct, null, null);
    }

    // Token: 0x060001EE RID: 494 RVA: 0x0034FD0C File Offset: 0x0034FD0C
    public void ReportError(ErrorReport report)
    {
      WorkerClient.<>c__DisplayClass19_0 CS$<>8__locals1 = new WorkerClient.<>c__DisplayClass19_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.report = report;
      Task.Run(delegate
      {
        WorkerClient.<>c__DisplayClass19_0.<<ReportError>b__0>d <<ReportError>b__0>d;
        <<ReportError>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<ReportError>b__0>d.<>4__this = CS$<>8__locals1;
        <<ReportError>b__0>d.<>1__state = -1;
        <<ReportError>b__0>d.<>t__builder.Start<WorkerClient.<>c__DisplayClass19_0.<<ReportError>b__0>d>(ref <<ReportError>b__0>d);
        return <<ReportError>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x060001EF RID: 495 RVA: 0x0034FD34 File Offset: 0x0034FD34
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<StarterPlanResponse> BuildStarterPlanAsync(StarterPlanRequest request, CancellationToken ct = default(CancellationToken))
    {
      ByteArrayContent body = WorkerClient.Serialize<StarterPlanRequest>(request, CoreJsonContext.Default.StarterPlanRequest);
      int attempt = 1;
      StarterPlanResponse starterPlanResponse;
      for (;;)
      {
        int num = 0;
        object obj;
        Exception ex;
        bool flag;
        Exception ex2;
        try
        {
          starterPlanResponse = await this.SendAsync<StarterPlanResponse>(HttpMethod.Post, "/onboarding/starter-plan", body, CoreJsonContext.Default.StarterPlanResponse, true, ct, new TimeSpan?(TimeSpan.FromSeconds(120L)), null).ConfigureAwait(false);
          break;
        }
        catch when (delegate
        {
          // Failed to create a 'catch-when' expression
          ex = obj as Exception;
          if (ex == null)
          {
            flag = false;
          }
          else
          {
            ex2 = (Exception)ex;
            flag = (attempt < 2 && !(ex2 is PaywallReachedException) && !ct.IsCancellationRequested) > false;
          }
          endfilter(flag);
        })
        {
          num = 1;
        }
        if (num == 1)
        {
          string text = "Worker";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 2);
          defaultInterpolatedStringHandler.AppendLiteral("starter plan attempt ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(attempt);
          defaultInterpolatedStringHandler.AppendLiteral(" failed: ");
          defaultInterpolatedStringHandler.AppendFormatted(ex2.Message);
          Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
          await Task.Delay(2000, ct).ConfigureAwait(false);
        }
        num = attempt;
        attempt = num + 1;
      }
      return starterPlanResponse;
    }

    // Token: 0x060001F0 RID: 496 RVA: 0x0034FD88 File Offset: 0x0034FD88
    public async Task<IReadOnlyList<ImageSearchResult>> SearchImagesAsync(string query, int limit = 8, bool preferSubjectPhoto = false, [Nullable(2)] string pageUrl = null, CancellationToken ct = default(CancellationToken))
    {
      IReadOnlyList<ImageSearchResult> readOnlyList;
      try
      {
        ImageSearchRequest imageSearchRequest = new ImageSearchRequest(query, limit, preferSubjectPhoto ? new bool?(true) : null, pageUrl);
        object obj = await this.SendAsync<ImageSearchResponse>(HttpMethod.Post, "/image-search", WorkerClient.Serialize<ImageSearchRequest>(imageSearchRequest, CoreJsonContext.Default.ImageSearchRequest), CoreJsonContext.Default.ImageSearchResponse, true, ct, new TimeSpan?(TimeSpan.FromSeconds(15L)), null).ConfigureAwait(false);
        readOnlyList = ((obj != null) ? obj.Images : null) ?? Array.Empty<ImageSearchResult>();
      }
      catch (Exception ex)
      {
        Log.Warn("Worker", "image search failed: " + ex.Message);
        readOnlyList = Array.Empty<ImageSearchResult>();
      }
      return readOnlyList;
    }

    // Token: 0x060001F1 RID: 497 RVA: 0x0034FDF8 File Offset: 0x0034FDF8
    [return: Nullable(new byte[] { 1, 2 })]
    public Task<WebSearchResponse> SearchWebAsync(string query, int? count = null, [Nullable(2)] string freshness = null, CancellationToken ct = default(CancellationToken))
    {
      return this.SendAsync<WebSearchResponse>(HttpMethod.Post, "/web-search", WorkerClient.Serialize<WebSearchRequest>(new WebSearchRequest(query, count, freshness), CoreJsonContext.Default.WebSearchRequest), CoreJsonContext.Default.WebSearchResponse, true, ct, null, null);
    }

    // Token: 0x060001F2 RID: 498 RVA: 0x0034FE44 File Offset: 0x0034FE44
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<CheckoutResponse> CreateCheckoutAsync(string plan, string interval = "month", [Nullable(2)] string discountCode = null, CancellationToken ct = default(CancellationToken))
    {
      CheckoutResponse checkoutResponse = await this.SendAsync<CheckoutResponse>(HttpMethod.Post, "/billing/checkout", WorkerClient.Serialize<CheckoutRequest>(new CheckoutRequest(plan, interval, "web", discountCode), CoreJsonContext.Default.CheckoutRequest), CoreJsonContext.Default.CheckoutResponse, true, ct, null, null).ConfigureAwait(false);
      if (checkoutResponse != null && discountCode != null && checkoutResponse.DiscountCodeApplied != discountCode)
      {
        throw new WorkerHttpException(409, "discount code not applied", "/billing/checkout");
      }
      return checkoutResponse;
    }

    // Token: 0x060001F3 RID: 499 RVA: 0x0034FEA8 File Offset: 0x0034FEA8
    [return: Nullable(new byte[] { 1, 2 })]
    public Task<PortalResponse> CreatePortalSessionAsync(CancellationToken ct = default(CancellationToken))
    {
      return this.SendAsync<PortalResponse>(HttpMethod.Post, "/billing/portal", WorkerClient.EmptyJsonContent(), CoreJsonContext.Default.PortalResponse, true, ct, null, null);
    }

    // Token: 0x060001F4 RID: 500 RVA: 0x0034FEE0 File Offset: 0x0034FEE0
    public async Task DeleteAccountAsync([Nullable(2)] string reason, CancellationToken ct = default(CancellationToken))
    {
      string text = ((reason != null) ? reason.Trim() : null);
      byte[] array;
      if (!string.IsNullOrEmpty(text))
      {
        Encoding utf = Encoding.UTF8;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["reason"] = text;
        array = utf.GetBytes(jsonObject.ToJsonString(null));
      }
      else
      {
        array = Array.Empty<byte>();
      }
      ByteArrayContent byteArrayContent = new ByteArrayContent(array);
      byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      ConfiguredTaskAwaitable<byte[]>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.SendRawAsync(HttpMethod.Post, "/me/account/delete", byteArrayContent, true, ct, new TimeSpan?(TimeSpan.FromSeconds(60L)), null).ConfigureAwait(false).GetAwaiter();
      if (!configuredTaskAwaiter.IsCompleted)
      {
        await configuredTaskAwaiter;
        ConfiguredTaskAwaitable<byte[]>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
        configuredTaskAwaiter = configuredTaskAwaiter2;
        configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<byte[]>.ConfiguredTaskAwaiter);
      }
      if (configuredTaskAwaiter.GetResult() == null)
      {
        throw new InvalidOperationException("Sign in again to delete your account.");
      }
    }

    // Token: 0x060001F5 RID: 501 RVA: 0x0034FF34 File Offset: 0x0034FF34
    [return: Nullable(new byte[] { 1, 2 })]
    public Task<JsonObject> GetReferralAsync(CancellationToken ct = default(CancellationToken))
    {
      return this.GetJsonObjectAsync("/me/referral", ct);
    }

    // Token: 0x060001F6 RID: 502 RVA: 0x0034FF44 File Offset: 0x0034FF44
    [return: Nullable(new byte[] { 1, 2 })]
    public Task<ReferralOutcomeResponse> ClaimReferralAsync(string code, string source, CancellationToken ct = default(CancellationToken))
    {
      return this.SendAsync<ReferralOutcomeResponse>(HttpMethod.Post, "/me/referral/claim", WorkerClient.Serialize<ReferralClaimRequest>(new ReferralClaimRequest(code, source), CoreJsonContext.Default.ReferralClaimRequest), CoreJsonContext.Default.ReferralOutcomeResponse, true, ct, null, null);
    }

    // Token: 0x060001F7 RID: 503 RVA: 0x0034FF90 File Offset: 0x0034FF90
    [return: Nullable(new byte[] { 1, 2 })]
    public Task<ReferralOutcomeResponse> ChangeReferralCodeAsync(string code, CancellationToken ct = default(CancellationToken))
    {
      return this.SendAsync<ReferralOutcomeResponse>(HttpMethod.Post, "/me/referral/code", WorkerClient.Serialize<ReferralCodeRequest>(new ReferralCodeRequest(code), CoreJsonContext.Default.ReferralCodeRequest), CoreJsonContext.Default.ReferralOutcomeResponse, true, ct, null, null);
    }

    // Token: 0x060001F8 RID: 504 RVA: 0x0034FFD8 File Offset: 0x0034FFD8
    [return: Nullable(2)]
    public static CheckoutCompleteLink ParseCheckoutCompleteLink(Uri uri)
    {
      if (!uri.IsAbsoluteUri || !AuthDeepLink.Schemes.Contains(uri.Scheme, StringComparer.OrdinalIgnoreCase) || !string.Equals(uri.Host, "checkout-complete", StringComparison.OrdinalIgnoreCase))
      {
        return null;
      }
      Dictionary<string, string> dictionary = AuthDeepLink.ParseParameters(uri.OriginalString);
      string text;
      dictionary.TryGetValue("session_id", out text);
      string text2;
      string text3;
      return new CheckoutCompleteLink(string.IsNullOrEmpty(text) ? null : text, dictionary.TryGetValue("canceled", out text2) && text2 == "1", dictionary.TryGetValue("portal", out text3) && text3 == "1");
    }

    // Token: 0x060001F9 RID: 505 RVA: 0x00350080 File Offset: 0x00350080
    public async Task<IReadOnlyList<string>> GetLanguageHintsAsync([Nullable(2)] string deviceRegion, CancellationToken ct = default(CancellationToken))
    {
      string text = "/onboarding/language-hints" + ((deviceRegion != null && deviceRegion.Length == 2) ? ("?device_region=" + Uri.EscapeDataString(deviceRegion)) : "");
      object obj = await this.GetJsonObjectAsync(text, ct).ConfigureAwait(false);
      JsonArray jsonArray = ((obj != null) ? obj["languages"] : null) as JsonArray;
      IReadOnlyList<string> readOnlyList;
      if (jsonArray == null)
      {
        readOnlyList = new List<string>();
      }
      else
      {
        readOnlyList = jsonArray.Select<JsonNode, string>(delegate(JsonNode l)
        {
          if (l == null)
          {
            return null;
          }
          return l.GetValue<string>();
        }).OfType<string>().ToList<string>();
      }
      return readOnlyList;
    }

    // Token: 0x060001FA RID: 506 RVA: 0x003500D4 File Offset: 0x003500D4
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<T> GetAsync<T>(string path, JsonTypeInfo<T> typeInfo, CancellationToken ct = default(CancellationToken)) where T : class
    {
      return await this.SendAsync<T>(HttpMethod.Get, path, null, typeInfo, true, ct, null, null).ConfigureAwait(false);
    }

    // Token: 0x060001FB RID: 507 RVA: 0x00350130 File Offset: 0x00350130
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<JsonObject> GetJsonObjectAsync(string path, CancellationToken ct = default(CancellationToken))
    {
      byte[] array = await this.SendRawAsync(HttpMethod.Get, path, null, true, ct, null, null).ConfigureAwait(false);
      return (array == null) ? null : (JsonNode.Parse(array, null, default(JsonDocumentOptions)) as JsonObject);
    }

    // Token: 0x060001FC RID: 508 RVA: 0x00350184 File Offset: 0x00350184
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<T> SendAsync<T>(HttpMethod method, string path, [Nullable(2)] HttpContent body, JsonTypeInfo<T> typeInfo, bool authenticated = true, CancellationToken ct = default(CancellationToken), TimeSpan? timeout = null, [Nullable(2)] string usageContext = null) where T : class
    {
      byte[] array = await this.SendRawAsync(method, path, body, authenticated, ct, timeout, usageContext).ConfigureAwait(false);
      return (array == null || array.Length == 0) ? default(T) : JsonSerializer.Deserialize<T>(array, typeInfo);
    }

    // Token: 0x060001FD RID: 509 RVA: 0x0035020C File Offset: 0x0035020C
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<byte[]> SendRawAsync(HttpMethod method, string path, [Nullable(2)] HttpContent body, bool authenticated, CancellationToken ct = default(CancellationToken), TimeSpan? timeout = null, [Nullable(2)] string usageContext = null)
    {
      byte[] array;
      if (body == null)
      {
        array = null;
      }
      else
      {
        array = await body.ReadAsByteArrayAsync(ct).ConfigureAwait(false);
      }
      byte[] bodyBytes = array;
      MediaTypeHeaderValue contentType = ((body != null) ? body.Headers.ContentType : null);
      for (int attempt = 0; attempt < 2; attempt++)
      {
        string text = null;
        if (authenticated)
        {
          text = await this.auth.GetValidAccessTokenAsync(attempt > 0, ct).ConfigureAwait(false);
          if (text == null)
          {
            return null;
          }
        }
        using (HttpRequestMessage request = new HttpRequestMessage(method, this.http.WorkerUrl(path)))
        {
          if (text != null)
          {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", text);
          }
          if (usageContext != null)
          {
            request.Headers.TryAddWithoutValidation("X-Clicky-Usage-Context", usageContext);
          }
          if (bodyBytes != null)
          {
            request.Content = new ByteArrayContent(bodyBytes);
            request.Content.Headers.ContentType = contentType ?? new MediaTypeHeaderValue("application/json");
          }
          using (CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
          {
            cts.CancelAfter(timeout ?? TimeSpan.FromSeconds(60L));
            using (HttpResponseMessage response = await this.http.Client.SendAsync(request, cts.Token).ConfigureAwait(false))
            {
              byte[] array2 = await response.Content.ReadAsByteArrayAsync(cts.Token).ConfigureAwait(false);
              int statusCode = (int)response.StatusCode;
              if (statusCode >= 200 && statusCode < 300)
              {
                return array2;
              }
              if (statusCode != 401 || !authenticated || attempt != 0)
              {
                PaywallPayload paywallPayload;
                JsonElement jsonElement;
                if (BillingPaywallDetector.HandleIfPaywall(statusCode, array2, true, out paywallPayload, out jsonElement))
                {
                  throw new PaywallReachedException(paywallPayload, jsonElement);
                }
                throw new WorkerHttpException(statusCode, Encoding.UTF8.GetString(array2), path);
              }
            }
          }
        }
      }
      return null;
    }

    // Token: 0x060001FE RID: 510 RVA: 0x0035028C File Offset: 0x0035028C
    internal static ByteArrayContent Serialize<[Nullable(2)] T>(T value, JsonTypeInfo<T> typeInfo)
    {
      return new ByteArrayContent(JsonSerializer.SerializeToUtf8Bytes<T>(value, typeInfo))
      {
        Headers = 
        {
          ContentType = new MediaTypeHeaderValue("application/json")
        }
      };
    }

    // Token: 0x060001FF RID: 511 RVA: 0x003502B0 File Offset: 0x003502B0
    private static ByteArrayContent EmptyJsonContent()
    {
      return new ByteArrayContent(Array.Empty<byte>())
      {
        Headers = 
        {
          ContentType = new MediaTypeHeaderValue("application/json")
        }
      };
    }

    // Token: 0x040001D1 RID: 465
    private const string Area = "Worker";

    // Token: 0x040001D2 RID: 466
    private readonly ClickyHttp http;

    // Token: 0x040001D3 RID: 467
    private readonly SupabaseAuthService auth;

    // Token: 0x040001D4 RID: 468
    public static readonly IReadOnlyList<string> DiscoveryChannels = new <>z__ReadOnlyArray<string>(new string[]
    {
      "instagram_ad", "instagram", "youtube_ad", "youtube", "tiktok", "x", "linkedin", "reddit", "chatgpt", "claude",
      "google", "friend", "other"
    });
  }
}
