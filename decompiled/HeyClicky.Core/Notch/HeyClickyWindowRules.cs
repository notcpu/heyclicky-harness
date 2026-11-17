using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Notch
{
  // Token: 0x0200010B RID: 267
  public static class HeyClickyWindowRules
  {
    // Token: 0x060011A4 RID: 4516 RVA: 0x0039D67C File Offset: 0x0039D67C
    public static double BottomRoom(double composerHeight, double composerExtraHeight, bool isInWindow)
    {
      return composerHeight + Math.Max(0.0, composerExtraHeight) + (isInWindow ? 24.0 : 6.0);
    }

    // Token: 0x060011A5 RID: 4517 RVA: 0x0039D6A8 File Offset: 0x0039D6A8
    [NullableContext(1)]
    public static string AgentComposerPlaceholder([Nullable(2)] string name)
    {
      if (!string.IsNullOrWhiteSpace(name))
      {
        return "Message " + name.Trim() + "…";
      }
      return "Message it…";
    }

    // Token: 0x040007A4 RID: 1956
    public const double GapAboveComposer = 24.0;

    // Token: 0x040007A5 RID: 1957
    public const double NotchGapAboveComposer = 6.0;
  }
}
