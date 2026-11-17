using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Notch
{
  // Token: 0x0200011D RID: 285
  public static class TrayTooltip
  {
    // Token: 0x060012B9 RID: 4793 RVA: 0x003A318C File Offset: 0x003A318C
    [NullableContext(1)]
    public static string Text(bool isSignedIn, bool isPaused, IReadOnlyList<string> workingClickyNames)
    {
      List<string> list = new List<string> { "HeyClicky" };
      if (!isSignedIn)
      {
        list.Add("Signed out");
        return string.Join(" · ", list);
      }
      if (isPaused)
      {
        list.Add("Paused");
      }
      if (workingClickyNames.Count == 1)
      {
        list.Add(workingClickyNames[0] + " is working");
      }
      else if (workingClickyNames.Count > 1)
      {
        List<string> list2 = list;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(workingClickyNames.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" Clickys working");
        list2.Add(defaultInterpolatedStringHandler.ToStringAndClear());
      }
      return string.Join(" · ", list);
    }
  }
}
