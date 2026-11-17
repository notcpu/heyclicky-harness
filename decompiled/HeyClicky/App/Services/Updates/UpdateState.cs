using System;

namespace HeyClicky.App.Services.Updates
{
  // Token: 0x020000ED RID: 237
  public enum UpdateState
  {
    // Token: 0x04000428 RID: 1064
    Unavailable,
    // Token: 0x04000429 RID: 1065
    Idle,
    // Token: 0x0400042A RID: 1066
    Checking,
    // Token: 0x0400042B RID: 1067
    UpToDate,
    // Token: 0x0400042C RID: 1068
    Downloading,
    // Token: 0x0400042D RID: 1069
    ReadyToInstall,
    // Token: 0x0400042E RID: 1070
    Failed
  }
}
