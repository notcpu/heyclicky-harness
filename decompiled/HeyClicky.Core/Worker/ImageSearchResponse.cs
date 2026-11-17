using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000058 RID: 88
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ImageSearchResponse : IEquatable<ImageSearchResponse>
  {
    // Token: 0x06000449 RID: 1097 RVA: 0x00355BC8 File Offset: 0x00355BC8
    public ImageSearchResponse([Nullable(new byte[] { 2, 1 })] IReadOnlyList<ImageSearchResult> Images)
    {
      this.Images = Images;
      base..ctor();
    }

    // Token: 0x170000F7 RID: 247
    // (get) Token: 0x0600044A RID: 1098 RVA: 0x00355BD8 File Offset: 0x00355BD8
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ImageSearchResponse);
      }
    }

    // Token: 0x170000F8 RID: 248
    // (get) Token: 0x0600044B RID: 1099 RVA: 0x00355BE4 File Offset: 0x00355BE4
    // (set) Token: 0x0600044C RID: 1100 RVA: 0x00355BEC File Offset: 0x00355BEC
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<ImageSearchResult> Images
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x0600044D RID: 1101 RVA: 0x00355BF8 File Offset: 0x00355BF8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ImageSearchResponse");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600044E RID: 1102 RVA: 0x00355C44 File Offset: 0x00355C44
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Images = ");
      builder.Append(this.Images);
      return true;
    }

    // Token: 0x0600044F RID: 1103 RVA: 0x00355C68 File Offset: 0x00355C68
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ImageSearchResponse left, ImageSearchResponse right)
    {
      return !(left == right);
    }

    // Token: 0x06000450 RID: 1104 RVA: 0x00355C74 File Offset: 0x00355C74
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ImageSearchResponse left, ImageSearchResponse right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000451 RID: 1105 RVA: 0x00355C88 File Offset: 0x00355C88
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<ImageSearchResult>>.Default.GetHashCode(this.<Images>k__BackingField);
    }

    // Token: 0x06000452 RID: 1106 RVA: 0x00355CB4 File Offset: 0x00355CB4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ImageSearchResponse);
    }

    // Token: 0x06000453 RID: 1107 RVA: 0x00355CC4 File Offset: 0x00355CC4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ImageSearchResponse other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<ImageSearchResult>>.Default.Equals(this.<Images>k__BackingField, other.<Images>k__BackingField));
    }

    // Token: 0x06000455 RID: 1109 RVA: 0x00355D04 File Offset: 0x00355D04
    [CompilerGenerated]
    private ImageSearchResponse(ImageSearchResponse original)
    {
      this.Images = original.<Images>k__BackingField;
    }

    // Token: 0x06000456 RID: 1110 RVA: 0x00355D18 File Offset: 0x00355D18
    [CompilerGenerated]
    public void Deconstruct([Nullable(new byte[] { 2, 1 })] out IReadOnlyList<ImageSearchResult> Images)
    {
      Images = this.Images;
    }
  }
}
