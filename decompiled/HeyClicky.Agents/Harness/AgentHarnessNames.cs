using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Agents.Harness
{
  // Token: 0x0200005A RID: 90
  [NullableContext(1)]
  [Nullable(0)]
  public static class AgentHarnessNames
  {
    // Token: 0x06000770 RID: 1904 RVA: 0x000FCDE4 File Offset: 0x000FCDE4
    public static string RawValue(this AgentHarness harness)
    {
      string text;
      if (harness != AgentHarness.Codex)
      {
        if (harness != AgentHarness.Pi)
        {
          text = "h1";
        }
        else
        {
          text = "pi";
        }
      }
      else
      {
        text = "codex";
      }
      return text;
    }

    // Token: 0x06000771 RID: 1905 RVA: 0x000FCE14 File Offset: 0x000FCE14
    public static string DisplayName(this AgentHarness harness)
    {
      string text;
      if (harness != AgentHarness.Codex)
      {
        if (harness != AgentHarness.Pi)
        {
          text = "H1";
        }
        else
        {
          text = "Pi";
        }
      }
      else
      {
        text = "Codex";
      }
      return text;
    }

    // Token: 0x06000772 RID: 1906 RVA: 0x000FCE44 File Offset: 0x000FCE44
    public static IReadOnlyList<AgentHarnessPlace> Places(this AgentHarness harness)
    {
      return new <>z__ReadOnlyArray<AgentHarnessPlace>(new AgentHarnessPlace[]
      {
        AgentHarnessPlace.Mac,
        AgentHarnessPlace.Cloud
      });
    }

    // Token: 0x06000773 RID: 1907 RVA: 0x000FCE58 File Offset: 0x000FCE58
    [NullableContext(2)]
    public static AgentHarness? ParseHarness(string raw)
    {
      string text = ((raw != null) ? raw.Trim().ToLowerInvariant() : null);
      AgentHarness? agentHarness;
      if (!(text == "codex"))
      {
        if (!(text == "pi"))
        {
          if (!(text == "h1"))
          {
            agentHarness = null;
          }
          else
          {
            agentHarness = new AgentHarness?(AgentHarness.H1);
          }
        }
        else
        {
          agentHarness = new AgentHarness?(AgentHarness.Pi);
        }
      }
      else
      {
        agentHarness = new AgentHarness?(AgentHarness.Codex);
      }
      return agentHarness;
    }

    // Token: 0x06000774 RID: 1908 RVA: 0x000FCEC8 File Offset: 0x000FCEC8
    public static string RawValue(this AgentHarnessPlace place)
    {
      if (place != AgentHarnessPlace.Cloud)
      {
        return "mac";
      }
      return "cloud";
    }

    // Token: 0x06000775 RID: 1909 RVA: 0x000FCEDC File Offset: 0x000FCEDC
    public static string DisplayName(this AgentHarnessPlace place)
    {
      if (place != AgentHarnessPlace.Cloud)
      {
        return "On this PC";
      }
      return "In the cloud";
    }

    // Token: 0x06000776 RID: 1910 RVA: 0x000FCEF0 File Offset: 0x000FCEF0
    [NullableContext(2)]
    public static AgentHarnessPlace? ParsePlace(string raw)
    {
      string text = ((raw != null) ? raw.Trim().ToLowerInvariant() : null);
      AgentHarnessPlace? agentHarnessPlace;
      if (!(text == "mac"))
      {
        if (!(text == "cloud"))
        {
          agentHarnessPlace = null;
        }
        else
        {
          agentHarnessPlace = new AgentHarnessPlace?(AgentHarnessPlace.Cloud);
        }
      }
      else
      {
        agentHarnessPlace = new AgentHarnessPlace?(AgentHarnessPlace.Mac);
      }
      return agentHarnessPlace;
    }

    // Token: 0x170001DD RID: 477
    // (get) Token: 0x06000777 RID: 1911 RVA: 0x000FCF48 File Offset: 0x000FCF48
    public static IReadOnlyList<AgentHarnessModel> AllModels { get; } = Enum.GetValues<AgentHarnessModel>();

    // Token: 0x06000778 RID: 1912 RVA: 0x000FCF50 File Offset: 0x000FCF50
    public static string RawValue(this AgentHarnessModel model)
    {
      string text;
      switch (model)
      {
      case AgentHarnessModel.Astra:
        text = "gpt-6-astra";
        break;
      case AgentHarnessModel.Sol:
        text = "gpt-6.1-sol";
        break;
      case AgentHarnessModel.Terra:
        text = "gpt-6.1-terra";
        break;
      case AgentHarnessModel.Luna:
        text = "gpt-6-luna";
        break;
      case AgentHarnessModel.Fable:
        text = "claude-fable-5-1";
        break;
      case AgentHarnessModel.Opus:
        text = "claude-opus-5-5";
        break;
      case AgentHarnessModel.Sonnet:
        text = "claude-sonnet-5-5";
        break;
      case AgentHarnessModel.Haiku:
        text = "claude-haiku-5-5";
        break;
      default:
        text = "default";
        break;
      }
      return text;
    }

    // Token: 0x06000779 RID: 1913 RVA: 0x000FCFD0 File Offset: 0x000FCFD0
    public static string DisplayName(this AgentHarnessModel model)
    {
      string text;
      switch (model)
      {
      case AgentHarnessModel.Astra:
        text = "GPT-6 Astra";
        break;
      case AgentHarnessModel.Sol:
        text = "GPT-6.1 Sol";
        break;
      case AgentHarnessModel.Terra:
        text = "GPT-6.1 Terra";
        break;
      case AgentHarnessModel.Luna:
        text = "GPT-6 Luna";
        break;
      case AgentHarnessModel.Fable:
        text = "Claude Fable 5.1";
        break;
      case AgentHarnessModel.Opus:
        text = "Claude Opus 5.5";
        break;
      case AgentHarnessModel.Sonnet:
        text = "Claude Sonnet 5.5";
        break;
      case AgentHarnessModel.Haiku:
        text = "Claude Haiku 5.5";
        break;
      default:
        text = "Default";
        break;
      }
      return text;
    }

    // Token: 0x0600077A RID: 1914 RVA: 0x000FD050 File Offset: 0x000FD050
    public static bool IsClaude(this AgentHarnessModel model)
    {
      return model - AgentHarnessModel.Fable <= 3;
    }

    // Token: 0x0600077B RID: 1915 RVA: 0x000FD06C File Offset: 0x000FD06C
    [NullableContext(2)]
    public static AgentHarnessModel ParseModel(string raw)
    {
      return AgentHarnessNames.AllModels.FirstOrDefault<AgentHarnessModel>((AgentHarnessModel model) => model.RawValue() == raw);
    }
  }
}
