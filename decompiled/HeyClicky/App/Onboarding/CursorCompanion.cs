using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using HeyClicky.App.Design;
using HeyClicky.App.Interop;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using HeyClicky.Core.Telemetry;
using HeyClicky.Platform.Audio;
using HeyClicky.Platform.Overlay;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x0200011A RID: 282
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CursorCompanion
  {
    // Token: 0x06000F0E RID: 3854 RVA: 0x00577B20 File Offset: 0x00577B20
    public CursorCompanion(bool excludeFromCapture, Func<string> cursorColorHex)
    {
      this.tint = cursorColorHex;
      this.dq = DispatcherQueue.GetForCurrentThread();
      this.window = new OverlayWindow("HeyClicky Cursor", excludeFromCapture, true, false, true, false, false);
      NonRudeWindow.Mark(this.window.Hwnd);
      this.window.Root.Children.Add(this.stage);
      this.stage.HorizontalAlignment = HorizontalAlignment.Left;
      this.stage.VerticalAlignment = VerticalAlignment.Top;
      ShadowLayer shadowLayer = new ShadowLayer(10.0, DS.Black(0.4), 12.0, 6.0, 0.0);
      this.videoCard.Children.Add(shadowLayer);
      Canvas.SetLeft(this.videoCard, 10.0);
      Canvas.SetTop(this.videoCard, 18.0);
      this.stage.Children.Add(this.videoCard);
      this.countdown = new TextBlock
      {
        FontSize = 11.0,
        FontWeight = FontWeights.Medium,
        Foreground = Ui.Brush(DS.White(0.85)),
        Width = 330.0,
        TextAlignment = TextAlignment.Right,
        IsTextScaleFactorEnabled = false
      };
      this.countdownHost = new Grid
      {
        Opacity = 0.0,
        IsHitTestVisible = false
      };
      this.countdownHost.Children.Add(CursorCompanion.TextShadowHost(this.countdown));
      this.countdownHost.Children.Add(this.countdown);
      Canvas.SetLeft(this.countdownHost, 10.0);
      Canvas.SetTop(this.countdownHost, 210.0);
      this.stage.Children.Add(this.countdownHost);
      this.frame = this.dq.CreateTimer();
      this.frame.Interval = TimeSpan.FromMilliseconds(15L, 0L);
      this.frame.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        if (!this.following)
        {
          this.EscapeWatch();
        }
      };
    }

    // Token: 0x06000F0F RID: 3855 RVA: 0x00577E24 File Offset: 0x00577E24
    private static Grid TextShadowHost(TextBlock text)
    {
      Grid shadowHost = new Grid();
      text.Loaded += delegate(object _, RoutedEventArgs _)
      {
        Compositor compositor = Ui.Compositor;
        DropShadow dropShadow = compositor.CreateDropShadow();
        dropShadow.Color = DS.Black(0.6);
        dropShadow.BlurRadius = 8f;
        dropShadow.Offset = new Vector3(0f, 1f, 0f);
        dropShadow.Mask = text.GetAlphaMask();
        SpriteVisual spriteVisual = compositor.CreateSpriteVisual();
        spriteVisual.Shadow = dropShadow;
        spriteVisual.RelativeSizeAdjustment = Vector2.One;
        ElementCompositionPreview.SetElementChildVisual(shadowHost, spriteVisual);
      };
      return shadowHost;
    }

    // Token: 0x06000F10 RID: 3856 RVA: 0x00577E68 File Offset: 0x00577E68
    private void EnsureShown()
    {
      if (this.visible)
      {
        return;
      }
      this.visible = true;
      this.placed = false;
      this.windowMonitor = (IntPtr)0;
      this.lastTick = -1.0;
      this.lastAnchorMove = -1.0;
      this.Follow(double.NaN);
      this.window.ShowNoActivate();
      this.frame.Start();
      if (!this.following)
      {
        this.following = true;
        CompositionTarget.Rendering += this.OnRenderingFrame;
      }
    }

    // Token: 0x06000F11 RID: 3857 RVA: 0x00577EF8 File Offset: 0x00577EF8
    private void HideIfEmpty()
    {
      if (this.player != null)
      {
        return;
      }
      if (this.visible)
      {
        this.LogSmoothness();
      }
      this.visible = false;
      this.frame.Stop();
      if (this.following)
      {
        this.following = false;
        CompositionTarget.Rendering -= this.OnRenderingFrame;
      }
      this.window.HideWindow();
    }

    // Token: 0x06000F12 RID: 3858 RVA: 0x00577F5C File Offset: 0x00577F5C
    private void OnRenderingFrame([Nullable(2)] object sender, object e)
    {
      if (!this.visible)
      {
        return;
      }
      RenderingEventArgs e2 = e as RenderingEventArgs;
      if (e2 == null)
      {
        this.Follow(double.NaN);
        return;
      }
      double totalSeconds = e2.RenderingTime.TotalSeconds;
      if (totalSeconds == this.lastTick)
      {
        return;
      }
      this.Follow(totalSeconds);
    }

    // Token: 0x06000F13 RID: 3859 RVA: 0x00577FAC File Offset: 0x00577FAC
    private void Follow(double frameTime)
    {
      Native.POINT point;
      Native.GetCursorPos(out point);
      Displays.Monitor monitor = Displays.UnderCursor();
      double scale = monitor.Scale;
      double num = (double)point.X + 35.0 * scale;
      double num2 = (double)point.Y + 25.0 * scale;
      double num3 = 0.0;
      if (!double.IsNaN(frameTime))
      {
        if (this.lastTick >= 0.0)
        {
          num3 = Math.Max(0.0, frameTime - this.lastTick);
        }
        this.lastTick = frameTime;
      }
      double num4 = 330.0 * scale;
      double num5 = 214.0 * scale;
      CursorAdjacentPlacement.Placed placed = CursorAdjacentPlacement.Place(this.cardSides, num + 10.0 * scale, num2 + 18.0 * scale, (double)point.X - 12.0 * scale, (double)point.Y - 12.0 * scale, num4, num5, num4, num5, new RectD((double)monitor.Work.Left, (double)monitor.Work.Top, (double)monitor.Work.Width, (double)monitor.Work.Height), scale);
      if (placed.Sides != this.cardSides && this.placed)
      {
        this.statFlips++;
      }
      this.cardSides = placed.Sides;
      bool flag = monitor.Handle != this.windowMonitor || !CursorCompanion.SameRect(monitor.Bounds, this.windowBounds);
      if (flag)
      {
        this.windowMonitor = monitor.Handle;
        this.windowBounds = monitor.Bounds;
        this.window.PlacePx(monitor.Bounds.Left, monitor.Bounds.Top, monitor.Bounds.Width, monitor.Bounds.Height);
      }
      Vector3 vector = new Vector3((float)((Math.Round(num) - (double)monitor.Bounds.Left) / scale), (float)((Math.Round(num2) - (double)monitor.Bounds.Top) / scale), 0f);
      Vector3 vector2 = new Vector3((float)(Math.Round(placed.Left - 10.0 * scale - Math.Round(num)) / scale), (float)(Math.Round(placed.Top - 18.0 * scale - Math.Round(num2)) / scale), 0f);
      if (this.motion == null)
      {
        this.motion = new CompositorSpringMotion(this.stage);
      }
      if (!this.placed || flag || double.IsNaN(frameTime) || (vector - this.anchorRest).Length() > 1200f)
      {
        this.motion.Snap(vector + vector2);
        this.anchorRest = vector;
        this.offsetRest = vector2;
        this.placed = true;
      }
      else
      {
        if (vector != this.anchorRest && (this.lastAnchorMove < 0.0 || frameTime - this.lastAnchorMove >= 0.015) && this.motion.Move(vector - this.anchorRest, OverlayCursorSpring.CursorFollow, frameTime))
        {
          this.anchorRest = vector;
          this.lastAnchorMove = frameTime;
        }
        if (vector2 != this.offsetRest)
        {
          if (Anim.ReduceMotion)
          {
            this.motion.Jump(vector2 - this.offsetRest);
            this.offsetRest = vector2;
          }
          else if (this.motion.Move(vector2 - this.offsetRest, CursorCompanion.PlacementSpring, frameTime))
          {
            this.offsetRest = vector2;
          }
        }
      }
      if (num3 > 0.0)
      {
        this.statFrameMs.Add(num3 * 1000.0);
      }
      this.EscapeWatch();
    }

    // Token: 0x06000F14 RID: 3860 RVA: 0x005783A4 File Offset: 0x005783A4
    private void LogSmoothness()
    {
      if (this.statFrameMs.Count > 0)
      {
        CursorCompanion.<>c__DisplayClass39_0 CS$<>8__locals1;
        CS$<>8__locals1.sortedFrameMs = this.statFrameMs.OrderBy<double, double>((double frameMs) => frameMs).ToArray<double>();
        int num = CS$<>8__locals1.sortedFrameMs.Count<double>((double frameMs) => frameMs > 25.0);
        string text = "video";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(115, 8);
        defaultInterpolatedStringHandler.AppendLiteral("card motion (compositor): ");
        CompositorSpringMotion compositorSpringMotion = this.motion;
        defaultInterpolatedStringHandler.AppendFormatted<int>((compositorSpringMotion != null) ? compositorSpringMotion.Moves : 0);
        defaultInterpolatedStringHandler.AppendLiteral(" moves, ");
        CompositorSpringMotion compositorSpringMotion2 = this.motion;
        defaultInterpolatedStringHandler.AppendFormatted<int>((compositorSpringMotion2 != null) ? compositorSpringMotion2.SlotWaits : 0);
        defaultInterpolatedStringHandler.AppendLiteral(" slot waits, ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(this.statFlips);
        defaultInterpolatedStringHandler.AppendLiteral(" side flip(s); ");
        defaultInterpolatedStringHandler.AppendLiteral("UI frames ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.sortedFrameMs.Length);
        defaultInterpolatedStringHandler.AppendLiteral(", p50 ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(CursorCompanion.<LogSmoothness>g__Percentile|39_1(0.5, ref CS$<>8__locals1), "0.0");
        defaultInterpolatedStringHandler.AppendLiteral(" ms, p95 ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(CursorCompanion.<LogSmoothness>g__Percentile|39_1(0.95, ref CS$<>8__locals1), "0.0");
        defaultInterpolatedStringHandler.AppendLiteral(" ms, max ");
        double[] sortedFrameMs = CS$<>8__locals1.sortedFrameMs;
        defaultInterpolatedStringHandler.AppendFormatted<double>(sortedFrameMs[sortedFrameMs.Length - 1], "0.0");
        defaultInterpolatedStringHandler.AppendLiteral(" ms, ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num);
        defaultInterpolatedStringHandler.AppendLiteral(" late (>25 ms)");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      }
      this.statFrameMs.Clear();
      this.statFlips = 0;
      CompositorSpringMotion compositorSpringMotion3 = this.motion;
      if (compositorSpringMotion3 == null)
      {
        return;
      }
      compositorSpringMotion3.ResetCounts();
    }

    // Token: 0x06000F15 RID: 3861 RVA: 0x00578580 File Offset: 0x00578580
    private static bool SameRect(Native.RECT a, Native.RECT b)
    {
      return a.Left == b.Left && a.Top == b.Top && a.Right == b.Right && a.Bottom == b.Bottom;
    }

    // Token: 0x170002C3 RID: 707
    // (get) Token: 0x06000F16 RID: 3862 RVA: 0x005785BC File Offset: 0x005785BC
    public bool IsPlayingVideo
    {
      get
      {
        return this.player != null;
      }
    }

    // Token: 0x170002C4 RID: 708
    // (get) Token: 0x06000F17 RID: 3863 RVA: 0x005785CC File Offset: 0x005785CC
    internal OverlayWindow WindowForSnapshot
    {
      get
      {
        return this.window;
      }
    }

    // Token: 0x06000F18 RID: 3864 RVA: 0x005785D4 File Offset: 0x005785D4
    public bool PlayVideo(string path, IReadOnlyList<CursorCompanion.Cue> videoCues, Action ended, double audioRampSeconds = 2.0, double countdownFromSeconds = 60.0, double hintDelaySeconds = 5.0, bool onboardingAnalytics = true)
    {
      if (!File.Exists(path))
      {
        Log.Warn("video", "missing " + path);
        return false;
      }
      return this.PlayVideo(CursorCompanion.CreatePlayer(new Uri(path), false), "Assets/Video/" + Path.GetFileName(path), "bundled", videoCues, ended, null, audioRampSeconds, countdownFromSeconds, hintDelaySeconds, onboardingAnalytics);
    }

    // Token: 0x06000F19 RID: 3865 RVA: 0x00578634 File Offset: 0x00578634
    public static MediaPlayer CreatePlayer(Uri source, bool muted)
    {
      return new MediaPlayer
      {
        Source = MediaSource.CreateFromUri(source),
        AudioCategory = MediaPlayerAudioCategory.Media,
        Volume = 0.0,
        IsMuted = muted,
        AutoPlay = false
      };
    }

    // Token: 0x170002C5 RID: 709
    // (get) Token: 0x06000F1A RID: 3866 RVA: 0x0057866C File Offset: 0x0057866C
    [Nullable(2)]
    public MediaPlayer CurrentPlayer
    {
      [NullableContext(2)]
      get
      {
        return this.player;
      }
    }

    // Token: 0x06000F1B RID: 3867 RVA: 0x00578674 File Offset: 0x00578674
    public bool PlayVideo(MediaPlayer mp, string assetUrl, string sourceName, IReadOnlyList<CursorCompanion.Cue> videoCues, Action ended, [Nullable(new byte[] { 2, 1 })] Func<MediaPlayerFailedEventArgs, bool> failed, double audioRampSeconds = 2.0, double countdownFromSeconds = 60.0, double hintDelaySeconds = 5.0, bool onboardingAnalytics = true)
    {
      this.StopVideo();
      this.cues = videoCues;
      this.firedCues.Clear();
      this.videoEnded = ended;
      this.hintOpacity = 0.0;
      this.countdownFrom = countdownFromSeconds;
      this.videoAssetName = assetUrl;
      this.videoSourceName = sourceName;
      this.videoStartedTracked = !onboardingAnalytics;
      this.onboardingAnalytics = onboardingAnalytics;
      mp.IsMuted = Environment.GetEnvironmentVariable("HEYCLICKY_MUTE_AUDIO") == "1";
      mp.Volume = 0.0;
      this.player = mp;
      RoundedVideoView roundedVideoView = new RoundedVideoView(330.0, 186.0, 10.0);
      roundedVideoView.Attach(mp);
      while (this.videoCard.Children.Count > 1)
      {
        this.RemoveVideoView();
      }
      this.videoCard.Children.Add(roundedVideoView);
      DispatcherQueueHandler <>9__6;
      mp.MediaEnded += delegate(MediaPlayer _, object _)
      {
        DispatcherQueue dispatcherQueue = this.dq;
        DispatcherQueueHandler dispatcherQueueHandler;
        if ((dispatcherQueueHandler = <>9__6) == null)
        {
          dispatcherQueueHandler = (<>9__6 = delegate
          {
            Log.Info("video", "played to the end");
            if (this.player == mp && this.videoEnded != null && this.onboardingAnalytics)
            {
              ClickyAnalytics.TrackOnboardingVideoCompleted();
            }
            this.FinishVideo();
          });
        }
        dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
      };
      mp.MediaFailed += delegate(MediaPlayer _, MediaPlayerFailedEventArgs e)
      {
        this.dq.TryEnqueue(delegate
        {
          string text2 = "video";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(9, 2);
          defaultInterpolatedStringHandler2.AppendLiteral("failed: ");
          defaultInterpolatedStringHandler2.AppendFormatted<MediaPlayerError>(e.Error);
          defaultInterpolatedStringHandler2.AppendLiteral(" ");
          defaultInterpolatedStringHandler2.AppendFormatted(e.ErrorMessage);
          Log.Warn(text2, defaultInterpolatedStringHandler2.ToStringAndClear());
          if (this.player != mp)
          {
            return;
          }
          Func<MediaPlayerFailedEventArgs, bool> failed2 = failed;
          if (failed2 != null && failed2(e))
          {
            return;
          }
          this.FinishVideo();
        });
      };
      mp.PlaybackSession.PlaybackStateChanged += delegate(MediaPlaybackSession sess, object _)
      {
        string text3 = "video";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(15, 3);
        defaultInterpolatedStringHandler3.AppendLiteral("state ");
        defaultInterpolatedStringHandler3.AppendFormatted<MediaPlaybackState>(sess.PlaybackState);
        defaultInterpolatedStringHandler3.AppendLiteral(" at ");
        defaultInterpolatedStringHandler3.AppendFormatted<double>(sess.Position.TotalSeconds, "0.0");
        defaultInterpolatedStringHandler3.AppendLiteral("s / ");
        defaultInterpolatedStringHandler3.AppendFormatted<double>(sess.NaturalDuration.TotalSeconds, "0.0");
        defaultInterpolatedStringHandler3.AppendLiteral("s");
        Log.Info(text3, defaultInterpolatedStringHandler3.ToStringAndClear());
      };
      this.EnsureShown();
      this.videoCard.Opacity = 0.0;
      mp.Play();
      string text = "video";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 2);
      defaultInterpolatedStringHandler.AppendLiteral("playing ");
      defaultInterpolatedStringHandler.AppendFormatted(assetUrl);
      defaultInterpolatedStringHandler.AppendLiteral(" (");
      defaultInterpolatedStringHandler.AppendFormatted(sourceName);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      this.After(0.2, delegate
      {
        if (this.player != mp)
        {
          return;
        }
        Anim.Opacity(this.videoCard, 1f, 2.0, Easing.EaseInOut(Ui.Compositor), 0.0);
        this.RampVolume(mp, audioRampSeconds);
      });
      if (hintDelaySeconds <= 0.0)
      {
        this.hintOpacity = 1.0;
      }
      else
      {
        this.After(hintDelaySeconds, delegate
        {
          if (this.player == mp)
          {
            this.hintOpacity = 1.0;
          }
        });
      }
      this.videoTimer = this.dq.CreateTimer();
      this.videoTimer.Interval = TimeSpan.FromMilliseconds(100L, 0L);
      this.videoTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.VideoTick(mp);
      };
      this.videoTimer.Start();
      return true;
    }

    // Token: 0x06000F1C RID: 3868 RVA: 0x00578900 File Offset: 0x00578900
    private void RampVolume(MediaPlayer mp, double seconds)
    {
      int step = 0;
      DispatcherQueueTimer t = this.dq.CreateTimer().Rooted();
      t.Interval = TimeSpan.FromSeconds(seconds / 20.0);
      t.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        int step2 = step;
        step = step2 + 1;
        if (this.player != mp)
        {
          t.Stop();
          this.pending.Remove(t);
          return;
        }
        mp.Volume = Math.Min(1.0, (double)step / 20.0);
        if (step >= 20)
        {
          t.Stop();
          this.pending.Remove(t);
        }
      };
      this.pending.Add(t);
      t.Start();
    }

    // Token: 0x06000F1D RID: 3869 RVA: 0x00578990 File Offset: 0x00578990
    private void VideoTick(MediaPlayer mp)
    {
      if (this.player != mp)
      {
        return;
      }
      MediaPlaybackSession playbackSession = mp.PlaybackSession;
      double totalSeconds = playbackSession.Position.TotalSeconds;
      double totalSeconds2 = playbackSession.NaturalDuration.TotalSeconds;
      if (!this.videoStartedTracked && totalSeconds >= 0.1)
      {
        this.videoStartedTracked = true;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["video_kind"] = "onboarding";
        jsonObject["phase"] = "playback";
        jsonObject["asset_url"] = this.videoAssetName;
        jsonObject["player_status"] = "ready_to_play";
        jsonObject["item_status"] = "ready_to_play";
        JsonObject jsonObject2 = jsonObject;
        string text;
        switch (playbackSession.PlaybackState)
        {
        case MediaPlaybackState.Opening:
        case MediaPlaybackState.Buffering:
          text = "waiting_to_play";
          break;
        case MediaPlaybackState.Playing:
          text = "playing";
          break;
        case MediaPlaybackState.Paused:
          text = "paused";
          break;
        default:
          text = "none";
          break;
        }
        jsonObject2["time_control_status"] = text;
        jsonObject["source"] = this.videoSourceName;
        ClickyAnalytics.TrackOnboardingVideoStarted(jsonObject);
      }
      foreach (CursorCompanion.Cue cue in this.cues)
      {
        if (totalSeconds >= cue.AtSeconds && this.firedCues.Add(cue.AtSeconds))
        {
          string text2 = "video";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
          defaultInterpolatedStringHandler.AppendLiteral("cue @");
          defaultInterpolatedStringHandler.AppendFormatted<double>(cue.AtSeconds, "0");
          defaultInterpolatedStringHandler.AppendLiteral("s");
          Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
          try
          {
            cue.Fire();
          }
          catch (Exception ex)
          {
            string text3 = "video";
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 2);
            defaultInterpolatedStringHandler.AppendLiteral("cue @");
            defaultInterpolatedStringHandler.AppendFormatted<double>(cue.AtSeconds);
            defaultInterpolatedStringHandler.AppendLiteral(": ");
            defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
            Log.Warn(text3, defaultInterpolatedStringHandler.ToStringAndClear());
          }
        }
      }
      int num = this.meterTicks + 1;
      this.meterTicks = num;
      if (num % 50 == 0)
      {
        string text4 = "video";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 5);
        defaultInterpolatedStringHandler.AppendLiteral("at ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(totalSeconds, "0.0");
        defaultInterpolatedStringHandler.AppendLiteral("s: volume ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(mp.Volume, "0.00");
        defaultInterpolatedStringHandler.AppendFormatted(mp.IsMuted ? " (muted)" : "");
        defaultInterpolatedStringHandler.AppendLiteral(", output peak ");
        defaultInterpolatedStringHandler.AppendFormatted<float>(SystemVolume.Peak(), "0.000");
        defaultInterpolatedStringHandler.AppendLiteral(", system level ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(SystemVolume.EffectiveLevel(), "0.00");
        Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
      }
      double num2 = totalSeconds2 - totalSeconds;
      bool flag = totalSeconds2 > 0.0 && num2 > 0.0 && num2 <= this.countdownFrom;
      TextBlock textBlock = this.countdown;
      string text5;
      if (!flag)
      {
        text5 = "";
      }
      else
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
        defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Ceiling(num2), "0");
        defaultInterpolatedStringHandler.AppendLiteral("s");
        text5 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      textBlock.Text = text5;
      this.countdownHost.Opacity = (flag ? (this.hintOpacity * Math.Min(1.0, Math.Max(0.0, num2 / 5.0))) : 0.0);
    }

    // Token: 0x170002C6 RID: 710
    // (get) Token: 0x06000F1E RID: 3870 RVA: 0x00578D8C File Offset: 0x00578D8C
    [TupleElementNames(new string[] { "Position", "Duration" })]
    [Nullable(0)]
    public ValueTuple<double, double>? VideoProgress
    {
      [NullableContext(0)]
      [return: TupleElementNames(new string[] { "Position", "Duration" })]
      get
      {
        MediaPlayer mediaPlayer = this.player;
        if (mediaPlayer == null)
        {
          return null;
        }
        return new ValueTuple<double, double>?(new ValueTuple<double, double>(mediaPlayer.PlaybackSession.Position.TotalSeconds, mediaPlayer.PlaybackSession.NaturalDuration.TotalSeconds));
      }
    }

    // Token: 0x06000F1F RID: 3871 RVA: 0x00578DE0 File Offset: 0x00578DE0
    public void DismissVideo()
    {
      if (this.player == null)
      {
        return;
      }
      Log.Info("video", "dismissed");
      this.FinishVideo();
    }

    // Token: 0x06000F20 RID: 3872 RVA: 0x00578E00 File Offset: 0x00578E00
    public void SkipVideo()
    {
      if (!this.IsVideoOnScreen)
      {
        return;
      }
      Log.Info("video", "skipped (double Escape)");
      ClickyAnalytics.TrackOnboardingSkippedByShake();
      this.FinishVideo();
    }

    // Token: 0x170002C7 RID: 711
    // (get) Token: 0x06000F21 RID: 3873 RVA: 0x00578E28 File Offset: 0x00578E28
    private bool IsVideoOnScreen
    {
      get
      {
        return this.player != null && this.videoEnded != null;
      }
    }

    // Token: 0x170002C8 RID: 712
    // (get) Token: 0x06000F22 RID: 3874 RVA: 0x00578E40 File Offset: 0x00578E40
    private double VideoRemainingSeconds
    {
      get
      {
        MediaPlayer mediaPlayer = this.player;
        if (mediaPlayer == null)
        {
          return 0.0;
        }
        double num;
        try
        {
          MediaPlaybackSession playbackSession = mediaPlayer.PlaybackSession;
          num = Math.Max(0.0, (playbackSession.NaturalDuration - playbackSession.Position).TotalSeconds);
        }
        catch
        {
          num = 0.0;
        }
        return num;
      }
    }

    // Token: 0x06000F23 RID: 3875 RVA: 0x00578EB4 File Offset: 0x00578EB4
    internal void SeekNearEnd(double secondsBeforeEnd)
    {
      MediaPlayer mediaPlayer = this.player;
      if (mediaPlayer == null)
      {
        return;
      }
      MediaPlaybackSession playbackSession = mediaPlayer.PlaybackSession;
      if (playbackSession.NaturalDuration.TotalSeconds <= secondsBeforeEnd)
      {
        return;
      }
      playbackSession.Position = playbackSession.NaturalDuration - TimeSpan.FromSeconds(secondsBeforeEnd);
    }

    // Token: 0x06000F24 RID: 3876 RVA: 0x00578EFC File Offset: 0x00578EFC
    private void FinishVideo()
    {
      MediaPlayer mp = this.player;
      if (mp == null)
      {
        return;
      }
      DispatcherQueueTimer dispatcherQueueTimer = this.videoTimer;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      this.countdownHost.Opacity = 0.0;
      Anim.Opacity(this.videoCard, 0f, 2.0, Easing.EaseInOut(Ui.Compositor), 0.0);
      Action action = this.videoEnded;
      this.videoEnded = null;
      this.After(2.0, delegate
      {
        if (this.player == mp)
        {
          this.StopVideo();
        }
      });
      if (action == null)
      {
        return;
      }
      action();
    }

    // Token: 0x06000F25 RID: 3877 RVA: 0x00578FB0 File Offset: 0x00578FB0
    private void StopVideo()
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.videoTimer;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      while (this.videoCard.Children.Count > 1)
      {
        this.RemoveVideoView();
      }
      MediaPlayer mediaPlayer = this.player;
      if (mediaPlayer != null)
      {
        this.player = null;
        try
        {
          mediaPlayer.Pause();
          mediaPlayer.Dispose();
        }
        catch
        {
        }
      }
      this.HideIfEmpty();
    }

    // Token: 0x06000F26 RID: 3878 RVA: 0x00579024 File Offset: 0x00579024
    private void RemoveVideoView()
    {
      UIElementCollection children = this.videoCard.Children;
      RoundedVideoView roundedVideoView = children[children.Count - 1] as RoundedVideoView;
      if (roundedVideoView != null)
      {
        roundedVideoView.Attach(null);
      }
      this.videoCard.Children.RemoveAt(this.videoCard.Children.Count - 1);
    }

    // Token: 0x14000034 RID: 52
    // (add) Token: 0x06000F27 RID: 3879 RVA: 0x0057907C File Offset: 0x0057907C
    // (remove) Token: 0x06000F28 RID: 3880 RVA: 0x005790B4 File Offset: 0x005790B4
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action DoubleEscape;

    // Token: 0x06000F29 RID: 3881 RVA: 0x005790EC File Offset: 0x005790EC
    private void EscapeWatch()
    {
      bool flag = ((int)Native.GetAsyncKeyState(27) & 32768) != 0;
      if (flag && !this.escapeWasDown)
      {
        this.OnEscapePressed();
      }
      this.escapeWasDown = flag;
    }

    // Token: 0x06000F2A RID: 3882 RVA: 0x00579124 File Offset: 0x00579124
    internal void OnEscapePressed()
    {
      long tickCount = Environment.TickCount64;
      if (this.firstEscapeAt != 0L && tickCount - this.firstEscapeAt <= 2000L)
      {
        this.firstEscapeAt = 0L;
        if (this.IsVideoOnScreen)
        {
          if (this.onboardingAnalytics)
          {
            ClickyAnalytics.TrackOnboardingVideoEscapePressed("confirmed_skip", this.VideoRemainingSeconds);
          }
          this.SkipVideo();
          return;
        }
        if (this.player == null)
        {
          Action doubleEscape = this.DoubleEscape;
          if (doubleEscape == null)
          {
            return;
          }
          doubleEscape();
          return;
        }
      }
      else
      {
        this.firstEscapeAt = tickCount;
        if (this.IsVideoOnScreen && this.onboardingAnalytics)
        {
          ClickyAnalytics.TrackOnboardingVideoEscapePressed("armed_skip", this.VideoRemainingSeconds);
        }
      }
    }

    // Token: 0x06000F2B RID: 3883 RVA: 0x005791C0 File Offset: 0x005791C0
    public void WatchEscape(bool on)
    {
      if (on && !this.frame.IsRunning)
      {
        this.frame.Start();
        return;
      }
      if (!on && !this.visible)
      {
        this.frame.Stop();
      }
    }

    // Token: 0x06000F2C RID: 3884 RVA: 0x005791F4 File Offset: 0x005791F4
    private void After(double seconds, Action a)
    {
      DispatcherQueueTimer t = this.dq.CreateTimer().Rooted();
      t.Interval = TimeSpan.FromSeconds(Math.Max(0.001, seconds));
      t.IsRepeating = false;
      this.pending.Add(t);
      t.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.pending.Remove(t);
        a();
      };
      t.Start();
    }

    // Token: 0x06000F2F RID: 3887 RVA: 0x005792BC File Offset: 0x005792BC
    [CompilerGenerated]
    internal static double <LogSmoothness>g__Percentile|39_1(double fraction, ref CursorCompanion.<>c__DisplayClass39_0 A_1)
    {
      return A_1.sortedFrameMs[Math.Min(A_1.sortedFrameMs.Length - 1, (int)(fraction * (double)A_1.sortedFrameMs.Length))];
    }

    // Token: 0x0400062E RID: 1582
    public const double VideoWidth = 330.0;

    // Token: 0x0400062F RID: 1583
    public const double VideoHeight = 186.0;

    // Token: 0x04000630 RID: 1584
    private const double StageWidth = 440.0;

    // Token: 0x04000631 RID: 1585
    private const double StageHeight = 320.0;

    // Token: 0x04000632 RID: 1586
    private const double CardLeftInStage = 10.0;

    // Token: 0x04000633 RID: 1587
    private const double CardTopInStage = 18.0;

    // Token: 0x04000634 RID: 1588
    private const double HintRowHeight = 28.0;

    // Token: 0x04000635 RID: 1589
    private const double AnchorFromPointerX = 35.0;

    // Token: 0x04000636 RID: 1590
    private const double AnchorFromPointerY = 25.0;

    // Token: 0x04000637 RID: 1591
    private const double GapFromPointer = 12.0;

    // Token: 0x04000638 RID: 1592
    private readonly OverlayWindow window;

    // Token: 0x04000639 RID: 1593
    private readonly DispatcherQueue dq;

    // Token: 0x0400063A RID: 1594
    private readonly DispatcherQueueTimer frame;

    // Token: 0x0400063B RID: 1595
    private readonly Canvas stage = new Canvas
    {
      Width = 440.0,
      Height = 320.0
    };

    // Token: 0x0400063C RID: 1596
    private readonly Grid videoCard = new Grid
    {
      Width = 330.0,
      Height = 186.0,
      Opacity = 0.0
    };

    // Token: 0x0400063D RID: 1597
    private readonly TextBlock countdown;

    // Token: 0x0400063E RID: 1598
    private readonly Grid countdownHost;

    // Token: 0x0400063F RID: 1599
    private CursorAdjacentPlacement.Sides cardSides;

    // Token: 0x04000640 RID: 1600
    private readonly Func<string> tint;

    // Token: 0x04000641 RID: 1601
    [Nullable(2)]
    private MediaPlayer player;

    // Token: 0x04000642 RID: 1602
    [Nullable(2)]
    private CompositorSpringMotion motion;

    // Token: 0x04000643 RID: 1603
    private static readonly OverlayCursorSpring PlacementSpring = new OverlayCursorSpring(0.28, 0.98);

    // Token: 0x04000644 RID: 1604
    private Vector3 anchorRest;

    // Token: 0x04000645 RID: 1605
    private Vector3 offsetRest;

    // Token: 0x04000646 RID: 1606
    private bool placed;

    // Token: 0x04000647 RID: 1607
    private bool visible;

    // Token: 0x04000648 RID: 1608
    private bool following;

    // Token: 0x04000649 RID: 1609
    private double lastTick = -1.0;

    // Token: 0x0400064A RID: 1610
    private double lastAnchorMove = -1.0;

    // Token: 0x0400064B RID: 1611
    private IntPtr windowMonitor;

    // Token: 0x0400064C RID: 1612
    private Native.RECT windowBounds;

    // Token: 0x0400064D RID: 1613
    private readonly List<double> statFrameMs = new List<double>();

    // Token: 0x0400064E RID: 1614
    private int statFlips;

    // Token: 0x0400064F RID: 1615
    private double hintOpacity;

    // Token: 0x04000650 RID: 1616
    private int meterTicks;

    // Token: 0x04000651 RID: 1617
    private readonly HashSet<double> firedCues = new HashSet<double>();

    // Token: 0x04000652 RID: 1618
    private IReadOnlyList<CursorCompanion.Cue> cues = Array.Empty<CursorCompanion.Cue>();

    // Token: 0x04000653 RID: 1619
    [Nullable(2)]
    private Action videoEnded;

    // Token: 0x04000654 RID: 1620
    [Nullable(2)]
    private DispatcherQueueTimer videoTimer;

    // Token: 0x04000655 RID: 1621
    private double countdownFrom = 60.0;

    // Token: 0x04000656 RID: 1622
    private string videoAssetName = "";

    // Token: 0x04000657 RID: 1623
    private string videoSourceName = "bundled";

    // Token: 0x04000658 RID: 1624
    private bool videoStartedTracked;

    // Token: 0x04000659 RID: 1625
    private bool onboardingAnalytics = true;

    // Token: 0x0400065B RID: 1627
    private bool escapeWasDown;

    // Token: 0x0400065C RID: 1628
    private long firstEscapeAt;

    // Token: 0x0400065D RID: 1629
    private readonly HashSet<DispatcherQueueTimer> pending = new HashSet<DispatcherQueueTimer>();

    // Token: 0x02000618 RID: 1560
    [Nullable(0)]
    public sealed class Cue : IEquatable<CursorCompanion.Cue>
    {
      // Token: 0x060030D4 RID: 12500 RVA: 0x0066A040 File Offset: 0x0066A040
      public Cue(double AtSeconds, Action Fire)
      {
        this.AtSeconds = AtSeconds;
        this.Fire = Fire;
        base..ctor();
      }

      // Token: 0x17000778 RID: 1912
      // (get) Token: 0x060030D5 RID: 12501 RVA: 0x0066A058 File Offset: 0x0066A058
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(CursorCompanion.Cue);
        }
      }

      // Token: 0x17000779 RID: 1913
      // (get) Token: 0x060030D6 RID: 12502 RVA: 0x0066A064 File Offset: 0x0066A064
      // (set) Token: 0x060030D7 RID: 12503 RVA: 0x0066A06C File Offset: 0x0066A06C
      public double AtSeconds { get; set; }

      // Token: 0x1700077A RID: 1914
      // (get) Token: 0x060030D8 RID: 12504 RVA: 0x0066A078 File Offset: 0x0066A078
      // (set) Token: 0x060030D9 RID: 12505 RVA: 0x0066A080 File Offset: 0x0066A080
      public Action Fire { get; set; }

      // Token: 0x060030DA RID: 12506 RVA: 0x0066A08C File Offset: 0x0066A08C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Cue");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060030DB RID: 12507 RVA: 0x0066A0D8 File Offset: 0x0066A0D8
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("AtSeconds = ");
        builder.Append(this.AtSeconds.ToString());
        builder.Append(", Fire = ");
        builder.Append(this.Fire);
        return true;
      }

      // Token: 0x060030DC RID: 12508 RVA: 0x0066A12C File Offset: 0x0066A12C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(CursorCompanion.Cue left, CursorCompanion.Cue right)
      {
        return !(left == right);
      }

      // Token: 0x060030DD RID: 12509 RVA: 0x0066A138 File Offset: 0x0066A138
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(CursorCompanion.Cue left, CursorCompanion.Cue right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060030DE RID: 12510 RVA: 0x0066A14C File Offset: 0x0066A14C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<AtSeconds>k__BackingField)) * -1521134295 + EqualityComparer<Action>.Default.GetHashCode(this.<Fire>k__BackingField);
      }

      // Token: 0x060030DF RID: 12511 RVA: 0x0066A18C File Offset: 0x0066A18C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as CursorCompanion.Cue);
      }

      // Token: 0x060030E0 RID: 12512 RVA: 0x0066A19C File Offset: 0x0066A19C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(CursorCompanion.Cue other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<double>.Default.Equals(this.<AtSeconds>k__BackingField, other.<AtSeconds>k__BackingField) && EqualityComparer<Action>.Default.Equals(this.<Fire>k__BackingField, other.<Fire>k__BackingField));
      }

      // Token: 0x060030E2 RID: 12514 RVA: 0x0066A200 File Offset: 0x0066A200
      [CompilerGenerated]
      private Cue(CursorCompanion.Cue original)
      {
        this.AtSeconds = original.<AtSeconds>k__BackingField;
        this.Fire = original.<Fire>k__BackingField;
      }

      // Token: 0x060030E3 RID: 12515 RVA: 0x0066A220 File Offset: 0x0066A220
      [CompilerGenerated]
      public void Deconstruct(out double AtSeconds, out Action Fire)
      {
        AtSeconds = this.AtSeconds;
        Fire = this.Fire;
      }
    }
  }
}
