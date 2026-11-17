using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.UI
{
  // Token: 0x020000A4 RID: 164
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BrandMark : Grid
  {
    // Token: 0x060006D6 RID: 1750 RVA: 0x005329F8 File Offset: 0x005329F8
    public BrandMark(double markWidth, Color stroke, double strokeWeight = 1.0)
    {
      BrandMark.<>c__DisplayClass18_0 CS$<>8__locals1 = new BrandMark.<>c__DisplayClass18_0();
      CS$<>8__locals1.stroke = stroke;
      base..ctor();
      CS$<>8__locals1.<>4__this = this;
      this.markWidth = markWidth;
      CS$<>8__locals1.sx = markWidth / 48.0;
      double num = markWidth * 31.0 / 48.0;
      CS$<>8__locals1.sy = num / 31.0;
      base.Width = markWidth;
      base.Height = num;
      base.IsHitTestVisible = false;
      CS$<>8__locals1.lineWidth = markWidth * 3.0 / 48.0 * strokeWeight;
      Polyline polyline = CS$<>8__locals1.<.ctor>g__Line|0(new ValueTuple<double, double>[]
      {
        new ValueTuple<double, double>(24.3, 23.3),
        new ValueTuple<double, double>(33.4, 23.3)
      });
      this.openZig = CS$<>8__locals1.<.ctor>g__Line|0(new ValueTuple<double, double>[]
      {
        new ValueTuple<double, double>(9.5, 12.4),
        new ValueTuple<double, double>(14.5, 6.5),
        new ValueTuple<double, double>(24.3, 23.3)
      });
      this.openRightEye = CS$<>8__locals1.<.ctor>g__Line|0(new ValueTuple<double, double>[]
      {
        new ValueTuple<double, double>(27.1, 12.4),
        new ValueTuple<double, double>(32.86, 6.5),
        new ValueTuple<double, double>(38.4, 12.4)
      });
      this.shutZig = CS$<>8__locals1.<.ctor>g__Line|0(new ValueTuple<double, double>[]
      {
        new ValueTuple<double, double>(9.5, 9.5),
        new ValueTuple<double, double>(14.5, 9.5),
        new ValueTuple<double, double>(24.3, 23.3)
      });
      this.shutRightEye = CS$<>8__locals1.<.ctor>g__Line|0(new ValueTuple<double, double>[]
      {
        new ValueTuple<double, double>(27.1, 9.5),
        new ValueTuple<double, double>(38.4, 9.5)
      });
      ElementCompositionPreview.GetElementVisual(this.shutZig).Opacity = 0f;
      ElementCompositionPreview.GetElementVisual(this.shutRightEye).Opacity = 0f;
      Canvas canvas = new Canvas
      {
        Width = markWidth,
        Height = num
      };
      canvas.Children.Add(polyline);
      this.upper = new Canvas
      {
        Width = markWidth,
        Height = num
      };
      this.upper.Children.Add(this.openZig);
      this.upper.Children.Add(this.shutZig);
      CS$<>8__locals1.rightEye = new Canvas
      {
        Width = markWidth,
        Height = num
      };
      CS$<>8__locals1.rightEye.Children.Add(this.openRightEye);
      CS$<>8__locals1.rightEye.Children.Add(this.shutRightEye);
      this.upper.Children.Add(CS$<>8__locals1.rightEye);
      base.Children.Add(canvas);
      base.Children.Add(this.upper);
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        CS$<>8__locals1.<>4__this.Start(CS$<>8__locals1.rightEye, (float)((double)BrandMark.Pivot.X * CS$<>8__locals1.sx), (float)((double)BrandMark.Pivot.Y * CS$<>8__locals1.sy));
      };
    }

    // Token: 0x060006D7 RID: 1751 RVA: 0x00532D84 File Offset: 0x00532D84
    public void SetStroke(Color stroke)
    {
      SolidColorBrush solidColorBrush = Ui.Brush(stroke);
      Polyline[] array = new Polyline[] { this.openZig, this.openRightEye, this.shutZig, this.shutRightEye };
      for (int i = 0; i < array.Length; i++)
      {
        array[i].Stroke = solidColorBrush;
      }
      foreach (Canvas canvas in base.Children.OfType<Canvas>())
      {
        foreach (Polyline polyline in canvas.Children.OfType<Polyline>())
        {
          polyline.Stroke = solidColorBrush;
        }
      }
    }

    // Token: 0x060006D8 RID: 1752 RVA: 0x00532E5C File Offset: 0x00532E5C
    public void BlinkOnce()
    {
      BrandMark.<>c__DisplayClass20_0 CS$<>8__locals1 = new BrandMark.<>c__DisplayClass20_0();
      CS$<>8__locals1.<>4__this = this;
      if (Anim.Instant || Anim.ReduceMotion || base.XamlRoot == null)
      {
        return;
      }
      Compositor compositor = Ui.Compositor;
      StepEasingFunction stepEasingFunction = compositor.CreateStepEasingFunction();
      stepEasingFunction.IsFinalStepSingleFrame = false;
      CS$<>8__locals1.duration = 0.36;
      CompositionScopedBatch compositionScopedBatch = compositor.CreateScopedBatch(CompositionBatchTypes.Animation);
      foreach (ValueTuple<UIElement, bool> valueTuple in new ValueTuple<UIElement, bool>[]
      {
        new ValueTuple<UIElement, bool>(this.openZig, false),
        new ValueTuple<UIElement, bool>(this.openRightEye, false),
        new ValueTuple<UIElement, bool>(this.shutZig, true),
        new ValueTuple<UIElement, bool>(this.shutRightEye, true)
      })
      {
        UIElement item = valueTuple.Item1;
        bool item2 = valueTuple.Item2;
        ScalarKeyFrameAnimation scalarKeyFrameAnimation = compositor.CreateScalarKeyFrameAnimation();
        scalarKeyFrameAnimation.InsertKeyFrame(0f, item2 > false, stepEasingFunction);
        scalarKeyFrameAnimation.InsertKeyFrame(CS$<>8__locals1.<BlinkOnce>g__K|0(0.09), !item2, stepEasingFunction);
        scalarKeyFrameAnimation.InsertKeyFrame(CS$<>8__locals1.<BlinkOnce>g__K|0(0.27), item2 > false, stepEasingFunction);
        scalarKeyFrameAnimation.InsertKeyFrame(1f, !item2, stepEasingFunction);
        scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(CS$<>8__locals1.duration);
        ElementCompositionPreview.GetElementVisual(item).StartAnimation("Opacity", scalarKeyFrameAnimation);
      }
      compositionScopedBatch.End();
      compositionScopedBatch.Completed += delegate(object _, CompositionBatchCompletedEventArgs _)
      {
        CS$<>8__locals1.<>4__this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(CS$<>8__locals1.<>4__this.RestartEyeCycle));
      };
    }

    // Token: 0x060006D9 RID: 1753 RVA: 0x00532FE4 File Offset: 0x00532FE4
    private void RestartEyeCycle()
    {
      if (this.started && this.upper.Children.Count >= 3)
      {
        UIElement uielement = this.upper.Children[2];
        if (uielement != null)
        {
          this.started = false;
          double num = this.markWidth / 48.0;
          double num2 = this.markWidth * 31.0 / 48.0 / 31.0;
          this.Start(uielement, (float)((double)BrandMark.Pivot.X * num), (float)((double)BrandMark.Pivot.Y * num2));
          return;
        }
      }
    }

    // Token: 0x060006DA RID: 1754 RVA: 0x00533084 File Offset: 0x00533084
    private void Start(UIElement rightEye, float pivotX, float pivotY)
    {
      if (this.started || Anim.Instant)
      {
        return;
      }
      this.started = true;
      BrandMark.<>c__DisplayClass22_0 CS$<>8__locals1;
      CS$<>8__locals1.c = Ui.Compositor;
      CS$<>8__locals1.linear = CS$<>8__locals1.c.CreateLinearEasingFunction();
      CS$<>8__locals1.look = CS$<>8__locals1.c.CreateCubicBezierEasingFunction(new Vector2(0.4f, 0f), new Vector2(0.6f, 1.2f));
      CS$<>8__locals1.settle = Easing.EaseInOut(CS$<>8__locals1.c);
      CS$<>8__locals1.step = CS$<>8__locals1.c.CreateStepEasingFunction();
      CS$<>8__locals1.step.IsFinalStepSingleFrame = false;
      Visual elementVisual = ElementCompositionPreview.GetElementVisual(this.upper);
      elementVisual.CenterPoint = new Vector3(pivotX, pivotY, 0f);
      elementVisual.StartAnimation("RotationAngleInDegrees", BrandMark.<Start>g__Look|22_1(-4.5f, ref CS$<>8__locals1));
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = BrandMark.<Start>g__Look|22_1((float)(-(float)this.markWidth * 0.03), ref CS$<>8__locals1);
      ElementCompositionPreview.SetIsTranslationEnabled(rightEye, true);
      ElementCompositionPreview.GetElementVisual(rightEye).StartAnimation("Translation.X", scalarKeyFrameAnimation);
      CS$<>8__locals1.second = 4.47;
      foreach (ValueTuple<UIElement, bool> valueTuple in new ValueTuple<UIElement, bool>[]
      {
        new ValueTuple<UIElement, bool>(this.openZig, false),
        new ValueTuple<UIElement, bool>(this.openRightEye, false),
        new ValueTuple<UIElement, bool>(this.shutZig, true),
        new ValueTuple<UIElement, bool>(this.shutRightEye, true)
      })
      {
        UIElement item = valueTuple.Item1;
        bool item2 = valueTuple.Item2;
        Visual elementVisual2 = ElementCompositionPreview.GetElementVisual(item);
        elementVisual2.Opacity = !item2;
        elementVisual2.StartAnimation("Opacity", BrandMark.<Start>g__Blink|22_2(item2, ref CS$<>8__locals1));
      }
    }

    // Token: 0x060006DC RID: 1756 RVA: 0x00533250 File Offset: 0x00533250
    [CompilerGenerated]
    internal static float <Start>g__K|22_0(double seconds)
    {
      return (float)(seconds / 6.16);
    }

    // Token: 0x060006DD RID: 1757 RVA: 0x00533260 File Offset: 0x00533260
    [CompilerGenerated]
    internal static ScalarKeyFrameAnimation <Start>g__Look|22_1(float peak, ref BrandMark.<>c__DisplayClass22_0 A_1)
    {
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = A_1.c.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(0f, 0f, A_1.linear);
      scalarKeyFrameAnimation.InsertKeyFrame(BrandMark.<Start>g__K|22_0(1.9), 0f, A_1.linear);
      scalarKeyFrameAnimation.InsertKeyFrame(BrandMark.<Start>g__K|22_0(2.45), peak, A_1.look);
      scalarKeyFrameAnimation.InsertKeyFrame(BrandMark.<Start>g__K|22_0(3.2), peak, A_1.linear);
      scalarKeyFrameAnimation.InsertKeyFrame(BrandMark.<Start>g__K|22_0(3.7), 0f, A_1.settle);
      scalarKeyFrameAnimation.InsertKeyFrame(1f, 0f, A_1.linear);
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(6.16);
      scalarKeyFrameAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
      return scalarKeyFrameAnimation;
    }

    // Token: 0x060006DE RID: 1758 RVA: 0x00533334 File Offset: 0x00533334
    [CompilerGenerated]
    internal static ScalarKeyFrameAnimation <Start>g__Blink|22_2(bool shutTwin, ref BrandMark.<>c__DisplayClass22_0 A_1)
    {
      float num = !shutTwin;
      float num2 = shutTwin > false;
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = A_1.c.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(0f, num, A_1.step);
      scalarKeyFrameAnimation.InsertKeyFrame(BrandMark.<Start>g__K|22_0(4.2), num2, A_1.step);
      scalarKeyFrameAnimation.InsertKeyFrame(BrandMark.<Start>g__K|22_0(4.29), num, A_1.step);
      scalarKeyFrameAnimation.InsertKeyFrame(BrandMark.<Start>g__K|22_0(A_1.second), num2, A_1.step);
      scalarKeyFrameAnimation.InsertKeyFrame(BrandMark.<Start>g__K|22_0(A_1.second + 0.09), num, A_1.step);
      scalarKeyFrameAnimation.InsertKeyFrame(1f, num, A_1.step);
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(6.16);
      scalarKeyFrameAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
      return scalarKeyFrameAnimation;
    }

    // Token: 0x040001A8 RID: 424
    private const double ViewW = 48.0;

    // Token: 0x040001A9 RID: 425
    private const double ViewH = 31.0;

    // Token: 0x040001AA RID: 426
    private const double LookStart = 1.9;

    // Token: 0x040001AB RID: 427
    private const double LookDuration = 0.55;

    // Token: 0x040001AC RID: 428
    private const double SettleStart = 3.2;

    // Token: 0x040001AD RID: 429
    private const double SettleDuration = 0.5;

    // Token: 0x040001AE RID: 430
    private const double FirstBlink = 4.2;

    // Token: 0x040001AF RID: 431
    private const double BlinkShut = 0.09;

    // Token: 0x040001B0 RID: 432
    private const double BlinkGap = 0.18;

    // Token: 0x040001B1 RID: 433
    private const double Cycle = 6.16;

    // Token: 0x040001B2 RID: 434
    private static readonly Vector2 Pivot = new Vector2(24.3f, 23.3f);

    // Token: 0x040001B3 RID: 435
    private readonly double markWidth;

    // Token: 0x040001B4 RID: 436
    private readonly Canvas upper;

    // Token: 0x040001B5 RID: 437
    private readonly Polyline openZig;

    // Token: 0x040001B6 RID: 438
    private readonly Polyline openRightEye;

    // Token: 0x040001B7 RID: 439
    private readonly Polyline shutZig;

    // Token: 0x040001B8 RID: 440
    private readonly Polyline shutRightEye;

    // Token: 0x040001B9 RID: 441
    private bool started;
  }
}
