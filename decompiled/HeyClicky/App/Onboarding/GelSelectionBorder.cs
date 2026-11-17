using System;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x02000123 RID: 291
  public sealed class GelSelectionBorder : Grid
  {
    // Token: 0x06001012 RID: 4114 RVA: 0x00581738 File Offset: 0x00581738
    public GelSelectionBorder(double radius)
    {
      base.IsHitTestVisible = false;
      base.Margin = new Thickness(-14.0);
      base.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(radius),
        BorderThickness = new Thickness(3.0),
        BorderBrush = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Hex(15594751U, 1.0)),
          new ValueTuple<double, Color>(0.18, DS.Hex(14084095U, 1.0)),
          new ValueTuple<double, Color>(0.5, DS.Hex(10668031U, 1.0)),
          new ValueTuple<double, Color>(0.8, DS.Hex(10079224U, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Hex(10670839U, 1.0))
        })
      });
      base.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(radius),
        BorderThickness = new Thickness(0.6),
        BorderBrush = Ui.Brush(DS.Hex(2234520U, 1.0))
      });
      base.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(radius - 2.4),
        Margin = new Thickness(2.4),
        BorderThickness = new Thickness(0.6),
        BorderBrush = Ui.Brush(DS.White(0.6))
      });
    }
  }
}
