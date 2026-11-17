using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000321 RID: 801
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BreathingText : Grid
  {
    // Token: 0x0600227F RID: 8831 RVA: 0x00624A64 File Offset: 0x00624A64
    public BreathingText(string text, double fontSize, Color restColor, Color lightColor, bool breathes)
    {
      this.breathes = breathes;
      base.IsHitTestVisible = false;
      this.rest = Ui.Text(text, fontSize, FontWeights.Medium, restColor, TextAlignment.Left, 0, false, false);
      this.light = Ui.Text(text, fontSize, FontWeights.Medium, lightColor, TextAlignment.Left, 0, false, false);
      this.light.Opacity = 0.0;
      base.Children.Add(this.rest);
      base.Children.Add(this.light);
      if (!breathes)
      {
        return;
      }
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        this.Build();
      };
      base.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        this.Rebuild();
      };
    }

    // Token: 0x06002280 RID: 8832 RVA: 0x00624B14 File Offset: 0x00624B14
    private void Build()
    {
      if (this.band != null)
      {
        return;
      }
      Compositor compositor = Ui.Compositor;
      CompositionBrush alphaMask = this.light.GetAlphaMask();
      this.gradient = compositor.CreateLinearGradientBrush();
      this.gradient.MappingMode = CompositionMappingMode.Absolute;
      CompositionMaskBrush compositionMaskBrush = compositor.CreateMaskBrush();
      compositionMaskBrush.Source = this.gradient;
      compositionMaskBrush.Mask = alphaMask;
      this.band = compositor.CreateSpriteVisual();
      this.band.Brush = compositionMaskBrush;
      ElementCompositionPreview.SetElementChildVisual(this, this.band);
      this.Rebuild();
    }

    // Token: 0x06002281 RID: 8833 RVA: 0x00624B98 File Offset: 0x00624B98
    private void Rebuild()
    {
      if (this.band == null || this.gradient == null)
      {
        return;
      }
      float num = (float)base.ActualWidth;
      float num2 = (float)base.ActualHeight;
      if (num <= 0f || num2 <= 0f)
      {
        return;
      }
      this.band.Size = new Vector2(num, num2);
      Compositor compositor = Ui.Compositor;
      Color color = ((SolidColorBrush)this.light.Foreground).Color;
      this.gradient.ColorStops.Clear();
      this.gradient.ColorStops.Add(compositor.CreateColorGradientStop(0f, Color.FromArgb(0, color.R, color.G, color.B)));
      this.gradient.ColorStops.Add(compositor.CreateColorGradientStop(0.5f, color));
      this.gradient.ColorStops.Add(compositor.CreateColorGradientStop(1f, Color.FromArgb(0, color.R, color.G, color.B)));
      this.gradient.StartPoint = new Vector2(-0.45f * num, 0f);
      this.gradient.EndPoint = new Vector2(0f, 0f);
      Vector2KeyFrameAnimation vector2KeyFrameAnimation = compositor.CreateVector2KeyFrameAnimation();
      vector2KeyFrameAnimation.InsertKeyFrame(0f, Vector2.Zero);
      vector2KeyFrameAnimation.InsertKeyFrame(1f, new Vector2(1.45f * num, 0f), Easing.EaseInOut(compositor));
      vector2KeyFrameAnimation.Duration = TimeSpan.FromSeconds(1.7);
      vector2KeyFrameAnimation.Direction = AnimationDirection.Alternate;
      vector2KeyFrameAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
      this.gradient.StartAnimation("Offset", vector2KeyFrameAnimation);
    }

    // Token: 0x040010AF RID: 4271
    private const double PassSeconds = 1.7;

    // Token: 0x040010B0 RID: 4272
    private readonly TextBlock rest;

    // Token: 0x040010B1 RID: 4273
    private readonly TextBlock light;

    // Token: 0x040010B2 RID: 4274
    private readonly bool breathes;

    // Token: 0x040010B3 RID: 4275
    [Nullable(2)]
    private SpriteVisual band;

    // Token: 0x040010B4 RID: 4276
    [Nullable(2)]
    private CompositionLinearGradientBrush gradient;
  }
}
