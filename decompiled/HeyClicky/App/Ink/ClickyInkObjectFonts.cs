using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Media;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000283 RID: 643
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkObjectFonts
  {
    // Token: 0x06001B12 RID: 6930 RVA: 0x005EA964 File Offset: 0x005EA964
    public static PaperFont System(double size, FontWeight? weight = null)
    {
      return ClickyPaperFonts.Typed(size, weight);
    }

    // Token: 0x06001B13 RID: 6931 RVA: 0x005EA970 File Offset: 0x005EA970
    public static PaperFont Mono(double size, FontWeight? weight = null)
    {
      return new PaperFont(DS.Fonts.Mono, "Cascadia Mono", size, weight ?? FontWeights.Normal);
    }

    // Token: 0x06001B14 RID: 6932 RVA: 0x005EA9A8 File Offset: 0x005EA9A8
    public static PaperFont SerifFont(double size, FontWeight? weight = null)
    {
      return new PaperFont(ClickyInkObjectFonts.Serif, "Georgia", size, weight ?? FontWeights.Normal);
    }

    // Token: 0x06001B15 RID: 6933 RVA: 0x005EA9E0 File Offset: 0x005EA9E0
    public static PaperFont Rounded(double size, FontWeight weight)
    {
      return ClickyPaperFonts.Rounded(size, weight);
    }

    // Token: 0x04000CFA RID: 3322
    private static readonly FontFamily Serif = new FontFamily("Georgia");
  }
}
