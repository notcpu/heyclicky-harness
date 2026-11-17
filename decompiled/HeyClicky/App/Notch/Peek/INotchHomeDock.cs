using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000184 RID: 388
  [NullableContext(2)]
  public interface INotchHomeDock
  {
    // Token: 0x170003C3 RID: 963
    // (get) Token: 0x06001510 RID: 5392
    [TupleElementNames(new string[] { "W", "H" })]
    [Nullable(0)]
    ValueTuple<double, double>? FrontCardSize
    {
      [NullableContext(0)]
      [return: TupleElementNames(new string[] { "W", "H" })]
      get;
    }

    // Token: 0x1400005C RID: 92
    // (add) Token: 0x06001511 RID: 5393
    // (remove) Token: 0x06001512 RID: 5394
    event Action FrontCardSizeChanged;

    // Token: 0x06001513 RID: 5395
    FrameworkElement CreateHomeSlot();

    // Token: 0x170003C4 RID: 964
    // (get) Token: 0x06001514 RID: 5396
    string StatusLine { get; }

    // Token: 0x06001515 RID: 5397
    void NotchShowsHome(bool showsHome);

    // Token: 0x06001516 RID: 5398
    [NullableContext(1)]
    void NotchFolded(string reason);
  }
}
