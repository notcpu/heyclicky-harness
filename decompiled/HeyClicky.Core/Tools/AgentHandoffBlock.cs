using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Tools
{
  // Token: 0x02000099 RID: 153
  [NullableContext(1)]
  [Nullable(0)]
  public static class AgentHandoffBlock
  {
    // Token: 0x060009A9 RID: 2473 RVA: 0x0036E494 File Offset: 0x0036E494
    public static bool IsPresent(string text)
    {
      return AgentHandoffBlock.OpeningTags.Any<string>((string tag) => text.Contains(tag, StringComparison.Ordinal));
    }

    // Token: 0x0400045C RID: 1116
    public const string OpeningTag = "<AGENT_HANDOFF>";

    // Token: 0x0400045D RID: 1117
    public const string ClosingTag = "</AGENT_HANDOFF>";

    // Token: 0x0400045E RID: 1118
    public const string UserRequestHeader = "Original user request (typed/STT text):";

    // Token: 0x0400045F RID: 1119
    public const string BriefHeader = "HeyClicky's brief:";

    // Token: 0x04000460 RID: 1120
    public const string HowToReadHeader = "How to read this:";

    // Token: 0x04000461 RID: 1121
    public static readonly string[] OpeningTags = new string[] { "<AGENT_HANDOFF>", "<REALTIME_AGENT_HANDOFF>" };

    // Token: 0x04000462 RID: 1122
    public static readonly string[] ClosingTags = new string[] { "</AGENT_HANDOFF>", "</REALTIME_AGENT_HANDOFF>" };

    // Token: 0x04000463 RID: 1123
    public static readonly string[] BriefHeaders = new string[] { "HeyClicky's brief:", "Realtime-inferred task:" };

    // Token: 0x04000464 RID: 1124
    public static readonly string[] AfterBriefHeaders = new string[] { "How to read this:", "How to read this (", "Bounded recent Realtime conversation context" };
  }
}
