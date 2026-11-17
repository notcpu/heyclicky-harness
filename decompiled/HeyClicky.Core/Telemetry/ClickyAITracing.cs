using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using HeyClicky.Core.Configuration;

namespace HeyClicky.Core.Telemetry
{
  // Token: 0x020000AE RID: 174
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyAITracing
  {
    // Token: 0x17000248 RID: 584
    // (get) Token: 0x06000A83 RID: 2691 RVA: 0x00374DF4 File Offset: 0x00374DF4
    public static string SessionId
    {
      get
      {
        object gate = ClickyAITracing.Gate;
        string text;
        lock (gate)
        {
          text = ClickyAITracing.sessionId;
        }
        return text;
      }
    }

    // Token: 0x06000A84 RID: 2692 RVA: 0x00374E34 File Offset: 0x00374E34
    public static void UseStore(string path)
    {
      object gate = ClickyAITracing.Gate;
      lock (gate)
      {
        ClickyAITracing.storePath = path;
        ClickyAITracing.agentThreadTraceIds = null;
      }
    }

    // Token: 0x06000A85 RID: 2693 RVA: 0x00374E7C File Offset: 0x00374E7C
    private static string NewId()
    {
      return Guid.NewGuid().ToString().ToLowerInvariant();
    }

    // Token: 0x06000A86 RID: 2694 RVA: 0x00374EA4 File Offset: 0x00374EA4
    public static string EnsureNormalModeTraceId()
    {
      object gate = ClickyAITracing.Gate;
      string text2;
      lock (gate)
      {
        string text;
        if ((text = ClickyAITracing.normalModeTraceId) == null)
        {
          text = (ClickyAITracing.normalModeTraceId = ClickyAITracing.NewId());
        }
        text2 = text;
      }
      return text2;
    }

    // Token: 0x06000A87 RID: 2695 RVA: 0x00374EF4 File Offset: 0x00374EF4
    public static string TraceIdForAgentThread(string agentThreadId)
    {
      object gate = ClickyAITracing.Gate;
      string text2;
      lock (gate)
      {
        Dictionary<string, string> dictionary = ClickyAITracing.LoadMap();
        string text;
        if (dictionary.TryGetValue(agentThreadId, out text))
        {
          text2 = text;
        }
        else
        {
          string text3 = ClickyAITracing.NewId();
          dictionary[agentThreadId] = text3;
          ClickyAITracing.SaveMap(dictionary);
          text2 = text3;
        }
      }
      return text2;
    }

    // Token: 0x06000A88 RID: 2696 RVA: 0x00374F60 File Offset: 0x00374F60
    public static IReadOnlyDictionary<string, string> HttpHeaders(ClickyAITracing.Mode mode = ClickyAITracing.Mode.Normal, [Nullable(2)] string agentThreadId = null)
    {
      if (mode == ClickyAITracing.Mode.Agent && !string.IsNullOrEmpty(agentThreadId))
      {
        return ClickyAITracing.BaseHeaders(ClickyAITracing.TraceIdForAgentThread(agentThreadId), ClickyAITracing.Mode.Agent, agentThreadId);
      }
      return ClickyAITracing.BaseHeaders(ClickyAITracing.EnsureNormalModeTraceId(), ClickyAITracing.Mode.Normal, null);
    }

    // Token: 0x06000A89 RID: 2697 RVA: 0x00374F88 File Offset: 0x00374F88
    private static Dictionary<string, string> BaseHeaders(string traceId, ClickyAITracing.Mode mode, [Nullable(2)] string agentThreadId)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["X-Clicky-Distinct-Id"] = ClickyAnalytics.DistinctId;
      dictionary["X-Clicky-Session-Id"] = ClickyAITracing.SessionId;
      dictionary["X-Clicky-Trace-Id"] = traceId;
      dictionary["X-Clicky-Mode"] = ClickyAITracing.ModeName(mode);
      Dictionary<string, string> dictionary2 = dictionary;
      if (!string.IsNullOrEmpty(agentThreadId))
      {
        dictionary2["X-Clicky-Agent-Thread-Id"] = agentThreadId;
      }
      return dictionary2;
    }

