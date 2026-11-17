using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000220 RID: 544
  [NullableContext(1)]
  [Nullable(0)]
  internal static class InkDashboardLayout
  {
    // Token: 0x06001985 RID: 6533 RVA: 0x005D2DBC File Offset: 0x005D2DBC
    public static FrameworkElement Fixed(UIElement content)
    {
      return new Grid
      {
        Children = { content }
      };
    }

    // Token: 0x06001986 RID: 6534 RVA: 0x005D2DD0 File Offset: 0x005D2DD0
    public static StackPanel VStack(double spacing, [Nullable(new byte[] { 1, 2 })] params UIElement[] children)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = spacing,
        HorizontalAlignment = HorizontalAlignment.Left
      };
      foreach (UIElement uielement in children)
      {
        if (uielement != null)
        {
          stackPanel.Children.Add(uielement);
        }
      }
      return stackPanel;
    }

    // Token: 0x06001987 RID: 6535 RVA: 0x005D2E18 File Offset: 0x005D2E18
    public static ClickyInkHStack HStack(double spacing, InkVerticalAlignment alignment, [Nullable(new byte[] { 1, 2 })] params UIElement[] children)
    {
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(spacing, alignment, Array.Empty<UIElement>());
      foreach (UIElement uielement in children)
      {
        if (uielement != null)
        {
          clickyInkHStack.Children.Add(uielement);
        }
      }
      return clickyInkHStack;
    }

    // Token: 0x06001988 RID: 6536 RVA: 0x005D2E58 File Offset: 0x005D2E58
    public static T Width<[Nullable(0)] T>(T element, double width) where T : FrameworkElement
    {
      element.Width = Math.Max(0.0, width);
      element.HorizontalAlignment = HorizontalAlignment.Left;
      return element;
    }

    // Token: 0x06001989 RID: 6537 RVA: 0x005D2E84 File Offset: 0x005D2E84
    public static PaperPath RotatedRoundedRect(Rect rect, double radius, double degrees)
    {
      InkDashboardLayout.<>c__DisplayClass4_0 CS$<>8__locals1;
      CS$<>8__locals1.center = new Point(rect.X + rect.Width / 2.0, rect.Y + rect.Height / 2.0);
      double num = degrees * 3.141592653589793 / 180.0;
      CS$<>8__locals1.cos = Math.Cos(num);
      CS$<>8__locals1.sin = Math.Sin(num);
      double num2 = Math.Max(0.0, Math.Min(radius, Math.Min(rect.Width, rect.Height) / 2.0));
      List<Point> list = new List<Point>();
      foreach (ValueTuple<double, double, double> valueTuple in new ValueTuple<double, double, double>[]
      {
        new ValueTuple<double, double, double>(rect.X + num2, rect.Y + num2, 3.141592653589793),
        new ValueTuple<double, double, double>(rect.X + rect.Width - num2, rect.Y + num2, 4.71238898038469),
        new ValueTuple<double, double, double>(rect.X + rect.Width - num2, rect.Y + rect.Height - num2, 0.0),
        new ValueTuple<double, double, double>(rect.X + num2, rect.Y + rect.Height - num2, 1.5707963267948966)
      })
      {
        double item = valueTuple.Item1;
        double item2 = valueTuple.Item2;
        double item3 = valueTuple.Item3;
        for (int j = 0; j <= 6; j++)
        {
          double num3 = item3 + (double)j / 6.0 * 1.5707963267948966;
          list.Add(InkDashboardLayout.<RotatedRoundedRect>g__Turn|4_0(item + num2 * Math.Cos(num3), item2 + num2 * Math.Sin(num3), ref CS$<>8__locals1));
        }
      }
      return new PaperPath().AddLines(list).Close();
    }

    // Token: 0x0600198A RID: 6538 RVA: 0x005D3090 File Offset: 0x005D3090
    [CompilerGenerated]
    internal static Point <RotatedRoundedRect>g__Turn|4_0(double x, double y, ref InkDashboardLayout.<>c__DisplayClass4_0 A_2)
    {
      double num = x - A_2.center.X;
      double num2 = y - A_2.center.Y;
      return new Point(A_2.center.X + num * A_2.cos - num2 * A_2.sin, A_2.center.Y + num * A_2.sin + num2 * A_2.cos);
    }
  }
}
