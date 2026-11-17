using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001B5 RID: 437
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class SpreadsheetReadException : Exception
  {
    // Token: 0x06001961 RID: 6497 RVA: 0x003D1284 File Offset: 0x003D1284
    public SpreadsheetReadException(SpreadsheetReadErrorKind kind, string part = null)
    {
      string text;
      if (part != null)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
        defaultInterpolatedStringHandler.AppendFormatted<SpreadsheetReadErrorKind>(kind);
        defaultInterpolatedStringHandler.AppendLiteral("(");
        defaultInterpolatedStringHandler.AppendFormatted(part);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        text = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text = kind.ToString();
      }
      base..ctor(text);
    }

    // Token: 0x170005A4 RID: 1444
    // (get) Token: 0x06001962 RID: 6498 RVA: 0x003D12F0 File Offset: 0x003D12F0
    public SpreadsheetReadErrorKind Kind { get; } = kind;

    // Token: 0x170005A5 RID: 1445
    // (get) Token: 0x06001963 RID: 6499 RVA: 0x003D12F8 File Offset: 0x003D12F8
    public string Part { get; } = part;
  }
}
