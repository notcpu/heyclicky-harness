using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000151 RID: 337
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MicrophoneTroubleSurface : NotchSurface
  {
    // Token: 0x060012D6 RID: 4822 RVA: 0x0059A510 File Offset: 0x0059A510
    public MicrophoneTroubleSurface(NotchLiveEvent.MicrophoneTrouble e)
    {
      Grid grid = CardParts.IconTile(e.ShowsMicrophoneSettingsAction ? "\uf781" : "\uec71", 14.0, DS.Hex(2450411U, 1.0));
      StackPanel stackPanel = CardParts.TwoLines(e.Message, e.Subtitle ?? "HeyClicky can't hear your microphone", true);
      GelActionButton gelActionButton = null;
      if (e.ShowsMicrophoneSettingsAction)
      {
        gelActionButton = new GelActionButton("Mic settings", null, GelAppearance.PrimaryBlue, 30.0, 14.0, 0.0, 12.0, 10.0, 5.0, false).Tip("Open microphone settings to pick a working mic");
        gelActionButton.Click += delegate
        {
          base.Host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.MicrophoneTrouble));
          AppHost appHost = base.Host.Actions as AppHost;
          if (appHost != null)
          {
            appHost.OpenMicrophoneSettings();
            return;
          }
          base.Host.Services.Permissions.OpenSystemSettings(PermissionKind.Microphone);
        };
      }
      this.close = new RingCloseButton(true);
      this.close.Click += delegate
      {
        base.Host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.MicrophoneTrouble));
      };
      this.view = CardParts.HeadsUpRow(grid, stackPanel, gelActionButton, this.close);
    }

    // Token: 0x1700036A RID: 874
    // (get) Token: 0x060012D7 RID: 4823 RVA: 0x0059A618 File Offset: 0x0059A618
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x060012D8 RID: 4824 RVA: 0x0059A620 File Offset: 0x0059A620
    protected override void OnAttached()
    {
      NotchPresenter notchPresenter = base.Host.Services.Presenter as NotchPresenter;
      if (notchPresenter != null)
      {
        ValueTuple<TimeSpan, TimeSpan>? valueTuple = notchPresenter.LiveEventCountdown(typeof(NotchLiveEvent.MicrophoneTrouble));
        if (valueTuple != null)
        {
          ValueTuple<TimeSpan, TimeSpan> valueOrDefault = valueTuple.GetValueOrDefault();
          TimeSpan item = valueOrDefault.Item1;
          TimeSpan item2 = valueOrDefault.Item2;
          this.close.StartCountdown(item.TotalSeconds, item / item2);
          return;
        }
      }
      this.close.StartCountdown(NotchPresenter.MicrophoneTroubleAutoHide.TotalSeconds, 1.0);
    }

    // Token: 0x040008AA RID: 2218
    private readonly Grid view;

    // Token: 0x040008AB RID: 2219
    private readonly RingCloseButton close;
  }
}
