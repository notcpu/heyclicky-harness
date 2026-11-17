using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x0200001A RID: 26
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class UserProfile : IEquatable<UserProfile>
  {
    // Token: 0x06000111 RID: 273 RVA: 0x0001E6FC File Offset: 0x0001E6FC
    public UserProfile(string UserId, string Email, [Nullable(2)] string DisplayName, [Nullable(2)] string AvatarUrl)
    {
      this.UserId = UserId;
      this.Email = Email;
      this.DisplayName = DisplayName;
      this.AvatarUrl = AvatarUrl;
      base..ctor();
    }

    // Token: 0x1700004D RID: 77
    // (get) Token: 0x06000112 RID: 274 RVA: 0x0001E724 File Offset: 0x0001E724
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(UserProfile);
      }
    }

    // Token: 0x1700004E RID: 78
    // (get) Token: 0x06000113 RID: 275 RVA: 0x0001E730 File Offset: 0x0001E730
    // (set) Token: 0x06000114 RID: 276 RVA: 0x0001E738 File Offset: 0x0001E738
    public string UserId { get; set; }

    // Token: 0x1700004F RID: 79
    // (get) Token: 0x06000115 RID: 277 RVA: 0x0001E744 File Offset: 0x0001E744
    // (set) Token: 0x06000116 RID: 278 RVA: 0x0001E74C File Offset: 0x0001E74C
    public string Email { get; set; }

    // Token: 0x17000050 RID: 80
    // (get) Token: 0x06000117 RID: 279 RVA: 0x0001E758 File Offset: 0x0001E758
    // (set) Token: 0x06000118 RID: 280 RVA: 0x0001E760 File Offset: 0x0001E760
    [Nullable(2)]
    public string DisplayName
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000051 RID: 81
    // (get) Token: 0x06000119 RID: 281 RVA: 0x0001E76C File Offset: 0x0001E76C
    // (set) Token: 0x0600011A RID: 282 RVA: 0x0001E774 File Offset: 0x0001E774
    [Nullable(2)]
    public string AvatarUrl
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x0600011B RID: 283 RVA: 0x0001E780 File Offset: 0x0001E780
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("UserProfile");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600011C RID: 284 RVA: 0x0001E7CC File Offset: 0x0001E7CC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("UserId = ");
      builder.Append(this.UserId);
      builder.Append(", Email = ");
      builder.Append(this.Email);
      builder.Append(", DisplayName = ");
      builder.Append(this.DisplayName);
      builder.Append(", AvatarUrl = ");
      builder.Append(this.AvatarUrl);
      return true;
    }

    // Token: 0x0600011D RID: 285 RVA: 0x0001E844 File Offset: 0x0001E844
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(UserProfile left, UserProfile right)
    {
      return !(left == right);
    }

    // Token: 0x0600011E RID: 286 RVA: 0x0001E850 File Offset: 0x0001E850
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(UserProfile left, UserProfile right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600011F RID: 287 RVA: 0x0001E864 File Offset: 0x0001E864
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UserId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Email>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AvatarUrl>k__BackingField);
    }

    // Token: 0x06000120 RID: 288 RVA: 0x0001E8E0 File Offset: 0x0001E8E0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as UserProfile);
    }

    // Token: 0x06000121 RID: 289 RVA: 0x0001E8F0 File Offset: 0x0001E8F0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(UserProfile other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<UserId>k__BackingField, other.<UserId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Email>k__BackingField, other.<Email>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AvatarUrl>k__BackingField, other.<AvatarUrl>k__BackingField));
    }

    // Token: 0x06000123 RID: 291 RVA: 0x0001E984 File Offset: 0x0001E984
    [CompilerGenerated]
    private UserProfile(UserProfile original)
    {
      this.UserId = original.<UserId>k__BackingField;
      this.Email = original.<Email>k__BackingField;
      this.DisplayName = original.<DisplayName>k__BackingField;
      this.AvatarUrl = original.<AvatarUrl>k__BackingField;
    }

    // Token: 0x06000124 RID: 292 RVA: 0x0001E9BC File Offset: 0x0001E9BC
    [CompilerGenerated]
    public void Deconstruct(out string UserId, out string Email, [Nullable(2)] out string DisplayName, [Nullable(2)] out string AvatarUrl)
    {
      UserId = this.UserId;
      Email = this.Email;
      DisplayName = this.DisplayName;
      AvatarUrl = this.AvatarUrl;
    }
  }
}
