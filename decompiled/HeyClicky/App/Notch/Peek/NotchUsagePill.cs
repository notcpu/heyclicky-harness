using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Notch;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000195 RID: 405
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchUsagePill : Pressable
  {
    // Token: 0x060015FB RID: 5627 RVA: 0x005B9248 File Offset: 0x005B9248
    public NotchUsagePill(INotchHost host, BillingUsageRing usage)
      : base(true)
    {
      NotchUsagePill <>4__this = this;
      this.usage = usage;
      base.Width = (base.Height = 32.0);
      base.CornerRadius = new CornerRadius(16.0);
      base.Background = Ui.Brush(DS.White(0.08));
      base.Tag = "notch-usage";
      base.Children.Add(NotchUsagePill.Ring(Math.Max(0.03, usage.Progress), DS.White(0.18), usage.IsSpent ? NotchUsagePill.SpentRed : DS.White(0.9)));
      base.HoverChanged += delegate(bool h)
      {
        <>4__this.Background = Ui.Brush(DS.White(h ? 0.14 : 0.08));
        if (h)
        {
          <>4__this.ShowCard();
          return;
        }
        <>4__this.HideCard();
      };
      base.Click += delegate
      {
        <>4__this.HideCard();
        NotchPresenter notchPresenter = host.Services.Presenter as NotchPresenter;
        if (notchPresenter == null)
        {
          return;
        }
        notchPresenter.Tabs.ShowSettings(NotchSettingsSection.Account);
      };
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        <>4__this.HideCard();
      };
      AutomationProperties.SetName(this, "Monthly usage");
      AutomationProperties.SetHelpText(this, usage.AccessibilityValue);
    }

    // Token: 0x060015FC RID: 5628 RVA: 0x005B9364 File Offset: 0x005B9364
    private static FrameworkElement Ring(double progress, Color track, Color tint)
    {
      Grid grid = new Grid();
      grid.Width = 13.0;
      grid.Height = 13.0;
      grid.HorizontalAlignment = HorizontalAlignment.Center;
      grid.VerticalAlignment = VerticalAlignment.Center;
      grid.IsHitTestVisible = false;
      grid.Children.Add(new Ellipse
      {
        Stroke = Ui.Brush(track),
        StrokeThickness = 2.5
      });
      double num = 32.98672286269283;
      double num2 = Math.Max(0.03, Math.Clamp(progress, 0.0, 1.0)) * num / 2.5;
      grid.Children.Add(new Ellipse
      {
        Stroke = Ui.Brush(tint),
        StrokeThickness = 2.5,
        StrokeDashCap = PenLineCap.Round,
        StrokeStartLineCap = PenLineCap.Round,
        StrokeEndLineCap = PenLineCap.Round,
        StrokeDashArray = new DoubleCollection { num2, 1000.0 },
        RenderTransformOrigin = new Point(0.5, 0.5),
        RenderTransform = new RotateTransform
        {
          Angle = -90.0
        }
      });
      return grid;
    }

    // Token: 0x060015FD RID: 5629 RVA: 0x005B94AC File Offset: 0x005B94AC
    private void ShowCard()
    {
      if (base.XamlRoot == null)
      {
        return;
      }
      if (this.card == null)
      {
        this.card = NotchUsagePill.UsageCard(this.usage);
      }
      if (this.popup == null)
      {
        this.popup = new Popup
        {
          Child = this.card,
          IsHitTestVisible = false,
          XamlRoot = base.XamlRoot,
          ShouldConstrainToRootBounds = false
        };
      }
      Point point = base.TransformToVisual(null).TransformPoint(new Point(0f, 0f));
      this.popup.HorizontalOffset = point.X;
      this.popup.VerticalOffset = point.Y + base.ActualHeight + 8.0;
      if (this.popup.IsOpen)
      {
        return;
      }
      this.card.Opacity = 0.0;
      this.popup.IsOpen = true;
      Anim.Opacity(this.card, 1f, 0.16, Easing.EaseOut(Ui.Compositor), 0.0);
    }

    // Token: 0x060015FE RID: 5630 RVA: 0x005B95C0 File Offset: 0x005B95C0
    public void HideCard()
    {
      Popup popup = this.popup;
      if (popup != null && popup.IsOpen)
      {
        this.popup.IsOpen = false;
      }
    }

    // Token: 0x060015FF RID: 5631 RVA: 0x005B95EC File Offset: 0x005B95EC
    private static Border UsageCard(BillingUsageRing usage)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 10.0
      };
      stackPanel.Children.Add(NotchUsagePill.Section(usage.Agents));
      stackPanel.Children.Add(new Border
      {
        Height = 1.0,
        Background = Ui.Brush(DS.White(0.12))
      });
      stackPanel.Children.Add(NotchUsagePill.Section(usage.Voice));
      string resetsLine = usage.ResetsLine;
      if (resetsLine != null)
      {
        stackPanel.Children.Add(Ui.Text(resetsLine, 10.5, FontWeights.Medium, HomeTheme.SecondaryText, TextAlignment.Left, 0, false, false));
      }
      return new Border
      {
        Padding = new Thickness(12.0, 10.0, 12.0, 10.0),
        CornerRadius = new CornerRadius(10.0),
        Background = Ui.Brush(HomeTheme.IncomingBubble),
        BorderBrush = Ui.Brush(DS.White(0.08)),
        BorderThickness = new Thickness(1.0),
        IsHitTestVisible = false,
        Child = stackPanel
      };
    }

    // Token: 0x06001600 RID: 5632 RVA: 0x005B9738 File Offset: 0x005B9738
    private static FrameworkElement Section(BillingUsageSection usage)
    {
      TextBlock textBlock = Ui.Text(usage.Title, 9.5, FontWeights.Bold, HomeTheme.TertiaryText, TextAlignment.Left, 0, false, false);
      textBlock.CharacterSpacing = 53;
      TextBlock textBlock2 = Ui.Text(usage.CountText, 15.0, FontWeights.SemiBold, HomeTheme.PrimaryText, TextAlignment.Left, 0, false, false);
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 4.0,
        Children = { textBlock2 }
      };
      string capText = usage.CapText;
      if (capText != null)
      {
        TextBlock textBlock3 = Ui.Text(capText, 11.0, FontWeights.Medium, HomeTheme.SecondaryText, TextAlignment.Left, 0, false, false);
        textBlock3.VerticalAlignment = VerticalAlignment.Bottom;
        textBlock3.Margin = new Thickness(0.0, 0.0, 0.0, 2.0);
        stackPanel.Children.Add(textBlock3);
      }
      Grid grid = new Grid
      {
        Width = 150.0,
        Height = 4.0,
        HorizontalAlignment = HorizontalAlignment.Left
      };
      grid.Children.Add(new Border
      {
        Background = Ui.Brush(DS.White(0.14))
      }.Capsule());
      grid.Children.Add(new Border
      {
        Width = Math.Max(4.0, 150.0 * usage.Progress),
        HorizontalAlignment = HorizontalAlignment.Left,
        Background = Ui.Brush(usage.IsSpent ? NotchUsagePill.SpentRed : DS.White(0.9))
      }.Capsule());
      return new StackPanel
      {
        Spacing = 6.0,
        Children = { textBlock, stackPanel, grid }
      };
    }

    // Token: 0x04000AAC RID: 2732
    private static readonly Color SpentRed = DS.Rgb(0.96, 0.3, 0.28, 1.0);

    // Token: 0x04000AAD RID: 2733
    private readonly BillingUsageRing usage;

    // Token: 0x04000AAE RID: 2734
    [Nullable(2)]
    private Popup popup;

    // Token: 0x04000AAF RID: 2735
    [Nullable(2)]
    private Border card;
  }
}
