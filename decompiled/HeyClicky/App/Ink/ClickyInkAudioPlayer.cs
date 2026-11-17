using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Dispatching;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000244 RID: 580
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ClickyInkAudioPlayer
  {
    // Token: 0x17000484 RID: 1156
    // (get) Token: 0x060019E7 RID: 6631 RVA: 0x005DBB6C File Offset: 0x005DBB6C
    [Nullable(1)]
    public static ClickyInkAudioPlayer Shared
    {
      [NullableContext(1)]
      get;
    } = new ClickyInkAudioPlayer();

    // Token: 0x17000485 RID: 1157
    // (get) Token: 0x060019E8 RID: 6632 RVA: 0x005DBB74 File Offset: 0x005DBB74
    // (set) Token: 0x060019E9 RID: 6633 RVA: 0x005DBB7C File Offset: 0x005DBB7C
    public string PlayingPath { get; private set; }

    // Token: 0x14000064 RID: 100
    // (add) Token: 0x060019EA RID: 6634 RVA: 0x005DBB88 File Offset: 0x005DBB88
    // (remove) Token: 0x060019EB RID: 6635 RVA: 0x005DBBC0 File Offset: 0x005DBBC0
    public event Action Changed;

    // Token: 0x060019EC RID: 6636 RVA: 0x005DBBF8 File Offset: 0x005DBBF8
    [NullableContext(1)]
    public void Toggle(string path)
    {
      if (this.PlayingPath == path)
      {
        this.Stop();
        return;
      }
      this.Stop();
      try
      {
        MediaPlayer mediaPlayer = new MediaPlayer
        {
          Source = MediaSource.CreateFromUri(new Uri(path))
        };
        mediaPlayer.MediaEnded += delegate(MediaPlayer _, object _)
        {
          DispatcherQueue dispatcherQueue = this.dispatcher;
          if (dispatcherQueue == null)
          {
            return;
          }
          dispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.Stop));
        };
        mediaPlayer.MediaFailed += delegate(MediaPlayer _, MediaPlayerFailedEventArgs e)
        {
          Log.Warn("ink", "note gallery sound failed: " + e.ErrorMessage);
          DispatcherQueue dispatcherQueue2 = this.dispatcher;
          if (dispatcherQueue2 == null)
          {
            return;
          }
          dispatcherQueue2.TryEnqueue(new DispatcherQueueHandler(this.Stop));
        };
        this.player = mediaPlayer;
        this.PlayingPath = path;
        Action changed = this.Changed;
        if (changed != null)
        {
          changed();
        }
        mediaPlayer.Play();
      }
      catch (Exception ex)
      {
        Log.Warn("ink", "note gallery sound: " + ex.Message);
        this.player = null;
        this.PlayingPath = null;
      }
    }

    // Token: 0x060019ED RID: 6637 RVA: 0x005DBCC0 File Offset: 0x005DBCC0
    public void Stop()
    {
      MediaPlayer mediaPlayer = this.player;
      if (mediaPlayer != null)
      {
        try
        {
          mediaPlayer.Pause();
          mediaPlayer.Dispose();
        }
        catch
        {
        }
      }
      this.player = null;
      if (this.PlayingPath == null)
      {
        return;
      }
      this.PlayingPath = null;
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x04000C7B RID: 3195
    private MediaPlayer player;

    // Token: 0x04000C7C RID: 3196
    private readonly DispatcherQueue dispatcher = DispatcherQueue.GetForCurrentThread();
  }
}
