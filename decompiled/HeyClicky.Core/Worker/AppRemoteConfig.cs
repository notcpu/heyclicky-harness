using System;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Auth;
using HeyClicky.Core.Configuration;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000038 RID: 56
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AppRemoteConfig
  {
    // Token: 0x1700005D RID: 93
    // (get) Token: 0x060001D7 RID: 471 RVA: 0x0034F5C0 File Offset: 0x0034F5C0
    public static AppRemoteConfig Shared
    {
      get
      {
        return AppRemoteConfig.shared.Value;
      }
    }

    // Token: 0x14000001 RID: 1
    // (add) Token: 0x060001D8 RID: 472 RVA: 0x0034F5CC File Offset: 0x0034F5CC
    // (remove) Token: 0x060001D9 RID: 473 RVA: 0x0034F604 File Offset: 0x0034F604
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x060001DA RID: 474 RVA: 0x0034F63C File Offset: 0x0034F63C
    public AppRemoteConfig(string cachePath)
    {
      this.cachePath = cachePath;
      try
      {
        if (File.Exists(cachePath))
        {
          this.config = JsonNode.Parse(File.ReadAllText(cachePath), null, default(JsonDocumentOptions)) as JsonObject;
        }
      }
      catch (Exception ex)
      {
        Log.Warn("app-config", "cached config unreadable, using the shipped defaults: " + ex.Message);
      }
    }

    // Token: 0x1700005E RID: 94
    // (get) Token: 0x060001DB RID: 475 RVA: 0x0034F6C0 File Offset: 0x0034F6C0
    public AppRemoteLinks Links
    {
      get
      {
        JsonObject jsonObject2;
        using (this.gate.EnterScope())
        {
          JsonObject jsonObject = this.config;
          jsonObject2 = ((jsonObject != null) ? jsonObject["links"] : null) as JsonObject;
        }
        AppRemoteLinks bakedInDefaults = AppRemoteLinks.BakedInDefaults;
        return new AppRemoteLinks(AppRemoteConfig.Link(jsonObject2, "featureRequest") ?? bakedInDefaults.FeatureRequest, AppRemoteConfig.Link(jsonObject2, "communityReddit") ?? bakedInDefaults.CommunityReddit, AppRemoteConfig.Link(jsonObject2, "communityWhatsApp") ?? bakedInDefaults.CommunityWhatsApp, AppRemoteConfig.Link(jsonObject2, "changelog") ?? bakedInDefaults.Changelog);
      }
    }

    // Token: 0x060001DC RID: 476 RVA: 0x0034F770 File Offset: 0x0034F770
    [NullableContext(2)]
    private static string Link(JsonObject links, [Nullable(1)] string key)
    {
      JsonValue jsonValue = ((links != null) ? links[key] : null) as JsonValue;
      string text;
      Uri uri;
      bool flag = jsonValue != null && jsonValue.TryGetValue<string>(out text) && Uri.TryCreate(text, UriKind.Absolute, out uri);
      if (flag)
      {
        string scheme = uri.Scheme;
        bool flag2 = scheme == "https" || scheme == "http";
        flag = flag2;
      }
      if (!flag)
      {
        return null;
      }
      return text;
    }

    // Token: 0x060001DD RID: 477 RVA: 0x0034F7E4 File Offset: 0x0034F7E4
    public bool Apply(byte[] responseBody)
    {
      JsonObject jsonObject2;
      try
      {
        JsonObject jsonObject = JsonNode.Parse(responseBody, null, default(JsonDocumentOptions)) as JsonObject;
        jsonObject2 = ((jsonObject != null) ? jsonObject["appConfig"] : null) as JsonObject;
      }
      catch (Exception)
      {
        return false;
      }
      if (jsonObject2 == null)
      {
        return false;
      }
      string text = jsonObject2.ToJsonString(null);
      bool flag;
      using (this.gate.EnterScope())
      {
        try
        {
          Directory.CreateDirectory(Path.GetDirectoryName(this.cachePath));
          AtomicFile.WriteAllBytes(this.cachePath, Encoding.UTF8.GetBytes(text));
        }
        catch (Exception ex)
        {
          Log.Warn("app-config", "cache write failed: " + ex.Message);
        }
        JsonObject jsonObject3 = this.config;
        flag = ((jsonObject3 != null) ? jsonObject3.ToJsonString(null) : null) != text;
        this.config = (JsonObject)jsonObject2.DeepClone();
      }
      if (flag)
      {
        Log.Info("app-config", "fetched a new config");
        Action changed = this.Changed;
        if (changed != null)
        {
          changed();
        }
      }
      return true;
    }

    // Token: 0x060001DE RID: 478 RVA: 0x0034F924 File Offset: 0x0034F924
    public async Task RefreshAsync(WorkerClient worker, CancellationToken ct = default(CancellationToken))
    {
      try
      {
        byte[] array = await worker.SendRawAsync(HttpMethod.Get, "/app-config", null, false, ct, new TimeSpan?(TimeSpan.FromSeconds(15L)), null).ConfigureAwait(false);
        if (array != null)
        {
          this.Apply(array);
        }
      }
      catch (Exception ex) when (!ct.IsCancellationRequested)
      {
        Log.Warn("app-config", "/app-config: " + ex.Message);
      }
    }

    // Token: 0x040001C7 RID: 455
    private static readonly Lazy<AppRemoteConfig> shared = new Lazy<AppRemoteConfig>(() => new AppRemoteConfig(Path.Combine(AppBundleConfiguration.AppDataDirectory, "app-config.json")));

    // Token: 0x040001C8 RID: 456
    private readonly string cachePath;

    // Token: 0x040001C9 RID: 457
    private readonly Lock gate = new Lock();

    // Token: 0x040001CA RID: 458
    [Nullable(2)]
    private JsonObject config;
  }
}
