using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Proactive;
using HeyClicky.App.UI;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000152 RID: 338
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MeetingCountdownSurface : NotchSurface
  {
    // Token: 0x060012DB RID: 4827 RVA: 0x0059A730 File Offset: 0x0059A730
    public MeetingCountdownSurface(NotchLiveEvent.MeetingCountdown e)
    {
      this.startsAt = e.StartsAt;
      Grid grid = CardParts.IconTile("\ue787", 16.0, DS.Hex(2450411U, 1.0));
      TextBlock textBlock = Ui.Text(e.Title, 12.5, FontWeights.SemiBold, DS.Colors.TextPrimary, TextAlignment.Left, 1, false, false);
      this.countdown = Ui.Text(this.Line(), 10.5, FontWeights.Medium, DS.Colors.TextSecondary, TextAlignment.Left, 1, false, false);
      Typography.SetNumeralAlignment(this.countdown, FontNumeralAlignment.Tabular);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 3.0,
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          Ui.Shrink(textBlock, HorizontalAlignment.Left, 0.85),
          this.countdown
        }
      };
      Pressable pressable = null;
      Uri url = e.JoinUrl;
      if (url != null)
      {
        string text = e.ActionKind ?? "join";
        string text2;
        if (!(text == "directions"))
        {
          if (!(text == "open"))
          {
            text2 = "Join";
          }
          else
          {
            text2 = "Open";
          }
        }
        else
        {
          text2 = "Get directions";
        }
        string text3 = text2;
        string text4 = url.Host.ToLowerInvariant();
        if (text4 == "meet.google.com")
        {
          text2 = "google-meet-logo";
        }
        else
        {
          string text5;
          if (text4 == "zoom.us" || text4.EndsWith(".zoom.us") || text4 == "zoom.com" || text4.EndsWith(".zoom.com"))
          {
            text5 = "zoom-logo";
          }
          else
          {
            bool flag = text4 == "teams.microsoft.com" || text4 == "teams.live.com";
            text5 = (flag ? "ms-teams-logo" : null);
          }
          text2 = text5;
        }
        string text6 = text2;
        if (text == "join" && text6 != null)
        {
          Pressable pressable2 = new Pressable(true)
          {
            Height = 34.0,
            MinWidth = 94.0
          };
          GelSurface gel = new GelSurface(GelAppearance.PrimaryBlue, 17.0);
          pressable2.Children.Add(gel);
          pressable2.HoverChanged += delegate(bool h)
          {
            gel.Hovered = h;
          };
          FrameworkElement frameworkElement = IntegrationLogos.Image(text6, 16.0, null, 2.0);
          Ui.ClipRounded(frameworkElement, 3.5);
          StackPanel stackPanel2 = Ui.H(6.0, new UIElement[]
          {
            frameworkElement,
            Ui.Embossed(Ui.Text(text3, 12.0, FontWeights.SemiBold, DS.Black(0.85), TextAlignment.Left, 0, false, false), 0.65, 1.0, 1.0)
          });
          stackPanel2.HorizontalAlignment = HorizontalAlignment.Center;
          stackPanel2.VerticalAlignment = VerticalAlignment.Center;
          stackPanel2.Margin = new Thickness(16.0, 0.0, 16.0, 0.0);
          pressable2.Children.Add(stackPanel2);
          pressable2.WithPressScale(0.97f, 0.12, true);
          pressable = pressable2;
        }
        else if (text == "directions")
        {
          string text7 = text3;
          Func<Color, FrameworkElement> func;
          if ((func = MeetingCountdownSurface.<>O.<0>__MapMarker) == null)
          {
            func = (MeetingCountdownSurface.<>O.<0>__MapMarker = new Func<Color, FrameworkElement>(MeetingCountdownSurface.MapMarker));
          }
          pressable = GelActionButton.WithIcon(text7, func, GelAppearance.PrimaryBlue, 34.0, 16.0, 94.0, 12.0, 6.0, false);
        }
        else
        {
          pressable = new GelActionButton(text3, "\uea0c", GelAppearance.PrimaryBlue, 34.0, 16.0, 94.0, 12.0, 11.5, 6.0, false);
        }
        pressable.Tip(text3);
        pressable.Click += delegate
        {
          ProactiveCoordinator proactiveCoordinator = ProactiveCoordinator.Current;
          if (proactiveCoordinator != null)
          {
            proactiveCoordinator.JoinActiveMeeting();
            return;
          }
          this.Host.Actions.OpenUrl(url.ToString());
        };
      }
      Pressable pressable3 = CardParts.PlainClose(delegate
      {
        ProactiveCoordinator proactiveCoordinator2 = ProactiveCoordinator.Current;
        if (proactiveCoordinator2 != null)
        {
          proactiveCoordinator2.DismissActiveMeeting();
          return;
        }
        base.Host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.MeetingCountdown));
      }, "Dismiss");
      this.view = CardParts.HeadsUpRow(grid, stackPanel, pressable, pressable3);
    }

    // Token: 0x060012DC RID: 4828 RVA: 0x0059ABA8 File Offset: 0x0059ABA8
    private static FrameworkElement MapMarker(Color c)
    {
      bool flag = c.R == byte.MaxValue && c.G == byte.MaxValue && c.B == byte.MaxValue;
      Grid grid = new Grid
      {
        Width = 12.0,
        Height = 16.0
      };
      Geometry geometry = (Geometry)XamlBindingHelper.ConvertValue(typeof(Geometry), "M6,16 L3,11.196 A6,6 0 1 1 9,11.196 Z");
      grid.Children.Add(new Path
      {
        Data = geometry,
        Fill = Ui.Brush(flag ? c : Color.FromArgb(byte.MaxValue, 235, 66, 54))
      });
      if (!flag)
      {
        grid.Children.Add(new Ellipse
        {
          Width = 4.5,
          Height = 4.5,
          Fill = Ui.Brush(Colors.White),
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Top,
          Margin = new Thickness(0.0, 3.75, 0.0, 0.0)
        });
      }
      return grid;
    }

    // Token: 0x060012DD RID: 4829 RVA: 0x0059ACDC File Offset: 0x0059ACDC
    private string Line()
    {
      int num = (int)Math.Round((this.startsAt - DateTimeOffset.Now).TotalSeconds);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (num > 0)
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 2);
        defaultInterpolatedStringHandler.AppendLiteral("Starting in ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num / 60);
        defaultInterpolatedStringHandler.AppendLiteral(":");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num % 60, "00");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
      int num2 = -num / 60;
      if (num2 < 1)
      {
        return "Starting now";
      }
      if (num2 < 60)
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 1);
        defaultInterpolatedStringHandler.AppendLiteral("Started ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
        defaultInterpolatedStringHandler.AppendLiteral("m ago");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 2);
      defaultInterpolatedStringHandler.AppendLiteral("Started ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(num2 / 60);
      defaultInterpolatedStringHandler.AppendLiteral("h ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(num2 % 60);
      defaultInterpolatedStringHandler.AppendLiteral("m ago");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x060012DE RID: 4830 RVA: 0x0059ADE4 File Offset: 0x0059ADE4
    protected override void OnAttached()
    {
      this.timer = DispatcherQueue.GetForCurrentThread().CreateTimer();
      this.timer.Interval = TimeSpan.FromSeconds(1L);
      this.timer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.countdown.Text = this.Line();
      };
      this.timer.Start();
    }

    // Token: 0x060012DF RID: 4831 RVA: 0x0059AE38 File Offset: 0x0059AE38
    public override void Detached()
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.timer;
      if (dispatcherQueueTimer == null)
      {
        return;
      }
      dispatcherQueueTimer.Stop();
    }

    // Token: 0x1700036B RID: 875
    // (get) Token: 0x060012E0 RID: 4832 RVA: 0x0059AE4C File Offset: 0x0059AE4C
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x040008AC RID: 2220
    private readonly Grid view;

    // Token: 0x040008AD RID: 2221
    private readonly TextBlock countdown;

    // Token: 0x040008AE RID: 2222
    private readonly DateTimeOffset startsAt;

    // Token: 0x040008AF RID: 2223
    [Nullable(2)]
    private DispatcherQueueTimer timer;

    // Token: 0x020006B1 RID: 1713
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001E71 RID: 7793
      [Nullable(new byte[] { 0, 1 })]
      public static Func<Color, FrameworkElement> <0>__MapMarker;
    }
  }
}
