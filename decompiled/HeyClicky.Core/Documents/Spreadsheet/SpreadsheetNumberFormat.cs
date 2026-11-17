using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x0200019F RID: 415
  [NullableContext(1)]
  [Nullable(0)]
  public static class SpreadsheetNumberFormat
  {
    // Token: 0x060018AD RID: 6317 RVA: 0x003CBC60 File Offset: 0x003CBC60
    [NullableContext(2)]
    public static string BuiltInFormatCode(int id)
    {
      switch (id)
      {
      case 0:
        return "General";
      case 1:
        return "0";
      case 2:
        return "0.00";
      case 3:
        return "#,##0";
      case 4:
        return "#,##0.00";
      case 5:
        return "$#,##0;($#,##0)";
      case 6:
        return "$#,##0;[Red]($#,##0)";
      case 7:
        return "$#,##0.00;($#,##0.00)";
      case 8:
        return "$#,##0.00;[Red]($#,##0.00)";
      case 9:
        return "0%";
      case 10:
        return "0.00%";
      case 11:
        return "0.00E+00";
      case 12:
        return "# ?/?";
      case 13:
        return "# ??/??";
      case 14:
        return "m/d/yyyy";
      case 15:
        return "d-mmm-yy";
      case 16:
        return "d-mmm";
      case 17:
        return "mmm-yy";
      case 18:
        return "h:mm AM/PM";
      case 19:
        return "h:mm:ss AM/PM";
      case 20:
        return "h:mm";
      case 21:
        return "h:mm:ss";
      case 22:
        return "m/d/yyyy h:mm";
      case 37:
        return "#,##0 ;(#,##0)";
      case 38:
        return "#,##0 ;[Red](#,##0)";
      case 39:
        return "#,##0.00;(#,##0.00)";
      case 40:
        return "#,##0.00;[Red](#,##0.00)";
      case 44:
        return "$#,##0.00";
      case 45:
        return "mm:ss";
      case 46:
        return "[h]:mm:ss";
      case 47:
        return "mm:ss.0";
      case 48:
        return "##0.0E+0";
      case 49:
        return "@";
      }
      return null;
    }

    // Token: 0x060018AE RID: 6318 RVA: 0x003CBE80 File Offset: 0x003CBE80
    public static string Format(double value, string rawCode)
    {
      if (!double.IsFinite(value))
      {
        return "#NUM!";
      }
      List<string> list = SpreadsheetNumberFormat.SplitSections(rawCode);
      string text = ((list.Count > 0) ? list[0] : "General");
      double num = value;
      if (value < 0.0 && list.Count > 1)
      {
        text = list[1];
        num = -value;
      }
      else if (value == 0.0 && list.Count > 2)
      {
        text = list[2];
      }
      string text2 = SpreadsheetNumberFormat.CleanedSection(text);
      if (text2.Length == 0 || text2.Equals("General", StringComparison.OrdinalIgnoreCase) || text2 == "@")
      {
        return SpreadsheetNumberFormat.General(value);
      }
      if (SpreadsheetNumberFormat.IsDateFormat(text2))
      {
        return SpreadsheetNumberFormat.FormatDate(num, text2);
      }
      string text3 = SpreadsheetNumberFormat.FormatNumber(num, text2);
      if (value < 0.0 && list.Count == 1 && !text3.StartsWith('-'))
      {
        return "-" + text3;
      }
      return text3;
    }

    // Token: 0x060018AF RID: 6319 RVA: 0x003CBF74 File Offset: 0x003CBF74
    public static string General(double value)
    {
      if (value == Math.Round(value) && Math.Abs(value) < 1000000000000000.0)
      {
        return ((long)value).ToString(CultureInfo.InvariantCulture);
      }
      double num = Math.Abs(value);
      if (num >= 100000000000.0 || num < 1E-09)
      {
        return SpreadsheetNumberFormat.TrailingZerosBeforeExponent().Replace(SpreadsheetNumberFormat.PrintfE(value, 5), "E");
      }
      return value.ToString("G10", CultureInfo.InvariantCulture).Replace("E", "e");
    }

    // Token: 0x060018B0 RID: 6320 RVA: 0x003CC004 File Offset: 0x003CC004
    [GeneratedRegex("\\.?0+E")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex TrailingZerosBeforeExponent()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__TrailingZerosBeforeExponent_35.Instance;
    }

    // Token: 0x060018B1 RID: 6321 RVA: 0x003CC00C File Offset: 0x003CC00C
    private static string PrintfE(double value, int decimals)
    {
      string text = ((decimals > 0) ? ("0." + new string('0', decimals)) : "0") + "E+00";
      return value.ToString(text, CultureInfo.InvariantCulture);
    }

    // Token: 0x060018B2 RID: 6322 RVA: 0x003CC050 File Offset: 0x003CC050
    private static List<string> SplitSections(string code)
    {
      List<string> list = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = false;
      bool flag2 = false;
      foreach (char c in code)
      {
        if (flag2)
        {
          stringBuilder.Append(c);
          flag2 = false;
        }
        else if (c == '\\')
        {
          stringBuilder.Append(c);
          flag2 = true;
        }
        else
        {
          if (c == '"')
          {
            flag = !flag;
          }
          if (c == ';' && !flag)
          {
            list.Add(stringBuilder.ToString());
            stringBuilder.Clear();
          }
          else
          {
            stringBuilder.Append(c);
          }
        }
      }
      list.Add(stringBuilder.ToString());
      return list;
    }

    // Token: 0x060018B3 RID: 6323 RVA: 0x003CC0F4 File Offset: 0x003CC0F4
    private static string CleanedSection(string section)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int i = 0;
      while (i < section.Length)
      {
        char c = section[i];
        if (c != '"')
        {
          if (c != '*')
          {
            switch (c)
            {
            case '[':
            {
              int num = i + 1;
              while (num < section.Length && section[num] != ']')
              {
                num++;
              }
              int num2 = i + 1;
              string text = section.Substring(num2, Math.Min(num, section.Length) - num2);
              if (text.StartsWith('$'))
              {
                string text2 = text;
                string text3 = SpreadsheetNumberFormat.SwiftSplitFirst(text2.Substring(1, text2.Length - 1), '-') ?? "";
                stringBuilder.Append(SpreadsheetNumberFormat.Literal(text3));
              }
              else
              {
                string lowered = text.ToLowerInvariant();
                if (lowered.Length > 0 && "hms".Contains(lowered[0]) && lowered.All<char>((char ch) => ch == lowered[0]))
                {
                  stringBuilder.Append('\u0002').Append(lowered);
                }
              }
              i = num + 1;
              continue;
            }
            case '\\':
              if (i + 1 < section.Length)
              {
                stringBuilder.Append(SpreadsheetNumberFormat.Literal(section[i + 1].ToString()));
              }
              i += 2;
              continue;
            case '_':
              goto IL_01C6;
            }
            stringBuilder.Append(c);
            i++;
            continue;
          }
          IL_01C6:
          i += 2;
        }
        else
        {
          int num3 = i + 1;
          StringBuilder stringBuilder2 = new StringBuilder();
          while (num3 < section.Length && section[num3] != '"')
          {
            stringBuilder2.Append(section[num3]);
            num3++;
          }
          stringBuilder.Append(SpreadsheetNumberFormat.Literal(stringBuilder2.ToString()));
          i = num3 + 1;
        }
      }
      return SwiftText.TrimWhitespace(stringBuilder.ToString());
    }

    // Token: 0x060018B4 RID: 6324 RVA: 0x003CC2F0 File Offset: 0x003CC2F0
    [return: Nullable(2)]
    private static string SwiftSplitFirst(string text, char separator)
    {
      int num = 0;
      while (num < text.Length && text[num] == separator)
      {
        num++;
      }
      if (num == text.Length)
      {
        return null;
      }
      int num2 = text.IndexOf(separator, num);
      int num3;
      if (num2 >= 0)
      {
        num3 = num;
        return text.Substring(num3, num2 - num3);
      }
      num3 = num;
      return text.Substring(num3, text.Length - num3);
    }

    // Token: 0x060018B5 RID: 6325 RVA: 0x003CC350 File Offset: 0x003CC350
    private static string Literal(string text)
    {
      StringBuilder stringBuilder = new StringBuilder(text.Length * 2);
      foreach (char c in text)
      {
        stringBuilder.Append('\u0001').Append(c);
      }
      return stringBuilder.ToString();
    }

    // Token: 0x060018B6 RID: 6326 RVA: 0x003CC39C File Offset: 0x003CC39C
    private static bool IsDateFormat(string cleaned)
    {
      bool flag = false;
      foreach (char c in cleaned)
      {
        if (c == '\u0001')
        {
          flag = true;
        }
        else
        {
          if (c == '\u0002')
          {
            return true;
          }
          bool flag2 = flag;
          flag = false;
          if (!flag2)
          {
            char c2 = char.ToLowerInvariant(c);
            if (c2 <= 'h')
            {
              if (c2 != 'd' && c2 != 'h')
              {
                goto IL_006B;
              }
            }
            else
            {
              if (c2 == 'm')
              {
                return !cleaned.Contains('0') && !cleaned.Contains('#');
              }
              if (c2 != 's' && c2 != 'y')
              {
                goto IL_006B;
              }
            }
            return true;
          }
        }
        IL_006B:;
      }
      return false;
    }

    // Token: 0x060018B7 RID: 6327 RVA: 0x003CC424 File Offset: 0x003CC424
    private static string FormatNumber(double value, string code)
    {
      List<ValueTuple<char, bool>> items = new List<ValueTuple<char, bool>>();
      int j = 0;
      while (j < code.Length)
      {
        if (code[j] == '\u0001' && j + 1 < code.Length)
        {
          items.Add(new ValueTuple<char, bool>(code[j + 1], true));
          j += 2;
        }
        else
        {
          items.Add(new ValueTuple<char, bool>(code[j], false));
          j++;
        }
      }
      List<int> list = (from i in Enumerable.Range(0, items.Count)
        where !items[i].Item2 && "0#?".Contains(items[i].Item1)
        select i).ToList<int>();
      if (list.Count == 0)
      {
        return new string(items.Select<ValueTuple<char, bool>, char>(([TupleElementNames(new string[] { "Character", "IsLiteral" })] ValueTuple<char, bool> i) => i.Item1).ToArray<char>()) + SpreadsheetNumberFormat.General(value);
      }
      int num = list[0];
      List<int> list2 = list;
      int num2 = list2[list2.Count - 1];
      int num3 = num;
      if (num3 > 0 && !items[num3 - 1].Item2 && items[num3 - 1].Item1 == '.')
      {
        num3--;
      }
      while (num2 + 1 < items.Count && !items[num2 + 1].Item2 && items[num2 + 1].Item1 == ',')
      {
        num2++;
      }
      string text = new string((from i in items.Take<ValueTuple<char, bool>>(num3)
        select i.Item1).ToArray<char>());
      string text2 = new string((from i in items.Skip<ValueTuple<char, bool>>(num3).Take<ValueTuple<char, bool>>(num2 - num3 + 1)
        where !i.Item2
        select i.Item1).ToArray<char>());
      string text3 = new string((from i in items.Skip<ValueTuple<char, bool>>(num2 + 1)
        select i.Item1).ToArray<char>());
      int num4 = items.Take<ValueTuple<char, bool>>(num3).Concat<ValueTuple<char, bool>>(items.Skip<ValueTuple<char, bool>>(num2 + 1)).Count<ValueTuple<char, bool>>(([TupleElementNames(new string[] { "Character", "IsLiteral" })] ValueTuple<char, bool> i) => !i.Item2 && i.Item1 == '%');
      double num5 = value;
      if (num4 > 0)
      {
        num5 *= Math.Pow(100.0, (double)num4);
      }
      while (text2.EndsWith(','))
      {
        string text4 = text2;
        text2 = text4.Substring(0, text4.Length - 1);
        num5 /= 1000.0;
      }
      int num6 = text2.IndexOfAny(new char[] { 'E', 'e' });
      if (num6 >= 0)
      {
        List<string> list3 = (from p in text2.Substring(0, num6).Split('.', 2, StringSplitOptions.None)
          where p.Length > 0
          select p).ToList<string>();
        int num7 = ((list3.Count > 1) ? list3[1].Length : 0);
        return text + SpreadsheetNumberFormat.PrintfE(num5, num7) + text3;
      }
      string[] array = text2.Split('.', StringSplitOptions.None);
      string text5 = array[0];
      string text6 = ((array.Length > 1) ? array[1] : "");
      int num8 = text6.Count<char>((char c) => c == '0');
      int num9 = text6.Count<char>((char c) => "0#?".Contains(c));
      int num10 = text5.Count<char>((char c) => c == '0');
      string text7 = SpreadsheetNumberFormat.DecimalFormat(num5, text5.Contains(','), num10, num8, num9) ?? SpreadsheetNumberFormat.General(num5);
      return text + text7 + text3;
    }

    // Token: 0x060018B8 RID: 6328 RVA: 0x003CC880 File Offset: 0x003CC880
    [NullableContext(2)]
    internal static string DecimalFormat(double number, bool usesGrouping, int minimumIntegerDigits, int minimumFractionDigits, int maximumFractionDigits)
    {
      if (!double.IsFinite(number))
      {
        return null;
      }
      bool flag = number < 0.0 || (number == 0.0 && double.IsNegative(number));
      ValueTuple<string, string> valueTuple = SpreadsheetNumberFormat.RoundedDigits(Math.Abs(number), maximumFractionDigits);
      string text = valueTuple.Item1;
      string text2 = valueTuple.Item2;
      text = text.TrimStart('0');
      while (text2.Length > minimumFractionDigits && text2.EndsWith('0'))
      {
        string text3 = text2;
        text2 = text3.Substring(0, text3.Length - 1);
      }
      if (text.Length < minimumIntegerDigits)
      {
        text = new string('0', minimumIntegerDigits - text.Length) + text;
      }
      if (text.Length == 0 && text2.Length == 0)
      {
        text = "0";
      }
      if (usesGrouping && text.Length > 3)
      {
        StringBuilder stringBuilder = new StringBuilder();
        int num = text.Length % 3;
        if (num > 0)
        {
          stringBuilder.Append(text, 0, num);
        }
        for (int i = num; i < text.Length; i += 3)
        {
          if (stringBuilder.Length > 0)
          {
            stringBuilder.Append(',');
          }
          stringBuilder.Append(text, i, 3);
        }
        text = stringBuilder.ToString();
      }
      string text4 = ((text2.Length > 0) ? (text + "." + text2) : text);
      if (!flag)
      {
        return text4;
      }
      return "-" + text4;
    }

    // Token: 0x060018B9 RID: 6329 RVA: 0x003CC9D4 File Offset: 0x003CC9D4
    [return: TupleElementNames(new string[] { "Integer", "Fraction" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    private static ValueTuple<string, string> RoundedDigits(double magnitude, int fractionDigits)
    {
      string text = magnitude.ToString("R", CultureInfo.InvariantCulture);
      string text2 = text;
      int num = 0;
      int num2 = text.IndexOfAny(new char[] { 'E', 'e' });
      string text3;
      int num3;
      if (num2 >= 0)
      {
        text2 = text.Substring(0, num2);
        text3 = text;
        num3 = num2 + 1;
        num = int.Parse(text3.Substring(num3, text3.Length - num3), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
      }
      int num4 = text2.IndexOf('.');
      string text4 = ((num4 >= 0) ? text2.Remove(num4, 1) : text2);
      int num5 = ((num4 >= 0) ? num4 : text2.Length) + num;
      if (num5 < 0)
      {
        text4 = new string('0', -num5) + text4;
        num5 = 0;
      }
      if (num5 > text4.Length)
      {
        text4 += new string('0', num5 - text4.Length);
      }
      string text5 = text4.Substring(0, num5);
      text3 = text4;
      num3 = num5;
      string text6 = text3.Substring(num3, text3.Length - num3);
      if (text6.Length <= fractionDigits)
      {
        return new ValueTuple<string, string>(text5, text6 + new string('0', fractionDigits - text6.Length));
      }
      bool flag = text6[fractionDigits] >= '5';
      char[] array = (text5 + text6.Substring(0, fractionDigits)).ToCharArray();
      if (!flag)
      {
        string text7 = new string(array);
        string text8 = text7.Substring(0, text5.Length);
        text3 = text7;
        num3 = text5.Length;
        return new ValueTuple<string, string>(text8, text3.Substring(num3, text3.Length - num3));
      }
      int i;
      for (i = array.Length - 1; i >= 0; i--)
      {
        if (array[i] != '9')
        {
          char[] array2 = array;
          int num6 = i;
          array2[num6] += '\u0001';
          break;
        }
        array[i] = '0';
      }
      string text9 = new string(array);
      if (i < 0)
      {
        text9 = "1" + text9;
        text5 = text9.Substring(0, text5.Length + 1);
        string text10 = text5;
        text3 = text9;
        num3 = text5.Length;
        return new ValueTuple<string, string>(text10, text3.Substring(num3, text3.Length - num3));
      }
      string text11 = text9.Substring(0, text5.Length);
      text3 = text9;
      num3 = text5.Length;
      return new ValueTuple<string, string>(text11, text3.Substring(num3, text3.Length - num3));
    }

    // Token: 0x060018BA RID: 6330 RVA: 0x003CCC30 File Offset: 0x003CCC30
    private static string FormatDate(double serial, string code)
    {
      List<string> list = SpreadsheetNumberFormat.DateTokens(code);
      if (list.Any<string>((string t) => t.Length > 0 && t[0] == '\u0002') && serial < 0.0)
      {
        return "-" + SpreadsheetNumberFormat.FormatDate(-serial, code);
      }
      double num = Math.Round(serial * 86400.0, MidpointRounding.AwayFromZero);
      SpreadsheetNumberFormat.CalendarDate? calendarDate = SpreadsheetNumberFormat.CalendarDate.FromExcelSeconds(num);
      if (calendarDate != null)
      {
        SpreadsheetNumberFormat.CalendarDate valueOrDefault = calendarDate.GetValueOrDefault();
        bool flag = list.Any<string>(delegate(string t)
        {
          string text6 = t.ToLowerInvariant();
          return text6 == "am/pm" || text6 == "a/p";
        });
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < list.Count; i++)
        {
          string text = list[i];
          string text2 = text.ToLowerInvariant();
          if (text[0] == '\u0002')
          {
            string text3 = text;
            string text4 = text3.Substring(1, text3.Length - 1);
            double num2 = (double)((text4[0] == 'h') ? 3600 : ((text4[0] == 'm') ? 60 : 1));
            string text5 = ((long)Math.Floor(num / num2)).ToString(CultureInfo.InvariantCulture);
            if (text5.Length < text4.Length)
            {
              text5 = new string('0', text4.Length - text5.Length) + text5;
            }
            stringBuilder.Append(text5);
          }
          else if (text[0] == '\u0001')
          {
            stringBuilder.Append(text, 1, text.Length - 1);
          }
          else
          {
            char c = text2[0];
            if (c <= 'd')
            {
              if (c == '0')
              {
                stringBuilder.Append('0', text2.Length);
                goto IL_0487;
              }
              if (c == 'a')
              {
                stringBuilder.Append((valueOrDefault.Hour < 12) ? "AM" : "PM");
                goto IL_0487;
              }
              if (c == 'd')
              {
                StringBuilder stringBuilder2 = stringBuilder;
                int num3 = text2.Length;
                string text3;
                switch (num3)
                {
                case 1:
                  text3 = valueOrDefault.Day.ToString(CultureInfo.InvariantCulture);
                  break;
                case 2:
                  text3 = valueOrDefault.Day.ToString("00", CultureInfo.InvariantCulture);
                  break;
                case 3:
                  text3 = SpreadsheetNumberFormat.DayAbbreviations[valueOrDefault.DayOfWeek];
                  break;
                default:
                  text3 = SpreadsheetNumberFormat.DayNames[valueOrDefault.DayOfWeek];
                  break;
                }
                stringBuilder2.Append(text3);
                goto IL_0487;
              }
            }
            else if (c <= 'm')
            {
              if (c == 'h')
              {
                stringBuilder.Append((flag ? ((valueOrDefault.Hour % 12 == 0) ? 12 : (valueOrDefault.Hour % 12)) : valueOrDefault.Hour).ToString((text2.Length >= 2) ? "00" : "0", CultureInfo.InvariantCulture));
                goto IL_0487;
              }
              if (c == 'm')
              {
                int num3;
                if (SpreadsheetNumberFormat.IsMinuteToken(i, list))
                {
                  StringBuilder stringBuilder3 = stringBuilder;
                  num3 = valueOrDefault.Minute;
                  stringBuilder3.Append(num3.ToString((text2.Length >= 2) ? "00" : "0", CultureInfo.InvariantCulture));
                  goto IL_0487;
                }
                StringBuilder stringBuilder2 = stringBuilder;
                num3 = text2.Length;
                string text3;
                switch (num3)
                {
                case 1:
                  text3 = valueOrDefault.Month.ToString(CultureInfo.InvariantCulture);
                  break;
                case 2:
                  text3 = valueOrDefault.Month.ToString("00", CultureInfo.InvariantCulture);
                  break;
                case 3:
                  text3 = SpreadsheetNumberFormat.MonthAbbreviations[valueOrDefault.Month - 1];
                  break;
                case 4:
                  goto IL_042C;
                case 5:
                  text3 = SpreadsheetNumberFormat.MonthNames[valueOrDefault.Month - 1].Substring(0, 1);
                  break;
                default:
                  goto IL_042C;
                }
                IL_043D:
                stringBuilder2.Append(text3);
                goto IL_0487;
                IL_042C:
                text3 = SpreadsheetNumberFormat.MonthNames[valueOrDefault.Month - 1];
                goto IL_043D;
              }
            }
            else
            {
              if (c == 's')
              {
                StringBuilder stringBuilder4 = stringBuilder;
                int num3 = valueOrDefault.Second;
                stringBuilder4.Append(num3.ToString((text2.Length >= 2) ? "00" : "0", CultureInfo.InvariantCulture));
                goto IL_0487;
              }
              if (c == 'y')
              {
                stringBuilder.Append((text2.Length <= 2) ? (valueOrDefault.Year % 100L).ToString("00", CultureInfo.InvariantCulture) : valueOrDefault.Year.ToString("0000", CultureInfo.InvariantCulture));
                goto IL_0487;
              }
            }
            stringBuilder.Append(text);
          }
          IL_0487:;
        }
        return stringBuilder.ToString();
      }
      return SpreadsheetNumberFormat.General(serial);
    }

    // Token: 0x060018BB RID: 6331 RVA: 0x003CD0E0 File Offset: 0x003CD0E0
    private unsafe static List<string> DateTokens(string code)
    {
      List<string> list = new List<string>();
      int i = 0;
      while (i < code.Length)
      {
        char c = code[i];
        if (c == '\u0001' && i + 1 < code.Length)
        {
          List<string> list2 = list;
          <>y__InlineArray2<char> <>y__InlineArray = default(<>y__InlineArray2<char>);
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray2<char>, char>(ref <>y__InlineArray, 0) = '\u0001';
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray2<char>, char>(ref <>y__InlineArray, 1) = code[i + 1];
          list2.Add(new string(<PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray2<char>, char>(in <>y__InlineArray, 2)));
          i += 2;
        }
        else if (c == '\u0002' && i + 1 < code.Length)
        {
          char c2 = code[i + 1];
          int num = i + 1;
          while (num < code.Length && code[num] == c2)
          {
            num++;
          }
          List<string> list3 = list;
          int num2 = i;
          list3.Add(code.Substring(num2, num - num2));
          i = num;
        }
        else
        {
          char c3 = char.ToLowerInvariant(c);
          if ("ydhms0".Contains(c3))
          {
            int num3 = i;
            while (num3 < code.Length && char.ToLowerInvariant(code[num3]) == c3)
            {
              num3++;
            }
            List<string> list4 = list;
            int num2 = i;
            list4.Add(code.Substring(num2, num3 - num2));
            i = num3;
          }
          else
          {
            if (c3 == 'a')
            {
              int num2 = i;
              string text = code.Substring(num2, code.Length - num2).ToLowerInvariant();
              if (text.StartsWith("am/pm", StringComparison.Ordinal))
              {
                list.Add("AM/PM");
                i += 5;
                continue;
              }
              if (text.StartsWith("a/p", StringComparison.Ordinal))
              {
                list.Add("a/p");
                i += 3;
                continue;
              }
            }
            list.Add(c.ToString());
            i++;
          }
        }
      }
      return list;
    }

    // Token: 0x060018BC RID: 6332 RVA: 0x003CD290 File Offset: 0x003CD290
    private static bool IsMinuteToken(int index, List<string> tokens)
    {
      for (int i = index - 1; i >= 0; i--)
      {
        char? c = SpreadsheetNumberFormat.UnitLetter(tokens[i]);
        if (c != null)
        {
          char valueOrDefault = c.GetValueOrDefault();
          if ("ydhms".Contains(valueOrDefault))
          {
            if (valueOrDefault == 'h')
            {
              return true;
            }
            break;
          }
        }
      }
      for (int j = index + 1; j < tokens.Count; j++)
      {
        char? c = SpreadsheetNumberFormat.UnitLetter(tokens[j]);
        if (c != null)
        {
          char valueOrDefault2 = c.GetValueOrDefault();
          if ("ydhms".Contains(valueOrDefault2))
          {
            return valueOrDefault2 == 's';
          }
        }
      }
      return false;
    }

    // Token: 0x060018BD RID: 6333 RVA: 0x003CD328 File Offset: 0x003CD328
    private static char? UnitLetter(string token)
    {
      if (token.Length == 0)
      {
        return null;
      }
      string text = token.ToLowerInvariant();
      if (text[0] != '\u0002')
      {
        return new char?(text[0]);
      }
      if (text.Length <= 1)
      {
        return null;
      }
      return new char?(text[1]);
    }

    // Token: 0x04000A76 RID: 2678
    private const char LiteralMarker = '\u0001';

    // Token: 0x04000A77 RID: 2679
    private const char ElapsedMarker = '\u0002';

    // Token: 0x04000A78 RID: 2680
    private static readonly string[] MonthNames = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames;

    // Token: 0x04000A79 RID: 2681
    private static readonly string[] MonthAbbreviations = CultureInfo.InvariantCulture.DateTimeFormat.AbbreviatedMonthNames;

    // Token: 0x04000A7A RID: 2682
    private static readonly string[] DayNames = CultureInfo.InvariantCulture.DateTimeFormat.DayNames;

    // Token: 0x04000A7B RID: 2683
    private static readonly string[] DayAbbreviations = CultureInfo.InvariantCulture.DateTimeFormat.AbbreviatedDayNames;

    // Token: 0x0200058E RID: 1422
    [NullableContext(0)]
    internal readonly struct CalendarDate : IEquatable<SpreadsheetNumberFormat.CalendarDate>
    {
      // Token: 0x0600359D RID: 13725 RVA: 0x00441F54 File Offset: 0x00441F54
      public CalendarDate(long Year, int Month, int Day, int DayOfWeek, int Hour, int Minute, int Second)
      {
        this.Year = Year;
        this.Month = Month;
        this.Day = Day;
        this.DayOfWeek = DayOfWeek;
        this.Hour = Hour;
        this.Minute = Minute;
        this.Second = Second;
      }

      // Token: 0x1700096F RID: 2415
      // (get) Token: 0x0600359E RID: 13726 RVA: 0x00441F8C File Offset: 0x00441F8C
      // (set) Token: 0x0600359F RID: 13727 RVA: 0x00441F94 File Offset: 0x00441F94
      public long Year { get; set; }

      // Token: 0x17000970 RID: 2416
      // (get) Token: 0x060035A0 RID: 13728 RVA: 0x00441FA0 File Offset: 0x00441FA0
      // (set) Token: 0x060035A1 RID: 13729 RVA: 0x00441FA8 File Offset: 0x00441FA8
      public int Month { get; set; }

      // Token: 0x17000971 RID: 2417
      // (get) Token: 0x060035A2 RID: 13730 RVA: 0x00441FB4 File Offset: 0x00441FB4
      // (set) Token: 0x060035A3 RID: 13731 RVA: 0x00441FBC File Offset: 0x00441FBC
      public int Day { get; set; }

      // Token: 0x17000972 RID: 2418
      // (get) Token: 0x060035A4 RID: 13732 RVA: 0x00441FC8 File Offset: 0x00441FC8
      // (set) Token: 0x060035A5 RID: 13733 RVA: 0x00441FD0 File Offset: 0x00441FD0
      public int DayOfWeek { get; set; }

      // Token: 0x17000973 RID: 2419
      // (get) Token: 0x060035A6 RID: 13734 RVA: 0x00441FDC File Offset: 0x00441FDC
      // (set) Token: 0x060035A7 RID: 13735 RVA: 0x00441FE4 File Offset: 0x00441FE4
      public int Hour { get; set; }

      // Token: 0x17000974 RID: 2420
      // (get) Token: 0x060035A8 RID: 13736 RVA: 0x00441FF0 File Offset: 0x00441FF0
      // (set) Token: 0x060035A9 RID: 13737 RVA: 0x00441FF8 File Offset: 0x00441FF8
      public int Minute { get; set; }

      // Token: 0x17000975 RID: 2421
      // (get) Token: 0x060035AA RID: 13738 RVA: 0x00442004 File Offset: 0x00442004
      // (set) Token: 0x060035AB RID: 13739 RVA: 0x0044200C File Offset: 0x0044200C
      public int Second { get; set; }

      // Token: 0x060035AC RID: 13740 RVA: 0x00442018 File Offset: 0x00442018
      public static SpreadsheetNumberFormat.CalendarDate? FromExcelSeconds(double totalSeconds)
      {
        if (double.IsNaN(totalSeconds) || Math.Abs(totalSeconds) > 1000000000000000.0)
        {
          return null;
        }
        long num2;
        long num = Math.DivRem((long)totalSeconds, 86400L, out num2);
        if (num2 < 0L)
        {
          num2 += 86400L;
          num -= 1L;
        }
        long num3 = 2415019L + num;
        long num4;
        int num5;
        int num6;
        if (num3 < 2299161L)
        {
          ValueTuple<long, int, int> valueTuple = SpreadsheetNumberFormat.CalendarDate.JulianFromJulianDay(num3);
          num4 = valueTuple.Item1;
          num5 = valueTuple.Item2;
          num6 = valueTuple.Item3;
        }
        else
        {
          ValueTuple<long, int, int> valueTuple = SpreadsheetNumberFormat.CalendarDate.GregorianFromJulianDay(num3);
          num4 = valueTuple.Item1;
          num5 = valueTuple.Item2;
          num6 = valueTuple.Item3;
        }
        int num7 = (int)(((num3 + 1L) % 7L + 7L) % 7L);
        return new SpreadsheetNumberFormat.CalendarDate?(new SpreadsheetNumberFormat.CalendarDate((num4 > 0L) ? num4 : (1L - num4), num5, num6, num7, (int)(num2 / 3600L), (int)(num2 / 60L % 60L), (int)(num2 % 60L)));
      }

      // Token: 0x060035AD RID: 13741 RVA: 0x0044210C File Offset: 0x0044210C
      [return: TupleElementNames(new string[] { "Year", "Month", "Day" })]
      private static ValueTuple<long, int, int> GregorianFromJulianDay(long jd)
      {
        long num = jd + 32044L;
        long num2 = SpreadsheetNumberFormat.CalendarDate.FloorDiv(4L * num + 3L, 146097L);
        return SpreadsheetNumberFormat.CalendarDate.FromDayOfFourYears(num - SpreadsheetNumberFormat.CalendarDate.FloorDiv(146097L * num2, 4L), 100L * num2);
      }

      // Token: 0x060035AE RID: 13742 RVA: 0x00442150 File Offset: 0x00442150
      [return: TupleElementNames(new string[] { "Year", "Month", "Day" })]
      private static ValueTuple<long, int, int> JulianFromJulianDay(long jd)
      {
        return SpreadsheetNumberFormat.CalendarDate.FromDayOfFourYears(jd + 32082L, 0L);
      }

      // Token: 0x060035AF RID: 13743 RVA: 0x00442164 File Offset: 0x00442164
      [return: TupleElementNames(new string[] { "Year", "Month", "Day" })]
      private static ValueTuple<long, int, int> FromDayOfFourYears(long c, long centuries)
      {
        long num = SpreadsheetNumberFormat.CalendarDate.FloorDiv(4L * c + 3L, 1461L);
        long num2 = c - SpreadsheetNumberFormat.CalendarDate.FloorDiv(1461L * num, 4L);
        long num3 = SpreadsheetNumberFormat.CalendarDate.FloorDiv(5L * num2 + 2L, 153L);
        int num4 = (int)(num2 - SpreadsheetNumberFormat.CalendarDate.FloorDiv(153L * num3 + 2L, 5L) + 1L);
        int num5 = (int)(num3 + 3L - 12L * SpreadsheetNumberFormat.CalendarDate.FloorDiv(num3, 10L));
        return new ValueTuple<long, int, int>(centuries + num - 4800L + SpreadsheetNumberFormat.CalendarDate.FloorDiv(num3, 10L), num5, num4);
      }

      // Token: 0x060035B0 RID: 13744 RVA: 0x004421F4 File Offset: 0x004421F4
      private static long FloorDiv(long a, long b)
      {
        long num = a / b;
        if (a % b != 0L && a < 0L != b < 0L)
        {
          num -= 1L;
        }
        return num;
      }

      // Token: 0x060035B1 RID: 13745 RVA: 0x0044221C File Offset: 0x0044221C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("CalendarDate");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060035B2 RID: 13746 RVA: 0x00442268 File Offset: 0x00442268
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("Year = ");
        builder.Append(this.Year.ToString());
        builder.Append(", Month = ");
        builder.Append(this.Month.ToString());
        builder.Append(", Day = ");
        builder.Append(this.Day.ToString());
        builder.Append(", DayOfWeek = ");
        builder.Append(this.DayOfWeek.ToString());
        builder.Append(", Hour = ");
        builder.Append(this.Hour.ToString());
        builder.Append(", Minute = ");
        builder.Append(this.Minute.ToString());
        builder.Append(", Second = ");
        builder.Append(this.Second.ToString());
        return true;
      }

      // Token: 0x060035B3 RID: 13747 RVA: 0x00442388 File Offset: 0x00442388
      [CompilerGenerated]
      public static bool operator !=(SpreadsheetNumberFormat.CalendarDate left, SpreadsheetNumberFormat.CalendarDate right)
      {
        return !(left == right);
      }

      // Token: 0x060035B4 RID: 13748 RVA: 0x00442394 File Offset: 0x00442394
      [CompilerGenerated]
      public static bool operator ==(SpreadsheetNumberFormat.CalendarDate left, SpreadsheetNumberFormat.CalendarDate right)
      {
        return left.Equals(right);
      }

      // Token: 0x060035B5 RID: 13749 RVA: 0x004423A0 File Offset: 0x004423A0
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((((EqualityComparer<long>.Default.GetHashCode(this.<Year>k__BackingField) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Month>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Day>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<DayOfWeek>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Hour>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Minute>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Second>k__BackingField);
      }

      // Token: 0x060035B6 RID: 13750 RVA: 0x00442448 File Offset: 0x00442448
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is SpreadsheetNumberFormat.CalendarDate && this.Equals((SpreadsheetNumberFormat.CalendarDate)obj);
      }

      // Token: 0x060035B7 RID: 13751 RVA: 0x00442460 File Offset: 0x00442460
      [CompilerGenerated]
      public bool Equals(SpreadsheetNumberFormat.CalendarDate other)
      {
        return EqualityComparer<long>.Default.Equals(this.<Year>k__BackingField, other.<Year>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Month>k__BackingField, other.<Month>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Day>k__BackingField, other.<Day>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<DayOfWeek>k__BackingField, other.<DayOfWeek>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Hour>k__BackingField, other.<Hour>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Minute>k__BackingField, other.<Minute>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Second>k__BackingField, other.<Second>k__BackingField);
      }

      // Token: 0x060035B8 RID: 13752 RVA: 0x00442518 File Offset: 0x00442518
      [CompilerGenerated]
      public void Deconstruct(out long Year, out int Month, out int Day, out int DayOfWeek, out int Hour, out int Minute, out int Second)
      {
        Year = this.Year;
        Month = this.Month;
        Day = this.Day;
        DayOfWeek = this.DayOfWeek;
        Hour = this.Hour;
        Minute = this.Minute;
        Second = this.Second;
      }

      // Token: 0x040020D1 RID: 8401
      private const long ExcelEpochJulianDay = 2415019L;

      // Token: 0x040020D2 RID: 8402
      private const long GregorianCutoverJulianDay = 2299161L;
    }
  }
}
