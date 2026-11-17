using System;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000C6 RID: 198
  public static class ProactiveLanePause
  {
    // Token: 0x06000D07 RID: 3335 RVA: 0x00381058 File Offset: 0x00381058
    public static bool IsLaneEnabled(bool storedPreference, bool isPaused = true)
    {
      return !isPaused && storedPreference;
    }

    // Token: 0x04000585 RID: 1413
    public const bool IsPausedForRelease = true;
  }
}
