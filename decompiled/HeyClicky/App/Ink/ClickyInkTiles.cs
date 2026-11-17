using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020002A5 RID: 677
  public static class ClickyInkTiles
  {
    // Token: 0x06001BD9 RID: 7129 RVA: 0x005F561C File Offset: 0x005F561C
    [NullableContext(1)]
    public static FrameworkElement Create(List<ClickyInkLiveNote.Block> blocks, double clock, ClickyInkStyle style, double width, double? tileHeight = null, ClickyInkTiles.Look look = ClickyInkTiles.Look.Ruled)
    {
      bool isWindowPreview = ClickyInkEnvironment.Current.IsWindowPreview;
      int num = ClickyInkDashboardViewMath.TileColumns(blocks.Count, width, isWindowPreview);
      double num2;
      if (blocks.Count != 0)
      {
        num2 = blocks.Min<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.LandsAt);
      }
      else
      {
        num2 = 0.0;
      }
      double num3 = num2;
      double num4 = ClickyInkClock.Progress(clock, num3, num3 + 0.55);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 0.0,
        Width = Math.Max(0.0, width),
        HorizontalAlignment = HorizontalAlignment.Left
      };
      int i = 0;
      int num5 = 0;
      while (i < blocks.Count)
      {
        List<ClickyInkLiveNote.Block> list = blocks.Skip<ClickyInkLiveNote.Block>(i).Take<ClickyInkLiveNote.Block>(num).ToList<ClickyInkLiveNote.Block>();
        double cellWidth = width / (double)list.Count;
        double num6 = tileHeight ?? list.Max<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => ClickyInkTile.Height(b.Source, new double?(cellWidth), isWindowPreview));
        StackPanel stackPanel2 = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 0.0
        };
        for (int j = 0; j < list.Count; j++)
        {
          FrameworkElement frameworkElement = ClickyInkTile.Create(list[j], clock, style, num5 * num + j, look, new double?(cellWidth));
          Grid grid = new Grid
          {
            Width = Math.Max(0.0, cellWidth),
            Height = Math.Max(0.0, num6),
            Children = { frameworkElement }
          };
          if (j > 0 && look != ClickyInkTiles.Look.Tape)
          {
            double num7 = ((look == ClickyInkTiles.Look.Ruled) ? (-0.04) : 0.14);
            ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyInkRuledLine(new Point(0.0, num7), new Point(0.0, 1.0 - num7), num5 * 7 + j).Trim(0.0, num4), new PaperStroke(ClickyInkDashboardPalette.Pencil.Opacity((look == ClickyInkTiles.Look.Ruled) ? 0.4 : 0.5), (look == ClickyInkTiles.Look.Ruled) ? 1.0 : 1.4, PenLineCap.Round, PenLineJoin.Miter, null));
            clickyShapeView.Width = 1.0;
            clickyShapeView.HorizontalAlignment = HorizontalAlignment.Left;
            grid.Children.Add(clickyShapeView);
          }
          stackPanel2.Children.Add(grid);
        }
        Grid grid2 = new Grid
        {
          Children = { stackPanel2 }
        };
        if (num5 > 0 && look == ClickyInkTiles.Look.Ruled)
        {
          ClickyShapeView clickyShapeView2 = ClickyShapeView.Stroke(new ClickyInkRuledLine(new Point(-0.01, 0.0), new Point(1.01, 0.0), num5).Trim(0.0, num4), new PaperStroke(ClickyInkDashboardPalette.Pencil.Opacity(0.4), 1.0, PenLineCap.Round, PenLineJoin.Miter, null));
          clickyShapeView2.Height = 1.0;
          clickyShapeView2.VerticalAlignment = VerticalAlignment.Top;
          grid2.Children.Add(clickyShapeView2);
        }
        stackPanel.Children.Add(grid2);
        i += num;
        num5++;
      }
      ClickyShapeView clickyShapeView3 = ClickyShapeView.Stroke(new ClickyInkRuledBox((blocks.Count > 0) ? blocks[0].Id.CharacterCount() : 1, 5.0).Trim(0.0, (look == ClickyInkTiles.Look.Ruled) ? num4 : 0.0), new PaperStroke(ClickyInkDashboardPalette.Pencil.Opacity(0.55), 1.15, PenLineCap.Round, PenLineJoin.Miter, null));
      return new ClickyInkOverlay(stackPanel, new UIElement[] { clickyShapeView3 })
      {
        HorizontalAlignment = HorizontalAlignment.Left,
        Margin = new Thickness(0.0, 4.0, 0.0, 4.0)
      };
    }

    // Token: 0x0200087E RID: 2174
    public enum Look
    {
      // Token: 0x040023DC RID: 9180
      Ruled,
      // Token: 0x040023DD RID: 9181
      Ink,
      // Token: 0x040023DE RID: 9182
      Tape
    }
  }
}
