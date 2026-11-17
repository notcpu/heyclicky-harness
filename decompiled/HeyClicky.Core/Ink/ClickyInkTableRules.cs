using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000171 RID: 369
  public static class ClickyInkTableRules
  {
    // Token: 0x06001687 RID: 5767 RVA: 0x003BF140 File Offset: 0x003BF140
    [NullableContext(1)]
    public static bool IsCrossingOut(ClickyInkLiveNote.Words words, double clock)
    {
      return words.Segments.Any<ClickyInkLiveNote.Segment>(delegate(ClickyInkLiveNote.Segment segment)
      {
        InkRange? struck = segment.Struck;
        if (struck != null)
        {
          InkRange valueOrDefault = struck.GetValueOrDefault();
          if (clock >= valueOrDefault.LowerBound)
          {
            double? vanishesAt = segment.VanishesAt;
            if (vanishesAt != null)
            {
              double valueOrDefault2 = vanishesAt.GetValueOrDefault();
              return clock < valueOrDefault2;
            }
            return true;
          }
        }
        return false;
      });
    }
  }
}
