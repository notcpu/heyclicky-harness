using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Windows.UI.Composition;

namespace HeyClicky.App.Design
{
  // Token: 0x0200034D RID: 845
  [NullableContext(1)]
  [Nullable(0)]
  public static class SystemEasing
  {
    // Token: 0x060023FB RID: 9211 RVA: 0x00630850 File Offset: 0x00630850
    public static CubicBezierEasingFunction Expressive(Compositor c)
    {
      return c.CreateCubicBezierEasingFunction(new Vector2(0.23f, 1f), new Vector2(0.32f, 1f));
    }

    // Token: 0x060023FC RID: 9212 RVA: 0x00630878 File Offset: 0x00630878
    public static CubicBezierEasingFunction EaseInOut(Compositor c)
    {
      return c.CreateCubicBezierEasingFunction(new Vector2(0.42f, 0f), new Vector2(0.58f, 1f));
    }
  }
}
