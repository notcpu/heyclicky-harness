using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.Agents.Backend
{
  // Token: 0x0200006E RID: 110
  [NullableContext(1)]
  [Nullable(0)]
  internal static class AgentIntegrationsApi
  {
    // Token: 0x060008D9 RID: 2265 RVA: 0x0010252C File Offset: 0x0010252C
    public static AgentIntegrationCatalog ParseCatalog([Nullable(2)] JsonObject o)
    {
      if (o == null)
      {
        return new AgentIntegrationCatalog(false, Array.Empty<AgentIntegration>());
      }
      JsonValue jsonValue = o["composio_configured"] as JsonValue;
      bool flag2;
      bool flag = jsonValue == null || !jsonValue.TryGetValue<bool>(out flag2) || flag2;
      JsonArray jsonArray;
      if ((jsonArray = o["integrations"] as JsonArray) == null)
      {
        jsonArray = new JsonArray(null);
      }
      IEnumerable<JsonObject> enumerable = jsonArray.OfType<JsonObject>();
      Func<JsonObject, AgentIntegration> func;
      if ((func = AgentIntegrationsApi.<>O.<0>__Parse) == null)
      {
        func = (AgentIntegrationsApi.<>O.<0>__Parse = new Func<JsonObject, AgentIntegration>(AgentIntegration.Parse));
      }
      List<AgentIntegration> list = enumerable.Select<JsonObject, AgentIntegration>(func).OfType<AgentIntegration>().ToList<AgentIntegration>();
      return new AgentIntegrationCatalog(flag, list);
    }

    // Token: 0x060008DA RID: 2266 RVA: 0x001025C4 File Offset: 0x001025C4
    public static string ErrorText(string body, string fallback)
    {
      try
      {
        JsonObject jsonObject = JsonNode.Parse(body, null, default(JsonDocumentOptions)) as JsonObject;
        if (jsonObject != null)
        {
          JsonNode jsonNode = jsonObject["error"];
          string text = ((jsonNode != null) ? jsonNode.ToString() : null);
          if (text != null && text.Length > 0)
          {
            return text;
          }
        }
      }
      catch (JsonException)
      {
      }
      return fallback;
    }

    // Token: 0x020001CC RID: 460
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04000864 RID: 2148
      [Nullable(0)]
      public static Func<JsonObject, AgentIntegration> <0>__Parse;
    }
  }
}
