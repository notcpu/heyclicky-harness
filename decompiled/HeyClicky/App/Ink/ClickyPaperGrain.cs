using System;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001E5 RID: 485
  public sealed class ClickyPaperGrain : Grid
  {
    // Token: 0x0600182C RID: 6188 RVA: 0x005C9414 File Offset: 0x005C9414
    public ClickyPaperGrain(double opacity = 0.45)
    {
      base.IsHitTestVisible = false;
      CanvasControl canvasControl = new CanvasControl
      {
        IsHitTestVisible = false
      };
      canvasControl.Draw += delegate(CanvasControl s, CanvasDrawEventArgs e)
      {
        ClickyPaperTextures.DrawGrain(e.DrawingSession, new Rect(0.0, 0.0, s.ActualWidth, s.ActualHeight), opacity);
      };
      base.Children.Add(canvasControl);
    }
  }
}
