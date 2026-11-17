using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;

namespace HeyClicky.Core.Documents.Markdown
{
  // Token: 0x020001BC RID: 444
  [NullableContext(1)]
  [Nullable(0)]
  public static class MarkdownViewerParser
  {
    // Token: 0x060019B9 RID: 6585 RVA: 0x003D3850 File Offset: 0x003D3850
    public static string[] SplitLines(string source)
    {
      return source.Replace("\r\n", "\n").Split(RuntimeHelpers.CreateSpan<char>(fieldof(<PrivateImplementationDetails>.2D93FD65168E5F5D296FC0C10CFBEB2FDEFE409D4B157B16675882B3B115EABB2).FieldHandle));
    }

    // Token: 0x060019BA RID: 6586 RVA: 0x003D3874 File Offset: 0x003D3874
    public static string TrimWhitespace(string text)
    {
      int i = 0;
      int num = text.Length;
      while (i < num)
      {
        if (!MarkdownViewerParser.IsSpace(text[i]))
        {
          break;
        }
        i++;
      }
      while (num > i && MarkdownViewerParser.IsSpace(text[num - 1]))
      {
        num--;
      }
      int num2 = i;
      return text.Substring(num2, num - num2);
    }

    // Token: 0x060019BB RID: 6587 RVA: 0x003D38C8 File Offset: 0x003D38C8
    private static bool IsSpace(char c)
    {
      return c == ' ' || c == '\t' || char.GetUnicodeCategory(c) == UnicodeCategory.SpaceSeparator;
    }

