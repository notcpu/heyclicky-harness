using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace HeyClicky.Core.Browser
{
  // Token: 0x020001EA RID: 490
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class LoopbackMcpServer : IDisposable
  {
    // Token: 0x06001CAD RID: 7341 RVA: 0x003E46D0 File Offset: 0x003E46D0
    public LoopbackMcpServer(string serverName, IReadOnlyList<LoopbackMcpServer.ToolDefinition> tools, LoopbackMcpServer.ToolCallHandler handler)
    {
      this.serverName = serverName;
      this.tools = tools;
      this.handler = handler;
      this.http = new LoopbackHttpServer(new Func<LoopbackHttpServer.Request, Task<LoopbackHttpServer.Response>>(this.HandleAsync), "/mcp");
    }

    // Token: 0x1700065C RID: 1628
    // (get) Token: 0x06001CAE RID: 7342 RVA: 0x003E470C File Offset: 0x003E470C
    public string BearerToken
    {
      get
      {
        return this.http.BearerToken;
      }
    }

    // Token: 0x1700065D RID: 1629
    // (get) Token: 0x06001CAF RID: 7343 RVA: 0x003E471C File Offset: 0x003E471C
    public int? Port
    {
      get
      {
        return this.http.Port;
      }
    }

    // Token: 0x1700065E RID: 1630
    // (get) Token: 0x06001CB0 RID: 7344 RVA: 0x003E472C File Offset: 0x003E472C
    [Nullable(2)]
    public string Url
    {
      [NullableContext(2)]
      get
      {
        return this.http.Url;
      }
    }

    // Token: 0x1700065F RID: 1631
    // (get) Token: 0x06001CB1 RID: 7345 RVA: 0x003E473C File Offset: 0x003E473C
    public bool IsListening
    {
      get
      {
        return this.http.IsListening;
      }
    }

    // Token: 0x06001CB2 RID: 7346 RVA: 0x003E474C File Offset: 0x003E474C
    public void Start()
    {
      this.http.Start();
    }

    // Token: 0x06001CB3 RID: 7347 RVA: 0x003E475C File Offset: 0x003E475C
    public void Stop()
    {
      this.http.Stop();
    }

    // Token: 0x06001CB4 RID: 7348 RVA: 0x003E476C File Offset: 0x003E476C
    public void Dispose()
    {
      this.http.Dispose();
    }

    // Token: 0x06001CB5 RID: 7349 RVA: 0x003E477C File Offset: 0x003E477C
    private Task<LoopbackHttpServer.Response> HandleAsync(LoopbackHttpServer.Request request)
    {
      LoopbackMcpServer.<HandleAsync>d__20 <HandleAsync>d__;
      <HandleAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoopbackHttpServer.Response>.Create();
      <HandleAsync>d__.<>4__this = this;
      <HandleAsync>d__.request = request;
      <HandleAsync>d__.<>1__state = -1;
      <HandleAsync>d__.<>t__builder.Start<LoopbackMcpServer.<HandleAsync>d__20>(ref <HandleAsync>d__);
      return <HandleAsync>d__.<>t__builder.Task;
    }

    // Token: 0x06001CB6 RID: 7350 RVA: 0x003E47C8 File Offset: 0x003E47C8
    public static string JsonText(JsonObject value)
    {
      return LoopbackMcpServer.Sorted(value).ToJsonString(null);
    }

    // Token: 0x06001CB7 RID: 7351 RVA: 0x003E47D8 File Offset: 0x003E47D8
    [NullableContext(2)]
    private static JsonNode Sorted(JsonNode node)
    {
      JsonObject jsonObject = node as JsonObject;
      JsonNode jsonNode;
      if (jsonObject == null)
      {
        JsonArray jsonArray = node as JsonArray;
        if (jsonArray == null)
        {
          if (node != null)
          {
            jsonNode = node.DeepClone();
          }
          else
          {
            jsonNode = null;
          }
        }
        else
        {
          IEnumerable<JsonNode> enumerable = jsonArray;
          Func<JsonNode, JsonNode> func;
          if ((func = LoopbackMcpServer.<>O.<0>__Sorted) == null)
          {
            func = (LoopbackMcpServer.<>O.<0>__Sorted = new Func<JsonNode, JsonNode>(LoopbackMcpServer.Sorted));
          }
          jsonNode = new JsonArray(enumerable.Select<JsonNode, JsonNode>(func).ToArray<JsonNode>());
        }
      }
      else
      {
        jsonNode = new JsonObject(from kv in jsonObject.OrderBy<KeyValuePair<string, JsonNode>, string>((KeyValuePair<string, JsonNode> kv) => kv.Key, StringComparer.Ordinal)
          select KeyValuePair.Create<string, JsonNode>(kv.Key, LoopbackMcpServer.Sorted(kv.Value)), null);
      }
      return jsonNode;
    }

    // Token: 0x06001CB8 RID: 7352 RVA: 0x003E48A0 File Offset: 0x003E48A0
    [CompilerGenerated]
    internal static LoopbackHttpServer.Response <HandleAsync>g__Reply|20_0(JsonObject payload, ref LoopbackMcpServer.<>c__DisplayClass20_0 A_1)
    {
      payload["jsonrpc"] = "2.0";
      payload["id"] = A_1.requestId;
      return LoopbackHttpServer.Response.Json(200, payload, A_1.sessionHeaders);
    }

    // Token: 0x04000C52 RID: 3154
    public const string ContentItemsResultKey = "mcp_content";

    // Token: 0x04000C53 RID: 3155
    public const string ServerVersion = "1";

    // Token: 0x04000C54 RID: 3156
    private readonly string serverName;

    // Token: 0x04000C55 RID: 3157
    private readonly IReadOnlyList<LoopbackMcpServer.ToolDefinition> tools;

    // Token: 0x04000C56 RID: 3158
    private readonly LoopbackMcpServer.ToolCallHandler handler;

    // Token: 0x04000C57 RID: 3159
    private readonly LoopbackHttpServer http;

    // Token: 0x02000634 RID: 1588
    [Nullable(0)]
    public sealed class ToolDefinition : IEquatable<LoopbackMcpServer.ToolDefinition>
    {
      // Token: 0x060038D2 RID: 14546 RVA: 0x00452EC8 File Offset: 0x00452EC8
      public ToolDefinition(string Name, string Description, JsonObject InputSchema)
      {
        this.Name = Name;
        this.Description = Description;
        this.InputSchema = InputSchema;
        base..ctor();
      }

      // Token: 0x170009D0 RID: 2512
      // (get) Token: 0x060038D3 RID: 14547 RVA: 0x00452EE8 File Offset: 0x00452EE8
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(LoopbackMcpServer.ToolDefinition);
        }
      }

      // Token: 0x170009D1 RID: 2513
      // (get) Token: 0x060038D4 RID: 14548 RVA: 0x00452EF4 File Offset: 0x00452EF4
      // (set) Token: 0x060038D5 RID: 14549 RVA: 0x00452EFC File Offset: 0x00452EFC
      public string Name { get; set; }

      // Token: 0x170009D2 RID: 2514
      // (get) Token: 0x060038D6 RID: 14550 RVA: 0x00452F08 File Offset: 0x00452F08
      // (set) Token: 0x060038D7 RID: 14551 RVA: 0x00452F10 File Offset: 0x00452F10
      public string Description { get; set; }

      // Token: 0x170009D3 RID: 2515
      // (get) Token: 0x060038D8 RID: 14552 RVA: 0x00452F1C File Offset: 0x00452F1C
      // (set) Token: 0x060038D9 RID: 14553 RVA: 0x00452F24 File Offset: 0x00452F24
      public JsonObject InputSchema { get; set; }

      // Token: 0x060038DA RID: 14554 RVA: 0x00452F30 File Offset: 0x00452F30
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ToolDefinition");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060038DB RID: 14555 RVA: 0x00452F7C File Offset: 0x00452F7C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Name = ");
        builder.Append(this.Name);
        builder.Append(", Description = ");
        builder.Append(this.Description);
        builder.Append(", InputSchema = ");
        builder.Append(this.InputSchema);
        return true;
      }

      // Token: 0x060038DC RID: 14556 RVA: 0x00452FDC File Offset: 0x00452FDC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(LoopbackMcpServer.ToolDefinition left, LoopbackMcpServer.ToolDefinition right)
      {
        return !(left == right);
      }

      // Token: 0x060038DD RID: 14557 RVA: 0x00452FE8 File Offset: 0x00452FE8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(LoopbackMcpServer.ToolDefinition left, LoopbackMcpServer.ToolDefinition right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060038DE RID: 14558 RVA: 0x00452FFC File Offset: 0x00452FFC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<JsonObject>.Default.GetHashCode(this.<InputSchema>k__BackingField);
      }

      // Token: 0x060038DF RID: 14559 RVA: 0x00453060 File Offset: 0x00453060
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as LoopbackMcpServer.ToolDefinition);
      }

      // Token: 0x060038E0 RID: 14560 RVA: 0x00453070 File Offset: 0x00453070
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(LoopbackMcpServer.ToolDefinition other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<JsonObject>.Default.Equals(this.<InputSchema>k__BackingField, other.<InputSchema>k__BackingField));
      }

      // Token: 0x060038E2 RID: 14562 RVA: 0x004530EC File Offset: 0x004530EC
      [CompilerGenerated]
      private ToolDefinition(LoopbackMcpServer.ToolDefinition original)
      {
        this.Name = original.<Name>k__BackingField;
        this.Description = original.<Description>k__BackingField;
        this.InputSchema = original.<InputSchema>k__BackingField;
      }

      // Token: 0x060038E3 RID: 14563 RVA: 0x00453118 File Offset: 0x00453118
      [CompilerGenerated]
      public void Deconstruct(out string Name, out string Description, out JsonObject InputSchema)
      {
        Name = this.Name;
        Description = this.Description;
        InputSchema = this.InputSchema;
      }
    }

    // Token: 0x02000635 RID: 1589
    // (Invoke) Token: 0x060038E5 RID: 14565
    [NullableContext(0)]
    public delegate Task<JsonObject> ToolCallHandler(string toolName, JsonObject arguments, JsonObject meta);

    // Token: 0x02000636 RID: 1590
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04002439 RID: 9273
      [Nullable(0)]
      public static Func<JsonNode, JsonNode> <0>__Sorted;
    }
  }
}
