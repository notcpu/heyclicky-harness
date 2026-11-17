using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001FE RID: 510
  public sealed class ClickySketchIconTile : Grid
  {
    // Token: 0x06001868 RID: 6248 RVA: 0x005CB690 File Offset: 0x005CB690
    [NullableContext(1)]
    public ClickySketchIconTile(Func<Color, UIElement> glyph, ClickySketchTone tone = ClickySketchTone.BluePale, long seed = 20L, double size = 26.0)
    {
      base.Height = size;
      base.Width = size;
      base.Children.Add(new ClickySketchShapeView(tone.Fill(), tone.Pencil(), 5.0, seed, 1.1, false, false));
      UIElement uielement = glyph(tone.Words());
      FrameworkElement frameworkElement = uielement as FrameworkElement;
      if (frameworkElement != null)
      {
        frameworkElement.HorizontalAlignment = HorizontalAlignment.Center;
        frameworkElement.VerticalAlignment = VerticalAlignment.Center;
      }
      base.Children.Add(uielement);
    }
  }
}
