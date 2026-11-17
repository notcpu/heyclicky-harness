using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Onboarding;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace HeyClicky.App.Shell
{
  // Token: 0x020000C0 RID: 192
  [NullableContext(1)]
  [Nullable(0)]
  public class PanelWindow : OverlayWindow
  {
    // Token: 0x17000140 RID: 320
    // (get) Token: 0x060007E3 RID: 2019 RVA: 0x0053B570 File Offset: 0x0053B570
    public Grid Panel { get; }

    // Token: 0x17000141 RID: 321
    // (get) Token: 0x060007E4 RID: 2020 RVA: 0x0053B578 File Offset: 0x0053B578
    public Border Chrome { get; }

    // Token: 0x060007E5 RID: 2021 RVA: 0x0053B580 File Offset: 0x0053B580
    public PanelWindow(string title, bool excludeFromCapture, bool showInTaskbar = false, double cornerRadius = 14.0, Color? fill = null, double borderOpacity = 0.1)
      : base(title, excludeFromCapture, false, true, false, showInTaskbar, false)
    {
      this.monitor = Displays.UnderCursor();
      this.shadow = new ShadowLayer(cornerRadius, DS.Black(0.45), 16.0, 10.0, 0.0)
      {
        Margin = new Thickness(32.0)
      };
      this.Chrome = new Border
      {
        Margin = new Thickness(32.0),
        CornerRadius = new CornerRadius(cornerRadius),
        Background = Ui.Brush(fill ?? DS.Colors.Background)
      };
      this.Panel = new Grid();
      Ui.ClipRounded(this.Panel, cornerRadius);
      Border border = new Border
      {
        CornerRadius = new CornerRadius(cornerRadius),
        BorderBrush = Ui.Brush(DS.White(borderOpacity)),
        BorderThickness = new Thickness(0.5),
        IsHitTestVisible = false
      };
      this.Chrome.Child = new Grid
      {
        Children = { this.Panel, border }
      };
      base.Root.Children.Add(this.shadow);
      base.Root.Children.Add(this.Chrome);
      base.Root.RequestedTheme = ElementTheme.Dark;
      base.MoveByBackground(this.Chrome);
    }

    // Token: 0x17000142 RID: 322
    // (get) Token: 0x060007E6 RID: 2022 RVA: 0x0053B718 File Offset: 0x0053B718
    public double PanelWidth
    {
      get
      {
        return this.panelW;
      }
    }

    // Token: 0x17000143 RID: 323
    // (get) Token: 0x060007E7 RID: 2023 RVA: 0x0053B720 File Offset: 0x0053B720
    public double PanelHeight
    {
      get
      {
        return this.panelH;
      }
    }

    // Token: 0x14000013 RID: 19
    // (add) Token: 0x060007E8 RID: 2024 RVA: 0x0053B728 File Offset: 0x0053B728
    // (remove) Token: 0x060007E9 RID: 2025 RVA: 0x0053B760 File Offset: 0x0053B760
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<double, double> ShownSizeChanged;

    // Token: 0x17000144 RID: 324
    // (get) Token: 0x060007EA RID: 2026 RVA: 0x0053B798 File Offset: 0x0053B798
    public double ShownWidth
    {
      get
      {
        return this.shownW;
      }
    }

    // Token: 0x17000145 RID: 325
    // (get) Token: 0x060007EB RID: 2027 RVA: 0x0053B7A0 File Offset: 0x0053B7A0
    public double ShownHeight
    {
      get
      {
        return this.shownH;
      }
    }

    // Token: 0x17000146 RID: 326
    // (get) Token: 0x060007EC RID: 2028 RVA: 0x0053B7A8 File Offset: 0x0053B7A8
    [TupleElementNames(new string[] { "W", "H" })]
    [Nullable(0)]
    private ValueTuple<double, double> WindowDip
    {
      [NullableContext(0)]
      [return: TupleElementNames(new string[] { "W", "H" })]
      get
      {
        ValueTuple<double, double>? valueTuple = this.envelope;
        if (valueTuple != null)
        {
          ValueTuple<double, double> valueOrDefault = valueTuple.GetValueOrDefault();
          return new ValueTuple<double, double>(valueOrDefault.Item1 + 64.0, valueOrDefault.Item2 + 64.0);
        }
        return new ValueTuple<double, double>(this.panelW + 64.0, this.panelH + 64.0);
      }
    }

    // Token: 0x060007ED RID: 2029 RVA: 0x0053B81C File Offset: 0x0053B81C
    public void UseFixedEnvelope(double maxWidth, double maxHeight)
    {
      this.envelope = new ValueTuple<double, double>?(new ValueTuple<double, double>(maxWidth, maxHeight));
      foreach (FrameworkElement frameworkElement in new FrameworkElement[] { this.Chrome, this.shadow })
      {
        frameworkElement.HorizontalAlignment = HorizontalAlignment.Center;
        frameworkElement.VerticalAlignment = VerticalAlignment.Center;
      }
      if (this.panelW > 0.0)
      {
        this.ApplyShownSize(this.panelW, this.panelH);
      }
    }

    // Token: 0x060007EE RID: 2030 RVA: 0x0053B898 File Offset: 0x0053B898
    public void SetPanelSize(double width, double height)
    {
      if (Math.Abs(width - this.panelW) < 0.5 && Math.Abs(height - this.panelH) < 0.5 && !this.animating)
      {
        return;
      }
      this.StopAnimating();
      this.panelW = width;
      this.panelH = height;
      this.ApplyShownSize(width, height);
      if (base.IsShown && !OverlayWindow.Offscreen)
      {
        this.monitor = Displays.ForWindow(base.Hwnd);
      }
      ValueTuple<double, double> windowDip = this.WindowDip;
      double item = windowDip.Item1;
      double item2 = windowDip.Item2;
      ValueTuple<int, int, int, int> valueTuple = Displays.CenteredInWork(this.monitor, item, item2);
      int item3 = valueTuple.Item1;
      int item4 = valueTuple.Item2;
      int item5 = valueTuple.Item3;
      int item6 = valueTuple.Item4;
      base.PlacePx(item3, item4, item5, item6);
      this.UpdateRegion(null, null);
    }

    // Token: 0x060007EF RID: 2031 RVA: 0x0053B980 File Offset: 0x0053B980
    public void AnimatePanelSize(double width, double height, SwiftSpring spring)
    {
      ValueTuple<double, double>? valueTuple = this.envelope;
      if (valueTuple == null || !base.IsShown || Anim.Instant || this.shownW <= 0.0)
      {
        this.SetPanelSize(width, height);
        return;
      }
      if (Math.Abs(width - this.panelW) < 0.5 && Math.Abs(height - this.panelH) < 0.5)
      {
        return;
      }
      this.panelW = width;
      this.panelH = height;
      if (Anim.ReduceMotion)
      {
        this.springW = (this.springH = null);
        this.easeStarted = -1.0;
        this.easeFromW = this.shownW;
        this.easeFromH = this.shownH;
      }
      else
      {
        if (this.springW == null)
        {
          this.springW = new SpringAxis(spring.Response, spring.DampingFraction, this.shownW);
        }
        if (this.springH == null)
        {
          this.springH = new SpringAxis(spring.Response, spring.DampingFraction, this.shownH);
        }
        this.springW.Target = width;
        this.springH.Target = height;
      }
      if (!OverlayWindow.Offscreen)
      {
        this.monitor = Displays.ForWindow(base.Hwnd);
      }
      ValueTuple<double, double> windowDip = this.WindowDip;
      double item = windowDip.Item1;
      double item2 = windowDip.Item2;
      ValueTuple<int, int, int, int> valueTuple2 = Displays.CenteredInWork(this.monitor, item, item2);
      int item3 = valueTuple2.Item1;
      int item4 = valueTuple2.Item2;
      int item5 = valueTuple2.Item3;
      int item6 = valueTuple2.Item4;
      base.PlacePx(item3, item4, item5, item6);
      this.UpdateRegion(new double?(Math.Max(this.shownW, width)), new double?(Math.Max(this.shownH, height)));
      if (!this.animating)
      {
        this.animating = true;
        this.lastFrame = null;
        this.frames = 0;
        this.longestFrameGap = 0.0;
        this.animationStarted = Environment.TickCount64;
        this.animatedFrom = new ValueTuple<double, double>(this.shownW, this.shownH);
        CompositionTarget.Rendering += this.OnFrame;
      }
    }

    // Token: 0x060007F0 RID: 2032 RVA: 0x0053BB9C File Offset: 0x0053BB9C
    private void OnFrame([Nullable(2)] object sender, object e)
    {
      RenderingEventArgs e2 = e as RenderingEventArgs;
      TimeSpan timeSpan = ((e2 != null) ? e2.RenderingTime : TimeSpan.FromMilliseconds(Environment.TickCount64, 0L));
      TimeSpan? timeSpan2 = this.lastFrame;
      double num;
      if (timeSpan2 != null)
      {
        TimeSpan valueOrDefault = timeSpan2.GetValueOrDefault();
        num = (timeSpan - valueOrDefault).TotalSeconds;
      }
      else
      {
        num = 0.0;
      }
      double num2 = num;
      double num3 = Math.Clamp(num2, 0.0, 0.05);
      this.lastFrame = new TimeSpan?(timeSpan);
      int num4 = this.frames;
      this.frames = num4 + 1;
      if (num4 == 0)
      {
        this.firstFrameMs = Environment.TickCount64 - this.animationStarted;
      }
      this.longestFrameGap = Math.Max(this.longestFrameGap, num2);
      SpringAxis springAxis = this.springW;
      bool flag;
      double num5;
      double num6;
      if (springAxis != null)
      {
        SpringAxis springAxis2 = this.springH;
        if (springAxis2 != null)
        {
          springAxis.Step(num3);
          springAxis2.Step(num3);
          flag = springAxis.IsSettled && springAxis2.IsSettled;
          if (flag)
          {
            springAxis.Settle();
            springAxis2.Settle();
          }
          num5 = springAxis.Value;
          num6 = springAxis2.Value;
          goto IL_01AC;
        }
      }
      if (this.easeStarted < 0.0)
      {
        this.easeStarted = timeSpan.TotalSeconds;
      }
      double num7 = Math.Min(1.0, (timeSpan.TotalSeconds - this.easeStarted) / 0.12);
      double num8 = TourMotion.EaseOut(num7);
      num5 = this.easeFromW + (this.panelW - this.easeFromW) * num8;
      num6 = this.easeFromH + (this.panelH - this.easeFromH) * num8;
      flag = num7 >= 1.0;
      IL_01AC:
      this.ApplyShownSize(num5, num6);
      if (!flag)
      {
        return;
      }
      this.StopAnimating();
      this.ApplyShownSize(this.panelW, this.panelH);
      this.UpdateRegion(null, null);
      double num9 = (double)(Environment.TickCount64 - this.animationStarted) / 1000.0;
      string text = "panel";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(82, 10);
      defaultInterpolatedStringHandler.AppendFormatted(base.Title);
      defaultInterpolatedStringHandler.AppendLiteral(": ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(this.animatedFrom.Item1, "0");
      defaultInterpolatedStringHandler.AppendLiteral("x");
      defaultInterpolatedStringHandler.AppendFormatted<double>(this.animatedFrom.Item2, "0");
      defaultInterpolatedStringHandler.AppendLiteral(" → ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(this.panelW, "0");
      defaultInterpolatedStringHandler.AppendLiteral("x");
      defaultInterpolatedStringHandler.AppendFormatted<double>(this.panelH, "0");
      defaultInterpolatedStringHandler.AppendLiteral(" in ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(num9, "0.00");
      defaultInterpolatedStringHandler.AppendLiteral(" s, ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(this.frames);
      defaultInterpolatedStringHandler.AppendLiteral(" frames ");
      defaultInterpolatedStringHandler.AppendLiteral("(");
      defaultInterpolatedStringHandler.AppendFormatted<double>((num9 > 0.0) ? ((double)this.frames / num9) : 0.0, "0");
      defaultInterpolatedStringHandler.AppendLiteral(" fps, first frame ");
      defaultInterpolatedStringHandler.AppendFormatted<long>(this.firstFrameMs);
      defaultInterpolatedStringHandler.AppendLiteral(" ms after the change, longest frame ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(this.longestFrameGap * 1000.0, "0");
      defaultInterpolatedStringHandler.AppendLiteral(" ms)");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x060007F1 RID: 2033 RVA: 0x0053BF20 File Offset: 0x0053BF20
    private void StopAnimating()
    {
      if (this.animating)
      {
        CompositionTarget.Rendering -= this.OnFrame;
      }
      this.animating = false;
      this.springW = (this.springH = null);
      this.easeStarted = -1.0;
    }

    // Token: 0x060007F2 RID: 2034 RVA: 0x0053BF6C File Offset: 0x0053BF6C
    private void ApplyShownSize(double width, double height)
    {
      this.shownW = width;
      this.shownH = height;
      this.Chrome.Width = width;
      this.Chrome.Height = height;
      ValueTuple<double, double>? valueTuple = this.envelope;
      if (valueTuple != null)
      {
        this.shadow.Width = width;
        this.shadow.Height = height;
      }
      Action<double, double> shownSizeChanged = this.ShownSizeChanged;
      if (shownSizeChanged == null)
      {
        return;
      }
      shownSizeChanged(width, height);
    }

    // Token: 0x060007F3 RID: 2035 RVA: 0x0053BFDC File Offset: 0x0053BFDC
    private void UpdateRegion(double? width = null, double? height = null)
    {
      ValueTuple<double, double>? valueTuple = this.envelope;
      if (valueTuple == null || OverlayWindow.Offscreen)
      {
        return;
      }
      ValueTuple<int, int, int, int> rectPx = base.RectPx;
      int item = rectPx.Item3;
      int item2 = rectPx.Item4;
      ValueTuple<int, int, int, int> valueTuple2 = TourMotion.CenteredRegionPx(item, item2, base.Scale, width ?? this.panelW, height ?? this.panelH, 32.0);
      int item3 = valueTuple2.Item1;
      int item4 = valueTuple2.Item2;
      int item5 = valueTuple2.Item3;
      int item6 = valueTuple2.Item4;
      base.SetRegionPx(item3, item4, item5, item6);
    }

    // Token: 0x060007F4 RID: 2036 RVA: 0x0053C08C File Offset: 0x0053C08C
    public void BringToFrontRecentered()
    {
      this.monitor = Displays.ForWindow(this);
      ValueTuple<double, double> windowDip = this.WindowDip;
      double item = windowDip.Item1;
      double item2 = windowDip.Item2;
      ValueTuple<int, int, int, int> valueTuple = Displays.CenteredInWork(this.monitor, item, item2);
      int item3 = valueTuple.Item1;
      int item4 = valueTuple.Item2;
      int item5 = valueTuple.Item3;
      int item6 = valueTuple.Item4;
      base.PlacePx(item3, item4, item5, item6);
      base.ShowActivated();
      this.UpdateRegion(null, null);
    }

    // Token: 0x060007F5 RID: 2037 RVA: 0x0053C110 File Offset: 0x0053C110
    public void ShowCentered()
    {
      this.monitor = Displays.UnderCursor();
      ValueTuple<double, double> windowDip = this.WindowDip;
      double item = windowDip.Item1;
      double item2 = windowDip.Item2;
      ValueTuple<int, int, int, int> valueTuple = Displays.CenteredInWork(this.monitor, item, item2);
      int item3 = valueTuple.Item1;
      int item4 = valueTuple.Item2;
      int item5 = valueTuple.Item3;
      int item6 = valueTuple.Item4;
      base.PlacePx(item3, item4, item5, item6);
      base.ShowActivated();
      this.UpdateRegion(null, null);
    }

    // Token: 0x0400027F RID: 639
    public const double ShadowMargin = 32.0;

    // Token: 0x04000282 RID: 642
    private double panelW;

    // Token: 0x04000283 RID: 643
    private double panelH;

    // Token: 0x04000284 RID: 644
    private Displays.Monitor monitor;

    // Token: 0x04000285 RID: 645
    private readonly ShadowLayer shadow;

    // Token: 0x04000286 RID: 646
    [TupleElementNames(new string[] { "W", "H" })]
    [Nullable(0)]
    private ValueTuple<double, double>? envelope;

    // Token: 0x04000287 RID: 647
    private double shownW;

    // Token: 0x04000288 RID: 648
    private double shownH;

    // Token: 0x04000289 RID: 649
    [Nullable(2)]
    private SpringAxis springW;

    // Token: 0x0400028A RID: 650
    [Nullable(2)]
    private SpringAxis springH;

    // Token: 0x0400028B RID: 651
    private TimeSpan? lastFrame;

    // Token: 0x0400028C RID: 652
    private double easeStarted = -1.0;

    // Token: 0x0400028D RID: 653
    private double easeFromW;

    // Token: 0x0400028E RID: 654
    private double easeFromH;

    // Token: 0x0400028F RID: 655
    private bool animating;

    // Token: 0x04000291 RID: 657
    private int frames;

    // Token: 0x04000292 RID: 658
    private long firstFrameMs;

    // Token: 0x04000293 RID: 659
    private double longestFrameGap;

    // Token: 0x04000294 RID: 660
    private long animationStarted;

    // Token: 0x04000295 RID: 661
    [TupleElementNames(new string[] { "W", "H" })]
    [Nullable(0)]
    private ValueTuple<double, double> animatedFrom;
  }
}
