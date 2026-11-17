using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200005F RID: 95
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class CheckoutCompleteLink : IEquatable<CheckoutCompleteLink>
  {
    // Token: 0x060004C1 RID: 1217 RVA: 0x00356B00 File Offset: 0x00356B00
    public CheckoutCompleteLink(string CheckoutSessionId, bool IsCanceled, bool IsPortal)
    {
      this.CheckoutSessionId = CheckoutSessionId;
      this.IsCanceled = IsCanceled;
      this.IsPortal = IsPortal;
      base..ctor();
    }

    // Token: 0x17000110 RID: 272
    // (get) Token: 0x060004C2 RID: 1218 RVA: 0x00356B20 File Offset: 0x00356B20
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(CheckoutCompleteLink);
      }
    }

    // Token: 0x17000111 RID: 273
    // (get) Token: 0x060004C3 RID: 1219 RVA: 0x00356B2C File Offset: 0x00356B2C
    // (set) Token: 0x060004C4 RID: 1220 RVA: 0x00356B34 File Offset: 0x00356B34
    public string CheckoutSessionId { get; set; }

    // Token: 0x17000112 RID: 274
    // (get) Token: 0x060004C5 RID: 1221 RVA: 0x00356B40 File Offset: 0x00356B40
    // (set) Token: 0x060004C6 RID: 1222 RVA: 0x00356B48 File Offset: 0x00356B48
    public bool IsCanceled { get; set; }

    // Token: 0x17000113 RID: 275
    // (get) Token: 0x060004C7 RID: 1223 RVA: 0x00356B54 File Offset: 0x00356B54
    // (set) Token: 0x060004C8 RID: 1224 RVA: 0x00356B5C File Offset: 0x00356B5C
    public bool IsPortal { get; set; }

    // Token: 0x060004C9 RID: 1225 RVA: 0x00356B68 File Offset: 0x00356B68
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("CheckoutCompleteLink");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060004CA RID: 1226 RVA: 0x00356BB4 File Offset: 0x00356BB4
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("CheckoutSessionId = ");
      builder.Append(this.CheckoutSessionId);
      builder.Append(", IsCanceled = ");
      builder.Append(this.IsCanceled.ToString());
      builder.Append(", IsPortal = ");
      builder.Append(this.IsPortal.ToString());
      return true;
    }

    // Token: 0x060004CB RID: 1227 RVA: 0x00356C30 File Offset: 0x00356C30
    [CompilerGenerated]
    public static bool operator !=(CheckoutCompleteLink left, CheckoutCompleteLink right)
    {
      return !(left == right);
    }

    // Token: 0x060004CC RID: 1228 RVA: 0x00356C3C File Offset: 0x00356C3C
    [CompilerGenerated]
    public static bool operator ==(CheckoutCompleteLink left, CheckoutCompleteLink right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060004CD RID: 1229 RVA: 0x00356C50 File Offset: 0x00356C50
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CheckoutSessionId>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsCanceled>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsPortal>k__BackingField);
    }

    // Token: 0x060004CE RID: 1230 RVA: 0x00356CB4 File Offset: 0x00356CB4
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CheckoutCompleteLink);
    }

    // Token: 0x060004CF RID: 1231 RVA: 0x00356CC4 File Offset: 0x00356CC4
    [CompilerGenerated]
    public bool Equals(CheckoutCompleteLink other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<CheckoutSessionId>k__BackingField, other.<CheckoutSessionId>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsCanceled>k__BackingField, other.<IsCanceled>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsPortal>k__BackingField, other.<IsPortal>k__BackingField));
    }

    // Token: 0x060004D1 RID: 1233 RVA: 0x00356D40 File Offset: 0x00356D40
    [CompilerGenerated]
    private CheckoutCompleteLink([Nullable(1)] CheckoutCompleteLink original)
    {
      this.CheckoutSessionId = original.<CheckoutSessionId>k__BackingField;
      this.IsCanceled = original.<IsCanceled>k__BackingField;
      this.IsPortal = original.<IsPortal>k__BackingField;
    }

    // Token: 0x060004D2 RID: 1234 RVA: 0x00356D6C File Offset: 0x00356D6C
    [CompilerGenerated]
    public void Deconstruct(out string CheckoutSessionId, out bool IsCanceled, out bool IsPortal)
    {
      CheckoutSessionId = this.CheckoutSessionId;
      IsCanceled = this.IsCanceled;
      IsPortal = this.IsPortal;
    }
  }
}
