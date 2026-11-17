using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000C4 RID: 196
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ProactiveJobStatus : IEquatable<ProactiveJobStatus>
  {
    // Token: 0x06000CDC RID: 3292 RVA: 0x00380224 File Offset: 0x00380224
    [NullableContext(1)]
    public ProactiveJobStatus(string JobId, ProactiveJobState State, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<ProactiveSuggestionCard> Cards, [Nullable(2)] string Error, DateTimeOffset? BreakUntil)
    {
      this.JobId = JobId;
      this.State = State;
      this.Cards = Cards;
      this.Error = Error;
      this.BreakUntil = BreakUntil;
      base..ctor();
    }

    // Token: 0x170002D0 RID: 720
    // (get) Token: 0x06000CDD RID: 3293 RVA: 0x00380254 File Offset: 0x00380254
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ProactiveJobStatus);
      }
    }

    // Token: 0x170002D1 RID: 721
    // (get) Token: 0x06000CDE RID: 3294 RVA: 0x00380260 File Offset: 0x00380260
    // (set) Token: 0x06000CDF RID: 3295 RVA: 0x00380268 File Offset: 0x00380268
    [Nullable(1)]
    public string JobId
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170002D2 RID: 722
    // (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00380274 File Offset: 0x00380274
    // (set) Token: 0x06000CE1 RID: 3297 RVA: 0x0038027C File Offset: 0x0038027C
    public ProactiveJobState State { get; set; }

    // Token: 0x170002D3 RID: 723
    // (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00380288 File Offset: 0x00380288
    // (set) Token: 0x06000CE3 RID: 3299 RVA: 0x00380290 File Offset: 0x00380290
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<ProactiveSuggestionCard> Cards
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170002D4 RID: 724
    // (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0038029C File Offset: 0x0038029C
    // (set) Token: 0x06000CE5 RID: 3301 RVA: 0x003802A4 File Offset: 0x003802A4
    public string Error { get; set; }

    // Token: 0x170002D5 RID: 725
    // (get) Token: 0x06000CE6 RID: 3302 RVA: 0x003802B0 File Offset: 0x003802B0
    // (set) Token: 0x06000CE7 RID: 3303 RVA: 0x003802B8 File Offset: 0x003802B8
    public DateTimeOffset? BreakUntil { get; set; }

    // Token: 0x170002D6 RID: 726
    // (get) Token: 0x06000CE8 RID: 3304 RVA: 0x003802C4 File Offset: 0x003802C4
    public bool IsSettled
    {
      get
      {
        ProactiveJobState state = this.State;
        return state - ProactiveJobState.Succeeded <= 1;
      }
    }

    // Token: 0x170002D7 RID: 727
    // (get) Token: 0x06000CE9 RID: 3305 RVA: 0x003802E8 File Offset: 0x003802E8
    // (set) Token: 0x06000CEA RID: 3306 RVA: 0x003802F0 File Offset: 0x003802F0
    public string CuratedEvidence { get; set; }

    // Token: 0x170002D8 RID: 728
    // (get) Token: 0x06000CEB RID: 3307 RVA: 0x003802FC File Offset: 0x003802FC
    // (set) Token: 0x06000CEC RID: 3308 RVA: 0x00380304 File Offset: 0x00380304
    public string MorningHeadline { get; set; }

    // Token: 0x170002D9 RID: 729
    // (get) Token: 0x06000CED RID: 3309 RVA: 0x00380310 File Offset: 0x00380310
    // (set) Token: 0x06000CEE RID: 3310 RVA: 0x00380318 File Offset: 0x00380318
    public string MorningHeadlineLine { get; set; }

    // Token: 0x06000CEF RID: 3311 RVA: 0x00380324 File Offset: 0x00380324
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ProactiveJobStatus");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000CF0 RID: 3312 RVA: 0x00380370 File Offset: 0x00380370
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("JobId = ");
      builder.Append(this.JobId);
      builder.Append(", State = ");
      builder.Append(this.State.ToString());
      builder.Append(", Cards = ");
      builder.Append(this.Cards);
      builder.Append(", Error = ");
      builder.Append(this.Error);
      builder.Append(", BreakUntil = ");
      builder.Append(this.BreakUntil.ToString());
      builder.Append(", IsSettled = ");
      builder.Append(this.IsSettled.ToString());
      builder.Append(", CuratedEvidence = ");
      builder.Append(this.CuratedEvidence);
      builder.Append(", MorningHeadline = ");
      builder.Append(this.MorningHeadline);
      builder.Append(", MorningHeadlineLine = ");
      builder.Append(this.MorningHeadlineLine);
      return true;
    }

    // Token: 0x06000CF1 RID: 3313 RVA: 0x00380490 File Offset: 0x00380490
    [CompilerGenerated]
    public static bool operator !=(ProactiveJobStatus left, ProactiveJobStatus right)
    {
      return !(left == right);
    }

    // Token: 0x06000CF2 RID: 3314 RVA: 0x0038049C File Offset: 0x0038049C
    [CompilerGenerated]
    public static bool operator ==(ProactiveJobStatus left, ProactiveJobStatus right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000CF3 RID: 3315 RVA: 0x003804B0 File Offset: 0x003804B0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<JobId>k__BackingField)) * -1521134295 + EqualityComparer<ProactiveJobState>.Default.GetHashCode(this.<State>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ProactiveSuggestionCard>>.Default.GetHashCode(this.<Cards>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Error>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<BreakUntil>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CuratedEvidence>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<MorningHeadline>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<MorningHeadlineLine>k__BackingField);
    }

    // Token: 0x06000CF4 RID: 3316 RVA: 0x00380588 File Offset: 0x00380588
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ProactiveJobStatus);
    }

    // Token: 0x06000CF5 RID: 3317 RVA: 0x00380598 File Offset: 0x00380598
    [CompilerGenerated]
    public bool Equals(ProactiveJobStatus other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<JobId>k__BackingField, other.<JobId>k__BackingField) && EqualityComparer<ProactiveJobState>.Default.Equals(this.<State>k__BackingField, other.<State>k__BackingField) && EqualityComparer<IReadOnlyList<ProactiveSuggestionCard>>.Default.Equals(this.<Cards>k__BackingField, other.<Cards>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Error>k__BackingField, other.<Error>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<BreakUntil>k__BackingField, other.<BreakUntil>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CuratedEvidence>k__BackingField, other.<CuratedEvidence>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<MorningHeadline>k__BackingField, other.<MorningHeadline>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<MorningHeadlineLine>k__BackingField, other.<MorningHeadlineLine>k__BackingField));
    }

    // Token: 0x06000CF7 RID: 3319 RVA: 0x00380698 File Offset: 0x00380698
    [CompilerGenerated]
    private ProactiveJobStatus([Nullable(1)] ProactiveJobStatus original)
    {
      this.JobId = original.<JobId>k__BackingField;
      this.State = original.<State>k__BackingField;
      this.Cards = original.<Cards>k__BackingField;
      this.Error = original.<Error>k__BackingField;
      this.BreakUntil = original.<BreakUntil>k__BackingField;
      this.CuratedEvidence = original.<CuratedEvidence>k__BackingField;
      this.MorningHeadline = original.<MorningHeadline>k__BackingField;
      this.MorningHeadlineLine = original.<MorningHeadlineLine>k__BackingField;
    }

    // Token: 0x06000CF8 RID: 3320 RVA: 0x0038070C File Offset: 0x0038070C
    [NullableContext(1)]
    [CompilerGenerated]
    public void Deconstruct(out string JobId, out ProactiveJobState State, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<ProactiveSuggestionCard> Cards, [Nullable(2)] out string Error, out DateTimeOffset? BreakUntil)
    {
      JobId = this.JobId;
      State = this.State;
      Cards = this.Cards;
      Error = this.Error;
      BreakUntil = this.BreakUntil;
    }
  }
}
