using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000268 RID: 616
  public static class ClickyInkBarcode
  {
    // Token: 0x06001A9E RID: 6814 RVA: 0x005E4DC8 File Offset: 0x005E4DC8
    [NullableContext(1)]
    public static FrameworkElement Make(int seed, double width, double height, Color color)
    {
      ClickyPaperRandom clickyPaperRandom = new ClickyPaperRandom((ulong)((long)(seed + 31)));
      PaperPath paperPath = new PaperPath();
      double num2;
      for (double num = 0.0; num < width; num += num2 + clickyPaperRandom.Next() % 3UL + 1.0)
      {
        num2 = clickyPaperRandom.Next() % 3UL + 1UL;
        paperPath.AddRect(new Rect(num, 0.0, num2, height));
      }
      PaperDrawing paperDrawing = new PaperDrawing().Fill(paperPath, color).Sized(width, height);
      paperDrawing.Clip = new RectangleGeometry
      {
        Rect = new Rect(0.0, 0.0, width, height)
      };
      return paperDrawing;
    }
  }
}
