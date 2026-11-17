using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents.Runtime;

namespace HeyClicky.Agents.Backend
{
  // Token: 0x02000073 RID: 115
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class AgentWorkerApi
  {
    // Token: 0x0600090C RID: 2316 RVA: 0x0010309C File Offset: 0x0010309C
    public AgentWorkerApi(Uri workerBaseUrl, HttpClient http)
    {
    }

    // Token: 0x1700024F RID: 591
    // (get) Token: 0x0600090D RID: 2317 RVA: 0x001030B4 File Offset: 0x001030B4
    public Uri WorkerBaseUrl { get; } = workerBaseUrl;

    // Token: 0x0600090E RID: 2318 RVA: 0x001030BC File Offset: 0x001030BC
    private Uri Route(string path)
    {
      return new Uri(this.WorkerBaseUrl, path.TrimStart('/'));
    }

    // Token: 0x0600090F RID: 2319 RVA: 0x001030D4 File Offset: 0x001030D4
    [return: TupleElementNames(new string[] { "Status", "Body" })]
    [return: Nullable(new byte[] { 1, 0, 1 })]
    private async Task<ValueTuple<int, string>> SendAsync(HttpMethod method, string path, string bearer, [Nullable(2)] JsonObject body, CancellationToken ct, TimeSpan timeout, [Nullable(new byte[] { 2, 1, 1 })] IDictionary<string, string> headers = null)
    {
      ValueTuple<int, string> valueTuple;
      using (HttpRequestMessage request = new HttpRequestMessage(method, this.Route(path)))
      {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        if (headers != null)
        {
          foreach (KeyValuePair<string, string> keyValuePair in headers)
          {
            string text;
            string text2;
            keyValuePair.Deconstruct(out text, out text2);
            string text3 = text;
            string text4 = text2;
            request.Headers.TryAddWithoutValidation(text3, text4);
          }
        }
        if (body != null || method == HttpMethod.Post)
        {
          request.Content = new StringContent((body ?? new JsonObject(null)).ToJsonString(null), Encoding.UTF8, "application/json");
        }
        using (CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
        {
          cts.CancelAfter(timeout);
          HttpResponseMessage httpResponseMessage = await this.<http>P.SendAsync(request, cts.Token).ConfigureAwait(false);
          using (HttpResponseMessage response = httpResponseMessage)
          {
            string text5 = await response.Content.ReadAsStringAsync(cts.Token).ConfigureAwait(false);
            valueTuple = new ValueTuple<int, string>((int)response.StatusCode, text5);
          }
        }
      }
      return valueTuple;
    }

    // Token: 0x06000910 RID: 2320 RVA: 0x00103154 File Offset: 0x00103154
    [return: Nullable(2)]
    private static JsonObject ParseObject(string body)
    {
      if (string.IsNullOrWhiteSpace(body))
      {
        return null;
      }
      JsonObject jsonObject;
      try
      {
        jsonObject = JsonNode.Parse(body, null, default(JsonDocumentOptions)) as JsonObject;
      }
      catch (JsonException)
      {
        jsonObject = null;
      }
      return jsonObject;
    }

    // Token: 0x06000911 RID: 2321 RVA: 0x001031A4 File Offset: 0x001031A4
    [return: TupleElementNames(new string[] { "Token", "ExpiresAt", "DeveloperModelSelection" })]
    [return: Nullable(new byte[] { 1, 0, 1 })]
    public async Task<ValueTuple<string, DateTimeOffset, bool>> MintSessionTokenAsync(string supabaseAccessToken, CancellationToken ct)
    {
      object obj = await this.SendAsync(HttpMethod.Post, "/agent/session-token", supabaseAccessToken, new JsonObject(null), ct, TimeSpan.FromSeconds(30L), null).ConfigureAwait(false);
      int item = obj.Item1;
      string item2 = obj.Item2;
      if (item != 200)
      {
        throw new AgentBackendException("/agent/session-token", item, item2);
      }
      JsonObject jsonObject = AgentWorkerApi.ParseObject(item2);
      string text;
      if (jsonObject == null)
      {
        text = null;
      }
      else
      {
        JsonNode jsonNode = jsonObject["token"];
        text = ((jsonNode != null) ? jsonNode.ToString() : null);
      }
      string text2 = text;
      if (string.IsNullOrWhiteSpace(text2))
      {
        throw new AgentBackendException("/agent/session-token", item, "empty token");
      }
      JsonValue jsonValue = jsonObject["expires_at"] as JsonValue;
      double num;
      DateTimeOffset dateTimeOffset = ((jsonValue != null && jsonValue.TryGetValue<double>(out num)) ? DateTimeOffset.FromUnixTimeMilliseconds((long)(num * 1000.0)) : DateTimeOffset.UtcNow.AddMinutes(15.0));
      JsonNode jsonNode2 = jsonObject["capabilities"];
      JsonValue jsonValue2 = ((jsonNode2 != null) ? jsonNode2["developer_model_selection"] : null) as JsonValue;
      bool b;
      bool flag = jsonValue2 != null && jsonValue2.TryGetValue<bool>(out b) && b;
      return new ValueTuple<string, DateTimeOffset, bool>(text2, dateTimeOffset, flag);
    }

    // Token: 0x06000912 RID: 2322 RVA: 0x001031F8 File Offset: 0x001031F8
    public async Task<AgentIntegrationCatalog> ListIntegrationsAsync(string agentSessionToken, CancellationToken ct)
    {
      object obj = await this.SendAsync(HttpMethod.Get, "/agent/integrations?limit=1000", agentSessionToken, null, ct, TimeSpan.FromSeconds(30L), null).ConfigureAwait(false);
      int item = obj.Item1;
      string item2 = obj.Item2;
      if (item < 200 || item >= 300)
      {
        throw new AgentBackendException("/agent/integrations", item, item2);
      }
      return AgentIntegrationsApi.ParseCatalog(AgentWorkerApi.ParseObject(item2));
    }

    // Token: 0x06000913 RID: 2323 RVA: 0x0010324C File Offset: 0x0010324C
    public async Task<string> ConnectIntegrationAsync(string agentSessionToken, string toolkit, CancellationToken ct)
    {
      string path = "/agent/integrations/" + Uri.EscapeDataString(toolkit) + "/connect";
      object obj = await this.SendAsync(HttpMethod.Post, path, agentSessionToken, new JsonObject(null), ct, TimeSpan.FromSeconds(30L), null).ConfigureAwait(false);
      int item = obj.Item1;
      string item2 = obj.Item2;
      if (item < 200 || item >= 300)
      {
        throw new AgentBackendException(path, item, AgentIntegrationsApi.ErrorText(item2, item2));
      }
      JsonObject jsonObject = AgentWorkerApi.ParseObject(item2);
      string text;
      if (jsonObject == null)
      {
        text = null;
      }
      else
      {
        JsonNode jsonNode = jsonObject["redirect_url"];
        text = ((jsonNode != null) ? jsonNode.ToString() : null);
      }
      string text2 = text;
      if (text2 == null || text2.Length <= 0)
      {
        throw new AgentBackendException(path, item, "HeyClicky received an invalid integration connect URL.");
      }
      return text2;
    }

    // Token: 0x06000914 RID: 2324 RVA: 0x001032A8 File Offset: 0x001032A8
    public async Task DisconnectIntegrationAsync(string agentSessionToken, string toolkit, [Nullable(2)] string connectedAccountId, CancellationToken ct)
    {
      string path = "/agent/integrations/" + Uri.EscapeDataString(toolkit) + "/disconnect";
      JsonObject jsonObject;
      if (connectedAccountId != null)
      {
        (jsonObject = new JsonObject(null))["connected_account_id"] = connectedAccountId;
      }
      else
      {
        jsonObject = new JsonObject(null);
      }
      JsonObject jsonObject2 = jsonObject;
      object obj = await this.SendAsync(HttpMethod.Post, path, agentSessionToken, jsonObject2, ct, TimeSpan.FromSeconds(30L), null).ConfigureAwait(false);
      int item = obj.Item1;
      string item2 = obj.Item2;
      if (item < 200 || item >= 300)
      {
        throw new AgentBackendException(path, item, AgentIntegrationsApi.ErrorText(item2, item2));
      }
    }

    // Token: 0x06000915 RID: 2325 RVA: 0x0010330C File Offset: 0x0010330C
    public async Task LabelIntegrationAccountAsync(string agentSessionToken, string toolkit, string connectedAccountId, string label, CancellationToken ct)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 2);
      defaultInterpolatedStringHandler.AppendLiteral("/agent/integrations/");
      defaultInterpolatedStringHandler.AppendFormatted(Uri.EscapeDataString(toolkit));
      defaultInterpolatedStringHandler.AppendLiteral("/accounts/");
      defaultInterpolatedStringHandler.AppendFormatted(Uri.EscapeDataString(connectedAccountId));
      defaultInterpolatedStringHandler.AppendLiteral("/label");
      string path = defaultInterpolatedStringHandler.ToStringAndClear();
      HttpMethod post = HttpMethod.Post;
      string text = path;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["label"] = label;
      object obj = await this.SendAsync(post, text, agentSessionToken, jsonObject, ct, TimeSpan.FromSeconds(20L), null).ConfigureAwait(false);
      int item = obj.Item1;
      string item2 = obj.Item2;
      if (item < 200 || item >= 300)
      {
        throw new AgentBackendException(path, item, AgentIntegrationsApi.ErrorText(item2, item2));
      }
    }

