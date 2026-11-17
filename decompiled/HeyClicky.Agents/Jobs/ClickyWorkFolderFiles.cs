using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000051 RID: 81
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyWorkFolderFiles
  {
    // Token: 0x170001D1 RID: 465
    // (get) Token: 0x06000716 RID: 1814 RVA: 0x000FA278 File Offset: 0x000FA278
    // (set) Token: 0x06000717 RID: 1815 RVA: 0x000FA280 File Offset: 0x000FA280
    public bool IsBackfillingReports { get; set; }

    // Token: 0x170001D2 RID: 466
    // (get) Token: 0x06000718 RID: 1816 RVA: 0x000FA28C File Offset: 0x000FA28C
    // (set) Token: 0x06000719 RID: 1817 RVA: 0x000FA2D0 File Offset: 0x000FA2D0
    [Nullable(2)]
    public string OwnerStamp
    {
      [NullableContext(2)]
      get
      {
        object obj = this.gate;
        string text;
        lock (obj)
        {
          text = this.ownerStamp;
        }
        return text;
      }
      [NullableContext(2)]
      set
      {
        object obj = this.gate;
        lock (obj)
        {
          this.ownerStamp = value;
        }
      }
    }

    // Token: 0x0600071A RID: 1818 RVA: 0x000FA314 File Offset: 0x000FA314
    private void Enqueue(Action work, string what)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.writes = this.writes.ContinueWith(delegate(Task _)
        {
          try
          {
            work();
          }
          catch (Exception ex)
          {
            Log.Warn("Jobs", what + ": " + ex.Message);
          }
        }, TaskScheduler.Default);
      }
    }

    // Token: 0x0600071B RID: 1819 RVA: 0x000FA384 File Offset: 0x000FA384
    public Task WhenWritten()
    {
      object obj = this.gate;
      Task task;
      lock (obj)
      {
        task = this.writes;
      }
      return task;
    }

    // Token: 0x0600071C RID: 1820 RVA: 0x000FA3C8 File Offset: 0x000FA3C8
    public void Refresh(IReadOnlyList<ClickyJob> jobs, bool force = false)
    {
      foreach (IGrouping<string, ClickyJob> grouping in jobs.GroupBy<ClickyJob, string>((ClickyJob j) => Path.Combine(Path.GetDirectoryName(j.FolderPath.TrimEnd(new char[] { '\\', '/' })) ?? j.FolderPath, "WORK.md"), StringComparer.OrdinalIgnoreCase))
      {
        string indexPath = grouping.Key;
        List<ClickyJob> list = grouping.ToList<ClickyJob>();
        IReadOnlyList<ValueTuple<DateTimeOffset, string>> listedLines = ClickyWorkFolderFiles.IndexLines(list);
        string text = string.Join("\n", listedLines.Select<ValueTuple<DateTimeOffset, string>, string>(([TupleElementNames(new string[] { "Date", "Line" })] ValueTuple<DateTimeOffset, string> l) => l.Item2));
        object obj = this.gate;
        string stamp;
        lock (obj)
        {
          string text2;
          if (!force && this.lastIndexLinesByPath.TryGetValue(indexPath, out text2) && text2 == text)
          {
            continue;
          }
          this.lastIndexLinesByPath[indexPath] = text;
          stamp = this.ownerStamp;
        }
        List<string> listedFolders = list.Select<ClickyJob, string>((ClickyJob j) => j.FolderPath).ToList<string>();
        this.Enqueue(delegate
        {
          string directoryName = Path.GetDirectoryName(indexPath);
          IEnumerable<ValueTuple<DateTimeOffset, string>> enumerable = from entry in ClickyWorkIndex.UnlistedFolderEntries(directoryName, listedFolders, stamp)
            select new ValueTuple<DateTimeOffset, string>(entry.LastActive, ClickyWorkFolderFiles.IndexLine(entry));
          string text3 = ClickyWorkFolderFiles.IndexText(listedLines.Concat<ValueTuple<DateTimeOffset, string>>(enumerable).ToList<ValueTuple<DateTimeOffset, string>>());
          if (File.Exists(indexPath) && File.ReadAllText(indexPath) == text3)
          {
            return;
          }
          Directory.CreateDirectory(directoryName);
          File.WriteAllText(indexPath, text3, new UTF8Encoding(false));
        }, "WORK.md not written");
      }
    }

    // Token: 0x0600071D RID: 1821 RVA: 0x000FA564 File Offset: 0x000FA564
    public bool ClaimReport(string jobId, string turnKey)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = this.reportedTurnKeys.Add(jobId + "|" + turnKey);
      }
      return flag2;
    }

    // Token: 0x0600071E RID: 1822 RVA: 0x000FA5B8 File Offset: 0x000FA5B8
    public void AppendReportSection(string section, string title, string jobId, string folderPath)
    {
      string reportPath = Path.Combine(folderPath, "report.md");
      string header = ClickyWorkIndex.ReportHeader(title, jobId);
      string stamp = this.OwnerStamp;
      this.Enqueue(delegate
      {
        string text = (File.Exists(reportPath) ? File.ReadAllText(reportPath) : "");
        string text2 = ((text.Length == 0) ? (header + "\n" + section) : (text + (text.EndsWith('\n') ? "\n" : "\n\n") + section));
        Directory.CreateDirectory(folderPath);
        File.WriteAllText(reportPath, text2, new UTF8Encoding(false));
        if (stamp != null)
        {
          ClickyWorkIndex.StampFolder(folderPath, stamp);
        }
      }, "report.md not written");
    }

    // Token: 0x0600071F RID: 1823 RVA: 0x000FA620 File Offset: 0x000FA620
    public void WriteReportIfMissing(string text, string folderPath)
    {
      string reportPath = Path.Combine(folderPath, "report.md");
      string stamp = this.OwnerStamp;
      this.Enqueue(delegate
      {
        if (stamp != null)
        {
          ClickyWorkIndex.StampFolder(folderPath, stamp);
        }
        if (File.Exists(reportPath))
        {
          return;
        }
        File.WriteAllText(reportPath, text, new UTF8Encoding(false));
      }, "report.md not written");
    }

    // Token: 0x06000720 RID: 1824 RVA: 0x000FA67C File Offset: 0x000FA67C
    public void StampFolders(IReadOnlyList<string> folderPaths)
    {
      string stamp = this.OwnerStamp;
      if (stamp == null)
      {
        return;
      }
      this.Enqueue(delegate
      {
        IEnumerable<string> folderPaths2 = folderPaths;
        Func<string, bool> func;
        if ((func = ClickyWorkFolderFiles.<>O.<0>__Exists) == null)
        {
          func = (ClickyWorkFolderFiles.<>O.<0>__Exists = new Func<string, bool>(Directory.Exists));
        }
        foreach (string text in folderPaths2.Where<string>(func))
        {
          ClickyWorkIndex.StampFolder(text, stamp);
        }
      }, "job folders not stamped");
    }

    // Token: 0x06000721 RID: 1825 RVA: 0x000FA6C4 File Offset: 0x000FA6C4
    public void ResetForSignOut(string jobsRootPath)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.ownerStamp = null;
        this.lastIndexLinesByPath.Clear();
        this.reportedTurnKeys.Clear();
      }
      string indexPath = Path.Combine(jobsRootPath, "WORK.md");
      this.Enqueue(delegate
      {
        if (File.Exists(indexPath))
        {
          File.Delete(indexPath);
        }
      }, "WORK.md not removed");
    }

    // Token: 0x06000722 RID: 1826 RVA: 0x000FA74C File Offset: 0x000FA74C
    public void RemoveReports([Nullable(2)] string deletedAccountId, string jobsRootPath)
    {
      string stamp = ClickyWorkIndex.OwnerStamp(deletedAccountId);
      if (stamp == null)
      {
        return;
      }
      this.Enqueue(delegate
      {
        ClickyWorkIndex.RemoveReports(stamp, jobsRootPath);
      }, "reports not removed");
    }

    // Token: 0x06000723 RID: 1827 RVA: 0x000FA794 File Offset: 0x000FA794
    public static bool RetireNote(string folderPath)
    {
      string text = Path.Combine(folderPath, "note.md");
      string text2;
      try
      {
        if (!File.Exists(text))
        {
          return false;
        }
        text2 = File.ReadAllText(text);
      }
      catch (Exception)
      {
        return false;
      }
      try
      {
        File.Delete(text);
      }
      catch (Exception)
      {
      }
      if (text2.Trim().Length == 0)
      {
        return false;
      }
      string text3 = Path.Combine(folderPath, ClickyWorkFolderFiles.PreviousNoteRelativePath);
      try
      {
        Directory.CreateDirectory(Path.GetDirectoryName(text3));
        File.WriteAllText(text3, text2, new UTF8Encoding(false));
      }
      catch (Exception ex)
      {
        Log.Warn("Jobs", "previous-note.md not written: " + ex.Message);
      }
      return true;
    }

    // Token: 0x06000724 RID: 1828 RVA: 0x000FA854 File Offset: 0x000FA854
    public static string FolderSlug(string title)
    {
      string text = new string((from c in title.Normalize(NormalizationForm.FormD)
        where CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark
        select c).ToArray<char>()).ToLowerInvariant();
      List<string> list = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      foreach (char c2 in text)
      {
        if (c2 < '\u0080' && char.IsLetterOrDigit(c2))
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
      string text3 = "";
      foreach (string text4 in list)
      {
        string text5 = ((text3.Length == 0) ? text4 : (text3 + "-" + text4));
        if (text5.Length > 40)
        {
          break;
        }
        text3 = text5;
      }
      return text3;
    }

    // Token: 0x06000725 RID: 1829 RVA: 0x000FA984 File Offset: 0x000FA984
    private static string RunDate(DateTimeOffset date)
    {
      return date.LocalDateTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
    }

    // Token: 0x06000726 RID: 1830 RVA: 0x000FA9AC File Offset: 0x000FA9AC
    private static string OneLine(string text, int limit)
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

    // Token: 0x06000727 RID: 1831 RVA: 0x000FAA24 File Offset: 0x000FAA24
    private static string OutcomeText(ClickyJobOutcome outcome, [Nullable(2)] string summary)
    {
      string text = ((summary == null) ? "" : (": " + ClickyWorkFolderFiles.OneLine(summary, 220)));
      string text2;
      switch (outcome)
      {
      case ClickyJobOutcome.Working:
        text2 = "working now";
        break;
      case ClickyJobOutcome.Done:
        text2 = "done" + text;
        break;
      case ClickyJobOutcome.Failed:
        text2 = "failed" + text;
        break;
      default:
        text2 = "stopped by the user" + text;
        break;
      }
      return text2;
    }

    // Token: 0x06000728 RID: 1832 RVA: 0x000FAA98 File Offset: 0x000FAA98
    public static string RunsBlock(ClickyJob job)
    {
      IEnumerable<string> enumerable = (job.Runs ?? Array.Empty<ClickyJobRun>()).Select<ClickyJobRun, string>(delegate(ClickyJobRun run)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 2);
        defaultInterpolatedStringHandler.AppendLiteral("- ");
        defaultInterpolatedStringHandler.AppendFormatted(ClickyWorkFolderFiles.RunDate(run.StartedAt));
        defaultInterpolatedStringHandler.AppendLiteral(" · asked: \"");
        defaultInterpolatedStringHandler.AppendFormatted(ClickyWorkFolderFiles.OneLine(run.Ask, 240));
        defaultInterpolatedStringHandler.AppendLiteral("\" · ");
        return defaultInterpolatedStringHandler.ToStringAndClear() + ClickyWorkFolderFiles.OutcomeText(run.Outcome, run.Summary);
      });
      return string.Join("\n", new string[] { "<!-- clicky:runs -->", "## Runs (HeyClicky keeps this list, newest last)" }.Concat<string>(enumerable).Append("<!-- /clicky:runs -->"));
    }

    // Token: 0x06000729 RID: 1833 RVA: 0x000FAB0C File Offset: 0x000FAB0C
    [return: TupleElementNames(new string[] { "Date", "Line" })]
    [return: Nullable(new byte[] { 1, 0, 1 })]
    internal static IReadOnlyList<ValueTuple<DateTimeOffset, string>> IndexLines(IReadOnlyList<ClickyJob> jobs)
    {
      return jobs.Select<ClickyJob, ValueTuple<DateTimeOffset, string>>(delegate(ClickyJob job)
      {
        string fileName = Path.GetFileName(job.FolderPath.TrimEnd(new char[] { '\\', '/' }));
        DateTimeOffset? finishedAt = job.FinishedAt;
        DateTimeOffset dateTimeOffset2;
        if (finishedAt == null)
        {
          IReadOnlyList<ClickyJobRun> runs = job.Runs;
          DateTimeOffset? dateTimeOffset;
          if (runs == null)
          {
            dateTimeOffset = null;
          }
          else
          {
            ClickyJobRun clickyJobRun = runs.LastOrDefault<ClickyJobRun>();
            dateTimeOffset = ((clickyJobRun != null) ? new DateTimeOffset?(clickyJobRun.StartedAt) : null);
          }
          dateTimeOffset2 = dateTimeOffset ?? job.CreatedAt;
        }
        else
        {
          dateTimeOffset2 = finishedAt.GetValueOrDefault();
        }
        DateTimeOffset dateTimeOffset3 = dateTimeOffset2;
        IReadOnlyList<ClickyJobRun> runs2 = job.Runs;
        string text;
        if (runs2 == null)
        {
          text = null;
        }
        else
        {
          ClickyJobRun clickyJobRun2 = runs2.LastOrDefault<ClickyJobRun>();
          text = ((clickyJobRun2 != null) ? clickyJobRun2.Ask : null);
        }
        string text2 = text ?? job.Prompt;
        DateTimeOffset dateTimeOffset4 = dateTimeOffset3;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 4);
        defaultInterpolatedStringHandler.AppendLiteral("- ");
        defaultInterpolatedStringHandler.AppendFormatted(ClickyWorkFolderFiles.RunDate(dateTimeOffset3));
        defaultInterpolatedStringHandler.AppendLiteral(" · ");
        defaultInterpolatedStringHandler.AppendFormatted(ClickyWorkFolderFiles.OneLine(job.Title, 80));
        defaultInterpolatedStringHandler.AppendLiteral(" · ");
        defaultInterpolatedStringHandler.AppendFormatted(fileName);
        defaultInterpolatedStringHandler.AppendLiteral("/ · ");
        defaultInterpolatedStringHandler.AppendLiteral("asked: \"");
        defaultInterpolatedStringHandler.AppendFormatted(ClickyWorkFolderFiles.OneLine(text2, 160));
        defaultInterpolatedStringHandler.AppendLiteral("\" · ");
        return new ValueTuple<DateTimeOffset, string>(dateTimeOffset4, defaultInterpolatedStringHandler.ToStringAndClear() + ClickyWorkFolderFiles.OutcomeText(job.Outcome, job.Summary));
      }).ToList<ValueTuple<DateTimeOffset, string>>();
    }

    // Token: 0x0600072A RID: 1834 RVA: 0x000FAB38 File Offset: 0x000FAB38
    internal static string IndexLine(ClickyWorkEntry entry)
    {
      string fileName = Path.GetFileName((entry.FolderPath ?? entry.Id).TrimEnd(new char[] { '\\', '/' }));
      string text;
      if (entry.Asks.Count <= 0)
      {
        text = "";
      }
      else
      {
        string text2 = " · asked: \"";
        IReadOnlyList<string> asks = entry.Asks;
        text = text2 + ClickyWorkIndex.OneLine(asks[asks.Count - 1], 160) + "\"";
      }
      string text3 = text;
      string result = entry.Result;
      string text4 = ((result != null) ? (": " + ClickyWorkIndex.OneLine(result, 220)) : "");
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 6);
      defaultInterpolatedStringHandler.AppendLiteral("- ");
      defaultInterpolatedStringHandler.AppendFormatted(ClickyWorkIndex.DateText(entry.LastActive));
      defaultInterpolatedStringHandler.AppendLiteral(" · ");
      defaultInterpolatedStringHandler.AppendFormatted(ClickyWorkIndex.OneLine(entry.Title, 80));
      defaultInterpolatedStringHandler.AppendLiteral(" · ");
      defaultInterpolatedStringHandler.AppendFormatted(fileName);
      defaultInterpolatedStringHandler.AppendLiteral("/");
      defaultInterpolatedStringHandler.AppendFormatted(text3);
      defaultInterpolatedStringHandler.AppendLiteral(" · ");
      defaultInterpolatedStringHandler.AppendFormatted(entry.State);
      defaultInterpolatedStringHandler.AppendFormatted(text4);
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x0600072B RID: 1835 RVA: 0x000FAC74 File Offset: 0x000FAC74
    internal static string IndexText([TupleElementNames(new string[] { "Date", "Line" })] [Nullable(new byte[] { 1, 0, 1 })] IReadOnlyList<ValueTuple<DateTimeOffset, string>> lines)
    {
      return "# Work HeyClicky has done for the user\n\nOne folder per piece of work, newest first. In each: report.md (what each run was asked and what it reported) and output/ (the files it made).\n\n" + string.Join("\n", from l in lines
        orderby l.Item1 descending
        select l.Item2) + "\n";
    }

    // Token: 0x04000294 RID: 660
    public const string StateFileName = "state.md";

    // Token: 0x04000295 RID: 661
    public const bool WritesStateFiles = false;

    // Token: 0x04000296 RID: 662
    public const string IndexFileName = "WORK.md";

    // Token: 0x04000297 RID: 663
    public static readonly string PreviousNoteRelativePath = Path.Combine(".clicky", "previous-note.md");

    // Token: 0x04000298 RID: 664
    public const string NoteFileName = "note.md";

    // Token: 0x04000299 RID: 665
    public const string RunsBlockStart = "<!-- clicky:runs -->";

    // Token: 0x0400029A RID: 666
    public const string RunsBlockEnd = "<!-- /clicky:runs -->";

    // Token: 0x0400029B RID: 667
    private readonly object gate = new object();

    // Token: 0x0400029C RID: 668
    private readonly Dictionary<string, string> lastIndexLinesByPath = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    // Token: 0x0400029D RID: 669
    private readonly HashSet<string> reportedTurnKeys = new HashSet<string>();

    // Token: 0x0400029E RID: 670
    private Task writes = Task.CompletedTask;

    // Token: 0x040002A0 RID: 672
    [Nullable(2)]
    private string ownerStamp;

    // Token: 0x0200018C RID: 396
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04000798 RID: 1944
      [Nullable(0)]
      public static Func<string, bool> <0>__Exists;
    }
  }
}
