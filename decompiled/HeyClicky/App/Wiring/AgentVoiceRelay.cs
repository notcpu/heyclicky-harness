using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Notch.Window;
using HeyClicky.Core;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Tools;
using HeyClicky.Core.Voice;
using HeyClicky.Core.Worker;
using HeyClicky.Platform.Win32;

namespace HeyClicky.App.Wiring
{
  // Token: 0x02000092 RID: 146
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentVoiceRelay
  {
    // Token: 0x060005C1 RID: 1473 RVA: 0x0052DAD8 File Offset: 0x0052DAD8
    public AgentVoiceRelay(AgentService agents, CoreServices core, CompanionSession companion, BrainContext context)
    {
      this.agents = agents;
      this.core = core;
      this.companion = companion;
      this.context = context;
      agents.TaskEvent += delegate([Nullable(2)] object _, AgentTaskEvent e)
      {
        try
        {
          this.OnTaskEvent(e);
        }
        catch (Exception ex)
        {
          string text = "AgentVoice";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
          defaultInterpolatedStringHandler.AppendFormatted<AgentTaskEventKind>(e.Kind);
          defaultInterpolatedStringHandler.AppendLiteral(": ");
          defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
          Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
        }
      };
    }

    // Token: 0x170000DA RID: 218
    // (get) Token: 0x060005C2 RID: 1474 RVA: 0x0052DB28 File Offset: 0x0052DB28
    private bool VoiceAnnouncementsEnabled
    {
      get
      {
        return this.core.Settings.Read<bool>((AppSettings s) => s.AgentAnnouncementsVoiceEnabled);
      }
    }

    // Token: 0x170000DB RID: 219
    // (get) Token: 0x060005C3 RID: 1475 RVA: 0x0052DB5C File Offset: 0x0052DB5C
    // (set) Token: 0x060005C4 RID: 1476 RVA: 0x0052DB64 File Offset: 0x0052DB64
    [Nullable(new byte[] { 2, 1 })]
    public Action<string> ShowAtCursor
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170000DC RID: 220
    // (get) Token: 0x060005C5 RID: 1477 RVA: 0x0052DB70 File Offset: 0x0052DB70
    // (set) Token: 0x060005C6 RID: 1478 RVA: 0x0052DB78 File Offset: 0x0052DB78
    [Nullable(2)]
    public OneBrainJobReports JobReports
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170000DD RID: 221
    // (get) Token: 0x060005C7 RID: 1479 RVA: 0x0052DB84 File Offset: 0x0052DB84
    // (set) Token: 0x060005C8 RID: 1480 RVA: 0x0052DB8C File Offset: 0x0052DB8C
    [Nullable(new byte[] { 2, 1 })]
    public Func<AgentTaskDetail, bool> FinishShowsOnNotchCard
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170000DE RID: 222
    // (get) Token: 0x060005C9 RID: 1481 RVA: 0x0052DB98 File Offset: 0x0052DB98
    private static bool JobsWorld
    {
      get
      {
        return ClickyJobsPreferences.IsEnabled;
      }
    }

    // Token: 0x060005CA RID: 1482 RVA: 0x0052DBA0 File Offset: 0x0052DBA0
    private string BrainName(AgentTaskDetail detail)
    {
      string jobId = detail.JobId;
      if (jobId != null)
      {
        ClickyJob clickyJob = this.agents.Jobs.Job(jobId);
        if (clickyJob != null)
        {
          string title = clickyJob.Title;
          if (title != null && title.Length > 0)
          {
            return title;
          }
        }
      }
      string clickyName;
      if ((clickyName = detail.ClickyName) == null)
      {
        return detail.Snapshot.Title;
      }
      return clickyName;
    }

    // Token: 0x060005CB RID: 1483 RVA: 0x0052DBF8 File Offset: 0x0052DBF8
    private void OnTaskEvent(AgentTaskEvent e)
    {
      AgentTaskDetail taskDetail = this.agents.GetTaskDetail(e.TaskId);
      if (taskDetail == null)
      {
        return;
      }
      string text = this.BrainName(taskDetail);
      string clickySlug = taskDetail.ClickySlug;
      string text2 = ClickyNotes.ProgressNoteKey(clickySlug, taskDetail.Snapshot.ThreadId, e.TaskId);
      switch (e.Kind)
      {
      case AgentTaskEventKind.Commentary:
        if (!string.IsNullOrWhiteSpace(e.Text))
        {
          if (taskDetail.IsDemo)
          {
            return;
          }
          AgentTaskStatus status = taskDetail.Snapshot.Status;
          if (status > AgentTaskStatus.Running)
          {
            return;
          }
          string text3 = ClickyNotes.ProgressNote(text, clickySlug, taskDetail.Snapshot.ThreadId, e.Text, AgentVoiceRelay.JobsWorld, taskDetail.JobId != null);
          this.core.BrainSession.NoteForNextTurn(text3, text2);
          Log.Info("AgentVoice", "progress for the next turn: " + ClickyNotes.Condensed(e.Text).Substring(0, Math.Min(80, ClickyNotes.Condensed(e.Text).Length)));
          return;
        }
        break;
      case AgentTaskEventKind.ResponseDelta:
      case AgentTaskEventKind.ToolActivity:
      case AgentTaskEventKind.TitleChanged:
        break;
      case AgentTaskEventKind.Finished:
      {
        string text4;
        if ((text4 = taskDetail.Summary) == null && (text4 = taskDetail.Snapshot.FinalResponseMarkdown) == null)
        {
          text4 = e.Text ?? "";
        }
        string text5 = text4;
        this.Report(taskDetail, text2, "finished", text5);
        string text6 = OneBrainJobReports.TurnKey(e.TaskId, taskDetail.TurnId);
        bool flag = this.agents.IsTaskWaitingOnUserAnswer(e.TaskId);
        if (!taskDetail.IsDemo && !flag && this.agents.FullReplyText(e.TaskId) != null)
        {
          OneBrainJobReports jobReports = this.JobReports;
          if (jobReports != null)
          {
            jobReports.Present(text6, e.TaskId, taskDetail.Snapshot.ThreadId, text);
          }
        }
        if (taskDetail.IsDemo)
        {
          Log.Info("AgentVoice", "finish not announced: an onboarding demo agent");
          return;
        }
        if (flag)
        {
          return;
        }
        Func<AgentTaskDetail, bool> finishShowsOnNotchCard = this.FinishShowsOnNotchCard;
        if (finishShowsOnNotchCard != null && finishShowsOnNotchCard(taskDetail))
        {
          Log.Info("AgentVoice", "finish not spoken: its result shows on the notch's card");
          return;
        }
        if (!this.announcedFinish.TryAdd(e.TaskId + ":" + (taskDetail.TurnId ?? taskDetail.Snapshot.StartedAt.ToString("O")), true))
        {
          return;
        }
        if (QuietContext.IsQuiet() || (!QuietContext.IgnoredForTests && QuietContext.QueryNotificationState() == QuietContext.NotificationState.PresentationMode))
        {
          Log.Info("AgentVoice", "finish not announced: quiet (a call, presenting or Do Not Disturb)");
          return;
        }
        bool flag2 = this.core.Settings.Read<bool>((AppSettings s) => s.AgentCursorTextAnnouncementsEnabled) && !HeyClickyWindow.KeepsAgentLinesOffCursor;
        if (string.IsNullOrWhiteSpace(text5) || (!this.VoiceAnnouncementsEnabled && !flag2))
        {
          return;
        }
        if (this.VoiceAnnouncementsEnabled)
        {
          OneBrainJobReports jobReports2 = this.JobReports;
          if (jobReports2 != null && jobReports2.SpeakWhenReady(text6))
          {
            return;
          }
        }
        string text7 = ClickyNotes.FinishedLine(text, text5, false, false);
        if (this.VoiceAnnouncementsEnabled)
        {
          this.companion.AnnounceAsync(text7, flag2, default(CancellationToken));
          return;
        }
        Action<string> showAtCursor = this.ShowAtCursor;
        if (showAtCursor == null)
        {
          return;
        }
        showAtCursor(text7);
        return;
      }
      case AgentTaskEventKind.Failed:
        this.Report(taskDetail, text2, "failed", taskDetail.FailureMessage ?? e.Text);
        return;
      case AgentTaskEventKind.Stopped:
        this.Report(taskDetail, text2, "was stopped before finishing", e.Text);
        return;
      case AgentTaskEventKind.NeedsExtraUsageApproval:
        if (taskDetail.IsDemo)
        {
          return;
        }
        this.core.BrainSession.NoteForNextTurn(AgentVoiceRelay.JobsWorld ? ClickyNotes.JobEventNote(text, taskDetail.Snapshot.ThreadId, "is paused: it needs more agent messages from their plan to finish, and a yes from the user (sessions_answer_request, request extra_usage) keeps it going", null, null) : ClickyNotes.EventNote(taskDetail.ClickyName, clickySlug, taskDetail.Snapshot.Title, "is paused: it needs more agent messages from their plan to finish, and a yes from the user (sessions_answer_request, request extra_usage) keeps it going", null, false, null, null, null), null);
        if (this.VoiceAnnouncementsEnabled)
        {
          this.companion.AnnounceAsync(ClickyNotes.PausedForExtraUsageLine(text), true, default(CancellationToken));
        }
        break;
      default:
        return;
      }
    }

    // Token: 0x170000DF RID: 223
    // (get) Token: 0x060005CC RID: 1484 RVA: 0x0052DFE0 File Offset: 0x0052DFE0
    // (set) Token: 0x060005CD RID: 1485 RVA: 0x0052DFE8 File Offset: 0x0052DFE8
    [Nullable(new byte[] { 2, 1, 2 })]
    public static Func<AgentTaskDetail, string> ResultFacts
    {
      [return: Nullable(new byte[] { 2, 1, 2 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 2 })]
      set;
    }

    // Token: 0x060005CE RID: 1486 RVA: 0x0052DFF0 File Offset: 0x0052DFF0
    private void Report(AgentTaskDetail detail, string progressKey, string @event, [Nullable(2)] string summary)
    {
      if (detail.IsDemo)
      {
        return;
      }
      this.core.BrainSession.DropNoteForNextTurn(progressKey);
      this.core.BrainSession.NoteForNextTurn(AgentVoiceRelay.JobsWorld ? ClickyNotes.JobEventNote(this.BrainName(detail), detail.Snapshot.ThreadId, @event, summary, (@event == "finished") ? this.agents.ResultReport(detail.Snapshot.TaskId) : null) : ClickyNotes.EventNote(detail.ClickyName, detail.ClickySlug, detail.Snapshot.Title, @event, summary, false, null, null, null), null);
      string clickySlug = detail.ClickySlug;
      if (clickySlug != null)
      {
        this.context.LastReport = new ClickyNotes.Report(clickySlug, detail.ClickyName ?? detail.Snapshot.Title, @event, DateTimeOffset.Now);
      }
      Log.Info("AgentVoice", (detail.ClickyName ?? detail.Snapshot.Title) + " " + @event + ": noted for the next turn");
    }

    // Token: 0x060005CF RID: 1487 RVA: 0x0052E0FC File Offset: 0x0052E0FC
    public async Task SpeakStartCueAsync(string prompt)
    {
      if (this.VoiceAnnouncementsEnabled && !string.IsNullOrWhiteSpace(prompt))
      {
        try
        {
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["user_prompt"] = prompt;
          jsonObject["is_fresh_thread"] = false;
          jsonObject["launch_source"] = "voice";
          string text = jsonObject.ToJsonString(null);
          WorkerClient worker = this.core.Worker;
          HttpMethod post = HttpMethod.Post;
          string text2 = "/codex-thread-launch";
          HttpContent httpContent = new StringContent(text, Encoding.UTF8, "application/json");
          bool flag = true;
          TimeSpan? timeSpan = new TimeSpan?(TimeSpan.FromSeconds(8L));
          byte[] array = await worker.SendRawAsync(post, text2, httpContent, flag, default(CancellationToken), timeSpan, "companion").ConfigureAwait(false);
          if (array != null)
          {
            JsonObject jsonObject2 = JsonNode.Parse(array, null, default(JsonDocumentOptions)) as JsonObject;
            string text3 = (string)((jsonObject2 != null) ? jsonObject2["spoken_start_cue"] : null);
            if (!string.IsNullOrWhiteSpace(text3))
            {
              Log.Info("AgentVoice", "start cue: " + text3);
              await this.companion.AnnounceAsync(ClickyNotes.StartedLine(text3), true, default(CancellationToken)).ConfigureAwait(false);
            }
          }
        }
        catch (Exception ex)
        {
          Log.Warn("AgentVoice", "start cue: " + ex.Message);
        }
      }
    }

    // Token: 0x0400012B RID: 299
    private readonly AgentService agents;

    // Token: 0x0400012C RID: 300
    private readonly CoreServices core;

    // Token: 0x0400012D RID: 301
    private readonly CompanionSession companion;

    // Token: 0x0400012E RID: 302
    private readonly BrainContext context;

    // Token: 0x0400012F RID: 303
    private readonly ConcurrentDictionary<string, bool> announcedFinish = new ConcurrentDictionary<string, bool>();
  }
}
