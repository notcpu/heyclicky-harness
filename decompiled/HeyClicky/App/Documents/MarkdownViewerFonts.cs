using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Media;
using Windows.UI.Text;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002D5 RID: 725
  [NullableContext(1)]
  [Nullable(0)]
  public static class MarkdownViewerFonts
  {
    // Token: 0x06001E93 RID: 7827 RVA: 0x00607B38 File Offset: 0x00607B38
    public static FontFamily System(double size)
    {
      return DS.Fonts.For(size);
    }

    // Token: 0x1700057E RID: 1406
    // (get) Token: 0x06001E94 RID: 7828 RVA: 0x00607B40 File Offset: 0x00607B40
    public static FontFamily Mono
    {
      get
      {
        return DS.Fonts.Mono;
      }
    }

    // Token: 0x06001E95 RID: 7829 RVA: 0x00607B48 File Offset: 0x00607B48
    public static FontWeight Weight(MarkdownFontWeight weight)
    {
      FontWeight fontWeight;
      switch (weight)
      {
      case MarkdownFontWeight.Medium:
        fontWeight = FontWeights.Medium;
        break;
      case MarkdownFontWeight.Semibold:
        fontWeight = FontWeights.SemiBold;
        break;
      case MarkdownFontWeight.Bold:
        fontWeight = FontWeights.Bold;
        break;
      default:
        fontWeight = FontWeights.Normal;
        break;
      }
      return fontWeight;
    }

    // Token: 0x04000EBF RID: 3775
    public static readonly FontFamily Symbol = new FontFamily("Segoe UI Symbol");
  }
}
