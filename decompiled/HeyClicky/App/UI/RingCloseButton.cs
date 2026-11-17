using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using Microsoft.UI.Composition;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Shapes;

namespace HeyClicky.App.UI
{
  // Token: 0x020000AE RID: 174
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class RingCloseButton : Pressable
  {
    // Token: 0x06000728 RID: 1832 RVA: 0x00535DA4 File Offset: 0x00535DA4
    public RingCloseButton(bool showRing)
      : base(true)
    {
      base.Width = 24.0;
      base.Height = 24.0;
      AutomationProperties.SetName(this, "Close");
      Ellipse ellipse = new Ellipse
      {
        Width = 25.8,
        Height = 25.8,
        Margin = new Thickness(-0.9),
        Stroke = Ui.Brush(DS.White(0.12)),
        StrokeThickness = 1.8,
        Visibility = (showRing ? Visibility.Visible : Visibility.Collapsed)
      };
      Ellipse ellipse2 = new Ellipse
      {
        Width = 20.0,
        Height = 20.0,
        Fill = Ui.Brush(DS.White(0.1)),
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      };
      TextBlock textBlock = Icons.Glyph("\ue711", 8.5, DS.Colors.TextSecondary);
      textBlock.FontWeight = FontWeights.Bold;
      Grid grid = new Grid
      {
        Width = 25.8,
        Height = 25.8,
        Margin = new Thickness(-0.9),
        IsHitTestVisible = false
      };
      Compositor compositor = Ui.Compositor;
      this.ringGeo = compositor.CreateEllipseGeometry();
      this.ringGeo.Center = new Vector2(12.9f);
      this.ringGeo.Radius = new Vector2(12f);
      this.ring = compositor.CreateSpriteShape(this.ringGeo);
      this.ring.StrokeBrush = compositor.CreateColorBrush(DS.White(0.45));
      this.ring.StrokeThickness = 1.8f;
      this.ring.StrokeStartCap = (this.ring.StrokeEndCap = CompositionStrokeCap.Round);
      this.ring.CenterPoint = new Vector2(12.9f);
      this.ring.RotationAngleInDegrees = -90f;
      ShapeVisual shapeVisual = compositor.CreateShapeVisual();
      shapeVisual.Size = new Vector2(25.8f);
      shapeVisual.Shapes.Add(this.ring);
      ElementCompositionPreview.SetElementChildVisual(grid, shapeVisual);
      grid.Visibility = (showRing ? Visibility.Visible : Visibility.Collapsed);
      base.Children.Add(ellipse);
      base.Children.Add(grid);
      base.Children.Add(ellipse2);
      base.Children.Add(textBlock);
      base.WithIconHover(24.0);
    }

    // Token: 0x06000729 RID: 1833 RVA: 0x00536040 File Offset: 0x00536040
    public void StartCountdown(double seconds, double fraction = 1.0)
    {
      Compositor compositor = Ui.Compositor;
      this.ringGeo.TrimEnd = (float)fraction;
      if (Anim.Instant)
      {
        return;
      }
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = compositor.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(0f, (float)fraction);
      scalarKeyFrameAnimation.InsertKeyFrame(1f, 0f, compositor.CreateLinearEasingFunction());
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(Math.Max(0.01, seconds));
      this.ringGeo.StartAnimation("TrimEnd", scalarKeyFrameAnimation);
    }

    // Token: 0x0600072A RID: 1834 RVA: 0x005360C0 File Offset: 0x005360C0
    public void SetProgress(double fraction)
    {
      this.ringGeo.TrimEnd = (float)Math.Clamp(fraction, 0.0, 1.0);
    }

    // Token: 0x0600072B RID: 1835 RVA: 0x005360E8 File Offset: 0x005360E8
    public void GlideTo(double fraction, double seconds)
    {
      Compositor compositor = Ui.Compositor;
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = compositor.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(1f, (float)Math.Clamp(fraction, 0.0, 1.0), compositor.CreateLinearEasingFunction());
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(Anim.Instant ? 0.001 : Math.Max(0.001, seconds));
      this.ringGeo.StartAnimation("TrimEnd", scalarKeyFrameAnimation);
    }

    // Token: 0x0400021F RID: 543
    private readonly CompositionSpriteShape ring;

    // Token: 0x04000220 RID: 544
    private readonly CompositionEllipseGeometry ringGeo;
  }
}
