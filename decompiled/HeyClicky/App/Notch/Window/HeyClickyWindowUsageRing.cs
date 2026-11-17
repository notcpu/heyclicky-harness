using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using HeyClicky.Core.Billing;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Notch.Window
{
  // Token: 0x02000148 RID: 328
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class HeyClickyWindowUsageRing : Grid
  {
    // Token: 0x0600128F RID: 4751 RVA: 0x00597854 File Offset: 0x00597854
    public HeyClickyWindowUsageRing(BillingUsageRing usage, HeyClickyWindowPalette palette)
    {
      this.usage = usage;
      this.palette = palette;
      base.Padding = new Thickness(3.0);
      base.Background = Ui.Transparent;
      base.VerticalAlignment = VerticalAlignment.Center;
      base.Children.Add(HeyClickyWindowUsageRing.Glyph(usage.Progress, palette.InkAt(0.18), usage.IsSpent ? HeyClickyWindowUsageRing.SpentRed : palette.InkAt(0.9)));
      base.PointerEntered += delegate(object _, PointerRoutedEventArgs _)
      {
        this.ShowCard();
      };
      base.PointerExited += delegate(object _, PointerRoutedEventArgs e)
      {
        if (!PointerFrames.IsStale(e))
        {
          this.HideCard();
        }
      };
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        this.HideCard();
      };
      AutomationProperties.SetName(this, "Monthly usage");
      AutomationProperties.SetHelpText(this, usage.AccessibilityValue);
    }

    // Token: 0x06001290 RID: 4752 RVA: 0x0059792C File Offset: 0x0059792C
    private static FrameworkElement Glyph(double progress, Color track, Color tint)
    {
      Grid grid = new Grid
      {
        Width = 13.0,
        Height = 13.0
      };
      grid.Children.Add(new Ellipse
      {
        Stroke = Ui.Brush(track),
        StrokeThickness = 2.5
      });
      double num = Math.Clamp(Math.Max(0.03, progress), 0.0, 1.0);
      if (num >= 0.999)
      {
        grid.Children.Add(new Ellipse
        {
          Stroke = Ui.Brush(tint),
          StrokeThickness = 2.5
        });
        return grid;
      }
      double num2 = 5.25;
      double num3 = 6.5;
      double num4 = num * 2.0 * 3.141592653589793;
      PathFigure pathFigure = new PathFigure
      {
        StartPoint = new Point(num3, num3 - num2),
        IsClosed = false,
        IsFilled = false
      };
      PathSegmentCollection pathSegmentCollection = new PathSegmentCollection
      {
        new ArcSegment
        {
          Point = new Point(num3 + num2 * Math.Sin(num4), num3 - num2 * Math.Cos(num4)),
          Size = new Size(num2, num2),
          SweepDirection = SweepDirection.Clockwise,
          IsLargeArc = (num > 0.5)
        }
      };
      pathFigure.Segments = pathSegmentCollection;
      PathGeometry pathGeometry = new PathGeometry();
      pathGeometry.Figures.Add(pathFigure);
      grid.Children.Add(new Path
      {
        Data = pathGeometry,
        Stroke = Ui.Brush(tint),
        StrokeThickness = 2.5,
        StrokeStartLineCap = PenLineCap.Round,
        StrokeEndLineCap = PenLineCap.Round
      });
      return grid;
    }

    // Token: 0x06001291 RID: 4753 RVA: 0x00597AF0 File Offset: 0x00597AF0
    private void ShowCard()
    {
      if (base.XamlRoot == null)
      {
        return;
      }
      if (this.card == null)
      {
        this.card = HeyClickyWindowUsageRing.Card(this.usage, this.palette);
      }
      if (this.popup == null)
      {
        this.popup = new Popup
        {
          Child = this.card,
          IsHitTestVisible = false,
          XamlRoot = base.XamlRoot
        };
      }
      this.card.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      Point point = base.TransformToVisual(null).TransformPoint(new Point(0.0, base.ActualHeight));
      this.popup.HorizontalOffset = Math.Max(4.0, point.X - 40.0);
      this.popup.VerticalOffset = point.Y - 26.0 - this.card.DesiredSize.Height;
      this.popup.IsOpen = true;
      this.card.Opacity = 0.0;
      Anim.Opacity(this.card, 1f, 0.14, Easing.EaseOut(Ui.Compositor), 0.0);
    }

    // Token: 0x06001292 RID: 4754 RVA: 0x00597C40 File Offset: 0x00597C40
    private void HideCard()
    {
      Popup popup = this.popup;
      if (popup != null && popup.IsOpen)
      {
        this.popup.IsOpen = false;
      }
    }

    // Token: 0x06001293 RID: 4755 RVA: 0x00597C6C File Offset: 0x00597C6C
    private static FrameworkElement Card(BillingUsageRing usage, HeyClickyWindowPalette palette)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 10.0,
        Padding = new Thickness(12.0, 10.0, 12.0, 10.0)
      };
      stackPanel.Children.Add(HeyClickyWindowUsageRing.Section(usage.Agents, palette));
      stackPanel.Children.Add(new Border
      {
        Height = 1.0,
        Background = Ui.Brush(palette.InkAt(0.12))
      });
      stackPanel.Children.Add(HeyClickyWindowUsageRing.Section(usage.Voice, palette));
      string resetsLine = usage.ResetsLine;
      if (resetsLine != null)
      {
        stackPanel.Children.Add(Ui.Text(resetsLine, 10.5, FontWeights.Medium, palette.SecondaryText, TextAlignment.Left, 0, false, false));
      }
      Border border = new Border
      {
        CornerRadius = new CornerRadius(10.0),
        Background = Ui.Brush(palette.CardPlate),
        BorderBrush = Ui.Brush(palette.InkAt(0.08)),
        BorderThickness = new Thickness(1.0),
        Child = stackPanel
      };
      return new Grid
      {
        Children = 
        {
          new ShadowLayer(10.0, DS.Black(0.3), 10.0, 4.0, 0.0),
          border
        }
      };
    }

    // Token: 0x06001294 RID: 4756 RVA: 0x00597E0C File Offset: 0x00597E0C
    private static FrameworkElement Section(BillingUsageSection section, HeyClickyWindowPalette palette)
    {
      StackPanel stackPanel = new StackPanel();
      stackPanel.Spacing = 6.0;
      TextBlock textBlock = Ui.Text(section.Title, 9.5, FontWeights.Bold, palette.TertiaryText, TextAlignment.Left, 1, false, false);
      textBlock.CharacterSpacing = 50;
      stackPanel.Children.Add(textBlock);
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 4.0
      };
      stackPanel2.Children.Add(Ui.Text(section.CountText, 15.0, FontWeights.SemiBold, palette.PrimaryText, TextAlignment.Left, 1, false, false));
      string capText = section.CapText;
      if (capText != null)
      {
        TextBlock textBlock2 = Ui.Text(capText, 11.0, FontWeights.Medium, palette.SecondaryText, TextAlignment.Left, 1, false, false);
        textBlock2.VerticalAlignment = VerticalAlignment.Bottom;
        textBlock2.Margin = new Thickness(0.0, 0.0, 0.0, 2.0);
        stackPanel2.Children.Add(textBlock2);
      }
      stackPanel.Children.Add(stackPanel2);
      Color color = (section.IsSpent ? HeyClickyWindowUsageRing.SpentRed : palette.InkAt(0.9));
      Grid grid = new Grid
      {
        Width = 150.0,
        Height = 4.0,
        HorizontalAlignment = HorizontalAlignment.Left
      };
      grid.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(2.0),
        Background = Ui.Brush(palette.InkAt(0.14))
      });
      grid.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(2.0),
        Background = Ui.Brush(color),
        HorizontalAlignment = HorizontalAlignment.Left,
        Width = Math.Max(4.0, 150.0 * Math.Clamp(section.Progress, 0.0, 1.0))
      });
      stackPanel.Children.Add(grid);
      return stackPanel;
    }

    // Token: 0x04000888 RID: 2184
    private static readonly Color SpentRed = DS.Rgb(0.96, 0.3, 0.28, 1.0);

    // Token: 0x04000889 RID: 2185
    private const double Diameter = 13.0;

    // Token: 0x0400088A RID: 2186
    private const double Line = 2.5;

    // Token: 0x0400088B RID: 2187
    private readonly BillingUsageRing usage;

    // Token: 0x0400088C RID: 2188
    private readonly HeyClickyWindowPalette palette;

    // Token: 0x0400088D RID: 2189
    [Nullable(2)]
    private Popup popup;

    // Token: 0x0400088E RID: 2190
    [Nullable(2)]
    private FrameworkElement card;
  }
}
