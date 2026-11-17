using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.Core.Documents.Spreadsheet;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002DC RID: 732
  public readonly struct SpreadsheetSelection : IEquatable<SpreadsheetSelection>
  {
    // Token: 0x06001ED6 RID: 7894 RVA: 0x00609DB0 File Offset: 0x00609DB0
    public SpreadsheetSelection(SpreadsheetCellAddress Anchor, SpreadsheetCellAddress Extent, SpreadsheetCellRange Range)
    {
      this.Anchor = Anchor;
      this.Extent = Extent;
      this.Range = Range;
    }

    // Token: 0x1700058B RID: 1419
    // (get) Token: 0x06001ED7 RID: 7895 RVA: 0x00609DC8 File Offset: 0x00609DC8
    // (set) Token: 0x06001ED8 RID: 7896 RVA: 0x00609DD0 File Offset: 0x00609DD0
    public SpreadsheetCellAddress Anchor { get; set; }

    // Token: 0x1700058C RID: 1420
    // (get) Token: 0x06001ED9 RID: 7897 RVA: 0x00609DDC File Offset: 0x00609DDC
    // (set) Token: 0x06001EDA RID: 7898 RVA: 0x00609DE4 File Offset: 0x00609DE4
    public SpreadsheetCellAddress Extent { get; set; }

    // Token: 0x1700058D RID: 1421
    // (get) Token: 0x06001EDB RID: 7899 RVA: 0x00609DF0 File Offset: 0x00609DF0
    // (set) Token: 0x06001EDC RID: 7900 RVA: 0x00609DF8 File Offset: 0x00609DF8
    public SpreadsheetCellRange Range { get; set; }

    // Token: 0x06001EDD RID: 7901 RVA: 0x00609E04 File Offset: 0x00609E04
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SpreadsheetSelection");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001EDE RID: 7902 RVA: 0x00609E50 File Offset: 0x00609E50
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Anchor = ");
      builder.Append(this.Anchor.ToString());
      builder.Append(", Extent = ");
      builder.Append(this.Extent.ToString());
      builder.Append(", Range = ");
      builder.Append(this.Range.ToString());
      return true;
    }

    // Token: 0x06001EDF RID: 7903 RVA: 0x00609ED4 File Offset: 0x00609ED4
    [CompilerGenerated]
    public static bool operator !=(SpreadsheetSelection left, SpreadsheetSelection right)
    {
      return !(left == right);
    }

    // Token: 0x06001EE0 RID: 7904 RVA: 0x00609EE0 File Offset: 0x00609EE0
    [CompilerGenerated]
    public static bool operator ==(SpreadsheetSelection left, SpreadsheetSelection right)
    {
      return left.Equals(right);
    }

    // Token: 0x06001EE1 RID: 7905 RVA: 0x00609EEC File Offset: 0x00609EEC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<SpreadsheetCellAddress>.Default.GetHashCode(this.<Anchor>k__BackingField) * -1521134295 + EqualityComparer<SpreadsheetCellAddress>.Default.GetHashCode(this.<Extent>k__BackingField)) * -1521134295 + EqualityComparer<SpreadsheetCellRange>.Default.GetHashCode(this.<Range>k__BackingField);
    }

    // Token: 0x06001EE2 RID: 7906 RVA: 0x00609F2C File Offset: 0x00609F2C
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is SpreadsheetSelection && this.Equals((SpreadsheetSelection)obj);
    }

    // Token: 0x06001EE3 RID: 7907 RVA: 0x00609F44 File Offset: 0x00609F44
    [CompilerGenerated]
    public bool Equals(SpreadsheetSelection other)
    {
      return EqualityComparer<SpreadsheetCellAddress>.Default.Equals(this.<Anchor>k__BackingField, other.<Anchor>k__BackingField) && EqualityComparer<SpreadsheetCellAddress>.Default.Equals(this.<Extent>k__BackingField, other.<Extent>k__BackingField) && EqualityComparer<SpreadsheetCellRange>.Default.Equals(this.<Range>k__BackingField, other.<Range>k__BackingField);
    }

    // Token: 0x06001EE4 RID: 7908 RVA: 0x00609F9C File Offset: 0x00609F9C
    [CompilerGenerated]
    public void Deconstruct(out SpreadsheetCellAddress Anchor, out SpreadsheetCellAddress Extent, out SpreadsheetCellRange Range)
    {
      Anchor = this.Anchor;
      Extent = this.Extent;
      Range = this.Range;
    }
  }
}