    // Token: 0x06000916 RID: 2326 RVA: 0x0010337C File Offset: 0x0010337C
    [return: TupleElementNames(new string[] { "Kind", "Config", "Error" })]
    [return: Nullable(new byte[] { 1, 0, 2, 2 })]
    public async Task<ValueTuple<AgentWorkerApi.ComposioFetchKind, ComposioMcpConfiguration, Exception>> ComposioSessionAsync(string agentSessionToken, CancellationToken ct)
    {
      int[] retryDelays = new int[] { 1, 3 };
      int attempt = 0;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      Exception ex2;
      for (;;)
      {
        int num = 0;
        object obj2;
        Exception ex;
        bool flag2;
        try
        {
          object obj = await this.SendAsync(HttpMethod.Post, "/agent/composio/session", agentSessionToken, new JsonObject(null), ct, TimeSpan.FromSeconds(20L), null).ConfigureAwait(false);
          int item = obj.Item1;
          string item2 = obj.Item2;
          if (item < 200 || item >= 300)
          {
            throw new AgentBackendException("/agent/composio/session", item, item2);
          }
          JsonObject jsonObject = AgentWorkerApi.ParseObject(item2);
          if (jsonObject == null)
          {
            throw new AgentBackendException("/agent/composio/session", -1, item2);
          }
          JsonObject jsonObject2 = jsonObject;
          JsonValue jsonValue = jsonObject2["enabled"] as JsonValue;
          bool en;
          bool flag = jsonValue != null && jsonValue.TryGetValue<bool>(out en) && en;
          JsonNode jsonNode = jsonObject2["proxy_url"];
          string text = ((jsonNode != null) ? jsonNode.ToString() : null);
          if (!flag || string.IsNullOrEmpty(text))
          {
            return new ValueTuple<AgentWorkerApi.ComposioFetchKind, ComposioMcpConfiguration, Exception>(AgentWorkerApi.ComposioFetchKind.NoConnectedApps, null, null);
          }
          JsonArray jsonArray = jsonObject2["toolkits"] as JsonArray;
          List<string> list;
          if (jsonArray == null)
          {
            list = null;
          }
          else
          {
            list = jsonArray.Select<JsonNode, string>(delegate(JsonNode n)
            {
              if (n == null)
              {
                return null;
              }
              return n.ToString();
            }).OfType<string>().ToList<string>();
          }
          List<string> list2 = list ?? new List<string>();
          List<string> list3 = new List<string>();
          JsonArray jsonArray2;
          if ((jsonArray2 = jsonObject2["multi_account_toolkits"] as JsonArray) == null)
          {
            jsonArray2 = new JsonArray(null);
          }
          foreach (JsonObject jsonObject3 in jsonArray2.OfType<JsonObject>())
          {
            JsonArray jsonArray3;
            if ((jsonArray3 = jsonObject3["accounts"] as JsonArray) == null)
            {
              jsonArray3 = new JsonArray(null);
            }
            List<JsonObject> list4 = jsonArray3.OfType<JsonObject>().ToList<JsonObject>();
            IEnumerable<string> enumerable = list4.Select<JsonObject, string>(delegate(JsonObject a, int i)
            {
              JsonNode jsonNode3 = a["account_name"];
              string text4 = AgentWorkerApi.PromptSafe((jsonNode3 != null) ? jsonNode3.ToString() : null);
              JsonNode jsonNode4 = a["label"];
              string text5 = AgentWorkerApi.PromptSafe((jsonNode4 != null) ? jsonNode4.ToString() : null);
              string text6;
              if (text4 == null || text5 == null)
              {
                if ((text6 = text4) == null && (text6 = text5) == null)
                {
                  DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(8, 1);
                  defaultInterpolatedStringHandler2.AppendLiteral("account ");
                  defaultInterpolatedStringHandler2.AppendFormatted<int>(i + 1);
                  text6 = defaultInterpolatedStringHandler2.ToStringAndClear();
                }
              }
              else
              {
                text6 = text5 + " (" + text4 + ")";
              }
              string text7 = " [account id ";
              JsonNode jsonNode5 = a["connected_account_id"];
              return text6 + text7 + (AgentWorkerApi.PromptSafe((jsonNode5 != null) ? jsonNode5.ToString() : null) ?? "unknown") + "]";
            });
            JsonNode jsonNode2 = jsonObject3["toolkit"];
            string text2 = ((jsonNode2 != null) ? jsonNode2.ToString() : null) ?? "";
            List<string> list5 = list3;
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 3);
            defaultInterpolatedStringHandler.AppendFormatted(AgentWorkerApi.PromptSafe(text2) ?? text2);
            defaultInterpolatedStringHandler.AppendLiteral(" has ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(list4.Count);
            defaultInterpolatedStringHandler.AppendLiteral(" connected accounts: ");
            defaultInterpolatedStringHandler.AppendFormatted(string.Join(", ", enumerable));
            defaultInterpolatedStringHandler.AppendLiteral(".");
            list5.Add(defaultInterpolatedStringHandler.ToStringAndClear());
          }
          return new ValueTuple<AgentWorkerApi.ComposioFetchKind, ComposioMcpConfiguration, Exception>(AgentWorkerApi.ComposioFetchKind.Connected, new ComposioMcpConfiguration(text, list2, list3), null);
        }
        catch when (delegate
        {
          // Failed to create a 'catch-when' expression
          ex = obj2 as Exception;
          if (ex == null)
          {
            flag2 = false;
          }
          else
          {
            ex2 = (Exception)ex;
            flag2 = (!(ex2 is OperationCanceledException) || !ct.IsCancellationRequested) > false;
          }
          endfilter(flag2);
        })
        {
          num = 1;
        }
        if (num == 1)
        {
          bool flag3 = ex2 is HttpRequestException || ex2 is TaskCanceledException;
          if (!flag3)
          {
            AgentBackendException ex3 = ex2 as AgentBackendException;
            if (ex3 == null)
            {
              goto IL_03D9;
            }
            int statusCode = ex3.StatusCode;
            if (statusCode < 500 && statusCode != -1 && statusCode != 408 && statusCode != 429)
            {
              goto IL_03D9;
            }
            bool flag4 = true;
            IL_03DC:
            flag3 = flag4;
            goto IL_03E0;
            IL_03D9:
            flag4 = false;
            goto IL_03DC;
          }
          IL_03E0:
          if (!flag3 || attempt >= retryDelays.Length)
          {
            break;
          }
          await Task.Delay(TimeSpan.FromSeconds((long)retryDelays[attempt]), ct).ConfigureAwait(false);
        }
        num = attempt;
        attempt = num + 1;
      }
      string text3 = "Agents";
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 2);
      defaultInterpolatedStringHandler.AppendLiteral("composio session fetch failed after ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(attempt + 1);
      defaultInterpolatedStringHandler.AppendLiteral(" attempt(s): ");
      defaultInterpolatedStringHandler.AppendFormatted(ex2.Message);
      Log.Warn(text3, defaultInterpolatedStringHandler.ToStringAndClear());
      return new ValueTuple<AgentWorkerApi.ComposioFetchKind, ComposioMcpConfiguration, Exception>(AgentWorkerApi.ComposioFetchKind.Unreachable, null, ex2);
    }

