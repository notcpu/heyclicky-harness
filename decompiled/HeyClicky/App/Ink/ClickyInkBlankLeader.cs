using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200024C RID: 588
  public sealed class ClickyInkBlankLeader : IClickyShape
  {
    // Token: 0x06001A1A RID: 6682 RVA: 0x005DCFBC File Offset: 0x005DCFBC
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      return new PaperPath().MoveTo(rect.X + 1.0, rect.Y + rect.Height - 1.0).LineTo(rect.X + rect.Width - 1.0, rect.Y + rect.Height - 1.0);
    }
  }
}
