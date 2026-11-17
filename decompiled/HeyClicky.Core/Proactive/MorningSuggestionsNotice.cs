using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000BD RID: 189
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MorningSuggestionsNotice : IEquatable<MorningSuggestionsNotice>
  {
    // Token: 0x06000C82 RID: 3202 RVA: 0x0037EE8C File Offset: 0x0037EE8C
    public MorningSuggestionsNotice(Guid Id, IReadOnlySet<Guid> CardIds, DateTimeOffset CreatedAt, DateTimeOffset PresentNotBefore, int? PreviewCardCount = null)
    {
      this.Id = Id;
      this.CardIds = CardIds;
      this.CreatedAt = CreatedAt;
      this.PresentNotBefore = PresentNotBefore;
      this.PreviewCardCount = PreviewCardCount;
      base..ctor();
    }

    // Token: 0x170002B9 RID: 697
    // (get) Token: 0x06000C83 RID: 3203 RVA: 0x0037EEBC File Offset: 0x0037EEBC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(MorningSuggestionsNotice);
      }
    }

    // Token: 0x170002BA RID: 698
    // (get) Token: 0x06000C84 RID: 3204 RVA: 0x0037EEC8 File Offset: 0x0037EEC8
    // (set) Token: 0x06000C85 RID: 3205 RVA: 0x0037EED0 File Offset: 0x0037EED0
    public Guid Id { get; set; }

    // Token: 0x170002BB RID: 699
    // (get) Token: 0x06000C86 RID: 3206 RVA: 0x0037EEDC File Offset: 0x0037EEDC
    // (set) Token: 0x06000C87 RID: 3207 RVA: 0x0037EEE4 File Offset: 0x0037EEE4
    public IReadOnlySet<Guid> CardIds { get; set; }

    // Token: 0x170002BC RID: 700
    // (get) Token: 0x06000C88 RID: 3208 RVA: 0x0037EEF0 File Offset: 0x0037EEF0
    // (set) Token: 0x06000C89 RID: 3209 RVA: 0x0037EEF8 File Offset: 0x0037EEF8
    public DateTimeOffset CreatedAt { get; set; }

    // Token: 0x170002BD RID: 701
    // (get) Token: 0x06000C8A RID: 3210 RVA: 0x0037EF04 File Offset: 0x0037EF04
    // (set) Token: 0x06000C8B RID: 3211 RVA: 0x0037EF0C File Offset: 0x0037EF0C
    public DateTimeOffset PresentNotBefore { get; set; }

    // Token: 0x170002BE RID: 702
    // (get) Token: 0x06000C8C RID: 3212 RVA: 0x0037EF18 File Offset: 0x0037EF18
    // (set) Token: 0x06000C8D RID: 3213 RVA: 0x0037EF20 File Offset: 0x0037EF20
    public int? PreviewCardCount { get; set; }

    // Token: 0x170002BF RID: 703
    // (get) Token: 0x06000C8E RID: 3214 RVA: 0x0037EF2C File Offset: 0x0037EF2C
    public bool IsDebugPreview
    {
      get
      {
        return this.PreviewCardCount != null;
      }
    }

    // Token: 0x06000C8F RID: 3215 RVA: 0x0037EF50 File Offset: 0x0037EF50
    public int RemainingCardCount(IReadOnlyList<ProactiveSuggestionCard> cards, DateTimeOffset now)
    {
      int? previewCardCount = this.PreviewCardCount;
      if (previewCardCount != null)
      {
        int valueOrDefault = previewCardCount.GetValueOrDefault();
        if (this.CardIds.Count == 0)
        {
          return valueOrDefault;
        }
      }
      return cards.Count<ProactiveSuggestionCard>((ProactiveSuggestionCard c) => this.CardIds.Contains(c.Id) && !c.IsExpired(now));
    }

    // Token: 0x06000C90 RID: 3216 RVA: 0x0037EFAC File Offset: 0x0037EFAC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("MorningSuggestionsNotice");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000C91 RID: 3217 RVA: 0x0037EFF8 File Offset: 0x0037EFF8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id.ToString());
      builder.Append(", CardIds = ");
      builder.Append(this.CardIds);
      builder.Append(", CreatedAt = ");
      builder.Append(this.CreatedAt.ToString());
      builder.Append(", PresentNotBefore = ");
      builder.Append(this.PresentNotBefore.ToString());
      builder.Append(", PreviewCardCount = ");
      builder.Append(this.PreviewCardCount.ToString());
      builder.Append(", IsDebugPreview = ");
      builder.Append(this.IsDebugPreview.ToString());
      return true;
    }

    // Token: 0x06000C92 RID: 3218 RVA: 0x0037F0E8 File Offset: 0x0037F0E8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(MorningSuggestionsNotice left, MorningSuggestionsNotice right)
    {
      return !(left == right);
    }

    // Token: 0x06000C93 RID: 3219 RVA: 0x0037F0F4 File Offset: 0x0037F0F4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(MorningSuggestionsNotice left, MorningSuggestionsNotice right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000C94 RID: 3220 RVA: 0x0037F108 File Offset: 0x0037F108
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlySet<Guid>>.Default.GetHashCode(this.<CardIds>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<CreatedAt>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<PresentNotBefore>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<PreviewCardCount>k__BackingField);
    }

    // Token: 0x06000C95 RID: 3221 RVA: 0x0037F198 File Offset: 0x0037F198
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as MorningSuggestionsNotice);
    }

    // Token: 0x06000C96 RID: 3222 RVA: 0x0037F1A8 File Offset: 0x0037F1A8
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(MorningSuggestionsNotice other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Guid>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<IReadOnlySet<Guid>>.Default.Equals(this.<CardIds>k__BackingField, other.<CardIds>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<CreatedAt>k__BackingField, other.<CreatedAt>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<PresentNotBefore>k__BackingField, other.<PresentNotBefore>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<PreviewCardCount>k__BackingField, other.<PreviewCardCount>k__BackingField));
    }

    // Token: 0x06000C98 RID: 3224 RVA: 0x0037F258 File Offset: 0x0037F258
    [CompilerGenerated]
    private MorningSuggestionsNotice(MorningSuggestionsNotice original)
    {
      this.Id = original.<Id>k__BackingField;
      this.CardIds = original.<CardIds>k__BackingField;
      this.CreatedAt = original.<CreatedAt>k__BackingField;
      this.PresentNotBefore = original.<PresentNotBefore>k__BackingField;
      this.PreviewCardCount = original.<PreviewCardCount>k__BackingField;
    }

    // Token: 0x06000C99 RID: 3225 RVA: 0x0037F2A8 File Offset: 0x0037F2A8
    [CompilerGenerated]
    public void Deconstruct(out Guid Id, out IReadOnlySet<Guid> CardIds, out DateTimeOffset CreatedAt, out DateTimeOffset PresentNotBefore, out int? PreviewCardCount)
    {
      Id = this.Id;
      CardIds = this.CardIds;
      CreatedAt = this.CreatedAt;
      PresentNotBefore = this.PresentNotBefore;
      PreviewCardCount = this.PreviewCardCount;
    }
  }
}
