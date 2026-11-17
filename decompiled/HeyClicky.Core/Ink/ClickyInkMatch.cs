using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000160 RID: 352
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkMatch
  {
    // Token: 0x06001570 RID: 5488 RVA: 0x003B7B40 File Offset: 0x003B7B40
    public static List<string> Words(string text)
    {
      List<string> list = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = false;
      foreach (Rune rune in text.Lowered().EnumerateRunes())
      {
        UnicodeCategory unicodeCategory = Rune.GetUnicodeCategory(rune);
        bool flag2 = unicodeCategory - UnicodeCategory.NonSpacingMark <= 2;
        if (flag2)
        {
          flag2 = flag;
        }
        else
        {
          bool flag3 = Rune.IsLetter(rune) || Rune.IsNumber(rune);
          if (!flag3)
          {
            int value = rune.Value;
            bool flag4 = value == 36 || value == 46;
            flag3 = flag4;
          }
          flag2 = flag3;
        }
        bool flag5 = flag2;
        if (flag5)
        {
          stringBuilder.Append(rune.ToString());
        }
        else if (stringBuilder.Length > 0)
        {
          list.Add(stringBuilder.ToString());
          stringBuilder.Clear();
        }
        flag = flag5;
      }
      if (stringBuilder.Length > 0)
      {
        list.Add(stringBuilder.ToString());
      }
      return list;
    }

    // Token: 0x06001571 RID: 5489 RVA: 0x003B7C58 File Offset: 0x003B7C58
    public static double Similarity(string a, string b)
    {
      if (a == b)
      {
        return 1.0;
      }
      HashSet<string> hashSet = ClickyInkMatch.Words(ClickyInkParser.Emphasis.Strip(a)).ToHashSet<string>();
      HashSet<string> hashSet2 = ClickyInkMatch.Words(ClickyInkParser.Emphasis.Strip(b)).ToHashSet<string>();
      if (hashSet.Count == 0 && hashSet2.Count == 0)
      {
        return 0.0;
      }
      int num = hashSet.Count<string>(new Func<string, bool>(hashSet2.Contains));
      int num2 = hashSet.Count + hashSet2.Count - num;
      return (double)num / (double)num2;
    }

    // Token: 0x06001572 RID: 5490 RVA: 0x003B7CE0 File Offset: 0x003B7CE0
    public static string Signature(ClickyInkDocument.Block block)
    {
      List<string> list = new List<string> { block.Text };
      list.AddRange(block.Columns);
      list.AddRange(from i in block.Items.Take<ClickyInkDocument.Item>(8)
        select i.Text);
      foreach (string text in new string[] { "to", "from", "subject", "path", "query", "url", "title" })
      {
        string text2 = block[text];
        if (text2 != null)
        {
          list.Add(text2);
        }
      }
      return string.Join(" ", list);
    }

    // Token: 0x06001573 RID: 5491 RVA: 0x003B7DB4 File Offset: 0x003B7DB4
    public static bool SameFamily(ClickyInkDocument.Kind a, ClickyInkDocument.Kind b)
    {
      return a == b || (ClickyInkMatch.<SameFamily>g__IsList|3_0(a) && ClickyInkMatch.<SameFamily>g__IsList|3_0(b)) || (ClickyInkMatch.<SameFamily>g__IsProse|3_1(a) && ClickyInkMatch.<SameFamily>g__IsProse|3_1(b));
    }

    // Token: 0x06001574 RID: 5492 RVA: 0x003B7DE0 File Offset: 0x003B7DE0
    public static bool IsStillCatchingUp(ClickyInkDocument.Block old, ClickyInkDocument.Block @new)
    {
      string text = ClickyInkParser.Emphasis.Strip(old.Text);
      string text2 = ClickyInkParser.Emphasis.Strip(@new.Text);
      return @new.Items.Count < old.Items.Count || (text2.CharacterCount() < text.CharacterCount() && text.Lowered().HasPrefix(text2.Lowered()));
    }

    // Token: 0x06001575 RID: 5493 RVA: 0x003B7E40 File Offset: 0x003B7E40
    public static List<string> Tokens(string text)
    {
      List<string> list = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string text2 in text.Characters())
      {
        stringBuilder.Append(text2);
        if (text2 == " ")
        {
          list.Add(stringBuilder.ToString());
          stringBuilder.Clear();
        }
      }
      if (stringBuilder.Length > 0)
      {
        list.Add(stringBuilder.ToString());
      }
      return list;
    }

    // Token: 0x06001576 RID: 5494 RVA: 0x003B7EB4 File Offset: 0x003B7EB4
    [return: TupleElementNames(new string[] { "Piece", "Change" })]
    [return: Nullable(new byte[] { 1, 0, 1 })]
    public static List<ValueTuple<string, ClickyInkMatch.Change>> Lcs(List<string> a, List<string> b)
    {
      int count = a.Count;
      int count2 = b.Count;
      if ((long)count * (long)count2 >= 200000L)
      {
        List<ValueTuple<string, ClickyInkMatch.Change>> list = new List<ValueTuple<string, ClickyInkMatch.Change>>();
        list.AddRange(a.Select<string, ValueTuple<string, ClickyInkMatch.Change>>((string x) => new ValueTuple<string, ClickyInkMatch.Change>(x, ClickyInkMatch.Change.Removed)));
        list.AddRange(b.Select<string, ValueTuple<string, ClickyInkMatch.Change>>((string x) => new ValueTuple<string, ClickyInkMatch.Change>(x, ClickyInkMatch.Change.Added)));
        return list;
      }
      Func<string, string> func;
      if ((func = ClickyInkMatch.<>O.<0>__Key) == null)
      {
        func = (ClickyInkMatch.<>O.<0>__Key = new Func<string, string>(ClickyInkMatch.<Lcs>g__Key|7_0));
      }
      string[] array = a.Select<string, string>(func).ToArray<string>();
      Func<string, string> func2;
      if ((func2 = ClickyInkMatch.<>O.<0>__Key) == null)
      {
        func2 = (ClickyInkMatch.<>O.<0>__Key = new Func<string, string>(ClickyInkMatch.<Lcs>g__Key|7_0));
      }
      string[] array2 = b.Select<string, string>(func2).ToArray<string>();
      int[,] array3 = new int[count + 1, count2 + 1];
      for (int i = count - 1; i >= 0; i--)
      {
        for (int j = count2 - 1; j >= 0; j--)
        {
          array3[i, j] = ((array[i] == array2[j]) ? (array3[i + 1, j + 1] + 1) : Math.Max(array3[i + 1, j], array3[i, j + 1]));
        }
      }
      List<ValueTuple<string, ClickyInkMatch.Change>> list2 = new List<ValueTuple<string, ClickyInkMatch.Change>>();
      int k = 0;
      int l = 0;
      while (k < count)
      {
        if (l >= count2)
        {
          break;
        }
        if (array[k] == array2[l])
        {
          list2.Add(new ValueTuple<string, ClickyInkMatch.Change>(b[l], ClickyInkMatch.Change.Kept));
          k++;
          l++;
        }
        else if (array3[k + 1, l] >= array3[k, l + 1])
        {
          list2.Add(new ValueTuple<string, ClickyInkMatch.Change>(a[k], ClickyInkMatch.Change.Removed));
          k++;
        }
        else
        {
          list2.Add(new ValueTuple<string, ClickyInkMatch.Change>(b[l], ClickyInkMatch.Change.Added));
          l++;
        }
      }
      while (k < count)
      {
        list2.Add(new ValueTuple<string, ClickyInkMatch.Change>(a[k], ClickyInkMatch.Change.Removed));
        k++;
      }
      while (l < count2)
      {
        list2.Add(new ValueTuple<string, ClickyInkMatch.Change>(b[l], ClickyInkMatch.Change.Added));
        l++;
      }
      return list2;
    }

    // Token: 0x06001577 RID: 5495 RVA: 0x003B80F0 File Offset: 0x003B80F0
    public static List<ClickyInkLiveNote.Segment> Merged(List<ClickyInkLiveNote.Segment> segments)
    {
      List<ClickyInkLiveNote.Segment> list = new List<ClickyInkLiveNote.Segment>();
      foreach (ClickyInkLiveNote.Segment segment in segments)
      {
        if (list.Count > 0)
        {
          List<ClickyInkLiveNote.Segment> list2 = list;
          ClickyInkLiveNote.Segment segment2 = list2[list2.Count - 1];
          if (segment2.Struck == segment.Struck)
          {
            double? num = segment2.VanishesAt;
            double? num2 = segment.VanishesAt;
            if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
            {
              int? num3 = segment2.Edit;
              int? num4 = segment.Edit;
              if (((num3.GetValueOrDefault() == num4.GetValueOrDefault()) & (num3 != null == (num4 != null))) && segment2.IsPreview == segment.IsPreview)
              {
                InkRange? inkRange = segment2.Written;
                if (inkRange != null)
                {
                  InkRange valueOrDefault = inkRange.GetValueOrDefault();
                  inkRange = segment.Written;
                  if (inkRange != null)
                  {
                    InkRange valueOrDefault2 = inkRange.GetValueOrDefault();
                    if (Math.Abs(valueOrDefault.UpperBound - valueOrDefault2.LowerBound) < 0.001)
                    {
                      List<ClickyInkLiveNote.Segment> list3 = list;
                      int num5 = list3.Count - 1;
                      ClickyInkLiveNote.Segment segment3 = segment2;
                      segment3.Text = segment2.Text + segment.Text;
                      segment3.Written = new InkRange?(new InkRange(valueOrDefault.LowerBound, valueOrDefault2.UpperBound));
                      list3[num5] = segment3;
                      continue;
                    }
                  }
                }
              }
            }
          }
          if (segment2.Written == null && segment.Written == null && segment2.Struck == segment.Struck)
          {
            double? num2 = segment2.VanishesAt;
            double? num = segment.VanishesAt;
            if ((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null)))
            {
              int? num4 = segment2.Edit;
              int? num3 = segment.Edit;
              if (((num4.GetValueOrDefault() == num3.GetValueOrDefault()) & (num4 != null == (num3 != null))) && segment2.IsPreview == segment.IsPreview)
              {
                List<ClickyInkLiveNote.Segment> list4 = list;
                int num6 = list4.Count - 1;
                ClickyInkLiveNote.Segment segment3 = segment2;
                segment3.Text = segment2.Text + segment.Text;
                list4[num6] = segment3;
                continue;
              }
            }
          }
        }
        list.Add(segment);
      }
      return list;
    }

    // Token: 0x06001578 RID: 5496 RVA: 0x003B8404 File Offset: 0x003B8404
    [CompilerGenerated]
    internal static bool <SameFamily>g__IsList|3_0(ClickyInkDocument.Kind k)
    {
      return k == ClickyInkDocument.Kind.List || k == ClickyInkDocument.Kind.Numbered;
    }

    // Token: 0x06001579 RID: 5497 RVA: 0x003B8420 File Offset: 0x003B8420
    [CompilerGenerated]
    internal static bool <SameFamily>g__IsProse|3_1(ClickyInkDocument.Kind k)
    {
      return k == ClickyInkDocument.Kind.Paragraph || k == ClickyInkDocument.Kind.Quote || k == ClickyInkDocument.Kind.Note;
    }

    // Token: 0x0600157A RID: 5498 RVA: 0x003B8444 File Offset: 0x003B8444
    [CompilerGenerated]
    internal static string <Lcs>g__Key|7_0(string s)
    {
      return ClickyInkParser.Emphasis.Strip(s).TrimWs().Lowered();
    }

    // Token: 0x0200051B RID: 1307
    [NullableContext(0)]
    public enum Change
    {
      // Token: 0x04001F5E RID: 8030
      Kept,
      // Token: 0x04001F5F RID: 8031
      Removed,
      // Token: 0x04001F60 RID: 8032
      Added
    }

    // Token: 0x0200051C RID: 1308
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001F61 RID: 8033
      [Nullable(0)]
      public static Func<string, string> <0>__Key;
    }
  }
}
