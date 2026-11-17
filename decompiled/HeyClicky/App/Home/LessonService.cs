using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.OneBrain;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Brain;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Tools;
using HeyClicky.Core.Voice;
using HeyClicky.Drawing;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;

namespace HeyClicky.App.Home
{
  // Token: 0x020002B7 RID: 695
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class LessonService
  {
    // Token: 0x170004F8 RID: 1272
    // (get) Token: 0x06001CA8 RID: 7336 RVA: 0x005FB490 File Offset: 0x005FB490
    public bool IsReplaying
    {
      get
      {
        return this.replaying || ClickyDrawingWindowViewer.Shared.IsOpen;
      }
    }

    // Token: 0x170004F9 RID: 1273
    // (get) Token: 0x06001CA9 RID: 7337 RVA: 0x005FB4A8 File Offset: 0x005FB4A8
    public LessonStore Store { get; } = new LessonStore();

    // Token: 0x170004FA RID: 1274
    // (get) Token: 0x06001CAA RID: 7338 RVA: 0x005FB4B0 File Offset: 0x005FB4B0
    public IReadOnlyList<LessonService.Recent> RecentDrawings
    {
      get
      {
        return this.recent;
      }
    }

    // Token: 0x14000073 RID: 115
    // (add) Token: 0x06001CAB RID: 7339 RVA: 0x005FB4B8 File Offset: 0x005FB4B8
    // (remove) Token: 0x06001CAC RID: 7340 RVA: 0x005FB4F0 File Offset: 0x005FB4F0
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x170004FB RID: 1275
    // (get) Token: 0x06001CAD RID: 7341 RVA: 0x005FB528 File Offset: 0x005FB528
    // (set) Token: 0x06001CAE RID: 7342 RVA: 0x005FB530 File Offset: 0x005FB530
    [TupleElementNames(new string[] { "X", "Y" })]
    [Nullable(new byte[] { 2, 0 })]
    public Func<ValueTuple<int, int>> NotchPoint
    {
      [return: TupleElementNames(new string[] { "X", "Y" })]
      [return: Nullable(new byte[] { 2, 0 })]
      get;
      [param: TupleElementNames(new string[] { "X", "Y" })]
      [param: Nullable(new byte[] { 2, 0 })]
      set;
    }

    // Token: 0x06001CAF RID: 7343 RVA: 0x005FB53C File Offset: 0x005FB53C
    public LessonService(RealStack real, DispatcherQueue dq, bool excludeFromCapture, Func<string> cursorColor)
    {
      LessonService.<>c__DisplayClass25_0 CS$<>8__locals1 = new LessonService.<>c__DisplayClass25_0();
      CS$<>8__locals1.dq = dq;
      CS$<>8__locals1.real = real;
      base..ctor();
      CS$<>8__locals1.<>4__this = this;
      this.real = CS$<>8__locals1.real;
      this.dq = CS$<>8__locals1.dq;
      this.excludeFromCapture = excludeFromCapture;
      this.cursorColor = cursorColor;
      CS$<>8__locals1.real.Companion.DrawingTold += delegate([Nullable(2)] object _, DrawingRecord record)
      {
        _ = CS$<>8__locals1.<>4__this.OnToldAsync(record);
      };
      CS$<>8__locals1.real.Companion.DrawingLeftUp += delegate([Nullable(2)] object _, ScreenCaptureFrame frame)
      {
        CS$<>8__locals1.dq.TryEnqueue(delegate
        {
          if (!CS$<>8__locals1.real.Canvas.IsShowingDrawing || CS$<>8__locals1.<>4__this.replaying)
          {
            return;
          }
          CS$<>8__locals1.<>4__this.ShowOffer(frame.Display, null);
        });
      };
      CS$<>8__locals1.real.Canvas.Cleared += delegate([Nullable(2)] object _, EventArgs _)
      {
        DispatcherQueue dispatcherQueue = CS$<>8__locals1.dq;
        DispatcherQueueHandler dispatcherQueueHandler;
        if ((dispatcherQueueHandler = CS$<>8__locals1.<>9__6) == null)
        {
          dispatcherQueueHandler = (CS$<>8__locals1.<>9__6 = delegate
          {
            DrawingSaveOffer drawingSaveOffer = CS$<>8__locals1.<>4__this.offer;
            if (drawingSaveOffer == null)
            {
              return;
            }
            drawingSaveOffer.Hide();
          });
        }
        dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
      };
      this.Store.Changed += delegate
      {
        DispatcherQueue dispatcherQueue2 = CS$<>8__locals1.dq;
        DispatcherQueueHandler dispatcherQueueHandler2;
        if ((dispatcherQueueHandler2 = CS$<>8__locals1.<>9__7) == null)
        {
          dispatcherQueueHandler2 = (CS$<>8__locals1.<>9__7 = delegate
          {
            Action changed = CS$<>8__locals1.<>4__this.Changed;
            if (changed == null)
            {
              return;
            }
            changed();
          });
        }
        dispatcherQueue2.TryEnqueue(dispatcherQueueHandler2);
      };
      bool savesDrawings = this.SavesDrawings;
      CS$<>8__locals1.real.Core.Settings.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        if (CS$<>8__locals1.<>4__this.SavesDrawings == savesDrawings)
        {
          return;
        }
        savesDrawings = CS$<>8__locals1.<>4__this.SavesDrawings;
        Log.Info("lessons", "save drawings " + (savesDrawings ? "on" : "off"));
        DispatcherQueue dispatcherQueue3 = CS$<>8__locals1.dq;
        DispatcherQueueHandler dispatcherQueueHandler3;
        if ((dispatcherQueueHandler3 = CS$<>8__locals1.<>9__8) == null)
        {
          dispatcherQueueHandler3 = (CS$<>8__locals1.<>9__8 = delegate
          {
            Action changed2 = CS$<>8__locals1.<>4__this.Changed;
            if (changed2 == null)
            {
              return;
            }
            changed2();
          });
        }
        dispatcherQueue3.TryEnqueue(dispatcherQueueHandler3);
      };
      AgentToolHooks.SaveDrawing = new Func<string, Task<string>>(this.SaveFromToolAsync);
    }

