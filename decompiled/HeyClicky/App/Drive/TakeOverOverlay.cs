using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Interop;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Composition;
using Microsoft.Graphics.DirectX;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.Graphics.DirectX;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Drive
{
  // Token: 0x020002C7 RID: 711
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class TakeOverOverlay
  {
    // Token: 0x17000556 RID: 1366
    // (get) Token: 0x06001DFB RID: 7675 RVA: 0x006041F8 File Offset: 0x006041F8
    public static double PillWidth
    {
      get
      {
        return Math.Max(360.0, 460.0);
      }
    }

    // Token: 0x17000557 RID: 1367
    // (get) Token: 0x06001DFC RID: 7676 RVA: 0x00604214 File Offset: 0x00604214
    public static double PillHeight
    {
      get
      {
        return 62.0;
      }
    }

    // Token: 0x17000558 RID: 1368
    // (get) Token: 0x06001DFD RID: 7677 RVA: 0x00604220 File Offset: 0x00604220
    public bool IsShown
    {
      get
      {
        OverlayWindow overlayWindow = this.window;
        return overlayWindow != null && overlayWindow.IsShown;
      }
    }

    // Token: 0x06001DFE RID: 7678 RVA: 0x00604234 File Offset: 0x00604234
    public void Show(Displays.Monitor display, Color accent, string goal)
    {
      this.Hide(false);
      DriveNotchOwnership.SetDrivingShown(true);
      this.generation++;
      this.reduceMotion = Anim.ReduceMotion;
      if (this.window == null)
      {
        this.window = new OverlayWindow("HeyClicky drive", true, true, false, true, false, true);
        NonRudeWindow.Mark(this.window.Hwnd);
      }
      Native.RECT bounds = display.Bounds;
      this.window.PlacePx(bounds.Left, bounds.Top, bounds.Width, bounds.Height);
      double scale = display.Scale;
      double num = (double)bounds.Width / scale;
      double num2 = (double)bounds.Height / scale;
      Grid grid = new Grid
      {
        Width = num,
        Height = num2,
        IsHitTestVisible = false
      };
      this.window.Root.Children.Clear();
      this.window.Root.Children.Add(grid);
      this.AddEdgeGlow(grid, num, num2, accent, scale);
      this.AddPill(grid, num, accent, goal, scale);
      this.window.ShowNoActivate();
    }

    // Token: 0x06001DFF RID: 7679 RVA: 0x0060434C File Offset: 0x0060434C
    public void SetStatus(string text)
    {
      this.statusTextBehindHint = text;
      if (this.isShowingMouseHint)
      {
        return;
      }
      if (this.status == null || this.statusOutgoing == null || this.status.Text == text)
      {
        return;
      }
      this.statusOutgoing.Text = this.status.Text;
      this.statusOutgoing.Opacity = 1.0;
      this.status.Text = text;
      this.status.Opacity = 0.0;
      Anim.Opacity(this.statusOutgoing, 0f, 0.25, Easing.EaseInOut(Ui.Compositor), 0.0);
      Anim.Opacity(this.status, 1f, 0.25, Easing.EaseInOut(Ui.Compositor), 0.0);
    }

    // Token: 0x06001E00 RID: 7680 RVA: 0x00604430 File Offset: 0x00604430
    public void SetWaitingForUserMouse(bool waiting, bool nudge)
    {
      if (this.status == null || this.hint == null || this.keyCapElement == null || this.pill == null)
      {
        return;
      }
      if (waiting != this.isShowingMouseHint)
      {
        this.isShowingMouseHint = waiting;
        this.status.Text = (waiting ? "You moved the mouse. I'm still driving." : this.statusTextBehindHint);
        this.status.Foreground = Ui.Brush(waiting ? TakeOverOverlay.MouseHintColor : DS.White(0.62));
        this.status.FontWeight = (waiting ? FontWeights.SemiBold : FontWeights.Normal);
        this.hint.Text = (waiting ? "to take over" : "to stop");
        this.hint.Foreground = Ui.Brush(DS.White(waiting ? 0.9 : 0.5));
        this.hint.FontWeight = (waiting ? FontWeights.SemiBold : FontWeights.Normal);
        Anim.ScaleSpring(this.keyCapElement, waiting ? 1.12f : 1f, new SwiftSpring(0.3, 0.6), new Vector3?(new Vector3((float)(this.keyCapElement.ActualWidth / 2.0), (float)(this.keyCapElement.ActualHeight / 2.0), 0f)));
      }
      if (waiting && nudge && !this.reduceMotion)
      {
        Compositor compositor = Ui.Compositor;
        Vector3KeyFrameAnimation vector3KeyFrameAnimation = compositor.CreateVector3KeyFrameAnimation();
        float[] array = new float[] { 0f, 5f, -5f, 4f, -4f, 2f, 0f };
        for (int i = 0; i < array.Length; i++)
        {
          vector3KeyFrameAnimation.InsertKeyFrame((float)i / (float)(array.Length - 1), new Vector3(array[i], 0f, 0f), compositor.CreateLinearEasingFunction());
        }
        vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(0.42);
        vector3KeyFrameAnimation.Target = "Translation";
        this.pill.StartAnimation(vector3KeyFrameAnimation);
      }
    }

    // Token: 0x06001E01 RID: 7681 RVA: 0x0060462C File Offset: 0x0060462C
    public void SetActing(bool acting)
    {
      if (this.shimmer == null || this.shimmerBrush == null || this.cursor == null || this.glowHolder == null)
      {
        return;
      }
      Compositor compositor = Ui.Compositor;
      if (acting)
      {
        if (!this.reduceMotion)
        {
          Vector3KeyFrameAnimation vector3KeyFrameAnimation = compositor.CreateVector3KeyFrameAnimation();
          vector3KeyFrameAnimation.InsertKeyFrame(0f, Vector3.One);
          vector3KeyFrameAnimation.InsertKeyFrame(0.35f, new Vector3(1.22f, 1.22f, 1f));
          vector3KeyFrameAnimation.InsertKeyFrame(0.7f, new Vector3(0.94f, 0.94f, 1f));
          vector3KeyFrameAnimation.InsertKeyFrame(1f, Vector3.One);
          vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(0.45);
          vector3KeyFrameAnimation.Target = "Scale";
          this.cursor.StartAnimation(vector3KeyFrameAnimation);
          CompositionColorGradientStopCollection colorStops = this.shimmerBrush.ColorStops;
          ValueTuple<float, float>[] array = new ValueTuple<float, float>[]
          {
            new ValueTuple<float, float>(-0.4f, 1f),
            new ValueTuple<float, float>(-0.2f, 1.2f),
            new ValueTuple<float, float>(0f, 1.4f)
          };
          int num = 0;
          while (num < colorStops.Count && num < array.Length)
          {
            ScalarKeyFrameAnimation scalarKeyFrameAnimation = compositor.CreateScalarKeyFrameAnimation();
            scalarKeyFrameAnimation.InsertKeyFrame(0f, array[num].Item1, compositor.CreateLinearEasingFunction());
            scalarKeyFrameAnimation.InsertKeyFrame(1f, array[num].Item2, compositor.CreateLinearEasingFunction());
            scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(1.1);
            scalarKeyFrameAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
            colorStops[num].StartAnimation("Offset", scalarKeyFrameAnimation);
            num++;
          }
        }
        TakeOverOverlay.FadeVisual(this.shimmer, 1f, 0.25);
        TakeOverOverlay.FadeVisual(this.glowHolder, 1f, 0.25);
        return;
      }
      CompositionColorGradientStopCollection colorStops2 = this.shimmerBrush.ColorStops;
      float[] array2 = new float[3];
      array2[0] = -0.4f;
      array2[1] = -0.2f;
      float[] array3 = array2;
      int num2 = 0;
      while (num2 < colorStops2.Count && num2 < array3.Length)
      {
        colorStops2[num2].StopAnimation("Offset");
        colorStops2[num2].Offset = array3[num2];
        num2++;
      }
      TakeOverOverlay.FadeVisual(this.shimmer, 0f, 0.25);
      TakeOverOverlay.FadeVisual(this.glowHolder, 0.82f, 0.25);
    }

    // Token: 0x06001E02 RID: 7682 RVA: 0x006048BC File Offset: 0x006048BC
    public void Hide(bool animated = true)
    {
      TakeOverOverlay.<>c__DisplayClass27_0 CS$<>8__locals1 = new TakeOverOverlay.<>c__DisplayClass27_0();
      CS$<>8__locals1.<>4__this = this;
      DriveNotchOwnership.SetDrivingShown(false);
      if (this.window == null || !this.window.IsShown)
      {
        return;
      }
      TakeOverOverlay.<>c__DisplayClass27_0 CS$<>8__locals2 = CS$<>8__locals1;
      int num = this.generation + 1;
      this.generation = num;
      CS$<>8__locals2.shownGeneration = num;
      CS$<>8__locals1.closing = this.window;
      Grid grid = this.pill;
      ContainerVisual containerVisual = this.glowHolder;
      this.isShowingMouseHint = false;
      this.pill = null;
      this.status = null;
      this.statusOutgoing = null;
      this.hint = null;
      this.keyCapElement = null;
      this.cursor = null;
      this.shimmer = null;
      this.shimmerBrush = null;
      this.glowHolder = null;
      if (!animated || grid == null)
      {
        CS$<>8__locals1.closing.HideWindow();
        CS$<>8__locals1.closing.Root.Children.Clear();
        return;
      }
      Compositor compositor = Ui.Compositor;
      if (containerVisual != null)
      {
        TakeOverOverlay.FadeVisual(containerVisual, 0f, 0.3);
      }
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = compositor.CreateVector3KeyFrameAnimation();
      vector3KeyFrameAnimation.InsertKeyFrame(1f, new Vector3(0.35f, 0.35f, 1f), compositor.CreateCubicBezierEasingFunction(new Vector2(0.42f, 0f), new Vector2(1f, 1f)));
      vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(0.28);
      vector3KeyFrameAnimation.Target = "Scale";
      grid.StartAnimation(vector3KeyFrameAnimation);
      Anim.Opacity(grid, 0f, 0.3, null, 0.0);
      DispatcherQueueTimer dispatcherQueueTimer = CS$<>8__locals1.closing.DispatcherQueue.CreateTimer();
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(0.3);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        if (CS$<>8__locals1.<>4__this.generation != CS$<>8__locals1.shownGeneration)
        {
          return;
        }
        CS$<>8__locals1.closing.HideWindow();
        CS$<>8__locals1.closing.Root.Children.Clear();
      };
      dispatcherQueueTimer.Start();
    }

    // Token: 0x06001E03 RID: 7683 RVA: 0x00604A88 File Offset: 0x00604A88
    private void AddEdgeGlow(Grid root, double width, double height, Color accent, double scale)
    {
      Compositor compositor = Ui.Compositor;
      Grid grid = new Grid
      {
        Width = width,
        Height = height,
        IsHitTestVisible = false
      };
      root.Children.Add(grid);
      Vector2 vector = new Vector2((float)width, (float)height);
      CompositionGraphicsDevice compositionGraphicsDevice = CanvasComposition.CreateCompositionGraphicsDevice(compositor, CanvasDevice.GetSharedDevice());
      double num = Math.Min(130.0, height * 0.12);
      ValueTuple<byte[], int, int> valueTuple = DriveArt.EdgeGlowMask(width, height, num);
      byte[] item = valueTuple.Item1;
      int item2 = valueTuple.Item2;
      int item3 = valueTuple.Item3;
      CompositionDrawingSurface compositionDrawingSurface = TakeOverOverlay.BitmapSurface(compositionGraphicsDevice, item, item2, item3);
      CompositionSurfaceBrush compositionSurfaceBrush = compositor.CreateSurfaceBrush(compositionDrawingSurface);
      compositionSurfaceBrush.Stretch = CompositionStretch.Fill;
      double num2 = Math.Sqrt(width * width + height * height);
      CompositionDrawingSurface compositionDrawingSurface2 = TakeOverOverlay.BitmapSurface(compositionGraphicsDevice, DriveArt.ConicGradient(512, DriveArt.CloudColors(accent)), 512, 512);
      CompositionSurfaceBrush compositionSurfaceBrush2 = compositor.CreateSurfaceBrush(compositionDrawingSurface2);
      compositionSurfaceBrush2.Stretch = CompositionStretch.Fill;
      ContainerVisual containerVisual = compositor.CreateContainerVisual();
      containerVisual.Size = vector;
      ContainerVisual containerVisual2 = compositor.CreateContainerVisual();
      containerVisual2.Size = vector;
      containerVisual2.CenterPoint = new Vector3(vector / 2f, 0f);
      SpriteVisual spriteVisual = compositor.CreateSpriteVisual();
      spriteVisual.Size = new Vector2((float)num2);
      spriteVisual.Offset = new Vector3((float)((width - num2) / 2.0), (float)((height - num2) / 2.0), 0f);
      spriteVisual.CenterPoint = new Vector3((float)num2 / 2f, (float)num2 / 2f, 0f);
      spriteVisual.Brush = compositionSurfaceBrush2;
      containerVisual2.Children.InsertAtTop(spriteVisual);
      containerVisual.Children.InsertAtTop(containerVisual2);
      CompositionVisualSurface compositionVisualSurface = compositor.CreateVisualSurface();
      compositionVisualSurface.SourceVisual = containerVisual;
      compositionVisualSurface.SourceSize = vector;
      CompositionSurfaceBrush compositionSurfaceBrush3 = compositor.CreateSurfaceBrush(compositionVisualSurface);
      compositionSurfaceBrush3.Stretch = CompositionStretch.None;
      CompositionMaskBrush compositionMaskBrush = compositor.CreateMaskBrush();
      compositionMaskBrush.Source = compositionSurfaceBrush3;
      compositionMaskBrush.Mask = compositionSurfaceBrush;
      SpriteVisual spriteVisual2 = compositor.CreateSpriteVisual();
      spriteVisual2.Size = vector;
      spriteVisual2.Brush = compositionMaskBrush;
      ContainerVisual containerVisual3 = compositor.CreateContainerVisual();
      containerVisual3.Size = vector;
      containerVisual3.Children.InsertAtTop(spriteVisual2);
      ContainerVisual containerVisual4 = compositor.CreateContainerVisual();
      containerVisual4.Size = vector;
      containerVisual4.Opacity = 0f;
      containerVisual4.Children.InsertAtTop(containerVisual3);
      ElementCompositionPreview.SetElementChildVisual(grid, containerVisual4);
      this.glowHolder = containerVisual4;
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = compositor.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(0f, 0f);
      scalarKeyFrameAnimation.InsertKeyFrame(1f, 0.82f, compositor.CreateCubicBezierEasingFunction(new Vector2(0f, 0f), new Vector2(0.58f, 1f)));
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(0.7);
      containerVisual4.StartAnimation("Opacity", scalarKeyFrameAnimation);
      if (this.reduceMotion)
      {
        return;
      }
      ScalarKeyFrameAnimation scalarKeyFrameAnimation2 = compositor.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation2.InsertKeyFrame(0f, -180f);
      scalarKeyFrameAnimation2.InsertKeyFrame(1f, 0f, compositor.CreateCubicBezierEasingFunction(new Vector2(0.2f, 0.7f), new Vector2(0.3f, 1f)));
      scalarKeyFrameAnimation2.Duration = TimeSpan.FromSeconds(1.4);
      containerVisual2.StartAnimation("RotationAngleInDegrees", scalarKeyFrameAnimation2);
      ScalarKeyFrameAnimation scalarKeyFrameAnimation3 = compositor.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation3.InsertKeyFrame(0f, 0f, compositor.CreateLinearEasingFunction());
      scalarKeyFrameAnimation3.InsertKeyFrame(1f, 360f, compositor.CreateLinearEasingFunction());
      scalarKeyFrameAnimation3.Duration = TimeSpan.FromSeconds(16L);
      scalarKeyFrameAnimation3.DelayTime = TimeSpan.FromSeconds(1.4);
      scalarKeyFrameAnimation3.IterationBehavior = AnimationIterationBehavior.Forever;
      spriteVisual.StartAnimation("RotationAngleInDegrees", scalarKeyFrameAnimation3);
      ScalarKeyFrameAnimation scalarKeyFrameAnimation4 = compositor.CreateScalarKeyFrameAnimation();
      CubicBezierEasingFunction cubicBezierEasingFunction = compositor.CreateCubicBezierEasingFunction(new Vector2(0.42f, 0f), new Vector2(0.58f, 1f));
      scalarKeyFrameAnimation4.InsertKeyFrame(0f, 0.72f);
      scalarKeyFrameAnimation4.InsertKeyFrame(0.5f, 1f, cubicBezierEasingFunction);
      scalarKeyFrameAnimation4.InsertKeyFrame(1f, 0.72f, cubicBezierEasingFunction);
      scalarKeyFrameAnimation4.Duration = TimeSpan.FromSeconds(5.2);
      scalarKeyFrameAnimation4.IterationBehavior = AnimationIterationBehavior.Forever;
      containerVisual3.StartAnimation("Opacity", scalarKeyFrameAnimation4);
    }

    // Token: 0x06001E04 RID: 7684 RVA: 0x00604EE0 File Offset: 0x00604EE0
    private static CompositionDrawingSurface BitmapSurface(CompositionGraphicsDevice graphics, byte[] bgra, int width, int height)
    {
      CompositionDrawingSurface compositionDrawingSurface = graphics.CreateDrawingSurface(new Size((float)width, (float)height), Microsoft.Graphics.DirectX.DirectXPixelFormat.B8G8R8A8UIntNormalized, Microsoft.Graphics.DirectX.DirectXAlphaMode.Premultiplied);
      CompositionDrawingSurface compositionDrawingSurface2;
      using (CanvasBitmap canvasBitmap = CanvasBitmap.CreateFromBytes(CanvasDevice.GetSharedDevice(), bgra, width, height, Windows.Graphics.DirectX.DirectXPixelFormat.B8G8R8A8UIntNormalized, 96f, CanvasAlphaMode.Premultiplied))
      {
        using (CanvasDrawingSession canvasDrawingSession = CanvasComposition.CreateDrawingSession(compositionDrawingSurface))
        {
          canvasDrawingSession.Clear(Color.FromArgb(0, 0, 0, 0));
          canvasDrawingSession.DrawImage(canvasBitmap, new Rect(0f, 0f, (float)width, (float)height));
        }
        compositionDrawingSurface2 = compositionDrawingSurface;
      }
      return compositionDrawingSurface2;
    }

    // Token: 0x06001E05 RID: 7685 RVA: 0x00604F80 File Offset: 0x00604F80
    private void AddPill(Grid root, double screenWidth, Color accent, string statusText, double scale)
    {
      Compositor compositor = Ui.Compositor;
      double pillWidth = TakeOverOverlay.PillWidth;
      double pillHeight = TakeOverOverlay.PillHeight;
      double num = 10.0;
      Grid grid = new Grid
      {
        Width = pillWidth,
        Height = pillHeight,
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top,
        Margin = new Thickness((screenWidth - pillWidth) / 2.0, 0.0, 0.0, 0.0),
        IsHitTestVisible = false
      };
      Canvas canvas = new Canvas
      {
        Width = pillWidth,
        Height = pillHeight
      };
      grid.Children.Add(canvas);
      root.Children.Add(grid);
      CompositionPath compositionPath = new CompositionPath(DriveArt.NotchAttachedPath(CanvasDevice.GetSharedDevice(), pillWidth, pillHeight, 24.0));
      CompositionPathGeometry compositionPathGeometry = compositor.CreatePathGeometry(compositionPath);
      CompositionSpriteShape compositionSpriteShape = compositor.CreateSpriteShape(compositionPathGeometry);
      compositionSpriteShape.FillBrush = compositor.CreateColorBrush(Colors.Black);
      ShapeVisual shapeVisual = compositor.CreateShapeVisual();
      shapeVisual.Size = new Vector2((float)pillWidth, (float)pillHeight);
      shapeVisual.Shapes.Add(compositionSpriteShape);
      CompositionSpriteShape compositionSpriteShape2 = compositor.CreateSpriteShape(compositor.CreatePathGeometry(compositionPath));
      compositionSpriteShape2.FillBrush = compositor.CreateColorBrush(Colors.Black);
      ShapeVisual shapeVisual2 = compositor.CreateShapeVisual();
      shapeVisual2.Size = shapeVisual.Size;
      shapeVisual2.Shapes.Add(compositionSpriteShape2);
      CompositionVisualSurface compositionVisualSurface = compositor.CreateVisualSurface();
      compositionVisualSurface.SourceVisual = shapeVisual2;
      compositionVisualSurface.SourceSize = shapeVisual.Size;
      CompositionSurfaceBrush compositionSurfaceBrush = compositor.CreateSurfaceBrush(compositionVisualSurface);
      compositionSurfaceBrush.Stretch = CompositionStretch.None;
      DropShadow dropShadow = compositor.CreateDropShadow();
      dropShadow.Color = DS.Black(0.4);
      dropShadow.BlurRadius = 36f;
      dropShadow.Offset = new Vector3(0f, 6f, 0f);
      dropShadow.Mask = compositionSurfaceBrush;
      SpriteVisual spriteVisual = compositor.CreateSpriteVisual();
      spriteVisual.Size = shapeVisual.Size;
      spriteVisual.Shadow = dropShadow;
      CompositionSpriteShape compositionSpriteShape3 = compositor.CreateSpriteShape(compositor.CreatePathGeometry(compositionPath));
      compositionSpriteShape3.StrokeBrush = compositor.CreateColorBrush(accent.WithAlpha(0.35));
      compositionSpriteShape3.StrokeThickness = 1f;
      ShapeVisual shapeVisual3 = compositor.CreateShapeVisual();
      shapeVisual3.Size = shapeVisual.Size;
      shapeVisual3.Shapes.Add(compositionSpriteShape3);
      shapeVisual3.Clip = compositor.CreateInsetClip(0f, (float)(pillHeight - 37.8), 0f, 0f);
      ContainerVisual containerVisual = compositor.CreateContainerVisual();
      containerVisual.Size = shapeVisual.Size;
      containerVisual.Children.InsertAtTop(spriteVisual);
      containerVisual.Children.InsertAtTop(shapeVisual);
      containerVisual.Children.InsertAtTop(shapeVisual3);
      Grid grid2 = new Grid
      {
        Width = pillWidth,
        Height = pillHeight
      };
      ElementCompositionPreview.SetElementChildVisual(grid2, containerVisual);
      canvas.Children.Add(grid2);
      double num2 = pillHeight - 27.0;
      double num3 = num + 22.0;
      Image image = new Image
      {
        Width = 44.0,
        Height = 44.0,
        Margin = new Thickness(-4.0),
        Source = DriveArt.DriveChip(28.0, 44.0, scale, 11),
        Stretch = Stretch.Fill
      };
      Grid grid3 = new Grid
      {
        Width = 36.0,
        Height = 36.0
      };
      grid3.Children.Add(image);
      Canvas.SetLeft(grid3, num3 + 14.0 - 18.0);
      Canvas.SetTop(grid3, num2 - 18.0);
      image.CenterPoint = new Vector3(22f, 22f, 0f);
      canvas.Children.Add(grid3);
      if (!this.reduceMotion)
      {
        Anim.LoopVector3(grid3, "Translation", 2.8, (double t) => new Vector3(0f, (float)(1.5 * Math.Cos(3.141592653589793 * t / 1.4)), 0f), 64, 0.0);
      }
      double num4 = num3 + 28.0 + 12.0;
      double num5 = pillWidth - num4 - num3 - 30.0 - 92.0 - 14.0;
      TextBlock textBlock = TakeOverOverlay.Text("HeyClicky is driving", 13.0, FontWeights.SemiBold, Colors.White, num5);
      TakeOverOverlay.Place(canvas, textBlock, num4, pillHeight - 28.0 - 18.0);
      Grid grid4 = new Grid
      {
        Width = num5,
        Height = 16.0
      };
      TextBlock textBlock2 = TakeOverOverlay.Text("", 11.0, FontWeights.Normal, DS.White(0.62), num5);
      textBlock2.Opacity = 0.0;
      TextBlock textBlock3 = TakeOverOverlay.Text(statusText, 11.0, FontWeights.Normal, DS.White(0.62), num5);
      grid4.Children.Add(textBlock2);
      grid4.Children.Add(textBlock3);
      TakeOverOverlay.Place(canvas, grid4, num4, pillHeight - 10.0 - 16.0);
      Border border = new Border
      {
        Width = 30.0,
        Height = 20.0,
        CornerRadius = new CornerRadius(5.0),
        Background = Ui.Brush(DS.White(0.9)),
        Child = new TextBlock
        {
          Text = "esc",
          FontSize = 10.5,
          FontWeight = FontWeights.SemiBold,
          Foreground = Ui.Brush(DS.Black(0.8)),
          FontFamily = DS.Fonts.For(10.5),
          IsTextScaleFactorEnabled = false,
          TextAlignment = TextAlignment.Center,
          HorizontalAlignment = HorizontalAlignment.Stretch,
          VerticalAlignment = VerticalAlignment.Top,
          Margin = new Thickness(0.0, 3.0, 0.0, 0.0),
          Height = 14.0
        }
      };
      TakeOverOverlay.Place(canvas, border, pillWidth - num3 - 92.0 - 30.0, pillHeight - 17.0 - 20.0);
      TextBlock textBlock4 = TakeOverOverlay.Text("to stop", 11.0, FontWeights.Normal, DS.White(0.5), 92.0);
      TakeOverOverlay.Place(canvas, textBlock4, pillWidth - num3 - 92.0 + 6.0, pillHeight - 19.0 - 16.0);
      SpriteVisual spriteVisual2 = compositor.CreateSpriteVisual();
      spriteVisual2.Size = new Vector2((float)(pillWidth - num3 * 2.0), 1.5f);
      spriteVisual2.Offset = new Vector3((float)num3, (float)(pillHeight - 5.0 - 1.5), 0f);
      CompositionLinearGradientBrush compositionLinearGradientBrush = compositor.CreateLinearGradientBrush();
      compositionLinearGradientBrush.StartPoint = new Vector2(0f, 0.5f);
      compositionLinearGradientBrush.EndPoint = new Vector2(1f, 0.5f);
      compositionLinearGradientBrush.ColorStops.Add(compositor.CreateColorGradientStop(-0.4f, accent.WithAlpha(0.0)));
      compositionLinearGradientBrush.ColorStops.Add(compositor.CreateColorGradientStop(-0.2f, DS.White(0.95)));
      compositionLinearGradientBrush.ColorStops.Add(compositor.CreateColorGradientStop(0f, accent.WithAlpha(0.0)));
      spriteVisual2.Brush = compositionLinearGradientBrush;
      spriteVisual2.Opacity = 0f;
      Grid grid5 = new Grid
      {
        Width = pillWidth,
        Height = pillHeight,
        IsHitTestVisible = false
      };
      ElementCompositionPreview.SetElementChildVisual(grid5, spriteVisual2);
      canvas.Children.Add(grid5);
      this.pill = grid;
      this.status = textBlock3;
      this.statusOutgoing = textBlock2;
      this.hint = textBlock4;
      this.keyCapElement = border;
      this.statusTextBehindHint = statusText;
      this.isShowingMouseHint = false;
      this.cursor = image;
      this.shimmer = spriteVisual2;
      this.shimmerBrush = compositionLinearGradientBrush;
      grid.CenterPoint = new Vector3((float)pillWidth / 2f, 0f, 0f);
      if (this.reduceMotion)
      {
        grid.Opacity = 0.0;
        Anim.Opacity(grid, 1f, 0.25, null, 0.0);
        return;
      }
      grid.Scale = Vector3.One;
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = SpringCurve.Scale(compositor, TakeOverOverlay.GrowSpring, new Vector3((float)(60.0 / pillWidth), (float)(8.0 / pillHeight), 1f), Vector3.One);
      vector3KeyFrameAnimation.Target = "Scale";
      grid.StartAnimation(vector3KeyFrameAnimation);
      UIElement[] array = new UIElement[] { grid3, textBlock, grid4, border, textBlock4 };
      for (int i = 0; i < array.Length; i++)
      {
        array[i].Opacity = 0.0;
        Anim.Opacity(array[i], 1f, 0.22, compositor.CreateLinearEasingFunction(), 0.16 + (double)i * 0.035);
      }
    }

    // Token: 0x06001E06 RID: 7686 RVA: 0x0060594C File Offset: 0x0060594C
    private static TextBlock Text(string text, double size, FontWeight weight, Color color, double width)
    {
      TextBlock textBlock = Ui.Text(text, size, weight, color, TextAlignment.Left, 0, false, false);
      textBlock.Width = width;
      textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
      textBlock.TextWrapping = TextWrapping.NoWrap;
      return textBlock;
    }

    // Token: 0x06001E07 RID: 7687 RVA: 0x00605974 File Offset: 0x00605974
    private static void Place(Canvas canvas, FrameworkElement element, double x, double y)
    {
      Canvas.SetLeft(element, x);
      Canvas.SetTop(element, y);
      canvas.Children.Add(element);
    }

    // Token: 0x06001E08 RID: 7688 RVA: 0x00605990 File Offset: 0x00605990
    private static void FadeVisual(Visual visual, float to, double seconds)
    {
      Compositor compositor = visual.Compositor;
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = compositor.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(1f, to, compositor.CreateCubicBezierEasingFunction(new Vector2(0.25f, 0.1f), new Vector2(0.25f, 1f)));
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(seconds);
      visual.StartAnimation("Opacity", scalarKeyFrameAnimation);
    }

    // Token: 0x04000E64 RID: 3684
    [Nullable(2)]
    private OverlayWindow window;

    // Token: 0x04000E65 RID: 3685
    [Nullable(2)]
    private Grid pill;

    // Token: 0x04000E66 RID: 3686
    [Nullable(2)]
    private TextBlock status;

    // Token: 0x04000E67 RID: 3687
    [Nullable(2)]
    private TextBlock statusOutgoing;

    // Token: 0x04000E68 RID: 3688
    [Nullable(2)]
    private TextBlock hint;

    // Token: 0x04000E69 RID: 3689
    [Nullable(2)]
    private FrameworkElement cursor;

    // Token: 0x04000E6A RID: 3690
    [Nullable(2)]
    private FrameworkElement keyCapElement;

    // Token: 0x04000E6B RID: 3691
    private string statusTextBehindHint = "";

    // Token: 0x04000E6C RID: 3692
    private bool isShowingMouseHint;

    // Token: 0x04000E6D RID: 3693
    [Nullable(2)]
    private SpriteVisual shimmer;

    // Token: 0x04000E6E RID: 3694
    [Nullable(2)]
    private CompositionLinearGradientBrush shimmerBrush;

    // Token: 0x04000E6F RID: 3695
    [Nullable(2)]
    private ContainerVisual glowHolder;

    // Token: 0x04000E70 RID: 3696
    private bool reduceMotion;

    // Token: 0x04000E71 RID: 3697
    private int generation;

    // Token: 0x04000E72 RID: 3698
    public const double Band = 54.0;

    // Token: 0x04000E73 RID: 3699
    private static readonly Color MouseHintColor = Color.FromArgb(byte.MaxValue, byte.MaxValue, 199, 107);

    // Token: 0x04000E74 RID: 3700
    private const string MouseHintLine = "You moved the mouse. I'm still driving.";

    // Token: 0x04000E75 RID: 3701
    private static readonly SwiftSpring GrowSpring = new SwiftSpring(6.283185307179586 / Math.Sqrt(210.0), 16.0 / (2.0 * Math.Sqrt(210.0)));
  }
}
