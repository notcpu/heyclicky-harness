using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Dictation
{
  // Token: 0x020001C3 RID: 451
  [NullableContext(1)]
  public interface IDictationScheduler
  {
    // Token: 0x06001A38 RID: 6712
    IDisposable Schedule(TimeSpan delay, Action action);

    // Token: 0x170005CC RID: 1484
    // (get) Token: 0x06001A39 RID: 6713
    DateTimeOffset Now { get; }
  }
}
