using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000149 RID: 329
  [NullableContext(1)]
  [Nullable(0)]
  public static class ActivityIndicators
  {
    // Token: 0x06001299 RID: 4761 RVA: 0x00598084 File Offset: 0x00598084
    private static CompositionPropertySet Clock(Compositor c, double period = 3600.0)
    {
      CompositionPropertySet compositionPropertySet = c.CreatePropertySet();
      compositionPropertySet.InsertScalar("T", 0f);
      compositionPropertySet.InsertScalar("Level", 0.15f);
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = c.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(0f, 0f);
      scalarKeyFrameAnimation.InsertKeyFrame(1f, (float)period, c.CreateLinearEasingFunction());
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(period);
      scalarKeyFrameAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
      compositionPropertySet.StartAnimation("T", scalarKeyFrameAnimation);
      return compositionPropertySet;
    }

    // Token: 0x0600129A RID: 4762 RVA: 0x00598100 File Offset: 0x00598100
    private static void Bind(UIElement e, string target, string expr, CompositionPropertySet clock, Compositor c)
    {
      ExpressionAnimation expressionAnimation = c.CreateExpressionAnimation(expr);
      expressionAnimation.SetReferenceParameter("k", clock);
      expressionAnimation.Target = target;
      e.StartAnimation(expressionAnimation);
    }

    // Token: 0x0600129B RID: 4763 RVA: 0x00598130 File Offset: 0x00598130
    [return: TupleElementNames(new string[] { "view", "clock" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    public static ValueTuple<FrameworkElement, CompositionPropertySet> ListeningBars(Color tint)
    {
      Compositor compositor = Ui.Compositor;
      CompositionPropertySet compositionPropertySet = ActivityIndicators.Clock(compositor, 3600.0);
      return new ValueTuple<FrameworkElement, CompositionPropertySet>(ActivityIndicators.Bars(compositor, compositionPropertySet, tint, (int i) => FormattableString.Invariant(FormattableStringFactory.Create("Max(3, (Sin(k.T * 3.6 + {0:0.##}) * 0.5 + 0.5) * 12 * Clamp(k.Level, 0.15, 1) + 3)", new object[] { (double)i * 0.85 })), (int i) => FormattableString.Invariant(FormattableStringFactory.Create("0.7 + 0.3 * (Sin(k.T * 3.6 + {0:0.##}) * 0.5 + 0.5)", new object[] { (double)i * 0.85 }))), compositionPropertySet);
    }

    // Token: 0x0600129C RID: 4764 RVA: 0x005981A0 File Offset: 0x005981A0
    private static FrameworkElement Bars(Compositor c, CompositionPropertySet clock, Color tint, Func<int, string> height, [Nullable(new byte[] { 2, 1 })] Func<int, string> opacity = null)
    {
      Grid grid = new Grid
      {
        Width = 14.0,
        Height = 14.0,
        VerticalAlignment = VerticalAlignment.Center,
        IsHitTestVisible = false
      };
      ContainerVisual containerVisual = c.CreateContainerVisual();
      containerVisual.Size = new Vector2(14f, 14f);
      CompositionColorBrush compositionColorBrush = c.CreateColorBrush(tint);
      for (int i = 0; i < 4; i++)
      {
        ShapeVisual shapeVisual = c.CreateShapeVisual();
        shapeVisual.Size = new Vector2(14f, 14f);
        CompositionRoundedRectangleGeometry compositionRoundedRectangleGeometry = c.CreateRoundedRectangleGeometry();
        compositionRoundedRectangleGeometry.CornerRadius = new Vector2(1f, 1f);
        ExpressionAnimation expressionAnimation = c.CreateExpressionAnimation("Vector2(2, " + height(i) + ")");
        expressionAnimation.SetReferenceParameter("k", clock);
        compositionRoundedRectangleGeometry.StartAnimation("Size", expressionAnimation);
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 2);
        defaultInterpolatedStringHandler.AppendLiteral("Vector2(");
        defaultInterpolatedStringHandler.AppendFormatted<int>(i * 4);
        defaultInterpolatedStringHandler.AppendLiteral(", 7 - (");
        defaultInterpolatedStringHandler.AppendFormatted(height(i));
        defaultInterpolatedStringHandler.AppendLiteral(") / 2)");
        ExpressionAnimation expressionAnimation2 = c.CreateExpressionAnimation(defaultInterpolatedStringHandler.ToStringAndClear());
        expressionAnimation2.SetReferenceParameter("k", clock);
        compositionRoundedRectangleGeometry.StartAnimation("Offset", expressionAnimation2);
        CompositionSpriteShape compositionSpriteShape = c.CreateSpriteShape(compositionRoundedRectangleGeometry);
        compositionSpriteShape.FillBrush = compositionColorBrush;
        shapeVisual.Shapes.Add(compositionSpriteShape);
        if (opacity != null)
        {
          ExpressionAnimation expressionAnimation3 = c.CreateExpressionAnimation(opacity(i));
          expressionAnimation3.SetReferenceParameter("k", clock);
          shapeVisual.StartAnimation("Opacity", expressionAnimation3);
        }
        containerVisual.Children.InsertAtTop(shapeVisual);
      }
      ElementCompositionPreview.SetElementChildVisual(grid, containerVisual);
      return grid;
    }

    // Token: 0x0600129D RID: 4765 RVA: 0x00598360 File Offset: 0x00598360
    public static FrameworkElement ThinkingDots(Color tint, double diameter = 4.0, double spacing = 3.0, double freq = 2.8, double phaseStep = 0.85, double scaleBase = 0.55, double scaleAmp = 0.45, double opBase = 0.4, double opAmp = 0.6, double height = 14.0)
    {
      Compositor compositor = Ui.Compositor;
      CompositionPropertySet compositionPropertySet = ActivityIndicators.Clock(compositor, 3600.0);
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = spacing,
        Height = height,
        VerticalAlignment = VerticalAlignment.Center
      };
      for (int i = 0; i < 3; i++)
      {
        Grid grid = new Grid
        {
          Width = diameter,
          Height = diameter,
          VerticalAlignment = VerticalAlignment.Center
        };
        Grid grid2 = Ui.GlowSpot(tint, 0.55, diameter + 6.0, 0.3).Pad(-3.0);
        Ellipse ellipse = new Ellipse
        {
          Width = diameter,
          Height = diameter,
          Fill = Ui.Brush(tint)
        };
        grid.Children.Add(grid2);
        grid.Children.Add(ellipse);
        ellipse.CenterPoint = new Vector3((float)diameter / 2f, (float)diameter / 2f, 0f);
        string text = FormattableString.Invariant(FormattableStringFactory.Create("((Sin(k.T * {0:0.##} + {1:0.##}) + 1) * 0.5)", new object[]
        {
          freq,
          (double)i * phaseStep
        }));
        string text2 = FormattableString.Invariant(FormattableStringFactory.Create("({0:0.##} + {1:0.##} * {2})", new object[] { opBase, opAmp, text }));
        ActivityIndicators.Bind(ellipse, "Scale", FormattableString.Invariant(FormattableStringFactory.Create("Vector3({0:0.##} + {1:0.##} * {2}, {3:0.##} + {4:0.##} * {5}, 1)", new object[] { scaleBase, scaleAmp, text, scaleBase, scaleAmp, text })), compositionPropertySet, compositor);
        ActivityIndicators.Bind(grid, "Opacity", text2, compositionPropertySet, compositor);
        ActivityIndicators.Bind(grid2, "Opacity", text2, compositionPropertySet, compositor);
        stackPanel.Children.Add(grid);
      }
      return stackPanel;
    }

    // Token: 0x0600129E RID: 4766 RVA: 0x00598548 File Offset: 0x00598548
    public static FrameworkElement AgentDots(Color tint)
    {
      return ActivityIndicators.ThinkingDots(tint, 5.0, 4.0, 3.4, 1.05, 0.6, 0.6, 0.3, 0.7, 18.0);
    }

    // Token: 0x0600129F RID: 4767 RVA: 0x005985AC File Offset: 0x005985AC
    public static FrameworkElement SpeakingBars(Color tint)
    {
      Compositor compositor = Ui.Compositor;
      return ActivityIndicators.Bars(compositor, ActivityIndicators.Clock(compositor, 3600.0), tint, (int i) => FormattableString.Invariant(FormattableStringFactory.Create("Max(3, (Sin(k.T * 4.2 + {0:0.##}) * 0.5 + 0.5) * 11 + 3)", new object[] { (double)i * 1.7 })), null);
    }

    // Token: 0x060012A0 RID: 4768 RVA: 0x005985E8 File Offset: 0x005985E8
    public static FrameworkElement DictationDashes(Color tint, bool recording)
    {
      Compositor compositor = Ui.Compositor;
      CompositionPropertySet compositionPropertySet = ActivityIndicators.Clock(compositor, 3600.0);
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 2.5,
        Height = 14.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      for (int i = 0; i < 4; i++)
      {
        Rectangle rectangle = new Rectangle
        {
          Width = 5.0,
          Height = 2.5,
          RadiusX = 1.0,
          RadiusY = 1.0,
          Fill = Ui.Brush(tint),
          VerticalAlignment = VerticalAlignment.Center
        };
        rectangle.CenterPoint = new Vector3(0f, 1.25f, 0f);
        if (recording)
        {
          rectangle.Opacity = 0.9;
          if (i > 0)
          {
            float num = (float)i / 4f;
            float num2 = 0.099999994f;
            ScalarKeyFrameAnimation scalarKeyFrameAnimation = compositor.CreateScalarKeyFrameAnimation();
            LinearEasingFunction linearEasingFunction = compositor.CreateLinearEasingFunction();
            CubicBezierEasingFunction cubicBezierEasingFunction = Easing.EaseOut(compositor);
            float num3 = Math.Min(1f, num + num2);
            scalarKeyFrameAnimation.InsertKeyFrame(0f, 0.9f);
            scalarKeyFrameAnimation.InsertKeyFrame(num2, 0f, cubicBezierEasingFunction);
            scalarKeyFrameAnimation.InsertKeyFrame(num, 0f, linearEasingFunction);
            scalarKeyFrameAnimation.InsertKeyFrame(num3, 0.9f, cubicBezierEasingFunction);
            scalarKeyFrameAnimation.InsertKeyFrame(1f, 0.9f, linearEasingFunction);
            scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(1.6);
            scalarKeyFrameAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
            scalarKeyFrameAnimation.Target = "Opacity";
            rectangle.StartAnimation(scalarKeyFrameAnimation);
            Vector3KeyFrameAnimation vector3KeyFrameAnimation = compositor.CreateVector3KeyFrameAnimation();
            vector3KeyFrameAnimation.InsertKeyFrame(0f, Vector3.One);
            vector3KeyFrameAnimation.InsertKeyFrame(num2, new Vector3(0.4f, 0.4f, 1f), cubicBezierEasingFunction);
            vector3KeyFrameAnimation.InsertKeyFrame(num, new Vector3(0.4f, 0.4f, 1f), linearEasingFunction);
            vector3KeyFrameAnimation.InsertKeyFrame(num3, Vector3.One, cubicBezierEasingFunction);
            vector3KeyFrameAnimation.InsertKeyFrame(1f, Vector3.One, linearEasingFunction);
            vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(1.6);
            vector3KeyFrameAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
            vector3KeyFrameAnimation.Target = "Scale";
            rectangle.StartAnimation(vector3KeyFrameAnimation);
          }
        }
        else
        {
          string text = FormattableString.Invariant(FormattableStringFactory.Create("(Sin(k.T * 3.6 - {0:0.###}) * 0.5 + 0.5)", new object[] { (double)i * 3.141592653589793 / 2.0 }));
          ActivityIndicators.Bind(rectangle, "Opacity", "0.25 + 0.75 * " + text, compositionPropertySet, compositor);
          rectangle.CenterPoint = new Vector3(2.5f, 1.25f, 0f);
          ActivityIndicators.Bind(rectangle, "Scale", "Vector3(1, 0.85 + 0.45 * " + text + ", 1)", compositionPropertySet, compositor);
        }
        stackPanel.Children.Add(rectangle);
      }
      return stackPanel;
    }

    // Token: 0x060012A1 RID: 4769 RVA: 0x005988E8 File Offset: 0x005988E8
    public static FrameworkElement DockedCursor(Color theme)
    {
      Compositor compositor = Ui.Compositor;
      CompositionPropertySet compositionPropertySet = ActivityIndicators.Clock(compositor, 3600.0);
      Grid grid = new Grid
      {
        Width = 14.0,
        Height = 14.0,
        VerticalAlignment = VerticalAlignment.Center,
        RenderTransform = new RotateTransform
        {
          Angle = -35.0,
          CenterX = 7.0,
          CenterY = 7.0
        }
      };
      Path path = IntroMark.Cursor(14.0, theme, 0.0);
      Grid grid2 = new Grid
      {
        Width = 14.0,
        Height = 14.0,
        IsHitTestVisible = false
      };
      DropShadow dropShadow = compositor.CreateDropShadow();
      dropShadow.Color = theme;
      dropShadow.Opacity = 0.85f;
      dropShadow.Mask = path.GetAlphaMask();
      ExpressionAnimation expressionAnimation = compositor.CreateExpressionAnimation("2 * (3 + 3 * ((Sin(k.T * 2.4) + 1) * 0.5))");
      expressionAnimation.SetReferenceParameter("k", compositionPropertySet);
      dropShadow.StartAnimation("BlurRadius", expressionAnimation);
      SpriteVisual spriteVisual = compositor.CreateSpriteVisual();
      spriteVisual.Size = new Vector2(14f, 14f);
      spriteVisual.Shadow = dropShadow;
      ElementCompositionPreview.SetElementChildVisual(grid2, spriteVisual);
      grid.Children.Add(grid2);
      grid.Children.Add(path);
      return grid;
    }
  }
}
