using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Notch;
using Microsoft.UI.Xaml;

namespace HeyClicky.App
{
  // Token: 0x0200007D RID: 125
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class WorkChatLiveWork : IChatLiveWork
  {
    // Token: 0x060004AA RID: 1194 RVA: 0x0052A088 File Offset: 0x0052A088
    public WorkChatLiveWork(AgentService agents, [Nullable(new byte[] { 1, 2 })] Func<OneBrainJobReports> reports)
    {
    }

    // Token: 0x060004AB RID: 1195 RVA: 0x0052A0AC File Offset: 0x0052A0AC
    [return: Nullable(2)]
    public FrameworkElement WorkCard(ChatHandOff entry)
    {
      return null;
    }

    // Token: 0x14000002 RID: 2
    // (add) Token: 0x060004AC RID: 1196 RVA: 0x0052A0B0 File Offset: 0x0052A0B0
    // (remove) Token: 0x060004AD RID: 1197 RVA: 0x0052A12C File Offset: 0x0052A12C
    [Nullable(2)]
    public event Action Changed
    {
      [NullableContext(2)]
      add
      {
        if (value == null)
        {
          return;
        }
        EventHandler eventHandler = delegate([Nullable(2)] object _, EventArgs _)
        {
          value();
        };
        this.handlers[value] = eventHandler;
        this.<agents>P.TasksChanged += eventHandler;
        this.<agents>P.Jobs.Changed += eventHandler;
        OneBrainJobReports oneBrainJobReports = this.<reports>P();
        if (oneBrainJobReports != null)
        {
          oneBrainJobReports.PresentingChanged += value;
        }
      }
      [NullableContext(2)]
      remove
      {
        EventHandler eventHandler;
        if (value == null || !this.handlers.Remove(value, out eventHandler))
        {
          return;
        }
        this.<agents>P.TasksChanged -= eventHandler;
        this.<agents>P.Jobs.Changed -= eventHandler;
        OneBrainJobReports oneBrainJobReports = this.<reports>P();
        if (oneBrainJobReports != null)
        {
          oneBrainJobReports.PresentingChanged -= value;
        }
      }
    }

    // Token: 0x060004AE RID: 1198 RVA: 0x0052A180 File Offset: 0x0052A180
    public bool IsWorking(ChatHandOff entry)
    {
      string jobId = entry.JobId;
      if (jobId != null)
      {
        ClickyJob clickyJob = this.<agents>P.Jobs.Job(jobId);
        if (clickyJob != null)
        {
          ClickyJobRailPhase clickyJobRailPhase = this.<agents>P.RailPhase(clickyJob);
          if (clickyJobRailPhase <= ClickyJobRailPhase.Working)
          {
            return true;
          }
          if (clickyJobRailPhase == ClickyJobRailPhase.Done)
          {
            OneBrainJobReports oneBrainJobReports = this.<reports>P();
            return oneBrainJobReports != null && oneBrainJobReports.IsPresenting(clickyJob.ThreadId);
          }
          return false;
        }
      }
      string clickySlug = entry.ClickySlug;
      return clickySlug != null && this.<agents>P.LiveStatusForClicky(clickySlug).Phase == ClickyAppLiveStatus.Phases.Working;
    }

    // Token: 0x060004AF RID: 1199 RVA: 0x0052A210 File Offset: 0x0052A210
    [return: Nullable(2)]
    public string DisplayName(ChatHandOff entry)
    {
      string jobId = entry.JobId;
      if (jobId == null)
      {
        string clickySlug = entry.ClickySlug;
        if (clickySlug == null)
        {
          return null;
        }
        ClickyInfo clickyInfo = this.<agents>P.FindClicky(clickySlug);
        if (clickyInfo == null)
        {
          return null;
        }
        return clickyInfo.Name;
      }
      else
      {
        ClickyJob clickyJob = this.<agents>P.Jobs.Job(jobId);
        if (clickyJob == null)
        {
          return null;
        }
        return clickyJob.DisplayName;
      }
    }

    // Token: 0x060004B0 RID: 1200 RVA: 0x0052A268 File Offset: 0x0052A268
    [return: Nullable(2)]
    public FrameworkElement Chip(ChatHandOff entry, double size)
    {
      return null;
    }

    // Token: 0x060004B1 RID: 1201 RVA: 0x0052A26C File Offset: 0x0052A26C
    [return: Nullable(2)]
    public FrameworkElement FileCards(IReadOnlyList<string> entries, Action<string> open)
    {
      return null;
    }

    // Token: 0x040000E9 RID: 233
    [CompilerGenerated]
    private AgentService <agents>P = agents;

    // Token: 0x040000EA RID: 234
    [Nullable(new byte[] { 1, 2 })]
    [CompilerGenerated]
    private Func<OneBrainJobReports> <reports>P = reports;

    // Token: 0x040000EB RID: 235
    private readonly Dictionary<Action, EventHandler> handlers = new Dictionary<Action, EventHandler>();
  }
}
