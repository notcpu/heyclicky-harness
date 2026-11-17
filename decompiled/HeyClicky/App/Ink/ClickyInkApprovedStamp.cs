using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000216 RID: 534
  public static class ClickyInkApprovedStamp
  {
    // Token: 0x06001954 RID: 6484 RVA: 0x005D129C File Offset: 0x005D129C
    [NullableContext(1)]
    public static FrameworkElement Make(string words)
    {
      InkTextView inkTextView = InkTextView.Plain(words, ClickyPaperFonts.Rounded(10.5, FontWeights.ExtraBold), ClickyPaperInk.DoneGreen.Opacity(0.85)).With(delegate(InkTextView t)
      {
        t.Tracking = 1.2;
      });
      inkTextView.Margin = new Thickness(8.0, 3.0, 8.0, 3.0);
      ClickyShapeView clickyShapeView = ClickyShapeView.StrokeBorder(ClickyShape.RoundedRectangle(4.0), new PaperStroke(ClickyPaperInk.DoneGreen.Opacity(0.7), 1.6, PenLineCap.Flat, PenLineJoin.Miter, null));
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(inkTextView, new UIElement[] { clickyShapeView });
      clickyInkOverlay.Transformed(0.0, 0.0, -8.0, 1.0, null);
      return clickyInkOverlay;
    }
  }
}
