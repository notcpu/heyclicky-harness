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

namespace HeyClicky.Core.Connectors
{
  // Token: 0x020001DC RID: 476
  [NullableContext(1)]
  [Nullable(0)]
  public static class McpProbe
  {
    // Token: 0x06001B56 RID: 6998 RVA: 0x003DAB00 File Offset: 0x003DAB00
    [return: Nullable(new byte[] { 1, 2 })]
    public static async Task<McpToolCatalog> FetchToolCatalogAsync(HttpClient http, Uri serverUrl, [Nullable(2)] string bearerToken, IReadOnlyDictionary<string, string> headers, CancellationToken ct = default(CancellationToken))
    {
      ValueTuple<JsonObject, string>? valueTuple = await McpProbe.SendRequestAsync(http, serverUrl, bearerToken, headers, null, 1, "initialize", McpProbe.InitializeParams(), ct).ConfigureAwait(false);
      McpToolCatalog mcpToolCatalog;
      if (valueTuple != null)
      {
        ValueTuple<JsonObject, string> valueOrDefault = valueTuple.GetValueOrDefault();
        string sessionId = valueOrDefault.Item2;
        JsonObject jsonObject = valueOrDefault.Item1["serverInfo"] as JsonObject;
        JsonNode jsonNode = ((jsonObject != null) ? jsonObject["title"] : null) ?? ((jsonObject != null) ? jsonObject["name"] : null);
        string text;
        if (jsonNode == null)
        {
          text = null;
        }
        else
        {
          string text2 = jsonNode.ToString();
          text = ((text2 != null) ? text2.Trim() : null);
        }
        string title = text;
        string text3 = sessionId;
        JsonObject jsonObject2 = new JsonObject(null);
        jsonObject2["jsonrpc"] = "2.0";
        jsonObject2["method"] = "notifications/initialized";
        await McpProbe.PostAsync(http, serverUrl, bearerToken, headers, text3, jsonObject2, ct).ConfigureAwait(false);
        List<string> names = new List<string>();
        string text4 = null;
        int id = 2;
        for (int page = 0; page < 5; page++)
        {
          JsonObject jsonObject3 = new JsonObject(null);
          if (text4 != null)
          {
            jsonObject3["cursor"] = text4;
          }
          ValueTuple<JsonObject, string>? valueTuple2 = await McpProbe.SendRequestAsync(http, serverUrl, bearerToken, headers, sessionId, id++, "tools/list", jsonObject3, ct).ConfigureAwait(false);
          if (valueTuple2 == null)
          {
            break;
          }
          ValueTuple<JsonObject, string> valueOrDefault2 = valueTuple2.GetValueOrDefault();
          List<string> list = names;
          JsonArray jsonArray;
          if ((jsonArray = valueOrDefault2.Item1["tools"] as JsonArray) == null)
          {
            jsonArray = new JsonArray(null);
          }
          list.AddRange(jsonArray.OfType<JsonObject>().Select<JsonObject, string>(delegate(JsonObject t)
          {
            JsonNode jsonNode3 = t["name"];
            if (jsonNode3 == null)
            {
              return null;
            }
            return jsonNode3.ToString();
          }).OfType<string>());
          JsonNode jsonNode2 = valueOrDefault2.Item1["nextCursor"];
          text4 = ((jsonNode2 != null) ? jsonNode2.ToString() : null);
          if (string.IsNullOrEmpty(text4))
          {
            break;
          }
        }
        if (names.Count == 0)
        {
          mcpToolCatalog = (string.IsNullOrEmpty(title) ? null : new McpToolCatalog(title, Array.Empty<string>()));
        }
        else
        {
          mcpToolCatalog = new McpToolCatalog(string.IsNullOrEmpty(title) ? null : title, names);
        }
      }
      else
      {
        mcpToolCatalog = null;
      }
      return mcpToolCatalog;
    }

