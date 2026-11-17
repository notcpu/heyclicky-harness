using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001C2 RID: 450
  public sealed class ClickyWobblyRect : IClickyShape
  {
    // Token: 0x060017B2 RID: 6066 RVA: 0x005C4914 File Offset: 0x005C4914
    public ClickyWobblyRect(int seed = 1, double wobble = 1.1)
    {
    }

    // Token: 0x060017B3 RID: 6067 RVA: 0x005C492C File Offset: 0x005C492C
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      ClickyWobblyRect.<>c__DisplayClass3_0 CS$<>8__locals1;
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.generator = new ClickyPaperRandom((ulong)((long)(this.<seed>P + 1)));
      PaperPath paperPath = new PaperPath();
      double num = rect.Left + this.<PathIn>g__Jitter|3_0(ref CS$<>8__locals1);
      double num2 = rect.Top + this.<PathIn>g__Jitter|3_0(ref CS$<>8__locals1);
      paperPath.MoveTo(num, num2);
      double num3 = rect.Right + this.<PathIn>g__Jitter|3_0(ref CS$<>8__locals1);
      double num4 = rect.Top + this.<PathIn>g__Jitter|3_0(ref CS$<>8__locals1);
      paperPath.LineTo(num3, num4);
      double num5 = rect.Right + this.<PathIn>g__Jitter|3_0(ref CS$<>8__locals1);
      double num6 = rect.Bottom + this.<PathIn>g__Jitter|3_0(ref CS$<>8__locals1);
      paperPath.LineTo(num5, num6);
      double num7 = rect.Left + this.<PathIn>g__Jitter|3_0(ref CS$<>8__locals1);
      double num8 = rect.Bottom + this.<PathIn>g__Jitter|3_0(ref CS$<>8__locals1);
      paperPath.LineTo(num7, num8);
      return paperPath.Close();
    }

    // Token: 0x060017B4 RID: 6068 RVA: 0x005C4A18 File Offset: 0x005C4A18
    [CompilerGenerated]
    private double <PathIn>g__Jitter|3_0(ref ClickyWobblyRect.<>c__DisplayClass3_0 A_1)
    {
      return A_1.generator.Next(-1.0, 1.0) * this.<wobble>P;
    }

    // Token: 0x04000B86 RID: 2950
    [CompilerGenerated]
    private int <seed>P = seed;

    // Token: 0x04000B87 RID: 2951
    [CompilerGenerated]
    private double <wobble>P = wobble;
  }
}
