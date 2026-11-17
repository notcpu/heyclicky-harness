using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Backend;
using HeyClicky.App.Services;
using HeyClicky.Core.Ink;
using HeyClicky.Core.Telemetry;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001A6 RID: 422
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkNoteConnect
  {
    // Token: 0x17000406 RID: 1030
    // (get) Token: 0x060016C1 RID: 5825 RVA: 0x005BC2BC File Offset: 0x005BC2BC
    // (set) Token: 0x060016C2 RID: 5826 RVA: 0x005BC2C4 File Offset: 0x005BC2C4
    [Nullable(2)]
    public Action BringAppToFront
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x060016C3 RID: 5827 RVA: 0x005BC2D0 File Offset: 0x005BC2D0
    public ClickyInkNoteConnect(AgentService agents, DispatcherQueue dispatcher, Action<string> openUrl)
    {
      this.agents = agents;
      this.dispatcher = dispatcher;
      this.openUrl = openUrl;
    }

    // Token: 0x060016C4 RID: 5828 RVA: 0x005BC310 File Offset: 0x005BC310
    private static string Lowered(string value)
    {
      return value.Trim().ToLowerInvariant();
    }

    // Token: 0x060016C5 RID: 5829 RVA: 0x005BC320 File Offset: 0x005BC320
    public static HashSet<string> IdentifierCandidates(AgentIntegration integration)
    {
      HashSet<string> hashSet = new HashSet<string>
      {
        ClickyInkNoteConnect.Lowered(integration.Id),
        ClickyInkNoteConnect.Lowered(integration.PrimaryToolkitSlug)
      };
      foreach (AgentIntegrationToolkit agentIntegrationToolkit in integration.Toolkits)
      {
        hashSet.Add(ClickyInkNoteConnect.Lowered(agentIntegrationToolkit.Slug));
      }
      hashSet.Remove("");
      return hashSet;
    }

    // Token: 0x060016C6 RID: 5830 RVA: 0x005BC3B0 File Offset: 0x005BC3B0
    public void Sync([Nullable(new byte[] { 2, 1 })] IReadOnlyList<AgentIntegration> catalog)
    {
      ClickyInkConnections shared;
      ClickyInkConnections clickyInkConnections = (shared = ClickyInkConnections.Shared);
      if (shared.Connect == null)
      {
        shared.Connect = new Action<string>(this.ConnectFromNote);
      }
      IReadOnlyList<AgentIntegration> readOnlyList = catalog ?? Array.Empty<AgentIntegration>();
      clickyInkConnections.SetApps(readOnlyList.Where<AgentIntegration>((AgentIntegration i) => !i.IsLocalIntegration).Select<AgentIntegration, ClickyInkConnections.App>(delegate(AgentIntegration i)
      {
        Uri uri;
        return new ClickyInkConnections.App(i.PrimaryToolkitSlug, i.DisplayName, Uri.TryCreate(i.LogoUrl, UriKind.Absolute, out uri) ? uri : null, i.IsActiveConnection, i.Status == "needs_reconnect");
      }));
      this.TrackPendingConnections(readOnlyList);
    }

    // Token: 0x060016C7 RID: 5831 RVA: 0x005BC440 File Offset: 0x005BC440
    [return: Nullable(2)]
    private AgentIntegration IntegrationFor(string normalizedSlug)
    {
      AgentIntegrationCatalog catalog = ConnectedApps.Catalog;
      Func<string, bool> <>9__1;
      return (((catalog != null) ? catalog.Integrations : null) ?? Array.Empty<AgentIntegration>()).FirstOrDefault<AgentIntegration>(delegate(AgentIntegration integration)
      {
        IEnumerable<string> enumerable = ClickyInkNoteConnect.IdentifierCandidates(integration);
        Func<string, bool> func;
        if ((func = <>9__1) == null)
        {
          func = (<>9__1 = (string candidate) => ClickyInkConnections.Normalized(candidate) == normalizedSlug);
        }
        return enumerable.Any<string>(func);
      });
    }

    // Token: 0x060016C8 RID: 5832 RVA: 0x005BC488 File Offset: 0x005BC488
    private void ConnectFromNote(string normalizedSlug)
    {
      ClickyInkNoteConnect.<>c__DisplayClass17_0 CS$<>8__locals1 = new ClickyInkNoteConnect.<>c__DisplayClass17_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.normalizedSlug = normalizedSlug;
      AgentIntegration agentIntegration = this.IntegrationFor(CS$<>8__locals1.normalizedSlug);
      if (agentIntegration != null)
      {
        this.StartSignIn(agentIntegration, CS$<>8__locals1.normalizedSlug);
        return;
      }
      Task.Run(delegate
      {
        ClickyInkNoteConnect.<>c__DisplayClass17_0.<<ConnectFromNote>b__0>d <<ConnectFromNote>b__0>d;
        <<ConnectFromNote>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<ConnectFromNote>b__0>d.<>4__this = CS$<>8__locals1;
        <<ConnectFromNote>b__0>d.<>1__state = -1;
        <<ConnectFromNote>b__0>d.<>t__builder.Start<ClickyInkNoteConnect.<>c__DisplayClass17_0.<<ConnectFromNote>b__0>d>(ref <<ConnectFromNote>b__0>d);
        return <<ConnectFromNote>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x060016C9 RID: 5833 RVA: 0x005BC4DC File Offset: 0x005BC4DC
    private void StartSignIn(AgentIntegration integration, string normalizedSlug)
    {
      ClickyInkNoteConnect.<>c__DisplayClass18_0 CS$<>8__locals1 = new ClickyInkNoteConnect.<>c__DisplayClass18_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.integration = integration;
      CS$<>8__locals1.normalizedSlug = normalizedSlug;
      string text;
      if (!(CS$<>8__locals1.integration.Status == "needs_reconnect"))
      {
        text = null;
      }
      else
      {
        AgentIntegrationAccount agentIntegrationAccount = CS$<>8__locals1.integration.Accounts.FirstOrDefault<AgentIntegrationAccount>((AgentIntegrationAccount a) => !a.IsActive);
        text = ((agentIntegrationAccount != null) ? agentIntegrationAccount.ConnectedAccountId : null);
      }
      string text2 = text;
      CS$<>8__locals1.id = ClickyInkNoteConnect.Lowered(CS$<>8__locals1.integration.PrimaryToolkitSlug);
      if (text2 != null)
      {
        this.replacedAccounts[CS$<>8__locals1.id] = text2;
      }
      else
      {
        this.replacedAccounts.Remove(CS$<>8__locals1.id);
      }
      this.pending.Add(CS$<>8__locals1.id);
      this.awaitingCallback.Add(CS$<>8__locals1.id);
      ClickyAnalytics.TrackIntegrationInitiated(CS$<>8__locals1.integration.Id, CS$<>8__locals1.integration.DisplayName, CS$<>8__locals1.integration.Toolkits.Select<AgentIntegrationToolkit, string>((AgentIntegrationToolkit t) => t.Slug).ToList<string>(), "ink_note", CS$<>8__locals1.integration.Status, new int?(CS$<>8__locals1.integration.ConnectedCount), new int?(CS$<>8__locals1.integration.TotalCount));
      Task.Run(delegate
      {
        ClickyInkNoteConnect.<>c__DisplayClass18_0.<<StartSignIn>b__2>d <<StartSignIn>b__2>d;
        <<StartSignIn>b__2>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<StartSignIn>b__2>d.<>4__this = CS$<>8__locals1;
        <<StartSignIn>b__2>d.<>1__state = -1;
        <<StartSignIn>b__2>d.<>t__builder.Start<ClickyInkNoteConnect.<>c__DisplayClass18_0.<<StartSignIn>b__2>d>(ref <<StartSignIn>b__2>d);
        return <<StartSignIn>b__2>d.<>t__builder.Task;
      });
    }

    // Token: 0x060016CA RID: 5834 RVA: 0x005BC650 File Offset: 0x005BC650
    [NullableContext(2)]
    public bool OnAuthCallback(string integrationID, bool didFail)
    {
      ClickyInkNoteConnect.<>c__DisplayClass19_0 CS$<>8__locals1 = new ClickyInkNoteConnect.<>c__DisplayClass19_0();
      CS$<>8__locals1.<>4__this = this;
      if (integrationID == null)
      {
        return false;
      }
      CS$<>8__locals1.id = ClickyInkNoteConnect.Lowered(integrationID);
      if (!this.pending.Contains(CS$<>8__locals1.id))
      {
        return false;
      }
      this.awaitingCallback.Remove(CS$<>8__locals1.id);
      string text = ClickyInkConnections.Normalized(CS$<>8__locals1.id);
      if (ClickyInkConnections.Shared.IsConnectingFromNote(text))
      {
        ClickyInkConnections.Shared.SetPhase(didFail ? new ClickyInkConnections.Phase.Failed("The sign-in wasn't finished. Try again.") : new ClickyInkConnections.Phase.Checking(), text);
        Action bringAppToFront = this.BringAppToFront;
        if (bringAppToFront != null)
        {
          bringAppToFront();
        }
      }
      if (didFail)
      {
        this.pending.Remove(CS$<>8__locals1.id);
        this.replacedAccounts.Remove(CS$<>8__locals1.id);
        return true;
      }
      CancellationTokenSource cancellationTokenSource = this.refresh;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      CS$<>8__locals1.cancel = (this.refresh = new CancellationTokenSource());
      Task.Run(delegate
      {
        ClickyInkNoteConnect.<>c__DisplayClass19_0.<<OnAuthCallback>b__0>d <<OnAuthCallback>b__0>d;
        <<OnAuthCallback>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<OnAuthCallback>b__0>d.<>4__this = CS$<>8__locals1;
        <<OnAuthCallback>b__0>d.<>1__state = -1;
        <<OnAuthCallback>b__0>d.<>t__builder.Start<ClickyInkNoteConnect.<>c__DisplayClass19_0.<<OnAuthCallback>b__0>d>(ref <<OnAuthCallback>b__0>d);
        return <<OnAuthCallback>b__0>d.<>t__builder.Task;
      });
      return true;
    }

    // Token: 0x060016CB RID: 5835 RVA: 0x005BC74C File Offset: 0x005BC74C
    private Task<T> OnUi<[Nullable(2)] T>(Func<T> work)
    {
      TaskCompletionSource<T> done = new TaskCompletionSource<T>();
      if (!this.dispatcher.TryEnqueue(delegate
      {
        try
        {
          done.SetResult(work());
        }
        catch (Exception ex)
        {
          done.SetException(ex);
        }
      }))
      {
        done.SetCanceled();
      }
      return done.Task;
    }

    // Token: 0x060016CC RID: 5836 RVA: 0x005BC7A0 File Offset: 0x005BC7A0
    private static void StillSyncing(string integrationID)
    {
      string text = ClickyInkConnections.Normalized(integrationID);
      if (!(ClickyInkConnections.Shared.PhaseFor(text) is ClickyInkConnections.Phase.Checking))
      {
        return;
      }
      ClickyInkConnections.Shared.SetPhase(new ClickyInkConnections.Phase.Failed("It's still syncing. Try again in a moment."), text);
    }

    // Token: 0x060016CD RID: 5837 RVA: 0x005BC7DC File Offset: 0x005BC7DC
    private void TrackPendingConnections(IReadOnlyList<AgentIntegration> integrations)
    {
      if (this.pending.Count == 0)
      {
        return;
      }
      using (IEnumerator<AgentIntegration> enumerator = integrations.Where<AgentIntegration>((AgentIntegration i) => i.IsActiveConnection).GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ClickyInkNoteConnect.<>c__DisplayClass22_0 CS$<>8__locals1 = new ClickyInkNoteConnect.<>c__DisplayClass22_0();
          CS$<>8__locals1.<>4__this = this;
          CS$<>8__locals1.integration = enumerator.Current;
          HashSet<string> hashSet = ClickyInkNoteConnect.IdentifierCandidates(CS$<>8__locals1.integration);
          if (!hashSet.Overlaps(this.awaitingCallback) && hashSet.Overlaps(this.pending))
          {
            ClickyAnalytics.TrackIntegrationConnected(CS$<>8__locals1.integration.Id, CS$<>8__locals1.integration.DisplayName, CS$<>8__locals1.integration.Toolkits.Select<AgentIntegrationToolkit, string>((AgentIntegrationToolkit t) => t.Slug).ToList<string>(), "ink_note", CS$<>8__locals1.integration.Status, new int?(CS$<>8__locals1.integration.ConnectedCount), new int?(CS$<>8__locals1.integration.TotalCount));
            CS$<>8__locals1.replacedID = hashSet.Select<string, string>((string c) => this.replacedAccounts.GetValueOrDefault(c)).FirstOrDefault<string>((string r) => r != null);
            if (CS$<>8__locals1.replacedID != null)
            {
              AgentIntegrationAccount agentIntegrationAccount = CS$<>8__locals1.integration.Accounts.FirstOrDefault<AgentIntegrationAccount>((AgentIntegrationAccount a) => a.ConnectedAccountId == CS$<>8__locals1.replacedID);
              if (agentIntegrationAccount != null && !agentIntegrationAccount.IsActive)
              {
                Task.Run(delegate
                {
                  ClickyInkNoteConnect.<>c__DisplayClass22_0.<<TrackPendingConnections>b__4>d <<TrackPendingConnections>b__4>d;
                  <<TrackPendingConnections>b__4>d.<>t__builder = AsyncTaskMethodBuilder.Create();
                  <<TrackPendingConnections>b__4>d.<>4__this = CS$<>8__locals1;
                  <<TrackPendingConnections>b__4>d.<>1__state = -1;
                  <<TrackPendingConnections>b__4>d.<>t__builder.Start<ClickyInkNoteConnect.<>c__DisplayClass22_0.<<TrackPendingConnections>b__4>d>(ref <<TrackPendingConnections>b__4>d);
                  return <<TrackPendingConnections>b__4>d.<>t__builder.Task;
                });
              }
            }
            foreach (string text in hashSet)
            {
              string text2 = ClickyInkConnections.Normalized(text);
              if (ClickyInkConnections.Shared.IsConnectingFromNote(text2))
              {
                ClickyInkConnections.Shared.SetPhase(new ClickyInkConnections.Phase.Connected(), text2);
              }
              this.pending.Remove(text);
              this.replacedAccounts.Remove(text);
            }
          }
        }
      }
    }

    // Token: 0x04000B29 RID: 2857
    public const string IntegrationSource = "ink_note";

    // Token: 0x04000B2A RID: 2858
    private readonly AgentService agents;

    // Token: 0x04000B2B RID: 2859
    private readonly DispatcherQueue dispatcher;

    // Token: 0x04000B2C RID: 2860
    private readonly Action<string> openUrl;

    // Token: 0x04000B2E RID: 2862
    private readonly HashSet<string> pending = new HashSet<string>();

    // Token: 0x04000B2F RID: 2863
    private readonly HashSet<string> awaitingCallback = new HashSet<string>();

    // Token: 0x04000B30 RID: 2864
    private readonly Dictionary<string, string> replacedAccounts = new Dictionary<string, string>();

    // Token: 0x04000B31 RID: 2865
    [Nullable(2)]
    private CancellationTokenSource refresh;
  }
}
