using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200008B RID: 139
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PageContext : IEquatable<PageContext>
  {
    // Token: 0x0600086D RID: 2157 RVA: 0x00369784 File Offset: 0x00369784
    public PageContext(string Kind, string Reference, [Nullable(2)] string Text, bool IsTruncated, int? PageCount, bool AccessDenied)
    {
      this.Kind = Kind;
      this.Reference = Reference;
      this.Text = Text;
      this.IsTruncated = IsTruncated;
      this.PageCount = PageCount;
      this.AccessDenied = AccessDenied;
      base..ctor();
    }

    // Token: 0x170001DB RID: 475
    // (get) Token: 0x0600086E RID: 2158 RVA: 0x003697BC File Offset: 0x003697BC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(PageContext);
      }
    }

    // Token: 0x170001DC RID: 476
    // (get) Token: 0x0600086F RID: 2159 RVA: 0x003697C8 File Offset: 0x003697C8
    // (set) Token: 0x06000870 RID: 2160 RVA: 0x003697D0 File Offset: 0x003697D0
    public string Kind { get; set; }

    // Token: 0x170001DD RID: 477
    // (get) Token: 0x06000871 RID: 2161 RVA: 0x003697DC File Offset: 0x003697DC
    // (set) Token: 0x06000872 RID: 2162 RVA: 0x003697E4 File Offset: 0x003697E4
    public string Reference { get; set; }

    // Token: 0x170001DE RID: 478
    // (get) Token: 0x06000873 RID: 2163 RVA: 0x003697F0 File Offset: 0x003697F0
    // (set) Token: 0x06000874 RID: 2164 RVA: 0x003697F8 File Offset: 0x003697F8
    [Nullable(2)]
    public string Text
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170001DF RID: 479
    // (get) Token: 0x06000875 RID: 2165 RVA: 0x00369804 File Offset: 0x00369804
    // (set) Token: 0x06000876 RID: 2166 RVA: 0x0036980C File Offset: 0x0036980C
    public bool IsTruncated { get; set; }

    // Token: 0x170001E0 RID: 480
    // (get) Token: 0x06000877 RID: 2167 RVA: 0x00369818 File Offset: 0x00369818
    // (set) Token: 0x06000878 RID: 2168 RVA: 0x00369820 File Offset: 0x00369820
    public int? PageCount { get; set; }

    // Token: 0x170001E1 RID: 481
    // (get) Token: 0x06000879 RID: 2169 RVA: 0x0036982C File Offset: 0x0036982C
    // (set) Token: 0x0600087A RID: 2170 RVA: 0x00369834 File Offset: 0x00369834
    public bool AccessDenied { get; set; }

    // Token: 0x0600087B RID: 2171 RVA: 0x00369840 File Offset: 0x00369840
    public static PageContext ReferenceOnly(PageReference reference)
    {
      return new PageContext(reference.IsWebPage ? "web_page" : "file", reference.Reference, null, false, null, false);
    }

    // Token: 0x0600087C RID: 2172 RVA: 0x00369878 File Offset: 0x00369878
    internal void Write(Utf8JsonWriter writer, bool includeAccessDenied = true)
    {
      writer.WriteStartObject("page");
      writer.WriteString("kind", this.Kind);
      writer.WriteString("reference", this.Reference);
      if (!string.IsNullOrEmpty(this.Text))
      {
        writer.WriteString("text", this.Text);
        writer.WriteBoolean("truncated", this.IsTruncated);
      }
      int? pageCount = this.PageCount;
      if (pageCount != null)
      {
        int valueOrDefault = pageCount.GetValueOrDefault();
        writer.WriteNumber("page_count", valueOrDefault);
      }
      if (this.AccessDenied && includeAccessDenied)
      {
        writer.WriteBoolean("access_denied", true);
      }
      writer.WriteEndObject();
    }

    // Token: 0x170001E2 RID: 482
    // (get) Token: 0x0600087D RID: 2173 RVA: 0x00369924 File Offset: 0x00369924
    public string ThreadSafeReference
    {
      get
      {
        if (this.Kind == "web_page")
        {
          Uri uri;
          if (!Uri.TryCreate(this.Reference, UriKind.Absolute, out uri) || uri.Host.Length <= 0)
          {
            return "a web page";
          }
          return uri.Host;
        }
        else
        {
          string fileName = Path.GetFileName(this.Reference.TrimEnd(new char[] { '\\', '/' }));
          if (fileName.Length <= 0)
          {
            return this.Reference;
          }
          return fileName;
        }
      }
    }

    // Token: 0x0600087E RID: 2174 RVA: 0x003699A0 File Offset: 0x003699A0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PageContext");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600087F RID: 2175 RVA: 0x003699EC File Offset: 0x003699EC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Kind = ");
      builder.Append(this.Kind);
      builder.Append(", Reference = ");
      builder.Append(this.Reference);
      builder.Append(", Text = ");
      builder.Append(this.Text);
      builder.Append(", IsTruncated = ");
      builder.Append(this.IsTruncated.ToString());
      builder.Append(", PageCount = ");
      builder.Append(this.PageCount.ToString());
      builder.Append(", AccessDenied = ");
      builder.Append(this.AccessDenied.ToString());
      builder.Append(", ThreadSafeReference = ");
      builder.Append(this.ThreadSafeReference);
      return true;
    }

    // Token: 0x06000880 RID: 2176 RVA: 0x00369AD8 File Offset: 0x00369AD8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(PageContext left, PageContext right)
    {
      return !(left == right);
    }

    // Token: 0x06000881 RID: 2177 RVA: 0x00369AE4 File Offset: 0x00369AE4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(PageContext left, PageContext right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000882 RID: 2178 RVA: 0x00369AF8 File Offset: 0x00369AF8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Reference>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsTruncated>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<PageCount>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<AccessDenied>k__BackingField);
    }

    // Token: 0x06000883 RID: 2179 RVA: 0x00369BA0 File Offset: 0x00369BA0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PageContext);
    }

    // Token: 0x06000884 RID: 2180 RVA: 0x00369BB0 File Offset: 0x00369BB0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(PageContext other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Reference>k__BackingField, other.<Reference>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsTruncated>k__BackingField, other.<IsTruncated>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<PageCount>k__BackingField, other.<PageCount>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<AccessDenied>k__BackingField, other.<AccessDenied>k__BackingField));
    }

    // Token: 0x06000886 RID: 2182 RVA: 0x00369C7C File Offset: 0x00369C7C
    [CompilerGenerated]
    private PageContext(PageContext original)
    {
      this.Kind = original.<Kind>k__BackingField;
      this.Reference = original.<Reference>k__BackingField;
      this.Text = original.<Text>k__BackingField;
      this.IsTruncated = original.<IsTruncated>k__BackingField;
      this.PageCount = original.<PageCount>k__BackingField;
      this.AccessDenied = original.<AccessDenied>k__BackingField;
    }

    // Token: 0x06000887 RID: 2183 RVA: 0x00369CD8 File Offset: 0x00369CD8
    [CompilerGenerated]
    public void Deconstruct(out string Kind, out string Reference, [Nullable(2)] out string Text, out bool IsTruncated, out int? PageCount, out bool AccessDenied)
    {
      Kind = this.Kind;
      Reference = this.Reference;
      Text = this.Text;
      IsTruncated = this.IsTruncated;
      PageCount = this.PageCount;
      AccessDenied = this.AccessDenied;
    }
  }
}
