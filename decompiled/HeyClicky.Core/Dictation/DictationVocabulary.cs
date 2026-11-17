using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Settings;

namespace HeyClicky.Core.Dictation
{
  // Token: 0x020001CB RID: 459
  [NullableContext(1)]
  [Nullable(0)]
  public static class DictationVocabulary
  {
    // Token: 0x06001A88 RID: 6792 RVA: 0x003D7D6C File Offset: 0x003D7D6C
    public static IReadOnlyList<string> Terms(IEnumerable<string> rawLines)
    {
      HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
      List<string> list = new List<string>();
      foreach (string text in rawLines)
      {
        string[] array = text.Split(RuntimeHelpers.CreateSpan<char>(fieldof(<PrivateImplementationDetails>.056E311224C55DE36654F8E4796323C07DF8632C5F8CE4BC1C110DB66D8F88622).FieldHandle));
        for (int i = 0; i < array.Length; i++)
        {
          string text2 = array[i].Trim();
          if (text2.Length != 0 && text2.Length <= 60 && hashSet.Add(text2))
          {
            list.Add(text2);
            if (list.Count >= 50)
            {
              return list;
            }
          }
        }
      }
      return list;
    }

    // Token: 0x06001A89 RID: 6793 RVA: 0x003D7E24 File Offset: 0x003D7E24
    public static IReadOnlyList<string> SelectedTerms(SettingsStore settings)
    {
      return DictationVocabulary.Terms(settings.Read<List<string>>(([Nullable(1)] AppSettings s) => s.DictationCustomWords.ToList<string>()));
    }

    // Token: 0x06001A8A RID: 6794 RVA: 0x003D7E50 File Offset: 0x003D7E50
    public static bool AddTerm(SettingsStore settings, string term)
    {
      string trimmed = term.Trim();
      if (trimmed.Length == 0 || trimmed.Length > 60)
      {
        return false;
      }
      if (DictationVocabulary.SelectedTerms(settings).Any<string>((string t) => string.Equals(t, trimmed, StringComparison.OrdinalIgnoreCase)))
      {
        return false;
      }
      settings.Update(delegate(AppSettings s)
      {
        s.DictationCustomWords.Add(trimmed);
      });
      return true;
    }

    // Token: 0x06001A8B RID: 6795 RVA: 0x003D7EBC File Offset: 0x003D7EBC
    public static void AddBuiltInTermsIfNeeded(SettingsStore settings)
    {
      if (settings.Read<bool>((AppSettings s) => s.DictationClaudeSeeded))
      {
        return;
      }
      DictationVocabulary.AddTerm(settings, "Claude");
      settings.Update(delegate(AppSettings s)
      {
        s.DictationClaudeSeeded = true;
      });
    }

    // Token: 0x06001A8C RID: 6796 RVA: 0x003D7F24 File Offset: 0x003D7F24
    public static void AddAccountNameIfNeeded(SettingsStore settings, [Nullable(2)] string displayName)
    {
      string name = ((displayName != null) ? displayName.Trim() : null);
      if (!string.IsNullOrEmpty(name) && !name.Contains('@'))
      {
        if (!(settings.Read<string>(([Nullable(1)] AppSettings s) => s.DictationAccountNameSeeded) == name))
        {
          foreach (string text in (from word in name.Split(null, StringSplitOptions.None).Select<string, string>(delegate(string word)
            {
              int i = 0;
              int num = word.Length;
              while (i < num)
              {
                if (DictationVocabulary.<AddAccountNameIfNeeded>g__Keeps|8_1(word[i]))
                {
                  break;
                }
                i++;
              }
              while (num > i && !DictationVocabulary.<AddAccountNameIfNeeded>g__Keeps|8_1(word[num - 1]))
              {
                num--;
              }
              int num2 = i;
              return word.Substring(num2, num - num2);
            })
            where word.Length >= 2
            select word).Take<string>(3))
          {
            DictationVocabulary.AddTerm(settings, text);
          }
          settings.Update(delegate(AppSettings s)
          {
            s.DictationAccountNameSeeded = name;
          });
          return;
        }
      }
    }

    // Token: 0x06001A8D RID: 6797 RVA: 0x003D8040 File Offset: 0x003D8040
    public static void RemoveTerm(SettingsStore settings, string term)
    {
      string normalized = term.Trim();
      if (normalized.Length == 0)
      {
        return;
      }
      Func<string, bool> <>9__1;
      settings.Update(delegate(AppSettings s)
      {
        List<string> list = new List<string>();
        foreach (string text in s.DictationCustomWords)
        {
          IEnumerable<string> enumerable = text.Split(',', StringSplitOptions.None);
          Func<string, bool> func;
          if ((func = <>9__1) == null)
          {
            func = (<>9__1 = (string p) => !string.Equals(p.Trim(), normalized, StringComparison.OrdinalIgnoreCase));
          }
          List<string> list2 = enumerable.Where<string>(func).ToList<string>();
          if (!list2.All<string>((string p) => p.Trim().Length == 0))
          {
            list.Add(string.Join(",", list2));
          }
        }
        s.DictationCustomWords = list;
      });
    }