    // Token: 0x060019BC RID: 6588 RVA: 0x003D38E0 File Offset: 0x003D38E0
    public static IReadOnlyList<MarkdownViewerBlock> ParseBlocks(string markdownSource)
    {
      string[] array = MarkdownViewerParser.SplitLines(markdownSource);
      MarkdownViewerParser.<>c__DisplayClass5_0 CS$<>8__locals1;
      CS$<>8__locals1.blocks = new List<MarkdownViewerBlock>();
      CS$<>8__locals1.pendingParagraphLines = new List<string>();
      int i = 0;
      while (i < array.Length)
      {
        string text = array[i];
        string text2 = MarkdownViewerParser.TrimWhitespace(text);
        if (text2.Length == 0)
        {
          MarkdownViewerParser.<ParseBlocks>g__FlushPendingParagraph|5_0(ref CS$<>8__locals1);
          i++;
        }
        else if (text2.StartsWith("```", StringComparison.Ordinal) || text2.StartsWith("~~~", StringComparison.Ordinal))
        {
          MarkdownViewerParser.<ParseBlocks>g__FlushPendingParagraph|5_0(ref CS$<>8__locals1);
          string text3 = text2.Substring(0, 3);
          string text4 = text2;
          string text5 = MarkdownViewerParser.TrimWhitespace(text4.Substring(3, text4.Length - 3));
          List<string> list = new List<string>();
          for (i++; i < array.Length; i++)
          {
            string text6 = array[i];
            if (MarkdownViewerParser.TrimWhitespace(text6).StartsWith(text3, StringComparison.Ordinal))
            {
              i++;
              break;
            }
            list.Add(text6);
          }
          CS$<>8__locals1.blocks.Add(new MarkdownViewerBlock.CodeBlock((text5.Length == 0) ? null : text5, string.Join("\n", list)));
        }
        else
        {
          if (CS$<>8__locals1.pendingParagraphLines.Count > 0)
          {
            int? num = MarkdownViewerParser.SetextHeadingLevel(text2);
            if (num != null)
            {
              int valueOrDefault = num.GetValueOrDefault();
              string text7 = string.Join(" ", CS$<>8__locals1.pendingParagraphLines);
              CS$<>8__locals1.pendingParagraphLines.Clear();
              CS$<>8__locals1.blocks.Add(new MarkdownViewerBlock.Heading(valueOrDefault, text7));
              i++;
              continue;
            }
          }
          ValueTuple<int, string>? valueTuple = MarkdownViewerParser.ParseAtxHeading(text2);
          if (valueTuple != null)
          {
            ValueTuple<int, string> valueOrDefault2 = valueTuple.GetValueOrDefault();
            MarkdownViewerParser.<ParseBlocks>g__FlushPendingParagraph|5_0(ref CS$<>8__locals1);
            CS$<>8__locals1.blocks.Add(new MarkdownViewerBlock.Heading(valueOrDefault2.Item1, valueOrDefault2.Item2));
            i++;
          }
          else if (MarkdownViewerParser.IsHorizontalRule(text2))
          {
            MarkdownViewerParser.<ParseBlocks>g__FlushPendingParagraph|5_0(ref CS$<>8__locals1);
            CS$<>8__locals1.blocks.Add(new MarkdownViewerBlock.HorizontalRule());
            i++;
          }
          else if (text2.StartsWith('>'))
          {
            MarkdownViewerParser.<ParseBlocks>g__FlushPendingParagraph|5_0(ref CS$<>8__locals1);
            List<string> list2 = new List<string>();
            while (i < array.Length)
            {
              string text8 = MarkdownViewerParser.TrimWhitespace(array[i]);
              if (!text8.StartsWith('>'))
              {
                break;
              }
              string text4 = text8;
              string text9 = text4.Substring(1, text4.Length - 1);
              if (text9.StartsWith(' '))
              {
                text4 = text9;
                text9 = text4.Substring(1, text4.Length - 1);
              }
              list2.Add(text9);
              i++;
            }
            CS$<>8__locals1.blocks.Add(new MarkdownViewerBlock.Blockquote(MarkdownViewerParser.ParseBlocks(string.Join("\n", list2))));
          }
          else if (text2.Contains('|') && i + 1 < array.Length && MarkdownViewerParser.IsTableSeparatorRow(array[i + 1]))
          {
            MarkdownViewerParser.<ParseBlocks>g__FlushPendingParagraph|5_0(ref CS$<>8__locals1);
            List<string> list3 = MarkdownViewerParser.ParseTableRowCells(text2);
            i += 2;
            List<IReadOnlyList<string>> list4 = new List<IReadOnlyList<string>>();
            while (i < array.Length)
            {
              string text10 = MarkdownViewerParser.TrimWhitespace(array[i]);
              if (text10.Length == 0 || !text10.Contains('|'))
              {
                break;
              }
              list4.Add(MarkdownViewerParser.ParseTableRowCells(text10));
              i++;
            }
            CS$<>8__locals1.blocks.Add(new MarkdownViewerBlock.Table(list3, list4));
          }
          else if (MarkdownViewerParser.ParseListItemStart(text) != null)
          {
            MarkdownViewerParser.<ParseBlocks>g__FlushPendingParagraph|5_0(ref CS$<>8__locals1);
            List<MarkdownViewerListItem> list5 = new List<MarkdownViewerListItem>();
            while (i < array.Length)
            {
              string text11 = array[i];
              string text12 = MarkdownViewerParser.TrimWhitespace(text11);
              if (text12.Length == 0)
              {
                int num2 = i + 1;
                while (num2 < array.Length && MarkdownViewerParser.TrimWhitespace(array[num2]).Length == 0)
                {
                  num2++;
                }
                if (num2 >= array.Length || MarkdownViewerParser.ParseListItemStart(array[num2]) == null)
                {
                  break;
                }
                i = num2;
              }
              else
              {
                MarkdownViewerListItem markdownViewerListItem = MarkdownViewerParser.ParseListItemStart(text11);
                if (markdownViewerListItem != null)
                {
                  list5.Add(markdownViewerListItem);
                  i++;
                }
                else
                {
                  if (list5.Count <= 0 || !text11.StartsWith("  ", StringComparison.Ordinal))
                  {
                    break;
                  }
                  List<MarkdownViewerListItem> list6 = list5;
                  MarkdownViewerListItem markdownViewerListItem2 = list6[list6.Count - 1];
                  List<MarkdownViewerListItem> list7 = list5;
                  int num3 = list7.Count - 1;
                  MarkdownViewerListItem markdownViewerListItem3 = markdownViewerListItem2.<Clone>$();
                  markdownViewerListItem3.InlineMarkdown = markdownViewerListItem2.InlineMarkdown + " " + text12;
                  list7[num3] = markdownViewerListItem3;
                  i++;
                }
              }
            }
            CS$<>8__locals1.blocks.Add(new MarkdownViewerBlock.List(list5));
          }
          else
          {
            ValueTuple<string, string>? valueTuple2 = MarkdownViewerParser.ParseStandaloneImage(text2);
            if (valueTuple2 != null)
            {
              ValueTuple<string, string> valueOrDefault3 = valueTuple2.GetValueOrDefault();
              MarkdownViewerParser.<ParseBlocks>g__FlushPendingParagraph|5_0(ref CS$<>8__locals1);
              CS$<>8__locals1.blocks.Add(new MarkdownViewerBlock.Image(valueOrDefault3.Item1, valueOrDefault3.Item2));
              i++;
            }
            else
            {
              CS$<>8__locals1.pendingParagraphLines.Add(text2);
              i++;
            }
          }
        }
      }
      MarkdownViewerParser.<ParseBlocks>g__FlushPendingParagraph|5_0(ref CS$<>8__locals1);
      return CS$<>8__locals1.blocks;
    }