    // Token: 0x06000A8A RID: 2698 RVA: 0x00374FF0 File Offset: 0x00374FF0
    private static string ModeName(ClickyAITracing.Mode mode)
    {
      if (mode != ClickyAITracing.Mode.Agent)
      {
        return "normal";
      }
      return "agent";
    }

    // Token: 0x06000A8B RID: 2699 RVA: 0x00375004 File Offset: 0x00375004
    public static void Apply(HttpRequestHeaders headers)
    {
      if (headers.Contains("X-Clicky-Trace-Id"))
      {
        return;
      }
      IEnumerable<string> enumerable;
      string text = (headers.TryGetValues("X-Clicky-Agent-Thread-Id", out enumerable) ? enumerable.FirstOrDefault<string>() : null);
      foreach (KeyValuePair<string, string> keyValuePair in ClickyAITracing.HttpHeaders(string.IsNullOrEmpty(text) ? ClickyAITracing.Mode.Normal : ClickyAITracing.Mode.Agent, text))
      {
        string text2;
        string text3;
        keyValuePair.Deconstruct(out text2, out text3);
        string text4 = text2;
        string text5 = text3;
        if (!headers.Contains(text4))
        {
          headers.TryAddWithoutValidation(text4, text5);
        }
      }
    }

    // Token: 0x06000A8C RID: 2700 RVA: 0x003750A4 File Offset: 0x003750A4
    [NullableContext(2)]
    public static void EmitTraceStartIfNeeded(ClickyAITracing.Mode mode, string agentThreadId, JsonObject extraProperties = null)
    {
      string text;
      if (mode == ClickyAITracing.Mode.Normal)
      {
        text = ClickyAITracing.EnsureNormalModeTraceId();
      }
      else
      {
        if (string.IsNullOrEmpty(agentThreadId))
        {
          return;
        }
        text = ClickyAITracing.TraceIdForAgentThread(agentThreadId);
      }
      object gate = ClickyAITracing.Gate;
      lock (gate)
      {
        if (!ClickyAITracing.TraceIdsWithEmittedStart.Add(text))
        {
          return;
        }
      }
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["$ai_trace_id"] = text;
      jsonObject["$ai_session_id"] = ClickyAITracing.SessionId;
      jsonObject["$ai_span_name"] = ((mode == ClickyAITracing.Mode.Normal) ? "normal_companion" : "agent_thread");
      jsonObject["clicky_mode"] = ClickyAITracing.ModeName(mode);
      jsonObject["clicky_app_version"] = ClickyAnalytics.AppVersion;
      JsonObject jsonObject2 = jsonObject;
      if (!string.IsNullOrEmpty(agentThreadId))
      {
        jsonObject2["clicky_agent_thread_id"] = agentThreadId;
      }
      ClickyAITracing.Merge(jsonObject2, extraProperties);
      ClickyAnalytics.Capture("$ai_trace", jsonObject2, null, null);
    }

