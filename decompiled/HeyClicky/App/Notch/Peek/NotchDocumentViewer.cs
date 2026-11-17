using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200017F RID: 383
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchDocumentViewer : Grid
  {
    // Token: 0x060014E1 RID: 5345 RVA: 0x005B247C File Offset: 0x005B247C
    public NotchDocumentViewer(INotchHost host, bool showsFollowUp = true)
    {
      this.host = host;
      this.showsFollowUp = showsFollowUp;
      this.dq = DispatcherQueue.GetForCurrentThread();
      base.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      base.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      this.pane = new Border
      {
        Background = Ui.Brush(this.look.SidebarFill),
        CornerRadius = new CornerRadius(12.0),
        BorderBrush = Ui.Brush(this.look.InkAt(0.08)),
        BorderThickness = new Thickness(1.0),
        Margin = new Thickness(4.0, 6.0, 4.0, 4.0)
      };
      base.Children.Add(this.pane);
      Grid.SetRow(this.footer, 1);
      base.Children.Add(this.footer);
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        this.StopWatching();
      };
    }

    // Token: 0x060014E2 RID: 5346 RVA: 0x005B25D8 File Offset: 0x005B25D8
    [NullableContext(2)]
    public void Show(string path, string threadId = null)
    {
      NotchDocumentViewer.<>c__DisplayClass12_0 CS$<>8__locals1 = new NotchDocumentViewer.<>c__DisplayClass12_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.path = path;
      if (CS$<>8__locals1.path == this.shownPath)
      {
        return;
      }
      this.shownPath = CS$<>8__locals1.path;
      NotchDocumentViewer.<>c__DisplayClass12_0 CS$<>8__locals2 = CS$<>8__locals1;
      int num = this.version + 1;
      this.version = num;
      CS$<>8__locals2.v = num;
      this.pane.Child = null;
      this.StopWatching();
      this.ShowFollowUpFooter(CS$<>8__locals1.path, threadId);
      if (CS$<>8__locals1.path == null)
      {
        return;
      }
      DispatcherQueueTimer dispatcherQueueTimer = this.mountTimer;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      this.mountTimer = this.dq.CreateTimer().Rooted();
      this.mountTimer.Interval = NotchPeekLayout.DocumentMountDelay;
      this.mountTimer.IsRepeating = false;
      this.mountTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        if (CS$<>8__locals1.v == CS$<>8__locals1.<>4__this.version)
        {
          CS$<>8__locals1.<>4__this.Mount(CS$<>8__locals1.path, CS$<>8__locals1.v);
        }
      };
      this.mountTimer.Start();
    }

    // Token: 0x060014E3 RID: 5347 RVA: 0x005B26C0 File Offset: 0x005B26C0
    private void Mount(string path, int v)
    {
      if (v != this.version)
      {
        return;
      }
      using (MessagesPalette.Use(this.look))
      {
        INotchDocumentRenderer notchDocumentRenderer = NotchExtensions.DocumentRenderers.FirstOrDefault<INotchDocumentRenderer>((INotchDocumentRenderer r) => r.CanRender(path));
        INotchDocumentRenderer notchDocumentRenderer2 = notchDocumentRenderer ?? NotchDocumentRenderers.For(path);
        try
        {
          DispatcherQueueHandler <>9__2;
          this.pane.Child = notchDocumentRenderer2.Render(path, delegate
          {
            DispatcherQueue dispatcherQueue = this.dq;
            DispatcherQueueHandler dispatcherQueueHandler;
            if ((dispatcherQueueHandler = <>9__2) == null)
            {
              dispatcherQueueHandler = (<>9__2 = delegate
              {
                if (v == this.version)
                {
                  this.Mount(path, v);
                }
              });
            }
            dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
          });
        }
        catch (Exception ex)
        {
          Log.Warn("notch-doc", "render " + Path.GetFileName(path) + " failed: " + ex.Message);
          this.pane.Child = NotchDocumentRenderers.Message("This document couldn’t be loaded.");
        }
        Ui.ClipRounded(this.pane, 12.0);
        if (notchDocumentRenderer == null)
        {
          this.Watch(path, v);
        }
        string text = "notch-doc";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 2);
        defaultInterpolatedStringHandler.AppendLiteral("showing ");
        defaultInterpolatedStringHandler.AppendFormatted(Path.GetFileName(path));
        defaultInterpolatedStringHandler.AppendLiteral(" (");
        defaultInterpolatedStringHandler.AppendFormatted(ChatBlockViews.Kind(path));
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      }
    }

    // Token: 0x060014E4 RID: 5348 RVA: 0x005B2864 File Offset: 0x005B2864
    [NullableContext(2)]
    private void ShowFollowUpFooter(string path, string threadId)
    {
      this.footer.Children.Clear();
      if (this.showsFollowUp && path != null)
      {
        ChatHandOff chatHandOff = NotchDocumentViewer.ReplyEntry(path, threadId);
        if (chatHandOff != null)
        {
          this.footer.Children.Add(new NotchChatComposer(this.host, chatHandOff.Id, false, null, null));
          return;
        }
      }
    }

    // Token: 0x060014E5 RID: 5349 RVA: 0x005B28BC File Offset: 0x005B28BC
    [NullableContext(2)]
    internal static ChatHandOff ReplyEntry([Nullable(1)] string path, string threadId)
    {
      NotchDocumentViewer.<>c__DisplayClass15_0 CS$<>8__locals1 = new NotchDocumentViewer.<>c__DisplayClass15_0();
      CS$<>8__locals1.threadId = threadId;
      ChatHandOffs handOffs = NotchChat.HandOffs;
      if (handOffs == null)
      {
        return null;
      }
      try
      {
        CS$<>8__locals1.documentPath = Path.GetFullPath(path);
      }
      catch
      {
        return null;
      }
      List<ChatHandOff> list = handOffs.Entries.Where<ChatHandOff>((ChatHandOff e) => e.Kind == ChatHandOffKind.CameBack && e.Outcome.GetValueOrDefault() == ChatHandOffOutcome.Finished).Reverse<ChatHandOff>().ToList<ChatHandOff>();
      ChatHandOff chatHandOff = list.FirstOrDefault<ChatHandOff>(new Func<ChatHandOff, bool>(CS$<>8__locals1.<ReplyEntry>g__Made|1));
      if (chatHandOff != null)
      {
        return chatHandOff;
      }
      if (CS$<>8__locals1.threadId == null)
      {
        return null;
      }
      NotchDocumentViewer.<>c__DisplayClass15_0 CS$<>8__locals2 = CS$<>8__locals1;
      IChatHandOffDirectory directory = handOffs.Directory;
      ValueTuple<string, string>? valueTuple;
      CS$<>8__locals2.jobId = ((directory != null) ? ((directory.JobForThread(CS$<>8__locals1.threadId) != null) ? valueTuple.GetValueOrDefault().Item1 : null) : null);
      return list.FirstOrDefault<ChatHandOff>((ChatHandOff e) => e.ThreadId == CS$<>8__locals1.threadId || (CS$<>8__locals1.jobId != null && e.JobId == CS$<>8__locals1.jobId));
    }

    // Token: 0x060014E6 RID: 5350 RVA: 0x005B29B0 File Offset: 0x005B29B0
    private void Watch(string path, int v)
    {
      try
      {
        string directoryName = Path.GetDirectoryName(path);
        if (directoryName != null && Directory.Exists(directoryName))
        {
          this.watcher = new FileSystemWatcher(directoryName, Path.GetFileName(path))
          {
            NotifyFilter = (NotifyFilters.Size | NotifyFilters.LastWrite),
            EnableRaisingEvents = true
          };
          TypedEventHandler<DispatcherQueueTimer, object> <>9__2;
          DispatcherQueueHandler <>9__1;
          this.watcher.Changed += delegate(object _, FileSystemEventArgs _)
          {
            DispatcherQueue dispatcherQueue = this.dq;
            DispatcherQueueHandler dispatcherQueueHandler;
            if ((dispatcherQueueHandler = <>9__1) == null)
            {
              dispatcherQueueHandler = (<>9__1 = delegate
              {
                if (v != this.version)
                {
                  return;
                }
                DispatcherQueueTimer dispatcherQueueTimer = this.reloadTimer;
                if (dispatcherQueueTimer != null)
                {
                  dispatcherQueueTimer.Stop();
                }
                this.reloadTimer = this.dq.CreateTimer().Rooted();
                this.reloadTimer.Interval = TimeSpan.FromMilliseconds(300L, 0L);
                this.reloadTimer.IsRepeating = false;
                DispatcherQueueTimer dispatcherQueueTimer2 = this.reloadTimer;
                TypedEventHandler<DispatcherQueueTimer, object> typedEventHandler;
                if ((typedEventHandler = <>9__2) == null)
                {
                  typedEventHandler = (<>9__2 = delegate(DispatcherQueueTimer _, object _)
                  {
                    if (v == this.version)
                    {
                      this.Mount(path, v);
                    }
                  });
                }
                dispatcherQueueTimer2.Tick += typedEventHandler;
                this.reloadTimer.Start();
              });
            }
            dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
          };
        }
      }
      catch (Exception ex)
      {
        Log.Warn("notch-doc", "watch: " + ex.Message);
      }
    }

    // Token: 0x060014E7 RID: 5351 RVA: 0x005B2A60 File Offset: 0x005B2A60
    private void StopWatching()
    {
      FileSystemWatcher fileSystemWatcher = this.watcher;
      if (fileSystemWatcher != null)
      {
        fileSystemWatcher.Dispose();
      }
      this.watcher = null;
    }

    // Token: 0x04000A14 RID: 2580
    private readonly Border pane;

    // Token: 0x04000A15 RID: 2581
    private readonly Grid footer = new Grid();

    // Token: 0x04000A16 RID: 2582
    private readonly INotchHost host;

    // Token: 0x04000A17 RID: 2583
    private readonly DispatcherQueue dq;

    // Token: 0x04000A18 RID: 2584
    [Nullable(2)]
    private FileSystemWatcher watcher;

    // Token: 0x04000A19 RID: 2585
    [Nullable(2)]
    private DispatcherQueueTimer mountTimer;

    // Token: 0x04000A1A RID: 2586
    [Nullable(2)]
    private DispatcherQueueTimer reloadTimer;

    // Token: 0x04000A1B RID: 2587
    [Nullable(2)]
    private string shownPath;

    // Token: 0x04000A1C RID: 2588
    private int version;

    // Token: 0x04000A1D RID: 2589
    private readonly bool showsFollowUp;

    // Token: 0x04000A1E RID: 2590
    private readonly MessagesPalette look = MessagesPalette.Current;
  }
}
