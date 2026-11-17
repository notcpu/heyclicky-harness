using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000225 RID: 549
  public sealed class ClickyInkRuledLine : IClickyShape
  {
    // Token: 0x06001993 RID: 6547 RVA: 0x005D3B3C File Offset: 0x005D3B3C
    public ClickyInkRuledLine(Point from, Point to, int seed = 1)
    {
    }

    // Token: 0x06001994 RID: 6548 RVA: 0x005D3B5C File Offset: 0x005D3B5C
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      ClickyPaperRandom clickyPaperRandom = new ClickyPaperRandom((ulong)((long)(this.<seed>P + 89)));
      Point point = new Point(rect.X + rect.Width * this.<from>P.X, rect.Y + rect.Height * this.<from>P.Y);
      Point point2 = new Point(rect.X + rect.Width * this.<to>P.X, rect.Y + rect.Height * this.<to>P.Y);
      double num = (point.X + point2.X) / 2.0 + clickyPaperRandom.Next(-0.9, 0.9);
      double num2 = (point.Y + point2.Y) / 2.0 + clickyPaperRandom.Next(-0.9, 0.9);
      return new PaperPath().MoveTo(point).QuadTo(point2, new Point(num, num2));
    }

    // Token: 0x04000C63 RID: 3171
    [CompilerGenerated]
    private Point <from>P = from;

    // Token: 0x04000C64 RID: 3172
    [CompilerGenerated]
    private Point <to>P = to;

    // Token: 0x04000C65 RID: 3173
    [CompilerGenerated]
    private int <seed>P = seed;
  }
}
