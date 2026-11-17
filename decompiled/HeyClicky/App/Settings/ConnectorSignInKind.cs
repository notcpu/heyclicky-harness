using System;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000C6 RID: 198
  public enum ConnectorSignInKind
  {
    // Token: 0x040002DA RID: 730
    Unknown,
    // Token: 0x040002DB RID: 731
    Checking,
    // Token: 0x040002DC RID: 732
    SignInUnavailable,
    // Token: 0x040002DD RID: 733
    NotNeeded,
    // Token: 0x040002DE RID: 734
    NeedsSignIn,
    // Token: 0x040002DF RID: 735
    AwaitingBrowser,
    // Token: 0x040002E0 RID: 736
    SignedIn,
    // Token: 0x040002E1 RID: 737
    Failed,
    // Token: 0x040002E2 RID: 738
    TokenRejected
  }
}
