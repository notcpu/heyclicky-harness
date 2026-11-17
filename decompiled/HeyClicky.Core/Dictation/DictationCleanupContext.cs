using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Dictation
{
  // Token: 0x020001C9 RID: 457
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DictationCleanupContext : IEquatable<DictationCleanupContext>
  {
    // Token: 0x06001A62 RID: 6754 RVA: 0x003D7374 File Offset: 0x003D7374
    public DictationCleanupContext(string AppName, [Nullable(2)] string SiteHost, string BeforeText, string SelectedText, string AfterText, bool SingleLine)
    {
      this.AppName = AppName;
      this.SiteHost = SiteHost;
      this.BeforeText = BeforeText;
      this.SelectedText = SelectedText;
      this.AfterText = AfterText;
      this.SingleLine = SingleLine;
      base..ctor();
    }

    // Token: 0x170005D7 RID: 1495
    // (get) Token: 0x06001A63 RID: 6755 RVA: 0x003D73AC File Offset: 0x003D73AC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(DictationCleanupContext);
      }
    }

    // Token: 0x170005D8 RID: 1496
    // (get) Token: 0x06001A64 RID: 6756 RVA: 0x003D73B8 File Offset: 0x003D73B8
    // (set) Token: 0x06001A65 RID: 6757 RVA: 0x003D73C0 File Offset: 0x003D73C0
    public string AppName { get; set; }

    // Token: 0x170005D9 RID: 1497
    // (get) Token: 0x06001A66 RID: 6758 RVA: 0x003D73CC File Offset: 0x003D73CC
    // (set) Token: 0x06001A67 RID: 6759 RVA: 0x003D73D4 File Offset: 0x003D73D4
    [Nullable(2)]
    public string SiteHost
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170005DA RID: 1498
    // (get) Token: 0x06001A68 RID: 6760 RVA: 0x003D73E0 File Offset: 0x003D73E0
    // (set) Token: 0x06001A69 RID: 6761 RVA: 0x003D73E8 File Offset: 0x003D73E8
    public string BeforeText { get; set; }

    // Token: 0x170005DB RID: 1499
    // (get) Token: 0x06001A6A RID: 6762 RVA: 0x003D73F4 File Offset: 0x003D73F4
    // (set) Token: 0x06001A6B RID: 6763 RVA: 0x003D73FC File Offset: 0x003D73FC
    public string SelectedText { get; set; }

    // Token: 0x170005DC RID: 1500
    // (get) Token: 0x06001A6C RID: 6764 RVA: 0x003D7408 File Offset: 0x003D7408
    // (set) Token: 0x06001A6D RID: 6765 RVA: 0x003D7410 File Offset: 0x003D7410
    public string AfterText { get; set; }

    // Token: 0x170005DD RID: 1501
    // (get) Token: 0x06001A6E RID: 6766 RVA: 0x003D741C File Offset: 0x003D741C
    // (set) Token: 0x06001A6F RID: 6767 RVA: 0x003D7424 File Offset: 0x003D7424
    public bool SingleLine { get; set; }

    // Token: 0x06001A70 RID: 6768 RVA: 0x003D7430 File Offset: 0x003D7430
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DictationCleanupContext");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001A71 RID: 6769 RVA: 0x003D747C File Offset: 0x003D747C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("AppName = ");
      builder.Append(this.AppName);
      builder.Append(", SiteHost = ");
      builder.Append(this.SiteHost);
      builder.Append(", BeforeText = ");
      builder.Append(this.BeforeText);
      builder.Append(", SelectedText = ");
      builder.Append(this.SelectedText);
      builder.Append(", AfterText = ");
      builder.Append(this.AfterText);
      builder.Append(", SingleLine = ");
      builder.Append(this.SingleLine.ToString());
      return true;
    }

    // Token: 0x06001A72 RID: 6770 RVA: 0x003D7534 File Offset: 0x003D7534
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(DictationCleanupContext left, DictationCleanupContext right)
    {
      return !(left == right);
    }

    // Token: 0x06001A73 RID: 6771 RVA: 0x003D7540 File Offset: 0x003D7540
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(DictationCleanupContext left, DictationCleanupContext right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001A74 RID: 6772 RVA: 0x003D7554 File Offset: 0x003D7554
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AppName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SiteHost>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BeforeText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SelectedText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AfterText>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<SingleLine>k__BackingField);
    }

    // Token: 0x06001A75 RID: 6773 RVA: 0x003D75FC File Offset: 0x003D75FC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DictationCleanupContext);
    }

    // Token: 0x06001A76 RID: 6774 RVA: 0x003D760C File Offset: 0x003D760C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(DictationCleanupContext other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<AppName>k__BackingField, other.<AppName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SiteHost>k__BackingField, other.<SiteHost>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BeforeText>k__BackingField, other.<BeforeText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SelectedText>k__BackingField, other.<SelectedText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AfterText>k__BackingField, other.<AfterText>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<SingleLine>k__BackingField, other.<SingleLine>k__BackingField));
    }

    // Token: 0x06001A78 RID: 6776 RVA: 0x003D76D8 File Offset: 0x003D76D8
    [CompilerGenerated]
    private DictationCleanupContext(DictationCleanupContext original)
    {
      this.AppName = original.<AppName>k__BackingField;
      this.SiteHost = original.<SiteHost>k__BackingField;
      this.BeforeText = original.<BeforeText>k__BackingField;
      this.SelectedText = original.<SelectedText>k__BackingField;
      this.AfterText = original.<AfterText>k__BackingField;
      this.SingleLine = original.<SingleLine>k__BackingField;
    }

    // Token: 0x06001A79 RID: 6777 RVA: 0x003D7734 File Offset: 0x003D7734
    [CompilerGenerated]
    public void Deconstruct(out string AppName, [Nullable(2)] out string SiteHost, out string BeforeText, out string SelectedText, out string AfterText, out bool SingleLine)
    {
      AppName = this.AppName;
      SiteHost = this.SiteHost;
      BeforeText = this.BeforeText;
      SelectedText = this.SelectedText;
      AfterText = this.AfterText;
      SingleLine = this.SingleLine;
    }
  }
}
