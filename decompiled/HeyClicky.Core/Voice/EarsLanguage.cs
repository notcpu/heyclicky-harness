using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using HeyClicky.Core.Configuration;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000073 RID: 115
  [NullableContext(1)]
  [Nullable(0)]
  public static class EarsLanguage
  {
    // Token: 0x06000704 RID: 1796 RVA: 0x00360D20 File Offset: 0x00360D20
    public static string Code()
    {
      return "multi";
    }

    // Token: 0x17000199 RID: 409
    // (get) Token: 0x06000705 RID: 1797 RVA: 0x00360D28 File Offset: 0x00360D28
    // (set) Token: 0x06000706 RID: 1798 RVA: 0x00360D30 File Offset: 0x00360D30
    internal static string StorePath { get; set; } = Path.Combine(AppBundleConfiguration.AppDataDirectory, "OneBrainHeardScripts.json");

    // Token: 0x06000707 RID: 1799 RVA: 0x00360D38 File Offset: 0x00360D38
    public static void RecordHeard(string words)
    {
      EarsLanguage.RecordHeardScripts(EarsLanguage.ScriptsIn(words));
    }

    // Token: 0x06000708 RID: 1800 RVA: 0x00360D48 File Offset: 0x00360D48
    private static void RecordHeardScripts(IReadOnlySet<EarsLanguage.Script> scripts)
    {
      if (scripts.Count == 0)
      {
        return;
      }
      object obj = EarsLanguage.storeGate;
      lock (obj)
      {
        List<List<string>> list = EarsLanguage.LoadLocked();
        list.Add(scripts.Select<EarsLanguage.Script, string>((EarsLanguage.Script s) => s.ToString()).OrderBy<string, string>((string s) => s, StringComparer.Ordinal).ToList<string>());
        if (list.Count > 30)
        {
          list.RemoveRange(0, list.Count - 30);
        }
        EarsLanguage.cachedHistory = list;
        try
        {
          Directory.CreateDirectory(Path.GetDirectoryName(EarsLanguage.StorePath));
          File.WriteAllText(EarsLanguage.StorePath, JsonSerializer.Serialize<List<List<string>>>(list, EarsLanguageJson.Default.ListListString));
        }
        catch (Exception ex)
        {
          Log.Warn("Companion", "ears: the heard-scripts history wasn't saved: " + ex.Message);
        }
      }
    }

    // Token: 0x06000709 RID: 1801 RVA: 0x00360E5C File Offset: 0x00360E5C
    public static IReadOnlyList<IReadOnlyList<string>> StoredHeardScripts()
    {
      object obj = EarsLanguage.storeGate;
      IReadOnlyList<IReadOnlyList<string>> readOnlyList;
      lock (obj)
      {
        readOnlyList = (from t in EarsLanguage.LoadLocked()
          select t.ToList<string>()).ToList<IReadOnlyList<string>>();
      }
      return readOnlyList;
    }

    // Token: 0x0600070A RID: 1802 RVA: 0x00360EC8 File Offset: 0x00360EC8
    internal static void ResetCacheForTests()
    {
      object obj = EarsLanguage.storeGate;
      lock (obj)
      {
        EarsLanguage.cachedHistory = null;
      }
    }

    // Token: 0x0600070B RID: 1803 RVA: 0x00360F08 File Offset: 0x00360F08
    private static List<List<string>> LoadLocked()
    {
      List<List<string>> list = EarsLanguage.cachedHistory;
      if (list != null)
      {
        return list;
      }
      List<List<string>> list2 = new List<List<string>>();
      try
      {
        if (File.Exists(EarsLanguage.StorePath))
        {
          list2 = JsonSerializer.Deserialize<List<List<string>>>(File.ReadAllText(EarsLanguage.StorePath), EarsLanguageJson.Default.ListListString) ?? new List<List<string>>();
        }
      }
      catch (Exception ex) when (ex is IOException || ex is JsonException || ex is UnauthorizedAccessException)
      {
        list2 = new List<List<string>>();
      }
      EarsLanguage.cachedHistory = list2;
      return list2;
    }

    // Token: 0x0600070C RID: 1804 RVA: 0x00360FA8 File Offset: 0x00360FA8
    public static IReadOnlySet<EarsLanguage.Script> AcceptedScripts(IEnumerable<string> voiceLanguageCodes, [Nullable(2)] string dictationLanguageCode, IEnumerable<string> systemLanguageTags, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyList<IReadOnlyList<string>> heardScripts = null)
    {
      HashSet<EarsLanguage.Script> hashSet = new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Latin };
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      foreach (IReadOnlyList<string> readOnlyList in (heardScripts ?? EarsLanguage.StoredHeardScripts()))
      {
        foreach (string text in readOnlyList.Distinct<string>())
        {
          dictionary[text] = dictionary.GetValueOrDefault(text) + 1;
        }
      }
      foreach (KeyValuePair<string, int> keyValuePair in dictionary)
      {
        string text2;
        int num;
        keyValuePair.Deconstruct(out text2, out num);
        string text3 = text2;
        EarsLanguage.Script script;
        if (num >= 2 && Enum.TryParse<EarsLanguage.Script>(text3, out script))
        {
          hashSet.Add(script);
        }
      }
      List<string> list = voiceLanguageCodes.Concat<string>(systemLanguageTags).ToList<string>();
      if (dictationLanguageCode != null && dictationLanguageCode.Length > 0 && dictationLanguageCode != "multi")
      {
        list.Add(dictationLanguageCode);
      }
      foreach (string text4 in list)
      {
        hashSet.UnionWith(EarsLanguage.Scripts(EarsLanguage.BaseCode(text4)));
      }
      return hashSet;
    }

    // Token: 0x0600070D RID: 1805 RVA: 0x00361130 File Offset: 0x00361130
    private static string BaseCode(string code)
    {
      string text = code.Replace('_', '-');
      if (text.StartsWith("zh", StringComparison.OrdinalIgnoreCase))
      {
        return "zh";
      }
      return text.Split('-', StringSplitOptions.None)[0].ToLowerInvariant();
    }

    // Token: 0x0600070E RID: 1806 RVA: 0x0036116C File Offset: 0x0036116C
    public static IReadOnlySet<EarsLanguage.Script> Scripts(string code)
    {
      uint num = <PrivateImplementationDetails>.ComputeStringHash(code);
      if (num <= 1245219017U)
      {
        if (num <= 1079120113U)
        {
          if (num <= 995232018U)
          {
            if (num <= 944899161U)
            {
              if (num != 942383232U)
              {
                if (num != 944060518U)
                {
                  if (num != 944899161U)
                  {
                    goto IL_08C6;
                  }
                  if (!(code == "sa"))
                  {
                    goto IL_08C6;
                  }
                  goto IL_070E;
                }
                else
                {
                  if (!(code == "ta"))
                  {
                    goto IL_08C6;
                  }
                  return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Tamil };
                }
              }
              else
              {
                if (!(code == "be"))
                {
                  goto IL_08C6;
                }
                goto IL_08A6;
              }
            }
            else if (num != 975938470U)
            {
              if (num != 977615756U)
              {
                if (num != 995232018U)
                {
                  goto IL_08C6;
                }
                if (!(code == "sd"))
                {
                  goto IL_08C6;
                }
              }
              else
              {
                if (!(code == "tg"))
                {
                  goto IL_08C6;
                }
                goto IL_08A6;
              }
            }
            else
            {
              if (!(code == "bg"))
              {
                goto IL_08C6;
              }
              goto IL_08A6;
            }
          }
          else
          {
            if (num > 1058693732U)
            {
              if (num != 1076162899U)
              {
                if (num != 1078281470U)
                {
                  if (num != 1079120113U)
                  {
                    goto IL_08C6;
                  }
                  if (!(code == "si"))
                  {
                    goto IL_08C6;
                  }
                  return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Sinhala };
                }
                else if (!(code == "ti"))
                {
                  goto IL_08C6;
                }
              }
              else if (!(code == "am"))
              {
                goto IL_08C6;
              }
              return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Ethiopic };
            }
            if (num != 1011170994U)
            {
              if (num != 1044181779U)
              {
                if (num != 1058693732U)
                {
                  goto IL_08C6;
                }
                if (!(code == "el"))
                {
                  goto IL_08C6;
                }
                return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Greek };
              }
              else
              {
                if (!(code == "kk"))
                {
                  goto IL_08C6;
                }
                goto IL_08A6;
              }
            }
            else
            {
              if (!(code == "te"))
              {
                goto IL_08C6;
              }
              return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Telugu };
            }
          }
        }
        else if (num <= 1144847493U)
        {
          if (num <= 1110556780U)
          {
            if (num != 1094514636U)
            {
              if (num != 1095059089U)
              {
                if (num != 1110556780U)
                {
                  goto IL_08C6;
                }
                if (!(code == "lo"))
                {
                  goto IL_08C6;
                }
                return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Lao };
              }
              else
              {
                if (!(code == "th"))
                {
                  goto IL_08C6;
                }
                return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Thai };
              }
            }
            else
            {
              if (!(code == "kn"))
              {
                goto IL_08C6;
              }
              return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Kannada };
            }
          }
          else if (num != 1111292255U)
          {
            if (num != 1126937041U)
            {
              if (num != 1144847493U)
              {
                goto IL_08C6;
              }
              if (!(code == "km"))
              {
                goto IL_08C6;
              }
              return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Khmer };
            }
            else
            {
              if (!(code == "bn"))
              {
                goto IL_08C6;
              }
              goto IL_0721;
            }
          }
          else
          {
            if (!(code == "ko"))
            {
              goto IL_08C6;
            }
            return new HashSet<EarsLanguage.Script>
            {
              EarsLanguage.Script.Hangul,
              EarsLanguage.Script.Han
            };
          }
        }
        else if (num <= 1164435231U)
        {
          if (num != 1162022470U)
          {
            if (num != 1163008208U)
            {
              if (num != 1164435231U)
              {
                goto IL_08C6;
              }
              if (!(code == "zh"))
              {
                goto IL_08C6;
              }
              goto IL_0895;
            }
            else
            {
              if (!(code == "sr"))
              {
                goto IL_08C6;
              }
              goto IL_08A6;
            }
          }
          else if (!(code == "ur"))
          {
            goto IL_08C6;
          }
        }
        else if (num != 1211957969U)
        {
          if (num != 1213488160U)
          {
            if (num != 1245219017U)
            {
              goto IL_08C6;
            }
            if (!(code == "iw"))
            {
              goto IL_08C6;
            }
            goto IL_06E8;
          }
          else
          {
            if (!(code == "ru"))
            {
              goto IL_08C6;
            }
            goto IL_08A6;
          }
        }
        else
        {
          if (!(code == "ka"))
          {
            goto IL_08C6;
          }
          return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Georgian };
        }
      }
      else if (num <= 1562713850U)
      {
        if (num <= 1429081278U)
        {
          if (num <= 1278082707U)
          {
            if (num != 1260172255U)
            {
              if (num != 1277200137U)
              {
                if (num != 1278082707U)
                {
                  goto IL_08C6;
                }
                if (!(code == "my"))
                {
                  goto IL_08C6;
                }
                return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Myanmar };
              }
              else
              {
                if (!(code == "gu"))
                {
                  goto IL_08C6;
                }
                return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Gujarati };
              }
            }
            else
            {
              if (!(code == "dv"))
              {
                goto IL_08C6;
              }
              return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Thaana };
            }
          }
          else if (num != 1346178921U)
          {
            if (num != 1347311754U)
            {
              if (num != 1429081278U)
              {
                goto IL_08C6;
              }
              if (!(code == "mr"))
              {
                goto IL_08C6;
              }
              goto IL_070E;
            }
            else
            {
              if (!(code == "pa"))
              {
                goto IL_08C6;
              }
              return new HashSet<EarsLanguage.Script>
              {
                EarsLanguage.Script.Gurmukhi,
                EarsLanguage.Script.Arabic
              };
            }
          }
          else
          {
            if (!(code == "ky"))
            {
              goto IL_08C6;
            }
            goto IL_08A6;
          }
        }
        else if (num <= 1514352469U)
        {
          if (num != 1479958588U)
          {
            if (num != 1480252778U)
            {
              if (num != 1514352469U)
              {
                goto IL_08C6;
              }
              if (!(code == "ug"))
              {
                goto IL_08C6;
              }
            }
            else
            {
              if (!(code == "hy"))
              {
                goto IL_08C6;
              }
              return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Armenian };
            }
          }
          else
          {
            if (!(code == "ne"))
            {
              goto IL_08C6;
            }
            goto IL_070E;
          }
        }
        else if (num != 1547363254U)
        {
          if (num != 1547510349U)
          {
            if (num != 1562713850U)
            {
              goto IL_08C6;
            }
            if (!(code == "ar"))
            {
              goto IL_08C6;
            }
          }
          else if (!(code == "ku"))
          {
            goto IL_08C6;
          }
        }
        else
        {
          if (!(code == "he"))
          {
            goto IL_08C6;
          }
          goto IL_06E8;
        }
      }
      else if (num <= 1596857468U)
      {
        if (num <= 1580079849U)
        {
          if (num != 1563699588U)
          {
            if (num != 1579491469U)
            {
              if (num != 1580079849U)
              {
                goto IL_08C6;
              }
              if (!(code == "mk"))
              {
                goto IL_08C6;
              }
              goto IL_08A6;
            }
            else
            {
              if (!(code == "as"))
              {
                goto IL_08C6;
              }
              goto IL_0721;
            }
          }
          else
          {
            if (!(code == "or"))
            {
              goto IL_08C6;
            }
            return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Oriya };
          }
        }
        else if (num != 1581462945U)
        {
          if (num != 1582198420U)
          {
            if (num != 1596857468U)
            {
              goto IL_08C6;
            }
            if (!(code == "ml"))
            {
              goto IL_08C6;
            }
            return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Malayalam };
          }
          else if (!(code == "ps"))
          {
            goto IL_08C6;
          }
        }
        else
        {
          if (!(code == "uk"))
          {
            goto IL_08C6;
          }
          goto IL_08A6;
        }
      }
      else if (num <= 1683996967U)
      {
        if (num != 1630412706U)
        {
          if (num != 1680010088U)
          {
            if (num != 1683996967U)
            {
              goto IL_08C6;
            }
            if (!(code == "yi"))
            {
              goto IL_08C6;
            }
            goto IL_06E8;
          }
          else if (!(code == "fa"))
          {
            goto IL_08C6;
          }
        }
        else
        {
          if (!(code == "mn"))
          {
            goto IL_08C6;
          }
          goto IL_08A6;
        }
      }
      else if (num != 1685604546U)
      {
        if (num != 1748694682U)
        {
          if (num != 1816099348U)
          {
            goto IL_08C6;
          }
          if (!(code == "ja"))
          {
            goto IL_08C6;
          }
          return new HashSet<EarsLanguage.Script>
          {
            EarsLanguage.Script.Kana,
            EarsLanguage.Script.Han
          };
        }
        else
        {
          if (!(code == "hi"))
          {
            goto IL_08C6;
          }
          goto IL_070E;
        }
      }
      else
      {
        if (!(code == "yue"))
        {
          goto IL_08C6;
        }
        goto IL_0895;
      }
      return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Arabic };
      IL_06E8:
      return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Hebrew };
      IL_070E:
      return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Devanagari };
      IL_0721:
      return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Bengali };
      IL_0895:
      return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Han };
      IL_08A6:
      return new HashSet<EarsLanguage.Script> { EarsLanguage.Script.Cyrillic };
      IL_08C6:
      return new HashSet<EarsLanguage.Script>();
    }

    // Token: 0x0600070F RID: 1807 RVA: 0x00361A48 File Offset: 0x00361A48
    public static EarsLanguage.Script? ScriptOf(Rune rune)
    {
      UnicodeCategory unicodeCategory = Rune.GetUnicodeCategory(rune);
      bool flag = Rune.IsLetter(rune);
      if (!flag)
      {
        bool flag2 = unicodeCategory - UnicodeCategory.NonSpacingMark <= 1;
        flag = flag2;
      }
      if (!flag)
      {
        return null;
      }
      int value = rune.Value;
      if (value > 2815)
      {
        if (value >= 7680)
        {
          if (value >= 19968)
          {
            if (value >= 64336)
            {
              if (value >= 65136)
              {
                if (value > 65279)
                {
                  goto IL_03D0;
                }
                goto IL_02E8;
              }
              else
              {
                if (value > 65023)
                {
                  goto IL_03D0;
                }
                goto IL_02E8;
              }
            }
            else if (value >= 44032)
            {
              if (value > 55215)
              {
                goto IL_03D0;
              }
              goto IL_03B2;
            }
            else if (value > 40959)
            {
              goto IL_03D0;
            }
          }
          else if (value >= 12592)
          {
            if (value >= 13312)
            {
              if (value > 19903)
              {
                goto IL_03D0;
              }
            }
            else
            {
              if (value > 12687)
              {
                goto IL_03D0;
              }
              goto IL_03B2;
            }
          }
          else if (value >= 12352)
          {
            if (value > 12543)
            {
              goto IL_03D0;
            }
            return new EarsLanguage.Script?(EarsLanguage.Script.Kana);
          }
          else
          {
            if (value > 7935)
            {
              goto IL_03D0;
            }
            goto IL_02AB;
          }
          return new EarsLanguage.Script?(EarsLanguage.Script.Han);
        }
        if (value <= 3839)
        {
          if (value <= 3327)
          {
            if (value <= 3071)
            {
              if (value > 2943)
              {
                return new EarsLanguage.Script?(EarsLanguage.Script.Tamil);
              }
              return new EarsLanguage.Script?(EarsLanguage.Script.Oriya);
            }
            else
            {
              if (value > 3199)
              {
                return new EarsLanguage.Script?(EarsLanguage.Script.Kannada);
              }
              return new EarsLanguage.Script?(EarsLanguage.Script.Telugu);
            }
          }
          else if (value <= 3583)
          {
            if (value > 3455)
            {
              return new EarsLanguage.Script?(EarsLanguage.Script.Sinhala);
            }
            return new EarsLanguage.Script?(EarsLanguage.Script.Malayalam);
          }
          else
          {
            if (value > 3711)
            {
              return new EarsLanguage.Script?(EarsLanguage.Script.Lao);
            }
            return new EarsLanguage.Script?(EarsLanguage.Script.Thai);
          }
        }
        else if (value >= 4608)
        {
          if (value >= 6016)
          {
            if (value > 6143)
            {
              goto IL_03D0;
            }
            return new EarsLanguage.Script?(EarsLanguage.Script.Khmer);
          }
          else
          {
            if (value > 5023)
            {
              goto IL_03D0;
            }
            return new EarsLanguage.Script?(EarsLanguage.Script.Ethiopic);
          }
        }
        else if (value <= 4255)
        {
          if (value < 4096)
          {
            goto IL_03D0;
          }
          return new EarsLanguage.Script?(EarsLanguage.Script.Myanmar);
        }
        else if (value <= 4351)
        {
          return new EarsLanguage.Script?(EarsLanguage.Script.Georgian);
        }
        IL_03B2:
        return new EarsLanguage.Script?(EarsLanguage.Script.Hangul);
      }
      if (value >= 1872)
      {
        if (value <= 2303)
        {
          if (value <= 1983)
          {
            if (value > 1919)
            {
              return new EarsLanguage.Script?(EarsLanguage.Script.Thaana);
            }
            goto IL_02E8;
          }
          else
          {
            if (value < 2208)
            {
              goto IL_03D0;
            }
            goto IL_02E8;
          }
        }
        else if (value <= 2559)
        {
          if (value > 2431)
          {
            return new EarsLanguage.Script?(EarsLanguage.Script.Bengali);
          }
          return new EarsLanguage.Script?(EarsLanguage.Script.Devanagari);
        }
        else
        {
          if (value > 2687)
          {
            return new EarsLanguage.Script?(EarsLanguage.Script.Gujarati);
          }
          return new EarsLanguage.Script?(EarsLanguage.Script.Gurmukhi);
        }
      }
      else if (value <= 1327)
      {
        if (value >= 880)
        {
          if (value > 1023)
          {
            return new EarsLanguage.Script?(EarsLanguage.Script.Cyrillic);
          }
          return new EarsLanguage.Script?(EarsLanguage.Script.Greek);
        }
        else
        {
          if (value < 65)
          {
            goto IL_03D0;
          }
          if (value > 591)
          {
            goto IL_03D0;
          }
        }
      }
      else if (value <= 1535)
      {
        if (value > 1423)
        {
          return new EarsLanguage.Script?(EarsLanguage.Script.Hebrew);
        }
        return new EarsLanguage.Script?(EarsLanguage.Script.Armenian);
      }
      else
      {
        if (value > 1791)
        {
          goto IL_03D0;
        }
        goto IL_02E8;
      }
      IL_02AB:
      return new EarsLanguage.Script?(EarsLanguage.Script.Latin);
      IL_02E8:
      return new EarsLanguage.Script?(EarsLanguage.Script.Arabic);
      IL_03D0:
      return null;
    }

    // Token: 0x06000710 RID: 1808 RVA: 0x00361E34 File Offset: 0x00361E34
    private static string[] Tokens(string words)
    {
      return words.Split(null, StringSplitOptions.RemoveEmptyEntries);
    }

    // Token: 0x06000711 RID: 1809 RVA: 0x00361E40 File Offset: 0x00361E40
    public static HashSet<EarsLanguage.Script> ScriptsIn(string token)
    {
      HashSet<EarsLanguage.Script> hashSet = new HashSet<EarsLanguage.Script>();
      foreach (Rune rune in token.EnumerateRunes())
      {
        EarsLanguage.Script? script = EarsLanguage.ScriptOf(rune);
        if (script != null)
        {
          EarsLanguage.Script valueOrDefault = script.GetValueOrDefault();
          hashSet.Add(valueOrDefault);
        }
      }
      return hashSet;
    }

    // Token: 0x06000712 RID: 1810 RVA: 0x00361EB8 File Offset: 0x00361EB8
    public static int WordCount(string token)
    {
      int num = 0;
      foreach (Rune rune in token.EnumerateRunes())
      {
        EarsLanguage.Script? script = EarsLanguage.ScriptOf(rune);
        if (script != null)
        {
          EarsLanguage.Script valueOrDefault = script.GetValueOrDefault();
          if (EarsLanguage.ScriptsWrittenWithoutSpaces.Contains(valueOrDefault))
          {
            num++;
          }
        }
      }
      if (num != 0)
      {
        return Math.Max(1, (num + 1) / 2);
      }
      return 1;
    }

    // Token: 0x06000713 RID: 1811 RVA: 0x00361F44 File Offset: 0x00361F44
    [return: Nullable(2)]
    public static string GuardingNoise(string words, IReadOnlySet<EarsLanguage.Script> acceptedScripts, DateTime? now = null)
    {
      DateTime dateTime = now ?? DateTime.UtcNow;
      string text = EarsLanguage.RemovingStrayScript(words, acceptedScripts);
      HashSet<EarsLanguage.Script> hashSet = null;
      object obj = EarsLanguage.droppedGate;
      lock (obj)
      {
        if (text != null)
        {
          EarsLanguage.lastDroppedPress = null;
          return text;
        }
        HashSet<EarsLanguage.Script> hashSet2 = EarsLanguage.ScriptsIn(words);
        hashSet2.ExceptWith(acceptedScripts);
        hashSet2.Remove(EarsLanguage.Script.Latin);
        ValueTuple<HashSet<EarsLanguage.Script>, DateTime>? valueTuple = EarsLanguage.lastDroppedPress;
        if (valueTuple != null)
        {
          ValueTuple<HashSet<EarsLanguage.Script>, DateTime> valueOrDefault = valueTuple.GetValueOrDefault();
          if (dateTime - valueOrDefault.Item2 <= EarsLanguage.RepeatWindow && valueOrDefault.Item1.Overlaps(hashSet2))
          {
            EarsLanguage.lastDroppedPress = null;
            hashSet = valueOrDefault.Item1;
            goto IL_00E0;
          }
        }
        EarsLanguage.lastDroppedPress = new ValueTuple<HashSet<EarsLanguage.Script>, DateTime>?(new ValueTuple<HashSet<EarsLanguage.Script>, DateTime>(hashSet2, dateTime));
        return null;
      }
      IL_00E0:
      EarsLanguage.RecordHeardScripts(hashSet);
      return words;
    }

    // Token: 0x06000714 RID: 1812 RVA: 0x0036204C File Offset: 0x0036204C
    internal static void ForgetDroppedPress()
    {
      object obj = EarsLanguage.droppedGate;
      lock (obj)
      {
        EarsLanguage.lastDroppedPress = null;
      }
    }

    // Token: 0x06000715 RID: 1813 RVA: 0x00362090 File Offset: 0x00362090
    [return: Nullable(2)]
    public static string RemovingStrayScript(string words, IEnumerable<string> knownLanguageCodes)
    {
      HashSet<EarsLanguage.Script> hashSet = new HashSet<EarsLanguage.Script>();
      foreach (string text in knownLanguageCodes)
      {
        hashSet.UnionWith(EarsLanguage.Scripts(text));
      }
      return EarsLanguage.RemovingStrayScript(words, hashSet);
    }

    // Token: 0x06000716 RID: 1814 RVA: 0x003620EC File Offset: 0x003620EC
    [return: Nullable(2)]
    public static string RemovingStrayScript(string words, IReadOnlySet<EarsLanguage.Script> acceptedScripts)
    {
      EarsLanguage.<>c__DisplayClass30_0 CS$<>8__locals1 = new EarsLanguage.<>c__DisplayClass30_0();
      CS$<>8__locals1.accepted = new HashSet<EarsLanguage.Script>(acceptedScripts) { EarsLanguage.Script.Latin };
      List<string> list = EarsLanguage.Tokens(words).ToList<string>();
      int num = list.Count<string>(new Func<string, bool>(CS$<>8__locals1.<RemovingStrayScript>g__IsStray|0));
      if (num == 0)
      {
        return words;
      }
      if (!list.Any<string>((string t) => EarsLanguage.<RemovingStrayScript>g__HasLetters|30_1(t) && !base.<RemovingStrayScript>g__IsStray|0(t)))
      {
        IEnumerable<string> enumerable = list.Where<string>(new Func<string, bool>(CS$<>8__locals1.<RemovingStrayScript>g__IsStray|0));
        Func<string, int> func;
        if ((func = EarsLanguage.<>O.<0>__WordCount) == null)
        {
          func = (EarsLanguage.<>O.<0>__WordCount = new Func<string, int>(EarsLanguage.WordCount));
        }
        if (enumerable.Sum<string>(func) > 6)
        {
          return words;
        }
        return null;
      }
      else
      {
        int num2 = 0;
        while (list.Count > 0)
        {
          if (!CS$<>8__locals1.<RemovingStrayScript>g__IsStray|0(list[0]))
          {
            break;
          }
          list.RemoveAt(0);
          num2++;
        }
        while (list.Count > 0)
        {
          EarsLanguage.<>c__DisplayClass30_0 CS$<>8__locals2 = CS$<>8__locals1;
          List<string> list2 = list;
          if (!CS$<>8__locals2.<RemovingStrayScript>g__IsStray|0(list2[list2.Count - 1]))
          {
            break;
          }
          list.RemoveAt(list.Count - 1);
          num2++;
        }
        if (num2 != num || num2 > 2)
        {
          return words;
        }
        return string.Join(" ", list);
      }
    }

    // Token: 0x06000718 RID: 1816 RVA: 0x0036227C File Offset: 0x0036227C
    [CompilerGenerated]
    internal static bool <RemovingStrayScript>g__HasLetters|30_1(string token)
    {
      return EarsLanguage.ScriptsIn(token).Count > 0;
    }

    // Token: 0x04000340 RID: 832
    public const int HeardScriptsKept = 30;

    // Token: 0x04000341 RID: 833
    public const int HistoryNeededForNoiseGuard = 10;

    // Token: 0x04000342 RID: 834
    public const int TurnsNeededToAcceptAScript = 2;

    // Token: 0x04000343 RID: 835
    private static readonly object storeGate = new object();

    // Token: 0x04000345 RID: 837
    [Nullable(new byte[] { 2, 1, 1 })]
    private static List<List<string>> cachedHistory;

    // Token: 0x04000346 RID: 838
    private static readonly HashSet<EarsLanguage.Script> ScriptsWrittenWithoutSpaces = new HashSet<EarsLanguage.Script>
    {
      EarsLanguage.Script.Han,
      EarsLanguage.Script.Kana,
      EarsLanguage.Script.Thai,
      EarsLanguage.Script.Lao,
      EarsLanguage.Script.Khmer,
      EarsLanguage.Script.Myanmar
    };

    // Token: 0x04000347 RID: 839
    private static readonly object droppedGate = new object();

    // Token: 0x04000348 RID: 840
    [TupleElementNames(new string[] { "Scripts", "At" })]
    [Nullable(new byte[] { 0, 1 })]
    private static ValueTuple<HashSet<EarsLanguage.Script>, DateTime>? lastDroppedPress;

    // Token: 0x04000349 RID: 841
    public static readonly TimeSpan RepeatWindow = TimeSpan.FromSeconds(180L);

    // Token: 0x0200031F RID: 799
    [NullableContext(0)]
    public enum Script
    {
      // Token: 0x04001629 RID: 5673
      Latin,
      // Token: 0x0400162A RID: 5674
      Cyrillic,
      // Token: 0x0400162B RID: 5675
      Greek,
      // Token: 0x0400162C RID: 5676
      Arabic,
      // Token: 0x0400162D RID: 5677
      Hebrew,
      // Token: 0x0400162E RID: 5678
      Thaana,
      // Token: 0x0400162F RID: 5679
      Devanagari,
      // Token: 0x04001630 RID: 5680
      Bengali,
      // Token: 0x04001631 RID: 5681
      Gurmukhi,
      // Token: 0x04001632 RID: 5682
      Gujarati,
      // Token: 0x04001633 RID: 5683
      Oriya,
      // Token: 0x04001634 RID: 5684
      Tamil,
      // Token: 0x04001635 RID: 5685
      Telugu,
      // Token: 0x04001636 RID: 5686
      Kannada,
      // Token: 0x04001637 RID: 5687
      Malayalam,
      // Token: 0x04001638 RID: 5688
      Sinhala,
      // Token: 0x04001639 RID: 5689
      Thai,
      // Token: 0x0400163A RID: 5690
      Lao,
      // Token: 0x0400163B RID: 5691
      Khmer,
      // Token: 0x0400163C RID: 5692
      Myanmar,
      // Token: 0x0400163D RID: 5693
      Georgian,
      // Token: 0x0400163E RID: 5694
      Armenian,
      // Token: 0x0400163F RID: 5695
      Ethiopic,
      // Token: 0x04001640 RID: 5696
      Hangul,
      // Token: 0x04001641 RID: 5697
      Kana,
      // Token: 0x04001642 RID: 5698
      Han
    }

    // Token: 0x02000320 RID: 800
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001643 RID: 5699
      [Nullable(0)]
      public static Func<string, int> <0>__WordCount;
    }
  }
}
