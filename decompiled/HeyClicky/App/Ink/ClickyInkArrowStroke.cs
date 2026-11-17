using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000232 RID: 562
  public sealed class ClickyInkArrowStroke : IClickyShape
  {
    // Token: 0x060019AD RID: 6573 RVA: 0x005D6B18 File Offset: 0x005D6B18
    public ClickyInkArrowStroke(int seed = 1)
    {
    }

    // Token: 0x060019AE RID: 6574 RVA: 0x005D6B28 File Offset: 0x005D6B28
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      ClickyPaperRandom clickyPaperRandom = new ClickyPaperRandom((ulong)((long)(this.<seed>P + 3)));
      double num = clickyPaperRandom.Next(-1.2, 1.2);
      double num2 = rect.X + rect.Width / 2.0;
      double num3 = rect.Y + rect.Height / 2.0;
      double num4 = rect.X + rect.Width;
      return new PaperPath().MoveTo(rect.X, num3 + num).QuadTo(new Point(num4, num3), new Point(num2, num3 - 2.0 + num)).MoveTo(num4 - 5.0, num3 - 4.0)
        .LineTo(num4, num3)
        .LineTo(num4 - 5.0, num3 + 4.0);
    }

    // Token: 0x04000C68 RID: 3176
    [CompilerGenerated]
    private int <seed>P = seed;
  }
}
