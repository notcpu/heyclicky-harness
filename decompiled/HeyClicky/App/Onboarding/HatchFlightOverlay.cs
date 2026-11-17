using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Interop;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x0200011B RID: 283
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class HatchFlightOverlay
  {
    // Token: 0x14000035 RID: 53
    // (add) Token: 0x06000F30 RID: 3888 RVA: 0x005792E0 File Offset: 0x005792E0
    // (remove) Token: 0x06000F31 RID: 3889 RVA: 0x00579318 File Offset: 0x00579318
    public event Action NotchShouldExpand;

    // Token: 0x14000036 RID: 54
    // (add) Token: 0x06000F32 RID: 3890 RVA: 0x00579350 File Offset: 0x00579350
    // (remove) Token: 0x06000F33 RID: 3891 RVA: 0x00579388 File Offset: 0x00579388
    public event Action Landed;

    // Token: 0x14000037 RID: 55
    // (add) Token: 0x06000F34 RID: 3892 RVA: 0x005793C0 File Offset: 0x005793C0
    // (remove) Token: 0x06000F35 RID: 3893 RVA: 0x005793F8 File Offset: 0x005793F8
    public event Action PeekFinishing;

    // Token: 0x14000038 RID: 56
    // (add) Token: 0x06000F36 RID: 3894 RVA: 0x00579430 File Offset: 0x00579430
    // (remove) Token: 0x06000F37 RID: 3895 RVA: 0x00579468 File Offset: 0x00579468
    public event Action Completed;

    // Token: 0x06000F38 RID: 3896 RVA: 0x005794A0 File Offset: 0x005794A0
    public HatchFlightOverlay(bool excludeFromCapture)
    {
      this.dq = DispatcherQueue.GetForCurrentThread();
      this.window = new OverlayWindow("HeyClicky Flight", excludeFromCapture, true, false, true, false, false);
    }

    // Token: 0x06000F39 RID: 3897 RVA: 0x005794CC File Offset: 0x005794CC
    public void Fly(double startPxX, double startPxY, Color color)
    {
      Displays.Monitor monitor = Displays.From(Native.MonitorFromPoint(new Native.POINT
      {
        X = (int)startPxX,
        Y = (int)startPxY
      }, 2));
      this.window.PlacePx(monitor.Bounds.Left, monitor.Bounds.Top, monitor.Bounds.Width, monitor.Bounds.Height);
      double scale = monitor.Scale;
      double num = (double)monitor.Bounds.Width / scale;
      Vector2 vector = new Vector2((float)((startPxX - (double)monitor.Bounds.Left) / scale), (float)((startPxY - (double)monitor.Bounds.Top) / scale));
      double num2 = Math.Max(240.0, 130.0);
      Vector2 vector2 = new Vector2((float)(num / 2.0 + num2 / 2.0 - 26.0), 16f);
      Vector2 vector3 = new Vector2(vector2.X - 44f, vector2.Y);
      Vector2 vector4 = new Vector2((vector.X + vector3.X) / 2f, Math.Min(vector.Y, vector3.Y) - 80f);
      Canvas canvas = new Canvas();
      this.window.Root.Children.Clear();
      this.window.Root.Children.Add(canvas);
      Grid grid = new Grid
      {
        Width = 44.0,
        Height = 44.0
      };
      Grid grid2 = Ui.GlowSpot(color, 0.32, 118.0, 0.3).Pad(-37.0);
      grid2.Opacity = 0.558;
      grid.Children.Add(grid2);
      grid.Children.Add(IntroMark.Create(44.0, color, 0.0));
      grid.CenterPoint = new Vector3(22f, 22f, 0f);
      canvas.Children.Add(grid);
      grid.Translation = new Vector3(vector.X - 22f, vector.Y - 22f, 0f);
      this.window.ShowNoActivate();
      Compositor compositor = Ui.Compositor;
      CubicBezierEasingFunction cubicBezierEasingFunction = compositor.CreateCubicBezierEasingFunction(new Vector2(0f, 0f), new Vector2(0.58f, 1f));
      CubicBezierEasingFunction cubicBezierEasingFunction2 = compositor.CreateCubicBezierEasingFunction(new Vector2(0.42f, 0f), new Vector2(1f, 1f));
      float num3 = 0.5833333f;
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = compositor.CreateVector3KeyFrameAnimation();
      vector3KeyFrameAnimation.InsertKeyFrame(0f, new Vector3(vector.X - 22f, vector.Y - 22f, 0f));
      vector3KeyFrameAnimation.InsertKeyFrame(num3, new Vector3(vector4.X - 22f, vector4.Y - 22f, 0f), cubicBezierEasingFunction);
      vector3KeyFrameAnimation.InsertKeyFrame(1f, new Vector3(vector3.X - 22f, vector3.Y - 22f, 0f), cubicBezierEasingFunction2);
      vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(0.72);
      vector3KeyFrameAnimation.Target = "Translation";
      grid.StartAnimation(vector3KeyFrameAnimation);
      Vector3KeyFrameAnimation vector3KeyFrameAnimation2 = compositor.CreateVector3KeyFrameAnimation();
      vector3KeyFrameAnimation2.InsertKeyFrame(0f, Vector3.One);
      vector3KeyFrameAnimation2.InsertKeyFrame(num3, new Vector3(0.78f, 0.78f, 1f), cubicBezierEasingFunction);
      vector3KeyFrameAnimation2.InsertKeyFrame(1f, new Vector3(0.26f, 0.26f, 1f), cubicBezierEasingFunction2);
      vector3KeyFrameAnimation2.Duration = TimeSpan.FromSeconds(0.72);
      vector3KeyFrameAnimation2.Target = "Scale";
      grid.StartAnimation(vector3KeyFrameAnimation2);
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = compositor.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(0f, 0f);
      scalarKeyFrameAnimation.InsertKeyFrame(num3, -14f, cubicBezierEasingFunction);
      scalarKeyFrameAnimation.InsertKeyFrame(1f, -32f, cubicBezierEasingFunction2);
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(0.72);
      scalarKeyFrameAnimation.Target = "Rotation";
      grid.StartAnimation(scalarKeyFrameAnimation);
      ScalarKeyFrameAnimation scalarKeyFrameAnimation2 = compositor.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation2.InsertKeyFrame(0f, 1f);
      scalarKeyFrameAnimation2.InsertKeyFrame(num3, 1f);
      scalarKeyFrameAnimation2.InsertKeyFrame(1f, 0f, cubicBezierEasingFunction2);
      scalarKeyFrameAnimation2.Duration = TimeSpan.FromSeconds(0.72);
      scalarKeyFrameAnimation2.Target = "Opacity";
      grid.StartAnimation(scalarKeyFrameAnimation2);
      ScalarKeyFrameAnimation scalarKeyFrameAnimation3 = compositor.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation3.InsertKeyFrame(0f, 0.55799997f);
      scalarKeyFrameAnimation3.InsertKeyFrame(num3, 0.4836f, cubicBezierEasingFunction);
      scalarKeyFrameAnimation3.InsertKeyFrame(1f, 0.124000005f, cubicBezierEasingFunction2);
      scalarKeyFrameAnimation3.Duration = TimeSpan.FromSeconds(0.72);
      scalarKeyFrameAnimation3.Target = "Opacity";
      grid2.StartAnimation(scalarKeyFrameAnimation3);
      this.After(0.72, delegate
      {
        Action notchShouldExpand = this.NotchShouldExpand;
        if (notchShouldExpand == null)
        {
          return;
        }
        notchShouldExpand();
      });
      this.After(0.96, delegate
      {
        Action landed = this.Landed;
        if (landed == null)
        {
          return;
        }
        landed();
      });
      this.After(3.46, delegate
      {
        Action peekFinishing = this.PeekFinishing;
        if (peekFinishing == null)
        {
          return;
        }
        peekFinishing();
      });
      this.After(3.68, delegate
      {
        this.window.HideWindow();
        Action completed = this.Completed;
        if (completed == null)
        {
          return;
        }
        completed();
      });
    }

    // Token: 0x06000F3A RID: 3898 RVA: 0x00579A84 File Offset: 0x00579A84
    [NullableContext(1)]
    private void After(double seconds, Action a)
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.dq.CreateTimer().Rooted();
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(seconds);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        a();
      };
      dispatcherQueueTimer.Start();
    }

    // Token: 0x0400065E RID: 1630
    [Nullable(1)]
    private readonly OverlayWindow window;

    // Token: 0x0400065F RID: 1631
    [Nullable(1)]
    private readonly DispatcherQueue dq;
  }
}
