using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Connectors
{
  // Token: 0x020001D4 RID: 468
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class McpOAuthClientRegistration : IEquatable<McpOAuthClientRegistration>
  {
    // Token: 0x06001AF3 RID: 6899 RVA: 0x003D96B8 File Offset: 0x003D96B8
    public McpOAuthClientRegistration(string ClientId, [Nullable(2)] string ClientSecret)
    {
      this.ClientId = ClientId;
      this.ClientSecret = ClientSecret;
      base..ctor();
    }

    // Token: 0x170005F6 RID: 1526
    // (get) Token: 0x06001AF4 RID: 6900 RVA: 0x003D96D0 File Offset: 0x003D96D0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(McpOAuthClientRegistration);
      }
    }

    // Token: 0x170005F7 RID: 1527
    // (get) Token: 0x06001AF5 RID: 6901 RVA: 0x003D96DC File Offset: 0x003D96DC
    // (set) Token: 0x06001AF6 RID: 6902 RVA: 0x003D96E4 File Offset: 0x003D96E4
    public string ClientId { get; set; }

    // Token: 0x170005F8 RID: 1528
    // (get) Token: 0x06001AF7 RID: 6903 RVA: 0x003D96F0 File Offset: 0x003D96F0
    // (set) Token: 0x06001AF8 RID: 6904 RVA: 0x003D96F8 File Offset: 0x003D96F8
    [Nullable(2)]
    public string ClientSecret
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06001AF9 RID: 6905 RVA: 0x003D9704 File Offset: 0x003D9704
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("McpOAuthClientRegistration");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001AFA RID: 6906 RVA: 0x003D9750 File Offset: 0x003D9750
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ClientId = ");
      builder.Append(this.ClientId);
      builder.Append(", ClientSecret = ");
      builder.Append(this.ClientSecret);
      return true;
    }

    // Token: 0x06001AFB RID: 6907 RVA: 0x003D978C File Offset: 0x003D978C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(McpOAuthClientRegistration left, McpOAuthClientRegistration right)
    {
      return !(left == right);
    }

    // Token: 0x06001AFC RID: 6908 RVA: 0x003D9798 File Offset: 0x003D9798
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(McpOAuthClientRegistration left, McpOAuthClientRegistration right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001AFD RID: 6909 RVA: 0x003D97AC File Offset: 0x003D97AC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClientId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClientSecret>k__BackingField);
    }

    // Token: 0x06001AFE RID: 6910 RVA: 0x003D97EC File Offset: 0x003D97EC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as McpOAuthClientRegistration);
    }

    // Token: 0x06001AFF RID: 6911 RVA: 0x003D97FC File Offset: 0x003D97FC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(McpOAuthClientRegistration other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ClientId>k__BackingField, other.<ClientId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClientSecret>k__BackingField, other.<ClientSecret>k__BackingField));
    }

    // Token: 0x06001B01 RID: 6913 RVA: 0x003D9860 File Offset: 0x003D9860
    [CompilerGenerated]
    private McpOAuthClientRegistration(McpOAuthClientRegistration original)
    {
      this.ClientId = original.<ClientId>k__BackingField;
      this.ClientSecret = original.<ClientSecret>k__BackingField;
    }

    // Token: 0x06001B02 RID: 6914 RVA: 0x003D9880 File Offset: 0x003D9880
    [CompilerGenerated]
    public void Deconstruct(out string ClientId, [Nullable(2)] out string ClientSecret)
    {
      ClientId = this.ClientId;
      ClientSecret = this.ClientSecret;
    }
  }
}
