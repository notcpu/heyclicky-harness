using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200033B RID: 827
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class FitDecision
  {
    // Token: 0x06002337 RID: 9015 RVA: 0x0062DB60 File Offset: 0x0062DB60
    public int FirstCandidate(Size available, UIElementCollection children, int chosen)
    {
      if (chosen < 0 || chosen >= children.Count || this.count != children.Count || this.first != children[0] || this.last != children[children.Count - 1])
      {
        this.previous = -1;
        return 0;
      }
      bool flag = Environment.TickCount64 - this.changedAt < 300L;
      if (!(this.size == available) && !flag)
      {
        return 0;
      }
      return chosen;
    }

    // Token: 0x06002338 RID: 9016 RVA: 0x0062DBFC File Offset: 0x0062DBFC
    public void Remember(Size available, UIElementCollection children, int chosen)
    {
      if (this.previous >= 0 && chosen != this.previous)
      {
        this.changedAt = Environment.TickCount64;
      }
      this.previous = chosen;
      this.size = new Size?(available);
      this.count = children.Count;
      this.first = ((this.count > 0) ? children[0] : null);
      this.last = ((this.count > 0) ? children[this.count - 1] : null);
    }

    // Token: 0x04001133 RID: 4403
    private const long SettleMilliseconds = 300L;

    // Token: 0x04001134 RID: 4404
    private Size? size;

    // Token: 0x04001135 RID: 4405
    [Nullable(2)]
    private UIElement first;

    // Token: 0x04001136 RID: 4406
    [Nullable(2)]
    private UIElement last;

    // Token: 0x04001137 RID: 4407
    private int count = -1;

    // Token: 0x04001138 RID: 4408
    private int previous = -1;

    // Token: 0x04001139 RID: 4409
    private long changedAt = -4611686018427387904L;
  }
}
