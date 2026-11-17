using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001D4 RID: 468
  public sealed class ClickyPaperStatusTag : Grid
  {
    // Token: 0x060017E1 RID: 6113 RVA: 0x005C7774 File Offset: 0x005C7774
    [NullableContext(1)]
    public ClickyPaperStatusTag(ClickyThingStatus status, string text, double height = 22.0, double fontSize = 11.0)
    {
      base.Height = height;
      base.HorizontalAlignment = HorizontalAlignment.Left;
      base.IsHitTestVisible = false;
      bool flag = status - ClickyThingStatus.Done <= 1;
      bool flag2 = flag;
      Color color;
      switch (status)
      {
      case ClickyThingStatus.NeedsYou:
        color = ClickyPaperGel.Butter.Words();
        break;
      case ClickyThingStatus.Working:
      case ClickyThingStatus.Watching:
        color = ClickyPaperGel.Blue.Words();
        break;
      case ClickyThingStatus.Ready:
        color = ClickyPaperGel.Mint.Words();
        break;
      default:
        color = PaperColor.Hex("#C9CBD3");
        break;
      }
      Color color2 = color;
      switch (status)
      {
      case ClickyThingStatus.NeedsYou:
        base.Children.Add(new ClickyPaperGelPlate(ClickyPaperGel.Butter, true));
        break;
      case ClickyThingStatus.Working:
        base.Children.Add(new ClickyPaperGelPlate(ClickyPaperGel.Blue, true));
        base.Children.Add(new PaperSurface(ClickyShape.Capsule, PaperFill.Of(PaperColor.Clear), null).WithWash(PaperFill.Of(PaperColor.White.Opacity(0.45)), (Size s) => new ClickyPaperStripes(10.0, 5.0, 60.0).PathIn(new Rect(0.0, 0.0, s.Width, s.Height)), true).WithOutline(new PaperStroke(ClickyPaperGel.Blue.Rim(), 1.0, PenLineCap.Flat, PenLineJoin.Miter, null), false));
        break;
      case ClickyThingStatus.Watching:
        base.Children.Add(new ClickyPaperGelPlate(ClickyPaperGel.Blue, true));
        break;
      case ClickyThingStatus.Ready:
        base.Children.Add(new ClickyPaperGelPlate(ClickyPaperGel.Mint, true));
        break;
      default:
        base.Children.Add(new PaperSurface(ClickyShape.Capsule, PaperFill.Of(PaperColor.White.Opacity(0.1)), null).WithOutline(new PaperStroke(PaperColor.White.Opacity(0.18), 0.75, PenLineCap.Flat, PenLineJoin.Miter, null), false));
        break;
      }
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 6.0,
        Margin = new Thickness(10.0, 0.0, 10.0, 0.0),
        VerticalAlignment = VerticalAlignment.Center
      };
      if (status == ClickyThingStatus.Watching)
      {
        Grid grid = new Grid
        {
          Width = 6.0,
          Height = 6.0,
          VerticalAlignment = VerticalAlignment.Center
        };
        grid.Children.Add(new Ellipse
        {
          Fill = Ui.Brush(PaperColor.Hex("#1E5FE0"))
        });
        grid.Children.Add(new Ellipse
        {
          Stroke = Ui.Brush(PaperColor.Hex("#1E5FE0").Opacity(0.18)),
          StrokeThickness = 2.5,
          Margin = new Thickness(-1.25)
        });
        stackPanel.Children.Add(grid);
      }
      TextBlock textBlock = PaperText.Line(text, ClickyPaperFonts.Typed(fontSize, new FontWeight?(FontWeights.SemiBold)), color2, TextAlignment.Left);
      stackPanel.Children.Add(flag2 ? textBlock : Ui.Embossed(textBlock, 0.6, 1.0, 1.0));
      base.Children.Add(stackPanel);
    }
  }
}
