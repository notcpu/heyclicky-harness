using System;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001AD RID: 429
  public sealed class ClickyEdgeStackInkScreenPaper : Grid
  {
    // Token: 0x060016FE RID: 5886 RVA: 0x005BE7C0 File Offset: 0x005BE7C0
    public ClickyEdgeStackInkScreenPaper(ClickyHomeSpreadPaper paper, bool showsDots = true)
    {
      base.IsHitTestVisible = false;
      base.Children.Add(new PaperSurface(ClickyShape.Rectangle, paper.Fill, new double?(0.45)));
      if (showsDots)
      {
        base.Children.Add(new ClickyPaperBoardDots(null, false));
      }
    }
  }
}
