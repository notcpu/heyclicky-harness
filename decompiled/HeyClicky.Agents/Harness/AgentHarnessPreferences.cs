using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Agents.Harness
{
  // Token: 0x0200005B RID: 91
  [NullableContext(1)]
  [Nullable(0)]
  public static class AgentHarnessPreferences
  {
    // Token: 0x170001DE RID: 478
    // (get) Token: 0x0600077D RID: 1917 RVA: 0x000FD0A8 File Offset: 0x000FD0A8
    public static AgentHarnessModel Model
    {
      get
      {
        return AgentHarnessNames.ParseModel(AgentDefaults.String("ClickyAgentHarnessModel"));
      }
    }

    // Token: 0x170001DF RID: 479
    // (get) Token: 0x0600077E RID: 1918 RVA: 0x000FD0BC File Offset: 0x000FD0BC
    public static bool UsesDeviceHelper
    {
      get
      {
        return !AgentDefaults.Bool("ClickyAgentHarnessDeviceHelperOff");
      }
    }

    // Token: 0x0600077F RID: 1919 RVA: 0x000FD0CC File Offset: 0x000FD0CC
    public static bool UsesNoteTools(AgentHarness harness, AgentHarnessPlace place)
    {
      return harness == AgentHarness.H1 && place == AgentHarnessPlace.Mac && !AgentDefaults.Bool("ClickyAgentHarnessNoteToolsOff");
    }

    // Token: 0x06000780 RID: 1920 RVA: 0x000FD0E4 File Offset: 0x000FD0E4
    public static string ThreadIdPrefix(AgentHarness harness, AgentHarnessPlace place)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 2);
      defaultInterpolatedStringHandler.AppendLiteral("harness-");
      defaultInterpolatedStringHandler.AppendFormatted(harness.RawValue());
      defaultInterpolatedStringHandler.AppendLiteral("-");
      defaultInterpolatedStringHandler.AppendFormatted(place.RawValue());
      defaultInterpolatedStringHandler.AppendLiteral("-");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06000781 RID: 1921 RVA: 0x000FD140 File Offset: 0x000FD140
    public static bool IsRetiredHarnessThread(string threadId)
    {
      return AgentHarnessPreferences.IsHarnessThread(threadId) && AgentHarnessPreferences.RuntimeChoice(threadId) == null;
    }

    // Token: 0x06000782 RID: 1922 RVA: 0x000FD168 File Offset: 0x000FD168
    [NullableContext(2)]
    public static bool IsHarnessThread(string threadId)
    {
      return threadId != null && threadId.StartsWith("harness-", StringComparison.Ordinal);
    }

    // Token: 0x06000783 RID: 1923 RVA: 0x000FD17C File Offset: 0x000FD17C
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "Harness", "Place" })]
    public static ValueTuple<AgentHarness, AgentHarnessPlace>? RuntimeChoice([Nullable(1)] string threadId)
    {
      if (!threadId.StartsWith("harness-", StringComparison.Ordinal))
      {
        return null;
      }
      string[] array = threadId.Split('-', 4, StringSplitOptions.None);
      if (array.Length < 3)
      {
        return null;
      }
      AgentHarness? agentHarness = AgentHarnessNames.ParseHarness(array[1]);
      AgentHarnessPlace? agentHarnessPlace = AgentHarnessNames.ParsePlace(array[2]);
      if (agentHarness != null)
      {
        AgentHarness valueOrDefault = agentHarness.GetValueOrDefault();
        if (agentHarnessPlace != null)
        {
          AgentHarnessPlace valueOrDefault2 = agentHarnessPlace.GetValueOrDefault();
          return new ValueTuple<AgentHarness, AgentHarnessPlace>?(new ValueTuple<AgentHarness, AgentHarnessPlace>(valueOrDefault, valueOrDefault2));
        }
      }
      return null;
    }

    // Token: 0x040002C7 RID: 711
    public const string ModelKey = "ClickyAgentHarnessModel";

    // Token: 0x040002C8 RID: 712
    public const string DeviceHelperOffKey = "ClickyAgentHarnessDeviceHelperOff";

    // Token: 0x040002C9 RID: 713
    public const string NoteToolsOffKey = "ClickyAgentHarnessNoteToolsOff";

    // Token: 0x040002CA RID: 714
    public const AgentHarness Harness = AgentHarness.H1;

    // Token: 0x040002CB RID: 715
    public const AgentHarnessPlace Place = AgentHarnessPlace.Mac;
  }
}
