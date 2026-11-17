using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002E0 RID: 736
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SpreadsheetSelectionSummary : IEquatable<SpreadsheetSelectionSummary>
  {
    // Token: 0x06001F17 RID: 7959 RVA: 0x0060C664 File Offset: 0x0060C664
    public SpreadsheetSelectionSummary(string NameBoxText = "", string FormulaBarText = "", [Nullable(2)] string Hyperlink = null, [Nullable(2)] string Statistics = null)
    {
      this.NameBoxText = NameBoxText;
      this.FormulaBarText = FormulaBarText;
      this.Hyperlink = Hyperlink;
      this.Statistics = Statistics;
      base..ctor();
    }

    // Token: 0x1700059B RID: 1435
    // (get) Token: 0x06001F18 RID: 7960 RVA: 0x0060C68C File Offset: 0x0060C68C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(SpreadsheetSelectionSummary);
      }
    }

    // Token: 0x1700059C RID: 1436
    // (get) Token: 0x06001F19 RID: 7961 RVA: 0x0060C698 File Offset: 0x0060C698
    // (set) Token: 0x06001F1A RID: 7962 RVA: 0x0060C6A0 File Offset: 0x0060C6A0
    public string NameBoxText { get; set; }

    // Token: 0x1700059D RID: 1437
    // (get) Token: 0x06001F1B RID: 7963 RVA: 0x0060C6AC File Offset: 0x0060C6AC
    // (set) Token: 0x06001F1C RID: 7964 RVA: 0x0060C6B4 File Offset: 0x0060C6B4
    public string FormulaBarText { get; set; }

    // Token: 0x1700059E RID: 1438
    // (get) Token: 0x06001F1D RID: 7965 RVA: 0x0060C6C0 File Offset: 0x0060C6C0
    // (set) Token: 0x06001F1E RID: 7966 RVA: 0x0060C6C8 File Offset: 0x0060C6C8
    [Nullable(2)]
    public string Hyperlink
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700059F RID: 1439
    // (get) Token: 0x06001F1F RID: 7967 RVA: 0x0060C6D4 File Offset: 0x0060C6D4
    // (set) Token: 0x06001F20 RID: 7968 RVA: 0x0060C6DC File Offset: 0x0060C6DC
    [Nullable(2)]
    public string Statistics
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06001F21 RID: 7969 RVA: 0x0060C6E8 File Offset: 0x0060C6E8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SpreadsheetSelectionSummary");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001F22 RID: 7970 RVA: 0x0060C734 File Offset: 0x0060C734
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("NameBoxText = ");
      builder.Append(this.NameBoxText);
      builder.Append(", FormulaBarText = ");
      builder.Append(this.FormulaBarText);
      builder.Append(", Hyperlink = ");
      builder.Append(this.Hyperlink);
      builder.Append(", Statistics = ");
      builder.Append(this.Statistics);
      return true;
    }

    // Token: 0x06001F23 RID: 7971 RVA: 0x0060C7AC File Offset: 0x0060C7AC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(SpreadsheetSelectionSummary left, SpreadsheetSelectionSummary right)
    {
      return !(left == right);
    }

    // Token: 0x06001F24 RID: 7972 RVA: 0x0060C7B8 File Offset: 0x0060C7B8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(SpreadsheetSelectionSummary left, SpreadsheetSelectionSummary right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001F25 RID: 7973 RVA: 0x0060C7CC File Offset: 0x0060C7CC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NameBoxText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FormulaBarText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Hyperlink>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Statistics>k__BackingField);
    }

    // Token: 0x06001F26 RID: 7974 RVA: 0x0060C848 File Offset: 0x0060C848
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as SpreadsheetSelectionSummary);
    }

    // Token: 0x06001F27 RID: 7975 RVA: 0x0060C858 File Offset: 0x0060C858
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(SpreadsheetSelectionSummary other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<NameBoxText>k__BackingField, other.<NameBoxText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FormulaBarText>k__BackingField, other.<FormulaBarText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Hyperlink>k__BackingField, other.<Hyperlink>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Statistics>k__BackingField, other.<Statistics>k__BackingField));
    }

    // Token: 0x06001F29 RID: 7977 RVA: 0x0060C8EC File Offset: 0x0060C8EC
    [CompilerGenerated]
    private SpreadsheetSelectionSummary(SpreadsheetSelectionSummary original)
    {
      this.NameBoxText = original.<NameBoxText>k__BackingField;
      this.FormulaBarText = original.<FormulaBarText>k__BackingField;
      this.Hyperlink = original.<Hyperlink>k__BackingField;
      this.Statistics = original.<Statistics>k__BackingField;
    }

    // Token: 0x06001F2A RID: 7978 RVA: 0x0060C924 File Offset: 0x0060C924
    [CompilerGenerated]
    public void Deconstruct(out string NameBoxText, out string FormulaBarText, [Nullable(2)] out string Hyperlink, [Nullable(2)] out string Statistics)
    {
      NameBoxText = this.NameBoxText;
      FormulaBarText = this.FormulaBarText;
      Hyperlink = this.Hyperlink;
      Statistics = this.Statistics;
    }
  }
}
