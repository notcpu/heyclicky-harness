using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Microsoft.UI.Composition;

namespace HeyClicky.App.Design
{
  // Token: 0x0200034E RID: 846
  [NullableContext(1)]
  [Nullable(0)]
  public static class Easing
  {
    // Token: 0x060023FD RID: 9213 RVA: 0x006308A0 File Offset: 0x006308A0
    public static CubicBezierEasingFunction Expressive(Compositor c)
    {
      return c.CreateCubicBezierEasingFunction(new Vector2(0.23f, 1f), new Vector2(0.32f, 1f));
    }

    // Token: 0x060023FE RID: 9214 RVA: 0x006308C8 File Offset: 0x006308C8
    public static CubicBezierEasingFunction EaseOut(Compositor c)
    {
      return c.CreateCubicBezierEasingFunction(new Vector2(0f, 0f), new Vector2(0.58f, 1f));
    }

    // Token: 0x060023FF RID: 9215 RVA: 0x006308F0 File Offset: 0x006308F0
    public static CubicBezierEasingFunction EaseInOut(Compositor c)
    {
      return c.CreateCubicBezierEasingFunction(new Vector2(0.42f, 0f), new Vector2(0.58f, 1f));
    }

    // Token: 0x06002400 RID: 9216 RVA: 0x00630918 File Offset: 0x00630918
    public static CubicBezierEasingFunction Drawer(Compositor c)
    {
      return c.CreateCubicBezierEasingFunction(new Vector2(0.32f, 0.72f), new Vector2(0f, 1f));
    }
  }
}
