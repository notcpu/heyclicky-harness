using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200003F RID: 63
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class PlanSubscription : IEquatable<PlanSubscription>
  {
    // Token: 0x06000239 RID: 569 RVA: 0x00350AFC File Offset: 0x00350AFC
    public PlanSubscription([Nullable(1)] string Status, bool CancelAtPeriodEnd, string CurrentPeriodStart = null, string CurrentPeriodEnd = null, string ProductId = null, string CancelAt = null, string BillingInterval = null)
    {
      this.Status = Status;
      this.CancelAtPeriodEnd = CancelAtPeriodEnd;
      this.CurrentPeriodStart = CurrentPeriodStart;
      this.CurrentPeriodEnd = CurrentPeriodEnd;
      this.ProductId = ProductId;
      this.CancelAt = CancelAt;
      this.BillingInterval = BillingInterval;
      base..ctor();
    }

    // Token: 0x1700006B RID: 107
    // (get) Token: 0x0600023A RID: 570 RVA: 0x00350B3C File Offset: 0x00350B3C
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(PlanSubscription);
      }
    }

    // Token: 0x1700006C RID: 108
    // (get) Token: 0x0600023B RID: 571 RVA: 0x00350B48 File Offset: 0x00350B48
    // (set) Token: 0x0600023C RID: 572 RVA: 0x00350B50 File Offset: 0x00350B50
    [Nullable(1)]
    public string Status
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x1700006D RID: 109
    // (get) Token: 0x0600023D RID: 573 RVA: 0x00350B5C File Offset: 0x00350B5C
    // (set) Token: 0x0600023E RID: 574 RVA: 0x00350B64 File Offset: 0x00350B64
    public bool CancelAtPeriodEnd { get; set; }

    // Token: 0x1700006E RID: 110
    // (get) Token: 0x0600023F RID: 575 RVA: 0x00350B70 File Offset: 0x00350B70
    // (set) Token: 0x06000240 RID: 576 RVA: 0x00350B78 File Offset: 0x00350B78
    public string CurrentPeriodStart { get; set; }

    // Token: 0x1700006F RID: 111
    // (get) Token: 0x06000241 RID: 577 RVA: 0x00350B84 File Offset: 0x00350B84
    // (set) Token: 0x06000242 RID: 578 RVA: 0x00350B8C File Offset: 0x00350B8C
    public string CurrentPeriodEnd { get; set; }

    // Token: 0x17000070 RID: 112
    // (get) Token: 0x06000243 RID: 579 RVA: 0x00350B98 File Offset: 0x00350B98
    // (set) Token: 0x06000244 RID: 580 RVA: 0x00350BA0 File Offset: 0x00350BA0
    public string ProductId { get; set; }

    // Token: 0x17000071 RID: 113
    // (get) Token: 0x06000245 RID: 581 RVA: 0x00350BAC File Offset: 0x00350BAC
    // (set) Token: 0x06000246 RID: 582 RVA: 0x00350BB4 File Offset: 0x00350BB4
    public string CancelAt { get; set; }

    // Token: 0x17000072 RID: 114
    // (get) Token: 0x06000247 RID: 583 RVA: 0x00350BC0 File Offset: 0x00350BC0
    // (set) Token: 0x06000248 RID: 584 RVA: 0x00350BC8 File Offset: 0x00350BC8
    public string BillingInterval { get; set; }

    // Token: 0x06000249 RID: 585 RVA: 0x00350BD4 File Offset: 0x00350BD4
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PlanSubscription");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600024A RID: 586 RVA: 0x00350C20 File Offset: 0x00350C20
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Status = ");
      builder.Append(this.Status);
      builder.Append(", CancelAtPeriodEnd = ");
      builder.Append(this.CancelAtPeriodEnd.ToString());
      builder.Append(", CurrentPeriodStart = ");
      builder.Append(this.CurrentPeriodStart);
      builder.Append(", CurrentPeriodEnd = ");
      builder.Append(this.CurrentPeriodEnd);
      builder.Append(", ProductId = ");
      builder.Append(this.ProductId);
      builder.Append(", CancelAt = ");
      builder.Append(this.CancelAt);
      builder.Append(", BillingInterval = ");
      builder.Append(this.BillingInterval);
      return true;
    }

    // Token: 0x0600024B RID: 587 RVA: 0x00350CF0 File Offset: 0x00350CF0
    [CompilerGenerated]
    public static bool operator !=(PlanSubscription left, PlanSubscription right)
    {
      return !(left == right);
    }

    // Token: 0x0600024C RID: 588 RVA: 0x00350CFC File Offset: 0x00350CFC
    [CompilerGenerated]
    public static bool operator ==(PlanSubscription left, PlanSubscription right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600024D RID: 589 RVA: 0x00350D10 File Offset: 0x00350D10
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<CancelAtPeriodEnd>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CurrentPeriodStart>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CurrentPeriodEnd>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ProductId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CancelAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BillingInterval>k__BackingField);
    }

    // Token: 0x0600024E RID: 590 RVA: 0x00350DD0 File Offset: 0x00350DD0
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PlanSubscription);
    }

    // Token: 0x0600024F RID: 591 RVA: 0x00350DE0 File Offset: 0x00350DE0
    [CompilerGenerated]
    public bool Equals(PlanSubscription other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<CancelAtPeriodEnd>k__BackingField, other.<CancelAtPeriodEnd>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CurrentPeriodStart>k__BackingField, other.<CurrentPeriodStart>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CurrentPeriodEnd>k__BackingField, other.<CurrentPeriodEnd>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ProductId>k__BackingField, other.<ProductId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CancelAt>k__BackingField, other.<CancelAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BillingInterval>k__BackingField, other.<BillingInterval>k__BackingField));
    }

    // Token: 0x06000251 RID: 593 RVA: 0x00350EC8 File Offset: 0x00350EC8
    [CompilerGenerated]
    private PlanSubscription([Nullable(1)] PlanSubscription original)
    {
      this.Status = original.<Status>k__BackingField;
      this.CancelAtPeriodEnd = original.<CancelAtPeriodEnd>k__BackingField;
      this.CurrentPeriodStart = original.<CurrentPeriodStart>k__BackingField;
      this.CurrentPeriodEnd = original.<CurrentPeriodEnd>k__BackingField;
      this.ProductId = original.<ProductId>k__BackingField;
      this.CancelAt = original.<CancelAt>k__BackingField;
      this.BillingInterval = original.<BillingInterval>k__BackingField;
    }

    // Token: 0x06000252 RID: 594 RVA: 0x00350F30 File Offset: 0x00350F30
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string Status, out bool CancelAtPeriodEnd, out string CurrentPeriodStart, out string CurrentPeriodEnd, out string ProductId, out string CancelAt, out string BillingInterval)
    {
      Status = this.Status;
      CancelAtPeriodEnd = this.CancelAtPeriodEnd;
      CurrentPeriodStart = this.CurrentPeriodStart;
      CurrentPeriodEnd = this.CurrentPeriodEnd;
      ProductId = this.ProductId;
      CancelAt = this.CancelAt;
      BillingInterval = this.BillingInterval;
    }
  }
}
