using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200004A RID: 74
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class UserMemoryResponse : IEquatable<UserMemoryResponse>
  {
    // Token: 0x0600031F RID: 799 RVA: 0x00352E04 File Offset: 0x00352E04
    public UserMemoryResponse(string Markdown, string ProfileMarkdown, string ContextMarkdown, bool? HasProfile, string UpdatedAt)
    {
      this.Markdown = Markdown;
      this.ProfileMarkdown = ProfileMarkdown;
      this.ContextMarkdown = ContextMarkdown;
      this.HasProfile = HasProfile;
      this.UpdatedAt = UpdatedAt;
      base..ctor();
    }

    // Token: 0x170000A8 RID: 168
    // (get) Token: 0x06000320 RID: 800 RVA: 0x00352E34 File Offset: 0x00352E34
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(UserMemoryResponse);
      }
    }

    // Token: 0x170000A9 RID: 169
    // (get) Token: 0x06000321 RID: 801 RVA: 0x00352E40 File Offset: 0x00352E40
    // (set) Token: 0x06000322 RID: 802 RVA: 0x00352E48 File Offset: 0x00352E48
    public string Markdown { get; set; }

    // Token: 0x170000AA RID: 170
    // (get) Token: 0x06000323 RID: 803 RVA: 0x00352E54 File Offset: 0x00352E54
    // (set) Token: 0x06000324 RID: 804 RVA: 0x00352E5C File Offset: 0x00352E5C
    public string ProfileMarkdown { get; set; }

    // Token: 0x170000AB RID: 171
    // (get) Token: 0x06000325 RID: 805 RVA: 0x00352E68 File Offset: 0x00352E68
    // (set) Token: 0x06000326 RID: 806 RVA: 0x00352E70 File Offset: 0x00352E70
    public string ContextMarkdown { get; set; }

    // Token: 0x170000AC RID: 172
    // (get) Token: 0x06000327 RID: 807 RVA: 0x00352E7C File Offset: 0x00352E7C
    // (set) Token: 0x06000328 RID: 808 RVA: 0x00352E84 File Offset: 0x00352E84
    public bool? HasProfile { get; set; }

    // Token: 0x170000AD RID: 173
    // (get) Token: 0x06000329 RID: 809 RVA: 0x00352E90 File Offset: 0x00352E90
    // (set) Token: 0x0600032A RID: 810 RVA: 0x00352E98 File Offset: 0x00352E98
    public string UpdatedAt { get; set; }

    // Token: 0x0600032B RID: 811 RVA: 0x00352EA4 File Offset: 0x00352EA4
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("UserMemoryResponse");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600032C RID: 812 RVA: 0x00352EF0 File Offset: 0x00352EF0
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Markdown = ");
      builder.Append(this.Markdown);
      builder.Append(", ProfileMarkdown = ");
      builder.Append(this.ProfileMarkdown);
      builder.Append(", ContextMarkdown = ");
      builder.Append(this.ContextMarkdown);
      builder.Append(", HasProfile = ");
      builder.Append(this.HasProfile.ToString());
      builder.Append(", UpdatedAt = ");
      builder.Append(this.UpdatedAt);
      return true;
    }

    // Token: 0x0600032D RID: 813 RVA: 0x00352F90 File Offset: 0x00352F90
    [CompilerGenerated]
    public static bool operator !=(UserMemoryResponse left, UserMemoryResponse right)
    {
      return !(left == right);
    }

    // Token: 0x0600032E RID: 814 RVA: 0x00352F9C File Offset: 0x00352F9C
    [CompilerGenerated]
    public static bool operator ==(UserMemoryResponse left, UserMemoryResponse right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600032F RID: 815 RVA: 0x00352FB0 File Offset: 0x00352FB0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Markdown>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ProfileMarkdown>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ContextMarkdown>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<HasProfile>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UpdatedAt>k__BackingField);
    }

    // Token: 0x06000330 RID: 816 RVA: 0x00353040 File Offset: 0x00353040
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as UserMemoryResponse);
    }

    // Token: 0x06000331 RID: 817 RVA: 0x00353050 File Offset: 0x00353050
    [CompilerGenerated]
    public bool Equals(UserMemoryResponse other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Markdown>k__BackingField, other.<Markdown>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ProfileMarkdown>k__BackingField, other.<ProfileMarkdown>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ContextMarkdown>k__BackingField, other.<ContextMarkdown>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<HasProfile>k__BackingField, other.<HasProfile>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UpdatedAt>k__BackingField, other.<UpdatedAt>k__BackingField));
    }

    // Token: 0x06000333 RID: 819 RVA: 0x00353100 File Offset: 0x00353100
    [CompilerGenerated]
    private UserMemoryResponse([Nullable(1)] UserMemoryResponse original)
    {
      this.Markdown = original.<Markdown>k__BackingField;
      this.ProfileMarkdown = original.<ProfileMarkdown>k__BackingField;
      this.ContextMarkdown = original.<ContextMarkdown>k__BackingField;
      this.HasProfile = original.<HasProfile>k__BackingField;
      this.UpdatedAt = original.<UpdatedAt>k__BackingField;
    }

    // Token: 0x06000334 RID: 820 RVA: 0x00353150 File Offset: 0x00353150
    [CompilerGenerated]
    public void Deconstruct(out string Markdown, out string ProfileMarkdown, out string ContextMarkdown, out bool? HasProfile, out string UpdatedAt)
    {
      Markdown = this.Markdown;
      ProfileMarkdown = this.ProfileMarkdown;
      ContextMarkdown = this.ContextMarkdown;
      HasProfile = this.HasProfile;
      UpdatedAt = this.UpdatedAt;
    }
  }
}
