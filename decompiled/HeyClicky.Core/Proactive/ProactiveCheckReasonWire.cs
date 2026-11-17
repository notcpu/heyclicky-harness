using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000C8 RID: 200
  public static class ProactiveCheckReasonWire
  {
    // Token: 0x06000D08 RID: 3336 RVA: 0x00381060 File Offset: 0x00381060
    [NullableContext(1)]
    public static string Wire(this ProactiveCheckReason r)
    {
      string text;
      switch (r)
      {
      case ProactiveCheckReason.Morning:
        text = "morning";
        break;
      case ProactiveCheckReason.Debug:
        text = "debug";
        break;
      case ProactiveCheckReason.HomeSpace:
        text = "agent_space";
        break;
      case ProactiveCheckReason.Onboarding:
        text = "onboarding";
        break;
      default:
        text = "conversation";
        break;
      }
      return text;
    }
  }
}
