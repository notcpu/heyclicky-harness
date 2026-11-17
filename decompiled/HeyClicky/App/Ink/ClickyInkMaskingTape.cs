using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200024D RID: 589
  public static class ClickyInkMaskingTape
  {
    // Token: 0x06001A1C RID: 6684 RVA: 0x005DD03C File Offset: 0x005DD03C
    [NullableContext(1)]
    public static FrameworkElement Make(double width = 58.0, double angle = -3.0, Color? color = null)
    {
      Color color2 = color ?? PaperColor.Hex("#EEE2C0");
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
      defaultInterpolatedStringHandler.AppendLiteral("tape|");
      defaultInterpolatedStringHandler.AppendFormatted<Color>(color2);
      InkPaperSheet inkPaperSheet = new InkPaperSheet(defaultInterpolatedStringHandler.ToStringAndClear(), new ClickyInkTornStrip(), PaperFill.Of(color2.Opacity(0.7)), new double?(0.3), new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.05), 0.6, 0.0, 0.4)
      }, null, PaperFill.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, PaperColor.White.Opacity(0.25)),
        new ValueTuple<double, Color>(1.0, PaperColor.White.Opacity(0.0))
      }));
      Grid grid = new Grid();
      grid.Width = width;
      grid.Height = 14.0;
      grid.IsHitTestVisible = false;
      grid.Children.Add(inkPaperSheet);
      grid.Transformed(0.0, 0.0, angle * 0.6, 1.0, null);
      AutomationProperties.SetAccessibilityView(grid, AccessibilityView.Raw);
      return grid;
    }
  }
}
