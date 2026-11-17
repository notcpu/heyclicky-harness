using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000284 RID: 644
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkObjectKit
  {
    // Token: 0x06001B17 RID: 6935 RVA: 0x005EAA00 File Offset: 0x005EAA00
    public static FrameworkElement Leading(FrameworkElement content, double width)
    {
      content.HorizontalAlignment = HorizontalAlignment.Left;
      return new Grid
      {
        Width = Math.Max(0.0, width),
        HorizontalAlignment = HorizontalAlignment.Left,
        Children = { content }
      };
    }

    // Token: 0x06001B18 RID: 6936 RVA: 0x005EAA38 File Offset: 0x005EAA38
    public static InkTextView Line(string text, PaperFont font, Color color, int? lineLimit = 1)
    {
      return InkTextView.Plain(text, font, color).With(delegate(InkTextView t)
      {
        t.LineLimit = lineLimit;
      });
    }

    // Token: 0x06001B19 RID: 6937 RVA: 0x005EAA6C File Offset: 0x005EAA6C
    public static void FileMenu(FrameworkElement target, string rawPath, string resolvedPath, bool isWebLink, Action open)
    {
      MenuFlyout menuFlyout = new MenuFlyout();
      MenuFlyoutItem menuFlyoutItem = new MenuFlyoutItem
      {
        Text = "Open"
      };
      menuFlyoutItem.Click += delegate(object _, RoutedEventArgs _)
      {
        open();
      };
      menuFlyout.Items.Add(menuFlyoutItem);
      if (!isWebLink)
      {
        MenuFlyoutItem menuFlyoutItem2 = new MenuFlyoutItem
        {
          Text = "Show in Explorer"
        };
        menuFlyoutItem2.Click += delegate(object _, RoutedEventArgs _)
        {
          ArtifactPaths.Reveal(resolvedPath);
        };
        menuFlyout.Items.Add(menuFlyoutItem2);
      }
      target.ContextFlyout = menuFlyout;
    }

    // Token: 0x06001B1A RID: 6938 RVA: 0x005EAAFC File Offset: 0x005EAAFC
    public static FrameworkElement Favicon(string urlString, double size, FrameworkElement fallback)
    {
      Uri uri;
      if (!Uri.TryCreate(urlString, UriKind.Absolute, out uri) || string.IsNullOrEmpty(uri.Host))
      {
        return new Grid
        {
          Width = size,
          Height = size,
          Children = { fallback }
        };
      }
      return IntegrationLogos.Remote(ClickyInkImages.WorkerBaseUrl.TrimEnd('/') + "/favicon?host=" + Uri.EscapeDataString(uri.Host.ToLowerInvariant()) + "&sz=128", size, fallback);
    }
  }
}
