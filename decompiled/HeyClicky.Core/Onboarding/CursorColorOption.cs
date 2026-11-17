using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Onboarding
{
  // Token: 0x020000F1 RID: 241
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CursorColorOption : IEquatable<CursorColorOption>
  {
    // Token: 0x06000F63 RID: 3939 RVA: 0x0038E7FC File Offset: 0x0038E7FC
    public CursorColorOption(string Id, string Hex)
    {
      this.Id = Id;
      this.Hex = Hex;
      base..ctor();
    }

    // Token: 0x1700037A RID: 890
    // (get) Token: 0x06000F64 RID: 3940 RVA: 0x0038E814 File Offset: 0x0038E814
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(CursorColorOption);
      }
    }

    // Token: 0x1700037B RID: 891
    // (get) Token: 0x06000F65 RID: 3941 RVA: 0x0038E820 File Offset: 0x0038E820
    // (set) Token: 0x06000F66 RID: 3942 RVA: 0x0038E828 File Offset: 0x0038E828
    public string Id { get; set; }

    // Token: 0x1700037C RID: 892
    // (get) Token: 0x06000F67 RID: 3943 RVA: 0x0038E834 File Offset: 0x0038E834
    // (set) Token: 0x06000F68 RID: 3944 RVA: 0x0038E83C File Offset: 0x0038E83C
    public string Hex { get; set; }

    // Token: 0x06000F69 RID: 3945 RVA: 0x0038E848 File Offset: 0x0038E848
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("CursorColorOption");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000F6A RID: 3946 RVA: 0x0038E894 File Offset: 0x0038E894
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", Hex = ");
      builder.Append(this.Hex);
      return true;
    }

    // Token: 0x06000F6B RID: 3947 RVA: 0x0038E8D0 File Offset: 0x0038E8D0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(CursorColorOption left, CursorColorOption right)
    {
      return !(left == right);
    }

    // Token: 0x06000F6C RID: 3948 RVA: 0x0038E8DC File Offset: 0x0038E8DC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(CursorColorOption left, CursorColorOption right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000F6D RID: 3949 RVA: 0x0038E8F0 File Offset: 0x0038E8F0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Hex>k__BackingField);
    }

    // Token: 0x06000F6E RID: 3950 RVA: 0x0038E930 File Offset: 0x0038E930
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CursorColorOption);
    }

    // Token: 0x06000F6F RID: 3951 RVA: 0x0038E940 File Offset: 0x0038E940
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(CursorColorOption other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Hex>k__BackingField, other.<Hex>k__BackingField));
    }

    // Token: 0x06000F71 RID: 3953 RVA: 0x0038E9A4 File Offset: 0x0038E9A4
    [CompilerGenerated]
    private CursorColorOption(CursorColorOption original)
    {
      this.Id = original.<Id>k__BackingField;
      this.Hex = original.<Hex>k__BackingField;
    }

    // Token: 0x06000F72 RID: 3954 RVA: 0x0038E9C4 File Offset: 0x0038E9C4
    [CompilerGenerated]
    public void Deconstruct(out string Id, out string Hex)
    {
      Id = this.Id;
      Hex = this.Hex;
    }
  }
}
