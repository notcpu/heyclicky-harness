using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;

namespace HeyClicky.App.Notch
{
  // Token: 0x02000139 RID: 313
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class NotchSurface
  {
    // Token: 0x1700031F RID: 799
    // (get) Token: 0x06001108 RID: 4360 RVA: 0x0058A980 File Offset: 0x0058A980
    // (set) Token: 0x06001109 RID: 4361 RVA: 0x0058A988 File Offset: 0x0058A988
    private protected INotchHost Host { protected get; private set; }

    // Token: 0x17000320 RID: 800
    // (get) Token: 0x0600110A RID: 4362
    public abstract FrameworkElement View { get; }

    // Token: 0x17000321 RID: 801
    // (get) Token: 0x0600110B RID: 4363 RVA: 0x0058A994 File Offset: 0x0058A994
    [Nullable(2)]
    public virtual FrameworkElement Overlay
    {
      [NullableContext(2)]
      get
      {
        return null;
      }
    }

    // Token: 0x17000322 RID: 802
    // (get) Token: 0x0600110C RID: 4364 RVA: 0x0058A998 File Offset: 0x0058A998
    public virtual VerticalAlignment ContentAlignment
    {
      get
      {
        return VerticalAlignment.Top;
      }
    }

    // Token: 0x0600110D RID: 4365 RVA: 0x0058A99C File Offset: 0x0058A99C
    public virtual bool CanUpdate(NotchPresentation p)
    {
      return false;
    }

    // Token: 0x0600110E RID: 4366 RVA: 0x0058A9A0 File Offset: 0x0058A9A0
    public virtual void Update(NotchPresentation p)
    {
    }

    // Token: 0x17000323 RID: 803
    // (get) Token: 0x0600110F RID: 4367 RVA: 0x0058A9A4 File Offset: 0x0058A9A4
    public virtual bool WantsKeyboard
    {
      get
      {
        return false;
      }
    }

    // Token: 0x17000324 RID: 804
    // (get) Token: 0x06001110 RID: 4368 RVA: 0x0058A9A8 File Offset: 0x0058A9A8
    public virtual bool HoldsKeyboard
    {
      get
      {
        return false;
      }
    }

    // Token: 0x06001111 RID: 4369 RVA: 0x0058A9AC File Offset: 0x0058A9AC
    public virtual void FocusInput()
    {
    }

    // Token: 0x06001112 RID: 4370 RVA: 0x0058A9B0 File Offset: 0x0058A9B0
    public virtual bool AcceptFiles(IReadOnlyList<string> paths)
    {
      return false;
    }

    // Token: 0x06001113 RID: 4371 RVA: 0x0058A9B4 File Offset: 0x0058A9B4
    public virtual void SetDropTargeting(bool on)
    {
    }

    // Token: 0x06001114 RID: 4372 RVA: 0x0058A9B8 File Offset: 0x0058A9B8
    public void Attach(INotchHost host, bool stillShowing = false)
    {
      this.Host = host;
      this.StillShowing = stillShowing;
      this.OnAttached();
    }

    // Token: 0x17000325 RID: 805
    // (get) Token: 0x06001115 RID: 4373 RVA: 0x0058A9D0 File Offset: 0x0058A9D0
    // (set) Token: 0x06001116 RID: 4374 RVA: 0x0058A9D8 File Offset: 0x0058A9D8
    private protected bool StillShowing { protected get; private set; }

    // Token: 0x06001117 RID: 4375 RVA: 0x0058A9E4 File Offset: 0x0058A9E4
    protected virtual void OnAttached()
    {
    }

    // Token: 0x06001118 RID: 4376 RVA: 0x0058A9E8 File Offset: 0x0058A9E8
    public virtual void Detached()
    {
    }
  }
}
