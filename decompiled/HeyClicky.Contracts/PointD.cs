using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x02000005 RID: 5
  public readonly struct PointD : IEquatable<PointD>
  {
    // Token: 0x06000031 RID: 49 RVA: 0x0001C904 File Offset: 0x0001C904
    public PointD(double X, double Y)
    {
      this.X = X;
      this.Y = Y;
    }

    // Token: 0x1700000F RID: 15
    // (get) Token: 0x06000032 RID: 50 RVA: 0x0001C914 File Offset: 0x0001C914
    // (set) Token: 0x06000033 RID: 51 RVA: 0x0001C91C File Offset: 0x0001C91C
    public double X { get; set; }

    // Token: 0x17000010 RID: 16
    // (get) Token: 0x06000034 RID: 52 RVA: 0x0001C928 File Offset: 0x0001C928
    // (set) Token: 0x06000035 RID: 53 RVA: 0x0001C930 File Offset: 0x0001C930
    public double Y { get; set; }

    // Token: 0x06000036 RID: 54 RVA: 0x0001C93C File Offset: 0x0001C93C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PointD");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000037 RID: 55 RVA: 0x0001C988 File Offset: 0x0001C988
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("X = ");
      builder.Append(this.X.ToString());
      builder.Append(", Y = ");
      builder.Append(this.Y.ToString());
      return true;
    }

    // Token: 0x06000038 RID: 56 RVA: 0x0001C9E4 File Offset: 0x0001C9E4
    [CompilerGenerated]
    public static bool operator !=(PointD left, PointD right)
    {
      return !(left == right);
    }

    // Token: 0x06000039 RID: 57 RVA: 0x0001C9F0 File Offset: 0x0001C9F0
    [CompilerGenerated]
    public static bool operator ==(PointD left, PointD right)
    {
      return left.Equals(right);
    }

    // Token: 0x0600003A RID: 58 RVA: 0x0001C9FC File Offset: 0x0001C9FC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<double>.Default.GetHashCode(this.<X>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Y>k__BackingField);
    }

    // Token: 0x0600003B RID: 59 RVA: 0x0001CA28 File Offset: 0x0001CA28
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is PointD && this.Equals((PointD)obj);
    }

    // Token: 0x0600003C RID: 60 RVA: 0x0001CA40 File Offset: 0x0001CA40
    [CompilerGenerated]
    public bool Equals(PointD other)
    {
      return EqualityComparer<double>.Default.Equals(this.<X>k__BackingField, other.<X>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Y>k__BackingField, other.<Y>k__BackingField);
    }

    // Token: 0x0600003D RID: 61 RVA: 0x0001CA74 File Offset: 0x0001CA74
    [CompilerGenerated]
    public void Deconstruct(out double X, out double Y)
    {
      X = this.X;
      Y = this.Y;
    }
  }
}
