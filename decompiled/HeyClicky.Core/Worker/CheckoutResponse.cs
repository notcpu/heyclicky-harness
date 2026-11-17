using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200005D RID: 93
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CheckoutResponse : IEquatable<CheckoutResponse>
  {
    // Token: 0x060004A3 RID: 1187 RVA: 0x003567C8 File Offset: 0x003567C8
    public CheckoutResponse(string Url, [Nullable(2)] string DiscountCodeApplied)
    {
      this.Url = Url;
      this.DiscountCodeApplied = DiscountCodeApplied;
      base..ctor();
    }

    // Token: 0x1700010B RID: 267
    // (get) Token: 0x060004A4 RID: 1188 RVA: 0x003567E0 File Offset: 0x003567E0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(CheckoutResponse);
      }
    }

    // Token: 0x1700010C RID: 268
    // (get) Token: 0x060004A5 RID: 1189 RVA: 0x003567EC File Offset: 0x003567EC
    // (set) Token: 0x060004A6 RID: 1190 RVA: 0x003567F4 File Offset: 0x003567F4
    public string Url { get; set; }

    // Token: 0x1700010D RID: 269
    // (get) Token: 0x060004A7 RID: 1191 RVA: 0x00356800 File Offset: 0x00356800
    // (set) Token: 0x060004A8 RID: 1192 RVA: 0x00356808 File Offset: 0x00356808
    [Nullable(2)]
    public string DiscountCodeApplied
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x060004A9 RID: 1193 RVA: 0x00356814 File Offset: 0x00356814
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("CheckoutResponse");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060004AA RID: 1194 RVA: 0x00356860 File Offset: 0x00356860
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Url = ");
      builder.Append(this.Url);
      builder.Append(", DiscountCodeApplied = ");
      builder.Append(this.DiscountCodeApplied);
      return true;
    }

    // Token: 0x060004AB RID: 1195 RVA: 0x0035689C File Offset: 0x0035689C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(CheckoutResponse left, CheckoutResponse right)
    {
      return !(left == right);
    }

    // Token: 0x060004AC RID: 1196 RVA: 0x003568A8 File Offset: 0x003568A8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(CheckoutResponse left, CheckoutResponse right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060004AD RID: 1197 RVA: 0x003568BC File Offset: 0x003568BC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Url>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DiscountCodeApplied>k__BackingField);
    }

    // Token: 0x060004AE RID: 1198 RVA: 0x003568FC File Offset: 0x003568FC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CheckoutResponse);
    }

    // Token: 0x060004AF RID: 1199 RVA: 0x0035690C File Offset: 0x0035690C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(CheckoutResponse other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Url>k__BackingField, other.<Url>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DiscountCodeApplied>k__BackingField, other.<DiscountCodeApplied>k__BackingField));
    }

    // Token: 0x060004B1 RID: 1201 RVA: 0x00356970 File Offset: 0x00356970
    [CompilerGenerated]
    private CheckoutResponse(CheckoutResponse original)
    {
      this.Url = original.<Url>k__BackingField;
      this.DiscountCodeApplied = original.<DiscountCodeApplied>k__BackingField;
    }

    // Token: 0x060004B2 RID: 1202 RVA: 0x00356990 File Offset: 0x00356990
    [CompilerGenerated]
    public void Deconstruct(out string Url, [Nullable(2)] out string DiscountCodeApplied)
    {
      Url = this.Url;
      DiscountCodeApplied = this.DiscountCodeApplied;
    }
  }
}
