using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace HeyClicky.Core.Configuration
{
  // Token: 0x020001DE RID: 478
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class TelemetryConfiguration : IEquatable<TelemetryConfiguration>
  {
    // Token: 0x06001B72 RID: 7026 RVA: 0x003DC0A8 File Offset: 0x003DC0A8
    public TelemetryConfiguration([Nullable(2)] string PostHogApiKey, string PostHogHost, [Nullable(2)] string SentryDsn, string UpdateFeedUrl)
    {
      this.PostHogApiKey = PostHogApiKey;
      this.PostHogHost = PostHogHost;
      this.SentryDsn = SentryDsn;
      this.UpdateFeedUrl = UpdateFeedUrl;
      base..ctor();
    }

    // Token: 0x17000609 RID: 1545
    // (get) Token: 0x06001B73 RID: 7027 RVA: 0x003DC0D0 File Offset: 0x003DC0D0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(TelemetryConfiguration);
      }
    }

    // Token: 0x1700060A RID: 1546
    // (get) Token: 0x06001B74 RID: 7028 RVA: 0x003DC0DC File Offset: 0x003DC0DC
    // (set) Token: 0x06001B75 RID: 7029 RVA: 0x003DC0E4 File Offset: 0x003DC0E4
    [Nullable(2)]
    public string PostHogApiKey
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700060B RID: 1547
    // (get) Token: 0x06001B76 RID: 7030 RVA: 0x003DC0F0 File Offset: 0x003DC0F0
    // (set) Token: 0x06001B77 RID: 7031 RVA: 0x003DC0F8 File Offset: 0x003DC0F8
    public string PostHogHost { get; set; }

    // Token: 0x1700060C RID: 1548
    // (get) Token: 0x06001B78 RID: 7032 RVA: 0x003DC104 File Offset: 0x003DC104
    // (set) Token: 0x06001B79 RID: 7033 RVA: 0x003DC10C File Offset: 0x003DC10C
    [Nullable(2)]
    public string SentryDsn
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700060D RID: 1549
    // (get) Token: 0x06001B7A RID: 7034 RVA: 0x003DC118 File Offset: 0x003DC118
    // (set) Token: 0x06001B7B RID: 7035 RVA: 0x003DC120 File Offset: 0x003DC120
    public string UpdateFeedUrl { get; set; }

    // Token: 0x1700060E RID: 1550
    // (get) Token: 0x06001B7C RID: 7036 RVA: 0x003DC12C File Offset: 0x003DC12C
    // (set) Token: 0x06001B7D RID: 7037 RVA: 0x003DC134 File Offset: 0x003DC134
    public static TelemetryConfiguration Current { get; private set; } = TelemetryConfiguration.Load(null, null);

    // Token: 0x1700060F RID: 1551
    // (get) Token: 0x06001B7E RID: 7038 RVA: 0x003DC13C File Offset: 0x003DC13C
    public bool HasPostHogKey
    {
      get
      {
        return !string.IsNullOrWhiteSpace(this.PostHogApiKey);
      }
    }

    // Token: 0x17000610 RID: 1552
    // (get) Token: 0x06001B7F RID: 7039 RVA: 0x003DC14C File Offset: 0x003DC14C
    public bool HasSentryDsn
    {
      get
      {
        return !string.IsNullOrWhiteSpace(this.SentryDsn);
      }
    }

    // Token: 0x17000611 RID: 1553
    // (get) Token: 0x06001B80 RID: 7040 RVA: 0x003DC15C File Offset: 0x003DC15C
    public bool IsPlaceholderFeed
    {
      get
      {
        return this.UpdateFeedUrl.StartsWith("https://heyclicky-releases.invalid/windows", StringComparison.OrdinalIgnoreCase);
      }
    }

    // Token: 0x06001B81 RID: 7041 RVA: 0x003DC170 File Offset: 0x003DC170
    [NullableContext(2)]
    [return: Nullable(1)]
    public static TelemetryConfiguration Reload(string overrideFilePath = null, Assembly entry = null)
    {
      return TelemetryConfiguration.Current = TelemetryConfiguration.Load(overrideFilePath, entry);
    }

    // Token: 0x06001B82 RID: 7042 RVA: 0x003DC180 File Offset: 0x003DC180
    [NullableContext(2)]
    [return: Nullable(1)]
    public unsafe static TelemetryConfiguration Load(string overrideFilePath = null, Assembly entry = null)
    {
      TelemetryConfiguration.<>c__DisplayClass32_0 CS$<>8__locals1;
      CS$<>8__locals1.entry = entry;
      if (CS$<>8__locals1.entry == null)
      {
        CS$<>8__locals1.entry = Assembly.GetEntryAssembly();
      }
      string text = TelemetryConfiguration.<Load>g__Meta|32_0("HeyClicky.PostHogApiKey", ref CS$<>8__locals1);
      string text2 = TelemetryConfiguration.<Load>g__Meta|32_0("HeyClicky.PostHogHost", ref CS$<>8__locals1) ?? "https://us.i.posthog.com";
      string text3 = TelemetryConfiguration.<Load>g__Meta|32_0("HeyClicky.SentryDsn", ref CS$<>8__locals1);
      string text4 = TelemetryConfiguration.<Load>g__Meta|32_0("HeyClicky.UpdateFeedUrl", ref CS$<>8__locals1) ?? "https://heyclicky-releases.invalid/windows";
      string text5 = overrideFilePath ?? AppBundleConfiguration.OverrideFilePath;
      try
      {
        if (File.Exists(text5))
        {
          ReadOnlyMemory<byte> readOnlyMemory = File.ReadAllBytes(text5);
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
            string text6;
            if (TelemetryConfiguration.Read(rootElement, "postHogApiKey", out text6))
            {
              text = text6;
            }
            string text7;
            if (TelemetryConfiguration.Read(rootElement, "postHogHost", out text7) && text7 != null)
            {
              text2 = text7;
            }
            string text8;
            if (TelemetryConfiguration.Read(rootElement, "sentryDsn", out text8))
            {
              text3 = text8;
            }
            string text9;
            if (TelemetryConfiguration.Read(rootElement, "updateFeedUrl", out text9) && text9 != null)
            {
              text4 = text9;
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Config", "endpoints.json telemetry keys ignored: " + ex.Message);
      }
      return new TelemetryConfiguration(text, text2.TrimEnd('/'), text3, text4);
    }

    // Token: 0x06001B83 RID: 7043 RVA: 0x003DC31C File Offset: 0x003DC31C
    private static bool Read(JsonElement root, string name, [Nullable(2)] out string value)
    {
      value = null;
      JsonElement jsonElement;
      if (root.ValueKind != JsonValueKind.Object || !root.TryGetProperty(name, out jsonElement))
      {
        return false;
      }
      if (jsonElement.ValueKind == JsonValueKind.Null)
      {
        return true;
      }
      if (jsonElement.ValueKind != JsonValueKind.String)
      {
        return false;
      }
      value = TelemetryConfiguration.Clean(jsonElement.GetString());
      return true;
    }

    // Token: 0x06001B84 RID: 7044 RVA: 0x003DC36C File Offset: 0x003DC36C
    [NullableContext(2)]
    private static string Clean(string text)
    {
      text = ((text != null) ? text.Trim() : null);
      if (!string.IsNullOrEmpty(text) && (!text.StartsWith("$(") || !text.EndsWith(')')))
      {
        return text;
      }
      return null;
    }

    // Token: 0x06001B85 RID: 7045 RVA: 0x003DC3A0 File Offset: 0x003DC3A0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("TelemetryConfiguration");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001B86 RID: 7046 RVA: 0x003DC3EC File Offset: 0x003DC3EC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("PostHogApiKey = ");
      builder.Append(this.PostHogApiKey);
      builder.Append(", PostHogHost = ");
      builder.Append(this.PostHogHost);
      builder.Append(", SentryDsn = ");
      builder.Append(this.SentryDsn);
      builder.Append(", UpdateFeedUrl = ");
      builder.Append(this.UpdateFeedUrl);
      builder.Append(", HasPostHogKey = ");
      builder.Append(this.HasPostHogKey.ToString());
      builder.Append(", HasSentryDsn = ");
      builder.Append(this.HasSentryDsn.ToString());
      builder.Append(", IsPlaceholderFeed = ");
      builder.Append(this.IsPlaceholderFeed.ToString());
      return true;
    }

    // Token: 0x06001B87 RID: 7047 RVA: 0x003DC4D8 File Offset: 0x003DC4D8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(TelemetryConfiguration left, TelemetryConfiguration right)
    {
      return !(left == right);
    }

    // Token: 0x06001B88 RID: 7048 RVA: 0x003DC4E4 File Offset: 0x003DC4E4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(TelemetryConfiguration left, TelemetryConfiguration right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001B89 RID: 7049 RVA: 0x003DC4F8 File Offset: 0x003DC4F8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PostHogApiKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PostHogHost>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SentryDsn>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UpdateFeedUrl>k__BackingField);
    }

    // Token: 0x06001B8A RID: 7050 RVA: 0x003DC574 File Offset: 0x003DC574
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as TelemetryConfiguration);
    }

    // Token: 0x06001B8B RID: 7051 RVA: 0x003DC584 File Offset: 0x003DC584
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(TelemetryConfiguration other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<PostHogApiKey>k__BackingField, other.<PostHogApiKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PostHogHost>k__BackingField, other.<PostHogHost>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SentryDsn>k__BackingField, other.<SentryDsn>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UpdateFeedUrl>k__BackingField, other.<UpdateFeedUrl>k__BackingField));
    }

    // Token: 0x06001B8D RID: 7053 RVA: 0x003DC618 File Offset: 0x003DC618
    [CompilerGenerated]
    private TelemetryConfiguration(TelemetryConfiguration original)
    {
      this.PostHogApiKey = original.<PostHogApiKey>k__BackingField;
      this.PostHogHost = original.<PostHogHost>k__BackingField;
      this.SentryDsn = original.<SentryDsn>k__BackingField;
      this.UpdateFeedUrl = original.<UpdateFeedUrl>k__BackingField;
    }

    // Token: 0x06001B8E RID: 7054 RVA: 0x003DC650 File Offset: 0x003DC650
    [CompilerGenerated]
    public void Deconstruct([Nullable(2)] out string PostHogApiKey, out string PostHogHost, [Nullable(2)] out string SentryDsn, out string UpdateFeedUrl)
    {
      PostHogApiKey = this.PostHogApiKey;
      PostHogHost = this.PostHogHost;
      SentryDsn = this.SentryDsn;
      UpdateFeedUrl = this.UpdateFeedUrl;
    }

    // Token: 0x06001B90 RID: 7056 RVA: 0x003DC684 File Offset: 0x003DC684
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <Load>g__Meta|32_0(string key, ref TelemetryConfiguration.<>c__DisplayClass32_0 A_1)
    {
      Assembly entry = A_1.entry;
      string text;
      if (entry == null)
      {
        text = null;
      }
      else
      {
        AssemblyMetadataAttribute assemblyMetadataAttribute = entry.GetCustomAttributes<AssemblyMetadataAttribute>().FirstOrDefault<AssemblyMetadataAttribute>((AssemblyMetadataAttribute a) => a.Key == key);
        text = ((assemblyMetadataAttribute != null) ? assemblyMetadataAttribute.Value : null);
      }
      return TelemetryConfiguration.Clean(text);
    }

    // Token: 0x04000BBA RID: 3002
    public const string DefaultPostHogHost = "https://us.i.posthog.com";

    // Token: 0x04000BBB RID: 3003
    public const string PlaceholderUpdateFeedUrl = "https://heyclicky-releases.invalid/windows";
  }
}
