using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Windows.UI;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000157 RID: 343
  public sealed class UseCaseCarousel : Grid
  {
    // Token: 0x060012FB RID: 4859 RVA: 0x0059C6B0 File Offset: 0x0059C6B0
    [NullableContext(1)]
    public UseCaseCarousel(IReadOnlyList<string> useCases)
    {
      UseCaseCarousel.<>c__DisplayClass0_0 CS$<>8__locals1 = new UseCaseCarousel.<>c__DisplayClass0_0();
      CS$<>8__locals1.useCases = useCases;
      base..ctor();
      CS$<>8__locals1.<>4__this = this;
      base.Height = 26.0;
      StackPanel strip = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 10.0
      };
      StackPanel first = CS$<>8__locals1.<.ctor>g__Copy|0();
      strip.Children.Add(first);
      Grid grid = new Grid
      {
        Children = { strip }
      };
      Ui.ClipRounded(grid, 0.0);
      Grid grid2 = new Grid
      {
        IsHitTestVisible = false,
        Children = { grid }
      };
      Grid grid3 = new Grid
      {
        IsHitTestVisible = false
      };
      base.Children.Add(grid2);
      base.Children.Add(grid3);
      Compositor compositor = Ui.Compositor;
      CompositionVisualSurface surface = compositor.CreateVisualSurface();
      surface.SourceVisual = ElementCompositionPreview.GetElementVisual(grid);
      ElementCompositionPreview.GetElementVisual(grid2).Clip = compositor.CreateInsetClip(0f, 0f, 100000f, 100000f);
      CompositionLinearGradientBrush compositionLinearGradientBrush = compositor.CreateLinearGradientBrush();
      compositionLinearGradientBrush.StartPoint = new Vector2(0f, 0.5f);
      compositionLinearGradientBrush.EndPoint = new Vector2(1f, 0.5f);
      foreach (ValueTuple<float, int> valueTuple in new ValueTuple<float, int>[]
      {
        new ValueTuple<float, int>(0f, 0),
        new ValueTuple<float, int>(0.055f, 255),
        new ValueTuple<float, int>(0.945f, 255),
        new ValueTuple<float, int>(1f, 0)
      })
      {
        float item = valueTuple.Item1;
        int item2 = valueTuple.Item2;
        compositionLinearGradientBrush.ColorStops.Add(compositor.CreateColorGradientStop(item, Color.FromArgb((byte)item2, byte.MaxValue, byte.MaxValue, byte.MaxValue)));
      }
      CompositionMaskBrush compositionMaskBrush = compositor.CreateMaskBrush();
      compositionMaskBrush.Source = compositor.CreateSurfaceBrush(surface);
      compositionMaskBrush.Mask = compositionLinearGradientBrush;
      SpriteVisual sprite = compositor.CreateSpriteVisual();
      sprite.Brush = compositionMaskBrush;
      ElementCompositionPreview.SetElementChildVisual(grid3, sprite);
      double laidOutSegment = -1.0;
      double laidOutWidth = -1.0;
      first.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        base.<.ctor>g__Layout|1();
      };
      base.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        base.<.ctor>g__Layout|1();
      };
    }
  }
}
