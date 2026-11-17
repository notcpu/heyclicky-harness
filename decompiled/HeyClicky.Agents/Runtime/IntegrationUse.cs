using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;

namespace HeyClicky.Agents.Runtime
{
  // Token: 0x02000026 RID: 38
  [NullableContext(1)]
  [Nullable(0)]
  public static class IntegrationUse
  {
    // Token: 0x06000405 RID: 1029 RVA: 0x000E9F78 File Offset: 0x000E9F78
    [return: Nullable(2)]
    public static string ProviderKey(JsonObject item)
    {
      JsonNode jsonNode = item["type"];
      string text = ((jsonNode != null) ? jsonNode.ToString() : null);
      if (!(text == "mcpToolCall") && !(text == "mcp_tool_call") && !(text == "dynamicToolCall"))
      {
        return null;
      }
      List<string> list = new List<string>();
      IntegrationUse.Collect(item, IntegrationUse.ServerKeys, list);
      string text2 = list.FirstOrDefault<string>((string s) => s.StartsWith("custom_", StringComparison.Ordinal));
      if (text2 != null)
      {
        return text2;
      }
      List<string> list2 = new List<string>(list);
      IntegrationUse.Collect(item, IntegrationUse.ToolKeys, list2);
      if (!list2.Any<string>((string v) => IntegrationUse.Normalized(v).Contains("composio", StringComparison.Ordinal)))
      {
        return null;
      }
      foreach (string text3 in list2)
      {
        string text4 = IntegrationUse.ToolkitSlug(text3);
        if (text4 != null)
        {
          return text4;
        }
      }
      return null;
    }

    // Token: 0x06000406 RID: 1030 RVA: 0x000EA098 File Offset: 0x000EA098
    private static string Normalized(string key)
    {
      Func<char, bool> func;
      if ((func = IntegrationUse.<>O.<0>__IsLetterOrDigit) == null)
      {
        func = (IntegrationUse.<>O.<0>__IsLetterOrDigit = new Func<char, bool>(char.IsLetterOrDigit));
      }
      return new string(key.Where<char>(func).ToArray<char>()).ToLowerInvariant();
    }

    // Token: 0x06000407 RID: 1031 RVA: 0x000EA0CC File Offset: 0x000EA0CC
    private static void Collect([Nullable(2)] JsonNode node, HashSet<string> keys, List<string> into)
    {
      JsonObject jsonObject = node as JsonObject;
      JsonArray jsonArray;
      if (jsonObject == null)
      {
        jsonArray = node as JsonArray;
        if (jsonArray == null)
        {
          return;
        }
      }
      else
      {
        using (IEnumerator<KeyValuePair<string, JsonNode>> enumerator = jsonObject.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<string, JsonNode> keyValuePair = enumerator.Current;
            string text;
            JsonNode jsonNode;
            keyValuePair.Deconstruct(out text, out jsonNode);
            string text2 = text;
            JsonNode jsonNode2 = jsonNode;
            string text3 = IntegrationUse.Normalized(text2);
            if (!IntegrationUse.SkippedKeys.Contains(text3))
            {
              if (keys.Contains(text3))
              {
                IntegrationUse.Leaves(jsonNode2, into);
              }
              IntegrationUse.Collect(jsonNode2, keys, into);
            }
          }
          return;
        }
      }
      foreach (JsonNode jsonNode3 in jsonArray)
      {
        IntegrationUse.Collect(jsonNode3, keys, into);
      }
    }

