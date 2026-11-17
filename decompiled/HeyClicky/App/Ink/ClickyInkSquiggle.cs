using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000290 RID: 656
  public sealed class ClickyInkSquiggle : IClickyShape
  {
    // Token: 0x06001B53 RID: 6995 RVA: 0x005EDB5C File Offset: 0x005EDB5C
    public ClickyInkSquiggle(int seed = 1)
    {
    }

    // Token: 0x06001B54 RID: 6996 RVA: 0x005EDB6C File Offset: 0x005EDB6C
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      ClickyPaperRandom clickyPaperRandom = new ClickyPaperRandom((ulong)((long)(this.<seed>P + 11)));
      PaperPath paperPath = new PaperPath();
      double num = rect.Y + rect.Height / 2.0;
      paperPath.MoveTo(rect.X, num);
      for (int i = 1; i <= 14; i++)
      {
        double num2 = rect.X + rect.Width * (double)i / 14.0;
        double num3 = (clickyPaperRandom.Next() % 100UL / 100.0 - 0.5) * rect.Height * 0.9;
        paperPath.LineTo(num2, num + num3);
      }
      return paperPath;
    }

    // Token: 0x04000D16 RID: 3350
    [CompilerGenerated]
    private int <seed>P = seed;
  }
}
