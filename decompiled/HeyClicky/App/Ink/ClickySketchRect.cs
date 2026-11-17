using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001EB RID: 491
  public sealed class ClickySketchRect : IClickyShape
  {
    // Token: 0x0600183B RID: 6203 RVA: 0x005C9C58 File Offset: 0x005C9C58
    public ClickySketchRect(double radius = 3.0, long seed = 1L, double jitter = 0.7)
    {
    }

    // Token: 0x0600183C RID: 6204 RVA: 0x005C9C78 File Offset: 0x005C9C78
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      return ClickySketchPath.RoundedRect(rect, this.<radius>P, this.<seed>P, this.<jitter>P);
    }

    // Token: 0x04000BEE RID: 3054
    [CompilerGenerated]
    private double <radius>P = radius;

    // Token: 0x04000BEF RID: 3055
    [CompilerGenerated]
    private long <seed>P = seed;

    // Token: 0x04000BF0 RID: 3056
    [CompilerGenerated]
    private double <jitter>P = jitter;
  }
}
