using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.Agents.Harness
{
  // Token: 0x02000066 RID: 102
  [NullableContext(1)]
  [Nullable(0)]
  public static class AgentHarnessRuntimes
  {
    // Token: 0x1700020C RID: 524
    // (get) Token: 0x06000823 RID: 2083 RVA: 0x000FF668 File Offset: 0x000FF668
    public static Dictionary<string, Func<IAgentHarnessRuntime>> Factories { get; } = AgentHarnessRuntimes.BuildFactories();

    // Token: 0x06000824 RID: 2084 RVA: 0x000FF670 File Offset: 0x000FF670
    private static Dictionary<string, Func<IAgentHarnessRuntime>> BuildFactories()
    {
      Dictionary<string, Func<IAgentHarnessRuntime>> dictionary = new Dictionary<string, Func<IAgentHarnessRuntime>>();
      AgentHarness[] values = Enum.GetValues<AgentHarness>();
      for (int i = 0; i < values.Length; i++)
      {
        AgentHarness harness = values[i];
        using (IEnumerator<AgentHarnessPlace> enumerator = harness.Places().GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            AgentHarnessPlace place = enumerator.Current;
            if (harness != AgentHarness.Codex || place != AgentHarnessPlace.Mac)
            {
              dictionary[AgentHarnessRuntimes.Key(harness, place)] = () => new AgentHarnessRunnerRuntime(harness, place);
            }
          }
        }
      }
      return dictionary;
    }

    // Token: 0x06000825 RID: 2085 RVA: 0x000FF748 File Offset: 0x000FF748
    public static string Key(AgentHarness harness, AgentHarnessPlace place)
    {
      return harness.RawValue() + "-" + place.RawValue();
    }

    // Token: 0x06000826 RID: 2086 RVA: 0x000FF760 File Offset: 0x000FF760
    [NullableContext(2)]
    public static IAgentHarnessRuntime Runtime(AgentHarness harness, AgentHarnessPlace place)
    {
      string text = AgentHarnessRuntimes.Key(harness, place);
      object gate = AgentHarnessRuntimes.Gate;
      IAgentHarnessRuntime agentHarnessRuntime2;
      lock (gate)
      {
        IAgentHarnessRuntime agentHarnessRuntime;
        Func<IAgentHarnessRuntime> func;
        if (AgentHarnessRuntimes.Instances.TryGetValue(text, out agentHarnessRuntime))
        {
          agentHarnessRuntime2 = agentHarnessRuntime;
        }
        else if (!AgentHarnessRuntimes.Factories.TryGetValue(text, out func))
        {
          agentHarnessRuntime2 = null;
        }
        else
        {
          agentHarnessRuntime = func();
          AgentHarnessRuntimes.Instances[text] = agentHarnessRuntime;
          agentHarnessRuntime2 = agentHarnessRuntime;
        }
      }
      return agentHarnessRuntime2;
    }

    // Token: 0x040002F9 RID: 761
    private static readonly object Gate = new object();

    // Token: 0x040002FA RID: 762
    private static readonly Dictionary<string, IAgentHarnessRuntime> Instances = new Dictionary<string, IAgentHarnessRuntime>();
  }
}
