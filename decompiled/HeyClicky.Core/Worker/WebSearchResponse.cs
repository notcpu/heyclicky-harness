using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200005B RID: 91
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class WebSearchResponse : IEquatable<WebSearchResponse>
  {
    // Token: 0x0600047F RID: 1151 RVA: 0x00356308 File Offset: 0x00356308
    public WebSearchResponse(string Query, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<WebSearchResult> Results)
    {
      this.Query = Query;
      this.Results = Results;
      base..ctor();
    }

    // Token: 0x17000103 RID: 259
    // (get) Token: 0x06000480 RID: 1152 RVA: 0x00356320 File Offset: 0x00356320
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(WebSearchResponse);
      }
    }

    // Token: 0x17000104 RID: 260
    // (get) Token: 0x06000481 RID: 1153 RVA: 0x0035632C File Offset: 0x0035632C
    // (set) Token: 0x06000482 RID: 1154 RVA: 0x00356334 File Offset: 0x00356334
    public string Query { get; set; }

    // Token: 0x17000105 RID: 261
    // (get) Token: 0x06000483 RID: 1155 RVA: 0x00356340 File Offset: 0x00356340
    // (set) Token: 0x06000484 RID: 1156 RVA: 0x00356348 File Offset: 0x00356348
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<WebSearchResult> Results
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x06000485 RID: 1157 RVA: 0x00356354 File Offset: 0x00356354
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("WebSearchResponse");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000486 RID: 1158 RVA: 0x003563A0 File Offset: 0x003563A0
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Query = ");
      builder.Append(this.Query);
      builder.Append(", Results = ");
      builder.Append(this.Results);
      return true;
    }

    // Token: 0x06000487 RID: 1159 RVA: 0x003563DC File Offset: 0x003563DC
    [CompilerGenerated]
    public static bool operator !=(WebSearchResponse left, WebSearchResponse right)
    {
      return !(left == right);
    }

    // Token: 0x06000488 RID: 1160 RVA: 0x003563E8 File Offset: 0x003563E8
    [CompilerGenerated]
    public static bool operator ==(WebSearchResponse left, WebSearchResponse right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000489 RID: 1161 RVA: 0x003563FC File Offset: 0x003563FC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Query>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<WebSearchResult>>.Default.GetHashCode(this.<Results>k__BackingField);
    }

    // Token: 0x0600048A RID: 1162 RVA: 0x0035643C File Offset: 0x0035643C
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as WebSearchResponse);
    }

    // Token: 0x0600048B RID: 1163 RVA: 0x0035644C File Offset: 0x0035644C
    [CompilerGenerated]
    public bool Equals(WebSearchResponse other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Query>k__BackingField, other.<Query>k__BackingField) && EqualityComparer<IReadOnlyList<WebSearchResult>>.Default.Equals(this.<Results>k__BackingField, other.<Results>k__BackingField));
    }

    // Token: 0x0600048D RID: 1165 RVA: 0x003564B0 File Offset: 0x003564B0
    [CompilerGenerated]
    private WebSearchResponse([Nullable(1)] WebSearchResponse original)
    {
      this.Query = original.<Query>k__BackingField;
      this.Results = original.<Results>k__BackingField;
    }

    // Token: 0x0600048E RID: 1166 RVA: 0x003564D0 File Offset: 0x003564D0
    [CompilerGenerated]
    public void Deconstruct(out string Query, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<WebSearchResult> Results)
    {
      Query = this.Query;
      Results = this.Results;
    }
  }
}
