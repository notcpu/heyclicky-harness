using System;
using System.Runtime.CompilerServices;
using HeyClicky.Agents;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Drive;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002F9 RID: 761
  [NullableContext(2)]
  [Nullable(0)]
  internal sealed class DriveJob : IAgentDriveJob
  {
    // Token: 0x0600212D RID: 8493 RVA: 0x0061B998 File Offset: 0x0061B998
    [NullableContext(1)]
    public DriveJob(AgentService agents, string taskId)
    {
    }

    // Token: 0x17000613 RID: 1555
    // (get) Token: 0x0600212E RID: 8494 RVA: 0x0061B9B0 File Offset: 0x0061B9B0
    private AgentTaskDetail Detail
    {
      get
      {
        return this.<agents>P.GetTaskDetail(this.<taskId>P);
      }
    }

    // Token: 0x17000614 RID: 1556
    // (get) Token: 0x0600212F RID: 8495 RVA: 0x0061B9C4 File Offset: 0x0061B9C4
    [Nullable(1)]
    public string TaskId
    {
      [NullableContext(1)]
      get
      {
        return this.<taskId>P;
      }
    } = taskId;

    // Token: 0x17000615 RID: 1557
    // (get) Token: 0x06002130 RID: 8496 RVA: 0x0061B9CC File Offset: 0x0061B9CC
    public string ThreadId
    {
      get
      {
        AgentTaskDetail detail = this.Detail;
        if (detail == null)
        {
          return null;
        }
        return detail.Snapshot.ThreadId;
      }
    }

    // Token: 0x17000616 RID: 1558
    // (get) Token: 0x06002131 RID: 8497 RVA: 0x0061B9E4 File Offset: 0x0061B9E4
    [Nullable(1)]
    public string Name
    {
      [NullableContext(1)]
      get
      {
        AgentTaskDetail detail = this.Detail;
        string text = ((detail != null) ? detail.ClickyName : null);
        if (text != null && text.Length > 0)
        {
          return text;
        }
        string text2 = ((detail != null) ? detail.JobId : null);
        if (text2 != null)
        {
          ClickyJob clickyJob = this.<agents>P.Jobs.Job(text2);
          string text3 = ((clickyJob != null) ? clickyJob.Title : null);
          if (text3 != null && text3.Length > 0)
          {
            return text3;
          }
        }
        return "Your job";
      }
    }

    // Token: 0x17000617 RID: 1559
    // (get) Token: 0x06002132 RID: 8498 RVA: 0x0061BA54 File Offset: 0x0061BA54
    [Nullable(1)]
    public string Title
    {
      [NullableContext(1)]
      get
      {
        AgentTaskDetail detail = this.Detail;
        return ((detail != null) ? detail.Snapshot.Title : null) ?? "";
      }
    }

    // Token: 0x17000618 RID: 1560
    // (get) Token: 0x06002133 RID: 8499 RVA: 0x0061BA78 File Offset: 0x0061BA78
    public string AgentTurnLeaseId
    {
      get
      {
        return this.<agents>P.TurnLeaseId(this.<taskId>P);
      }
    }

    // Token: 0x17000619 RID: 1561
    // (get) Token: 0x06002134 RID: 8500 RVA: 0x0061BA8C File Offset: 0x0061BA8C
    public bool IsRunning
    {
      get
      {
        AgentTaskDetail detail = this.Detail;
        AgentTaskStatus? agentTaskStatus = ((detail != null) ? new AgentTaskStatus?(detail.Snapshot.Status) : null);
        if (agentTaskStatus != null)
        {
          AgentTaskStatus valueOrDefault = agentTaskStatus.GetValueOrDefault();
          if (valueOrDefault <= AgentTaskStatus.Running)
          {
            return true;
          }
        }
        return false;
      }
    }

    // Token: 0x1700061A RID: 1562
    // (get) Token: 0x06002135 RID: 8501 RVA: 0x0061BADC File Offset: 0x0061BADC
    public bool NeedsChromeConnection
    {
      get
      {
        string threadId = this.ThreadId;
        return threadId != null && this.<agents>P.NeedsChromeConnection(threadId);
      }
    }

    // Token: 0x1700061B RID: 1563
    // (get) Token: 0x06002136 RID: 8502 RVA: 0x0061BB04 File Offset: 0x0061BB04
    public bool FoundChromeNotConnectedThisTurn
    {
      get
      {
        string threadId = this.ThreadId;
        return threadId != null && this.<agents>P.FoundChromeNotConnectedThisTurn(threadId);
      }
    }

    // Token: 0x1700061C RID: 1564
    // (get) Token: 0x06002137 RID: 8503 RVA: 0x0061BB2C File Offset: 0x0061BB2C
    // (set) Token: 0x06002138 RID: 8504 RVA: 0x0061BB40 File Offset: 0x0061BB40
    public string PendingComputerUseRequest
    {
      get
      {
        return this.<agents>P.PendingComputerUseRequest(this.<taskId>P);
      }
      set
      {
        this.<agents>P.SetPendingComputerUseRequest(this.<taskId>P, value);
      }
    }

    // Token: 0x1700061D RID: 1565
    // (get) Token: 0x06002139 RID: 8505 RVA: 0x0061BB54 File Offset: 0x0061BB54
    // (set) Token: 0x0600213A RID: 8506 RVA: 0x0061BB68 File Offset: 0x0061BB68
    public string PendingComputerUseSummary
    {
      get
      {
        return this.<agents>P.PendingComputerUseSummary(this.<taskId>P);
      }
      set
      {
        this.<agents>P.SetPendingComputerUseSummary(this.<taskId>P, value);
      }
    }

    // Token: 0x1700061E RID: 1566
    // (get) Token: 0x0600213B RID: 8507 RVA: 0x0061BB7C File Offset: 0x0061BB7C
    // (set) Token: 0x0600213C RID: 8508 RVA: 0x0061BB90 File Offset: 0x0061BB90
    public bool DidAnnounceComputerUseRequest
    {
      get
      {
        return this.<agents>P.DidAnnounceComputerUseRequest(this.<taskId>P);
      }
      set
      {
        this.<agents>P.SetDidAnnounceComputerUseRequest(this.<taskId>P, value);
      }
    }

    // Token: 0x04001000 RID: 4096
    [Nullable(1)]
    [CompilerGenerated]
    private AgentService <agents>P = agents;

    // Token: 0x04001001 RID: 4097
    [Nullable(1)]
    [CompilerGenerated]
    private string <taskId>P;
  }
}
