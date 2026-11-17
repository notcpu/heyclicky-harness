using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002DA RID: 730
  internal static class MarkdownViewerLinks
  {
    // Token: 0x06001ED0 RID: 7888 RVA: 0x00609C18 File Offset: 0x00609C18
    [NullableContext(1)]
    internal static void Opened([Nullable(2)] XamlRoot root, Uri uri)
    {
      Markdown.RaiseLinkOpened(root, uri);
      if (uri.IsFile)
      {
        ChatBlockViews.OpenPath(uri.LocalPath);
      }
    }
  }
}
