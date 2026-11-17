using System;
using System.Runtime.CompilerServices;

namespace HeyClicky
{
  // Token: 0x02000011 RID: 17
  [NullableContext(2)]
  public interface IShortcutMonitor : IDisposable
  {
    // Token: 0x14000001 RID: 1
    // (add) Token: 0x060000D1 RID: 209
    // (remove) Token: 0x060000D2 RID: 210
    event EventHandler<ShortcutAction> ShortcutTriggered;

    // Token: 0x1700003B RID: 59
    // (get) Token: 0x060000D3 RID: 211
    bool IsPushToTalkHeld { get; }

    // Token: 0x060000D4 RID: 212
    void Start();

    // Token: 0x060000D5 RID: 213
    void Stop();
  }
}
