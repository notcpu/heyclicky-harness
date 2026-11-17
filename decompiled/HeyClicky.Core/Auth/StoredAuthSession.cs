using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Auth
{
  // Token: 0x02000227 RID: 551
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class StoredAuthSession
  {
    // Token: 0x1700073F RID: 1855
    // (get) Token: 0x06002038 RID: 8248 RVA: 0x003F4BE8 File Offset: 0x003F4BE8
    // (set) Token: 0x06002039 RID: 8249 RVA: 0x003F4BF0 File Offset: 0x003F4BF0
    public string AccessToken { get; set; }

    // Token: 0x17000740 RID: 1856
    // (get) Token: 0x0600203A RID: 8250 RVA: 0x003F4BFC File Offset: 0x003F4BFC
    // (set) Token: 0x0600203B RID: 8251 RVA: 0x003F4C04 File Offset: 0x003F4C04
    public string RefreshToken { get; set; }

    // Token: 0x17000741 RID: 1857
    // (get) Token: 0x0600203C RID: 8252 RVA: 0x003F4C10 File Offset: 0x003F4C10
    // (set) Token: 0x0600203D RID: 8253 RVA: 0x003F4C18 File Offset: 0x003F4C18
    public string UserId { get; set; }

    // Token: 0x17000742 RID: 1858
    // (get) Token: 0x0600203E RID: 8254 RVA: 0x003F4C24 File Offset: 0x003F4C24
    // (set) Token: 0x0600203F RID: 8255 RVA: 0x003F4C2C File Offset: 0x003F4C2C
    public string Email { get; set; }

    // Token: 0x17000743 RID: 1859
    // (get) Token: 0x06002040 RID: 8256 RVA: 0x003F4C38 File Offset: 0x003F4C38
    // (set) Token: 0x06002041 RID: 8257 RVA: 0x003F4C40 File Offset: 0x003F4C40
    public string DisplayName { get; set; }

    // Token: 0x17000744 RID: 1860
    // (get) Token: 0x06002042 RID: 8258 RVA: 0x003F4C4C File Offset: 0x003F4C4C
    // (set) Token: 0x06002043 RID: 8259 RVA: 0x003F4C54 File Offset: 0x003F4C54
    public string AvatarUrl { get; set; }

    // Token: 0x17000745 RID: 1861
    // (get) Token: 0x06002044 RID: 8260 RVA: 0x003F4C60 File Offset: 0x003F4C60
    // (set) Token: 0x06002045 RID: 8261 RVA: 0x003F4C68 File Offset: 0x003F4C68
    public double? SignupDateEpochSeconds { get; set; }
  }
}
