using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200006E RID: 110
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CompanionTurnFinish : IEquatable<CompanionTurnFinish>
  {
    // Token: 0x060006A8 RID: 1704 RVA: 0x0035F6EC File Offset: 0x0035F6EC
    public CompanionTurnFinish(bool WordedTurn, IReadOnlyList<string> HeardLines, bool DrewMarks, [Nullable(2)] string UserWords = null)
    {
      this.WordedTurn = WordedTurn;
      this.HeardLines = HeardLines;
      this.DrewMarks = DrewMarks;
      this.UserWords = UserWords;
      base..ctor();
    }

    // Token: 0x1700017A RID: 378
    // (get) Token: 0x060006A9 RID: 1705 RVA: 0x0035F714 File Offset: 0x0035F714
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(CompanionTurnFinish);
      }
    }

    // Token: 0x1700017B RID: 379
    // (get) Token: 0x060006AA RID: 1706 RVA: 0x0035F720 File Offset: 0x0035F720
    // (set) Token: 0x060006AB RID: 1707 RVA: 0x0035F728 File Offset: 0x0035F728
    public bool WordedTurn { get; set; }

    // Token: 0x1700017C RID: 380
    // (get) Token: 0x060006AC RID: 1708 RVA: 0x0035F734 File Offset: 0x0035F734
    // (set) Token: 0x060006AD RID: 1709 RVA: 0x0035F73C File Offset: 0x0035F73C
    public IReadOnlyList<string> HeardLines { get; set; }

    // Token: 0x1700017D RID: 381
    // (get) Token: 0x060006AE RID: 1710 RVA: 0x0035F748 File Offset: 0x0035F748
    // (set) Token: 0x060006AF RID: 1711 RVA: 0x0035F750 File Offset: 0x0035F750
    public bool DrewMarks { get; set; }

    // Token: 0x1700017E RID: 382
    // (get) Token: 0x060006B0 RID: 1712 RVA: 0x0035F75C File Offset: 0x0035F75C
    // (set) Token: 0x060006B1 RID: 1713 RVA: 0x0035F764 File Offset: 0x0035F764
    [Nullable(2)]
    public string UserWords
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700017F RID: 383
    // (get) Token: 0x060006B2 RID: 1714 RVA: 0x0035F770 File Offset: 0x0035F770
    public string SpokenText
    {
      get
      {
        return string.Join(" ", this.HeardLines).Trim();
      }
    }

    // Token: 0x060006B3 RID: 1715 RVA: 0x0035F788 File Offset: 0x0035F788
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("CompanionTurnFinish");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060006B4 RID: 1716 RVA: 0x0035F7D4 File Offset: 0x0035F7D4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("WordedTurn = ");
      builder.Append(this.WordedTurn.ToString());
      builder.Append(", HeardLines = ");
      builder.Append(this.HeardLines);
      builder.Append(", DrewMarks = ");
      builder.Append(this.DrewMarks.ToString());
      builder.Append(", UserWords = ");
      builder.Append(this.UserWords);
      builder.Append(", SpokenText = ");
      builder.Append(this.SpokenText);
      return true;
    }

    // Token: 0x060006B5 RID: 1717 RVA: 0x0035F880 File Offset: 0x0035F880
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(CompanionTurnFinish left, CompanionTurnFinish right)
    {
      return !(left == right);
    }

    // Token: 0x060006B6 RID: 1718 RVA: 0x0035F88C File Offset: 0x0035F88C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(CompanionTurnFinish left, CompanionTurnFinish right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060006B7 RID: 1719 RVA: 0x0035F8A0 File Offset: 0x0035F8A0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<WordedTurn>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<HeardLines>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<DrewMarks>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UserWords>k__BackingField);
    }

    // Token: 0x060006B8 RID: 1720 RVA: 0x0035F91C File Offset: 0x0035F91C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CompanionTurnFinish);
    }

    // Token: 0x060006B9 RID: 1721 RVA: 0x0035F92C File Offset: 0x0035F92C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(CompanionTurnFinish other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<WordedTurn>k__BackingField, other.<WordedTurn>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<HeardLines>k__BackingField, other.<HeardLines>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<DrewMarks>k__BackingField, other.<DrewMarks>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UserWords>k__BackingField, other.<UserWords>k__BackingField));
    }

    // Token: 0x060006BB RID: 1723 RVA: 0x0035F9C0 File Offset: 0x0035F9C0
    [CompilerGenerated]
    private CompanionTurnFinish(CompanionTurnFinish original)
    {
      this.WordedTurn = original.<WordedTurn>k__BackingField;
      this.HeardLines = original.<HeardLines>k__BackingField;
      this.DrewMarks = original.<DrewMarks>k__BackingField;
      this.UserWords = original.<UserWords>k__BackingField;
    }

    // Token: 0x060006BC RID: 1724 RVA: 0x0035F9F8 File Offset: 0x0035F9F8
    [CompilerGenerated]
    public void Deconstruct(out bool WordedTurn, out IReadOnlyList<string> HeardLines, out bool DrewMarks, [Nullable(2)] out string UserWords)
    {
      WordedTurn = this.WordedTurn;
      HeardLines = this.HeardLines;
      DrewMarks = this.DrewMarks;
      UserWords = this.UserWords;
    }
  }
}
