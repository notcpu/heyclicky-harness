using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.UI
{
  // Token: 0x020000A5 RID: 165
  [NullableContext(2)]
  [Nullable(0)]
  public class Pressable : Grid
  {
    // Token: 0x1400000C RID: 12
    // (add) Token: 0x060006DF RID: 1759 RVA: 0x00533408 File Offset: 0x00533408
    // (remove) Token: 0x060006E0 RID: 1760 RVA: 0x00533440 File Offset: 0x00533440
    public event Action Click;

    // Token: 0x1400000D RID: 13
    // (add) Token: 0x060006E1 RID: 1761 RVA: 0x00533478 File Offset: 0x00533478
    // (remove) Token: 0x060006E2 RID: 1762 RVA: 0x005334B0 File Offset: 0x005334B0
    public event Action<bool> HoverChanged;

    // Token: 0x1400000E RID: 14
    // (add) Token: 0x060006E3 RID: 1763 RVA: 0x005334E8 File Offset: 0x005334E8
    // (remove) Token: 0x060006E4 RID: 1764 RVA: 0x00533520 File Offset: 0x00533520
    public event Action<bool> PressedChanged;

    // Token: 0x17000118 RID: 280
    // (get) Token: 0x060006E5 RID: 1765 RVA: 0x00533558 File Offset: 0x00533558
    // (set) Token: 0x060006E6 RID: 1766 RVA: 0x00533560 File Offset: 0x00533560
    public bool IsHovered { get; private set; }

    // Token: 0x17000119 RID: 281
    // (get) Token: 0x060006E7 RID: 1767 RVA: 0x0053356C File Offset: 0x0053356C
    // (set) Token: 0x060006E8 RID: 1768 RVA: 0x00533574 File Offset: 0x00533574
    public bool IsPressed { get; private set; }

    // Token: 0x060006E9 RID: 1769 RVA: 0x00533580 File Offset: 0x00533580
    private void SetPressedLook(bool pressed)
    {
      if (this.IsPressed == pressed)
      {
        return;
      }
      this.IsPressed = pressed;
      Action<bool> pressedChanged = this.PressedChanged;
      if (pressedChanged != null)
      {
        pressedChanged(pressed);
      }
      this.ShowDefaultHover();
    }

    // Token: 0x1700011A RID: 282
    // (get) Token: 0x060006EA RID: 1770 RVA: 0x005335AC File Offset: 0x005335AC
    // (set) Token: 0x060006EB RID: 1771 RVA: 0x005335B4 File Offset: 0x005335B4
    public bool ShowsDefaultHover { get; set; } = true;

    // Token: 0x060006EC RID: 1772 RVA: 0x005335C0 File Offset: 0x005335C0
    private void ShowDefaultHover()
    {
      bool flag = this.IsHovered && this.enabled && this.looksClickable && this.ShowsDefaultHover && this.HoverChanged == null && base.ActualWidth <= 640.0 && base.ActualHeight <= 360.0;
      if (!flag && this.hoverFill == null && this.hoverGel == null)
      {
        return;
      }
      if (this.hoverGel != null && this.hoverGel.Parent != this)
      {
        this.hoverGel = null;
      }
      if (this.hoverGel == null)
      {
        this.hoverGel = (flag ? base.Children.OfType<GelSurface>().FirstOrDefault<GelSurface>() : null);
      }
      if (this.hoverGel != null)
      {
        this.hoverGel.Hovered = flag;
        return;
      }
      if (this.hoverFill != null && this.hoverFill.Parent != this)
      {
        this.hoverFill = null;
      }
      if (this.hoverFill == null)
      {
        ValueTuple<bool, CornerRadius> valueTuple = this.HoverSurface();
        bool item = valueTuple.Item1;
        CornerRadius item2 = valueTuple.Item2;
        this.hoverFill = new Border
        {
          IsHitTestVisible = false,
          Opacity = 0.0,
          CornerRadius = item2,
          Background = Ui.Brush(item ? DS.White(0.06) : DS.Black(0.035))
        };
        base.Children.Add(this.hoverFill);
      }
      Anim.Opacity(this.hoverFill, (!flag) ? 0f : (this.IsPressed ? 0.67f : 1f), 0.083, Easing.EaseOut(Ui.Compositor), 0.0);
    }

    // Token: 0x060006ED RID: 1773 RVA: 0x0053376C File Offset: 0x0053376C
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "OnDark", "Corners" })]
    private ValueTuple<bool, CornerRadius> HoverSurface()
    {
      bool flag = base.ActualTheme != ElementTheme.Light;
      CornerRadius cornerRadius = base.CornerRadius;
      Queue<ValueTuple<DependencyObject, int>> queue = new Queue<ValueTuple<DependencyObject, int>>();
      for (int i = 0; i < base.Children.Count; i++)
      {
        queue.Enqueue(new ValueTuple<DependencyObject, int>(base.Children[i], 1));
      }
      while (queue.Count > 0)
      {
        ValueTuple<DependencyObject, int> valueTuple = queue.Dequeue();
        DependencyObject item = valueTuple.Item1;
        int item2 = valueTuple.Item2;
        FrameworkElement frameworkElement = item as FrameworkElement;
        if (frameworkElement != null && frameworkElement.Visibility == Visibility.Visible && frameworkElement.ActualWidth >= base.ActualWidth * 0.7 && frameworkElement.ActualHeight >= base.ActualHeight * 0.7)
        {
          Border border = frameworkElement as Border;
          ValueTuple<Brush, CornerRadius> valueTuple2;
          if (border == null)
          {
            Panel panel = frameworkElement as Panel;
            if (panel == null)
            {
              Control control = frameworkElement as Control;
              if (control == null)
              {
                valueTuple2 = new ValueTuple<Brush, CornerRadius>(null, default(CornerRadius));
              }
              else
              {
                valueTuple2 = new ValueTuple<Brush, CornerRadius>(control.Background, control.CornerRadius);
              }
            }
            else
            {
              Brush background = panel.Background;
              Grid grid = panel as Grid;
              valueTuple2 = new ValueTuple<Brush, CornerRadius>(background, (grid != null) ? grid.CornerRadius : default(CornerRadius));
            }
          }
          else
          {
            valueTuple2 = new ValueTuple<Brush, CornerRadius>(border.Background, border.CornerRadius);
          }
          ValueTuple<Brush, CornerRadius> valueTuple3 = valueTuple2;
          Brush item3 = valueTuple3.Item1;
          CornerRadius item4 = valueTuple3.Item2;
          SolidColorBrush solidColorBrush = item3 as SolidColorBrush;
          Color? color;
          if (solidColorBrush == null)
          {
            GradientBrush gradientBrush = item3 as GradientBrush;
            if (gradientBrush != null)
            {
              GradientStopCollection gradientStops = gradientBrush.GradientStops;
              if (gradientStops != null)
              {
                if (gradientStops.Count > 0)
                {
                  color = new Color?(gradientBrush.GradientStops[0].Color);
                  goto IL_01CA;
                }
              }
            }
            color = null;
          }
          else
          {
            color = new Color?(solidColorBrush.Color);
          }
          IL_01CA:
          Color? color2 = color;
          if (color2 != null)
          {
            Color valueOrDefault = color2.GetValueOrDefault();
            if (valueOrDefault.A > 200)
            {
              flag = 0.2126 * (double)valueOrDefault.R + 0.7152 * (double)valueOrDefault.G + 0.0722 * (double)valueOrDefault.B < 140.0;
              if (cornerRadius == default(CornerRadius))
              {
                cornerRadius = item4;
                break;
              }
              break;
            }
          }
        }
        if (item2 < 3)
        {
          for (int j = 0; j < VisualTreeHelper.GetChildrenCount(item); j++)
          {
            queue.Enqueue(new ValueTuple<DependencyObject, int>(VisualTreeHelper.GetChild(item, j), item2 + 1));
          }
        }
      }
      if (cornerRadius == default(CornerRadius))
      {
        cornerRadius = new CornerRadius(Math.Min(6.0, Math.Min(base.ActualWidth, base.ActualHeight) / 2.0));
      }
      return new ValueTuple<bool, CornerRadius>(flag, cornerRadius);
    }

    // Token: 0x060006EE RID: 1774 RVA: 0x00533A4C File Offset: 0x00533A4C
    public Pressable(bool handCursor = true)
    {
      base.Background = Ui.Transparent;
      this.looksClickable = handCursor;
      base.PointerEntered += delegate(object _, PointerRoutedEventArgs _)
      {
        this.IsHovered = true;
        Action<bool> hoverChanged = this.HoverChanged;
        if (hoverChanged != null)
        {
          hoverChanged(true);
        }
        this.ShowDefaultHover();
      };
      base.PointerExited += delegate(object _, PointerRoutedEventArgs e)
      {
        if (PointerFrames.IsStale(e))
        {
          return;
        }
        this.IsHovered = false;
        Action<bool> hoverChanged2 = this.HoverChanged;
        if (hoverChanged2 != null)
        {
          hoverChanged2(false);
        }
        this.SetPressedLook(false);
        this.ShowDefaultHover();
      };
      base.PointerPressed += delegate(object _, PointerRoutedEventArgs e)
      {
        if (!this.enabled || !e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
          return;
        }
        base.CapturePointer(e.Pointer);
        this.pressArmed = true;
        this.SetPressedLook(true);
        e.Handled = true;
      };
      base.PointerReleased += delegate(object _, PointerRoutedEventArgs e)
      {
        if (!this.pressArmed)
        {
          return;
        }
        this.pressArmed = false;
        base.ReleasePointerCapture(e.Pointer);
        this.SetPressedLook(false);
        Point position = e.GetCurrentPoint(this).Position;
        if (this.enabled && position.X >= 0.0 && position.Y >= 0.0 && position.X <= base.ActualWidth && position.Y <= base.ActualHeight)
        {
          Action click = this.Click;
          if (click != null)
          {
            click();
          }
        }
        e.Handled = true;
      };
      base.PointerCaptureLost += delegate(object _, PointerRoutedEventArgs _)
      {
        this.SetPressedLook(false);
      };
      base.RegisterPropertyChangedCallback(Grid.CornerRadiusProperty, delegate(DependencyObject _, DependencyProperty _)
      {
        if (!this.fittingCorners)
        {
          this.requestedCorners = base.CornerRadius;
          this.FitCorners();
        }
      });
      base.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        this.FitCorners();
      };
    }

    // Token: 0x060006EF RID: 1775 RVA: 0x00533B08 File Offset: 0x00533B08
    private void FitCorners()
    {
      double num = Math.Min(base.ActualWidth, base.ActualHeight) / 2.0;
      if (num <= 0.0)
      {
        return;
      }
      CornerRadius cornerRadius = this.requestedCorners;
      CornerRadius cornerRadius2 = new CornerRadius(Math.Min(cornerRadius.TopLeft, num), Math.Min(cornerRadius.TopRight, num), Math.Min(cornerRadius.BottomRight, num), Math.Min(cornerRadius.BottomLeft, num));
      if (cornerRadius2 == base.CornerRadius)
      {
        return;
      }
      this.fittingCorners = true;
      base.CornerRadius = cornerRadius2;
      this.fittingCorners = false;
    }

    // Token: 0x1700011B RID: 283
    // (get) Token: 0x060006F0 RID: 1776 RVA: 0x00533BA4 File Offset: 0x00533BA4
    // (set) Token: 0x060006F1 RID: 1777 RVA: 0x00533BAC File Offset: 0x00533BAC
    public bool Enabled
    {
      get
      {
        return this.enabled;
      }
      set
      {
        bool flag = this.enabled != value;
        this.enabled = value;
        if (flag)
        {
          this.ShowDefaultHover();
        }
      }
    }

    // Token: 0x1700011C RID: 284
    // (get) Token: 0x060006F2 RID: 1778 RVA: 0x00533BCC File Offset: 0x00533BCC
    internal static InputCursor ClickableCursor
    {
      get
      {
        return null;
      }
    }

    // Token: 0x1700011D RID: 285
    // (get) Token: 0x060006F3 RID: 1779 RVA: 0x00533BD0 File Offset: 0x00533BD0
    [Nullable(1)]
    internal static InputCursor HandCursor
    {
      [NullableContext(1)]
      get
      {
        InputCursor inputCursor;
        if ((inputCursor = Pressable.sharedHand) == null)
        {
          inputCursor = (Pressable.sharedHand = InputSystemCursor.Create(InputSystemCursorShape.Hand));
        }
        return inputCursor;
      }
    }

    // Token: 0x1700011E RID: 286
    // (get) Token: 0x060006F4 RID: 1780 RVA: 0x00533BE8 File Offset: 0x00533BE8
    [Nullable(1)]
    internal static InputCursor ArrowCursor
    {
      [NullableContext(1)]
      get
      {
        InputCursor inputCursor;
        if ((inputCursor = Pressable.sharedArrow) == null)
        {
          inputCursor = (Pressable.sharedArrow = InputSystemCursor.Create(InputSystemCursorShape.Arrow));
        }
        return inputCursor;
      }
    }

    // Token: 0x060006F5 RID: 1781 RVA: 0x00533C00 File Offset: 0x00533C00
    public void RaiseClick()
    {
      Action click = this.Click;
      if (click == null)
      {
        return;
      }
      click();
    }

    // Token: 0x1700011F RID: 287
    // (get) Token: 0x060006F6 RID: 1782 RVA: 0x00533C14 File Offset: 0x00533C14
    // (set) Token: 0x060006F7 RID: 1783 RVA: 0x00533C1C File Offset: 0x00533C1C
    public bool? AccessibleToggleState { get; set; }

    // Token: 0x060006F8 RID: 1784 RVA: 0x00533C28 File Offset: 0x00533C28
    [NullableContext(1)]
    protected override AutomationPeer OnCreateAutomationPeer()
    {
      return new PressableAutomationPeer(this);
    }

    // Token: 0x060006F9 RID: 1785 RVA: 0x00533C30 File Offset: 0x00533C30
    [NullableContext(1)]
    public Pressable WithPressScale(float scale = 0.97f, double seconds = 0.14, bool easeOut = false)
    {
      this.PressedChanged += delegate(bool p)
      {
        this.CenterPoint = new Vector3((float)this.ActualWidth / 2f, (float)this.ActualHeight / 2f, 0f);
        Anim.Scale(this, p ? scale : 1f, seconds, easeOut ? Easing.EaseOut(Ui.Compositor) : Easing.Expressive(Ui.Compositor), null);
      };
      return this;
    }

    // Token: 0x060006FA RID: 1786 RVA: 0x00533C74 File Offset: 0x00533C74
    [NullableContext(1)]
    public Pressable WithIconHover(double diameter)
    {
      ShadowLayer shadow = new ShadowLayer(diameter / 2.0, DS.Black(0.18), 5.0, 1.0, 0.0)
      {
        Width = diameter,
        Height = diameter,
        Opacity = 0.0
      };
      Ellipse plate = new Ellipse
      {
        Width = diameter,
        Height = diameter,
        Fill = Ui.Brush(DS.White(0.06)),
        Stroke = Ui.Brush(DS.White(0.14)),
        StrokeThickness = 0.8,
        Opacity = 0.0,
        IsHitTestVisible = false
      };
      foreach (FrameworkElement frameworkElement in new FrameworkElement[] { shadow, plate })
      {
        frameworkElement.HorizontalAlignment = HorizontalAlignment.Center;
        frameworkElement.VerticalAlignment = VerticalAlignment.Center;
      }
      base.Children.Insert(0, plate);
      base.Children.Insert(0, shadow);
      this.HoverChanged += delegate(bool h)
      {
        Anim.Opacity(shadow, h > false, 0.15, Easing.EaseOut(Ui.Compositor), 0.0);
        Anim.Opacity(plate, h > false, 0.15, Easing.EaseOut(Ui.Compositor), 0.0);
        Anim.Scale(this, (h && !Anim.Instant) ? 1.025f : 1f, 0.15, Easing.EaseOut(Ui.Compositor), new Vector3?(new Vector3((float)this.ActualWidth / 2f, (float)this.ActualHeight / 2f, 0f)));
      };
      return this;
    }

    // Token: 0x060006FB RID: 1787 RVA: 0x00533DC8 File Offset: 0x00533DC8
    [NullableContext(1)]
    public Pressable WithHoverScale(float scale, double seconds = 0.15, bool bottomAnchor = false)
    {
      this.HoverChanged += delegate(bool h)
      {
        if (!this.enabled)
        {
          return;
        }
        this.CenterPoint = new Vector3((float)this.ActualWidth / 2f, bottomAnchor ? ((float)this.ActualHeight) : ((float)this.ActualHeight / 2f), 0f);
        Anim.Scale(this, h ? scale : 1f, seconds, Easing.EaseOut(Ui.Compositor), null);
      };
      return this;
    }

    // Token: 0x040001BA RID: 442
    internal const bool UsesHandCursor = false;

    // Token: 0x040001C0 RID: 448
    private bool enabled = true;

    // Token: 0x040001C1 RID: 449
    private bool pressArmed;

    // Token: 0x040001C2 RID: 450
    private readonly bool looksClickable;

    // Token: 0x040001C3 RID: 451
    private Border hoverFill;

    // Token: 0x040001C4 RID: 452
    private GelSurface hoverGel;

    // Token: 0x040001C6 RID: 454
    private const double DefaultHoverMaxWidth = 640.0;

    // Token: 0x040001C7 RID: 455
    private const double DefaultHoverMaxHeight = 360.0;

    // Token: 0x040001C8 RID: 456
    private CornerRadius requestedCorners;

    // Token: 0x040001C9 RID: 457
    private bool fittingCorners;

    // Token: 0x040001CA RID: 458
    private static InputCursor sharedHand;

    // Token: 0x040001CB RID: 459
    private static InputCursor sharedArrow;
  }
}
