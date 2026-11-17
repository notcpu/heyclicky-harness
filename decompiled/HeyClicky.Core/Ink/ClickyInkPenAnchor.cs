using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200017E RID: 382
  public readonly struct ClickyInkPenAnchor : IEquatable<ClickyInkPenAnchor>
  {
    // Token: 0x060016D8 RID: 5848 RVA: 0x003C0C4C File Offset: 0x003C0C4C
    public ClickyInkPenAnchor(double PenY, bool IsWriting)
    {
      this.PenY = PenY;
      this.IsWriting = IsWriting;
    }

    // Token: 0x17000532 RID: 1330
    // (get) Token: 0x060016D9 RID: 5849 RVA: 0x003C0C5C File Offset: 0x003C0C5C
    // (set) Token: 0x060016DA RID: 5850 RVA: 0x003C0C64 File Offset: 0x003C0C64
    public double PenY { get; set; }

    // Token: 0x17000533 RID: 1331
    // (get) Token: 0x060016DB RID: 5851 RVA: 0x003C0C70 File Offset: 0x003C0C70
    // (set) Token: 0x060016DC RID: 5852 RVA: 0x003C0C78 File Offset: 0x003C0C78
    public bool IsWriting { get; set; }

    // Token: 0x060016DD RID: 5853 RVA: 0x003C0C84 File Offset: 0x003C0C84
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkPenAnchor");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060016DE RID: 5854 RVA: 0x003C0CD0 File Offset: 0x003C0CD0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("PenY = ");
      builder.Append(this.PenY.ToString());
      builder.Append(", IsWriting = ");
      builder.Append(this.IsWriting.ToString());
      return true;
    }

    // Token: 0x060016DF RID: 5855 RVA: 0x003C0D2C File Offset: 0x003C0D2C
    [CompilerGenerated]
    public static bool operator !=(ClickyInkPenAnchor left, ClickyInkPenAnchor right)
    {
      return !(left == right);
    }

    // Token: 0x060016E0 RID: 5856 RVA: 0x003C0D38 File Offset: 0x003C0D38
    [CompilerGenerated]
    public static bool operator ==(ClickyInkPenAnchor left, ClickyInkPenAnchor right)
    {
      return left.Equals(right);
    }

    // Token: 0x060016E1 RID: 5857 RVA: 0x003C0D44 File Offset: 0x003C0D44
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<double>.Default.GetHashCode(this.<PenY>k__BackingField) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsWriting>k__BackingField);
    }

    // Token: 0x060016E2 RID: 5858 RVA: 0x003C0D70 File Offset: 0x003C0D70
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is ClickyInkPenAnchor && this.Equals((ClickyInkPenAnchor)obj);
    }

    // Token: 0x060016E3 RID: 5859 RVA: 0x003C0D88 File Offset: 0x003C0D88
    [CompilerGenerated]
    public bool Equals(ClickyInkPenAnchor other)
    {
      return EqualityComparer<double>.Default.Equals(this.<PenY>k__BackingField, other.<PenY>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsWriting>k__BackingField, other.<IsWriting>k__BackingField);
    }

    // Token: 0x060016E4 RID: 5860 RVA: 0x003C0DBC File Offset: 0x003C0DBC
    [CompilerGenerated]
    public void Deconstruct(out double PenY, out bool IsWriting)
    {
      PenY = this.PenY;
      IsWriting = this.IsWriting;
    }
  }
}
