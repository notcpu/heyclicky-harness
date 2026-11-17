using System;
using System.Runtime.CompilerServices;

namespace HeyClicky
{
  // Token: 0x02000021 RID: 33
  [NullableContext(1)]
  public interface IScreenDrawingCanvas : IDisposable
  {
    // Token: 0x1400000C RID: 12
    // (add) Token: 0x06000178 RID: 376
    // (remove) Token: 0x06000179 RID: 377
    [Nullable(new byte[] { 2, 1 })]
    event EventHandler<DrawingAction> ActionRequested;

    // Token: 0x17000065 RID: 101
    // (get) Token: 0x0600017A RID: 378
    bool IsShowing { get; }

    // Token: 0x17000066 RID: 102
    // (get) Token: 0x0600017B RID: 379 RVA: 0x0001F254 File Offset: 0x0001F254
    bool IsShowingDrawing
    {
      get
      {
        return this.IsShowing;
      }
    }

    // Token: 0x0600017C RID: 380
    void BeginProgram(ScreenCaptureFrame screenshot, string cursorColorHex);

    // Token: 0x0600017D RID: 381
    DrawingFeedResult Feed(string codeSoFar, bool done);

    // Token: 0x0600017E RID: 382
    void ReleaseLine(int index);

    // Token: 0x0600017F RID: 383
    void SetLineLength(int index, double seconds);

    // Token: 0x06000180 RID: 384
    void Clear(bool animated = true);

    // Token: 0x06000181 RID: 385 RVA: 0x0001F25C File Offset: 0x0001F25C
    void Finish()
    {
    }

    // Token: 0x17000067 RID: 103
    // (get) Token: 0x06000182 RID: 386 RVA: 0x0001F260 File Offset: 0x0001F260
    // (set) Token: 0x06000183 RID: 387 RVA: 0x0001F264 File Offset: 0x0001F264
    bool IsStreamingNarration
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    // Token: 0x06000184 RID: 388 RVA: 0x0001F268 File Offset: 0x0001F268
    bool CanContinueDrawing(ScreenCaptureFrame capture)
    {
      return false;
    }

    // Token: 0x06000185 RID: 389 RVA: 0x0001F26C File Offset: 0x0001F26C
    [NullableContext(2)]
    DrawingContinuation ContinueStream()
    {
      return null;
    }

    // Token: 0x06000186 RID: 390 RVA: 0x0001F270 File Offset: 0x0001F270
    [NullableContext(2)]
    string DrawingStateForModel()
    {
      return null;
    }

    // Token: 0x17000068 RID: 104
    // (get) Token: 0x06000187 RID: 391 RVA: 0x0001F274 File Offset: 0x0001F274
    bool IsSwingingFiguresToFront
    {
      get
      {
        return false;
      }
    }

    // Token: 0x06000188 RID: 392 RVA: 0x0001F278 File Offset: 0x0001F278
    [return: Nullable(2)]
    ScreenCaptureFrame CaptureDrawingForModel(ScreenCaptureFrame capture)
    {
      return null;
    }

    // Token: 0x1400000D RID: 13
    // (add) Token: 0x06000189 RID: 393 RVA: 0x0001F27C File Offset: 0x0001F27C
    // (remove) Token: 0x0600018A RID: 394 RVA: 0x0001F280 File Offset: 0x0001F280
    [Nullable(new byte[] { 2, 1 })]
    public virtual event EventHandler<string> UserActedOnScreen
    {
      add
      {
      }
      remove
      {
      }
    }

    // Token: 0x17000069 RID: 105
    // (get) Token: 0x0600018B RID: 395 RVA: 0x0001F284 File Offset: 0x0001F284
    bool LatestAnswerPointsAtScreen
    {
      get
      {
        return false;
      }
    }

    // Token: 0x0600018C RID: 396 RVA: 0x0001F288 File Offset: 0x0001F288
    void ForgetWhetherLatestAnswerPointsAtScreen()
    {
    }

    // Token: 0x0600018D RID: 397 RVA: 0x0001F28C File Offset: 0x0001F28C
    void PlayClearSoundIfShowingDrawing()
    {
    }

    // Token: 0x1700006A RID: 106
    // (get) Token: 0x0600018E RID: 398 RVA: 0x0001F290 File Offset: 0x0001F290
    // (set) Token: 0x0600018F RID: 399 RVA: 0x0001F294 File Offset: 0x0001F294
    bool HidesLoneSources
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    // Token: 0x1700006B RID: 107
    // (get) Token: 0x06000190 RID: 400 RVA: 0x0001F298 File Offset: 0x0001F298
    int InkedMarkCount
    {
      get
      {
        return 0;
      }
    }

    // Token: 0x06000191 RID: 401 RVA: 0x0001F29C File Offset: 0x0001F29C
    void ShowLiveCaption(string line, double revealSeconds, double holdSeconds)
    {
    }

    // Token: 0x06000192 RID: 402 RVA: 0x0001F2A0 File Offset: 0x0001F2A0
    void FadeDrawingKeepingAskButtons()
    {
    }

    // Token: 0x06000193 RID: 403 RVA: 0x0001F2A4 File Offset: 0x0001F2A4
    void SetDrawingOpacity(double opacity, double durationSeconds)
    {
    }

    // Token: 0x06000194 RID: 404 RVA: 0x0001F2A8 File Offset: 0x0001F2A8
    void SetBelowNotch(bool isBelow)
    {
    }

    // Token: 0x1700006C RID: 108
    // (get) Token: 0x06000195 RID: 405 RVA: 0x0001F2AC File Offset: 0x0001F2AC
    bool IsDrawingFadedAway
    {
      get
      {
        return false;
      }
    }

    // Token: 0x1700006D RID: 109
    // (get) Token: 0x06000196 RID: 406 RVA: 0x0001F2B0 File Offset: 0x0001F2B0
    [Nullable(2)]
    DisplayInfo DrawingDisplay
    {
      [NullableContext(2)]
      get
      {
        return null;
      }
    }

    // Token: 0x1700006E RID: 110
    // (get) Token: 0x06000197 RID: 407 RVA: 0x0001F2B4 File Offset: 0x0001F2B4
    DateTime? LatestLineReleasedAt
    {
      get
      {
        return null;
      }
    }

    // Token: 0x1700006F RID: 111
    // (get) Token: 0x06000198 RID: 408 RVA: 0x0001F2CC File Offset: 0x0001F2CC
    // (set) Token: 0x06000199 RID: 409 RVA: 0x0001F2E4 File Offset: 0x0001F2E4
    DateTime? NarrationAudioEndsAt
    {
      get
      {
        return null;
      }
      set
      {
      }
    }
  }
}
