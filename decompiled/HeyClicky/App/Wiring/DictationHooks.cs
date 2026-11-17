using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Wiring
{
  // Token: 0x02000099 RID: 153
  public static class DictationHooks
  {
    // Token: 0x0400015F RID: 351
    [Nullable(new byte[] { 2, 1 })]
    public static volatile Action<string> RemoveLearnedWord;

    // Token: 0x04000160 RID: 352
    [Nullable(2)]
    public static volatile Func<bool> TutorialAllowsPress;

    // Token: 0x04000161 RID: 353
    [Nullable(new byte[] { 2, 1 })]
    public static volatile Func<string, bool> ConsumeTutorialTranscript;
  }
}
