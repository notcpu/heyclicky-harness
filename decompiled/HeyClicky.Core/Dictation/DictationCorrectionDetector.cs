using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Dictation
{
  // Token: 0x020001CD RID: 461
  [NullableContext(1)]
  [Nullable(0)]
  public static class DictationCorrectionDetector
  {
    // Token: 0x06001AA4 RID: 6820 RVA: 0x003D8494 File Offset: 0x003D8494
    [return: Nullable(2)]
    public static DictationLearnedCorrection DetectSingleWordCorrection(string baseline, string current, string insertedText)
    {
      string[] array = DictationCorrectionDetector.Tokenize(baseline);
      string[] array2 = DictationCorrectionDetector.Tokenize(current);
      if (array.Length == 0 || array.Length != array2.Length)
      {
        return null;
      }
      int? num = null;
      for (int i = 0; i < array.Length; i++)
      {
        if (!(array[i] == array2[i]))
        {
          if (num != null)
          {
            return null;
          }
          num = new int?(i);
        }
      }
      if (num == null)
      {
        return null;
      }
      int valueOrDefault = num.GetValueOrDefault();
      string text = array[valueOrDefault].Trim(DictationCorrectionDetector.WordTrim);
      string text2 = array2[valueOrDefault].Trim(DictationCorrectionDetector.WordTrim);
      if (text.Length == 0 || text2.Length == 0 || string.Equals(text, text2, StringComparison.OrdinalIgnoreCase))
      {
        return null;
      }
      if (!DictationCorrectionDetector.IsDistinctiveLearnableWord(text2))
      {
        return null;
      }
      if (!(from w in DictationCorrectionDetector.Tokenize(insertedText)
        select w.Trim(DictationCorrectionDetector.WordTrim).ToLowerInvariant()).ToHashSet<string>().Contains(text.ToLowerInvariant()))
      {
        return null;
      }
      if (DictationCorrectionDetector.NormalizedSimilarity(text.ToLowerInvariant(), text2.ToLowerInvariant()) < 0.3)
      {
        return null;
      }
      return new DictationLearnedCorrection(text, text2);
    }

    // Token: 0x06001AA5 RID: 6821 RVA: 0x003D85C0 File Offset: 0x003D85C0
    private static string[] Tokenize(string text)
    {
      return text.Split(null, StringSplitOptions.RemoveEmptyEntries);
    }

    // Token: 0x06001AA6 RID: 6822 RVA: 0x003D85CC File Offset: 0x003D85CC
    private static bool IsDistinctiveLearnableWord(string word)
    {
      int length = word.Length;
      if (length >= 2 && length <= 40)
      {
        Func<char, bool> func;
        if ((func = DictationCorrectionDetector.<>O.<0>__IsLetter) == null)
        {
          func = (DictationCorrectionDetector.<>O.<0>__IsLetter = new Func<char, bool>(char.IsLetter));
        }
        if (word.Any<char>(func))
        {
          Func<char, bool> func2;
          if ((func2 = DictationCorrectionDetector.<>O.<1>__IsDigit) == null)
          {
            func2 = (DictationCorrectionDetector.<>O.<1>__IsDigit = new Func<char, bool>(char.IsDigit));
          }
          if (word.All<char>(func2))
          {
            return false;
          }
          if (char.IsUpper(word[0]))
          {
            return true;
          }
          if (word.Contains('_'))
          {
            return true;
          }
          if (word.Length <= 6)
          {
            if (word.All<char>((char c) => char.IsUpper(c) || char.IsDigit(c)))
            {
              return true;
            }
          }
          if (char.IsLower(word[0]))
          {
            IEnumerable<char> enumerable = word.Skip<char>(1);
            Func<char, bool> func3;
            if ((func3 = DictationCorrectionDetector.<>O.<2>__IsUpper) == null)
            {
              func3 = (DictationCorrectionDetector.<>O.<2>__IsUpper = new Func<char, bool>(char.IsUpper));
            }
            return enumerable.Any<char>(func3);
          }
          return false;
        }
      }
      return false;
    }

    // Token: 0x06001AA7 RID: 6823 RVA: 0x003D86C4 File Offset: 0x003D86C4
    private static double NormalizedSimilarity(string a, string b)
    {
      int num = Math.Max(a.Length, b.Length);
      if (num != 0)
      {
        return 1.0 - (double)DictationCorrectionDetector.Levenshtein(a, b) / (double)num;
      }
      return 1.0;
    }

    // Token: 0x06001AA8 RID: 6824 RVA: 0x003D8708 File Offset: 0x003D8708
    private static int Levenshtein(string a, string b)
    {
      if (a.Length == 0)
      {
        return b.Length;
      }
      if (b.Length == 0)
      {
        return a.Length;
      }
      int[] array = new int[b.Length + 1];
      int[] array2 = new int[b.Length + 1];
      for (int i = 0; i <= b.Length; i++)
      {
        array[i] = i;
      }
      for (int j = 1; j <= a.Length; j++)
      {
        array2[0] = j;
        for (int k = 1; k <= b.Length; k++)
        {
          array2[k] = Math.Min(Math.Min(array[k] + 1, array2[k - 1] + 1), array[k - 1] + ((a[j - 1] != b[k - 1]) ? 1 : 0));
        }
        int[] array3 = array2;
        array2 = array;
        array = array3;
      }
      return array[b.Length];
    }

    // Token: 0x04000B6F RID: 2927
    public static readonly TimeSpan ObservationWindow = TimeSpan.FromSeconds(20L);

    // Token: 0x04000B70 RID: 2928
    public static readonly TimeSpan BaselineDelay = TimeSpan.FromSeconds(0.7);

    // Token: 0x04000B71 RID: 2929
    public static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(2.5);

    // Token: 0x04000B72 RID: 2930
    public const int MaxFieldCharacterCount = 20000;

    // Token: 0x04000B73 RID: 2931
    private static readonly char[] WordTrim = ".,;:!?\"'()[]{}…".ToCharArray();

    // Token: 0x020005DE RID: 1502
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04002216 RID: 8726
      [Nullable(0)]
      public static Func<char, bool> <0>__IsLetter;

      // Token: 0x04002217 RID: 8727
      [Nullable(0)]
      public static Func<char, bool> <1>__IsDigit;

      // Token: 0x04002218 RID: 8728
      [Nullable(0)]
      public static Func<char, bool> <2>__IsUpper;
    }
  }
}
