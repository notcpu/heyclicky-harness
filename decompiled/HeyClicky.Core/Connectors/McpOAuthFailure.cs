using System;

namespace HeyClicky.Core.Connectors
{
  // Token: 0x020001D6 RID: 470
  public enum McpOAuthFailure
  {
    // Token: 0x04000B90 RID: 2960
    ServerDoesNotRequireOAuth,
    // Token: 0x04000B91 RID: 2961
    ServerUnreachable,
    // Token: 0x04000B92 RID: 2962
    DiscoveryFailed,
    // Token: 0x04000B93 RID: 2963
    CallbackPortUnavailable,
    // Token: 0x04000B94 RID: 2964
    AuthorizationDenied,
    // Token: 0x04000B95 RID: 2965
    StateMismatch,
    // Token: 0x04000B96 RID: 2966
    RegistrationFailed,
    // Token: 0x04000B97 RID: 2967
    TokenExchangeFailed,
    // Token: 0x04000B98 RID: 2968
    TimedOut
  }
}
