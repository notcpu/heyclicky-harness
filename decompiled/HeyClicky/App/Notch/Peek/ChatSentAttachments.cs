using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000177 RID: 375
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ChatSentAttachments : StackPanel
  {
    // Token: 0x060014A1 RID: 5281 RVA: 0x005AE204 File Offset: 0x005AE204
    public ChatSentAttachments(IReadOnlyList<string> paths, Action<string> open)
    {
      MessagesPalette messagesPalette = MessagesPalette.Current;
      base.Orientation = Orientation.Horizontal;
      List<string> list = paths.Take<string>(4).ToList<string>();
      base.Spacing = (double)((list.Count > 1) ? (-18) : 0);
      base.Padding = new Thickness(4.0);
      for (int i = 0; i < list.Count; i++)
      {
        base.Children.Add(ChatSentAttachments.Preview(list[i], (list.Count > 1) ? ChatSentAttachments.Tilts[i % ChatSentAttachments.Tilts.Length] : 0.0, open, messagesPalette));
      }
      if (paths.Count > 4)
      {
        Border border = new Border();
        border.Height = 24.0;
        border.Padding = new Thickness(8.0, 0.0, 8.0, 0.0);
        border.CornerRadius = new CornerRadius(12.0);
        border.Background = Ui.Brush(messagesPalette.InkAt(0.14));
        border.Margin = new Thickness(24.0, 0.0, 0.0, 0.0);
        border.VerticalAlignment = VerticalAlignment.Center;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
        defaultInterpolatedStringHandler.AppendLiteral("+");
        defaultInterpolatedStringHandler.AppendFormatted<int>(paths.Count - 4);
        border.Child = Ui.Text(defaultInterpolatedStringHandler.ToStringAndClear(), 11.0, FontWeights.SemiBold, messagesPalette.InkAt(0.85), TextAlignment.Left, 0, false, false).Center<TextBlock>();
        Border border2 = border;
        Canvas.SetZIndex(border2, 200);
        base.Children.Add(border2);
      }
    }

    // Token: 0x060014A2 RID: 5282 RVA: 0x005AE3D0 File Offset: 0x005AE3D0
    private static FrameworkElement Preview(string path, double tilt, Action<string> open, MessagesPalette look)
    {
      bool flag = File.Exists(path);
      Pressable p = new Pressable(true)
      {
        Width = 58.0,
        Height = 58.0,
        Opacity = (flag ? 1.0 : 0.45)
      };
      p.Enabled = flag;
      ShadowLayer shadowLayer = new ShadowLayer(11.0, DS.Black(0.35), 3.0, 1.5, 0.0);
      Border border = new Border
      {
        CornerRadius = new CornerRadius(11.0),
        Background = Ui.Brush(look.InkAt(0.3))
      };
      Border border2 = new Border
      {
        Margin = new Thickness(3.0),
        CornerRadius = new CornerRadius(8.0),
        Background = Ui.Brush(look.InkAt(0.08))
      };
      if (flag && ChatBlockViews.IsImage(path))
      {
        border2.Background = new ImageBrush
        {
          ImageSource = new BitmapImage(new Uri(path))
          {
            DecodePixelWidth = 156
          },
          Stretch = Stretch.UniformToFill
        };
      }
      else if (flag)
      {
        ChatSentAttachments.ShellIcon(border2, path, look.SecondaryText);
      }
      p.Children.Add(shadowLayer);
      p.Children.Add(border);
      p.Children.Add(border2);
      p.Rotation = (float)tilt;
      p.Loaded += delegate(object _, RoutedEventArgs _)
      {
        p.CenterPoint = new Vector3((float)p.ActualWidth / 2f, (float)p.ActualHeight / 2f, 0f);
      };
      SwiftSpring spring = new SwiftSpring(0.28, 0.78);
      p.HoverChanged += delegate(bool h)
      {
        Canvas.SetZIndex(p, h ? 100 : 0);
        Anim.ScaleSpring(p, h ? 1.06f : 1f, spring, null);
        p.Rotation = (h ? 0f : ((float)tilt));
      };
      p.Click += delegate
      {
        open(path);
      };
      p.Tip(Path.GetFileName(path));
      return p;
    }

    // Token: 0x060014A3 RID: 5283 RVA: 0x005AE630 File Offset: 0x005AE630
    private static async Task ShellIcon(Border inner, string path, Color fallbackInk)
    {
      try
      {
        BitmapImage bmp;
        using (StorageItemThumbnail thumb = await (await StorageFile.GetFileFromPathAsync(path)).GetThumbnailAsync(ThumbnailMode.SingleItem, 96U))
        {
          if (thumb == null)
          {
            return;
          }
          bmp = new BitmapImage();
          await bmp.SetSourceAsync(thumb);
          inner.Child = new Image
          {
            Source = bmp,
            Margin = new Thickness(4.0),
            Stretch = Stretch.Uniform
          };
        }
        StorageItemThumbnail thumb = null;
        bmp = null;
      }
      catch
      {
        inner.Child = Icons.Glyph("\ue8a5", 18.0, fallbackInk);
      }
    }

    // Token: 0x040009F6 RID: 2550
    private const double PreviewSize = 52.0;

    // Token: 0x040009F7 RID: 2551
    private static readonly double[] Tilts = new double[] { -5.0, 4.0, -2.5, 6.0 };

    // Token: 0x040009F8 RID: 2552
    private const int MaximumShown = 4;
  }
}
