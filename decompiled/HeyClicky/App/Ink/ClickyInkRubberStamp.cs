using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200024E RID: 590
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkRubberStamp
  {
    // Token: 0x06001A1D RID: 6685 RVA: 0x005DD1C0 File Offset: 0x005DD1C0
    public static FrameworkElement Make(string words, Color? color = null, double angle = -4.0)
    {
      Color color2 = color ?? PaperColor.Hex("#C2410C");
      InkTextView inkTextView = InkViews.Plain(words.ToUpperInvariant(), InkObjectFonts.Mono(9.5, new FontWeight?(FontWeights.ExtraBold)), color2.Opacity(0.72), delegate(InkTextView t)
      {
        t.Tracking = 1.4;
      });
      inkTextView.Margin = new Thickness(6.0, 2.0, 6.0, 2.0);
      ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(ClickyShape.RoundedRectangle(3.0), new PaperStroke(color2.Opacity(0.55), 1.2, PenLineCap.Flat, PenLineJoin.Miter, null));
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(inkTextView, new UIElement[] { clickyShapeView });
      clickyInkOverlay.HorizontalAlignment = HorizontalAlignment.Left;
      clickyInkOverlay.VerticalAlignment = VerticalAlignment.Top;
      clickyInkOverlay.Transformed(0.0, 0.0, angle * 0.6, 1.0, null);
      return clickyInkOverlay;
    }

    // Token: 0x06001A1E RID: 6686 RVA: 0x005DD2F4 File Offset: 0x005DD2F4
    public static InkTextView TypedLabel(string words, Color color)
    {
      return InkViews.Plain(words.ToUpperInvariant(), InkObjectFonts.Mono(9.5, new FontWeight?(FontWeights.Bold)), color, delegate(InkTextView t)
      {
        t.Tracking = 1.0;
      });
    }
  }
}
