using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001F0 RID: 496
  public sealed class ScreenshotFilesUnavailableException : Exception
  {
    // Token: 0x06001CEB RID: 7403 RVA: 0x003E5170 File Offset: 0x003E5170
    public ScreenshotFilesUnavailableException()
      : base("the Worker couldn't read a screenshot by reference")
    {
    }

    // Token: 0x04000C63 RID: 3171
    [Nullable(1)]
    public const string Code = "screenshot_files_unavailable";
  }
}
