using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Settings;
using HeyClicky.Core;
using HeyClicky.Core.Tools;
using HeyClicky.Core.Voice;
using HeyClicky.Core.Worker;

namespace HeyClicky.App.Wiring
{
  // Token: 0x02000093 RID: 147
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class BrainContext
  {
    // Token: 0x060005D1 RID: 1489 RVA: 0x0052E1B4 File Offset: 0x0052E1B4
    [NullableContext(1)]
    public BrainContext(CoreServices core, IAgentToolHost agents)
    {
      this.core = core;
      this.agents = agents;
    }

    // Token: 0x170000E0 RID: 224
    // (get) Token: 0x060005D2 RID: 1490 RVA: 0x0052E1E4 File Offset: 0x0052E1E4
    // (set) Token: 0x060005D3 RID: 1491 RVA: 0x0052E1EC File Offset: 0x0052E1EC
    public Func<string> JobsNote { get; set; }

    // Token: 0x060005D4 RID: 1492 RVA: 0x0052E1F8 File Offset: 0x0052E1F8
    public string RosterNote()
    {
      Func<string> jobsNote = this.JobsNote;
      if (jobsNote == null)
      {
        return ClickyNotes.RosterNote(this.agents.Clickys, this.agents.Tasks, this.LastReport, DateTimeOffset.Now);
      }
      return jobsNote();
    }

    // Token: 0x060005D5 RID: 1493 RVA: 0x0052E240 File Offset: 0x0052E240
    public string PlanNote()
    {
      this.RefreshIfStale();
      return ClickyNotes.PlanNote(this.plan, this.core.Billing.DisplayedMessagesUsage);
    }

    // Token: 0x060005D6 RID: 1494 RVA: 0x0052E268 File Offset: 0x0052E268
    [NullableContext(1)]
    public BrainUserContext UserContext()
    {
      this.RefreshIfStale();
      BrainUserContext brainUserContext = BrainContext.WithLocalApps(this.apps).<Clone>$();
      brainUserContext.RosterNote = this.RosterNote() ?? "";
      return brainUserContext;
    }

    // Token: 0x060005D7 RID: 1495 RVA: 0x0052E298 File Offset: 0x0052E298
    [NullableContext(1)]
    public static BrainUserContext WithLocalApps(BrainUserContext fromCatalog)
    {
      List<string> list = fromCatalog.ConnectedApps.Where<string>((string id) => id != "obsidian" && !id.StartsWith("custom:", StringComparison.Ordinal)).ToList<string>();
      List<string> list2 = fromCatalog.NeedsReconnect.Where<string>((string n) => !string.Equals(n, "Obsidian", StringComparison.OrdinalIgnoreCase)).ToList<string>();
      if (ObsidianVault.ConfiguredPath != null)
      {
        list.Add("obsidian");
      }
      else if (ObsidianVault.PersistedPath != null)
      {
        list2.Add("Obsidian");
      }
      List<CustomConnector> list3 = (from c in CustomConnectorStore.Load()
        orderby c.CreatedAt
        select c).ToList<CustomConnector>();
      foreach (CustomConnector customConnector in list3)
      {
        if (ConnectorAuth.NeedsReconnect(customConnector.Id))
        {
          list2.Add(customConnector.Name);
        }
        else
        {
          list.Add(customConnector.IntegrationId);
        }
      }
      BrainUserContext brainUserContext = fromCatalog.<Clone>$();
      brainUserContext.ConnectedApps = list;
      brainUserContext.NeedsReconnect = list2.Order<string>(StringComparer.OrdinalIgnoreCase).ToList<string>();
      brainUserContext.CustomConnectors = list3.Select<CustomConnector, CustomConnectorDescription>((CustomConnector c) => new CustomConnectorDescription(c.Name, c.EffectiveCapabilityDescription)).ToList<CustomConnectorDescription>();
      return brainUserContext;
    }

    // Token: 0x060005D8 RID: 1496 RVA: 0x0052E414 File Offset: 0x0052E414
    private void RefreshIfStale()
    {
      if (DateTimeOffset.Now - this.fetchedAt > BrainContext.RefreshAfter)
      {
        this.Refresh();
      }
    }

    // Token: 0x060005D9 RID: 1497 RVA: 0x0052E438 File Offset: 0x0052E438
    public void Refresh()
    {
      if (!this.core.Auth.IsSignedIn || Interlocked.Exchange(ref this.refreshing, 1) == 1)
      {
        return;
      }
      Task.Run(async delegate
      {
        try
        {
          Task<PlanSnapshot> planAsync = this.core.Worker.GetPlanAsync(default(CancellationToken));
          Task<JsonObject> integrationsTask = this.core.Worker.GetJsonObjectAsync("/agent/integrations", default(CancellationToken));
          try
          {
            PlanSnapshot planSnapshot = await planAsync.ConfigureAwait(false);
            this.plan = planSnapshot ?? this.plan;
          }
          catch (Exception ex)
          {
            Log.Warn("BrainContext", "/me/plan: " + ex.Message);
          }
          try
          {
            JsonObject jsonObject = await integrationsTask.ConfigureAwait(false);
            if (jsonObject != null)
            {
              this.apps = BrainUserContext.FromIntegrations(jsonObject);
              string text = "BrainContext";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 2);
              defaultInterpolatedStringHandler.AppendLiteral("connected apps: [");
              defaultInterpolatedStringHandler.AppendFormatted(string.Join(", ", this.apps.ConnectedApps));
              defaultInterpolatedStringHandler.AppendLiteral("], reconnect: [");
              defaultInterpolatedStringHandler.AppendFormatted(string.Join(", ", this.apps.NeedsReconnect));
              defaultInterpolatedStringHandler.AppendLiteral("]");
              Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
            }
          }
          catch (Exception ex2)
          {
            Log.Warn("BrainContext", "/agent/integrations: " + ex2.Message);
          }
          this.fetchedAt = DateTimeOffset.Now;
          integrationsTask = null;
        }
        finally
        {
          Interlocked.Exchange(ref this.refreshing, 0);
        }
      });
    }

    // Token: 0x04000134 RID: 308
    private static readonly TimeSpan RefreshAfter = TimeSpan.FromMinutes(10L);

    // Token: 0x04000135 RID: 309
    [Nullable(1)]
    private readonly CoreServices core;

    // Token: 0x04000136 RID: 310
    [Nullable(1)]
    private readonly IAgentToolHost agents;

    // Token: 0x04000137 RID: 311
    private volatile PlanSnapshot plan;

    // Token: 0x04000138 RID: 312
    [Nullable(1)]
    private volatile BrainUserContext apps = BrainUserContext.Empty;

    // Token: 0x04000139 RID: 313
    private DateTimeOffset fetchedAt = DateTimeOffset.MinValue;

    // Token: 0x0400013A RID: 314
    private int refreshing;

    // Token: 0x0400013B RID: 315
    public volatile ClickyNotes.Report LastReport;
  }
}
