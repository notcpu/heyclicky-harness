using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200005C RID: 92
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CheckoutRequest : IEquatable<CheckoutRequest>
  {
    // Token: 0x0600048F RID: 1167 RVA: 0x003564E4 File Offset: 0x003564E4
    public CheckoutRequest(string Plan, string Interval, string CheckoutUi = "web", [Nullable(2)] string DiscountCode = null)
    {
      this.Plan = Plan;
      this.Interval = Interval;
      this.CheckoutUi = CheckoutUi;
      this.DiscountCode = DiscountCode;
      base..ctor();
    }

    // Token: 0x17000106 RID: 262
    // (get) Token: 0x06000490 RID: 1168 RVA: 0x0035650C File Offset: 0x0035650C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(CheckoutRequest);
      }
    }

    // Token: 0x17000107 RID: 263
    // (get) Token: 0x06000491 RID: 1169 RVA: 0x00356518 File Offset: 0x00356518
    // (set) Token: 0x06000492 RID: 1170 RVA: 0x00356520 File Offset: 0x00356520
    public string Plan { get; set; }

    // Token: 0x17000108 RID: 264
    // (get) Token: 0x06000493 RID: 1171 RVA: 0x0035652C File Offset: 0x0035652C
    // (set) Token: 0x06000494 RID: 1172 RVA: 0x00356534 File Offset: 0x00356534
    public string Interval { get; set; }

    // Token: 0x17000109 RID: 265
    // (get) Token: 0x06000495 RID: 1173 RVA: 0x00356540 File Offset: 0x00356540
    // (set) Token: 0x06000496 RID: 1174 RVA: 0x00356548 File Offset: 0x00356548
    public string CheckoutUi { get; set; }

    // Token: 0x1700010A RID: 266
    // (get) Token: 0x06000497 RID: 1175 RVA: 0x00356554 File Offset: 0x00356554
    // (set) Token: 0x06000498 RID: 1176 RVA: 0x0035655C File Offset: 0x0035655C
    [Nullable(2)]
    public string DiscountCode
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000499 RID: 1177 RVA: 0x00356568 File Offset: 0x00356568
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("CheckoutRequest");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600049A RID: 1178 RVA: 0x003565B4 File Offset: 0x003565B4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Plan = ");
      builder.Append(this.Plan);
      builder.Append(", Interval = ");
      builder.Append(this.Interval);
      builder.Append(", CheckoutUi = ");
      builder.Append(this.CheckoutUi);
      builder.Append(", DiscountCode = ");
      builder.Append(this.DiscountCode);
      return true;
    }

    // Token: 0x0600049B RID: 1179 RVA: 0x0035662C File Offset: 0x0035662C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(CheckoutRequest left, CheckoutRequest right)
    {
      return !(left == right);
    }

    // Token: 0x0600049C RID: 1180 RVA: 0x00356638 File Offset: 0x00356638
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(CheckoutRequest left, CheckoutRequest right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600049D RID: 1181 RVA: 0x0035664C File Offset: 0x0035664C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Plan>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Interval>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CheckoutUi>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DiscountCode>k__BackingField);
    }

    // Token: 0x0600049E RID: 1182 RVA: 0x003566C8 File Offset: 0x003566C8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CheckoutRequest);
    }

    // Token: 0x0600049F RID: 1183 RVA: 0x003566D8 File Offset: 0x003566D8
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(CheckoutRequest other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Plan>k__BackingField, other.<Plan>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Interval>k__BackingField, other.<Interval>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CheckoutUi>k__BackingField, other.<CheckoutUi>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DiscountCode>k__BackingField, other.<DiscountCode>k__BackingField));
    }

    // Token: 0x060004A1 RID: 1185 RVA: 0x0035676C File Offset: 0x0035676C
    [CompilerGenerated]
    private CheckoutRequest(CheckoutRequest original)
    {
      this.Plan = original.<Plan>k__BackingField;
      this.Interval = original.<Interval>k__BackingField;
      this.CheckoutUi = original.<CheckoutUi>k__BackingField;
      this.DiscountCode = original.<DiscountCode>k__BackingField;
    }

    // Token: 0x060004A2 RID: 1186 RVA: 0x003567A4 File Offset: 0x003567A4
    [CompilerGenerated]
    public void Deconstruct(out string Plan, out string Interval, out string CheckoutUi, [Nullable(2)] out string DiscountCode)
    {
      Plan = this.Plan;
      Interval = this.Interval;
      CheckoutUi = this.CheckoutUi;
      DiscountCode = this.DiscountCode;
    }
  }
}
