using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Agents.Backend
{
  // Token: 0x0200006F RID: 111
  public static class ComposioSessionReuse
  {
    // Token: 0x060008DB RID: 2267 RVA: 0x00102634 File Offset: 0x00102634
    public static bool IsStale(DateTimeOffset fetchedAt, DateTimeOffset now, TimeSpan lifetime)
    {
      TimeSpan timeSpan = now - fetchedAt;
      return timeSpan < TimeSpan.Zero || timeSpan >= lifetime;
    }

    // Token: 0x060008DC RID: 2268 RVA: 0x00102660 File Offset: 0x00102660
    [NullableContext(2)]
    [return: Nullable(new byte[] { 2, 1 })]
    public static IReadOnlySet<string> ConnectedToolkits(AgentIntegrationCatalog catalog)
    {
      if (catalog != null)
      {
        return (from t in catalog.Integrations.Where<AgentIntegration>((AgentIntegration i) => !i.IsLocalIntegration).SelectMany<AgentIntegration, AgentIntegrationToolkit>((AgentIntegration i) => i.Toolkits)
          where t.IsConnected
          select t.Slug.ToLowerInvariant()).ToHashSet<string>();
      }
      return null;
    }

    // Token: 0x060008DD RID: 2269 RVA: 0x00102710 File Offset: 0x00102710
    [NullableContext(1)]
    public static bool ShouldRefetchForConnectedApps([Nullable(new byte[] { 2, 1 })] IReadOnlySet<string> knownToApp, IReadOnlySet<string> sessionToolkits, [Nullable(new byte[] { 2, 1 })] ref IReadOnlySet<string> lastRefetchedFor)
    {
      if (knownToApp == null || knownToApp.SetEquals(sessionToolkits) || (lastRefetchedFor != null && knownToApp.SetEquals(lastRefetchedFor)))
      {
        return false;
      }
      lastRefetchedFor = knownToApp;
      return true;
    }
  }
}
