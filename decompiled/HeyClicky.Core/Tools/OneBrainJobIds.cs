using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Tools
{
  // Token: 0x020000A1 RID: 161
  [NullableContext(1)]
  [Nullable(0)]
  public static class OneBrainJobIds
  {
    // Token: 0x06000A14 RID: 2580 RVA: 0x00373164 File Offset: 0x00373164
    public static string ArgumentsWithThreadIds(string argumentsJson, [TupleElementNames(new string[] { "IsJob", "ThreadId" })] [Nullable(new byte[] { 1, 1, 0, 2 })] Func<string, ValueTuple<bool, string>> threadIdForJobId)
    {
      JsonObject jsonObject;
      try
      {
        jsonObject = JsonNode.Parse(argumentsJson, null, default(JsonDocumentOptions)) as JsonObject;
      }
      catch (JsonException)
      {
        return argumentsJson;
      }
      if (jsonObject == null)
      {
        return argumentsJson;
      }
      string text = null;
      JsonNode jsonNode;
      if (jsonObject.TryGetPropertyValue("job_id", out jsonNode))
      {
        jsonObject.Remove("job_id");
        text = OneBrainJobIds.Str(jsonNode);
      }
      if (text == null)
      {
        text = OneBrainJobIds.Str(jsonObject["codexThreadId"]);
      }
      text = ((text != null) ? text.Trim() : null);
      if (string.IsNullOrEmpty(text))
      {
        return argumentsJson;
      }
      ValueTuple<bool, string> valueTuple = threadIdForJobId(text);
      bool item = valueTuple.Item1;
      string item2 = valueTuple.Item2;
      if (!item)
      {
        jsonObject["codexThreadId"] = text;
      }
      else if (item2 != null)
      {
        jsonObject["codexThreadId"] = item2;
      }
      else
      {
        jsonObject.Remove("codexThreadId");
      }
      return jsonObject.ToJsonString(OneBrainJobIds.Relaxed);
    }

    // Token: 0x06000A15 RID: 2581 RVA: 0x00373260 File Offset: 0x00373260
    public static string OutputWithJobIds(string output, Func<string, string> jobIdForThreadId)
    {
      JsonNode jsonNode;
      try
      {
        jsonNode = JsonNode.Parse(output, null, default(JsonDocumentOptions));
      }
      catch (JsonException)
      {
        return output.Replace("codexThreadId", "job_id", StringComparison.Ordinal);
      }
      if (jsonNode == null)
      {
        return output.Replace("codexThreadId", "job_id", StringComparison.Ordinal);
      }
      JsonNode jsonNode2 = OneBrainJobIds.Rewrite(jsonNode, jobIdForThreadId);
      return ((jsonNode2 != null) ? jsonNode2.ToJsonString(OneBrainJobIds.Relaxed) : null) ?? output;
    }

    // Token: 0x06000A16 RID: 2582 RVA: 0x003732E4 File Offset: 0x003732E4
    [NullableContext(2)]
    private static JsonNode Rewrite(JsonNode node, [Nullable(1)] Func<string, string> jobIdForThreadId)
    {
      JsonObject jsonObject = node as JsonObject;
      if (jsonObject != null)
      {
        JsonObject jsonObject2 = jsonObject;
        JsonObject jsonObject3 = new JsonObject(null);
        foreach (KeyValuePair<string, JsonNode> keyValuePair in jsonObject2)
        {
          string text;
          JsonNode jsonNode;
          keyValuePair.Deconstruct(out text, out jsonNode);
          string text2 = text;
          JsonNode jsonNode2 = jsonNode;
          if (text2 == "codexThreadId")
          {
            string text3 = OneBrainJobIds.Str(jsonNode2);
            if (text3 != null)
            {
              if (!jsonObject2.ContainsKey("job_id"))
              {
                jsonObject3["job_id"] = jobIdForThreadId(text3);
                continue;
              }
              continue;
            }
          }
          jsonObject3[text2] = OneBrainJobIds.Rewrite(jsonNode2, jobIdForThreadId);
        }
        return jsonObject3;
      }
      JsonArray jsonArray = node as JsonArray;
      if (jsonArray != null)
      {
        return new JsonArray(jsonArray.Select<JsonNode, JsonNode>((JsonNode item) => OneBrainJobIds.Rewrite(item, jobIdForThreadId)).ToArray<JsonNode>());
      }
      JsonValue jsonValue = node as JsonValue;
      if (jsonValue != null)
      {
        string text4;
        if (jsonValue.TryGetValue<string>(out text4))
        {
          return JsonValue.Create(text4.Replace("codexThreadId", "job_id", StringComparison.Ordinal), null);
        }
      }
      if (node == null)
      {
        return null;
      }
      return node.DeepClone();
    }

    // Token: 0x06000A17 RID: 2583 RVA: 0x0037344C File Offset: 0x0037344C
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

    // Token: 0x04000494 RID: 1172
    private static readonly JsonSerializerOptions Relaxed = new JsonSerializerOptions
    {
      Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
  }
}
