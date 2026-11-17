using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Tools
{
  // Token: 0x02000093 RID: 147
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentTaskInfo : IEquatable<AgentTaskInfo>
  {
    // Token: 0x060008EB RID: 2283 RVA: 0x0036C5C4 File Offset: 0x0036C5C4
    public AgentTaskInfo(string TaskId, [Nullable(2)] string ThreadId, string Title, AgentTaskStatus Status, [Nullable(2)] string ClickySlug, [Nullable(2)] string ClickyName, string ProgressSummary, [Nullable(2)] string Summary, IReadOnlyList<string> Artifacts, bool RequiresExtraUsageApproval, IReadOnlyList<string> UserPrompts, IReadOnlyList<string> Commentary, string StreamingResponse, [Nullable(2)] string WorkspacePath, DateTimeOffset StartedAt)
    {
      this.TaskId = TaskId;
      this.ThreadId = ThreadId;
      this.Title = Title;
      this.Status = Status;
      this.ClickySlug = ClickySlug;
      this.ClickyName = ClickyName;
      this.ProgressSummary = ProgressSummary;
      this.Summary = Summary;
      this.Artifacts = Artifacts;
      this.RequiresExtraUsageApproval = RequiresExtraUsageApproval;
      this.UserPrompts = UserPrompts;
      this.Commentary = Commentary;
      this.StreamingResponse = StreamingResponse;
      this.WorkspacePath = WorkspacePath;
      this.StartedAt = StartedAt;
      base..ctor();
    }

    // Token: 0x170001F5 RID: 501
    // (get) Token: 0x060008EC RID: 2284 RVA: 0x0036C64C File Offset: 0x0036C64C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentTaskInfo);
      }
    }

    // Token: 0x170001F6 RID: 502
    // (get) Token: 0x060008ED RID: 2285 RVA: 0x0036C658 File Offset: 0x0036C658
    // (set) Token: 0x060008EE RID: 2286 RVA: 0x0036C660 File Offset: 0x0036C660
    public string TaskId { get; set; }

    // Token: 0x170001F7 RID: 503
    // (get) Token: 0x060008EF RID: 2287 RVA: 0x0036C66C File Offset: 0x0036C66C
    // (set) Token: 0x060008F0 RID: 2288 RVA: 0x0036C674 File Offset: 0x0036C674
    [Nullable(2)]
    public string ThreadId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170001F8 RID: 504
    // (get) Token: 0x060008F1 RID: 2289 RVA: 0x0036C680 File Offset: 0x0036C680
    // (set) Token: 0x060008F2 RID: 2290 RVA: 0x0036C688 File Offset: 0x0036C688
    public string Title { get; set; }

    // Token: 0x170001F9 RID: 505
    // (get) Token: 0x060008F3 RID: 2291 RVA: 0x0036C694 File Offset: 0x0036C694
    // (set) Token: 0x060008F4 RID: 2292 RVA: 0x0036C69C File Offset: 0x0036C69C
    public AgentTaskStatus Status { get; set; }

    // Token: 0x170001FA RID: 506
    // (get) Token: 0x060008F5 RID: 2293 RVA: 0x0036C6A8 File Offset: 0x0036C6A8
    // (set) Token: 0x060008F6 RID: 2294 RVA: 0x0036C6B0 File Offset: 0x0036C6B0
    [Nullable(2)]
    public string ClickySlug
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170001FB RID: 507
    // (get) Token: 0x060008F7 RID: 2295 RVA: 0x0036C6BC File Offset: 0x0036C6BC
    // (set) Token: 0x060008F8 RID: 2296 RVA: 0x0036C6C4 File Offset: 0x0036C6C4
    [Nullable(2)]
    public string ClickyName
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170001FC RID: 508
    // (get) Token: 0x060008F9 RID: 2297 RVA: 0x0036C6D0 File Offset: 0x0036C6D0
    // (set) Token: 0x060008FA RID: 2298 RVA: 0x0036C6D8 File Offset: 0x0036C6D8
    public string ProgressSummary { get; set; }

    // Token: 0x170001FD RID: 509
    // (get) Token: 0x060008FB RID: 2299 RVA: 0x0036C6E4 File Offset: 0x0036C6E4
    // (set) Token: 0x060008FC RID: 2300 RVA: 0x0036C6EC File Offset: 0x0036C6EC
    [Nullable(2)]
    public string Summary
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170001FE RID: 510
    // (get) Token: 0x060008FD RID: 2301 RVA: 0x0036C6F8 File Offset: 0x0036C6F8
    // (set) Token: 0x060008FE RID: 2302 RVA: 0x0036C700 File Offset: 0x0036C700
    public IReadOnlyList<string> Artifacts { get; set; }

    // Token: 0x170001FF RID: 511
    // (get) Token: 0x060008FF RID: 2303 RVA: 0x0036C70C File Offset: 0x0036C70C
    // (set) Token: 0x06000900 RID: 2304 RVA: 0x0036C714 File Offset: 0x0036C714
    public bool RequiresExtraUsageApproval { get; set; }

    // Token: 0x17000200 RID: 512
    // (get) Token: 0x06000901 RID: 2305 RVA: 0x0036C720 File Offset: 0x0036C720
    // (set) Token: 0x06000902 RID: 2306 RVA: 0x0036C728 File Offset: 0x0036C728
    public IReadOnlyList<string> UserPrompts { get; set; }

    // Token: 0x17000201 RID: 513
    // (get) Token: 0x06000903 RID: 2307 RVA: 0x0036C734 File Offset: 0x0036C734
    // (set) Token: 0x06000904 RID: 2308 RVA: 0x0036C73C File Offset: 0x0036C73C
    public IReadOnlyList<string> Commentary { get; set; }

    // Token: 0x17000202 RID: 514
    // (get) Token: 0x06000905 RID: 2309 RVA: 0x0036C748 File Offset: 0x0036C748
    // (set) Token: 0x06000906 RID: 2310 RVA: 0x0036C750 File Offset: 0x0036C750
    public string StreamingResponse { get; set; }

    // Token: 0x17000203 RID: 515
    // (get) Token: 0x06000907 RID: 2311 RVA: 0x0036C75C File Offset: 0x0036C75C
    // (set) Token: 0x06000908 RID: 2312 RVA: 0x0036C764 File Offset: 0x0036C764
    [Nullable(2)]
    public string WorkspacePath
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000204 RID: 516
    // (get) Token: 0x06000909 RID: 2313 RVA: 0x0036C770 File Offset: 0x0036C770
    // (set) Token: 0x0600090A RID: 2314 RVA: 0x0036C778 File Offset: 0x0036C778
    public DateTimeOffset StartedAt { get; set; }

    // Token: 0x17000205 RID: 517
    // (get) Token: 0x0600090B RID: 2315 RVA: 0x0036C784 File Offset: 0x0036C784
    public bool IsWorking
    {
      get
      {
        AgentTaskStatus status = this.Status;
        return status <= AgentTaskStatus.Running;
      }
    }

    // Token: 0x17000206 RID: 518
    // (get) Token: 0x0600090C RID: 2316 RVA: 0x0036C7A4 File Offset: 0x0036C7A4
    // (set) Token: 0x0600090D RID: 2317 RVA: 0x0036C7AC File Offset: 0x0036C7AC
    [Nullable(2)]
    public string PendingComputerUseRequest
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000207 RID: 519
    // (get) Token: 0x0600090E RID: 2318 RVA: 0x0036C7B8 File Offset: 0x0036C7B8
    // (set) Token: 0x0600090F RID: 2319 RVA: 0x0036C7C0 File Offset: 0x0036C7C0
    public bool IsDemo { get; set; }

    // Token: 0x06000910 RID: 2320 RVA: 0x0036C7CC File Offset: 0x0036C7CC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentTaskInfo");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000911 RID: 2321 RVA: 0x0036C818 File Offset: 0x0036C818
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("TaskId = ");
      builder.Append(this.TaskId);
      builder.Append(", ThreadId = ");
      builder.Append(this.ThreadId);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Status = ");
      builder.Append(this.Status.ToString());
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      builder.Append(", ClickyName = ");
      builder.Append(this.ClickyName);
      builder.Append(", ProgressSummary = ");
      builder.Append(this.ProgressSummary);
      builder.Append(", Summary = ");
      builder.Append(this.Summary);
      builder.Append(", Artifacts = ");
      builder.Append(this.Artifacts);
      builder.Append(", RequiresExtraUsageApproval = ");
      builder.Append(this.RequiresExtraUsageApproval.ToString());
      builder.Append(", UserPrompts = ");
      builder.Append(this.UserPrompts);
      builder.Append(", Commentary = ");
      builder.Append(this.Commentary);
      builder.Append(", StreamingResponse = ");
      builder.Append(this.StreamingResponse);
      builder.Append(", WorkspacePath = ");
      builder.Append(this.WorkspacePath);
      builder.Append(", StartedAt = ");
      builder.Append(this.StartedAt.ToString());
      builder.Append(", IsWorking = ");
      builder.Append(this.IsWorking.ToString());
      builder.Append(", PendingComputerUseRequest = ");
      builder.Append(this.PendingComputerUseRequest);
      builder.Append(", IsDemo = ");
      builder.Append(this.IsDemo.ToString());
      return true;
    }

    // Token: 0x06000912 RID: 2322 RVA: 0x0036CA34 File Offset: 0x0036CA34
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentTaskInfo left, AgentTaskInfo right)
    {
      return !(left == right);
    }

    // Token: 0x06000913 RID: 2323 RVA: 0x0036CA40 File Offset: 0x0036CA40
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentTaskInfo left, AgentTaskInfo right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000914 RID: 2324 RVA: 0x0036CA54 File Offset: 0x0036CA54
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TaskId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<AgentTaskStatus>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickyName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ProgressSummary>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Summary>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Artifacts>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<RequiresExtraUsageApproval>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<UserPrompts>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Commentary>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<StreamingResponse>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WorkspacePath>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<StartedAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PendingComputerUseRequest>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsDemo>k__BackingField);
    }

    // Token: 0x06000915 RID: 2325 RVA: 0x0036CBF8 File Offset: 0x0036CBF8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentTaskInfo);
    }

    // Token: 0x06000916 RID: 2326 RVA: 0x0036CC08 File Offset: 0x0036CC08
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentTaskInfo other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<TaskId>k__BackingField, other.<TaskId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<AgentTaskStatus>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickyName>k__BackingField, other.<ClickyName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ProgressSummary>k__BackingField, other.<ProgressSummary>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Summary>k__BackingField, other.<Summary>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Artifacts>k__BackingField, other.<Artifacts>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<RequiresExtraUsageApproval>k__BackingField, other.<RequiresExtraUsageApproval>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<UserPrompts>k__BackingField, other.<UserPrompts>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Commentary>k__BackingField, other.<Commentary>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<StreamingResponse>k__BackingField, other.<StreamingResponse>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WorkspacePath>k__BackingField, other.<WorkspacePath>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<StartedAt>k__BackingField, other.<StartedAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PendingComputerUseRequest>k__BackingField, other.<PendingComputerUseRequest>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsDemo>k__BackingField, other.<IsDemo>k__BackingField));
    }

    // Token: 0x06000918 RID: 2328 RVA: 0x0036CDFC File Offset: 0x0036CDFC
    [CompilerGenerated]
    private AgentTaskInfo(AgentTaskInfo original)
    {
      this.TaskId = original.<TaskId>k__BackingField;
      this.ThreadId = original.<ThreadId>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.Status = original.<Status>k__BackingField;
      this.ClickySlug = original.<ClickySlug>k__BackingField;
      this.ClickyName = original.<ClickyName>k__BackingField;
      this.ProgressSummary = original.<ProgressSummary>k__BackingField;
      this.Summary = original.<Summary>k__BackingField;
      this.Artifacts = original.<Artifacts>k__BackingField;
      this.RequiresExtraUsageApproval = original.<RequiresExtraUsageApproval>k__BackingField;
      this.UserPrompts = original.<UserPrompts>k__BackingField;
      this.Commentary = original.<Commentary>k__BackingField;
      this.StreamingResponse = original.<StreamingResponse>k__BackingField;
      this.WorkspacePath = original.<WorkspacePath>k__BackingField;
      this.StartedAt = original.<StartedAt>k__BackingField;
      this.PendingComputerUseRequest = original.<PendingComputerUseRequest>k__BackingField;
      this.IsDemo = original.<IsDemo>k__BackingField;
    }

    // Token: 0x06000919 RID: 2329 RVA: 0x0036CEDC File Offset: 0x0036CEDC
    [CompilerGenerated]
    public void Deconstruct(out string TaskId, [Nullable(2)] out string ThreadId, out string Title, out AgentTaskStatus Status, [Nullable(2)] out string ClickySlug, [Nullable(2)] out string ClickyName, out string ProgressSummary, [Nullable(2)] out string Summary, out IReadOnlyList<string> Artifacts, out bool RequiresExtraUsageApproval, out IReadOnlyList<string> UserPrompts, out IReadOnlyList<string> Commentary, out string StreamingResponse, [Nullable(2)] out string WorkspacePath, out DateTimeOffset StartedAt)
    {
      TaskId = this.TaskId;
      ThreadId = this.ThreadId;
      Title = this.Title;
      Status = this.Status;
      ClickySlug = this.ClickySlug;
      ClickyName = this.ClickyName;
      ProgressSummary = this.ProgressSummary;
      Summary = this.Summary;
      Artifacts = this.Artifacts;
      RequiresExtraUsageApproval = this.RequiresExtraUsageApproval;
      UserPrompts = this.UserPrompts;
      Commentary = this.Commentary;
      StreamingResponse = this.StreamingResponse;
      WorkspacePath = this.WorkspacePath;
      StartedAt = this.StartedAt;
    }
  }
}
