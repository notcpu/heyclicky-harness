using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001A9 RID: 425
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SpreadsheetCell : IEquatable<SpreadsheetCell>
  {
    // Token: 0x0600190A RID: 6410 RVA: 0x003CE61C File Offset: 0x003CE61C
    public SpreadsheetCell(string DisplayText, string FormulaBarText, SpreadsheetCellKind Kind, double? NumericValue, int StyleIndex)
    {
      this.DisplayText = DisplayText;
      this.FormulaBarText = FormulaBarText;
      this.Kind = Kind;
      this.NumericValue = NumericValue;
      this.StyleIndex = StyleIndex;
      base..ctor();
    }

    // Token: 0x17000588 RID: 1416
    // (get) Token: 0x0600190B RID: 6411 RVA: 0x003CE64C File Offset: 0x003CE64C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(SpreadsheetCell);
      }
    }

    // Token: 0x17000589 RID: 1417
    // (get) Token: 0x0600190C RID: 6412 RVA: 0x003CE658 File Offset: 0x003CE658
    // (set) Token: 0x0600190D RID: 6413 RVA: 0x003CE660 File Offset: 0x003CE660
    public string DisplayText { get; set; }

    // Token: 0x1700058A RID: 1418
    // (get) Token: 0x0600190E RID: 6414 RVA: 0x003CE66C File Offset: 0x003CE66C
    // (set) Token: 0x0600190F RID: 6415 RVA: 0x003CE674 File Offset: 0x003CE674
    public string FormulaBarText { get; set; }

    // Token: 0x1700058B RID: 1419
    // (get) Token: 0x06001910 RID: 6416 RVA: 0x003CE680 File Offset: 0x003CE680
    // (set) Token: 0x06001911 RID: 6417 RVA: 0x003CE688 File Offset: 0x003CE688
    public SpreadsheetCellKind Kind { get; set; }

    // Token: 0x1700058C RID: 1420
    // (get) Token: 0x06001912 RID: 6418 RVA: 0x003CE694 File Offset: 0x003CE694
    // (set) Token: 0x06001913 RID: 6419 RVA: 0x003CE69C File Offset: 0x003CE69C
    public double? NumericValue { get; set; }

    // Token: 0x1700058D RID: 1421
    // (get) Token: 0x06001914 RID: 6420 RVA: 0x003CE6A8 File Offset: 0x003CE6A8
    // (set) Token: 0x06001915 RID: 6421 RVA: 0x003CE6B0 File Offset: 0x003CE6B0
    public int StyleIndex { get; set; }

    // Token: 0x06001916 RID: 6422 RVA: 0x003CE6BC File Offset: 0x003CE6BC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SpreadsheetCell");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001917 RID: 6423 RVA: 0x003CE708 File Offset: 0x003CE708
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("DisplayText = ");
      builder.Append(this.DisplayText);
      builder.Append(", FormulaBarText = ");
      builder.Append(this.FormulaBarText);
      builder.Append(", Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", NumericValue = ");
      builder.Append(this.NumericValue.ToString());
      builder.Append(", StyleIndex = ");
      builder.Append(this.StyleIndex.ToString());
      return true;
    }

    // Token: 0x06001918 RID: 6424 RVA: 0x003CE7C4 File Offset: 0x003CE7C4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(SpreadsheetCell left, SpreadsheetCell right)
    {
      return !(left == right);
    }

    // Token: 0x06001919 RID: 6425 RVA: 0x003CE7D0 File Offset: 0x003CE7D0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(SpreadsheetCell left, SpreadsheetCell right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600191A RID: 6426 RVA: 0x003CE7E4 File Offset: 0x003CE7E4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FormulaBarText>k__BackingField)) * -1521134295 + EqualityComparer<SpreadsheetCellKind>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<NumericValue>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<StyleIndex>k__BackingField);
    }

    // Token: 0x0600191B RID: 6427 RVA: 0x003CE874 File Offset: 0x003CE874
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as SpreadsheetCell);
    }

    // Token: 0x0600191C RID: 6428 RVA: 0x003CE884 File Offset: 0x003CE884
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(SpreadsheetCell other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<DisplayText>k__BackingField, other.<DisplayText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FormulaBarText>k__BackingField, other.<FormulaBarText>k__BackingField) && EqualityComparer<SpreadsheetCellKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<double?>.Default.Equals(this.<NumericValue>k__BackingField, other.<NumericValue>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<StyleIndex>k__BackingField, other.<StyleIndex>k__BackingField));
    }

    // Token: 0x0600191E RID: 6430 RVA: 0x003CE934 File Offset: 0x003CE934
    [CompilerGenerated]
    private SpreadsheetCell(SpreadsheetCell original)
    {
      this.DisplayText = original.<DisplayText>k__BackingField;
      this.FormulaBarText = original.<FormulaBarText>k__BackingField;
      this.Kind = original.<Kind>k__BackingField;
      this.NumericValue = original.<NumericValue>k__BackingField;
      this.StyleIndex = original.<StyleIndex>k__BackingField;
    }

    // Token: 0x0600191F RID: 6431 RVA: 0x003CE984 File Offset: 0x003CE984
    [CompilerGenerated]
    public void Deconstruct(out string DisplayText, out string FormulaBarText, out SpreadsheetCellKind Kind, out double? NumericValue, out int StyleIndex)
    {
      DisplayText = this.DisplayText;
      FormulaBarText = this.FormulaBarText;
      Kind = this.Kind;
      NumericValue = this.NumericValue;
      StyleIndex = this.StyleIndex;
    }
  }
}
