using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x0200012D RID: 301
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SpokenBubble : Grid
  {
    // Token: 0x06001035 RID: 4149 RVA: 0x00582B3C File Offset: 0x00582B3C
    public SpokenBubble(string line)
    {
      base.HorizontalAlignment = HorizontalAlignment.Center;
      this.fill = new Path
      {
        Fill = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Hex(16771542U, 1.0)),
          new ValueTuple<double, Color>(0.42, DS.Hex(16763289U, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Hex(16771542U, 1.0))
        })
      };
      this.stroke = new Path
      {
        Fill = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Hex(10114328U, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Hex(6447714U, 1.0))
        })
      };
      Border border = new Border
      {
        Height = 8.0,
        Margin = new Thickness(16.0, 11.0, 16.0, 0.0),
        VerticalAlignment = VerticalAlignment.Top,
        CornerRadius = new CornerRadius(4.0),
        Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.White(0.85)),
          new ValueTuple<double, Color>(1.0, DS.White(0.02))
        }),
        IsHitTestVisible = false
      };
      Path path = Icons.PathIcon("M3,10 h2 v4 h-2 z M7,7 h2 v10 h-2 z M11,3 h2 v18 h-2 z M15,6 h2 v12 h-2 z M19,9 h2 v6 h-2 z", 11.0, Colors.Black);
      TextBlock textBlock = Ui.Text("“" + line + "”", 13.0, FontWeights.SemiBold, Colors.Black, TextAlignment.Center, 0, true, false);
      Grid grid = new Grid
      {
        ColumnSpacing = 7.0,
        Margin = new Thickness(17.0, 18.0, 17.0, 10.0),
        VerticalAlignment = VerticalAlignment.Center
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      path.VerticalAlignment = VerticalAlignment.Center;
      Grid grid2 = Ui.Embossed(textBlock, 0.8, 1.5, 1.0);
      Grid.SetColumn(grid2, 1);
      grid.Children.Add(path);
      grid.Children.Add(grid2);
      base.Children.Add(new ShadowLayer(16.0, DS.Black(0.11), 5.0, 4.0, 0.0)
      {
        Margin = new Thickness(0.0, 8.0, 0.0, 0.0)
      });
      base.Children.Add(this.stroke);
      base.Children.Add(this.fill);
      base.Children.Add(border);
      base.Children.Add(grid);
      this.fill.Margin = new Thickness(1.3, 1.3, 0.0, 0.0);
      base.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        if (base.ActualWidth < 32.0 || base.ActualHeight < 20.0)
        {
          return;
        }
        this.stroke.Data = SpokenBubble.Shape(base.ActualWidth, base.ActualHeight, 8.0);
        this.fill.Data = SpokenBubble.Shape(base.ActualWidth - 2.6, base.ActualHeight - 2.6, 7.22);
      };
    }

    // Token: 0x06001036 RID: 4150 RVA: 0x00582F30 File Offset: 0x00582F30
    private static Geometry Shape(double w, double h, double tailTop)
    {
      double num = h - tailTop;
      SpokenBubble.<>c__DisplayClass5_0 CS$<>8__locals1;
      CS$<>8__locals1.r = Math.Min(16.0, Math.Min(num / 2.0, w / 2.0));
      double num2 = w / 2.0;
      CS$<>8__locals1.f = new PathFigure
      {
        StartPoint = new Point(CS$<>8__locals1.r, tailTop),
        IsClosed = true,
        IsFilled = true
      };
      CS$<>8__locals1.f.Segments = new PathSegmentCollection();
      CS$<>8__locals1.last = new Point(num2 - 13.0, tailTop);
      SpokenBubble.<Shape>g__L|5_0(num2 - 13.0, tailTop, ref CS$<>8__locals1);
      SpokenBubble.<Shape>g__C|5_2(num2 - 6.5, tailTop, num2 - 4.6, 4.2, num2 - 2.4, 1.6, ref CS$<>8__locals1);
      SpokenBubble.<Shape>g__C|5_2(num2 - 2.4 + 1.5999999999999999, -0.1333333333333333, num2 + 2.4 - 1.5999999999999999, -0.1333333333333333, num2 + 2.4, 1.6, ref CS$<>8__locals1);
      SpokenBubble.<Shape>g__C|5_2(num2 + 4.6, 4.2, num2 + 6.5, tailTop, num2 + 13.0, tailTop, ref CS$<>8__locals1);
      SpokenBubble.<Shape>g__L|5_0(w - CS$<>8__locals1.r, tailTop, ref CS$<>8__locals1);
      SpokenBubble.<Shape>g__A|5_1(w, tailTop + CS$<>8__locals1.r, ref CS$<>8__locals1);
      SpokenBubble.<Shape>g__L|5_0(w, h - CS$<>8__locals1.r, ref CS$<>8__locals1);
      SpokenBubble.<Shape>g__A|5_1(w - CS$<>8__locals1.r, h, ref CS$<>8__locals1);
      SpokenBubble.<Shape>g__L|5_0(CS$<>8__locals1.r, h, ref CS$<>8__locals1);
      SpokenBubble.<Shape>g__A|5_1(0.0, h - CS$<>8__locals1.r, ref CS$<>8__locals1);
      SpokenBubble.<Shape>g__L|5_0(0.0, tailTop + CS$<>8__locals1.r, ref CS$<>8__locals1);
      SpokenBubble.<Shape>g__A|5_1(CS$<>8__locals1.r, tailTop, ref CS$<>8__locals1);
      return new PathGeometry
      {
        Figures = { CS$<>8__locals1.f }
      };
    }

    // Token: 0x06001038 RID: 4152 RVA: 0x005831E0 File Offset: 0x005831E0
    [CompilerGenerated]
    internal static void <Shape>g__L|5_0(double x, double y, ref SpokenBubble.<>c__DisplayClass5_0 A_2)
    {
      A_2.f.Segments.Add(new LineSegment
      {
        Point = new Point(x, y)
      });
    }

    // Token: 0x06001039 RID: 4153 RVA: 0x00583204 File Offset: 0x00583204
    [CompilerGenerated]
    internal static void <Shape>g__A|5_1(double x, double y, ref SpokenBubble.<>c__DisplayClass5_0 A_2)
    {
      A_2.f.Segments.Add(new ArcSegment
      {
        Point = new Point(x, y),
        Size = new Size(A_2.r, A_2.r),
        SweepDirection = SweepDirection.Clockwise
      });
    }

    // Token: 0x0600103A RID: 4154 RVA: 0x00583254 File Offset: 0x00583254
    [CompilerGenerated]
    internal static void <Shape>g__C|5_2(double x1, double y1, double x2, double y2, double x, double y, ref SpokenBubble.<>c__DisplayClass5_0 A_6)
    {
      Point last = A_6.last;
      for (int i = 1; i <= 10; i++)
      {
        double num = (double)i / 10.0;
        double num2 = 1.0 - num;
        double num3 = num2 * num2 * num2 * last.X + 3.0 * num2 * num2 * num * x1 + 3.0 * num2 * num * num * x2 + num * num * num * x;
        double num4 = num2 * num2 * num2 * last.Y + 3.0 * num2 * num2 * num * y1 + 3.0 * num2 * num * num * y2 + num * num * num * y;
        A_6.f.Segments.Add(new LineSegment
        {
          Point = new Point(num3, num4)
        });
      }
      A_6.last = new Point(x, y);
    }

    // Token: 0x0400070A RID: 1802
    private readonly Path fill;

    // Token: 0x0400070B RID: 1803
    private readonly Path stroke;

    // Token: 0x0400070C RID: 1804
    private const double Tail = 8.0;

    // Token: 0x0400070D RID: 1805
    private const double StrokeWidth = 1.3;
  }
}
