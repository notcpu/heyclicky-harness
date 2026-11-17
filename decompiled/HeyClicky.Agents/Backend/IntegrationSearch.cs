using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace HeyClicky.Agents.Backend
{
  // Token: 0x02000074 RID: 116
  [NullableContext(1)]
  [Nullable(0)]
  public static class IntegrationSearch
  {
    // Token: 0x06000923 RID: 2339 RVA: 0x0010388C File Offset: 0x0010388C
    public static int? RecommendedRank(AgentIntegration integration)
    {
      for (int i = 0; i < IntegrationSearch.PreferredOrder.Length; i++)
      {
        if (string.Equals(integration.Id, IntegrationSearch.PreferredOrder[i], StringComparison.OrdinalIgnoreCase) || string.Equals(integration.PrimaryToolkitSlug, IntegrationSearch.PreferredOrder[i], StringComparison.OrdinalIgnoreCase))
        {
          return new int?(i);
        }
      }
      return null;
    }

    // Token: 0x06000924 RID: 2340 RVA: 0x001038E8 File Offset: 0x001038E8
    public static int Compare(AgentIntegration left, AgentIntegration right)
    {
      int? num = IntegrationSearch.RecommendedRank(left);
      int? num2 = IntegrationSearch.RecommendedRank(right);
      int? num3 = num;
      if (num3 != null)
      {
        int valueOrDefault = num3.GetValueOrDefault();
        if (num2 != null)
        {
          int valueOrDefault2 = num2.GetValueOrDefault();
          if (valueOrDefault != valueOrDefault2)
          {
            return valueOrDefault.CompareTo(valueOrDefault2);
          }
        }
      }
      if (num3 != null && num2 == null)
      {
        return -1;
      }
      if (num3 == null && num2 != null)
      {
        return 1;
      }
      if (left.IsActiveConnection == right.IsActiveConnection)
      {
        return string.Compare(left.DisplayName, right.DisplayName, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase);
      }
      if (!left.IsActiveConnection)
      {
        return 1;
      }
      return -1;
    }

    // Token: 0x06000925 RID: 2341 RVA: 0x0010398C File Offset: 0x0010398C
    public static List<AgentIntegration> Sorted(IEnumerable<AgentIntegration> integrations)
    {
      List<AgentIntegration> list = integrations.ToList<AgentIntegration>();
      Comparison<AgentIntegration> comparison;
      if ((comparison = IntegrationSearch.<>O.<0>__Compare) == null)
      {
        comparison = (IntegrationSearch.<>O.<0>__Compare = new Comparison<AgentIntegration>(IntegrationSearch.Compare));
      }
      list.Sort(comparison);
      return list;
    }

    // Token: 0x06000926 RID: 2342 RVA: 0x001039B8 File Offset: 0x001039B8
    public static bool Matches(AgentIntegration integration, string query)
    {
      string text = IntegrationSearch.Normalize(query);
      string[] array = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      if (array.Length == 0)
      {
        return true;
      }
      string text2 = text.Replace(" ", "");
      foreach (string text3 in new string[] { integration.DisplayName, integration.Description, integration.Id, integration.PrimaryToolkitSlug }.Concat<string>(integration.UseCases).Concat<string>(integration.Toolkits.SelectMany<AgentIntegrationToolkit, string>((AgentIntegrationToolkit t) => new string[] { t.Slug, t.Name })))
      {
        string text4 = IntegrationSearch.Normalize(text3);
        if (text4.Length != 0)
        {
          if (text4.Contains(text, StringComparison.Ordinal))
          {
            return true;
          }
          if (text4.Replace(" ", "").Contains(text2, StringComparison.Ordinal))
          {
            return true;
          }
          string[] textTokens = text4.Split(' ', StringSplitOptions.RemoveEmptyEntries);
          if (array.All<string>((string queryToken) => textTokens.Any<string>((string textToken) => textToken.StartsWith(queryToken, StringComparison.Ordinal) || (queryToken.Length >= 3 && IntegrationSearch.Similarity(textToken, queryToken) >= ((queryToken.Length <= 4) ? 0.8 : 0.72)))))
          {
            return true;
          }
        }
      }
      return false;
    }

    // Token: 0x06000927 RID: 2343 RVA: 0x00103AF8 File Offset: 0x00103AF8
    public static string Normalize([Nullable(2)] string text)
    {
      if (string.IsNullOrEmpty(text))
      {
        return "";
      }
      string text2 = text.Normalize(NormalizationForm.FormD);
      StringBuilder stringBuilder = new StringBuilder(text2.Length);
      foreach (char c in text2)
      {
        if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
        {
          stringBuilder.Append(c);
        }
      }
      return Regex.Replace(stringBuilder.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant(), "[^a-z0-9]+", " ").Trim();
    }

    // Token: 0x06000928 RID: 2344 RVA: 0x00103B78 File Offset: 0x00103B78
    public static double Similarity(string a, string b)
    {
      int num = Math.Max(a.Length, b.Length);
      if (num != 0)
      {
        return 1.0 - (double)IntegrationSearch.LevenshteinDistance(a, b) / (double)num;
      }
      return 1.0;
    }

    // Token: 0x06000929 RID: 2345 RVA: 0x00103BBC File Offset: 0x00103BBC
    public static int LevenshteinDistance(string source, string target)
    {
      if (source.Length == 0)
      {
        return target.Length;
      }
      if (target.Length == 0)
      {
        return source.Length;
      }
      int[] array = new int[target.Length + 1];
      int[] array2 = new int[target.Length + 1];
      for (int i = 0; i <= target.Length; i++)
      {
        array[i] = i;
      }
      for (int j = 1; j <= source.Length; j++)
      {
        array2[0] = j;
        for (int k = 1; k <= target.Length; k++)
        {
          int num = array[k] + 1;
          int num2 = array2[k - 1] + 1;
          int num3 = array[k - 1] + ((source[j - 1] != target[k - 1]) ? 1 : 0);
          array2[k] = Math.Min(Math.Min(num, num2), num3);
        }
        int[] array3 = array2;
        array2 = array;
        array = array3;
      }
      return array[target.Length];
    }

    // Token: 0x04000337 RID: 823
    private static readonly string[] PreferredOrder = new string[]
    {
      "notion", "linear", "github", "googledocs", "googlecalendar", "linkedin", "slack", "gmail", "googlesheets", "googledrive",
      "obsidian"
    };

    // Token: 0x020001E2 RID: 482
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04000919 RID: 2329
      [Nullable(0)]
      public static Comparison<AgentIntegration> <0>__Compare;
    }
  }
}
