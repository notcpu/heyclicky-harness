using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000057 RID: 87
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ImageSearchResult : IEquatable<ImageSearchResult>
  {
    // Token: 0x06000435 RID: 1077 RVA: 0x003558E4 File Offset: 0x003558E4
    public ImageSearchResult(string ThumbnailUrl, string FullUrl, string SourceUrl, string Title)
    {
      this.ThumbnailUrl = ThumbnailUrl;
      this.FullUrl = FullUrl;
      this.SourceUrl = SourceUrl;
      this.Title = Title;
      base..ctor();
    }

    // Token: 0x170000F2 RID: 242
    // (get) Token: 0x06000436 RID: 1078 RVA: 0x0035590C File Offset: 0x0035590C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ImageSearchResult);
      }
    }

    // Token: 0x170000F3 RID: 243
    // (get) Token: 0x06000437 RID: 1079 RVA: 0x00355918 File Offset: 0x00355918
    // (set) Token: 0x06000438 RID: 1080 RVA: 0x00355920 File Offset: 0x00355920
    public string ThumbnailUrl { get; set; }

    // Token: 0x170000F4 RID: 244
    // (get) Token: 0x06000439 RID: 1081 RVA: 0x0035592C File Offset: 0x0035592C
    // (set) Token: 0x0600043A RID: 1082 RVA: 0x00355934 File Offset: 0x00355934
    public string FullUrl { get; set; }

    // Token: 0x170000F5 RID: 245
    // (get) Token: 0x0600043B RID: 1083 RVA: 0x00355940 File Offset: 0x00355940
    // (set) Token: 0x0600043C RID: 1084 RVA: 0x00355948 File Offset: 0x00355948
    public string SourceUrl { get; set; }

    // Token: 0x170000F6 RID: 246
    // (get) Token: 0x0600043D RID: 1085 RVA: 0x00355954 File Offset: 0x00355954
    // (set) Token: 0x0600043E RID: 1086 RVA: 0x0035595C File Offset: 0x0035595C
    public string Title { get; set; }

    // Token: 0x0600043F RID: 1087 RVA: 0x00355968 File Offset: 0x00355968
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ImageSearchResult");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000440 RID: 1088 RVA: 0x003559B4 File Offset: 0x003559B4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ThumbnailUrl = ");
      builder.Append(this.ThumbnailUrl);
      builder.Append(", FullUrl = ");
      builder.Append(this.FullUrl);
      builder.Append(", SourceUrl = ");
      builder.Append(this.SourceUrl);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      return true;
    }

    // Token: 0x06000441 RID: 1089 RVA: 0x00355A2C File Offset: 0x00355A2C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ImageSearchResult left, ImageSearchResult right)
    {
      return !(left == right);
    }

    // Token: 0x06000442 RID: 1090 RVA: 0x00355A38 File Offset: 0x00355A38
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ImageSearchResult left, ImageSearchResult right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000443 RID: 1091 RVA: 0x00355A4C File Offset: 0x00355A4C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThumbnailUrl>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FullUrl>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SourceUrl>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField);
    }

    // Token: 0x06000444 RID: 1092 RVA: 0x00355AC8 File Offset: 0x00355AC8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ImageSearchResult);
    }

    // Token: 0x06000445 RID: 1093 RVA: 0x00355AD8 File Offset: 0x00355AD8
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ImageSearchResult other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ThumbnailUrl>k__BackingField, other.<ThumbnailUrl>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FullUrl>k__BackingField, other.<FullUrl>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SourceUrl>k__BackingField, other.<SourceUrl>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField));
    }

    // Token: 0x06000447 RID: 1095 RVA: 0x00355B6C File Offset: 0x00355B6C
    [CompilerGenerated]
    private ImageSearchResult(ImageSearchResult original)
    {
      this.ThumbnailUrl = original.<ThumbnailUrl>k__BackingField;
      this.FullUrl = original.<FullUrl>k__BackingField;
      this.SourceUrl = original.<SourceUrl>k__BackingField;
      this.Title = original.<Title>k__BackingField;
    }

    // Token: 0x06000448 RID: 1096 RVA: 0x00355BA4 File Offset: 0x00355BA4
    [CompilerGenerated]
    public void Deconstruct(out string ThumbnailUrl, out string FullUrl, out string SourceUrl, out string Title)
    {
      ThumbnailUrl = this.ThumbnailUrl;
      FullUrl = this.FullUrl;
      SourceUrl = this.SourceUrl;
      Title = this.Title;
    }
  }
}
