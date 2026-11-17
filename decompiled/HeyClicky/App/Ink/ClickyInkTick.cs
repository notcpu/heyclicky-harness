using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000293 RID: 659
  public sealed class ClickyInkTick : IClickyShape
  {
    // Token: 0x06001B5A RID: 7002 RVA: 0x005EDEC0 File Offset: 0x005EDEC0
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      return new PaperPath().MoveTo(rect.X, rect.Y + rect.Height / 2.0 + rect.Height * 0.05).QuadTo(new Point(rect.X + rect.Width * 0.36, rect.Y + rect.Height), new Point(rect.X + rect.Width * 0.2, rect.Y + rect.Height - rect.Height * 0.1)).QuadTo(new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width * 0.55, rect.Y + rect.Height / 2.0 - rect.Height * 0.1));
    }
  }
}
