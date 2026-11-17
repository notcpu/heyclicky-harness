using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x0200019C RID: 412
  [NullableContext(1)]
  public interface ISpreadsheetTextMeasurer
  {
    // Token: 0x06001873 RID: 6259
    double SingleLineWidth(string text, SpreadsheetCellStyle style);

    // Token: 0x06001874 RID: 6260
    double WrappedHeight(string text, SpreadsheetCellStyle style, double width);
  }
}
