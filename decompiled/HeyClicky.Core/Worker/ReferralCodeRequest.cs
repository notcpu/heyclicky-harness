using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000061 RID: 97
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ReferralCodeRequest : IEquatable<ReferralCodeRequest>
  {
    // Token: 0x060004E3 RID: 1251 RVA: 0x00356F64 File Offset: 0x00356F64
    public ReferralCodeRequest(string Code)
    {
      this.Code = Code;
      base..ctor();
    }

    // Token: 0x17000117 RID: 279
    // (get) Token: 0x060004E4 RID: 1252 RVA: 0x00356F74 File Offset: 0x00356F74
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ReferralCodeRequest);
      }
    }

    // Token: 0x17000118 RID: 280
    // (get) Token: 0x060004E5 RID: 1253 RVA: 0x00356F80 File Offset: 0x00356F80
    // (set) Token: 0x060004E6 RID: 1254 RVA: 0x00356F88 File Offset: 0x00356F88
    public string Code { get; set; }

    // Token: 0x060004E7 RID: 1255 RVA: 0x00356F94 File Offset: 0x00356F94
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ReferralCodeRequest");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060004E8 RID: 1256 RVA: 0x00356FE0 File Offset: 0x00356FE0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Code = ");
      builder.Append(this.Code);
      return true;
    }

    // Token: 0x060004E9 RID: 1257 RVA: 0x00357004 File Offset: 0x00357004
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ReferralCodeRequest left, ReferralCodeRequest right)
    {
      return !(left == right);
    }

    // Token: 0x060004EA RID: 1258 RVA: 0x00357010 File Offset: 0x00357010
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ReferralCodeRequest left, ReferralCodeRequest right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060004EB RID: 1259 RVA: 0x00357024 File Offset: 0x00357024
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Code>k__BackingField);
    }

    // Token: 0x060004EC RID: 1260 RVA: 0x00357050 File Offset: 0x00357050
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ReferralCodeRequest);
    }

    // Token: 0x060004ED RID: 1261 RVA: 0x00357060 File Offset: 0x00357060
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ReferralCodeRequest other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Code>k__BackingField, other.<Code>k__BackingField));
    }

    // Token: 0x060004EF RID: 1263 RVA: 0x003570A0 File Offset: 0x003570A0
    [CompilerGenerated]
    private ReferralCodeRequest(ReferralCodeRequest original)
    {
      this.Code = original.<Code>k__BackingField;
    }

    // Token: 0x060004F0 RID: 1264 RVA: 0x003570B4 File Offset: 0x003570B4
    [CompilerGenerated]
    public void Deconstruct(out string Code)
    {
      Code = this.Code;
    }
  }
}
