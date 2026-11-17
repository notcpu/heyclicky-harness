using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Interop;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.Notch.Surfaces;
using HeyClicky.App.Services;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Telemetry;
using HeyClicky.Drawing;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Composition;

namespace HeyClicky.App.Notch
{
  // Token: 0x0200013D RID: 317
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchView : INotchHost
  {
    // Token: 0x1700032C RID: 812
    // (get) Token: 0x06001130 RID: 4400 RVA: 0x0058B448 File Offset: 0x0058B448
    public AppServices Services { get; }

    // Token: 0x1700032D RID: 813
    // (get) Token: 0x06001131 RID: 4401 RVA: 0x0058B450 File Offset: 0x0058B450
    public IAppActions Actions { get; }

    // Token: 0x1700032E RID: 814
    // (get) Token: 0x06001132 RID: 4402 RVA: 0x0058B458 File Offset: 0x0058B458
    public NotchGeometry.SizeContext SizeContext { get; } = new NotchGeometry.SizeContext();

    // Token: 0x1700032F RID: 815
    // (get) Token: 0x06001133 RID: 4403 RVA: 0x0058B460 File Offset: 0x0058B460
    public OverlayWindow Window { get; }

    // Token: 0x17000330 RID: 816
    // (get) Token: 0x06001134 RID: 4404 RVA: 0x0058B468 File Offset: 0x0058B468
    // (set) Token: 0x06001135 RID: 4405 RVA: 0x0058B470 File Offset: 0x0058B470
    public bool IsHovering { get; private set; }

    // Token: 0x17000331 RID: 817
    // (get) Token: 0x06001136 RID: 4406 RVA: 0x0058B47C File Offset: 0x0058B47C
    public IntPtr WindowHandle
    {
      get
      {
        return this.Window.Hwnd;
      }
    }

    // Token: 0x14000045 RID: 69
    // (add) Token: 0x06001137 RID: 4407 RVA: 0x0058B48C File Offset: 0x0058B48C
    // (remove) Token: 0x06001138 RID: 4408 RVA: 0x0058B4C4 File Offset: 0x0058B4C4
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<bool> HoverChanged;

    // Token: 0x14000046 RID: 70
    // (add) Token: 0x06001139 RID: 4409 RVA: 0x0058B4FC File Offset: 0x0058B4FC
    // (remove) Token: 0x0600113A RID: 4410 RVA: 0x0058B534 File Offset: 0x0058B534
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action HoverDwellCompleted;

    // Token: 0x17000332 RID: 818
    // (get) Token: 0x0600113B RID: 4411 RVA: 0x0058B56C File Offset: 0x0058B56C
    public MultiAgentRotationCursor MultiAgentRotation { get; } = new MultiAgentRotationCursor();

    // Token: 0x14000047 RID: 71
    // (add) Token: 0x0600113C RID: 4412 RVA: 0x0058B574 File Offset: 0x0058B574
    // (remove) Token: 0x0600113D RID: 4413 RVA: 0x0058B5AC File Offset: 0x0058B5AC
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action MultiAgentRotated;

    // Token: 0x14000048 RID: 72
    // (add) Token: 0x0600113E RID: 4414 RVA: 0x0058B5E4 File Offset: 0x0058B5E4
    // (remove) Token: 0x0600113F RID: 4415 RVA: 0x0058B61C File Offset: 0x0058B61C
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action FirstFrameShown;

    // Token: 0x14000049 RID: 73
    // (add) Token: 0x06001140 RID: 4416 RVA: 0x0058B654 File Offset: 0x0058B654
    // (remove) Token: 0x06001141 RID: 4417 RVA: 0x0058B68C File Offset: 0x0058B68C
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action IdleClicked;

    // Token: 0x06001142 RID: 4418 RVA: 0x0058B6C4 File Offset: 0x0058B6C4
    public NotchView(AppServices services, IAppActions actions, bool excludeFromCapture)
    {
      this.Services = services;
      this.Actions = actions;
      this.dq = DispatcherQueue.GetForCurrentThread();
      this.Window = new OverlayWindow("HeyClicky Notch", excludeFromCapture, false, false, true, false, false);
      this.c = Ui.Compositor;
      this.monitor = Displays.UnderCursor();
      this.envH = NotchGeometry.EnvelopeHeight((double)this.monitor.Bounds.Height / this.monitor.Scale, 1.0);
      this.SizeContext.ScreenHeightDip = (double)this.monitor.Bounds.Height / this.monitor.Scale;
      Grid root = this.Window.Root;
      root.Width = this.envW;
      root.Height = this.envH;
      root.HorizontalAlignment = HorizontalAlignment.Left;
      root.VerticalAlignment = VerticalAlignment.Top;
      this.silhouette = new NotchSilhouette(this.c, this.envW);
      this.silhouette.SetDeviceScale(this.monitor.Scale);
      Windows.UI.Composition.Compositor systemCompositor = TransparentBackdrop.SystemCompositor;
      NotchSystemShape notchSystemShape = new NotchSystemShape(systemCompositor, this.envW);
      this.silhouette.AttachTwin(notchSystemShape);
      try
      {
        this.glow = new NotchCornerGlow(systemCompositor, notchSystemShape.Props, notchSystemShape.ClipGeometry);
      }
      catch (Exception ex)
      {
        Log.Error("notch", "corner glow failed to build", ex);
        ClickyAnalytics.ReportInternalError("notch", "corner_glow", ex, null, CrashReportLevel.Error, null, null);
      }
      this.workingSweep = new NotchWorkingSweep(systemCompositor, notchSystemShape);
      this.systemLayer = NotchSystemLayer.Create(this.Window.Hwnd);
      if (this.systemLayer != null)
      {
        this.systemLayer.SetScale(this.monitor.Scale, this.envW, this.envH);
        this.systemLayer.Add(notchSystemShape.Root);
        this.systemLayer.Add(this.workingSweep.Root);
        if (this.glow != null)
        {
          this.systemLayer.Add(this.glow.Root);
        }
      }
      else
      {
        Grid grid = new Grid
        {
          IsHitTestVisible = false
        };
        Microsoft.UI.Composition.ContainerVisual containerVisual = this.c.CreateContainerVisual();
        containerVisual.RelativeSizeAdjustment = Vector2.One;
        containerVisual.Children.InsertAtTop(this.silhouette.Root);
        ElementCompositionPreview.SetElementChildVisual(grid, containerVisual);
        root.Children.Add(grid);
      }
      this.contentLayer = new Canvas
      {
        Background = Ui.Transparent,
        Width = this.envW,
        Height = this.envH
      };
      root.Children.Add(this.contentLayer);
      ElementCompositionPreview.GetElementVisual(this.contentLayer).Clip = this.c.CreateGeometricClip(this.silhouette.ClipGeometry);
      this.contentLayer.PointerEntered += delegate(object _, PointerRoutedEventArgs _)
      {
        this.StopHoverExitWatch();
        this.SetHover(true);
      };
      this.contentLayer.PointerExited += delegate(object _, PointerRoutedEventArgs e)
      {
        if (PointerFrames.IsStale(e))
        {
          return;
        }
        this.BeginHoverExit();
      };
      this.contentLayer.PointerPressed += delegate(object _, PointerRoutedEventArgs e)
      {
        if (NotchGeometry.IsFloatingSliver(this.current))
        {
          Action idleClicked = this.IdleClicked;
          if (idleClicked == null)
          {
            return;
          }
          idleClicked();
        }
      };
      this.contentLayer.AllowDrop = true;
      this.contentLayer.DragEnter += this.OnDragEnter;
      this.contentLayer.DragOver += this.OnDragOver;
      this.contentLayer.DragLeave += delegate(object _, DragEventArgs _)
      {
        NotchView.Frame frame = this.active;
        if (frame == null)
        {
          return;
        }
        frame.Surface.SetDropTargeting(false);
      };
      this.contentLayer.Drop += this.OnDrop;
      NotchPresenter notchPresenter = services.Presenter as NotchPresenter;
      if (notchPresenter != null)
      {
        notchPresenter.HoverDwellCompleted += delegate
        {
          this.dq.TryEnqueue(delegate
          {
            if (this.IsHovering)
            {
              Action hoverDwellCompleted = this.HoverDwellCompleted;
              if (hoverDwellCompleted == null)
              {
                return;
              }
              hoverDwellCompleted();
            }
          });
        };
      }
      this.rotationTimer = this.dq.CreateTimer();
      this.rotationTimer.IsRepeating = false;
      this.rotationTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.RotateMultiAgentStrip();
      };
      this.regionTimer = this.dq.CreateTimer();
      this.regionTimer.IsRepeating = false;
      this.regionTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.ApplyHeldRegion();
      };
      this.glowQuiesceTimer = this.dq.CreateTimer();
      this.glowQuiesceTimer.IsRepeating = false;
      this.glowQuiesceTimer.Interval = TimeSpan.FromSeconds(0.5);
      this.glowQuiesceTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        NotchCornerGlow notchCornerGlow = this.glow;
        if (notchCornerGlow == null)
        {
          return;
        }
        notchCornerGlow.Quiesce();
      };
      this.zOrderTimer = this.dq.CreateTimer();
      this.zOrderTimer.Interval = TimeSpan.FromSeconds(0.5);
      this.zOrderTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.KeepOnTop("while shown");
      };
      this.HoverChanged += delegate(bool hovering)
      {
        if (hovering)
        {
          this.KeepOnTop("pointer in");
        }
      };
      this.Window.RawMessage += delegate(uint msg, IntPtr w, IntPtr l)
      {
        if (msg == 126U || msg == 736U || msg == 26U)
        {
          this.dq.TryEnqueue(new DispatcherQueueHandler(this.Reposition));
        }
        if (msg == 6U)
        {
          bool activated = (w.ToInt64() & 65535L) != 0L;
          IntPtr other = l;
          this.dq.TryEnqueue(delegate
          {
            if (!activated && this.TakeKeyboardBackFrom(other))
            {
              return;
            }
            this.SetHasKeyboard(activated);
          });
        }
      };
      this.Services.Presenter.PresentationChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.dq.TryEnqueue(delegate
        {
          this.Apply(this.Services.Presenter.Presentation, false, false);
        });
      };
      INotchPresenter presenter = this.Services.Presenter;
      NotchPresenter tabbed = presenter as NotchPresenter;
      if (tabbed != null)
      {
        this.SizeContext.PeekSize = delegate
        {
          NotchTabs tabs = tabbed.Tabs;
          ChatHandOffs handOffs = NotchChat.HandOffs;
          INotchHomeDock homeDock = NotchExtensions.HomeDock;
          return NotchPeekLayout.PanelSize(tabs, handOffs, (homeDock != null) ? homeDock.FrontCardSize : null);
        };
        tabbed.Tabs.SelectionChanged += delegate(NotchTab tab)
        {
          this.dq.TryEnqueue(delegate
          {
            if (tab != NotchTab.Chat || tabbed.Tabs.ExpandedReplyEntryId == null || tabbed.Tabs.FullReplyContentHeight != null)
            {
              this.RelayoutPeek();
              return;
            }
            DispatcherQueueTimer fallback = this.dq.CreateTimer().Rooted();
            fallback.Interval = TimeSpan.FromSeconds(0.2);
            fallback.IsRepeating = false;
            fallback.Tick += delegate(DispatcherQueueTimer _, object _)
            {
              fallback.Stop();
              if (tabbed.Tabs.FullReplyContentHeight == null)
              {
                this.RelayoutPeek();
              }
            };
            fallback.Start();
          });
        };
        DispatcherQueueHandler <>9__21;
        tabbed.Tabs.Changed += delegate
        {
          DispatcherQueue dispatcherQueue = this.dq;
          DispatcherQueueHandler dispatcherQueueHandler;
          if ((dispatcherQueueHandler = <>9__21) == null)
          {
            dispatcherQueueHandler = (<>9__21 = delegate
            {
              ValueTuple<string, double?> valueTuple = new ValueTuple<string, double?>(tabbed.Tabs.ExpandedReplyEntryId, tabbed.Tabs.FullReplyContentHeight);
              ValueTuple<string, double?> valueTuple2 = valueTuple;
              ValueTuple<string, double?> valueTuple3 = this.lastFullReply;
              if (valueTuple2.Item1 == valueTuple3.Item1)
              {
                double? item = valueTuple2.Item2;
                double? num = valueTuple3.Item2;
                if ((item.GetValueOrDefault() == num.GetValueOrDefault()) & (item != null == (num != null)))
                {
                  return;
                }
              }
              this.lastFullReply = valueTuple;
              if (valueTuple.Item1 != null)
              {
                double? num = valueTuple.Item2;
                if (num == null)
                {
                  return;
                }
              }
              this.RelayoutPeek();
            });
          }
          dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
        };
      }
      root.Loaded += delegate(object _, RoutedEventArgs _)
      {
        bool flag = this.shown;
      };
      Microsoft.UI.Xaml.Media.CompositionTarget.Rendered += this.OnFirstRender;
    }

    // Token: 0x06001143 RID: 4419 RVA: 0x0058BD08 File Offset: 0x0058BD08
    private void OnFirstRender([Nullable(2)] object sender, RenderedEventArgs e)
    {
      if (!this.shown)
      {
        return;
      }
      Microsoft.UI.Xaml.Media.CompositionTarget.Rendered -= this.OnFirstRender;
      Action firstFrameShown = this.FirstFrameShown;
      if (firstFrameShown == null)
      {
        return;
      }
      firstFrameShown();
    }

    // Token: 0x06001144 RID: 4420 RVA: 0x0058BD34 File Offset: 0x0058BD34
    public void BindToEdges(UIElement element, double ax, double ay)
    {
      if (this.active == null)
      {
        return;
      }
      Microsoft.UI.Composition.ExpressionAnimation expressionAnimation = this.c.CreateExpressionAnimation(FormattableString.Invariant(FormattableStringFactory.Create("Vector3({0:0.###} * (p.Width - Wf) * 0.5, {1:0.###} * (p.Height - Hf), 0)", new object[] { ax, ay })));
      expressionAnimation.SetReferenceParameter("p", this.silhouette.Props);
      expressionAnimation.SetScalarParameter("Wf", (float)this.active.W);
      expressionAnimation.SetScalarParameter("Hf", (float)this.active.H);
      expressionAnimation.Target = "Translation";
      element.StartAnimation(expressionAnimation);
    }

    // Token: 0x06001145 RID: 4421 RVA: 0x0058BDD4 File Offset: 0x0058BDD4
    public void Show()
    {
      this.Reposition();
      this.shown = true;
      this.Apply(this.Services.Presenter.Presentation, true, false);
      this.Window.ShowNoActivate();
      this.zOrderTimer.Start();
    }

    // Token: 0x1400004A RID: 74
    // (add) Token: 0x06001146 RID: 4422 RVA: 0x0058BE14 File Offset: 0x0058BE14
    // (remove) Token: 0x06001147 RID: 4423 RVA: 0x0058BE4C File Offset: 0x0058BE4C
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action RaisedToTop;

    // Token: 0x06001148 RID: 4424 RVA: 0x0058BE84 File Offset: 0x0058BE84
    private void KeepOnTop(string why)
    {
      if (!this.shown || this.IsAmbientHidden)
      {
        return;
      }
      ValueTuple<int, int, int, int> valueTuple = this.SurfaceRectPx();
      int num = valueTuple.Item1;
      int num2 = valueTuple.Item2;
      int item = valueTuple.Item3;
      int item2 = valueTuple.Item4;
      if (OverlayWindow.Offscreen)
      {
        num += -24000;
        num2 += -24000;
      }
      IntPtr intPtr = ZOrder.ForeignWindowAbove(this.Window.Hwnd, new ValueTuple<int, int, int, int>(num, num2, item, item2));
      if (intPtr == 0)
      {
        this.lastCoveringWindow = (IntPtr)0;
        return;
      }
      if (intPtr == FullscreenGuard.GameWindow())
      {
        this.lastCoveringWindow = intPtr;
        return;
      }
      ZOrder.RaiseToTop(this.Window.Hwnd);
      if (intPtr != this.lastCoveringWindow)
      {
        string text = "notch";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 2);
        defaultInterpolatedStringHandler.AppendLiteral("back on top of ");
        defaultInterpolatedStringHandler.AppendFormatted(ZOrder.Describe(intPtr));
        defaultInterpolatedStringHandler.AppendLiteral(" (");
        defaultInterpolatedStringHandler.AppendFormatted(why);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      }
      this.lastCoveringWindow = intPtr;
      Action raisedToTop = this.RaisedToTop;
      if (raisedToTop == null)
      {
        return;
      }
      raisedToTop();
    }

    // Token: 0x06001149 RID: 4425 RVA: 0x0058BF9C File Offset: 0x0058BF9C
    private void SyncWorkingSweep()
    {
      this.workingSweep.Show(this.shown && NotchGeometry.IsWorkingBar(this.current) && !this.ambientHidden);
    }

    // Token: 0x17000333 RID: 819
    // (get) Token: 0x0600114A RID: 4426 RVA: 0x0058BFCC File Offset: 0x0058BFCC
    internal NotchWorkingSweep WorkingSweep
    {
      get
      {
        return this.workingSweep;
      }
    }

    // Token: 0x0600114B RID: 4427 RVA: 0x0058BFD4 File Offset: 0x0058BFD4
    private void Reposition()
    {
      this.monitor = (this.shown ? Displays.ForWindow(this.Window.Hwnd) : this.monitor);
      this.contentScale = NotchGeometry.ContentScale(this.streamMode);
      double num = (double)this.monitor.Bounds.Height / this.monitor.Scale;
      this.SizeContext.ScreenHeightDip = num;
      this.envH = NotchGeometry.EnvelopeHeight(num, this.contentScale);
      this.Window.Root.Height = this.envH;
      this.contentLayer.Height = this.envH;
      this.rootScale.ScaleX = (this.rootScale.ScaleY = this.contentScale);
      this.Window.Root.RenderTransform = ((this.contentScale == 1.0) ? null : this.rootScale);
      double drawnScale = this.DrawnScale;
      int num2 = (int)Math.Round(this.envW * drawnScale);
      int num3 = (int)Math.Round(this.envH * drawnScale);
      int num4 = this.monitor.Bounds.Left + (this.monitor.Bounds.Width - num2) / 2;
      this.Window.PlacePx(num4, this.monitor.Bounds.Top, num2, num3);
      this.silhouette.SetDeviceScale(drawnScale);
      NotchSystemLayer notchSystemLayer = this.systemLayer;
      if (notchSystemLayer != null)
      {
        notchSystemLayer.SetScale(drawnScale, this.envW, this.envH);
      }
      this.ApplyRegion(this.currentSize, null);
      this.SyncWorkingSweep();
    }

    // Token: 0x17000334 RID: 820
    // (get) Token: 0x0600114C RID: 4428 RVA: 0x0058C180 File Offset: 0x0058C180
    private double DrawnScale
    {
      get
      {
        return this.monitor.Scale * this.contentScale;
      }
    }

    // Token: 0x0600114D RID: 4429 RVA: 0x0058C194 File Offset: 0x0058C194
    public void SetStreamMode(bool on)
    {
      if (this.streamMode == on)
      {
        return;
      }
      this.streamMode = on;
      Log.Info("notch", "stream mode " + (on ? "on: the notch draws 1.5x as big" : "off"));
      this.Reposition();
      this.PublishVisibleSurface();
      this.KeepOnTop("stream mode");
    }

    // Token: 0x17000335 RID: 821
    // (get) Token: 0x0600114E RID: 4430 RVA: 0x0058C1EC File Offset: 0x0058C1EC
    public double ContentScale
    {
      get
      {
        return this.contentScale;
      }
    }

    // Token: 0x0600114F RID: 4431 RVA: 0x0058C1F4 File Offset: 0x0058C1F4
    public void FollowCursorDisplay()
    {
      Displays.Monitor monitor = Displays.UnderCursor();
      if (monitor.Handle == this.monitor.Handle)
      {
        return;
      }
      this.monitor = monitor;
      string text = "notch";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 4);
      defaultInterpolatedStringHandler.AppendLiteral("moved to the display under the cursor (");
      defaultInterpolatedStringHandler.AppendFormatted<int>(monitor.Bounds.Width);
      defaultInterpolatedStringHandler.AppendLiteral("x");
      defaultInterpolatedStringHandler.AppendFormatted<int>(monitor.Bounds.Height);
      defaultInterpolatedStringHandler.AppendLiteral(" at ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(monitor.Bounds.Left);
      defaultInterpolatedStringHandler.AppendLiteral(",");
      defaultInterpolatedStringHandler.AppendFormatted<int>(monitor.Bounds.Top);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      this.Reposition();
    }

    // Token: 0x17000336 RID: 822
    // (get) Token: 0x06001150 RID: 4432 RVA: 0x0058C2D4 File Offset: 0x0058C2D4
    public IntPtr MonitorHandle
    {
      get
      {
        return this.monitor.Handle;
      }
    }

    // Token: 0x17000337 RID: 823
    // (get) Token: 0x06001151 RID: 4433 RVA: 0x0058C2E4 File Offset: 0x0058C2E4
    public Displays.Monitor Monitor
    {
      get
      {
        return this.monitor;
      }
    }

    // Token: 0x06001152 RID: 4434 RVA: 0x0058C2EC File Offset: 0x0058C2EC
    public void SetAmbientHidden(bool hidden)
    {
      if (this.ambientHidden == hidden)
      {
        return;
      }
      this.ambientHidden = hidden;
      this.ApplyAmbientVisibility();
    }

    // Token: 0x06001153 RID: 4435 RVA: 0x0058C308 File Offset: 0x0058C308
    private void ApplyAmbientVisibility()
    {
      this.SyncWorkingSweep();
      bool flag = this.ambientHidden && this.current is NotchPresentation.Idle;
      Anim.Opacity(this.Window.Root, !flag, 0.22, null, 0.0);
      if (!flag)
      {
        this.ApplyHeldRegion();
        return;
      }
      DispatcherQueueTimer t = this.dq.CreateTimer().Rooted();
      t.Interval = TimeSpan.FromSeconds(0.24);
      t.IsRepeating = false;
      t.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        t.Stop();
        if (this.IsAmbientHidden)
        {
          this.Window.SetRegionPx(0, 0, 0, 0);
        }
      };
      t.Start();
    }

    // Token: 0x17000338 RID: 824
    // (get) Token: 0x06001154 RID: 4436 RVA: 0x0058C3D4 File Offset: 0x0058C3D4
    public bool IsAmbientHidden
    {
      get
      {
        return this.ambientHidden && this.current is NotchPresentation.Idle;
      }
    }

    // Token: 0x17000339 RID: 825
    // (get) Token: 0x06001155 RID: 4437 RVA: 0x0058C3F0 File Offset: 0x0058C3F0
    [Nullable(2)]
    internal NotchSurface ActiveSurfaceForTest
    {
      [NullableContext(2)]
      get
      {
        NotchView.Frame frame = this.active;
        if (frame == null)
        {
          return null;
        }
        return frame.Surface;
      }
    }

    // Token: 0x06001156 RID: 4438 RVA: 0x0058C404 File Offset: 0x0058C404
    public IReadOnlyList<string> TakePendingFiles()
    {
      IReadOnlyList<string> readOnlyList = this.pendingFiles.ToList<string>();
      this.pendingFiles.Clear();
      return readOnlyList;
    }

    // Token: 0x06001157 RID: 4439 RVA: 0x0058C41C File Offset: 0x0058C41C
    private static bool HasFiles(DragEventArgs e)
    {
      return e.DataView.Contains(StandardDataFormats.StorageItems);
    }

    // Token: 0x06001158 RID: 4440 RVA: 0x0058C430 File Offset: 0x0058C430
    private void OnDragEnter(object sender, DragEventArgs e)
    {
      if (!NotchView.HasFiles(e) || !this.Services.Auth.IsSignedIn)
      {
        return;
      }
      e.AcceptedOperation = DataPackageOperation.Copy;
      e.DragUIOverride.Caption = "Attach";
      NotchPresentation notchPresentation = this.current;
      if (!(notchPresentation is NotchPresentation.TextInput) && !(notchPresentation is NotchPresentation.TextResponse))
      {
        this.Services.Audio.PlayEarcon("textOpen");
        this.Services.Presenter.Present(new NotchPresentation.TextInput("", null));
        if (!this.composerOpenedByDrag)
        {
          ClickyAnalytics.TrackNotchFileDragOpenedComposer();
        }
        this.composerOpenedByDrag = true;
      }
      this.dq.TryEnqueue(delegate
      {
        NotchView.Frame frame = this.active;
        if (frame == null)
        {
          return;
        }
        frame.Surface.SetDropTargeting(true);
      });
    }

    // Token: 0x06001159 RID: 4441 RVA: 0x0058C4EC File Offset: 0x0058C4EC
    private void OnDragOver(object sender, DragEventArgs e)
    {
      if (!NotchView.HasFiles(e) || !this.Services.Auth.IsSignedIn)
      {
        return;
      }
      e.AcceptedOperation = DataPackageOperation.Copy;
    }

    // Token: 0x0600115A RID: 4442 RVA: 0x0058C510 File Offset: 0x0058C510
    private async void OnDrop(object sender, DragEventArgs e)
    {
      if (NotchView.HasFiles(e))
      {
        DragOperationDeferral deferral = e.GetDeferral();
        try
        {
          List<string> list = (from i in await e.DataView.GetStorageItemsAsync()
            select i.Path into p
            where !string.IsNullOrEmpty(p)
            select p).ToList<string>();
          string text = "notch";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 1);
          defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" file(s) dropped on the notch");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
          string text2 = ((this.composerOpenedByDrag && this.current is NotchPresentation.TextInput) ? "drag_opened" : ((this.current is NotchPresentation.TextInput) ? "user_opened" : "drop_opened"));
          this.composerOpenedByDrag = false;
          if (list.Count > 0)
          {
            ClickyAnalytics.TrackNotchFilesDropped(list.Count, text2);
          }
          NotchView.Frame frame = this.active;
          if (frame != null)
          {
            frame.Surface.SetDropTargeting(false);
          }
          NotchView.Frame frame2 = this.active;
          if (frame2 == null || !frame2.Surface.AcceptFiles(list))
          {
            this.pendingFiles.AddRange(list);
            this.Services.Presenter.Present(new NotchPresentation.TextInput("", null));
          }
        }
        catch (Exception ex)
        {
          Log.Warn("notch", "file drop failed: " + ex.Message);
        }
        finally
        {
          deferral.Complete();
        }
      }
    }

    // Token: 0x0600115B RID: 4443 RVA: 0x0058C550 File Offset: 0x0058C550
    private double Gap(NotchPresentation p)
    {
      if (!NotchGeometry.IsFloatingSliver(p))
      {
        return 0.0;
      }
      return 4.0;
    }

    // Token: 0x0600115C RID: 4444 RVA: 0x0058C570 File Offset: 0x0058C570
    private void ApplyHeldRegion()
    {
      this.regionTimer.Stop();
      TimeSpan timeSpan = this.regionHoldUntil - DateTimeOffset.Now;
      ValueTuple<double, double>? valueTuple = this.regionHold;
      if (valueTuple != null)
      {
        ValueTuple<double, double> valueOrDefault = valueTuple.GetValueOrDefault();
        if (timeSpan > TimeSpan.Zero)
        {
          this.ApplyRegion(this.currentSize, new ValueTuple<double, double>?(valueOrDefault));
          this.regionTimer.Interval = timeSpan + TimeSpan.FromMilliseconds(20L, 0L);
          this.regionTimer.Start();
          return;
        }
      }
      this.regionHold = null;
      this.ApplyRegion(this.currentSize, null);
    }

    // Token: 0x0600115D RID: 4445 RVA: 0x0058C618 File Offset: 0x0058C618
    [NullableContext(0)]
    private void ApplyRegion([TupleElementNames(new string[] { "W", "H" })] ValueTuple<double, double> size, [TupleElementNames(new string[] { "W", "H" })] ValueTuple<double, double>? union)
    {
      if (this.IsAmbientHidden)
      {
        this.Window.SetRegionPx(0, 0, 0, 0);
        return;
      }
      double num = size.Item1;
      double num2 = size.Item2;
      if (union != null)
      {
        ValueTuple<double, double> valueOrDefault = union.GetValueOrDefault();
        num = Math.Max(num, valueOrDefault.Item1);
        num2 = Math.Max(num2, valueOrDefault.Item2);
      }
      double num3 = this.Gap(this.current);
      double num4 = 0.0;
      double num5 = num2 + num3;
      double drawnScale = this.DrawnScale;
      int num6 = (int)Math.Floor((this.envW - num) / 2.0 * drawnScale);
      int num7 = (int)Math.Ceiling(num * drawnScale);
      this.Window.SetRegionPx(num6, (int)(num4 * drawnScale), num7, (int)Math.Ceiling(num5 * drawnScale));
    }

    // Token: 0x1700033A RID: 826
    // (get) Token: 0x0600115E RID: 4446 RVA: 0x0058C6E4 File Offset: 0x0058C6E4
    [TupleElementNames(new string[] { "W", "H" })]
    [Nullable(0)]
    internal ValueTuple<double, double> PanelSizeDip
    {
      [NullableContext(0)]
      [return: TupleElementNames(new string[] { "W", "H" })]
      get
      {
        return new ValueTuple<double, double>(this.currentSize.Item1, this.currentSize.Item2);
      }
    }

    // Token: 0x0600115F RID: 4447 RVA: 0x0058C704 File Offset: 0x0058C704
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "X", "Y", "W", "H" })]
    public ValueTuple<int, int, int, int> SurfaceRectPx()
    {
      if (!this.shown || this.IsAmbientHidden)
      {
        return new ValueTuple<int, int, int, int>(0, 0, 0, 0);
      }
      Displays.Monitor monitor = Displays.ForWindow(this.Window.Hwnd);
      double num = monitor.Scale * this.contentScale;
      return new ValueTuple<int, int, int, int>(monitor.Bounds.Left + (monitor.Bounds.Width - (int)Math.Round(this.envW * num)) / 2 + (int)Math.Floor((this.envW - this.currentSize.Item1) / 2.0 * num), monitor.Bounds.Top, (int)Math.Ceiling(this.currentSize.Item1 * num), (int)Math.Ceiling((this.currentSize.Item2 + this.Gap(this.current)) * num));
    }

    // Token: 0x06001160 RID: 4448 RVA: 0x0058C7E4 File Offset: 0x0058C7E4
    private void PublishVisibleSurface()
    {
      ValueTuple<int, int, int, int> valueTuple = this.SurfaceRectPx();
      int item = valueTuple.Item1;
      int item2 = valueTuple.Item2;
      int item3 = valueTuple.Item3;
      int item4 = valueTuple.Item4;
      bool flag = item3 > 0 && !NotchView.IsAmbient(this.current);
      if (flag)
      {
        NotchPresentation.LiveEvent liveEvent = this.current as NotchPresentation.LiveEvent;
        bool flag2;
        if (liveEvent != null)
        {
          NotchLiveEvent @event = liveEvent.Event;
          if (@event is NotchLiveEvent.ActivityListening || @event is NotchLiveEvent.ActivityThinking || @event is NotchLiveEvent.ActivitySpeaking || @event is NotchLiveEvent.ActivityCustomStatus || @event is NotchLiveEvent.Dictation || @event is NotchLiveEvent.IntroHatchPeek)
          {
            flag2 = true;
            goto IL_0094;
          }
        }
        flag2 = false;
        IL_0094:
        flag = !flag2;
      }
      bool flag3 = flag;
      NotchVisibleSurfaces.Replace((item3 > 0) ? new RectInt32?(new RectInt32(item, item2, item3, item4)) : null, flag3);
    }

    // Token: 0x06001161 RID: 4449 RVA: 0x0058C8B8 File Offset: 0x0058C8B8
    public bool ContainsScreenPointPx(PointD p)
    {
      ValueTuple<int, int, int, int> valueTuple = this.SurfaceRectPx();
      int item = valueTuple.Item1;
      int item2 = valueTuple.Item2;
      int item3 = valueTuple.Item3;
      int item4 = valueTuple.Item4;
      if (item3 <= 0 || item4 <= 0)
      {
        return false;
      }
      double num = 3.0 * Displays.ForWindow(this.Window.Hwnd).Scale;
      return p.X >= (double)item - num && p.X <= (double)(item + item3) + num && p.Y >= (double)item2 - num && p.Y <= (double)(item2 + item4) + num;
    }

    // Token: 0x1700033B RID: 827
    // (get) Token: 0x06001162 RID: 4450 RVA: 0x0058C958 File Offset: 0x0058C958
    public NotchPresentation Current
    {
      get
      {
        return this.current;
      }
    }

    // Token: 0x06001163 RID: 4451 RVA: 0x0058C960 File Offset: 0x0058C960
    private void BeginHoverExit()
    {
      if (!this.IsHovering || NotchView.IsAmbient(this.current))
      {
        this.SetHover(false);
        return;
      }
      this.hoverExitOutsideSince = null;
      if (this.hoverExitTimer == null)
      {
        this.hoverExitTimer = this.dq.CreateTimer();
        this.hoverExitTimer.Interval = TimeSpan.FromMilliseconds(50L, 0L);
        this.hoverExitTimer.Tick += delegate(DispatcherQueueTimer _, object _)
        {
          this.TickHoverExit();
        };
      }
      this.hoverExitTimer.Start();
      this.TickHoverExit();
    }

    // Token: 0x06001164 RID: 4452 RVA: 0x0058C9EC File Offset: 0x0058C9EC
    private void NoteStickyRect()
    {
      if (!this.IsHovering || NotchView.IsAmbient(this.current))
      {
        return;
      }
      ValueTuple<int, int, int, int> valueTuple = this.SurfaceRectPx();
      int item = valueTuple.Item1;
      int item2 = valueTuple.Item2;
      int item3 = valueTuple.Item3;
      int item4 = valueTuple.Item4;
      if (item3 <= 0 || item4 <= 0)
      {
        return;
      }
      ValueTuple<int, int, int, int>? valueTuple2 = this.stickyRectPx;
      if (valueTuple2 != null)
      {
        ValueTuple<int, int, int, int> valueOrDefault = valueTuple2.GetValueOrDefault();
        int num = Math.Min(valueOrDefault.Item1, item);
        int num2 = Math.Min(valueOrDefault.Item2, item2);
        int num3 = Math.Max(valueOrDefault.Item1 + valueOrDefault.Item3, item + item3);
        int num4 = Math.Max(valueOrDefault.Item2 + valueOrDefault.Item4, item2 + item4);
        this.stickyRectPx = new ValueTuple<int, int, int, int>?(new ValueTuple<int, int, int, int>(num, num2, num3 - num, num4 - num2));
        return;
      }
      this.stickyRectPx = new ValueTuple<int, int, int, int>?(new ValueTuple<int, int, int, int>(item, item2, item3, item4));
    }

    // Token: 0x06001165 RID: 4453 RVA: 0x0058CAD8 File Offset: 0x0058CAD8
    private void TickHoverExit()
    {
      if (!this.IsHovering || NotchView.IsAmbient(this.current))
      {
        this.StopHoverExitWatch();
        this.SetHover(false);
        return;
      }
      if (((int)Native.GetAsyncKeyState(1) & 32768) != 0 || ((int)Native.GetAsyncKeyState(2) & 32768) != 0)
      {
        this.hoverExitOutsideSince = null;
        return;
      }
      XamlRoot xamlRoot = this.contentLayer.XamlRoot;
      if (xamlRoot != null)
      {
        if (VisualTreeHelper.GetOpenPopupsForXamlRoot(xamlRoot).Any<Popup>((Popup p) => p.Child is MenuFlyoutPresenter))
        {
          this.hoverExitOutsideSince = null;
          return;
        }
      }
      Native.POINT point;
      Native.GetCursorPos(out point);
      this.NoteStickyRect();
      ValueTuple<int, int, int, int> valueTuple = this.stickyRectPx ?? this.SurfaceRectPx();
      int item = valueTuple.Item1;
      int item2 = valueTuple.Item2;
      int item3 = valueTuple.Item3;
      int item4 = valueTuple.Item4;
      double num = 24.0 * Displays.ForWindow(this.Window.Hwnd).Scale * this.contentScale;
      if (item3 > 0 && (double)point.X >= (double)item - num && (double)point.X <= (double)(item + item3) + num && point.Y >= item2 && (double)point.Y <= (double)(item2 + item4) + num)
      {
        this.hoverExitOutsideSince = null;
        return;
      }
      DateTimeOffset now = DateTimeOffset.Now;
      DateTimeOffset dateTimeOffset = this.hoverExitOutsideSince.GetValueOrDefault();
      if (this.hoverExitOutsideSince == null)
      {
        dateTimeOffset = now;
        this.hoverExitOutsideSince = new DateTimeOffset?(dateTimeOffset);
      }
      if (now - this.hoverExitOutsideSince.Value < NotchView.HoverExitCloseDelay)
      {
        return;
      }
      this.StopHoverExitWatch();
      this.SetHover(false);
    }

    // Token: 0x06001166 RID: 4454 RVA: 0x0058CCA4 File Offset: 0x0058CCA4
    private void StopHoverExitWatch()
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.hoverExitTimer;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      this.hoverExitOutsideSince = null;
    }

    // Token: 0x06001167 RID: 4455 RVA: 0x0058CCC4 File Offset: 0x0058CCC4
    private void SetHover(bool hovering)
    {
      if (this.IsHovering == hovering)
      {
        return;
      }
      this.IsHovering = hovering;
      if (!hovering)
      {
        this.stickyRectPx = null;
      }
      Log.Info("notch", hovering ? "pointer in" : "pointer out");
      Action<bool> hoverChanged = this.HoverChanged;
      if (hoverChanged == null)
      {
        return;
      }
      hoverChanged(hovering);
    }

    // Token: 0x06001168 RID: 4456 RVA: 0x0058CD1C File Offset: 0x0058CD1C
    public void Relayout()
    {
      this.Apply(this.current, false, true);
    }

    // Token: 0x06001169 RID: 4457 RVA: 0x0058CD2C File Offset: 0x0058CD2C
    public void RelayoutPeek()
    {
      NotchPresentation.LiveEvent liveEvent = this.current as NotchPresentation.LiveEvent;
      if (liveEvent != null && liveEvent.Event is NotchLiveEvent.AgentPeek)
      {
        this.Relayout();
      }
    }

    // Token: 0x1700033C RID: 828
    // (get) Token: 0x0600116A RID: 4458 RVA: 0x0058CD5C File Offset: 0x0058CD5C
    public bool HasKeyboard
    {
      get
      {
        return this.hasKeyboard;
      }
    }

    // Token: 0x1400004B RID: 75
    // (add) Token: 0x0600116B RID: 4459 RVA: 0x0058CD64 File Offset: 0x0058CD64
    // (remove) Token: 0x0600116C RID: 4460 RVA: 0x0058CD9C File Offset: 0x0058CD9C
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<bool> KeyboardChanged;

    // Token: 0x0600116D RID: 4461 RVA: 0x0058CDD4 File Offset: 0x0058CDD4
    private void SetHasKeyboard(bool has)
    {
      has = has && this.Window.CanActivate;
      if (has == this.hasKeyboard)
      {
        return;
      }
      this.hasKeyboard = has;
      Action<bool> keyboardChanged = this.KeyboardChanged;
      if (keyboardChanged == null)
      {
        return;
      }
      keyboardChanged(has);
    }

    // Token: 0x0600116E RID: 4462 RVA: 0x0058CE0C File Offset: 0x0058CE0C
    public void RebuildAfterLayoutCycle()
    {
      foreach (NotchView.Frame frame in new NotchView.Frame[] { this.active, this.peekFrame }.OfType<NotchView.Frame>().Distinct<NotchView.Frame>())
      {
        DispatcherQueueTimer removal = frame.Removal;
        if (removal != null)
        {
          removal.Stop();
        }
        frame.Removal = null;
        frame.Surface.Detached();
        this.contentLayer.Children.Remove(frame.Root);
      }
      this.active = null;
      this.peekFrame = null;
      this.Apply(this.Services.Presenter.Presentation, true, false);
    }

    // Token: 0x0600116F RID: 4463 RVA: 0x0058CED0 File Offset: 0x0058CED0
    public void ReleaseKeyboardIfUnheld()
    {
      NotchView.Frame frame = this.active;
      if (frame != null && frame.Surface.HoldsKeyboard)
      {
        return;
      }
      this.ReleaseKeyboard();
    }

    // Token: 0x06001170 RID: 4464 RVA: 0x0058CEF4 File Offset: 0x0058CEF4
    public void Apply(NotchPresentation next, bool force = false, bool relayoutOnly = false)
    {
      Stopwatch stopwatch = Stopwatch.StartNew();
      this.NoteStickyRect();
      NotchPresentation notchPresentation = this.current;
      bool flag = NotchView.SameKind(notchPresentation, next);
      if (!relayoutOnly && NotchView.IsAmbient(notchPresentation) && !NotchView.IsAmbient(next) && this.shown && !this.IsHovering)
      {
        this.FollowCursorDisplay();
      }
      bool isAmbientHidden = this.IsAmbientHidden;
      this.current = next;
      if (this.ambientHidden && isAmbientHidden != this.IsAmbientHidden)
      {
        this.ApplyAmbientVisibility();
      }
      NotchAccessibility.Announce(this.Window.Root, next);
      NotchGeometry.SizeContext sizeContext = this.SizeContext;
      IAgentService agents = this.Services.Agents;
      sizeContext.AgentDriving = agents != null && AgentColors.RunningTasks(agents).Count > 0;
      ValueTuple<double, double> valueTuple = NotchGeometry.PanelSize(next, this.SizeContext);
      SwiftSpring? swiftSpring = NotchGeometry.SpringFor(next);
      if (NotchChatSize.IsResizing)
      {
        NotchPresentation.LiveEvent liveEvent = next as NotchPresentation.LiveEvent;
        if (liveEvent != null && liveEvent.Event is NotchLiveEvent.AgentPeek)
        {
          swiftSpring = null;
        }
      }
      bool flag2 = NotchGeometry.IsFloatingSliver(next);
      if (!relayoutOnly)
      {
        if (this.active != null && flag && this.active.Surface.CanUpdate(next) && !force)
        {
          this.active.Surface.Update(next);
          valueTuple = NotchGeometry.PanelSize(next, this.SizeContext);
        }
        else
        {
          if (this.active != null)
          {
            this.Retire(this.active, swiftSpring ?? NotchSprings.Content);
          }
          this.active = null;
          if (!(next is NotchPresentation.Idle))
          {
            NotchPresentation.LiveEvent liveEvent = next as NotchPresentation.LiveEvent;
            NotchView.Frame frame = ((liveEvent != null && liveEvent.Event is NotchLiveEvent.AgentPeek) ? this.peekFrame : null);
            NotchSurface notchSurface = ((frame != null) ? frame.Surface : null) ?? NotchSurfaces.Create(next, this);
            if (notchSurface != null)
            {
              bool flag3 = ((frame != null) ? frame.Removal : null) != null;
              this.active = ((frame != null) ? this.Remount(frame, next, valueTuple) : this.Mount(notchSurface, next, valueTuple));
              if (notchSurface is NotchPeekSurface)
              {
                this.peekFrame = this.active;
              }
              notchSurface.Attach(this, flag3);
              valueTuple = NotchGeometry.PanelSize(next, this.SizeContext);
              SwiftSpring swiftSpring2 = ((next is NotchPresentation.TextInput) ? NotchSprings.TextInputOpen : NotchSprings.Content);
              if (swiftSpring == null)
              {
                this.active.Root.Opacity = 1.0;
              }
              else if (flag3)
              {
                Anim.ContentBack(this.active.Root, swiftSpring2);
              }
              else
              {
                Anim.ContentIn(this.active.Root, swiftSpring2);
              }
            }
          }
        }
      }
      if (this.active != null)
      {
        this.Resize(this.active, valueTuple);
      }
      this.silhouette.AnimateTo(valueTuple.Item1, valueTuple.Item2, NotchGeometry.BottomCornerRadius(next), this.Gap(next), !flag2, flag2 ? 1.0 : 1.0, flag2 > false, swiftSpring);
      this.UpdateMultiAgentRotation(notchPresentation, next);
      NotchCornerGlow notchCornerGlow = this.glow;
      if (notchCornerGlow != null)
      {
        notchCornerGlow.Update(NotchGeometry.Shown(next), this.AgentTint(NotchGeometry.Shown(next)));
      }
      this.SyncWorkingSweep();
      if (next is NotchPresentation.LiveEvent)
      {
        this.glowQuiesceTimer.Stop();
      }
      else
      {
        this.glowQuiesceTimer.Start();
      }
      ValueTuple<double, double> valueTuple2 = this.currentSize;
      this.currentSize = valueTuple;
      if (swiftSpring != null)
      {
        SwiftSpring valueOrDefault = swiftSpring.GetValueOrDefault();
        ValueTuple<double, double> valueTuple3 = valueTuple;
        ValueTuple<double, double> valueTuple4 = valueTuple2;
        if ((valueTuple3.Item1 != valueTuple4.Item1 || valueTuple3.Item2 != valueTuple4.Item2) && !Anim.Instant && !Anim.ReduceMotion)
        {
          ValueTuple<double, double> valueTuple5 = new ValueTuple<double, double>(Math.Max(valueTuple2.Item1, valueTuple.Item1 * 1.03 + 4.0), Math.Max(valueTuple2.Item2, valueTuple.Item2 * 1.03 + 4.0));
          ValueTuple<double, double>? valueTuple6 = this.regionHold;
          if (valueTuple6 != null)
          {
            ValueTuple<double, double> valueOrDefault2 = valueTuple6.GetValueOrDefault();
            if (DateTimeOffset.Now < this.regionHoldUntil)
            {
              valueTuple5 = new ValueTuple<double, double>(Math.Max(valueTuple5.Item1, valueOrDefault2.Item1), Math.Max(valueTuple5.Item2, valueOrDefault2.Item2));
            }
          }
          this.regionHold = new ValueTuple<double, double>?(valueTuple5);
          DateTimeOffset dateTimeOffset = DateTimeOffset.Now + valueOrDefault.SettleTime;
          if (dateTimeOffset > this.regionHoldUntil)
          {
            this.regionHoldUntil = dateTimeOffset;
          }
        }
      }
      this.ApplyHeldRegion();
      this.UpdateKeyboardOwnership(notchPresentation, next);
      this.PublishVisibleSurface();
      if (!relayoutOnly)
      {
        this.KeepOnTop("present " + NotchView.Describe(next));
      }
      if (!relayoutOnly)
      {
        string text = "notch";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 4);
        defaultInterpolatedStringHandler.AppendLiteral("present ");
        defaultInterpolatedStringHandler.AppendFormatted(NotchView.Describe(next));
        defaultInterpolatedStringHandler.AppendLiteral(" ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(valueTuple.Item1, "0");
        defaultInterpolatedStringHandler.AppendLiteral("x");
        defaultInterpolatedStringHandler.AppendFormatted<double>(valueTuple.Item2, "0");
        defaultInterpolatedStringHandler.AppendLiteral(" in ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(stopwatch.Elapsed.TotalMilliseconds, "0.0");
        defaultInterpolatedStringHandler.AppendLiteral(" ms");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      }
    }

    // Token: 0x06001171 RID: 4465 RVA: 0x0058D468 File Offset: 0x0058D468
    private Color? AgentTint(NotchPresentation p)
    {
      NotchPresentation.LiveEvent liveEvent = p as NotchPresentation.LiveEvent;
      if (liveEvent != null)
      {
        NotchLiveEvent @event = liveEvent.Event;
        NotchLiveEvent.AgentTask agentTask = @event as NotchLiveEvent.AgentTask;
        if (agentTask != null)
        {
          return new Color?(AgentColors.For(agentTask.TaskId, 0));
        }
        if (@event is NotchLiveEvent.AgentMultiActivity || @event is NotchLiveEvent.ActivityThinking || @event is NotchLiveEvent.ActivityCustomStatus)
        {
          AgentTaskSnapshot agentTaskSnapshot = AgentColors.DisplayedRunningTask(this.Services.Agents, this.MultiAgentRotation, liveEvent.Event is NotchLiveEvent.AgentMultiActivity);
          if (agentTaskSnapshot != null)
          {
            return new Color?(AgentColors.For(agentTaskSnapshot.TaskId, 0));
          }
          return null;
        }
      }
      return null;
    }

    // Token: 0x06001172 RID: 4466 RVA: 0x0058D510 File Offset: 0x0058D510
    private void UpdateMultiAgentRotation(NotchPresentation previous, NotchPresentation next)
    {
      NotchPresentation.LiveEvent liveEvent = previous as NotchPresentation.LiveEvent;
      bool flag = liveEvent != null && liveEvent.Event is NotchLiveEvent.AgentMultiActivity;
      liveEvent = next as NotchPresentation.LiveEvent;
      if (liveEvent == null || !(liveEvent.Event is NotchLiveEvent.AgentMultiActivity))
      {
        this.rotationTimer.Stop();
        return;
      }
      this.MultiAgentRotation.Clamp(AgentColors.RunningTasks(this.Services.Agents).Count);
      if (flag)
      {
        return;
      }
      this.MultiAgentRotation.Restart(DateTimeOffset.UtcNow);
      this.ScheduleRotation();
    }

    // Token: 0x06001173 RID: 4467 RVA: 0x0058D59C File Offset: 0x0058D59C
    private void ScheduleRotation()
    {
      this.rotationTimer.Interval = this.MultiAgentRotation.UntilDue(DateTimeOffset.UtcNow) + TimeSpan.FromMilliseconds(20L, 0L);
      this.rotationTimer.Start();
    }

    // Token: 0x06001174 RID: 4468 RVA: 0x0058D5D4 File Offset: 0x0058D5D4
    private void RotateMultiAgentStrip()
    {
      NotchPresentation.LiveEvent liveEvent = this.current as NotchPresentation.LiveEvent;
      if (liveEvent == null || !(liveEvent.Event is NotchLiveEvent.AgentMultiActivity))
      {
        return;
      }
      int count = AgentColors.RunningTasks(this.Services.Agents).Count;
      this.MultiAgentRotation.Clamp(count);
      if (this.MultiAgentRotation.AdvanceIfHoldElapsed(DateTimeOffset.UtcNow, count))
      {
        Action multiAgentRotated = this.MultiAgentRotated;
        if (multiAgentRotated != null)
        {
          multiAgentRotated();
        }
        NotchCornerGlow notchCornerGlow = this.glow;
        if (notchCornerGlow != null)
        {
          notchCornerGlow.Update(NotchGeometry.Shown(this.current), this.AgentTint(NotchGeometry.Shown(this.current)));
        }
      }
      this.ScheduleRotation();
    }

    // Token: 0x06001175 RID: 4469 RVA: 0x0058D678 File Offset: 0x0058D678
    public static bool IsAmbient(NotchPresentation p)
    {
      bool flag = p is NotchPresentation.Idle;
      if (!flag)
      {
        NotchPresentation.LiveEvent liveEvent = p as NotchPresentation.LiveEvent;
        bool flag2;
        if (liveEvent != null)
        {
          NotchLiveEvent @event = liveEvent.Event;
          if (@event is NotchLiveEvent.RestingMark || @event is NotchLiveEvent.SkillPinned || @event is NotchLiveEvent.AgentMultiActivity || @event is NotchLiveEvent.AgentTask)
          {
            flag2 = true;
            goto IL_0044;
          }
        }
        flag2 = false;
        IL_0044:
        flag = flag2;
      }
      return flag;
    }

    // Token: 0x06001176 RID: 4470 RVA: 0x0058D6CC File Offset: 0x0058D6CC
    private static bool SameKind(NotchPresentation a, NotchPresentation b)
    {
      if (a.GetType() != b.GetType())
      {
        return false;
      }
      NotchPresentation.LiveEvent liveEvent = a as NotchPresentation.LiveEvent;
      if (liveEvent != null)
      {
        NotchPresentation.LiveEvent liveEvent2 = b as NotchPresentation.LiveEvent;
        if (liveEvent2 != null)
        {
          return liveEvent.Event.GetType() == liveEvent2.Event.GetType();
        }
      }
      return true;
    }

    // Token: 0x06001177 RID: 4471 RVA: 0x0058D720 File Offset: 0x0058D720
    public static string Describe(NotchPresentation p)
    {
      NotchPresentation.LiveEvent liveEvent = p as NotchPresentation.LiveEvent;
      string text;
      if (liveEvent != null)
      {
        text = "live:" + liveEvent.Event.GetType().Name;
      }
      else
      {
        text = p.GetType().Name;
      }
      return text;
    }

    // Token: 0x06001178 RID: 4472 RVA: 0x0058D764 File Offset: 0x0058D764
    private static Thickness ContentPadding(NotchPresentation p)
    {
      return new Thickness(18.0, NotchGeometry.ContentTopPadding(p), 18.0, NotchGeometry.ContentBottomPadding(p));
    }

    // Token: 0x06001179 RID: 4473 RVA: 0x0058D78C File Offset: 0x0058D78C
    private NotchView.Frame Mount(NotchSurface surface, NotchPresentation p, [TupleElementNames(new string[] { "W", "H" })] [Nullable(0)] ValueTuple<double, double> size)
    {
      Border border = new Border
      {
        Padding = NotchView.ContentPadding(p),
        Child = surface.View
      };
      surface.View.VerticalAlignment = surface.ContentAlignment;
      Grid grid = new Grid();
      grid.Children.Add(border);
      FrameworkElement overlay = surface.Overlay;
      if (overlay != null)
      {
        grid.Children.Add(overlay);
      }
      this.contentLayer.Children.Add(grid);
      NotchView.Frame frame = new NotchView.Frame
      {
        Surface = surface,
        Root = grid,
        Padded = border
      };
      this.Resize(frame, size);
      return frame;
    }

    // Token: 0x0600117A RID: 4474 RVA: 0x0058D828 File Offset: 0x0058D828
    private NotchView.Frame Remount(NotchView.Frame f, NotchPresentation p, [TupleElementNames(new string[] { "W", "H" })] [Nullable(0)] ValueTuple<double, double> size)
    {
      DispatcherQueueTimer removal = f.Removal;
      if (removal != null)
      {
        TimerRoots.Release(removal);
        f.Removal = null;
      }
      if (!this.contentLayer.Children.Contains(f.Root))
      {
        this.contentLayer.Children.Add(f.Root);
      }
      f.Root.IsHitTestVisible = true;
      f.Padded.Padding = NotchView.ContentPadding(p);
      this.Resize(f, size);
      return f;
    }

    // Token: 0x0600117B RID: 4475 RVA: 0x0058D8A0 File Offset: 0x0058D8A0
    [NullableContext(0)]
    private void Resize([Nullable(1)] NotchView.Frame f, [TupleElementNames(new string[] { "W", "H" })] ValueTuple<double, double> size)
    {
      f.W = size.Item1;
      f.H = size.Item2;
      f.Root.Width = size.Item1;
      f.Root.Height = size.Item2;
      Canvas.SetLeft(f.Root, (this.envW - size.Item1) / 2.0);
      Canvas.SetTop(f.Root, this.Gap(this.current));
    }

    // Token: 0x0600117C RID: 4476 RVA: 0x0058D920 File Offset: 0x0058D920
    private void Retire(NotchView.Frame f, SwiftSpring spring)
    {
      f.Surface.Detached();
      f.Root.IsHitTestVisible = false;
      Anim.ContentOut(f.Root, spring);
      DispatcherQueueTimer t = this.dq.CreateTimer().Rooted();
      t.Interval = TimeSpan.FromSeconds(Math.Max(0.25, spring.SettleTime.TotalSeconds * 0.6));
      t.IsRepeating = false;
      f.Removal = t;
      t.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        if (f.Removal != t)
        {
          return;
        }
        f.Removal = null;
        this.contentLayer.Children.Remove(f.Root);
      };
      t.Start();
    }

    // Token: 0x0600117D RID: 4477 RVA: 0x0058DA00 File Offset: 0x0058DA00
    private void UpdateKeyboardOwnership(NotchPresentation previous, NotchPresentation next)
    {
      NotchPresentation.LiveEvent liveEvent = next as NotchPresentation.LiveEvent;
      if (liveEvent != null && liveEvent.Event is NotchLiveEvent.AgentPeek)
      {
        NotchView.Frame frame = this.active;
        if ((frame != null && frame.Surface.HoldsKeyboard) || Environment.TickCount64 - this.keyboardRequestedAt < 1500L)
        {
          return;
        }
      }
      NotchView.Frame frame2 = this.active;
      bool flag = frame2 != null && frame2.Surface.WantsKeyboard && next is NotchPresentation.TextInput;
      bool flag2 = previous is NotchPresentation.TextInput;
      if (flag && !this.Window.CanActivate)
      {
        IntPtr foregroundWindow = Native.GetForegroundWindow();
        if (foregroundWindow != this.Window.Hwnd)
        {
          this.previousForeground = foregroundWindow;
        }
        this.Window.CanActivate = true;
        Native.ForceForeground(this.Window.Hwnd);
        this.Window.Activate();
        this.active.Surface.FocusInput();
        return;
      }
      if (!flag && this.Window.CanActivate)
      {
        this.Window.CanActivate = false;
        if (Native.GetForegroundWindow() == this.Window.Hwnd && this.previousForeground != 0 && Native.IsWindow(this.previousForeground))
        {
          Log.Info("notch", "keyboard handed back to the app behind (" + next.GetType().Name + ")");
          Native.SetForegroundWindow(this.previousForeground);
        }
        this.previousForeground = (IntPtr)0;
        return;
      }
      if (flag && flag2)
      {
        NotchView.Frame frame3 = this.active;
        if (frame3 == null)
        {
          return;
        }
        frame3.Surface.FocusInput();
      }
    }

    // Token: 0x0600117E RID: 4478 RVA: 0x0058DB7C File Offset: 0x0058DB7C
    public void RequestKeyboard()
    {
      this.keyboardRequestedAt = Environment.TickCount64;
      IntPtr foregroundWindow = Native.GetForegroundWindow();
      if (this.Window.CanActivate)
      {
        if (foregroundWindow != this.Window.Hwnd)
        {
          if (foregroundWindow != 0 && !Native.IsOwnWindow(foregroundWindow))
          {
            this.previousForeground = foregroundWindow;
          }
          this.TakeForeground();
        }
        this.SetHasKeyboard(Native.GetForegroundWindow() == this.Window.Hwnd);
        return;
      }
      if (foregroundWindow != this.Window.Hwnd && !Native.IsOwnWindow(foregroundWindow))
      {
        this.previousForeground = foregroundWindow;
      }
      this.Window.CanActivate = true;
      this.TakeForeground();
      this.SetHasKeyboard(Native.GetForegroundWindow() == this.Window.Hwnd);
    }

    // Token: 0x0600117F RID: 4479 RVA: 0x0058DC30 File Offset: 0x0058DC30
    private void TakeForeground()
    {
      Native.ForceForeground(this.Window.Hwnd);
      if (Native.GetForegroundWindow() == this.Window.Hwnd)
      {
        return;
      }
      this.Window.Activate();
      if (Native.GetForegroundWindow() != this.Window.Hwnd)
      {
        Log.Warn("notch", "couldn't take the keyboard (in front: " + Native.DescribeForeground(Native.GetForegroundWindow()) + ")");
      }
    }

    // Token: 0x1700033D RID: 829
    // (get) Token: 0x06001180 RID: 4480 RVA: 0x0058DCA0 File Offset: 0x0058DCA0
    // (set) Token: 0x06001181 RID: 4481 RVA: 0x0058DCA8 File Offset: 0x0058DCA8
    public Func<IntPtr, bool> IsNonTypingWindow { get; set; } = (IntPtr _) => false;

    // Token: 0x06001182 RID: 4482 RVA: 0x0058DCB4 File Offset: 0x0058DCB4
    private bool TakeKeyboardBackFrom(IntPtr activated)
    {
      if (activated != 0 && activated != this.Window.Hwnd && this.Window.CanActivate)
      {
        NotchView.Frame frame = this.active;
        if (frame != null && frame.Surface.HoldsKeyboard)
        {
          if (!this.IsNonTypingWindow(activated))
          {
            return false;
          }
          string text = "notch";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(86, 1);
          defaultInterpolatedStringHandler.AppendLiteral("HeyClicky's non-typing window ");
          defaultInterpolatedStringHandler.AppendFormatted<IntPtr>(activated);
          defaultInterpolatedStringHandler.AppendLiteral(" took activation from the chat; taking the keyboard back");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
          this.TakeForeground();
          this.SetHasKeyboard(Native.GetForegroundWindow() == this.Window.Hwnd);
          return this.hasKeyboard;
        }
      }
      return false;
    }

    // Token: 0x06001183 RID: 4483 RVA: 0x0058DD6C File Offset: 0x0058DD6C
    public void ReleaseKeyboard()
    {
      if (!this.Window.CanActivate)
      {
        return;
      }
      this.keyboardRequestedAt = -1500L;
      this.Window.CanActivate = false;
      if (Native.GetForegroundWindow() == this.Window.Hwnd && this.previousForeground != 0 && Native.IsWindow(this.previousForeground))
      {
        Native.SetForegroundWindow(this.previousForeground);
      }
      this.previousForeground = (IntPtr)0;
      this.SetHasKeyboard(false);
    }

    // Token: 0x040007A9 RID: 1961
    private readonly Microsoft.UI.Composition.Compositor c;

    // Token: 0x040007AA RID: 1962
    private readonly NotchSilhouette silhouette;

    // Token: 0x040007AB RID: 1963
    [Nullable(2)]
    private readonly NotchSystemLayer systemLayer;

    // Token: 0x040007AC RID: 1964
    [Nullable(2)]
    private readonly NotchCornerGlow glow;

    // Token: 0x040007AD RID: 1965
    private readonly NotchWorkingSweep workingSweep;

    // Token: 0x040007AE RID: 1966
    private readonly Canvas contentLayer;

    // Token: 0x040007AF RID: 1967
    private readonly DispatcherQueue dq;

    // Token: 0x040007B0 RID: 1968
    private readonly DispatcherQueueTimer regionTimer;

    // Token: 0x040007B1 RID: 1969
    private readonly DispatcherQueueTimer glowQuiesceTimer;

    // Token: 0x040007B2 RID: 1970
    private readonly DispatcherQueueTimer rotationTimer;

    // Token: 0x040007B3 RID: 1971
    private readonly DispatcherQueueTimer zOrderTimer;

    // Token: 0x040007B4 RID: 1972
    private double envW = 820.0;

    // Token: 0x040007B5 RID: 1973
    private double envH = 820.0;

    // Token: 0x040007B6 RID: 1974
    private double contentScale = 1.0;

    // Token: 0x040007B7 RID: 1975
    private bool streamMode;

    // Token: 0x040007B8 RID: 1976
    private readonly ScaleTransform rootScale = new ScaleTransform();

    // Token: 0x040007B9 RID: 1977
    private Displays.Monitor monitor;

    // Token: 0x040007BA RID: 1978
    private NotchPresentation current = new NotchPresentation.Idle();

    // Token: 0x040007BB RID: 1979
    [TupleElementNames(new string[] { "W", "H" })]
    [Nullable(0)]
    private ValueTuple<double, double> currentSize = new ValueTuple<double, double>(60.0, 8.0);

    // Token: 0x040007BC RID: 1980
    [Nullable(2)]
    private NotchView.Frame active;

    // Token: 0x040007BD RID: 1981
    [Nullable(2)]
    private NotchView.Frame peekFrame;

    // Token: 0x040007BE RID: 1982
    private IntPtr previousForeground;

    // Token: 0x040007BF RID: 1983
    private bool shown;

    // Token: 0x040007C1 RID: 1985
    private IntPtr lastCoveringWindow;

    // Token: 0x040007C2 RID: 1986
    private bool ambientHidden;

    // Token: 0x040007C3 RID: 1987
    private readonly List<string> pendingFiles = new List<string>();

    // Token: 0x040007C4 RID: 1988
    private bool composerOpenedByDrag;

    // Token: 0x040007C5 RID: 1989
    [TupleElementNames(new string[] { "W", "H" })]
    [Nullable(0)]
    private ValueTuple<double, double>? regionHold;

    // Token: 0x040007C6 RID: 1990
    private DateTimeOffset regionHoldUntil = DateTimeOffset.MinValue;

    // Token: 0x040007C7 RID: 1991
    private const double KeepOpenMarginDip = 24.0;

    // Token: 0x040007C8 RID: 1992
    private static readonly TimeSpan HoverExitCloseDelay = TimeSpan.FromSeconds(0.35);

    // Token: 0x040007C9 RID: 1993
    [Nullable(2)]
    private DispatcherQueueTimer hoverExitTimer;

    // Token: 0x040007CA RID: 1994
    private DateTimeOffset? hoverExitOutsideSince;

    // Token: 0x040007CB RID: 1995
    [TupleElementNames(new string[] { "X", "Y", "W", "H" })]
    [Nullable(0)]
    private ValueTuple<int, int, int, int>? stickyRectPx;

    // Token: 0x040007CC RID: 1996
    [TupleElementNames(new string[] { "ExpandedReplyEntryId", "FullReplyContentHeight" })]
    [Nullable(new byte[] { 0, 2 })]
    private ValueTuple<string, double?> lastFullReply;

    // Token: 0x040007CD RID: 1997
    private bool hasKeyboard;

    // Token: 0x040007D0 RID: 2000
    private long keyboardRequestedAt = -1500L;

    // Token: 0x040007D1 RID: 2001
    private const long KeyboardRequestGraceMs = 1500L;

    // Token: 0x02000682 RID: 1666
    [Nullable(0)]
    [RequiredMember]
    private sealed class Frame
    {
      // Token: 0x0600324E RID: 12878 RVA: 0x00671E0C File Offset: 0x00671E0C
      [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
      [CompilerFeatureRequired("RequiredMembers")]
      public Frame()
      {
      }

      // Token: 0x04001DBD RID: 7613
      [RequiredMember]
      public NotchSurface Surface;

      // Token: 0x04001DBE RID: 7614
      [RequiredMember]
      public Grid Root;

      // Token: 0x04001DBF RID: 7615
      [RequiredMember]
      public Border Padded;

      // Token: 0x04001DC0 RID: 7616
      public double W;

      // Token: 0x04001DC1 RID: 7617
      public double H;

      // Token: 0x04001DC2 RID: 7618
      [Nullable(2)]
      public DispatcherQueueTimer Removal;
    }
  }
}
