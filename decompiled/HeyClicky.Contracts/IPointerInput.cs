using System;
using System.Runtime.CompilerServices;

namespace HeyClicky
{
  // Token: 0x02000025 RID: 37
  [NullableContext(1)]
  public interface IPointerInput
  {
    // Token: 0x060001DE RID: 478
    IDisposable WatchPresses(Action<PointD> onPress);

    // Token: 0x1700007D RID: 125
    // (get) Token: 0x060001DF RID: 479
    PointD CursorPosition { get; }

    // Token: 0x1700007E RID: 126
    // (get) Token: 0x060001E0 RID: 480
    bool IsAnyButtonDown { get; }
  }
}
