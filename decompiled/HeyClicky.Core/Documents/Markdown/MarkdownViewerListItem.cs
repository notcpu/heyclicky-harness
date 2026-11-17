using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Documents.Markdown
{
  // Token: 0x020001BB RID: 443
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MarkdownViewerListItem : IEquatable<MarkdownViewerListItem>
  {
    // Token: 0x060019A5 RID: 6565 RVA: 0x003D354C File Offset: 0x003D354C
    public MarkdownViewerListItem(int IndentLevel, [Nullable(2)] string OrdinalLabel, bool? CheckboxState, string InlineMarkdown)
    {
      this.IndentLevel = IndentLevel;
      this.OrdinalLabel = OrdinalLabel;
      this.CheckboxState = CheckboxState;
      this.InlineMarkdown = InlineMarkdown;
      base..ctor();
    }

    // Token: 0x170005AF RID: 1455
    // (get) Token: 0x060019A6 RID: 6566 RVA: 0x003D3574 File Offset: 0x003D3574
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(MarkdownViewerListItem);
      }
    }

    // Token: 0x170005B0 RID: 1456
    // (get) Token: 0x060019A7 RID: 6567 RVA: 0x003D3580 File Offset: 0x003D3580
    // (set) Token: 0x060019A8 RID: 6568 RVA: 0x003D3588 File Offset: 0x003D3588
    public int IndentLevel { get; set; }

    // Token: 0x170005B1 RID: 1457
    // (get) Token: 0x060019A9 RID: 6569 RVA: 0x003D3594 File Offset: 0x003D3594
    // (set) Token: 0x060019AA RID: 6570 RVA: 0x003D359C File Offset: 0x003D359C
    [Nullable(2)]
    public string OrdinalLabel
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170005B2 RID: 1458
    // (get) Token: 0x060019AB RID: 6571 RVA: 0x003D35A8 File Offset: 0x003D35A8
    // (set) Token: 0x060019AC RID: 6572 RVA: 0x003D35B0 File Offset: 0x003D35B0
    public bool? CheckboxState { get; set; }

    // Token: 0x170005B3 RID: 1459
    // (get) Token: 0x060019AD RID: 6573 RVA: 0x003D35BC File Offset: 0x003D35BC
    // (set) Token: 0x060019AE RID: 6574 RVA: 0x003D35C4 File Offset: 0x003D35C4
    public string InlineMarkdown { get; set; }

    // Token: 0x060019AF RID: 6575 RVA: 0x003D35D0 File Offset: 0x003D35D0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("MarkdownViewerListItem");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060019B0 RID: 6576 RVA: 0x003D361C File Offset: 0x003D361C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("IndentLevel = ");
      builder.Append(this.IndentLevel.ToString());
      builder.Append(", OrdinalLabel = ");
      builder.Append(this.OrdinalLabel);
      builder.Append(", CheckboxState = ");
      builder.Append(this.CheckboxState.ToString());
      builder.Append(", InlineMarkdown = ");
      builder.Append(this.InlineMarkdown);
      return true;
    }

    // Token: 0x060019B1 RID: 6577 RVA: 0x003D36B0 File Offset: 0x003D36B0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(MarkdownViewerListItem left, MarkdownViewerListItem right)
    {
      return !(left == right);
    }

    // Token: 0x060019B2 RID: 6578 RVA: 0x003D36BC File Offset: 0x003D36BC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(MarkdownViewerListItem left, MarkdownViewerListItem right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060019B3 RID: 6579 RVA: 0x003D36D0 File Offset: 0x003D36D0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<IndentLevel>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<OrdinalLabel>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<CheckboxState>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<InlineMarkdown>k__BackingField);
    }

    // Token: 0x060019B4 RID: 6580 RVA: 0x003D374C File Offset: 0x003D374C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as MarkdownViewerListItem);
    }

    // Token: 0x060019B5 RID: 6581 RVA: 0x003D375C File Offset: 0x003D375C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(MarkdownViewerListItem other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<IndentLevel>k__BackingField, other.<IndentLevel>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<OrdinalLabel>k__BackingField, other.<OrdinalLabel>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<CheckboxState>k__BackingField, other.<CheckboxState>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<InlineMarkdown>k__BackingField, other.<InlineMarkdown>k__BackingField));
    }

    // Token: 0x060019B7 RID: 6583 RVA: 0x003D37F0 File Offset: 0x003D37F0
    [CompilerGenerated]
    private MarkdownViewerListItem(MarkdownViewerListItem original)
    {
      this.IndentLevel = original.<IndentLevel>k__BackingField;
      this.OrdinalLabel = original.<OrdinalLabel>k__BackingField;
      this.CheckboxState = original.<CheckboxState>k__BackingField;
      this.InlineMarkdown = original.<InlineMarkdown>k__BackingField;
    }

    // Token: 0x060019B8 RID: 6584 RVA: 0x003D3828 File Offset: 0x003D3828
    [CompilerGenerated]
    public void Deconstruct(out int IndentLevel, [Nullable(2)] out string OrdinalLabel, out bool? CheckboxState, out string InlineMarkdown)
    {
      IndentLevel = this.IndentLevel;
      OrdinalLabel = this.OrdinalLabel;
      CheckboxState = this.CheckboxState;
      InlineMarkdown = this.InlineMarkdown;
    }
  }
}
