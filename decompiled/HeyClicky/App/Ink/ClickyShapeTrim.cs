using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200028F RID: 655
  public static class ClickyShapeTrim
  {
    // Token: 0x06001B52 RID: 6994 RVA: 0x005EDB30 File Offset: 0x005EDB30
    [NullableContext(1)]
    public static IClickyShape Trim(this IClickyShape shape, double from, double to)
    {
      return new ClickyShape((Rect rect) => shape.PathIn(rect).Trimmed(from, to));
    }
  }
}
