using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200022E RID: 558
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkStreakBoxes
  {
    // Token: 0x060019A7 RID: 6567 RVA: 0x005D5C04 File Offset: 0x005D5C04
    public static FrameworkElement Create(IReadOnlyList<ClickyInkStreakDay> days, int? previousCount = null, double progress = 1.0, double appear = 1.0, double box = 14.0, int maximumPerRow = 14, [Nullable(2)] ClickyInkStyle style = null, int seed = 1)
    {
      ClickyInkStyle clickyInkStyle = style ?? new ClickyInkStyle(ClickyInkDashboardPalette.Pencil, ClickyInkDashboardPalette.Pencil.Opacity(0.58), null, null);
      int num = Math.Min(maximumPerRow, Math.Max(1, days.Count));
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 4.0,
        HorizontalAlignment = HorizontalAlignment.Left
      };
      for (int i = 0; i < days.Count; i += num)
      {
        StackPanel stackPanel2 = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 3.0
        };
        int num2 = i / num;
        int num3 = 0;
        while (num3 < num && i + num3 < days.Count)
        {
          stackPanel2.Children.Add(ClickyInkStreakBoxes.Cell(days[i + num3], num2 * num + num3, days.Count, previousCount, progress, appear, box, clickyInkStyle, seed));
          num3++;
        }
        stackPanel.Children.Add(stackPanel2);
      }
      return stackPanel;
    }

    // Token: 0x060019A8 RID: 6568 RVA: 0x005D5D14 File Offset: 0x005D5D14
    private static FrameworkElement Cell(ClickyInkStreakDay day, int index, int dayCount, int? previousCount, double progress, double appear, double box, ClickyInkStyle style, int seed)
    {
      bool flag;
      if (previousCount != null)
      {
        int valueOrDefault = previousCount.GetValueOrDefault();
        flag = index >= valueOrDefault;
      }
      else
      {
        flag = false;
      }
      bool flag2 = flag;
      bool flag3 = (double)index / (double)Math.Max(1, dayCount) <= appear;
      double num = (flag2 ? progress : 1.0);
      Grid grid = new Grid
      {
        Width = box,
        Height = box,
        IsHitTestVisible = false
      };
      switch (day)
      {
      case ClickyInkStreakDay.Done:
      {
        FrameworkElement frameworkElement = ClickyInkHatch.Create(flag2 ? style.Fresh : ClickyPaperInk.DoneGreen, num, 2.0, 1.2, seed + index, 0.0, new double?(Math.Max(0.0, box - 3.0)), new double?(Math.Max(0.0, box - 3.0)), null);
        frameworkElement.HorizontalAlignment = HorizontalAlignment.Center;
        frameworkElement.VerticalAlignment = VerticalAlignment.Center;
        grid.Children.Add(frameworkElement);
        break;
      }
      case ClickyInkStreakDay.Missed:
        grid.Children.Add(new Ellipse
        {
          Width = 2.5,
          Height = 2.5,
          Fill = Ui.Brush(style.Quiet.Opacity(0.45))
        });
        break;
      case ClickyInkStreakDay.Rest:
        grid.Children.Add(new PaperDrawing().Stroke(new PaperPath().MoveTo(3.0, box - 3.0).LineTo(box - 3.0, 3.0), new PaperStroke(style.Quiet.Opacity(0.7), 1.0, PenLineCap.Flat, PenLineJoin.Miter, null)));
        break;
      }
      grid.Children.Add(ClickyShapeView.Stroke(new ClickyInkBox(index + seed), ClickyInkDashboardPalette.Pencil.Opacity((day == ClickyInkStreakDay.Upcoming) ? 0.25 : 0.6), 1.0));
      if (day == ClickyInkStreakDay.Today)
      {
        ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyHandRing(index, 0.16), style.Fresh, 1.4);
        clickyShapeView.Margin = new Thickness(-3.0);
        grid.Children.Add(clickyShapeView);
      }
      grid.Opacity = flag3 > false;
      return grid;
    }
  }
}
