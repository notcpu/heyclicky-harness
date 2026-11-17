using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Windows.UI;

namespace HeyClicky.App.UI
{
  // Token: 0x020000AD RID: 173
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ShadowLayer : Grid
  {
    // Token: 0x06000727 RID: 1831 RVA: 0x00535C68 File Offset: 0x00535C68
    public ShadowLayer(double cornerRadius, Color color, double swiftRadius, double offsetY, double offsetX = 0.0)
    {
      ShadowLayer <>4__this = this;
      base.IsHitTestVisible = false;
      Compositor compositor = Ui.Compositor;
      this.geo = compositor.CreateRoundedRectangleGeometry();
      this.geo.CornerRadius = new Vector2((float)cornerRadius);
      CompositionSpriteShape compositionSpriteShape = compositor.CreateSpriteShape(this.geo);
      compositionSpriteShape.FillBrush = compositor.CreateColorBrush(Colors.Black);
      ShapeVisual shapeVisual = compositor.CreateShapeVisual();
      shapeVisual.Shapes.Add(compositionSpriteShape);
      CompositionVisualSurface surface = compositor.CreateVisualSurface();
      surface.SourceVisual = shapeVisual;
      CompositionSurfaceBrush compositionSurfaceBrush = compositor.CreateSurfaceBrush(surface);
      compositionSurfaceBrush.Stretch = CompositionStretch.None;
      DropShadow dropShadow = compositor.CreateDropShadow();
      dropShadow.Color = color;
      dropShadow.BlurRadius = (float)(swiftRadius * 2.0);
      dropShadow.Offset = new Vector3((float)offsetX, (float)offsetY, 0f);
      dropShadow.Mask = compositionSurfaceBrush;
      this.sprite = compositor.CreateSpriteVisual();
      this.sprite.Shadow = dropShadow;
      ElementCompositionPreview.SetElementChildVisual(this, this.sprite);
      base.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        Vector2 vector = new Vector2((float)<>4__this.ActualWidth, (float)<>4__this.ActualHeight);
        float num = (float)Math.Min(cornerRadius, Math.Min(<>4__this.ActualWidth, <>4__this.ActualHeight) / 2.0);
        <>4__this.geo.CornerRadius = new Vector2(num);
        <>4__this.geo.Size = vector;
        shapeVisual.Size = vector;
        surface.SourceSize = vector;
        <>4__this.sprite.Size = vector;
      };
    }

    // Token: 0x0400021D RID: 541
    private readonly CompositionRoundedRectangleGeometry geo;

    // Token: 0x0400021E RID: 542
    private readonly SpriteVisual sprite;
  }
}
