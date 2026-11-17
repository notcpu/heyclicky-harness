using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using HeyClicky.App.Billing;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.Services;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Telemetry;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000191 RID: 401
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchPeekSurface : NotchSurface
  {
    // Token: 0x170003EF RID: 1007
    // (get) Token: 0x060015BA RID: 5562 RVA: 0x005B7028 File Offset: 0x005B7028
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x060015BB RID: 5563 RVA: 0x005B7030 File Offset: 0x005B7030
    public NotchPeekSurface()
    {
      this.view.Margin = new Thickness(0.0);
      this.background = new Grid
      {
        Margin = new Thickness(-18.0, -6.0, -18.0, -6.0),
        IsHitTestVisible = false,
        Opacity = 0.0
      };
      this.background.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(30.0)
      });
      this.background.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(56.0)
      });
      this.background.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      Border border = new Border
      {
        Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, Colors.Black),
          new ValueTuple<double, Color>(1.0, HomeTheme.PaneFill)
        })
      };
      Border border2 = new Border
      {
        Background = Ui.Brush(HomeTheme.PaneFill)
      };
      Grid.SetRow(border, 1);
      Grid.SetRow(border2, 2);
      this.background.Children.Add(new Border
      {
        Background = Ui.Brush(Colors.Black)
      });
      this.background.Children.Add(border);
      this.background.Children.Add(border2);
      this.view.Children.Add(this.background);
      this.body.Margin = new Thickness(0.0, 32.0, 0.0, 0.0);
      this.view.Children.Add(this.body);
      this.header.VerticalAlignment = VerticalAlignment.Top;
      this.header.Margin = new Thickness(12.0, -2.0, 12.0, 0.0);
      this.switcher = new NotchTabSwitcher();
      this.switcher.PlanPillHost = this.planPillHost;
      this.header.Children.Add(this.switcher);
      this.view.Children.Add(this.header);
      this.view.Children.Add(this.overlayLayer);
      this.pillLayer.Margin = new Thickness(0.0, 4.0, 0.0, 0.0);
      this.view.Children.Add(this.pillLayer);
    }

    // Token: 0x060015BC RID: 5564 RVA: 0x005B73A8 File Offset: 0x005B73A8
    public override bool CanUpdate(NotchPresentation p)
    {
      NotchPresentation.LiveEvent liveEvent = p as NotchPresentation.LiveEvent;
      return liveEvent != null && liveEvent.Event is NotchLiveEvent.AgentPeek;
    }

    // Token: 0x060015BD RID: 5565 RVA: 0x005B73D0 File Offset: 0x005B73D0
    public override void Update(NotchPresentation p)
    {
      this.Sync();
    }

    // Token: 0x170003F0 RID: 1008
    // (get) Token: 0x060015BE RID: 5566 RVA: 0x005B73D8 File Offset: 0x005B73D8
    public override bool WantsKeyboard
    {
      get
      {
        return false;
      }
    }

    // Token: 0x170003F1 RID: 1009
    // (get) Token: 0x060015BF RID: 5567 RVA: 0x005B73DC File Offset: 0x005B73DC
    public override VerticalAlignment ContentAlignment
    {
      get
      {
        return VerticalAlignment.Stretch;
      }
    }

    // Token: 0x170003F2 RID: 1010
    // (get) Token: 0x060015C0 RID: 5568 RVA: 0x005B73E0 File Offset: 0x005B73E0
    public override bool HoldsKeyboard
    {
      get
      {
        NotchChatView notchChatView = this.chat;
        if (notchChatView == null || !notchChatView.ComposerHoldsKeyboard)
        {
          NotchSettingsTab notchSettingsTab = this.settings;
          if (notchSettingsTab == null || !notchSettingsTab.HoldsKeyboard)
          {
            NotchPresenter notchPresenter = this.presenter;
            return notchPresenter != null && notchPresenter.NotchChatComposerHasKeyboard;
          }
        }
        return true;
      }
    }

    // Token: 0x060015C1 RID: 5569 RVA: 0x005B7420 File Offset: 0x005B7420
    public override bool AcceptFiles(IReadOnlyList<string> paths)
    {
      bool flag = this.presenter == null || this.tabs == null;
      if (!flag)
      {
        NotchTab selected = this.tabs.Selected;
        bool flag2 = selected - NotchTab.Settings <= 1;
        flag = flag2;
      }
      if (flag)
      {
        return false;
      }
      string text = this.presenter.DropFilesOnNotchChat(paths);
      ClickyAnalytics.TrackNotchFilesDropped(paths.Count, text);
      return true;
    }

    // Token: 0x060015C2 RID: 5570 RVA: 0x005B7480 File Offset: 0x005B7480
    public override void SetDropTargeting(bool on)
    {
      NotchChatView notchChatView = this.chat;
      if (notchChatView == null)
      {
        return;
      }
      notchChatView.SetDropTargeting(on);
    }

    // Token: 0x060015C3 RID: 5571 RVA: 0x005B7494 File Offset: 0x005B7494
    protected override void OnAttached()
    {
      this.attached = true;
      this.presenter = base.Host.Services.Presenter as NotchPresenter;
      NotchPresenter notchPresenter = this.presenter;
      this.tabs = ((notchPresenter != null) ? notchPresenter.Tabs : null);
      this.switcher.Bind(base.Host, this.tabs);
      this.BuildPlanPill();
      HeaderPills.Changed += this.OnPillsChanged;
      if (this.tabs != null)
      {
        this.tabs.Changed += this.OnTabsChanged;
        this.tabs.SelectionChanged += this.OnSelectionChanged;
      }
      if (this.presenter != null)
      {
        this.presenter.VoicePillChanged += this.OnVoicePillChanged;
      }
      NotchExtensions.ShowcaseChanged += this.OnShowcaseChanged;
      NotchChatSize.Changed += this.OnChatSizeChanged;
      if (base.Host.WindowHandle != 0)
      {
        Displays.Monitor monitor = Displays.ForWindow(base.Host.WindowHandle);
        NotchChatSize.NoteScreenHeight((double)monitor.Bounds.Height / monitor.Scale);
      }
      ConversationHistory history = NotchChat.History;
      if (history != null)
      {
        history.Refresh();
      }
      ChatHandOffs handOffs = NotchChat.HandOffs;
      if (handOffs != null)
      {
        handOffs.Load();
      }
      BillingUi billing = base.Host.Services.Billing;
      BillingStateService billingStateService = ((billing != null) ? billing.State : null);
      if (billingStateService != null && billingStateService.AuthenticatedUserId != null)
      {
        billingStateService.RefreshIfStaleAsync(null);
      }
      NotchSettingsTab notchSettingsTab = this.settings;
      if (notchSettingsTab != null)
      {
        notchSettingsTab.Attached();
      }
      this.snapTabs = !base.StillShowing;
      this.Sync();
      this.snapTabs = false;
      this.OnVoicePillChanged();
      this.OnShowcaseChanged();
      this.openedAt = new long?(Environment.TickCount64);
      NotchTabs notchTabs = this.tabs;
      this.openedOnTab = ((notchTabs != null) ? notchTabs.Selected : NotchTab.Home).RawValue();
    }

    // Token: 0x060015C4 RID: 5572 RVA: 0x005B767C File Offset: 0x005B767C
    public override void Detached()
    {
      if (!this.attached)
      {
        return;
      }
      this.attached = false;
      HeaderPills.Changed -= this.OnPillsChanged;
      if (this.tabs != null)
      {
        this.tabs.Changed -= this.OnTabsChanged;
        this.tabs.SelectionChanged -= this.OnSelectionChanged;
      }
      if (this.presenter != null)
      {
        this.presenter.VoicePillChanged -= this.OnVoicePillChanged;
      }
      NotchExtensions.ShowcaseChanged -= this.OnShowcaseChanged;
      NotchChatSize.Changed -= this.OnChatSizeChanged;
      NotchChatView notchChatView = this.chat;
      if (notchChatView != null)
      {
        notchChatView.Detached();
      }
      NotchSettingsTab notchSettingsTab = this.settings;
      if (notchSettingsTab != null)
      {
        notchSettingsTab.Detached();
      }
      NotchPresenter notchPresenter = this.presenter;
      if (notchPresenter != null)
      {
        notchPresenter.SetNotchAgentPeekHeldOpen(false);
      }
      long? num = this.openedAt;
      if (num != null)
      {
        long valueOrDefault = num.GetValueOrDefault();
        double num2 = (double)(Environment.TickCount64 - valueOrDefault) / 1000.0;
        if (num2 >= 3.0)
        {
          string text = "opened_" + this.openedOnTab;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["open_seconds"] = Math.Round(num2 * 10.0, MidpointRounding.AwayFromZero) / 10.0;
          NotchPeekSurface.PeekAction(text, jsonObject);
        }
      }
      this.openedAt = null;
      this.shownTab = (NotchTab)(-1);
      this.visibleTab = null;
    }

    // Token: 0x060015C5 RID: 5573 RVA: 0x005B77F4 File Offset: 0x005B77F4
    private void OnPillsChanged()
    {
      this.view.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.BuildPlanPill));
    }

    // Token: 0x060015C6 RID: 5574 RVA: 0x005B7814 File Offset: 0x005B7814
    internal static Pressable ExpandToWindowButton(Action expand)
    {
      Pressable p = new Pressable(true)
      {
        Width = 32.0,
        Height = 32.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      GelSurface gel = new GelSurface(GelAppearance.SecondaryOutline, 16.0);
      TextBlock glyph = Icons.Glyph("\ue740", 11.0, DS.White(0.75));
      glyph.FontWeight = FontWeights.SemiBold;
      glyph.HorizontalAlignment = HorizontalAlignment.Center;
      p.Children.Add(gel);
      p.Children.Add(glyph);
      p.RenderTransformOrigin = new Point(0.5, 0.5);
      p.RenderTransform = new ScaleTransform();
      p.HoverChanged += delegate(bool h)
      {
        gel.Hovered = h;
        glyph.Foreground = Ui.Brush(DS.White(h ? 0.95 : 0.75));
        ((ScaleTransform)p.RenderTransform).ScaleX = (((ScaleTransform)p.RenderTransform).ScaleY = (h ? 1.05 : 1.0));
      };
      p.Click += delegate
      {
        NotchPeekSurface.PeekAction("expand_to_window", null);
        expand();
      };
      p.Tip("Open in a window");
      AutomationProperties.SetName(p, "Open in a window");
      return p;
    }

    // Token: 0x060015C7 RID: 5575 RVA: 0x005B796C File Offset: 0x005B796C
    private void OnChatSizeChanged()
    {
      base.Host.Relayout();
    }

    // Token: 0x060015C8 RID: 5576 RVA: 0x005B797C File Offset: 0x005B797C
    [NullableContext(2)]
    private void SyncResizeGrips(NotchTab selected, string expandedReplyId)
    {
      bool flag = selected == NotchTab.Chat && expandedReplyId == null;
      if (flag && this.resizeGrips == null)
      {
        this.resizeGrips = new NotchChatResizeGrips(base.Host)
        {
          Margin = new Thickness(-18.0, -6.0, -18.0, -6.0)
        };
        this.view.Children.Insert(this.view.Children.IndexOf(this.pillLayer), this.resizeGrips);
      }
      if (this.resizeGrips != null)
      {
        this.resizeGrips.Visibility = (flag ? Visibility.Visible : Visibility.Collapsed);
      }
    }

    // Token: 0x060015C9 RID: 5577 RVA: 0x005B7A28 File Offset: 0x005B7A28
    private void OnTabsChanged()
    {
      this.view.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.Sync));
    }

    // Token: 0x060015CA RID: 5578 RVA: 0x005B7A48 File Offset: 0x005B7A48
    private void OnSelectionChanged(NotchTab _)
    {
      this.view.DispatcherQueue.TryEnqueue(delegate
      {
        this.Sync();
        base.Host.Relayout();
      });
    }

    // Token: 0x060015CB RID: 5579 RVA: 0x005B7A68 File Offset: 0x005B7A68
    private void OnShowcaseChanged()
    {
      this.view.DispatcherQueue.TryEnqueue(delegate
      {
        UIElement uielement = this.view;
        NotchPresenter notchPresenter = this.presenter;
        uielement.IsHitTestVisible = notchPresenter == null || !notchPresenter.IsTourShowcaseShowing;
      });
    }

    // Token: 0x060015CC RID: 5580 RVA: 0x005B7A88 File Offset: 0x005B7A88
    private void BuildPlanPill()
    {
      AppServices services = base.Host.Services;
      string text = (HeaderPills.ShowsUpdate(services) ? "update" : (HeaderPills.ShowsUpgrade(services) ? "upgrade" : "none"));
      if (text == this.planPillKind)
      {
        return;
      }
      this.planPillKind = text;
      this.planPillHost.Children.Clear();
      if (HeaderPills.ShowsUpdate(services))
      {
        this.planPillHost.Children.Add(HeaderPills.UpdateButton(services, "notch_peek", true, false, new Action(this.<BuildPlanPill>g__Fold|49_0)));
        return;
      }
      if (HeaderPills.ShowsUpgrade(services))
      {
        this.planPillHost.Children.Add(HeaderPills.UpgradeButton(services, PaywallSource.NotchPeekUpgradeButton, true, false, new Action(this.<BuildPlanPill>g__Fold|49_0)));
      }
    }

    // Token: 0x060015CD RID: 5581 RVA: 0x005B7B4C File Offset: 0x005B7B4C
    private void Sync()
    {
      if (this.tabs == null)
      {
        return;
      }
      NotchTab selected = this.tabs.Selected;
      bool flag = selected == NotchTab.Document;
      NotchTab notchTab = (flag ? this.tabs.TabBeforeDocument : selected);
      if (notchTab == NotchTab.Document)
      {
        notchTab = NotchTab.Home;
      }
      bool flag2 = selected - NotchTab.Chat <= 2;
      bool flag3 = flag2;
      Anim.Opacity(this.background, flag3 > false, 0.18, Easing.EaseOut(Ui.Compositor), 0.0);
      FrameworkElement frameworkElement2;
      if (notchTab != NotchTab.Chat)
      {
        if (notchTab != NotchTab.Settings)
        {
          FrameworkElement frameworkElement;
          if ((frameworkElement = this.homeTab) == null)
          {
            frameworkElement = (this.homeTab = new NotchHomeTab(base.Host));
          }
          frameworkElement2 = frameworkElement;
        }
        else
        {
          FrameworkElement frameworkElement3;
          if ((frameworkElement3 = this.settingsTab) == null)
          {
            frameworkElement3 = (this.settingsTab = this.BuildSettings());
          }
          frameworkElement2 = frameworkElement3;
        }
      }
      else
      {
        FrameworkElement frameworkElement4;
        if ((frameworkElement4 = this.chatTab) == null)
        {
          frameworkElement4 = (this.chatTab = this.BuildChat());
        }
        frameworkElement2 = frameworkElement4;
      }
      FrameworkElement frameworkElement5 = frameworkElement2;
      if (selected != this.shownTab)
      {
        FrameworkElement frameworkElement6 = (flag ? null : frameworkElement5);
        foreach (FrameworkElement frameworkElement7 in this.body.Children.OfType<FrameworkElement>().ToList<FrameworkElement>())
        {
          if (!(frameworkElement7 == frameworkElement6) && !(frameworkElement7 == this.documentView))
          {
            if (frameworkElement7 == this.settingsTab && this.visibleTab == this.settingsTab)
            {
              NotchSettingsTab notchSettingsTab = this.settings;
              if (notchSettingsTab != null)
              {
                notchSettingsTab.Hidden();
              }
            }
            if (this.snapTabs)
            {
              NotchPeekSurface.SnapTab(frameworkElement7, false);
            }
            else
            {
              this.FadeOutTab(frameworkElement7);
            }
          }
        }
        if (!this.body.Children.Contains(frameworkElement5))
        {
          this.body.Children.Insert(0, frameworkElement5);
          frameworkElement5.Opacity = 0.0;
          if (flag)
          {
            frameworkElement5.Visibility = Visibility.Collapsed;
          }
        }
        this.visibleTab = frameworkElement6;
        if (frameworkElement6 != null)
        {
          if (this.snapTabs)
          {
            NotchPeekSurface.SnapTab(frameworkElement6, true);
          }
          else
          {
            this.FadeInTab(frameworkElement6);
          }
          if (frameworkElement6 == this.settingsTab)
          {
            NotchSettingsTab notchSettingsTab2 = this.settings;
            if (notchSettingsTab2 != null)
            {
              notchSettingsTab2.Shown();
            }
          }
          if (frameworkElement6 == this.chatTab)
          {
            NotchChatView notchChatView = this.chat;
            if (notchChatView != null)
            {
              notchChatView.Shown();
            }
          }
          if (frameworkElement6 == this.homeTab)
          {
            NotchHomeTab notchHomeTab = this.homeTab as NotchHomeTab;
            if (notchHomeTab != null)
            {
              notchHomeTab.Refresh();
            }
          }
        }
        if (flag)
        {
          if (this.documentView == null)
          {
            this.documentView = new NotchDocumentViewer(base.Host, true);
          }
          if (!this.body.Children.Contains(this.documentView))
          {
            this.body.Children.Add(this.documentView);
          }
          this.documentView.Opacity = 0.0;
          this.documentView.IsHitTestVisible = true;
          Anim.Opacity(this.documentView, 1f, 0.18, Easing.EaseOut(Ui.Compositor), 0.0);
        }
        else if (this.documentView != null && this.body.Children.Contains(this.documentView))
        {
          FrameworkElement leaving = this.documentView;
          leaving.IsHitTestVisible = false;
          if (this.snapTabs)
          {
            this.body.Children.Remove(leaving);
          }
          else
          {
            Anim.Opacity(leaving, 0f, 0.18, Easing.EaseOut(Ui.Compositor), 0.0);
            this.After(0.2, delegate
            {
              NotchTabs notchTabs = this.tabs;
              if (notchTabs == null || notchTabs.Selected != NotchTab.Document)
              {
                this.body.Children.Remove(leaving);
              }
            });
          }
        }
        bool flag4 = this.header.Children.Count > 0 && this.header.Children[0] != this.switcher;
        if (flag && !flag4)
        {
          this.header.Children.Clear();
          this.documentHeader = new NotchDocumentHeaderRow(base.Host);
          this.header.Children.Add(this.documentHeader);
        }
        else if (!flag && (flag4 || this.header.Children.Count == 0))
        {
          this.header.Children.Clear();
          this.header.Children.Add(this.switcher);
        }
        this.shownTab = selected;
        INotchHomeDock homeDock = NotchExtensions.HomeDock;
        if (homeDock != null)
        {
          homeDock.NotchShowsHome(selected == NotchTab.Home);
        }
      }
      if (flag)
      {
        NotchDocumentViewer notchDocumentViewer = this.documentView as NotchDocumentViewer;
        if (notchDocumentViewer != null)
        {
          notchDocumentViewer.Show(this.tabs.DocumentPath, this.tabs.DocumentThreadId);
        }
      }
      this.switcher.Select(selected, true);
      this.SyncFullReply((selected == NotchTab.Chat) ? this.tabs.ExpandedReplyEntryId : null);
      this.SyncResizeGrips(selected, this.tabs.ExpandedReplyEntryId);
    }

    // Token: 0x060015CE RID: 5582 RVA: 0x005B8058 File Offset: 0x005B8058
    private void FadeInTab(FrameworkElement tab)
    {
      bool flag = tab.Visibility == Visibility.Collapsed;
      tab.Visibility = Visibility.Visible;
      tab.IsHitTestVisible = true;
      if (Anim.Instant)
      {
        Anim.Stop(tab, "Opacity");
        tab.Opacity = 1.0;
        return;
      }
      Compositor compositor = Ui.Compositor;
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = compositor.CreateScalarKeyFrameAnimation();
      if (flag)
      {
        scalarKeyFrameAnimation.InsertKeyFrame(0f, 0f);
      }
      scalarKeyFrameAnimation.InsertKeyFrame(1f, 1f, Easing.EaseOut(compositor));
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(Anim.ReduceMotion ? 0.12 : 0.18);
      scalarKeyFrameAnimation.Target = "Opacity";
      tab.StartAnimation(scalarKeyFrameAnimation);
    }

    // Token: 0x060015CF RID: 5583 RVA: 0x005B8110 File Offset: 0x005B8110
    private static void SnapTab(FrameworkElement tab, bool shows)
    {
      tab.Visibility = (shows ? Visibility.Visible : Visibility.Collapsed);
      tab.IsHitTestVisible = shows;
      Anim.Opacity(tab, shows > false, 0.001, null, 0.0);
    }

    // Token: 0x060015D0 RID: 5584 RVA: 0x005B8144 File Offset: 0x005B8144
    private void FadeOutTab(FrameworkElement tab)
    {
      tab.IsHitTestVisible = false;
      if (tab.Visibility == Visibility.Collapsed)
      {
        return;
      }
      Anim.Opacity(tab, 0f, 0.18, Easing.EaseOut(Ui.Compositor), 0.0);
      this.After(0.2, delegate
      {
        if (tab != this.visibleTab)
        {
          tab.Visibility = Visibility.Collapsed;
        }
      });
    }

    // Token: 0x060015D1 RID: 5585 RVA: 0x005B81C8 File Offset: 0x005B81C8
    private void After(double seconds, Action action)
    {
      DispatcherQueueTimer t = this.view.DispatcherQueue.CreateTimer().Rooted();
      t.Interval = TimeSpan.FromSeconds(seconds);
      t.IsRepeating = false;
      t.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        t.Stop();
        action();
      };
      t.Start();
    }

    // Token: 0x060015D2 RID: 5586 RVA: 0x005B823C File Offset: 0x005B823C
    private FrameworkElement BuildChat()
    {
      this.chat = new NotchChatView(base.Host, false);
      return this.chat;
    }

    // Token: 0x060015D3 RID: 5587 RVA: 0x005B8258 File Offset: 0x005B8258
    private FrameworkElement BuildSettings()
    {
      this.settings = new NotchSettingsTab(base.Host);
      return this.settings;
    }

    // Token: 0x060015D4 RID: 5588 RVA: 0x005B8274 File Offset: 0x005B8274
    [NullableContext(2)]
    private void SyncFullReply(string entryId)
    {
      NotchPeekSurface.<>c__DisplayClass58_0 CS$<>8__locals1 = new NotchPeekSurface.<>c__DisplayClass58_0();
      CS$<>8__locals1.<>4__this = this;
      if (entryId != null)
      {
        ChatHandOffs handOffs = NotchChat.HandOffs;
        if (((handOffs != null) ? handOffs.Entry(entryId) : null) == null)
        {
          entryId = null;
        }
      }
      if (entryId == this.shownReplyId)
      {
        return;
      }
      this.shownReplyId = entryId;
      FrameworkElement frameworkElement = this.fullReply;
      if (frameworkElement != null)
      {
        FrameworkElement leaving = frameworkElement;
        Anim.Opacity(leaving, 0f, 0.18, Easing.EaseOut(Ui.Compositor), 0.0);
        DispatcherQueueTimer dispatcherQueueTimer = this.view.DispatcherQueue.CreateTimer().Rooted();
        dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(0.2);
        dispatcherQueueTimer.IsRepeating = false;
        dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
        {
          CS$<>8__locals1.<>4__this.overlayLayer.Children.Remove(leaving);
        };
        dispatcherQueueTimer.Start();
        this.fullReply = null;
      }
      if (entryId == null)
      {
        return;
      }
      CS$<>8__locals1.reply = new NotchFullReply(base.Host, entryId);
      this.fullReply = CS$<>8__locals1.reply;
      this.overlayLayer.Children.Add(CS$<>8__locals1.reply);
      CS$<>8__locals1.reply.Opacity = 0.0;
      CS$<>8__locals1.spring = new SwiftSpring(0.32, 0.88);
      if (!Anim.ReduceMotion)
      {
        CS$<>8__locals1.reply.SizeChanged += CS$<>8__locals1.<SyncFullReply>g__Grow|0;
        return;
      }
      Anim.OpacitySpring(CS$<>8__locals1.reply, 1f, CS$<>8__locals1.spring);
    }

    // Token: 0x060015D5 RID: 5589 RVA: 0x005B83F8 File Offset: 0x005B83F8
    private void OnVoicePillChanged()
    {
      this.view.DispatcherQueue.TryEnqueue(delegate
      {
        NotchPresenter notchPresenter = this.presenter;
        ValueTuple<NotchActivityPhase, string>? valueTuple = ((notchPresenter != null) ? notchPresenter.VoicePill : null);
        NotchPresenter notchPresenter2 = this.presenter;
        if (notchPresenter2 != null && notchPresenter2.IsNotchHeldOpenByArc)
        {
          valueTuple = null;
        }
        NotchVoicePill.Update(this.pillLayer, valueTuple, base.Host);
      });
    }

    // Token: 0x060015D6 RID: 5590 RVA: 0x005B8418 File Offset: 0x005B8418
    internal static void PeekAction(string action, [Nullable(2)] JsonObject extra = null)
    {
      JsonObject jsonObject = extra ?? new JsonObject(null);
      jsonObject["action"] = action;
      ClickyAnalytics.TrackProductEvent("notch_peek_action", null, null, jsonObject);
    }

    // Token: 0x060015D9 RID: 5593 RVA: 0x005B8490 File Offset: 0x005B8490
    [CompilerGenerated]
    private void <BuildPlanPill>g__Fold|49_0()
    {
      NotchPresenter notchPresenter = this.presenter;
      if (notchPresenter == null)
      {
        return;
      }
      notchPresenter.DismissNotchAgentPeek();
    }

    // Token: 0x04000A7B RID: 2683
    private readonly Grid view = new Grid();

    // Token: 0x04000A7C RID: 2684
    private readonly Grid background;

    // Token: 0x04000A7D RID: 2685
    private readonly Grid body = new Grid();

    // Token: 0x04000A7E RID: 2686
    private readonly Grid header = new Grid
    {
      Height = 30.0
    };

    // Token: 0x04000A7F RID: 2687
    private readonly Grid overlayLayer = new Grid();

    // Token: 0x04000A80 RID: 2688
    private readonly Grid pillLayer = new Grid
    {
      IsHitTestVisible = false,
      VerticalAlignment = VerticalAlignment.Top,
      HorizontalAlignment = HorizontalAlignment.Center
    };

    // Token: 0x04000A81 RID: 2689
    private readonly NotchTabSwitcher switcher;

    // Token: 0x04000A82 RID: 2690
    private readonly Grid planPillHost = new Grid
    {
      VerticalAlignment = VerticalAlignment.Center
    };

    // Token: 0x04000A83 RID: 2691
    [Nullable(2)]
    private NotchPresenter presenter;

    // Token: 0x04000A84 RID: 2692
    [Nullable(2)]
    private NotchTabs tabs;

    // Token: 0x04000A85 RID: 2693
    [Nullable(2)]
    private FrameworkElement homeTab;

    // Token: 0x04000A86 RID: 2694
    [Nullable(2)]
    private FrameworkElement chatTab;

    // Token: 0x04000A87 RID: 2695
    [Nullable(2)]
    private FrameworkElement settingsTab;

    // Token: 0x04000A88 RID: 2696
    [Nullable(2)]
    private FrameworkElement documentView;

    // Token: 0x04000A89 RID: 2697
    [Nullable(2)]
    private FrameworkElement documentHeader;

    // Token: 0x04000A8A RID: 2698
    [Nullable(2)]
    private FrameworkElement fullReply;

    // Token: 0x04000A8B RID: 2699
    [Nullable(2)]
    private NotchChatView chat;

    // Token: 0x04000A8C RID: 2700
    [Nullable(2)]
    private NotchChatResizeGrips resizeGrips;

    // Token: 0x04000A8D RID: 2701
    [Nullable(2)]
    private NotchSettingsTab settings;

    // Token: 0x04000A8E RID: 2702
    private NotchTab shownTab = (NotchTab)(-1);

    // Token: 0x04000A8F RID: 2703
    [Nullable(2)]
    private string shownReplyId;

    // Token: 0x04000A90 RID: 2704
    private bool attached;

    // Token: 0x04000A91 RID: 2705
    private bool snapTabs;

    // Token: 0x04000A92 RID: 2706
    private long? openedAt;

    // Token: 0x04000A93 RID: 2707
    private string openedOnTab = "";

    // Token: 0x04000A94 RID: 2708
    private const double MinimumTrackedOpenSeconds = 3.0;

    // Token: 0x04000A95 RID: 2709
    [Nullable(2)]
    private string planPillKind;

    // Token: 0x04000A96 RID: 2710
    [Nullable(2)]
    private FrameworkElement visibleTab;
  }
}
