using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Jobs;
using HeyClicky.Agents.Routines;
using HeyClicky.App.Ink;
using HeyClicky.Core.Ink;

namespace HeyClicky.App
{
  // Token: 0x0200007B RID: 123
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentsInkNoteHost : IClickyInkNoteHost
  {
    // Token: 0x06000488 RID: 1160 RVA: 0x00529CC4 File Offset: 0x00529CC4
    public AgentsInkNoteHost(AgentService agents)
    {
    }

    // Token: 0x17000093 RID: 147
    // (get) Token: 0x06000489 RID: 1161 RVA: 0x00529CD4 File Offset: 0x00529CD4
    // (set) Token: 0x0600048A RID: 1162 RVA: 0x00529CDC File Offset: 0x00529CDC
    [Nullable(new byte[] { 2, 1, 2 })]
    public Func<string, ClickyInkJob> FindJob
    {
      [return: Nullable(new byte[] { 2, 1, 2 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 2 })]
      set;
    }

    // Token: 0x17000094 RID: 148
    // (get) Token: 0x0600048B RID: 1163 RVA: 0x00529CE8 File Offset: 0x00529CE8
    // (set) Token: 0x0600048C RID: 1164 RVA: 0x00529CF0 File Offset: 0x00529CF0
    [Nullable(new byte[] { 2, 1 })]
    public Func<string, bool> IsUnread
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000095 RID: 149
    // (get) Token: 0x0600048D RID: 1165 RVA: 0x00529CFC File Offset: 0x00529CFC
    // (set) Token: 0x0600048E RID: 1166 RVA: 0x00529D04 File Offset: 0x00529D04
    [Nullable(new byte[] { 2, 1, 1 })]
    public Func<string, ClickyInkClickyLive> LiveStatus
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x17000096 RID: 150
    // (get) Token: 0x0600048F RID: 1167 RVA: 0x00529D10 File Offset: 0x00529D10
    // (set) Token: 0x06000490 RID: 1168 RVA: 0x00529D18 File Offset: 0x00529D18
    [Nullable(new byte[] { 2, 1, 1 })]
    public Action<string, string> OpenJobArtifactHandler
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x17000097 RID: 151
    // (get) Token: 0x06000491 RID: 1169 RVA: 0x00529D24 File Offset: 0x00529D24
    // (set) Token: 0x06000492 RID: 1170 RVA: 0x00529D2C File Offset: 0x00529D2C
    [Nullable(new byte[] { 2, 1, 2, 2 })]
    public Func<string, string, string, bool> OpenInViewer
    {
      [return: Nullable(new byte[] { 2, 1, 2, 2 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 2, 2 })]
      set;
    }

    // Token: 0x17000098 RID: 152
    // (get) Token: 0x06000493 RID: 1171 RVA: 0x00529D38 File Offset: 0x00529D38
    // (set) Token: 0x06000494 RID: 1172 RVA: 0x00529D40 File Offset: 0x00529D40
    [Nullable(new byte[] { 2, 1, 1 })]
    public Action<string, int, string> SetInterval
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x17000099 RID: 153
    // (get) Token: 0x06000495 RID: 1173 RVA: 0x00529D4C File Offset: 0x00529D4C
    // (set) Token: 0x06000496 RID: 1174 RVA: 0x00529D54 File Offset: 0x00529D54
    [Nullable(new byte[] { 2, 1, 1, 1 })]
    public Action<string, string, string> SendOnThread
    {
      [return: Nullable(new byte[] { 2, 1, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1, 1 })]
      set;
    }

    // Token: 0x06000497 RID: 1175 RVA: 0x00529D60 File Offset: 0x00529D60
    public static ClickyInkClickyLive InkLive(ClickyAppLiveStatus live)
    {
      ClickyInkClickyPhase clickyInkClickyPhase;
      switch (live.Phase)
      {
      case ClickyAppLiveStatus.Phases.Working:
        clickyInkClickyPhase = ClickyInkClickyPhase.Working;
        break;
      case ClickyAppLiveStatus.Phases.NeedsYou:
        clickyInkClickyPhase = ClickyInkClickyPhase.NeedsYou;
        break;
      case ClickyAppLiveStatus.Phases.Finished:
        clickyInkClickyPhase = ClickyInkClickyPhase.Finished;
        break;
      case ClickyAppLiveStatus.Phases.Failed:
        clickyInkClickyPhase = ClickyInkClickyPhase.Failed;
        break;
      default:
        clickyInkClickyPhase = ClickyInkClickyPhase.Idle;
        break;
      }
      return new ClickyInkClickyLive(clickyInkClickyPhase, live.FinishedAt);
    }

    // Token: 0x06000498 RID: 1176 RVA: 0x00529DAC File Offset: 0x00529DAC
    public bool ClickyExists(string slug)
    {
      return this.<agents>P.FindClicky(slug) != null;
    }

    // Token: 0x06000499 RID: 1177 RVA: 0x00529DC0 File Offset: 0x00529DC0
    public ClickyInkClickyLive ClickyLiveStatus(string slug)
    {
      Func<string, ClickyInkClickyLive> liveStatus = this.LiveStatus;
      if (liveStatus != null)
      {
        return liveStatus(slug);
      }
      return AgentsInkNoteHost.InkLive(this.<agents>P.LiveStatusForClicky(slug));
    }

    // Token: 0x0600049A RID: 1178 RVA: 0x00529DF0 File Offset: 0x00529DF0
    public bool IsClickyUnread(string slug)
    {
      Func<string, bool> isUnread = this.IsUnread;
      return isUnread != null && isUnread(slug);
    }

    // Token: 0x0600049B RID: 1179 RVA: 0x00529E04 File Offset: 0x00529E04
    [return: Nullable(2)]
    public ClickyInkJob Job(string jobId)
    {
      Func<string, ClickyInkJob> findJob = this.FindJob;
      if (findJob == null)
      {
        return null;
      }
      return findJob(jobId);
    }

    // Token: 0x0600049C RID: 1180 RVA: 0x00529E18 File Offset: 0x00529E18
    public IReadOnlyList<ClickyInkRoutineControl> RoutineControls(string slug, DateTimeOffset now)
    {
      return this.<agents>P.Routines.ForClicky(slug).Select<ClickyRoutine, ClickyInkRoutineControl>(delegate(ClickyRoutine routine)
      {
        string id = routine.Id;
        string title = routine.Title;
        int intervalMinutes = routine.IntervalMinutes;
        bool isPaused = routine.IsPaused;
        bool isPausedAfterRepeatedFailures = routine.IsPausedAfterRepeatedFailures;
        DateTimeOffset? lastRunStartedAt = routine.LastRunStartedAt;
        bool flag;
        if (lastRunStartedAt != null)
        {
          DateTimeOffset valueOrDefault = lastRunStartedAt.GetValueOrDefault();
          if (routine.LastRunFinishedAt == null)
          {
            flag = (now - valueOrDefault).TotalSeconds < 7200.0;
            goto IL_0068;
          }
        }
        flag = false;
        IL_0068:
        return new ClickyInkRoutineControl(id, title, intervalMinutes, isPaused, isPausedAfterRepeatedFailures, flag, routine.NextRunAt, routine.RunCount);
      }).ToList<ClickyInkRoutineControl>();
    }

    // Token: 0x0600049D RID: 1181 RVA: 0x00529E5C File Offset: 0x00529E5C
    public void SetRoutinePaused(string routineId, bool isPaused, string source)
    {
      this.<agents>P.Routines.SetPaused(routineId, isPaused, source);
    }

    // Token: 0x0600049E RID: 1182 RVA: 0x00529E74 File Offset: 0x00529E74
    public void SetRoutineInterval(string routineId, int minutes, string source)
    {
      Action<string, int, string> setInterval = this.SetInterval;
      if (setInterval != null)
      {
        setInterval(routineId, minutes, source);
        return;
      }
      this.<agents>P.Routines.SetInterval(routineId, minutes, source);
    }

    // Token: 0x0600049F RID: 1183 RVA: 0x00529EA8 File Offset: 0x00529EA8
    public bool RunRoutineNow(string routineId)
    {
      this.<agents>P.Routines.RunNowAsync(routineId);
      return true;
    }

    // Token: 0x060004A0 RID: 1184 RVA: 0x00529EC0 File Offset: 0x00529EC0
    public void SendToClicky(string slug, string message, string launchContext)
    {
      this.<agents>P.LaunchAsync(new AgentLaunchRequest
      {
        Prompt = message,
        ClickySlug = slug,
        LaunchContext = launchContext,
        LaunchSource = "text",
        UsageContext = "agent_chat"
      }, default(CancellationToken));
    }

    // Token: 0x060004A1 RID: 1185 RVA: 0x00529F14 File Offset: 0x00529F14
    public void SendToThread(string threadId, string message, string launchContext)
    {
      Action<string, string, string> sendOnThread = this.SendOnThread;
      if (sendOnThread != null)
      {
        sendOnThread(threadId, message, launchContext);
        return;
      }
      Log.Warn("ink", "no job thread route yet for " + threadId);
    }

    // Token: 0x060004A2 RID: 1186 RVA: 0x00529F4C File Offset: 0x00529F4C
    public void OpenJobArtifact(string target, string jobId)
    {
      Action<string, string> openJobArtifactHandler = this.OpenJobArtifactHandler;
      if (openJobArtifactHandler != null)
      {
        openJobArtifactHandler(target, jobId);
        return;
      }
      if (this.OpenInViewerIfSupported(target, null, null))
      {
        return;
      }
      ClickyInkNoteBridge.OpenInItsApp(target);
    }

    // Token: 0x060004A3 RID: 1187 RVA: 0x00529F80 File Offset: 0x00529F80
    [return: Nullable(2)]
    public string BoundThreadId(string slug)
    {
      ClickyInfo clickyInfo = this.<agents>P.FindClicky(slug);
      if (clickyInfo == null)
      {
        return null;
      }
      return clickyInfo.ThreadId;
    }

    // Token: 0x060004A4 RID: 1188 RVA: 0x00529F9C File Offset: 0x00529F9C
    [NullableContext(2)]
    public bool OpenInViewerIfSupported([Nullable(1)] string target, string slug, string threadId)
    {
      Func<string, string, string, bool> openInViewer = this.OpenInViewer;
      return openInViewer != null && openInViewer(target, slug, threadId);
    }

    // Token: 0x040000DF RID: 223
    [CompilerGenerated]
    private AgentService <agents>P = agents;

    // Token: 0x040000E7 RID: 231
    private const double RoutineRunStaleAfterSeconds = 7200.0;
  }
}
