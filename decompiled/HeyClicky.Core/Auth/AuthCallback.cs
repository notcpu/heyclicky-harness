using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Auth
{
  // Token: 0x02000223 RID: 547
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class AuthCallback : IEquatable<AuthCallback>
  {
    // Token: 0x06002003 RID: 8195 RVA: 0x003F3F64 File Offset: 0x003F3F64
    public AuthCallback([Nullable(1)] string AccessToken, string RefreshToken, int? ExpiresIn, string ReferralCode, string State = null)
    {
      this.AccessToken = AccessToken;
      this.RefreshToken = RefreshToken;
      this.ExpiresIn = ExpiresIn;
      this.ReferralCode = ReferralCode;
      this.State = State;
      base..ctor();
    }

    // Token: 0x17000731 RID: 1841
    // (get) Token: 0x06002004 RID: 8196 RVA: 0x003F3F94 File Offset: 0x003F3F94
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(AuthCallback);
      }
    }

    // Token: 0x17000732 RID: 1842
    // (get) Token: 0x06002005 RID: 8197 RVA: 0x003F3FA0 File Offset: 0x003F3FA0
    // (set) Token: 0x06002006 RID: 8198 RVA: 0x003F3FA8 File Offset: 0x003F3FA8
    [Nullable(1)]
    public string AccessToken
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000733 RID: 1843
    // (get) Token: 0x06002007 RID: 8199 RVA: 0x003F3FB4 File Offset: 0x003F3FB4
    // (set) Token: 0x06002008 RID: 8200 RVA: 0x003F3FBC File Offset: 0x003F3FBC
    public string RefreshToken { get; set; }

    // Token: 0x17000734 RID: 1844
    // (get) Token: 0x06002009 RID: 8201 RVA: 0x003F3FC8 File Offset: 0x003F3FC8
    // (set) Token: 0x0600200A RID: 8202 RVA: 0x003F3FD0 File Offset: 0x003F3FD0
    public int? ExpiresIn { get; set; }

    // Token: 0x17000735 RID: 1845
    // (get) Token: 0x0600200B RID: 8203 RVA: 0x003F3FDC File Offset: 0x003F3FDC
    // (set) Token: 0x0600200C RID: 8204 RVA: 0x003F3FE4 File Offset: 0x003F3FE4
    public string ReferralCode { get; set; }

    // Token: 0x17000736 RID: 1846
    // (get) Token: 0x0600200D RID: 8205 RVA: 0x003F3FF0 File Offset: 0x003F3FF0
    // (set) Token: 0x0600200E RID: 8206 RVA: 0x003F3FF8 File Offset: 0x003F3FF8
    public string State { get; set; }

    // Token: 0x0600200F RID: 8207 RVA: 0x003F4004 File Offset: 0x003F4004
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AuthCallback");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06002010 RID: 8208 RVA: 0x003F4050 File Offset: 0x003F4050
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("AccessToken = ");
      builder.Append(this.AccessToken);
      builder.Append(", RefreshToken = ");
      builder.Append(this.RefreshToken);
      builder.Append(", ExpiresIn = ");
      builder.Append(this.ExpiresIn.ToString());
      builder.Append(", ReferralCode = ");
      builder.Append(this.ReferralCode);
      builder.Append(", State = ");
      builder.Append(this.State);
      return true;
    }

    // Token: 0x06002011 RID: 8209 RVA: 0x003F40F0 File Offset: 0x003F40F0
    [CompilerGenerated]
    public static bool operator !=(AuthCallback left, AuthCallback right)
    {
      return !(left == right);
    }

    // Token: 0x06002012 RID: 8210 RVA: 0x003F40FC File Offset: 0x003F40FC
    [CompilerGenerated]
    public static bool operator ==(AuthCallback left, AuthCallback right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06002013 RID: 8211 RVA: 0x003F4110 File Offset: 0x003F4110
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AccessToken>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RefreshToken>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<ExpiresIn>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ReferralCode>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<State>k__BackingField);
    }

    // Token: 0x06002014 RID: 8212 RVA: 0x003F41A0 File Offset: 0x003F41A0
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AuthCallback);
    }

    // Token: 0x06002015 RID: 8213 RVA: 0x003F41B0 File Offset: 0x003F41B0
    [CompilerGenerated]
    public bool Equals(AuthCallback other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<AccessToken>k__BackingField, other.<AccessToken>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RefreshToken>k__BackingField, other.<RefreshToken>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<ExpiresIn>k__BackingField, other.<ExpiresIn>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ReferralCode>k__BackingField, other.<ReferralCode>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<State>k__BackingField, other.<State>k__BackingField));
    }

    // Token: 0x06002017 RID: 8215 RVA: 0x003F4260 File Offset: 0x003F4260
    [CompilerGenerated]
    private AuthCallback([Nullable(1)] AuthCallback original)
    {
      this.AccessToken = original.<AccessToken>k__BackingField;
      this.RefreshToken = original.<RefreshToken>k__BackingField;
      this.ExpiresIn = original.<ExpiresIn>k__BackingField;
      this.ReferralCode = original.<ReferralCode>k__BackingField;
      this.State = original.<State>k__BackingField;
    }

    // Token: 0x06002018 RID: 8216 RVA: 0x003F42B0 File Offset: 0x003F42B0
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string AccessToken, out string RefreshToken, out int? ExpiresIn, out string ReferralCode, out string State)
    {
      AccessToken = this.AccessToken;
      RefreshToken = this.RefreshToken;
      ExpiresIn = this.ExpiresIn;
      ReferralCode = this.ReferralCode;
      State = this.State;
    }
  }
}
