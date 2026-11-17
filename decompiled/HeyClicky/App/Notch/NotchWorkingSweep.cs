using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Windows.UI;
using Windows.UI.Composition;

namespace HeyClicky.App.Notch
{
  // Token: 0x0200013E RID: 318
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchWorkingSweep
  {
    // Token: 0x1700033E RID: 830
    // (get) Token: 0x06001196 RID: 4502 RVA: 0x0058DFB8 File Offset: 0x0058DFB8
    public ContainerVisual Root { get; }

    // Token: 0x06001197 RID: 4503 RVA: 0x0058DFC0 File Offset: 0x0058DFC0
    public NotchWorkingSweep(Compositor compositor, NotchSystemShape shape)
    {
      this.c = compositor;
      this.props = shape.Props;
      this.run = this.c.CreatePropertySet();
      this.run.InsertScalar("Progress", 0f);
      this.Root = this.c.CreateContainerVisual();
      this.Root.RelativeSizeAdjustment = Vector2.One;
      this.Root.Clip = this.c.CreateGeometricClip(shape.ClipGeometry);
      this.Root.Opacity = 0f;
      ContainerVisual containerVisual = this.c.CreateContainerVisual();
      containerVisual.RelativeSizeAdjustment = Vector2.One;
      this.Bind(containerVisual, "Opacity", "p.Sliver * Clamp((16 - p.Height) / 8, 0, 1)");
      this.Root.Children.InsertAtTop(containerVisual);
      SpriteVisual spriteVisual = this.c.CreateSpriteVisual();
      spriteVisual.Brush = this.c.CreateColorBrush(NotchWorkingSweep.Green.WithAlpha(0.26));
      this.Bind(spriteVisual, "Offset", "Vector3((p.EnvW - p.Width) / 2, p.Gap, 0)");
      this.Bind(spriteVisual, "Size", "Vector2(p.Width, p.Height)");
      containerVisual.Children.InsertAtTop(spriteVisual);
      ShapeVisual shapeVisual = this.c.CreateShapeVisual();
      shapeVisual.RelativeSizeAdjustment = Vector2.One;
      CompositionRoundedRectangleGeometry compositionRoundedRectangleGeometry = this.c.CreateRoundedRectangleGeometry();
      shape.BindHairline(compositionRoundedRectangleGeometry);
      this.hairline = this.c.CreateSpriteShape(compositionRoundedRectangleGeometry);
      this.hairline.StrokeBrush = this.c.CreateColorBrush(NotchWorkingSweep.Green.WithAlpha(0.75));
      this.hairline.StrokeThickness = shape.Hairline;
      shapeVisual.Shapes.Add(this.hairline);
      containerVisual.Children.InsertAtTop(shapeVisual);
      shape.HairlineChanged += delegate(float px)
      {
        this.hairline.StrokeThickness = px;
      };
      CompositionLinearGradientBrush compositionLinearGradientBrush = this.c.CreateLinearGradientBrush();
      compositionLinearGradientBrush.StartPoint = new Vector2(0f, 0.5f);
      compositionLinearGradientBrush.EndPoint = new Vector2(1f, 0.5f);
      foreach (ValueTuple<float, Color> valueTuple in new ValueTuple<float, Color>[]
      {
        new ValueTuple<float, Color>(0f, NotchWorkingSweep.Green.WithAlpha(0.0)),
        new ValueTuple<float, Color>(0.28f, NotchWorkingSweep.Green.WithAlpha(0.5)),
        new ValueTuple<float, Color>(0.5f, NotchWorkingSweep.Mint.WithAlpha(0.95)),
        new ValueTuple<float, Color>(0.72f, NotchWorkingSweep.Green.WithAlpha(0.5)),
        new ValueTuple<float, Color>(1f, NotchWorkingSweep.Green.WithAlpha(0.0))
      })
      {
        float item = valueTuple.Item1;
        Color item2 = valueTuple.Item2;
        compositionLinearGradientBrush.ColorStops.Add(this.c.CreateColorGradientStop(item, item2));
      }
      SpriteVisual spriteVisual2 = this.c.CreateSpriteVisual();
      spriteVisual2.Brush = compositionLinearGradientBrush;
      ExpressionAnimation expressionAnimation = this.c.CreateExpressionAnimation(FormattableString.Invariant(FormattableStringFactory.Create("Vector3((p.EnvW - p.Width) / 2 - {0} + (p.Width + {1}) * r.Progress, p.Gap, 0)", new object[] { 30f, 30f })));
      expressionAnimation.SetReferenceParameter("p", this.props);
      expressionAnimation.SetReferenceParameter("r", this.run);
      spriteVisual2.StartAnimation("Offset", expressionAnimation);
      this.Bind(spriteVisual2, "Size", FormattableString.Invariant(FormattableStringFactory.Create("Vector2({0}, p.Height)", new object[] { 30f })));
      containerVisual.Children.InsertAtTop(spriteVisual2);
    }

    // Token: 0x1700033F RID: 831
    // (get) Token: 0x06001198 RID: 4504 RVA: 0x0058E390 File Offset: 0x0058E390
    public bool IsShowing
    {
      get
      {
        return this.showing;
      }
    }

    // Token: 0x06001199 RID: 4505 RVA: 0x0058E398 File Offset: 0x0058E398
    public void Show(bool working)
    {
      if (working == this.showing)
      {
        return;
      }
      this.showing = working;
      if (working)
      {
        this.StartRun();
      }
      this.Fade(working > false);
    }

    // Token: 0x0600119A RID: 4506 RVA: 0x0058E3C0 File Offset: 0x0058E3C0
    private void StartRun()
    {
      if (Anim.ReduceMotion || Anim.Instant)
      {
        return;
      }
      double num = 1.6;
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = this.c.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(0f, 0f);
      scalarKeyFrameAnimation.InsertKeyFrame((float)(1.1 / num), 1f, SystemEasing.EaseInOut(this.c));
      scalarKeyFrameAnimation.InsertKeyFrame(1f, 1f);
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(num);
      scalarKeyFrameAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
      scalarKeyFrameAnimation.DelayTime = TimeSpan.FromSeconds(0.3);
      scalarKeyFrameAnimation.DelayBehavior = AnimationDelayBehavior.SetInitialValueBeforeDelay;
      this.run.StartAnimation("Progress", scalarKeyFrameAnimation);
    }

    // Token: 0x0600119B RID: 4507 RVA: 0x0058E474 File Offset: 0x0058E474
    private void StopRun()
    {
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = this.c.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(1f, 0f);
      scalarKeyFrameAnimation.Duration = TimeSpan.FromMilliseconds(1L, 0L);
      this.run.StartAnimation("Progress", scalarKeyFrameAnimation);
    }

    // Token: 0x0600119C RID: 4508 RVA: 0x0058E4C0 File Offset: 0x0058E4C0
    private void Fade(float to)
    {
      if (Anim.Instant)
      {
        this.Root.StopAnimation("Opacity");
        this.Root.Opacity = to;
        if (to == 0f)
        {
          this.StopRun();
        }
        return;
      }
      CompositionScopedBatch compositionScopedBatch = this.c.CreateScopedBatch(CompositionBatchTypes.Animation);
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = this.c.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(1f, to, SystemEasing.Expressive(this.c));
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(0.36);
      this.Root.StartAnimation("Opacity", scalarKeyFrameAnimation);
      compositionScopedBatch.End();
      if (to == 0f)
      {
        compositionScopedBatch.Completed += delegate(object _, CompositionBatchCompletedEventArgs _)
        {
          if (!this.showing)
          {
            this.StopRun();
          }
        };
      }
    }

    // Token: 0x0600119D RID: 4509 RVA: 0x0058E574 File Offset: 0x0058E574
    private void Bind(CompositionObject target, string property, string expression)
    {
      ExpressionAnimation expressionAnimation = this.c.CreateExpressionAnimation(expression);
      expressionAnimation.SetReferenceParameter("p", this.props);
      target.StartAnimation(property, expressionAnimation);
    }

    // Token: 0x040007D3 RID: 2003
    private const float LightLength = 30f;

    // Token: 0x040007D4 RID: 2004
    private const double CrossSeconds = 1.1;

    // Token: 0x040007D5 RID: 2005
    private const double RestSeconds = 0.5;

    // Token: 0x040007D6 RID: 2006
    private const double FirstRunDelaySeconds = 0.3;

    // Token: 0x040007D7 RID: 2007
    private const double TintAlpha = 0.26;

    // Token: 0x040007D8 RID: 2008
    private const double HairlineAlpha = 0.75;

    // Token: 0x040007D9 RID: 2009
    private static readonly Color Green = DS.Hex(1755979U, 1.0);

    // Token: 0x040007DA RID: 2010
    private static readonly Color Mint = DS.Hex(12124107U, 1.0);

    // Token: 0x040007DB RID: 2011
    private readonly Compositor c;

    // Token: 0x040007DC RID: 2012
    private readonly CompositionPropertySet props;

    // Token: 0x040007DD RID: 2013
    private readonly CompositionPropertySet run;

    // Token: 0x040007DE RID: 2014
    private readonly CompositionSpriteShape hairline;

    // Token: 0x040007DF RID: 2015
    private bool showing;
  }
}