    // Token: 0x06001B57 RID: 6999 RVA: 0x003DAB64 File Offset: 0x003DAB64
    public static async Task<McpVerification> VerifyConnectionAsync(HttpClient http, Uri serverUrl, [Nullable(2)] string bearerToken, IReadOnlyDictionary<string, string> headers, CancellationToken ct = default(CancellationToken))
    {
      string text = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["jsonrpc"] = "2.0";
      jsonObject["id"] = 1;
      jsonObject["method"] = "initialize";
      jsonObject["params"] = McpProbe.InitializeParams();
      McpProbe.Posted posted = await McpProbe.PostAsync(http, serverUrl, bearerToken, headers, text, jsonObject, ct).ConfigureAwait(false);
      McpVerification mcpVerification = McpVerification.Failure((posted != null) ? new int?(posted.Status) : null);
      McpVerification mcpVerification2;
      if (mcpVerification != null)
      {
        mcpVerification2 = mcpVerification;
      }
      else
      {
        string sessionId = posted.SessionId;
        string text2 = sessionId;
        JsonObject jsonObject2 = new JsonObject(null);
        jsonObject2["jsonrpc"] = "2.0";
        jsonObject2["method"] = "notifications/initialized";
        await McpProbe.PostAsync(http, serverUrl, bearerToken, headers, text2, jsonObject2, ct).ConfigureAwait(false);
        string text3 = sessionId;
        JsonObject jsonObject3 = new JsonObject(null);
        jsonObject3["jsonrpc"] = "2.0";
        jsonObject3["id"] = 2;
        jsonObject3["method"] = "tools/list";
        jsonObject3["params"] = new JsonObject(null);
        McpProbe.Posted posted2 = await McpProbe.PostAsync(http, serverUrl, bearerToken, headers, text3, jsonObject3, ct).ConfigureAwait(false);
        McpVerification mcpVerification3 = McpVerification.Failure((posted2 != null) ? new int?(posted2.Status) : null);
        if (mcpVerification3 != null)
        {
          mcpVerification2 = mcpVerification3;
        }
        else
        {
          JsonObject jsonObject4 = McpProbe.ParseJsonRpcResponse(posted2.Body, posted2.IsEventStream, 2);
          object obj;
          if (jsonObject4 == null)
          {
            obj = null;
          }
          else
          {
            JsonNode jsonNode = jsonObject4["result"];
            obj = ((jsonNode != null) ? jsonNode["tools"] : null);
          }
          JsonArray jsonArray = obj as JsonArray;
          mcpVerification2 = new McpVerification.Reachable((jsonArray != null) ? jsonArray.Count : 0);
        }
      }
      return mcpVerification2;
    }

    // Token: 0x06001B58 RID: 7000 RVA: 0x003DABC8 File Offset: 0x003DABC8
    private static JsonObject InitializeParams()
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["protocolVersion"] = "2025-06-18";
      jsonObject["capabilities"] = new JsonObject(null);
      string text = "clientInfo";
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["name"] = "HeyClicky";
      jsonObject2["version"] = "1";
      jsonObject[text] = jsonObject2;
      return jsonObject;
    }

