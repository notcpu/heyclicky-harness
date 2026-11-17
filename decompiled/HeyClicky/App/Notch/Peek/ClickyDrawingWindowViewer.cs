using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Home;
using HeyClicky.App.Shell;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200016C RID: 364
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyDrawingWindowViewer
  {
    // Token: 0x170003A0 RID: 928
    // (get) Token: 0x06001401 RID: 5121 RVA: 0x005A79B0 File Offset: 0x005A79B0
    public static ClickyDrawingWindowViewer Shared { get; } = new ClickyDrawingWindowViewer();

    // Token: 0x170003A1 RID: 929
    // (get) Token: 0x06001402 RID: 5122 RVA: 0x005A79B8 File Offset: 0x005A79B8
    public bool IsOpen
    {
      get
      {
        return this.isOpen;
      }
    }

    // Token: 0x170003A2 RID: 930
    // (get) Token: 0x06001403 RID: 5123 RVA: 0x005A79C4 File Offset: 0x005A79C4
    // (set) Token: 0x06001404 RID: 5124 RVA: 0x005A79CC File Offset: 0x005A79CC
    public bool IsRecording { get; set; }

    // Token: 0x06001405 RID: 5125 RVA: 0x005A79D8 File Offset: 0x005A79D8
    public async void Present(StoredLesson stored, LessonService service)
    {
      Displays.Monitor monitor = Displays.UnderCursor();
      this.Dismiss(false);
      int num = this.presentation + 1;
      this.presentation = num;
      int current = num;
      ClickyLessonInlinePlayer newPlayer = new ClickyLessonInlinePlayer(stored, service);
      double scale = monitor.Scale;
      Vector2 screenSize = new Vector2((float)((double)monitor.Bounds.Width / scale), (float)((double)monitor.Bounds.Height / scale));
      Vector2 stageSize = ClickyDrawingWindowView.StageSize(screenSize, stored);
      TaskAwaiter<bool> taskAwaiter = newPlayer.LoadAsync((int)Math.Round((double)stageSize.X * scale)).GetAwaiter();
      if (!taskAwaiter.IsCompleted)
      {
        await taskAwaiter;
        TaskAwaiter<bool> taskAwaiter2;
        taskAwaiter = taskAwaiter2;
        taskAwaiter2 = default(TaskAwaiter<bool>);
      }
      if (!taskAwaiter.GetResult() || current != this.presentation)
      {
        newPlayer.Dispose();
        if (current == this.presentation)
        {
          Log.Warn("lessons", "drawing window: the playback engine didn't load; replaying full screen");
          service.PlayAsync(stored, true, false);
        }
      }
      else
      {
        if (this.window == null)
        {
          this.window = new OverlayWindow("HeyClicky drawing", service.ExcludesFromCapture, false, true, true, false, false);
          this.window.Activated += delegate(object _, WindowActivatedEventArgs e)
          {
            if (e.WindowActivationState != WindowActivationState.Deactivated)
            {
              ClickyDrawingWindowView clickyDrawingWindowView = this.view;
              if (clickyDrawingWindowView == null)
              {
                return;
              }
              clickyDrawingWindowView.TakeKeyboardFocus();
            }
          };
        }
        this.player = newPlayer;
        this.view = new ClickyDrawingWindowView(newPlayer, screenSize, stageSize, scale, delegate
        {
          this.Dismiss(true);
        }, delegate
        {
          this.Dismiss(false);
          service.PlayAsync(stored, true, false);
        });
        this.window.Root.Children.Clear();
        this.window.Root.Children.Add(this.view);
        this.window.PlacePx(monitor.Bounds.Left, monitor.Bounds.Top, monitor.Bounds.Width, monitor.Bounds.Height);
        this.window.ShowActivated();
        this.isOpen = true;
        this.view.TakeKeyboardFocus();
        this.view.Appear();
        newPlayer.Play();
        NotchPeekSurface.PeekAction("drawing_window_opened", null);
        string text = "lessons";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 4);
        defaultInterpolatedStringHandler.AppendLiteral("drawing window: \"");
        defaultInterpolatedStringHandler.AppendFormatted(stored.Data.Title);
        defaultInterpolatedStringHandler.AppendLiteral("\" (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(stored.Data.Lines.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" lines, stage ");
        defaultInterpolatedStringHandler.AppendFormatted<float>(stageSize.X, "0");
        defaultInterpolatedStringHandler.AppendLiteral("x");
        defaultInterpolatedStringHandler.AppendFormatted<float>(stageSize.Y, "0");
        defaultInterpolatedStringHandler.AppendLiteral(" pt)");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      }
    }

    // Token: 0x06001406 RID: 5126 RVA: 0x005A7A20 File Offset: 0x005A7A20
    public void Dismiss(bool animated)
    {
      this.presentation++;
      OverlayWindow shownWindow = this.window;
      if (shownWindow != null)
      {
        ClickyDrawingWindowView shownView = this.view;
        if (shownView != null)
        {
          this.isOpen = false;
          ClickyLessonInlinePlayer clickyLessonInlinePlayer = this.player;
          if (clickyLessonInlinePlayer != null)
          {
            clickyLessonInlinePlayer.Dispose();
          }
          this.player = null;
          this.IsRecording = false;
          this.view = null;
          shownView.Closing();
          if (!animated)
          {
            ClickyDrawingWindowViewer.Hide(shownWindow, shownView);
            return;
          }
          shownView.Disappear();
          DispatcherQueueTimer dispatcherQueueTimer = DispatcherQueue.GetForCurrentThread().CreateTimer().Rooted();
          dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(0.22);
          dispatcherQueueTimer.IsRepeating = false;
          dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
          {
            if (this.view == null)
            {
              ClickyDrawingWindowViewer.Hide(shownWindow, shownView);
            }
          };
          dispatcherQueueTimer.Start();
          return;
        }
      }
    }

    // Token: 0x06001407 RID: 5127 RVA: 0x005A7B0C File Offset: 0x005A7B0C
    private static void Hide(OverlayWindow shownWindow, ClickyDrawingWindowView shownView)
    {
      shownWindow.HideWindow();
      shownWindow.Root.Children.Remove(shownView);
    }

    // Token: 0x04000967 RID: 2407
    [Nullable(2)]
    private OverlayWindow window;

    // Token: 0x04000968 RID: 2408
    [Nullable(2)]
    private ClickyDrawingWindowView view;

    // Token: 0x04000969 RID: 2409
    [Nullable(2)]
    private ClickyLessonInlinePlayer player;

    // Token: 0x0400096A RID: 2410
    private int presentation;

    // Token: 0x0400096B RID: 2411
    private volatile bool isOpen;
  }
}
