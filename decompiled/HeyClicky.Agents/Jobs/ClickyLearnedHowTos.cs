using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000045 RID: 69
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyLearnedHowTos
  {
    // Token: 0x1700019D RID: 413
    // (get) Token: 0x0600062B RID: 1579 RVA: 0x000F46AC File Offset: 0x000F46AC
    public IReadOnlyList<ClickyLearnedHowTo> HowTos
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<ClickyLearnedHowTo> readOnlyList;
        lock (obj)
        {
          readOnlyList = this.howTos.ToList<ClickyLearnedHowTo>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x0600062C RID: 1580 RVA: 0x000F46F4 File Offset: 0x000F46F4
    public static IReadOnlyList<string> Lines([Nullable(2)] string block)
    {
      if (block == null)
      {
        return Array.Empty<string>();
      }
      return (from l in block.Split(RuntimeHelpers.CreateSpan<char>(fieldof(<PrivateImplementationDetails>.FBB68C637180E81FFB4AF91596B53064C159DDF8F56E330BDD31C0DE6472348D2).FieldHandle))
        select l.Trim().Trim(new char[] { '-', '*', '•', ' ', '\t' }).Trim() into l
        where l.Length > 0
        select l).Take<string>(3).Select<string, string>(delegate(string l)
      {
        if (l.Length <= 240)
        {
          return l;
        }
        return l.Substring(0, 240);
      }).ToList<string>();
    }

    // Token: 0x0600062D RID: 1581 RVA: 0x000F4794 File Offset: 0x000F4794
    public void Record(IReadOnlyList<string> lines, [Nullable(2)] string jobId)
    {
      object obj = this.gate;
      lock (obj)
      {
        List<string> list = lines.Where<string>((string line) => !this.howTos.Any<ClickyLearnedHowTo>((ClickyLearnedHowTo h) => ClickyLearnedHowTos.SameHowTo(h.Text, line))).ToList<string>();
        if (list.Count != 0)
        {
          this.howTos.AddRange(list.Select<string, ClickyLearnedHowTo>((string t) => new ClickyLearnedHowTo(t, DateTimeOffset.Now, jobId)));
          if (this.howTos.Count > 60)
          {
            this.howTos.RemoveRange(0, this.howTos.Count - 60);
          }
          AgentDefaults.Set<List<ClickyLearnedHowTo>>("clicky.jobs.learnedHowTos.v1", this.howTos);
        }
      }
    }

    // Token: 0x0600062E RID: 1582 RVA: 0x000F485C File Offset: 0x000F485C
    public string PromptSection(string ask)
    {
      IReadOnlyList<string> readOnlyList = ClickyLearnedHowTos.Choose(this.HowTos.Select<ClickyLearnedHowTo, string>((ClickyLearnedHowTo h) => h.Text).ToList<string>(), ask);
      if (readOnlyList.Count == 0)
      {
        return "";
      }
      return "\nWhat earlier jobs learned about this user's setup (use what fits; it may be out of date):\n" + string.Join("\n", readOnlyList.Select<string, string>((string c) => "- " + c));
    }

    // Token: 0x0600062F RID: 1583 RVA: 0x000F48E8 File Offset: 0x000F48E8
    public void RemoveAllForSignOut()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.howTos = new List<ClickyLearnedHowTo>();
      }
      AgentDefaults.Remove("clicky.jobs.learnedHowTos.v1");
    }

    // Token: 0x06000630 RID: 1584 RVA: 0x000F4938 File Offset: 0x000F4938
    public static IReadOnlyList<string> Choose(IReadOnlyList<string> texts, string ask)
    {
      List<string> list = texts.Skip<string>(Math.Max(0, texts.Count - 10)).ToList<string>();
      IEnumerable<string> enumerable = texts.Take<string>(Math.Max(0, texts.Count - 10)).ToList<string>();
      HashSet<string> askWords = ClickyLearnedHowTos.Words(ask);
      return (from x in (from t in enumerable.Reverse<string>()
          select new ValueTuple<string, int>(t, ClickyLearnedHowTos.Words(t).Intersect<string>(askWords).Count<string>()) into x
          where x.Item2 > 0
          orderby x.Item2 descending
          select x).Take<ValueTuple<string, int>>(5)
        select x.Item1).Reverse<string>().ToList<string>().Concat<string>(list)
        .ToList<string>();
    }

    // Token: 0x06000631 RID: 1585 RVA: 0x000F4A2C File Offset: 0x000F4A2C
    private static HashSet<string> Words(string text)
    {
      ClickyLearnedHowTos.<>c__DisplayClass15_0 CS$<>8__locals1;
      CS$<>8__locals1.words = new HashSet<string>();
      CS$<>8__locals1.current = new StringBuilder();
      foreach (char c in text.ToLowerInvariant())
      {
        if (char.IsLetterOrDigit(c))
        {
          CS$<>8__locals1.current.Append(c);
        }
        else
        {
          ClickyLearnedHowTos.<Words>g__Flush|15_0(ref CS$<>8__locals1);
        }
      }
      ClickyLearnedHowTos.<Words>g__Flush|15_0(ref CS$<>8__locals1);
      return CS$<>8__locals1.words;
    }

    // Token: 0x06000632 RID: 1586 RVA: 0x000F4A9C File Offset: 0x000F4A9C
    private static bool SameHowTo(string a, string b)
    {
      return ClickyLearnedHowTos.<SameHowTo>g__Normalize|16_0(a) == ClickyLearnedHowTos.<SameHowTo>g__Normalize|16_0(b);
    }

    // Token: 0x06000635 RID: 1589 RVA: 0x000F4CC8 File Offset: 0x000F4CC8
    [CompilerGenerated]
    internal static void <Words>g__Flush|15_0(ref ClickyLearnedHowTos.<>c__DisplayClass15_0 A_0)
    {
      if (A_0.current.Length >= 3 && !ClickyLearnedHowTos.IgnoredWords.Contains(A_0.current.ToString()))
      {
        A_0.words.Add(A_0.current.ToString());
      }
      A_0.current.Clear();
    }

    // Token: 0x06000636 RID: 1590 RVA: 0x000F4D20 File Offset: 0x000F4D20
    [CompilerGenerated]
    internal static string <SameHowTo>g__Normalize|16_0(string s)
    {
      IEnumerable<char> enumerable = s.ToLowerInvariant();
      Func<char, bool> func;
      if ((func = ClickyLearnedHowTos.<>O.<0>__IsLetterOrDigit) == null)
      {
        func = (ClickyLearnedHowTos.<>O.<0>__IsLetterOrDigit = new Func<char, bool>(char.IsLetterOrDigit));
      }
      return new string(enumerable.Where<char>(func).ToArray<char>());
    }

    // Token: 0x0400024A RID: 586
    private const string DefaultsKey = "clicky.jobs.learnedHowTos.v1";

    // Token: 0x0400024B RID: 587
    public const int MaximumKept = 60;

    // Token: 0x0400024C RID: 588
    public const int NewestCarried = 10;

    // Token: 0x0400024D RID: 589
    public const int RelatedCarried = 5;

    // Token: 0x0400024E RID: 590
    public const int MaximumLineLength = 240;

    // Token: 0x0400024F RID: 591
    private readonly object gate = new object();

    // Token: 0x04000250 RID: 592
    private List<ClickyLearnedHowTo> howTos = AgentDefaults.Get<List<ClickyLearnedHowTo>>("clicky.jobs.learnedHowTos.v1") ?? new List<ClickyLearnedHowTo>();

    // Token: 0x04000251 RID: 593
    private static readonly HashSet<string> IgnoredWords = new HashSet<string>
    {
      "the", "and", "for", "with", "from", "that", "this", "what", "when", "where",
      "which", "your", "their", "have", "has", "was", "are", "can", "you", "my",
      "me", "it", "is", "in", "on", "of", "to", "a", "an", "or",
      "at", "by", "use", "used", "search", "find", "get", "next", "latest"
    };

    // Token: 0x02000168 RID: 360
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x0400072B RID: 1835
      [Nullable(0)]
      public static Func<char, bool> <0>__IsLetterOrDigit;
    }
  }
}
