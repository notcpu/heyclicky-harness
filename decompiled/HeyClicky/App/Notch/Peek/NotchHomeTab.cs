using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.Billing;
using HeyClicky.App.Design;
using HeyClicky.App.Services;
using HeyClicky.App.UI;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Telemetry;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200018E RID: 398
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchHomeTab : Grid
  {
    // Token: 0x170003EA RID: 1002
    // (get) Token: 0x060015A4 RID: 5540 RVA: 0x005B6068 File Offset: 0x005B6068
    public Grid DockSlot { get; } = new Grid
    {
      Tag = "home-dock-slot"
    };

    // Token: 0x060015A5 RID: 5541 RVA: 0x005B6070 File Offset: 0x005B6070
    public NotchHomeTab(INotchHost host)
    {
      this.host = host;
      base.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(58.0)
      });
      base.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      base.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(44.0)
      });
      this.greeting = Ui.Text("", 19.0, FontWeights.SemiBold, DS.White(0.95), TextAlignment.Left, 0, false, false);
      this.greeting.HorizontalAlignment = HorizontalAlignment.Center;
      this.status = Ui.Text("", 12.0, FontWeights.Medium, DS.White(0.55), TextAlignment.Left, 1, false, false);
      this.status.HorizontalAlignment = HorizontalAlignment.Center;
      this.status.TextTrimming = TextTrimming.CharacterEllipsis;
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 4.0,
        VerticalAlignment = VerticalAlignment.Center,
        Children = { this.greeting, this.status }
      };
      base.Children.Add(stackPanel);
      Grid.SetRow(this.DockSlot, 1);
      base.Children.Add(this.DockSlot);
      this.footer.Margin = new Thickness(12.0, 0.0, 12.0, 0.0);
      this.footer.VerticalAlignment = VerticalAlignment.Top;
      this.footer.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      this.footer.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      this.footer.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      Grid.SetRow(this.footer, 2);
      base.Children.Add(this.footer);
      base.RegisterPropertyChangedCallback(UIElement.IsHitTestVisibleProperty, delegate(DependencyObject _, DependencyProperty _)
      {
        if (!base.IsHitTestVisible)
        {
          NotchUsagePill notchUsagePill = this.usagePill;
          if (notchUsagePill == null)
          {
            return;
          }
          notchUsagePill.HideCard();
        }
      });
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        this.Refresh();
        this.BuildFooter();
        if (!this.listening)
        {
          this.listening = true;
          ConnectedApps.Changed += this.OnAppsChanged;
          HeaderPills.Changed += this.OnAppsChanged;
          NotchExtensions.ShowcaseChanged += this.OnAppsChanged;
          INotchHomeDock homeDock = NotchExtensions.HomeDock;
          if (homeDock != null)
          {
            homeDock.FrontCardSizeChanged += this.OnDockChanged;
          }
        }
        this.DockSlot.Children.Clear();
        INotchHomeDock homeDock2 = NotchExtensions.HomeDock;
        FrameworkElement frameworkElement = ((homeDock2 != null) ? homeDock2.CreateHomeSlot() : null);
        if (frameworkElement != null)
        {
          this.DockSlot.Children.Add(frameworkElement);
        }
      };
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        if (base.IsLoaded || !this.listening)
        {
          return;
        }
        this.listening = false;
        ConnectedApps.Changed -= this.OnAppsChanged;
        HeaderPills.Changed -= this.OnAppsChanged;
        NotchExtensions.ShowcaseChanged -= this.OnAppsChanged;
        INotchHomeDock homeDock3 = NotchExtensions.HomeDock;
        if (homeDock3 != null)
        {
          homeDock3.FrontCardSizeChanged -= this.OnDockChanged;
        }
      };
    }

    // Token: 0x060015A6 RID: 5542 RVA: 0x005B6318 File Offset: 0x005B6318
    private void OnAppsChanged()
    {
      base.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.BuildFooter));
    }

    // Token: 0x060015A7 RID: 5543 RVA: 0x005B6334 File Offset: 0x005B6334
    private void OnDockChanged()
    {
      base.DispatcherQueue.TryEnqueue(delegate
      {
        this.Refresh();
        this.host.Relayout();
      });
    }

    // Token: 0x060015A8 RID: 5544 RVA: 0x005B6350 File Offset: 0x005B6350
    public void Refresh()
    {
      this.greeting.Text = NotchGreeting.Greeting(this.host.Services.Auth.User);
      TextBlock textBlock = this.status;
      INotchHomeDock homeDock = NotchExtensions.HomeDock;
      string text = ((homeDock != null) ? homeDock.StatusLine : null);
      textBlock.Text = ((text != null && text.Length > 0) ? text : ("Hold " + this.host.Services.Settings.PushToTalkDisplay + " and tell me what to take off your plate."));
    }

    // Token: 0x060015A9 RID: 5545 RVA: 0x005B63D4 File Offset: 0x005B63D4
    private void BuildFooter()
    {
      IReadOnlyList<string> showcaseSampleIntegrationIds = NotchExtensions.ShowcaseSampleIntegrationIds;
      IEnumerable<string> enumerable2;
      if (showcaseSampleIntegrationIds.Count != 0)
      {
        IEnumerable<string> enumerable = showcaseSampleIntegrationIds;
        enumerable2 = enumerable;
      }
      else
      {
        enumerable2 = from a in ConnectedApps.Connected()
          select a.Id + "=" + a.LogoUrl;
      }
      IEnumerable<string> enumerable3 = enumerable2;
      BillingUi billing = this.host.Services.Billing;
      BillingStateService billingStateService = ((billing != null) ? billing.State : null);
      BillingUsageRing billingUsageRing = BillingUsageRing.From((billingStateService != null) ? billingStateService.Plan : null, (billingStateService != null) ? billingStateService.UsageResetsPhrase : null);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 5);
      defaultInterpolatedStringHandler.AppendFormatted<bool>(HeaderPills.HidesPromotionalButtons());
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(NotchPreferences.HasClickedHomeReferralButton);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(string.Join(",", enumerable3));
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted((billingUsageRing != null) ? billingUsageRing.AccessibilityValue : null);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted((billingUsageRing != null) ? billingUsageRing.ResetsLine : null);
      string text = defaultInterpolatedStringHandler.ToStringAndClear();
      if (text == this.footerLook && this.footer.Children.Count > 0)
      {
        return;
      }
      this.footerLook = text;
      NotchUsagePill notchUsagePill = this.usagePill;
      if (notchUsagePill != null)
      {
        notchUsagePill.HideCard();
      }
      this.usagePill = null;
      this.footer.Children.Clear();
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 8.0
      };
      if (!HeaderPills.HidesPromotionalButtons())
      {
        stackPanel.Children.Add(this.ReferralButton());
      }
      if (billingUsageRing != null)
      {
        stackPanel.Children.Add(this.usagePill = new NotchUsagePill(this.host, billingUsageRing));
      }
      this.footer.Children.Add(stackPanel);
      FrameworkElement frameworkElement = this.AppsPill();
      Grid.SetColumn(frameworkElement, 2);
      this.footer.Children.Add(frameworkElement);
    }

    // Token: 0x060015AA RID: 5546 RVA: 0x005B65DC File Offset: 0x005B65DC
    private FrameworkElement ReferralButton()
    {
      NotchHomeTab.<>c__DisplayClass15_0 CS$<>8__locals1 = new NotchHomeTab.<>c__DisplayClass15_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.highlighted = !NotchPreferences.HasClickedHomeReferralButton;
      CS$<>8__locals1.p = new Pressable(true)
      {
        Height = 32.0,
        CornerRadius = new CornerRadius(100.0),
        Tag = "notch-referral"
      };
      CS$<>8__locals1.gift = Icons.PathIcon("M2.5,8.2 h8.6 v3.8 h-8.6 z M12.9,8.2 h8.6 v3.8 h-8.6 z M4,13.4 h7.1 v8.1 h-5.6 a1.5,1.5 0 0 1 -1.5,-1.5 z M12.9,13.4 h7.1 v6.6 a1.5,1.5 0 0 1 -1.5,1.5 h-5.6 z M12,7.6 C10.6,4.6 7.8,2.8 6.6,4.2 C5.6,5.4 6.6,7.4 12,7.6 z M12,7.6 C13.4,4.6 16.2,2.8 17.4,4.2 C18.4,5.4 17.4,7.4 12,7.6 z", 10.0, CS$<>8__locals1.<ReferralButton>g__Ink|0(false));
      CS$<>8__locals1.label = Ui.Text("Invite & earn", 11.0, FontWeights.SemiBold, CS$<>8__locals1.<ReferralButton>g__Ink|0(false), TextAlignment.Left, 1, false, false);
      CS$<>8__locals1.label.VerticalAlignment = VerticalAlignment.Center;
      StackPanel stackPanel = Ui.H(5.0, new UIElement[] { CS$<>8__locals1.gift, CS$<>8__locals1.label });
      stackPanel.VerticalAlignment = VerticalAlignment.Center;
      stackPanel.Margin = new Thickness(10.0, 0.0, 10.0, 0.0);
      if (CS$<>8__locals1.highlighted)
      {
        GelSurface gel = new GelSurface(GelAppearance.Mint, 100.0);
        CS$<>8__locals1.p.Children.Add(gel);
        CS$<>8__locals1.p.HoverChanged += delegate(bool h)
        {
          gel.Hovered = h;
        };
      }
      else
      {
        CS$<>8__locals1.p.Background = Ui.Brush(DS.White(0.08));
        CS$<>8__locals1.p.HoverChanged += delegate(bool h)
        {
          CS$<>8__locals1.p.Background = Ui.Brush(DS.White(h ? 0.14 : 0.08));
          CS$<>8__locals1.gift.Fill = Ui.Brush(base.<ReferralButton>g__Ink|0(h));
          CS$<>8__locals1.label.Foreground = Ui.Brush(base.<ReferralButton>g__Ink|0(h));
        };
      }
      CS$<>8__locals1.p.Children.Add(stackPanel);
      AutomationProperties.SetName(CS$<>8__locals1.p, "Invite & Earn");
      CS$<>8__locals1.p.Click += delegate
      {
        NotchPreferences.HasClickedHomeReferralButton = true;
        ClickyAnalytics.NoteReferralPageEntryPoint("notch");
        NotchPresenter notchPresenter = CS$<>8__locals1.<>4__this.host.Services.Presenter as NotchPresenter;
        if (notchPresenter == null)
        {
          return;
        }
        notchPresenter.Tabs.ShowSettings(NotchSettingsSection.Referral);
      };
      return CS$<>8__locals1.p;
    }

    // Token: 0x060015AB RID: 5547 RVA: 0x005B67B8 File Offset: 0x005B67B8
    private FrameworkElement AppsPill()
    {
      IReadOnlyList<string> showcaseSampleIntegrationIds = NotchExtensions.ShowcaseSampleIntegrationIds;
      List<AppEntry> list;
      if (showcaseSampleIntegrationIds.Count != 0)
      {
        list = showcaseSampleIntegrationIds.Select<string, AppEntry>((string id) => ConnectedApps.All().FirstOrDefault<AppEntry>((AppEntry a) => a.Id == id)).OfType<AppEntry>().ToList<AppEntry>();
      }
      else
      {
        list = ConnectedApps.Connected().ToList<AppEntry>();
      }
      List<AppEntry> list2 = list;
      Pressable p = new Pressable(true)
      {
        Height = 32.0,
        CornerRadius = new CornerRadius(100.0),
        Background = Ui.Brush(DS.White(0.08)),
        Tag = "notch-apps"
      };
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 4.0,
        VerticalAlignment = VerticalAlignment.Center,
        Margin = new Thickness(10.0, 0.0, 10.0, 0.0)
      };
      if (list2.Count == 0)
      {
        TextBlock textBlock = Icons.Glyph("\ue71d", 10.0, DS.White(0.7));
        textBlock.FontWeight = FontWeights.SemiBold;
        stackPanel.Children.Add(textBlock);
      }
      else
      {
        foreach (AppEntry appEntry in list2.Take<AppEntry>(3))
        {
          FrameworkElement frameworkElement = IntegrationLogos.Artwork(appEntry.ToolkitSlug, appEntry.DisplayName, 15.0, 9.0, appEntry.LogoUrl);
          frameworkElement.Tag = "app:" + appEntry.Id;
          stackPanel.Children.Add(frameworkElement);
        }
        if (list2.Count > 3)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
          defaultInterpolatedStringHandler.AppendLiteral("+");
          defaultInterpolatedStringHandler.AppendFormatted<int>(list2.Count - 3);
          TextBlock textBlock2 = Ui.Text(defaultInterpolatedStringHandler.ToStringAndClear(), 9.0, FontWeights.Bold, DS.White(0.6), TextAlignment.Left, 0, false, false);
          textBlock2.VerticalAlignment = VerticalAlignment.Center;
          stackPanel.Children.Add(textBlock2);
        }
      }
      TextBlock appsLabel = Ui.Text("Apps", 11.0, FontWeights.SemiBold, DS.White(0.7), TextAlignment.Left, 1, false, false);
      appsLabel.VerticalAlignment = VerticalAlignment.Center;
      if (!list2.Any<AppEntry>((AppEntry a) => !a.IsLocal))
      {
        stackPanel.Children.Add(appsLabel);
      }
      TextBlock textBlock3 = Icons.Glyph("\ue710", 8.5, DS.White(0.6));
      textBlock3.FontWeight = FontWeights.Bold;
      stackPanel.Children.Add(textBlock3);
      p.Children.Add(stackPanel);
      p.HoverChanged += delegate(bool h)
      {
        p.Background = Ui.Brush(DS.White(h ? 0.14 : 0.08));
        appsLabel.Foreground = Ui.Brush(DS.White(h ? 0.95 : 0.7));
      };
      p.Tip((list2.Count == 0) ? "Connect apps" : "Connected apps");
      AutomationProperties.SetName(p, "Apps");
      p.Click += delegate
      {
        NotchPeekSurface.PeekAction("open_integrations", null);
        NotchPresenter notchPresenter = this.host.Services.Presenter as NotchPresenter;
        if (notchPresenter == null)
        {
          return;
        }
        notchPresenter.Tabs.ShowSettings(NotchSettingsSection.Integrations);
      };
      return p;
    }

    // Token: 0x04000A4E RID: 2638
    private readonly INotchHost host;

    // Token: 0x04000A4F RID: 2639
    private readonly TextBlock greeting;

    // Token: 0x04000A50 RID: 2640
    private readonly TextBlock status;

    // Token: 0x04000A51 RID: 2641
    private readonly Grid footer = new Grid
    {
      ColumnSpacing = 8.0
    };

    // Token: 0x04000A53 RID: 2643
    private bool listening;

    // Token: 0x04000A54 RID: 2644
    [Nullable(2)]
    private NotchUsagePill usagePill;

    // Token: 0x04000A55 RID: 2645
    [Nullable(2)]
    private string footerLook;
  }
}
