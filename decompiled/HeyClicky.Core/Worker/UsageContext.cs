using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000039 RID: 57
  [NullableContext(1)]
  [Nullable(0)]
  public static class UsageContext
  {
    // Token: 0x040001CC RID: 460
    public const string Companion = "companion";

    // Token: 0x040001CD RID: 461
    public const string AgentChat = "agent_chat";

    // Token: 0x040001CE RID: 462
    public const string ClickyCreation = "clicky_creation";

    // Token: 0x040001CF RID: 463
    public const string Onboarding = "onboarding";

    // Token: 0x040001D0 RID: 464
    public const string HeaderName = "X-Clicky-Usage-Context";
  }
}
