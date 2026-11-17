using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000157 RID: 343
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkConnectRules
  {
    // Token: 0x06001503 RID: 5379 RVA: 0x003B2E78 File Offset: 0x003B2E78
    public static string Title([Nullable(2)] ClickyInkConnections.Phase phase, string appName, bool isAlreadyConnected, bool needsReconnect)
    {
      if (phase is ClickyInkConnections.Phase.Connected || isAlreadyConnected)
      {
        return appName + " is connected";
      }
      if (!needsReconnect)
      {
        return "Connect " + appName;
      }
      return "Reconnect " + appName;
    }

    // Token: 0x06001504 RID: 5380 RVA: 0x003B2EB0 File Offset: 0x003B2EB0
    public static string Label([Nullable(2)] ClickyInkConnections.Phase phase, bool isAlreadyConnected)
    {
      if (!(phase is ClickyInkConnections.Phase.Connected) && !isAlreadyConnected)
      {
        return "access needed";
      }
      return "access granted";
    }

    // Token: 0x06001505 RID: 5381 RVA: 0x003B2ECC File Offset: 0x003B2ECC
    [NullableContext(2)]
    public static bool ShowsConnectedStamp(ClickyInkConnections.Phase phase, bool isAlreadyConnected)
    {
      return phase is ClickyInkConnections.Phase.Connected || (phase == null && isAlreadyConnected);
    }
  }
}
