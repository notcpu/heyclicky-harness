using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000060 RID: 96
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ReferralClaimRequest : IEquatable<ReferralClaimRequest>
  {
    // Token: 0x060004D3 RID: 1235 RVA: 0x00356D88 File Offset: 0x00356D88
    public ReferralClaimRequest(string Code, string Source)
    {
      this.Code = Code;
      this.Source = Source;
      base..ctor();
    }

    // Token: 0x17000114 RID: 276
    // (get) Token: 0x060004D4 RID: 1236 RVA: 0x00356DA0 File Offset: 0x00356DA0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ReferralClaimRequest);
      }
    }

    // Token: 0x17000115 RID: 277
    // (get) Token: 0x060004D5 RID: 1237 RVA: 0x00356DAC File Offset: 0x00356DAC
    // (set) Token: 0x060004D6 RID: 1238 RVA: 0x00356DB4 File Offset: 0x00356DB4
    public string Code { get; set; }

    // Token: 0x17000116 RID: 278
    // (get) Token: 0x060004D7 RID: 1239 RVA: 0x00356DC0 File Offset: 0x00356DC0
    // (set) Token: 0x060004D8 RID: 1240 RVA: 0x00356DC8 File Offset: 0x00356DC8
    public string Source { get; set; }

    // Token: 0x060004D9 RID: 1241 RVA: 0x00356DD4 File Offset: 0x00356DD4
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ReferralClaimRequest");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060004DA RID: 1242 RVA: 0x00356E20 File Offset: 0x00356E20
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Code = ");
      builder.Append(this.Code);
      builder.Append(", Source = ");
      builder.Append(this.Source);
      return true;
    }

    // Token: 0x060004DB RID: 1243 RVA: 0x00356E5C File Offset: 0x00356E5C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ReferralClaimRequest left, ReferralClaimRequest right)
    {
      return !(left == right);
    }

    // Token: 0x060004DC RID: 1244 RVA: 0x00356E68 File Offset: 0x00356E68
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ReferralClaimRequest left, ReferralClaimRequest right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060004DD RID: 1245 RVA: 0x00356E7C File Offset: 0x00356E7C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Code>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Source>k__BackingField);
    }

    // Token: 0x060004DE RID: 1246 RVA: 0x00356EBC File Offset: 0x00356EBC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ReferralClaimRequest);
    }

    // Token: 0x060004DF RID: 1247 RVA: 0x00356ECC File Offset: 0x00356ECC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ReferralClaimRequest other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Code>k__BackingField, other.<Code>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Source>k__BackingField, other.<Source>k__BackingField));
    }

    // Token: 0x060004E1 RID: 1249 RVA: 0x00356F30 File Offset: 0x00356F30
    [CompilerGenerated]
    private ReferralClaimRequest(ReferralClaimRequest original)
    {
      this.Code = original.<Code>k__BackingField;
      this.Source = original.<Source>k__BackingField;
    }

    // Token: 0x060004E2 RID: 1250 RVA: 0x00356F50 File Offset: 0x00356F50
    [CompilerGenerated]
    public void Deconstruct(out string Code, out string Source)
    {
      Code = this.Code;
      Source = this.Source;
    }
  }
}
