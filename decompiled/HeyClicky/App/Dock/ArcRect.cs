using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002FB RID: 763
  public readonly struct ArcRect : IEquatable<ArcRect>
  {
    // Token: 0x06002148 RID: 8520 RVA: 0x0061BDC4 File Offset: 0x0061BDC4
    public ArcRect(double X, double Y, double W, double H)
    {
      this.X = X;
      this.Y = Y;
      this.W = W;
      this.H = H;
    }

    // Token: 0x1700061F RID: 1567
    // (get) Token: 0x06002149 RID: 8521 RVA: 0x0061BDE4 File Offset: 0x0061BDE4
    // (set) Token: 0x0600214A RID: 8522 RVA: 0x0061BDEC File Offset: 0x0061BDEC
    public double X { get; set; }

    // Token: 0x17000620 RID: 1568
    // (get) Token: 0x0600214B RID: 8523 RVA: 0x0061BDF8 File Offset: 0x0061BDF8
    // (set) Token: 0x0600214C RID: 8524 RVA: 0x0061BE00 File Offset: 0x0061BE00
    public double Y { get; set; }

    // Token: 0x17000621 RID: 1569
    // (get) Token: 0x0600214D RID: 8525 RVA: 0x0061BE0C File Offset: 0x0061BE0C
    // (set) Token: 0x0600214E RID: 8526 RVA: 0x0061BE14 File Offset: 0x0061BE14
    public double W { get; set; }

    // Token: 0x17000622 RID: 1570
    // (get) Token: 0x0600214F RID: 8527 RVA: 0x0061BE20 File Offset: 0x0061BE20
    // (set) Token: 0x06002150 RID: 8528 RVA: 0x0061BE28 File Offset: 0x0061BE28
    public double H { get; set; }

    // Token: 0x17000623 RID: 1571
    // (get) Token: 0x06002151 RID: 8529 RVA: 0x0061BE34 File Offset: 0x0061BE34
    public double MidX
    {
      get
      {
        return this.X + this.W / 2.0;
      }
    }

    // Token: 0x17000624 RID: 1572
    // (get) Token: 0x06002152 RID: 8530 RVA: 0x0061BE50 File Offset: 0x0061BE50
    public double MidY
    {
      get
      {
        return this.Y + this.H / 2.0;
      }
    }

    // Token: 0x17000625 RID: 1573
    // (get) Token: 0x06002153 RID: 8531 RVA: 0x0061BE6C File Offset: 0x0061BE6C
    public double MaxX
    {
      get
      {
        return this.X + this.W;
      }
    }

    // Token: 0x17000626 RID: 1574
    // (get) Token: 0x06002154 RID: 8532 RVA: 0x0061BE7C File Offset: 0x0061BE7C
    public double MaxY
    {
      get
      {
        return this.Y + this.H;
      }
    }

    // Token: 0x17000627 RID: 1575
    // (get) Token: 0x06002155 RID: 8533 RVA: 0x0061BE8C File Offset: 0x0061BE8C
    public bool IsEmpty
    {
      get
      {
        return this.W <= 0.0 || this.H <= 0.0;
      }
    }

    // Token: 0x06002156 RID: 8534 RVA: 0x0061BEB8 File Offset: 0x0061BEB8
    public ArcRect Inset(double dx, double dy)
    {
      return new ArcRect(this.X + dx, this.Y + dy, this.W - 2.0 * dx, this.H - 2.0 * dy);
    }

    // Token: 0x06002157 RID: 8535 RVA: 0x0061BEF4 File Offset: 0x0061BEF4
    public bool Contains(double x, double y)
    {
      return x >= this.X && x <= this.MaxX && y >= this.Y && y <= this.MaxY;
    }

    // Token: 0x06002158 RID: 8536 RVA: 0x0061BF20 File Offset: 0x0061BF20
    public ArcRect Union(ArcRect other)
    {
      if (this.IsEmpty)
      {
        return other;
      }
      if (other.IsEmpty)
      {
        return this;
      }
      double num = Math.Min(this.X, other.X);
      double num2 = Math.Min(this.Y, other.Y);
      return new ArcRect(num, num2, Math.Max(this.MaxX, other.MaxX) - num, Math.Max(this.MaxY, other.MaxY) - num2);
    }

    // Token: 0x06002159 RID: 8537 RVA: 0x0061BF9C File Offset: 0x0061BF9C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ArcRect");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600215A RID: 8538 RVA: 0x0061BFE8 File Offset: 0x0061BFE8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("X = ");
      builder.Append(this.X.ToString());
      builder.Append(", Y = ");
      builder.Append(this.Y.ToString());
      builder.Append(", W = ");
      builder.Append(this.W.ToString());
      builder.Append(", H = ");
      builder.Append(this.H.ToString());
      builder.Append(", MidX = ");
      builder.Append(this.MidX.ToString());
      builder.Append(", MidY = ");
      builder.Append(this.MidY.ToString());
      builder.Append(", MaxX = ");
      builder.Append(this.MaxX.ToString());
      builder.Append(", MaxY = ");
      builder.Append(this.MaxY.ToString());
      builder.Append(", IsEmpty = ");
      builder.Append(this.IsEmpty.ToString());
      return true;
    }

    // Token: 0x0600215B RID: 8539 RVA: 0x0061C158 File Offset: 0x0061C158
    [CompilerGenerated]
    public static bool operator !=(ArcRect left, ArcRect right)
    {
      return !(left == right);
    }

    // Token: 0x0600215C RID: 8540 RVA: 0x0061C164 File Offset: 0x0061C164
    [CompilerGenerated]
    public static bool operator ==(ArcRect left, ArcRect right)
    {
      return left.Equals(right);
    }

    // Token: 0x0600215D RID: 8541 RVA: 0x0061C170 File Offset: 0x0061C170
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<double>.Default.GetHashCode(this.<X>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Y>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<W>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<H>k__BackingField);
    }

    // Token: 0x0600215E RID: 8542 RVA: 0x0061C1D4 File Offset: 0x0061C1D4
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is ArcRect && this.Equals((ArcRect)obj);
    }

    // Token: 0x0600215F RID: 8543 RVA: 0x0061C1EC File Offset: 0x0061C1EC
    [CompilerGenerated]
    public bool Equals(ArcRect other)
    {
      return EqualityComparer<double>.Default.Equals(this.<X>k__BackingField, other.<X>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Y>k__BackingField, other.<Y>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<W>k__BackingField, other.<W>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<H>k__BackingField, other.<H>k__BackingField);
    }

    // Token: 0x06002160 RID: 8544 RVA: 0x0061C25C File Offset: 0x0061C25C
    [CompilerGenerated]
    public void Deconstruct(out double X, out double Y, out double W, out double H)
    {
      X = this.X;
      Y = this.Y;
      W = this.W;
      H = this.H;
    }
  }
}