    // Token: 0x060019BD RID: 6589 RVA: 0x003D3D74 File Offset: 0x003D3D74
    [return: TupleElementNames(new string[] { "Level", "InlineMarkdown" })]
    [return: Nullable(new byte[] { 0, 1 })]
    private static ValueTuple<int, string>? ParseAtxHeading(string trimmedLine)
    {
      if (!trimmedLine.StartsWith('#'))
      {
        return null;
      }
      int num = 0;
      while (num < trimmedLine.Length && trimmedLine[num] == '#')
      {
        num++;
      }
      if (num > 6)
      {
        return null;
      }
      int num2 = num;
      string text = trimmedLine.Substring(num2, trimmedLine.Length - num2);
      if (text.Length != 0 && text[0] != ' ')
      {
        return null;
      }
      string text2 = MarkdownViewerParser.TrimWhitespace(text);
      text2 = text2.TrimEnd('#');
      text2 = MarkdownViewerParser.TrimWhitespace(text2);
      return new ValueTuple<int, string>?(new ValueTuple<int, string>(num, text2));
    }

    // Token: 0x060019BE RID: 6590 RVA: 0x003D3E1C File Offset: 0x003D3E1C
    private static int? SetextHeadingLevel(string trimmedLine)
    {
      if (trimmedLine.Length < 2)
      {
        return null;
      }
      if (trimmedLine.All<char>((char c) => c == '='))
      {
        return new int?(1);
      }
      if (trimmedLine.All<char>((char c) => c == '-'))
      {
        return new int?(2);
      }
      return null;
    }

    // Token: 0x060019BF RID: 6591 RVA: 0x003D3EA4 File Offset: 0x003D3EA4
    private static bool IsHorizontalRule(string trimmedLine)
    {
      string text = trimmedLine.Replace(" ", "");
      if (text.Length < 3)
      {
        return false;
      }
      if (!text.All<char>((char c) => c == '-'))
      {
        if (!text.All<char>((char c) => c == '*'))
        {
          return text.All<char>((char c) => c == '_');
        }
      }
      return true;
    }

    // Token: 0x060019C0 RID: 6592 RVA: 0x003D3F44 File Offset: 0x003D3F44
    public static bool IsTableSeparatorRow(string line)
    {
      string text = MarkdownViewerParser.TrimWhitespace(line);
      if (!text.Contains('-') || (!text.Contains('|') && !text.StartsWith(':')))
      {
        return false;
      }
      if (text.Length > 0)
      {
        return text.All<char>(delegate(char c)
        {
          if (c <= '-')
          {
            if (c != ' ' && c != '-')
            {
              goto IL_001F;
            }
          }
          else if (c != ':' && c != '|')
          {
            goto IL_001F;
          }
          return true;
          IL_001F:
          return false;
        });
      }
      return false;
    }

