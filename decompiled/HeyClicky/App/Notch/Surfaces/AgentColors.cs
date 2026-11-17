using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.Portraits;
using HeyClicky.Core.Notch;
using Windows.UI;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x0200014C RID: 332
  [NullableContext(1)]
  [Nullable(0)]
  public static class AgentColors
  {
    // Token: 0x060012BD RID: 4797 RVA: 0x005998FC File Offset: 0x005998FC
    public static Color For(string taskId, int fallbackIndex)
    {
      ClickyRoster shared = ClickyRoster.Shared;
      string text;
      if (!string.IsNullOrEmpty(taskId))
      {
        text = taskId;
      }
      else
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
        defaultInterpolatedStringHandler.AppendLiteral("agent-");
        defaultInterpolatedStringHandler.AppendFormatted<int>(fallbackIndex);
        text = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      return shared.HairColorFor(text);
    }

    // Token: 0x060012BE RID: 4798 RVA: 0x00599944 File Offset: 0x00599944
    [return: Nullable(2)]
    public static AgentTaskSnapshot FirstRunningTask(IAgentService agents)
    {
      return agents.Tasks.FirstOrDefault<AgentTaskSnapshot>(delegate(AgentTaskSnapshot x)
      {
        AgentTaskStatus status = x.Status;
        return status <= AgentTaskStatus.Running;
      });
    }

    // Token: 0x060012BF RID: 4799 RVA: 0x00599970 File Offset: 0x00599970
    public static Color? FirstRunning(IAgentService agents)
    {
      AgentTaskSnapshot agentTaskSnapshot = AgentColors.FirstRunningTask(agents);
      if (agentTaskSnapshot != null)
      {
        return new Color?(AgentColors.For(agentTaskSnapshot.TaskId, 0));
      }
      return null;
    }

    // Token: 0x060012C0 RID: 4800 RVA: 0x005999A4 File Offset: 0x005999A4
    public static IReadOnlyList<AgentTaskSnapshot> RunningTasks([Nullable(2)] IAgentService agents)
    {
      if (agents != null)
      {
        return agents.Tasks.Where<AgentTaskSnapshot>(delegate(AgentTaskSnapshot x)
        {
          AgentTaskStatus status = x.Status;
          return status <= AgentTaskStatus.Running;
        }).ToList<AgentTaskSnapshot>();
      }
      return new List<AgentTaskSnapshot>();
    }

    // Token: 0x060012C1 RID: 4801 RVA: 0x005999E0 File Offset: 0x005999E0
    [NullableContext(2)]
    public static AgentTaskSnapshot DisplayedRunningTask(IAgentService agents, [Nullable(1)] MultiAgentRotationCursor rotation, bool isMultiAgentPhase)
    {
      IReadOnlyList<AgentTaskSnapshot> readOnlyList = AgentColors.RunningTasks(agents);
      if (readOnlyList.Count != 0)
      {
        return readOnlyList[rotation.IndexFor(readOnlyList.Count, isMultiAgentPhase)];
      }
      return null;
    }
  }
}
