using System;

namespace HeyClicky.Core.Notch
{
  // Token: 0x0200010D RID: 269
  public static class ChatWorkCardRules
  {
    // Token: 0x060011A6 RID: 4518 RVA: 0x0039D6D0 File Offset: 0x0039D6D0
    public static ChatWorkCardMode Mode(bool isWorking, bool isFinalizing, bool hasAskOfTheUser, bool hidesFinalizing)
    {
      if (isWorking && (!hidesFinalizing || !isFinalizing))
      {
        return ChatWorkCardMode.Working;
      }
      if (!hasAskOfTheUser)
      {
        return ChatWorkCardMode.Hidden;
      }
      return ChatWorkCardMode.Ask;
    }
  }
}
