using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002D0 RID: 720
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class MarkdownViewerImage : IEquatable<MarkdownViewerImage>
  {
    // Token: 0x06001E43 RID: 7747 RVA: 0x00606AC8 File Offset: 0x00606AC8
    public MarkdownViewerImage(string Path, byte[] Bytes, double Width, double Height)
    {
      this.Path = Path;
      this.Bytes = Bytes;
      this.Width = Width;
      this.Height = Height;
      base..ctor();
    }

    // Token: 0x17000564 RID: 1380
    // (get) Token: 0x06001E44 RID: 7748 RVA: 0x00606AF0 File Offset: 0x00606AF0
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(MarkdownViewerImage);
      }
    }

    // Token: 0x17000565 RID: 1381
    // (get) Token: 0x06001E45 RID: 7749 RVA: 0x00606AFC File Offset: 0x00606AFC
    // (set) Token: 0x06001E46 RID: 7750 RVA: 0x00606B04 File Offset: 0x00606B04
    public string Path { get; set; }

    // Token: 0x17000566 RID: 1382
    // (get) Token: 0x06001E47 RID: 7751 RVA: 0x00606B10 File Offset: 0x00606B10
    // (set) Token: 0x06001E48 RID: 7752 RVA: 0x00606B18 File Offset: 0x00606B18
    public byte[] Bytes { get; set; }

    // Token: 0x17000567 RID: 1383
    // (get) Token: 0x06001E49 RID: 7753 RVA: 0x00606B24 File Offset: 0x00606B24
    // (set) Token: 0x06001E4A RID: 7754 RVA: 0x00606B2C File Offset: 0x00606B2C
    public double Width { get; set; }

    // Token: 0x17000568 RID: 1384
    // (get) Token: 0x06001E4B RID: 7755 RVA: 0x00606B38 File Offset: 0x00606B38
    // (set) Token: 0x06001E4C RID: 7756 RVA: 0x00606B40 File Offset: 0x00606B40
    public double Height { get; set; }

    // Token: 0x06001E4D RID: 7757 RVA: 0x00606B4C File Offset: 0x00606B4C
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("MarkdownViewerImage");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001E4E RID: 7758 RVA: 0x00606B98 File Offset: 0x00606B98
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Path = ");
      builder.Append(this.Path);
      builder.Append(", Bytes = ");
      builder.Append(this.Bytes);
      builder.Append(", Width = ");
      builder.Append(this.Width.ToString());
      builder.Append(", Height = ");
      builder.Append(this.Height.ToString());
      return true;
    }

    // Token: 0x06001E4F RID: 7759 RVA: 0x00606C2C File Offset: 0x00606C2C
    [CompilerGenerated]
    public static bool operator !=(MarkdownViewerImage left, MarkdownViewerImage right)
    {
      return !(left == right);
    }

    // Token: 0x06001E50 RID: 7760 RVA: 0x00606C38 File Offset: 0x00606C38
    [CompilerGenerated]
    public static bool operator ==(MarkdownViewerImage left, MarkdownViewerImage right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001E51 RID: 7761 RVA: 0x00606C4C File Offset: 0x00606C4C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Path>k__BackingField)) * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(this.<Bytes>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Width>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Height>k__BackingField);
    }

    // Token: 0x06001E52 RID: 7762 RVA: 0x00606CC8 File Offset: 0x00606CC8
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as MarkdownViewerImage);
    }

    // Token: 0x06001E53 RID: 7763 RVA: 0x00606CD8 File Offset: 0x00606CD8
    [CompilerGenerated]
    public bool Equals(MarkdownViewerImage other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField) && EqualityComparer<byte[]>.Default.Equals(this.<Bytes>k__BackingField, other.<Bytes>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Width>k__BackingField, other.<Width>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Height>k__BackingField, other.<Height>k__BackingField));
    }

    // Token: 0x06001E55 RID: 7765 RVA: 0x00606D6C File Offset: 0x00606D6C
    [CompilerGenerated]
    private MarkdownViewerImage([Nullable(1)] MarkdownViewerImage original)
    {
      this.Path = original.<Path>k__BackingField;
      this.Bytes = original.<Bytes>k__BackingField;
      this.Width = original.<Width>k__BackingField;
      this.Height = original.<Height>k__BackingField;
    }

    // Token: 0x06001E56 RID: 7766 RVA: 0x00606DA4 File Offset: 0x00606DA4
    [CompilerGenerated]
    public void Deconstruct(out string Path, out byte[] Bytes, out double Width, out double Height)
    {
      Path = this.Path;
      Bytes = this.Bytes;
      Width = this.Width;
      Height = this.Height;
    }
  }
}
