using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky
{
  // Token: 0x02000028 RID: 40
  [NullableContext(1)]
  public interface IHighlightOverlay : IDisposable
  {
    // Token: 0x1700008B RID: 139
    // (get) Token: 0x0600020D RID: 525
    // (set) Token: 0x0600020E RID: 526
    bool IsEnabled { get; set; }

    // Token: 0x0600020F RID: 527
    void BeginWatch(string colorHex);

    // Token: 0x06000210 RID: 528
    IReadOnlyList<HighlightStroke> EndWatch();
  }
}
