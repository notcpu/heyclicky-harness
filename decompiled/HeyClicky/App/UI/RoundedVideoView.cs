using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.Media.Playback;

namespace HeyClicky.App.UI
{
  // Token: 0x020000B8 RID: 184
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class RoundedVideoView : Grid
  {
    // Token: 0x0600077A RID: 1914 RVA: 0x005389E4 File Offset: 0x005389E4
    public RoundedVideoView(double width, double height, double cornerRadius)
    {
      base.Width = width;
      base.Height = height;
      base.IsHitTestVisible = false;
      base.Background = Ui.Brush(Colors.Black);
      this.dq = DispatcherQueue.GetForCurrentThread();
      base.Children.Add(this.image);
      Ui.ClipRounded(this, cornerRadius);
    }

    // Token: 0x0600077B RID: 1915 RVA: 0x00538A68 File Offset: 0x00538A68
    [NullableContext(2)]
    public void Attach(MediaPlayer mp)
    {
      MediaPlayer mediaPlayer = this.player;
      if (mediaPlayer != null)
      {
        mediaPlayer.VideoFrameAvailable -= this.OnFrame;
        try
        {
          mediaPlayer.IsVideoFrameServerEnabled = false;
        }
        catch
        {
        }
      }
      this.player = mp;
      if (mp == null)
      {
        return;
      }
      mp.IsVideoFrameServerEnabled = true;
      mp.VideoFrameAvailable += this.OnFrame;
    }

    // Token: 0x0600077C RID: 1916 RVA: 0x00538AD4 File Offset: 0x00538AD4
    private void OnFrame(MediaPlayer sender, object args)
    {
      if (Interlocked.Exchange(ref this.pending, 1) == 1)
      {
        return;
      }
      this.dq.TryEnqueue(delegate
      {
        Interlocked.Exchange(ref this.pending, 0);
        if (sender == this.player)
        {
          this.Draw(sender);
        }
      });
    }

    // Token: 0x0600077D RID: 1917 RVA: 0x00538B20 File Offset: 0x00538B20
    private void Draw(MediaPlayer mp)
    {
      try
      {
        CanvasDevice sharedDevice = CanvasDevice.GetSharedDevice();
        XamlRoot xamlRoot = base.XamlRoot;
        this.scale = ((xamlRoot != null) ? xamlRoot.RasterizationScale : this.scale);
        int num = Math.Max(1, (int)Math.Round(base.Width * this.scale));
        int num2 = Math.Max(1, (int)Math.Round(base.Height * this.scale));
        if (this.target == null || Math.Abs(this.targetScale - this.scale) > 0.001)
        {
          this.target = new CanvasImageSource(sharedDevice, (float)base.Width, (float)base.Height, (float)(96.0 * this.scale));
          this.targetScale = this.scale;
          this.image.Source = this.target;
        }
        MediaPlaybackSession playbackSession = mp.PlaybackSession;
        double num3 = playbackSession.NaturalVideoWidth;
        double num4 = playbackSession.NaturalVideoHeight;
        double num5 = ((num3 > 0.0 && num4 > 0.0) ? Math.Max((double)num / num3, (double)num2 / num4) : 1.0);
        int num6 = ((num3 > 0.0) ? Math.Max(num, (int)Math.Ceiling(num3 * num5)) : num);
        int num7 = ((num4 > 0.0) ? Math.Max(num2, (int)Math.Ceiling(num4 * num5)) : num2);
        if (this.frame == null || this.frame.SizeInPixels.Width != (uint)num6 || this.frame.SizeInPixels.Height != (uint)num7)
        {
          this.frame = new CanvasRenderTarget(sharedDevice, (float)num6, (float)num7, 96f);
        }
        mp.CopyFrameToVideoSurface(this.frame);
        using (CanvasDrawingSession canvasDrawingSession = this.target.CreateDrawingSession(Colors.Black))
        {
          canvasDrawingSession.DrawImage(this.frame, new Rect(0.0, 0.0, base.Width, base.Height), new Rect((double)(num6 - num) / 2.0, (double)(num7 - num2) / 2.0, (double)num, (double)num2));
        }
      }
      catch (Exception ex) when (CanvasDevice.GetSharedDevice().IsDeviceLost(ex.HResult))
      {
        this.target = null;
        this.frame = null;
      }
      catch (Exception ex2)
      {
        if (!this.warned)
        {
          this.warned = true;
          Log.Warn("video", "frame: " + ex2.Message);
        }
      }
    }

    // Token: 0x0400023C RID: 572
    private readonly Image image = new Image
    {
      Stretch = Stretch.Fill,
      IsHitTestVisible = false
    };

    // Token: 0x0400023D RID: 573
    private readonly DispatcherQueue dq;

    // Token: 0x0400023E RID: 574
    [Nullable(2)]
    private MediaPlayer player;

    // Token: 0x0400023F RID: 575
    [Nullable(2)]
    private CanvasImageSource target;

    // Token: 0x04000240 RID: 576
    [Nullable(2)]
    private CanvasRenderTarget frame;

    // Token: 0x04000241 RID: 577
    private double scale = 1.0;

    // Token: 0x04000242 RID: 578
    private double targetScale;

    // Token: 0x04000243 RID: 579
    private int pending;

    // Token: 0x04000244 RID: 580
    private bool warned;
  }
}
