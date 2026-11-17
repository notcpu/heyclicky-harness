using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;

namespace HeyClicky.Core.Brain
{
  // Token: 0x02000203 RID: 515
  [NullableContext(1)]
  [Nullable(0)]
  public static class ProgramText
  {
    // Token: 0x06001E13 RID: 7699 RVA: 0x003EBD84 File Offset: 0x003EBD84
    public static string WithPlainFences(string text)
    {
      if (!text.Contains("\\`"))
      {
        return text;
      }
      return text.Replace("\\`", "`");
    }

    // Token: 0x06001E14 RID: 7700 RVA: 0x003EBDA8 File Offset: 0x003EBDA8
    public static string ExtractProgramCode(string rawText)
    {
      string text = ProgramText.WithPlainFences(rawText);
      int num = text.IndexOf("```", StringComparison.Ordinal);
      if (num >= 0)
      {
        int num2 = num + 3;
        int num3 = text.IndexOf('\n', num2);
        int num4 = ((num3 >= 0) ? (num3 + 1) : num2);
        int num5 = text.IndexOf("```", num4, StringComparison.Ordinal);
        string text2 = text;
        int num6 = num4;
        return text2.Substring(num6, ((num5 >= 0) ? num5 : text.Length) - num6).Trim();
      }
      if (!text.Contains("step(") && !text.Contains("say("))
      {
        return "";
      }
      return text.Trim();
    }

    // Token: 0x06001E15 RID: 7701 RVA: 0x003EBE44 File Offset: 0x003EBE44
    [return: Nullable(2)]
    public static string ProgramCode(string rawText, bool isComplete)
    {
      ProgramText.<>c__DisplayClass3_0 CS$<>8__locals1;
      CS$<>8__locals1.isComplete = isComplete;
      string text = ProgramText.WithPlainFences(rawText);
      CS$<>8__locals1.pieces = new List<string>(4);
      bool flag = false;
      int num = 0;
      int num5;
      int num7;
      for (;;)
      {
        int num2 = text.IndexOf("```", num, StringComparison.Ordinal);
        if (num2 < 0)
        {
          goto IL_014B;
        }
        ProgramText.<ProgramCode>g__AddOutside|3_1(text.AsSpan(num, num2 - num), true, ref CS$<>8__locals1);
        int num3 = num2 + 3;
        int num4 = text.IndexOf('\n', num3);
        if (num4 < 0)
        {
          break;
        }
        flag = true;
        num5 = num4 + 1;
        int num6 = text.IndexOf("```", num5, StringComparison.Ordinal);
        if (num6 < 0)
        {
          goto Block_3;
        }
        string text2 = text;
        num7 = num5;
        string text3 = text2.Substring(num7, num6 - num7).Trim(new char[] { '\n', '\r' });
        if (!string.IsNullOrWhiteSpace(text3))
        {
          CS$<>8__locals1.pieces.Add(text3 + "\n;");
        }
        num = num6 + 3;
      }
      num = text.Length;
      goto IL_014B;
      Block_3:
      string text4 = text;
      num7 = num5;
      string text5 = text4.Substring(num7, text4.Length - num7).Trim(new char[] { '\n', '\r' });
      CS$<>8__locals1.pieces.Add(CS$<>8__locals1.isComplete ? text5 : text5.TrimEnd('`').TrimEnd(new char[] { '\n', '\r' }));
      num = text.Length;
      IL_014B:
      if (num < text.Length)
      {
        ProgramText.<ProgramCode>g__AddOutside|3_1(text.AsSpan(num), false, ref CS$<>8__locals1);
      }
      string text6 = string.Join("\n", CS$<>8__locals1.pieces.Where<string>((string p) => p.Length > 0));
      if (CS$<>8__locals1.isComplete)
      {
        if (text6.Length != 0 || flag)
        {
          return text6.Trim();
        }
        return ProgramText.ExtractProgramCode(rawText);
      }
      else
      {
        if (text6.Length != 0)
        {
          return text6;
        }
        return null;
      }
    }

