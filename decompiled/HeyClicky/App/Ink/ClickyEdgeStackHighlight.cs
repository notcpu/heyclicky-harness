using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001B0 RID: 432
  public static class ClickyEdgeStackHighlight
  {
    // Token: 0x06001702 RID: 5890 RVA: 0x005BEC64 File Offset: 0x005BEC64
    [NullableContext(1)]
    public static FrameworkElement Wrap(FrameworkElement content, bool isOn, int seed)
    {
      if (!isOn)
      {
        return content;
      }
      Grid grid = new Grid();
      ClickyShapeView clickyShapeView = ClickyShapeView.Fill(new ClickyWobblyRect(seed, 1.6), ClickyEdgeStackInkLook.Highlighter);
      clickyShapeView.Margin = new Thickness(-4.0, -1.0, -4.0, -1.0);
      grid.Children.Add(clickyShapeView);
      grid.Children.Add(content);
      return grid;
    }
  }
}
