using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x02000027 RID: 39
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class HighlightStroke : IEquatable<HighlightStroke>
  {
    // Token: 0x060001FB RID: 507 RVA: 0x0001FDF0 File Offset: 0x0001FDF0
    public HighlightStroke(IReadOnlyList<PointD> PointsPx, IReadOnlyList<PointD> DwellsPx, int DisplayIndex)
    {
      this.PointsPx = PointsPx;
      this.DwellsPx = DwellsPx;
      this.DisplayIndex = DisplayIndex;
      base..ctor();
    }

    // Token: 0x17000087 RID: 135
    // (get) Token: 0x060001FC RID: 508 RVA: 0x0001FE10 File Offset: 0x0001FE10
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(HighlightStroke);
      }
    }

    // Token: 0x17000088 RID: 136
    // (get) Token: 0x060001FD RID: 509 RVA: 0x0001FE1C File Offset: 0x0001FE1C
    // (set) Token: 0x060001FE RID: 510 RVA: 0x0001FE24 File Offset: 0x0001FE24
    public IReadOnlyList<PointD> PointsPx { get; set; }

    // Token: 0x17000089 RID: 137
    // (get) Token: 0x060001FF RID: 511 RVA: 0x0001FE30 File Offset: 0x0001FE30
    // (set) Token: 0x06000200 RID: 512 RVA: 0x0001FE38 File Offset: 0x0001FE38
    public IReadOnlyList<PointD> DwellsPx { get; set; }

    // Token: 0x1700008A RID: 138
    // (get) Token: 0x06000201 RID: 513 RVA: 0x0001FE44 File Offset: 0x0001FE44
    // (set) Token: 0x06000202 RID: 514 RVA: 0x0001FE4C File Offset: 0x0001FE4C
    public int DisplayIndex { get; set; }

    // Token: 0x06000203 RID: 515 RVA: 0x0001FE58 File Offset: 0x0001FE58
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("HighlightStroke");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000204 RID: 516 RVA: 0x0001FEA4 File Offset: 0x0001FEA4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("PointsPx = ");
      builder.Append(this.PointsPx);
      builder.Append(", DwellsPx = ");
      builder.Append(this.DwellsPx);
      builder.Append(", DisplayIndex = ");
      builder.Append(this.DisplayIndex.ToString());
      return true;
    }

    // Token: 0x06000205 RID: 517 RVA: 0x0001FF10 File Offset: 0x0001FF10
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(HighlightStroke left, HighlightStroke right)
    {
      return !(left == right);
    }

    // Token: 0x06000206 RID: 518 RVA: 0x0001FF1C File Offset: 0x0001FF1C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(HighlightStroke left, HighlightStroke right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000207 RID: 519 RVA: 0x0001FF30 File Offset: 0x0001FF30
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<PointD>>.Default.GetHashCode(this.<PointsPx>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<PointD>>.Default.GetHashCode(this.<DwellsPx>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<DisplayIndex>k__BackingField);
    }

    // Token: 0x06000208 RID: 520 RVA: 0x0001FF94 File Offset: 0x0001FF94
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as HighlightStroke);
    }

    // Token: 0x06000209 RID: 521 RVA: 0x0001FFA4 File Offset: 0x0001FFA4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(HighlightStroke other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<PointD>>.Default.Equals(this.<PointsPx>k__BackingField, other.<PointsPx>k__BackingField) && EqualityComparer<IReadOnlyList<PointD>>.Default.Equals(this.<DwellsPx>k__BackingField, other.<DwellsPx>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<DisplayIndex>k__BackingField, other.<DisplayIndex>k__BackingField));
    }

    // Token: 0x0600020B RID: 523 RVA: 0x00020020 File Offset: 0x00020020
    [CompilerGenerated]
    private HighlightStroke(HighlightStroke original)
    {
      this.PointsPx = original.<PointsPx>k__BackingField;
      this.DwellsPx = original.<DwellsPx>k__BackingField;
      this.DisplayIndex = original.<DisplayIndex>k__BackingField;
    }

    // Token: 0x0600020C RID: 524 RVA: 0x0002004C File Offset: 0x0002004C
    [CompilerGenerated]
    public void Deconstruct(out IReadOnlyList<PointD> PointsPx, out IReadOnlyList<PointD> DwellsPx, out int DisplayIndex)
    {
      PointsPx = this.PointsPx;
      DwellsPx = this.DwellsPx;
      DisplayIndex = this.DisplayIndex;
    }
  }
}
