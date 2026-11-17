using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.App.UI;
using HeyClicky.Platform.Audio;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000150 RID: 336
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MutedSpeakerSurface : NotchSurface
  {
    // Token: 0x060012D2 RID: 4818 RVA: 0x0059A254 File Offset: 0x0059A254
    public MutedSpeakerSurface(NotchLiveEvent.VoiceReplyMutedCopiedToClipboard e)
    {
      string kind = e.Kind;
      ValueTuple<string, string> valueTuple;
      if (!(kind == "muted"))
      {
        if (!(kind == "sound_came_back"))
        {
          valueTuple = new ValueTuple<string, string>("Your volume is at zero", "Set volume to 50%");
        }
        else
        {
          valueTuple = new ValueTuple<string, string>("That reply wasn't heard", "");
        }
      }
      else
      {
        valueTuple = new ValueTuple<string, string>("Your PC is muted", "Unmute");
      }
      ValueTuple<string, string> valueTuple2 = valueTuple;
      string item = valueTuple2.Item1;
      string item2 = valueTuple2.Item2;
      StackPanel stackPanel = CardParts.CenteredTitle(item, "The answer is on your clipboard. Press Ctrl+V to paste", 3.0);
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 8.0,
        Margin = new Thickness(34.0, 34.0, 34.0, 0.0),
        VerticalAlignment = VerticalAlignment.Top,
        Children = { stackPanel }
      };
      if (item2.Length > 0)
      {
        GelActionButton gelActionButton = new GelActionButton(item2, "\ue767", GelAppearance.PrimaryBlue, 32.0, 16.0, 0.0, 12.0, 11.5, 6.0, false).Tip("Turn your sound back on");
        gelActionButton.HorizontalAlignment = HorizontalAlignment.Center;
        gelActionButton.Margin = new Thickness(0.0, 6.0, 0.0, 0.0);
        gelActionButton.Click += delegate
        {
          Task.Run<bool>(() => SystemAudioOutputVolume.RestoreAudibleOutput(0.5f));
          base.Host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.VoiceReplyMutedCopiedToClipboard));
        };
        stackPanel2.Children.Add(gelActionButton);
      }
      RingCloseButton ringCloseButton = new RingCloseButton(true).Tip("Dismiss");
      ringCloseButton.Click += delegate
      {
        base.Host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.VoiceReplyMutedCopiedToClipboard));
      };
      ringCloseButton.HorizontalAlignment = HorizontalAlignment.Right;
      ringCloseButton.VerticalAlignment = VerticalAlignment.Top;
      ringCloseButton.Margin = new Thickness(0.0, (double)(NotchGeometry.HasHardwareNotchEar ? 2 : 8), 10.0, 0.0);
      ringCloseButton.StartCountdown(10.0, 1.0);
      this.view = CardParts.Stack(new UIElement[] { stackPanel2, ringCloseButton });
    }

    // Token: 0x17000369 RID: 873
    // (get) Token: 0x060012D3 RID: 4819 RVA: 0x0059A490 File Offset: 0x0059A490
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x040008A9 RID: 2217
    private readonly Grid view;
  }
}
