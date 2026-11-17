using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001A1 RID: 417
  public readonly struct SpreadsheetColor : IEquatable<SpreadsheetColor>
  {
    // Token: 0x060018C4 RID: 6340 RVA: 0x003CD8C0 File Offset: 0x003CD8C0
    public SpreadsheetColor(double Red, double Green, double Blue, double Alpha = 1.0)
    {
      this.Red = Red;
      this.Green = Green;
      this.Blue = Blue;
      this.Alpha = Alpha;
    }

    // Token: 0x17000577 RID: 1399
    // (get) Token: 0x060018C5 RID: 6341 RVA: 0x003CD8E0 File Offset: 0x003CD8E0
    // (set) Token: 0x060018C6 RID: 6342 RVA: 0x003CD8E8 File Offset: 0x003CD8E8
    public double Red { get; set; }

    // Token: 0x17000578 RID: 1400
    // (get) Token: 0x060018C7 RID: 6343 RVA: 0x003CD8F4 File Offset: 0x003CD8F4
    // (set) Token: 0x060018C8 RID: 6344 RVA: 0x003CD8FC File Offset: 0x003CD8FC
    public double Green { get; set; }

    // Token: 0x17000579 RID: 1401
    // (get) Token: 0x060018C9 RID: 6345 RVA: 0x003CD908 File Offset: 0x003CD908
    // (set) Token: 0x060018CA RID: 6346 RVA: 0x003CD910 File Offset: 0x003CD910
    public double Blue { get; set; }

    // Token: 0x1700057A RID: 1402
    // (get) Token: 0x060018CB RID: 6347 RVA: 0x003CD91C File Offset: 0x003CD91C
    // (set) Token: 0x060018CC RID: 6348 RVA: 0x003CD924 File Offset: 0x003CD924
    public double Alpha { get; set; }

    // Token: 0x060018CD RID: 6349 RVA: 0x003CD930 File Offset: 0x003CD930
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SpreadsheetColor");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060018CE RID: 6350 RVA: 0x003CD97C File Offset: 0x003CD97C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Red = ");
      builder.Append(this.Red.ToString());
      builder.Append(", Green = ");
      builder.Append(this.Green.ToString());
      builder.Append(", Blue = ");
      builder.Append(this.Blue.ToString());
      builder.Append(", Alpha = ");
      builder.Append(this.Alpha.ToString());
      return true;
    }

    // Token: 0x060018CF RID: 6351 RVA: 0x003CDA28 File Offset: 0x003CDA28
    [CompilerGenerated]
    public static bool operator !=(SpreadsheetColor left, SpreadsheetColor right)
    {
      return !(left == right);
    }

    // Token: 0x060018D0 RID: 6352 RVA: 0x003CDA34 File Offset: 0x003CDA34
    [CompilerGenerated]
    public static bool operator ==(SpreadsheetColor left, SpreadsheetColor right)
    {
      return left.Equals(right);
    }

    // Token: 0x060018D1 RID: 6353 RVA: 0x003CDA40 File Offset: 0x003CDA40
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<double>.Default.GetHashCode(this.<Red>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Green>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Blue>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Alpha>k__BackingField);
    }

    // Token: 0x060018D2 RID: 6354 RVA: 0x003CDAA4 File Offset: 0x003CDAA4
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is SpreadsheetColor && this.Equals((SpreadsheetColor)obj);
    }

    // Token: 0x060018D3 RID: 6355 RVA: 0x003CDABC File Offset: 0x003CDABC
    [CompilerGenerated]
    public bool Equals(SpreadsheetColor other)
    {
      return EqualityComparer<double>.Default.Equals(this.<Red>k__BackingField, other.<Red>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Green>k__BackingField, other.<Green>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Blue>k__BackingField, other.<Blue>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Alpha>k__BackingField, other.<Alpha>k__BackingField);
    }

    // Token: 0x060018D4 RID: 6356 RVA: 0x003CDB2C File Offset: 0x003CDB2C
    [CompilerGenerated]
    public void Deconstruct(out double Red, out double Green, out double Blue, out double Alpha)
    {
      Red = this.Red;
      Green = this.Green;
      Blue = this.Blue;
      Alpha = this.Alpha;
    }
  }
}
