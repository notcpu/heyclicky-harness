using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x02000006 RID: 6
  public readonly struct RectD : IEquatable<RectD>
  {
    // Token: 0x0600003E RID: 62 RVA: 0x0001CA88 File Offset: 0x0001CA88
    public RectD(double X, double Y, double Width, double Height)
    {
      this.X = X;
      this.Y = Y;
      this.Width = Width;
      this.Height = Height;
    }

    // Token: 0x17000011 RID: 17
    // (get) Token: 0x0600003F RID: 63 RVA: 0x0001CAA8 File Offset: 0x0001CAA8
    // (set) Token: 0x06000040 RID: 64 RVA: 0x0001CAB0 File Offset: 0x0001CAB0
    public double X { get; set; }

    // Token: 0x17000012 RID: 18
    // (get) Token: 0x06000041 RID: 65 RVA: 0x0001CABC File Offset: 0x0001CABC
    // (set) Token: 0x06000042 RID: 66 RVA: 0x0001CAC4 File Offset: 0x0001CAC4
    public double Y { get; set; }

    // Token: 0x17000013 RID: 19
    // (get) Token: 0x06000043 RID: 67 RVA: 0x0001CAD0 File Offset: 0x0001CAD0
    // (set) Token: 0x06000044 RID: 68 RVA: 0x0001CAD8 File Offset: 0x0001CAD8
    public double Width { get; set; }

    // Token: 0x17000014 RID: 20
    // (get) Token: 0x06000045 RID: 69 RVA: 0x0001CAE4 File Offset: 0x0001CAE4
    // (set) Token: 0x06000046 RID: 70 RVA: 0x0001CAEC File Offset: 0x0001CAEC
    public double Height { get; set; }

    // Token: 0x17000015 RID: 21
    // (get) Token: 0x06000047 RID: 71 RVA: 0x0001CAF8 File Offset: 0x0001CAF8
    public double Right
    {
      get
      {
        return this.X + this.Width;
      }
    }

    // Token: 0x17000016 RID: 22
    // (get) Token: 0x06000048 RID: 72 RVA: 0x0001CB08 File Offset: 0x0001CB08
    public double Bottom
    {
      get
      {
        return this.Y + this.Height;
      }
    }

    // Token: 0x06000049 RID: 73 RVA: 0x0001CB18 File Offset: 0x0001CB18
    public bool Contains(PointD p)
    {
      return p.X >= this.X && p.X < this.Right && p.Y >= this.Y && p.Y < this.Bottom;
    }

    // Token: 0x0600004A RID: 74 RVA: 0x0001CB58 File Offset: 0x0001CB58
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("RectD");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600004B RID: 75 RVA: 0x0001CBA4 File Offset: 0x0001CBA4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("X = ");
      builder.Append(this.X.ToString());
      builder.Append(", Y = ");
      builder.Append(this.Y.ToString());
      builder.Append(", Width = ");
      builder.Append(this.Width.ToString());
      builder.Append(", Height = ");
      builder.Append(this.Height.ToString());
      builder.Append(", Right = ");
      builder.Append(this.Right.ToString());
      builder.Append(", Bottom = ");
      builder.Append(this.Bottom.ToString());
      return true;
    }

    // Token: 0x0600004C RID: 76 RVA: 0x0001CC9C File Offset: 0x0001CC9C
    [CompilerGenerated]
    public static bool operator !=(RectD left, RectD right)
    {
      return !(left == right);
    }

    // Token: 0x0600004D RID: 77 RVA: 0x0001CCA8 File Offset: 0x0001CCA8
    [CompilerGenerated]
    public static bool operator ==(RectD left, RectD right)
    {
      return left.Equals(right);
    }

    // Token: 0x0600004E RID: 78 RVA: 0x0001CCB4 File Offset: 0x0001CCB4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<double>.Default.GetHashCode(this.<X>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Y>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Width>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Height>k__BackingField);
    }

    // Token: 0x0600004F RID: 79 RVA: 0x0001CD18 File Offset: 0x0001CD18
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is RectD && this.Equals((RectD)obj);
    }

    // Token: 0x06000050 RID: 80 RVA: 0x0001CD30 File Offset: 0x0001CD30
    [CompilerGenerated]
    public bool Equals(RectD other)
    {
      return EqualityComparer<double>.Default.Equals(this.<X>k__BackingField, other.<X>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Y>k__BackingField, other.<Y>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Width>k__BackingField, other.<Width>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Height>k__BackingField, other.<Height>k__BackingField);
    }

    // Token: 0x06000051 RID: 81 RVA: 0x0001CDA0 File Offset: 0x0001CDA0
    [CompilerGenerated]
    public void Deconstruct(out double X, out double Y, out double Width, out double Height)
    {
      X = this.X;
      Y = this.Y;
      Width = this.Width;
      Height = this.Height;
    }
  }
}