    // Token: 0x06000A8D RID: 2701 RVA: 0x003751B8 File Offset: 0x003751B8
    public static void EmitAgentGeneration(string agentThreadId, string spanName, [Nullable(2)] string model, string userMessage, string assistantMessage, long? inputTokens, long? outputTokens, double? latencySeconds, bool isError, [Nullable(2)] string error, [Nullable(2)] JsonObject extraProperties = null)
    {
      if (string.IsNullOrEmpty(agentThreadId))
      {
        return;
      }
      string text = ClickyAITracing.TraceIdForAgentThread(agentThreadId);
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["role"] = "user";
      jsonObject["content"] = ClickyAITracing.Cap(userMessage, 8192);
      JsonNode jsonNode = jsonObject;
      JsonArray jsonArray = new JsonArray(new ReadOnlySpan<JsonNode>(ref jsonNode));
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["role"] = "assistant";
      jsonObject2["content"] = ClickyAITracing.Cap(assistantMessage, 8192);
      JsonNode jsonNode2 = jsonObject2;
      JsonArray jsonArray2 = new JsonArray(new ReadOnlySpan<JsonNode>(ref jsonNode2));
      JsonObject jsonObject3 = new JsonObject(null);
      jsonObject3["$ai_trace_id"] = text;
      jsonObject3["$ai_session_id"] = ClickyAITracing.SessionId;
      jsonObject3["$ai_span_name"] = spanName;
      jsonObject3["$ai_provider"] = "openai";
      jsonObject3["$ai_input"] = jsonArray;
      jsonObject3["$ai_input_state"] = jsonArray.DeepClone();
      jsonObject3["$ai_output_choices"] = jsonArray2;
      jsonObject3["$ai_output_state"] = jsonArray2.DeepClone();
      jsonObject3["clicky_mode"] = "agent";
      jsonObject3["clicky_agent_thread_id"] = agentThreadId;
      JsonObject jsonObject4 = jsonObject3;
      if (!string.IsNullOrEmpty(model))
      {
        jsonObject4["$ai_model"] = model;
      }
      if (inputTokens != null)
      {
        long valueOrDefault = inputTokens.GetValueOrDefault();
        jsonObject4["$ai_input_tokens"] = valueOrDefault;
      }
      if (outputTokens != null)
      {
        long valueOrDefault2 = outputTokens.GetValueOrDefault();
        jsonObject4["$ai_output_tokens"] = valueOrDefault2;
      }
      if (latencySeconds != null)
      {
        double valueOrDefault3 = latencySeconds.GetValueOrDefault();
        jsonObject4["$ai_latency"] = valueOrDefault3;
      }
      if (isError)
      {
        jsonObject4["$ai_is_error"] = true;
      }
      if (error != null)
      {
        jsonObject4["$ai_error"] = ClickyAITracing.Cap(error, 1024);
      }
      ClickyAITracing.Merge(jsonObject4, extraProperties);
      ClickyAnalytics.Capture("$ai_generation", jsonObject4, null, null);
    }

    // Token: 0x06000A8E RID: 2702 RVA: 0x00375400 File Offset: 0x00375400
    public static void EmitClientErrorSpan(ClickyAITracing.Mode mode, [Nullable(2)] string agentThreadId, string stage, string error)
    {
      if (mode == ClickyAITracing.Mode.Agent && string.IsNullOrEmpty(agentThreadId))
      {
        mode = ClickyAITracing.Mode.Normal;
      }
      string text = ((mode == ClickyAITracing.Mode.Normal) ? ClickyAITracing.EnsureNormalModeTraceId() : ClickyAITracing.TraceIdForAgentThread(agentThreadId));
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["$ai_trace_id"] = text;
      jsonObject["$ai_session_id"] = ClickyAITracing.SessionId;
      jsonObject["$ai_span_name"] = "client_error";
      jsonObject["$ai_is_error"] = true;
      jsonObject["$ai_error"] = ClickyAITracing.Cap(error, 1024);
      jsonObject["clicky_mode"] = ClickyAITracing.ModeName(mode);
      jsonObject["clicky_stage"] = stage;
      JsonObject jsonObject2 = jsonObject;
      if (mode == ClickyAITracing.Mode.Agent)
      {
        jsonObject2["clicky_agent_thread_id"] = agentThreadId;
      }
      ClickyAnalytics.Capture("$ai_span", jsonObject2, null, null);
    }

    // Token: 0x06000A8F RID: 2703 RVA: 0x003754EC File Offset: 0x003754EC
    private static string Cap(string text, int limit)
    {
      if (text.Length > limit)
      {
        return text.Substring(0, limit) + "…";
      }
      return text;
    }

    // Token: 0x06000A90 RID: 2704 RVA: 0x0037550C File Offset: 0x0037550C
    private static void Merge(JsonObject target, [Nullable(2)] JsonObject extra)
    {
      if (extra == null)
      {
        return;
      }
      foreach (KeyValuePair<string, JsonNode> keyValuePair in extra)
      {
        string text;
        JsonNode jsonNode;
        keyValuePair.Deconstruct(out text, out jsonNode);
        string text2 = text;
        JsonNode jsonNode2 = jsonNode;
        target[text2] = ((jsonNode2 != null) ? jsonNode2.DeepClone() : null);
      }
    }

