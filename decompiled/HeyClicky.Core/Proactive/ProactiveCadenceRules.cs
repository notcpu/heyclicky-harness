using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000D1 RID: 209
  [NullableContext(1)]
  [Nullable(0)]
  public static class ProactiveCadenceRules
  {
    // Token: 0x06000D86 RID: 3462 RVA: 0x00382770 File Offset: 0x00382770
    public static int[] Hours(this ProactiveCadence c)
    {
      int[] array;
      if (c != ProactiveCadence.ThreeTimes)
      {
        if (c != ProactiveCadence.FiveTimes)
        {
          array = new int[] { 6 };
        }
        else
        {
          array = new int[] { 6, 9, 12, 15, 18 };
        }
      }
      else
      {
        array = new int[] { 6, 12, 18 };
      }
      return array;
    }

    // Token: 0x06000D87 RID: 3463 RVA: 0x003827BC File Offset: 0x003827BC
    public static string Title(this ProactiveCadence c)
    {
      string text;
      if (c != ProactiveCadence.ThreeTimes)
      {
        if (c != ProactiveCadence.FiveTimes)
        {
          text = "Once a day";
        }
        else
        {
          text = "5 times a day";
        }
      }
      else
      {
        text = "3 times a day";
      }
      return text;
    }

    // Token: 0x06000D88 RID: 3464 RVA: 0x003827EC File Offset: 0x003827EC
    public static bool IsBeforeFirstMorning(DateTime onboardingLocal, DateTime nowLocal)
    {
      return nowLocal < onboardingLocal.Date.AddDays(1.0).AddHours(6.0);
    }

    // Token: 0x06000D89 RID: 3465 RVA: 0x00382828 File Offset: 0x00382828
    public static bool IsDue(this ProactiveCadence c, DateTime nowLocal, DateTime? lastSuccessLocal, DateTime? retryAfterLocal)
    {
      if (retryAfterLocal != null)
      {
        DateTime valueOrDefault = retryAfterLocal.GetValueOrDefault();
        if (nowLocal < valueOrDefault)
        {
          return false;
        }
      }
      DateTime? dateTime = null;
      foreach (int num in c.Hours())
      {
        DateTime dateTime2 = nowLocal.Date.AddHours((double)num);
        if (dateTime2 <= nowLocal)
        {
          dateTime = new DateTime?(dateTime2);
        }
      }
      return dateTime != null && (lastSuccessLocal == null || lastSuccessLocal.GetValueOrDefault() < dateTime);
    }
  }
}
