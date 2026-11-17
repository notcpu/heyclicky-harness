using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001C3 RID: 451
  public sealed class ClickyHandRing : IClickyShape
  {
    // Token: 0x060017B5 RID: 6069 RVA: 0x005C4A40 File Offset: 0x005C4A40
    public ClickyHandRing(int seed = 1, double overshoot = 0.16)
    {
    }

    // Token: 0x060017B6 RID: 6070 RVA: 0x005C4A58 File Offset: 0x005C4A58
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      ClickyPaperRandom clickyPaperRandom = new ClickyPaperRandom((ulong)((long)(this.<seed>P + 7)));
      Point point = new Point(rect.X + rect.Width / 2.0, rect.Y + rect.Height / 2.0);
      double num = rect.Width / 2.0;
      double num2 = rect.Height / 2.0;
      double num3 = -0.35 + clickyPaperRandom.Next(-0.2, 0.2);
      List<Point> list = new List<Point>();
      for (int i = 0; i <= 40; i++)
      {
        double num4 = num3 + (double)i / 40.0 * 6.283185307179586 * (1.0 + this.<overshoot>P);
        double num5 = 1.0 + clickyPaperRandom.Next(-0.035, 0.035);
        list.Add(new Point(point.X + num * (Math.Cos(num4) * num5), point.Y + num2 * (Math.Sin(num4) * num5)));
      }
      PaperPath paperPath = new PaperPath().MoveTo(list[0]);
      for (int j = 1; j < list.Count - 1; j++)
      {
        paperPath.QuadTo(new Point((list[j].X + list[j + 1].X) / 2.0, (list[j].Y + list[j + 1].Y) / 2.0), list[j]);
      }
      PaperPath paperPath2 = paperPath;
      List<Point> list2 = list;
      return paperPath2.LineTo(list2[list2.Count - 1]);
    }

    // Token: 0x04000B88 RID: 2952
    [CompilerGenerated]
    private int <seed>P = seed;

    // Token: 0x04000B89 RID: 2953
    [CompilerGenerated]
    private double <overshoot>P = overshoot;
  }
}
