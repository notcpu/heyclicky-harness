using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HeyClicky.Core.Tools
{
  // Token: 0x0200009A RID: 154
  public static class AgentToolHooks
  {
    // Token: 0x060009AB RID: 2475 RVA: 0x0036E548 File Offset: 0x0036E548
    [NullableContext(1)]
    public static bool BrainAskedAboutChrome(IEnumerable<string> spokenLines)
    {
      return spokenLines.Any<string>((string line) => line.Contains("chrome", StringComparison.CurrentCultureIgnoreCase));
    }

    // Token: 0x04000465 RID: 1125
    [Nullable(2)]
    public static volatile Func<bool> IsTourInterviewFoundingOnly;

    // Token: 0x04000466 RID: 1126
    [Nullable(new byte[] { 2, 2, 1, 1 })]
    public static volatile Func<string, Task<string>> SaveDrawing;

    // Token: 0x04000467 RID: 1127
    [Nullable(new byte[] { 2, 1, 1 })]
    public static volatile Func<string, string> ShowResult;

    // Token: 0x04000468 RID: 1128
    [Nullable(new byte[] { 2, 1, 1, 1 })]
    public static volatile Func<string, Task<string>> Routines;

    // Token: 0x04000469 RID: 1129
    [Nullable(new byte[] { 2, 1, 2 })]
    public static volatile Func<AgentToolLaunch, string> HoldJobForChrome;

    // Token: 0x0400046A RID: 1130
    [Nullable(new byte[] { 2, 1, 1, 1, 2 })]
    public static volatile Func<string, string, Task<string>> StartWaitingJobNamed;

    // Token: 0x0400046B RID: 1131
    [Nullable(new byte[] { 2, 2, 1, 1, 2 })]
    public static volatile Func<string, string, Task<string>> AnswerChromeSetup;

    // Token: 0x0400046C RID: 1132
    [Nullable(new byte[] { 2, 1 })]
    public static volatile Func<string> ChromeSetupBrowserName;

    // Token: 0x0400046D RID: 1133
    [Nullable(new byte[] { 2, 1, 1 })]
    public static volatile Func<string, string> JobIdForThread;

    // Token: 0x0400046E RID: 1134
    public static volatile bool JobsEnabled = true;
  }
}
