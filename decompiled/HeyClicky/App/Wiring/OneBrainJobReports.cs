using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Jobs;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Voice;

namespace HeyClicky.App.Wiring
{
  // Token: 0x0200009C RID: 156
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class OneBrainJobReports
  {
    // Token: 0x170000F7 RID: 247
    // (get) Token: 0x0600063A RID: 1594 RVA: 0x0052F950 File Offset: 0x0052F950
    // (set) Token: 0x0600063B RID: 1595 RVA: 0x0052F958 File Offset: 0x0052F958
    [Nullable(new byte[] { 2, 1, 1 })]
    public Func<string, Task> Speak
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x170000F8 RID: 248
    // (get) Token: 0x0600063C RID: 1596 RVA: 0x0052F964 File Offset: 0x0052F964
    // (set) Token: 0x0600063D RID: 1597 RVA: 0x0052F96C File Offset: 0x0052F96C
    [Nullable(2)]
    public Action PlayDoneChime
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x14000009 RID: 9
    // (add) Token: 0x0600063E RID: 1598 RVA: 0x0052F978 File Offset: 0x0052F978
    // (remove) Token: 0x0600063F RID: 1599 RVA: 0x0052F9B0 File Offset: 0x0052F9B0
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<string> HeadsUpReady;

    // Token: 0x1400000A RID: 10
    // (add) Token: 0x06000640 RID: 1600 RVA: 0x0052F9E8 File Offset: 0x0052F9E8
    // (remove) Token: 0x06000641 RID: 1601 RVA: 0x0052FA20 File Offset: 0x0052FA20
    [Nullable(new byte[] { 2, 1, 1, 1, 1 })]
    [field: Nullable(new byte[] { 2, 1, 1, 1, 1 })]
    public event Action<IReadOnlyList<string>, IReadOnlyList<string>> ReportPresented;

    // Token: 0x1400000B RID: 11
    // (add) Token: 0x06000642 RID: 1602 RVA: 0x0052FA58 File Offset: 0x0052FA58
    // (remove) Token: 0x06000643 RID: 1603 RVA: 0x0052FA90 File Offset: 0x0052FA90
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action PresentingChanged;

    // Token: 0x170000F9 RID: 249
    // (get) Token: 0x06000644 RID: 1604 RVA: 0x0052FAC8 File Offset: 0x0052FAC8
    // (set) Token: 0x06000645 RID: 1605 RVA: 0x0052FAD0 File Offset: 0x0052FAD0
    [Nullable(2)]
    public Func<bool> IsPcStepMomentInProgress
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000646 RID: 1606 RVA: 0x0052FADC File Offset: 0x0052FADC
    public OneBrainJobReports(AgentService agents, CompanionSession companion)
    {
      this.agents = agents;
      this.companion = companion;
    }

    // Token: 0x06000647 RID: 1607 RVA: 0x0052FB6C File Offset: 0x0052FB6C
    public static string TurnKey(string taskId, [Nullable(2)] string turnId)
    {
      return taskId + ":" + turnId;
    }

    // Token: 0x170000FA RID: 250
    // (get) Token: 0x06000648 RID: 1608 RVA: 0x0052FB7C File Offset: 0x0052FB7C
    public static bool IsOn
    {
      get
      {
        return true;
      }
    }

    // Token: 0x06000649 RID: 1609 RVA: 0x0052FB80 File Offset: 0x0052FB80
    [NullableContext(2)]
    public bool IsPresenting(string threadId)
    {
      if (threadId == null)
      {
        return false;
      }
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = this.presentingThreadIds.Contains(threadId);
      }
      return flag2;
    }

    // Token: 0x0600064A RID: 1610 RVA: 0x0052FBD0 File Offset: 0x0052FBD0
    public bool IsFinalizingResult(ClickyJob job)
    {
      if (!this.IsPresentingAny || this.agents.RailPhase(job) != ClickyJobRailPhase.Done)
      {
        return false;
      }
      string text = this.agents.LiveTaskId(job.Id);
      string text2;
      if (text == null)
      {
        text2 = null;
      }
      else
      {
        AgentTaskDetail taskDetail = this.agents.GetTaskDetail(text);
        text2 = ((taskDetail != null) ? taskDetail.Snapshot.ThreadId : null);
      }
      string text3 = text2;
      return this.IsPresenting(text3 ?? job.ThreadId);
    }

