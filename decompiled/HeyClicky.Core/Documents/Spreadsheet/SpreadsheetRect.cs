using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x0200019E RID: 414
  public readonly struct SpreadsheetRect : IEquatable<SpreadsheetRect>
  {
    // Token: 0x06001890 RID: 6288 RVA: 0x003CB6A4 File Offset: 0x003CB6A4
    public SpreadsheetRect(double X, double Y, double Width, double Height)
    {
      this.X = X;
      this.Y = Y;
      this.Width = Width;
      this.Height = Height;
    }

    // Token: 0x1700056C RID: 1388
    // (get) Token: 0x06001891 RID: 6289 RVA: 0x003CB6C4 File Offset: 0x003CB6C4
    // (set) Token: 0x06001892 RID: 6290 RVA: 0x003CB6CC File Offset: 0x003CB6CC
    public double X { get; set; }

    // Token: 0x1700056D RID: 1389
    // (get) Token: 0x06001893 RID: 6291 RVA: 0x003CB6D8 File Offset: 0x003CB6D8
    // (set) Token: 0x06001894 RID: 6292 RVA: 0x003CB6E0 File Offset: 0x003CB6E0
    public double Y { get; set; }

    // Token: 0x1700056E RID: 1390
    // (get) Token: 0x06001895 RID: 6293 RVA: 0x003CB6EC File Offset: 0x003CB6EC
    // (set) Token: 0x06001896 RID: 6294 RVA: 0x003CB6F4 File Offset: 0x003CB6F4
    public double Width { get; set; }

    // Token: 0x1700056F RID: 1391
    // (get) Token: 0x06001897 RID: 6295 RVA: 0x003CB700 File Offset: 0x003CB700
    // (set) Token: 0x06001898 RID: 6296 RVA: 0x003CB708 File Offset: 0x003CB708
    public double Height { get; set; }

    // Token: 0x17000570 RID: 1392
    // (get) Token: 0x06001899 RID: 6297 RVA: 0x003CB714 File Offset: 0x003CB714
    public double MinX
    {
      get
      {
        return this.X;
      }
    }

    // Token: 0x17000571 RID: 1393
    // (get) Token: 0x0600189A RID: 6298 RVA: 0x003CB71C File Offset: 0x003CB71C
    public double MinY
    {
      get
      {
        return this.Y;
      }
    }

    // Token: 0x17000572 RID: 1394
    // (get) Token: 0x0600189B RID: 6299 RVA: 0x003CB724 File Offset: 0x003CB724
    public double MaxX
    {
      get
      {
        return this.X + this.Width;
      }
    }

    // Token: 0x17000573 RID: 1395
    // (get) Token: 0x0600189C RID: 6300 RVA: 0x003CB734 File Offset: 0x003CB734
    public double MaxY
    {
      get
      {
        return this.Y + this.Height;
      }
    }

    // Token: 0x17000574 RID: 1396
    // (get) Token: 0x0600189D RID: 6301 RVA: 0x003CB744 File Offset: 0x003CB744
    public double MidX
    {
      get
      {
        return this.X + this.Width / 2.0;
      }
    }

    // Token: 0x17000575 RID: 1397
    // (get) Token: 0x0600189E RID: 6302 RVA: 0x003CB760 File Offset: 0x003CB760
    public double MidY
    {
      get
      {
        return this.Y + this.Height / 2.0;
      }
    }

    // Token: 0x17000576 RID: 1398
    // (get) Token: 0x0600189F RID: 6303 RVA: 0x003CB77C File Offset: 0x003CB77C
    public bool IsEmpty
    {
      get
      {
        return this.Width <= 0.0 || this.Height <= 0.0;
      }
    }

    // Token: 0x060018A0 RID: 6304 RVA: 0x003CB7A8 File Offset: 0x003CB7A8
    public SpreadsheetRect Offset(double dx, double dy)
    {
      SpreadsheetRect spreadsheetRect = this;
      spreadsheetRect.X = this.X + dx;
      spreadsheetRect.Y = this.Y + dy;
      return spreadsheetRect;
    }

    // Token: 0x060018A1 RID: 6305 RVA: 0x003CB7DC File Offset: 0x003CB7DC
    public SpreadsheetRect Inset(double dx, double dy)
    {
      return new SpreadsheetRect(this.X + dx, this.Y + dy, this.Width - 2.0 * dx, this.Height - 2.0 * dy);
    }

    // Token: 0x060018A2 RID: 6306 RVA: 0x003CB818 File Offset: 0x003CB818
    public SpreadsheetRect Intersection(SpreadsheetRect other)
    {
      double num = Math.Max(this.MinX, other.MinX);
      double num2 = Math.Max(this.MinY, other.MinY);
      double num3 = Math.Min(this.MaxX, other.MaxX);
      double num4 = Math.Min(this.MaxY, other.MaxY);
      if (num3 > num && num4 > num2)
      {
        return new SpreadsheetRect(num, num2, num3 - num, num4 - num2);
      }
      return default(SpreadsheetRect);
    }

    // Token: 0x060018A3 RID: 6307 RVA: 0x003CB894 File Offset: 0x003CB894
    public SpreadsheetRect Union(SpreadsheetRect other)
    {
      if (this.IsEmpty)
      {
        return other;
      }
      if (other.IsEmpty)
      {
        return this;
      }
      double num = Math.Min(this.MinX, other.MinX);
      double num2 = Math.Min(this.MinY, other.MinY);
      return new SpreadsheetRect(num, num2, Math.Max(this.MaxX, other.MaxX) - num, Math.Max(this.MaxY, other.MaxY) - num2);
    }

    // Token: 0x060018A4 RID: 6308 RVA: 0x003CB910 File Offset: 0x003CB910
    public bool Intersects(SpreadsheetRect other)
    {
      return !this.Intersection(other).IsEmpty;
    }

    // Token: 0x060018A5 RID: 6309 RVA: 0x003CB930 File Offset: 0x003CB930
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SpreadsheetRect");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060018A6 RID: 6310 RVA: 0x003CB97C File Offset: 0x003CB97C
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
      builder.Append(", MinX = ");
      builder.Append(this.MinX.ToString());
      builder.Append(", MinY = ");
      builder.Append(this.MinY.ToString());
      builder.Append(", MaxX = ");
      builder.Append(this.MaxX.ToString());
      builder.Append(", MaxY = ");
      builder.Append(this.MaxY.ToString());
      builder.Append(", MidX = ");
      builder.Append(this.MidX.ToString());
      builder.Append(", MidY = ");
      builder.Append(this.MidY.ToString());
      builder.Append(", IsEmpty = ");
      builder.Append(this.IsEmpty.ToString());
      return true;
    }

    // Token: 0x060018A7 RID: 6311 RVA: 0x003CBB38 File Offset: 0x003CBB38
    [CompilerGenerated]
    public static bool operator !=(SpreadsheetRect left, SpreadsheetRect right)
    {
      return !(left == right);
    }

    // Token: 0x060018A8 RID: 6312 RVA: 0x003CBB44 File Offset: 0x003CBB44
    [CompilerGenerated]
    public static bool operator ==(SpreadsheetRect left, SpreadsheetRect right)
    {
      return left.Equals(right);
    }

    // Token: 0x060018A9 RID: 6313 RVA: 0x003CBB50 File Offset: 0x003CBB50
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<double>.Default.GetHashCode(this.<X>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Y>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Width>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Height>k__BackingField);
    }

    // Token: 0x060018AA RID: 6314 RVA: 0x003CBBB4 File Offset: 0x003CBBB4
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is SpreadsheetRect && this.Equals((SpreadsheetRect)obj);
    }

    // Token: 0x060018AB RID: 6315 RVA: 0x003CBBCC File Offset: 0x003CBBCC
    [CompilerGenerated]
    public bool Equals(SpreadsheetRect other)
    {
      return EqualityComparer<double>.Default.Equals(this.<X>k__BackingField, other.<X>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Y>k__BackingField, other.<Y>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Width>k__BackingField, other.<Width>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Height>k__BackingField, other.<Height>k__BackingField);
    }

    // Token: 0x060018AC RID: 6316 RVA: 0x003CBC3C File Offset: 0x003CBC3C
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
