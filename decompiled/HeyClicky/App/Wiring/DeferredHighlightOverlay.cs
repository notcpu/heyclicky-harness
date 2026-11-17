using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.Platform.Overlay;

namespace HeyClicky.App.Wiring
{
  // Token: 0x02000096 RID: 150
  public sealed class DeferredHighlightOverlay : IHighlightOverlay, IDisposable
  {
    // Token: 0x170000E6 RID: 230
    // (get) Token: 0x0600060A RID: 1546 RVA: 0x0052EE9C File Offset: 0x0052EE9C
    [Nullable(2)]
    public HighlightOverlay Inner
    {
      [NullableContext(2)]
      get
      {
        return this.inner;
      }
    }

    // Token: 0x0600060B RID: 1547 RVA: 0x0052EEA8 File Offset: 0x0052EEA8
    [NullableContext(1)]
    public void Attach(HighlightOverlay real)
    {
      real.IsEnabled = this.enabled;
      this.inner = real;
    }

    // Token: 0x170000E7 RID: 231
    // (get) Token: 0x0600060C RID: 1548 RVA: 0x0052EEC0 File Offset: 0x0052EEC0
    // (set) Token: 0x0600060D RID: 1549 RVA: 0x0052EEDC File Offset: 0x0052EEDC
    public bool IsEnabled
    {
      get
      {
        HighlightOverlay highlightOverlay = this.inner;
        if (highlightOverlay == null)
        {
          return this.enabled;
        }
        return highlightOverlay.IsEnabled;
      }
      set
      {
        this.enabled = value;
        HighlightOverlay highlightOverlay = this.inner;
        if (highlightOverlay != null)
        {
          highlightOverlay.IsEnabled = value;
        }
      }
    }

    // Token: 0x0600060E RID: 1550 RVA: 0x0052EF04 File Offset: 0x0052EF04
    [NullableContext(1)]
    public void BeginWatch(string colorHex)
    {
      HighlightOverlay highlightOverlay = this.inner;
      if (highlightOverlay == null)
      {
        return;
      }
      highlightOverlay.BeginWatch(colorHex);
    }

    // Token: 0x0600060F RID: 1551 RVA: 0x0052EF1C File Offset: 0x0052EF1C
    [NullableContext(1)]
    public IReadOnlyList<HighlightStroke> EndWatch()
    {
      HighlightOverlay highlightOverlay = this.inner;
      return ((highlightOverlay != null) ? highlightOverlay.EndWatch() : null) ?? Array.Empty<HighlightStroke>();
    }

    // Token: 0x06000610 RID: 1552 RVA: 0x0052EF3C File Offset: 0x0052EF3C
    public void Clear()
    {
      HighlightOverlay highlightOverlay = this.inner;
      if (highlightOverlay == null)
      {
        return;
      }
      highlightOverlay.Clear();
    }

    // Token: 0x06000611 RID: 1553 RVA: 0x0052EF50 File Offset: 0x0052EF50
    public void Dispose()
    {
      HighlightOverlay highlightOverlay = this.inner;
      if (highlightOverlay == null)
      {
        return;
      }
      highlightOverlay.Dispose();
    }

    // Token: 0x0400014A RID: 330
    [Nullable(2)]
    private volatile HighlightOverlay inner;

    // Token: 0x0400014B RID: 331
    private bool enabled = true;
  }
}
