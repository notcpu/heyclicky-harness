using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using HeyClicky.App.Dock;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000162 RID: 354
  public static class NotchUpdateCardAnalytics
  {
    // Token: 0x06001370 RID: 4976 RVA: 0x005A2138 File Offset: 0x005A2138
    [NullableContext(1)]
    public static JsonObject ShownProperties(ArcCardContent content, bool isDone)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["state"] = (content.IsFinalizing ? "finalizing" : ArcDockAnalytics.StateName(content.State));
      JsonObject jsonObject2 = jsonObject;
      if (isDone)
      {
        JsonNode jsonNode = jsonObject2;
        string text = "output_kind";
        CardReadyOutput readyOutput = content.ReadyOutput;
        jsonNode[text] = ((readyOutput != null) ? ArcDockAnalytics.OutputKind(readyOutput.Kind) : "none");
        jsonObject2["has_result_card"] = content.Answer != null;
      }
      else
      {
        CardFooterRequest request = content.Request;
        if (request != null)
        {
          jsonObject2["request"] = ArcDockAnalytics.RequestKind(request.Kind);
        }
      }
      return jsonObject2;
    }
  }
}
