using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Backend;
using HeyClicky.App.Settings;

namespace HeyClicky.App.Services
{
  // Token: 0x020000E1 RID: 225
  [NullableContext(1)]
  [Nullable(0)]
  public static class ConnectedApps
  {
    // Token: 0x14000027 RID: 39
    // (add) Token: 0x06000AD5 RID: 2773 RVA: 0x00558D04 File Offset: 0x00558D04
    // (remove) Token: 0x06000AD6 RID: 2774 RVA: 0x00558D38 File Offset: 0x00558D38
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public static event Action Changed;

    // Token: 0x06000AD7 RID: 2775 RVA: 0x00558D6C File Offset: 0x00558D6C
    public static void Initialize(AgentService service, Func<string> worker)
    {
      ConnectedApps.agents = service;
      ConnectedApps.workerBaseUrl = worker;
      ConnectorAuth.Changed += delegate
      {
        Action changed = ConnectedApps.Changed;
        if (changed == null)
        {
          return;
        }
        changed();
      };
      ObsidianVault.Changed += delegate
      {
        Action changed2 = ConnectedApps.Changed;
        if (changed2 == null)
        {
          return;
        }
        changed2();
      };
    }

    // Token: 0x170001C9 RID: 457
    // (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00558DD0 File Offset: 0x00558DD0
    [Nullable(2)]
    public static AgentIntegrationCatalog Catalog
    {
      [NullableContext(2)]
      get
      {
        return ConnectedApps.catalog;
      }
    }

