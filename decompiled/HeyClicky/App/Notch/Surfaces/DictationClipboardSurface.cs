using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x0200014F RID: 335
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DictationClipboardSurface : NotchSurface
  {
    // Token: 0x060012CF RID: 4815 RVA: 0x0059A13C File Offset: 0x0059A13C
    public DictationClipboardSurface()
    {
      StackPanel stackPanel = CardParts.CenteredTitle("Copied to your clipboard", "Press Ctrl+V to paste", 3.0);
      stackPanel.Margin = new Thickness(34.0, 34.0, 34.0, 0.0);
      stackPanel.VerticalAlignment = VerticalAlignment.Top;
      RingCloseButton ringCloseButton = new RingCloseButton(true);
      ringCloseButton.Click += delegate
      {
        base.Host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.DictationCopiedToClipboard));
      };
      ringCloseButton.HorizontalAlignment = HorizontalAlignment.Right;
      ringCloseButton.VerticalAlignment = VerticalAlignment.Top;
      ringCloseButton.Margin = new Thickness(0.0, (double)(NotchGeometry.HasHardwareNotchEar ? 2 : 8), 10.0, 0.0);
      ringCloseButton.StartCountdown(6.0, 1.0);
      this.view = CardParts.Stack(new UIElement[] { stackPanel, ringCloseButton });
    }

    // Token: 0x17000368 RID: 872
    // (get) Token: 0x060012D0 RID: 4816 RVA: 0x0059A228 File Offset: 0x0059A228
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x040008A8 RID: 2216
    private readonly Grid view;
  }
}
