using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.Ink;
using HeyClicky.App.UI;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000337 RID: 823
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class FileCard : Grid
  {
    // Token: 0x17000674 RID: 1652
    // (get) Token: 0x06002309 RID: 8969 RVA: 0x0062A6E8 File Offset: 0x0062A6E8
    public string Entry { get; }

    // Token: 0x0600230A RID: 8970 RVA: 0x0062A6F0 File Offset: 0x0062A6F0
    public FileCard(string entry, [Nullable(2)] string workspace, string readyButtonTitle, Action open)
    {
      this.Entry = entry;
      base.Width = 138.0;
      base.Height = 166.0;
      bool flag = CardReadyOutput.IsWebLink(entry);
      string text = DockFiles.Resolve(entry, workspace);
      DockFileStyle dockFileStyle = DockFileStyle.For(flag ? entry : text);
      this.restShadow = new ShadowLayer(10.0, DS.Black(0.16), 5.0, 3.0, 0.0);
      this.liftShadow = new ShadowLayer(10.0, DS.Black(0.24), 10.0, 6.0, 0.0)
      {
        Opacity = 0.0
      };
      base.Children.Add(this.restShadow);
      base.Children.Add(this.liftShadow);
      base.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(10.0),
        Background = Ui.Brush(Colors.White),
        BorderBrush = Ui.Brush(DS.Black(0.07)),
        BorderThickness = new Thickness(0.5)
      });
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 7.0,
        Padding = new Thickness(7.0)
      };
      Grid grid = new Grid
      {
        Width = 124.0,
        Height = 92.0
      };
      if (flag)
      {
        grid.Children.Add(ClickyInkLinkPreviewPlate.Make(entry, 40.0, null));
      }
      else
      {
        grid.Children.Add(new Border
        {
          Background = Ui.Brush(dockFileStyle.Tint.WithAlpha(0.14))
        });
        grid.Children.Add(Icons.Glyph(dockFileStyle.Glyph, 26.0, dockFileStyle.Tint));
      }
      Ui.ClipRounded(grid, 6.0);
      Grid grid2 = new Grid
      {
        Children = 
        {
          grid,
          new Border
          {
            CornerRadius = new CornerRadius(6.0),
            BorderBrush = Ui.Brush(DS.Black(0.22)),
            BorderThickness = new Thickness(1.0)
          }
        }
      };
      stackPanel.Children.Add(grid2);
      if (!flag)
      {
        ImageSource imageSource = ArtifactThumbnails.Cached(text);
        if (imageSource != null)
        {
          FileCard.ShowPicture(grid, imageSource);
        }
        else
        {
          FileCard.LoadAsync(grid, text);
        }
      }
      Uri uri;
      TextBlock textBlock = Ui.Text((flag && Uri.TryCreate(entry, UriKind.Absolute, out uri)) ? uri.Host : Path.GetFileName(text.TrimEnd(new char[] { '\\', '/' })), 12.0, FontWeights.Bold, ArcColors.Ink, TextAlignment.Left, 0, false, false);
      textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 5.0,
        Padding = new Thickness(3.0, 4.0, 3.0, 6.0)
      };
      stackPanel2.Children.Add(textBlock);
      GelButton gelButton = ArcButtons.Gel(readyButtonTitle, FileCardStack.FileKind(flag ? entry : text).Item2, GelAppearance.SecondaryWhite, null, null, GelButton.Sizing.Compact, open);
      gelButton.HorizontalAlignment = HorizontalAlignment.Left;
      stackPanel2.Children.Add(gelButton);
      stackPanel.Children.Add(stackPanel2);
      base.Children.Add(stackPanel);
    }

    // Token: 0x0600230B RID: 8971 RVA: 0x0062AAD4 File Offset: 0x0062AAD4
    private static void ShowPicture(Grid preview, ImageSource image)
    {
      preview.Children.Add(new Image
      {
        Source = image,
        Stretch = Stretch.UniformToFill,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      });
    }

    // Token: 0x0600230C RID: 8972 RVA: 0x0062AB04 File Offset: 0x0062AB04
    private static async Task LoadAsync(Grid preview, string path)
    {
      if (File.Exists(path))
      {
        try
        {
          ImageSource imageSource = await ArtifactThumbnails.GetAsync(path, 260);
          if (imageSource != null)
          {
            FileCard.ShowPicture(preview, imageSource);
          }
        }
        catch (Exception ex)
        {
          Log.Warn("dock", "file card picture: " + ex.Message);
        }
      }
    }

    // Token: 0x0600230D RID: 8973 RVA: 0x0062AB50 File Offset: 0x0062AB50
    public void SetHovered(bool hovered)
    {
      Anim.Opacity(this.restShadow, !hovered, 0.2, null, 0.0);
      Anim.Opacity(this.liftShadow, hovered > false, 0.2, null, 0.0);
    }

    // Token: 0x0600230E RID: 8974 RVA: 0x0062ABA4 File Offset: 0x0062ABA4
    public static string AccessibilityText(string entry, [Nullable(2)] string workspace)
    {
      bool flag = CardReadyOutput.IsWebLink(entry);
      string text = DockFiles.Resolve(entry, workspace);
      Uri uri;
      string text2 = (flag ? (Uri.TryCreate(entry, UriKind.Absolute, out uri) ? uri.Host : entry) : Path.GetFileName(text));
      return "Open " + text2 + ", " + DockFileStyle.For(flag ? entry : text).Kind;
    }

    // Token: 0x04001112 RID: 4370
    public const double CardWidth = 138.0;

    // Token: 0x04001113 RID: 4371
    public const double CardHeight = 166.0;

    // Token: 0x04001114 RID: 4372
    private const double PreviewHeight = 92.0;

    // Token: 0x04001116 RID: 4374
    private readonly ShadowLayer restShadow;

    // Token: 0x04001117 RID: 4375
    private readonly ShadowLayer liftShadow;
  }
}
