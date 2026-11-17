using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002D7 RID: 727
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MarkdownViewerTableContent : IEquatable<MarkdownViewerTableContent>
  {
    // Token: 0x06001E97 RID: 7831 RVA: 0x00607BA0 File Offset: 0x00607BA0
    public MarkdownViewerTableContent(IReadOnlyList<string> Headers, IReadOnlyList<IReadOnlyList<string>> Rows, string PlainText)
    {
      this.Headers = Headers;
      this.Rows = Rows;
      this.PlainText = PlainText;
      base..ctor();
    }

    // Token: 0x1700057F RID: 1407
    // (get) Token: 0x06001E98 RID: 7832 RVA: 0x00607BC0 File Offset: 0x00607BC0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(MarkdownViewerTableContent);
      }
    }

    // Token: 0x17000580 RID: 1408
    // (get) Token: 0x06001E99 RID: 7833 RVA: 0x00607BCC File Offset: 0x00607BCC
    // (set) Token: 0x06001E9A RID: 7834 RVA: 0x00607BD4 File Offset: 0x00607BD4
    public IReadOnlyList<string> Headers { get; set; }

    // Token: 0x17000581 RID: 1409
    // (get) Token: 0x06001E9B RID: 7835 RVA: 0x00607BE0 File Offset: 0x00607BE0
    // (set) Token: 0x06001E9C RID: 7836 RVA: 0x00607BE8 File Offset: 0x00607BE8
    public IReadOnlyList<IReadOnlyList<string>> Rows { get; set; }

    // Token: 0x17000582 RID: 1410
    // (get) Token: 0x06001E9D RID: 7837 RVA: 0x00607BF4 File Offset: 0x00607BF4
    // (set) Token: 0x06001E9E RID: 7838 RVA: 0x00607BFC File Offset: 0x00607BFC
    public string PlainText { get; set; }

    // Token: 0x06001E9F RID: 7839 RVA: 0x00607C08 File Offset: 0x00607C08
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("MarkdownViewerTableContent");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001EA0 RID: 7840 RVA: 0x00607C54 File Offset: 0x00607C54
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Headers = ");
      builder.Append(this.Headers);
      builder.Append(", Rows = ");
      builder.Append(this.Rows);
      builder.Append(", PlainText = ");
      builder.Append(this.PlainText);
      return true;
    }

    // Token: 0x06001EA1 RID: 7841 RVA: 0x00607CB4 File Offset: 0x00607CB4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(MarkdownViewerTableContent left, MarkdownViewerTableContent right)
    {
      return !(left == right);
    }

    // Token: 0x06001EA2 RID: 7842 RVA: 0x00607CC0 File Offset: 0x00607CC0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(MarkdownViewerTableContent left, MarkdownViewerTableContent right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001EA3 RID: 7843 RVA: 0x00607CD4 File Offset: 0x00607CD4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Headers>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<IReadOnlyList<string>>>.Default.GetHashCode(this.<Rows>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PlainText>k__BackingField);
    }

    // Token: 0x06001EA4 RID: 7844 RVA: 0x00607D38 File Offset: 0x00607D38
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as MarkdownViewerTableContent);
    }

    // Token: 0x06001EA5 RID: 7845 RVA: 0x00607D48 File Offset: 0x00607D48
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(MarkdownViewerTableContent other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Headers>k__BackingField, other.<Headers>k__BackingField) && EqualityComparer<IReadOnlyList<IReadOnlyList<string>>>.Default.Equals(this.<Rows>k__BackingField, other.<Rows>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PlainText>k__BackingField, other.<PlainText>k__BackingField));
    }

    // Token: 0x06001EA7 RID: 7847 RVA: 0x00607DC4 File Offset: 0x00607DC4
    [CompilerGenerated]
    private MarkdownViewerTableContent(MarkdownViewerTableContent original)
    {
      this.Headers = original.<Headers>k__BackingField;
      this.Rows = original.<Rows>k__BackingField;
      this.PlainText = original.<PlainText>k__BackingField;
    }

    // Token: 0x06001EA8 RID: 7848 RVA: 0x00607DF0 File Offset: 0x00607DF0
    [CompilerGenerated]
    public void Deconstruct(out IReadOnlyList<string> Headers, out IReadOnlyList<IReadOnlyList<string>> Rows, out string PlainText)
    {
      Headers = this.Headers;
      Rows = this.Rows;
      PlainText = this.PlainText;
    }
  }
}
