using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Browser
{
  // Token: 0x0200035F RID: 863
  [NullableContext(1)]
  public interface IChromeClickyDrive
  {
    // Token: 0x170006A5 RID: 1701
    // (get) Token: 0x0600257B RID: 9595
    bool IsAvailable { get; }

    // Token: 0x170006A6 RID: 1702
    // (get) Token: 0x0600257C RID: 9596
    bool IsDrivingNow { get; }

    // Token: 0x0600257D RID: 9597
    bool DriveForApp(string goal, [Nullable(2)] string targetProcessName, Action<ClickyDriveEnding> ended);

    // Token: 0x0600257E RID: 9598
    bool HandleEscape();

    // Token: 0x0600257F RID: 9599
    void SayVerbatim(string line);
  }
}
