using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HeyClicky.Core.Auth
{
  // Token: 0x02000225 RID: 549
  [NullableContext(1)]
  [Nullable(0)]
  public static class AuthDeepLink
  {
    // Token: 0x0600202F RID: 8239 RVA: 0x003F46C8 File Offset: 0x003F46C8
    public static bool IsAuthCallback(Uri uri)
    {
      return uri.IsAbsoluteUri && AuthDeepLink.Schemes.Contains(uri.Scheme, StringComparer.OrdinalIgnoreCase) && string.Equals(uri.Host, "auth-callback", StringComparison.OrdinalIgnoreCase);
    }

    // Token: 0x06002030 RID: 8240 RVA: 0x003F46FC File Offset: 0x003F46FC
    [return: Nullable(2)]
    public static AuthCallback Parse(Uri uri)
    {
      if (!AuthDeepLink.IsAuthCallback(uri))
      {
        return null;
      }
      Dictionary<string, string> dictionary = AuthDeepLink.ParseParameters(uri.OriginalString);
      string text;
      if (!dictionary.TryGetValue("access_token", out text) || string.IsNullOrEmpty(text))
      {
        return null;
      }
      string text2;
      dictionary.TryGetValue("refresh_token", out text2);
      string text3;
      int num2;
      int? num = ((dictionary.TryGetValue("expires_in", out text3) && int.TryParse(text3, out num2)) ? new int?(num2) : null);
      string text4;
      dictionary.TryGetValue("referral_code", out text4);
      string text5;
      dictionary.TryGetValue("clicky_state", out text5);
      return new AuthCallback(text, string.IsNullOrEmpty(text2) ? null : text2, num, string.IsNullOrEmpty(text4) ? null : text4, string.IsNullOrEmpty(text5) ? null : text5);
    }

    // Token: 0x06002031 RID: 8241 RVA: 0x003F47C0 File Offset: 0x003F47C0
    [return: Nullable(2)]
    public static AuthCallbackError ParseError(Uri uri)
    {
      if (!AuthDeepLink.IsAuthCallback(uri))
      {
        return null;
      }
      AuthDeepLink.<>c__DisplayClass3_0 CS$<>8__locals1;
      CS$<>8__locals1.parameters = AuthDeepLink.ParseParameters(uri.OriginalString);
      string text;
      if (CS$<>8__locals1.parameters.TryGetValue("access_token", out text) && !string.IsNullOrEmpty(text))
      {
        return null;
      }
      return new AuthCallbackError(AuthDeepLink.<ParseError>g__FormDecoded|3_0("error", ref CS$<>8__locals1), AuthDeepLink.<ParseError>g__FormDecoded|3_0("error_code", ref CS$<>8__locals1), AuthDeepLink.<ParseError>g__FormDecoded|3_0("error_description", ref CS$<>8__locals1));
    }

    // Token: 0x06002032 RID: 8242 RVA: 0x003F4830 File Offset: 0x003F4830
    public static string RedactSignInErrorText([Nullable(2)] string text)
    {
      if (string.IsNullOrWhiteSpace(text))
      {
        return "";
      }
      string text2 = AuthDeepLink.EmailPattern.Replace(text.Trim(), "[email]");
      text2 = AuthDeepLink.TokenLikeRunPattern.Replace(text2, "[redacted]");
      if (text2.Length <= 200)
      {
        return text2;
      }
      return text2.Substring(0, 200);
    }

    // Token: 0x06002033 RID: 8243 RVA: 0x003F4890 File Offset: 0x003F4890
    public static Dictionary<string, string> ParseParameters(string url)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
      int num = url.IndexOf('?');
      int num2 = url.IndexOf('#');
      if (num >= 0)
      {
        int num3 = ((num2 > num) ? num2 : url.Length);
        AuthDeepLink.AddPairs(url.AsSpan(num + 1, num3 - num - 1), dictionary);
      }
      if (num2 >= 0)
      {
        AuthDeepLink.AddPairs(url.AsSpan(num2 + 1), dictionary);
      }
      return dictionary;
    }

    // Token: 0x06002034 RID: 8244 RVA: 0x003F48F4 File Offset: 0x003F48F4
    [NullableContext(0)]
    private static void AddPairs(ReadOnlySpan<char> text, [Nullable(1)] Dictionary<string, string> into)
    {
      foreach (Range range in text.Split('&'))
      {
        Range range2 = range;
        int length = text.Length;
        int offset = range2.Start.GetOffset(length);
        int num = range2.End.GetOffset(length) - offset;
        ReadOnlySpan<char> readOnlySpan = text.Slice(offset, num);
        if (!readOnlySpan.IsEmpty)
        {
          int num2 = readOnlySpan.IndexOf('=');
          ReadOnlySpan<char> readOnlySpan2 = ((num2 < 0) ? readOnlySpan : readOnlySpan.Slice(0, num2));
          ReadOnlySpan<char> readOnlySpan3;
          if (num2 >= 0)
          {
            ref ReadOnlySpan<char> ptr = ref readOnlySpan;
            num = num2 + 1;
            readOnlySpan3 = ptr.Slice(num, ptr.Length - num);
          }
          else
          {
            readOnlySpan3 = ReadOnlySpan<char>.Empty;
          }
          ReadOnlySpan<char> readOnlySpan4 = readOnlySpan3;
          into[Uri.UnescapeDataString(readOnlySpan2.ToString())] = Uri.UnescapeDataString(readOnlySpan4.ToString());
        }
      }
    }

    // Token: 0x06002036 RID: 8246 RVA: 0x003F4A3C File Offset: 0x003F4A3C
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <ParseError>g__FormDecoded|3_0(string name, ref AuthDeepLink.<>c__DisplayClass3_0 A_1)
    {
      string text;
      if (!A_1.parameters.TryGetValue(name, out text) || string.IsNullOrWhiteSpace(text))
      {
        return null;
      }
      return text.Replace('+', ' ').Trim();
    }

    // Token: 0x04000D9A RID: 3482
    public static readonly string[] Schemes = new string[] { "clicky", "heyclicky" };

    // Token: 0x04000D9B RID: 3483
    private static readonly Regex EmailPattern = new Regex("[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}", RegexOptions.CultureInvariant);

    // Token: 0x04000D9C RID: 3484
    private static readonly Regex TokenLikeRunPattern = new Regex("[A-Za-z0-9/_+=.-]{24,}", RegexOptions.CultureInvariant);
  }
}
