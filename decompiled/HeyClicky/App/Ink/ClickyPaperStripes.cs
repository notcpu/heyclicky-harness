using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001D6 RID: 470
  public sealed class ClickyPaperStripes : IClickyShape
  {
    // Token: 0x060017E3 RID: 6115 RVA: 0x005C7AD4 File Offset: 0x005C7AD4
    public ClickyPaperStripes(double spacing = 10.0, double width = 5.0, double angleDegrees = 60.0)
    {
    }

    // Token: 0x060017E4 RID: 6116 RVA: 0x005C7AF4 File Offset: 0x005C7AF4
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      PaperPath paperPath = new PaperPath();
      double num = rect.Height / Math.Tan(this.<angleDegrees>P * 3.141592653589793 / 180.0);
      for (double num2 = -num - this.<spacing>P; num2 < rect.Right + this.<spacing>P; num2 += this.<spacing>P)
      {
        paperPath.MoveTo(num2, rect.Bottom).LineTo(num2 + num, rect.Top).LineTo(num2 + num + this.<width>P, rect.Top)
          .LineTo(num2 + this.<width>P, rect.Bottom)
          .Close();
      }
      return paperPath;
    }

    // Token: 0x04000BB2 RID: 2994
    [CompilerGenerated]
    private double <spacing>P = spacing;

    // Token: 0x04000BB3 RID: 2995
    [CompilerGenerated]
    private double <width>P = width;

    // Token: 0x04000BB4 RID: 2996
    [CompilerGenerated]
    private double <angleDegrees>P = angleDegrees;
  }
}
