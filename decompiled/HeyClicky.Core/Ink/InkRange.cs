using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200015C RID: 348
  public readonly struct InkRange : IEquatable<InkRange>
  {
    // Token: 0x0600151C RID: 5404 RVA: 0x003B3B7C File Offset: 0x003B3B7C
    public InkRange(double LowerBound, double UpperBound)
    {
      this.LowerBound = LowerBound;
      this.UpperBound = UpperBound;
    }

    // Token: 0x170004E6 RID: 1254
    // (get) Token: 0x0600151D RID: 5405 RVA: 0x003B3B8C File Offset: 0x003B3B8C
    // (set) Token: 0x0600151E RID: 5406 RVA: 0x003B3B94 File Offset: 0x003B3B94
    public double LowerBound { get; set; }

    // Token: 0x170004E7 RID: 1255
    // (get) Token: 0x0600151F RID: 5407 RVA: 0x003B3BA0 File Offset: 0x003B3BA0
    // (set) Token: 0x06001520 RID: 5408 RVA: 0x003B3BA8 File Offset: 0x003B3BA8
    public double UpperBound { get; set; }

    // Token: 0x06001521 RID: 5409 RVA: 0x003B3BB4 File Offset: 0x003B3BB4
    public bool Contains(double value)
    {
      return value >= this.LowerBound && value <= this.UpperBound;
    }

    // Token: 0x06001522 RID: 5410 RVA: 0x003B3BD0 File Offset: 0x003B3BD0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("InkRange");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001523 RID: 5411 RVA: 0x003B3C1C File Offset: 0x003B3C1C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("LowerBound = ");
      builder.Append(this.LowerBound.ToString());
      builder.Append(", UpperBound = ");
      builder.Append(this.UpperBound.ToString());
      return true;
    }

    // Token: 0x06001524 RID: 5412 RVA: 0x003B3C78 File Offset: 0x003B3C78
    [CompilerGenerated]
    public static bool operator !=(InkRange left, InkRange right)
    {
      return !(left == right);
    }

    // Token: 0x06001525 RID: 5413 RVA: 0x003B3C84 File Offset: 0x003B3C84
    [CompilerGenerated]
    public static bool operator ==(InkRange left, InkRange right)
    {
      return left.Equals(right);
    }

    // Token: 0x06001526 RID: 5414 RVA: 0x003B3C90 File Offset: 0x003B3C90
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<double>.Default.GetHashCode(this.<LowerBound>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<UpperBound>k__BackingField);
    }

    // Token: 0x06001527 RID: 5415 RVA: 0x003B3CBC File Offset: 0x003B3CBC
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is InkRange && this.Equals((InkRange)obj);
    }

    // Token: 0x06001528 RID: 5416 RVA: 0x003B3CD4 File Offset: 0x003B3CD4
    [CompilerGenerated]
    public bool Equals(InkRange other)
    {
      return EqualityComparer<double>.Default.Equals(this.<LowerBound>k__BackingField, other.<LowerBound>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<UpperBound>k__BackingField, other.<UpperBound>k__BackingField);
    }

    // Token: 0x06001529 RID: 5417 RVA: 0x003B3D08 File Offset: 0x003B3D08
    [CompilerGenerated]
    public void Deconstruct(out double LowerBound, out double UpperBound)
    {
      LowerBound = this.LowerBound;
      UpperBound = this.UpperBound;
    }
  }
}
