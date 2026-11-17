using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Notch.Window
{
  // Token: 0x02000142 RID: 322
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class HeyClickyWindowComposerBacking : Grid
  {
    // Token: 0x06001245 RID: 4677 RVA: 0x00595B34 File Offset: 0x00595B34
    public HeyClickyWindowComposerBacking()
    {
      MessagesPalette messagesPalette = MessagesPalette.Current;
      base.IsHitTestVisible = false;
      base.VerticalAlignment = VerticalAlignment.Bottom;
      this.fade = new Border
      {
        Height = 38.0,
        VerticalAlignment = VerticalAlignment.Bottom,
        Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, messagesPalette.PaneFill.WithAlpha(0.0)),
          new ValueTuple<double, Color>(1.0, messagesPalette.PaneFill)
        })
      };
      this.fill = new Border
      {
        VerticalAlignment = VerticalAlignment.Bottom,
        Background = Ui.Brush(messagesPalette.PaneFill)
      };
      base.Children.Add(this.fade);
      base.Children.Add(this.fill);
      this.Layout(0.0);
    }

    // Token: 0x06001246 RID: 4678 RVA: 0x00595C28 File Offset: 0x00595C28
    public void Layout(double extraHeight)
    {
      double num = 54.0 + extraHeight;
      this.fill.Height = num;
      this.fade.Margin = new Thickness(0.0, 0.0, 0.0, num);
    }

    // Token: 0x0400084E RID: 2126
    private readonly Border fade;

    // Token: 0x0400084F RID: 2127
    private readonly Border fill;
  }
}
