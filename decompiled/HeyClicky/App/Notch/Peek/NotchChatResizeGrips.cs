using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000172 RID: 370
  public sealed class NotchChatResizeGrips : Grid
  {
    // Token: 0x0600148B RID: 5259 RVA: 0x005ACB68 File Offset: 0x005ACB68
    [NullableContext(1)]
    public NotchChatResizeGrips(INotchHost host)
    {
      Grid grid = new Grid
      {
        Margin = new Thickness(0.0, 60.0, 0.0, 18.0)
      };
      grid.Children.Add(new NotchChatResizeGrip(host, true, false, false)
      {
        Width = 7.0,
        HorizontalAlignment = HorizontalAlignment.Left
      });
      grid.Children.Add(new NotchChatResizeGrip(host, false, true, false)
      {
        Width = 7.0,
        HorizontalAlignment = HorizontalAlignment.Right
      });
      base.Children.Add(grid);
      Grid grid2 = new Grid
      {
        Height = 7.0,
        VerticalAlignment = VerticalAlignment.Bottom
      };
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(18.0)
      });
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(18.0)
      });
      NotchChatResizeGrip notchChatResizeGrip = new NotchChatResizeGrip(host, true, false, true);
      NotchChatResizeGrip notchChatResizeGrip2 = new NotchChatResizeGrip(host, false, false, true);
      NotchChatResizeGrip notchChatResizeGrip3 = new NotchChatResizeGrip(host, false, true, true);
      Grid.SetColumn(notchChatResizeGrip2, 1);
      Grid.SetColumn(notchChatResizeGrip3, 2);
      grid2.Children.Add(notchChatResizeGrip);
      grid2.Children.Add(notchChatResizeGrip2);
      grid2.Children.Add(notchChatResizeGrip3);
      base.Children.Add(grid2);
      Grid grid3 = new Grid
      {
        Height = 18.0,
        VerticalAlignment = VerticalAlignment.Bottom
      };
      grid3.Children.Add(new NotchChatResizeGrip(host, true, false, true)
      {
        Width = 7.0,
        HorizontalAlignment = HorizontalAlignment.Left
      });
      grid3.Children.Add(new NotchChatResizeGrip(host, false, true, true)
      {
        Width = 7.0,
        HorizontalAlignment = HorizontalAlignment.Right
      });
      base.Children.Add(grid3);
    }

    // Token: 0x040009DC RID: 2524
    private const double EdgeThickness = 7.0;

    // Token: 0x040009DD RID: 2525
    private const double CornerSize = 18.0;
  }
}
