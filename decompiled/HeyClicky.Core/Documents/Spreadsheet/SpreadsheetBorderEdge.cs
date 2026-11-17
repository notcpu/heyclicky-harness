using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001A4 RID: 420
  public readonly struct SpreadsheetBorderEdge : IEquatable<SpreadsheetBorderEdge>
  {
    // Token: 0x060018F9 RID: 6393 RVA: 0x003CE314 File Offset: 0x003CE314
    public SpreadsheetBorderEdge(double Width, SpreadsheetColor Color, bool IsDashed)
    {
      this.Width = Width;
      this.Color = Color;
      this.IsDashed = IsDashed;
    }

    // Token: 0x17000584 RID: 1412
    // (get) Token: 0x060018FA RID: 6394 RVA: 0x003CE32C File Offset: 0x003CE32C
    // (set) Token: 0x060018FB RID: 6395 RVA: 0x003CE334 File Offset: 0x003CE334
    public double Width { get; set; }

    // Token: 0x17000585 RID: 1413
    // (get) Token: 0x060018FC RID: 6396 RVA: 0x003CE340 File Offset: 0x003CE340
    // (set) Token: 0x060018FD RID: 6397 RVA: 0x003CE348 File Offset: 0x003CE348
    public SpreadsheetColor Color { get; set; }

    // Token: 0x17000586 RID: 1414
    // (get) Token: 0x060018FE RID: 6398 RVA: 0x003CE354 File Offset: 0x003CE354
    // (set) Token: 0x060018FF RID: 6399 RVA: 0x003CE35C File Offset: 0x003CE35C
    public bool IsDashed { get; set; }

    // Token: 0x06001900 RID: 6400 RVA: 0x003CE368 File Offset: 0x003CE368
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SpreadsheetBorderEdge");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001901 RID: 6401 RVA: 0x003CE3B4 File Offset: 0x003CE3B4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Width = ");
      builder.Append(this.Width.ToString());
      builder.Append(", Color = ");
      builder.Append(this.Color.ToString());
      builder.Append(", IsDashed = ");
      builder.Append(this.IsDashed.ToString());
      return true;
    }

    // Token: 0x06001902 RID: 6402 RVA: 0x003CE438 File Offset: 0x003CE438
    [CompilerGenerated]
    public static bool operator !=(SpreadsheetBorderEdge left, SpreadsheetBorderEdge right)
    {
      return !(left == right);
    }

    // Token: 0x06001903 RID: 6403 RVA: 0x003CE444 File Offset: 0x003CE444
    [CompilerGenerated]
    public static bool operator ==(SpreadsheetBorderEdge left, SpreadsheetBorderEdge right)
    {
      return left.Equals(right);
    }

    // Token: 0x06001904 RID: 6404 RVA: 0x003CE450 File Offset: 0x003CE450
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<double>.Default.GetHashCode(this.<Width>k__BackingField) * -1521134295 + EqualityComparer<SpreadsheetColor>.Default.GetHashCode(this.<Color>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsDashed>k__BackingField);
    }

    // Token: 0x06001905 RID: 6405 RVA: 0x003CE490 File Offset: 0x003CE490
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is SpreadsheetBorderEdge && this.Equals((SpreadsheetBorderEdge)obj);
    }

    // Token: 0x06001906 RID: 6406 RVA: 0x003CE4A8 File Offset: 0x003CE4A8
    [CompilerGenerated]
    public bool Equals(SpreadsheetBorderEdge other)
    {
      return EqualityComparer<double>.Default.Equals(this.<Width>k__BackingField, other.<Width>k__BackingField) && EqualityComparer<SpreadsheetColor>.Default.Equals(this.<Color>k__BackingField, other.<Color>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsDashed>k__BackingField, other.<IsDashed>k__BackingField);
    }

    // Token: 0x06001907 RID: 6407 RVA: 0x003CE500 File Offset: 0x003CE500
    [CompilerGenerated]
    public void Deconstruct(out double Width, out SpreadsheetColor Color, out bool IsDashed)
    {
      Width = this.Width;
      Color = this.Color;
      IsDashed = this.IsDashed;
    }
  }
}
