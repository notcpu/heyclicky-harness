using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Documents.Spreadsheet;
using Microsoft.UI.Xaml;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002C9 RID: 713
  [NullableContext(1)]
  [Nullable(0)]
  public static class DocumentViewers
  {
    // Token: 0x06001E0D RID: 7693 RVA: 0x00605B4C File Offset: 0x00605B4C
    public static bool IsMarkdown(string path)
    {
      return DocumentViewers.MarkdownExtensions.Contains(Path.GetExtension(path).TrimStart('.'));
    }

    // Token: 0x06001E0E RID: 7694 RVA: 0x00605B68 File Offset: 0x00605B68
    public static bool IsSpreadsheet(string path)
    {
      return SpreadsheetWorkbook.CanRead(path);
    }

    // Token: 0x06001E0F RID: 7695 RVA: 0x00605B70 File Offset: 0x00605B70
    public static bool HasViewer(string path)
    {
      return DocumentViewers.IsMarkdown(path) || DocumentViewers.IsSpreadsheet(path);
    }

    // Token: 0x06001E10 RID: 7696 RVA: 0x00605B84 File Offset: 0x00605B84
    [return: Nullable(2)]
    public static FrameworkElement CreateViewer(string path, [Nullable(new byte[] { 2, 1 })] Func<FrameworkElement> fallback = null)
    {
      FrameworkElement frameworkElement = null;
      if (DocumentViewers.IsMarkdown(path))
      {
        DocumentViewers.CreateMarkdownViewer(path, ref frameworkElement);
      }
      else if (DocumentViewers.IsSpreadsheet(path))
      {
        DocumentViewers.CreateSpreadsheetViewer(path, fallback, ref frameworkElement);
      }
      return frameworkElement;
    }

    // Token: 0x06001E11 RID: 7697 RVA: 0x00605BB8 File Offset: 0x00605BB8
    private static void CreateMarkdownViewer(string path, [Nullable(2)] ref FrameworkElement viewer)
    {
      viewer = new MarkdownViewerContent(path);
    }

    // Token: 0x06001E12 RID: 7698 RVA: 0x00605BC4 File Offset: 0x00605BC4
    private static void CreateSpreadsheetViewer(string path, [Nullable(new byte[] { 2, 1 })] Func<FrameworkElement> fallback, [Nullable(2)] ref FrameworkElement viewer)
    {
      viewer = new SpreadsheetDocumentView(path, fallback);
    }

    // Token: 0x04000E79 RID: 3705
    private static readonly HashSet<string> MarkdownExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "md", "markdown", "mdown", "mkd", "mkdn" };
  }
}
