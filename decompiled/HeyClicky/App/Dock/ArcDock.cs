using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Jobs;
using HeyClicky.Agents.Routines;
using HeyClicky.App.Browser;
using HeyClicky.App.Drive;
using HeyClicky.App.Ink;
using HeyClicky.App.Interop;
using HeyClicky.App.Notch.Window;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Ink;
using HeyClicky.Core.Telemetry;
using HeyClicky.Drawing.Audio;
using HeyClicky.Platform.Input;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Windows.ApplicationModel.DataTransfer;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002F4 RID: 756
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ArcDock
  {
    // Token: 0x170005EA RID: 1514
    // (get) Token: 0x0600205F RID: 8287 RVA: 0x00616F7C File Offset: 0x00616F7C
    // (set) Token: 0x06002060 RID: 8288 RVA: 0x00616F84 File Offset: 0x00616F84
    [Nullable(2)]
    public AgentDriveConsent DriveConsent
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170005EB RID: 1515
    // (get) Token: 0x06002061 RID: 8289 RVA: 0x00616F90 File Offset: 0x00616F90
    // (set) Token: 0x06002062 RID: 8290 RVA: 0x00616F98 File Offset: 0x00616F98
    [Nullable(new byte[] { 2, 1, 1 })]
    public Func<string, Task> SubmitFact
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x06002063 RID: 8291 RVA: 0x00616FA4 File Offset: 0x00616FA4
    private void Track(ArcCardContent content, string action, [Nullable(2)] JsonObject extra = null)
    {
      JsonObject jsonObject = extra ?? new JsonObject(null);
      jsonObject["action"] = action;
      if (content.JobId != null)
      {
        ClickyAnalytics.TrackProductEvent("job_action", null, content.ThreadId, jsonObject);
        return;
      }
      ClickyAnalytics.TrackProductEvent("clicky_dashboard_action", content.ClickySlug, null, jsonObject);
    }

    // Token: 0x06002064 RID: 8292 RVA: 0x00617004 File Offset: 0x00617004
    public static void TrackWindowAction(string action, ArcCardContent content, [Nullable(2)] JsonObject extra = null)
    {
      JsonObject jsonObject = ArcDock.WindowProperties(content);
      jsonObject["action"] = action;
      if (extra != null)
      {
        foreach (KeyValuePair<string, JsonNode> keyValuePair in extra)
        {
          string text;
          JsonNode jsonNode;
          keyValuePair.Deconstruct(out text, out jsonNode);
          string text2 = text;
          JsonNode jsonNode2 = jsonNode;
          jsonObject[text2] = ((jsonNode2 != null) ? jsonNode2.DeepClone() : null);
        }
      }
      ClickyAnalytics.TrackProductEvent("arc_dock_action", content.ClickySlug, content.ThreadId, jsonObject);
    }

    // Token: 0x06002065 RID: 8293 RVA: 0x0061709C File Offset: 0x0061709C
    public static JsonObject WindowProperties(ArcCardContent content)
    {
      return ArcDockAnalytics.WindowProperties(content.State, content.IsFinalizing, content.JobId != null);
    }

    // Token: 0x06002066 RID: 8294 RVA: 0x006170BC File Offset: 0x006170BC
    public void TrackRequestAnswered(string answer, string surface, string railKey, [Nullable(2)] string taskId)
    {
      ArcDock.TrackRequestAnswered(this.agents, answer, surface, railKey, taskId);
    }

    // Token: 0x06002067 RID: 8295 RVA: 0x006170D0 File Offset: 0x006170D0
    public static void TrackRequestAnswered(AgentService agents, string answer, string surface, string railKey, [Nullable(2)] string taskId)
    {
      string text = ClickyJob.JobIdFromRailKey(railKey);
      string text2;
      if (text != null)
      {
        ClickyJob clickyJob = agents.Jobs.Job(text);
        text2 = ((clickyJob != null) ? clickyJob.ThreadId : null);
      }
      else
      {
        text2 = null;
      }
      string text3 = text2;
      string text4;
      if (taskId != null)
      {
        AgentTaskDetail taskDetail = agents.GetTaskDetail(taskId);
        text4 = ((taskDetail != null) ? taskDetail.Snapshot.ThreadId : null);
      }
      else
      {
        text4 = null;
      }
      string text5 = text4;
      ClickyAnalytics.TrackProductEvent("agent_request_answered", (text3 == null) ? railKey : null, text5 ?? text3, ArcDockAnalytics.RequestAnswered(answer, surface, text != null, taskId != null));
    }

    // Token: 0x06002068 RID: 8296 RVA: 0x0061714C File Offset: 0x0061714C
    private void MarkWindowRead(string railKey)
    {
      this.Host.DismissNotchUpdate(railKey);
      string text = ClickyJob.JobIdFromRailKey(railKey);
      if (text != null)
      {
        this.agents.Jobs.MarkSeen(text);
        return;
      }
      this.Host.MarkClickyRead(railKey);
    }

    // Token: 0x06002069 RID: 8297 RVA: 0x00617198 File Offset: 0x00617198
    private void MarkRead(ArcCardContent content)
    {
      string jobId = content.JobId;
      if (jobId != null)
      {
        this.agents.Jobs.MarkSeen(jobId);
        return;
      }
      string clickySlug = content.ClickySlug;
      if (clickySlug != null)
      {
        this.Host.MarkClickyRead(clickySlug);
      }
    }

    // Token: 0x0600206A RID: 8298 RVA: 0x006171DC File Offset: 0x006171DC
    public void OpenInWindow(string key)
    {
      this.Host.OpenAgentInWindow(key);
    }

    // Token: 0x0600206B RID: 8299 RVA: 0x006171F0 File Offset: 0x006171F0
    public void MoveToEarlier(string key)
    {
      this.TakeOffDock(key);
    }

    // Token: 0x0600206C RID: 8300 RVA: 0x006171FC File Offset: 0x006171FC
    private void TakeOffDock(string key)
    {
      if (this.hoveredKey == key)
      {
        this.hoveredKey = null;
      }
      string text = ClickyJob.JobIdFromRailKey(key);
      if (text != null)
      {
        this.agents.Jobs.MarkSeen(text);
        this.agents.DismissJob(text);
      }
      else
      {
        this.Host.MarkClickyRead(key);
        this.RemoveDockedClicky(key);
        this.closedFromDockAt[key] = DateTimeOffset.Now;
      }
      this.Host.DismissNotchUpdate(key);
      this.Refresh();
    }

    // Token: 0x0600206D RID: 8301 RVA: 0x00617288 File Offset: 0x00617288
    public void RemoveFromDock(string key)
    {
      string text = DockOrder.NextFrontKeyAfterRemoving(this.DeckKeys(), key);
      this.TakeOffDock(key);
      List<string> list = this.DeckKeys().ToList<string>();
      if (text != null)
      {
        int num = list.IndexOf(text);
        if (num >= 0)
        {
          this.deckFocusIndex = new int?(num);
          this.view.Layout(LayoutReason.Items, true);
          this.Open(text, "the window beside a removed one");
          return;
        }
      }
      this.Close(false, "the dock's last window was removed");
    }

    // Token: 0x0600206E RID: 8302 RVA: 0x006172F8 File Offset: 0x006172F8
    public void StopWork(ArcCardContent content)
    {
      string jobId = content.JobId;
      if (jobId != null)
      {
        this.agents.StopJobAsync(jobId);
        return;
      }
      string taskId = content.TaskId;
      if (taskId != null)
      {
        string text = "agent_task_control_clicked";
        string clickySlug = content.ClickySlug;
        string threadId = content.ThreadId;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["action"] = "stop";
        ClickyAnalytics.TrackProductEvent(text, clickySlug, threadId, jsonObject);
        this.agents.StopAsync(taskId);
      }
    }

    // Token: 0x0600206F RID: 8303 RVA: 0x00617370 File Offset: 0x00617370
    public bool CanShowFullResponse(ArcCardContent content)
    {
      return this.Host.HasLatestReply(content.JobId, content.ClickySlug);
    }

    // Token: 0x06002070 RID: 8304 RVA: 0x00617390 File Offset: 0x00617390
    public void OpenFullResponse(ArcCardContent content)
    {
      string jobId = content.JobId;
      if (jobId != null)
      {
        ClickyJob clickyJob = this.agents.Jobs.Job(jobId);
        if (clickyJob != null && clickyJob.IsWaitingToStart)
        {
          this.StartWaitingJob(clickyJob);
          return;
        }
      }
      this.MarkRead(content);
      if (this.Host.HasLatestReply(content.JobId, content.ClickySlug))
      {
        this.Host.ShowFullReply(content.JobId, content.ClickySlug);
        return;
      }
      this.OpenInChat(content);
    }

    // Token: 0x06002071 RID: 8305 RVA: 0x00617418 File Offset: 0x00617418
    private void StartWaitingJob(ClickyJob job)
    {
      Sounds.Play("clicky-text-send", 0.35);
      if (!this.Host.StartWaitingJob(job))
      {
        this.agents.Jobs.Dismiss(job.Id);
        return;
      }
      ClickyJob clickyJob = this.agents.Jobs.Job(job.Id);
      if (clickyJob != null && clickyJob.IsWaitingToStart)
      {
        this.agents.Jobs.Dismiss(job.Id);
      }
      string text = "job_action";
      string text2 = null;
      string text3 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "started";
      jsonObject["from"] = "waiting_window";
      ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
    }

    // Token: 0x06002072 RID: 8306 RVA: 0x006174E0 File Offset: 0x006174E0
    public void OpenInChat(ArcCardContent content)
    {
      this.MarkRead(content);
      this.Host.ShowChat(content.JobId, content.ClickySlug);
    }

    // Token: 0x06002073 RID: 8307 RVA: 0x00617508 File Offset: 0x00617508
    public void OpenCardFile(string entry, ArcCardContent content)
    {
      this.MarkRead(content);
      string clickySlug = content.ClickySlug;
      if (clickySlug != null)
      {
        string text = "clicky_dashboard_action";
        string text2 = clickySlug;
        string text3 = null;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["action"] = "latest_file_opened";
        ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
      }
      string text4 = DockFiles.Resolve(entry, content.WorkspacePath);
      if (!this.Host.OpensInNotchDocumentViewer(text4))
      {
        this.Host.DismissNotch();
      }
      if (this.Host.OpenInNotchViewer(text4, content.ClickySlug, content.ThreadId))
      {
        return;
      }
      DockFiles.Open(entry, content.WorkspacePath);
    }

    // Token: 0x06002074 RID: 8308 RVA: 0x006175B4 File Offset: 0x006175B4
    public void OpenResultCardTarget(string target, ArcCardContent content)
    {
      this.MarkRead(content);
      if (target.StartsWith("http", StringComparison.OrdinalIgnoreCase))
      {
        DockFiles.Open(target, null);
        return;
      }
      this.OpenCardFile(target, content);
    }

    // Token: 0x06002075 RID: 8309 RVA: 0x006175DC File Offset: 0x006175DC
    public void AnswerResultCardQuestion(ArcCardContent content, ClickyAnswer.Choice choice)
    {
      Sounds.Play("clicky-text-send", 0.2);
      string reply = choice.Reply;
      string text = "The user answered your question by tapping \"" + reply + "\" on the job's card.";
      string file = choice.File;
      if (file != null)
      {
        text = text + " The file they picked: " + file;
      }
      string jobId = content.JobId;
      if (jobId != null)
      {
        this.agents.Jobs.MarkSeen(jobId);
      }
      string clickySlug = content.ClickySlug;
      if (clickySlug != null)
      {
        this.agents.LaunchAsync(new AgentLaunchRequest
        {
          Prompt = reply,
          ClickySlug = clickySlug,
          LaunchContext = text,
          LaunchSource = "text",
          IsHudFollowUp = true
        }, default(CancellationToken));
        string text2 = "clicky_dashboard_action";
        string text3 = clickySlug;
        string text4 = null;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["action"] = "result_card_answer";
        ClickyAnalytics.TrackProductEvent(text2, text3, text4, jsonObject);
        return;
      }
      string threadId = content.ThreadId;
      if (threadId == null)
      {
        return;
      }
      this.agents.LaunchAsync(new AgentLaunchRequest
      {
        Prompt = reply,
        JobId = content.JobId,
        TargetThreadId = threadId,
        LaunchContext = text,
        LaunchSource = "text",
        IsHudFollowUp = true
      }, default(CancellationToken));
      string text5 = "job_action";
      string text6 = null;
      string text7 = threadId;
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["action"] = "result_card_answer";
      ClickyAnalytics.TrackProductEvent(text5, text6, text7, jsonObject2);
    }

    // Token: 0x06002076 RID: 8310 RVA: 0x00617754 File Offset: 0x00617754
    public void CopyDraft(ArcCardContent content, string text)
    {
      DataPackage dataPackage = new DataPackage();
      dataPackage.SetText(text);
      try
      {
        Clipboard.SetContent(dataPackage);
      }
      catch (Exception ex)
      {
        Log.Warn("dock", "copy draft: " + ex.Message);
      }
      Sounds.Play("clicky-text-send", 0.2);
      string jobId = content.JobId;
      if (jobId != null)
      {
        this.agents.Jobs.MarkSeen(jobId);
      }
    }

    // Token: 0x06002077 RID: 8311 RVA: 0x006177D4 File Offset: 0x006177D4
    [return: Nullable(2)]
    public string ResultCardRoutineId(ArcCardContent content)
    {
      string clickySlug = content.ClickySlug;
      if (clickySlug == null)
      {
        return null;
      }
      ClickyRoutine clickyRoutine = this.agents.Routines.ForClicky(clickySlug).FirstOrDefault<ClickyRoutine>((ClickyRoutine r) => !r.IsPaused);
      if (clickyRoutine == null)
      {
        return null;
      }
      return clickyRoutine.Id;
    }

    // Token: 0x06002078 RID: 8312 RVA: 0x00617830 File Offset: 0x00617830
    public void RunRoutineNow(string routineId)
    {
      this.agents.Routines.RunNowAsync(routineId);
    }

    // Token: 0x06002079 RID: 8313 RVA: 0x00617844 File Offset: 0x00617844
    [return: Nullable(2)]
    public string ResultCardWhenLine(ArcCardContent content, ClickyAnswer answer)
    {
      if (!answer.IsQuiet && answer.NumbersLook.GetValueOrDefault() != ClickyAnswer.NumbersLookKind.Changes)
      {
        return null;
      }
      List<string> list = new List<string>();
      string jobId = content.JobId;
      DateTimeOffset? dateTimeOffset;
      if (jobId == null)
      {
        string clickySlug = content.ClickySlug;
        dateTimeOffset = ((clickySlug != null) ? this.agents.LiveStatusForClicky(clickySlug).FinishedAt : null);
      }
      else
      {
        ClickyJob clickyJob = this.agents.Jobs.Job(jobId);
        dateTimeOffset = ((clickyJob != null) ? clickyJob.FinishedAt : null);
      }
      DateTimeOffset? dateTimeOffset2 = dateTimeOffset;
      DateTimeOffset now = DateTimeOffset.Now;
      if (answer.IsQuiet && dateTimeOffset2 != null)
      {
        DateTimeOffset valueOrDefault = dateTimeOffset2.GetValueOrDefault();
        list.Add("Checked " + ResultCardLayouts.Moment(valueOrDefault, now));
      }
      string clickySlug2 = content.ClickySlug;
      ClickyRoutine clickyRoutine;
      if (clickySlug2 == null)
      {
        clickyRoutine = null;
      }
      else
      {
        clickyRoutine = this.agents.Routines.ForClicky(clickySlug2).FirstOrDefault<ClickyRoutine>((ClickyRoutine r) => !r.IsPaused);
      }
      ClickyRoutine clickyRoutine2 = clickyRoutine;
      if (clickyRoutine2 != null)
      {
        ClickyRoutine clickyRoutine3 = clickyRoutine2;
        if (clickyRoutine3.NextRunAt > now)
        {
          list.Add("next check " + ResultCardLayouts.Moment(clickyRoutine3.NextRunAt, now));
        }
      }
      if (list.Count == 0)
      {
        return null;
      }
      string text = string.Join(" · ", list);
      char c = char.ToUpperInvariant(text[0]);
      ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c);
      string text2 = text;
      return readOnlySpan + text2.Substring(1, text2.Length - 1);
    }

    // Token: 0x0600207A RID: 8314 RVA: 0x006179D0 File Offset: 0x006179D0
    public void OpenReadyOutput(string railKey, CardReadyOutput output)
    {
      string text = ClickyJob.JobIdFromRailKey(railKey);
      string text2 = ((text == null) ? railKey : null);
      string cardEntry = output.CardEntry;
      if (cardEntry == null)
      {
        this.MarkWindowRead(railKey);
        if (this.Host.HasLatestReply(text, text2))
        {
          this.Host.ShowFullReply(text, text2);
          return;
        }
        this.Host.ShowChat(text, text2);
        return;
      }
      else
      {
        this.MarkWindowRead(railKey);
        string text3;
        if (text == null)
        {
          text3 = this.agents.WorkspacePath(railKey);
        }
        else
        {
          ClickyJob clickyJob = this.agents.Jobs.Job(text);
          text3 = ((clickyJob != null) ? clickyJob.FolderPath : null);
        }
        string text4 = text3;
        string text5 = DockFiles.Resolve(cardEntry, text4);
        if (!this.Host.OpensInNotchDocumentViewer(text5))
        {
          this.Host.DismissNotch();
        }
        if (this.Host.OpenInNotchViewer(text5, text2, output.ThreadId))
        {
          return;
        }
        DockFiles.Open(cardEntry, text4);
        return;
      }
    }

    // Token: 0x0600207B RID: 8315 RVA: 0x00617AC0 File Offset: 0x00617AC0
    public void ShowJobThroughBrain(string railKey)
    {
      string text = ClickyJob.JobIdFromRailKey(railKey);
      if (text != null)
      {
        ClickyJob clickyJob = this.agents.Jobs.Job(text);
        if (clickyJob != null)
        {
          this.MarkWindowRead(railKey);
          this.Close(true, "Show me on a finished job");
          this.agents.Jobs.MarkSeen(text);
          string brief = ArcDock.JobResultBrief(clickyJob);
          string text2 = "job_action";
          string text3 = null;
          string threadId = clickyJob.ThreadId;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["action"] = "show_me";
          ClickyAnalytics.TrackProductEvent(text2, text3, threadId, jsonObject);
          DockTimer.After(0.35, delegate
          {
            Func<string, Task> submitFact = this.SubmitFact;
            if (submitFact != null)
            {
              submitFact(brief);
            }
          });
          return;
        }
      }
    }

    // Token: 0x0600207C RID: 8316 RVA: 0x00617B7C File Offset: 0x00617B7C
    public static string JobResultBrief(ClickyJob job)
    {
      List<string> list = new List<string>();
      list.Add("[Job result] The user pressed Show me on the job \"" + job.Title + "\". Present what it found: the answer in your first line, then draw it at the size it deserves (one card for a number or a pick, a card with rows or a small chart for a comparison or a plan, a timeline for a schedule). Use only what's below and never add facts. A few beats, then end by asking what they want to do with it.");
      string text = "Asked: ";
      IReadOnlyList<ClickyJobRun> runs = job.Runs;
      string text2;
      if (runs == null)
      {
        text2 = null;
      }
      else
      {
        ClickyJobRun clickyJobRun = runs.LastOrDefault<ClickyJobRun>();
        text2 = ((clickyJobRun != null) ? clickyJobRun.Ask : null);
      }
      list.Add(text + (text2 ?? job.Prompt));
      List<string> list2 = list;
      string summary = job.Summary;
      if (summary != null && summary.Length > 0)
      {
        list2.Add("Its answer in one line: " + summary);
      }
      string replyText = job.ReplyText;
      string text3 = ((replyText != null) ? replyText.Trim() : null);
      if (text3 != null && text3.Length > 0)
      {
        list2.Add("Its reply:\n" + ((text3.Length > 1200) ? text3.Substring(0, 1200) : text3));
      }
      try
      {
        string text4 = ArcLiveNotes.NotePath(job.FolderPath);
        if (File.Exists(text4))
        {
          string text5 = File.ReadAllText(text4);
          if (text5 != null && text5.Trim().Length > 0)
          {
            list2.Add("Its note (the page the user sees):\n" + ((text5.Length > 4000) ? text5.Substring(0, 4000) : text5));
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("dock", "show me note: " + ex.Message);
      }
      if (job.ArtifactEntries.Count > 0)
      {
        list2.Add("What it made (open one with open_artifact when they ask): " + string.Join(", ", job.ArtifactEntries.Take<string>(6)));
      }
      return string.Join("\n", list2);
    }

    // Token: 0x0600207D RID: 8317 RVA: 0x00617D20 File Offset: 0x00617D20
    public void LetClickyDrive(string taskId)
    {
      AgentDriveConsent driveConsent = this.DriveConsent;
      if (driveConsent != null)
      {
        driveConsent.LetClickyDriveForAgent(taskId);
        return;
      }
      Log.Warn("dock", "Let Clicky drive: Clicky drive isn't attached");
    }

    // Token: 0x0600207E RID: 8318 RVA: 0x00617D50 File Offset: 0x00617D50
    public void DeclineComputerUse(string taskId)
    {
      AgentDriveConsent driveConsent = this.DriveConsent;
      if (driveConsent != null)
      {
        driveConsent.DeclineComputerUse(taskId);
        return;
      }
      this.agents.SetPendingComputerUseRequest(taskId, null);
    }

    // Token: 0x0600207F RID: 8319 RVA: 0x00617D7C File Offset: 0x00617D7C
    public void Retry(ArcCardContent content)
    {
      string taskId = content.TaskId;
      if (taskId != null && (this.agents.CanRetryAfterFailure(taskId) || this.agents.CanRetryAfterInterruption(taskId)))
      {
        AgentTaskDetail taskDetail = this.agents.GetTaskDetail(taskId);
        string text = "agent_task_control_clicked";
        string text2 = ((taskDetail != null) ? taskDetail.ClickySlug : null);
        string text3 = ((taskDetail != null) ? taskDetail.Snapshot.ThreadId : null);
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["action"] = "retry";
        ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
        this.agents.RetryFailedTask(taskId);
        return;
      }
      this.RetryWithoutLiveTask(content.RailKey);
    }

    // Token: 0x06002080 RID: 8320 RVA: 0x00617E24 File Offset: 0x00617E24
    private void RetryWithoutLiveTask(string railKey)
    {
      string text = ClickyJob.JobIdFromRailKey(railKey);
      if (text != null)
      {
        ClickyJob clickyJob = this.agents.Jobs.Job(text);
        if (clickyJob != null)
        {
          string threadId = clickyJob.ThreadId;
          if (threadId != null)
          {
            this.agents.LaunchAsync(new AgentLaunchRequest
            {
              Prompt = "Retry the task you were working on and finish it.",
              JobId = text,
              TargetThreadId = threadId,
              LaunchContext = "The user tapped Retry on this task's window on the edge of their screen. The previous turn failed or was stopped partway through, so first check what this thread has already completed, keep that finished work, and continue from where it stopped instead of starting over.",
              LaunchSource = "retry",
              IsHudFollowUp = true
            }, default(CancellationToken));
            string text2 = "agent_task_control_clicked";
            string text3 = null;
            string text4 = threadId;
            JsonObject jsonObject = new JsonObject(null);
            jsonObject["action"] = "retry";
            jsonObject["from"] = "edge_dock_saved";
            ClickyAnalytics.TrackProductEvent(text2, text3, text4, jsonObject);
            return;
          }
        }
        return;
      }
      if (this.agents.FindClicky(railKey) != null)
      {
        this.agents.LaunchAsync(new AgentLaunchRequest
        {
          Prompt = "Retry the task you were working on and finish it.",
          ClickySlug = railKey,
          LaunchContext = "The user tapped Retry on this task's window on the edge of their screen. The previous turn failed or was stopped partway through, so first check what this thread has already completed, keep that finished work, and continue from where it stopped instead of starting over.",
          LaunchSource = "retry",
          IsHudFollowUp = true
        }, default(CancellationToken));
        string text5 = "agent_task_control_clicked";
        string text6 = null;
        JsonObject jsonObject2 = new JsonObject(null);
        jsonObject2["action"] = "retry";
        jsonObject2["from"] = "edge_dock_saved";
        ClickyAnalytics.TrackProductEvent(text5, railKey, text6, jsonObject2);
      }
    }

    // Token: 0x06002081 RID: 8321 RVA: 0x00617F94 File Offset: 0x00617F94
    public void ChooseChrome(ChromeSetupFlow.Choice choice, ArcCardContent content)
    {
      ChromeSetupFlow.Shared.Choose(choice, new ChromeSetupFlow.Target(content.JobId, content.ThreadId));
    }

    // Token: 0x06002082 RID: 8322 RVA: 0x00617FB4 File Offset: 0x00617FB4
    public void ContinueAfterUserStep(string jobId)
    {
      this.agents.ContinueJobAfterUserStep(jobId, null);
    }

    // Token: 0x06002083 RID: 8323 RVA: 0x00617FC4 File Offset: 0x00617FC4
    public void ApproveExtraUsage(string taskId, bool always)
    {
      this.ApproveExtraUsageAsync(taskId, always);
    }

    // Token: 0x06002084 RID: 8324 RVA: 0x00617FD0 File Offset: 0x00617FD0
    private async Task ApproveExtraUsageAsync(string taskId, bool always)
    {
      try
      {
        await this.agents.ApproveExtraUsageAsync(taskId, true, default(CancellationToken), always);
      }
      catch (Exception ex)
      {
        Log.Warn("dock", "extra usage approval: " + ex.Message);
      }
    }

    // Token: 0x170005EC RID: 1516
    // (get) Token: 0x06002085 RID: 8325 RVA: 0x00618024 File Offset: 0x00618024
    public ArcDockHost Host { get; }

    // Token: 0x06002086 RID: 8326 RVA: 0x0061802C File Offset: 0x0061802C
    public ArcDock(AgentService agents, ArcDockHost host, [Nullable(new byte[] { 1, 2 })] Func<OneBrainJobReports> jobReports, bool excludeFromCapture)
    {
      this.agents = agents;
      this.Host = host;
      this.jobReports = jobReports;
      this.dq = DispatcherQueue.GetForCurrentThread();
      this.source = new ArcCardContentSource(agents, jobReports, host);
      this.view = new ArcDockView(this, excludeFromCapture);
      this.pointerEventDriver = new PointerEvaluationDriver(60.0, 1.0, delegate
      {
        this.Tick();
        this.ScheduleNextTimedTick();
      });
      this.refreshDebounce = this.dq.CreateTimer();
      this.refreshDebounce.Interval = TimeSpan.FromMilliseconds(250L, 0L);
      this.refreshDebounce.IsRepeating = false;
      this.refreshDebounce.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.Refresh();
      };
      FileCardSound.PlaysSounds = () => this.Host.PlaysSounds();
    }

    // Token: 0x170005ED RID: 1517
    // (get) Token: 0x06002087 RID: 8327 RVA: 0x0061817C File Offset: 0x0061817C
    public bool ReplacesAgentHud
    {
      get
      {
        return this.isRunning && ClickyAppsPreferences.ShowsEdgeShelf;
      }
    }

    // Token: 0x170005EE RID: 1518
    // (get) Token: 0x06002088 RID: 8328 RVA: 0x00618190 File Offset: 0x00618190
    public bool IsRunning
    {
      get
      {
        return this.isRunning;
      }
    }

    // Token: 0x170005EF RID: 1519
    // (get) Token: 0x06002089 RID: 8329 RVA: 0x00618198 File Offset: 0x00618198
    public bool IsTabTucked
    {
      get
      {
        return this.isTabTucked;
      }
    }

    // Token: 0x170005F0 RID: 1520
    // (get) Token: 0x0600208A RID: 8330 RVA: 0x006181A0 File Offset: 0x006181A0
    public bool IsOpen
    {
      get
      {
        return this.isOpen;
      }
    }

    // Token: 0x0600208B RID: 8331 RVA: 0x006181A8 File Offset: 0x006181A8
    public void Start()
    {
      if (this.isRunning)
      {
        return;
      }
      this.isRunning = true;
      this.agents.TasksChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.RefreshSoon();
      };
      this.agents.TaskEvent += delegate([Nullable(2)] object _, AgentTaskEvent e)
      {
        AgentTaskEventKind kind = e.Kind;
        bool flag = kind - AgentTaskEventKind.Finished <= 3 || kind == AgentTaskEventKind.PlanUpdated;
        if (flag)
        {
          this.RefreshSoon();
        }
      };
      this.agents.Jobs.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.RefreshSoon();
      };
      this.agents.LiveSteps.Changed += delegate(string _)
      {
        this.RefreshSoon();
      };
      this.agents.ResultCardShown += delegate(string _)
      {
        this.RefreshSoon();
      };
      this.agents.ClickyApps.Changed += delegate(string _)
      {
        this.RefreshSoon();
      };
      OneBrainJobReports oneBrainJobReports = this.jobReports();
      if (oneBrainJobReports != null)
      {
        oneBrainJobReports.PresentingChanged += this.RefreshSoon;
      }
      ClickyInkNoteStore.Shared.RevisionChanged += this.RefreshSoon;
      this.membershipWatch.Note(this.ShowsDock, this.Host.LiveSuggestionCardIds());
      this.Refresh();
      Log.Info("dock", "the agent dock is running (it stands in for the HUD)");
    }

    // Token: 0x0600208C RID: 8332 RVA: 0x006182CC File Offset: 0x006182CC
    private void RefreshSoon()
    {
      this.dq.TryEnqueue(delegate
      {
        this.refreshDebounce.Stop();
        this.refreshDebounce.Start();
      });
    }

    // Token: 0x0600208D RID: 8333 RVA: 0x006182E8 File Offset: 0x006182E8
    public void NoteMembershipInputsChanged()
    {
      this.dq.TryEnqueue(delegate
      {
        if (!this.isRunning)
        {
          return;
        }
        if (this.membershipWatch.Note(this.ShowsDock, this.Host.LiveSuggestionCardIds()))
        {
          this.RefreshSoon();
        }
      });
    }

    // Token: 0x0600208E RID: 8334 RVA: 0x00618304 File Offset: 0x00618304
    public void NotchChanged()
    {
      this.dq.TryEnqueue(delegate
      {
        this.notchMovingUntil = PointerEvaluationDriver.Now + 0.6;
        this.UpdateNotchState();
        this.Tick();
        this.ScheduleNextTimedTick();
      });
    }

    // Token: 0x0600208F RID: 8335 RVA: 0x00618320 File Offset: 0x00618320
    [NullableContext(2)]
    public void NotchTabChanged(bool isHome, string tab = null)
    {
      if (!isHome && this.isOpen)
      {
        this.deckFocusKeyToRestore = this.FrontWindowKey;
        this.Close(true, (tab == null) ? "notch tab changed" : ("notch tab → " + tab));
      }
      if (isHome)
      {
        this.UpdateNotchState();
      }
      this.Tick();
    }

    // Token: 0x06002090 RID: 8336 RVA: 0x00618370 File Offset: 0x00618370
    public void CloseTopArc(string reason = "the notch's gear")
    {
      if (!this.isOpen)
      {
        return;
      }
      this.Close(true, reason);
    }

    // Token: 0x170005F1 RID: 1521
    // (get) Token: 0x06002091 RID: 8337 RVA: 0x00618384 File Offset: 0x00618384
    [TupleElementNames(new string[] { "W", "H" })]
    [Nullable(0)]
    public ValueTuple<double, double>? TopArcFrontCardSize
    {
      [NullableContext(0)]
      [return: TupleElementNames(new string[] { "W", "H" })]
      get
      {
        if (this.isRunning)
        {
          ArcScreenMetrics? arcScreenMetrics = this.metrics;
          if (arcScreenMetrics != null)
          {
            ArcScreenMetrics valueOrDefault = arcScreenMetrics.GetValueOrDefault();
            if (this.view.IsShowing)
            {
              if (this.faceFrames.Count == 0 && this.DeckKeys().Count == 0)
              {
                return null;
              }
              return new ValueTuple<double, double>?(TopArcLayout.FrontCardSize(valueOrDefault.PanelWidth, valueOrDefault.PanelHeight));
            }
          }
        }
        return null;
      }
    }

    // Token: 0x1400007A RID: 122
    // (add) Token: 0x06002092 RID: 8338 RVA: 0x00618404 File Offset: 0x00618404
    // (remove) Token: 0x06002093 RID: 8339 RVA: 0x0061843C File Offset: 0x0061843C
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action FrontCardSizeChanged;

    // Token: 0x06002094 RID: 8340 RVA: 0x00618474 File Offset: 0x00618474
    private void NotifyIfFrontCardSizeChanged()
    {
      ValueTuple<double, double>? topArcFrontCardSize = this.TopArcFrontCardSize;
      ValueTuple<double, double>? valueTuple = topArcFrontCardSize;
      ValueTuple<double, double>? valueTuple2 = this.lastAnnouncedFrontCardSize;
      bool flag = valueTuple != null;
      bool flag2;
      if (flag != (valueTuple2 != null))
      {
        flag2 = false;
      }
      else if (!flag)
      {
        flag2 = true;
      }
      else
      {
        ValueTuple<double, double> valueOrDefault = valueTuple.GetValueOrDefault();
        ValueTuple<double, double> valueOrDefault2 = valueTuple2.GetValueOrDefault();
        flag2 = valueOrDefault.Item1 == valueOrDefault2.Item1 && valueOrDefault.Item2 == valueOrDefault2.Item2;
      }
      if (flag2)
      {
        return;
      }
      this.lastAnnouncedFrontCardSize = topArcFrontCardSize;
      Action frontCardSizeChanged = this.FrontCardSizeChanged;
      if (frontCardSizeChanged == null)
      {
        return;
      }
      frontCardSizeChanged();
    }

    // Token: 0x170005F2 RID: 1522
    // (get) Token: 0x06002095 RID: 8341 RVA: 0x006184FC File Offset: 0x006184FC
    // (set) Token: 0x06002096 RID: 8342 RVA: 0x00618504 File Offset: 0x00618504
    public bool IsTourShowcase
    {
      get
      {
        return this.isTourShowcase;
      }
      set
      {
        this.isTourShowcase = value;
        this.SyncDeckPointer();
        this.view.Layout(LayoutReason.Open, true);
      }
    }

    // Token: 0x170005F3 RID: 1523
    // (get) Token: 0x06002097 RID: 8343 RVA: 0x00618520 File Offset: 0x00618520
    private List<string> DockedClickys
    {
      get
      {
        List<string> list;
        if ((list = this.dockedClickys) == null)
        {
          list = (this.dockedClickys = AgentDefaults.Get<List<string>>("clicky.clickyApps.dockedUnpinnedSlugs.v1") ?? new List<string>());
        }
        return list;
      }
    }

    // Token: 0x06002098 RID: 8344 RVA: 0x00618554 File Offset: 0x00618554
    private void AddDockedClicky(string slug)
    {
      if (this.DockedClickys.Contains(slug))
      {
        return;
      }
      this.DockedClickys.Add(slug);
      AgentDefaults.Set<List<string>>("clicky.clickyApps.dockedUnpinnedSlugs.v1", this.DockedClickys);
    }

    // Token: 0x06002099 RID: 8345 RVA: 0x00618584 File Offset: 0x00618584
    public void DockLegacyClickys(IEnumerable<string> slugs)
    {
      foreach (string text in slugs)
      {
        this.AddDockedClicky(text);
      }
      this.Refresh();
    }

    // Token: 0x0600209A RID: 8346 RVA: 0x006185D4 File Offset: 0x006185D4
    private void RemoveDockedClicky(string slug)
    {
      if (!this.DockedClickys.Remove(slug))
      {
        return;
      }
      AgentDefaults.Set<List<string>>("clicky.clickyApps.dockedUnpinnedSlugs.v1", this.DockedClickys);
    }

    // Token: 0x0600209B RID: 8347 RVA: 0x006185F8 File Offset: 0x006185F8
    public void RemoveAllForSignOut()
    {
      this.dockedClickys = new List<string>();
      AgentDefaults.Remove("clicky.clickyApps.dockedUnpinnedSlugs.v1");
      this.closedFromDockAt.Clear();
      this.Refresh();
    }

    // Token: 0x170005F4 RID: 1524
    // (get) Token: 0x0600209C RID: 8348 RVA: 0x00618620 File Offset: 0x00618620
    private bool ShowsDock
    {
      get
      {
        return this.Host.HasCompletedOnboarding() && !this.Host.OnboardingHidesDock();
      }
    }

    // Token: 0x0600209D RID: 8349 RVA: 0x0061864C File Offset: 0x0061864C
    public IReadOnlyList<string> ShelfClickys()
    {
      if (!this.ShowsDock)
      {
        return Array.Empty<string>();
      }
      HashSet<string> hashSet = new HashSet<string>();
      List<string> list = new List<string>();
      foreach (AgentTaskSnapshot agentTaskSnapshot in this.agents.Tasks)
      {
        AgentTaskDetail taskDetail = this.agents.GetTaskDetail(agentTaskSnapshot.TaskId);
        if (taskDetail != null && !taskDetail.IsDemo)
        {
          string clickySlug = taskDetail.ClickySlug;
          if (clickySlug != null && taskDetail.JobId == null && !hashSet.Contains(clickySlug))
          {
            bool flag = this.agents.PendingComputerUseRequest(agentTaskSnapshot.TaskId) != null || taskDetail.RequiresExtraUsageApproval;
            AgentTaskStatus status = agentTaskSnapshot.Status;
            bool flag2 = status <= AgentTaskStatus.Running;
            bool flag3 = flag2 || flag;
            DateTimeOffset dateTimeOffset;
            if ((!this.closedFromDockAt.TryGetValue(clickySlug, out dateTimeOffset) || flag || !(agentTaskSnapshot.StartedAt < dateTimeOffset)) && flag3 && this.agents.FindClicky(clickySlug) != null)
            {
              hashSet.Add(clickySlug);
              list.Add(clickySlug);
            }
          }
        }
      }
      foreach (string text in this.Host.UnreadClickySlugs())
      {
        if (!hashSet.Contains(text) && this.agents.FindClicky(text) != null)
        {
          hashSet.Add(text);
          list.Add(text);
        }
      }
      foreach (string text2 in list)
      {
        this.AddDockedClicky(text2);
      }
      foreach (string text3 in this.DockedClickys.ToList<string>())
      {
        if (!hashSet.Contains(text3) && this.agents.FindClicky(text3) != null)
        {
          hashSet.Add(text3);
          list.Add(text3);
        }
      }
      return list.Take<string>(30).ToList<string>();
    }

    // Token: 0x0600209E RID: 8350 RVA: 0x006188B4 File Offset: 0x006188B4
    public IReadOnlyList<ClickyJob> ShelfJobs()
    {
      if (!ClickyJobsPreferences.IsEnabled || !this.ShowsDock)
      {
        return Array.Empty<ClickyJob>();
      }
      ISet<Guid> liveCards = this.Host.LiveSuggestionCardIds();
      return this.agents.Jobs.RailJobs.Where<ClickyJob>(delegate(ClickyJob job)
      {
        if (job.IsWaitingToStart)
        {
          Guid? suggestionCardId = job.SuggestionCardId;
          if (suggestionCardId != null)
          {
            Guid valueOrDefault = suggestionCardId.GetValueOrDefault();
            ISet<Guid> liveCards2 = liveCards;
            return liveCards2 == null || liveCards2.Contains(valueOrDefault);
          }
        }
        return true;
      }).ToList<ClickyJob>();
    }

    // Token: 0x0600209F RID: 8351 RVA: 0x00618918 File Offset: 0x00618918
    public IReadOnlyList<DockWindowKey> DeckWindows()
    {
      return DockOrder.DeckOrder(this.ShelfClickys(), this.ShelfJobs(), new Func<string, DockRecency>(this.ArcRecency));
    }

    // Token: 0x060020A0 RID: 8352 RVA: 0x00618938 File Offset: 0x00618938
    private IReadOnlyList<string> DeckKeys()
    {
      return (from w in this.DeckWindows()
        select w.Key).ToList<string>();
    }

    // Token: 0x060020A1 RID: 8353 RVA: 0x0061896C File Offset: 0x0061896C
    public IReadOnlyList<string> DockKeys()
    {
      return this.DeckKeys();
    }

    // Token: 0x060020A2 RID: 8354 RVA: 0x00618974 File Offset: 0x00618974
    public DateTimeOffset LatestActivity(string key)
    {
      return this.ArcRecency(key).Date;
    }

    // Token: 0x060020A3 RID: 8355 RVA: 0x00618990 File Offset: 0x00618990
    private DockRecency ArcRecency(string key)
    {
      DateTimeOffset date = this.EdgeDockRecency(key).Date;
      string text = ClickyJob.JobIdFromRailKey(key);
      string text2 = ((text != null) ? ("job:" + text) : ("clicky:" + key));
      DateTimeOffset dateTimeOffset = this.Host.LatestChatEventAt(text2) ?? DateTimeOffset.MinValue;
      return new DockRecency(false, (date > dateTimeOffset) ? date : dateTimeOffset);
    }

    // Token: 0x060020A4 RID: 8356 RVA: 0x00618A10 File Offset: 0x00618A10
    private DockRecency EdgeDockRecency(string key)
    {
      string text = ClickyJob.JobIdFromRailKey(key);
      if (text == null)
      {
        string text2 = this.source.ClickyTaskId(key);
        if (text2 != null)
        {
          AgentTaskDetail taskDetail = this.agents.GetTaskDetail(text2);
          if (taskDetail != null)
          {
            AgentTaskStatus status = taskDetail.Snapshot.Status;
            bool flag = status <= AgentTaskStatus.Running;
            bool flag2 = flag || this.agents.PendingComputerUseRequest(text2) != null || taskDetail.RequiresExtraUsageApproval;
            return new DockRecency(flag2, flag2 ? taskDetail.Snapshot.StartedAt : (this.agents.LiveStatusForClicky(key).FinishedAt ?? taskDetail.Snapshot.StartedAt));
          }
        }
        ClickyWorkPiece clickyWorkPiece = (from w in this.agents.ClickyApps.Works(key)
          orderby w.FinishedAt descending
          select w).FirstOrDefault<ClickyWorkPiece>();
        return new DockRecency(false, (clickyWorkPiece != null) ? clickyWorkPiece.FinishedAt : DateTimeOffset.MinValue);
      }
      ClickyJob clickyJob = this.agents.Jobs.Job(text);
      if (clickyJob == null)
      {
        return new DockRecency(false, DateTimeOffset.MinValue);
      }
      string text3 = this.agents.LiveTaskId(clickyJob.Id);
      AgentTaskDetail agentTaskDetail = ((text3 != null) ? this.agents.GetTaskDetail(text3) : null);
      ClickyJobRailPhase clickyJobRailPhase = this.agents.RailPhase(clickyJob);
      DockRecency dockRecency;
      if (clickyJobRailPhase <= ClickyJobRailPhase.NeedsYou)
      {
        dockRecency = new DockRecency(true, (agentTaskDetail != null) ? agentTaskDetail.Snapshot.StartedAt : clickyJob.CreatedAt);
      }
      else
      {
        dockRecency = new DockRecency(false, clickyJob.FinishedAt ?? clickyJob.CreatedAt);
      }
      return dockRecency;
    }

    // Token: 0x060020A5 RID: 8357 RVA: 0x00618BCC File Offset: 0x00618BCC
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "Working", "NeedsYou", "Failed" })]
    private ValueTuple<int, int, bool> Counts()
    {
      int num = 0;
      int num2 = 0;
      bool flag = false;
      OneBrainJobReports oneBrainJobReports = this.jobReports();
      foreach (string text in this.ShelfClickys())
      {
        switch (this.agents.LiveStatusForClicky(text).Phase)
        {
        case ClickyAppLiveStatus.Phases.Working:
          num++;
          break;
        case ClickyAppLiveStatus.Phases.NeedsYou:
          num2++;
          break;
        case ClickyAppLiveStatus.Phases.Finished:
          if (oneBrainJobReports != null && oneBrainJobReports.IsFinalizingResult(text))
          {
            num++;
          }
          break;
        case ClickyAppLiveStatus.Phases.Failed:
          flag = true;
          break;
        }
      }
      foreach (ClickyJob clickyJob in this.ShelfJobs())
      {
        switch (this.agents.RailPhase(clickyJob))
        {
        case ClickyJobRailPhase.Queued:
        case ClickyJobRailPhase.Working:
          num++;
          break;
        case ClickyJobRailPhase.NeedsYou:
          num2++;
          break;
        case ClickyJobRailPhase.Done:
          if (oneBrainJobReports != null && oneBrainJobReports.IsFinalizingResult(clickyJob))
          {
            num++;
          }
          break;
        case ClickyJobRailPhase.Failed:
          flag = true;
          break;
        }
      }
      return new ValueTuple<int, int, bool>(num, num2, flag);
    }

    // Token: 0x060020A6 RID: 8358 RVA: 0x00618D0C File Offset: 0x00618D0C
    public string StatusLine()
    {
      ValueTuple<int, int, bool> valueTuple = this.Counts();
      int item = valueTuple.Item1;
      int item2 = valueTuple.Item2;
      return TopArcGreetingLayout.StatusLine(item, item2, this.Host.VoiceHoldDescription());
    }

    // Token: 0x060020A7 RID: 8359 RVA: 0x00618D44 File Offset: 0x00618D44
    [return: TupleElementNames(new string[] { "WorkingCount", "AlertPhase" })]
    [return: Nullable(new byte[] { 0, 1 })]
    public ValueTuple<int, string> RestingMark()
    {
      ValueTuple<int, int, bool> valueTuple = this.Counts();
      int item = valueTuple.Item1;
      int item2 = valueTuple.Item2;
      bool item3 = valueTuple.Item3;
      return new ValueTuple<int, string>(item, (item2 > 0) ? "needsYou" : (item3 ? "failed" : "idle"));
    }

    // Token: 0x060020A8 RID: 8360 RVA: 0x00618D8C File Offset: 0x00618D8C
    public ArcCardContent Content(DockWindowKey window)
    {
      ClickyJob job = window.Job;
      if (job == null)
      {
        return this.source.ForClicky(window.ClickySlug);
      }
      return this.source.ForJob(this.agents.Jobs.Job(job.Id) ?? job);
    }

    // Token: 0x060020A9 RID: 8361 RVA: 0x00618DE0 File Offset: 0x00618DE0
    [return: Nullable(2)]
    public ArcCardContent ContentForKey(string railKey)
    {
      string text = ClickyJob.JobIdFromRailKey(railKey);
      if (text != null)
      {
        ClickyJob clickyJob = this.agents.Jobs.Job(text);
        if (clickyJob == null)
        {
          return null;
        }
        return this.source.ForJob(clickyJob);
      }
      else
      {
        if (this.agents.FindClicky(railKey) != null)
        {
          return this.source.ForClicky(railKey);
        }
        return null;
      }
    }

    // Token: 0x1400007B RID: 123
    // (add) Token: 0x060020AA RID: 8362 RVA: 0x00618E38 File Offset: 0x00618E38
    // (remove) Token: 0x060020AB RID: 8363 RVA: 0x00618E70 File Offset: 0x00618E70
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action ContentChanged;

    // Token: 0x060020AC RID: 8364 RVA: 0x00618EA8 File Offset: 0x00618EA8
    public void KeepAboveNotch()
    {
      this.view.KeepZOrder();
    }

    // Token: 0x170005F5 RID: 1525
    // (get) Token: 0x060020AD RID: 8365 RVA: 0x00618EB8 File Offset: 0x00618EB8
    public IntPtr DeckHwnd
    {
      get
      {
        return this.view.DeckHwnd;
      }
    }

    // Token: 0x060020AE RID: 8366 RVA: 0x00618EC8 File Offset: 0x00618EC8
    public void RebuildAfterLayoutCycle()
    {
      this.view.RebuildCards();
      this.Refresh();
    }

    // Token: 0x060020AF RID: 8367 RVA: 0x00618EDC File Offset: 0x00618EDC
    public void Refresh()
    {
      if (!this.isRunning)
      {
        return;
      }
      Action contentChanged = this.ContentChanged;
      if (contentChanged != null)
      {
        contentChanged();
      }
      bool showsEdgeShelf = ClickyAppsPreferences.ShowsEdgeShelf;
      bool showsDock = this.ShowsDock;
      bool flag = this.Host.SuppressesVisualSurfaces();
      if (!showsEdgeShelf || !showsDock || flag)
      {
        if (this.view.IsShowing)
        {
          this.HidePanel((!showsEdgeShelf) ? "dock switched off" : ((!showsDock) ? "dock not shown (setup not done or signed out)" : "HeyClicky's visuals suppressed"));
        }
        return;
      }
      this.ShowPanel();
      IReadOnlyList<DockWindowKey> readOnlyList = this.DeckWindows();
      this.deckWindowCountForArrowKeys = readOnlyList.Count;
      this.view.Sync(readOnlyList);
      this.NotifyIfFrontCardSizeChanged();
      this.pointerEventDriver.EvaluateSoon();
    }

    // Token: 0x060020B0 RID: 8368 RVA: 0x00618F94 File Offset: 0x00618F94
    private void ShowPanel()
    {
      this.UpdateNotchState();
      this.view.Show();
      this.pointerEventDriver.Start();
    }

    // Token: 0x060020B1 RID: 8369 RVA: 0x00618FB4 File Offset: 0x00618FB4
    private void HidePanel(string reason)
    {
      if (this.isOpen)
      {
        this.LogArc("close", "panel hidden: " + reason, null);
      }
      this.Host.SetNotchHeldOpenByArc(false);
      this.pointerEventDriver.Stop();
      this.isOpen = false;
      this.hoveredKey = null;
      this.view.Hide();
      this.NotifyIfFrontCardSizeChanged();
    }

    // Token: 0x060020B2 RID: 8370 RVA: 0x0061901C File Offset: 0x0061901C
    internal Displays.Monitor HostMonitor()
    {
      ValueTuple<int, int, int, int>? valueTuple = this.Host.NotchSurfacePx();
      if (valueTuple != null)
      {
        ValueTuple<int, int, int, int> valueOrDefault = valueTuple.GetValueOrDefault();
        if (valueOrDefault.Item3 > 0)
        {
          return Displays.At(valueOrDefault.Item1 + valueOrDefault.Item3 / 2, valueOrDefault.Item2 + Math.Max(1, valueOrDefault.Item4 / 2));
        }
      }
      return Displays.Primary();
    }

    // Token: 0x170005F6 RID: 1526
    // (get) Token: 0x060020B3 RID: 8371 RVA: 0x00619084 File Offset: 0x00619084
    internal ArcScreenMetrics? Metrics
    {
      get
      {
        return this.metrics;
      }
    }

    // Token: 0x060020B4 RID: 8372 RVA: 0x0061908C File Offset: 0x0061908C
    private bool UpdateNotchState()
    {
      Displays.Monitor monitor = this.HostMonitor();
      double scale = monitor.Scale;
      double num = (double)monitor.Bounds.Width / scale;
      double num2 = (double)monitor.Bounds.Height / scale;
      double num3 = Math.Max(0.0, (double)(monitor.Work.Top - monitor.Bounds.Top) / scale);
      ValueTuple<int, int, int, int>? valueTuple = this.Host.NotchSurfacePx();
      ArcRect? arcRect;
      if (valueTuple != null)
      {
        ValueTuple<int, int, int, int> valueOrDefault = valueTuple.GetValueOrDefault();
        if (valueOrDefault.Item3 > 0 && valueOrDefault.Item4 > 0)
        {
          arcRect = new ArcRect?(new ArcRect((double)(valueOrDefault.Item1 - monitor.Bounds.Left) / scale, (double)(valueOrDefault.Item2 - monitor.Bounds.Top) / scale, (double)valueOrDefault.Item3 / scale, (double)valueOrDefault.Item4 / scale));
          goto IL_00F3;
        }
      }
      arcRect = null;
      IL_00F3:
      ArcRect? arcRect2 = arcRect;
      ArcScreenMetrics arcScreenMetrics = new ArcScreenMetrics(num, num2, Math.Ceiling(num3), null, arcRect2);
      this.isNotchExpanded = this.Host.NotchIsExpanded();
      bool flag = this.Host.IsFullscreenOnDockDisplay();
      if (flag != this.isTabTucked)
      {
        this.isTabTucked = flag;
      }
      if (this.metrics == arcScreenMetrics)
      {
        return false;
      }
      ArcScreenMetrics? arcScreenMetrics2 = this.metrics;
      bool flag2 = arcScreenMetrics2 != null && this.view.Monitor.Handle != monitor.Handle;
      this.metrics = new ArcScreenMetrics?(arcScreenMetrics);
      this.view.Place(monitor);
      this.view.Layout(LayoutReason.Metrics, !flag2);
      this.NotifyIfFrontCardSizeChanged();
      return true;
    }

    // Token: 0x170005F7 RID: 1527
    // (get) Token: 0x060020B5 RID: 8373 RVA: 0x00619278 File Offset: 0x00619278
    private bool IsGreetingNotchOpen
    {
      get
      {
        return this.isNotchExpanded && this.Host.NotchIsOpenOnHome();
      }
    }

    // Token: 0x060020B6 RID: 8374 RVA: 0x00619294 File Offset: 0x00619294
    private void SyncGreetingNotchHold()
    {
      this.Host.SetNotchHeldOpenByArc(this.isOpen);
    }

    // Token: 0x060020B7 RID: 8375 RVA: 0x006192AC File Offset: 0x006192AC
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "X", "Y" })]
    private ValueTuple<double, double> PointerInPanel()
    {
      ValueTuple<double, double>? demoPointer = this.DemoPointer;
      if (demoPointer != null)
      {
        return demoPointer.GetValueOrDefault();
      }
      Native.POINT point;
      Native.GetCursorPos(out point);
      Displays.Monitor monitor = this.view.Monitor;
      return new ValueTuple<double, double>((double)(point.X - monitor.Bounds.Left) / monitor.Scale, (double)(point.Y - monitor.Bounds.Top) / monitor.Scale);
    }

    // Token: 0x060020B8 RID: 8376 RVA: 0x00619324 File Offset: 0x00619324
    private void ScheduleNextTimedTick()
    {
      if (!this.pointerEventDriver.IsRunning)
      {
        return;
      }
      double now = PointerEvaluationDriver.Now;
      double? num = null;
      if (this.isOpen)
      {
        DateTimeOffset? dateTimeOffset = this.leftAt;
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
          double num2 = now + (valueOrDefault - DateTimeOffset.Now).TotalSeconds + this.CloseDelay + 0.001;
          if (num2 > now)
          {
            num = new double?(num2);
          }
        }
      }
      if (this.isOpen && this.IsScrollGestureGoingOn)
      {
        double num3 = now + (this.lastScrollEventAt - DateTimeOffset.Now).TotalSeconds + 0.22 + 0.01;
        double num4;
        if (num != null)
        {
          double valueOrDefault2 = num.GetValueOrDefault();
          num4 = Math.Min(valueOrDefault2, num3);
        }
        else
        {
          num4 = num3;
        }
        num = new double?(num4);
      }
      if (this.notchMovingUntil > now)
      {
        double num5 = Math.Max(now, this.lastNotchReadAt + 0.1);
        double num6;
        if (num != null)
        {
          double valueOrDefault3 = num.GetValueOrDefault();
          num6 = Math.Min(valueOrDefault3, num5);
        }
        else
        {
          num6 = num5;
        }
        num = new double?(num6);
      }
      this.pointerEventDriver.EvaluateAt(num);
    }

    // Token: 0x170005F8 RID: 1528
    // (get) Token: 0x060020B9 RID: 8377 RVA: 0x00619464 File Offset: 0x00619464
    private double CloseDelay
    {
      get
      {
        if (this.hoveredKey != null)
        {
          return 0.45;
        }
        return 0.3;
      }
    }

    // Token: 0x170005F9 RID: 1529
    // (get) Token: 0x060020BA RID: 8378 RVA: 0x00619484 File Offset: 0x00619484
    private bool IsWindowHeldOpen
    {
      get
      {
        return this.voiceFollowUpKey != null && this.Host.IsListening();
      }
    }

    // Token: 0x060020BB RID: 8379 RVA: 0x006194A4 File Offset: 0x006194A4
    private void Tick()
    {
      this.Evaluate();
      this.SyncDeckPointer();
    }

    // Token: 0x060020BC RID: 8380 RVA: 0x006194B4 File Offset: 0x006194B4
    private void SyncDeckPointer()
    {
      if (!this.view.IsShowing)
      {
        this.SetPointerOnNotch(false);
        return;
      }
      ValueTuple<double, double> valueTuple = this.PointerInPanel();
      ArcRect? arcRect = ((this.metrics != null) ? this.metrics.GetValueOrDefault().NotchSurface : null);
      this.SetPointerOnNotch(arcRect != null && arcRect.GetValueOrDefault().Contains(valueTuple.Item1, valueTuple.Item2));
      double num = (double)(this.view.TakesPointer ? 6 : 0);
      bool flag = this.isOpen && !this.isTourShowcase && !this.isAutoPresentedWindowAwaitingVisit && this.IsPointerOverInteractiveArea(valueTuple, num);
      if (!flag && this.isOpen && this.view.TakesPointer && ((int)Native.GetAsyncKeyState(1) & 32768) != 0)
      {
        return;
      }
      if (!flag && this.isOpen && this.view.TakesPointer && this.IsScrollGestureGoingOn)
      {
        return;
      }
      if (flag != this.view.TakesPointer)
      {
        this.view.SetTakesPointer(flag);
      }
    }

    // Token: 0x1400007C RID: 124
    // (add) Token: 0x060020BD RID: 8381 RVA: 0x006195D0 File Offset: 0x006195D0
    // (remove) Token: 0x060020BE RID: 8382 RVA: 0x00619608 File Offset: 0x00619608
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<bool> PointerOnNotchChanged;

    // Token: 0x060020BF RID: 8383 RVA: 0x00619640 File Offset: 0x00619640
    private void SetPointerOnNotch(bool on)
    {
      if (on == this.pointerOnNotch)
      {
        return;
      }
      this.pointerOnNotch = on;
      Action<bool> pointerOnNotchChanged = this.PointerOnNotchChanged;
      if (pointerOnNotchChanged == null)
      {
        return;
      }
      pointerOnNotchChanged(on);
    }

    // Token: 0x060020C0 RID: 8384 RVA: 0x00619664 File Offset: 0x00619664
    private void Evaluate()
    {
      if (this.view.IsShowing)
      {
        ArcScreenMetrics? arcScreenMetrics = this.metrics;
        if (arcScreenMetrics != null)
        {
          ArcScreenMetrics valueOrDefault = arcScreenMetrics.GetValueOrDefault();
          DateTimeOffset now = DateTimeOffset.Now;
          double now2 = PointerEvaluationDriver.Now;
          if (now2 - this.lastNotchReadAt >= 0.1)
          {
            this.lastNotchReadAt = now2;
            if (this.UpdateNotchState())
            {
              this.notchMovingUntil = Math.Max(this.notchMovingUntil, now2 + 0.6);
            }
            if (this.isOpen)
            {
              this.view.KeepZOrder();
            }
          }
          this.SyncGreetingNotchHold();
          if ((now - this.lastStatusTickAt).TotalSeconds > 20.0)
          {
            this.lastStatusTickAt = now;
            this.Refresh();
          }
          if (this.DemoHoldsOpen)
          {
            this.leftAt = null;
            return;
          }
          if (this.deckFocusKeyToRestore != null && !this.IsGreetingNotchOpen)
          {
            this.deckFocusKeyToRestore = null;
          }
          ValueTuple<double, double> pointer = this.PointerInPanel();
          this.cardKeyUnderPointer = ((this.hoveredKey != null && this.cardFrame.Inset(-2.0, -2.0).Contains(pointer.Item1, pointer.Item2)) ? this.hoveredKey : null);
          if (this.isTourShowcase)
          {
            if (!this.isOpen && this.IsGreetingNotchOpen && this.faceFrames.Count > 0)
            {
              this.OpenRail(pointer, "the welcome tour showing the notch");
            }
            this.leftAt = null;
            return;
          }
          if (this.IsWindowHeldOpen && this.hoveredKey != null)
          {
            this.leftAt = null;
            return;
          }
          if (!this.isOpen && (now - this.lastSoundPrepareAt).TotalSeconds > 5.0 && this.Host.PlaysSounds() && valueOrDefault.NotchTriggerFrame.Inset(-60.0, -30.0).Contains(pointer.Item1, pointer.Item2))
          {
            this.lastSoundPrepareAt = now;
            try
            {
              ScreenDrawingSounds.Prepare();
            }
            catch (Exception ex)
            {
              Log.Warn("dock", "sound prepare: " + ex.Message);
            }
          }
          if (this.isAutoPresentedWindowAwaitingVisit)
          {
            if (this.hoveredKey != null && !this.IsPointerOverInteractiveArea(pointer, 0.0))
            {
              this.leftAt = null;
              return;
            }
            this.isAutoPresentedWindowAwaitingVisit = false;
          }
          if (this.isOpen && !this.Host.NotchTabIsHome())
          {
            this.Close(true, "notch not on Home");
            return;
          }
          if (!this.isOpen && this.IsGreetingNotchOpen && this.faceFrames.Count > 0)
          {
            this.leftAt = null;
            this.OpenRail(pointer, "notch open on Home");
          }
          ValueTuple<double, double>? valueTuple = this.railOpenedAtPointer;
          if (valueTuple != null)
          {
            ValueTuple<double, double> valueOrDefault2 = valueTuple.GetValueOrDefault();
            if (Math.Sqrt(Math.Pow(pointer.Item1 - valueOrDefault2.Item1, 2.0) + Math.Pow(pointer.Item2 - valueOrDefault2.Item2, 2.0)) > 8.0)
            {
              this.railOpenedAtPointer = null;
            }
          }
          ArcRect? arcRect = this.pressedMoreFrame;
          if (arcRect != null && !arcRect.GetValueOrDefault().Inset(-4.0, -4.0).Contains(pointer.Item1, pointer.Item2))
          {
            this.pressedMoreFrame = null;
          }
          string text;
          if (this.isOpen)
          {
            valueTuple = this.railOpenedAtPointer;
            if (valueTuple == null)
            {
              text = this.faceFrames.FirstOrDefault<KeyValuePair<string, ArcRect>>((KeyValuePair<string, ArcRect> f) => f.Value.Inset(-4.0, -6.0).Contains(pointer.Item1, pointer.Item2)).Key;
              goto IL_0413;
            }
          }
          text = null;
          IL_0413:
          string text2 = text;
          if (this.isOpen && this.hoveredKey != null && this.cardFrame.Contains(pointer.Item1, pointer.Item2))
          {
            this.leftAt = null;
            return;
          }
          if (text2 != null)
          {
            this.leftAt = null;
            this.Open(text2, "pointer on a window");
            return;
          }
          if (this.hoveredKey != null && this.cardFrame.Inset(-18.0, -18.0).Contains(pointer.Item1, pointer.Item2))
          {
            this.leftAt = null;
            return;
          }
          if (this.isOpen && (this.railFrame.Inset(-10.0, -10.0).Contains(pointer.Item1, pointer.Item2) || ArcDock.OpenDeckEdgeStrip(valueOrDefault).Contains(pointer.Item1, pointer.Item2) || this.IsPointerOnMoreButton(pointer) || ArcDock.IsPointerOnTopArcNotch(valueOrDefault, pointer)))
          {
            this.leftAt = null;
            return;
          }
          if (this.isOpen && this.IsGreetingNotchOpen && this.Host.NotchHoldsOpenWithoutArc())
          {
            if (this.hoveredKey == null)
            {
              this.leftAt = null;
              return;
            }
            DateTimeOffset? dateTimeOffset = this.leftAt;
            if (dateTimeOffset == null)
            {
              this.leftAt = new DateTimeOffset?(now);
              return;
            }
            DateTimeOffset valueOrDefault3 = dateTimeOffset.GetValueOrDefault();
            if ((now - valueOrDefault3).TotalSeconds > this.CloseDelay)
            {
              this.leftAt = null;
              this.FinishClose(true, true, "a window shrank back");
              return;
            }
          }
          else if (this.isOpen)
          {
            DateTimeOffset? dateTimeOffset = this.leftAt;
            if (dateTimeOffset != null)
            {
              DateTimeOffset valueOrDefault4 = dateTimeOffset.GetValueOrDefault();
              if ((now - valueOrDefault4).TotalSeconds > this.CloseDelay)
              {
                this.Close(false, "pointer left the arc and the notch");
                return;
              }
            }
            else
            {
              this.leftAt = new DateTimeOffset?(now);
            }
          }
          return;
        }
      }
    }

    // Token: 0x060020C1 RID: 8385 RVA: 0x00619CCC File Offset: 0x00619CCC
    private static ArcRect OpenDeckEdgeStrip(ArcScreenMetrics arc)
    {
      return new ArcRect(0.0, 0.0, arc.PanelWidth, TopArcLayout.FrontTop(arc));
    }

    // Token: 0x060020C2 RID: 8386 RVA: 0x00619CF4 File Offset: 0x00619CF4
    [NullableContext(0)]
    private bool IsPointerOnMoreButton([TupleElementNames(new string[] { "X", "Y" })] ValueTuple<double, double> p)
    {
      if (!this.moreFrames.Any<ArcRect>((ArcRect f) => f.Inset(-4.0, -4.0).Contains(p.Item1, p.Item2)))
      {
        ArcRect? arcRect = this.pressedMoreFrame;
        return arcRect != null && arcRect.GetValueOrDefault().Inset(-4.0, -4.0).Contains(p.Item1, p.Item2);
      }
      return true;
    }

    // Token: 0x060020C3 RID: 8387 RVA: 0x00619D7C File Offset: 0x00619D7C
    [NullableContext(0)]
    private static bool IsPointerOnTopArcNotch(ArcScreenMetrics arc, [TupleElementNames(new string[] { "X", "Y" })] ValueTuple<double, double> p)
    {
      return (arc.NotchSurface ?? arc.NotchTriggerFrame).Inset(-6.0, -6.0).Contains(p.Item1, p.Item2);
    }

    // Token: 0x060020C4 RID: 8388 RVA: 0x00619DD8 File Offset: 0x00619DD8
    [NullableContext(0)]
    private bool IsPointerOverInteractiveArea([TupleElementNames(new string[] { "X", "Y" })] ValueTuple<double, double> p, double slack = 0.0)
    {
      if (this.hoveredKey != null && this.cardFrame.Inset(-2.0 - slack, -2.0 - slack).Contains(p.Item1, p.Item2))
      {
        return true;
      }
      ArcRect? arcRect = ((this.metrics != null) ? this.metrics.GetValueOrDefault().NotchSurface : null);
      return (arcRect == null || !arcRect.GetValueOrDefault().Contains(p.Item1, p.Item2) || this.faceFrames.Values.Any<ArcRect>((ArcRect f) => f.Inset(-slack, -slack).Contains(p.Item1, p.Item2)) || this.DeckBand().Inset(-slack, -slack).Contains(p.Item1, p.Item2)) && this.isOpen && (this.railFrame.Inset(-4.0 - slack, -4.0 - slack).Contains(p.Item1, p.Item2) || this.IsPointerOnMoreButton(p));
    }

    // Token: 0x060020C5 RID: 8389 RVA: 0x00619F6C File Offset: 0x00619F6C
    private ArcRect DeckBand()
    {
      if (this.faceFrames.Count == 0 || this.railFrame.IsEmpty)
      {
        return default(ArcRect);
      }
      double num = this.faceFrames.Values.Min<ArcRect>((ArcRect f) => f.Y);
      string frontWindowKey = this.FrontWindowKey;
      ArcRect arcRect;
      double num2;
      if (frontWindowKey == null || !this.faceFrames.TryGetValue(frontWindowKey, out arcRect))
      {
        num2 = this.faceFrames.Values.Max<ArcRect>((ArcRect f) => f.MaxY);
      }
      else
      {
        num2 = arcRect.MaxY;
      }
      double num3 = num2;
      return new ArcRect(this.railFrame.X, num, this.railFrame.W, Math.Max(0.0, num3 - num));
    }

    // Token: 0x060020C6 RID: 8390 RVA: 0x0061A050 File Offset: 0x0061A050
    internal void FramesChanged(ArcRect rail, IReadOnlyDictionary<string, ArcRect> faces, ArcRect? card, IReadOnlyList<ArcRect> more)
    {
      this.railFrame = rail;
      this.faceFrames.Clear();
      foreach (KeyValuePair<string, ArcRect> keyValuePair in faces)
      {
        string text;
        ArcRect arcRect;
        keyValuePair.Deconstruct(out text, out arcRect);
        string text2 = text;
        ArcRect arcRect2 = arcRect;
        this.faceFrames[text2] = arcRect2;
      }
      if (card != null)
      {
        ArcRect valueOrDefault = card.GetValueOrDefault();
        this.cardFrame = valueOrDefault;
      }
      this.moreFrames.Clear();
      this.moreFrames.AddRange(more);
      this.NotifyIfFrontCardSizeChanged();
      this.pointerEventDriver.EvaluateSoon();
    }

    // Token: 0x170005FA RID: 1530
    // (get) Token: 0x060020C7 RID: 8391 RVA: 0x0061A104 File Offset: 0x0061A104
    internal bool IsOpenNow
    {
      get
      {
        return this.isOpen;
      }
    }

    // Token: 0x170005FB RID: 1531
    // (get) Token: 0x060020C8 RID: 8392 RVA: 0x0061A10C File Offset: 0x0061A10C
    public bool IsTabTuckedNow
    {
      get
      {
        return this.isTabTucked;
      }
    }

    // Token: 0x170005FC RID: 1532
    // (get) Token: 0x060020C9 RID: 8393 RVA: 0x0061A114 File Offset: 0x0061A114
    internal bool ClosesInstantly
    {
      get
      {
        return this.closesInstantly;
      }
    }

    // Token: 0x170005FD RID: 1533
    // (get) Token: 0x060020CA RID: 8394 RVA: 0x0061A11C File Offset: 0x0061A11C
    [Nullable(2)]
    internal string HoveredKey
    {
      [NullableContext(2)]
      get
      {
        return this.hoveredKey;
      }
    }

    // Token: 0x170005FE RID: 1534
    // (get) Token: 0x060020CB RID: 8395 RVA: 0x0061A124 File Offset: 0x0061A124
    internal int? FocusIndex
    {
      get
      {
        return this.deckFocusIndex;
      }
    }

    // Token: 0x060020CC RID: 8396 RVA: 0x0061A12C File Offset: 0x0061A12C
    private void Open(string key, string reason)
    {
      if (this.hoveredKey == key && this.isOpen)
      {
        return;
      }
      if (!this.isOpen)
      {
        this.LogArc("open", reason, key);
      }
      FileCardSound.NoteFocusChanged();
      this.PlaySwitchSound(this.hoveredKey, key);
      string text = this.hoveredKey;
      if (text != null && text != key)
      {
        this.NoteCardClosed(text);
      }
      this.isOpen = true;
      this.hoveredKey = key;
      this.view.Layout(LayoutReason.Hover, true);
      if (ClickyJob.JobIdFromRailKey(key) == null)
      {
        string clickySlug = this.DeckWindows().FirstOrDefault<DockWindowKey>((DockWindowKey w) => w.Key == key).ClickySlug;
        if (clickySlug != null)
        {
          IReadOnlyList<DockWindowKey> readOnlyList = this.DeckWindows();
          if (readOnlyList.Count > 0 && readOnlyList[DeckFocus.Clamped(this.deckFocusIndex, readOnlyList.Count)].Key == key)
          {
            string text2 = "clicky_dashboard_action";
            string text3 = clickySlug;
            string text4 = null;
            JsonObject jsonObject = new JsonObject(null);
            jsonObject["action"] = "edge_window_opened";
            ClickyAnalytics.TrackProductEvent(text2, text3, text4, jsonObject);
          }
        }
      }
    }

    // Token: 0x060020CD RID: 8397 RVA: 0x0061A274 File Offset: 0x0061A274
    [NullableContext(0)]
    private void OpenRail([TupleElementNames(new string[] { "X", "Y" })] ValueTuple<double, double> pointer, [Nullable(1)] string reason)
    {
      if (!this.isOpen)
      {
        this.LogArc("open", reason, null);
      }
      this.railOpenedAtPointer = new ValueTuple<double, double>?(pointer);
      if (!this.isOpen)
      {
        IReadOnlyList<string> readOnlyList = this.DeckKeys();
        string text = this.deckFocusKeyToRestore;
        if (text != null)
        {
          int num = readOnlyList.ToList<string>().IndexOf(text);
          if (num >= 0)
          {
            this.deckFocusIndex = new int?(num);
            goto IL_0075;
          }
        }
        if (readOnlyList.Count > 0)
        {
          this.deckFocusIndex = new int?(readOnlyList.Count - 1);
        }
        IL_0075:
        this.deckFocusKeyToRestore = null;
      }
      this.PlaySwitchSound(null, "rail");
      this.isOpen = true;
      this.closesInstantly = false;
      this.view.Layout(LayoutReason.Open, true);
    }

    // Token: 0x060020CE RID: 8398 RVA: 0x0061A324 File Offset: 0x0061A324
    private void Close(bool immediately = false, string reason = "")
    {
      this.leftAt = null;
      this.pressedMoreFrame = null;
      this.FinishClose(false, !immediately, reason);
    }

    // Token: 0x060020CF RID: 8399 RVA: 0x0061A34C File Offset: 0x0061A34C
    private void FinishClose(bool keepingRailOpen = false, bool isAnimated = true, string reason = "a window shrank back")
    {
      if (!keepingRailOpen && this.isOpen)
      {
        this.LogArc("close", reason, null);
      }
      this.isAutoPresentedWindowAwaitingVisit = false;
      if (!keepingRailOpen)
      {
        this.railOpenedAtPointer = null;
      }
      string text = this.hoveredKey;
      if (text != null)
      {
        this.NoteCardClosed(text);
      }
      if (!keepingRailOpen)
      {
        this.closesInstantly = !isAnimated;
        this.isOpen = false;
      }
      this.hoveredKey = null;
      this.view.Layout(keepingRailOpen ? LayoutReason.Hover : LayoutReason.Open, true);
      this.SyncGreetingNotchHold();
      this.SyncDeckPointer();
    }

    // Token: 0x060020D0 RID: 8400 RVA: 0x0061A3D4 File Offset: 0x0061A3D4
    private void NoteCardClosed(string railKey)
    {
      string text = ClickyJob.JobIdFromRailKey(railKey);
      if (text == null)
      {
        return;
      }
      if (this.voiceFollowUpKey == railKey)
      {
        this.voiceFollowUpKey = null;
      }
      if (railKey != this.FrontWindowKey)
      {
        return;
      }
      this.agents.NoteJobCardClosed(text);
    }

    // Token: 0x170005FF RID: 1535
    // (get) Token: 0x060020D1 RID: 8401 RVA: 0x0061A420 File Offset: 0x0061A420
    [Nullable(2)]
    private string FrontWindowKey
    {
      [NullableContext(2)]
      get
      {
        IReadOnlyList<string> readOnlyList = this.DeckKeys();
        if (readOnlyList.Count != 0)
        {
          return readOnlyList[DeckFocus.Clamped(this.deckFocusIndex, readOnlyList.Count)];
        }
        return null;
      }
    }

    // Token: 0x060020D2 RID: 8402 RVA: 0x0061A458 File Offset: 0x0061A458
    public void PresentWindow(string railKey)
    {
      if (!this.isRunning || this.isTabTucked)
      {
        return;
      }
      this.Refresh();
      int num = this.DeckKeys().ToList<string>().IndexOf(railKey);
      if (num < 0)
      {
        return;
      }
      if (this.hoveredKey != null && this.hoveredKey != railKey && this.IsPointerOverInteractiveArea(this.PointerInPanel(), 0.0))
      {
        return;
      }
      this.deckFocusIndex = new int?(num);
      this.view.Layout(LayoutReason.Focus, true);
      this.Open(railKey, "a window came forward by itself");
      this.isAutoPresentedWindowAwaitingVisit = true;
    }

    // Token: 0x17000600 RID: 1536
    // (get) Token: 0x060020D3 RID: 8403 RVA: 0x0061A4F0 File Offset: 0x0061A4F0
    private bool IsScrollGestureGoingOn
    {
      get
      {
        return (DateTimeOffset.Now - this.lastScrollEventAt).TotalSeconds <= 0.22;
      }
    }

    // Token: 0x060020D4 RID: 8404 RVA: 0x0061A524 File Offset: 0x0061A524
    internal bool HandleDeckScroll(int delta, bool horizontal, ulong timestamp = 0UL)
    {
      return this.HandleDeckScroll(delta, horizontal, ArcDock.EventTime(timestamp), PrecisionTouchpad.Snapshot);
    }

    // Token: 0x060020D5 RID: 8405 RVA: 0x0061A53C File Offset: 0x0061A53C
    internal bool HandleDeckScroll(int delta, bool horizontal, double time, TouchpadSnapshot pad)
    {
      if (!this.isOpen || this.IsWindowHeldOpen)
      {
        return false;
      }
      FileCardSound.NoteFocusChanged();
      this.lastScrollEventAt = DateTimeOffset.Now;
      this.ScheduleNextTimedTick();
      int num = this.scrollGestures.Feed(time, (double)(horizontal ? (-(double)delta) : delta), pad);
      if (num == 0)
      {
        return true;
      }
      int count = this.DeckKeys().Count;
      int num2 = DeckFocus.Clamped(this.deckFocusIndex, count);
      int num3 = Math.Min(Math.Max(num2 + num, 0), Math.Max(count - 1, 0));
      if (num3 != num2)
      {
        this.TurnDeck(num3, this.PointerInPanel(), "scroll");
      }
      return true;
    }

    // Token: 0x060020D6 RID: 8406 RVA: 0x0061A5D8 File Offset: 0x0061A5D8
    private static double EventTime(ulong timestamp)
    {
      double now = PointerEvaluationDriver.Now;
      if (timestamp == 0UL)
      {
        return now;
      }
      double num = timestamp / 1000000.0;
      double num2 = now - num;
      if (num2 < -0.05 || num2 > 2.0)
      {
        return now;
      }
      return Math.Min(num, now);
    }

    // Token: 0x17000601 RID: 1537
    // (get) Token: 0x060020D7 RID: 8407 RVA: 0x0061A624 File Offset: 0x0061A624
    public bool ArrowKeysTurnDeck
    {
      get
      {
        return this.isOpen && this.isNotchExpanded && this.deckWindowCountForArrowKeys > 1 && this.Host.NotchIsOpenOnHome();
      }
    }

    // Token: 0x060020D8 RID: 8408 RVA: 0x0061A654 File Offset: 0x0061A654
    public void TurnDeckWithArrowKey(int step)
    {
      if (!this.isOpen || !this.IsGreetingNotchOpen)
      {
        return;
      }
      int count = this.DeckKeys().Count;
      if (count < 2)
      {
        return;
      }
      int num = DeckFocus.Clamped(this.deckFocusIndex, count);
      int num2 = Math.Min(Math.Max(num + step, 0), count - 1);
      if (num2 != num)
      {
        this.TurnDeck(num2, this.PointerInPanel(), "arrow_key");
      }
    }

    // Token: 0x060020D9 RID: 8409 RVA: 0x0061A6B8 File Offset: 0x0061A6B8
    [NullableContext(0)]
    private void TurnDeck(int next, [TupleElementNames(new string[] { "X", "Y" })] ValueTuple<double, double> pointer, [Nullable(1)] string via)
    {
      IReadOnlyList<string> readOnlyList = this.DeckKeys();
      int num = DeckFocus.Clamped(this.deckFocusIndex, readOnlyList.Count);
      if (this.hoveredKey != null)
      {
        this.FinishClose(true, true, "a window shrank back");
      }
      this.deckFocusIndex = new int?(next);
      if (next != num && next >= 0 && next < readOnlyList.Count)
      {
        this.TrackDeckTurned(readOnlyList[next], num, next, readOnlyList.Count, via);
      }
      this.view.Layout(LayoutReason.Focus, true);
      string text = "deck";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
      defaultInterpolatedStringHandler.AppendLiteral("deck-");
      defaultInterpolatedStringHandler.AppendFormatted<int>(next);
      this.PlaySwitchSound(text, defaultInterpolatedStringHandler.ToStringAndClear());
      IReadOnlyList<string> readOnlyList2 = this.DeckKeys();
      if (readOnlyList2.Count > 0)
      {
        this.Open(readOnlyList2[DeckFocus.Clamped(new int?(next), readOnlyList2.Count)], "the deck turned to it");
      }
      this.railOpenedAtPointer = new ValueTuple<double, double>?(pointer);
    }

    // Token: 0x060020DA RID: 8410 RVA: 0x0061A7A0 File Offset: 0x0061A7A0
    internal void ShowDeckWindow(int index, string via)
    {
      if (!this.isOpen)
      {
        return;
      }
      int count = this.DeckKeys().Count;
      int num = Math.Min(Math.Max(index, 0), Math.Max(count - 1, 0));
      if (num == DeckFocus.Clamped(this.deckFocusIndex, count) && this.hoveredKey == null)
      {
        return;
      }
      ValueTuple<double, double> pointer = this.PointerInPanel();
      if (via == "more_pill")
      {
        ArcRect arcRect = this.moreFrames.FirstOrDefault<ArcRect>((ArcRect f) => f.Inset(-4.0, -4.0).Contains(pointer.Item1, pointer.Item2));
        this.pressedMoreFrame = ((arcRect.W > 0.0) ? new ArcRect?(arcRect) : null);
      }
      this.TurnDeck(num, pointer, via);
    }

    // Token: 0x060020DB RID: 8411 RVA: 0x0061A860 File Offset: 0x0061A860
    private void TrackArcOpenedOrClosed(bool isOpening, string reason)
    {
      long tickCount = Environment.TickCount64;
      if (isOpening)
      {
        this.arcOpenedAt = new long?(tickCount);
        this.arcOpenedReason = reason;
        this.arcOpenedWindowCount = this.DeckKeys().Count;
        return;
      }
      long? num = this.arcOpenedAt;
      if (num != null)
      {
        long valueOrDefault = num.GetValueOrDefault();
        this.arcOpenedAt = null;
        JsonObject jsonObject = ArcDockAnalytics.Closed(reason, this.arcOpenedReason, this.arcOpenedWindowCount, (double)Math.Max(0L, tickCount - valueOrDefault) / 1000.0);
        if (jsonObject != null)
        {
          ClickyAnalytics.TrackProductEvent("arc_dock_action", null, null, jsonObject);
        }
        return;
      }
    }

    // Token: 0x060020DC RID: 8412 RVA: 0x0061A8FC File Offset: 0x0061A8FC
    private void TrackDeckTurned(string key, int previous, int next, int windowCount, string via)
    {
      string text = ClickyJob.JobIdFromRailKey(key);
      ClickyJob clickyJob = ((text == null) ? null : this.agents.Jobs.Job(text));
      string text2 = ((text == null) ? "clicky" : ((clickyJob != null && clickyJob.IsWaitingToStart) ? "suggested" : "job"));
      ClickyAnalytics.TrackProductEvent("arc_dock_action", (text == null) ? key : null, (clickyJob != null) ? clickyJob.ThreadId : null, ArcDockAnalytics.DeckTurned(via, previous, next, windowCount, text2));
    }

    // Token: 0x060020DD RID: 8413 RVA: 0x0061A974 File Offset: 0x0061A974
    private void PlaySwitchSound([Nullable(2)] string previous, string next)
    {
      if (!this.Host.PlaysSounds() || previous == next)
      {
        return;
      }
      DateTimeOffset now = DateTimeOffset.Now;
      if ((now - this.lastSwitchSoundAt).TotalSeconds <= 0.12)
      {
        return;
      }
      this.lastSwitchSoundAt = now;
      try
      {
        ScreenDrawingSounds.Play("gel-pop", 0.2f, 0.03, this.soundOwner);
      }
      catch (Exception ex)
      {
        Log.Warn("dock", "gel pop: " + ex.Message);
      }
    }

    // Token: 0x060020DE RID: 8414 RVA: 0x0061AA18 File Offset: 0x0061AA18
    private void LogArc(string arcEvent, string reason, [Nullable(2)] string key = null)
    {
      this.TrackArcOpenedOrClosed(arcEvent == "open", reason);
    }

    // Token: 0x060020DF RID: 8415 RVA: 0x0061AA2C File Offset: 0x0061AA2C
    public bool NotePushToTalkDown()
    {
      this.voiceFollowUpKey = null;
      this.oneBrainFocusedWindow = null;
      this.windowDirectVoiceKey = null;
      if (this.Host.OnboardingHidesDock())
      {
        return false;
      }
      string text = this.cardKeyUnderPointer;
      if (text == null)
      {
        return false;
      }
      this.voiceFollowUpKey = text;
      this.oneBrainFocusedWindow = new ValueTuple<string, DateTimeOffset, AgentFocusPlace>?(new ValueTuple<string, DateTimeOffset, AgentFocusPlace>(text, DateTimeOffset.Now, AgentFocusPlace.Dock));
      string text2 = ClickyJob.JobIdFromRailKey(text);
      if (text2 != null)
      {
        ClickyJob clickyJob = this.agents.Jobs.Job(text2);
        string text3 = ((clickyJob != null) ? clickyJob.ThreadId : null);
        if (text3 != null)
        {
          string text4 = "job_action";
          string text5 = null;
          string text6 = text3;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["action"] = "voice_follow_up";
          jsonObject["via"] = "one_brain";
          ClickyAnalytics.TrackProductEvent(text4, text5, text6, jsonObject);
          return true;
        }
      }
      string text7 = "clicky_dashboard_action";
      string text8 = text;
      string text9 = null;
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["action"] = "footer_voice_follow_up";
      jsonObject2["via"] = "one_brain";
      ClickyAnalytics.TrackProductEvent(text7, text8, text9, jsonObject2);
      return true;
    }

    // Token: 0x060020E0 RID: 8416 RVA: 0x0061AB54 File Offset: 0x0061AB54
    public void NoteFullReplyVoiceFollowUp(string key)
    {
      this.oneBrainFocusedWindow = new ValueTuple<string, DateTimeOffset, AgentFocusPlace>?(new ValueTuple<string, DateTimeOffset, AgentFocusPlace>(key, DateTimeOffset.Now, AgentFocusPlace.NotchFullReply));
      string text = "notch_peek_action";
      string text2 = null;
      string text3 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "full_reply_voice_follow_up";
      ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
    }

    // Token: 0x060020E1 RID: 8417 RVA: 0x0061ABA8 File Offset: 0x0061ABA8
    public void ClearVoiceFollowUp()
    {
      this.voiceFollowUpKey = null;
    }

    // Token: 0x060020E2 RID: 8418 RVA: 0x0061ABB4 File Offset: 0x0061ABB4
    public void NoteVoiceStateChanged()
    {
      this.dq.TryEnqueue(new DispatcherQueueHandler(this.pointerEventDriver.EvaluateSoon));
    }

    // Token: 0x17000602 RID: 1538
    // (get) Token: 0x060020E3 RID: 8419 RVA: 0x0061ABD4 File Offset: 0x0061ABD4
    public bool IsLaunchingFromOpenWindow
    {
      get
      {
        return this.voiceFollowUpKey != null || this.cardKeyUnderPointer != null || HeyClickyWindow.Shared.IsInFront || HeyClickyWindow.Shared.AgentKeyUnderPointer() != null;
      }
    }

    // Token: 0x060020E4 RID: 8420 RVA: 0x0061AC08 File Offset: 0x0061AC08
    public void NoteTypedAgentMessage(string key, bool isNotchFullReply)
    {
      this.oneBrainTypedAgentFocus = new ValueTuple<string, DateTimeOffset, AgentFocusPlace>?(new ValueTuple<string, DateTimeOffset, AgentFocusPlace>(key, DateTimeOffset.Now, isNotchFullReply ? AgentFocusPlace.NotchFullReply : AgentFocusPlace.Notch));
    }

    // Token: 0x060020E5 RID: 8421 RVA: 0x0061AC28 File Offset: 0x0061AC28
    public void NoteTypedAgentMessageInWindow(string key)
    {
      this.oneBrainTypedAgentFocus = new ValueTuple<string, DateTimeOffset, AgentFocusPlace>?(new ValueTuple<string, DateTimeOffset, AgentFocusPlace>(key, DateTimeOffset.Now, AgentFocusPlace.Window));
    }

    // Token: 0x060020E6 RID: 8422 RVA: 0x0061AC44 File Offset: 0x0061AC44
    public void NoteWindowVoiceFollowUp(string key)
    {
      this.oneBrainFocusedWindow = new ValueTuple<string, DateTimeOffset, AgentFocusPlace>?(new ValueTuple<string, DateTimeOffset, AgentFocusPlace>(key, DateTimeOffset.Now, AgentFocusPlace.Window));
      this.windowDirectVoiceKey = new ValueTuple<string, DateTimeOffset>?(new ValueTuple<string, DateTimeOffset>(key, DateTimeOffset.Now));
      string text = "notch_peek_action";
      string text2 = null;
      string text3 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "window_voice_follow_up";
      ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
    }

    // Token: 0x060020E7 RID: 8423 RVA: 0x0061ACB0 File Offset: 0x0061ACB0
    [NullableContext(2)]
    public string TakeWindowDirectVoiceKey()
    {
      ValueTuple<string, DateTimeOffset>? valueTuple = this.windowDirectVoiceKey;
      if (valueTuple == null)
      {
        return null;
      }
      ValueTuple<string, DateTimeOffset> valueOrDefault = valueTuple.GetValueOrDefault();
      this.windowDirectVoiceKey = null;
      if ((DateTimeOffset.Now - valueOrDefault.Item2).TotalSeconds >= 180.0)
      {
        return null;
      }
      return valueOrDefault.Item1;
    }

    // Token: 0x060020E8 RID: 8424 RVA: 0x0061AD10 File Offset: 0x0061AD10
    public void DropOneBrainFocusedWindow()
    {
      this.oneBrainFocusedWindow = null;
    }

    // Token: 0x060020E9 RID: 8425 RVA: 0x0061AD20 File Offset: 0x0061AD20
    [NullableContext(2)]
    public string FocusNoteForOneBrainTurn(bool typed = false)
    {
      ArcDock.<>c__DisplayClass220_0 CS$<>8__locals1;
      CS$<>8__locals1.typed = typed;
      if (CS$<>8__locals1.typed)
      {
        ValueTuple<string, DateTimeOffset, AgentFocusPlace>? valueTuple = this.oneBrainTypedAgentFocus;
        if (valueTuple == null)
        {
          return null;
        }
        ValueTuple<string, DateTimeOffset, AgentFocusPlace> valueOrDefault = valueTuple.GetValueOrDefault();
        this.oneBrainTypedAgentFocus = null;
        if ((DateTimeOffset.Now - valueOrDefault.Item2).TotalSeconds >= 30.0)
        {
          return null;
        }
        CS$<>8__locals1.focus = valueOrDefault;
      }
      else
      {
        ValueTuple<string, DateTimeOffset, AgentFocusPlace>? valueTuple = this.oneBrainFocusedWindow;
        if (valueTuple == null)
        {
          return null;
        }
        ValueTuple<string, DateTimeOffset, AgentFocusPlace> valueOrDefault2 = valueTuple.GetValueOrDefault();
        this.oneBrainFocusedWindow = null;
        if ((DateTimeOffset.Now - valueOrDefault2.Item2).TotalSeconds >= 180.0)
        {
          return null;
        }
        CS$<>8__locals1.focus = valueOrDefault2;
      }
      string text = ClickyJob.JobIdFromRailKey(CS$<>8__locals1.focus.Item1);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (text != null)
      {
        ClickyJob clickyJob = this.agents.Jobs.Job(text);
        if (clickyJob != null)
        {
          string text2 = clickyJob.Title.Replace("\"", "'");
          if (text2.Length > 80)
          {
            text2 = text2.Substring(0, 80);
          }
          if (clickyJob.ThreadId == null)
          {
            return ArcDock.<FocusNoteForOneBrainTurn>g__Origin|220_0("the job \"" + text2 + "\"", ref CS$<>8__locals1) + " It hasn't started yet; their words are most likely about it.";
          }
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 2);
          defaultInterpolatedStringHandler.AppendLiteral("the job \"");
          defaultInterpolatedStringHandler.AppendFormatted(text2);
          defaultInterpolatedStringHandler.AppendLiteral("\" (job_id: ");
          defaultInterpolatedStringHandler.AppendFormatted(clickyJob.Id);
          defaultInterpolatedStringHandler.AppendLiteral(")");
          return ArcDock.<FocusNoteForOneBrainTurn>g__Origin|220_0(defaultInterpolatedStringHandler.ToStringAndClear(), ref CS$<>8__locals1) + " " + ArcDock.<FocusNoteForOneBrainTurn>g__Guidance|220_1("job_id " + clickyJob.Id);
        }
      }
      ClickyInfo clickyInfo = this.agents.FindClicky(CS$<>8__locals1.focus.Item1);
      if (clickyInfo == null)
      {
        return null;
      }
      string text3 = clickyInfo.Name.Replace("\"", "'");
      string threadId = clickyInfo.ThreadId;
      if (threadId != null)
      {
        string text4 = this.agents.OneBrainJobId(threadId);
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 2);
        defaultInterpolatedStringHandler.AppendLiteral("\"");
        defaultInterpolatedStringHandler.AppendFormatted(text3);
        defaultInterpolatedStringHandler.AppendLiteral("\" (job_id: ");
        defaultInterpolatedStringHandler.AppendFormatted(text4);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        return ArcDock.<FocusNoteForOneBrainTurn>g__Origin|220_0(defaultInterpolatedStringHandler.ToStringAndClear(), ref CS$<>8__locals1) + " " + ArcDock.<FocusNoteForOneBrainTurn>g__Guidance|220_1("job_id " + text4);
      }
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 2);
      defaultInterpolatedStringHandler.AppendLiteral("\"");
      defaultInterpolatedStringHandler.AppendFormatted(text3);
      defaultInterpolatedStringHandler.AppendLiteral("\" (clicky_slug: ");
      defaultInterpolatedStringHandler.AppendFormatted(clickyInfo.Slug);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      return ArcDock.<FocusNoteForOneBrainTurn>g__Origin|220_0(defaultInterpolatedStringHandler.ToStringAndClear(), ref CS$<>8__locals1) + " " + ArcDock.<FocusNoteForOneBrainTurn>g__Guidance|220_1("clicky_slug " + clickyInfo.Slug);
    }

    // Token: 0x060020EA RID: 8426 RVA: 0x0061B018 File Offset: 0x0061B018
    public PointD? LaunchLandingPointPx()
    {
      if (!this.ReplacesAgentHud)
      {
        return null;
      }
      Displays.Monitor monitor = this.HostMonitor();
      return new PointD?(new PointD((double)monitor.Bounds.Left + (double)monitor.Bounds.Width / 2.0, (double)monitor.Work.Top + 8.0 * monitor.Scale));
    }

    // Token: 0x17000603 RID: 1539
    // (get) Token: 0x060020EB RID: 8427 RVA: 0x0061B090 File Offset: 0x0061B090
    public IReadOnlyDictionary<string, ArcCardView> DemoCards
    {
      get
      {
        return this.view.Cards;
      }
    }

    // Token: 0x17000604 RID: 1540
    // (get) Token: 0x060020EC RID: 8428 RVA: 0x0061B0A0 File Offset: 0x0061B0A0
    [Nullable(2)]
    public UIElement DemoDeckRoot
    {
      [NullableContext(2)]
      get
      {
        return this.view.DeckRoot;
      }
    }

    // Token: 0x17000605 RID: 1541
    // (get) Token: 0x060020ED RID: 8429 RVA: 0x0061B0B0 File Offset: 0x0061B0B0
    public IntPtr DemoDeckHwnd
    {
      get
      {
        return this.view.DeckHwnd;
      }
    }

    // Token: 0x060020EE RID: 8430 RVA: 0x0061B0C0 File Offset: 0x0061B0C0
    [NullableContext(2)]
    public void DemoOpen(string key = null)
    {
      if (!this.view.IsShowing)
      {
        this.Refresh();
      }
      this.OpenRail(this.PointerInPanel(), "demo");
      this.railOpenedAtPointer = null;
      if (key != null)
      {
        int num = this.DeckKeys().ToList<string>().IndexOf(key);
        if (num >= 0)
        {
          this.deckFocusIndex = new int?(num);
          this.view.Layout(LayoutReason.Focus, true);
        }
        this.Open(key, "demo");
      }
    }

    // Token: 0x060020EF RID: 8431 RVA: 0x0061B13C File Offset: 0x0061B13C
    public void DemoClose()
    {
      this.Close(false, "demo");
    }

    // Token: 0x060020F0 RID: 8432 RVA: 0x0061B14C File Offset: 0x0061B14C
    public void DemoPeek(string key)
    {
      this.Open(key, "demo peek");
    }

    // Token: 0x17000606 RID: 1542
    // (get) Token: 0x060020F1 RID: 8433 RVA: 0x0061B15C File Offset: 0x0061B15C
    public int DemoFrontIndex
    {
      get
      {
        return DeckFocus.Clamped(this.deckFocusIndex, this.DeckKeys().Count);
      }
    }

    // Token: 0x17000607 RID: 1543
    // (get) Token: 0x060020F2 RID: 8434 RVA: 0x0061B174 File Offset: 0x0061B174
    // (set) Token: 0x060020F3 RID: 8435 RVA: 0x0061B17C File Offset: 0x0061B17C
    public bool DemoHoldsOpen { get; set; }

    // Token: 0x17000608 RID: 1544
    // (get) Token: 0x060020F4 RID: 8436 RVA: 0x0061B188 File Offset: 0x0061B188
    // (set) Token: 0x060020F5 RID: 8437 RVA: 0x0061B190 File Offset: 0x0061B190
    [TupleElementNames(new string[] { "X", "Y" })]
    [Nullable(0)]
    public ValueTuple<double, double>? DemoPointer
    {
      [NullableContext(0)]
      [return: TupleElementNames(new string[] { "X", "Y" })]
      get;
      [NullableContext(0)]
      [param: TupleElementNames(new string[] { "X", "Y" })]
      set;
    }

    // Token: 0x060020F6 RID: 8438 RVA: 0x0061B19C File Offset: 0x0061B19C
    public void DemoEvaluate()
    {
      this.Tick();
    }

    // Token: 0x17000609 RID: 1545
    // (get) Token: 0x060020F7 RID: 8439 RVA: 0x0061B1A4 File Offset: 0x0061B1A4
    public bool DemoTakesPointer
    {
      get
      {
        return this.view.TakesPointer;
      }
    }

    // Token: 0x1700060A RID: 1546
    // (get) Token: 0x060020F8 RID: 8440 RVA: 0x0061B1B4 File Offset: 0x0061B1B4
    public IReadOnlyDictionary<string, ArcRect> DemoFaceFrames
    {
      get
      {
        return this.faceFrames;
      }
    }

    // Token: 0x1700060B RID: 1547
    // (get) Token: 0x060020F9 RID: 8441 RVA: 0x0061B1BC File Offset: 0x0061B1BC
    public ArcRect? DemoNotchSurface
    {
      get
      {
        if (this.metrics == null)
        {
          return null;
        }
        return this.metrics.GetValueOrDefault().NotchSurface;
      }
    }

    // Token: 0x060020FA RID: 8442 RVA: 0x0061B1F0 File Offset: 0x0061B1F0
    [return: Nullable(2)]
    internal ArcLiveNote LiveNote(ArcCardContent content)
    {
      if (ArcLiveNotes.IsEnabled)
      {
        string notePath = content.NotePath;
        if (notePath != null)
        {
          string jobId = content.JobId;
          ClickyInkCardSubject clickyInkCardSubject;
          ClickyInkNoteTarget clickyInkNoteTarget;
          if (jobId != null)
          {
            ClickyJob clickyJob = this.agents.Jobs.Job(jobId);
            if (clickyJob != null)
            {
              clickyInkCardSubject = ClickyInkNoteBridge.SubjectForJob(ClickyInkLiveNoteTurns.InkJob(this.agents, clickyJob));
              clickyInkNoteTarget = new ClickyInkNoteTarget.Job(clickyJob.Id);
              goto IL_0085;
            }
          }
          string clickySlug = content.ClickySlug;
          if (clickySlug == null || this.agents.FindClicky(clickySlug) == null)
          {
            return null;
          }
          clickyInkCardSubject = ClickyInkNoteBridge.SubjectForClicky(clickySlug);
          clickyInkNoteTarget = new ClickyInkNoteTarget.Clicky(clickySlug);
          IL_0085:
          ClickyHomeInkNote clickyHomeInkNote = ClickyInkNoteStore.Shared.InkNote(notePath);
          if (clickyHomeInkNote == null)
          {
            return null;
          }
          return new ArcLiveNote(notePath, clickyInkCardSubject, clickyInkNoteTarget, clickyHomeInkNote);
        }
      }
      return null;
    }

    // Token: 0x060020FB RID: 8443 RVA: 0x0061B2A0 File Offset: 0x0061B2A0
    internal static ClickyInkTopOfNote TopOfNote(ArcLiveNote note, bool isWorking, bool animates)
    {
      return new ClickyInkTopOfNote(note.Ink, ClickyInkNoteBridge.Paper(note.Subject), ClickyInkNoteBridge.Actions(note.NotePath, note.Target, note.Ink, note.Subject), isWorking, animates, true);
    }

    // Token: 0x06002108 RID: 8456 RVA: 0x0061B3D4 File Offset: 0x0061B3D4
    [CompilerGenerated]
    internal static string <FocusNoteForOneBrainTurn>g__Origin|220_0(string who, ref ArcDock.<>c__DisplayClass220_0 A_1)
    {
      AgentFocusPlace item = A_1.focus.Item3;
      string text;
      if (A_1.typed)
      {
        if (item != AgentFocusPlace.NotchFullReply)
        {
          if (item != AgentFocusPlace.Window)
          {
            text = "[Notch] The user typed this to " + who + " in the notch.";
          }
          else
          {
            text = "[HeyClicky window] The user typed this in " + who + "'s conversation in the HeyClicky window.";
          }
        }
        else
        {
          text = "[Notch] The user typed this in the box under " + who + "'s reply, open in full in the notch.";
        }
      }
      else if (item != AgentFocusPlace.NotchFullReply)
      {
        if (item != AgentFocusPlace.Window)
        {
          text = "[Edge dock] The user held the voice keys with the pointer on " + who + "'s window on the dock.";
        }
        else
        {
          text = "[HeyClicky window] The user held the voice keys with the pointer on " + who + "'s conversation in the HeyClicky window.";
        }
      }
      else
      {
        text = "[Notch] The user held the voice keys while " + who + "'s reply was open in full in the notch.";
      }
      return text;
    }

    // Token: 0x06002109 RID: 8457 RVA: 0x0061B47C File Offset: 0x0061B47C
    [CompilerGenerated]
    internal static string <FocusNoteForOneBrainTurn>g__Guidance|220_1(string handle)
    {
      return "Their words are most likely for or about it. Use judgment: an instruction, a change, or an answer to what it asked goes to it with sessions_send (" + handle + "); HeyClicky passes on their exact words, so don't reword them, and you say in a few words that you passed it on. A question about its progress or result you answer yourself (clicky_memory); if they clearly mean something else, do that instead.";
    }

    // Token: 0x04000FB4 RID: 4020
    private readonly AgentService agents;

    // Token: 0x04000FB5 RID: 4021
    [Nullable(new byte[] { 1, 2 })]
    private readonly Func<OneBrainJobReports> jobReports;

    // Token: 0x04000FB6 RID: 4022
    private readonly DispatcherQueue dq;

    // Token: 0x04000FB7 RID: 4023
    private readonly ArcCardContentSource source;

    // Token: 0x04000FB9 RID: 4025
    private bool isOpen;

    // Token: 0x04000FBA RID: 4026
    private bool closesInstantly;

    // Token: 0x04000FBB RID: 4027
    private bool isTourShowcase;

    // Token: 0x04000FBC RID: 4028
    private bool isTabTucked;

    // Token: 0x04000FBD RID: 4029
    private bool isNotchExpanded;

    // Token: 0x04000FBE RID: 4030
    [Nullable(2)]
    private string hoveredKey;

    // Token: 0x04000FBF RID: 4031
    private int? deckFocusIndex;

    // Token: 0x04000FC0 RID: 4032
    private ArcScreenMetrics? metrics;

    // Token: 0x04000FC1 RID: 4033
    private readonly Dictionary<string, DateTimeOffset> closedFromDockAt = new Dictionary<string, DateTimeOffset>();

    // Token: 0x04000FC2 RID: 4034
    [Nullable(2)]
    private volatile string voiceFollowUpKey;

    // Token: 0x04000FC3 RID: 4035
    [TupleElementNames(new string[] { "Key", "At", "Place" })]
    [Nullable(new byte[] { 0, 1 })]
    private ValueTuple<string, DateTimeOffset, AgentFocusPlace>? oneBrainFocusedWindow;

    // Token: 0x04000FC4 RID: 4036
    [TupleElementNames(new string[] { "Key", "At", "Place" })]
    [Nullable(new byte[] { 0, 1 })]
    private ValueTuple<string, DateTimeOffset, AgentFocusPlace>? oneBrainTypedAgentFocus;

    // Token: 0x04000FC5 RID: 4037
    [TupleElementNames(new string[] { "Key", "At" })]
    [Nullable(new byte[] { 0, 1 })]
    private ValueTuple<string, DateTimeOffset>? windowDirectVoiceKey;

    // Token: 0x04000FC6 RID: 4038
    private ArcRect railFrame;

    // Token: 0x04000FC7 RID: 4039
    private ArcRect cardFrame;

    // Token: 0x04000FC8 RID: 4040
    private readonly Dictionary<string, ArcRect> faceFrames = new Dictionary<string, ArcRect>();

    // Token: 0x04000FC9 RID: 4041
    private readonly List<ArcRect> moreFrames = new List<ArcRect>();

    // Token: 0x04000FCA RID: 4042
    [Nullable(2)]
    private volatile string cardKeyUnderPointer;

    // Token: 0x04000FCB RID: 4043
    private bool isRunning;

    // Token: 0x04000FCC RID: 4044
    private DateTimeOffset? leftAt;

    // Token: 0x04000FCD RID: 4045
    [TupleElementNames(new string[] { "X", "Y" })]
    [Nullable(0)]
    private ValueTuple<double, double>? railOpenedAtPointer;

    // Token: 0x04000FCE RID: 4046
    [Nullable(2)]
    private string deckFocusKeyToRestore;

    // Token: 0x04000FCF RID: 4047
    private bool isAutoPresentedWindowAwaitingVisit;

    // Token: 0x04000FD0 RID: 4048
    private DateTimeOffset lastStatusTickAt = DateTimeOffset.Now;

    // Token: 0x04000FD1 RID: 4049
    private DateTimeOffset lastSwitchSoundAt = DateTimeOffset.MinValue;

    // Token: 0x04000FD2 RID: 4050
    private DateTimeOffset lastSoundPrepareAt = DateTimeOffset.MinValue;

    // Token: 0x04000FD3 RID: 4051
    private readonly DispatcherQueueTimer refreshDebounce;

    // Token: 0x04000FD4 RID: 4052
    private readonly PointerEvaluationDriver pointerEventDriver;

    // Token: 0x04000FD5 RID: 4053
    private readonly ArcDockView view;

    // Token: 0x04000FD6 RID: 4054
    private readonly DockMembershipWatch membershipWatch = new DockMembershipWatch();

    // Token: 0x04000FD8 RID: 4056
    [TupleElementNames(new string[] { "W", "H" })]
    [Nullable(0)]
    private ValueTuple<double, double>? lastAnnouncedFrontCardSize;

    // Token: 0x04000FD9 RID: 4057
    private const string DockedClickysKey = "clicky.clickyApps.dockedUnpinnedSlugs.v1";

    // Token: 0x04000FDA RID: 4058
    public const int MaximumFaces = 30;

    // Token: 0x04000FDB RID: 4059
    [Nullable(new byte[] { 2, 1 })]
    private List<string> dockedClickys;

    // Token: 0x04000FDD RID: 4061
    private const double NotchReadInterval = 0.1;

    // Token: 0x04000FDE RID: 4062
    private double lastNotchReadAt;

    // Token: 0x04000FDF RID: 4063
    private double notchMovingUntil;

    // Token: 0x04000FE1 RID: 4065
    private bool pointerOnNotch;

    // Token: 0x04000FE2 RID: 4066
    private ArcRect? pressedMoreFrame;

    // Token: 0x04000FE3 RID: 4067
    private readonly DeckScrollGestures scrollGestures = new DeckScrollGestures();

    // Token: 0x04000FE4 RID: 4068
    private DateTimeOffset lastScrollEventAt = DateTimeOffset.MinValue;

    // Token: 0x04000FE5 RID: 4069
    private const double ScrollGestureGap = 0.22;

    // Token: 0x04000FE6 RID: 4070
    private volatile int deckWindowCountForArrowKeys;

    // Token: 0x04000FE7 RID: 4071
    private long? arcOpenedAt;

    // Token: 0x04000FE8 RID: 4072
    private string arcOpenedReason = "";

    // Token: 0x04000FE9 RID: 4073
    private int arcOpenedWindowCount;

    // Token: 0x04000FEA RID: 4074
    private readonly object soundOwner = new object();
  }
}
