using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Agents
{
  // Token: 0x02000011 RID: 17
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentPaywallException : Exception
  {
    // Token: 0x06000128 RID: 296 RVA: 0x000D9718 File Offset: 0x000D9718
    public AgentPaywallException(CompanionError error)
      : base(error.Message)
    {
    }

    // Token: 0x17000063 RID: 99
    // (get) Token: 0x06000129 RID: 297 RVA: 0x000D9730 File Offset: 0x000D9730
    public CompanionError Error { get; } = error;
  }
}
