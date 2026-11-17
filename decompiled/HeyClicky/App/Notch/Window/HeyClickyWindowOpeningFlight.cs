using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using HeyClicky.App.Interop;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Composition.Desktop;
using WinRT;

namespace HeyClicky.App.Notch.Window
{
  // Token: 0x02000145 RID: 325
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class HeyClickyWindowOpeningFlight : IDisposable
  {
    // Token: 0x17000355 RID: 853
    // (get) Token: 0x0600124C RID: 4684 RVA: 0x00595D78 File Offset: 0x00595D78
    // (set) Token: 0x0600124D RID: 4685 RVA: 0x00595D80 File Offset: 0x00595D80
    public static bool IsFlying { get; private set; }

    // Token: 0x17000356 RID: 854
    // (get) Token: 0x0600124E RID: 4686 RVA: 0x00595D88 File Offset: 0x00595D88
    // (set) Token: 0x0600124F RID: 4687 RVA: 0x00595D90 File Offset: 0x00595D90
    internal static IntPtr CurrentCarrier { get; private set; }

    // Token: 0x17000357 RID: 855
    // (get) Token: 0x06001250 RID: 4688 RVA: 0x00595D98 File Offset: 0x00595D98
    // (set) Token: 0x06001251 RID: 4689 RVA: 0x00595DA0 File Offset: 0x00595DA0
    internal static DateTime LastUncoveredAt { get; set; }

    // Token: 0x17000358 RID: 856
    // (get) Token: 0x06001252 RID: 4690 RVA: 0x00595DA8 File Offset: 0x00595DA8
    // (set) Token: 0x06001253 RID: 4691 RVA: 0x00595DB0 File Offset: 0x00595DB0
    internal static DateTime LastNotchHiddenAt { get; private set; }

    // Token: 0x17000359 RID: 857
    // (get) Token: 0x06001254 RID: 4692 RVA: 0x00595DB8 File Offset: 0x00595DB8
    // (set) Token: 0x06001255 RID: 4693 RVA: 0x00595DC0 File Offset: 0x00595DC0
    internal static DateTime LastNotchShownAt { get; private set; }

    // Token: 0x06001256 RID: 4694 RVA: 0x00595DC8 File Offset: 0x00595DC8
    [NullableContext(0)]
    [return: Nullable(2)]
    public static HeyClickyWindowOpeningFlight Begin([TupleElementNames(new string[] { "X", "Y", "W", "H" })] ValueTuple<int, int, int, int> openNotchPx, Displays.Monitor monitor, IntPtr notchWindow, Color windowGround, bool excludeFromCapture)
    {
      if (Anim.Instant || Anim.ReduceMotion || openNotchPx.Item3 <= 0 || openNotchPx.Item4 <= 0)
      {
        return null;
      }
      HeyClickyWindowOpeningFlight heyClickyWindowOpeningFlight;
      try
      {
        heyClickyWindowOpeningFlight = new HeyClickyWindowOpeningFlight(openNotchPx, monitor, notchWindow, windowGround, excludeFromCapture);
      }
      catch (Exception ex)
      {
        Log.Warn("window", "no flight out of the notch: " + ex.Message);
        heyClickyWindowOpeningFlight = null;
      }
      return heyClickyWindowOpeningFlight;
    }

    // Token: 0x06001257 RID: 4695 RVA: 0x00595E38 File Offset: 0x00595E38
    [NullableContext(0)]
    private HeyClickyWindowOpeningFlight([TupleElementNames(new string[] { "X", "Y", "W", "H" })] ValueTuple<int, int, int, int> openNotchPx, Displays.Monitor monitor, IntPtr notchWindow, Color windowGround, bool excludeFromCapture)
    {
      this.windowGround = windowGround;
      this.notchWindow = notchWindow;
      this.scale = monitor.Scale;
      int num = (OverlayWindow.Offscreen ? (-24000) : 0);
      this.originX = monitor.Bounds.Left + num;
      this.originY = monitor.Bounds.Top + num;
      this.hwnd = HeyClickyWindowOpeningFlight.CreateCarrier(this.originX, this.originY, monitor.Bounds.Width, monitor.Bounds.Height, excludeFromCapture);
      this.c = TransparentBackdrop.SystemCompositor;
      this.target = HeyClickyWindowOpeningFlight.CreateTarget(this.c, this.hwnd);
      ContainerVisual containerVisual = this.c.CreateContainerVisual();
      containerVisual.RelativeSizeAdjustment = Vector2.One;
      this.target.Root = containerVisual;
      this.p = this.c.CreatePropertySet();
      float num2 = (float)(22.0 * this.scale);
      Vector2 vector = new Vector2((float)(openNotchPx.Item1 + num - this.originX), (float)(openNotchPx.Item2 + num - this.originY) - num2);
      Vector2 vector2 = new Vector2((float)openNotchPx.Item3, (float)openNotchPx.Item4 + num2);
      float num3 = (float)Math.Min((double)openNotchPx.Item3, 224.0 * this.scale);
      float num4 = (float)(46.0 * this.scale);
      float num5 = num4 / 2f;
      Vector2 vector3 = new Vector2(vector.X + ((float)openNotchPx.Item3 - num3) / 2f, (float)(openNotchPx.Item2 + num - this.originY) - num5);
      Vector2 vector4 = new Vector2(num3, num4 + num5);
      this.p.InsertVector2("OpenOff", vector);
      this.p.InsertVector2("OpenSize", vector2);
      this.p.InsertVector2("OpenR", new Vector2(num2));
      this.p.InsertVector2("DropOff", vector3);
      this.p.InsertVector2("DropSize", vector4);
      this.p.InsertVector2("DropR", new Vector2(num5));
      this.p.InsertVector2("WinOff", vector3);
      this.p.InsertVector2("WinSize", vector4);
      this.p.InsertVector2("WinR", new Vector2(num5));
      this.p.InsertScalar("F", 0f);
      this.p.InsertScalar("B", 0f);
      this.shape = this.c.CreateRoundedRectangleGeometry();
      this.Bind(this.shape, "Offset", "Off", null);
      this.Bind(this.shape, "Size", "Size", null);
      this.Bind(this.shape, "CornerRadius", "R", null);
      this.fill = this.c.CreateColorBrush(Colors.Black);
      CompositionSpriteShape compositionSpriteShape = this.c.CreateSpriteShape(this.shape);
      compositionSpriteShape.FillBrush = this.fill;
      ShapeVisual shapeVisual = this.c.CreateShapeVisual();
      shapeVisual.RelativeSizeAdjustment = Vector2.One;
      this.shadowShapes = this.c.CreateShapeVisual();
      this.shadowShapes.RelativeSizeAdjustment = Vector2.One;
      this.shadowShapes.Opacity = 0f;
      CompositionColorBrush compositionColorBrush = this.c.CreateColorBrush(Color.FromArgb(5, 0, 0, 0));
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      for (int i = 14; i >= 1; i--)
      {
        CompositionRoundedRectangleGeometry compositionRoundedRectangleGeometry = this.c.CreateRoundedRectangleGeometry();
        float num6 = (float)((double)i * 2.2 * this.scale);
        float num7 = (float)(7.0 * this.scale);
        CompositionObject compositionObject = compositionRoundedRectangleGeometry;
        string text = "Offset";
        string text2 = "Off";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 2);
        defaultInterpolatedStringHandler.AppendLiteral("Vector2(");
        defaultInterpolatedStringHandler.AppendFormatted<float>(-num6);
        defaultInterpolatedStringHandler.AppendLiteral(", ");
        defaultInterpolatedStringHandler.AppendFormatted<float>(num7 - num6);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        this.Bind(compositionObject, text, text2, defaultInterpolatedStringHandler.ToStringAndClear());
        CompositionObject compositionObject2 = compositionRoundedRectangleGeometry;
        string text3 = "Size";
        string text4 = "Size";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 2);
        defaultInterpolatedStringHandler.AppendLiteral("Vector2(");
        defaultInterpolatedStringHandler.AppendFormatted<float>(2f * num6);
        defaultInterpolatedStringHandler.AppendLiteral(", ");
        defaultInterpolatedStringHandler.AppendFormatted<float>(2f * num6);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        this.Bind(compositionObject2, text3, text4, defaultInterpolatedStringHandler.ToStringAndClear());
        CompositionObject compositionObject3 = compositionRoundedRectangleGeometry;
        string text5 = "CornerRadius";
        string text6 = "R";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 2);
        defaultInterpolatedStringHandler.AppendLiteral("Vector2(");
        defaultInterpolatedStringHandler.AppendFormatted<float>(num6);
        defaultInterpolatedStringHandler.AppendLiteral(", ");
        defaultInterpolatedStringHandler.AppendFormatted<float>(num6);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        this.Bind(compositionObject3, text5, text6, defaultInterpolatedStringHandler.ToStringAndClear());
        CompositionSpriteShape compositionSpriteShape2 = this.c.CreateSpriteShape(compositionRoundedRectangleGeometry);
        compositionSpriteShape2.FillBrush = compositionColorBrush;
        this.shadowShapes.Shapes.Add(compositionSpriteShape2);
      }
      shapeVisual.Shapes.Add(compositionSpriteShape);
      this.blob = this.c.CreateContainerVisual();
      this.blob.RelativeSizeAdjustment = Vector2.One;
      this.blob.Children.InsertAtTop(this.shadowShapes);
      this.blob.Children.InsertAtTop(shapeVisual);
      this.blob.Opacity = 0f;
      containerVisual.Children.InsertAtTop(this.blob);
      Native.SetWindowPos(this.hwnd, Native.HWND_TOPMOST, 0, 0, 0, 0, 83U);
      HeyClickyWindowOpeningFlight.IsFlying = true;
      HeyClickyWindowOpeningFlight.CurrentCarrier = this.hwnd;
      string text7 = "window";
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(31, 4);
      defaultInterpolatedStringHandler.AppendLiteral("flight: out of the notch ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(openNotchPx.Item3);
      defaultInterpolatedStringHandler.AppendLiteral("x");
      defaultInterpolatedStringHandler.AppendFormatted<int>(openNotchPx.Item4);
      defaultInterpolatedStringHandler.AppendLiteral(" at ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(openNotchPx.Item1);
      defaultInterpolatedStringHandler.AppendLiteral(",");
      defaultInterpolatedStringHandler.AppendFormatted<int>(openNotchPx.Item2);
      Log.Info(text7, defaultInterpolatedStringHandler.ToStringAndClear());
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = this.c.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(0f, 0f);
      scalarKeyFrameAnimation.InsertKeyFrame(1f, 1f, this.c.CreateCubicBezierEasingFunction(new Vector2(0f, 0f), new Vector2(0.58f, 1f)));
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(0.09);
      CompositionScopedBatch compositionScopedBatch = this.c.CreateScopedBatch(CompositionBatchTypes.Animation);
      this.blob.StartAnimation("Opacity", scalarKeyFrameAnimation);
      compositionScopedBatch.End();
      DispatcherQueue ui = DispatcherQueue.GetForCurrentThread();
      compositionScopedBatch.Completed += delegate(object _, CompositionBatchCompletedEventArgs _)
      {
        ui.TryEnqueue(new DispatcherQueueHandler(this.Covered));
      };
      DispatcherQueueTimer coverTimeout = ui.CreateTimer();
      coverTimeout.Interval = TimeSpan.FromSeconds(0.3);
      coverTimeout.IsRepeating = false;
      coverTimeout.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        coverTimeout.Stop();
        this.Covered();
      };
      coverTimeout.Start();
      this.watchdog = new Timer(delegate(object _)
      {
        ui.TryEnqueue(delegate
        {
          if (!this.revealing && !this.disposed)
          {
            Log.Warn("window", "flight: never landed; the shape fades");
            this.Reveal(null);
          }
        });
      }, null, TimeSpan.FromSeconds(3L), Timeout.InfiniteTimeSpan);
      ScalarKeyFrameAnimation scalarKeyFrameAnimation2 = this.c.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation2.InsertKeyFrame(0f, 0f);
      scalarKeyFrameAnimation2.InsertKeyFrame(1f, 1f, this.c.CreateCubicBezierEasingFunction(new Vector2(0.55f, 0f), new Vector2(0.3f, 1f)));
      scalarKeyFrameAnimation2.Duration = TimeSpan.FromSeconds(0.25);
      scalarKeyFrameAnimation2.DelayTime = TimeSpan.FromSeconds(0.06);
      scalarKeyFrameAnimation2.DelayBehavior = AnimationDelayBehavior.SetInitialValueBeforeDelay;
      this.p.StartAnimation("F", scalarKeyFrameAnimation2);
    }

    // Token: 0x06001258 RID: 4696 RVA: 0x00596668 File Offset: 0x00596668
    public void AfterCover(Action action)
    {
      if (this.covered || this.disposed)
      {
        action();
        return;
      }
      this.afterCover.Add(action);
    }

    // Token: 0x06001259 RID: 4697 RVA: 0x00596690 File Offset: 0x00596690
    private void Covered()
    {
      if (this.covered)
      {
        return;
      }
      this.covered = true;
      this.HideNotch(true);
      foreach (Action action in this.afterCover)
      {
        action();
      }
      this.afterCover.Clear();
    }

    // Token: 0x0600125A RID: 4698 RVA: 0x00596704 File Offset: 0x00596704
    private void Bind(CompositionObject o, string property, string name, [Nullable(2)] string extra = null)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 3);
      defaultInterpolatedStringHandler.AppendLiteral("(p.Open");
      defaultInterpolatedStringHandler.AppendFormatted(name);
      defaultInterpolatedStringHandler.AppendLiteral(" + (p.Drop");
      defaultInterpolatedStringHandler.AppendFormatted(name);
      defaultInterpolatedStringHandler.AppendLiteral(" - p.Open");
      defaultInterpolatedStringHandler.AppendFormatted(name);
      defaultInterpolatedStringHandler.AppendLiteral(") * p.F)");
      string text = defaultInterpolatedStringHandler.ToStringAndClear();
      Compositor compositor = this.c;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(19, 3);
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral(" + (p.Win");
      defaultInterpolatedStringHandler.AppendFormatted(name);
      defaultInterpolatedStringHandler.AppendLiteral(" - ");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral(") * p.B");
      ExpressionAnimation expressionAnimation = compositor.CreateExpressionAnimation(defaultInterpolatedStringHandler.ToStringAndClear() + ((extra == null) ? "" : (" + " + extra)));
      expressionAnimation.SetReferenceParameter("p", this.p);
      o.StartAnimation(property, expressionAnimation);
    }

    // Token: 0x0600125B RID: 4699 RVA: 0x005967FC File Offset: 0x005967FC
    public void Bloom(Native.RECT windowPx, Action spans)
    {
      if (this.disposed)
      {
        spans();
        return;
      }
      double totalSeconds = (DateTime.UtcNow - this.startedAt).TotalSeconds;
      double num = Math.Max(0.0, 0.31 - totalSeconds);
      float num2 = (float)(8.0 * this.scale);
      this.p.InsertVector2("WinOff", new Vector2((float)(windowPx.Left - this.originX), (float)(windowPx.Top - this.originY)));
      this.p.InsertVector2("WinSize", new Vector2((float)windowPx.Width, (float)windowPx.Height));
      this.p.InsertVector2("WinR", new Vector2(num2));
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = this.c.CreateScalarKeyFrameAnimation();
      LinearEasingFunction linearEasingFunction = this.c.CreateLinearEasingFunction();
      for (int i = 1; i <= 60; i++)
      {
        double num3 = 0.95 * (double)i / 60.0;
        scalarKeyFrameAnimation.InsertKeyFrame((float)i / 60f, (i == 60) ? 1f : ((float)HeyClickyWindowFlightSpring.Progress(num3)), linearEasingFunction);
      }
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(0.95);
      scalarKeyFrameAnimation.DelayTime = TimeSpan.FromSeconds(num);
      scalarKeyFrameAnimation.DelayBehavior = AnimationDelayBehavior.SetInitialValueBeforeDelay;
      this.p.StartAnimation("B", scalarKeyFrameAnimation);
      string text = "window";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 6);
      defaultInterpolatedStringHandler.AppendLiteral("flight: blooms into ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(windowPx.Width);
      defaultInterpolatedStringHandler.AppendLiteral("x");
      defaultInterpolatedStringHandler.AppendFormatted<int>(windowPx.Height);
      defaultInterpolatedStringHandler.AppendLiteral(" at ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(windowPx.Left);
      defaultInterpolatedStringHandler.AppendLiteral(",");
      defaultInterpolatedStringHandler.AppendFormatted<int>(windowPx.Top);
      defaultInterpolatedStringHandler.AppendLiteral(" in ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(num, "0.000");
      defaultInterpolatedStringHandler.AppendLiteral(" s (");
      defaultInterpolatedStringHandler.AppendFormatted<double>(totalSeconds, "0.000");
      defaultInterpolatedStringHandler.AppendLiteral(" s in)");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      ScalarKeyFrameAnimation scalarKeyFrameAnimation2 = this.c.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation2.InsertKeyFrame(0f, 0f);
      scalarKeyFrameAnimation2.InsertKeyFrame(1f, 1f);
      scalarKeyFrameAnimation2.Duration = TimeSpan.FromSeconds(0.4);
      scalarKeyFrameAnimation2.DelayTime = TimeSpan.FromSeconds(num);
      this.shadowShapes.StartAnimation("Opacity", scalarKeyFrameAnimation2);
      ColorKeyFrameAnimation colorKeyFrameAnimation = this.c.CreateColorKeyFrameAnimation();
      colorKeyFrameAnimation.InsertKeyFrame(0f, Colors.Black);
      colorKeyFrameAnimation.InsertKeyFrame(1f, this.windowGround, this.c.CreateCubicBezierEasingFunction(new Vector2(0.42f, 0f), new Vector2(0.58f, 1f)));
      colorKeyFrameAnimation.Duration = TimeSpan.FromSeconds(0.36);
      colorKeyFrameAnimation.DelayTime = TimeSpan.FromSeconds(num + 0.06);
      colorKeyFrameAnimation.DelayBehavior = AnimationDelayBehavior.SetInitialValueBeforeDelay;
      this.fill.StartAnimation("Color", colorKeyFrameAnimation);
      DispatcherQueue queue = DispatcherQueue.GetForCurrentThread();
      CompositionScopedBatch compositionScopedBatch = this.c.CreateScopedBatch(CompositionBatchTypes.Animation);
      ScalarKeyFrameAnimation scalarKeyFrameAnimation3 = this.c.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation3.InsertKeyFrame(1f, 1f, linearEasingFunction);
      scalarKeyFrameAnimation3.Duration = TimeSpan.FromSeconds(num + HeyClickyWindowFlightSpring.FirstCrossing() + 0.02);
      this.p.InsertScalar("Clock", 0f);
      this.p.StartAnimation("Clock", scalarKeyFrameAnimation3);
      compositionScopedBatch.End();
      DispatcherQueueHandler <>9__1;
      compositionScopedBatch.Completed += delegate(object _, CompositionBatchCompletedEventArgs _)
      {
        DispatcherQueue queue2 = queue;
        DispatcherQueueHandler dispatcherQueueHandler;
        if ((dispatcherQueueHandler = <>9__1) == null)
        {
          dispatcherQueueHandler = (<>9__1 = delegate
          {
            string text2 = "window";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(30, 1);
            defaultInterpolatedStringHandler2.AppendLiteral("flight: spans the window ");
            defaultInterpolatedStringHandler2.AppendFormatted<double>((DateTime.UtcNow - this.startedAt).TotalSeconds, "0.000");
            defaultInterpolatedStringHandler2.AppendLiteral(" s in");
            Log.Info(text2, defaultInterpolatedStringHandler2.ToStringAndClear());
            spans();
          });
        }
        queue2.TryEnqueue(dispatcherQueueHandler);
      };
    }

    // Token: 0x0600125C RID: 4700 RVA: 0x00596BE0 File Offset: 0x00596BE0
    [NullableContext(2)]
    public void Reveal(Action done = null)
    {
      if (!this.disposed)
      {
        this.revealing = true;
        this.HideNotch(false);
        ScalarKeyFrameAnimation scalarKeyFrameAnimation = this.c.CreateScalarKeyFrameAnimation();
        scalarKeyFrameAnimation.InsertKeyFrame(1f, 0f, this.c.CreateCubicBezierEasingFunction(new Vector2(0f, 0f), new Vector2(0.58f, 1f)));
        scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(0.3);
        this.blob.StartAnimation("Opacity", scalarKeyFrameAnimation);
        DispatcherQueue forCurrentThread = DispatcherQueue.GetForCurrentThread();
        DispatcherQueueTimer timer = forCurrentThread.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(0.35);
        timer.IsRepeating = false;
        timer.Tick += delegate(DispatcherQueueTimer _, object _)
        {
          timer.Stop();
          this.Dispose();
          Action done3 = done;
          if (done3 == null)
          {
            return;
          }
          done3();
        };
        timer.Start();
        return;
      }
      Action done2 = done;
      if (done2 == null)
      {
        return;
      }
      done2();
    }

    // Token: 0x0600125D RID: 4701 RVA: 0x00596CF0 File Offset: 0x00596CF0
    public void Abandon()
    {
      this.Reveal(null);
    }

    // Token: 0x0600125E RID: 4702 RVA: 0x00596CFC File Offset: 0x00596CFC
    public void Dispose()
    {
      if (this.disposed)
      {
        return;
      }
      this.disposed = true;
      Timer timer = this.watchdog;
      if (timer != null)
      {
        timer.Dispose();
      }
      HeyClickyWindowOpeningFlight.IsFlying = false;
      this.HideNotch(false);
      if (HeyClickyWindowOpeningFlight.CurrentCarrier == this.hwnd)
      {
        HeyClickyWindowOpeningFlight.CurrentCarrier = (IntPtr)0;
      }
      try
      {
        this.target.Root = null;
        this.target.Dispose();
      }
      catch
      {
      }
      if (this.hwnd != 0)
      {
        HeyClickyWindowOpeningFlight.DestroyWindow(this.hwnd);
      }
    }

    // Token: 0x0600125F RID: 4703 RVA: 0x00596D90 File Offset: 0x00596D90
    private void HideNotch(bool hide)
    {
      object obj = this.notchGate;
      lock (obj)
      {
        if (this.notchWindow != 0 && hide != this.notchHidden && (!hide || (!this.revealing && !this.disposed)))
        {
          this.notchHidden = hide;
          if (hide)
          {
            HeyClickyWindowOpeningFlight.LastNotchHiddenAt = DateTime.UtcNow;
          }
          else
          {
            HeyClickyWindowOpeningFlight.LastNotchShownAt = DateTime.UtcNow;
          }
          int num = ((hide > false) ? 1 : 0);
          Native.DwmSetWindowAttribute(this.notchWindow, 13, ref num, 4);
        }
      }
    }

    // Token: 0x06001260 RID: 4704 RVA: 0x00596E2C File Offset: 0x00596E2C
    public static void Cloak(IntPtr window, bool cloaked)
    {
      int num = ((cloaked > false) ? 1 : 0);
      Native.DwmSetWindowAttribute(window, 3, ref num, 4);
      Native.DwmSetWindowAttribute(window, 13, ref num, 4);
    }

    // Token: 0x06001261 RID: 4705 RVA: 0x00596E58 File Offset: 0x00596E58
    public static Native.RECT VisibleFrame(IntPtr window)
    {
      Native.RECT rect;
      if (HeyClickyWindowOpeningFlight.DwmGetWindowAttribute(window, 9, out rect, Marshal.SizeOf<Native.RECT>()) >= 0 && rect.Width > 0)
      {
        return rect;
      }
      Native.RECT rect2;
      Native.GetWindowRectPublic(window, out rect2);
      return rect2;
    }

    // Token: 0x06001262 RID: 4706
    [DllImport("dwmapi.dll")]
    private static extern int DwmGetWindowAttribute(IntPtr hwnd, int attribute, out Native.RECT value, int size);

    // Token: 0x06001263 RID: 4707 RVA: 0x00596E90 File Offset: 0x00596E90
    private static IntPtr CreateCarrier(int x, int y, int w, int h, bool excludeFromCapture)
    {
      IntPtr moduleHandleW = HeyClickyWindowOpeningFlight.GetModuleHandleW((IntPtr)0);
      if (!HeyClickyWindowOpeningFlight.registered)
      {
        HeyClickyWindowOpeningFlight.procedure = (IntPtr window, uint message, IntPtr wParam, IntPtr lParam) => Native.DefWindowProc(window, message, wParam, lParam);
        HeyClickyWindowOpeningFlight.WNDCLASSEXW wndclassexw = new HeyClickyWindowOpeningFlight.WNDCLASSEXW
        {
          cbSize = (uint)Marshal.SizeOf<HeyClickyWindowOpeningFlight.WNDCLASSEXW>(),
          lpfnWndProc = Marshal.GetFunctionPointerForDelegate<HeyClickyWindowOpeningFlight.WndProcDelegate>(HeyClickyWindowOpeningFlight.procedure),
          hInstance = moduleHandleW,
          lpszClassName = "HeyClickyWindowFlight"
        };
        if (HeyClickyWindowOpeningFlight.RegisterClassExW(ref wndclassexw) == 0)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 1);
          defaultInterpolatedStringHandler.AppendLiteral("RegisterClassEx failed (");
          defaultInterpolatedStringHandler.AppendFormatted<int>(Marshal.GetLastWin32Error());
          defaultInterpolatedStringHandler.AppendLiteral(")");
          throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
        }
        HeyClickyWindowOpeningFlight.registered = true;
      }
      IntPtr intPtr = HeyClickyWindowOpeningFlight.CreateWindowExW(136839336U, "HeyClickyWindowFlight", "HeyClicky", 2147483648U, x, y, w, h, (IntPtr)0, (IntPtr)0, moduleHandleW, (IntPtr)0);
      if (intPtr == 0)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 1);
        defaultInterpolatedStringHandler.AppendLiteral("CreateWindowEx failed (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(Marshal.GetLastWin32Error());
        defaultInterpolatedStringHandler.AppendLiteral(")");
        throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
      }
      HeyClickyWindowOpeningFlight.SetLayeredWindowAttributes(intPtr, 0U, byte.MaxValue, 2U);
      if (excludeFromCapture && !OverlayWindow.ShowInScreenRecordings)
      {
        HeyClickyWindowOpeningFlight.SetWindowDisplayAffinity(intPtr, 17U);
      }
      return intPtr;
    }

    // Token: 0x06001264 RID: 4708 RVA: 0x00596FE0 File Offset: 0x00596FE0
    private unsafe static DesktopWindowTarget CreateTarget(Compositor compositor, IntPtr window)
    {
      IntPtr thisPtr = ((IWinRTObject)compositor).NativeObject.ThisPtr;
      Guid guid = new Guid("29E691FA-4567-4DCA-B319-D0F207EB6807");
      IntPtr intPtr;
      Marshal.ThrowExceptionForHR(Marshal.QueryInterface(thisPtr, in guid, out intPtr));
      IntPtr intPtr2;
      try
      {
        delegate* unmanaged[Stdcall]<IntPtr, IntPtr, int, IntPtr*, int> system.Int32_u0020(System.IntPtr,System.IntPtr,System.Int32,System.IntPtr*) = *(*intPtr + (IntPtr)3 * (IntPtr)sizeof(IntPtr));
        delegate* unmanaged[Stdcall]<IntPtr, IntPtr, int, IntPtr*, int> system.Int32_u0020(System.IntPtr,System.IntPtr,System.Int32,System.IntPtr*)2 = system.Int32_u0020(System.IntPtr,System.IntPtr,System.Int32,System.IntPtr*);
        Marshal.ThrowExceptionForHR(calli(System.Int32(System.IntPtr,System.IntPtr,System.Int32,System.IntPtr*), intPtr, window, 1, &intPtr2, system.Int32_u0020(System.IntPtr,System.IntPtr,System.Int32,System.IntPtr*)2));
      }
      finally
      {
        Marshal.Release(intPtr);
      }
      DesktopWindowTarget desktopWindowTarget;
      try
      {
        desktopWindowTarget = DesktopWindowTarget.FromAbi(intPtr2);
      }
      finally
      {
        Marshal.Release(intPtr2);
      }
      return desktopWindowTarget;
    }

    // Token: 0x06001265 RID: 4709
    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern ushort RegisterClassExW(ref HeyClickyWindowOpeningFlight.WNDCLASSEXW wc);

    // Token: 0x06001266 RID: 4710
    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr CreateWindowExW(uint exStyle, string className, string title, uint style, int x, int y, int w, int h, IntPtr parent, IntPtr menu, IntPtr instance, IntPtr param);

    // Token: 0x06001267 RID: 4711
    [DllImport("user32.dll")]
    private static extern bool DestroyWindow(IntPtr hwnd);

    // Token: 0x06001268 RID: 4712
    [DllImport("user32.dll")]
    private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint key, byte alpha, uint flags);

    // Token: 0x06001269 RID: 4713
    [DllImport("user32.dll")]
    private static extern bool SetWindowDisplayAffinity(IntPtr hwnd, uint affinity);

    // Token: 0x0600126A RID: 4714
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr GetModuleHandleW(IntPtr name);

    // Token: 0x04000853 RID: 2131
    private const double NotchBottomRadius = 22.0;

    // Token: 0x04000854 RID: 2132
    private const double WindowCornerRadius = 8.0;

    // Token: 0x04000855 RID: 2133
    private const double DropletWidth = 224.0;

    // Token: 0x04000856 RID: 2134
    private const double DropletHeight = 46.0;

    // Token: 0x04000857 RID: 2135
    private const double CoverDuration = 0.09;

    // Token: 0x04000858 RID: 2136
    private const double FoldDelay = 0.06;

    // Token: 0x04000859 RID: 2137
    private const double FoldDuration = 0.25;

    // Token: 0x0400085A RID: 2138
    private const double RevealFadeDuration = 0.3;

    // Token: 0x0400085B RID: 2139
    private const int ShadowRings = 14;

    // Token: 0x0400085C RID: 2140
    private const double ShadowStep = 2.2;

    // Token: 0x0400085D RID: 2141
    private const double ShadowDrop = 7.0;

    // Token: 0x04000863 RID: 2147
    private readonly Compositor c;

    // Token: 0x04000864 RID: 2148
    private readonly IntPtr hwnd;

    // Token: 0x04000865 RID: 2149
    private readonly DesktopWindowTarget target;

    // Token: 0x04000866 RID: 2150
    private readonly ContainerVisual blob;

    // Token: 0x04000867 RID: 2151
    private readonly CompositionPropertySet p;

    // Token: 0x04000868 RID: 2152
    private readonly CompositionColorBrush fill;

    // Token: 0x04000869 RID: 2153
    private readonly ShapeVisual shadowShapes;

    // Token: 0x0400086A RID: 2154
    private readonly CompositionRoundedRectangleGeometry shape;

    // Token: 0x0400086B RID: 2155
    private readonly int originX;

    // Token: 0x0400086C RID: 2156
    private readonly int originY;

    // Token: 0x0400086D RID: 2157
    private readonly double scale;

    // Token: 0x0400086E RID: 2158
    private readonly IntPtr notchWindow;

    // Token: 0x0400086F RID: 2159
    private bool notchHidden;

    // Token: 0x04000870 RID: 2160
    private readonly Color windowGround;

    // Token: 0x04000871 RID: 2161
    private readonly DateTime startedAt = DateTime.UtcNow;

    // Token: 0x04000872 RID: 2162
    private volatile bool disposed;

    // Token: 0x04000873 RID: 2163
    private const double CoverTimeout = 0.3;

    // Token: 0x04000874 RID: 2164
    private readonly List<Action> afterCover = new List<Action>();

    // Token: 0x04000875 RID: 2165
    private bool covered;

    // Token: 0x04000876 RID: 2166
    private readonly object notchGate = new object();

    // Token: 0x04000877 RID: 2167
    [Nullable(2)]
    private Timer watchdog;

    // Token: 0x04000878 RID: 2168
    private volatile bool revealing;

    // Token: 0x04000879 RID: 2169
    private const string ClassName = "HeyClickyWindowFlight";

    // Token: 0x0400087A RID: 2170
    [Nullable(2)]
    private static HeyClickyWindowOpeningFlight.WndProcDelegate procedure;

    // Token: 0x0400087B RID: 2171
    private static bool registered;

    // Token: 0x020006A6 RID: 1702
    // (Invoke) Token: 0x060032ED RID: 13037
    [NullableContext(0)]
    private delegate IntPtr WndProcDelegate(IntPtr hwnd, uint message, IntPtr wParam, IntPtr lParam);

    // Token: 0x020006A7 RID: 1703
    [NullableContext(0)]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct WNDCLASSEXW
    {
      // Token: 0x04001E4C RID: 7756
      public uint cbSize;

      // Token: 0x04001E4D RID: 7757
      public uint style;

      // Token: 0x04001E4E RID: 7758
      public IntPtr lpfnWndProc;

      // Token: 0x04001E4F RID: 7759
      public int cbClsExtra;

      // Token: 0x04001E50 RID: 7760
      public int cbWndExtra;

      // Token: 0x04001E51 RID: 7761
      public IntPtr hInstance;

      // Token: 0x04001E52 RID: 7762
      public IntPtr hIcon;

      // Token: 0x04001E53 RID: 7763
      public IntPtr hCursor;

      // Token: 0x04001E54 RID: 7764
      public IntPtr hbrBackground;

      // Token: 0x04001E55 RID: 7765
      [Nullable(2)]
      public string lpszMenuName;

      // Token: 0x04001E56 RID: 7766
      [Nullable(1)]
      public string lpszClassName;

      // Token: 0x04001E57 RID: 7767
      public IntPtr hIconSm;
    }
  }
}
