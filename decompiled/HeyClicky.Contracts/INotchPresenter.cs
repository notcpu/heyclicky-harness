using System;
using System.Runtime.CompilerServices;

namespace HeyClicky
{
  // Token: 0x0200002D RID: 45
  [NullableContext(1)]
  public interface INotchPresenter
  {
    // Token: 0x14000011 RID: 17
    // (add) Token: 0x06000237 RID: 567
    // (remove) Token: 0x06000238 RID: 568
    [Nullable(2)]
    event EventHandler PresentationChanged;

    // Token: 0x17000091 RID: 145
    // (get) Token: 0x06000239 RID: 569
    NotchPresentation Presentation { get; }

    // Token: 0x17000092 RID: 146
    // (get) Token: 0x0600023A RID: 570
    bool IsHovering { get; }

    // Token: 0x0600023B RID: 571
    void Present(NotchPresentation presentation);

    // Token: 0x0600023C RID: 572
    void ShowLiveEvent(NotchLiveEvent liveEvent, TimeSpan? autoDismissAfter = null);

    // Token: 0x0600023D RID: 573
    void DismissLiveEvent(Type liveEventType);

    // Token: 0x0600023E RID: 574
    void ReturnToIdle();
  }
}
