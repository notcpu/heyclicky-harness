using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Documents;
using HeyClicky.Core.Voice;
using HeyClicky.Platform.Win32;

namespace HeyClicky.App.Wiring
{
  // Token: 0x0200009F RID: 159
  public sealed class WindowsPageReader : IPageReader
  {
    // Token: 0x060006A9 RID: 1705 RVA: 0x00531C6C File Offset: 0x00531C6C
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<PageReference> CaptureReferenceAsync(CancellationToken ct = default(CancellationToken))
    {
      ForegroundApp foregroundApp = await ForegroundAppInfo.CaptureUserAppAsync(true, ct).ConfigureAwait(false);
      PageReference pageReference;
      if (foregroundApp == null || foregroundApp.ProcessId == Environment.ProcessId)
      {
        pageReference = null;
      }
      else if (ForegroundAppInfo.IsBrowser(foregroundApp.ProcessName))
      {
        string text = WindowsPageReader.WebAddress(foregroundApp.BrowserUrl);
        Uri uri;
        if (text == null)
        {
          pageReference = null;
        }
        else if (text.StartsWith("file:", StringComparison.OrdinalIgnoreCase) && Uri.TryCreate(text, UriKind.Absolute, out uri) && File.Exists(uri.LocalPath))
        {
          pageReference = new PageReference("file", uri.LocalPath, foregroundApp.ProcessId, foregroundApp.WindowHandle);
        }
        else
        {
          pageReference = (text.StartsWith("http", StringComparison.OrdinalIgnoreCase) ? new PageReference("web_page", text, foregroundApp.ProcessId, foregroundApp.WindowHandle) : null);
        }
      }
      else
      {
        string text2 = RecentFiles.FileNameFromTitle(foregroundApp.WindowTitle);
        if (text2 == null)
        {
          pageReference = null;
        }
        else
        {
          string text3 = RecentFiles.Resolve(text2);
          pageReference = new PageReference("file", text3 ?? text2, foregroundApp.ProcessId, foregroundApp.WindowHandle);
        }
      }
      return pageReference;
    }

    // Token: 0x060006AA RID: 1706 RVA: 0x00531CB0 File Offset: 0x00531CB0
    [NullableContext(2)]
    internal static string WebAddress(string shown)
    {
      string text = ((shown != null) ? shown.Trim() : null);
      if (string.IsNullOrEmpty(text))
      {
        return null;
      }
      bool flag = text.Length > 3 && char.IsAsciiLetter(text[0]) && text[1] == ':';
      if (flag)
      {
        char c = text[2];
        bool flag2 = c == '/' || c == '\\';
        flag = flag2;
      }
      if (flag)
      {
        return new Uri(text.Replace('/', '\\')).AbsoluteUri;
      }
      if (text.Contains(' '))
      {
        return null;
      }
      if (text.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || text.StartsWith("https://", StringComparison.OrdinalIgnoreCase) || text.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
      {
        return text;
      }
      if (text.Contains("://", StringComparison.Ordinal) || text.StartsWith("about:", StringComparison.OrdinalIgnoreCase))
      {
        return null;
      }
      if (!text.Contains('.') && !text.StartsWith("localhost", StringComparison.OrdinalIgnoreCase))
      {
        return null;
      }
      return "https://" + text;
    }

    // Token: 0x060006AB RID: 1707 RVA: 0x00531DAC File Offset: 0x00531DAC
    [NullableContext(1)]
    public async Task<PageContext> ExtractAsync(PageReference reference, CancellationToken ct = default(CancellationToken))
    {
      PageContext pageContext;
      if (reference.IsWebPage)
      {
        UiaPageText.PageText pageText = await UiaPageText.ReadAsync(reference.WindowHandle, 60000, ct).ConfigureAwait(false);
        pageContext = new PageContext("web_page", reference.Reference, (pageText != null) ? pageText.Text : null, pageText != null && pageText.IsTruncated, null, false);
      }
      else
      {
        PageContext fromFile = null;
        if (Path.IsPathFullyQualified(reference.Reference) && File.Exists(reference.Reference))
        {
          fromFile = await Task.Run<PageContext>(() => DocumentText.Extract(reference.Reference, 60000), ct).ConfigureAwait(false);
          if (fromFile.AccessDenied || !string.IsNullOrEmpty(fromFile.Text))
          {
            return fromFile;
          }
        }
        bool flag = reference.WindowHandle > (IntPtr)0;
        UiaPageText.PageText shown;
        if (flag)
        {
          shown = await UiaPageText.ReadAsync(reference.WindowHandle, 60000, ct).ConfigureAwait(false);
          flag = shown != null;
        }
        if (flag)
        {
          PageContext pageContext2 = fromFile;
          string text = ((((pageContext2 != null) ? pageContext2.Kind : null) == "pdf") ? "pdf" : "text_file");
          string reference2 = reference.Reference;
          string text2 = shown.Text;
          bool isTruncated = shown.IsTruncated;
          PageContext pageContext3 = fromFile;
          pageContext = new PageContext(text, reference2, text2, isTruncated, (pageContext3 != null) ? pageContext3.PageCount : null, false);
        }
        else
        {
          pageContext = fromFile ?? PageContext.ReferenceOnly(reference);
        }
      }
      return pageContext;
    }
  }
}
