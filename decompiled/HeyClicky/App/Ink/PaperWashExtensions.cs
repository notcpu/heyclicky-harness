using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001D5 RID: 469
  public static class PaperWashExtensions
  {
    // Token: 0x060017E2 RID: 6114 RVA: 0x005C7AB4 File Offset: 0x005C7AB4
    [NullableContext(1)]
    public static PaperSurface WithWash(this PaperSurface surface, PaperFill fill, Func<Size, PaperPath> path, bool clipped = true)
    {
      surface.Washes.Add(new PaperWash(fill, null, clipped, path));
      surface.Redraw();
      return surface;
    }
  }
}
