using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000C5 RID: 197
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ProactiveAgentsClient
  {
    // Token: 0x06000CF9 RID: 3321 RVA: 0x0038073C File Offset: 0x0038073C
    public ProactiveAgentsClient(WorkerClient worker)
    {
      this.worker = worker;
    }

    // Token: 0x06000CFA RID: 3322 RVA: 0x0038074C File Offset: 0x0038074C
    public async Task<ProactiveJobStatus> StartJobAsync(ProactiveRequestInputs inputs, DateTimeOffset now, CancellationToken ct = default(CancellationToken))
    {
      string text = ProactiveAgentsClient.BuildRequestBody(inputs, now).ToJsonString(null);
      ProactiveJobStatus proactiveJobStatus;
      WorkerHttpException ex;
      object obj2;
      bool flag;
      int status;
      try
      {
        object obj = await this.worker.SendRawAsync(HttpMethod.Post, "/proactive-agents/jobs", ProactiveAgentsClient.Json(text), true, ct, new TimeSpan?(TimeSpan.FromSeconds(30L)), null).ConfigureAwait(false);
        if (obj == null)
        {
          throw new InvalidOperationException("Sign in to get suggestions.");
        }
        proactiveJobStatus = ProactiveAgentsClient.ParseJob(obj, inputs.CheckReason, now);
      }
      catch when (delegate
      {
        // Failed to create a 'catch-when' expression
        ex = obj2 as WorkerHttpException;
        if (ex == null)
        {
          flag = false;
        }
        else
        {
          status = ex.Status;
          flag = (status == 429 || status == 503) > false;
        }
        endfilter(flag);
      })
      {
        string text2;
        if ((text2 = ProactiveAgentsClient.ErrorMessage(ex.Body)) == null)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 1);
          defaultInterpolatedStringHandler.AppendLiteral("The suggestion request failed (HTTP ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(ex.Status);
          defaultInterpolatedStringHandler.AppendLiteral(").");
          text2 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        throw new InvalidOperationException(text2);
      }
      catch (WorkerHttpException ex2)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 1);
        defaultInterpolatedStringHandler.AppendLiteral("The suggestion request failed (HTTP ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(ex2.Status);
        defaultInterpolatedStringHandler.AppendLiteral(").");
        throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
      }
      return proactiveJobStatus;
    }

    // Token: 0x06000CFB RID: 3323 RVA: 0x003807A8 File Offset: 0x003807A8
    public async Task<ProactiveJobStatus> PollJobAsync(string jobId, ProactiveCheckReason reason, DateTimeOffset now, CancellationToken ct = default(CancellationToken))
    {
      object obj = await this.worker.SendRawAsync(HttpMethod.Get, "/proactive-agents/jobs/" + Uri.EscapeDataString(jobId), null, true, ct, new TimeSpan?(TimeSpan.FromSeconds(20L)), null).ConfigureAwait(false);
      if (obj == null)
      {
        throw new InvalidOperationException("Signed out.");
      }
      return ProactiveAgentsClient.ParseJob(obj, reason, now);
    }

    // Token: 0x06000CFC RID: 3324 RVA: 0x0038080C File Offset: 0x0038080C
    public async Task ReportResolutionAsync(IReadOnlyList<int> suggestionIds, string status, [Nullable(2)] string denyReason = null)
    {
      if (suggestionIds.Count != 0)
      {
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["suggestion_ids"] = new JsonArray(suggestionIds.Select<int, JsonNode>((int i) => i).ToArray<JsonNode>());
        jsonObject["status"] = status;
        JsonObject jsonObject2 = jsonObject;
        if (denyReason != null)
        {
          jsonObject2["deny_reason"] = denyReason;
        }
        try
        {
          WorkerClient workerClient = this.worker;
          HttpMethod post = HttpMethod.Post;
          string text = "/proactive-agents/resolution";
          HttpContent httpContent = ProactiveAgentsClient.Json(jsonObject2.ToJsonString(null));
          bool flag = true;
          TimeSpan? timeSpan = new TimeSpan?(TimeSpan.FromSeconds(20L));
          await workerClient.SendRawAsync(post, text, httpContent, flag, default(CancellationToken), timeSpan, null).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
          Log.Info("Proactive", "resolution " + status + " not recorded: " + ex.Message);
        }
      }
    }

    // Token: 0x06000CFD RID: 3325 RVA: 0x00380868 File Offset: 0x00380868
    [return: Nullable(new byte[] { 1, 2, 1 })]
    public async Task<IReadOnlyDictionary<int, string>> FetchStatusesAsync(IReadOnlyList<int> suggestionIds, CancellationToken ct = default(CancellationToken))
    {
      IReadOnlyDictionary<int, string> readOnlyDictionary;
      if (suggestionIds.Count == 0)
      {
        readOnlyDictionary = null;
      }
      else
      {
        try
        {
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["suggestion_ids"] = new JsonArray(suggestionIds.Select<int, JsonNode>((int i) => i).ToArray<JsonNode>());
          JsonObject jsonObject2 = jsonObject;
          byte[] array = await this.worker.SendRawAsync(HttpMethod.Post, "/proactive-agents/suggestions/statuses", ProactiveAgentsClient.Json(jsonObject2.ToJsonString(null)), true, ct, new TimeSpan?(TimeSpan.FromSeconds(15L)), null).ConfigureAwait(false);
          if (array != null)
          {
            JsonNode jsonNode = JsonNode.Parse(array, null, default(JsonDocumentOptions));
            JsonObject jsonObject3 = ((jsonNode != null) ? jsonNode["statuses"] : null) as JsonObject;
            if (jsonObject3 != null)
            {
              Dictionary<int, string> dictionary = new Dictionary<int, string>();
              foreach (KeyValuePair<string, JsonNode> keyValuePair in jsonObject3)
              {
                string text;
                JsonNode jsonNode2;
                keyValuePair.Deconstruct(out text, out jsonNode2);
                string text2 = text;
                JsonNode jsonNode3 = jsonNode2;
                int num;
                if (int.TryParse(text2, out num))
                {
                  string text3 = ((jsonNode3 != null) ? jsonNode3.GetValue<string>() : null);
                  if (text3 != null)
                  {
                    dictionary[num] = text3;
                  }
                }
              }
              return dictionary;
            }
          }
          readOnlyDictionary = null;
        }
        catch (Exception ex)
        {
          Log.Info("Proactive", "status sync: " + ex.Message);
          readOnlyDictionary = null;
        }
      }
      return readOnlyDictionary;
    }

    // Token: 0x06000CFE RID: 3326 RVA: 0x003808BC File Offset: 0x003808BC
    public async Task<bool> ClaimMorningInvitationAsync(IReadOnlyList<int> suggestionIds, CancellationToken ct = default(CancellationToken))
    {
      bool flag;
      if (suggestionIds.Count == 0)
      {
        flag = true;
      }
      else
      {
        try
        {
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["suggestion_ids"] = new JsonArray(suggestionIds.Select<int, JsonNode>((int i) => i).ToArray<JsonNode>());
          JsonObject jsonObject2 = jsonObject;
          byte[] array = await this.worker.SendRawAsync(HttpMethod.Post, "/proactive-agents/invitation-claim", ProactiveAgentsClient.Json(jsonObject2.ToJsonString(null)), true, ct, new TimeSpan?(TimeSpan.FromSeconds(10L)), null).ConfigureAwait(false);
          bool flag2;
          if (array != null)
          {
            JsonNode jsonNode = JsonNode.Parse(array, null, default(JsonDocumentOptions));
            if (jsonNode == null)
            {
              flag2 = true;
            }
            else
            {
              JsonNode jsonNode2 = jsonNode["granted"];
              bool? flag3 = ((jsonNode2 != null) ? new bool?(jsonNode2.GetValue<bool>()) : null);
              bool flag4 = false;
              flag2 = !((flag3.GetValueOrDefault() == flag4) & (flag3 != null));
            }
          }
          else
          {
            flag2 = true;
          }
          flag = flag2;
        }
        catch (Exception)
        {
          flag = true;
        }
      }
      return flag;
    }

    // Token: 0x06000CFF RID: 3327 RVA: 0x00380910 File Offset: 0x00380910
    public static JsonObject BuildRequestBody(ProactiveRequestInputs inputs, DateTimeOffset now)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["connected_integrations"] = new JsonArray(inputs.ConnectedIntegrationNames.Select<string, JsonNode>((string n) => n).ToArray<JsonNode>());
      jsonObject["check_reason"] = inputs.CheckReason.Wire();
      jsonObject["recent_agent_suggestions"] = new JsonArray(ProactiveHistory.WireEntries(inputs.RecentSuggestions, now).Select<ProactiveHistoryEntry, JsonNode>(delegate(ProactiveHistoryEntry e)
      {
        JsonObject jsonObject3 = new JsonObject(null);
        jsonObject3["title"] = e.Title;
        jsonObject3["description"] = e.Description ?? "";
        jsonObject3["status"] = e.WireStatus;
        jsonObject3["hours_ago"] = Math.Max(0.0, (now - e.DecidedAt).TotalHours);
        JsonObject jsonObject4 = jsonObject3;
        if (e.DenyReason != null)
        {
          jsonObject4["deny_reason"] = e.DenyReason;
        }
        return jsonObject4;
      }).ToArray<JsonNode>());
      jsonObject["local_day"] = ProactiveAgentsClient.LocalDay(now);
      jsonObject["jobs"] = true;
      jsonObject["reports_suggestion_views"] = true;
      JsonObject jsonObject2 = jsonObject;
      if (inputs.ResearchOnlyForGoodMorning)
      {
        jsonObject2["research_only"] = true;
      }
      if (inputs.ResearchOnlyForGoodMorning || inputs.ReadsForMorningPaper)
      {
        jsonObject2["brief_style"] = "good_morning";
      }
      if (inputs.ExistingRoutines.Count > 0)
      {
        jsonObject2["routines"] = new JsonArray(inputs.ExistingRoutines.Select<ValueTuple<string, int>, JsonNode>(delegate([TupleElementNames(new string[] { "Title", "EveryMinutes" })] ValueTuple<string, int> r)
        {
          JsonObject jsonObject5 = new JsonObject(null);
          jsonObject5["title"] = r.Item1;
          jsonObject5["every_minutes"] = r.Item2;
          return jsonObject5;
        }).ToArray<JsonNode>());
      }
      if (inputs.CheckReason == ProactiveCheckReason.Morning)
      {
        jsonObject2["supports_morning_notch"] = true;
      }
      if (inputs.Clickys.Count > 0)
      {
        jsonObject2["clickys"] = new JsonArray(inputs.Clickys.Select<ProactiveClickyDescriptor, JsonNode>(delegate(ProactiveClickyDescriptor c)
        {
          JsonObject jsonObject6 = new JsonObject(null);
          jsonObject6["slug"] = c.Slug;
          jsonObject6["name"] = c.Name;
          jsonObject6["role"] = c.Role;
          jsonObject6["description"] = c.Description;
          jsonObject6["routines"] = new JsonArray(c.Routines.Select<ValueTuple<string, int>, JsonNode>(delegate([TupleElementNames(new string[] { "Title", "EveryMinutes" })] ValueTuple<string, int> r)
          {
            JsonObject jsonObject7 = new JsonObject(null);
            jsonObject7["title"] = r.Item1;
            jsonObject7["every_minutes"] = r.Item2;
            return jsonObject7;
          }).ToArray<JsonNode>());
          jsonObject6["thread_ids"] = new JsonArray(c.ThreadIds.Select<string, JsonNode>((string t) => t).ToArray<JsonNode>());
          return jsonObject6;
        }).ToArray<JsonNode>());
      }
      if (inputs.CustomConnectors.Count > 0)
      {
        jsonObject2["custom_connectors"] = new JsonArray(inputs.CustomConnectors.Select<ValueTuple<string, string>, JsonNode>(delegate([TupleElementNames(new string[] { "Name", "Description" })] ValueTuple<string, string> c)
        {
          JsonObject jsonObject8 = new JsonObject(null);
          jsonObject8["name"] = c.Item1;
          jsonObject8["description"] = c.Item2;
          return jsonObject8;
        }).ToArray<JsonNode>());
      }
      return jsonObject2;
    }

    // Token: 0x06000D00 RID: 3328 RVA: 0x00380B48 File Offset: 0x00380B48
    public static string LocalDay(DateTimeOffset now)
    {
      return now.LocalDateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }

    // Token: 0x06000D01 RID: 3329 RVA: 0x00380B70 File Offset: 0x00380B70
    public static ProactiveJobStatus ParseJob(byte[] bytes, ProactiveCheckReason reason, DateTimeOffset now)
    {
      JsonObject jsonObject = JsonNode.Parse(bytes, null, default(JsonDocumentOptions)) as JsonObject;
      if (jsonObject == null)
      {
        throw new InvalidOperationException("The suggestion service returned an invalid response.");
      }
      ProactiveAgentsClient.<>c__DisplayClass9_0 CS$<>8__locals1;
      CS$<>8__locals1.o = jsonObject;
      JsonNode jsonNode = CS$<>8__locals1.o["status"];
      string text = ((jsonNode != null) ? jsonNode.GetValue<string>() : null);
      ProactiveJobState proactiveJobState;
      if (!(text == "running"))
      {
        if (!(text == "succeeded"))
        {
          if (!(text == "failed"))
          {
            proactiveJobState = ProactiveJobState.Queued;
          }
          else
          {
            proactiveJobState = ProactiveJobState.Failed;
          }
        }
        else
        {
          proactiveJobState = ProactiveJobState.Succeeded;
        }
      }
      else
      {
        proactiveJobState = ProactiveJobState.Running;
      }
      ProactiveJobState proactiveJobState2 = proactiveJobState;
      IReadOnlyList<ProactiveSuggestionCard> readOnlyList = null;
      JsonArray jsonArray = CS$<>8__locals1.o["suggested_agents"] as JsonArray;
      if (jsonArray != null)
      {
        readOnlyList = ProactiveAgentsClient.ParseCards(jsonArray, reason, now);
      }
      JsonNode jsonNode2 = CS$<>8__locals1.o["break_until"];
      string text2 = ((jsonNode2 != null) ? jsonNode2.GetValue<string>() : null);
      DateTimeOffset dateTimeOffset2;
      DateTimeOffset? dateTimeOffset = ((text2 != null && DateTimeOffset.TryParse(text2, out dateTimeOffset2)) ? new DateTimeOffset?(dateTimeOffset2) : null);
      JsonNode jsonNode3 = CS$<>8__locals1.o["job_id"];
      string text3 = ((jsonNode3 != null) ? jsonNode3.GetValue<string>() : null) ?? "";
      ProactiveJobState proactiveJobState3 = proactiveJobState2;
      IReadOnlyList<ProactiveSuggestionCard> readOnlyList2 = readOnlyList;
      JsonNode jsonNode4 = CS$<>8__locals1.o["error"];
      return new ProactiveJobStatus(text3, proactiveJobState3, readOnlyList2, (jsonNode4 != null) ? jsonNode4.GetValue<string>() : null, dateTimeOffset)
      {
        CuratedEvidence = ProactiveAgentsClient.<ParseJob>g__Str|9_0("curated_evidence", ref CS$<>8__locals1),
        MorningHeadline = ProactiveAgentsClient.<ParseJob>g__Str|9_0("morning_headline", ref CS$<>8__locals1),
        MorningHeadlineLine = ProactiveAgentsClient.<ParseJob>g__Str|9_0("morning_headline_line", ref CS$<>8__locals1)
      };
    }

    // Token: 0x06000D02 RID: 3330 RVA: 0x00380CFC File Offset: 0x00380CFC
    public static IReadOnlyList<ProactiveSuggestionCard> ParseCards(JsonArray agents, ProactiveCheckReason reason, DateTimeOffset now)
    {
      List<ProactiveSuggestionCard> list = new List<ProactiveSuggestionCard>();
      foreach (JsonObject jsonObject in agents.OfType<JsonObject>())
      {
        ProactiveAgentsClient.<>c__DisplayClass10_0 CS$<>8__locals1;
        CS$<>8__locals1.a = jsonObject;
        string text = ProactiveAgentsClient.<ParseCards>g__Str|10_0("title", ref CS$<>8__locals1);
        string text2 = ProactiveAgentsClient.<ParseCards>g__Str|10_0("description", ref CS$<>8__locals1);
        string text3 = ProactiveAgentsClient.<ParseCards>g__Str|10_0("agent_prompt", ref CS$<>8__locals1);
        if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text3))
        {
          JsonObject jsonObject2 = CS$<>8__locals1.a["new_clicky"] as JsonObject;
          ProactiveNewClicky proactiveNewClicky;
          if (jsonObject2 == null)
          {
            proactiveNewClicky = null;
          }
          else
          {
            JsonNode jsonNode = jsonObject2["name"];
            string text4 = ((jsonNode != null) ? jsonNode.GetValue<string>() : null) ?? "";
            JsonNode jsonNode2 = jsonObject2["role"];
            string text5 = ((jsonNode2 != null) ? jsonNode2.GetValue<string>() : null) ?? "";
            JsonNode jsonNode3 = jsonObject2["description"];
            proactiveNewClicky = new ProactiveNewClicky(text4, text5, ((jsonNode3 != null) ? jsonNode3.GetValue<string>() : null) ?? "");
          }
          ProactiveNewClicky proactiveNewClicky2 = proactiveNewClicky;
          JsonObject jsonObject3 = CS$<>8__locals1.a["routine"] as JsonObject;
          if (jsonObject3 == null)
          {
            goto IL_0135;
          }
          JsonValue jsonValue = jsonObject3["every_minutes"] as JsonValue;
          int num;
          if (jsonValue == null || !jsonValue.TryGetValue<int>(out num))
          {
            goto IL_0135;
          }
          int num2 = num;
          JsonNode jsonNode4 = jsonObject3["title"];
          ProactiveRoutine proactiveRoutine = new ProactiveRoutine(num2, ((jsonNode4 != null) ? jsonNode4.GetValue<string>() : null) ?? "");
          IL_0160:
          ProactiveRoutine proactiveRoutine2 = proactiveRoutine;
          JsonValue jsonValue2 = CS$<>8__locals1.a["suggestion_id"] as JsonValue;
          int num4;
          int? num3 = ((jsonValue2 != null && jsonValue2.TryGetValue<int>(out num4)) ? new int?(num4) : null);
          List<ProactiveSuggestionCard> list2 = list;
          ProactiveSuggestionCard proactiveSuggestionCard = new ProactiveSuggestionCard();
          proactiveSuggestionCard.Title = text;
          proactiveSuggestionCard.Description = text2;
          proactiveSuggestionCard.AgentPrompt = text3;
          proactiveSuggestionCard.CheckReason = reason;
          proactiveSuggestionCard.ServerSuggestionId = num3;
          string text6 = ProactiveAgentsClient.<ParseCards>g__Str|10_0("clicky_slug", ref CS$<>8__locals1);
          proactiveSuggestionCard.ClickySlug = ((text6 != null && text6.Length > 0) ? text6 : null);
          proactiveSuggestionCard.NewClicky = proactiveNewClicky2;
          proactiveSuggestionCard.Routine = proactiveRoutine2;
          string text7 = ProactiveAgentsClient.<ParseCards>g__Str|10_0("integration", ref CS$<>8__locals1);
          proactiveSuggestionCard.IntegrationName = ((text7 != null && text7.Length > 0) ? text7 : null);
          proactiveSuggestionCard.CreatedAt = now;
          list2.Add(proactiveSuggestionCard);
          continue;
          IL_0135:
          proactiveRoutine = null;
          goto IL_0160;
        }
      }
      return list;
    }

    // Token: 0x06000D03 RID: 3331 RVA: 0x00380F70 File Offset: 0x00380F70
    private static ByteArrayContent Json(string json)
    {
      return new ByteArrayContent(Encoding.UTF8.GetBytes(json))
      {
        Headers = 
        {
          ContentType = new MediaTypeHeaderValue("application/json")
        }
      };
    }

    // Token: 0x06000D04 RID: 3332 RVA: 0x00380F98 File Offset: 0x00380F98
    [return: Nullable(2)]
    private static string ErrorMessage(string body)
    {
      string text2;
      try
      {
        JsonNode jsonNode = JsonNode.Parse(body, null, default(JsonDocumentOptions));
        string text;
        if (jsonNode == null)
        {
          text = null;
        }
        else
        {
          JsonNode jsonNode2 = jsonNode["error"];
          text = ((jsonNode2 != null) ? jsonNode2.GetValue<string>() : null);
        }
        text2 = text;
      }
      catch (JsonException)
      {
        text2 = null;
      }
      return text2;
    }

    // Token: 0x06000D05 RID: 3333 RVA: 0x00380FF4 File Offset: 0x00380FF4
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <ParseJob>g__Str|9_0(string key, ref ProactiveAgentsClient.<>c__DisplayClass9_0 A_1)
    {
      JsonValue jsonValue = A_1.o[key] as JsonValue;
      string text;
      if (jsonValue == null || !jsonValue.TryGetValue<string>(out text))
      {
        return null;
      }
      return text;
    }

    // Token: 0x06000D06 RID: 3334 RVA: 0x00381024 File Offset: 0x00381024
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <ParseCards>g__Str|10_0(string k, ref ProactiveAgentsClient.<>c__DisplayClass10_0 A_1)
    {
      JsonValue jsonValue = A_1.a[k] as JsonValue;
      string text;
      if (jsonValue == null || !jsonValue.TryGetValue<string>(out text))
      {
        return null;
      }
      return text.Trim();
    }

    // Token: 0x04000584 RID: 1412
    private readonly WorkerClient worker;
  }
}
