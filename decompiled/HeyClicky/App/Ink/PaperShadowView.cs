using System;
using System.Runtime.CompilerServices;
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000209 RID: 521
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PaperShadowView : Grid
  {
    // Token: 0x0600190E RID: 6414 RVA: 0x005CEA10 File Offset: 0x005CEA10
    public PaperShadowView(IClickyShape shape, params PaperShadow[] shadows)
    {
      base.IsHitTestVisible = false;
      this.surface = new PaperSurface(shape, PaperFill.Of(Colors.Transparent), null);
      this.surface.Shadows.AddRange(shadows);
      base.Children.Add(this.surface);
    }

    // Token: 0x04000C37 RID: 3127
    private readonly PaperSurface surface;
  }
}
