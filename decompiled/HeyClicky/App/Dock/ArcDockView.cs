using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Interop;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using HeyClicky.Platform.Win32;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Input;
using Windows.Foundation;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000314 RID: 788
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class ArcDockView
  {
    // Token: 0x06002231 RID: 8753 RVA: 0x0062092C File Offset: 0x0062092C
    public ArcDockView(ArcDock dock, bool excludeFromCapture)
    {
      ArcDockView <>4__this = this;
      this.dock = dock;
      this.dq = DispatcherQueue.GetForCurrentThread();
      this.backdropWindow = new OverlayWindow("HeyClicky Agents Arc", excludeFromCapture, true, false, true, false, false);
      this.backdropWindow.Root.Children.Add(this.backdrop);
      this.backdropWindow.Root.IsHitTestVisible = false;
      this.backdrop.Opacity = 0.0;
      this.deckWindow = new OverlayWindow("HeyClicky Agents", excludeFromCapture, false, false, true, false, false);
      this.deckWindow.StayLayered();
      this.deckWindow.SetIgnoresMouseEvents(true);
      this.deckWindow.Root.RequestedTheme = ElementTheme.Light;
      this.deckWindow.Root.Children.Add(this.deck);
      this.more = new ArcDockView.MorePill[]
      {
        new ArcDockView.MorePill(),
        new ArcDockView.MorePill()
      };
      foreach (ArcDockView.MorePill morePill in this.more)
      {
        this.deck.Children.Add(morePill.Host);
        Canvas.SetZIndex(morePill.Host, 200);
      }
      ToolTipService.SetToolTip(this.bringToFront, "Bring to the front");
      this.bringToFront.Click += delegate
      {
        if (<>4__this.bringToFrontIndex >= 0)
        {
          dock.ShowDeckWindow(<>4__this.bringToFrontIndex, "side_window_click");
        }
      };
      this.deck.Children.Add(this.bringToFront);
      Canvas.SetZIndex(this.bringToFront, 150);
      this.deckWindow.Root.PointerWheelChanged += delegate(object _, PointerRoutedEventArgs e)
      {
        PointerPoint currentPoint = e.GetCurrentPoint(<>4__this.deckWindow.Root);
        if (dock.HandleDeckScroll(currentPoint.Properties.MouseWheelDelta, currentPoint.Properties.IsHorizontalMouseWheel, currentPoint.Timestamp))
        {
          e.Handled = true;
        }
      };
      this.deckWindow.RawMessage += delegate(uint msg, IntPtr _, IntPtr _)
      {
        bool flag = msg == 26U || msg == 126U || msg == 736U;
        if (flag)
        {
          dock.NotchChanged();
        }
      };
      this.monitor = Displays.Primary();
      ScreenCaptureYield.Register(this.deckWindow.Hwnd);
      ScreenCaptureYield.Register(this.backdropWindow.Hwnd);
    }

    // Token: 0x17000655 RID: 1621
    // (get) Token: 0x06002232 RID: 8754 RVA: 0x00620B7C File Offset: 0x00620B7C
    public bool IsShowing
    {
      get
      {
        return this.showing;
      }
    }

    // Token: 0x17000656 RID: 1622
    // (get) Token: 0x06002233 RID: 8755 RVA: 0x00620B84 File Offset: 0x00620B84
    public Displays.Monitor Monitor
    {
      get
      {
        return this.monitor;
      }
    }

    // Token: 0x17000657 RID: 1623
    // (get) Token: 0x06002234 RID: 8756 RVA: 0x00620B8C File Offset: 0x00620B8C
    public IReadOnlyDictionary<string, ArcCardView> Cards
    {
      get
      {
        return this.slots.Values.Where<ArcDockView.Slot>((ArcDockView.Slot s) => !s.Leaving).ToDictionary<ArcDockView.Slot, string, ArcCardView>((ArcDockView.Slot s) => s.Key, (ArcDockView.Slot s) => s.Card);
      }
    }

    // Token: 0x17000658 RID: 1624
    // (get) Token: 0x06002235 RID: 8757 RVA: 0x00620C0C File Offset: 0x00620C0C
    public UIElement DeckRoot
    {
      get
      {
        return this.deckWindow.Root;
      }
    }

    // Token: 0x17000659 RID: 1625
    // (get) Token: 0x06002236 RID: 8758 RVA: 0x00620C1C File Offset: 0x00620C1C
    public IntPtr DeckHwnd
    {
      get
      {
        return this.deckWindow.Hwnd;
      }
    }

    // Token: 0x1700065A RID: 1626
    // (get) Token: 0x06002237 RID: 8759 RVA: 0x00620C2C File Offset: 0x00620C2C
    // (set) Token: 0x06002238 RID: 8760 RVA: 0x00620C34 File Offset: 0x00620C34
    public bool TakesPointer { get; private set; }

    // Token: 0x06002239 RID: 8761 RVA: 0x00620C40 File Offset: 0x00620C40
    public void Show()
    {
      if (this.showing)
      {
        return;
      }
      this.showing = true;
      this.Place(this.dock.HostMonitor());
      this.backdropWindow.ShowNoActivate();
      this.backdropWindow.Root.IsHitTestVisible = false;
      this.deckWindow.ShowNoActivate();
      this.deckWindow.ClearRegion();
      this.deckWindow.SetIgnoresMouseEvents(!this.TakesPointer);
      this.KeepZOrder();
    }

    // Token: 0x0600223A RID: 8762 RVA: 0x00620CBC File Offset: 0x00620CBC
    public void Hide()
    {
      if (!this.showing)
      {
        return;
      }
      this.showing = false;
      this.SetTakesPointer(false);
      this.backdropWindow.HideWindow();
      this.deckWindow.HideWindow();
    }

    // Token: 0x0600223B RID: 8763 RVA: 0x00620CEC File Offset: 0x00620CEC
    public void SetTakesPointer(bool takes)
    {
      this.TakesPointer = takes;
      this.deckWindow.SetIgnoresMouseEvents(!takes);
    }

    // Token: 0x0600223C RID: 8764 RVA: 0x00620D04 File Offset: 0x00620D04
    public void Place(Displays.Monitor on)
    {
      this.monitor = on;
      double num = (double)on.Bounds.Width / on.Scale;
      double num2 = (double)on.Bounds.Height / on.Scale;
      foreach (OverlayWindow overlayWindow in new OverlayWindow[] { this.backdropWindow, this.deckWindow })
      {
        overlayWindow.Root.Width = num;
        overlayWindow.Root.Height = num2;
        overlayWindow.PlacePx(on.Bounds.Left, on.Bounds.Top, on.Bounds.Width, on.Bounds.Height);
      }
      this.deck.Width = num;
      this.deck.Height = num2;
      this.KeepZOrder();
    }

    // Token: 0x0600223D RID: 8765 RVA: 0x00620DEC File Offset: 0x00620DEC
    public void KeepZOrder()
    {
      if (!this.showing)
      {
        return;
      }
      Func<IntPtr> notchHwnd = ArcDockView.NotchHwnd;
      IntPtr intPtr = ((notchHwnd != null) ? notchHwnd() : ((IntPtr)0));
      Native.SetWindowPos(this.deckWindow.Hwnd, FullscreenGuard.TopmostInsertAfter(), 0, 0, 0, 0, 19U);
      if (intPtr != 0)
      {
        Native.SetWindowPos(this.backdropWindow.Hwnd, intPtr, 0, 0, 0, 0, 19U);
      }
      if (ArcDockView.aboveDeck != 0)
      {
        Native.SetWindowPos(ArcDockView.aboveDeck, FullscreenGuard.TopmostInsertAfter(), 0, 0, 0, 0, 19U);
      }
    }

    // Token: 0x0600223E RID: 8766 RVA: 0x00620E6C File Offset: 0x00620E6C
    public static void KeepAboveDeck(IntPtr hwnd)
    {
      ArcDockView.aboveDeck = hwnd;
      if (hwnd != 0)
      {
        Native.SetWindowPos(hwnd, FullscreenGuard.TopmostInsertAfter(), 0, 0, 0, 0, 19U);
      }
    }

    // Token: 0x1700065B RID: 1627
    // (get) Token: 0x0600223F RID: 8767 RVA: 0x00620E8C File Offset: 0x00620E8C
    // (set) Token: 0x06002240 RID: 8768 RVA: 0x00620E94 File Offset: 0x00620E94
    [Nullable(2)]
    public static Func<IntPtr> NotchHwnd
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06002241 RID: 8769 RVA: 0x00620E9C File Offset: 0x00620E9C
    public void Sync(IReadOnlyList<DockWindowKey> next)
    {
      bool flag = !next.Select<DockWindowKey, string>((DockWindowKey w) => w.RailId).SequenceEqual<string>(this.items.Select<DockWindowKey, string>((DockWindowKey w) => w.RailId));
      this.items = next.ToList<DockWindowKey>();
      HashSet<string> ids = this.items.Select<DockWindowKey, string>((DockWindowKey w) => w.RailId).ToHashSet<string>();
      IEnumerable<ArcDockView.Slot> values = this.slots.Values;
      Func<ArcDockView.Slot, bool> <>9__3;
      Func<ArcDockView.Slot, bool> func;
      if ((func = <>9__3) == null)
      {
        func = (<>9__3 = (ArcDockView.Slot s) => !ids.Contains(s.RailId) && !s.Leaving);
      }
      foreach (ArcDockView.Slot slot in values.Where<ArcDockView.Slot>(func).ToList<ArcDockView.Slot>())
      {
        this.Remove(slot);
      }
      foreach (DockWindowKey dockWindowKey in this.items)
      {
        ArcCardContent arcCardContent = this.dock.Content(dockWindowKey);
        ArcDockView.Slot slot2;
        if (this.slots.TryGetValue(dockWindowKey.RailId, out slot2) && !slot2.Leaving)
        {
          slot2.Key = dockWindowKey.Key;
          slot2.Card.Update(arcCardContent);
        }
        else
        {
          this.Add(dockWindowKey, arcCardContent);
        }
      }
      this.Layout(flag ? LayoutReason.Items : LayoutReason.Hover, true);
    }

    // Token: 0x06002242 RID: 8770 RVA: 0x00621060 File Offset: 0x00621060
    public void RebuildCards()
    {
      foreach (ArcDockView.Slot slot in this.slots.Values)
      {
        this.deck.Children.Remove(slot.Holder);
      }
      this.slots.Clear();
      foreach (DockWindowKey dockWindowKey in this.items)
      {
        this.Add(dockWindowKey, this.dock.Content(dockWindowKey));
      }
      this.Layout(LayoutReason.Items, false);
    }

    // Token: 0x06002243 RID: 8771 RVA: 0x0062112C File Offset: 0x0062112C
    private void Add(DockWindowKey window, ArcCardContent content)
    {
      ArcDockView.Slot slot;
      if (this.slots.Remove(window.RailId, out slot))
      {
        this.deck.Children.Remove(slot.Holder);
      }
      ArcCardView arcCardView = new ArcCardView(this.dock, content);
      Vector3 vector = new Vector3(247f, 157f, 0f);
      Grid grid = new Grid
      {
        Width = 494.0,
        Height = 314.0,
        CenterPoint = vector,
        Opacity = 0.0
      };
      Grid grid2 = new Grid();
      Visual elementVisual = ElementCompositionPreview.GetElementVisual(grid2);
      elementVisual.CenterPoint = vector;
      grid2.Children.Add(arcCardView);
      grid.Children.Add(grid2);
      PoseMotion poseMotion = new PoseMotion(grid, elementVisual, new Vector2(vector.X, vector.Y));
      poseMotion.SetRaster(this.raster);
      this.slots[window.RailId] = new ArcDockView.Slot(window.RailId, window.Key, arcCardView, grid, poseMotion);
      this.deck.Children.Add(grid);
    }

    // Token: 0x06002244 RID: 8772 RVA: 0x00621258 File Offset: 0x00621258
    private void Remove(ArcDockView.Slot slot)
    {
      slot.Leaving = true;
      slot.Holder.IsHitTestVisible = false;
      DeckPose target = slot.Motion.Target;
      if (Anim.ReduceMotion || Anim.Instant)
      {
        PoseMotion motion = slot.Motion;
        DeckPose deckPose = target;
        deckPose.Opacity = 0.0;
        motion.Jump(deckPose);
      }
      else
      {
        PoseMotion motion2 = slot.Motion;
        DeckPose deckPose = target;
        deckPose.Scale = target.Scale * 0.82;
        deckPose.Y = target.Y - 12.0;
        deckPose.Opacity = 0.0;
        motion2.SpringTo(deckPose, new SwiftSpring(0.38, 0.86), 0.0);
      }
      DockTimer.After(0.6, delegate
      {
        this.deck.Children.Remove(slot.Holder);
        ArcDockView.Slot slot2;
        if (this.slots.TryGetValue(slot.RailId, out slot2) && slot2 == slot)
        {
          this.slots.Remove(slot.RailId);
        }
      });
    }

    // Token: 0x06002245 RID: 8773 RVA: 0x00621368 File Offset: 0x00621368
    public void Layout(LayoutReason reason, bool animated = true)
    {
      ArcScreenMetrics? metrics = this.dock.Metrics;
      if (metrics != null)
      {
        ArcScreenMetrics valueOrDefault = metrics.GetValueOrDefault();
        this.SetRaster(TopArcLayout.FrontCardSize(valueOrDefault.PanelWidth, valueOrDefault.PanelHeight).Item1 / 494.0);
        List<DockWindowKey> list = this.LiveWindows();
        int count = list.Count;
        bool isOpenNow = this.dock.IsOpenNow;
        int num = DeckFocus.Clamped(this.dock.FocusIndex, count);
        int? num3;
        if (isOpenNow)
        {
          string key = this.dock.HoveredKey;
          if (key != null)
          {
            int num2 = list.FindIndex((DockWindowKey w) => w.Key == key);
            num3 = ((num2 >= 0) ? new int?(num2) : null);
            goto IL_00D2;
          }
        }
        num3 = null;
        IL_00D2:
        int? num4 = num3;
        bool flag = !isOpenNow && this.dock.ClosesInstantly;
        Dictionary<string, ArcRect> dictionary = new Dictionary<string, ArcRect>();
        ArcRect arcRect = default(ArcRect);
        ArcRect? arcRect2 = null;
        for (int i = 0; i < count; i++)
        {
          ArcDockView.Slot slot = this.slots[list[i].RailId];
          ArcPlacement arcPlacement = TopArcLayout.Placement(i, num, num4, valueOrDefault, isOpenNow);
          ArcPlacement arcPlacement2 = TopArcLayout.Placement(i, num, num4, valueOrDefault, true);
          int num5 = i;
          int? num6 = num4;
          bool flag2 = (num5 == num6.GetValueOrDefault()) & (num6 != null);
          bool flag3 = i == num;
          if (arcPlacement2.Opacity > 0.0)
          {
            arcRect = arcRect.Union(arcPlacement2.Frame);
            dictionary[slot.Key] = (flag2 ? arcPlacement2.Frame : TopArcLayout.VisibleFrame(i, num, num4, valueOrDefault));
            if (flag2)
            {
              arcRect2 = new ArcRect?(arcPlacement2.Frame);
            }
          }
          slot.Card.SetPresentation(!flag3 && !flag2, flag2 && flag3, reason > LayoutReason.Open);
          slot.Card.IsHitTestVisible = flag2 && flag3;
          Canvas.SetZIndex(slot.Holder, flag2 ? 100 : ((int)(arcPlacement.Z * 10.0)));
          slot.Target = new DeckPose?(new DeckPose(arcPlacement.CenterX, arcPlacement.CenterY, arcPlacement.Scale, arcPlacement.Degrees, arcPlacement.Opacity));
        }
        int num7;
        if (num4 != null)
        {
          int valueOrDefault2 = num4.GetValueOrDefault();
          if (valueOrDefault2 != num)
          {
            num7 = valueOrDefault2;
            goto IL_0278;
          }
        }
        num7 = -1;
        IL_0278:
        this.PlaceBringToFront(num7, num, num4, valueOrDefault);
        this.dock.FramesChanged(arcRect, dictionary, arcRect2, this.LayoutMore(valueOrDefault, count, num, num4, isOpenNow));
        this.LayoutBackdrop(valueOrDefault, isOpenNow, !animated, flag);
        if (!animated)
        {
          this.pendingJump = true;
        }
        this.QueueMotion(reason);
        this.KeepZOrder();
        return;
      }
    }

    // Token: 0x06002246 RID: 8774 RVA: 0x0062163C File Offset: 0x0062163C
    private List<DockWindowKey> LiveWindows()
    {
      return this.items.Where<DockWindowKey>(delegate(DockWindowKey w)
      {
        ArcDockView.Slot slot;
        return this.slots.TryGetValue(w.RailId, out slot) && !slot.Leaving;
      }).ToList<DockWindowKey>();
    }

    // Token: 0x06002247 RID: 8775 RVA: 0x0062165C File Offset: 0x0062165C
    private void SetRaster(double scale)
    {
      if (scale <= 0.0 || Math.Abs(scale - this.raster) < 1E-09)
      {
        return;
      }
      this.raster = scale;
      foreach (ArcDockView.Slot slot in this.slots.Values)
      {
        slot.Motion.SetRaster(scale);
      }
    }

    // Token: 0x06002248 RID: 8776 RVA: 0x006216E4 File Offset: 0x006216E4
    private void QueueMotion(LayoutReason reason)
    {
      LayoutReason? layoutReason = this.pendingReason;
      if (layoutReason != null)
      {
        LayoutReason valueOrDefault = layoutReason.GetValueOrDefault();
        if (ArcDockView.Rank(reason) <= ArcDockView.Rank(valueOrDefault))
        {
          goto IL_0032;
        }
      }
      this.pendingReason = new LayoutReason?(reason);
      IL_0032:
      if (this.motionQueued)
      {
        return;
      }
      this.motionQueued = true;
      this.dq.TryEnqueue(DispatcherQueuePriority.High, new DispatcherQueueHandler(this.MoveWindows));
    }

    // Token: 0x06002249 RID: 8777 RVA: 0x00621750 File Offset: 0x00621750
    private static int Rank(LayoutReason reason)
    {
      switch (reason)
      {
      case LayoutReason.Open:
        return 4;
      case LayoutReason.Focus:
        return 3;
      case LayoutReason.Metrics:
        return 1;
      case LayoutReason.Items:
        return 2;
      }
      return 0;
    }

    // Token: 0x0600224A RID: 8778 RVA: 0x0062178C File Offset: 0x0062178C
    private static SwiftSpring SpringFor(LayoutReason reason)
    {
      SwiftSpring swiftSpring;
      switch (reason)
      {
      case LayoutReason.Open:
        swiftSpring = new SwiftSpring(0.42, 0.84);
        break;
      case LayoutReason.Focus:
        swiftSpring = new SwiftSpring(0.36, 0.86);
        break;
      case LayoutReason.Hover:
        swiftSpring = new SwiftSpring(0.34, 0.84);
        break;
      case LayoutReason.Metrics:
        swiftSpring = new SwiftSpring(0.42, 0.86);
        break;
      default:
        swiftSpring = new SwiftSpring(0.38, 0.86);
        break;
      }
      return swiftSpring;
    }

    // Token: 0x0600224B RID: 8779 RVA: 0x00621834 File Offset: 0x00621834
    private void MoveWindows()
    {
      this.motionQueued = false;
      LayoutReason? layoutReason = this.pendingReason;
      if (layoutReason != null)
      {
        LayoutReason valueOrDefault = layoutReason.GetValueOrDefault();
        this.pendingReason = null;
        bool flag = this.pendingJump;
        this.pendingJump = false;
        bool isOpenNow = this.dock.IsOpenNow;
        bool flag2 = flag || (!isOpenNow && this.dock.ClosesInstantly) || Anim.ReduceMotion || Anim.Instant;
        SwiftSpring swiftSpring = ArcDockView.SpringFor(valueOrDefault);
        List<DockWindowKey> list = this.LiveWindows();
        int num = DeckFocus.Clamped(this.dock.FocusIndex, list.Count);
        for (int i = 0; i < list.Count; i++)
        {
          ArcDockView.Slot slot = this.slots[list[i].RailId];
          DeckPose? deckPose = slot.Target;
          if (deckPose != null)
          {
            DeckPose valueOrDefault2 = deckPose.GetValueOrDefault();
            if (slot.IsNew)
            {
              slot.IsNew = false;
              PoseMotion motion = slot.Motion;
              DeckPose deckPose2 = valueOrDefault2;
              deckPose2.Opacity = 0.0;
              motion.Jump(deckPose2);
              if (flag2)
              {
                slot.Motion.Jump(valueOrDefault2);
              }
              else
              {
                slot.Motion.SpringTo(valueOrDefault2, new SwiftSpring(0.38, 0.86), 0.0);
              }
            }
            else if (flag2)
            {
              slot.Motion.Jump(valueOrDefault2);
            }
            else
            {
              if (valueOrDefault == LayoutReason.Open && isOpenNow && !slot.Motion.IsMoving && slot.Motion.Target.Opacity == 0.0)
              {
                ArcScreenMetrics? metrics = this.dock.Metrics;
                if (metrics != null)
                {
                  ArcScreenMetrics valueOrDefault3 = metrics.GetValueOrDefault();
                  ArcPlacement arcPlacement = TopArcLayout.Placement(i, num, null, valueOrDefault3, false);
                  slot.Motion.Jump(new DeckPose(arcPlacement.CenterX, arcPlacement.CenterY, arcPlacement.Scale, arcPlacement.Degrees, arcPlacement.Opacity));
                }
              }
              double num2 = ((valueOrDefault == LayoutReason.Open && isOpenNow) ? ((double)Math.Min(Math.Abs(i - num), 3) * 0.04) : 0.0);
              slot.Motion.SpringTo(valueOrDefault2, swiftSpring, num2);
            }
          }
        }
        SwiftSpring swiftSpring2 = new SwiftSpring(0.36, 0.86);
        foreach (ArcDockView.MorePill morePill in this.more)
        {
          DeckPose? deckPose = morePill.Target;
          if (deckPose != null)
          {
            DeckPose valueOrDefault4 = deckPose.GetValueOrDefault();
            if (!morePill.Motion.HasPose)
            {
              PoseMotion motion2 = morePill.Motion;
              DeckPose deckPose2 = valueOrDefault4;
              deckPose2.Opacity = 0.0;
              motion2.Jump(deckPose2);
            }
            if (flag2)
            {
              morePill.Motion.Jump(valueOrDefault4);
            }
            else
            {
              morePill.Motion.SpringTo(valueOrDefault4, swiftSpring2, 0.0);
            }
          }
        }
        return;
      }
    }

    // Token: 0x0600224C RID: 8780 RVA: 0x00621B60 File Offset: 0x00621B60
    private void PlaceBringToFront(int index, int focus, int? hovered, ArcScreenMetrics arc)
    {
      this.bringToFrontIndex = index;
      if (index < 0)
      {
        this.bringToFront.Visibility = Visibility.Collapsed;
        return;
      }
      ArcRect frame = TopArcLayout.Placement(index, focus, hovered, arc, true).Frame;
      Canvas.SetLeft(this.bringToFront, frame.X);
      Canvas.SetTop(this.bringToFront, frame.Y);
      this.bringToFront.Width = frame.W;
      this.bringToFront.Height = frame.H;
      this.bringToFront.Visibility = Visibility.Visible;
    }

    // Token: 0x0600224D RID: 8781 RVA: 0x00621BF0 File Offset: 0x00621BF0
    private List<ArcRect> LayoutMore(ArcScreenMetrics arc, int count, int focus, int? hovered, bool isOpen)
    {
      List<ArcRect> list = new List<ArcRect>();
      int num = 2;
      int num2 = Math.Max(0, focus - num);
      int num3 = Math.Max(0, count - 1 - (focus + num));
      for (int i = 0; i < 2; i++)
      {
        ArcDockView.MorePill morePill = this.more[i];
        int num4 = ((i == 0) ? num2 : num3);
        int num5 = ((i == 0) ? (-1) : 1);
        if (num4 == 0)
        {
          morePill.Shows = false;
          morePill.Host.IsHitTestVisible = false;
          DeckPose? target = morePill.Target;
          if (target != null)
          {
            DeckPose valueOrDefault = target.GetValueOrDefault();
            ArcDockView.MorePill morePill2 = morePill;
            DeckPose deckPose = valueOrDefault;
            deckPose.Opacity = 0.0;
            morePill2.Target = new DeckPose?(deckPose);
          }
        }
        else
        {
          morePill.Reveal = focus + num5;
          if (morePill.Button == null || morePill.Count != num4)
          {
            int index = i;
            morePill.Count = num4;
            morePill.Button = new DeckMoreButton(num4, num5 < 0, (num5 < 0) ? "Show the windows on the left" : "Show the windows on the right", delegate
            {
              this.dock.ShowDeckWindow(this.more[index].Reveal, "more_pill");
            });
            morePill.Host.Children.Clear();
            morePill.Host.Children.Add(morePill.Button);
          }
          ValueTuple<double, double> valueTuple = TopArcLayout.MorePillCenter(TopArcLayout.Placement(focus + num5 * num, focus, hovered, arc, isOpen), num5, arc.PanelWidth);
          morePill.Button.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
          double num6 = Math.Max(60.0, morePill.Button.DesiredSize.Width);
          double num7 = 26.0;
          DeckPose deckPose2 = new DeckPose(valueTuple.Item1 - num6 / 2.0, valueTuple.Item2 - num7 / 2.0, 1.0, 0.0, isOpen > false);
          if (!morePill.Shows && morePill.Motion.HasPose)
          {
            PoseMotion motion = morePill.Motion;
            DeckPose deckPose = deckPose2;
            deckPose.Opacity = morePill.Motion.PoseAt(PoseMotion.Now).Opacity;
            motion.Jump(deckPose);
          }
          morePill.Shows = true;
          morePill.Target = new DeckPose?(deckPose2);
          morePill.Host.IsHitTestVisible = isOpen;
          if (isOpen)
          {
            list.Add(new ArcRect(deckPose2.X, deckPose2.Y, num6, num7));
          }
        }
      }
      return list;
    }

    // Token: 0x0600224E RID: 8782 RVA: 0x00621E98 File Offset: 0x00621E98
    private void LayoutBackdrop(ArcScreenMetrics arc, bool shows, bool instant, bool quick = false)
    {
      Compositor compositor = Ui.Compositor;
      double panelWidth = arc.PanelWidth;
      double num = TopArcLayout.ArcDepth(arc);
      if (this.ellipse == null)
      {
        ContainerVisual containerVisual = compositor.CreateContainerVisual();
        this.ellipse = compositor.CreateSpriteVisual();
        CompositionRadialGradientBrush compositionRadialGradientBrush = compositor.CreateRadialGradientBrush();
        compositionRadialGradientBrush.EllipseCenter = new Vector2(0.5f, 0.5f);
        compositionRadialGradientBrush.EllipseRadius = new Vector2(0.5f, 0.5f);
        foreach (ValueTuple<float, double> valueTuple in new ValueTuple<float, double>[]
        {
          new ValueTuple<float, double>(0f, 1.0),
          new ValueTuple<float, double>(0.38f, 0.96),
          new ValueTuple<float, double>(0.6f, 0.78),
          new ValueTuple<float, double>(0.8f, 0.42),
          new ValueTuple<float, double>(0.92f, 0.12),
          new ValueTuple<float, double>(1f, 0.0)
        })
        {
          float item = valueTuple.Item1;
          double item2 = valueTuple.Item2;
          compositionRadialGradientBrush.ColorStops.Add(compositor.CreateColorGradientStop(item, DS.Black(item2)));
        }
        this.ellipse.Brush = compositionRadialGradientBrush;
        this.band = compositor.CreateSpriteVisual();
        CompositionLinearGradientBrush compositionLinearGradientBrush = compositor.CreateLinearGradientBrush();
        compositionLinearGradientBrush.StartPoint = new Vector2(0f, 0f);
        compositionLinearGradientBrush.EndPoint = new Vector2(0f, 1f);
        foreach (ValueTuple<float, double> valueTuple2 in new ValueTuple<float, double>[]
        {
          new ValueTuple<float, double>(0f, 1.0),
          new ValueTuple<float, double>(0.45f, 1.0),
          new ValueTuple<float, double>(1f, 0.0)
        })
        {
          float item3 = valueTuple2.Item1;
          double item4 = valueTuple2.Item2;
          compositionLinearGradientBrush.ColorStops.Add(compositor.CreateColorGradientStop(item3, DS.Black(item4)));
        }
        this.band.Brush = compositionLinearGradientBrush;
        containerVisual.Children.InsertAtTop(this.ellipse);
        containerVisual.Children.InsertAtTop(this.band);
        containerVisual.Clip = compositor.CreateInsetClip();
        ElementCompositionPreview.SetElementChildVisual(this.backdrop, containerVisual);
      }
      ElementCompositionPreview.GetElementChildVisual(this.backdrop).Size = new Vector2((float)panelWidth, (float)num);
      this.ellipse.Size = new Vector2((float)(panelWidth * 1.5), (float)(num * 2.0));
      this.ellipse.Offset = new Vector3((float)(panelWidth / 2.0 - panelWidth * 0.75), (float)(-(float)num), 0f);
      this.band.Size = new Vector2((float)panelWidth, (float)(arc.MenuBarHeight + 56.0));
      this.backdrop.Width = panelWidth;
      this.backdrop.Height = num;
      this.backdrop.HorizontalAlignment = HorizontalAlignment.Left;
      this.backdrop.VerticalAlignment = VerticalAlignment.Top;
      bool? flag = this.backdropShows;
      if (((flag.GetValueOrDefault() == shows) & (flag != null)) && !instant)
      {
        return;
      }
      this.backdropShows = new bool?(shows);
      if (instant)
      {
        Anim.Stop(this.backdrop, "Opacity");
        this.backdrop.Opacity = shows > false;
        return;
      }
      if (!shows && quick)
      {
        Anim.Opacity(this.backdrop, 0f, 0.2, Easing.EaseOut(Ui.Compositor), 0.0);
        return;
      }
      Anim.Opacity(this.backdrop, shows > false, shows ? 0.32 : 0.26, Easing.EaseInOut(Ui.Compositor), 0.0);
    }

    // Token: 0x0400106C RID: 4204
    private readonly ArcDock dock;

    // Token: 0x0400106D RID: 4205
    private readonly OverlayWindow backdropWindow;

    // Token: 0x0400106E RID: 4206
    private readonly OverlayWindow deckWindow;

    // Token: 0x0400106F RID: 4207
    private readonly Grid backdrop = new Grid
    {
      IsHitTestVisible = false
    };

    // Token: 0x04001070 RID: 4208
    private readonly Canvas deck = new Canvas();

    // Token: 0x04001071 RID: 4209
    private readonly Dictionary<string, ArcDockView.Slot> slots = new Dictionary<string, ArcDockView.Slot>();

    // Token: 0x04001072 RID: 4210
    private readonly ArcDockView.MorePill[] more;

    // Token: 0x04001073 RID: 4211
    private readonly Pressable bringToFront = new Pressable(true)
    {
      Visibility = Visibility.Collapsed
    };

    // Token: 0x04001074 RID: 4212
    private int bringToFrontIndex = -1;

    // Token: 0x04001075 RID: 4213
    [Nullable(2)]
    private SpriteVisual ellipse;

    // Token: 0x04001076 RID: 4214
    [Nullable(2)]
    private SpriteVisual band;

    // Token: 0x04001077 RID: 4215
    private List<DockWindowKey> items = new List<DockWindowKey>();

    // Token: 0x04001078 RID: 4216
    private bool showing;

    // Token: 0x04001079 RID: 4217
    private Displays.Monitor monitor;

    // Token: 0x0400107A RID: 4218
    private double raster = 1.0;

    // Token: 0x0400107B RID: 4219
    private LayoutReason? pendingReason;

    // Token: 0x0400107C RID: 4220
    private bool motionQueued;

    // Token: 0x0400107D RID: 4221
    private bool pendingJump;

    // Token: 0x0400107E RID: 4222
    private readonly DispatcherQueue dq;

    // Token: 0x04001080 RID: 4224
    private static IntPtr aboveDeck;

    // Token: 0x04001082 RID: 4226
    private bool? backdropShows;

    // Token: 0x0200095E RID: 2398
    [Nullable(0)]
    private sealed class Slot
    {
      // Token: 0x06003D29 RID: 15657 RVA: 0x006997AC File Offset: 0x006997AC
      public Slot(string railId, string key, ArcCardView card, Grid holder, PoseMotion motion)
      {
      }

      // Token: 0x17000858 RID: 2136
      // (get) Token: 0x06003D2A RID: 15658 RVA: 0x006997E0 File Offset: 0x006997E0
      public string RailId { get; } = railId;

      // Token: 0x17000859 RID: 2137
      // (get) Token: 0x06003D2B RID: 15659 RVA: 0x006997E8 File Offset: 0x006997E8
      // (set) Token: 0x06003D2C RID: 15660 RVA: 0x006997F0 File Offset: 0x006997F0
      public string Key { get; set; } = key;

      // Token: 0x1700085A RID: 2138
      // (get) Token: 0x06003D2D RID: 15661 RVA: 0x006997FC File Offset: 0x006997FC
      public ArcCardView Card { get; } = card;

      // Token: 0x1700085B RID: 2139
      // (get) Token: 0x06003D2E RID: 15662 RVA: 0x00699804 File Offset: 0x00699804
      public Grid Holder { get; } = holder;

      // Token: 0x1700085C RID: 2140
      // (get) Token: 0x06003D2F RID: 15663 RVA: 0x0069980C File Offset: 0x0069980C
      public PoseMotion Motion { get; } = motion;

      // Token: 0x04002764 RID: 10084
      public bool Leaving;

      // Token: 0x04002765 RID: 10085
      public bool IsNew = true;

      // Token: 0x04002766 RID: 10086
      public DeckPose? Target;
    }

    // Token: 0x0200095F RID: 2399
    [Nullable(0)]
    private sealed class MorePill
    {
      // Token: 0x06003D30 RID: 15664 RVA: 0x00699814 File Offset: 0x00699814
      public MorePill()
      {
        this.Motion = new PoseMotion(this.Host, null, Vector2.Zero);
      }

      // Token: 0x04002767 RID: 10087
      public readonly Grid Host = new Grid
      {
        Opacity = 0.0,
        IsHitTestVisible = false
      };

      // Token: 0x04002768 RID: 10088
      public readonly PoseMotion Motion;

      // Token: 0x04002769 RID: 10089
      [Nullable(2)]
      public DeckMoreButton Button;

      // Token: 0x0400276A RID: 10090
      public int Count;

      // Token: 0x0400276B RID: 10091
      public int Reveal;

      // Token: 0x0400276C RID: 10092
      public DeckPose? Target;

      // Token: 0x0400276D RID: 10093
      public bool Shows;
    }
  }
}
