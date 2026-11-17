using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;

namespace HeyClicky.App.Home
{
  // Token: 0x020002AC RID: 684
  [NullableContext(1)]
  [Nullable(0)]
  public static class HomeParts
  {
    // Token: 0x06001C06 RID: 7174 RVA: 0x005F8400 File Offset: 0x005F8400
    public static Grid AccountAvatar(double size, string initial, [Nullable(2)] string avatarUrl)
    {
      Grid grid = new Grid
      {
        Width = size,
        Height = size,
        Tag = "account-avatar"
      };
      grid.Children.Add(new Ellipse
      {
        Fill = Ui.Brush(HomeTheme.AvatarPlate)
      });
      grid.Children.Add(Ui.Text(initial, Math.Round(size * 0.43), FontWeights.SemiBold, HomeTheme.PrimaryText, TextAlignment.Left, 0, false, false).Center<TextBlock>());
      Uri uri = HomeParts.AvatarUri(avatarUrl);
      if (uri != null)
      {
        BitmapImage bitmapImage = new BitmapImage
        {
          DecodePixelWidth = (int)Math.Ceiling(size * 2.0),
          DecodePixelType = DecodePixelType.Logical
        };
        Ellipse photo = new Ellipse
        {
          Opacity = 0.0,
          Tag = "account-avatar-photo",
          Fill = new ImageBrush
          {
            ImageSource = bitmapImage,
            Stretch = Stretch.UniformToFill
          }
        };
        bitmapImage.ImageOpened += delegate(object _, RoutedEventArgs _)
        {
          photo.Opacity = 1.0;
        };
        bitmapImage.ImageFailed += delegate(object _, ExceptionRoutedEventArgs e)
        {
          Log.Warn("home", "profile photo failed: " + e.ErrorMessage);
        };
        bitmapImage.UriSource = uri;
        grid.Children.Add(photo);
      }
      return grid;
    }

    // Token: 0x06001C07 RID: 7175 RVA: 0x005F8548 File Offset: 0x005F8548
    [NullableContext(2)]
    public static Uri AvatarUri(string avatarUrl)
    {
      Uri uri;
      bool flag = avatarUrl != null && avatarUrl.Length > 0 && Uri.TryCreate(avatarUrl, UriKind.Absolute, out uri);
      if (flag)
      {
        string scheme = uri.Scheme;
        bool flag2 = scheme == "https" || scheme == "http";
        flag = flag2 || uri.IsFile;
      }
      if (!flag)
      {
        return null;
      }
      return uri;
    }

    // Token: 0x04000D74 RID: 3444
    public const string GiftPath = "M2.5,8.2 h8.6 v3.8 h-8.6 z M12.9,8.2 h8.6 v3.8 h-8.6 z M4,13.4 h7.1 v8.1 h-5.6 a1.5,1.5 0 0 1 -1.5,-1.5 z M12.9,13.4 h7.1 v6.6 a1.5,1.5 0 0 1 -1.5,1.5 h-5.6 z M12,7.6 C10.6,4.6 7.8,2.8 6.6,4.2 C5.6,5.4 6.6,7.4 12,7.6 z M12,7.6 C13.4,4.6 16.2,2.8 17.4,4.2 C18.4,5.4 17.4,7.4 12,7.6 z";
  }
}
