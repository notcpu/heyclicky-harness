using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using HeyClicky.Agents.Harness;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000054 RID: 84
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyHarnessRecord
  {
    // Token: 0x06000763 RID: 1891 RVA: 0x000FC59C File Offset: 0x000FC59C
    [return: Nullable(2)]
    public static string RecordDirectory(string recordsRoot, string threadId)
    {
      if (!AgentHarnessPreferences.IsHarnessThread(threadId))
      {
        return null;
      }
      string text = Path.Combine(recordsRoot, threadId);
      if (!Directory.Exists(text))
      {
        return null;
      }
      return text;
    }

    // Token: 0x06000764 RID: 1892 RVA: 0x000FC5C8 File Offset: 0x000FC5C8
    [return: TupleElementNames(new string[] { "ThreadId", "Workspace" })]
    [return: Nullable(new byte[] { 1, 0, 1, 1 })]
    public static IReadOnlyList<ValueTuple<string, string>> AllRecords(string recordsRoot)
    {
      List<ValueTuple<string, string>> list = new List<ValueTuple<string, string>>();
      string[] array;
      try
      {
        array = (Directory.Exists(recordsRoot) ? Directory.GetDirectories(recordsRoot) : Array.Empty<string>());
      }
      catch (Exception)
      {
        return list;
      }
      foreach (string text in array)
      {
        try
        {
          string text2 = Path.Combine(text, "job.json");
          if (File.Exists(text2))
          {
            JsonObject jsonObject = JsonNode.Parse(File.ReadAllText(text2), null, default(JsonDocumentOptions)) as JsonObject;
            if (jsonObject != null)
            {
              JsonObject jsonObject2 = jsonObject["cfg"] as JsonObject;
              if (jsonObject2 != null)
              {
                JsonValue jsonValue = jsonObject2["workspace"] as JsonValue;
                string text3;
                if (jsonValue != null && jsonValue.TryGetValue<string>(out text3) && text3 != null)
                {
                  list.Add(new ValueTuple<string, string>(Path.GetFileName(text), text3));
                }
              }
            }
          }
        }
        catch (Exception)
        {
        }
      }
      return list;
    }

    // Token: 0x06000765 RID: 1893 RVA: 0x000FC6D8 File Offset: 0x000FC6D8
    public static IReadOnlyList<ClickyHarnessRecord.Turn> Turns(string recordDirectory)
    {
      Dictionary<int, ClickyHarnessRecord.Turn> dictionary = new Dictionary<int, ClickyHarnessRecord.Turn>();
      try
      {
        string text = Path.Combine(recordDirectory, "turns.json");
        if (File.Exists(text))
        {
          JsonObject jsonObject = JsonNode.Parse(File.ReadAllText(text), null, default(JsonDocumentOptions)) as JsonObject;
          if (jsonObject != null)
          {
            foreach (KeyValuePair<string, JsonNode> keyValuePair in jsonObject)
            {
              string text2;
              JsonNode jsonNode;
              keyValuePair.Deconstruct(out text2, out jsonNode);
              string text3 = text2;
              JsonNode jsonNode2 = jsonNode;
              int num;
              if (int.TryParse(text3, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
              {
                JsonObject jsonObject2 = jsonNode2 as JsonObject;
                if (jsonObject2 == null)
                {
                  goto IL_00B0;
                }
                JsonValue jsonValue = jsonObject2["text"] as JsonValue;
                string text4;
                if (jsonValue == null || !jsonValue.TryGetValue<string>(out text4))
                {
                  goto IL_00B0;
                }
                string text5 = text4 ?? "";
                IL_00C2:
                string text6 = text5;
                dictionary[num] = new ClickyHarnessRecord.Turn
                {
                  Number = num,
                  RawAsk = text6
                };
                continue;
                IL_00B0:
                text5 = "";
                goto IL_00C2;
              }
            }
          }
        }
      }
      catch (Exception)
      {
      }
      string[] array;
      try
      {
        string text7 = Path.Combine(recordDirectory, "events.jsonl");
        if (!File.Exists(text7))
        {
          return dictionary.Values.OrderBy<ClickyHarnessRecord.Turn, int>((ClickyHarnessRecord.Turn t) => t.Number).ToList<ClickyHarnessRecord.Turn>();
        }
        array = File.ReadAllLines(text7);
      }
      catch (Exception)
      {
        return dictionary.Values.OrderBy<ClickyHarnessRecord.Turn, int>((ClickyHarnessRecord.Turn t) => t.Number).ToList<ClickyHarnessRecord.Turn>();
      }
      int num2 = 1;
      foreach (string text8 in array)
      {
        if (text8.Contains("\"kind\":\"tool_call\"", StringComparison.Ordinal) || text8.Contains("\"kind\":\"message\"", StringComparison.Ordinal) || text8.Contains("\"kind\":\"turn_end\"", StringComparison.Ordinal))
        {
          JsonObject jsonObject3;
          try
          {
            jsonObject3 = JsonNode.Parse(text8, null, default(JsonDocumentOptions)) as JsonObject;
          }
          catch (Exception)
          {
            goto IL_03E4;
          }
          if (jsonObject3 != null)
          {
            string text9 = ClickyHarnessRecord.Str(jsonObject3["kind"]);
            if (text9 != null)
            {
              int num4;
              int num3 = (int.TryParse(ClickyHarnessRecord.Str(jsonObject3["turn"]), NumberStyles.Integer, CultureInfo.InvariantCulture, out num4) ? num4 : ClickyHarnessRecord.Int(jsonObject3["turn"]).GetValueOrDefault(num2));
              ClickyHarnessRecord.Turn turn;
              if (!dictionary.TryGetValue(num3, out turn))
              {
                turn = new ClickyHarnessRecord.Turn
                {
                  Number = num3
                };
              }
              string text2 = text9;
              if (!(text2 == "tool_call"))
              {
                if (!(text2 == "message"))
                {
                  if (text2 == "turn_end")
                  {
                    turn.Status = ClickyHarnessRecord.Str(jsonObject3["status"]);
                    ClickyHarnessRecord.Turn turn2 = turn;
                    if (turn2.FinalText == null)
                    {
                      turn2.FinalText = ClickyHarnessRecord.Str(jsonObject3["finalText"]);
                    }
                    double? num5 = ClickyHarnessRecord.Double(jsonObject3["t"]);
                    double num7;
                    double? num6 = ((num5 != null) ? num5 : (double.TryParse(ClickyHarnessRecord.Str(jsonObject3["t"]), NumberStyles.Float, CultureInfo.InvariantCulture, out num7) ? new double?(num7) : null));
                    if (num6 != null)
                    {
                      double valueOrDefault = num6.GetValueOrDefault();
                      turn.EndedAt = new DateTimeOffset?(DateTimeOffset.FromUnixTimeMilliseconds((long)valueOrDefault).ToLocalTime());
                    }
                    num2 = num3 + 1;
                  }
                }
                else if (ClickyHarnessRecord.Str(jsonObject3["phase"]) == "final")
                {
                  turn.FinalText = ClickyHarnessRecord.Str(jsonObject3["text"]);
                }
              }
              else
              {
                string text10 = ClickyHarnessRecord.ToolLine(jsonObject3);
                if (text10 != null)
                {
                  turn.ToolLines.Add(text10);
                }
              }
              dictionary[num3] = turn;
            }
          }
        }
        IL_03E4:;
      }
      return dictionary.Values.OrderBy<ClickyHarnessRecord.Turn, int>((ClickyHarnessRecord.Turn t) => t.Number).ToList<ClickyHarnessRecord.Turn>();
    }

    // Token: 0x06000766 RID: 1894 RVA: 0x000FCB40 File Offset: 0x000FCB40
    [return: Nullable(2)]
    public static string ToolLine(JsonObject record)
    {
      string text = ClickyHarnessRecord.Str(record["name"]);
      if (text == null || text == "update_plan")
      {
        return null;
      }
      JsonObject jsonObject;
      if ((jsonObject = record["args"] as JsonObject) == null)
      {
        jsonObject = new JsonObject(null);
      }
      JsonObject jsonObject2 = jsonObject;
      string text2;
      if ((text2 = ClickyHarnessRecord.Str(jsonObject2["command"])) == null && (text2 = ClickyHarnessRecord.Str(jsonObject2["code"])) == null && (text2 = ClickyHarnessRecord.Str(jsonObject2["query"])) == null)
      {
        text2 = ClickyHarnessRecord.Str(jsonObject2["url"]) ?? ClickyHarnessRecord.SortedJson(jsonObject2);
      }
      string text3 = text2;
      string text4 = "- " + text + ": " + ClickyWorkIndex.OneLine(text3, 220);
      JsonValue jsonValue = record["ok"] as JsonValue;
      bool flag;
      if (!((jsonValue != null && jsonValue.TryGetValue<bool>(out flag)) ? flag : (ClickyHarnessRecord.Str(record["ok"]) != "False")))
      {
        string text5 = ClickyHarnessRecord.Str(record["error"]);
        if (text5 != null && text5.Length > 0)
        {
          text4 = text4 + " → failed: " + ClickyWorkIndex.OneLine(text5, 220);
        }
      }
      return text4;
    }

    // Token: 0x06000767 RID: 1895 RVA: 0x000FCC84 File Offset: 0x000FCC84
    private static string SortedJson(JsonObject arguments)
    {
      string text;
      try
      {
        JsonObject jsonObject = new JsonObject(null);
        foreach (KeyValuePair<string, JsonNode> keyValuePair in arguments.OrderBy<KeyValuePair<string, JsonNode>, string>((KeyValuePair<string, JsonNode> p) => p.Key, StringComparer.Ordinal))
        {
          JsonNode jsonNode;
          keyValuePair.Deconstruct(out text, out jsonNode);
          string text2 = text;
          JsonNode jsonNode2 = jsonNode;
          jsonObject[text2] = ((jsonNode2 != null) ? jsonNode2.DeepClone() : null);
        }
        text = jsonObject.ToJsonString(new JsonSerializerOptions
        {
          Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
      }
      catch (Exception)
      {
        text = "";
      }
      return text;
    }

    // Token: 0x06000768 RID: 1896 RVA: 0x000FCD58 File Offset: 0x000FCD58
    [NullableContext(2)]
    private static string Str(JsonNode node)
    {
      JsonValue jsonValue = node as JsonValue;
      string text;
      if (jsonValue == null || !jsonValue.TryGetValue<string>(out text))
      {
        return null;
      }
      return text;
    }

    // Token: 0x06000769 RID: 1897 RVA: 0x000FCD7C File Offset: 0x000FCD7C
    [NullableContext(2)]
    private static int? Int(JsonNode node)
    {
      JsonValue jsonValue = node as JsonValue;
      int num;
      if (jsonValue == null || !jsonValue.TryGetValue<int>(out num))
      {
        return null;
      }
      return new int?(num);
    }

    // Token: 0x0600076A RID: 1898 RVA: 0x000FCDB0 File Offset: 0x000FCDB0
    [NullableContext(2)]
    private static double? Double(JsonNode node)
    {
      JsonValue jsonValue = node as JsonValue;
      double num;
      if (jsonValue == null || !jsonValue.TryGetValue<double>(out num))
      {
        return null;
      }
      return new double?(num);
    }

    // Token: 0x0200019A RID: 410
    [NullableContext(2)]
    [Nullable(0)]
    public sealed class Turn
    {
      // Token: 0x170002D5 RID: 725
      // (get) Token: 0x06000E0C RID: 3596 RVA: 0x00116A6C File Offset: 0x00116A6C
      // (set) Token: 0x06000E0D RID: 3597 RVA: 0x00116A74 File Offset: 0x00116A74
      public int Number { get; set; }

      // Token: 0x170002D6 RID: 726
      // (get) Token: 0x06000E0E RID: 3598 RVA: 0x00116A80 File Offset: 0x00116A80
      // (set) Token: 0x06000E0F RID: 3599 RVA: 0x00116A88 File Offset: 0x00116A88
      [Nullable(1)]
      public string RawAsk
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      } = "";

      // Token: 0x170002D7 RID: 727
      // (get) Token: 0x06000E10 RID: 3600 RVA: 0x00116A94 File Offset: 0x00116A94
      [Nullable(1)]
      public List<string> ToolLines
      {
        [NullableContext(1)]
        get;
      } = new List<string>();

      // Token: 0x170002D8 RID: 728
      // (get) Token: 0x06000E11 RID: 3601 RVA: 0x00116A9C File Offset: 0x00116A9C
      // (set) Token: 0x06000E12 RID: 3602 RVA: 0x00116AA4 File Offset: 0x00116AA4
      public string FinalText { get; set; }

      // Token: 0x170002D9 RID: 729
      // (get) Token: 0x06000E13 RID: 3603 RVA: 0x00116AB0 File Offset: 0x00116AB0
      // (set) Token: 0x06000E14 RID: 3604 RVA: 0x00116AB8 File Offset: 0x00116AB8
      public string Status { get; set; }

      // Token: 0x170002DA RID: 730
      // (get) Token: 0x06000E15 RID: 3605 RVA: 0x00116AC4 File Offset: 0x00116AC4
      // (set) Token: 0x06000E16 RID: 3606 RVA: 0x00116ACC File Offset: 0x00116ACC
      public DateTimeOffset? EndedAt { get; set; }
    }
  }
}
