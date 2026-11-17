using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000053 RID: 83
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyWorkIndex
  {
    // Token: 0x0600074B RID: 1867 RVA: 0x000FB220 File Offset: 0x000FB220
    public static IReadOnlyList<ClickyWorkEntry> Search(IReadOnlyList<ClickyWorkEntry> entries, string query, int limit)
    {
      limit = Math.Max(1, Math.Min(10, limit));
      List<string> list = (from t in ClickyWorkIndex.Tokens(query)
        where !ClickyWorkIndex.StopWords.Contains(t)
        select t).ToList<string>();
      if (list.Count == 0)
      {
        return entries.OrderByDescending<ClickyWorkEntry, DateTimeOffset>((ClickyWorkEntry e) => e.LastActive).Take<ClickyWorkEntry>(limit).ToList<ClickyWorkEntry>();
      }
      List<ValueTuple<ClickyWorkEntry, int>> list2 = new List<ValueTuple<ClickyWorkEntry, int>>();
      foreach (ClickyWorkEntry clickyWorkEntry in entries)
      {
        ValueTuple<IReadOnlyList<string>, int>[] array = new ValueTuple<IReadOnlyList<string>, int>[]
        {
          new ValueTuple<IReadOnlyList<string>, int>(ClickyWorkIndex.Tokens(clickyWorkEntry.Title), 4),
          new ValueTuple<IReadOnlyList<string>, int>(ClickyWorkIndex.Tokens(string.Join(" ", clickyWorkEntry.Asks)), 2),
          new ValueTuple<IReadOnlyList<string>, int>(ClickyWorkIndex.Tokens(clickyWorkEntry.Result ?? ""), 2),
          new ValueTuple<IReadOnlyList<string>, int>(ClickyWorkIndex.Tokens(clickyWorkEntry.ReportText ?? ""), 1)
        };
        int num = 0;
        using (List<string>.Enumerator enumerator2 = list.GetEnumerator())
        {
          while (enumerator2.MoveNext())
          {
            string queryToken = enumerator2.Current;
            Func<string, bool> <>9__5;
            foreach (ValueTuple<IReadOnlyList<string>, int> valueTuple in array)
            {
              IEnumerable<string> item = valueTuple.Item1;
              Func<string, bool> func;
              if ((func = <>9__5) == null)
              {
                func = (<>9__5 = (string word) => ClickyWorkIndex.Matches(word, queryToken));
              }
              if (item.Any<string>(func))
              {
                num += valueTuple.Item2;
              }
            }
          }
        }
        if (num > 0)
        {
          list2.Add(new ValueTuple<ClickyWorkEntry, int>(clickyWorkEntry, num));
        }
      }
      return (from s in (from s in list2
          orderby s.Item2 descending, s.Item1.LastActive descending
          select s).Take<ValueTuple<ClickyWorkEntry, int>>(limit)
        select s.Item1).ToList<ClickyWorkEntry>();
    }

    // Token: 0x0600074C RID: 1868 RVA: 0x000FB4B4 File Offset: 0x000FB4B4
    private static bool Matches(string word, string queryToken)
    {
      return word == queryToken || (queryToken.Length >= 3 && word.StartsWith(queryToken, StringComparison.Ordinal));
    }

    // Token: 0x0600074D RID: 1869 RVA: 0x000FB4D4 File Offset: 0x000FB4D4
    public static IReadOnlyList<string> Tokens(string text)
    {
      string text2 = new string((from c in text.Normalize(NormalizationForm.FormD)
        where CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark
        select c).ToArray<char>()).ToLowerInvariant();
      List<string> list = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      foreach (char c2 in text2)
      {
        if (char.IsLetterOrDigit(c2))
        {
          stringBuilder.Append(c2);
        }
        else if (stringBuilder.Length > 0)
        {
          list.Add(stringBuilder.ToString());
          stringBuilder.Clear();
        }
      }
      if (stringBuilder.Length > 0)
      {
        list.Add(stringBuilder.ToString());
      }
      return list.Where<string>((string w) => w.Length >= 2).ToList<string>();
    }

    // Token: 0x0600074E RID: 1870 RVA: 0x000FB5B4 File Offset: 0x000FB5B4
    public static string DateText(DateTimeOffset date)
    {
      DateTime dateTime;
      try
      {
        dateTime = ((date.Year <= 1) ? date.DateTime : date.LocalDateTime);
      }
      catch (ArgumentOutOfRangeException)
      {
        dateTime = date.DateTime;
      }
      return dateTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
    }

    // Token: 0x0600074F RID: 1871 RVA: 0x000FB60C File Offset: 0x000FB60C
    public static JsonObject ResultObject(ClickyWorkEntry entry, bool forHelper, bool isCallingJob = false)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["job_id"] = entry.Id;
      jsonObject["title"] = entry.Title;
      jsonObject["last_active"] = ClickyWorkIndex.DateText(entry.LastActive);
      jsonObject["state"] = entry.State;
      JsonObject jsonObject2 = jsonObject;
      if (entry.Asks.Count > 0)
      {
        jsonObject2["asked"] = ClickyWorkIndex.OneLine(entry.Asks[0], 300);
      }
      if (entry.Asks.Count > 1)
      {
        JsonNode jsonNode = jsonObject2;
        string text = "latest_ask";
        IReadOnlyList<string> asks = entry.Asks;
        jsonNode[text] = ClickyWorkIndex.OneLine(asks[asks.Count - 1], 300);
      }
      if (entry.Asks.Count > 1)
      {
        jsonObject2["runs"] = entry.Asks.Count;
      }
      string result = entry.Result;
      if (result != null && result.Length > 0)
      {
        jsonObject2["result"] = ClickyWorkIndex.OneLine(result, 300);
      }
      if (entry.IsEarlierClicky)
      {
        jsonObject2["kind"] = "a Clicky from before jobs";
      }
      if (forHelper)
      {
        string folderPath = entry.FolderPath;
        if (folderPath != null)
        {
          jsonObject2["folder"] = folderPath;
        }
      }
      if (isCallingJob)
      {
        jsonObject2["this_job"] = true;
      }
      return jsonObject2;
    }

    // Token: 0x06000750 RID: 1872 RVA: 0x000FB79C File Offset: 0x000FB79C
    public static string OneLine(string text, int limit)
    {
      string text2 = string.Join(" ", from l in text.Split(RuntimeHelpers.CreateSpan<char>(fieldof(<PrivateImplementationDetails>.FBB68C637180E81FFB4AF91596B53064C159DDF8F56E330BDD31C0DE6472348D2).FieldHandle))
        where l.Length > 0
        select l).Trim();
      if (text2.Length > limit)
      {
        return text2.Substring(0, limit - 1).TrimEnd() + "…";
      }
      return text2;
    }

    // Token: 0x06000751 RID: 1873 RVA: 0x000FB814 File Offset: 0x000FB814
    public static string ReportHeader(string title, string jobId)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(106, 2);
      defaultInterpolatedStringHandler.AppendLiteral("# ");
      defaultInterpolatedStringHandler.AppendFormatted(title);
      defaultInterpolatedStringHandler.AppendLiteral("\n\nHeyClicky's record of this job (job_id ");
      defaultInterpolatedStringHandler.AppendFormatted(jobId);
      defaultInterpolatedStringHandler.AppendLiteral("): what each run was asked and what it reported, oldest first.\n");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06000752 RID: 1874 RVA: 0x000FB868 File Offset: 0x000FB868
    public static string ReportOutcomeText(ClickyJobOutcome outcome)
    {
      string text;
      switch (outcome)
      {
      case ClickyJobOutcome.Done:
        text = "done";
        break;
      case ClickyJobOutcome.Failed:
        text = "failed";
        break;
      case ClickyJobOutcome.Stopped:
        text = "stopped by the user";
        break;
      default:
        text = "working";
        break;
      }
      return text;
    }

    // Token: 0x06000753 RID: 1875 RVA: 0x000FB8AC File Offset: 0x000FB8AC
    public static string ReportSection(DateTimeOffset finishedAt, string outcome, string ask, string reply, [Nullable(2)] string summary, IReadOnlyList<string> files, string folderPath)
    {
      List<string> list = new List<string>
      {
        "## " + ClickyWorkIndex.DateText(finishedAt) + " · " + outcome,
        "",
        "Asked: " + ClickyWorkIndex.OneLine(ask, 2000),
        ""
      };
      string text = reply.Trim();
      if (text.Length > 0)
      {
        list.Add(ClickyWorkIndex.HeadingsBelowRuns(text));
      }
      else if (summary != null && summary.Length > 0)
      {
        list.Add(summary);
      }
      else
      {
        list.Add("(no report)");
      }
      if (files.Count > 0)
      {
        string text2 = folderPath.TrimEnd(new char[] { '\\', '/' });
        list.Add("");
        list.Add("Files:");
        foreach (string text3 in files)
        {
          string text4 = text3;
          foreach (string text5 in new string[]
          {
            text2 + "\\",
            text2 + "/"
          })
          {
            if (text3.StartsWith(text5, StringComparison.OrdinalIgnoreCase))
            {
              string text6 = text3;
              int length = text5.Length;
              text4 = text6.Substring(length, text6.Length - length);
              break;
            }
          }
          list.Add("- " + text4);
        }
      }
      return string.Join("\n", list) + "\n";
    }

    // Token: 0x06000754 RID: 1876 RVA: 0x000FBA60 File Offset: 0x000FBA60
    public static bool IsRunHeading(string line)
    {
      return ClickyWorkIndex.RunHeadingPattern.IsMatch(line);
    }

    // Token: 0x06000755 RID: 1877 RVA: 0x000FBA70 File Offset: 0x000FBA70
    public static string HeadingsBelowRuns(string reply)
    {
      bool inCode = false;
      return string.Join("\n", reply.Split('\n', StringSplitOptions.None).Select<string, string>(delegate(string line)
      {
        string text = line.Trim(new char[] { ' ', '\t' });
        if (text.StartsWith("```", StringComparison.Ordinal) || text.StartsWith("~~~", StringComparison.Ordinal))
        {
          inCode = !inCode;
          return line;
        }
        if (inCode || !line.StartsWith('#') || !ClickyWorkIndex.ReplyHeadingPattern.IsMatch(line))
        {
          return line;
        }
        return "##" + line;
      }));
    }

    // Token: 0x06000756 RID: 1878 RVA: 0x000FBAB0 File Offset: 0x000FBAB0
    [return: TupleElementNames(new string[] { "Title", "Runs" })]
    [return: Nullable(new byte[] { 0, 2, 1, 1 })]
    public static ValueTuple<string, IReadOnlyList<ClickyWorkIndex.ReportRun>> ParseReport(string text)
    {
      string text2 = null;
      List<ClickyWorkIndex.ReportRun> list = new List<ClickyWorkIndex.ReportRun>();
      foreach (string text3 in text.Split('\n', StringSplitOptions.None))
      {
        if (text2 == null && text3.StartsWith("# ", StringComparison.Ordinal))
        {
          string text4 = text3;
          text2 = text4.Substring(2, text4.Length - 2).Trim(new char[] { ' ', '\t' });
        }
        else if (ClickyWorkIndex.IsRunHeading(text3))
        {
          List<ClickyWorkIndex.ReportRun> list2 = list;
          string text4 = text3;
          list2.Add(new ClickyWorkIndex.ReportRun(text4.Substring(3, text4.Length - 3).Trim(new char[] { ' ', '\t' }), "", ""));
        }
        else if (list.Count > 0)
        {
          string text5 = text3.Trim(new char[] { ' ', '\t' });
          List<ClickyWorkIndex.ReportRun> list3 = list;
          ClickyWorkIndex.ReportRun reportRun = list3[list3.Count - 1];
          if (reportRun.Ask.Length == 0 && text5.StartsWith("Asked: ", StringComparison.Ordinal))
          {
            List<ClickyWorkIndex.ReportRun> list4 = list;
            int num = list4.Count - 1;
            ClickyWorkIndex.ReportRun reportRun2 = reportRun.<Clone>$();
            string text4 = text5;
            reportRun2.Ask = text4.Substring(7, text4.Length - 7);
            list4[num] = reportRun2;
          }
          else if (reportRun.Ask.Length > 0 && reportRun.FirstLine.Length == 0 && text5.Length > 0)
          {
            List<ClickyWorkIndex.ReportRun> list5 = list;
            int num2 = list5.Count - 1;
            ClickyWorkIndex.ReportRun reportRun3 = reportRun.<Clone>$();
            reportRun3.FirstLine = text5;
            list5[num2] = reportRun3;
          }
        }
      }
      return new ValueTuple<string, IReadOnlyList<ClickyWorkIndex.ReportRun>>(text2, list);
    }

    // Token: 0x06000757 RID: 1879 RVA: 0x000FBC48 File Offset: 0x000FBC48
    public unsafe static string TrimmedReport(string text, int characterLimit)
    {
      if (text.Length <= characterLimit)
      {
        return text;
      }
      List<string> list = new List<string>();
      List<List<string>> list2 = new List<List<string>>();
      foreach (string text2 in text.Split('\n', StringSplitOptions.None))
      {
        if (ClickyWorkIndex.IsRunHeading(text2))
        {
          List<List<string>> list3 = list2;
          int num = 1;
          List<string> list4 = new List<string>(num);
          CollectionsMarshal.SetCount<string>(list4, num);
          Span<string> span = CollectionsMarshal.AsSpan<string>(list4);
          int num2 = 0;
          *span[num2] = text2;
          list3.Add(list4);
        }
        else if (list2.Count == 0)
        {
          list.Add(text2);
        }
        else
        {
          List<List<string>> list5 = list2;
          list5[list5.Count - 1].Add(text2);
        }
      }
      string text3 = string.Join("\n", list);
      List<string> list6 = list2.Select<List<string>, string>((List<string> r) => string.Join("\n", r)).ToList<string>();
      List<string> list7 = new List<string>();
      int num3 = text3.Length;
      int num4 = list6.Count - 1;
      while (num4 >= 0 && num3 + list6[num4].Length + 1 <= characterLimit)
      {
        list7.Insert(0, list6[num4]);
        num3 += list6[num4].Length + 1;
        num4--;
      }
      int num5 = list6.Count - list7.Count;
      string text4;
      if (num5 <= 0)
      {
        text4 = "";
      }
      else
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 2);
        defaultInterpolatedStringHandler.AppendLiteral("\n(");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num5);
        defaultInterpolatedStringHandler.AppendLiteral(" older run");
        defaultInterpolatedStringHandler.AppendFormatted((num5 == 1) ? "" : "s");
        defaultInterpolatedStringHandler.AppendLiteral(" left out)\n");
        text4 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      string text5 = text4;
      return text3 + text5 + string.Concat(list7.Select<string, string>((string r) => "\n" + r));
    }

    // Token: 0x06000758 RID: 1880 RVA: 0x000FBE30 File Offset: 0x000FBE30
    public static string JobIdFromFolderName(string folderName)
    {
      string text = folderName.Split('-', StringSplitOptions.RemoveEmptyEntries).LastOrDefault<string>() ?? folderName;
      bool flag;
      if (text.Length == 10)
      {
        flag = text.All<char>(delegate(char c)
        {
          if (c >= 'a')
          {
            if (c > 'f')
            {
              goto IL_001A;
            }
          }
          else if (c < '0' || c > '9')
          {
            goto IL_001A;
          }
          return true;
          IL_001A:
          return false;
        });
      }
      else
      {
        flag = false;
      }
      if (!flag)
      {
        return folderName;
      }
      return text;
    }

    // Token: 0x06000759 RID: 1881 RVA: 0x000FBE88 File Offset: 0x000FBE88
    public static string TitleFromFolderName(string folderName)
    {
      bool flag = ClickyWorkIndex.JobIdFromFolderName(folderName) != folderName;
      List<string> list = folderName.Split('-', StringSplitOptions.RemoveEmptyEntries).ToList<string>();
      if (flag && list.Count > 0)
      {
        list.RemoveAt(list.Count - 1);
      }
      string text = string.Join(" ", list);
      if (text.Length != 0)
      {
        char c = char.ToUpperInvariant(text[0]);
        ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c);
        string text2 = text;
        return readOnlySpan + text2.Substring(1, text2.Length - 1);
      }
      return folderName;
    }

    // Token: 0x0600075A RID: 1882 RVA: 0x000FBF10 File Offset: 0x000FBF10
    [NullableContext(2)]
    public static string OwnerStamp(string accountId)
    {
      if (string.IsNullOrEmpty(accountId))
      {
        return null;
      }
      return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes("clicky-work-owner:" + accountId)), 0, 12).ToLowerInvariant();
    }

    // Token: 0x0600075B RID: 1883 RVA: 0x000FBF44 File Offset: 0x000FBF44
    [return: Nullable(2)]
    public static string FolderOwnerStamp(string folderPath)
    {
      string text2;
      try
      {
        string text = Path.Combine(folderPath, ClickyWorkIndex.OwnerFileRelativePath);
        text2 = (File.Exists(text) ? File.ReadAllText(text).Trim() : null);
      }
      catch (Exception)
      {
        text2 = null;
      }
      return text2;
    }

    // Token: 0x0600075C RID: 1884 RVA: 0x000FBF8C File Offset: 0x000FBF8C
    public static void StampFolder(string folderPath, string ownerStamp)
    {
      if (ClickyWorkIndex.FolderOwnerStamp(folderPath) == ownerStamp)
      {
        return;
      }
      try
      {
        string text = Path.Combine(folderPath, ClickyWorkIndex.OwnerFileRelativePath);
        Directory.CreateDirectory(Path.GetDirectoryName(text));
        File.WriteAllText(text, ownerStamp, new UTF8Encoding(false));
      }
      catch (Exception ex)
      {
        Log.Warn("Jobs", "owner stamp not written: " + ex.Message);
      }
    }

    // Token: 0x0600075D RID: 1885 RVA: 0x000FBFFC File Offset: 0x000FBFFC
    public static void RemoveReports(string ownerStamp, string jobsRootPath)
    {
      foreach (string text in ClickyWorkIndex.SafeDirectories(jobsRootPath))
      {
        if (!Path.GetFileName(text).StartsWith('.') && !(ClickyWorkIndex.FolderOwnerStamp(text) != ownerStamp))
        {
          try
          {
            File.Delete(Path.Combine(text, "report.md"));
          }
          catch (Exception)
          {
          }
          try
          {
            File.Delete(Path.Combine(text, ClickyWorkIndex.OwnerFileRelativePath));
          }
          catch (Exception)
          {
          }
        }
      }
    }

    // Token: 0x0600075E RID: 1886 RVA: 0x000FC0A4 File Offset: 0x000FC0A4
    public static string StandardizedPath(string path)
    {
      string text;
      try
      {
        text = Path.GetFullPath(path).TrimEnd(new char[] { '\\', '/' });
      }
      catch (Exception)
      {
        text = path.TrimEnd(new char[] { '\\', '/' });
      }
      return text;
    }

    // Token: 0x0600075F RID: 1887 RVA: 0x000FC0FC File Offset: 0x000FC0FC
    public static IReadOnlyList<ClickyWorkEntry> UnlistedFolderEntries(string jobsRootPath, IEnumerable<string> listedFolderPaths, [Nullable(2)] string ownerStamp)
    {
      if (ownerStamp == null)
      {
        return Array.Empty<ClickyWorkEntry>();
      }
      Func<string, string> func;
      if ((func = ClickyWorkIndex.<>O.<0>__StandardizedPath) == null)
      {
        func = (ClickyWorkIndex.<>O.<0>__StandardizedPath = new Func<string, string>(ClickyWorkIndex.StandardizedPath));
      }
      HashSet<string> hashSet = listedFolderPaths.Select<string, string>(func).ToHashSet<string>(StringComparer.OrdinalIgnoreCase);
      List<ClickyWorkEntry> list = new List<ClickyWorkEntry>();
      foreach (string text in ClickyWorkIndex.SafeDirectories(jobsRootPath))
      {
        string fileName = Path.GetFileName(text);
        if (!fileName.StartsWith('.') && !hashSet.Contains(ClickyWorkIndex.StandardizedPath(text)) && !(ClickyWorkIndex.FolderOwnerStamp(text) != ownerStamp))
        {
          string text2 = Path.Combine(text, "report.md");
          string text3 = null;
          try
          {
            if (File.Exists(text2))
            {
              text3 = File.ReadAllText(text2);
            }
          }
          catch (Exception)
          {
          }
          DateTimeOffset dateTimeOffset;
          try
          {
            dateTimeOffset = ((text3 == null) ? Directory.GetLastWriteTime(text) : File.GetLastWriteTime(text2));
          }
          catch (Exception)
          {
            dateTimeOffset = DateTimeOffset.MinValue;
          }
          ValueTuple<string, IReadOnlyList<ClickyWorkIndex.ReportRun>> valueTuple = ((text3 == null) ? new ValueTuple<string, IReadOnlyList<ClickyWorkIndex.ReportRun>>(null, Array.Empty<ClickyWorkIndex.ReportRun>()) : ClickyWorkIndex.ParseReport(text3));
          ClickyWorkIndex.ReportRun reportRun = valueTuple.Item2.LastOrDefault<ClickyWorkIndex.ReportRun>();
          List<ClickyWorkEntry> list2 = list;
          ClickyWorkEntry clickyWorkEntry = new ClickyWorkEntry();
          clickyWorkEntry.Id = ClickyWorkIndex.JobIdFromFolderName(fileName);
          clickyWorkEntry.Title = valueTuple.Item1 ?? ClickyWorkIndex.TitleFromFolderName(fileName);
          clickyWorkEntry.LastActive = dateTimeOffset;
          clickyWorkEntry.Asks = (from r in valueTuple.Item2
            select r.Ask into a
            where a.Length > 0
            select a).ToList<string>();
          clickyWorkEntry.Result = ((reportRun != null) ? reportRun.FirstLine : null);
          clickyWorkEntry.State = ((reportRun == null) ? "no record" : (reportRun.Heading.Split('·', StringSplitOptions.RemoveEmptyEntries).LastOrDefault<string>() ?? "").Trim());
          clickyWorkEntry.FolderPath = text;
          clickyWorkEntry.ReportText = text3;
          list2.Add(clickyWorkEntry);
        }
      }
      return list;
    }

    // Token: 0x06000760 RID: 1888 RVA: 0x000FC35C File Offset: 0x000FC35C
    public static IReadOnlyList<ClickyWorkEntry> WithReports(IReadOnlyList<ClickyWorkEntry> entries)
    {
      return entries.Select<ClickyWorkEntry, ClickyWorkEntry>(delegate(ClickyWorkEntry entry)
      {
        if (entry.ReportText == null)
        {
          string folderPath = entry.FolderPath;
          if (folderPath != null)
          {
            ClickyWorkEntry clickyWorkEntry2;
            try
            {
              string text = Path.Combine(folderPath, "report.md");
              ClickyWorkEntry clickyWorkEntry;
              if (!File.Exists(text))
              {
                clickyWorkEntry = entry;
              }
              else
              {
                (clickyWorkEntry = entry.<Clone>$()).ReportText = File.ReadAllText(text);
              }
              clickyWorkEntry2 = clickyWorkEntry;
            }
            catch (Exception)
            {
              clickyWorkEntry2 = entry;
            }
            return clickyWorkEntry2;
          }
        }
        return entry;
      }).ToList<ClickyWorkEntry>();
    }

    // Token: 0x06000761 RID: 1889 RVA: 0x000FC388 File Offset: 0x000FC388
    private static IEnumerable<string> SafeDirectories(string root)
    {
      IEnumerable<string> enumerable;
      try
      {
        enumerable = (Directory.Exists(root) ? Directory.GetDirectories(root) : Array.Empty<string>());
      }
      catch (Exception)
      {
        enumerable = Array.Empty<string>();
      }
      return enumerable;
    }

    // Token: 0x040002AA RID: 682
    public const string ReportFileName = "report.md";

    // Token: 0x040002AB RID: 683
    public const int DefaultLimit = 5;

    // Token: 0x040002AC RID: 684
    public const int MaximumLimit = 10;

    // Token: 0x040002AD RID: 685
    private static readonly HashSet<string> StopWords = new HashSet<string>
    {
      "the", "an", "and", "or", "of", "to", "for", "in", "on", "at",
      "by", "about", "with", "from", "my", "me", "it", "that", "this", "what",
      "which", "did", "do", "you", "your", "was", "were", "is", "job", "jobs",
      "work", "thing"
    };

    // Token: 0x040002AE RID: 686
    private static readonly Regex RunHeadingPattern = new Regex("^## [0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2} · ", RegexOptions.CultureInvariant);

    // Token: 0x040002AF RID: 687
    private static readonly Regex ReplyHeadingPattern = new Regex("^#{1,4} ", RegexOptions.CultureInvariant);

    // Token: 0x040002B0 RID: 688
    public static readonly string OwnerFileRelativePath = Path.Combine(".clicky", "owner");

    // Token: 0x02000195 RID: 405
    [Nullable(0)]
    public sealed class ReportRun : IEquatable<ClickyWorkIndex.ReportRun>
    {
      // Token: 0x06000DE6 RID: 3558 RVA: 0x00116624 File Offset: 0x00116624
      public ReportRun(string Heading, string Ask, string FirstLine)
      {
        this.Heading = Heading;
        this.Ask = Ask;
        this.FirstLine = FirstLine;
        base..ctor();
      }

      // Token: 0x170002D1 RID: 721
      // (get) Token: 0x06000DE7 RID: 3559 RVA: 0x00116644 File Offset: 0x00116644
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyWorkIndex.ReportRun);
        }
      }

      // Token: 0x170002D2 RID: 722
      // (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00116650 File Offset: 0x00116650
      // (set) Token: 0x06000DE9 RID: 3561 RVA: 0x00116658 File Offset: 0x00116658
      public string Heading { get; set; }

      // Token: 0x170002D3 RID: 723
      // (get) Token: 0x06000DEA RID: 3562 RVA: 0x00116664 File Offset: 0x00116664
      // (set) Token: 0x06000DEB RID: 3563 RVA: 0x0011666C File Offset: 0x0011666C
      public string Ask { get; set; }

      // Token: 0x170002D4 RID: 724
      // (get) Token: 0x06000DEC RID: 3564 RVA: 0x00116678 File Offset: 0x00116678
      // (set) Token: 0x06000DED RID: 3565 RVA: 0x00116680 File Offset: 0x00116680
      public string FirstLine { get; set; }

      // Token: 0x06000DEE RID: 3566 RVA: 0x0011668C File Offset: 0x0011668C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ReportRun");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000DEF RID: 3567 RVA: 0x001166D8 File Offset: 0x001166D8
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Heading = ");
        builder.Append(this.Heading);
        builder.Append(", Ask = ");
        builder.Append(this.Ask);
        builder.Append(", FirstLine = ");
        builder.Append(this.FirstLine);
        return true;
      }

      // Token: 0x06000DF0 RID: 3568 RVA: 0x00116738 File Offset: 0x00116738
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyWorkIndex.ReportRun left, ClickyWorkIndex.ReportRun right)
      {
        return !(left == right);
      }

      // Token: 0x06000DF1 RID: 3569 RVA: 0x00116744 File Offset: 0x00116744
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyWorkIndex.ReportRun left, ClickyWorkIndex.ReportRun right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000DF2 RID: 3570 RVA: 0x00116758 File Offset: 0x00116758
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Heading>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Ask>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FirstLine>k__BackingField);
      }

      // Token: 0x06000DF3 RID: 3571 RVA: 0x001167BC File Offset: 0x001167BC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyWorkIndex.ReportRun);
      }

      // Token: 0x06000DF4 RID: 3572 RVA: 0x001167CC File Offset: 0x001167CC
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyWorkIndex.ReportRun other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Heading>k__BackingField, other.<Heading>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Ask>k__BackingField, other.<Ask>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FirstLine>k__BackingField, other.<FirstLine>k__BackingField));
      }

      // Token: 0x06000DF6 RID: 3574 RVA: 0x00116848 File Offset: 0x00116848
      [CompilerGenerated]
      private ReportRun(ClickyWorkIndex.ReportRun original)
      {
        this.Heading = original.<Heading>k__BackingField;
        this.Ask = original.<Ask>k__BackingField;
        this.FirstLine = original.<FirstLine>k__BackingField;
      }

      // Token: 0x06000DF7 RID: 3575 RVA: 0x00116874 File Offset: 0x00116874
      [CompilerGenerated]
      public void Deconstruct(out string Heading, out string Ask, out string FirstLine)
      {
        Heading = this.Heading;
        Ask = this.Ask;
        FirstLine = this.FirstLine;
      }
    }

    // Token: 0x02000196 RID: 406
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040007BB RID: 1979
      [Nullable(0)]
      public static Func<string, string> <0>__StandardizedPath;
    }
  }
}
