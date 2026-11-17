using System;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000272 RID: 626
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkMiddleTruncation
  {
    // Token: 0x06001AC7 RID: 6855 RVA: 0x005E6C80 File Offset: 0x005E6C80
    public static string Fit(string text, PaperFont font, double width)
    {
      if (text.Length == 0 || ClickyInkMiddleTruncation.Width(text, font) <= width)
      {
        return text;
      }
      string[] array = text.Characters();
      int i = 0;
      int num = array.Length;
      string text2 = "…";
      while (i <= num)
      {
        int num2 = (i + num) / 2;
        int num3 = (num2 + 1) / 2;
        int num4 = num2 / 2;
        string text3 = string.Concat(array.Take<string>(num3)) + "…" + string.Concat(array.Skip<string>(array.Length - num4));
        if (ClickyInkMiddleTruncation.Width(text3, font) <= width)
        {
          text2 = text3;
          i = num2 + 1;
        }
        else
        {
          num = num2 - 1;
        }
      }
      return text2;
    }

    // Token: 0x06001AC8 RID: 6856 RVA: 0x005E6D14 File Offset: 0x005E6D14
    private static double Width(string text, PaperFont font)
    {
      double width;
      using (CanvasTextFormat canvasTextFormat = font.TextFormat())
      {
        using (CanvasTextLayout canvasTextLayout = new CanvasTextLayout(CanvasDevice.GetSharedDevice(), text, canvasTextFormat, 100000f, 100000f))
        {
          width = canvasTextLayout.LayoutBoundsIncludingTrailingWhitespace.Width;
        }
      }
      return width;
    }
  }
}
