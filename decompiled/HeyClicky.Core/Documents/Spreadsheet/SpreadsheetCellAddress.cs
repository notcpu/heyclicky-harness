using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001A2 RID: 418
  public readonly struct SpreadsheetCellAddress : IEquatable<SpreadsheetCellAddress>
  {
    // Token: 0x060018D5 RID: 6357 RVA: 0x003CDB50 File Offset: 0x003CDB50
    public SpreadsheetCellAddress(int Row, int Column)
    {
      this.Row = Row;
      this.Column = Column;
    }

    // Token: 0x1700057B RID: 1403
    // (get) Token: 0x060018D6 RID: 6358 RVA: 0x003CDB60 File Offset: 0x003CDB60
    // (set) Token: 0x060018D7 RID: 6359 RVA: 0x003CDB68 File Offset: 0x003CDB68
    public int Row { get; set; }

    // Token: 0x1700057C RID: 1404
    // (get) Token: 0x060018D8 RID: 6360 RVA: 0x003CDB74 File Offset: 0x003CDB74
    // (set) Token: 0x060018D9 RID: 6361 RVA: 0x003CDB7C File Offset: 0x003CDB7C
    public int Column { get; set; }

    // Token: 0x1700057D RID: 1405
    // (get) Token: 0x060018DA RID: 6362 RVA: 0x003CDB88 File Offset: 0x003CDB88
    [Nullable(1)]
    public string Reference
    {
      [NullableContext(1)]
      get
      {
        return SpreadsheetCellAddress.ColumnLetters(this.Column) + (this.Row + 1).ToString(CultureInfo.InvariantCulture);
      }
    }

    // Token: 0x060018DB RID: 6363 RVA: 0x003CDBBC File Offset: 0x003CDBBC
    [NullableContext(1)]
    public static string ColumnLetters(int column)
    {
      int i = column + 1;
      string text = "";
      while (i > 0)
      {
        int num = (i - 1) % 26;
        char c = (char)(65 + num);
        text = new ReadOnlySpan<char>(ref c) + text;
        i = (i - 1) / 26;
      }
      return text;
    }

    // Token: 0x060018DC RID: 6364 RVA: 0x003CDC04 File Offset: 0x003CDC04
    public unsafe static SpreadsheetCellAddress? Parse(ReadOnlySpan<char> reference)
    {
      int num = 0;
      int num2 = 0;
      bool flag = false;
      bool flag2 = false;
      ReadOnlySpan<char> readOnlySpan = reference;
      int i = 0;
      while (i < readOnlySpan.Length)
      {
        char c = (char)(*readOnlySpan[i]);
        if (c >= 'A')
        {
          if (c >= 'a')
          {
            if (c > 'z')
            {
              goto IL_009A;
            }
            if (flag2)
            {
              return null;
            }
            num = num * 26 + (int)(c - '`');
            flag = true;
          }
          else
          {
            if (c > 'Z')
            {
              goto IL_009A;
            }
            if (flag2)
            {
              return null;
            }
            num = num * 26 + (int)(c - '@');
            flag = true;
          }
        }
        else if (c >= '0')
        {
          if (c > '9')
          {
            goto IL_009A;
          }
          num2 = num2 * 10 + (int)(c - '0');
          flag2 = true;
        }
        else if (c != '$')
        {
          goto IL_009A;
        }
        i++;
        continue;
        IL_009A:
        return null;
      }
      if (!flag || !flag2 || num2 <= 0)
      {
        return null;
      }
      return new SpreadsheetCellAddress?(new SpreadsheetCellAddress(num2 - 1, num - 1));
    }

    // Token: 0x060018DD RID: 6365 RVA: 0x003CDCF0 File Offset: 0x003CDCF0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SpreadsheetCellAddress");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060018DE RID: 6366 RVA: 0x003CDD3C File Offset: 0x003CDD3C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Row = ");
      builder.Append(this.Row.ToString());
      builder.Append(", Column = ");
      builder.Append(this.Column.ToString());
      builder.Append(", Reference = ");
      builder.Append(this.Reference);
      return true;
    }

    // Token: 0x060018DF RID: 6367 RVA: 0x003CDDB4 File Offset: 0x003CDDB4
    [CompilerGenerated]
    public static bool operator !=(SpreadsheetCellAddress left, SpreadsheetCellAddress right)
    {
      return !(left == right);
    }

    // Token: 0x060018E0 RID: 6368 RVA: 0x003CDDC0 File Offset: 0x003CDDC0
    [CompilerGenerated]
    public static bool operator ==(SpreadsheetCellAddress left, SpreadsheetCellAddress right)
    {
      return left.Equals(right);
    }

    // Token: 0x060018E1 RID: 6369 RVA: 0x003CDDCC File Offset: 0x003CDDCC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<int>.Default.GetHashCode(this.<Row>k__BackingField) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Column>k__BackingField);
    }

    // Token: 0x060018E2 RID: 6370 RVA: 0x003CDDF8 File Offset: 0x003CDDF8
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is SpreadsheetCellAddress && this.Equals((SpreadsheetCellAddress)obj);
    }

    // Token: 0x060018E3 RID: 6371 RVA: 0x003CDE10 File Offset: 0x003CDE10
    [CompilerGenerated]
    public bool Equals(SpreadsheetCellAddress other)
    {
      return EqualityComparer<int>.Default.Equals(this.<Row>k__BackingField, other.<Row>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Column>k__BackingField, other.<Column>k__BackingField);
    }

    // Token: 0x060018E4 RID: 6372 RVA: 0x003CDE44 File Offset: 0x003CDE44
    [CompilerGenerated]
    public void Deconstruct(out int Row, out int Column)
    {
      Row = this.Row;
      Column = this.Column;
    }
  }
}
