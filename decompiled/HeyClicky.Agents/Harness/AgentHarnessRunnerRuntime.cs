using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents.Jobs;

namespace HeyClicky.Agents.Harness
{
  // Token: 0x0200005E RID: 94
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentHarnessRunnerRuntime : IAgentHarnessRuntime
  {
    // Token: 0x0600079D RID: 1949 RVA: 0x000FDB08 File Offset: 0x000FDB08
    public AgentHarnessRunnerRuntime(AgentHarness harness, AgentHarnessPlace place)
    {
    }

    // Token: 0x170001E5 RID: 485
    // (get) Token: 0x0600079E RID: 1950 RVA: 0x000FDB20 File Offset: 0x000FDB20
    public AgentHarness Harness { get; } = harness;

    // Token: 0x170001E6 RID: 486
    // (get) Token: 0x0600079F RID: 1951 RVA: 0x000FDB28 File Offset: 0x000FDB28
    public AgentHarnessPlace Place { get; } = place;

    // Token: 0x060007A0 RID: 1952 RVA: 0x000FDB30 File Offset: 0x000FDB30
    public async Task RunTurnAsync(AgentHarnessTurn turn, Action<AgentHarnessEvent> emit, CancellationToken ct)
    {
      AgentHarnessRunnerProcess.Endpoint endpoint2 = await AgentHarnessRunnerProcess.EnsureRunningAsync(ct).ConfigureAwait(false);
      AgentHarnessRunnerProcess.Endpoint endpoint = endpoint2;
      string job = turn.HarnessThreadId;
      JsonObject jsonObject = await AgentHarnessRunnerRuntime.RequestAsync(endpoint, HttpMethod.Get, "/events?job=" + AgentHarnessRunnerRuntime.Escaped(job) + "&after=0&wait=0", null, ct).ConfigureAwait(false);
      long num;
      if (jsonObject["latest"] == null)
      {
        JsonArray jsonArray;
        if ((jsonArray = jsonObject["events"] as JsonArray) == null)
        {
          jsonArray = new JsonArray(null);
        }
        num = (from e in jsonArray.OfType<JsonObject>()
          select AgentHarnessRunnerRuntime.Long(e["seq"])).DefaultIfEmpty(0L).Max();
      }
      else
      {
        num = AgentHarnessRunnerRuntime.Long(jsonObject["latest"]);
      }
      long cursor = num;
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["job"] = job;
      jsonObject2["harness"] = this.Harness.RawValue();
      jsonObject2["place"] = this.Place.RawValue();
      jsonObject2["model"] = turn.Model.RawValue();
      jsonObject2["workspace"] = turn.Workspace;
      jsonObject2["instructions"] = turn.Context.Instructions;
      jsonObject2["mcp"] = new JsonArray(turn.Context.McpServers.Select<AgentHarnessMcpServer, JsonNode>(delegate(AgentHarnessMcpServer server)
      {
        AgentHarnessLocalMcpCommand localCommand = server.LocalCommand;
        if (localCommand != null)
        {
          JsonObject jsonObject8 = new JsonObject(null);
          jsonObject8["name"] = server.Name;
          jsonObject8["command"] = localCommand.Command;
          jsonObject8["args"] = new JsonArray(localCommand.Arguments.Select<string, JsonNode>((string a) => a).ToArray<JsonNode>());
          jsonObject8["env"] = new JsonObject(localCommand.Environment.Select<KeyValuePair<string, string>, KeyValuePair<string, JsonNode>>((KeyValuePair<string, string> kv) => KeyValuePair.Create<string, JsonNode>(kv.Key, kv.Value)), null);
          return jsonObject8;
        }
        JsonObject jsonObject9 = new JsonObject(null);
        jsonObject9["name"] = server.Name;
        jsonObject9["url"] = server.Url;
        JsonObject jsonObject10 = jsonObject9;
        string bearerToken = server.BearerToken;
        if (bearerToken != null)
        {
          jsonObject10["token"] = bearerToken;
        }
        if (server.Headers.Count > 0)
        {
          jsonObject10["headers"] = new JsonObject(server.Headers.Select<KeyValuePair<string, string>, KeyValuePair<string, JsonNode>>((KeyValuePair<string, string> h) => KeyValuePair.Create<string, JsonNode>(h.Key, h.Value)), null);
        }
        if (server.SendsTurnIdentity)
        {
          string turnId = turn.TurnId;
          if (turnId != null)
          {
            JsonNode jsonNode4 = jsonObject10;
            string text4 = "meta";
            JsonObject jsonObject11 = new JsonObject(null);
            string text5 = "x-codex-turn-metadata";
            JsonObject jsonObject12 = new JsonObject(null);
            jsonObject12["thread_id"] = turn.AppThreadId;
            jsonObject12["turn_id"] = turnId;
            jsonObject11[text5] = jsonObject12;
            jsonNode4[text4] = jsonObject11;
          }
        }
        return jsonObject10;
      }).ToArray<JsonNode>());
      jsonObject2["skills"] = new JsonArray(turn.Context.SkillDirectories.Select<string, JsonNode>((string d) => d).ToArray<JsonNode>());
      jsonObject2["userSkills"] = new JsonArray(turn.Context.UserSkillDirectories.Select<string, JsonNode>((string d) => d).ToArray<JsonNode>());
      jsonObject2["leaseMetadata"] = new JsonObject((turn.LeaseMetadata ?? new Dictionary<string, string>()).Select<KeyValuePair<string, string>, KeyValuePair<string, JsonNode>>((KeyValuePair<string, string> kv) => KeyValuePair.Create<string, JsonNode>(kv.Key, kv.Value)), null);
      jsonObject2["deviceHelper"] = AgentHarnessPreferences.UsesDeviceHelper;
      string text = "worker";
      JsonObject jsonObject3 = new JsonObject(null);
      jsonObject3["base"] = turn.Context.WorkerBaseUrl;
      jsonObject3["sessionToken"] = turn.Context.SessionToken;
      jsonObject2[text] = jsonObject3;
      jsonObject2["text"] = turn.Text;
      jsonObject2["images"] = new JsonArray(turn.ImagePaths.Select<string, JsonNode>((string p) => p).ToArray<JsonNode>());
      JsonObject jsonObject4 = jsonObject2;
      if (turn.Context.UsesPlanTool)
      {
        jsonObject4["planTool"] = true;
      }
      string notePath = turn.Context.NotePath;
      if (notePath != null)
      {
        JsonNode jsonNode = jsonObject4;
        string text2 = "note";
        JsonObject jsonObject5 = new JsonObject(null);
        jsonObject5["path"] = notePath;
        jsonObject5["kept"] = turn.Context.IsKeptThing;
        jsonObject5["tools"] = turn.Context.WritesNoteWithTools;
        jsonNode[text2] = jsonObject5;
      }
      JsonObject jsonObject6 = await AgentHarnessRunnerRuntime.RequestAsync(endpoint, HttpMethod.Post, "/turn", jsonObject4, ct).ConfigureAwait(false);
      if (jsonObject6["after"] != null)
      {
        cursor = Math.Max(cursor, AgentHarnessRunnerRuntime.Long(jsonObject6["after"]));
      }
      JsonNode jsonNode2 = jsonObject6["turnRef"];
      string thisTurnRef = ((jsonNode2 != null) ? jsonNode2.GetValue<string>() : null);
      for (;;)
      {
        AgentHarnessRunnerProcess.Endpoint endpoint3 = endpoint;
        HttpMethod get = HttpMethod.Get;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 2);
        defaultInterpolatedStringHandler.AppendLiteral("/events?job=");
        defaultInterpolatedStringHandler.AppendFormatted(AgentHarnessRunnerRuntime.Escaped(job));
        defaultInterpolatedStringHandler.AppendLiteral("&after=");
        defaultInterpolatedStringHandler.AppendFormatted<long>(cursor);
        defaultInterpolatedStringHandler.AppendLiteral("&wait=20000");
        ConfiguredTaskAwaitable<JsonObject>.ConfiguredTaskAwaiter configuredTaskAwaiter = AgentHarnessRunnerRuntime.RequestAsync(endpoint3, get, defaultInterpolatedStringHandler.ToStringAndClear(), null, ct).ConfigureAwait(false).GetAwaiter();
        if (!configuredTaskAwaiter.IsCompleted)
        {
          await configuredTaskAwaiter;
          ConfiguredTaskAwaitable<JsonObject>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
          configuredTaskAwaiter = configuredTaskAwaiter2;
          configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<JsonObject>.ConfiguredTaskAwaiter);
        }
        JsonArray jsonArray2;
        if ((jsonArray2 = configuredTaskAwaiter.GetResult()["events"] as JsonArray) == null)
        {
          jsonArray2 = new JsonArray(null);
        }
        using (IEnumerator<JsonObject> enumerator = jsonArray2.OfType<JsonObject>().GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            JsonObject jsonObject7 = enumerator.Current;
            cursor = Math.Max(cursor, AgentHarnessRunnerRuntime.Long(jsonObject7["seq"]));
            if (thisTurnRef != null)
            {
              JsonNode jsonNode3 = jsonObject7["turnRef"];
              string text3 = ((jsonNode3 != null) ? jsonNode3.GetValue<string>() : null);
              if (text3 != null && text3 != thisTurnRef)
              {
                continue;
              }
            }
            if (AgentHarnessRunnerRuntime.Deliver(jsonObject7, emit))
            {
              return;
            }
          }
          continue;
        }
        break;
      }
    }

    // Token: 0x060007A1 RID: 1953 RVA: 0x000FDB8C File Offset: 0x000FDB8C
    internal static bool Deliver(JsonObject e, Action<AgentHarnessEvent> emit)
    {
      JsonNode jsonNode = e["text"];
      string text = ((jsonNode != null && jsonNode.GetValueKind() == JsonValueKind.String) ? e["text"].ToString() : "");
      JsonNode jsonNode2 = e["kind"];
      string text2 = ((jsonNode2 != null) ? jsonNode2.ToString() : null);
      if (text2 != null)
      {
        switch (text2.Length)
        {
        case 3:
          if (text2 == "log")
          {
            Log.Info("Harness", "[harness] " + ((text.Length > 600) ? text.Substring(0, 600) : text));
          }
          break;
        case 4:
        {
          char c = text2[0];
          if (c != 'n')
          {
            if (c != 'p')
            {
              if (c == 't')
              {
                if (text2 == "tool")
                {
                  JsonNode jsonNode3 = e["id"];
                  string text3 = ((jsonNode3 != null) ? jsonNode3.ToString() : null) ?? Guid.NewGuid().ToString("D");
                  JsonNode jsonNode4 = e["name"];
                  string text4 = ((jsonNode4 != null) ? jsonNode4.ToString() : null) ?? "tool";
                  JsonNode jsonNode5 = e["server"];
                  string text5 = ((jsonNode5 != null) ? jsonNode5.ToString() : null);
                  JsonNode jsonNode6 = e["command"];
                  string text6 = ((jsonNode6 != null) ? jsonNode6.ToString() : null);
                  JsonNode jsonNode7 = e["phase"];
                  bool flag = ((jsonNode7 != null) ? jsonNode7.ToString() : null) == "start";
                  JsonNode jsonNode8 = e["url"];
                  string text7 = ((jsonNode8 != null) ? jsonNode8.ToString() : null);
                  string text8 = ((text7 != null && text7.Length > 0) ? text7 : null);
                  if (text4 == "web_search")
                  {
                    if (flag)
                    {
                      string text9 = text3;
                      JsonNode jsonNode9 = e["query"];
                      emit(new AgentHarnessEvent.WebSearch(text9, ((jsonNode9 != null) ? jsonNode9.ToString() : null) ?? "", text8));
                    }
                  }
                  else if (text4 == "fetch_url" && text8 != null)
                  {
                    if (flag)
                    {
                      emit(new AgentHarnessEvent.WebSearch(text3, "", text8));
                    }
                  }
                  else if (flag)
                  {
                    emit(new AgentHarnessEvent.ToolStarted(text3, text4, text5, text6));
                  }
                  else
                  {
                    emit(new AgentHarnessEvent.ToolFinished(text3, text4, text5, text6));
                  }
                }
              }
            }
            else if (text2 == "plan")
            {
              JsonArray jsonArray;
              if ((jsonArray = e["plan"] as JsonArray) == null)
              {
                jsonArray = new JsonArray(null);
              }
              List<ValueTuple<string, string>> list = (from s in jsonArray.OfType<JsonObject>().Select<JsonObject, ValueTuple<string, string>>(delegate(JsonObject s)
                {
                  JsonNode jsonNode16 = s["step"];
                  string text14;
                  if (jsonNode16 == null)
                  {
                    text14 = null;
                  }
                  else
                  {
                    string text15 = jsonNode16.ToString();
                    text14 = ((text15 != null) ? text15.Trim() : null);
                  }
                  string text16 = text14 ?? "";
                  JsonNode jsonNode17 = s["status"];
                  return new ValueTuple<string, string>(text16, ((jsonNode17 != null) ? jsonNode17.ToString() : null) ?? "pending");
                })
                where s.Item1.Length > 0
                select s).ToList<ValueTuple<string, string>>();
              if (list.Count > 0)
              {
                emit(new AgentHarnessEvent.Plan(list));
              }
            }
          }
          else if (text2 == "note")
          {
            JsonNode jsonNode10 = e["path"];
            string text10 = ((jsonNode10 != null) ? jsonNode10.ToString() : null);
            if (text10 != null)
            {
              JsonNode jsonNode11 = e["phase"];
              if (((jsonNode11 != null) ? jsonNode11.ToString() : null) == "preview")
              {
                if (text.Length > 0)
                {
                  emit(new AgentHarnessEvent.NotePreview(text10, text));
                }
              }
              else
              {
                emit(new AgentHarnessEvent.NoteSaved(text10));
              }
            }
          }
          break;
        }
        case 5:
        {
          char c = text2[0];
          if (c != 'b')
          {
            if (c == 's')
            {
              if (text2 == "stats")
              {
                Dictionary<string, double> dictionary = new Dictionary<string, double>();
                foreach (KeyValuePair<string, JsonNode> keyValuePair in e)
                {
                  string text11;
                  JsonNode jsonNode12;
                  keyValuePair.Deconstruct(out text11, out jsonNode12);
                  string text12 = text11;
                  JsonNode jsonNode13 = jsonNode12;
                  if (text12 != "seq")
                  {
                    JsonValue jsonValue = jsonNode13 as JsonValue;
                    double num;
                    if (jsonValue != null && jsonValue.GetValueKind() == JsonValueKind.Number && jsonValue.TryGetValue<double>(out num))
                    {
                      dictionary[text12] = num;
                    }
                  }
                }
                emit(new AgentHarnessEvent.TurnStats(dictionary));
              }
            }
          }
          else if (text2 == "batch")
          {
            ClickyProgramBatch clickyProgramBatch = ClickyProgramBatch.FromEvent(e);
            if (clickyProgramBatch != null)
            {
              emit(new AgentHarnessEvent.ProgramBatch(clickyProgramBatch));
            }
          }
          break;
        }
        case 6:
          if (text2 == "answer")
          {
            if (text.Length > 0)
            {
              emit(new AgentHarnessEvent.AnswerDelta(text));
            }
          }
          break;
        case 8:
        {
          char c = text2[1];
          if (c != 'h')
          {
            if (c == 'u')
            {
              if (text2 == "turn_end")
              {
                JsonNode jsonNode14 = e["status"];
                string text13 = ((jsonNode14 != null) ? jsonNode14.ToString() : null) ?? "completed";
                JsonNode jsonNode15 = e["error"];
                emit(new AgentHarnessEvent.Finished(text13, (jsonNode15 != null) ? jsonNode15.ToString() : null));
                return true;
              }
            }
          }
          else if (text2 == "thinking")
          {
            if (text.Trim().Length > 0)
            {
              emit(new AgentHarnessEvent.Thinking(text));
            }
          }
          break;
        }
        case 9:
          if (text2 == "compacted")
          {
            emit(new AgentHarnessEvent.Compacted());
          }
          break;
        case 10:
          if (text2 == "commentary")
          {
            if (text.Trim().Length > 0)
            {
              emit(new AgentHarnessEvent.Commentary(text));
            }
          }
          break;
        case 12:
          if (text2 == "tools_listed")
          {
            JsonArray jsonArray2 = e["servers"] as JsonArray;
            if (jsonArray2 != null && jsonArray2.Count > 0)
            {
              Log.Info("Harness", "a job's tools: " + string.Join(" · ", jsonArray2.OfType<JsonObject>().Select<JsonObject, string>(delegate(JsonObject listedServer)
              {
                JsonNode jsonNode18 = listedServer["server"];
                string text17 = ((jsonNode18 != null) ? jsonNode18.ToString() : null) ?? "?";
                JsonValue jsonValue2 = listedServer["tools"] as JsonValue;
                int num3;
                int num2 = ((jsonValue2 != null && jsonValue2.TryGetValue<int>(out num3)) ? num3 : 0);
                JsonValue jsonValue3 = listedServer["ok"] as JsonValue;
                bool flag2;
                if (jsonValue3 != null && jsonValue3.TryGetValue<bool>(out flag2) && !flag2)
                {
                  JsonNode jsonNode19 = listedServer["error"];
                  string text18 = ((jsonNode19 != null) ? jsonNode19.ToString() : null) ?? "no answer";
                  return text17 + ": no tools (couldn't list them: " + ((text18.Length > 200) ? text18.Substring(0, 200) : text18) + ")";
                }
                DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 2);
                defaultInterpolatedStringHandler.AppendFormatted(text17);
                defaultInterpolatedStringHandler.AppendLiteral(": ");
                defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
                defaultInterpolatedStringHandler.AppendLiteral(" tools");
                return defaultInterpolatedStringHandler.ToStringAndClear();
              })));
            }
          }
          break;
        case 16:
          if (text2 == "apps_unreachable")
          {
            emit(new AgentHarnessEvent.ConnectedAppsUnreachable());
          }
          break;
        }
      }
      return false;
    }

    // Token: 0x060007A2 RID: 1954 RVA: 0x000FE264 File Offset: 0x000FE264
    public async Task<bool> SteerAsync(string harnessThreadId, string text, bool informational, CancellationToken ct, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> imagePaths = null)
    {
      AgentHarnessRunnerProcess.Endpoint endpoint = await AgentHarnessRunnerProcess.EnsureRunningAsync(ct).ConfigureAwait(false);
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["job"] = harnessThreadId;
      jsonObject["text"] = text;
      JsonObject jsonObject2 = jsonObject;
      if (informational)
      {
        jsonObject2["info"] = true;
      }
      if (imagePaths != null && imagePaths.Count > 0)
      {
        jsonObject2["images"] = new JsonArray(imagePaths.Select<string, JsonNode>((string p) => p).ToArray<JsonNode>());
      }
      JsonObject jsonObject3 = await AgentHarnessRunnerRuntime.RequestAsync(endpoint, HttpMethod.Post, "/steer", jsonObject2, ct).ConfigureAwait(false);
      JsonValue jsonValue = jsonObject3["ok"] as JsonValue;
      bool accepted;
      bool flag;
      if (jsonValue != null && jsonValue.TryGetValue<bool>(out accepted) && accepted)
      {
        flag = true;
      }
      else
      {
        JsonValue jsonValue2 = jsonObject3["reason"] as JsonValue;
        string text2;
        if (jsonValue2 == null || !jsonValue2.TryGetValue<string>(out text2) || !(text2 == "not running"))
        {
          throw new AgentHarnessException("This agent can't take a message right now. Send it again once it finishes.", false, false);
        }
        flag = false;
      }
      return flag;
    }

    // Token: 0x060007A3 RID: 1955 RVA: 0x000FE2CC File Offset: 0x000FE2CC
    public async Task InterruptAsync(string harnessThreadId)
    {
      try
      {
        AgentHarnessRunnerProcess.Endpoint endpoint = await AgentHarnessRunnerProcess.EnsureRunningAsync(default(CancellationToken)).ConfigureAwait(false);
        HttpMethod post = HttpMethod.Post;
        string text = "/cancel";
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["job"] = harnessThreadId;
        await AgentHarnessRunnerRuntime.RequestAsync(endpoint, post, text, jsonObject, CancellationToken.None).ConfigureAwait(false);
      }
      catch (Exception ex)
      {
        Log.Warn("Harness", "cancel " + harnessThreadId + ": " + ex.Message);
      }
    }

    // Token: 0x060007A4 RID: 1956 RVA: 0x000FE310 File Offset: 0x000FE310
    private static string Escaped(string value)
    {
      return Uri.EscapeDataString(value);
    }

    // Token: 0x060007A5 RID: 1957 RVA: 0x000FE318 File Offset: 0x000FE318
    [NullableContext(2)]
    private static long Long(JsonNode n)
    {
      JsonValue jsonValue = n as JsonValue;
      long num;
      if (jsonValue != null && jsonValue.TryGetValue<long>(out num))
      {
        return num;
      }
      JsonValue jsonValue2 = n as JsonValue;
      double num2;
      if (jsonValue2 == null || !jsonValue2.TryGetValue<double>(out num2))
      {
        return 0L;
      }
      return (long)num2;
    }

    // Token: 0x060007A6 RID: 1958 RVA: 0x000FE354 File Offset: 0x000FE354
    private static async Task<JsonObject> RequestAsync(AgentHarnessRunnerProcess.Endpoint endpoint, HttpMethod method, string route, [Nullable(2)] JsonObject body, CancellationToken ct)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 2);
      defaultInterpolatedStringHandler.AppendLiteral("http://127.0.0.1:");
      defaultInterpolatedStringHandler.AppendFormatted<int>(endpoint.Port);
      defaultInterpolatedStringHandler.AppendFormatted(route);
      JsonObject jsonObject3;
      using (HttpRequestMessage request = new HttpRequestMessage(method, defaultInterpolatedStringHandler.ToStringAndClear()))
      {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", endpoint.Token);
        if (body != null)
        {
          request.Content = new StringContent(body.ToJsonString(null), Encoding.UTF8, "application/json");
        }
        using (CancellationTokenSource timeout = CancellationTokenSource.CreateLinkedTokenSource(ct))
        {
          timeout.CancelAfter(TimeSpan.FromSeconds(60L));
          HttpResponseMessage httpResponseMessage = await AgentHarnessRunnerRuntime.Http.SendAsync(request, timeout.Token).ConfigureAwait(false);
          using (HttpResponseMessage response = httpResponseMessage)
          {
            string text = await response.Content.ReadAsStringAsync(timeout.Token).ConfigureAwait(false);
            JsonObject jsonObject2;
            try
            {
              JsonObject jsonObject;
              if ((jsonObject = JsonNode.Parse(text, null, default(JsonDocumentOptions)) as JsonObject) == null)
              {
                jsonObject = new JsonObject(null);
              }
              jsonObject2 = jsonObject;
            }
            catch (JsonException)
            {
              jsonObject2 = new JsonObject(null);
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
              JsonNode jsonNode = jsonObject2["error"];
              string text2;
              if ((text2 = ((jsonNode != null) ? jsonNode.ToString() : null)) == null)
              {
                defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 1);
                defaultInterpolatedStringHandler.AppendLiteral("The agent harness runner answered ");
                defaultInterpolatedStringHandler.AppendFormatted<int>((int)response.StatusCode);
                defaultInterpolatedStringHandler.AppendLiteral(".");
                text2 = defaultInterpolatedStringHandler.ToStringAndClear();
              }
              throw new AgentHarnessException(text2, false, false);
            }
            jsonObject3 = jsonObject2;
          }
        }
      }
      return jsonObject3;
    }

    // Token: 0x040002D6 RID: 726
    private static readonly HttpClient Http = new HttpClient(new SocketsHttpHandler
    {
      UseProxy = false,
      PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2L)
    })
    {
      Timeout = Timeout.InfiniteTimeSpan
    };
  }
}
