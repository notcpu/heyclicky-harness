using System;
using System.Buffers;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000032 RID: 50
  [NullableContext(1)]
  [Nullable(0)]
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  internal static class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities
  {
    // Token: 0x06000061 RID: 97 RVA: 0x0033248C File Offset: 0x0033248C
    [NullableContext(0)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal unsafe static int IndexOfAnyDigit(this ReadOnlySpan<char> span)
    {
      int num = span.IndexOfAnyExcept<char>(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_asciiExceptDigits);
      if (num < span.Length)
      {
        if (char.IsAscii((char)(*span[num])))
        {
          return num;
        }
        while (!char.IsDigit((char)(*span[num])))
        {
          num++;
          if (num >= span.Length)
          {
            return -1;
          }
        }
        return num;
      }
      return -1;
    }

    // Token: 0x06000062 RID: 98 RVA: 0x003324E4 File Offset: 0x003324E4
    [NullableContext(0)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal unsafe static bool IsBoundary(ReadOnlySpan<char> inputSpan, int index)
    {
      int num = index - 1;
      return (num < inputSpan.Length && <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.<IsBoundary>g__IsBoundaryWordChar|3_0((char)(*inputSpan[num]))) != (index < inputSpan.Length && <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.<IsBoundary>g__IsBoundaryWordChar|3_0((char)(*inputSpan[index])));
    }

    // Token: 0x06000063 RID: 99 RVA: 0x00332530 File Offset: 0x00332530
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal unsafe static bool IsWordChar(char ch)
    {
      ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.3C17DA885F7916F14BECDDD5A559E146795A03758C28EA455EC8E9E803B90531), 16);
      int num = (int)(ch >> 3);
      if (num >= readOnlySpan.Length)
      {
        return (262463 & (1 << (int)CharUnicodeInfo.GetUnicodeCategory(ch))) != 0;
      }
      return ((int)(*readOnlySpan[num]) & (1 << (int)(ch & '\a'))) != 0;
    }

    // Token: 0x06000064 RID: 100 RVA: 0x00332584 File Offset: 0x00332584
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void StackPop(int[] stack, ref int pos, out int arg0, out int arg1)
    {
      int num = pos - 1;
      pos = num;
      arg0 = stack[num];
      num = pos - 1;
      pos = num;
      arg1 = stack[num];
    }

    // Token: 0x06000065 RID: 101 RVA: 0x003325AC File Offset: 0x003325AC
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void StackPush(ref int[] stack, ref int pos, int arg0)
    {
      int[] array = stack;
      int num = pos;
      if (num < array.Length)
      {
        array[num] = arg0;
        pos++;
        return;
      }
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.<StackPush>g__WithResize|6_0(ref stack, ref pos, arg0);
    }

    // Token: 0x06000066 RID: 102 RVA: 0x003325D8 File Offset: 0x003325D8
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void StackPush(ref int[] stack, ref int pos, int arg0, int arg1)
    {
      int[] array = stack;
      int num = pos;
      if (num + 1 < array.Length)
      {
        array[num] = arg0;
        array[num + 1] = arg1;
        pos += 2;
        return;
      }
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.<StackPush>g__WithResize|7_0(ref stack, ref pos, arg0, arg1);
    }

    // Token: 0x06000067 RID: 103 RVA: 0x00332610 File Offset: 0x00332610
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void StackPush(ref int[] stack, ref int pos, int arg0, int arg1, int arg2)
    {
      int[] array = stack;
      int num = pos;
      if (num + 2 < array.Length)
      {
        array[num] = arg0;
        array[num + 1] = arg1;
        array[num + 2] = arg2;
        pos += 3;
        return;
      }
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.<StackPush>g__WithResize|8_0(ref stack, ref pos, arg0, arg1, arg2);
    }

    // Token: 0x06000068 RID: 104 RVA: 0x00332650 File Offset: 0x00332650
    // Note: this type is marked as 'beforefieldinit'.
    static <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities()
    {
      object data = AppContext.GetData("REGEX_DEFAULT_MATCH_TIMEOUT");
      TimeSpan timeSpan2;
      if (data is TimeSpan)
      {
        TimeSpan timeSpan = (TimeSpan)data;
        timeSpan2 = timeSpan;
      }
      else
      {
        timeSpan2 = Regex.InfiniteMatchTimeout;
      }
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout = timeSpan2;
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout != Regex.InfiniteMatchTimeout;
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_asciiExceptDigits = SearchValues.Create("\0\u0001\u0002\u0003\u0004\u0005\u0006\a\b\t\n\v\f\r\u000e\u000f\u0010\u0011\u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001a\u001b\u001c\u001d\u001e\u001f !\"#$%&'()*+,-./:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~\u007f");
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_asciiHexDigits = SearchValues.Create("0123456789ABCDEFabcdef");
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_asciiLettersAndDigits = SearchValues.Create("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_ascii_FEFFFF87FEFFFF07 = SearchValues.Create("ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz");
      string text = "\n\n\n";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_0FEBAE6F8D3130021BEE242C5C87919F120C14E1F307F3C10E34BFDC4576BC4D = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "<a:t>";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_12E1DBB486F0197D53653D68DE3420934C9CD495A60442018C6EBA22DAEBA218 = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "/Root";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_1A32F470B3399637EEACAA4C40413209A17D35821C9C2505754B2D11F229B63A = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "<w:tab";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_2385CD56C40C5D62C478D5F27F84F1F9A7A50C0402090FFF8E7761C8AD9A74FC = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "/Type";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_4203FB690845B95F1E40E1EEB32CE001294F9D26760E6BA546169272699BC784 = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "==";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_4A9084D27E350810B767E64C519DE5B703918D35110A6B1184C035CE277E0E26 = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "**";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_4B938BB3612E6695167AB8A3A3AD73E3F6DB0CC76E502A73DB7C1EC79B5A2092 = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "/Encrypt";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_6EA455EE8B5DA4F1D7845D265904B991B56C71CC454D10190DEE48BAD102773D = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "/Length";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_921E30F6B7332314FAB4E2ECB69027ECA9BA90F395F673D24018A15794472969 = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "![";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_A35D34A63ABD1CA9D7860076AA22A8E02AF70FE7A4FEF864363EC52527771C59 = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "<text:tab";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_B6E25B48D6C5403C779E1AB3ECB2F42DD6921933466E65DB70DC7CFC9AC507AD = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "<si>";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_C19EAA2CA0036A82D9EC9ECB47A1B2E86367A0C3D616F1D0F994C214F6E265B7 = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "<t";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_C81372FD91E31CDD0ABA0C9786D61E845D30428353FADE938E54007304B3525A = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "</w:p>";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_D474EFD1CA39D108A6187B4741FD0C789626191596A3DE21E8493678667EC153 = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "<w:";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_E74F74D0ED66D8490D5B0B1D9BB2497AE7C17B70A136C471D98114C581F89718 = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "beginbfchar";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_beginbfchar_Ordinal = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "beginbfrange";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_beginbfrange_Ordinal = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      text = "begincodespacerange";
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_begincodespacerange_Ordinal = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_whitespace = SearchValues.Create("\t\n\v\f\r \u0085\u00a0\u1680\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u2028\u2029\u202f\u205f\u3000");
    }

    // Token: 0x06000069 RID: 105 RVA: 0x003328B0 File Offset: 0x003328B0
    [CompilerGenerated]
    internal static bool <IsBoundary>g__IsBoundaryWordChar|3_0(char ch)
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.IsWordChar(ch) || ((ch == '\u200c') | (ch == '\u200d'));
    }

    // Token: 0x0600006A RID: 106 RVA: 0x003328D0 File Offset: 0x003328D0
    [CompilerGenerated]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void <StackPush>g__WithResize|6_0(ref int[] stack, ref int pos, int arg0)
    {
      Array.Resize<int>(ref stack, pos * 2);
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref stack, ref pos, arg0);
    }

    // Token: 0x0600006B RID: 107 RVA: 0x003328E4 File Offset: 0x003328E4
    [CompilerGenerated]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void <StackPush>g__WithResize|7_0(ref int[] stack, ref int pos, int arg0, int arg1)
    {
      Array.Resize<int>(ref stack, (pos + 1) * 2);
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref stack, ref pos, arg0, arg1);
    }

    // Token: 0x0600006C RID: 108 RVA: 0x003328FC File Offset: 0x003328FC
    [CompilerGenerated]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void <StackPush>g__WithResize|8_0(ref int[] stack, ref int pos, int arg0, int arg1, int arg2)
    {
      Array.Resize<int>(ref stack, (pos + 2) * 2);
      <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref stack, ref pos, arg0, arg1, arg2);
    }

    // Token: 0x04000031 RID: 49
    internal static readonly TimeSpan s_defaultTimeout;

    // Token: 0x04000032 RID: 50
    internal static readonly bool s_hasTimeout;

    // Token: 0x04000033 RID: 51
    internal static readonly SearchValues<char> s_asciiExceptDigits;

    // Token: 0x04000034 RID: 52
    internal static readonly SearchValues<char> s_asciiHexDigits;

    // Token: 0x04000035 RID: 53
    internal static readonly SearchValues<char> s_asciiLettersAndDigits;

    // Token: 0x04000036 RID: 54
    internal static readonly SearchValues<char> s_ascii_FEFFFF87FEFFFF07;

    // Token: 0x04000037 RID: 55
    internal static readonly SearchValues<string> s_indexOfString_0FEBAE6F8D3130021BEE242C5C87919F120C14E1F307F3C10E34BFDC4576BC4D;

    // Token: 0x04000038 RID: 56
    internal static readonly SearchValues<string> s_indexOfString_12E1DBB486F0197D53653D68DE3420934C9CD495A60442018C6EBA22DAEBA218;

    // Token: 0x04000039 RID: 57
    internal static readonly SearchValues<string> s_indexOfString_1A32F470B3399637EEACAA4C40413209A17D35821C9C2505754B2D11F229B63A;

    // Token: 0x0400003A RID: 58
    internal static readonly SearchValues<string> s_indexOfString_2385CD56C40C5D62C478D5F27F84F1F9A7A50C0402090FFF8E7761C8AD9A74FC;

    // Token: 0x0400003B RID: 59
    internal static readonly SearchValues<string> s_indexOfString_4203FB690845B95F1E40E1EEB32CE001294F9D26760E6BA546169272699BC784;

    // Token: 0x0400003C RID: 60
    internal static readonly SearchValues<string> s_indexOfString_4A9084D27E350810B767E64C519DE5B703918D35110A6B1184C035CE277E0E26;

    // Token: 0x0400003D RID: 61
    internal static readonly SearchValues<string> s_indexOfString_4B938BB3612E6695167AB8A3A3AD73E3F6DB0CC76E502A73DB7C1EC79B5A2092;

    // Token: 0x0400003E RID: 62
    internal static readonly SearchValues<string> s_indexOfString_6EA455EE8B5DA4F1D7845D265904B991B56C71CC454D10190DEE48BAD102773D;

    // Token: 0x0400003F RID: 63
    internal static readonly SearchValues<string> s_indexOfString_921E30F6B7332314FAB4E2ECB69027ECA9BA90F395F673D24018A15794472969;

    // Token: 0x04000040 RID: 64
    internal static readonly SearchValues<string> s_indexOfString_A35D34A63ABD1CA9D7860076AA22A8E02AF70FE7A4FEF864363EC52527771C59;

    // Token: 0x04000041 RID: 65
    internal static readonly SearchValues<string> s_indexOfString_B6E25B48D6C5403C779E1AB3ECB2F42DD6921933466E65DB70DC7CFC9AC507AD;

    // Token: 0x04000042 RID: 66
    internal static readonly SearchValues<string> s_indexOfString_C19EAA2CA0036A82D9EC9ECB47A1B2E86367A0C3D616F1D0F994C214F6E265B7;

    // Token: 0x04000043 RID: 67
    internal static readonly SearchValues<string> s_indexOfString_C81372FD91E31CDD0ABA0C9786D61E845D30428353FADE938E54007304B3525A;

    // Token: 0x04000044 RID: 68
    internal static readonly SearchValues<string> s_indexOfString_D474EFD1CA39D108A6187B4741FD0C789626191596A3DE21E8493678667EC153;

    // Token: 0x04000045 RID: 69
    internal static readonly SearchValues<string> s_indexOfString_E74F74D0ED66D8490D5B0B1D9BB2497AE7C17B70A136C471D98114C581F89718;

    // Token: 0x04000046 RID: 70
    internal static readonly SearchValues<string> s_indexOfString_beginbfchar_Ordinal;

    // Token: 0x04000047 RID: 71
    internal static readonly SearchValues<string> s_indexOfString_beginbfrange_Ordinal;

    // Token: 0x04000048 RID: 72
    internal static readonly SearchValues<string> s_indexOfString_begincodespacerange_Ordinal;

    // Token: 0x04000049 RID: 73
    internal static readonly SearchValues<char> s_whitespace;
  }
}
