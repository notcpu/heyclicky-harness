using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x0200019D RID: 413
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SpreadsheetGridLayout
  {
    // Token: 0x1700055D RID: 1373
    // (get) Token: 0x06001875 RID: 6261 RVA: 0x003CAEE8 File Offset: 0x003CAEE8
    public SpreadsheetSheet Sheet { get; }

    // Token: 0x1700055E RID: 1374
    // (get) Token: 0x06001876 RID: 6262 RVA: 0x003CAEF0 File Offset: 0x003CAEF0
    public int ColumnCount { get; }

    // Token: 0x1700055F RID: 1375
    // (get) Token: 0x06001877 RID: 6263 RVA: 0x003CAEF8 File Offset: 0x003CAEF8
    public int RowCount { get; }

    // Token: 0x17000560 RID: 1376
    // (get) Token: 0x06001878 RID: 6264 RVA: 0x003CAF00 File Offset: 0x003CAF00
    public int FrozenRowCount { get; }

    // Token: 0x17000561 RID: 1377
    // (get) Token: 0x06001879 RID: 6265 RVA: 0x003CAF08 File Offset: 0x003CAF08
    public int FrozenColumnCount { get; }

    // Token: 0x17000562 RID: 1378
    // (get) Token: 0x0600187A RID: 6266 RVA: 0x003CAF10 File Offset: 0x003CAF10
    public double ColumnHeaderHeight { get; } = 22.0;

    // Token: 0x17000563 RID: 1379
    // (get) Token: 0x0600187B RID: 6267 RVA: 0x003CAF18 File Offset: 0x003CAF18
    public double RowHeaderWidth { get; }

    // Token: 0x17000564 RID: 1380
    // (get) Token: 0x0600187C RID: 6268 RVA: 0x003CAF20 File Offset: 0x003CAF20
    public IReadOnlyList<double> ColumnWidths
    {
      get
      {
        return this.columnWidths;
      }
    }

    // Token: 0x17000565 RID: 1381
    // (get) Token: 0x0600187D RID: 6269 RVA: 0x003CAF28 File Offset: 0x003CAF28
    public IReadOnlyList<double> RowHeights
    {
      get
      {
        return this.rowHeights;
      }
    }

    // Token: 0x17000566 RID: 1382
    // (get) Token: 0x0600187E RID: 6270 RVA: 0x003CAF30 File Offset: 0x003CAF30
    public IReadOnlyList<double> ColumnOffsets
    {
      get
      {
        return this.columnOffsets;
      }
    }

    // Token: 0x17000567 RID: 1383
    // (get) Token: 0x0600187F RID: 6271 RVA: 0x003CAF38 File Offset: 0x003CAF38
    public IReadOnlyList<double> RowOffsets
    {
      get
      {
        return this.rowOffsets;
      }
    }

    // Token: 0x06001880 RID: 6272 RVA: 0x003CAF40 File Offset: 0x003CAF40
    public SpreadsheetGridLayout(SpreadsheetSheet sheet, ISpreadsheetTextMeasurer measurer)
    {
      this.Sheet = sheet;
      this.measurer = measurer;
      this.ColumnCount = Math.Max(sheet.LastDataColumn + 1 + 4, 26);
      this.RowCount = Math.Max(sheet.LastDataRow + 1 + 60, 100);
      this.FrozenRowCount = Math.Min(sheet.FrozenRowCount, Math.Max(0, this.RowCount - 1));
      this.FrozenColumnCount = Math.Min(sheet.FrozenColumnCount, Math.Max(0, this.ColumnCount - 1));
      int length = this.RowCount.ToString(CultureInfo.InvariantCulture).Length;
      this.RowHeaderWidth = (double)Math.Max(42, length * 8 + 18);
      this.columnWidths = new double[this.ColumnCount];
      for (int i = 0; i < this.ColumnCount; i++)
      {
        double num;
        this.columnWidths[i] = (sheet.HiddenColumns.Contains(i) ? 0.0 : (sheet.CustomColumnWidths.TryGetValue(i, out num) ? num : sheet.DefaultColumnWidth));
      }
      this.rowHeights = new double[this.RowCount];
      for (int j = 0; j < this.RowCount; j++)
      {
        double num2;
        this.rowHeights[j] = (sheet.HiddenRows.Contains(j) ? 0.0 : (sheet.CustomRowHeights.TryGetValue(j, out num2) ? num2 : sheet.DefaultRowHeight));
      }
      this.FitWrappedRowsWithoutAuthoredHeights();
      this.RebuildColumnOffsets();
      this.RebuildRowOffsets();
    }

    // Token: 0x17000568 RID: 1384
    // (get) Token: 0x06001881 RID: 6273 RVA: 0x003CB0F4 File Offset: 0x003CB0F4
    public double TotalWidth
    {
      get
      {
        double[] array = this.columnOffsets;
        return array[array.Length - 1];
      }
    }

    // Token: 0x17000569 RID: 1385
    // (get) Token: 0x06001882 RID: 6274 RVA: 0x003CB104 File Offset: 0x003CB104
    public double TotalHeight
    {
      get
      {
        double[] array = this.rowOffsets;
        return array[array.Length - 1];
      }
    }

    // Token: 0x1700056A RID: 1386
    // (get) Token: 0x06001883 RID: 6275 RVA: 0x003CB114 File Offset: 0x003CB114
    public double FrozenWidth
    {
      get
      {
        return this.columnOffsets[this.FrozenColumnCount];
      }
    }

    // Token: 0x1700056B RID: 1387
    // (get) Token: 0x06001884 RID: 6276 RVA: 0x003CB124 File Offset: 0x003CB124
    public double FrozenHeight
    {
      get
      {
        return this.rowOffsets[this.FrozenRowCount];
      }
    }

    // Token: 0x06001885 RID: 6277 RVA: 0x003CB134 File Offset: 0x003CB134
    public SpreadsheetRect CellRect(int row, int column)
    {
      return new SpreadsheetRect(this.columnOffsets[column], this.rowOffsets[row], this.columnWidths[column], this.rowHeights[row]);
    }

    // Token: 0x06001886 RID: 6278 RVA: 0x003CB15C File Offset: 0x003CB15C
    public SpreadsheetRect Rect(SpreadsheetCellRange range)
    {
      int num = Math.Min(range.LastRow, this.RowCount - 1);
      int num2 = Math.Min(range.LastColumn, this.ColumnCount - 1);
      return new SpreadsheetRect(this.columnOffsets[range.FirstColumn], this.rowOffsets[range.FirstRow], this.columnOffsets[num2 + 1] - this.columnOffsets[range.FirstColumn], this.rowOffsets[num + 1] - this.rowOffsets[range.FirstRow]);
    }

    // Token: 0x06001887 RID: 6279 RVA: 0x003CB1E8 File Offset: 0x003CB1E8
    public int Column(double x)
    {
      return SpreadsheetGridLayout.Index(this.columnOffsets, x);
    }

    // Token: 0x06001888 RID: 6280 RVA: 0x003CB1F8 File Offset: 0x003CB1F8
    public int Row(double y)
    {
      return SpreadsheetGridLayout.Index(this.rowOffsets, y);
    }

    // Token: 0x06001889 RID: 6281 RVA: 0x003CB208 File Offset: 0x003CB208
    public void SetColumnWidth(double width, int column)
    {
      if (column < 0 || column >= this.columnWidths.Length)
      {
        return;
      }
      this.columnWidths[column] = Math.Max(12.0, Math.Round(width, MidpointRounding.AwayFromZero));
      this.RebuildColumnOffsets();
    }

    // Token: 0x0600188A RID: 6282 RVA: 0x003CB240 File Offset: 0x003CB240
    public void SetRowHeight(double height, int row)
    {
      if (row < 0 || row >= this.rowHeights.Length)
      {
        return;
      }
      this.rowHeights[row] = Math.Max(10.0, Math.Round(height, MidpointRounding.AwayFromZero));
      this.RebuildRowOffsets();
    }

    // Token: 0x0600188B RID: 6283 RVA: 0x003CB278 File Offset: 0x003CB278
    public double FittingWidth(int column)
    {
      double num = 0.0;
      int num2 = 0;
      foreach (int num3 in this.Sheet.CellsByRow.Keys.OrderBy<int, int>((int r) => r))
      {
        if (num2 >= 3000)
        {
          break;
        }
        num2++;
        SpreadsheetCell spreadsheetCell = this.Sheet.Cell(num3, column);
        if (spreadsheetCell != null && spreadsheetCell.DisplayText.Length != 0)
        {
          SpreadsheetCellStyle style = this.Sheet.Style(spreadsheetCell.StyleIndex);
          double num4 = (from line in spreadsheetCell.DisplayText.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            select this.measurer.SingleLineWidth(line, style)).DefaultIfEmpty(0.0).Max();
          num = Math.Max(num, num4 + (double)style.IndentLevel * 9.0);
        }
      }
      return Math.Min(640.0, Math.Max(40.0, Math.Ceiling(num) + 8.0 + 6.0));
    }

    // Token: 0x0600188C RID: 6284 RVA: 0x003CB3E8 File Offset: 0x003CB3E8
    private void RebuildColumnOffsets()
    {
      double[] array = new double[this.columnWidths.Length + 1];
      for (int i = 0; i < this.columnWidths.Length; i++)
      {
        array[i + 1] = array[i] + this.columnWidths[i];
      }
      this.columnOffsets = array;
    }

    // Token: 0x0600188D RID: 6285 RVA: 0x003CB430 File Offset: 0x003CB430
    private void RebuildRowOffsets()
    {
      double[] array = new double[this.rowHeights.Length + 1];
      for (int i = 0; i < this.rowHeights.Length; i++)
      {
        array[i + 1] = array[i] + this.rowHeights[i];
      }
      this.rowOffsets = array;
    }

    // Token: 0x0600188E RID: 6286 RVA: 0x003CB478 File Offset: 0x003CB478
    private void FitWrappedRowsWithoutAuthoredHeights()
    {
      int num = 0;
      foreach (KeyValuePair<int, Dictionary<int, SpreadsheetCell>> keyValuePair in this.Sheet.CellsByRow)
      {
        int num2;
        Dictionary<int, SpreadsheetCell> dictionary;
        keyValuePair.Deconstruct(out num2, out dictionary);
        int num3 = num2;
        Dictionary<int, SpreadsheetCell> dictionary2 = dictionary;
        if (!this.Sheet.CustomRowHeights.ContainsKey(num3) && num3 >= 0 && num3 < this.rowHeights.Length && num < 5000 && this.rowHeights[num3] > 0.0)
        {
          num++;
          double num4 = this.rowHeights[num3];
          foreach (KeyValuePair<int, SpreadsheetCell> keyValuePair2 in dictionary2)
          {
            SpreadsheetCell spreadsheetCell;
            keyValuePair2.Deconstruct(out num2, out spreadsheetCell);
            int num5 = num2;
            SpreadsheetCell spreadsheetCell2 = spreadsheetCell;
            if (spreadsheetCell2.DisplayText.Length != 0 && num5 >= 0 && num5 < this.columnWidths.Length)
            {
              SpreadsheetCellStyle spreadsheetCellStyle = this.Sheet.Style(spreadsheetCell2.StyleIndex);
              if (spreadsheetCellStyle.WrapsText)
              {
                double num6 = this.columnWidths[num5] - 8.0;
                if (num6 > 4.0)
                {
                  double num7 = this.measurer.WrappedHeight(spreadsheetCell2.DisplayText, spreadsheetCellStyle, num6);
                  num4 = Math.Max(num4, Math.Min(Math.Ceiling(num7) + 7.0, 420.0));
                }
              }
            }
          }
          this.rowHeights[num3] = num4;
        }
      }
    }

    // Token: 0x0600188F RID: 6287 RVA: 0x003CB650 File Offset: 0x003CB650
    private static int Index(double[] offsets, double position)
    {
      int num = offsets.Length - 2;
      if (num < 0)
      {
        return 0;
      }
      if (position <= 0.0)
      {
        return 0;
      }
      if (position >= offsets[num + 1])
      {
        return num;
      }
      int i = 0;
      int num2 = num;
      while (i < num2)
      {
        int num3 = (i + num2 + 1) / 2;
        if (offsets[num3] <= position)
        {
          i = num3;
        }
        else
        {
          num2 = num3 - 1;
        }
      }
      return i;
    }

    // Token: 0x04000A6D RID: 2669
    private readonly ISpreadsheetTextMeasurer measurer;

    // Token: 0x04000A6E RID: 2670
    private readonly double[] columnWidths;

    // Token: 0x04000A6F RID: 2671
    private readonly double[] rowHeights;

    // Token: 0x04000A70 RID: 2672
    private double[] columnOffsets = Array.Empty<double>();

    // Token: 0x04000A71 RID: 2673
    private double[] rowOffsets = Array.Empty<double>();
  }
}