    // Token: 0x06000917 RID: 2327 RVA: 0x001033D0 File Offset: 0x001033D0
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<AgentTurnLease> RecordAgentLaunchAsync(string supabaseAccessToken, [Nullable(2)] string threadId, string turnId, Guid taskId, bool isFollowUp, string launchSource, bool autoApproveExtraUsage, string usageContext, CancellationToken ct)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["supports_agent_turn_lease"] = true;
      jsonObject["thread_id"] = threadId;
      jsonObject["turn_id"] = turnId;
      jsonObject["task_id"] = taskId.ToString("D").ToUpperInvariant();
      jsonObject["is_follow_up"] = isFollowUp;
      jsonObject["launch_source"] = launchSource;
      jsonObject["idempotency_key"] = turnId;
      jsonObject["extra_usage_auto_approve"] = autoApproveExtraUsage;
      JsonObject jsonObject2 = jsonObject;
      HttpMethod post = HttpMethod.Post;
      string text = "/agent/record-agent-launch";
      JsonObject jsonObject3 = jsonObject2;
      TimeSpan timeSpan = TimeSpan.FromSeconds(15L);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["X-Clicky-Usage-Context"] = usageContext;
      object obj = await this.SendAsync(post, text, supabaseAccessToken, jsonObject3, ct, timeSpan, dictionary).ConfigureAwait(false);
      int item = obj.Item1;
      string item2 = obj.Item2;
      if (item < 200 || item >= 300)
      {
        throw new AgentBackendException("/agent/record-agent-launch", item, item2);
      }
      return AgentTurnLease.Parse(AgentWorkerApi.ParseObject(item2));
    }

    // Token: 0x06000918 RID: 2328 RVA: 0x00103464 File Offset: 0x00103464
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<AgentTurnLease> LeaseStatusAsync(string supabaseAccessToken, string leaseId, CancellationToken ct)
    {
      object obj = await this.SendAsync(HttpMethod.Get, "/agent/turn-lease/" + leaseId + "/status", supabaseAccessToken, null, ct, TimeSpan.FromSeconds(15L), null).ConfigureAwait(false);
      int item = obj.Item1;
      string item2 = obj.Item2;
      if (item < 200 || item >= 300)
      {
        throw new AgentBackendException("/agent/turn-lease/status", item, item2);
      }
      return AgentTurnLease.Parse(AgentWorkerApi.ParseObject(item2));
    }

    // Token: 0x06000919 RID: 2329 RVA: 0x001034C0 File Offset: 0x001034C0
    [return: TupleElementNames(new string[] { "Continued", "Lease", "Reason" })]
    [return: Nullable(new byte[] { 1, 0, 2, 2 })]
    public async Task<ValueTuple<bool, AgentTurnLease, string>?> ContinueAfterComputerUseConsentAsync(string supabaseAccessToken, string leaseId, CancellationToken ct)
    {
      object obj = await this.SendAsync(HttpMethod.Post, "/agent/turn-lease/" + leaseId + "/continue-after-computer-use-consent", supabaseAccessToken, new JsonObject(null), ct, TimeSpan.FromSeconds(15L), null).ConfigureAwait(false);
      int item = obj.Item1;
      string item2 = obj.Item2;
      ValueTuple<bool, AgentTurnLease, string>? valueTuple;
      if (item >= 500 || item < 0)
      {
        valueTuple = null;
      }
      else if (item < 200 || item >= 300)
      {
        bool flag = false;
        AgentTurnLease agentTurnLease = null;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
        defaultInterpolatedStringHandler.AppendLiteral("status_");
        defaultInterpolatedStringHandler.AppendFormatted<int>(item);
        valueTuple = new ValueTuple<bool, AgentTurnLease, string>?(new ValueTuple<bool, AgentTurnLease, string>(flag, agentTurnLease, defaultInterpolatedStringHandler.ToStringAndClear()));
      }
      else
      {
        JsonObject jsonObject = AgentWorkerApi.ParseObject(item2);
        JsonValue jsonValue = ((jsonObject != null) ? jsonObject["continued"] : null) as JsonValue;
        bool flag2;
        if (jsonValue == null || !jsonValue.TryGetValue<bool>(out flag2))
        {
          valueTuple = null;
        }
        else if (!flag2)
        {
          bool flag3 = false;
          AgentTurnLease agentTurnLease2 = null;
          JsonNode jsonNode = jsonObject["reason"];
          valueTuple = new ValueTuple<bool, AgentTurnLease, string>?(new ValueTuple<bool, AgentTurnLease, string>(flag3, agentTurnLease2, ((jsonNode != null) ? jsonNode.ToString() : null) ?? "unknown"));
        }
        else
        {
          AgentTurnLease agentTurnLease3 = AgentTurnLease.Parse(jsonObject);
          valueTuple = ((agentTurnLease3 == null) ? null : new ValueTuple<bool, AgentTurnLease, string>?(new ValueTuple<bool, AgentTurnLease, string>(true, agentTurnLease3, null)));
        }
      }
      return valueTuple;
    }

    // Token: 0x0600091A RID: 2330 RVA: 0x0010351C File Offset: 0x0010351C
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<AgentTurnLease> ContinueLeaseAsync(string supabaseAccessToken, string leaseId, bool asManyAsNeeded, CancellationToken ct)
    {
      HttpMethod post = HttpMethod.Post;
      string text = "/agent/turn-lease/" + leaseId + "/continue";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["extra_usage_approval"] = (asManyAsNeeded ? "as_many_as_needed" : "one_time");
      object obj = await this.SendAsync(post, text, supabaseAccessToken, jsonObject, ct, TimeSpan.FromSeconds(15L), null).ConfigureAwait(false);
      int item = obj.Item1;
      string item2 = obj.Item2;
      if (item < 200 || item >= 300)
      {
        throw new AgentBackendException("/agent/turn-lease/continue", item, item2);
      }
      return AgentTurnLease.Parse(AgentWorkerApi.ParseObject(item2));
    }

    // Token: 0x0600091B RID: 2331 RVA: 0x00103580 File Offset: 0x00103580
    public async Task CompleteLeaseAsync(string supabaseAccessToken, string leaseId, string leaseStatus, [Nullable(2)] string threadId, CancellationToken ct)
    {
      HttpMethod post = HttpMethod.Post;
      string text = "/agent/turn-lease/" + leaseId + "/complete";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["status"] = leaseStatus;
      jsonObject["thread_id"] = threadId;
      object obj = await this.SendAsync(post, text, supabaseAccessToken, jsonObject, ct, TimeSpan.FromSeconds(15L), null).ConfigureAwait(false);
      int item = obj.Item1;
      string item2 = obj.Item2;
      if (item < 200 || item >= 300)
      {
        throw new AgentBackendException("/agent/turn-lease/complete", item, item2);
      }
    }

    // Token: 0x0600091C RID: 2332 RVA: 0x001035F0 File Offset: 0x001035F0
    public async Task<bool> RefundEarlyStopAsync(string supabaseAccessToken, string leaseId, CancellationToken ct)
    {
      object obj = await this.SendAsync(HttpMethod.Post, "/agent/turn-lease/" + leaseId + "/refund-early-stop", supabaseAccessToken, new JsonObject(null), ct, TimeSpan.FromSeconds(8L), null).ConfigureAwait(false);
      int item = obj.Item1;
      string item2 = obj.Item2;
      if (item < 200 || item >= 300)
      {
        throw new AgentBackendException("/agent/turn-lease/refund-early-stop", item, item2);
      }
      JsonObject jsonObject = AgentWorkerApi.ParseObject(item2);
      JsonValue jsonValue = ((jsonObject != null) ? jsonObject["refunded"] : null) as JsonValue;
      bool b;
      return jsonValue != null && jsonValue.TryGetValue<bool>(out b) && b;
    }

    // Token: 0x0600091D RID: 2333 RVA: 0x0010364C File Offset: 0x0010364C
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<string> ThreadTitleAsync(string supabaseAccessToken, string userPrompt, CancellationToken ct, TimeSpan? timeout = null, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> namesInUse = null)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["user_prompt"] = userPrompt;
      JsonObject jsonObject2 = jsonObject;
      if (namesInUse != null && namesInUse.Count > 0)
      {
        jsonObject2["names_in_use"] = new JsonArray((from name in namesInUse.Take<string>(30)
          select JsonValue.Create(name, null)).ToArray<JsonNode>());
      }
      object obj = await this.SendAsync(HttpMethod.Post, "/codex-thread-title", supabaseAccessToken, jsonObject2, ct, timeout ?? TimeSpan.FromSeconds(10L), null).ConfigureAwait(false);
      int item = obj.Item1;
      string item2 = obj.Item2;
      if (item < 200 || item >= 300)
      {
        throw new AgentBackendException("/codex-thread-title", item, item2);
      }
      JsonObject jsonObject3 = AgentWorkerApi.ParseObject(item2);
      string text;
      if (jsonObject3 == null)
      {
        text = null;
      }
      else
      {
        JsonNode jsonNode = jsonObject3["title"];
        if (jsonNode == null)
        {
          text = null;
        }
        else
        {
          string text2 = jsonNode.ToString();
          text = ((text2 != null) ? text2.Trim() : null);
        }
      }
      string text3 = text;
      return string.IsNullOrEmpty(text3) ? null : text3;
    }

    // Token: 0x0600091E RID: 2334 RVA: 0x001036BC File Offset: 0x001036BC
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<string> TextStartCueAsync(string supabaseAccessToken, string userPrompt, bool isFreshThread, CancellationToken ct)
    {
      HttpMethod post = HttpMethod.Post;
      string text = "/codex-thread-launch";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["user_prompt"] = userPrompt;
      jsonObject["is_fresh_thread"] = isFreshThread;
      jsonObject["launch_source"] = "text";
      object obj = await this.SendAsync(post, text, supabaseAccessToken, jsonObject, ct, TimeSpan.FromSeconds(10L), null).ConfigureAwait(false);
      int item = obj.Item1;
      string item2 = obj.Item2;
      if (item < 200 || item >= 300)
      {
        throw new AgentBackendException("/codex-thread-launch", item, item2);
      }
      JsonObject jsonObject2 = AgentWorkerApi.ParseObject(item2);
      string text2;
      if (jsonObject2 == null)
      {
        text2 = null;
      }
      else
      {
        JsonNode jsonNode = jsonObject2["text_start_cue"];
        if (jsonNode == null)
        {
          text2 = null;
        }
        else
        {
          string text3 = jsonNode.ToString();
          text2 = ((text3 != null) ? text3.Trim() : null);
        }
      }
      string text4 = text2;
      return string.IsNullOrEmpty(text4) ? null : text4;
    }

    // Token: 0x0600091F RID: 2335 RVA: 0x00103720 File Offset: 0x00103720
    public async Task LogAgentMessageAsync(string supabaseAccessToken, [Nullable(2)] string threadId, string role, string content, bool isFollowUp, bool isDemo, long? proactiveSuggestionId, CancellationToken ct)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["role"] = role;
      jsonObject["content"] = content;
      jsonObject["is_follow_up"] = isFollowUp;
      jsonObject["is_demo"] = isDemo;
      JsonObject jsonObject2 = jsonObject;
      if (!string.IsNullOrWhiteSpace(threadId))
      {
        jsonObject2["thread_id"] = threadId.Trim();
      }
      if (proactiveSuggestionId != null)
      {
        long valueOrDefault = proactiveSuggestionId.GetValueOrDefault();
        jsonObject2["is_proactive"] = true;
        jsonObject2["proactive_suggestion_id"] = valueOrDefault;
      }
      object obj = await this.SendAsync(HttpMethod.Post, "/agent-messages", supabaseAccessToken, jsonObject2, ct, TimeSpan.FromSeconds(15L), null).ConfigureAwait(false);
      int item = obj.Item1;
      string item2 = obj.Item2;
      if (item != 200)
      {
        throw new AgentBackendException("/agent-messages", item, item2);
      }
    }

    // Token: 0x06000920 RID: 2336 RVA: 0x001037A8 File Offset: 0x001037A8
    [return: TupleElementNames(new string[] { "Status", "Markdown" })]
    [return: Nullable(new byte[] { 1, 0, 2 })]
    public async Task<ValueTuple<UserMemoryFetchStatus, string>> UserMemoryAsync(string supabaseAccessToken, CancellationToken ct)
    {
      object obj = await this.SendAsync(HttpMethod.Get, "/me/memory", supabaseAccessToken, null, ct, TimeSpan.FromSeconds(4L), null).ConfigureAwait(false);
      int item = obj.Item1;
      string item2 = obj.Item2;
      ValueTuple<UserMemoryFetchStatus, string> valueTuple;
      if (item < 200 || item >= 300)
      {
        valueTuple = new ValueTuple<UserMemoryFetchStatus, string>(UserMemoryFetchStatus.Failed, null);
      }
      else
      {
        JsonObject jsonObject = AgentWorkerApi.ParseObject(item2);
        if (jsonObject == null)
        {
          valueTuple = new ValueTuple<UserMemoryFetchStatus, string>(UserMemoryFetchStatus.Failed, null);
        }
        else
        {
          JsonValue jsonValue = jsonObject["has_profile"] as JsonValue;
          bool b;
          bool flag = jsonValue != null && jsonValue.TryGetValue<bool>(out b) && b;
          JsonNode jsonNode = jsonObject["markdown"];
          string text = ((jsonNode != null) ? jsonNode.ToString() : null);
          valueTuple = ((flag && !string.IsNullOrWhiteSpace(text)) ? new ValueTuple<UserMemoryFetchStatus, string>(UserMemoryFetchStatus.Profile, text) : new ValueTuple<UserMemoryFetchStatus, string>(UserMemoryFetchStatus.NoProfile, null));
        }
      }
      return valueTuple;
    }

    // Token: 0x06000921 RID: 2337 RVA: 0x001037FC File Offset: 0x001037FC
    [NullableContext(2)]
    private static string PromptSafe(string value)
    {
      if (value == null)
      {
        return null;
      }
      string text = new string(value.Where<char>((char c) => !char.IsControl(c) && (c != '<' && c != '>' && c != '[') && c != ']').ToArray<char>()).Trim();
      if (text.Length == 0)
      {
        return null;
      }
      if (text.Length <= 80)
      {
        return text;
      }
      return text.Substring(0, 80);
    }

    // Token: 0x06000922 RID: 2338 RVA: 0x00103864 File Offset: 0x00103864
    public static bool IsUnauthorized(Exception ex)
    {
      AgentBackendException ex2 = ex as AgentBackendException;
      return ex2 != null && ex2.StatusCode == 401;
    }

    // Token: 0x04000335 RID: 821
    [CompilerGenerated]
    private HttpClient <http>P = http;

    // Token: 0x020001CF RID: 463
    [NullableContext(0)]
    public enum ComposioFetchKind
    {
      // Token: 0x04000878 RID: 2168
      Connected,
      // Token: 0x04000879 RID: 2169
      NoConnectedApps,
      // Token: 0x0400087A RID: 2170
      Unreachable
    }
  }
}
