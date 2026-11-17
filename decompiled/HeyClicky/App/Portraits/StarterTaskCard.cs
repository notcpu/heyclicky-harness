using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Portraits
{
  // Token: 0x020000F8 RID: 248
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class StarterTaskCard : IEquatable<StarterTaskCard>
  {
    // Token: 0x06000CF2 RID: 3314 RVA: 0x0056406C File Offset: 0x0056406C
    public StarterTaskCard(string Title, string Description, string AgentPrompt, [Nullable(2)] string ClickySlug, [Nullable(2)] string RoutineTitle, int? RoutineEveryMinutes, int? SuggestionId, [Nullable(2)] string Deliverable)
    {
      this.Title = Title;
      this.Description = Description;
      this.AgentPrompt = AgentPrompt;
      this.ClickySlug = ClickySlug;
      this.RoutineTitle = RoutineTitle;
      this.RoutineEveryMinutes = RoutineEveryMinutes;
      this.SuggestionId = SuggestionId;
      this.Deliverable = Deliverable;
      base..ctor();
    }

    // Token: 0x1700024C RID: 588
    // (get) Token: 0x06000CF3 RID: 3315 RVA: 0x005640BC File Offset: 0x005640BC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(StarterTaskCard);
      }
    }

    // Token: 0x1700024D RID: 589
    // (get) Token: 0x06000CF4 RID: 3316 RVA: 0x005640C8 File Offset: 0x005640C8
    // (set) Token: 0x06000CF5 RID: 3317 RVA: 0x005640D0 File Offset: 0x005640D0
    public string Title { get; set; }

    // Token: 0x1700024E RID: 590
    // (get) Token: 0x06000CF6 RID: 3318 RVA: 0x005640DC File Offset: 0x005640DC
    // (set) Token: 0x06000CF7 RID: 3319 RVA: 0x005640E4 File Offset: 0x005640E4
    public string Description { get; set; }

    // Token: 0x1700024F RID: 591
    // (get) Token: 0x06000CF8 RID: 3320 RVA: 0x005640F0 File Offset: 0x005640F0
    // (set) Token: 0x06000CF9 RID: 3321 RVA: 0x005640F8 File Offset: 0x005640F8
    public string AgentPrompt { get; set; }

    // Token: 0x17000250 RID: 592
    // (get) Token: 0x06000CFA RID: 3322 RVA: 0x00564104 File Offset: 0x00564104
    // (set) Token: 0x06000CFB RID: 3323 RVA: 0x0056410C File Offset: 0x0056410C
    [Nullable(2)]
    public string ClickySlug
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000251 RID: 593
    // (get) Token: 0x06000CFC RID: 3324 RVA: 0x00564118 File Offset: 0x00564118
    // (set) Token: 0x06000CFD RID: 3325 RVA: 0x00564120 File Offset: 0x00564120
    [Nullable(2)]
    public string RoutineTitle
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000252 RID: 594
    // (get) Token: 0x06000CFE RID: 3326 RVA: 0x0056412C File Offset: 0x0056412C
    // (set) Token: 0x06000CFF RID: 3327 RVA: 0x00564134 File Offset: 0x00564134
    public int? RoutineEveryMinutes { get; set; }

    // Token: 0x17000253 RID: 595
    // (get) Token: 0x06000D00 RID: 3328 RVA: 0x00564140 File Offset: 0x00564140
    // (set) Token: 0x06000D01 RID: 3329 RVA: 0x00564148 File Offset: 0x00564148
    public int? SuggestionId { get; set; }

    // Token: 0x17000254 RID: 596
    // (get) Token: 0x06000D02 RID: 3330 RVA: 0x00564154 File Offset: 0x00564154
    // (set) Token: 0x06000D03 RID: 3331 RVA: 0x0056415C File Offset: 0x0056415C
    [Nullable(2)]
    public string Deliverable
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000255 RID: 597
    // (get) Token: 0x06000D04 RID: 3332 RVA: 0x00564168 File Offset: 0x00564168
    public bool IsRoutine
    {
      get
      {
        return this.RoutineEveryMinutes > 0;
      }
    }

    // Token: 0x06000D05 RID: 3333 RVA: 0x00564194 File Offset: 0x00564194
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("StarterTaskCard");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000D06 RID: 3334 RVA: 0x005641E0 File Offset: 0x005641E0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Title = ");
      builder.Append(this.Title);
      builder.Append(", Description = ");
      builder.Append(this.Description);
      builder.Append(", AgentPrompt = ");
      builder.Append(this.AgentPrompt);
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      builder.Append(", RoutineTitle = ");
      builder.Append(this.RoutineTitle);
      builder.Append(", RoutineEveryMinutes = ");
      builder.Append(this.RoutineEveryMinutes.ToString());
      builder.Append(", SuggestionId = ");
      builder.Append(this.SuggestionId.ToString());
      builder.Append(", Deliverable = ");
      builder.Append(this.Deliverable);
      builder.Append(", IsRoutine = ");
      builder.Append(this.IsRoutine.ToString());
      return true;
    }

    // Token: 0x06000D07 RID: 3335 RVA: 0x00564300 File Offset: 0x00564300
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(StarterTaskCard left, StarterTaskCard right)
    {
      return !(left == right);
    }

    // Token: 0x06000D08 RID: 3336 RVA: 0x0056430C File Offset: 0x0056430C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(StarterTaskCard left, StarterTaskCard right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000D09 RID: 3337 RVA: 0x00564320 File Offset: 0x00564320
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AgentPrompt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RoutineTitle>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<RoutineEveryMinutes>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<SuggestionId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Deliverable>k__BackingField);
    }

    // Token: 0x06000D0A RID: 3338 RVA: 0x005643F8 File Offset: 0x005643F8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as StarterTaskCard);
    }

    // Token: 0x06000D0B RID: 3339 RVA: 0x00564408 File Offset: 0x00564408
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(StarterTaskCard other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AgentPrompt>k__BackingField, other.<AgentPrompt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RoutineTitle>k__BackingField, other.<RoutineTitle>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<RoutineEveryMinutes>k__BackingField, other.<RoutineEveryMinutes>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<SuggestionId>k__BackingField, other.<SuggestionId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Deliverable>k__BackingField, other.<Deliverable>k__BackingField));
    }

    // Token: 0x06000D0D RID: 3341 RVA: 0x00564508 File Offset: 0x00564508
    [CompilerGenerated]
    private StarterTaskCard(StarterTaskCard original)
    {
      this.Title = original.<Title>k__BackingField;
      this.Description = original.<Description>k__BackingField;
      this.AgentPrompt = original.<AgentPrompt>k__BackingField;
      this.ClickySlug = original.<ClickySlug>k__BackingField;
      this.RoutineTitle = original.<RoutineTitle>k__BackingField;
      this.RoutineEveryMinutes = original.<RoutineEveryMinutes>k__BackingField;
      this.SuggestionId = original.<SuggestionId>k__BackingField;
      this.Deliverable = original.<Deliverable>k__BackingField;
    }

    // Token: 0x06000D0E RID: 3342 RVA: 0x0056457C File Offset: 0x0056457C
    [CompilerGenerated]
    public void Deconstruct(out string Title, out string Description, out string AgentPrompt, [Nullable(2)] out string ClickySlug, [Nullable(2)] out string RoutineTitle, out int? RoutineEveryMinutes, out int? SuggestionId, [Nullable(2)] out string Deliverable)
    {
      Title = this.Title;
      Description = this.Description;
      AgentPrompt = this.AgentPrompt;
      ClickySlug = this.ClickySlug;
      RoutineTitle = this.RoutineTitle;
      RoutineEveryMinutes = this.RoutineEveryMinutes;
      SuggestionId = this.SuggestionId;
      Deliverable = this.Deliverable;
    }
  }
}
