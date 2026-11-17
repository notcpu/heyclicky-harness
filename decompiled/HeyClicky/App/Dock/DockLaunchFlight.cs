using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Interop;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200032F RID: 815
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DockLaunchFlight
  {
    // Token: 0x060022D1 RID: 8913 RVA: 0x00627FA4 File Offset: 0x00627FA4
    public DockLaunchFlight(bool excludeFromCapture)
    {
      this.excludeFromCapture = excludeFromCapture;
    }

    // Token: 0x060022D2 RID: 8914 RVA: 0x00627FB4 File Offset: 0x00627FB4
    public void Fly(PointD endPx, Color color, Action onFirstFrame, PointD? startPx = null)
    {
      if (Anim.Instant || Anim.ReduceMotion)
      {
        onFirstFrame();
        return;
      }
      try
      {
        Native.POINT point;
        Native.GetCursorPos(out point);
        Displays.Monitor monitor;
        if (startPx != null)
        {
          PointD valueOrDefault = startPx.GetValueOrDefault();
          monitor = Displays.At((int)valueOrDefault.X, (int)valueOrDefault.Y);
        }
        else
        {
          monitor = Displays.UnderCursor();
        }
        Displays.Monitor monitor2 = monitor;
        this.EnsureWindow(monitor2);
        double scale = monitor2.Scale;
        Vector2 vector;
        if (startPx != null)
        {
          PointD valueOrDefault2 = startPx.GetValueOrDefault();
          vector = new Vector2((float)((valueOrDefault2.X - (double)monitor2.Bounds.Left) / scale), (float)((valueOrDefault2.Y - (double)monitor2.Bounds.Top) / scale));
        }
        else
        {
          vector = new Vector2((float)((double)(point.X - monitor2.Bounds.Left) / scale + 35.0), (float)((double)(point.Y - monitor2.Bounds.Top) / scale + 25.0));
        }
        Vector2 vector2 = vector;
        Vector2 vector3 = new Vector2((float)((endPx.X - (double)monitor2.Bounds.Left) / scale), (float)((endPx.Y - (double)monitor2.Bounds.Top) / scale));
        Grid outer = new Grid
        {
          Width = 26.0,
          Height = 26.0,
          IsHitTestVisible = false
        };
        outer.Children.Add(Ui.GlowSpot(color, 0.8, 54.0, 0.2));
        Grid grid = new Grid
        {
          Width = 26.0,
          Height = 26.0,
          CenterPoint = new Vector3(13f, 13f, 0f)
        };
        grid.Children.Add(new ShadowLayer(3.0, color.WithAlpha(0.4), 3.0, 1.0, 0.0)
        {
          Margin = new Thickness(6.0)
        });
        grid.Children.Add(IntroMark.Cursor(26.0, color, 0.0));
        outer.Children.Add(grid);
        this.layer.Children.Add(outer);
        Compositor compositor = Ui.Compositor;
        LinearEasingFunction linearEasingFunction = compositor.CreateLinearEasingFunction();
        Vector3KeyFrameAnimation vector3KeyFrameAnimation = compositor.CreateVector3KeyFrameAnimation();
        ScalarKeyFrameAnimation scalarKeyFrameAnimation = compositor.CreateScalarKeyFrameAnimation();
        Vector3KeyFrameAnimation vector3KeyFrameAnimation2 = compositor.CreateVector3KeyFrameAnimation();
        ScalarKeyFrameAnimation scalarKeyFrameAnimation2 = compositor.CreateScalarKeyFrameAnimation();
        int num = (int)Math.Ceiling(DockLaunchFlight.Visible * 240.0);
        for (int i = 0; i <= num; i++)
        {
          double num2 = (double)i / (double)num * DockLaunchFlight.Visible;
          float num3 = (float)((double)i / (double)num);
          double num4 = DockLaunchFlight.Progress(num2, 0.05, 0.7, DockLaunchFlight.EaseInOut);
          Vector2 vector4 = vector2 + (vector3 - vector2) * (float)num4;
          vector3KeyFrameAnimation.InsertKeyFrame(num3, new Vector3(vector4.X - 13f, vector4.Y - 13f, 0f), linearEasingFunction);
          scalarKeyFrameAnimation.InsertKeyFrame(num3, (float)DockLaunchFlight.Opacity(num2), linearEasingFunction);
          float num5 = (float)DockLaunchFlight.Scale(num2);
          vector3KeyFrameAnimation2.InsertKeyFrame(num3, new Vector3(num5, num5, 1f), linearEasingFunction);
          scalarKeyFrameAnimation2.InsertKeyFrame(num3, (float)(-140.0 + 108.0 * (1.0 - num4)), linearEasingFunction);
        }
        KeyFrameAnimation[] array = new KeyFrameAnimation[] { vector3KeyFrameAnimation, scalarKeyFrameAnimation, vector3KeyFrameAnimation2, scalarKeyFrameAnimation2 };
        for (int j = 0; j < array.Length; j++)
        {
          array[j].Duration = TimeSpan.FromSeconds(DockLaunchFlight.Visible);
        }
        vector3KeyFrameAnimation.Target = "Translation";
        scalarKeyFrameAnimation.Target = "Opacity";
        vector3KeyFrameAnimation2.Target = "Scale";
        scalarKeyFrameAnimation2.Target = "Rotation";
        outer.Opacity = 0.0;
        outer.StartAnimation(vector3KeyFrameAnimation);
        outer.StartAnimation(scalarKeyFrameAnimation);
        grid.StartAnimation(vector3KeyFrameAnimation2);
        grid.StartAnimation(scalarKeyFrameAnimation2);
        this.inFlight++;
        this.window.ShowNoActivate();
        DispatcherQueue.GetForCurrentThread().TryEnqueue(DispatcherQueuePriority.Low, delegate
        {
          onFirstFrame();
        });
        Canvas host = this.layer;
        DockTimer.After(1.1, delegate
        {
          host.Children.Remove(outer);
          DockLaunchFlight <>4__this = this;
          int num6 = this.inFlight - 1;
          <>4__this.inFlight = num6;
          if (num6 == 0)
          {
            this.window.HideWindow();
          }
        });
      }
      catch (Exception ex)
      {
        Log.Warn("dock", "launch flight: " + ex.Message);
        onFirstFrame();
      }
    }

    // Token: 0x060022D3 RID: 8915 RVA: 0x006284F4 File Offset: 0x006284F4
    private void EnsureWindow(Displays.Monitor m)
    {
      if (this.window == null)
      {
        this.window = new OverlayWindow("HeyClicky Launch Flight", this.excludeFromCapture, true, false, true, false, false);
      }
      if (this.layer == null)
      {
        this.layer = new Canvas();
        this.window.Root.Children.Add(this.layer);
      }
      this.window.Root.Width = (double)m.Bounds.Width / m.Scale;
      this.window.Root.Height = (double)m.Bounds.Height / m.Scale;
      this.window.PlacePx(m.Bounds.Left, m.Bounds.Top, m.Bounds.Width, m.Bounds.Height);
    }

    // Token: 0x060022D4 RID: 8916 RVA: 0x006285E4 File Offset: 0x006285E4
    private static double Scale(double t)
    {
      return 0.35 + -0.25 * (1.0 - DockLaunchFlight.Progress(t, 0.0, 0.12, DockLaunchFlight.EaseOut)) + 0.9 * (1.0 - DockLaunchFlight.Progress(t, 0.05, 0.7, DockLaunchFlight.EaseInOut));
    }

    // Token: 0x060022D5 RID: 8917 RVA: 0x00628660 File Offset: 0x00628660
    private static double Opacity(double t)
    {
      return Math.Clamp(0.0 + -1.0 * (1.0 - DockLaunchFlight.Progress(t, 0.0, 0.12, DockLaunchFlight.EaseOut)) + 1.0 * (1.0 - DockLaunchFlight.Progress(t, 0.62, 0.25, DockLaunchFlight.EaseIn)), 0.0, 1.0);
    }

    // Token: 0x060022D6 RID: 8918 RVA: 0x006286F4 File Offset: 0x006286F4
    private static double Progress(double t, double delay, double duration, Func<double, double> curve)
    {
      return curve(Math.Clamp((t - delay) / duration, 0.0, 1.0));
    }

    // Token: 0x060022D7 RID: 8919 RVA: 0x00628718 File Offset: 0x00628718
    private static Func<double, double> Bezier(double x1, double y1, double x2, double y2)
    {
      double cx = 3.0 * x1;
      double bx = 3.0 * (x2 - x1) - cx;
      double ax = 1.0 - cx - bx;
      double cy = 3.0 * y1;
      double by = 3.0 * (y2 - y1) - cy;
      double ay = 1.0 - cy - by;
      return delegate(double x)
      {
        if (x <= 0.0)
        {
          return 0.0;
        }
        if (x >= 1.0)
        {
          return 1.0;
        }
        double num = x;
        for (int i = 0; i < 8; i++)
        {
          double num2 = base.<Bezier>g__X|0(num) - x;
          if (Math.Abs(num2) < 1E-07)
          {
            return base.<Bezier>g__Y|1(num);
          }
          double num3 = base.<Bezier>g__dX|2(num);
          if (Math.Abs(num3) < 1E-06)
          {
            break;
          }
          num -= num2 / num3;
        }
        double num4 = 0.0;
        double num5 = 1.0;
        num = x;
        int num6 = 0;
        while (num6 < 64 && num4 < num5)
        {
          double num7 = base.<Bezier>g__X|0(num);
          if (Math.Abs(num7 - x) < 1E-07)
          {
            break;
          }
          if (x > num7)
          {
            num4 = num;
          }
          else
          {
            num5 = num;
          }
          num = (num4 + num5) / 2.0;
          num6++;
        }
        return base.<Bezier>g__Y|1(num);
      };
    }

    // Token: 0x040010E9 RID: 4329
    private const double TriangleSize = 26.0;

    // Token: 0x040010EA RID: 4330
    private const double StartRotation = -32.0;

    // Token: 0x040010EB RID: 4331
    private const double EndRotation = -140.0;

    // Token: 0x040010EC RID: 4332
    private const double StartScale = 1.0;

    // Token: 0x040010ED RID: 4333
    private const double PopScale = 1.25;

    // Token: 0x040010EE RID: 4334
    private const double EndScale = 0.35;

    // Token: 0x040010EF RID: 4335
    private const double PopDuration = 0.12;

    // Token: 0x040010F0 RID: 4336
    private const double TravelDelay = 0.05;

    // Token: 0x040010F1 RID: 4337
    private const double TravelDuration = 0.7;

    // Token: 0x040010F2 RID: 4338
    private const double FadeDelay = 0.62;

    // Token: 0x040010F3 RID: 4339
    private const double FadeDuration = 0.25;

    // Token: 0x040010F4 RID: 4340
    private static readonly double Visible = Math.Max(0.75, 0.87);

    // Token: 0x040010F5 RID: 4341
    private readonly bool excludeFromCapture;

    // Token: 0x040010F6 RID: 4342
    [Nullable(2)]
    private OverlayWindow window;

    // Token: 0x040010F7 RID: 4343
    [Nullable(2)]
    private Canvas layer;

    // Token: 0x040010F8 RID: 4344
    private int inFlight;

    // Token: 0x040010F9 RID: 4345
    private static readonly Func<double, double> EaseOut = DockLaunchFlight.Bezier(0.0, 0.0, 0.58, 1.0);

    // Token: 0x040010FA RID: 4346
    private static readonly Func<double, double> EaseInOut = DockLaunchFlight.Bezier(0.42, 0.0, 0.58, 1.0);

    // Token: 0x040010FB RID: 4347
    private static readonly Func<double, double> EaseIn = DockLaunchFlight.Bezier(0.42, 0.0, 1.0, 1.0);
  }
}
