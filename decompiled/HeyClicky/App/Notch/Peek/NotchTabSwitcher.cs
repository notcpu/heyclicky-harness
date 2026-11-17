using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000192 RID: 402
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchTabSwitcher : Grid
  {
    // Token: 0x170003F3 RID: 1011
    // (set) Token: 0x060015DB RID: 5595 RVA: 0x005B84FC File Offset: 0x005B84FC
    public Grid PlanPillHost
    {
      set
      {
        this.planSlot.Children.Clear();
        this.planSlot.Children.Add(value);
      }
    }

    // Token: 0x060015DC RID: 5596 RVA: 0x005B8520 File Offset: 0x005B8520
    public NotchTabSwitcher()
    {
      base.ColumnSpacing = 6.0;
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      FrameworkElement frameworkElement = this.Group(new NotchTab[]
      {
        NotchTab.Home,
        NotchTab.Chat
      });
      FrameworkElement frameworkElement2 = this.Group(new NotchTab[] { NotchTab.Settings });
      Grid.SetColumn(this.planSlot, 2);
      Grid.SetColumn(frameworkElement2, 3);
      base.Children.Add(frameworkElement);
      base.Children.Add(this.planSlot);
      base.Children.Add(frameworkElement2);
      Grid.SetColumnSpan(this.capsuleLayer, 4);
      this.capsuleLayer.Children.Add(this.capsule);
      base.Children.Insert(0, this.capsuleLayer);
      ElementCompositionPreview.SetIsTranslationEnabled(this.capsule, true);
      base.LayoutUpdated += delegate([Nullable(2)] object _, object _)
      {
        this.PlaceCapsule(false, true);
      };
    }

    // Token: 0x060015DD RID: 5597 RVA: 0x005B8720 File Offset: 0x005B8720
    public void Bind(INotchHost host, [Nullable(2)] NotchTabs tabs)
    {
      this.host = host;
      this.tabs = tabs;
      this.Select((tabs != null) ? tabs.Selected : NotchTab.Home, false);
    }

    // Token: 0x060015DE RID: 5598 RVA: 0x005B8744 File Offset: 0x005B8744
    private FrameworkElement Group(params NotchTab[] shown)
    {
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 2.0,
        Padding = new Thickness(2.0),
        VerticalAlignment = VerticalAlignment.Center
      };
      foreach (NotchTab notchTab in shown)
      {
        stackPanel.Children.Add(this.Button(notchTab));
      }
      return new Border
      {
        CornerRadius = new CornerRadius(14.0),
        Background = Ui.Brush(DS.White(0.05)),
        Child = stackPanel,
        VerticalAlignment = VerticalAlignment.Center
      };
    }

    // Token: 0x060015DF RID: 5599 RVA: 0x005B87F0 File Offset: 0x005B87F0
    private Pressable Button(NotchTab tab)
    {
      Pressable pressable = new Pressable(true)
      {
        Width = 34.0,
        Height = 24.0
      };
      FrameworkElement frameworkElement;
      if (tab == NotchTab.Home)
      {
        this.mark = new BrandMark(16.0, DS.White(0.5), 1.35)
        {
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Center
        };
        frameworkElement = this.mark;
      }
      else
      {
        TextBlock textBlock = Icons.Glyph((tab == NotchTab.Chat) ? "\ue8f2" : "\ue713", 11.0, DS.White(0.5));
        textBlock.FontWeight = FontWeights.SemiBold;
        frameworkElement = textBlock;
      }
      pressable.Children.Add(frameworkElement);
      this.icons[tab] = frameworkElement;
      this.buttons[tab] = pressable;
      pressable.HoverChanged += delegate(bool _)
      {
        this.Tint(tab);
      };
      pressable.Tip(tab.Title());
      AutomationProperties.SetName(pressable, tab.Title());
      pressable.Click += delegate
      {
        if (tab == NotchTab.Home)
        {
          BrandMark brandMark = this.mark;
          if (brandMark != null)
          {
            brandMark.BlinkOnce();
          }
        }
        if (this.tabs == null || this.tabs.Selected == tab)
        {
          return;
        }
        if (tab == NotchTab.Settings)
        {
          this.tabs.SettingsSection = NotchSettingsSection.Account;
        }
        if (tab == NotchTab.Chat)
        {
          this.tabs.DropExpandedReply();
        }
        INotchHost notchHost = this.host;
        NotchPresenter notchPresenter = ((notchHost != null) ? notchHost.Services.Presenter : null) as NotchPresenter;
        if (notchPresenter != null)
        {
          notchPresenter.NoteTabPickedInsideOpenNotch();
        }
        this.tabs.Select(tab);
      };
      return pressable;
    }

    // Token: 0x060015E0 RID: 5600 RVA: 0x005B8938 File Offset: 0x005B8938
    private void Tint(NotchTab tab)
    {
      Pressable pressable = this.buttons[tab];
      NotchTab? notchTab = this.selected;
      Color color = DS.White(((notchTab.GetValueOrDefault() == tab) & (notchTab != null)) ? 0.92 : (pressable.IsHovered ? 0.75 : 0.5));
      FrameworkElement frameworkElement = this.icons[tab];
      BrandMark brandMark = frameworkElement as BrandMark;
      if (brandMark == null)
      {
        TextBlock textBlock = frameworkElement as TextBlock;
        if (textBlock != null)
        {
          textBlock.Foreground = Ui.Brush(color);
        }
      }
      else
      {
        brandMark.SetStroke(color);
      }
      DependencyObject dependencyObject = pressable;
      notchTab = this.selected;
      AutomationProperties.SetItemStatus(dependencyObject, ((notchTab.GetValueOrDefault() == tab) & (notchTab != null)) ? "Selected" : "");
    }

    // Token: 0x060015E1 RID: 5601 RVA: 0x005B8A0C File Offset: 0x005B8A0C
    public void Select(NotchTab tab, bool animated)
    {
      NotchTab? notchTab = this.selected;
      this.selected = new NotchTab?(tab);
      foreach (NotchTab notchTab2 in this.buttons.Keys)
      {
        this.Tint(notchTab2);
      }
      NotchTab? notchTab3 = notchTab;
      if (!((notchTab3.GetValueOrDefault() == tab) & (notchTab3 != null)))
      {
        this.PlaceCapsule(animated && notchTab != null, false);
      }
    }

    // Token: 0x060015E2 RID: 5602 RVA: 0x005B8AA8 File Offset: 0x005B8AA8
    private void PlaceCapsule(bool animated, bool onlyIfMoved)
    {
      NotchTab? notchTab = this.selected;
      if (notchTab != null)
      {
        NotchTab valueOrDefault = notchTab.GetValueOrDefault();
        Pressable pressable;
        if (this.buttons.TryGetValue(valueOrDefault, out pressable) && pressable.XamlRoot != null && pressable.ActualWidth > 0.0)
        {
          this.capsule.Opacity = 1.0;
          Point point = pressable.TransformToVisual(this.capsuleLayer).TransformPoint(new Point(0f, 0f));
          Vector3 vector = new Vector3((float)point.X, (float)point.Y, 0f);
          if (onlyIfMoved && vector == this.lastCapsule)
          {
            return;
          }
          this.lastCapsule = vector;
          if (animated && !Anim.ReduceMotion)
          {
            Anim.TranslationSpring(this.capsule, vector, new SwiftSpring(0.28, 0.86));
            return;
          }
          this.capsule.Translation = vector;
          return;
        }
      }
      this.capsule.Opacity = (this.buttons.ContainsKey(this.selected.GetValueOrDefault(NotchTab.Document)) ? this.capsule.Opacity : 0.0);
    }

    // Token: 0x04000A97 RID: 2711
    private readonly Dictionary<NotchTab, Pressable> buttons = new Dictionary<NotchTab, Pressable>();

    // Token: 0x04000A98 RID: 2712
    private readonly Dictionary<NotchTab, FrameworkElement> icons = new Dictionary<NotchTab, FrameworkElement>();

    // Token: 0x04000A99 RID: 2713
    private readonly Border capsule = new Border
    {
      Width = 34.0,
      Height = 24.0,
      CornerRadius = new CornerRadius(12.0),
      Background = Ui.Brush(DS.White(0.14)),
      IsHitTestVisible = false,
      HorizontalAlignment = HorizontalAlignment.Left,
      VerticalAlignment = VerticalAlignment.Top
    };

    // Token: 0x04000A9A RID: 2714
    private readonly Canvas capsuleLayer = new Canvas
    {
      IsHitTestVisible = false
    };

    // Token: 0x04000A9B RID: 2715
    private readonly Grid planSlot = new Grid
    {
      VerticalAlignment = VerticalAlignment.Center
    };

    // Token: 0x04000A9C RID: 2716
    [Nullable(2)]
    private INotchHost host;

    // Token: 0x04000A9D RID: 2717
    [Nullable(2)]
    private NotchTabs tabs;

    // Token: 0x04000A9E RID: 2718
    private NotchTab? selected;

    // Token: 0x04000A9F RID: 2719
    [Nullable(2)]
    private BrandMark mark;

    // Token: 0x04000AA0 RID: 2720
    private Vector3 lastCapsule = new Vector3(float.NaN);
  }
}
