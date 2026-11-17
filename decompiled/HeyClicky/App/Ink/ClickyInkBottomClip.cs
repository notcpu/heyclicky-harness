using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200029A RID: 666
  public static class ClickyInkBottomClip
  {
    // Token: 0x06001B95 RID: 7061 RVA: 0x005F2440 File Offset: 0x005F2440
    [NullableContext(1)]
    public static void Apply(FrameworkElement element)
    {
      element.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        base.<Apply>g__Update|0();
      };
    }
  }
}
