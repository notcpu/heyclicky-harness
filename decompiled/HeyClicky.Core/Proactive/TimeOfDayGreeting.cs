using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000C0 RID: 192
  public static class TimeOfDayGreeting
  {
    // Token: 0x06000CA7 RID: 3239 RVA: 0x0037F90C File Offset: 0x0037F90C
    [NullableContext(1)]
    public static string For(DateTime local)
    {
      int hour = local.Hour;
      if (hour < 17)
      {
        if (hour >= 5)
        {
          if (hour >= 12)
          {
            return "Good afternoon";
          }
          return "Good morning";
        }
      }
      else if (hour < 22)
      {
        return "Good evening";
      }
      return "Hey";
    }
  }
}
