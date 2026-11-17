using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000040 RID: 64
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyJobsNoteSelection
  {
    // Token: 0x060005C7 RID: 1479 RVA: 0x000F2640 File Offset: 0x000F2640
    public static DateTimeOffset LastActive(ClickyJob job)
    {
      DateTimeOffset? finishedAt = job.FinishedAt;
      if (finishedAt != null)
      {
        return finishedAt.GetValueOrDefault();
      }
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
      DateTimeOffset? dateTimeOffset2 = dateTimeOffset;
      if (dateTimeOffset2 == null)
      {
        return job.CreatedAt;
      }
      return dateTimeOffset2.GetValueOrDefault();
    }

    // Token: 0x060005C8 RID: 1480 RVA: 0x000F26B0 File Offset: 0x000F26B0
    public static IReadOnlyList<ClickyJob> Select(IReadOnlyList<ClickyJob> allJobs, [Nullable(2)] ISet<Guid> liveCardIds, Func<ClickyJob, ClickyJobRailPhase> phase, Func<ClickyJob, bool> repeats)
    {
      List<ClickyJob> list = allJobs.Where<ClickyJob>(delegate(ClickyJob job)
      {
        if (job.IsWaitingToStart)
        {
          Guid? suggestionCardId = job.SuggestionCardId;
          if (suggestionCardId != null)
          {
            Guid valueOrDefault = suggestionCardId.GetValueOrDefault();
            if (!job.IsDismissed)
            {
              ISet<Guid> liveCardIds2 = liveCardIds;
              return liveCardIds2 != null && liveCardIds2.Contains(valueOrDefault);
            }
            return false;
          }
        }
        return true;
      }).ToList<ClickyJob>();
      List<ClickyJob> list2 = list.Where<ClickyJob>(delegate(ClickyJob job)
      {
        bool flag = job.IsWaitingToStart || job.IsHeldForChrome || repeats(job);
        if (!flag)
        {
          ClickyJobRailPhase clickyJobRailPhase = phase(job);
          bool flag2 = clickyJobRailPhase <= ClickyJobRailPhase.NeedsYou;
          flag = flag2;
        }
        return flag;
      }).ToList<ClickyJob>();
      HashSet<string> attentionIds = list2.Select<ClickyJob, string>((ClickyJob j) => j.Id).ToHashSet<string>();
      IEnumerable<ClickyJob> enumerable = list.Where<ClickyJob>(delegate(ClickyJob job)
      {
        bool flag3 = !job.IsWaitingToStart && !job.IsHeldForChrome && !job.IsSeen && !job.IsDismissed && !attentionIds.Contains(job.Id);
        if (flag3)
        {
          ClickyJobRailPhase clickyJobRailPhase2 = phase(job);
          bool flag4 = clickyJobRailPhase2 - ClickyJobRailPhase.Done <= 2;
          flag3 = flag4;
        }
        return flag3;
      });
      Func<ClickyJob, DateTimeOffset> func;
      if ((func = ClickyJobsNoteSelection.<>O.<0>__LastActive) == null)
      {
        func = (ClickyJobsNoteSelection.<>O.<0>__LastActive = new Func<ClickyJob, DateTimeOffset>(ClickyJobsNoteSelection.LastActive));
      }
      List<ClickyJob> list3 = enumerable.OrderByDescending<ClickyJob, DateTimeOffset>(func).Take<ClickyJob>(5).ToList<ClickyJob>();
      attentionIds.UnionWith(list3.Select<ClickyJob, string>((ClickyJob j) => j.Id));
      IEnumerable<ClickyJob> enumerable2 = list.Where<ClickyJob>((ClickyJob job) => !attentionIds.Contains(job.Id) && !job.IsWaitingToStart);
      Func<ClickyJob, DateTimeOffset> func2;
      if ((func2 = ClickyJobsNoteSelection.<>O.<0>__LastActive) == null)
      {
        func2 = (ClickyJobsNoteSelection.<>O.<0>__LastActive = new Func<ClickyJob, DateTimeOffset>(ClickyJobsNoteSelection.LastActive));
      }
      IEnumerable<ClickyJob> enumerable3 = enumerable2.OrderByDescending<ClickyJob, DateTimeOffset>(func2).Take<ClickyJob>(6);
      IEnumerable<ClickyJob> enumerable4 = list2.Concat<ClickyJob>(list3).Concat<ClickyJob>(enumerable3);
      Func<ClickyJob, DateTimeOffset> func3;
      if ((func3 = ClickyJobsNoteSelection.<>O.<0>__LastActive) == null)
      {
        func3 = (ClickyJobsNoteSelection.<>O.<0>__LastActive = new Func<ClickyJob, DateTimeOffset>(ClickyJobsNoteSelection.LastActive));
      }
      return enumerable4.OrderBy<ClickyJob, DateTimeOffset>(func3).ToList<ClickyJob>();
    }

    // Token: 0x0400022E RID: 558
    public const int UnopenedListed = 5;

    // Token: 0x0400022F RID: 559
    public const int MostRecentListed = 6;

    // Token: 0x0200014D RID: 333
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040006E7 RID: 1767
      [Nullable(0)]
      public static Func<ClickyJob, DateTimeOffset> <0>__LastActive;
    }
  }
}
