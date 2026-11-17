using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Connectors
{
  // Token: 0x020001D7 RID: 471
  public sealed class McpOAuthException : Exception
  {
    // Token: 0x06001B17 RID: 6935 RVA: 0x003D9B88 File Offset: 0x003D9B88
    [NullableContext(1)]
    public McpOAuthException(McpOAuthFailure failure, string message)
      : base(message)
    {
    }

    // Token: 0x170005FE RID: 1534
    // (get) Token: 0x06001B18 RID: 6936 RVA: 0x003D9B98 File Offset: 0x003D9B98
    public McpOAuthFailure Failure { get; } = failure;
  }
}
