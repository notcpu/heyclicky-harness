using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000086 RID: 134
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BrainUserContext : IEquatable<BrainUserContext>
  {
    // Token: 0x170001C4 RID: 452
    // (get) Token: 0x0600080C RID: 2060 RVA: 0x003685EC File Offset: 0x003685EC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(BrainUserContext);
      }
    }

    // Token: 0x170001C5 RID: 453
    // (get) Token: 0x0600080D RID: 2061 RVA: 0x003685F8 File Offset: 0x003685F8
    // (set) Token: 0x0600080E RID: 2062 RVA: 0x00368600 File Offset: 0x00368600
    public string RosterNote { get; set; }

    // Token: 0x170001C6 RID: 454
    // (get) Token: 0x0600080F RID: 2063 RVA: 0x0036860C File Offset: 0x0036860C
    // (set) Token: 0x06000810 RID: 2064 RVA: 0x00368614 File Offset: 0x00368614
    public IReadOnlyList<string> ConnectedApps { get; set; }

    // Token: 0x170001C7 RID: 455
    // (get) Token: 0x06000811 RID: 2065 RVA: 0x00368620 File Offset: 0x00368620
    // (set) Token: 0x06000812 RID: 2066 RVA: 0x00368628 File Offset: 0x00368628
    public IReadOnlyList<string> NeedsReconnect { get; set; }

    // Token: 0x170001C8 RID: 456
    // (get) Token: 0x06000813 RID: 2067 RVA: 0x00368634 File Offset: 0x00368634
    // (set) Token: 0x06000814 RID: 2068 RVA: 0x0036863C File Offset: 0x0036863C
    public IReadOnlyList<CustomConnectorDescription> CustomConnectors { get; set; }

    // Token: 0x170001C9 RID: 457
    // (get) Token: 0x06000815 RID: 2069 RVA: 0x00368648 File Offset: 0x00368648
    // (set) Token: 0x06000816 RID: 2070 RVA: 0x00368650 File Offset: 0x00368650
    public IReadOnlyList<IntegrationAccounts> ConnectedAccounts { get; set; }

    // Token: 0x06000817 RID: 2071 RVA: 0x0036865C File Offset: 0x0036865C
    public static BrainUserContext FromIntegrations(JsonObject body)
    {
      List<string> list = new List<string>();
      List<string> list2 = new List<string>();
      List<IntegrationAccounts> list3 = new List<IntegrationAccounts>();
      JsonArray jsonArray;
      if ((jsonArray = body["integrations"] as JsonArray) == null)
      {
        jsonArray = new JsonArray(null);
      }
      foreach (JsonObject jsonObject in jsonArray.OfType<JsonObject>())
      {
        string text = BrainUserContext.Str(jsonObject["id"]);
        string text2 = BrainUserContext.Str(jsonObject["display_name"]) ?? text;
        string text3 = BrainUserContext.Str(jsonObject["status"]);
        if (text != null && text2 != null)
        {
          bool flag = text3 == "connected" || text3 == "partial";
          if (flag)
          {
            list.Add(text);
          }
          else if (text3 == "needs_reconnect")
          {
            list2.Add(text2);
          }
          JsonArray jsonArray2;
          if ((jsonArray2 = jsonObject["accounts"] as JsonArray) == null)
          {
            jsonArray2 = new JsonArray(null);
          }
          List<IntegrationAccount> list4 = (from a in jsonArray2.OfType<JsonObject>().Where<JsonObject>(delegate(JsonObject a)
            {
              string text4 = BrainUserContext.Str(a["status"]);
              bool flag2 = text4 == null || text4 == "connected";
              return flag2 && BrainUserContext.Str(a["connected_account_id"]) != null;
            })
            select new IntegrationAccount(BrainUserContext.Str(a["connected_account_id"]), BrainUserContext.Str(a["account_name"]), BrainUserContext.Str(a["label"]))).ToList<IntegrationAccount>();
          if (list4.Count > 1)
          {
            list3.Add(new IntegrationAccounts(text2, list4));
          }
        }
      }
      return new BrainUserContext
      {
        ConnectedApps = list,
        NeedsReconnect = list2.Order<string>(StringComparer.OrdinalIgnoreCase).ToList<string>(),
        ConnectedAccounts = list3
      };
    }

    // Token: 0x06000818 RID: 2072 RVA: 0x00368838 File Offset: 0x00368838
    [NullableContext(2)]
    private static string Str(JsonNode node)
    {
      JsonValue jsonValue = node as JsonValue;
      string text;
      if (jsonValue == null || !jsonValue.TryGetValue<string>(out text) || string.IsNullOrWhiteSpace(text))
      {
        return null;
      }
      return text.Trim();
    }

    // Token: 0x06000819 RID: 2073 RVA: 0x0036886C File Offset: 0x0036886C
    internal void Write(Utf8JsonWriter writer, string timeZone)
    {
      writer.WriteStartObject("user_context");
      writer.WriteStartArray("needs_reconnect");
      foreach (string text in this.NeedsReconnect)
      {
        writer.WriteStringValue(text);
      }
      writer.WriteEndArray();
      if (this.CustomConnectors.Count > 0)
      {
        writer.WriteStartArray("custom_connectors");
        foreach (CustomConnectorDescription customConnectorDescription in this.CustomConnectors)
        {
          writer.WriteStartObject();
          writer.WriteString("name", customConnectorDescription.Name);
          writer.WriteString("description", customConnectorDescription.Description);
          writer.WriteEndObject();
        }
        writer.WriteEndArray();
      }
      if (this.ConnectedAccounts.Count > 0)
      {
        writer.WriteStartArray("connected_accounts");
        foreach (IntegrationAccounts integrationAccounts in this.ConnectedAccounts)
        {
          writer.WriteStartObject();
          writer.WriteString("integration", integrationAccounts.Integration);
          writer.WriteStartArray("accounts");
          foreach (IntegrationAccount integrationAccount in integrationAccounts.Accounts)
          {
            writer.WriteStartObject();
            writer.WriteString("connected_account_id", integrationAccount.ConnectedAccountId);
            string accountName = integrationAccount.AccountName;
            if (accountName != null)
            {
              writer.WriteString("account_name", accountName);
            }
            string label = integrationAccount.Label;
            if (label != null)
            {
              writer.WriteString("label", label);
            }
            writer.WriteEndObject();
          }
          writer.WriteEndArray();
          writer.WriteEndObject();
        }
        writer.WriteEndArray();
      }
      writer.WriteString("roster_note", this.RosterNote);
      writer.WriteStartArray("connected_apps");
      foreach (string text2 in this.ConnectedApps)
      {
        writer.WriteStringValue(text2);
      }
      writer.WriteEndArray();
      writer.WriteString("time_zone", timeZone);
      writer.WriteEndObject();
    }

    // Token: 0x0600081A RID: 2074 RVA: 0x00368AEC File Offset: 0x00368AEC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("BrainUserContext");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600081B RID: 2075 RVA: 0x00368B38 File Offset: 0x00368B38
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("RosterNote = ");
      builder.Append(this.RosterNote);
      builder.Append(", ConnectedApps = ");
      builder.Append(this.ConnectedApps);
      builder.Append(", NeedsReconnect = ");
      builder.Append(this.NeedsReconnect);
      builder.Append(", CustomConnectors = ");
      builder.Append(this.CustomConnectors);
      builder.Append(", ConnectedAccounts = ");
      builder.Append(this.ConnectedAccounts);
      return true;
    }

    // Token: 0x0600081C RID: 2076 RVA: 0x00368BC8 File Offset: 0x00368BC8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(BrainUserContext left, BrainUserContext right)
    {
      return !(left == right);
    }

    // Token: 0x0600081D RID: 2077 RVA: 0x00368BD4 File Offset: 0x00368BD4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(BrainUserContext left, BrainUserContext right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600081E RID: 2078 RVA: 0x00368BE8 File Offset: 0x00368BE8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RosterNote>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ConnectedApps>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<NeedsReconnect>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<CustomConnectorDescription>>.Default.GetHashCode(this.<CustomConnectors>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<IntegrationAccounts>>.Default.GetHashCode(this.<ConnectedAccounts>k__BackingField);
    }

    // Token: 0x0600081F RID: 2079 RVA: 0x00368C78 File Offset: 0x00368C78
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as BrainUserContext);
    }

    // Token: 0x06000820 RID: 2080 RVA: 0x00368C88 File Offset: 0x00368C88
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(BrainUserContext other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<RosterNote>k__BackingField, other.<RosterNote>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ConnectedApps>k__BackingField, other.<ConnectedApps>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<NeedsReconnect>k__BackingField, other.<NeedsReconnect>k__BackingField) && EqualityComparer<IReadOnlyList<CustomConnectorDescription>>.Default.Equals(this.<CustomConnectors>k__BackingField, other.<CustomConnectors>k__BackingField) && EqualityComparer<IReadOnlyList<IntegrationAccounts>>.Default.Equals(this.<ConnectedAccounts>k__BackingField, other.<ConnectedAccounts>k__BackingField));
    }

    // Token: 0x06000822 RID: 2082 RVA: 0x00368D38 File Offset: 0x00368D38
    [CompilerGenerated]
    private BrainUserContext(BrainUserContext original)
    {
      this.RosterNote = original.<RosterNote>k__BackingField;
      this.ConnectedApps = original.<ConnectedApps>k__BackingField;
      this.NeedsReconnect = original.<NeedsReconnect>k__BackingField;
      this.CustomConnectors = original.<CustomConnectors>k__BackingField;
      this.ConnectedAccounts = original.<ConnectedAccounts>k__BackingField;
    }

    // Token: 0x06000823 RID: 2083 RVA: 0x00368D88 File Offset: 0x00368D88
    public BrainUserContext()
    {
      this.RosterNote = "";
      this.ConnectedApps = Array.Empty<string>();
      this.NeedsReconnect = Array.Empty<string>();
      this.CustomConnectors = Array.Empty<CustomConnectorDescription>();
      this.ConnectedAccounts = Array.Empty<IntegrationAccounts>();
      base..ctor();
    }

    // Token: 0x040003FB RID: 1019
    public static readonly BrainUserContext Empty = new BrainUserContext();
  }
}
