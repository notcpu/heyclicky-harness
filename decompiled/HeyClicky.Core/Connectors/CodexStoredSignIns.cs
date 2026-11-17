using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Connectors
{
  // Token: 0x020001D0 RID: 464
  [NullableContext(1)]
  [Nullable(0)]
  public static class CodexStoredSignIns
  {
    // Token: 0x06001AC4 RID: 6852 RVA: 0x003D8C94 File Offset: 0x003D8C94
    [return: Nullable(2)]
    public static JsonObject ReadFile(string codexHome)
    {
      JsonObject jsonObject;
      try
      {
        string text = Path.Combine(codexHome, ".credentials.json");
        jsonObject = (File.Exists(text) ? (JsonNode.Parse(File.ReadAllText(text), null, default(JsonDocumentOptions)) as JsonObject) : null);
      }
      catch (Exception ex)
      {
        Log.Warn("connectors", "codex credentials unreadable: " + ex.Message);
        jsonObject = null;
      }
      return jsonObject;
    }

    // Token: 0x06001AC5 RID: 6853 RVA: 0x003D8D10 File Offset: 0x003D8D10
    [return: Nullable(2)]
    public static CodexStoredSignIn Find(JsonObject entries, Uri serverUrl, string serverName, DateTimeOffset now)
    {
      CodexStoredSignIn codexStoredSignIn = null;
      foreach (JsonObject jsonObject in CodexStoredSignIns.Candidates(entries, serverUrl, serverName))
      {
        JsonNode jsonNode = jsonObject["access_token"];
        string text = ((jsonNode != null) ? jsonNode.ToString() : null);
        string text2 = ((text != null && text.Length > 0) ? text : null);
        JsonNode jsonNode2 = jsonObject["refresh_token"];
        string text3 = ((jsonNode2 != null) ? jsonNode2.ToString() : null);
        string text4 = ((text3 != null && text3.Length > 0) ? text3 : null);
        JsonNode jsonNode3 = jsonObject["client_id"];
        string text5 = ((jsonNode3 != null) ? jsonNode3.ToString() : null);
        string text6 = ((text5 != null && text5.Length > 0) ? text5 : null);
        JsonNode jsonNode4 = jsonObject["client_secret"];
        string text7 = ((jsonNode4 != null) ? jsonNode4.ToString() : null);
        string text8 = ((text7 != null && text7.Length > 0) ? text7 : null);
        double? num = CodexStoredSignIns.Number(jsonObject["expires_at"]);
        if (num == null)
        {
          goto IL_00FF;
        }
        double valueOrDefault = num.GetValueOrDefault();
        if (valueOrDefault <= 0.0)
        {
          goto IL_00FF;
        }
        DateTimeOffset? dateTimeOffset = new DateTimeOffset?((valueOrDefault > 1000000000000.0) ? DateTimeOffset.FromUnixTimeMilliseconds((long)valueOrDefault) : DateTimeOffset.FromUnixTimeSeconds((long)valueOrDefault));
        IL_012F:
        DateTimeOffset? dateTimeOffset2 = dateTimeOffset;
        if (text2 != null && (dateTimeOffset2 == null || dateTimeOffset2 > now))
        {
          return new CodexStoredSignIn(text2, text4, dateTimeOffset2, text6, text8);
        }
        if (codexStoredSignIn == null && text4 != null && text6 != null)
        {
          codexStoredSignIn = new CodexStoredSignIn(text2 ?? "", text4, new DateTimeOffset?(dateTimeOffset2.GetValueOrDefault(now)), text6, text8);
          continue;
        }
        continue;
        IL_00FF:
        dateTimeOffset = null;
        goto IL_012F;
      }
      return codexStoredSignIn;
    }

    // Token: 0x06001AC6 RID: 6854 RVA: 0x003D8EFC File Offset: 0x003D8EFC
    internal static List<JsonObject> Candidates(JsonObject entries, Uri serverUrl, string serverName)
    {
      string wanted = CodexStoredSignIns.NormalizedUrl(serverUrl.AbsoluteUri);
      List<JsonObject> list = (from e in entries.OrderBy<KeyValuePair<string, JsonNode>, string>((KeyValuePair<string, JsonNode> e) => e.Key, StringComparer.Ordinal)
        select e.Value).OfType<JsonObject>().Where<JsonObject>(delegate(JsonObject r)
      {
        JsonNode jsonNode = r["server_url"];
        string text = ((jsonNode != null) ? jsonNode.ToString() : null);
        return text != null && CodexStoredSignIns.NormalizedUrl(text) == wanted;
      }).ToList<JsonObject>();
      List<JsonObject> list2 = list.Where<JsonObject>(delegate(JsonObject r)
      {
        JsonNode jsonNode2 = r["server_name"];
        string text2 = ((jsonNode2 != null) ? jsonNode2.ToString() : null);
        return text2 != null && (text2 == serverName || text2 == "local:" + serverName);
      }).ToList<JsonObject>();
      if (list2.Count > 0)
      {
        return list2;
      }
      if (!list.All<JsonObject>(delegate(JsonObject r)
      {
        JsonNode jsonNode3 = r["server_name"];
        return string.IsNullOrWhiteSpace((jsonNode3 != null) ? jsonNode3.ToString() : null);
      }))
      {
        return new List<JsonObject>();
      }
      return list;
    }

    // Token: 0x06001AC7 RID: 6855 RVA: 0x003D8FE4 File Offset: 0x003D8FE4
    [NullableContext(2)]
    private static double? Number(JsonNode node)
    {
      JsonValue jsonValue = node as JsonValue;
      if (jsonValue == null)
      {
        return null;
      }
      double num;
      if (jsonValue.TryGetValue<double>(out num))
      {
        return new double?(num);
      }
      long num2;
      if (jsonValue.TryGetValue<long>(out num2))
      {
        return new double?((double)num2);
      }
      int num3;
      if (!jsonValue.TryGetValue<int>(out num3))
      {
        return null;
      }
      return new double?((double)num3);
    }

    // Token: 0x06001AC8 RID: 6856 RVA: 0x003D9048 File Offset: 0x003D9048
    private static string NormalizedUrl(string value)
    {
      return value.Trim().ToLowerInvariant().TrimEnd('/');
    }
  }
}
