using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Services;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000155 RID: 341
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class IntegrationSuggestionSurface : NotchSurface
  {
    // Token: 0x060012F2 RID: 4850 RVA: 0x0059BC20 File Offset: 0x0059BC20
    public IntegrationSuggestionSurface(NotchLiveEvent.IntegrationSuggestion e, IReadOnlyList<string> useCases)
    {
      IntegrationSuggestionSurface <>4__this = this;
      Canvas canvas = new Canvas
      {
        Width = 58.0,
        Height = 38.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      FrameworkElement frameworkElement = IntegrationLogos.Image("heyclicky-logo", 24.0, null, 2.0);
      Ui.ClipRounded(frameworkElement, 5.5);
      Grid grid = CardParts.Tile(frameworkElement, 34.0);
      Canvas.SetLeft(grid, -10.0);
      Canvas.SetTop(grid, 2.0);
      Grid grid2 = CardParts.Tile(IntegrationSuggestionSurface.ToolkitBadge(e.Toolkit, 24.0, e.Title), 34.0);
      Canvas.SetLeft(grid2, 34.0);
      Canvas.SetTop(grid2, 2.0);
      canvas.Children.Add(grid);
      canvas.Children.Add(grid2);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 3.0,
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          Ui.Text("Connect " + e.Title + " to HeyClicky?", 12.5, FontWeights.SemiBold, DS.Colors.TextPrimary, TextAlignment.Left, 1, false, false),
          Ui.Text("Use HeyClicky to:", 10.5, FontWeights.Medium, DS.Colors.TextSecondary, TextAlignment.Left, 1, false, false)
        }
      };
      GelActionButton gelActionButton = IntegrationSuggestionSurface.<.ctor>g__Btn|1_0("No", "\ue711", 72.0, GelAppearance.SecondaryWhite, "Never ask again for this integration");
      GelActionButton gelActionButton2 = IntegrationSuggestionSurface.<.ctor>g__Btn|1_0("Not now", "\ue823", 98.0, GelAppearance.SecondaryWhite, "Not now");
      GelActionButton gelActionButton3 = IntegrationSuggestionSurface.<.ctor>g__Btn|1_0("Yes", "\ue71b", 72.0, GelAppearance.PrimaryBlue, "Yes");
      gelActionButton.Click += delegate
      {
        base.<.ctor>g__Answer|1(IntegrationAnswer.No);
      };
      gelActionButton2.Click += delegate
      {
        base.<.ctor>g__Answer|1(IntegrationAnswer.NotNow);
      };
      gelActionButton3.Click += delegate
      {
        base.<.ctor>g__Answer|1(IntegrationAnswer.Yes);
      };
      Grid grid3 = new Grid
      {
        ColumnSpacing = 20.0
      };
      grid3.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid3.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid3.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      StackPanel stackPanel2 = Ui.H(8.0, new UIElement[] { gelActionButton, gelActionButton2, gelActionButton3 });
      stackPanel2.Margin = new Thickness(30.0, 0.0, 0.0, 0.0);
      Grid.SetColumn(stackPanel, 1);
      Grid.SetColumn(stackPanel2, 2);
      grid3.Children.Add(canvas);
      grid3.Children.Add(stackPanel);
      grid3.Children.Add(stackPanel2);
      this.view = new StackPanel
      {
        Spacing = 8.0,
        Margin = new Thickness(16.0, (double)((useCases.Count == 0) ? 4 : 2), 16.0, 0.0),
        RenderTransform = new TranslateTransform
        {
          Y = (double)((useCases.Count == 0) ? 6 : 4)
        }
      };
      this.view.Children.Add(grid3);
      if (useCases.Count > 0)
      {
        this.view.Children.Add(new UseCaseCarousel(useCases.Take<string>(4).ToList<string>()));
      }
    }

    // Token: 0x1700036F RID: 879
    // (get) Token: 0x060012F3 RID: 4851 RVA: 0x0059C00C File Offset: 0x0059C00C
    public override VerticalAlignment ContentAlignment
    {
      get
      {
        if (!NotchGeometry.HasHardwareNotchEar)
        {
          return VerticalAlignment.Center;
        }
        return VerticalAlignment.Top;
      }
    }

    // Token: 0x060012F4 RID: 4852 RVA: 0x0059C018 File Offset: 0x0059C018
    public static FrameworkElement ToolkitBadge(string toolkit, double size, [Nullable(2)] string displayName = null)
    {
      return IntegrationLogos.Artwork(toolkit, displayName, size, 16.0, null);
    }

    // Token: 0x17000370 RID: 880
    // (get) Token: 0x060012F5 RID: 4853 RVA: 0x0059C02C File Offset: 0x0059C02C
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x060012F6 RID: 4854 RVA: 0x0059C034 File Offset: 0x0059C034
    [CompilerGenerated]
    internal static GelActionButton <.ctor>g__Btn|1_0(string t, string glyph, double min, GelAppearance a, string tip)
    {
      return new GelActionButton(t, glyph, a, 28.0, 10.0, min, 10.5, 10.0, 5.0, false).Tip(tip);
    }

    // Token: 0x040008B2 RID: 2226
    private readonly StackPanel view;
  }
}
