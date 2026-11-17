using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000B5 RID: 181
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentIntegrationInfo : IEquatable<AgentIntegrationInfo>
  {
    // Token: 0x06000BE3 RID: 3043 RVA: 0x0037B5A8 File Offset: 0x0037B5A8
    public AgentIntegrationInfo(string Id, string DisplayName, IReadOnlyList<string> UseCases, string Status, int ConnectedCount, IReadOnlyList<string> ToolkitSlugs, [Nullable(2)] string LogoUrl)
    {
      this.Id = Id;
      this.DisplayName = DisplayName;
      this.UseCases = UseCases;
      this.Status = Status;
      this.ConnectedCount = ConnectedCount;
      this.ToolkitSlugs = ToolkitSlugs;
      this.LogoUrl = LogoUrl;
      base..ctor();
    }

    // Token: 0x17000290 RID: 656
    // (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0037B5E8 File Offset: 0x0037B5E8
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentIntegrationInfo);
      }
    }

    // Token: 0x17000291 RID: 657
    // (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0037B5F4 File Offset: 0x0037B5F4
    // (set) Token: 0x06000BE6 RID: 3046 RVA: 0x0037B5FC File Offset: 0x0037B5FC
    public string Id { get; set; }

    // Token: 0x17000292 RID: 658
    // (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0037B608 File Offset: 0x0037B608
    // (set) Token: 0x06000BE8 RID: 3048 RVA: 0x0037B610 File Offset: 0x0037B610
    public string DisplayName { get; set; }

    // Token: 0x17000293 RID: 659
    // (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0037B61C File Offset: 0x0037B61C
    // (set) Token: 0x06000BEA RID: 3050 RVA: 0x0037B624 File Offset: 0x0037B624
    public IReadOnlyList<string> UseCases { get; set; }

    // Token: 0x17000294 RID: 660
    // (get) Token: 0x06000BEB RID: 3051 RVA: 0x0037B630 File Offset: 0x0037B630
    // (set) Token: 0x06000BEC RID: 3052 RVA: 0x0037B638 File Offset: 0x0037B638
    public string Status { get; set; }

    // Token: 0x17000295 RID: 661
    // (get) Token: 0x06000BED RID: 3053 RVA: 0x0037B644 File Offset: 0x0037B644
    // (set) Token: 0x06000BEE RID: 3054 RVA: 0x0037B64C File Offset: 0x0037B64C
    public int ConnectedCount { get; set; }

    // Token: 0x17000296 RID: 662
    // (get) Token: 0x06000BEF RID: 3055 RVA: 0x0037B658 File Offset: 0x0037B658
    // (set) Token: 0x06000BF0 RID: 3056 RVA: 0x0037B660 File Offset: 0x0037B660
    public IReadOnlyList<string> ToolkitSlugs { get; set; }

    // Token: 0x17000297 RID: 663
    // (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0037B66C File Offset: 0x0037B66C
    // (set) Token: 0x06000BF2 RID: 3058 RVA: 0x0037B674 File Offset: 0x0037B674
    [Nullable(2)]
    public string LogoUrl
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000298 RID: 664
    // (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0037B680 File Offset: 0x0037B680
    public string PrimaryToolkitSlug
    {
      get
      {
        if (this.ToolkitSlugs.Count <= 0)
        {
          return this.Id;
        }
        return this.ToolkitSlugs[0];
      }
    }

    // Token: 0x17000299 RID: 665
    // (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0037B6A4 File Offset: 0x0037B6A4
    public bool IsActiveConnection
    {
      get
      {
        string status = this.Status;
        return status == "connected" || status == "partial";
      }
    }

    // Token: 0x1700029A RID: 666
    // (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0037B6DC File Offset: 0x0037B6DC
    public bool IsEligibleForConnectionSuggestion
    {
      get
      {
        bool flag = this.ConnectedCount == 0;
        if (flag)
        {
          string status = this.Status;
          bool flag2 = status == "not_connected" || status == "needs_reconnect";
          flag = flag2;
        }
        return flag;
      }
    }

    // Token: 0x1700029B RID: 667
    // (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0037B720 File Offset: 0x0037B720
    public bool IsLocal
    {
      get
      {
        return this.Id.StartsWith("local", StringComparison.OrdinalIgnoreCase) || this.Id.Equals("obsidian", StringComparison.OrdinalIgnoreCase);
      }
    }

    // Token: 0x06000BF7 RID: 3063 RVA: 0x0037B748 File Offset: 0x0037B748
    public static IReadOnlyList<AgentIntegrationInfo> ParseList(byte[] bytes)
    {
      List<AgentIntegrationInfo> list = new List<AgentIntegrationInfo>();
      JsonNode jsonNode = JsonNode.Parse(bytes, null, default(JsonDocumentOptions));
      JsonArray jsonArray = ((jsonNode != null) ? jsonNode["integrations"] : null) as JsonArray;
      if (jsonArray == null)
      {
        return list;
      }
      foreach (JsonObject jsonObject in jsonArray.OfType<JsonObject>())
      {
        string text = AgentIntegrationInfo.<ParseList>g__Str|39_0(jsonObject, "id");
        if (text != null)
        {
          JsonArray jsonArray2;
          if ((jsonArray2 = jsonObject["toolkits"] as JsonArray) == null)
          {
            jsonArray2 = new JsonArray(null);
          }
          List<JsonObject> list2 = jsonArray2.OfType<JsonObject>().ToList<JsonObject>();
          JsonArray jsonArray3;
          if ((jsonArray3 = jsonObject["use_cases"] as JsonArray) == null)
          {
            jsonArray3 = new JsonArray(null);
          }
          List<string> list3 = (from s in jsonArray3.Select<JsonNode, string>(delegate(JsonNode u)
            {
              JsonValue jsonValue2 = u as JsonValue;
              string text5;
              if (jsonValue2 == null || !jsonValue2.TryGetValue<string>(out text5))
              {
                return null;
              }
              return text5.Trim();
            })
            where !string.IsNullOrEmpty(s)
            select s).Cast<string>().ToList<string>();
          List<AgentIntegrationInfo> list4 = list;
          string text2 = text;
          string text3 = AgentIntegrationInfo.<ParseList>g__Str|39_0(jsonObject, "display_name") ?? text;
          IReadOnlyList<string> readOnlyList = list3;
          string text4 = AgentIntegrationInfo.<ParseList>g__Str|39_0(jsonObject, "status") ?? "not_connected";
          JsonValue jsonValue = jsonObject["connected_count"] as JsonValue;
          int num;
          list4.Add(new AgentIntegrationInfo(text2, text3, readOnlyList, text4, (jsonValue != null && jsonValue.TryGetValue<int>(out num)) ? num : 0, (from t in list2
            select AgentIntegrationInfo.<ParseList>g__Str|39_0(t, "slug") into s
            where s != null
            select s).Cast<string>().ToList<string>(), list2.Select<JsonObject, string>((JsonObject t) => AgentIntegrationInfo.<ParseList>g__Str|39_0(t, "logo")).FirstOrDefault<string>((string l) => !string.IsNullOrWhiteSpace(l))));
        }
      }
      return list;
    }

    // Token: 0x06000BF8 RID: 3064 RVA: 0x0037B99C File Offset: 0x0037B99C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentIntegrationInfo");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000BF9 RID: 3065 RVA: 0x0037B9E8 File Offset: 0x0037B9E8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", DisplayName = ");
      builder.Append(this.DisplayName);
      builder.Append(", UseCases = ");
      builder.Append(this.UseCases);
      builder.Append(", Status = ");
      builder.Append(this.Status);
      builder.Append(", ConnectedCount = ");
      builder.Append(this.ConnectedCount.ToString());
      builder.Append(", ToolkitSlugs = ");
      builder.Append(this.ToolkitSlugs);
      builder.Append(", LogoUrl = ");
      builder.Append(this.LogoUrl);
      builder.Append(", PrimaryToolkitSlug = ");
      builder.Append(this.PrimaryToolkitSlug);
      builder.Append(", IsActiveConnection = ");
      builder.Append(this.IsActiveConnection.ToString());
      builder.Append(", IsEligibleForConnectionSuggestion = ");
      builder.Append(this.IsEligibleForConnectionSuggestion.ToString());
      builder.Append(", IsLocal = ");
      builder.Append(this.IsLocal.ToString());
      return true;
    }

    // Token: 0x06000BFA RID: 3066 RVA: 0x0037BB48 File Offset: 0x0037BB48
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentIntegrationInfo left, AgentIntegrationInfo right)
    {
      return !(left == right);
    }

    // Token: 0x06000BFB RID: 3067 RVA: 0x0037BB54 File Offset: 0x0037BB54
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentIntegrationInfo left, AgentIntegrationInfo right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000BFC RID: 3068 RVA: 0x0037BB68 File Offset: 0x0037BB68
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<UseCases>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ConnectedCount>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ToolkitSlugs>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LogoUrl>k__BackingField);
    }

    // Token: 0x06000BFD RID: 3069 RVA: 0x0037BC28 File Offset: 0x0037BC28
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentIntegrationInfo);
    }

    // Token: 0x06000BFE RID: 3070 RVA: 0x0037BC38 File Offset: 0x0037BC38
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentIntegrationInfo other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<UseCases>k__BackingField, other.<UseCases>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ConnectedCount>k__BackingField, other.<ConnectedCount>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ToolkitSlugs>k__BackingField, other.<ToolkitSlugs>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LogoUrl>k__BackingField, other.<LogoUrl>k__BackingField));
    }

    // Token: 0x06000C00 RID: 3072 RVA: 0x0037BD20 File Offset: 0x0037BD20
    [CompilerGenerated]
    private AgentIntegrationInfo(AgentIntegrationInfo original)
    {
      this.Id = original.<Id>k__BackingField;
      this.DisplayName = original.<DisplayName>k__BackingField;
      this.UseCases = original.<UseCases>k__BackingField;
      this.Status = original.<Status>k__BackingField;
      this.ConnectedCount = original.<ConnectedCount>k__BackingField;
      this.ToolkitSlugs = original.<ToolkitSlugs>k__BackingField;
      this.LogoUrl = original.<LogoUrl>k__BackingField;
    }

    // Token: 0x06000C01 RID: 3073 RVA: 0x0037BD88 File Offset: 0x0037BD88
    [CompilerGenerated]
    public void Deconstruct(out string Id, out string DisplayName, out IReadOnlyList<string> UseCases, out string Status, out int ConnectedCount, out IReadOnlyList<string> ToolkitSlugs, [Nullable(2)] out string LogoUrl)
    {
      Id = this.Id;
      DisplayName = this.DisplayName;
      UseCases = this.UseCases;
      Status = this.Status;
      ConnectedCount = this.ConnectedCount;
      ToolkitSlugs = this.ToolkitSlugs;
      LogoUrl = this.LogoUrl;
    }

    // Token: 0x06000C02 RID: 3074 RVA: 0x0037BDC8 File Offset: 0x0037BDC8
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <ParseList>g__Str|39_0(JsonObject o, string k)
    {
      JsonValue jsonValue = o[k] as JsonValue;
      string text;
      if (jsonValue == null || !jsonValue.TryGetValue<string>(out text))
      {
        return null;
      }
      return text;
    }
  }
}
