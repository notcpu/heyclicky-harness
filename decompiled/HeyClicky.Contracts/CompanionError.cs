using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace HeyClicky
{
  // Token: 0x0200001E RID: 30
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CompanionError : IEquatable<CompanionError>
  {
    // Token: 0x06000140 RID: 320 RVA: 0x0001E9F8 File Offset: 0x0001E9F8
    public CompanionError(string Code, string Message, JsonElement? Paywall = null)
    {
      this.Code = Code;
      this.Message = Message;
      this.Paywall = Paywall;
      base..ctor();
    }

    // Token: 0x17000057 RID: 87
    // (get) Token: 0x06000141 RID: 321 RVA: 0x0001EA18 File Offset: 0x0001EA18
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(CompanionError);
      }
    }

    // Token: 0x17000058 RID: 88
    // (get) Token: 0x06000142 RID: 322 RVA: 0x0001EA24 File Offset: 0x0001EA24
    // (set) Token: 0x06000143 RID: 323 RVA: 0x0001EA2C File Offset: 0x0001EA2C
    public string Code { get; set; }

    // Token: 0x17000059 RID: 89
    // (get) Token: 0x06000144 RID: 324 RVA: 0x0001EA38 File Offset: 0x0001EA38
    // (set) Token: 0x06000145 RID: 325 RVA: 0x0001EA40 File Offset: 0x0001EA40
    public string Message { get; set; }

    // Token: 0x1700005A RID: 90
    // (get) Token: 0x06000146 RID: 326 RVA: 0x0001EA4C File Offset: 0x0001EA4C
    // (set) Token: 0x06000147 RID: 327 RVA: 0x0001EA54 File Offset: 0x0001EA54
    public JsonElement? Paywall { get; set; }

    // Token: 0x06000148 RID: 328 RVA: 0x0001EA60 File Offset: 0x0001EA60
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("CompanionError");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000149 RID: 329 RVA: 0x0001EAAC File Offset: 0x0001EAAC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Code = ");
      builder.Append(this.Code);
      builder.Append(", Message = ");
      builder.Append(this.Message);
      builder.Append(", Paywall = ");
      builder.Append(this.Paywall.ToString());
      return true;
    }

    // Token: 0x0600014A RID: 330 RVA: 0x0001EB18 File Offset: 0x0001EB18
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(CompanionError left, CompanionError right)
    {
      return !(left == right);
    }

    // Token: 0x0600014B RID: 331 RVA: 0x0001EB24 File Offset: 0x0001EB24
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(CompanionError left, CompanionError right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600014C RID: 332 RVA: 0x0001EB38 File Offset: 0x0001EB38
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Code>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Message>k__BackingField)) * -1521134295 + EqualityComparer<JsonElement?>.Default.GetHashCode(this.<Paywall>k__BackingField);
    }

    // Token: 0x0600014D RID: 333 RVA: 0x0001EB9C File Offset: 0x0001EB9C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CompanionError);
    }

    // Token: 0x0600014E RID: 334 RVA: 0x0001EBAC File Offset: 0x0001EBAC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(CompanionError other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Code>k__BackingField, other.<Code>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Message>k__BackingField, other.<Message>k__BackingField) && EqualityComparer<JsonElement?>.Default.Equals(this.<Paywall>k__BackingField, other.<Paywall>k__BackingField));
    }

    // Token: 0x06000150 RID: 336 RVA: 0x0001EC28 File Offset: 0x0001EC28
    [CompilerGenerated]
    private CompanionError(CompanionError original)
    {
      this.Code = original.<Code>k__BackingField;
      this.Message = original.<Message>k__BackingField;
      this.Paywall = original.<Paywall>k__BackingField;
    }

    // Token: 0x06000151 RID: 337 RVA: 0x0001EC54 File Offset: 0x0001EC54
    [CompilerGenerated]
    public void Deconstruct(out string Code, out string Message, out JsonElement? Paywall)
    {
      Code = this.Code;
      Message = this.Message;
      Paywall = this.Paywall;
    }
  }
}
