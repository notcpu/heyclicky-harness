using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200027B RID: 635
  public sealed class ClickyInkFlap : IClickyShape
  {
    // Token: 0x06001AE0 RID: 6880 RVA: 0x005E9204 File Offset: 0x005E9204
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      return new PaperPath().MoveTo(rect.X, rect.Y + rect.Height).LineTo(rect.X + rect.Width / 2.0, rect.Y).LineTo(rect.X + rect.Width, rect.Y + rect.Height)
        .Close();
    }
  }
}
