using System;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace HeyClicky.Core.Billing
{
  // Token: 0x02000218 RID: 536
  public static class BillingPaywallDetector
  {
    // Token: 0x14000040 RID: 64
    // (add) Token: 0x06001F93 RID: 8083 RVA: 0x003F25CC File Offset: 0x003F25CC
    // (remove) Token: 0x06001F94 RID: 8084 RVA: 0x003F2600 File Offset: 0x003F2600
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public static event Action<PaywallPayload, JsonElement> PaywallDetected;

    // Token: 0x06001F95 RID: 8085 RVA: 0x003F2634 File Offset: 0x003F2634
    public static bool HandleIfPaywall(int httpStatusCode, ReadOnlySpan<byte> responseBody, bool presentsPaywall = true)
    {
      PaywallPayload paywallPayload;
      JsonElement jsonElement;
      return BillingPaywallDetector.HandleIfPaywall(httpStatusCode, responseBody, presentsPaywall, out paywallPayload, out jsonElement);
    }

    // Token: 0x06001F96 RID: 8086 RVA: 0x003F2650 File Offset: 0x003F2650
    public static bool HandleIfPaywall(int httpStatusCode, ReadOnlySpan<byte> responseBody, bool presentsPaywall, [Nullable(2)] out PaywallPayload payload, out JsonElement raw)
    {
      payload = null;
      raw = default(JsonElement);
      if (httpStatusCode != 402 || !PaywallPayload.TryDecode(responseBody, out payload, out raw))
      {
        return false;
      }
      if (presentsPaywall)
      {
        string text = "Billing";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 4);
        defaultInterpolatedStringHandler.AppendLiteral("paywall: surface=");
        defaultInterpolatedStringHandler.AppendFormatted(payload.Surface);
        defaultInterpolatedStringHandler.AppendLiteral(" reason=");
        defaultInterpolatedStringHandler.AppendFormatted(payload.Reason);
        defaultInterpolatedStringHandler.AppendLiteral(" usage=");
        defaultInterpolatedStringHandler.AppendFormatted<int>(payload.CurrentUsage);
        defaultInterpolatedStringHandler.AppendLiteral("/");
        defaultInterpolatedStringHandler.AppendFormatted<int>(payload.Limit);
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        try
        {
          Action<PaywallPayload, JsonElement> paywallDetected = BillingPaywallDetector.PaywallDetected;
          if (paywallDetected != null)
          {
            paywallDetected(payload, raw);
          }
        }
        catch (Exception ex)
        {
          Log.Error("Billing", "paywall handler threw", ex);
        }
      }
      return true;
    }

    // Token: 0x06001F97 RID: 8087 RVA: 0x003F2748 File Offset: 0x003F2748
    [NullableContext(1)]
    public static void Present(PaywallPayload payload)
    {
      string text = "Billing";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 3);
      defaultInterpolatedStringHandler.AppendLiteral("paywall (local): surface=");
      defaultInterpolatedStringHandler.AppendFormatted(payload.Surface);
      defaultInterpolatedStringHandler.AppendLiteral(" usage=");
      defaultInterpolatedStringHandler.AppendFormatted<int>(payload.CurrentUsage);
      defaultInterpolatedStringHandler.AppendLiteral("/");
      defaultInterpolatedStringHandler.AppendFormatted<int>(payload.Limit);
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      try
      {
        Action<PaywallPayload, JsonElement> paywallDetected = BillingPaywallDetector.PaywallDetected;
        if (paywallDetected != null)
        {
          paywallDetected(payload, default(JsonElement));
        }
      }
      catch (Exception ex)
      {
        Log.Error("Billing", "paywall handler threw", ex);
      }
    }
  }
}
