using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200026B RID: 619
  public sealed class ClickyInkLeader : IClickyShape
  {
    // Token: 0x06001AA5 RID: 6821 RVA: 0x005E5850 File Offset: 0x005E5850
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      return new PaperPath().MoveTo(rect.X + 1.0, rect.Y + rect.Height - 1.0).LineTo(rect.X + rect.Width - 1.0, rect.Y + rect.Height - 1.0);
    }
  }
}
