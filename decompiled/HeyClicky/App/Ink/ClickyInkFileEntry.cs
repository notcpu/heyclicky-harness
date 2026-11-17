using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.Home;
using HeyClicky.App.Notch.Peek;
using HeyClicky.Core.Ink;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000270 RID: 624
  [NullableContext(1)]
  [Nullable(0)]
  internal readonly struct ClickyInkFileEntry : IEquatable<ClickyInkFileEntry>
  {
    // Token: 0x06001AAD RID: 6829 RVA: 0x005E60C4 File Offset: 0x005E60C4
    public ClickyInkFileEntry(string RawPath, string ResolvedPath, bool IsWebLink, string Name, NotchDocumentStyle Style)
    {
      this.RawPath = RawPath;
      this.ResolvedPath = ResolvedPath;
      this.IsWebLink = IsWebLink;
      this.Name = Name;
      this.Style = Style;
    }

    // Token: 0x17000498 RID: 1176
    // (get) Token: 0x06001AAE RID: 6830 RVA: 0x005E60EC File Offset: 0x005E60EC
    // (set) Token: 0x06001AAF RID: 6831 RVA: 0x005E60F4 File Offset: 0x005E60F4
    public string RawPath { get; set; }

    // Token: 0x17000499 RID: 1177
    // (get) Token: 0x06001AB0 RID: 6832 RVA: 0x005E6100 File Offset: 0x005E6100
    // (set) Token: 0x06001AB1 RID: 6833 RVA: 0x005E6108 File Offset: 0x005E6108
    public string ResolvedPath { get; set; }

    // Token: 0x1700049A RID: 1178
    // (get) Token: 0x06001AB2 RID: 6834 RVA: 0x005E6114 File Offset: 0x005E6114
    // (set) Token: 0x06001AB3 RID: 6835 RVA: 0x005E611C File Offset: 0x005E611C
    public bool IsWebLink { get; set; }

    // Token: 0x1700049B RID: 1179
    // (get) Token: 0x06001AB4 RID: 6836 RVA: 0x005E6128 File Offset: 0x005E6128
    // (set) Token: 0x06001AB5 RID: 6837 RVA: 0x005E6130 File Offset: 0x005E6130
    public string Name { get; set; }

    // Token: 0x1700049C RID: 1180
    // (get) Token: 0x06001AB6 RID: 6838 RVA: 0x005E613C File Offset: 0x005E613C
    // (set) Token: 0x06001AB7 RID: 6839 RVA: 0x005E6144 File Offset: 0x005E6144
    public NotchDocumentStyle Style { get; set; }

    // Token: 0x06001AB8 RID: 6840 RVA: 0x005E6150 File Offset: 0x005E6150
    public static ClickyInkFileEntry Of(ClickyInkLiveNote.Block block, ClickyInkActions actions)
    {
      string text = block.Source["path"] ?? "";
      bool flag = ArtifactPaths.IsWebLink(text);
      string text2 = (flag ? text : actions.ResolvePath(text));
      Uri uri;
      string text3 = (flag ? ((Uri.TryCreate(text, UriKind.Absolute, out uri) && uri.Host.Length > 0) ? uri.Host : text) : Path.GetFileName(text2.TrimEnd(new char[] { '/', '\\' })));
      return new ClickyInkFileEntry(text, text2, flag, text3, NotchDocumentStyle.For(text2, flag));
    }

    // Token: 0x06001AB9 RID: 6841 RVA: 0x005E61E8 File Offset: 0x005E61E8
    [NullableContext(0)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkFileEntry");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001ABA RID: 6842 RVA: 0x005E6234 File Offset: 0x005E6234
    [NullableContext(0)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("RawPath = ");
      builder.Append(this.RawPath);
      builder.Append(", ResolvedPath = ");
      builder.Append(this.ResolvedPath);
      builder.Append(", IsWebLink = ");
      builder.Append(this.IsWebLink.ToString());
      builder.Append(", Name = ");
      builder.Append(this.Name);
      builder.Append(", Style = ");
      builder.Append(this.Style);
      return true;
    }

    // Token: 0x06001ABB RID: 6843 RVA: 0x005E62D0 File Offset: 0x005E62D0
    [CompilerGenerated]
    public static bool operator !=(ClickyInkFileEntry left, ClickyInkFileEntry right)
    {
      return !(left == right);
    }

    // Token: 0x06001ABC RID: 6844 RVA: 0x005E62DC File Offset: 0x005E62DC
    [CompilerGenerated]
    public static bool operator ==(ClickyInkFileEntry left, ClickyInkFileEntry right)
    {
      return left.Equals(right);
    }

    // Token: 0x06001ABD RID: 6845 RVA: 0x005E62E8 File Offset: 0x005E62E8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<string>.Default.GetHashCode(this.<RawPath>k__BackingField) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ResolvedPath>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsWebLink>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<NotchDocumentStyle>.Default.GetHashCode(this.<Style>k__BackingField);
    }

    // Token: 0x06001ABE RID: 6846 RVA: 0x005E6364 File Offset: 0x005E6364
    [NullableContext(0)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is ClickyInkFileEntry && this.Equals((ClickyInkFileEntry)obj);
    }

    // Token: 0x06001ABF RID: 6847 RVA: 0x005E637C File Offset: 0x005E637C
    [CompilerGenerated]
    public bool Equals(ClickyInkFileEntry other)
    {
      return EqualityComparer<string>.Default.Equals(this.<RawPath>k__BackingField, other.<RawPath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ResolvedPath>k__BackingField, other.<ResolvedPath>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsWebLink>k__BackingField, other.<IsWebLink>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<NotchDocumentStyle>.Default.Equals(this.<Style>k__BackingField, other.<Style>k__BackingField);
    }

    // Token: 0x06001AC0 RID: 6848 RVA: 0x005E6404 File Offset: 0x005E6404
    [CompilerGenerated]
    public void Deconstruct(out string RawPath, out string ResolvedPath, out bool IsWebLink, out string Name, out NotchDocumentStyle Style)
    {
      RawPath = this.RawPath;
      ResolvedPath = this.ResolvedPath;
      IsWebLink = this.IsWebLink;
      Name = this.Name;
      Style = this.Style;
    }
  }
}
