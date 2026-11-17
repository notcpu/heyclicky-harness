using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000033 RID: 51
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyWorkShelf
  {
    // Token: 0x17000153 RID: 339
    // (get) Token: 0x06000520 RID: 1312 RVA: 0x000EF888 File Offset: 0x000EF888
    [Nullable(2)]
    public ClickyWorkPiece OpenQuestion
    {
      [NullableContext(2)]
      get;
    }

    // Token: 0x17000154 RID: 340
    // (get) Token: 0x06000521 RID: 1313 RVA: 0x000EF890 File Offset: 0x000EF890
    public IReadOnlyList<ClickyWorkPiece> Made { get; }

    // Token: 0x17000155 RID: 341
    // (get) Token: 0x06000522 RID: 1314 RVA: 0x000EF898 File Offset: 0x000EF898
    public IReadOnlyList<ClickyWorkPiece> QuickAnswers { get; }

    // Token: 0x17000156 RID: 342
    // (get) Token: 0x06000523 RID: 1315 RVA: 0x000EF8A0 File Offset: 0x000EF8A0
    public IReadOnlyDictionary<string, int> RoutineRunCounts { get; }

    // Token: 0x06000524 RID: 1316 RVA: 0x000EF8A8 File Offset: 0x000EF8A8
    public ClickyWorkShelf(IReadOnlyList<ClickyWorkPiece> works)
    {
      HashSet<string> hashSet = new HashSet<string>();
      HashSet<string> hashSet2 = new HashSet<string>();
      foreach (ClickyWorkPiece clickyWorkPiece in works.OrderBy<ClickyWorkPiece, DateTimeOffset>((ClickyWorkPiece p) => p.FinishedAt))
      {
        string text = clickyWorkPiece.ThreadId ?? clickyWorkPiece.Id;
        if (!hashSet.Add(text))
        {
          hashSet2.Add(clickyWorkPiece.Id);
        }
      }
      List<ClickyWorkPiece> list = new List<ClickyWorkPiece>();
      List<ClickyWorkPiece> list2 = new List<ClickyWorkPiece>();
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
      ClickyWorkPiece clickyWorkPiece2 = null;
      for (int i = 0; i < works.Count; i++)
      {
        ClickyWorkPiece clickyWorkPiece3 = works[i];
        switch (ClickyWorkShelf.KindOf(clickyWorkPiece3, hashSet2.Contains(clickyWorkPiece3.Id)))
        {
        case ClickyWorkShelf.Kind.Made:
          if (clickyWorkPiece3.IsRoutineRun)
          {
            string text2 = ClickyWorkShelf.RoutineKey(clickyWorkPiece3);
            string text3;
            if (dictionary2.TryGetValue(text2, out text3))
            {
              dictionary[text3] = dictionary.GetValueOrDefault(text3, 1) + 1;
              break;
            }
            dictionary2[text2] = clickyWorkPiece3.Id;
            dictionary[clickyWorkPiece3.Id] = 1;
          }
          list.Add(clickyWorkPiece3);
          break;
        case ClickyWorkShelf.Kind.QuickAnswer:
          list2.Add(clickyWorkPiece3);
          break;
        case ClickyWorkShelf.Kind.Question:
          if (i == 0)
          {
            clickyWorkPiece2 = clickyWorkPiece3;
          }
          break;
        }
      }
      this.OpenQuestion = clickyWorkPiece2;
      this.Made = list;
      this.QuickAnswers = list2;
      this.RoutineRunCounts = dictionary.Where<KeyValuePair<string, int>>((KeyValuePair<string, int> kv) => kv.Value > 1).ToDictionary<KeyValuePair<string, int>, string, int>((KeyValuePair<string, int> kv) => kv.Key, (KeyValuePair<string, int> kv) => kv.Value);
    }

    // Token: 0x06000525 RID: 1317 RVA: 0x000EFAB4 File Offset: 0x000EFAB4
    public static ClickyWorkShelf.Kind KindOf(ClickyWorkPiece piece, bool isFollowUp)
    {
      if (piece.Failed)
      {
        return ClickyWorkShelf.Kind.QuickAnswer;
      }
      if (piece.ArtifactPaths.Count > 0 || piece.IsRoutineRun)
      {
        return ClickyWorkShelf.Kind.Made;
      }
      int length = piece.Reply.Length;
      if (ClickyWorkShelf.EndsWithQuestionToUser(piece.Reply) && (length < 700 || ClickyWorkShelf.QuestionLineCount(piece.Reply) >= 2))
      {
        return ClickyWorkShelf.Kind.Question;
      }
      if (length >= (isFollowUp ? 1600 : 160))
      {
        return ClickyWorkShelf.Kind.Made;
      }
      return ClickyWorkShelf.Kind.QuickAnswer;
    }

    // Token: 0x06000526 RID: 1318 RVA: 0x000EFB2C File Offset: 0x000EFB2C
    public static string RoutineKey(ClickyWorkPiece piece)
    {
      return Regex.Replace(piece.Ask, "\\s*\\u00B7\\s*run \\d+$", "").ToLowerInvariant();
    }

    // Token: 0x06000527 RID: 1319 RVA: 0x000EFB48 File Offset: 0x000EFB48
    private static List<string> LastLines(string text, int count)
    {
      return (from l in text.Split(RuntimeHelpers.CreateSpan<char>(fieldof(<PrivateImplementationDetails>.FBB68C637180E81FFB4AF91596B53064C159DDF8F56E330BDD31C0DE6472348D2).FieldHandle))
        select l.Trim().Trim(new char[] { '*', '_', ' ' }) into l
        where l.Length > 0
        select l).TakeLast<string>(count).ToList<string>();
    }

    // Token: 0x06000528 RID: 1320 RVA: 0x000EFBB8 File Offset: 0x000EFBB8
    private static bool IsQuestionLine(string line)
    {
      return line.EndsWith('?') || line.EndsWith('？') || line.EndsWith('؟');
    }

    // Token: 0x06000529 RID: 1321 RVA: 0x000EFBE0 File Offset: 0x000EFBE0
    private static bool EndsWithQuestionToUser(string text)
    {
      string text2 = ClickyWorkShelf.LastLines(text, 1).FirstOrDefault<string>();
      return text2 != null && ClickyWorkShelf.IsQuestionLine(text2);
    }

    // Token: 0x0600052A RID: 1322 RVA: 0x000EFC08 File Offset: 0x000EFC08
    private static int QuestionLineCount(string text)
    {
      IEnumerable<string> enumerable = ClickyWorkShelf.LastLines(text, 6);
      Func<string, bool> func;
      if ((func = ClickyWorkShelf.<>O.<0>__IsQuestionLine) == null)
      {
        func = (ClickyWorkShelf.<>O.<0>__IsQuestionLine = new Func<string, bool>(ClickyWorkShelf.IsQuestionLine));
      }
      return enumerable.Count<string>(func);
    }

    // Token: 0x0600052B RID: 1323 RVA: 0x000EFC34 File Offset: 0x000EFC34
    public static string QuestionText(ClickyWorkPiece piece)
    {
      IEnumerable<string> enumerable = ClickyWorkShelf.LastLines(piece.Reply, 6);
      Func<string, bool> func;
      if ((func = ClickyWorkShelf.<>O.<0>__IsQuestionLine) == null)
      {
        func = (ClickyWorkShelf.<>O.<0>__IsQuestionLine = new Func<string, bool>(ClickyWorkShelf.IsQuestionLine));
      }
      IEnumerable<string> enumerable2 = from line in enumerable.Where<string>(func).Take<string>(3)
        select Regex.Replace(Regex.Replace(line, "^([-*•]|\\d+[.)])\\s*", ""), "[*_`]", "");
      string text = string.Join("\n", enumerable2);
      if (text.Length != 0)
      {
        return text;
      }
      return ClickyWorkReplyParts.FirstSentence(piece.Reply, 220);
    }

    // Token: 0x040001E1 RID: 481
    public const int FirstAnswerLength = 160;

    // Token: 0x040001E2 RID: 482
    public const int FullFollowUpAnswerLength = 1600;

    // Token: 0x040001E3 RID: 483
    public const int QuestionMaximumLength = 700;

    // Token: 0x02000145 RID: 325
    [NullableContext(0)]
    public enum Kind
    {
      // Token: 0x040006CC RID: 1740
      Made,
      // Token: 0x040006CD RID: 1741
      QuickAnswer,
      // Token: 0x040006CE RID: 1742
      Question
    }

    // Token: 0x02000146 RID: 326
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040006CF RID: 1743
      [Nullable(0)]
      public static Func<string, bool> <0>__IsQuestionLine;
    }
  }
}
