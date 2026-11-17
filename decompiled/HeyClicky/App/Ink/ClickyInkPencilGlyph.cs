using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200029F RID: 671
  public static class ClickyInkPencilGlyph
  {
    // Token: 0x06001BCB RID: 7115 RVA: 0x005F4F00 File Offset: 0x005F4F00
    [NullableContext(1)]
    public static FrameworkElement Make(double size, Color tint, bool bold = false)
    {
      TextBlock textBlock = ClickyInkIcon.Glyph("pencil", size * 0.92, tint, bold) ?? new TextBlock();
      return new Grid
      {
        Width = size,
        Height = size,
        IsHitTestVisible = false,
        Children = { textBlock }
      };
    }
  }
}
