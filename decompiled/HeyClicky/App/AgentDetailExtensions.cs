using System;
using System.Runtime.CompilerServices;
using HeyClicky.Agents;

namespace HeyClicky.App
{
  // Token: 0x02000078 RID: 120
  internal static class AgentDetailExtensions
  {
    // Token: 0x0600047D RID: 1149 RVA: 0x00529AD4 File Offset: 0x00529AD4
    [NullableContext(1)]
    [return: Nullable(2)]
    public static string FinalResponseOrSummary(this AgentTaskDetail d)
    {
      if (string.IsNullOrWhiteSpace(d.Snapshot.FinalResponseMarkdown))
      {
        return d.Summary;
      }
      return d.Snapshot.FinalResponseMarkdown;
    }
  }
}
