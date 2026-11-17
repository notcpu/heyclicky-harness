using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using HeyClicky.Agents;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001BA RID: 442
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyNotePaperColors
  {
    // Token: 0x06001762 RID: 5986 RVA: 0x005C0474 File Offset: 0x005C0474
    private static Dictionary<string, string> Load()
    {
      Dictionary<string, string> dictionary = AgentDefaults.Get<Dictionary<string, string>>("clicky.notePaperColors.v1") ?? new Dictionary<string, string>();
      if (ClickyNotePaperColors.legacyFolded)
      {
        return dictionary;
      }
      ClickyNotePaperColors.legacyFolded = true;
      try
      {
        if (!File.Exists(ClickyNotePaperColors.LegacyFilePath))
        {
          return dictionary;
        }
        Dictionary<string, string> dictionary2 = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(ClickyNotePaperColors.LegacyFilePath), null) ?? new Dictionary<string, string>();
        int count = dictionary.Count;
        foreach (KeyValuePair<string, string> keyValuePair in dictionary2)
        {
          string text;
          string text2;
          keyValuePair.Deconstruct(out text, out text2);
          string text3 = text;
          string text4 = text2;
          dictionary.TryAdd(text3, text4);
        }
        if (dictionary.Count != count)
        {
          ClickyNotePaperColors.Save(dictionary);
        }
        File.Delete(ClickyNotePaperColors.LegacyFilePath);
      }
      catch (Exception ex)
      {
        Log.Warn("ink", "old note paper colours not folded in: " + ex.Message);
      }
      return dictionary;
    }

    // Token: 0x06001763 RID: 5987 RVA: 0x005C0578 File Offset: 0x005C0578
    private static void Save(Dictionary<string, string> assignments)
    {
      AgentDefaults.Set<Dictionary<string, string>>("clicky.notePaperColors.v1", assignments);
    }

    // Token: 0x06001764 RID: 5988 RVA: 0x005C0588 File Offset: 0x005C0588
    public static void Adopt(string key, string sourceKey)
    {
      using (ClickyNotePaperColors.Gate.EnterScope())
      {
        Dictionary<string, string> dictionary = ClickyNotePaperColors.Load();
        string text;
        if (dictionary.TryGetValue(sourceKey, out text))
        {
          dictionary[key] = text;
          ClickyNotePaperColors.Save(dictionary);
        }
      }
    }

    // Token: 0x06001765 RID: 5989 RVA: 0x005C05DC File Offset: 0x005C05DC
    public static ClickyPaperStock Stock(string key)
    {
      ClickyPaperStock clickyPaperStock4;
      using (ClickyNotePaperColors.Gate.EnterScope())
      {
        Dictionary<string, string> dictionary = ClickyNotePaperColors.Load();
        string text;
        if (dictionary.TryGetValue(key, out text))
        {
          ClickyPaperStock? clickyPaperStock = ClickyPaperStockStyle.FromRawValue(text);
          if (clickyPaperStock != null)
          {
            return clickyPaperStock.GetValueOrDefault();
          }
        }
        Dictionary<ClickyPaperStock, int> useCounts = ClickyNotePaperColors.Choices.ToDictionary<ClickyPaperStock, ClickyPaperStock, int>((ClickyPaperStock c) => c, (ClickyPaperStock _) => 0);
        foreach (string text2 in dictionary.Values)
        {
          ClickyPaperStock? clickyPaperStock = ClickyPaperStockStyle.FromRawValue(text2);
          if (clickyPaperStock != null)
          {
            ClickyPaperStock valueOrDefault = clickyPaperStock.GetValueOrDefault();
            if (useCounts.ContainsKey(valueOrDefault))
            {
              Dictionary<ClickyPaperStock, int> useCounts2 = useCounts;
              ClickyPaperStock clickyPaperStock2 = valueOrDefault;
              int num = useCounts2[clickyPaperStock2];
              useCounts2[clickyPaperStock2] = num + 1;
            }
          }
        }
        int fewest = useCounts.Values.DefaultIfEmpty(0).Min();
        ClickyPaperStock[] array = ClickyNotePaperColors.Choices.Where<ClickyPaperStock>((ClickyPaperStock c) => useCounts[c] == fewest).ToArray<ClickyPaperStock>();
        ClickyPaperStock clickyPaperStock3 = ((array.Length != 0) ? array[Random.Shared.Next(array.Length)] : ClickyPaperStock.Sky);
        dictionary[key] = clickyPaperStock3.RawValue();
        ClickyNotePaperColors.Save(dictionary);
        clickyPaperStock4 = clickyPaperStock3;
      }
      return clickyPaperStock4;
    }

    // Token: 0x04000B5D RID: 2909
    public static readonly ClickyPaperStock[] Choices = new ClickyPaperStock[]
    {
      ClickyPaperStock.Sky,
      ClickyPaperStock.Blush,
      ClickyPaperStock.Butter,
      ClickyPaperStock.Mint,
      ClickyPaperStock.Periwinkle,
      ClickyPaperStock.Peach,
      ClickyPaperStock.Kraft
    };

    // Token: 0x04000B5E RID: 2910
    private const string DefaultsKey = "clicky.notePaperColors.v1";

    // Token: 0x04000B5F RID: 2911
    private static readonly string LegacyFilePath = AppPaths.Combine(new string[] { "note-paper-colors.json" });

    // Token: 0x04000B60 RID: 2912
    private static readonly Lock Gate = new Lock();

    // Token: 0x04000B61 RID: 2913
    private static bool legacyFolded;
  }
}
