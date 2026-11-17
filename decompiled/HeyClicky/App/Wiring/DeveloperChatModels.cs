using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Settings;

namespace HeyClicky.App.Wiring
{
  // Token: 0x02000097 RID: 151
  [NullableContext(1)]
  [Nullable(0)]
  public static class DeveloperChatModels
  {
    // Token: 0x06000613 RID: 1555 RVA: 0x0052EF74 File Offset: 0x0052EF74
    private static IReadOnlyList<string> EffortsFor(string modelId)
    {
      if (!(modelId == "gpt-6-astra") && !(modelId == "gpt-6.1-sol"))
      {
        return DeveloperChatModels.AllEfforts;
      }
      return DeveloperChatModels.AllEfforts.Where<string>((string e) => e != "none").ToArray<string>();
    }

    // Token: 0x06000614 RID: 1556 RVA: 0x0052EFD8 File Offset: 0x0052EFD8
    private static string Key(string email, string slug)
    {
      return email.Trim().ToLowerInvariant() + "|" + slug;
    }

    // Token: 0x06000615 RID: 1557 RVA: 0x0052EFF0 File Offset: 0x0052EFF0
    [return: TupleElementNames(new string[] { "Model", "Effort" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    public static ValueTuple<string, string>? Load(SettingsStore store, string email, string slug)
    {
      string text = store.Read<string>(delegate([Nullable(1)] AppSettings s)
      {
        Dictionary<string, string> developerChatModels = s.DeveloperChatModels;
        if (developerChatModels == null)
        {
          return null;
        }
        return developerChatModels.GetValueOrDefault(DeveloperChatModels.Key(email, slug));
      });
      if (text == null)
      {
        return null;
      }
      int num = text.IndexOf('|');
      string text5;
      string text6;
      if (num >= 0)
      {
        string text2 = text.Substring(0, num);
        string text3 = text;
        int num2 = num + 1;
        string text4 = text3.Substring(num2, text3.Length - num2);
        text5 = text4;
        text6 = text2;
      }
      else
      {
        string text7 = text;
        text5 = "high";
        text6 = text7;
      }
      if (!DeveloperChatModels.ModelIds.Contains(text6))
      {
        return null;
      }
      return new ValueTuple<string, string>?(new ValueTuple<string, string>(text6, DeveloperChatModels.EffortsFor(text6).Contains(text5) ? text5 : "low"));
    }

    // Token: 0x0400014C RID: 332
    private static readonly string[] ModelIds = new string[] { "gpt-6.1-sol", "gpt-6-sol", "gpt-6-luna", "gpt-5.6-luna", "gpt-5.6-terra", "gpt-5.6-sol", "gpt-6-astra" };

    // Token: 0x0400014D RID: 333
    private static readonly string[] AllEfforts = new string[] { "none", "low", "medium", "high", "xhigh", "max" };
  }
}
