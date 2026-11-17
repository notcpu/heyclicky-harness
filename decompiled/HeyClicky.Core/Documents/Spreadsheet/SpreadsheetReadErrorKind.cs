using System;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001B4 RID: 436
  public enum SpreadsheetReadErrorKind
  {
    // Token: 0x04000AE2 RID: 2786
    NotAZipArchive,
    // Token: 0x04000AE3 RID: 2787
    UnsupportedEntry,
    // Token: 0x04000AE4 RID: 2788
    MissingPart,
    // Token: 0x04000AE5 RID: 2789
    TooLarge,
    // Token: 0x04000AE6 RID: 2790
    Unreadable
  }
}
