using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.DevFakes
{
  // Token: 0x02000346 RID: 838
  public sealed class FakeHighlightOverlay : IHighlightOverlay, IDisposable
  {
    // Token: 0x17000689 RID: 1673
    // (get) Token: 0x060023B1 RID: 9137 RVA: 0x0062F200 File Offset: 0x0062F200
    // (set) Token: 0x060023B2 RID: 9138 RVA: 0x0062F208 File Offset: 0x0062F208
    public bool IsEnabled { get; set; } = true;

    // Token: 0x060023B3 RID: 9139 RVA: 0x0062F214 File Offset: 0x0062F214
    [NullableContext(1)]
    public void BeginWatch(string colorHex)
    {
    }

    // Token: 0x060023B4 RID: 9140 RVA: 0x0062F218 File Offset: 0x0062F218
    [NullableContext(1)]
    public IReadOnlyList<HighlightStroke> EndWatch()
    {
      return Array.Empty<HighlightStroke>();
    }

    // Token: 0x060023B5 RID: 9141 RVA: 0x0062F220 File Offset: 0x0062F220
    public void Dispose()
    {
    }
  }
}
