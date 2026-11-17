using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace HeyClicky.Core.Configuration
{
  // Token: 0x020001DD RID: 477
  [NullableContext(1)]
  [Nullable(0)]
  public static class AppBundleConfiguration
  {
    // Token: 0x17000603 RID: 1539
    // (get) Token: 0x06001B61 RID: 7009 RVA: 0x003DBB18 File Offset: 0x003DBB18
    public static string AppDataDirectory { get; } = AppPaths.Root;

    // Token: 0x17000604 RID: 1540
    // (get) Token: 0x06001B62 RID: 7010 RVA: 0x003DBB20 File Offset: 0x003DBB20
    public static string OverrideFilePath
    {
      get
      {
        return Path.Combine(AppBundleConfiguration.AppDataDirectory, "endpoints.json");
      }
    }

    // Token: 0x17000605 RID: 1541
    // (get) Token: 0x06001B63 RID: 7011 RVA: 0x003DBB34 File Offset: 0x003DBB34
    public static ClickyEndpoints Defaults
    {
      get
      {
        return new ClickyEndpoints(new Uri("https://api.heyclicky.com"), new Uri("https://mrpvynsdsnimuisyhkow.supabase.co"), "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1ycHZ5bnNkc25pbXVpc3loa293Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NzYyODAxNzgsImV4cCI6MjA5MTg1NjE3OH0.qf1-em-OB-OtyX4WTuGnSi28djq-5T0k0whKvcTtTAY");
      }
    }

    // Token: 0x17000606 RID: 1542
    // (get) Token: 0x06001B64 RID: 7012 RVA: 0x003DBB54 File Offset: 0x003DBB54
    public static string UseProductionBackendFlagPath
    {
      get
      {
        return Path.Combine(AppBundleConfiguration.AppDataDirectory, "use-production-backend.flag");
      }
    }

    // Token: 0x17000607 RID: 1543
    // (get) Token: 0x06001B65 RID: 7013 RVA: 0x003DBB68 File Offset: 0x003DBB68
    public static bool UseProductionBackendRequested
    {
      get
      {
        return File.Exists(AppBundleConfiguration.UseProductionBackendFlagPath);
      }
    }

    // Token: 0x17000608 RID: 1544
    // (get) Token: 0x06001B66 RID: 7014 RVA: 0x003DBB74 File Offset: 0x003DBB74
    // (set) Token: 0x06001B67 RID: 7015 RVA: 0x003DBB7C File Offset: 0x003DBB7C
    public static bool IsProductionBackendOverrideActive { get; private set; }

    // Token: 0x06001B68 RID: 7016 RVA: 0x003DBB84 File Offset: 0x003DBB84
    public static ClickyEndpoints ApplyProductionBackendOverride(ClickyEndpoints endpoints, bool requested)
    {
      if (!requested || !AppBundleConfiguration.IsLocalUrl(endpoints.WorkerBaseUrl))
      {
        return endpoints;
      }
      return new ClickyEndpoints(new Uri("https://api.heyclicky.com"), new Uri("https://mrpvynsdsnimuisyhkow.supabase.co"), "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1ycHZ5bnNkc25pbXVpc3loa293Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NzYyODAxNzgsImV4cCI6MjA5MTg1NjE3OH0.qf1-em-OB-OtyX4WTuGnSi28djq-5T0k0whKvcTtTAY");
    }

    // Token: 0x06001B69 RID: 7017 RVA: 0x003DBBB8 File Offset: 0x003DBBB8
    public static ClickyEndpoints Load([Nullable(2)] string overrideFilePath = null)
    {
      ClickyEndpoints clickyEndpoints = AppBundleConfiguration.LoadConfigured(overrideFilePath);
      ClickyEndpoints clickyEndpoints2 = AppBundleConfiguration.ApplyProductionBackendOverride(clickyEndpoints, AppBundleConfiguration.UseProductionBackendRequested);
      AppBundleConfiguration.IsProductionBackendOverrideActive = clickyEndpoints2 != clickyEndpoints;
      if (AppBundleConfiguration.IsProductionBackendOverrideActive)
      {
        Log.Warn("Config", "Developer → Use production backend is on: this build talks to the production Worker and Supabase");
      }
      return clickyEndpoints2;
    }

    // Token: 0x06001B6A RID: 7018 RVA: 0x003DBBFC File Offset: 0x003DBBFC
    private unsafe static ClickyEndpoints LoadConfigured([Nullable(2)] string overrideFilePath)
    {
      ClickyEndpoints clickyEndpoints = AppBundleConfiguration.WithBuildMetadata(AppBundleConfiguration.Defaults);
      string text = overrideFilePath ?? AppBundleConfiguration.OverrideFilePath;
      try
      {
        if (!File.Exists(text))
        {
          return AppBundleConfiguration.ApplyWorkerOverride(clickyEndpoints, Environment.GetEnvironmentVariable("HEYCLICKY_WORKER_BASE_URL"));
        }
        ReadOnlyMemory<byte> readOnlyMemory = File.ReadAllBytes(text);
        if (readOnlyMemory.Span.StartsWith<byte>(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.F1945CD6C19E56B3C1C78943EF5EC18116907A4CA1EFC40A57D48AB1DB7ADFC5), 3)))
        {
          ref ReadOnlyMemory<byte> ptr = ref readOnlyMemory;
          readOnlyMemory = ptr.Slice(3, ptr.Length - 3);
        }
        using (JsonDocument jsonDocument = JsonDocument.Parse(readOnlyMemory, new JsonDocumentOptions
        {
          CommentHandling = JsonCommentHandling.Skip,
          AllowTrailingCommas = true
        }))
        {
          JsonElement rootElement = jsonDocument.RootElement;
          string text2 = AppBundleConfiguration.ReadConfigured(rootElement, "workerBaseUrl");
          string text3 = AppBundleConfiguration.ReadConfigured(rootElement, "supabaseUrl");
          string text4 = AppBundleConfiguration.ReadConfigured(rootElement, "supabaseAnonKey");
          clickyEndpoints = new ClickyEndpoints((text2 == null) ? clickyEndpoints.WorkerBaseUrl : new Uri(text2.TrimEnd('/')), (text3 == null) ? clickyEndpoints.SupabaseUrl : new Uri(text3.TrimEnd('/')), text4 ?? clickyEndpoints.SupabaseAnonKey);
          string text5 = "Config";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 2);
          defaultInterpolatedStringHandler.AppendLiteral("endpoints.json override: worker=");
          defaultInterpolatedStringHandler.AppendFormatted<Uri>(clickyEndpoints.WorkerBaseUrl);
          defaultInterpolatedStringHandler.AppendLiteral(" supabase=");
          defaultInterpolatedStringHandler.AppendFormatted<Uri>(clickyEndpoints.SupabaseUrl);
          Log.Info(text5, defaultInterpolatedStringHandler.ToStringAndClear());
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Config", "endpoints.json ignored: " + ex.Message);
      }
      return AppBundleConfiguration.ApplyWorkerOverride(clickyEndpoints, Environment.GetEnvironmentVariable("HEYCLICKY_WORKER_BASE_URL"));
    }

    // Token: 0x06001B6B RID: 7019 RVA: 0x003DBDD0 File Offset: 0x003DBDD0
    public static ClickyEndpoints ApplyWorkerOverride(ClickyEndpoints endpoints, [Nullable(2)] string workerBaseUrl)
    {
      if (string.IsNullOrWhiteSpace(workerBaseUrl))
      {
        return endpoints;
      }
      Uri uri;
      if (!Uri.TryCreate(workerBaseUrl.Trim().TrimEnd('/'), UriKind.Absolute, out uri) || !AppBundleConfiguration.IsLocalUrl(uri))
      {
        Log.Warn("Config", "HEYCLICKY_WORKER_BASE_URL ignored (loopback URLs only)");
        return endpoints;
      }
      string text = "Config";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 2);
      defaultInterpolatedStringHandler.AppendFormatted("HEYCLICKY_WORKER_BASE_URL");
      defaultInterpolatedStringHandler.AppendLiteral(": worker=");
      defaultInterpolatedStringHandler.AppendFormatted<Uri>(uri);
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      ClickyEndpoints clickyEndpoints = endpoints.<Clone>$();
      clickyEndpoints.WorkerBaseUrl = uri;
      return clickyEndpoints;
    }

    // Token: 0x06001B6C RID: 7020 RVA: 0x003DBE60 File Offset: 0x003DBE60
    private static ClickyEndpoints WithBuildMetadata(ClickyEndpoints endpoints)
    {
      ClickyEndpoints clickyEndpoints;
      try
      {
        AppBundleConfiguration.<>c__DisplayClass26_0 CS$<>8__locals1;
        CS$<>8__locals1.entry = Assembly.GetEntryAssembly();
        if (CS$<>8__locals1.entry == null)
        {
          clickyEndpoints = endpoints;
        }
        else
        {
          string text = AppBundleConfiguration.<WithBuildMetadata>g__Meta|26_0("HeyClicky.WorkerBaseUrl", ref CS$<>8__locals1);
          string text2 = AppBundleConfiguration.<WithBuildMetadata>g__Meta|26_0("HeyClicky.SupabaseUrl", ref CS$<>8__locals1);
          string text3 = AppBundleConfiguration.<WithBuildMetadata>g__Meta|26_0("HeyClicky.SupabaseAnonKey", ref CS$<>8__locals1);
          if (text == null && text2 == null && text3 == null)
          {
            clickyEndpoints = endpoints;
          }
          else
          {
            string text4 = ((text2 != null && AppBundleConfiguration.IsLocalUrl(new Uri(text2))) ? "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZS1kZW1vIiwicm9sZSI6ImFub24iLCJleHAiOjE5ODM4MTI5OTZ9.CRXP1A7WOeoJeXxjNni43kdQwgnWNReilDMblYTn_I0" : endpoints.SupabaseAnonKey);
            clickyEndpoints = new ClickyEndpoints((text == null) ? endpoints.WorkerBaseUrl : new Uri(text.TrimEnd('/')), (text2 == null) ? endpoints.SupabaseUrl : new Uri(text2.TrimEnd('/')), text3 ?? text4);
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Config", "build endpoints ignored: " + ex.Message);
        clickyEndpoints = endpoints;
      }
      return clickyEndpoints;
    }

    // Token: 0x06001B6D RID: 7021 RVA: 0x003DBF54 File Offset: 0x003DBF54
    [return: Nullable(2)]
    private static string ReadConfigured(JsonElement root, string name)
    {
      JsonElement jsonElement;
      if (root.ValueKind != JsonValueKind.Object || !root.TryGetProperty(name, out jsonElement) || jsonElement.ValueKind != JsonValueKind.String)
      {
        return null;
      }
      string @string = jsonElement.GetString();
      string text = ((@string != null) ? @string.Trim() : null);
      if (!string.IsNullOrEmpty(text) && (!text.StartsWith("$(") || !text.EndsWith(')')))
      {
        return text;
      }
      return null;
    }

    // Token: 0x06001B6E RID: 7022 RVA: 0x003DBFBC File Offset: 0x003DBFBC
    public static bool IsLocalUrl(Uri url)
    {
      string text = url.Host.ToLowerInvariant().Trim(new char[] { '[', ']' });
      return text == "localhost" || text == "127.0.0.1" || text == "::1";
    }

    // Token: 0x06001B6F RID: 7023 RVA: 0x003DC018 File Offset: 0x003DC018
    public static bool IsLocalDevelopmentConfiguration(ClickyEndpoints endpoints)
    {
      return AppBundleConfiguration.IsLocalUrl(endpoints.WorkerBaseUrl) || AppBundleConfiguration.IsLocalUrl(endpoints.SupabaseUrl);
    }

    // Token: 0x06001B71 RID: 7025 RVA: 0x003DC040 File Offset: 0x003DC040
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <WithBuildMetadata>g__Meta|26_0(string key, ref AppBundleConfiguration.<>c__DisplayClass26_0 A_1)
    {
      object[] customAttributes = A_1.entry.GetCustomAttributes(typeof(AssemblyMetadataAttribute), false);
      for (int i = 0; i < customAttributes.Length; i++)
      {
        AssemblyMetadataAttribute assemblyMetadataAttribute = customAttributes[i] as AssemblyMetadataAttribute;
        if (assemblyMetadataAttribute != null && assemblyMetadataAttribute.Key == key && !string.IsNullOrWhiteSpace(assemblyMetadataAttribute.Value))
        {
          return assemblyMetadataAttribute.Value.Trim();
        }
      }
      return null;
    }

    // Token: 0x04000BAD RID: 2989
    public const string ProductionWorkerBaseUrl = "https://api.heyclicky.com";

    // Token: 0x04000BAE RID: 2990
    public const string ProductionSupabaseUrl = "https://mrpvynsdsnimuisyhkow.supabase.co";

    // Token: 0x04000BAF RID: 2991
    public const string ProductionSupabaseAnonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1ycHZ5bnNkc25pbXVpc3loa293Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NzYyODAxNzgsImV4cCI6MjA5MTg1NjE3OH0.qf1-em-OB-OtyX4WTuGnSi28djq-5T0k0whKvcTtTAY";

    // Token: 0x04000BB0 RID: 2992
    public const string LocalWorkerBaseUrl = "http://127.0.0.1:8787";

    // Token: 0x04000BB1 RID: 2993
    public const string LocalSupabaseUrl = "http://127.0.0.1:54321";

    // Token: 0x04000BB2 RID: 2994
    public const string LocalSupabaseAnonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZS1kZW1vIiwicm9sZSI6ImFub24iLCJleHAiOjE5ODM4MTI5OTZ9.CRXP1A7WOeoJeXxjNni43kdQwgnWNReilDMblYTn_I0";

    // Token: 0x04000BB5 RID: 2997
    public const string WorkerOverrideVariable = "HEYCLICKY_WORKER_BASE_URL";
  }
}
