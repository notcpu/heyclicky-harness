using System;

namespace HeyClicky.Agents
{
  // Token: 0x0200000B RID: 11
  public enum AgentTaskEventKind
  {
    // Token: 0x04000050 RID: 80
    StatusChanged,
    // Token: 0x04000051 RID: 81
    ProgressChanged,
    // Token: 0x04000052 RID: 82
    ThinkingHeading,
    // Token: 0x04000053 RID: 83
    CommentaryDelta,
    // Token: 0x04000054 RID: 84
    Commentary,
    // Token: 0x04000055 RID: 85
    ResponseDelta,
    // Token: 0x04000056 RID: 86
    ToolActivity,
    // Token: 0x04000057 RID: 87
    TitleChanged,
    // Token: 0x04000058 RID: 88
    Finished,
    // Token: 0x04000059 RID: 89
    Failed,
    // Token: 0x0400005A RID: 90
    Stopped,
    // Token: 0x0400005B RID: 91
    NeedsExtraUsageApproval,
    // Token: 0x0400005C RID: 92
    EarlyStopRefunded,
    // Token: 0x0400005D RID: 93
    SteerAccepted,
    // Token: 0x0400005E RID: 94
    IntegrationUsed,
    // Token: 0x0400005F RID: 95
    PlanUpdated,
    // Token: 0x04000060 RID: 96
    ExtraUsageApproved
  }
}
