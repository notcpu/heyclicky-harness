using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky
{
  // Token: 0x02000023 RID: 35
  [NullableContext(1)]
  public interface ICursorOverlay : IDisposable
  {
    // Token: 0x060001AC RID: 428
    void Show();

    // Token: 0x060001AD RID: 429
    void Hide();

    // Token: 0x060001AE RID: 430
    void SetColor(string hex);

    // Token: 0x060001AF RID: 431
    void SetVoiceState(VoiceState state, float level);

    // Token: 0x060001B0 RID: 432
    Task PointAtAsync(PointD screenPx, [Nullable(2)] string label, CancellationToken ct = default(CancellationToken));

    // Token: 0x060001B1 RID: 433
    void ReturnToCursor();

    // Token: 0x060001B2 RID: 434 RVA: 0x0001F570 File Offset: 0x0001F570
    void SetHoverDwell(double? seconds)
    {
    }

    // Token: 0x060001B3 RID: 435 RVA: 0x0001F574 File Offset: 0x0001F574
    void RidePen(IReadOnlyList<PenStrokePath> strokes)
    {
    }

    // Token: 0x060001B4 RID: 436 RVA: 0x0001F578 File Offset: 0x0001F578
    void EndPenRide()
    {
    }

    // Token: 0x060001B5 RID: 437 RVA: 0x0001F57C File Offset: 0x0001F57C
    void ShowClickTarget(ClickTargetRing ring)
    {
    }

    // Token: 0x060001B6 RID: 438 RVA: 0x0001F580 File Offset: 0x0001F580
    void HideClickTarget()
    {
    }

    // Token: 0x060001B7 RID: 439 RVA: 0x0001F584 File Offset: 0x0001F584
    void ShowMessage(string text)
    {
      this.ShowMessageAsync(text, true, 6.0, default(CancellationToken));
    }

    // Token: 0x060001B8 RID: 440 RVA: 0x0001F5AC File Offset: 0x0001F5AC
    Task ShowMessageAsync(string text, bool streamed = true, double holdSeconds = 6.0, CancellationToken ct = default(CancellationToken))
    {
      return Task.CompletedTask;
    }

    // Token: 0x060001B9 RID: 441 RVA: 0x0001F5B4 File Offset: 0x0001F5B4
    void HideMessage()
    {
    }

    // Token: 0x060001BA RID: 442 RVA: 0x0001F5B8 File Offset: 0x0001F5B8
    Task PlayWelcomeAsync()
    {
      return Task.CompletedTask;
    }

    // Token: 0x17000074 RID: 116
    // (get) Token: 0x060001BB RID: 443 RVA: 0x0001F5C0 File Offset: 0x0001F5C0
    // (set) Token: 0x060001BC RID: 444 RVA: 0x0001F5C4 File Offset: 0x0001F5C4
    bool ShakeToDetachEnabled
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    // Token: 0x17000075 RID: 117
    // (get) Token: 0x060001BD RID: 445 RVA: 0x0001F5C8 File Offset: 0x0001F5C8
    bool IsDetached
    {
      get
      {
        return false;
      }
    }

    // Token: 0x1400000E RID: 14
    // (add) Token: 0x060001BE RID: 446 RVA: 0x0001F5CC File Offset: 0x0001F5CC
    // (remove) Token: 0x060001BF RID: 447 RVA: 0x0001F5D0 File Offset: 0x0001F5D0
    [Nullable(2)]
    public virtual event Action DetachedCursorClicked
    {
      [NullableContext(2)]
      add
      {
      }
      [NullableContext(2)]
      remove
      {
      }
    }

    // Token: 0x1400000F RID: 15
    // (add) Token: 0x060001C0 RID: 448 RVA: 0x0001F5D4 File Offset: 0x0001F5D4
    // (remove) Token: 0x060001C1 RID: 449 RVA: 0x0001F5D8 File Offset: 0x0001F5D8
    [Nullable(2)]
    public virtual event Action CursorDetached
    {
      [NullableContext(2)]
      add
      {
      }
      [NullableContext(2)]
      remove
      {
      }
    }

    // Token: 0x14000010 RID: 16
    // (add) Token: 0x060001C2 RID: 450 RVA: 0x0001F5DC File Offset: 0x0001F5DC
    // (remove) Token: 0x060001C3 RID: 451 RVA: 0x0001F5E0 File Offset: 0x0001F5E0
    [Nullable(2)]
    public virtual event Action DetachedCursorAutoReattached
    {
      [NullableContext(2)]
      add
      {
      }
      [NullableContext(2)]
      remove
      {
      }
    }

    // Token: 0x060001C4 RID: 452 RVA: 0x0001F5E4 File Offset: 0x0001F5E4
    void EndDetach()
    {
    }

    // Token: 0x17000076 RID: 118
    // (get) Token: 0x060001C5 RID: 453 RVA: 0x0001F5E8 File Offset: 0x0001F5E8
    bool IsDocked
    {
      get
      {
        return false;
      }
    }

    // Token: 0x17000077 RID: 119
    // (get) Token: 0x060001C6 RID: 454 RVA: 0x0001F5EC File Offset: 0x0001F5EC
    bool IsSayingGoodbye
    {
      get
      {
        return false;
      }
    }

    // Token: 0x060001C7 RID: 455 RVA: 0x0001F5F0 File Offset: 0x0001F5F0
    Task DockAsync(PointD anchorPx)
    {
      return Task.CompletedTask;
    }

    // Token: 0x060001C8 RID: 456 RVA: 0x0001F5F8 File Offset: 0x0001F5F8
    Task SayGoodbyeAndDockAsync(string text)
    {
      return this.DockAsync(default(PointD));
    }

    // Token: 0x060001C9 RID: 457 RVA: 0x0001F614 File Offset: 0x0001F614
    Task UndockAsync(PointD anchorPx)
    {
      return Task.CompletedTask;
    }
  }
}