    // Token: 0x060019C1 RID: 6593 RVA: 0x003D3FA8 File Offset: 0x003D3FA8
    private static List<string> ParseTableRowCells(string rowLine)
    {
      string text = MarkdownViewerParser.TrimWhitespace(rowLine);
      if (text.StartsWith('|'))
      {
        string text2 = text;
        text = text2.Substring(1, text2.Length - 1);
      }
      if (text.EndsWith('|'))
      {
        string text2 = text;
        text = text2.Substring(0, text2.Length - 1);
      }
      IEnumerable<string> enumerable = text.Split('|', StringSplitOptions.None);
      Func<string, string> func;
      if ((func = MarkdownViewerParser.<>O.<0>__TrimWhitespace) == null)
      {
        func = (MarkdownViewerParser.<>O.<0>__TrimWhitespace = new Func<string, string>(MarkdownViewerParser.TrimWhitespace));
      }
      return enumerable.Select<string, string>(func).ToList<string>();
    }

    // Token: 0x060019C2 RID: 6594 RVA: 0x003D4024 File Offset: 0x003D4024
    [return: Nullable(2)]
    public static MarkdownViewerListItem ParseListItemStart(string rawLine)
    {
      int num = 0;
      int num2 = 0;
      bool flag;
      for (;;)
      {
        flag = num < rawLine.Length;
        if (flag)
        {
          char c = rawLine[num];
          bool flag2 = c == '\t' || c == ' ';
          flag = flag2;
        }
        if (!flag)
        {
          break;
        }
        num2 += ((rawLine[num] == '\t') ? 4 : 1);
        num++;
      }
      int num3 = num;
      string text = rawLine.Substring(num3, rawLine.Length - num3);
      string text2 = null;
      int num4 = text.IndexOfAny(new char[] { '.', ')' });
      flag = text.Length >= 2;
      if (flag)
      {
        bool flag2;
        switch (text[0])
        {
        case '*':
        case '+':
        case '-':
          flag2 = true;
          goto IL_00C3;
        }
        flag2 = false;
        IL_00C3:
        flag = flag2;
      }
      string text3;
      string text4;
      if (!flag || text[1] != ' ')
      {
        if (num4 >= 1 && num4 <= 3)
        {
          IEnumerable<char> enumerable = text.Substring(0, num4);
          Func<char, bool> func;
          if ((func = MarkdownViewerParser.<>O.<1>__IsNumber) == null)
          {
            func = (MarkdownViewerParser.<>O.<1>__IsNumber = new Func<char, bool>(char.IsNumber));
          }
          if (enumerable.All<char>(func) && num4 + 1 < text.Length && text[num4 + 1] == ' ')
          {
            text2 = text.Substring(0, num4) + ".";
            text3 = text;
            num3 = num4 + 2;
            text4 = text3.Substring(num3, text3.Length - num3);
            goto IL_0176;
          }
        }
        return null;
      }
      text3 = text;
      text4 = text3.Substring(2, text3.Length - 2);
      IL_0176:
      bool? flag3 = null;
      string text5 = text4.ToLowerInvariant();
      if (text5.StartsWith("[ ] ", StringComparison.Ordinal))
      {
        flag3 = new bool?(false);
        text3 = text4;
        text4 = text3.Substring(4, text3.Length - 4);
      }
      else if (text5.StartsWith("[x] ", StringComparison.Ordinal))
      {
        flag3 = new bool?(true);
        text3 = text4;
        text4 = text3.Substring(4, text3.Length - 4);
      }
      return new MarkdownViewerListItem(Math.Min(num2 / 2, 5), text2, flag3, MarkdownViewerParser.TrimWhitespace(text4));
    }

