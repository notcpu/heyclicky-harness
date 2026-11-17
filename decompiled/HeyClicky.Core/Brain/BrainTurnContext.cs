using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001F8 RID: 504
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BrainTurnContext : IEquatable<BrainTurnContext>
  {
    // Token: 0x06001D40 RID: 7488 RVA: 0x003E84EC File Offset: 0x003E84EC
    public BrainTurnContext(string SessionId, bool IsSessionsFirstTurn, bool SendEnvironment, [Nullable(2)] string DrawingCodeToResend)
    {
      this.SessionId = SessionId;
      this.IsSessionsFirstTurn = IsSessionsFirstTurn;
      this.SendEnvironment = SendEnvironment;
      this.DrawingCodeToResend = DrawingCodeToResend;
      base..ctor();
    }

    // Token: 0x1700066E RID: 1646
    // (get) Token: 0x06001D41 RID: 7489 RVA: 0x003E8514 File Offset: 0x003E8514
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(BrainTurnContext);
      }
    }

    // Token: 0x1700066F RID: 1647
    // (get) Token: 0x06001D42 RID: 7490 RVA: 0x003E8520 File Offset: 0x003E8520
    // (set) Token: 0x06001D43 RID: 7491 RVA: 0x003E8528 File Offset: 0x003E8528
    public string SessionId { get; set; }

    // Token: 0x17000670 RID: 1648
    // (get) Token: 0x06001D44 RID: 7492 RVA: 0x003E8534 File Offset: 0x003E8534
    // (set) Token: 0x06001D45 RID: 7493 RVA: 0x003E853C File Offset: 0x003E853C
    public bool IsSessionsFirstTurn { get; set; }

    // Token: 0x17000671 RID: 1649
    // (get) Token: 0x06001D46 RID: 7494 RVA: 0x003E8548 File Offset: 0x003E8548
    // (set) Token: 0x06001D47 RID: 7495 RVA: 0x003E8550 File Offset: 0x003E8550
    public bool SendEnvironment { get; set; }

    // Token: 0x17000672 RID: 1650
    // (get) Token: 0x06001D48 RID: 7496 RVA: 0x003E855C File Offset: 0x003E855C
    // (set) Token: 0x06001D49 RID: 7497 RVA: 0x003E8564 File Offset: 0x003E8564
    [Nullable(2)]
    public string DrawingCodeToResend
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06001D4A RID: 7498 RVA: 0x003E8570 File Offset: 0x003E8570
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("BrainTurnContext");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001D4B RID: 7499 RVA: 0x003E85BC File Offset: 0x003E85BC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("SessionId = ");
      builder.Append(this.SessionId);
      builder.Append(", IsSessionsFirstTurn = ");
      builder.Append(this.IsSessionsFirstTurn.ToString());
      builder.Append(", SendEnvironment = ");
      builder.Append(this.SendEnvironment.ToString());
      builder.Append(", DrawingCodeToResend = ");
      builder.Append(this.DrawingCodeToResend);
      return true;
    }

    // Token: 0x06001D4C RID: 7500 RVA: 0x003E8650 File Offset: 0x003E8650
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(BrainTurnContext left, BrainTurnContext right)
    {
      return !(left == right);
    }

    // Token: 0x06001D4D RID: 7501 RVA: 0x003E865C File Offset: 0x003E865C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(BrainTurnContext left, BrainTurnContext right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001D4E RID: 7502 RVA: 0x003E8670 File Offset: 0x003E8670
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SessionId>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsSessionsFirstTurn>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<SendEnvironment>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DrawingCodeToResend>k__BackingField);
    }

    // Token: 0x06001D4F RID: 7503 RVA: 0x003E86EC File Offset: 0x003E86EC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as BrainTurnContext);
    }

    // Token: 0x06001D50 RID: 7504 RVA: 0x003E86FC File Offset: 0x003E86FC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(BrainTurnContext other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<SessionId>k__BackingField, other.<SessionId>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsSessionsFirstTurn>k__BackingField, other.<IsSessionsFirstTurn>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<SendEnvironment>k__BackingField, other.<SendEnvironment>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DrawingCodeToResend>k__BackingField, other.<DrawingCodeToResend>k__BackingField));
    }

    // Token: 0x06001D52 RID: 7506 RVA: 0x003E8790 File Offset: 0x003E8790
    [CompilerGenerated]
    private BrainTurnContext(BrainTurnContext original)
    {
      this.SessionId = original.<SessionId>k__BackingField;
      this.IsSessionsFirstTurn = original.<IsSessionsFirstTurn>k__BackingField;
      this.SendEnvironment = original.<SendEnvironment>k__BackingField;
      this.DrawingCodeToResend = original.<DrawingCodeToResend>k__BackingField;
    }

    // Token: 0x06001D53 RID: 7507 RVA: 0x003E87C8 File Offset: 0x003E87C8
    [CompilerGenerated]
    public void Deconstruct(out string SessionId, out bool IsSessionsFirstTurn, out bool SendEnvironment, [Nullable(2)] out string DrawingCodeToResend)
    {
      SessionId = this.SessionId;
      IsSessionsFirstTurn = this.IsSessionsFirstTurn;
      SendEnvironment = this.SendEnvironment;
      DrawingCodeToResend = this.DrawingCodeToResend;
    }
  }
}
