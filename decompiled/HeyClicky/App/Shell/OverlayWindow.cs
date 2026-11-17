using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.Interop;
using HeyClicky.App.UI;
using HeyClicky.Platform.Win32;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using WinRT.Interop;

namespace HeyClicky.App.Shell
{
  // Token: 0x020000BF RID: 191
  [NullableContext(1)]
  [Nullable(0)]
  public class OverlayWindow : Window
  {
    // Token: 0x17000135 RID: 309
    // (get) Token: 0x060007BB RID: 1979 RVA: 0x0053A4A8 File Offset: 0x0053A4A8
    public IntPtr Hwnd { get; }

    // Token: 0x17000136 RID: 310
    // (get) Token: 0x060007BC RID: 1980 RVA: 0x0053A4B0 File Offset: 0x0053A4B0
    public AppWindow AppWin { get; }

    // Token: 0x17000137 RID: 311
    // (get) Token: 0x060007BD RID: 1981 RVA: 0x0053A4B8 File Offset: 0x0053A4B8
    public Grid Root { get; }

    // Token: 0x17000138 RID: 312
    // (get) Token: 0x060007BE RID: 1982 RVA: 0x0053A4C0 File Offset: 0x0053A4C0
    // (set) Token: 0x060007BF RID: 1983 RVA: 0x0053A4C8 File Offset: 0x0053A4C8
    public double Scale { get; private set; } = 1.0;

    // Token: 0x14000012 RID: 18
    // (add) Token: 0x060007C0 RID: 1984 RVA: 0x0053A4D4 File Offset: 0x0053A4D4
    // (remove) Token: 0x060007C1 RID: 1985 RVA: 0x0053A50C File Offset: 0x0053A50C
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<uint, IntPtr, IntPtr> RawMessage;

    // Token: 0x17000139 RID: 313
    // (get) Token: 0x060007C2 RID: 1986 RVA: 0x0053A544 File Offset: 0x0053A544
    private IntPtr ZOrder
    {
      get
      {
        if (!this.isTopmost)
        {
          return (IntPtr)0;
        }
        return FullscreenGuard.TopmostInsertAfter();
      }
    }

