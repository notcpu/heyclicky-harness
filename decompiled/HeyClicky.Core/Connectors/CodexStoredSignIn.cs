using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Connectors
{
  // Token: 0x020001CF RID: 463
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class CodexStoredSignIn : IEquatable<CodexStoredSignIn>
  {
    // Token: 0x06001AAE RID: 6830 RVA: 0x003D8918 File Offset: 0x003D8918
    public CodexStoredSignIn([Nullable(1)] string AccessToken, string RefreshToken, DateTimeOffset? ExpiresAt, string ClientId, string ClientSecret)
    {
      this.AccessToken = AccessToken;
      this.RefreshToken = RefreshToken;
      this.ExpiresAt = ExpiresAt;
      this.ClientId = ClientId;
      this.ClientSecret = ClientSecret;
      base..ctor();
    }

    // Token: 0x170005E5 RID: 1509
    // (get) Token: 0x06001AAF RID: 6831 RVA: 0x003D8948 File Offset: 0x003D8948
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(CodexStoredSignIn);
      }
    }

    // Token: 0x170005E6 RID: 1510
    // (get) Token: 0x06001AB0 RID: 6832 RVA: 0x003D8954 File Offset: 0x003D8954
    // (set) Token: 0x06001AB1 RID: 6833 RVA: 0x003D895C File Offset: 0x003D895C
    [Nullable(1)]
    public string AccessToken
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170005E7 RID: 1511
    // (get) Token: 0x06001AB2 RID: 6834 RVA: 0x003D8968 File Offset: 0x003D8968
    // (set) Token: 0x06001AB3 RID: 6835 RVA: 0x003D8970 File Offset: 0x003D8970
    public string RefreshToken { get; set; }

    // Token: 0x170005E8 RID: 1512
    // (get) Token: 0x06001AB4 RID: 6836 RVA: 0x003D897C File Offset: 0x003D897C
    // (set) Token: 0x06001AB5 RID: 6837 RVA: 0x003D8984 File Offset: 0x003D8984
    public DateTimeOffset? ExpiresAt { get; set; }

    // Token: 0x170005E9 RID: 1513
    // (get) Token: 0x06001AB6 RID: 6838 RVA: 0x003D8990 File Offset: 0x003D8990
    // (set) Token: 0x06001AB7 RID: 6839 RVA: 0x003D8998 File Offset: 0x003D8998
    public string ClientId { get; set; }

    // Token: 0x170005EA RID: 1514
    // (get) Token: 0x06001AB8 RID: 6840 RVA: 0x003D89A4 File Offset: 0x003D89A4
    // (set) Token: 0x06001AB9 RID: 6841 RVA: 0x003D89AC File Offset: 0x003D89AC
    public string ClientSecret { get; set; }

    // Token: 0x06001ABA RID: 6842 RVA: 0x003D89B8 File Offset: 0x003D89B8
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("CodexStoredSignIn");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001ABB RID: 6843 RVA: 0x003D8A04 File Offset: 0x003D8A04
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("AccessToken = ");
      builder.Append(this.AccessToken);
      builder.Append(", RefreshToken = ");
      builder.Append(this.RefreshToken);
      builder.Append(", ExpiresAt = ");
      builder.Append(this.ExpiresAt.ToString());
      builder.Append(", ClientId = ");
      builder.Append(this.ClientId);
      builder.Append(", ClientSecret = ");
      builder.Append(this.ClientSecret);
      return true;
    }

    // Token: 0x06001ABC RID: 6844 RVA: 0x003D8AA4 File Offset: 0x003D8AA4
    [CompilerGenerated]
    public static bool operator !=(CodexStoredSignIn left, CodexStoredSignIn right)
    {
      return !(left == right);
    }

    // Token: 0x06001ABD RID: 6845 RVA: 0x003D8AB0 File Offset: 0x003D8AB0
    [CompilerGenerated]
    public static bool operator ==(CodexStoredSignIn left, CodexStoredSignIn right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001ABE RID: 6846 RVA: 0x003D8AC4 File Offset: 0x003D8AC4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AccessToken>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RefreshToken>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<ExpiresAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClientId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClientSecret>k__BackingField);
    }

    // Token: 0x06001ABF RID: 6847 RVA: 0x003D8B54 File Offset: 0x003D8B54
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CodexStoredSignIn);
    }

    // Token: 0x06001AC0 RID: 6848 RVA: 0x003D8B64 File Offset: 0x003D8B64
    [CompilerGenerated]
    public bool Equals(CodexStoredSignIn other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<AccessToken>k__BackingField, other.<AccessToken>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RefreshToken>k__BackingField, other.<RefreshToken>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<ExpiresAt>k__BackingField, other.<ExpiresAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClientId>k__BackingField, other.<ClientId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClientSecret>k__BackingField, other.<ClientSecret>k__BackingField));
    }

    // Token: 0x06001AC2 RID: 6850 RVA: 0x003D8C14 File Offset: 0x003D8C14
    [CompilerGenerated]
    private CodexStoredSignIn([Nullable(1)] CodexStoredSignIn original)
    {
      this.AccessToken = original.<AccessToken>k__BackingField;
      this.RefreshToken = original.<RefreshToken>k__BackingField;
      this.ExpiresAt = original.<ExpiresAt>k__BackingField;
      this.ClientId = original.<ClientId>k__BackingField;
      this.ClientSecret = original.<ClientSecret>k__BackingField;
    }

    // Token: 0x06001AC3 RID: 6851 RVA: 0x003D8C64 File Offset: 0x003D8C64
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string AccessToken, out string RefreshToken, out DateTimeOffset? ExpiresAt, out string ClientId, out string ClientSecret)
    {
      AccessToken = this.AccessToken;
      RefreshToken = this.RefreshToken;
      ExpiresAt = this.ExpiresAt;
      ClientId = this.ClientId;
      ClientSecret = this.ClientSecret;
    }
  }
}
