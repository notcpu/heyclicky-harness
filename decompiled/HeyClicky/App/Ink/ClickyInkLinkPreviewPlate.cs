using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000243 RID: 579
  [NullableContext(2)]
  [Nullable(0)]
  public static class ClickyInkLinkPreviewPlate
  {
    // Token: 0x17000483 RID: 1155
    // (get) Token: 0x060019E3 RID: 6627 RVA: 0x005DB918 File Offset: 0x005DB918
    // (set) Token: 0x060019E4 RID: 6628 RVA: 0x005DB920 File Offset: 0x005DB920
    public static string WorkerBaseUrl { get; set; }

    // Token: 0x060019E5 RID: 6629 RVA: 0x005DB928 File Offset: 0x005DB928
    [NullableContext(1)]
    public static FrameworkElement Make(string urlString, double iconSize = 40.0, Color? plate = null)
    {
      FrameworkElement frameworkElement = InkViews.Symbol("link", iconSize * 0.62, PaperColor.Hex("#4A7CF0"), true) ?? new Grid();
      Grid grid = new Grid
      {
        Width = iconSize,
        Height = iconSize,
        Children = { frameworkElement }
      };
      FrameworkElement frameworkElement2 = grid;
      string text = ClickyInkLinkPreviewPlate.FaviconUrl(urlString);
      if (text != null)
      {
        frameworkElement2 = IntegrationLogos.Remote(text, iconSize, grid);
      }
      frameworkElement2.Width = iconSize;
      frameworkElement2.Height = iconSize;
      frameworkElement2.Clip = new RectangleGeometry
      {
        Rect = new Rect(0.0, 0.0, iconSize, iconSize)
      };
      string text2 = "link-plate-icon";
      IClickyShape clickyShape = ClickyShape.RoundedRectangle(iconSize * 0.22);
      PaperFill paperFill = PaperFill.Of(PaperColor.Clear);
      PaperShadow[] array = new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.12), 3.0, 0.0, 1.5)
      };
      InkPaperSheet inkPaperSheet = new InkPaperSheet(text2, clickyShape, paperFill, null, array, null, null)
      {
        Width = iconSize,
        Height = iconSize
      };
      Grid grid2 = new Grid
      {
        Width = iconSize,
        Height = iconSize,
        CornerRadius = new CornerRadius(iconSize * 0.22),
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        Children = { frameworkElement2 }
      };
      Grid grid3 = new Grid
      {
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        Children = { inkPaperSheet }
      };
      return new Grid
      {
        Background = Ui.Brush(plate ?? Color.FromArgb(byte.MaxValue, 242, 242, 242)),
        Children = { grid3, grid2 }
      };
    }

    // Token: 0x060019E6 RID: 6630 RVA: 0x005DBB0C File Offset: 0x005DBB0C
    [NullableContext(1)]
    [return: Nullable(2)]
    private static string FaviconUrl(string urlString)
    {
      string workerBaseUrl = ClickyInkLinkPreviewPlate.WorkerBaseUrl;
      Uri uri;
      if (workerBaseUrl == null || workerBaseUrl.Length <= 0 || !Uri.TryCreate(urlString, UriKind.Absolute, out uri) || uri.Host.Length == 0)
      {
        return null;
      }
      return workerBaseUrl.TrimEnd('/') + "/favicon?host=" + Uri.EscapeDataString(uri.Host.ToLowerInvariant()) + "&sz=128";
    }
  }
}
