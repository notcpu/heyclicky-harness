using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Tools
{
  // Token: 0x02000096 RID: 150
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentJobInfo : IEquatable<AgentJobInfo>
  {
    // Token: 0x06000946 RID: 2374 RVA: 0x0036D668 File Offset: 0x0036D668
    public AgentJobInfo([Nullable(2)] string ThreadId, string Title, string State, string FolderPath, IReadOnlyList<AgentJobRunInfo> Runs, string LatestReply, [Nullable(2)] string Summary, IReadOnlyList<string> ArtifactEntries, [Nullable(2)] AgentTaskInfo Live)
    {
      this.ThreadId = ThreadId;
      this.Title = Title;
      this.State = State;
      this.FolderPath = FolderPath;
      this.Runs = Runs;
      this.LatestReply = LatestReply;
      this.Summary = Summary;
      this.ArtifactEntries = ArtifactEntries;
      this.Live = Live;
      base..ctor();
    }

    // Token: 0x17000214 RID: 532
    // (get) Token: 0x06000947 RID: 2375 RVA: 0x0036D6C0 File Offset: 0x0036D6C0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentJobInfo);
      }
    }

    // Token: 0x17000215 RID: 533
    // (get) Token: 0x06000948 RID: 2376 RVA: 0x0036D6CC File Offset: 0x0036D6CC
    // (set) Token: 0x06000949 RID: 2377 RVA: 0x0036D6D4 File Offset: 0x0036D6D4
    [Nullable(2)]
    public string ThreadId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000216 RID: 534
    // (get) Token: 0x0600094A RID: 2378 RVA: 0x0036D6E0 File Offset: 0x0036D6E0
    // (set) Token: 0x0600094B RID: 2379 RVA: 0x0036D6E8 File Offset: 0x0036D6E8
    public string Title { get; set; }

    // Token: 0x17000217 RID: 535
    // (get) Token: 0x0600094C RID: 2380 RVA: 0x0036D6F4 File Offset: 0x0036D6F4
    // (set) Token: 0x0600094D RID: 2381 RVA: 0x0036D6FC File Offset: 0x0036D6FC
    public string State { get; set; }

    // Token: 0x17000218 RID: 536
    // (get) Token: 0x0600094E RID: 2382 RVA: 0x0036D708 File Offset: 0x0036D708
    // (set) Token: 0x0600094F RID: 2383 RVA: 0x0036D710 File Offset: 0x0036D710
    public string FolderPath { get; set; }

    // Token: 0x17000219 RID: 537
    // (get) Token: 0x06000950 RID: 2384 RVA: 0x0036D71C File Offset: 0x0036D71C
    // (set) Token: 0x06000951 RID: 2385 RVA: 0x0036D724 File Offset: 0x0036D724
    public IReadOnlyList<AgentJobRunInfo> Runs { get; set; }

    // Token: 0x1700021A RID: 538
    // (get) Token: 0x06000952 RID: 2386 RVA: 0x0036D730 File Offset: 0x0036D730
    // (set) Token: 0x06000953 RID: 2387 RVA: 0x0036D738 File Offset: 0x0036D738
    public string LatestReply { get; set; }

    // Token: 0x1700021B RID: 539
    // (get) Token: 0x06000954 RID: 2388 RVA: 0x0036D744 File Offset: 0x0036D744
    // (set) Token: 0x06000955 RID: 2389 RVA: 0x0036D74C File Offset: 0x0036D74C
    [Nullable(2)]
    public string Summary
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700021C RID: 540
    // (get) Token: 0x06000956 RID: 2390 RVA: 0x0036D758 File Offset: 0x0036D758
    // (set) Token: 0x06000957 RID: 2391 RVA: 0x0036D760 File Offset: 0x0036D760
    public IReadOnlyList<string> ArtifactEntries { get; set; }

    // Token: 0x1700021D RID: 541
    // (get) Token: 0x06000958 RID: 2392 RVA: 0x0036D76C File Offset: 0x0036D76C
    // (set) Token: 0x06000959 RID: 2393 RVA: 0x0036D774 File Offset: 0x0036D774
    [Nullable(2)]
    public AgentTaskInfo Live
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x0600095A RID: 2394 RVA: 0x0036D780 File Offset: 0x0036D780
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentJobInfo");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600095B RID: 2395 RVA: 0x0036D7CC File Offset: 0x0036D7CC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ThreadId = ");
      builder.Append(this.ThreadId);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", State = ");
      builder.Append(this.State);
      builder.Append(", FolderPath = ");
      builder.Append(this.FolderPath);
      builder.Append(", Runs = ");
      builder.Append(this.Runs);
      builder.Append(", LatestReply = ");
      builder.Append(this.LatestReply);
      builder.Append(", Summary = ");
      builder.Append(this.Summary);
      builder.Append(", ArtifactEntries = ");
      builder.Append(this.ArtifactEntries);
      builder.Append(", Live = ");
      builder.Append(this.Live);
      return true;
    }

    // Token: 0x0600095C RID: 2396 RVA: 0x0036D8C0 File Offset: 0x0036D8C0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentJobInfo left, AgentJobInfo right)
    {
      return !(left == right);
    }

    // Token: 0x0600095D RID: 2397 RVA: 0x0036D8CC File Offset: 0x0036D8CC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentJobInfo left, AgentJobInfo right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600095E RID: 2398 RVA: 0x0036D8E0 File Offset: 0x0036D8E0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<State>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FolderPath>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<AgentJobRunInfo>>.Default.GetHashCode(this.<Runs>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LatestReply>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Summary>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ArtifactEntries>k__BackingField)) * -1521134295 + EqualityComparer<AgentTaskInfo>.Default.GetHashCode(this.<Live>k__BackingField);
    }

    // Token: 0x0600095F RID: 2399 RVA: 0x0036D9CC File Offset: 0x0036D9CC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentJobInfo);
    }

    // Token: 0x06000960 RID: 2400 RVA: 0x0036D9DC File Offset: 0x0036D9DC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentJobInfo other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<State>k__BackingField, other.<State>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FolderPath>k__BackingField, other.<FolderPath>k__BackingField) && EqualityComparer<IReadOnlyList<AgentJobRunInfo>>.Default.Equals(this.<Runs>k__BackingField, other.<Runs>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LatestReply>k__BackingField, other.<LatestReply>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Summary>k__BackingField, other.<Summary>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ArtifactEntries>k__BackingField, other.<ArtifactEntries>k__BackingField) && EqualityComparer<AgentTaskInfo>.Default.Equals(this.<Live>k__BackingField, other.<Live>k__BackingField));
    }

    // Token: 0x06000962 RID: 2402 RVA: 0x0036DAF8 File Offset: 0x0036DAF8
    [CompilerGenerated]
    private AgentJobInfo(AgentJobInfo original)
    {
      this.ThreadId = original.<ThreadId>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.State = original.<State>k__BackingField;
      this.FolderPath = original.<FolderPath>k__BackingField;
      this.Runs = original.<Runs>k__BackingField;
      this.LatestReply = original.<LatestReply>k__BackingField;
      this.Summary = original.<Summary>k__BackingField;
      this.ArtifactEntries = original.<ArtifactEntries>k__BackingField;
      this.Live = original.<Live>k__BackingField;
    }

    // Token: 0x06000963 RID: 2403 RVA: 0x0036DB78 File Offset: 0x0036DB78
    [CompilerGenerated]
    public void Deconstruct([Nullable(2)] out string ThreadId, out string Title, out string State, out string FolderPath, out IReadOnlyList<AgentJobRunInfo> Runs, out string LatestReply, [Nullable(2)] out string Summary, out IReadOnlyList<string> ArtifactEntries, [Nullable(2)] out AgentTaskInfo Live)
    {
      ThreadId = this.ThreadId;
      Title = this.Title;
      State = this.State;
      FolderPath = this.FolderPath;
      Runs = this.Runs;
      LatestReply = this.LatestReply;
      Summary = this.Summary;
      ArtifactEntries = this.ArtifactEntries;
      Live = this.Live;
    }
  }
}
