using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.App.Interop;
using Microsoft.UI;
using Microsoft.UI.Content;
using Microsoft.UI.Xaml;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000251 RID: 593
  public static class ClickyInkFilePicker
  {
    // Token: 0x06001A31 RID: 6705 RVA: 0x005DDA60 File Offset: 0x005DDA60
    [NullableContext(1)]
    [return: Nullable(new byte[] { 1, 2 })]
    public static Task<string> ChooseAsync(FrameworkElement anchor, string message, bool folder = false)
    {
      Task<string> task;
      try
      {
        IntPtr intPtr = (IntPtr)0;
        XamlRoot xamlRoot = anchor.XamlRoot;
        ContentIslandEnvironment contentIslandEnvironment = ((xamlRoot != null) ? xamlRoot.ContentIslandEnvironment : null);
        if (contentIslandEnvironment != null)
        {
          intPtr = Win32Interop.GetWindowFromWindowId(contentIslandEnvironment.AppWindowId);
        }
        string text = (string.IsNullOrWhiteSpace(message) ? (folder ? "Choose a folder" : "Choose a file") : message.Trim());
        task = Task.FromResult<string>(FolderDialog.Pick(intPtr, text, "Choose", null, folder));
      }
      catch (Exception ex)
      {
        Log.Warn("ink", "file picker (" + message + "): " + ex.Message);
        task = Task.FromResult<string>(null);
      }
      return task;
    }
  }
}
