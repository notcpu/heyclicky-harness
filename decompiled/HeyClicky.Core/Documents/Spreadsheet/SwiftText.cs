using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001B7 RID: 439
  [NullableContext(1)]
  [Nullable(0)]
  public static class SwiftText
  {
    // Token: 0x0600196C RID: 6508 RVA: 0x003D17B4 File Offset: 0x003D17B4
    [NullableContext(2)]
    public unsafe static double? ParseDouble(string text)
    {
      if (string.IsNullOrEmpty(text))
      {
        return null;
      }
      ReadOnlySpan<char> readOnlySpan = text.AsSpan();
      int num = 0;
      bool flag = false;
      char c = (char)(*readOnlySpan[0]);
      bool flag2 = c == '+' || c == '-';
      if (flag2)
      {
        flag = *readOnlySpan[0] == 45;
        num = 1;
      }
      ref ReadOnlySpan<char> ptr = ref readOnlySpan;
      int i = num;
      ReadOnlySpan<char> readOnlySpan2 = ptr.Slice(i, ptr.Length - i);
      if (readOnlySpan2.Length == 0)
      {
        return null;
      }
      if (readOnlySpan2.Equals("inf", StringComparison.OrdinalIgnoreCase) || readOnlySpan2.Equals("infinity", StringComparison.OrdinalIgnoreCase))
      {
        return new double?(flag ? double.NegativeInfinity : double.PositiveInfinity);
      }
      if (readOnlySpan2.StartsWith("nan", StringComparison.OrdinalIgnoreCase))
      {
        if (readOnlySpan2.Length == 3)
        {
          return new double?(double.NaN);
        }
        if (*readOnlySpan2[3] == 40 && *readOnlySpan2[readOnlySpan2.Length - 1] == 41)
        {
          ptr = ref readOnlySpan2;
          ReadOnlySpan<char> readOnlySpan3 = ptr.Slice(4, ptr.Length - 1 - 4);
          for (i = 0; i < readOnlySpan3.Length; i++)
          {
            char c2 = (char)(*readOnlySpan3[i]);
            if (!char.IsAsciiLetterOrDigit(c2) && c2 != '_')
            {
              return null;
            }
          }
          return new double?(double.NaN);
        }
        return null;
      }
      else
      {
        flag2 = readOnlySpan2.Length > 2 && *readOnlySpan2[0] == 48;
        if (flag2)
        {
          c = (char)(*readOnlySpan2[1]);
          bool flag3 = c == 'X' || c == 'x';
          flag2 = flag3;
        }
        if (flag2)
        {
          ptr = ref readOnlySpan2;
          return SwiftText.ParseHex(ptr.Slice(2, ptr.Length - 2), flag);
        }
        int num2 = 0;
        int num3 = 0;
        while (num2 < readOnlySpan2.Length && char.IsAsciiDigit((char)(*readOnlySpan2[num2])))
        {
          num2++;
          num3++;
        }
        if (num2 < readOnlySpan2.Length && *readOnlySpan2[num2] == 46)
        {
          num2++;
          while (num2 < readOnlySpan2.Length && char.IsAsciiDigit((char)(*readOnlySpan2[num2])))
          {
            num2++;
            num3++;
          }
        }
        if (num3 == 0)
        {
          return null;
        }
        flag2 = num2 < readOnlySpan2.Length;
        if (flag2)
        {
          c = (char)(*readOnlySpan2[num2]);
          bool flag3 = c == 'E' || c == 'e';
          flag2 = flag3;
        }
        if (flag2)
        {
          num2++;
          flag2 = num2 < readOnlySpan2.Length;
          if (flag2)
          {
            c = (char)(*readOnlySpan2[num2]);
            bool flag3 = c == '+' || c == '-';
            flag2 = flag3;
          }
          if (flag2)
          {
            num2++;
          }
          int num4 = 0;
          while (num2 < readOnlySpan2.Length && char.IsAsciiDigit((char)(*readOnlySpan2[num2])))
          {
            num2++;
            num4++;
          }
          if (num4 == 0)
          {
            return null;
          }
        }
        if (num2 != readOnlySpan2.Length)
        {
          return null;
        }
        double num5;
        if (!double.TryParse(readOnlySpan2, NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out num5))
        {
          return null;
        }
        return new double?(flag ? (-num5) : num5);
      }
    }

    // Token: 0x0600196D RID: 6509 RVA: 0x003D1B30 File Offset: 0x003D1B30
    [NullableContext(0)]
    private unsafe static double? ParseHex(ReadOnlySpan<char> s, bool negative)
    {
      double num = 0.0;
      int num2 = 0;
      int num3 = 0;
      int i = 0;
      bool flag = false;
      while (i < s.Length)
      {
        char c = (char)(*s[i]);
        if (c == '.' && !flag)
        {
          flag = true;
        }
        else
        {
          int num4 = SwiftText.HexValue(c);
          if (num4 < 0)
          {
            break;
          }
          num = num * 16.0 + (double)num4;
          num2++;
          if (flag)
          {
            num3++;
          }
        }
        i++;
      }
      if (num2 == 0)
      {
        return null;
      }
      int num5 = 0;
      bool flag2 = i < s.Length;
      if (flag2)
      {
        char c2 = (char)(*s[i]);
        bool flag3 = c2 == 'P' || c2 == 'p';
        flag2 = flag3;
      }
      if (flag2)
      {
        i++;
        bool flag4 = false;
        flag2 = i < s.Length;
        if (flag2)
        {
          char c2 = (char)(*s[i]);
          bool flag3 = c2 == '+' || c2 == '-';
          flag2 = flag3;
        }
        if (flag2)
        {
          flag4 = *s[i] == 45;
          i++;
        }
        int num6 = 0;
        while (i < s.Length && char.IsAsciiDigit((char)(*s[i])))
        {
          num5 = Math.Min(100000, num5 * 10 + (int)(*s[i] - 48));
          i++;
          num6++;
        }
        if (num6 == 0)
        {
          return null;
        }
        if (flag4)
        {
          num5 = -num5;
        }
      }
      if (i != s.Length)
      {
        return null;
      }
      double num7 = num * Math.Pow(2.0, (double)(num5 - 4 * num3));
      return new double?(negative ? (-num7) : num7);
    }

    // Token: 0x0600196E RID: 6510 RVA: 0x003D1CDC File Offset: 0x003D1CDC
    private static int HexValue(char ch)
    {
      if (ch >= 'A')
      {
        if (ch >= 'a')
        {
          if (ch <= 'f')
          {
            return (int)(ch - 'a' + '\n');
          }
        }
        else if (ch <= 'F')
        {
          return (int)(ch - 'A' + '\n');
        }
      }
      else if (ch >= '0' && ch <= '9')
      {
        return (int)(ch - '0');
      }
      return -1;
    }

    // Token: 0x0600196F RID: 6511 RVA: 0x003D1D2C File Offset: 0x003D1D2C
    public static int CharacterCount(string text)
    {
      if (text.Length == 0)
      {
        return 0;
      }
      bool flag = true;
      foreach (char c in text)
      {
        if (c > '\u02ff' || char.IsSurrogate(c))
        {
          flag = false;
          break;
        }
      }
      if (flag)
      {
        return text.Replace("\r\n", "\n").Length;
      }
      return new StringInfo(text).LengthInTextElements;
    }

    // Token: 0x06001970 RID: 6512 RVA: 0x003D1D98 File Offset: 0x003D1D98
    public static bool IsWhitespace(char ch)
    {
      return ch == '\t' || CharUnicodeInfo.GetUnicodeCategory(ch) == UnicodeCategory.SpaceSeparator;
    }

    // Token: 0x06001971 RID: 6513 RVA: 0x003D1DAC File Offset: 0x003D1DAC
    public static string TrimWhitespace(string text)
    {
      int i = 0;
      int num = text.Length;
      while (i < num)
      {
        if (!SwiftText.IsWhitespace(text[i]))
        {
          break;
        }
        i++;
      }
      while (num > i && SwiftText.IsWhitespace(text[num - 1]))
      {
        num--;
      }
      if (i != 0 || num != text.Length)
      {
        int num2 = i;
        return text.Substring(num2, num - num2);
      }
      return text;
    }

    // Token: 0x06001972 RID: 6514 RVA: 0x003D1E10 File Offset: 0x003D1E10
    public static string TrimWhitespaceAndNewlines(string text)
    {
      int i = 0;
      int num = text.Length;
      while (i < num)
      {
        if (!SwiftText.<TrimWhitespaceAndNewlines>g__Is|6_0(text[i]))
        {
          break;
        }
        i++;
      }
      while (num > i && SwiftText.<TrimWhitespaceAndNewlines>g__Is|6_0(text[num - 1]))
      {
        num--;
      }
      int num2 = i;
      return text.Substring(num2, num - num2);
    }

    // Token: 0x06001973 RID: 6515 RVA: 0x003D1E64 File Offset: 0x003D1E64
    [NullableContext(2)]
    public static int? ParseInt(string text)
    {
      if (string.IsNullOrEmpty(text))
      {
        return null;
      }
      char c = text[0];
      bool flag = c == '+' || c == '-';
      int num = ((flag > false) ? 1 : 0);
      if (num == text.Length)
      {
        return null;
      }
      for (int i = num; i < text.Length; i++)
      {
        if (!char.IsAsciiDigit(text[i]))
        {
          return null;
        }
      }
      int num2;
      if (!int.TryParse(text, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out num2))
      {
        return null;
      }
      return new int?(num2);
    }

    // Token: 0x06001974 RID: 6516 RVA: 0x003D1F04 File Offset: 0x003D1F04
    [return: Nullable(2)]
    public static string DecodeText(byte[] data)
    {
      try
      {
        return new UTF8Encoding(false, true).GetString(data);
      }
      catch (DecoderFallbackException)
      {
      }
      if (data.Length % 2 == 0 && data.Length != 0)
      {
        bool flag = data.Length >= 2 && data[0] == byte.MaxValue && data[1] == 254;
        try
        {
          return new UnicodeEncoding(!flag, false, true).GetString(data);
        }
        catch (DecoderFallbackException)
        {
        }
      }
      string text;
      try
      {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        text = Encoding.GetEncoding(1252, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback).GetString(data);
      }
      catch (Exception)
      {
        text = null;
      }
      return text;
    }

    // Token: 0x06001975 RID: 6517 RVA: 0x003D1FBC File Offset: 0x003D1FBC
    [CompilerGenerated]
    internal static bool <TrimWhitespaceAndNewlines>g__Is|6_0(char ch)
    {
      bool flag = SwiftText.IsWhitespace(ch);
      if (!flag)
      {
        bool flag2;
        switch (ch)
        {
        case '\n':
        case '\v':
        case '\f':
        case '\r':
          break;
        default:
          if (ch != '\u0085')
          {
            flag2 = false;
            goto IL_0031;
          }
          break;
        }
        flag2 = true;
        IL_0031:
        flag = flag2;
      }
      return flag || ch == '\u2028' || ch == '\u2029';
    }
  }
}
