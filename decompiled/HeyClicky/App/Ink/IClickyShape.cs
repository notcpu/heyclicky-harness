using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000200 RID: 512
  [NullableContext(1)]
  public interface IClickyShape
  {
    // Token: 0x0600188B RID: 6283
    PaperPath PathIn(Rect rect);
  }
}
