using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Billing
{
  // Token: 0x02000368 RID: 872
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PaywallWindow : OverlayWindow
  {
    // Token: 0x170006CF RID: 1743
    // (get) Token: 0x0600263E RID: 9790 RVA: 0x0063FF34 File Offset: 0x0063FF34
    // (set) Token: 0x0600263F RID: 9791 RVA: 0x0063FF3C File Offset: 0x0063FF3C
    public bool IsShowing { get; private set; }

    // Token: 0x06002640 RID: 9792 RVA: 0x0063FF48 File Offset: 0x0063FF48
    public PaywallWindow(bool excludeFromCapture)
      : base("HeyClicky", excludeFromCapture, false, false, true, false, false)
    {
      this.stage = new Grid
      {
        Margin = new Thickness(32.0),
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top
      };
      this.shadow = new ShadowLayer(18.0, DS.Black(0.45), 16.0, 10.0, 0.0);
      this.chrome = new Border
      {
        CornerRadius = new CornerRadius(18.0),
        BorderThickness = new Thickness(0.5),
        Padding = new Thickness(28.0, 26.0, 28.0, 26.0)
      };
      this.scroller = new ScrollViewer
      {
        HorizontalScrollMode = ScrollMode.Disabled,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
        VerticalScrollMode = ScrollMode.Enabled,
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
        IsTabStop = false
      };
      this.stage.Children.Add(this.shadow);
      this.stage.Children.Add(this.chrome);
      base.Root.Children.Add(this.stage);
      base.MoveByBackground(this.chrome);
    }

    // Token: 0x170006D0 RID: 1744
    // (get) Token: 0x06002641 RID: 9793 RVA: 0x006400B0 File Offset: 0x006400B0
    [Nullable(2)]
    public UIElement Card
    {
      [NullableContext(2)]
      get
      {
        return this.card;
      }
    }

    // Token: 0x06002642 RID: 9794 RVA: 0x006400B8 File Offset: 0x006400B8
    public void Present(PaywallCard card, double width, PaywallPalette palette)
    {
      this.generation++;
      this.requestedCardWidth = width - 16.0;
      this.chrome.Background = Ui.Brush(palette.CardSurface);
      this.chrome.BorderBrush = Ui.Brush(palette.CardBorder);
      base.Root.RequestedTheme = (palette.IsDark ? ElementTheme.Dark : ElementTheme.Light);
      this.card = card;
      this.scroller.Content = null;
      this.chrome.Child = card;
      this.monitor = Displays.UnderCursor();
      this.Fit();
      bool isShowing = this.IsShowing;
      this.IsShowing = true;
      Anim.Stop(base.Root, "Opacity");
      base.Root.Opacity = 1.0;
      base.ShowNoActivate();
      Vector3 vector = new Vector3((float)(this.cardWidth / 2.0), (float)(this.MeasuredHeight() / 2.0), 0f);
      SwiftSpring swiftSpring = new SwiftSpring(0.35, 0.82);
      bool flag = !isShowing || this.stage.Opacity < 1.0;
      if (flag)
      {
        this.stage.CenterPoint = vector;
      }
      if (flag && !Anim.Instant)
      {
        this.stage.Opacity = 0.0;
        Anim.ScaleUpFrom(this.stage, 0.94f, swiftSpring, new Vector3?(vector));
      }
      else
      {
        Anim.ScaleSpring(this.stage, 1f, swiftSpring, new Vector3?(vector));
      }
      Anim.OpacitySpring(this.stage, 1f, swiftSpring);
    }

    // Token: 0x06002643 RID: 9795 RVA: 0x00640260 File Offset: 0x00640260
    private double MeasuredHeight()
    {
      this.chrome.Height = double.NaN;
      this.chrome.Measure(new Size(this.cardWidth, double.PositiveInfinity));
      return Math.Ceiling(this.chrome.DesiredSize.Height);
    }

    // Token: 0x06002644 RID: 9796 RVA: 0x006402B8 File Offset: 0x006402B8
    public void Fit()
    {
      double num = (double)this.monitor.Work.Width / this.monitor.Scale;
      double num2 = (double)this.monitor.Work.Height / this.monitor.Scale;
      this.cardWidth = PaywallFit.CardWidth(this.requestedCardWidth, num, 32.0);
      this.chrome.Width = this.cardWidth;
      this.shadow.Width = this.cardWidth;
      double num3 = this.MeasuredHeight();
      double num4 = PaywallFit.MaxCardHeight(num2, 32.0);
      this.SetScrolling(num3 > num4);
      double num5 = Math.Min(num3, num4);
      this.shadow.Height = num5;
      this.chrome.Height = num5;
      double num6 = this.cardWidth + 64.0;
      double num7 = num5 + 64.0;
      int num8 = (int)Math.Round(num6 * this.monitor.Scale);
      int num9 = (int)Math.Round(num7 * this.monitor.Scale);
      ValueTuple<int, int> valueTuple = PaywallFit.Origin(this.monitor.Bounds.Left, this.monitor.Bounds.Top, this.monitor.Bounds.Width, this.monitor.Bounds.Height, this.monitor.Work.Left, this.monitor.Work.Top, this.monitor.Work.Right, this.monitor.Work.Bottom, num8, num9);
      int item = valueTuple.Item1;
      int item2 = valueTuple.Item2;
      base.PlacePx(item, item2, num8, num9);
    }

    // Token: 0x06002645 RID: 9797 RVA: 0x0064047C File Offset: 0x0064047C
    private void SetScrolling(bool scrolls)
    {
      if (this.card == null || (scrolls ? (this.chrome.Child == this.scroller) : (this.chrome.Child == this.card)))
      {
        return;
      }
      this.chrome.Child = null;
      this.scroller.Content = null;
      if (scrolls)
      {
        this.scroller.Content = this.card;
        this.chrome.Child = this.scroller;
        return;
      }
      this.chrome.Child = this.card;
    }

    // Token: 0x06002646 RID: 9798 RVA: 0x00640514 File Offset: 0x00640514
    public void Dismiss()
    {
      PaywallWindow.<>c__DisplayClass21_0 CS$<>8__locals1 = new PaywallWindow.<>c__DisplayClass21_0();
      CS$<>8__locals1.<>4__this = this;
      if (!this.IsShowing)
      {
        return;
      }
      this.IsShowing = false;
      PaywallWindow.<>c__DisplayClass21_0 CS$<>8__locals2 = CS$<>8__locals1;
      int num = this.generation + 1;
      this.generation = num;
      CS$<>8__locals2.dismissGeneration = num;
      CubicBezierEasingFunction cubicBezierEasingFunction = Easing.EaseOut(Ui.Compositor);
      Anim.Opacity(this.stage, 0f, 0.4, cubicBezierEasingFunction, 0.0);
      Anim.Scale(this.stage, 0.96f, 0.4, cubicBezierEasingFunction, new Vector3?(new Vector3((float)(this.cardWidth / 2.0), (float)(this.chrome.ActualHeight / 2.0), 0f)));
      Anim.Opacity(base.Root, 0f, 0.45, cubicBezierEasingFunction, 0.0);
      DispatcherQueueTimer dispatcherQueueTimer = base.DispatcherQueue.CreateTimer().Rooted();
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(Anim.Instant ? 0.0 : 0.48);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        if (CS$<>8__locals1.<>4__this.generation != CS$<>8__locals1.dismissGeneration || CS$<>8__locals1.<>4__this.IsShowing)
        {
          return;
        }
        CS$<>8__locals1.<>4__this.chrome.Child = null;
        CS$<>8__locals1.<>4__this.scroller.Content = null;
        CS$<>8__locals1.<>4__this.card = null;
        CS$<>8__locals1.<>4__this.HideWindow();
        Anim.Stop(CS$<>8__locals1.<>4__this.Root, "Opacity");
        CS$<>8__locals1.<>4__this.Root.Opacity = 1.0;
      };
      dispatcherQueueTimer.Start();
    }

    // Token: 0x04001213 RID: 4627
    private const double ShadowMargin = 32.0;

    // Token: 0x04001214 RID: 4628
    private readonly Grid stage;

    // Token: 0x04001215 RID: 4629
    private readonly Border chrome;

    // Token: 0x04001216 RID: 4630
    private readonly ShadowLayer shadow;

    // Token: 0x04001217 RID: 4631
    private readonly ScrollViewer scroller;

    // Token: 0x04001218 RID: 4632
    [Nullable(2)]
    private UIElement card;

    // Token: 0x04001219 RID: 4633
    private double requestedCardWidth;

    // Token: 0x0400121A RID: 4634
    private double cardWidth;

    // Token: 0x0400121B RID: 4635
    private Displays.Monitor monitor;

    // Token: 0x0400121C RID: 4636
    private int generation;
  }
}
