using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace HeyClicky.Agents.Backend
{
  // Token: 0x0200006C RID: 108
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentIntegration : IEquatable<AgentIntegration>
  {
    // Token: 0x060008A1 RID: 2209 RVA: 0x001018F0 File Offset: 0x001018F0
    public AgentIntegration(string Id, string DisplayName, string Description, string Status, int ConnectedCount, int TotalCount, IReadOnlyList<AgentIntegrationToolkit> Toolkits, IReadOnlyList<AgentIntegrationAccount> Accounts)
    {
      this.Id = Id;
      this.DisplayName = DisplayName;
      this.Description = Description;
      this.Status = Status;
      this.ConnectedCount = ConnectedCount;
      this.TotalCount = TotalCount;
      this.Toolkits = Toolkits;
      this.Accounts = Accounts;
      this.UseCases = Array.Empty<string>();
      base..ctor();
    }

    // Token: 0x1700022A RID: 554
    // (get) Token: 0x060008A2 RID: 2210 RVA: 0x0010194C File Offset: 0x0010194C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentIntegration);
      }
    }

    // Token: 0x1700022B RID: 555
    // (get) Token: 0x060008A3 RID: 2211 RVA: 0x00101958 File Offset: 0x00101958
    // (set) Token: 0x060008A4 RID: 2212 RVA: 0x00101960 File Offset: 0x00101960
    public string Id { get; set; }

    // Token: 0x1700022C RID: 556
    // (get) Token: 0x060008A5 RID: 2213 RVA: 0x0010196C File Offset: 0x0010196C
    // (set) Token: 0x060008A6 RID: 2214 RVA: 0x00101974 File Offset: 0x00101974
    public string DisplayName { get; set; }

    // Token: 0x1700022D RID: 557
    // (get) Token: 0x060008A7 RID: 2215 RVA: 0x00101980 File Offset: 0x00101980
    // (set) Token: 0x060008A8 RID: 2216 RVA: 0x00101988 File Offset: 0x00101988
    public string Description { get; set; }

    // Token: 0x1700022E RID: 558
    // (get) Token: 0x060008A9 RID: 2217 RVA: 0x00101994 File Offset: 0x00101994
    // (set) Token: 0x060008AA RID: 2218 RVA: 0x0010199C File Offset: 0x0010199C
    public string Status { get; set; }

    // Token: 0x1700022F RID: 559
    // (get) Token: 0x060008AB RID: 2219 RVA: 0x001019A8 File Offset: 0x001019A8
    // (set) Token: 0x060008AC RID: 2220 RVA: 0x001019B0 File Offset: 0x001019B0
    public int ConnectedCount { get; set; }

    // Token: 0x17000230 RID: 560
    // (get) Token: 0x060008AD RID: 2221 RVA: 0x001019BC File Offset: 0x001019BC
    // (set) Token: 0x060008AE RID: 2222 RVA: 0x001019C4 File Offset: 0x001019C4
    public int TotalCount { get; set; }

    // Token: 0x17000231 RID: 561
    // (get) Token: 0x060008AF RID: 2223 RVA: 0x001019D0 File Offset: 0x001019D0
    // (set) Token: 0x060008B0 RID: 2224 RVA: 0x001019D8 File Offset: 0x001019D8
    public IReadOnlyList<AgentIntegrationToolkit> Toolkits { get; set; }

    // Token: 0x17000232 RID: 562
    // (get) Token: 0x060008B1 RID: 2225 RVA: 0x001019E4 File Offset: 0x001019E4
    // (set) Token: 0x060008B2 RID: 2226 RVA: 0x001019EC File Offset: 0x001019EC
    public IReadOnlyList<AgentIntegrationAccount> Accounts { get; set; }

    // Token: 0x17000233 RID: 563
    // (get) Token: 0x060008B3 RID: 2227 RVA: 0x001019F8 File Offset: 0x001019F8
    public string PrimaryToolkitSlug
    {
      get
      {
        if (this.Toolkits.Count <= 0)
        {
          return this.Id;
        }
        return this.Toolkits[0].Slug;
      }
    }

    // Token: 0x17000234 RID: 564
    // (get) Token: 0x060008B4 RID: 2228 RVA: 0x00101A20 File Offset: 0x00101A20
    [Nullable(2)]
    public string LogoUrl
    {
      [NullableContext(2)]
      get
      {
        return this.Toolkits.Select<AgentIntegrationToolkit, string>((AgentIntegrationToolkit t) => t.Logo).FirstOrDefault<string>((string l) => !string.IsNullOrEmpty(l));
      }
    }

    // Token: 0x17000235 RID: 565
    // (get) Token: 0x060008B5 RID: 2229 RVA: 0x00101A7C File Offset: 0x00101A7C
    public bool IsActiveConnection
    {
      get
      {
        string status = this.Status;
        return status == "connected" || status == "partial";
      }
    }

    // Token: 0x17000236 RID: 566
    // (get) Token: 0x060008B6 RID: 2230 RVA: 0x00101AB4 File Offset: 0x00101AB4
    public IReadOnlyList<AgentIntegrationAccount> ActiveAccounts
    {
      get
      {
        return this.Accounts.Where<AgentIntegrationAccount>((AgentIntegrationAccount a) => a.IsActive).ToList<AgentIntegrationAccount>();
      }
    }

    // Token: 0x17000237 RID: 567
    // (get) Token: 0x060008B7 RID: 2231 RVA: 0x00101AE8 File Offset: 0x00101AE8
    public bool HasReachedActiveAccountLimit
    {
      get
      {
        return this.ActiveAccounts.Count >= 5;
      }
    }

    // Token: 0x17000238 RID: 568
    // (get) Token: 0x060008B8 RID: 2232 RVA: 0x00101AFC File Offset: 0x00101AFC
    public bool IsLocalIntegration
    {
      get
      {
        return this.Id == "obsidian";
      }
    }

    // Token: 0x17000239 RID: 569
    // (get) Token: 0x060008B9 RID: 2233 RVA: 0x00101B10 File Offset: 0x00101B10
    // (set) Token: 0x060008BA RID: 2234 RVA: 0x00101B18 File Offset: 0x00101B18
    public IReadOnlyList<string> UseCases { get; set; }

    // Token: 0x060008BB RID: 2235 RVA: 0x00101B24 File Offset: 0x00101B24
    public static IReadOnlyList<string> NormalizedUseCases(IEnumerable<string> values)
    {
      HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
      List<string> list = new List<string>();
      foreach (string text in values)
      {
        string text2 = Regex.Replace(text, "\\s+", " ").Trim().Trim(new char[] { '.', '。' });
        if (text2.Length > 0 && hashSet.Add(text2))
        {
          list.Add(text2);
        }
      }
      return list;
    }

    // Token: 0x060008BC RID: 2236 RVA: 0x00101BBC File Offset: 0x00101BBC
    [return: Nullable(2)]
    internal static AgentIntegration Parse(JsonObject o)
    {
      JsonNode jsonNode = o["id"];
      string text = ((jsonNode != null) ? jsonNode.ToString() : null);
      if (string.IsNullOrEmpty(text))
      {
        return null;
      }
      JsonArray jsonArray;
      if ((jsonArray = o["toolkits"] as JsonArray) == null)
      {
        jsonArray = new JsonArray(null);
      }
      List<AgentIntegrationToolkit> list = (from t in jsonArray.OfType<JsonObject>().Select<JsonObject, AgentIntegrationToolkit>(delegate(JsonObject t)
        {
          JsonNode jsonNode5 = t["slug"];
          string text5 = ((jsonNode5 != null) ? jsonNode5.ToString() : null) ?? "";
          JsonNode jsonNode6 = t["name"];
          string text6 = ((jsonNode6 != null) ? jsonNode6.ToString() : null) ?? "";
          JsonNode jsonNode7 = t["logo"];
          return new AgentIntegrationToolkit(text5, text6, (jsonNode7 != null) ? jsonNode7.ToString() : null, AgentIntegration.<Parse>g__Bool|53_1(t["is_connected"]));
        })
        where t.Slug.Length > 0
        select t).ToList<AgentIntegrationToolkit>();
      JsonArray jsonArray2;
      if ((jsonArray2 = o["accounts"] as JsonArray) == null)
      {
        jsonArray2 = new JsonArray(null);
      }
      List<AgentIntegrationAccount> list2 = (from a in jsonArray2.OfType<JsonObject>().Select<JsonObject, AgentIntegrationAccount>(delegate(JsonObject a)
        {
          JsonNode jsonNode8 = a["connected_account_id"];
          string text7 = ((jsonNode8 != null) ? jsonNode8.ToString() : null) ?? "";
          JsonNode jsonNode9 = a["account_name"];
          string text8 = ((jsonNode9 != null) ? jsonNode9.ToString() : null);
          JsonNode jsonNode10 = a["label"];
          string text9 = ((jsonNode10 != null) ? jsonNode10.ToString() : null);
          JsonNode jsonNode11 = a["avatar_url"];
          string text10 = ((jsonNode11 != null) ? jsonNode11.ToString() : null);
          JsonNode jsonNode12 = a["account_name_kind"];
          string text11 = ((jsonNode12 != null) ? jsonNode12.ToString() : null);
          JsonNode jsonNode13 = a["status"];
          string text12 = ((jsonNode13 != null) ? jsonNode13.ToString() : null) ?? "connected";
          JsonNode jsonNode14 = a["connected_at"];
          return new AgentIntegrationAccount(text7, text8, text9, text10, text11, text12, (jsonNode14 != null) ? jsonNode14.ToString() : null);
        })
        where a.ConnectedAccountId.Length > 0
        select a).ToList<AgentIntegrationAccount>();
      string text2 = text;
      JsonNode jsonNode2 = o["display_name"];
      string text3 = ((jsonNode2 != null) ? jsonNode2.ToString() : null) ?? text;
      JsonNode jsonNode3 = o["description"];
      string text4 = ((jsonNode3 != null) ? jsonNode3.ToString() : null) ?? "";
      JsonNode jsonNode4 = o["status"];
      AgentIntegration agentIntegration = new AgentIntegration(text2, text3, text4, ((jsonNode4 != null) ? jsonNode4.ToString() : null) ?? "not_connected", AgentIntegration.<Parse>g__Int|53_0(o["connected_count"]), AgentIntegration.<Parse>g__Int|53_0(o["total_count"]), list, list2);
      JsonArray jsonArray3;
      if ((jsonArray3 = o["use_cases"] as JsonArray) == null)
      {
        jsonArray3 = new JsonArray(null);
      }
      agentIntegration.UseCases = AgentIntegration.NormalizedUseCases(jsonArray3.Select<JsonNode, string>(delegate(JsonNode u)
      {
        JsonValue jsonValue = u as JsonValue;
        string text13;
        if (jsonValue == null || !jsonValue.TryGetValue<string>(out text13))
        {
          return null;
        }
        return text13;
      }).OfType<string>());
      return agentIntegration;
    }

    // Token: 0x060008BD RID: 2237 RVA: 0x00101DB0 File Offset: 0x00101DB0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentIntegration");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060008BE RID: 2238 RVA: 0x00101DFC File Offset: 0x00101DFC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", DisplayName = ");
      builder.Append(this.DisplayName);
      builder.Append(", Description = ");
      builder.Append(this.Description);
      builder.Append(", Status = ");
      builder.Append(this.Status);
      builder.Append(", ConnectedCount = ");
      builder.Append(this.ConnectedCount.ToString());
      builder.Append(", TotalCount = ");
      builder.Append(this.TotalCount.ToString());
      builder.Append(", Toolkits = ");
      builder.Append(this.Toolkits);
      builder.Append(", Accounts = ");
      builder.Append(this.Accounts);
      builder.Append(", PrimaryToolkitSlug = ");
      builder.Append(this.PrimaryToolkitSlug);
      builder.Append(", LogoUrl = ");
      builder.Append(this.LogoUrl);
      builder.Append(", IsActiveConnection = ");
      builder.Append(this.IsActiveConnection.ToString());
      builder.Append(", ActiveAccounts = ");
      builder.Append(this.ActiveAccounts);
      builder.Append(", HasReachedActiveAccountLimit = ");
      builder.Append(this.HasReachedActiveAccountLimit.ToString());
      builder.Append(", IsLocalIntegration = ");
      builder.Append(this.IsLocalIntegration.ToString());
      builder.Append(", UseCases = ");
      builder.Append(this.UseCases);
      return true;
    }

    // Token: 0x060008BF RID: 2239 RVA: 0x00101FCC File Offset: 0x00101FCC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentIntegration left, AgentIntegration right)
    {
      return !(left == right);
    }

    // Token: 0x060008C0 RID: 2240 RVA: 0x00101FD8 File Offset: 0x00101FD8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentIntegration left, AgentIntegration right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060008C1 RID: 2241 RVA: 0x00101FEC File Offset: 0x00101FEC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ConnectedCount>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<TotalCount>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<AgentIntegrationToolkit>>.Default.GetHashCode(this.<Toolkits>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<AgentIntegrationAccount>>.Default.GetHashCode(this.<Accounts>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<UseCases>k__BackingField);
    }

    // Token: 0x060008C2 RID: 2242 RVA: 0x001020D8 File Offset: 0x001020D8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentIntegration);
    }

    // Token: 0x060008C3 RID: 2243 RVA: 0x001020E8 File Offset: 0x001020E8
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentIntegration other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ConnectedCount>k__BackingField, other.<ConnectedCount>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<TotalCount>k__BackingField, other.<TotalCount>k__BackingField) && EqualityComparer<IReadOnlyList<AgentIntegrationToolkit>>.Default.Equals(this.<Toolkits>k__BackingField, other.<Toolkits>k__BackingField) && EqualityComparer<IReadOnlyList<AgentIntegrationAccount>>.Default.Equals(this.<Accounts>k__BackingField, other.<Accounts>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<UseCases>k__BackingField, other.<UseCases>k__BackingField));
    }

    // Token: 0x060008C5 RID: 2245 RVA: 0x00102204 File Offset: 0x00102204
    [CompilerGenerated]
    private AgentIntegration(AgentIntegration original)
    {
      this.Id = original.<Id>k__BackingField;
      this.DisplayName = original.<DisplayName>k__BackingField;
      this.Description = original.<Description>k__BackingField;
      this.Status = original.<Status>k__BackingField;
      this.ConnectedCount = original.<ConnectedCount>k__BackingField;
      this.TotalCount = original.<TotalCount>k__BackingField;
      this.Toolkits = original.<Toolkits>k__BackingField;
      this.Accounts = original.<Accounts>k__BackingField;
      this.UseCases = original.<UseCases>k__BackingField;
    }

    // Token: 0x060008C6 RID: 2246 RVA: 0x00102284 File Offset: 0x00102284
    [CompilerGenerated]
    public void Deconstruct(out string Id, out string DisplayName, out string Description, out string Status, out int ConnectedCount, out int TotalCount, out IReadOnlyList<AgentIntegrationToolkit> Toolkits, out IReadOnlyList<AgentIntegrationAccount> Accounts)
    {
      Id = this.Id;
      DisplayName = this.DisplayName;
      Description = this.Description;
      Status = this.Status;
      ConnectedCount = this.ConnectedCount;
      TotalCount = this.TotalCount;
      Toolkits = this.Toolkits;
      Accounts = this.Accounts;
    }

    // Token: 0x060008C7 RID: 2247 RVA: 0x001022D8 File Offset: 0x001022D8
    [NullableContext(2)]
    [CompilerGenerated]
    internal static int <Parse>g__Int|53_0(JsonNode n)
    {
      JsonValue jsonValue = n as JsonValue;
      int num;
      if (jsonValue != null && jsonValue.TryGetValue<int>(out num))
      {
        return num;
      }
      int num2;
      if (!int.TryParse((n != null) ? n.ToString() : null, out num2))
      {
        return 0;
      }
      return num2;
    }

    // Token: 0x060008C8 RID: 2248 RVA: 0x00102314 File Offset: 0x00102314
    [NullableContext(2)]
    [CompilerGenerated]
    internal static bool <Parse>g__Bool|53_1(JsonNode n)
    {
      JsonValue jsonValue = n as JsonValue;
      bool flag;
      return jsonValue != null && jsonValue.TryGetValue<bool>(out flag) && flag;
    }

    // Token: 0x0400031D RID: 797
    public const int MaximumActiveAccountsPerApp = 5;
  }
}
