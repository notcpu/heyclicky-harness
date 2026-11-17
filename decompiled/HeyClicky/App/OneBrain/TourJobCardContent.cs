using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.OneBrain
{
  // Token: 0x02000112 RID: 274
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class TourJobCardContent : IEquatable<TourJobCardContent>
  {
    // Token: 0x06000E61 RID: 3681 RVA: 0x00573910 File Offset: 0x00573910
    public TourJobCardContent(string RailKey, string Title, [Nullable(2)] string Summary, TourPaperStock Paper, bool IsTourRunning)
    {
      this.RailKey = RailKey;
      this.Title = Title;
      this.Summary = Summary;
      this.Paper = Paper;
      this.IsTourRunning = IsTourRunning;
      base..ctor();
    }

    // Token: 0x17000299 RID: 665
    // (get) Token: 0x06000E62 RID: 3682 RVA: 0x00573940 File Offset: 0x00573940
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(TourJobCardContent);
      }
    }

    // Token: 0x1700029A RID: 666
    // (get) Token: 0x06000E63 RID: 3683 RVA: 0x0057394C File Offset: 0x0057394C
    // (set) Token: 0x06000E64 RID: 3684 RVA: 0x00573954 File Offset: 0x00573954
    public string RailKey { get; set; }

    // Token: 0x1700029B RID: 667
    // (get) Token: 0x06000E65 RID: 3685 RVA: 0x00573960 File Offset: 0x00573960
    // (set) Token: 0x06000E66 RID: 3686 RVA: 0x00573968 File Offset: 0x00573968
    public string Title { get; set; }

    // Token: 0x1700029C RID: 668
    // (get) Token: 0x06000E67 RID: 3687 RVA: 0x00573974 File Offset: 0x00573974
    // (set) Token: 0x06000E68 RID: 3688 RVA: 0x0057397C File Offset: 0x0057397C
    [Nullable(2)]
    public string Summary
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700029D RID: 669
    // (get) Token: 0x06000E69 RID: 3689 RVA: 0x00573988 File Offset: 0x00573988
    // (set) Token: 0x06000E6A RID: 3690 RVA: 0x00573990 File Offset: 0x00573990
    public TourPaperStock Paper { get; set; }

    // Token: 0x1700029E RID: 670
    // (get) Token: 0x06000E6B RID: 3691 RVA: 0x0057399C File Offset: 0x0057399C
    // (set) Token: 0x06000E6C RID: 3692 RVA: 0x005739A4 File Offset: 0x005739A4
    public bool IsTourRunning { get; set; }

    // Token: 0x06000E6D RID: 3693 RVA: 0x005739B0 File Offset: 0x005739B0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("TourJobCardContent");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000E6E RID: 3694 RVA: 0x005739FC File Offset: 0x005739FC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("RailKey = ");
      builder.Append(this.RailKey);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Summary = ");
      builder.Append(this.Summary);
      builder.Append(", Paper = ");
      builder.Append(this.Paper.ToString());
      builder.Append(", IsTourRunning = ");
      builder.Append(this.IsTourRunning.ToString());
      return true;
    }

    // Token: 0x06000E6F RID: 3695 RVA: 0x00573AA8 File Offset: 0x00573AA8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(TourJobCardContent left, TourJobCardContent right)
    {
      return !(left == right);
    }

    // Token: 0x06000E70 RID: 3696 RVA: 0x00573AB4 File Offset: 0x00573AB4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(TourJobCardContent left, TourJobCardContent right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000E71 RID: 3697 RVA: 0x00573AC8 File Offset: 0x00573AC8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RailKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Summary>k__BackingField)) * -1521134295 + EqualityComparer<TourPaperStock>.Default.GetHashCode(this.<Paper>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsTourRunning>k__BackingField);
    }

    // Token: 0x06000E72 RID: 3698 RVA: 0x00573B58 File Offset: 0x00573B58
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as TourJobCardContent);
    }

    // Token: 0x06000E73 RID: 3699 RVA: 0x00573B68 File Offset: 0x00573B68
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(TourJobCardContent other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<RailKey>k__BackingField, other.<RailKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Summary>k__BackingField, other.<Summary>k__BackingField) && EqualityComparer<TourPaperStock>.Default.Equals(this.<Paper>k__BackingField, other.<Paper>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsTourRunning>k__BackingField, other.<IsTourRunning>k__BackingField));
    }

    // Token: 0x06000E75 RID: 3701 RVA: 0x00573C18 File Offset: 0x00573C18
    [CompilerGenerated]
    private TourJobCardContent(TourJobCardContent original)
    {
      this.RailKey = original.<RailKey>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.Summary = original.<Summary>k__BackingField;
      this.Paper = original.<Paper>k__BackingField;
      this.IsTourRunning = original.<IsTourRunning>k__BackingField;
    }

    // Token: 0x06000E76 RID: 3702 RVA: 0x00573C68 File Offset: 0x00573C68
    [CompilerGenerated]
    public void Deconstruct(out string RailKey, out string Title, [Nullable(2)] out string Summary, out TourPaperStock Paper, out bool IsTourRunning)
    {
      RailKey = this.RailKey;
      Title = this.Title;
      Summary = this.Summary;
      Paper = this.Paper;
      IsTourRunning = this.IsTourRunning;
    }
  }
}
