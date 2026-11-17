using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace HeyClicky.App.DevFakes
{
  // Token: 0x0200033D RID: 829
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class FakeNotchPresenter : INotchPresenter
  {
    // Token: 0x1400007E RID: 126
    // (add) Token: 0x06002348 RID: 9032 RVA: 0x0062E030 File Offset: 0x0062E030
    // (remove) Token: 0x06002349 RID: 9033 RVA: 0x0062E068 File Offset: 0x0062E068
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler PresentationChanged;

    // Token: 0x17000678 RID: 1656
    // (get) Token: 0x0600234A RID: 9034 RVA: 0x0062E0A0 File Offset: 0x0062E0A0
    public NotchPresentation Presentation
    {
      get
      {
        object obj = this.gate;
        NotchPresentation notchPresentation;
        lock (obj)
        {
          notchPresentation = this.presentation;
        }
        return notchPresentation;
      }
    }

    // Token: 0x17000679 RID: 1657
    // (get) Token: 0x0600234B RID: 9035 RVA: 0x0062E0E4 File Offset: 0x0062E0E4
    // (set) Token: 0x0600234C RID: 9036 RVA: 0x0062E0EC File Offset: 0x0062E0EC
    public bool IsHovering { get; set; }

    // Token: 0x0600234D RID: 9037 RVA: 0x0062E0F8 File Offset: 0x0062E0F8
    public void Present(NotchPresentation p)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.presentation = p;
        this.generation++;
        Timer timer = this.autoDismiss;
        if (timer != null)
        {
          timer.Dispose();
        }
        this.autoDismiss = null;
      }
      EventHandler presentationChanged = this.PresentationChanged;
      if (presentationChanged == null)
      {
        return;
      }
      presentationChanged(this, EventArgs.Empty);
    }

    // Token: 0x0600234E RID: 9038 RVA: 0x0062E178 File Offset: 0x0062E178
    public void ShowLiveEvent(NotchLiveEvent liveEvent, TimeSpan? autoDismissAfter = null)
    {
      this.Present(new NotchPresentation.LiveEvent(liveEvent));
      if (autoDismissAfter != null)
      {
        TimeSpan valueOrDefault = autoDismissAfter.GetValueOrDefault();
        object obj = this.gate;
        int gen;
        lock (obj)
        {
          gen = this.generation;
        }
        this.autoDismiss = new Timer(delegate(object _)
        {
          object obj2 = this.gate;
          lock (obj2)
          {
            if (gen != this.generation)
            {
              return;
            }
          }
          this.ReturnToIdle();
        }, null, valueOrDefault, Timeout.InfiniteTimeSpan);
      }
    }

    // Token: 0x0600234F RID: 9039 RVA: 0x0062E208 File Offset: 0x0062E208
    public void DismissLiveEvent(Type liveEventType)
    {
      NotchPresentation.LiveEvent liveEvent = this.Presentation as NotchPresentation.LiveEvent;
      if (liveEvent != null && liveEventType.IsInstanceOfType(liveEvent.Event))
      {
        this.ReturnToIdle();
      }
    }

    // Token: 0x06002350 RID: 9040 RVA: 0x0062E238 File Offset: 0x0062E238
    public void ReturnToIdle()
    {
      this.Present(new NotchPresentation.Idle());
    }

    // Token: 0x0400114F RID: 4431
    private readonly object gate = new object();

    // Token: 0x04001150 RID: 4432
    private NotchPresentation presentation = new NotchPresentation.Idle();

    // Token: 0x04001151 RID: 4433
    [Nullable(2)]
    private Timer autoDismiss;

    // Token: 0x04001152 RID: 4434
    private int generation;
  }
}
