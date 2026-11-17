using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Design;
using HeyClicky.App.Dock;
using HeyClicky.App.Home;
using HeyClicky.App.Ink;
using HeyClicky.App.UI;
using HeyClicky.Drawing;
using HeyClicky.Drawing.Media;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Foundation;
using Windows.System;
using Windows.UI;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200016D RID: 365
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyDrawingWindowView : Grid
  {
    // Token: 0x0600140A RID: 5130 RVA: 0x005A7B3C File Offset: 0x005A7B3C
    public static Vector2 StageSize(Vector2 screenSize, StoredLesson stored)
    {
      StoredLesson.Meta data = stored.Data;
      double num = ((data.DisplayBounds.Length == 4 && data.DisplayBounds[3] > 0.0) ? (data.DisplayBounds[2] / data.DisplayBounds[3]) : ((data.CanvasHeight > 0) ? ((double)data.CanvasWidth / (double)data.CanvasHeight) : 1.6));
      double num2 = (double)Math.Min(screenSize.X - 200f, 1500f);
      double num3 = (double)(screenSize.Y - 160f) - 34.0 - 132.0;
      double num4 = Math.Min(num2, num3 * num);
      return new Vector2((float)Math.Round(num4), (float)Math.Round(num4 / num));
    }

    // Token: 0x0600140B RID: 5131 RVA: 0x005A7BFC File Offset: 0x005A7BFC
    public ClickyDrawingWindowView(ClickyLessonInlinePlayer player, Vector2 screenSize, Vector2 stageSize, double displayScale, Action close, Action playFullScreen)
    {
      ClickyDrawingWindowView <>4__this = this;
      this.player = player;
      this.close = close;
      this.stageSize = stageSize;
      this.dq = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
      base.Width = (double)screenSize.X;
      base.Height = (double)screenSize.Y;
      base.RequestedTheme = ElementTheme.Light;
      this.dim = new Border
      {
        Background = Ui.Brush(DS.Hex(263692U, 0.82)),
        Opacity = 0.0
      };
      this.dim.Tapped += delegate(object _, TappedRoutedEventArgs e)
      {
        e.Handled = true;
        close();
      };
      base.Children.Add(this.dim);
      base.Children.Add(this.focusTarget);
      double num = (double)stageSize.X + 20.0;
      double num2 = 34.0 + (double)stageSize.Y + 132.0 + 20.0;
      this.card = new Grid
      {
        Width = num,
        Height = num2,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        Opacity = 0.0
      };
      this.card.Children.Add(new ShadowLayer(22.0, DS.Black(0.5), 40.0, 20.0, 0.0));
      this.card.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(22.0),
        Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Rgb(0.16, 0.16, 0.16, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Rgb(0.07, 0.07, 0.07, 1.0))
        }),
        BorderBrush = Ui.Brush(DS.White(0.16)),
        BorderThickness = new Thickness(1.0)
      });
      Grid grid = new Grid
      {
        Margin = new Thickness(10.0),
        Background = Ui.Brush(DS.Rgb(0.09, 0.09, 0.09, 1.0))
      };
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(34.0)
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength((double)stageSize.Y)
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(132.0)
      });
      Ui.ClipRounded(grid, 12.0);
      this.card.Children.Add(grid);
      base.Children.Add(this.card);
      Grid grid2 = new Grid
      {
        Background = Ui.Transparent
      };
      grid2.Children.Add(new Border
      {
        Background = Ui.Horizontal(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Hex(14079447U, 1.0)),
          new ValueTuple<double, Color>(0.5, DS.Hex(15790320U, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Hex(14079447U, 1.0))
        })
      });
      this.wash.Background = Ui.Brush(ClickyDrawingWindowView.Paper.TitleWash.WithAlpha(0.62));
      grid2.Children.Add(this.wash);
      grid2.Children.Add(new Border
      {
        Height = 0.5,
        VerticalAlignment = VerticalAlignment.Bottom,
        Background = Ui.Brush(DS.Black(0.1))
      });
      CaptionButton captionButton = new CaptionButton(CaptionButton.Kind.Maximize, 34.0, ClickyDrawingWindowView.Paper.Ink.WithAlpha(0.8), "Play it full screen over your screen");
      captionButton.Click += playFullScreen;
      CaptionButton captionButton2 = new CaptionButton(CaptionButton.Kind.Close, 34.0, ClickyDrawingWindowView.Paper.Ink.WithAlpha(0.8), "Close (esc)");
      captionButton2.Click += close;
      grid2.Children.Add(new StackPanel
      {
        Orientation = Orientation.Horizontal,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Top,
        Children = { captionButton, captionButton2 }
      });
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 5.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      if (player.Lesson.HasNarration)
      {
        TextBlock textBlock = Icons.Glyph("\ue767", 10.5, ClickyDrawingWindowView.Paper.Ink.WithAlpha(0.55));
        textBlock.FontWeight = FontWeights.SemiBold;
        stackPanel.Children.Add(textBlock);
      }
      TextBlock textBlock2 = Ui.Text(ClickyDrawingLaptopScreen.LengthText(player.Lesson.Duration), 12.0, FontWeights.SemiBold, ClickyDrawingWindowView.Paper.Ink.WithAlpha(0.55), TextAlignment.Left, 0, false, false);
      Typography.SetNumeralAlignment(textBlock2, FontNumeralAlignment.Tabular);
      stackPanel.Children.Add(textBlock2);
      this.download = new ClickyDrawingWindowView.DownloadButton(ClickyDrawingWindowView.Paper.Ink);
      this.download.Click += this.DownloadButtonPressed;
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 10.0,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Center,
        Margin = new Thickness(0.0, 0.0, 104.0, 0.0),
        Children = { stackPanel, this.download }
      };
      grid2.Children.Add(stackPanel2);
      TextBlock textBlock3 = Ui.Text(player.Lesson.Title, 13.0, FontWeights.SemiBold, ClickyDrawingWindowView.Paper.Ink.WithAlpha(0.85), TextAlignment.Center, 1, false, false);
      textBlock3.Margin = new Thickness(280.0, 0.0, 280.0, 0.0);
      textBlock3.VerticalAlignment = VerticalAlignment.Center;
      textBlock3.IsHitTestVisible = false;
      grid2.Children.Add(textBlock3);
      grid.Children.Add(grid2);
      Pressable pressable = new Pressable(true)
      {
        Width = (double)stageSize.X,
        Height = (double)stageSize.Y,
        Background = Ui.Brush(Colors.White)
      };
      if (File.Exists(player.Stored.PosterPath))
      {
        this.picture.Source = new BitmapImage(new Uri(player.Stored.PosterPath));
      }
      else if (File.Exists(player.Stored.ScreenPath))
      {
        this.picture.Source = new BitmapImage(new Uri(player.Stored.ScreenPath));
      }
      pressable.Children.Add(this.picture);
      this.stageOverlay = new Grid
      {
        Width = 72.0,
        Height = 72.0,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        IsHitTestVisible = false,
        CenterPoint = new Vector3(36f, 36f, 0f)
      };
      this.stageOverlay.Children.Add(new ShadowLayer(36.0, DS.Black(0.35), 10.0, 3.0, 0.0));
      this.stageGel = new GelSurface(GelAppearance.PrimaryBlue, 36.0);
      this.stageOverlay.Children.Add(this.stageGel);
      this.stageGlyph = Icons.Glyph("\uf5b0", 26.0, DS.Black(0.82));
      this.stageGlyph.FontWeight = FontWeights.Bold;
      this.stageOverlay.Children.Add(this.stageGlyph);
      pressable.Children.Add(this.stageOverlay);
      pressable.HoverChanged += delegate(bool hovered)
      {
        <>4__this.stageGel.Hovered = hovered;
      };
      pressable.Click += delegate
      {
        if (!ClickyDrawingWindowViewer.Shared.IsRecording)
        {
          player.TogglePlay();
        }
      };
      Grid.SetRow(pressable, 1);
      grid.Children.Add(pressable);
      this.controls = new Grid
      {
        Padding = new Thickness(18.0, 0.0, 18.0, 16.0)
      };
      this.controls.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      this.controls.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(32.0)
      });
      this.caption = new ClickyDrawingWindowView.SpokenCaption(player.Lesson, player.Stored.ReplayShift);
      this.controls.Children.Add(this.caption);
      Grid grid3 = new Grid
      {
        ColumnSpacing = 14.0
      };
      grid3.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid3.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid3.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      this.playPause = new Pressable(true)
      {
        Width = 32.0,
        Height = 32.0
      };
      this.playPause.Children.Add(new GelSurface(GelAppearance.PrimaryBlue, 16.0));
      this.playPauseGlyph = Icons.Glyph("\uf5b0", 13.0, DS.Black(0.82));
      this.playPauseGlyph.FontWeight = FontWeights.Bold;
      this.playPause.Children.Add(this.playPauseGlyph);
      this.playPause.Click += delegate
      {
        player.TogglePlay();
      };
      grid3.Children.Add(this.playPause);
      this.scrubber = new ClickyDrawingWindowView.BeatScrubber(player)
      {
        Height = 14.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      Grid.SetColumn(this.scrubber, 1);
      grid3.Children.Add(this.scrubber);
      this.timeLabel = Ui.Text("", 11.5, FontWeights.Medium, HomeTheme.SecondaryText, TextAlignment.Left, 0, false, false);
      this.timeLabel.VerticalAlignment = VerticalAlignment.Center;
      Typography.SetNumeralAlignment(this.timeLabel, FontNumeralAlignment.Tabular);
      Grid.SetColumn(this.timeLabel, 2);
      grid3.Children.Add(this.timeLabel);
      Grid.SetRow(grid3, 1);
      this.controls.Children.Add(grid3);
      Grid.SetRow(this.controls, 2);
      grid.Children.Add(this.controls);
      player.Changed += this.Refresh;
      player.ProgressChanged += this.ShowProgress;
      player.FrameReady += this.OnFrame;
      base.KeyDown += this.OnKey;
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        <>4__this.TakeKeyboardFocus();
      };
      this.Refresh();
      this.ShowProgress(player.Position);
      this.LoadTintAsync();
    }

    // Token: 0x0600140C RID: 5132 RVA: 0x005A8964 File Offset: 0x005A8964
    public void TakeKeyboardFocus()
    {
      this.focusTarget.Focus(FocusState.Programmatic);
    }

    // Token: 0x0600140D RID: 5133 RVA: 0x005A8974 File Offset: 0x005A8974
    private void OnKey(object sender, KeyRoutedEventArgs e)
    {
      if (e.Key == VirtualKey.Escape)
      {
        e.Handled = true;
        this.close();
        return;
      }
      if (e.Key == VirtualKey.Space)
      {
        e.Handled = true;
        if (!ClickyDrawingWindowViewer.Shared.IsRecording)
        {
          this.player.TogglePlay();
        }
      }
    }

    // Token: 0x0600140E RID: 5134 RVA: 0x005A89C8 File Offset: 0x005A89C8
    public void Appear()
    {
      this.card.CenterPoint = new Vector3((float)this.card.Width / 2f, (float)this.card.Height / 2f, 0f);
      Anim.OpacitySpring(this.dim, 1f, ClickyDrawingWindowView.Showing);
      Anim.OpacitySpring(this.card, 1f, ClickyDrawingWindowView.Showing);
      if (!Anim.ReduceMotion)
      {
        Anim.ScaleUpFrom(this.card, 0.94f, ClickyDrawingWindowView.Showing, null);
      }
    }

    // Token: 0x0600140F RID: 5135 RVA: 0x005A8A60 File Offset: 0x005A8A60
    public void Disappear()
    {
      CubicBezierEasingFunction cubicBezierEasingFunction = Ui.Compositor.CreateCubicBezierEasingFunction(new Vector2(0.42f, 0f), new Vector2(1f, 1f));
      Anim.Opacity(this.dim, 0f, 0.18, cubicBezierEasingFunction, 0.0);
      Anim.Opacity(this.card, 0f, 0.18, cubicBezierEasingFunction, 0.0);
      if (!Anim.ReduceMotion)
      {
        Anim.Scale(this.card, 0.94f, 0.18, cubicBezierEasingFunction, new Vector3?(this.card.CenterPoint));
      }
    }

    // Token: 0x06001410 RID: 5136 RVA: 0x005A8B10 File Offset: 0x005A8B10
    public void Closing()
    {
      this.player.Changed -= this.Refresh;
      this.player.ProgressChanged -= this.ShowProgress;
      this.player.FrameReady -= this.OnFrame;
      CancellationTokenSource cancellationTokenSource = this.downloadCancellation;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      LessonVideoRecorder lessonVideoRecorder = this.recorder;
      if (lessonVideoRecorder != null)
      {
        lessonVideoRecorder.Cancel();
      }
      this.recorder = null;
    }

    // Token: 0x06001411 RID: 5137 RVA: 0x005A8B8C File Offset: 0x005A8B8C
    private void Refresh()
    {
      bool isRecording = ClickyDrawingWindowViewer.Shared.IsRecording;
      bool flag = !this.player.IsPlaying;
      ClickyDrawingWindowView.ShowPlayGlyph(this.stageGlyph, this.player.HasEnded, false, 4.0);
      if (flag != this.showsStageButton)
      {
        this.showsStageButton = flag;
        CubicBezierEasingFunction cubicBezierEasingFunction = Easing.EaseOut(Ui.Compositor);
        Anim.Opacity(this.stageOverlay, flag > false, 0.18, cubicBezierEasingFunction, 0.0);
        Anim.Scale(this.stageOverlay, (flag || Anim.ReduceMotion) ? 1f : 0.9f, 0.18, cubicBezierEasingFunction, new Vector3?(new Vector3(36f, 36f, 0f)));
      }
      ClickyDrawingWindowView.ShowPlayGlyph(this.playPauseGlyph, !this.player.IsPlaying && this.player.HasEnded, this.player.IsPlaying, 0.0);
      ToolTipService.SetToolTip(this.playPause, this.player.IsPlaying ? "Pause (space)" : "Play (space)");
      this.caption.Show(this.player.CurrentBeat, this.player.Position);
      this.controls.IsHitTestVisible = !isRecording;
      this.controls.Opacity = (isRecording ? 0.55 : 1.0);
    }

    // Token: 0x06001412 RID: 5138 RVA: 0x005A8D08 File Offset: 0x005A8D08
    private static void ShowPlayGlyph(TextBlock glyph, bool turnBack, bool pause, double nudge)
    {
      glyph.Text = (pause ? "\ue769" : (turnBack ? "\ue72c" : "\uf5b0"));
      glyph.Margin = new Thickness((pause || turnBack) ? 0.0 : nudge, 0.0, 0.0, 0.0);
      glyph.RenderTransformOrigin = new Point(0.5, 0.5);
      object obj;
      if (!turnBack)
      {
        obj = null;
      }
      else
      {
        (obj = new ScaleTransform()).ScaleX = -1.0;
      }
      glyph.RenderTransform = obj;
    }

    // Token: 0x06001413 RID: 5139 RVA: 0x005A8DA8 File Offset: 0x005A8DA8
    private void ShowProgress(double position)
    {
      this.timeLabel.Text = ClickyDrawingWindowView.Clock(position) + " / " + ClickyDrawingWindowView.Clock(this.player.Duration);
      this.scrubber.Show(position);
      this.caption.Show(this.player.CurrentBeat, position);
      this.download.ShowRecordingProgress(position, this.player.Duration);
    }

    // Token: 0x06001414 RID: 5140 RVA: 0x005A8E1C File Offset: 0x005A8E1C
    private static string Clock(double seconds)
    {
      int num = Math.Max(0, (int)Math.Floor(seconds));
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
      defaultInterpolatedStringHandler.AppendFormatted<int>(num / 60);
      defaultInterpolatedStringHandler.AppendLiteral(":");
      defaultInterpolatedStringHandler.AppendFormatted<int>(num % 60, "00");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06001415 RID: 5141 RVA: 0x005A8E70 File Offset: 0x005A8E70
    private void OnFrame(LessonPlaybackFrame frame)
    {
      LessonVideoRecorder lessonVideoRecorder = this.recorder;
      if (lessonVideoRecorder != null)
      {
        lessonVideoRecorder.Add(frame);
      }
      if (Interlocked.Exchange<LessonPlaybackFrame>(ref this.pendingFrame, frame) != null)
      {
        return;
      }
      this.dq.TryEnqueue(delegate
      {
        LessonPlaybackFrame lessonPlaybackFrame = Interlocked.Exchange<LessonPlaybackFrame>(ref this.pendingFrame, null);
        if (lessonPlaybackFrame == null)
        {
          return;
        }
        if (this.frameBitmap == null || this.frameBitmap.PixelWidth != lessonPlaybackFrame.Width || this.frameBitmap.PixelHeight != lessonPlaybackFrame.Height)
        {
          this.frameBitmap = new WriteableBitmap(lessonPlaybackFrame.Width, lessonPlaybackFrame.Height);
          this.picture.Source = this.frameBitmap;
        }
        using (Stream stream = this.frameBitmap.PixelBuffer.AsStream())
        {
          stream.Write(lessonPlaybackFrame.Pixels, 0, Math.Min(lessonPlaybackFrame.Pixels.Length, (int)stream.Length));
        }
        this.frameBitmap.Invalidate();
      });
    }

    // Token: 0x06001416 RID: 5142 RVA: 0x005A8EAC File Offset: 0x005A8EAC
    private async Task LoadTintAsync()
    {
      Color? color = await ClickyDrawingLaptopScreen.Tint(this.player.Stored.PosterPath);
      if (color != null)
      {
        Color valueOrDefault = color.GetValueOrDefault();
        this.wash.Background = Ui.Brush(valueOrDefault.WithAlpha(0.62));
      }
    }

    // Token: 0x06001417 RID: 5143 RVA: 0x005A8EF0 File Offset: 0x005A8EF0
    private void DownloadButtonPressed()
    {
      switch (this.download.State)
      {
      case ClickyDrawingWindowView.DownloadButton.Phase.Idle:
      case ClickyDrawingWindowView.DownloadButton.Phase.Failed:
        this.StartDownloadAsync();
        return;
      case ClickyDrawingWindowView.DownloadButton.Phase.Recording:
      case ClickyDrawingWindowView.DownloadButton.Phase.Saving:
        break;
      case ClickyDrawingWindowView.DownloadButton.Phase.Saved:
      {
        string savedPath = this.download.SavedPath;
        if (savedPath != null)
        {
          ChatBlockViews.RevealPath(savedPath);
        }
        break;
      }
      default:
        return;
      }
    }

    // Token: 0x06001418 RID: 5144 RVA: 0x005A8F44 File Offset: 0x005A8F44
    private async Task StartDownloadAsync()
    {
      ValueTuple<int, int> frameSize = this.player.FrameSize;
      int item = frameSize.Item1;
      int item2 = frameSize.Item2;
      if (item <= 0)
      {
        this.download.Show(ClickyDrawingWindowView.DownloadButton.Phase.Failed, null);
      }
      else
      {
        CancellationTokenSource cancellation = (this.downloadCancellation = new CancellationTokenSource());
        this.download.Show(ClickyDrawingWindowView.DownloadButton.Phase.Recording, null);
        ClickyDrawingWindowViewer.Shared.IsRecording = true;
        this.Refresh();
        NotchPeekSurface.PeekAction("drawing_download_started", null);
        try
        {
          ClickyDrawingWindowView.<>c__DisplayClass44_0 CS$<>8__locals1 = new ClickyDrawingWindowView.<>c__DisplayClass44_0();
          LessonVideoRecorder started;
          try
          {
            started = LessonVideoRecorder.Start(item, item2);
          }
          catch (Exception ex)
          {
            Log.Warn("lessons", "drawing video: couldn't start recording: " + ex.Message);
            this.download.Show(ClickyDrawingWindowView.DownloadButton.Phase.Failed, null);
            return;
          }
          this.recorder = started;
          this.player.Pause();
          this.player.Seek(0.0);
          await Task.Delay(250);
          if (!cancellation.IsCancellationRequested)
          {
            this.player.BeginCapturingSounds();
            started.MarkPlaybackStarted();
            this.player.Play();
            while (!this.player.HasEnded)
            {
              await Task.Delay(100);
              if (cancellation.IsCancellationRequested)
              {
                return;
              }
            }
            await Task.Delay(600);
            if (!cancellation.IsCancellationRequested)
            {
              this.download.Show(ClickyDrawingWindowView.DownloadButton.Phase.Saving, null);
              CS$<>8__locals1.sounds = await this.player.EndCapturingSoundsAsync();
              CS$<>8__locals1.seconds = this.player.Duration + 0.6;
              this.recorder = null;
              CS$<>8__locals1.videoPath = await started.FinishAsync(CS$<>8__locals1.seconds);
              if (CS$<>8__locals1.videoPath == null)
              {
                this.download.Show(ClickyDrawingWindowView.DownloadButton.Phase.Failed, null);
              }
              else
              {
                CS$<>8__locals1.narration = (this.player.Lesson.HasNarration ? this.player.Narration : null);
                CS$<>8__locals1.offset = this.player.Stored.ReplayShift;
                CS$<>8__locals1.title = this.player.Lesson.Title;
                string text = await Task.Run<string>(delegate
                {
                  short[] array = LessonVideoFiles.MixSoundtrack((CS$<>8__locals1.narration != null) ? CS$<>8__locals1.narration.GetValueOrDefault().Item1 : null, (CS$<>8__locals1.narration != null) ? CS$<>8__locals1.narration.GetValueOrDefault().Item2 : 0, CS$<>8__locals1.offset, (CS$<>8__locals1.sounds.Count > 0) ? CS$<>8__locals1.sounds.Mix(CS$<>8__locals1.seconds) : null, CS$<>8__locals1.seconds);
                  return LessonVideoRecorder.Save(CS$<>8__locals1.videoPath, array, CS$<>8__locals1.title, LessonVideoRecorder.DownloadsFolder());
                });
                if (text != null)
                {
                  this.download.Show(ClickyDrawingWindowView.DownloadButton.Phase.Saved, text);
                  NotchPeekSurface.PeekAction("drawing_downloaded", null);
                }
                else
                {
                  this.download.Show(ClickyDrawingWindowView.DownloadButton.Phase.Failed, null);
                }
                CS$<>8__locals1 = null;
                started = null;
              }
            }
          }
        }
        finally
        {
          if (this.downloadCancellation == cancellation)
          {
            ClickyDrawingWindowViewer.Shared.IsRecording = false;
            if (!cancellation.IsCancellationRequested)
            {
              this.Refresh();
            }
          }
        }
      }
    }

    // Token: 0x0400096D RID: 2413
    private const double TitleBarHeight = 34.0;

    // Token: 0x0400096E RID: 2414
    private const double ControlsHeight = 132.0;

    // Token: 0x0400096F RID: 2415
    private const double Bezel = 10.0;

    // Token: 0x04000970 RID: 2416
    private const double ScreenCornerRadius = 12.0;

    // Token: 0x04000971 RID: 2417
    private static readonly SwiftSpring Showing = new SwiftSpring(0.38, 0.86);

    // Token: 0x04000972 RID: 2418
    private static readonly ClickyHomeSpreadPaper Paper = ClickyDrawingLaptopScreen.Paper;

    // Token: 0x04000973 RID: 2419
    private readonly ClickyLessonInlinePlayer player;

    // Token: 0x04000974 RID: 2420
    private readonly Action close;

    // Token: 0x04000975 RID: 2421
    private readonly Vector2 stageSize;

    // Token: 0x04000976 RID: 2422
    private readonly Microsoft.UI.Dispatching.DispatcherQueue dq;

    // Token: 0x04000977 RID: 2423
    private readonly Border dim;

    // Token: 0x04000978 RID: 2424
    private readonly Grid card;

    // Token: 0x04000979 RID: 2425
    private readonly Border wash = new Border();

    // Token: 0x0400097A RID: 2426
    private readonly Image picture = new Image
    {
      Stretch = Stretch.Uniform
    };

    // Token: 0x0400097B RID: 2427
    private readonly Grid stageOverlay;

    // Token: 0x0400097C RID: 2428
    private readonly GelSurface stageGel;

    // Token: 0x0400097D RID: 2429
    private readonly TextBlock stageGlyph;

    // Token: 0x0400097E RID: 2430
    private readonly Grid controls;

    // Token: 0x0400097F RID: 2431
    private readonly ClickyDrawingWindowView.SpokenCaption caption;

    // Token: 0x04000980 RID: 2432
    private readonly ClickyDrawingWindowView.BeatScrubber scrubber;

    // Token: 0x04000981 RID: 2433
    private readonly TextBlock timeLabel;

    // Token: 0x04000982 RID: 2434
    private readonly Pressable playPause;

    // Token: 0x04000983 RID: 2435
    private readonly TextBlock playPauseGlyph;

    // Token: 0x04000984 RID: 2436
    private readonly Control focusTarget = new ContentControl
    {
      IsTabStop = true,
      Width = 1.0,
      Height = 1.0,
      Opacity = 0.0,
      HorizontalAlignment = HorizontalAlignment.Left,
      VerticalAlignment = VerticalAlignment.Top
    };

    // Token: 0x04000985 RID: 2437
    private readonly ClickyDrawingWindowView.DownloadButton download;

    // Token: 0x04000986 RID: 2438
    [Nullable(2)]
    private WriteableBitmap frameBitmap;

    // Token: 0x04000987 RID: 2439
    [Nullable(2)]
    private LessonPlaybackFrame pendingFrame;

    // Token: 0x04000988 RID: 2440
    [Nullable(2)]
    private LessonVideoRecorder recorder;

    // Token: 0x04000989 RID: 2441
    [Nullable(2)]
    private CancellationTokenSource downloadCancellation;

    // Token: 0x0400098A RID: 2442
    private bool showsStageButton = true;

    // Token: 0x020006E5 RID: 1765
    [NullableContext(2)]
    [Nullable(0)]
    private sealed class DownloadButton : Pressable
    {
      // Token: 0x170007AB RID: 1963
      // (get) Token: 0x0600338C RID: 13196 RVA: 0x0067692C File Offset: 0x0067692C
      // (set) Token: 0x0600338D RID: 13197 RVA: 0x00676934 File Offset: 0x00676934
      public ClickyDrawingWindowView.DownloadButton.Phase State { get; private set; }

      // Token: 0x170007AC RID: 1964
      // (get) Token: 0x0600338E RID: 13198 RVA: 0x00676940 File Offset: 0x00676940
      // (set) Token: 0x0600338F RID: 13199 RVA: 0x00676948 File Offset: 0x00676948
      public string SavedPath { get; private set; }

      // Token: 0x06003390 RID: 13200 RVA: 0x00676954 File Offset: 0x00676954
      public DownloadButton(Color ink)
        : base(true)
      {
        this.ink = ink.WithAlpha(0.8);
        base.Height = 22.0;
        base.Padding = new Thickness(10.0, 0.0, 10.0, 0.0);
        base.CornerRadius = new CornerRadius(11.0);
        base.Background = Ui.Brush(DS.White(0.55));
        base.BorderBrush = Ui.Brush(DS.Black(0.1));
        base.BorderThickness = new Thickness(0.5);
        base.Children.Add(this.content);
        this.Show(ClickyDrawingWindowView.DownloadButton.Phase.Idle, null);
      }

      // Token: 0x06003391 RID: 13201 RVA: 0x00676A60 File Offset: 0x00676A60
      public void Show(ClickyDrawingWindowView.DownloadButton.Phase phase, string savedPath = null)
      {
        this.State = phase;
        this.SavedPath = savedPath;
        this.content.Children.Clear();
        this.progressText = null;
        switch (phase)
        {
        case ClickyDrawingWindowView.DownloadButton.Phase.Idle:
          this.content.Children.Add(this.Glyph("\ue896", 11.0));
          this.content.Children.Add(this.Label("Download"));
          break;
        case ClickyDrawingWindowView.DownloadButton.Phase.Recording:
          this.content.Children.Add(this.Spinner());
          this.content.Children.Add(this.progressText = this.Label("Recording 0:00 / 0:00"));
          break;
        case ClickyDrawingWindowView.DownloadButton.Phase.Saving:
          this.content.Children.Add(this.Spinner());
          this.content.Children.Add(this.Label("Saving…"));
          break;
        case ClickyDrawingWindowView.DownloadButton.Phase.Saved:
          this.content.Children.Add(this.Glyph("\ue73e", 11.0));
          this.content.Children.Add(this.Label("In Downloads"));
          break;
        case ClickyDrawingWindowView.DownloadButton.Phase.Failed:
          this.content.Children.Add(this.Glyph("\ue7ba", 10.5));
          this.content.Children.Add(this.Label("Try again"));
          break;
        }
        bool flag = phase - ClickyDrawingWindowView.DownloadButton.Phase.Recording <= 1;
        base.Enabled = !flag;
        string text;
        switch (phase)
        {
        case ClickyDrawingWindowView.DownloadButton.Phase.Recording:
          text = "Recording it as it plays…";
          break;
        case ClickyDrawingWindowView.DownloadButton.Phase.Saving:
          text = "Saving the video…";
          break;
        case ClickyDrawingWindowView.DownloadButton.Phase.Saved:
          text = "Show the video in Explorer";
          break;
        default:
          text = "Save this drawing as a video in your Downloads folder (it plays through once while it records)";
          break;
        }
        ToolTipService.SetToolTip(this, text);
      }

      // Token: 0x06003392 RID: 13202 RVA: 0x00676C40 File Offset: 0x00676C40
      public void ShowRecordingProgress(double position, double duration)
      {
        if (this.progressText != null)
        {
          this.progressText.Text = "Recording " + ClickyDrawingLaptopScreen.LengthText(position) + " / " + ClickyDrawingLaptopScreen.LengthText(duration);
        }
      }

      // Token: 0x06003393 RID: 13203 RVA: 0x00676C70 File Offset: 0x00676C70
      [NullableContext(1)]
      private TextBlock Label(string text)
      {
        TextBlock textBlock = Ui.Text(text, 11.5, FontWeights.SemiBold, this.ink, TextAlignment.Left, 0, false, false);
        textBlock.VerticalAlignment = VerticalAlignment.Center;
        Typography.SetNumeralAlignment(textBlock, FontNumeralAlignment.Tabular);
        return textBlock;
      }

      // Token: 0x06003394 RID: 13204 RVA: 0x00676CA0 File Offset: 0x00676CA0
      [NullableContext(1)]
      private TextBlock Glyph(string glyph, double size)
      {
        TextBlock textBlock = Icons.Glyph(glyph, size, this.ink);
        textBlock.FontWeight = FontWeights.Bold;
        return textBlock;
      }

      // Token: 0x06003395 RID: 13205 RVA: 0x00676CBC File Offset: 0x00676CBC
      [NullableContext(1)]
      private ProgressRing Spinner()
      {
        return new ProgressRing
        {
          Width = 10.0,
          Height = 10.0,
          MinWidth = 0.0,
          MinHeight = 0.0,
          IsActive = true,
          Foreground = Ui.Brush(this.ink),
          VerticalAlignment = VerticalAlignment.Center
        };
      }

      // Token: 0x04001F13 RID: 7955
      [Nullable(1)]
      private readonly StackPanel content = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 5.0,
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center
      };

      // Token: 0x04001F14 RID: 7956
      private readonly Color ink;

      // Token: 0x04001F15 RID: 7957
      private TextBlock progressText;

      // Token: 0x02000AE0 RID: 2784
      [NullableContext(0)]
      public enum Phase
      {
        // Token: 0x04002ED6 RID: 11990
        Idle,
        // Token: 0x04002ED7 RID: 11991
        Recording,
        // Token: 0x04002ED8 RID: 11992
        Saving,
        // Token: 0x04002ED9 RID: 11993
        Saved,
        // Token: 0x04002EDA RID: 11994
        Failed
      }
    }

    // Token: 0x020006E6 RID: 1766
    [Nullable(0)]
    private sealed class SpokenCaption : Grid
    {
      // Token: 0x06003396 RID: 13206 RVA: 0x00676D2C File Offset: 0x00676D2C
      public SpokenCaption(StoredLesson.Meta lesson, double shift)
      {
        this.lesson = lesson;
        this.shift = shift;
        this.text = new TextBlock
        {
          FontSize = 17.0,
          FontWeight = FontWeights.Medium,
          FontFamily = DS.Fonts.Text,
          TextAlignment = TextAlignment.Center,
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Center,
          TextWrapping = TextWrapping.WrapWholeWords,
          MaxLines = 2,
          TextTrimming = TextTrimming.CharacterEllipsis,
          IsTextScaleFactorEnabled = false,
          LineStackingStrategy = LineStackingStrategy.BlockLineHeight,
          LineHeight = 23.4
        };
        base.Children.Add(this.text);
      }

      // Token: 0x06003397 RID: 13207 RVA: 0x00676DE4 File Offset: 0x00676DE4
      public void Show(int? beat, double position)
      {
        int? num = beat;
        int? num2 = this.shownBeat;
        if (!((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null))))
        {
          this.shownBeat = beat;
          this.text.Opacity = 0.0;
          Anim.Opacity(this.text, 1f, 0.2, Easing.EaseOut(Ui.Compositor), 0.0);
        }
        this.text.Inlines.Clear();
        if (beat != null)
        {
          int valueOrDefault = beat.GetValueOrDefault();
          if (valueOrDefault >= 0 && valueOrDefault < this.lesson.Lines.Count)
          {
            string[] array = this.lesson.Lines[valueOrDefault].Text.Split(' ', StringSplitOptions.None);
            double num3 = this.SpokenFraction(valueOrDefault, position + this.shift);
            double num4 = Math.Max(1.0, array.Sum<string>((string w) => (double)w.Length + 1.0));
            double num5 = 0.0;
            for (int i = 0; i < array.Length; i++)
            {
              double num6 = (double)array[i].Length + 1.0;
              double num7 = num5 / num4;
              double num8 = (num5 + num6) / num4;
              num5 += num6;
              double num9 = Math.Clamp((num3 - num7) / Math.Max(0.0001, num8 - num7), 0.0, 1.0);
              this.text.Inlines.Add(ClickyDrawingWindowView.SpokenCaption.Run((i + 1 < array.Length) ? (array[i] + " ") : array[i], num9));
            }
            return;
          }
        }
        if (this.lesson.Question.Length > 0)
        {
          this.text.Inlines.Add(ClickyDrawingWindowView.SpokenCaption.Run("“" + this.lesson.Question + "”", 0.0));
        }
      }

      // Token: 0x06003398 RID: 13208 RVA: 0x00677004 File Offset: 0x00677004
      private double SpokenFraction(int beat, double time)
      {
        StoredLesson.LineData lineData = this.lesson.Lines[beat];
        double num = lineData.Length ?? ((beat + 1 < this.lesson.Lines.Count) ? (this.lesson.Lines[beat + 1].Start - lineData.Start) : ((double)lineData.Text.Length * 0.065));
        if (num <= 0.0)
        {
          return 1.0;
        }
        return Math.Clamp((time - lineData.Start) / num, 0.0, 1.0);
      }

      // Token: 0x06003399 RID: 13209 RVA: 0x006770C0 File Offset: 0x006770C0
      private static Run Run(string words, double progress)
      {
        double num = ClickyDrawingWindowView.SpokenCaption.Unspoken.Item1 + (ClickyDrawingWindowView.SpokenCaption.Spoken.Item1 - ClickyDrawingWindowView.SpokenCaption.Unspoken.Item1) * progress;
        double num2 = ClickyDrawingWindowView.SpokenCaption.Unspoken.Item2 + (ClickyDrawingWindowView.SpokenCaption.Spoken.Item2 - ClickyDrawingWindowView.SpokenCaption.Unspoken.Item2) * progress;
        return new Run
        {
          Text = words,
          Foreground = Ui.Brush(DS.Rgb(num, num, num, num2))
        };
      }

      // Token: 0x04001F16 RID: 7958
      [TupleElementNames(new string[] { "White", "Opacity" })]
      [Nullable(0)]
      private static readonly ValueTuple<double, double> Unspoken = new ValueTuple<double, double>(0.56, 0.9);

      // Token: 0x04001F17 RID: 7959
      [TupleElementNames(new string[] { "White", "Opacity" })]
      [Nullable(0)]
      private static readonly ValueTuple<double, double> Spoken = new ValueTuple<double, double>(1.0, 0.96);

      // Token: 0x04001F18 RID: 7960
      private readonly StoredLesson.Meta lesson;

      // Token: 0x04001F19 RID: 7961
      private readonly double shift;

      // Token: 0x04001F1A RID: 7962
      private readonly TextBlock text;

      // Token: 0x04001F1B RID: 7963
      private int? shownBeat = new int?(-2);
    }

    // Token: 0x020006E7 RID: 1767
    [Nullable(0)]
    private sealed class BeatScrubber : Grid
    {
      // Token: 0x0600339B RID: 13211 RVA: 0x00677170 File Offset: 0x00677170
      public BeatScrubber(ClickyLessonInlinePlayer player)
      {
        ClickyDrawingWindowView.BeatScrubber <>4__this = this;
        this.player = player;
        List<StoredLesson.LineData> lines = player.Lesson.Lines;
        double replayShift = player.Stored.ReplayShift;
        double num = Math.Max(player.Duration, 0.1);
        for (int i = 0; i < lines.Count; i++)
        {
          double num2 = ((i == 0) ? 0.0 : (lines[i].Start - replayShift));
          double num3 = ((i + 1 < lines.Count) ? (lines[i + 1].Start - replayShift) : num);
          Border border = new Border
          {
            Height = 6.0,
            CornerRadius = new CornerRadius(3.0),
            Background = Ui.Brush(DS.White(0.85)),
            HorizontalAlignment = HorizontalAlignment.Left,
            Width = 0.0
          };
          Pressable pressable = new Pressable(true)
          {
            Height = 14.0
          };
          pressable.Children.Add(new Border
          {
            Height = 6.0,
            CornerRadius = new CornerRadius(3.0),
            Background = Ui.Brush(DS.White(0.14)),
            VerticalAlignment = VerticalAlignment.Center
          });
          border.VerticalAlignment = VerticalAlignment.Center;
          pressable.Children.Add(border);
          int beat = i;
          pressable.Click += delegate
          {
            player.SeekToBeat(beat);
          };
          ToolTipService.SetToolTip(pressable, lines[i].Text);
          this.segments.Add(new ValueTuple<double, double, Pressable, Border>(num2, num3, pressable, border));
          this.row.Children.Add(pressable);
        }
        base.Children.Add(this.row);
        base.SizeChanged += delegate(object _, SizeChangedEventArgs _)
        {
          <>4__this.Layout();
        };
      }

      // Token: 0x0600339C RID: 13212 RVA: 0x006773D0 File Offset: 0x006773D0
      [NullableContext(2)]
      public FrameworkElement SegmentForTests(int beat)
      {
        if (beat < 0 || beat >= this.segments.Count)
        {
          return null;
        }
        return this.segments[beat].Item3;
      }

      // Token: 0x0600339D RID: 13213 RVA: 0x006773F8 File Offset: 0x006773F8
      private void Layout()
      {
        double num = Math.Max(this.player.Duration, 0.1);
        double num2 = Math.Max(0.0, base.ActualWidth - 3.0 * (double)Math.Max(0, this.segments.Count - 1));
        foreach (ValueTuple<double, double, Pressable, Border> valueTuple in this.segments)
        {
          valueTuple.Item3.Width = Math.Max(4.0, num2 * (valueTuple.Item2 - valueTuple.Item1) / num);
        }
        this.Show(this.position);
      }

      // Token: 0x0600339E RID: 13214 RVA: 0x006774C8 File Offset: 0x006774C8
      public void Show(double position)
      {
        this.position = position;
        foreach (ValueTuple<double, double, Pressable, Border> valueTuple in this.segments)
        {
          double item = valueTuple.Item1;
          double item2 = valueTuple.Item2;
          Pressable item3 = valueTuple.Item3;
          FrameworkElement item4 = valueTuple.Item4;
          double num = Math.Clamp((position - item) / Math.Max(0.01, item2 - item), 0.0, 1.0);
          item4.Width = Math.Max(0.0, (double.IsNaN(item3.Width) ? 0.0 : item3.Width) * num);
        }
      }

      // Token: 0x04001F1C RID: 7964
      private const double Spacing = 3.0;

      // Token: 0x04001F1D RID: 7965
      private readonly ClickyLessonInlinePlayer player;

      // Token: 0x04001F1E RID: 7966
      [TupleElementNames(new string[] { "Start", "End", "Hit", "Played" })]
      [Nullable(new byte[] { 1, 0, 1, 1 })]
      private readonly List<ValueTuple<double, double, Pressable, Border>> segments = new List<ValueTuple<double, double, Pressable, Border>>();

      // Token: 0x04001F1F RID: 7967
      private readonly StackPanel row = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 3.0
      };

      // Token: 0x04001F20 RID: 7968
      private double position;
    }
  }
}
