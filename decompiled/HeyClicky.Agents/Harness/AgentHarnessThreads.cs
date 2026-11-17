using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Agents.Harness
{
  // Token: 0x0200005C RID: 92
  [NullableContext(1)]
  [Nullable(0)]
  public static class AgentHarnessThreads
  {
    // Token: 0x06000784 RID: 1924 RVA: 0x000FD210 File Offset: 0x000FD210
    private static string MapKey(AgentHarness harness, AgentHarnessPlace place, string appThreadId)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 4);
      defaultInterpolatedStringHandler.AppendFormatted("ClickyAgentHarnessThreadMap.");
      defaultInterpolatedStringHandler.AppendFormatted(harness.RawValue());
      defaultInterpolatedStringHandler.AppendLiteral("-");
      defaultInterpolatedStringHandler.AppendFormatted(place.RawValue());
      defaultInterpolatedStringHandler.AppendLiteral(".");
      defaultInterpolatedStringHandler.AppendFormatted(appThreadId);
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06000785 RID: 1925 RVA: 0x000FD274 File Offset: 0x000FD274
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "Harness", "Place" })]
    public static ValueTuple<AgentHarness, AgentHarnessPlace>? TakenOverBy([Nullable(1)] string appThreadId)
    {
      ValueTuple<AgentHarness, AgentHarnessPlace>? valueTuple = AgentHarnessPreferences.RuntimeChoice(appThreadId);
      if (valueTuple != null)
      {
        valueTuple = null;
        return valueTuple;
      }
      foreach (AgentHarness agentHarness in Enum.GetValues<AgentHarness>())
      {
        foreach (AgentHarnessPlace agentHarnessPlace in agentHarness.Places())
        {
          if (AgentDefaults.String(AgentHarnessThreads.MapKey(agentHarness, agentHarnessPlace, appThreadId)) != null)
          {
            return new ValueTuple<AgentHarness, AgentHarnessPlace>?(new ValueTuple<AgentHarness, AgentHarnessPlace>(agentHarness, agentHarnessPlace));
          }
        }
      }
      return null;
    }

    // Token: 0x06000786 RID: 1926 RVA: 0x000FD320 File Offset: 0x000FD320
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "Harness", "Place" })]
    public static ValueTuple<AgentHarness, AgentHarnessPlace> Choice([Nullable(2)] string appThreadId)
    {
      if (appThreadId != null)
      {
        ValueTuple<AgentHarness, AgentHarnessPlace>? valueTuple = AgentHarnessPreferences.RuntimeChoice(appThreadId);
        if (valueTuple != null)
        {
          return valueTuple.GetValueOrDefault();
        }
        valueTuple = AgentHarnessThreads.TakenOverBy(appThreadId);
        if (valueTuple != null)
        {
          return valueTuple.GetValueOrDefault();
        }
      }
      return new ValueTuple<AgentHarness, AgentHarnessPlace>(AgentHarness.H1, AgentHarnessPlace.Mac);
    }

    // Token: 0x06000787 RID: 1927 RVA: 0x000FD36C File Offset: 0x000FD36C
    public static string HarnessThreadId(string appThreadId, AgentHarness harness, AgentHarnessPlace place)
    {
      if (AgentHarnessPreferences.RuntimeChoice(appThreadId) != null)
      {
        return appThreadId;
      }
      string text = AgentHarnessThreads.MapKey(harness, place, appThreadId);
      string text2 = AgentDefaults.String(text);
      if (text2 != null)
      {
        return text2;
      }
      string text3 = AgentHarnessPreferences.ThreadIdPrefix(harness, place) + Guid.NewGuid().ToString("D").ToLowerInvariant();
      AgentDefaults.Set(text, text3);
      return text3;
    }

    // Token: 0x06000788 RID: 1928 RVA: 0x000FD3D0 File Offset: 0x000FD3D0
    [return: Nullable(2)]
    public static string ExistingHarnessThreadId(string appThreadId)
    {
      ValueTuple<AgentHarness, AgentHarnessPlace>? valueTuple = AgentHarnessThreads.TakenOverBy(appThreadId);
      if (valueTuple != null)
      {
        ValueTuple<AgentHarness, AgentHarnessPlace> valueOrDefault = valueTuple.GetValueOrDefault();
        return AgentDefaults.String(AgentHarnessThreads.MapKey(valueOrDefault.Item1, valueOrDefault.Item2, appThreadId));
      }
      return null;
    }

    // Token: 0x06000789 RID: 1929 RVA: 0x000FD410 File Offset: 0x000FD410
    public static bool ContinuesOnNewConversationHarness(string appThreadId)
    {
      if (AgentHarnessPreferences.IsRetiredHarnessThread(appThreadId))
      {
        return false;
      }
      ValueTuple<AgentHarness, AgentHarnessPlace>? valueTuple = AgentHarnessPreferences.RuntimeChoice(appThreadId);
      ValueTuple<AgentHarness, AgentHarnessPlace>? valueTuple2 = ((valueTuple != null) ? valueTuple : AgentHarnessThreads.TakenOverBy(appThreadId));
      if (valueTuple2 != null)
      {
        ValueTuple<AgentHarness, AgentHarnessPlace> valueOrDefault = valueTuple2.GetValueOrDefault();
        ValueTuple<AgentHarness, AgentHarnessPlace> valueTuple3 = valueOrDefault;
        ValueTuple<AgentHarness, AgentHarnessPlace> valueTuple4 = AgentHarnessThreads.Choice(null);
        return valueTuple3.Item1 == valueTuple4.Item1 && valueTuple3.Item2 == valueTuple4.Item2;
      }
      return false;
    }

    // Token: 0x040002CC RID: 716
    private const string ThreadMapPrefix = "ClickyAgentHarnessThreadMap.";
  }
}
