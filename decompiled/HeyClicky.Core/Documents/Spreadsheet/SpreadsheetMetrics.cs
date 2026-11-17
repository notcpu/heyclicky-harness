using System;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001AC RID: 428
  public static class SpreadsheetMetrics
  {
    // Token: 0x06001939 RID: 6457 RVA: 0x003CED6C File Offset: 0x003CED6C
    public static double Points(double excelColumnWidthCharacters)
    {
      if (excelColumnWidthCharacters <= 0.0)
      {
        return 0.0;
      }
      return Math.Round(excelColumnWidthCharacters * 7.4 + 5.0, MidpointRounding.AwayFromZero);
    }

    // Token: 0x04000ABF RID: 2751
    public const double DefaultFontSize = 12.5;

    // Token: 0x04000AC0 RID: 2752
    public const double FontPointScale = 1.17;

    // Token: 0x04000AC1 RID: 2753
    public const double RowHeightScale = 1.3333333333333333;

    // Token: 0x04000AC2 RID: 2754
    public const double DefaultRowHeight = 21.0;

    // Token: 0x04000AC3 RID: 2755
    public const double MinimumRowHeight = 4.0;

    // Token: 0x04000AC4 RID: 2756
    public const double DefaultColumnWidth = 100.0;

    // Token: 0x04000AC5 RID: 2757
    public const double ColumnHeaderHeight = 22.0;

    // Token: 0x04000AC6 RID: 2758
    public const double CellHorizontalPadding = 4.0;

    // Token: 0x04000AC7 RID: 2759
    public const double IndentWidth = 9.0;
  }
}
