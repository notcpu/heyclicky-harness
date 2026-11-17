using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001A3 RID: 419
  public readonly struct SpreadsheetCellRange : IEquatable<SpreadsheetCellRange>
  {
    // Token: 0x1700057E RID: 1406
    // (get) Token: 0x060018E5 RID: 6373 RVA: 0x003CDE58 File Offset: 0x003CDE58
    public int FirstRow { get; }

    // Token: 0x1700057F RID: 1407
    // (get) Token: 0x060018E6 RID: 6374 RVA: 0x003CDE60 File Offset: 0x003CDE60
    public int FirstColumn { get; }

    // Token: 0x17000580 RID: 1408
    // (get) Token: 0x060018E7 RID: 6375 RVA: 0x003CDE68 File Offset: 0x003CDE68
    public int LastRow { get; }

    // Token: 0x17000581 RID: 1409
    // (get) Token: 0x060018E8 RID: 6376 RVA: 0x003CDE70 File Offset: 0x003CDE70
    public int LastColumn { get; }

    // Token: 0x060018E9 RID: 6377 RVA: 0x003CDE78 File Offset: 0x003CDE78
    public SpreadsheetCellRange(int firstRow, int firstColumn, int lastRow, int lastColumn)
    {
      this.FirstRow = Math.Min(firstRow, lastRow);
      this.LastRow = Math.Max(firstRow, lastRow);
      this.FirstColumn = Math.Min(firstColumn, lastColumn);
      this.LastColumn = Math.Max(firstColumn, lastColumn);
    }

    // Token: 0x060018EA RID: 6378 RVA: 0x003CDEB0 File Offset: 0x003CDEB0
    public SpreadsheetCellRange(SpreadsheetCellAddress address)
    {
      this = new SpreadsheetCellRange(address.Row, address.Column, address.Row, address.Column);
    }

    // Token: 0x060018EB RID: 6379 RVA: 0x003CDED4 File Offset: 0x003CDED4
    public static SpreadsheetCellRange Spanning(SpreadsheetCellAddress first, SpreadsheetCellAddress second)
    {
      return new SpreadsheetCellRange(first.Row, first.Column, second.Row, second.Column);
    }

    // Token: 0x060018EC RID: 6380 RVA: 0x003CDEF8 File Offset: 0x003CDEF8
    [NullableContext(1)]
    public static SpreadsheetCellRange? Parse(string reference)
    {
      string[] array = reference.Split(':', StringSplitOptions.RemoveEmptyEntries);
      if (array.Length != 0)
      {
        SpreadsheetCellAddress? spreadsheetCellAddress = SpreadsheetCellAddress.Parse(array[0]);
        if (spreadsheetCellAddress != null)
        {
          SpreadsheetCellAddress valueOrDefault = spreadsheetCellAddress.GetValueOrDefault();
          SpreadsheetCellAddress? spreadsheetCellAddress2 = ((array.Length > 1) ? SpreadsheetCellAddress.Parse(array[1]) : new SpreadsheetCellAddress?(valueOrDefault));
          if (spreadsheetCellAddress2 != null)
          {
            SpreadsheetCellAddress valueOrDefault2 = spreadsheetCellAddress2.GetValueOrDefault();
            return new SpreadsheetCellRange?(SpreadsheetCellRange.Spanning(valueOrDefault, valueOrDefault2));
          }
          return null;
        }
      }
      return null;
    }

    // Token: 0x060018ED RID: 6381 RVA: 0x003CDF88 File Offset: 0x003CDF88
    public bool Contains(int row, int column)
    {
      return row >= this.FirstRow && row <= this.LastRow && column >= this.FirstColumn && column <= this.LastColumn;
    }

    // Token: 0x17000582 RID: 1410
    // (get) Token: 0x060018EE RID: 6382 RVA: 0x003CDFB4 File Offset: 0x003CDFB4
    public bool IsSingleCell
    {
      get
      {
        return this.FirstRow == this.LastRow && this.FirstColumn == this.LastColumn;
      }
    }

    // Token: 0x17000583 RID: 1411
    // (get) Token: 0x060018EF RID: 6383 RVA: 0x003CDFD4 File Offset: 0x003CDFD4
    [Nullable(1)]
    public string Reference
    {
      [NullableContext(1)]
      get
      {
        string reference = new SpreadsheetCellAddress(this.FirstRow, this.FirstColumn).Reference;
        if (this.IsSingleCell)
        {
          return reference;
        }
        return reference + ":" + new SpreadsheetCellAddress(this.LastRow, this.LastColumn).Reference;
      }
    }

    // Token: 0x060018F0 RID: 6384 RVA: 0x003CE02C File Offset: 0x003CE02C
    public SpreadsheetCellRange Union(SpreadsheetCellRange other)
    {
      return new SpreadsheetCellRange(Math.Min(this.FirstRow, other.FirstRow), Math.Min(this.FirstColumn, other.FirstColumn), Math.Max(this.LastRow, other.LastRow), Math.Max(this.LastColumn, other.LastColumn));
    }

    // Token: 0x060018F1 RID: 6385 RVA: 0x003CE088 File Offset: 0x003CE088
    public bool Intersects(SpreadsheetCellRange other)
    {
      return this.FirstRow <= other.LastRow && this.LastRow >= other.FirstRow && this.FirstColumn <= other.LastColumn && this.LastColumn >= other.FirstColumn;
    }

    // Token: 0x060018F2 RID: 6386 RVA: 0x003CE0D8 File Offset: 0x003CE0D8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SpreadsheetCellRange");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060018F3 RID: 6387 RVA: 0x003CE124 File Offset: 0x003CE124
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("FirstRow = ");
      builder.Append(this.FirstRow.ToString());
      builder.Append(", FirstColumn = ");
      builder.Append(this.FirstColumn.ToString());
      builder.Append(", LastRow = ");
      builder.Append(this.LastRow.ToString());
      builder.Append(", LastColumn = ");
      builder.Append(this.LastColumn.ToString());
      builder.Append(", IsSingleCell = ");
      builder.Append(this.IsSingleCell.ToString());
      builder.Append(", Reference = ");
      builder.Append(this.Reference);
      return true;
    }

    // Token: 0x060018F4 RID: 6388 RVA: 0x003CE210 File Offset: 0x003CE210
    [CompilerGenerated]
    public static bool operator !=(SpreadsheetCellRange left, SpreadsheetCellRange right)
    {
      return !(left == right);
    }

    // Token: 0x060018F5 RID: 6389 RVA: 0x003CE21C File Offset: 0x003CE21C
    [CompilerGenerated]
    public static bool operator ==(SpreadsheetCellRange left, SpreadsheetCellRange right)
    {
      return left.Equals(right);
    }

    // Token: 0x060018F6 RID: 6390 RVA: 0x003CE228 File Offset: 0x003CE228
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<int>.Default.GetHashCode(this.<FirstRow>k__BackingField) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<FirstColumn>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<LastRow>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<LastColumn>k__BackingField);
    }

    // Token: 0x060018F7 RID: 6391 RVA: 0x003CE28C File Offset: 0x003CE28C
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is SpreadsheetCellRange && this.Equals((SpreadsheetCellRange)obj);
    }

    // Token: 0x060018F8 RID: 6392 RVA: 0x003CE2A4 File Offset: 0x003CE2A4
    [CompilerGenerated]
    public bool Equals(SpreadsheetCellRange other)
    {
      return EqualityComparer<int>.Default.Equals(this.<FirstRow>k__BackingField, other.<FirstRow>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<FirstColumn>k__BackingField, other.<FirstColumn>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<LastRow>k__BackingField, other.<LastRow>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<LastColumn>k__BackingField, other.<LastColumn>k__BackingField);
    }
  }
}
