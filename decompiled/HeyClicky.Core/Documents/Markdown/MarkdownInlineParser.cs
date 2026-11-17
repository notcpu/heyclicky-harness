using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace HeyClicky.Core.Documents.Markdown
{
  // Token: 0x020001B9 RID: 441
  [NullableContext(1)]
  [Nullable(0)]
  public static class MarkdownInlineParser
  {
    // Token: 0x0600198E RID: 6542 RVA: 0x003D2438 File Offset: 0x003D2438
    public static IReadOnlyList<MarkdownInlineRun> Parse(string markdown)
    {
      List<MarkdownInlineParser.Node> list = MarkdownInlineParser.ParseNodes(markdown);
      List<MarkdownInlineRun> list2 = new List<MarkdownInlineRun>();
      MarkdownInlineParser.Flatten(list, false, false, false, null, list2);
      List<MarkdownInlineRun> list3 = new List<MarkdownInlineRun>();
      foreach (MarkdownInlineRun markdownInlineRun in list2)
      {
        if (markdownInlineRun.Text.Length != 0)
        {
          if (list3.Count > 0)
          {
            List<MarkdownInlineRun> list4 = list3;
            MarkdownInlineRun markdownInlineRun2 = list4[list4.Count - 1];
            MarkdownInlineRun markdownInlineRun3 = markdownInlineRun2.<Clone>$();
            markdownInlineRun3.Text = "";
            MarkdownInlineRun markdownInlineRun4 = markdownInlineRun.<Clone>$();
            markdownInlineRun4.Text = "";
            if (markdownInlineRun3 == markdownInlineRun4)
            {
              List<MarkdownInlineRun> list5 = list3;
              int num = list5.Count - 1;
              MarkdownInlineRun markdownInlineRun5 = markdownInlineRun2.<Clone>$();
              markdownInlineRun5.Text = markdownInlineRun2.Text + markdownInlineRun.Text;
              list5[num] = markdownInlineRun5;
              continue;
            }
          }
          list3.Add(markdownInlineRun);
        }
      }
      return list3;
    }

    // Token: 0x0600198F RID: 6543 RVA: 0x003D2528 File Offset: 0x003D2528
    public static string PlainText(string markdown)
    {
      return string.Concat(from r in MarkdownInlineParser.Parse(markdown)
        select r.Text);
    }

    // Token: 0x06001990 RID: 6544 RVA: 0x003D255C File Offset: 0x003D255C
    private static void Flatten(List<MarkdownInlineParser.Node> nodes, bool strong, bool emphasis, bool strike, [Nullable(2)] string link, List<MarkdownInlineRun> runs)
    {
      foreach (MarkdownInlineParser.Node node in nodes)
      {
        MarkdownInlineParser.TextNode textNode = node as MarkdownInlineParser.TextNode;
        if (textNode == null)
        {
          MarkdownInlineParser.CodeNode codeNode = node as MarkdownInlineParser.CodeNode;
          if (codeNode == null)
          {
            MarkdownInlineParser.StyleNode styleNode = node as MarkdownInlineParser.StyleNode;
            if (styleNode == null)
            {
              MarkdownInlineParser.LinkNode linkNode = node as MarkdownInlineParser.LinkNode;
              if (linkNode != null)
              {
                MarkdownInlineParser.Flatten(linkNode.Children, strong, emphasis, strike, linkNode.Url, runs);
              }
            }
            else
            {
              MarkdownInlineParser.Flatten(styleNode.Children, strong || styleNode.Kind == 'S', emphasis || styleNode.Kind == 'E', strike || styleNode.Kind == '~', link, runs);
            }
          }
          else
          {
            runs.Add(new MarkdownInlineRun(codeNode.Text, strong, emphasis, true, strike, link));
          }
        }
        else
        {
          runs.Add(new MarkdownInlineRun(textNode.Text.ToString(), strong, emphasis, false, strike, link));
        }
      }
    }

    // Token: 0x06001991 RID: 6545 RVA: 0x003D2674 File Offset: 0x003D2674
    private static bool IsAsciiPunctuation(char c)
    {
      if (c >= '[')
      {
        if (c >= '{')
        {
          if (c > '~')
          {
            goto IL_0032;
          }
        }
        else if (c > '`')
        {
          goto IL_0032;
        }
      }
      else if (c >= ':')
      {
        if (c > '@')
        {
          goto IL_0032;
        }
      }
      else if (c < '!' || c > '/')
      {
        goto IL_0032;
      }
      return true;
      IL_0032:
      return false;
    }

    // Token: 0x06001992 RID: 6546 RVA: 0x003D26B8 File Offset: 0x003D26B8
    private static bool IsPunctuation(char c)
    {
      bool flag = MarkdownInlineParser.IsAsciiPunctuation(c);
      if (!flag)
      {
        UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
        bool flag2 = unicodeCategory - UnicodeCategory.ConnectorPunctuation <= 10;
        flag = flag2;
      }
      return flag;
    }

    // Token: 0x06001993 RID: 6547 RVA: 0x003D26E8 File Offset: 0x003D26E8
    private unsafe static List<MarkdownInlineParser.Node> ParseNodes(string s)
    {
      MarkdownInlineParser.<>c__DisplayClass12_0 CS$<>8__locals1;
      CS$<>8__locals1.nodes = new List<MarkdownInlineParser.Node>();
      CS$<>8__locals1.delimiters = new List<MarkdownInlineParser.Delimiter>();
      CS$<>8__locals1.brackets = new List<MarkdownInlineParser.Bracket>();
      CS$<>8__locals1.text = new StringBuilder();
      int i = 0;
      while (i < s.Length)
      {
        char c = s[i];
        string text4;
        int num4;
        string text5;
        int num5;
        if (c == '\\' && i + 1 < s.Length && MarkdownInlineParser.IsAsciiPunctuation(s[i + 1]))
        {
          CS$<>8__locals1.text.Append(s[i + 1]);
          i += 2;
        }
        else if (c == '`')
        {
          int num = 0;
          while (i + num < s.Length && s[i + num] == '`')
          {
            num++;
          }
          int num2 = MarkdownInlineParser.FindBacktickRun(s, i + num, num);
          if (num2 < 0)
          {
            CS$<>8__locals1.text.Append('`', num);
            i += num;
          }
          else
          {
            int num3 = i + num;
            string text = s.Substring(num3, num2 - num3).Replace('\n', ' ');
            if (text.Length >= 2 && text[0] == ' ')
            {
              string text2 = text;
              if (text2[text2.Length - 1] == ' ')
              {
                if (text.Any<char>((char ch) => ch != ' '))
                {
                  string text3 = text;
                  text = text3.Substring(1, text3.Length - 1 - 1);
                }
              }
            }
            MarkdownInlineParser.<ParseNodes>g__FlushText|12_0(ref CS$<>8__locals1);
            CS$<>8__locals1.nodes.Add(new MarkdownInlineParser.CodeNode(text));
            i = num2 + num;
          }
        }
        else if (c == '&' && MarkdownInlineParser.TryEntity(s, i, out text4, out num4))
        {
          CS$<>8__locals1.text.Append(text4);
          i += num4;
        }
        else if (c == '<' && MarkdownInlineParser.TryAutolink(s, i, out text5, out num5))
        {
          MarkdownInlineParser.<ParseNodes>g__FlushText|12_0(ref CS$<>8__locals1);
          string text6;
          if (!text5.StartsWith("mailto:", StringComparison.OrdinalIgnoreCase) || s.AsSpan(i + 1).StartsWith("mailto:", StringComparison.OrdinalIgnoreCase))
          {
            text6 = text5;
          }
          else
          {
            string text3 = text5;
            text6 = text3.Substring(7, text3.Length - 7);
          }
          string text7 = text6;
          List<MarkdownInlineParser.Node> nodes = CS$<>8__locals1.nodes;
          string text8 = text5;
          int num3 = 1;
          List<MarkdownInlineParser.Node> list = new List<MarkdownInlineParser.Node>(num3);
          CollectionsMarshal.SetCount<MarkdownInlineParser.Node>(list, num3);
          Span<MarkdownInlineParser.Node> span = CollectionsMarshal.AsSpan<MarkdownInlineParser.Node>(list);
          int num6 = 0;
          *span[num6] = new MarkdownInlineParser.TextNode(text7);
          nodes.Add(new MarkdownInlineParser.LinkNode(text8, list));
          i += num5;
        }
        else
        {
          bool flag = c == '*' || c == '_' || c == '~';
          if (flag)
          {
            int num7 = 0;
            while (i + num7 < s.Length && s[i + num7] == c)
            {
              num7++;
            }
            if (c == '~' && num7 > 2)
            {
              CS$<>8__locals1.text.Append(c, num7);
              i += num7;
            }
            else
            {
              char c2 = ((i > 0) ? s[i - 1] : ' ');
              char c3 = ((i + num7 < s.Length) ? s[i + num7] : ' ');
              bool flag2 = char.IsWhiteSpace(c3);
              bool flag3 = char.IsWhiteSpace(c2);
              bool flag4 = !flag2 && (!MarkdownInlineParser.IsPunctuation(c3) || flag3 || MarkdownInlineParser.IsPunctuation(c2));
              bool flag5 = !flag3 && (!MarkdownInlineParser.IsPunctuation(c2) || flag2 || MarkdownInlineParser.IsPunctuation(c3));
              bool flag6;
              bool flag7;
              if (c == '_')
              {
                flag6 = flag4 && (!flag5 || MarkdownInlineParser.IsPunctuation(c2));
                flag7 = flag5 && (!flag4 || MarkdownInlineParser.IsPunctuation(c3));
              }
              else
              {
                flag6 = flag4;
                flag7 = flag5;
              }
              MarkdownInlineParser.<ParseNodes>g__FlushText|12_0(ref CS$<>8__locals1);
              MarkdownInlineParser.TextNode textNode = new MarkdownInlineParser.TextNode(new string(c, num7));
              CS$<>8__locals1.nodes.Add(textNode);
              CS$<>8__locals1.delimiters.Add(new MarkdownInlineParser.Delimiter(c, num7, flag6, flag7, textNode));
              i += num7;
            }
          }
          else if (c == '[')
          {
            MarkdownInlineParser.<ParseNodes>g__FlushText|12_0(ref CS$<>8__locals1);
            MarkdownInlineParser.TextNode textNode2 = new MarkdownInlineParser.TextNode("[");
            CS$<>8__locals1.nodes.Add(textNode2);
            CS$<>8__locals1.brackets.Add(new MarkdownInlineParser.Bracket(CS$<>8__locals1.nodes.Count - 1, CS$<>8__locals1.delimiters.Count, textNode2));
            i++;
          }
          else if (c == ']')
          {
            MarkdownInlineParser.Bracket bracket;
            if (CS$<>8__locals1.brackets.Count <= 0)
            {
              bracket = null;
            }
            else
            {
              List<MarkdownInlineParser.Bracket> brackets = CS$<>8__locals1.brackets;
              bracket = brackets[brackets.Count - 1];
            }
            MarkdownInlineParser.Bracket bracket2 = bracket;
            string text9;
            int num8;
            if (bracket2 == null)
            {
              CS$<>8__locals1.text.Append(']');
              i++;
            }
            else if (!bracket2.Active)
            {
              CS$<>8__locals1.brackets.RemoveAt(CS$<>8__locals1.brackets.Count - 1);
              CS$<>8__locals1.text.Append(']');
              i++;
            }
            else if (MarkdownInlineParser.TryLinkTail(s, i + 1, out text9, out num8))
            {
              MarkdownInlineParser.<ParseNodes>g__FlushText|12_0(ref CS$<>8__locals1);
              List<MarkdownInlineParser.Node> range = CS$<>8__locals1.nodes.GetRange(bracket2.NodeIndex + 1, CS$<>8__locals1.nodes.Count - bracket2.NodeIndex - 1);
              List<MarkdownInlineParser.Delimiter> range2 = CS$<>8__locals1.delimiters.GetRange(bracket2.DelimiterIndex, CS$<>8__locals1.delimiters.Count - bracket2.DelimiterIndex);
              MarkdownInlineParser.ProcessEmphasis(range, range2);
              CS$<>8__locals1.nodes.RemoveRange(bracket2.NodeIndex, CS$<>8__locals1.nodes.Count - bracket2.NodeIndex);
              CS$<>8__locals1.delimiters.RemoveRange(bracket2.DelimiterIndex, CS$<>8__locals1.delimiters.Count - bracket2.DelimiterIndex);
              CS$<>8__locals1.nodes.Add(new MarkdownInlineParser.LinkNode(text9, range));
              CS$<>8__locals1.brackets.RemoveAt(CS$<>8__locals1.brackets.Count - 1);
              foreach (MarkdownInlineParser.Bracket bracket3 in CS$<>8__locals1.brackets)
              {
                bracket3.Active = false;
              }
              i += 1 + num8;
            }
            else
            {
              CS$<>8__locals1.brackets.RemoveAt(CS$<>8__locals1.brackets.Count - 1);
              CS$<>8__locals1.text.Append(']');
              i++;
            }
          }
          else
          {
            CS$<>8__locals1.text.Append(c);
            i++;
          }
        }
      }
      MarkdownInlineParser.<ParseNodes>g__FlushText|12_0(ref CS$<>8__locals1);
      MarkdownInlineParser.ProcessEmphasis(CS$<>8__locals1.nodes, CS$<>8__locals1.delimiters);
      return CS$<>8__locals1.nodes;
    }

    // Token: 0x06001994 RID: 6548 RVA: 0x003D2D34 File Offset: 0x003D2D34
    private static int FindBacktickRun(string s, int from, int length)
    {
      int i = from;
      while (i < s.Length)
      {
        if (s[i] != '`')
        {
          i++;
        }
        else
        {
          int num = 0;
          while (i + num < s.Length && s[i + num] == '`')
          {
            num++;
          }
          if (num == length)
          {
            return i;
          }
          i += num;
        }
      }
      return -1;
    }

    // Token: 0x06001995 RID: 6549 RVA: 0x003D2D8C File Offset: 0x003D2D8C
    private static bool TryEntity(string s, int i, out string decoded, out int length)
    {
      decoded = "";
      length = 0;
      int num = s.IndexOf(';', i);
      if (num < 0 || num - i > 32)
      {
        return false;
      }
      string text = s.Substring(i, num + 1 - i);
      string text2 = WebUtility.HtmlDecode(text);
      if (text2 == text)
      {
        return false;
      }
      decoded = text2;
      length = text.Length;
      return true;
    }

    // Token: 0x06001996 RID: 6550 RVA: 0x003D2DE8 File Offset: 0x003D2DE8
    private static bool TryAutolink(string s, int i, out string url, out int length)
    {
      url = "";
      length = 0;
      int num = s.IndexOf('>', i + 1);
      if (num < 0)
      {
        return false;
      }
      int num2 = i + 1;
      string text = s.Substring(num2, num - num2);
      if (text.Length != 0)
      {
        if (!text.Any<char>((char ch) => char.IsWhiteSpace(ch) || ch == '<'))
        {
          int num3 = text.IndexOf(':');
          object obj;
          if (num3 >= 2 && num3 <= 32 && char.IsAsciiLetter(text[0]))
          {
            obj = text.Substring(0, num3).All<char>(delegate(char ch)
            {
              bool flag2 = char.IsAsciiLetterOrDigit(ch);
              if (!flag2)
              {
                bool flag3;
                switch (ch)
                {
                case '+':
                case '-':
                case '.':
                  flag3 = true;
                  goto IL_002B;
                }
                flag3 = false;
                IL_002B:
                flag2 = flag3;
              }
              return flag2;
            });
          }
          else
          {
            obj = 0;
          }
          object obj2 = obj;
          bool flag = obj2 == null && Regex.IsMatch(text, "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
          if (obj2 == null && !flag)
          {
            return false;
          }
          url = (flag ? ("mailto:" + text) : text);
          length = num - i + 1;
          return true;
        }
      }
      return false;
    }

    // Token: 0x06001997 RID: 6551 RVA: 0x003D2ED8 File Offset: 0x003D2ED8
    private static bool TryLinkTail(string s, int i, out string destination, out int length)
    {
      destination = "";
      length = 0;
      if (i >= s.Length || s[i] != '(')
      {
        return false;
      }
      int num = i + 1;
      while (num < s.Length && char.IsWhiteSpace(s[num]))
      {
        num++;
      }
      StringBuilder stringBuilder = new StringBuilder();
      if (num < s.Length && s[num] == '<')
      {
        num++;
        while (num < s.Length && s[num] != '>' && s[num] != '\n')
        {
          if (s[num] == '\\' && num + 1 < s.Length && MarkdownInlineParser.IsAsciiPunctuation(s[num + 1]))
          {
            stringBuilder.Append(s[num + 1]);
            num += 2;
          }
          else
          {
            if (s[num] == '<')
            {
              return false;
            }
            stringBuilder.Append(s[num++]);
          }
        }
        if (num >= s.Length || s[num] != '>')
        {
          return false;
        }
        num++;
      }
      else
      {
        int num2 = 0;
        while (num < s.Length && !char.IsWhiteSpace(s[num]) && !char.IsControl(s[num]))
        {
          if (s[num] == '\\' && num + 1 < s.Length && MarkdownInlineParser.IsAsciiPunctuation(s[num + 1]))
          {
            stringBuilder.Append(s[num + 1]);
            num += 2;
          }
          else
          {
            if (s[num] == '(')
            {
              num2++;
            }
            else if (s[num] == ')')
            {
              if (num2 == 0)
              {
                break;
              }
              num2--;
            }
            stringBuilder.Append(s[num++]);
          }
        }
        if (num2 != 0)
        {
          return false;
        }
      }
      int num3 = num;
      while (num < s.Length && char.IsWhiteSpace(s[num]))
      {
        num++;
      }
      bool flag = num < s.Length && num > num3;
      if (flag)
      {
        char c = s[num];
        bool flag2 = c == '"' || c == '\'' || c == '(';
        flag = flag2;
      }
      if (flag)
      {
        char c2 = ((s[num] == '(') ? ')' : s[num]);
        num++;
        while (num < s.Length && s[num] != c2)
        {
          if (s[num] == '\\' && num + 1 < s.Length)
          {
            num++;
          }
          num++;
        }
        if (num >= s.Length)
        {
          return false;
        }
        num++;
        while (num < s.Length && char.IsWhiteSpace(s[num]))
        {
          num++;
        }
      }
      if (num >= s.Length || s[num] != ')')
      {
        return false;
      }
      destination = stringBuilder.ToString();
      length = num - i + 1;
      return true;
    }

    // Token: 0x06001998 RID: 6552 RVA: 0x003D318C File Offset: 0x003D318C
    private static void ProcessEmphasis(List<MarkdownInlineParser.Node> nodes, List<MarkdownInlineParser.Delimiter> delimiters)
    {
      int i = 0;
      while (i < delimiters.Count)
      {
        MarkdownInlineParser.Delimiter delimiter = delimiters[i];
        if (!delimiter.CanClose || delimiter.Count == 0)
        {
          i++;
        }
        else
        {
          int num = -1;
          for (int j = i - 1; j >= 0; j--)
          {
            MarkdownInlineParser.Delimiter delimiter2 = delimiters[j];
            if (delimiter2.Char == delimiter.Char && delimiter2.CanOpen && delimiter2.Count != 0)
            {
              if (delimiter.Char == '~')
              {
                if (delimiter2.Count != delimiter.Count)
                {
                  goto IL_00B7;
                }
              }
              else if ((delimiter2.CanClose || delimiter.CanOpen) && (delimiter2.Original + delimiter.Original) % 3 == 0 && (delimiter2.Original % 3 != 0 || delimiter.Original % 3 != 0))
              {
                goto IL_00B7;
              }
              num = j;
              break;
            }
            IL_00B7:;
          }
          if (num < 0)
          {
            i++;
          }
          else
          {
            MarkdownInlineParser.Delimiter delimiter3 = delimiters[num];
            int num2 = ((delimiter.Char == '~') ? delimiter.Count : ((delimiter.Count >= 2 && delimiter3.Count >= 2) ? 2 : 1));
            char c = ((delimiter.Char == '~') ? '~' : ((num2 == 2) ? 'S' : 'E'));
            int num3 = nodes.IndexOf(delimiter3.Node);
            int num4 = nodes.IndexOf(delimiter.Node);
            List<MarkdownInlineParser.Node> range = nodes.GetRange(num3 + 1, num4 - num3 - 1);
            nodes.RemoveRange(num3 + 1, num4 - num3 - 1);
            nodes.Insert(num3 + 1, new MarkdownInlineParser.StyleNode(c, range));
            delimiters.RemoveRange(num + 1, i - num - 1);
            i = num + 1;
            delimiter3.Count -= num2;
            delimiter.Count -= num2;
            delimiter3.Node.Text.Length = delimiter3.Count;
            delimiter.Node.Text.Length = delimiter.Count;
            if (delimiter3.Count == 0)
            {
              nodes.Remove(delimiter3.Node);
              delimiters.RemoveAt(num);
              i--;
            }
            if (delimiter.Count == 0)
            {
              nodes.Remove(delimiter.Node);
              delimiters.RemoveAt(i);
            }
          }
        }
      }
    }

    // Token: 0x06001999 RID: 6553 RVA: 0x003D33B0 File Offset: 0x003D33B0
    [CompilerGenerated]
    internal static void <ParseNodes>g__FlushText|12_0(ref MarkdownInlineParser.<>c__DisplayClass12_0 A_0)
    {
      if (A_0.text.Length == 0)
      {
        return;
      }
      if (A_0.nodes.Count > 0)
      {
        List<MarkdownInlineParser.Node> nodes = A_0.nodes;
        MarkdownInlineParser.Node node = nodes[nodes.Count - 1];
        MarkdownInlineParser.TextNode last = node as MarkdownInlineParser.TextNode;
        if (last != null && !A_0.delimiters.Any<MarkdownInlineParser.Delimiter>((MarkdownInlineParser.Delimiter d) => d.Node == last) && !A_0.brackets.Any<MarkdownInlineParser.Bracket>((MarkdownInlineParser.Bracket b) => b.Node == last))
        {
          last.Text.Append(A_0.text);
          goto IL_00B0;
        }
      }
      A_0.nodes.Add(new MarkdownInlineParser.TextNode(A_0.text.ToString()));
      IL_00B0:
      A_0.text.Clear();
    }

    // Token: 0x020005A1 RID: 1441
    [NullableContext(0)]
    private abstract class Node
    {
    }

    // Token: 0x020005A2 RID: 1442
    [Nullable(0)]
    private sealed class TextNode : MarkdownInlineParser.Node
    {
      // Token: 0x06003616 RID: 13846 RVA: 0x00443574 File Offset: 0x00443574
      public TextNode(string text)
      {
      }

      // Token: 0x04002127 RID: 8487
      public StringBuilder Text = new StringBuilder(text);
    }

    // Token: 0x020005A3 RID: 1443
    [Nullable(0)]
    private sealed class CodeNode : MarkdownInlineParser.Node
    {
      // Token: 0x06003617 RID: 13847 RVA: 0x00443588 File Offset: 0x00443588
      public CodeNode(string text)
      {
      }

      // Token: 0x04002128 RID: 8488
      public string Text = text;
    }

    // Token: 0x020005A4 RID: 1444
    [Nullable(0)]
    private sealed class StyleNode : MarkdownInlineParser.Node
    {
      // Token: 0x06003618 RID: 13848 RVA: 0x00443598 File Offset: 0x00443598
      public StyleNode(char kind, List<MarkdownInlineParser.Node> children)
      {
      }

      // Token: 0x04002129 RID: 8489
      public char Kind = kind;

      // Token: 0x0400212A RID: 8490
      public List<MarkdownInlineParser.Node> Children = children;
    }

    // Token: 0x020005A5 RID: 1445
    [Nullable(0)]
    private sealed class LinkNode : MarkdownInlineParser.Node
    {
      // Token: 0x06003619 RID: 13849 RVA: 0x004435B0 File Offset: 0x004435B0
      public LinkNode(string url, List<MarkdownInlineParser.Node> children)
      {
      }

      // Token: 0x0400212B RID: 8491
      public string Url = url;

      // Token: 0x0400212C RID: 8492
      public List<MarkdownInlineParser.Node> Children = children;
    }

    // Token: 0x020005A6 RID: 1446
    [Nullable(0)]
    private sealed class Delimiter
    {
      // Token: 0x0600361A RID: 13850 RVA: 0x004435C8 File Offset: 0x004435C8
      public Delimiter(char ch, int count, bool canOpen, bool canClose, MarkdownInlineParser.TextNode node)
      {
      }

      // Token: 0x0400212D RID: 8493
      public char Char = ch;

      // Token: 0x0400212E RID: 8494
      public int Count = count;

      // Token: 0x0400212F RID: 8495
      public int Original = count;

      // Token: 0x04002130 RID: 8496
      public bool CanOpen = canOpen;

      // Token: 0x04002131 RID: 8497
      public bool CanClose = canClose;

      // Token: 0x04002132 RID: 8498
      public bool Active = true;

      // Token: 0x04002133 RID: 8499
      public MarkdownInlineParser.TextNode Node = node;
    }

    // Token: 0x020005A7 RID: 1447
    [Nullable(0)]
    private sealed class Bracket
    {
      // Token: 0x0600361B RID: 13851 RVA: 0x00443604 File Offset: 0x00443604
      public Bracket(int nodeIndex, int delimiterIndex, MarkdownInlineParser.TextNode node)
      {
      }

      // Token: 0x04002134 RID: 8500
      public int NodeIndex = nodeIndex;

      // Token: 0x04002135 RID: 8501
      public int DelimiterIndex = delimiterIndex;

      // Token: 0x04002136 RID: 8502
      public MarkdownInlineParser.TextNode Node = node;

      // Token: 0x04002137 RID: 8503
      public bool Active = true;
    }
  }
}
