using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Services;
using HeyClicky.App.UI;
using HeyClicky.App.Wiring;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000160 RID: 352
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DictionaryLearnedSurface : NotchSurface
  {
    // Token: 0x0600136C RID: 4972 RVA: 0x005A1C78 File Offset: 0x005A1C78
    public DictionaryLearnedSurface(NotchLiveEvent.DictionaryWordLearned e)
    {
      DictionaryLearnedSurface <>4__this = this;
      Grid grid = CardParts.IconTile("\ue82d", 15.0, DS.Hex(2450411U, 1.0));
      StackPanel stackPanel = CardParts.TwoLines("Added to your dictionary", "“" + e.Word + "”", false);
      GelActionButton gelActionButton = new GelActionButton("Remove", null, GelAppearance.SecondaryWhite, 30.0, 14.0, 0.0, 12.0, 10.0, 5.0, false).Tip("Remove this word from your dictionary");
      gelActionButton.Click += delegate
      {
        Action<string> removeLearnedWord = DictationHooks.RemoveLearnedWord;
        if (removeLearnedWord != null)
        {
          removeLearnedWord(e.Word);
        }
        else
        {
          INotchCardActions notchCardActions = <>4__this.Host.Actions as INotchCardActions;
          if (notchCardActions != null)
          {
            notchCardActions.RemoveDictionaryWord(e.Word);
          }
        }
        <>4__this.Host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.DictionaryWordLearned));
      };
      RingCloseButton ringCloseButton = new RingCloseButton(true).Tip("Dismiss");
      ringCloseButton.Click += delegate
      {
        <>4__this.Host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.DictionaryWordLearned));
      };
      ringCloseButton.StartCountdown(6.0, 1.0);
      this.view = CardParts.HeadsUpRow(grid, stackPanel, gelActionButton, ringCloseButton);
    }

    // Token: 0x17000389 RID: 905
    // (get) Token: 0x0600136D RID: 4973 RVA: 0x005A1D9C File Offset: 0x005A1D9C
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x04000901 RID: 2305
    public const double AutoHideSeconds = 6.0;

    // Token: 0x04000902 RID: 2306
    private readonly Grid view;
  }
}
