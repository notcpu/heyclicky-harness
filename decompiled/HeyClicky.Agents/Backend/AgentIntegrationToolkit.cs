using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Backend
{
  // Token: 0x0200006B RID: 107
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentIntegrationToolkit : IEquatable<AgentIntegrationToolkit>
  {
    // Token: 0x0600088D RID: 2189 RVA: 0x001015FC File Offset: 0x001015FC
    public AgentIntegrationToolkit(string Slug, string Name, [Nullable(2)] string Logo, bool IsConnected)
    {
      this.Slug = Slug;
      this.Name = Name;
      this.Logo = Logo;
      this.IsConnected = IsConnected;
      base..ctor();
    }

    // Token: 0x17000225 RID: 549
    // (get) Token: 0x0600088E RID: 2190 RVA: 0x00101624 File Offset: 0x00101624
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentIntegrationToolkit);
      }
    }

    // Token: 0x17000226 RID: 550
    // (get) Token: 0x0600088F RID: 2191 RVA: 0x00101630 File Offset: 0x00101630
    // (set) Token: 0x06000890 RID: 2192 RVA: 0x00101638 File Offset: 0x00101638
    public string Slug { get; set; }

    // Token: 0x17000227 RID: 551
    // (get) Token: 0x06000891 RID: 2193 RVA: 0x00101644 File Offset: 0x00101644
    // (set) Token: 0x06000892 RID: 2194 RVA: 0x0010164C File Offset: 0x0010164C
    public string Name { get; set; }

    // Token: 0x17000228 RID: 552
    // (get) Token: 0x06000893 RID: 2195 RVA: 0x00101658 File Offset: 0x00101658
    // (set) Token: 0x06000894 RID: 2196 RVA: 0x00101660 File Offset: 0x00101660
    [Nullable(2)]
    public string Logo
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000229 RID: 553
    // (get) Token: 0x06000895 RID: 2197 RVA: 0x0010166C File Offset: 0x0010166C
    // (set) Token: 0x06000896 RID: 2198 RVA: 0x00101674 File Offset: 0x00101674
    public bool IsConnected { get; set; }

    // Token: 0x06000897 RID: 2199 RVA: 0x00101680 File Offset: 0x00101680
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentIntegrationToolkit");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000898 RID: 2200 RVA: 0x001016CC File Offset: 0x001016CC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Slug = ");
      builder.Append(this.Slug);
      builder.Append(", Name = ");
      builder.Append(this.Name);
      builder.Append(", Logo = ");
      builder.Append(this.Logo);
      builder.Append(", IsConnected = ");
      builder.Append(this.IsConnected.ToString());
      return true;
    }

    // Token: 0x06000899 RID: 2201 RVA: 0x00101754 File Offset: 0x00101754
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentIntegrationToolkit left, AgentIntegrationToolkit right)
    {
      return !(left == right);
    }

    // Token: 0x0600089A RID: 2202 RVA: 0x00101760 File Offset: 0x00101760
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentIntegrationToolkit left, AgentIntegrationToolkit right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600089B RID: 2203 RVA: 0x00101774 File Offset: 0x00101774
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Slug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Logo>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsConnected>k__BackingField);
    }

    // Token: 0x0600089C RID: 2204 RVA: 0x001017F0 File Offset: 0x001017F0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentIntegrationToolkit);
    }

    // Token: 0x0600089D RID: 2205 RVA: 0x00101800 File Offset: 0x00101800
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentIntegrationToolkit other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Slug>k__BackingField, other.<Slug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Logo>k__BackingField, other.<Logo>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsConnected>k__BackingField, other.<IsConnected>k__BackingField));
    }

    // Token: 0x0600089F RID: 2207 RVA: 0x00101894 File Offset: 0x00101894
    [CompilerGenerated]
    private AgentIntegrationToolkit(AgentIntegrationToolkit original)
    {
      this.Slug = original.<Slug>k__BackingField;
      this.Name = original.<Name>k__BackingField;
      this.Logo = original.<Logo>k__BackingField;
      this.IsConnected = original.<IsConnected>k__BackingField;
    }

    // Token: 0x060008A0 RID: 2208 RVA: 0x001018CC File Offset: 0x001018CC
    [CompilerGenerated]
    public void Deconstruct(out string Slug, out string Name, [Nullable(2)] out string Logo, out bool IsConnected)
    {
      Slug = this.Slug;
      Name = this.Name;
      Logo = this.Logo;
      IsConnected = this.IsConnected;
    }
  }
}
