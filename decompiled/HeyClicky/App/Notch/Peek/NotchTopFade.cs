using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000193 RID: 403
  public static class NotchTopFade
  {
    // Token: 0x060015E4 RID: 5604 RVA: 0x005B8BE4 File Offset: 0x005B8BE4
    [NullableContext(1)]
    public static Border Create(double bodyTopOffsetIntoRamp, double height = 52.0)
    {
      List<ValueTuple<double, Color>> list = new List<ValueTuple<double, Color>>();
      for (int i = 0; i <= 8; i++)
      {
        double num = (double)i / 8.0;
        double num2 = bodyTopOffsetIntoRamp + num * height;
        double num3 = Math.Min(1.0, num2 / 56.0) * 30.0 / 255.0;
        double num4 = 1.0 - num * num * (3.0 - 2.0 * num);
        list.Add(new ValueTuple<double, Color>(num, DS.Rgb(num3, num3, num3, num4)));
      }
      return new Border
      {
        Height = height,
        VerticalAlignment = VerticalAlignment.Top,
        IsHitTestVisible = false,
        Background = Ui.Vertical(list.ToArray())
      };
    }
  }
}
