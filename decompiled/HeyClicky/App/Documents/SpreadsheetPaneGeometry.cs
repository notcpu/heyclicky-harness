using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002DD RID: 733
  public readonly struct SpreadsheetPaneGeometry : IEquatable<SpreadsheetPaneGeometry>
  {
    // Token: 0x06001EE5 RID: 7909 RVA: 0x00609FC4 File Offset: 0x00609FC4
    public SpreadsheetPaneGeometry(int RowStart, int RowEnd, int ColumnStart, int ColumnEnd, double OffsetX, double OffsetY, double ColumnHeaderHeight, double RowHeaderWidth)
    {
      this.RowStart = RowStart;
      this.RowEnd = RowEnd;
      this.ColumnStart = ColumnStart;
      this.ColumnEnd = ColumnEnd;
      this.OffsetX = OffsetX;
      this.OffsetY = OffsetY;
      this.ColumnHeaderHeight = ColumnHeaderHeight;
      this.RowHeaderWidth = RowHeaderWidth;
    }

    // Token: 0x1700058E RID: 1422
    // (get) Token: 0x06001EE6 RID: 7910 RVA: 0x0060A004 File Offset: 0x0060A004
    // (set) Token: 0x06001EE7 RID: 7911 RVA: 0x0060A00C File Offset: 0x0060A00C
    public int RowStart { get; set; }

    // Token: 0x1700058F RID: 1423
    // (get) Token: 0x06001EE8 RID: 7912 RVA: 0x0060A018 File Offset: 0x0060A018
    // (set) Token: 0x06001EE9 RID: 7913 RVA: 0x0060A020 File Offset: 0x0060A020
    public int RowEnd { get; set; }

    // Token: 0x17000590 RID: 1424
    // (get) Token: 0x06001EEA RID: 7914 RVA: 0x0060A02C File Offset: 0x0060A02C
    // (set) Token: 0x06001EEB RID: 7915 RVA: 0x0060A034 File Offset: 0x0060A034
    public int ColumnStart { get; set; }

    // Token: 0x17000591 RID: 1425
    // (get) Token: 0x06001EEC RID: 7916 RVA: 0x0060A040 File Offset: 0x0060A040
    // (set) Token: 0x06001EED RID: 7917 RVA: 0x0060A048 File Offset: 0x0060A048
    public int ColumnEnd { get; set; }

    // Token: 0x17000592 RID: 1426
    // (get) Token: 0x06001EEE RID: 7918 RVA: 0x0060A054 File Offset: 0x0060A054
    // (set) Token: 0x06001EEF RID: 7919 RVA: 0x0060A05C File Offset: 0x0060A05C
    public double OffsetX { get; set; }

    // Token: 0x17000593 RID: 1427
    // (get) Token: 0x06001EF0 RID: 7920 RVA: 0x0060A068 File Offset: 0x0060A068
    // (set) Token: 0x06001EF1 RID: 7921 RVA: 0x0060A070 File Offset: 0x0060A070
    public double OffsetY { get; set; }

    // Token: 0x17000594 RID: 1428
    // (get) Token: 0x06001EF2 RID: 7922 RVA: 0x0060A07C File Offset: 0x0060A07C
    // (set) Token: 0x06001EF3 RID: 7923 RVA: 0x0060A084 File Offset: 0x0060A084
    public double ColumnHeaderHeight { get; set; }

    // Token: 0x17000595 RID: 1429
    // (get) Token: 0x06001EF4 RID: 7924 RVA: 0x0060A090 File Offset: 0x0060A090
    // (set) Token: 0x06001EF5 RID: 7925 RVA: 0x0060A098 File Offset: 0x0060A098
    public double RowHeaderWidth { get; set; }

    // Token: 0x17000596 RID: 1430
    // (get) Token: 0x06001EF6 RID: 7926 RVA: 0x0060A0A4 File Offset: 0x0060A0A4
    public bool RowsEmpty
    {
      get
      {
        return this.RowEnd <= this.RowStart;
      }
    }

    // Token: 0x17000597 RID: 1431
    // (get) Token: 0x06001EF7 RID: 7927 RVA: 0x0060A0B8 File Offset: 0x0060A0B8
    public bool ColumnsEmpty
    {
      get
      {
        return this.ColumnEnd <= this.ColumnStart;
      }
    }

    // Token: 0x06001EF8 RID: 7928 RVA: 0x0060A0CC File Offset: 0x0060A0CC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SpreadsheetPaneGeometry");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001EF9 RID: 7929 RVA: 0x0060A118 File Offset: 0x0060A118
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("RowStart = ");
      builder.Append(this.RowStart.ToString());
      builder.Append(", RowEnd = ");
      builder.Append(this.RowEnd.ToString());
      builder.Append(", ColumnStart = ");
      builder.Append(this.ColumnStart.ToString());
      builder.Append(", ColumnEnd = ");
      builder.Append(this.ColumnEnd.ToString());
      builder.Append(", OffsetX = ");
      builder.Append(this.OffsetX.ToString());
      builder.Append(", OffsetY = ");
      builder.Append(this.OffsetY.ToString());
      builder.Append(", ColumnHeaderHeight = ");
      builder.Append(this.ColumnHeaderHeight.ToString());
      builder.Append(", RowHeaderWidth = ");
      builder.Append(this.RowHeaderWidth.ToString());
      builder.Append(", RowsEmpty = ");
      builder.Append(this.RowsEmpty.ToString());
      builder.Append(", ColumnsEmpty = ");
      builder.Append(this.ColumnsEmpty.ToString());
      return true;
    }

    // Token: 0x06001EFA RID: 7930 RVA: 0x0060A2AC File Offset: 0x0060A2AC
    [CompilerGenerated]
    public static bool operator !=(SpreadsheetPaneGeometry left, SpreadsheetPaneGeometry right)
    {
      return !(left == right);
    }

    // Token: 0x06001EFB RID: 7931 RVA: 0x0060A2B8 File Offset: 0x0060A2B8
    [CompilerGenerated]
    public static bool operator ==(SpreadsheetPaneGeometry left, SpreadsheetPaneGeometry right)
    {
      return left.Equals(right);
    }

    // Token: 0x06001EFC RID: 7932 RVA: 0x0060A2C4 File Offset: 0x0060A2C4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((EqualityComparer<int>.Default.GetHashCode(this.<RowStart>k__BackingField) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<RowEnd>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ColumnStart>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ColumnEnd>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<OffsetX>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<OffsetY>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<ColumnHeaderHeight>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<RowHeaderWidth>k__BackingField);
    }

    // Token: 0x06001EFD RID: 7933 RVA: 0x0060A384 File Offset: 0x0060A384
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is SpreadsheetPaneGeometry && this.Equals((SpreadsheetPaneGeometry)obj);
    }

    // Token: 0x06001EFE RID: 7934 RVA: 0x0060A39C File Offset: 0x0060A39C
    [CompilerGenerated]
    public bool Equals(SpreadsheetPaneGeometry other)
    {
      return EqualityComparer<int>.Default.Equals(this.<RowStart>k__BackingField, other.<RowStart>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<RowEnd>k__BackingField, other.<RowEnd>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ColumnStart>k__BackingField, other.<ColumnStart>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ColumnEnd>k__BackingField, other.<ColumnEnd>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<OffsetX>k__BackingField, other.<OffsetX>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<OffsetY>k__BackingField, other.<OffsetY>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<ColumnHeaderHeight>k__BackingField, other.<ColumnHeaderHeight>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<RowHeaderWidth>k__BackingField, other.<RowHeaderWidth>k__BackingField);
    }

    // Token: 0x06001EFF RID: 7935 RVA: 0x0060A470 File Offset: 0x0060A470
    [CompilerGenerated]
    public void Deconstruct(out int RowStart, out int RowEnd, out int ColumnStart, out int ColumnEnd, out double OffsetX, out double OffsetY, out double ColumnHeaderHeight, out double RowHeaderWidth)
    {
      RowStart = this.RowStart;
      RowEnd = this.RowEnd;
      ColumnStart = this.ColumnStart;
      ColumnEnd = this.ColumnEnd;
      OffsetX = this.OffsetX;
      OffsetY = this.OffsetY;
      ColumnHeaderHeight = this.ColumnHeaderHeight;
      RowHeaderWidth = this.RowHeaderWidth;
    }
  }
}
