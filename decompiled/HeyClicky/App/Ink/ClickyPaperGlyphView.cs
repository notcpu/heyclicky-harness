using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001C8 RID: 456
  public sealed class ClickyPaperGlyphView : Grid
  {
    // Token: 0x060017C6 RID: 6086 RVA: 0x005C5C7C File Offset: 0x005C5C7C
    public ClickyPaperGlyphView(ClickyPaperGlyph glyph, Color color, double size = 17.0, double lineWidth = 1.8)
    {
      base.Height = size;
      base.Width = size;
      base.IsHitTestVisible = false;
      double num = size / 24.0;
      base.Children.Add(new PaperDrawing().Stroke(glyph.Path().Transformed(num, num, 0.0, 0.0), new PaperStroke(color, lineWidth, PenLineCap.Round, PenLineJoin.Round, null)));
    }
  }
}
