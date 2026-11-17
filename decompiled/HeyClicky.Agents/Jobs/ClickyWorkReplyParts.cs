using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000032 RID: 50
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyWorkReplyParts
  {
    // Token: 0x06000519 RID: 1305 RVA: 0x000EF4D4 File Offset: 0x000EF4D4
    [return: TupleElementNames(new string[] { "Text", "Title", "Summary", "ArtifactPaths" })]
    [return: Nullable(new byte[] { 0, 1, 2, 2, 1, 1 })]
    public static ValueTuple<string, string, string, IReadOnlyList<string>> Parse(string rawReply)
    {
      string text = rawReply;
      string text2 = ClickyWorkReplyParts.FirstBlock("DONE_TITLE", text);
      string text3;
      if ((text3 = ClickyWorkReplyParts.FirstBlock("SUMMARY", text)) == null)
      {
        ClickyAnswer clickyAnswer = ClickyAnswer.Parse(ClickyWorkReplyParts.FirstBlock("ANSWER", text));
        text3 = ((clickyAnswer != null) ? clickyAnswer.Line : null);
      }
      string text4 = text3;
      IEnumerable<string> enumerable = from l in (ClickyWorkReplyParts.FirstBlock("ARTIFACTS", text) ?? "").Split(RuntimeHelpers.CreateSpan<char>(fieldof(<PrivateImplementationDetails>.FBB68C637180E81FFB4AF91596B53064C159DDF8F56E330BDD31C0DE6472348D2).FieldHandle))
        select l.Trim().Trim(new char[] { '-', '*', '•', ' ', '\t' }) into l
        where l.StartsWith('/') || l.StartsWith('~') || l.StartsWith("http", StringComparison.Ordinal) || ClickyWorkReplyParts.IsWindowsPath(l)
        select l;
      Func<string, string> func;
      if ((func = ClickyWorkReplyParts.<>O.<0>__ExpandTilde) == null)
      {
        func = (ClickyWorkReplyParts.<>O.<0>__ExpandTilde = new Func<string, string>(ClickyWorkReplyParts.ExpandTilde));
      }
      List<string> list = enumerable.Select<string, string>(func).ToList<string>();
      foreach (string text5 in ClickyWorkReplyParts.MetadataBlocks)
      {
        string text6 = text;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 2);
        defaultInterpolatedStringHandler.AppendLiteral("(?s)<");
        defaultInterpolatedStringHandler.AppendFormatted(text5);
        defaultInterpolatedStringHandler.AppendLiteral(">.*?</");
        defaultInterpolatedStringHandler.AppendFormatted(text5);
        defaultInterpolatedStringHandler.AppendLiteral(">");
        text = Regex.Replace(text6, defaultInterpolatedStringHandler.ToStringAndClear(), "");
      }
      return new ValueTuple<string, string, string, IReadOnlyList<string>>(text.Trim(), ClickyWorkReplyParts.NilIfEmpty((text2 != null) ? text2.Trim() : null), ClickyWorkReplyParts.NilIfEmpty((text4 != null) ? text4.Trim() : null), list);
    }

    // Token: 0x0600051A RID: 1306 RVA: 0x000EF650 File Offset: 0x000EF650
    private static bool IsWindowsPath(string line)
    {
      bool flag = line.Length > 2 && char.IsLetter(line[0]) && line[1] == ':';
      if (flag)
      {
        char c = line[2];
        bool flag2 = c == '/' || c == '\\';
        flag = flag2;
      }
      return flag || line.StartsWith("\\\\", StringComparison.Ordinal);
    }

    // Token: 0x0600051B RID: 1307 RVA: 0x000EF6B4 File Offset: 0x000EF6B4
    private static string ExpandTilde(string path)
    {
      if (!path.StartsWith('~'))
      {
        return path;
      }
      return Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), path.Substring(1, path.Length - 1).TrimStart(new char[] { '/', '\\' }));
    }

    // Token: 0x0600051C RID: 1308 RVA: 0x000EF700 File Offset: 0x000EF700
    [NullableContext(2)]
    private static string NilIfEmpty(string s)
    {
      if (!string.IsNullOrEmpty(s))
      {
        return s;
      }
      return null;
    }

    // Token: 0x0600051D RID: 1309 RVA: 0x000EF710 File Offset: 0x000EF710
    [return: Nullable(2)]
    private static string FirstBlock(string name, string text)
    {
      int num = text.IndexOf("<" + name + ">", StringComparison.Ordinal);
      if (num < 0)
      {
        return null;
      }
      num += name.Length + 2;
      int num2 = text.IndexOf("</" + name + ">", num, StringComparison.Ordinal);
      if (num2 >= 0)
      {
        int num3 = num;
        return text.Substring(num3, num2 - num3);
      }
      return null;
    }

    // Token: 0x0600051E RID: 1310 RVA: 0x000EF774 File Offset: 0x000EF774
    public static string FirstSentence(string markdown, int maximumLength = 220)
    {
      string text = Regex.Replace(markdown, "(?m)^#+\\s*", "");
      text = Regex.Replace(text, "[*_`>|]", "");
      text = Regex.Replace(text, "\\[([^\\]]+)\\]\\([^)]+\\)", "$1");
      text = Regex.Replace(text, "\\s+", " ").Trim(' ');
      if (text.Length == 0)
      {
        return "";
      }
      Match match = Regex.Match(text, "(?<=[.!?])\\s");
      if (match.Success && match.Index < maximumLength)
      {
        return text.Substring(0, match.Index);
      }
      if (text.Length <= maximumLength)
      {
        return text;
      }
      return text.Substring(0, maximumLength - 1) + "…";
    }

    // Token: 0x040001DC RID: 476
    private static readonly string[] MetadataBlocks = new string[] { "NEXT_ACTIONS", "DONE_TITLE", "ARTIFACTS", "SUMMARY", "ROUTINE", "COMPUTER_USE_REQUEST", "WAIT_FOR", "LEARNED", "ANSWER" };

    // Token: 0x02000143 RID: 323
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040006C7 RID: 1735
      [Nullable(0)]
      public static Func<string, string> <0>__ExpandTilde;
    }
  }
}
