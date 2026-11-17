using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Notch
{
  // Token: 0x0200011A RID: 282
  [NullableContext(1)]
  [Nullable(0)]
  public static class NotchUpdateKindNames
  {
    // Token: 0x06001288 RID: 4744 RVA: 0x003A2168 File Offset: 0x003A2168
    public static string RawValue(this NotchUpdateKind kind)
    {
      string text;
      if (kind != NotchUpdateKind.NeedsYou)
      {
        if (kind != NotchUpdateKind.Failed)
        {
          text = "done";
        }
        else
        {
          text = "failed";
        }
      }
      else
      {
        text = "needsYou";
      }
      return text;
    }

    // Token: 0x06001289 RID: 4745 RVA: 0x003A2198 File Offset: 0x003A2198
    public static NotchUpdateKind Parse(string raw)
    {
      NotchUpdateKind notchUpdateKind;
      if (!(raw == "needsYou"))
      {
        if (!(raw == "failed"))
        {
          notchUpdateKind = NotchUpdateKind.Done;
        }
        else
        {
          notchUpdateKind = NotchUpdateKind.Failed;
        }
      }
      else
      {
        notchUpdateKind = NotchUpdateKind.NeedsYou;
      }
      return notchUpdateKind;
    }

    // Token: 0x0600128A RID: 4746 RVA: 0x003A21CC File Offset: 0x003A21CC
    public static string AnalyticsName(this NotchUpdateKind kind)
    {
      string text;
      if (kind != NotchUpdateKind.NeedsYou)
      {
        if (kind != NotchUpdateKind.Failed)
        {
          text = "done";
        }
        else
        {
          text = "failed";
        }
      }
      else
      {
        text = "needs_you";
      }
      return text;
    }
  }
}
