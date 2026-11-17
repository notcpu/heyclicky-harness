using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000CA RID: 202
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class CustomConnectorSecrets
  {
    // Token: 0x17000164 RID: 356
    // (get) Token: 0x060008B6 RID: 2230 RVA: 0x005403E0 File Offset: 0x005403E0
    // (set) Token: 0x060008B7 RID: 2231 RVA: 0x005403E8 File Offset: 0x005403E8
    public string BearerToken { get; set; }

    // Token: 0x17000165 RID: 357
    // (get) Token: 0x060008B8 RID: 2232 RVA: 0x005403F4 File Offset: 0x005403F4
    // (set) Token: 0x060008B9 RID: 2233 RVA: 0x005403FC File Offset: 0x005403FC
    [Nullable(1)]
    public Dictionary<string, string> Headers
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    // Token: 0x17000166 RID: 358
    // (get) Token: 0x060008BA RID: 2234 RVA: 0x00540408 File Offset: 0x00540408
    // (set) Token: 0x060008BB RID: 2235 RVA: 0x00540410 File Offset: 0x00540410
    [Nullable(1)]
    public Dictionary<string, string> Environment
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = new Dictionary<string, string>(StringComparer.Ordinal);

    // Token: 0x17000167 RID: 359
    // (get) Token: 0x060008BC RID: 2236 RVA: 0x0054041C File Offset: 0x0054041C
    // (set) Token: 0x060008BD RID: 2237 RVA: 0x00540424 File Offset: 0x00540424
    public string RefreshToken { get; set; }

    // Token: 0x17000168 RID: 360
    // (get) Token: 0x060008BE RID: 2238 RVA: 0x00540430 File Offset: 0x00540430
    // (set) Token: 0x060008BF RID: 2239 RVA: 0x00540438 File Offset: 0x00540438
    public DateTimeOffset? ExpiresAt { get; set; }

    // Token: 0x17000169 RID: 361
    // (get) Token: 0x060008C0 RID: 2240 RVA: 0x00540444 File Offset: 0x00540444
    // (set) Token: 0x060008C1 RID: 2241 RVA: 0x0054044C File Offset: 0x0054044C
    public string TokenEndpoint { get; set; }

    // Token: 0x1700016A RID: 362
    // (get) Token: 0x060008C2 RID: 2242 RVA: 0x00540458 File Offset: 0x00540458
    // (set) Token: 0x060008C3 RID: 2243 RVA: 0x00540460 File Offset: 0x00540460
    public string RegisteredClientId { get; set; }

    // Token: 0x1700016B RID: 363
    // (get) Token: 0x060008C4 RID: 2244 RVA: 0x0054046C File Offset: 0x0054046C
    // (set) Token: 0x060008C5 RID: 2245 RVA: 0x00540474 File Offset: 0x00540474
    public string RegisteredClientSecret { get; set; }

    // Token: 0x1700016C RID: 364
    // (get) Token: 0x060008C6 RID: 2246 RVA: 0x00540480 File Offset: 0x00540480
    // (set) Token: 0x060008C7 RID: 2247 RVA: 0x00540488 File Offset: 0x00540488
    public string RegisteredClientRedirectUri { get; set; }
  }
}