    // Token: 0x06001E16 RID: 7702 RVA: 0x003EC01C File Offset: 0x003EC01C
    public static string WithoutLinks(string code)
    {
      if (!code.Contains("http", StringComparison.Ordinal))
      {
        return code;
      }
      string text = code;
      ValueTuple<Regex, string>[] linkPatterns = ProgramText.LinkPatterns;
      for (int i = 0; i < linkPatterns.Length; i++)
      {
        ValueTuple<Regex, string> valueTuple = linkPatterns[i];
        Regex item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        List<ValueTuple<int, int>> stringRanges = ProgramText.StringLiteralRanges(text);
        List<Match> list = (from m in item.Matches(text)
          where !stringRanges.Any<ValueTuple<int, int>>(([TupleElementNames(new string[] { "Start", "Length" })] ValueTuple<int, int> r) => r.Item1 < m.Index + m.Length && m.Index < r.Item1 + r.Item2)
          select m).ToList<Match>();
        if (list.Count != 0)
        {
          StringBuilder stringBuilder = new StringBuilder(text);
          for (int j = list.Count - 1; j >= 0; j--)
          {
            Match match = list[j];
            stringBuilder.Remove(match.Index, match.Length).Insert(match.Index, match.Result(item2));
          }
          text = stringBuilder.ToString();
        }
      }
      return text;
    }

    // Token: 0x06001E17 RID: 7703 RVA: 0x003EC108 File Offset: 0x003EC108
    [GeneratedRegex("\\s*\\(\\[[^\\]\\n]{1,80}\\]\\(https?://[^)\\s\"]+\\)\\)")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex CitationPattern()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__CitationPattern_0.Instance;
    }

    // Token: 0x06001E18 RID: 7704 RVA: 0x003EC110 File Offset: 0x003EC110
    [GeneratedRegex("\\[([^\\]\\n]{1,80})\\]\\(https?://[^)\\s\"]+\\)")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex MarkdownLinkPattern()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MarkdownLinkPattern_1.Instance;
    }

    // Token: 0x06001E19 RID: 7705 RVA: 0x003EC118 File Offset: 0x003EC118
    [GeneratedRegex("\\s*https?://[^\\s\"'),]+")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex BareUrlPattern()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BareUrlPattern_2.Instance;
    }

    // Token: 0x06001E1A RID: 7706 RVA: 0x003EC120 File Offset: 0x003EC120
    [return: TupleElementNames(new string[] { "Start", "Length" })]
    [return: Nullable(new byte[] { 1, 0 })]
    public static List<ValueTuple<int, int>> StringLiteralRanges(string code)
    {
      List<ValueTuple<int, int>> list = new List<ValueTuple<int, int>>();
      int i = 0;
      while (i < code.Length)
      {
        char c = code[i];
        if (c == '/' && i + 1 < code.Length && code[i + 1] == '/')
        {
          if (i != 0)
          {
            if (code[i - 1] == ':')
            {
              goto IL_0064;
            }
          }
          while (i < code.Length)
          {
            if (code[i] == '\n')
            {
              break;
            }
            i++;
          }
          continue;
        }
        IL_0064:
        if (c == '/' && i + 1 < code.Length && code[i + 1] == '*')
        {
          i += 2;
          while (i + 1 < code.Length && (code[i] != '*' || code[i + 1] != '/'))
          {
            i++;
          }
          i += 2;
        }
        else
        {
          bool flag = c == '"' || c == '\'' || c == '`';
          if (flag)
          {
            int num = i;
            i++;
            while (i < code.Length && code[i] != c && (c == '`' || code[i] != '\n'))
            {
              i += ((code[i] == '\\') ? 2 : 1);
            }
            int num2 = Math.Min(i + 1, code.Length);
            list.Add(new ValueTuple<int, int>(num, num2 - num));
            i = num2;
          }
          else
          {
            i++;
          }
        }
      }
      return list;
    }

