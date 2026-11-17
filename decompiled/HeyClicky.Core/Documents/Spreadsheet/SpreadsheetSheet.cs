using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001AA RID: 426
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SpreadsheetSheet
  {
    // Token: 0x1700058E RID: 1422
    // (get) Token: 0x06001920 RID: 6432 RVA: 0x003CE9B4 File Offset: 0x003CE9B4
    public string Name { get; }

    // Token: 0x1700058F RID: 1423
    // (get) Token: 0x06001921 RID: 6433 RVA: 0x003CE9BC File Offset: 0x003CE9BC
    public IReadOnlyDictionary<int, Dictionary<int, SpreadsheetCell>> CellsByRow { get; }

    // Token: 0x17000590 RID: 1424
    // (get) Token: 0x06001922 RID: 6434 RVA: 0x003CE9C4 File Offset: 0x003CE9C4
    public int LastDataRow { get; }

    // Token: 0x17000591 RID: 1425
    // (get) Token: 0x06001923 RID: 6435 RVA: 0x003CE9CC File Offset: 0x003CE9CC
    public int LastDataColumn { get; }

    // Token: 0x17000592 RID: 1426
    // (get) Token: 0x06001924 RID: 6436 RVA: 0x003CE9D4 File Offset: 0x003CE9D4
    public IReadOnlyDictionary<int, double> CustomColumnWidths { get; }

    // Token: 0x17000593 RID: 1427
    // (get) Token: 0x06001925 RID: 6437 RVA: 0x003CE9DC File Offset: 0x003CE9DC
    public double DefaultColumnWidth { get; }

    // Token: 0x17000594 RID: 1428
    // (get) Token: 0x06001926 RID: 6438 RVA: 0x003CE9E4 File Offset: 0x003CE9E4
    public IReadOnlyDictionary<int, double> CustomRowHeights { get; }

    // Token: 0x17000595 RID: 1429
    // (get) Token: 0x06001927 RID: 6439 RVA: 0x003CE9EC File Offset: 0x003CE9EC
    public double DefaultRowHeight { get; }

    // Token: 0x17000596 RID: 1430
    // (get) Token: 0x06001928 RID: 6440 RVA: 0x003CE9F4 File Offset: 0x003CE9F4
    public IReadOnlySet<int> HiddenRows { get; }

    // Token: 0x17000597 RID: 1431
    // (get) Token: 0x06001929 RID: 6441 RVA: 0x003CE9FC File Offset: 0x003CE9FC
    public IReadOnlySet<int> HiddenColumns { get; }

    // Token: 0x17000598 RID: 1432
    // (get) Token: 0x0600192A RID: 6442 RVA: 0x003CEA04 File Offset: 0x003CEA04
    public IReadOnlyList<SpreadsheetCellRange> MergedRanges { get; }

    // Token: 0x17000599 RID: 1433
    // (get) Token: 0x0600192B RID: 6443 RVA: 0x003CEA0C File Offset: 0x003CEA0C
    public int FrozenRowCount { get; }

    // Token: 0x1700059A RID: 1434
    // (get) Token: 0x0600192C RID: 6444 RVA: 0x003CEA14 File Offset: 0x003CEA14
    public int FrozenColumnCount { get; }

    // Token: 0x1700059B RID: 1435
    // (get) Token: 0x0600192D RID: 6445 RVA: 0x003CEA1C File Offset: 0x003CEA1C
    public bool ShowsGridLines { get; }

    // Token: 0x1700059C RID: 1436
    // (get) Token: 0x0600192E RID: 6446 RVA: 0x003CEA24 File Offset: 0x003CEA24
    public IReadOnlyDictionary<SpreadsheetCellAddress, string> Hyperlinks { get; }

    // Token: 0x1700059D RID: 1437
    // (get) Token: 0x0600192F RID: 6447 RVA: 0x003CEA2C File Offset: 0x003CEA2C
    public IReadOnlyList<SpreadsheetCellStyle> Styles { get; }

    // Token: 0x06001930 RID: 6448 RVA: 0x003CEA34 File Offset: 0x003CEA34
    public unsafe SpreadsheetSheet(string name, Dictionary<int, Dictionary<int, SpreadsheetCell>> cellsByRow, Dictionary<int, double> customColumnWidths, double defaultColumnWidth, Dictionary<int, double> customRowHeights, double defaultRowHeight, HashSet<int> hiddenRows, HashSet<int> hiddenColumns, List<SpreadsheetCellRange> mergedRanges, int frozenRowCount, int frozenColumnCount, bool showsGridLines, Dictionary<SpreadsheetCellAddress, string> hyperlinks, List<SpreadsheetCellStyle> styles)
    {
      this.Name = name;
      this.CellsByRow = cellsByRow;
      this.CustomColumnWidths = customColumnWidths;
      this.DefaultColumnWidth = defaultColumnWidth;
      this.CustomRowHeights = customRowHeights;
      this.DefaultRowHeight = defaultRowHeight;
      this.HiddenRows = hiddenRows;
      this.HiddenColumns = hiddenColumns;
      this.MergedRanges = mergedRanges;
      this.FrozenRowCount = frozenRowCount;
      this.FrozenColumnCount = frozenColumnCount;
      this.ShowsGridLines = showsGridLines;
      this.Hyperlinks = hyperlinks;
      IReadOnlyList<SpreadsheetCellStyle> readOnlyList;
      if (styles.Count != 0)
      {
        readOnlyList = styles;
      }
      else
      {
        int num = 1;
        List<SpreadsheetCellStyle> list = new List<SpreadsheetCellStyle>(num);
        CollectionsMarshal.SetCount<SpreadsheetCellStyle>(list, num);
        readOnlyList = list;
        Span<SpreadsheetCellStyle> span = CollectionsMarshal.AsSpan<SpreadsheetCellStyle>(list);
        int num2 = 0;
        *span[num2] = new SpreadsheetCellStyle();
      }
      this.Styles = readOnlyList;
      int num3 = -1;
      int num4 = -1;
      foreach (KeyValuePair<int, Dictionary<int, SpreadsheetCell>> keyValuePair in cellsByRow)
      {
        int num2;
        Dictionary<int, SpreadsheetCell> dictionary;
        keyValuePair.Deconstruct(out num2, out dictionary);
        int num5 = num2;
        Dictionary<int, SpreadsheetCell> dictionary2 = dictionary;
        if (dictionary2.Count != 0)
        {
          num3 = Math.Max(num3, num5);
          num4 = Math.Max(num4, dictionary2.Keys.Max());
        }
      }
      foreach (SpreadsheetCellRange spreadsheetCellRange in mergedRanges)
      {
        num3 = Math.Max(num3, spreadsheetCellRange.LastRow);
        num4 = Math.Max(num4, spreadsheetCellRange.LastColumn);
      }
      this.LastDataRow = num3;
      this.LastDataColumn = num4;
    }

    // Token: 0x06001931 RID: 6449 RVA: 0x003CEBC8 File Offset: 0x003CEBC8
    [NullableContext(2)]
    public SpreadsheetCell Cell(int row, int column)
    {
      Dictionary<int, SpreadsheetCell> dictionary;
      SpreadsheetCell spreadsheetCell;
      if (!this.CellsByRow.TryGetValue(row, out dictionary) || !dictionary.TryGetValue(column, out spreadsheetCell))
      {
        return null;
      }
      return spreadsheetCell;
    }

    // Token: 0x06001932 RID: 6450 RVA: 0x003CEBF4 File Offset: 0x003CEBF4
    public SpreadsheetCellStyle Style(int index)
    {
      if (index < 0 || index >= this.Styles.Count)
      {
        return this.Styles[0];
      }
      return this.Styles[index];
    }
  }
}
