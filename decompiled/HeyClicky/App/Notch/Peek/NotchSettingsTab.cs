using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Settings;
using HeyClicky.Core.Notch;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000194 RID: 404
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class NotchSettingsTab : Grid
  {
    // Token: 0x060015E5 RID: 5605 RVA: 0x005B8CB4 File Offset: 0x005B8CB4
    [NullableContext(1)]
    public NotchSettingsTab(INotchHost host)
    {
      this.host = host;
      this.presenter = host.Services.Presenter as NotchPresenter;
      this.content = new SettingsContent(host.Services, host.Actions, null);
      this.content.UseNotchGrid(12.0, 12.0);
      this.content.Margin = new Thickness(0.0, 6.0, 12.0, 0.0);
      base.Children.Add(this.content);
      base.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(delegate(object _, PointerRoutedEventArgs _)
      {
        this.TakeKeyboard();
      }), true);
      this.content.PageChanged += delegate(SettingsWindow.Page page)
      {
        if (this.syncing || this.presenter == null)
        {
          return;
        }
        NotchSettingsSection notchSettingsSection = NotchSettingsTab.ToSection(page);
        if (this.presenter.Tabs.SettingsSection != notchSettingsSection)
        {
          this.presenter.Tabs.SettingsSection = notchSettingsSection;
        }
      };
      this.Attached();
    }

    // Token: 0x060015E6 RID: 5606 RVA: 0x005B8D94 File Offset: 0x005B8D94
    public void Attached()
    {
      if (this.listening)
      {
        return;
      }
      this.listening = true;
      this.host.KeyboardChanged += this.OnKeyboardChanged;
    }

    // Token: 0x170003F4 RID: 1012
    // (get) Token: 0x060015E7 RID: 5607 RVA: 0x005B8DC0 File Offset: 0x005B8DC0
    public bool HoldsKeyboard
    {
      get
      {
        return this.holdsKeyboard;
      }
    }

    // Token: 0x170003F5 RID: 1013
    // (get) Token: 0x060015E8 RID: 5608 RVA: 0x005B8DC8 File Offset: 0x005B8DC8
    // (set) Token: 0x060015E9 RID: 5609 RVA: 0x005B8DD0 File Offset: 0x005B8DD0
    public static SettingsContent ShownContent { get; private set; }

    // Token: 0x060015EA RID: 5610 RVA: 0x005B8DD8 File Offset: 0x005B8DD8
    public void Shown()
    {
      NotchSettingsTab.<>c__DisplayClass16_0 CS$<>8__locals1 = new NotchSettingsTab.<>c__DisplayClass16_0();
      CS$<>8__locals1.<>4__this = this;
      NotchSettingsTab.ShownContent = this.content;
      if (this.presenter == null)
      {
        return;
      }
      NotchSettingsSection settingsSection = this.presenter.Tabs.SettingsSection;
      this.content.IsSurfaceSettled = false;
      NotchSettingsTab.<>c__DisplayClass16_0 CS$<>8__locals2 = CS$<>8__locals1;
      int num = this.settleGeneration + 1;
      this.settleGeneration = num;
      CS$<>8__locals2.generation = num;
      DispatcherQueueTimer dispatcherQueueTimer = base.DispatcherQueue.CreateTimer().Rooted();
      dispatcherQueueTimer.Interval = NotchSettingsTab.SettleDelay;
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        if (CS$<>8__locals1.generation == CS$<>8__locals1.<>4__this.settleGeneration)
        {
          CS$<>8__locals1.<>4__this.content.IsSurfaceSettled = true;
        }
      };
      dispatcherQueueTimer.Start();
      this.syncing = true;
      this.content.Show(new SettingsWindow.Page?(NotchSettingsTab.ToPage(settingsSection)));
      this.syncing = false;
      NotchPeekSurface.PeekAction("settings_" + settingsSection.RawValue(), null);
      if (this.host.Services.Auth.IsSignedIn)
      {
        this.content.RefreshReferralIfStale(30.0);
      }
      this.presenter.Tabs.Changed -= this.OnTabsChanged;
      this.presenter.Tabs.Changed += this.OnTabsChanged;
      NotchSettingsTab.IntegrationCallbackArrived -= this.DeliverIntegrationCallback;
      NotchSettingsTab.IntegrationCallbackArrived += this.DeliverIntegrationCallback;
      this.DeliverIntegrationCallback();
    }

    // Token: 0x14000061 RID: 97
    // (add) Token: 0x060015EB RID: 5611 RVA: 0x005B8F38 File Offset: 0x005B8F38
    // (remove) Token: 0x060015EC RID: 5612 RVA: 0x005B8F6C File Offset: 0x005B8F6C
    private static event Action IntegrationCallbackArrived;

    // Token: 0x060015ED RID: 5613 RVA: 0x005B8FA0 File Offset: 0x005B8FA0
    public static void NoteIntegrationCallback(string toolkit)
    {
      NotchSettingsTab.integrationCallback = new ValueTuple<bool, string>(true, toolkit);
      Action integrationCallbackArrived = NotchSettingsTab.IntegrationCallbackArrived;
      if (integrationCallbackArrived == null)
      {
        return;
      }
      integrationCallbackArrived();
    }

    // Token: 0x060015EE RID: 5614 RVA: 0x005B8FC0 File Offset: 0x005B8FC0
    private void DeliverIntegrationCallback()
    {
      if (!NotchSettingsTab.integrationCallback.Item1)
      {
        return;
      }
      string item = NotchSettingsTab.integrationCallback.Item2;
      NotchSettingsTab.integrationCallback = default(ValueTuple<bool, string>);
      this.content.OnIntegrationCallback(item);
    }

    // Token: 0x060015EF RID: 5615 RVA: 0x005B8FFC File Offset: 0x005B8FFC
    public void Hidden()
    {
      this.content.Hidden();
      NotchSettingsTab.IntegrationCallbackArrived -= this.DeliverIntegrationCallback;
      if (this.presenter != null)
      {
        this.presenter.Tabs.Changed -= this.OnTabsChanged;
      }
      this.ReleaseKeyboard();
    }

    // Token: 0x060015F0 RID: 5616 RVA: 0x005B9050 File Offset: 0x005B9050
    public void Detached()
    {
      if (NotchSettingsTab.ShownContent == this.content)
      {
        NotchSettingsTab.ShownContent = null;
      }
      this.Hidden();
      if (!this.listening)
      {
        return;
      }
      this.listening = false;
      this.host.KeyboardChanged -= this.OnKeyboardChanged;
    }

    // Token: 0x060015F1 RID: 5617 RVA: 0x005B90A4 File Offset: 0x005B90A4
    private void OnTabsChanged()
    {
      base.DispatcherQueue.TryEnqueue(delegate
      {
        if (this.presenter == null || this.presenter.Tabs.Selected != NotchTab.Settings)
        {
          return;
        }
        SettingsWindow.Page page = NotchSettingsTab.ToPage(this.presenter.Tabs.SettingsSection);
        if (this.content.CurrentPage == page)
        {
          return;
        }
        this.syncing = true;
        this.content.Show(new SettingsWindow.Page?(page));
        this.syncing = false;
      });
    }

    // Token: 0x060015F2 RID: 5618 RVA: 0x005B90C0 File Offset: 0x005B90C0
    private void TakeKeyboard()
    {
      if (this.holdsKeyboard)
      {
        return;
      }
      this.holdsKeyboard = true;
      NotchPresenter notchPresenter = this.presenter;
      if (notchPresenter != null)
      {
        notchPresenter.SetNotchAgentPeekHeldOpen(true);
      }
      this.host.RequestKeyboard();
    }

    // Token: 0x060015F3 RID: 5619 RVA: 0x005B90F0 File Offset: 0x005B90F0
    private void ReleaseKeyboard()
    {
      if (!this.holdsKeyboard)
      {
        return;
      }
      this.holdsKeyboard = false;
      NotchPresenter notchPresenter = this.presenter;
      if (notchPresenter != null)
      {
        notchPresenter.SetNotchAgentPeekHeldOpen(false);
      }
      this.host.ReleaseKeyboardIfUnheld();
    }

    // Token: 0x060015F4 RID: 5620 RVA: 0x005B9120 File Offset: 0x005B9120
    private void OnKeyboardChanged(bool hasKeyboard)
    {
      if (hasKeyboard || !this.holdsKeyboard)
      {
        return;
      }
      this.holdsKeyboard = false;
      NotchPresenter notchPresenter = this.presenter;
      if (notchPresenter == null)
      {
        return;
      }
      notchPresenter.SetNotchAgentPeekHeldOpen(false);
    }

    // Token: 0x060015F5 RID: 5621 RVA: 0x005B9148 File Offset: 0x005B9148
    public static SettingsWindow.Page ToPage(NotchSettingsSection section)
    {
      return Enum.Parse<SettingsWindow.Page>(section.ToString());
    }

    // Token: 0x060015F6 RID: 5622 RVA: 0x005B915C File Offset: 0x005B915C
    public static NotchSettingsSection ToSection(SettingsWindow.Page page)
    {
      return Enum.Parse<NotchSettingsSection>(page.ToString());
    }

    // Token: 0x04000AA1 RID: 2721
    [Nullable(1)]
    private readonly INotchHost host;

    // Token: 0x04000AA2 RID: 2722
    private readonly NotchPresenter presenter;

    // Token: 0x04000AA3 RID: 2723
    [Nullable(1)]
    private readonly SettingsContent content;

    // Token: 0x04000AA4 RID: 2724
    private bool holdsKeyboard;

    // Token: 0x04000AA5 RID: 2725
    private bool syncing;

    // Token: 0x04000AA6 RID: 2726
    private bool listening;

    // Token: 0x04000AA7 RID: 2727
    private int settleGeneration;

    // Token: 0x04000AA8 RID: 2728
    private static readonly TimeSpan SettleDelay = TimeSpan.FromMilliseconds(360L, 0L);

    // Token: 0x04000AAA RID: 2730
    [TupleElementNames(new string[] { "Pending", "Toolkit" })]
    [Nullable(new byte[] { 0, 2 })]
    private static ValueTuple<bool, string> integrationCallback;
  }
}