    // Token: 0x170004FC RID: 1276
    // (get) Token: 0x06001CB0 RID: 7344 RVA: 0x005FB69C File Offset: 0x005FB69C
    public bool SavesDrawings
    {
      get
      {
        return this.real.Core.Settings.Read<bool>((AppSettings s) => s.SaveDrawingsEffective);
      }
    }

    // Token: 0x06001CB1 RID: 7345 RVA: 0x005FB6D4 File Offset: 0x005FB6D4
    public LessonPlaybackStage NewPlaybackStage(StoredLesson lesson)
    {
      return new LessonPlaybackStage((string query, CancellationToken token) => this.real.Canvas.PhotoAsync(query, token, lesson.PhotosFolder), this.real.Core.Settings.Read<bool>((AppSettings s) => s.DrawingSoundsEnabled));
    }

    // Token: 0x170004FD RID: 1277
    // (get) Token: 0x06001CB2 RID: 7346 RVA: 0x005FB738 File Offset: 0x005FB738
    public string CursorColorHex
    {
      get
      {
        return this.cursorColor();
      }
    }

    // Token: 0x170004FE RID: 1278
    // (get) Token: 0x06001CB3 RID: 7347 RVA: 0x005FB748 File Offset: 0x005FB748
    public bool ExcludesFromCapture
    {
      get
      {
        return this.excludeFromCapture;
      }
    }

    // Token: 0x06001CB4 RID: 7348 RVA: 0x005FB750 File Offset: 0x005FB750
    public void PlayInWindow(StoredLesson lesson)
    {
      ClickyDrawingWindowViewer.Shared.Present(lesson, this);
    }

    // Token: 0x06001CB5 RID: 7349 RVA: 0x005FB760 File Offset: 0x005FB760
    private void ShowOffer(DisplayInfo display, [Nullable(2)] LessonService.Recent entry)
    {
      if (DrawnOnboardingTour.IsRunning || this.real.Canvas.IsDrawingFadedAway)
      {
        return;
      }
      if (this.offer == null)
      {
        this.offer = new DrawingSaveOffer(this.excludeFromCapture);
      }
      this.offer.Show(display, (entry == null) ? null : new Action(delegate
      {
        this.SaveAsync(entry);
      }), delegate
      {
        Log.Info("lessons", "drawing cleared with the Clear button");
        this.real.Canvas.PlayClearSoundIfShowingDrawing();
        this.real.Canvas.Clear(true);
      });
    }

