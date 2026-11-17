using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000292 RID: 658
  public sealed class ClickyInkBox : IClickyShape
  {
    // Token: 0x06001B57 RID: 6999 RVA: 0x005EDD28 File Offset: 0x005EDD28
    public ClickyInkBox(int seed = 1)
    {
    }

    // Token: 0x06001B58 RID: 7000 RVA: 0x005EDD38 File Offset: 0x005EDD38
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      ClickyInkBox.<>c__DisplayClass2_0 CS$<>8__locals1;
      CS$<>8__locals1.generator = new ClickyPaperRandom((ulong)((long)(this.<seed>P + 5)));
      Point[] array = new Point[]
      {
        new Point(rect.X, rect.Y),
        new Point(rect.X + rect.Width, rect.Y),
        new Point(rect.X + rect.Width, rect.Y + rect.Height),
        new Point(rect.X, rect.Y + rect.Height)
      };
      PaperPath paperPath = new PaperPath();
      paperPath.MoveTo(array[0].X + ClickyInkBox.<PathIn>g__Jitter|2_0(ref CS$<>8__locals1), array[0].Y + ClickyInkBox.<PathIn>g__Jitter|2_0(ref CS$<>8__locals1));
      foreach (Point point in array.Skip<Point>(1).Append(array[0]))
      {
        paperPath.LineTo(point.X + ClickyInkBox.<PathIn>g__Jitter|2_0(ref CS$<>8__locals1), point.Y + ClickyInkBox.<PathIn>g__Jitter|2_0(ref CS$<>8__locals1));
      }
      return paperPath;
    }

    // Token: 0x06001B59 RID: 7001 RVA: 0x005EDE8C File Offset: 0x005EDE8C
    [CompilerGenerated]
    internal static double <PathIn>g__Jitter|2_0(ref ClickyInkBox.<>c__DisplayClass2_0 A_0)
    {
      return (A_0.generator.Next() % 100UL / 100.0 - 0.5) * 1.6;
    }

    // Token: 0x04000D18 RID: 3352
    [CompilerGenerated]
    private int <seed>P = seed;
  }
}
