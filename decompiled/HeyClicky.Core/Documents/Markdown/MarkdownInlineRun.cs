using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Documents.Markdown
{
  // Token: 0x020001B8 RID: 440
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MarkdownInlineRun : IEquatable<MarkdownInlineRun>
  {
    // Token: 0x06001976 RID: 6518 RVA: 0x003D2014 File Offset: 0x003D2014
    public MarkdownInlineRun(string Text, bool IsStrong = false, bool IsEmphasized = false, bool IsCode = false, bool IsStrikethrough = false, [Nullable(2)] string Link = null)
    {
      this.Text = Text;
      this.IsStrong = IsStrong;
      this.IsEmphasized = IsEmphasized;
      this.IsCode = IsCode;
      this.IsStrikethrough = IsStrikethrough;
      this.Link = Link;
      base..ctor();
    }

    // Token: 0x170005A7 RID: 1447
    // (get) Token: 0x06001977 RID: 6519 RVA: 0x003D204C File Offset: 0x003D204C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(MarkdownInlineRun);
      }
    }

    // Token: 0x170005A8 RID: 1448
    // (get) Token: 0x06001978 RID: 6520 RVA: 0x003D2058 File Offset: 0x003D2058
    // (set) Token: 0x06001979 RID: 6521 RVA: 0x003D2060 File Offset: 0x003D2060
    public string Text { get; set; }

    // Token: 0x170005A9 RID: 1449
    // (get) Token: 0x0600197A RID: 6522 RVA: 0x003D206C File Offset: 0x003D206C
    // (set) Token: 0x0600197B RID: 6523 RVA: 0x003D2074 File Offset: 0x003D2074
    public bool IsStrong { get; set; }

    // Token: 0x170005AA RID: 1450
    // (get) Token: 0x0600197C RID: 6524 RVA: 0x003D2080 File Offset: 0x003D2080
    // (set) Token: 0x0600197D RID: 6525 RVA: 0x003D2088 File Offset: 0x003D2088
    public bool IsEmphasized { get; set; }

    // Token: 0x170005AB RID: 1451
    // (get) Token: 0x0600197E RID: 6526 RVA: 0x003D2094 File Offset: 0x003D2094
    // (set) Token: 0x0600197F RID: 6527 RVA: 0x003D209C File Offset: 0x003D209C
    public bool IsCode { get; set; }

    // Token: 0x170005AC RID: 1452
    // (get) Token: 0x06001980 RID: 6528 RVA: 0x003D20A8 File Offset: 0x003D20A8
    // (set) Token: 0x06001981 RID: 6529 RVA: 0x003D20B0 File Offset: 0x003D20B0
    public bool IsStrikethrough { get; set; }

    // Token: 0x170005AD RID: 1453
    // (get) Token: 0x06001982 RID: 6530 RVA: 0x003D20BC File Offset: 0x003D20BC
    // (set) Token: 0x06001983 RID: 6531 RVA: 0x003D20C4 File Offset: 0x003D20C4
    [Nullable(2)]
    public string Link
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06001984 RID: 6532 RVA: 0x003D20D0 File Offset: 0x003D20D0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("MarkdownInlineRun");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001985 RID: 6533 RVA: 0x003D211C File Offset: 0x003D211C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Text = ");
      builder.Append(this.Text);
      builder.Append(", IsStrong = ");
      builder.Append(this.IsStrong.ToString());
      builder.Append(", IsEmphasized = ");
      builder.Append(this.IsEmphasized.ToString());
      builder.Append(", IsCode = ");
      builder.Append(this.IsCode.ToString());
      builder.Append(", IsStrikethrough = ");
      builder.Append(this.IsStrikethrough.ToString());
      builder.Append(", Link = ");
      builder.Append(this.Link);
      return true;
    }

    // Token: 0x06001986 RID: 6534 RVA: 0x003D2200 File Offset: 0x003D2200
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(MarkdownInlineRun left, MarkdownInlineRun right)
    {
      return !(left == right);
    }

    // Token: 0x06001987 RID: 6535 RVA: 0x003D220C File Offset: 0x003D220C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(MarkdownInlineRun left, MarkdownInlineRun right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001988 RID: 6536 RVA: 0x003D2220 File Offset: 0x003D2220
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsStrong>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsEmphasized>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsCode>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsStrikethrough>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Link>k__BackingField);
    }

    // Token: 0x06001989 RID: 6537 RVA: 0x003D22C8 File Offset: 0x003D22C8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as MarkdownInlineRun);
    }

    // Token: 0x0600198A RID: 6538 RVA: 0x003D22D8 File Offset: 0x003D22D8
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(MarkdownInlineRun other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsStrong>k__BackingField, other.<IsStrong>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsEmphasized>k__BackingField, other.<IsEmphasized>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsCode>k__BackingField, other.<IsCode>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsStrikethrough>k__BackingField, other.<IsStrikethrough>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Link>k__BackingField, other.<Link>k__BackingField));
    }

    // Token: 0x0600198C RID: 6540 RVA: 0x003D23A4 File Offset: 0x003D23A4
    [CompilerGenerated]
    private MarkdownInlineRun(MarkdownInlineRun original)
    {
      this.Text = original.<Text>k__BackingField;
      this.IsStrong = original.<IsStrong>k__BackingField;
      this.IsEmphasized = original.<IsEmphasized>k__BackingField;
      this.IsCode = original.<IsCode>k__BackingField;
      this.IsStrikethrough = original.<IsStrikethrough>k__BackingField;
      this.Link = original.<Link>k__BackingField;
    }

    // Token: 0x0600198D RID: 6541 RVA: 0x003D2400 File Offset: 0x003D2400
    [CompilerGenerated]
    public void Deconstruct(out string Text, out bool IsStrong, out bool IsEmphasized, out bool IsCode, out bool IsStrikethrough, [Nullable(2)] out string Link)
    {
      Text = this.Text;
      IsStrong = this.IsStrong;
      IsEmphasized = this.IsEmphasized;
      IsCode = this.IsCode;
      IsStrikethrough = this.IsStrikethrough;
      Link = this.Link;
    }
  }
}
