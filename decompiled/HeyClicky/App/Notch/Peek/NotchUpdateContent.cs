using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000187 RID: 391
  [NullableContext(2)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class NotchUpdateContent : IEquatable<NotchUpdateContent>
  {
    // Token: 0x170003C5 RID: 965
    // (get) Token: 0x06001523 RID: 5411 RVA: 0x005B43D0 File Offset: 0x005B43D0
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(NotchUpdateContent);
      }
    }

    // Token: 0x170003C6 RID: 966
    // (get) Token: 0x06001524 RID: 5412 RVA: 0x005B43DC File Offset: 0x005B43DC
    // (set) Token: 0x06001525 RID: 5413 RVA: 0x005B43E4 File Offset: 0x005B43E4
    [Nullable(1)]
    [RequiredMember]
    public string RailKey
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170003C7 RID: 967
    // (get) Token: 0x06001526 RID: 5414 RVA: 0x005B43F0 File Offset: 0x005B43F0
    // (set) Token: 0x06001527 RID: 5415 RVA: 0x005B43F8 File Offset: 0x005B43F8
    public string ClickySlug { get; set; }

    // Token: 0x170003C8 RID: 968
    // (get) Token: 0x06001528 RID: 5416 RVA: 0x005B4404 File Offset: 0x005B4404
    // (set) Token: 0x06001529 RID: 5417 RVA: 0x005B440C File Offset: 0x005B440C
    public string JobId { get; set; }

    // Token: 0x170003C9 RID: 969
    // (get) Token: 0x0600152A RID: 5418 RVA: 0x005B4418 File Offset: 0x005B4418
    // (set) Token: 0x0600152B RID: 5419 RVA: 0x005B4420 File Offset: 0x005B4420
    public string ThreadId { get; set; }

    // Token: 0x170003CA RID: 970
    // (get) Token: 0x0600152C RID: 5420 RVA: 0x005B442C File Offset: 0x005B442C
    // (set) Token: 0x0600152D RID: 5421 RVA: 0x005B4434 File Offset: 0x005B4434
    [Nullable(1)]
    [RequiredMember]
    public string Title
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170003CB RID: 971
    // (get) Token: 0x0600152E RID: 5422 RVA: 0x005B4440 File Offset: 0x005B4440
    // (set) Token: 0x0600152F RID: 5423 RVA: 0x005B4448 File Offset: 0x005B4448
    public string Summary { get; set; }

    // Token: 0x170003CC RID: 972
    // (get) Token: 0x06001530 RID: 5424 RVA: 0x005B4454 File Offset: 0x005B4454
    // (set) Token: 0x06001531 RID: 5425 RVA: 0x005B445C File Offset: 0x005B445C
    public NotchUpdateRequest Request { get; set; }

    // Token: 0x170003CD RID: 973
    // (get) Token: 0x06001532 RID: 5426 RVA: 0x005B4468 File Offset: 0x005B4468
    // (set) Token: 0x06001533 RID: 5427 RVA: 0x005B4470 File Offset: 0x005B4470
    public NotchUpdateReadyOutput ReadyOutput { get; set; }

    // Token: 0x170003CE RID: 974
    // (get) Token: 0x06001534 RID: 5428 RVA: 0x005B447C File Offset: 0x005B447C
    // (set) Token: 0x06001535 RID: 5429 RVA: 0x005B4484 File Offset: 0x005B4484
    [Nullable(1)]
    public IReadOnlyList<string> Files
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170003CF RID: 975
    // (get) Token: 0x06001536 RID: 5430 RVA: 0x005B4490 File Offset: 0x005B4490
    // (set) Token: 0x06001537 RID: 5431 RVA: 0x005B4498 File Offset: 0x005B4498
    public bool IsStopped { get; set; }

    // Token: 0x170003D0 RID: 976
    // (get) Token: 0x06001538 RID: 5432 RVA: 0x005B44A4 File Offset: 0x005B44A4
    // (set) Token: 0x06001539 RID: 5433 RVA: 0x005B44AC File Offset: 0x005B44AC
    public string RoutineBadge { get; set; }

    // Token: 0x170003D1 RID: 977
    // (get) Token: 0x0600153A RID: 5434 RVA: 0x005B44B8 File Offset: 0x005B44B8
    // (set) Token: 0x0600153B RID: 5435 RVA: 0x005B44C0 File Offset: 0x005B44C0
    public string RoutineTitle { get; set; }

    // Token: 0x0600153C RID: 5436 RVA: 0x005B44CC File Offset: 0x005B44CC
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("NotchUpdateContent");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600153D RID: 5437 RVA: 0x005B4518 File Offset: 0x005B4518
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("RailKey = ");
      builder.Append(this.RailKey);
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      builder.Append(", JobId = ");
      builder.Append(this.JobId);
      builder.Append(", ThreadId = ");
      builder.Append(this.ThreadId);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Summary = ");
      builder.Append(this.Summary);
      builder.Append(", Request = ");
      builder.Append(this.Request);
      builder.Append(", ReadyOutput = ");
      builder.Append(this.ReadyOutput);
      builder.Append(", Files = ");
      builder.Append(this.Files);
      builder.Append(", IsStopped = ");
      builder.Append(this.IsStopped.ToString());
      builder.Append(", RoutineBadge = ");
      builder.Append(this.RoutineBadge);
      builder.Append(", RoutineTitle = ");
      builder.Append(this.RoutineTitle);
      return true;
    }

    // Token: 0x0600153E RID: 5438 RVA: 0x005B4668 File Offset: 0x005B4668
    [CompilerGenerated]
    public static bool operator !=(NotchUpdateContent left, NotchUpdateContent right)
    {
      return !(left == right);
    }

    // Token: 0x0600153F RID: 5439 RVA: 0x005B4674 File Offset: 0x005B4674
    [CompilerGenerated]
    public static bool operator ==(NotchUpdateContent left, NotchUpdateContent right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001540 RID: 5440 RVA: 0x005B4688 File Offset: 0x005B4688
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RailKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<JobId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Summary>k__BackingField)) * -1521134295 + EqualityComparer<NotchUpdateRequest>.Default.GetHashCode(this.<Request>k__BackingField)) * -1521134295 + EqualityComparer<NotchUpdateReadyOutput>.Default.GetHashCode(this.<ReadyOutput>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Files>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsStopped>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RoutineBadge>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RoutineTitle>k__BackingField);
    }

    // Token: 0x06001541 RID: 5441 RVA: 0x005B47BC File Offset: 0x005B47BC
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as NotchUpdateContent);
    }

    // Token: 0x06001542 RID: 5442 RVA: 0x005B47CC File Offset: 0x005B47CC
    [CompilerGenerated]
    public bool Equals(NotchUpdateContent other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<RailKey>k__BackingField, other.<RailKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<JobId>k__BackingField, other.<JobId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Summary>k__BackingField, other.<Summary>k__BackingField) && EqualityComparer<NotchUpdateRequest>.Default.Equals(this.<Request>k__BackingField, other.<Request>k__BackingField) && EqualityComparer<NotchUpdateReadyOutput>.Default.Equals(this.<ReadyOutput>k__BackingField, other.<ReadyOutput>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Files>k__BackingField, other.<Files>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsStopped>k__BackingField, other.<IsStopped>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RoutineBadge>k__BackingField, other.<RoutineBadge>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RoutineTitle>k__BackingField, other.<RoutineTitle>k__BackingField));
    }

    // Token: 0x06001544 RID: 5444 RVA: 0x005B4938 File Offset: 0x005B4938
    [CompilerGenerated]
    [SetsRequiredMembers]
    private NotchUpdateContent([Nullable(1)] NotchUpdateContent original)
    {
      this.RailKey = original.<RailKey>k__BackingField;
      this.ClickySlug = original.<ClickySlug>k__BackingField;
      this.JobId = original.<JobId>k__BackingField;
      this.ThreadId = original.<ThreadId>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.Summary = original.<Summary>k__BackingField;
      this.Request = original.<Request>k__BackingField;
      this.ReadyOutput = original.<ReadyOutput>k__BackingField;
      this.Files = original.<Files>k__BackingField;
      this.IsStopped = original.<IsStopped>k__BackingField;
      this.RoutineBadge = original.<RoutineBadge>k__BackingField;
      this.RoutineTitle = original.<RoutineTitle>k__BackingField;
    }

    // Token: 0x06001545 RID: 5445 RVA: 0x005B49DC File Offset: 0x005B49DC
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public NotchUpdateContent()
    {
      this.Files = Array.Empty<string>();
      base..ctor();
    }
  }
}
