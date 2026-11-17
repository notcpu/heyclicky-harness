using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000D2 RID: 210
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class QueuedProactiveTask : IEquatable<QueuedProactiveTask>
  {
    // Token: 0x06000D8A RID: 3466 RVA: 0x003828E0 File Offset: 0x003828E0
    public QueuedProactiveTask(ProactiveSuggestionCard Card, string ClickySlug, [Nullable(2)] string AdjustmentText, bool WasFounded, bool WasRestored)
    {
      this.Card = Card;
      this.ClickySlug = ClickySlug;
      this.AdjustmentText = AdjustmentText;
      this.WasFounded = WasFounded;
      this.WasRestored = WasRestored;
      base..ctor();
    }

    // Token: 0x170002FE RID: 766
    // (get) Token: 0x06000D8B RID: 3467 RVA: 0x00382910 File Offset: 0x00382910
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(QueuedProactiveTask);
      }
    }

    // Token: 0x170002FF RID: 767
    // (get) Token: 0x06000D8C RID: 3468 RVA: 0x0038291C File Offset: 0x0038291C
    // (set) Token: 0x06000D8D RID: 3469 RVA: 0x00382924 File Offset: 0x00382924
    public ProactiveSuggestionCard Card { get; set; }

    // Token: 0x17000300 RID: 768
    // (get) Token: 0x06000D8E RID: 3470 RVA: 0x00382930 File Offset: 0x00382930
    // (set) Token: 0x06000D8F RID: 3471 RVA: 0x00382938 File Offset: 0x00382938
    public string ClickySlug { get; set; }

    // Token: 0x17000301 RID: 769
    // (get) Token: 0x06000D90 RID: 3472 RVA: 0x00382944 File Offset: 0x00382944
    // (set) Token: 0x06000D91 RID: 3473 RVA: 0x0038294C File Offset: 0x0038294C
    [Nullable(2)]
    public string AdjustmentText
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000302 RID: 770
    // (get) Token: 0x06000D92 RID: 3474 RVA: 0x00382958 File Offset: 0x00382958
    // (set) Token: 0x06000D93 RID: 3475 RVA: 0x00382960 File Offset: 0x00382960
    public bool WasFounded { get; set; }

    // Token: 0x17000303 RID: 771
    // (get) Token: 0x06000D94 RID: 3476 RVA: 0x0038296C File Offset: 0x0038296C
    // (set) Token: 0x06000D95 RID: 3477 RVA: 0x00382974 File Offset: 0x00382974
    public bool WasRestored { get; set; }

    // Token: 0x17000304 RID: 772
    // (get) Token: 0x06000D96 RID: 3478 RVA: 0x00382980 File Offset: 0x00382980
    public Guid Id
    {
      get
      {
        return this.Card.Id;
      }
    }

    // Token: 0x17000305 RID: 773
    // (get) Token: 0x06000D97 RID: 3479 RVA: 0x00382990 File Offset: 0x00382990
    // (set) Token: 0x06000D98 RID: 3480 RVA: 0x00382998 File Offset: 0x00382998
    public bool IsPaused { get; set; }

    // Token: 0x17000306 RID: 774
    // (get) Token: 0x06000D99 RID: 3481 RVA: 0x003829A4 File Offset: 0x003829A4
    // (set) Token: 0x06000D9A RID: 3482 RVA: 0x003829AC File Offset: 0x003829AC
    public bool IsLaunching { get; set; }

    // Token: 0x06000D9B RID: 3483 RVA: 0x003829B8 File Offset: 0x003829B8
    public static IReadOnlyList<QueuedProactiveTask> NextCandidates(IReadOnlyList<QueuedProactiveTask> entries)
    {
      HashSet<string> seen = new HashSet<string>();
      return entries.Where<QueuedProactiveTask>((QueuedProactiveTask e) => seen.Add(e.ClickySlug) && !e.IsPaused && !e.IsLaunching).ToList<QueuedProactiveTask>();
    }

    // Token: 0x06000D9C RID: 3484 RVA: 0x003829F0 File Offset: 0x003829F0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("QueuedProactiveTask");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000D9D RID: 3485 RVA: 0x00382A3C File Offset: 0x00382A3C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Card = ");
      builder.Append(this.Card);
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      builder.Append(", AdjustmentText = ");
      builder.Append(this.AdjustmentText);
      builder.Append(", WasFounded = ");
      builder.Append(this.WasFounded.ToString());
      builder.Append(", WasRestored = ");
      builder.Append(this.WasRestored.ToString());
      builder.Append(", Id = ");
      builder.Append(this.Id.ToString());
      builder.Append(", IsPaused = ");
      builder.Append(this.IsPaused.ToString());
      builder.Append(", IsLaunching = ");
      builder.Append(this.IsLaunching.ToString());
      return true;
    }

    // Token: 0x06000D9E RID: 3486 RVA: 0x00382B60 File Offset: 0x00382B60
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(QueuedProactiveTask left, QueuedProactiveTask right)
    {
      return !(left == right);
    }

    // Token: 0x06000D9F RID: 3487 RVA: 0x00382B6C File Offset: 0x00382B6C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(QueuedProactiveTask left, QueuedProactiveTask right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000DA0 RID: 3488 RVA: 0x00382B80 File Offset: 0x00382B80
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ProactiveSuggestionCard>.Default.GetHashCode(this.<Card>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AdjustmentText>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<WasFounded>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<WasRestored>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsPaused>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsLaunching>k__BackingField);
    }

    // Token: 0x06000DA1 RID: 3489 RVA: 0x00382C40 File Offset: 0x00382C40
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as QueuedProactiveTask);
    }

    // Token: 0x06000DA2 RID: 3490 RVA: 0x00382C50 File Offset: 0x00382C50
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(QueuedProactiveTask other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ProactiveSuggestionCard>.Default.Equals(this.<Card>k__BackingField, other.<Card>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AdjustmentText>k__BackingField, other.<AdjustmentText>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<WasFounded>k__BackingField, other.<WasFounded>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<WasRestored>k__BackingField, other.<WasRestored>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsPaused>k__BackingField, other.<IsPaused>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsLaunching>k__BackingField, other.<IsLaunching>k__BackingField));
    }

    // Token: 0x06000DA4 RID: 3492 RVA: 0x00382D38 File Offset: 0x00382D38
    [CompilerGenerated]
    private QueuedProactiveTask(QueuedProactiveTask original)
    {
      this.Card = original.<Card>k__BackingField;
      this.ClickySlug = original.<ClickySlug>k__BackingField;
      this.AdjustmentText = original.<AdjustmentText>k__BackingField;
      this.WasFounded = original.<WasFounded>k__BackingField;
      this.WasRestored = original.<WasRestored>k__BackingField;
      this.IsPaused = original.<IsPaused>k__BackingField;
      this.IsLaunching = original.<IsLaunching>k__BackingField;
    }

    // Token: 0x06000DA5 RID: 3493 RVA: 0x00382DA0 File Offset: 0x00382DA0
    [CompilerGenerated]
    public void Deconstruct(out ProactiveSuggestionCard Card, out string ClickySlug, [Nullable(2)] out string AdjustmentText, out bool WasFounded, out bool WasRestored)
    {
      Card = this.Card;
      ClickySlug = this.ClickySlug;
      AdjustmentText = this.AdjustmentText;
      WasFounded = this.WasFounded;
      WasRestored = this.WasRestored;
    }
  }
}