    // Token: 0x06000408 RID: 1032 RVA: 0x000EA19C File Offset: 0x000EA19C
    private static void Leaves([Nullable(2)] JsonNode node, List<string> into)
    {
      JsonValue jsonValue = node as JsonValue;
      string text2;
      if (jsonValue == null)
      {
        JsonArray jsonArray = node as JsonArray;
        JsonObject jsonObject;
        if (jsonArray == null)
        {
          jsonObject = node as JsonObject;
          if (jsonObject == null)
          {
            return;
          }
        }
        else
        {
          using (IEnumerator<JsonNode> enumerator = jsonArray.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              JsonNode jsonNode = enumerator.Current;
              IntegrationUse.Leaves(jsonNode, into);
            }
            return;
          }
        }
        foreach (KeyValuePair<string, JsonNode> keyValuePair in jsonObject)
        {
          string text;
          JsonNode jsonNode2;
          keyValuePair.Deconstruct(out text, out jsonNode2);
          IntegrationUse.Leaves(jsonNode2, into);
        }
      }
      else if (jsonValue.TryGetValue<string>(out text2) && text2 != null)
      {
        into.Add(text2);
        return;
      }
    }

    // Token: 0x06000409 RID: 1033 RVA: 0x000EA26C File Offset: 0x000EA26C
    [return: Nullable(2)]
    internal static string ToolkitSlug(string raw)
    {
      string text = raw.Trim();
      if (text.Length == 0)
      {
        return null;
      }
      if (text.StartsWith("mcp__", StringComparison.OrdinalIgnoreCase))
      {
        string[] array = text.Split("__", StringSplitOptions.None);
        if (array.Length >= 3)
        {
          text = string.Join("__", array.Skip<string>(2));
        }
      }
      IEnumerable<char> enumerable = (text.Split(new char[] { '_', '.', '-', ' ', '/', ':' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault<string>() ?? "").ToLowerInvariant();
      Func<char, bool> func;
      if ((func = IntegrationUse.<>O.<1>__IsAsciiLetterOrDigit) == null)
      {
        func = (IntegrationUse.<>O.<1>__IsAsciiLetterOrDigit = new Func<char, bool>(char.IsAsciiLetterOrDigit));
      }
      string text2 = new string(enumerable.Where<char>(func).ToArray<char>());
      if (text2.Length < 3 || IntegrationUse.Rejected.Contains(text2))
      {
        return null;
      }
      return IntegrationUse.GoogleAliases.GetValueOrDefault(text2, text2);
    }

    // Token: 0x0600040A RID: 1034 RVA: 0x000EA338 File Offset: 0x000EA338
    // Note: this type is marked as 'beforefieldinit'.
    static IntegrationUse()
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["sheets"] = "googlesheets";
      dictionary["calendar"] = "googlecalendar";
      dictionary["drive"] = "googledrive";
      dictionary["docs"] = "googledocs";
      IntegrationUse.GoogleAliases = dictionary;
      IntegrationUse.Rejected = new HashSet<string> { "composio", "computer", "computeruse", "mcp", "tool", "tools", "function", "call", "unknown" };
    }

    // Token: 0x0400017B RID: 379
    private static readonly HashSet<string> ServerKeys = new HashSet<string> { "server", "serverid", "servername", "mcpserver", "mcpserverid", "mcpservername" };

    // Token: 0x0400017C RID: 380
    private static readonly HashSet<string> ToolKeys = new HashSet<string> { "functionname", "name", "tool", "toolname", "toolslug", "toolslugs" };

    // Token: 0x0400017D RID: 381
    private static readonly HashSet<string> SkippedKeys = new HashSet<string> { "result", "output", "aggregatedoutput", "stdout", "stderr", "content", "encryptedcontent" };

    // Token: 0x0400017E RID: 382
    private static readonly Dictionary<string, string> GoogleAliases;

    // Token: 0x0400017F RID: 383
    private static readonly HashSet<string> Rejected;

    // Token: 0x0200010B RID: 267
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040005F2 RID: 1522
      [Nullable(0)]
      public static Func<char, bool> <0>__IsLetterOrDigit;

      // Token: 0x040005F3 RID: 1523
      [Nullable(0)]
      public static Func<char, bool> <1>__IsAsciiLetterOrDigit;
    }
  }
}
