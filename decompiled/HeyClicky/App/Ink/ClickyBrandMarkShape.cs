using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001C9 RID: 457
  public sealed class ClickyBrandMarkShape : IClickyShape
  {
    // Token: 0x060017C7 RID: 6087 RVA: 0x005C5CF4 File Offset: 0x005C5CF4
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      ClickyBrandMarkShape.<>c__DisplayClass2_0 CS$<>8__locals1;
      CS$<>8__locals1.rect = rect;
      CS$<>8__locals1.scale = Math.Min(CS$<>8__locals1.rect.Width / ClickyBrandMarkShape.CropSize.Width, CS$<>8__locals1.rect.Height / ClickyBrandMarkShape.CropSize.Height);
      return new PaperPath().MoveTo(ClickyBrandMarkShape.<PathIn>g__P|2_0(9.5, 12.4, ref CS$<>8__locals1)).LineTo(ClickyBrandMarkShape.<PathIn>g__P|2_0(14.5, 6.5, ref CS$<>8__locals1)).LineTo(ClickyBrandMarkShape.<PathIn>g__P|2_0(24.3, 23.3, ref CS$<>8__locals1))
        .MoveTo(ClickyBrandMarkShape.<PathIn>g__P|2_0(24.3, 23.3, ref CS$<>8__locals1))
        .LineTo(ClickyBrandMarkShape.<PathIn>g__P|2_0(33.4, 23.3, ref CS$<>8__locals1))
        .MoveTo(ClickyBrandMarkShape.<PathIn>g__P|2_0(27.1, 12.4, ref CS$<>8__locals1))
        .LineTo(ClickyBrandMarkShape.<PathIn>g__P|2_0(32.86, 6.5, ref CS$<>8__locals1))
        .LineTo(ClickyBrandMarkShape.<PathIn>g__P|2_0(38.4, 12.4, ref CS$<>8__locals1));
    }

    // Token: 0x060017CA RID: 6090 RVA: 0x005C5E7C File Offset: 0x005C5E7C
    [CompilerGenerated]
    internal static Point <PathIn>g__P|2_0(double x, double y, ref ClickyBrandMarkShape.<>c__DisplayClass2_0 A_2)
    {
      return new Point(A_2.rect.X + (x - ClickyBrandMarkShape.CropOrigin.X) * A_2.scale, A_2.rect.Y + (y - ClickyBrandMarkShape.CropOrigin.Y) * A_2.scale);
    }

    // Token: 0x04000B9D RID: 2973
    public static readonly Point CropOrigin = new Point(8.0, 4.5);

    // Token: 0x04000B9E RID: 2974
    public static readonly Size CropSize = new Size(32f, 21f);
  }
}
