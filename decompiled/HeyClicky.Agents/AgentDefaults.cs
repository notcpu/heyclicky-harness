using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.Agents
{
  // Token: 0x02000004 RID: 4
  [NullableContext(1)]
  [Nullable(0)]
  public static class AgentDefaults
  {
    // Token: 0x06000004 RID: 4 RVA: 0x000D5D14 File Offset: 0x000D5D14
    public static void Configure(string stateDirectory)
    {
      object gate = AgentDefaults.Gate;
      lock (gate)
      {
        Directory.CreateDirectory(stateDirectory);
        string text = Path.Combine(stateDirectory, "defaults.json");
        if (!string.Equals(text, AgentDefaults.path, StringComparison.OrdinalIgnoreCase))
        {
          AgentDefaults.path = text;
          AgentDefaults.values = AgentDefaults.Load(text);
        }
      }
    }

    // Token: 0x06000005 RID: 5 RVA: 0x000D5D84 File Offset: 0x000D5D84
    private static JsonObject Load(string file)
    {
      try
      {
        if (File.Exists(file))
        {
          JsonObject jsonObject = JsonNode.Parse(File.ReadAllText(file), null, default(JsonDocumentOptions)) as JsonObject;
          if (jsonObject != null)
          {
            return jsonObject;
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Agents", "defaults.json unreadable, starting fresh: " + ex.Message);
      }
      return new JsonObject(null);
    }

    // Token: 0x06000006 RID: 6 RVA: 0x000D5E04 File Offset: 0x000D5E04
    private static void SaveLocked()
    {
      if (AgentDefaults.path == null)
      {
        return;
      }
      try
      {
        string text = AgentDefaults.path + ".tmp";
        File.WriteAllText(text, AgentDefaults.values.ToJsonString(AgentDefaults.Json));
        File.Move(text, AgentDefaults.path, true);
      }
      catch (Exception ex)
      {
        Log.Warn("Agents", "defaults.json not saved: " + ex.Message);
      }
    }

    // Token: 0x06000007 RID: 7 RVA: 0x000D5E78 File Offset: 0x000D5E78
    [return: Nullable(2)]
    public static string String(string key)
    {
      object gate = AgentDefaults.Gate;
      string text;
      lock (gate)
      {
        JsonValue jsonValue = AgentDefaults.values[key] as JsonValue;
        string text2;
        text = ((jsonValue != null && jsonValue.TryGetValue<string>(out text2)) ? text2 : null);
      }
      return text;
    }

    // Token: 0x06000008 RID: 8 RVA: 0x000D5ED8 File Offset: 0x000D5ED8
    public static bool Bool(string key)
    {
      object gate = AgentDefaults.Gate;
      bool flag2;
      lock (gate)
      {
        JsonValue jsonValue = AgentDefaults.values[key] as JsonValue;
        bool flag3;
        flag2 = jsonValue != null && jsonValue.TryGetValue<bool>(out flag3) && flag3;
      }
      return flag2;
    }

    // Token: 0x06000009 RID: 9 RVA: 0x000D5F38 File Offset: 0x000D5F38
    [NullableContext(2)]
    public static T Get<T>([Nullable(1)] string key)
    {
      object gate = AgentDefaults.Gate;
      T t;
      lock (gate)
      {
        JsonNode jsonNode = AgentDefaults.values[key];
        if (jsonNode == null)
        {
          t = default(T);
          t = t;
        }
        else
        {
          try
          {
            t = jsonNode.Deserialize<T>(null);
          }
          catch (JsonException)
          {
            t = default(T);
          }
        }
      }
      return t;
    }

    // Token: 0x0600000A RID: 10 RVA: 0x000D5FB0 File Offset: 0x000D5FB0
    public static void Set(string key, [Nullable(2)] string value)
    {
      AgentDefaults.SetNode(key, (value == null) ? null : JsonValue.Create(value, null));
    }

    // Token: 0x0600000B RID: 11 RVA: 0x000D5FD8 File Offset: 0x000D5FD8
    public static void Set(string key, bool value)
    {
      AgentDefaults.SetNode(key, JsonValue.Create(value, null));
    }

    // Token: 0x0600000C RID: 12 RVA: 0x000D5FFC File Offset: 0x000D5FFC
    public static void Set<[Nullable(2)] T>(string key, T value)
    {
      AgentDefaults.SetNode(key, (value == null) ? null : JsonSerializer.SerializeToNode<T>(value, null));
    }

    // Token: 0x0600000D RID: 13 RVA: 0x000D6018 File Offset: 0x000D6018
    public static void Remove(string key)
    {
      AgentDefaults.SetNode(key, null);
    }

    // Token: 0x0600000E RID: 14 RVA: 0x000D6024 File Offset: 0x000D6024
    public static IReadOnlyList<string> Keys(string prefix)
    {
      object gate = AgentDefaults.Gate;
      IReadOnlyList<string> readOnlyList;
      lock (gate)
      {
        readOnlyList = (from kv in AgentDefaults.values
          select kv.Key into k
          where k.StartsWith(prefix, StringComparison.Ordinal)
          select k).ToList<string>();
      }
      return readOnlyList;
    }

    // Token: 0x0600000F RID: 15 RVA: 0x000D60AC File Offset: 0x000D60AC
    private static void SetNode(string key, [Nullable(2)] JsonNode node)
    {
      object gate = AgentDefaults.Gate;
      lock (gate)
      {
        if (node == null)
        {
          if (!AgentDefaults.values.Remove(key))
          {
            return;
          }
        }
        else
        {
          JsonNode jsonNode = AgentDefaults.values[key];
          if (jsonNode != null && JsonNode.DeepEquals(jsonNode, node))
          {
            return;
          }
          AgentDefaults.values[key] = node;
        }
        AgentDefaults.SaveLocked();
      }
    }

    // Token: 0x04000005 RID: 5
    private static readonly object Gate = new object();

    // Token: 0x04000006 RID: 6
    private static readonly JsonSerializerOptions Json = new JsonSerializerOptions
    {
      WriteIndented = false
    };

    // Token: 0x04000007 RID: 7
    [Nullable(2)]
    private static string path;

    // Token: 0x04000008 RID: 8
    private static JsonObject values = new JsonObject(null);
  }
}
