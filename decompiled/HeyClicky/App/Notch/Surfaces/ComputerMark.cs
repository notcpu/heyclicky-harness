using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Windows.UI;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000166 RID: 358
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ComputerMark : Grid
  {
    // Token: 0x17000394 RID: 916
    // (get) Token: 0x060013A5 RID: 5029 RVA: 0x005A37CC File Offset: 0x005A37CC
    private static float MarkHeight
    {
      get
      {
        return 15.5f;
      }
    }

    // Token: 0x17000395 RID: 917
    // (get) Token: 0x060013A6 RID: 5030 RVA: 0x005A37D4 File Offset: 0x005A37D4
    private static float ViewScale
    {
      get
      {
        return 0.5f;
      }
    }

    // Token: 0x17000396 RID: 918
    // (get) Token: 0x060013A7 RID: 5031 RVA: 0x005A37DC File Offset: 0x005A37DC
    private static float BaseLineWidth
    {
      get
      {
        return 1.875f;
      }
    }

    // Token: 0x060013A8 RID: 5032 RVA: 0x005A37E4 File Offset: 0x005A37E4
    public ComputerMark()
    {
      base.Width = 27.0;
      base.Height = 24.0;
      base.IsHitTestVisible = false;
      base.Children.Add(this.markHost);
      base.Children.Add(this.attentionHost);
      this.BuildStrokes();
    }

    // Token: 0x060013A9 RID: 5033 RVA: 0x005A3890 File Offset: 0x005A3890
    [NullableContext(2)]
    public void Set(bool isWorking, string attention)
    {
      ComputerMark.<>c__DisplayClass33_0 CS$<>8__locals1;
      CS$<>8__locals1.attention = attention;
      if (this.attention != CS$<>8__locals1.attention)
      {
        this.attention = CS$<>8__locals1.attention;
        this.attentionHost.Children.Clear();
        this.attentionHost.Visibility = ((CS$<>8__locals1.attention == null) ? Visibility.Collapsed : Visibility.Visible);
        this.markHost.Visibility = ((CS$<>8__locals1.attention == null) ? Visibility.Visible : Visibility.Collapsed);
        if (CS$<>8__locals1.attention != null)
        {
          FrameworkElement frameworkElement = ComputerMark.<Set>g__Symbol|33_0(Colors.White, ref CS$<>8__locals1);
          FrameworkElement coral = ComputerMark.<Set>g__Symbol|33_0(ComputerMark.Coral, ref CS$<>8__locals1);
          this.attentionHost.Children.Add(frameworkElement);
          this.attentionHost.Children.Add(coral);
          coral.Loaded += delegate(object _, RoutedEventArgs _)
          {
            if (Anim.ReduceMotion)
            {
              coral.Opacity = 0.0;
              return;
            }
            Anim.LoopScalar(coral, "Opacity", 1.8, (double x) => (float)(0.5 - 0.5 * Math.Cos(6.283185307179586 * x / 1.8)), 36, 0.0);
          };
        }
      }
      if (this.working == isWorking)
      {
        return;
      }
      this.working = isWorking;
      this.BuildStrokes();
    }

    // Token: 0x060013AA RID: 5034 RVA: 0x005A3988 File Offset: 0x005A3988
    private void BuildStrokes()
    {
      bool flag = this.working && !Anim.ReduceMotion && !Anim.Instant;
      bool? flag2 = this.builtLooping;
      bool flag3 = flag;
      if ((flag2.GetValueOrDefault() == flag3) & (flag2 != null))
      {
        return;
      }
      this.builtLooping = new bool?(flag);
      ComputerMark.<>c__DisplayClass34_0 CS$<>8__locals1;
      CS$<>8__locals1.c = Ui.Compositor;
      Vector2 vector = new Vector2(24f, ComputerMark.MarkHeight);
      ContainerVisual containerVisual = CS$<>8__locals1.c.CreateContainerVisual();
      containerVisual.Size = vector;
      ContainerVisual containerVisual2 = CS$<>8__locals1.c.CreateContainerVisual();
      containerVisual2.Size = vector;
      containerVisual2.CenterPoint = new Vector3(ComputerMark.Pivot.Item1 * ComputerMark.ViewScale, ComputerMark.Pivot.Item2 * ComputerMark.ViewScale, 0f);
      ValueTuple<ShapeVisual, CompositionSpriteShape, CompositionPathGeometry> valueTuple = ComputerMark.Stroke(CS$<>8__locals1.c, ComputerMark.Mouth);
      ShapeVisual item = valueTuple.Item1;
      CompositionSpriteShape item2 = valueTuple.Item2;
      CompositionPathGeometry item3 = valueTuple.Item3;
      ValueTuple<ShapeVisual, CompositionSpriteShape, CompositionPathGeometry> valueTuple2 = ComputerMark.Stroke(CS$<>8__locals1.c, ComputerMark.Zig);
      ShapeVisual item4 = valueTuple2.Item1;
      CompositionSpriteShape item5 = valueTuple2.Item2;
      CompositionPathGeometry item6 = valueTuple2.Item3;
      ValueTuple<ShapeVisual, CompositionSpriteShape, CompositionPathGeometry> valueTuple3 = ComputerMark.Stroke(CS$<>8__locals1.c, ComputerMark.Eye);
      ShapeVisual item7 = valueTuple3.Item1;
      CompositionSpriteShape item8 = valueTuple3.Item2;
      CompositionPathGeometry item9 = valueTuple3.Item3;
      ShapeVisual item10 = ComputerMark.Stroke(CS$<>8__locals1.c, ComputerMark.ShutZig).Item1;
      ShapeVisual item11 = ComputerMark.Stroke(CS$<>8__locals1.c, ComputerMark.ShutEye).Item1;
      item10.Opacity = (item11.Opacity = 0f);
      containerVisual.Children.InsertAtTop(item);
      containerVisual.Children.InsertAtTop(containerVisual2);
      foreach (ShapeVisual shapeVisual in new ShapeVisual[] { item4, item7, item10, item11 })
      {
        containerVisual2.Children.InsertAtTop(shapeVisual);
      }
      if (flag)
      {
        ComputerMark.<>c__DisplayClass34_1 CS$<>8__locals2;
        CS$<>8__locals2.linear = CS$<>8__locals1.c.CreateLinearEasingFunction();
        CS$<>8__locals2.lookOut = CS$<>8__locals1.c.CreateCubicBezierEasingFunction(new Vector2(0.4f, 0f), new Vector2(0.6f, 1.2f));
        CS$<>8__locals2.easeInOut = CS$<>8__locals1.c.CreateCubicBezierEasingFunction(new Vector2(0.42f, 0f), new Vector2(0.58f, 1f));
        containerVisual2.StartAnimation("RotationAngleInDegrees", ComputerMark.<BuildStrokes>g__Look|34_0(-4.5f, ref CS$<>8__locals1, ref CS$<>8__locals2));
        ScalarKeyFrameAnimation scalarKeyFrameAnimation = ComputerMark.<BuildStrokes>g__Look|34_0(-0.71999997f, ref CS$<>8__locals1, ref CS$<>8__locals2);
        item7.StartAnimation("Offset.X", scalarKeyFrameAnimation);
        item11.StartAnimation("Offset.X", scalarKeyFrameAnimation);
        CS$<>8__locals2.step = CS$<>8__locals1.c.CreateStepEasingFunction();
        CS$<>8__locals2.step.IsFinalStepSingleFrame = true;
        CS$<>8__locals2.firstBlink = 2.35f;
        CS$<>8__locals2.secondBlink = CS$<>8__locals2.firstBlink + 0.09f + 0.18f;
        item4.StartAnimation("Opacity", ComputerMark.<BuildStrokes>g__Blink|34_1(false, ref CS$<>8__locals1, ref CS$<>8__locals2));
        item7.StartAnimation("Opacity", ComputerMark.<BuildStrokes>g__Blink|34_1(false, ref CS$<>8__locals1, ref CS$<>8__locals2));
        item10.StartAnimation("Opacity", ComputerMark.<BuildStrokes>g__Blink|34_1(true, ref CS$<>8__locals1, ref CS$<>8__locals2));
        item11.StartAnimation("Opacity", ComputerMark.<BuildStrokes>g__Blink|34_1(true, ref CS$<>8__locals1, ref CS$<>8__locals2));
        CS$<>8__locals2.timeline = new ValueTuple<float, float, float, float, bool>[]
        {
          new ValueTuple<float, float, float, float, bool>(0f, 16f, 16f, 16f, false),
          new ValueTuple<float, float, float, float, bool>(3f, 16f, 16f, 16f, false),
          new ValueTuple<float, float, float, float, bool>(3.35f, 16f, 16f, 16f, true),
          new ValueTuple<float, float, float, float, bool>(3.6f, 6.5f, 16f, 16f, true),
          new ValueTuple<float, float, float, float, bool>(3.85f, 16f, 6.5f, 16f, true),
          new ValueTuple<float, float, float, float, bool>(4.1f, 16f, 16f, 6.5f, true),
          new ValueTuple<float, float, float, float, bool>(4.35f, 16f, 16f, 16f, true),
          new ValueTuple<float, float, float, float, bool>(4.6f, 6.5f, 16f, 16f, true),
          new ValueTuple<float, float, float, float, bool>(4.85f, 16f, 6.5f, 16f, true),
          new ValueTuple<float, float, float, float, bool>(5.1f, 16f, 16f, 6.5f, true),
          new ValueTuple<float, float, float, float, bool>(5.35f, 16f, 16f, 16f, true),
          new ValueTuple<float, float, float, float, bool>(5.8f, 16f, 16f, 16f, false),
          new ValueTuple<float, float, float, float, bool>(6.6f, 16f, 16f, 16f, false)
        };
        ComputerMark.<BuildStrokes>g__Morph|34_2(item6, item5, ComputerMark.Zig, ([TupleElementNames(new string[] { "L", "M", "R" })] ValueTuple<float, float, float> d) => new ValueTuple<float, float>(8f, d.Item1), ref CS$<>8__locals1, ref CS$<>8__locals2);
        ComputerMark.<BuildStrokes>g__Morph|34_2(item9, item8, ComputerMark.Eye, ([TupleElementNames(new string[] { "L", "M", "R" })] ValueTuple<float, float, float> d) => new ValueTuple<float, float>(40f, d.Item3), ref CS$<>8__locals1, ref CS$<>8__locals2);
        ComputerMark.<BuildStrokes>g__Morph|34_2(item3, item2, ComputerMark.Mouth, ([TupleElementNames(new string[] { "L", "M", "R" })] ValueTuple<float, float, float> d) => new ValueTuple<float, float>(24f, d.Item2), ref CS$<>8__locals1, ref CS$<>8__locals2);
      }
      ElementCompositionPreview.SetElementChildVisual(this.markHost, containerVisual);
    }

    // Token: 0x060013AB RID: 5035 RVA: 0x005A3F28 File Offset: 0x005A3F28
    private static T Loop<[Nullable(0)] T>(T animation) where T : KeyFrameAnimation
    {
      animation.Duration = TimeSpan.FromSeconds(6.599999904632568);
      animation.IterationBehavior = AnimationIterationBehavior.Forever;
      return animation;
    }

    // Token: 0x060013AC RID: 5036 RVA: 0x005A3F50 File Offset: 0x005A3F50
    [return: TupleElementNames(new string[] { "Visual", "Shape", "Path" })]
    [return: Nullable(new byte[] { 0, 1, 1, 1 })]
    private static ValueTuple<ShapeVisual, CompositionSpriteShape, CompositionPathGeometry> Stroke(Compositor c, [TupleElementNames(new string[] { "X", "Y" })] [Nullable(new byte[] { 1, 0 })] ValueTuple<float, float>[] points)
    {
      CompositionPathGeometry compositionPathGeometry = c.CreatePathGeometry(ComputerMark.PathOf(points));
      CompositionSpriteShape compositionSpriteShape = c.CreateSpriteShape(compositionPathGeometry);
      compositionSpriteShape.StrokeBrush = c.CreateColorBrush(Colors.White);
      compositionSpriteShape.StrokeThickness = ComputerMark.BaseLineWidth;
      compositionSpriteShape.StrokeStartCap = (compositionSpriteShape.StrokeEndCap = CompositionStrokeCap.Round);
      compositionSpriteShape.StrokeLineJoin = CompositionStrokeLineJoin.Round;
      ShapeVisual shapeVisual = c.CreateShapeVisual();
      shapeVisual.Size = new Vector2(24f, ComputerMark.MarkHeight);
      shapeVisual.Shapes.Add(compositionSpriteShape);
      return new ValueTuple<ShapeVisual, CompositionSpriteShape, CompositionPathGeometry>(shapeVisual, compositionSpriteShape, compositionPathGeometry);
    }

    // Token: 0x060013AD RID: 5037 RVA: 0x005A3FD4 File Offset: 0x005A3FD4
    private static CompositionPath PathOf([TupleElementNames(new string[] { "X", "Y" })] [Nullable(new byte[] { 1, 0 })] ValueTuple<float, float>[] points)
    {
      CanvasPathBuilder canvasPathBuilder = new CanvasPathBuilder(CanvasDevice.GetSharedDevice());
      canvasPathBuilder.BeginFigure(points[0].Item1 * ComputerMark.ViewScale, points[0].Item2 * ComputerMark.ViewScale);
      for (int i = 1; i < points.Length; i++)
      {
        canvasPathBuilder.AddLine(points[i].Item1 * ComputerMark.ViewScale, points[i].Item2 * ComputerMark.ViewScale);
      }
      canvasPathBuilder.EndFigure(CanvasFigureLoop.Open);
      return new CompositionPath(CanvasGeometry.CreatePath(canvasPathBuilder));
    }

    // Token: 0x060013AF RID: 5039 RVA: 0x005A4204 File Offset: 0x005A4204
    [CompilerGenerated]
    internal static FrameworkElement <Set>g__Symbol|33_0(Color c, ref ComputerMark.<>c__DisplayClass33_0 A_1)
    {
      if (A_1.attention == "hand")
      {
        return Icons.PathIcon("M8,12 V5.6 a1.3,1.3 0 0 1 2.6,0 V10.5 V3.9 a1.3,1.3 0 0 1 2.6,0 V10.5 V4.6 a1.3,1.3 0 0 1 2.6,0 V11 V7.4 a1.3,1.3 0 0 1 2.6,0 V14.2 C18.4,18.4 15.9,21.5 12,21.5 C9.4,21.5 7.8,20.2 6.4,18 L3.9,13.9 a1.3,1.3 0 0 1 2.1,-1.5 Z", 15.0, c);
      }
      TextBlock textBlock = Icons.Glyph("\ue7ba", 14.0, c);
      textBlock.FontWeight = FontWeights.Bold;
      return textBlock;
    }

    // Token: 0x060013B0 RID: 5040 RVA: 0x005A4258 File Offset: 0x005A4258
    [CompilerGenerated]
    internal static ScalarKeyFrameAnimation <BuildStrokes>g__Look|34_0(float peak, ref ComputerMark.<>c__DisplayClass34_0 A_1, ref ComputerMark.<>c__DisplayClass34_1 A_2)
    {
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = A_1.c.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(0f, 0f, A_2.linear);
      scalarKeyFrameAnimation.InsertKeyFrame(0.09090909f, 0f, A_2.linear);
      scalarKeyFrameAnimation.InsertKeyFrame(0.17424242f, peak, A_2.lookOut);
      scalarKeyFrameAnimation.InsertKeyFrame(0.24242425f, peak, A_2.linear);
      scalarKeyFrameAnimation.InsertKeyFrame(0.3181818f, 0f, A_2.easeInOut);
      scalarKeyFrameAnimation.InsertKeyFrame(1f, 0f, A_2.linear);
      return ComputerMark.Loop<ScalarKeyFrameAnimation>(scalarKeyFrameAnimation);
    }

    // Token: 0x060013B1 RID: 5041 RVA: 0x005A42F4 File Offset: 0x005A42F4
    [CompilerGenerated]
    internal static ScalarKeyFrameAnimation <BuildStrokes>g__Blink|34_1(bool shutTwin, ref ComputerMark.<>c__DisplayClass34_0 A_1, ref ComputerMark.<>c__DisplayClass34_1 A_2)
    {
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = A_1.c.CreateScalarKeyFrameAnimation();
      float num = !shutTwin;
      float num2 = shutTwin > false;
      scalarKeyFrameAnimation.InsertKeyFrame(0f, num, A_2.step);
      scalarKeyFrameAnimation.InsertKeyFrame(A_2.firstBlink / 6.6f, num2, A_2.step);
      scalarKeyFrameAnimation.InsertKeyFrame((A_2.firstBlink + 0.09f) / 6.6f, num, A_2.step);
      scalarKeyFrameAnimation.InsertKeyFrame(A_2.secondBlink / 6.6f, num2, A_2.step);
      scalarKeyFrameAnimation.InsertKeyFrame((A_2.secondBlink + 0.09f) / 6.6f, num, A_2.step);
      scalarKeyFrameAnimation.InsertKeyFrame(1f, num, A_2.step);
      return ComputerMark.Loop<ScalarKeyFrameAnimation>(scalarKeyFrameAnimation);
    }

    // Token: 0x060013B2 RID: 5042 RVA: 0x005A43B4 File Offset: 0x005A43B4
    [CompilerGenerated]
    internal static void <BuildStrokes>g__Morph|34_2(CompositionPathGeometry path, CompositionSpriteShape shape, [TupleElementNames(new string[] { "X", "Y" })] [Nullable(new byte[] { 1, 0 })] ValueTuple<float, float>[] mark, [TupleElementNames(new string[] { "L", "M", "R", "X", "Y" })] [Nullable(new byte[] { 1, 0, 0 })] Func<ValueTuple<float, float, float>, ValueTuple<float, float>> dot, ref ComputerMark.<>c__DisplayClass34_0 A_4, ref ComputerMark.<>c__DisplayClass34_1 A_5)
    {
      PathKeyFrameAnimation pathKeyFrameAnimation = A_4.c.CreatePathKeyFrameAnimation();
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = A_4.c.CreateScalarKeyFrameAnimation();
      for (int i = 0; i < A_5.timeline.Length; i++)
      {
        ValueTuple<float, float, float, float, bool> valueTuple = A_5.timeline[i];
        float num = valueTuple.Item1 / 6.6f;
        ValueTuple<float, float>[] array = (valueTuple.Item5 ? Enumerable.Repeat<ValueTuple<float, float>>(dot(new ValueTuple<float, float, float>(valueTuple.Item2, valueTuple.Item3, valueTuple.Item4)), mark.Length).ToArray<ValueTuple<float, float>>() : mark);
        CompositionEasingFunction compositionEasingFunction = ((i == 0) ? A_5.linear : A_5.easeInOut);
        pathKeyFrameAnimation.InsertKeyFrame(num, ComputerMark.PathOf(array), compositionEasingFunction);
        scalarKeyFrameAnimation.InsertKeyFrame(num, ComputerMark.BaseLineWidth * (valueTuple.Item5 ? 2.3f : 1f), compositionEasingFunction);
      }
      path.StartAnimation("Path", ComputerMark.Loop<PathKeyFrameAnimation>(pathKeyFrameAnimation));
      shape.StartAnimation("StrokeThickness", ComputerMark.Loop<ScalarKeyFrameAnimation>(scalarKeyFrameAnimation));
    }

    // Token: 0x04000913 RID: 2323
    private const float ViewW = 48f;

    // Token: 0x04000914 RID: 2324
    private const float ViewH = 31f;

    // Token: 0x04000915 RID: 2325
    private const float MarkWidth = 24f;

    // Token: 0x04000916 RID: 2326
    private const float StrokeWeight = 1.25f;

    // Token: 0x04000917 RID: 2327
    private const float Cycle = 6.6f;

    // Token: 0x04000918 RID: 2328
    private const float LookStart = 0.6f;

    // Token: 0x04000919 RID: 2329
    private const float LookPeak = 1.15f;

    // Token: 0x0400091A RID: 2330
    private const float SettleStart = 1.6f;

    // Token: 0x0400091B RID: 2331
    private const float SettleEnd = 2.1f;

    // Token: 0x0400091C RID: 2332
    private const float BlinkShut = 0.09f;

    // Token: 0x0400091D RID: 2333
    private const float BlinkGap = 0.18f;

    // Token: 0x0400091E RID: 2334
    private const float DotRise = 9.5f;

    // Token: 0x0400091F RID: 2335
    private const float LookDegrees = -4.5f;

    // Token: 0x04000920 RID: 2336
    [TupleElementNames(new string[] { "X", "Y" })]
    [Nullable(new byte[] { 1, 0 })]
    private static readonly ValueTuple<float, float>[] Zig = new ValueTuple<float, float>[]
    {
      new ValueTuple<float, float>(9.5f, 12.4f),
      new ValueTuple<float, float>(14.5f, 6.5f),
      new ValueTuple<float, float>(24.3f, 23.3f)
    };

    // Token: 0x04000921 RID: 2337
    [TupleElementNames(new string[] { "X", "Y" })]
    [Nullable(new byte[] { 1, 0 })]
    private static readonly ValueTuple<float, float>[] Eye = new ValueTuple<float, float>[]
    {
      new ValueTuple<float, float>(27.1f, 12.4f),
      new ValueTuple<float, float>(32.86f, 6.5f),
      new ValueTuple<float, float>(38.4f, 12.4f)
    };

    // Token: 0x04000922 RID: 2338
    [TupleElementNames(new string[] { "X", "Y" })]
    [Nullable(new byte[] { 1, 0 })]
    private static readonly ValueTuple<float, float>[] Mouth = new ValueTuple<float, float>[]
    {
      new ValueTuple<float, float>(24.3f, 23.3f),
      new ValueTuple<float, float>(33.4f, 23.3f)
    };

    // Token: 0x04000923 RID: 2339
    [TupleElementNames(new string[] { "X", "Y" })]
    [Nullable(new byte[] { 1, 0 })]
    private static readonly ValueTuple<float, float>[] ShutZig = new ValueTuple<float, float>[]
    {
      new ValueTuple<float, float>(9.5f, 9.5f),
      new ValueTuple<float, float>(14.5f, 9.5f),
      new ValueTuple<float, float>(24.3f, 23.3f)
    };

    // Token: 0x04000924 RID: 2340
    [TupleElementNames(new string[] { "X", "Y" })]
    [Nullable(new byte[] { 1, 0 })]
    private static readonly ValueTuple<float, float>[] ShutEye = new ValueTuple<float, float>[]
    {
      new ValueTuple<float, float>(27.1f, 9.5f),
      new ValueTuple<float, float>(38.4f, 9.5f)
    };

    // Token: 0x04000925 RID: 2341
    [TupleElementNames(new string[] { "X", "Y" })]
    [Nullable(0)]
    private static readonly ValueTuple<float, float> Pivot = new ValueTuple<float, float>(24.3f, 23.3f);

    // Token: 0x04000926 RID: 2342
    private static readonly Color Coral = DS.Rgb(0.85, 0.33, 0.24, 1.0);

    // Token: 0x04000927 RID: 2343
    private const string RaisedHandPath = "M8,12 V5.6 a1.3,1.3 0 0 1 2.6,0 V10.5 V3.9 a1.3,1.3 0 0 1 2.6,0 V10.5 V4.6 a1.3,1.3 0 0 1 2.6,0 V11 V7.4 a1.3,1.3 0 0 1 2.6,0 V14.2 C18.4,18.4 15.9,21.5 12,21.5 C9.4,21.5 7.8,20.2 6.4,18 L3.9,13.9 a1.3,1.3 0 0 1 2.1,-1.5 Z";

    // Token: 0x04000928 RID: 2344
    private readonly Grid markHost = new Grid
    {
      Width = 24.0,
      Height = 15.5,
      HorizontalAlignment = HorizontalAlignment.Center,
      VerticalAlignment = VerticalAlignment.Center
    };

    // Token: 0x04000929 RID: 2345
    private readonly Grid attentionHost = new Grid
    {
      Visibility = Visibility.Collapsed
    };

    // Token: 0x0400092A RID: 2346
    private bool? builtLooping;

    // Token: 0x0400092B RID: 2347
    private bool working;

    // Token: 0x0400092C RID: 2348
    [Nullable(2)]
    private string attention;
  }
}
