using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Tools
{
  // Token: 0x02000095 RID: 149
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentJobRunInfo : IEquatable<AgentJobRunInfo>
  {
    // Token: 0x06000932 RID: 2354 RVA: 0x0036D370 File Offset: 0x0036D370
    public AgentJobRunInfo(string Ask, DateTimeOffset StartedAt, string Outcome, [Nullable(2)] string Summary)
    {
      this.Ask = Ask;
      this.StartedAt = StartedAt;
      this.Outcome = Outcome;
      this.Summary = Summary;
      base..ctor();
    }

    // Token: 0x1700020F RID: 527
    // (get) Token: 0x06000933 RID: 2355 RVA: 0x0036D398 File Offset: 0x0036D398
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentJobRunInfo);
      }
    }

    // Token: 0x17000210 RID: 528
    // (get) Token: 0x06000934 RID: 2356 RVA: 0x0036D3A4 File Offset: 0x0036D3A4
    // (set) Token: 0x06000935 RID: 2357 RVA: 0x0036D3AC File Offset: 0x0036D3AC
    public string Ask { get; set; }

    // Token: 0x17000211 RID: 529
    // (get) Token: 0x06000936 RID: 2358 RVA: 0x0036D3B8 File Offset: 0x0036D3B8
    // (set) Token: 0x06000937 RID: 2359 RVA: 0x0036D3C0 File Offset: 0x0036D3C0
    public DateTimeOffset StartedAt { get; set; }

    // Token: 0x17000212 RID: 530
    // (get) Token: 0x06000938 RID: 2360 RVA: 0x0036D3CC File Offset: 0x0036D3CC
    // (set) Token: 0x06000939 RID: 2361 RVA: 0x0036D3D4 File Offset: 0x0036D3D4
    public string Outcome { get; set; }

    // Token: 0x17000213 RID: 531
    // (get) Token: 0x0600093A RID: 2362 RVA: 0x0036D3E0 File Offset: 0x0036D3E0
    // (set) Token: 0x0600093B RID: 2363 RVA: 0x0036D3E8 File Offset: 0x0036D3E8
    [Nullable(2)]
    public string Summary
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x0600093C RID: 2364 RVA: 0x0036D3F4 File Offset: 0x0036D3F4
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentJobRunInfo");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600093D RID: 2365 RVA: 0x0036D440 File Offset: 0x0036D440
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Ask = ");
      builder.Append(this.Ask);
      builder.Append(", StartedAt = ");
      builder.Append(this.StartedAt.ToString());
      builder.Append(", Outcome = ");
      builder.Append(this.Outcome);
      builder.Append(", Summary = ");
      builder.Append(this.Summary);
      return true;
    }

    // Token: 0x0600093E RID: 2366 RVA: 0x0036D4C8 File Offset: 0x0036D4C8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentJobRunInfo left, AgentJobRunInfo right)
    {
      return !(left == right);
    }

    // Token: 0x0600093F RID: 2367 RVA: 0x0036D4D4 File Offset: 0x0036D4D4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentJobRunInfo left, AgentJobRunInfo right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000940 RID: 2368 RVA: 0x0036D4E8 File Offset: 0x0036D4E8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Ask>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<StartedAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Outcome>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Summary>k__BackingField);
    }

    // Token: 0x06000941 RID: 2369 RVA: 0x0036D564 File Offset: 0x0036D564
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentJobRunInfo);
    }

    // Token: 0x06000942 RID: 2370 RVA: 0x0036D574 File Offset: 0x0036D574
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentJobRunInfo other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Ask>k__BackingField, other.<Ask>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<StartedAt>k__BackingField, other.<StartedAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Outcome>k__BackingField, other.<Outcome>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Summary>k__BackingField, other.<Summary>k__BackingField));
    }

    // Token: 0x06000944 RID: 2372 RVA: 0x0036D608 File Offset: 0x0036D608
    [CompilerGenerated]
    private AgentJobRunInfo(AgentJobRunInfo original)
    {
      this.Ask = original.<Ask>k__BackingField;
      this.StartedAt = original.<StartedAt>k__BackingField;
      this.Outcome = original.<Outcome>k__BackingField;
      this.Summary = original.<Summary>k__BackingField;
    }

    // Token: 0x06000945 RID: 2373 RVA: 0x0036D640 File Offset: 0x0036D640
    [CompilerGenerated]
    public void Deconstruct(out string Ask, out DateTimeOffset StartedAt, out string Outcome, [Nullable(2)] out string Summary)
    {
      Ask = this.Ask;
      StartedAt = this.StartedAt;
      Outcome = this.Outcome;
      Summary = this.Summary;
    }
  }
}
