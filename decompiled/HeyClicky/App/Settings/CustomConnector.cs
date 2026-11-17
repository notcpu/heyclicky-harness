using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000C9 RID: 201
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CustomConnector : IEquatable<CustomConnector>
  {
    // Token: 0x06000880 RID: 2176 RVA: 0x0053F754 File Offset: 0x0053F754
    public CustomConnector(string Id, string Name, string Kind, [Nullable(2)] string Url, [Nullable(2)] string Command, IReadOnlyList<string> Arguments, IReadOnlyList<string> EnvironmentNames, bool HasApiKey)
    {
      this.Id = Id;
      this.Name = Name;
      this.Kind = Kind;
      this.Url = Url;
      this.Command = Command;
      this.Arguments = Arguments;
      this.EnvironmentNames = EnvironmentNames;
      this.HasApiKey = HasApiKey;
      this.HeaderNames = Array.Empty<string>();
      base..ctor();
    }

    // Token: 0x1700014C RID: 332
    // (get) Token: 0x06000881 RID: 2177 RVA: 0x0053F7B0 File Offset: 0x0053F7B0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(CustomConnector);
      }
    }

    // Token: 0x1700014D RID: 333
    // (get) Token: 0x06000882 RID: 2178 RVA: 0x0053F7BC File Offset: 0x0053F7BC
    // (set) Token: 0x06000883 RID: 2179 RVA: 0x0053F7C4 File Offset: 0x0053F7C4
    public string Id { get; set; }

    // Token: 0x1700014E RID: 334
    // (get) Token: 0x06000884 RID: 2180 RVA: 0x0053F7D0 File Offset: 0x0053F7D0
    // (set) Token: 0x06000885 RID: 2181 RVA: 0x0053F7D8 File Offset: 0x0053F7D8
    public string Name { get; set; }

    // Token: 0x1700014F RID: 335
    // (get) Token: 0x06000886 RID: 2182 RVA: 0x0053F7E4 File Offset: 0x0053F7E4
    // (set) Token: 0x06000887 RID: 2183 RVA: 0x0053F7EC File Offset: 0x0053F7EC
    public string Kind { get; set; }

    // Token: 0x17000150 RID: 336
    // (get) Token: 0x06000888 RID: 2184 RVA: 0x0053F7F8 File Offset: 0x0053F7F8
    // (set) Token: 0x06000889 RID: 2185 RVA: 0x0053F800 File Offset: 0x0053F800
    [Nullable(2)]
    public string Url
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000151 RID: 337
    // (get) Token: 0x0600088A RID: 2186 RVA: 0x0053F80C File Offset: 0x0053F80C
    // (set) Token: 0x0600088B RID: 2187 RVA: 0x0053F814 File Offset: 0x0053F814
    [Nullable(2)]
    public string Command
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000152 RID: 338
    // (get) Token: 0x0600088C RID: 2188 RVA: 0x0053F820 File Offset: 0x0053F820
    // (set) Token: 0x0600088D RID: 2189 RVA: 0x0053F828 File Offset: 0x0053F828
    public IReadOnlyList<string> Arguments { get; set; }

    // Token: 0x17000153 RID: 339
    // (get) Token: 0x0600088E RID: 2190 RVA: 0x0053F834 File Offset: 0x0053F834
    // (set) Token: 0x0600088F RID: 2191 RVA: 0x0053F83C File Offset: 0x0053F83C
    public IReadOnlyList<string> EnvironmentNames { get; set; }

    // Token: 0x17000154 RID: 340
    // (get) Token: 0x06000890 RID: 2192 RVA: 0x0053F848 File Offset: 0x0053F848
    // (set) Token: 0x06000891 RID: 2193 RVA: 0x0053F850 File Offset: 0x0053F850
    public bool HasApiKey { get; set; }

    // Token: 0x17000155 RID: 341
    // (get) Token: 0x06000892 RID: 2194 RVA: 0x0053F85C File Offset: 0x0053F85C
    // (set) Token: 0x06000893 RID: 2195 RVA: 0x0053F864 File Offset: 0x0053F864
    [Nullable(2)]
    public string OAuthClientId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000156 RID: 342
    // (get) Token: 0x06000894 RID: 2196 RVA: 0x0053F870 File Offset: 0x0053F870
    // (set) Token: 0x06000895 RID: 2197 RVA: 0x0053F878 File Offset: 0x0053F878
    public IReadOnlyList<string> HeaderNames { get; set; }

    // Token: 0x17000157 RID: 343
    // (get) Token: 0x06000896 RID: 2198 RVA: 0x0053F884 File Offset: 0x0053F884
    // (set) Token: 0x06000897 RID: 2199 RVA: 0x0053F88C File Offset: 0x0053F88C
    public bool TokenFromSignIn { get; set; }

    // Token: 0x17000158 RID: 344
    // (get) Token: 0x06000898 RID: 2200 RVA: 0x0053F898 File Offset: 0x0053F898
    // (set) Token: 0x06000899 RID: 2201 RVA: 0x0053F8A0 File Offset: 0x0053F8A0
    [Nullable(2)]
    public string DerivedCapabilityDescription
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000159 RID: 345
    // (get) Token: 0x0600089A RID: 2202 RVA: 0x0053F8AC File Offset: 0x0053F8AC
    // (set) Token: 0x0600089B RID: 2203 RVA: 0x0053F8B4 File Offset: 0x0053F8B4
    [Nullable(2)]
    public string BrowserSignInSupport
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700015A RID: 346
    // (get) Token: 0x0600089C RID: 2204 RVA: 0x0053F8C0 File Offset: 0x0053F8C0
    // (set) Token: 0x0600089D RID: 2205 RVA: 0x0053F8C8 File Offset: 0x0053F8C8
    public DateTimeOffset CreatedAt { get; set; }

    // Token: 0x1700015B RID: 347
    // (get) Token: 0x0600089E RID: 2206 RVA: 0x0053F8D4 File Offset: 0x0053F8D4
    [JsonIgnore]
    public bool IsRemote
    {
      get
      {
        return this.Kind == "remote";
      }
    }

    // Token: 0x1700015C RID: 348
    // (get) Token: 0x0600089F RID: 2207 RVA: 0x0053F8E8 File Offset: 0x0053F8E8
    [JsonIgnore]
    public string ServerKey
    {
      get
      {
        string text = "custom_";
        IEnumerable<char> id = this.Id;
        Func<char, bool> func;
        if ((func = CustomConnector.<>O.<0>__IsAsciiLetterOrDigit) == null)
        {
          func = (CustomConnector.<>O.<0>__IsAsciiLetterOrDigit = new Func<char, bool>(char.IsAsciiLetterOrDigit));
        }
        return text + new string(id.Where<char>(func).ToArray<char>()).ToLowerInvariant();
      }
    }

    // Token: 0x1700015D RID: 349
    // (get) Token: 0x060008A0 RID: 2208 RVA: 0x0053F934 File Offset: 0x0053F934
    [JsonIgnore]
    public string IntegrationId
    {
      get
      {
        return "custom:" + this.Id;
      }
    }

    // Token: 0x1700015E RID: 350
    // (get) Token: 0x060008A1 RID: 2209 RVA: 0x0053F948 File Offset: 0x0053F948
    [JsonIgnore]
    public string TrimmedOAuthClientId
    {
      get
      {
        string oauthClientId = this.OAuthClientId;
        return ((oauthClientId != null) ? oauthClientId.Trim() : null) ?? "";
      }
    }

    // Token: 0x1700015F RID: 351
    // (get) Token: 0x060008A2 RID: 2210 RVA: 0x0053F968 File Offset: 0x0053F968
    [JsonIgnore]
    public bool UsesBrowserSignIn
    {
      get
      {
        return this.IsRemote && (this.TrimmedOAuthClientId.Length > 0 || this.TokenFromSignIn || !this.HasApiKey);
      }
    }

    // Token: 0x17000160 RID: 352
    // (get) Token: 0x060008A3 RID: 2211 RVA: 0x0053F998 File Offset: 0x0053F998
    [JsonIgnore]
    public bool UsesPlainBearerToken
    {
      get
      {
        return this.IsRemote && this.HasApiKey && !this.UsesBrowserSignIn;
      }
    }

    // Token: 0x17000161 RID: 353
    // (get) Token: 0x060008A4 RID: 2212 RVA: 0x0053F9B8 File Offset: 0x0053F9B8
    [JsonIgnore]
    public string LocationSummary
    {
      get
      {
        if (!this.IsRemote)
        {
          return string.Join(" ", new string[] { Path.GetFileName(this.Command ?? "") }.Concat<string>(this.Arguments));
        }
        Uri uri;
        string text;
        if (!Uri.TryCreate(this.Url, UriKind.Absolute, out uri))
        {
          if ((text = this.Url) == null)
          {
            return "";
          }
        }
        else
        {
          text = uri.Host;
        }
        return text;
      }
    }

    // Token: 0x17000162 RID: 354
    // (get) Token: 0x060008A5 RID: 2213 RVA: 0x0053FA28 File Offset: 0x0053FA28
    [JsonIgnore]
    public string Summary
    {
      get
      {
        if (!this.IsRemote)
        {
          return string.Join(" ", new string[] { this.Command ?? "" }.Concat<string>(this.Arguments));
        }
        return this.Url ?? "";
      }
    }

    // Token: 0x17000163 RID: 355
    // (get) Token: 0x060008A6 RID: 2214 RVA: 0x0053FA7C File Offset: 0x0053FA7C
    [JsonIgnore]
    public string EffectiveCapabilityDescription
    {
      get
      {
        string derivedCapabilityDescription = this.DerivedCapabilityDescription;
        return ((derivedCapabilityDescription != null) ? derivedCapabilityDescription.Trim() : null) ?? "";
      }
    }

    // Token: 0x060008A7 RID: 2215 RVA: 0x0053FA9C File Offset: 0x0053FA9C
    public bool HasSameAuthenticationTarget(CustomConnector other)
    {
      return !(this.Kind != other.Kind) && !(this.TrimmedOAuthClientId != other.TrimmedOAuthClientId) && (!this.IsRemote || CustomConnector.<HasSameAuthenticationTarget>g__Origin|77_0(this.Url) == CustomConnector.<HasSameAuthenticationTarget>g__Origin|77_0(other.Url));
    }

    // Token: 0x060008A8 RID: 2216 RVA: 0x0053FAF8 File Offset: 0x0053FAF8
    [return: Nullable(2)]
    public string LogoUrl(string workerBaseUrl)
    {
      if (this.IsRemote)
      {
        string url = this.Url;
        Uri uri;
        if (Uri.TryCreate((url != null) ? url.Trim() : null, UriKind.Absolute, out uri) && uri.Host.Length != 0 && !uri.IsLoopback)
        {
          string[] array = uri.Host.ToLowerInvariant().Split('.', StringSplitOptions.None);
          string text = ((array.Length > 2) ? string.Join('.', RuntimeHelpers.GetSubArray<string>(array, Range.StartAt(new Index(2, true)))) : uri.Host.ToLowerInvariant());
          return workerBaseUrl.TrimEnd('/') + "/favicon?host=" + Uri.EscapeDataString(text) + "&sz=64";
        }
      }
      return null;
    }

    // Token: 0x060008A9 RID: 2217 RVA: 0x0053FBA0 File Offset: 0x0053FBA0
    public static bool IsValidEnvironmentVariableName(string name)
    {
      if (name.Length > 0 && (name[0] == '_' || char.IsAsciiLetter(name[0])))
      {
        return name.All<char>((char c) => c == '_' || char.IsAsciiLetterOrDigit(c));
      }
      return false;
    }

    // Token: 0x060008AA RID: 2218 RVA: 0x0053FBF8 File Offset: 0x0053FBF8
    public static bool IsValidHeaderName(string name)
    {
      if (name.Length > 0)
      {
        return name.All<char>(delegate(char c)
        {
          bool flag = char.IsAsciiLetterOrDigit(c);
          if (!flag)
          {
            bool flag2 = c == '-' || c == '_';
            flag = flag2;
          }
          return flag;
        });
      }
      return false;
    }

    // Token: 0x060008AB RID: 2219 RVA: 0x0053FC2C File Offset: 0x0053FC2C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("CustomConnector");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060008AC RID: 2220 RVA: 0x0053FC78 File Offset: 0x0053FC78
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", Name = ");
      builder.Append(this.Name);
      builder.Append(", Kind = ");
      builder.Append(this.Kind);
      builder.Append(", Url = ");
      builder.Append(this.Url);
      builder.Append(", Command = ");
      builder.Append(this.Command);
      builder.Append(", Arguments = ");
      builder.Append(this.Arguments);
      builder.Append(", EnvironmentNames = ");
      builder.Append(this.EnvironmentNames);
      builder.Append(", HasApiKey = ");
      builder.Append(this.HasApiKey.ToString());
      builder.Append(", OAuthClientId = ");
      builder.Append(this.OAuthClientId);
      builder.Append(", HeaderNames = ");
      builder.Append(this.HeaderNames);
      builder.Append(", TokenFromSignIn = ");
      builder.Append(this.TokenFromSignIn.ToString());
      builder.Append(", DerivedCapabilityDescription = ");
      builder.Append(this.DerivedCapabilityDescription);
      builder.Append(", BrowserSignInSupport = ");
      builder.Append(this.BrowserSignInSupport);
      builder.Append(", CreatedAt = ");
      builder.Append(this.CreatedAt.ToString());
      builder.Append(", IsRemote = ");
      builder.Append(this.IsRemote.ToString());
      builder.Append(", ServerKey = ");
      builder.Append(this.ServerKey);
      builder.Append(", IntegrationId = ");
      builder.Append(this.IntegrationId);
      builder.Append(", TrimmedOAuthClientId = ");
      builder.Append(this.TrimmedOAuthClientId);
      builder.Append(", UsesBrowserSignIn = ");
      builder.Append(this.UsesBrowserSignIn.ToString());
      builder.Append(", UsesPlainBearerToken = ");
      builder.Append(this.UsesPlainBearerToken.ToString());
      builder.Append(", LocationSummary = ");
      builder.Append(this.LocationSummary);
      builder.Append(", Summary = ");
      builder.Append(this.Summary);
      builder.Append(", EffectiveCapabilityDescription = ");
      builder.Append(this.EffectiveCapabilityDescription);
      return true;
    }

    // Token: 0x060008AD RID: 2221 RVA: 0x0053FF20 File Offset: 0x0053FF20
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(CustomConnector left, CustomConnector right)
    {
      return !(left == right);
    }

    // Token: 0x060008AE RID: 2222 RVA: 0x0053FF2C File Offset: 0x0053FF2C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(CustomConnector left, CustomConnector right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060008AF RID: 2223 RVA: 0x0053FF40 File Offset: 0x0053FF40
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Url>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Command>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Arguments>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<EnvironmentNames>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<HasApiKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<OAuthClientId>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<HeaderNames>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<TokenFromSignIn>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DerivedCapabilityDescription>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BrowserSignInSupport>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<CreatedAt>k__BackingField);
    }

    // Token: 0x060008B0 RID: 2224 RVA: 0x005400A0 File Offset: 0x005400A0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CustomConnector);
    }

    // Token: 0x060008B1 RID: 2225 RVA: 0x005400B0 File Offset: 0x005400B0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(CustomConnector other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Url>k__BackingField, other.<Url>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Command>k__BackingField, other.<Command>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Arguments>k__BackingField, other.<Arguments>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<EnvironmentNames>k__BackingField, other.<EnvironmentNames>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<HasApiKey>k__BackingField, other.<HasApiKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<OAuthClientId>k__BackingField, other.<OAuthClientId>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<HeaderNames>k__BackingField, other.<HeaderNames>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<TokenFromSignIn>k__BackingField, other.<TokenFromSignIn>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DerivedCapabilityDescription>k__BackingField, other.<DerivedCapabilityDescription>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BrowserSignInSupport>k__BackingField, other.<BrowserSignInSupport>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<CreatedAt>k__BackingField, other.<CreatedAt>k__BackingField));
    }

    // Token: 0x060008B3 RID: 2227 RVA: 0x00540254 File Offset: 0x00540254
    [CompilerGenerated]
    private CustomConnector(CustomConnector original)
    {
      this.Id = original.<Id>k__BackingField;
      this.Name = original.<Name>k__BackingField;
      this.Kind = original.<Kind>k__BackingField;
      this.Url = original.<Url>k__BackingField;
      this.Command = original.<Command>k__BackingField;
      this.Arguments = original.<Arguments>k__BackingField;
      this.EnvironmentNames = original.<EnvironmentNames>k__BackingField;
      this.HasApiKey = original.<HasApiKey>k__BackingField;
      this.OAuthClientId = original.<OAuthClientId>k__BackingField;
      this.HeaderNames = original.<HeaderNames>k__BackingField;
      this.TokenFromSignIn = original.<TokenFromSignIn>k__BackingField;
      this.DerivedCapabilityDescription = original.<DerivedCapabilityDescription>k__BackingField;
      this.BrowserSignInSupport = original.<BrowserSignInSupport>k__BackingField;
      this.CreatedAt = original.<CreatedAt>k__BackingField;
    }

    // Token: 0x060008B4 RID: 2228 RVA: 0x00540310 File Offset: 0x00540310
    [CompilerGenerated]
    public void Deconstruct(out string Id, out string Name, out string Kind, [Nullable(2)] out string Url, [Nullable(2)] out string Command, out IReadOnlyList<string> Arguments, out IReadOnlyList<string> EnvironmentNames, out bool HasApiKey)
    {
      Id = this.Id;
      Name = this.Name;
      Kind = this.Kind;
      Url = this.Url;
      Command = this.Command;
      Arguments = this.Arguments;
      EnvironmentNames = this.EnvironmentNames;
      HasApiKey = this.HasApiKey;
    }

    // Token: 0x060008B5 RID: 2229 RVA: 0x00540364 File Offset: 0x00540364
    [NullableContext(2)]
    [CompilerGenerated]
    internal static string <HasSameAuthenticationTarget>g__Origin|77_0(string u)
    {
      Uri uri;
      if (!Uri.TryCreate((u != null) ? u.Trim() : null, UriKind.Absolute, out uri))
      {
        return null;
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
      defaultInterpolatedStringHandler.AppendFormatted(uri.Scheme);
      defaultInterpolatedStringHandler.AppendLiteral("://");
      defaultInterpolatedStringHandler.AppendFormatted(uri.Host.ToLowerInvariant());
      defaultInterpolatedStringHandler.AppendLiteral(":");
      defaultInterpolatedStringHandler.AppendFormatted<int>(uri.Port);
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x02000491 RID: 1169
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040016C6 RID: 5830
      [Nullable(0)]
      public static Func<char, bool> <0>__IsAsciiLetterOrDigit;
    }
  }
}
