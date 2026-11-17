using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Auth
{
  // Token: 0x02000228 RID: 552
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class StoredPendingSignIn
  {
    // Token: 0x17000746 RID: 1862
    // (get) Token: 0x06002047 RID: 8263 RVA: 0x003F4C7C File Offset: 0x003F4C7C
    // (set) Token: 0x06002048 RID: 8264 RVA: 0x003F4C84 File Offset: 0x003F4C84
    public string State { get; set; }

    // Token: 0x17000747 RID: 1863
    // (get) Token: 0x06002049 RID: 8265 RVA: 0x003F4C90 File Offset: 0x003F4C90
    // (set) Token: 0x0600204A RID: 8266 RVA: 0x003F4C98 File Offset: 0x003F4C98
    public double? StartedAtEpochSeconds { get; set; }
  }
}
