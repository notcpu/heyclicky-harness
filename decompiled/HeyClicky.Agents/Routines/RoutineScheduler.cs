using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Agents.Routines
{
  // Token: 0x0200002B RID: 43
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class RoutineScheduler : IDisposable
  {
    // Token: 0x14000012 RID: 18
    // (add) Token: 0x0600046B RID: 1131 RVA: 0x000EB51C File Offset: 0x000EB51C
    // (remove) Token: 0x0600046C RID: 1132 RVA: 0x000EB554 File Offset: 0x000EB554
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler Changed;

    // Token: 0x1700012A RID: 298
    // (get) Token: 0x0600046D RID: 1133 RVA: 0x000EB58C File Offset: 0x000EB58C
    // (set) Token: 0x0600046E RID: 1134 RVA: 0x000EB594 File Offset: 0x000EB594
    [Nullable(2)]
    public Func<DateTimeOffset?> AgentAllowanceResetsAt
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x14000013 RID: 19
    // (add) Token: 0x0600046F RID: 1135 RVA: 0x000EB5A0 File Offset: 0x000EB5A0
    // (remove) Token: 0x06000470 RID: 1136 RVA: 0x000EB5D8 File Offset: 0x000EB5D8
    [Nullable(new byte[] { 2, 1, 2, 1, 1, 2 })]
    [field: Nullable(new byte[] { 2, 1, 2, 1, 1, 2 })]
    public event Action<string, string, IReadOnlyDictionary<string, object>> ProductEvent;

    // Token: 0x06000471 RID: 1137 RVA: 0x000EB610 File Offset: 0x000EB610
    private void Track(string productEvent, [Nullable(2)] string clickySlug, [Nullable(new byte[] { 1, 1, 2 })] Dictionary<string, object> properties)
    {
      try
      {
        Action<string, string, IReadOnlyDictionary<string, object>> productEvent2 = this.ProductEvent;
        if (productEvent2 != null)
        {
          productEvent2(productEvent, clickySlug, properties);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Routines", "analytics " + productEvent + ": " + ex.Message);
      }
    }

    // Token: 0x06000472 RID: 1138 RVA: 0x000EB668 File Offset: 0x000EB668
    private static int Minutes(double intervalSeconds)
    {
      return (int)(intervalSeconds / 60.0);
    }

    // Token: 0x06000473 RID: 1139 RVA: 0x000EB678 File Offset: 0x000EB678
    internal RoutineScheduler(string stateDirectory, IRoutineHost host)
    {
      Directory.CreateDirectory(stateDirectory);
      this.path = Path.Combine(stateDirectory, "routines.json");
      this.host = host;
      this.routines = this.Load();
    }

    // Token: 0x06000474 RID: 1140 RVA: 0x000EB6D8 File Offset: 0x000EB6D8
    private List<ClickyRoutine> Load()
    {
      try
      {
        if (File.Exists(this.path))
        {
          return JsonSerializer.Deserialize<List<ClickyRoutine>>(File.ReadAllText(this.path), RoutineScheduler.Json) ?? new List<ClickyRoutine>();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Routines", "routines.json unreadable: " + ex.Message);
      }
      return new List<ClickyRoutine>();
    }

    // Token: 0x06000475 RID: 1141 RVA: 0x000EB74C File Offset: 0x000EB74C
    private void SaveLocked()
    {
      string text = this.path + ".tmp";
      File.WriteAllText(text, JsonSerializer.Serialize<List<ClickyRoutine>>(this.routines, RoutineScheduler.Json));
      File.Move(text, this.path, true);
      RoutineKeepAwake.Update(this.routines.Any<ClickyRoutine>((ClickyRoutine r) => !r.IsPaused));
    }

    // Token: 0x06000476 RID: 1142 RVA: 0x000EB7BC File Offset: 0x000EB7BC
    private void Mutate(string id, Action<ClickyRoutine> change)
    {
      object obj = this.gate;
      lock (obj)
      {
        ClickyRoutine clickyRoutine = this.routines.FirstOrDefault<ClickyRoutine>((ClickyRoutine x) => x.Id == id);
        if (clickyRoutine == null)
        {
          return;
        }
        change(clickyRoutine);
        this.SaveLocked();
      }
      EventHandler changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed(this, EventArgs.Empty);
    }

    // Token: 0x1700012B RID: 299
    // (get) Token: 0x06000477 RID: 1143 RVA: 0x000EB844 File Offset: 0x000EB844
    public IReadOnlyList<ClickyRoutine> Routines
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<ClickyRoutine> readOnlyList;
        lock (obj)
        {
          readOnlyList = this.routines.ToList<ClickyRoutine>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x06000478 RID: 1144 RVA: 0x000EB88C File Offset: 0x000EB88C
    public IReadOnlyList<ClickyRoutine> ForClicky(string slug)
    {
      object obj = this.gate;
      IReadOnlyList<ClickyRoutine> readOnlyList;
      lock (obj)
      {
        readOnlyList = (from r in this.routines
          where r.JobId == null && r.ClickySlug == slug
          orderby r.CreatedAt
          select r).ToList<ClickyRoutine>();
      }
      return readOnlyList;
    }

    // Token: 0x06000479 RID: 1145 RVA: 0x000EB918 File Offset: 0x000EB918
    public IReadOnlyList<ClickyRoutine> ForJob(string jobId)
    {
      object obj = this.gate;
      IReadOnlyList<ClickyRoutine> readOnlyList;
      lock (obj)
      {
        readOnlyList = (from r in this.routines
          where r.JobId == jobId
          orderby r.CreatedAt
          select r).ToList<ClickyRoutine>();
      }
      return readOnlyList;
    }

    // Token: 0x0600047A RID: 1146 RVA: 0x000EB9A4 File Offset: 0x000EB9A4
    public ISet<string> JobIdsWithRoutines()
    {
      object obj = this.gate;
      ISet<string> set;
      lock (obj)
      {
        set = this.routines.Select<ClickyRoutine, string>((ClickyRoutine r) => r.JobId).OfType<string>().ToHashSet<string>();
      }
      return set;
    }

    // Token: 0x0600047B RID: 1147 RVA: 0x000EBA14 File Offset: 0x000EBA14
    public void RemoveAllJobRoutinesForSignOut()
    {
      object obj = this.gate;
      lock (obj)
      {
        if (!this.routines.Any<ClickyRoutine>((ClickyRoutine r) => r.JobId != null))
        {
          return;
        }
        this.routines.RemoveAll((ClickyRoutine r) => r.JobId != null);
        this.SaveLocked();
      }
      EventHandler changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed(this, EventArgs.Empty);
    }

    // Token: 0x0600047C RID: 1148 RVA: 0x000EBAC4 File Offset: 0x000EBAC4
    private bool IsOwnerBusy(ClickyRoutine r)
    {
      string jobId = r.JobId;
      if (jobId == null)
      {
        return this.host.IsClickyBusy(r.ClickySlug);
      }
      return this.host.IsJobBusy(jobId);
    }

    // Token: 0x0600047D RID: 1149 RVA: 0x000EBAFC File Offset: 0x000EBAFC
    public void Start()
    {
      if (this.timer != null)
      {
        return;
      }
      this.SettleOverdue(DateTimeOffset.Now);
      this.timer = new Timer(delegate(object _)
      {
        _ = this.EvaluateAsync();
      }, null, TimeSpan.FromSeconds(5L), RoutineScheduler.PollInterval);
      string text = "Routines";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 1);
      defaultInterpolatedStringHandler.AppendLiteral("scheduler started with ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(this.Routines.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" routine(s)");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x0600047E RID: 1150 RVA: 0x000EBB88 File Offset: 0x000EBB88
    public void Stop()
    {
      RoutineKeepAwake.Update(false);
      Timer timer = this.timer;
      if (timer != null)
      {
        timer.Dispose();
      }
      this.timer = null;
    }

    // Token: 0x0600047F RID: 1151 RVA: 0x000EBBA8 File Offset: 0x000EBBA8
    public void SettleOverdue(DateTimeOffset now)
    {
      object obj = this.gate;
      lock (obj)
      {
        bool flag2 = false;
        IEnumerable<ClickyRoutine> enumerable = this.routines;
        Func<ClickyRoutine, bool> <>9__0;
        Func<ClickyRoutine, bool> func;
        if ((func = <>9__0) == null)
        {
          func = (<>9__0 = (ClickyRoutine r) => !r.IsPaused && r.NextRunAt < now);
        }
        foreach (ClickyRoutine clickyRoutine in enumerable.Where<ClickyRoutine>(func))
        {
          clickyRoutine.NextRunAt = now + RoutineScheduler.OverdueSettleDelay;
          flag2 = true;
        }
        if (flag2)
        {
          this.SaveLocked();
        }
      }
    }

    // Token: 0x06000480 RID: 1152 RVA: 0x000EBC70 File Offset: 0x000EBC70
    public void MoveClickyRoutinesOntoJobs()
    {
      object obj = this.moveGate;
      lock (obj)
      {
        this.MoveClickyRoutinesOntoJobsLocked();
      }
    }

    // Token: 0x06000481 RID: 1153 RVA: 0x000EBCB0 File Offset: 0x000EBCB0
    private void MoveClickyRoutinesOntoJobsLocked()
    {
      object obj = this.gate;
      List<ClickyRoutine> list;
      lock (obj)
      {
        list = this.routines.Where<ClickyRoutine>((ClickyRoutine r) => r.IsSavedOnAClicky).ToList<ClickyRoutine>();
      }
      bool flag2 = false;
      using (List<ClickyRoutine>.Enumerator enumerator = list.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ClickyRoutine routine = enumerator.Current;
          if (this.host.ClickyExists(routine.ClickySlug) && !this.host.IsClickyBusy(routine.ClickySlug))
          {
            string text;
            try
            {
              text = this.host.CreateJobForMovedRoutine(routine);
            }
            catch (Exception ex)
            {
              string text2 = "Routines";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 3);
              defaultInterpolatedStringHandler.AppendLiteral("routine ");
              defaultInterpolatedStringHandler.AppendFormatted(routine.Id);
              defaultInterpolatedStringHandler.AppendLiteral(" not moved off Clicky ");
              defaultInterpolatedStringHandler.AppendFormatted(routine.ClickySlug);
              defaultInterpolatedStringHandler.AppendLiteral(": ");
              defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
              Log.Warn(text2, defaultInterpolatedStringHandler.ToStringAndClear());
              continue;
            }
            if (text != null)
            {
              obj = this.gate;
              lock (obj)
              {
                ClickyRoutine clickyRoutine = this.routines.FirstOrDefault<ClickyRoutine>((ClickyRoutine r) => r.Id == routine.Id);
                if (clickyRoutine == null || clickyRoutine.JobId != null)
                {
                  continue;
                }
                clickyRoutine.JobId = text;
                this.SaveLocked();
              }
              flag2 = true;
              this.host.RetireClickysNoteForMovedRoutineJob(text, false);
              string text3 = "Routines";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(37, 3);
              defaultInterpolatedStringHandler.AppendLiteral("routine ");
              defaultInterpolatedStringHandler.AppendFormatted(routine.Id);
              defaultInterpolatedStringHandler.AppendLiteral(" moved from Clicky ");
              defaultInterpolatedStringHandler.AppendFormatted(routine.ClickySlug);
              defaultInterpolatedStringHandler.AppendLiteral(" onto job ");
              defaultInterpolatedStringHandler.AppendFormatted(text);
              Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
              string text4 = "clicky_routine_updated";
              string text5 = null;
              Dictionary<string, object> dictionary = new Dictionary<string, object>();
              dictionary["routine_id"] = routine.Id;
              dictionary["is_job"] = true;
              dictionary["change"] = "moved_from_clicky";
              this.Track(text4, text5, dictionary);
            }
          }
        }
      }
      if (flag2)
      {
        EventHandler changed = this.Changed;
        if (changed != null)
        {
          changed(this, EventArgs.Empty);
        }
      }
      obj = this.gate;
      List<string> list2;
      lock (obj)
      {
        list2 = (from r in this.routines
          where r.JobId != null && r.ClickySlug.Length > 0
          select r.JobId).ToList<string>();
      }
      foreach (string text6 in list2)
      {
        this.host.RetireClickysNoteForMovedRoutineJob(text6, true);
      }
    }

    // Token: 0x06000482 RID: 1154 RVA: 0x000EC098 File Offset: 0x000EC098
    public async Task EvaluateAsync()
    {
      if (Interlocked.Exchange(ref this.evaluating, 1) != 1)
      {
        try
        {
          RoutineScheduler.<>c__DisplayClass42_0 CS$<>8__locals1 = new RoutineScheduler.<>c__DisplayClass42_0();
          object obj = this.gate;
          bool flag2;
          lock (obj)
          {
            flag2 = this.routines.Any<ClickyRoutine>((ClickyRoutine r) => !r.IsPaused);
          }
          RoutineKeepAwake.Update(flag2);
          if (this.host.IsSignedIn)
          {
            this.MoveClickyRoutinesOntoJobs();
            this.host.BackfillWorkReportsIfNeeded();
            CS$<>8__locals1.now = DateTimeOffset.Now;
            this.ReconcileUnfinishedRuns(CS$<>8__locals1.now);
            obj = this.gate;
            List<ClickyRoutine> list;
            lock (obj)
            {
              list = this.routines.Where<ClickyRoutine>((ClickyRoutine r) => !r.IsPaused && r.NextRunAt <= CS$<>8__locals1.now).ToList<ClickyRoutine>();
            }
            if (list.Count != 0)
            {
              if (!NetworkInterface.GetIsNetworkAvailable())
              {
                this.MarkWaitingForNetwork(list, CS$<>8__locals1.now, "offline");
              }
              else if (this.Wake.IsInDarkWake)
              {
                this.MarkWaitingForNetwork(list, CS$<>8__locals1.now, "display off after sleep");
              }
              else if (this.Wake.ShouldHoldForFullWake(CS$<>8__locals1.now, RoutineScheduler.OverdueSettleDelay))
              {
                string text = "Routines";
                DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(58, 1);
                defaultInterpolatedStringHandler.AppendLiteral("waking: holding ");
                defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
                defaultInterpolatedStringHandler.AppendLiteral(" due routine(s) until the wake settle runs");
                Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
              }
              else
              {
                foreach (ClickyRoutine clickyRoutine in list)
                {
                  await this.LaunchRunAsync(clickyRoutine.Id, CS$<>8__locals1.now, true).ConfigureAwait(false);
                }
                List<ClickyRoutine>.Enumerator enumerator = default(List<ClickyRoutine>.Enumerator);
                CS$<>8__locals1 = null;
              }
            }
          }
        }
        catch (Exception ex)
        {
          Log.Error("Routines", "evaluation failed", ex);
        }
        finally
        {
          Volatile.Write(ref this.evaluating, 0);
        }
      }
    }

    // Token: 0x06000483 RID: 1155 RVA: 0x000EC0DC File Offset: 0x000EC0DC
    private void MarkWaitingForNetwork(List<ClickyRoutine> due, DateTimeOffset now, string reason)
    {
      List<ClickyRoutine> list = due.Where<ClickyRoutine>((ClickyRoutine r) => r.WaitingForNetworkSince == null).ToList<ClickyRoutine>();
      if (list.Count == 0)
      {
        return;
      }
      Action<ClickyRoutine> <>9__1;
      foreach (ClickyRoutine clickyRoutine in list)
      {
        string id = clickyRoutine.Id;
        Action<ClickyRoutine> action;
        if ((action = <>9__1) == null)
        {
          action = (<>9__1 = delegate(ClickyRoutine x)
          {
            x.NoteWaitingForNetwork(now);
          });
        }
        this.Mutate(id, action);
      }
      string text = "Routines";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 2);
      defaultInterpolatedStringHandler.AppendFormatted(reason);
      defaultInterpolatedStringHandler.AppendLiteral(": ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" due routine(s) now waiting for the internet");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x1700012C RID: 300
    // (get) Token: 0x06000484 RID: 1156 RVA: 0x000EC1DC File Offset: 0x000EC1DC
    public RoutineWakeGate Wake { get; } = new RoutineWakeGate();

    // Token: 0x06000485 RID: 1157 RVA: 0x000EC1E4 File Offset: 0x000EC1E4
    public void NoteSystemSleeping()
    {
      this.Wake.NoteSleeping();
      Log.Info("Routines", "going to sleep: due routines hold until the display is back");
    }

    // Token: 0x06000486 RID: 1158 RVA: 0x000EC200 File Offset: 0x000EC200
    public void NoteDisplayPower(bool isOn)
    {
      if (this.Wake.NoteDisplay(isOn))
      {
        this.HandleFullWake("display on");
      }
    }

    // Token: 0x06000487 RID: 1159 RVA: 0x000EC21C File Offset: 0x000EC21C
    public void HandleFullWake(string source)
    {
      if (this.Wake.ConsumeFullWake())
      {
        Log.Info("Routines", source + ": settling overdue routines, then evaluating");
        this.SettleOverdue(DateTimeOffset.Now);
      }
      else
      {
        Log.Info("Routines", source + ": wake already settled, evaluating");
      }
      if (this.timer != null)
      {
        this.EvaluateAsync();
      }
    }

    // Token: 0x06000488 RID: 1160 RVA: 0x000EC27C File Offset: 0x000EC27C
    private void ReconcileUnfinishedRuns(DateTimeOffset now)
    {
      object obj = this.gate;
      List<ClickyRoutine> list;
      lock (obj)
      {
        list = this.routines.Where<ClickyRoutine>(delegate(ClickyRoutine r)
        {
          DateTimeOffset? lastRunStartedAt = r.LastRunStartedAt;
          if (lastRunStartedAt != null)
          {
            DateTimeOffset valueOrDefault = lastRunStartedAt.GetValueOrDefault();
            if (r.LastRunFinishedAt == null)
            {
              return now - valueOrDefault > RoutineScheduler.StaleRunAfter;
            }
          }
          return false;
        }).ToList<ClickyRoutine>();
      }
      Action<ClickyRoutine> <>9__1;
      foreach (ClickyRoutine clickyRoutine in list)
      {
        if (!this.IsOwnerBusy(clickyRoutine))
        {
          string id = clickyRoutine.Id;
          Action<ClickyRoutine> action;
          if ((action = <>9__1) == null)
          {
            action = (<>9__1 = delegate(ClickyRoutine x)
            {
              x.RecordOutcome(now, "The previous run ended before its result was saved.", true, true, true);
            });
          }
          this.Mutate(id, action);
        }
      }
    }

    // Token: 0x06000489 RID: 1161 RVA: 0x000EC350 File Offset: 0x000EC350
    private async Task<bool> LaunchRunAsync(string routineId, DateTimeOffset now, bool scheduled)
    {
      object obj = this.gate;
      ClickyRoutine routine;
      lock (obj)
      {
        routine = this.routines.FirstOrDefault<ClickyRoutine>((ClickyRoutine r) => r.Id == routineId);
      }
      bool flag2;
      if (routine == null)
      {
        flag2 = false;
      }
      else
      {
        string jobId = routine.JobId;
        if (jobId != null)
        {
          flag2 = await this.LaunchJobRunAsync(routine, jobId, now, scheduled).ConfigureAwait(false);
        }
        else if (!this.host.ClickyExists(routine.ClickySlug))
        {
          string text = "Routines";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 2);
          defaultInterpolatedStringHandler.AppendLiteral("routine ");
          defaultInterpolatedStringHandler.AppendFormatted(routineId);
          defaultInterpolatedStringHandler.AppendLiteral(" paused: its Clicky ");
          defaultInterpolatedStringHandler.AppendFormatted(routine.ClickySlug);
          defaultInterpolatedStringHandler.AppendLiteral(" is gone");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
          this.Mutate(routineId, delegate(ClickyRoutine r)
          {
            r.IsPaused = true;
          });
          flag2 = false;
        }
        else if (this.host.IsClickyBusy(routine.ClickySlug) || this.host.IsClickyAwaitingExtraUsage(routine.ClickySlug))
        {
          this.Mutate(routineId, delegate(ClickyRoutine r)
          {
            r.NextRunAt = now + RoutineScheduler.BlockedRetryDelay;
          });
          flag2 = false;
        }
        else
        {
          int runNumber = routine.RunCount + 1;
          string text2 = RoutineScheduler.RunPrompt(routine, runNumber, now);
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(144, 3);
          defaultInterpolatedStringHandler.AppendLiteral("This turn is run #");
          defaultInterpolatedStringHandler.AppendFormatted<int>(runNumber);
          defaultInterpolatedStringHandler.AppendLiteral(" of the routine \"");
          defaultInterpolatedStringHandler.AppendFormatted(routine.Title);
          defaultInterpolatedStringHandler.AppendLiteral("\" (");
          defaultInterpolatedStringHandler.AppendFormatted(routine.CadenceLabel.ToLowerInvariant());
          defaultInterpolatedStringHandler.AppendLiteral(") that the user set up, so HeyClicky is launching it automatically with the user's standing instructions. ");
          string text3 = defaultInterpolatedStringHandler.ToStringAndClear() + ((runNumber > 1) ? "Earlier runs of this routine are in this same conversation above; build on them and report what is new rather than repeating them. " : "");
          string taskId = null;
          try
          {
            taskId = await this.host.LaunchRoutineRunAsync(routine, runNumber, text2, text3).ConfigureAwait(false);
          }
          catch (Exception ex)
          {
            Log.Error("Routines", "routine " + routineId + " launch failed", ex);
          }
          if (taskId != null)
          {
            this.Mutate(routineId, delegate(ClickyRoutine r)
            {
              r.RecordDispatch(now, taskId);
            });
            string text4 = "Routines";
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 3);
            defaultInterpolatedStringHandler.AppendLiteral("routine ");
            defaultInterpolatedStringHandler.AppendFormatted(routineId);
            defaultInterpolatedStringHandler.AppendLiteral(" run #");
            defaultInterpolatedStringHandler.AppendFormatted<int>(runNumber);
            defaultInterpolatedStringHandler.AppendLiteral(" dispatched to ");
            defaultInterpolatedStringHandler.AppendFormatted(routine.ClickySlug);
            Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
            string text5 = "clicky_routine_run_dispatched";
            string clickySlug = routine.ClickySlug;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary["routine_id"] = routineId;
            dictionary["run_number"] = runNumber;
            dictionary["trigger"] = (scheduled ? "scheduled" : "manual");
            this.Track(text5, clickySlug, dictionary);
            flag2 = true;
          }
          else
          {
            this.Mutate(routineId, delegate(ClickyRoutine r)
            {
              r.ScheduleRetry(now);
            });
            flag2 = false;
          }
        }
      }
      return flag2;
    }

    // Token: 0x0600048A RID: 1162 RVA: 0x000EC3AC File Offset: 0x000EC3AC
    private async Task<bool> LaunchJobRunAsync(ClickyRoutine routine, string jobId, DateTimeOffset now, bool scheduled)
    {
      bool flag;
      if (!this.host.JobExists(jobId))
      {
        string text = "Routines";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 2);
        defaultInterpolatedStringHandler.AppendLiteral("routine ");
        defaultInterpolatedStringHandler.AppendFormatted(routine.Id);
        defaultInterpolatedStringHandler.AppendLiteral(" paused — its job ");
        defaultInterpolatedStringHandler.AppendFormatted(jobId);
        defaultInterpolatedStringHandler.AppendLiteral(" is gone");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        this.Mutate(routine.Id, delegate(ClickyRoutine r)
        {
          r.IsPaused = true;
        });
        flag = false;
      }
      else if (this.host.IsJobBusy(jobId))
      {
        string text2 = "Routines";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(51, 2);
        defaultInterpolatedStringHandler.AppendLiteral("routine ");
        defaultInterpolatedStringHandler.AppendFormatted(routine.Id);
        defaultInterpolatedStringHandler.AppendLiteral(" skipped — job ");
        defaultInterpolatedStringHandler.AppendFormatted(jobId);
        defaultInterpolatedStringHandler.AppendLiteral(" is busy; retrying next tick");
        Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
        this.Mutate(routine.Id, delegate(ClickyRoutine r)
        {
          r.NextRunAt = now + RoutineScheduler.BlockedRetryDelay;
        });
        flag = false;
      }
      else if (this.host.IsJobAwaitingExtraUsage(jobId))
      {
        this.Mutate(routine.Id, delegate(ClickyRoutine r)
        {
          r.NextRunAt = now + RoutineScheduler.BlockedRetryDelay;
        });
        flag = false;
      }
      else
      {
        int runNumber = routine.RunCount + 1;
        string text3 = RoutineScheduler.RunPrompt(routine, runNumber, now);
        string taskId = null;
        try
        {
          string text4 = await this.host.LaunchJobRoutineRunAsync(routine, runNumber, text3, scheduled).ConfigureAwait(false);
          taskId = text4;
        }
        catch (Exception ex)
        {
          Log.Error("Routines", "routine " + routine.Id + " launch failed", ex);
        }
        if (taskId != null)
        {
          string text5 = "clicky_routine_run_dispatched";
          string text6 = null;
          Dictionary<string, object> dictionary = new Dictionary<string, object>();
          dictionary["routine_id"] = routine.Id;
          dictionary["run_number"] = runNumber;
          dictionary["is_job"] = true;
          dictionary["trigger"] = (scheduled ? "scheduled" : "manual");
          this.Track(text5, text6, dictionary);
          this.Mutate(routine.Id, delegate(ClickyRoutine r)
          {
            r.RecordDispatch(now, taskId);
          });
          string text7 = "Routines";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 3);
          defaultInterpolatedStringHandler.AppendLiteral("routine ");
          defaultInterpolatedStringHandler.AppendFormatted(routine.Id);
          defaultInterpolatedStringHandler.AppendLiteral(" run #");
          defaultInterpolatedStringHandler.AppendFormatted<int>(runNumber);
          defaultInterpolatedStringHandler.AppendLiteral(" dispatched to job ");
          defaultInterpolatedStringHandler.AppendFormatted(jobId);
          Log.Info(text7, defaultInterpolatedStringHandler.ToStringAndClear());
          flag = true;
        }
        else
        {
          string text8 = "Routines";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(56, 4);
          defaultInterpolatedStringHandler.AppendLiteral("routine ");
          defaultInterpolatedStringHandler.AppendFormatted(routine.Id);
          defaultInterpolatedStringHandler.AppendLiteral(" launch refused for job ");
          defaultInterpolatedStringHandler.AppendFormatted(jobId);
          defaultInterpolatedStringHandler.AppendLiteral(" (scheduled=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(scheduled);
          defaultInterpolatedStringHandler.AppendLiteral(", signedIn=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(this.host.IsSignedIn);
          defaultInterpolatedStringHandler.AppendLiteral(")");
          Log.Info(text8, defaultInterpolatedStringHandler.ToStringAndClear());
          this.Mutate(routine.Id, delegate(ClickyRoutine r)
          {
            r.ScheduleRetry(now);
          });
          flag = false;
        }
      }
      return flag;
    }

    // Token: 0x0600048B RID: 1163 RVA: 0x000EC410 File Offset: 0x000EC410
    private static string RunPrompt(ClickyRoutine r, int runNumber, DateTimeOffset now)
    {
      List<string> list = new List<string>
      {
        ClickyRoutine.PromptHeaderLine(r.Title, runNumber, r.CadenceLabel, now),
        "",
        r.TaskPrompt,
        ""
      };
      DateTimeOffset? lastRunStartedAt = r.LastRunStartedAt;
      if (lastRunStartedAt != null)
      {
        DateTimeOffset valueOrDefault = lastRunStartedAt.GetValueOrDefault();
        if (runNumber > 1)
        {
          List<string> list2 = list;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(200, 1);
          defaultInterpolatedStringHandler.AppendLiteral("This is a repeating routine; the previous run started ");
          defaultInterpolatedStringHandler.AppendFormatted<DateTimeOffset>(valueOrDefault, "MMM d, yyyy h:mm tt");
          defaultInterpolatedStringHandler.AppendLiteral(" and its answer is above in this conversation. Report what is NEW or changed since then, keep it short, and say so in one line if nothing changed.");
          list2.Add(defaultInterpolatedStringHandler.ToStringAndClear());
          goto IL_00A9;
        }
      }
      list.Add("This is the first run of a repeating routine. Do the task fully this time; later runs will report only what changed.");
      IL_00A9:
      if (r.PendingRetryAt != null && r.LastRunFailed)
      {
        list.Add("The previous attempt failed or was interrupted. Check the conversation and existing results first, and finish only what is still missing. Do not repeat any message, post, upload, or other external action that already succeeded. If an external action's outcome is uncertain, verify it before trying again.");
      }
      list.Add("Post the answer in this chat as your reply. Do not create files unless the task itself asks for one.");
      return string.Join("\n", list);
    }

    // Token: 0x0600048C RID: 1164 RVA: 0x000EC500 File Offset: 0x000EC500
    public ClickyRoutine CreateRoutine(string clickySlug, string title, string taskPrompt, int intervalMinutes, bool runImmediately, [Nullable(2)] string jobId = null)
    {
      ClickyRoutine clickyRoutine = ClickyRoutine.Create(clickySlug, title, ClickyRoutine.TaskPromptStrippingLeadingCadence(taskPrompt), intervalMinutes, jobId);
      if (!runImmediately)
      {
        clickyRoutine.NextRunAt = DateTimeOffset.Now.AddSeconds(clickyRoutine.IntervalSeconds);
      }
      object obj = this.gate;
      lock (obj)
      {
        this.routines.Add(clickyRoutine);
        this.SaveLocked();
      }
      EventHandler changed = this.Changed;
      if (changed != null)
      {
        changed(this, EventArgs.Empty);
      }
      this.TrackCreated(clickyRoutine, runImmediately);
      string text = "Routines";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(22, 3);
      defaultInterpolatedStringHandler.AppendLiteral("created routine ");
      defaultInterpolatedStringHandler.AppendFormatted(clickyRoutine.Id);
      defaultInterpolatedStringHandler.AppendLiteral(" for ");
      defaultInterpolatedStringHandler.AppendFormatted(clickyRoutine.OwnerLogName);
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(clickyRoutine.CadenceLabel);
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      if (runImmediately)
      {
        this.EvaluateAsync();
      }
      return clickyRoutine;
    }

    // Token: 0x0600048D RID: 1165 RVA: 0x000EC610 File Offset: 0x000EC610
    private void TrackCreated(ClickyRoutine routine, bool runImmediately)
    {
      string text = "clicky_routine_created";
      string owningClickySlug = routine.OwningClickySlug;
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary["routine_id"] = routine.Id;
      dictionary["interval_minutes"] = RoutineScheduler.Minutes(routine.IntervalSeconds);
      dictionary["run_immediately"] = runImmediately;
      dictionary["is_job"] = routine.JobId != null;
      this.Track(text, owningClickySlug, dictionary);
    }

    // Token: 0x0600048E RID: 1166 RVA: 0x000EC68C File Offset: 0x000EC68C
    public void SetInterval(string routineId, int minutes, string source = "profile")
    {
      object obj = this.gate;
      ClickyRoutine clickyRoutine;
      lock (obj)
      {
        clickyRoutine = this.routines.FirstOrDefault<ClickyRoutine>((ClickyRoutine r) => r.Id == routineId);
      }
      if (clickyRoutine == null)
      {
        return;
      }
      int clamped = Math.Clamp(minutes, 2, 10080);
      if (clamped == clickyRoutine.IntervalMinutes)
      {
        return;
      }
      string text = "clicky_routine_updated";
      string owningClickySlug = clickyRoutine.OwningClickySlug;
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary["routine_id"] = routineId;
      dictionary["interval_minutes"] = clamped;
      dictionary["previous_interval_minutes"] = clickyRoutine.IntervalMinutes;
      dictionary["source"] = source;
      this.Track(text, owningClickySlug, dictionary);
      this.Mutate(routineId, delegate(ClickyRoutine r)
      {
        r.IntervalSeconds = (double)(clamped * 60);
        if (!r.HasSetTime)
        {
          r.ScheduledMinuteOfDay = null;
          r.ScheduledWeekday = null;
        }
        r.ScheduledWeekdays = null;
        RoutineScheduler.RescheduleAfterCadenceChange(r);
      });
    }

    // Token: 0x0600048F RID: 1167 RVA: 0x000EC788 File Offset: 0x000EC788
    public void SetSchedule(string routineId, int minutes, int? minuteOfDay, [Nullable(2)] IReadOnlyList<int> weekdays, string source = "settings")
    {
      object obj = this.gate;
      ClickyRoutine clickyRoutine;
      lock (obj)
      {
        clickyRoutine = this.routines.FirstOrDefault<ClickyRoutine>((ClickyRoutine r) => r.Id == routineId);
      }
      if (clickyRoutine == null)
      {
        return;
      }
      ClickyRoutine.Schedule schedule = ClickyRoutine.NormalizedSchedule(minutes, minuteOfDay, weekdays);
      if (schedule.Minutes == clickyRoutine.IntervalMinutes)
      {
        int? num = schedule.MinuteOfDay;
        int? num2 = clickyRoutine.ScheduledMinuteOfDay;
        if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
        {
          num2 = schedule.Weekday;
          num = clickyRoutine.ScheduledWeekday;
          if (((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null))) && RoutineScheduler.SameDays(schedule.Weekdays, clickyRoutine.ScheduledWeekdays))
          {
            return;
          }
        }
      }
      string text = "clicky_routine_updated";
      string owningClickySlug = clickyRoutine.OwningClickySlug;
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary["routine_id"] = routineId;
      dictionary["interval_minutes"] = schedule.Minutes;
      dictionary["previous_interval_minutes"] = clickyRoutine.IntervalMinutes;
      dictionary["has_set_time"] = schedule.MinuteOfDay != null;
      string text2 = "day_count";
      IReadOnlyList<int> weekdays2 = schedule.Weekdays;
      dictionary[text2] = ((weekdays2 != null) ? weekdays2.Count : ((schedule.Weekday != null) ? 1 : 0));
      dictionary["source"] = source;
      this.Track(text, owningClickySlug, dictionary);
      this.Mutate(routineId, delegate(ClickyRoutine r)
      {
        r.IntervalSeconds = (double)(schedule.Minutes * 60);
        r.ScheduledMinuteOfDay = schedule.MinuteOfDay;
        r.ScheduledWeekday = schedule.Weekday;
        IReadOnlyList<int> weekdays3 = schedule.Weekdays;
        r.ScheduledWeekdays = ((weekdays3 != null) ? weekdays3.ToList<int>() : null);
        RoutineScheduler.RescheduleAfterCadenceChange(r);
      });
    }

    // Token: 0x06000490 RID: 1168 RVA: 0x000EC980 File Offset: 0x000EC980
    [NullableContext(2)]
    private static bool SameDays(IReadOnlyList<int> a, IReadOnlyList<int> b)
    {
      if (a != null && b != null)
      {
        return a.SequenceEqual<int>(b);
      }
      return a == null && b == null;
    }

    // Token: 0x06000491 RID: 1169 RVA: 0x000EC99C File Offset: 0x000EC99C
    private static void RescheduleAfterCadenceChange(ClickyRoutine routine)
    {
      DateTimeOffset now = DateTimeOffset.Now;
      DateTimeOffset? dateTimeOffset = routine.NextSetTime(now.AddSeconds(60.0));
      if (dateTimeOffset != null)
      {
        DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
        routine.NextRunAt = valueOrDefault;
      }
      else
      {
        DateTimeOffset dateTimeOffset2 = routine.LastRunStartedAt.GetValueOrDefault(now).AddSeconds(routine.IntervalSeconds);
        routine.NextRunAt = ((dateTimeOffset2 > now.AddSeconds(60.0)) ? dateTimeOffset2 : now.AddSeconds(60.0));
      }
      routine.PendingRetryAt = null;
    }

    // Token: 0x06000492 RID: 1170 RVA: 0x000ECA40 File Offset: 0x000ECA40
    public void UpdateDetails(string routineId, string title, string taskPrompt, string source = "settings")
    {
      string trimmedTitle = title.Trim();
      string trimmedTask = taskPrompt.Trim();
      object obj = this.gate;
      ClickyRoutine clickyRoutine;
      lock (obj)
      {
        clickyRoutine = this.routines.FirstOrDefault<ClickyRoutine>((ClickyRoutine r) => r.Id == routineId);
      }
      if (clickyRoutine == null || trimmedTitle.Length == 0 || trimmedTask.Length == 0 || (trimmedTitle == clickyRoutine.Title && trimmedTask == clickyRoutine.TaskPrompt))
      {
        return;
      }
      string text = "clicky_routine_updated";
      string owningClickySlug = clickyRoutine.OwningClickySlug;
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary["routine_id"] = routineId;
      dictionary["changed_title"] = trimmedTitle != clickyRoutine.Title;
      dictionary["changed_task"] = trimmedTask != clickyRoutine.TaskPrompt;
      dictionary["source"] = source;
      this.Track(text, owningClickySlug, dictionary);
      this.Mutate(routineId, delegate(ClickyRoutine r)
      {
        r.Title = trimmedTitle;
        r.TaskPrompt = trimmedTask;
      });
    }

    // Token: 0x06000493 RID: 1171 RVA: 0x000ECB8C File Offset: 0x000ECB8C
    public ClickyRoutine RegisterApprovedSuggestion(string clickySlug, string title, string taskPrompt, int intervalMinutes, [Nullable(2)] string taskId, [Nullable(2)] string jobId = null)
    {
      DateTimeOffset now = DateTimeOffset.Now;
      double seconds = (double)(Math.Clamp(intervalMinutes, 2, 10080) * 60);
      if (jobId != null)
      {
        ClickyRoutine clickyRoutine = this.ForJob(jobId).FirstOrDefault<ClickyRoutine>();
        if (clickyRoutine != null)
        {
          this.Delete(clickyRoutine.Id, "suggestion_restarted");
        }
      }
      ClickyRoutine clickyRoutine2 = ((jobId == null) ? this.ForClicky(clickySlug).FirstOrDefault<ClickyRoutine>((ClickyRoutine r) => string.Equals(r.Title, title, StringComparison.OrdinalIgnoreCase)) : null);
      if (clickyRoutine2 != null)
      {
        this.Mutate(clickyRoutine2.Id, delegate(ClickyRoutine r)
        {
          r.TaskPrompt = taskPrompt;
          r.IntervalSeconds = seconds;
          r.IsPaused = false;
          int runCount = r.RunCount;
          r.RunCount = runCount + 1;
          r.LastRunStartedAt = new DateTimeOffset?(now);
          r.LastRunFinishedAt = null;
          r.LastRunFailed = false;
          r.PendingRetryAt = null;
          r.LastRunTaskId = taskId;
          r.NextRunAt = r.RegularNextRunAt(now);
        });
        Log.Info("Routines", "approved suggestion updated routine " + clickyRoutine2.Id + " on " + clickyRoutine2.OwnerLogName);
        return clickyRoutine2;
      }
      ClickyRoutine clickyRoutine3 = ClickyRoutine.Create(clickySlug, title, taskPrompt, intervalMinutes, jobId);
      clickyRoutine3.RunCount = 1;
      clickyRoutine3.LastRunStartedAt = new DateTimeOffset?(now);
      clickyRoutine3.LastRunTaskId = taskId;
      clickyRoutine3.NextRunAt = now.AddSeconds(clickyRoutine3.IntervalSeconds);
      object obj = this.gate;
      lock (obj)
      {
        this.routines.Add(clickyRoutine3);
        this.SaveLocked();
      }
      EventHandler changed = this.Changed;
      if (changed != null)
      {
        changed(this, EventArgs.Empty);
      }
      this.TrackCreated(clickyRoutine3, false);
      string text = "Routines";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 3);
      defaultInterpolatedStringHandler.AppendLiteral("approved suggestion registered routine ");
      defaultInterpolatedStringHandler.AppendFormatted(clickyRoutine3.Id);
      defaultInterpolatedStringHandler.AppendLiteral(" on ");
      defaultInterpolatedStringHandler.AppendFormatted(clickyRoutine3.OwnerLogName);
      defaultInterpolatedStringHandler.AppendLiteral(" (");
      defaultInterpolatedStringHandler.AppendFormatted(clickyRoutine3.CadenceLabel);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      return clickyRoutine3;
    }

    // Token: 0x06000494 RID: 1172 RVA: 0x000ECD88 File Offset: 0x000ECD88
    public void SetPaused(string routineId, bool paused, string source = "profile")
    {
      object obj = this.gate;
      ClickyRoutine clickyRoutine;
      lock (obj)
      {
        clickyRoutine = this.routines.FirstOrDefault<ClickyRoutine>((ClickyRoutine r) => r.Id == routineId);
      }
      if (clickyRoutine != null && clickyRoutine.IsPaused != paused)
      {
        string text = "clicky_routine_pause_changed";
        string owningClickySlug = clickyRoutine.OwningClickySlug;
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary["routine_id"] = routineId;
        dictionary["paused"] = paused;
        dictionary["source"] = source;
        this.Track(text, owningClickySlug, dictionary);
      }
      this.Mutate(routineId, delegate(ClickyRoutine r)
      {
        r.IsPaused = paused;
        if (!paused)
        {
          r.ConsecutiveFailedRunCount = 0;
          if (r.NextRunAt < DateTimeOffset.Now)
          {
            r.NextRunAt = DateTimeOffset.Now;
          }
        }
      });
    }

    // Token: 0x06000495 RID: 1173 RVA: 0x000ECE60 File Offset: 0x000ECE60
    public void Delete(string routineId, string source = "profile")
    {
      object obj = this.gate;
      ClickyRoutine clickyRoutine;
      lock (obj)
      {
        clickyRoutine = this.routines.FirstOrDefault<ClickyRoutine>((ClickyRoutine r) => r.Id == routineId);
        this.routines.RemoveAll((ClickyRoutine r) => r.Id == routineId);
        this.SaveLocked();
      }
      if (clickyRoutine != null)
      {
        string text = "clicky_routine_deleted";
        string owningClickySlug = clickyRoutine.OwningClickySlug;
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary["routine_id"] = routineId;
        dictionary["source"] = source;
        this.Track(text, owningClickySlug, dictionary);
      }
      EventHandler changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed(this, EventArgs.Empty);
    }

    // Token: 0x1700012D RID: 301
    // (get) Token: 0x06000496 RID: 1174 RVA: 0x000ECF2C File Offset: 0x000ECF2C
    public static TimeSpan RunStaleAfter
    {
      get
      {
        return RoutineScheduler.StaleRunAfter;
      }
    }

    // Token: 0x06000497 RID: 1175 RVA: 0x000ECF34 File Offset: 0x000ECF34
    public async Task<bool> RunNowAsync(string routineId)
    {
      bool flag = await this.LaunchRunAsync(routineId, DateTimeOffset.Now, false).ConfigureAwait(false);
      object obj = this.gate;
      string text;
      lock (obj)
      {
        ClickyRoutine clickyRoutine = this.routines.FirstOrDefault<ClickyRoutine>((ClickyRoutine r) => r.Id == routineId);
        text = ((clickyRoutine != null) ? clickyRoutine.OwningClickySlug : null);
      }
      string text2 = "clicky_routine_run_requested";
      string text3 = text;
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary["routine_id"] = routineId;
      dictionary["dispatched"] = flag;
      this.Track(text2, text3, dictionary);
      return flag;
    }

    // Token: 0x06000498 RID: 1176 RVA: 0x000ECF80 File Offset: 0x000ECF80
    public IReadOnlyList<string> PromptLines(string slug)
    {
      return RoutineScheduler.RoutinePromptLines(this.ForClicky(slug));
    }

    // Token: 0x06000499 RID: 1177 RVA: 0x000ECF90 File Offset: 0x000ECF90
    public IReadOnlyList<string> PromptLinesForJob(string jobId)
    {
      return RoutineScheduler.RoutinePromptLines(this.ForJob(jobId));
    }

    // Token: 0x0600049A RID: 1178 RVA: 0x000ECFA0 File Offset: 0x000ECFA0
    private static IReadOnlyList<string> RoutinePromptLines(IReadOnlyList<ClickyRoutine> routines)
    {
      return routines.Select<ClickyRoutine, string>(delegate(ClickyRoutine r)
      {
        string text = string.Join<string>(' ', from l in r.TaskPrompt.Split('\n', StringSplitOptions.None)
          select l.Trim() into l
          where l.Length > 0
          select l);
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 5);
        defaultInterpolatedStringHandler.AppendLiteral("- title: \"");
        defaultInterpolatedStringHandler.AppendFormatted(r.Title);
        defaultInterpolatedStringHandler.AppendLiteral("\" · ");
        defaultInterpolatedStringHandler.AppendFormatted(r.CadenceLabel.ToLowerInvariant());
        defaultInterpolatedStringHandler.AppendLiteral(" · ");
        defaultInterpolatedStringHandler.AppendFormatted(r.IsPaused ? "paused" : "active");
        defaultInterpolatedStringHandler.AppendLiteral(" · ran ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(r.RunCount);
        defaultInterpolatedStringHandler.AppendLiteral("× · task: ");
        defaultInterpolatedStringHandler.AppendFormatted(text);
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }).ToList<string>();
    }

    // Token: 0x0600049B RID: 1179 RVA: 0x000ECFCC File Offset: 0x000ECFCC
    [return: Nullable(2)]
    public string JobRoutineLine(string jobId)
    {
      ClickyRoutine clickyRoutine = this.ForJob(jobId).FirstOrDefault<ClickyRoutine>();
      if (clickyRoutine == null)
      {
        return null;
      }
      return "repeats (routine_id " + clickyRoutine.Id + ")";
    }

    // Token: 0x0600049C RID: 1180 RVA: 0x000ED000 File Offset: 0x000ED000
    [NullableContext(2)]
    internal void NoteTaskEnded(string clickySlug, string jobId, [Nullable(1)] string taskId, string summary, bool failed, bool interrupted, bool awaitingExtraUsage, bool transientConnectivityFailure = false, bool hitAgentMessageLimit = false)
    {
      RoutineScheduler.<>c__DisplayClass71_0 CS$<>8__locals1 = new RoutineScheduler.<>c__DisplayClass71_0();
      CS$<>8__locals1.jobId = jobId;
      CS$<>8__locals1.clickySlug = clickySlug;
      CS$<>8__locals1.taskId = taskId;
      CS$<>8__locals1.summary = summary;
      CS$<>8__locals1.failed = failed;
      CS$<>8__locals1.transientConnectivityFailure = transientConnectivityFailure;
      if (CS$<>8__locals1.clickySlug == null && CS$<>8__locals1.jobId == null)
      {
        return;
      }
      object obj = this.gate;
      ClickyRoutine clickyRoutine;
      lock (obj)
      {
        clickyRoutine = this.routines.FirstOrDefault<ClickyRoutine>((ClickyRoutine r) => ((CS$<>8__locals1.jobId != null) ? (r.JobId == CS$<>8__locals1.jobId) : (r.JobId == null && r.ClickySlug == CS$<>8__locals1.clickySlug)) && r.LastRunStartedAt != null && r.LastRunFinishedAt == null && (r.LastRunTaskId == null || r.LastRunTaskId == CS$<>8__locals1.taskId));
      }
      if (clickyRoutine == null)
      {
        return;
      }
      string owningClickySlug = clickyRoutine.OwningClickySlug;
      string text = "clicky_routine_run_ended";
      string text2 = owningClickySlug;
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary["routine_id"] = clickyRoutine.Id;
      dictionary["run_number"] = clickyRoutine.RunCount;
      dictionary["outcome"] = (interrupted ? "interrupted" : (CS$<>8__locals1.failed ? "failed" : "completed"));
      dictionary["duration_seconds"] = Math.Max(0.0, (DateTimeOffset.Now - (clickyRoutine.LastRunStartedAt ?? DateTimeOffset.Now)).TotalSeconds);
      this.Track(text, text2, dictionary);
      string summary2 = CS$<>8__locals1.summary;
      bool flag = summary2 == "Codex disconnected while background agent tasks were still running." || summary2 == "HeyClicky stopped before the agent task finished.";
      bool flag2 = flag;
      CS$<>8__locals1.shouldRetry = !awaitingExtraUsage && (!interrupted || flag2);
      CS$<>8__locals1.paused = false;
      CS$<>8__locals1.failedInARow = 0;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (hitAgentMessageLimit)
      {
        RoutineScheduler.<>c__DisplayClass71_1 CS$<>8__locals2 = new RoutineScheduler.<>c__DisplayClass71_1();
        CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
        RoutineScheduler.<>c__DisplayClass71_1 CS$<>8__locals3 = CS$<>8__locals2;
        Func<DateTimeOffset?> agentAllowanceResetsAt = this.AgentAllowanceResetsAt;
        CS$<>8__locals3.resetsAt = ((agentAllowanceResetsAt != null) ? agentAllowanceResetsAt() : null);
        CS$<>8__locals2.next = default(DateTimeOffset);
        this.Mutate(clickyRoutine.Id, delegate(ClickyRoutine r)
        {
          r.RecordAgentMessageLimit(DateTimeOffset.Now, CS$<>8__locals2.CS$<>8__locals1.summary, CS$<>8__locals2.resetsAt);
          CS$<>8__locals2.next = r.NextRunAt;
        });
        string text3 = "Routines";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(61, 3);
        defaultInterpolatedStringHandler.AppendLiteral("routine ");
        defaultInterpolatedStringHandler.AppendFormatted(clickyRoutine.Id);
        defaultInterpolatedStringHandler.AppendLiteral(" out of agent messages; next run ");
        defaultInterpolatedStringHandler.AppendFormatted<DateTimeOffset>(CS$<>8__locals2.next, "yyyy-MM-dd HH:mm");
        defaultInterpolatedStringHandler.AppendLiteral(" (allowance resets ");
        RoutineScheduler.<>c__DisplayClass71_1 CS$<>8__locals4 = CS$<>8__locals2;
        defaultInterpolatedStringHandler.AppendFormatted(((CS$<>8__locals4.resetsAt != null) ? CS$<>8__locals4.resetsAt.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm") : null) ?? "unknown");
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
        return;
      }
      this.Mutate(clickyRoutine.Id, delegate(ClickyRoutine r)
      {
        CS$<>8__locals1.paused = r.RecordOutcome(DateTimeOffset.Now, CS$<>8__locals1.summary, CS$<>8__locals1.failed, CS$<>8__locals1.shouldRetry, !CS$<>8__locals1.transientConnectivityFailure);
        CS$<>8__locals1.failedInARow = r.ConsecutiveFailedRunCount;
      });
      string text4 = "Routines";
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 3);
      defaultInterpolatedStringHandler.AppendLiteral("routine ");
      defaultInterpolatedStringHandler.AppendFormatted(clickyRoutine.Id);
      defaultInterpolatedStringHandler.AppendLiteral(" run ended failed=");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.failed);
      defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.paused ? " (paused after repeated failures)" : "");
      Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
      if (CS$<>8__locals1.paused)
      {
        string text5 = "clicky_routine_pause_changed";
        string text6 = owningClickySlug;
        Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
        dictionary2["routine_id"] = clickyRoutine.Id;
        dictionary2["paused"] = true;
        dictionary2["source"] = "repeated_failures";
        dictionary2["failed_runs_in_a_row"] = CS$<>8__locals1.failedInARow;
        this.Track(text5, text6, dictionary2);
      }
    }

    // Token: 0x0600049D RID: 1181 RVA: 0x000ED38C File Offset: 0x000ED38C
    public void Dispose()
    {
      this.Stop();
    }

    // Token: 0x040001A1 RID: 417
    public static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(30L);

    // Token: 0x040001A2 RID: 418
    private static readonly TimeSpan StaleRunAfter = TimeSpan.FromHours(2);

    // Token: 0x040001A3 RID: 419
    private static readonly TimeSpan BlockedRetryDelay = TimeSpan.FromSeconds(60L);

    // Token: 0x040001A4 RID: 420
    private static readonly TimeSpan OverdueSettleDelay = TimeSpan.FromMinutes(3L);

    // Token: 0x040001A5 RID: 421
    private const string InterruptedByDisconnect = "Codex disconnected while background agent tasks were still running.";

    // Token: 0x040001A6 RID: 422
    private const string InterruptedByShutdown = "HeyClicky stopped before the agent task finished.";

    // Token: 0x040001A7 RID: 423
    private static readonly JsonSerializerOptions Json = new JsonSerializerOptions
    {
      WriteIndented = true
    };

    // Token: 0x040001A8 RID: 424
    private readonly object gate = new object();

    // Token: 0x040001A9 RID: 425
    private readonly object moveGate = new object();

    // Token: 0x040001AA RID: 426
    private readonly string path;

    // Token: 0x040001AB RID: 427
    private readonly IRoutineHost host;

    // Token: 0x040001AC RID: 428
    private List<ClickyRoutine> routines;

    // Token: 0x040001AD RID: 429
    [Nullable(2)]
    private Timer timer;

    // Token: 0x040001AE RID: 430
    private int evaluating;
  }
}
