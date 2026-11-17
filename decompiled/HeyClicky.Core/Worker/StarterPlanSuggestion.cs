using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000054 RID: 84
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class StarterPlanSuggestion : IEquatable<StarterPlanSuggestion>
  {
    // Token: 0x060003F1 RID: 1009 RVA: 0x00354DF4 File Offset: 0x00354DF4
    public StarterPlanSuggestion([Nullable(1)] string Title, [Nullable(1)] string Description, [Nullable(1)] string AgentPrompt, string Deliverable = null, string ClickySlug = null, string ClickyKey = null, StarterPlanRoutine Routine = null, int? SuggestionId = null)
    {
      this.Title = Title;
      this.Description = Description;
      this.AgentPrompt = AgentPrompt;
      this.Deliverable = Deliverable;
      this.ClickySlug = ClickySlug;
      this.ClickyKey = ClickyKey;
      this.Routine = Routine;
      this.SuggestionId = SuggestionId;
      base..ctor();
    }

    // Token: 0x170000DF RID: 223
    // (get) Token: 0x060003F2 RID: 1010 RVA: 0x00354E44 File Offset: 0x00354E44
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(StarterPlanSuggestion);
      }
    }

    // Token: 0x170000E0 RID: 224
    // (get) Token: 0x060003F3 RID: 1011 RVA: 0x00354E50 File Offset: 0x00354E50
    // (set) Token: 0x060003F4 RID: 1012 RVA: 0x00354E58 File Offset: 0x00354E58
    [Nullable(1)]
    public string Title
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170000E1 RID: 225
    // (get) Token: 0x060003F5 RID: 1013 RVA: 0x00354E64 File Offset: 0x00354E64
    // (set) Token: 0x060003F6 RID: 1014 RVA: 0x00354E6C File Offset: 0x00354E6C
    [Nullable(1)]
    public string Description
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170000E2 RID: 226
    // (get) Token: 0x060003F7 RID: 1015 RVA: 0x00354E78 File Offset: 0x00354E78
    // (set) Token: 0x060003F8 RID: 1016 RVA: 0x00354E80 File Offset: 0x00354E80
    [Nullable(1)]
    public string AgentPrompt
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170000E3 RID: 227
    // (get) Token: 0x060003F9 RID: 1017 RVA: 0x00354E8C File Offset: 0x00354E8C
    // (set) Token: 0x060003FA RID: 1018 RVA: 0x00354E94 File Offset: 0x00354E94
    public string Deliverable { get; set; }

    // Token: 0x170000E4 RID: 228
    // (get) Token: 0x060003FB RID: 1019 RVA: 0x00354EA0 File Offset: 0x00354EA0
    // (set) Token: 0x060003FC RID: 1020 RVA: 0x00354EA8 File Offset: 0x00354EA8
    public string ClickySlug { get; set; }

    // Token: 0x170000E5 RID: 229
    // (get) Token: 0x060003FD RID: 1021 RVA: 0x00354EB4 File Offset: 0x00354EB4
    // (set) Token: 0x060003FE RID: 1022 RVA: 0x00354EBC File Offset: 0x00354EBC
    public string ClickyKey { get; set; }

    // Token: 0x170000E6 RID: 230
    // (get) Token: 0x060003FF RID: 1023 RVA: 0x00354EC8 File Offset: 0x00354EC8
    // (set) Token: 0x06000400 RID: 1024 RVA: 0x00354ED0 File Offset: 0x00354ED0
    public StarterPlanRoutine Routine { get; set; }

    // Token: 0x170000E7 RID: 231
    // (get) Token: 0x06000401 RID: 1025 RVA: 0x00354EDC File Offset: 0x00354EDC
    // (set) Token: 0x06000402 RID: 1026 RVA: 0x00354EE4 File Offset: 0x00354EE4
    public int? SuggestionId { get; set; }

    // Token: 0x06000403 RID: 1027 RVA: 0x00354EF0 File Offset: 0x00354EF0
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("StarterPlanSuggestion");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000404 RID: 1028 RVA: 0x00354F3C File Offset: 0x00354F3C
    [NullableContext(1)]
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
      builder.Append(", Deliverable = ");
      builder.Append(this.Deliverable);
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      builder.Append(", ClickyKey = ");
      builder.Append(this.ClickyKey);
      builder.Append(", Routine = ");
      builder.Append(this.Routine);
      builder.Append(", SuggestionId = ");
      builder.Append(this.SuggestionId.ToString());
      return true;
    }

    // Token: 0x06000405 RID: 1029 RVA: 0x00355028 File Offset: 0x00355028
    [CompilerGenerated]
    public static bool operator !=(StarterPlanSuggestion left, StarterPlanSuggestion right)
    {
      return !(left == right);
    }

    // Token: 0x06000406 RID: 1030 RVA: 0x00355034 File Offset: 0x00355034
    [CompilerGenerated]
    public static bool operator ==(StarterPlanSuggestion left, StarterPlanSuggestion right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000407 RID: 1031 RVA: 0x00355048 File Offset: 0x00355048
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AgentPrompt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Deliverable>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickyKey>k__BackingField)) * -1521134295 + EqualityComparer<StarterPlanRoutine>.Default.GetHashCode(this.<Routine>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<SuggestionId>k__BackingField);
    }

    // Token: 0x06000408 RID: 1032 RVA: 0x00355120 File Offset: 0x00355120
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as StarterPlanSuggestion);
    }

    // Token: 0x06000409 RID: 1033 RVA: 0x00355130 File Offset: 0x00355130
    [CompilerGenerated]
    public bool Equals(StarterPlanSuggestion other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AgentPrompt>k__BackingField, other.<AgentPrompt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Deliverable>k__BackingField, other.<Deliverable>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickyKey>k__BackingField, other.<ClickyKey>k__BackingField) && EqualityComparer<StarterPlanRoutine>.Default.Equals(this.<Routine>k__BackingField, other.<Routine>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<SuggestionId>k__BackingField, other.<SuggestionId>k__BackingField));
    }

    // Token: 0x0600040B RID: 1035 RVA: 0x00355230 File Offset: 0x00355230
    [CompilerGenerated]
    private StarterPlanSuggestion([Nullable(1)] StarterPlanSuggestion original)
    {
      this.Title = original.<Title>k__BackingField;
      this.Description = original.<Description>k__BackingField;
      this.AgentPrompt = original.<AgentPrompt>k__BackingField;
      this.Deliverable = original.<Deliverable>k__BackingField;
      this.ClickySlug = original.<ClickySlug>k__BackingField;
      this.ClickyKey = original.<ClickyKey>k__BackingField;
      this.Routine = original.<Routine>k__BackingField;
      this.SuggestionId = original.<SuggestionId>k__BackingField;
    }

    // Token: 0x0600040C RID: 1036 RVA: 0x003552A4 File Offset: 0x003552A4
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string Title, [Nullable(1)] out string Description, [Nullable(1)] out string AgentPrompt, out string Deliverable, out string ClickySlug, out string ClickyKey, out StarterPlanRoutine Routine, out int? SuggestionId)
    {
      Title = this.Title;
      Description = this.Description;
      AgentPrompt = this.AgentPrompt;
      Deliverable = this.Deliverable;
      ClickySlug = this.ClickySlug;
      ClickyKey = this.ClickyKey;
      Routine = this.Routine;
      SuggestionId = this.SuggestionId;
    }
  }
}
