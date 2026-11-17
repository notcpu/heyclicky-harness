using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000322 RID: 802
  public sealed class ShimmerSweep : Grid
  {
    // Token: 0x06002284 RID: 8836 RVA: 0x00624D58 File Offset: 0x00624D58
    public ShimmerSweep(double peakWhiteOpacity = 0.08, double sweepSeconds = 1.2)
    {
      this.peak = peakWhiteOpacity;
      this.seconds = sweepSeconds;
      base.IsHitTestVisible = false;
      base.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        this.Rebuild();
      };
    }

    // Token: 0x06002285 RID: 8837 RVA: 0x00624D88 File Offset: 0x00624D88
    private void Rebuild()
    {
      float num = (float)base.ActualWidth;
      float num2 = (float)base.ActualHeight;
      if (num <= 0f || num2 <= 0f || Anim.ReduceMotion)
      {
        return;
      }
      Compositor compositor = Ui.Compositor;
      if (this.sprite == null)
      {
        CompositionLinearGradientBrush compositionLinearGradientBrush = compositor.CreateLinearGradientBrush();
        compositionLinearGradientBrush.StartPoint = new Vector2(0f, 0.5f);
        compositionLinearGradientBrush.EndPoint = new Vector2(1f, 0.5f);
        compositionLinearGradientBrush.ColorStops.Add(compositor.CreateColorGradientStop(0f, DS.White(0.0)));
        compositionLinearGradientBrush.ColorStops.Add(compositor.CreateColorGradientStop(0.5f, DS.White(this.peak)));
        compositionLinearGradientBrush.ColorStops.Add(compositor.CreateColorGradientStop(1f, DS.White(0.0)));
        this.sprite = compositor.CreateSpriteVisual();
        this.sprite.Brush = compositionLinearGradientBrush;
        ContainerVisual containerVisual = compositor.CreateContainerVisual();
        containerVisual.Clip = compositor.CreateInsetClip();
        containerVisual.Children.InsertAtTop(this.sprite);
        ElementCompositionPreview.SetElementChildVisual(this, containerVisual);
        ElementCompositionPreview.GetElementChildVisual(this).Size = new Vector2(num, num2);
      }
      Visual elementChildVisual = ElementCompositionPreview.GetElementChildVisual(this);
      if (elementChildVisual != null)
      {
        elementChildVisual.Size = new Vector2(num, num2);
      }
      float num3 = num * 1.5f;
      this.sprite.Size = new Vector2(num3, num2);
      float num4 = num / 2f - num3 / 2f;
      float num5 = num * 1.5f;
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = compositor.CreateVector3KeyFrameAnimation();
      vector3KeyFrameAnimation.InsertKeyFrame(0f, new Vector3(num4 - num5, 0f, 0f));
      vector3KeyFrameAnimation.InsertKeyFrame(1f, new Vector3(num4 + num5, 0f, 0f), Easing.EaseInOut(compositor));
      vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(this.seconds);
      vector3KeyFrameAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
      this.sprite.StartAnimation("Offset", vector3KeyFrameAnimation);
    }

    // Token: 0x040010B5 RID: 4277
    private readonly double peak;

    // Token: 0x040010B6 RID: 4278
    private readonly double seconds;

    // Token: 0x040010B7 RID: 4279
    [Nullable(2)]
    private SpriteVisual sprite;
  }
}
