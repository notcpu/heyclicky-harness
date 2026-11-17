using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200028E RID: 654
  public static class ClickyInkTag
  {
    // Token: 0x06001B51 RID: 6993 RVA: 0x005EDA70 File Offset: 0x005EDA70
    [NullableContext(1)]
    public static FrameworkElement Make(string words, Color color)
    {
      InkTextView inkTextView = InkTextView.Plain(words, ClickyPaperFonts.Hand(14.0), color);
      inkTextView.Margin = new Thickness(6.0, 0.5, 6.0, 0.5);
      ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(ClickyShape.Capsule, color.Opacity(0.55), 1.0);
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(inkTextView, new UIElement[] { clickyShapeView });
      clickyInkOverlay.Transformed(0.0, 0.0, -2.0, 1.0, null);
      clickyInkOverlay.HorizontalAlignment = HorizontalAlignment.Left;
      return clickyInkOverlay;
    }
  }
}
