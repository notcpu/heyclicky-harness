using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.Agents.Backend;
using HeyClicky.Agents.Jobs;

namespace HeyClicky.Agents
{
  // Token: 0x0200000A RID: 10
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class AgentTaskDetail : IEquatable<AgentTaskDetail>
  {
    // Token: 0x06000060 RID: 96 RVA: 0x000D70B4 File Offset: 0x000D70B4
    public AgentTaskDetail([Nullable(1)] AgentTaskSnapshot Snapshot, string ClickySlug, string ClickyName, string WorkspacePath, string TurnId, [Nullable(1)] string ProgressSummary, string ThinkingHeading, [Nullable(1)] string StreamingResponse, [Nullable(1)] string StreamingCommentary, [Nullable(1)] IReadOnlyList<AgentActivityItem> Activity, [Nullable(1)] IReadOnlyList<string> UserPrompts, string Summary, string DoneTitle, [Nullable(1)] IReadOnlyList<string> NextActions, [Nullable(1)] IReadOnlyList<string> Artifacts, string FailureMessage, CompanionError Error, bool RequiresExtraUsageApproval, bool DidRefundEarlyStop, AgentTurnLease Lease, long InputTokens, long CachedInputTokens, long OutputTokens, bool IsRoutineRun, bool IsDemo = false, [Nullable(1)] string LaunchSource = "voice", string JobId = null, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<AgentPlanStep> Plan = null, ClickyAnswer Answer = null, ClickyJobUserStep PendingUserStep = null, bool IsHarnessTurn = false, bool IsNewJobRun = false, bool IsSentFromItsChat = false, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> SitesRead = null, string FailureDiagnostic = null, bool InterruptedUnexpectedly = false)
    {
      this.Snapshot = Snapshot;
      this.ClickySlug = ClickySlug;
      this.ClickyName = ClickyName;
      this.WorkspacePath = WorkspacePath;
      this.TurnId = TurnId;
      this.ProgressSummary = ProgressSummary;
      this.ThinkingHeading = ThinkingHeading;
      this.StreamingResponse = StreamingResponse;
      this.StreamingCommentary = StreamingCommentary;
      this.Activity = Activity;
      this.UserPrompts = UserPrompts;
      this.Summary = Summary;
      this.DoneTitle = DoneTitle;
      this.NextActions = NextActions;
      this.Artifacts = Artifacts;
      this.FailureMessage = FailureMessage;
      this.Error = Error;
      this.RequiresExtraUsageApproval = RequiresExtraUsageApproval;
      this.DidRefundEarlyStop = DidRefundEarlyStop;
      this.Lease = Lease;
      this.InputTokens = InputTokens;
      this.CachedInputTokens = CachedInputTokens;
      this.OutputTokens = OutputTokens;
      this.IsRoutineRun = IsRoutineRun;
      this.IsDemo = IsDemo;
      this.LaunchSource = LaunchSource;
      this.JobId = JobId;
      this.Plan = Plan;
      this.Answer = Answer;
      this.PendingUserStep = PendingUserStep;
      this.IsHarnessTurn = IsHarnessTurn;
      this.IsNewJobRun = IsNewJobRun;
      this.IsSentFromItsChat = IsSentFromItsChat;
      this.SitesRead = SitesRead;
      this.FailureDiagnostic = FailureDiagnostic;
      this.InterruptedUnexpectedly = InterruptedUnexpectedly;
      base..ctor();
    }

    // Token: 0x1700001D RID: 29
    // (get) Token: 0x06000061 RID: 97 RVA: 0x000D71E4 File Offset: 0x000D71E4
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(AgentTaskDetail);
      }
    }

    // Token: 0x1700001E RID: 30
    // (get) Token: 0x06000062 RID: 98 RVA: 0x000D71F0 File Offset: 0x000D71F0
    // (set) Token: 0x06000063 RID: 99 RVA: 0x000D71F8 File Offset: 0x000D71F8
    [Nullable(1)]
    public AgentTaskSnapshot Snapshot
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x1700001F RID: 31
    // (get) Token: 0x06000064 RID: 100 RVA: 0x000D7204 File Offset: 0x000D7204
    // (set) Token: 0x06000065 RID: 101 RVA: 0x000D720C File Offset: 0x000D720C
    public string ClickySlug { get; set; }

    // Token: 0x17000020 RID: 32
    // (get) Token: 0x06000066 RID: 102 RVA: 0x000D7218 File Offset: 0x000D7218
    // (set) Token: 0x06000067 RID: 103 RVA: 0x000D7220 File Offset: 0x000D7220
    public string ClickyName { get; set; }

    // Token: 0x17000021 RID: 33
    // (get) Token: 0x06000068 RID: 104 RVA: 0x000D722C File Offset: 0x000D722C
    // (set) Token: 0x06000069 RID: 105 RVA: 0x000D7234 File Offset: 0x000D7234
    public string WorkspacePath { get; set; }

    // Token: 0x17000022 RID: 34
    // (get) Token: 0x0600006A RID: 106 RVA: 0x000D7240 File Offset: 0x000D7240
    // (set) Token: 0x0600006B RID: 107 RVA: 0x000D7248 File Offset: 0x000D7248
    public string TurnId { get; set; }

    // Token: 0x17000023 RID: 35
    // (get) Token: 0x0600006C RID: 108 RVA: 0x000D7254 File Offset: 0x000D7254
    // (set) Token: 0x0600006D RID: 109 RVA: 0x000D725C File Offset: 0x000D725C
    [Nullable(1)]
    public string ProgressSummary
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000024 RID: 36
    // (get) Token: 0x0600006E RID: 110 RVA: 0x000D7268 File Offset: 0x000D7268
    // (set) Token: 0x0600006F RID: 111 RVA: 0x000D7270 File Offset: 0x000D7270
    public string ThinkingHeading { get; set; }

    // Token: 0x17000025 RID: 37
    // (get) Token: 0x06000070 RID: 112 RVA: 0x000D727C File Offset: 0x000D727C
    // (set) Token: 0x06000071 RID: 113 RVA: 0x000D7284 File Offset: 0x000D7284
    [Nullable(1)]
    public string StreamingResponse
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000026 RID: 38
    // (get) Token: 0x06000072 RID: 114 RVA: 0x000D7290 File Offset: 0x000D7290
    // (set) Token: 0x06000073 RID: 115 RVA: 0x000D7298 File Offset: 0x000D7298
    [Nullable(1)]
    public string StreamingCommentary
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000027 RID: 39
    // (get) Token: 0x06000074 RID: 116 RVA: 0x000D72A4 File Offset: 0x000D72A4
    // (set) Token: 0x06000075 RID: 117 RVA: 0x000D72AC File Offset: 0x000D72AC
    [Nullable(1)]
    public IReadOnlyList<AgentActivityItem> Activity
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000028 RID: 40
    // (get) Token: 0x06000076 RID: 118 RVA: 0x000D72B8 File Offset: 0x000D72B8
    // (set) Token: 0x06000077 RID: 119 RVA: 0x000D72C0 File Offset: 0x000D72C0
    [Nullable(1)]
    public IReadOnlyList<string> UserPrompts
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000029 RID: 41
    // (get) Token: 0x06000078 RID: 120 RVA: 0x000D72CC File Offset: 0x000D72CC
    // (set) Token: 0x06000079 RID: 121 RVA: 0x000D72D4 File Offset: 0x000D72D4
    public string Summary { get; set; }

    // Token: 0x1700002A RID: 42
    // (get) Token: 0x0600007A RID: 122 RVA: 0x000D72E0 File Offset: 0x000D72E0
    // (set) Token: 0x0600007B RID: 123 RVA: 0x000D72E8 File Offset: 0x000D72E8
    public string DoneTitle { get; set; }

    // Token: 0x1700002B RID: 43
    // (get) Token: 0x0600007C RID: 124 RVA: 0x000D72F4 File Offset: 0x000D72F4
    // (set) Token: 0x0600007D RID: 125 RVA: 0x000D72FC File Offset: 0x000D72FC
    [Nullable(1)]
    public IReadOnlyList<string> NextActions
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x1700002C RID: 44
    // (get) Token: 0x0600007E RID: 126 RVA: 0x000D7308 File Offset: 0x000D7308
    // (set) Token: 0x0600007F RID: 127 RVA: 0x000D7310 File Offset: 0x000D7310
    [Nullable(1)]
    public IReadOnlyList<string> Artifacts
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x1700002D RID: 45
    // (get) Token: 0x06000080 RID: 128 RVA: 0x000D731C File Offset: 0x000D731C
    // (set) Token: 0x06000081 RID: 129 RVA: 0x000D7324 File Offset: 0x000D7324
    public string FailureMessage { get; set; }

    // Token: 0x1700002E RID: 46
    // (get) Token: 0x06000082 RID: 130 RVA: 0x000D7330 File Offset: 0x000D7330
    // (set) Token: 0x06000083 RID: 131 RVA: 0x000D7338 File Offset: 0x000D7338
    public CompanionError Error { get; set; }

    // Token: 0x1700002F RID: 47
    // (get) Token: 0x06000084 RID: 132 RVA: 0x000D7344 File Offset: 0x000D7344
    // (set) Token: 0x06000085 RID: 133 RVA: 0x000D734C File Offset: 0x000D734C
    public bool RequiresExtraUsageApproval { get; set; }

    // Token: 0x17000030 RID: 48
    // (get) Token: 0x06000086 RID: 134 RVA: 0x000D7358 File Offset: 0x000D7358
    // (set) Token: 0x06000087 RID: 135 RVA: 0x000D7360 File Offset: 0x000D7360
    public bool DidRefundEarlyStop { get; set; }

    // Token: 0x17000031 RID: 49
    // (get) Token: 0x06000088 RID: 136 RVA: 0x000D736C File Offset: 0x000D736C
    // (set) Token: 0x06000089 RID: 137 RVA: 0x000D7374 File Offset: 0x000D7374
    public AgentTurnLease Lease { get; set; }

    // Token: 0x17000032 RID: 50
    // (get) Token: 0x0600008A RID: 138 RVA: 0x000D7380 File Offset: 0x000D7380
    // (set) Token: 0x0600008B RID: 139 RVA: 0x000D7388 File Offset: 0x000D7388
    public long InputTokens { get; set; }

    // Token: 0x17000033 RID: 51
    // (get) Token: 0x0600008C RID: 140 RVA: 0x000D7394 File Offset: 0x000D7394
    // (set) Token: 0x0600008D RID: 141 RVA: 0x000D739C File Offset: 0x000D739C
    public long CachedInputTokens { get; set; }

    // Token: 0x17000034 RID: 52
    // (get) Token: 0x0600008E RID: 142 RVA: 0x000D73A8 File Offset: 0x000D73A8
    // (set) Token: 0x0600008F RID: 143 RVA: 0x000D73B0 File Offset: 0x000D73B0
    public long OutputTokens { get; set; }

    // Token: 0x17000035 RID: 53
    // (get) Token: 0x06000090 RID: 144 RVA: 0x000D73BC File Offset: 0x000D73BC
    // (set) Token: 0x06000091 RID: 145 RVA: 0x000D73C4 File Offset: 0x000D73C4
    public bool IsRoutineRun { get; set; }

    // Token: 0x17000036 RID: 54
    // (get) Token: 0x06000092 RID: 146 RVA: 0x000D73D0 File Offset: 0x000D73D0
    // (set) Token: 0x06000093 RID: 147 RVA: 0x000D73D8 File Offset: 0x000D73D8
    public bool IsDemo { get; set; }

    // Token: 0x17000037 RID: 55
    // (get) Token: 0x06000094 RID: 148 RVA: 0x000D73E4 File Offset: 0x000D73E4
    // (set) Token: 0x06000095 RID: 149 RVA: 0x000D73EC File Offset: 0x000D73EC
    [Nullable(1)]
    public string LaunchSource
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000038 RID: 56
    // (get) Token: 0x06000096 RID: 150 RVA: 0x000D73F8 File Offset: 0x000D73F8
    // (set) Token: 0x06000097 RID: 151 RVA: 0x000D7400 File Offset: 0x000D7400
    public string JobId { get; set; }

    // Token: 0x17000039 RID: 57
    // (get) Token: 0x06000098 RID: 152 RVA: 0x000D740C File Offset: 0x000D740C
    // (set) Token: 0x06000099 RID: 153 RVA: 0x000D7414 File Offset: 0x000D7414
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<AgentPlanStep> Plan
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x1700003A RID: 58
    // (get) Token: 0x0600009A RID: 154 RVA: 0x000D7420 File Offset: 0x000D7420
    // (set) Token: 0x0600009B RID: 155 RVA: 0x000D7428 File Offset: 0x000D7428
    public ClickyAnswer Answer { get; set; }

    // Token: 0x1700003B RID: 59
    // (get) Token: 0x0600009C RID: 156 RVA: 0x000D7434 File Offset: 0x000D7434
    // (set) Token: 0x0600009D RID: 157 RVA: 0x000D743C File Offset: 0x000D743C
    public ClickyJobUserStep PendingUserStep { get; set; }

    // Token: 0x1700003C RID: 60
    // (get) Token: 0x0600009E RID: 158 RVA: 0x000D7448 File Offset: 0x000D7448
    // (set) Token: 0x0600009F RID: 159 RVA: 0x000D7450 File Offset: 0x000D7450
    public bool IsHarnessTurn { get; set; }

    // Token: 0x1700003D RID: 61
    // (get) Token: 0x060000A0 RID: 160 RVA: 0x000D745C File Offset: 0x000D745C
    // (set) Token: 0x060000A1 RID: 161 RVA: 0x000D7464 File Offset: 0x000D7464
    public bool IsNewJobRun { get; set; }

    // Token: 0x1700003E RID: 62
    // (get) Token: 0x060000A2 RID: 162 RVA: 0x000D7470 File Offset: 0x000D7470
    // (set) Token: 0x060000A3 RID: 163 RVA: 0x000D7478 File Offset: 0x000D7478
    public bool IsSentFromItsChat { get; set; }

    // Token: 0x1700003F RID: 63
    // (get) Token: 0x060000A4 RID: 164 RVA: 0x000D7484 File Offset: 0x000D7484
    // (set) Token: 0x060000A5 RID: 165 RVA: 0x000D748C File Offset: 0x000D748C
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> SitesRead
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000040 RID: 64
    // (get) Token: 0x060000A6 RID: 166 RVA: 0x000D7498 File Offset: 0x000D7498
    // (set) Token: 0x060000A7 RID: 167 RVA: 0x000D74A0 File Offset: 0x000D74A0
    public string FailureDiagnostic { get; set; }

    // Token: 0x17000041 RID: 65
    // (get) Token: 0x060000A8 RID: 168 RVA: 0x000D74AC File Offset: 0x000D74AC
    // (set) Token: 0x060000A9 RID: 169 RVA: 0x000D74B4 File Offset: 0x000D74B4
    public bool InterruptedUnexpectedly { get; set; }

    // Token: 0x060000AA RID: 170 RVA: 0x000D74C0 File Offset: 0x000D74C0
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentTaskDetail");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060000AB RID: 171 RVA: 0x000D750C File Offset: 0x000D750C
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Snapshot = ");
      builder.Append(this.Snapshot);
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      builder.Append(", ClickyName = ");
      builder.Append(this.ClickyName);
      builder.Append(", WorkspacePath = ");
      builder.Append(this.WorkspacePath);
      builder.Append(", TurnId = ");
      builder.Append(this.TurnId);
      builder.Append(", ProgressSummary = ");
      builder.Append(this.ProgressSummary);
      builder.Append(", ThinkingHeading = ");
      builder.Append(this.ThinkingHeading);
      builder.Append(", StreamingResponse = ");
      builder.Append(this.StreamingResponse);
      builder.Append(", StreamingCommentary = ");
      builder.Append(this.StreamingCommentary);
      builder.Append(", Activity = ");
      builder.Append(this.Activity);
      builder.Append(", UserPrompts = ");
      builder.Append(this.UserPrompts);
      builder.Append(", Summary = ");
      builder.Append(this.Summary);
      builder.Append(", DoneTitle = ");
      builder.Append(this.DoneTitle);
      builder.Append(", NextActions = ");
      builder.Append(this.NextActions);
      builder.Append(", Artifacts = ");
      builder.Append(this.Artifacts);
      builder.Append(", FailureMessage = ");
      builder.Append(this.FailureMessage);
      builder.Append(", Error = ");
      builder.Append(this.Error);
      builder.Append(", RequiresExtraUsageApproval = ");
      builder.Append(this.RequiresExtraUsageApproval.ToString());
      builder.Append(", DidRefundEarlyStop = ");
      builder.Append(this.DidRefundEarlyStop.ToString());
      builder.Append(", Lease = ");
      builder.Append(this.Lease);
      builder.Append(", InputTokens = ");
      builder.Append(this.InputTokens.ToString());
      builder.Append(", CachedInputTokens = ");
      builder.Append(this.CachedInputTokens.ToString());
      builder.Append(", OutputTokens = ");
      builder.Append(this.OutputTokens.ToString());
      builder.Append(", IsRoutineRun = ");
      builder.Append(this.IsRoutineRun.ToString());
      builder.Append(", IsDemo = ");
      builder.Append(this.IsDemo.ToString());
      builder.Append(", LaunchSource = ");
      builder.Append(this.LaunchSource);
      builder.Append(", JobId = ");
      builder.Append(this.JobId);
      builder.Append(", Plan = ");
      builder.Append(this.Plan);
      builder.Append(", Answer = ");
      builder.Append(this.Answer);
      builder.Append(", PendingUserStep = ");
      builder.Append(this.PendingUserStep);
      builder.Append(", IsHarnessTurn = ");
      builder.Append(this.IsHarnessTurn.ToString());
      builder.Append(", IsNewJobRun = ");
      builder.Append(this.IsNewJobRun.ToString());
      builder.Append(", IsSentFromItsChat = ");
      builder.Append(this.IsSentFromItsChat.ToString());
      builder.Append(", SitesRead = ");
      builder.Append(this.SitesRead);
      builder.Append(", FailureDiagnostic = ");
      builder.Append(this.FailureDiagnostic);
      builder.Append(", InterruptedUnexpectedly = ");
      builder.Append(this.InterruptedUnexpectedly.ToString());
      return true;
    }

    // Token: 0x060000AC RID: 172 RVA: 0x000D7940 File Offset: 0x000D7940
    [CompilerGenerated]
    public static bool operator !=(AgentTaskDetail left, AgentTaskDetail right)
    {
      return !(left == right);
    }

    // Token: 0x060000AD RID: 173 RVA: 0x000D794C File Offset: 0x000D794C
    [CompilerGenerated]
    public static bool operator ==(AgentTaskDetail left, AgentTaskDetail right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060000AE RID: 174 RVA: 0x000D7960 File Offset: 0x000D7960
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((((((((((((((((((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<AgentTaskSnapshot>.Default.GetHashCode(this.<Snapshot>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickyName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WorkspacePath>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TurnId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ProgressSummary>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThinkingHeading>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<StreamingResponse>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<StreamingCommentary>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<AgentActivityItem>>.Default.GetHashCode(this.<Activity>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<UserPrompts>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Summary>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DoneTitle>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<NextActions>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Artifacts>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FailureMessage>k__BackingField)) * -1521134295 + EqualityComparer<CompanionError>.Default.GetHashCode(this.<Error>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<RequiresExtraUsageApproval>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<DidRefundEarlyStop>k__BackingField)) * -1521134295 + EqualityComparer<AgentTurnLease>.Default.GetHashCode(this.<Lease>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<InputTokens>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<CachedInputTokens>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<OutputTokens>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsRoutineRun>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsDemo>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LaunchSource>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<JobId>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<AgentPlanStep>>.Default.GetHashCode(this.<Plan>k__BackingField)) * -1521134295 + EqualityComparer<ClickyAnswer>.Default.GetHashCode(this.<Answer>k__BackingField)) * -1521134295 + EqualityComparer<ClickyJobUserStep>.Default.GetHashCode(this.<PendingUserStep>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsHarnessTurn>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsNewJobRun>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsSentFromItsChat>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<SitesRead>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FailureDiagnostic>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<InterruptedUnexpectedly>k__BackingField);
    }

    // Token: 0x060000AF RID: 175 RVA: 0x000D7CBC File Offset: 0x000D7CBC
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentTaskDetail);
    }

    // Token: 0x060000B0 RID: 176 RVA: 0x000D7CCC File Offset: 0x000D7CCC
    [CompilerGenerated]
    public bool Equals(AgentTaskDetail other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<AgentTaskSnapshot>.Default.Equals(this.<Snapshot>k__BackingField, other.<Snapshot>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickyName>k__BackingField, other.<ClickyName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WorkspacePath>k__BackingField, other.<WorkspacePath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TurnId>k__BackingField, other.<TurnId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ProgressSummary>k__BackingField, other.<ProgressSummary>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThinkingHeading>k__BackingField, other.<ThinkingHeading>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<StreamingResponse>k__BackingField, other.<StreamingResponse>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<StreamingCommentary>k__BackingField, other.<StreamingCommentary>k__BackingField) && EqualityComparer<IReadOnlyList<AgentActivityItem>>.Default.Equals(this.<Activity>k__BackingField, other.<Activity>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<UserPrompts>k__BackingField, other.<UserPrompts>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Summary>k__BackingField, other.<Summary>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DoneTitle>k__BackingField, other.<DoneTitle>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<NextActions>k__BackingField, other.<NextActions>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Artifacts>k__BackingField, other.<Artifacts>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FailureMessage>k__BackingField, other.<FailureMessage>k__BackingField) && EqualityComparer<CompanionError>.Default.Equals(this.<Error>k__BackingField, other.<Error>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<RequiresExtraUsageApproval>k__BackingField, other.<RequiresExtraUsageApproval>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<DidRefundEarlyStop>k__BackingField, other.<DidRefundEarlyStop>k__BackingField) && EqualityComparer<AgentTurnLease>.Default.Equals(this.<Lease>k__BackingField, other.<Lease>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<InputTokens>k__BackingField, other.<InputTokens>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<CachedInputTokens>k__BackingField, other.<CachedInputTokens>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<OutputTokens>k__BackingField, other.<OutputTokens>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsRoutineRun>k__BackingField, other.<IsRoutineRun>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsDemo>k__BackingField, other.<IsDemo>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LaunchSource>k__BackingField, other.<LaunchSource>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<JobId>k__BackingField, other.<JobId>k__BackingField) && EqualityComparer<IReadOnlyList<AgentPlanStep>>.Default.Equals(this.<Plan>k__BackingField, other.<Plan>k__BackingField) && EqualityComparer<ClickyAnswer>.Default.Equals(this.<Answer>k__BackingField, other.<Answer>k__BackingField) && EqualityComparer<ClickyJobUserStep>.Default.Equals(this.<PendingUserStep>k__BackingField, other.<PendingUserStep>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsHarnessTurn>k__BackingField, other.<IsHarnessTurn>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsNewJobRun>k__BackingField, other.<IsNewJobRun>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsSentFromItsChat>k__BackingField, other.<IsSentFromItsChat>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<SitesRead>k__BackingField, other.<SitesRead>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FailureDiagnostic>k__BackingField, other.<FailureDiagnostic>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<InterruptedUnexpectedly>k__BackingField, other.<InterruptedUnexpectedly>k__BackingField));
    }

    // Token: 0x060000B2 RID: 178 RVA: 0x000D80C0 File Offset: 0x000D80C0
    [CompilerGenerated]
    private AgentTaskDetail([Nullable(1)] AgentTaskDetail original)
    {
      this.Snapshot = original.<Snapshot>k__BackingField;
      this.ClickySlug = original.<ClickySlug>k__BackingField;
      this.ClickyName = original.<ClickyName>k__BackingField;
      this.WorkspacePath = original.<WorkspacePath>k__BackingField;
      this.TurnId = original.<TurnId>k__BackingField;
      this.ProgressSummary = original.<ProgressSummary>k__BackingField;
      this.ThinkingHeading = original.<ThinkingHeading>k__BackingField;
      this.StreamingResponse = original.<StreamingResponse>k__BackingField;
      this.StreamingCommentary = original.<StreamingCommentary>k__BackingField;
      this.Activity = original.<Activity>k__BackingField;
      this.UserPrompts = original.<UserPrompts>k__BackingField;
      this.Summary = original.<Summary>k__BackingField;
      this.DoneTitle = original.<DoneTitle>k__BackingField;
      this.NextActions = original.<NextActions>k__BackingField;
      this.Artifacts = original.<Artifacts>k__BackingField;
      this.FailureMessage = original.<FailureMessage>k__BackingField;
      this.Error = original.<Error>k__BackingField;
      this.RequiresExtraUsageApproval = original.<RequiresExtraUsageApproval>k__BackingField;
      this.DidRefundEarlyStop = original.<DidRefundEarlyStop>k__BackingField;
      this.Lease = original.<Lease>k__BackingField;
      this.InputTokens = original.<InputTokens>k__BackingField;
      this.CachedInputTokens = original.<CachedInputTokens>k__BackingField;
      this.OutputTokens = original.<OutputTokens>k__BackingField;
      this.IsRoutineRun = original.<IsRoutineRun>k__BackingField;
      this.IsDemo = original.<IsDemo>k__BackingField;
      this.LaunchSource = original.<LaunchSource>k__BackingField;
      this.JobId = original.<JobId>k__BackingField;
      this.Plan = original.<Plan>k__BackingField;
      this.Answer = original.<Answer>k__BackingField;
      this.PendingUserStep = original.<PendingUserStep>k__BackingField;
      this.IsHarnessTurn = original.<IsHarnessTurn>k__BackingField;
      this.IsNewJobRun = original.<IsNewJobRun>k__BackingField;
      this.IsSentFromItsChat = original.<IsSentFromItsChat>k__BackingField;
      this.SitesRead = original.<SitesRead>k__BackingField;
      this.FailureDiagnostic = original.<FailureDiagnostic>k__BackingField;
      this.InterruptedUnexpectedly = original.<InterruptedUnexpectedly>k__BackingField;
    }

    // Token: 0x060000B3 RID: 179 RVA: 0x000D8284 File Offset: 0x000D8284
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out AgentTaskSnapshot Snapshot, out string ClickySlug, out string ClickyName, out string WorkspacePath, out string TurnId, [Nullable(1)] out string ProgressSummary, out string ThinkingHeading, [Nullable(1)] out string StreamingResponse, [Nullable(1)] out string StreamingCommentary, [Nullable(1)] out IReadOnlyList<AgentActivityItem> Activity, [Nullable(1)] out IReadOnlyList<string> UserPrompts, out string Summary, out string DoneTitle, [Nullable(1)] out IReadOnlyList<string> NextActions, [Nullable(1)] out IReadOnlyList<string> Artifacts, out string FailureMessage, out CompanionError Error, out bool RequiresExtraUsageApproval, out bool DidRefundEarlyStop, out AgentTurnLease Lease, out long InputTokens, out long CachedInputTokens, out long OutputTokens, out bool IsRoutineRun, out bool IsDemo, [Nullable(1)] out string LaunchSource, out string JobId, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<AgentPlanStep> Plan, out ClickyAnswer Answer, out ClickyJobUserStep PendingUserStep, out bool IsHarnessTurn, out bool IsNewJobRun, out bool IsSentFromItsChat, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<string> SitesRead, out string FailureDiagnostic, out bool InterruptedUnexpectedly)
    {
      Snapshot = this.Snapshot;
      ClickySlug = this.ClickySlug;
      ClickyName = this.ClickyName;
      WorkspacePath = this.WorkspacePath;
      TurnId = this.TurnId;
      ProgressSummary = this.ProgressSummary;
      ThinkingHeading = this.ThinkingHeading;
      StreamingResponse = this.StreamingResponse;
      StreamingCommentary = this.StreamingCommentary;
      Activity = this.Activity;
      UserPrompts = this.UserPrompts;
      Summary = this.Summary;
      DoneTitle = this.DoneTitle;
      NextActions = this.NextActions;
      Artifacts = this.Artifacts;
      FailureMessage = this.FailureMessage;
      Error = this.Error;
      RequiresExtraUsageApproval = this.RequiresExtraUsageApproval;
      DidRefundEarlyStop = this.DidRefundEarlyStop;
      Lease = this.Lease;
      InputTokens = this.InputTokens;
      CachedInputTokens = this.CachedInputTokens;
      OutputTokens = this.OutputTokens;
      IsRoutineRun = this.IsRoutineRun;
      IsDemo = this.IsDemo;
      LaunchSource = this.LaunchSource;
      JobId = this.JobId;
      Plan = this.Plan;
      Answer = this.Answer;
      PendingUserStep = this.PendingUserStep;
      IsHarnessTurn = this.IsHarnessTurn;
      IsNewJobRun = this.IsNewJobRun;
      IsSentFromItsChat = this.IsSentFromItsChat;
      SitesRead = this.SitesRead;
      FailureDiagnostic = this.FailureDiagnostic;
      InterruptedUnexpectedly = this.InterruptedUnexpectedly;
    }
  }
}
