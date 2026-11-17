using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Services;
using HeyClicky.App.UI;
using HeyClicky.Core.Telemetry;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Windows.Foundation;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000170 RID: 368
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ReadAloudSwitch : Pressable
  {
    // Token: 0x0600146C RID: 5228 RVA: 0x005ABF84 File Offset: 0x005ABF84
    public ReadAloudSwitch(AppServices services, double diameter, bool tooltipBelow = false)
      : base(true)
    {
      this.services = services;
      this.diameter = diameter;
      this.tooltipBelow = tooltipBelow;
      base.Height = diameter;
      base.Width = diameter;
      this.gel = new GelSurface(GelAppearance.SecondaryOutline, diameter / 2.0);
      this.glyph = Icons.Glyph("\ue767", (double)((diameter >= 26.0) ? 12 : 10), DS.White(0.5));
      this.glyph.FontWeight = FontWeights.SemiBold;
      base.Children.Add(this.gel);
      base.Children.Add(this.glyph);
      this.tipState = Ui.Text("", 10.0, FontWeights.Bold, DS.White(0.75), TextAlignment.Left, 0, false, false);
      this.tipStateGel = new GelSurface(GelAppearance.SecondaryOutline, 100.0);
      Grid grid = new Grid
      {
        Children = 
        {
          this.tipStateGel,
          new Border
          {
            Padding = new Thickness(7.0, 2.0, 7.0, 2.0),
            Child = this.tipState
          }
        }
      };
      this.tipBody = Ui.Text("", 11.0, FontWeights.Normal, DS.White(0.68), TextAlignment.Left, 0, true, false);
      TextBlock textBlock = Ui.Text("Read aloud", 11.5, FontWeights.SemiBold, DS.White(0.95), TextAlignment.Left, 0, false, false);
      this.tooltip = new Border
      {
        Width = 210.0,
        Padding = new Thickness(10.0, 8.0, 10.0, 8.0),
        CornerRadius = new CornerRadius(10.0),
        Background = Ui.Brush(DS.Rgb(0.17, 0.17, 0.17, 1.0)),
        BorderBrush = Ui.Brush(DS.White(0.12)),
        BorderThickness = new Thickness(1.0),
        Child = new StackPanel
        {
          Spacing = 3.0,
          Children = 
          {
            Ui.H(6.0, new UIElement[] { textBlock, grid }),
            this.tipBody
          }
        }
      };
      this.Apply();
      base.HoverChanged += this.OnHover;
      base.Click += this.Toggle;
      AutomationProperties.SetName(this, "Read answers aloud");
      AutomationProperties.SetHelpText(this, "When on, HeyClicky speaks its answers as well as writing them.");
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        this.HideTip();
      };
    }

    // Token: 0x170003B2 RID: 946
    // (get) Token: 0x0600146D RID: 5229 RVA: 0x005AC2A4 File Offset: 0x005AC2A4
    private bool IsOn
    {
      get
      {
        return this.services.Settings.LibraryModeEnabled;
      }
    }

    // Token: 0x0600146E RID: 5230 RVA: 0x005AC2B8 File Offset: 0x005AC2B8
    private void Apply()
    {
      bool isOn = this.IsOn;
      this.gel.Appearance = (isOn ? GelAppearance.SecondaryWhite : GelAppearance.SecondaryOutline);
      this.glyph.Foreground = Ui.Brush(isOn ? DS.Black(0.82) : DS.White(base.IsHovered ? 0.75 : 0.5));
      this.tipState.Text = (isOn ? "On" : "Off");
      this.tipState.Foreground = Ui.Brush(isOn ? DS.Black(0.8) : DS.White(0.75));
      this.tipStateGel.Appearance = (isOn ? GelAppearance.SecondaryWhite : GelAppearance.SecondaryOutline);
      this.tipBody.Text = (isOn ? "HeyClicky speaks its answers out loud as it writes them. Click to turn off." : "Click to have HeyClicky speak its answers out loud as it writes them.");
      AutomationProperties.SetItemStatus(this, isOn ? "On" : "Off");
    }

    // Token: 0x0600146F RID: 5231 RVA: 0x005AC3B0 File Offset: 0x005AC3B0
    private void OnHover(bool hovering)
    {
      this.gel.Hovered = hovering;
      this.Apply();
      DispatcherQueueTimer dispatcherQueueTimer = this.tipTimer;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      if (!hovering)
      {
        this.HideTip();
        return;
      }
      this.tipTimer = base.DispatcherQueue.CreateTimer().Rooted();
      this.tipTimer.Interval = TimeSpan.FromSeconds(0.25);
      this.tipTimer.IsRepeating = false;
      this.tipTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.ShowTip();
      };
      this.tipTimer.Start();
    }

    // Token: 0x1400005A RID: 90
    // (add) Token: 0x06001470 RID: 5232 RVA: 0x005AC448 File Offset: 0x005AC448
    // (remove) Token: 0x06001471 RID: 5233 RVA: 0x005AC480 File Offset: 0x005AC480
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<bool> Toggled;

    // Token: 0x06001472 RID: 5234 RVA: 0x005AC4B8 File Offset: 0x005AC4B8
    private void Toggle()
    {
      bool flag = !this.IsOn;
      this.services.Settings.LibraryModeEnabled = flag;
      ReadAloudSwitch.ClickyAnalytics_TrackLibraryModeToggled(flag);
      Action<bool> toggled = this.Toggled;
      if (toggled != null)
      {
        toggled(flag);
      }
      this.Apply();
      DispatcherQueueTimer dispatcherQueueTimer = this.tipTimer;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      this.ShowTip();
    }

    // Token: 0x06001473 RID: 5235 RVA: 0x005AC518 File Offset: 0x005AC518
    private static void ClickyAnalytics_TrackLibraryModeToggled(bool on)
    {
      ClickyAnalytics.TrackLibraryModeToggled(on);
    }

    // Token: 0x06001474 RID: 5236 RVA: 0x005AC520 File Offset: 0x005AC520
    private void ShowTip()
    {
      if (base.XamlRoot == null)
      {
        return;
      }
      if (this.popup == null)
      {
        this.popup = new Popup
        {
          Child = this.tooltip,
          IsHitTestVisible = false,
          XamlRoot = base.XamlRoot,
          ShouldConstrainToRootBounds = !this.tooltipBelow
        };
        this.tooltip.Opacity = 0.0;
      }
      this.tooltip.Measure(new Size(210.0, double.PositiveInfinity));
      Point point = base.TransformToVisual(null).TransformPoint(new Point(0f, 0f));
      this.popup.HorizontalOffset = point.X + this.diameter - 210.0;
      this.popup.VerticalOffset = (this.tooltipBelow ? (point.Y + this.diameter + 8.0) : (point.Y - this.tooltip.DesiredSize.Height - 8.0));
      this.popup.IsOpen = true;
      Anim.Opacity(this.tooltip, 1f, 0.16, Easing.EaseOut(Ui.Compositor), 0.0);
    }

    // Token: 0x06001475 RID: 5237 RVA: 0x005AC678 File Offset: 0x005AC678
    private void HideTip()
    {
      Popup popup = this.popup;
      if (popup != null && popup.IsOpen)
      {
        this.popup.IsOpen = false;
      }
    }

    // Token: 0x040009C8 RID: 2504
    private readonly AppServices services;

    // Token: 0x040009C9 RID: 2505
    private readonly GelSurface gel;

    // Token: 0x040009CA RID: 2506
    private readonly TextBlock glyph;

    // Token: 0x040009CB RID: 2507
    private readonly double diameter;

    // Token: 0x040009CC RID: 2508
    [Nullable(2)]
    private DispatcherQueueTimer tipTimer;

    // Token: 0x040009CD RID: 2509
    private readonly Border tooltip;

    // Token: 0x040009CE RID: 2510
    private readonly TextBlock tipState;

    // Token: 0x040009CF RID: 2511
    private readonly TextBlock tipBody;

    // Token: 0x040009D0 RID: 2512
    private readonly GelSurface tipStateGel;

    // Token: 0x040009D1 RID: 2513
    [Nullable(2)]
    private Popup popup;

    // Token: 0x040009D2 RID: 2514
    private readonly bool tooltipBelow;
  }
}
