using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HeyClicky
{
  // Token: 0x02000019 RID: 25
  [NullableContext(2)]
  public interface IPermissionService
  {
    // Token: 0x14000005 RID: 5
    // (add) Token: 0x06000109 RID: 265
    // (remove) Token: 0x0600010A RID: 266
    event EventHandler Changed;

    // Token: 0x0600010B RID: 267
    PermissionStatus GetStatus(PermissionKind kind);

    // Token: 0x0600010C RID: 268
    [NullableContext(1)]
    Task<PermissionStatus> RequestAsync(PermissionKind kind);

    // Token: 0x0600010D RID: 269
    void OpenSystemSettings(PermissionKind kind);

    // Token: 0x0600010E RID: 270
    void StartPolling();

    // Token: 0x0600010F RID: 271
    void StopPolling();

    // Token: 0x1700004C RID: 76
    // (get) Token: 0x06000110 RID: 272 RVA: 0x0001E6F8 File Offset: 0x0001E6F8
    MicrophoneBlock MicrophoneBlockedBy
    {
      get
      {
        return MicrophoneBlock.None;
      }
    }
  }
}
