using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000226 RID: 550
  public static class ClickyInkLamp
  {
    // Token: 0x06001995 RID: 6549 RVA: 0x005D3C78 File Offset: 0x005D3C78
    [NullableContext(1)]
    public static FrameworkElement Create(ClickyInkStatusState state, ClickyInkStatusState? previous = null, double progress = 1.0, double diameter = 22.0, int seed = 1, double appear = 1.0)
    {
      Grid grid = new Grid
      {
        Width = diameter,
        Height = diameter,
        IsHitTestVisible = false
      };
      double num = Math.Max(2.0, diameter / 9.0);
      double num2 = Math.Max(1.1, diameter / 15.0);
      bool flag;
      if (previous != null)
      {
        ClickyInkStatusState valueOrDefault = previous.GetValueOrDefault();
        flag = valueOrDefault != state;
      }
      else
      {
        flag = false;
      }
      bool flag2 = flag;
      if (flag2 && progress < 1.0)
      {
        grid.Children.Add(ClickyInkHatch.Create(ClickyInkDashboardPalette.Lamp(previous.Value), 1.0, num, num2, seed + 3, 0.0, new double?(diameter), new double?(diameter), new double?(diameter * 0.08)));
      }
      grid.Children.Add(ClickyInkHatch.Create(ClickyInkDashboardPalette.Lamp(state), Math.Min(appear, flag2 ? progress : 1.0), num, num2, seed, 0.0, new double?(diameter), new double?(diameter), new double?(diameter * 0.08)));
      double num3 = diameter * 0.24;
      double num4 = (diameter - 2.0 * num3) / 2.0;
      PaperPath paperPath = new PaperPath();
      for (int i = 0; i <= 12; i++)
      {
        double num5 = (0.56 + 0.1399999999999999 * (double)i / 12.0) * 2.0 * 3.141592653589793;
        Point point = new Point(diameter / 2.0 + num4 * Math.Cos(num5), diameter / 2.0 + num4 * Math.Sin(num5));
        if (i == 0)
        {
          paperPath.MoveTo(point);
        }
        else
        {
          paperPath.LineTo(point);
        }
      }
      grid.Children.Add(new PaperDrawing().Stroke(paperPath, PaperStroke.Round(PaperColor.Hex("#FFFDF6").Opacity(0.85), Math.Max(1.4, diameter / 11.0))));
      grid.Children.Add(ClickyShapeView.Stroke(new ClickyHandRing(seed, 0.1).Trim(0.0, appear), new PaperStroke(ClickyInkDashboardPalette.Pencil.Opacity(0.75), Math.Max(1.1, diameter / 17.0), PenLineCap.Round, PenLineJoin.Miter, null)));
      return grid;
    }
  }
}
