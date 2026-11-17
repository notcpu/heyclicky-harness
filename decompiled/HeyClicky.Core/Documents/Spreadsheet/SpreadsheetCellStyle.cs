using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001A7 RID: 423
  public struct SpreadsheetCellStyle
  {
    // Token: 0x06001908 RID: 6408 RVA: 0x003CE520 File Offset: 0x003CE520
    public SpreadsheetCellStyle()
    {
      this.IsBold = false;
      this.IsItalic = false;
      this.IsUnderlined = false;
      this.IsStruckThrough = false;
      this.FontSize = 12.5;
      this.TextColor = null;
      this.FillColor = null;
      this.HorizontalAlignment = SpreadsheetHorizontalAlignment.General;
      this.VerticalAlignment = SpreadsheetVerticalAlignment.Bottom;
      this.WrapsText = false;
      this.IndentLevel = 0;
      this.LeftBorder = null;
      this.RightBorder = null;
      this.TopBorder = null;
      this.BottomBorder = null;
      this.NumberFormatCode = "General";
    }

    // Token: 0x17000587 RID: 1415
    // (get) Token: 0x06001909 RID: 6409 RVA: 0x003CE5C8 File Offset: 0x003CE5C8
    public readonly bool HasBorders
    {
      get
      {
        SpreadsheetBorderEdge? spreadsheetBorderEdge = this.LeftBorder;
        if (spreadsheetBorderEdge == null)
        {
          spreadsheetBorderEdge = this.RightBorder;
          if (spreadsheetBorderEdge == null)
          {
            spreadsheetBorderEdge = this.TopBorder;
            if (spreadsheetBorderEdge == null)
            {
              spreadsheetBorderEdge = this.BottomBorder;
              return spreadsheetBorderEdge != null;
            }
          }
        }
        return true;
      }
    }

    // Token: 0x04000A92 RID: 2706
    public bool IsBold;

    // Token: 0x04000A93 RID: 2707
    public bool IsItalic;

    // Token: 0x04000A94 RID: 2708
    public bool IsUnderlined;

    // Token: 0x04000A95 RID: 2709
    public bool IsStruckThrough;

    // Token: 0x04000A96 RID: 2710
    public double FontSize;

    // Token: 0x04000A97 RID: 2711
    public SpreadsheetColor? TextColor;

    // Token: 0x04000A98 RID: 2712
    public SpreadsheetColor? FillColor;

    // Token: 0x04000A99 RID: 2713
    public SpreadsheetHorizontalAlignment HorizontalAlignment;

    // Token: 0x04000A9A RID: 2714
    public SpreadsheetVerticalAlignment VerticalAlignment;

    // Token: 0x04000A9B RID: 2715
    public bool WrapsText;

    // Token: 0x04000A9C RID: 2716
    public int IndentLevel;

    // Token: 0x04000A9D RID: 2717
    public SpreadsheetBorderEdge? LeftBorder;

    // Token: 0x04000A9E RID: 2718
    public SpreadsheetBorderEdge? RightBorder;

    // Token: 0x04000A9F RID: 2719
    public SpreadsheetBorderEdge? TopBorder;

    // Token: 0x04000AA0 RID: 2720
    public SpreadsheetBorderEdge? BottomBorder;

    // Token: 0x04000AA1 RID: 2721
    [Nullable(1)]
    public string NumberFormatCode;
  }
}
