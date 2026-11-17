using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.Core.Browser;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002EF RID: 751
  [NullableContext(1)]
  [Nullable(0)]
  internal static class ChromeIcon
  {
    // Token: 0x06002044 RID: 8260 RVA: 0x00615EBC File Offset: 0x00615EBC
    [NullableContext(2)]
    public static FrameworkElement Make(double size)
    {
      string setupBrowserName = ClickyBrowserBridge.Shared.SetupBrowserName;
      string text;
      if (!ChromeIcon.executables.TryGetValue(setupBrowserName, out text))
      {
        text = (ChromeIcon.executables[setupBrowserName] = ChromiumBrowsers.ExecutablePathFor(setupBrowserName));
      }
      if (text == null || text.Length <= 0)
      {
        return null;
      }
      Image image = new Image
      {
        Width = size,
        Height = size,
        Stretch = Stretch.Uniform
      };
      ImageSource imageSource;
      if (ChromeIcon.icons.TryGetValue(setupBrowserName, out imageSource))
      {
        image.Source = imageSource;
      }
      else if (ChromeIcon.lookedUp.Add(setupBrowserName))
      {
        ChromeIcon.LoadAsync(setupBrowserName, text, image);
      }
      return image;
    }

    // Token: 0x06002045 RID: 8261 RVA: 0x00615F50 File Offset: 0x00615F50
    private static async Task LoadAsync(string name, string executable, Image image)
    {
      try
      {
        BitmapImage bitmap;
        using (StorageItemThumbnail thumbnail = await (await StorageFile.GetFileFromPathAsync(executable)).GetThumbnailAsync(ThumbnailMode.SingleItem, 64U))
        {
          bitmap = new BitmapImage();
          await bitmap.SetSourceAsync(thumbnail);
          ChromeIcon.icons[name] = bitmap;
          image.Source = bitmap;
        }
        StorageItemThumbnail thumbnail = null;
        bitmap = null;
      }
      catch (Exception ex)
      {
        Log.Warn("dock", "browser icon: " + ex.Message);
      }
    }

    // Token: 0x04000F9F RID: 3999
    private static readonly Dictionary<string, ImageSource> icons = new Dictionary<string, ImageSource>();

    // Token: 0x04000FA0 RID: 4000
    [Nullable(new byte[] { 1, 1, 2 })]
    private static readonly Dictionary<string, string> executables = new Dictionary<string, string>();

    // Token: 0x04000FA1 RID: 4001
    private static readonly HashSet<string> lookedUp = new HashSet<string>();
  }
}
