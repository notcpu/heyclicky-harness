using System;
using System.IO;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Proactive;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x0200015F RID: 351
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AppTipSurface : NotchSurface
  {
    // Token: 0x06001368 RID: 4968 RVA: 0x005A1794 File Offset: 0x005A1794
    public AppTipSurface(NotchLiveEvent e)
    {
      AppTipSurface <>4__this = this;
      Type dismissType = e.GetType();
      NotchLiveEvent.SidecarLearningSuggestion sidecarLearningSuggestion = e as NotchLiveEvent.SidecarLearningSuggestion;
      string text;
      string text2;
      int num;
      string text3;
      string text4;
      FrameworkElement frameworkElement;
      if (sidecarLearningSuggestion != null)
      {
        text = "I can help you with " + sidecarLearningSuggestion.AppDisplayName;
        text2 = "I have a special skill file for " + sidecarLearningSuggestion.AppDisplayName + " so my AI can better answer questions.";
        num = 2;
        text3 = "Activate Skill";
        text4 = "\uf4a5";
        frameworkElement = AppTipSurface.AppIcon(sidecarLearningSuggestion.AppIconPath);
      }
      else
      {
        text = "I can play songs on Spotify";
        text2 = "Try saying \"Play Good Morning by Kanye West on Spotify\"";
        num = 1;
        text3 = "Got it";
        text4 = "\ue73e";
        frameworkElement = IntegrationLogos.Image("spotify-logo", 24.0, null, 2.0);
      }
      Canvas canvas = new Canvas
      {
        Width = 58.0,
        Height = 38.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      FrameworkElement frameworkElement2 = IntegrationLogos.Image("heyclicky-logo", 24.0, null, 2.0);
      Ui.ClipRounded(frameworkElement2, 5.5);
      Grid grid = CardParts.Tile(frameworkElement2, 34.0);
      Canvas.SetLeft(grid, -10.0);
      Canvas.SetTop(grid, 2.0);
      Grid grid2 = CardParts.Tile(frameworkElement, 34.0);
      Canvas.SetLeft(grid2, 34.0);
      Canvas.SetTop(grid2, 2.0);
      canvas.Children.Add(grid);
      canvas.Children.Add(grid2);
      TextBlock textBlock = Ui.Text(text, 12.5, FontWeights.SemiBold, DS.Colors.TextPrimary, TextAlignment.Left, 1, false, false);
      TextBlock textBlock2 = Ui.Text(text2, 10.5, FontWeights.Medium, DS.Colors.TextSecondary, TextAlignment.Left, num, num > 1, false);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 3.0,
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          (num > 1) ? textBlock : Ui.Shrink(textBlock, HorizontalAlignment.Left, 0.85),
          (num > 1) ? textBlock2 : Ui.Shrink(textBlock2, HorizontalAlignment.Left, 0.85)
        }
      };
      GelActionButton gelActionButton = new GelActionButton(text3, text4, GelAppearance.PrimaryBlue, 28.0, 12.0, 86.0, 10.5, 10.0, 5.0, false).Tip(text3);
      gelActionButton.VerticalAlignment = VerticalAlignment.Center;
      gelActionButton.Click += delegate
      {
        if (dismissType == typeof(NotchLiveEvent.SpotifyVoiceTip))
        {
          ProactiveCoordinator proactiveCoordinator = ProactiveCoordinator.Current;
          if (proactiveCoordinator != null)
          {
            proactiveCoordinator.AcknowledgeSpotifyTip();
            return;
          }
        }
        <>4__this.Host.Services.Presenter.DismissLiveEvent(dismissType);
      };
      this.view = new Grid
      {
        ColumnSpacing = 20.0,
        Margin = new Thickness(16.0, 4.0, 16.0, 0.0),
        RenderTransform = new TranslateTransform
        {
          Y = 6.0
        }
      };
      this.view.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      this.view.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      this.view.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      Grid.SetColumn(stackPanel, 1);
      Grid.SetColumn(gelActionButton, 2);
      gelActionButton.Margin = new Thickness(30.0, 0.0, 0.0, 0.0);
      this.view.Children.Add(canvas);
      this.view.Children.Add(stackPanel);
      this.view.Children.Add(gelActionButton);
    }

    // Token: 0x06001369 RID: 4969 RVA: 0x005A1B8C File Offset: 0x005A1B8C
    private static FrameworkElement AppIcon([Nullable(2)] string path)
    {
      bool flag = path != null && path.Length > 0 && File.Exists(path);
      if (flag)
      {
        string text = Path.GetExtension(path).ToLowerInvariant();
        bool flag2 = text == ".png" || text == ".jpg" || text == ".jpeg" || text == ".ico" || text == ".bmp";
        flag = flag2;
      }
      if (flag)
      {
        return new Image
        {
          Source = new BitmapImage(new Uri(path)),
          Width = 24.0,
          Height = 24.0,
          Stretch = Stretch.Uniform
        };
      }
      TextBlock textBlock = Icons.Glyph("\ue9ce", 16.0, DS.Colors.TextSecondary);
      textBlock.FontWeight = FontWeights.SemiBold;
      return textBlock;
    }

    // Token: 0x17000387 RID: 903
    // (get) Token: 0x0600136A RID: 4970 RVA: 0x005A1C6C File Offset: 0x005A1C6C
    public override VerticalAlignment ContentAlignment
    {
      get
      {
        return VerticalAlignment.Center;
      }
    }

    // Token: 0x17000388 RID: 904
    // (get) Token: 0x0600136B RID: 4971 RVA: 0x005A1C70 File Offset: 0x005A1C70
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x04000900 RID: 2304
    private readonly Grid view;
  }
}