    // Token: 0x060007C3 RID: 1987 RVA: 0x0053A558 File Offset: 0x0053A558
    public OverlayWindow(string title, bool excludeFromCapture, bool clickThroughEverywhere = false, bool activatable = false, bool topmost = true, bool showInTaskbar = false, bool hiddenFromOwnCapture = false)
    {
      this.excludeFromCapture = excludeFromCapture;
      this.hiddenFromOwnCapture = hiddenFromOwnCapture;
      this.noActivate = !activatable;
      this.isTopmost = topmost;
      base.Title = title;
      this.Root = new AccessibleGrid
      {
        Background = null,
        ControlType = AutomationControlType.Pane
      };
      AutomationProperties.SetName(this.Root, title);
      base.Content = this.Root;
      base.SystemBackdrop = new TransparentBackdrop();
      this.Hwnd = WindowNative.GetWindowHandle(this);
      this.AppWin = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(this.Hwnd));
      try
      {
        this.AppWin.IsShownInSwitchers = showInTaskbar;
      }
      catch (Exception ex)
      {
        Log.Warn("shell", "IsShownInSwitchers: " + ex.Message);
      }
      if (showInTaskbar)
      {
        try
        {
          this.AppWin.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets", "AppIcon.ico"));
        }
        catch (Exception ex2)
        {
          Log.Warn("shell", "window icon: " + ex2.Message);
        }
      }
      OverlappedPresenter overlappedPresenter = this.AppWin.Presenter as OverlappedPresenter;
      if (overlappedPresenter != null)
      {
        overlappedPresenter.SetBorderAndTitleBar(false, false);
        overlappedPresenter.IsResizable = false;
        overlappedPresenter.IsMaximizable = false;
        overlappedPresenter.IsMinimizable = false;
        overlappedPresenter.IsAlwaysOnTop = topmost;
      }
      Native.SetStyle(this.Hwnd, 2147483648U, 13565952U);
      Native.AddExStyle(this.Hwnd, (showInTaskbar ? 262144U : 128U) | (activatable ? 0U : 134217728U) | (topmost ? 8U : 0U) | (clickThroughEverywhere ? 524320U : 0U), showInTaskbar ? 128U : 262144U);
      if (clickThroughEverywhere)
      {
        Native.SetLayeredWindowAttributes(this.Hwnd, 0U, byte.MaxValue, 2U);
      }
      int num = 1;
      Native.DwmSetWindowAttribute(this.Hwnd, 33, ref num, 4);
      int num2 = -2;
      Native.DwmSetWindowAttribute(this.Hwnd, 34, ref num2, 4);
      int num3 = 1;
      Native.DwmSetWindowAttribute(this.Hwnd, 3, ref num3, 4);
      Native.MARGINS margins = new Native.MARGINS
      {
        L = -1,
        R = -1,
        T = -1,
        B = -1
      };
      Native.DwmExtendFrameIntoClientArea(this.Hwnd, ref margins);
      this.subclass = new WindowSubclass(this.Hwnd, new WindowSubclass.Handler(this.WndProc));
      Native.SetWindowPos(this.Hwnd, this.ZOrder, 0, 0, 0, 0, 51U | (this.isTopmost ? 0U : 4U));
      this.ApplyCapturePolicy();
      this.Scale = Native.GetDpiForWindow(this.Hwnd) / 96.0;
      Native.RECT rect;
      Native.GetWindowRectPublic(this.Hwnd, out rect);
      this.placement = new ValueTuple<int, int, int, int>(rect.Left, rect.Top, rect.Width, rect.Height);
      this.Park();
      this.Root.IsHitTestVisible = false;
      PointerEventHandler pointerEventHandler = delegate(object _, PointerRoutedEventArgs e)
      {
        PointerFrames.Note(e);
      };
      this.Root.AddHandler(UIElement.PointerMovedEvent, pointerEventHandler, true);
      this.Root.AddHandler(UIElement.PointerPressedEvent, pointerEventHandler, true);
      this.Root.AddHandler(UIElement.PointerReleasedEvent, pointerEventHandler, true);
      this.Root.AddHandler(UIElement.PointerEnteredEvent, pointerEventHandler, true);
      List<WeakReference<OverlayWindow>> live = OverlayWindow.Live;
      lock (live)
      {
        OverlayWindow.Live.Add(new WeakReference<OverlayWindow>(this));
      }
      if (OverlayWindow.InputTrace)
      {
        this.StartInputTrace(title);
      }
      base.Closed += delegate(object _, WindowEventArgs _)
      {
        this.subclass.Dispose();
        List<WeakReference<OverlayWindow>> live2 = OverlayWindow.Live;
        lock (live2)
        {
          OverlayWindow.Live.RemoveAll(delegate(WeakReference<OverlayWindow> w)
          {
            OverlayWindow overlayWindow;
            return !w.TryGetTarget(out overlayWindow) || overlayWindow == this;
          });
        }
      };
    }

