using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000056 RID: 86
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ImageSearchRequest : IEquatable<ImageSearchRequest>
  {
    // Token: 0x06000421 RID: 1057 RVA: 0x003555E0 File Offset: 0x003555E0
    public ImageSearchRequest(string Query, int Limit, bool? SubjectPhoto = null, [Nullable(2)] string PageUrl = null)
    {
      this.Query = Query;
      this.Limit = Limit;
      this.SubjectPhoto = SubjectPhoto;
      this.PageUrl = PageUrl;
      base..ctor();
    }

    // Token: 0x170000ED RID: 237
    // (get) Token: 0x06000422 RID: 1058 RVA: 0x00355608 File Offset: 0x00355608
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ImageSearchRequest);
      }
    }

    // Token: 0x170000EE RID: 238
    // (get) Token: 0x06000423 RID: 1059 RVA: 0x00355614 File Offset: 0x00355614
    // (set) Token: 0x06000424 RID: 1060 RVA: 0x0035561C File Offset: 0x0035561C
    public string Query { get; set; }

    // Token: 0x170000EF RID: 239
    // (get) Token: 0x06000425 RID: 1061 RVA: 0x00355628 File Offset: 0x00355628
    // (set) Token: 0x06000426 RID: 1062 RVA: 0x00355630 File Offset: 0x00355630
    public int Limit { get; set; }

    // Token: 0x170000F0 RID: 240
    // (get) Token: 0x06000427 RID: 1063 RVA: 0x0035563C File Offset: 0x0035563C
    // (set) Token: 0x06000428 RID: 1064 RVA: 0x00355644 File Offset: 0x00355644
    public bool? SubjectPhoto { get; set; }

    // Token: 0x170000F1 RID: 241
    // (get) Token: 0x06000429 RID: 1065 RVA: 0x00355650 File Offset: 0x00355650
    // (set) Token: 0x0600042A RID: 1066 RVA: 0x00355658 File Offset: 0x00355658
    [Nullable(2)]
    public string PageUrl
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x0600042B RID: 1067 RVA: 0x00355664 File Offset: 0x00355664
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ImageSearchRequest");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600042C RID: 1068 RVA: 0x003556B0 File Offset: 0x003556B0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Query = ");
      builder.Append(this.Query);
      builder.Append(", Limit = ");
      builder.Append(this.Limit.ToString());
      builder.Append(", SubjectPhoto = ");
      builder.Append(this.SubjectPhoto.ToString());
      builder.Append(", PageUrl = ");
      builder.Append(this.PageUrl);
      return true;
    }

    // Token: 0x0600042D RID: 1069 RVA: 0x00355744 File Offset: 0x00355744
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ImageSearchRequest left, ImageSearchRequest right)
    {
      return !(left == right);
    }

    // Token: 0x0600042E RID: 1070 RVA: 0x00355750 File Offset: 0x00355750
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ImageSearchRequest left, ImageSearchRequest right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600042F RID: 1071 RVA: 0x00355764 File Offset: 0x00355764
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Query>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Limit>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<SubjectPhoto>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PageUrl>k__BackingField);
    }

    // Token: 0x06000430 RID: 1072 RVA: 0x003557E0 File Offset: 0x003557E0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ImageSearchRequest);
    }

    // Token: 0x06000431 RID: 1073 RVA: 0x003557F0 File Offset: 0x003557F0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ImageSearchRequest other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Query>k__BackingField, other.<Query>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Limit>k__BackingField, other.<Limit>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<SubjectPhoto>k__BackingField, other.<SubjectPhoto>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PageUrl>k__BackingField, other.<PageUrl>k__BackingField));
    }

    // Token: 0x06000433 RID: 1075 RVA: 0x00355884 File Offset: 0x00355884
    [CompilerGenerated]
    private ImageSearchRequest(ImageSearchRequest original)
    {
      this.Query = original.<Query>k__BackingField;
      this.Limit = original.<Limit>k__BackingField;
      this.SubjectPhoto = original.<SubjectPhoto>k__BackingField;
      this.PageUrl = original.<PageUrl>k__BackingField;
    }

    // Token: 0x06000434 RID: 1076 RVA: 0x003558BC File Offset: 0x003558BC
    [CompilerGenerated]
    public void Deconstruct(out string Query, out int Limit, out bool? SubjectPhoto, [Nullable(2)] out string PageUrl)
    {
      Query = this.Query;
      Limit = this.Limit;
      SubjectPhoto = this.SubjectPhoto;
      PageUrl = this.PageUrl;
    }
  }
}
