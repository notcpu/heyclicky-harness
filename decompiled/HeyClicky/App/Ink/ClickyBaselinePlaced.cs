using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001D8 RID: 472
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyBaselinePlaced : Canvas
  {
    // Token: 0x060017E6 RID: 6118 RVA: 0x005C7CE4 File Offset: 0x005C7CE4
    public ClickyBaselinePlaced(UIElement content, double x, double y, double baseline)
    {
      base.IsHitTestVisible = false;
      Canvas.SetLeft(content, x);
      Canvas.SetTop(content, y - baseline);
      base.Children.Add(content);
    }

    // Token: 0x060017E7 RID: 6119 RVA: 0x005C7D10 File Offset: 0x005C7D10
    public static double TextBaseline(PaperFont font)
    {
      return ClickyHandLineBreaker.Ascent(font);
    }
  }
}
