using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200015A RID: 346
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkReviewListRules
  {
    // Token: 0x06001514 RID: 5396 RVA: 0x003B32C8 File Offset: 0x003B32C8
    public static bool IsPathLike(string text)
    {
      string text2 = ClickyInkParser.Emphasis.Strip(text).TrimWs();
      if (!text2.HasPrefix("/") && !text2.HasPrefix("~/"))
      {
        return text2.Count<char>((char c) => c == '/') >= 2;
      }
      return true;
    }

    // Token: 0x06001515 RID: 5397 RVA: 0x003B3328 File Offset: 0x003B3328
    public static bool IsCompact(int itemCount)
    {
      return itemCount > 6;
    }

    // Token: 0x06001516 RID: 5398 RVA: 0x003B3330 File Offset: 0x003B3330
    public static string Count(int toSend, int open)
    {
      if (toSend != 0)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 2);
        defaultInterpolatedStringHandler.AppendFormatted<int>(toSend);
        defaultInterpolatedStringHandler.AppendLiteral(" of ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(open);
        defaultInterpolatedStringHandler.AppendLiteral(" ticked");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
      return "tick the ones to do";
    }
  }
}
