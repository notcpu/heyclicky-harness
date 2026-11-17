using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200018A RID: 394
  [NullableContext(1)]
  [Nullable(0)]
  public static class InkText
  {
    // Token: 0x06001774 RID: 6004 RVA: 0x003C3C88 File Offset: 0x003C3C88
    public static bool IsSwiftWhitespace(char c)
    {
      return c == '\t' || CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.SpaceSeparator;
    }

    // Token: 0x06001775 RID: 6005 RVA: 0x003C3C9C File Offset: 0x003C3C9C
    public static bool IsSwiftNewline(char c)
    {
      return c >= '\n' && (c <= '\r' || c == '\u0085' || c == '\u2028' || c == '\u2029');
    }

    // Token: 0x06001776 RID: 6006 RVA: 0x003C3CD4 File Offset: 0x003C3CD4
    public static string TrimWs(this string s)
    {
      Func<char, bool> func;
      if ((func = InkText.<>O.<0>__IsSwiftWhitespace) == null)
      {
        func = (InkText.<>O.<0>__IsSwiftWhitespace = new Func<char, bool>(InkText.IsSwiftWhitespace));
      }
      return InkText.TrimWhere(s, func);
    }

    // Token: 0x06001777 RID: 6007 RVA: 0x003C3CF8 File Offset: 0x003C3CF8
    public static string TrimWsNl(this string s)
    {
      return InkText.TrimWhere(s, (char c) => InkText.IsSwiftWhitespace(c) || InkText.IsSwiftNewline(c));
    }

    // Token: 0x06001778 RID: 6008 RVA: 0x003C3D20 File Offset: 0x003C3D20
    public static string TrimSet(this string s, string set)
    {
      return InkText.TrimWhere(s, (char c) => set.Contains(c));
    }

    // Token: 0x06001779 RID: 6009 RVA: 0x003C3D4C File Offset: 0x003C3D4C
    public static string TrimWhere(string s, Func<char, bool> trims)
    {
      int i = 0;
      int num = s.Length;
      while (i < num)
      {
        if (!trims(s[i]))
        {
          break;
        }
        i++;
      }
      while (num > i && trims(s[num - 1]))
      {
        num--;
      }
      if (i != 0 || num != s.Length)
      {
        int num2 = i;
        return s.Substring(num2, num - num2);
      }
      return s;
    }

    // Token: 0x0600177A RID: 6010 RVA: 0x003C3DB0 File Offset: 0x003C3DB0
    public static bool HasPrefix(this string s, string prefix)
    {
      return s.StartsWith(prefix, StringComparison.Ordinal);
    }

    // Token: 0x0600177B RID: 6011 RVA: 0x003C3DBC File Offset: 0x003C3DBC
    public static bool HasSuffix(this string s, string suffix)
    {
      return s.EndsWith(suffix, StringComparison.Ordinal);
    }

    // Token: 0x0600177C RID: 6012 RVA: 0x003C3DC8 File Offset: 0x003C3DC8
    public static string Lowered(this string s)
    {
      return s.ToLowerInvariant();
    }

    // Token: 0x0600177D RID: 6013 RVA: 0x003C3DD0 File Offset: 0x003C3DD0
    public static string[] Components(this string s, string separator)
    {
      return s.Split(separator, StringSplitOptions.None);
    }

    // Token: 0x0600177E RID: 6014 RVA: 0x003C3DDC File Offset: 0x003C3DDC
    public static int Find(this string s, string value)
    {
      return s.IndexOf(value, StringComparison.Ordinal);
    }

    // Token: 0x0600177F RID: 6015 RVA: 0x003C3DE8 File Offset: 0x003C3DE8
    public static int FindLast(this string s, string value)
    {
      return s.LastIndexOf(value, StringComparison.Ordinal);
    }

    // Token: 0x06001780 RID: 6016 RVA: 0x003C3DF4 File Offset: 0x003C3DF4
    public static string[] Characters(this string s)
    {
      if (s.Length == 0)
      {
        return Array.Empty<string>();
      }
      List<string> list = new List<string>(s.Length);
      TextElementEnumerator textElementEnumerator = StringInfo.GetTextElementEnumerator(s);
      while (textElementEnumerator.MoveNext())
      {
        object obj = textElementEnumerator.Current;
        list.Add((string)obj);
      }
      return list.ToArray();
    }

    // Token: 0x06001781 RID: 6017 RVA: 0x003C3E44 File Offset: 0x003C3E44
    public static int CharacterCount(this string s)
    {
      if (s.Length == 0)
      {
        return 0;
      }
      bool flag = true;
      foreach (char c in s)
      {
        bool flag2 = char.IsSurrogate(c) || c == '\r';
        if (!flag2)
        {
          UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
          bool flag3 = unicodeCategory - UnicodeCategory.NonSpacingMark <= 2 || unicodeCategory == UnicodeCategory.Format;
          flag2 = flag3;
        }
        if (flag2)
        {
          flag = false;
          break;
        }
      }
      if (!flag)
      {
        return new StringInfo(s).LengthInTextElements;
      }
      return s.Length;
    }

    // Token: 0x06001782 RID: 6018 RVA: 0x003C3ECC File Offset: 0x003C3ECC
    public static string PrefixCharacters(this string s, int n)
    {
      if (n <= 0)
      {
        return "";
      }
      if (s.CharacterCount() == s.Length)
      {
        if (n < s.Length)
        {
          return s.Substring(0, n);
        }
        return s;
      }
      else
      {
        string[] array = s.Characters();
        if (n < array.Length)
        {
          return string.Concat(array.Take<string>(n));
        }
        return s;
      }
    }

    // Token: 0x06001783 RID: 6019 RVA: 0x003C3F20 File Offset: 0x003C3F20
    public static string SuffixCharacters(this string s, int n)
    {
      if (n <= 0)
      {
        return "";
      }
      string[] array = s.Characters();
      if (n < array.Length)
      {
        return string.Concat(array.Skip<string>(array.Length - n));
      }
      return s;
    }

    // Token: 0x06001784 RID: 6020 RVA: 0x003C3F58 File Offset: 0x003C3F58
    public static bool IsLetter(string character)
    {
      return character.Length > 0 && Rune.IsLetter(Rune.GetRuneAt(character, 0));
    }

    // Token: 0x06001785 RID: 6021 RVA: 0x003C3F74 File Offset: 0x003C3F74
    public static bool IsNumber(string character)
    {
      return character.Length > 0 && Rune.IsNumber(Rune.GetRuneAt(character, 0));
    }

    // Token: 0x06001786 RID: 6022 RVA: 0x003C3F90 File Offset: 0x003C3F90
    public static bool IsWhitespace(string character)
    {
      return character.Length > 0 && Rune.IsWhiteSpace(Rune.GetRuneAt(character, 0));
    }

    // Token: 0x06001787 RID: 6023 RVA: 0x003C3FAC File Offset: 0x003C3FAC
    public static bool AllWhitespace(string s)
    {
      string[] array = s.Characters();
      for (int i = 0; i < array.Length; i++)
      {
        if (!InkText.IsWhitespace(array[i]))
        {
          return false;
        }
      }
      return true;
    }

    // Token: 0x06001788 RID: 6024 RVA: 0x003C3FDC File Offset: 0x003C3FDC
    public static List<string> SplitCharacters(this string s, Func<string, bool> isSeparator)
    {
      List<string> list = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string text in s.Characters())
      {
        if (isSeparator(text))
        {
          if (stringBuilder.Length > 0)
          {
            list.Add(stringBuilder.ToString());
            stringBuilder.Clear();
          }
        }
        else
        {
          stringBuilder.Append(text);
        }
      }
      if (stringBuilder.Length > 0)
      {
        list.Add(stringBuilder.ToString());
      }
      return list;
    }

    // Token: 0x06001789 RID: 6025 RVA: 0x003C4058 File Offset: 0x003C4058
    public static List<string> SplitOmittingEmpty(this string s, char separator)
    {
      return s.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
    }

    // Token: 0x0600178A RID: 6026 RVA: 0x003C4068 File Offset: 0x003C4068
    [NullableContext(2)]
    public static double? ParseDouble(string text)
    {
      if (string.IsNullOrEmpty(text))
      {
        return null;
      }
      int num = 0;
      bool flag = false;
      char c = text[0];
      bool flag2 = c == '+' || c == '-';
      if (flag2)
      {
        flag = text[0] == '-';
        num = 1;
      }
      int num2 = num;
      string text2 = text.Substring(num2, text.Length - num2);
      if (text2.Length == 0)
      {
        return null;
      }
      string text3 = text2.ToLowerInvariant();
      flag2 = text3 == "inf" || text3 == "infinity";
      if (flag2)
      {
        return new double?(flag ? double.NegativeInfinity : double.PositiveInfinity);
      }
      if (text3 == "nan")
      {
        return new double?(double.NaN);
      }
      if (text3.HasPrefix("0x"))
      {
        string text4 = text3;
        double? num3 = InkText.ParseHexDouble(text4.Substring(2, text4.Length - 2));
        if (num3 != null)
        {
          double valueOrDefault = num3.GetValueOrDefault();
          return new double?(flag ? (-valueOrDefault) : valueOrDefault);
        }
        return null;
      }
      else
      {
        int i = 0;
        int num4 = 0;
        while (i < text2.Length)
        {
          c = text2[i];
          if (c < '0' || c > '9')
          {
            break;
          }
          i++;
          num4++;
        }
        if (i < text2.Length && text2[i] == '.')
        {
          i++;
          while (i < text2.Length)
          {
            c = text2[i];
            if (c < '0' || c > '9')
            {
              break;
            }
            i++;
            num4++;
          }
        }
        if (num4 == 0)
        {
          return null;
        }
        flag2 = i < text2.Length;
        if (flag2)
        {
          c = text2[i];
          bool flag3 = c == 'E' || c == 'e';
          flag2 = flag3;
        }
        if (flag2)
        {
          i++;
          flag2 = i < text2.Length;
          if (flag2)
          {
            c = text2[i];
            bool flag3 = c == '+' || c == '-';
            flag2 = flag3;
          }
          if (flag2)
          {
            i++;
          }
          int num5 = 0;
          while (i < text2.Length)
          {
            c = text2[i];
            if (c < '0' || c > '9')
            {
              break;
            }
            i++;
            num5++;
          }
          if (num5 == 0)
          {
            return null;
          }
        }
        if (i != text2.Length)
        {
          return null;
        }
        double num6;
        if (!double.TryParse(text2, NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out num6))
        {
          return null;
        }
        return new double?(flag ? (-num6) : num6);
      }
    }

    // Token: 0x0600178B RID: 6027 RVA: 0x003C4338 File Offset: 0x003C4338
    private static double? ParseHexDouble(string s)
    {
      if (s.Length == 0)
      {
        return null;
      }
      double num = 0.0;
      int i = 0;
      int num2 = 0;
      int num3 = 0;
      bool flag = false;
      while (i < s.Length)
      {
        char c = s[i];
        if (c == '.' && !flag)
        {
          flag = true;
        }
        else
        {
          int num4 = ((c >= '0' && c <= '9') ? ((int)(c - '0')) : ((c >= 'a' && c <= 'f') ? ((int)(c - 'a' + '\n')) : (-1)));
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
      if (i < s.Length)
      {
        if (s[i] != 'p')
        {
          return null;
        }
        i++;
        int num6 = 1;
        bool flag2 = i < s.Length;
        if (flag2)
        {
          char c2 = s[i];
          bool flag3 = c2 == '+' || c2 == '-';
          flag2 = flag3;
        }
        if (flag2)
        {
          num6 = ((s[i] == '-') ? (-1) : 1);
          i++;
        }
        int num7 = 0;
        while (i < s.Length)
        {
          char c2 = s[i];
          if (c2 < '0' || c2 > '9')
          {
            break;
          }
          num5 = num5 * 10 + (int)(s[i] - '0');
          num7++;
          i++;
        }
        if (num7 == 0 || i != s.Length)
        {
          return null;
        }
        num5 *= num6;
      }
      return new double?(num * Math.Pow(2.0, (double)(num5 - 4 * num3)));
    }

    // Token: 0x0600178C RID: 6028 RVA: 0x003C44DC File Offset: 0x003C44DC
    [NullableContext(2)]
    public static long? ParseInt(string text)
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
        c = text[i];
        flag = c < '0' || c > '9';
        if (flag)
        {
          return null;
        }
      }
      long num2;
      if (!long.TryParse(text, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out num2))
      {
        return null;
      }
      return new long?(num2);
    }

    // Token: 0x0600178D RID: 6029 RVA: 0x003C458C File Offset: 0x003C458C
    public static string DoubleDescription(double value)
    {
      if (double.IsNaN(value))
      {
        return "nan";
      }
      if (double.IsPositiveInfinity(value))
      {
        return "inf";
      }
      if (double.IsNegativeInfinity(value))
      {
        return "-inf";
      }
      string text = value.ToString("R", CultureInfo.InvariantCulture);
      if (text.Contains('E'))
      {
        return text.Replace('E', 'e');
      }
      if (!text.Contains('.'))
      {
        return text + ".0";
      }
      return text;
    }

    // Token: 0x0600178E RID: 6030 RVA: 0x003C4604 File Offset: 0x003C4604
    public static long InkClamping(double value)
    {
      if (double.IsFinite(value))
      {
        return (long)Math.Min(Math.Max(value, -1000000000000000.0), 1000000000000000.0);
      }
      if (double.IsNaN(value))
      {
        return 0L;
      }
      if (value <= 0.0)
      {
        return -1000000000000000L;
      }
      return 1000000000000000L;
    }

    // Token: 0x0200056A RID: 1386
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04002073 RID: 8307
      [Nullable(0)]
      public static Func<char, bool> <0>__IsSwiftWhitespace;
    }
  }
}
