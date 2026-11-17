using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000072 RID: 114
  [NullableContext(1)]
  [Nullable(0)]
  public static class DrawingCaptionTiming
  {
    // Token: 0x06000702 RID: 1794 RVA: 0x00360CA8 File Offset: 0x00360CA8
    public static double RevealSeconds(string line, double readingSeconds)
    {
      int num = line.Split(null, StringSplitOptions.RemoveEmptyEntries).Length;
      return Math.Min(readingSeconds * 0.8, Math.Max(0.6, (double)num * 0.24));
    }

    // Token: 0x06000703 RID: 1795 RVA: 0x00360CEC File Offset: 0x00360CEC
    public static double SpokenSeconds(string line, double speed)
    {
      return Math.Max(0.8, (double)line.Length / (14.5 * Math.Max(0.5, speed)));
    }
  }
}
