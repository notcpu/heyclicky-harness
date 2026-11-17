using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000D8 RID: 216
  [NullableContext(1)]
  [Nullable(0)]
  public static class FallbackClickyIdentity
  {
    // Token: 0x06000DC2 RID: 3522 RVA: 0x00383734 File Offset: 0x00383734
    private static bool IsTaskVerb(string word)
    {
      if (FallbackClickyIdentity.TaskVerbs.Contains(word))
      {
        return true;
      }
      if (!word.EndsWith("ing", StringComparison.Ordinal) || word.Length <= 4)
      {
        return false;
      }
      string text = word.Substring(0, word.Length - 3);
      if (FallbackClickyIdentity.TaskVerbs.Contains(text) || FallbackClickyIdentity.TaskVerbs.Contains(text + "e"))
      {
        return true;
      }
      if (text.Length >= 2)
      {
        string text2 = text;
        char c = text2[text2.Length - 1];
        string text3 = text;
        if (c == text3[text3.Length - 2])
        {
          HashSet<string> taskVerbs = FallbackClickyIdentity.TaskVerbs;
          string text4 = text;
          return taskVerbs.Contains(text4.Substring(0, text4.Length - 1));
        }
      }
      return false;
    }

    // Token: 0x06000DC3 RID: 3523 RVA: 0x003837E8 File Offset: 0x003837E8
    [return: TupleElementNames(new string[] { "Name", "Role" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    public static ValueTuple<string, string> For(string title)
    {
      List<string> list = (from w in (from w in Regex.Split(title, "[^A-Za-z0-9]+")
          where w.Length > 0
          select w).ToList<string>()
        where !FallbackClickyIdentity.IsTaskVerb(w.ToLowerInvariant()) && !FallbackClickyIdentity.Filler.Contains(w.ToLowerInvariant())
        select w).ToList<string>();
      string text = ((list.Count > 0) ? FallbackClickyIdentity.<For>g__TitleCased|4_2(list[0]) : "Task");
      int num = text.ToLowerInvariant().Sum<char>((char ch) => (int)ch);
      string text2 = text + " " + FallbackClickyIdentity.Family[num % FallbackClickyIdentity.Family.Length];
      IEnumerable<string> enumerable = list.Take<string>(3);
      Func<string, string> func;
      if ((func = FallbackClickyIdentity.<>O.<0>__TitleCased) == null)
      {
        func = (FallbackClickyIdentity.<>O.<0>__TitleCased = new Func<string, string>(FallbackClickyIdentity.<For>g__TitleCased|4_2));
      }
      List<string> list2 = enumerable.Select<string, string>(func).ToList<string>();
      return new ValueTuple<string, string>(text2, (list2.Count == 0) ? "Agent" : string.Join(" ", list2));
    }

    // Token: 0x06000DC4 RID: 3524 RVA: 0x00383900 File Offset: 0x00383900
    public static string NormalizedName(string name)
    {
      IEnumerable<char> enumerable = name.ToLowerInvariant();
      Func<char, bool> func;
      if ((func = FallbackClickyIdentity.<>O.<1>__IsLetterOrDigit) == null)
      {
        func = (FallbackClickyIdentity.<>O.<1>__IsLetterOrDigit = new Func<char, bool>(char.IsLetterOrDigit));
      }
      return new string(enumerable.Where<char>(func).ToArray<char>());
    }

    // Token: 0x06000DC6 RID: 3526 RVA: 0x00383D38 File Offset: 0x00383D38
    [CompilerGenerated]
    internal static string <For>g__TitleCased|4_2(string w)
    {
      string text = w.ToLowerInvariant();
      if (text == "pdf" || text == "csv" || text == "seo" || text == "ads" || text == "api")
      {
        return w.ToUpperInvariant();
      }
      if (!(w == w.ToLowerInvariant()))
      {
        return w;
      }
      char c = char.ToUpperInvariant(w[0]);
      return new ReadOnlySpan<char>(ref c) + w.Substring(1, w.Length - 1);
    }

    // Token: 0x040005CB RID: 1483
    private static readonly string[] Family = new string[] { "Buddy", "Scout", "Desk", "Keeper", "Pal", "Lab" };

    // Token: 0x040005CC RID: 1484
    private static readonly HashSet<string> TaskVerbs = new HashSet<string>
    {
      "run", "check", "draft", "write", "build", "make", "create", "update", "send", "analyze",
      "analyse", "review", "summarize", "summarise", "find", "prepare", "plan", "turn", "set", "pull",
      "list", "sync", "clean", "fix", "monitor", "track", "watch", "scan", "audit", "compile",
      "collect", "gather", "organize", "organise", "research", "look", "dig", "map", "triage", "reply",
      "answer", "follow", "generate", "produce", "put", "get", "do"
    };

    // Token: 0x040005CD RID: 1485
    private static readonly HashSet<string> Filler = new HashSet<string>
    {
      "the", "a", "an", "my", "your", "our", "for", "of", "to", "in",
      "on", "with", "and", "into", "from", "this", "that", "up", "out", "me",
      "you", "please", "agent", "clicky", "assistant", "can", "could", "would", "want", "like",
      "help"
    };

    // Token: 0x020003E8 RID: 1000
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001A3A RID: 6714
      [Nullable(0)]
      public static Func<string, string> <0>__TitleCased;

      // Token: 0x04001A3B RID: 6715
      [Nullable(0)]
      public static Func<char, bool> <1>__IsLetterOrDigit;
    }
  }
}