    // Token: 0x06001E1B RID: 7707 RVA: 0x003EC274 File Offset: 0x003EC274
    [return: Nullable(2)]
    public static string FirstSayLiteral(string code)
    {
      int num = 0;
      int num2;
      for (;;)
      {
        num2 = code.IndexOf("say(", num, StringComparison.Ordinal);
        if (num2 < 0)
        {
          break;
        }
        num = num2 + 4;
        if (ProgramText.StartsStatementOnItsLine(code, num2))
        {
          goto Block_2;
        }
      }
      return null;
      Block_2:
      int num3 = num2 + 4;
      bool flag;
      for (;;)
      {
        flag = num3 < code.Length;
        if (flag)
        {
          char c = code[num3];
          bool flag2 = c == '\t' || c == ' ';
          flag = flag2;
        }
        if (!flag)
        {
          break;
        }
        num3++;
      }
      if (num3 >= code.Length)
      {
        return null;
      }
      char c2 = code[num3];
      if (c2 != '"' && c2 != '\'' && c2 != '`')
      {
        return null;
      }
      int num4 = num3 + 1;
      int num5 = num4;
      while (num5 < code.Length && code[num5] != c2)
      {
        if (code[num5] == '\\' || (c2 != '`' && code[num5] == '\n'))
        {
          return null;
        }
        if (c2 == '`' && code[num5] == '$' && num5 + 1 < code.Length && code[num5 + 1] == '{')
        {
          return null;
        }
        num5++;
      }
      if (num5 >= code.Length)
      {
        return null;
      }
      int num6 = num5 + 1;
      for (;;)
      {
        flag = num6 < code.Length;
        if (flag)
        {
          char c = code[num6];
          bool flag2 = c == '\t' || c == ' ';
          flag = flag2;
        }
        if (!flag)
        {
          break;
        }
        num6++;
      }
      flag = num6 >= code.Length;
      if (!flag)
      {
        char c = code[num6];
        bool flag2 = c == ')' || c == ',';
        flag = !flag2;
      }
      if (flag)
      {
        return null;
      }
      int num7 = num4;
      string text = code.Substring(num7, num5 - num7).Trim();
      if (text.Length != 0)
      {
        return text;
      }
      return null;
    }

    // Token: 0x06001E1C RID: 7708 RVA: 0x003EC438 File Offset: 0x003EC438
    private unsafe static bool StartsStatementOnItsLine(string code, int index)
    {
      int num = ((index == 0) ? 0 : (code.LastIndexOf('\n', index - 1) + 1));
      ReadOnlySpan<char> readOnlySpan = code.AsSpan(num, index - num).TrimEnd();
      if (readOnlySpan.Length == 0)
      {
        return true;
      }
      char c = (char)(*readOnlySpan[readOnlySpan.Length - 1]);
      bool flag = c == ';' || c == '{' || c == '}';
      return flag && readOnlySpan.IndexOfAny<char>("\"'`/") < 0;
    }

    // Token: 0x06001E1E RID: 7710 RVA: 0x003EC510 File Offset: 0x003EC510
    [NullableContext(0)]
    [CompilerGenerated]
    internal static void <ProgramCode>g__AddOutside|3_1(ReadOnlySpan<char> segment, bool closed, ref ProgramText.<>c__DisplayClass3_0 A_2)
    {
      ReadOnlySpan<char> readOnlySpan = segment.Trim();
      if (!A_2.isComplete)
      {
        readOnlySpan = readOnlySpan.TrimEnd('`').Trim();
      }
      if (readOnlySpan.IndexOf("say(", StringComparison.Ordinal) >= 0)
      {
        A_2.pieces.Add(readOnlySpan.ToString() + (closed ? "\n;" : ""));
      }
    }

    // Token: 0x04000CE9 RID: 3305
    public const string BlockEndMarker = "\n;";

    // Token: 0x04000CEA RID: 3306
    [TupleElementNames(new string[] { "Pattern", "Replacement" })]
    [Nullable(new byte[] { 1, 0, 1, 1 })]
    private static readonly ValueTuple<Regex, string>[] LinkPatterns = new ValueTuple<Regex, string>[]
    {
      new ValueTuple<Regex, string>(ProgramText.CitationPattern(), ""),
      new ValueTuple<Regex, string>(ProgramText.MarkdownLinkPattern(), "$1"),
      new ValueTuple<Regex, string>(ProgramText.BareUrlPattern(), "")
    };
  }
}
