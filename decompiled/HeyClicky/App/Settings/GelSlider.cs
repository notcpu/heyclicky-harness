using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Shapes;
using Windows.System;
using Windows.UI;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000D1 RID: 209
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class GelSlider : Grid
  {
    // Token: 0x14000023 RID: 35
    // (add) Token: 0x060008FE RID: 2302 RVA: 0x00542CD8 File Offset: 0x00542CD8
    // (remove) Token: 0x060008FF RID: 2303 RVA: 0x00542D10 File Offset: 0x00542D10
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<double> ValueChanged;

    // Token: 0x17000174 RID: 372
    // (get) Token: 0x06000900 RID: 2304 RVA: 0x00542D48 File Offset: 0x00542D48
    public double Value
    {
      get
      {
        return this.value;
      }
    }

    // Token: 0x06000901 RID: 2305 RVA: 0x00542D50 File Offset: 0x00542D50
    public GelSlider(double value, double min, double max, GelAppearance gel, Color glow, string accessibleName)
    {
      GelSlider <>4__this = this;
      this.min = min;
      this.max = max;
      this.gel = gel;
      this.value = Math.Clamp(value, min, max);
      base.Height = 30.0;
      base.Background = Ui.Transparent;
      base.IsTabStop = true;
      base.UseSystemFocusVisuals = true;
      AutomationProperties.SetName(this, accessibleName);
      base.ProtectedCursor = Pressable.ClickableCursor;
      base.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateRailsX;
      Border border = new Border
      {
        Height = 26.0,
        CornerRadius = new CornerRadius(8.0),
        Background = Ui.Brush(DS.Black(0.42)),
        BorderBrush = Ui.Brush(SK.Ink.WithAlpha(0.05)),
        BorderThickness = new Thickness(1.0),
        VerticalAlignment = VerticalAlignment.Center
      };
      base.Children.Add(border);
      this.fillHost = new Grid
      {
        Height = 20.0,
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Center,
        Margin = new Thickness(3.0, 0.0, 0.0, 0.0),
        IsHitTestVisible = false
      };
      this.fillHost.Children.Add(new ShadowLayer(6.0, glow.WithAlpha(0.4), 6.0, 0.0, 0.0));
      this.fillHost.Children.Add(new GelSurface(gel, 6.0));
      base.Children.Add(this.fillHost);
      this.dots.VerticalAlignment = VerticalAlignment.Center;
      base.Children.Add(this.dots);
      this.knob = new Grid
      {
        Width = 30.0,
        Height = 30.0,
        HorizontalAlignment = HorizontalAlignment.Left,
        IsHitTestVisible = false
      };
      this.knob.Children.Add(new ShadowLayer(15.0, DS.Black(0.35), 4.0, 2.0, 0.0));
      this.knob.Children.Add(new Ellipse
      {
        Fill = Ui.Brush(Colors.White)
      });
      TextBlock textBlock = new TextBlock
      {
        Text = "‹›",
        FontSize = 13.0,
        FontWeight = FontWeights.Black,
        Foreground = Ui.Brush(DS.Black(0.8)),
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        Margin = new Thickness(0.0, -3.0, 0.0, 0.0),
        IsTextScaleFactorEnabled = false
      };
      this.knob.Children.Add(textBlock);
      base.Children.Add(this.knob);
      base.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        <>4__this.Layout();
      };
      base.PointerEntered += delegate(object _, PointerRoutedEventArgs _)
      {
        Anim.Scale(<>4__this.knob, 1.06f, 0.15, null, new Vector3?(new Vector3(15f, 15f, 0f)));
      };
      base.PointerExited += delegate(object _, PointerRoutedEventArgs e)
      {
        if (PointerFrames.IsStale(e))
        {
          return;
        }
        if (!<>4__this.dragging)
        {
          Anim.Scale(<>4__this.knob, 1f, 0.15, null, new Vector3?(new Vector3(15f, 15f, 0f)));
        }
      };
      base.PointerPressed += delegate(object _, PointerRoutedEventArgs e)
      {
        if (!e.GetCurrentPoint(<>4__this).Properties.IsLeftButtonPressed && e.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
        {
          return;
        }
        <>4__this.dragging = <>4__this.CapturePointer(e.Pointer);
        <>4__this.moves = 0;
        <>4__this.SetFromX(e.GetCurrentPoint(<>4__this).Position.X);
        e.Handled = true;
      };
      base.PointerMoved += delegate(object _, PointerRoutedEventArgs e)
      {
        if (<>4__this.dragging)
        {
          <>4__this.moves++;
          <>4__this.SetFromX(e.GetCurrentPoint(<>4__this).Position.X);
          e.Handled = true;
        }
      };
      base.PointerReleased += delegate(object _, PointerRoutedEventArgs e)
      {
        if (!<>4__this.dragging)
        {
          return;
        }
        <>4__this.dragging = false;
        <>4__this.ReleasePointerCapture(e.Pointer);
        string text = "settings";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 3);
        defaultInterpolatedStringHandler.AppendLiteral("gel slider \"");
        defaultInterpolatedStringHandler.AppendFormatted(accessibleName);
        defaultInterpolatedStringHandler.AppendLiteral("\": ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(<>4__this.value);
        defaultInterpolatedStringHandler.AppendLiteral(" after a drag of ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(<>4__this.moves);
        defaultInterpolatedStringHandler.AppendLiteral(" moves");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        e.Handled = true;
      };
      base.PointerCaptureLost += delegate(object _, PointerRoutedEventArgs _)
      {
        if (<>4__this.dragging)
        {
          string text2 = "settings";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(31, 2);
          defaultInterpolatedStringHandler2.AppendLiteral("gel slider \"");
          defaultInterpolatedStringHandler2.AppendFormatted(accessibleName);
          defaultInterpolatedStringHandler2.AppendLiteral("\": capture lost at ");
          defaultInterpolatedStringHandler2.AppendFormatted<double>(<>4__this.value);
          Log.Info(text2, defaultInterpolatedStringHandler2.ToStringAndClear());
        }
        <>4__this.dragging = false;
      };
      base.KeyDown += delegate(object _, KeyRoutedEventArgs e)
      {
        VirtualKey virtualKey = e.Key;
        bool flag = virtualKey - VirtualKey.Up <= 1;
        if (flag)
        {
          <>4__this.Set(<>4__this.value + 1.0);
          e.Handled = true;
          return;
        }
        virtualKey = e.Key;
        flag = virtualKey == VirtualKey.Left || virtualKey == VirtualKey.Down;
        if (flag)
        {
          <>4__this.Set(<>4__this.value - 1.0);
          e.Handled = true;
        }
      };
    }

    // Token: 0x17000175 RID: 373
    // (get) Token: 0x06000902 RID: 2306 RVA: 0x0054315C File Offset: 0x0054315C
    private double Progress
    {
      get
      {
        if (this.max <= this.min)
        {
          return 0.0;
        }
        return (this.value - this.min) / (this.max - this.min);
      }
    }

    // Token: 0x06000903 RID: 2307 RVA: 0x00543194 File Offset: 0x00543194
    private void SetFromX(double x)
    {
      double num = Math.Max(1.0, base.ActualWidth - 30.0);
      double num2 = Math.Clamp((x - 15.0) / num, 0.0, 1.0);
      this.Set(Math.Round(this.min + num2 * (this.max - this.min)));
    }

    // Token: 0x06000904 RID: 2308 RVA: 0x00543208 File Offset: 0x00543208
    public void Set(double v)
    {
      v = Math.Clamp(Math.Round(v), this.min, this.max);
      if (v == this.value)
      {
        return;
      }
      this.value = v;
      this.Layout();
      Action<double> valueChanged = this.ValueChanged;
      if (valueChanged == null)
      {
        return;
      }
      valueChanged(v);
    }

    // Token: 0x06000905 RID: 2309 RVA: 0x00543258 File Offset: 0x00543258
    private void Layout()
    {
      double actualWidth = base.ActualWidth;
      if (actualWidth <= 0.0)
      {
        return;
      }
      double num = Math.Max(1.0, actualWidth - 30.0);
      double num2 = num * this.Progress;
      double num3 = num2 + 15.0;
      this.knob.Margin = new Thickness(num2, 0.0, 0.0, 0.0);
      this.fillHost.Width = Math.Max(0.0, num3 - 3.0);
      this.fillHost.Opacity = this.Progress > 0.0;
      int num4 = (int)Math.Round(this.max - this.min) + 1;
      int num5 = ((num4 <= 24) ? Math.Max(2, num4) : 16);
      this.dots.Children.Clear();
      this.dots.Width = actualWidth;
      for (int i = 0; i < num5; i++)
      {
        double num6 = 15.0 + num * (double)i / (double)(num5 - 1);
        Ellipse ellipse = new Ellipse
        {
          Width = 3.2,
          Height = 3.2,
          Fill = Ui.Brush((num6 < num3) ? DS.Black(0.22) : DS.White(0.2))
        };
        Canvas.SetLeft(ellipse, num6 - 1.6);
        Canvas.SetTop(ellipse, 11.4);
        this.dots.Children.Add(ellipse);
      }
    }

    // Token: 0x0400031A RID: 794
    private const double KnobDiameter = 30.0;

    // Token: 0x0400031B RID: 795
    private const double TroughHeight = 26.0;

    // Token: 0x0400031C RID: 796
    private const double FillInset = 3.0;

    // Token: 0x0400031D RID: 797
    private readonly double min;

    // Token: 0x0400031E RID: 798
    private readonly double max;

    // Token: 0x0400031F RID: 799
    private readonly Grid fillHost;

    // Token: 0x04000320 RID: 800
    private readonly Canvas dots = new Canvas
    {
      IsHitTestVisible = false,
      Height = 26.0
    };

    // Token: 0x04000321 RID: 801
    private readonly Grid knob;

    // Token: 0x04000322 RID: 802
    private readonly GelAppearance gel;

    // Token: 0x04000323 RID: 803
    private double value;

    // Token: 0x04000324 RID: 804
    private bool dragging;

    // Token: 0x04000326 RID: 806
    private int moves;
  }
}
