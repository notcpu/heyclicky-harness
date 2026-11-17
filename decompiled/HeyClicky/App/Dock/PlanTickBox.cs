using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Notch.Surfaces;
using HeyClicky.App.UI;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200031D RID: 797
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PlanTickBox : Grid
  {
    // Token: 0x06002274 RID: 8820 RVA: 0x00623B3C File Offset: 0x00623B3C
    public PlanTickBox(bool isDone, bool isCurrent, double size, double cornerRadius, Color ink, bool todoIsCircle = true)
    {
      this.size = size;
      this.ink = ink;
      base.Height = size;
      base.Width = size;
      this.current = ActivityIndicators.ThinkingDots(ink.WithAlpha(0.6), 4.0, 3.0, 2.8, 0.85, 0.55, 0.45, 0.4, 0.6, 14.0);
      this.current.HorizontalAlignment = HorizontalAlignment.Center;
      double num = size * 0.95 / 18.0;
      this.current.RenderTransform = new ScaleTransform
      {
        ScaleX = num,
        ScaleY = num,
        CenterX = 9.0,
        CenterY = 7.0
      };
      FrameworkElement frameworkElement;
      if (!todoIsCircle)
      {
        Border border = new Border();
        border.Width = size;
        border.Height = size;
        border.CornerRadius = new CornerRadius(cornerRadius);
        border.BorderBrush = Ui.Brush(ink.WithAlpha(0.28));
        frameworkElement = border;
        border.BorderThickness = new Thickness(2.0);
      }
      else
      {
        Ellipse ellipse = new Ellipse();
        ellipse.Width = size * 0.8;
        ellipse.Height = size * 0.8;
        ellipse.Stroke = Ui.Brush(ink.WithAlpha(0.25));
        frameworkElement = ellipse;
        ellipse.StrokeThickness = 1.4;
      }
      this.todo = frameworkElement;
      this.todo.HorizontalAlignment = HorizontalAlignment.Center;
      this.todo.VerticalAlignment = VerticalAlignment.Center;
      this.doneBox = new Border
      {
        CornerRadius = new CornerRadius(cornerRadius),
        Background = Ui.Brush(ink.WithAlpha(0.3)),
        CenterPoint = new Vector3((float)size / 2f, (float)size / 2f, 0f)
      };
      double num2 = size * 0.24;
      double num3 = size - num2 * 2.0;
      this.checkGeometry = PlanTickBox.CheckGeometry(num2, num3);
      this.check = new Path
      {
        Data = this.checkGeometry,
        Stroke = Ui.Brush(Colors.White),
        StrokeThickness = Math.Max(1.6, size * 0.12),
        StrokeStartLineCap = PenLineCap.Round,
        StrokeEndLineCap = PenLineCap.Round,
        StrokeLineJoin = PenLineJoin.Round
      };
      base.Children.Add(this.current);
      base.Children.Add(this.todo);
      base.Children.Add(this.doneBox);
      base.Children.Add(this.check);
      this.Set(isDone, isCurrent, false);
    }

    // Token: 0x06002275 RID: 8821 RVA: 0x00623E20 File Offset: 0x00623E20
    private static PathGeometry CheckGeometry(double inset, double inner)
    {
      PathFigure pathFigure = new PathFigure
      {
        StartPoint = new Point(inset + inner * 0.08, inset + inner * 0.55),
        IsClosed = false
      };
      pathFigure.Segments.Add(new LineSegment
      {
        Point = new Point(inset + inner * 0.4, inset + inner * 0.85)
      });
      pathFigure.Segments.Add(new LineSegment
      {
        Point = new Point(inset + inner * 0.94, inset + inner * 0.18)
      });
      return new PathGeometry
      {
        Figures = { pathFigure }
      };
    }

    // Token: 0x06002276 RID: 8822 RVA: 0x00623EDC File Offset: 0x00623EDC
    public void Set(bool done, bool isCurrent, bool animated = true)
    {
      this.current.Visibility = ((isCurrent && !done) ? Visibility.Visible : Visibility.Collapsed);
      this.todo.Visibility = ((!isCurrent && !done) ? Visibility.Visible : Visibility.Collapsed);
      bool flag = done != this.isDone;
      this.isDone = done;
      if (!animated || !flag || Anim.ReduceMotion)
      {
        this.doneBox.Opacity = done > false;
        this.doneBox.Scale = (done ? Vector3.One : new Vector3(0.55f, 0.55f, 1f));
        this.check.Opacity = done > false;
        this.check.StrokeDashArray = null;
        return;
      }
      SwiftSpring swiftSpring = new SwiftSpring(0.28, 0.55);
      Anim.ScaleSpring(this.doneBox, done ? 1f : 0.55f, swiftSpring, null);
      Anim.OpacitySpring(this.doneBox, done > false, swiftSpring);
      if (!done)
      {
        this.check.Opacity = 0.0;
        return;
      }
      double length = this.size * 1.25;
      double thickness = this.check.StrokeThickness;
      this.check.StrokeDashArray = new DoubleCollection
      {
        length / thickness,
        length / thickness
      };
      this.check.StrokeDashOffset = length / thickness;
      this.check.Opacity = 1.0;
      DateTimeOffset started = DateTimeOffset.Now;
      DispatcherQueueTimer timer = DispatcherQueue.GetForCurrentThread().CreateTimer();
      timer.Interval = TimeSpan.FromMilliseconds(16L, 0L);
      timer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        double num = Math.Clamp(((DateTimeOffset.Now - started).TotalSeconds - 0.06) / 0.22, 0.0, 1.0);
        num = 1.0 - (1.0 - num) * (1.0 - num);
        this.check.StrokeDashOffset = length / thickness * (1.0 - num);
        if (num >= 1.0)
        {
          timer.Stop();
          this.check.StrokeDashArray = null;
        }
      };
      timer.Start();
    }

    // Token: 0x0400109F RID: 4255
    private readonly double size;

    // Token: 0x040010A0 RID: 4256
    private readonly Color ink;

    // Token: 0x040010A1 RID: 4257
    private readonly FrameworkElement current;

    // Token: 0x040010A2 RID: 4258
    private readonly FrameworkElement todo;

    // Token: 0x040010A3 RID: 4259
    private readonly Border doneBox;

    // Token: 0x040010A4 RID: 4260
    private readonly Path check;

    // Token: 0x040010A5 RID: 4261
    private readonly PathGeometry checkGeometry;

    // Token: 0x040010A6 RID: 4262
    private bool isDone;
  }
}