    // Token: 0x0600064B RID: 1611 RVA: 0x0052FC40 File Offset: 0x0052FC40
    public bool IsFinalizingResult(string clickySlug)
    {
      return this.IsPresentingAny && this.agents.LiveStatusForClicky(clickySlug).Phase == ClickyAppLiveStatus.Phases.Finished && this.IsPresenting(this.agents.ClickyAppLiveThreadId(clickySlug));
    }

    // Token: 0x170000FB RID: 251
    // (get) Token: 0x0600064C RID: 1612 RVA: 0x0052FC74 File Offset: 0x0052FC74
    private bool IsPresentingAny
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.presentingThreadIds.Count > 0;
        }
        return flag2;
      }
    }

    // Token: 0x0600064D RID: 1613 RVA: 0x0052FCC0 File Offset: 0x0052FCC0
    public bool HasPresented(string threadId)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = this.presentedThreadIds.Contains(threadId);
      }
      return flag2;
    }

    // Token: 0x0600064E RID: 1614 RVA: 0x0052FD08 File Offset: 0x0052FD08
    public IReadOnlyList<string> Lines(string threadId)
    {
      object obj = this.gate;
      IReadOnlyList<string> readOnlyList;
      lock (obj)
      {
        readOnlyList = this.linesByThread.GetValueOrDefault(threadId) ?? Array.Empty<string>();
      }
      return readOnlyList;
    }

    // Token: 0x0600064F RID: 1615 RVA: 0x0052FD5C File Offset: 0x0052FD5C
    public bool Present(string turnKey, string taskId, [Nullable(2)] string threadId, string name)
    {
      if (!OneBrainJobReports.IsOn)
      {
        return false;
      }
      object obj = this.gate;
      lock (obj)
      {
        if (this.presentedTurnOrder.Contains(turnKey))
        {
          return true;
        }
        this.presentedTurnOrder.Add(turnKey);
        if (this.presentedTurnOrder.Count > 40)
        {
          foreach (string text in this.presentedTurnOrder.Take<string>(this.presentedTurnOrder.Count - 40))
          {
            this.linesByTurn.Remove(text);
            this.spokenTurns.Remove(text);
          }
          this.presentedTurnOrder.RemoveRange(0, this.presentedTurnOrder.Count - 40);
        }
        this.waiting.Add(new OneBrainJobReports.Report(turnKey, taskId, threadId, name));
        if (threadId != null)
        {
          this.presentingThreadIds.Add(threadId);
          this.presentedThreadIds.Remove(threadId);
        }
        if (this.flushTask == null)
        {
          this.flushTask = Task.Run(async delegate
          {
            await Task.Delay(OneBrainJobReports.Gather).ConfigureAwait(false);
            await this.FlushAsync().ConfigureAwait(false);
          });
        }
      }
      Action presentingChanged = this.PresentingChanged;
      if (presentingChanged != null)
      {
        presentingChanged();
      }
      string text2 = "JobReports";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 2);
      defaultInterpolatedStringHandler.AppendLiteral("[one brain] report queued for ");
      defaultInterpolatedStringHandler.AppendFormatted(name);
      defaultInterpolatedStringHandler.AppendLiteral(" (");
      defaultInterpolatedStringHandler.AppendFormatted(threadId);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
      return true;
    }

    // Token: 0x06000650 RID: 1616 RVA: 0x0052FF04 File Offset: 0x0052FF04
    private bool IsKnown(string turnKey)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = this.presentedTurnOrder.Contains(turnKey);
      }
      return flag2;
    }

    // Token: 0x06000651 RID: 1617 RVA: 0x0052FF4C File Offset: 0x0052FF4C
    public bool SpeakWhenReady(string turnKey)
    {
      if (!OneBrainJobReports.IsOn || !this.IsKnown(turnKey))
      {
        return false;
      }
      IReadOnlyList<string> readOnlyList = null;
      object obj = this.gate;
      lock (obj)
      {
        OneBrainJobReports.Report report = this.waiting.FirstOrDefault<OneBrainJobReports.Report>((OneBrainJobReports.Report r) => r.TurnKey == turnKey);
        if (report != null)
        {
          report.SpeaksAloud = true;
        }
        else
        {
          OneBrainJobReports.Report report2 = this.inFlight.FirstOrDefault<OneBrainJobReports.Report>((OneBrainJobReports.Report r) => r.TurnKey == turnKey);
          IReadOnlyList<string> readOnlyList2;
          if (report2 != null)
          {
            report2.SpeaksAloud = true;
          }
          else if (this.linesByTurn.TryGetValue(turnKey, out readOnlyList2) && !this.spokenTurns.Contains(turnKey) && !this.UserIsTalking())
          {
            this.spokenTurns.Add(turnKey);
            readOnlyList = readOnlyList2;
          }
        }
      }
      if (readOnlyList != null)
      {
        Func<string, Task> speak = this.Speak;
        if (speak != null)
        {
          speak(string.Join(" ", readOnlyList));
        }
        string text = "job_report_action";
        string text2 = null;
        string text3 = null;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["action"] = "spoken_after_presented";
        jsonObject["line_count"] = readOnlyList.Count;
        ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
      }
      return true;
    }

    // Token: 0x06000652 RID: 1618 RVA: 0x005300A8 File Offset: 0x005300A8
    [NullableContext(2)]
    public bool SpeakOnRequest(string threadId)
    {
      if (!OneBrainJobReports.IsOn || threadId == null)
      {
        return false;
      }
      object obj = this.gate;
      IReadOnlyList<string> valueOrDefault;
      lock (obj)
      {
        valueOrDefault = this.linesByThread.GetValueOrDefault(threadId);
      }
      if (valueOrDefault == null || valueOrDefault.Count <= 0)
      {
        return false;
      }
      bool flag2 = !this.UserIsTalking();
      if (flag2)
      {
        Func<string, Task> speak = this.Speak;
        if (speak != null)
        {
          speak(string.Join(" ", valueOrDefault));
        }
      }
      string text = "job_report_action";
      string text2 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "spoken_on_request";
      jsonObject["spoke"] = flag2;
      jsonObject["line_count"] = valueOrDefault.Count;
      ClickyAnalytics.TrackProductEvent(text, text2, threadId, jsonObject);
      return true;
    }

    // Token: 0x06000653 RID: 1619 RVA: 0x0053018C File Offset: 0x0053018C
    public bool HoldHeadsUpForCard(string turnKey, bool playsChime = false)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        OneBrainJobReports.Report report = this.waiting.FirstOrDefault<OneBrainJobReports.Report>((OneBrainJobReports.Report r) => r.TurnKey == turnKey) ?? this.inFlight.FirstOrDefault<OneBrainJobReports.Report>((OneBrainJobReports.Report r) => r.TurnKey == turnKey);
        if (report == null)
        {
          flag2 = false;
        }
        else
        {
          report.RaisesHeadsUp = true;
          report.PlaysChime = playsChime;
          flag2 = true;
        }
      }
      return flag2;
    }

    // Token: 0x06000654 RID: 1620 RVA: 0x00530228 File Offset: 0x00530228
    private bool UserIsTalking()
    {
      return this.companion.State != VoiceState.Idle || this.companion.IsListening || this.companion.ActiveTurnCount > 0;
    }

    // Token: 0x06000655 RID: 1621 RVA: 0x00530254 File Offset: 0x00530254
    private async Task FlushAsync()
    {
      DateTimeOffset waitedFrom = DateTimeOffset.Now;
      for (;;)
      {
        TimeSpan timeSpan = DateTimeOffset.Now - waitedFrom;
        bool flag = this.UserIsTalking() && timeSpan < OneBrainJobReports.QuietWait;
        Func<bool> isPcStepMomentInProgress = this.IsPcStepMomentInProgress;
        bool flag2 = isPcStepMomentInProgress != null && isPcStepMomentInProgress() && timeSpan < OneBrainJobReports.PcStepWait;
        if (!flag && !flag2)
        {
          break;
        }
        await Task.Delay(300).ConfigureAwait(false);
      }
      double totalSeconds = (DateTimeOffset.Now - waitedFrom).TotalSeconds;
      object obj = this.gate;
      List<OneBrainJobReports.Report> batch;
      lock (obj)
      {
        this.inFlight = this.waiting;
        this.waiting = new List<OneBrainJobReports.Report>();
        this.flushTask = null;
        batch = this.inFlight;
        if (batch.Count > 0)
        {
          this.cardsShownThisTurn.Clear();
          this.cardBlocksShownThisTurn = 0;
          this.cardErrorsThisTurn = 0;
        }
      }
      if (batch.Count != 0)
      {
        long reportStartedAt = Environment.TickCount64;
        int jobCount = batch.Count;
        string reportThreadId = ((jobCount == 1) ? batch[0].ThreadId : null);
        string text = "job_report_action";
        string text2 = null;
        string text3 = reportThreadId;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["action"] = "started";
        jsonObject["job_count"] = jobCount;
        jsonObject["waited_seconds"] = ClickyAnalytics.RoundedSeconds(totalSeconds);
        ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
        string text4 = string.Join(", ", batch.Select<OneBrainJobReports.Report, string>(delegate(OneBrainJobReports.Report r)
        {
          string text10 = "\"";
          string name = r.Name;
          string text11 = "\"";
          string threadId2 = r.ThreadId;
          return text10 + name + text11 + ((threadId2 != null) ? (" (job_id: " + this.agents.OneBrainJobId(threadId2) + ")") : "");
        }));
        string fact = ((batch.Count == 1) ? ("[Job ready] The job " + text4 + " just came back; its report is above. Show its card with show_result, then tell them in one or two short say() lines.") : ("[Job ready] These jobs just came back: " + text4 + "; their reports are above. Show each one's card with show_result, then tell them, a short say() line for each."));
        IReadOnlyList<string> lines = await this.RunReportTurnAsync(fact).ConfigureAwait(false);
        int attempts = 1;
        if (lines == null)
        {
          Log.Info("JobReports", "[one brain] report turn failed: trying once more");
          string text5 = "job_report_action";
          string text6 = null;
          string text7 = reportThreadId;
          JsonObject jsonObject2 = new JsonObject(null);
          jsonObject2["action"] = "retried";
          jsonObject2["job_count"] = jobCount;
          ClickyAnalytics.TrackProductEvent(text5, text6, text7, jsonObject2);
          attempts = 2;
          lines = await this.RunReportTurnAsync(fact).ConfigureAwait(false);
        }
        obj = this.gate;
        JsonObject jsonObject3;
        lock (obj)
        {
          this.inFlight = new List<OneBrainJobReports.Report>();
          int num = batch.Select<OneBrainJobReports.Report, string>((OneBrainJobReports.Report r) => r.ThreadId).OfType<string>().Count<string>(new Func<string, bool>(this.cardsShownThisTurn.Contains));
          int count = batch.Count;
          int num2 = attempts;
          int num3 = this.cardBlocksShownThisTurn;
          int num4 = this.cardErrorsThisTurn;
          IReadOnlyList<string> readOnlyList = lines;
          jsonObject3 = ClickyAnalytics.JobReportProperties(count, num2, num, num3, num4, (readOnlyList != null) ? readOnlyList.Count : 0, batch.Any<OneBrainJobReports.Report>((OneBrainJobReports.Report r) => r.RaisesHeadsUp), batch.Any<OneBrainJobReports.Report>((OneBrainJobReports.Report r) => r.PlaysChime), Environment.TickCount64 - reportStartedAt);
          foreach (OneBrainJobReports.Report report in batch.Where<OneBrainJobReports.Report>((OneBrainJobReports.Report r) => r.ThreadId != null))
          {
            this.presentingThreadIds.Remove(report.ThreadId);
            this.presentedThreadIds.Add(report.ThreadId);
          }
        }
        Action presentingChanged = this.PresentingChanged;
        if (presentingChanged != null)
        {
          presentingChanged();
        }
        if (batch.Any<OneBrainJobReports.Report>((OneBrainJobReports.Report r) => r.PlaysChime))
        {
          Action playDoneChime = this.PlayDoneChime;
          if (playDoneChime != null)
          {
            playDoneChime();
          }
        }
        foreach (OneBrainJobReports.Report report2 in batch.Where<OneBrainJobReports.Report>((OneBrainJobReports.Report r) => r.RaisesHeadsUp))
        {
          Action<string> headsUpReady = this.HeadsUpReady;
          if (headsUpReady != null)
          {
            headsUpReady(report2.TaskId);
          }
        }
        if (lines == null || lines.Count <= 0)
        {
          string text8 = "JobReports";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(73, 1);
          defaultInterpolatedStringHandler.AppendLiteral("[one brain] report turn said nothing for ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(batch.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" job(s): only their status shows");
          Log.Info(text8, defaultInterpolatedStringHandler.ToStringAndClear());
          jsonObject3["action"] = ((lines == null) ? "failed" : "no_lines");
          jsonObject3["spoke"] = false;
          ClickyAnalytics.TrackProductEvent("job_report_action", null, reportThreadId, jsonObject3);
        }
        else
        {
          obj = this.gate;
          bool flag4;
          lock (obj)
          {
            foreach (OneBrainJobReports.Report report3 in batch)
            {
              this.linesByTurn[report3.TurnKey] = lines;
              string threadId = report3.ThreadId;
              if (threadId != null)
              {
                this.linesByThread[threadId] = lines;
              }
            }
            flag4 = batch.Any<OneBrainJobReports.Report>((OneBrainJobReports.Report r) => r.SpeaksAloud) && !this.UserIsTalking();
            if (flag4)
            {
              foreach (OneBrainJobReports.Report report4 in batch)
              {
                this.spokenTurns.Add(report4.TurnKey);
              }
            }
          }
          string text9 = "JobReports";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(56, 4);
          defaultInterpolatedStringHandler.AppendLiteral("[one brain] report turn: ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(lines.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" line(s) for ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(batch.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" job(s), spoken=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(flag4);
          defaultInterpolatedStringHandler.AppendLiteral(": ");
          defaultInterpolatedStringHandler.AppendFormatted(string.Join(" ", lines));
          Log.Info(text9, defaultInterpolatedStringHandler.ToStringAndClear());
          jsonObject3["action"] = "finished";
          jsonObject3["spoke"] = flag4;
          jsonObject3["wanted_to_speak"] = batch.Any<OneBrainJobReports.Report>((OneBrainJobReports.Report r) => r.SpeaksAloud);
          ClickyAnalytics.TrackProductEvent("job_report_action", null, reportThreadId, jsonObject3);
          if (flag4)
          {
            Func<string, Task> speak = this.Speak;
            if (speak != null)
            {
              await speak(string.Join(" ", lines)).ConfigureAwait(false);
            }
          }
          try
          {
            Action<IReadOnlyList<string>, IReadOnlyList<string>> reportPresented = this.ReportPresented;
            if (reportPresented != null)
            {
              reportPresented(batch.Select<OneBrainJobReports.Report, string>((OneBrainJobReports.Report r) => r.ThreadId).OfType<string>().ToList<string>(), lines);
            }
          }
          catch (Exception ex)
          {
            Log.Warn("JobReports", "ReportPresented handler threw: " + ex.Message);
          }
        }
      }
    }

    // Token: 0x06000656 RID: 1622 RVA: 0x00530298 File Offset: 0x00530298
    [return: Nullable(new byte[] { 1, 2, 1 })]
    private async Task<IReadOnlyList<string>> RunReportTurnAsync(string fact)
    {
      IReadOnlyList<string> readOnlyList;
      using (CancellationTokenSource timeout = new CancellationTokenSource(OneBrainJobReports.AnswerTimeout))
      {
        readOnlyList = await this.companion.RunReportTurnAsync(fact, timeout.Token).ConfigureAwait(false);
      }
      return readOnlyList;
    }

    // Token: 0x06000657 RID: 1623 RVA: 0x005302E4 File Offset: 0x005302E4
    public string ShowResult(string argumentsJson)
    {
      JsonObject jsonObject = null;
      try
      {
        jsonObject = JsonNode.Parse(argumentsJson, null, default(JsonDocumentOptions)) as JsonObject;
      }
      catch (JsonException)
      {
      }
      string text;
      if (jsonObject == null)
      {
        text = null;
      }
      else
      {
        JsonNode jsonNode = jsonObject["codexThreadId"];
        if (jsonNode == null)
        {
          text = null;
        }
        else
        {
          string text2 = jsonNode.ToString();
          text = ((text2 != null) ? text2.Trim() : null);
        }
      }
      string text3 = text;
      if (string.IsNullOrEmpty(text3))
      {
        return this.CardError("show_result needs the job's codexThreadId.");
      }
      ClickyAnswer clickyAnswer = ClickyAnswer.Parse(argumentsJson);
      if (clickyAnswer == null)
      {
        return this.CardError("The card needs an outcome line.");
      }
      if (!this.agents.NoteResultCard(text3, clickyAnswer))
      {
        return this.CardError("No job with that codexThreadId came back just now.");
      }
      object obj = this.gate;
      lock (obj)
      {
        this.cardsShownThisTurn.Add(text3);
        this.cardBlocksShownThisTurn += clickyAnswer.Blocks.Count;
      }
      Log.Info("JobReports", "[one brain] show_result: card for " + text3.Substring(0, Math.Min(8, text3.Length)) + " blocks=" + string.Join<ClickyAnswer.Block>(",", clickyAnswer.Blocks));
      return OneBrainJobReports.Json("shown", null);
    }

    // Token: 0x06000658 RID: 1624 RVA: 0x00530430 File Offset: 0x00530430
    private string CardError(string message)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.cardErrorsThisTurn++;
      }
      return OneBrainJobReports.Json("error", message);
    }

    // Token: 0x06000659 RID: 1625 RVA: 0x00530484 File Offset: 0x00530484
    private static string Json(string status, [Nullable(2)] string message)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["status"] = status;
      JsonObject jsonObject2 = jsonObject;
      if (message != null)
      {
        jsonObject2["message"] = message;
      }
      return jsonObject2.ToJsonString(null);
    }

    // Token: 0x04000173 RID: 371
    public static readonly TimeSpan Gather = TimeSpan.FromSeconds(1.2);

    // Token: 0x04000174 RID: 372
    public static readonly TimeSpan QuietWait = TimeSpan.FromSeconds(120L);

    // Token: 0x04000175 RID: 373
    public static readonly TimeSpan PcStepWait = TimeSpan.FromSeconds(300L);

    // Token: 0x04000176 RID: 374
    public static readonly TimeSpan AnswerTimeout = TimeSpan.FromSeconds(25L);

    // Token: 0x04000177 RID: 375
    private readonly AgentService agents;

    // Token: 0x04000178 RID: 376
    private readonly CompanionSession companion;

    // Token: 0x04000179 RID: 377
    private readonly object gate = new object();

    // Token: 0x0400017A RID: 378
    private List<OneBrainJobReports.Report> waiting = new List<OneBrainJobReports.Report>();

    // Token: 0x0400017B RID: 379
    private List<OneBrainJobReports.Report> inFlight = new List<OneBrainJobReports.Report>();

    // Token: 0x0400017C RID: 380
    [Nullable(2)]
    private Task flushTask;

    // Token: 0x0400017D RID: 381
    private readonly Dictionary<string, IReadOnlyList<string>> linesByTurn = new Dictionary<string, IReadOnlyList<string>>();

    // Token: 0x0400017E RID: 382
    private readonly Dictionary<string, IReadOnlyList<string>> linesByThread = new Dictionary<string, IReadOnlyList<string>>();

    // Token: 0x0400017F RID: 383
    private readonly HashSet<string> spokenTurns = new HashSet<string>();

    // Token: 0x04000180 RID: 384
    private readonly List<string> presentedTurnOrder = new List<string>();

    // Token: 0x04000181 RID: 385
    private readonly HashSet<string> presentingThreadIds = new HashSet<string>();

    // Token: 0x04000182 RID: 386
    private readonly HashSet<string> presentedThreadIds = new HashSet<string>();

    // Token: 0x04000183 RID: 387
    private readonly HashSet<string> cardsShownThisTurn = new HashSet<string>();

    // Token: 0x04000184 RID: 388
    private int cardBlocksShownThisTurn;

    // Token: 0x04000185 RID: 389
    private int cardErrorsThisTurn;

    // Token: 0x02000432 RID: 1074
    [Nullable(0)]
    private sealed class Report
    {
      // Token: 0x06002963 RID: 10595 RVA: 0x0064A864 File Offset: 0x0064A864
      public Report(string turnKey, string taskId, [Nullable(2)] string threadId, string name)
      {
      }

      // Token: 0x170006F2 RID: 1778
      // (get) Token: 0x06002964 RID: 10596 RVA: 0x0064A88C File Offset: 0x0064A88C
      public string TurnKey { get; } = turnKey;

      // Token: 0x170006F3 RID: 1779
      // (get) Token: 0x06002965 RID: 10597 RVA: 0x0064A894 File Offset: 0x0064A894
      public string TaskId { get; } = taskId;

      // Token: 0x170006F4 RID: 1780
      // (get) Token: 0x06002966 RID: 10598 RVA: 0x0064A89C File Offset: 0x0064A89C
      [Nullable(2)]
      public string ThreadId
      {
        [NullableContext(2)]
        get;
      } = threadId;

      // Token: 0x170006F5 RID: 1781
      // (get) Token: 0x06002967 RID: 10599 RVA: 0x0064A8A4 File Offset: 0x0064A8A4
      public string Name { get; } = name;

      // Token: 0x04001503 RID: 5379
      public bool SpeaksAloud;

      // Token: 0x04001504 RID: 5380
      public bool RaisesHeadsUp;

      // Token: 0x04001505 RID: 5381
      public bool PlaysChime;
    }
  }
}
