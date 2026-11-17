using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Portraits;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Demo
{
  // Token: 0x02000356 RID: 854
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PortraitGallery
  {
    // Token: 0x170006A0 RID: 1696
    // (get) Token: 0x06002537 RID: 9527 RVA: 0x0063A0FC File Offset: 0x0063A0FC
    public PanelWindow Window { get; }

    // Token: 0x06002538 RID: 9528 RVA: 0x0063A104 File Offset: 0x0063A104
    public PortraitGallery(bool excludeFromCapture)
    {
      this.Window = new PanelWindow("Portraits", excludeFromCapture, true, 14.0, null, 0.1);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 10.0,
        Margin = new Thickness(18.0)
      };
      foreach (PortraitPack portraitPack in new PortraitPack[]
      {
        PortraitPack.ClassicClouds,
        PortraitPack.PuffPals,
        PortraitPack.StorybookSpirits,
        PortraitPack.CloudClub,
        PortraitPack.ArcadeAllStars
      })
      {
        stackPanel.Children.Add(Ui.Text(portraitPack.Title(), 12.0, FontWeights.SemiBold, DS.Colors.TextSecondary, TextAlignment.Left, 0, false, false));
        StackPanel stackPanel2 = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 6.0
        };
        foreach (PortraitCharacter portraitCharacter in PortraitCharacter.CharactersOf(portraitPack))
        {
          stackPanel2.Children.Add(new PortraitView(portraitCharacter.Appearance, 64.0, 64.0, PortraitExpression.Happy, true, null, 0.0)
          {
            AnimatesWhileIdle = false
          });
        }
        stackPanel.Children.Add(stackPanel2);
      }
      stackPanel.Children.Add(Ui.Text("Expressions (no plate)", 12.0, FontWeights.SemiBold, DS.Colors.TextSecondary, TextAlignment.Left, 0, false, false));
      foreach (PortraitCharacter portraitCharacter2 in new PortraitCharacter[]
      {
        PortraitCharacter.CharactersOf(PortraitPack.ClassicClouds)[0],
        PortraitCharacter.Sunny,
        PortraitCharacter.CharactersOf(PortraitPack.StorybookSpirits)[1]
      })
      {
        StackPanel stackPanel3 = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 2.0
        };
        foreach (PortraitExpression portraitExpression in Enum.GetValues<PortraitExpression>())
        {
          stackPanel3.Children.Add(new PortraitView(portraitCharacter2.Appearance, 56.0, 56.0, portraitExpression, false, null, 0.0)
          {
            AnimatesWhileIdle = false
          });
        }
        stackPanel.Children.Add(stackPanel3);
      }
      stackPanel.Children.Add(Ui.Text("Sizes: notch badge 26, chat 20, peek 36, HUD 40, reveal 64, stage 195", 12.0, FontWeights.SemiBold, DS.Colors.TextSecondary, TextAlignment.Left, 0, false, false));
      StackPanel stackPanel4 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 10.0
      };
      stackPanel4.Children.Add(ClickyFace.NotchBadge("demo-1", 26.0));
      stackPanel4.Children.Add(ClickyFace.Create("demo-2", 20.0, PortraitMood.Happy, true, true, false));
      stackPanel4.Children.Add(ClickyFace.Create("demo-3", 36.0, PortraitMood.Running, true, true, false));
      stackPanel4.Children.Add(ClickyFace.Create("demo-1", 40.0, PortraitMood.Sad, true, true, false));
      stackPanel4.Children.Add(ClickyFace.Create("demo-2", 64.0, PortraitMood.Sleepy, true, true, false));
      PortraitAppearance portraitAppearance = PortraitAppearance.HomeMascot("#3380FF");
      stackPanel4.Children.Add(new PortraitView(portraitAppearance, 175.5, 144.3, PortraitExpression.Excited, false, new Color?(portraitAppearance.HairColor.Hair()), 15.6));
      stackPanel4.Children.Add(new PortraitView(PortraitCharacter.Sunny.Appearance, 195.0, 195.0, PortraitExpression.Listening, false, new Color?(PortraitCharacter.Sunny.Appearance.HairColor.Hair()), 15.6));
      stackPanel.Children.Add(stackPanel4);
      this.Window.Panel.Children.Add(stackPanel);
      this.Window.SetPanelSize(1180.0, 940.0);
    }
  }
}
