using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Backend
{
  // Token: 0x0200006D RID: 109
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentIntegrationCatalog : IEquatable<AgentIntegrationCatalog>
  {
    // Token: 0x060008C9 RID: 2249 RVA: 0x00102338 File Offset: 0x00102338
    public AgentIntegrationCatalog(bool ComposioConfigured, IReadOnlyList<AgentIntegration> Integrations)
    {
      this.ComposioConfigured = ComposioConfigured;
      this.Integrations = Integrations;
      base..ctor();
    }

    // Token: 0x1700023A RID: 570
    // (get) Token: 0x060008CA RID: 2250 RVA: 0x00102350 File Offset: 0x00102350
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentIntegrationCatalog);
      }
    }

    // Token: 0x1700023B RID: 571
    // (get) Token: 0x060008CB RID: 2251 RVA: 0x0010235C File Offset: 0x0010235C
    // (set) Token: 0x060008CC RID: 2252 RVA: 0x00102364 File Offset: 0x00102364
    public bool ComposioConfigured { get; set; }

    // Token: 0x1700023C RID: 572
    // (get) Token: 0x060008CD RID: 2253 RVA: 0x00102370 File Offset: 0x00102370
    // (set) Token: 0x060008CE RID: 2254 RVA: 0x00102378 File Offset: 0x00102378
    public IReadOnlyList<AgentIntegration> Integrations { get; set; }

    // Token: 0x060008CF RID: 2255 RVA: 0x00102384 File Offset: 0x00102384
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentIntegrationCatalog");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060008D0 RID: 2256 RVA: 0x001023D0 File Offset: 0x001023D0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ComposioConfigured = ");
      builder.Append(this.ComposioConfigured.ToString());
      builder.Append(", Integrations = ");
      builder.Append(this.Integrations);
      return true;
    }

    // Token: 0x060008D1 RID: 2257 RVA: 0x00102424 File Offset: 0x00102424
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentIntegrationCatalog left, AgentIntegrationCatalog right)
    {
      return !(left == right);
    }

    // Token: 0x060008D2 RID: 2258 RVA: 0x00102430 File Offset: 0x00102430
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentIntegrationCatalog left, AgentIntegrationCatalog right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060008D3 RID: 2259 RVA: 0x00102444 File Offset: 0x00102444
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ComposioConfigured>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<AgentIntegration>>.Default.GetHashCode(this.<Integrations>k__BackingField);
    }

    // Token: 0x060008D4 RID: 2260 RVA: 0x00102484 File Offset: 0x00102484
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentIntegrationCatalog);
    }

    // Token: 0x060008D5 RID: 2261 RVA: 0x00102494 File Offset: 0x00102494
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentIntegrationCatalog other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<ComposioConfigured>k__BackingField, other.<ComposioConfigured>k__BackingField) && EqualityComparer<IReadOnlyList<AgentIntegration>>.Default.Equals(this.<Integrations>k__BackingField, other.<Integrations>k__BackingField));
    }

    // Token: 0x060008D7 RID: 2263 RVA: 0x001024F8 File Offset: 0x001024F8
    [CompilerGenerated]
    private AgentIntegrationCatalog(AgentIntegrationCatalog original)
    {
      this.ComposioConfigured = original.<ComposioConfigured>k__BackingField;
      this.Integrations = original.<Integrations>k__BackingField;
    }

    // Token: 0x060008D8 RID: 2264 RVA: 0x00102518 File Offset: 0x00102518
    [CompilerGenerated]
    public void Deconstruct(out bool ComposioConfigured, out IReadOnlyList<AgentIntegration> Integrations)
    {
      ComposioConfigured = this.ComposioConfigured;
      Integrations = this.Integrations;
    }
  }
}