    // Token: 0x06000A91 RID: 2705 RVA: 0x00375578 File Offset: 0x00375578
    private static Dictionary<string, string> LoadMap()
    {
      if (ClickyAITracing.agentThreadTraceIds != null)
      {
        return ClickyAITracing.agentThreadTraceIds;
      }
      ClickyAITracing.agentThreadTraceIds = new Dictionary<string, string>();
      try
      {
        if (File.Exists(ClickyAITracing.storePath))
        {
          JsonObject jsonObject = JsonNode.Parse(File.ReadAllText(ClickyAITracing.storePath), null, default(JsonDocumentOptions)) as JsonObject;
          if (jsonObject != null)
          {
            foreach (KeyValuePair<string, JsonNode> keyValuePair in jsonObject)
            {
              string text;
              JsonNode jsonNode;
              keyValuePair.Deconstruct(out text, out jsonNode);
              string text2 = text;
              JsonValue jsonValue = jsonNode as JsonValue;
              string text3;
              if (jsonValue != null && jsonValue.TryGetValue<string>(out text3) && !string.IsNullOrEmpty(text3))
              {
                ClickyAITracing.agentThreadTraceIds[text2] = text3;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("ai-tracing", "trace id map ignored: " + ex.Message);
      }
      return ClickyAITracing.agentThreadTraceIds;
    }

    // Token: 0x06000A92 RID: 2706 RVA: 0x0037567C File Offset: 0x0037567C
    private static void SaveMap(Dictionary<string, string> map)
    {
      try
      {
        Directory.CreateDirectory(Path.GetDirectoryName(ClickyAITracing.storePath));
        JsonObject jsonObject = new JsonObject(null);
        foreach (KeyValuePair<string, string> keyValuePair in map)
        {
          string text;
          string text2;
          keyValuePair.Deconstruct(out text, out text2);
          string text3 = text;
          string text4 = text2;
          jsonObject[text3] = text4;
        }
        File.WriteAllText(ClickyAITracing.storePath, jsonObject.ToJsonString(null));
      }
      catch (Exception ex)
      {
        Log.Warn("ai-tracing", "trace id map not saved: " + ex.Message);
      }
    }

    // Token: 0x040004BB RID: 1211
    public const string DistinctIdHeader = "X-Clicky-Distinct-Id";

    // Token: 0x040004BC RID: 1212
    public const string SessionIdHeader = "X-Clicky-Session-Id";

    // Token: 0x040004BD RID: 1213
    public const string TraceIdHeader = "X-Clicky-Trace-Id";

    // Token: 0x040004BE RID: 1214
    public const string ModeHeader = "X-Clicky-Mode";

    // Token: 0x040004BF RID: 1215
    public const string AgentThreadIdHeader = "X-Clicky-Agent-Thread-Id";

    // Token: 0x040004C0 RID: 1216
    private static readonly object Gate = new object();

    // Token: 0x040004C1 RID: 1217
    private static string sessionId = ClickyAITracing.NewId();

    // Token: 0x040004C2 RID: 1218
    [Nullable(2)]
    private static string normalModeTraceId;

    // Token: 0x040004C3 RID: 1219
    [Nullable(new byte[] { 2, 1, 1 })]
    private static Dictionary<string, string> agentThreadTraceIds;

    // Token: 0x040004C4 RID: 1220
    private static readonly HashSet<string> TraceIdsWithEmittedStart = new HashSet<string>();

    // Token: 0x040004C5 RID: 1221
    private static string storePath = Path.Combine(AppBundleConfiguration.AppDataDirectory, "ai-trace-ids.json");

    // Token: 0x020003B6 RID: 950
    [NullableContext(0)]
    public enum Mode
    {
      // Token: 0x04001942 RID: 6466
      Normal,
      // Token: 0x04001943 RID: 6467
      Agent
    }
  }
}