    // Token: 0x06000AD9 RID: 2777 RVA: 0x00558DD8 File Offset: 0x00558DD8
    public static void SetCatalog(AgentIntegrationCatalog fresh)
    {
      ConnectedApps.catalog = fresh;
      ConnectedApps.fetchedAt = DateTimeOffset.Now;
      Action changed = ConnectedApps.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06000ADA RID: 2778 RVA: 0x00558DFC File Offset: 0x00558DFC
    public static void RefreshIfStale()
    {
      if (DateTimeOffset.Now - ConnectedApps.fetchedAt > ConnectedApps.RefreshAfter)
      {
        ConnectedApps.RefreshAsync();
      }
    }

    // Token: 0x06000ADB RID: 2779 RVA: 0x00558E20 File Offset: 0x00558E20
    public static async Task RefreshAsync()
    {
      if (ConnectedApps.agents != null && Interlocked.Exchange(ref ConnectedApps.refreshing, 1) != 1)
      {
        try
        {
          AgentIntegrationCatalog agentIntegrationCatalog = await ConnectedApps.agents.ListIntegrationsAsync(default(CancellationToken)).ConfigureAwait(false);
          ConnectedApps.SetCatalog(agentIntegrationCatalog);
          string text = "apps";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 2);
          defaultInterpolatedStringHandler.AppendLiteral("catalog: ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(agentIntegrationCatalog.Integrations.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" apps, ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(agentIntegrationCatalog.Integrations.Count<AgentIntegration>((AgentIntegration i) => i.IsActiveConnection));
          defaultInterpolatedStringHandler.AppendLiteral(" connected");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        }
        catch (Exception ex)
        {
          Log.Warn("apps", "/agent/integrations: " + ex.Message);
        }
        finally
        {
          Interlocked.Exchange(ref ConnectedApps.refreshing, 0);
        }
      }
    }

    // Token: 0x06000ADC RID: 2780 RVA: 0x00558E5C File Offset: 0x00558E5C
    public static IReadOnlyList<AppEntry> All()
    {
      List<AppEntry> list = new List<AppEntry>();
      AgentIntegrationCatalog agentIntegrationCatalog = ConnectedApps.catalog;
      foreach (AgentIntegration agentIntegration in (((agentIntegrationCatalog != null) ? agentIntegrationCatalog.Integrations : null) ?? Array.Empty<AgentIntegration>()))
      {
        if (!agentIntegration.IsLocalIntegration)
        {
          list.Add(new AppEntry(agentIntegration.Id, agentIntegration.DisplayName, agentIntegration.PrimaryToolkitSlug, agentIntegration.LogoUrl, agentIntegration.Status, agentIntegration.Description, false));
        }
      }
      AgentIntegration agentIntegration2 = ObsidianVault.Row();
      list.Add(new AppEntry(agentIntegration2.Id, agentIntegration2.DisplayName, "obsidian", null, agentIntegration2.Status, agentIntegration2.Description, true));
      Func<string> func = ConnectedApps.workerBaseUrl;
      string text = ((func != null) ? func() : null) ?? "";
      foreach (CustomConnector customConnector in from c in CustomConnectorStore.Load()
        orderby c.CreatedAt
        select c)
      {
        list.Add(new AppEntry(customConnector.IntegrationId, customConnector.Name, customConnector.ServerKey, customConnector.LogoUrl(text), ConnectorAuth.NeedsReconnect(customConnector.Id) ? "needs_reconnect" : "connected", (customConnector.EffectiveCapabilityDescription.Length > 0) ? customConnector.EffectiveCapabilityDescription : ("Custom MCP connector · " + customConnector.LocationSummary), true));
      }
      return list.OrderBy<AppEntry, int>((AppEntry r) => ConnectedApps.RecommendedRank(r).GetValueOrDefault(int.MaxValue)).ThenBy<AppEntry, string>((AppEntry r) => r.DisplayName, StringComparer.CurrentCultureIgnoreCase).ToList<AppEntry>();
    }

    // Token: 0x06000ADD RID: 2781 RVA: 0x00559064 File Offset: 0x00559064
    public static IReadOnlyList<AppEntry> Connected()
    {
      return (from a in ConnectedApps.All()
        where a.Status == "connected"
        select a).ToList<AppEntry>();
    }

    // Token: 0x06000ADE RID: 2782 RVA: 0x00559094 File Offset: 0x00559094
    private static int? RecommendedRank(AppEntry a)
    {
      int num = Array.FindIndex<string>(ConnectedApps.Recommended, (string r) => string.Equals(r, a.Id, StringComparison.OrdinalIgnoreCase) || string.Equals(r, a.ToolkitSlug, StringComparison.OrdinalIgnoreCase));
      if (num >= 0)
      {
        return new int?(num);
      }
      return null;
    }

    // Token: 0x06000ADF RID: 2783 RVA: 0x005590DC File Offset: 0x005590DC
    [return: Nullable(2)]
    public static AppEntry Find(string providerKey)
    {
      string key = providerKey.Trim().ToLowerInvariant();
      foreach (AppEntry appEntry in ConnectedApps.All())
      {
        if (string.Equals(appEntry.Id, key, StringComparison.OrdinalIgnoreCase) || string.Equals(appEntry.ToolkitSlug, key, StringComparison.OrdinalIgnoreCase))
        {
          return appEntry;
        }
      }
      AgentIntegrationCatalog agentIntegrationCatalog = ConnectedApps.catalog;
      Func<AgentIntegrationToolkit, bool> <>9__1;
      AgentIntegration agentIntegration = ((agentIntegrationCatalog != null) ? agentIntegrationCatalog.Integrations.FirstOrDefault<AgentIntegration>(delegate(AgentIntegration i)
      {
        IEnumerable<AgentIntegrationToolkit> toolkits = i.Toolkits;
        Func<AgentIntegrationToolkit, bool> func;
        if ((func = <>9__1) == null)
        {
          func = (<>9__1 = (AgentIntegrationToolkit t) => string.Equals(t.Slug, key, StringComparison.OrdinalIgnoreCase));
        }
        return toolkits.Any<AgentIntegrationToolkit>(func);
      }) : null);
      if (agentIntegration != null)
      {
        return new AppEntry(agentIntegration.Id, agentIntegration.DisplayName, agentIntegration.PrimaryToolkitSlug, agentIntegration.LogoUrl, agentIntegration.Status, agentIntegration.Description, false);
      }
      return null;
    }

    // Token: 0x06000AE0 RID: 2784 RVA: 0x005591BC File Offset: 0x005591BC
    public static IReadOnlyList<AppEntry> SampleApps()
    {
      AgentIntegrationCatalog agentIntegrationCatalog = ConnectedApps.catalog;
      List<AppEntry> list = (from i in ((agentIntegrationCatalog != null) ? agentIntegrationCatalog.Integrations : null) ?? Array.Empty<AgentIntegration>()
        where !i.IsLocalIntegration
        select new AppEntry(i.Id, i.DisplayName, i.PrimaryToolkitSlug, i.LogoUrl, "connected", i.Description, false)).ToList<AppEntry>();
      List<AppEntry> sample = new List<AppEntry>();
      string[] array = new string[] { "gmail", "googlecalendar", "notion", "slack" };
      for (int j = 0; j < array.Length; j++)
      {
        string key = array[j];
        AppEntry appEntry = list.FirstOrDefault<AppEntry>((AppEntry a) => !sample.Contains(a) && string.Concat(new string[] { a.Id, " ", a.ToolkitSlug, " ", a.DisplayName }).ToLowerInvariant().Replace(" ", "")
          .Contains(key, StringComparison.Ordinal));
        if (appEntry != null)
        {
          sample.Add(appEntry);
        }
      }
      foreach (AppEntry appEntry2 in list)
      {
        if (sample.Count >= 4)
        {
          break;
        }
        if (!sample.Contains(appEntry2))
        {
          sample.Add(appEntry2);
        }
      }
      return sample;
    }

    // Token: 0x040003E7 RID: 999
    private static readonly TimeSpan RefreshAfter = TimeSpan.FromMinutes(10L);

    // Token: 0x040003E8 RID: 1000
    [Nullable(2)]
    private static AgentService agents;

    // Token: 0x040003E9 RID: 1001
    [Nullable(new byte[] { 2, 1 })]
    private static Func<string> workerBaseUrl;

    // Token: 0x040003EA RID: 1002
    [Nullable(2)]
    private static AgentIntegrationCatalog catalog;

    // Token: 0x040003EB RID: 1003
    private static DateTimeOffset fetchedAt;

    // Token: 0x040003EC RID: 1004
    private static int refreshing;

    // Token: 0x040003EE RID: 1006
    private static readonly string[] Recommended = new string[]
    {
      "notion", "linear", "github", "googledocs", "googlecalendar", "linkedin", "slack", "gmail", "googlesheets", "googledrive",
      "obsidian"
    };
  }
}
