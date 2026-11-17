using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200005A RID: 90
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class WebSearchResult : IEquatable<WebSearchResult>
  {
    // Token: 0x06000469 RID: 1129 RVA: 0x00355FA0 File Offset: 0x00355FA0
    public WebSearchResult(string Title, string Url, [Nullable(2)] string Description, [Nullable(2)] string Age, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> Highlights)
    {
      this.Title = Title;
      this.Url = Url;
      this.Description = Description;
      this.Age = Age;
      this.Highlights = Highlights;
      base..ctor();
    }

    // Token: 0x170000FD RID: 253
    // (get) Token: 0x0600046A RID: 1130 RVA: 0x00355FD0 File Offset: 0x00355FD0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(WebSearchResult);
      }
    }

    // Token: 0x170000FE RID: 254
    // (get) Token: 0x0600046B RID: 1131 RVA: 0x00355FDC File Offset: 0x00355FDC
    // (set) Token: 0x0600046C RID: 1132 RVA: 0x00355FE4 File Offset: 0x00355FE4
    public string Title { get; set; }

    // Token: 0x170000FF RID: 255
    // (get) Token: 0x0600046D RID: 1133 RVA: 0x00355FF0 File Offset: 0x00355FF0
    // (set) Token: 0x0600046E RID: 1134 RVA: 0x00355FF8 File Offset: 0x00355FF8
    public string Url { get; set; }

    // Token: 0x17000100 RID: 256
    // (get) Token: 0x0600046F RID: 1135 RVA: 0x00356004 File Offset: 0x00356004
    // (set) Token: 0x06000470 RID: 1136 RVA: 0x0035600C File Offset: 0x0035600C
    [Nullable(2)]
    public string Description
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000101 RID: 257
    // (get) Token: 0x06000471 RID: 1137 RVA: 0x00356018 File Offset: 0x00356018
    // (set) Token: 0x06000472 RID: 1138 RVA: 0x00356020 File Offset: 0x00356020
    [Nullable(2)]
    public string Age
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000102 RID: 258
    // (get) Token: 0x06000473 RID: 1139 RVA: 0x0035602C File Offset: 0x0035602C
    // (set) Token: 0x06000474 RID: 1140 RVA: 0x00356034 File Offset: 0x00356034
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> Highlights
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x06000475 RID: 1141 RVA: 0x00356040 File Offset: 0x00356040
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("WebSearchResult");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000476 RID: 1142 RVA: 0x0035608C File Offset: 0x0035608C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Title = ");
      builder.Append(this.Title);
      builder.Append(", Url = ");
      builder.Append(this.Url);
      builder.Append(", Description = ");
      builder.Append(this.Description);
      builder.Append(", Age = ");
      builder.Append(this.Age);
      builder.Append(", Highlights = ");
      builder.Append(this.Highlights);
      return true;
    }

    // Token: 0x06000477 RID: 1143 RVA: 0x0035611C File Offset: 0x0035611C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(WebSearchResult left, WebSearchResult right)
    {
      return !(left == right);
    }

    // Token: 0x06000478 RID: 1144 RVA: 0x00356128 File Offset: 0x00356128
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(WebSearchResult left, WebSearchResult right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000479 RID: 1145 RVA: 0x0035613C File Offset: 0x0035613C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Url>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Age>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Highlights>k__BackingField);
    }

    // Token: 0x0600047A RID: 1146 RVA: 0x003561CC File Offset: 0x003561CC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as WebSearchResult);
    }

    // Token: 0x0600047B RID: 1147 RVA: 0x003561DC File Offset: 0x003561DC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(WebSearchResult other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Url>k__BackingField, other.<Url>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Age>k__BackingField, other.<Age>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Highlights>k__BackingField, other.<Highlights>k__BackingField));
    }

    // Token: 0x0600047D RID: 1149 RVA: 0x0035628C File Offset: 0x0035628C
    [CompilerGenerated]
    private WebSearchResult(WebSearchResult original)
    {
      this.Title = original.<Title>k__BackingField;
      this.Url = original.<Url>k__BackingField;
      this.Description = original.<Description>k__BackingField;
      this.Age = original.<Age>k__BackingField;
      this.Highlights = original.<Highlights>k__BackingField;
    }

    // Token: 0x0600047E RID: 1150 RVA: 0x003562DC File Offset: 0x003562DC
    [CompilerGenerated]
    public void Deconstruct(out string Title, out string Url, [Nullable(2)] out string Description, [Nullable(2)] out string Age, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<string> Highlights)
    {
      Title = this.Title;
      Url = this.Url;
      Description = this.Description;
      Age = this.Age;
      Highlights = this.Highlights;
    }
  }
}
