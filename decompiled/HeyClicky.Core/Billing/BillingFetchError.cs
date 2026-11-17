using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Billing
{
  // Token: 0x02000213 RID: 531
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BillingFetchError : IEquatable<BillingFetchError>
  {
    // Token: 0x06001F08 RID: 7944 RVA: 0x003F0B2C File Offset: 0x003F0B2C
    public BillingFetchError(BillingFetchErrorKind Kind, int Status, string Detail)
    {
      this.Kind = Kind;
      this.Status = Status;
      this.Detail = Detail;
      base..ctor();
    }

    // Token: 0x170006EE RID: 1774
    // (get) Token: 0x06001F09 RID: 7945 RVA: 0x003F0B4C File Offset: 0x003F0B4C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(BillingFetchError);
      }
    }

    // Token: 0x170006EF RID: 1775
    // (get) Token: 0x06001F0A RID: 7946 RVA: 0x003F0B58 File Offset: 0x003F0B58
    // (set) Token: 0x06001F0B RID: 7947 RVA: 0x003F0B60 File Offset: 0x003F0B60
    public BillingFetchErrorKind Kind { get; set; }

    // Token: 0x170006F0 RID: 1776
    // (get) Token: 0x06001F0C RID: 7948 RVA: 0x003F0B6C File Offset: 0x003F0B6C
    // (set) Token: 0x06001F0D RID: 7949 RVA: 0x003F0B74 File Offset: 0x003F0B74
    public int Status { get; set; }

    // Token: 0x170006F1 RID: 1777
    // (get) Token: 0x06001F0E RID: 7950 RVA: 0x003F0B80 File Offset: 0x003F0B80
    // (set) Token: 0x06001F0F RID: 7951 RVA: 0x003F0B88 File Offset: 0x003F0B88
    public string Detail { get; set; }

    // Token: 0x06001F10 RID: 7952 RVA: 0x003F0B94 File Offset: 0x003F0B94
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("BillingFetchError");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001F11 RID: 7953 RVA: 0x003F0BE0 File Offset: 0x003F0BE0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", Status = ");
      builder.Append(this.Status.ToString());
      builder.Append(", Detail = ");
      builder.Append(this.Detail);
      return true;
    }

    // Token: 0x06001F12 RID: 7954 RVA: 0x003F0C5C File Offset: 0x003F0C5C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(BillingFetchError left, BillingFetchError right)
    {
      return !(left == right);
    }

    // Token: 0x06001F13 RID: 7955 RVA: 0x003F0C68 File Offset: 0x003F0C68
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(BillingFetchError left, BillingFetchError right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001F14 RID: 7956 RVA: 0x003F0C7C File Offset: 0x003F0C7C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<BillingFetchErrorKind>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Detail>k__BackingField);
    }

    // Token: 0x06001F15 RID: 7957 RVA: 0x003F0CE0 File Offset: 0x003F0CE0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as BillingFetchError);
    }

    // Token: 0x06001F16 RID: 7958 RVA: 0x003F0CF0 File Offset: 0x003F0CF0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(BillingFetchError other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<BillingFetchErrorKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Detail>k__BackingField, other.<Detail>k__BackingField));
    }

    // Token: 0x06001F18 RID: 7960 RVA: 0x003F0D6C File Offset: 0x003F0D6C
    [CompilerGenerated]
    private BillingFetchError(BillingFetchError original)
    {
      this.Kind = original.<Kind>k__BackingField;
      this.Status = original.<Status>k__BackingField;
      this.Detail = original.<Detail>k__BackingField;
    }

    // Token: 0x06001F19 RID: 7961 RVA: 0x003F0D98 File Offset: 0x003F0D98
    [CompilerGenerated]
    public void Deconstruct(out BillingFetchErrorKind Kind, out int Status, out string Detail)
    {
      Kind = this.Kind;
      Status = this.Status;
      Detail = this.Detail;
    }
  }
}
