using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000075 RID: 117
  public static class FullscreenGameNote
  {
    // Token: 0x0600072B RID: 1835 RVA: 0x00362690 File Offset: 0x00362690
    [NullableContext(2)]
    public static string Text()
    {
      IntPtr intPtr = FullscreenGuard.GameWindow();
      if (intPtr == 0)
      {
        return null;
      }
      string text = FullscreenGuard.ProcessName(intPtr);
      return string.Concat(new string[] { "Screen: ", text, " is a game in exclusive fullscreen, so nothing you draw, point at or highlight can be seen over it (HeyClicky stays out of its way so it keeps fullscreen), and a circle the user draws doesn't show either. Your voice works and you still see the screen: answer by voice. If the user asks you to point, draw or circle something, or wonders why nothing shows, tell them once, in a short sentence, to switch ", text, "'s window mode to Borderless in its video settings so you can draw over it. Don't repeat it every turn." });
    }
  }
}