    // Token: 0x060019C3 RID: 6595 RVA: 0x003D4230 File Offset: 0x003D4230
    [GeneratedRegex("^!\\[([^\\]]*)\\]\\(\\s*(\\S+?)(?:\\s+\"[^\"]*\")?\\s*\\)$")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex StandaloneImageRegex()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__StandaloneImageRegex_18.Instance;
    }

    // Token: 0x060019C4 RID: 6596 RVA: 0x003D4238 File Offset: 0x003D4238
    [return: TupleElementNames(new string[] { "AlternateText", "Source" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    private static ValueTuple<string, string>? ParseStandaloneImage(string trimmedLine)
    {
      if (!trimmedLine.StartsWith("![", StringComparison.Ordinal) || !MarkdownViewerParser.StandaloneImageRegex().IsMatch(trimmedLine))
      {
        return null;
      }
      int num = trimmedLine.IndexOf(']');
      if (num < 0)
      {
        return null;
      }
      string text = trimmedLine.Substring(2, num - 2);
      int num2 = num + 2;
      string text2 = trimmedLine.Substring(num2, trimmedLine.Length - 1 - num2).Split(RuntimeHelpers.CreateSpan<char>(fieldof(<PrivateImplementationDetails>.A4BF0C9AFF52C60939B608E6491E797B1B1DD56F5BA1A4A9D8B34EDFD6E70E192).FieldHandle)).FirstOrDefault<string>((string s) => s.Length > 0) ?? "";
      return new ValueTuple<string, string>?(new ValueTuple<string, string>(text, text2));
    }

    // Token: 0x060019C5 RID: 6597 RVA: 0x003D42F4 File Offset: 0x003D42F4
    public static bool IsCallout(IReadOnlyList<MarkdownViewerBlock> childBlocks)
    {
      if (childBlocks.Count != 0)
      {
        MarkdownViewerBlock.Paragraph paragraph = childBlocks[0] as MarkdownViewerBlock.Paragraph;
        if (paragraph != null)
        {
          string head = new string(paragraph.InlineMarkdown.Take<char>(24).ToArray<char>()).ToLowerInvariant();
          return new string[] { "note", "tip", "important", "warning", "caution" }.Any<string>((string word) => head.StartsWith("**" + word + "** ·", StringComparison.Ordinal));
        }
      }
      return false;
    }

    // Token: 0x060019C6 RID: 6598 RVA: 0x003D4384 File Offset: 0x003D4384
    [GeneratedRegex("!\\[([^\\]]*)\\]\\(")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex InlineImageRegex()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__InlineImageRegex_19.Instance;
    }

    // Token: 0x060019C7 RID: 6599 RVA: 0x003D438C File Offset: 0x003D438C
    public static string RewriteInlineImagesAsLinks(string inlineMarkdown)
    {
      return MarkdownViewerParser.InlineImageRegex().Replace(inlineMarkdown, "[$1](");
    }

    // Token: 0x060019C8 RID: 6600 RVA: 0x003D43A0 File Offset: 0x003D43A0
    [CompilerGenerated]
    internal static void <ParseBlocks>g__FlushPendingParagraph|5_0(ref MarkdownViewerParser.<>c__DisplayClass5_0 A_0)
    {
      if (A_0.pendingParagraphLines.Count == 0)
      {
        return;
      }
      string text = string.Join(" ", A_0.pendingParagraphLines);
      A_0.pendingParagraphLines.Clear();
      A_0.blocks.Add(new MarkdownViewerBlock.Paragraph(text));
    }

    // Token: 0x04000AF7 RID: 2807
    private const char LineSeparator = '\u2028';

    // Token: 0x04000AF8 RID: 2808
    private const char ParagraphSeparator = '\u2029';

    // Token: 0x020005B3 RID: 1459
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x0400214D RID: 8525
      [Nullable(0)]
      public static Func<string, string> <0>__TrimWhitespace;

      // Token: 0x0400214E RID: 8526
      [Nullable(0)]
      public static Func<char, bool> <1>__IsNumber;
    }
  }
}
