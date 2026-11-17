using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents
{
  // Token: 0x02000007 RID: 7
  [NullableContext(2)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class AgentLaunchRequest : IEquatable<AgentLaunchRequest>
  {
    // Token: 0x17000002 RID: 2
    // (get) Token: 0x06000017 RID: 23 RVA: 0x000D6350 File Offset: 0x000D6350
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(AgentLaunchRequest);
      }
    }

    // Token: 0x17000003 RID: 3
    // (get) Token: 0x06000018 RID: 24 RVA: 0x000D635C File Offset: 0x000D635C
    // (set) Token: 0x06000019 RID: 25 RVA: 0x000D6364 File Offset: 0x000D6364
    [Nullable(1)]
    [RequiredMember]
    public string Prompt
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000004 RID: 4
    // (get) Token: 0x0600001A RID: 26 RVA: 0x000D6370 File Offset: 0x000D6370
    // (set) Token: 0x0600001B RID: 27 RVA: 0x000D6378 File Offset: 0x000D6378
    public string DisplayPrompt { get; set; }

    // Token: 0x17000005 RID: 5
    // (get) Token: 0x0600001C RID: 28 RVA: 0x000D6384 File Offset: 0x000D6384
    // (set) Token: 0x0600001D RID: 29 RVA: 0x000D638C File Offset: 0x000D638C
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<byte[]> ImagesJpeg
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000006 RID: 6
    // (get) Token: 0x0600001E RID: 30 RVA: 0x000D6398 File Offset: 0x000D6398
    // (set) Token: 0x0600001F RID: 31 RVA: 0x000D63A0 File Offset: 0x000D63A0
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> AttachedFilePaths
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000007 RID: 7
    // (get) Token: 0x06000020 RID: 32 RVA: 0x000D63AC File Offset: 0x000D63AC
    // (set) Token: 0x06000021 RID: 33 RVA: 0x000D63B4 File Offset: 0x000D63B4
    public string ClickySlug { get; set; }

    // Token: 0x17000008 RID: 8
    // (get) Token: 0x06000022 RID: 34 RVA: 0x000D63C0 File Offset: 0x000D63C0
    // (set) Token: 0x06000023 RID: 35 RVA: 0x000D63C8 File Offset: 0x000D63C8
    public string JobId { get; set; }

    // Token: 0x17000009 RID: 9
    // (get) Token: 0x06000024 RID: 36 RVA: 0x000D63D4 File Offset: 0x000D63D4
    // (set) Token: 0x06000025 RID: 37 RVA: 0x000D63DC File Offset: 0x000D63DC
    public string TargetThreadId { get; set; }

    // Token: 0x1700000A RID: 10
    // (get) Token: 0x06000026 RID: 38 RVA: 0x000D63E8 File Offset: 0x000D63E8
    // (set) Token: 0x06000027 RID: 39 RVA: 0x000D63F0 File Offset: 0x000D63F0
    public bool IsHudFollowUp { get; set; }

    // Token: 0x1700000B RID: 11
    // (get) Token: 0x06000028 RID: 40 RVA: 0x000D63FC File Offset: 0x000D63FC
    // (set) Token: 0x06000029 RID: 41 RVA: 0x000D6404 File Offset: 0x000D6404
    public bool StartsNewJobConversation { get; set; }

    // Token: 0x1700000C RID: 12
    // (get) Token: 0x0600002A RID: 42 RVA: 0x000D6410 File Offset: 0x000D6410
    // (set) Token: 0x0600002B RID: 43 RVA: 0x000D6418 File Offset: 0x000D6418
    public string NewClickyName { get; set; }

    // Token: 0x1700000D RID: 13
    // (get) Token: 0x0600002C RID: 44 RVA: 0x000D6424 File Offset: 0x000D6424
    // (set) Token: 0x0600002D RID: 45 RVA: 0x000D642C File Offset: 0x000D642C
    [Nullable(1)]
    public string NewClickyRole
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x1700000E RID: 14
    // (get) Token: 0x0600002E RID: 46 RVA: 0x000D6438 File Offset: 0x000D6438
    // (set) Token: 0x0600002F RID: 47 RVA: 0x000D6440 File Offset: 0x000D6440
    public string NewClickyOneLiner { get; set; }

    // Token: 0x1700000F RID: 15
    // (get) Token: 0x06000030 RID: 48 RVA: 0x000D644C File Offset: 0x000D644C
    // (set) Token: 0x06000031 RID: 49 RVA: 0x000D6454 File Offset: 0x000D6454
    public string LaunchContext { get; set; }

    // Token: 0x17000010 RID: 16
    // (get) Token: 0x06000032 RID: 50 RVA: 0x000D6460 File Offset: 0x000D6460
    // (set) Token: 0x06000033 RID: 51 RVA: 0x000D6468 File Offset: 0x000D6468
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> BuildsOnJobIds
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000011 RID: 17
    // (get) Token: 0x06000034 RID: 52 RVA: 0x000D6474 File Offset: 0x000D6474
    // (set) Token: 0x06000035 RID: 53 RVA: 0x000D647C File Offset: 0x000D647C
    [Nullable(1)]
    public string LaunchSource
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000012 RID: 18
    // (get) Token: 0x06000036 RID: 54 RVA: 0x000D6488 File Offset: 0x000D6488
    // (set) Token: 0x06000037 RID: 55 RVA: 0x000D6490 File Offset: 0x000D6490
    [Nullable(1)]
    public string UsageContext
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000013 RID: 19
    // (get) Token: 0x06000038 RID: 56 RVA: 0x000D649C File Offset: 0x000D649C
    // (set) Token: 0x06000039 RID: 57 RVA: 0x000D64A4 File Offset: 0x000D64A4
    public bool IsGeneratedTaskBrief { get; set; }

    // Token: 0x17000014 RID: 20
    // (get) Token: 0x0600003A RID: 58 RVA: 0x000D64B0 File Offset: 0x000D64B0
    // (set) Token: 0x0600003B RID: 59 RVA: 0x000D64B8 File Offset: 0x000D64B8
    [TupleElementNames(new string[] { "User", "Assistant" })]
    [Nullable(new byte[] { 2, 0, 1, 1 })]
    public IReadOnlyList<ValueTuple<string, string>> PriorCompanionTurns
    {
      [return: TupleElementNames(new string[] { "User", "Assistant" })]
      [return: Nullable(new byte[] { 2, 0, 1, 1 })]
      get;
      [param: TupleElementNames(new string[] { "User", "Assistant" })]
      [param: Nullable(new byte[] { 2, 0, 1, 1 })]
      set;
    }

    // Token: 0x17000015 RID: 21
    // (get) Token: 0x0600003C RID: 60 RVA: 0x000D64C4 File Offset: 0x000D64C4
    // (set) Token: 0x0600003D RID: 61 RVA: 0x000D64CC File Offset: 0x000D64CC
    public bool AutoApproveExtraUsage { get; set; }

    // Token: 0x17000016 RID: 22
    // (get) Token: 0x0600003E RID: 62 RVA: 0x000D64D8 File Offset: 0x000D64D8
    // (set) Token: 0x0600003F RID: 63 RVA: 0x000D64E0 File Offset: 0x000D64E0
    public long? ProactiveSuggestionId { get; set; }

    // Token: 0x06000040 RID: 64 RVA: 0x000D64EC File Offset: 0x000D64EC
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentLaunchRequest");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000041 RID: 65 RVA: 0x000D6538 File Offset: 0x000D6538
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Prompt = ");
      builder.Append(this.Prompt);
      builder.Append(", DisplayPrompt = ");
      builder.Append(this.DisplayPrompt);
      builder.Append(", ImagesJpeg = ");
      builder.Append(this.ImagesJpeg);
      builder.Append(", AttachedFilePaths = ");
      builder.Append(this.AttachedFilePaths);
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      builder.Append(", JobId = ");
      builder.Append(this.JobId);
      builder.Append(", TargetThreadId = ");
      builder.Append(this.TargetThreadId);
      builder.Append(", IsHudFollowUp = ");
      builder.Append(this.IsHudFollowUp.ToString());
      builder.Append(", StartsNewJobConversation = ");
      builder.Append(this.StartsNewJobConversation.ToString());
      builder.Append(", NewClickyName = ");
      builder.Append(this.NewClickyName);
      builder.Append(", NewClickyRole = ");
      builder.Append(this.NewClickyRole);
      builder.Append(", NewClickyOneLiner = ");
      builder.Append(this.NewClickyOneLiner);
      builder.Append(", LaunchContext = ");
      builder.Append(this.LaunchContext);
      builder.Append(", BuildsOnJobIds = ");
      builder.Append(this.BuildsOnJobIds);
      builder.Append(", LaunchSource = ");
      builder.Append(this.LaunchSource);
      builder.Append(", UsageContext = ");
      builder.Append(this.UsageContext);
      builder.Append(", IsGeneratedTaskBrief = ");
      builder.Append(this.IsGeneratedTaskBrief.ToString());
      builder.Append(", PriorCompanionTurns = ");
      builder.Append(this.PriorCompanionTurns);
      builder.Append(", AutoApproveExtraUsage = ");
      builder.Append(this.AutoApproveExtraUsage.ToString());
      builder.Append(", ProactiveSuggestionId = ");
      builder.Append(this.ProactiveSuggestionId.ToString());
      return true;
    }

    // Token: 0x06000042 RID: 66 RVA: 0x000D6788 File Offset: 0x000D6788
    [CompilerGenerated]
    public static bool operator !=(AgentLaunchRequest left, AgentLaunchRequest right)
    {
      return !(left == right);
    }

    // Token: 0x06000043 RID: 67 RVA: 0x000D6794 File Offset: 0x000D6794
    [CompilerGenerated]
    public static bool operator ==(AgentLaunchRequest left, AgentLaunchRequest right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000044 RID: 68 RVA: 0x000D67A8 File Offset: 0x000D67A8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Prompt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayPrompt>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<byte[]>>.Default.GetHashCode(this.<ImagesJpeg>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<AttachedFilePaths>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<JobId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TargetThreadId>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsHudFollowUp>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<StartsNewJobConversation>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NewClickyName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NewClickyRole>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NewClickyOneLiner>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LaunchContext>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<BuildsOnJobIds>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LaunchSource>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UsageContext>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsGeneratedTaskBrief>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ValueTuple<string, string>>>.Default.GetHashCode(this.<PriorCompanionTurns>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<AutoApproveExtraUsage>k__BackingField)) * -1521134295 + EqualityComparer<long?>.Default.GetHashCode(this.<ProactiveSuggestionId>k__BackingField);
    }

    // Token: 0x06000045 RID: 69 RVA: 0x000D6994 File Offset: 0x000D6994
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentLaunchRequest);
    }

    // Token: 0x06000046 RID: 70 RVA: 0x000D69A4 File Offset: 0x000D69A4
    [CompilerGenerated]
    public bool Equals(AgentLaunchRequest other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Prompt>k__BackingField, other.<Prompt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayPrompt>k__BackingField, other.<DisplayPrompt>k__BackingField) && EqualityComparer<IReadOnlyList<byte[]>>.Default.Equals(this.<ImagesJpeg>k__BackingField, other.<ImagesJpeg>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<AttachedFilePaths>k__BackingField, other.<AttachedFilePaths>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<JobId>k__BackingField, other.<JobId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TargetThreadId>k__BackingField, other.<TargetThreadId>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsHudFollowUp>k__BackingField, other.<IsHudFollowUp>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<StartsNewJobConversation>k__BackingField, other.<StartsNewJobConversation>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NewClickyName>k__BackingField, other.<NewClickyName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NewClickyRole>k__BackingField, other.<NewClickyRole>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NewClickyOneLiner>k__BackingField, other.<NewClickyOneLiner>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LaunchContext>k__BackingField, other.<LaunchContext>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<BuildsOnJobIds>k__BackingField, other.<BuildsOnJobIds>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LaunchSource>k__BackingField, other.<LaunchSource>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UsageContext>k__BackingField, other.<UsageContext>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsGeneratedTaskBrief>k__BackingField, other.<IsGeneratedTaskBrief>k__BackingField) && EqualityComparer<IReadOnlyList<ValueTuple<string, string>>>.Default.Equals(this.<PriorCompanionTurns>k__BackingField, other.<PriorCompanionTurns>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<AutoApproveExtraUsage>k__BackingField, other.<AutoApproveExtraUsage>k__BackingField) && EqualityComparer<long?>.Default.Equals(this.<ProactiveSuggestionId>k__BackingField, other.<ProactiveSuggestionId>k__BackingField));
    }

    // Token: 0x06000048 RID: 72 RVA: 0x000D6BE8 File Offset: 0x000D6BE8
    [CompilerGenerated]
    [SetsRequiredMembers]
    private AgentLaunchRequest([Nullable(1)] AgentLaunchRequest original)
    {
      this.Prompt = original.<Prompt>k__BackingField;
      this.DisplayPrompt = original.<DisplayPrompt>k__BackingField;
      this.ImagesJpeg = original.<ImagesJpeg>k__BackingField;
      this.AttachedFilePaths = original.<AttachedFilePaths>k__BackingField;
      this.ClickySlug = original.<ClickySlug>k__BackingField;
      this.JobId = original.<JobId>k__BackingField;
      this.TargetThreadId = original.<TargetThreadId>k__BackingField;
      this.IsHudFollowUp = original.<IsHudFollowUp>k__BackingField;
      this.StartsNewJobConversation = original.<StartsNewJobConversation>k__BackingField;
      this.NewClickyName = original.<NewClickyName>k__BackingField;
      this.NewClickyRole = original.<NewClickyRole>k__BackingField;
      this.NewClickyOneLiner = original.<NewClickyOneLiner>k__BackingField;
      this.LaunchContext = original.<LaunchContext>k__BackingField;
      this.BuildsOnJobIds = original.<BuildsOnJobIds>k__BackingField;
      this.LaunchSource = original.<LaunchSource>k__BackingField;
      this.UsageContext = original.<UsageContext>k__BackingField;
      this.IsGeneratedTaskBrief = original.<IsGeneratedTaskBrief>k__BackingField;
      this.PriorCompanionTurns = original.<PriorCompanionTurns>k__BackingField;
      this.AutoApproveExtraUsage = original.<AutoApproveExtraUsage>k__BackingField;
      this.ProactiveSuggestionId = original.<ProactiveSuggestionId>k__BackingField;
    }

    // Token: 0x06000049 RID: 73 RVA: 0x000D6CEC File Offset: 0x000D6CEC
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public AgentLaunchRequest()
    {
      this.NewClickyRole = "Helper";
      this.LaunchSource = "voice";
      this.UsageContext = "companion";
      base..ctor();
    }
  }
}
