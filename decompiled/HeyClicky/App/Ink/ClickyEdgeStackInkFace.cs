using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200020E RID: 526
  public sealed class ClickyEdgeStackInkFace : Grid
  {
    // Token: 0x1700047B RID: 1147
    // (get) Token: 0x06001939 RID: 6457 RVA: 0x005CF4C8 File Offset: 0x005CF4C8
    public static Size BoxSize
    {
      get
      {
        return new Size(ClickyEdgeStackInkFace.SheetSize.Width + ClickyEdgeStackInkFace.ShadowInsets.Left + ClickyEdgeStackInkFace.ShadowInsets.Right, ClickyEdgeStackInkFace.SheetSize.Height + ClickyEdgeStackInkFace.ShadowInsets.Top + ClickyEdgeStackInkFace.ShadowInsets.Bottom);
      }
    }

    // Token: 0x0600193A RID: 6458 RVA: 0x005CF52C File Offset: 0x005CF52C
    [NullableContext(1)]
    public ClickyEdgeStackInkFace(ClickyInkLiveNote note, ClickyEdgeStackInkReading reading, double clock, ClickyHomeSpreadPaper paper, bool isJob, [Nullable(2)] string liveStep = null, bool showsTape = false, bool fillsScreen = false)
    {
      ClickyInkStyle clickyInkStyle = ClickyEdgeStackInkLook.Style(note, paper, clock, reading);
      Grid grid = new Grid
      {
        Padding = new Thickness(18.0, (double)(isJob ? 18 : 16), 16.0, 13.0)
      };
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid.Children.Add(new ClickyEdgeStackInkHeader(reading, clock, clickyInkStyle, 22.0, 8.5));
      FrameworkElement frameworkElement = ClickyEdgeStackInkLead.Make(reading, clock, clickyInkStyle, false, true);
      if (frameworkElement != null)
      {
        frameworkElement.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
        Grid.SetRow(frameworkElement, 1);
        grid.Children.Add(frameworkElement);
      }
      Border border = new Border
      {
        Height = 8.0
      };
      Grid.SetRow(border, 2);
      border.VerticalAlignment = VerticalAlignment.Top;
      grid.Children.Add(border);
      grid.RowDefinitions[2].MinHeight = 8.0;
      FrameworkElement frameworkElement2 = ClickyEdgeStackInkFooter.Make(reading, liveStep, clickyInkStyle, 2, false);
      if (frameworkElement2 != null)
      {
        frameworkElement2.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
        Grid.SetRow(frameworkElement2, 3);
        grid.Children.Add(frameworkElement2);
      }
      Grid grid2 = new Grid();
      if (fillsScreen)
      {
        grid2.HorizontalAlignment = HorizontalAlignment.Stretch;
        grid2.VerticalAlignment = VerticalAlignment.Stretch;
      }
      else
      {
        grid2.Width = ClickyEdgeStackInkFace.SheetSize.Width;
        grid2.Height = ClickyEdgeStackInkFace.SheetSize.Height;
        grid2.HorizontalAlignment = HorizontalAlignment.Left;
        grid2.VerticalAlignment = VerticalAlignment.Top;
        grid2.Children.Add(new ClickyEdgeStackInkPaper(paper, isJob, false, 9.0));
      }
      grid2.Children.Add(grid);
      if (showsTape && reading.CurrentState.WantsAttention())
      {
        grid2.Children.Add(new ClickyEdgeStackInkTape(reading.CurrentState));
      }
      base.Children.Add(grid2);
      if (!fillsScreen)
      {
        base.Padding = ClickyEdgeStackInkFace.ShadowInsets;
        base.HorizontalAlignment = HorizontalAlignment.Left;
        base.VerticalAlignment = VerticalAlignment.Top;
      }
    }

    // Token: 0x04000C4B RID: 3147
    public static readonly Size SheetSize = new Size(330f, 210f);

    // Token: 0x04000C4C RID: 3148
    public static readonly Thickness ShadowInsets = new Thickness(6.0, 5.0, 6.0, 9.0);
  }
}
