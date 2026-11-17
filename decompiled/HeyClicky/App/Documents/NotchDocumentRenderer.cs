using System;
using System.IO;
using System.Runtime.CompilerServices;
using HeyClicky.App.Notch.Peek;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002DB RID: 731
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchDocumentRenderer : INotchDocumentRenderer
  {
    // Token: 0x06001ED1 RID: 7889 RVA: 0x00609C34 File Offset: 0x00609C34
    public static void Register()
    {
      NotchExtensions.RegisterDocumentRenderer(new NotchDocumentRenderer());
    }

    // Token: 0x06001ED2 RID: 7890 RVA: 0x00609C40 File Offset: 0x00609C40
    public bool CanRender(string path)
    {
      return DocumentViewers.HasViewer(path);
    }

    // Token: 0x06001ED3 RID: 7891 RVA: 0x00609C48 File Offset: 0x00609C48
    public FrameworkElement Render(string path, Action reload)
    {
      NotchDocumentRenderer.<>c__DisplayClass2_0 CS$<>8__locals1 = new NotchDocumentRenderer.<>c__DisplayClass2_0();
      CS$<>8__locals1.path = path;
      CS$<>8__locals1.reload = reload;
      FrameworkElement frameworkElement = DocumentViewers.CreateViewer(CS$<>8__locals1.path, new Func<FrameworkElement>(CS$<>8__locals1.<Render>g__Fallback|0)) ?? CS$<>8__locals1.<Render>g__Fallback|0();
      if (DocumentViewers.IsSpreadsheet(CS$<>8__locals1.path))
      {
        NotchDocumentRenderer.WatchForRewrites(frameworkElement, CS$<>8__locals1.path, CS$<>8__locals1.reload);
      }
      return frameworkElement;
    }

    // Token: 0x06001ED4 RID: 7892 RVA: 0x00609CAC File Offset: 0x00609CAC
    private static void WatchForRewrites(FrameworkElement viewer, string path, Action reload)
    {
      DispatcherQueue dq = DispatcherQueue.GetForCurrentThread();
      FileSystemWatcher watcher = null;
      DispatcherQueueTimer timer = null;
      try
      {
        string directoryName = Path.GetDirectoryName(path);
        if (directoryName == null || !Directory.Exists(directoryName))
        {
          return;
        }
        watcher = new FileSystemWatcher(directoryName, Path.GetFileName(path))
        {
          NotifyFilter = (NotifyFilters.FileName | NotifyFilters.Size | NotifyFilters.LastWrite),
          EnableRaisingEvents = true
        };
        watcher.Changed += delegate(object _, FileSystemEventArgs _)
        {
          base.<WatchForRewrites>g__Changed|1();
        };
        watcher.Renamed += delegate(object _, RenamedEventArgs _)
        {
          base.<WatchForRewrites>g__Changed|1();
        };
        watcher.Created += delegate(object _, FileSystemEventArgs _)
        {
          base.<WatchForRewrites>g__Changed|1();
        };
      }
      catch (Exception ex)
      {
        Log.Warn("notch-doc", "watch " + Path.GetFileName(path) + ": " + ex.Message);
      }
      viewer.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        FileSystemWatcher watcher2 = watcher;
        if (watcher2 != null)
        {
          watcher2.Dispose();
        }
        watcher = null;
        DispatcherQueueTimer timer2 = timer;
        if (timer2 == null)
        {
          return;
        }
        timer2.Stop();
      };
    }
  }
}