    // Token: 0x06001B59 RID: 7001 RVA: 0x003DAC54 File Offset: 0x003DAC54
    [return: TupleElementNames(new string[] { "Result", "SessionId" })]
    [return: Nullable(new byte[] { 1, 0, 1, 2 })]
    private static async Task<ValueTuple<JsonObject, string>?> SendRequestAsync(HttpClient http, Uri serverUrl, [Nullable(2)] string bearerToken, IReadOnlyDictionary<string, string> headers, [Nullable(2)] string sessionId, int id, string method, JsonObject p, CancellationToken ct)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["jsonrpc"] = "2.0";
      jsonObject["id"] = id;
      jsonObject["method"] = method;
      jsonObject["params"] = p;
      McpProbe.Posted posted = await McpProbe.PostAsync(http, serverUrl, bearerToken, headers, sessionId, jsonObject, ct).ConfigureAwait(false);
      bool flag = posted == null;
      if (!flag)
      {
        int status = posted.Status;
        flag = status < 200 || status >= 300;
      }
      ValueTuple<JsonObject, string>? valueTuple;
      if (flag)
      {
        valueTuple = null;
      }
      else
      {
        JsonObject jsonObject2 = McpProbe.ParseJsonRpcResponse(posted.Body, posted.IsEventStream, id);
        JsonObject jsonObject3 = ((jsonObject2 != null) ? jsonObject2["result"] : null) as JsonObject;
        if (jsonObject3 == null)
        {
          valueTuple = null;
        }
        else
        {
          valueTuple = new ValueTuple<JsonObject, string>?(new ValueTuple<JsonObject, string>(jsonObject3, posted.SessionId ?? sessionId));
        }
      }
      return valueTuple;
    }

    // Token: 0x06001B5A RID: 7002 RVA: 0x003DACDC File Offset: 0x003DACDC
    [return: Nullable(new byte[] { 1, 2 })]
    private static async Task<McpProbe.Posted> PostAsync(HttpClient http, Uri serverUrl, [Nullable(2)] string bearerToken, IReadOnlyDictionary<string, string> headers, [Nullable(2)] string sessionId, JsonObject payload, CancellationToken ct)
    {
      McpProbe.Posted posted;
      try
      {
        using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, serverUrl)
        {
          Content = new StringContent(payload.ToJsonString(null), Encoding.UTF8, "application/json")
        })
        {
          request.Headers.Accept.ParseAdd("application/json");
          request.Headers.Accept.ParseAdd("text/event-stream");
          request.Headers.TryAddWithoutValidation("MCP-Protocol-Version", "2025-06-18");
          foreach (KeyValuePair<string, string> keyValuePair in headers)
          {
            string text;
            string text2;
            keyValuePair.Deconstruct(out text, out text2);
            string text3 = text;
            string text4 = text2;
            if (text3.Length > 0)
            {
              request.Headers.TryAddWithoutValidation(text3, text4);
            }
          }
          if (!string.IsNullOrEmpty(bearerToken))
          {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
          }
          if (!string.IsNullOrEmpty(sessionId))
          {
            request.Headers.TryAddWithoutValidation("Mcp-Session-Id", sessionId);
          }
          using (CancellationTokenSource timeout = CancellationTokenSource.CreateLinkedTokenSource(ct))
          {
            timeout.CancelAfter(TimeSpan.FromSeconds(20L));
            HttpResponseMessage httpResponseMessage = await http.SendAsync(request, timeout.Token).ConfigureAwait(false);
            using (HttpResponseMessage response = httpResponseMessage)
            {
              string text5 = await response.Content.ReadAsStringAsync(timeout.Token).ConfigureAwait(false);
              MediaTypeHeaderValue contentType = response.Content.Headers.ContentType;
              string text6 = ((contentType != null) ? contentType.MediaType : null) ?? "";
              IEnumerable<string> enumerable;
              string text7 = (response.Headers.TryGetValues("Mcp-Session-Id", out enumerable) ? enumerable.FirstOrDefault<string>() : null);
              posted = new McpProbe.Posted((int)response.StatusCode, text5, text6.Contains("text/event-stream", StringComparison.OrdinalIgnoreCase), text7);
            }
          }
        }
      }
      catch (Exception ex) when ((ex is HttpRequestException || ex is TaskCanceledException) && !ct.IsCancellationRequested)
      {
        posted = null;
      }
      return posted;
    }

    // Token: 0x06001B5B RID: 7003 RVA: 0x003DAD54 File Offset: 0x003DAD54
    [return: Nullable(2)]
    internal static JsonObject ParseJsonRpcResponse(string body, bool isEventStream, int requestId)
    {
      JsonObject jsonObject;
      try
      {
        if (!isEventStream)
        {
          jsonObject = JsonNode.Parse(body, null, default(JsonDocumentOptions)) as JsonObject;
        }
        else
        {
          JsonObject jsonObject2 = null;
          string[] array = body.Split('\n', StringSplitOptions.RemoveEmptyEntries);
          for (int i = 0; i < array.Length; i++)
          {
            string text = array[i].TrimEnd('\r');
            if (text.StartsWith("data:", StringComparison.Ordinal))
            {
              string text2 = text;
              JsonObject jsonObject3 = JsonNode.Parse(text2.Substring(5, text2.Length - 5).Trim(), null, default(JsonDocumentOptions)) as JsonObject;
              if (jsonObject3 != null)
              {
                JsonValue jsonValue = jsonObject3["id"] as JsonValue;
                int num;
                if (jsonValue != null && jsonValue.TryGetValue<int>(out num) && num == requestId)
                {
                  return jsonObject3;
                }
                if (jsonObject3["result"] != null)
                {
                  jsonObject2 = jsonObject3;
                }
              }
            }
          }
          jsonObject = jsonObject2;
        }
      }
      catch (JsonException)
      {
        jsonObject = null;
      }
      return jsonObject;
    }

    // Token: 0x06001B5C RID: 7004 RVA: 0x003DAE60 File Offset: 0x003DAE60
    [return: Nullable(2)]
    public static string DeriveCapabilityDescription(string displayName, IReadOnlyList<string> toolNames)
    {
      McpProbe.<>c__DisplayClass16_0 CS$<>8__locals1 = new McpProbe.<>c__DisplayClass16_0();
      Func<string, List<string>> func;
      if ((func = McpProbe.<>O.<0>__Tokenize) == null)
      {
        func = (McpProbe.<>O.<0>__Tokenize = new Func<string, List<string>>(McpProbe.Tokenize));
      }
      List<List<string>> list = (from t in toolNames.Select<string, List<string>>(func)
        where t.Count > 0
        select t).ToList<List<string>>();
      if (list.Count == 0)
      {
        return null;
      }
      List<List<string>> list2 = list;
      CS$<>8__locals1.lead = list[0][0];
      if (!McpProbe.ReadVerbs.Contains(CS$<>8__locals1.lead) && !McpProbe.WriteVerbs.Contains(CS$<>8__locals1.lead) && list.Count<List<string>>((List<string> t) => t[0] == CS$<>8__locals1.lead) * 10 >= list.Count * 6)
      {
        list2 = list.Select<List<string>, List<string>>(delegate(List<string> t)
        {
          if (!(t[0] == CS$<>8__locals1.lead) || t.Count <= 1)
          {
            return t;
          }
          return t.Skip<string>(1).ToList<string>();
        }).ToList<List<string>>();
      }
      Dictionary<ValueTuple<string, string>, McpProbe.Usage> dictionary = new Dictionary<ValueTuple<string, string>, McpProbe.Usage>();
      List<ValueTuple<string, string>> list3 = new List<ValueTuple<string, string>>();
      foreach (List<string> list4 in list2)
      {
        string text = null;
        List<string> list5 = new List<string>();
        foreach (string text2 in list4)
        {
          if (text == null && (McpProbe.ReadVerbs.Contains(text2) || McpProbe.WriteVerbs.Contains(text2)))
          {
            text = text2;
          }
          else if (!McpProbe.NoiseTokens.Contains(text2))
          {
            string text3 = McpProbe.Singularized(text2);
            if (list5.Count > 0)
            {
              List<string> list6 = list5;
              if (list6[list6.Count - 1] == text3)
              {
                continue;
              }
            }
            list5.Add(text3);
          }
        }
        if (list5.Count != 0)
        {
          ValueTuple<string, string> valueTuple = new ValueTuple<string, string>(list5[0], string.Join<string>(' ', list5));
          McpProbe.Usage usage;
          if (!dictionary.TryGetValue(valueTuple, out usage))
          {
            usage = (dictionary[valueTuple] = new McpProbe.Usage());
            list3.Add(valueTuple);
          }
          usage.Count++;
          if (text != null && McpProbe.WriteVerbs.Contains(text))
          {
            usage.HasWrite = true;
          }
        }
      }
      if (dictionary.Count == 0)
      {
        return null;
      }
      CS$<>8__locals1.families = new Dictionary<string, McpProbe.Family>();
      List<string> list7 = new List<string>();
      foreach (ValueTuple<string, string> valueTuple2 in list3)
      {
        McpProbe.Usage usage2 = dictionary[valueTuple2];
        McpProbe.Family family;
        if (!CS$<>8__locals1.families.TryGetValue(valueTuple2.Item1, out family))
        {
          family = (CS$<>8__locals1.families[valueTuple2.Item1] = new McpProbe.Family());
          list7.Add(valueTuple2.Item1);
        }
        family.Total += usage2.Count;
        family.HasWrite |= usage2.HasWrite;
        if (valueTuple2.Item2 == valueTuple2.Item1)
        {
          family.BareCount += usage2.Count;
        }
        else
        {
          string[] array = valueTuple2.Item2.Split(' ', StringSplitOptions.None);
          string text4 = array[array.Length - 1];
          if (!family.TrailingCounts.ContainsKey(text4))
          {
            family.TrailingOrder.Add(text4);
          }
          family.TrailingCounts[text4] = family.TrailingCounts.GetValueOrDefault(text4) + usage2.Count;
        }
      }
      List<string> list8 = (from x in list7.Select<string, ValueTuple<string, int>>((string name, int index) => new ValueTuple<string, int>(name, index))
        orderby CS$<>8__locals1.families[x.Item1].Total descending, x.Item2
        select x.Item1).Take<string>(6).ToList<string>();
      CS$<>8__locals1.readWrite = list8.Where<string>((string n) => CS$<>8__locals1.families[n].HasWrite).Select<string, string>(new Func<string, string>(CS$<>8__locals1.<DeriveCapabilityDescription>g__Label|5)).ToList<string>();
      CS$<>8__locals1.readOnly = list8.Where<string>((string n) => !CS$<>8__locals1.families[n].HasWrite).Select<string, string>(new Func<string, string>(CS$<>8__locals1.<DeriveCapabilityDescription>g__Label|5)).ToList<string>();
      string text5 = displayName.Trim();
      CS$<>8__locals1.prefix = ((text5.Length == 0) ? "" : (text5 + ": "));
      string text6;
      for (;;)
      {
        text6 = CS$<>8__locals1.<DeriveCapabilityDescription>g__Compose|8();
        if (text6 == null)
        {
          goto IL_04DB;
        }
        if (text6.Length <= 240)
        {
          break;
        }
        if (CS$<>8__locals1.readOnly.Count > 0)
        {
          CS$<>8__locals1.readOnly.RemoveAt(CS$<>8__locals1.readOnly.Count - 1);
        }
        else
        {
          if (CS$<>8__locals1.readWrite.Count <= 1)
          {
            goto IL_04DB;
          }
          CS$<>8__locals1.readWrite.RemoveAt(CS$<>8__locals1.readWrite.Count - 1);
        }
      }
      return text6;
      IL_04DB:
      return null;
    }

    // Token: 0x06001B5D RID: 7005 RVA: 0x003DB398 File Offset: 0x003DB398
    private static List<string> Tokenize(string toolName)
    {
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = false;
      foreach (char c in toolName)
      {
        if (char.IsUpper(c) && flag)
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append(char.IsLetterOrDigit(c) ? c : ' ');
        flag = char.IsLower(c);
      }
      return stringBuilder.ToString().ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .ToList<string>();
    }

    // Token: 0x06001B5E RID: 7006 RVA: 0x003DB414 File Offset: 0x003DB414
    private static string Singularized(string word)
    {
      if (word.EndsWith("ies", StringComparison.Ordinal) && word.Length > 4)
      {
        return word.Substring(0, word.Length - 3) + "y";
      }
      if ((word.EndsWith("ses", StringComparison.Ordinal) || word.EndsWith("xes", StringComparison.Ordinal) || word.EndsWith("shes", StringComparison.Ordinal) || word.EndsWith("ches", StringComparison.Ordinal)) && word.Length > 4)
      {
        return word.Substring(0, word.Length - 2);
      }
      if (word.EndsWith('s') && !word.EndsWith("ss", StringComparison.Ordinal) && word.Length > 3)
      {
        return word.Substring(0, word.Length - 1);
      }
      return word;
    }

    // Token: 0x06001B5F RID: 7007 RVA: 0x003DB4DC File Offset: 0x003DB4DC
    private static string Pluralized(string word)
    {
      if (word.EndsWith('y') && word.Length > 2 && !"aeiou".Contains(word[word.Length - 2]))
      {
        return word.Substring(0, word.Length - 1) + "ies";
      }
      if (word.EndsWith('s') || word.EndsWith('x') || word.EndsWith("sh", StringComparison.Ordinal) || word.EndsWith("ch", StringComparison.Ordinal))
      {
        return word + "es";
      }
      return word + "s";
    }

    // Token: 0x04000BA6 RID: 2982
    public const int MaximumDescriptionLength = 240;

    // Token: 0x04000BA7 RID: 2983
    private const int MaximumNounFamilies = 6;

    // Token: 0x04000BA8 RID: 2984
    private const int MaximumToolListPages = 5;

    // Token: 0x04000BA9 RID: 2985
    private const string ProtocolVersion = "2025-06-18";

    // Token: 0x04000BAA RID: 2986
    private static readonly HashSet<string> ReadVerbs = new HashSet<string>
    {
      "list", "get", "search", "find", "fetch", "read", "query", "retrieve", "describe", "check",
      "show", "count", "lookup", "browse", "view", "preview", "extract"
    };

    // Token: 0x04000BAB RID: 2987
    private static readonly HashSet<string> WriteVerbs = new HashSet<string>
    {
      "create", "save", "update", "delete", "remove", "add", "set", "send", "post", "put",
      "patch", "upload", "activate", "pause", "boost", "merge", "submit", "resolve", "retire", "restore",
      "share", "unshare", "mark", "connect", "disconnect", "archive", "move", "assign", "import", "export",
      "sync", "run", "execute", "trigger", "cancel", "start", "stop", "edit", "rename", "publish",
      "unpublish", "invite", "reply", "comment", "schedule", "prepare", "finalize", "log", "write", "insert",
      "append", "duplicate", "clone", "generate", "apply", "approve", "reject", "complete", "close", "open",
      "enable", "disable", "toggle", "attach", "detach", "link", "unlink"
    };

    // Token: 0x04000BAC RID: 2988
    private static readonly HashSet<string> NoiseTokens = new HashSet<string>
    {
      "by", "id", "ids", "from", "to", "for", "with", "and", "or", "a",
      "an", "the", "of", "in", "on", "all", "my", "me", "one", "single",
      "multiple", "batch", "v1", "v2", "api", "tool", "tools", "mcp", "local", "remote",
      "ui", "internal"
    };

    // Token: 0x020005F5 RID: 1525
    [Nullable(0)]
    private sealed class Posted : IEquatable<McpProbe.Posted>
    {
      // Token: 0x06003798 RID: 14232 RVA: 0x0044A404 File Offset: 0x0044A404
      public Posted(int Status, string Body, bool IsEventStream, [Nullable(2)] string SessionId)
      {
        this.Status = Status;
        this.Body = Body;
        this.IsEventStream = IsEventStream;
        this.SessionId = SessionId;
        base..ctor();
      }

      // Token: 0x170009AD RID: 2477
      // (get) Token: 0x06003799 RID: 14233 RVA: 0x0044A42C File Offset: 0x0044A42C
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(McpProbe.Posted);
        }
      }

      // Token: 0x170009AE RID: 2478
      // (get) Token: 0x0600379A RID: 14234 RVA: 0x0044A438 File Offset: 0x0044A438
      // (set) Token: 0x0600379B RID: 14235 RVA: 0x0044A440 File Offset: 0x0044A440
      public int Status { get; set; }

      // Token: 0x170009AF RID: 2479
      // (get) Token: 0x0600379C RID: 14236 RVA: 0x0044A44C File Offset: 0x0044A44C
      // (set) Token: 0x0600379D RID: 14237 RVA: 0x0044A454 File Offset: 0x0044A454
      public string Body { get; set; }

      // Token: 0x170009B0 RID: 2480
      // (get) Token: 0x0600379E RID: 14238 RVA: 0x0044A460 File Offset: 0x0044A460
      // (set) Token: 0x0600379F RID: 14239 RVA: 0x0044A468 File Offset: 0x0044A468
      public bool IsEventStream { get; set; }

      // Token: 0x170009B1 RID: 2481
      // (get) Token: 0x060037A0 RID: 14240 RVA: 0x0044A474 File Offset: 0x0044A474
      // (set) Token: 0x060037A1 RID: 14241 RVA: 0x0044A47C File Offset: 0x0044A47C
      [Nullable(2)]
      public string SessionId
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x060037A2 RID: 14242 RVA: 0x0044A488 File Offset: 0x0044A488
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Posted");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060037A3 RID: 14243 RVA: 0x0044A4D4 File Offset: 0x0044A4D4
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Status = ");
        builder.Append(this.Status.ToString());
        builder.Append(", Body = ");
        builder.Append(this.Body);
        builder.Append(", IsEventStream = ");
        builder.Append(this.IsEventStream.ToString());
        builder.Append(", SessionId = ");
        builder.Append(this.SessionId);
        return true;
      }

      // Token: 0x060037A4 RID: 14244 RVA: 0x0044A568 File Offset: 0x0044A568
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(McpProbe.Posted left, McpProbe.Posted right)
      {
        return !(left == right);
      }

      // Token: 0x060037A5 RID: 14245 RVA: 0x0044A574 File Offset: 0x0044A574
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(McpProbe.Posted left, McpProbe.Posted right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060037A6 RID: 14246 RVA: 0x0044A588 File Offset: 0x0044A588
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Body>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsEventStream>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SessionId>k__BackingField);
      }

      // Token: 0x060037A7 RID: 14247 RVA: 0x0044A604 File Offset: 0x0044A604
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as McpProbe.Posted);
      }

      // Token: 0x060037A8 RID: 14248 RVA: 0x0044A614 File Offset: 0x0044A614
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(McpProbe.Posted other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Body>k__BackingField, other.<Body>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsEventStream>k__BackingField, other.<IsEventStream>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SessionId>k__BackingField, other.<SessionId>k__BackingField));
      }

      // Token: 0x060037AA RID: 14250 RVA: 0x0044A6A8 File Offset: 0x0044A6A8
      [CompilerGenerated]
      private Posted(McpProbe.Posted original)
      {
        this.Status = original.<Status>k__BackingField;
        this.Body = original.<Body>k__BackingField;
        this.IsEventStream = original.<IsEventStream>k__BackingField;
        this.SessionId = original.<SessionId>k__BackingField;
      }

      // Token: 0x060037AB RID: 14251 RVA: 0x0044A6E0 File Offset: 0x0044A6E0
      [CompilerGenerated]
      public void Deconstruct(out int Status, out string Body, out bool IsEventStream, [Nullable(2)] out string SessionId)
      {
        Status = this.Status;
        Body = this.Body;
        IsEventStream = this.IsEventStream;
        SessionId = this.SessionId;
      }
    }

    // Token: 0x020005F6 RID: 1526
    [NullableContext(0)]
    private sealed class Usage
    {
      // Token: 0x040022AE RID: 8878
      public int Count;

      // Token: 0x040022AF RID: 8879
      public bool HasWrite;
    }

    // Token: 0x020005F7 RID: 1527
    [Nullable(0)]
    private sealed class Family
    {
      // Token: 0x040022B0 RID: 8880
      public int Total;

      // Token: 0x040022B1 RID: 8881
      public bool HasWrite;

      // Token: 0x040022B2 RID: 8882
      public int BareCount;

      // Token: 0x040022B3 RID: 8883
      public readonly Dictionary<string, int> TrailingCounts = new Dictionary<string, int>();

      // Token: 0x040022B4 RID: 8884
      public readonly List<string> TrailingOrder = new List<string>();
    }

    // Token: 0x020005F8 RID: 1528
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040022B5 RID: 8885
      [Nullable(0)]
      public static Func<string, List<string>> <0>__Tokenize;
    }
  }
}
