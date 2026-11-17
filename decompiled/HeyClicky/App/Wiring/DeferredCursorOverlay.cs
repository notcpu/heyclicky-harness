using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Platform.Overlay;

namespace HeyClicky.App.Wiring
{
  // Token: 0x02000095 RID: 149
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class DeferredCursorOverlay : ICursorOverlay, IDisposable
  {
    // Token: 0x170000E1 RID: 225
    // (get) Token: 0x060005E2 RID: 1506 RVA: 0x0052E75C File Offset: 0x0052E75C
    public CursorOverlay Inner
    {
      get
      {
        return this.inner;
      }
    }

    // Token: 0x14000005 RID: 5
    // (add) Token: 0x060005E3 RID: 1507 RVA: 0x0052E768 File Offset: 0x0052E768
    // (remove) Token: 0x060005E4 RID: 1508 RVA: 0x0052E7A0 File Offset: 0x0052E7A0
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<CursorOverlay> Attached;

    // Token: 0x060005E5 RID: 1509 RVA: 0x0052E7D8 File Offset: 0x0052E7D8
    [NullableContext(1)]
    public void Attach(CursorOverlay real)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.inner = real;
        if (this.color != null)
        {
          real.SetColor(this.color);
        }
        real.ShakeToDetachEnabled = this.shakeToDetach;
        real.DetachedCursorClicked += delegate
        {
          Action detachedCursorClicked = this.DetachedCursorClicked;
          if (detachedCursorClicked == null)
          {
            return;
          }
          detachedCursorClicked();
        };
        real.CursorDetached += delegate
        {
          Action cursorDetached = this.CursorDetached;
          if (cursorDetached == null)
          {
            return;
          }
          cursorDetached();
        };
        real.DetachedCursorAutoReattached += delegate
        {
          Action detachedCursorAutoReattached = this.DetachedCursorAutoReattached;
          if (detachedCursorAutoReattached == null)
          {
            return;
          }
          detachedCursorAutoReattached();
        };
        if (this.dockedAtStart)
        {
          real.DockAsync(default(PointD));
        }
        if (this.shown)
        {
          real.Show();
        }
      }
      Action<CursorOverlay> attached = this.Attached;
      if (attached == null)
      {
        return;
      }
      attached(real);
    }

    // Token: 0x060005E6 RID: 1510 RVA: 0x0052E8A8 File Offset: 0x0052E8A8
    [NullableContext(1)]
    public Task ShowMessageAsync(string text, bool streamed = true, double holdSeconds = 6.0, CancellationToken ct = default(CancellationToken))
    {
      CursorOverlay cursorOverlay = this.inner;
      return ((cursorOverlay != null) ? cursorOverlay.ShowMessageAsync(text, streamed, holdSeconds, ct) : null) ?? Task.CompletedTask;
    }

    // Token: 0x060005E7 RID: 1511 RVA: 0x0052E8CC File Offset: 0x0052E8CC
    public void HideMessage()
    {
      CursorOverlay cursorOverlay = this.inner;
      if (cursorOverlay == null)
      {
        return;
      }
      cursorOverlay.HideMessage();
    }

    // Token: 0x060005E8 RID: 1512 RVA: 0x0052E8E0 File Offset: 0x0052E8E0
    [NullableContext(1)]
    public Task PlayWelcomeAsync()
    {
      CursorOverlay cursorOverlay = this.inner;
      return ((cursorOverlay != null) ? cursorOverlay.PlayWelcomeAsync() : null) ?? Task.CompletedTask;
    }

    // Token: 0x170000E2 RID: 226
    // (get) Token: 0x060005E9 RID: 1513 RVA: 0x0052E900 File Offset: 0x0052E900
    // (set) Token: 0x060005EA RID: 1514 RVA: 0x0052E908 File Offset: 0x0052E908
    public bool ShakeToDetachEnabled
    {
      get
      {
        return this.shakeToDetach;
      }
      set
      {
        object obj = this.gate;
        lock (obj)
        {
          this.shakeToDetach = value;
          CursorOverlay cursorOverlay = this.inner;
          if (cursorOverlay != null)
          {
            cursorOverlay.ShakeToDetachEnabled = value;
          }
        }
      }
    }

    // Token: 0x170000E3 RID: 227
    // (get) Token: 0x060005EB RID: 1515 RVA: 0x0052E95C File Offset: 0x0052E95C
    public bool IsDetached
    {
      get
      {
        CursorOverlay cursorOverlay = this.inner;
        return cursorOverlay != null && cursorOverlay.IsDetached;
      }
    }

    // Token: 0x14000006 RID: 6
    // (add) Token: 0x060005EC RID: 1516 RVA: 0x0052E974 File Offset: 0x0052E974
    // (remove) Token: 0x060005ED RID: 1517 RVA: 0x0052E9AC File Offset: 0x0052E9AC
    public event Action DetachedCursorClicked;

    // Token: 0x14000007 RID: 7
    // (add) Token: 0x060005EE RID: 1518 RVA: 0x0052E9E4 File Offset: 0x0052E9E4
    // (remove) Token: 0x060005EF RID: 1519 RVA: 0x0052EA1C File Offset: 0x0052EA1C
    public event Action CursorDetached;

    // Token: 0x14000008 RID: 8
    // (add) Token: 0x060005F0 RID: 1520 RVA: 0x0052EA54 File Offset: 0x0052EA54
    // (remove) Token: 0x060005F1 RID: 1521 RVA: 0x0052EA8C File Offset: 0x0052EA8C
    public event Action DetachedCursorAutoReattached;

    // Token: 0x060005F2 RID: 1522 RVA: 0x0052EAC4 File Offset: 0x0052EAC4
    public void EndDetach()
    {
      CursorOverlay cursorOverlay = this.inner;
      if (cursorOverlay == null)
      {
        return;
      }
      cursorOverlay.EndDetach();
    }

    // Token: 0x170000E4 RID: 228
    // (get) Token: 0x060005F3 RID: 1523 RVA: 0x0052EAD8 File Offset: 0x0052EAD8
    public bool IsDocked
    {
      get
      {
        CursorOverlay cursorOverlay = this.inner;
        if (cursorOverlay == null)
        {
          return this.dockedAtStart;
        }
        return cursorOverlay.IsDocked;
      }
    }

    // Token: 0x060005F4 RID: 1524 RVA: 0x0052EAF4 File Offset: 0x0052EAF4
    public PointD? RenderedPositionPx()
    {
      CursorOverlay cursorOverlay = this.inner;
      if (cursorOverlay == null)
      {
        return null;
      }
      return cursorOverlay.RenderedPositionPx();
    }

    // Token: 0x060005F5 RID: 1525 RVA: 0x0052EB1C File Offset: 0x0052EB1C
    [NullableContext(1)]
    public Task DockAsync(PointD anchorPx)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.inner == null)
        {
          this.dockedAtStart = true;
          return Task.CompletedTask;
        }
      }
      return this.inner.DockAsync(anchorPx);
    }

    // Token: 0x170000E5 RID: 229
    // (get) Token: 0x060005F6 RID: 1526 RVA: 0x0052EB80 File Offset: 0x0052EB80
    public bool IsSayingGoodbye
    {
      get
      {
        CursorOverlay cursorOverlay = this.inner;
        return cursorOverlay != null && cursorOverlay.IsSayingGoodbye;
      }
    }

    // Token: 0x060005F7 RID: 1527 RVA: 0x0052EB98 File Offset: 0x0052EB98
    [NullableContext(1)]
    public Task SayGoodbyeAndDockAsync(string text)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.inner == null)
        {
          this.dockedAtStart = true;
          return Task.CompletedTask;
        }
      }
      return this.inner.SayGoodbyeAndDockAsync(text);
    }

    // Token: 0x060005F8 RID: 1528 RVA: 0x0052EBFC File Offset: 0x0052EBFC
    [NullableContext(1)]
    public Task UndockAsync(PointD anchorPx)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.inner == null)
        {
          this.dockedAtStart = false;
          return Task.CompletedTask;
        }
      }
      return this.inner.UndockAsync(anchorPx);
    }

    // Token: 0x060005F9 RID: 1529 RVA: 0x0052EC60 File Offset: 0x0052EC60
    public void Show()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.shown = true;
        CursorOverlay cursorOverlay = this.inner;
        if (cursorOverlay != null)
        {
          cursorOverlay.Show();
        }
      }
    }

    // Token: 0x060005FA RID: 1530 RVA: 0x0052ECB4 File Offset: 0x0052ECB4
    public void Hide()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.shown = false;
        CursorOverlay cursorOverlay = this.inner;
        if (cursorOverlay != null)
        {
          cursorOverlay.Hide();
        }
      }
    }

    // Token: 0x060005FB RID: 1531 RVA: 0x0052ED08 File Offset: 0x0052ED08
    [NullableContext(1)]
    public void SetColor(string hex)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.color = hex;
        CursorOverlay cursorOverlay = this.inner;
        if (cursorOverlay != null)
        {
          cursorOverlay.SetColor(hex);
        }
      }
    }

    // Token: 0x060005FC RID: 1532 RVA: 0x0052ED60 File Offset: 0x0052ED60
    public void SetVoiceState(VoiceState state, float level)
    {
      CursorOverlay cursorOverlay = this.inner;
      if (cursorOverlay == null)
      {
        return;
      }
      cursorOverlay.SetVoiceState(state, level);
    }

    // Token: 0x060005FD RID: 1533 RVA: 0x0052ED78 File Offset: 0x0052ED78
    [NullableContext(1)]
    public Task PointAtAsync(PointD screenPx, [Nullable(2)] string label, CancellationToken ct = default(CancellationToken))
    {
      CursorOverlay cursorOverlay = this.inner;
      return ((cursorOverlay != null) ? cursorOverlay.PointAtAsync(screenPx, label, ct) : null) ?? Task.CompletedTask;
    }

    // Token: 0x060005FE RID: 1534 RVA: 0x0052ED9C File Offset: 0x0052ED9C
    public void ReturnToCursor()
    {
      CursorOverlay cursorOverlay = this.inner;
      if (cursorOverlay == null)
      {
        return;
      }
      cursorOverlay.ReturnToCursor();
    }

    // Token: 0x060005FF RID: 1535 RVA: 0x0052EDB0 File Offset: 0x0052EDB0
    public void SetHoverDwell(double? seconds)
    {
      CursorOverlay cursorOverlay = this.inner;
      if (cursorOverlay == null)
      {
        return;
      }
      cursorOverlay.SetHoverDwell(seconds);
    }

    // Token: 0x06000600 RID: 1536 RVA: 0x0052EDC8 File Offset: 0x0052EDC8
    [NullableContext(1)]
    public void RidePen(IReadOnlyList<PenStrokePath> strokes)
    {
      CursorOverlay cursorOverlay = this.inner;
      if (cursorOverlay == null)
      {
        return;
      }
      cursorOverlay.RidePen(strokes);
    }

    // Token: 0x06000601 RID: 1537 RVA: 0x0052EDE0 File Offset: 0x0052EDE0
    public void EndPenRide()
    {
      CursorOverlay cursorOverlay = this.inner;
      if (cursorOverlay == null)
      {
        return;
      }
      cursorOverlay.EndPenRide();
    }

    // Token: 0x06000602 RID: 1538 RVA: 0x0052EDF4 File Offset: 0x0052EDF4
    [NullableContext(1)]
    public void ShowClickTarget(ClickTargetRing ring)
    {
      CursorOverlay cursorOverlay = this.inner;
      if (cursorOverlay == null)
      {
        return;
      }
      cursorOverlay.ShowClickTarget(ring);
    }

    // Token: 0x06000603 RID: 1539 RVA: 0x0052EE0C File Offset: 0x0052EE0C
    public void HideClickTarget()
    {
      CursorOverlay cursorOverlay = this.inner;
      if (cursorOverlay == null)
      {
        return;
      }
      cursorOverlay.HideClickTarget();
    }

    // Token: 0x06000604 RID: 1540 RVA: 0x0052EE20 File Offset: 0x0052EE20
    [NullableContext(1)]
    public void ShowMessage(string text)
    {
      CursorOverlay cursorOverlay = this.inner;
      if (cursorOverlay == null)
      {
        return;
      }
      cursorOverlay.ShowMessage(text);
    }

    // Token: 0x06000605 RID: 1541 RVA: 0x0052EE38 File Offset: 0x0052EE38
    public void Dispose()
    {
      CursorOverlay cursorOverlay = this.inner;
      if (cursorOverlay == null)
      {
        return;
      }
      cursorOverlay.Dispose();
    }

    // Token: 0x04000140 RID: 320
    private volatile CursorOverlay inner;

    // Token: 0x04000141 RID: 321
    private string color;

    // Token: 0x04000142 RID: 322
    private bool shown;

    // Token: 0x04000143 RID: 323
    [Nullable(1)]
    private readonly object gate = new object();

    // Token: 0x04000144 RID: 324
    private bool shakeToDetach;

    // Token: 0x04000145 RID: 325
    private bool dockedAtStart;
  }
}
