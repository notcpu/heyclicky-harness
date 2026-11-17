using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200024B RID: 587
  public sealed class ClickyInkTornStrip : IClickyShape
  {
    // Token: 0x06001A18 RID: 6680 RVA: 0x005DCEB0 File Offset: 0x005DCEB0
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      double num = rect.Height / 4.0;
      double x = rect.X;
      double num2 = rect.X + rect.Width;
      double y = rect.Y;
      double num3 = rect.Y + rect.Height;
      PaperPath paperPath = new PaperPath().MoveTo(x, y).LineTo(num2, y);
      for (int i = 0; i < 4; i++)
      {
        paperPath.LineTo(num2 - 2.5, y + num * ((double)i + 0.5));
        paperPath.LineTo(num2, y + num * (double)(i + 1));
      }
      paperPath.LineTo(x, num3);
      for (int j = 3; j >= 0; j--)
      {
        paperPath.LineTo(x + 2.5, y + num * ((double)j + 0.5));
        paperPath.LineTo(x, y + num * (double)j);
      }
      return paperPath.Close();
    }
  }
}
