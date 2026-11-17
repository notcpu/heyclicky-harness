using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002CF RID: 719
  public sealed class MarkdownViewerFileWatcher : IDisposable
  {
    // Token: 0x06001E41 RID: 7745 RVA: 0x0060692C File Offset: 0x0060692C
    [NullableContext(1)]
    public MarkdownViewerFileWatcher(string path, Action onFileChanged)
    {
      MarkdownViewerFileWatcher.<>c__DisplayClass2_0 CS$<>8__locals1 = new MarkdownViewerFileWatcher.<>c__DisplayClass2_0();
      CS$<>8__locals1.onFileChanged = onFileChanged;
      base..ctor();
      CS$<>8__locals1.<>4__this = this;
      DispatcherQueue dq = DispatcherQueue.GetForCurrentThread();
      string directoryName = Path.GetDirectoryName(path);
      string name = Path.GetFileName(path);
      if (dq == null || directoryName == null || !Directory.Exists(directoryName))
      {
        return;
      }
      this.debounce = dq.CreateTimer();
      this.debounce.Interval = TimeSpan.FromSeconds(0.15);
      this.debounce.IsRepeating = false;
      this.debounce.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        CS$<>8__locals1.onFileChanged();
      };
      try
      {
        this.watcher = new FileSystemWatcher(directoryName)
        {
          NotifyFilter = (NotifyFilters.FileName | NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.CreationTime),
          IncludeSubdirectories = false
        };
        this.watcher.Changed += delegate(object _, FileSystemEventArgs e)
        {
          if (base.<.ctor>g__Matches|2(e.Name))
          {
            base.<.ctor>g__Schedule|1();
          }
        };
        this.watcher.Created += delegate(object _, FileSystemEventArgs e)
        {
          if (base.<.ctor>g__Matches|2(e.Name))
          {
            base.<.ctor>g__Schedule|1();
          }
        };
        this.watcher.Deleted += delegate(object _, FileSystemEventArgs e)
        {
          if (base.<.ctor>g__Matches|2(e.Name))
          {
            base.<.ctor>g__Schedule|1();
          }
        };
        this.watcher.Renamed += delegate(object _, RenamedEventArgs e)
        {
          if (base.<.ctor>g__Matches|2(e.Name) || base.<.ctor>g__Matches|2(e.OldName))
          {
            base.<.ctor>g__Schedule|1();
          }
        };
        this.watcher.EnableRaisingEvents = true;
      }
      catch (Exception ex)
      {
        Log.Warn("markdown", "watch " + name + ": " + ex.Message);
      }
    }

    // Token: 0x06001E42 RID: 7746 RVA: 0x00606AA4 File Offset: 0x00606AA4
    public void Dispose()
    {
      FileSystemWatcher fileSystemWatcher = this.watcher;
      if (fileSystemWatcher != null)
      {
        fileSystemWatcher.Dispose();
      }
      DispatcherQueueTimer dispatcherQueueTimer = this.debounce;
      if (dispatcherQueueTimer == null)
      {
        return;
      }
      dispatcherQueueTimer.Stop();
    }

    // Token: 0x04000E99 RID: 3737
    [Nullable(2)]
    private readonly FileSystemWatcher watcher;

    // Token: 0x04000E9A RID: 3738
    [Nullable(2)]
    private readonly DispatcherQueueTimer debounce;
  }
}
