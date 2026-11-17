using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000055 RID: 85
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class StarterPlanResponse : IEquatable<StarterPlanResponse>
  {
    // Token: 0x0600040D RID: 1037 RVA: 0x003552FC File Offset: 0x003552FC
    public StarterPlanResponse([Nullable(new byte[] { 2, 1 })] IReadOnlyList<StarterPlanClicky> Clickys, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<StarterPlanSuggestion> SuggestedAgents, string AgentFamiliarity, string GoalLine)
    {
      this.Clickys = Clickys;
      this.SuggestedAgents = SuggestedAgents;
      this.AgentFamiliarity = AgentFamiliarity;
      this.GoalLine = GoalLine;
      base..ctor();
    }

    // Token: 0x170000E8 RID: 232
    // (get) Token: 0x0600040E RID: 1038 RVA: 0x00355324 File Offset: 0x00355324
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(StarterPlanResponse);
      }
    }

    // Token: 0x170000E9 RID: 233
    // (get) Token: 0x0600040F RID: 1039 RVA: 0x00355330 File Offset: 0x00355330
    // (set) Token: 0x06000410 RID: 1040 RVA: 0x00355338 File Offset: 0x00355338
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<StarterPlanClicky> Clickys
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170000EA RID: 234
    // (get) Token: 0x06000411 RID: 1041 RVA: 0x00355344 File Offset: 0x00355344
    // (set) Token: 0x06000412 RID: 1042 RVA: 0x0035534C File Offset: 0x0035534C
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<StarterPlanSuggestion> SuggestedAgents
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170000EB RID: 235
    // (get) Token: 0x06000413 RID: 1043 RVA: 0x00355358 File Offset: 0x00355358
    // (set) Token: 0x06000414 RID: 1044 RVA: 0x00355360 File Offset: 0x00355360
    public string AgentFamiliarity { get; set; }

    // Token: 0x170000EC RID: 236
    // (get) Token: 0x06000415 RID: 1045 RVA: 0x0035536C File Offset: 0x0035536C
    // (set) Token: 0x06000416 RID: 1046 RVA: 0x00355374 File Offset: 0x00355374
    public string GoalLine { get; set; }

    // Token: 0x06000417 RID: 1047 RVA: 0x00355380 File Offset: 0x00355380
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("StarterPlanResponse");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000418 RID: 1048 RVA: 0x003553CC File Offset: 0x003553CC
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Clickys = ");
      builder.Append(this.Clickys);
      builder.Append(", SuggestedAgents = ");
      builder.Append(this.SuggestedAgents);
      builder.Append(", AgentFamiliarity = ");
      builder.Append(this.AgentFamiliarity);
      builder.Append(", GoalLine = ");
      builder.Append(this.GoalLine);
      return true;
    }

    // Token: 0x06000419 RID: 1049 RVA: 0x00355444 File Offset: 0x00355444
    [CompilerGenerated]
    public static bool operator !=(StarterPlanResponse left, StarterPlanResponse right)
    {
      return !(left == right);
    }

    // Token: 0x0600041A RID: 1050 RVA: 0x00355450 File Offset: 0x00355450
    [CompilerGenerated]
    public static bool operator ==(StarterPlanResponse left, StarterPlanResponse right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600041B RID: 1051 RVA: 0x00355464 File Offset: 0x00355464
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<StarterPlanClicky>>.Default.GetHashCode(this.<Clickys>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<StarterPlanSuggestion>>.Default.GetHashCode(this.<SuggestedAgents>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AgentFamiliarity>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GoalLine>k__BackingField);
    }

    // Token: 0x0600041C RID: 1052 RVA: 0x003554E0 File Offset: 0x003554E0
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as StarterPlanResponse);
    }

    // Token: 0x0600041D RID: 1053 RVA: 0x003554F0 File Offset: 0x003554F0
    [CompilerGenerated]
    public bool Equals(StarterPlanResponse other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<StarterPlanClicky>>.Default.Equals(this.<Clickys>k__BackingField, other.<Clickys>k__BackingField) && EqualityComparer<IReadOnlyList<StarterPlanSuggestion>>.Default.Equals(this.<SuggestedAgents>k__BackingField, other.<SuggestedAgents>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AgentFamiliarity>k__BackingField, other.<AgentFamiliarity>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GoalLine>k__BackingField, other.<GoalLine>k__BackingField));
    }

    // Token: 0x0600041F RID: 1055 RVA: 0x00355584 File Offset: 0x00355584
    [CompilerGenerated]
    private StarterPlanResponse([Nullable(1)] StarterPlanResponse original)
    {
      this.Clickys = original.<Clickys>k__BackingField;
      this.SuggestedAgents = original.<SuggestedAgents>k__BackingField;
      this.AgentFamiliarity = original.<AgentFamiliarity>k__BackingField;
      this.GoalLine = original.<GoalLine>k__BackingField;
    }

    // Token: 0x06000420 RID: 1056 RVA: 0x003555BC File Offset: 0x003555BC
    [CompilerGenerated]
    public void Deconstruct([Nullable(new byte[] { 2, 1 })] out IReadOnlyList<StarterPlanClicky> Clickys, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<StarterPlanSuggestion> SuggestedAgents, out string AgentFamiliarity, out string GoalLine)
    {
      Clickys = this.Clickys;
      SuggestedAgents = this.SuggestedAgents;
      AgentFamiliarity = this.AgentFamiliarity;
      GoalLine = this.GoalLine;
    }
  }
}
