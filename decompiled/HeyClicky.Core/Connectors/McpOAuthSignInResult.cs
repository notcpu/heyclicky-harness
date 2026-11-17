using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Connectors
{
  // Token: 0x020001D5 RID: 469
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class McpOAuthSignInResult : IEquatable<McpOAuthSignInResult>
  {
    // Token: 0x06001B03 RID: 6915 RVA: 0x003D9894 File Offset: 0x003D9894
    public McpOAuthSignInResult(McpOAuthTokens Tokens, Uri TokenEndpoint, McpOAuthClientRegistration Client, bool RegisteredDynamically)
    {
      this.Tokens = Tokens;
      this.TokenEndpoint = TokenEndpoint;
      this.Client = Client;
      this.RegisteredDynamically = RegisteredDynamically;
      base..ctor();
    }

    // Token: 0x170005F9 RID: 1529
    // (get) Token: 0x06001B04 RID: 6916 RVA: 0x003D98BC File Offset: 0x003D98BC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(McpOAuthSignInResult);
      }
    }

    // Token: 0x170005FA RID: 1530
    // (get) Token: 0x06001B05 RID: 6917 RVA: 0x003D98C8 File Offset: 0x003D98C8
    // (set) Token: 0x06001B06 RID: 6918 RVA: 0x003D98D0 File Offset: 0x003D98D0
    public McpOAuthTokens Tokens { get; set; }

    // Token: 0x170005FB RID: 1531
    // (get) Token: 0x06001B07 RID: 6919 RVA: 0x003D98DC File Offset: 0x003D98DC
    // (set) Token: 0x06001B08 RID: 6920 RVA: 0x003D98E4 File Offset: 0x003D98E4
    public Uri TokenEndpoint { get; set; }

    // Token: 0x170005FC RID: 1532
    // (get) Token: 0x06001B09 RID: 6921 RVA: 0x003D98F0 File Offset: 0x003D98F0
    // (set) Token: 0x06001B0A RID: 6922 RVA: 0x003D98F8 File Offset: 0x003D98F8
    public McpOAuthClientRegistration Client { get; set; }

    // Token: 0x170005FD RID: 1533
    // (get) Token: 0x06001B0B RID: 6923 RVA: 0x003D9904 File Offset: 0x003D9904
    // (set) Token: 0x06001B0C RID: 6924 RVA: 0x003D990C File Offset: 0x003D990C
    public bool RegisteredDynamically { get; set; }

    // Token: 0x06001B0D RID: 6925 RVA: 0x003D9918 File Offset: 0x003D9918
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("McpOAuthSignInResult");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001B0E RID: 6926 RVA: 0x003D9964 File Offset: 0x003D9964
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Tokens = ");
      builder.Append(this.Tokens);
      builder.Append(", TokenEndpoint = ");
      builder.Append(this.TokenEndpoint);
      builder.Append(", Client = ");
      builder.Append(this.Client);
      builder.Append(", RegisteredDynamically = ");
      builder.Append(this.RegisteredDynamically.ToString());
      return true;
    }

    // Token: 0x06001B0F RID: 6927 RVA: 0x003D99EC File Offset: 0x003D99EC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(McpOAuthSignInResult left, McpOAuthSignInResult right)
    {
      return !(left == right);
    }

    // Token: 0x06001B10 RID: 6928 RVA: 0x003D99F8 File Offset: 0x003D99F8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(McpOAuthSignInResult left, McpOAuthSignInResult right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001B11 RID: 6929 RVA: 0x003D9A0C File Offset: 0x003D9A0C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<McpOAuthTokens>.Default.GetHashCode(this.<Tokens>k__BackingField)) * -1521134295 + EqualityComparer<Uri>.Default.GetHashCode(this.<TokenEndpoint>k__BackingField)) * -1521134295 + EqualityComparer<McpOAuthClientRegistration>.Default.GetHashCode(this.<Client>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<RegisteredDynamically>k__BackingField);
    }

    // Token: 0x06001B12 RID: 6930 RVA: 0x003D9A88 File Offset: 0x003D9A88
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as McpOAuthSignInResult);
    }

    // Token: 0x06001B13 RID: 6931 RVA: 0x003D9A98 File Offset: 0x003D9A98
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(McpOAuthSignInResult other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<McpOAuthTokens>.Default.Equals(this.<Tokens>k__BackingField, other.<Tokens>k__BackingField) && EqualityComparer<Uri>.Default.Equals(this.<TokenEndpoint>k__BackingField, other.<TokenEndpoint>k__BackingField) && EqualityComparer<McpOAuthClientRegistration>.Default.Equals(this.<Client>k__BackingField, other.<Client>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<RegisteredDynamically>k__BackingField, other.<RegisteredDynamically>k__BackingField));
    }

    // Token: 0x06001B15 RID: 6933 RVA: 0x003D9B2C File Offset: 0x003D9B2C
    [CompilerGenerated]
    private McpOAuthSignInResult(McpOAuthSignInResult original)
    {
      this.Tokens = original.<Tokens>k__BackingField;
      this.TokenEndpoint = original.<TokenEndpoint>k__BackingField;
      this.Client = original.<Client>k__BackingField;
      this.RegisteredDynamically = original.<RegisteredDynamically>k__BackingField;
    }

    // Token: 0x06001B16 RID: 6934 RVA: 0x003D9B64 File Offset: 0x003D9B64
    [CompilerGenerated]
    public void Deconstruct(out McpOAuthTokens Tokens, out Uri TokenEndpoint, out McpOAuthClientRegistration Client, out bool RegisteredDynamically)
    {
      Tokens = this.Tokens;
      TokenEndpoint = this.TokenEndpoint;
      Client = this.Client;
      RegisteredDynamically = this.RegisteredDynamically;
    }
  }
}
