using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Connectors
{
  // Token: 0x020001D3 RID: 467
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class McpOAuthTokens : IEquatable<McpOAuthTokens>
  {
    // Token: 0x06001AE1 RID: 6881 RVA: 0x003D943C File Offset: 0x003D943C
    public McpOAuthTokens(string AccessToken, [Nullable(2)] string RefreshToken, DateTimeOffset? ExpiresAt)
    {
      this.AccessToken = AccessToken;
      this.RefreshToken = RefreshToken;
      this.ExpiresAt = ExpiresAt;
      base..ctor();
    }

    // Token: 0x170005F2 RID: 1522
    // (get) Token: 0x06001AE2 RID: 6882 RVA: 0x003D945C File Offset: 0x003D945C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(McpOAuthTokens);
      }
    }

    // Token: 0x170005F3 RID: 1523
    // (get) Token: 0x06001AE3 RID: 6883 RVA: 0x003D9468 File Offset: 0x003D9468
    // (set) Token: 0x06001AE4 RID: 6884 RVA: 0x003D9470 File Offset: 0x003D9470
    public string AccessToken { get; set; }

    // Token: 0x170005F4 RID: 1524
    // (get) Token: 0x06001AE5 RID: 6885 RVA: 0x003D947C File Offset: 0x003D947C
    // (set) Token: 0x06001AE6 RID: 6886 RVA: 0x003D9484 File Offset: 0x003D9484
    [Nullable(2)]
    public string RefreshToken
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170005F5 RID: 1525
    // (get) Token: 0x06001AE7 RID: 6887 RVA: 0x003D9490 File Offset: 0x003D9490
    // (set) Token: 0x06001AE8 RID: 6888 RVA: 0x003D9498 File Offset: 0x003D9498
    public DateTimeOffset? ExpiresAt { get; set; }

    // Token: 0x06001AE9 RID: 6889 RVA: 0x003D94A4 File Offset: 0x003D94A4
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("McpOAuthTokens");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001AEA RID: 6890 RVA: 0x003D94F0 File Offset: 0x003D94F0
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
      return true;
    }

    // Token: 0x06001AEB RID: 6891 RVA: 0x003D955C File Offset: 0x003D955C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(McpOAuthTokens left, McpOAuthTokens right)
    {
      return !(left == right);
    }

    // Token: 0x06001AEC RID: 6892 RVA: 0x003D9568 File Offset: 0x003D9568
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(McpOAuthTokens left, McpOAuthTokens right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001AED RID: 6893 RVA: 0x003D957C File Offset: 0x003D957C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AccessToken>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RefreshToken>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<ExpiresAt>k__BackingField);
    }

    // Token: 0x06001AEE RID: 6894 RVA: 0x003D95E0 File Offset: 0x003D95E0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as McpOAuthTokens);
    }

    // Token: 0x06001AEF RID: 6895 RVA: 0x003D95F0 File Offset: 0x003D95F0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(McpOAuthTokens other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<AccessToken>k__BackingField, other.<AccessToken>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RefreshToken>k__BackingField, other.<RefreshToken>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<ExpiresAt>k__BackingField, other.<ExpiresAt>k__BackingField));
    }

    // Token: 0x06001AF1 RID: 6897 RVA: 0x003D966C File Offset: 0x003D966C
    [CompilerGenerated]
    private McpOAuthTokens(McpOAuthTokens original)
    {
      this.AccessToken = original.<AccessToken>k__BackingField;
      this.RefreshToken = original.<RefreshToken>k__BackingField;
      this.ExpiresAt = original.<ExpiresAt>k__BackingField;
    }

    // Token: 0x06001AF2 RID: 6898 RVA: 0x003D9698 File Offset: 0x003D9698
    [CompilerGenerated]
    public void Deconstruct(out string AccessToken, [Nullable(2)] out string RefreshToken, out DateTimeOffset? ExpiresAt)
    {
      AccessToken = this.AccessToken;
      RefreshToken = this.RefreshToken;
      ExpiresAt = this.ExpiresAt;
    }
  }
}
