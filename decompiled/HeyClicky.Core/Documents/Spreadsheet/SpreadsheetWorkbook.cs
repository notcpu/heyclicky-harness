using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001AB RID: 427
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SpreadsheetWorkbook
  {
    // Token: 0x1700059E RID: 1438
    // (get) Token: 0x06001933 RID: 6451 RVA: 0x003CEC24 File Offset: 0x003CEC24
    public IReadOnlyList<SpreadsheetSheet> Sheets { get; }

    // Token: 0x1700059F RID: 1439
    // (get) Token: 0x06001934 RID: 6452 RVA: 0x003CEC2C File Offset: 0x003CEC2C
    public int InitialSheetIndex { get; }

    // Token: 0x06001935 RID: 6453 RVA: 0x003CEC34 File Offset: 0x003CEC34
    public SpreadsheetWorkbook(IReadOnlyList<SpreadsheetSheet> sheets, int initialSheetIndex)
    {
      this.Sheets = sheets;
      this.InitialSheetIndex = ((initialSheetIndex >= 0 && initialSheetIndex < sheets.Count) ? initialSheetIndex : 0);
    }

    // Token: 0x06001936 RID: 6454 RVA: 0x003CEC5C File Offset: 0x003CEC5C
    public static bool CanRead(string path)
    {
      return SpreadsheetWorkbook.SupportedPathExtensions.Contains(Path.GetExtension(path).TrimStart('.').ToLowerInvariant());
    }

    // Token: 0x06001937 RID: 6455 RVA: 0x003CEC7C File Offset: 0x003CEC7C
    [return: Nullable(2)]
    public static SpreadsheetWorkbook Read(string path)
    {
      string text = Path.GetExtension(path).TrimStart('.').ToLowerInvariant();
      SpreadsheetWorkbook spreadsheetWorkbook2;
      try
      {
        SpreadsheetWorkbook spreadsheetWorkbook;
        if (!(text == "xlsx") && !(text == "xlsm"))
        {
          if (!(text == "csv") && !(text == "tsv"))
          {
            spreadsheetWorkbook = null;
          }
          else
          {
            spreadsheetWorkbook = SpreadsheetDelimitedTextReader.Read(path, text == "tsv");
          }
        }
        else
        {
          spreadsheetWorkbook = SpreadsheetXlsxReader.Read(path);
        }
        spreadsheetWorkbook2 = spreadsheetWorkbook;
      }
      catch (Exception ex)
      {
        Log.Info("spreadsheet", "couldn't read " + Path.GetFileName(path) + ": " + ex.Message);
        spreadsheetWorkbook2 = null;
      }
      return spreadsheetWorkbook2;
    }

    // Token: 0x04000ABE RID: 2750
    public static readonly IReadOnlySet<string> SupportedPathExtensions = new HashSet<string> { "xlsx", "xlsm", "csv", "tsv" };
  }
}
