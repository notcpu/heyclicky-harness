using System;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace HeyClicky.Agents.Backend
{
  // Token: 0x02000071 RID: 113
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentBackendException : Exception
  {
    // Token: 0x060008E5 RID: 2277 RVA: 0x00102840 File Offset: 0x00102840
    public AgentBackendException(string route, int statusCode, string body)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 3);
      defaultInterpolatedStringHandler.AppendFormatted(route);
      defaultInterpolatedStringHandler.AppendLiteral(" returned ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(statusCode);
      defaultInterpolatedStringHandler.AppendLiteral(": ");
      defaultInterpolatedStringHandler.AppendFormatted(AgentBackendException.Truncate(body));
      base..ctor(defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x17000240 RID: 576
    // (get) Token: 0x060008E6 RID: 2278 RVA: 0x001028B0 File Offset: 0x001028B0
    public string Route { get; } = route;

    // Token: 0x17000241 RID: 577
    // (get) Token: 0x060008E7 RID: 2279 RVA: 0x001028B8 File Offset: 0x001028B8
    public int StatusCode { get; } = statusCode;

    // Token: 0x17000242 RID: 578
    // (get) Token: 0x060008E8 RID: 2280 RVA: 0x001028C0 File Offset: 0x001028C0
    public string Body { get; } = body;

    // Token: 0x17000243 RID: 579
    // (get) Token: 0x060008E9 RID: 2281 RVA: 0x001028C8 File Offset: 0x001028C8
    public bool IsPaywall
    {
      get
      {
        return this.StatusCode == 402 || this.Body.Contains("\"paywall\"", StringComparison.OrdinalIgnoreCase);
      }
    }

    // Token: 0x17000244 RID: 580
    // (get) Token: 0x060008EA RID: 2282 RVA: 0x001028EC File Offset: 0x001028EC
    public JsonElement? BodyJson
    {
      get
      {
        JsonElement? jsonElement;
        try
        {
          jsonElement = new JsonElement?(JsonDocument.Parse(this.Body, default(JsonDocumentOptions)).RootElement.Clone());
        }
        catch (JsonException)
        {
          jsonElement = null;
        }
        return jsonElement;
      }
    }

    // Token: 0x060008EB RID: 2283 RVA: 0x00102940 File Offset: 0x00102940
    private static string Truncate(string s)
    {
      if (s.Length <= 400)
      {
        return s;
      }
      return s.Substring(0, 400) + "...";
    }
  }
}
