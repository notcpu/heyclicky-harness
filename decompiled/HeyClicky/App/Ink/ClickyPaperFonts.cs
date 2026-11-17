using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using Microsoft.UI.Text;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001E0 RID: 480
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyPaperFonts
  {
    // Token: 0x17000446 RID: 1094
    // (get) Token: 0x06001813 RID: 6163 RVA: 0x005C86BC File Offset: 0x005C86BC
    [Nullable(2)]
    private unsafe static string PatrickHandPath
    {
      [NullableContext(2)]
      get
      {
        string[] array = new string[2];
        array[0] = Path.Combine(AppContext.BaseDirectory, "Assets", "Fonts", "ClickyDraw-PatrickHand.ttf");
        int num = 1;
        <>y__InlineArray5<string> <>y__InlineArray = default(<>y__InlineArray5<string>);
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray5<string>, string>(ref <>y__InlineArray, 0) = AppContext.BaseDirectory;
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray5<string>, string>(ref <>y__InlineArray, 1) = "HeyClicky.Drawing";
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray5<string>, string>(ref <>y__InlineArray, 2) = "Assets";
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray5<string>, string>(ref <>y__InlineArray, 3) = "Fonts";
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray5<string>, string>(ref <>y__InlineArray, 4) = "ClickyDraw-PatrickHand.ttf";
        array[num] = Path.Combine(<PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray5<string>, string>(in <>y__InlineArray, 5));
        Func<string, bool> func;
        if ((func = ClickyPaperFonts.<>O.<0>__Exists) == null)
        {
          func = (ClickyPaperFonts.<>O.<0>__Exists = new Func<string, bool>(File.Exists));
        }
        return array.FirstOrDefault<string>(func);
      }
    }

    // Token: 0x06001814 RID: 6164 RVA: 0x005C876C File Offset: 0x005C876C
    private static string SystemFamilyName(double size)
    {
      if (size < 12.0)
      {
        return "Segoe UI Variable Small";
      }
      if (size < 20.0)
      {
        return "Segoe UI Variable Text";
      }
      return "Segoe UI Variable Display";
    }

    // Token: 0x06001815 RID: 6165 RVA: 0x005C8798 File Offset: 0x005C8798
    private static PaperFont SystemFont(double size, FontWeight weight)
    {
      string text = ClickyPaperFonts.SystemFamilyName(size);
      return new PaperFont(DS.Fonts.For(size), text, size, weight);
    }

    // Token: 0x06001816 RID: 6166 RVA: 0x005C87BC File Offset: 0x005C87BC
    public static PaperFont Hand(double size)
    {
      string text = "hand|" + size.ToString(CultureInfo.InvariantCulture);
      PaperFont paperFont;
      if (ClickyPaperFonts.cache.TryGetValue(text, out paperFont))
      {
        return paperFont;
      }
      FontWeight fontWeight = ((size >= 22.0) ? FontWeights.SemiBold : FontWeights.Normal);
      PaperFont paperFont2 = ClickyPaperFonts.SystemFont(Math.Round(size * 0.86, MidpointRounding.ToEven), fontWeight);
      ClickyPaperFonts.cache[text] = paperFont2;
      return paperFont2;
    }

    // Token: 0x06001817 RID: 6167 RVA: 0x005C8834 File Offset: 0x005C8834
    public static PaperFont Voice(double size, double weight = 500.0)
    {
      return ClickyPaperFonts.SystemFont(size, FontWeights.Medium);
    }

    // Token: 0x06001818 RID: 6168 RVA: 0x005C8844 File Offset: 0x005C8844
    public static PaperFont Typed(double size, FontWeight? weight = null)
    {
      return ClickyPaperFonts.SystemFont(size, weight ?? FontWeights.Normal);
    }

    // Token: 0x06001819 RID: 6169 RVA: 0x005C8870 File Offset: 0x005C8870
    public static PaperFont BigNumber(double size)
    {
      return new PaperFont(DS.Fonts.Display, "Segoe UI Variable Display", size, FontWeights.Bold);
    }

    // Token: 0x0600181A RID: 6170 RVA: 0x005C8888 File Offset: 0x005C8888
    public static PaperFont Rounded(double size, FontWeight weight)
    {
      return new PaperFont(DS.Fonts.For(size), ClickyPaperFonts.SystemFamilyName(size), size, weight);
    }

    // Token: 0x04000BD1 RID: 3025
    public const bool WritesInHandwriting = false;

    // Token: 0x04000BD2 RID: 3026
    private const double SystemFontSizeScale = 0.86;

    // Token: 0x04000BD3 RID: 3027
    private const double HeadingSizeThreshold = 22.0;

    // Token: 0x04000BD4 RID: 3028
    private static readonly Dictionary<string, PaperFont> cache = new Dictionary<string, PaperFont>();

    // Token: 0x0200078E RID: 1934
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040020EB RID: 8427
      [Nullable(0)]
      public static Func<string, bool> <0>__Exists;
    }
  }
}
