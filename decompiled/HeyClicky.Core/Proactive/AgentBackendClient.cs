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
using HeyClicky.Core.Net;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000B9 RID: 185
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentBackendClient
  {
    // Token: 0x170002A9 RID: 681
    // (get) Token: 0x06000C34 RID: 3124 RVA: 0x0037C998 File Offset: 0x0037C998
    // (set) Token: 0x06000C35 RID: 3125 RVA: 0x0037C9A0 File Offset: 0x0037C9A0
    [Nullable(2)]
    public Func<byte[]> TestResponse
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170002AA RID: 682
    // (get) Token: 0x06000C36 RID: 3126 RVA: 0x0037C9AC File Offset: 0x0037C9AC
    // (set) Token: 0x06000C37 RID: 3127 RVA: 0x0037C9B4 File Offset: 0x0037C9B4
    [Nullable(new byte[] { 2, 1 })]
    public Action<string> TestMarkRead
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x06000C38 RID: 3128 RVA: 0x0037C9C0 File Offset: 0x0037C9C0
    public AgentBackendClient(ClickyHttp http, [Nullable(new byte[] { 1, 1, 2 })] Func<bool, CancellationToken, Task<string>> agentToken)
    {
      this.http = http;
      this.agentToken = agentToken;
    }

    // Token: 0x06000C39 RID: 3129 RVA: 0x0037C9D8 File Offset: 0x0037C9D8
    public async Task<IReadOnlyList<AgentNotification>> FetchUnreadAsync(CancellationToken ct = default(CancellationToken))
    {
      Func<byte[]> testResponse = this.TestResponse;
      byte[] array = ((testResponse != null) ? testResponse() : null);
      IReadOnlyList<AgentNotification> readOnlyList;
      if (array != null)
      {
        readOnlyList = AgentNotification.ParseList(array);
      }
      else
      {
        readOnlyList = AgentNotification.ParseList(await this.SendAsync(HttpMethod.Get, "/agent/notifications?limit=20", ct).ConfigureAwait(false));
      }
      return readOnlyList;
    }

    // Token: 0x06000C3A RID: 3130 RVA: 0x0037CA24 File Offset: 0x0037CA24
    public Task MarkReadAsync(string id, CancellationToken ct = default(CancellationToken))
    {
      if (this.TestResponse != null)
      {
        Action<string> testMarkRead = this.TestMarkRead;
        if (testMarkRead != null)
        {
          testMarkRead(id);
        }
        return Task.CompletedTask;
      }
      return this.SendAsync(HttpMethod.Post, "/agent/notifications/" + Uri.EscapeDataString(id) + "/read", ct);
    }

    // Token: 0x06000C3B RID: 3131 RVA: 0x0037CA74 File Offset: 0x0037CA74
    public Task MarkAllReadAsync(CancellationToken ct = default(CancellationToken))
    {
      if (this.TestResponse != null)
      {
        Action<string> testMarkRead = this.TestMarkRead;
        if (testMarkRead != null)
        {
          testMarkRead("*");
        }
        return Task.CompletedTask;
      }
      return this.SendAsync(HttpMethod.Post, "/agent/notifications/read-all", ct);
    }

    // Token: 0x06000C3C RID: 3132 RVA: 0x0037CAAC File Offset: 0x0037CAAC
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<JsonObject> FetchCronDetailAsync(string cronId, CancellationToken ct = default(CancellationToken))
    {
      JsonObject jsonObject;
      if (this.TestResponse != null)
      {
        jsonObject = null;
      }
      else
      {
        try
        {
          jsonObject = JsonNode.Parse(await this.SendAsync(HttpMethod.Get, "/agent/cron/" + Uri.EscapeDataString(cronId), ct).ConfigureAwait(false), null, default(JsonDocumentOptions)) as JsonObject;
        }
        catch (Exception ex)
        {
          Log.Info("Notifications", "cron detail " + cronId + ": " + ex.Message);
          jsonObject = null;
        }
      }
      return jsonObject;
    }

    // Token: 0x06000C3D RID: 3133 RVA: 0x0037CB00 File Offset: 0x0037CB00
    public async Task<IReadOnlyList<AgentIntegrationInfo>> FetchIntegrationsAsync(CancellationToken ct = default(CancellationToken))
    {
      return AgentIntegrationInfo.ParseList(await this.SendAsync(HttpMethod.Get, "/agent/integrations?limit=1000", ct).ConfigureAwait(false));
    }

    // Token: 0x06000C3E RID: 3134 RVA: 0x0037CB4C File Offset: 0x0037CB4C
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<Uri> ConnectIntegrationAsync(string toolkitSlug, CancellationToken ct = default(CancellationToken))
    {
      JsonNode jsonNode = JsonNode.Parse(await this.SendAsync(HttpMethod.Post, "/agent/integrations/" + Uri.EscapeDataString(toolkitSlug) + "/connect", ct).ConfigureAwait(false), null, default(JsonDocumentOptions));
      string text;
      if (jsonNode == null)
      {
        text = null;
      }
      else
      {
        JsonNode jsonNode2 = jsonNode["redirect_url"];
        text = ((jsonNode2 != null) ? jsonNode2.GetValue<string>() : null);
      }
      string text2 = text;
      Uri uri;
      return (text2 != null && Uri.TryCreate(text2, UriKind.Absolute, out uri)) ? uri : null;
    }

    // Token: 0x06000C3F RID: 3135 RVA: 0x0037CBA0 File Offset: 0x0037CBA0
    public async Task<byte[]> SendAsync(HttpMethod method, string path, CancellationToken ct)
    {
      int attempt = 0;
      for (;;)
      {
        object obj = await this.agentToken(attempt > 0, ct).ConfigureAwait(false);
        if (obj == null)
        {
          break;
        }
        string text = obj;
        using (HttpRequestMessage request = new HttpRequestMessage(method, this.http.WorkerUrl(path)))
        {
          request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", text);
          request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
          if (method == HttpMethod.Post)
          {
            request.Content = new ByteArrayContent(Encoding.UTF8.GetBytes("{}"));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
          }
          using (CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
          {
            cts.CancelAfter(TimeSpan.FromSeconds(20L));
            using (HttpResponseMessage response = await this.http.Client.SendAsync(request, cts.Token).ConfigureAwait(false))
            {
              byte[] array = await response.Content.ReadAsByteArrayAsync(cts.Token).ConfigureAwait(false);
              int statusCode = (int)response.StatusCode;
              if (statusCode >= 200 && statusCode < 300)
              {
                return array;
              }
              if (statusCode != 401 || attempt != 0)
              {
                string text4;
                try
                {
                  JsonNode jsonNode = JsonNode.Parse(array, null, default(JsonDocumentOptions));
                  string text2;
                  if (jsonNode == null)
                  {
                    text2 = null;
                  }
                  else
                  {
                    JsonNode jsonNode2 = jsonNode["error"];
                    text2 = ((jsonNode2 != null) ? jsonNode2.GetValue<string>() : null);
                  }
                  string text3;
                  if ((text3 = text2) == null)
                  {
                    DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
                    defaultInterpolatedStringHandler.AppendLiteral("HTTP ");
                    defaultInterpolatedStringHandler.AppendFormatted<int>(statusCode);
                    text3 = defaultInterpolatedStringHandler.ToStringAndClear();
                  }
                  text4 = text3;
                }
                catch (JsonException)
                {
                  DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
                  defaultInterpolatedStringHandler.AppendLiteral("HTTP ");
                  defaultInterpolatedStringHandler.AppendFormatted<int>(statusCode);
                  text4 = defaultInterpolatedStringHandler.ToStringAndClear();
                }
                throw new HttpRequestException(text4, null, new HttpStatusCode?(response.StatusCode));
              }
            }
          }
        }
        attempt++;
      }
      throw new InvalidOperationException("Signed out.");
    }

    // Token: 0x06000C40 RID: 3136 RVA: 0x0037CBFC File Offset: 0x0037CBFC
    public static string BuildContext(AgentNotification n, [Nullable(2)] JsonObject detail)
    {
      List<string> list = new List<string>();
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(104, 7);
      defaultInterpolatedStringHandler.AppendLiteral("Notification:\n- title: ");
      defaultInterpolatedStringHandler.AppendFormatted(n.Title);
      defaultInterpolatedStringHandler.AppendLiteral("\n- severity: ");
      defaultInterpolatedStringHandler.AppendFormatted(n.Severity.ToString().ToLowerInvariant());
      defaultInterpolatedStringHandler.AppendLiteral("\n- created_at: ");
      defaultInterpolatedStringHandler.AppendFormatted(n.CreatedAt);
      defaultInterpolatedStringHandler.AppendLiteral("\n- summary: ");
      defaultInterpolatedStringHandler.AppendFormatted(n.Summary);
      defaultInterpolatedStringHandler.AppendLiteral("\n- source_type: ");
      defaultInterpolatedStringHandler.AppendFormatted(n.SourceType);
      defaultInterpolatedStringHandler.AppendLiteral("\n- source_id: ");
      defaultInterpolatedStringHandler.AppendFormatted(n.SourceId ?? "none");
      defaultInterpolatedStringHandler.AppendLiteral("\n- run_id: ");
      defaultInterpolatedStringHandler.AppendFormatted(n.RunId ?? "none");
      list.Add(defaultInterpolatedStringHandler.ToStringAndClear());
      List<string> list2 = list;
      JsonNode jsonNode = ((detail != null) ? detail["cron"] : null);
      AgentBackendClient.<>c__DisplayClass19_0 CS$<>8__locals1;
      CS$<>8__locals1.cron = jsonNode as JsonObject;
      if (CS$<>8__locals1.cron != null)
      {
        List<string> list3 = list2;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(135, 10);
        defaultInterpolatedStringHandler.AppendLiteral("Remote Task:\n- id: ");
        defaultInterpolatedStringHandler.AppendFormatted(AgentBackendClient.<BuildContext>g__C|19_1("id", ref CS$<>8__locals1));
        defaultInterpolatedStringHandler.AppendLiteral("\n- name: ");
        defaultInterpolatedStringHandler.AppendFormatted(AgentBackendClient.<BuildContext>g__C|19_1("name", ref CS$<>8__locals1));
        defaultInterpolatedStringHandler.AppendLiteral("\n- description: ");
        defaultInterpolatedStringHandler.AppendFormatted(AgentBackendClient.<BuildContext>g__C|19_1("description", ref CS$<>8__locals1));
        defaultInterpolatedStringHandler.AppendLiteral("\n- enabled: ");
        defaultInterpolatedStringHandler.AppendFormatted(AgentBackendClient.<BuildContext>g__C|19_1("enabled", ref CS$<>8__locals1));
        defaultInterpolatedStringHandler.AppendLiteral("\n- schedule: ");
        defaultInterpolatedStringHandler.AppendFormatted(AgentBackendClient.<BuildContext>g__C|19_1("schedule_kind", ref CS$<>8__locals1));
        defaultInterpolatedStringHandler.AppendLiteral(" ");
        defaultInterpolatedStringHandler.AppendFormatted(AgentBackendClient.<BuildContext>g__C|19_1("schedule_expr", ref CS$<>8__locals1));
        defaultInterpolatedStringHandler.AppendLiteral(" ");
        defaultInterpolatedStringHandler.AppendFormatted(AgentBackendClient.<BuildContext>g__C|19_1("timezone", ref CS$<>8__locals1));
        defaultInterpolatedStringHandler.AppendLiteral("\n- next_run_at: ");
        JsonNode jsonNode2 = CS$<>8__locals1.cron["next_run_at"];
        defaultInterpolatedStringHandler.AppendFormatted(((jsonNode2 != null) ? jsonNode2.ToString() : null) ?? "none");
        defaultInterpolatedStringHandler.AppendLiteral("\n- last_run_status: ");
        JsonNode jsonNode3 = CS$<>8__locals1.cron["last_run_status"];
        defaultInterpolatedStringHandler.AppendFormatted(((jsonNode3 != null) ? jsonNode3.ToString() : null) ?? "none");
        defaultInterpolatedStringHandler.AppendLiteral("\n\nCurrent source:\n```ts\n");
        defaultInterpolatedStringHandler.AppendFormatted(AgentBackendClient.<BuildContext>g__Trim|19_0(AgentBackendClient.<BuildContext>g__C|19_1("source", ref CS$<>8__locals1), 12000));
        defaultInterpolatedStringHandler.AppendLiteral("\n```");
        list3.Add(defaultInterpolatedStringHandler.ToStringAndClear());
      }
      JsonArray jsonArray = ((detail != null) ? detail["runs"] : null) as JsonArray;
      if (jsonArray != null && jsonArray.Count > 0)
      {
        string text = string.Join("\n\n", jsonArray.OfType<JsonObject>().Take<JsonObject>(5).Select<JsonObject, string>(delegate(JsonObject r)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(127, 10);
          defaultInterpolatedStringHandler2.AppendLiteral("Run ");
          defaultInterpolatedStringHandler2.AppendFormatted<JsonNode>(r["id"]);
          defaultInterpolatedStringHandler2.AppendLiteral(":\n- status: ");
          defaultInterpolatedStringHandler2.AppendFormatted<JsonNode>(r["status"]);
          defaultInterpolatedStringHandler2.AppendLiteral("\n- attempt: ");
          defaultInterpolatedStringHandler2.AppendFormatted<JsonNode>(r["attempt"]);
          defaultInterpolatedStringHandler2.AppendLiteral("\n- created_at: ");
          JsonNode jsonNode4 = r["created_at"];
          defaultInterpolatedStringHandler2.AppendFormatted(((jsonNode4 != null) ? jsonNode4.ToString() : null) ?? "none");
          defaultInterpolatedStringHandler2.AppendLiteral("\n- started_at: ");
          JsonNode jsonNode5 = r["started_at"];
          defaultInterpolatedStringHandler2.AppendFormatted(((jsonNode5 != null) ? jsonNode5.ToString() : null) ?? "none");
          defaultInterpolatedStringHandler2.AppendLiteral("\n- finished_at: ");
          JsonNode jsonNode6 = r["finished_at"];
          defaultInterpolatedStringHandler2.AppendFormatted(((jsonNode6 != null) ? jsonNode6.ToString() : null) ?? "none");
          defaultInterpolatedStringHandler2.AppendLiteral("\n- duration_ms: ");
          JsonNode jsonNode7 = r["duration_ms"];
          defaultInterpolatedStringHandler2.AppendFormatted(((jsonNode7 != null) ? jsonNode7.ToString() : null) ?? "none");
          defaultInterpolatedStringHandler2.AppendLiteral("\n- result_summary: ");
          defaultInterpolatedStringHandler2.AppendFormatted<JsonNode>(r["result_summary"]);
          defaultInterpolatedStringHandler2.AppendLiteral("\n- error: ");
          JsonNode jsonNode8 = r["error"];
          defaultInterpolatedStringHandler2.AppendFormatted(AgentBackendClient.<BuildContext>g__Trim|19_0((jsonNode8 != null) ? jsonNode8.ToString() : null, 2000));
          defaultInterpolatedStringHandler2.AppendLiteral("\n- log:\n");
          JsonNode jsonNode9 = r["stdout_log"];
          defaultInterpolatedStringHandler2.AppendFormatted(AgentBackendClient.<BuildContext>g__Trim|19_0((jsonNode9 != null) ? jsonNode9.ToString() : null, 5000));
          return defaultInterpolatedStringHandler2.ToStringAndClear();
        }));
        list2.Add("Recent runs:\n" + text);
      }
      return string.Join("\n\n", list2);
    }

    // Token: 0x06000C41 RID: 3137 RVA: 0x0037CF3C File Offset: 0x0037CF3C
    public static string ComposePrompt([Nullable(2)] string context, string instruction)
    {
      string text = instruction.Trim();
      if (string.IsNullOrWhiteSpace(context))
      {
        return text;
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(152, 2);
      defaultInterpolatedStringHandler.AppendLiteral("HeyClicky Agent remote task modification request.\n\n");
      defaultInterpolatedStringHandler.AppendFormatted(context);
      defaultInterpolatedStringHandler.AppendLiteral("\n\nUser requested change:\n");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("\n\nUse the clicky-crons tools to inspect, edit, run, or fix this Remote Task.");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06000C43 RID: 3139 RVA: 0x0037CFB4 File Offset: 0x0037CFB4
    [CompilerGenerated]
    internal static string <BuildContext>g__Trim|19_0([Nullable(2)] string value, int limit)
    {
      if (value == null)
      {
        return "";
      }
      if (value.Length > limit)
      {
        return value.Substring(0, limit) + "\n...[truncated]";
      }
      return value;
    }

    // Token: 0x06000C44 RID: 3140 RVA: 0x0037CFDC File Offset: 0x0037CFDC
    [CompilerGenerated]
    internal static string <BuildContext>g__C|19_1(string k, ref AgentBackendClient.<>c__DisplayClass19_0 A_1)
    {
      JsonNode jsonNode = A_1.cron[k];
      return ((jsonNode != null) ? jsonNode.ToString() : null) ?? "";
    }

    // Token: 0x0400053A RID: 1338
    private readonly ClickyHttp http;

    // Token: 0x0400053B RID: 1339
    [Nullable(new byte[] { 1, 1, 2 })]
    private readonly Func<bool, CancellationToken, Task<string>> agentToken;

    // Token: 0x0400053E RID: 1342
    public static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(60L);

    // Token: 0x0400053F RID: 1343
    public const string FixInstruction = "Fix this remote task. Use the run error, logs, result, current source, and HeyClicky scheduled cron APIs to update the task so the next run succeeds. Show the proposed source/scope changes before deploying if a destructive or permission-expanding change is needed.";

    // Token: 0x04000540 RID: 1344
    public const string FixLaunchContext = "The user tapped Fix the Task with HeyClicky Agent on a failed Remote Task notification, so HeyClicky is launching a Codex agent with the task source, run logs, and failure result.";
  }
}
