using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Runtime;
using HeyClicky.Core.Connectors;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000C8 RID: 200
  [NullableContext(1)]
  [Nullable(0)]
  public static class ConnectorAuth
  {
    // Token: 0x1400001E RID: 30
    // (add) Token: 0x06000862 RID: 2146 RVA: 0x0053EB00 File Offset: 0x0053EB00
    // (remove) Token: 0x06000863 RID: 2147 RVA: 0x0053EB34 File Offset: 0x0053EB34
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public static event Action Changed;

    // Token: 0x06000864 RID: 2148 RVA: 0x0053EB68 File Offset: 0x0053EB68
    public static void Initialize(AgentService service, Action<string> open, Action returned)
    {
      ConnectorAuth.agents = service;
      ConnectorAuth.openBrowser = open;
      ConnectorAuth.browserReturned = returned;
      ConnectorAuth.ImportCodexSignInsOnce(service.CodexHome);
      Func<bool> func;
      if ((func = ConnectorAuth.<>O.<0>__AnyTokenExpiring) == null)
      {
        func = (ConnectorAuth.<>O.<0>__AnyTokenExpiring = new Func<bool>(ConnectorAuth.AnyTokenExpiring));
      }
      service.CustomConnectorTokensExpiring = func;
      Func<CancellationToken, Task<IReadOnlyList<CustomConnectorRegistration>>> func2;
      if ((func2 = ConnectorAuth.<>O.<1>__RefreshExpiredTokensAsync) == null)
      {
        func2 = (ConnectorAuth.<>O.<1>__RefreshExpiredTokensAsync = new Func<CancellationToken, Task<IReadOnlyList<CustomConnectorRegistration>>>(ConnectorAuth.RefreshExpiredTokensAsync));
      }
      service.RefreshCustomConnectorTokensAsync = func2;
      CustomConnectorStore.Changed += delegate
      {
        Action changed = ConnectorAuth.Changed;
        if (changed == null)
        {
          return;
        }
        changed();
      };
      CustomConnectorStore.Updated += delegate
      {
        Action changed2 = ConnectorAuth.Changed;
        if (changed2 == null)
        {
          return;
        }
        changed2();
      };
    }

    // Token: 0x06000865 RID: 2149 RVA: 0x0053EC1C File Offset: 0x0053EC1C
    public static ConnectorSignInState StateFor(string id)
    {
      return ConnectorAuth.states.GetValueOrDefault(id, ConnectorSignInState.Unknown);
    }

    // Token: 0x06000866 RID: 2150 RVA: 0x0053EC30 File Offset: 0x0053EC30
    private static void Set(string id, ConnectorSignInState state)
    {
      ConnectorSignInState valueOrDefault = ConnectorAuth.states.GetValueOrDefault(id);
      ConnectorAuth.states[id] = state;
      if (valueOrDefault != state)
      {
        string text = "connectors";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 3);
        defaultInterpolatedStringHandler.AppendLiteral("sign-in ");
        defaultInterpolatedStringHandler.AppendFormatted(id);
        defaultInterpolatedStringHandler.AppendLiteral(": ");
        defaultInterpolatedStringHandler.AppendFormatted<ConnectorSignInKind>(state.Kind);
        defaultInterpolatedStringHandler.AppendFormatted((state.Text == null) ? "" : (" — " + state.Text));
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        Action changed = ConnectorAuth.Changed;
        if (changed == null)
        {
          return;
        }
        changed();
      }
    }

    // Token: 0x06000867 RID: 2151 RVA: 0x0053ECDC File Offset: 0x0053ECDC
    public static bool NeedsReconnect(string id)
    {
      switch (ConnectorAuth.StateFor(id).Kind)
      {
      case ConnectorSignInKind.SignInUnavailable:
      case ConnectorSignInKind.NeedsSignIn:
      case ConnectorSignInKind.AwaitingBrowser:
      case ConnectorSignInKind.Failed:
      case ConnectorSignInKind.TokenRejected:
        return true;
      }
      return false;
    }

    // Token: 0x06000868 RID: 2152 RVA: 0x0053ED24 File Offset: 0x0053ED24
    private static bool Current(string id, Guid revision)
    {
      return ConnectorAuth.revisions.GetValueOrDefault(id) == revision && CustomConnectorStore.Get(id) != null;
    }

    // Token: 0x06000869 RID: 2153 RVA: 0x0053ED48 File Offset: 0x0053ED48
    [return: Nullable(2)]
    private static Uri ServerUrl(CustomConnector c)
    {
      string url = c.Url;
      Uri uri;
      if (!Uri.TryCreate((url != null) ? url.Trim() : null, UriKind.Absolute, out uri))
      {
        return null;
      }
      return uri;
    }

    // Token: 0x0600086A RID: 2154 RVA: 0x0053ED74 File Offset: 0x0053ED74
    public static void AfterSave(string id)
    {
      CustomConnector customConnector = CustomConnectorStore.Get(id);
      if (customConnector == null)
      {
        return;
      }
      ConnectorAuth.revisions[id] = Guid.NewGuid();
      if (customConnector.HasApiKey)
      {
        if (customConnector.UsesBrowserSignIn)
        {
          ConnectorSignInState connectorSignInState;
          ConnectorAuth.states.TryRemove(id, out connectorSignInState);
        }
        else
        {
          ConnectorAuth.VerifyToken(id, true);
        }
      }
      else
      {
        ConnectorSignInState connectorSignInState;
        ConnectorAuth.states.TryRemove(id, out connectorSignInState);
      }
      if (customConnector.IsRemote && !customConnector.HasApiKey)
      {
        ConnectorAuth.ProbeAfterSaveAsync(id);
      }
      ConnectorAuth.DeriveCapabilities(id);
      Action changed = ConnectorAuth.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x0600086B RID: 2155 RVA: 0x0053EE00 File Offset: 0x0053EE00
    public static void Removed(string id)
    {
      ConnectorAuth.revisions[id] = Guid.NewGuid();
      ConnectorSignInState connectorSignInState;
      ConnectorAuth.states.TryRemove(id, out connectorSignInState);
      Action changed = ConnectorAuth.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x0600086C RID: 2156 RVA: 0x0053EE3C File Offset: 0x0053EE3C
    public static void SignIn(string id, bool probeOnly = false)
    {
      CustomConnector customConnector = CustomConnectorStore.Get(id);
      if (customConnector == null || !customConnector.IsRemote || ConnectorAuth.agents == null)
      {
        return;
      }
      if (ConnectorAuth.StateFor(id).Kind == ConnectorSignInKind.AwaitingBrowser)
      {
        return;
      }
      if (probeOnly)
      {
        ConnectorAuth.ProbeSignInAsync(customConnector);
        return;
      }
      string text = "custom_connector_action";
      string text2 = null;
      string text3 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "sign_in";
      jsonObject["connector_id"] = id;
      ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
      ConnectorAuth.AppSideSignInAsync(customConnector);
    }

    // Token: 0x0600086D RID: 2157 RVA: 0x0053EEC4 File Offset: 0x0053EEC4
    private static async Task ProbeSignInAsync(CustomConnector c)
    {
      string id = c.Id;
      Uri url = ConnectorAuth.ServerUrl(c);
      if (url != null && ConnectorAuth.probing.TryAdd(id, 0))
      {
        Guid revision = ConnectorAuth.revisions.GetOrAdd(id, (string _) => Guid.NewGuid());
        try
        {
          ConnectorAuth.Set(id, new ConnectorSignInState(ConnectorSignInKind.Checking, null));
          McpOAuthSignInSupport mcpOAuthSignInSupport = await McpOAuth.ProbeSignInSupportAsync(ConnectorAuth.Http, url, c.TrimmedOAuthClientId.Length > 0, default(CancellationToken)).ConfigureAwait(false);
          if (ConnectorAuth.Current(id, revision))
          {
            switch (mcpOAuthSignInSupport)
            {
            case McpOAuthSignInSupport.NotNeeded:
              ConnectorAuth.Set(id, new ConnectorSignInState(ConnectorSignInKind.NotNeeded, null));
              break;
            case McpOAuthSignInSupport.Supported:
              CustomConnectorStore.UpdateQuietly(id, delegate(CustomConnector x)
              {
                CustomConnector customConnector = x.<Clone>$();
                customConnector.BrowserSignInSupport = "supported";
                return customConnector;
              });
              ConnectorAuth.Set(id, new ConnectorSignInState(ConnectorSignInKind.NeedsSignIn, null));
              break;
            case McpOAuthSignInSupport.Unsupported:
              CustomConnectorStore.UpdateQuietly(id, delegate(CustomConnector x)
              {
                CustomConnector customConnector2 = x.<Clone>$();
                customConnector2.BrowserSignInSupport = "unsupported";
                return customConnector2;
              });
              ConnectorAuth.Set(id, new ConnectorSignInState(ConnectorSignInKind.SignInUnavailable, ConnectorAuth.UnavailableText(url)));
              break;
            default:
              ConnectorAuth.Set(id, ConnectorSignInState.Unknown);
              break;
            }
          }
        }
        finally
        {
          byte b;
          ConnectorAuth.probing.TryRemove(id, out b);
        }
      }
    }

    // Token: 0x0600086E RID: 2158 RVA: 0x0053EF08 File Offset: 0x0053EF08
    private static async Task AppSideSignInAsync(CustomConnector c)
    {
      string id = c.Id;
      Uri url = ConnectorAuth.ServerUrl(c);
      if (url != null)
      {
        Guid revision = ConnectorAuth.revisions.GetOrAdd(id, (string _) => Guid.NewGuid());
        string redirectUri = McpOAuth.RedirectUri(48611);
        CustomConnectorSecrets customConnectorSecrets = CustomConnectorStore.ReadSecrets(id);
        McpOAuthClientRegistration mcpOAuthClientRegistration;
        if (customConnectorSecrets != null)
        {
          string registeredClientId = customConnectorSecrets.RegisteredClientId;
          if (registeredClientId != null && registeredClientId.Length > 0 && customConnectorSecrets.RegisteredClientRedirectUri == redirectUri)
          {
            mcpOAuthClientRegistration = new McpOAuthClientRegistration(registeredClientId, customConnectorSecrets.RegisteredClientSecret);
            goto IL_0100;
          }
        }
        mcpOAuthClientRegistration = null;
        IL_0100:
        McpOAuthClientRegistration mcpOAuthClientRegistration2 = mcpOAuthClientRegistration;
        ConnectorAuth.Set(id, new ConnectorSignInState(ConnectorSignInKind.AwaitingBrowser, null));
        try
        {
          string text = ((c.TrimmedOAuthClientId.Length > 0) ? c.TrimmedOAuthClientId : null);
          HttpClient http = ConnectorAuth.Http;
          Uri uri = url;
          string text2 = text;
          Action<Uri> action = delegate(Uri auth)
          {
            if (ConnectorAuth.Current(id, revision))
            {
              ConnectorAuth.OpenAuthorization(auth.AbsoluteUri, c.Name);
            }
          };
          McpOAuthClientRegistration mcpOAuthClientRegistration3 = ((text == null) ? mcpOAuthClientRegistration2 : null);
          McpOAuthSignInResult mcpOAuthSignInResult = await McpOAuth.SignInAsync(http, uri, text2, action, default(CancellationToken), 48611, null, mcpOAuthClientRegistration3).ConfigureAwait(false);
          McpOAuthSignInResult result = mcpOAuthSignInResult;
          if (ConnectorAuth.Current(id, revision))
          {
            McpOAuthClientRegistration registered = (result.RegisteredDynamically ? result.Client : null);
            string text3 = await McpOAuth.VerifyBearerTokenAsync(ConnectorAuth.Http, url, result.Tokens.AccessToken, default(CancellationToken)).ConfigureAwait(false);
            if (ConnectorAuth.Current(id, revision))
            {
              ConnectorAuth.revisions[id] = revision;
              CustomConnectorStore.StoreSignInTokens(id, result.Tokens, result.TokenEndpoint, registered, (registered == null) ? null : redirectUri);
              ConnectorAuth.Set(id, (text3 == null) ? new ConnectorSignInState(ConnectorSignInKind.SignedIn, null) : new ConnectorSignInState(ConnectorSignInKind.Failed, ConnectorAuth.ServerRefusalText(text3, url)));
              if (text3 == null)
              {
                ConnectorAuth.DeriveCapabilities(id);
              }
              result = null;
              registered = null;
            }
          }
        }
        catch (McpOAuthException ex) when (ex.Failure == McpOAuthFailure.ServerDoesNotRequireOAuth)
        {
          if (ConnectorAuth.Current(id, revision))
          {
            ConnectorAuth.Set(id, new ConnectorSignInState(ConnectorSignInKind.NotNeeded, null));
          }
        }
        catch (Exception ex2)
        {
          if (ConnectorAuth.Current(id, revision))
          {
            Log.Warn("connectors", c.Name + ": app-side sign-in failed: " + ex2.Message);
            string id2 = id;
            McpOAuthException ex3 = ex2 as McpOAuthException;
            ConnectorAuth.Set(id2, (ex3 != null && ex3.Failure == McpOAuthFailure.RegistrationFailed) ? new ConnectorSignInState(ConnectorSignInKind.SignInUnavailable, ConnectorAuth.UnavailableText(url)) : new ConnectorSignInState(ConnectorSignInKind.Failed, ex2.Message));
          }
        }
        finally
        {
          ConnectorAuth.FinishBrowserReturn();
        }
      }
    }

    // Token: 0x0600086F RID: 2159 RVA: 0x0053EF4C File Offset: 0x0053EF4C
    private static void OpenAuthorization(string url, string name)
    {
      Log.Info("connectors", name + ": authorization page → browser");
      ConnectorAuth.awaitingReturn = true;
      Action<string> action = ConnectorAuth.openBrowser;
      if (action == null)
      {
        return;
      }
      action(url);
    }

    // Token: 0x06000870 RID: 2160 RVA: 0x0053EF7C File Offset: 0x0053EF7C
    private static void FinishBrowserReturn()
    {
      if (!ConnectorAuth.awaitingReturn)
      {
        return;
      }
      ConnectorAuth.awaitingReturn = false;
      Action action = ConnectorAuth.browserReturned;
      if (action == null)
      {
        return;
      }
      action();
    }

    // Token: 0x06000871 RID: 2161 RVA: 0x0053EF9C File Offset: 0x0053EF9C
    private static async Task ProbeAfterSaveAsync(string id)
    {
      CustomConnector customConnector = CustomConnectorStore.Get(id);
      if (customConnector != null)
      {
        if (customConnector.TrimmedOAuthClientId.Length > 0)
        {
          ConnectorAuth.SignIn(id, false);
        }
        else
        {
          await ConnectorAuth.ProbeSignInAsync(customConnector).ConfigureAwait(false);
          if (ConnectorAuth.StateFor(id).Kind == ConnectorSignInKind.NeedsSignIn)
          {
            ConnectorAuth.SignIn(id, false);
          }
        }
      }
    }

    // Token: 0x06000872 RID: 2162 RVA: 0x0053EFE0 File Offset: 0x0053EFE0
    public static Task RefreshStatesAsync()
    {
      foreach (CustomConnector customConnector in from c in CustomConnectorStore.Load()
        where c.UsesBrowserSignIn
        select c)
      {
        bool flag;
        switch (ConnectorAuth.StateFor(customConnector.Id).Kind)
        {
        case ConnectorSignInKind.Checking:
        case ConnectorSignInKind.SignInUnavailable:
        case ConnectorSignInKind.NotNeeded:
        case ConnectorSignInKind.AwaitingBrowser:
        case ConnectorSignInKind.Failed:
          flag = true;
          break;
        case ConnectorSignInKind.NeedsSignIn:
        case ConnectorSignInKind.SignedIn:
          goto IL_0074;
        default:
          goto IL_0074;
        }
        IL_0077:
        if (flag)
        {
          continue;
        }
        ConnectorSignInState connectorSignInState = ConnectorAuth.StandingFromStore(customConnector);
        if (connectorSignInState != null)
        {
          ConnectorAuth.Set(customConnector.Id, connectorSignInState);
          continue;
        }
        ConnectorAuth.SignIn(customConnector.Id, true);
        continue;
        IL_0074:
        flag = false;
        goto IL_0077;
      }
      ConnectorAuth.DeriveMissingCapabilities();
      ConnectorAuth.VerifyTokensIfNeeded();
      return Task.CompletedTask;
    }

    // Token: 0x06000873 RID: 2163 RVA: 0x0053F0C0 File Offset: 0x0053F0C0
    public static async Task OnLaunchAsync()
    {
      await ConnectorAuth.RefreshExpiredTokensAsync(CancellationToken.None).ConfigureAwait(false);
      ConnectorAuth.DeriveMissingCapabilities();
      ConnectorAuth.VerifyTokensIfNeeded();
      foreach (CustomConnector customConnector in from c in CustomConnectorStore.Load()
        where c.UsesBrowserSignIn
        select c)
      {
        if (ConnectorAuth.StateFor(customConnector.Id).Kind == ConnectorSignInKind.Unknown)
        {
          ConnectorSignInState connectorSignInState = ConnectorAuth.StandingFromStore(customConnector);
          if (connectorSignInState != null)
          {
            ConnectorAuth.Set(customConnector.Id, connectorSignInState);
          }
          else
          {
            ConnectorAuth.SignIn(customConnector.Id, true);
          }
        }
      }
    }

    // Token: 0x06000874 RID: 2164 RVA: 0x0053F0FC File Offset: 0x0053F0FC
    [return: Nullable(2)]
    private static ConnectorSignInState StandingFromStore(CustomConnector c)
    {
      if (c.HasApiKey)
      {
        return new ConnectorSignInState(ConnectorSignInKind.SignedIn, null);
      }
      if (c.BrowserSignInSupport == "unsupported" && c.TrimmedOAuthClientId.Length == 0)
      {
        return new ConnectorSignInState(ConnectorSignInKind.SignInUnavailable, ConnectorAuth.UnavailableText(ConnectorAuth.ServerUrl(c)));
      }
      if (c.BrowserSignInSupport == "supported" || c.TrimmedOAuthClientId.Length > 0)
      {
        return new ConnectorSignInState(ConnectorSignInKind.NeedsSignIn, null);
      }
      return null;
    }

    // Token: 0x06000875 RID: 2165 RVA: 0x0053F174 File Offset: 0x0053F174
    private static void ImportCodexSignInsOnce(string codexHome)
    {
      string text = AppPaths.Combine(new string[] { "connectors.codex-sign-ins-imported" });
      if (File.Exists(text))
      {
        return;
      }
      try
      {
        JsonObject jsonObject = CodexStoredSignIns.ReadFile(codexHome);
        if (jsonObject != null)
        {
          int num = 0;
          foreach (CustomConnector customConnector in from c in CustomConnectorStore.Load()
            where c.IsRemote && !c.HasApiKey && c.TrimmedOAuthClientId.Length == 0
            select c)
          {
            Uri uri = ConnectorAuth.ServerUrl(customConnector);
            if (uri != null)
            {
              CodexStoredSignIn codexStoredSignIn = CodexStoredSignIns.Find(jsonObject, uri, customConnector.ServerKey, DateTimeOffset.UtcNow);
              if (codexStoredSignIn != null)
              {
                string clientId = codexStoredSignIn.ClientId;
                McpOAuthClientRegistration mcpOAuthClientRegistration = ((clientId != null) ? new McpOAuthClientRegistration(clientId, codexStoredSignIn.ClientSecret) : null);
                CustomConnectorStore.StoreSignInTokens(customConnector.Id, new McpOAuthTokens(codexStoredSignIn.AccessToken, codexStoredSignIn.RefreshToken, codexStoredSignIn.ExpiresAt), null, mcpOAuthClientRegistration, null);
                num++;
              }
            }
          }
          string text2 = "connectors";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 1);
          defaultInterpolatedStringHandler.AppendLiteral("imported ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(num);
          defaultInterpolatedStringHandler.AppendLiteral(" sign-in(s) Codex had saved");
          Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
        }
        Directory.CreateDirectory(Path.GetDirectoryName(text));
        File.WriteAllText(text, DateTimeOffset.UtcNow.ToString("O"));
      }
      catch (Exception ex)
      {
        Log.Warn("connectors", "importing Codex's sign-ins: " + ex.Message);
      }
    }

    // Token: 0x06000876 RID: 2166 RVA: 0x0053F328 File Offset: 0x0053F328
    public static void VerifyToken(string id, bool userInitiated)
    {
      ConnectorAuth.<>c__DisplayClass29_0 CS$<>8__locals1 = new ConnectorAuth.<>c__DisplayClass29_0();
      CS$<>8__locals1.id = id;
      CS$<>8__locals1.userInitiated = userInitiated;
      CS$<>8__locals1.c = CustomConnectorStore.Get(CS$<>8__locals1.id);
      if (CS$<>8__locals1.c != null && CS$<>8__locals1.c.UsesPlainBearerToken)
      {
        CS$<>8__locals1.url = ConnectorAuth.ServerUrl(CS$<>8__locals1.c);
        if (CS$<>8__locals1.url != null)
        {
          CS$<>8__locals1.secrets = CustomConnectorStore.ReadSecrets(CS$<>8__locals1.id);
          if (string.IsNullOrEmpty(CS$<>8__locals1.secrets.BearerToken))
          {
            return;
          }
          CS$<>8__locals1.revision = ConnectorAuth.revisions.GetOrAdd(CS$<>8__locals1.id, (string _) => Guid.NewGuid());
          CS$<>8__locals1.before = ConnectorAuth.StateFor(CS$<>8__locals1.id);
          ConnectorAuth.Set(CS$<>8__locals1.id, new ConnectorSignInState(ConnectorSignInKind.Checking, null));
          Task.Run(delegate
          {
            ConnectorAuth.<>c__DisplayClass29_0.<<VerifyToken>b__1>d <<VerifyToken>b__1>d;
            <<VerifyToken>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
            <<VerifyToken>b__1>d.<>4__this = CS$<>8__locals1;
            <<VerifyToken>b__1>d.<>1__state = -1;
            <<VerifyToken>b__1>d.<>t__builder.Start<ConnectorAuth.<>c__DisplayClass29_0.<<VerifyToken>b__1>d>(ref <<VerifyToken>b__1>d);
            return <<VerifyToken>b__1>d.<>t__builder.Task;
          });
          return;
        }
      }
    }

    // Token: 0x06000877 RID: 2167 RVA: 0x0053F418 File Offset: 0x0053F418
    public static void VerifyTokensIfNeeded()
    {
      foreach (CustomConnector customConnector in from c in CustomConnectorStore.Load()
        where c.UsesPlainBearerToken
        select c)
      {
        ConnectorSignInKind kind = ConnectorAuth.StateFor(customConnector.Id).Kind;
        if (kind != ConnectorSignInKind.Checking && kind != ConnectorSignInKind.NotNeeded)
        {
          ConnectorAuth.VerifyToken(customConnector.Id, false);
        }
      }
    }

    // Token: 0x06000878 RID: 2168 RVA: 0x0053F4B0 File Offset: 0x0053F4B0
    private static bool AnyTokenExpiring()
    {
      return CustomConnectorStore.Load().Any<CustomConnector>(delegate(CustomConnector c)
      {
        if (c.UsesBrowserSignIn && c.HasApiKey)
        {
          CustomConnectorSecrets customConnectorSecrets = CustomConnectorStore.ReadSecrets(c.Id);
          if (customConnectorSecrets != null)
          {
            DateTimeOffset? expiresAt = customConnectorSecrets.ExpiresAt;
            if (expiresAt != null)
            {
              DateTimeOffset valueOrDefault = expiresAt.GetValueOrDefault();
              string refreshToken = customConnectorSecrets.RefreshToken;
              if (refreshToken != null && refreshToken.Length > 0)
              {
                return valueOrDefault - DateTimeOffset.UtcNow < ConnectorAuth.RefreshMargin;
              }
            }
          }
        }
        return false;
      });
    }

    // Token: 0x06000879 RID: 2169 RVA: 0x0053F4DC File Offset: 0x0053F4DC
    [return: Nullable(new byte[] { 1, 2, 1 })]
    private static async Task<IReadOnlyList<CustomConnectorRegistration>> RefreshExpiredTokensAsync(CancellationToken ct)
    {
      await ConnectorAuth.RefreshGate.WaitAsync(ct).ConfigureAwait(false);
      IReadOnlyList<CustomConnectorRegistration> readOnlyList;
      try
      {
        readOnlyList = await ConnectorAuth.RefreshExpiredTokensOnceAsync(ct).ConfigureAwait(false);
      }
      finally
      {
        ConnectorAuth.RefreshGate.Release();
      }
      return readOnlyList;
    }

    // Token: 0x0600087A RID: 2170 RVA: 0x0053F520 File Offset: 0x0053F520
    [return: Nullable(new byte[] { 1, 2, 1 })]
    private static async Task<IReadOnlyList<CustomConnectorRegistration>> RefreshExpiredTokensOnceAsync(CancellationToken ct)
    {
      bool changed = false;
      foreach (CustomConnector c2 in from c in CustomConnectorStore.Load()
        where c.UsesBrowserSignIn && c.HasApiKey
        select c)
      {
        CustomConnectorSecrets s = CustomConnectorStore.ReadSecrets(c2.Id);
        DateTimeOffset? expiresAt = s.ExpiresAt;
        if (expiresAt != null)
        {
          DateTimeOffset valueOrDefault = expiresAt.GetValueOrDefault();
          if (!(valueOrDefault - DateTimeOffset.UtcNow >= ConnectorAuth.RefreshMargin) && !string.IsNullOrEmpty(s.RefreshToken))
          {
            string clientId = ((c2.TrimmedOAuthClientId.Length > 0) ? c2.TrimmedOAuthClientId : s.RegisteredClientId);
            if (!string.IsNullOrEmpty(clientId))
            {
              Uri uri = ConnectorAuth.ServerUrl(c2);
              if (uri != null)
              {
                Guid revision = ConnectorAuth.revisions.GetOrAdd(c2.Id, (string _) => Guid.NewGuid());
                try
                {
                  Uri uri2;
                  Uri uri3;
                  if (Uri.TryCreate(s.TokenEndpoint, UriKind.Absolute, out uri2))
                  {
                    uri3 = uri2;
                  }
                  else
                  {
                    uri3 = (await McpOAuth.DiscoverAsync(ConnectorAuth.Http, uri, ct).ConfigureAwait(false)).TokenEndpoint;
                  }
                  Uri endpoint = uri3;
                  McpOAuthClientRegistration client = new McpOAuthClientRegistration(clientId, (c2.TrimmedOAuthClientId.Length > 0) ? null : s.RegisteredClientSecret);
                  McpOAuthTokens mcpOAuthTokens = await McpOAuth.RefreshAsync(ConnectorAuth.Http, endpoint, client, s.RefreshToken, ct).ConfigureAwait(false);
                  if (!ConnectorAuth.Current(c2.Id, revision))
                  {
                    continue;
                  }
                  ConnectorAuth.revisions[c2.Id] = revision;
                  string id = c2.Id;
                  McpOAuthTokens mcpOAuthTokens2 = mcpOAuthTokens.<Clone>$();
                  mcpOAuthTokens2.RefreshToken = mcpOAuthTokens.RefreshToken ?? s.RefreshToken;
                  CustomConnectorStore.StoreSignInTokens(id, mcpOAuthTokens2, endpoint, (c2.TrimmedOAuthClientId.Length > 0) ? null : client, (c2.TrimmedOAuthClientId.Length > 0) ? null : s.RegisteredClientRedirectUri);
                  Log.Info("connectors", c2.Name + ": access token refreshed");
                  changed = true;
                  endpoint = null;
                  client = null;
                }
                catch (Exception ex) when (!(ex is OperationCanceledException) || !ct.IsCancellationRequested)
                {
                  if (!ConnectorAuth.Current(c2.Id, revision))
                  {
                    continue;
                  }
                  Log.Warn("connectors", c2.Name + ": token refresh failed: " + ex.Message);
                  McpOAuthException ex2 = ex as McpOAuthException;
                  if (ex2 != null && McpOAuth.IsRefusedRefresh(ex2))
                  {
                    ConnectorAuth.Set(c2.Id, new ConnectorSignInState(ConnectorSignInKind.Failed, "Your sign-in expired. Sign in again."));
                  }
                }
                s = null;
                clientId = null;
                c2 = null;
              }
            }
          }
        }
      }
      IEnumerator<CustomConnector> enumerator = null;
      return changed ? CustomConnectorStore.Registrations() : null;
    }

    // Token: 0x0600087B RID: 2171 RVA: 0x0053F564 File Offset: 0x0053F564
    public static void DeriveCapabilities(string id)
    {
      ConnectorAuth.<>c__DisplayClass36_0 CS$<>8__locals1 = new ConnectorAuth.<>c__DisplayClass36_0();
      CS$<>8__locals1.id = id;
      CustomConnector customConnector = CustomConnectorStore.Get(CS$<>8__locals1.id);
      if (customConnector != null && customConnector.IsRemote)
      {
        CS$<>8__locals1.url = ConnectorAuth.ServerUrl(customConnector);
        if (CS$<>8__locals1.url != null)
        {
          CS$<>8__locals1.secrets = CustomConnectorStore.ReadSecrets(CS$<>8__locals1.id);
          Task.Run(delegate
          {
            ConnectorAuth.<>c__DisplayClass36_0.<<DeriveCapabilities>b__0>d <<DeriveCapabilities>b__0>d;
            <<DeriveCapabilities>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
            <<DeriveCapabilities>b__0>d.<>4__this = CS$<>8__locals1;
            <<DeriveCapabilities>b__0>d.<>1__state = -1;
            <<DeriveCapabilities>b__0>d.<>t__builder.Start<ConnectorAuth.<>c__DisplayClass36_0.<<DeriveCapabilities>b__0>d>(ref <<DeriveCapabilities>b__0>d);
            return <<DeriveCapabilities>b__0>d.<>t__builder.Task;
          });
          return;
        }
      }
    }

    // Token: 0x0600087C RID: 2172 RVA: 0x0053F5D0 File Offset: 0x0053F5D0
    public static void DeriveMissingCapabilities()
    {
      foreach (CustomConnector customConnector in from c in CustomConnectorStore.Load()
        where c.IsRemote && string.IsNullOrEmpty(c.DerivedCapabilityDescription)
        select c)
      {
        ConnectorAuth.DeriveCapabilities(customConnector.Id);
      }
    }

    // Token: 0x0600087D RID: 2173 RVA: 0x0053F644 File Offset: 0x0053F644
    public static string UnavailableText([Nullable(2)] Uri serverUrl)
    {
      if (serverUrl == null || !serverUrl.Host.EndsWith("facebook.com", StringComparison.OrdinalIgnoreCase))
      {
        return "This server only signs in clients it has approved, so HeyClicky can't log in through the browser. Add its client ID or an access token under Edit.";
      }
      return "Meta only signs in apps it has approved, so HeyClicky can't log in through the browser. Under Edit, paste your Meta app's App ID as the OAuth client ID (the app needs the \"Create & manage ads with ads MCP server\" use case and the redirect URL shown there), then Sign in.";
    }

    // Token: 0x0600087E RID: 2174 RVA: 0x0053F668 File Offset: 0x0053F668
    private static string ServerRefusalText(string refusal, [Nullable(2)] Uri serverUrl)
    {
      string text = Regex.Replace(refusal, "\\s+", " ").Trim();
      string text2 = "The server refused the connection: " + ((text.Length > 200) ? text.Substring(0, 200) : text);
      if (serverUrl != null && serverUrl.Host.EndsWith("facebook.com", StringComparison.OrdinalIgnoreCase))
      {
        text2 += " For Meta Ads, add the \"Create & manage ads with ads MCP server\" use case to your Meta app (it grants ads_mcp_management), then sign in again.";
      }
      return text2;
    }

    // Token: 0x040002E6 RID: 742
    private static readonly HttpClient Http = new HttpClient(new SocketsHttpHandler
    {
      PooledConnectionLifetime = TimeSpan.FromMinutes(5L),
      ConnectTimeout = TimeSpan.FromSeconds(10L)
    })
    {
      Timeout = TimeSpan.FromSeconds(60L)
    };

    // Token: 0x040002E7 RID: 743
    private static readonly ConcurrentDictionary<string, ConnectorSignInState> states = new ConcurrentDictionary<string, ConnectorSignInState>();

    // Token: 0x040002E8 RID: 744
    private static readonly ConcurrentDictionary<string, Guid> revisions = new ConcurrentDictionary<string, Guid>();

    // Token: 0x040002E9 RID: 745
    private static readonly ConcurrentDictionary<string, byte> probing = new ConcurrentDictionary<string, byte>();

    // Token: 0x040002EA RID: 746
    [Nullable(2)]
    private static AgentService agents;

    // Token: 0x040002EB RID: 747
    [Nullable(new byte[] { 2, 1 })]
    private static Action<string> openBrowser;

    // Token: 0x040002EC RID: 748
    [Nullable(2)]
    private static Action browserReturned;

    // Token: 0x040002ED RID: 749
    private static bool awaitingReturn;

    // Token: 0x040002EF RID: 751
    private static readonly TimeSpan RefreshMargin = TimeSpan.FromMinutes(5L);

    // Token: 0x040002F0 RID: 752
    private static readonly SemaphoreSlim RefreshGate = new SemaphoreSlim(1, 1);

    // Token: 0x040002F1 RID: 753
    public const string BrowserSignInUnavailableText = "This server only signs in clients it has approved, so HeyClicky can't log in through the browser. Add its client ID or an access token under Edit.";

    // Token: 0x02000485 RID: 1157
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x0400167C RID: 5756
      [Nullable(0)]
      public static Func<bool> <0>__AnyTokenExpiring;

      // Token: 0x0400167D RID: 5757
      [Nullable(new byte[] { 0, 1, 2, 1 })]
      public static Func<CancellationToken, Task<IReadOnlyList<CustomConnectorRegistration>>> <1>__RefreshExpiredTokensAsync;
    }
  }
}
