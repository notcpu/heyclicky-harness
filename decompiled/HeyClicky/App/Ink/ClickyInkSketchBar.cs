using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200027E RID: 638
  public sealed class ClickyInkSketchBar : IClickyShape
  {
    // Token: 0x06001AE7 RID: 6887 RVA: 0x005E9900 File Offset: 0x005E9900
    public ClickyInkSketchBar(int seed)
    {
    }

    // Token: 0x06001AE8 RID: 6888 RVA: 0x005E9910 File Offset: 0x005E9910
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      ClickyInkSketchBar.<>c__DisplayClass2_0 CS$<>8__locals1;
      CS$<>8__locals1.generator = new ClickyPaperRandom((ulong)((long)(this.<seed>P + 17)));
      double x = rect.X;
      double y = rect.Y;
      double num = rect.X + rect.Width;
      double num2 = rect.Y + rect.Height;
      PaperPath paperPath = new PaperPath();
      double num3 = x + ClickyInkSketchBar.<PathIn>g__J|2_0(ref CS$<>8__locals1);
      paperPath.MoveTo(num3, y + ClickyInkSketchBar.<PathIn>g__J|2_0(ref CS$<>8__locals1));
      num3 = num + ClickyInkSketchBar.<PathIn>g__J|2_0(ref CS$<>8__locals1);
      paperPath.LineTo(num3, y + ClickyInkSketchBar.<PathIn>g__J|2_0(ref CS$<>8__locals1));
      num3 = num + ClickyInkSketchBar.<PathIn>g__J|2_0(ref CS$<>8__locals1);
      paperPath.LineTo(num3, num2 + ClickyInkSketchBar.<PathIn>g__J|2_0(ref CS$<>8__locals1));
      num3 = x + ClickyInkSketchBar.<PathIn>g__J|2_0(ref CS$<>8__locals1);
      paperPath.LineTo(num3, num2 + ClickyInkSketchBar.<PathIn>g__J|2_0(ref CS$<>8__locals1));
      return paperPath.Close();
    }

    // Token: 0x06001AE9 RID: 6889 RVA: 0x005E99E4 File Offset: 0x005E99E4
    [CompilerGenerated]
    internal static double <PathIn>g__J|2_0(ref ClickyInkSketchBar.<>c__DisplayClass2_0 A_0)
    {
      return (A_0.generator.Next() % 100UL / 100.0 - 0.5) * 1.6;
    }

    // Token: 0x04000CE1 RID: 3297
    [CompilerGenerated]
    private int <seed>P = seed;
  }
}
