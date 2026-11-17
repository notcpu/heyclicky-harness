using System;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace HeyClicky.Core.Billing
{
  // Token: 0x02000219 RID: 537
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PaywallReachedException : Exception
  {
    // Token: 0x06001F98 RID: 8088 RVA: 0x003F27FC File Offset: 0x003F27FC
    public PaywallReachedException(PaywallPayload payload, JsonElement raw)
      : base("Paywall (" + payload.Surface + ")")
    {
    }

    // Token: 0x1700071A RID: 1818
    // (get) Token: 0x06001F99 RID: 8089 RVA: 0x003F2828 File Offset: 0x003F2828
    public PaywallPayload Payload { get; } = payload;

    // Token: 0x1700071B RID: 1819
    // (get) Token: 0x06001F9A RID: 8090 RVA: 0x003F2830 File Offset: 0x003F2830
    public JsonElement Raw { get; } = raw;
  }
}
