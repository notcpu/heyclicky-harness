using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000233 RID: 563
  public sealed class ClickyInkTornTape : IClickyShape
  {
    // Token: 0x060019AF RID: 6575 RVA: 0x005D6C18 File Offset: 0x005D6C18
    public ClickyInkTornTape(double tooth = 3.5)
    {
    }

    // Token: 0x060019B0 RID: 6576 RVA: 0x005D6C28 File Offset: 0x005D6C28
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      PaperPath paperPath = new PaperPath();
      int num = Math.Max(3, (int)(rect.Height / 6.0));
      double x = rect.X;
      double y = rect.Y;
      double num2 = rect.X + rect.Width;
      double num3 = rect.Y + rect.Height;
      paperPath.MoveTo(x, y);
      paperPath.LineTo(num2, y);
      for (int i = 1; i <= num; i++)
      {
        paperPath.LineTo(num2 - ((i % 2 == 1) ? this.<tooth>P : 0.0), y + rect.Height * (double)i / (double)num);
      }
      paperPath.LineTo(x, num3);
      for (int j = num - 1; j >= 0; j--)
      {
        paperPath.LineTo(x + ((j % 2 == 1) ? this.<tooth>P : 0.0), y + rect.Height * (double)j / (double)num);
      }
      return paperPath.Close();
    }

    // Token: 0x04000C69 RID: 3177
    [CompilerGenerated]
    private double <tooth>P = tooth;
  }
}
