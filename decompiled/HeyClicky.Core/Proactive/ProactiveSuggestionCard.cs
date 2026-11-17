using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000CB RID: 203
  [NullableContext(2)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class ProactiveSuggestionCard : IEquatable<ProactiveSuggestionCard>
  {
    // Token: 0x170002E4 RID: 740
    // (get) Token: 0x06000D2F RID: 3375 RVA: 0x00381788 File Offset: 0x00381788
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ProactiveSuggestionCard);
      }
    }

    // Token: 0x170002E5 RID: 741
    // (get) Token: 0x06000D30 RID: 3376 RVA: 0x00381794 File Offset: 0x00381794
    // (set) Token: 0x06000D31 RID: 3377 RVA: 0x0038179C File Offset: 0x0038179C
    public Guid Id { get; set; }

    // Token: 0x170002E6 RID: 742
    // (get) Token: 0x06000D32 RID: 3378 RVA: 0x003817A8 File Offset: 0x003817A8
    // (set) Token: 0x06000D33 RID: 3379 RVA: 0x003817B0 File Offset: 0x003817B0
    [Nullable(1)]
    [RequiredMember]
    public string Title
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170002E7 RID: 743
    // (get) Token: 0x06000D34 RID: 3380 RVA: 0x003817BC File Offset: 0x003817BC
    // (set) Token: 0x06000D35 RID: 3381 RVA: 0x003817C4 File Offset: 0x003817C4
    [Nullable(1)]
    [RequiredMember]
    public string Description
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170002E8 RID: 744
    // (get) Token: 0x06000D36 RID: 3382 RVA: 0x003817D0 File Offset: 0x003817D0
    // (set) Token: 0x06000D37 RID: 3383 RVA: 0x003817D8 File Offset: 0x003817D8
    [Nullable(1)]
    [RequiredMember]
    public string AgentPrompt
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170002E9 RID: 745
    // (get) Token: 0x06000D38 RID: 3384 RVA: 0x003817E4 File Offset: 0x003817E4
    // (set) Token: 0x06000D39 RID: 3385 RVA: 0x003817EC File Offset: 0x003817EC
    public ProactiveCheckReason CheckReason { get; set; }

    // Token: 0x170002EA RID: 746
    // (get) Token: 0x06000D3A RID: 3386 RVA: 0x003817F8 File Offset: 0x003817F8
    // (set) Token: 0x06000D3B RID: 3387 RVA: 0x00381800 File Offset: 0x00381800
    public int? ServerSuggestionId { get; set; }

    // Token: 0x170002EB RID: 747
    // (get) Token: 0x06000D3C RID: 3388 RVA: 0x0038180C File Offset: 0x0038180C
    // (set) Token: 0x06000D3D RID: 3389 RVA: 0x00381814 File Offset: 0x00381814
    public string ClickySlug { get; set; }

    // Token: 0x170002EC RID: 748
    // (get) Token: 0x06000D3E RID: 3390 RVA: 0x00381820 File Offset: 0x00381820
    // (set) Token: 0x06000D3F RID: 3391 RVA: 0x00381828 File Offset: 0x00381828
    public ProactiveNewClicky NewClicky { get; set; }

    // Token: 0x170002ED RID: 749
    // (get) Token: 0x06000D40 RID: 3392 RVA: 0x00381834 File Offset: 0x00381834
    // (set) Token: 0x06000D41 RID: 3393 RVA: 0x0038183C File Offset: 0x0038183C
    public ProactiveRoutine Routine { get; set; }

    // Token: 0x170002EE RID: 750
    // (get) Token: 0x06000D42 RID: 3394 RVA: 0x00381848 File Offset: 0x00381848
    // (set) Token: 0x06000D43 RID: 3395 RVA: 0x00381850 File Offset: 0x00381850
    public string IntegrationName { get; set; }

    // Token: 0x170002EF RID: 751
    // (get) Token: 0x06000D44 RID: 3396 RVA: 0x0038185C File Offset: 0x0038185C
    // (set) Token: 0x06000D45 RID: 3397 RVA: 0x00381864 File Offset: 0x00381864
    public DateTimeOffset CreatedAt { get; set; }

    // Token: 0x170002F0 RID: 752
    // (get) Token: 0x06000D46 RID: 3398 RVA: 0x00381870 File Offset: 0x00381870
    // (set) Token: 0x06000D47 RID: 3399 RVA: 0x00381878 File Offset: 0x00381878
    public DateTimeOffset? ViewedAt { get; set; }

    // Token: 0x170002F1 RID: 753
    // (get) Token: 0x06000D48 RID: 3400 RVA: 0x00381884 File Offset: 0x00381884
    public DateTimeOffset? ExpiresAt
    {
      get
      {
        ProactiveCheckReason checkReason = this.CheckReason;
        DateTimeOffset? dateTimeOffset;
        if (checkReason != ProactiveCheckReason.Debug)
        {
          if (checkReason == ProactiveCheckReason.Onboarding)
          {
            dateTimeOffset = null;
          }
          else
          {
            dateTimeOffset = new DateTimeOffset?(this.CreatedAt + ProactiveSuggestionCard.DailyInterval);
          }
        }
        else
        {
          dateTimeOffset = new DateTimeOffset?(this.CreatedAt + TimeSpan.FromMinutes(5L));
        }
        return dateTimeOffset;
      }
    }

    // Token: 0x06000D49 RID: 3401 RVA: 0x003818E0 File Offset: 0x003818E0
    public bool IsExpired(DateTimeOffset now)
    {
      DateTimeOffset? expiresAt = this.ExpiresAt;
      if (expiresAt != null)
      {
        DateTimeOffset valueOrDefault = expiresAt.GetValueOrDefault();
        return now >= valueOrDefault;
      }
      return false;
    }

    // Token: 0x06000D4A RID: 3402 RVA: 0x00381910 File Offset: 0x00381910
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ProactiveSuggestionCard");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000D4B RID: 3403 RVA: 0x0038195C File Offset: 0x0038195C
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id.ToString());
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Description = ");
      builder.Append(this.Description);
      builder.Append(", AgentPrompt = ");
      builder.Append(this.AgentPrompt);
      builder.Append(", CheckReason = ");
      builder.Append(this.CheckReason.ToString());
      builder.Append(", ServerSuggestionId = ");
      builder.Append(this.ServerSuggestionId.ToString());
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      builder.Append(", NewClicky = ");
      builder.Append(this.NewClicky);
      builder.Append(", Routine = ");
      builder.Append(this.Routine);
      builder.Append(", IntegrationName = ");
      builder.Append(this.IntegrationName);
      builder.Append(", CreatedAt = ");
      builder.Append(this.CreatedAt.ToString());
      builder.Append(", ViewedAt = ");
      builder.Append(this.ViewedAt.ToString());
      builder.Append(", ExpiresAt = ");
      builder.Append(this.ExpiresAt.ToString());
      return true;
    }

    // Token: 0x06000D4C RID: 3404 RVA: 0x00381B0C File Offset: 0x00381B0C
    [CompilerGenerated]
    public static bool operator !=(ProactiveSuggestionCard left, ProactiveSuggestionCard right)
    {
      return !(left == right);
    }

    // Token: 0x06000D4D RID: 3405 RVA: 0x00381B18 File Offset: 0x00381B18
    [CompilerGenerated]
    public static bool operator ==(ProactiveSuggestionCard left, ProactiveSuggestionCard right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000D4E RID: 3406 RVA: 0x00381B2C File Offset: 0x00381B2C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AgentPrompt>k__BackingField)) * -1521134295 + EqualityComparer<ProactiveCheckReason>.Default.GetHashCode(this.<CheckReason>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<ServerSuggestionId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<ProactiveNewClicky>.Default.GetHashCode(this.<NewClicky>k__BackingField)) * -1521134295 + EqualityComparer<ProactiveRoutine>.Default.GetHashCode(this.<Routine>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<IntegrationName>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<CreatedAt>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<ViewedAt>k__BackingField);
    }

    // Token: 0x06000D4F RID: 3407 RVA: 0x00381C60 File Offset: 0x00381C60
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ProactiveSuggestionCard);
    }

    // Token: 0x06000D50 RID: 3408 RVA: 0x00381C70 File Offset: 0x00381C70
    [CompilerGenerated]
    public bool Equals(ProactiveSuggestionCard other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Guid>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AgentPrompt>k__BackingField, other.<AgentPrompt>k__BackingField) && EqualityComparer<ProactiveCheckReason>.Default.Equals(this.<CheckReason>k__BackingField, other.<CheckReason>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<ServerSuggestionId>k__BackingField, other.<ServerSuggestionId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<ProactiveNewClicky>.Default.Equals(this.<NewClicky>k__BackingField, other.<NewClicky>k__BackingField) && EqualityComparer<ProactiveRoutine>.Default.Equals(this.<Routine>k__BackingField, other.<Routine>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<IntegrationName>k__BackingField, other.<IntegrationName>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<CreatedAt>k__BackingField, other.<CreatedAt>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<ViewedAt>k__BackingField, other.<ViewedAt>k__BackingField));
    }

    // Token: 0x06000D52 RID: 3410 RVA: 0x00381DDC File Offset: 0x00381DDC
    [CompilerGenerated]
    [SetsRequiredMembers]
    private ProactiveSuggestionCard([Nullable(1)] ProactiveSuggestionCard original)
    {
      this.Id = original.<Id>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.Description = original.<Description>k__BackingField;
      this.AgentPrompt = original.<AgentPrompt>k__BackingField;
      this.CheckReason = original.<CheckReason>k__BackingField;
      this.ServerSuggestionId = original.<ServerSuggestionId>k__BackingField;
      this.ClickySlug = original.<ClickySlug>k__BackingField;
      this.NewClicky = original.<NewClicky>k__BackingField;
      this.Routine = original.<Routine>k__BackingField;
      this.IntegrationName = original.<IntegrationName>k__BackingField;
      this.CreatedAt = original.<CreatedAt>k__BackingField;
      this.ViewedAt = original.<ViewedAt>k__BackingField;
    }

    // Token: 0x06000D53 RID: 3411 RVA: 0x00381E80 File Offset: 0x00381E80
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public ProactiveSuggestionCard()
    {
      this.Id = Guid.NewGuid();
      this.CreatedAt = DateTimeOffset.Now;
      base..ctor();
    }

    // Token: 0x0400059F RID: 1439
    public static readonly TimeSpan DailyInterval = TimeSpan.FromHours(24);
  }
}