    // Token: 0x060007C4 RID: 1988 RVA: 0x0053A93C File Offset: 0x0053A93C
    private void StartInputTrace(string title)
    {
      OverlayWindow.<>c__DisplayClass26_0 CS$<>8__locals1 = new OverlayWindow.<>c__DisplayClass26_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.title = title;
      CS$<>8__locals1.<StartInputTrace>g__Hook|0(this.Hwnd, "top");
      CS$<>8__locals1.hooked = new HashSet<IntPtr> { this.Hwnd };
      CS$<>8__locals1.<StartInputTrace>g__HookChildren|1();
      DispatcherQueueTimer dispatcherQueueTimer = DispatcherQueue.GetForCurrentThread().CreateTimer();
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(4L);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        base.<StartInputTrace>g__HookChildren|1();
      };
      dispatcherQueueTimer.Start();
      this.Root.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(delegate(object _, PointerRoutedEventArgs e)
      {
        string text = "inputtrace";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 3);
        defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.title);
        defaultInterpolatedStringHandler.AppendLiteral(" xaml PointerPressed on ");
        FrameworkElement frameworkElement = e.OriginalSource as FrameworkElement;
        defaultInterpolatedStringHandler.AppendFormatted((frameworkElement != null) ? frameworkElement.GetType().Name : null);
        defaultInterpolatedStringHandler.AppendLiteral(" handled=");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(e.Handled);
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      }), true);
      this.Root.AddHandler(UIElement.PointerExitedEvent, new PointerEventHandler(delegate(object _, PointerRoutedEventArgs e)
      {
        Log.Info("inputtrace", CS$<>8__locals1.title + " xaml PointerExited " + base.<StartInputTrace>g__Where|4(e));
      }), true);
      this.Root.AddHandler(UIElement.PointerEnteredEvent, new PointerEventHandler(delegate(object _, PointerRoutedEventArgs e)
      {
        Log.Info("inputtrace", CS$<>8__locals1.title + " xaml PointerEntered " + base.<StartInputTrace>g__Where|4(e));
      }), true);
      this.Root.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(delegate(object _, PointerRoutedEventArgs e)
      {
        string text2 = "inputtrace";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(31, 3);
        defaultInterpolatedStringHandler2.AppendFormatted(CS$<>8__locals1.title);
        defaultInterpolatedStringHandler2.AppendLiteral(" xaml PointerReleased ");
        defaultInterpolatedStringHandler2.AppendFormatted(base.<StartInputTrace>g__Where|4(e));
        defaultInterpolatedStringHandler2.AppendLiteral(" handled=");
        defaultInterpolatedStringHandler2.AppendFormatted<bool>(e.Handled);
        Log.Info(text2, defaultInterpolatedStringHandler2.ToStringAndClear());
      }), true);
      this.Root.AddHandler(UIElement.PointerCaptureLostEvent, new PointerEventHandler(delegate(object _, PointerRoutedEventArgs e)
      {
        string text3 = "inputtrace";
        string title2 = CS$<>8__locals1.title;
        string text4 = " xaml PointerCaptureLost ";
        FrameworkElement frameworkElement2 = e.OriginalSource as FrameworkElement;
        Log.Info(text3, title2 + text4 + ((frameworkElement2 != null) ? frameworkElement2.GetType().Name : null));
      }), true);
      this.Root.AddHandler(UIElement.PointerMovedEvent, new PointerEventHandler(delegate(object _, PointerRoutedEventArgs e)
      {
        if (Environment.TickCount % 8 == 0)
        {
          string text5 = "inputtrace";
          string title3 = CS$<>8__locals1.title;
          string text6 = " xaml PointerMoved on ";
          FrameworkElement frameworkElement3 = e.OriginalSource as FrameworkElement;
          Log.Info(text5, title3 + text6 + ((frameworkElement3 != null) ? frameworkElement3.GetType().Name : null));
        }
      }), true);
    }

    // Token: 0x1700013A RID: 314
    // (get) Token: 0x060007C5 RID: 1989 RVA: 0x0053AA70 File Offset: 0x0053AA70
    // (set) Token: 0x060007C6 RID: 1990 RVA: 0x0053AA78 File Offset: 0x0053AA78
    public static bool ShowInScreenRecordings
    {
      get
      {
        return OverlayWindow.showInScreenRecordings;
      }
      set
      {
        OverlayWindow.showInScreenRecordings = value;
        List<WeakReference<OverlayWindow>> live = OverlayWindow.Live;
        List<OverlayWindow> list;
        lock (live)
        {
          list = OverlayWindow.Live.Select<WeakReference<OverlayWindow>, OverlayWindow>(delegate(WeakReference<OverlayWindow> w)
          {
            OverlayWindow overlayWindow2;
            if (!w.TryGetTarget(out overlayWindow2))
            {
              return null;
            }
            return overlayWindow2;
          }).OfType<OverlayWindow>().ToList<OverlayWindow>();
        }
        foreach (OverlayWindow overlayWindow in list)
        {
          overlayWindow.ApplyCapturePolicy();
        }
        string text = "capture";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 2);
        defaultInterpolatedStringHandler.AppendLiteral("show in screen recordings: ");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(value);
        defaultInterpolatedStringHandler.AppendLiteral(" (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" windows)");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      }
    }

    // Token: 0x1700013B RID: 315
    // (get) Token: 0x060007C7 RID: 1991 RVA: 0x0053AB7C File Offset: 0x0053AB7C
    private bool ExcludesNow
    {
      get
      {
        if (this.neverInCapture || this.temporarilyHiddenFromCapture)
        {
          return true;
        }
        if (!this.hiddenFromOwnCapture)
        {
          return this.excludeFromCapture && !OverlayWindow.showInScreenRecordings;
        }
        return CaptureExclusion.Enabled && (!OverlayWindow.showInScreenRecordings || OverlayWindow.ownCapturesInProgress > 0);
      }
    }

    // Token: 0x060007C8 RID: 1992 RVA: 0x0053ABD0 File Offset: 0x0053ABD0
    public static bool SetHiddenFromOwnCapture(bool hidden)
    {
      bool flag = OverlayWindow.ownCapturesInProgress > 0;
      OverlayWindow.ownCapturesInProgress = Math.Max(0, OverlayWindow.ownCapturesInProgress + (hidden ? 1 : (-1)));
      if (flag == OverlayWindow.ownCapturesInProgress > 0 || !OverlayWindow.showInScreenRecordings)
      {
        return false;
      }
      List<WeakReference<OverlayWindow>> live = OverlayWindow.Live;
      List<OverlayWindow> list;
      lock (live)
      {
        list = (from o in OverlayWindow.Live.Select<WeakReference<OverlayWindow>, OverlayWindow>(delegate(WeakReference<OverlayWindow> w)
          {
            OverlayWindow overlayWindow2;
            if (!w.TryGetTarget(out overlayWindow2))
            {
              return null;
            }
            return overlayWindow2;
          }).OfType<OverlayWindow>()
          where o.hiddenFromOwnCapture
          select o).ToList<OverlayWindow>();
      }
      foreach (OverlayWindow overlayWindow in list)
      {
        overlayWindow.ApplyCapturePolicy();
      }
      return list.Count > 0;
    }

    // Token: 0x060007C9 RID: 1993 RVA: 0x0053ACD8 File Offset: 0x0053ACD8
    public void SetTemporarilyHiddenFromOwnCapture(bool hidden)
    {
      if (this.temporarilyHiddenFromCapture == hidden)
      {
        return;
      }
      this.temporarilyHiddenFromCapture = hidden;
      this.ApplyCapturePolicy();
    }

    // Token: 0x060007CA RID: 1994 RVA: 0x0053ACF4 File Offset: 0x0053ACF4
    public void KeepOutOfEveryCapture()
    {
      this.neverInCapture = true;
      this.ApplyCapturePolicy();
    }

    // Token: 0x060007CB RID: 1995 RVA: 0x0053AD04 File Offset: 0x0053AD04
    private void ApplyCapturePolicy()
    {
      Native.SetWindowDisplayAffinity(this.Hwnd, this.ExcludesNow ? 17U : 0U);
    }

    // Token: 0x060007CC RID: 1996 RVA: 0x0053AD20 File Offset: 0x0053AD20
    private IntPtr? WndProc(IntPtr h, uint msg, IntPtr w, IntPtr l)
    {
      if (msg != 6U)
      {
        if (msg == 20U)
        {
          Native.RECT rect;
          Native.GetClientRect(h, out rect);
          Native.FillRect(w, ref rect, Native.GetStockObject(4));
          return new IntPtr?((IntPtr)1);
        }
        if (msg == 33U)
        {
          if (this.noActivate)
          {
            return new IntPtr?((IntPtr)3);
          }
        }
      }
      else if ((w.ToInt64() & 65535L) != 0L)
      {
        FocusWatch.NoteActivated(base.Title);
      }
      Action<uint, IntPtr, IntPtr> rawMessage = this.RawMessage;
      if (rawMessage != null)
      {
        rawMessage(msg, w, l);
      }
      return null;
    }

    // Token: 0x1700013C RID: 316
    // (get) Token: 0x060007CD RID: 1997 RVA: 0x0053ADA8 File Offset: 0x0053ADA8
    [TupleElementNames(new string[] { "X", "Y", "W", "H" })]
    [Nullable(0)]
    public ValueTuple<int, int, int, int> RectPx
    {
      [NullableContext(0)]
      [return: TupleElementNames(new string[] { "X", "Y", "W", "H" })]
      get
      {
        if (!this.shown)
        {
          return this.placement;
        }
        Native.RECT rect;
        Native.GetWindowRectPublic(this.Hwnd, out rect);
        return new ValueTuple<int, int, int, int>(rect.Left, rect.Top, rect.Width, rect.Height);
      }
    }

    // Token: 0x1700013D RID: 317
    // (get) Token: 0x060007CE RID: 1998 RVA: 0x0053ADF4 File Offset: 0x0053ADF4
    public bool IsShown
    {
      get
      {
        return this.shown;
      }
    }

    // Token: 0x1700013E RID: 318
    // (get) Token: 0x060007CF RID: 1999 RVA: 0x0053ADFC File Offset: 0x0053ADFC
    public Native.RECT WindowRect
    {
      get
      {
        ValueTuple<int, int, int, int> rectPx = this.RectPx;
        int item = rectPx.Item1;
        int item2 = rectPx.Item2;
        int item3 = rectPx.Item3;
        int item4 = rectPx.Item4;
        return new Native.RECT
        {
          Left = item,
          Top = item2,
          Right = item + item3,
          Bottom = item2 + item4
        };
      }
    }

    // Token: 0x060007D0 RID: 2000 RVA: 0x0053AE58 File Offset: 0x0053AE58
    private void Park()
    {
      if (OverlayWindow.Offscreen)
      {
        return;
      }
      Native.SetWindowPos(this.Hwnd, (IntPtr)0, -32000, -32000, 0, 0, 21U);
    }

    // Token: 0x060007D1 RID: 2001 RVA: 0x0053AE80 File Offset: 0x0053AE80
    private void Unpark()
    {
      if (this.shown)
      {
        return;
      }
      this.shown = true;
      this.Root.IsHitTestVisible = !this.ignoresMouseEvents;
      ValueTuple<int, int, int, int> valueTuple = this.placement;
      int item = valueTuple.Item1;
      int item2 = valueTuple.Item2;
      int item3 = valueTuple.Item3;
      int item4 = valueTuple.Item4;
      Native.SetWindowPos(this.Hwnd, this.ZOrder, item, item2, item3, item4, 16U | (this.isTopmost ? 0U : 4U));
      this.Scale = Native.GetDpiForWindow(this.Hwnd) / 96.0;
    }

    // Token: 0x060007D2 RID: 2002 RVA: 0x0053AF14 File Offset: 0x0053AF14
    public void PlacePx(int x, int y, int w, int h)
    {
      if (OverlayWindow.Offscreen)
      {
        x += -24000;
        y += -24000;
      }
      this.placement = new ValueTuple<int, int, int, int>(x, y, w, h);
      if (!this.shown)
      {
        Native.SetWindowPos(this.Hwnd, this.ZOrder, OverlayWindow.Offscreen ? x : (-32000), OverlayWindow.Offscreen ? y : (-32000), w, h, 16U | (this.isTopmost ? 0U : 4U));
        this.Scale = Displays.DpiAt(x + w / 2, y + h / 2) / 96.0;
        return;
      }
      Native.SetWindowPos(this.Hwnd, this.ZOrder, x, y, w, h, 16U | (this.isTopmost ? 0U : 4U));
      this.Scale = Native.GetDpiForWindow(this.Hwnd) / 96.0;
    }

    // Token: 0x060007D3 RID: 2003 RVA: 0x0053AFF8 File Offset: 0x0053AFF8
    public void ShowNoActivate()
    {
      this.Unpark();
      Native.ShowWindow(this.Hwnd, 4);
      Native.SetWindowPos(this.Hwnd, this.ZOrder, 0, 0, 0, 0, 83U);
      this.ApplyCapturePolicy();
    }

    // Token: 0x060007D4 RID: 2004 RVA: 0x0053B02C File Offset: 0x0053B02C
    public void ShowActivated()
    {
      if (OverlayWindow.Offscreen)
      {
        this.ShowNoActivate();
        return;
      }
      this.Unpark();
      Native.ShowWindow(this.Hwnd, 5);
      Native.ForceForeground(this.Hwnd);
      this.ApplyCapturePolicy();
    }

    // Token: 0x060007D5 RID: 2005 RVA: 0x0053B060 File Offset: 0x0053B060
    public void MoveByBackground(UIElement background)
    {
      ValueTuple<Native.POINT, int, int>? drag = null;
      background.PointerPressed += delegate(object _, PointerRoutedEventArgs e)
      {
        if (!e.GetCurrentPoint(background).Properties.IsLeftButtonPressed || OverlayWindow.IsPressOnControl(e.OriginalSource, background))
        {
          return;
        }
        Native.POINT point;
        Native.RECT rect;
        if (!Native.IsPrimaryButtonDown() || !Native.GetCursorPos(out point) || !Native.GetWindowRectPublic(this.Hwnd, out rect))
        {
          return;
        }
        if (!background.CapturePointer(e.Pointer))
        {
          return;
        }
        drag = new ValueTuple<Native.POINT, int, int>?(new ValueTuple<Native.POINT, int, int>(point, rect.Left, rect.Top));
        e.Handled = true;
      };
      background.PointerMoved += delegate(object _, PointerRoutedEventArgs e)
      {
        if (drag == null)
        {
          return;
        }
        ValueTuple<Native.POINT, int, int> valueOrDefault = drag.GetValueOrDefault();
        Native.POINT point2;
        if (!Native.IsPrimaryButtonDown() || !Native.GetCursorPos(out point2))
        {
          base.<MoveByBackground>g__End|0(e.Pointer);
          return;
        }
        Native.SetWindowPos(this.Hwnd, (IntPtr)0, valueOrDefault.Item2 + point2.X - valueOrDefault.Item1.X, valueOrDefault.Item3 + point2.Y - valueOrDefault.Item1.Y, 0, 0, 21U);
        e.Handled = true;
      };
      background.PointerReleased += delegate(object _, PointerRoutedEventArgs e)
      {
        base.<MoveByBackground>g__End|0(e.Pointer);
      };
      background.PointerCanceled += delegate(object _, PointerRoutedEventArgs e)
      {
        base.<MoveByBackground>g__End|0(e.Pointer);
      };
      background.PointerCaptureLost += delegate(object _, PointerRoutedEventArgs _)
      {
        drag = null;
      };
    }

    // Token: 0x060007D6 RID: 2006 RVA: 0x0053B100 File Offset: 0x0053B100
    public static bool IsPressOnControl([Nullable(2)] object source, DependencyObject background)
    {
      DependencyObject dependencyObject = source as DependencyObject;
      while (dependencyObject != null && dependencyObject != background)
      {
        bool flag = dependencyObject is Pressable || dependencyObject is Control;
        if (flag)
        {
          return true;
        }
        dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
      }
      return false;
    }

    // Token: 0x060007D7 RID: 2007 RVA: 0x0053B140 File Offset: 0x0053B140
    public void HideWindow()
    {
      if (!this.shown)
      {
        Native.ShowWindow(this.Hwnd, 0);
        return;
      }
      if (!OverlayWindow.Offscreen)
      {
        Native.RECT rect;
        Native.GetWindowRectPublic(this.Hwnd, out rect);
        this.placement = new ValueTuple<int, int, int, int>(rect.Left, rect.Top, rect.Width, rect.Height);
      }
      this.shown = false;
      this.Park();
      Native.ShowWindow(this.Hwnd, 0);
      this.Root.IsHitTestVisible = false;
    }

    // Token: 0x060007D8 RID: 2008 RVA: 0x0053B1C4 File Offset: 0x0053B1C4
    public void SetRegionPx(int x, int y, int w, int h)
    {
      this.SetRegion(Native.CreateRectRgn(x, y, x + Math.Max(0, w), y + Math.Max(0, h)));
    }

    // Token: 0x060007D9 RID: 2009 RVA: 0x0053B1E8 File Offset: 0x0053B1E8
    public void SetRegion(IntPtr region)
    {
      Native.RECT rect;
      Native.GetWindowRectPublic(this.Hwnd, out rect);
      foreach (IntPtr intPtr in Native.DirectChildren(this.Hwnd))
      {
        Native.RECT rect2;
        if (Native.GetWindowRectPublic(intPtr, out rect2) && rect2.Width > 0 && rect2.Height > 0)
        {
          IntPtr intPtr2 = Native.CreateRectRgn(0, 0, 0, 0);
          Native.CombineRgn(intPtr2, region, (IntPtr)0, 5);
          Native.OffsetRgn(intPtr2, rect.Left - rect2.Left, rect.Top - rect2.Top);
          if (OverlayWindow.HasRegion(intPtr, intPtr2))
          {
            Native.DeleteObject(intPtr2);
          }
          else if (Native.SetWindowRgn(intPtr, intPtr2, true) == 0)
          {
            Native.DeleteObject(intPtr2);
          }
        }
      }
      if (OverlayWindow.HasRegion(this.Hwnd, region))
      {
        Native.DeleteObject(region);
        return;
      }
      if (Native.SetWindowRgn(this.Hwnd, region, true) == 0)
      {
        Native.DeleteObject(region);
      }
    }

    // Token: 0x060007DA RID: 2010 RVA: 0x0053B2F8 File Offset: 0x0053B2F8
    private static bool HasRegion(IntPtr hwnd, IntPtr region)
    {
      IntPtr intPtr = Native.CreateRectRgn(0, 0, 0, 0);
      bool flag = Native.GetWindowRgn(hwnd, intPtr) != 0 && Native.EqualRgn(intPtr, region);
      Native.DeleteObject(intPtr);
      return flag;
    }

    // Token: 0x060007DB RID: 2011 RVA: 0x0053B32C File Offset: 0x0053B32C
    public void SetIgnoresMouseEvents(bool ignores)
    {
      if (ignores == this.ignoresMouseEvents)
      {
        return;
      }
      this.ignoresMouseEvents = ignores;
      if (ignores)
      {
        Native.AddExStyle(this.Hwnd, 524320U, 0U);
        Native.SetLayeredWindowAttributes(this.Hwnd, 0U, byte.MaxValue, 2U);
      }
      else
      {
        Native.AddExStyle(this.Hwnd, 0U, this.staysLayered ? 32U : 524320U);
      }
      this.Root.IsHitTestVisible = !ignores && this.shown;
    }

    // Token: 0x060007DC RID: 2012 RVA: 0x0053B3A8 File Offset: 0x0053B3A8
    public void StayLayered()
    {
      this.staysLayered = true;
      Native.AddExStyle(this.Hwnd, 524288U, 0U);
      Native.SetLayeredWindowAttributes(this.Hwnd, 0U, byte.MaxValue, 2U);
    }

    // Token: 0x060007DD RID: 2013 RVA: 0x0053B3D8 File Offset: 0x0053B3D8
    public void ClearRegion()
    {
      foreach (IntPtr intPtr in Native.DirectChildren(this.Hwnd))
      {
        Native.SetWindowRgn(intPtr, (IntPtr)0, true);
      }
      Native.SetWindowRgn(this.Hwnd, (IntPtr)0, true);
    }

    // Token: 0x1700013F RID: 319
    // (get) Token: 0x060007DE RID: 2014 RVA: 0x0053B440 File Offset: 0x0053B440
    // (set) Token: 0x060007DF RID: 2015 RVA: 0x0053B44C File Offset: 0x0053B44C
    public bool CanActivate
    {
      get
      {
        return !this.noActivate;
      }
      set
      {
        if (this.noActivate == !value)
        {
          return;
        }
        this.noActivate = !value;
        if (value)
        {
          Native.AddExStyle(this.Hwnd, 0U, 134217728U);
          return;
        }
        Native.AddExStyle(this.Hwnd, 134217728U, 0U);
      }
    }

    // Token: 0x0400026A RID: 618
    private bool noActivate = true;

    // Token: 0x0400026B RID: 619
    private readonly bool isTopmost;

    // Token: 0x0400026C RID: 620
    private readonly bool excludeFromCapture;

    // Token: 0x0400026D RID: 621
    private readonly bool hiddenFromOwnCapture;

    // Token: 0x0400026E RID: 622
    private readonly WindowSubclass subclass;

    // Token: 0x0400026F RID: 623
    private static readonly bool InputTrace = Environment.GetEnvironmentVariable("HEYCLICKY_INPUT_TRACE") == "1";

    // Token: 0x04000270 RID: 624
    private readonly List<WindowSubclass> traceSubclasses = new List<WindowSubclass>();

    // Token: 0x04000271 RID: 625
    private static readonly List<WeakReference<OverlayWindow>> Live = new List<WeakReference<OverlayWindow>>();

    // Token: 0x04000272 RID: 626
    private static bool showInScreenRecordings = true;

    // Token: 0x04000273 RID: 627
    private bool neverInCapture;

    // Token: 0x04000274 RID: 628
    private bool temporarilyHiddenFromCapture;

    // Token: 0x04000275 RID: 629
    private static int ownCapturesInProgress;

    // Token: 0x04000276 RID: 630
    public static readonly bool Offscreen = Environment.GetEnvironmentVariable("HEYCLICKY_OFFSCREEN") == "1";

    // Token: 0x04000277 RID: 631
    internal const int OffscreenShift = -24000;

    // Token: 0x04000278 RID: 632
    public static readonly bool OnscreenBesideReal = Environment.GetEnvironmentVariable("HEYCLICKY_ONSCREEN_BESIDE_REAL") == "1";

    // Token: 0x04000279 RID: 633
    private const int ParkedX = -32000;

    // Token: 0x0400027A RID: 634
    private const int ParkedY = -32000;

    // Token: 0x0400027B RID: 635
    private bool shown;

    // Token: 0x0400027C RID: 636
    [TupleElementNames(new string[] { "X", "Y", "W", "H" })]
    [Nullable(0)]
    private ValueTuple<int, int, int, int> placement;

    // Token: 0x0400027D RID: 637
    private bool ignoresMouseEvents;

    // Token: 0x0400027E RID: 638
    private bool staysLayered;
  }
}
