using System;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Tools
{
  // Token: 0x020000A5 RID: 165
  [NullableContext(1)]
  [Nullable(0)]
  public static class ToolResults
  {
    // Token: 0x06000A41 RID: 2625 RVA: 0x00373FD0 File Offset: 0x00373FD0
    public static string Json(JsonObject result)
    {
      return result.ToJsonString(ToolResults.Options);
    }

    // Token: 0x06000A42 RID: 2626 RVA: 0x00373FE0 File Offset: 0x00373FE0
    public static string Status(string status, string message)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["status"] = status;
      jsonObject["message"] = message;
      return ToolResults.Json(jsonObject);
    }

    // Token: 0x06000A43 RID: 2627 RVA: 0x00374024 File Offset: 0x00374024
    public static string Summary(string status, string toolName, string summary, [Nullable(2)] JsonObject result = null)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["status"] = status;
      jsonObject["toolName"] = toolName;
      jsonObject["summary"] = summary;
      JsonObject jsonObject2 = jsonObject;
      if (result != null)
      {
        jsonObject2["result"] = result;
      }
      return ToolResults.Json(jsonObject2);
    }

    // Token: 0x06000A44 RID: 2628 RVA: 0x00374088 File Offset: 0x00374088
    public static string Truncate(string text, int limit)
    {
      if (text.Length > limit)
      {
        return text.Substring(0, limit) + "…";
      }
      return text;
    }

    // Token: 0x040004A2 RID: 1186
    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
      Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
  }
}
