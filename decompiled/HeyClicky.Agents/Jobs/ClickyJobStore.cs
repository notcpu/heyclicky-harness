using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000041 RID: 65
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyJobStore
  {
    // Token: 0x14000015 RID: 21
    // (add) Token: 0x060005C9 RID: 1481 RVA: 0x000F2810 File Offset: 0x000F2810
    // (remove) Token: 0x060005CA RID: 1482 RVA: 0x000F2848 File Offset: 0x000F2848
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler Changed;

    // Token: 0x1700018D RID: 397
    // (get) Token: 0x060005CB RID: 1483 RVA: 0x000F2880 File Offset: 0x000F2880
    // (set) Token: 0x060005CC RID: 1484 RVA: 0x000F2888 File Offset: 0x000F2888
    [Nullable(new byte[] { 2, 1, 1 })]
    public Func<IReadOnlyList<string>> CloudColorsInUseByClickys
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x1700018E RID: 398
    // (get) Token: 0x060005CD RID: 1485 RVA: 0x000F2894 File Offset: 0x000F2894
    // (set) Token: 0x060005CE RID: 1486 RVA: 0x000F289C File Offset: 0x000F289C
    [Nullable(new byte[] { 2, 1, 1 })]
    public Func<ISet<string>> JobIdsWithRoutines
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x060005CF RID: 1487 RVA: 0x000F28A8 File Offset: 0x000F28A8
    public ClickyJobStore()
    {
      this.jobs = (AgentDefaults.Get<List<ClickyJob>>("clicky.jobs.v1") ?? new List<ClickyJob>()).Select<ClickyJob, ClickyJob>(delegate(ClickyJob job)
      {
        if (job.Outcome != ClickyJobOutcome.Working)
        {
          return job;
        }
        ClickyJob clickyJob = job.<Clone>$();
        clickyJob.Outcome = ClickyJobOutcome.Stopped;
        clickyJob.FinishedAt = new DateTimeOffset?(job.FinishedAt ?? DateTimeOffset.Now);
        return clickyJob;
      }).ToList<ClickyJob>();
    }

    // Token: 0x1700018F RID: 399
    // (get) Token: 0x060005D0 RID: 1488 RVA: 0x000F2914 File Offset: 0x000F2914
    public IReadOnlyList<ClickyJob> Jobs
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<ClickyJob> readOnlyList;
        lock (obj)
        {
          readOnlyList = this.jobs.ToList<ClickyJob>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x17000190 RID: 400
    // (get) Token: 0x060005D1 RID: 1489 RVA: 0x000F295C File Offset: 0x000F295C
    public IReadOnlyList<ClickyJob> RailJobs
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<ClickyJob> readOnlyList;
        lock (obj)
        {
          readOnlyList = (from j in this.jobs
            where !j.IsDismissed
            orderby j.CreatedAt
            select j).ToList<ClickyJob>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x060005D2 RID: 1490 RVA: 0x000F29EC File Offset: 0x000F29EC
    [return: Nullable(2)]
    public ClickyJob Job(string jobId)
    {
      object obj = this.gate;
      ClickyJob clickyJob;
      lock (obj)
      {
        clickyJob = this.jobs.FirstOrDefault<ClickyJob>((ClickyJob j) => j.Id == jobId);
      }
      return clickyJob;
    }

    // Token: 0x060005D3 RID: 1491 RVA: 0x000F2A4C File Offset: 0x000F2A4C
    [return: Nullable(2)]
    public ClickyJob JobForThread(string threadId)
    {
      object obj = this.gate;
      ClickyJob clickyJob;
      lock (obj)
      {
        Func<ClickyJobRun, bool> <>9__2;
        clickyJob = this.jobs.FirstOrDefault<ClickyJob>((ClickyJob j) => j.ThreadId == threadId) ?? this.jobs.FirstOrDefault<ClickyJob>(delegate(ClickyJob j)
        {
          IReadOnlyList<ClickyJobRun> runs = j.Runs;
          if (runs == null)
          {
            return false;
          }
          Func<ClickyJobRun, bool> func;
          if ((func = <>9__2) == null)
          {
            func = (<>9__2 = (ClickyJobRun r) => r.ThreadId == threadId);
          }
          return runs.Any<ClickyJobRun>(func);
        });
      }
      return clickyJob;
    }

    // Token: 0x060005D4 RID: 1492 RVA: 0x000F2AC8 File Offset: 0x000F2AC8
    [NullableContext(2)]
    public string BuildsOnContext([Nullable(1)] IEnumerable<string> jobIds, string excludingJobId, [TupleElementNames(new string[] { "Id", "Title", "Folder" })] [Nullable(new byte[] { 2, 1, 0, 1, 1, 2 })] Func<string, ValueTuple<string, string, string>?> resolveElsewhere = null)
    {
      HashSet<string> seen = new HashSet<string>();
      List<ValueTuple<string, string, string>> list = (from work in jobIds.Select<string, ValueTuple<string, string, string>?>(delegate(string id)
        {
          ClickyJob clickyJob = this.Job(id) ?? this.JobForThread(id);
          if (clickyJob != null)
          {
            return new ValueTuple<string, string, string>?(new ValueTuple<string, string, string>(clickyJob.Id, clickyJob.Title, clickyJob.FolderPath));
          }
          Func<string, ValueTuple<string, string, string>?> resolveElsewhere2 = resolveElsewhere;
          if (resolveElsewhere2 == null)
          {
            return null;
          }
          return resolveElsewhere2(id);
        }).OfType<ValueTuple<string, string, string>>()
        where work.Item1 != excludingJobId && seen.Add(work.Item1)
        select work).ToList<ValueTuple<string, string, string>>();
      if (list.Count == 0)
      {
        return null;
      }
      string text = string.Join("; ", list.Select<ValueTuple<string, string, string>, string>(delegate([TupleElementNames(new string[] { "Id", "Title", "Folder" })] ValueTuple<string, string, string> work)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
        if (work.Item3 != null)
        {
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 3);
          defaultInterpolatedStringHandler.AppendLiteral("\"");
          defaultInterpolatedStringHandler.AppendFormatted(work.Item2);
          defaultInterpolatedStringHandler.AppendLiteral("\" (job_id ");
          defaultInterpolatedStringHandler.AppendFormatted(work.Item1);
          defaultInterpolatedStringHandler.AppendLiteral(", folder ");
          defaultInterpolatedStringHandler.AppendFormatted(work.Item3);
          defaultInterpolatedStringHandler.AppendLiteral(")");
          return defaultInterpolatedStringHandler.ToStringAndClear();
        }
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 2);
        defaultInterpolatedStringHandler.AppendLiteral("\"");
        defaultInterpolatedStringHandler.AppendFormatted(work.Item2);
        defaultInterpolatedStringHandler.AppendLiteral("\" (job_id ");
        defaultInterpolatedStringHandler.AppendFormatted(work.Item1);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }));
      return "This builds on earlier work: " + text + ". Before you start, read what it found with read_job (its report, and the files and rules it names), and use it: don't redo that work or ask the user for what it already has.";
    }

    // Token: 0x060005D5 RID: 1493 RVA: 0x000F2B74 File Offset: 0x000F2B74
    public ClickyJob CreateJob(string title, string prompt, string projectsRootPath, bool startsWorking = true)
    {
      string text = Guid.NewGuid().ToString("N").Substring(0, 10)
        .ToLowerInvariant();
      string text2 = title.Trim();
      string text3 = ((text2.Length == 0) ? ClickyJobStore.FallbackTitle(prompt) : text2);
      string text4 = ClickyWorkFolderFiles.FolderSlug(text3);
      string text5 = Path.Combine(projectsRootPath, "jobs", (text4.Length == 0) ? text : (text4 + "-" + text));
      foreach (string text6 in new string[] { "tmp", "output" })
      {
        try
        {
          Directory.CreateDirectory(Path.Combine(text5, text6));
        }
        catch (Exception ex)
        {
          string text7 = "Jobs";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 3);
          defaultInterpolatedStringHandler.AppendLiteral("couldn't make ");
          defaultInterpolatedStringHandler.AppendFormatted(text6);
          defaultInterpolatedStringHandler.AppendLiteral(" in ");
          defaultInterpolatedStringHandler.AppendFormatted(text5);
          defaultInterpolatedStringHandler.AppendLiteral(": ");
          defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
          Log.Warn(text7, defaultInterpolatedStringHandler.ToStringAndClear());
        }
      }
      string text8 = ((prompt.Length > 2000) ? prompt.Substring(0, 2000) : prompt);
      ClickyJob clickyJob = new ClickyJob();
      clickyJob.Id = text;
      clickyJob.Title = text3;
      clickyJob.Prompt = text8;
      clickyJob.FolderPath = text5;
      clickyJob.CreatedAt = DateTimeOffset.Now;
      clickyJob.FinishedAt = (startsWorking ? null : new DateTimeOffset?(DateTimeOffset.Now));
      clickyJob.Outcome = (startsWorking ? ClickyJobOutcome.Working : ClickyJobOutcome.Done);
      clickyJob.IsSeen = !startsWorking;
      clickyJob.IsDismissed = !startsWorking;
      clickyJob.CloudColorName = this.LeastUsedCloudColor();
      IReadOnlyList<ClickyJobRun> readOnlyList2;
      if (!startsWorking)
      {
        IReadOnlyList<ClickyJobRun> readOnlyList = Array.Empty<ClickyJobRun>();
        readOnlyList2 = readOnlyList;
      }
      else
      {
        IReadOnlyList<ClickyJobRun> readOnlyList = new <>z__ReadOnlySingleElementList<ClickyJobRun>(new ClickyJobRun
        {
          StartedAt = DateTimeOffset.Now,
          Ask = text8,
          Outcome = ClickyJobOutcome.Working
        });
        readOnlyList2 = readOnlyList;
      }
      clickyJob.Runs = readOnlyList2;
      ClickyJob clickyJob2 = clickyJob;
      object obj = this.gate;
      lock (obj)
      {
        this.jobs.Add(clickyJob2);
        this.TrimKeptJobsLocked();
        this.SaveLocked();
      }
      this.RaiseChanged();
      return clickyJob2;
    }

    // Token: 0x060005D6 RID: 1494 RVA: 0x000F2DCC File Offset: 0x000F2DCC
    public void BeginRun(string jobId, string ask, [Nullable(2)] string threadId = null)
    {
      this.Update(jobId, delegate(ClickyJob job)
      {
        IReadOnlyList<ClickyJobRun> readOnlyList;
        if ((readOnlyList = job.Runs) == null)
        {
          string threadId2 = job.ThreadId;
          if (threadId2 == null)
          {
            IReadOnlyList<ClickyJobRun> readOnlyList2 = Array.Empty<ClickyJobRun>();
            readOnlyList = readOnlyList2;
          }
          else
          {
            IReadOnlyList<ClickyJobRun> readOnlyList2 = new <>z__ReadOnlySingleElementList<ClickyJobRun>(new ClickyJobRun
            {
              ThreadId = threadId2,
              StartedAt = job.CreatedAt,
              Ask = job.Prompt,
              FinishedAt = job.FinishedAt,
              Outcome = ((job.Outcome == ClickyJobOutcome.Working) ? ClickyJobOutcome.Stopped : job.Outcome),
              Summary = job.Summary
            });
            readOnlyList = readOnlyList2;
          }
        }
        List<ClickyJobRun> list = readOnlyList.ToList<ClickyJobRun>();
        list.Add(new ClickyJobRun
        {
          ThreadId = threadId,
          StartedAt = DateTimeOffset.Now,
          Ask = ((ask.Length > 2000) ? ask.Substring(0, 2000) : ask),
          Outcome = ClickyJobOutcome.Working
        });
        ClickyJob clickyJob = job.<Clone>$();
        clickyJob.Runs = list;
        clickyJob.WaitingStep = null;
        return clickyJob;
      });
    }

    // Token: 0x060005D7 RID: 1495 RVA: 0x000F2E00 File Offset: 0x000F2E00
    public void SetResultCard(string jobId, [Nullable(2)] ClickyAnswer card)
    {
      this.Update(jobId, delegate(ClickyJob job)
      {
        ClickyJob clickyJob = job.<Clone>$();
        clickyJob.ResultCard = card;
        return clickyJob;
      });
    }

    // Token: 0x060005D8 RID: 1496 RVA: 0x000F2E30 File Offset: 0x000F2E30
    public void SetWaitingStep(string jobId, [Nullable(2)] ClickyJobUserStep step)
    {
      this.Update(jobId, delegate(ClickyJob job)
      {
        ClickyJob clickyJob = job.<Clone>$();
        clickyJob.WaitingStep = step;
        return clickyJob;
      });
    }

    // Token: 0x060005D9 RID: 1497 RVA: 0x000F2E60 File Offset: 0x000F2E60
    public void SetWaitsForChrome(string jobId, bool waits)
    {
      this.Update(jobId, delegate(ClickyJob job)
      {
        ClickyJob clickyJob = job.<Clone>$();
        clickyJob.WaitsForChrome = (waits ? new bool?(true) : null);
        return clickyJob;
      });
    }

    // Token: 0x060005DA RID: 1498 RVA: 0x000F2E90 File Offset: 0x000F2E90
    public void SetSkipsChrome(string jobId)
    {
      this.Update(jobId, delegate(ClickyJob job)
      {
        ClickyJob clickyJob = job.<Clone>$();
        clickyJob.SkipsChrome = new bool?(true);
        return clickyJob;
      });
    }

    // Token: 0x060005DB RID: 1499 RVA: 0x000F2EB8 File Offset: 0x000F2EB8
    public void SetNickname(string jobId, string nickname)
    {
      this.Update(jobId, delegate(ClickyJob job)
      {
        ClickyJob clickyJob = job.<Clone>$();
        clickyJob.Nickname = nickname;
        return clickyJob;
      });
    }

    // Token: 0x060005DC RID: 1500 RVA: 0x000F2EE8 File Offset: 0x000F2EE8
    private string LeastUsedCloudColor()
    {
      ClickyJobStore.<>c__DisplayClass31_0 CS$<>8__locals1 = new ClickyJobStore.<>c__DisplayClass31_0();
      object obj = this.gate;
      lock (obj)
      {
        CS$<>8__locals1.inUse = (from j in this.jobs
          where !j.IsDismissed
          select j.CloudColorName ?? ClickyJobStore.SeededCloudColor(j.Id)).ToList<string>();
      }
      try
      {
        List<string> inUse = CS$<>8__locals1.inUse;
        Func<IReadOnlyList<string>> cloudColorsInUseByClickys = this.CloudColorsInUseByClickys;
        inUse.AddRange(((cloudColorsInUseByClickys != null) ? cloudColorsInUseByClickys() : null) ?? Array.Empty<string>());
      }
      catch (Exception)
      {
      }
      CS$<>8__locals1.fewest = ClickyJobStore.CloudColors.Min<string>(new Func<string, int>(CS$<>8__locals1.<LeastUsedCloudColor>g__Uses|0));
      List<string> list = ClickyJobStore.CloudColors.Where<string>((string c) => base.<LeastUsedCloudColor>g__Uses|0(c) == CS$<>8__locals1.fewest).ToList<string>();
      return list[Random.Shared.Next(list.Count)];
    }

    // Token: 0x060005DD RID: 1501 RVA: 0x000F3008 File Offset: 0x000F3008
    public static string SeededCloudColor(string seed)
    {
      ulong num = 5381UL;
      foreach (Rune rune in seed.EnumerateRunes())
      {
        num = num * 33UL + (ulong)((long)rune.Value);
      }
      return ClickyJobStore.CloudColors[(int)(num % (ulong)((long)ClickyJobStore.CloudColors.Length))];
    }

    // Token: 0x060005DE RID: 1502 RVA: 0x000F3080 File Offset: 0x000F3080
    public ClickyJob CreateWaitingJob(string title, string pitch, Guid suggestionCardId, string projectsRootPath)
    {
      object obj = this.gate;
      lock (obj)
      {
        ClickyJob clickyJob = this.jobs.FirstOrDefault<ClickyJob>((ClickyJob j) => j.SuggestionCardId == suggestionCardId && !j.IsDismissed);
        if (clickyJob != null)
        {
          return clickyJob;
        }
      }
      ClickyJob clickyJob2 = this.CreateJob(title, pitch, projectsRootPath, true);
      this.Update(clickyJob2.Id, delegate(ClickyJob j)
      {
        ClickyJob clickyJob3 = j.<Clone>$();
        clickyJob3.Outcome = ClickyJobOutcome.Stopped;
        clickyJob3.FinishedAt = new DateTimeOffset?(DateTimeOffset.Now);
        clickyJob3.IsSeen = true;
        clickyJob3.SuggestionCardId = new Guid?(suggestionCardId);
        clickyJob3.Runs = Array.Empty<ClickyJobRun>();
        return clickyJob3;
      });
      return this.Job(clickyJob2.Id) ?? clickyJob2;
    }

    // Token: 0x060005DF RID: 1503 RVA: 0x000F3124 File Offset: 0x000F3124
    [NullableContext(2)]
    public ClickyJob WaitingJob(Guid suggestionCardId)
    {
      object obj = this.gate;
      ClickyJob clickyJob;
      lock (obj)
      {
        clickyJob = this.jobs.FirstOrDefault<ClickyJob>((ClickyJob j) => j.SuggestionCardId == suggestionCardId && j.ThreadId == null && !j.IsDismissed);
      }
      return clickyJob;
    }

    // Token: 0x060005E0 RID: 1504 RVA: 0x000F3184 File Offset: 0x000F3184
    [return: Nullable(2)]
    public ClickyJob BeginWaitingJob(string jobId)
    {
      this.Update(jobId, delegate(ClickyJob job)
      {
        ClickyJob clickyJob = job.<Clone>$();
        clickyJob.SuggestionCardId = null;
        clickyJob.Runs = new <>z__ReadOnlySingleElementList<ClickyJobRun>(new ClickyJobRun
        {
          StartedAt = DateTimeOffset.Now,
          Ask = job.Prompt,
          Outcome = ClickyJobOutcome.Working
        });
        return clickyJob;
      });
      this.MarkWorking(jobId);
      return this.Job(jobId);
    }

    // Token: 0x060005E1 RID: 1505 RVA: 0x000F31BC File Offset: 0x000F31BC
    public void RelinkWaitingJobsToRestoredCards([TupleElementNames(new string[] { "Id", "Description" })] [Nullable(new byte[] { 1, 0, 1 })] IReadOnlyList<ValueTuple<Guid, string>> cards)
    {
      foreach (KeyValuePair<string, Guid> keyValuePair in ClickyJobStore.WaitingJobRelinks(this.Jobs, cards))
      {
        string text;
        Guid guid;
        keyValuePair.Deconstruct(out text, out guid);
        string text2 = text;
        Guid cardId = guid;
        this.Update(text2, delegate(ClickyJob job)
        {
          ClickyJob clickyJob = job.<Clone>$();
          clickyJob.SuggestionCardId = new Guid?(cardId);
          return clickyJob;
        });
      }
    }

    // Token: 0x060005E2 RID: 1506 RVA: 0x000F3240 File Offset: 0x000F3240
    public static Dictionary<string, Guid> WaitingJobRelinks(IReadOnlyList<ClickyJob> jobs, [TupleElementNames(new string[] { "Id", "Description" })] [Nullable(new byte[] { 1, 0, 1 })] IReadOnlyList<ValueTuple<Guid, string>> cards)
    {
      HashSet<Guid> hashSet = cards.Select<ValueTuple<Guid, string>, Guid>(([TupleElementNames(new string[] { "Id", "Description" })] ValueTuple<Guid, string> c) => c.Item1).ToHashSet<Guid>();
      HashSet<Guid> claimedCardIds = (from job in jobs
        where job.IsWaitingToStart && !job.IsDismissed
        select job.SuggestionCardId.Value).Where<Guid>(new Func<Guid, bool>(hashSet.Contains)).ToHashSet<Guid>();
      Dictionary<string, Guid> dictionary = new Dictionary<string, Guid>();
      using (IEnumerator<ClickyJob> enumerator = jobs.Where<ClickyJob>((ClickyJob job) => job.IsWaitingToStart && !job.IsDismissed).GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ClickyJob job = enumerator.Current;
          Guid? suggestionCardId = job.SuggestionCardId;
          if (suggestionCardId != null)
          {
            Guid valueOrDefault = suggestionCardId.GetValueOrDefault();
            if (!hashSet.Contains(valueOrDefault))
            {
              List<ValueTuple<Guid, string>> list = cards.Where<ValueTuple<Guid, string>>(([TupleElementNames(new string[] { "Id", "Description" })] ValueTuple<Guid, string> card) => !claimedCardIds.Contains(card.Item1) && ((card.Item2.Length > 2000) ? card.Item2.Substring(0, 2000) : card.Item2) == job.Prompt).ToList<ValueTuple<Guid, string>>();
              if (list.Count == 1)
              {
                claimedCardIds.Add(list[0].Item1);
                dictionary[job.Id] = list[0].Item1;
              }
            }
          }
        }
      }
      return dictionary;
    }

    // Token: 0x060005E3 RID: 1507 RVA: 0x000F33EC File Offset: 0x000F33EC
    public ClickyJob CreateJobForMovedRoutine(string title, string prompt, string folderPath, [Nullable(2)] string earlierThreadId, DateTimeOffset? lastRunAt)
    {
      string text = Guid.NewGuid().ToString("N").Substring(0, 10)
        .ToLowerInvariant();
      foreach (string text2 in new string[] { "tmp", "output" })
      {
        try
        {
          Directory.CreateDirectory(Path.Combine(folderPath, text2));
        }
        catch (Exception ex)
        {
          string text3 = "Jobs";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 3);
          defaultInterpolatedStringHandler.AppendLiteral("couldn't make ");
          defaultInterpolatedStringHandler.AppendFormatted(text2);
          defaultInterpolatedStringHandler.AppendLiteral(" in ");
          defaultInterpolatedStringHandler.AppendFormatted(folderPath);
          defaultInterpolatedStringHandler.AppendLiteral(": ");
          defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
          Log.Warn(text3, defaultInterpolatedStringHandler.ToStringAndClear());
        }
      }
      DateTimeOffset now = DateTimeOffset.Now;
      string text4 = title.Trim();
      string text5 = ((prompt.Length > 2000) ? prompt.Substring(0, 2000) : prompt);
      ClickyJob clickyJob = new ClickyJob();
      clickyJob.Id = text;
      clickyJob.Title = ((text4.Length == 0) ? ClickyJobStore.FallbackTitle(prompt) : text4);
      clickyJob.Prompt = text5;
      clickyJob.FolderPath = folderPath;
      clickyJob.CreatedAt = now;
      clickyJob.FinishedAt = new DateTimeOffset?(lastRunAt.GetValueOrDefault(now));
      clickyJob.Outcome = ClickyJobOutcome.Done;
      clickyJob.IsSeen = true;
      clickyJob.IsDismissed = true;
      clickyJob.CloudColorName = this.LeastUsedCloudColor();
      IReadOnlyList<ClickyJobRun> readOnlyList2;
      if (earlierThreadId != null)
      {
        IReadOnlyList<ClickyJobRun> readOnlyList = new <>z__ReadOnlySingleElementList<ClickyJobRun>(new ClickyJobRun
        {
          ThreadId = earlierThreadId,
          StartedAt = lastRunAt.GetValueOrDefault(now),
          Ask = text5,
          FinishedAt = new DateTimeOffset?(lastRunAt.GetValueOrDefault(now)),
          Outcome = ClickyJobOutcome.Done
        });
        readOnlyList2 = readOnlyList;
      }
      else
      {
        IReadOnlyList<ClickyJobRun> readOnlyList = Array.Empty<ClickyJobRun>();
        readOnlyList2 = readOnlyList;
      }
      clickyJob.Runs = readOnlyList2;
      ClickyJob clickyJob2 = clickyJob;
      object obj = this.gate;
      lock (obj)
      {
        this.jobs.Add(clickyJob2);
        this.SaveLocked();
      }
      this.RaiseChanged();
      return clickyJob2;
    }

    // Token: 0x060005E4 RID: 1508 RVA: 0x000F3614 File Offset: 0x000F3614
    public void RecordThreadId(string threadId, string jobId)
    {
      this.Update(jobId, delegate(ClickyJob job)
      {
        IReadOnlyList<ClickyJobRun> runs = job.Runs;
        List<ClickyJobRun> list = ((runs != null) ? runs.ToList<ClickyJobRun>() : null);
        if (list != null && list.Count > 0)
        {
          List<ClickyJobRun> list2 = list;
          if (list2[list2.Count - 1].ThreadId == null)
          {
            List<ClickyJobRun> list3 = list;
            int num = list3.Count - 1;
            List<ClickyJobRun> list4 = list;
            ClickyJobRun clickyJobRun = list4[list4.Count - 1].<Clone>$();
            clickyJobRun.ThreadId = threadId;
            list3[num] = clickyJobRun;
          }
        }
        ClickyJob clickyJob = job.<Clone>$();
        clickyJob.ThreadId = threadId;
        IReadOnlyList<ClickyJobRun> readOnlyList = list;
        clickyJob.Runs = readOnlyList ?? job.Runs;
        return clickyJob;
      });
    }

    // Token: 0x060005E5 RID: 1509 RVA: 0x000F3644 File Offset: 0x000F3644
    public void Retitle(string jobId, string newTitle)
    {
      string title = newTitle.Trim();
      if (title.Length == 0)
      {
        return;
      }
      this.Update(jobId, delegate(ClickyJob job)
      {
        ClickyJob clickyJob = job.<Clone>$();
        clickyJob.Title = ((title.Length > 80) ? title.Substring(0, 80) : title);
        return clickyJob;
      });
    }

    // Token: 0x060005E6 RID: 1510 RVA: 0x000F3684 File Offset: 0x000F3684
    public void MarkWorking(string jobId)
    {
      this.Update(jobId, delegate(ClickyJob job)
      {
        ClickyJob clickyJob = job.<Clone>$();
        clickyJob.FollowedUpOnLaterDay = ((job.ThreadId != null && job.CreatedAt.LocalDateTime.Date != DateTime.Now.Date) ? new bool?(true) : job.FollowedUpOnLaterDay);
        clickyJob.Outcome = ClickyJobOutcome.Working;
        clickyJob.FinishedAt = null;
        clickyJob.WaitingStep = null;
        clickyJob.ResultCard = null;
        clickyJob.IsSeen = false;
        clickyJob.IsDismissed = false;
        clickyJob.IsArchived = null;
        return clickyJob;
      });
    }

    // Token: 0x060005E7 RID: 1511 RVA: 0x000F36AC File Offset: 0x000F36AC
    public void MarkEnded(string jobId, ClickyJobOutcome outcome, [Nullable(2)] string summary, [Nullable(2)] string replyText, IReadOnlyList<string> artifactEntries)
    {
      this.Update(jobId, delegate(ClickyJob job)
      {
        DateTimeOffset now = DateTimeOffset.Now;
        IReadOnlyList<ClickyJobRun> runs = job.Runs;
        List<ClickyJobRun> list = ((runs != null) ? runs.ToList<ClickyJobRun>() : null);
        if (list != null && list.Count > 0)
        {
          List<ClickyJobRun> list2 = list;
          if (list2[list2.Count - 1].FinishedAt == null)
          {
            List<ClickyJobRun> list3 = list;
            int num = list3.Count - 1;
            List<ClickyJobRun> list4 = list;
            ClickyJobRun clickyJobRun = list4[list4.Count - 1].<Clone>$();
            clickyJobRun.FinishedAt = new DateTimeOffset?(now);
            clickyJobRun.Outcome = outcome;
            string text;
            if (!string.IsNullOrEmpty(summary))
            {
              text = ((summary.Length > 400) ? summary.Substring(0, 400) : summary);
            }
            else
            {
              List<ClickyJobRun> list5 = list;
              text = list5[list5.Count - 1].Summary;
            }
            clickyJobRun.Summary = text;
            list3[num] = clickyJobRun;
          }
        }
        ClickyJob clickyJob = job.<Clone>$();
        clickyJob.Outcome = outcome;
        clickyJob.FinishedAt = new DateTimeOffset?(now);
        clickyJob.Summary = (string.IsNullOrEmpty(summary) ? job.Summary : summary);
        clickyJob.ReplyText = (string.IsNullOrEmpty(replyText) ? job.ReplyText : ((replyText.Length > 1200) ? replyText.Substring(0, 1200) : replyText));
        clickyJob.ArtifactEntries = ((artifactEntries.Count > 0) ? artifactEntries : job.ArtifactEntries);
        clickyJob.LatestReplyArtifactEntries = artifactEntries;
        clickyJob.IsSeen = false;
        IReadOnlyList<ClickyJobRun> readOnlyList = list;
        clickyJob.Runs = readOnlyList ?? job.Runs;
        return clickyJob;
      });
    }

    // Token: 0x060005E8 RID: 1512 RVA: 0x000F36F0 File Offset: 0x000F36F0
    public void MarkSeen(string jobId)
    {
      ClickyJob clickyJob = this.Job(jobId);
      if (clickyJob == null || !clickyJob.IsFinished || clickyJob.IsSeen)
      {
        return;
      }
      this.Update(jobId, delegate(ClickyJob job)
      {
        ClickyJob clickyJob2 = job.<Clone>$();
        clickyJob2.IsSeen = true;
        return clickyJob2;
      });
    }

    // Token: 0x060005E9 RID: 1513 RVA: 0x000F3740 File Offset: 0x000F3740
    public void KeepOnDock(string jobId)
    {
      this.Update(jobId, delegate(ClickyJob job)
      {
        ClickyJob clickyJob = job.<Clone>$();
        clickyJob.IsDismissed = false;
        clickyJob.IsArchived = null;
        return clickyJob;
      });
    }

    // Token: 0x060005EA RID: 1514 RVA: 0x000F3768 File Offset: 0x000F3768
    public void Dismiss(string jobId)
    {
      this.Update(jobId, delegate(ClickyJob job)
      {
        ClickyJob clickyJob = job.<Clone>$();
        clickyJob.IsDismissed = true;
        return clickyJob;
      });
    }

    // Token: 0x060005EB RID: 1515 RVA: 0x000F3790 File Offset: 0x000F3790
    public void Archive(string jobId)
    {
      this.Update(jobId, delegate(ClickyJob job)
      {
        ClickyJob clickyJob = job.<Clone>$();
        clickyJob.IsDismissed = true;
        clickyJob.IsArchived = new bool?(true);
        return clickyJob;
      });
    }

    // Token: 0x060005EC RID: 1516 RVA: 0x000F37B8 File Offset: 0x000F37B8
    public void Unarchive(string jobId)
    {
      this.Update(jobId, delegate(ClickyJob job)
      {
        ClickyJob clickyJob = job.<Clone>$();
        clickyJob.IsDismissed = false;
        clickyJob.IsArchived = null;
        return clickyJob;
      });
    }

    // Token: 0x17000191 RID: 401
    // (get) Token: 0x060005ED RID: 1517 RVA: 0x000F37E0 File Offset: 0x000F37E0
    public IReadOnlyList<ClickyJob> ArchivedJobs
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<ClickyJob> readOnlyList;
        lock (obj)
        {
          readOnlyList = (from j in this.jobs
            where j.IsArchived.GetValueOrDefault()
            orderby j.CreatedAt descending
            select j).ToList<ClickyJob>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x060005EE RID: 1518 RVA: 0x000F3870 File Offset: 0x000F3870
    public void RemoveAllForSignOut()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.jobs = new List<ClickyJob>();
      }
      AgentDefaults.Remove("clicky.jobs.v1");
      this.RaiseChanged();
    }

    // Token: 0x060005EF RID: 1519 RVA: 0x000F38C8 File Offset: 0x000F38C8
    private void Update(string jobId, Func<ClickyJob, ClickyJob> change)
    {
      object obj = this.gate;
      lock (obj)
      {
        int num = this.jobs.FindIndex((ClickyJob j) => j.Id == jobId);
        if (num < 0)
        {
          return;
        }
        ClickyJob clickyJob = change(this.jobs[num]);
        if (ClickyJobStore.JobsEqual(clickyJob, this.jobs[num]))
        {
          return;
        }
        this.jobs[num] = clickyJob;
        this.SaveLocked();
      }
      this.RaiseChanged();
    }

    // Token: 0x060005F0 RID: 1520 RVA: 0x000F3974 File Offset: 0x000F3974
    private static bool JobsEqual(ClickyJob a, ClickyJob b)
    {
      return JsonSerializer.Serialize<ClickyJob>(a, null) == JsonSerializer.Serialize<ClickyJob>(b, null);
    }

    // Token: 0x060005F1 RID: 1521 RVA: 0x000F398C File Offset: 0x000F398C
    private void TrimKeptJobsLocked()
    {
      ClickyJobStore.<>c__DisplayClass53_0 CS$<>8__locals1 = new ClickyJobStore.<>c__DisplayClass53_0();
      if (this.jobs.Count <= 80)
      {
        return;
      }
      ClickyJobStore.<>c__DisplayClass53_0 CS$<>8__locals2 = CS$<>8__locals1;
      Func<ISet<string>> jobIdsWithRoutines = this.JobIdsWithRoutines;
      CS$<>8__locals2.withRoutines = ((jobIdsWithRoutines != null) ? jobIdsWithRoutines() : null) ?? new HashSet<string>();
      CS$<>8__locals1.removable = (from j in (from j in this.jobs
          where j.IsDismissed && !CS$<>8__locals1.withRoutines.Contains(j.Id)
          orderby j.CreatedAt
          select j).Take<ClickyJob>(this.jobs.Count - 80)
        select j.Id).ToHashSet<string>();
      this.jobs.RemoveAll((ClickyJob j) => CS$<>8__locals1.removable.Contains(j.Id));
    }

    // Token: 0x060005F2 RID: 1522 RVA: 0x000F3A68 File Offset: 0x000F3A68
    private void SaveLocked()
    {
      AgentDefaults.Set<List<ClickyJob>>("clicky.jobs.v1", this.jobs);
      this.WorkFolderFiles.Refresh(this.jobs.ToList<ClickyJob>(), false);
    }

    // Token: 0x060005F3 RID: 1523 RVA: 0x000F3A94 File Offset: 0x000F3A94
    private void RaiseChanged()
    {
      try
      {
        EventHandler changed = this.Changed;
        if (changed != null)
        {
          changed(this, EventArgs.Empty);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Jobs", "Changed handler threw: " + ex.Message);
      }
    }

    // Token: 0x17000192 RID: 402
    // (get) Token: 0x060005F4 RID: 1524 RVA: 0x000F3AE8 File Offset: 0x000F3AE8
    public ClickyWorkFolderFiles WorkFolderFiles { get; } = new ClickyWorkFolderFiles();

    // Token: 0x060005F5 RID: 1525 RVA: 0x000F3AF0 File Offset: 0x000F3AF0
    private static string FallbackTitle(string prompt)
    {
      string text = (prompt.Split(RuntimeHelpers.CreateSpan<char>(fieldof(<PrivateImplementationDetails>.FBB68C637180E81FFB4AF91596B53064C159DDF8F56E330BDD31C0DE6472348D2).FieldHandle)).FirstOrDefault<string>((string l) => l.Length > 0) ?? prompt).Trim();
      if (text.Length > 48)
      {
        return text.Substring(0, 46).Trim() + "…";
      }
      if (text.Length != 0)
      {
        return text;
      }
      return "Job";
    }

    // Token: 0x04000230 RID: 560
    private const string DefaultsKey = "clicky.jobs.v1";

    // Token: 0x04000231 RID: 561
    private const int MaximumJobsKept = 80;

    // Token: 0x04000232 RID: 562
    private static readonly string[] CloudColors = new string[] { "red", "blue", "yellow", "green" };

    // Token: 0x04000233 RID: 563
    private readonly object gate = new object();

    // Token: 0x04000234 RID: 564
    private List<ClickyJob> jobs;
  }
}
