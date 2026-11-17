using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Billing
{
  // Token: 0x0200020F RID: 527
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BillingPlanAction : IEquatable<BillingPlanAction>
  {
    // Token: 0x06001EA6 RID: 7846 RVA: 0x003EF010 File Offset: 0x003EF010
    public BillingPlanAction(BillingPlanActionKind Kind, string Title, BillingActionStyle Style)
    {
      this.Kind = Kind;
      this.Title = Title;
      this.Style = Style;
      base..ctor();
    }

    // Token: 0x170006CB RID: 1739
    // (get) Token: 0x06001EA7 RID: 7847 RVA: 0x003EF030 File Offset: 0x003EF030
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(BillingPlanAction);
      }
    }

    // Token: 0x170006CC RID: 1740
    // (get) Token: 0x06001EA8 RID: 7848 RVA: 0x003EF03C File Offset: 0x003EF03C
    // (set) Token: 0x06001EA9 RID: 7849 RVA: 0x003EF044 File Offset: 0x003EF044
    public BillingPlanActionKind Kind { get; set; }

    // Token: 0x170006CD RID: 1741
    // (get) Token: 0x06001EAA RID: 7850 RVA: 0x003EF050 File Offset: 0x003EF050
    // (set) Token: 0x06001EAB RID: 7851 RVA: 0x003EF058 File Offset: 0x003EF058
    public string Title { get; set; }

    // Token: 0x170006CE RID: 1742
    // (get) Token: 0x06001EAC RID: 7852 RVA: 0x003EF064 File Offset: 0x003EF064
    // (set) Token: 0x06001EAD RID: 7853 RVA: 0x003EF06C File Offset: 0x003EF06C
    public BillingActionStyle Style { get; set; }

    // Token: 0x06001EAE RID: 7854 RVA: 0x003EF078 File Offset: 0x003EF078
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("BillingPlanAction");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001EAF RID: 7855 RVA: 0x003EF0C4 File Offset: 0x003EF0C4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Style = ");
      builder.Append(this.Style.ToString());
      return true;
    }

    // Token: 0x06001EB0 RID: 7856 RVA: 0x003EF140 File Offset: 0x003EF140
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(BillingPlanAction left, BillingPlanAction right)
    {
      return !(left == right);
    }

    // Token: 0x06001EB1 RID: 7857 RVA: 0x003EF14C File Offset: 0x003EF14C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(BillingPlanAction left, BillingPlanAction right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001EB2 RID: 7858 RVA: 0x003EF160 File Offset: 0x003EF160
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<BillingPlanActionKind>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<BillingActionStyle>.Default.GetHashCode(this.<Style>k__BackingField);
    }

    // Token: 0x06001EB3 RID: 7859 RVA: 0x003EF1C4 File Offset: 0x003EF1C4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as BillingPlanAction);
    }

    // Token: 0x06001EB4 RID: 7860 RVA: 0x003EF1D4 File Offset: 0x003EF1D4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(BillingPlanAction other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<BillingPlanActionKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<BillingActionStyle>.Default.Equals(this.<Style>k__BackingField, other.<Style>k__BackingField));
    }

    // Token: 0x06001EB6 RID: 7862 RVA: 0x003EF250 File Offset: 0x003EF250
    [CompilerGenerated]
    private BillingPlanAction(BillingPlanAction original)
    {
      this.Kind = original.<Kind>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.Style = original.<Style>k__BackingField;
    }

    // Token: 0x06001EB7 RID: 7863 RVA: 0x003EF27C File Offset: 0x003EF27C
    [CompilerGenerated]
    public void Deconstruct(out BillingPlanActionKind Kind, out string Title, out BillingActionStyle Style)
    {
      Kind = this.Kind;
      Title = this.Title;
      Style = this.Style;
    }
  }
}
