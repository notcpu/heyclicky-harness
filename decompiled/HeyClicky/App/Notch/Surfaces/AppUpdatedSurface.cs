using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using HeyClicky.Core.Telemetry;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x0200014E RID: 334
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AppUpdatedSurface : NotchSurface
  {
    // Token: 0x060012CB RID: 4811 RVA: 0x00599FAC File Offset: 0x00599FAC
    public AppUpdatedSurface(NotchLiveEvent.AppUpdated e)
    {
      Grid grid = CardParts.Tile(Icons.PathIcon("M10,3 C10.6,7.6 12.4,9.4 17,10 C12.4,10.6 10.6,12.4 10,17 C9.4,12.4 7.6,10.6 3,10 C7.6,9.4 9.4,7.6 10,3 Z M18,13 C18.3,15.2 18.8,15.7 21,16 C18.8,16.3 18.3,16.8 18,19 C17.7,16.8 17.2,16.3 15,16 C17.2,15.7 17.7,15.2 18,13 Z M17,2 C17.2,3.4 17.6,3.8 19,4 C17.6,4.2 17.2,4.6 17,6 C16.8,4.6 16.4,4.2 15,4 C16.4,3.8 16.8,3.4 17,2 Z", 17.0, DS.Hex(2450411U, 1.0)), 34.0);
      StackPanel stackPanel = CardParts.TwoLines("HeyClicky updated", "You're on v" + e.Version, false);
      GelActionButton gelActionButton = GelActionButton.WithIcon("See what's new", (Color c) => Icons.StrokeIcon("M5,19 L19,5 M8,5 L19,5 L19,16", 8.0, c, 1.5), GelAppearance.PrimaryBlue, 30.0, 14.0, 0.0, 12.0, 5.0, true).Tip("Open the HeyClicky changelog");
      gelActionButton.Click += delegate
      {
        ClickyAnalytics.TrackChangelogOpened("update_card");
        base.Host.Actions.OpenUrl(AppHost.ChangelogUrl);
        base.Host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.AppUpdated));
      };
      RingCloseButton ringCloseButton = new RingCloseButton(true).Tip("Dismiss");
      ringCloseButton.Click += delegate
      {
        base.Host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.AppUpdated));
      };
      ringCloseButton.StartCountdown(12.0, 1.0);
      this.view = CardParts.HeadsUpRow(grid, stackPanel, gelActionButton, ringCloseButton);
    }

    // Token: 0x17000367 RID: 871
    // (get) Token: 0x060012CC RID: 4812 RVA: 0x0059A0D0 File Offset: 0x0059A0D0
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x040008A6 RID: 2214
    private readonly Grid view;

    // Token: 0x040008A7 RID: 2215
    public const double AutoHideSeconds = 12.0;
  }
}
