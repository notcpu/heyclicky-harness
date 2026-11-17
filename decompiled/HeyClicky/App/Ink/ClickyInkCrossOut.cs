using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000291 RID: 657
  public sealed class ClickyInkCrossOut : IClickyShape
  {
    // Token: 0x06001B55 RID: 6997 RVA: 0x005EDC28 File Offset: 0x005EDC28
    public ClickyInkCrossOut(int seed = 1)
    {
    }

    // Token: 0x06001B56 RID: 6998 RVA: 0x005EDC38 File Offset: 0x005EDC38
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      ClickyPaperRandom clickyPaperRandom = new ClickyPaperRandom((ulong)((long)(this.<seed>P + 23)));
      double num = (clickyPaperRandom.Next() % 100UL / 100.0 - 0.5) * rect.Height * 0.2;
      return new PaperPath().MoveTo(rect.X, rect.Y + rect.Height - rect.Height * 0.25).QuadTo(new Point(rect.X + rect.Width, rect.Y + rect.Height * 0.25), new Point(rect.X + rect.Width / 2.0, rect.Y + rect.Height / 2.0 + num));
    }

    // Token: 0x04000D17 RID: 3351
    [CompilerGenerated]
    private int <seed>P = seed;
  }
}
