using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001EE RID: 494
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class BrainCompactReply : IEquatable<BrainCompactReply>
  {
    // Token: 0x06001CDA RID: 7386 RVA: 0x003E4F6C File Offset: 0x003E4F6C
    public BrainCompactReply(BrainCompactResult Compacted, int? TokensUntilFold)
    {
      this.Compacted = Compacted;
      this.TokensUntilFold = TokensUntilFold;
      base..ctor();
    }

    // Token: 0x17000665 RID: 1637
    // (get) Token: 0x06001CDB RID: 7387 RVA: 0x003E4F84 File Offset: 0x003E4F84
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(BrainCompactReply);
      }
    }

    // Token: 0x17000666 RID: 1638
    // (get) Token: 0x06001CDC RID: 7388 RVA: 0x003E4F90 File Offset: 0x003E4F90
    // (set) Token: 0x06001CDD RID: 7389 RVA: 0x003E4F98 File Offset: 0x003E4F98
    public BrainCompactResult Compacted { get; set; }

    // Token: 0x17000667 RID: 1639
    // (get) Token: 0x06001CDE RID: 7390 RVA: 0x003E4FA4 File Offset: 0x003E4FA4
    // (set) Token: 0x06001CDF RID: 7391 RVA: 0x003E4FAC File Offset: 0x003E4FAC
    public int? TokensUntilFold { get; set; }

    // Token: 0x06001CE0 RID: 7392 RVA: 0x003E4FB8 File Offset: 0x003E4FB8
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("BrainCompactReply");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001CE1 RID: 7393 RVA: 0x003E5004 File Offset: 0x003E5004
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Compacted = ");
      builder.Append(this.Compacted);
      builder.Append(", TokensUntilFold = ");
      builder.Append(this.TokensUntilFold.ToString());
      return true;
    }

    // Token: 0x06001CE2 RID: 7394 RVA: 0x003E5058 File Offset: 0x003E5058
    [CompilerGenerated]
    public static bool operator !=(BrainCompactReply left, BrainCompactReply right)
    {
      return !(left == right);
    }

    // Token: 0x06001CE3 RID: 7395 RVA: 0x003E5064 File Offset: 0x003E5064
    [CompilerGenerated]
    public static bool operator ==(BrainCompactReply left, BrainCompactReply right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001CE4 RID: 7396 RVA: 0x003E5078 File Offset: 0x003E5078
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<BrainCompactResult>.Default.GetHashCode(this.<Compacted>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<TokensUntilFold>k__BackingField);
    }

    // Token: 0x06001CE5 RID: 7397 RVA: 0x003E50B8 File Offset: 0x003E50B8
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as BrainCompactReply);
    }

    // Token: 0x06001CE6 RID: 7398 RVA: 0x003E50C8 File Offset: 0x003E50C8
    [CompilerGenerated]
    public bool Equals(BrainCompactReply other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<BrainCompactResult>.Default.Equals(this.<Compacted>k__BackingField, other.<Compacted>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<TokensUntilFold>k__BackingField, other.<TokensUntilFold>k__BackingField));
    }

    // Token: 0x06001CE8 RID: 7400 RVA: 0x003E512C File Offset: 0x003E512C
    [CompilerGenerated]
    private BrainCompactReply([Nullable(1)] BrainCompactReply original)
    {
      this.Compacted = original.<Compacted>k__BackingField;
      this.TokensUntilFold = original.<TokensUntilFold>k__BackingField;
    }

    // Token: 0x06001CE9 RID: 7401 RVA: 0x003E514C File Offset: 0x003E514C
    [CompilerGenerated]
    public void Deconstruct(out BrainCompactResult Compacted, out int? TokensUntilFold)
    {
      Compacted = this.Compacted;
      TokensUntilFold = this.TokensUntilFold;
    }
  }
}
