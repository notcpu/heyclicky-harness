using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002FD RID: 765
  public readonly struct ArcPlacement : IEquatable<ArcPlacement>
  {
    // Token: 0x06002176 RID: 8566 RVA: 0x0061C68C File Offset: 0x0061C68C
    public ArcPlacement(double CenterX, double CenterY, double Scale, double Degrees, double Opacity, double Z)
    {
      this.CenterX = CenterX;
      this.CenterY = CenterY;
      this.Scale = Scale;
      this.Degrees = Degrees;
      this.Opacity = Opacity;
      this.Z = Z;
    }

    // Token: 0x1700062E RID: 1582
    // (get) Token: 0x06002177 RID: 8567 RVA: 0x0061C6BC File Offset: 0x0061C6BC
    // (set) Token: 0x06002178 RID: 8568 RVA: 0x0061C6C4 File Offset: 0x0061C6C4
    public double CenterX { get; set; }

    // Token: 0x1700062F RID: 1583
    // (get) Token: 0x06002179 RID: 8569 RVA: 0x0061C6D0 File Offset: 0x0061C6D0
    // (set) Token: 0x0600217A RID: 8570 RVA: 0x0061C6D8 File Offset: 0x0061C6D8
    public double CenterY { get; set; }

    // Token: 0x17000630 RID: 1584
    // (get) Token: 0x0600217B RID: 8571 RVA: 0x0061C6E4 File Offset: 0x0061C6E4
    // (set) Token: 0x0600217C RID: 8572 RVA: 0x0061C6EC File Offset: 0x0061C6EC
    public double Scale { get; set; }

    // Token: 0x17000631 RID: 1585
    // (get) Token: 0x0600217D RID: 8573 RVA: 0x0061C6F8 File Offset: 0x0061C6F8
    // (set) Token: 0x0600217E RID: 8574 RVA: 0x0061C700 File Offset: 0x0061C700
    public double Degrees { get; set; }

    // Token: 0x17000632 RID: 1586
    // (get) Token: 0x0600217F RID: 8575 RVA: 0x0061C70C File Offset: 0x0061C70C
    // (set) Token: 0x06002180 RID: 8576 RVA: 0x0061C714 File Offset: 0x0061C714
    public double Opacity { get; set; }

    // Token: 0x17000633 RID: 1587
    // (get) Token: 0x06002181 RID: 8577 RVA: 0x0061C720 File Offset: 0x0061C720
    // (set) Token: 0x06002182 RID: 8578 RVA: 0x0061C728 File Offset: 0x0061C728
    public double Z { get; set; }

    // Token: 0x17000634 RID: 1588
    // (get) Token: 0x06002183 RID: 8579 RVA: 0x0061C734 File Offset: 0x0061C734
    public ArcRect Frame
    {
      get
      {
        double num = 494.0 * this.Scale;
        double num2 = 314.0 * this.Scale;
        return new ArcRect(this.CenterX - num / 2.0, this.CenterY - num2 / 2.0, num, num2);
      }
    }

    // Token: 0x06002184 RID: 8580 RVA: 0x0061C790 File Offset: 0x0061C790
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ArcPlacement");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06002185 RID: 8581 RVA: 0x0061C7DC File Offset: 0x0061C7DC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("CenterX = ");
      builder.Append(this.CenterX.ToString());
      builder.Append(", CenterY = ");
      builder.Append(this.CenterY.ToString());
      builder.Append(", Scale = ");
      builder.Append(this.Scale.ToString());
      builder.Append(", Degrees = ");
      builder.Append(this.Degrees.ToString());
      builder.Append(", Opacity = ");
      builder.Append(this.Opacity.ToString());
      builder.Append(", Z = ");
      builder.Append(this.Z.ToString());
      builder.Append(", Frame = ");
      builder.Append(this.Frame.ToString());
      return true;
    }

    // Token: 0x06002186 RID: 8582 RVA: 0x0061C8FC File Offset: 0x0061C8FC
    [CompilerGenerated]
    public static bool operator !=(ArcPlacement left, ArcPlacement right)
    {
      return !(left == right);
    }

    // Token: 0x06002187 RID: 8583 RVA: 0x0061C908 File Offset: 0x0061C908
    [CompilerGenerated]
    public static bool operator ==(ArcPlacement left, ArcPlacement right)
    {
      return left.Equals(right);
    }

    // Token: 0x06002188 RID: 8584 RVA: 0x0061C914 File Offset: 0x0061C914
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<double>.Default.GetHashCode(this.<CenterX>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<CenterY>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Scale>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Degrees>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Opacity>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Z>k__BackingField);
    }

    // Token: 0x06002189 RID: 8585 RVA: 0x0061C9A4 File Offset: 0x0061C9A4
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is ArcPlacement && this.Equals((ArcPlacement)obj);
    }

    // Token: 0x0600218A RID: 8586 RVA: 0x0061C9BC File Offset: 0x0061C9BC
    [CompilerGenerated]
    public bool Equals(ArcPlacement other)
    {
      return EqualityComparer<double>.Default.Equals(this.<CenterX>k__BackingField, other.<CenterX>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<CenterY>k__BackingField, other.<CenterY>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Scale>k__BackingField, other.<Scale>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Degrees>k__BackingField, other.<Degrees>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Opacity>k__BackingField, other.<Opacity>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Z>k__BackingField, other.<Z>k__BackingField);
    }

    // Token: 0x0600218B RID: 8587 RVA: 0x0061CA5C File Offset: 0x0061CA5C
    [CompilerGenerated]
    public void Deconstruct(out double CenterX, out double CenterY, out double Scale, out double Degrees, out double Opacity, out double Z)
    {
      CenterX = this.CenterX;
      CenterY = this.CenterY;
      Scale = this.Scale;
      Degrees = this.Degrees;
      Opacity = this.Opacity;
      Z = this.Z;
    }
  }
}
