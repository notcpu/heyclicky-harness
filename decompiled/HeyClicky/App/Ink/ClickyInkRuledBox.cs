using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000224 RID: 548
  public sealed class ClickyInkRuledBox : IClickyShape
  {
    // Token: 0x06001990 RID: 6544 RVA: 0x005D39D0 File Offset: 0x005D39D0
    public ClickyInkRuledBox(int seed = 1, double overshoot = 4.0)
    {
    }

    // Token: 0x06001991 RID: 6545 RVA: 0x005D39E8 File Offset: 0x005D39E8
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      ClickyInkRuledBox.<>c__DisplayClass3_0 CS$<>8__locals1;
      CS$<>8__locals1.generator = new ClickyPaperRandom((ulong)((long)(this.<seed>P + 67)));
      double num = this.<overshoot>P;
      double x = rect.X;
      double y = rect.Y;
      double num2 = rect.X + rect.Width;
      double num3 = rect.Y + rect.Height;
      PaperPath paperPath = new PaperPath();
      paperPath.MoveTo(x - num, y + ClickyInkRuledBox.<PathIn>g__J|3_0(ref CS$<>8__locals1));
      paperPath.LineTo(num2 + num * 0.6, y + ClickyInkRuledBox.<PathIn>g__J|3_0(ref CS$<>8__locals1));
      double num4 = num2 + ClickyInkRuledBox.<PathIn>g__J|3_0(ref CS$<>8__locals1);
      paperPath.MoveTo(num4, y - num);
      paperPath.LineTo(num2 + ClickyInkRuledBox.<PathIn>g__J|3_0(ref CS$<>8__locals1), num3 + num * 0.7);
      paperPath.MoveTo(num2 + num, num3 + ClickyInkRuledBox.<PathIn>g__J|3_0(ref CS$<>8__locals1));
      paperPath.LineTo(x - num * 0.5, num3 + ClickyInkRuledBox.<PathIn>g__J|3_0(ref CS$<>8__locals1));
      num4 = x + ClickyInkRuledBox.<PathIn>g__J|3_0(ref CS$<>8__locals1);
      paperPath.MoveTo(num4, num3 + num);
      paperPath.LineTo(x + ClickyInkRuledBox.<PathIn>g__J|3_0(ref CS$<>8__locals1), y - num * 0.6);
      return paperPath;
    }

    // Token: 0x06001992 RID: 6546 RVA: 0x005D3B1C File Offset: 0x005D3B1C
    [CompilerGenerated]
    internal static double <PathIn>g__J|3_0(ref ClickyInkRuledBox.<>c__DisplayClass3_0 A_0)
    {
      return A_0.generator.Next(-0.8, 0.8);
    }

    // Token: 0x04000C61 RID: 3169
    [CompilerGenerated]
    private int <seed>P = seed;

    // Token: 0x04000C62 RID: 3170
    [CompilerGenerated]
    private double <overshoot>P = overshoot;
  }
}
