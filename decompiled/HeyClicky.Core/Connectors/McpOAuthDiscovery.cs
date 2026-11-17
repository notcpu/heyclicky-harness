using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Connectors
{
  // Token: 0x020001D1 RID: 465
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class McpOAuthDiscovery : IEquatable<McpOAuthDiscovery>
  {
    // Token: 0x06001AC9 RID: 6857 RVA: 0x003D905C File Offset: 0x003D905C
    public McpOAuthDiscovery(Uri AuthorizationEndpoint, Uri TokenEndpoint, [Nullable(2)] Uri RegistrationEndpoint, IReadOnlyList<string> Scopes, string Resource)
    {
      this.AuthorizationEndpoint = AuthorizationEndpoint;
      this.TokenEndpoint = TokenEndpoint;
      this.RegistrationEndpoint = RegistrationEndpoint;
      this.Scopes = Scopes;
      this.Resource = Resource;
      this.ResourceScopes = Array.Empty<string>();
      base..ctor();
    }

    // Token: 0x170005EB RID: 1515
    // (get) Token: 0x06001ACA RID: 6858 RVA: 0x003D9094 File Offset: 0x003D9094
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(McpOAuthDiscovery);
      }
    }

    // Token: 0x170005EC RID: 1516
    // (get) Token: 0x06001ACB RID: 6859 RVA: 0x003D90A0 File Offset: 0x003D90A0
    // (set) Token: 0x06001ACC RID: 6860 RVA: 0x003D90A8 File Offset: 0x003D90A8
    public Uri AuthorizationEndpoint { get; set; }

    // Token: 0x170005ED RID: 1517
    // (get) Token: 0x06001ACD RID: 6861 RVA: 0x003D90B4 File Offset: 0x003D90B4
    // (set) Token: 0x06001ACE RID: 6862 RVA: 0x003D90BC File Offset: 0x003D90BC
    public Uri TokenEndpoint { get; set; }

    // Token: 0x170005EE RID: 1518
    // (get) Token: 0x06001ACF RID: 6863 RVA: 0x003D90C8 File Offset: 0x003D90C8
    // (set) Token: 0x06001AD0 RID: 6864 RVA: 0x003D90D0 File Offset: 0x003D90D0
    [Nullable(2)]
    public Uri RegistrationEndpoint
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170005EF RID: 1519
    // (get) Token: 0x06001AD1 RID: 6865 RVA: 0x003D90DC File Offset: 0x003D90DC
    // (set) Token: 0x06001AD2 RID: 6866 RVA: 0x003D90E4 File Offset: 0x003D90E4
    public IReadOnlyList<string> Scopes { get; set; }

    // Token: 0x170005F0 RID: 1520
    // (get) Token: 0x06001AD3 RID: 6867 RVA: 0x003D90F0 File Offset: 0x003D90F0
    // (set) Token: 0x06001AD4 RID: 6868 RVA: 0x003D90F8 File Offset: 0x003D90F8
    public string Resource { get; set; }

    // Token: 0x170005F1 RID: 1521
    // (get) Token: 0x06001AD5 RID: 6869 RVA: 0x003D9104 File Offset: 0x003D9104
    // (set) Token: 0x06001AD6 RID: 6870 RVA: 0x003D910C File Offset: 0x003D910C
    public IReadOnlyList<string> ResourceScopes { get; set; }

    // Token: 0x06001AD7 RID: 6871 RVA: 0x003D9118 File Offset: 0x003D9118
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("McpOAuthDiscovery");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001AD8 RID: 6872 RVA: 0x003D9164 File Offset: 0x003D9164
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("AuthorizationEndpoint = ");
      builder.Append(this.AuthorizationEndpoint);
      builder.Append(", TokenEndpoint = ");
      builder.Append(this.TokenEndpoint);
      builder.Append(", RegistrationEndpoint = ");
      builder.Append(this.RegistrationEndpoint);
      builder.Append(", Scopes = ");
      builder.Append(this.Scopes);
      builder.Append(", Resource = ");
      builder.Append(this.Resource);
      builder.Append(", ResourceScopes = ");
      builder.Append(this.ResourceScopes);
      return true;
    }

    // Token: 0x06001AD9 RID: 6873 RVA: 0x003D9210 File Offset: 0x003D9210
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(McpOAuthDiscovery left, McpOAuthDiscovery right)
    {
      return !(left == right);
    }

    // Token: 0x06001ADA RID: 6874 RVA: 0x003D921C File Offset: 0x003D921C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(McpOAuthDiscovery left, McpOAuthDiscovery right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001ADB RID: 6875 RVA: 0x003D9230 File Offset: 0x003D9230
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Uri>.Default.GetHashCode(this.<AuthorizationEndpoint>k__BackingField)) * -1521134295 + EqualityComparer<Uri>.Default.GetHashCode(this.<TokenEndpoint>k__BackingField)) * -1521134295 + EqualityComparer<Uri>.Default.GetHashCode(this.<RegistrationEndpoint>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Scopes>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Resource>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ResourceScopes>k__BackingField);
    }

    // Token: 0x06001ADC RID: 6876 RVA: 0x003D92D8 File Offset: 0x003D92D8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as McpOAuthDiscovery);
    }

    // Token: 0x06001ADD RID: 6877 RVA: 0x003D92E8 File Offset: 0x003D92E8
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(McpOAuthDiscovery other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Uri>.Default.Equals(this.<AuthorizationEndpoint>k__BackingField, other.<AuthorizationEndpoint>k__BackingField) && EqualityComparer<Uri>.Default.Equals(this.<TokenEndpoint>k__BackingField, other.<TokenEndpoint>k__BackingField) && EqualityComparer<Uri>.Default.Equals(this.<RegistrationEndpoint>k__BackingField, other.<RegistrationEndpoint>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Scopes>k__BackingField, other.<Scopes>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Resource>k__BackingField, other.<Resource>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ResourceScopes>k__BackingField, other.<ResourceScopes>k__BackingField));
    }

    // Token: 0x06001ADF RID: 6879 RVA: 0x003D93B4 File Offset: 0x003D93B4
    [CompilerGenerated]
    private McpOAuthDiscovery(McpOAuthDiscovery original)
    {
      this.AuthorizationEndpoint = original.<AuthorizationEndpoint>k__BackingField;
      this.TokenEndpoint = original.<TokenEndpoint>k__BackingField;
      this.RegistrationEndpoint = original.<RegistrationEndpoint>k__BackingField;
      this.Scopes = original.<Scopes>k__BackingField;
      this.Resource = original.<Resource>k__BackingField;
      this.ResourceScopes = original.<ResourceScopes>k__BackingField;
    }

    // Token: 0x06001AE0 RID: 6880 RVA: 0x003D9410 File Offset: 0x003D9410
    [CompilerGenerated]
    public void Deconstruct(out Uri AuthorizationEndpoint, out Uri TokenEndpoint, [Nullable(2)] out Uri RegistrationEndpoint, out IReadOnlyList<string> Scopes, out string Resource)
    {
      AuthorizationEndpoint = this.AuthorizationEndpoint;
      TokenEndpoint = this.TokenEndpoint;
      RegistrationEndpoint = this.RegistrationEndpoint;
      Scopes = this.Scopes;
      Resource = this.Resource;
    }
  }
}