    // Token: 0x06001A8E RID: 6798 RVA: 0x003D8080 File Offset: 0x003D8080
    public static IReadOnlyList<string> ContextKeyterms(string surroundingText)
    {
      if (string.IsNullOrEmpty(surroundingText))
      {
        return Array.Empty<string>();
      }
      string[] array = surroundingText.Split(null, StringSplitOptions.RemoveEmptyEntries);
      DictationVocabulary.<>c__DisplayClass10_0 CS$<>8__locals1;
      CS$<>8__locals1.results = new List<string>();
      CS$<>8__locals1.seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
      int num = 0;
      while (num < array.Length && CS$<>8__locals1.results.Count < 12)
      {
        string text = array[num];
        if (DictationVocabulary.StartsCapitalized(text))
        {
          int num2 = num + 1;
          while (num2 < array.Length && DictationVocabulary.StartsCapitalized(array[num2]))
          {
            num2++;
          }
          if (num2 - num >= 2)
          {
            DictationVocabulary.<ContextKeyterms>g__Append|10_0(string.Join(" ", RuntimeHelpers.GetSubArray<string>(array, new Range(num, num2))), ref CS$<>8__locals1);
            num = num2;
            continue;
          }
        }
        if (DictationVocabulary.IsAcronymOrIdentifier(text))
        {
          DictationVocabulary.<ContextKeyterms>g__Append|10_0(text, ref CS$<>8__locals1);
        }
        num++;
      }
      return CS$<>8__locals1.results;
    }

    // Token: 0x06001A8F RID: 6799 RVA: 0x003D8158 File Offset: 0x003D8158
    private static bool StartsCapitalized(string word)
    {
      return word.Length > 0 && char.IsUpper(word[0]);
    }

    // Token: 0x06001A90 RID: 6800 RVA: 0x003D8174 File Offset: 0x003D8174
    private static bool IsAcronymOrIdentifier(string word)
    {
      string text = word.Trim(DictationVocabulary.PunctuationTrim);
      if (text.Length < 2)
      {
        return false;
      }
      if (text.Contains('_'))
      {
        return true;
      }
      if (text.Length <= 6)
      {
        if (text.All<char>((char c) => char.IsUpper(c) || char.IsDigit(c)))
        {
          IEnumerable<char> enumerable = text;
          Func<char, bool> func;
          if ((func = DictationVocabulary.<>O.<0>__IsLetter) == null)
          {
            func = (DictationVocabulary.<>O.<0>__IsLetter = new Func<char, bool>(char.IsLetter));
          }
          if (enumerable.Any<char>(func))
          {
            return true;
          }
        }
      }
      if (char.IsLower(text[0]))
      {
        IEnumerable<char> enumerable2 = text.Skip<char>(1);
        Func<char, bool> func2;
        if ((func2 = DictationVocabulary.<>O.<1>__IsUpper) == null)
        {
          func2 = (DictationVocabulary.<>O.<1>__IsUpper = new Func<char, bool>(char.IsUpper));
        }
        return enumerable2.Any<char>(func2);
      }
      return false;
    }

    // Token: 0x06001A92 RID: 6802 RVA: 0x003D8244 File Offset: 0x003D8244
    [CompilerGenerated]
    internal static bool <AddAccountNameIfNeeded>g__Keeps|8_1(char c)
    {
      bool flag = char.IsLetter(c);
      if (!flag)
      {
        bool flag2 = c == '\'' || c == '-';
        flag = flag2;
      }
      return flag;
    }

    // Token: 0x06001A93 RID: 6803 RVA: 0x003D8270 File Offset: 0x003D8270
    [CompilerGenerated]
    internal static void <ContextKeyterms>g__Append|10_0(string candidate, ref DictationVocabulary.<>c__DisplayClass10_0 A_1)
    {
      string text = candidate.Trim(DictationVocabulary.PunctuationTrim);
      if (text.Length < 2 || text.Length > 60 || !A_1.seen.Add(text))
      {
        return;
      }
      A_1.results.Add(text);
    }

    // Token: 0x04000B69 RID: 2921
    public const int MaxTermCount = 50;

    // Token: 0x04000B6A RID: 2922
    public const int MaxTermCharacterCount = 60;

    // Token: 0x04000B6B RID: 2923
    private const int MaxContextKeytermCount = 12;

    // Token: 0x04000B6C RID: 2924
    private static readonly char[] PunctuationTrim = ".,;:!?\"'()[]{}".ToCharArray();

    // Token: 0x020005D8 RID: 1496
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04002205 RID: 8709
      [Nullable(0)]
      public static Func<char, bool> <0>__IsLetter;

      // Token: 0x04002206 RID: 8710
      [Nullable(0)]
      public static Func<char, bool> <1>__IsUpper;
    }
  }
}
