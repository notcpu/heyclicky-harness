using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace HeyClicky.Core.Notch
{
  // Token: 0x02000106 RID: 262
  [NullableContext(1)]
  [Nullable(0)]
  public static class ChatReplyPreview
  {
    // Token: 0x17000401 RID: 1025
    // (get) Token: 0x0600112B RID: 4395 RVA: 0x0039A468 File Offset: 0x0039A468
    // (set) Token: 0x0600112C RID: 4396 RVA: 0x0039A470 File Offset: 0x0039A470
    public static Func<string, string> StripCitationMarkers { get; set; } = (string s) => s;

    // Token: 0x0600112D RID: 4397 RVA: 0x0039A478 File Offset: 0x0039A478
    [return: TupleElementNames(new string[] { "Chat", "HasWriteUp" })]
    [return: Nullable(new byte[] { 0, 2 })]
    public static ValueTuple<string, bool> Parts(string markdown)
    {
      bool flag = false;
      bool flag2 = false;
      List<string> list = new List<string>();
      foreach (string text in markdown.Split('\n', StringSplitOptions.None))
      {
        string text2 = text.Trim();
        if (text2.StartsWith("```", StringComparison.Ordinal))
        {
          flag = !flag;
        }
        if (!flag && text2.StartsWith('#') && text2.TrimStart('#').StartsWith(' '))
        {
          flag2 = true;
          break;
        }
        list.Add(text);
      }
      string text3 = string.Join("\n", list).Trim();
      return new ValueTuple<string, bool>((text3.Length == 0) ? null : text3, flag2);
    }

    // Token: 0x0600112E RID: 4398 RVA: 0x0039A520 File Offset: 0x0039A520
    public static string WithoutCitations(string markdown)
    {
      return ChatReplyPreview.CitationRegex.Replace(markdown, "");
    }

    // Token: 0x0600112F RID: 4399 RVA: 0x0039A534 File Offset: 0x0039A534
    [NullableContext(2)]
    public static string FullReplyMarkdown(ChatHandOff entry)
    {
      string text = ((entry != null) ? entry.FullReply : null) ?? ((entry != null) ? entry.ReplyText : null);
      if (text == null)
      {
        return null;
      }
      return ChatReplyPreview.ForDocumentViewer(ChatReplyPreview.StripCitationMarkers(text));
    }

    // Token: 0x06001130 RID: 4400 RVA: 0x0039A574 File Offset: 0x0039A574
    public static string ForDocumentViewer(string markdown)
    {
      List<string> list = new List<string>();
      List<string> list2 = null;
      foreach (string text in ChatReplyPreview.CitationsMovedToSources(markdown).Split('\n', StringSplitOptions.None))
      {
        string text2 = text.Trim();
        if (list2 != null)
        {
          if (text2.StartsWith("```", StringComparison.Ordinal))
          {
            list.AddRange(ChatReplyPreview.ChartAsTable(list2));
            list2 = null;
          }
          else
          {
            list2.Add(text2);
          }
        }
        else if (text2 == "```chart")
        {
          list2 = new List<string>();
        }
        else
        {
          if (text2.StartsWith('>'))
          {
            string text3 = ChatReplyPreview.CalloutKind(text2);
            if (text3 != null)
            {
              List<string> list3 = list;
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 2);
              defaultInterpolatedStringHandler.AppendLiteral("> **");
              defaultInterpolatedStringHandler.AppendFormatted<char>(char.ToUpperInvariant(text3[0]));
              string text4 = text3;
              defaultInterpolatedStringHandler.AppendFormatted(text4.Substring(1, text4.Length - 1));
              defaultInterpolatedStringHandler.AppendLiteral("** ·");
              list3.Add(defaultInterpolatedStringHandler.ToStringAndClear());
              goto IL_00F7;
            }
          }
          list.Add(text);
        }
        IL_00F7:;
      }
      if (list2 != null)
      {
        List<string> list4 = list;
        string text4 = "```chart";
        List<string> list5 = list2;
        int i = 0;
        string[] array = new string[1 + list5.Count];
        array[i] = text4;
        i++;
        Span<string> span = CollectionsMarshal.AsSpan<string>(list5);
        span.CopyTo(new Span<string>(array).Slice(i, span.Length));
        i += span.Length;
        list4.AddRange(new <>z__ReadOnlyArray<string>(array));
      }
      return string.Join("\n", list);
    }

    // Token: 0x06001131 RID: 4401 RVA: 0x0039A6F4 File Offset: 0x0039A6F4
    [return: Nullable(2)]
    private static string CalloutKind(string line)
    {
      string text = line.Substring(1, line.Length - 1).Trim().ToLowerInvariant();
      if (!text.StartsWith("[!", StringComparison.Ordinal) || !text.EndsWith(']'))
      {
        return null;
      }
      string text2 = text;
      string text3 = text2.Substring(2, text2.Length - 1 - 2);
      if (!ChatReplyPreview.CalloutKinds.Contains(text3))
      {
        return null;
      }
      return text3;
    }

    // Token: 0x06001132 RID: 4402 RVA: 0x0039A75C File Offset: 0x0039A75C
    private static List<string> ChartAsTable(List<string> lines)
    {
      string text = null;
      string text2 = null;
      List<string> list = new List<string>();
      string text5;
      int num2;
      foreach (string text3 in lines.Where<string>((string l) => l.Length > 0))
      {
        int num = text3.IndexOf(':');
        if (num >= 0)
        {
          string text4 = text3.Substring(0, num).Trim();
          text5 = text3;
          num2 = num + 1;
          string text6 = text5.Substring(num2, text5.Length - num2).Trim();
          text5 = text4.ToLowerInvariant();
          if (!(text5 == "title"))
          {
            if (!(text5 == "caption"))
            {
              if (!(text5 == "type"))
              {
                List<string> list2 = list;
                DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 2);
                defaultInterpolatedStringHandler.AppendLiteral("| ");
                defaultInterpolatedStringHandler.AppendFormatted(text4);
                defaultInterpolatedStringHandler.AppendLiteral(" | ");
                defaultInterpolatedStringHandler.AppendFormatted(text6);
                defaultInterpolatedStringHandler.AppendLiteral(" |");
                list2.Add(defaultInterpolatedStringHandler.ToStringAndClear());
              }
            }
            else
            {
              text2 = text6;
            }
          }
          else
          {
            text = text6;
          }
        }
      }
      if (list.Count == 0)
      {
        return new List<string>();
      }
      List<string> list3 = new List<string>();
      if (text != null)
      {
        list3.AddRange(new <>z__ReadOnlyArray<string>(new string[]
        {
          "**" + text + "**",
          ""
        }));
      }
      List<string> list4 = list3;
      text5 = "| | " + text2 + " |";
      string text7 = "|---|---|";
      List<string> list5 = list;
      num2 = 0;
      string[] array = new string[2 + list5.Count];
      array[num2] = text5;
      num2++;
      array[num2] = text7;
      num2++;
      Span<string> span = CollectionsMarshal.AsSpan<string>(list5);
      span.CopyTo(new Span<string>(array).Slice(num2, span.Length));
      num2 += span.Length;
      list4.AddRange(new <>z__ReadOnlyArray<string>(array));
      list3.Add("");
      return list3;
    }

    // Token: 0x06001133 RID: 4403 RVA: 0x0039A984 File Offset: 0x0039A984
    public static string CitationsMovedToSources(string markdown)
    {
      List<ValueTuple<string, string>> list = new List<ValueTuple<string, string>>();
      foreach (object obj in ChatReplyPreview.CitationRegex.Matches(markdown))
      {
        Match match = (Match)obj;
        string url = match.Groups[2].Value;
        if (!list.Any<ValueTuple<string, string>>(([TupleElementNames(new string[] { "Title", "Url" })] ValueTuple<string, string> c) => c.Item2 == url))
        {
          list.Add(new ValueTuple<string, string>(match.Groups[1].Value, url));
        }
      }
      if (list.Count == 0)
      {
        return markdown;
      }
      string text = ChatReplyPreview.CitationRegex.Replace(markdown, "");
      HashSet<string> seenTitles = new HashSet<string>();
      List<ValueTuple<string, string>> list2 = list.Where<ValueTuple<string, string>>(delegate([TupleElementNames(new string[] { "Title", "Url" })] ValueTuple<string, string> c)
      {
        string text2 = c.Item1.ToLowerInvariant();
        if (text.Contains(c.Item2, StringComparison.Ordinal) || text.ToLowerInvariant().Contains("[" + text2 + "]", StringComparison.Ordinal) || seenTitles.Contains(text2))
        {
          return false;
        }
        seenTitles.Add(text2);
        return true;
      }).ToList<ValueTuple<string, string>>();
      if (list2.Count == 0)
      {
        return text;
      }
      string text3 = string.Join("\n", list2.Select<ValueTuple<string, string>, string>(delegate([TupleElementNames(new string[] { "Title", "Url" })] ValueTuple<string, string> c)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 2);
        defaultInterpolatedStringHandler.AppendLiteral("- [");
        defaultInterpolatedStringHandler.AppendFormatted(c.Item1);
        defaultInterpolatedStringHandler.AppendLiteral("](");
        defaultInterpolatedStringHandler.AppendFormatted(c.Item2);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }));
      bool flag = Regex.IsMatch(text, "(?m)^#{2,6}\\s+Sources?\\s*$", RegexOptions.IgnoreCase);
      text = text.Trim();
      if (!flag)
      {
        return text + "\n\n## Sources\n\n" + text3;
      }
      return text + "\n" + text3;
    }

    // Token: 0x06001134 RID: 4404 RVA: 0x0039AB14 File Offset: 0x0039AB14
    [return: TupleElementNames(new string[] { "Text", "IsTruncated" })]
    [return: Nullable(new byte[] { 0, 1 })]
    public static ValueTuple<string, bool> Preview(string markdown)
    {
      List<string> list = new List<string>();
      int num = 0;
      bool flag = false;
      foreach (string text in markdown.Split('\n', StringSplitOptions.None))
      {
        string text2 = text.Trim();
        int num2 = ((text2.Length == 0) ? ((flag > false) ? 1 : 0) : Math.Max(1, (int)Math.Ceiling((double)text2.Length / 52.0)));
        if (num + num2 > 10 && list.Count > 0)
        {
          if (flag)
          {
            list.Add("```");
          }
          while (list.Count > 0)
          {
            List<string> list2 = list;
            if (list2[list2.Count - 1].Trim().Length != 0)
            {
              break;
            }
            list.RemoveAt(list.Count - 1);
          }
          return new ValueTuple<string, bool>(string.Join("\n", list), true);
        }
        num += num2;
        list.Add(text);
        if (text2.StartsWith("```", StringComparison.Ordinal))
        {
          flag = !flag;
        }
      }
      return new ValueTuple<string, bool>(markdown, false);
    }

    // Token: 0x06001135 RID: 4405 RVA: 0x0039AC18 File Offset: 0x0039AC18
    public static string BubbleText(string markdown)
    {
      List<string> list = new List<string>();
      foreach (string text in markdown.Split('\n', StringSplitOptions.None))
      {
        ChatReplyPreview.<>c__DisplayClass17_0 CS$<>8__locals1;
        CS$<>8__locals1.line = text;
        string text2 = CS$<>8__locals1.line.Trim();
        if (!text2.StartsWith("```", StringComparison.Ordinal))
        {
          if (text2.StartsWith('#'))
          {
            CS$<>8__locals1.line = text2.TrimStart('#').Trim();
            if (CS$<>8__locals1.line.Length > 0)
            {
              CS$<>8__locals1.line = "**" + CS$<>8__locals1.line + "**";
            }
          }
          else if (text2.StartsWith('|'))
          {
            List<string> list2 = (from c in text2.Split('|', StringSplitOptions.RemoveEmptyEntries)
              select c.Trim()).ToList<string>();
            if (list2.All<string>((string c) => c.All<char>((char ch) => "-: ".Contains(ch))))
            {
              goto IL_020D;
            }
            CS$<>8__locals1.line = string.Join(" · ", list2.Where<string>((string c) => c.Length > 0));
          }
          else if (text2.StartsWith("- [ ] ", StringComparison.Ordinal) || text2.StartsWith("- [x] ", StringComparison.Ordinal) || text2.StartsWith("- [X] ", StringComparison.Ordinal))
          {
            string text3 = ChatReplyPreview.<BubbleText>g__Indent|17_0(ref CS$<>8__locals1);
            string text4 = (text2.StartsWith("- [ ] ", StringComparison.Ordinal) ? "○ " : "✓ ");
            string text5 = text2;
            CS$<>8__locals1.line = text3 + text4 + text5.Substring(6, text5.Length - 6);
          }
          else if (text2.StartsWith("- ", StringComparison.Ordinal) || text2.StartsWith("* ", StringComparison.Ordinal))
          {
            string text6 = ChatReplyPreview.<BubbleText>g__Indent|17_0(ref CS$<>8__locals1);
            string text7 = "• ";
            string text5 = text2;
            CS$<>8__locals1.line = text6 + text7 + text5.Substring(2, text5.Length - 2);
          }
          list.Add(CS$<>8__locals1.line);
        }
        IL_020D:;
      }
      List<string> list3 = new List<string>();
      foreach (string text8 in list)
      {
        if (text8.Trim().Length == 0)
        {
          if (list3.Count == 0)
          {
            continue;
          }
          List<string> list4 = list3;
          if (list4[list4.Count - 1].Trim().Length == 0)
          {
            continue;
          }
        }
        list3.Add(text8);
      }
      return string.Join("\n", list3).Trim();
    }

    // Token: 0x06001136 RID: 4406 RVA: 0x0039AECC File Offset: 0x0039AECC
    [NullableContext(2)]
    public static bool UsesWideLayout(string markdown)
    {
      if (markdown == null)
      {
        return false;
      }
      if (markdown.Length > 1200)
      {
        return true;
      }
      if (markdown.Contains("```") || markdown.Contains("~~~") || markdown.Contains("!["))
      {
        return true;
      }
      return markdown.Split('\n', StringSplitOptions.RemoveEmptyEntries).Any<string>(delegate(string line)
      {
        string text = line.Trim();
        return text.StartsWith('|') && text.Contains("---");
      });
    }

    // Token: 0x06001138 RID: 4408 RVA: 0x0039AFAC File Offset: 0x0039AFAC
    [CompilerGenerated]
    internal static string <BubbleText>g__Indent|17_0(ref ChatReplyPreview.<>c__DisplayClass17_0 A_0)
    {
      return new string(' ', A_0.line.Length - A_0.line.TrimStart(' ').Length);
    }

    // Token: 0x04000772 RID: 1906
    public const int MaximumLines = 10;

    // Token: 0x04000773 RID: 1907
    public const int CharactersPerLine = 52;

    // Token: 0x04000775 RID: 1909
    private const string CitationPattern = "\\s?\\(\\[([^\\]]{1,80})\\]\\((https?://[^)\\s]+)\\)\\)";

    // Token: 0x04000776 RID: 1910
    private static readonly Regex CitationRegex = new Regex("\\s?\\(\\[([^\\]]{1,80})\\]\\((https?://[^)\\s]+)\\)\\)", RegexOptions.Compiled);

    // Token: 0x04000777 RID: 1911
    private static readonly string[] CalloutKinds = new string[] { "note", "tip", "important", "warning", "caution" };
  }
}
