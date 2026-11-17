using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents.Backend;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Harness;
using HeyClicky.Agents.Jobs;
using HeyClicky.Agents.Routines;
using HeyClicky.Agents.Runtime;
using HeyClicky.Agents.Turns;

namespace HeyClicky.Agents
{
  // Token: 0x02000015 RID: 21
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentService : IAgentService, IAsyncDisposable
  {
    // Token: 0x14000001 RID: 1
    // (add) Token: 0x0600016D RID: 365 RVA: 0x000D9F64 File Offset: 0x000D9F64
    // (remove) Token: 0x0600016E RID: 366 RVA: 0x000D9F9C File Offset: 0x000D9F9C
    [Nullable(new byte[] { 2, 1, 1 })]
    [field: Nullable(new byte[] { 2, 1, 1 })]
    public event Action<string, string> BrowserTurnEnded;

    // Token: 0x14000002 RID: 2
    // (add) Token: 0x0600016F RID: 367 RVA: 0x000D9FD4 File Offset: 0x000D9FD4
    // (remove) Token: 0x06000170 RID: 368 RVA: 0x000DA00C File Offset: 0x000DA00C
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<string> TaskNeedsChromeConnection;

    // Token: 0x14000003 RID: 3
    // (add) Token: 0x06000171 RID: 369 RVA: 0x000DA044 File Offset: 0x000DA044
    // (remove) Token: 0x06000172 RID: 370 RVA: 0x000DA07C File Offset: 0x000DA07C
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<string> TaskNoLongerWaitsOnChrome;

    // Token: 0x1700007B RID: 123
    // (get) Token: 0x06000173 RID: 371 RVA: 0x000DA0B4 File Offset: 0x000DA0B4
    // (set) Token: 0x06000174 RID: 372 RVA: 0x000DA0BC File Offset: 0x000DA0BC
    public Func<bool> ChromeIsConnected { get; set; } = () => false;

    // Token: 0x1700007C RID: 124
    // (get) Token: 0x06000175 RID: 373 RVA: 0x000DA0C8 File Offset: 0x000DA0C8
    // (set) Token: 0x06000176 RID: 374 RVA: 0x000DA0D0 File Offset: 0x000DA0D0
    public Func<string, bool> ThreadSkipsChrome { get; set; } = (string _) => false;

    // Token: 0x06000177 RID: 375 RVA: 0x000DA0DC File Offset: 0x000DA0DC
    [return: Nullable(2)]
    public AgentBrowserTurn BrowserTurn(string threadId, string turnId)
    {
      AgentTask task = this.ActiveTask(threadId);
      if (task == null)
      {
        return null;
      }
      object gate = task.Gate;
      AgentBrowserTurn agentBrowserTurn;
      lock (gate)
      {
        if (task.TurnId != turnId || !task.IsActive)
        {
          agentBrowserTurn = null;
        }
        else
        {
          string title = task.Title;
          string text = (string.IsNullOrWhiteSpace(task.LatestCommentary) ? null : task.LatestCommentary);
          agentBrowserTurn = new AgentBrowserTurn(task.Title, title, text, task.ThinkingHeading, delegate
          {
            object gate2 = task.Gate;
            bool flag3;
            lock (gate2)
            {
              flag3 = task.TurnId == turnId && task.IsActive && this.ActiveTask(threadId) == task;
            }
            return flag3;
          });
        }
      }
      return agentBrowserTurn;
    }

    // Token: 0x06000178 RID: 376 RVA: 0x000DA1D8 File Offset: 0x000DA1D8
    [return: Nullable(2)]
    private AgentTask ActiveTask(string threadId)
    {
      return this.tasks.Values.FirstOrDefault<AgentTask>((AgentTask t) => AgentService.Lock<bool>(t, () => t.ThreadId == threadId && t.IsActive));
    }

    // Token: 0x06000179 RID: 377 RVA: 0x000DA210 File Offset: 0x000DA210
    public void NoteFoundChromeNotConnected(string threadId)
    {
      AgentTask agentTask = this.ActiveTask(threadId);
      if (agentTask != null)
      {
        object gate = agentTask.Gate;
        lock (gate)
        {
          agentTask.FoundChromeNotConnectedThisTurn = true;
        }
      }
    }

    // Token: 0x0600017A RID: 378 RVA: 0x000DA25C File Offset: 0x000DA25C
    public bool FoundChromeNotConnectedThisTurn(string threadId)
    {
      AgentTask task = this.ActiveTask(threadId);
      return task != null && AgentService.Lock<bool>(task, () => task.FoundChromeNotConnectedThisTurn);
    }

    // Token: 0x0600017B RID: 379 RVA: 0x000DA2A0 File Offset: 0x000DA2A0
    private void AskAboutChromeIfStoppedForIt(AgentTask task)
    {
      object obj = task.Gate;
      string threadId;
      bool foundChromeNotConnectedThisTurn;
      string finalResponseMarkdown;
      string summary;
      lock (obj)
      {
        threadId = task.ThreadId;
        foundChromeNotConnectedThisTurn = task.FoundChromeNotConnectedThisTurn;
        finalResponseMarkdown = task.FinalResponseMarkdown;
        summary = task.Summary;
      }
      if (threadId == null)
      {
        return;
      }
      bool flag2 = foundChromeNotConnectedThisTurn && ChromeTurnEnd.HelperStoppedForChrome(finalResponseMarkdown, summary) && !this.ChromeIsConnected();
      if (!foundChromeNotConnectedThisTurn || !ChromeTurnEnd.AsksAtTurnEnd(foundChromeNotConnectedThisTurn, flag2, this.ThreadSkipsChrome(threadId)))
      {
        Action<string> taskNoLongerWaitsOnChrome = this.TaskNoLongerWaitsOnChrome;
        if (taskNoLongerWaitsOnChrome == null)
        {
          return;
        }
        taskNoLongerWaitsOnChrome(threadId);
        return;
      }
      else
      {
        obj = task.Gate;
        lock (obj)
        {
          task.NeedsChromeConnection = true;
        }
        Action<string> taskNeedsChromeConnection = this.TaskNeedsChromeConnection;
        if (taskNeedsChromeConnection == null)
        {
          return;
        }
        taskNeedsChromeConnection(threadId);
        return;
      }
    }

    // Token: 0x0600017C RID: 380 RVA: 0x000DA390 File Offset: 0x000DA390
    public async Task<bool> PassInformationToRunningTurnAsync(string threadId, string text, CancellationToken ct = default(CancellationToken))
    {
      bool flag;
      if (this.ActiveTask(threadId) == null)
      {
        flag = false;
      }
      else
      {
        try
        {
          if (!this.session.IsHarnessTurnRunning(threadId))
          {
            flag = false;
          }
          else
          {
            flag = await this.session.SteerHarnessTurnAsync(threadId, text, ct, true, null).ConfigureAwait(false);
          }
        }
        catch (Exception ex)
        {
          Log.Warn("Agents", "passing information to " + threadId + "'s running turn failed: " + ex.Message);
          flag = false;
        }
      }
      return flag;
    }

    // Token: 0x0600017D RID: 381 RVA: 0x000DA3EC File Offset: 0x000DA3EC
    [return: Nullable(2)]
    public string TaskIdForThread(string threadId)
    {
      AgentTask agentTask = (from t in this.tasks.Values
        where AgentService.Lock<bool>(t, () => t.ThreadId == threadId)
        orderby t.StartedAt descending
        select t).FirstOrDefault<AgentTask>();
      if (agentTask == null)
      {
        return null;
      }
      return agentTask.Id;
    }

    // Token: 0x0600017E RID: 382 RVA: 0x000DA458 File Offset: 0x000DA458
    public bool IsThreadRunning(string threadId)
    {
      return this.ActiveTask(threadId) != null;
    }

    // Token: 0x0600017F RID: 383 RVA: 0x000DA468 File Offset: 0x000DA468
    public IReadOnlyList<string> ThreadsNeedingChrome()
    {
      return (from t in this.tasks.Values
        where AgentService.Lock<bool>(t, () => t.NeedsChromeConnection && t.ThreadId != null)
        orderby t.StartedAt
        select AgentService.Lock<string>(t, () => t.ThreadId)).ToList<string>();
    }

    // Token: 0x06000180 RID: 384 RVA: 0x000DA4F4 File Offset: 0x000DA4F4
    public void ClearNeedsChromeConnection(string threadId)
    {
      foreach (AgentTask agentTask in this.tasks.Values)
      {
        object gate = agentTask.Gate;
        lock (gate)
        {
          if (agentTask.ThreadId == threadId)
          {
            agentTask.NeedsChromeConnection = false;
          }
        }
      }
      this.RaiseTasksChanged();
    }

    // Token: 0x06000181 RID: 385 RVA: 0x000DA584 File Offset: 0x000DA584
    public bool NeedsChromeConnection(string threadId)
    {
      return this.tasks.Values.Any<AgentTask>((AgentTask t) => AgentService.Lock<bool>(t, () => t.ThreadId == threadId && t.NeedsChromeConnection));
    }

    // Token: 0x06000182 RID: 386 RVA: 0x000DA5BC File Offset: 0x000DA5BC
    public bool IsTaskWaitingOnUserAnswer(string taskId)
    {
      AgentTask task;
      return this.tasks.TryGetValue(taskId, out task) && AgentService.Lock<bool>(task, () => task.PendingComputerUseRequest != null || task.RequiresExtraUsageApproval || task.NeedsChromeConnection);
    }

    // Token: 0x06000183 RID: 387 RVA: 0x000DA5FC File Offset: 0x000DA5FC
    private void NotifyBrowserTurnEnded(AgentTask task, TurnOutcome outcome)
    {
      object gate = task.Gate;
      string threadId;
      string turnId;
      lock (gate)
      {
        threadId = task.ThreadId;
        turnId = task.TurnId;
      }
      if (threadId != null && turnId != null)
      {
        Action<string, string> browserTurnEnded = this.BrowserTurnEnded;
        if (browserTurnEnded == null)
        {
          return;
        }
        browserTurnEnded(threadId, turnId);
      }
    }

    // Token: 0x1700007D RID: 125
    // (get) Token: 0x06000184 RID: 388 RVA: 0x000DA65C File Offset: 0x000DA65C
    // (set) Token: 0x06000185 RID: 389 RVA: 0x000DA664 File Offset: 0x000DA664
    public ClickyAppStore ClickyApps { get; private set; }

    // Token: 0x06000186 RID: 390 RVA: 0x000DA670 File Offset: 0x000DA670
    private void CreateClickyApps()
    {
      this.ClickyApps = new ClickyAppStore(Path.Combine(Path.GetDirectoryName(Path.GetFullPath(this.options.StateDirectory.TrimEnd(new char[] { '\\', '/' }))) ?? this.options.StateDirectory, "ClickyApps"));
    }

    // Token: 0x06000187 RID: 391 RVA: 0x000DA6CC File Offset: 0x000DA6CC
    [return: Nullable(2)]
    private AgentTask ClickyAppLiveTask(string slug)
    {
      AgentService.<>c__DisplayClass36_0 CS$<>8__locals1 = new AgentService.<>c__DisplayClass36_0();
      CS$<>8__locals1.slug = slug;
      List<AgentTask> list = (from t in this.tasks.Values
        where !t.IsDemo
        orderby t.StartedAt descending
        select t).ToList<AgentTask>();
      AgentService.<>c__DisplayClass36_0 CS$<>8__locals2 = CS$<>8__locals1;
      ClickyInfo clickyInfo = this.clickys.Get(CS$<>8__locals1.slug);
      CS$<>8__locals2.boundThreadId = ((clickyInfo != null) ? clickyInfo.ThreadId : null);
      if (CS$<>8__locals1.boundThreadId != null)
      {
        AgentTask agentTask = list.FirstOrDefault<AgentTask>((AgentTask t) => t.ThreadId == CS$<>8__locals1.boundThreadId);
        if (agentTask != null)
        {
          return agentTask;
        }
      }
      return list.FirstOrDefault<AgentTask>((AgentTask t) => t.ClickySlug == CS$<>8__locals1.slug);
    }

    // Token: 0x06000188 RID: 392 RVA: 0x000DA794 File Offset: 0x000DA794
    [return: Nullable(2)]
    public string ClickyAppLiveThreadId(string slug)
    {
      AgentTask task = this.ClickyAppLiveTask(slug);
      string text;
      if ((text = ((task != null) ? AgentService.Lock<string>(task, () => task.ThreadId) : null)) == null)
      {
        ClickyInfo clickyInfo = this.clickys.Get(slug);
        if (clickyInfo == null)
        {
          return null;
        }
        text = clickyInfo.ThreadId;
      }
      return text;
    }

    // Token: 0x06000189 RID: 393 RVA: 0x000DA7F4 File Offset: 0x000DA7F4
    public ClickyAppLiveStatus LiveStatusForClicky(string slug)
    {
      AgentTask agentTask = this.ClickyAppLiveTask(slug);
      if (agentTask == null)
      {
        return ClickyAppLiveStatus.Idle;
      }
      object gate = agentTask.Gate;
      ClickyAppLiveStatus clickyAppLiveStatus;
      lock (gate)
      {
        if (agentTask.PendingComputerUseRequest != null)
        {
          clickyAppLiveStatus = new ClickyAppLiveStatus(ClickyAppLiveStatus.Phases.NeedsYou, "I need a yes to use your PC", Array.Empty<string>(), new DateTimeOffset?(agentTask.StartedAt), null);
        }
        else if (agentTask.RequiresExtraUsageApproval || agentTask.Status == AgentTaskStatus.WaitingForUser)
        {
          clickyAppLiveStatus = new ClickyAppLiveStatus(ClickyAppLiveStatus.Phases.NeedsYou, "I need your OK to keep going", Array.Empty<string>(), new DateTimeOffset?(agentTask.StartedAt), null);
        }
        else
        {
          AgentTaskStatus status = agentTask.Status;
          if (status > AgentTaskStatus.Running)
          {
            if (status != AgentTaskStatus.Finished)
            {
              string text = ((agentTask.Status == AgentTaskStatus.Stopped) ? "Stopped before I finished" : "Something went wrong");
              clickyAppLiveStatus = new ClickyAppLiveStatus(ClickyAppLiveStatus.Phases.Failed, text, Array.Empty<string>(), new DateTimeOffset?(agentTask.StartedAt), new DateTimeOffset?(this.clickyAppFinishedAt.GetValueOrDefault(agentTask.Id)));
            }
            else
            {
              string summary = agentTask.Summary;
              string text2 = ((summary != null) ? summary.Trim() : null);
              string text4;
              if (text2 == null || text2.Length <= 0)
              {
                string doneTitle = agentTask.DoneTitle;
                string text3 = ((doneTitle != null) ? doneTitle.Trim() : null);
                text4 = ((text3 != null && text3.Length > 0) ? text3 : "Done");
              }
              else
              {
                text4 = text2;
              }
              string text5 = text4;
              clickyAppLiveStatus = new ClickyAppLiveStatus(ClickyAppLiveStatus.Phases.Finished, text5, Array.Empty<string>(), new DateTimeOffset?(agentTask.StartedAt), new DateTimeOffset?(this.clickyAppFinishedAt.GetValueOrDefault(agentTask.Id)));
            }
          }
          else
          {
            List<string> list = (from a in agentTask.Activity
              where a.Kind == AgentActivityKind.Commentary
              select ClickyWorkReplyParts.FirstSentence(a.Text, 140) into l
              where l.Length > 0
              select l).ToList<string>();
            string thinkingHeading = agentTask.ThinkingHeading;
            string text6 = ((thinkingHeading != null) ? thinkingHeading.Trim() : null);
            string text7 = list.LastOrDefault<string>() ?? (string.IsNullOrEmpty(text6) ? "Working on it" : text6);
            clickyAppLiveStatus = new ClickyAppLiveStatus(ClickyAppLiveStatus.Phases.Working, text7, list.TakeLast<string>(5).ToList<string>(), new DateTimeOffset?(agentTask.StartedAt), null);
          }
        }
      }
      return clickyAppLiveStatus;
    }

    // Token: 0x0600018A RID: 394 RVA: 0x000DAA80 File Offset: 0x000DAA80
    private void NoteClickyAppWorkFinished(AgentTask task)
    {
      this.clickyAppFinishedAt[task.Id] = DateTimeOffset.Now;
      object gate = task.Gate;
      bool flag = false;
      bool isDemo;
      string text2;
      try
      {
        Monitor.Enter(gate, ref flag);
        isDemo = task.IsDemo;
        string text;
        if ((text = task.ClickySlug) == null)
        {
          string thread = task.ThreadId;
          if (thread == null)
          {
            text = null;
          }
          else
          {
            ClickyInfo clickyInfo = this.clickys.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo c) => c.ThreadId == thread);
            text = ((clickyInfo != null) ? clickyInfo.Slug : null);
          }
        }
        text2 = text;
      }
      finally
      {
        if (flag)
        {
          Monitor.Exit(gate);
        }
      }
      if (!ClickyAppsPreferences.IsEnabled || isDemo || text2 == null)
      {
        return;
      }
      ClickyWorkPiece clickyWorkPiece = this.ClickyAppWorkPiece(task, text2);
      if (clickyWorkPiece == null)
      {
        return;
      }
      this.ClickyApps.Merge(new <>z__ReadOnlySingleElementList<ClickyWorkPiece>(clickyWorkPiece), text2, true);
    }

    // Token: 0x0600018B RID: 395 RVA: 0x000DAB60 File Offset: 0x000DAB60
    [return: Nullable(2)]
    private ClickyWorkPiece ClickyAppWorkPiece(AgentTask task, string slug)
    {
      object gate = task.Gate;
      ClickyWorkPiece clickyWorkPiece;
      lock (gate)
      {
        ValueTuple<string, string, string, IReadOnlyList<string>> valueTuple = ClickyWorkReplyParts.Parse(task.FinalResponseMarkdown ?? string.Join("\n\n", task.CompletedReplies));
        string item = valueTuple.Item1;
        IReadOnlyList<string> readOnlyList = ((task.Artifacts.Count == 0) ? valueTuple.Item4 : task.Artifacts);
        if (item.Length == 0 && readOnlyList.Count == 0)
        {
          clickyWorkPiece = null;
        }
        else
        {
          string text = task.ThreadId ?? ("task-" + task.Id);
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
          defaultInterpolatedStringHandler.AppendFormatted(task.Id);
          defaultInterpolatedStringHandler.AppendLiteral(":");
          defaultInterpolatedStringHandler.AppendFormatted<int>(task.UserPrompts.Count);
          string text2 = defaultInterpolatedStringHandler.ToStringAndClear();
          string text3 = task.TurnId ?? text2;
          string text4 = task.UserPrompts.LastOrDefault<string>() ?? "";
          string summary = task.Summary;
          string text5 = ((summary != null) ? summary.Trim() : null);
          string text6 = ((text5 != null && text5.Length > 0) ? text5 : (valueTuple.Item3 ?? ClickyWorkReplyParts.FirstSentence(item, 220)));
          ClickyWorkPiece clickyWorkPiece2 = new ClickyWorkPiece();
          clickyWorkPiece2.Id = text + "#" + text3;
          clickyWorkPiece2.Slug = slug;
          clickyWorkPiece2.ThreadId = task.ThreadId;
          clickyWorkPiece2.ClientTurnKey = text2;
          clickyWorkPiece2.Ask = this.DisplayAsk(text4);
          string doneTitle = task.DoneTitle;
          string text7 = ((doneTitle != null) ? doneTitle.Trim() : null);
          clickyWorkPiece2.Title = ((text7 != null && text7.Length > 0) ? text7 : (valueTuple.Item2 ?? ""));
          clickyWorkPiece2.Summary = text6;
          clickyWorkPiece2.Reply = ((item.Length > 20000) ? item.Substring(0, 20000) : item);
          clickyWorkPiece2.ArtifactPaths = readOnlyList.ToList<string>();
          clickyWorkPiece2.FinishedAt = this.clickyAppFinishedAt.GetValueOrDefault(task.Id, DateTimeOffset.Now);
          clickyWorkPiece2.IsRoutineRun = task.IsRoutineRun || AgentService.RoutineHeader.IsMatch(text4.TrimStart());
          clickyWorkPiece = clickyWorkPiece2;
        }
      }
      return clickyWorkPiece;
    }

    // Token: 0x0600018C RID: 396 RVA: 0x000DADB4 File Offset: 0x000DADB4
    public static IReadOnlyList<ClickyWorkPiece> ClickyAppWorkPieces(IReadOnlyList<AgentTranscriptTurn> turns, string threadId, string slug, [Nullable(new byte[] { 2, 1, 1 })] Func<string, string> displayAsk = null)
    {
      List<ClickyWorkPiece> list = new List<ClickyWorkPiece>();
      for (int i = 0; i < turns.Count; i++)
      {
        AgentTranscriptTurn agentTranscriptTurn = turns[i];
        if (string.Equals(agentTranscriptTurn.Status, "completed", StringComparison.OrdinalIgnoreCase) && agentTranscriptTurn.ErrorMessage == null)
        {
          ValueTuple<string, string, string, IReadOnlyList<string>> valueTuple = ClickyWorkReplyParts.Parse(string.Join("\n\n", agentTranscriptTurn.AssistantMessages));
          IReadOnlyList<string> readOnlyList = ((agentTranscriptTurn.Artifacts.Count == 0) ? valueTuple.Item4 : agentTranscriptTurn.Artifacts);
          if (valueTuple.Item1.Length != 0 || readOnlyList.Count != 0)
          {
            string text = agentTranscriptTurn.UserMessages.FirstOrDefault<string>() ?? "";
            string text2;
            if ((text2 = agentTranscriptTurn.TurnId) == null)
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
              defaultInterpolatedStringHandler.AppendFormatted<int>(i);
              defaultInterpolatedStringHandler.AppendLiteral("-");
              defaultInterpolatedStringHandler.AppendFormatted<long>((agentTranscriptTurn.StartedAt ?? DateTimeOffset.UnixEpoch).ToUnixTimeSeconds());
              text2 = defaultInterpolatedStringHandler.ToStringAndClear();
            }
            string text3 = text2;
            List<ClickyWorkPiece> list2 = list;
            ClickyWorkPiece clickyWorkPiece = new ClickyWorkPiece();
            clickyWorkPiece.Id = threadId + "#" + text3;
            clickyWorkPiece.Slug = slug;
            clickyWorkPiece.ThreadId = threadId;
            Func<string, string> func = displayAsk;
            if (displayAsk == null && (func = AgentService.<>c.<>9__42_0) == null)
            {
              func = (AgentService.<>c.<>9__42_0 = (string a) => AgentService.RoutinePreviewLine(a) ?? a.Trim());
            }
            clickyWorkPiece.Ask = func(text);
            clickyWorkPiece.Title = valueTuple.Item2 ?? "";
            clickyWorkPiece.Summary = valueTuple.Item3 ?? ClickyWorkReplyParts.FirstSentence(valueTuple.Item1, 220);
            clickyWorkPiece.Reply = ((valueTuple.Item1.Length > 20000) ? valueTuple.Item1.Substring(0, 20000) : valueTuple.Item1);
            clickyWorkPiece.ArtifactPaths = readOnlyList.ToList<string>();
            clickyWorkPiece.FinishedAt = agentTranscriptTurn.FinishedAt ?? agentTranscriptTurn.StartedAt ?? DateTimeOffset.MinValue;
            clickyWorkPiece.IsRoutineRun = AgentService.RoutineHeader.IsMatch(text.TrimStart());
            list2.Add(clickyWorkPiece);
          }
        }
      }
      return list;
    }

    // Token: 0x0600018D RID: 397 RVA: 0x000DAFF0 File Offset: 0x000DAFF0
    public async Task GatherClickyAppWorkAsync(string slug, CancellationToken ct = default(CancellationToken))
    {
      this.ClickyApps.LoadIfNeeded(slug);
      List<ClickyWorkPiece> pieces = new List<ClickyWorkPiece>();
      HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
      ClickyInfo clickyInfo = this.clickys.Get(slug);
      string text = ((clickyInfo != null) ? clickyInfo.ThreadId : null);
      if (text != null)
      {
        hashSet.Add(text);
      }
      foreach (AgentTask agentTask in this.tasks.Values)
      {
        if (agentTask.ClickySlug == slug)
        {
          string threadId2 = agentTask.ThreadId;
          if (threadId2 != null)
          {
            hashSet.Add(threadId2);
          }
        }
      }
      foreach (string threadId in hashSet)
      {
        try
        {
          IReadOnlyList<AgentTranscriptTurn> readOnlyList = await this.ReadThreadAsync(threadId, ct).ConfigureAwait(false);
          pieces.AddRange(AgentService.ClickyAppWorkPieces(readOnlyList, threadId, slug, new Func<string, string>(this.DisplayAsk)));
        }
        catch (Exception ex) when (!(ex is OperationCanceledException))
        {
          Log.Warn("Agents", "clicky app work for " + slug + ": " + ex.Message);
        }
        threadId = null;
      }
      HashSet<string>.Enumerator enumerator2 = default(HashSet<string>.Enumerator);
      AgentTask live = this.ClickyAppLiveTask(slug);
      if (live != null && AgentService.Lock<bool>(live, () => live.Status == AgentTaskStatus.Finished))
      {
        ClickyWorkPiece clickyWorkPiece = this.ClickyAppWorkPiece(live, slug);
        if (clickyWorkPiece != null)
        {
          pieces.Add(clickyWorkPiece);
        }
      }
      this.ClickyApps.Merge(pieces, slug, true);
    }

    // Token: 0x0600018E RID: 398 RVA: 0x000DB044 File Offset: 0x000DB044
    [return: Nullable(2)]
    private static string RoutinePreviewLine(string rawAsk)
    {
      Match match = AgentService.RoutineHeader.Match(rawAsk.TrimStart());
      if (match == null || !match.Success)
      {
        return null;
      }
      return match.Groups[1].Value + " · run " + match.Groups[2].Value;
    }

    // Token: 0x0600018F RID: 399 RVA: 0x000DB09C File Offset: 0x000DB09C
    private string DisplayAsk(string rawAsk)
    {
      string text = rawAsk.Trim();
      if (text.StartsWith("<clicky_agent_turn_lease_metadata>", StringComparison.Ordinal))
      {
        int num = text.IndexOf("</clicky_agent_turn_lease_metadata>", StringComparison.Ordinal);
        if (num >= 0)
        {
          string text2 = text;
          int num2 = num + "</clicky_agent_turn_lease_metadata>".Length;
          text = text2.Substring(num2, text2.Length - num2).Trim();
        }
      }
      string text3;
      if ((text3 = AgentService.RoutinePreviewLine(text)) == null)
      {
        text3 = this.clickys.DisplayFor(text) ?? text;
      }
      return text3;
    }

    // Token: 0x06000190 RID: 400 RVA: 0x000DB110 File Offset: 0x000DB110
    [return: Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> TakeLegacyClickyMigration()
    {
      if (AgentDefaults.Bool("clicky.clickyApps.legacyClickysAsJobs.v1"))
      {
        return null;
      }
      AgentDefaults.Set("clicky.clickyApps.legacyClickysAsJobs.v1", true);
      List<string> list = this.clickys.Clickys.Select<ClickyInfo, string>((ClickyInfo c) => c.Slug).ToList<string>();
      string text = "Agents";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(52, 1);
      defaultInterpolatedStringHandler.AppendLiteral("[migration] old Clickys marked read (");
      defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
      defaultInterpolatedStringHandler.AppendLiteral("), pins cleared");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      return list;
    }

    // Token: 0x06000191 RID: 401 RVA: 0x000DB1AC File Offset: 0x000DB1AC
    [return: Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> TakeLegacyClickysToDock()
    {
      if (AgentDefaults.Bool("clicky.clickyApps.legacyClickysDocked.v1"))
      {
        return null;
      }
      IReadOnlyList<ClickyInfo> readOnlyList = this.clickys.Clickys;
      if (readOnlyList.Count == 0)
      {
        return null;
      }
      AgentDefaults.Set("clicky.clickyApps.legacyClickysDocked.v1", true);
      try
      {
        Dictionary<string, DateTimeOffset> rolloutUpdated = (from t in CodexRolloutHistory.List(this.options.CodexHome)
          group t by t.ThreadId).ToDictionary<IGrouping<string, CodexRolloutHistory.RolloutThread>, string, DateTimeOffset>((IGrouping<string, CodexRolloutHistory.RolloutThread> g) => g.Key, (IGrouping<string, CodexRolloutHistory.RolloutThread> g) => g.Max<CodexRolloutHistory.RolloutThread, DateTimeOffset>((CodexRolloutHistory.RolloutThread t) => t.UpdatedAt));
      }
      catch (Exception ex)
      {
        Log.Warn("Agents", "[migration] rollout list: " + ex.Message);
        Dictionary<string, DateTimeOffset> rolloutUpdated = new Dictionary<string, DateTimeOffset>();
      }
      List<string> list = (from c in readOnlyList
        where c.ThreadId != null
        select new ValueTuple<string, DateTimeOffset>(c.Slug, base.<TakeLegacyClickysToDock>g__LastUsed|3(c.ThreadId)) into c
        orderby c.Item2 descending
        select c.Item1).Distinct<string>().Take<string>(30).ToList<string>();
      string text = "Agents";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 1);
      defaultInterpolatedStringHandler.AppendLiteral("[migration] docked ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" recent old Clickys");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      return list;
    }

    // Token: 0x06000192 RID: 402 RVA: 0x000DB378 File Offset: 0x000DB378
    public void BackfillLegacyClickyWorkIfNeeded()
    {
      AgentService.<>c__DisplayClass53_0 CS$<>8__locals1 = new AgentService.<>c__DisplayClass53_0();
      CS$<>8__locals1.<>4__this = this;
      if (AgentDefaults.Bool("clicky.clickyApps.legacyClickyWorkBackfilled.v1"))
      {
        return;
      }
      CS$<>8__locals1.slugs = (from c in this.clickys.Clickys
        where c.ThreadId != null
        select c.Slug).ToList<string>();
      if (CS$<>8__locals1.slugs.Count == 0)
      {
        return;
      }
      Task.Run(delegate
      {
        AgentService.<>c__DisplayClass53_0.<<BackfillLegacyClickyWorkIfNeeded>b__2>d <<BackfillLegacyClickyWorkIfNeeded>b__2>d;
        <<BackfillLegacyClickyWorkIfNeeded>b__2>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<BackfillLegacyClickyWorkIfNeeded>b__2>d.<>4__this = CS$<>8__locals1;
        <<BackfillLegacyClickyWorkIfNeeded>b__2>d.<>1__state = -1;
        <<BackfillLegacyClickyWorkIfNeeded>b__2>d.<>t__builder.Start<AgentService.<>c__DisplayClass53_0.<<BackfillLegacyClickyWorkIfNeeded>b__2>d>(ref <<BackfillLegacyClickyWorkIfNeeded>b__2>d);
        return <<BackfillLegacyClickyWorkIfNeeded>b__2>d.<>t__builder.Task;
      });
    }

    // Token: 0x1700007E RID: 126
    // (get) Token: 0x06000193 RID: 403 RVA: 0x000DB420 File Offset: 0x000DB420
    // (set) Token: 0x06000194 RID: 404 RVA: 0x000DB428 File Offset: 0x000DB428
    [Nullable(new byte[] { 2, 1, 2 })]
    public Func<string, string> TakeUnansweredComputerUseStep
    {
      [return: Nullable(new byte[] { 2, 1, 2 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 2 })]
      set;
    }

    // Token: 0x1700007F RID: 127
    // (get) Token: 0x06000195 RID: 405 RVA: 0x000DB434 File Offset: 0x000DB434
    // (set) Token: 0x06000196 RID: 406 RVA: 0x000DB43C File Offset: 0x000DB43C
    [TupleElementNames(new string[] { "User", "Assistant" })]
    [Nullable(new byte[] { 2, 1, 1, 0, 1, 1 })]
    public Func<string, IReadOnlyList<ValueTuple<string, string>>> CompanionTurnsSinceLastMessage
    {
      [return: TupleElementNames(new string[] { "User", "Assistant" })]
      [return: Nullable(new byte[] { 2, 1, 1, 0, 1, 1 })]
      get;
      [param: TupleElementNames(new string[] { "User", "Assistant" })]
      [param: Nullable(new byte[] { 2, 1, 1, 0, 1, 1 })]
      set;
    }

    // Token: 0x17000080 RID: 128
    // (get) Token: 0x06000197 RID: 407 RVA: 0x000DB448 File Offset: 0x000DB448
    // (set) Token: 0x06000198 RID: 408 RVA: 0x000DB450 File Offset: 0x000DB450
    [TupleElementNames(new string[] { "User", "Assistant" })]
    [Nullable(new byte[] { 2, 1, 1, 0, 1, 1 })]
    public Func<string, IReadOnlyList<ValueTuple<string, string>>> CompanionTurnsForFreshLaunch
    {
      [return: TupleElementNames(new string[] { "User", "Assistant" })]
      [return: Nullable(new byte[] { 2, 1, 1, 0, 1, 1 })]
      get;
      [param: TupleElementNames(new string[] { "User", "Assistant" })]
      [param: Nullable(new byte[] { 2, 1, 1, 0, 1, 1 })]
      set;
    }

    // Token: 0x17000081 RID: 129
    // (get) Token: 0x06000199 RID: 409 RVA: 0x000DB45C File Offset: 0x000DB45C
    // (set) Token: 0x0600019A RID: 410 RVA: 0x000DB464 File Offset: 0x000DB464
    [Nullable(new byte[] { 2, 1, 1 })]
    public Func<string, string, bool> WasComputerUseStepRecentlyDeclined
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x17000082 RID: 130
    // (get) Token: 0x0600019B RID: 411 RVA: 0x000DB470 File Offset: 0x000DB470
    // (set) Token: 0x0600019C RID: 412 RVA: 0x000DB478 File Offset: 0x000DB478
    [Nullable(new byte[] { 2, 1 })]
    public Action<string> ForgetDeclinedComputerUseSteps
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x0600019D RID: 413 RVA: 0x000DB484 File Offset: 0x000DB484
    private void ForgetDeclinedStepsIfUserMessage(string taskId, string launchSource)
    {
      bool flag = launchSource == "text" || launchSource == "voice";
      if (flag)
      {
        Action<string> forgetDeclinedComputerUseSteps = this.ForgetDeclinedComputerUseSteps;
        if (forgetDeclinedComputerUseSteps == null)
        {
          return;
        }
        forgetDeclinedComputerUseSteps(taskId);
      }
    }

    // Token: 0x0600019E RID: 414 RVA: 0x000DB4C8 File Offset: 0x000DB4C8
    [return: Nullable(2)]
    public string PendingComputerUseRequest(string taskId)
    {
      AgentTask task;
      if (!this.tasks.TryGetValue(taskId, out task))
      {
        return null;
      }
      return AgentService.Lock<string>(task, () => task.PendingComputerUseRequest);
    }

    // Token: 0x0600019F RID: 415 RVA: 0x000DB508 File Offset: 0x000DB508
    public void SetPendingComputerUseRequest(string taskId, [Nullable(2)] string request)
    {
      AgentTask agentTask;
      if (!this.tasks.TryGetValue(taskId, out agentTask))
      {
        return;
      }
      object gate = agentTask.Gate;
      bool flag2;
      lock (gate)
      {
        flag2 = agentTask.PendingComputerUseRequest != request;
        agentTask.PendingComputerUseRequest = request;
      }
      if (request == null)
      {
        string text;
        this.computerUseSummaries.TryRemove(taskId, out text);
      }
      if (!flag2)
      {
        return;
      }
      this.SyncPendingRequest(agentTask);
      this.RaiseTasksChanged();
    }

    // Token: 0x060001A0 RID: 416 RVA: 0x000DB58C File Offset: 0x000DB58C
    [return: Nullable(2)]
    public string PendingComputerUseSummary(string taskId)
    {
      return this.computerUseSummaries.GetValueOrDefault(taskId);
    }

    // Token: 0x060001A1 RID: 417 RVA: 0x000DB59C File Offset: 0x000DB59C
    public void SetPendingComputerUseSummary(string taskId, [Nullable(2)] string summary)
    {
      if (summary == null)
      {
        string text;
        this.computerUseSummaries.TryRemove(taskId, out text);
        return;
      }
      this.computerUseSummaries[taskId] = summary;
    }

    // Token: 0x060001A2 RID: 418 RVA: 0x000DB5CC File Offset: 0x000DB5CC
    public bool DidAnnounceComputerUseRequest(string taskId)
    {
      return this.computerUseAnnounced.GetValueOrDefault(taskId);
    }

    // Token: 0x060001A3 RID: 419 RVA: 0x000DB5DC File Offset: 0x000DB5DC
    public void SetDidAnnounceComputerUseRequest(string taskId, bool value)
    {
      this.computerUseAnnounced[taskId] = value;
    }

    // Token: 0x060001A4 RID: 420 RVA: 0x000DB5EC File Offset: 0x000DB5EC
    public bool IsThreadActive(string threadId)
    {
      return this.session.IsThreadActive(threadId);
    }

    // Token: 0x060001A5 RID: 421 RVA: 0x000DB5FC File Offset: 0x000DB5FC
    [return: Nullable(2)]
    public string TurnLeaseId(string taskId)
    {
      AgentTask task;
      if (!this.tasks.TryGetValue(taskId, out task))
      {
        return null;
      }
      return AgentService.Lock<string>(task, delegate
      {
        AgentTurnLease lease = task.Lease;
        if (lease == null)
        {
          return null;
        }
        return lease.LeaseId;
      });
    }

    // Token: 0x060001A6 RID: 422 RVA: 0x000DB63C File Offset: 0x000DB63C
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<string> ContinueLeaseAfterComputerUseConsentAsync(string leaseId, CancellationToken ct = default(CancellationToken))
    {
      double[] array = new double[] { 0.0, 0.8, 1.6, 3.0 };
      int i = 0;
      while (i < array.Length)
      {
        double num = array[i];
        if (num > 0.0)
        {
          await Task.Delay(TimeSpan.FromSeconds(num), ct).ConfigureAwait(false);
        }
        ValueTuple<bool, AgentTurnLease, string>? valueTuple;
        try
        {
          string text = await this.supabaseAccessToken(ct).ConfigureAwait(false);
          if (string.IsNullOrEmpty(text))
          {
            Log.Info("Agents", "[lease/computer-use-consent] no access token");
            goto IL_0301;
          }
          valueTuple = await this.api.ContinueAfterComputerUseConsentAsync(text, leaseId, ct).ConfigureAwait(false);
        }
        catch (Exception ex) when (!(ex is OperationCanceledException))
        {
          Log.Info("Agents", "[lease/computer-use-consent] lease=" + leaseId.Substring(0, Math.Min(8, leaseId.Length)) + " uncertain: " + ex.Message);
          goto IL_0301;
        }
        goto IL_024D;
        IL_0301:
        i++;
        continue;
        IL_024D:
        if (valueTuple != null)
        {
          ValueTuple<bool, AgentTurnLease, string> valueOrDefault = valueTuple.GetValueOrDefault();
          if (valueOrDefault.Item1)
          {
            AgentTurnLease item = valueOrDefault.Item2;
            if (item != null)
            {
              this.reopenedLeases[item.LeaseId] = item;
              return item.LeaseId;
            }
          }
        }
        if (valueTuple != null)
        {
          ValueTuple<bool, AgentTurnLease, string> valueOrDefault2 = valueTuple.GetValueOrDefault();
          if (!valueOrDefault2.Item1 && valueOrDefault2.Item3 != "no_computer_use_request")
          {
            return null;
          }
        }
        Log.Info("Agents", "[lease/computer-use-consent] lease=" + leaseId.Substring(0, Math.Min(8, leaseId.Length)) + " not reopened yet");
        goto IL_0301;
      }
      array = null;
      Log.Info("Agents", "[lease/computer-use-consent] lease=" + leaseId.Substring(0, Math.Min(8, leaseId.Length)) + " gave up; sending the answer as a new turn");
      return null;
    }

    // Token: 0x060001A7 RID: 423 RVA: 0x000DB690 File Offset: 0x000DB690
    public void CompleteLease(string leaseId, [Nullable(2)] string threadId, string status = "completed")
    {
      AgentService.<>c__DisplayClass87_0 CS$<>8__locals1 = new AgentService.<>c__DisplayClass87_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.leaseId = leaseId;
      CS$<>8__locals1.status = status;
      CS$<>8__locals1.threadId = threadId;
      Task.Run(delegate
      {
        AgentService.<>c__DisplayClass87_0.<<CompleteLease>b__0>d <<CompleteLease>b__0>d;
        <<CompleteLease>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<CompleteLease>b__0>d.<>4__this = CS$<>8__locals1;
        <<CompleteLease>b__0>d.<>1__state = -1;
        <<CompleteLease>b__0>d.<>t__builder.Start<AgentService.<>c__DisplayClass87_0.<<CompleteLease>b__0>d>(ref <<CompleteLease>b__0>d);
        return <<CompleteLease>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x060001A8 RID: 424 RVA: 0x000DB6C4 File Offset: 0x000DB6C4
    public async Task SubmitThreadFollowUpAsync(string threadId, string text, [Nullable(2)] string preauthorizedLeaseId, CancellationToken ct = default(CancellationToken), [Nullable(2)] string launchContext = null)
    {
      AgentTask task = (from t in this.tasks.Values
        where AgentService.Lock<string>(t, () => t.ThreadId) == threadId
        orderby t.StartedAt descending
        select t).FirstOrDefault<AgentTask>();
      if (task == null)
      {
        Log.Warn("Agents", "follow-up for " + threadId + ": no task on that thread");
      }
      else
      {
        AgentTurnLease agentTurnLease2;
        AgentTurnLease agentTurnLease = ((preauthorizedLeaseId != null && this.reopenedLeases.TryRemove(preauthorizedLeaseId, out agentTurnLease2)) ? agentTurnLease2 : null);
        if (AgentService.Lock<bool>(task, () => task.IsActive) || this.session.IsThreadActive(threadId))
        {
          await this.SteerAsync(task.Id, text, ct).ConfigureAwait(false);
        }
        else
        {
          string text3;
          if (launchContext != null)
          {
            string text2 = AgentService.Lock<string>(task, () => task.ClickySlug);
            if (text2 != null)
            {
              ClickyInfo clickyInfo = this.clickys.Get(text2);
              if (clickyInfo != null)
              {
                text3 = TurnPromptBuilder.ClickyLaunchContext(clickyInfo, false, launchContext.Trim() + " ", false);
                goto IL_01DB;
              }
            }
            text3 = launchContext;
          }
          else
          {
            text3 = null;
          }
          IL_01DB:
          string text4 = text3;
          await this.StartFollowUpTurnAsync(task, text, null, "text", "companion", agentTurnLease, ct, text4, null, false, null).ConfigureAwait(false);
        }
      }
    }

    // Token: 0x060001A9 RID: 425 RVA: 0x000DB734 File Offset: 0x000DB734
    public bool CanRetryAfterFailure(string taskId)
    {
      AgentTask task;
      return this.tasks.TryGetValue(taskId, out task) && AgentService.Lock<bool>(task, delegate
      {
        if (task.Status != AgentTaskStatus.Failed)
        {
          return false;
        }
        if (task.ThreadId == null)
        {
          string text = task.UserPrompts.LastOrDefault<string>();
          return ((text != null) ? text.Trim().Length : 0) > 0;
        }
        return true;
      });
    }

    // Token: 0x060001AA RID: 426 RVA: 0x000DB774 File Offset: 0x000DB774
    public bool CanRetryAfterInterruption(string taskId)
    {
      AgentTask task;
      return this.tasks.TryGetValue(taskId, out task) && AgentService.Lock<bool>(task, delegate
      {
        if (task.Status != AgentTaskStatus.Stopped || task.RequiresExtraUsageApproval)
        {
          return false;
        }
        if (task.ThreadId == null)
        {
          string text = task.UserPrompts.LastOrDefault<string>();
          return ((text != null) ? text.Trim().Length : 0) > 0;
        }
        return true;
      });
    }

    // Token: 0x060001AB RID: 427 RVA: 0x000DB7B4 File Offset: 0x000DB7B4
    public bool RetryFailedTask(string taskId)
    {
      AgentService.<>c__DisplayClass91_0 CS$<>8__locals1 = new AgentService.<>c__DisplayClass91_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.taskId = taskId;
      if (!this.CanRetryAfterFailure(CS$<>8__locals1.taskId) && !this.CanRetryAfterInterruption(CS$<>8__locals1.taskId))
      {
        return false;
      }
      CS$<>8__locals1.task = this.GetTask(CS$<>8__locals1.taskId);
      string text = AgentService.Lock<string>(CS$<>8__locals1.task, delegate
      {
        string text2 = CS$<>8__locals1.task.UserPrompts.LastOrDefault<string>();
        return ((text2 != null) ? text2.Trim() : null) ?? "";
      });
      CS$<>8__locals1.retry = ((text.Length == 0) ? "Retry the task you were working on and finish it." : text);
      Task.Run(delegate
      {
        AgentService.<>c__DisplayClass91_0.<<RetryFailedTask>b__1>d <<RetryFailedTask>b__1>d;
        <<RetryFailedTask>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<RetryFailedTask>b__1>d.<>4__this = CS$<>8__locals1;
        <<RetryFailedTask>b__1>d.<>1__state = -1;
        <<RetryFailedTask>b__1>d.<>t__builder.Start<AgentService.<>c__DisplayClass91_0.<<RetryFailedTask>b__1>d>(ref <<RetryFailedTask>b__1>d);
        return <<RetryFailedTask>b__1>d.<>t__builder.Task;
      });
      return true;
    }

    // Token: 0x14000004 RID: 4
    // (add) Token: 0x060001AC RID: 428 RVA: 0x000DB848 File Offset: 0x000DB848
    // (remove) Token: 0x060001AD RID: 429 RVA: 0x000DB880 File Offset: 0x000DB880
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler TasksChanged;

    // Token: 0x14000005 RID: 5
    // (add) Token: 0x060001AE RID: 430 RVA: 0x000DB8B8 File Offset: 0x000DB8B8
    // (remove) Token: 0x060001AF RID: 431 RVA: 0x000DB8F0 File Offset: 0x000DB8F0
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<AgentTaskEvent> TaskEvent;

    // Token: 0x14000006 RID: 6
    // (add) Token: 0x060001B0 RID: 432 RVA: 0x000DB928 File Offset: 0x000DB928
    // (remove) Token: 0x060001B1 RID: 433 RVA: 0x000DB960 File Offset: 0x000DB960
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler RoutinesChanged;

    // Token: 0x14000007 RID: 7
    // (add) Token: 0x060001B2 RID: 434 RVA: 0x000DB998 File Offset: 0x000DB998
    // (remove) Token: 0x060001B3 RID: 435 RVA: 0x000DB9D0 File Offset: 0x000DB9D0
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<AgentPaywallEventArgs> PaywallReached;

    // Token: 0x060001B4 RID: 436 RVA: 0x000DBA08 File Offset: 0x000DBA08
    public AgentService(ClickyEndpoints endpoints, [Nullable(new byte[] { 1, 1, 2 })] Func<CancellationToken, Task<string>> supabaseAccessToken, HttpClient http, ClientIdentity identity, [Nullable(2)] AgentRuntimeOptions options = null)
    {
      this.endpoints = endpoints;
      this.supabaseAccessToken = supabaseAccessToken;
      this.options = options ?? new AgentRuntimeOptions();
      AgentDefaults.Configure(this.options.StateDirectory);
      this.Jobs = new ClickyJobStore();
      this.Jobs.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.NameJobsWithoutNicknames();
      };
      this.LearnedHowTos = new ClickyLearnedHowTos();
      this.RestartInterruptions = new ClickyRestartInterruptions();
      this.TasksChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.NoteRunningTurnsForRestart();
      };
      this.CreateClickyApps();
      this.api = new AgentWorkerApi(AgentService.EnsureTrailingSlash(endpoints.WorkerBaseUrl), http);
      this.tokens = new AgentSessionTokenProvider(this.api, supabaseAccessToken, this.options.SessionTokenRefreshMargin);
      this.home = new CodexHomeManager(this.options);
      this.clickys = new ClickyStore(this.options.StateDirectory, this.options.ProjectsRoot);
      this.userMemory = new UserMemoryCache(this.options.StateDirectory);
      this.slots = new SemaphoreSlim(Math.Max(1, this.options.MaxConcurrentTasks));
      this.routines = new RoutineScheduler(this.options.StateDirectory, new AgentService.RoutineHost(this));
      this.routines.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        EventHandler routinesChanged = this.RoutinesChanged;
        if (routinesChanged == null)
        {
          return;
        }
        routinesChanged(this, EventArgs.Empty);
      };
      this.RoutinesTool = new RoutinesTool(this.routines, this.Jobs, delegate(string threadId)
      {
        ClickyInfo clickyInfo = this.clickys.ForThread(threadId);
        if (clickyInfo == null)
        {
          return null;
        }
        return clickyInfo.Slug;
      }, delegate(string slug)
      {
        ClickyInfo clickyInfo2 = this.clickys.Get(slug);
        if (clickyInfo2 == null)
        {
          return null;
        }
        return clickyInfo2.Name;
      }, () => this.ProjectsRoot);
      this.session.OnTaskEvent = delegate(AgentTask task, AgentTaskEventKind kind, [Nullable(2)] string text)
      {
        this.Raise(task, kind, text);
      };
      this.session.OnTurnEnded = new Action<AgentTask, TurnOutcome, string>(this.OnTurnEnded);
      this.WireLiveSteps();
      this.WireIntegrationEvents();
      this.ConfigureAgentHarness();
      this.Jobs.JobIdsWithRoutines = new Func<ISet<string>>(this.routines.JobIdsWithRoutines);
    }

    // Token: 0x060001B5 RID: 437 RVA: 0x000DBD00 File Offset: 0x000DBD00
    public AgentService(ClickyEndpoints endpoints, [Nullable(new byte[] { 1, 1, 2 })] Func<CancellationToken, Task<string>> supabaseAccessToken, Func<HttpClient> httpFactory, ClientIdentity identity, [Nullable(2)] AgentRuntimeOptions options = null)
      : this(endpoints, supabaseAccessToken, httpFactory(), identity, options)
    {
    }

    // Token: 0x060001B6 RID: 438 RVA: 0x000DBD14 File Offset: 0x000DBD14
    private static Uri EnsureTrailingSlash(Uri u)
    {
      if (!u.AbsoluteUri.EndsWith('/'))
      {
        return new Uri(u.AbsoluteUri + "/");
      }
      return u;
    }

    // Token: 0x060001B7 RID: 439 RVA: 0x000DBD3C File Offset: 0x000DBD3C
    [return: TupleElementNames(new string[] { "User", "Assistant" })]
    [return: Nullable(new byte[] { 2, 0, 1, 1 })]
    private IReadOnlyList<ValueTuple<string, string>> FollowUpCompanionTurns(AgentLaunchRequest request, [Nullable(2)] string conversationKey)
    {
      string launchSource = request.LaunchSource;
      bool flag = launchSource == "text" || launchSource == "voice";
      if (!flag)
      {
        return null;
      }
      IReadOnlyList<ValueTuple<string, string>> priorCompanionTurns = request.PriorCompanionTurns;
      if (priorCompanionTurns == null || priorCompanionTurns.Count <= 0)
      {
        if (conversationKey != null)
        {
          Func<string, IReadOnlyList<ValueTuple<string, string>>> companionTurnsSinceLastMessage = this.CompanionTurnsSinceLastMessage;
          IReadOnlyList<ValueTuple<string, string>> readOnlyList = ((companionTurnsSinceLastMessage != null) ? companionTurnsSinceLastMessage(conversationKey) : null);
          if (readOnlyList != null && readOnlyList.Count > 0)
          {
            return readOnlyList;
          }
        }
        return null;
      }
      return priorCompanionTurns;
    }

    // Token: 0x060001B8 RID: 440 RVA: 0x000DBDB0 File Offset: 0x000DBDB0
    [return: TupleElementNames(new string[] { "User", "Assistant" })]
    [return: Nullable(new byte[] { 2, 0, 1, 1 })]
    private IReadOnlyList<ValueTuple<string, string>> FreshLaunchCompanionTurns(AgentLaunchRequest request)
    {
      IReadOnlyList<ValueTuple<string, string>> priorCompanionTurns = request.PriorCompanionTurns;
      IReadOnlyList<ValueTuple<string, string>> readOnlyList;
      if (priorCompanionTurns != null && priorCompanionTurns.Count > 0)
      {
        readOnlyList = priorCompanionTurns;
      }
      else
      {
        string launchSource = request.LaunchSource;
        bool flag = launchSource == "text" || launchSource == "voice";
        IReadOnlyList<ValueTuple<string, string>> readOnlyList3;
        if (flag)
        {
          Func<string, IReadOnlyList<ValueTuple<string, string>>> companionTurnsForFreshLaunch = this.CompanionTurnsForFreshLaunch;
          IReadOnlyList<ValueTuple<string, string>> readOnlyList2 = ((companionTurnsForFreshLaunch != null) ? companionTurnsForFreshLaunch(request.Prompt) : null);
          if (readOnlyList2 != null && readOnlyList2.Count > 0)
          {
            readOnlyList3 = readOnlyList2;
            goto IL_006D;
          }
        }
        readOnlyList3 = null;
        IL_006D:
        readOnlyList = readOnlyList3;
      }
      return readOnlyList;
    }

    // Token: 0x060001B9 RID: 441 RVA: 0x000DBE2C File Offset: 0x000DBE2C
    [return: Nullable(2)]
    private string ConversationKey(AgentTask task)
    {
      return AgentService.Lock<string>(task, () => task.JobId ?? task.ThreadId);
    }

    // Token: 0x060001BA RID: 442 RVA: 0x000DBE60 File Offset: 0x000DBE60
    [NullableContext(2)]
    private string LaunchContextWithBuildsOn([Nullable(1)] AgentLaunchRequest request, string receivingJobId)
    {
      string launchContext = request.LaunchContext;
      string text = ((launchContext != null) ? launchContext.Trim() : null);
      string text2 = ((text != null && text.Length > 0) ? text : null);
      IReadOnlyList<string> buildsOnJobIds = request.BuildsOnJobIds;
      string text3;
      if (buildsOnJobIds == null || buildsOnJobIds.Count <= 0)
      {
        text3 = null;
      }
      else
      {
        text3 = this.Jobs.BuildsOnContext((from id in buildsOnJobIds
          select id.Trim() into id
          where id.Length > 0
          select id).Distinct<string>().Take<string>(3), receivingJobId, new Func<string, ValueTuple<string, string, string>?>(this.EarlierWorkElsewhere));
      }
      string text4 = text3;
      if (text2 == null)
      {
        return text4;
      }
      if (text4 != null)
      {
        return text2 + " " + text4;
      }
      return text2;
    }

    // Token: 0x17000083 RID: 131
    // (get) Token: 0x060001BB RID: 443 RVA: 0x000DBF2C File Offset: 0x000DBF2C
    public IReadOnlyList<AgentTaskSnapshot> Tasks
    {
      get
      {
        return (from s in this.tasks.Values.Select<AgentTask, AgentTaskSnapshot>(delegate(AgentTask t)
          {
            object gate = t.Gate;
            AgentTaskSnapshot agentTaskSnapshot;
            lock (gate)
            {
              agentTaskSnapshot = t.Snapshot();
            }
            return agentTaskSnapshot;
          })
          orderby s.StartedAt descending
          select s).ToList<AgentTaskSnapshot>();
      }
    }

    // Token: 0x17000084 RID: 132
    // (get) Token: 0x060001BC RID: 444 RVA: 0x000DBF94 File Offset: 0x000DBF94
    public bool IsRuntimeReady
    {
      get
      {
        return this.runtimeFilesPrepared;
      }
    }

    // Token: 0x060001BD RID: 445 RVA: 0x000DBF9C File Offset: 0x000DBF9C
    public async Task EnsureRuntimeReadyAsync(CancellationToken ct = default(CancellationToken))
    {
      this.RefetchComposioIfConnectedAppsChanged();
      if (this.runtimeFilesPrepared && !this.configDirty && !this.ComposioSessionIsStale)
      {
        Func<bool> customConnectorTokensExpiring = this.CustomConnectorTokensExpiring;
        if (customConnectorTokensExpiring == null || !customConnectorTokensExpiring())
        {
          return;
        }
      }
      await this.runtimeGate.WaitAsync(ct).ConfigureAwait(false);
      try
      {
        await this.PrepareRuntimeLockedAsync(ct).ConfigureAwait(false);
      }
      finally
      {
        this.runtimeGate.Release();
      }
    }

    // Token: 0x060001BE RID: 446 RVA: 0x000DBFE8 File Offset: 0x000DBFE8
    public Task<string> LaunchAsync(string prompt, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<byte[]> imagesJpeg = null, CancellationToken ct = default(CancellationToken))
    {
      return this.LaunchAsync(new AgentLaunchRequest
      {
        Prompt = prompt,
        ImagesJpeg = imagesJpeg
      }, ct);
    }

    // Token: 0x060001BF RID: 447 RVA: 0x000DC004 File Offset: 0x000DC004
    public Task SteerAsync(string taskId, string text, CancellationToken ct = default(CancellationToken))
    {
      return this.SteerAsync(taskId, text, null, ct, false);
    }

    // Token: 0x060001C0 RID: 448 RVA: 0x000DC014 File Offset: 0x000DC014
    public Task SteerAsync(string taskId, string text, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<byte[]> imagesJpeg, CancellationToken ct = default(CancellationToken), bool carriesCompanionConversation = false)
    {
      AgentTask task = this.GetTask(taskId);
      if (carriesCompanionConversation)
      {
        Action<string> forgetDeclinedComputerUseSteps = this.ForgetDeclinedComputerUseSteps;
        if (forgetDeclinedComputerUseSteps != null)
        {
          forgetDeclinedComputerUseSteps(taskId);
        }
      }
      IReadOnlyList<ValueTuple<string, string>> readOnlyList;
      if (carriesCompanionConversation)
      {
        string text2 = this.ConversationKey(task);
        if (text2 != null)
        {
          Func<string, IReadOnlyList<ValueTuple<string, string>>> companionTurnsSinceLastMessage = this.CompanionTurnsSinceLastMessage;
          readOnlyList = ((companionTurnsSinceLastMessage != null) ? companionTurnsSinceLastMessage(text2) : null);
          goto IL_0043;
        }
      }
      readOnlyList = null;
      IL_0043:
      IReadOnlyList<ValueTuple<string, string>> readOnlyList2 = readOnlyList;
      return this.SteerAsync(task, text, imagesJpeg, (readOnlyList2 != null && readOnlyList2.Count > 0) ? readOnlyList2 : null, ct);
    }

    // Token: 0x060001C1 RID: 449 RVA: 0x000DC080 File Offset: 0x000DC080
    private async Task SteerAsync(AgentTask task, string text, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<byte[]> imagesJpeg, [TupleElementNames(new string[] { "User", "Assistant" })] [Nullable(new byte[] { 2, 0, 1, 1 })] IReadOnlyList<ValueTuple<string, string>> priorTurns, CancellationToken ct)
    {
      AgentService.<>c__DisplayClass150_0 CS$<>8__locals1 = new AgentService.<>c__DisplayClass150_0();
      CS$<>8__locals1.task = task;
      string taskId = CS$<>8__locals1.task.Id;
      object obj = CS$<>8__locals1.task.Gate;
      string threadId;
      AgentTaskStatus agentTaskStatus;
      lock (obj)
      {
        threadId = CS$<>8__locals1.task.ThreadId;
        agentTaskStatus = CS$<>8__locals1.task.Status;
      }
      if (agentTaskStatus == AgentTaskStatus.Starting)
      {
        Stopwatch sw = Stopwatch.StartNew();
        while (sw.Elapsed < TimeSpan.FromSeconds(60L))
        {
          obj = CS$<>8__locals1.task.Gate;
          lock (obj)
          {
            threadId = CS$<>8__locals1.task.ThreadId;
            agentTaskStatus = CS$<>8__locals1.task.Status;
          }
          if (agentTaskStatus != AgentTaskStatus.Starting || (threadId != null && this.session.IsThreadActive(threadId)))
          {
            break;
          }
          await Task.Delay(200, ct).ConfigureAwait(false);
        }
        sw = null;
      }
      if (threadId != null && this.session.IsHarnessTurnRunning(threadId))
      {
        obj = CS$<>8__locals1.task.Gate;
        lock (obj)
        {
          CS$<>8__locals1.task.UserPrompts.Add(text);
        }
        bool flag2;
        try
        {
          List<string> list = await this.WriteTaskImagesAsync(CS$<>8__locals1.task, imagesJpeg, "steer", ct).ConfigureAwait(false);
          string text2 = ((priorTurns == null) ? text : (text + "\n" + TurnPromptBuilder.PriorConversationSection(priorTurns)));
          flag2 = await this.session.SteerHarnessTurnAsync(threadId, text2, ct, false, list).ConfigureAwait(false);
        }
        catch (Exception)
        {
          obj = CS$<>8__locals1.task.Gate;
          bool flag = false;
          try
          {
            Monitor.Enter(obj, ref flag);
            CS$<>8__locals1.task.UserPrompts.RemoveAt(CS$<>8__locals1.task.UserPrompts.Count - 1);
          }
          finally
          {
            int num;
            if (num < 0 && flag)
            {
              Monitor.Exit(obj);
            }
          }
          throw;
        }
        if (flag2)
        {
          obj = CS$<>8__locals1.task.Gate;
          lock (obj)
          {
            CS$<>8__locals1.task.ProgressSummary = "Steering the agent…";
          }
          this.Raise(CS$<>8__locals1.task, AgentTaskEventKind.ProgressChanged, "Steering the agent…");
          this.Raise(CS$<>8__locals1.task, AgentTaskEventKind.SteerAccepted, text);
          this.LogAgentMessage(CS$<>8__locals1.task, threadId, "user", text, true);
          Log.Info("Agents", "steered harness task " + taskId);
        }
        else
        {
          obj = CS$<>8__locals1.task.Gate;
          lock (obj)
          {
            CS$<>8__locals1.task.UserPrompts.RemoveAt(CS$<>8__locals1.task.UserPrompts.Count - 1);
          }
          Stopwatch sw = Stopwatch.StartNew();
          for (;;)
          {
            if (!this.session.IsThreadActive(threadId))
            {
              AgentTask task2 = CS$<>8__locals1.task;
              Func<bool> func;
              if ((func = CS$<>8__locals1.<>9__0) == null)
              {
                AgentService.<>c__DisplayClass150_0 CS$<>8__locals2 = CS$<>8__locals1;
                Func<bool> func2 = () => CS$<>8__locals1.task.IsActive;
                CS$<>8__locals2.<>9__0 = func2;
                func = func2;
              }
              if (!AgentService.Lock<bool>(task2, func))
              {
                break;
              }
            }
            if (!(sw.Elapsed < TimeSpan.FromSeconds(25L)))
            {
              break;
            }
            await Task.Delay(100, ct).ConfigureAwait(false);
          }
          Log.Info("Agents", "harness task " + taskId + " had just finished: the message starts a new turn");
          await this.StartFollowUpTurnAsync(CS$<>8__locals1.task, text, imagesJpeg, "text", "companion", null, ct, null, null, false, priorTurns).ConfigureAwait(false);
        }
      }
      else
      {
        await this.StartFollowUpTurnAsync(CS$<>8__locals1.task, text, imagesJpeg, "text", "companion", null, ct, null, null, false, priorTurns).ConfigureAwait(false);
      }
    }

    // Token: 0x060001C2 RID: 450 RVA: 0x000DC0F0 File Offset: 0x000DC0F0
    private async Task<List<string>> WriteTaskImagesAsync(AgentTask task, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<byte[]> imagesJpeg, string prefix, CancellationToken ct)
    {
      List<string> paths = new List<string>();
      List<string> list;
      if (imagesJpeg == null || imagesJpeg.Count <= 0)
      {
        list = paths;
      }
      else
      {
        string dir = this.home.TaskAssetsDirectory(task.NumericId);
        for (int i = 0; i < imagesJpeg.Count; i++)
        {
          string text = dir;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 3);
          defaultInterpolatedStringHandler.AppendFormatted(prefix);
          defaultInterpolatedStringHandler.AppendLiteral("-");
          defaultInterpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now, "HHmmss");
          defaultInterpolatedStringHandler.AppendLiteral("-");
          defaultInterpolatedStringHandler.AppendFormatted<int>(i + 1);
          defaultInterpolatedStringHandler.AppendLiteral(".jpg");
          string path = Path.Combine(text, defaultInterpolatedStringHandler.ToStringAndClear());
          await File.WriteAllBytesAsync(path, imagesJpeg[i], ct).ConfigureAwait(false);
          paths.Add(path);
          path = null;
        }
        list = paths;
      }
      return list;
    }

    // Token: 0x060001C3 RID: 451 RVA: 0x000DC154 File Offset: 0x000DC154
    public async Task StopAsync(string taskId)
    {
      AgentTask task = this.GetTask(taskId);
      object obj = task.Gate;
      AgentTurnLease lease;
      DateTimeOffset? leaseRecordedAt;
      lock (obj)
      {
        lease = task.Lease;
        leaseRecordedAt = task.LeaseRecordedAt;
      }
      if (lease != null && leaseRecordedAt != null)
      {
        DateTimeOffset valueOrDefault = leaseRecordedAt.GetValueOrDefault();
        if (DateTimeOffset.Now - valueOrDefault <= AgentService.EarlyStopRefundWindow)
        {
          try
          {
            string text = await this.supabaseAccessToken(this.lifetime.Token).ConfigureAwait(false);
            bool flag = text != null;
            if (flag)
            {
              flag = await this.api.RefundEarlyStopAsync(text, lease.LeaseId, this.lifetime.Token).ConfigureAwait(false);
            }
            if (flag)
            {
              obj = task.Gate;
              lock (obj)
              {
                task.DidRefundEarlyStop = true;
              }
              this.Raise(task, AgentTaskEventKind.EarlyStopRefunded, "Stopped. This didn't count toward your agent messages because you stopped it before it really got to work.");
            }
          }
          catch (Exception ex)
          {
            Log.Warn("Agents", "early-stop refund failed: " + ex.Message);
          }
        }
      }
      if (!this.session.Interrupt(task, TurnOutcome.Interrupted, "Stopped."))
      {
        obj = task.Gate;
        CancellationTokenSource turnCts;
        lock (obj)
        {
          turnCts = task.TurnCts;
        }
        if (turnCts != null)
        {
          turnCts.Cancel();
        }
        bool flag2 = false;
        obj = task.Gate;
        lock (obj)
        {
          if (task.IsActive || task.Status == AgentTaskStatus.WaitingForUser)
          {
            task.Status = AgentTaskStatus.Stopped;
            task.ProgressSummary = "Stopped.";
            task.RequiresExtraUsageApproval = false;
            flag2 = true;
          }
        }
        if (flag2)
        {
          this.Raise(task, AgentTaskEventKind.Stopped, "Stopped.");
          this.CompleteLeaseInBackground(task, "interrupted");
          this.RaiseTasksChanged();
        }
      }
    }

    // Token: 0x060001C4 RID: 452 RVA: 0x000DC1A0 File Offset: 0x000DC1A0
    public Task<string> LaunchAsync(AgentLaunchRequest request, CancellationToken ct = default(CancellationToken))
    {
      AgentService.<>c__DisplayClass153_0 CS$<>8__locals1 = new AgentService.<>c__DisplayClass153_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.request = request;
      CS$<>8__locals1.ct = ct;
      ObjectDisposedException.ThrowIf(this.disposed, this);
      AgentService.<>c__DisplayClass153_0 CS$<>8__locals2 = CS$<>8__locals1;
      string clickySlug = CS$<>8__locals1.request.ClickySlug;
      CS$<>8__locals2.clicky = ((clickySlug != null) ? this.clickys.Get(clickySlug) : null);
      if (CS$<>8__locals1.request.ClickySlug != null && CS$<>8__locals1.clicky == null)
      {
        throw new ArgumentException("Unknown Clicky '" + CS$<>8__locals1.request.ClickySlug + "'.");
      }
      if (CS$<>8__locals1.clicky == null && CS$<>8__locals1.request.JobId == null)
      {
        string targetThreadId = CS$<>8__locals1.request.TargetThreadId;
        if (targetThreadId != null)
        {
          AgentService.<>c__DisplayClass153_0 CS$<>8__locals3 = CS$<>8__locals1;
          ClickyInfo clickyInfo = this.clickys.ForThread(targetThreadId);
          CS$<>8__locals3.clicky = ((clickyInfo != null && !clickyInfo.IsArchived) ? clickyInfo : null);
        }
      }
      AgentService.<>c__DisplayClass153_0 CS$<>8__locals4 = CS$<>8__locals1;
      ClickyJob clickyJob;
      if (CS$<>8__locals1.clicky == null)
      {
        string jobId = CS$<>8__locals1.request.JobId;
        if (jobId == null)
        {
          string targetThreadId2 = CS$<>8__locals1.request.TargetThreadId;
          clickyJob = ((targetThreadId2 != null) ? this.Jobs.JobForThread(targetThreadId2) : null);
        }
        else if ((clickyJob = this.Jobs.Job(jobId)) == null)
        {
          throw new ArgumentException("Unknown job '" + jobId + "'.");
        }
      }
      else
      {
        clickyJob = null;
      }
      CS$<>8__locals4.job = clickyJob;
      object obj;
      if (CS$<>8__locals1.job != null)
      {
        CS$<>8__locals1.existingJobTask = this.tasks.Values.FirstOrDefault<AgentTask>((AgentTask t) => AgentService.Lock<string>(t, () => t.JobId) == CS$<>8__locals1.job.Id);
        if (CS$<>8__locals1.existingJobTask != null)
        {
          obj = CS$<>8__locals1.existingJobTask.Gate;
          lock (obj)
          {
            CS$<>8__locals1.existingJobTask.ProactiveSuggestionId = CS$<>8__locals1.request.ProactiveSuggestionId;
            CS$<>8__locals1.existingJobTask.IsDemo = CS$<>8__locals1.request.UsageContext == "onboarding";
            CS$<>8__locals1.existingJobTask.IsSentFromItsChat = CS$<>8__locals1.request.IsHudFollowUp;
          }
          this.ForgetDeclinedStepsIfUserMessage(CS$<>8__locals1.existingJobTask.Id, CS$<>8__locals1.request.LaunchSource);
          Task.Run(delegate
          {
            AgentService.<>c__DisplayClass153_0.<<LaunchAsync>b__1>d <<LaunchAsync>b__1>d;
            <<LaunchAsync>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
            <<LaunchAsync>b__1>d.<>4__this = CS$<>8__locals1;
            <<LaunchAsync>b__1>d.<>1__state = -1;
            <<LaunchAsync>b__1>d.<>t__builder.Start<AgentService.<>c__DisplayClass153_0.<<LaunchAsync>b__1>d>(ref <<LaunchAsync>b__1>d);
            return <<LaunchAsync>b__1>d.<>t__builder.Task;
          }, CancellationToken.None);
          return Task.FromResult<string>(CS$<>8__locals1.existingJobTask.Id);
        }
      }
      if (CS$<>8__locals1.clicky != null)
      {
        CS$<>8__locals1.existing = this.tasks.Values.FirstOrDefault<AgentTask>((AgentTask t) => t.ClickySlug == CS$<>8__locals1.clicky.Slug);
        if (CS$<>8__locals1.existing != null)
        {
          obj = CS$<>8__locals1.existing.Gate;
          lock (obj)
          {
            CS$<>8__locals1.existing.ProactiveSuggestionId = CS$<>8__locals1.request.ProactiveSuggestionId;
            CS$<>8__locals1.existing.IsDemo = CS$<>8__locals1.request.UsageContext == "onboarding";
            CS$<>8__locals1.existing.IsSentFromItsChat = CS$<>8__locals1.request.IsHudFollowUp;
          }
          this.ForgetDeclinedStepsIfUserMessage(CS$<>8__locals1.existing.Id, CS$<>8__locals1.request.LaunchSource);
          Task.Run(delegate
          {
            AgentService.<>c__DisplayClass153_0.<<LaunchAsync>b__3>d <<LaunchAsync>b__3>d;
            <<LaunchAsync>b__3>d.<>t__builder = AsyncTaskMethodBuilder.Create();
            <<LaunchAsync>b__3>d.<>4__this = CS$<>8__locals1;
            <<LaunchAsync>b__3>d.<>1__state = -1;
            <<LaunchAsync>b__3>d.<>t__builder.Start<AgentService.<>c__DisplayClass153_0.<<LaunchAsync>b__3>d>(ref <<LaunchAsync>b__3>d);
            return <<LaunchAsync>b__3>d.<>t__builder.Task;
          }, CancellationToken.None);
          return Task.FromResult<string>(CS$<>8__locals1.existing.Id);
        }
      }
      string displayPrompt = CS$<>8__locals1.request.DisplayPrompt;
      if (displayPrompt != null && displayPrompt.Length > 0)
      {
        this.clickys.RecordDisplayPrompt(CS$<>8__locals1.request.Prompt, displayPrompt);
      }
      CS$<>8__locals1.isNewJob = false;
      bool flag2 = false;
      if (CS$<>8__locals1.clicky == null && CS$<>8__locals1.job == null)
      {
        string text = CS$<>8__locals1.request.Prompt.Trim();
        AgentService.<>c__DisplayClass153_0 CS$<>8__locals5 = CS$<>8__locals1;
        ClickyJobStore jobs = this.Jobs;
        string newClickyName = CS$<>8__locals1.request.NewClickyName;
        string text2 = ((newClickyName != null) ? newClickyName.Trim() : null);
        CS$<>8__locals5.job = jobs.CreateJob((text2 != null && text2.Length > 0) ? text2 : text, text, this.options.ProjectsRoot, true);
        string targetThreadId3 = CS$<>8__locals1.request.TargetThreadId;
        if (targetThreadId3 != null)
        {
          this.Jobs.RecordThreadId(targetThreadId3, CS$<>8__locals1.job.Id);
          string launchSource = CS$<>8__locals1.request.LaunchSource;
          bool flag = launchSource == "text" || launchSource == "voice";
          flag2 = flag && !AgentHarnessThreads.ContinuesOnNewConversationHarness(targetThreadId3) && !this.session.IsThreadActive(targetThreadId3);
        }
        CS$<>8__locals1.isNewJob = true;
      }
      CS$<>8__locals1.task = new AgentTask
      {
        LaunchSource = CS$<>8__locals1.request.LaunchSource,
        LaunchTimestamp = Stopwatch.GetTimestamp(),
        IsNewJobRun = flag2
      };
      ClickyInfo clicky = CS$<>8__locals1.clicky;
      string text3;
      if ((text3 = ((clicky != null) ? clicky.ThreadId : null)) == null)
      {
        text3 = ((CS$<>8__locals1.job != null && !CS$<>8__locals1.request.StartsNewJobConversation && !flag2) ? (CS$<>8__locals1.request.TargetThreadId ?? CS$<>8__locals1.job.ThreadId) : null);
      }
      string text4 = text3;
      obj = CS$<>8__locals1.task.Gate;
      lock (obj)
      {
        CS$<>8__locals1.task.UserPrompts.Add(CS$<>8__locals1.request.DisplayPrompt ?? CS$<>8__locals1.request.Prompt);
        CS$<>8__locals1.task.IsRoutineRun = CS$<>8__locals1.request.LaunchSource == "routine";
        CS$<>8__locals1.task.IsDemo = CS$<>8__locals1.request.UsageContext == "onboarding";
        CS$<>8__locals1.task.IsSentFromItsChat = CS$<>8__locals1.request.IsHudFollowUp;
        CS$<>8__locals1.task.ProactiveSuggestionId = CS$<>8__locals1.request.ProactiveSuggestionId;
        if (text4 == null)
        {
          CS$<>8__locals1.task.PendingInitialMessageLog = CS$<>8__locals1.request.DisplayPrompt ?? CS$<>8__locals1.request.Prompt;
        }
        if (CS$<>8__locals1.clicky != null)
        {
          CS$<>8__locals1.task.ClickySlug = CS$<>8__locals1.clicky.Slug;
          CS$<>8__locals1.task.ClickyName = CS$<>8__locals1.clicky.Name;
          CS$<>8__locals1.task.Title = CS$<>8__locals1.clicky.Name;
          CS$<>8__locals1.task.ThreadId = CS$<>8__locals1.clicky.ThreadId;
        }
        else if (CS$<>8__locals1.job != null)
        {
          CS$<>8__locals1.task.JobId = CS$<>8__locals1.job.Id;
          CS$<>8__locals1.task.Title = CS$<>8__locals1.job.Title;
          CS$<>8__locals1.task.ThreadId = text4;
          CS$<>8__locals1.task.WorkspacePath = CS$<>8__locals1.job.FolderPath;
        }
        CS$<>8__locals1.task.TurnCts = CancellationTokenSource.CreateLinkedTokenSource(this.lifetime.Token);
      }
      this.tasks[CS$<>8__locals1.task.Id] = CS$<>8__locals1.task;
      if (text4 != null)
      {
        string text5 = text4;
        this.LogAgentMessage(CS$<>8__locals1.task, text5, "user", CS$<>8__locals1.request.DisplayPrompt ?? CS$<>8__locals1.request.Prompt, true);
      }
      this.RaiseTasksChanged();
      this.Raise(CS$<>8__locals1.task, AgentTaskEventKind.StatusChanged, "Starting");
      Task.Run(() => CS$<>8__locals1.<>4__this.RunLaunchAsync(CS$<>8__locals1.task, CS$<>8__locals1.request, CS$<>8__locals1.clicky, CS$<>8__locals1.job, CS$<>8__locals1.isNewJob), CancellationToken.None);
      return Task.FromResult<string>(CS$<>8__locals1.task.Id);
    }

    // Token: 0x060001C5 RID: 453 RVA: 0x000DC904 File Offset: 0x000DC904
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<string> TextStartCueAsync(string prompt, bool isFreshThread, CancellationToken ct = default(CancellationToken))
    {
      string text2;
      try
      {
        string text = await this.supabaseAccessToken(ct).ConfigureAwait(false);
        if (text == null)
        {
          text2 = null;
        }
        else
        {
          text2 = await this.api.TextStartCueAsync(text, prompt, isFreshThread, ct).ConfigureAwait(false);
        }
      }
      catch (Exception ex) when (!(ex is OperationCanceledException))
      {
        Log.Warn("Agents", "text start cue: " + ex.Message);
        text2 = null;
      }
      return text2;
    }

    // Token: 0x060001C6 RID: 454 RVA: 0x000DC960 File Offset: 0x000DC960
    [return: Nullable(2)]
    public AgentTaskDetail GetTaskDetail(string taskId)
    {
      AgentTask t;
      if (!this.tasks.TryGetValue(taskId, out t))
      {
        return null;
      }
      AgentTaskDetail agentTaskDetail = AgentService.Lock<AgentTaskDetail>(t, () => t.Detail());
      if (agentTaskDetail != null && agentTaskDetail.JobId == null && agentTaskDetail.ClickySlug == null)
      {
        AgentTaskSnapshot snapshot = agentTaskDetail.Snapshot;
        if (snapshot != null)
        {
          string threadId = snapshot.ThreadId;
          if (threadId != null)
          {
            ClickyJob clickyJob = this.Jobs.JobForThread(threadId);
            if (clickyJob != null)
            {
              AgentTaskDetail agentTaskDetail2 = agentTaskDetail.<Clone>$();
              agentTaskDetail2.JobId = clickyJob.Id;
              return agentTaskDetail2;
            }
          }
        }
      }
      return agentTaskDetail;
    }

    // Token: 0x060001C7 RID: 455 RVA: 0x000DC9F0 File Offset: 0x000DC9F0
    public void DismissTask(string taskId)
    {
      AgentTask t;
      AgentTask agentTask;
      if (this.tasks.TryGetValue(taskId, out t) && !AgentService.Lock<bool>(t, () => t.IsActive) && this.tasks.TryRemove(taskId, out agentTask))
      {
        this.RaiseTasksChanged();
      }
    }

    // Token: 0x060001C8 RID: 456 RVA: 0x000DCA48 File Offset: 0x000DCA48
    public async Task ApproveExtraUsageAsync(string taskId, bool asManyAsNeeded, CancellationToken ct = default(CancellationToken), bool remembersAlways = true)
    {
      AgentTask task = this.GetTask(taskId);
      object obj = task.Gate;
      AgentTurnLease lease;
      lock (obj)
      {
        lease = task.Lease;
      }
      if (lease == null)
      {
        throw new InvalidOperationException("This task has no credit lease to continue.");
      }
      string text = await this.RequireSupabaseTokenAsync(ct).ConfigureAwait(false);
      AgentTurnLease agentTurnLease;
      try
      {
        agentTurnLease = await this.api.ContinueLeaseAsync(text, lease.LeaseId, asManyAsNeeded, ct).ConfigureAwait(false);
      }
      catch (AgentBackendException ex) when (ex.IsPaywall)
      {
        this.RaisePaywall(ex, true);
        CompanionError companionError = new CompanionError("agent_paywall", "You're out of agent messages.", ex.BodyJson);
        obj = task.Gate;
        bool flag = false;
        try
        {
          Monitor.Enter(obj, ref flag);
          task.Error = companionError;
        }
        finally
        {
          int num;
          if (num < 0 && flag)
          {
            Monitor.Exit(obj);
          }
        }
        this.Raise(task, AgentTaskEventKind.NeedsExtraUsageApproval, companionError.Message);
        throw new AgentPaywallException(companionError);
      }
      if (agentTurnLease == null || agentTurnLease.RequiresExtraEffort)
      {
        obj = task.Gate;
        lock (obj)
        {
          if (agentTurnLease != null)
          {
            task.Lease = agentTurnLease;
          }
        }
        this.Raise(task, AgentTaskEventKind.NeedsExtraUsageApproval, "Still needs approval.");
      }
      else
      {
        this.Raise(task, AgentTaskEventKind.ExtraUsageApproved, null);
        if (asManyAsNeeded && remembersAlways && !this.options.AutoApproveExtraUsage)
        {
          this.options.AutoApproveExtraUsage = true;
          Action<bool> autoApproveExtraUsageChanged = this.AutoApproveExtraUsageChanged;
          if (autoApproveExtraUsageChanged != null)
          {
            autoApproveExtraUsageChanged(true);
          }
        }
        string text2 = (asManyAsNeeded ? "The user approved the extra usage to run this HeyClicky agent task to completion. Continue the same task from where it stopped." : "The user approved continuing this HeyClicky agent task once. Continue the same task from where it stopped.");
        await this.StartFollowUpTurnAsync(task, "Keep going.", null, "extra_effort", "companion", agentTurnLease, ct, text2, null, false, null).ConfigureAwait(false);
      }
    }

    // Token: 0x060001C9 RID: 457 RVA: 0x000DCAAC File Offset: 0x000DCAAC
    public Task PrewarmAsync(CancellationToken ct = default(CancellationToken))
    {
      AgentService.<>c__DisplayClass158_0 CS$<>8__locals1 = new AgentService.<>c__DisplayClass158_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.ct = ct;
      this.SyncWorkOwnerStamp();
      if (this.options.EnableRoutines)
      {
        this.routines.Start();
      }
      this.RestorePendingAgentRequestsFromLastRun();
      this.StepWatcher.Reconcile();
      this.NameJobsWithoutNicknames();
      return Task.Run(delegate
      {
        AgentService.<>c__DisplayClass158_0.<<PrewarmAsync>b__0>d <<PrewarmAsync>b__0>d;
        <<PrewarmAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<PrewarmAsync>b__0>d.<>4__this = CS$<>8__locals1;
        <<PrewarmAsync>b__0>d.<>1__state = -1;
        <<PrewarmAsync>b__0>d.<>t__builder.Start<AgentService.<>c__DisplayClass158_0.<<PrewarmAsync>b__0>d>(ref <<PrewarmAsync>b__0>d);
        return <<PrewarmAsync>b__0>d.<>t__builder.Task;
      }, CS$<>8__locals1.ct);
    }

    // Token: 0x060001CA RID: 458 RVA: 0x000DCB1C File Offset: 0x000DCB1C
    public Task SignOutAsync()
    {
      this.routines.Stop();
      foreach (AgentTask agentTask in this.tasks.Values.ToList<AgentTask>())
      {
        this.session.Interrupt(agentTask, TurnOutcome.Interrupted, "Signed out.");
      }
      Interlocked.Increment(ref this.accountGeneration);
      Interlocked.Increment(ref this.composioGeneration);
      this.tokens.Invalidate();
      this.clickys.ClearHiddenThreads();
      this.composio = null;
      this.composioFetchedAt = default(DateTimeOffset);
      this.composioRefetchedForConnectedApps = null;
      this.lastIntegrationCatalog = null;
      this.tasks.Clear();
      this.RemoveJobsForSignOut();
      this.RaiseTasksChanged();
      return Task.CompletedTask;
    }

    // Token: 0x060001CB RID: 459 RVA: 0x000DCBFC File Offset: 0x000DCBFC
    public void NotifyResumedFromSleep()
    {
      this.routines.HandleFullWake("resume");
    }

    // Token: 0x060001CC RID: 460 RVA: 0x000DCC10 File Offset: 0x000DCC10
    public void NotifySystemSleeping()
    {
      this.routines.NoteSystemSleeping();
    }

    // Token: 0x060001CD RID: 461 RVA: 0x000DCC20 File Offset: 0x000DCC20
    public void NotifyDisplayPower(bool isOn)
    {
      this.routines.NoteDisplayPower(isOn);
    }

    // Token: 0x060001CE RID: 462 RVA: 0x000DCC30 File Offset: 0x000DCC30
    public void SetCustomConnectors(IReadOnlyList<CustomConnectorRegistration> connectors)
    {
      this.customConnectors = connectors;
      this.InvalidateConnectedApps();
    }

    // Token: 0x060001CF RID: 463 RVA: 0x000DCC40 File Offset: 0x000DCC40
    public void InvalidateConnectedApps()
    {
      this.composioFetchedAt = default(DateTimeOffset);
      this.composioRefetchedForConnectedApps = null;
      this.configDirty = true;
      Interlocked.Increment(ref this.composioGeneration);
    }

    // Token: 0x17000085 RID: 133
    // (get) Token: 0x060001D0 RID: 464 RVA: 0x000DCC68 File Offset: 0x000DCC68
    private bool ComposioSessionIsStale
    {
      get
      {
        return ComposioSessionReuse.IsStale(this.composioFetchedAt, DateTimeOffset.Now, AgentService.ComposioRefreshAfter);
      }
    }

    // Token: 0x060001D1 RID: 465 RVA: 0x000DCC80 File Offset: 0x000DCC80
    private void RefetchComposioIfConnectedAppsChanged()
    {
      if (this.ComposioSessionIsStale)
      {
        return;
      }
      ComposioMcpConfiguration composioMcpConfiguration = this.composio;
      HashSet<string> hashSet = (((composioMcpConfiguration != null) ? composioMcpConfiguration.Toolkits : null) ?? Array.Empty<string>()).Select<string, string>((string t) => t.ToLowerInvariant()).ToHashSet<string>();
      if (!ComposioSessionReuse.ShouldRefetchForConnectedApps(ComposioSessionReuse.ConnectedToolkits(this.lastIntegrationCatalog), hashSet, ref this.composioRefetchedForConnectedApps))
      {
        return;
      }
      Log.Info("Agents", "composio session: the integrations list's connected apps differ from the session's; fetching a fresh one");
      this.composioFetchedAt = default(DateTimeOffset);
      this.configDirty = true;
    }

    // Token: 0x060001D2 RID: 466 RVA: 0x000DCD18 File Offset: 0x000DCD18
    private async Task<T> WithSessionTokenAsync<[Nullable(2)] T>(Func<string, Task<T>> call, CancellationToken ct)
    {
      int num = 0;
      try
      {
        Func<string, Task<T>> func = call;
        string text = await this.tokens.GetAsync(ct, false).ConfigureAwait(false);
        T t = await func(text).ConfigureAwait(false);
        func = null;
        return t;
      }
      catch (Exception ex) when (AgentWorkerApi.IsUnauthorized((Exception)ex))
      {
        num = 1;
      }
      T t3;
      if (num == 1)
      {
        Func<string, Task<T>> func = call;
        string text = await this.tokens.GetAsync(ct, true).ConfigureAwait(false);
        T t2 = await func(text).ConfigureAwait(false);
        func = null;
        t3 = t2;
      }
      return t3;
    }

    // Token: 0x060001D3 RID: 467 RVA: 0x000DCD6C File Offset: 0x000DCD6C
    public async Task<AgentIntegrationCatalog> ListIntegrationsAsync(CancellationToken ct = default(CancellationToken))
    {
      AgentIntegrationCatalog agentIntegrationCatalog = await this.WithSessionTokenAsync<AgentIntegrationCatalog>((string token) => this.api.ListIntegrationsAsync(token, ct), ct).ConfigureAwait(false);
      this.lastIntegrationCatalog = agentIntegrationCatalog;
      return agentIntegrationCatalog;
    }

    // Token: 0x060001D4 RID: 468 RVA: 0x000DCDB8 File Offset: 0x000DCDB8
    public Task<string> ConnectIntegrationAsync(string toolkit, CancellationToken ct = default(CancellationToken))
    {
      return this.WithSessionTokenAsync<string>((string token) => this.api.ConnectIntegrationAsync(token, toolkit, ct), ct);
    }

    // Token: 0x060001D5 RID: 469 RVA: 0x000DCDF8 File Offset: 0x000DCDF8
    public async Task DisconnectIntegrationAsync(string toolkit, [Nullable(2)] string connectedAccountId = null, CancellationToken ct = default(CancellationToken))
    {
      AgentService.<>c__DisplayClass173_0 CS$<>8__locals1 = new AgentService.<>c__DisplayClass173_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.toolkit = toolkit;
      CS$<>8__locals1.connectedAccountId = connectedAccountId;
      CS$<>8__locals1.ct = ct;
      await this.WithSessionTokenAsync<bool>(delegate(string token)
      {
        AgentService.<>c__DisplayClass173_0.<<DisconnectIntegrationAsync>b__0>d <<DisconnectIntegrationAsync>b__0>d;
        <<DisconnectIntegrationAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
        <<DisconnectIntegrationAsync>b__0>d.<>4__this = CS$<>8__locals1;
        <<DisconnectIntegrationAsync>b__0>d.token = token;
        <<DisconnectIntegrationAsync>b__0>d.<>1__state = -1;
        <<DisconnectIntegrationAsync>b__0>d.<>t__builder.Start<AgentService.<>c__DisplayClass173_0.<<DisconnectIntegrationAsync>b__0>d>(ref <<DisconnectIntegrationAsync>b__0>d);
        return <<DisconnectIntegrationAsync>b__0>d.<>t__builder.Task;
      }, CS$<>8__locals1.ct).ConfigureAwait(false);
      this.InvalidateConnectedApps();
    }

    // Token: 0x060001D6 RID: 470 RVA: 0x000DCE54 File Offset: 0x000DCE54
    public async Task LabelIntegrationAccountAsync(string toolkit, string connectedAccountId, string label, CancellationToken ct = default(CancellationToken))
    {
      AgentService.<>c__DisplayClass174_0 CS$<>8__locals1 = new AgentService.<>c__DisplayClass174_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.toolkit = toolkit;
      CS$<>8__locals1.connectedAccountId = connectedAccountId;
      CS$<>8__locals1.label = label;
      CS$<>8__locals1.ct = ct;
      await this.WithSessionTokenAsync<bool>(delegate(string token)
      {
        AgentService.<>c__DisplayClass174_0.<<LabelIntegrationAccountAsync>b__0>d <<LabelIntegrationAccountAsync>b__0>d;
        <<LabelIntegrationAccountAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
        <<LabelIntegrationAccountAsync>b__0>d.<>4__this = CS$<>8__locals1;
        <<LabelIntegrationAccountAsync>b__0>d.token = token;
        <<LabelIntegrationAccountAsync>b__0>d.<>1__state = -1;
        <<LabelIntegrationAccountAsync>b__0>d.<>t__builder.Start<AgentService.<>c__DisplayClass174_0.<<LabelIntegrationAccountAsync>b__0>d>(ref <<LabelIntegrationAccountAsync>b__0>d);
        return <<LabelIntegrationAccountAsync>b__0>d.<>t__builder.Task;
      }, CS$<>8__locals1.ct).ConfigureAwait(false);
      this.InvalidateConnectedApps();
    }

    // Token: 0x17000086 RID: 134
    // (get) Token: 0x060001D7 RID: 471 RVA: 0x000DCEB8 File Offset: 0x000DCEB8
    public string DefaultModel
    {
      get
      {
        return this.options.Model;
      }
    }

    // Token: 0x17000087 RID: 135
    // (get) Token: 0x060001D8 RID: 472 RVA: 0x000DCEC8 File Offset: 0x000DCEC8
    public string DefaultReasoningEffort
    {
      get
      {
        return this.options.ReasoningEffort;
      }
    }

    // Token: 0x060001D9 RID: 473 RVA: 0x000DCED8 File Offset: 0x000DCED8
    public async Task<double?> LeaseBilledCostAsync(string leaseId, CancellationToken ct = default(CancellationToken))
    {
      string text = await this.supabaseAccessToken(ct).ConfigureAwait(false);
      double? num;
      if (text == null)
      {
        num = null;
      }
      else
      {
        object obj = await this.api.LeaseStatusAsync(text, leaseId, ct).ConfigureAwait(false);
        num = ((obj != null) ? obj.CostUsd : null);
      }
      return num;
    }

    // Token: 0x17000088 RID: 136
    // (get) Token: 0x060001DA RID: 474 RVA: 0x000DCF2C File Offset: 0x000DCF2C
    // (set) Token: 0x060001DB RID: 475 RVA: 0x000DCF34 File Offset: 0x000DCF34
    [Nullable(2)]
    public Func<string> CurrentUserId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000089 RID: 137
    // (get) Token: 0x060001DC RID: 476 RVA: 0x000DCF40 File Offset: 0x000DCF40
    // (set) Token: 0x060001DD RID: 477 RVA: 0x000DCF50 File Offset: 0x000DCF50
    public bool AutoApproveExtraUsage
    {
      get
      {
        return this.options.AutoApproveExtraUsage;
      }
      set
      {
        this.options.AutoApproveExtraUsage = value;
      }
    }

    // Token: 0x1700008A RID: 138
    // (get) Token: 0x060001DE RID: 478 RVA: 0x000DCF60 File Offset: 0x000DCF60
    public bool AllowsDeveloperModelSelection
    {
      get
      {
        return this.tokens.AllowsDeveloperModelSelection;
      }
    }

    // Token: 0x1700008B RID: 139
    // (get) Token: 0x060001DF RID: 479 RVA: 0x000DCF70 File Offset: 0x000DCF70
    // (set) Token: 0x060001E0 RID: 480 RVA: 0x000DCF78 File Offset: 0x000DCF78
    [TupleElementNames(new string[] { "Model", "Effort" })]
    [Nullable(new byte[] { 2, 1, 0, 1, 1 })]
    public Func<string, ValueTuple<string, string>?> DeveloperModelSelection
    {
      [return: TupleElementNames(new string[] { "Model", "Effort" })]
      [return: Nullable(new byte[] { 2, 1, 0, 1, 1 })]
      get;
      [param: TupleElementNames(new string[] { "Model", "Effort" })]
      [param: Nullable(new byte[] { 2, 1, 0, 1, 1 })]
      set;
    }

    // Token: 0x14000008 RID: 8
    // (add) Token: 0x060001E1 RID: 481 RVA: 0x000DCF84 File Offset: 0x000DCF84
    // (remove) Token: 0x060001E2 RID: 482 RVA: 0x000DCFBC File Offset: 0x000DCFBC
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<bool> AutoApproveExtraUsageChanged;

    // Token: 0x060001E3 RID: 483 RVA: 0x000DCFF4 File Offset: 0x000DCFF4
    public void SetProjectsRoot(string path)
    {
      if (string.Equals(this.options.ProjectsRoot, path, StringComparison.OrdinalIgnoreCase))
      {
        return;
      }
      Directory.CreateDirectory(path);
      this.options.ProjectsRoot = path;
      this.clickys.ProjectsRoot = path;
      this.configDirty = true;
      Log.Info("Agents", "agent folder: " + path);
    }

    // Token: 0x1700008C RID: 140
    // (get) Token: 0x060001E4 RID: 484 RVA: 0x000DD054 File Offset: 0x000DD054
    public static string DefaultProjectsRoot
    {
      get
      {
        return new AgentRuntimeOptions().ProjectsRoot;
      }
    }

    // Token: 0x1700008D RID: 141
    // (get) Token: 0x060001E5 RID: 485 RVA: 0x000DD060 File Offset: 0x000DD060
    public IReadOnlyList<ClickyInfo> Clickys
    {
      get
      {
        return this.clickys.Clickys;
      }
    }

    // Token: 0x060001E6 RID: 486 RVA: 0x000DD070 File Offset: 0x000DD070
    [return: Nullable(2)]
    public ClickyInfo FindClicky(string slug)
    {
      return this.clickys.Get(slug);
    }

    // Token: 0x060001E7 RID: 487 RVA: 0x000DD080 File Offset: 0x000DD080
    public Task<string> GetAgentSessionTokenAsync(bool forceRefresh = false, CancellationToken ct = default(CancellationToken))
    {
      return this.tokens.GetAsync(ct, forceRefresh);
    }

    // Token: 0x060001E8 RID: 488 RVA: 0x000DD090 File Offset: 0x000DD090
    public bool IsClickyBusy(string slug)
    {
      return this.tasks.Values.Any<AgentTask>((AgentTask t) => t.ClickySlug == slug && AgentService.Lock<bool>(t, () => t.IsActive));
    }

    // Token: 0x1700008E RID: 142
    // (get) Token: 0x060001E9 RID: 489 RVA: 0x000DD0C8 File Offset: 0x000DD0C8
    public RoutineScheduler Routines
    {
      get
      {
        return this.routines;
      }
    }

    // Token: 0x1700008F RID: 143
    // (get) Token: 0x060001EA RID: 490 RVA: 0x000DD0D0 File Offset: 0x000DD0D0
    public RoutinesTool RoutinesTool { get; }

    // Token: 0x17000090 RID: 144
    // (get) Token: 0x060001EB RID: 491 RVA: 0x000DD0D8 File Offset: 0x000DD0D8
    public string ProjectsRoot
    {
      get
      {
        return this.options.ProjectsRoot;
      }
    }

    // Token: 0x17000091 RID: 145
    // (get) Token: 0x060001EC RID: 492 RVA: 0x000DD0E8 File Offset: 0x000DD0E8
    public string CodexHome
    {
      get
      {
        return this.options.CodexHome;
      }
    }

    // Token: 0x060001ED RID: 493 RVA: 0x000DD0F8 File Offset: 0x000DD0F8
    [return: Nullable(2)]
    public string WorkspacePath(string clickySlug)
    {
      ClickyInfo clickyInfo = this.clickys.Get(clickySlug);
      if (clickyInfo == null)
      {
        return null;
      }
      return this.clickys.WorkspacePath(clickyInfo.Slug);
    }

    // Token: 0x17000092 RID: 146
    // (get) Token: 0x060001EE RID: 494 RVA: 0x000DD128 File Offset: 0x000DD128
    public AgentRuntimeTimings Timings
    {
      get
      {
        return new AgentRuntimeTimings(this.runtimeReadyMs, this.launchToTurnStartedMs, this.launchToFirstTokenMs, this.launchToFinishedMs);
      }
    }

    // Token: 0x060001EF RID: 495 RVA: 0x000DD148 File Offset: 0x000DD148
    public ClickyInfo CreateClicky(string name, string roleText, string oneLiner)
    {
      ClickyInfo clickyInfo = this.clickys.Found(name, roleText, oneLiner);
      this.clickys.EnsureWorkspace(clickyInfo);
      this.RaiseTasksChanged();
      return clickyInfo;
    }

    // Token: 0x060001F0 RID: 496 RVA: 0x000DD178 File Offset: 0x000DD178
    public bool KnowsClicky(string slug)
    {
      return this.clickys.Get(slug) != null;
    }

    // Token: 0x060001F1 RID: 497 RVA: 0x000DD18C File Offset: 0x000DD18C
    [NullableContext(2)]
    [return: TupleElementNames(new string[] { "Threads", "NextCursor" })]
    [return: Nullable(new byte[] { 1, 0, 1, 1, 2 })]
    public async Task<ValueTuple<IReadOnlyList<AgentThreadSummary>, string>> ListThreadsAsync(int limit = 50, string cursor = null, string cwd = null, CancellationToken ct = default(CancellationToken))
    {
      List<CodexRolloutHistory.RolloutThread> list = (await Task.Run<IReadOnlyList<CodexRolloutHistory.RolloutThread>>(() => CodexRolloutHistory.List(this.options.CodexHome), ct).ConfigureAwait(false)).Where<CodexRolloutHistory.RolloutThread>(delegate(CodexRolloutHistory.RolloutThread t)
      {
        if (this.clickys.IsThreadHidden(t.ThreadId))
        {
          return false;
        }
        if (cwd != null)
        {
          string cwd2 = t.Cwd;
          return string.Equals((cwd2 != null) ? cwd2.TrimEnd(new char[] { '\\', '/' }) : null, cwd.TrimEnd(new char[] { '\\', '/' }), StringComparison.OrdinalIgnoreCase);
        }
        return true;
      }).ToList<CodexRolloutHistory.RolloutThread>();
      int num2;
      int num = ((int.TryParse(cursor, out num2) && num2 > 0) ? num2 : 0);
      List<CodexRolloutHistory.RolloutThread> list2 = list.Skip<CodexRolloutHistory.RolloutThread>(num).Take<CodexRolloutHistory.RolloutThread>(Math.Max(1, limit)).ToList<CodexRolloutHistory.RolloutThread>();
      List<AgentThreadSummary> list3 = new List<AgentThreadSummary>();
      foreach (CodexRolloutHistory.RolloutThread rolloutThread in list2)
      {
        string text = AgentService.PreviewText(rolloutThread.FirstUserMessage);
        text = this.clickys.DisplayFor(text) ?? text;
        ClickyInfo clickyInfo = this.clickys.ForThread(rolloutThread.ThreadId);
        list3.Add(new AgentThreadSummary(rolloutThread.ThreadId, this.clickys.TitleForThread(rolloutThread.ThreadId) ?? ((clickyInfo != null) ? clickyInfo.Name : null), text, rolloutThread.CreatedAt, new DateTimeOffset?(rolloutThread.UpdatedAt), "idle", rolloutThread.Cwd, (clickyInfo != null) ? clickyInfo.Slug : null));
      }
      if (cursor == null)
      {
        this.StartPersonaNameBackfillOnce(list3);
      }
      int num3 = num + list2.Count;
      return new ValueTuple<IReadOnlyList<AgentThreadSummary>, string>(list3, (num3 < list.Count) ? num3.ToString(CultureInfo.InvariantCulture) : null);
    }

    // Token: 0x14000009 RID: 9
    // (add) Token: 0x060001F2 RID: 498 RVA: 0x000DD1F0 File Offset: 0x000DD1F0
    // (remove) Token: 0x060001F3 RID: 499 RVA: 0x000DD228 File Offset: 0x000DD228
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler ThreadHistoryChanged;

    // Token: 0x1400000A RID: 10
    // (add) Token: 0x060001F4 RID: 500 RVA: 0x000DD260 File Offset: 0x000DD260
    // (remove) Token: 0x060001F5 RID: 501 RVA: 0x000DD298 File Offset: 0x000DD298
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<string> ClickyFounded;

    // Token: 0x060001F6 RID: 502 RVA: 0x000DD2D0 File Offset: 0x000DD2D0
    private void StartPersonaNameBackfillOnce(IReadOnlyList<AgentThreadSummary> threads)
    {
      AgentService.<>c__DisplayClass227_0 CS$<>8__locals1 = new AgentService.<>c__DisplayClass227_0();
      CS$<>8__locals1.<>4__this = this;
      if (Interlocked.Exchange(ref this.personaNameBackfillStarted, 1) == 1)
      {
        return;
      }
      CS$<>8__locals1.unnamed = threads.Where<AgentThreadSummary>((AgentThreadSummary t) => CS$<>8__locals1.<>4__this.clickys.TitleForThread(t.ThreadId) == null && CS$<>8__locals1.<>4__this.clickys.ForThread(t.ThreadId) == null && (string.IsNullOrWhiteSpace(t.Name) || t.Name.Trim() == "Untitled agent thread") && !string.IsNullOrWhiteSpace(t.Preview)).Take<AgentThreadSummary>(15).ToList<AgentThreadSummary>();
      if (CS$<>8__locals1.unnamed.Count == 0)
      {
        return;
      }
      Task.Run(delegate
      {
        AgentService.<>c__DisplayClass227_0.<<StartPersonaNameBackfillOnce>b__1>d <<StartPersonaNameBackfillOnce>b__1>d;
        <<StartPersonaNameBackfillOnce>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<StartPersonaNameBackfillOnce>b__1>d.<>4__this = CS$<>8__locals1;
        <<StartPersonaNameBackfillOnce>b__1>d.<>1__state = -1;
        <<StartPersonaNameBackfillOnce>b__1>d.<>t__builder.Start<AgentService.<>c__DisplayClass227_0.<<StartPersonaNameBackfillOnce>b__1>d>(ref <<StartPersonaNameBackfillOnce>b__1>d);
        return <<StartPersonaNameBackfillOnce>b__1>d.<>t__builder.Task;
      });
    }

    // Token: 0x060001F7 RID: 503 RVA: 0x000DD340 File Offset: 0x000DD340
    public Task<IReadOnlyList<AgentTranscriptTurn>> ReadThreadAsync(string threadId, CancellationToken ct = default(CancellationToken))
    {
      if (!AgentHarnessPreferences.IsHarnessThread(threadId))
      {
        return Task.Run<IReadOnlyList<AgentTranscriptTurn>>(() => this.RolloutTranscriptTurns(threadId), ct);
      }
      return Task.FromResult<IReadOnlyList<AgentTranscriptTurn>>(Array.Empty<AgentTranscriptTurn>());
    }

    // Token: 0x060001F8 RID: 504 RVA: 0x000DD38C File Offset: 0x000DD38C
    private IReadOnlyList<AgentTranscriptTurn> RolloutTranscriptTurns(string threadId)
    {
      List<AgentTranscriptTurn> list = new List<AgentTranscriptTurn>();
      foreach (CodexRolloutHistory.RolloutTurn rolloutTurn in CodexRolloutHistory.ReadTurns(this.options.CodexHome, threadId))
      {
        List<string> list2 = new List<string>();
        List<string> list3 = new List<string>();
        List<string> list4 = new List<string>();
        List<string> list5 = new List<string>();
        foreach (string text in rolloutTurn.UserMessages)
        {
          string text2 = AgentService.DisplayUserText(text);
          list2.Add(this.clickys.DisplayFor(text2) ?? text2);
        }
        foreach (string text3 in rolloutTurn.Replies)
        {
          StructuredFinalReply structuredFinalReply = StructuredFinalReply.Parse(text3);
          if (structuredFinalReply.Text.Length > 0)
          {
            list3.Add(structuredFinalReply.Text);
          }
          list4.AddRange(structuredFinalReply.NextActions.Except<string>(list4));
          list5.AddRange(structuredFinalReply.Artifacts.Except<string>(list5));
        }
        list.Add(new AgentTranscriptTurn(null, list2, rolloutTurn.ProgressLines.ToList<string>(), list3, list4, list5, "completed", null, rolloutTurn.StartedAt, rolloutTurn.FinishedAt));
      }
      return list;
    }

    // Token: 0x060001F9 RID: 505 RVA: 0x000DD53C File Offset: 0x000DD53C
    public Task ArchiveThreadAsync(string threadId, CancellationToken ct = default(CancellationToken))
    {
      if (this.session.IsThreadActive(threadId))
      {
        throw new InvalidOperationException("Stop the agent before archiving its conversation.");
      }
      this.HideThreadFromHistory(threadId);
      ClickyInfo clickyInfo = this.clickys.ForThread(threadId);
      if (clickyInfo != null && clickyInfo.ThreadId == threadId)
      {
        this.clickys.Archive(clickyInfo.Slug);
      }
      IEnumerable<AgentTask> values = this.tasks.Values;
      Func<AgentTask, bool> <>9__0;
      Func<AgentTask, bool> func;
      if ((func = <>9__0) == null)
      {
        func = (<>9__0 = (AgentTask t) => t.ThreadId == threadId);
      }
      foreach (AgentTask agentTask in values.Where<AgentTask>(func).ToList<AgentTask>())
      {
        AgentTask agentTask2;
        this.tasks.TryRemove(agentTask.Id, out agentTask2);
      }
      this.RaiseTasksChanged();
      return Task.CompletedTask;
    }

    // Token: 0x060001FA RID: 506 RVA: 0x000DD648 File Offset: 0x000DD648
    public bool HideThreadFromHistory(string threadId)
    {
      return !string.IsNullOrWhiteSpace(threadId) && this.clickys.HideThread(threadId);
    }

    // Token: 0x060001FB RID: 507 RVA: 0x000DD660 File Offset: 0x000DD660
    public bool IsThreadHidden(string threadId)
    {
      return this.clickys.IsThreadHidden(threadId);
    }

    // Token: 0x060001FC RID: 508 RVA: 0x000DD670 File Offset: 0x000DD670
    public void ArchiveClicky(string slug)
    {
      if (this.tasks.Values.Any<AgentTask>((AgentTask t) => t.ClickySlug == slug && AgentService.Lock<bool>(t, () => t.IsActive)))
      {
        throw new InvalidOperationException("Stop the agent before archiving it.");
      }
      this.clickys.Archive(slug);
      IEnumerable<AgentTask> values = this.tasks.Values;
      Func<AgentTask, bool> <>9__2;
      Func<AgentTask, bool> func;
      if ((func = <>9__2) == null)
      {
        func = (<>9__2 = (AgentTask t) => t.ClickySlug == slug);
      }
      foreach (AgentTask agentTask in values.Where<AgentTask>(func).ToList<AgentTask>())
      {
        AgentTask agentTask2;
        this.tasks.TryRemove(agentTask.Id, out agentTask2);
      }
      this.RaiseTasksChanged();
    }

    // Token: 0x060001FD RID: 509 RVA: 0x000DD74C File Offset: 0x000DD74C
    public string OpenThread(string threadId)
    {
      AgentTask agentTask = this.tasks.Values.FirstOrDefault<AgentTask>((AgentTask t) => t.ThreadId == threadId);
      if (agentTask != null)
      {
        return agentTask.Id;
      }
      ClickyInfo clickyInfo = this.clickys.ForThread(threadId);
      ClickyJob clickyJob = ((clickyInfo == null) ? this.Jobs.JobForThread(threadId) : null);
      AgentTask agentTask2 = new AgentTask
      {
        ThreadId = threadId,
        Status = AgentTaskStatus.Finished,
        ProgressSummary = ""
      };
      AgentTask agentTask3 = agentTask2;
      string text;
      if ((text = this.clickys.TitleForThread(threadId)) == null && (text = ((clickyInfo != null) ? clickyInfo.Name : null)) == null)
      {
        text = ((clickyJob != null) ? clickyJob.Title : null) ?? agentTask2.Title;
      }
      agentTask3.Title = text;
      agentTask2.ClickySlug = ((clickyInfo != null) ? clickyInfo.Slug : null);
      agentTask2.ClickyName = ((clickyInfo != null) ? clickyInfo.Name : null);
      agentTask2.JobId = ((clickyJob != null) ? clickyJob.Id : null);
      agentTask2.WorkspacePath = ((clickyInfo != null) ? this.clickys.WorkspacePath(clickyInfo.Slug) : ((clickyJob != null) ? clickyJob.FolderPath : null));
      this.tasks[agentTask2.Id] = agentTask2;
      this.RaiseTasksChanged();
      return agentTask2.Id;
    }

    // Token: 0x060001FE RID: 510 RVA: 0x000DD8A4 File Offset: 0x000DD8A4
    private Task PrepareRuntimeLockedAsync(CancellationToken ct)
    {
      AgentService.<PrepareRuntimeLockedAsync>d__235 <PrepareRuntimeLockedAsync>d__;
      <PrepareRuntimeLockedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
      <PrepareRuntimeLockedAsync>d__.<>4__this = this;
      <PrepareRuntimeLockedAsync>d__.ct = ct;
      <PrepareRuntimeLockedAsync>d__.<>1__state = -1;
      <PrepareRuntimeLockedAsync>d__.<>t__builder.Start<AgentService.<PrepareRuntimeLockedAsync>d__235>(ref <PrepareRuntimeLockedAsync>d__);
      return <PrepareRuntimeLockedAsync>d__.<>t__builder.Task;
    }

    // Token: 0x060001FF RID: 511 RVA: 0x000DD8F0 File Offset: 0x000DD8F0
    private Task RunLaunchAsync(AgentTask task, AgentLaunchRequest request, [Nullable(2)] ClickyInfo clicky, [Nullable(2)] ClickyJob job, bool isNewJob)
    {
      AgentService.<RunLaunchAsync>d__236 <RunLaunchAsync>d__;
      <RunLaunchAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
      <RunLaunchAsync>d__.<>4__this = this;
      <RunLaunchAsync>d__.task = task;
      <RunLaunchAsync>d__.request = request;
      <RunLaunchAsync>d__.clicky = clicky;
      <RunLaunchAsync>d__.job = job;
      <RunLaunchAsync>d__.isNewJob = isNewJob;
      <RunLaunchAsync>d__.<>1__state = -1;
      <RunLaunchAsync>d__.<>t__builder.Start<AgentService.<RunLaunchAsync>d__236>(ref <RunLaunchAsync>d__);
      return <RunLaunchAsync>d__.<>t__builder.Task;
    }

    // Token: 0x06000200 RID: 512 RVA: 0x000DD960 File Offset: 0x000DD960
    private Task StartFollowUpTurnAsync(AgentTask task, string text, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<byte[]> images, string source, string usageContext, [Nullable(2)] AgentTurnLease preauthorizedLease, CancellationToken ct, [Nullable(2)] string contextOverride = null, [Nullable(2)] string requestedThreadId = null, bool startsNewJobConversation = false, [TupleElementNames(new string[] { "User", "Assistant" })] [Nullable(new byte[] { 2, 0, 1, 1 })] IReadOnlyList<ValueTuple<string, string>> priorTurns = null)
    {
      AgentService.<>c__DisplayClass237_0 CS$<>8__locals1 = new AgentService.<>c__DisplayClass237_0();
      CS$<>8__locals1.task = task;
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.contextOverride = contextOverride;
      CS$<>8__locals1.text = text;
      CS$<>8__locals1.images = images;
      CS$<>8__locals1.source = source;
      CS$<>8__locals1.usageContext = usageContext;
      CS$<>8__locals1.priorTurns = priorTurns;
      CS$<>8__locals1.preauthorizedLease = preauthorizedLease;
      object obj = CS$<>8__locals1.task.Gate;
      lock (obj)
      {
        if (CS$<>8__locals1.task.IsActive)
        {
          throw new InvalidOperationException("This job is still working on its current task.");
        }
        AgentService.<>c__DisplayClass237_0 CS$<>8__locals2 = CS$<>8__locals1;
        string clickySlug = CS$<>8__locals1.task.ClickySlug;
        CS$<>8__locals2.clicky = ((clickySlug != null) ? this.clickys.Get(clickySlug) : null);
        AgentService.<>c__DisplayClass237_0 CS$<>8__locals3 = CS$<>8__locals1;
        string jobId = CS$<>8__locals1.task.JobId;
        CS$<>8__locals3.job = ((jobId != null) ? this.Jobs.Job(jobId) : null);
      }
      string text2 = AgentService.Lock<string>(CS$<>8__locals1.task, () => CS$<>8__locals1.task.ThreadId);
      if (text2 != null)
      {
        ClickyPendingAgentRequestStore.Remove(text2);
        ClickyAnswer clickyAnswer;
        this.resultCards.TryRemove(text2, out clickyAnswer);
      }
      CS$<>8__locals1.jobContext = "";
      string text3 = null;
      if (CS$<>8__locals1.job != null)
      {
        ValueTuple<string, string> valueTuple = this.PlanJobTurn(CS$<>8__locals1.job, CS$<>8__locals1.task, CS$<>8__locals1.text.Trim(), CS$<>8__locals1.source, CS$<>8__locals1.preauthorizedLease != null, requestedThreadId ?? AgentService.Lock<string>(CS$<>8__locals1.task, () => CS$<>8__locals1.task.ThreadId), startsNewJobConversation);
        text3 = valueTuple.Item1;
        CS$<>8__locals1.jobContext = valueTuple.Item2;
      }
      obj = CS$<>8__locals1.task.Gate;
      lock (obj)
      {
        CS$<>8__locals1.task.ResetForNewTurn(CS$<>8__locals1.text);
        CS$<>8__locals1.task.LaunchSource = CS$<>8__locals1.source;
        CS$<>8__locals1.task.LaunchTimestamp = Stopwatch.GetTimestamp();
        CS$<>8__locals1.task.TurnCts = CancellationTokenSource.CreateLinkedTokenSource(this.lifetime.Token);
        if (CS$<>8__locals1.job != null)
        {
          CS$<>8__locals1.task.ThreadId = text3;
          CS$<>8__locals1.task.WorkspacePath = CS$<>8__locals1.job.FolderPath;
        }
      }
      CS$<>8__locals1.leftThread = ((CS$<>8__locals1.clicky != null) ? this.LeaveOldHarnessThread(CS$<>8__locals1.task, CS$<>8__locals1.source, CS$<>8__locals1.preauthorizedLease != null) : null);
      this.RaiseTasksChanged();
      this.Raise(CS$<>8__locals1.task, AgentTaskEventKind.StatusChanged, "Starting");
      if (CS$<>8__locals1.source != "extra_effort")
      {
        obj = CS$<>8__locals1.task.Gate;
        string threadId;
        lock (obj)
        {
          threadId = CS$<>8__locals1.task.ThreadId;
        }
        if (threadId == null)
        {
          obj = CS$<>8__locals1.task.Gate;
          lock (obj)
          {
            CS$<>8__locals1.task.PendingInitialMessageLog = CS$<>8__locals1.text;
            goto IL_02F3;
          }
        }
        this.LogAgentMessage(CS$<>8__locals1.task, threadId, "user", CS$<>8__locals1.text, true);
      }
      IL_02F3:
      CS$<>8__locals1.turnCt = CS$<>8__locals1.task.TurnCts.Token;
      Task.Run(delegate
      {
        AgentService.<>c__DisplayClass237_0.<<StartFollowUpTurnAsync>b__1>d <<StartFollowUpTurnAsync>b__1>d;
        <<StartFollowUpTurnAsync>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<StartFollowUpTurnAsync>b__1>d.<>4__this = CS$<>8__locals1;
        <<StartFollowUpTurnAsync>b__1>d.<>1__state = -1;
        <<StartFollowUpTurnAsync>b__1>d.<>t__builder.Start<AgentService.<>c__DisplayClass237_0.<<StartFollowUpTurnAsync>b__1>d>(ref <<StartFollowUpTurnAsync>b__1>d);
        return <<StartFollowUpTurnAsync>b__1>d.<>t__builder.Task;
      }, CancellationToken.None);
      return Task.CompletedTask;
    }

    // Token: 0x06000201 RID: 513 RVA: 0x000DDCC8 File Offset: 0x000DDCC8
    private async Task RunTurnAsync(AgentTask task, AgentService.TurnSpec spec, CancellationToken ct)
    {
      if (!this.slots.Wait(0))
      {
        this.SetProgress(task, "Waiting for another agent to finish…");
        await this.slots.WaitAsync(ct).ConfigureAwait(false);
      }
      object obj = task.Gate;
      lock (obj)
      {
        task.HoldsSlot = true;
      }
      try
      {
        string clientTurnId = Guid.NewGuid().ToString("D").ToUpperInvariant();
        AgentTurnLease lease = spec.PreauthorizedLease;
        obj = task.Gate;
        string threadId;
        lock (obj)
        {
          threadId = task.ThreadId;
        }
        if (lease == null)
        {
          string text = await this.RequireSupabaseTokenAsync(ct).ConfigureAwait(false);
          try
          {
            lease = await this.api.RecordAgentLaunchAsync(text, threadId, clientTurnId, task.NumericId, threadId != null, spec.LaunchSource, spec.AutoApproveExtraUsage || this.options.AutoApproveExtraUsage, spec.UsageContext, ct).ConfigureAwait(false);
          }
          catch (AgentBackendException ex) when (ex.IsPaywall)
          {
            obj = task.Gate;
            bool flag = false;
            bool flag2;
            try
            {
              Monitor.Enter(obj, ref flag);
              flag2 = task.IsRoutineRun || spec.LaunchSource == "routine";
            }
            finally
            {
              int num;
              if (num < 0 && flag)
              {
                Monitor.Exit(obj);
              }
            }
            this.RaisePaywall(ex, !flag2);
            throw new AgentPaywallException(new CompanionError("agent_paywall", "Oops, you're out of tokens! Beef up HeyClicky to continue.", ex.BodyJson));
          }
          obj = task.Gate;
          lock (obj)
          {
            task.LeaseRecordedAt = new DateTimeOffset?(DateTimeOffset.Now);
          }
        }
        obj = task.Gate;
        lock (obj)
        {
          task.Lease = lease;
        }
        ct.ThrowIfCancellationRequested();
        obj = task.Gate;
        string workspace;
        lock (obj)
        {
          workspace = task.WorkspacePath;
        }
        string cwd = workspace ?? this.options.ProjectsRoot;
        bool flag3 = threadId == null;
        ValueTuple<AgentHarness, AgentHarnessPlace> harness = AgentHarnessThreads.Choice(threadId);
        bool flag4 = threadId != null && AgentHarnessPreferences.RuntimeChoice(threadId) == null && AgentHarnessThreads.TakenOverBy(threadId) == null;
        if (threadId == null)
        {
          threadId = AgentHarnessPreferences.ThreadIdPrefix(harness.Item1, harness.Item2) + Guid.NewGuid().ToString("D").ToLowerInvariant();
          obj = task.Gate;
          lock (obj)
          {
            task.ThreadId = threadId;
          }
          this.BindNewThread(task, threadId);
          string deliveredModelInstructions = this.home.DeliveredModelInstructions;
          if (deliveredModelInstructions != null)
          {
            this.clickys.MarkThreadStartedWithInstructions(threadId, deliveredModelInstructions);
          }
          this.clickys.SetThreadTitle(threadId, task.Title);
          this.RaiseTasksChanged();
        }
        else if (flag4)
        {
          Log.Info("Harness", "[harness] " + harness.Item1.DisplayName() + " takes over conversation " + threadId);
          string deliveredModelInstructions2 = this.home.DeliveredModelInstructions;
          if (deliveredModelInstructions2 != null)
          {
            this.clickys.MarkThreadStartedWithInstructions(threadId, deliveredModelInstructions2);
          }
        }
        bool startsHarnessConversation = flag3 || flag4;
        string launchContext = (flag4 ? (this.TakeoverContext(threadId, task) + "\n" + spec.LaunchContext) : spec.LaunchContext);
        this.FlushPendingInitialMessageLog(task, threadId);
        List<string> list = await this.WriteTaskImagesAsync(task, spec.Images, "screenshot", ct).ConfigureAwait(false);
        obj = task.Gate;
        string jobId;
        lock (obj)
        {
          jobId = task.JobId;
        }
        bool flag5 = ClickyJobsPreferences.IsEnabled && jobId != null;
        if (flag5)
        {
          string text2 = this.LiveStepsKey(task);
          if (text2 != null)
          {
            this.LiveSteps.Clear(text2);
          }
        }
        string text3 = (flag5 ? this.LearnedHowTos.PromptSection(spec.Prompt) : "");
        bool flag6 = StandingContextLedger.NeedsCarrying(StandingContextLedger.Section.LearnedHowTos, text3, startsHarnessConversation ? null : threadId);
        string text4 = launchContext;
        string prompt = spec.Prompt;
        IReadOnlyList<string> readOnlyList = spec.AttachedFiles ?? Array.Empty<string>();
        string text5 = workspace;
        string text6 = spec.UserMemory;
        string clickySlug = task.ClickySlug;
        TurnPromptBuilder.Input input = new TurnPromptBuilder.Input(text4, prompt, list, readOnlyList, text5, text6, (clickySlug != null) ? this.routines.PromptLines(clickySlug) : ((jobId == null) ? Array.Empty<string>() : this.routines.PromptLinesForJob(jobId)), spec.IsGeneratedBrief, spec.LaunchSource, startsHarnessConversation ? null : this.clickys.InstructionsToCarry(threadId, this.home.DeliveredModelInstructions), spec.PriorTurns);
        input.IsJob = flag5;
        input.UserProfileFilePath = (flag5 ? ClickyHelperFiles.WriteUserProfile(spec.UserMemory) : null);
        input.LearnedHowTosSection = (flag6 ? text3 : "");
        input.LiveNoteSection = (flag5 ? "\nIMPORTANT — THIS JOB'S PLAN\nYour plan tool (update_plan) is the job's progress bar: the user watches it while you work. First thing, in the same response as your first searches, call it with 3 to 5 steps of a few words each (under 40 characters: the card has one short line for each). The steps are the stages of the work in the order they will happen, each one a stage the user would recognize (\"Find where each is sold\", \"Check stock in San Francisco\", \"Compare the prices\", \"Build the PDF\"), never your own housekeeping (reading notes, files or memory about them, setting up, saving). Things you look into side by side go in one step together, so every step finishes before the next one starts. Keep exactly one step in progress; when it is done, mark it completed and the next one in progress. Never send an update on its own: put it in the same response as your next search, read or command. A long step (building a file, checking its pages) stays in progress while you're on it. Don't tick the last step: HeyClicky ticks it when your answer arrives. On a later message, plan just that message's work. There is no note to write: the answer goes in your final message." : this.ClickyLiveNoteSection(task, spec.Prompt, spec.LaunchSource, spec.IsGeneratedBrief, threadId, false));
        Func<bool> isClickyDriveAvailable = this.IsClickyDriveAvailable;
        input.IsClickyDriveAvailable = isClickyDriveAvailable != null && isClickyDriveAvailable();
        string text7 = TurnPromptBuilder.Build(input);
        if (flag6)
        {
          Dictionary<StandingContextLedger.Section, string> dictionary = new Dictionary<StandingContextLedger.Section, string>();
          dictionary[StandingContextLedger.Section.LearnedHowTos] = text3;
          StandingContextLedger.RecordCarried(dictionary, threadId);
        }
        ValueTuple<string, string>? valueTuple;
        if (this.tokens.AllowsDeveloperModelSelection)
        {
          string clickySlug2 = task.ClickySlug;
          if (clickySlug2 != null)
          {
            Func<string, ValueTuple<string, string>?> developerModelSelection = this.DeveloperModelSelection;
            valueTuple = ((developerModelSelection != null) ? developerModelSelection(clickySlug2) : null);
            goto IL_0910;
          }
        }
        valueTuple = null;
        IL_0910:
        ValueTuple<string, string>? valueTuple2 = valueTuple;
        obj = task.Gate;
        lock (obj)
        {
          task.DeveloperModel = valueTuple2;
        }
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
        if (valueTuple2 != null)
        {
          ValueTuple<string, string> valueOrDefault = valueTuple2.GetValueOrDefault();
          string text8 = "Agents";
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 3);
          defaultInterpolatedStringHandler.AppendLiteral("task ");
          defaultInterpolatedStringHandler.AppendFormatted(task.Id);
          defaultInterpolatedStringHandler.AppendLiteral(": developer model ");
          defaultInterpolatedStringHandler.AppendFormatted(valueOrDefault.Item1);
          defaultInterpolatedStringHandler.AppendLiteral(" / ");
          defaultInterpolatedStringHandler.AppendFormatted(valueOrDefault.Item2);
          Log.Info(text8, defaultInterpolatedStringHandler.ToStringAndClear());
        }
        TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
        obj = task.Gate;
        lock (obj)
        {
          task.TurnCompletion = taskCompletionSource;
          task.Status = AgentTaskStatus.Running;
          task.ProgressSummary = "Sending your message…";
        }
        this.Raise(task, AgentTaskEventKind.StatusChanged, "Running");
        this.RaiseTasksChanged();
        CodexSession codexSession = this.session;
        ValueTuple<AgentHarness, AgentHarnessPlace> valueTuple3 = harness;
        string text9 = threadId;
        bool flag7 = startsHarnessConversation;
        string text10 = cwd;
        JsonObject jsonObject = AgentService.LeaseMetadata(lease, task, threadId);
        IReadOnlyDictionary<string, string> readOnlyDictionary;
        if (jsonObject == null)
        {
          readOnlyDictionary = null;
        }
        else
        {
          readOnlyDictionary = jsonObject.ToDictionary<KeyValuePair<string, JsonNode>, string, string>((KeyValuePair<string, JsonNode> kv) => kv.Key, delegate(KeyValuePair<string, JsonNode> kv)
          {
            JsonNode value = kv.Value;
            return ((value != null) ? value.ToString() : null) ?? "";
          });
        }
        codexSession.StartHarnessTurn(valueTuple3, task, text9, flag7, text7, list, text10, readOnlyDictionary, new Func<string, AgentTask, string, CancellationToken, Task<AgentHarnessTurnContext>>(this.HarnessTurnContextAsync), ct);
        obj = task.Gate;
        lock (obj)
        {
          task.TurnStartedTimestamp = Stopwatch.GetTimestamp();
          this.launchToTurnStartedMs = (long)Stopwatch.GetElapsedTime(task.LaunchTimestamp).TotalMilliseconds;
        }
        string text11 = "Agents";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 3);
        defaultInterpolatedStringHandler.AppendLiteral("task ");
        defaultInterpolatedStringHandler.AppendFormatted(task.Id);
        defaultInterpolatedStringHandler.AppendLiteral(" turn started on thread ");
        defaultInterpolatedStringHandler.AppendFormatted(threadId);
        defaultInterpolatedStringHandler.AppendLiteral(" (");
        defaultInterpolatedStringHandler.AppendFormatted<long>(this.launchToTurnStartedMs);
        defaultInterpolatedStringHandler.AppendLiteral(" ms after launch)");
        Log.Info(text11, defaultInterpolatedStringHandler.ToStringAndClear());
        using (CancellationTokenSource leasePoll = ((lease == null) ? null : CancellationTokenSource.CreateLinkedTokenSource(ct)))
        {
          if (leasePoll != null)
          {
            this.PollLeaseAsync(task, lease, leasePoll.Token);
          }
          await taskCompletionSource.Task.ConfigureAwait(false);
          CancellationTokenSource cancellationTokenSource = leasePoll;
          if (cancellationTokenSource != null)
          {
            cancellationTokenSource.Cancel();
          }
        }
        clientTurnId = null;
        lease = null;
        threadId = null;
        workspace = null;
        cwd = null;
        launchContext = null;
        CancellationTokenSource leasePoll = null;
      }
      finally
      {
        obj = task.Gate;
        bool flag = false;
        bool holdsSlot;
        try
        {
          Monitor.Enter(obj, ref flag);
          holdsSlot = task.HoldsSlot;
          task.HoldsSlot = false;
        }
        finally
        {
          int num;
          if (num < 0 && flag)
          {
            Monitor.Exit(obj);
          }
        }
        if (holdsSlot)
        {
          this.slots.Release();
        }
      }
    }

    // Token: 0x06000202 RID: 514 RVA: 0x000DDD24 File Offset: 0x000DDD24
    private void BindNewThread(AgentTask task, string threadId)
    {
      object gate = task.Gate;
      string clickySlug;
      string jobId;
      lock (gate)
      {
        clickySlug = task.ClickySlug;
        jobId = task.JobId;
      }
      if (clickySlug != null)
      {
        this.clickys.BindThread(clickySlug, threadId);
        return;
      }
      if (jobId != null)
      {
        this.Jobs.RecordThreadId(threadId, jobId);
      }
    }

    // Token: 0x06000203 RID: 515 RVA: 0x000DDD90 File Offset: 0x000DDD90
    private async Task PollLeaseAsync(AgentTask task, AgentTurnLease lease, CancellationToken ct)
    {
      while (!ct.IsCancellationRequested)
      {
        try
        {
          await Task.Delay(3000, ct).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
          break;
        }
        try
        {
          string text = await this.supabaseAccessToken(ct).ConfigureAwait(false);
          if (text != null)
          {
            AgentTurnLease agentTurnLease = await this.api.LeaseStatusAsync(text, lease.LeaseId, ct).ConfigureAwait(false);
            if (agentTurnLease != null)
            {
              object obj = task.Gate;
              lock (obj)
              {
                task.Lease = agentTurnLease;
              }
              if (agentTurnLease.RequiresExtraEffort)
              {
                obj = task.Gate;
                lock (obj)
                {
                  task.RequiresExtraUsageApproval = true;
                }
                this.session.Interrupt(task, TurnOutcome.ExtraEffortRequired, "This task requires extra effort");
                break;
              }
            }
          }
        }
        catch (OperationCanceledException)
        {
          break;
        }
        catch (Exception ex)
        {
          Log.Warn("Agents", "lease status poll: " + ex.Message);
        }
      }
    }

    // Token: 0x06000204 RID: 516 RVA: 0x000DDDEC File Offset: 0x000DDDEC
    private void OnTurnEnded(AgentTask task, TurnOutcome outcome, [Nullable(2)] string message)
    {
      this.NotifyBrowserTurnEnded(task, outcome);
      object obj = task.Gate;
      string text;
      AgentTaskEventKind agentTaskEventKind;
      string text4;
      string text5;
      string clickySlug;
      string jobId;
      string text6;
      lock (obj)
      {
        text = task.FinishStreamingCommentary(task.CurrentNarrationItemId, null);
        switch (outcome)
        {
        case TurnOutcome.Completed:
        {
          StructuredFinalReply structuredFinalReply = StructuredFinalReply.Parse(task.ResponseAccumulator.ToString().Trim());
          task.FinalResponseMarkdown = ((structuredFinalReply.Text.Length > 0) ? structuredFinalReply.Text : null);
          task.Summary = structuredFinalReply.SummaryOrFallback;
          task.NextActions = structuredFinalReply.NextActions;
          task.DoneTitle = structuredFinalReply.DoneTitle;
          task.Artifacts = structuredFinalReply.Artifacts;
          task.Answer = structuredFinalReply.Answer;
          task.PendingUserStep = structuredFinalReply.UserStep;
          task.LearnedHowTos = structuredFinalReply.LearnedHowTos ?? Array.Empty<string>();
          Func<string, string> takeUnansweredComputerUseStep = this.TakeUnansweredComputerUseStep;
          string text2 = ((takeUnansweredComputerUseStep != null) ? takeUnansweredComputerUseStep(task.Id) : null);
          string text3 = structuredFinalReply.ComputerUseRequest ?? text2;
          if (text3 != null)
          {
            Func<string, string, bool> wasComputerUseStepRecentlyDeclined = this.WasComputerUseStepRecentlyDeclined;
            if (wasComputerUseStepRecentlyDeclined != null && wasComputerUseStepRecentlyDeclined(task.Id, text3))
            {
              text3 = null;
            }
          }
          task.PendingComputerUseRequest = text3 ?? task.PendingComputerUseRequest;
          task.Status = AgentTaskStatus.Finished;
          task.ProgressSummary = "Done";
          agentTaskEventKind = AgentTaskEventKind.Finished;
          text4 = task.Summary;
          text5 = ((task.PendingComputerUseRequest == null) ? "completed" : "");
          goto IL_02DC;
        }
        case TurnOutcome.Interrupted:
          task.Status = AgentTaskStatus.Stopped;
          task.ProgressSummary = message ?? "Stopped.";
          agentTaskEventKind = AgentTaskEventKind.Stopped;
          text4 = message;
          text5 = "interrupted";
          goto IL_02DC;
        case TurnOutcome.ExtraEffortRequired:
          task.Status = AgentTaskStatus.WaitingForUser;
          task.RequiresExtraUsageApproval = true;
          task.ProgressSummary = "Needs your OK to keep going";
          agentTaskEventKind = AgentTaskEventKind.NeedsExtraUsageApproval;
          text4 = "This task needs more effort than one agent message covers.";
          text5 = "";
          goto IL_02DC;
        }
        task.Status = AgentTaskStatus.Failed;
        task.FailureMessage = message ?? "Agent thread failed.";
        AgentTask task2 = task;
        if (task2.Error == null)
        {
          task2.Error = new CompanionError("agent_failed", task.FailureMessage, null);
        }
        task.ProgressSummary = task.FailureMessage;
        agentTaskEventKind = AgentTaskEventKind.Failed;
        text4 = task.FailureMessage;
        text5 = "failed";
        IL_02DC:
        if (task.TurnStartedTimestamp != 0L)
        {
          this.launchToFinishedMs = (long)Stopwatch.GetElapsedTime(task.LaunchTimestamp).TotalMilliseconds;
          if (task.FirstTokenTimestamp != 0L)
          {
            this.launchToFirstTokenMs = (long)((double)(task.FirstTokenTimestamp - task.LaunchTimestamp) * 1000.0 / (double)Stopwatch.Frequency);
          }
        }
        clickySlug = task.ClickySlug;
        jobId = task.JobId;
        text6 = task.Summary ?? task.FailureMessage;
      }
      string text7 = "Agents";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 3);
      defaultInterpolatedStringHandler.AppendLiteral("task ");
      defaultInterpolatedStringHandler.AppendFormatted(task.Id);
      defaultInterpolatedStringHandler.AppendLiteral(" turn ended: ");
      defaultInterpolatedStringHandler.AppendFormatted<TurnOutcome>(outcome);
      defaultInterpolatedStringHandler.AppendFormatted((message == null) ? "" : (" (" + message + ")"));
      Log.Info(text7, defaultInterpolatedStringHandler.ToStringAndClear());
      if (outcome == TurnOutcome.Failed && ComposioTurnErrors.IsListingFailure(message ?? text6))
      {
        Log.Info("Agents", "the turn couldn't list the Composio tools: the next turn fetches a fresh Composio session");
        this.InvalidateConnectedApps();
      }
      if (text != null)
      {
        this.Raise(task, AgentTaskEventKind.Commentary, text);
      }
      if (outcome == TurnOutcome.Completed)
      {
        this.AskAboutChromeIfStoppedForIt(task);
      }
      if (clickySlug != null || jobId != null)
      {
        this.routines.NoteTaskEnded(clickySlug, jobId, task.Id, text6, outcome > TurnOutcome.Completed, outcome == TurnOutcome.Interrupted, outcome == TurnOutcome.ExtraEffortRequired, false, false);
      }
      switch (outcome)
      {
      case TurnOutcome.Completed:
        this.NoteJobTaskEnded(task, ClickyJobOutcome.Done, text6);
        break;
      case TurnOutcome.Failed:
        this.NoteJobTaskEnded(task, ClickyJobOutcome.Failed, text6);
        break;
      case TurnOutcome.Interrupted:
        this.NoteJobTaskEnded(task, ClickyJobOutcome.Stopped, message);
        break;
      }
      if (outcome != TurnOutcome.ExtraEffortRequired && AgentService.Lock<bool>(task, () => task.JobId != null))
      {
        string text8 = this.LiveStepsKey(task);
        if (text8 != null)
        {
          this.LiveSteps.Clear(text8);
        }
      }
      if (outcome == TurnOutcome.Completed)
      {
        this.NoteClickyAppWorkFinished(task);
      }
      this.SyncPendingRequest(task);
      this.Raise(task, agentTaskEventKind, text4);
      if (outcome == TurnOutcome.Completed)
      {
        obj = task.Gate;
        string threadId;
        lock (obj)
        {
          threadId = task.ThreadId;
        }
        this.LogAgentMessage(task, threadId, "assistant", text6, false);
      }
      if (text5.Length > 0)
      {
        this.CompleteLeaseInBackground(task, text5);
      }
      TaskCompletionSource<string> turnCompletion = task.TurnCompletion;
      if (turnCompletion != null)
      {
        turnCompletion.TrySetResult(outcome.ToString());
      }
      this.RaiseTasksChanged();
    }

    // Token: 0x06000205 RID: 517 RVA: 0x000DE3D8 File Offset: 0x000DE3D8
    private void FailLaunch(AgentTask task, Exception ex)
    {
      bool flag = ex is OperationCanceledException;
      AgentPaywallException ex2 = ex as AgentPaywallException;
      CompanionError companionError;
      if (ex2 == null)
      {
        if (!(ex is OperationCanceledException))
        {
          AgentHarnessException ex3 = ex as AgentHarnessException;
          if (ex3 != null)
          {
            if (ex3.IsNotAvailable)
            {
              companionError = new CompanionError("agent_runtime_missing", ex3.Message, null);
              goto IL_00FE;
            }
          }
          else
          {
            AgentBackendException ex4 = ex as AgentBackendException;
            if (ex4 != null)
            {
              companionError = new CompanionError("agent_backend_error", ex4.Message, ex4.BodyJson);
              goto IL_00FE;
            }
            if (ex is HttpRequestException)
            {
              companionError = new CompanionError("agent_offline", AgentFailureDescription.UserMessage(ex), null);
              goto IL_00FE;
            }
          }
          companionError = new CompanionError("agent_launch_failed", AgentFailureDescription.UserMessage(ex), null);
        }
        else
        {
          companionError = new CompanionError("agent_cancelled", "Agent cancelled.", null);
        }
      }
      else
      {
        companionError = ex2.Error;
      }
      IL_00FE:
      CompanionError companionError2 = companionError;
      AgentPaywallException ex5 = ex as AgentPaywallException;
      if (ex5 != null)
      {
        this.InterruptForPaywall(task, ex5.Error);
        return;
      }
      if (!flag)
      {
        Log.Error("Agents", "task " + task.Id + " launch failed", ex);
      }
      this.FlushPendingInitialMessageLog(task, AgentService.Lock<string>(task, () => task.ThreadId));
      object gate = task.Gate;
      string clickySlug;
      string jobId;
      lock (gate)
      {
        if (!task.IsActive)
        {
          return;
        }
        task.Status = (flag ? AgentTaskStatus.Stopped : AgentTaskStatus.Failed);
        task.FailureMessage = (flag ? null : companionError2.Message);
        task.FailureDiagnostic = (flag ? null : AgentFailureDescription.Diagnostic(ex));
        task.Error = (flag ? null : companionError2);
        task.ProgressSummary = (flag ? "Stopped." : companionError2.Message);
        clickySlug = task.ClickySlug;
        jobId = task.JobId;
      }
      this.NoteJobTaskEnded(task, flag ? ClickyJobOutcome.Stopped : ClickyJobOutcome.Failed, companionError2.Message);
      this.Raise(task, flag ? AgentTaskEventKind.Stopped : AgentTaskEventKind.Failed, companionError2.Message);
      this.CompleteLeaseInBackground(task, flag ? "interrupted" : "failed");
      if (clickySlug != null || jobId != null)
      {
        RoutineScheduler routineScheduler = this.routines;
        string text = clickySlug;
        string text2 = jobId;
        string id = task.Id;
        string message = companionError2.Message;
        bool flag2 = flag;
        bool flag3 = ex is HttpRequestException || (ex is TaskCanceledException && ex.InnerException is TimeoutException);
        routineScheduler.NoteTaskEnded(text, text2, id, message, true, flag2, false, flag3, false);
      }
      TaskCompletionSource<string> turnCompletion = task.TurnCompletion;
      if (turnCompletion != null)
      {
        turnCompletion.TrySetResult("failed");
      }
      this.RaiseTasksChanged();
    }

    // Token: 0x06000206 RID: 518 RVA: 0x000DE6F8 File Offset: 0x000DE6F8
    private void InterruptForPaywall(AgentTask task, CompanionError error)
    {
      Log.Info("Agents", "task " + task.Id + " hit the paywall before the turn started");
      this.FlushPendingInitialMessageLog(task, AgentService.Lock<string>(task, () => task.ThreadId));
      object gate = task.Gate;
      string clickySlug;
      string jobId;
      lock (gate)
      {
        if (!task.IsActive)
        {
          return;
        }
        task.Status = AgentTaskStatus.Stopped;
        task.FailureMessage = "Oops, you're out of tokens! Beef up HeyClicky to continue.";
        task.Error = error;
        task.ProgressSummary = "Oops, you're out of tokens! Beef up HeyClicky to continue.";
        clickySlug = task.ClickySlug;
        jobId = task.JobId;
      }
      this.NoteJobTaskEnded(task, ClickyJobOutcome.Stopped, "Oops, you're out of tokens! Beef up HeyClicky to continue.");
      this.Raise(task, AgentTaskEventKind.Stopped, "Oops, you're out of tokens! Beef up HeyClicky to continue.");
      this.CompleteLeaseInBackground(task, "interrupted");
      if (clickySlug != null || jobId != null)
      {
        string text = AgentService.PaywallReason(error);
        bool flag = text == null || text == "quota_exceeded";
        bool flag2 = flag;
        this.routines.NoteTaskEnded(clickySlug, jobId, task.Id, "Oops, you're out of tokens! Beef up HeyClicky to continue.", true, true, AgentService.PaywallReason(error) != "payment_problem", false, flag2);
      }
      TaskCompletionSource<string> turnCompletion = task.TurnCompletion;
      if (turnCompletion != null)
      {
        turnCompletion.TrySetResult("failed");
      }
      this.RaiseTasksChanged();
    }

    // Token: 0x06000207 RID: 519 RVA: 0x000DE8A8 File Offset: 0x000DE8A8
    [return: Nullable(2)]
    private static string PaywallReason(CompanionError error)
    {
      JsonElement? paywall = error.Paywall;
      if (paywall != null)
      {
        JsonElement valueOrDefault = paywall.GetValueOrDefault();
        JsonElement jsonElement;
        if (valueOrDefault.ValueKind == JsonValueKind.Object && valueOrDefault.TryGetProperty("reason", out jsonElement) && jsonElement.ValueKind == JsonValueKind.String)
        {
          return jsonElement.GetString();
        }
      }
      return null;
    }

    // Token: 0x06000208 RID: 520 RVA: 0x000DE8FC File Offset: 0x000DE8FC
    private void RaisePaywall(AgentBackendException ex, bool presents)
    {
      try
      {
        EventHandler<AgentPaywallEventArgs> paywallReached = this.PaywallReached;
        if (paywallReached != null)
        {
          paywallReached(this, new AgentPaywallEventArgs(ex.StatusCode, ex.Body, presents));
        }
      }
      catch (Exception ex2)
      {
        Log.Error("Agents", "PaywallReached handler threw", ex2);
      }
    }

    // Token: 0x06000209 RID: 521 RVA: 0x000DE954 File Offset: 0x000DE954
    private void LogAgentMessage(AgentTask task, [Nullable(2)] string threadId, string role, [Nullable(2)] string content, bool isFollowUp)
    {
      AgentService.<>c__DisplayClass248_0 CS$<>8__locals1 = new AgentService.<>c__DisplayClass248_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.threadId = threadId;
      CS$<>8__locals1.role = role;
      CS$<>8__locals1.isFollowUp = isFollowUp;
      CS$<>8__locals1.trimmed = ((content != null) ? content.Trim() : null);
      if (string.IsNullOrEmpty(CS$<>8__locals1.trimmed))
      {
        return;
      }
      object gate = task.Gate;
      lock (gate)
      {
        CS$<>8__locals1.isDemo = task.IsDemo;
        CS$<>8__locals1.suggestionId = task.ProactiveSuggestionId;
      }
      Task.Run(delegate
      {
        AgentService.<>c__DisplayClass248_0.<<LogAgentMessage>b__0>d <<LogAgentMessage>b__0>d;
        <<LogAgentMessage>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<LogAgentMessage>b__0>d.<>4__this = CS$<>8__locals1;
        <<LogAgentMessage>b__0>d.<>1__state = -1;
        <<LogAgentMessage>b__0>d.<>t__builder.Start<AgentService.<>c__DisplayClass248_0.<<LogAgentMessage>b__0>d>(ref <<LogAgentMessage>b__0>d);
        return <<LogAgentMessage>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x0600020A RID: 522 RVA: 0x000DEA00 File Offset: 0x000DEA00
    private void FlushPendingInitialMessageLog(AgentTask task, [Nullable(2)] string threadId)
    {
      object gate = task.Gate;
      string pendingInitialMessageLog;
      lock (gate)
      {
        pendingInitialMessageLog = task.PendingInitialMessageLog;
        task.PendingInitialMessageLog = null;
      }
      if (pendingInitialMessageLog != null)
      {
        this.LogAgentMessage(task, threadId, "user", pendingInitialMessageLog, false);
      }
    }

    // Token: 0x0600020B RID: 523 RVA: 0x000DEA5C File Offset: 0x000DEA5C
    private void CompleteLeaseInBackground(AgentTask task, string status)
    {
      AgentService.<>c__DisplayClass250_0 CS$<>8__locals1 = new AgentService.<>c__DisplayClass250_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.status = status;
      object gate = task.Gate;
      lock (gate)
      {
        CS$<>8__locals1.lease = task.Lease;
        CS$<>8__locals1.threadId = task.ThreadId;
      }
      if (CS$<>8__locals1.lease == null)
      {
        return;
      }
      Task.Run(delegate
      {
        AgentService.<>c__DisplayClass250_0.<<CompleteLeaseInBackground>b__0>d <<CompleteLeaseInBackground>b__0>d;
        <<CompleteLeaseInBackground>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<CompleteLeaseInBackground>b__0>d.<>4__this = CS$<>8__locals1;
        <<CompleteLeaseInBackground>b__0>d.<>1__state = -1;
        <<CompleteLeaseInBackground>b__0>d.<>t__builder.Start<AgentService.<>c__DisplayClass250_0.<<CompleteLeaseInBackground>b__0>d>(ref <<CompleteLeaseInBackground>b__0>d);
        return <<CompleteLeaseInBackground>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x0600020C RID: 524 RVA: 0x000DEAE0 File Offset: 0x000DEAE0
    private async Task<string> TitleAsync(string prompt, CancellationToken ct)
    {
      try
      {
        string text = await this.supabaseAccessToken(ct).ConfigureAwait(false);
        bool flag = text != null;
        string t;
        if (flag)
        {
          t = await this.api.ThreadTitleAsync(text, (prompt.Length > 2000) ? prompt.Substring(0, 2000) : prompt, ct, null, null).ConfigureAwait(false);
          flag = t != null;
        }
        if (flag)
        {
          return t;
        }
        t = null;
      }
      catch (Exception ex) when (!(ex is OperationCanceledException) || !ct.IsCancellationRequested)
      {
        Log.Warn("Agents", "thread title failed: " + ex.Message);
      }
      string text2 = prompt.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault<string>();
      char[] array;
      if (text2 == null)
      {
        array = null;
      }
      else
      {
        Func<char, bool> func;
        if ((func = AgentService.<>O.<0>__IsLetterOrDigit) == null)
        {
          func = (AgentService.<>O.<0>__IsLetterOrDigit = new Func<char, bool>(char.IsLetterOrDigit));
        }
        array = text2.Where<char>(func).ToArray<char>();
      }
      string text3 = new string(array ?? Array.Empty<char>());
      string text5;
      if (text3.Length != 0)
      {
        char c = char.ToUpperInvariant(text3[0]);
        ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c);
        string text4 = text3;
        text5 = readOnlySpan + text4.Substring(1, text4.Length - 1).ToLowerInvariant() + " Buddy";
      }
      else
      {
        text5 = "Clicky Buddy";
      }
      return text5;
    }

    // Token: 0x0600020D RID: 525 RVA: 0x000DEB34 File Offset: 0x000DEB34
    [return: Nullable(new byte[] { 1, 2 })]
    private async Task<string> FetchMemoryAsync(CancellationToken ct)
    {
      Func<string> currentUserId = this.CurrentUserId;
      string userId = ((currentUserId != null) ? currentUserId() : null);
      string text2;
      try
      {
        string text = await this.supabaseAccessToken(ct).ConfigureAwait(false);
        if (text == null)
        {
          text2 = this.userMemory.Resolve(userId, UserMemoryFetchStatus.Failed, null);
        }
        else
        {
          object obj = await this.api.UserMemoryAsync(text, ct).ConfigureAwait(false);
          UserMemoryFetchStatus item = obj.Item1;
          string item2 = obj.Item2;
          text2 = this.userMemory.Resolve(userId, item, item2);
        }
      }
      catch (Exception obj2) when (!ct.IsCancellationRequested)
      {
        text2 = this.userMemory.Resolve(userId, UserMemoryFetchStatus.Failed, null);
      }
      catch (Exception)
      {
        text2 = null;
      }
      return text2;
    }

    // Token: 0x0600020E RID: 526 RVA: 0x000DEB80 File Offset: 0x000DEB80
    private async Task<string> RequireSupabaseTokenAsync(CancellationToken ct)
    {
      object obj = await this.supabaseAccessToken(ct).ConfigureAwait(false);
      if (obj == null)
      {
        throw new InvalidOperationException("Sign in to HeyClicky to use agents.");
      }
      return obj;
    }

    // Token: 0x0600020F RID: 527 RVA: 0x000DEBCC File Offset: 0x000DEBCC
    private static string OneLinerFrom(string prompt)
    {
      string text = string.Join(' ', prompt.Split(null, StringSplitOptions.RemoveEmptyEntries));
      int num = text.IndexOfAny(new char[] { '.', '?', '!' });
      if (num > 0)
      {
        text = text.Substring(0, num);
      }
      if (text.Length <= 120)
      {
        return text;
      }
      return text.Substring(0, 120);
    }

    // Token: 0x06000210 RID: 528 RVA: 0x000DEC24 File Offset: 0x000DEC24
    [return: Nullable(2)]
    private static JsonObject LeaseMetadata([Nullable(2)] AgentTurnLease lease, AgentTask task, string threadId)
    {
      object gate = task.Gate;
      ValueTuple<string, string>? developerModel;
      lock (gate)
      {
        developerModel = task.DeveloperModel;
      }
      if (lease == null && developerModel == null)
      {
        return null;
      }
      JsonObject jsonObject = new JsonObject(null);
      if (developerModel != null)
      {
        ValueTuple<string, string> valueOrDefault = developerModel.GetValueOrDefault();
        jsonObject["clicky_developer_model"] = valueOrDefault.Item1;
        jsonObject["clicky_developer_reasoning_effort"] = valueOrDefault.Item2;
      }
      if (lease == null)
      {
        return jsonObject;
      }
      jsonObject["clicky_agent_turn_lease_id"] = lease.LeaseId;
      jsonObject["clicky_agent_turn_id"] = lease.TurnId;
      jsonObject["clicky_agent_thread_id"] = threadId;
      jsonObject["clicky_agent_task_id"] = task.NumericId.ToString("D").ToUpperInvariant();
      return jsonObject;
    }

    // Token: 0x06000211 RID: 529 RVA: 0x000DED30 File Offset: 0x000DED30
    private static string PreviewText(string raw)
    {
      string text = raw.Trim();
      int num = text.IndexOf("User transcript:", StringComparison.OrdinalIgnoreCase);
      if (num >= 0)
      {
        string text2 = text;
        int num2 = num + "User transcript:".Length;
        string text3 = (from l in text2.Substring(num2, text2.Length - num2).Split('\n', StringSplitOptions.None)
          select l.Trim()).FirstOrDefault<string>((string l) => l.Length > 0);
        if (text3 != null)
        {
          return text3;
        }
      }
      if (text.Length != 0)
      {
        return text.Replace('\n', ' ');
      }
      return "Untitled agent thread";
    }

    // Token: 0x06000212 RID: 530 RVA: 0x000DEDE4 File Offset: 0x000DEDE4
    private static string DisplayUserText(string raw)
    {
      int num = raw.LastIndexOf("User transcript:", StringComparison.Ordinal);
      string text;
      if (num < 0)
      {
        text = raw;
      }
      else
      {
        int num2 = num + "User transcript:".Length;
        text = raw.Substring(num2, raw.Length - num2);
      }
      string text2 = text;
      int num3 = text2.IndexOf("</clicky_agent_turn_lease_metadata>", StringComparison.Ordinal);
      if (num3 >= 0)
      {
        string text3 = text2;
        int num2 = num3 + "</clicky_agent_turn_lease_metadata>".Length;
        text2 = text3.Substring(num2, text3.Length - num2);
      }
      text2 = text2.Trim();
      if (num >= 0 && !text2.Contains("\nAttached files:\n", StringComparison.Ordinal))
      {
        List<string> list = (from p in (from l in raw.Substring(0, num).Split('\n', StringSplitOptions.None)
            select l.Trim() into l
            where l.StartsWith("- File #", StringComparison.Ordinal) && l.IndexOf(": ", StringComparison.Ordinal) > 0
            select l).Select<string, string>(delegate(string l)
          {
            int num4 = l.IndexOf(": ", StringComparison.Ordinal) + 2;
            return l.Substring(num4, l.Length - num4).Trim();
          })
          where p.Length > 0
          select p).ToList<string>();
        if (list.Count > 0)
        {
          text2 = (text2 + "\n\nAttached files:\n" + string.Join("\n", list.Select<string, string>((string f) => "- " + f))).TrimStart();
        }
      }
      return text2;
    }

    // Token: 0x06000213 RID: 531 RVA: 0x000DEF6C File Offset: 0x000DEF6C
    private AgentTask GetTask(string taskId)
    {
      AgentTask agentTask;
      if (!this.tasks.TryGetValue(taskId, out agentTask))
      {
        throw new KeyNotFoundException("No agent task " + taskId + ".");
      }
      return agentTask;
    }

    // Token: 0x06000214 RID: 532 RVA: 0x000DEFA0 File Offset: 0x000DEFA0
    private static T Lock<[Nullable(2)] T>(AgentTask t, Func<T> f)
    {
      object gate = t.Gate;
      T t2;
      lock (gate)
      {
        t2 = f();
      }
      return t2;
    }

    // Token: 0x06000215 RID: 533 RVA: 0x000DEFE4 File Offset: 0x000DEFE4
    private void SetProgress(AgentTask task, string summary)
    {
      object gate = task.Gate;
      lock (gate)
      {
        task.ProgressSummary = summary;
      }
      this.Raise(task, AgentTaskEventKind.ProgressChanged, summary);
    }

    // Token: 0x06000216 RID: 534 RVA: 0x000DF030 File Offset: 0x000DF030
    private void Raise(AgentTask task, AgentTaskEventKind kind, [Nullable(2)] string text)
    {
      if (kind == AgentTaskEventKind.ThinkingHeading && text != null && text.Length > 0)
      {
        string text2 = this.LiveStepsKey(task);
        if (text2 != null)
        {
          this.LiveSteps.NoteThinking(text, text2, null);
        }
      }
      try
      {
        EventHandler<AgentTaskEvent> taskEvent = this.TaskEvent;
        if (taskEvent != null)
        {
          taskEvent(this, new AgentTaskEvent(task.Id, kind, text));
        }
      }
      catch (Exception ex)
      {
        Log.Error("Agents", "TaskEvent handler threw", ex);
      }
      if (kind <= AgentTaskEventKind.Commentary)
      {
        if (kind != AgentTaskEventKind.ProgressChanged && kind != AgentTaskEventKind.Commentary)
        {
          goto IL_0081;
        }
      }
      else if (kind != AgentTaskEventKind.TitleChanged && kind != AgentTaskEventKind.PlanUpdated)
      {
        goto IL_0081;
      }
      bool flag = true;
      goto IL_0083;
      IL_0081:
      flag = false;
      IL_0083:
      if (flag)
      {
        this.RaiseTasksChanged();
      }
    }

    // Token: 0x06000217 RID: 535 RVA: 0x000DF0DC File Offset: 0x000DF0DC
    private void RaiseTasksChanged()
    {
      if (Interlocked.Exchange(ref this.tasksChangedPending, 1) == 1)
      {
        return;
      }
      Task.Delay(50).ContinueWith(delegate(Task _)
      {
        Volatile.Write(ref this.tasksChangedPending, 0);
        try
        {
          EventHandler tasksChanged = this.TasksChanged;
          if (tasksChanged != null)
          {
            tasksChanged(this, EventArgs.Empty);
          }
        }
        catch (Exception ex)
        {
          Log.Error("Agents", "TasksChanged handler threw", ex);
        }
      }, TaskScheduler.Default);
    }

    // Token: 0x06000218 RID: 536 RVA: 0x000DF10C File Offset: 0x000DF10C
    public async ValueTask DisposeAsync()
    {
      if (!this.disposed)
      {
        this.disposed = true;
        this.routines.Dispose();
        foreach (AgentTask agentTask in this.tasks.Values.ToList<AgentTask>())
        {
          this.session.Interrupt(agentTask, TurnOutcome.Interrupted, "HeyClicky stopped before the agent task finished.");
        }
        this.lifetime.Cancel();
        Action action;
        if ((action = AgentService.<>O.<1>__Stop) == null)
        {
          action = (AgentService.<>O.<1>__Stop = new Action(AgentHarnessRunnerProcess.Stop));
        }
        await Task.Run(action).ConfigureAwait(false);
        this.lifetime.Dispose();
      }
    }

    // Token: 0x1400000B RID: 11
    // (add) Token: 0x06000219 RID: 537 RVA: 0x000DF150 File Offset: 0x000DF150
    // (remove) Token: 0x0600021A RID: 538 RVA: 0x000DF188 File Offset: 0x000DF188
    [Nullable(new byte[] { 2, 1, 1, 1, 1, 1 })]
    [field: Nullable(new byte[] { 2, 1, 1, 1, 1, 1 })]
    public event Action<string, string, string, double, IReadOnlyDictionary<string, double>> HarnessTurnEnded;

    // Token: 0x1400000C RID: 12
    // (add) Token: 0x0600021B RID: 539 RVA: 0x000DF1C0 File Offset: 0x000DF1C0
    // (remove) Token: 0x0600021C RID: 540 RVA: 0x000DF1F8 File Offset: 0x000DF1F8
    [Nullable(new byte[] { 2, 1, 1, 1, 1 })]
    [field: Nullable(new byte[] { 2, 1, 1, 1, 1 })]
    public event Action<string, string, string, string> InternalError;

    // Token: 0x17000093 RID: 147
    // (get) Token: 0x0600021D RID: 541 RVA: 0x000DF230 File Offset: 0x000DF230
    // (set) Token: 0x0600021E RID: 542 RVA: 0x000DF238 File Offset: 0x000DF238
    [Nullable(new byte[] { 2, 1, 2 })]
    public Func<CancellationToken, Task<AgentHarnessMcpServer>> BrowserMcpServerProvider
    {
      [return: Nullable(new byte[] { 2, 1, 2 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 2 })]
      set;
    }

    // Token: 0x17000094 RID: 148
    // (get) Token: 0x0600021F RID: 543 RVA: 0x000DF244 File Offset: 0x000DF244
    // (set) Token: 0x06000220 RID: 544 RVA: 0x000DF24C File Offset: 0x000DF24C
    [Nullable(2)]
    public Func<bool> IsClickyDriveAvailable
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000095 RID: 149
    // (get) Token: 0x06000221 RID: 545 RVA: 0x000DF258 File Offset: 0x000DF258
    // (set) Token: 0x06000222 RID: 546 RVA: 0x000DF260 File Offset: 0x000DF260
    [Nullable(2)]
    public IClickyLiveNoteTurns LiveNotes
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000223 RID: 547 RVA: 0x000DF26C File Offset: 0x000DF26C
    [return: Nullable(2)]
    private string LiveNotePath(AgentTask task)
    {
      object gate = task.Gate;
      string clickySlug;
      string workspacePath;
      bool isDemo;
      bool flag2;
      lock (gate)
      {
        clickySlug = task.ClickySlug;
        workspacePath = task.WorkspacePath;
        isDemo = task.IsDemo;
        flag2 = task.JobId != null;
      }
      IClickyLiveNoteTurns liveNotes = this.LiveNotes;
      if (liveNotes == null || isDemo || flag2 || clickySlug == null)
      {
        return null;
      }
      return liveNotes.NotePath(clickySlug, workspacePath ?? "");
    }

    // Token: 0x06000224 RID: 548 RVA: 0x000DF2FC File Offset: 0x000DF2FC
    private string ClickyLiveNoteSection(AgentTask task, string prompt, string launchSource, bool isGeneratedBrief, [Nullable(2)] string threadId, bool threadIsActive)
    {
      IClickyLiveNoteTurns liveNotes = this.LiveNotes;
      if (liveNotes != null)
      {
        string text = this.LiveNotePath(task);
        if (text != null)
        {
          object gate = task.Gate;
          bool isRoutineRun;
          string title;
          lock (gate)
          {
            isRoutineRun = task.IsRoutineRun;
            title = task.Title;
          }
          string text2 = ((!isRoutineRun && !isGeneratedBrief && !threadIsActive && !(launchSource == "routine")) ? prompt : null);
          string text3 = (isRoutineRun ? title : null);
          ValueTuple<AgentHarness, AgentHarnessPlace> valueTuple = AgentHarnessThreads.Choice(threadId);
          ClickyLiveNoteWriter clickyLiveNoteWriter = (AgentHarnessPreferences.UsesNoteTools(valueTuple.Item1, valueTuple.Item2) ? ClickyLiveNoteWriter.NoteTools : ClickyLiveNoteWriter.FileTools);
          string text4;
          try
          {
            text4 = liveNotes.PromptSection(text, text2, text3, clickyLiveNoteWriter);
          }
          catch (Exception ex)
          {
            Log.Warn("Agents", "live note section: " + ex.Message);
            text4 = "";
          }
          return text4;
        }
      }
      return "";
    }

    // Token: 0x06000225 RID: 549 RVA: 0x000DF3F4 File Offset: 0x000DF3F4
    private void NoteLiveNoteTaskEnded(AgentTask task)
    {
      IClickyLiveNoteTurns liveNotes = this.LiveNotes;
      if (liveNotes != null)
      {
        string text = this.LiveNotePath(task);
        if (text != null)
        {
          try
          {
            liveNotes.TurnEnded(text);
          }
          catch (Exception ex)
          {
            Log.Warn("Agents", "live note turn end: " + ex.Message);
          }
          return;
        }
      }
    }

    // Token: 0x06000226 RID: 550 RVA: 0x000DF450 File Offset: 0x000DF450
    private void NoteLiveNoteFileActivity(AgentTask task, string path, [Nullable(2)] string streamedKind, [Nullable(2)] string streamedDiff)
    {
      IClickyLiveNoteTurns liveNotes = this.LiveNotes;
      if (liveNotes != null)
      {
        string text = this.LiveNotePath(task);
        if (text != null)
        {
          try
          {
            liveNotes.FileActivity(text, path, streamedKind, streamedDiff);
          }
          catch (Exception ex)
          {
            Log.Warn("Agents", "live note file: " + ex.Message);
          }
          return;
        }
      }
    }

    // Token: 0x17000096 RID: 150
    // (get) Token: 0x06000227 RID: 551 RVA: 0x000DF4B0 File Offset: 0x000DF4B0
    private string WorkerBase
    {
      get
      {
        return this.endpoints.WorkerBaseUrl.AbsoluteUri.TrimEnd('/');
      }
    }

    // Token: 0x06000228 RID: 552 RVA: 0x000DF4CC File Offset: 0x000DF4CC
    private void ConfigureAgentHarness()
    {
      AgentHarnessRunnerProcess.StateDirectory = Path.Combine(this.options.StateDirectory, "AgentHarness");
      AgentHarnessRunnerProcess.BundledRunnerDirectory = this.options.HarnessRuntimeDirectory;
      AgentHarnessRunnerProcess.InternalError += delegate(string subsystem, string stage, string detail, string key)
      {
        this.RaiseInternalError(subsystem, stage, detail, key);
      };
      this.session.OnHarnessTurnEnded = delegate(AgentHarness harness, AgentHarnessPlace place, string status, double seconds, IReadOnlyDictionary<string, double> stats)
      {
        try
        {
          Action<string, string, string, double, IReadOnlyDictionary<string, double>> harnessTurnEnded = this.HarnessTurnEnded;
          if (harnessTurnEnded != null)
          {
            harnessTurnEnded(harness.RawValue(), place.RawValue(), status, seconds, stats);
          }
        }
        catch (Exception ex)
        {
          Log.Warn("Harness", "HarnessTurnEnded handler threw: " + ex.Message);
        }
      };
      this.session.OnHarnessInternalError = delegate(string stage, string detail, string key)
      {
        this.RaiseInternalError("agent_harness", stage, detail, key);
      };
      this.session.OnHarnessConnectedAppsUnreachable = delegate
      {
        Log.Info("Agents", "a harness job couldn't reach the connected apps: the next turn fetches a fresh Composio session");
        this.InvalidateConnectedApps();
      };
    }

    // Token: 0x06000229 RID: 553 RVA: 0x000DF55C File Offset: 0x000DF55C
    private void RaiseInternalError(string subsystem, string stage, string detail, string key)
    {
      try
      {
        Action<string, string, string, string> internalError = this.InternalError;
        if (internalError != null)
        {
          internalError(subsystem, stage, detail, key);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Harness", "InternalError handler threw: " + ex.Message);
      }
    }

    // Token: 0x0600022A RID: 554 RVA: 0x000DF5B0 File Offset: 0x000DF5B0
    private string TakeoverContext(string threadId, AgentTask task)
    {
      object gate = task.Gate;
      string jobId;
      lock (gate)
      {
        jobId = task.JobId;
      }
      ClickyJob clickyJob = ((jobId == null) ? null : this.Jobs.Job(jobId)) ?? this.Jobs.JobForThread(threadId);
      string text = ((clickyJob != null) ? this.JobConversationSoFar(clickyJob, 24000) : AgentService.NewestExchanges(this.CachedConversationExchanges(new <>z__ReadOnlySingleElementList<string>(threadId)), 24000));
      string text2 = ((clickyJob == null && this.clickys.Clickys.Any<ClickyInfo>((ClickyInfo c) => c.ThreadId == threadId)) ? "Your workspace (the working folder) has AGENTS.md with your notes and everything you made earlier; read AGENTS.md first, and open an earlier file only when this message needs it." : "The working folder has everything made earlier; open an earlier file only when this message needs it.");
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(250, 2);
      defaultInterpolatedStringHandler.AppendLiteral("This conversation began in an earlier agent setup, and you're carrying it on fresh: you can't see those earlier turns, so here they are, oldest first:\n<earlier_conversation>\n");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("\n</earlier_conversation>\n");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral(" Keep what was done; pick up from where it stopped.");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x0600022B RID: 555 RVA: 0x000DF6D4 File Offset: 0x000DF6D4
    private async Task<AgentHarnessTurnContext> HarnessTurnContextAsync(string appThreadId, AgentTask task, string workingDirectory, CancellationToken ct)
    {
      await this.EnsureRuntimeReadyAsync(ct).ConfigureAwait(false);
      string sessionToken = await this.tokens.GetAsync(ct, false).ConfigureAwait(false);
      List<AgentHarnessMcpServer> servers = new List<AgentHarnessMcpServer>();
      ComposioMcpConfiguration composioMcpConfiguration = this.composio;
      string text = ((composioMcpConfiguration != null) ? composioMcpConfiguration.Url : null);
      if (text != null && text.Length > 0)
      {
        servers.Add(new AgentHarnessMcpServer("composio", text, sessionToken));
      }
      servers.AddRange(this.HarnessCustomConnectorServers());
      Func<CancellationToken, Task<AgentHarnessMcpServer>> browserMcpServerProvider = this.BrowserMcpServerProvider;
      if (browserMcpServerProvider != null)
      {
        try
        {
          AgentHarnessMcpServer agentHarnessMcpServer = await browserMcpServerProvider(ct).ConfigureAwait(false);
          if (agentHarnessMcpServer != null)
          {
            List<AgentHarnessMcpServer> list = servers;
            AgentHarnessMcpServer agentHarnessMcpServer2 = agentHarnessMcpServer.<Clone>$();
            agentHarnessMcpServer2.SendsTurnIdentity = true;
            list.Add(agentHarnessMcpServer2);
          }
        }
        catch (Exception ex)
        {
          Log.Warn("Harness", "browser endpoint: " + ex.Message);
        }
      }
      object gate = task.Gate;
      bool isJob;
      lock (gate)
      {
        isJob = task.JobId != null;
      }
      string notePath = (isJob ? null : this.LiveNotePath(task));
      ValueTuple<AgentHarness, AgentHarnessPlace> choice = AgentHarnessThreads.Choice(appThreadId);
      AgentHarnessTurnContext agentHarnessTurnContext = new AgentHarnessTurnContext();
      agentHarnessTurnContext.Instructions = this.home.DeliveredModelInstructions ?? "";
      agentHarnessTurnContext.McpServers = servers;
      agentHarnessTurnContext.SkillDirectories = this.HarnessSkillDirectories();
      AgentHarnessTurnContext agentHarnessTurnContext2 = agentHarnessTurnContext;
      agentHarnessTurnContext2.UserSkillDirectories = await ClickyUserSkillFolders.SkillDirectoriesAsync(null).ConfigureAwait(false);
      agentHarnessTurnContext.WorkerBaseUrl = this.WorkerBase;
      agentHarnessTurnContext.SessionToken = sessionToken;
      agentHarnessTurnContext.NotePath = notePath;
      agentHarnessTurnContext.IsKeptThing = notePath != null && !isJob;
      agentHarnessTurnContext.WritesNoteWithTools = notePath != null && AgentHarnessPreferences.UsesNoteTools(choice.Item1, choice.Item2);
      agentHarnessTurnContext.UsesPlanTool = isJob;
      agentHarnessTurnContext.Model = (this.tokens.AllowsDeveloperModelSelection ? AgentHarnessPreferences.Model : AgentHarnessModel.Standard);
      return agentHarnessTurnContext;
    }

    // Token: 0x0600022C RID: 556 RVA: 0x000DF730 File Offset: 0x000DF730
    private IReadOnlyList<AgentHarnessMcpServer> HarnessCustomConnectorServers()
    {
      List<AgentHarnessMcpServer> list = new List<AgentHarnessMcpServer>();
      foreach (CustomConnectorRegistration customConnectorRegistration in this.customConnectors)
      {
        CustomConnectorTransport.Stdio stdio = customConnectorRegistration.Transport as CustomConnectorTransport.Stdio;
        if (stdio != null)
        {
          if (stdio.Command.Length != 0)
          {
            Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (string text in stdio.PassthroughEnvironmentVariables)
            {
              string text2;
              if (customConnectorRegistration.Environment.TryGetValue(text, out text2))
              {
                dictionary[text] = text2;
              }
            }
            dictionary["PATH"] = stdio.SearchPath;
            list.Add(new AgentHarnessMcpServer(customConnectorRegistration.ServerKey, "", null)
            {
              LocalCommand = new AgentHarnessLocalMcpCommand(stdio.Command, stdio.Arguments, dictionary)
            });
          }
        }
        else
        {
          CustomConnectorTransport.Remote remote = customConnectorRegistration.Transport as CustomConnectorTransport.Remote;
          Uri uri;
          bool flag = remote == null || !Uri.TryCreate(remote.Url.Trim(), UriKind.Absolute, out uri);
          if (!flag)
          {
            string scheme = uri.Scheme;
            bool flag2 = scheme == "https" || scheme == "http";
            flag = !flag2;
          }
          if (!flag)
          {
            string bearerTokenEnvironmentVariable = remote.BearerTokenEnvironmentVariable;
            string text4;
            string text3 = ((bearerTokenEnvironmentVariable != null && customConnectorRegistration.Environment.TryGetValue(bearerTokenEnvironmentVariable, out text4) && text4.Length > 0) ? text4 : null);
            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> keyValuePair in remote.HeaderEnvironmentVariables)
            {
              string scheme;
              string text5;
              keyValuePair.Deconstruct(out scheme, out text5);
              string text6 = scheme;
              string text7 = text5;
              string text8;
              if (customConnectorRegistration.Environment.TryGetValue(text7, out text8))
              {
                dictionary2[text6] = text8;
              }
            }
            list.Add(new AgentHarnessMcpServer(customConnectorRegistration.ServerKey, uri.AbsoluteUri, text3)
            {
              Headers = dictionary2
            });
          }
        }
      }
      return list;
    }

    // Token: 0x0600022D RID: 557 RVA: 0x000DF9A4 File Offset: 0x000DF9A4
    private IReadOnlyList<string> HarnessSkillDirectories()
    {
      List<string> list = new List<string>();
      foreach (string text in new string[] { "skills", "power-up-skills" })
      {
        string text2 = Path.Combine(this.options.CodexHome, text);
        if (Directory.Exists(text2))
        {
          foreach (string text3 in Directory.GetDirectories(text2).OrderBy<string, string>((string d) => Path.GetFileName(d), StringComparer.Ordinal))
          {
            if (!Path.GetFileName(text3).StartsWith('.') && File.Exists(Path.Combine(text3, "SKILL.md")))
            {
              list.Add(text3);
            }
          }
        }
      }
      return list;
    }

    // Token: 0x17000097 RID: 151
    // (get) Token: 0x0600022E RID: 558 RVA: 0x000DFA9C File Offset: 0x000DFA9C
    public IReadOnlyList<ClickyInfo> ArchivedClickys
    {
      get
      {
        return this.clickys.ArchivedClickys;
      }
    }

    // Token: 0x0600022F RID: 559 RVA: 0x000DFAAC File Offset: 0x000DFAAC
    [return: Nullable(2)]
    public ClickyInfo RenameClicky(string slug, string name)
    {
      ClickyInfo clickyInfo = this.clickys.Rename(slug, name);
      if (clickyInfo == null)
      {
        return null;
      }
      IEnumerable<AgentTask> values = this.tasks.Values;
      Func<AgentTask, bool> <>9__0;
      Func<AgentTask, bool> func;
      if ((func = <>9__0) == null)
      {
        func = (<>9__0 = (AgentTask t) => t.ClickySlug == slug);
      }
      foreach (AgentTask agentTask in values.Where<AgentTask>(func))
      {
        object gate = agentTask.Gate;
        lock (gate)
        {
          agentTask.ClickyName = clickyInfo.Name;
        }
      }
      this.RaiseTasksChanged();
      return clickyInfo;
    }

    // Token: 0x06000230 RID: 560 RVA: 0x000DFB84 File Offset: 0x000DFB84
    [return: Nullable(2)]
    public ClickyInfo UnarchiveClicky(string slug)
    {
      ClickyInfo clickyInfo = this.clickys.Unarchive(slug);
      if (clickyInfo != null)
      {
        this.clickys.EnsureWorkspace(clickyInfo);
      }
      this.RaiseTasksChanged();
      return clickyInfo;
    }

    // Token: 0x06000231 RID: 561 RVA: 0x000DFBB8 File Offset: 0x000DFBB8
    public bool IsThreadOwnedByClicky(string threadId)
    {
      return this.clickys.OwnsThread(threadId);
    }

    // Token: 0x06000232 RID: 562 RVA: 0x000DFBC8 File Offset: 0x000DFBC8
    public bool MoveThreadIntoClicky(string threadId, string slug, [Nullable(2)] string notesMarkdown)
    {
      ClickyInfo clickyInfo = this.clickys.Get(slug);
      if (clickyInfo == null || clickyInfo.IsArchived || this.clickys.OwnsThread(threadId))
      {
        return false;
      }
      this.clickys.EnsureWorkspace(clickyInfo);
      bool flag = clickyInfo.ThreadId == null;
      if (flag)
      {
        this.clickys.BindThread(slug, threadId);
      }
      else
      {
        this.clickys.FileThread(threadId, slug);
      }
      if (!string.IsNullOrWhiteSpace(notesMarkdown))
      {
        this.clickys.AppendNotes(clickyInfo, notesMarkdown);
      }
      this.RebindTasksForThread(threadId, this.clickys.Get(slug));
      string text = "Agents";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 3);
      defaultInterpolatedStringHandler.AppendLiteral("moved thread ");
      defaultInterpolatedStringHandler.AppendFormatted(threadId.Substring(0, Math.Min(8, threadId.Length)));
      defaultInterpolatedStringHandler.AppendLiteral(" into ");
      defaultInterpolatedStringHandler.AppendFormatted(slug);
      defaultInterpolatedStringHandler.AppendLiteral(" (became chat: ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      this.RaiseTasksChanged();
      return flag;
    }

    // Token: 0x06000233 RID: 563 RVA: 0x000DFCD8 File Offset: 0x000DFCD8
    [return: Nullable(2)]
    public ClickyInfo FoundClickyFromThread(string threadId, string name, string roleText, string oneLiner, [Nullable(2)] string notesMarkdown)
    {
      if (this.clickys.OwnsThread(threadId))
      {
        return null;
      }
      ClickyInfo clickyInfo = this.CreateClicky(name, string.IsNullOrWhiteSpace(roleText) ? "Agent" : roleText, oneLiner);
      this.clickys.BindThread(clickyInfo.Slug, threadId);
      if (!string.IsNullOrWhiteSpace(notesMarkdown))
      {
        this.clickys.AppendNotes(clickyInfo, notesMarkdown);
      }
      ClickyInfo clickyInfo2 = this.clickys.Get(clickyInfo.Slug);
      this.RebindTasksForThread(threadId, clickyInfo2);
      Log.Info("Agents", "founded " + clickyInfo2.Slug + " from thread " + threadId.Substring(0, Math.Min(8, threadId.Length)));
      this.RaiseTasksChanged();
      return clickyInfo2;
    }

    // Token: 0x06000234 RID: 564 RVA: 0x000DFD8C File Offset: 0x000DFD8C
    public void AppendClickyNotes(string slug, string markdown)
    {
      ClickyInfo clickyInfo = this.clickys.Get(slug);
      if (clickyInfo != null)
      {
        this.clickys.AppendNotes(clickyInfo, markdown);
      }
    }

    // Token: 0x06000235 RID: 565 RVA: 0x000DFDB8 File Offset: 0x000DFDB8
    [return: Nullable(2)]
    public string ClickyNotesPath(string slug)
    {
      if (this.clickys.Get(slug) != null)
      {
        return this.clickys.NotesPath(slug);
      }
      return null;
    }

    // Token: 0x06000236 RID: 566 RVA: 0x000DFDD8 File Offset: 0x000DFDD8
    private void RebindTasksForThread(string threadId, ClickyInfo clicky)
    {
      IEnumerable<AgentTask> values = this.tasks.Values;
      Func<AgentTask, bool> <>9__0;
      Func<AgentTask, bool> func;
      if ((func = <>9__0) == null)
      {
        func = (<>9__0 = (AgentTask t) => t.ThreadId == threadId);
      }
      foreach (AgentTask agentTask in values.Where<AgentTask>(func))
      {
        object gate = agentTask.Gate;
        lock (gate)
        {
          agentTask.ClickySlug = clicky.Slug;
          agentTask.ClickyName = clicky.Name;
          agentTask.WorkspacePath = this.clickys.WorkspacePath(clicky.Slug);
        }
      }
    }

    // Token: 0x1400000D RID: 13
    // (add) Token: 0x06000237 RID: 567 RVA: 0x000DFEB4 File Offset: 0x000DFEB4
    // (remove) Token: 0x06000238 RID: 568 RVA: 0x000DFEEC File Offset: 0x000DFEEC
    [Nullable(new byte[] { 2, 1, 1 })]
    [field: Nullable(new byte[] { 2, 1, 1 })]
    public event Action<string, string> DataSourceRecorded;

    // Token: 0x17000098 RID: 152
    // (get) Token: 0x06000239 RID: 569 RVA: 0x000DFF24 File Offset: 0x000DFF24
    // (set) Token: 0x0600023A RID: 570 RVA: 0x000DFF2C File Offset: 0x000DFF2C
    [Nullable(2)]
    public Func<bool> CustomConnectorTokensExpiring
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000099 RID: 153
    // (get) Token: 0x0600023B RID: 571 RVA: 0x000DFF38 File Offset: 0x000DFF38
    // (set) Token: 0x0600023C RID: 572 RVA: 0x000DFF40 File Offset: 0x000DFF40
    [Nullable(new byte[] { 2, 1, 2, 1 })]
    public Func<CancellationToken, Task<IReadOnlyList<CustomConnectorRegistration>>> RefreshCustomConnectorTokensAsync
    {
      [return: Nullable(new byte[] { 2, 1, 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 2, 1 })]
      set;
    }

    // Token: 0x0600023D RID: 573 RVA: 0x000DFF4C File Offset: 0x000DFF4C
    private async Task RefreshConnectorTokensBeforePrepareAsync(CancellationToken ct)
    {
      Func<CancellationToken, Task<IReadOnlyList<CustomConnectorRegistration>>> refreshCustomConnectorTokensAsync = this.RefreshCustomConnectorTokensAsync;
      if (refreshCustomConnectorTokensAsync != null)
      {
        try
        {
          IReadOnlyList<CustomConnectorRegistration> readOnlyList = await refreshCustomConnectorTokensAsync(ct).ConfigureAwait(false);
          if (readOnlyList != null)
          {
            this.customConnectors = readOnlyList;
            this.configDirty = true;
          }
        }
        catch (Exception ex)
        {
          Log.Warn("Agents", "connector token refresh: " + ex.Message);
        }
      }
    }

    // Token: 0x0600023E RID: 574 RVA: 0x000DFF98 File Offset: 0x000DFF98
    private void WireIntegrationEvents()
    {
      this.session.OnIntegrationUsed = delegate(AgentTask task, string key)
      {
        object gate = task.Gate;
        string clickySlug;
        bool flag2;
        lock (gate)
        {
          clickySlug = task.ClickySlug;
          flag2 = task.IntegrationsUsedThisTurn.Add(key);
        }
        if (clickySlug != null)
        {
          this.RecordDataSource(clickySlug, key);
        }
        if (flag2)
        {
          this.Raise(task, AgentTaskEventKind.IntegrationUsed, key);
        }
      };
    }

    // Token: 0x0600023F RID: 575 RVA: 0x000DFFB4 File Offset: 0x000DFFB4
    [NullableContext(2)]
    public void SetObsidianVaultPath(string path)
    {
      string text = (string.IsNullOrWhiteSpace(path) ? null : path.Trim());
      if (string.Equals(this.options.ObsidianVaultPath, text, StringComparison.OrdinalIgnoreCase))
      {
        return;
      }
      this.options.ObsidianVaultPath = text;
      this.InvalidateConnectedApps();
      Log.Info("Agents", (text == null) ? "obsidian vault cleared" : ("obsidian vault set (" + Path.GetFileName(text.TrimEnd('\\')) + ")"));
    }

    // Token: 0x1700009A RID: 154
    // (get) Token: 0x06000240 RID: 576 RVA: 0x000E002C File Offset: 0x000E002C
    private string DataSourcesPath
    {
      get
      {
        return Path.Combine(this.options.StateDirectory, "data-sources.json");
      }
    }

    // Token: 0x06000241 RID: 577 RVA: 0x000E0044 File Offset: 0x000E0044
    private Dictionary<string, List<string>> LoadDataSources()
    {
      if (this.dataSources != null)
      {
        return this.dataSources;
      }
      this.dataSources = new Dictionary<string, List<string>>(StringComparer.Ordinal);
      try
      {
        if (File.Exists(this.DataSourcesPath))
        {
          JsonObject jsonObject = JsonNode.Parse(File.ReadAllText(this.DataSourcesPath), null, default(JsonDocumentOptions)) as JsonObject;
          if (jsonObject != null)
          {
            foreach (KeyValuePair<string, JsonNode> keyValuePair in jsonObject)
            {
              string text;
              JsonNode jsonNode;
              keyValuePair.Deconstruct(out text, out jsonNode);
              string text2 = text;
              JsonNode jsonNode2 = jsonNode;
              Dictionary<string, List<string>> dictionary = this.dataSources;
              string text3 = text2;
              JsonArray jsonArray;
              if ((jsonArray = jsonNode2 as JsonArray) == null)
              {
                jsonArray = new JsonArray(null);
              }
              dictionary[text3] = jsonArray.Select<JsonNode, string>(delegate(JsonNode k)
              {
                if (k == null)
                {
                  return null;
                }
                return k.ToString();
              }).OfType<string>().ToList<string>();
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Agents", "data-sources.json unreadable: " + ex.Message);
      }
      return this.dataSources;
    }

    // Token: 0x06000242 RID: 578 RVA: 0x000E0180 File Offset: 0x000E0180
    public void RecordDataSource(string slug, string providerKey)
    {
      string text = providerKey.Trim().ToLowerInvariant();
      if (text.Length == 0)
      {
        return;
      }
      object obj = this.dataSourcesGate;
      lock (obj)
      {
        Dictionary<string, List<string>> dictionary = this.LoadDataSources();
        List<string> list;
        if (!dictionary.TryGetValue(slug, out list))
        {
          list = (dictionary[slug] = new List<string>());
        }
        if (list.Contains(text))
        {
          return;
        }
        list.Add(text);
        try
        {
          Directory.CreateDirectory(this.options.StateDirectory);
          JsonObject jsonObject = new JsonObject(null);
          foreach (KeyValuePair<string, List<string>> keyValuePair in dictionary)
          {
            string text2;
            List<string> list2;
            keyValuePair.Deconstruct(out text2, out list2);
            string text3 = text2;
            List<string> list3 = list2;
            jsonObject[text3] = new JsonArray(list3.Select<string, JsonNode>((string x) => x).ToArray<JsonNode>());
          }
          File.WriteAllText(this.DataSourcesPath, jsonObject.ToJsonString(new JsonSerializerOptions
          {
            WriteIndented = true
          }));
        }
        catch (Exception ex)
        {
          Log.Warn("Agents", "data-sources.json write failed: " + ex.Message);
        }
      }
      Log.Info("Agents", "data source: " + slug + " used " + text);
      Action<string, string> dataSourceRecorded = this.DataSourceRecorded;
      if (dataSourceRecorded == null)
      {
        return;
      }
      dataSourceRecorded(slug, text);
    }

    // Token: 0x06000243 RID: 579 RVA: 0x000E0348 File Offset: 0x000E0348
    public void RemoveDataSource(string slug, string providerKey)
    {
      object obj = this.dataSourcesGate;
      lock (obj)
      {
        Dictionary<string, List<string>> dictionary = this.LoadDataSources();
        List<string> list;
        if (dictionary.TryGetValue(slug, out list) && list.Remove(providerKey.Trim().ToLowerInvariant()))
        {
          if (list.Count == 0)
          {
            dictionary.Remove(slug);
          }
          try
          {
            JsonObject jsonObject = new JsonObject(null);
            foreach (KeyValuePair<string, List<string>> keyValuePair in dictionary)
            {
              string text;
              List<string> list2;
              keyValuePair.Deconstruct(out text, out list2);
              string text2 = text;
              List<string> list3 = list2;
              jsonObject[text2] = new JsonArray(list3.Select<string, JsonNode>((string x) => x).ToArray<JsonNode>());
            }
            File.WriteAllText(this.DataSourcesPath, jsonObject.ToJsonString(new JsonSerializerOptions
            {
              WriteIndented = true
            }));
          }
          catch (Exception ex)
          {
            Log.Warn("Agents", "data-sources.json write failed: " + ex.Message);
          }
        }
      }
    }

    // Token: 0x06000244 RID: 580 RVA: 0x000E04C0 File Offset: 0x000E04C0
    public IReadOnlyList<string> DataSources(string slug)
    {
      object obj = this.dataSourcesGate;
      IReadOnlyList<string> readOnlyList;
      lock (obj)
      {
        List<string> list;
        readOnlyList = (this.LoadDataSources().TryGetValue(slug, out list) ? list.ToList<string>() : new List<string>());
      }
      return readOnlyList;
    }

    // Token: 0x06000245 RID: 581 RVA: 0x000E051C File Offset: 0x000E051C
    private static string JobNamingBasis(ClickyJob job)
    {
      string text = job.Prompt.Trim();
      string text2 = ((text.Length == 0 || text == job.Title) ? job.Title : (job.Title + ". " + text));
      if (text2.Length <= 600)
      {
        return text2;
      }
      return text2.Substring(0, 600);
    }

    // Token: 0x06000246 RID: 582 RVA: 0x000E0580 File Offset: 0x000E0580
    internal static bool IsSentenceRatherThanName(string name)
    {
      List<string> list = name.Split(null, StringSplitOptions.RemoveEmptyEntries).Select<string, string>(delegate(string word)
      {
        IEnumerable<char> enumerable = word.ToLowerInvariant();
        Func<char, bool> func;
        if ((func = AgentService.<>O.<2>__IsLetter) == null)
        {
          func = (AgentService.<>O.<2>__IsLetter = new Func<char, bool>(char.IsLetter));
        }
        return new string(enumerable.Where<char>(func).ToArray<char>());
      }).ToList<string>();
      if (list.Count == 0)
      {
        return false;
      }
      if (!AgentService.SentenceStartWords.Contains(list[0]))
      {
        return list.Any<string>((string word) => word.Length == 1);
      }
      return true;
    }

    // Token: 0x06000247 RID: 583 RVA: 0x000E0604 File Offset: 0x000E0604
    private IReadOnlyList<string> JobNamesInUse(string exceptJobId)
    {
      return (from job in this.Jobs.Jobs
        where job.Id != exceptJobId && !job.IsArchived.GetValueOrDefault() && !string.IsNullOrWhiteSpace(job.Nickname)
        orderby job.CreatedAt descending
        select job.Nickname.Trim()).ToList<string>();
    }

    // Token: 0x06000248 RID: 584 RVA: 0x000E0688 File Offset: 0x000E0688
    public void NameJobsWithoutNicknames()
    {
      if (Interlocked.Exchange(ref this.jobNamingRoundRunning, 1) == 1)
      {
        return;
      }
      Task.Run(async delegate
      {
        AgentService.<>c__DisplayClass333_0 CS$<>8__locals1;
        CS$<>8__locals1.account = Volatile.Read(ref this.accountGeneration);
        try
        {
          using (List<ClickyJob>.Enumerator enumerator = this.Jobs.Jobs.Where<ClickyJob>((ClickyJob job) => !string.IsNullOrWhiteSpace(job.Nickname) && AgentService.IsSentenceRatherThanName(job.Nickname)).ToList<ClickyJob>().GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              ClickyJob clickyJob = enumerator.Current;
              this.Jobs.SetNickname(clickyJob.Id, "");
            }
            goto IL_030E;
          }
          IL_00B0:
          HashSet<string> hashSet = this.jobIdsBeingNamed;
          ClickyJob next;
          lock (hashSet)
          {
            next = (from job in this.Jobs.Jobs
              where string.IsNullOrWhiteSpace(job.Nickname) && !job.IsArchived.GetValueOrDefault() && !this.jobIdsBeingNamed.Contains(job.Id)
              orderby job.CreatedAt descending
              select job).FirstOrDefault<ClickyJob>();
          }
          if (next == null)
          {
            return;
          }
          hashSet = this.jobIdsBeingNamed;
          lock (hashSet)
          {
            this.jobIdsBeingNamed.Add(next.Id);
          }
          string basis = AgentService.JobNamingBasis(next);
          if (basis.Length >= 8)
          {
            string text = await this.supabaseAccessToken(this.lifetime.Token).ConfigureAwait(false);
            if (text == null || this.<NameJobsWithoutNicknames>g__SignedOutSince|333_1(ref CS$<>8__locals1))
            {
              return;
            }
            string text2 = await this.api.ThreadTitleAsync(text, basis, this.lifetime.Token, new TimeSpan?(TimeSpan.FromSeconds(20L)), this.JobNamesInUse(next.Id)).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(text2) || this.<NameJobsWithoutNicknames>g__SignedOutSince|333_1(ref CS$<>8__locals1))
            {
              return;
            }
            if (!AgentService.IsSentenceRatherThanName(text2))
            {
              this.Jobs.SetNickname(next.Id, text2.Trim());
              next = null;
              basis = null;
            }
          }
          IL_030E:
          if (!this.lifetime.IsCancellationRequested)
          {
            goto IL_00B0;
          }
        }
        catch (Exception ex) when (!(ex is OperationCanceledException))
        {
          Log.Warn("Agents", "job naming stopped: " + ex.Message);
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
          HashSet<string> hashSet = this.jobIdsBeingNamed;
          bool flag = false;
          try
          {
            Monitor.Enter(hashSet, ref flag);
            this.jobIdsBeingNamed.Clear();
          }
          finally
          {
            int num;
            if (num < 0 && flag)
            {
              Monitor.Exit(hashSet);
            }
          }
          Interlocked.Exchange(ref this.jobNamingRoundRunning, 0);
        }
      });
    }

    // Token: 0x1700009B RID: 155
    // (get) Token: 0x06000249 RID: 585 RVA: 0x000E06AC File Offset: 0x000E06AC
    public ClickyJobStore Jobs { get; }

    // Token: 0x1700009C RID: 156
    // (get) Token: 0x0600024A RID: 586 RVA: 0x000E06B4 File Offset: 0x000E06B4
    public ClickyLearnedHowTos LearnedHowTos { get; }

    // Token: 0x1700009D RID: 157
    // (get) Token: 0x0600024B RID: 587 RVA: 0x000E06BC File Offset: 0x000E06BC
    public ClickyRestartInterruptions RestartInterruptions { get; }

    // Token: 0x1700009E RID: 158
    // (get) Token: 0x0600024C RID: 588 RVA: 0x000E06C4 File Offset: 0x000E06C4
    public ClickyLiveSteps LiveSteps { get; } = new ClickyLiveSteps();

    // Token: 0x0600024D RID: 589 RVA: 0x000E06CC File Offset: 0x000E06CC
    [return: Nullable(2)]
    private string LiveStepsKey(AgentTask task)
    {
      object gate = task.Gate;
      lock (gate)
      {
        if (task.IsDemo)
        {
          return null;
        }
        string jobId = task.JobId;
        if (jobId != null)
        {
          return "job:" + jobId;
        }
      }
      return this.LiveNotePath(task);
    }

    // Token: 0x1400000E RID: 14
    // (add) Token: 0x0600024E RID: 590 RVA: 0x000E0734 File Offset: 0x000E0734
    // (remove) Token: 0x0600024F RID: 591 RVA: 0x000E076C File Offset: 0x000E076C
    [Nullable(new byte[] { 2, 1, 1, 1, 1 })]
    [field: Nullable(new byte[] { 2, 1, 1, 1, 1 })]
    public event Action<string, string, string, string> TaskFilePatchStreaming;

    // Token: 0x1400000F RID: 15
    // (add) Token: 0x06000250 RID: 592 RVA: 0x000E07A4 File Offset: 0x000E07A4
    // (remove) Token: 0x06000251 RID: 593 RVA: 0x000E07DC File Offset: 0x000E07DC
    [Nullable(new byte[] { 2, 1, 1, 1 })]
    [field: Nullable(new byte[] { 2, 1, 1, 1 })]
    public event Action<string, IReadOnlyList<string>> TaskFileChangeCompleted;

    // Token: 0x06000252 RID: 594 RVA: 0x000E0814 File Offset: 0x000E0814
    private void WireLiveSteps()
    {
      this.session.OnTaskFilePatchStreaming = delegate(AgentTask task, string path, string kind, string diff)
      {
        this.NoteLiveNoteFileActivity(task, path, kind, diff);
        Action<string, string, string, string> taskFilePatchStreaming = this.TaskFilePatchStreaming;
        if (taskFilePatchStreaming == null)
        {
          return;
        }
        taskFilePatchStreaming(task.Id, path, kind, diff);
      };
      this.session.OnTaskFileChangeCompleted = delegate(AgentTask task, IReadOnlyList<string> paths)
      {
        foreach (string text2 in paths)
        {
          this.NoteLiveNoteFileActivity(task, text2, null, null);
        }
        Action<string, IReadOnlyList<string>> taskFileChangeCompleted = this.TaskFileChangeCompleted;
        if (taskFileChangeCompleted == null)
        {
          return;
        }
        taskFileChangeCompleted(task.Id, paths);
      };
      this.session.OnTaskToolStep = delegate(AgentTask task, string text)
      {
        if (text.StartsWith("Reading:", StringComparison.Ordinal))
        {
          object gate = task.Gate;
          lock (gate)
          {
            int length = "Reading:".Length;
            task.NoteSitesRead(new <>z__ReadOnlySingleElementList<string>(text.Substring(length, text.Length - length)));
          }
        }
        string text3 = this.LiveStepsKey(task);
        if (text3 != null)
        {
          this.LiveSteps.Note(text, text3, null);
        }
      };
      this.session.OnTaskProgramBatch = delegate(AgentTask task, ClickyProgramBatch batch)
      {
        object gate2 = task.Gate;
        lock (gate2)
        {
          task.NoteSitesRead(batch.SitesRead);
        }
        string text4 = this.LiveStepsKey(task);
        if (text4 != null)
        {
          this.LiveSteps.NoteBatch(this.NamingApps(batch), text4, null);
        }
      };
      this.session.OnTaskPlanUpdated = delegate(AgentTask task, IReadOnlyList<AgentPlanStep> steps)
      {
        string text5 = this.LiveStepsKey(task);
        if (text5 != null)
        {
          this.LiveSteps.NotePlan(ClickyArcPlan.FromPlanTool(steps), text5);
        }
        this.Raise(task, AgentTaskEventKind.PlanUpdated, string.Join(" · ", steps.Select<AgentPlanStep, string>((AgentPlanStep s) => s.Text)));
      };
    }

    // Token: 0x06000253 RID: 595 RVA: 0x000E0894 File Offset: 0x000E0894
    private ClickyProgramBatch NamingApps(ClickyProgramBatch batch)
    {
      if (batch.Apps.Count == 0)
      {
        return batch;
      }
      AgentIntegrationCatalog agentIntegrationCatalog = this.lastIntegrationCatalog;
      List<ValueTuple<string, string, IReadOnlyList<ValueTuple<string, string>>>> list = (((agentIntegrationCatalog != null) ? agentIntegrationCatalog.Integrations : null) ?? Array.Empty<AgentIntegration>()).Select<AgentIntegration, ValueTuple<string, string, IReadOnlyList<ValueTuple<string, string>>>>((AgentIntegration i) => new ValueTuple<string, string, IReadOnlyList<ValueTuple<string, string>>>(i.DisplayName, i.LogoUrl, i.Toolkits.Select<AgentIntegrationToolkit, ValueTuple<string, string>>((AgentIntegrationToolkit t) => new ValueTuple<string, string>(t.Slug, t.Logo)).ToList<ValueTuple<string, string>>())).ToList<ValueTuple<string, string, IReadOnlyList<ValueTuple<string, string>>>>();
      return batch.NamingApps(list);
    }

    // Token: 0x1700009F RID: 159
    // (get) Token: 0x06000254 RID: 596 RVA: 0x000E08FC File Offset: 0x000E08FC
    public ClickyJobStepWatcher StepWatcher
    {
      get
      {
        ClickyJobStepWatcher clickyJobStepWatcher;
        if ((clickyJobStepWatcher = this.stepWatcher) == null)
        {
          clickyJobStepWatcher = (this.stepWatcher = this.CreateStepWatcher());
        }
        return clickyJobStepWatcher;
      }
    }

    // Token: 0x170000A0 RID: 160
    // (get) Token: 0x06000255 RID: 597 RVA: 0x000E0924 File Offset: 0x000E0924
    // (set) Token: 0x06000256 RID: 598 RVA: 0x000E092C File Offset: 0x000E092C
    [Nullable(new byte[] { 2, 1, 2, 1 })]
    public Func<string, IReadOnlyList<AgentTranscriptTurn>> CachedTranscript
    {
      [return: Nullable(new byte[] { 2, 1, 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 2, 1 })]
      set;
    }

    // Token: 0x170000A1 RID: 161
    // (get) Token: 0x06000257 RID: 599 RVA: 0x000E0938 File Offset: 0x000E0938
    // (set) Token: 0x06000258 RID: 600 RVA: 0x000E0940 File Offset: 0x000E0940
    [Nullable(new byte[] { 2, 1 })]
    public Func<string> ChromeAnswerMeaningsForBrain
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x06000259 RID: 601 RVA: 0x000E094C File Offset: 0x000E094C
    private string ChromeAnswerMeanings()
    {
      Func<string> chromeAnswerMeaningsForBrain = this.ChromeAnswerMeaningsForBrain;
      return ((chromeAnswerMeaningsForBrain != null) ? chromeAnswerMeaningsForBrain() : null) ?? "approve or show_how = they add it from the Chrome Web Store with you showing them each click";
    }

    // Token: 0x0600025A RID: 602 RVA: 0x000E096C File Offset: 0x000E096C
    private ClickyJobStepWatcher CreateStepWatcher()
    {
      ClickyJobStepWatcher clickyJobStepWatcher = new ClickyJobStepWatcher(this.Jobs);
      clickyJobStepWatcher.StepDone += delegate(string jobId, string address)
      {
        this.ContinueJobAfterUserStep(jobId, address);
      };
      return clickyJobStepWatcher;
    }

    // Token: 0x0600025B RID: 603 RVA: 0x000E098C File Offset: 0x000E098C
    [return: Nullable(2)]
    private AgentTask LiveTask(ClickyJob job)
    {
      AgentTask agentTask;
      if ((agentTask = this.tasks.Values.FirstOrDefault<AgentTask>((AgentTask t) => AgentService.Lock<string>(t, () => t.JobId) == job.Id)) == null)
      {
        string threadId = job.ThreadId;
        if (threadId == null)
        {
          return null;
        }
        agentTask = this.tasks.Values.FirstOrDefault<AgentTask>((AgentTask t) => AgentService.Lock<string>(t, () => t.ThreadId) == threadId);
      }
      return agentTask;
    }

    // Token: 0x0600025C RID: 604 RVA: 0x000E0A00 File Offset: 0x000E0A00
    [return: Nullable(2)]
    public string LiveTaskId(string jobId)
    {
      ClickyJob clickyJob = this.Jobs.Job(jobId);
      if (clickyJob == null)
      {
        return null;
      }
      AgentTask agentTask = this.LiveTask(clickyJob);
      if (agentTask == null)
      {
        return null;
      }
      return agentTask.Id;
    }

    // Token: 0x0600025D RID: 605 RVA: 0x000E0A34 File Offset: 0x000E0A34
    public ClickyJobRailPhase RailPhase(ClickyJob job)
    {
      if (job.IsHeldForChrome)
      {
        return ClickyJobRailPhase.NeedsYou;
      }
      AgentTask agentTask = this.LiveTask(job);
      if (agentTask != null)
      {
        object gate = agentTask.Gate;
        AgentTaskStatus status;
        bool requiresExtraUsageApproval;
        bool flag2;
        bool needsChromeConnection;
        lock (gate)
        {
          status = agentTask.Status;
          requiresExtraUsageApproval = agentTask.RequiresExtraUsageApproval;
          flag2 = agentTask.PendingUserStep != null;
          needsChromeConnection = agentTask.NeedsChromeConnection;
        }
        if (requiresExtraUsageApproval || agentTask.PendingComputerUseRequest != null)
        {
          return ClickyJobRailPhase.NeedsYou;
        }
        if (needsChromeConnection)
        {
          return ClickyJobRailPhase.NeedsYou;
        }
        if (status == AgentTaskStatus.Finished && (flag2 || job.WaitingStep != null))
        {
          return ClickyJobRailPhase.NeedsYou;
        }
        ClickyJobRailPhase clickyJobRailPhase;
        switch (status)
        {
        case AgentTaskStatus.Starting:
          clickyJobRailPhase = ClickyJobRailPhase.Queued;
          break;
        case AgentTaskStatus.Running:
          clickyJobRailPhase = ClickyJobRailPhase.Working;
          break;
        case AgentTaskStatus.WaitingForUser:
          clickyJobRailPhase = ClickyJobRailPhase.NeedsYou;
          break;
        case AgentTaskStatus.Finished:
          clickyJobRailPhase = ClickyJobRailPhase.Done;
          break;
        case AgentTaskStatus.Failed:
          clickyJobRailPhase = ClickyJobRailPhase.Failed;
          break;
        default:
          clickyJobRailPhase = ClickyJobRailPhase.Stopped;
          break;
        }
        return clickyJobRailPhase;
      }
      else
      {
        if (job.WaitingStep != null && job.Outcome == ClickyJobOutcome.Done)
        {
          return ClickyJobRailPhase.NeedsYou;
        }
        ClickyJobRailPhase clickyJobRailPhase;
        switch (job.Outcome)
        {
        case ClickyJobOutcome.Working:
          clickyJobRailPhase = ClickyJobRailPhase.Working;
          break;
        case ClickyJobOutcome.Done:
          clickyJobRailPhase = ClickyJobRailPhase.Done;
          break;
        case ClickyJobOutcome.Failed:
          clickyJobRailPhase = ClickyJobRailPhase.Failed;
          break;
        default:
          clickyJobRailPhase = ClickyJobRailPhase.Stopped;
          break;
        }
        return clickyJobRailPhase;
      }
    }

    // Token: 0x0600025E RID: 606 RVA: 0x000E0B54 File Offset: 0x000E0B54
    public ClickyJobMessageRoute JobMessageRoute(ClickyJob job, string requestedThreadId, string source, bool hasPreauthorizedLease)
    {
      ClickyJobMessageRoute clickyJobMessageRoute = new ClickyJobMessageRoute(requestedThreadId, null, false);
      bool flag = !ClickyJobsPreferences.IsEnabled;
      if (!flag)
      {
        bool flag2 = source == "text" || source == "voice";
        flag = !flag2;
      }
      if (flag || hasPreauthorizedLease)
      {
        return clickyJobMessageRoute;
      }
      AgentTask liveRun = this.LiveTask(job);
      if (liveRun != null)
      {
        string text = AgentService.Lock<string>(liveRun, () => liveRun.ThreadId);
        if (text != null)
        {
          bool flag3 = AgentService.Lock<bool>(liveRun, () => liveRun.IsActive) || this.session.IsThreadActive(text);
          bool flag4 = AgentService.Lock<bool>(liveRun, () => liveRun.RequiresExtraUsageApproval) || liveRun.PendingComputerUseRequest != null;
          if (flag3 || flag4)
          {
            return new ClickyJobMessageRoute(text, null, false);
          }
        }
      }
      if (this.session.IsThreadActive(requestedThreadId))
      {
        return clickyJobMessageRoute;
      }
      string threadId = job.ThreadId;
      flag = threadId != null;
      if (flag)
      {
        ClickyJobOutcome outcome = job.Outcome;
        bool flag2 = outcome == ClickyJobOutcome.Done || outcome == ClickyJobOutcome.Stopped;
        flag = flag2;
      }
      if (flag && DateTimeOffset.Now - (job.FinishedAt ?? DateTimeOffset.MinValue) < AgentService.JobResumeWindow && AgentHarnessThreads.ContinuesOnNewConversationHarness(threadId))
      {
        return new ClickyJobMessageRoute(threadId, null, true);
      }
      return new ClickyJobMessageRoute(null, job.ThreadId ?? requestedThreadId, false);
    }

    // Token: 0x0600025F RID: 607 RVA: 0x000E0CE4 File Offset: 0x000E0CE4
    [return: TupleElementNames(new string[] { "ThreadId", "Context" })]
    [return: Nullable(new byte[] { 0, 2, 1 })]
    private ValueTuple<string, string> PlanJobTurn(ClickyJob job, AgentTask task, string ask, string source, bool hasPreauthorizedLease, [Nullable(2)] string requestedThreadId, bool startsNewConversation = false)
    {
      string text = "";
      string text2 = (startsNewConversation ? null : (requestedThreadId ?? job.ThreadId));
      object obj = task.Gate;
      lock (obj)
      {
        task.IsNewJobRun = false;
      }
      if (text2 != null)
      {
        ClickyJobMessageRoute clickyJobMessageRoute = this.JobMessageRoute(job, text2, source, hasPreauthorizedLease);
        if (clickyJobMessageRoute.NewRunFollowsThreadId != null)
        {
          obj = task.Gate;
          lock (obj)
          {
            task.PendingUserStep = null;
            task.IsNewJobRun = true;
          }
          bool flag2 = ClickyWorkFolderFiles.RetireNote(job.FolderPath);
          text = text + "\n" + this.JobContinuationContext(job, flag2);
          this.Jobs.BeginRun(job.Id, ask, null);
          text2 = null;
        }
        else
        {
          if (clickyJobMessageRoute.ResumesFinishedRun)
          {
            obj = task.Gate;
            lock (obj)
            {
              task.PendingUserStep = null;
            }
            ClickyWorkFolderFiles.RetireNote(job.FolderPath);
            text += "\nThis is a new message to this job, in the conversation where you did the earlier work: build on it, and change what they ask now. Answer in the language of this newest message.";
            this.Jobs.BeginRun(job.Id, ask, job.ThreadId);
          }
          text2 = clickyJobMessageRoute.TargetThreadId;
        }
      }
      text = text + "\n" + ClickyJobBrief.WorkFolderContext;
      this.Jobs.MarkWorking(job.Id);
      return new ValueTuple<string, string>(text2, text);
    }

    // Token: 0x06000260 RID: 608 RVA: 0x000E0E68 File Offset: 0x000E0E68
    [return: Nullable(2)]
    private string LeaveOldHarnessThread(AgentTask task, string source, bool preauthorized)
    {
      if (source == "retry" || preauthorized)
      {
        return null;
      }
      object obj = task.Gate;
      string threadId;
      lock (obj)
      {
        threadId = task.ThreadId;
      }
      if (threadId == null || this.session.IsThreadActive(threadId) || AgentHarnessThreads.ContinuesOnNewConversationHarness(threadId))
      {
        return null;
      }
      obj = task.Gate;
      lock (obj)
      {
        task.ThreadId = null;
      }
      string text = "Harness";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(90, 2);
      defaultInterpolatedStringHandler.AppendLiteral("[harness] ");
      defaultInterpolatedStringHandler.AppendFormatted(AgentService.Lock<string>(task, () => task.ClickyName));
      defaultInterpolatedStringHandler.AppendLiteral("'s conversation ");
      defaultInterpolatedStringHandler.AppendFormatted(threadId);
      defaultInterpolatedStringHandler.AppendLiteral(" ran on another harness: a fresh conversation takes this message");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      return threadId;
    }

    // Token: 0x06000261 RID: 609 RVA: 0x000E0F8C File Offset: 0x000E0F8C
    public string JobContinuationContext(ClickyJob job, bool hasPreviousNote = false)
    {
      string text = this.JobConversationSoFar(job, 24000);
      string text2 = (hasPreviousNote ? ".clicky/previous-note.md has the page the last run left, and the folder has everything made earlier." : "The folder has everything made earlier.");
      ClickyJobOutcome outcome = job.Outcome;
      string text3;
      if (outcome != ClickyJobOutcome.Failed)
      {
        if (outcome != ClickyJobOutcome.Stopped)
        {
          text3 = "";
        }
        else
        {
          text3 = " The user stopped the last run partway through.";
        }
      }
      else
      {
        text3 = " The last run failed partway; carry on from what it got done.";
      }
      string text4 = text3;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(286, 4);
      defaultInterpolatedStringHandler.AppendLiteral("This message continues work done earlier in this folder. You're picking it up fresh; here is the conversation so far, oldest first:\n<earlier_conversation>\n");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("\n</earlier_conversation>\n");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral(" Open an earlier file only when this message needs it.");
      defaultInterpolatedStringHandler.AppendFormatted(text4);
      defaultInterpolatedStringHandler.AppendLiteral(" Keep what was done; change what the user asks now. ");
      defaultInterpolatedStringHandler.AppendFormatted("Answer in the language of this newest message.");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06000262 RID: 610 RVA: 0x000E104C File Offset: 0x000E104C
    public string JobConversationSoFar(ClickyJob job, int characterLimit = 24000)
    {
      List<string> list = this.CachedConversationExchanges(job.AllThreadIds);
      if (list.Count == 0)
      {
        list = (job.Runs ?? Array.Empty<ClickyJobRun>()).Select<ClickyJobRun, string>((ClickyJobRun run) => "They asked: " + run.Ask + "\nIt ended: " + (run.Summary ?? run.Outcome.ToString().ToLowerInvariant())).ToList<string>();
        string replyText = job.ReplyText;
        string text = ((replyText != null) ? replyText.Trim() : null);
        if (text != null && text.Length > 0)
        {
          list.Add("Your last reply, in full:\n" + text);
        }
      }
      return AgentService.NewestExchanges(list, characterLimit);
    }

    // Token: 0x06000263 RID: 611 RVA: 0x000E10E0 File Offset: 0x000E10E0
    private List<string> CachedConversationExchanges(IReadOnlyList<string> threadIds)
    {
      List<string> list = new List<string>();
      foreach (string text in threadIds)
      {
        IReadOnlyList<AgentTranscriptTurn> readOnlyList;
        try
        {
          Func<string, IReadOnlyList<AgentTranscriptTurn>> cachedTranscript = this.CachedTranscript;
          readOnlyList = ((cachedTranscript != null) ? cachedTranscript(text) : null);
        }
        catch (Exception)
        {
          readOnlyList = null;
        }
        if (readOnlyList == null || readOnlyList.Count <= 0)
        {
          IReadOnlyList<AgentTranscriptTurn> readOnlyList2;
          try
          {
            readOnlyList2 = this.HarnessRecordTranscript(text);
          }
          catch (Exception ex)
          {
            Log.Warn("Agents", "H1 record of " + text + ": " + ex.Message);
            readOnlyList2 = null;
          }
          object obj = this.RolloutTranscript(text) ?? Array.Empty<AgentTranscriptTurn>();
          IReadOnlyList<AgentTranscriptTurn> readOnlyList3 = readOnlyList2 ?? Array.Empty<AgentTranscriptTurn>();
          int num = 0;
          object obj2 = obj;
          AgentTranscriptTurn[] array = new AgentTranscriptTurn[((IReadOnlyCollection<AgentTranscriptTurn>)obj2).Count + readOnlyList3.Count];
          foreach (AgentTranscriptTurn agentTranscriptTurn in ((IEnumerable<AgentTranscriptTurn>)obj2))
          {
            array[num] = agentTranscriptTurn;
            num++;
          }
          foreach (AgentTranscriptTurn agentTranscriptTurn in readOnlyList3)
          {
            array[num] = agentTranscriptTurn;
            num++;
          }
          readOnlyList = new <>z__ReadOnlyArray<AgentTranscriptTurn>(array);
        }
        foreach (AgentTranscriptTurn agentTranscriptTurn2 in (readOnlyList ?? Array.Empty<AgentTranscriptTurn>()))
        {
          string text2 = string.Join("\n", from m in agentTranscriptTurn2.UserMessages
            select AgentService.DisplayUserText(m).Trim() into m
            where m.Length > 0
            select m);
          string text3 = string.Join("\n\n", from m in agentTranscriptTurn2.AssistantMessages
            select m.Trim() into m
            where m.Length > 0
            select m);
          if (text2.Length != 0 || text3.Length != 0)
          {
            list.Add("They asked: " + ((text2.Length == 0) ? "(nothing new)" : text2) + "\nYou replied:\n" + ((text3.Length == 0) ? "(no reply)" : text3));
          }
        }
      }
      return list;
    }

    // Token: 0x06000264 RID: 612 RVA: 0x000E13F8 File Offset: 0x000E13F8
    [return: Nullable(new byte[] { 2, 1 })]
    private IReadOnlyList<AgentTranscriptTurn> RolloutTranscript(string threadId)
    {
      if (AgentHarnessPreferences.IsHarnessThread(threadId))
      {
        return null;
      }
      IReadOnlyList<AgentTranscriptTurn> readOnlyList;
      try
      {
        readOnlyList = this.RolloutTranscriptTurns(threadId);
      }
      catch (Exception ex)
      {
        Log.Warn("Agents", "rollout of " + threadId + ": " + ex.Message);
        readOnlyList = null;
      }
      return readOnlyList;
    }

    // Token: 0x06000265 RID: 613 RVA: 0x000E1450 File Offset: 0x000E1450
    public static string NewestExchanges(IReadOnlyList<string> exchanges, int characterLimit)
    {
      List<string> list = new List<string>();
      int num = 0;
      foreach (string text in exchanges.Reverse<string>())
      {
        int num2 = characterLimit - num;
        if (num2 <= 400)
        {
          break;
        }
        string text2 = ((text.Length > num2) ? (text.Substring(0, num2) + "…") : text);
        list.Insert(0, text2);
        num += text2.Length;
      }
      if (list.Count != 0)
      {
        return string.Join("\n\n", list);
      }
      return "(nothing recorded)";
    }

    // Token: 0x06000266 RID: 614 RVA: 0x000E14FC File Offset: 0x000E14FC
    public string ClickyFreshConversationContext(string oldThreadId)
    {
      string text = AgentService.NewestExchanges(this.CachedConversationExchanges(new <>z__ReadOnlySingleElementList<string>(oldThreadId)), 24000);
      return "This message continues your earlier conversation with the user, which ran in another agent setup. You're picking it up fresh; here is the conversation so far, oldest first:\n<earlier_conversation>\n" + text + "\n</earlier_conversation>\nYour workspace (the working folder) has AGENTS.md with your notes and everything you made earlier; read AGENTS.md first, and open an earlier file only when this message needs it. Keep what was done; change what the user asks now. Answer in the language of this newest message.";
    }

    // Token: 0x06000267 RID: 615 RVA: 0x000E1530 File Offset: 0x000E1530
    private static string LiveNotePathForJob(ClickyJob job)
    {
      return Path.GetFullPath(Path.Combine(job.FolderPath, "note.md"));
    }

    // Token: 0x06000268 RID: 616 RVA: 0x000E1548 File Offset: 0x000E1548
    public bool RetireClickysNoteForMovedRoutineJob(ClickyJob job)
    {
      string text = AgentService.LiveNotePathForJob(job);
      bool flag = ClickyWorkFolderFiles.RetireNote(job.FolderPath);
      try
      {
        IClickyLiveNoteTurns liveNotes = this.LiveNotes;
        if (liveNotes != null)
        {
          liveNotes.Forget(text);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Routines", "moved routine's note versions not forgotten: " + ex.Message);
      }
      this.LiveSteps.Clear(text);
      if (flag)
      {
        Log.Info("Routines", "job " + job.Id + ": its Clicky's note.md set aside to .clicky\\previous-note.md");
      }
      return flag;
    }

    // Token: 0x06000269 RID: 617 RVA: 0x000E15D8 File Offset: 0x000E15D8
    private void NoteJobTaskEnded(AgentTask task, ClickyJobOutcome outcome, [Nullable(2)] string summary)
    {
      this.NoteLiveNoteTaskEnded(task);
      object gate = task.Gate;
      string jobId;
      bool requiresExtraUsageApproval;
      string text;
      string summary2;
      string failureMessage;
      IReadOnlyList<string> artifacts;
      ClickyJobUserStep pendingUserStep;
      IReadOnlyList<string> learnedHowTos;
      string threadId;
      lock (gate)
      {
        jobId = task.JobId;
        requiresExtraUsageApproval = task.RequiresExtraUsageApproval;
        string finalResponseMarkdown = task.FinalResponseMarkdown;
        text = ((finalResponseMarkdown != null) ? finalResponseMarkdown.Trim() : null);
        summary2 = task.Summary;
        failureMessage = task.FailureMessage;
        artifacts = task.Artifacts;
        pendingUserStep = task.PendingUserStep;
        learnedHowTos = task.LearnedHowTos;
        threadId = task.ThreadId;
      }
      if (jobId != null)
      {
        ClickyJob clickyJob = this.Jobs.Job(jobId);
        if (clickyJob != null)
        {
          if (outcome == ClickyJobOutcome.Failed && requiresExtraUsageApproval)
          {
            return;
          }
          string text2 = ((outcome == ClickyJobOutcome.Done) ? summary2 : failureMessage) ?? summary;
          this.Jobs.MarkEnded(clickyJob.Id, outcome, text2, string.IsNullOrEmpty(text) ? summary : text, artifacts);
          try
          {
            this.AppendJobReport(this.Jobs.Job(clickyJob.Id) ?? clickyJob, task, outcome, text2);
          }
          catch (Exception ex)
          {
            Log.Warn("Jobs", "report.md for " + clickyJob.Id + ": " + ex.Message);
          }
          this.Jobs.SetWaitingStep(clickyJob.Id, (outcome == ClickyJobOutcome.Done) ? pendingUserStep : null);
          ClickyJobStepWatcher clickyJobStepWatcher = this.StepWatcher;
          if (outcome == ClickyJobOutcome.Done)
          {
            this.LearnedHowTos.Record(learnedHowTos, clickyJob.Id);
          }
          string text3 = "ended";
          string text4 = threadId;
          Dictionary<string, object> dictionary = new Dictionary<string, object>();
          dictionary["outcome"] = outcome.ToString().ToLowerInvariant();
          dictionary["artifact_count"] = artifacts.Count;
          this.RaiseJobAction(text3, text4, dictionary);
          return;
        }
      }
    }

    // Token: 0x14000010 RID: 16
    // (add) Token: 0x0600026A RID: 618 RVA: 0x000E179C File Offset: 0x000E179C
    // (remove) Token: 0x0600026B RID: 619 RVA: 0x000E17D4 File Offset: 0x000E17D4
    [Nullable(new byte[] { 2, 1, 2, 1, 1, 2 })]
    [field: Nullable(new byte[] { 2, 1, 2, 1, 1, 2 })]
    public event Action<string, string, IReadOnlyDictionary<string, object>> JobAction;

    // Token: 0x0600026C RID: 620 RVA: 0x000E180C File Offset: 0x000E180C
    public void TrackJobAction(string action, [Nullable(2)] string threadId = null, [Nullable(new byte[] { 2, 1, 2 })] IReadOnlyDictionary<string, object> properties = null)
    {
      this.RaiseJobAction(action, threadId, properties);
    }

    // Token: 0x0600026D RID: 621 RVA: 0x000E1818 File Offset: 0x000E1818
    private void RaiseJobAction(string action, [Nullable(2)] string threadId, [Nullable(new byte[] { 2, 1, 2 })] IReadOnlyDictionary<string, object> properties = null)
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>(properties ?? new Dictionary<string, object>());
      dictionary["action"] = action;
      Dictionary<string, object> dictionary2 = dictionary;
      try
      {
        Action<string, string, IReadOnlyDictionary<string, object>> jobAction = this.JobAction;
        if (jobAction != null)
        {
          jobAction(action, threadId, dictionary2);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Jobs", "JobAction handler threw: " + ex.Message);
      }
    }

    // Token: 0x0600026E RID: 622 RVA: 0x000E1884 File Offset: 0x000E1884
    public void DismissJob(string jobId)
    {
      ClickyJob clickyJob = this.Jobs.Job(jobId);
      if (clickyJob == null)
      {
        return;
      }
      this.Jobs.Dismiss(jobId);
      this.RaiseJobAction("dismissed", clickyJob.ThreadId, null);
    }

    // Token: 0x0600026F RID: 623 RVA: 0x000E18C0 File Offset: 0x000E18C0
    public void NoteJobCardClosed(string jobId)
    {
      ClickyJob clickyJob = this.Jobs.Job(jobId);
      if (clickyJob != null && clickyJob.IsFinished && clickyJob.IsQuickActionWithoutOutput)
      {
        this.Jobs.MarkSeen(jobId);
      }
    }

    // Token: 0x06000270 RID: 624 RVA: 0x000E18FC File Offset: 0x000E18FC
    public void PinJob(string jobId)
    {
      ClickyJob clickyJob = this.Jobs.Job(jobId);
      if (clickyJob == null)
      {
        return;
      }
      this.Jobs.KeepOnDock(jobId);
      this.RaiseJobAction("pinned", clickyJob.ThreadId, null);
    }

    // Token: 0x06000271 RID: 625 RVA: 0x000E1938 File Offset: 0x000E1938
    public async Task StopJobAsync(string jobId)
    {
      ClickyJob job = this.Jobs.Job(jobId);
      if (job != null)
      {
        AgentTask agentTask = this.LiveTask(job);
        if (agentTask != null)
        {
          await this.StopAsync(agentTask.Id).ConfigureAwait(false);
          this.RaiseJobAction("stopped", job.ThreadId, null);
        }
      }
    }

    // Token: 0x06000272 RID: 626 RVA: 0x000E1984 File Offset: 0x000E1984
    public void ContinueJobAfterUserStep(string jobId, [Nullable(2)] string reachedAddress = null)
    {
      ClickyJob clickyJob = this.Jobs.Job(jobId);
      if (clickyJob != null)
      {
        ClickyJobUserStep waitingStep = clickyJob.WaitingStep;
        if (waitingStep != null)
        {
          string threadId = clickyJob.ThreadId;
          if (threadId != null)
          {
            string text;
            if (reachedAddress == null)
            {
              text = "The user tapped I'm done on the step you handed them: \"" + waitingStep.Step + "\". Check it went through before carrying on.";
            }
            else
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(183, 2);
              defaultInterpolatedStringHandler.AppendLiteral("HeyClicky saw the user's browser reach ");
              defaultInterpolatedStringHandler.AppendFormatted(reachedAddress);
              defaultInterpolatedStringHandler.AppendLiteral(", which you said means this step is done: \"");
              defaultInterpolatedStringHandler.AppendFormatted(waitingStep.Step);
              defaultInterpolatedStringHandler.AppendLiteral("\". Check it really went through (look at the page) before carrying on; if it didn't, say what's left.");
              text = defaultInterpolatedStringHandler.ToStringAndClear();
            }
            string text2 = text;
            this.LaunchAsync(new AgentLaunchRequest
            {
              Prompt = "Done: " + waitingStep.Step,
              JobId = clickyJob.Id,
              TargetThreadId = threadId,
              LaunchContext = text2,
              LaunchSource = "text",
              IsHudFollowUp = true
            }, default(CancellationToken));
            string text3 = "user_step_done";
            string text4 = threadId;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary["via"] = ((reachedAddress == null) ? "button" : "browser_page");
            this.RaiseJobAction(text3, text4, dictionary);
            return;
          }
        }
      }
    }

    // Token: 0x06000273 RID: 627 RVA: 0x000E1AA4 File Offset: 0x000E1AA4
    [NullableContext(2)]
    public unsafe string JobsContextNoteForBrainTurn(ISet<Guid> liveSuggestionCardIds = null, [Nullable(new byte[] { 2, 1, 2 })] Func<string, string> routineLine = null, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> extraLines = null)
    {
      if (!ClickyJobsPreferences.IsEnabled)
      {
        return null;
      }
      List<string> list = ClickyJobsNoteSelection.Select(this.Jobs.Jobs, liveSuggestionCardIds, new Func<ClickyJob, ClickyJobRailPhase>(this.RailPhase), (ClickyJob job) => base.<JobsContextNoteForBrainTurn>g__Repeats|0(job) != null).Select<ClickyJob, string>(delegate(ClickyJob job)
      {
        if (job.IsWaitingToStart)
        {
          return "- \"" + job.Title + "\" — OFFERED, NOT STARTED: it waits on the dock until they click it. When they say yes to it, sessions_spawn with this exact title as the label: that starts this one, never a second job beside it.";
        }
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
        if (job.IsHeldForChrome)
        {
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(112, 2);
          defaultInterpolatedStringHandler.AppendLiteral("- \"");
          defaultInterpolatedStringHandler.AppendFormatted(job.Title);
          defaultInterpolatedStringHandler.AppendLiteral("\" — job_id: ");
          defaultInterpolatedStringHandler.AppendFormatted(job.Id);
          defaultInterpolatedStringHandler.AppendLiteral(", not started, WAITING ON THE USER to choose about HeyClicky's Chrome extension before it starts ");
          return defaultInterpolatedStringHandler.ToStringAndClear() + "(their answer → sessions_answer_request, request chrome_setup, no job_id: " + this.ChromeAnswerMeanings() + ", decline = it starts now without it)";
        }
        string text2;
        switch (this.RailPhase(job))
        {
        case ClickyJobRailPhase.Queued:
          text2 = "STARTING";
          break;
        case ClickyJobRailPhase.Working:
          text2 = "WORKING NOW";
          break;
        case ClickyJobRailPhase.NeedsYou:
        {
          AgentTask live = this.LiveTask(job);
          AgentTask live2 = live;
          string text3 = ((live2 != null) ? live2.PendingComputerUseRequest : null);
          if (text3 != null)
          {
            text2 = "WAITING ON THE USER to let you drive one step on their PC: " + AgentService.Cap(text3, 140) + " (their yes or no → sessions_answer_request, request computer_use, this job_id)";
          }
          else if (live != null && AgentService.Lock<bool>(live, () => live.RequiresExtraUsageApproval))
          {
            text2 = "WAITING ON THE USER for more agent messages (their yes or no → sessions_answer_request, request extra_usage, this job_id)";
          }
          else if (live != null && AgentService.Lock<bool>(live, () => live.NeedsChromeConnection))
          {
            text2 = "WAITING ON THE USER to choose about HeyClicky's Chrome extension, which it needs for a website (their answer → sessions_answer_request, request chrome_setup, this job_id: " + this.ChromeAnswerMeanings() + ", decline = it goes on without it; it carries on by itself once Chrome is connected)";
          }
          else
          {
            string text4 = "WAITING ON THE USER";
            ClickyJobUserStep waitingStep = job.WaitingStep;
            text2 = text4 + ((waitingStep != null) ? (" to: " + AgentService.Cap(waitingStep.Step, 140) + " (when they say it's done, sessions_send it; " + ((waitingStep.DoneWhenUrlContains == null) ? "it waits for them)" : "it also continues by itself once their browser shows it's done)")) : "");
          }
          break;
        }
        case ClickyJobRailPhase.Done:
        {
          string text5 = (job.IsSeen ? "done" : "done, not opened yet");
          string summary = job.Summary;
          text2 = text5 + ((summary != null) ? (": " + AgentService.Cap(summary, 140)) : "");
          break;
        }
        case ClickyJobRailPhase.Failed:
          text2 = "failed";
          break;
        default:
          text2 = "stopped";
          break;
        }
        string text6 = base.<JobsContextNoteForBrainTurn>g__Repeats|0(job);
        string text7 = ((text6 != null) ? (", " + text6) : "");
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 4);
        defaultInterpolatedStringHandler.AppendLiteral("- \"");
        defaultInterpolatedStringHandler.AppendFormatted(job.Title);
        defaultInterpolatedStringHandler.AppendLiteral("\" — job_id: ");
        defaultInterpolatedStringHandler.AppendFormatted(job.Id);
        defaultInterpolatedStringHandler.AppendLiteral(", ");
        defaultInterpolatedStringHandler.AppendFormatted(text2);
        defaultInterpolatedStringHandler.AppendFormatted(text7);
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }).ToList<string>();
      string text = "[Your jobs] The jobs that need attention now (working, waiting on them, offered, not opened yet, repeating) and the six most recent others, oldest first. Address a job by its job_id; find_jobs finds any other job by what it was about. A job that repeats names its routine_id: its schedule is the routines tool's, never sessions_send.";
      list.AddRange(extraLines ?? Array.Empty<string>());
      if (list.Count == 0)
      {
        int num = 1;
        List<string> list2 = new List<string>(num);
        CollectionsMarshal.SetCount<string>(list2, num);
        Span<string> span = CollectionsMarshal.AsSpan<string>(list2);
        int num2 = 0;
        *span[num2] = "(nothing needs attention; find_jobs finds earlier work)";
        list = list2;
      }
      return text + "\n" + string.Join("\n", list);
    }

    // Token: 0x06000274 RID: 628 RVA: 0x000E1B6C File Offset: 0x000E1B6C
    private static string Cap(string s, int n)
    {
      if (s.Length <= n)
      {
        return s;
      }
      return s.Substring(0, n);
    }

    // Token: 0x06000275 RID: 629 RVA: 0x000E1B84 File Offset: 0x000E1B84
    [NullableContext(2)]
    public string WorkContextNoteForBrainTurn(ISet<Guid> liveSuggestionCardIds = null)
    {
      if (!ClickyJobsPreferences.IsEnabled)
      {
        return null;
      }
      string text = this.JobsContextNoteForBrainTurn(liveSuggestionCardIds, new Func<string, string>(this.routines.JobRoutineLine), this.EarlierClickyJobLinesForBrain());
      return string.Join("\n\n", new string[]
      {
        text,
        this.RoutinesTool.ContextNote()
      }.OfType<string>());
    }

    // Token: 0x06000276 RID: 630 RVA: 0x000E1BE0 File Offset: 0x000E1BE0
    private IReadOnlyList<string> EarlierClickyJobLinesForBrain()
    {
      return this.clickys.Clickys.Select<ClickyInfo, string>(delegate(ClickyInfo clicky)
      {
        string threadId = clicky.ThreadId;
        if (threadId == null)
        {
          return null;
        }
        AgentTask live = this.tasks.Values.LastOrDefault<AgentTask>((AgentTask t) => AgentService.Lock<bool>(t, () => !t.IsDemo && (t.ClickySlug == clicky.Slug || t.ThreadId == threadId)));
        AgentTask live2 = live;
        string text = ((live2 != null) ? live2.PendingComputerUseRequest : null);
        string text2;
        if (text != null)
        {
          text2 = "WAITING ON THE USER to let you drive one step on their PC: " + AgentService.Cap(text, 140) + " (their yes or no → sessions_answer_request, request computer_use, this job_id)";
        }
        else if (live != null && AgentService.Lock<bool>(live, () => live.RequiresExtraUsageApproval))
        {
          text2 = "WAITING ON THE USER for more agent messages (their yes or no → sessions_answer_request, request extra_usage, this job_id)";
        }
        else
        {
          if (live == null || !AgentService.Lock<bool>(live, () => live.IsActive))
          {
            return null;
          }
          string text3 = "WORKING NOW";
          string text4 = AgentService.LiveWorkDescription(live);
          text2 = text3 + ((text4 != null) ? (": " + text4) : "");
        }
        ClickyRoutine clickyRoutine = this.routines.ForClicky(clicky.Slug).FirstOrDefault<ClickyRoutine>();
        string text5 = ((clickyRoutine != null) ? (", repeats (routine_id " + clickyRoutine.Id + ")") : "");
        string text6 = clicky.OneLiner.Trim();
        string[] array = new string[5];
        array[0] = "- \"";
        array[1] = clicky.Name;
        array[2] = "\"";
        array[3] = ((text6.Length == 0) ? "" : (" (" + text6 + ")"));
        int num = 4;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 3);
        defaultInterpolatedStringHandler.AppendLiteral(" — job_id: ");
        defaultInterpolatedStringHandler.AppendFormatted(threadId);
        defaultInterpolatedStringHandler.AppendLiteral(", ");
        defaultInterpolatedStringHandler.AppendFormatted(text2);
        defaultInterpolatedStringHandler.AppendFormatted(text5);
        array[num] = defaultInterpolatedStringHandler.ToStringAndClear();
        return string.Concat(array);
      }).OfType<string>().ToList<string>();
    }

    // Token: 0x06000277 RID: 631 RVA: 0x000E1C08 File Offset: 0x000E1C08
    [return: Nullable(2)]
    private static string LiveWorkDescription(AgentTask task)
    {
      object gate = task.Gate;
      string text3;
      lock (gate)
      {
        List<string> list = new List<string>();
        string text = AgentService.<LiveWorkDescription>g__Clipped|400_0(task.UserPrompts.LastOrDefault<string>() ?? "", 140);
        if (text.Length > 0)
        {
          list.Add("on \"" + text + "\"");
        }
        int num = (int)(DateTimeOffset.Now - task.StartedAt).TotalMinutes;
        List<string> list2 = list;
        string text2;
        if (num >= 1)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
          defaultInterpolatedStringHandler.AppendLiteral("for ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(num);
          defaultInterpolatedStringHandler.AppendLiteral(" min");
          text2 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        else
        {
          text2 = "for under a minute";
        }
        list2.Add(text2);
        string latestCommentary = task.LatestCommentary;
        if (latestCommentary != null && latestCommentary.Length > 0)
        {
          list.Add("latest update \"" + AgentService.<LiveWorkDescription>g__Clipped|400_0(latestCommentary, 160) + "\"");
        }
        else if (task.Status == AgentTaskStatus.Starting)
        {
          list.Add("waiting to start");
        }
        text3 = string.Join(", ", list);
      }
      return text3;
    }

    // Token: 0x06000278 RID: 632 RVA: 0x000E1D4C File Offset: 0x000E1D4C
    private void SyncPendingRequest(AgentTask task)
    {
      ClickyPendingAgentRequest clickyPendingAgentRequest = null;
      object gate = task.Gate;
      string threadId;
      lock (gate)
      {
        threadId = task.ThreadId;
        if (threadId == null || task.IsDemo)
        {
          return;
        }
        ClickyPendingAgentRequest.RequestKind? requestKind = ((task.PendingComputerUseRequest != null) ? new ClickyPendingAgentRequest.RequestKind?(ClickyPendingAgentRequest.RequestKind.ComputerUse) : (task.RequiresExtraUsageApproval ? new ClickyPendingAgentRequest.RequestKind?(ClickyPendingAgentRequest.RequestKind.ExtraUsage) : null));
        if (requestKind != null)
        {
          ClickyPendingAgentRequest.RequestKind valueOrDefault = requestKind.GetValueOrDefault();
          ClickyPendingAgentRequest clickyPendingAgentRequest2 = new ClickyPendingAgentRequest();
          clickyPendingAgentRequest2.ThreadId = threadId;
          clickyPendingAgentRequest2.Kind = valueOrDefault;
          clickyPendingAgentRequest2.RequestText = task.PendingComputerUseRequest ?? "";
          AgentTurnLease lease = task.Lease;
          clickyPendingAgentRequest2.LeaseId = ((lease != null) ? lease.LeaseId : null);
          AgentTurnLease lease2 = task.Lease;
          clickyPendingAgentRequest2.LeaseTurnId = ((lease2 != null) ? lease2.TurnId : null);
          clickyPendingAgentRequest2.ClickySlug = task.ClickySlug;
          clickyPendingAgentRequest2.JobId = task.JobId;
          clickyPendingAgentRequest2.Title = task.DoneTitle ?? task.Title;
          clickyPendingAgentRequest2.Prompt = AgentService.Cap(task.UserPrompts.LastOrDefault<string>() ?? "", 4000);
          clickyPendingAgentRequest2.FinalSummary = task.Summary;
          clickyPendingAgentRequest2.AskedAt = DateTimeOffset.Now;
          clickyPendingAgentRequest = clickyPendingAgentRequest2;
        }
      }
      ClickyPendingAgentRequestStore.Sync(clickyPendingAgentRequest, threadId);
    }

    // Token: 0x06000279 RID: 633 RVA: 0x000E1EB8 File Offset: 0x000E1EB8
    public void RestorePendingAgentRequestsFromLastRun()
    {
      IReadOnlyList<ClickyPendingAgentRequest> readOnlyList = ClickyPendingAgentRequestStore.All();
      if (readOnlyList.Count == 0)
      {
        return;
      }
      bool flag = false;
      using (IEnumerator<ClickyPendingAgentRequest> enumerator = readOnlyList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ClickyPendingAgentRequest record = enumerator.Current;
          if (!this.tasks.Values.Any<AgentTask>((AgentTask t) => AgentService.Lock<string>(t, () => t.ThreadId) == record.ThreadId))
          {
            AgentTask agentTask = new AgentTask
            {
              ThreadId = record.ThreadId,
              StartedAt = record.AskedAt,
              Status = ((record.Kind == ClickyPendingAgentRequest.RequestKind.ComputerUse) ? AgentTaskStatus.Finished : AgentTaskStatus.WaitingForUser),
              ProgressSummary = ""
            };
            object gate = agentTask.Gate;
            lock (gate)
            {
              agentTask.ClickySlug = record.ClickySlug;
              AgentTask agentTask2 = agentTask;
              string clickySlug = record.ClickySlug;
              string text;
              if (clickySlug == null)
              {
                text = null;
              }
              else
              {
                ClickyInfo clickyInfo = this.clickys.Get(clickySlug);
                text = ((clickyInfo != null) ? clickyInfo.Name : null);
              }
              agentTask2.ClickyName = text;
              agentTask.JobId = record.JobId;
              agentTask.Title = record.Title ?? agentTask.Title;
              agentTask.Summary = record.FinalSummary;
              agentTask.UserPrompts.Add(record.Prompt);
              string leaseId = record.LeaseId;
              if (leaseId != null)
              {
                agentTask.Lease = new AgentTurnLease(leaseId, record.LeaseTurnId ?? "", null, 0, 0, null, null, record.Kind == ClickyPendingAgentRequest.RequestKind.ExtraUsage, null);
              }
              if (record.Kind == ClickyPendingAgentRequest.RequestKind.ComputerUse)
              {
                agentTask.PendingComputerUseRequest = record.RequestText;
              }
              else
              {
                agentTask.RequiresExtraUsageApproval = true;
              }
              AgentTask agentTask3 = agentTask;
              string jobId = record.JobId;
              string text2;
              if (jobId == null)
              {
                string clickySlug2 = record.ClickySlug;
                text2 = ((clickySlug2 != null) ? this.clickys.WorkspacePath(clickySlug2) : null);
              }
              else
              {
                ClickyJob clickyJob = this.Jobs.Job(jobId);
                text2 = ((clickyJob != null) ? clickyJob.FolderPath : null);
              }
              agentTask3.WorkspacePath = text2;
            }
            this.tasks[agentTask.Id] = agentTask;
            flag = true;
          }
        }
      }
      if (!flag)
      {
        return;
      }
      string text3 = "Agents";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(66, 1);
      defaultInterpolatedStringHandler.AppendLiteral("[pending requests] restored ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(readOnlyList.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" waiting question(s) from the last run");
      Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
      this.RaiseTasksChanged();
    }

    // Token: 0x14000011 RID: 17
    // (add) Token: 0x0600027A RID: 634 RVA: 0x000E21AC File Offset: 0x000E21AC
    // (remove) Token: 0x0600027B RID: 635 RVA: 0x000E21E4 File Offset: 0x000E21E4
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<string> ResultCardShown;

    // Token: 0x0600027C RID: 636 RVA: 0x000E221C File Offset: 0x000E221C
    [return: Nullable(2)]
    public string FullReplyText(string taskId)
    {
      AgentTask agentTask;
      if (!this.tasks.TryGetValue(taskId, out agentTask))
      {
        return null;
      }
      object gate = agentTask.Gate;
      List<string> list;
      lock (gate)
      {
        list = agentTask.CompletedReplies.Select<string, string>((string r) => StructuredFinalReply.Parse(r).Text).ToList<string>();
        string finalResponseMarkdown = agentTask.FinalResponseMarkdown;
        if (finalResponseMarkdown != null)
        {
          string text = finalResponseMarkdown.Trim();
          string text2 = list.LastOrDefault<string>();
          if (text != ((text2 != null) ? text2.Trim() : null))
          {
            list.Add(finalResponseMarkdown);
          }
        }
      }
      string text3 = string.Join("\n\n", from m in list
        select m.Trim() into m
        where m.Length > 0
        select m);
      if (text3.Length == 0)
      {
        return null;
      }
      if (text3.Length <= 40000)
      {
        return text3;
      }
      return text3.Substring(0, 39999) + "…";
    }

    // Token: 0x0600027D RID: 637 RVA: 0x000E2354 File Offset: 0x000E2354
    [return: Nullable(2)]
    public string ResultReport(string taskId)
    {
      AgentTaskDetail taskDetail = this.GetTaskDetail(taskId);
      if (taskDetail == null)
      {
        return null;
      }
      List<string> list = new List<string>();
      ClickyAnswer answer = taskDetail.Answer;
      string text = ((answer != null) ? answer.ReportJson : null);
      if (text != null)
      {
        list.Add("Its facts, to build their card from: " + text);
      }
      string text2 = this.FullReplyText(taskId);
      if (text2 != null)
      {
        string text3 = ((text2.Length > 6000) ? (text2.Substring(0, 6000) + "… (cut here; clicky_memory has the rest)") : text2);
        list.Add("Its full report, which they can open from the chat:\n<report>\n" + text3 + "\n</report>");
      }
      if (taskDetail.Artifacts.Count > 0)
      {
        list.Add("Files it made: " + string.Join(", ", taskDetail.Artifacts.Take<string>(6)));
      }
      if (list.Count != 0)
      {
        return string.Join("\n", list);
      }
      return null;
    }

    // Token: 0x0600027E RID: 638 RVA: 0x000E2430 File Offset: 0x000E2430
    public bool NoteResultCard(string threadId, ClickyAnswer card)
    {
      if (this.tasks.Values.FirstOrDefault<AgentTask>((AgentTask t) => AgentService.Lock<string>(t, () => t.ThreadId) == threadId && AgentService.Lock<AgentTaskStatus>(t, () => t.Status) == AgentTaskStatus.Finished) == null)
      {
        return false;
      }
      this.resultCards[threadId] = card;
      ClickyJob clickyJob = this.Jobs.JobForThread(threadId);
      if (clickyJob != null)
      {
        this.Jobs.SetResultCard(clickyJob.Id, card);
      }
      try
      {
        Action<string> resultCardShown = this.ResultCardShown;
        if (resultCardShown != null)
        {
          resultCardShown(threadId);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Jobs", "ResultCardShown handler threw: " + ex.Message);
      }
      return true;
    }

    // Token: 0x0600027F RID: 639 RVA: 0x000E24EC File Offset: 0x000E24EC
    [return: Nullable(2)]
    public ClickyAnswer ResultCard(string threadId)
    {
      ClickyAnswer clickyAnswer;
      if (this.resultCards.TryGetValue(threadId, out clickyAnswer))
      {
        return clickyAnswer;
      }
      ClickyJob clickyJob = this.Jobs.JobForThread(threadId);
      if (clickyJob == null)
      {
        return null;
      }
      return clickyJob.ResultCard;
    }

    // Token: 0x06000280 RID: 640 RVA: 0x000E2524 File Offset: 0x000E2524
    private void NoteRunningTurnsForRestart()
    {
      List<ValueTuple<string, string, DateTimeOffset>> list = (from t in this.tasks.Values
        select AgentService.Lock<ValueTuple<AgentTaskStatus, bool, string, string, string, DateTimeOffset>>(t, () => new ValueTuple<AgentTaskStatus, bool, string, string, string, DateTimeOffset>(t.Status, t.IsDemo, t.JobId, t.ClickySlug, t.ThreadId, t.StartedAt)) into t
        where t.Item1 == AgentTaskStatus.Running && !t.Item2 && t.Item3 == null && t.Item4 != null
        select new ValueTuple<string, string, DateTimeOffset>(t.Item4, t.Item5, t.Item6)).ToList<ValueTuple<string, string, DateTimeOffset>>();
      this.RestartInterruptions.NoteRunning(list);
    }

    // Token: 0x06000281 RID: 641 RVA: 0x000E25BC File Offset: 0x000E25BC
    private void RemoveJobsForSignOut()
    {
      this.routines.RemoveAllJobRoutinesForSignOut();
      this.Jobs.RemoveAllForSignOut();
      this.Jobs.WorkFolderFiles.ResetForSignOut(this.JobsRootPath);
      ClickyHelperFiles.RemoveAllForSignOut();
      this.LearnedHowTos.RemoveAllForSignOut();
      ClickyPendingAgentRequestStore.RemoveAllForSignOut();
      this.RestartInterruptions.Reset();
    }

    // Token: 0x06000282 RID: 642 RVA: 0x000E2618 File Offset: 0x000E2618
    public IReadOnlyList<string> DeleteAllLocalClickys()
    {
      if (this.tasks.Values.Any<AgentTask>((AgentTask t) => AgentService.Lock<bool>(t, () => t.IsActive)))
      {
        throw new InvalidOperationException("Stop the running agents first.");
      }
      IReadOnlyList<string> readOnlyList = this.clickys.DeleteAll();
      foreach (ClickyRoutine clickyRoutine in this.routines.Routines)
      {
        this.routines.Delete(clickyRoutine.Id, "debug_delete_all");
      }
      foreach (AgentTask agentTask in this.tasks.Values.Where<AgentTask>((AgentTask t) => t.ClickySlug != null).ToList<AgentTask>())
      {
        AgentTask agentTask2;
        this.tasks.TryRemove(agentTask.Id, out agentTask2);
      }
      this.RaiseTasksChanged();
      string text = "Agents";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(59, 1);
      defaultInterpolatedStringHandler.AppendLiteral("deleted all local Clickys (");
      defaultInterpolatedStringHandler.AppendFormatted<int>(readOnlyList.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" workspace folder(s) to recycle)");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      return readOnlyList;
    }

    // Token: 0x170000A2 RID: 162
    // (get) Token: 0x06000283 RID: 643 RVA: 0x000E2788 File Offset: 0x000E2788
    public string JobsRootPath
    {
      get
      {
        return Path.Combine(this.options.ProjectsRoot, "jobs");
      }
    }

    // Token: 0x170000A3 RID: 163
    // (get) Token: 0x06000284 RID: 644 RVA: 0x000E27A0 File Offset: 0x000E27A0
    private static string HarnessRecordsRoot
    {
      get
      {
        return Path.Combine(AgentHarnessRunnerProcess.StateDirectory, "h1-local");
      }
    }

    // Token: 0x06000285 RID: 645 RVA: 0x000E27B4 File Offset: 0x000E27B4
    [NullableContext(2)]
    public string SyncWorkOwnerStamp()
    {
      string text;
      try
      {
        Func<string> currentUserId = this.CurrentUserId;
        text = ClickyWorkIndex.OwnerStamp((currentUserId != null) ? currentUserId() : null);
      }
      catch (Exception)
      {
        text = null;
      }
      this.Jobs.WorkFolderFiles.OwnerStamp = text;
      return text;
    }

    // Token: 0x06000286 RID: 646 RVA: 0x000E2804 File Offset: 0x000E2804
    [NullableContext(2)]
    public void ForgetWorkReportsForDeletedAccount(string deletedAccountId)
    {
      this.Jobs.WorkFolderFiles.RemoveReports(deletedAccountId, this.JobsRootPath);
      this.Jobs.WorkFolderFiles.ResetForSignOut(this.JobsRootPath);
    }

    // Token: 0x06000287 RID: 647 RVA: 0x000E2834 File Offset: 0x000E2834
    private void AppendJobReport(ClickyJob job, AgentTask task, ClickyJobOutcome outcome, [Nullable(2)] string summary)
    {
      object gate = task.Gate;
      string text2;
      string text3;
      IReadOnlyList<string> artifacts;
      lock (gate)
      {
        string text;
        if ((text = task.TurnId) == null)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
          defaultInterpolatedStringHandler.AppendFormatted(task.Id);
          defaultInterpolatedStringHandler.AppendLiteral(":");
          defaultInterpolatedStringHandler.AppendFormatted<int>(task.UserPrompts.Count);
          text = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        text2 = text;
        text3 = StructuredFinalReply.Parse(task.FinalResponseMarkdown ?? "").Text;
        artifacts = task.Artifacts;
      }
      if (!this.Jobs.WorkFolderFiles.ClaimReport(job.Id, text2))
      {
        return;
      }
      this.SyncWorkOwnerStamp();
      DateTimeOffset now = DateTimeOffset.Now;
      string text4 = ClickyWorkIndex.ReportOutcomeText(outcome);
      IReadOnlyList<ClickyJobRun> runs = job.Runs;
      string text5;
      if (runs == null)
      {
        text5 = null;
      }
      else
      {
        ClickyJobRun clickyJobRun = runs.LastOrDefault<ClickyJobRun>();
        text5 = ((clickyJobRun != null) ? clickyJobRun.Ask : null);
      }
      string text6 = ClickyWorkIndex.ReportSection(now, text4, text5 ?? job.Prompt, text3, summary, artifacts, job.FolderPath);
      this.Jobs.WorkFolderFiles.AppendReportSection(text6, job.Title, job.Id, job.FolderPath);
    }

    // Token: 0x06000288 RID: 648 RVA: 0x000E2964 File Offset: 0x000E2964
    public void BackfillWorkReportsIfNeeded()
    {
      AgentService.<>c__DisplayClass422_0 CS$<>8__locals1 = new AgentService.<>c__DisplayClass422_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.ownerStamp = this.SyncWorkOwnerStamp();
      if (CS$<>8__locals1.ownerStamp == null)
      {
        return;
      }
      CS$<>8__locals1.flagKey = "clicky.work.reportsBackfilled.v2." + CS$<>8__locals1.ownerStamp;
      CS$<>8__locals1.files = this.Jobs.WorkFolderFiles;
      ClickyWorkFolderFiles files = CS$<>8__locals1.files;
      lock (files)
      {
        if (AgentDefaults.Bool(CS$<>8__locals1.flagKey) || CS$<>8__locals1.files.IsBackfillingReports)
        {
          return;
        }
        CS$<>8__locals1.files.IsBackfillingReports = true;
      }
      CS$<>8__locals1.jobsRootPath = ClickyWorkIndex.StandardizedPath(this.JobsRootPath);
      CS$<>8__locals1.jobs = this.Jobs.Jobs;
      CS$<>8__locals1.accountFolders = CS$<>8__locals1.jobs.Select<ClickyJob, string>((ClickyJob j) => ClickyWorkIndex.StandardizedPath(j.FolderPath)).ToHashSet<string>(StringComparer.OrdinalIgnoreCase);
      CS$<>8__locals1.files.StampFolders(CS$<>8__locals1.accountFolders.ToList<string>());
      Task.Run(delegate
      {
        AgentService.<>c__DisplayClass422_0.<<BackfillWorkReportsIfNeeded>b__1>d <<BackfillWorkReportsIfNeeded>b__1>d;
        <<BackfillWorkReportsIfNeeded>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<BackfillWorkReportsIfNeeded>b__1>d.<>4__this = CS$<>8__locals1;
        <<BackfillWorkReportsIfNeeded>b__1>d.<>1__state = -1;
        <<BackfillWorkReportsIfNeeded>b__1>d.<>t__builder.Start<AgentService.<>c__DisplayClass422_0.<<BackfillWorkReportsIfNeeded>b__1>d>(ref <<BackfillWorkReportsIfNeeded>b__1>d);
        return <<BackfillWorkReportsIfNeeded>b__1>d.<>t__builder.Task;
      });
    }

    // Token: 0x06000289 RID: 649 RVA: 0x000E2A98 File Offset: 0x000E2A98
    private unsafe IReadOnlyList<ClickyWorkEntry> ListedWorkEntries()
    {
      List<ClickyWorkEntry> list = this.Jobs.Jobs.Select<ClickyJob, ClickyWorkEntry>(delegate(ClickyJob job)
      {
        List<string> list3 = (from r in job.Runs ?? Array.Empty<ClickyJobRun>()
          select r.Ask into a
          where a.Length > 0
          select a).ToList<string>();
        ClickyWorkEntry clickyWorkEntry2 = new ClickyWorkEntry();
        clickyWorkEntry2.Id = job.Id;
        clickyWorkEntry2.Title = job.Title;
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
        clickyWorkEntry2.LastActive = dateTimeOffset2;
        IReadOnlyList<string> readOnlyList3;
        if (list3.Count != 0)
        {
          readOnlyList3 = list3;
        }
        else
        {
          int num = 1;
          List<string> list4 = new List<string>(num);
          CollectionsMarshal.SetCount<string>(list4, num);
          readOnlyList3 = list4;
          Span<string> span = CollectionsMarshal.AsSpan<string>(list4);
          int num2 = 0;
          *span[num2] = job.Prompt;
        }
        clickyWorkEntry2.Asks = readOnlyList3;
        clickyWorkEntry2.Result = job.Summary;
        clickyWorkEntry2.State = this.WorkStateText(job);
        clickyWorkEntry2.FolderPath = job.FolderPath;
        return clickyWorkEntry2;
      }).ToList<ClickyWorkEntry>();
      foreach (ClickyInfo clickyInfo in this.clickys.Clickys)
      {
        string threadId = clickyInfo.ThreadId;
        if (threadId != null)
        {
          List<ClickyWorkEntry> list2 = list;
          ClickyWorkEntry clickyWorkEntry = new ClickyWorkEntry();
          clickyWorkEntry.Id = threadId;
          clickyWorkEntry.Title = clickyInfo.Name;
          clickyWorkEntry.LastActive = this.ClickyLastActive(clickyInfo);
          IReadOnlyList<string> readOnlyList2;
          if (clickyInfo.OneLiner.Length != 0)
          {
            IReadOnlyList<string> readOnlyList = new <>z__ReadOnlySingleElementList<string>(clickyInfo.OneLiner);
            readOnlyList2 = readOnlyList;
          }
          else
          {
            IReadOnlyList<string> readOnlyList = Array.Empty<string>();
            readOnlyList2 = readOnlyList;
          }
          clickyWorkEntry.Asks = readOnlyList2;
          clickyWorkEntry.State = (this.IsClickyWorking(clickyInfo.Slug) ? "working now" : "done earlier");
          clickyWorkEntry.FolderPath = this.clickys.WorkspacePath(clickyInfo.Slug);
          clickyWorkEntry.IsEarlierClicky = true;
          list2.Add(clickyWorkEntry);
        }
      }
      return list;
    }

    // Token: 0x0600028A RID: 650 RVA: 0x000E2BB0 File Offset: 0x000E2BB0
    private DateTimeOffset ClickyLastActive(ClickyInfo clicky)
    {
      DateTimeOffset dateTimeOffset = (from t in this.tasks.Values
        where AgentService.Lock<bool>(t, () => t.ClickySlug == clicky.Slug || (clicky.ThreadId != null && t.ThreadId == clicky.ThreadId))
        select AgentService.Lock<DateTimeOffset>(t, () => t.StartedAt)).DefaultIfEmpty(DateTimeOffset.MinValue).Max<DateTimeOffset>();
      if (!(dateTimeOffset > clicky.CreatedAt))
      {
        return clicky.CreatedAt;
      }
      return dateTimeOffset;
    }

    // Token: 0x0600028B RID: 651 RVA: 0x000E2C3C File Offset: 0x000E2C3C
    private bool IsClickyWorking(string slug)
    {
      return this.tasks.Values.Any<AgentTask>((AgentTask t) => AgentService.Lock<bool>(t, () => t.ClickySlug == slug && t.IsActive));
    }

    // Token: 0x0600028C RID: 652 RVA: 0x000E2C74 File Offset: 0x000E2C74
    public string WorkStateText(ClickyJob job)
    {
      if (job.IsWaitingToStart)
      {
        return "offered, not started";
      }
      if (job.IsHeldForChrome)
      {
        return "waiting on their Chrome choice before it starts";
      }
      string text;
      switch (this.RailPhase(job))
      {
      case ClickyJobRailPhase.Queued:
        text = "starting";
        break;
      case ClickyJobRailPhase.Working:
        text = "working now";
        break;
      case ClickyJobRailPhase.NeedsYou:
        text = "waiting on the user";
        break;
      case ClickyJobRailPhase.Done:
        text = "done";
        break;
      case ClickyJobRailPhase.Failed:
        text = "failed";
        break;
      default:
        text = "stopped";
        break;
      }
      return text;
    }

    // Token: 0x0600028D RID: 653 RVA: 0x000E2CF0 File Offset: 0x000E2CF0
    public async Task<JsonObject> FindJobsAsync(string query, int limit, bool forHelper, [Nullable(2)] string callingThreadId = null)
    {
      AgentService.<>c__DisplayClass427_0 CS$<>8__locals1 = new AgentService.<>c__DisplayClass427_0();
      CS$<>8__locals1.query = query;
      CS$<>8__locals1.limit = limit;
      CS$<>8__locals1.forHelper = forHelper;
      CS$<>8__locals1.listed = this.ListedWorkEntries();
      CS$<>8__locals1.listedFolders = CS$<>8__locals1.listed.Select<ClickyWorkEntry, string>((ClickyWorkEntry e) => e.FolderPath).OfType<string>().ToList<string>();
      CS$<>8__locals1.jobsRootPath = this.JobsRootPath;
      AgentService.<>c__DisplayClass427_0 CS$<>8__locals2 = CS$<>8__locals1;
      string text;
      if (callingThreadId != null)
      {
        ClickyJob clickyJob = this.Jobs.JobForThread(callingThreadId);
        text = ((clickyJob != null) ? clickyJob.Id : null) ?? callingThreadId;
      }
      else
      {
        text = null;
      }
      CS$<>8__locals2.callingJobId = text;
      CS$<>8__locals1.ownerStamp = this.SyncWorkOwnerStamp();
      ConfiguredTaskAwaitable<IReadOnlyList<ClickyWorkEntry>>.ConfiguredTaskAwaiter configuredTaskAwaiter = Task.Run<IReadOnlyList<ClickyWorkEntry>>(() => ClickyWorkIndex.Search(ClickyWorkIndex.WithReports(CS$<>8__locals1.listed).Concat<ClickyWorkEntry>(ClickyWorkIndex.UnlistedFolderEntries(CS$<>8__locals1.jobsRootPath, CS$<>8__locals1.listedFolders, CS$<>8__locals1.ownerStamp)).ToList<ClickyWorkEntry>(), CS$<>8__locals1.query, CS$<>8__locals1.limit)).ConfigureAwait(false).GetAwaiter();
      if (!configuredTaskAwaiter.IsCompleted)
      {
        await configuredTaskAwaiter;
        ConfiguredTaskAwaitable<IReadOnlyList<ClickyWorkEntry>>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
        configuredTaskAwaiter = configuredTaskAwaiter2;
        configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<IReadOnlyList<ClickyWorkEntry>>.ConfiguredTaskAwaiter);
      }
      JsonArray jsonArray = new JsonArray((from m in configuredTaskAwaiter.GetResult()
        select ClickyWorkIndex.ResultObject(m, CS$<>8__locals1.forHelper, m.Id == CS$<>8__locals1.callingJobId)).ToArray<JsonNode>());
      string text2 = CS$<>8__locals1.query.Trim();
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["status"] = ((jsonArray.Count == 0) ? "none" : "found");
      jsonObject["jobs"] = jsonArray;
      JsonObject jsonObject2 = jsonObject;
      if (jsonArray.Count == 0)
      {
        jsonObject2["message"] = ((text2.Length == 0) ? "No earlier work on this PC yet." : ("Nothing matches \"" + text2 + "\". Try other words (a name, a site, a topic)."));
      }
      return jsonObject2;
    }

    // Token: 0x0600028E RID: 654 RVA: 0x000E2D54 File Offset: 0x000E2D54
    [return: Nullable(2)]
    private string UnlistedJobFolder(string jobId, string ownerStamp)
    {
      string[] array;
      try
      {
        array = (Directory.Exists(this.JobsRootPath) ? Directory.GetDirectories(this.JobsRootPath) : Array.Empty<string>());
      }
      catch (Exception)
      {
        array = Array.Empty<string>();
      }
      return array.Where<string>(delegate(string path)
      {
        string fileName = Path.GetFileName(path);
        return !fileName.StartsWith('.') && (ClickyWorkIndex.JobIdFromFolderName(fileName) == jobId || fileName == jobId);
      }).FirstOrDefault<string>((string path) => ClickyWorkIndex.FolderOwnerStamp(path) == ownerStamp);
    }

    // Token: 0x0600028F RID: 655 RVA: 0x000E2DD4 File Offset: 0x000E2DD4
    [return: TupleElementNames(new string[] { "Id", "Title", "Folder" })]
    [return: Nullable(new byte[] { 0, 1, 1, 2 })]
    public ValueTuple<string, string, string>? EarlierWorkElsewhere(string jobId)
    {
      jobId = jobId.Trim();
      if (jobId.Length == 0)
      {
        return null;
      }
      ClickyInfo clickyInfo = this.clickys.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo c) => c.ThreadId == jobId);
      if (clickyInfo != null)
      {
        return new ValueTuple<string, string, string>?(new ValueTuple<string, string, string>(jobId, clickyInfo.Name, this.clickys.WorkspacePath(clickyInfo.Slug)));
      }
      string text = this.SyncWorkOwnerStamp();
      if (text != null)
      {
        string text2 = this.UnlistedJobFolder(jobId, text);
        if (text2 != null)
        {
          return new ValueTuple<string, string, string>?(new ValueTuple<string, string, string>(jobId, ClickyWorkIndex.TitleFromFolderName(Path.GetFileName(text2.TrimEnd(new char[] { '\\', '/' }))), text2));
        }
      }
      return null;
    }

    // Token: 0x06000290 RID: 656 RVA: 0x000E2EB8 File Offset: 0x000E2EB8
    public async Task<JsonObject> ReadJobAsync(string rawJobId, string detail)
    {
      AgentService.<>c__DisplayClass430_0 CS$<>8__locals1 = new AgentService.<>c__DisplayClass430_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.jobId = rawJobId.Trim();
      JsonObject jsonObject2;
      if (CS$<>8__locals1.jobId.Length == 0)
      {
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["error"] = "Say which job: its job_id from find_jobs.";
        jsonObject2 = jsonObject;
      }
      else
      {
        ClickyJob job = this.Jobs.Job(CS$<>8__locals1.jobId) ?? this.Jobs.JobForThread(CS$<>8__locals1.jobId);
        ClickyInfo clicky = ((job == null) ? this.clickys.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo c) => c.ThreadId == CS$<>8__locals1.jobId) : null);
        string text = null;
        if (job == null && clicky == null)
        {
          CS$<>8__locals1.ownerStamp = this.SyncWorkOwnerStamp();
          if (CS$<>8__locals1.ownerStamp != null)
          {
            text = await Task.Run<string>(() => CS$<>8__locals1.<>4__this.UnlistedJobFolder(CS$<>8__locals1.jobId, CS$<>8__locals1.ownerStamp)).ConfigureAwait(false);
          }
        }
        AgentService.<>c__DisplayClass430_0 CS$<>8__locals2 = CS$<>8__locals1;
        ClickyJob clickyJob = job;
        string text2;
        if ((text2 = ((clickyJob != null) ? clickyJob.FolderPath : null)) == null)
        {
          text2 = text ?? ((clicky == null) ? null : this.clickys.WorkspacePath(clicky.Slug));
        }
        CS$<>8__locals2.folderPath = text2;
        if (job == null && clicky == null && text == null)
        {
          JsonObject jsonObject3 = new JsonObject(null);
          jsonObject3["error"] = "No job with that job_id. find_jobs lists them.";
          jsonObject2 = jsonObject3;
        }
        else
        {
          ClickyJob clickyJob2 = job;
          string text3;
          if ((text3 = ((clickyJob2 != null) ? clickyJob2.Title : null)) == null)
          {
            ClickyInfo clickyInfo = clicky;
            if ((text3 = ((clickyInfo != null) ? clickyInfo.Name : null)) == null)
            {
              text3 = ClickyWorkIndex.TitleFromFolderName((CS$<>8__locals1.folderPath == null) ? CS$<>8__locals1.jobId : Path.GetFileName(CS$<>8__locals1.folderPath.TrimEnd(new char[] { '\\', '/' })));
            }
          }
          string title = text3;
          if (detail != "conversation")
          {
            string text4 = await Task.Run<string>(delegate
            {
              string text12;
              try
              {
                string text11 = ((CS$<>8__locals1.folderPath == null) ? null : Path.Combine(CS$<>8__locals1.folderPath, "report.md"));
                text12 = ((text11 != null && File.Exists(text11)) ? File.ReadAllText(text11) : null);
              }
              catch (Exception)
              {
                text12 = null;
              }
              return text12;
            }).ConfigureAwait(false);
            if (text4 != null && text4.Length > 0)
            {
              JsonObject jsonObject4 = new JsonObject(null);
              string text5 = "job_id";
              ClickyJob clickyJob3 = job;
              jsonObject4[text5] = ((clickyJob3 != null) ? clickyJob3.Id : null) ?? CS$<>8__locals1.jobId;
              jsonObject4["title"] = title;
              jsonObject4["report"] = ClickyWorkIndex.TrimmedReport(text4, 24000);
              return jsonObject4;
            }
            if (job != null)
            {
              string text6 = string.Join("\n", (job.Runs ?? Array.Empty<ClickyJobRun>()).Select<ClickyJobRun, string>(delegate(ClickyJobRun run)
              {
                DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 2);
                defaultInterpolatedStringHandler.AppendLiteral("- ");
                DateTimeOffset? finishedAt = run.FinishedAt;
                string text13;
                if (finishedAt != null)
                {
                  DateTimeOffset valueOrDefault = finishedAt.GetValueOrDefault();
                  text13 = ClickyWorkIndex.DateText(valueOrDefault);
                }
                else
                {
                  text13 = "working";
                }
                defaultInterpolatedStringHandler.AppendFormatted(text13);
                defaultInterpolatedStringHandler.AppendLiteral(" · asked: ");
                defaultInterpolatedStringHandler.AppendFormatted(ClickyWorkIndex.OneLine(run.Ask, 400));
                defaultInterpolatedStringHandler.AppendLiteral(" · ");
                string text14 = defaultInterpolatedStringHandler.ToStringAndClear();
                string text15 = ClickyWorkIndex.ReportOutcomeText(run.Outcome);
                string summary = run.Summary;
                return text14 + text15 + ((summary != null) ? (": " + summary) : "");
              }));
              JsonObject jsonObject5 = new JsonObject(null);
              jsonObject5["job_id"] = job.Id;
              jsonObject5["title"] = title;
              jsonObject5["report"] = text6 + "\n\nLatest reply:\n" + (job.ReplyText ?? "(none)");
              return jsonObject5;
            }
            if (clicky == null)
            {
              JsonObject jsonObject6 = new JsonObject(null);
              jsonObject6["job_id"] = CS$<>8__locals1.jobId;
              jsonObject6["title"] = title;
              jsonObject6["report"] = "(no report kept for this folder; read its conversation or its output/ files)";
              return jsonObject6;
            }
          }
          AgentService.<>c__DisplayClass430_0 CS$<>8__locals3 = CS$<>8__locals1;
          ClickyJob clickyJob4 = job;
          CS$<>8__locals3.threadIds = ((clickyJob4 != null) ? clickyJob4.AllThreadIds : null) ?? ((clicky != null) ? new <>z__ReadOnlySingleElementList<string>(CS$<>8__locals1.jobId) : Array.Empty<string>());
          CS$<>8__locals1.folderForRecords = ((CS$<>8__locals1.folderPath == null) ? null : ClickyWorkIndex.StandardizedPath(CS$<>8__locals1.folderPath));
          List<ValueTuple<string, IReadOnlyList<ClickyHarnessRecord.Turn>>> list = await Task.Run<List<ValueTuple<string, IReadOnlyList<ClickyHarnessRecord.Turn>>>>(delegate
          {
            List<string> list3 = CS$<>8__locals1.threadIds.ToList<string>();
            List<ValueTuple<string, IReadOnlyList<ClickyHarnessRecord.Turn>>> list4 = new List<ValueTuple<string, IReadOnlyList<ClickyHarnessRecord.Turn>>>();
            if (list3.Count == 0 && CS$<>8__locals1.folderForRecords != null)
            {
              IEnumerable<ValueTuple<string, string>> enumerable = ClickyHarnessRecord.AllRecords(AgentService.HarnessRecordsRoot);
              Func<ValueTuple<string, string>, bool> func;
              if ((func = CS$<>8__locals1.<>9__7) == null)
              {
                func = (CS$<>8__locals1.<>9__7 = ([TupleElementNames(new string[] { "ThreadId", "Workspace" })] ValueTuple<string, string> r) => string.Equals(ClickyWorkIndex.StandardizedPath(r.Item2), CS$<>8__locals1.folderForRecords, StringComparison.OrdinalIgnoreCase));
              }
              foreach (ValueTuple<string, string> valueTuple2 in enumerable.Where<ValueTuple<string, string>>(func))
              {
                string text16 = ClickyHarnessRecord.RecordDirectory(AgentService.HarnessRecordsRoot, valueTuple2.Item1);
                if (text16 != null)
                {
                  list4.Add(new ValueTuple<string, IReadOnlyList<ClickyHarnessRecord.Turn>>(valueTuple2.Item1, ClickyHarnessRecord.Turns(text16)));
                }
              }
              return list4;
            }
            foreach (string text17 in list3)
            {
              string text18 = AgentService.HarnessRecordThreadId(text17);
              if (text18 != null)
              {
                string text19 = ClickyHarnessRecord.RecordDirectory(AgentService.HarnessRecordsRoot, text18);
                if (text19 != null)
                {
                  list4.Add(new ValueTuple<string, IReadOnlyList<ClickyHarnessRecord.Turn>>(text17, ClickyHarnessRecord.Turns(text19)));
                }
              }
            }
            return list4;
          }).ConfigureAwait(false);
          CS$<>8__locals1.harnessThreadIds = list.Select<ValueTuple<string, IReadOnlyList<ClickyHarnessRecord.Turn>>, string>(([TupleElementNames(new string[] { "ThreadId", "Turns" })] ValueTuple<string, IReadOnlyList<ClickyHarnessRecord.Turn>> r) => r.Item1).ToHashSet<string>();
          List<string> list2 = new List<string>();
          foreach (ValueTuple<string, IReadOnlyList<ClickyHarnessRecord.Turn>> valueTuple in list)
          {
            foreach (ClickyHarnessRecord.Turn turn in valueTuple.Item2)
            {
              string text7 = "They asked: " + AgentService.DisplayUserText(turn.RawAsk).Trim();
              if (turn.ToolLines.Count > 0)
              {
                text7 = text7 + "\nIt did:\n" + string.Join("\n", turn.ToolLines);
              }
              string finalText = turn.FinalText;
              string text8 = ((finalText != null) ? StructuredFinalReply.Parse(finalText).Text : "");
              string[] array = new string[5];
              array[0] = text7;
              array[1] = "\nIt replied";
              int num = 2;
              string status = turn.Status;
              array[num] = ((status != null) ? (" (" + status + ")") : "");
              array[3] = ":\n";
              array[4] = ((text8.Length == 0) ? "(no reply)" : text8);
              list2.Add(string.Concat(array));
            }
          }
          list2.AddRange(this.CachedConversationExchanges(CS$<>8__locals1.threadIds.Where<string>((string t) => !CS$<>8__locals1.harnessThreadIds.Contains(t)).ToList<string>()));
          if (list2.Count == 0)
          {
            JsonObject jsonObject7 = new JsonObject(null);
            string text9 = "job_id";
            ClickyJob clickyJob5 = job;
            jsonObject7[text9] = ((clickyJob5 != null) ? clickyJob5.Id : null) ?? CS$<>8__locals1.jobId;
            jsonObject7["title"] = title;
            jsonObject7["conversation"] = "(nothing recorded)";
            jsonObject2 = jsonObject7;
          }
          else
          {
            JsonObject jsonObject8 = new JsonObject(null);
            string text10 = "job_id";
            ClickyJob clickyJob6 = job;
            jsonObject8[text10] = ((clickyJob6 != null) ? clickyJob6.Id : null) ?? CS$<>8__locals1.jobId;
            jsonObject8["title"] = title;
            jsonObject8["conversation"] = AgentService.NewestExchanges(list2, 40000);
            jsonObject2 = jsonObject8;
          }
        }
      }
      return jsonObject2;
    }

    // Token: 0x06000291 RID: 657 RVA: 0x000E2F0C File Offset: 0x000E2F0C
    [return: Nullable(2)]
    private static string HarnessRecordThreadId(string appThreadId)
    {
      if (!AgentHarnessPreferences.IsHarnessThread(appThreadId))
      {
        return AgentHarnessThreads.ExistingHarnessThreadId(appThreadId);
      }
      return appThreadId;
    }

    // Token: 0x06000292 RID: 658 RVA: 0x000E2F20 File Offset: 0x000E2F20
    [return: Nullable(new byte[] { 2, 1 })]
    private IReadOnlyList<AgentTranscriptTurn> HarnessRecordTranscript(string appThreadId)
    {
      string text = AgentService.HarnessRecordThreadId(appThreadId);
      if (text != null)
      {
        string text2 = ClickyHarnessRecord.RecordDirectory(AgentService.HarnessRecordsRoot, text);
        if (text2 != null)
        {
          List<AgentTranscriptTurn> list = (from t in ClickyHarnessRecord.Turns(text2).Select<ClickyHarnessRecord.Turn, AgentTranscriptTurn>(delegate(ClickyHarnessRecord.Turn turn)
            {
              string text3 = AgentService.DisplayUserText(turn.RawAsk);
              string text4 = this.clickys.DisplayFor(text3) ?? text3;
              string finalText = turn.FinalText;
              StructuredFinalReply structuredFinalReply = ((finalText != null) ? StructuredFinalReply.Parse(finalText) : null);
              string text5 = null;
              IReadOnlyList<string> readOnlyList;
              IReadOnlyList<string> readOnlyList2;
              if (text4.Trim().Length != 0)
              {
                readOnlyList = new <>z__ReadOnlySingleElementList<string>(text4);
                readOnlyList2 = readOnlyList;
              }
              else
              {
                readOnlyList = Array.Empty<string>();
                readOnlyList2 = readOnlyList;
              }
              IReadOnlyList<string> toolLines = turn.ToolLines;
              IReadOnlyList<string> readOnlyList3;
              if (structuredFinalReply != null)
              {
                string text6 = structuredFinalReply.Text;
                if (text6 != null && text6.Length > 0)
                {
                  readOnlyList = new <>z__ReadOnlySingleElementList<string>(structuredFinalReply.Text);
                  readOnlyList3 = readOnlyList;
                  goto IL_008E;
                }
              }
              readOnlyList = Array.Empty<string>();
              readOnlyList3 = readOnlyList;
              IL_008E:
              return new AgentTranscriptTurn(text5, readOnlyList2, toolLines, readOnlyList3, ((structuredFinalReply != null) ? structuredFinalReply.NextActions : null) ?? Array.Empty<string>(), ((structuredFinalReply != null) ? structuredFinalReply.Artifacts : null) ?? Array.Empty<string>(), turn.Status ?? "completed", null, null, turn.EndedAt);
            })
            where t.UserMessages.Count > 0 || t.AssistantMessages.Count > 0
            select t).ToList<AgentTranscriptTurn>();
          if (list.Count != 0)
          {
            return list;
          }
          return null;
        }
      }
      return null;
    }

    // Token: 0x06000293 RID: 659 RVA: 0x000E2F94 File Offset: 0x000E2F94
    public string OneBrainJobId(string threadId)
    {
      ClickyJob clickyJob = this.Jobs.JobForThread(threadId);
      return ((clickyJob != null) ? clickyJob.Id : null) ?? threadId;
    }

    // Token: 0x06000294 RID: 660 RVA: 0x000E2FB4 File Offset: 0x000E2FB4
    [return: TupleElementNames(new string[] { "IsJob", "ThreadId" })]
    [return: Nullable(new byte[] { 0, 2 })]
    public ValueTuple<bool, string> ThreadIdForOneBrainJobId(string jobId)
    {
      ClickyJob clickyJob = this.Jobs.Job(jobId);
      if (clickyJob == null)
      {
        return new ValueTuple<bool, string>(false, null);
      }
      return new ValueTuple<bool, string>(true, clickyJob.ThreadId);
    }

    // Token: 0x0600029D RID: 669 RVA: 0x000E31EC File Offset: 0x000E31EC
    [CompilerGenerated]
    private bool <PrepareRuntimeLockedAsync>g__SignedOutSince|235_0(ref AgentService.<>c__DisplayClass235_0 A_1)
    {
      return A_1.account != Volatile.Read(ref this.accountGeneration);
    }

    // Token: 0x060002A5 RID: 677 RVA: 0x000E33A0 File Offset: 0x000E33A0
    [CompilerGenerated]
    private bool <NameJobsWithoutNicknames>g__SignedOutSince|333_1(ref AgentService.<>c__DisplayClass333_0 A_1)
    {
      return A_1.account != Volatile.Read(ref this.accountGeneration);
    }

    // Token: 0x060002AE RID: 686 RVA: 0x000E37EC File Offset: 0x000E37EC
    [CompilerGenerated]
    internal static string <LiveWorkDescription>g__Clipped|400_0(string text, int limit)
    {
      string text2 = text.Replace("\r", " ").Replace("\n", " ").Trim();
      if (text2.Length <= limit)
      {
        return text2;
      }
      return text2.Substring(0, limit) + "…";
    }

    // Token: 0x04000091 RID: 145
    public const string BrowserMcpServerKey = "clicky_browser";

    // Token: 0x04000098 RID: 152
    private readonly ConcurrentDictionary<string, DateTimeOffset> clickyAppFinishedAt = new ConcurrentDictionary<string, DateTimeOffset>();

    // Token: 0x04000099 RID: 153
    private static readonly Regex RoutineHeader = new Regex("^\\[Routine \"(.+?)\"(?: —)? run #(\\d+), ([^,\\]]+)");

    // Token: 0x0400009A RID: 154
    private const string LegacyClickyMigrationKey = "clicky.clickyApps.legacyClickysAsJobs.v1";

    // Token: 0x0400009B RID: 155
    private const string LegacyClickyDockingKey = "clicky.clickyApps.legacyClickysDocked.v1";

    // Token: 0x0400009C RID: 156
    private const string LegacyClickyWorkBackfillKey = "clicky.clickyApps.legacyClickyWorkBackfilled.v1";

    // Token: 0x0400009D RID: 157
    public const int LegacyClickyDockLimit = 30;

    // Token: 0x0400009E RID: 158
    private readonly ConcurrentDictionary<string, string> computerUseSummaries = new ConcurrentDictionary<string, string>();

    // Token: 0x0400009F RID: 159
    private readonly ConcurrentDictionary<string, bool> computerUseAnnounced = new ConcurrentDictionary<string, bool>();

    // Token: 0x040000A0 RID: 160
    private readonly ConcurrentDictionary<string, AgentTurnLease> reopenedLeases = new ConcurrentDictionary<string, AgentTurnLease>();

    // Token: 0x040000A6 RID: 166
    private const string LeaseMarkerStart = "<clicky_agent_turn_lease_metadata>";

    // Token: 0x040000A7 RID: 167
    private static readonly TimeSpan EarlyStopRefundWindow = TimeSpan.FromSeconds(10L);

    // Token: 0x040000A8 RID: 168
    private static readonly TimeSpan ComposioRefreshAfter = TimeSpan.FromMinutes(10L);

    // Token: 0x040000A9 RID: 169
    private readonly ClickyEndpoints endpoints;

    // Token: 0x040000AA RID: 170
    [Nullable(new byte[] { 1, 1, 2 })]
    private readonly Func<CancellationToken, Task<string>> supabaseAccessToken;

    // Token: 0x040000AB RID: 171
    private readonly AgentRuntimeOptions options;

    // Token: 0x040000AC RID: 172
    private readonly AgentWorkerApi api;

    // Token: 0x040000AD RID: 173
    private readonly AgentSessionTokenProvider tokens;

    // Token: 0x040000AE RID: 174
    private readonly CodexHomeManager home;

    // Token: 0x040000AF RID: 175
    private readonly ClickyStore clickys;

    // Token: 0x040000B0 RID: 176
    private readonly UserMemoryCache userMemory;

    // Token: 0x040000B1 RID: 177
    private readonly CodexSession session = new CodexSession();

    // Token: 0x040000B2 RID: 178
    private readonly RoutineScheduler routines;

    // Token: 0x040000B3 RID: 179
    private readonly SemaphoreSlim runtimeGate = new SemaphoreSlim(1, 1);

    // Token: 0x040000B4 RID: 180
    private readonly SemaphoreSlim slots;

    // Token: 0x040000B5 RID: 181
    private readonly ConcurrentDictionary<string, AgentTask> tasks = new ConcurrentDictionary<string, AgentTask>();

    // Token: 0x040000B6 RID: 182
    private readonly CancellationTokenSource lifetime = new CancellationTokenSource();

    // Token: 0x040000B7 RID: 183
    [Nullable(2)]
    private ComposioMcpConfiguration composio;

    // Token: 0x040000B8 RID: 184
    private bool composioUnreachable;

    // Token: 0x040000B9 RID: 185
    private DateTimeOffset composioFetchedAt;

    // Token: 0x040000BA RID: 186
    [Nullable(2)]
    private AgentIntegrationCatalog lastIntegrationCatalog;

    // Token: 0x040000BB RID: 187
    [Nullable(new byte[] { 2, 1 })]
    private IReadOnlySet<string> composioRefetchedForConnectedApps;

    // Token: 0x040000BC RID: 188
    private IReadOnlyList<CustomConnectorRegistration> customConnectors = Array.Empty<CustomConnectorRegistration>();

    // Token: 0x040000BD RID: 189
    private bool configDirty = true;

    // Token: 0x040000BE RID: 190
    private bool runtimeFilesPrepared;

    // Token: 0x040000BF RID: 191
    private int tasksChangedPending;

    // Token: 0x040000C0 RID: 192
    private bool disposed;

    // Token: 0x040000C1 RID: 193
    private long runtimeReadyMs = -1L;

    // Token: 0x040000C2 RID: 194
    private long launchToTurnStartedMs = -1L;

    // Token: 0x040000C3 RID: 195
    private long launchToFirstTokenMs = -1L;

    // Token: 0x040000C4 RID: 196
    private long launchToFinishedMs = -1L;

    // Token: 0x040000C9 RID: 201
    private int composioGeneration;

    // Token: 0x040000CA RID: 202
    private int accountGeneration;

    // Token: 0x040000CF RID: 207
    private const int MaxPersonaNameBackfillPerLaunch = 15;

    // Token: 0x040000D0 RID: 208
    private int personaNameBackfillStarted;

    // Token: 0x040000D3 RID: 211
    public const string PaywallSummary = "Oops, you're out of tokens! Beef up HeyClicky to continue.";

    // Token: 0x040000D9 RID: 217
    private readonly object dataSourcesGate = new object();

    // Token: 0x040000DA RID: 218
    [Nullable(new byte[] { 2, 1, 1, 1 })]
    private Dictionary<string, List<string>> dataSources;

    // Token: 0x040000DE RID: 222
    private readonly HashSet<string> jobIdsBeingNamed = new HashSet<string>();

    // Token: 0x040000DF RID: 223
    private int jobNamingRoundRunning;

    // Token: 0x040000E0 RID: 224
    private static readonly HashSet<string> SentenceStartWords = new HashSet<string>
    {
      "i", "im", "ive", "id", "sorry", "unfortunately", "please", "could", "can", "would",
      "there", "this", "it", "its", "you", "youre", "here", "heres", "based", "without",
      "what", "which", "how", "why", "unable", "no", "not"
    };

    // Token: 0x040000E5 RID: 229
    [Nullable(2)]
    private ClickyJobStepWatcher stepWatcher;

    // Token: 0x040000EA RID: 234
    public static readonly TimeSpan JobResumeWindow = TimeSpan.FromHours(24);

    // Token: 0x040000EB RID: 235
    public const string FollowUpLanguageLine = "Answer in the language of this newest message.";

    // Token: 0x040000EC RID: 236
    public const string ResumedRunContext = "This is a new message to this job, in the conversation where you did the earlier work: build on it, and change what they ask now. Answer in the language of this newest message.";

    // Token: 0x040000EE RID: 238
    private readonly ConcurrentDictionary<string, ClickyAnswer> resultCards = new ConcurrentDictionary<string, ClickyAnswer>();

    // Token: 0x040000F0 RID: 240
    public const int ReportReplyLimit = 6000;

    // Token: 0x0200007F RID: 127
    [Nullable(0)]
    private sealed class TurnSpec : IEquatable<AgentService.TurnSpec>
    {
      // Token: 0x0600097E RID: 2430 RVA: 0x0010433C File Offset: 0x0010433C
      public TurnSpec(string Prompt, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<byte[]> Images, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> AttachedFiles, string LaunchContext, string LaunchSource, string UsageContext, bool IsGeneratedBrief, [TupleElementNames(new string[] { "User", "Assistant" })] [Nullable(new byte[] { 2, 0, 1, 1 })] IReadOnlyList<ValueTuple<string, string>> PriorTurns, [Nullable(2)] AgentTurnLease PreauthorizedLease, bool AutoApproveExtraUsage, [Nullable(2)] string UserMemory)
      {
        this.Prompt = Prompt;
        this.Images = Images;
        this.AttachedFiles = AttachedFiles;
        this.LaunchContext = LaunchContext;
        this.LaunchSource = LaunchSource;
        this.UsageContext = UsageContext;
        this.IsGeneratedBrief = IsGeneratedBrief;
        this.PriorTurns = PriorTurns;
        this.PreauthorizedLease = PreauthorizedLease;
        this.AutoApproveExtraUsage = AutoApproveExtraUsage;
        this.UserMemory = UserMemory;
        base..ctor();
      }

      // Token: 0x17000266 RID: 614
      // (get) Token: 0x0600097F RID: 2431 RVA: 0x001043A4 File Offset: 0x001043A4
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentService.TurnSpec);
        }
      }

      // Token: 0x17000267 RID: 615
      // (get) Token: 0x06000980 RID: 2432 RVA: 0x001043B0 File Offset: 0x001043B0
      // (set) Token: 0x06000981 RID: 2433 RVA: 0x001043B8 File Offset: 0x001043B8
      public string Prompt { get; set; }

      // Token: 0x17000268 RID: 616
      // (get) Token: 0x06000982 RID: 2434 RVA: 0x001043C4 File Offset: 0x001043C4
      // (set) Token: 0x06000983 RID: 2435 RVA: 0x001043CC File Offset: 0x001043CC
      [Nullable(new byte[] { 2, 1 })]
      public IReadOnlyList<byte[]> Images
      {
        [return: Nullable(new byte[] { 2, 1 })]
        get;
        [param: Nullable(new byte[] { 2, 1 })]
        set;
      }

      // Token: 0x17000269 RID: 617
      // (get) Token: 0x06000984 RID: 2436 RVA: 0x001043D8 File Offset: 0x001043D8
      // (set) Token: 0x06000985 RID: 2437 RVA: 0x001043E0 File Offset: 0x001043E0
      [Nullable(new byte[] { 2, 1 })]
      public IReadOnlyList<string> AttachedFiles
      {
        [return: Nullable(new byte[] { 2, 1 })]
        get;
        [param: Nullable(new byte[] { 2, 1 })]
        set;
      }

      // Token: 0x1700026A RID: 618
      // (get) Token: 0x06000986 RID: 2438 RVA: 0x001043EC File Offset: 0x001043EC
      // (set) Token: 0x06000987 RID: 2439 RVA: 0x001043F4 File Offset: 0x001043F4
      public string LaunchContext { get; set; }

      // Token: 0x1700026B RID: 619
      // (get) Token: 0x06000988 RID: 2440 RVA: 0x00104400 File Offset: 0x00104400
      // (set) Token: 0x06000989 RID: 2441 RVA: 0x00104408 File Offset: 0x00104408
      public string LaunchSource { get; set; }

      // Token: 0x1700026C RID: 620
      // (get) Token: 0x0600098A RID: 2442 RVA: 0x00104414 File Offset: 0x00104414
      // (set) Token: 0x0600098B RID: 2443 RVA: 0x0010441C File Offset: 0x0010441C
      public string UsageContext { get; set; }

      // Token: 0x1700026D RID: 621
      // (get) Token: 0x0600098C RID: 2444 RVA: 0x00104428 File Offset: 0x00104428
      // (set) Token: 0x0600098D RID: 2445 RVA: 0x00104430 File Offset: 0x00104430
      public bool IsGeneratedBrief { get; set; }

      // Token: 0x1700026E RID: 622
      // (get) Token: 0x0600098E RID: 2446 RVA: 0x0010443C File Offset: 0x0010443C
      // (set) Token: 0x0600098F RID: 2447 RVA: 0x00104444 File Offset: 0x00104444
      [TupleElementNames(new string[] { "User", "Assistant" })]
      [Nullable(new byte[] { 2, 0, 1, 1 })]
      public IReadOnlyList<ValueTuple<string, string>> PriorTurns
      {
        [return: TupleElementNames(new string[] { "User", "Assistant" })]
        [return: Nullable(new byte[] { 2, 0, 1, 1 })]
        get;
        [param: TupleElementNames(new string[] { "User", "Assistant" })]
        [param: Nullable(new byte[] { 2, 0, 1, 1 })]
        set;
      }

      // Token: 0x1700026F RID: 623
      // (get) Token: 0x06000990 RID: 2448 RVA: 0x00104450 File Offset: 0x00104450
      // (set) Token: 0x06000991 RID: 2449 RVA: 0x00104458 File Offset: 0x00104458
      [Nullable(2)]
      public AgentTurnLease PreauthorizedLease
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x17000270 RID: 624
      // (get) Token: 0x06000992 RID: 2450 RVA: 0x00104464 File Offset: 0x00104464
      // (set) Token: 0x06000993 RID: 2451 RVA: 0x0010446C File Offset: 0x0010446C
      public bool AutoApproveExtraUsage { get; set; }

      // Token: 0x17000271 RID: 625
      // (get) Token: 0x06000994 RID: 2452 RVA: 0x00104478 File Offset: 0x00104478
      // (set) Token: 0x06000995 RID: 2453 RVA: 0x00104480 File Offset: 0x00104480
      [Nullable(2)]
      public string UserMemory
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x06000996 RID: 2454 RVA: 0x0010448C File Offset: 0x0010448C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("TurnSpec");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000997 RID: 2455 RVA: 0x001044D8 File Offset: 0x001044D8
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Prompt = ");
        builder.Append(this.Prompt);
        builder.Append(", Images = ");
        builder.Append(this.Images);
        builder.Append(", AttachedFiles = ");
        builder.Append(this.AttachedFiles);
        builder.Append(", LaunchContext = ");
        builder.Append(this.LaunchContext);
        builder.Append(", LaunchSource = ");
        builder.Append(this.LaunchSource);
        builder.Append(", UsageContext = ");
        builder.Append(this.UsageContext);
        builder.Append(", IsGeneratedBrief = ");
        builder.Append(this.IsGeneratedBrief.ToString());
        builder.Append(", PriorTurns = ");
        builder.Append(this.PriorTurns);
        builder.Append(", PreauthorizedLease = ");
        builder.Append(this.PreauthorizedLease);
        builder.Append(", AutoApproveExtraUsage = ");
        builder.Append(this.AutoApproveExtraUsage.ToString());
        builder.Append(", UserMemory = ");
        builder.Append(this.UserMemory);
        return true;
      }

      // Token: 0x06000998 RID: 2456 RVA: 0x0010461C File Offset: 0x0010461C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentService.TurnSpec left, AgentService.TurnSpec right)
      {
        return !(left == right);
      }

      // Token: 0x06000999 RID: 2457 RVA: 0x00104628 File Offset: 0x00104628
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentService.TurnSpec left, AgentService.TurnSpec right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600099A RID: 2458 RVA: 0x0010463C File Offset: 0x0010463C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Prompt>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<byte[]>>.Default.GetHashCode(this.<Images>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<AttachedFiles>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LaunchContext>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LaunchSource>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UsageContext>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsGeneratedBrief>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ValueTuple<string, string>>>.Default.GetHashCode(this.<PriorTurns>k__BackingField)) * -1521134295 + EqualityComparer<AgentTurnLease>.Default.GetHashCode(this.<PreauthorizedLease>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<AutoApproveExtraUsage>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UserMemory>k__BackingField);
      }

      // Token: 0x0600099B RID: 2459 RVA: 0x00104758 File Offset: 0x00104758
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentService.TurnSpec);
      }

      // Token: 0x0600099C RID: 2460 RVA: 0x00104768 File Offset: 0x00104768
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentService.TurnSpec other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Prompt>k__BackingField, other.<Prompt>k__BackingField) && EqualityComparer<IReadOnlyList<byte[]>>.Default.Equals(this.<Images>k__BackingField, other.<Images>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<AttachedFiles>k__BackingField, other.<AttachedFiles>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LaunchContext>k__BackingField, other.<LaunchContext>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LaunchSource>k__BackingField, other.<LaunchSource>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UsageContext>k__BackingField, other.<UsageContext>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsGeneratedBrief>k__BackingField, other.<IsGeneratedBrief>k__BackingField) && EqualityComparer<IReadOnlyList<ValueTuple<string, string>>>.Default.Equals(this.<PriorTurns>k__BackingField, other.<PriorTurns>k__BackingField) && EqualityComparer<AgentTurnLease>.Default.Equals(this.<PreauthorizedLease>k__BackingField, other.<PreauthorizedLease>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<AutoApproveExtraUsage>k__BackingField, other.<AutoApproveExtraUsage>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UserMemory>k__BackingField, other.<UserMemory>k__BackingField));
      }

      // Token: 0x0600099E RID: 2462 RVA: 0x001048BC File Offset: 0x001048BC
      [CompilerGenerated]
      private TurnSpec(AgentService.TurnSpec original)
      {
        this.Prompt = original.<Prompt>k__BackingField;
        this.Images = original.<Images>k__BackingField;
        this.AttachedFiles = original.<AttachedFiles>k__BackingField;
        this.LaunchContext = original.<LaunchContext>k__BackingField;
        this.LaunchSource = original.<LaunchSource>k__BackingField;
        this.UsageContext = original.<UsageContext>k__BackingField;
        this.IsGeneratedBrief = original.<IsGeneratedBrief>k__BackingField;
        this.PriorTurns = original.<PriorTurns>k__BackingField;
        this.PreauthorizedLease = original.<PreauthorizedLease>k__BackingField;
        this.AutoApproveExtraUsage = original.<AutoApproveExtraUsage>k__BackingField;
        this.UserMemory = original.<UserMemory>k__BackingField;
      }

      // Token: 0x0600099F RID: 2463 RVA: 0x00104954 File Offset: 0x00104954
      [CompilerGenerated]
      public void Deconstruct(out string Prompt, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<byte[]> Images, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<string> AttachedFiles, out string LaunchContext, out string LaunchSource, out string UsageContext, out bool IsGeneratedBrief, [TupleElementNames(new string[] { "User", "Assistant" })] [Nullable(new byte[] { 2, 0, 1, 1 })] out IReadOnlyList<ValueTuple<string, string>> PriorTurns, [Nullable(2)] out AgentTurnLease PreauthorizedLease, out bool AutoApproveExtraUsage, [Nullable(2)] out string UserMemory)
      {
        Prompt = this.Prompt;
        Images = this.Images;
        AttachedFiles = this.AttachedFiles;
        LaunchContext = this.LaunchContext;
        LaunchSource = this.LaunchSource;
        UsageContext = this.UsageContext;
        IsGeneratedBrief = this.IsGeneratedBrief;
        PriorTurns = this.PriorTurns;
        PreauthorizedLease = this.PreauthorizedLease;
        AutoApproveExtraUsage = this.AutoApproveExtraUsage;
        UserMemory = this.UserMemory;
      }
    }

    // Token: 0x02000080 RID: 128
    [Nullable(0)]
    private sealed class RoutineHost : IRoutineHost
    {
      // Token: 0x060009A0 RID: 2464 RVA: 0x001049C4 File Offset: 0x001049C4
      public RoutineHost(AgentService owner)
      {
      }

      // Token: 0x17000272 RID: 626
      // (get) Token: 0x060009A1 RID: 2465 RVA: 0x001049D4 File Offset: 0x001049D4
      public bool IsSignedIn
      {
        get
        {
          return this.<owner>P.tokens.ExpiresAt != null;
        }
      }

      // Token: 0x060009A2 RID: 2466 RVA: 0x00104A00 File Offset: 0x00104A00
      public void BackfillWorkReportsIfNeeded()
      {
        this.<owner>P.BackfillWorkReportsIfNeeded();
      }

      // Token: 0x060009A3 RID: 2467 RVA: 0x00104A10 File Offset: 0x00104A10
      public bool ClickyExists(string slug)
      {
        ClickyInfo clickyInfo = this.<owner>P.clickys.Get(slug);
        return clickyInfo != null && !clickyInfo.IsArchived;
      }

      // Token: 0x060009A4 RID: 2468 RVA: 0x00104A40 File Offset: 0x00104A40
      public bool IsClickyBusy(string slug)
      {
        return this.<owner>P.tasks.Values.Any<AgentTask>((AgentTask t) => t.ClickySlug == slug && AgentService.Lock<bool>(t, () => t.IsActive));
      }

      // Token: 0x060009A5 RID: 2469 RVA: 0x00104A7C File Offset: 0x00104A7C
      public bool IsClickyAwaitingExtraUsage(string slug)
      {
        return this.<owner>P.tasks.Values.Any<AgentTask>((AgentTask t) => t.ClickySlug == slug && AgentService.Lock<bool>(t, () => t.RequiresExtraUsageApproval));
      }

      // Token: 0x060009A6 RID: 2470 RVA: 0x00104AB8 File Offset: 0x00104AB8
      public bool JobExists(string jobId)
      {
        return this.<owner>P.Jobs.Job(jobId) != null;
      }

      // Token: 0x060009A7 RID: 2471 RVA: 0x00104AD4 File Offset: 0x00104AD4
      public bool IsJobBusy(string jobId)
      {
        ClickyJob clickyJob = this.<owner>P.Jobs.Job(jobId);
        if (clickyJob == null)
        {
          return false;
        }
        AgentTask task = this.<owner>P.LiveTask(clickyJob);
        return (task != null && AgentService.Lock<bool>(task, () => task.IsActive)) || clickyJob.AllThreadIds.Any<string>(new Func<string, bool>(this.<owner>P.session.IsThreadActive));
      }

      // Token: 0x060009A8 RID: 2472 RVA: 0x00104B54 File Offset: 0x00104B54
      public bool IsJobAwaitingExtraUsage(string jobId)
      {
        ClickyJob clickyJob = this.<owner>P.Jobs.Job(jobId);
        if (clickyJob != null)
        {
          AgentTask task = this.<owner>P.LiveTask(clickyJob);
          if (task != null)
          {
            return AgentService.Lock<bool>(task, () => task.RequiresExtraUsageApproval);
          }
        }
        return false;
      }

      // Token: 0x060009A9 RID: 2473 RVA: 0x00104BB0 File Offset: 0x00104BB0
      [return: Nullable(new byte[] { 1, 2 })]
      public async Task<string> LaunchJobRoutineRunAsync(ClickyRoutine routine, int runNumber, string prompt, bool scheduled)
      {
        ClickyJob job = this.<owner>P.Jobs.Job(routine.JobId);
        string text;
        if (job == null)
        {
          text = null;
        }
        else
        {
          string threadId = job.ThreadId;
          string continuedThreadId = ((threadId != null && AgentHarnessThreads.ContinuesOnNewConversationHarness(threadId)) ? threadId : null);
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(155, 3);
          defaultInterpolatedStringHandler.AppendLiteral("This turn is run #");
          defaultInterpolatedStringHandler.AppendFormatted<int>(runNumber);
          defaultInterpolatedStringHandler.AppendLiteral(" of the routine \"");
          defaultInterpolatedStringHandler.AppendFormatted(routine.Title);
          defaultInterpolatedStringHandler.AppendLiteral("\" (");
          defaultInterpolatedStringHandler.AppendFormatted(routine.CadenceLabel.ToLowerInvariant());
          defaultInterpolatedStringHandler.AppendLiteral(") that the user set up on this job, so HeyClicky is launching it automatically with the user's standing instructions.");
          string text2 = defaultInterpolatedStringHandler.ToStringAndClear();
          if (continuedThreadId == null)
          {
            this.<owner>P.RetireClickysNoteForMovedRoutineJob(job);
            bool flag = File.Exists(Path.Combine(job.FolderPath, ClickyWorkFolderFiles.PreviousNoteRelativePath));
            text2 = text2 + "\n" + this.<owner>P.JobContinuationContext(job, flag);
          }
          else if (runNumber > 1)
          {
            text2 += " Earlier runs are in this conversation above; build on them and report what is new rather than repeating them.";
          }
          AgentTask existing = this.<owner>P.tasks.Values.FirstOrDefault<AgentTask>((AgentTask x) => AgentService.Lock<string>(x, () => x.JobId) == job.Id);
          if (existing != null)
          {
            object obj = existing.Gate;
            lock (obj)
            {
              existing.IsRoutineRun = true;
            }
            await this.<owner>P.StartFollowUpTurnAsync(existing, prompt, null, "routine", "companion", null, CancellationToken.None, text2, continuedThreadId, continuedThreadId == null, null).ConfigureAwait(false);
            this.<owner>P.Jobs.BeginRun(job.Id, routine.Title, continuedThreadId);
            text = existing.Id;
          }
          else
          {
            string text3 = await this.<owner>P.LaunchAsync(new AgentLaunchRequest
            {
              Prompt = prompt,
              JobId = job.Id,
              TargetThreadId = continuedThreadId,
              StartsNewJobConversation = (continuedThreadId == null),
              LaunchSource = "routine",
              IsGeneratedTaskBrief = true,
              LaunchContext = text2
            }, default(CancellationToken)).ConfigureAwait(false);
            this.<owner>P.Jobs.BeginRun(job.Id, routine.Title, continuedThreadId);
            AgentTask agentTask;
            if (this.<owner>P.tasks.TryGetValue(text3, out agentTask))
            {
              object obj = agentTask.Gate;
              lock (obj)
              {
                agentTask.IsRoutineRun = true;
              }
            }
            text = text3;
          }
        }
        return text;
      }

      // Token: 0x060009AA RID: 2474 RVA: 0x00104C0C File Offset: 0x00104C0C
      [return: Nullable(new byte[] { 1, 2 })]
      public async Task<string> LaunchRoutineRunAsync(ClickyRoutine routine, int runNumber, string prompt, string launchContext)
      {
        ClickyInfo clicky = this.<owner>P.clickys.Get(routine.ClickySlug);
        string text;
        if (clicky == null)
        {
          text = null;
        }
        else
        {
          AgentTask existing = this.<owner>P.tasks.Values.FirstOrDefault<AgentTask>((AgentTask t) => t.ClickySlug == clicky.Slug);
          string text2 = launchContext + TurnPromptBuilder.ClickyLaunchContext(clicky, false, null, false);
          if (existing != null)
          {
            object obj = existing.Gate;
            lock (obj)
            {
              existing.IsRoutineRun = true;
            }
            await this.<owner>P.StartFollowUpTurnAsync(existing, prompt, null, "routine", "companion", null, CancellationToken.None, text2, null, false, null).ConfigureAwait(false);
            text = existing.Id;
          }
          else
          {
            string text3 = await this.<owner>P.LaunchAsync(new AgentLaunchRequest
            {
              Prompt = prompt,
              ClickySlug = clicky.Slug,
              LaunchSource = "routine",
              IsGeneratedTaskBrief = true,
              LaunchContext = launchContext
            }, default(CancellationToken)).ConfigureAwait(false);
            AgentTask agentTask;
            if (this.<owner>P.tasks.TryGetValue(text3, out agentTask))
            {
              object obj = agentTask.Gate;
              lock (obj)
              {
                agentTask.IsRoutineRun = true;
              }
            }
            text = text3;
          }
        }
        return text;
      }

      // Token: 0x060009AB RID: 2475 RVA: 0x00104C68 File Offset: 0x00104C68
      [return: Nullable(2)]
      public string CreateJobForMovedRoutine(ClickyRoutine routine)
      {
        ClickyInfo clickyInfo = this.<owner>P.clickys.Get(routine.ClickySlug);
        if (clickyInfo == null)
        {
          return null;
        }
        string text = this.<owner>P.clickys.EnsureWorkspace(clickyInfo);
        ClickyJobStore jobs = this.<owner>P.Jobs;
        string title = routine.Title;
        string taskPrompt = routine.TaskPrompt;
        string text2 = text;
        string threadId = clickyInfo.ThreadId;
        DateTimeOffset? lastRunFinishedAt = routine.LastRunFinishedAt;
        return jobs.CreateJobForMovedRoutine(title, taskPrompt, text2, threadId, (lastRunFinishedAt != null) ? lastRunFinishedAt : routine.LastRunStartedAt).Id;
      }

      // Token: 0x060009AC RID: 2476 RVA: 0x00104CE4 File Offset: 0x00104CE4
      public void RetireClickysNoteForMovedRoutineJob(string jobId, bool onlyWhenLeft)
      {
        ClickyJob clickyJob = this.<owner>P.Jobs.Job(jobId);
        if (clickyJob == null)
        {
          return;
        }
        if (onlyWhenLeft && !File.Exists(AgentService.LiveNotePathForJob(clickyJob)))
        {
          return;
        }
        this.<owner>P.RetireClickysNoteForMovedRoutineJob(clickyJob);
      }

      // Token: 0x0400035E RID: 862
      [CompilerGenerated]
      private AgentService <owner>P = owner;
    }

    // Token: 0x02000082 RID: 130
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04000366 RID: 870
      [Nullable(0)]
      public static Func<char, bool> <0>__IsLetterOrDigit;

      // Token: 0x04000367 RID: 871
      [Nullable(0)]
      public static Action <1>__Stop;

      // Token: 0x04000368 RID: 872
      [Nullable(0)]
      public static Func<char, bool> <2>__IsLetter;
    }
  }
}
