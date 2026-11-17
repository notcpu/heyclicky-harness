using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Media;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000249 RID: 585
  [NullableContext(1)]
  [Nullable(0)]
  public static class InkObjectFonts
  {
    // Token: 0x06001A0D RID: 6669 RVA: 0x005DC668 File Offset: 0x005DC668
    public static PaperFont Mono(double size, FontWeight? weight = null)
    {
      return new PaperFont(InkObjectFonts.MonoFamily, "Cascadia Mono", size, weight ?? FontWeights.Normal);
    }

    // Token: 0x06001A0E RID: 6670 RVA: 0x005DC6A0 File Offset: 0x005DC6A0
    public static PaperFont Serif(double size, FontWeight? weight = null)
    {
      return new PaperFont(InkObjectFonts.SerifFamily, "Cambria", size, weight ?? FontWeights.Normal);
    }

    // Token: 0x06001A0F RID: 6671 RVA: 0x005DC6D8 File Offset: 0x005DC6D8
    public static PaperFont Rounded(double size, FontWeight weight)
    {
      return ClickyPaperFonts.Rounded(size, weight);
    }

    // Token: 0x04000C8B RID: 3211
    private static readonly FontFamily MonoFamily = DS.Fonts.Mono;

    // Token: 0x04000C8C RID: 3212
    private static readonly FontFamily SerifFamily = new FontFamily("Cambria, Georgia");
  }
}
