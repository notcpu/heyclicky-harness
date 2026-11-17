using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Core.Connectors
{
  // Token: 0x020001D8 RID: 472
  [NullableContext(1)]
  [Nullable(0)]
  public static class McpOAuth
  {
    // Token: 0x06001B19 RID: 6937 RVA: 0x003D9BA0 File Offset: 0x003D9BA0
    public static string RedirectUri(int port = 48611)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 2);
      defaultInterpolatedStringHandler.AppendLiteral("http://localhost:");
      defaultInterpolatedStringHandler.AppendFormatted<int>(port);
      defaultInterpolatedStringHandler.AppendFormatted("/callback");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06001B1A RID: 6938 RVA: 0x003D9BE0 File Offset: 0x003D9BE0
    private static HttpRequestMessage InitializeProbe(Uri serverUrl)
    {
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, serverUrl);
      httpRequestMessage.Content = new StringContent("{\"jsonrpc\":\"2.0\",\"id\":1,\"method\":\"initialize\",\"params\":{\"protocolVersion\":\"2025-06-18\",\"capabilities\":{},\"clientInfo\":{\"name\":\"HeyClicky\",\"version\":\"1\"}}}", Encoding.UTF8, "application/json");
      httpRequestMessage.Headers.Accept.ParseAdd("application/json");
      httpRequestMessage.Headers.Accept.ParseAdd("text/event-stream");
      return httpRequestMessage;
    }

    // Token: 0x06001B1B RID: 6939 RVA: 0x003D9C3C File Offset: 0x003D9C3C
    public static async Task<McpOAuthDiscovery> DiscoverAsync(HttpClient http, Uri serverUrl, CancellationToken ct = default(CancellationToken))
    {
      HttpResponseMessage httpResponseMessage;
      try
      {
        using (CancellationTokenSource timeout = CancellationTokenSource.CreateLinkedTokenSource(ct))
        {
          timeout.CancelAfter(TimeSpan.FromSeconds(15L));
          httpResponseMessage = await http.SendAsync(McpOAuth.InitializeProbe(serverUrl), HttpCompletionOption.ResponseHeadersRead, timeout.Token).ConfigureAwait(false);
        }
        CancellationTokenSource timeout = null;
      }
      catch (Exception ex) when ((ex is HttpRequestException || ex is TaskCanceledException) && !ct.IsCancellationRequested)
      {
        throw new McpOAuthException(McpOAuthFailure.ServerUnreachable, "Couldn't reach the server: " + ex.Message);
      }
      using (httpResponseMessage)
      {
        if (httpResponseMessage.StatusCode != HttpStatusCode.Unauthorized)
        {
          throw new McpOAuthException(McpOAuthFailure.ServerDoesNotRequireOAuth, "This server didn't ask for a sign-in.");
        }
        Dictionary<string, string> dictionary = McpOAuth.ParseAuthenticateChallenge(string.Join(", ", httpResponseMessage.Headers.WwwAuthenticate.Select<AuthenticationHeaderValue, string>((AuthenticationHeaderValue h) => h.ToString())));
        List<string> scopes = McpOAuth.Split(dictionary.GetValueOrDefault("scope"));
        Uri issuer = null;
        string text;
        Uri uri2;
        Uri uri = ((dictionary.TryGetValue("resource_metadata", out text) && Uri.TryCreate(text, UriKind.Absolute, out uri2)) ? uri2 : McpOAuth.WellKnownUrl(serverUrl, "oauth-protected-resource"));
        bool flag = uri != null;
        JsonObject jsonObject;
        if (flag)
        {
          jsonObject = await McpOAuth.FetchJsonAsync(http, uri, TimeSpan.FromSeconds(15L), ct).ConfigureAwait(false);
          flag = jsonObject != null;
        }
        if (flag)
        {
          JsonArray jsonArray = jsonObject["authorization_servers"] as JsonArray;
          string text2;
          if (jsonArray == null)
          {
            text2 = null;
          }
          else
          {
            JsonNode jsonNode = jsonArray.FirstOrDefault<JsonNode>();
            text2 = ((jsonNode != null) ? jsonNode.ToString() : null);
          }
          string text3 = text2;
          Uri uri3;
          if (text3 != null && Uri.TryCreate(text3, UriKind.Absolute, out uri3))
          {
            issuer = uri3;
          }
          if (scopes.Count == 0)
          {
            scopes = McpOAuth.Strings(jsonObject["scopes_supported"]);
          }
        }
        if (issuer == null)
        {
          issuer = McpOAuth.Origin(serverUrl);
        }
        JsonObject metadata = null;
        foreach (Uri uri4 in McpOAuth.AuthorizationServerMetadataCandidates(issuer))
        {
          JsonObject jsonObject2 = await McpOAuth.FetchJsonAsync(http, uri4, TimeSpan.FromSeconds(15L), ct).ConfigureAwait(false);
          if (jsonObject2 != null && jsonObject2["authorization_endpoint"] != null)
          {
            metadata = jsonObject2;
            break;
          }
        }
        IEnumerator<Uri> enumerator = null;
        if (metadata != null)
        {
          JsonNode jsonNode2 = metadata["authorization_endpoint"];
          Uri uri5;
          if (Uri.TryCreate((jsonNode2 != null) ? jsonNode2.ToString() : null, UriKind.Absolute, out uri5))
          {
            JsonNode jsonNode3 = metadata["token_endpoint"];
            Uri uri6;
            if (Uri.TryCreate((jsonNode3 != null) ? jsonNode3.ToString() : null, UriKind.Absolute, out uri6))
            {
              List<string> list = scopes.ToList<string>();
              if (scopes.Count == 0)
              {
                scopes = McpOAuth.Strings(metadata["scopes_supported"]);
              }
              JsonNode jsonNode4 = metadata["registration_endpoint"];
              Uri uri7;
              Uri.TryCreate((jsonNode4 != null) ? jsonNode4.ToString() : null, UriKind.Absolute, out uri7);
              return new McpOAuthDiscovery(uri5, uri6, uri7, scopes, serverUrl.AbsoluteUri)
              {
                ResourceScopes = list
              };
            }
          }
        }
        McpOAuthFailure mcpOAuthFailure = McpOAuthFailure.DiscoveryFailed;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(81, 1);
        defaultInterpolatedStringHandler.AppendLiteral("Couldn't read the server's sign-in settings: no authorization server metadata at ");
        defaultInterpolatedStringHandler.AppendFormatted<Uri>(issuer);
        throw new McpOAuthException(mcpOAuthFailure, defaultInterpolatedStringHandler.ToStringAndClear());
      }
      McpOAuthDiscovery mcpOAuthDiscovery;
      return mcpOAuthDiscovery;
    }

    // Token: 0x06001B1C RID: 6940 RVA: 0x003D9C90 File Offset: 0x003D9C90
    public static async Task<McpOAuthSignInSupport> ProbeSignInSupportAsync(HttpClient http, Uri serverUrl, bool hasClientId, CancellationToken ct = default(CancellationToken))
    {
      McpOAuthSignInSupport mcpOAuthSignInSupport;
      try
      {
        ConfiguredTaskAwaitable<McpOAuthDiscovery>.ConfiguredTaskAwaiter configuredTaskAwaiter = McpOAuth.DiscoverAsync(http, serverUrl, ct).ConfigureAwait(false).GetAwaiter();
        if (!configuredTaskAwaiter.IsCompleted)
        {
          await configuredTaskAwaiter;
          ConfiguredTaskAwaitable<McpOAuthDiscovery>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
          configuredTaskAwaiter = configuredTaskAwaiter2;
          configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<McpOAuthDiscovery>.ConfiguredTaskAwaiter);
        }
        mcpOAuthSignInSupport = ((configuredTaskAwaiter.GetResult().RegistrationEndpoint != null || hasClientId) ? McpOAuthSignInSupport.Supported : McpOAuthSignInSupport.Unsupported);
      }
      catch (McpOAuthException ex) when (ex.Failure == McpOAuthFailure.ServerDoesNotRequireOAuth)
      {
        mcpOAuthSignInSupport = McpOAuthSignInSupport.NotNeeded;
      }
      catch (McpOAuthException)
      {
        mcpOAuthSignInSupport = McpOAuthSignInSupport.Unknown;
      }
      return mcpOAuthSignInSupport;
    }

    // Token: 0x06001B1D RID: 6941 RVA: 0x003D9CEC File Offset: 0x003D9CEC
    public unsafe static JsonObject RegistrationRequestBody(string redirectUri)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["client_name"] = "HeyClicky";
      string text = "redirect_uris";
      JsonNode jsonNode = redirectUri;
      jsonObject[text] = new JsonArray(new ReadOnlySpan<JsonNode>(ref jsonNode));
      string text2 = "grant_types";
      <>y__InlineArray2<JsonNode> <>y__InlineArray = default(<>y__InlineArray2<JsonNode>);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray2<JsonNode>, JsonNode>(ref <>y__InlineArray, 0) = "authorization_code";
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray2<JsonNode>, JsonNode>(ref <>y__InlineArray, 1) = "refresh_token";
      jsonObject[text2] = new JsonArray(<PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray2<JsonNode>, JsonNode>(in <>y__InlineArray, 2));
      string text3 = "response_types";
      JsonNode jsonNode2 = "code";
      jsonObject[text3] = new JsonArray(new ReadOnlySpan<JsonNode>(ref jsonNode2));
      jsonObject["token_endpoint_auth_method"] = "none";
      return jsonObject;
    }

    // Token: 0x06001B1E RID: 6942 RVA: 0x003D9DB8 File Offset: 0x003D9DB8
    public static async Task<McpOAuthClientRegistration> RegisterClientAsync(HttpClient http, McpOAuthDiscovery discovery, string redirectUri, CancellationToken ct = default(CancellationToken))
    {
      Uri registrationEndpoint = discovery.RegistrationEndpoint;
      if (registrationEndpoint == null)
      {
        throw new McpOAuthException(McpOAuthFailure.RegistrationFailed, "This server only signs in clients it has approved. Add its client ID or an access token under Edit.");
      }
      JsonObject jsonObject = McpOAuth.RegistrationRequestBody(redirectUri);
      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, registrationEndpoint)
      {
        Content = new StringContent(jsonObject.ToJsonString(null), Encoding.UTF8, "application/json")
      })
      {
        request.Headers.Accept.ParseAdd("application/json");
        HttpResponseMessage httpResponseMessage = await http.SendAsync(request, ct).ConfigureAwait(false);
        using (HttpResponseMessage response = httpResponseMessage)
        {
          JsonObject jsonObject2 = await McpOAuth.ReadJsonAsync(response, ct).ConfigureAwait(false);
          if (response.IsSuccessStatusCode)
          {
            string text;
            if (jsonObject2 == null)
            {
              text = null;
            }
            else
            {
              JsonNode jsonNode = jsonObject2["client_id"];
              text = ((jsonNode != null) ? jsonNode.ToString() : null);
            }
            string text2 = text;
            if (text2 != null && text2.Length > 0)
            {
              string text3 = text2;
              JsonNode jsonNode2 = jsonObject2["client_secret"];
              string text4 = ((jsonNode2 != null) ? jsonNode2.ToString() : null);
              return new McpOAuthClientRegistration(text3, (text4 != null && text4.Length > 0) ? text4 : null);
            }
          }
          throw new McpOAuthException(McpOAuthFailure.RegistrationFailed, "The server refused to register HeyClicky: " + McpOAuth.ErrorDetail(jsonObject2, response));
        }
      }
      McpOAuthClientRegistration mcpOAuthClientRegistration;
      return mcpOAuthClientRegistration;
    }

    // Token: 0x06001B1F RID: 6943 RVA: 0x003D9E14 File Offset: 0x003D9E14
    public static async Task<McpOAuthSignInResult> SignInAsync(HttpClient http, Uri serverUrl, [Nullable(2)] string clientId, Action<Uri> openBrowser, CancellationToken ct = default(CancellationToken), int port = 48611, TimeSpan? timeout = null, [Nullable(2)] McpOAuthClientRegistration previouslyRegistered = null)
    {
      McpOAuthDiscovery mcpOAuthDiscovery = await McpOAuth.DiscoverAsync(http, serverUrl, ct).ConfigureAwait(false);
      McpOAuthDiscovery discovery = mcpOAuthDiscovery;
      string redirectUri = McpOAuth.RedirectUri(port);
      bool dynamic = string.IsNullOrWhiteSpace(clientId);
      McpOAuthClientRegistration mcpOAuthClientRegistration;
      if (!dynamic)
      {
        mcpOAuthClientRegistration = new McpOAuthClientRegistration(clientId.Trim(), null);
      }
      else
      {
        McpOAuthClientRegistration mcpOAuthClientRegistration2 = previouslyRegistered;
        if (mcpOAuthClientRegistration2 == null)
        {
          mcpOAuthClientRegistration2 = await McpOAuth.RegisterClientAsync(http, discovery, redirectUri, ct).ConfigureAwait(false);
        }
        mcpOAuthClientRegistration = mcpOAuthClientRegistration2;
      }
      McpOAuthClientRegistration client = mcpOAuthClientRegistration;
      IReadOnlyList<string> readOnlyList = McpOAuth.SignInScopes(discovery, dynamic);
      ValueTuple<string, string> valueTuple = McpOAuth.NewPkce();
      string verifier = valueTuple.Item1;
      string item = valueTuple.Item2;
      string state = McpOAuth.RandomUrlSafe(24);
      McpOAuthSignInResult mcpOAuthSignInResult;
      using (LoopbackCallbackServer callback = new LoopbackCallbackServer(port, "/callback", state))
      {
        List<ValueTuple<string, string>> list = new List<ValueTuple<string, string>>
        {
          new ValueTuple<string, string>("response_type", "code"),
          new ValueTuple<string, string>("client_id", client.ClientId),
          new ValueTuple<string, string>("redirect_uri", redirectUri),
          new ValueTuple<string, string>("state", state),
          new ValueTuple<string, string>("code_challenge", item),
          new ValueTuple<string, string>("code_challenge_method", "S256"),
          new ValueTuple<string, string>("resource", discovery.Resource)
        };
        if (readOnlyList.Count > 0)
        {
          list.Add(new ValueTuple<string, string>("scope", string.Join<string>(' ', readOnlyList)));
        }
        UriBuilder uriBuilder = new UriBuilder(discovery.AuthorizationEndpoint);
        string text = uriBuilder.Query.TrimStart('?');
        uriBuilder.Query = ((text.Length > 0) ? (text + "&") : "") + McpOAuth.FormEncode(list);
        openBrowser(uriBuilder.Uri);
        Dictionary<string, string> dictionary = await callback.WaitAsync(timeout ?? McpOAuth.SignInTimeout, ct).ConfigureAwait(false);
        string text2;
        if (dictionary.TryGetValue("error", out text2))
        {
          McpOAuthFailure mcpOAuthFailure = McpOAuthFailure.AuthorizationDenied;
          string valueOrDefault = dictionary.GetValueOrDefault("error_description");
          throw new McpOAuthException(mcpOAuthFailure, (valueOrDefault != null && valueOrDefault.Length > 0) ? valueOrDefault : ((text2.Length > 0) ? text2 : "The sign-in was cancelled."));
        }
        if (dictionary.GetValueOrDefault("state") != state)
        {
          throw new McpOAuthException(McpOAuthFailure.StateMismatch, "The sign-in callback didn't match this request.");
        }
        string valueOrDefault2 = dictionary.GetValueOrDefault("code");
        if (valueOrDefault2 == null || valueOrDefault2.Length <= 0)
        {
          throw new McpOAuthException(McpOAuthFailure.AuthorizationDenied, "The sign-in returned no code.");
        }
        List<ValueTuple<string, string>> list2 = new List<ValueTuple<string, string>>
        {
          new ValueTuple<string, string>("grant_type", "authorization_code"),
          new ValueTuple<string, string>("code", valueOrDefault2),
          new ValueTuple<string, string>("redirect_uri", redirectUri),
          new ValueTuple<string, string>("client_id", client.ClientId),
          new ValueTuple<string, string>("code_verifier", verifier),
          new ValueTuple<string, string>("resource", discovery.Resource)
        };
        string clientSecret = client.ClientSecret;
        if (clientSecret != null)
        {
          list2.Add(new ValueTuple<string, string>("client_secret", clientSecret));
        }
        ConfiguredTaskAwaitable<McpOAuthTokens>.ConfiguredTaskAwaiter configuredTaskAwaiter = McpOAuth.RequestTokensAsync(http, discovery.TokenEndpoint, list2, ct).ConfigureAwait(false).GetAwaiter();
        if (!configuredTaskAwaiter.IsCompleted)
        {
          await configuredTaskAwaiter;
          ConfiguredTaskAwaitable<McpOAuthTokens>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
          configuredTaskAwaiter = configuredTaskAwaiter2;
          configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<McpOAuthTokens>.ConfiguredTaskAwaiter);
        }
        mcpOAuthSignInResult = new McpOAuthSignInResult(configuredTaskAwaiter.GetResult(), discovery.TokenEndpoint, client, dynamic);
      }
      return mcpOAuthSignInResult;
    }

    // Token: 0x06001B20 RID: 6944 RVA: 0x003D9E94 File Offset: 0x003D9E94
    public static IReadOnlyList<string> SignInScopes(McpOAuthDiscovery discovery, bool isRegisteredClient)
    {
      if (!isRegisteredClient)
      {
        return discovery.Scopes;
      }
      return discovery.ResourceScopes;
    }

    // Token: 0x06001B21 RID: 6945 RVA: 0x003D9EA8 File Offset: 0x003D9EA8
    public static Task<McpOAuthTokens> RefreshAsync(HttpClient http, Uri tokenEndpoint, McpOAuthClientRegistration client, string refreshToken, CancellationToken ct = default(CancellationToken))
    {
      List<ValueTuple<string, string>> list = new List<ValueTuple<string, string>>
      {
        new ValueTuple<string, string>("grant_type", "refresh_token"),
        new ValueTuple<string, string>("refresh_token", refreshToken),
        new ValueTuple<string, string>("client_id", client.ClientId)
      };
      string clientSecret = client.ClientSecret;
      if (clientSecret != null)
      {
        list.Add(new ValueTuple<string, string>("client_secret", clientSecret));
      }
      return McpOAuth.RequestTokensAsync(http, tokenEndpoint, list, ct);
    }

    // Token: 0x06001B22 RID: 6946 RVA: 0x003D9F1C File Offset: 0x003D9F1C
    public static bool IsRefusedRefresh(McpOAuthException error)
    {
      return error.Failure == McpOAuthFailure.TokenExchangeFailed && new string[] { "invalid_grant", "invalid_client", "unauthorized", "HTTP 400", "HTTP 401", "HTTP 403" }.Any<string>((string marker) => error.Message.Contains(marker, StringComparison.OrdinalIgnoreCase));
    }

    // Token: 0x06001B23 RID: 6947 RVA: 0x003D9F90 File Offset: 0x003D9F90
    private static async Task<McpOAuthTokens> RequestTokensAsync(HttpClient http, Uri tokenEndpoint, [Nullable(new byte[] { 1, 0, 1, 1 })] List<ValueTuple<string, string>> form, CancellationToken ct)
    {
      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint)
      {
        Content = new StringContent(McpOAuth.FormEncode(form), Encoding.UTF8, "application/x-www-form-urlencoded")
      })
      {
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        request.Headers.Accept.ParseAdd("application/json");
        HttpResponseMessage response;
        try
        {
          HttpResponseMessage httpResponseMessage = await http.SendAsync(request, ct).ConfigureAwait(false);
          response = httpResponseMessage;
        }
        catch (HttpRequestException ex)
        {
          throw new McpOAuthException(McpOAuthFailure.TokenExchangeFailed, "The server refused to issue a token: " + ex.Message);
        }
        using (response)
        {
          JsonObject jsonObject = await McpOAuth.ReadJsonAsync(response, ct).ConfigureAwait(false);
          if (response.IsSuccessStatusCode)
          {
            string text;
            if (jsonObject == null)
            {
              text = null;
            }
            else
            {
              JsonNode jsonNode = jsonObject["access_token"];
              text = ((jsonNode != null) ? jsonNode.ToString() : null);
            }
            string text2 = text;
            if (text2 != null && text2.Length > 0)
            {
              JsonValue jsonValue = jsonObject["expires_in"] as JsonValue;
              double num2;
              double? num = ((jsonValue != null && (jsonValue.TryGetValue<double>(out num2) || double.TryParse(jsonValue.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num2))) ? new double?(num2) : null);
              JsonNode jsonNode2 = jsonObject["refresh_token"];
              string text3 = ((jsonNode2 != null) ? jsonNode2.ToString() : null);
              string text4 = ((text3 != null && text3.Length > 0) ? text3 : null);
              DateTimeOffset? dateTimeOffset;
              if (num != null)
              {
                double valueOrDefault = num.GetValueOrDefault();
                dateTimeOffset = new DateTimeOffset?(DateTimeOffset.UtcNow.AddSeconds(valueOrDefault));
              }
              else
              {
                dateTimeOffset = null;
              }
              return new McpOAuthTokens(text2, text4, dateTimeOffset);
            }
          }
          throw new McpOAuthException(McpOAuthFailure.TokenExchangeFailed, "The server refused to issue a token: " + McpOAuth.ErrorDetail(jsonObject, response));
        }
      }
      McpOAuthTokens mcpOAuthTokens;
      return mcpOAuthTokens;
    }

    // Token: 0x06001B24 RID: 6948 RVA: 0x003D9FEC File Offset: 0x003D9FEC
    [return: Nullable(new byte[] { 1, 2 })]
    public static async Task<string> VerifyBearerTokenAsync(HttpClient http, Uri serverUrl, string accessToken, CancellationToken ct = default(CancellationToken))
    {
      string text;
      try
      {
        using (HttpRequestMessage request = McpOAuth.InitializeProbe(serverUrl))
        {
          request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
          request.Headers.TryAddWithoutValidation("MCP-Protocol-Version", "2025-06-18");
          using (CancellationTokenSource timeout = CancellationTokenSource.CreateLinkedTokenSource(ct))
          {
            timeout.CancelAfter(TimeSpan.FromSeconds(20L));
            HttpResponseMessage httpResponseMessage = await http.SendAsync(request, timeout.Token).ConfigureAwait(false);
            using (HttpResponseMessage response = httpResponseMessage)
            {
              HttpStatusCode statusCode = response.StatusCode;
              if (statusCode != HttpStatusCode.Unauthorized && statusCode != HttpStatusCode.Forbidden)
              {
                text = null;
              }
              else
              {
                JsonObject jsonObject = await McpOAuth.ReadJsonAsync(response, ct).ConfigureAwait(false);
                JsonNode jsonNode;
                if ((jsonNode = ((jsonObject != null) ? jsonObject["title"] : null)) == null)
                {
                  jsonNode = ((jsonObject != null) ? jsonObject["detail"] : null) ?? ((jsonObject != null) ? jsonObject["error"] : null);
                }
                JsonNode jsonNode2 = jsonNode;
                string text2;
                if (jsonNode2 == null)
                {
                  text2 = null;
                }
                else
                {
                  string text3 = jsonNode2.ToString();
                  text2 = ((text3 != null) ? text3.Trim() : null);
                }
                string text4 = text2;
                string text5;
                if (!string.IsNullOrEmpty(text4))
                {
                  text5 = text4;
                }
                else
                {
                  DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(37, 1);
                  defaultInterpolatedStringHandler.AppendLiteral("The server refused the token (HTTP ");
                  defaultInterpolatedStringHandler.AppendFormatted<int>((int)response.StatusCode);
                  defaultInterpolatedStringHandler.AppendLiteral(").");
                  text5 = defaultInterpolatedStringHandler.ToStringAndClear();
                }
                text = text5;
              }
            }
          }
        }
      }
      catch (Exception ex) when ((ex is HttpRequestException || ex is TaskCanceledException) && !ct.IsCancellationRequested)
      {
        text = null;
      }
      return text;
    }

    // Token: 0x06001B25 RID: 6949 RVA: 0x003DA048 File Offset: 0x003DA048
    [GeneratedRegex("([A-Za-z_]+)\\s*=\\s*\"([^\"]*)\"")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex ChallengeParameter()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ChallengeParameter_3.Instance;
    }

    // Token: 0x06001B26 RID: 6950 RVA: 0x003DA050 File Offset: 0x003DA050
    public static Dictionary<string, string> ParseAuthenticateChallenge(string header)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
      foreach (object obj in McpOAuth.ChallengeParameter().Matches(header ?? ""))
      {
        Match match = (Match)obj;
        dictionary[match.Groups[1].Value.ToLowerInvariant()] = match.Groups[2].Value;
      }
      return dictionary;
    }

    // Token: 0x06001B27 RID: 6951 RVA: 0x003DA0EC File Offset: 0x003DA0EC
    [return: TupleElementNames(new string[] { "Verifier", "Challenge" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    public static ValueTuple<string, string> NewPkce()
    {
      string text = McpOAuth.RandomUrlSafe(48);
      return new ValueTuple<string, string>(text, McpOAuth.Base64Url(SHA256.HashData(Encoding.ASCII.GetBytes(text))));
    }

    // Token: 0x06001B28 RID: 6952 RVA: 0x003DA11C File Offset: 0x003DA11C
    public static string RandomUrlSafe(int bytes)
    {
      return McpOAuth.Base64Url(RandomNumberGenerator.GetBytes(bytes));
    }

    // Token: 0x06001B29 RID: 6953 RVA: 0x003DA12C File Offset: 0x003DA12C
    public static string Base64Url(byte[] bytes)
    {
      return Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_')
        .TrimEnd('=');
    }

    // Token: 0x06001B2A RID: 6954 RVA: 0x003DA150 File Offset: 0x003DA150
    public static string FormEncode([TupleElementNames(new string[] { "Key", "Value" })] [Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<ValueTuple<string, string>> pairs)
    {
      return string.Join<string>('&', pairs.Select<ValueTuple<string, string>, string>(([TupleElementNames(new string[] { "Key", "Value" })] ValueTuple<string, string> p) => Uri.EscapeDataString(p.Item1) + "=" + Uri.EscapeDataString(p.Item2)));
    }

    // Token: 0x06001B2B RID: 6955 RVA: 0x003DA180 File Offset: 0x003DA180
    private static Uri Origin(Uri url)
    {
      return new Uri(url.Scheme + "://" + url.Authority);
    }

    // Token: 0x06001B2C RID: 6956 RVA: 0x003DA1A0 File Offset: 0x003DA1A0
    [return: Nullable(2)]
    public static Uri WellKnownUrl(Uri url, string suffix)
    {
      string text = url.AbsolutePath.Trim('/');
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 4);
      defaultInterpolatedStringHandler.AppendFormatted(url.Scheme);
      defaultInterpolatedStringHandler.AppendLiteral("://");
      defaultInterpolatedStringHandler.AppendFormatted(url.Authority);
      defaultInterpolatedStringHandler.AppendLiteral("/.well-known/");
      defaultInterpolatedStringHandler.AppendFormatted(suffix);
      defaultInterpolatedStringHandler.AppendFormatted((text.Length == 0) ? "" : ("/" + text));
      Uri uri;
      if (!Uri.TryCreate(defaultInterpolatedStringHandler.ToStringAndClear(), UriKind.Absolute, out uri))
      {
        return null;
      }
      return uri;
    }

    // Token: 0x06001B2D RID: 6957 RVA: 0x003DA234 File Offset: 0x003DA234
    private static IEnumerable<Uri> AuthorizationServerMetadataCandidates(Uri issuer)
    {
      McpOAuth.<AuthorizationServerMetadataCandidates>d__25 <AuthorizationServerMetadataCandidates>d__ = new McpOAuth.<AuthorizationServerMetadataCandidates>d__25(-2);
      <AuthorizationServerMetadataCandidates>d__.<>3__issuer = issuer;
      return <AuthorizationServerMetadataCandidates>d__;
    }

    // Token: 0x06001B2E RID: 6958 RVA: 0x003DA244 File Offset: 0x003DA244
    [return: Nullable(new byte[] { 1, 2 })]
    private static async Task<JsonObject> FetchJsonAsync(HttpClient http, Uri url, TimeSpan timeout, CancellationToken ct)
    {
      JsonObject jsonObject;
      try
      {
        using (CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
        {
          cts.CancelAfter(timeout);
          using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
          {
            request.Headers.Accept.ParseAdd("application/json");
            HttpResponseMessage httpResponseMessage = await http.SendAsync(request, cts.Token).ConfigureAwait(false);
            using (HttpResponseMessage response = httpResponseMessage)
            {
              jsonObject = ((!response.IsSuccessStatusCode) ? null : (await McpOAuth.ReadJsonAsync(response, cts.Token).ConfigureAwait(false)));
            }
          }
        }
      }
      catch (Exception ex) when ((ex is HttpRequestException || ex is TaskCanceledException) && !ct.IsCancellationRequested)
      {
        jsonObject = null;
      }
      return jsonObject;
    }

    // Token: 0x06001B2F RID: 6959 RVA: 0x003DA2A0 File Offset: 0x003DA2A0
    [return: Nullable(new byte[] { 1, 2 })]
    private static async Task<JsonObject> ReadJsonAsync(HttpResponseMessage response, CancellationToken ct)
    {
      JsonObject jsonObject;
      try
      {
        jsonObject = JsonNode.Parse(await response.Content.ReadAsStringAsync(ct).ConfigureAwait(false), null, default(JsonDocumentOptions)) as JsonObject;
      }
      catch (JsonException)
      {
        jsonObject = null;
      }
      return jsonObject;
    }

    // Token: 0x06001B30 RID: 6960 RVA: 0x003DA2EC File Offset: 0x003DA2EC
    private static string ErrorDetail([Nullable(2)] JsonObject json, HttpResponseMessage response)
    {
      string text;
      if (json == null)
      {
        text = null;
      }
      else
      {
        JsonNode jsonNode = json["error_description"];
        text = ((jsonNode != null) ? jsonNode.ToString() : null);
      }
      string text2 = text;
      if (text2 != null && text2.Length > 0)
      {
        return text2;
      }
      JsonObject jsonObject = ((json != null) ? json["error"] : null) as JsonObject;
      if (jsonObject != null)
      {
        JsonNode jsonNode2 = jsonObject["message"];
        string text3 = ((jsonNode2 != null) ? jsonNode2.ToString() : null);
        if (text3 != null && text3.Length > 0)
        {
          return text3;
        }
      }
      string text4;
      if (json == null)
      {
        text4 = null;
      }
      else
      {
        JsonNode jsonNode3 = json["error"];
        text4 = ((jsonNode3 != null) ? jsonNode3.ToString() : null);
      }
      string text5 = text4;
      if (text5 == null || text5.Length <= 0)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
        defaultInterpolatedStringHandler.AppendLiteral("HTTP ");
        defaultInterpolatedStringHandler.AppendFormatted<int>((int)response.StatusCode);
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
      return text5;
    }

    // Token: 0x06001B31 RID: 6961 RVA: 0x003DA3C0 File Offset: 0x003DA3C0
    private static List<string> Split([Nullable(2)] string value)
    {
      return (value ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList<string>();
    }

    // Token: 0x06001B32 RID: 6962 RVA: 0x003DA3DC File Offset: 0x003DA3DC
    private static List<string> Strings([Nullable(2)] JsonNode node)
    {
      JsonArray jsonArray;
      if ((jsonArray = node as JsonArray) == null)
      {
        jsonArray = new JsonArray(null);
      }
      return (from s in jsonArray.Select<JsonNode, string>(delegate(JsonNode n)
        {
          if (n == null)
          {
            return null;
          }
          string text = n.ToString();
          if (text == null)
          {
            return null;
          }
          return text.Trim();
        }).OfType<string>()
        where s.Length > 0
        select s).ToList<string>();
    }

    // Token: 0x04000B9A RID: 2970
    public const int CallbackPort = 48611;

    // Token: 0x04000B9B RID: 2971
    public const string CallbackPath = "/callback";

    // Token: 0x04000B9C RID: 2972
    public static readonly TimeSpan SignInTimeout = TimeSpan.FromMinutes(5L);

    // Token: 0x04000B9D RID: 2973
    private const string ProtocolVersion = "2025-06-18";

    // Token: 0x04000B9E RID: 2974
    private const string InitializeBody = "{\"jsonrpc\":\"2.0\",\"id\":1,\"method\":\"initialize\",\"params\":{\"protocolVersion\":\"2025-06-18\",\"capabilities\":{},\"clientInfo\":{\"name\":\"HeyClicky\",\"version\":\"1\"}}}";
  }
}
