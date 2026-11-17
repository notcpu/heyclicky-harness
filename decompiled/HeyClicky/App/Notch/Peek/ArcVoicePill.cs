using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Dock;
using HeyClicky.App.Shell;
using HeyClicky.Core.Notch;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000169 RID: 361
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class ArcVoicePill
  {
    // Token: 0x060013EF RID: 5103 RVA: 0x005A65C0 File Offset: 0x005A65C0
    public ArcVoicePill(NotchPresenter presenter, ArcDock dock, INotchHost host, bool excludeFromCapture)
    {
      this.presenter = presenter;
      this.dock = dock;
      this.host = host;
      this.excludeFromCapture = excludeFromCapture;
    }

    // Token: 0x1700039E RID: 926
    // (get) Token: 0x060013F0 RID: 5104 RVA: 0x005A6610 File Offset: 0x005A6610
    public IntPtr Hwnd
    {
      get
      {
        OverlayWindow overlayWindow = this.window;
        if (overlayWindow == null || !overlayWindow.IsShown)
        {
          return (IntPtr)0;
        }
        return this.window.Hwnd;
      }
    }

    // Token: 0x1700039F RID: 927
    // (get) Token: 0x060013F1 RID: 5105 RVA: 0x005A6634 File Offset: 0x005A6634
    [TupleElementNames(new string[] { "Phase", "Label" })]
    [Nullable(new byte[] { 0, 2 })]
    private ValueTuple<NotchActivityPhase, string>? Phase
    {
      [return: TupleElementNames(new string[] { "Phase", "Label" })]
      [return: Nullable(new byte[] { 0, 2 })]
      get
      {
        if (!this.presenter.IsShowingNotchAgentPeek || !this.presenter.IsNotchHeldOpenByArc)
        {
          return null;
        }
        return this.presenter.VoicePill;
      }
    }

    // Token: 0x060013F2 RID: 5106 RVA: 0x005A6670 File Offset: 0x005A6670
    public void Sync()
    {
      ValueTuple<NotchActivityPhase, string>? phase = this.Phase;
      if (phase != null)
      {
        DispatcherQueueTimer dispatcherQueueTimer = this.hideTimer;
        if (dispatcherQueueTimer != null)
        {
          dispatcherQueueTimer.Stop();
        }
        this.EnsureWindow();
        NotchVoicePill.Update(this.layer, phase, this.host);
        if (!this.window.IsShown)
        {
          this.window.ShowNoActivate();
        }
        this.window.Root.IsHitTestVisible = false;
        ArcDockView.KeepAboveDeck(this.window.Hwnd);
        return;
      }
      if (this.window == null || !this.window.IsShown)
      {
        return;
      }
      NotchVoicePill.Update(this.layer, null, this.host);
      if (this.hideTimer == null)
      {
        this.hideTimer = this.CreateHideTimer();
      }
      this.hideTimer.Stop();
      this.hideTimer.Start();
    }

    // Token: 0x060013F3 RID: 5107 RVA: 0x005A674C File Offset: 0x005A674C
    private DispatcherQueueTimer CreateHideTimer()
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.layer.DispatcherQueue.CreateTimer().Rooted();
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(0.35);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        if (this.Phase == null)
        {
          OverlayWindow overlayWindow = this.window;
          if (overlayWindow == null)
          {
            return;
          }
          overlayWindow.HideWindow();
        }
      };
      return dispatcherQueueTimer;
    }

    // Token: 0x060013F4 RID: 5108 RVA: 0x005A679C File Offset: 0x005A679C
    private void EnsureWindow()
    {
      if (this.window == null)
      {
        this.window = new OverlayWindow("HeyClicky Arc Voice", this.excludeFromCapture, true, false, true, false, false);
        this.window.Root.Children.Add(this.layer);
        this.window.Root.IsHitTestVisible = false;
      }
      Displays.Monitor monitor = this.dock.HostMonitor();
      ArcScreenMetrics? arcScreenMetrics;
      ArcRect? arcRect = ((this.dock.Metrics != null) ? arcScreenMetrics.GetValueOrDefault().HardwareNotch : null);
      double num = ((arcRect == null) ? 10.0 : (arcRect.GetValueOrDefault().MaxY + 2.0));
      this.window.Root.Width = (double)monitor.Bounds.Width / monitor.Scale;
      this.window.Root.Height = 80.0;
      this.layer.Margin = new Thickness(0.0, num, 0.0, 0.0);
      this.window.PlacePx(monitor.Bounds.Left, monitor.Bounds.Top, monitor.Bounds.Width, (int)Math.Ceiling(80.0 * monitor.Scale));
    }

    // Token: 0x04000954 RID: 2388
    private readonly NotchPresenter presenter;

    // Token: 0x04000955 RID: 2389
    private readonly ArcDock dock;

    // Token: 0x04000956 RID: 2390
    private readonly INotchHost host;

    // Token: 0x04000957 RID: 2391
    private readonly bool excludeFromCapture;

    // Token: 0x04000958 RID: 2392
    [Nullable(2)]
    private OverlayWindow window;

    // Token: 0x04000959 RID: 2393
    private readonly Grid layer = new Grid
    {
      IsHitTestVisible = false,
      VerticalAlignment = VerticalAlignment.Top,
      HorizontalAlignment = HorizontalAlignment.Center
    };

    // Token: 0x0400095A RID: 2394
    [Nullable(2)]
    private DispatcherQueueTimer hideTimer;
  }
}