    // Token: 0x06001CB6 RID: 7350 RVA: 0x005FB7E4 File Offset: 0x005FB7E4
    private async Task OnToldAsync(DrawingRecord record)
    {
      LessonService.<>c__DisplayClass36_0 CS$<>8__locals1 = new LessonService.<>c__DisplayClass36_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.record = record;
      byte[] array = await Task.Run<byte[]>(delegate
      {
        byte[] array2;
        try
        {
          array2 = CS$<>8__locals1.<>4__this.real.Canvas.RenderDrawingPng(null, 1.0, true, false);
        }
        catch
        {
          array2 = null;
        }
        return array2;
      });
      object obj = this.recentGate;
      lock (obj)
      {
        IEnumerable<LessonService.Recent> enumerable;
        Func<LessonService.Recent, bool> func;
        do
        {
          CS$<>8__locals1.handle = Random.Shared.Next(65536).ToString("x4");
          enumerable = this.recent;
          if ((func = CS$<>8__locals1.<>9__2) == null)
          {
            LessonService.<>c__DisplayClass36_0 CS$<>8__locals2 = CS$<>8__locals1;
            Func<LessonService.Recent, bool> func2 = (LessonService.Recent r) => r.Handle == CS$<>8__locals1.handle;
            CS$<>8__locals2.<>9__2 = func2;
            func = func2;
          }
        }
        while (enumerable.Any<LessonService.Recent>(func));
      }
      CS$<>8__locals1.entry = new LessonService.Recent(CS$<>8__locals1.record, array)
      {
        Handle = CS$<>8__locals1.handle,
        Photos = this.real.Canvas.KnownPhotos(CS$<>8__locals1.record.Program)
      };
      if (CS$<>8__locals1.record.KeepsInChat)
      {
        obj = this.recentGate;
        lock (obj)
        {
          this.recent.Add(CS$<>8__locals1.entry);
          if (this.recent.Count > 5)
          {
            this.recent.RemoveAt(0);
          }
        }
        StoredLesson stored = await this.Store.SaveAsync(CS$<>8__locals1.record, array, CS$<>8__locals1.entry.Photos);
        if (stored != null)
        {
          CS$<>8__locals1.entry.Saved = stored;
          this.dq.TryEnqueue(delegate
          {
            Action changed = CS$<>8__locals1.<>4__this.Changed;
            if (changed != null)
            {
              changed();
            }
            ChatHandOffs handOffs = NotchChat.HandOffs;
            if (handOffs != null)
            {
              handOffs.NoteDrawingKept(stored.Data.Id, stored.Data.Title);
            }
            if (CS$<>8__locals1.<>4__this.real.Canvas.IsShowing)
            {
              CS$<>8__locals1.<>4__this.ShowOffer(CS$<>8__locals1.record.Screen.Display, null);
            }
          });
        }
      }
      else
      {
        BrainSession brainSession = this.real.Core.BrainSession;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(83, 2);
        defaultInterpolatedStringHandler.AppendLiteral("[clicky drawing] \"");
        defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.record.Title);
        defaultInterpolatedStringHandler.AppendLiteral("\" can be saved to the user's Drawings (drawing id: ");
        defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.handle);
        defaultInterpolatedStringHandler.AppendLiteral(") if they ask.");
        brainSession.NoteForNextTurn(defaultInterpolatedStringHandler.ToStringAndClear(), null);
        this.dq.TryEnqueue(delegate
        {
          object obj2 = CS$<>8__locals1.<>4__this.recentGate;
          lock (obj2)
          {
            CS$<>8__locals1.<>4__this.recent.Add(CS$<>8__locals1.entry);
            if (CS$<>8__locals1.<>4__this.recent.Count > 5)
            {
              CS$<>8__locals1.<>4__this.recent.RemoveAt(0);
            }
          }
          Action changed2 = CS$<>8__locals1.<>4__this.Changed;
          if (changed2 != null)
          {
            changed2();
          }
          if (CS$<>8__locals1.<>4__this.real.Canvas.IsShowing)
          {
            CS$<>8__locals1.<>4__this.ShowOffer(CS$<>8__locals1.record.Screen.Display, CS$<>8__locals1.entry);
          }
        });
      }
    }

    // Token: 0x06001CB7 RID: 7351 RVA: 0x005FB830 File Offset: 0x005FB830
    private async Task<string> SaveFromToolAsync([Nullable(2)] string handle)
    {
      LessonService.<>c__DisplayClass37_0 CS$<>8__locals1 = new LessonService.<>c__DisplayClass37_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.wanted = ((handle != null) ? handle.Trim().ToLowerInvariant() : null) ?? "";
      int attempt = 0;
      for (;;)
      {
        object obj = this.recentGate;
        lock (obj)
        {
          LessonService.<>c__DisplayClass37_0 CS$<>8__locals2 = CS$<>8__locals1;
          LessonService.Recent recent;
          if (CS$<>8__locals1.wanted.Length != 0)
          {
            IEnumerable<LessonService.Recent> enumerable = this.recent;
            Func<LessonService.Recent, bool> func;
            if ((func = CS$<>8__locals1.<>9__2) == null)
            {
              func = (CS$<>8__locals1.<>9__2 = (LessonService.Recent r) => r.Handle == CS$<>8__locals1.wanted);
            }
            recent = enumerable.LastOrDefault<LessonService.Recent>(func);
          }
          else
          {
            recent = this.recent.LastOrDefault<LessonService.Recent>();
          }
          CS$<>8__locals2.entry = recent;
        }
        if (CS$<>8__locals1.entry != null || attempt >= 10)
        {
          break;
        }
        await Task.Delay(100);
        attempt++;
      }
      string text;
      if (CS$<>8__locals1.entry == null)
      {
        Log.Info("lessons", "save_drawing: no recent drawing" + ((CS$<>8__locals1.wanted.Length > 0) ? (" with id " + CS$<>8__locals1.wanted) : ""));
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["status"] = "not_found";
        jsonObject["message"] = "There is no recent drawing to save: only explanations drawn in this session can be saved.";
        text = LessonService.<SaveFromToolAsync>g__Json|37_0(jsonObject);
      }
      else
      {
        StoredLesson saved = CS$<>8__locals1.entry.Saved;
        if (saved != null)
        {
          JsonObject jsonObject2 = new JsonObject(null);
          jsonObject2["status"] = "already_saved";
          jsonObject2["title"] = saved.Data.Title;
          jsonObject2["message"] = "That drawing is already in the user's Drawings.";
          text = LessonService.<SaveFromToolAsync>g__Json|37_0(jsonObject2);
        }
        else
        {
          CS$<>8__locals1.completion = new TaskCompletionSource<StoredLesson>(TaskCreationOptions.RunContinuationsAsynchronously);
          if (!this.dq.TryEnqueue(delegate
          {
            LessonService.<>c__DisplayClass37_0.<<SaveFromToolAsync>b__1>d <<SaveFromToolAsync>b__1>d;
            <<SaveFromToolAsync>b__1>d.<>t__builder = AsyncVoidMethodBuilder.Create();
            <<SaveFromToolAsync>b__1>d.<>4__this = CS$<>8__locals1;
            <<SaveFromToolAsync>b__1>d.<>1__state = -1;
            <<SaveFromToolAsync>b__1>d.<>t__builder.Start<LessonService.<>c__DisplayClass37_0.<<SaveFromToolAsync>b__1>d>(ref <<SaveFromToolAsync>b__1>d);
          }))
          {
            JsonObject jsonObject3 = new JsonObject(null);
            jsonObject3["status"] = "failed";
            jsonObject3["message"] = "The drawing couldn't be saved.";
            text = LessonService.<SaveFromToolAsync>g__Json|37_0(jsonObject3);
          }
          else
          {
            StoredLesson storedLesson = await CS$<>8__locals1.completion.Task;
            string text2 = "lessons";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 2);
            defaultInterpolatedStringHandler.AppendLiteral("save_drawing: ");
            defaultInterpolatedStringHandler.AppendFormatted((storedLesson == null) ? "failed" : ("saved \"" + storedLesson.Data.Title + "\""));
            defaultInterpolatedStringHandler.AppendLiteral(" (id ");
            defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.entry.Handle);
            defaultInterpolatedStringHandler.AppendLiteral(")");
            Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
            string text3;
            if (storedLesson != null)
            {
              JsonObject jsonObject4 = new JsonObject(null);
              jsonObject4["status"] = "saved";
              jsonObject4["title"] = storedLesson.Data.Title;
              jsonObject4["message"] = "Saved to the user's Drawings in Home.";
              text3 = LessonService.<SaveFromToolAsync>g__Json|37_0(jsonObject4);
            }
            else
            {
              JsonObject jsonObject5 = new JsonObject(null);
              jsonObject5["status"] = "failed";
              jsonObject5["message"] = "The drawing couldn't be saved.";
              text3 = LessonService.<SaveFromToolAsync>g__Json|37_0(jsonObject5);
            }
            text = text3;
          }
        }
      }
      return text;
    }

    // Token: 0x06001CB8 RID: 7352 RVA: 0x005FB87C File Offset: 0x005FB87C
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<StoredLesson> SaveAsync(LessonService.Recent entry)
    {
      StoredLesson saved = entry.Saved;
      StoredLesson storedLesson;
      if (saved != null)
      {
        storedLesson = saved;
      }
      else
      {
        DrawingSaveOffer drawingSaveOffer = this.offer;
        if (drawingSaveOffer != null)
        {
          drawingSaveOffer.Hide();
        }
        StoredLesson storedLesson2 = await this.Store.SaveAsync(entry.Record, entry.Poster, entry.Photos);
        StoredLesson stored = storedLesson2;
        if (stored == null)
        {
          storedLesson = null;
        }
        else
        {
          entry.Saved = stored;
          this.dq.TryEnqueue(delegate
          {
            Action changed = this.Changed;
            if (changed != null)
            {
              changed();
            }
            if (this.real.Canvas.IsShowing)
            {
              this.real.Canvas.Clear(true);
            }
            if (this.printer == null)
            {
              this.printer = new LessonPrinter(this.excludeFromCapture);
            }
            LessonPrinter lessonPrinter = this.printer;
            byte[] poster = entry.Poster;
            string title = stored.Data.Title;
            DisplayInfo display = entry.Record.Screen.Display;
            Func<ValueTuple<int, int>> notchPoint = this.NotchPoint;
            lessonPrinter.Print(poster, title, display, (notchPoint != null) ? new ValueTuple<int, int>?(notchPoint()) : null);
          });
          storedLesson = stored;
        }
      }
      return storedLesson;
    }

    // Token: 0x06001CB9 RID: 7353 RVA: 0x005FB8C8 File Offset: 0x005FB8C8
    public async Task PlayAsync(StoredLesson lesson, bool withVoice = true, bool onWhiteBoard = false)
    {
      LessonService.<>c__DisplayClass40_0 CS$<>8__locals1 = new LessonService.<>c__DisplayClass40_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.lesson = lesson;
      CancellationTokenSource cancellationTokenSource = this.replay;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      CS$<>8__locals1.cts = (this.replay = new CancellationTokenSource());
      CS$<>8__locals1.d = CS$<>8__locals1.lesson.Data;
      byte[] jpeg;
      try
      {
        string text = await File.ReadAllTextAsync(CS$<>8__locals1.lesson.ProgramPath, default(CancellationToken));
        CS$<>8__locals1.program = text;
        jpeg = await File.ReadAllBytesAsync(CS$<>8__locals1.lesson.ScreenPath, default(CancellationToken));
      }
      catch (Exception ex)
      {
        Log.Warn("lessons", "replay read: " + ex.Message);
        return;
      }
      CS$<>8__locals1.narration = ((!withVoice) ? null : (await Task.Run<ValueTuple<byte[], int>?>(new Func<ValueTuple<byte[], int>?>(CS$<>8__locals1.lesson.ReadNarration))));
      if (!CS$<>8__locals1.cts.IsCancellationRequested)
      {
        IReadOnlyList<DisplayInfo> displays = this.real.Capture.GetDisplays();
        DisplayInfo displayInfo;
        if ((displayInfo = displays.FirstOrDefault<DisplayInfo>((DisplayInfo x) => x.DeviceName == CS$<>8__locals1.d.DisplayDevice)) == null)
        {
          displayInfo = displays.FirstOrDefault<DisplayInfo>((DisplayInfo x) => x.IsPrimary) ?? displays.FirstOrDefault<DisplayInfo>();
        }
        DisplayInfo displayInfo2 = displayInfo;
        if (displayInfo2 != null)
        {
          byte[] array = (onWhiteBoard ? (LessonService.WhiteJpeg(CS$<>8__locals1.d.CanvasWidth, CS$<>8__locals1.d.CanvasHeight) ?? jpeg) : jpeg);
          CS$<>8__locals1.frame = new ScreenCaptureFrame(displayInfo2, array, CS$<>8__locals1.d.CanvasWidth, CS$<>8__locals1.d.CanvasHeight, false, new PointD(0.0, 0.0));
          string text2 = "lessons";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 5);
          defaultInterpolatedStringHandler.AppendLiteral("replaying \"");
          defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.d.Title);
          defaultInterpolatedStringHandler.AppendLiteral("\" (");
          defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.d.Lines.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" lines, ");
          defaultInterpolatedStringHandler.AppendFormatted<double>(CS$<>8__locals1.d.Duration, "0.0");
          defaultInterpolatedStringHandler.AppendLiteral(" s");
          defaultInterpolatedStringHandler.AppendFormatted((CS$<>8__locals1.narration != null) ? ", with its recorded voice" : ", no voice");
          defaultInterpolatedStringHandler.AppendFormatted(onWhiteBoard ? ", on a white board" : "");
          defaultInterpolatedStringHandler.AppendLiteral(")");
          Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
          this.real.Canvas.Clear(false);
          this.ShowBackdrop(displayInfo2, onWhiteBoard ? null : CS$<>8__locals1.lesson.ScreenPath);
          CS$<>8__locals1.subtitlesSetting = this.real.Core.Settings.Read<bool>((AppSettings x) => x.DrawingSubtitlesEffective);
          this.replaying = true;
          try
          {
            await CS$<>8__locals1.<PlayAsync>g__ReplayAsync|3();
          }
          finally
          {
            if (this.replay == CS$<>8__locals1.cts)
            {
              this.replaying = false;
              this.real.Canvas.ReplayPhotosFolder = null;
            }
          }
        }
      }
    }

    // Token: 0x06001CBA RID: 7354 RVA: 0x005FB924 File Offset: 0x005FB924
    public void StopReplay()
    {
      this.replaying = false;
      CancellationTokenSource cancellationTokenSource = this.replay;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      LessonVoice lessonVoice = this.replayVoice;
      if (lessonVoice != null)
      {
        lessonVoice.Stop();
      }
      this.real.Canvas.ReplayPhotosFolder = null;
      OverlayWindow overlayWindow = this.whiteBoard;
      if (overlayWindow == null)
      {
        return;
      }
      overlayWindow.HideWindow();
    }

    // Token: 0x06001CBB RID: 7355 RVA: 0x005FB980 File Offset: 0x005FB980
    public void StopReplayForEscape()
    {
      if (ClickyDrawingWindowViewer.Shared.IsOpen)
      {
        Log.Info("lessons", "drawing window closed (Escape)");
        ClickyDrawingWindowViewer.Shared.Dismiss(true);
        return;
      }
      Log.Info("lessons", "replay stopped (Escape)");
      this.StopReplay();
      this.real.Companion.Cancel();
      this.real.Canvas.IsStreamingNarration = false;
      this.real.Canvas.ShowsSubtitles = false;
      this.real.Canvas.Clear(true);
    }

    // Token: 0x06001CBC RID: 7356 RVA: 0x005FBA0C File Offset: 0x005FBA0C
    private void ShowBackdrop(DisplayInfo display, [Nullable(2)] string screenPath)
    {
      if (this.whiteBoard == null)
      {
        this.whiteBoard = new OverlayWindow("HeyClicky board", this.excludeFromCapture, false, false, true, false, false);
        this.whiteBoard.Root.PointerPressed += delegate(object _, PointerRoutedEventArgs e)
        {
          e.Handled = true;
          if (!this.replaying)
          {
            return;
          }
          Log.Info("lessons", "replay stopped (a click)");
          this.StopReplay();
          this.real.Canvas.IsStreamingNarration = false;
          this.real.Canvas.Clear(true);
        };
      }
      Panel root = this.whiteBoard.Root;
      Brush brush;
      if (screenPath != null)
      {
        ImageBrush imageBrush = new ImageBrush();
        imageBrush.ImageSource = new BitmapImage(new Uri(screenPath));
        imageBrush.Stretch = Stretch.UniformToFill;
        brush = imageBrush;
        imageBrush.AlignmentY = AlignmentY.Top;
      }
      else
      {
        brush = Ui.Brush(Colors.White);
      }
      root.Background = brush;
      this.whiteBoard.PlacePx((int)display.BoundsPx.X, (int)display.BoundsPx.Y, (int)display.BoundsPx.Width, (int)display.BoundsPx.Height);
      this.whiteBoard.ShowNoActivate();
      IntPtr intPtr = LessonService.FindWindowW(null, "Clicky drawing");
      if (intPtr != 0)
      {
        LessonService.SetWindowPos(this.whiteBoard.Hwnd, intPtr, 0, 0, 0, 0, 19U);
      }
    }

    // Token: 0x06001CBD RID: 7357 RVA: 0x005FBB14 File Offset: 0x005FBB14
    private void EndReplayWhenCleared(CancellationTokenSource cts)
    {
      LessonService.<>c__DisplayClass44_0 CS$<>8__locals1 = new LessonService.<>c__DisplayClass44_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.cts = cts;
      this.real.Canvas.Cleared += CS$<>8__locals1.<EndReplayWhenCleared>g__OnCleared|0;
      CS$<>8__locals1.cts.Token.Register(delegate
      {
        DispatcherQueue dispatcherQueue = CS$<>8__locals1.<>4__this.dq;
        DispatcherQueueHandler dispatcherQueueHandler;
        if ((dispatcherQueueHandler = CS$<>8__locals1.<>9__3) == null)
        {
          dispatcherQueueHandler = (CS$<>8__locals1.<>9__3 = delegate
          {
            CS$<>8__locals1.<>4__this.real.Canvas.Cleared -= base.<EndReplayWhenCleared>g__OnCleared|0;
          });
        }
        dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
      });
    }

    // Token: 0x06001CBE RID: 7358
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr FindWindowW([Nullable(2)] string cls, string title);

    // Token: 0x06001CBF RID: 7359
    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr h, IntPtr after, int x, int y, int w, int ht, uint flags);

    // Token: 0x06001CC0 RID: 7360 RVA: 0x005FBB74 File Offset: 0x005FBB74
    [NullableContext(2)]
    private static byte[] WhiteJpeg(int w, int h)
    {
      LessonService.<>c__DisplayClass47_0 CS$<>8__locals1 = new LessonService.<>c__DisplayClass47_0();
      CS$<>8__locals1.w = w;
      CS$<>8__locals1.h = h;
      byte[] array;
      try
      {
        array = Task.Run<byte[]>(delegate
        {
          LessonService.<>c__DisplayClass47_0.<<WhiteJpeg>b__0>d <<WhiteJpeg>b__0>d;
          <<WhiteJpeg>b__0>d.<>t__builder = AsyncTaskMethodBuilder<byte[]>.Create();
          <<WhiteJpeg>b__0>d.<>4__this = CS$<>8__locals1;
          <<WhiteJpeg>b__0>d.<>1__state = -1;
          <<WhiteJpeg>b__0>d.<>t__builder.Start<LessonService.<>c__DisplayClass47_0.<<WhiteJpeg>b__0>d>(ref <<WhiteJpeg>b__0>d);
          return <<WhiteJpeg>b__0>d.<>t__builder.Task;
        }).GetAwaiter().GetResult();
      }
      catch
      {
        array = null;
      }
      return array;
    }

    // Token: 0x06001CC1 RID: 7361 RVA: 0x005FBBCC File Offset: 0x005FBBCC
    [CompilerGenerated]
    internal static string <SaveFromToolAsync>g__Json|37_0(JsonObject o)
    {
      return o.ToJsonString(null);
    }

    // Token: 0x04000DB6 RID: 3510
    private readonly RealStack real;

    // Token: 0x04000DB7 RID: 3511
    private readonly DispatcherQueue dq;

    // Token: 0x04000DB8 RID: 3512
    private readonly Func<string> cursorColor;

    // Token: 0x04000DB9 RID: 3513
    private readonly List<LessonService.Recent> recent = new List<LessonService.Recent>();

    // Token: 0x04000DBA RID: 3514
    private readonly bool excludeFromCapture;

    // Token: 0x04000DBB RID: 3515
    [Nullable(2)]
    private DrawingSaveOffer offer;

    // Token: 0x04000DBC RID: 3516
    [Nullable(2)]
    private LessonPrinter printer;

    // Token: 0x04000DBD RID: 3517
    [Nullable(2)]
    private OverlayWindow whiteBoard;

    // Token: 0x04000DBE RID: 3518
    [Nullable(2)]
    private CancellationTokenSource replay;

    // Token: 0x04000DBF RID: 3519
    private volatile bool replaying;

    // Token: 0x04000DC3 RID: 3523
    private readonly object recentGate = new object();

    // Token: 0x04000DC4 RID: 3524
    [Nullable(2)]
    private LessonVoice replayVoice;

    // Token: 0x0200089B RID: 2203
    [Nullable(0)]
    public sealed class Recent : IEquatable<LessonService.Recent>
    {
      // Token: 0x06003A07 RID: 14855 RVA: 0x0068B8B8 File Offset: 0x0068B8B8
      public Recent(DrawingRecord Record, [Nullable(2)] byte[] Poster)
      {
        this.Record = Record;
        this.Poster = Poster;
        this.Handle = "";
        this.Photos = new Dictionary<string, byte[]>();
        base..ctor();
      }

      // Token: 0x1700081A RID: 2074
      // (get) Token: 0x06003A08 RID: 14856 RVA: 0x0068B8E4 File Offset: 0x0068B8E4
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(LessonService.Recent);
        }
      }

      // Token: 0x1700081B RID: 2075
      // (get) Token: 0x06003A09 RID: 14857 RVA: 0x0068B8F0 File Offset: 0x0068B8F0
      // (set) Token: 0x06003A0A RID: 14858 RVA: 0x0068B8F8 File Offset: 0x0068B8F8
      public DrawingRecord Record { get; set; }

      // Token: 0x1700081C RID: 2076
      // (get) Token: 0x06003A0B RID: 14859 RVA: 0x0068B904 File Offset: 0x0068B904
      // (set) Token: 0x06003A0C RID: 14860 RVA: 0x0068B90C File Offset: 0x0068B90C
      [Nullable(2)]
      public byte[] Poster
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x1700081D RID: 2077
      // (get) Token: 0x06003A0D RID: 14861 RVA: 0x0068B918 File Offset: 0x0068B918
      // (set) Token: 0x06003A0E RID: 14862 RVA: 0x0068B920 File Offset: 0x0068B920
      [Nullable(2)]
      public StoredLesson Saved
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x1700081E RID: 2078
      // (get) Token: 0x06003A0F RID: 14863 RVA: 0x0068B92C File Offset: 0x0068B92C
      // (set) Token: 0x06003A10 RID: 14864 RVA: 0x0068B934 File Offset: 0x0068B934
      public string Handle { get; set; }

      // Token: 0x1700081F RID: 2079
      // (get) Token: 0x06003A11 RID: 14865 RVA: 0x0068B940 File Offset: 0x0068B940
      // (set) Token: 0x06003A12 RID: 14866 RVA: 0x0068B948 File Offset: 0x0068B948
      public IReadOnlyDictionary<string, byte[]> Photos { get; set; }

      // Token: 0x06003A13 RID: 14867 RVA: 0x0068B954 File Offset: 0x0068B954
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Recent");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003A14 RID: 14868 RVA: 0x0068B9A0 File Offset: 0x0068B9A0
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Record = ");
        builder.Append(this.Record);
        builder.Append(", Poster = ");
        builder.Append(this.Poster);
        builder.Append(", Saved = ");
        builder.Append(this.Saved);
        builder.Append(", Handle = ");
        builder.Append(this.Handle);
        builder.Append(", Photos = ");
        builder.Append(this.Photos);
        return true;
      }

      // Token: 0x06003A15 RID: 14869 RVA: 0x0068BA30 File Offset: 0x0068BA30
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(LessonService.Recent left, LessonService.Recent right)
      {
        return !(left == right);
      }

      // Token: 0x06003A16 RID: 14870 RVA: 0x0068BA3C File Offset: 0x0068BA3C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(LessonService.Recent left, LessonService.Recent right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003A17 RID: 14871 RVA: 0x0068BA50 File Offset: 0x0068BA50
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<DrawingRecord>.Default.GetHashCode(this.<Record>k__BackingField)) * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(this.<Poster>k__BackingField)) * -1521134295 + EqualityComparer<StoredLesson>.Default.GetHashCode(this.<Saved>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Handle>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyDictionary<string, byte[]>>.Default.GetHashCode(this.<Photos>k__BackingField);
      }

      // Token: 0x06003A18 RID: 14872 RVA: 0x0068BAE0 File Offset: 0x0068BAE0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as LessonService.Recent);
      }

      // Token: 0x06003A19 RID: 14873 RVA: 0x0068BAF0 File Offset: 0x0068BAF0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(LessonService.Recent other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<DrawingRecord>.Default.Equals(this.<Record>k__BackingField, other.<Record>k__BackingField) && EqualityComparer<byte[]>.Default.Equals(this.<Poster>k__BackingField, other.<Poster>k__BackingField) && EqualityComparer<StoredLesson>.Default.Equals(this.<Saved>k__BackingField, other.<Saved>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Handle>k__BackingField, other.<Handle>k__BackingField) && EqualityComparer<IReadOnlyDictionary<string, byte[]>>.Default.Equals(this.<Photos>k__BackingField, other.<Photos>k__BackingField));
      }

      // Token: 0x06003A1B RID: 14875 RVA: 0x0068BBA0 File Offset: 0x0068BBA0
      [CompilerGenerated]
      private Recent(LessonService.Recent original)
      {
        this.Record = original.<Record>k__BackingField;
        this.Poster = original.<Poster>k__BackingField;
        this.Saved = original.<Saved>k__BackingField;
        this.Handle = original.<Handle>k__BackingField;
        this.Photos = original.<Photos>k__BackingField;
      }

      // Token: 0x06003A1C RID: 14876 RVA: 0x0068BBF0 File Offset: 0x0068BBF0
      [CompilerGenerated]
      public void Deconstruct(out DrawingRecord Record, [Nullable(2)] out byte[] Poster)
      {
        Record = this.Record;
        Poster = this.Poster;
      }
    }
  }
}
