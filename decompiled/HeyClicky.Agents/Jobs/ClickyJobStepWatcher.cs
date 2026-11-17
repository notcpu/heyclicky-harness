using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000043 RID: 67
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyJobStepWatcher : IDisposable
  {
    // Token: 0x14000016 RID: 22
    // (add) Token: 0x0600060C RID: 1548 RVA: 0x000F3FBC File Offset: 0x000F3FBC
    // (remove) Token: 0x0600060D RID: 1549 RVA: 0x000F3FF4 File Offset: 0x000F3FF4
    [Nullable(new byte[] { 2, 1, 1 })]
    [field: Nullable(new byte[] { 2, 1, 1 })]
    public event Action<string, string> StepDone;

    // Token: 0x17000198 RID: 408
    // (get) Token: 0x0600060E RID: 1550 RVA: 0x000F402C File Offset: 0x000F402C
    // (set) Token: 0x0600060F RID: 1551 RVA: 0x000F4034 File Offset: 0x000F4034
    [Nullable(2)]
    public Func<string> FrontmostBrowserAddress
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000610 RID: 1552 RVA: 0x000F4040 File Offset: 0x000F4040
    public ClickyJobStepWatcher(ClickyJobStore store)
    {
      this.store = store;
      store.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.Reconcile();
      };
    }

    // Token: 0x06000611 RID: 1553 RVA: 0x000F4090 File Offset: 0x000F4090
    private List<ClickyJob> WatchedJobs()
    {
      DateTimeOffset now = DateTimeOffset.Now;
      object obj = this.gate;
      List<ClickyJob> list;
      lock (obj)
      {
        list = this.store.Jobs.Where<ClickyJob>(delegate(ClickyJob job)
        {
          if (!job.IsDismissed)
          {
            ClickyJobUserStep waitingStep = job.WaitingStep;
            if (waitingStep != null && waitingStep.DoneWhenUrlContains != null && now - waitingStep.AskedAt < ClickyJobStepWatcher.MaximumWait)
            {
              return !this.jobIdsContinued.Contains(job.Id);
            }
          }
          return false;
        }).ToList<ClickyJob>();
      }
      return list;
    }

    // Token: 0x06000612 RID: 1554 RVA: 0x000F4108 File Offset: 0x000F4108
    public void Reconcile()
    {
      HashSet<string> hashSet = (from j in this.WatchedJobs()
        select j.Id).ToHashSet<string>();
      object obj = this.gate;
      lock (obj)
      {
        this.jobIdsSeenNotDone.IntersectWith(hashSet);
        this.jobIdsContinued.RemoveWhere(delegate(string id)
        {
          ClickyJob clickyJob = this.store.Job(id);
          return ((clickyJob != null) ? clickyJob.WaitingStep : null) == null;
        });
        if (hashSet.Count == 0)
        {
          Timer timer = this.timer;
          if (timer != null)
          {
            timer.Dispose();
          }
          this.timer = null;
        }
        else if (this.timer == null)
        {
          this.timer = new Timer(delegate(object _)
          {
            this.Check();
          }, null, ClickyJobStepWatcher.CheckInterval, ClickyJobStepWatcher.CheckInterval);
        }
      }
    }

    // Token: 0x06000613 RID: 1555 RVA: 0x000F41E4 File Offset: 0x000F41E4
    private void Check()
    {
      try
      {
        List<ClickyJob> list = this.WatchedJobs();
        if (list.Count == 0)
        {
          this.Reconcile();
        }
        else
        {
          Func<string> frontmostBrowserAddress = this.FrontmostBrowserAddress;
          string text = ((frontmostBrowserAddress != null) ? frontmostBrowserAddress() : null);
          if (text != null && text.Length > 0)
          {
            string text2 = text.ToLowerInvariant();
            foreach (ClickyJob clickyJob in list)
            {
              ClickyJobUserStep waitingStep = clickyJob.WaitingStep;
              string text3;
              if (waitingStep == null)
              {
                text3 = null;
              }
              else
              {
                string doneWhenUrlContains = waitingStep.DoneWhenUrlContains;
                text3 = ((doneWhenUrlContains != null) ? doneWhenUrlContains.ToLowerInvariant() : null);
              }
              string text4 = text3;
              if (text4 != null)
              {
                object obj = this.gate;
                bool flag2;
                lock (obj)
                {
                  if (!text2.Contains(text4))
                  {
                    this.jobIdsSeenNotDone.Add(clickyJob.Id);
                    flag2 = false;
                  }
                  else if (this.jobIdsSeenNotDone.Contains(clickyJob.Id))
                  {
                    this.jobIdsContinued.Add(clickyJob.Id);
                    flag2 = true;
                  }
                  else
                  {
                    flag2 = false;
                  }
                }
                if (flag2)
                {
                  Action<string, string> stepDone = this.StepDone;
                  if (stepDone != null)
                  {
                    stepDone(clickyJob.Id, text);
                  }
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Jobs", "step watcher: " + ex.Message);
      }
    }

    // Token: 0x06000614 RID: 1556 RVA: 0x000F438C File Offset: 0x000F438C
    public void Dispose()
    {
      object obj = this.gate;
      lock (obj)
      {
        Timer timer = this.timer;
        if (timer != null)
        {
          timer.Dispose();
        }
        this.timer = null;
      }
    }

    // Token: 0x0400023E RID: 574
    private static readonly TimeSpan CheckInterval = TimeSpan.FromSeconds(2.5);

    // Token: 0x0400023F RID: 575
    private static readonly TimeSpan MaximumWait = TimeSpan.FromHours(6);

    // Token: 0x04000240 RID: 576
    private readonly ClickyJobStore store;

    // Token: 0x04000241 RID: 577
    private readonly object gate = new object();

    // Token: 0x04000242 RID: 578
    [Nullable(2)]
    private Timer timer;

    // Token: 0x04000243 RID: 579
    private readonly HashSet<string> jobIdsSeenNotDone = new HashSet<string>();

    // Token: 0x04000244 RID: 580
    private readonly HashSet<string> jobIdsContinued = new HashSet<string>();
  }
}
