using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace HeyClicky.Agents.Turns
{
  // Token: 0x02000019 RID: 25
  [NullableContext(1)]
  [Nullable(0)]
  internal static class StandingContextLedger
  {
    // Token: 0x0600033E RID: 830 RVA: 0x000E49D4 File Offset: 0x000E49D4
    private static string Fingerprint(string text)
    {
      return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(text))).ToLowerInvariant();
    }

    // Token: 0x0600033F RID: 831 RVA: 0x000E49F0 File Offset: 0x000E49F0
    private static string Key(StandingContextLedger.Section section)
    {
      string text;
      switch (section)
      {
      case StandingContextLedger.Section.UserMemory:
        text = "userMemory";
        break;
      case StandingContextLedger.Section.AgentInstructions:
        text = "agentInstructions";
        break;
      case StandingContextLedger.Section.ActiveSkills:
        text = "activeSkills";
        break;
      default:
        text = "learnedHowTos";
        break;
      }
      return text;
    }

    // Token: 0x06000340 RID: 832 RVA: 0x000E4A30 File Offset: 0x000E4A30
    [NullableContext(2)]
    public static bool NeedsCarrying(StandingContextLedger.Section section, string text, string threadId)
    {
      if (string.IsNullOrEmpty(text))
      {
        return false;
      }
      if (threadId == null)
      {
        return true;
      }
      object gate = StandingContextLedger.Gate;
      bool flag2;
      lock (gate)
      {
        Dictionary<string, string> dictionary;
        string text2;
        flag2 = !(AgentDefaults.Get<StandingContextLedger.Ledger>("clicky.codexAgent.standingContextCarried.v1") ?? new StandingContextLedger.Ledger()).FingerprintsByThreadId.TryGetValue(threadId, out dictionary) || !dictionary.TryGetValue(StandingContextLedger.Key(section), out text2) || !(text2 == StandingContextLedger.Fingerprint(text));
      }
      return flag2;
    }

    // Token: 0x06000341 RID: 833 RVA: 0x000E4AC0 File Offset: 0x000E4AC0
    public static void RecordCarried(IReadOnlyDictionary<StandingContextLedger.Section, string> carried, string threadId)
    {
      if (carried.Count == 0)
      {
        return;
      }
      object gate = StandingContextLedger.Gate;
      lock (gate)
      {
        StandingContextLedger.Ledger ledger = AgentDefaults.Get<StandingContextLedger.Ledger>("clicky.codexAgent.standingContextCarried.v1") ?? new StandingContextLedger.Ledger();
        Dictionary<string, string> dictionary;
        if (!ledger.FingerprintsByThreadId.TryGetValue(threadId, out dictionary))
        {
          dictionary = (ledger.FingerprintsByThreadId[threadId] = new Dictionary<string, string>());
        }
        foreach (KeyValuePair<StandingContextLedger.Section, string> keyValuePair in carried)
        {
          StandingContextLedger.Section section;
          string text;
          keyValuePair.Deconstruct(out section, out text);
          StandingContextLedger.Section section2 = section;
          string text2 = text;
          dictionary[StandingContextLedger.Key(section2)] = StandingContextLedger.Fingerprint(text2);
        }
        ledger.ThreadOrder.Remove(threadId);
        ledger.ThreadOrder.Add(threadId);
        while (ledger.ThreadOrder.Count > 2000)
        {
          ledger.FingerprintsByThreadId.Remove(ledger.ThreadOrder[0]);
          ledger.ThreadOrder.RemoveAt(0);
        }
        AgentDefaults.Set<StandingContextLedger.Ledger>("clicky.codexAgent.standingContextCarried.v1", ledger);
      }
    }

    // Token: 0x06000342 RID: 834 RVA: 0x000E4BF4 File Offset: 0x000E4BF4
    public static void ForgetThread(string threadId)
    {
      object gate = StandingContextLedger.Gate;
      lock (gate)
      {
        StandingContextLedger.Ledger ledger = AgentDefaults.Get<StandingContextLedger.Ledger>("clicky.codexAgent.standingContextCarried.v1") ?? new StandingContextLedger.Ledger();
        if (ledger.FingerprintsByThreadId.Remove(threadId))
        {
          ledger.ThreadOrder.Remove(threadId);
          AgentDefaults.Set<StandingContextLedger.Ledger>("clicky.codexAgent.standingContextCarried.v1", ledger);
        }
      }
    }

    // Token: 0x04000136 RID: 310
    private const string DefaultsKey = "clicky.codexAgent.standingContextCarried.v1";

    // Token: 0x04000137 RID: 311
    private const int MaximumRememberedThreadCount = 2000;

    // Token: 0x04000138 RID: 312
    private static readonly object Gate = new object();

    // Token: 0x020000F7 RID: 247
    [NullableContext(0)]
    public enum Section
    {
      // Token: 0x04000581 RID: 1409
      UserMemory,
      // Token: 0x04000582 RID: 1410
      AgentInstructions,
      // Token: 0x04000583 RID: 1411
      ActiveSkills,
      // Token: 0x04000584 RID: 1412
      LearnedHowTos
    }

    // Token: 0x020000F8 RID: 248
    [Nullable(0)]
    private sealed class Ledger
    {
      // Token: 0x17000273 RID: 627
      // (get) Token: 0x06000AFF RID: 2815 RVA: 0x0010DBA8 File Offset: 0x0010DBA8
      // (set) Token: 0x06000B00 RID: 2816 RVA: 0x0010DBB0 File Offset: 0x0010DBB0
      public Dictionary<string, Dictionary<string, string>> FingerprintsByThreadId { get; set; } = new Dictionary<string, Dictionary<string, string>>();

      // Token: 0x17000274 RID: 628
      // (get) Token: 0x06000B01 RID: 2817 RVA: 0x0010DBBC File Offset: 0x0010DBBC
      // (set) Token: 0x06000B02 RID: 2818 RVA: 0x0010DBC4 File Offset: 0x0010DBC4
      public List<string> ThreadOrder { get; set; } = new List<string>();
    }
  }
}
