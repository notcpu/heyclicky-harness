using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Agents.Turns
{
  // Token: 0x02000018 RID: 24
  public static class ChromeTurnEnd
  {
    // Token: 0x0600033C RID: 828 RVA: 0x000E4998 File Offset: 0x000E4998
    [NullableContext(2)]
    public static bool HelperStoppedForChrome(string finalResponseMarkdown, string finalSummary)
    {
      return string.Join("\n", new string[] { finalResponseMarkdown, finalSummary }.OfType<string>()).Contains("chrome", StringComparison.OrdinalIgnoreCase);
    }

    // Token: 0x0600033D RID: 829 RVA: 0x000E49C4 File Offset: 0x000E49C4
    public static bool AsksAtTurnEnd(bool foundChromeNotConnected, bool stoppedForChrome, bool wentWithoutChrome)
    {
      return foundChromeNotConnected && stoppedForChrome && !wentWithoutChrome;
    }
  }
}
