using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Drive
{
  // Token: 0x0200018F RID: 399
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyConsentReplyMarker
  {
    // Token: 0x060017AE RID: 6062 RVA: 0x003C4FC0 File Offset: 0x003C4FC0
    public static ClickyConsentReplyMarker.Kind? KindOf(string messageText)
    {
      string text = messageText.Trim();
      if (text.StartsWith("[Computer use approved for this turn]", StringComparison.Ordinal))
      {
        return new ClickyConsentReplyMarker.Kind?(ClickyConsentReplyMarker.Kind.Allowed);
      }
      if (text.StartsWith("[Clicky drove your screen for this step]", StringComparison.Ordinal))
      {
        return new ClickyConsentReplyMarker.Kind?(ClickyConsentReplyMarker.Kind.Drove);
      }
      if (text.StartsWith("Not now. Finish without controlling my PC", StringComparison.Ordinal))
      {
        return new ClickyConsentReplyMarker.Kind?(ClickyConsentReplyMarker.Kind.Declined);
      }
      return null;
    }

    // Token: 0x060017AF RID: 6063 RVA: 0x003C501C File Offset: 0x003C501C
    public static string PreviewLine(ClickyConsentReplyMarker.Kind kind)
    {
      string text;
      if (kind != ClickyConsentReplyMarker.Kind.Allowed)
      {
        if (kind != ClickyConsentReplyMarker.Kind.Declined)
        {
          text = "HeyClicky drove your PC for it";
        }
        else
        {
          text = "You said not now";
        }
      }
      else
      {
        text = "You allowed it to use your PC";
      }
      return text;
    }

    // Token: 0x04000A35 RID: 2613
    public const string ApprovedPrefix = "[Computer use approved for this turn]";

    // Token: 0x04000A36 RID: 2614
    public const string DeclinedPrefix = "Not now. Finish without controlling my PC";

    // Token: 0x04000A37 RID: 2615
    public const string DrovePrefix = "[Clicky drove your screen for this step]";

    // Token: 0x0200056D RID: 1389
    [NullableContext(0)]
    public enum Kind
    {
      // Token: 0x04002078 RID: 8312
      Allowed,
      // Token: 0x04002079 RID: 8313
      Declined,
      // Token: 0x0400207A RID: 8314
      Drove
    }
  }
}
