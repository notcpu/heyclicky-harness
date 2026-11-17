using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200014B RID: 331
  public static class ClickyInkFrontPageVariants
  {
    // Token: 0x060014BF RID: 5311 RVA: 0x003B00D8 File Offset: 0x003B00D8
    [NullableContext(1)]
    public static string RawValue(this ClickyInkFrontPageVariant variant)
    {
      if (variant != ClickyInkFrontPageVariant.Broadsheet)
      {
        return "missionControl";
      }
      return "broadsheet";
    }
  }
}
