using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using HeyClicky.Agents.Runtime;
using HeyClicky.Core.Auth;
using HeyClicky.Core.Connectors;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000CB RID: 203
  [NullableContext(1)]
  [Nullable(0)]
  public static class CustomConnectorStore
  {
    // Token: 0x1700016D RID: 365
    // (get) Token: 0x060008C9 RID: 2249 RVA: 0x005404BC File Offset: 0x005404BC
    private static string Dir
    {
      get
      {
        return AppPaths.Root;
      }
    }

    // Token: 0x1700016E RID: 366
    // (get) Token: 0x060008CA RID: 2250 RVA: 0x005404C4 File Offset: 0x005404C4
    private static string ListPath
    {
      get
      {
        return Path.Combine(CustomConnectorStore.Dir, "connectors.json");
      }
    }

    // Token: 0x1700016F RID: 367
    // (get) Token: 0x060008CB RID: 2251 RVA: 0x005404D8 File Offset: 0x005404D8
    private static string SecretsPath
    {
      get
      {
        return Path.Combine(CustomConnectorStore.Dir, "connectors.secrets");
      }
    }

    // Token: 0x1400001F RID: 31
    // (add) Token: 0x060008CC RID: 2252 RVA: 0x005404EC File Offset: 0x005404EC
    // (remove) Token: 0x060008CD RID: 2253 RVA: 0x00540520 File Offset: 0x00540520
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public static event Action Changed;

    // Token: 0x14000020 RID: 32
    // (add) Token: 0x060008CE RID: 2254 RVA: 0x00540554 File Offset: 0x00540554
    // (remove) Token: 0x060008CF RID: 2255 RVA: 0x00540588 File Offset: 0x00540588
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public static event Action Updated;

    // Token: 0x060008D0 RID: 2256 RVA: 0x005405BC File Offset: 0x005405BC
    public static List<CustomConnector> Load()
    {
      object gate = CustomConnectorStore.Gate;
      List<CustomConnector> list2;
      lock (gate)
      {
        try
        {
          if (File.Exists(CustomConnectorStore.ListPath))
          {
            List<CustomConnector> list = JsonSerializer.Deserialize<List<CustomConnector>>(File.ReadAllText(CustomConnectorStore.ListPath), CustomConnectorStore.ConnectorJsonContext.Default.ListCustomConnector) ?? new List<CustomConnector>();
            int num = list.RemoveAll((CustomConnector c) => c == null || string.IsNullOrWhiteSpace(c.Id) || string.IsNullOrWhiteSpace(c.Name));
            if (num > 0)
            {
              string text = "connectors";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(60, 1);
              defaultInterpolatedStringHandler.AppendLiteral("connectors.json: skipped ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(num);
              defaultInterpolatedStringHandler.AppendLiteral(" entry(ies) without an id or a name");
              Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
            }
            return list;
          }
        }
        catch (Exception ex)
        {
          Log.Warn("connectors", "connectors.json unreadable: " + ex.Message);
        }
        list2 = new List<CustomConnector>();
      }
      return list2;
    }

    // Token: 0x060008D1 RID: 2257 RVA: 0x005406C4 File Offset: 0x005406C4
    [return: Nullable(2)]
    public static CustomConnector Get(string id)
    {
      return CustomConnectorStore.Load().FirstOrDefault<CustomConnector>((CustomConnector c) => c.Id == id);
    }

    // Token: 0x060008D2 RID: 2258 RVA: 0x005406F4 File Offset: 0x005406F4
    private static void WriteList(List<CustomConnector> list)
    {
      Directory.CreateDirectory(CustomConnectorStore.Dir);
      AtomicFile.WriteAllBytes(CustomConnectorStore.ListPath, JsonSerializer.SerializeToUtf8Bytes<List<CustomConnector>>(list, CustomConnectorStore.ConnectorJsonContext.Default.ListCustomConnector));
    }

    // Token: 0x060008D3 RID: 2259 RVA: 0x00540720 File Offset: 0x00540720
    private static Dictionary<string, string> LoadSecrets()
    {
      Dictionary<string, string> dictionary;
      try
      {
        if (!File.Exists(CustomConnectorStore.SecretsPath))
        {
          dictionary = new Dictionary<string, string>();
        }
        else
        {
          dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(ProtectedData.Unprotect(File.ReadAllBytes(CustomConnectorStore.SecretsPath), CustomConnectorStore.Entropy, DataProtectionScope.CurrentUser), CustomConnectorStore.ConnectorJsonContext.Default.DictionaryStringString) ?? new Dictionary<string, string>();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("connectors", "secrets unreadable: " + ex.Message);
        dictionary = new Dictionary<string, string>();
      }
      return dictionary;
    }

    // Token: 0x060008D4 RID: 2260 RVA: 0x005407AC File Offset: 0x005407AC
    private static void SaveSecrets(Dictionary<string, string> secrets)
    {
      Directory.CreateDirectory(CustomConnectorStore.Dir);
      byte[] array = ProtectedData.Protect(JsonSerializer.SerializeToUtf8Bytes<Dictionary<string, string>>(secrets, CustomConnectorStore.ConnectorJsonContext.Default.DictionaryStringString), CustomConnectorStore.Entropy, DataProtectionScope.CurrentUser);
      AtomicFile.WriteAllBytes(CustomConnectorStore.SecretsPath, array);
    }

    // Token: 0x060008D5 RID: 2261 RVA: 0x005407F0 File Offset: 0x005407F0
    public static Dictionary<string, string> SecretsFor(string id)
    {
      object gate = CustomConnectorStore.Gate;
      Dictionary<string, string> dictionary;
      lock (gate)
      {
        dictionary = (from kv in CustomConnectorStore.LoadSecrets()
          where kv.Key.StartsWith(id + "|", StringComparison.Ordinal)
          select kv).ToDictionary<KeyValuePair<string, string>, string, string>(delegate(KeyValuePair<string, string> kv)
        {
          string key = kv.Key;
          int num = id.Length + 1;
          return key.Substring(num, key.Length - num);
        }, (KeyValuePair<string, string> kv) => kv.Value);
      }
      return dictionary;
    }

    // Token: 0x060008D6 RID: 2262 RVA: 0x00540880 File Offset: 0x00540880
    public static CustomConnectorSecrets ReadSecrets(string id)
    {
      Dictionary<string, string> dictionary = CustomConnectorStore.SecretsFor(id);
      DateTimeOffset dateTimeOffset;
      CustomConnectorSecrets customConnectorSecrets = new CustomConnectorSecrets
      {
        BearerToken = dictionary.GetValueOrDefault("api_key"),
        RefreshToken = dictionary.GetValueOrDefault("oauth_refresh"),
        TokenEndpoint = dictionary.GetValueOrDefault("oauth_token_endpoint"),
        RegisteredClientId = dictionary.GetValueOrDefault("oauth_client_id"),
        RegisteredClientSecret = dictionary.GetValueOrDefault("oauth_client_secret"),
        RegisteredClientRedirectUri = dictionary.GetValueOrDefault("oauth_client_redirect"),
        ExpiresAt = (DateTimeOffset.TryParse(dictionary.GetValueOrDefault("oauth_expires"), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dateTimeOffset) ? new DateTimeOffset?(dateTimeOffset) : null)
      };
      foreach (KeyValuePair<string, string> keyValuePair in dictionary)
      {
        string text;
        string text2;
        keyValuePair.Deconstruct(out text, out text2);
        string text3 = text;
        string text4 = text2;
        if (text3.StartsWith("env:", StringComparison.Ordinal))
        {
          Dictionary<string, string> environment = customConnectorSecrets.Environment;
          text2 = text3;
          environment[text2.Substring(4, text2.Length - 4)] = text4;
        }
        else if (text3.StartsWith("header:", StringComparison.Ordinal))
        {
          Dictionary<string, string> headers = customConnectorSecrets.Headers;
          text2 = text3;
          headers[text2.Substring(7, text2.Length - 7)] = text4;
        }
      }
      return customConnectorSecrets;
    }

    // Token: 0x060008D7 RID: 2263 RVA: 0x005409F0 File Offset: 0x005409F0
    private static void WriteSecrets(Dictionary<string, string> all, string id, CustomConnectorSecrets s)
    {
      CustomConnectorStore.<>c__DisplayClass22_0 CS$<>8__locals1 = new CustomConnectorStore.<>c__DisplayClass22_0();
      CS$<>8__locals1.id = id;
      CS$<>8__locals1.all = all;
      IEnumerable<string> keys = CS$<>8__locals1.all.Keys;
      Func<string, bool> func;
      if ((func = CS$<>8__locals1.<>9__1) == null)
      {
        func = (CS$<>8__locals1.<>9__1 = (string k) => k.StartsWith(CS$<>8__locals1.id + "|", StringComparison.Ordinal));
      }
      foreach (string text in keys.Where<string>(func).ToList<string>())
      {
        CS$<>8__locals1.all.Remove(text);
      }
      CS$<>8__locals1.<WriteSecrets>g__Put|0("api_key", s.BearerToken);
      CS$<>8__locals1.<WriteSecrets>g__Put|0("oauth_refresh", s.RefreshToken);
      CS$<>8__locals1.<WriteSecrets>g__Put|0("oauth_token_endpoint", s.TokenEndpoint);
      CS$<>8__locals1.<WriteSecrets>g__Put|0("oauth_client_id", s.RegisteredClientId);
      CS$<>8__locals1.<WriteSecrets>g__Put|0("oauth_client_secret", s.RegisteredClientSecret);
      CS$<>8__locals1.<WriteSecrets>g__Put|0("oauth_client_redirect", s.RegisteredClientRedirectUri);
      DateTimeOffset? dateTimeOffset;
      CS$<>8__locals1.<WriteSecrets>g__Put|0("oauth_expires", (s.ExpiresAt != null) ? dateTimeOffset.GetValueOrDefault().ToString("O", CultureInfo.InvariantCulture) : null);
      foreach (KeyValuePair<string, string> keyValuePair in s.Environment)
      {
        string text2;
        string text3;
        keyValuePair.Deconstruct(out text2, out text3);
        string text4 = text2;
        string text5 = text3;
        CS$<>8__locals1.<WriteSecrets>g__Put|0("env:" + text4, text5);
      }
      foreach (KeyValuePair<string, string> keyValuePair in s.Headers)
      {
        string text2;
        string text3;
        keyValuePair.Deconstruct(out text3, out text2);
        string text6 = text3;
        string text7 = text2;
        CS$<>8__locals1.<WriteSecrets>g__Put|0("header:" + text6, text7);
      }
    }

    // Token: 0x060008D8 RID: 2264 RVA: 0x00540BF4 File Offset: 0x00540BF4
    [return: Nullable(2)]
    public static string EnvironmentConflict(CustomConnector connector, IEnumerable<string> names, IReadOnlyList<CustomConnector> all)
    {
      if (connector.IsRemote)
      {
        return null;
      }
      Func<CustomConnector, bool> <>9__0;
      Func<CustomConnector, bool> func;
      if ((func = <>9__0) == null)
      {
        func = (<>9__0 = (CustomConnector o) => o.Id != connector.Id && !o.IsRemote);
      }
      using (IEnumerator<CustomConnector> enumerator = all.Where<CustomConnector>(func).GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          CustomConnector other = enumerator.Current;
          string text = names.FirstOrDefault<string>((string n) => other.EnvironmentNames.Contains(n));
          if (text != null)
          {
            return other.Name + " already uses " + text + ". Use different environment variable names or remove that connector first.";
          }
        }
      }
      return null;
    }

    // Token: 0x060008D9 RID: 2265 RVA: 0x00540CBC File Offset: 0x00540CBC
    [return: Nullable(2)]
    public static string Save(CustomConnector connector, CustomConnectorSecrets secrets, out bool isNew)
    {
      CustomConnectorStore.<>c__DisplayClass24_0 CS$<>8__locals1 = new CustomConnectorStore.<>c__DisplayClass24_0();
      CS$<>8__locals1.connector = connector;
      isNew = false;
      object gate = CustomConnectorStore.Gate;
      lock (gate)
      {
        List<CustomConnector> list = CustomConnectorStore.Load();
        if (list.Any<CustomConnector>((CustomConnector c) => c.Id != CS$<>8__locals1.connector.Id && string.Equals(c.Name, CS$<>8__locals1.connector.Name, StringComparison.OrdinalIgnoreCase)))
        {
          return "You already have a connector with that name.";
        }
        string text = CustomConnectorStore.EnvironmentConflict(CS$<>8__locals1.connector, secrets.Environment.Keys, list);
        if (text != null)
        {
          return text;
        }
        Uri uri;
        if (CS$<>8__locals1.connector.IsRemote && Uri.TryCreate(CS$<>8__locals1.connector.Url, UriKind.Absolute, out uri) && uri.UserInfo.Length > 0)
        {
          return "Remove credentials from the URL and use the authentication fields.";
        }
        Dictionary<string, string> dictionary = CustomConnectorStore.LoadSecrets();
        CustomConnector customConnector = list.FirstOrDefault<CustomConnector>((CustomConnector c) => c.Id == CS$<>8__locals1.connector.Id);
        isNew = customConnector == null;
        if (customConnector != null)
        {
          CustomConnectorSecrets customConnectorSecrets = CustomConnectorStore.ReadSecretsLocked(dictionary, CS$<>8__locals1.connector.Id);
          if (!CS$<>8__locals1.connector.HasSameAuthenticationTarget(customConnector))
          {
            secrets = new CustomConnectorSecrets
            {
              Environment = (CS$<>8__locals1.connector.IsRemote ? new Dictionary<string, string>() : secrets.Environment)
            };
            CustomConnectorStore.<>c__DisplayClass24_0 CS$<>8__locals2 = CS$<>8__locals1;
            CustomConnector customConnector2 = CS$<>8__locals1.connector.<Clone>$();
            customConnector2.HasApiKey = false;
            customConnector2.TokenFromSignIn = false;
            customConnector2.HeaderNames = Array.Empty<string>();
            customConnector2.BrowserSignInSupport = null;
            customConnector2.DerivedCapabilityDescription = null;
            CS$<>8__locals2.connector = customConnector2;
          }
          else
          {
            string bearerToken = secrets.BearerToken;
            if (bearerToken != null && bearerToken == customConnectorSecrets.BearerToken)
            {
              secrets.RefreshToken = customConnectorSecrets.RefreshToken;
              secrets.ExpiresAt = customConnectorSecrets.ExpiresAt;
              secrets.TokenEndpoint = customConnectorSecrets.TokenEndpoint;
              secrets.RegisteredClientId = customConnectorSecrets.RegisteredClientId;
              secrets.RegisteredClientSecret = customConnectorSecrets.RegisteredClientSecret;
              secrets.RegisteredClientRedirectUri = customConnectorSecrets.RegisteredClientRedirectUri;
              CustomConnectorStore.<>c__DisplayClass24_0 CS$<>8__locals3 = CS$<>8__locals1;
              CustomConnector customConnector3 = CS$<>8__locals1.connector.<Clone>$();
              customConnector3.TokenFromSignIn = customConnector.TokenFromSignIn;
              CS$<>8__locals3.connector = customConnector3;
            }
            string url = customConnector.Url;
            string text2 = ((url != null) ? url.Trim() : null);
            string url2 = CS$<>8__locals1.connector.Url;
            if (text2 != ((url2 != null) ? url2.Trim() : null))
            {
              CustomConnectorStore.<>c__DisplayClass24_0 CS$<>8__locals4 = CS$<>8__locals1;
              CustomConnector customConnector4 = CS$<>8__locals1.connector.<Clone>$();
              customConnector4.DerivedCapabilityDescription = null;
              customConnector4.BrowserSignInSupport = null;
              CS$<>8__locals4.connector = customConnector4;
            }
            else
            {
              CustomConnectorStore.<>c__DisplayClass24_0 CS$<>8__locals5 = CS$<>8__locals1;
              CustomConnector customConnector5 = CS$<>8__locals1.connector.<Clone>$();
              customConnector5.BrowserSignInSupport = customConnector.BrowserSignInSupport;
              CS$<>8__locals5.connector = customConnector5;
            }
          }
          CustomConnectorStore.<>c__DisplayClass24_0 CS$<>8__locals6 = CS$<>8__locals1;
          CustomConnector customConnector6 = CS$<>8__locals1.connector.<Clone>$();
          customConnector6.CreatedAt = customConnector.CreatedAt;
          CS$<>8__locals6.connector = customConnector6;
        }
        else if (CS$<>8__locals1.connector.CreatedAt == default(DateTimeOffset))
        {
          CustomConnectorStore.<>c__DisplayClass24_0 CS$<>8__locals7 = CS$<>8__locals1;
          CustomConnector customConnector7 = CS$<>8__locals1.connector.<Clone>$();
          customConnector7.CreatedAt = DateTimeOffset.Now;
          CS$<>8__locals7.connector = customConnector7;
        }
        CustomConnectorStore.<>c__DisplayClass24_0 CS$<>8__locals8 = CS$<>8__locals1;
        CustomConnector customConnector8 = CS$<>8__locals1.connector.<Clone>$();
        customConnector8.HasApiKey = !string.IsNullOrEmpty(secrets.BearerToken);
        customConnector8.EnvironmentNames = (CS$<>8__locals1.connector.IsRemote ? new List<string>() : secrets.Environment.Keys.ToList<string>());
        customConnector8.HeaderNames = (CS$<>8__locals1.connector.IsRemote ? secrets.Headers.Keys.ToList<string>() : new List<string>());
        CS$<>8__locals8.connector = customConnector8;
        if (!CS$<>8__locals1.connector.HasApiKey)
        {
          CustomConnectorStore.<>c__DisplayClass24_0 CS$<>8__locals9 = CS$<>8__locals1;
          CustomConnector customConnector9 = CS$<>8__locals1.connector.<Clone>$();
          customConnector9.TokenFromSignIn = false;
          CS$<>8__locals9.connector = customConnector9;
        }
        CustomConnectorStore.WriteSecrets(dictionary, CS$<>8__locals1.connector.Id, secrets);
        list.RemoveAll((CustomConnector c) => c.Id == CS$<>8__locals1.connector.Id);
        list.Add(CS$<>8__locals1.connector);
        CustomConnectorStore.WriteList(list);
        CustomConnectorStore.SaveSecrets(dictionary);
        string text3 = "connectors";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 6);
        defaultInterpolatedStringHandler.AppendLiteral("saved ");
        defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.connector.Name);
        defaultInterpolatedStringHandler.AppendLiteral(" (");
        defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.connector.Kind);
        defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.connector.HasApiKey ? ", bearer" : "");
        defaultInterpolatedStringHandler.AppendFormatted((CS$<>8__locals1.connector.TrimmedOAuthClientId.Length > 0) ? ", client id" : "");
        string text4;
        if (CS$<>8__locals1.connector.HeaderNames.Count <= 0)
        {
          text4 = "";
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(12, 1);
          defaultInterpolatedStringHandler2.AppendLiteral(", ");
          defaultInterpolatedStringHandler2.AppendFormatted<int>(CS$<>8__locals1.connector.HeaderNames.Count);
          defaultInterpolatedStringHandler2.AppendLiteral(" header(s)");
          text4 = defaultInterpolatedStringHandler2.ToStringAndClear();
        }
        defaultInterpolatedStringHandler.AppendFormatted(text4);
        string text5;
        if (CS$<>8__locals1.connector.EnvironmentNames.Count <= 0)
        {
          text5 = "";
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(6, 1);
          defaultInterpolatedStringHandler2.AppendLiteral(", ");
          defaultInterpolatedStringHandler2.AppendFormatted<int>(CS$<>8__locals1.connector.EnvironmentNames.Count);
          defaultInterpolatedStringHandler2.AppendLiteral(" env");
          text5 = defaultInterpolatedStringHandler2.ToStringAndClear();
        }
        defaultInterpolatedStringHandler.AppendFormatted(text5);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
      }
      Action changed = CustomConnectorStore.Changed;
      if (changed != null)
      {
        changed();
      }
      return null;
    }

    // Token: 0x060008DA RID: 2266 RVA: 0x005411F8 File Offset: 0x005411F8
    private static CustomConnectorSecrets ReadSecretsLocked(Dictionary<string, string> all, string id)
    {
      CustomConnectorSecrets customConnectorSecrets = new CustomConnectorSecrets();
      foreach (KeyValuePair<string, string> keyValuePair in all)
      {
        string text;
        string text2;
        keyValuePair.Deconstruct(out text, out text2);
        string text3 = text;
        string text4 = text2;
        if (text3.StartsWith(id + "|", StringComparison.Ordinal))
        {
          text2 = text3;
          int num = id.Length + 1;
          string text5 = text2.Substring(num, text2.Length - num);
          if (text5 != null)
          {
            num = text5.Length;
            if (num != 7)
            {
              switch (num)
              {
              case 13:
              {
                char c = text5[6];
                if (c != 'e')
                {
                  if (c == 'r')
                  {
                    if (text5 == "oauth_refresh")
                    {
                      customConnectorSecrets.RefreshToken = text4;
                      continue;
                    }
                  }
                }
                else if (text5 == "oauth_expires")
                {
                  DateTimeOffset dateTimeOffset;
                  customConnectorSecrets.ExpiresAt = (DateTimeOffset.TryParse(text4, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dateTimeOffset) ? new DateTimeOffset?(dateTimeOffset) : null);
                  continue;
                }
                break;
              }
              case 15:
                if (text5 == "oauth_client_id")
                {
                  customConnectorSecrets.RegisteredClientId = text4;
                  continue;
                }
                break;
              case 19:
                if (text5 == "oauth_client_secret")
                {
                  customConnectorSecrets.RegisteredClientSecret = text4;
                  continue;
                }
                break;
              case 20:
                if (text5 == "oauth_token_endpoint")
                {
                  customConnectorSecrets.TokenEndpoint = text4;
                  continue;
                }
                break;
              case 21:
                if (text5 == "oauth_client_redirect")
                {
                  customConnectorSecrets.RegisteredClientRedirectUri = text4;
                  continue;
                }
                break;
              }
            }
            else if (text5 == "api_key")
            {
              customConnectorSecrets.BearerToken = text4;
              continue;
            }
          }
          if (text5.StartsWith("env:", StringComparison.Ordinal))
          {
            Dictionary<string, string> environment = customConnectorSecrets.Environment;
            text2 = text5;
            environment[text2.Substring(4, text2.Length - 4)] = text4;
          }
          else if (text5.StartsWith("header:", StringComparison.Ordinal))
          {
            Dictionary<string, string> headers = customConnectorSecrets.Headers;
            text2 = text5;
            headers[text2.Substring(7, text2.Length - 7)] = text4;
          }
        }
      }
      return customConnectorSecrets;
    }

    // Token: 0x060008DB RID: 2267 RVA: 0x00541464 File Offset: 0x00541464
    [NullableContext(2)]
    public static void StoreSignInTokens([Nullable(1)] string id, [Nullable(1)] McpOAuthTokens tokens, Uri tokenEndpoint, McpOAuthClientRegistration registered, string registeredRedirectUri = null)
    {
      object gate = CustomConnectorStore.Gate;
      lock (gate)
      {
        List<CustomConnector> list = CustomConnectorStore.Load();
        int num = list.FindIndex((CustomConnector c) => c.Id == id);
        if (num < 0)
        {
          return;
        }
        Dictionary<string, string> dictionary = CustomConnectorStore.LoadSecrets();
        CustomConnectorSecrets customConnectorSecrets = CustomConnectorStore.ReadSecretsLocked(dictionary, id);
        customConnectorSecrets.BearerToken = tokens.AccessToken;
        customConnectorSecrets.RefreshToken = tokens.RefreshToken ?? customConnectorSecrets.RefreshToken;
        customConnectorSecrets.ExpiresAt = tokens.ExpiresAt;
        if (tokenEndpoint != null)
        {
          customConnectorSecrets.TokenEndpoint = tokenEndpoint.AbsoluteUri;
        }
        if (registered != null)
        {
          customConnectorSecrets.RegisteredClientId = registered.ClientId;
          customConnectorSecrets.RegisteredClientSecret = registered.ClientSecret;
          customConnectorSecrets.RegisteredClientRedirectUri = registeredRedirectUri;
        }
        CustomConnectorStore.WriteSecrets(dictionary, id, customConnectorSecrets);
        List<CustomConnector> list2 = list;
        int num2 = num;
        CustomConnector customConnector = list[num].<Clone>$();
        customConnector.HasApiKey = true;
        customConnector.TokenFromSignIn = true;
        customConnector.BrowserSignInSupport = "supported";
        list2[num2] = customConnector;
        CustomConnectorStore.WriteList(list);
        CustomConnectorStore.SaveSecrets(dictionary);
      }
      string text = "connectors";
      string text2 = "sign-in token stored for ";
      string id2 = id;
      DateTimeOffset? expiresAt = tokens.ExpiresAt;
      string text3;
      if (expiresAt != null)
      {
        DateTimeOffset valueOrDefault = expiresAt.GetValueOrDefault();
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 1);
        defaultInterpolatedStringHandler.AppendLiteral(" (expires ");
        defaultInterpolatedStringHandler.AppendFormatted<DateTimeOffset>(valueOrDefault, "u");
        defaultInterpolatedStringHandler.AppendLiteral(")");
        text3 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text3 = "";
      }
      Log.Info(text, text2 + id2 + text3);
      Action changed = CustomConnectorStore.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x060008DC RID: 2268 RVA: 0x00541618 File Offset: 0x00541618
    public static void UpdateQuietly(string id, Func<CustomConnector, CustomConnector> mutate)
    {
      bool flag = false;
      object gate = CustomConnectorStore.Gate;
      lock (gate)
      {
        List<CustomConnector> list = CustomConnectorStore.Load();
        int num = list.FindIndex((CustomConnector c) => c.Id == id);
        if (num < 0)
        {
          return;
        }
        CustomConnector customConnector = mutate(list[num]);
        if (customConnector != list[num])
        {
          list[num] = customConnector;
          CustomConnectorStore.WriteList(list);
          flag = true;
        }
      }
      if (flag)
      {
        Action updated = CustomConnectorStore.Updated;
        if (updated == null)
        {
          return;
        }
        updated();
      }
    }

    // Token: 0x060008DD RID: 2269 RVA: 0x005416CC File Offset: 0x005416CC
    public static void Remove(string id)
    {
      object gate = CustomConnectorStore.Gate;
      lock (gate)
      {
        List<CustomConnector> list = CustomConnectorStore.Load();
        list.RemoveAll((CustomConnector c) => c.Id == id);
        CustomConnectorStore.WriteList(list);
        Dictionary<string, string> dictionary = CustomConnectorStore.LoadSecrets();
        IEnumerable<string> keys = dictionary.Keys;
        Func<string, bool> <>9__1;
        Func<string, bool> func;
        if ((func = <>9__1) == null)
        {
          func = (<>9__1 = (string k) => k.StartsWith(id + "|", StringComparison.Ordinal));
        }
        foreach (string text in keys.Where<string>(func).ToList<string>())
        {
          dictionary.Remove(text);
        }
        CustomConnectorStore.SaveSecrets(dictionary);
      }
      Log.Info("connectors", "removed " + id);
      Action changed = CustomConnectorStore.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x060008DE RID: 2270 RVA: 0x005417D8 File Offset: 0x005417D8
    public static IReadOnlyList<CustomConnectorRegistration> Registrations()
    {
      List<CustomConnector> list = CustomConnectorStore.Load();
      object gate = CustomConnectorStore.Gate;
      Dictionary<string, string> dictionary;
      lock (gate)
      {
        dictionary = CustomConnectorStore.LoadSecrets();
      }
      List<CustomConnectorRegistration> list2 = new List<CustomConnectorRegistration>();
      foreach (CustomConnector customConnector in list.OrderBy<CustomConnector, DateTimeOffset>((CustomConnector c) => c.CreatedAt))
      {
        Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
        string text = "HEYCLICKY_" + customConnector.ServerKey.ToUpperInvariant();
        CustomConnectorTransport customConnectorTransport;
        if (customConnector.IsRemote)
        {
          string url = customConnector.Url;
          Uri uri;
          bool flag = !Uri.TryCreate((url != null) ? url.Trim() : null, UriKind.Absolute, out uri);
          if (!flag)
          {
            string scheme = uri.Scheme;
            bool flag2 = scheme == "https" || scheme == "http";
            flag = !flag2;
          }
          if (flag || uri.UserInfo.Length > 0)
          {
            continue;
          }
          string text2 = null;
          string text3;
          if (customConnector.HasApiKey && dictionary.TryGetValue(customConnector.Id + "|api_key", out text3) && text3.Length > 0)
          {
            text2 = text + "_TOKEN";
            dictionary2[text2] = text3;
          }
          Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
          for (int i = 0; i < customConnector.HeaderNames.Count; i++)
          {
            string text4;
            if (dictionary.TryGetValue(customConnector.Id + "|header:" + customConnector.HeaderNames[i], out text4) && text4.Length != 0)
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 2);
              defaultInterpolatedStringHandler.AppendFormatted(text);
              defaultInterpolatedStringHandler.AppendLiteral("_HEADER_");
              defaultInterpolatedStringHandler.AppendFormatted<int>(i);
              string text5 = defaultInterpolatedStringHandler.ToStringAndClear();
              dictionary3[customConnector.HeaderNames[i]] = text5;
              dictionary2[text5] = text4;
            }
          }
          customConnectorTransport = new CustomConnectorTransport.Remote(customConnector.Url.Trim(), text2, dictionary3);
        }
        else
        {
          if (CustomConnectorStore.EnvironmentConflict(customConnector, customConnector.EnvironmentNames, list) != null)
          {
            continue;
          }
          List<string> list3 = new List<string>();
          foreach (string text6 in customConnector.EnvironmentNames)
          {
            string text7;
            if (dictionary.TryGetValue(customConnector.Id + "|env:" + text6, out text7) && text7.Length > 0)
            {
              dictionary2[text6] = text7;
              list3.Add(text6);
            }
          }
          customConnectorTransport = new CustomConnectorTransport.Stdio(CommandResolver.Resolve(customConnector.Command ?? ""), customConnector.Arguments, list3, CommandResolver.SearchPath());
        }
        string effectiveCapabilityDescription = customConnector.EffectiveCapabilityDescription;
        list2.Add(new CustomConnectorRegistration(customConnector.ServerKey, customConnector.Name, effectiveCapabilityDescription, customConnectorTransport, dictionary2));
      }
      return list2;
    }

    // Token: 0x04000309 RID: 777
    private static readonly byte[] Entropy = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.68D84DC8D62FCC756B007A43E131C71622669E5D89442408F21F73500F532933), 23).ToArray();

    // Token: 0x0400030A RID: 778
    private static readonly object Gate = new object();

    // Token: 0x02000493 RID: 1171
    [Nullable(0)]
    [JsonSerializable(typeof(List<CustomConnector>))]
    [JsonSerializable(typeof(Dictionary<string, string>))]
    [GeneratedCode("System.Text.Json.SourceGeneration", "9.0.15.2218")]
    private sealed class ConnectorJsonContext : JsonSerializerContext, IJsonTypeInfoResolver
    {
      // Token: 0x1700070A RID: 1802
      // (get) Token: 0x06002AE0 RID: 10976 RVA: 0x006515E0 File Offset: 0x006515E0
      [Nullable(0)]
      public JsonTypeInfo<bool> Boolean
      {
        [NullableContext(0)]
        get
        {
          JsonTypeInfo<bool> jsonTypeInfo;
          if ((jsonTypeInfo = this._Boolean) == null)
          {
            jsonTypeInfo = (this._Boolean = (JsonTypeInfo<bool>)base.Options.GetTypeInfo(typeof(bool)));
          }
          return jsonTypeInfo;
        }
      }

      // Token: 0x06002AE1 RID: 10977 RVA: 0x0065161C File Offset: 0x0065161C
      private JsonTypeInfo<bool> Create_Boolean(JsonSerializerOptions options)
      {
        JsonTypeInfo<bool> jsonTypeInfo;
        if (!CustomConnectorStore.ConnectorJsonContext.TryGetTypeInfoForRuntimeCustomConverter<bool>(options, out jsonTypeInfo))
        {
          jsonTypeInfo = JsonMetadataServices.CreateValueInfo<bool>(options, JsonMetadataServices.BooleanConverter);
        }
        jsonTypeInfo.OriginatingResolver = this;
        return jsonTypeInfo;
      }

      // Token: 0x1700070B RID: 1803
      // (get) Token: 0x06002AE2 RID: 10978 RVA: 0x00651648 File Offset: 0x00651648
      [Nullable(0)]
      public JsonTypeInfo<CustomConnector> CustomConnector
      {
        [NullableContext(0)]
        get
        {
          JsonTypeInfo<CustomConnector> jsonTypeInfo;
          if ((jsonTypeInfo = this._CustomConnector) == null)
          {
            jsonTypeInfo = (this._CustomConnector = (JsonTypeInfo<CustomConnector>)base.Options.GetTypeInfo(typeof(CustomConnector)));
          }
          return jsonTypeInfo;
        }
      }

      // Token: 0x06002AE3 RID: 10979 RVA: 0x00651684 File Offset: 0x00651684
      private JsonTypeInfo<CustomConnector> Create_CustomConnector(JsonSerializerOptions options)
      {
        JsonTypeInfo<CustomConnector> jsonTypeInfo;
        if (!CustomConnectorStore.ConnectorJsonContext.TryGetTypeInfoForRuntimeCustomConverter<CustomConnector>(options, out jsonTypeInfo))
        {
          JsonObjectInfoValues<CustomConnector> jsonObjectInfoValues = new JsonObjectInfoValues<CustomConnector>();
          jsonObjectInfoValues.ObjectCreator = null;
          jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new CustomConnector((string)args[0], (string)args[1], (string)args[2], (string)args[3], (string)args[4], (IReadOnlyList<string>)args[5], (IReadOnlyList<string>)args[6], (bool)args[7])
          {
            OAuthClientId = (string)args[8],
            HeaderNames = (IReadOnlyList<string>)args[9],
            TokenFromSignIn = (bool)args[10],
            DerivedCapabilityDescription = (string)args[11],
            BrowserSignInSupport = (string)args[12],
            CreatedAt = (DateTimeOffset)args[13]
          };
          jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CustomConnectorStore.ConnectorJsonContext.CustomConnectorPropInit(options);
          Func<JsonParameterInfoValues[]> func;
          if ((func = CustomConnectorStore.ConnectorJsonContext.<>O.<0>__CustomConnectorCtorParamInit) == null)
          {
            func = (CustomConnectorStore.ConnectorJsonContext.<>O.<0>__CustomConnectorCtorParamInit = new Func<JsonParameterInfoValues[]>(CustomConnectorStore.ConnectorJsonContext.CustomConnectorCtorParamInit));
          }
          jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
          jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(CustomConnector).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
          {
            typeof(string),
            typeof(string),
            typeof(string),
            typeof(string),
            typeof(string),
            typeof(IReadOnlyList<string>),
            typeof(IReadOnlyList<string>),
            typeof(bool)
          }, null);
          jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, CustomConnector>(this.CustomConnectorSerializeHandler);
          JsonObjectInfoValues<CustomConnector> jsonObjectInfoValues2 = jsonObjectInfoValues;
          jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<CustomConnector>(options, jsonObjectInfoValues2);
          jsonTypeInfo.NumberHandling = null;
        }
        jsonTypeInfo.OriginatingResolver = this;
        return jsonTypeInfo;
      }

      // Token: 0x06002AE4 RID: 10980 RVA: 0x00651770 File Offset: 0x00651770
      private static JsonPropertyInfo[] CustomConnectorPropInit(JsonSerializerOptions options)
      {
        JsonPropertyInfo[] array = new JsonPropertyInfo[23];
        JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues.IsProperty = true;
        jsonPropertyInfoValues.IsPublic = true;
        jsonPropertyInfoValues.IsVirtual = false;
        jsonPropertyInfoValues.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues.Converter = null;
        jsonPropertyInfoValues.Getter = (object obj) => ((CustomConnector)obj).Id;
        jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
        {
          throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
        };
        jsonPropertyInfoValues.IgnoreCondition = null;
        jsonPropertyInfoValues.HasJsonInclude = false;
        jsonPropertyInfoValues.IsExtensionData = false;
        jsonPropertyInfoValues.NumberHandling = null;
        jsonPropertyInfoValues.PropertyName = "Id";
        jsonPropertyInfoValues.JsonPropertyName = null;
        jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
        array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
        array[0].IsGetNullable = false;
        array[0].IsSetNullable = false;
        JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues3.IsProperty = true;
        jsonPropertyInfoValues3.IsPublic = true;
        jsonPropertyInfoValues3.IsVirtual = false;
        jsonPropertyInfoValues3.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues3.Converter = null;
        jsonPropertyInfoValues3.Getter = (object obj) => ((CustomConnector)obj).Name;
        jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
        {
          throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
        };
        jsonPropertyInfoValues3.IgnoreCondition = null;
        jsonPropertyInfoValues3.HasJsonInclude = false;
        jsonPropertyInfoValues3.IsExtensionData = false;
        jsonPropertyInfoValues3.NumberHandling = null;
        jsonPropertyInfoValues3.PropertyName = "Name";
        jsonPropertyInfoValues3.JsonPropertyName = null;
        jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("Name", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
        array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
        array[1].IsGetNullable = false;
        array[1].IsSetNullable = false;
        JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues5.IsProperty = true;
        jsonPropertyInfoValues5.IsPublic = true;
        jsonPropertyInfoValues5.IsVirtual = false;
        jsonPropertyInfoValues5.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues5.Converter = null;
        jsonPropertyInfoValues5.Getter = (object obj) => ((CustomConnector)obj).Kind;
        jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
        {
          throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
        };
        jsonPropertyInfoValues5.IgnoreCondition = null;
        jsonPropertyInfoValues5.HasJsonInclude = false;
        jsonPropertyInfoValues5.IsExtensionData = false;
        jsonPropertyInfoValues5.NumberHandling = null;
        jsonPropertyInfoValues5.PropertyName = "Kind";
        jsonPropertyInfoValues5.JsonPropertyName = null;
        jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("Kind", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
        array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
        array[2].IsGetNullable = false;
        array[2].IsSetNullable = false;
        JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues7.IsProperty = true;
        jsonPropertyInfoValues7.IsPublic = true;
        jsonPropertyInfoValues7.IsVirtual = false;
        jsonPropertyInfoValues7.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues7.Converter = null;
        jsonPropertyInfoValues7.Getter = (object obj) => ((CustomConnector)obj).Url;
        jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
        {
          throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
        };
        jsonPropertyInfoValues7.IgnoreCondition = null;
        jsonPropertyInfoValues7.HasJsonInclude = false;
        jsonPropertyInfoValues7.IsExtensionData = false;
        jsonPropertyInfoValues7.NumberHandling = null;
        jsonPropertyInfoValues7.PropertyName = "Url";
        jsonPropertyInfoValues7.JsonPropertyName = null;
        jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("Url", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
        array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues9.IsProperty = true;
        jsonPropertyInfoValues9.IsPublic = true;
        jsonPropertyInfoValues9.IsVirtual = false;
        jsonPropertyInfoValues9.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues9.Converter = null;
        jsonPropertyInfoValues9.Getter = (object obj) => ((CustomConnector)obj).Command;
        jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
        {
          throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
        };
        jsonPropertyInfoValues9.IgnoreCondition = null;
        jsonPropertyInfoValues9.HasJsonInclude = false;
        jsonPropertyInfoValues9.IsExtensionData = false;
        jsonPropertyInfoValues9.NumberHandling = null;
        jsonPropertyInfoValues9.PropertyName = "Command";
        jsonPropertyInfoValues9.JsonPropertyName = null;
        jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("Command", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
        array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
        JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
        jsonPropertyInfoValues11.IsProperty = true;
        jsonPropertyInfoValues11.IsPublic = true;
        jsonPropertyInfoValues11.IsVirtual = false;
        jsonPropertyInfoValues11.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues11.Converter = null;
        jsonPropertyInfoValues11.Getter = (object obj) => ((CustomConnector)obj).Arguments;
        jsonPropertyInfoValues11.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
        {
          throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
        };
        jsonPropertyInfoValues11.IgnoreCondition = null;
        jsonPropertyInfoValues11.HasJsonInclude = false;
        jsonPropertyInfoValues11.IsExtensionData = false;
        jsonPropertyInfoValues11.NumberHandling = null;
        jsonPropertyInfoValues11.PropertyName = "Arguments";
        jsonPropertyInfoValues11.JsonPropertyName = null;
        jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("Arguments", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
        array[5] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues12);
        array[5].IsGetNullable = false;
        array[5].IsSetNullable = false;
        JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
        jsonPropertyInfoValues13.IsProperty = true;
        jsonPropertyInfoValues13.IsPublic = true;
        jsonPropertyInfoValues13.IsVirtual = false;
        jsonPropertyInfoValues13.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues13.Converter = null;
        jsonPropertyInfoValues13.Getter = (object obj) => ((CustomConnector)obj).EnvironmentNames;
        jsonPropertyInfoValues13.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
        {
          throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
        };
        jsonPropertyInfoValues13.IgnoreCondition = null;
        jsonPropertyInfoValues13.HasJsonInclude = false;
        jsonPropertyInfoValues13.IsExtensionData = false;
        jsonPropertyInfoValues13.NumberHandling = null;
        jsonPropertyInfoValues13.PropertyName = "EnvironmentNames";
        jsonPropertyInfoValues13.JsonPropertyName = null;
        jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("EnvironmentNames", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
        array[6] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues14);
        array[6].IsGetNullable = false;
        array[6].IsSetNullable = false;
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<bool>();
        jsonPropertyInfoValues15.IsProperty = true;
        jsonPropertyInfoValues15.IsPublic = true;
        jsonPropertyInfoValues15.IsVirtual = false;
        jsonPropertyInfoValues15.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues15.Converter = null;
        jsonPropertyInfoValues15.Getter = (object obj) => ((CustomConnector)obj).HasApiKey;
        jsonPropertyInfoValues15.Setter = delegate(object obj, bool value)
        {
          throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
        };
        jsonPropertyInfoValues15.IgnoreCondition = null;
        jsonPropertyInfoValues15.HasJsonInclude = false;
        jsonPropertyInfoValues15.IsExtensionData = false;
        jsonPropertyInfoValues15.NumberHandling = null;
        jsonPropertyInfoValues15.PropertyName = "HasApiKey";
        jsonPropertyInfoValues15.JsonPropertyName = null;
        jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("HasApiKey", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
        array[7] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues16);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues17 = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues17.IsProperty = true;
        jsonPropertyInfoValues17.IsPublic = true;
        jsonPropertyInfoValues17.IsVirtual = false;
        jsonPropertyInfoValues17.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues17.Converter = null;
        jsonPropertyInfoValues17.Getter = (object obj) => ((CustomConnector)obj).OAuthClientId;
        jsonPropertyInfoValues17.Setter = delegate(object obj, [Nullable(2)] string value)
        {
          throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
        };
        jsonPropertyInfoValues17.IgnoreCondition = null;
        jsonPropertyInfoValues17.HasJsonInclude = false;
        jsonPropertyInfoValues17.IsExtensionData = false;
        jsonPropertyInfoValues17.NumberHandling = null;
        jsonPropertyInfoValues17.PropertyName = "OAuthClientId";
        jsonPropertyInfoValues17.JsonPropertyName = null;
        jsonPropertyInfoValues17.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("OAuthClientId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues18 = jsonPropertyInfoValues17;
        array[8] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues18);
        JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues19 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
        jsonPropertyInfoValues19.IsProperty = true;
        jsonPropertyInfoValues19.IsPublic = true;
        jsonPropertyInfoValues19.IsVirtual = false;
        jsonPropertyInfoValues19.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues19.Converter = null;
        jsonPropertyInfoValues19.Getter = (object obj) => ((CustomConnector)obj).HeaderNames;
        jsonPropertyInfoValues19.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
        {
          throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
        };
        jsonPropertyInfoValues19.IgnoreCondition = null;
        jsonPropertyInfoValues19.HasJsonInclude = false;
        jsonPropertyInfoValues19.IsExtensionData = false;
        jsonPropertyInfoValues19.NumberHandling = null;
        jsonPropertyInfoValues19.PropertyName = "HeaderNames";
        jsonPropertyInfoValues19.JsonPropertyName = null;
        jsonPropertyInfoValues19.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("HeaderNames", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues20 = jsonPropertyInfoValues19;
        array[9] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues20);
        array[9].IsGetNullable = false;
        array[9].IsSetNullable = false;
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues21 = new JsonPropertyInfoValues<bool>();
        jsonPropertyInfoValues21.IsProperty = true;
        jsonPropertyInfoValues21.IsPublic = true;
        jsonPropertyInfoValues21.IsVirtual = false;
        jsonPropertyInfoValues21.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues21.Converter = null;
        jsonPropertyInfoValues21.Getter = (object obj) => ((CustomConnector)obj).TokenFromSignIn;
        jsonPropertyInfoValues21.Setter = delegate(object obj, bool value)
        {
          throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
        };
        jsonPropertyInfoValues21.IgnoreCondition = null;
        jsonPropertyInfoValues21.HasJsonInclude = false;
        jsonPropertyInfoValues21.IsExtensionData = false;
        jsonPropertyInfoValues21.NumberHandling = null;
        jsonPropertyInfoValues21.PropertyName = "TokenFromSignIn";
        jsonPropertyInfoValues21.JsonPropertyName = null;
        jsonPropertyInfoValues21.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("TokenFromSignIn", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues22 = jsonPropertyInfoValues21;
        array[10] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues22);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues23 = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues23.IsProperty = true;
        jsonPropertyInfoValues23.IsPublic = true;
        jsonPropertyInfoValues23.IsVirtual = false;
        jsonPropertyInfoValues23.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues23.Converter = null;
        jsonPropertyInfoValues23.Getter = (object obj) => ((CustomConnector)obj).DerivedCapabilityDescription;
        jsonPropertyInfoValues23.Setter = delegate(object obj, [Nullable(2)] string value)
        {
          throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
        };
        jsonPropertyInfoValues23.IgnoreCondition = null;
        jsonPropertyInfoValues23.HasJsonInclude = false;
        jsonPropertyInfoValues23.IsExtensionData = false;
        jsonPropertyInfoValues23.NumberHandling = null;
        jsonPropertyInfoValues23.PropertyName = "DerivedCapabilityDescription";
        jsonPropertyInfoValues23.JsonPropertyName = null;
        jsonPropertyInfoValues23.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("DerivedCapabilityDescription", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues24 = jsonPropertyInfoValues23;
        array[11] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues24);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues25 = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues25.IsProperty = true;
        jsonPropertyInfoValues25.IsPublic = true;
        jsonPropertyInfoValues25.IsVirtual = false;
        jsonPropertyInfoValues25.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues25.Converter = null;
        jsonPropertyInfoValues25.Getter = (object obj) => ((CustomConnector)obj).BrowserSignInSupport;
        jsonPropertyInfoValues25.Setter = delegate(object obj, [Nullable(2)] string value)
        {
          throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
        };
        jsonPropertyInfoValues25.IgnoreCondition = null;
        jsonPropertyInfoValues25.HasJsonInclude = false;
        jsonPropertyInfoValues25.IsExtensionData = false;
        jsonPropertyInfoValues25.NumberHandling = null;
        jsonPropertyInfoValues25.PropertyName = "BrowserSignInSupport";
        jsonPropertyInfoValues25.JsonPropertyName = null;
        jsonPropertyInfoValues25.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("BrowserSignInSupport", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues26 = jsonPropertyInfoValues25;
        array[12] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues26);
        JsonPropertyInfoValues<DateTimeOffset> jsonPropertyInfoValues27 = new JsonPropertyInfoValues<DateTimeOffset>();
        jsonPropertyInfoValues27.IsProperty = true;
        jsonPropertyInfoValues27.IsPublic = true;
        jsonPropertyInfoValues27.IsVirtual = false;
        jsonPropertyInfoValues27.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues27.Converter = null;
        jsonPropertyInfoValues27.Getter = (object obj) => ((CustomConnector)obj).CreatedAt;
        jsonPropertyInfoValues27.Setter = delegate(object obj, DateTimeOffset value)
        {
          throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
        };
        jsonPropertyInfoValues27.IgnoreCondition = null;
        jsonPropertyInfoValues27.HasJsonInclude = false;
        jsonPropertyInfoValues27.IsExtensionData = false;
        jsonPropertyInfoValues27.NumberHandling = null;
        jsonPropertyInfoValues27.PropertyName = "CreatedAt";
        jsonPropertyInfoValues27.JsonPropertyName = null;
        jsonPropertyInfoValues27.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("CreatedAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<DateTimeOffset> jsonPropertyInfoValues28 = jsonPropertyInfoValues27;
        array[13] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset>(options, jsonPropertyInfoValues28);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues29 = new JsonPropertyInfoValues<bool>();
        jsonPropertyInfoValues29.IsProperty = true;
        jsonPropertyInfoValues29.IsPublic = true;
        jsonPropertyInfoValues29.IsVirtual = false;
        jsonPropertyInfoValues29.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues29.Converter = null;
        jsonPropertyInfoValues29.Getter = null;
        jsonPropertyInfoValues29.Setter = null;
        jsonPropertyInfoValues29.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
        jsonPropertyInfoValues29.HasJsonInclude = false;
        jsonPropertyInfoValues29.IsExtensionData = false;
        jsonPropertyInfoValues29.NumberHandling = null;
        jsonPropertyInfoValues29.PropertyName = "IsRemote";
        jsonPropertyInfoValues29.JsonPropertyName = null;
        jsonPropertyInfoValues29.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("IsRemote", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues30 = jsonPropertyInfoValues29;
        array[14] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues30);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues31 = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues31.IsProperty = true;
        jsonPropertyInfoValues31.IsPublic = true;
        jsonPropertyInfoValues31.IsVirtual = false;
        jsonPropertyInfoValues31.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues31.Converter = null;
        jsonPropertyInfoValues31.Getter = null;
        jsonPropertyInfoValues31.Setter = null;
        jsonPropertyInfoValues31.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
        jsonPropertyInfoValues31.HasJsonInclude = false;
        jsonPropertyInfoValues31.IsExtensionData = false;
        jsonPropertyInfoValues31.NumberHandling = null;
        jsonPropertyInfoValues31.PropertyName = "ServerKey";
        jsonPropertyInfoValues31.JsonPropertyName = null;
        jsonPropertyInfoValues31.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("ServerKey", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues32 = jsonPropertyInfoValues31;
        array[15] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues32);
        array[15].IsGetNullable = false;
        JsonPropertyInfoValues<string> jsonPropertyInfoValues33 = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues33.IsProperty = true;
        jsonPropertyInfoValues33.IsPublic = true;
        jsonPropertyInfoValues33.IsVirtual = false;
        jsonPropertyInfoValues33.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues33.Converter = null;
        jsonPropertyInfoValues33.Getter = null;
        jsonPropertyInfoValues33.Setter = null;
        jsonPropertyInfoValues33.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
        jsonPropertyInfoValues33.HasJsonInclude = false;
        jsonPropertyInfoValues33.IsExtensionData = false;
        jsonPropertyInfoValues33.NumberHandling = null;
        jsonPropertyInfoValues33.PropertyName = "IntegrationId";
        jsonPropertyInfoValues33.JsonPropertyName = null;
        jsonPropertyInfoValues33.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("IntegrationId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues34 = jsonPropertyInfoValues33;
        array[16] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues34);
        array[16].IsGetNullable = false;
        JsonPropertyInfoValues<string> jsonPropertyInfoValues35 = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues35.IsProperty = true;
        jsonPropertyInfoValues35.IsPublic = true;
        jsonPropertyInfoValues35.IsVirtual = false;
        jsonPropertyInfoValues35.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues35.Converter = null;
        jsonPropertyInfoValues35.Getter = null;
        jsonPropertyInfoValues35.Setter = null;
        jsonPropertyInfoValues35.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
        jsonPropertyInfoValues35.HasJsonInclude = false;
        jsonPropertyInfoValues35.IsExtensionData = false;
        jsonPropertyInfoValues35.NumberHandling = null;
        jsonPropertyInfoValues35.PropertyName = "TrimmedOAuthClientId";
        jsonPropertyInfoValues35.JsonPropertyName = null;
        jsonPropertyInfoValues35.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("TrimmedOAuthClientId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues36 = jsonPropertyInfoValues35;
        array[17] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues36);
        array[17].IsGetNullable = false;
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues37 = new JsonPropertyInfoValues<bool>();
        jsonPropertyInfoValues37.IsProperty = true;
        jsonPropertyInfoValues37.IsPublic = true;
        jsonPropertyInfoValues37.IsVirtual = false;
        jsonPropertyInfoValues37.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues37.Converter = null;
        jsonPropertyInfoValues37.Getter = null;
        jsonPropertyInfoValues37.Setter = null;
        jsonPropertyInfoValues37.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
        jsonPropertyInfoValues37.HasJsonInclude = false;
        jsonPropertyInfoValues37.IsExtensionData = false;
        jsonPropertyInfoValues37.NumberHandling = null;
        jsonPropertyInfoValues37.PropertyName = "UsesBrowserSignIn";
        jsonPropertyInfoValues37.JsonPropertyName = null;
        jsonPropertyInfoValues37.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("UsesBrowserSignIn", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues38 = jsonPropertyInfoValues37;
        array[18] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues38);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues39 = new JsonPropertyInfoValues<bool>();
        jsonPropertyInfoValues39.IsProperty = true;
        jsonPropertyInfoValues39.IsPublic = true;
        jsonPropertyInfoValues39.IsVirtual = false;
        jsonPropertyInfoValues39.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues39.Converter = null;
        jsonPropertyInfoValues39.Getter = null;
        jsonPropertyInfoValues39.Setter = null;
        jsonPropertyInfoValues39.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
        jsonPropertyInfoValues39.HasJsonInclude = false;
        jsonPropertyInfoValues39.IsExtensionData = false;
        jsonPropertyInfoValues39.NumberHandling = null;
        jsonPropertyInfoValues39.PropertyName = "UsesPlainBearerToken";
        jsonPropertyInfoValues39.JsonPropertyName = null;
        jsonPropertyInfoValues39.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("UsesPlainBearerToken", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues40 = jsonPropertyInfoValues39;
        array[19] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues40);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues41 = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues41.IsProperty = true;
        jsonPropertyInfoValues41.IsPublic = true;
        jsonPropertyInfoValues41.IsVirtual = false;
        jsonPropertyInfoValues41.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues41.Converter = null;
        jsonPropertyInfoValues41.Getter = null;
        jsonPropertyInfoValues41.Setter = null;
        jsonPropertyInfoValues41.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
        jsonPropertyInfoValues41.HasJsonInclude = false;
        jsonPropertyInfoValues41.IsExtensionData = false;
        jsonPropertyInfoValues41.NumberHandling = null;
        jsonPropertyInfoValues41.PropertyName = "LocationSummary";
        jsonPropertyInfoValues41.JsonPropertyName = null;
        jsonPropertyInfoValues41.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("LocationSummary", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues42 = jsonPropertyInfoValues41;
        array[20] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues42);
        array[20].IsGetNullable = false;
        JsonPropertyInfoValues<string> jsonPropertyInfoValues43 = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues43.IsProperty = true;
        jsonPropertyInfoValues43.IsPublic = true;
        jsonPropertyInfoValues43.IsVirtual = false;
        jsonPropertyInfoValues43.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues43.Converter = null;
        jsonPropertyInfoValues43.Getter = null;
        jsonPropertyInfoValues43.Setter = null;
        jsonPropertyInfoValues43.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
        jsonPropertyInfoValues43.HasJsonInclude = false;
        jsonPropertyInfoValues43.IsExtensionData = false;
        jsonPropertyInfoValues43.NumberHandling = null;
        jsonPropertyInfoValues43.PropertyName = "Summary";
        jsonPropertyInfoValues43.JsonPropertyName = null;
        jsonPropertyInfoValues43.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("Summary", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues44 = jsonPropertyInfoValues43;
        array[21] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues44);
        array[21].IsGetNullable = false;
        JsonPropertyInfoValues<string> jsonPropertyInfoValues45 = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues45.IsProperty = true;
        jsonPropertyInfoValues45.IsPublic = true;
        jsonPropertyInfoValues45.IsVirtual = false;
        jsonPropertyInfoValues45.DeclaringType = typeof(CustomConnector);
        jsonPropertyInfoValues45.Converter = null;
        jsonPropertyInfoValues45.Getter = null;
        jsonPropertyInfoValues45.Setter = null;
        jsonPropertyInfoValues45.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
        jsonPropertyInfoValues45.HasJsonInclude = false;
        jsonPropertyInfoValues45.IsExtensionData = false;
        jsonPropertyInfoValues45.NumberHandling = null;
        jsonPropertyInfoValues45.PropertyName = "EffectiveCapabilityDescription";
        jsonPropertyInfoValues45.JsonPropertyName = null;
        jsonPropertyInfoValues45.AttributeProviderFactory = () => typeof(CustomConnector).GetProperty("EffectiveCapabilityDescription", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues46 = jsonPropertyInfoValues45;
        array[22] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues46);
        array[22].IsGetNullable = false;
        return array;
      }

      // Token: 0x06002AE5 RID: 10981 RVA: 0x00652B40 File Offset: 0x00652B40
      private void CustomConnectorSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] CustomConnector value)
      {
        if (value == null)
        {
          writer.WriteNullValue();
          return;
        }
        writer.WriteStartObject();
        writer.WriteString(CustomConnectorStore.ConnectorJsonContext.PropName_Id, value.Id);
        writer.WriteString(CustomConnectorStore.ConnectorJsonContext.PropName_Name, value.Name);
        writer.WriteString(CustomConnectorStore.ConnectorJsonContext.PropName_Kind, value.Kind);
        writer.WriteString(CustomConnectorStore.ConnectorJsonContext.PropName_Url, value.Url);
        writer.WriteString(CustomConnectorStore.ConnectorJsonContext.PropName_Command, value.Command);
        writer.WritePropertyName(CustomConnectorStore.ConnectorJsonContext.PropName_Arguments);
        this.IReadOnlyListStringSerializeHandler(writer, value.Arguments);
        writer.WritePropertyName(CustomConnectorStore.ConnectorJsonContext.PropName_EnvironmentNames);
        this.IReadOnlyListStringSerializeHandler(writer, value.EnvironmentNames);
        writer.WriteBoolean(CustomConnectorStore.ConnectorJsonContext.PropName_HasApiKey, value.HasApiKey);
        writer.WriteString(CustomConnectorStore.ConnectorJsonContext.PropName_OAuthClientId, value.OAuthClientId);
        writer.WritePropertyName(CustomConnectorStore.ConnectorJsonContext.PropName_HeaderNames);
        this.IReadOnlyListStringSerializeHandler(writer, value.HeaderNames);
        writer.WriteBoolean(CustomConnectorStore.ConnectorJsonContext.PropName_TokenFromSignIn, value.TokenFromSignIn);
        writer.WriteString(CustomConnectorStore.ConnectorJsonContext.PropName_DerivedCapabilityDescription, value.DerivedCapabilityDescription);
        writer.WriteString(CustomConnectorStore.ConnectorJsonContext.PropName_BrowserSignInSupport, value.BrowserSignInSupport);
        writer.WriteString(CustomConnectorStore.ConnectorJsonContext.PropName_CreatedAt, value.CreatedAt);
        writer.WriteEndObject();
      }

      // Token: 0x06002AE6 RID: 10982 RVA: 0x00652C68 File Offset: 0x00652C68
      private static JsonParameterInfoValues[] CustomConnectorCtorParamInit()
      {
        return new JsonParameterInfoValues[]
        {
          new JsonParameterInfoValues
          {
            Name = "Id",
            ParameterType = typeof(string),
            Position = 0,
            HasDefaultValue = false,
            DefaultValue = null,
            IsNullable = false
          },
          new JsonParameterInfoValues
          {
            Name = "Name",
            ParameterType = typeof(string),
            Position = 1,
            HasDefaultValue = false,
            DefaultValue = null,
            IsNullable = false
          },
          new JsonParameterInfoValues
          {
            Name = "Kind",
            ParameterType = typeof(string),
            Position = 2,
            HasDefaultValue = false,
            DefaultValue = null,
            IsNullable = false
          },
          new JsonParameterInfoValues
          {
            Name = "Url",
            ParameterType = typeof(string),
            Position = 3,
            HasDefaultValue = false,
            DefaultValue = null,
            IsNullable = true
          },
          new JsonParameterInfoValues
          {
            Name = "Command",
            ParameterType = typeof(string),
            Position = 4,
            HasDefaultValue = false,
            DefaultValue = null,
            IsNullable = true
          },
          new JsonParameterInfoValues
          {
            Name = "Arguments",
            ParameterType = typeof(IReadOnlyList<string>),
            Position = 5,
            HasDefaultValue = false,
            DefaultValue = null,
            IsNullable = false
          },
          new JsonParameterInfoValues
          {
            Name = "EnvironmentNames",
            ParameterType = typeof(IReadOnlyList<string>),
            Position = 6,
            HasDefaultValue = false,
            DefaultValue = null,
            IsNullable = false
          },
          new JsonParameterInfoValues
          {
            Name = "HasApiKey",
            ParameterType = typeof(bool),
            Position = 7,
            HasDefaultValue = false,
            DefaultValue = null,
            IsNullable = false
          },
          new JsonParameterInfoValues
          {
            Name = "OAuthClientId",
            ParameterType = typeof(string),
            Position = 8,
            IsNullable = true,
            IsMemberInitializer = true
          },
          new JsonParameterInfoValues
          {
            Name = "HeaderNames",
            ParameterType = typeof(IReadOnlyList<string>),
            Position = 9,
            IsNullable = false,
            IsMemberInitializer = true
          },
          new JsonParameterInfoValues
          {
            Name = "TokenFromSignIn",
            ParameterType = typeof(bool),
            Position = 10,
            IsNullable = false,
            IsMemberInitializer = true
          },
          new JsonParameterInfoValues
          {
            Name = "DerivedCapabilityDescription",
            ParameterType = typeof(string),
            Position = 11,
            IsNullable = true,
            IsMemberInitializer = true
          },
          new JsonParameterInfoValues
          {
            Name = "BrowserSignInSupport",
            ParameterType = typeof(string),
            Position = 12,
            IsNullable = true,
            IsMemberInitializer = true
          },
          new JsonParameterInfoValues
          {
            Name = "CreatedAt",
            ParameterType = typeof(DateTimeOffset),
            Position = 13,
            IsNullable = false,
            IsMemberInitializer = true
          }
        };
      }

      // Token: 0x1700070C RID: 1804
      // (get) Token: 0x06002AE7 RID: 10983 RVA: 0x00652FD0 File Offset: 0x00652FD0
      [Nullable(0)]
      public JsonTypeInfo<Dictionary<string, string>> DictionaryStringString
      {
        [NullableContext(0)]
        get
        {
          JsonTypeInfo<Dictionary<string, string>> jsonTypeInfo;
          if ((jsonTypeInfo = this._DictionaryStringString) == null)
          {
            jsonTypeInfo = (this._DictionaryStringString = (JsonTypeInfo<Dictionary<string, string>>)base.Options.GetTypeInfo(typeof(Dictionary<string, string>)));
          }
          return jsonTypeInfo;
        }
      }

      // Token: 0x06002AE8 RID: 10984 RVA: 0x0065300C File Offset: 0x0065300C
      private JsonTypeInfo<Dictionary<string, string>> Create_DictionaryStringString(JsonSerializerOptions options)
      {
        JsonTypeInfo<Dictionary<string, string>> jsonTypeInfo;
        if (!CustomConnectorStore.ConnectorJsonContext.TryGetTypeInfoForRuntimeCustomConverter<Dictionary<string, string>>(options, out jsonTypeInfo))
        {
          JsonCollectionInfoValues<Dictionary<string, string>> jsonCollectionInfoValues = new JsonCollectionInfoValues<Dictionary<string, string>>();
          jsonCollectionInfoValues.ObjectCreator = () => new Dictionary<string, string>();
          jsonCollectionInfoValues.SerializeHandler = new Action<Utf8JsonWriter, Dictionary<string, string>>(this.DictionaryStringStringSerializeHandler);
          JsonCollectionInfoValues<Dictionary<string, string>> jsonCollectionInfoValues2 = jsonCollectionInfoValues;
          jsonTypeInfo = JsonMetadataServices.CreateDictionaryInfo<Dictionary<string, string>, string, string>(options, jsonCollectionInfoValues2);
          jsonTypeInfo.NumberHandling = null;
        }
        jsonTypeInfo.OriginatingResolver = this;
        return jsonTypeInfo;
      }

      // Token: 0x06002AE9 RID: 10985 RVA: 0x00653080 File Offset: 0x00653080
      private void DictionaryStringStringSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1, 1 })] Dictionary<string, string> value)
      {
        if (value == null)
        {
          writer.WriteNullValue();
          return;
        }
        writer.WriteStartObject();
        foreach (KeyValuePair<string, string> keyValuePair in value)
        {
          writer.WriteString(keyValuePair.Key, keyValuePair.Value);
        }
        writer.WriteEndObject();
      }

      // Token: 0x1700070D RID: 1805
      // (get) Token: 0x06002AEA RID: 10986 RVA: 0x006530F4 File Offset: 0x006530F4
      [Nullable(0)]
      public JsonTypeInfo<IReadOnlyList<string>> IReadOnlyListString
      {
        [NullableContext(0)]
        get
        {
          JsonTypeInfo<IReadOnlyList<string>> jsonTypeInfo;
          if ((jsonTypeInfo = this._IReadOnlyListString) == null)
          {
            jsonTypeInfo = (this._IReadOnlyListString = (JsonTypeInfo<IReadOnlyList<string>>)base.Options.GetTypeInfo(typeof(IReadOnlyList<string>)));
          }
          return jsonTypeInfo;
        }
      }

      // Token: 0x06002AEB RID: 10987 RVA: 0x00653130 File Offset: 0x00653130
      private JsonTypeInfo<IReadOnlyList<string>> Create_IReadOnlyListString(JsonSerializerOptions options)
      {
        JsonTypeInfo<IReadOnlyList<string>> jsonTypeInfo;
        if (!CustomConnectorStore.ConnectorJsonContext.TryGetTypeInfoForRuntimeCustomConverter<IReadOnlyList<string>>(options, out jsonTypeInfo))
        {
          JsonCollectionInfoValues<IReadOnlyList<string>> jsonCollectionInfoValues = new JsonCollectionInfoValues<IReadOnlyList<string>>
          {
            ObjectCreator = null,
            SerializeHandler = new Action<Utf8JsonWriter, IReadOnlyList<string>>(this.IReadOnlyListStringSerializeHandler)
          };
          jsonTypeInfo = JsonMetadataServices.CreateIEnumerableInfo<IReadOnlyList<string>, string>(options, jsonCollectionInfoValues);
          jsonTypeInfo.NumberHandling = null;
        }
        jsonTypeInfo.OriginatingResolver = this;
        return jsonTypeInfo;
      }

      // Token: 0x06002AEC RID: 10988 RVA: 0x00653188 File Offset: 0x00653188
      private void IReadOnlyListStringSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        if (value == null)
        {
          writer.WriteNullValue();
          return;
        }
        writer.WriteStartArray();
        foreach (string text in value)
        {
          writer.WriteStringValue(text);
        }
        writer.WriteEndArray();
      }

      // Token: 0x1700070E RID: 1806
      // (get) Token: 0x06002AED RID: 10989 RVA: 0x006531E8 File Offset: 0x006531E8
      [Nullable(0)]
      public JsonTypeInfo<List<CustomConnector>> ListCustomConnector
      {
        [NullableContext(0)]
        get
        {
          JsonTypeInfo<List<CustomConnector>> jsonTypeInfo;
          if ((jsonTypeInfo = this._ListCustomConnector) == null)
          {
            jsonTypeInfo = (this._ListCustomConnector = (JsonTypeInfo<List<CustomConnector>>)base.Options.GetTypeInfo(typeof(List<CustomConnector>)));
          }
          return jsonTypeInfo;
        }
      }

      // Token: 0x06002AEE RID: 10990 RVA: 0x00653224 File Offset: 0x00653224
      private JsonTypeInfo<List<CustomConnector>> Create_ListCustomConnector(JsonSerializerOptions options)
      {
        JsonTypeInfo<List<CustomConnector>> jsonTypeInfo;
        if (!CustomConnectorStore.ConnectorJsonContext.TryGetTypeInfoForRuntimeCustomConverter<List<CustomConnector>>(options, out jsonTypeInfo))
        {
          JsonCollectionInfoValues<List<CustomConnector>> jsonCollectionInfoValues = new JsonCollectionInfoValues<List<CustomConnector>>();
          jsonCollectionInfoValues.ObjectCreator = () => new List<CustomConnector>();
          jsonCollectionInfoValues.SerializeHandler = new Action<Utf8JsonWriter, List<CustomConnector>>(this.ListCustomConnectorSerializeHandler);
          JsonCollectionInfoValues<List<CustomConnector>> jsonCollectionInfoValues2 = jsonCollectionInfoValues;
          jsonTypeInfo = JsonMetadataServices.CreateListInfo<List<CustomConnector>, CustomConnector>(options, jsonCollectionInfoValues2);
          jsonTypeInfo.NumberHandling = null;
        }
        jsonTypeInfo.OriginatingResolver = this;
        return jsonTypeInfo;
      }

      // Token: 0x06002AEF RID: 10991 RVA: 0x00653298 File Offset: 0x00653298
      private void ListCustomConnectorSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] List<CustomConnector> value)
      {
        if (value == null)
        {
          writer.WriteNullValue();
          return;
        }
        writer.WriteStartArray();
        for (int i = 0; i < value.Count; i++)
        {
          this.CustomConnectorSerializeHandler(writer, value[i]);
        }
        writer.WriteEndArray();
      }

      // Token: 0x1700070F RID: 1807
      // (get) Token: 0x06002AF0 RID: 10992 RVA: 0x006532DC File Offset: 0x006532DC
      [Nullable(0)]
      public JsonTypeInfo<DateTimeOffset> DateTimeOffset
      {
        [NullableContext(0)]
        get
        {
          JsonTypeInfo<DateTimeOffset> jsonTypeInfo;
          if ((jsonTypeInfo = this._DateTimeOffset) == null)
          {
            jsonTypeInfo = (this._DateTimeOffset = (JsonTypeInfo<DateTimeOffset>)base.Options.GetTypeInfo(typeof(DateTimeOffset)));
          }
          return jsonTypeInfo;
        }
      }

      // Token: 0x06002AF1 RID: 10993 RVA: 0x00653318 File Offset: 0x00653318
      private JsonTypeInfo<DateTimeOffset> Create_DateTimeOffset(JsonSerializerOptions options)
      {
        JsonTypeInfo<DateTimeOffset> jsonTypeInfo;
        if (!CustomConnectorStore.ConnectorJsonContext.TryGetTypeInfoForRuntimeCustomConverter<DateTimeOffset>(options, out jsonTypeInfo))
        {
          jsonTypeInfo = JsonMetadataServices.CreateValueInfo<DateTimeOffset>(options, JsonMetadataServices.DateTimeOffsetConverter);
        }
        jsonTypeInfo.OriginatingResolver = this;
        return jsonTypeInfo;
      }

      // Token: 0x17000710 RID: 1808
      // (get) Token: 0x06002AF2 RID: 10994 RVA: 0x00653344 File Offset: 0x00653344
      [Nullable(0)]
      public JsonTypeInfo<string> String
      {
        [NullableContext(0)]
        get
        {
          JsonTypeInfo<string> jsonTypeInfo;
          if ((jsonTypeInfo = this._String) == null)
          {
            jsonTypeInfo = (this._String = (JsonTypeInfo<string>)base.Options.GetTypeInfo(typeof(string)));
          }
          return jsonTypeInfo;
        }
      }

      // Token: 0x06002AF3 RID: 10995 RVA: 0x00653380 File Offset: 0x00653380
      private JsonTypeInfo<string> Create_String(JsonSerializerOptions options)
      {
        JsonTypeInfo<string> jsonTypeInfo;
        if (!CustomConnectorStore.ConnectorJsonContext.TryGetTypeInfoForRuntimeCustomConverter<string>(options, out jsonTypeInfo))
        {
          jsonTypeInfo = JsonMetadataServices.CreateValueInfo<string>(options, JsonMetadataServices.StringConverter);
        }
        jsonTypeInfo.OriginatingResolver = this;
        return jsonTypeInfo;
      }

      // Token: 0x17000711 RID: 1809
      // (get) Token: 0x06002AF4 RID: 10996 RVA: 0x006533AC File Offset: 0x006533AC
      public static CustomConnectorStore.ConnectorJsonContext Default { get; } = new CustomConnectorStore.ConnectorJsonContext(new JsonSerializerOptions(CustomConnectorStore.ConnectorJsonContext.s_defaultOptions));

      // Token: 0x17000712 RID: 1810
      // (get) Token: 0x06002AF5 RID: 10997 RVA: 0x006533B4 File Offset: 0x006533B4
      [Nullable(2)]
      protected override JsonSerializerOptions GeneratedSerializerOptions
      {
        [NullableContext(2)]
        get;
      } = CustomConnectorStore.ConnectorJsonContext.s_defaultOptions;

      // Token: 0x06002AF6 RID: 10998 RVA: 0x006533BC File Offset: 0x006533BC
      public ConnectorJsonContext()
        : base(null)
      {
      }

      // Token: 0x06002AF7 RID: 10999 RVA: 0x006533D0 File Offset: 0x006533D0
      public ConnectorJsonContext(JsonSerializerOptions options)
        : base(options)
      {
      }

      // Token: 0x06002AF8 RID: 11000 RVA: 0x006533E4 File Offset: 0x006533E4
      private static bool TryGetTypeInfoForRuntimeCustomConverter<[Nullable(2)] TJsonMetadataType>(JsonSerializerOptions options, out JsonTypeInfo<TJsonMetadataType> jsonTypeInfo)
      {
        JsonConverter runtimeConverterForType = CustomConnectorStore.ConnectorJsonContext.GetRuntimeConverterForType(typeof(TJsonMetadataType), options);
        if (runtimeConverterForType != null)
        {
          jsonTypeInfo = JsonMetadataServices.CreateValueInfo<TJsonMetadataType>(options, runtimeConverterForType);
          return true;
        }
        jsonTypeInfo = null;
        return false;
      }

      // Token: 0x06002AF9 RID: 11001 RVA: 0x00653414 File Offset: 0x00653414
      [return: Nullable(2)]
      private static JsonConverter GetRuntimeConverterForType(Type type, JsonSerializerOptions options)
      {
        for (int i = 0; i < options.Converters.Count; i++)
        {
          JsonConverter jsonConverter = options.Converters[i];
          if (jsonConverter != null && jsonConverter.CanConvert(type))
          {
            return CustomConnectorStore.ConnectorJsonContext.ExpandConverter(type, jsonConverter, options, false);
          }
        }
        return null;
      }

      // Token: 0x06002AFA RID: 11002 RVA: 0x0065345C File Offset: 0x0065345C
      private static JsonConverter ExpandConverter(Type type, JsonConverter converter, JsonSerializerOptions options, bool validateCanConvert = true)
      {
        if (validateCanConvert && !converter.CanConvert(type))
        {
          throw new InvalidOperationException(string.Format("The converter '{0}' is not compatible with the type '{1}'.", converter.GetType(), type));
        }
        JsonConverterFactory jsonConverterFactory = converter as JsonConverterFactory;
        if (jsonConverterFactory != null)
        {
          converter = jsonConverterFactory.CreateConverter(type, options);
          if (converter == null || converter is JsonConverterFactory)
          {
            throw new InvalidOperationException(string.Format("The converter '{0}' cannot return null or a JsonConverterFactory instance.", jsonConverterFactory.GetType()));
          }
        }
        return converter;
      }

      // Token: 0x06002AFB RID: 11003 RVA: 0x006534C4 File Offset: 0x006534C4
      [return: Nullable(2)]
      public override JsonTypeInfo GetTypeInfo(Type type)
      {
        JsonTypeInfo jsonTypeInfo;
        base.Options.TryGetTypeInfo(type, out jsonTypeInfo);
        return jsonTypeInfo;
      }

      // Token: 0x06002AFC RID: 11004 RVA: 0x006534E4 File Offset: 0x006534E4
      [return: Nullable(2)]
      JsonTypeInfo IJsonTypeInfoResolver.GetTypeInfo(Type type, JsonSerializerOptions options)
      {
        if (type == typeof(bool))
        {
          return this.Create_Boolean(options);
        }
        if (type == typeof(CustomConnector))
        {
          return this.Create_CustomConnector(options);
        }
        if (type == typeof(Dictionary<string, string>))
        {
          return this.Create_DictionaryStringString(options);
        }
        if (type == typeof(IReadOnlyList<string>))
        {
          return this.Create_IReadOnlyListString(options);
        }
        if (type == typeof(List<CustomConnector>))
        {
          return this.Create_ListCustomConnector(options);
        }
        if (type == typeof(DateTimeOffset))
        {
          return this.Create_DateTimeOffset(options);
        }
        if (type == typeof(string))
        {
          return this.Create_String(options);
        }
        return null;
      }

      // Token: 0x040016CA RID: 5834
      [Nullable(2)]
      private JsonTypeInfo<bool> _Boolean;

      // Token: 0x040016CB RID: 5835
      [Nullable(new byte[] { 2, 1 })]
      private JsonTypeInfo<CustomConnector> _CustomConnector;

      // Token: 0x040016CC RID: 5836
      [Nullable(new byte[] { 2, 1, 1, 1 })]
      private JsonTypeInfo<Dictionary<string, string>> _DictionaryStringString;

      // Token: 0x040016CD RID: 5837
      [Nullable(new byte[] { 2, 1, 1 })]
      private JsonTypeInfo<IReadOnlyList<string>> _IReadOnlyListString;

      // Token: 0x040016CE RID: 5838
      [Nullable(new byte[] { 2, 1, 1 })]
      private JsonTypeInfo<List<CustomConnector>> _ListCustomConnector;

      // Token: 0x040016CF RID: 5839
      [Nullable(2)]
      private JsonTypeInfo<DateTimeOffset> _DateTimeOffset;

      // Token: 0x040016D0 RID: 5840
      [Nullable(new byte[] { 2, 1 })]
      private JsonTypeInfo<string> _String;

      // Token: 0x040016D1 RID: 5841
      private static readonly JsonSerializerOptions s_defaultOptions = new JsonSerializerOptions();

      // Token: 0x040016D2 RID: 5842
      private const BindingFlags InstanceMemberBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

      // Token: 0x040016D5 RID: 5845
      private static readonly JsonEncodedText PropName_Id = JsonEncodedText.Encode("Id", null);

      // Token: 0x040016D6 RID: 5846
      private static readonly JsonEncodedText PropName_Name = JsonEncodedText.Encode("Name", null);

      // Token: 0x040016D7 RID: 5847
      private static readonly JsonEncodedText PropName_Kind = JsonEncodedText.Encode("Kind", null);

      // Token: 0x040016D8 RID: 5848
      private static readonly JsonEncodedText PropName_Url = JsonEncodedText.Encode("Url", null);

      // Token: 0x040016D9 RID: 5849
      private static readonly JsonEncodedText PropName_Command = JsonEncodedText.Encode("Command", null);

      // Token: 0x040016DA RID: 5850
      private static readonly JsonEncodedText PropName_Arguments = JsonEncodedText.Encode("Arguments", null);

      // Token: 0x040016DB RID: 5851
      private static readonly JsonEncodedText PropName_EnvironmentNames = JsonEncodedText.Encode("EnvironmentNames", null);

      // Token: 0x040016DC RID: 5852
      private static readonly JsonEncodedText PropName_HasApiKey = JsonEncodedText.Encode("HasApiKey", null);

      // Token: 0x040016DD RID: 5853
      private static readonly JsonEncodedText PropName_OAuthClientId = JsonEncodedText.Encode("OAuthClientId", null);

      // Token: 0x040016DE RID: 5854
      private static readonly JsonEncodedText PropName_HeaderNames = JsonEncodedText.Encode("HeaderNames", null);

      // Token: 0x040016DF RID: 5855
      private static readonly JsonEncodedText PropName_TokenFromSignIn = JsonEncodedText.Encode("TokenFromSignIn", null);

      // Token: 0x040016E0 RID: 5856
      private static readonly JsonEncodedText PropName_DerivedCapabilityDescription = JsonEncodedText.Encode("DerivedCapabilityDescription", null);

      // Token: 0x040016E1 RID: 5857
      private static readonly JsonEncodedText PropName_BrowserSignInSupport = JsonEncodedText.Encode("BrowserSignInSupport", null);

      // Token: 0x040016E2 RID: 5858
      private static readonly JsonEncodedText PropName_CreatedAt = JsonEncodedText.Encode("CreatedAt", null);

      // Token: 0x02000ABA RID: 2746
      [CompilerGenerated]
      private static class <>O
      {
        // Token: 0x04002DEC RID: 11756
        [Nullable(new byte[] { 0, 1, 1 })]
        public static Func<JsonParameterInfoValues[]> <0>__CustomConnectorCtorParamInit;
      }
    }
  }
}
